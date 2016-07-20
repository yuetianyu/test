Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Util.Grouping
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Logic.Impl.Tenkai
    Public Class SekkeiBlockSupplierOLD
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly unitKbn As String
        Private ReadOnly dao As SekkeiBlockDao
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo)
            Me.New(shisakuEventVo, New SekkeiBlockDaoImpl)
        End Sub
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo, _
                       ByVal dao As SekkeiBlockDao)
            Me.shisakuEventCode = shisakuEventVo.ShisakuEventCode
            Me.unitKbn = shisakuEventVo.UnitKbn
            Me.dao = dao
        End Sub

        Public Function MakeRegisterValues() As List(Of TShisakuSekkeiBlockVo)
            Dim alVos As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase(shisakuEventCode)
            Dim alVosNashi As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase(shisakuEventCode)

            Dim blockNames As Dictionary(Of String, String) = ExtractBlockNames(alVos, alVosNashi)

            Dim blockNos As List(Of BukaCodeBlockNoVo) = GroupBukaCodeBlockNos(alVos, alVosNashi)

            Dim results As New List(Of TShisakuSekkeiBlockVo)

            For Each instl As BukaCodeBlockNoVo In blockNos

                Dim vo As New TShisakuSekkeiBlockVo
                results.Add(vo)

                vo.ShisakuEventCode = shisakuEventCode
                vo.ShisakuBukaCode = instl.BukaCode
                vo.ShisakuBlockNoHyoujiJun = instl.HyoujiJun
                vo.ShisakuBlockNo = instl.BlockNo
                vo.ShisakuBlockNoKaiteiNo = TShisakuSekkeiBlockVoHelper.ShisakuBlockNoKaiteiNo.DEFAULT_VALUE
                vo.BlockFuyou = TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY
                vo.Jyoutai = String.Empty

                'マスターからブロックに該当するユニット区分を取得する。
                Dim MUnitKbn As Rhac0080Vo = dao.FindShisakuBlockUnit(instl.BlockNo)
                If instl.BlockNo = "604A" Then
                    MUnitKbn.MtKbn = "M"
                End If
                'vo.UnitKbn = unitKbn
                vo.UnitKbn = MUnitKbn.MtKbn

                vo.ShisakuBlockName = blockNames(instl.BlockNo)
                vo.UserId = String.Empty
                vo.TelNo = String.Empty
                vo.SaisyuKoushinbi = Nothing
                vo.SaisyuKoushinjikan = Nothing
                vo.Memo = String.Empty
                vo.TantoSyouninJyoutai = String.Empty
                vo.TantoSyouninKa = String.Empty
                vo.TantoSyouninSya = String.Empty
                vo.TantoSyouninHi = Nothing
                vo.TantoSyouninJikan = Nothing
                vo.KachouSyouninJyoutai = String.Empty
                vo.KachouSyouninKa = String.Empty
                vo.KachouSyouninSya = String.Empty
                vo.KachouSyouninHi = Nothing
                vo.KachouSyouninJikan = Nothing
                vo.KaiteiHandanFlg = String.Empty
            Next
            Return results
        End Function

        ''' <summary>
        ''' 試作設計ブロック情報へ登録する
        ''' </summary>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="shisakuSekkeiBlockDao">試作設計ブロック情報Dao</param>
        ''' <param name="aShisakuDate">試作システム日付</param>
        ''' <remarks></remarks>
        Public Sub Register(ByVal login As LoginInfo, _
                            ByVal shisakuSekkeiBlockDao As TShisakuSekkeiBlockDao, _
                            ByVal aShisakuDate As ShisakuDate)

            Dim vos As List(Of TShisakuSekkeiBlockVo) = MakeRegisterValues()

            For Each vo As TShisakuSekkeiBlockVo In vos
                '重複キーが無いかチェック。
                If shisakuSekkeiBlockDao.FindByPk(vo.ShisakuEventCode, _
                                  vo.ShisakuBukaCode, _
                                  vo.ShisakuBlockNo, _
                                  vo.ShisakuBlockNoKaiteiNo) Is Nothing Then

                    vo.CreatedUserId = login.UserId
                    vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                    vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                    vo.UpdatedUserId = login.UserId
                    vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                    vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat
                    '追加
                    shisakuSekkeiBlockDao.InsertBy(vo)
                End If
            Next
        End Sub

        Private Class BukaBlockGroup : Implements VoGroupingRule(Of BukaCodeBlockNoVo)
            Public Sub GroupRule(ByVal groupBy As VoGroupingLocator, ByVal vo As BukaCodeBlockNoVo) Implements VoGroupingRule(Of BukaCodeBlockNoVo).GroupRule
                groupBy.Prop(vo.BukaCode).Prop(vo.BlockNo)
            End Sub
        End Class

        Private Class BukaBlockComparer : Implements IComparer(Of BukaCodeBlockNoVo)
            Public Function Compare(ByVal x As BukaCodeBlockNoVo, ByVal y As BukaCodeBlockNoVo) As Integer Implements IComparer(Of BukaCodeBlockNoVo).Compare
                Dim resultA As Integer = x.BukaCode.CompareTo(y.BukaCode)
                If resultA <> 0 Then
                    Return resultA
                End If
                Return x.BlockNo.CompareTo(y.BlockNo)
            End Function
        End Class

        ''' <summary>
        ''' 設計課とブロックNoでグループ化した情報を返す
        ''' </summary>
        ''' <param name="alVos">A/Lの素の一覧</param>
        ''' <param name="alVosNashi">A/Lの素の一覧（）</param>
        ''' <returns>グループ化した情報</returns>
        ''' <remarks></remarks>
        Public Function GroupBukaCodeBlockNos(ByVal alVos As List(Of SekkeiBlockAlResultVo), _
                                              ByVal alVosNashi As List(Of SekkeiBlockAlResultVo) _
                                              ) As List(Of BukaCodeBlockNoVo)

            Dim results As New List(Of BukaCodeBlockNoVo)
            For Each alVo As SekkeiBlockAlResultVo In alVos
                'マスターからブロックに該当するユニット区分を取得する。
                Dim MUnitKbn As Rhac0080Vo = dao.FindShisakuBlockUnit(alVo.BlockNo)
                If alVo.BlockNo = "604A" Then
                    MUnitKbn.MtKbn = "M"
                End If
                'イベント情報のユニット区分に該当するデータのみ処理する。
                ' S:全て対象
                ' M:M、ブランクのみ対象
                ' T:T、ブランクのみ対象
                If unitKbn <> "S" Then
                    If unitKbn <> MUnitKbn.MtKbn And Not StringUtil.IsEmpty(MUnitKbn.MtKbn) Then
                        '条件に該当しないので次のデータへ
                        Continue For
                    End If
                End If
                'データを追加
                Dim vo As New BukaCodeBlockNoVo

                'マスタに無い場合、課略名をセットします。　2011/2/3 by柳沼
                If StringUtil.IsEmpty(alVo.BuCode & alVo.KaCode) Then
                    vo.BukaCode = alVo.BukaCode  '課略名
                Else
                    vo.BukaCode = alVo.BuCode & alVo.KaCode  '部コードと課コード
                End If

                vo.BlockNo = alVo.BlockNo
                results.Add(vo)
            Next
            For Each alVo As SekkeiBlockAlResultVo In alVosNashi
                'マスターからブロックに該当するユニット区分を取得する。
                Dim MUnitKbn As Rhac0080Vo = dao.FindShisakuBlockUnit(alVo.BlockNo)
                If alVo.BlockNo = "604A" Then
                    MUnitKbn.MtKbn = "M"
                End If
                'イベント情報のユニット区分に該当するデータのみ処理する。
                ' S:全て対象
                ' M:M、ブランクのみ対象
                ' T:T、ブランクのみ対象
                If unitKbn <> "S" Then
                    If unitKbn <> MUnitKbn.MtKbn And Not StringUtil.IsEmpty(MUnitKbn.MtKbn) Then
                        '条件に該当しないので次のデータへ
                        Continue For
                    End If
                End If
                'データを追加
                Dim vo As New BukaCodeBlockNoVo

                'マスタに無い場合、課略名をセットします。　2011/2/3 by柳沼
                If StringUtil.IsEmpty(alVo.BuCode & alVo.KaCode) Then
                    vo.BukaCode = alVo.BukaCode  '課略名
                Else
                    vo.BukaCode = alVo.BuCode & alVo.KaCode  '部コードと課コード
                End If

                vo.BlockNo = alVo.BlockNo
                results.Add(vo)
            Next

            Dim groupInstl As New VoGrouping(Of BukaCodeBlockNoVo)(New BukaBlockGroup)

            results = groupInstl.Grouping(results)
            results.Sort(New BukaBlockComparer)

            Dim backBukaCode As String = String.Empty
            Dim hyoujiJun As Integer

            For Each vo As BukaCodeBlockNoVo In results
                If vo.BukaCode.Equals(backBukaCode) Then
                    hyoujiJun += 1
                Else
                    hyoujiJun = TShisakuSekkeiBlockVoHelper.ShisakuBlockNoHyoujiJun.START_VALUE
                    backBukaCode = vo.BukaCode
                End If
                vo.HyoujiJun = hyoujiJun
            Next
            Return results
        End Function

        ''' <summary>
        ''' ブロックNoに対する名称をもつDictionaryを返す
        ''' </summary>
        ''' <param name="alVos">A/Lの素の一覧</param>
        ''' <param name="alVosNashi">A/Lの素の一覧（色無し）</param>
        ''' <returns>ブロック名称をもつDictionary</returns>
        ''' <remarks></remarks>
        Public Function ExtractBlockNames(ByVal alVos As List(Of SekkeiBlockAlResultVo), ByVal alVosNashi As List(Of SekkeiBlockAlResultVo)) As Dictionary(Of String, String)

            Dim blockNames As New Dictionary(Of String, String)
            For Each vo As SekkeiBlockAlResultVo In alVos
                If blockNames.ContainsKey(vo.BlockNo) Then
                    Continue For
                End If
                blockNames.Add(vo.BlockNo, vo.BlockName)
            Next
            For Each vo As SekkeiBlockAlResultVo In alVosNashi
                If blockNames.ContainsKey(vo.BlockNo) Then
                    Continue For
                End If
                blockNames.Add(vo.BlockNo, vo.BlockName)
            Next
            Return blockNames
        End Function
    End Class
End Namespace