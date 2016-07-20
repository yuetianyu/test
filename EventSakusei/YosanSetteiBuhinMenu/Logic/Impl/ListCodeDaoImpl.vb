Imports ShisakuCommon
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.YosanSetteiBuhinMenu.Dao


Namespace YosanSetteiBuhinMenu.Logic.Impl

    Public Class ListCodeDaoImpl : Inherits DaoEachFeatureImpl
        Implements ListCodeDao

        Public Function GetListCode(ByVal eventCode As String) As String Implements ListCodeDao.GetListCode
            Dim sql As String = _
            "SELECT " _
            & " YOSAN_LIST_CODE, " _
            & " YOSAN_GROUP_NO, " _
            & " YOSAN_KOUJI_SHIREI_NO, " _
            & " YOSAN_EVENT_NAME, " _
            & " YOSAN_DAISU, " _
            & " YOSAN_JIKYUHIN, " _
            & " YOSAN_HIKAKUKEKKA, " _
            & " YOSAN_SYUUKEI_CODE, " _
            & " YOSAN_MEMO " _
            & "FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_LISTCODE AS MAIN WITH (NOLOCK, NOWAIT) " _
            & "WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & "ORDER BY YOSAN_LIST_HYOJIJUN_NO "

            Return sql

        End Function

        Public Sub UpdateListCode(ByVal ListCode As String, ByVal Memo As String) Implements ListCodeDao.UpdateListCode

            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_LISTCODE " _
            & " SET " _
            & " YOSAN_MEMO = @ShisakuMemo, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE YOSAN_LIST_CODE = @YosanListCode "

            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TYosanSetteiListcodeVo

            param.YosanListCode = ListCode
            param.YosanMemo = Memo
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)

        End Sub

#Region "削除する"

        ''' <summary>
        ''' リストコードの削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByListCode(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByListCode
            Dim sql As String = _
                " DELETE LE " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_LISTCODE LE " _
                & " WHERE " _
                & " LE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND LE.YOSAN_LIST_CODE = @YosanListCode "

            Dim db As New EBomDbClient
            Dim param As New TYosanSetteiListcodeVo
            param.ShisakuEventCode = shisakuEventCode
            param.YosanListCode = shisakuListCode

            db.Delete(sql, param)

        End Sub

        ''' <summary>
        ''' 手配基本情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByTehaiKihon
            Dim sql As String = _
                " DELETE KO " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN KO " _
                & " WHERE " _
                & " KO.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND KO.YOSAN_LIST_CODE = @YosanListCode "

            Dim db As New EBomDbClient
            Dim param As New TYOSANSETTEIBUHINVo
            param.ShisakuEventCode = shisakuEventCode
            param.YosanListCode = shisakuListCode

            db.Delete(sql, param)

        End Sub

        ''' <summary>
        ''' 手配号車情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTehaiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByTehaiGousya
            Dim sql As String = _
                " DELETE GO " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA GO " _
                & " WHERE " _
                & " GO.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND GO.YOSAN_LIST_CODE = @YosanListCode "

            Dim db As New EBomDbClient
            Dim param As New TYosanSetteiGousyaVo
            param.ShisakuEventCode = shisakuEventCode
            param.YosanListCode = shisakuListCode

            db.Delete(sql, param)

        End Sub

        ''' <summary>
        ''' 基本TMPの削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByKihonTmp(ByVal shisakuEventCode As String) Implements ListCodeDao.DeleteByKihonTmp
            Dim sql As String = _
                " DELETE TK " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_TMP TK " _
                & " WHERE " _
                & " TK.SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TYosanSetteiBuhinTmpVo
            param.ShisakuEventCode = shisakuEventCode

            db.Delete(sql, param)

        End Sub

        ''' <summary>
        ''' 号車TMPの削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByGousyaTmp(ByVal shisakuEventCode As String) Implements ListCodeDao.DeleteByGousyaTmp
            Dim sql As String = _
                " DELETE TG " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_GOUSYA_TMP TG " _
                & " WHERE " _
                & " TG.SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TYosanSetteiGousyaTmpVo
            param.ShisakuEventCode = shisakuEventCode

            db.Delete(sql, param)

        End Sub

        ''' <summary>
        ''' 予算設定部品表変更履歴情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByYosanSetteiBuhinRireki(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByYosanSetteiBuhinRireki
            Dim sql As String = _
                " DELETE YSBR " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN_RIREKI YSBR " _
                & " WHERE " _
                & " YSBR.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND YSBR.YOSAN_LIST_CODE = @YosanListCode "

            Dim db As New EBomDbClient
            Dim param As New TYosanSetteiBuhinRirekiVo
            param.ShisakuEventCode = shisakuEventCode
            param.YosanListCode = shisakuListCode

            db.Delete(sql, param)

        End Sub

#End Region

    End Class

End Namespace