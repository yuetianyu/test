Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Util.Grouping
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Dao
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports EventSakusei.ShisakuBuhinMenu.Logic.Impl.Tenkai
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EBom.Common

Namespace ShisakuBuhinEditBlock.Logic.Tenkai
    Public Class SekkeiBlockInstlSupplier
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly dao As SekkeiBlockDao
        Private ReadOnly daoBase As ShisakuEventBaseDao
        Private aDate As New ShisakuDate
        Private blockNoList As List(Of String)
        Private shisakuBukaCode As String
        Private instlDao As TShisakuSekkeiBlockInstlDao = New TShisakuSekkeiBlockInstlDaoImpl

        Private ReadOnly makeDao As New MakeStructureResultDaoImpl

        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal blockNoList As List(Of String))
            Me.New(shisakuEventCode, New SekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl, shisakuBukaCode, blockNoList)
        End Sub
        Public Sub New(ByVal shisakuEventCode As String, ByVal dao As SekkeiBlockDao, _
                       ByVal daoBase As ShisakuEventBaseDao, ByVal shisakuBukaCode As String, ByVal blockNoList As List(Of String))
            Me.shisakuEventCode = shisakuEventCode
            Me.blockNoList = blockNoList
            Me.dao = dao
            Me.daoBase = daoBase
            Me.shisakuBukaCode = shisakuBukaCode
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="ShisakuBlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MakeRegisterValues(ByVal ShisakuBlockNo As String) As List(Of TShisakuSekkeiBlockInstlVo)

            Dim alVosFinal As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase("A", "A")
            Dim alVosFinalwk As List(Of SekkeiBlockAlResultVo) = alVosFinal
            Dim alVosFinalOld As List(Of SekkeiBlockAlResultVo) = alVosFinal
            Dim alVosFinalOldwk As List(Of SekkeiBlockAlResultVo) = alVosFinal
            Dim alVosLast As List(Of SekkeiBlockAlResultVo) = alVosFinal
            Dim alVos As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBaseByBlock(shisakuEventCode, LoginInfo.Now.UserId, ShisakuBlockNo)
            Dim alVosOld As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBaseOLDByBlock(shisakuEventCode, LoginInfo.Now.UserId, ShisakuBlockNo)
            Dim alVos2 As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase2ByBlock(shisakuEventCode, LoginInfo.Now.UserId, ShisakuBlockNo)
            Dim alVosOld2 As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBaseOLD2ByBlock(shisakuEventCode, LoginInfo.Now.UserId, ShisakuBlockNo)

            Dim wFlg As String = Nothing
            Dim BLOCKNO As String = Nothing
            Dim SHISAKUGOUSYA As String = Nothing
            Dim FFBUHINNO As String = Nothing
            Dim TOPCOLORKAITEINO As String = Nothing

            For Each group As SekkeiBlockAlResultVo In alVos2
                If Not StringUtil.IsEmpty(group.KumiawaseCodeSo) Then
                    alVosFinal.Add(group)
                End If
            Next
            For Each group As SekkeiBlockAlResultVo In alVos
                wFlg = Nothing
                For Each group2 As SekkeiBlockAlResultVo In alVosFinal
                    If group.BlockNo = group2.BlockNo And _
                       group.ShisakuGousya = group2.ShisakuGousya And _
                       group.FfBuhinNo = group2.FfBuhinNo Then
                        wFlg = "NO"
                        Exit For
                    End If
                Next
                If wFlg <> "NO" Then
                    If BLOCKNO = group.BlockNo And _
                        SHISAKUGOUSYA = group.ShisakuGousya And _
                        FFBUHINNO = group.FfBuhinNo Then
                        If StringUtil.IsEmpty(TOPCOLORKAITEINO) And _
                            StringUtil.IsEmpty(group.TopColorKaiteiNo) Then
                            alVosFinalwk.Add(group)
                        ElseIf Not StringUtil.IsEmpty(TOPCOLORKAITEINO) And _
                                Not StringUtil.IsEmpty(group.TopColorKaiteiNo) Then
                            alVosFinalwk.Add(group)
                        Else
                            '2012/02/25
                            'No.118の修正で部品番号が抜けている対応
                            alVosFinalwk.Add(group)
                        End If
                    Else
                        alVosFinalwk.Add(group)
                    End If
                End If

                BLOCKNO = group.BlockNo
                SHISAKUGOUSYA = group.ShisakuGousya
                TOPCOLORKAITEINO = group.TopColorKaiteiNo
                FFBUHINNO = group.FfBuhinNo
            Next
            If alVosFinalwk.Count > 0 Then
                alVosFinal.AddRange(alVosFinalwk)
            End If

            For Each group As SekkeiBlockAlResultVo In alVosOld2
                If Not StringUtil.IsEmpty(group.KumiawaseCodeSo) Then
                    alVosFinalOld.Add(group)
                End If
            Next

            For Each groupOLD As SekkeiBlockAlResultVo In alVosOld
                wFlg = Nothing
                For Each groupOLD2 As SekkeiBlockAlResultVo In alVosFinalOld
                    If groupOLD.BlockNo = groupOLD2.BlockNo And _
                       groupOLD.ShisakuGousya = groupOLD2.ShisakuGousya And _
                       groupOLD.FfBuhinNo = groupOLD2.FfBuhinNo Then
                        wFlg = "NO"
                        Exit For
                    End If
                Next
                If wFlg <> "NO" Then
                    If BLOCKNO = groupOLD.BlockNo And _
                        SHISAKUGOUSYA = groupOLD.ShisakuGousya And _
                        FFBUHINNO = groupOLD.FfBuhinNo Then
                        If StringUtil.IsEmpty(TOPCOLORKAITEINO) And _
                            StringUtil.IsEmpty(groupOLD.TopColorKaiteiNo) Then
                            alVosFinalOldwk.Add(groupOLD)
                        ElseIf Not StringUtil.IsEmpty(TOPCOLORKAITEINO) And _
                                Not StringUtil.IsEmpty(groupOLD.TopColorKaiteiNo) Then
                            alVosFinalOldwk.Add(groupOLD)
                        End If
                    Else
                        alVosFinalOldwk.Add(groupOLD)
                    End If
                End If

                BLOCKNO = groupOLD.BlockNo
                SHISAKUGOUSYA = groupOLD.ShisakuGousya
                TOPCOLORKAITEINO = groupOLD.TopColorKaiteiNo
                FFBUHINNO = groupOLD.FfBuhinNo
            Next
            If alVosFinalOldwk.Count > 0 Then
                alVosFinalOld.AddRange(alVosFinalOldwk)
            End If

            For Each groupOLD As SekkeiBlockAlResultVo In alVosFinalOld
                wFlg = Nothing
                For Each group As SekkeiBlockAlResultVo In alVosFinal
                    If groupOLD.BlockNo = group.BlockNo And _
                       groupOLD.ShisakuGousya = group.ShisakuGousya And _
                       groupOLD.FfBuhinNo = group.FfBuhinNo Then
                        wFlg = "NO"
                        Exit For
                    End If
                Next
                If wFlg <> "NO" Then
                    alVosLast.Add(groupOLD)
                End If
            Next
            If alVosLast.Count > 0 Then
                alVosFinal.AddRange(alVosLast)
            End If

            Dim groupedVos As List(Of BukaCodeBlockNoVo) = GroupBukaCodeBlockNos(alVosFinal)

            Dim results As New List(Of TShisakuSekkeiBlockInstlVo)
            For Each group As BukaCodeBlockNoVo In groupedVos
                Dim vo As New TShisakuSekkeiBlockInstlVo
                results.Add(vo)

                '＃＃に変換して落とすとINSTLの0レベルが壊れるのでそのまま落とす。
                vo.BfBuhinNo = group.BfBuhinNo

                vo.ShisakuEventCode = shisakuEventCode
                vo.ShisakuBukaCode = group.BukaCode
                vo.ShisakuBlockNo = group.BlockNo
                vo.ShisakuBlockNoKaiteiNo = TShisakuSekkeiBlockInstlVoHelper.ShisakuBlockNoKaiteiNo.DEFAULT_VALUE
                vo.ShisakuGousya = group.Gosha
                vo.InstlHinbanHyoujiJun = group.HyoujiJun
                vo.InstlHinban = group.FfBuhinNo
                vo.InstlHinbanKbn = group.InstlHinbanKbn
                vo.InstlDataKbn = group.InstlDataKbn

                '2014/11/19 追加
                vo.BaseInstlFlg = "1"

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

            If instlHinban Is Nothing OrElse instlHinban.Length < 12 _
                OrElse instlHinban.Length >= 13 Then
                Return instlHinban
            End If
            Return instlHinban.Substring(0, 10) & "##" & instlHinban.Substring(12)
        End Function

        ''' <summary>
        ''' 試作設計ブロックINSTL情報を削除
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <param name="bukaCode">試作部課</param>
        ''' <param name="strblockno">試作ブロック№</param>
        ''' <param name="strblocknokaiteino">試作ブロック№改訂№</param>
        ''' <remarks></remarks>
        Private Sub DeleteShisakuSekkeiBlockInstl(ByVal eventCode As String, _
                                                  ByVal bukaCode As String, _
                                                  ByVal strBlockNo As String, _
                                                  ByVal strblockNoKaiteiNo As String)
            Dim instlVo As New TShisakuSekkeiBlockInstlVo
            instlVo.ShisakuEventCode = eventCode
            instlVo.ShisakuBukaCode = bukaCode
            instlVo.ShisakuBlockNo = strBlockNo
            instlVo.ShisakuBlockNoKaiteiNo = strblockNoKaiteiNo
            instlDao.DeleteBy(instlVo)
        End Sub

        ''' <summary>
        ''' 試作設計ブロックINSTL情報へ登録する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()

            For Each v As String In blockNoList

                'ブロック№と改訂№を分割する。
                Dim foundIndex As Integer = v.IndexOf(":")
                Dim strBlockNo As String = Left(v, foundIndex)
                Dim strBlockNoKaiteiNo As String = Right(v, foundIndex - 1)

                Dim vos As List(Of TShisakuSekkeiBlockInstlVo) = MakeRegisterValues(strBlockNo)

                '前回のブロックのINSTL情報を削除する。
                '   ブロック№改訂№も必要！！！！
                DeleteShisakuSekkeiBlockInstl(shisakuEventCode, shisakuBukaCode, strBlockNo, strBlockNoKaiteiNo)

                '------------------------------------------------------------------
                '２次改修
                '　部課コードの問題について
                '   2012/08/08 部課略名で更新してみる。
                'Dim KARyakuName = New KaRyakuNameDaoImpl '部課略称名取得IMPL
                'Dim strRyakuName As String
                '------------------------------------------------------------------

                For Each vo As TShisakuSekkeiBlockInstlVo In vos

                    '------------------------------------------------------------------
                    '部課コード、改訂№は固定
                    vo.ShisakuBukaCode = shisakuBukaCode
                    vo.ShisakuBlockNoKaiteiNo = strBlockNoKaiteiNo

                    '------------------------------------------------------------------
                    '２次改修
                    '　部課コードの問題について
                    '   2012/08/08 部課略名で更新してみる。
                    'strRyakuName = KARyakuName.GetKa_Ryaku_Name(vo.ShisakuBukaCode).KaRyakuName()
                    '------------------------------------------------------------------
                    '------------------------------------------------------------------
                    '２次改修
                    '　部課コードの問題について
                    '   2012/08/08 部課略名で更新してみる。
                    'vo.BukaCode = instlVo.ShisakuBukaCode
                    'If StringUtil.IsNotEmpty(strRyakuName) Then
                    '    vo.ShisakuBukaCode = strRyakuName
                    'End If
                    '------------------------------------------------------------------

                    '重複キーが無いかチェック。
                    If dao.FindSekkeiBlockInstl(vo.ShisakuEventCode, _
                                                vo.ShisakuBukaCode, _
                                                vo.ShisakuBlockNo, _
                                                vo.ShisakuBlockNoKaiteiNo, _
                                                vo.ShisakuGousya, _
                                                vo.InstlDataKbn, _
                                                vo.InstlHinban, _
                                                vo.InstlHinbanKbn) Is Nothing Then

                        vo.CreatedUserId = LoginInfo.Now.UserId
                        vo.CreatedDate = aDate.CurrentDateDbFormat
                        vo.CreatedTime = aDate.CurrentTimeDbFormat
                        vo.UpdatedUserId = LoginInfo.Now.UserId
                        vo.UpdatedDate = aDate.CurrentDateDbFormat
                        vo.UpdatedTime = aDate.CurrentTimeDbFormat
                        '追加
                        instlDao.InsertBy(vo)

                    End If

                Next

            Next

        End Sub


        ''' <summary>
        ''' 試作設計ブロックINSTL情報の抽出チェック
        ''' </summary>
        ''' <remarks></remarks>
        Public Function Check() As Boolean

            Dim strInstlUmu As String = ""
            Dim strCheckMsg As String = "以下のブロックのＡＬ情報が存在しません。" & vbCrLf & vbCrLf & "ご確認ください。"

            For Each v As String In blockNoList

                'ブロック№と改訂№を分割する。
                Dim foundIndex As Integer = v.IndexOf(":")
                Dim strBlockNo As String = Left(v, foundIndex)
                Dim strBlockNoKaiteiNo As String = Right(v, foundIndex - 1)

                Dim vos As List(Of TShisakuSekkeiBlockInstlVo) = MakeRegisterValues(strBlockNo)

                '抽出できない。（件数が０）
                If vos.Count = 0 Then
                    strCheckMsg = strCheckMsg & vbCrLf & vbCrLf & v
                    strInstlUmu = "Nashi"
                End If

            Next
            '抽出チェックの戻り
            If strInstlUmu = "Nashi" Then
                ComFunc.ShowErrMsgBox(strCheckMsg)
                Return False
            Else
                Return True
            End If

        End Function

        Private Class RecordGroupingRule : Implements VoGroupingRule(Of BukaCodeBlockNoVo)
            Public Sub GroupRule(ByVal groupBy As VoGroupingLocator, ByVal vo As BukaCodeBlockNoVo) Implements VoGroupingRule(Of BukaCodeBlockNoVo).GroupRule
                '2014/11/19 InstlDataKbn追加
                groupBy.Prop(vo.BukaCode).Prop(vo.BlockNo).Prop(vo.Gosha).Prop(vo.FfBuhinNo).Prop(vo.InstlHinbanKbn).Prop(vo.BfBuhinNo).Prop(vo.InstlDataKbn)
                'groupBy.Prop(vo.BukaCode).Prop(vo.BlockNo).Prop(vo.Gosha).Prop(vo.FfBuhinNo).Prop(vo.InstlHinbanKbn).Prop(vo.BfBuhinNo)
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

                'Nothingならブランクに変換
                If StringUtil.IsEmpty(x.BfBuhinNo) Then
                    x.BfBuhinNo = ""
                End If
                If StringUtil.IsEmpty(y.BfBuhinNo) Then
                    y.BfBuhinNo = ""
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
        ''' <returns>グループ化した情報</returns>
        ''' <remarks>表示順は、設計課＆ブロックNoごと</remarks>
        Public Function GroupBukaCodeBlockNos(ByVal alVos As List(Of SekkeiBlockAlResultVo)) As List(Of BukaCodeBlockNoVo)
            'ByVal alVosOld As List(Of SekkeiBlockAlResultVo), _

            Dim packedVo As New List(Of BukaCodeBlockNoVo)
            For Each alVo As SekkeiBlockAlResultVo In alVos
                Dim baseVo As New TShisakuEventBaseVo

                '2012/02/16 号車ごとの開発符号を取得
                baseVo = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuGousya(shisakuEventCode, alVo.ShisakuGousya)
                If baseVo Is Nothing Then
                    baseVo = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuBaseGousya(shisakuEventCode, alVo.ShisakuGousya)
                End If

                'データ登録
                Rhac1500VoHelper.ResolveRule(alVo)
                Dim vo As New BukaCodeBlockNoVo

                'マスタに無い場合、課略名をセットします。　2011/2/3 by柳沼
                If StringUtil.IsEmpty(alVo.BuCode & alVo.KaCode) Then
                    vo.BukaCode = alVo.BukaCode  '課略名
                Else
                    '------------------------------------------------------------------
                    '２次改修
                    '　部課コードの問題について
                    '   2012/07/30 部課略名で更新してみる。
                    'vo.BukaCode = alVo.BuCode & alVo.KaCode  '部コードと課コード
                    vo.BukaCode = alVo.BukaCode  '課略名
                    '------------------------------------------------------------------
                End If

                vo.BlockNo = alVo.BlockNo
                vo.Gosha = alVo.ShisakuGousya
                vo.FfBuhinNo = alVo.FfBuhinNo
                vo.InstlHinbanKbn = String.Empty

                '2014/11/19 変更
                'vo.InstlDataKbn = "E"
                vo.InstlDataKbn = "0"


                vo.BfBuhinNo = StringUtil.Nvl(alVo.BfBuhinNo)
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

            For Each vo As BukaCodeBlockNoVo In results
                If vo.BukaCode.Equals(backBukaCode) AndAlso vo.BlockNo.Equals(backBlockNo) Then
                    If Not (EzUtil.IsEqualIfNull(hinban, vo.FfBuhinNo) AndAlso EzUtil.IsEqualIfNull(hinbanKbn, vo.InstlHinbanKbn)) Then
                        hyoujiJun += 1
                        hinban = vo.FfBuhinNo
                        hinbanKbn = vo.InstlHinbanKbn
                    End If
                Else
                    hyoujiJun = TShisakuSekkeiBlockInstlVoHelper.InstlHinbanHyoujiJun.START_VALUE
                    backBukaCode = vo.BukaCode
                    backBlockNo = vo.BlockNo
                    hinban = vo.FfBuhinNo
                    hinbanKbn = vo.InstlHinbanKbn
                End If
                vo.HyoujiJun = hyoujiJun
            Next
            Return results
        End Function

        ''' <summary>
        ''' ブロックNoに対する名称をもつDictionaryを返す
        ''' </summary>
        ''' <param name="alVos">A/Lの素の一覧</param>
        ''' <param name="instlVos">試作設計ブロックINSTL情報の一覧</param>
        ''' <returns>ブロック名称をもつDictionary</returns>
        ''' <remarks></remarks>
        Public Function ExtractBlockNames(ByVal alVos As List(Of SekkeiBlockAlResultVo), ByVal instlVos As List(Of SekkeiBlockInstlResultVo)) As Dictionary(Of String, String)

            Dim blockNames As New Dictionary(Of String, String)
            For Each vo As SekkeiBlockAlResultVo In alVos
                If blockNames.ContainsKey(vo.BlockNo) Then
                    Continue For
                End If
                blockNames.Add(vo.BlockNo, vo.BlockName)
            Next
            For Each vo As SekkeiBlockInstlResultVo In instlVos
                If blockNames.ContainsKey(vo.ShisakuBlockNo) Then
                    Continue For
                End If
                blockNames.Add(vo.ShisakuBlockNo, vo.ShisakuBlockName)
            Next
            Return blockNames
        End Function




    End Class
End Namespace