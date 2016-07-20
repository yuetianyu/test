Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Merge
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Tree

Namespace ShisakuBuhinEdit.Kosei.Logic.Matrix
    ''' <summary>
    ''' 試作部品構成情報と試作部品情報とで部品表を作成するメソッドクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MakeShisakuBlockKeyImpl
        Implements MakeShisakuBlockKey

        Private ReadOnly buhinDao As TShisakuBuhinDao
        Private ReadOnly buhinKouseiDao As TShisakuBuhinKouseiDao
        Private ReadOnly blockInstlDao As TShisakuSekkeiBlockInstlDao
        Private ReadOnly koseiDao As MakeShisakuBlockDao

        Public Sub New()
            Me.New(New TShisakuBuhinDaoImpl, New TShisakuBuhinKouseiDaoImpl, New TShisakuSekkeiBlockInstlDaoImpl, New MakeShisakuBlockDaoImpl)
        End Sub
        Public Sub New(ByVal buhinDao As TShisakuBuhinDao, ByVal buhinKouseiDao As TShisakuBuhinKouseiDao, ByVal blockInstlDao As TShisakuSekkeiBlockInstlDao, ByVal koseiDao As MakeShisakuBlockDao)
            Me.buhinDao = buhinDao
            Me.buhinKouseiDao = buhinKouseiDao
            Me.blockInstlDao = blockInstlDao
            Me.koseiDao = koseiDao
        End Sub

        ''' <summary>
        ''' 試作部品構成情報と試作部品情報とで部品表を作成する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Public Function Compute(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As BuhinKoseiMatrix Implements MakeShisakuBlockKey.Compute
            ' 存在しなかったら、TShisakuBuhinKousei を丸々参照して、BuhinTreeMaker → BuhinSingleMatrix → MergeNodeList  ※員数は「A/L窓のINSTL品番表示」に合せる
            Dim buhinVos As List(Of TShisakuBuhinVo) = FindBuhinBy(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, shisakuBlockNoKaiteiNo)
            Dim kouseiVos As List(Of TShisakuBuhinKouseiVo) = FindBuhinKouseiBy(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, shisakuBlockNoKaiteiNo)
            Dim rhac0610Vos As List(Of Rhac0610Vo) = koseiDao.FindMakerByShisakuBuhin(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, shisakuBlockNoKaiteiNo)

            Dim singleMatrices As BuhinSingleMatrix(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)() = BuhinTreeMaker.NewSingleMatrices(kouseiVos, buhinVos)

            Dim sortedSingleMatrices As BuhinSingleMatrix(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)() = SortByBlockInstl(singleMatrices, shisakuEventCode, shisakuBukaCode, shisakuBlockNo, shisakuBlockNoKaiteiNo)

            Dim merge As New MergeNodeList(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)(New ShisakuNodeAccessor(New MakeStructureResultImpl.MakerNameResolverImpl(rhac0610Vos)))
            ' TODO sortedSingleMatrices が Nodeなら、for eachする必要ないんだが、、、
            ' INSTLの表示順で、Mergeする
            For Each singleMatrix As BuhinSingleMatrix(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) In sortedSingleMatrices
                merge.Compute(singleMatrix.Nodes)
            Next

            Return merge.ResultMatrix

        End Function

        ''' <summary>
        ''' 試作設計ブロックINSTL情報のINSTL品番表示順で並び替えする
        ''' </summary>
        ''' <param name="singleMatrices">構成Nodeクラス</param>
        ''' <returns>並び替え済みの構成Nodeクラス</returns>
        ''' <remarks></remarks>
        Private Function SortByBlockInstl(ByVal singleMatrices As BuhinSingleMatrix(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)(), ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As BuhinSingleMatrix(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)()

            Dim blockInstlVos As List(Of TShisakuSekkeiBlockInstlVo) = FindBlockInstlOrderBy(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, shisakuBlockNoKaiteiNo)
            If blockInstlVos.Count = 0 Then
                Return singleMatrices   ' INSTL品番情報が無ければ、値をそのまま返す
            End If

            Dim singleMatrixByBuhinNo As New Dictionary(Of String, BuhinSingleMatrix(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo))
            For Each singleMatrix As BuhinSingleMatrix(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo) In singleMatrices
                If singleMatrixByBuhinNo.ContainsKey(singleMatrix.RootBuhinNo) Then
                    Continue For
                End If
                singleMatrixByBuhinNo.Add(singleMatrix.RootBuhinNo, singleMatrix)
            Next

            Dim results As New List(Of BuhinSingleMatrix(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo))
            For Each instlVo As TShisakuSekkeiBlockInstlVo In blockInstlVos
                If Not singleMatrixByBuhinNo.ContainsKey(instlVo.BfBuhinNo) Then
                    Throw New ShisakuDbException(String.Format("INSTL品番 {0} の基本F品番 {1} に該当する構成情報がありません.", instlVo.InstlHinban, instlVo.BfBuhinNo))
                End If
                results.Add(singleMatrixByBuhinNo(instlVo.BfBuhinNo))
            Next
            Return results.ToArray
        End Function

        Private Class BlockInstlComparer : Implements IComparer(Of TShisakuSekkeiBlockInstlVo)
            ''' <summary>
            ''' INSTL品番表示順でソート
            ''' </summary>
            Public Function Compare(ByVal x As TShisakuSekkeiBlockInstlVo, ByVal y As TShisakuSekkeiBlockInstlVo) As Integer Implements IComparer(Of TShisakuSekkeiBlockInstlVo).Compare
                ' 前提条件
                ' 表示順は、部課コードとブロック№とで一意になる
                If x.InstlHinbanHyoujiJun Is Nothing AndAlso y.InstlHinbanHyoujiJun Is Nothing Then
                    Return 0
                ElseIf x.InstlHinbanHyoujiJun Is Nothing Then
                    Return -1
                ElseIf y.InstlHinbanHyoujiJun Is Nothing Then
                    Return 1
                End If
                Return CInt(x.InstlHinbanHyoujiJun).CompareTo(CInt(y.InstlHinbanHyoujiJun))
            End Function
        End Class

        Private Function FindBlockInstlOrderBy(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuSekkeiBlockInstlVo)
            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            Dim instlVos As List(Of TShisakuSekkeiBlockInstlVo) = blockInstlDao.FindBy(param)
            instlVos.Sort(New BlockInstlComparer)
            Return instlVos
        End Function

        Private Function FindBuhinBy(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinVo)
            Dim param As New TShisakuBuhinVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            Return buhinDao.FindBy(param)
        End Function

        Private Function FindBuhinKouseiBy(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinKouseiVo)
            Dim param As New TShisakuBuhinKouseiVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            '廃止日が99999999のもの限定の処理を追加 樺澤'
            param.HaisiDate = TShisakuBuhinKouseiVoHelper.HaisiDate.NOW_EFFECTIVE_DATE
            Return buhinKouseiDao.FindBy(param)
        End Function
    End Class
End Namespace