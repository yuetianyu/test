Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    ''' <summary>
    ''' 試作部品作成メニューExcel出力（ヘーダ部分）
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuEventDaoImpl : Inherits DaoEachFeatureImpl
        Implements IShisakuEventDao
        ''' <summary>
        ''' 試作部品作成メニューExcel出力（ヘーダ部分）
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns>試作部品作成メニューExcel出力（ヘーダ部分）</returns>
        ''' <remarks></remarks>
        Public Function GetShisakuBuhinMenuHead(ByVal eventCode As String) As ShisakuSekkeiBlockHeadVo Implements IShisakuEventDao.GetShisakuBuhinMenuHead
            Dim sql As String = _
                "SELECT  " _
                & "   KAITEI_SYOCHI_SHIMEKIRIBI,  " _
                & "   SHIMEKIRIBI,  " _
                & "   COALESCE(SOUSUU,0) AS SOUSUU,  " _
                & "   COALESCE(KANRYOU,0) AS KANRYOU, " _
                & "   STATUS  " _
                & "FROM   " _
                & "   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E WITH (NOLOCK, NOWAIT) " _
                & "LEFT JOIN   " _
                & "   (  " _
                & "       SELECT " _
                & "           SHISAKU_EVENT_CODE,  " _
                & "           COUNT(SHISAKU_EVENT_CODE) AS SOUSUU  " _
                & "       FROM " _
                & "           " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK1 WITH (NOLOCK, NOWAIT) " _
                & "       WHERE BLOCK_FUYOU=@BlockFuyou " _
                & "       AND NOT SHISAKU_BUKA_CODE = ''  " _
                & "         AND SHISAKU_BLOCK_NO_KAITEI_NO= " _
                & "         ( " _
                & "          SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
                & "          FROM  " _
                & "             " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) " _
                & "         WHERE " _
                & "             SHISAKU_EVENT_CODE=BLOCK1.SHISAKU_EVENT_CODE" _
                & "         AND SHISAKU_BUKA_CODE=BLOCK1.SHISAKU_BUKA_CODE" _
                & "         AND SHISAKU_BLOCK_NO=BLOCK1.SHISAKU_BLOCK_NO" _
                & "         ) " _
                & "       GROUP BY  " _
                & "           SHISAKU_EVENT_CODE " _
                & "   ) B1  " _
                & "ON   " _
                & "   B1.SHISAKU_EVENT_CODE=E.SHISAKU_EVENT_CODE " _
                & "LEFT JOIN   " _
                & "   (  " _
                & "       SELECT " _
                & "           SHISAKU_EVENT_CODE,  " _
                & "           COUNT(SHISAKU_EVENT_CODE) AS KANRYOU  " _
                & "       FROM " _
                & "           " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK2 WITH (NOLOCK, NOWAIT) " _
                & "       WHERE " _
                & "           JYOUTAI= @Jyoutai " _
                & "         AND SHISAKU_BLOCK_NO_KAITEI_NO= " _
                & "         ( " _
                & "          SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
                & "          FROM  " _
                & "             " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) " _
                & "         WHERE " _
                & "             SHISAKU_EVENT_CODE=BLOCK2.SHISAKU_EVENT_CODE" _
                & "         AND SHISAKU_BUKA_CODE=BLOCK2.SHISAKU_BUKA_CODE" _
                & "         AND SHISAKU_BLOCK_NO=BLOCK2.SHISAKU_BLOCK_NO" _
                & "         ) " _
                & "       AND BLOCK_FUYOU=@BlockFuyou " _
                & "       GROUP BY  " _
                & "           SHISAKU_EVENT_CODE " _
                & "   ) B2  " _
                & "ON   " _
                & "   B2.SHISAKU_EVENT_CODE=E.SHISAKU_EVENT_CODE " _
                & "WHERE " _
                & "   E.SHISAKU_EVENT_CODE=@ShisakuEventCode "
            
            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = eventCode
            param.BlockFuyou = ShishakuSekkeiBlockHitsuyou
            param.Jyoutai = ShishakuSekkeiBlockStatusShouchiKanryou


            Dim db As New EBomDbClient
            Return db.QueryForObject(Of ShisakuSekkeiBlockHeadVo)(sql, param)
        End Function

        ''' <summary>
        ''' 試作イベント情報よりアラート情報を取得する。
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns>試作イベント情報</returns>
        ''' <remarks></remarks>
        Public Function GetShisakuEvent(ByVal eventCode As String) As TShisakuEventVo Implements IShisakuEventDao.GetShisakuEvent
            Dim sql As String = _
                "SELECT  " _
                & "   *  " _
                & "FROM   " _
                & "   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "   E.SHISAKU_EVENT_CODE=@ShisakuEventCode "

            Dim param As New TShisakuEventVo
            param.ShisakuEventCode = eventCode

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventVo)(sql, param)
        End Function

        ''' <summary>
        ''' 試作イベント情報よりアラート情報を取得する。
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="blockAlertFlg">ブロックアラートフラグ</param>
        ''' <param name="blockAlertKind">ブロックアラート種類</param>
        ''' <remarks></remarks>
        Public Sub UpdAlertInfo(ByVal eventCode As String, ByVal blockAlertFlg As String, ByVal blockAlertKind As String) _
                                Implements IShisakuEventDao.UpdAlertInfo

            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT " _
            & " SET " _
            & " BLOCK_ALERT_FLG = @BlockAlertFlg, " _
            & " BLOCK_ALERT_KIND = @BlockAlertKind, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuEventVo

            param.ShisakuEventCode = eventCode
            param.BlockAlertFlg = blockAlertFlg
            param.BlockAlertKind = blockAlertKind
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)

        End Sub

        ''' <summary>
        ''' ブロックチェックアラー情報よりアラート情報を取得する。
        ''' </summary>
        ''' <param name="blockNo">ブロック№</param>
        ''' <param name="blockAlertFlg">ブロックアラート種類</param>
        ''' <returns>試作イベント情報</returns>
        ''' <remarks></remarks>
        Public Function GetBlockCheckAlertInfo(ByVal blockNo As String, _
                                        ByVal blockAlertFlg As String) As MBlockCheckInformationVo _
                                        Implements IShisakuEventDao.GetBlockCheckAlertInfo
            Dim sql As String = _
                "SELECT  " _
                & "   *  " _
                & "FROM   " _
                & "   " & MBOM_DB_NAME & ".dbo.M_BLOCK_CHECK_INFORMATION M WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "     M.SHISAKU_BLOCK_NO  = @ShisakuBlockNo " _
                & " AND M.BLOCK_ALERT_KIND  = @BlockAlertKind "

            Dim param As New MBlockCheckInformationVo
            param.ShisakuBlockNo = blockNo
            param.BlockAlertKind = blockAlertFlg

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of MBlockCheckInformationVo)(sql, param)
        End Function


        ''' <summary>
        ''' 試作イベント情報のお知らせ通知情報を更新する。
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="infoMailFlg">お知らせ通知フラグ</param>
        ''' <remarks></remarks>
        Public Sub UpdInfoMail(ByVal eventCode As String, ByVal infoMailFlg As String) _
                                Implements IShisakuEventDao.UpdInfoMail

            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT " _
            & " SET " _
            & " INFO_MAIL_FLG = @InfoMailFlg, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuEventVo

            param.ShisakuEventCode = eventCode
            param.InfoMailFlg = infoMailFlg
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)

        End Sub

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_d) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 試作イベント情報の作り方情報を更新する。
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="TsukurikataFlg">作り方フラグ</param>
        ''' <remarks></remarks>
        Public Sub UpdTsukurikataFlg(ByVal eventCode As String, ByVal TsukurikataFlg As String) _
                                Implements IShisakuEventDao.UpdTsukurikataFlg

            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT " _
            & " SET " _
            & " TSUKURIKATA_FLG = @TsukurikataFlg, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuEventVo

            param.ShisakuEventCode = eventCode
            param.TsukurikataFlg = TsukurikataFlg
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)

        End Sub
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_d) (TES)張 ADD END

        ''↓↓2014/07/24 Ⅰ.2.管理項目追加_bh) (TES)張 ADD BEGIN
        Public Sub UpdTsukurikataTenkaiFlg(ByVal eventCode As String, ByVal TsukurikataTenkaiFlg As String) _
                                Implements IShisakuEventDao.UpdTsukurikataTenkaiFlg

            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT " _
            & " SET " _
            & " TSUKURIKATA_TENKAI_FLG = @TsukurikataTenkaiFlg, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuEventVo

            param.ShisakuEventCode = eventCode
            param.TsukurikataTenkaiFlg = TsukurikataTenkaiFlg
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)

        End Sub
        ''↑↑2014/07/24 Ⅰ.2.管理項目追加_bh) (TES)張 ADD END
        ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 au) (TES)施 ADD BEGIN
        Public Sub UpdEbomKanshi(ByVal eventCode As String, ByVal EbomKanshiFlg As String) _
                        Implements IShisakuEventDao.UpdEbomKanshi

            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT " _
            & " SET " _
            & " EBOM_KANSHI_FLG = @EbomKanshiFlg, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuEventVo

            param.ShisakuEventCode = eventCode
            param.EbomKanshiFlg = EbomKanshiFlg
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)
        End Sub
        ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 au) (TES)施 ADDEND	


'/*** 20140911 CHANGE START ***/
        ''' <summary>
        ''' 試作イベント情報のステータスを更新する。
        ''' </summary>
        ''' <param name="param">試作イベント情報</param>
        ''' <remarks></remarks>
        Public Sub UpdStatus(ByVal param As TShisakuEventVo) _
                                Implements IShisakuEventDao.UpdStatus

            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT " _
            & " SET " _
            & " STATUS = @Status, " _
            & " SEKKEI_TENKAIBI = @SekkeiTenkaibi, " _
            & " KAITEI_SYOCHI_SHIMEKIRIBI = @KaiteiSyochiShimekiribi, " _
            & " TENKAI_STATUS = @TenkaiStatus, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            db.Update(sql, param)

        End Sub
'/*** 20140911 CHANGE END ***/

    End Class
End Namespace

