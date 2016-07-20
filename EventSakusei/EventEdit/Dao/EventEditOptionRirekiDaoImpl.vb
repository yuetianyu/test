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
    Public Class EventEditOptionRirekiDaoImpl : Inherits DaoEachFeatureImpl
        Implements EventEditOptionRirekiDao

        ''' <summary>
        ''' 試作イベント装備仕様情報の一覧を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作イベント装備仕様情報の一覧</returns>
        ''' <remarks></remarks>
        Public Function GetShisakuEventOptionList(ByVal shisakuEventCode As String) As List(Of TShisakuEventSoubiVo) Implements EventEditOptionRirekiDao.GetShisakuEventOptionList
            Dim sql As String = _
                "SELECT " _
                & "    * " _
                & "FROM " _
                & "    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "    SHISAKU_EVENT_CODE    = @ShisakuEventCode " _
                & "ORDER BY " _
                & "    SHISAKU_EVENT_CODE ASC, " _
                & "    HYOJIJUN_NO ASC, " _
                & "    SHISAKU_SOUBI_HYOUJI_NO ASC, " _
                & "    SHISAKU_SOUBI_KBN ASC "
            Dim param As New TShisakuEventSoubiVo
            param.ShisakuEventCode = shisakuEventCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventSoubiVo)(sql, param)
        End Function

        ''' <summary>
        ''' 試作イベント装備仕様情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <param name="shisakuSoubiHyoujiNo">試作装備表示順</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <returns>試作イベント装備仕様情報</returns>
        ''' <remarks></remarks>
        Public Function FindShisakuEventOption(ByVal shisakuEventCode As String, _
                                               ByVal hyojiJunNo As Integer, _
                                               ByVal shisakuSoubiHyoujiNo As Integer, _
                                               ByVal shisakuSoubiKbn As String) As TShisakuEventSoubiVo Implements EventEditOptionRirekiDao.FindShisakuEventOption
            Dim sql As String = _
                "SELECT " _
                & "    * " _
                & "FROM " _
                & "    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "    SHISAKU_EVENT_CODE  = @ShisakuEventCode " _
                & "AND HYOJIJUN_NO = @HyojijunNo " _
                & "AND SHISAKU_SOUBI_HYOUJI_NO = @ShisakuSoubiHyoujiNo " _
                & "AND SHISAKU_SOUBI_KBN = @ShisakuSoubiKbn " _
                & "ORDER BY " _
                & "    SHISAKU_EVENT_CODE ASC, " _
                & "    HYOJIJUN_NO ASC, " _
                & "    SHISAKU_SOUBI_HYOUJI_NO ASC, " _
                & "    SHISAKU_SOUBI_KBN ASC "
            Dim param As New TShisakuEventSoubiVo
            param.ShisakuEventCode = shisakuEventCode
            param.HyojijunNo = hyojiJunNo
            param.ShisakuSoubiHyoujiNo = shisakuSoubiHyoujiNo
            param.ShisakuSoubiKbn = shisakuSoubiKbn
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventSoubiVo)(sql, param)
        End Function


        ''' <summary>
        ''' 試作イベント装備仕様情報を取得する
        ''' 登録時ＤＢより
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <param name="shisakuSoubiHyoujiNo">試作装備表示順</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <returns>試作イベント装備仕様情報</returns>
        ''' <remarks></remarks>
        Public Function FindShisakuEventOptionKaitei(ByVal shisakuEventCode As String, _
                                               ByVal hyojiJunNo As Integer, _
                                               ByVal shisakuSoubiHyoujiNo As Integer, _
                                               ByVal shisakuSoubiKbn As String) As TShisakuEventSoubiKaiteiVo Implements EventEditOptionRirekiDao.FindShisakuEventOptionKaitei
            Dim sql As String = _
                "SELECT " _
                & "    * " _
                & "FROM " _
                & "    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI_KAITEI WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "    SHISAKU_EVENT_CODE  = @ShisakuEventCode " _
                & "AND HYOJIJUN_NO = @HyojijunNo " _
                & "AND SHISAKU_SOUBI_HYOUJI_NO = @ShisakuSoubiHyoujiNo " _
                & "AND SHISAKU_SOUBI_KBN = @ShisakuSoubiKbn " _
                & "ORDER BY " _
                & "    SHISAKU_EVENT_CODE ASC, " _
                & "    HYOJIJUN_NO ASC, " _
                & "    SHISAKU_SOUBI_HYOUJI_NO ASC, " _
                & "    SHISAKU_SOUBI_KBN ASC "
            Dim param As New TShisakuEventSoubiKaiteiVo
            param.ShisakuEventCode = shisakuEventCode
            param.HyojijunNo = hyojiJunNo
            param.ShisakuSoubiHyoujiNo = shisakuSoubiHyoujiNo
            param.ShisakuSoubiKbn = shisakuSoubiKbn
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventSoubiKaiteiVo)(sql, param)
        End Function

        ''' <summary>
        ''' 試作イベント完成車の履歴情報を作成する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <param name="shisakuSoubiHyoujiNo">試作装備表示順</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <param name="shisakuRetuKoumokuCodeBefore">試作列項目コード変更前</param>
        ''' <param name="shisakuRetuKoumokuCodeAfter">試作列項目コード変更後</param>
        ''' <param name="shisakuTekiyouBefore">試作適用変更前</param>
        ''' <param name="shisakuTekiyouAfter">試作適用変更後</param>
        ''' <remarks></remarks>
        Public Sub InsertShisakuEventOption(ByVal shisakuEventCode As String, _
                                             ByVal hyojiJunNo As Integer, _
                                             ByVal shisakuSoubiHyoujiNo As Integer, _
                                             ByVal shisakuSoubiKbn As String, _
                                             ByVal shisakuRetuKoumokuCodeBefore As String, _
                                             ByVal shisakuRetuKoumokuCodeAfter As String, _
                                             ByVal shisakuTekiyouBefore As String, _
                                             ByVal shisakuTekiyouAfter As String) Implements EventEditOptionRirekiDao.InsertShisakuEventOption
            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI_RIREKI (" _
            & " SHISAKU_EVENT_CODE, " _
            & " HYOJIJUN_NO, " _
            & " SHISAKU_SOUBI_HYOUJI_NO, " _
            & " SHISAKU_SOUBI_KBN, " _
            & " UPDATE_BI, " _
            & " UPDATE_JIKAN, " _
            & " SHISAKU_RETU_KOUMOKU_CODE_BEFORE, " _
            & " SHISAKU_RETU_KOUMOKU_CODE_AFTER, " _
            & " SHISAKU_TEKIYOU_BEFORE, " _
            & " SHISAKU_TEKIYOU_AFTER, " _
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
            & " @ShisakuSoubiHyoujiNo, " _
            & " @ShisakuSoubiKbn, " _
            & " @UpdateBi, " _
            & " @UpdateJikan, " _
            & " @ShisakuRetuKoumokuCodeBefore, " _
            & " @ShisakuRetuKoumokuCodeAfter, " _
            & " @ShisakuTekiyouBefore, " _
            & " @ShisakuTekiyouAfter, " _
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
            Dim param As New TShisakuEventSoubiRirekiVo
            'パラメータセット
            param.ShisakuEventCode = shisakuEventCode
            param.HyojijunNo = hyojiJunNo
            param.ShisakuSoubiHyoujiNo = shisakuSoubiHyoujiNo
            param.ShisakuSoubiKbn = shisakuSoubiKbn
            param.UpdateBi = aDate.CurrentDateDbFormat
            param.UpdateJikan = aDate.CurrentTimeDbFormat
            param.ShisakuRetuKoumokuCodeBefore = shisakuRetuKoumokuCodeBefore
            param.ShisakuRetuKoumokuCodeAfter = shisakuRetuKoumokuCodeAfter
            param.ShisakuTekiyouBefore = shisakuTekiyouBefore
            param.ShisakuTekiyouAfter = shisakuTekiyouAfter
            param.CreatedUserId = LoginInfo.Now.UserId
            param.CreatedDate = aDate.CurrentDateDbFormat
            param.CreatedTime = aDate.CurrentTimeDbFormat
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Insert(sql, param)
        End Sub

        ''' <summary>
        ''' 試作イベント装備仕様の履歴情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <param name="shisakuSoubiHyoujiNo">試作装備表示順</param>
        ''' <param name="shisakusoubikbn">試作装備区分</param>
        ''' <remarks></remarks>
        Public Function GetShisakuEventOptionRirekiList( _
                                             ByVal shisakuEventCode As String, _
                                             ByVal hyojiJunNo As Integer, _
                                             ByVal shisakuSoubiHyoujiNo As Integer, _
                                             ByVal shisakuSoubiKbn As String) As List(Of TShisakuEventSoubiRirekiVo) _
                                             Implements EventEditOptionRirekiDao.GetShisakuEventOptionRirekiList

            Dim sql As String = _
                "SELECT " _
                & "    * " _
                & "FROM " _
                & "    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_SOUBI_RIREKI WITH (NOLOCK, NOWAIT) " _
                & "WHERE " _
                & "    SHISAKU_EVENT_CODE      = @ShisakuEventCode " _
                & "AND HYOJIJUN_NO             = @HyojijunNo " _
                & "AND SHISAKU_SOUBI_HYOUJI_NO = @ShisakuSoubiHyoujiNo " _
                & "AND SHISAKU_SOUBI_KBN       = @ShisakuSoubiKbn " _
                & "ORDER BY " _
                & "    SHISAKU_EVENT_CODE ASC, " _
                & "    HYOJIJUN_NO ASC, " _
                & "    SHISAKU_SOUBI_HYOUJI_NO ASC, " _
                & "    SHISAKU_SOUBI_KBN ASC, " _
                & "    UPDATE_BI DESC, " _
                & "    UPDATE_JIKAN DESC "
            Dim param As New TShisakuEventSoubiRirekiVo
            param.ShisakuEventCode = shisakuEventCode
            param.HyojijunNo = hyojiJunNo
            param.ShisakuSoubiHyoujiNo = shisakuSoubiHyoujiNo
            param.ShisakuSoubiKbn = shisakuSoubiKbn
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventSoubiRirekiVo)(sql, param)

        End Function


    End Class
End Namespace