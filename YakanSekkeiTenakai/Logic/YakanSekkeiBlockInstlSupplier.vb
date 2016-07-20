Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Util.Grouping
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Dao
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinMenu.Logic.Impl.Tenkai
Imports YakanSekkeiTenakai.Dao
Imports YakanSekkeiTenakai.Vo
Imports EventSakusei

'Imports EventSakusei.ShisakuBuhinEditBlock.Dao

Namespace Logic
    Public Class YakanSekkeiBlockInstlSupplier
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly UnitKbn As String
        Private ReadOnly dao As YakanSekkeiBlockDao
        Private ReadOnly daoBase As ShisakuEventBaseDao     '2012/01/13

        Private ReadOnly makeDao As New MakeStructureResultDaoImpl

        '/*** 20140911 CHANGE START ***/
        Private kaihatsuFugo As Dictionary(Of String, String)
        Private unitKbnDictionary As Dictionary2(Of String, String, String)
        '/*** 20140911 CHANGE END ***/

        '/*** 20140911 CHANGE START（コンストラクタ引数追加） ***/
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo)
            'Me.New(shisakuEventVo.ShisakuEventCode, New YakanSekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl, shisakuEventVo.UnitKbn)
            'Me.UnitKbn = shisakuEventVo.UnitKbn
            Me.New(shisakuEventVo.ShisakuEventCode, New Dictionary2(Of String, String, String), New YakanSekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl)
            Me.UnitKbn = shisakuEventVo.UnitKbn
        End Sub
        Public Sub New(ByVal shisakuEventCode As String, ByVal dao As YakanSekkeiBlockDao, ByVal daoBase As ShisakuEventBaseDao, ByVal Unitkbn As String)
            'Me.shisakuEventCode = shisakuEventCode
            'Me.dao = dao
            'Me.daoBase = daoBase
            Me.New(shisakuEventCode, New Dictionary2(Of String, String, String), New YakanSekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl)
        End Sub
        Public Sub New(ByVal shisakuEventVo As TShisakuEventVo, ByRef unitKbnDictionary As Dictionary2(Of String, String, String))
            Me.New(shisakuEventVo.ShisakuEventCode, unitKbnDictionary, New YakanSekkeiBlockDaoImpl, New ShisakuEventBaseDaoImpl)
            Me.UnitKbn = shisakuEventVo.UnitKbn
        End Sub
        Public Sub New(ByVal shisakuEventCode As String, ByRef unitKbnDictionary As Dictionary2(Of String, String, String), ByVal dao As YakanSekkeiBlockDao, ByVal daoBase As ShisakuEventBaseDao)
            Me.shisakuEventCode = shisakuEventCode
            Me.dao = dao
            Me.daoBase = daoBase

            Me.kaihatsuFugo = New Dictionary(Of String, String)
            Me.unitKbnDictionary = unitKbnDictionary
        End Sub
        '/*** 20140911 CHANGE END ***/

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MakeRegisterValues(Optional ByVal login As LoginInfo = Nothing) As List(Of TShisakuSekkeiBlockInstlEbomKanshiVo)
            'Public Function MakeRegisterValues(ByVal ShisakuBlockNo As String, Optional ByVal login As LoginInfo = Nothing) As List(Of TShisakuSekkeiBlockInstlEbomKanshiVo)
            '/*** 20140911 CHANGE（コンストラクタ引数削除） ***/

            ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力   酒井 ADD BEGIN
            'Public Function MakeRegisterValues(ByVal ShisakuBlockNo As String) As List(Of TShisakuSekkeiBlockInstlEbomKanshiVo)
            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力   酒井 ADD END
            '/*** 20140911 CHANGE START ***/
            'Dim alVosFinal As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase("A")
            'Dim alVosFinalwk As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase("A")
            'Dim alVosFinalOld As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase("A")
            'Dim alVosFinalOldwk As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase("A")
            'Dim alVosLast As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase("A")
            Dim alVosFinal As New List(Of SekkeiBlockAlResultVo)
            Dim alVosFinalwk As New List(Of SekkeiBlockAlResultVo)
            Dim alVosFinalOld As New List(Of SekkeiBlockAlResultVo)
            Dim alVosFinalOldwk As New List(Of SekkeiBlockAlResultVo)
            Dim alVosLast As New List(Of SekkeiBlockAlResultVo)
            '/*** 20140911 CHANGE END ***/

            '/*** 20140911 CHANGE START（条件削除） ***/
            'Dim alVos As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBaseByBlock(shisakuEventCode, ShisakuBlockNo)
            'Dim alVosOld As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBaseOLDByBlock(shisakuEventCode, ShisakuBlockNo)
            'Dim alVos2 As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase2ByBlock(shisakuEventCode, ShisakuBlockNo)
            'Dim alVosOld2 As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBaseOLD2ByBlock(shisakuEventCode, ShisakuBlockNo)
            Dim alVos As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBaseByBlock(shisakuEventCode, "")
            Dim alVosOld As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBaseOLDByBlock(shisakuEventCode, "")
            Dim alVos2 As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBase2ByBlock(shisakuEventCode, "")
            Dim alVosOld2 As List(Of SekkeiBlockAlResultVo) = dao.FindAlByShisakuEventBaseOLD2ByBlock(shisakuEventCode, "")
            '/*** 20140911 CHANGE END ***/

            Dim wFlg As String = Nothing
            Dim BLOCKNO As String = Nothing
            Dim SHISAKUGOUSYA As String = Nothing
            Dim TOPCOLORKAITEINO As String = Nothing

            For Each group As SekkeiBlockAlResultVo In alVos2
                If Not StringUtil.IsEmpty(group.KumiawaseCodeSo) Then
                    alVosFinal.Add(group)
                End If
            Next
            For Each group As SekkeiBlockAlResultVo In alVos
                wFlg = Nothing
                For Each group2 As SekkeiBlockAlResultVo In alVosFinal
                    If group.BlockNo = group2.BlockNo AndAlso _
                       group.ShisakuGousya = group2.ShisakuGousya Then
                        wFlg = "NO"
                        Exit For
                    End If
                Next
                If wFlg <> "NO" Then
                    '/*** 20140911 CHANGE START ***/
                    'If BLOCKNO = group.BlockNo And _
                    '    SHISAKUGOUSYA = group.ShisakuGousya Then
                    '    If StringUtil.IsEmpty(TOPCOLORKAITEINO) And _
                    '        StringUtil.IsEmpty(group.TopColorKaiteiNo) Then
                    '        alVosFinalwk.Add(group)
                    '    ElseIf Not StringUtil.IsEmpty(TOPCOLORKAITEINO) And _
                    '            Not StringUtil.IsEmpty(group.TopColorKaiteiNo) Then
                    '        alVosFinalwk.Add(group)
                    '    Else
                    '        '2012/02/25
                    '        'No.118の修正で部品番号が抜けている対応
                    '        alVosFinalwk.Add(group)
                    '    End If
                    'Else
                    '    alVosFinalwk.Add(group)
                    'End If

                    ' どの条件でも同じ処理のため、If文を削除
                    alVosFinalwk.Add(group)
                    '/*** 20140911 CHANGE END ***/
                End If

                BLOCKNO = group.BlockNo
                SHISAKUGOUSYA = group.ShisakuGousya
                TOPCOLORKAITEINO = group.TopColorKaiteiNo
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
                    If groupOLD.BlockNo = groupOLD2.BlockNo AndAlso _
                       groupOLD.ShisakuGousya = groupOLD2.ShisakuGousya Then
                        wFlg = "NO"
                        Exit For
                    End If
                Next
                If wFlg <> "NO" Then
                    If BLOCKNO = groupOLD.BlockNo AndAlso _
                        SHISAKUGOUSYA = groupOLD.ShisakuGousya Then
                        If StringUtil.IsEmpty(TOPCOLORKAITEINO) AndAlso _
                            StringUtil.IsEmpty(groupOLD.TopColorKaiteiNo) Then
                            alVosFinalOldwk.Add(groupOLD)
                        ElseIf Not StringUtil.IsEmpty(TOPCOLORKAITEINO) AndAlso _
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
            Next
            If alVosFinalOldwk.Count > 0 Then
                alVosFinalOld.AddRange(alVosFinalOldwk)
            End If

            For Each groupOLD As SekkeiBlockAlResultVo In alVosFinalOld
                wFlg = Nothing
                For Each group As SekkeiBlockAlResultVo In alVosFinal
                    If groupOLD.BlockNo = group.BlockNo AndAlso _
                       groupOLD.ShisakuGousya = group.ShisakuGousya Then
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

            '2012/03/15 取得方法修正'
            ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力   酒井 ADD BEGIN
            Dim instlVos As New List(Of YakanSekkeiBlockInstlResultVo)
            If login IsNot Nothing Then
                'Dim instlVos As List(Of YakanSekkeiBlockInstlResultVo) = dao.FindShisakuBlockInstlByShisakuEventBaseByBlock(shisakuEventCode, UnitKbn, ShisakuBlockNo)
                '/*** 20140911 CHANGE START（条件削除） ***/
                'instlVos = dao.FindShisakuBlockInstlByShisakuEventBaseByBlock(shisakuEventCode, UnitKbn, ShisakuBlockNo)
                instlVos = dao.FindShisakuBlockInstlByShisakuEventBaseByBlock(shisakuEventCode, UnitKbn, "")
                '/*** 20140911 CHANGE END ***/
            End If
            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力  酒井 ADD END

            Dim groupedVos As List(Of BukaCodeBlockNoVo) = GroupBukaCodeBlockNos(alVosFinal, instlVos)
            'Dim groupedVos As List(Of BukaCodeBlockNoVo) = GroupBukaCodeBlockNos(alVos, alVosOld, instlVos)

            Dim results As New List(Of TShisakuSekkeiBlockInstlEbomKanshiVo)
            For Each group As BukaCodeBlockNoVo In groupedVos
                Dim vo As New TShisakuSekkeiBlockInstlEbomKanshiVo
                results.Add(vo)

                ''色付き品番の場合、構成マスタに色付きで登録されているとは限らない。
                '' 例）　****AV　⇒　****##
                ''　構成マスタ（552⇒553⇒551の順番で）を色付き品番で検索し、
                ''　該当データが無ければ##品番に置き換えて以下のロジックから使用する。

                'Dim wHinban As String = Nothing
                ''12桁以外の場合処理無し。
                'If group.FfBuhinNo Is Nothing OrElse group.FfBuhinNo.Length < 12 _
                '    OrElse group.FfBuhinNo.Length >= 13 Then
                '    wHinban = group.FfBuhinNo
                'Else
                '    'まずは552をチェック
                '    Dim Rhac0552Vo As New Rhac0552Vo
                '    Rhac0552Vo = makeDao.FindByBuhinRhac0552(group.FfBuhinNo)
                '    '次に553をチェック
                '    Dim KaihatsuFugo As New TShisakuEventBaseVo
                '    KaihatsuFugo = makeDao.FindByBase(shisakuEventCode, group.Gosha)
                '    'KaihatsuFugo.BaseKaihatsuFugo = "FM5"
                '    Dim Rhac0553Vo As New Rhac0553Vo
                '    Rhac0553Vo = makeDao.FindByBuhinRhac0553(KaihatsuFugo.BaseKaihatsuFugo, group.FfBuhinNo)
                '    '最後に551をチェック
                '    Dim Rhac0551Vo As New Rhac0551Vo
                '    Rhac0551Vo = makeDao.FindByBuhinRhac0551(group.FfBuhinNo)
                '    '全てのテーブルに存在しない場合は、基本Ｆ品番の色コードを＃＃に置き換える。
                '    If Rhac0552Vo Is Nothing And Rhac0553Vo Is Nothing And Rhac0551Vo Is Nothing Then
                '        wHinban = Conv0532HinbanByRule(group.FfBuhinNo)
                '    Else
                '        wHinban = group.FfBuhinNo
                '    End If
                'End If
                'vo.BfBuhinNo = wHinban
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
                ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④) (ダニエル上海)柳沼 ADD BEGIN
                vo.InstlDataKbn = group.InstlDataKbn
                ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④) (ダニエル上海)柳沼 ADD END
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
        ''' 試作設計ブロックINSTL情報へ登録する
        ''' </summary>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="instlDao">試作設計ブロックINSTL情報Dao</param>
        ''' <param name="aShisakuDate">試作システム日付</param>
        ''' <remarks></remarks>
        Public Sub Register(ByVal login As LoginInfo, _
                            ByVal instlDao As TShisakuSekkeiBlockInstlEbomKanshiDao, _
                            ByVal aShisakuDate As ShisakuDate)

            'Dim alVosBase As List(Of ShisakuEventBaseResultVo) = daoBase.FindShisakuEventBase(shisakuEventCode)
            '/*** 20140911 CHANGE START（イベントコード単位のため不要） ***/
            'Dim blockGroup As List(Of TShisakuSekkeiBlockEbomKanshiVo) = dao.FindByShisakuBlockAllGroupByBlockNo(shisakuEventCode)
            '/*** 20140911 CHANGE END ***/

            '/*** 20140911 CHANGE START ***/
            Dim registeredDataList As List(Of TShisakuSekkeiBlockInstlEbomKanshiVo) = New List(Of TShisakuSekkeiBlockInstlEbomKanshiVo)
            '/*** 20140911 CHANGE END ***/

            '/*** 20140911 CHANGE START（イベントコード単位のため不要） ***/
            'For Each VoBlock As TShisakuSekkeiBlockEbomKanshiVo In blockGroup
            '/*** 20140911 CHANGE END ***/


            '#End If
#If DEBUG Then
                'エラーが発生するイベントFM5-K-T08
                'エラーが発生するブロック811B
                'If Not VoBlock.ShisakuBlockNo = "262A" Then
                '    Continue For
                'End If
                'If Not VoBlock.ShisakuBlockNo = "200B" Then
                '    Continue For
                'End If
#End If
            '#If DEBUG Then
            '                'エラーが発生するイベントFM5-K-T08
            '                'エラーが発生するブロック811B
            '                If VoBlock.ShisakuBlockNo = "200A" Or VoBlock.ShisakuBlockNo = "266A" Then
            '                    Dim x As Integer
            '                    x = 1
            '                End If
            '#End If
            '/*** 20140911 CHANGE START（イベントコード単位のため引数不要） ***/
            'Dim vos As List(Of TShisakuSekkeiBlockInstlEbomKanshiVo) = MakeRegisterValues(VoBlock.ShisakuBlockNo, login)
            Dim vos As List(Of TShisakuSekkeiBlockInstlEbomKanshiVo) = MakeRegisterValues(login)

            '------------------------------------------------------------------
            '２次改修
            '　部課コードの問題について
            '   2012/08/08 部課略名で更新してみる。
            'Dim KARyakuName = New ShisakuBuhinEditBlock.Dao.KaRyakuNameDaoImpl '部課略称名取得IMPL
            'Dim strRyakuName As String
            '------------------------------------------------------------------

            'Dim intInstlHinbanHyoujiJun As Integer = 0
            'Dim intCount As Integer = 0
            'Dim strInstlHinban As String = ""
            'Dim strInstlHinbanKbn As String = ""


            For Each vo As TShisakuSekkeiBlockInstlEbomKanshiVo In vos

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

                '-----------------------------------------------------------------------------------------------------
                '２次改修
                '   2012/08/21
                '   ここにinstlVos（INSTL品番情報）の表示順チェックを導入。
                '   理由）試作イベントをベースにした場合、表示順が同じで部品番号違いの場合、
                '       　部品表へ正しく反映されないことが判明。
                '       　試作イベントコード、部課コード、ブロック№、ブロック№改訂№、INSTL品番、INSTL品番試作区分
                'If intCount > 0 Then
                '    If strInstlHinban <> vo.InstlHinban Or strInstlHinbanKbn <> vo.InstlHinbanKbn Then
                '        intInstlHinbanHyoujiJun += 1
                '    End If
                'End If
                '-----------------------------------------------------------------------------------------------------

                '重複キーが無いかチェック。
                ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (3)) (ダニエル上海)柳沼 ADD BEGIN
                'If instlDao.FindByPk(vo.ShisakuEventCode, _
                '                  vo.ShisakuBukaCode, _
                '                  vo.ShisakuBlockNo, _
                '                  vo.ShisakuBlockNoKaiteiNo, _
                '                  vo.ShisakuGousya, _
                '                  vo.InstlHinban, _
                '                  vo.InstlHinbanKbn) Is Nothing Then
                '/*** 20140911 CHANGE START ***/
                'If dao.FindSekkeiBlockInstl(vo.ShisakuEventCode, _
                '                            vo.ShisakuBukaCode, _
                '                            vo.ShisakuBlockNo, _
                '                            vo.ShisakuBlockNoKaiteiNo, _
                '                            vo.ShisakuGousya, _
                '                            vo.InstlDataKbn, _
                '                            vo.InstlHinban, _
                '                            vo.InstlHinbanKbn) Is Nothing Then
                '    ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (3)) (ダニエル上海)柳沼 ADD END
                If Not CheckChofuku(vo, registeredDataList) Then
                    '/*** 20140911 CHANGE END ***/


                    '--------------------------------------------------
                    '２次改修
                    'vo.InstlHinbanHyoujiJun = intInstlHinbanHyoujiJun
                    '--------------------------------------------------

                    ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力  bc) (TES)施 ADD BEGIN
                    ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力   酒井 ADD BEGIN
                    'If login IsNot Nothing Then
                    If login Is Nothing Then
                        'vo.CreatedUserId = login.UserId
                        vo.CreatedUserId = ""
                        vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
                        vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
                        'vo.UpdatedUserId = login.UserId
                        vo.UpdatedUserId = ""
                        ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力  酒井 ADD END
                        vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
                        vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat
                        ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_h) (TES)張 ADD BEGIN
                        vo.BaseInstlFlg = 1
                        ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_h) (TES)張 ADD END
                        '追加
                        instlDao.InsertBy(vo)
                        'ベース用に追加'
                        ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力   酒井 ADD BEGIN
                        If login IsNot Nothing Then
                            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力  酒井 ADD END
                            vo.ShisakuBlockNoKaiteiNo = "  0"
                            instlDao.InsertBy(vo)
                            ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力   酒井 ADD BEGIN
                        End If
                        ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力  酒井 ADD END

                        '/*** 20140911 CHANGE START ***/
                        ' リストに追加
                        registeredDataList.Add(vo)
                        '/*** 20140911 CHANGE END ***/

                    End If
                    ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 bc) (TES)施 ADD END



                    '-----------------------------------------------------------------------------------------------------
                    '２次改修
                    'strInstlHinban = vo.InstlHinban
                    'strInstlHinbanKbn = vo.InstlHinbanKbn
                    'intCount += 1
                    '-----------------------------------------------------------------------------------------------------

                End If

            Next

            '/*** 20140911 CHANGE START（イベントコード単位のため不要） ***/
            'Next
            '/*** 20140911 CHANGE END ***/

            'Dim vos As List(Of TShisakuSekkeiBlockInstlVo) = MakeRegisterValues()

            'For Each vo As TShisakuSekkeiBlockInstlVo In vos
            '    '重複キーが無いかチェック。
            '    If instlDao.FindByPk(vo.ShisakuEventCode, _
            '                      vo.ShisakuBukaCode, _
            '                      vo.ShisakuBlockNo, _
            '                      vo.ShisakuBlockNoKaiteiNo, _
            '                      vo.ShisakuGousya, _
            '                      vo.InstlHinban, _
            '                      vo.InstlHinbanKbn) Is Nothing Then

            '        vo.CreatedUserId = login.UserId
            '        vo.CreatedDate = aShisakuDate.CurrentDateDbFormat
            '        vo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
            '        vo.UpdatedUserId = login.UserId
            '        vo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
            '        vo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat
            '        '追加
            '        instlDao.InsertBy(vo)
            '        'ベース用に追加'
            '        vo.ShisakuBlockNoKaiteiNo = "  0"
            '        instlDao.InsertBy(vo)
            '    End If

            'Next
            '/*** 20140911 CHANGE START ***/
            ' 明示的にクリアする
            registeredDataList = Nothing
            '/*** 20140911 CHANGE END ***/
        End Sub
        '/*** 20140911 CHANGE START ***/
        Private Function CheckChofuku(ByVal vo As TShisakuSekkeiBlockInstlEbomKanshiVo, ByRef registeredDataList As List(Of TShisakuSekkeiBlockInstlEbomKanshiVo)) As Boolean
            For Each registeredData As TShisakuSekkeiBlockInstlEbomKanshiVo In registeredDataList

                '' チェックにイベントコードは不要かも
                'If vo.ShisakuEventCode.Equals(registeredData.ShisakuEventCode) _
                '    AndAlso vo.ShisakuBukaCode.Equals(registeredData.ShisakuBukaCode) _
                '    AndAlso vo.ShisakuBlockNo.Equals(registeredData.ShisakuBlockNo) _
                '    AndAlso vo.ShisakuBlockNoKaiteiNo.Equals(registeredData.ShisakuBlockNoKaiteiNo) _
                '    AndAlso vo.ShisakuGousya.Equals(registeredData.ShisakuGousya) _
                '    AndAlso vo.InstlHinban.Equals(registeredData.InstlHinban) _
                '    AndAlso vo.InstlHinbanKbn.Equals(registeredData.InstlHinbanKbn) Then
                If vo.ShisakuBukaCode.Equals(registeredData.ShisakuBukaCode) _
                    AndAlso vo.ShisakuBlockNo.Equals(registeredData.ShisakuBlockNo) _
                    AndAlso vo.ShisakuBlockNoKaiteiNo.Equals(registeredData.ShisakuBlockNoKaiteiNo) _
                    AndAlso vo.ShisakuGousya.Equals(registeredData.ShisakuGousya) _
                    AndAlso vo.InstlDataKbn.Equals(registeredData.InstlDataKbn) _
                    AndAlso vo.InstlHinban.Equals(registeredData.InstlHinban) _
                    AndAlso vo.InstlHinbanKbn.Equals(registeredData.InstlHinbanKbn) Then

                    ' データがあった場合
                    Return True
                End If

            Next

            ' データがなかった場合
            Return False

        End Function
        '/*** 20140911 CHANGE END ***/


        Private Class RecordGroupingRule : Implements VoGroupingRule(Of BukaCodeBlockNoVo)
            Public Sub GroupRule(ByVal groupBy As VoGroupingLocator, ByVal vo As BukaCodeBlockNoVo) Implements VoGroupingRule(Of BukaCodeBlockNoVo).GroupRule
                ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅶ (4)) (ダニエル上海)柳沼 ADD BEGIN
                'groupBy.Prop(vo.BukaCode).Prop(vo.BlockNo).Prop(vo.Gosha).Prop(vo.FfBuhinNo).Prop(vo.InstlHinbanKbn).Prop(vo.BfBuhinNo)
                groupBy.Prop(vo.BukaCode).Prop(vo.BlockNo).Prop(vo.Gosha).Prop(vo.FfBuhinNo).Prop(vo.InstlHinbanKbn).Prop(vo.BfBuhinNo).Prop(vo.InstlDataKbn)
                ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅶ (4)) (ダニエル上海)柳沼 ADD END
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
        ''' <param name="instlVos">試作設計ブロックINSTL情報の一覧</param>
        ''' <returns>グループ化した情報</returns>
        ''' <remarks>表示順は、設計課＆ブロックNoごと</remarks>
        Public Function GroupBukaCodeBlockNos(ByVal alVos As List(Of SekkeiBlockAlResultVo), _
                                              ByVal instlVos As List(Of YakanSekkeiBlockInstlResultVo) _
                                              ) As List(Of BukaCodeBlockNoVo)
            'ByVal alVosOld As List(Of SekkeiBlockAlResultVo), _

            Dim packedVo As New List(Of BukaCodeBlockNoVo)
            Dim MUnitKbn As New Rhac0080Vo
            '/*** 20140911 CHANGE START ***/
            Dim tmpUnitKbn As String
            '/*** 20140911 CHANGE END ***/
            For Each alVo As SekkeiBlockAlResultVo In alVos
                Dim baseVo As New TShisakuEventBaseVo
                '↓↓2014/09/24 酒井 ADD BEGIN
                Dim ebomKanshiVo As New TShisakuEventEbomKanshiVo
                '↑↑2014/09/24 酒井 ADD END
                Dim shisakuKaihatsuFugo As String

                '/*** 20140911 CHANGE START（キャッシュ対応） ***/
                '2012/02/16 号車ごとの開発符号を取得
                'baseVo = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuGousya(shisakuEventCode, alVo.ShisakuGousya)
                'If baseVo Is Nothing Then
                '    baseVo = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuBaseGousya(shisakuEventCode, alVo.ShisakuGousya)
                'End If
                'shisakuKaihatsuFugo = baseVo.BaseKaihatsuFugo

                If kaihatsuFugo.ContainsKey(alVo.ShisakuGousya) Then
                    shisakuKaihatsuFugo = kaihatsuFugo(alVo.ShisakuGousya)
                Else
                    '2012/02/16 号車ごとの開発符号を取得
                    '↓↓2014/09/24 酒井 ADD BEGIN
                    'baseVo = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuGousya(shisakuEventCode, alVo.ShisakuGousya)
                    ebomKanshiVo = daoBase.FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuGousya(shisakuEventCode, alVo.ShisakuGousya)
                    'If baseVo Is Nothing Then
                    If ebomKanshiVo Is Nothing Then
                        'baseVo = daoBase.FindShisakuEventBaseByEventCodeAndGousyaForShisakuBaseGousya(shisakuEventCode, alVo.ShisakuGousya)
                        ebomKanshiVo = daoBase.FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuBaseGousya(shisakuEventCode, alVo.ShisakuGousya)
                    End If
                    VoUtil.CopyProperties(ebomKanshiVo, baseVo)
                    '↑↑2014/09/24 酒井 ADD END

                    kaihatsuFugo.Add(alVo.ShisakuGousya, baseVo.BaseKaihatsuFugo)
                    shisakuKaihatsuFugo = baseVo.BaseKaihatsuFugo
                End If
                '/*** 20140911 CHANGE END ***/

                'マスターからブロックに該当するユニット区分を取得する。
                'alVo.ShisakuGousya
                '/*** 20140911 CHANGE START（キャッシュ対応） ***/
                'MUnitKbn = dao.FindShisakuBlockUnit(alVo.BlockNo, shisakuKaihatsuFugo)

                If unitKbnDictionary.ContainsKeys(alVo.BlockNo, shisakuKaihatsuFugo) Then
                    tmpUnitKbn = unitKbnDictionary.item(alVo.BlockNo)(shisakuKaihatsuFugo)
                Else
                    'マスターからブロックに該当するユニット区分を取得する。
                    MUnitKbn = dao.FindShisakuBlockUnit(alVo.BlockNo, shisakuKaihatsuFugo)

                    If alVo.BlockNo = "604A" Then
                        tmpUnitKbn = "M"
                    Else
                        tmpUnitKbn = MUnitKbn.MtKbn
                    End If

                    unitKbnDictionary.Put(alVo.BlockNo, shisakuKaihatsuFugo, tmpUnitKbn)
                End If
                ' 上位のIf内で判定し、毎回判定しないようにする
                'If alVo.BlockNo = "604A" Then
                '    MUnitKbn.MtKbn = "M"
                'End If
                '/*** 20140911 CHANGE END ***/
                'イベント情報のユニット区分に該当するデータのみ処理する。
                ' S:全て対象
                ' M:M、ブランクのみ対象
                ' T:T、ブランクのみ対象
                If UnitKbn <> "S" Then
                    '/*** 20140911 CHANGE START ***/
                    'If UnitKbn <> MUnitKbn.MtKbn And Not StringUtil.IsEmpty(MUnitKbn.MtKbn) Then
                    If UnitKbn <> tmpUnitKbn AndAlso Not StringUtil.IsEmpty(tmpUnitKbn) Then
                        '/*** 20140911 CHANGE END ***/
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

                ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (2)) (ダニエル上海)柳沼 ADD BEGIN
                vo.InstlDataKbn = "0"
                ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (2)) (ダニエル上海)柳沼 ADD END

                vo.BfBuhinNo = StringUtil.Nvl(alVo.BfBuhinNo)
                packedVo.Add(vo)
            Next

            ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (2)) (ダニエル上海)柳沼 ADD BEGIN
            Dim backBlockNo As String = ""
            Dim backInstlHinban As String = ""
            Dim I As Integer
            ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (2)) (ダニエル上海)柳沼 ADD END





            'For Each alVo As SekkeiBlockAlResultVo In alVosOld
            '    'マスターからブロックに該当するユニット区分を取得する。
            '    MUnitKbn = dao.FindShisakuBlockUnit(alVo.BlockNo)
            '    If alVo.BlockNo = "604A" Then
            '        MUnitKbn.MtKbn = "M"
            '    End If
            '    'イベント情報のユニット区分に該当するデータのみ処理する。
            '    ' S:全て対象
            '    ' M:M、ブランクのみ対象
            '    ' T:T、ブランクのみ対象
            '    If UnitKbn <> "S" Then
            '        If UnitKbn <> MUnitKbn.MtKbn And Not StringUtil.IsEmpty(MUnitKbn.MtKbn) Then
            '            '条件に該当しないので次のデータへ
            '            Continue For
            '        End If
            '    End If
            '    'データ登録
            '    Rhac1500VoHelper.ResolveRule(alVo)
            '    Dim vo As New BukaCodeBlockNoVo

            '    'マスタに無い場合、課略名をセットします。　2011/2/3 by柳沼
            '    If StringUtil.IsEmpty(alVo.BuCode & alVo.KaCode) Then
            '        vo.BukaCode = alVo.BukaCode  '課略名
            '    Else
            '        vo.BukaCode = alVo.BuCode & alVo.KaCode  '部コードと課コード
            '    End If

            '    vo.BlockNo = alVo.BlockNo
            '    vo.Gosha = alVo.ShisakuGousya
            '    vo.FfBuhinNo = alVo.FfBuhinNo
            '    vo.InstlHinbanKbn = String.Empty
            '    vo.BfBuhinNo = StringUtil.Nvl(alVo.BfBuhinNo)
            '    packedVo.Add(vo)
            'Next

            For Each instlVo As YakanSekkeiBlockInstlResultVo In instlVos

                ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (2)) (ダニエル上海)柳沼 ADD BEGIN
                If backBlockNo = instlVo.ShisakuBlockNo AndAlso backInstlHinban = instlVo.InstlHinban Then
                    I = I + 1
                Else
                    I = 1
                    backBlockNo = instlVo.ShisakuBlockNo
                    backInstlHinban = instlVo.InstlHinban
                End If
                ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (2)) (ダニエル上海)柳沼 ADD END

                Dim vo As New BukaCodeBlockNoVo
                vo.BukaCode = instlVo.ShisakuBukaCode
                vo.BlockNo = instlVo.ShisakuBlockNo
                vo.Gosha = instlVo.BaseShisakuGousya
                vo.FfBuhinNo = instlVo.InstlHinban
                vo.InstlHinbanKbn = StringUtil.Nvl(instlVo.InstlHinbanKbn)

                ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (2)) (ダニエル上海)柳沼 ADD BEGIN
                vo.InstlDataKbn = I
                ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (2)) (ダニエル上海)柳沼 ADD END

                vo.BfBuhinNo = instlVo.BfBuhinNo
                packedVo.Add(vo)

                ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (2)) (ダニエル上海)柳沼 ADD BEGIN
                I = I + 1
                ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅱ④ (2)) (ダニエル上海)柳沼 ADD END

            Next

            Dim groupInstl As New VoGrouping(Of BukaCodeBlockNoVo)(New RecordGroupingRule)

            Dim results As List(Of BukaCodeBlockNoVo) = groupInstl.Grouping(packedVo)
            results.Sort(New BukaBlockComparer)

            Dim backBukaCode As String = String.Empty
            backBlockNo = String.Empty
            Dim hinban As String = String.Empty
            Dim hinbanKbn As String = String.Empty
            Dim hyoujiJun As Integer

            For Each vo As BukaCodeBlockNoVo In results
#If DEBUG Then
                If vo.BlockNo.Equals("880X") Then
                    Dim x As String
                    x = "dd"
                End If
#End If
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
        Public Function ExtractBlockNames(ByVal alVos As List(Of SekkeiBlockAlResultVo), ByVal instlVos As List(Of YakanSekkeiBlockInstlResultVo)) As Dictionary(Of String, String)

            Dim blockNames As New Dictionary(Of String, String)
            For Each vo As SekkeiBlockAlResultVo In alVos
                If blockNames.ContainsKey(vo.BlockNo) Then
                    Continue For
                End If
                blockNames.Add(vo.BlockNo, vo.BlockName)
            Next
            For Each vo As YakanSekkeiBlockInstlResultVo In instlVos
                If blockNames.ContainsKey(vo.ShisakuBlockNo) Then
                    Continue For
                End If
                blockNames.Add(vo.ShisakuBlockNo, vo.ShisakuBlockName)
            Next
            Return blockNames
        End Function




    End Class
End Namespace