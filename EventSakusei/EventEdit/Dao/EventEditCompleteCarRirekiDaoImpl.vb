Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Dao

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventEditCompleteCarRirekiDaoImpl : Inherits DaoEachFeatureImpl
        Implements EventEditCompleteCarRirekiDao

        ''' <summary>
        ''' 試作イベント完成車情報の一覧を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作イベント完成車情報の一覧</returns>
        ''' <remarks></remarks>
        Public Function GetShisakuEventCompleteCarList(ByVal shisakuEventCode As String) As List(Of TShisakuEventKanseiVo) Implements EventEditCompleteCarRirekiDao.GetShisakuEventCompleteCarList
            Dim sql As String = _
                "SELECT " _
                & "    * " _
                & "FROM " _
                & "    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "    SHISAKU_EVENT_CODE    = @ShisakuEventCode " _
                & "ORDER BY " _
                & "    SHISAKU_EVENT_CODE ASC, " _
                & "    HYOJIJUN_NO ASC "
            Dim param As New TShisakuEventKanseiVo
            param.ShisakuEventCode = shisakuEventCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventKanseiVo)(sql, param)
        End Function

        ''' <summary>
        ''' 試作イベント完成車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <returns>試作イベント完成車情報</returns>
        ''' <remarks></remarks>
        Public Function FindShisakuEventCompleteCar(ByVal shisakuEventCode As String, ByVal hyojiJunNo As Integer) As TShisakuEventKanseiVo Implements EventEditCompleteCarRirekiDao.FindShisakuEventCompleteCar
            Dim sql As String = _
                "SELECT " _
                & "    * " _
                & "FROM " _
                & "    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "    SHISAKU_EVENT_CODE  = @ShisakuEventCode " _
                & "AND HYOJIJUN_NO = @HyojijunNo " _
                & "ORDER BY " _
                & "    SHISAKU_EVENT_CODE ASC, " _
                & "    HYOJIJUN_NO ASC "
            Dim param As New TShisakuEventKanseiVo
            param.ShisakuEventCode = shisakuEventCode
            param.HyojijunNo = hyojiJunNo
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventKanseiVo)(sql, param)
        End Function


        ''' <summary>
        ''' 試作イベント完成車情報を取得する
        ''' 登録時のＤＢより
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <returns>試作イベント完成車情報</returns>
        ''' <remarks></remarks>
        Public Function FindShisakuEventCompleteCarKaitei(ByVal shisakuEventCode As String, ByVal hyojiJunNo As Integer) As TShisakuEventKanseiKaiteiVo Implements EventEditCompleteCarRirekiDao.FindShisakuEventCompleteCarKaitei
            Dim sql As String = _
                "SELECT " _
                & "    * " _
                & "FROM " _
                & "    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI_KAITEI WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "    SHISAKU_EVENT_CODE  = @ShisakuEventCode " _
                & "AND HYOJIJUN_NO = @HyojijunNo " _
                & "ORDER BY " _
                & "    SHISAKU_EVENT_CODE ASC, " _
                & "    HYOJIJUN_NO ASC "
            Dim param As New TShisakuEventKanseiKaiteiVo
            param.ShisakuEventCode = shisakuEventCode
            param.HyojijunNo = hyojiJunNo
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventKanseiKaiteiVo)(sql, param)
        End Function

        ''' <summary>
        ''' 試作イベント完成車の履歴情報を作成する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <param name="columnId">列ID</param>
        ''' <param name="columnname">列名</param>
        ''' <param name="before">変更前</param>
        ''' <param name="after">変更後</param>
        ''' <remarks></remarks>
        Public Sub InsertShisakuEventCompleteCar(ByVal shisakuEventCode As String, _
                                                 ByVal hyojiJunNo As Integer, _
                                                 ByVal columnId As String, _
                                                 ByVal columnName As String, _
                                                 ByVal before As String, _
                                                 ByVal after As String) _
                                                 Implements EventEditCompleteCarRirekiDao.InsertShisakuEventCompleteCar
            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI_RIREKI (" _
            & " SHISAKU_EVENT_CODE, " _
            & " HYOJIJUN_NO, " _
            & " COLUMN_ID, " _
            & " COLUMN_NAME, " _
            & " UPDATE_BI, " _
            & " UPDATE_JIKAN, " _
            & " BEFORE, " _
            & " AFTER, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & " @ShisakuEventCode, " _
            & " @HyojijunNo, " _
            & " @ColumnId, " _
            & " @ColumnName, " _
            & " @UpdateBi, " _
            & " @UpdateJikan, " _
            & " @Before, " _
            & " @After, " _
            & " @CreatedUserId, " _
            & " @CreatedDate, " _
            & " @CreatedTime, " _
            & " @UpdatedUserId, " _
            & " @UpdatedDate, " _
            & " @UpdatedTime " _
            & " ) "

            '初期設定
            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuEventKanseiRirekiVo
            'パラメータセット
            param.ShisakuEventCode = shisakuEventCode
            param.HyojijunNo = hyojiJunNo
            param.ColumnId = columnId
            param.ColumnName = columnName
            param.UpdateBi = aDate.CurrentDateDbFormat
            param.UpdateJikan = aDate.CurrentTimeDbFormat
            param.Before = before
            param.After = after
            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Insert(sql, param)
        End Sub

        ''' <summary>
        ''' 試作イベント完成車の履歴情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <param name="columnId">列ID</param>
        ''' <remarks></remarks>
        Public Function GetShisakuEventCompleteCarRirekiList( _
                                             ByVal shisakuEventCode As String, _
                                             ByVal hyojiJunNo As Integer, _
                                             ByVal columnId As String) As List(Of TShisakuEventKanseiRirekiVo) _
                                             Implements EventEditCompleteCarRirekiDao.GetShisakuEventCompleteCarRirekiList

            Dim sql As String = _
                "SELECT " _
                & "    * " _
                & "FROM " _
                & "    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI_RIREKI WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "    SHISAKU_EVENT_CODE    = @ShisakuEventCode " _
                & "AND HYOJIJUN_NO           = @HyojijunNo " _
                & "AND COLUMN_ID             = @ColumnId " _
                & "ORDER BY " _
                & "    SHISAKU_EVENT_CODE ASC, " _
                & "    HYOJIJUN_NO ASC, " _
                & "    COLUMN_ID ASC, " _
                & "    UPDATE_BI DESC, " _
                & "    UPDATE_JIKAN DESC "
            Dim param As New TShisakuEventKanseiRirekiVo
            param.ShisakuEventCode = shisakuEventCode
            param.HyojijunNo = hyojiJunNo
            param.ColumnId = columnId
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventKanseiRirekiVo)(sql, param)

        End Function

        ''' <summary>
        ''' 試作イベントベース車情報より種別を取得する。
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <returns>試作イベントベース車情報</returns>
        ''' <remarks></remarks>
        Public Function FindShisakuEventBaseSeisakuIchiranCar(ByVal shisakuEventCode As String, ByVal hyojiJunNo As Integer) As TShisakuEventBaseSeisakuIchiranVo Implements EventEditCompleteCarRirekiDao.FindShisakuEventBaseseisakuichiranCar
            Dim sql As String = _
                "SELECT " _
                & "    * " _
                & "FROM " _
                & "    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_SEISAKU_ICHIRAN WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "    SHISAKU_EVENT_CODE  = @ShisakuEventCode " _
                & "AND HYOJIJUN_NO = @HyojijunNo " _
                & "ORDER BY " _
                & "    SHISAKU_EVENT_CODE ASC, " _
                & "    HYOJIJUN_NO ASC "
            Dim param As New TShisakuEventBaseSeisakuIchiranVo
            param.ShisakuEventCode = shisakuEventCode
            param.HyojijunNo = hyojiJunNo
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventBaseSeisakuIchiranVo)(sql, param)
        End Function

        ''' <summary>
        ''' 試作イベントベース車情報改訂より種別を取得する。
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <returns>試作イベントベース車情報</returns>
        ''' <remarks></remarks>
        Public Function FindShisakuEventBaseKaiteiCar(ByVal shisakuEventCode As String, ByVal hyojiJunNo As Integer) As TShisakuEventBaseKaiteiVo Implements EventEditCompleteCarRirekiDao.FindShisakuEventBaseKaiteiCar
            Dim sql As String = _
                "SELECT " _
                & "    * " _
                & "FROM " _
                & "    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_KAITEI WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "    SHISAKU_EVENT_CODE  = @ShisakuEventCode " _
                & "AND HYOJIJUN_NO = @HyojijunNo " _
                & "ORDER BY " _
                & "    SHISAKU_EVENT_CODE ASC, " _
                & "    HYOJIJUN_NO ASC "
            Dim param As New TShisakuEventBaseKaiteiVo
            param.ShisakuEventCode = shisakuEventCode
            param.HyojijunNo = hyojiJunNo
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventBaseKaiteiVo)(sql, param)
        End Function

    End Class
End Namespace