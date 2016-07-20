Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Util.Grouping
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Dao
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect

Namespace ShisakuBuhinMenu.Logic.Impl.Tenkai
    Public Class SekkeiBlockInstlSupplierOLD
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly UnitKbn As String
        Private ReadOnly dao As SekkeiBlockDao

        Private ReadOnly makeDao As New MakeStructureResultDaoImpl

        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo)
            Me.New(shisakuEventVo.ShisakuEventCode, New SekkeiBlockDaoImpl, shisakuEventVo.UnitKbn)
            Me.UnitKbn = shisakuEventVo.UnitKbn
        End Sub
        Public Sub New(ByVal shisakuEventCode As String, ByVal dao As SekkeiBlockDao, ByVal Unitkbn As String)
            Me.shisakuEventCode = shisakuEventCode
            Me.dao = dao
        End Sub

        Public Function MakeRegisterValues() As List(Of TShisakuSekkeiBlockInstlVo)
            Dim alVos As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase(shisakuEventCode)
            Dim alVosNashi As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase(shisakuEventCode)

            Dim groupedVos As List(Of BukaCodeBlockNoVo) = GroupBukaCodeBlockNos(alVos, alVosNashi, shisakuEventCode)

            Dim results As New List(Of TShisakuSekkeiBlockInstlVo)
            For Each group As BukaCodeBlockNoVo In groupedVos
                Dim vo As New TShisakuSekkeiBlockInstlVo
                results.Add(vo)

                '色付き品番の場合、構成マスタに色付きで登録されているとは限らない。
                ' 例）　****AV　⇒　****##
                '　構成マスタ（552⇒553⇒551の順番で）を色付き品番で検索し、
                '　該当データが無ければ##品番に置き換えて以下のロジックから使用する。

                Dim wHinban As String = Nothing
                '12桁以外の場合処理無し。
                If group.FfBuhinNo Is Nothing OrElse group.FfBuhinNo.Length < 12 Then
                    wHinban = group.FfBuhinNo
                Else
                    'まずは552をチェック
                    Dim Rhac0552Vo As New Rhac0552Vo
                    Rhac0552Vo = makeDao.FindByBuhinRhac0552(group.FfBuhinNo)
                    '次に553をチェック
                    Dim KaihatsuFugo As New TShisakuEventBaseVo
                    KaihatsuFugo = makeDao.FindByBase(shisakuEventCode, group.Gosha)
                    Dim Rhac0553Vo As New Rhac0553Vo
                    Rhac0553Vo = makeDao.FindByBuhinRhac0553(KaihatsuFugo.BaseKaihatsuFugo, group.FfBuhinNo)
                    '最後に551をチェック
                    Dim Rhac0551Vo As New Rhac0551Vo
                    Rhac0551Vo = makeDao.FindByBuhinRhac0551(group.FfBuhinNo)
                    '全てのテーブルに存在しない場合は、基本Ｆ品番の色コードを＃＃に置き換える。
                    If Rhac0552Vo Is Nothing And Rhac0553Vo Is Nothing And Rhac0551Vo Is Nothing Then
                        wHinban = Conv0532HinbanByRule(group.FfBuhinNo)
                    Else
                        wHinban = group.FfBuhinNo
                    End If
                End If
                vo.BfBuhinNo = wHinban

                vo.ShisakuEventCode = shisakuEventCode
                vo.ShisakuBukaCode = group.BukaCode
                vo.ShisakuBlockNo = group.BlockNo
                vo.ShisakuBlockNoKaiteiNo = TShisakuSekkeiBlockInstlVoHelper.ShisakuBlockNoKaiteiNo.DEFAULT_VALUE
                vo.ShisakuGousya = group.Gosha
                vo.InstlHinbanHyoujiJun = group.HyoujiJun
                vo.InstlHinban = group.FfBuhinNo
                vo.InstlHinbanKbn = group.InstlHinbanKbn
                'vo.BfBuhinNo = group.BfBuhinNo
                vo.InsuSuryo = 1
                vo.SaisyuKoushinbi = Nothing
            Next
            Return results
        End Function

        ''' <summary>
        ''' 品番を##品番にして返す
        ''' </summary>
        ''' <param name="instlHinban">品番</param>
        ''' <returns>##品番</returns>
        ''' <remarks></remarks>
        Private Function Conv0532HinbanByRule(ByVal instlHinban As String) As String

            If instlHinban Is Nothing OrElse instlHinban.Length < 12 Then
                Return instlHinban
            End If
            Return instlHinban.Substring(0, 10) & "##" & instlHinban.Substring(12)
        End Function

        ''' <summary>
        ''' 試作設計ブロックINSTL情報へ登録する
        ''' </summary>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="instlDao">試作設計ブロックINSTL情報Dao</param>
        ''' <param name="aShisakuDate">試作システム日付</param>
        ''' <remarks></remarks>
        Public Sub Register(ByVal login As LoginInfo, _
                            ByVal instlDao As TShisakuSekkeiBlockInstlDao, _
                            ByVal aShisakuDate As ShisakuDate)

            Dim vos As List(Of TShisakuSekkeiBlockInstlVo) = MakeRegisterValues()

            For Each vo As TShisakuSekkeiBlockInstlVo In vos
                '重複キーが無いかチェック。
                If instlDao.FindByPk(vo.ShisakuEventCode, _
                                  vo.ShisakuBukaCode, _
                                  vo.ShisakuBlockNo, _
                                  vo.ShisakuBlockNoKaiteiNo, _
                                  vo.ShisakuGousya, _
                                  vo.InstlHinban, _
                                  vo.InstlHinbanKbn) Is Nothing Then

                    vo.CreatedUserId = login.UserId
                    vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                    vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                    vo.UpdatedUserId = login.UserId
                    vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                    vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat
                    '追加
                    instlDao.InsertBy(vo)
                End If

            Next
        End Sub

        Private Class RecordGroupingRule : Implements VoGroupingRule(Of BukaCodeBlockNoVo)
            Public Sub GroupRule(ByVal groupBy As VoGroupingLocator, ByVal vo As BukaCodeBlockNoVo) Implements VoGroupingRule(Of BukaCodeBlockNoVo).GroupRule
                groupBy.Prop(vo.BukaCode).Prop(vo.BlockNo).Prop(vo.Gosha).Prop(vo.FfBuhinNo).Prop(vo.InstlHinbanKbn).Prop(vo.BfBuhinNo)
            End Sub
        End Class

        Private Class BukaBlockComparer : Implements IComparer(Of BukaCodeBlockNoVo)
            Public Function Compare(ByVal x As BukaCodeBlockNoVo, ByVal y As BukaCodeBlockNoVo) As Integer Implements IComparer(Of BukaCodeBlockNoVo).Compare
                Dim resultA As Integer = x.BukaCode.CompareTo(y.BukaCode)
                If resultA <> 0 Then
                    Return resultA
                End If
                Dim resultB As Integer = x.BlockNo.CompareTo(y.BlockNo)
                If resultB <> 0 Then
                    Return resultB
                End If
                Dim resultC As Integer = x.FfBuhinNo.CompareTo(y.FfBuhinNo)
                If resultC <> 0 Then
                    Return resultC
                End If
                Dim resultD As Integer = x.InstlHinbanKbn.CompareTo(y.InstlHinbanKbn)
                If resultD <> 0 Then
                    Return resultD
                End If
                Dim resultE As Integer = x.BfBuhinNo.CompareTo(y.BfBuhinNo)
                If resultE <> 0 Then
                    Return resultE
                End If
                Return x.Gosha.CompareTo(y.Gosha)
            End Function
        End Class

        ''' <summary>
        ''' 設計課とブロックNo・号車・付加F品番でグループ化した情報を返す
        ''' </summary>
        ''' <param name="alVos">A/Lの素の一覧</param>
        ''' <param name="alVosNashi">A/Lの素の一覧（色無し）</param>
        ''' <returns>グループ化した情報</returns>
        ''' <remarks>表示順は、設計課＆ブロックNoごと</remarks>
        Public Function GroupBukaCodeBlockNos(ByVal alVos As List(Of SekkeiBlockAlResultVo), _
                                              ByVal alVosNashi As List(Of SekkeiBlockAlResultVo), ByVal shisakuEventCode As String _
                                              ) As List(Of BukaCodeBlockNoVo)

            Dim packedVo As New List(Of BukaCodeBlockNoVo)
            Dim MUnitKbn As New Rhac0080Vo
            For Each alVo As SekkeiBlockAlResultVo In alVos
                'マスターからブロックに該当するユニット区分を取得する。
                MUnitKbn = dao.FindShisakuBlockUnit(alVo.BlockNo)
                If alVo.BlockNo = "604A" Then
                    MUnitKbn.MtKbn = "M"
                End If
                'イベント情報のユニット区分に該当するデータのみ処理する。
                ' S:全て対象
                ' M:M、ブランクのみ対象
                ' T:T、ブランクのみ対象
                If UnitKbn <> "S" Then
                    If UnitKbn <> MUnitKbn.MtKbn And Not StringUtil.IsEmpty(MUnitKbn.MtKbn) Then
                        '条件に該当しないので次のデータへ
                        Continue For
                    End If
                End If
                'データ登録
                Rhac1500VoHelper.ResolveRule(alVo)
                Dim vo As New BukaCodeBlockNoVo

                'マスタに無い場合、課略名をセットします。　2011/2/3 by柳沼
                If StringUtil.IsEmpty(alVo.BuCode & alVo.KaCode) Then
                    vo.BukaCode = alVo.BukaCode  '課略名
                Else
                    vo.BukaCode = alVo.BuCode & alVo.KaCode  '部コードと課コード
                End If

                vo.BlockNo = alVo.BlockNo
                vo.Gosha = alVo.ShisakuGousya
                vo.FfBuhinNo = alVo.FfBuhinNo
                vo.InstlHinbanKbn = String.Empty
                vo.BfBuhinNo = StringUtil.Nvl(alVo.BfBuhinNo)
                packedVo.Add(vo)
            Next
            For Each alVoNashi As SekkeiBlockAlResultVo In alVosNashi
                'マスターからブロックに該当するユニット区分を取得する。
                MUnitKbn = dao.FindShisakuBlockUnit(alVoNashi.BlockNo)
                If alVoNashi.BlockNo = "604A" Then
                    MUnitKbn.MtKbn = "M"
                End If
                'イベント情報のユニット区分に該当するデータのみ処理する。
                ' S:全て対象
                ' M:M、ブランクのみ対象
                ' T:T、ブランクのみ対象
                If UnitKbn <> "S" Then
                    If UnitKbn <> MUnitKbn.MtKbn And Not StringUtil.IsEmpty(MUnitKbn.MtKbn) Then
                        '条件に該当しないので次のデータへ
                        Continue For
                    End If
                End If
                'データ登録
                Rhac1500VoHelper.ResolveRule(alVoNashi)
                Dim vo As New BukaCodeBlockNoVo
                vo.BukaCode = alVoNashi.BuCode & alVoNashi.KaCode

                'マスタに無い場合、課略名をセットします。　2011/2/3 by柳沼
                If StringUtil.IsEmpty(alVoNashi.BuCode & alVoNashi.KaCode) Then
                    vo.BukaCode = alVoNashi.BukaCode  '課略名
                Else
                    vo.BukaCode = alVoNashi.BuCode & alVoNashi.KaCode  '部コードと課コード
                End If

                vo.BlockNo = alVoNashi.BlockNo
                vo.Gosha = alVoNashi.ShisakuGousya
                vo.FfBuhinNo = alVoNashi.FfBuhinNo
                vo.InstlHinbanKbn = String.Empty
                vo.BfBuhinNo = StringUtil.Nvl(alVoNashi.BfBuhinNo)
                packedVo.Add(vo)
            Next

            Dim groupInstl As New VoGrouping(Of BukaCodeBlockNoVo)(New RecordGroupingRule)

            Dim results As List(Of BukaCodeBlockNoVo) = groupInstl.Grouping(packedVo)
            results.Sort(New BukaBlockComparer)

            Dim backBukaCode As String = String.Empty
            Dim backBlockNo As String = String.Empty
            Dim hinban As String = String.Empty
            Dim hinbanKbn As String = String.Empty
            Dim hyoujiJun As Integer
            Dim hyoujiJunMax As Integer
            Dim hyoujiJun2 As Integer

            Dim InstlVo As New TShisakuSekkeiBlockInstlVo
            Dim InstlVo2 As New TShisakuSekkeiBlockInstlVo

            For Each vo As BukaCodeBlockNoVo In results

                '該当データがあるならINSTL品番表示順の最大値を取得
                InstlVo = dao.FindShisakuBlockInstlHyoujiJun(shisakuEventCode, vo.BlockNo)
                If Not StringUtil.IsEmpty(InstlVo) Then
                    If Not StringUtil.IsEmpty(InstlVo.InstlHinbanHyoujiJun) Then
                        hyoujiJunMax = InstlVo.InstlHinbanHyoujiJun
                    Else
                        hyoujiJunMax = 0
                    End If
                Else
                    hyoujiJunMax = 0
                End If

                '該当データがあるならINSTL品番表示順を取得
                InstlVo2 = dao.FindShisakuBlockInstlHyoujiJun2(shisakuEventCode, vo.BlockNo, vo.FfBuhinNo)
                If Not StringUtil.IsEmpty(InstlVo2) Then
                    If Not StringUtil.IsEmpty(InstlVo2.InstlHinbanHyoujiJun) Then
                        hyoujiJun2 = InstlVo2.InstlHinbanHyoujiJun
                    Else
                        hyoujiJun2 = 0
                    End If
                Else
                    hyoujiJun2 = 0
                End If

                If vo.BukaCode.Equals(backBukaCode) AndAlso vo.BlockNo.Equals(backBlockNo) Then
                    If Not (EzUtil.IsEqualIfNull(hinban, vo.FfBuhinNo) AndAlso EzUtil.IsEqualIfNull(hinbanKbn, vo.InstlHinbanKbn)) Then

                        If hyoujiJun2 <> 0 Then
                            hyoujiJun = hyoujiJun2
                        Else
                            hyoujiJun += 1
                        End If

                        hinban = vo.FfBuhinNo
                        hinbanKbn = vo.InstlHinbanKbn
                    End If
                Else
                    If hyoujiJun2 <> 0 Then
                        hyoujiJun = hyoujiJun2
                    Else
                        If hyoujiJunMax <> 0 Then
                            hyoujiJun = hyoujiJunMax
                        Else
                            hyoujiJun = TShisakuSekkeiBlockInstlVoHelper.InstlHinbanHyoujiJun.START_VALUE
                        End If
                    End If

                    backBukaCode = vo.BukaCode
                    backBlockNo = vo.BlockNo
                    hinban = vo.FfBuhinNo
                    hinbanKbn = vo.InstlHinbanKbn
                End If
                vo.HyoujiJun = hyoujiJun
            Next
            Return results
        End Function

    End Class
End Namespace