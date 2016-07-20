Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Al.Logic
    ''' <summary>
    ''' 基本F品番を解決する事を担う実装クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BfBuhinNoResolverImpl : Implements BfBuhinNoResolver
        Private instlDao As TShisakuSekkeiBlockInstlDao = New TShisakuSekkeiBlockInstlDaoImpl
        Private aSekkeiTenkaiBlockDao As SekkeiBlockDao = New SekkeiBlockDaoImpl

        ''' <summary>
        ''' INSTL品番を基本F品番へ解決する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <returns>基本F品番</returns>
        ''' <remarks></remarks>
        Public Function Resolve(ByVal shisakuEventCode As String, ByVal instlHinban As String, ByVal instlHinbanKbn As String, ByVal colorUmu As String) As String Implements BfBuhinNoResolver.Resolve
            ' T_SHISAKU_EVENT_BASE を元に、RHAC1500 を抽出して、同一の付加F品番があれば、基本F品番を返す
            'まずは色有りのＡＬでチェック
            Dim alVos As List(Of SekkeiBlockAlResultVo) = aSekkeiTenkaiBlockDao.FindAlByShisakuEventBase(shisakuEventCode)
            For Each vo As SekkeiBlockAlResultVo In alVos
                If instlHinban.Equals(vo.FfBuhinNo) Then
                    Return vo.BfBuhinNo
                End If
            Next
            '' T_SHISAKU_EVENT_BASE を元に、RHAC1500 を抽出して、同一の付加F品番があれば、基本F品番を返す
            ''色無しのＡＬでもチェック
            'Dim alVosIroNashi As List(Of SekkeiBlockAlResultVo) = aSekkeiTenkaiBlockDao.FindAlByShisakuEventBaseIroNashi(shisakuEventCode)
            'For Each vo As SekkeiBlockAlResultVo In alVosIroNashi
            '    If instlHinban.Equals(vo.FfBuhinNo) Then
            '        Return vo.BfBuhinNo
            '    End If
            'Next

            'ＡＬの素で同一の品番が無ければ試作情報をチェック
            Dim param As New TShisakuSekkeiBlockInstlVo
            param.InstlHinban = instlHinban
            param.InstlHinbanKbn = instlHinbanKbn
            Dim instlVos As List(Of TShisakuSekkeiBlockInstlVo) = instlDao.FindBy(param)
            If 0 < instlVos.Count Then
                instlVos.Sort(New DescendingUpdated)
                Return instlVos(0).BfBuhinNo
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' 更新日時で降順にする IComparer実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class DescendingUpdated : Implements IComparer(Of TShisakuSekkeiBlockInstlVo)

            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockInstlVo, ByVal y As ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockInstlVo) As Integer Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.TShisakuSekkeiBlockInstlVo).Compare
                Dim result As Integer = y.UpdatedDate.CompareTo(x.UpdatedDate)
                If result <> 0 Then
                    Return result
                End If
                Return y.UpdatedTime.CompareTo(x.UpdatedTime)
            End Function
        End Class

    End Class
End NameSpace