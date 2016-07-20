Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu.Vo
Imports EBom.Data
Imports EBom.Common

Namespace TehaichoMenu.Impl
    Public Class KaiteiUpDaoImpl : Inherits DaoEachFeatureImpl
        Implements KaiteiUpDao


        ''' <summary>
        ''' 設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindBySekkeiBlock(ByVal shisakuEventCode As String) Implements KaiteiUpDao.FindBySekkeiBlock
            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SB " _
            & " WHERE  " _
            & " SB.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'" _
            & " AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ) ) ) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK " _
            & " WHERE SHISAKU_EVENT_CODE = SB.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_BUKA_CODE = SB.SHISAKU_BUKA_CODE  " _
            & " AND SHISAKU_BLOCK_NO = SB.SHISAKU_BLOCK_NO ) "

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sql)
        End Function

        ''' <summary>
        ''' 試作手配帳改訂抽出ブロック情報を追加する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <param name="sekkeiBlockVoList"></param>
        ''' <remarks></remarks>
        Public Sub InsertByKaiteiBlock(ByVal shisakuEventCode As String, _
                                       ByVal shisakuListCode As String, _
                                       ByVal shisakuListCodeKaiteiNo As String, _
                                       ByVal sekkeiBlockVoList As List(Of TShisakuSekkeiBlockVo)) Implements KaiteiUpDao.InsertByKaiteiBlock


            Dim aDate As New ShisakuDate
            Dim UsqlList(sekkeiBlockVoList.Count - 1) As String


            Dim Usql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KAITEI_BLOCK " _
            & " SET " _
            & " ZENKAI_BLOCK_NO_KAITEI_NO = KONKAI_BLOCK_NO_KAITEI_NO, " _
            & " KONKAI_BLOCK_NO_KAITEI_NO = '' , " _
            & " UPDATED_USER_ID = '" & LoginInfo.Now.UserId & "', " _
            & " UPDATED_DATE = '" & aDate.CurrentDateDbFormat & "', " _
            & " UPDATED_TIME = '" & aDate.CurrentTimeDbFormat & "' " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = '" & shisakuEventCode & "'" _
            & " AND SHISAKU_LIST_CODE = '" & shisakuListCode & "'" _
            & " AND NOT KONKAI_BLOCK_NO_KAITEI_NO = '' "

            Dim db As New EBomDbClient

            db.Update(Usql)


            'UsqlList(index) = Usql

            'Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            '    insert.Open()
            '    insert.BeginTransaction()
            '    For index As Integer = 0 To sekkeiBlockVoList.Count - 1
            '        Try
            '            '初回に作るからいらない'
            '            insert.ExecuteNonQuery(UsqlList(index))
            '        Catch ex As SqlClient.SqlException
            '            Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
            '            If prm < 0 Then
            '                Dim a As String = "a"
            '            Else

            '            End If

            '        End Try
            '    Next
            '    insert.Commit()
            'End Using


        End Sub

        ''' <summary>
        ''' リストコードの改訂繰上げ
        ''' </summary>
        ''' <param name="ListVo">リスト情報</param>
        ''' <remarks></remarks>
        Public Sub InsertByListKaiteiNo(ByVal ListVo As TShisakuListcodeVo) Implements KaiteiUpDao.InsertByListKaiteiNo
            'もっと簡単な方法があったら変更する'
            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE ( " _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_LIST_HYOJIJUN_NO, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_GROUP_NO, " _
            & " SHISAKU_KOUJI_KBN, " _
            & " SHISAKU_SEIHIN_KBN, " _
            & " SHISAKU_KOUJI_SHIREI_NO, " _
            & " SHISAKU_KOUJI_NO, " _
            & " SHISAKU_EVENT_NAME, " _
            & " SHISAKU_JIKYUHIN, " _
            & " SHISAKU_HIKAKUKEKKA, " _
            & " SHISAKU_SYUUKEI_CODE, " _
            & " SHISAKU_DAISU, " _
            & " SHISAKU_TEHAICHO_SAKUSEIBI, " _
            & " SHISAKU_TEHAICHO_SAKUSEIJIKAN, " _
            & " OLD_LIST_CODE, " _
            & " SHISAKU_DATA_TOUROKUBI, " _
            & " SHISAKU_DATA_TOUROKUJIKAN, " _
            & " RIREKI, " _
            & " SHISAKU_MEMO, " _
            & " SHISAKU_TENSOUBI, " _
            & " SHISAKU_TENSOUJIKAN, " _
            & " ZENKAI_KAITEIBI, " _
            & " SAISHIN_CHUSYUTUBI, " _
            & " SAISHIN_CHUSYUTUJIKAN, " _
            & " STATUS, " _
            & " AUTO_ORIKOMI_FLAG, " _
            & " UNIT_KBN, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & " @ShisakuEventCode, " _
            & " @ShisakuListHyojijunNo, " _
            & " @ShisakuListCode, " _
            & " @ShisakuListCodeKaiteiNo, " _
            & " @ShisakuGroupNo, " _
            & " @ShisakuKoujiKbn, " _
            & " @ShisakuSeihinKbn, " _
            & " @ShisakuKoujiShireiNo, " _
            & " @ShisakuKoujiNo, " _
            & " @ShisakuEventName, " _
            & " @ShisakuJikyuhin, " _
            & " @ShisakuHikakukekka, " _
            & " @ShisakuSyuukeiCode, " _
            & " @ShisakuDaisu, " _
            & " @ShisakuTehaichoSakuseibi, " _
            & " @ShisakuTehaichoSakuseijikan, " _
            & " @OldListCode, " _
            & " @ShisakuDataTourokubi, " _
            & " @ShisakuDataTourokujikan, " _
            & " @Rireki, " _
            & " @ShisakuMemo, " _
            & " @ShisakuTensoubi, " _
            & " @ShisakuTensoujikan, " _
            & " @ZenkaiKaiteibi, " _
            & " @SaishinChusyutubi, " _
            & " @SaishinChusyutujikan, " _
            & " @Status, " _
            & " @AutoOrikomiFlag, " _
            & " @UnitKbn, " _
            & " @CreatedUserId, " _
            & " @CreatedDate, " _
            & " @CreatedTime, " _
            & " @UpdatedUserId, " _
            & " @UpdatedDate, " _
            & " @UpdatedTime " _
            & " ) "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuListcodeVo
            param.ShisakuEventCode = ListVo.ShisakuEventCode
            param.ShisakuListHyojijunNo = ListVo.ShisakuListHyojijunNo
            param.ShisakuListCode = ListVo.ShisakuListCode
            param.ShisakuListCodeKaiteiNo = Right("000" + (Integer.Parse(ListVo.ShisakuListCodeKaiteiNo) + 1).ToString, 3)
            param.ShisakuGroupNo = ListVo.ShisakuGroupNo
            param.ShisakuKoujiKbn = ListVo.ShisakuKoujiKbn
            param.ShisakuSeihinKbn = ListVo.ShisakuSeihinKbn
            param.ShisakuKoujiShireiNo = ListVo.ShisakuKoujiShireiNo
            param.ShisakuKoujiNo = ListVo.ShisakuKoujiNo
            param.ShisakuEventName = ListVo.ShisakuEventName
            param.ShisakuJikyuhin = ListVo.ShisakuJikyuhin
            param.ShisakuHikakukekka = ListVo.ShisakuHikakukekka
            param.ShisakuSyuukeiCode = ListVo.ShisakuSyuukeiCode
            param.ShisakuDaisu = ListVo.ShisakuDaisu
            param.ShisakuTehaichoSakuseibi = ListVo.ShisakuTehaichoSakuseibi
            param.ShisakuTehaichoSakuseijikan = ListVo.ShisakuTehaichoSakuseijikan
            param.OldListCode = ListVo.OldListCode
            param.ShisakuDataTourokubi = ListVo.ShisakuDataTourokubi
            param.ShisakuDataTourokujikan = ListVo.ShisakuDataTourokujikan
            param.Rireki = "*"
            param.ShisakuMemo = ListVo.ShisakuMemo
            param.ShisakuTensoubi = ListVo.ShisakuTensoubi
            param.ShisakuTensoujikan = ListVo.ShisakuTensoujikan
            param.ZenkaiKaiteibi = Integer.Parse(Replace(aDate.CurrentDateDbFormat, "-", ""))
            param.SaishinChusyutubi = ListVo.SaishinChusyutubi
            param.SaishinChusyutujikan = ListVo.SaishinChusyutujikan
            param.Status = ""
            param.AutoOrikomiFlag = "0"
            param.UnitKbn = ListVo.UnitKbn
            param.CreatedUserId = ListVo.CreatedUserId
            param.CreatedDate = ListVo.CreatedDate
            param.CreatedTime = ListVo.CreatedTime
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Insert(sql, param)
        End Sub

        ''' <summary>
        ''' 手配号車情報の改訂繰上げ
        ''' </summary>
        ''' <param name="GousyaVo">手配号車情報</param>
        ''' <remarks></remarks>
        Public Sub InsertByTehaiGousyaKaiteiNo(ByVal GousyaVo As List(Of TShisakuTehaiGousyaVo)) Implements KaiteiUpDao.InsertByTehaiGousyaKaiteiNo
            '            & " VALUES ( " _
            '& " @ShisakuEventCode, " _
            '& " @ShisakuListCode, " _
            '& " @ShisakuListCodeKaiteiNo, " _
            '& " @ShisakuBukaCode, " _
            '& " @ShisakuBlockNo, " _
            '& " @BuhinNoHyoujiJun, " _
            '& " @SortJun, " _
            '& " @GyouId, " _
            '& " @ShisakuGousyaHyoujiJun, " _
            '& " @ShisakuGousya, " _
            '& " @InsuSuryo, " _
            '& " @CreatedUserId, " _
            '& " @CreatedDate, " _
            '& " @CreatedTime, " _
            '& " @UpdatedUserId, " _
            '& " @UpdatedDate, " _
            '& " @UpdatedTime " _
            '& " ) "


            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim sqlList(GousyaVo.Count - 1) As String

            For index As Integer = 0 To GousyaVo.Count - 1
                Dim param As New TShisakuTehaiGousyaVo
                param.ShisakuEventCode = GousyaVo(index).ShisakuEventCode
                param.ShisakuListCode = GousyaVo(index).ShisakuListCode
                param.ShisakuListCodeKaiteiNo = Right("000" + (Integer.Parse(GousyaVo(index).ShisakuListCodeKaiteiNo) + 1).ToString, 3)
                param.ShisakuBukaCode = GousyaVo(index).ShisakuBukaCode
                param.ShisakuBlockNo = GousyaVo(index).ShisakuBlockNo
                param.BuhinNoHyoujiJun = GousyaVo(index).BuhinNoHyoujiJun
                param.SortJun = GousyaVo(index).SortJun
                param.GyouId = GousyaVo(index).GyouId
                param.ShisakuGousyaHyoujiJun = GousyaVo(index).ShisakuGousyaHyoujiJun
                param.ShisakuGousya = GousyaVo(index).ShisakuGousya
                param.InsuSuryo = GousyaVo(index).InsuSuryo
                param.MNounyuShijibi = GousyaVo(index).MNounyuShijibi
                param.TNounyuShijibi = GousyaVo(index).TNounyuShijibi
                param.CreatedUserId = GousyaVo(index).CreatedUserId
                param.CreatedDate = GousyaVo(index).CreatedDate
                param.CreatedTime = GousyaVo(index).CreatedTime
                param.UpdatedUserId = LoginInfo.Now.UserId
                param.UpdatedDate = aDate.CurrentDateDbFormat
                param.UpdatedTime = aDate.CurrentTimeDbFormat

                Dim sql As String = _
                " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ( " _
                & " SHISAKU_EVENT_CODE, " _
                & " SHISAKU_LIST_CODE, " _
                & " SHISAKU_LIST_CODE_KAITEI_NO, " _
                & " SHISAKU_BUKA_CODE, " _
                & " SHISAKU_BLOCK_NO, " _
                & " BUHIN_NO_HYOUJI_JUN, " _
                & " SORT_JUN, " _
                & " GYOU_ID, " _
                & " SHISAKU_GOUSYA_HYOUJI_JUN, " _
                & " SHISAKU_GOUSYA, " _
                & " INSU_SURYO, " _
                & " M_NOUNYU_SHIJIBI, " _
                & " T_NOUNYU_SHIJIBI, " _
                & " CREATED_USER_ID, " _
                & " CREATED_DATE, " _
                & " CREATED_TIME, " _
                & " UPDATED_USER_ID, " _
                & " UPDATED_DATE, " _
                & " UPDATED_TIME " _
                & " ) " _
                & " VALUES ( " _
                & " '" & param.ShisakuEventCode & "' , " _
                & " '" & param.ShisakuListCode & "' , " _
                & " '" & param.ShisakuListCodeKaiteiNo & "' , " _
                & " '" & param.ShisakuBukaCode & "' , " _
                & " '" & param.ShisakuBlockNo & "' , " _
                & " '" & param.BuhinNoHyoujiJun & "' , " _
                & " '" & param.SortJun & "' , " _
                & " '" & param.GyouId & "' , " _
                & " '" & param.ShisakuGousyaHyoujiJun & "' , " _
                & " '" & param.ShisakuGousya & "' , " _
                & " '" & param.InsuSuryo & "' , " _
                & " '" & param.MNounyuShijibi & "' , " _
                & " '" & param.TNounyuShijibi & "' , " _
                & " '" & param.CreatedUserId & "' , " _
                & " '" & param.CreatedDate & "' , " _
                & " '" & param.CreatedTime & "' , " _
                & " '" & param.UpdatedUserId & "' , " _
                & " '" & param.UpdatedDate & "' , " _
                & " '" & param.UpdatedTime & "'  " _
                & " ) "

                sqlList(index) = sql
            Next

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                insert.BeginTransaction()
                Dim errorcount As Integer = 0
                For index As Integer = 0 To GousyaVo.Count - 1
                    'insert.ExecuteNonQuery(sqlList(index))
                    Try
                        '空なら何もしない'
                        If Not StringUtil.IsEmpty(sqlList(index)) Then
                            insert.ExecuteNonQuery(sqlList(index))
                        End If
                    Catch ex As SqlClient.SqlException
                        'プライマリキー違反のみ無視させたい'
                        Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                        If prm < 0 Then
                            Continue For
                            'Dim msg As String = sqlList(index) + ex.Message
                            'MsgBox(ex.Message)
                        Else
                            Continue For
                        End If
                    End Try
                Next
                insert.Commit()
            End Using


            'db.Insert(sql, param)

        End Sub

        ''' <summary>
        ''' 手配基本情報の改訂繰上げ
        ''' </summary>
        ''' <param name="KihonVo">手配基本情報</param>
        ''' <remarks></remarks>
        Public Sub InsertByTehaiKihonKaiteiNo(ByVal KihonVo As List(Of TShisakuTehaiKihonVo)) Implements KaiteiUpDao.InsertByTehaiKihonKaiteiNo
            '            & " VALUES ( " _
            '& " @ShisakuEventCode, " _
            '& " @ShisakuListCode, " _
            '& " @ShisakuListCodeKaiteiNo, " _
            '& " @ShisakuBukaCode, " _
            '& " @ShisakuBlockNo, " _
            '& " @BuhinNoHyoujiJun, " _
            '& " @SortJun, " _
            '& " @Rireki, " _
            '& " @GyouId, " _
            '& " @SenyouMark, " _
            '& " @Level, " _
            '& " @UnitKbn, " _
            '& " @BuhinNo, " _
            '& " @BuhinNoKbn, " _
            '& " @BuhinNoKaiteiNo, " _
            '& " @EdaBan, " _
            '& " @BuhinName, " _
            '& " @ShukeiCode, " _
            '& " @GencyoCkdKbn, " _
            '& " @TehaiKigou, " _
            '& " @Koutan, " _
            '& " @TorihikisakiCode, " _
            '& " @Nouba, " _
            '& " @KyoukuSection, " _
            '& " @NounyuShijibi, " _
            '& " @TotalInsuSuryo, " _
            '& " @Saishiyoufuka, " _
            '& " @ShutuzuYoteiDate, " _
            '& " @StsrDhstba, " _
            '& " @ZaishituKikaku1, " _
            '& " @ZaishituKikaku2, " _
            '& " @ZaishituKikaku3, " _
            '& " @ZaishituMekki, " _
            '& " @ShisakuBankoSuryo, " _
            '& " @ShisakuBankoSuryoU, " _
            '& " @ShisakuBuhinnHi, " _
            '& " @ShisakuKataHi, " _
            '& " @MakerCode, " _
            '& " @Bikou, " _
            '& " @BuhinNoOya, " _
            '& " @BuhinNoKbnOya, " _
            '& " @ErrorKbn, " _
            '& " @AudFlag, " _
            '& " @AudBi, " _
            '& " @KetugouNo, " _
            '& " @Henkaten, " _
            '& " @ShisakuSeihinKbn, " _
            '& " @CreatedUserId, " _
            '& " @CreatedDate, " _
            '& " @CreatedTime, " _
            '& " @UpdatedUserId, " _
            '& " @UpdatedDate, " _
            '& " @UpdatedTime " _
            '& " ) "

            Dim sqlList(KihonVo.Count - 1) As String

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate

            For index As Integer = 0 To KihonVo.Count - 1
                Dim param As New TShisakuTehaiKihonVo
                param.ShisakuEventCode = KihonVo(index).ShisakuEventCode
                param.ShisakuListCode = KihonVo(index).ShisakuListCode
                param.ShisakuListCodeKaiteiNo = Right("000" + (Integer.Parse(KihonVo(index).ShisakuListCodeKaiteiNo) + 1).ToString, 3)
                param.ShisakuBukaCode = KihonVo(index).ShisakuBukaCode
                param.ShisakuBlockNo = KihonVo(index).ShisakuBlockNo
                param.BuhinNoHyoujiJun = KihonVo(index).BuhinNoHyoujiJun
                param.SortJun = KihonVo(index).SortJun
                param.Rireki = KihonVo(index).Rireki
                param.GyouId = KihonVo(index).GyouId
                param.SenyouMark = KihonVo(index).SenyouMark
                param.Level = KihonVo(index).Level
                param.UnitKbn = KihonVo(index).UnitKbn
                param.BuhinNo = KihonVo(index).BuhinNo
                param.BuhinNoKbn = KihonVo(index).BuhinNoKbn
                param.BuhinNoKaiteiNo = KihonVo(index).BuhinNoKaiteiNo
                param.EdaBan = KihonVo(index).EdaBan
                param.BuhinName = KihonVo(index).BuhinName
                param.ShukeiCode = KihonVo(index).ShukeiCode
                param.GencyoCkdKbn = KihonVo(index).GencyoCkdKbn
                param.TehaiKigou = KihonVo(index).TehaiKigou
                param.Koutan = KihonVo(index).Koutan
                param.TorihikisakiCode = KihonVo(index).TorihikisakiCode
                param.Nouba = KihonVo(index).Nouba
                param.KyoukuSection = KihonVo(index).KyoukuSection
                param.NounyuShijibi = KihonVo(index).NounyuShijibi
                param.TotalInsuSuryo = KihonVo(index).TotalInsuSuryo
                param.Saishiyoufuka = KihonVo(index).Saishiyoufuka

                param.GousyaHachuTenkaiFlg = KihonVo(index).GousyaHachuTenkaiFlg
                param.GousyaHachuTenkaiUnitKbn = KihonVo(index).GousyaHachuTenkaiUnitKbn

                param.ShutuzuYoteiDate = KihonVo(index).ShutuzuYoteiDate
                param.ShutuzuJisekiDate = KihonVo(index).ShutuzuJisekiDate
                param.ShutuzuJisekiKaiteiNo = KihonVo(index).ShutuzuJisekiKaiteiNo
                param.ShutuzuJisekiStsrDhstba = KihonVo(index).ShutuzuJisekiStsrDhstba
                param.SaisyuSetsuhenDate = KihonVo(index).SaisyuSetsuhenDate
                param.SaisyuSetsuhenKaiteiNo = KihonVo(index).SaisyuSetsuhenKaiteiNo
                param.StsrDhstba = KihonVo(index).StsrDhstba
                param.ZaishituKikaku1 = KihonVo(index).ZaishituKikaku1
                param.ZaishituKikaku2 = KihonVo(index).ZaishituKikaku2
                param.ZaishituKikaku3 = KihonVo(index).ZaishituKikaku3
                param.ZaishituMekki = KihonVo(index).ZaishituMekki
                param.TsukurikataSeisaku = KihonVo(index).TsukurikataSeisaku
                param.TsukurikataKatashiyou1 = KihonVo(index).TsukurikataKatashiyou1
                param.TsukurikataKatashiyou2 = KihonVo(index).TsukurikataKatashiyou2
                param.TsukurikataKatashiyou3 = KihonVo(index).TsukurikataKatashiyou3
                param.TsukurikataTigu = KihonVo(index).TsukurikataTigu
                param.TsukurikataNounyu = KihonVo(index).TsukurikataNounyu
                param.TsukurikataKibo = KihonVo(index).TsukurikataKibo
                param.BaseBuhinFlg = KihonVo(index).BaseBuhinFlg
                param.ShisakuBankoSuryo = KihonVo(index).ShisakuBankoSuryo
                param.ShisakuBankoSuryoU = KihonVo(index).ShisakuBankoSuryoU
                param.MaterialInfoLength = KihonVo(index).MaterialInfoLength
                param.MaterialInfoWidth = KihonVo(index).MaterialInfoWidth

                param.ZairyoSunpoX = KihonVo(index).ZairyoSunpoX
                param.ZairyoSunpoY = KihonVo(index).ZairyoSunpoY
                param.ZairyoSunpoZ = KihonVo(index).ZairyoSunpoZ
                param.ZairyoSunpoXy = KihonVo(index).ZairyoSunpoXy
                param.ZairyoSunpoXz = KihonVo(index).ZairyoSunpoXz
                param.ZairyoSunpoYz = KihonVo(index).ZairyoSunpoYz

                param.MaterialInfoOrderTarget = KihonVo(index).MaterialInfoOrderTarget
                param.MaterialInfoOrderTargetDate = KihonVo(index).MaterialInfoOrderTargetDate
                param.MaterialInfoOrderChk = KihonVo(index).MaterialInfoOrderChk
                param.MaterialInfoOrderChkDate = KihonVo(index).MaterialInfoOrderChkDate
                param.DataItemKaiteiNo = KihonVo(index).DataItemKaiteiNo
                param.DataItemAreaName = KihonVo(index).DataItemAreaName
                param.DataItemSetName = KihonVo(index).DataItemSetName
                param.DataItemKaiteiInfo = KihonVo(index).DataItemKaiteiInfo
                param.DataItemDataProvision = KihonVo(index).DataItemDataProvision
                param.DataItemDataProvisionDate = KihonVo(index).DataItemDataProvisionDate

                param.ShisakuBuhinnHi = KihonVo(index).ShisakuBuhinnHi
                param.ShisakuKataHi = KihonVo(index).ShisakuKataHi
                param.MakerCode = KihonVo(index).MakerCode
                param.Bikou = KihonVo(index).Bikou
                param.BuhinNoOya = KihonVo(index).BuhinNoOya
                param.BuhinNoKbnOya = KihonVo(index).BuhinNoKbnOya
                param.ErrorKbn = KihonVo(index).ErrorKbn
                param.AudFlag = KihonVo(index).AudFlag
                param.AudBi = KihonVo(index).AudBi
                param.KetugouNo = KihonVo(index).KetugouNo
                param.Henkaten = KihonVo(index).Henkaten
                param.ShisakuSeihinKbn = KihonVo(index).ShisakuSeihinKbn
                param.AutoOrikomiKaiteiNo = KihonVo(index).AutoOrikomiKaiteiNo
                param.CreatedUserId = KihonVo(index).CreatedUserId
                param.CreatedDate = KihonVo(index).CreatedDate
                param.CreatedTime = KihonVo(index).CreatedTime
                param.UpdatedUserId = LoginInfo.Now.UserId
                param.UpdatedDate = aDate.CurrentDateDbFormat
                param.UpdatedTime = aDate.CurrentTimeDbFormat


                'INSERT構文の修正 (& param.AudBi & ", " _)→(& " '" & param.AudBi & "' , " _)
                Dim sql As String = _
                " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ( " _
                & " SHISAKU_EVENT_CODE, " _
                & " SHISAKU_LIST_CODE, " _
                & " SHISAKU_LIST_CODE_KAITEI_NO, " _
                & " SHISAKU_BUKA_CODE, " _
                & " SHISAKU_BLOCK_NO, " _
                & " BUHIN_NO_HYOUJI_JUN, " _
                & " SORT_JUN, " _
                & " RIREKI, " _
                & " GYOU_ID, " _
                & " SENYOU_MARK, " _
                & " LEVEL, " _
                & " UNIT_KBN, " _
                & " BUHIN_NO, " _
                & " BUHIN_NO_KBN, " _
                & " BUHIN_NO_KAITEI_NO, " _
                & " EDA_BAN, " _
                & " BUHIN_NAME, " _
                & " SHUKEI_CODE, " _
                & " GENCYO_CKD_KBN, " _
                & " TEHAI_KIGOU, " _
                & " KOUTAN, " _
                & " TORIHIKISAKI_CODE, " _
                & " NOUBA, " _
                & " KYOUKU_SECTION, " _
                & " NOUNYU_SHIJIBI, " _
                & " TOTAL_INSU_SURYO, " _
                & " SAISHIYOUFUKA, " _
                & " GOUSYA_HACHU_TENKAI_FLG, " _
                & " GOUSYA_HACHU_TENKAI_UNIT_KBN, " _
                & " SHUTUZU_YOTEI_DATE, " _
                & " SHUTUZU_JISEKI_DATE, " _
                & " SHUTUZU_JISEKI_KAITEI_NO, " _
                & " SHUTUZU_JISEKI_STSR_DHSTBA, " _
                & " SAISYU_SETSUHEN_DATE, " _
                & " SAISYU_SETSUHEN_KAITEI_NO, " _
                & " STSR_DHSTBA, " _
                & " ZAISHITU_KIKAKU_1, " _
                & " ZAISHITU_KIKAKU_2, " _
                & " ZAISHITU_KIKAKU_3, " _
                & " ZAISHITU_MEKKI, " _
                & " TSUKURIKATA_SEISAKU, " _
                & " TSUKURIKATA_KATASHIYOU_1, " _
                & " TSUKURIKATA_KATASHIYOU_2, " _
                & " TSUKURIKATA_KATASHIYOU_3, " _
                & " TSUKURIKATA_TIGU, " _
                & " TSUKURIKATA_NOUNYU, " _
                & " TSUKURIKATA_KIBO, " _
                & " BASE_BUHIN_FLG, " _
                & " SHISAKU_BANKO_SURYO, " _
                & " SHISAKU_BANKO_SURYO_U, " _
                & " MATERIAL_INFO_LENGTH, " _
                & " MATERIAL_INFO_WIDTH, " _
                & " ZAIRYO_SUNPO_X, " _
                & " ZAIRYO_SUNPO_Y, " _
                & " ZAIRYO_SUNPO_Z, " _
                & " ZAIRYO_SUNPO_XY, " _
                & " ZAIRYO_SUNPO_XZ, " _
                & " ZAIRYO_SUNPO_YZ, " _
                & " MATERIAL_INFO_ORDER_TARGET, " _
                & " MATERIAL_INFO_ORDER_TARGET_DATE, " _
                & " MATERIAL_INFO_ORDER_CHK, " _
                & " MATERIAL_INFO_ORDER_CHK_DATE, " _
                & " DATA_ITEM_KAITEI_NO, " _
                & " DATA_ITEM_AREA_NAME, " _
                & " DATA_ITEM_SET_NAME, " _
                & " DATA_ITEM_KAITEI_INFO, " _
                & " DATA_ITEM_DATA_PROVISION, " _
                & " DATA_ITEM_DATA_PROVISION_DATE, " _
                & " SHISAKU_BUHINN_HI, " _
                & " SHISAKU_KATA_HI, " _
                & " MAKER_CODE, " _
                & " BIKOU, " _
                & " BUHIN_NO_OYA, " _
                & " BUHIN_NO_KBN_OYA, " _
                & " ERROR_KBN, " _
                & " AUD_FLAG, " _
                & " AUD_BI, " _
                & " KETUGOU_NO, " _
                & " HENKATEN, " _
                & " SHISAKU_SEIHIN_KBN, " _
                & " AUTO_ORIKOMI_KAITEI_NO," _
                & " CREATED_USER_ID, " _
                & " CREATED_DATE, " _
                & " CREATED_TIME, " _
                & " UPDATED_USER_ID, " _
                & " UPDATED_DATE, " _
                & " UPDATED_TIME " _
                & " ) " _
                & " VALUES ( " _
                & " '" & param.ShisakuEventCode & "' , " _
                & " '" & param.ShisakuListCode & "' , " _
                & " '" & param.ShisakuListCodeKaiteiNo & "' , " _
                & " '" & param.ShisakuBukaCode & "' , " _
                & " '" & param.ShisakuBlockNo & "' , " _
                & param.BuhinNoHyoujiJun & ", " _
                & param.SortJun & ", " _
                & " '" & param.Rireki & "' , " _
                & " '" & param.GyouId & "' , " _
                & " '" & param.SenyouMark & "' , " _
                & param.Level & ", " _
                & " '" & param.UnitKbn & "' , " _
                & " '" & param.BuhinNo & "' , " _
                & " '" & param.BuhinNoKbn & "' , " _
                & " '" & param.BuhinNoKaiteiNo & "' , " _
                & " '" & param.EdaBan & "' , " _
                & " '" & param.BuhinName & "' , " _
                & " '" & param.ShukeiCode & "' , " _
                & " '" & param.GencyoCkdKbn & "' , " _
                & " '" & param.TehaiKigou & "' , " _
                & " '" & param.Koutan & "' , " _
                & " '" & param.TorihikisakiCode & "' , " _
                & " '" & param.Nouba & "' , " _
                & " '" & param.KyoukuSection & "' , " _
                & param.NounyuShijibi & ", " _
                & param.TotalInsuSuryo & ", " _
                & " '" & param.Saishiyoufuka & "' , " _
                & " '" & param.GousyaHachuTenkaiFlg & "' , " _
                & " '" & param.GousyaHachuTenkaiUnitKbn & "' , " _
                & param.ShutuzuYoteiDate & ", " _
                & param.ShutuzuJisekiDate & ", " _
                & " '" & param.ShutuzuJisekiKaiteiNo & "' , " _
                & " '" & param.ShutuzuJisekiStsrDhstba & "' , " _
                & param.SaisyuSetsuhenDate & ", " _
                & " '" & param.SaisyuSetsuhenKaiteiNo & "' , " _
                & " '" & param.StsrDhstba & "' , " _
                & " '" & param.ZaishituKikaku1 & "' , " _
                & " '" & param.ZaishituKikaku2 & "' , " _
                & " '" & param.ZaishituKikaku3 & "' , " _
                & " '" & param.ZaishituMekki & "' , " _
                & " '" & param.TsukurikataSeisaku & "' , " _
                & " '" & param.TsukurikataKatashiyou1 & "' , " _
                & " '" & param.TsukurikataKatashiyou2 & "' , " _
                & " '" & param.TsukurikataKatashiyou3 & "' , " _
                & " '" & param.TsukurikataTigu & "' , " _
                & param.TsukurikataNounyu & ", " _
                & " '" & param.TsukurikataKibo & "' , " _
                & " '" & param.BaseBuhinFlg & "' , " _
                & " '" & param.ShisakuBankoSuryo & "' , " _
                & " '" & param.ShisakuBankoSuryoU & "' , " _
                & param.MaterialInfoLength & ", " _
                & param.MaterialInfoWidth & ", " _
                & param.ZairyoSunpoX & ", " _
                & param.ZairyoSunpoY & ", " _
                & param.ZairyoSunpoZ & ", " _
                & param.ZairyoSunpoXy & ", " _
                & param.ZairyoSunpoXz & ", " _
                & param.ZairyoSunpoYz & ", " _
                & " '" & param.MaterialInfoOrderTarget & "' , " _
                & " '" & param.MaterialInfoOrderTargetDate & "' , " _
                & " '" & param.MaterialInfoOrderChk & "' , " _
                & " '" & param.MaterialInfoOrderChkDate & "' , " _
                & " '" & param.DataItemKaiteiNo & "' , " _
                & " '" & param.DataItemAreaName & "' , " _
                & " '" & param.DataItemSetName & "' , " _
                & " '" & param.DataItemKaiteiInfo & "' , " _
                & " '" & param.DataItemDataProvision & "' , " _
                & " '" & param.DataItemDataProvisionDate & "' , " _
                & param.ShisakuBuhinnHi & ", " _
                & param.ShisakuKataHi & ", " _
                & " '" & param.MakerCode & "' , " _
                & " '" & param.Bikou & "' , " _
                & " '" & param.BuhinNoOya & "' , " _
                & " '" & param.BuhinNoKbnOya & "' , " _
                & " '" & param.ErrorKbn & "' , " _
                & " '" & param.AudFlag & "' , " _
                & " '" & param.AudBi & "' , " _
                & " '" & param.KetugouNo & "' , " _
                & " '" & param.Henkaten & "' , " _
                & " '" & param.ShisakuSeihinKbn & "' , " _
                & " '" & param.AutoOrikomiKaiteiNo & "' , " _
                & " '" & param.CreatedUserId & "' , " _
                & " '" & param.CreatedDate & "' , " _
                & " '" & param.CreatedTime & "' , " _
                & " '" & param.UpdatedUserId & "' , " _
                & " '" & param.UpdatedDate & "' , " _
                & " '" & param.UpdatedTime & "'  " _
                & " ) "
                '↓↓2014/09/24 酒井 ADD BEGIN
                '作り方項目を追加
                '↑↑2014/09/24 酒井 ADD END
                sqlList(index) = sql

            Next

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                'insert.BeginTransaction()
                Dim errorcount As Integer = 0
                For index As Integer = 0 To KihonVo.Count - 1
                    'insert.ExecuteNonQuery(sqlList(index))
                    Try
                        '空なら何もしない'
                        If Not StringUtil.IsEmpty(sqlList(index)) Then
                            insert.ExecuteNonQuery(sqlList(index))
                        End If
                    Catch ex As SqlClient.SqlException
                        'プライマリキー違反のみ無視させたい'
                        Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                        If prm < 0 Then
                            Continue For
                            'Dim msg As String = sqlList(index) + ex.Message
                            'MsgBox(ex.Message)
                        Else
                            Continue For
                        End If
                    End Try
                Next
                insert.Commit()
            End Using
        End Sub

        ''' <summary>
        ''' 手配基本情報の取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Function FindByTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVo) Implements KaiteiUpDao.FindByTehaiKihon
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" WHERE(SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE) ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) ")
                .AppendLine(" ORDER BY BUHIN_NO_HYOUJI_JUN ")
            End With
            'Dim sql As String = _
            '" SELECT * " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON TK " _
            '& " WHERE " _
            '& " TK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            '& " AND TK.SHISAKU_LIST_CODE = @ShisakuListCode " _
            '& " AND TK.SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            '& " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON " _
            '& " WHERE(SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE) " _
            '& " AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) " _
            '& " ORDER BY BUHIN_NO_HYOUJI_JUN "

            Dim db As New EBomDbClient
            'Dim param As New TShisakuTehaiKihonVo
            'param.ShisakuEventCode = shisakuEventCode
            'param.ShisakuListCode = shisakuListCode

            Return db.QueryForList(Of TShisakuTehaiKihonVo)(sql.ToString)

        End Function

        ''' <summary>
        ''' 手配号車情報の取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Function FindByTehaiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiGousyaVo) Implements KaiteiUpDao.FindByTehaiGousya
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT TG.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG")
                .AppendLine(" WHERE ")
                .AppendFormat(" TG.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE(SHISAKU_EVENT_CODE = TG.SHISAKU_EVENT_CODE) ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TG.SHISAKU_LIST_CODE ) ")
                .AppendLine(" ORDER BY BUHIN_NO_HYOUJI_JUN ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiGousyaVo)(sql.ToString)
        End Function


        ''' <summary>
        ''' 試作手配出図実績情報の取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Function FindByTehaiShutuzuJiseki(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiShutuzuJisekiVo) Implements KaiteiUpDao.FindByTehaiShutuzuJiseki
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI TK ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI ")
                .AppendLine(" WHERE(SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE) ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiShutuzuJisekiVo)(sql.ToString)

        End Function
        ''' <summary>
        ''' 試作手配出図実績手入力情報の取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Function FindByTehaiShutuzuJisekiInput(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiShutuzuJisekiInputVo) Implements KaiteiUpDao.FindByTehaiShutuzuJisekiInput
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI_INPUT TK ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI_INPUT ")
                .AppendLine(" WHERE(SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE) ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiShutuzuJisekiInputVo)(sql.ToString)

        End Function
        ''' <summary>
        ''' 試作手配出図織込情報の取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Function FindByTehaiShutuzuOrikomi(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiShutuzuOrikomiVo) Implements KaiteiUpDao.FindByTehaiShutuzuOrikomi
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_ORIKOMI TK ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_ORIKOMI ")
                .AppendLine(" WHERE(SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE) ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiShutuzuOrikomiVo)(sql.ToString)

        End Function
        ''' <summary>
        ''' 試作手配帳情報（号車グループ情報）の取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Function FindByTehaiGousyaGroup(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiGousyaGroupVo) Implements KaiteiUpDao.FindByTehaiGousyaGroup
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA_GROUP TK ")
                .AppendLine(" WHERE ")
                .AppendFormat(" TK.SHISAKU_EVENT_CODE = '{0}' ", shisakuEventCode)
                .AppendFormat(" AND TK.SHISAKU_LIST_CODE = '{0}' ", shisakuListCode)
                .AppendLine(" AND TK.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine(" SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA_GROUP ")
                .AppendLine(" WHERE(SHISAKU_EVENT_CODE = TK.SHISAKU_EVENT_CODE) ")
                .AppendLine(" AND SHISAKU_LIST_CODE = TK.SHISAKU_LIST_CODE ) ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuTehaiGousyaGroupVo)(sql.ToString)

        End Function

        ''' <summary>
        ''' 試作手配出図実績情報の改訂繰上げ
        ''' </summary>
        ''' <param name="Vo">試作手配出図実績情報</param>
        ''' <param name="KaiteiNo">リストコード改訂№</param>
        ''' <remarks></remarks>
        Public Sub InsertByTehaiShutuzuJisekiKaiteiNo(ByVal Vo As List(Of TShisakuTehaiShutuzuJisekiVo), ByVal KaiteiNo As String) Implements KaiteiUpDao.InsertByTehaiShutuzuJisekiKaiteiNo

            Dim sqlList(Vo.Count - 1) As String

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate

            For index As Integer = 0 To Vo.Count - 1
                Dim param As New TShisakuTehaiShutuzuJisekiVo
                param.ShisakuEventCode = Vo(index).ShisakuEventCode
                param.ShisakuListCode = Vo(index).ShisakuListCode
                param.ShisakuListCodeKaiteiNo = Right("000" + (Integer.Parse(KaiteiNo) + 1).ToString, 3)
                param.ShisakuBlockNo = Vo(index).ShisakuBlockNo
                param.BuhinNo = Vo(index).BuhinNo
                param.ShutuzuJisekiKaiteiNo = Vo(index).ShutuzuJisekiKaiteiNo
                param.KataDaihyouBuhinNo = Vo(index).KataDaihyouBuhinNo
                param.ShutuzuJisekiJyuryoDate = Vo(index).ShutuzuJisekiJyuryoDate
                param.ShutuzuJisekiStsrDhstba = Vo(index).ShutuzuJisekiStsrDhstba
                param.ShutuzuKenmei = Vo(index).ShutuzuKenmei
                param.Comment = Vo(index).Comment

                param.CreatedUserId = Vo(index).CreatedUserId
                param.CreatedDate = Vo(index).CreatedDate
                param.CreatedTime = Vo(index).CreatedTime
                param.UpdatedUserId = LoginInfo.Now.UserId
                param.UpdatedDate = aDate.CurrentDateDbFormat
                param.UpdatedTime = aDate.CurrentTimeDbFormat


                'INSERT構文
                Dim sql As String = _
                " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI ( " _
                & " SHISAKU_EVENT_CODE, " _
                & " SHISAKU_LIST_CODE, " _
                & " SHISAKU_LIST_CODE_KAITEI_NO, " _
                & " SHISAKU_BLOCK_NO, " _
                & " BUHIN_NO, " _
                & " SHUTUZU_JISEKI_KAITEI_NO, " _
                & " KATA_DAIHYOU_BUHIN_NO, " _
                & " SHUTUZU_JISEKI_JYURYO_DATE, " _
                & " SHUTUZU_JISEKI_STSR_DHSTBA, " _
                & " SHUTUZU_KENMEI, " _
                & " COMMENT, " _
                & " CREATED_USER_ID, " _
                & " CREATED_DATE, " _
                & " CREATED_TIME, " _
                & " UPDATED_USER_ID, " _
                & " UPDATED_DATE, " _
                & " UPDATED_TIME " _
                & " ) " _
                & " VALUES ( " _
                & " '" & param.ShisakuEventCode & "' , " _
                & " '" & param.ShisakuListCode & "' , " _
                & " '" & param.ShisakuListCodeKaiteiNo & "' , " _
                & " '" & param.ShisakuBlockNo & "' , " _
                & " '" & param.BuhinNo & "' , " _
                & " '" & param.ShutuzuJisekiKaiteiNo & "' , " _
                & " '" & param.KataDaihyouBuhinNo & "' , " _
                & param.ShutuzuJisekiJyuryoDate & " , " _
                & " '" & param.ShutuzuJisekiStsrDhstba & "' , " _
                & " '" & param.ShutuzuKenmei & "' , " _
                & " '" & param.Comment & "' , " _
                & " '" & param.CreatedUserId & "' , " _
                & " '" & param.CreatedDate & "' , " _
                & " '" & param.CreatedTime & "' , " _
                & " '" & param.UpdatedUserId & "' , " _
                & " '" & param.UpdatedDate & "' , " _
                & " '" & param.UpdatedTime & "'  " _
                & " ) "
                sqlList(index) = sql

            Next

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                Dim errorcount As Integer = 0
                For index As Integer = 0 To Vo.Count - 1
                    Try
                        '空なら何もしない'
                        If Not StringUtil.IsEmpty(sqlList(index)) Then
                            insert.ExecuteNonQuery(sqlList(index))
                        End If
                    Catch ex As SqlClient.SqlException
                        'プライマリキー違反のみ無視させたい'
                        Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                        If prm < 0 Then
                            Continue For
                        Else
                            Continue For
                        End If
                    End Try
                Next
                insert.Commit()
            End Using
        End Sub
        ''' <summary>
        ''' 試作手配出図実績手入力情報の改訂繰上げ
        ''' </summary>
        ''' <param name="Vo">試作手配出図実績手入力情報</param>
        ''' <param name="KaiteiNo">リストコード改訂№</param>
        ''' <remarks></remarks>
        Public Sub InsertByTehaiShutuzuJisekiInputKaiteiNo(ByVal Vo As List(Of TShisakuTehaiShutuzuJisekiInputVo), ByVal KaiteiNo As String) Implements KaiteiUpDao.InsertByTehaiShutuzuJisekiInputKaiteiNo

            Dim sqlList(Vo.Count - 1) As String

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate

            For index As Integer = 0 To Vo.Count - 1
                Dim param As New TShisakuTehaiShutuzuJisekiInputVo
                param.ShisakuEventCode = Vo(index).ShisakuEventCode
                param.ShisakuListCode = Vo(index).ShisakuListCode
                param.ShisakuListCodeKaiteiNo = Right("000" + (Integer.Parse(KaiteiNo) + 1).ToString, 3)
                param.ShisakuBlockNo = Vo(index).ShisakuBlockNo
                param.BuhinNo = Vo(index).BuhinNo
                param.ShutuzuJisekiKaiteiNo = Vo(index).ShutuzuJisekiKaiteiNo
                'param.KataDaihyouBuhinNo = Vo(index).KataDaihyouBuhinNo
                param.ShutuzuJisekiJyuryoDate = Vo(index).ShutuzuJisekiJyuryoDate
                param.ShutuzuJisekiStsrDhstba = Vo(index).ShutuzuJisekiStsrDhstba
                param.ShutuzuKenmei = Vo(index).ShutuzuKenmei
                param.Comment = Vo(index).Comment

                param.CreatedUserId = Vo(index).CreatedUserId
                param.CreatedDate = Vo(index).CreatedDate
                param.CreatedTime = Vo(index).CreatedTime
                param.UpdatedUserId = LoginInfo.Now.UserId
                param.UpdatedDate = aDate.CurrentDateDbFormat
                param.UpdatedTime = aDate.CurrentTimeDbFormat


                'INSERT構文
                Dim sql As String = _
                " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI_INPUT ( " _
                & " SHISAKU_EVENT_CODE, " _
                & " SHISAKU_LIST_CODE, " _
                & " SHISAKU_LIST_CODE_KAITEI_NO, " _
                & " SHISAKU_BLOCK_NO, " _
                & " BUHIN_NO, " _
                & " SHUTUZU_JISEKI_KAITEI_NO, " _
                & " SHUTUZU_JISEKI_JYURYO_DATE, " _
                & " SHUTUZU_JISEKI_STSR_DHSTBA, " _
                & " SHUTUZU_KENMEI, " _
                & " COMMENT, " _
                & " CREATED_USER_ID, " _
                & " CREATED_DATE, " _
                & " CREATED_TIME, " _
                & " UPDATED_USER_ID, " _
                & " UPDATED_DATE, " _
                & " UPDATED_TIME " _
                & " ) " _
                & " VALUES ( " _
                & " '" & param.ShisakuEventCode & "' , " _
                & " '" & param.ShisakuListCode & "' , " _
                & " '" & param.ShisakuListCodeKaiteiNo & "' , " _
                & " '" & param.ShisakuBlockNo & "' , " _
                & " '" & param.BuhinNo & "' , " _
                & " '" & param.ShutuzuJisekiKaiteiNo & "' , " _
                & param.ShutuzuJisekiJyuryoDate & " , " _
                & " '" & param.ShutuzuJisekiStsrDhstba & "' , " _
                & " '" & param.ShutuzuKenmei & "' , " _
                & " '" & param.Comment & "' , " _
                & " '" & param.CreatedUserId & "' , " _
                & " '" & param.CreatedDate & "' , " _
                & " '" & param.CreatedTime & "' , " _
                & " '" & param.UpdatedUserId & "' , " _
                & " '" & param.UpdatedDate & "' , " _
                & " '" & param.UpdatedTime & "'  " _
                & " ) "
                sqlList(index) = sql

            Next

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                Dim errorcount As Integer = 0
                For index As Integer = 0 To Vo.Count - 1
                    Try
                        '空なら何もしない'
                        If Not StringUtil.IsEmpty(sqlList(index)) Then
                            insert.ExecuteNonQuery(sqlList(index))
                        End If
                    Catch ex As SqlClient.SqlException
                        'プライマリキー違反のみ無視させたい'
                        Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                        If prm < 0 Then
                            Continue For
                        Else
                            Continue For
                        End If
                    End Try
                Next
                insert.Commit()
            End Using
        End Sub
        ''' <summary>
        ''' 試作手配出図織込情報の改訂繰上げ
        ''' </summary>
        ''' <param name="Vo">試作手配出図織込情報</param>
        ''' <param name="KaiteiNo">リストコード改訂№</param>
        ''' <remarks></remarks>
        Public Sub InsertByTehaiShutuzuOrikomiKaiteiNo(ByVal Vo As List(Of TShisakuTehaiShutuzuOrikomiVo), ByVal KaiteiNo As String) Implements KaiteiUpDao.InsertByTehaiShutuzuOrikomiKaiteiNo

            Dim sqlList(Vo.Count - 1) As String

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate

            For index As Integer = 0 To Vo.Count - 1
                Dim param As New TShisakuTehaiShutuzuOrikomiVo
                param.ShisakuEventCode = Vo(index).ShisakuEventCode
                param.ShisakuListCode = Vo(index).ShisakuListCode
                param.ShisakuListCodeKaiteiNo = Right("000" + (Integer.Parse(KaiteiNo) + 1).ToString, 3)
                param.ShisakuBlockNo = Vo(index).ShisakuBlockNo
                param.BuhinNo = Vo(index).BuhinNo
                param.KataDaihyouBuhinNo = Vo(index).KataDaihyouBuhinNo
                param.Kakutei = Vo(index).Kakutei

                param.NewShutuzuJisekiJyuryoDate = Vo(index).NewShutuzuJisekiJyuryoDate
                param.NewShutuzuJisekiKaiteiNo = Vo(index).NewShutuzuJisekiKaiteiNo
                param.NewShutuzuJisekiStsrDhstba = Vo(index).NewShutuzuJisekiStsrDhstba
                param.NewShutuzuKenmei = Vo(index).NewShutuzuKenmei

                param.LastShutuzuJisekiJyuryoDate = Vo(index).LastShutuzuJisekiJyuryoDate
                param.LastShutuzuJisekiKaiteiNo = Vo(index).LastShutuzuJisekiKaiteiNo
                param.LastShutuzuJisekiStsrDhstba = Vo(index).LastShutuzuJisekiStsrDhstba
                param.LastShutuzuKenmei = Vo(index).LastShutuzuKenmei

                param.CreatedUserId = Vo(index).CreatedUserId
                param.CreatedDate = Vo(index).CreatedDate
                param.CreatedTime = Vo(index).CreatedTime
                param.UpdatedUserId = LoginInfo.Now.UserId
                param.UpdatedDate = aDate.CurrentDateDbFormat
                param.UpdatedTime = aDate.CurrentTimeDbFormat


                'INSERT構文
                Dim sql As String = _
                " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_ORIKOMI ( " _
                & " SHISAKU_EVENT_CODE, " _
                & " SHISAKU_LIST_CODE, " _
                & " SHISAKU_LIST_CODE_KAITEI_NO, " _
                & " SHISAKU_BLOCK_NO, " _
                & " BUHIN_NO, " _
                & " KATA_DAIHYOU_BUHIN_NO, " _
                & " KAKUTEI, " _
                & " NEW_SHUTUZU_JISEKI_JYURYO_DATE, " _
                & " NEW_SHUTUZU_JISEKI_KAITEI_NO, " _
                & " NEW_SHUTUZU_JISEKI_STSR_DHSTBA, " _
                & " NEW_SHUTUZU_KENMEI, " _
                & " LAST_SHUTUZU_JISEKI_JYURYO_DATE, " _
                & " LAST_SHUTUZU_JISEKI_KAITEI_NO, " _
                & " LAST_SHUTUZU_JISEKI_STSR_DHSTBA, " _
                & " LAST_SHUTUZU_KENMEI, " _
                & " CREATED_USER_ID, " _
                & " CREATED_DATE, " _
                & " CREATED_TIME, " _
                & " UPDATED_USER_ID, " _
                & " UPDATED_DATE, " _
                & " UPDATED_TIME " _
                & " ) " _
                & " VALUES ( " _
                & " '" & param.ShisakuEventCode & "' , " _
                & " '" & param.ShisakuListCode & "' , " _
                & " '" & param.ShisakuListCodeKaiteiNo & "' , " _
                & " '" & param.ShisakuBlockNo & "' , " _
                & " '" & param.BuhinNo & "' , " _
                & " '" & param.KataDaihyouBuhinNo & "' , " _
                & " '" & param.Kakutei & "' , " _
                & param.NewShutuzuJisekiJyuryoDate & " , " _
                & " '" & param.NewShutuzuJisekiKaiteiNo & "' , " _
                & " '" & param.NewShutuzuJisekiStsrDhstba & "' , " _
                & " '" & param.NewShutuzuKenmei & "' , " _
                & param.LastShutuzuJisekiJyuryoDate & " , " _
                & " '" & param.LastShutuzuJisekiKaiteiNo & "' , " _
                & " '" & param.LastShutuzuJisekiStsrDhstba & "' , " _
                & " '" & param.LastShutuzuKenmei & "' , " _
                & " '" & param.CreatedUserId & "' , " _
                & " '" & param.CreatedDate & "' , " _
                & " '" & param.CreatedTime & "' , " _
                & " '" & param.UpdatedUserId & "' , " _
                & " '" & param.UpdatedDate & "' , " _
                & " '" & param.UpdatedTime & "'  " _
                & " ) "
                sqlList(index) = sql

            Next

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                Dim errorcount As Integer = 0
                For index As Integer = 0 To Vo.Count - 1
                    Try
                        '空なら何もしない'
                        If Not StringUtil.IsEmpty(sqlList(index)) Then
                            insert.ExecuteNonQuery(sqlList(index))
                        End If
                    Catch ex As SqlClient.SqlException
                        'プライマリキー違反のみ無視させたい'
                        Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                        If prm < 0 Then
                            Continue For
                        Else
                            Continue For
                        End If
                    End Try
                Next
                insert.Commit()
            End Using
        End Sub
        ''' <summary>
        ''' 試作手配帳情報（号車グループ情報）の改訂繰上げ
        ''' </summary>
        ''' <param name="Vo">試作手配帳情報（号車グループ情報）</param>
        ''' <param name="KaiteiNo">リストコード改訂№</param>
        ''' <remarks></remarks>
        Public Sub InsertByTehaiGousyaGroupKaiteiNo(ByVal Vo As List(Of TShisakuTehaiGousyaGroupVo), ByVal KaiteiNo As String) Implements KaiteiUpDao.InsertByTehaiGousyaGroupKaiteiNo

            Dim sqlList(Vo.Count - 1) As String

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate

            For index As Integer = 0 To Vo.Count - 1
                Dim param As New TShisakuTehaiGousyaGroupVo
                param.ShisakuEventCode = Vo(index).ShisakuEventCode
                param.ShisakuListCode = Vo(index).ShisakuListCode
                param.ShisakuListCodeKaiteiNo = Right("000" + (Integer.Parse(KaiteiNo) + 1).ToString, 3)
                param.ShisakuGousya = Vo(index).ShisakuGousya
                param.ShisakuGousyaGroup = Vo(index).ShisakuGousyaGroup

                param.CreatedUserId = Vo(index).CreatedUserId
                param.CreatedDate = Vo(index).CreatedDate
                param.CreatedTime = Vo(index).CreatedTime
                param.UpdatedUserId = LoginInfo.Now.UserId
                param.UpdatedDate = aDate.CurrentDateDbFormat
                param.UpdatedTime = aDate.CurrentTimeDbFormat


                'INSERT構文
                Dim sql As String = _
                " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA_GROUP ( " _
                & " SHISAKU_EVENT_CODE, " _
                & " SHISAKU_LIST_CODE, " _
                & " SHISAKU_LIST_CODE_KAITEI_NO, " _
                & " SHISAKU_GOUSYA, " _
                & " SHISAKU_GOUSYA_GROUP, " _
                & " CREATED_USER_ID, " _
                & " CREATED_DATE, " _
                & " CREATED_TIME, " _
                & " UPDATED_USER_ID, " _
                & " UPDATED_DATE, " _
                & " UPDATED_TIME " _
                & " ) " _
                & " VALUES ( " _
                & " '" & param.ShisakuEventCode & "' , " _
                & " '" & param.ShisakuListCode & "' , " _
                & " '" & param.ShisakuListCodeKaiteiNo & "' , " _
                & " '" & param.ShisakuGousya & "' , " _
                & " '" & param.ShisakuGousyaGroup & "' , " _
                & " '" & param.CreatedUserId & "' , " _
                & " '" & param.CreatedDate & "' , " _
                & " '" & param.CreatedTime & "' , " _
                & " '" & param.UpdatedUserId & "' , " _
                & " '" & param.UpdatedDate & "' , " _
                & " '" & param.UpdatedTime & "'  " _
                & " ) "
                sqlList(index) = sql

            Next

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                insert.Open()
                Dim errorcount As Integer = 0
                For index As Integer = 0 To Vo.Count - 1
                    Try
                        '空なら何もしない'
                        If Not StringUtil.IsEmpty(sqlList(index)) Then
                            insert.ExecuteNonQuery(sqlList(index))
                        End If
                    Catch ex As SqlClient.SqlException
                        'プライマリキー違反のみ無視させたい'
                        Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                        If prm < 0 Then
                            Continue For
                        Else
                            Continue For
                        End If
                    End Try
                Next
                insert.Commit()
            End Using
        End Sub


    End Class
End Namespace