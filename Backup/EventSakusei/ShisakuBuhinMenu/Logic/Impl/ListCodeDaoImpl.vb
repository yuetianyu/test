Imports ShisakuCommon
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.ShisakuBuhinMenu.Dao


Namespace ShisakuBuhinMenu.Logic.Impl

    Public Class ListCodeDaoImpl : Inherits DaoEachFeatureImpl
        Implements ListCodeDao

        Public Function GetListCode(ByVal eventCode As String) As String Implements ListCodeDao.GetListCode
            Dim sql As String = _
            "SELECT " _
            & " RIREKI, " _
            & " SHISAKU_LIST_CODE, " _
            & " SHISAKU_GROUP_NO, " _
            & " SHISAKU_KOUJI_SHIREI_NO, " _
            & " SHISAKU_EVENT_NAME, " _
            & " SHISAKU_DAISU, " _
            & " SHISAKU_LIST_CODE_KAITEI_NO, " _
            & " SHISAKU_JIKYUHIN, " _
            & " SHISAKU_HIKAKUKEKKA, " _
            & " SHISAKU_SYUUKEI_CODE, " _
            & " SHISAKU_MEMO " _
            & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE AS MAIN WITH (NOLOCK, NOWAIT) " _
            & "WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & "AND SHISAKU_LIST_CODE_KAITEI_NO=(select max(SHISAKU_LIST_CODE_KAITEI_NO) " _
            & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE WITH (NOLOCK, NOWAIT) " _
            & "WHERE SHISAKU_LIST_CODE=MAIN.SHISAKU_LIST_CODE) " _
            & "ORDER BY SHISAKU_LIST_HYOJIJUN_NO "

            Return sql

        End Function

        Public Sub UpdateListCode(ByVal ListCode As String, ByVal Memo As String) Implements ListCodeDao.UpdateListCode

            Dim sql As String = _
            " UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE " _
            & " SET " _
            & " SHISAKU_MEMO = @ShisakuMemo, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE = @UpdatedDate, " _
            & " UPDATED_TIME = @UpdatedTime " _
            & " WHERE SHISAKU_LIST_CODE = @ShisakuListCode "
            'これだと最新だけしかとらない'
            '& " AND SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            '& " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            '& " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE  " _
            '& " WHERE SHISAKU_LIST_CODE = SHISAKU_LIST_CODE )"


            Dim db As New EBomDbClient
            Dim aDate As New ShisakuDate
            Dim param As New TShisakuListcodeVo

            param.ShisakuListCode = ListCode
            param.ShisakuMemo = Memo
            param.UpdatedUserId = LoginInfo.Now.UserId
            param.UpdatedDate = aDate.CurrentDateDbFormat
            param.UpdatedTime = aDate.CurrentTimeDbFormat

            db.Update(sql, param)

        End Sub

        ''' <summary>
        '''　発注実績の取得
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="listCode">リストコード</param>
        ''' <returns>発注実績</returns>
        ''' <remarks></remarks>
        Public Function FindByHacchuJisseki(ByVal eventCode As String, ByVal listCode As String) As String Implements ListCodeDao.FindByHacchuJisseki
            Dim sql As String = _
            "SELECT " _
            & " RIREKI " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE LE WITH (NOLOCK, NOWAIT) " _
            & " WHERE LE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND LE.SHISAKU_LIST_CODE = @ShisakuListCode " _
            & " AND LE.SHISAKU_LIST_CODE_KAITEI_NO = ( " _
            & " SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE WITH (NOLOCK, NOWAIT) " _
            & " WHERE SHISAKU_EVENT_CODE = LE.SHISAKU_EVENT_CODE " _
            & " AND SHISAKU_LIST_CODE = LE.SHISAKU_LIST_CODE )"

            Dim db As New EBomDbClient
            Dim param As New TShisakuListcodeVo

            param.ShisakuEventCode = eventCode
            param.ShisakuListCode = listCode

            Dim vo As New TShisakuListcodeVo
            vo = db.QueryForObject(Of TShisakuListcodeVo)(sql, param)

            If vo Is Nothing Then
                Return Nothing
            Else
                Return vo.Rireki
            End If



        End Function

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
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE LE " _
                & " WHERE " _
                & " LE.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND LE.SHISAKU_LIST_CODE = @ShisakuListCode "


            Dim db As New EBomDbClient
            Dim param As New TShisakuListcodeVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

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
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON KO " _
                & " WHERE " _
                & " KO.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND KO.SHISAKU_LIST_CODE = @ShisakuListCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKihonVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

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
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA GO " _
                & " WHERE " _
                & " GO.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND GO.SHISAKU_LIST_CODE = @ShisakuListCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiGousyaVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            db.Delete(sql, param)

        End Sub

        ''' <summary>
        ''' 手配エラー情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTehaiError(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByTehaiError
            Dim sql As String = _
                " DELETE ER " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_ERROR ER " _
                & " WHERE " _
                & " ER.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND ER.SHISAKU_LIST_CODE = @ShisakuListCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiErrorVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            db.Delete(sql, param)

        End Sub

        ''' <summary>
        ''' 手配改訂ブロック情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTehaiKaiteiBlock(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByTehaiKaiteiBlock
            Dim sql As String = _
                " DELETE KB " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KAITEI_BLOCK KB " _
                & " WHERE " _
                & " KB.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND KB.SHISAKU_LIST_CODE = @ShisakuListCode "


            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKaiteiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            db.Delete(sql, param)

        End Sub
        '↓↓2014/10/02 酒井 ADD BEGIN
        Public Sub DeleteByTehaiKaiteiBlock2(ByVal shisakuEventCode As String) Implements ListCodeDao.DeleteByTehaiKaiteiBlock2
            Dim sql As String = _
                " DELETE KB " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KAITEI_BLOCK KB " _
                & " WHERE " _
                & " KB.SHISAKU_EVENT_CODE = @ShisakuEventCode "


            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiKaiteiBlockVo
            param.ShisakuEventCode = shisakuEventCode

            db.Delete(sql, param)

        End Sub
        '↑↑2014/10/02 酒井 ADD END
        ''' <summary>
        ''' 手配訂正基本の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTeiseiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByTeiseiKihon
            Dim sql As String = _
                " DELETE TK " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_TEISEI_KIHON TK " _
                & " WHERE " _
                & " TK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND TK.SHISAKU_LIST_CODE = @ShisakuListCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiTeiseiKihonVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            db.Delete(sql, param)

        End Sub

        ''' <summary>
        ''' 手配訂正号車の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTehaiTeiseiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByTehaiTeiseiGousya
            Dim sql As String = _
                " DELETE TG " _
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE TG " _
                & " WHERE " _
                & " TG.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                & " AND TG.SHISAKU_LIST_CODE = @ShisakuListCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiTeiseiGousyaVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

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
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_TMP TK " _
                & " WHERE " _
                & " TK.SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditTmpVo
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
                & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP TG " _
                & " WHERE " _
                & " TG.SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditGousyaTmpVo
            param.ShisakuEventCode = shisakuEventCode

            db.Delete(sql, param)

        End Sub

        ''' <summary>
        ''' 部品編集改訂情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByBuhinEditKaitei(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByBuhinEditKaitei
            Dim sql As String = _
            " DELETE BEK " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_KAITEI BEK " _
            & " WHERE " _
            & " BEK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND BEK.SHISAKU_LIST_CODE = @ShisakuListCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditKaiteiVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            db.Delete(sql, param)
        End Sub

        ''' <summary>
        ''' 部品編集号車改訂情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByBuhinEditGousyaKaitei(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByBuhinEditGousyaKaitei
            Dim sql As String = _
            " DELETE GK " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_GOUSYA_KAITEI GK " _
            & " WHERE " _
            & " GK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND GK.SHISAKU_LIST_CODE = @ShisakuListCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuBuhinEditGousyaKaiteiVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            db.Delete(sql, param)
        End Sub


        ''' <summary>
        ''' 試作手配出図実績情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTehaiShutuzuJiseki(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByTehaiShutuzuJiseki
            Dim sql As String = _
            " DELETE GK " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI GK " _
            & " WHERE " _
            & " GK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND GK.SHISAKU_LIST_CODE = @ShisakuListCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiShutuzuJisekiVo()
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            db.Delete(sql, param)
        End Sub
        ''' <summary>
        ''' 試作手配出図実績手入力情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTehaiShutuzuJisekiInput(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByTehaiShutuzuJisekiInput
            Dim sql As String = _
            " DELETE GK " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_JISEKI_INPUT GK " _
            & " WHERE " _
            & " GK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND GK.SHISAKU_LIST_CODE = @ShisakuListCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiShutuzuJisekiInputVo()
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            db.Delete(sql, param)
        End Sub
        ''' <summary>
        ''' 試作手配出図織込情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTehaiShutuzuOrikomi(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByTehaiShutuzuOrikomi
            Dim sql As String = _
            " DELETE GK " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_SHUTUZU_ORIKOMI GK " _
            & " WHERE " _
            & " GK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND GK.SHISAKU_LIST_CODE = @ShisakuListCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiShutuzuOrikomiVo()
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            db.Delete(sql, param)
        End Sub
        ''' <summary>
        ''' 試作手配帳情報（号車グループ情報）の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub DeleteByTehaiGousyaGroup(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) Implements ListCodeDao.DeleteByTehaiGousyaGroup
            Dim sql As String = _
            " DELETE GK " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA_GROUP GK " _
            & " WHERE " _
            & " GK.SHISAKU_EVENT_CODE = @ShisakuEventCode " _
            & " AND GK.SHISAKU_LIST_CODE = @ShisakuListCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuTehaiGousyaGroupVo()
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuListCode = shisakuListCode

            db.Delete(sql, param)
        End Sub

#End Region


    End Class

End Namespace