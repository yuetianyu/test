Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.EventEdit.Dao
Imports EventSakusei.ShisakuBuhinEditBlock
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EBom.Data
Imports EBom.Common

Namespace ShisakuBuhinEditBlock.Dao

    Public Class ExclusiveControlTantoImpl : Inherits DaoEachFeatureImpl
        Implements ExclusiveControlTantoDao

        Public Function GetExclusiveControlBuka(ByVal HoyouEventCode As String, _
                                                   ByVal HoyouBukaCode As String) As THoyouExclusiveControlTantoVo _
                                                   Implements ExclusiveControlTantoDao.FindByPkBuka
            Dim sql As String = _
                "SELECT " _
                & "   * " _
                & "   FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_EXCLUSIVE_CONTROL_TANTO WITH (NOLOCK, NOWAIT) " _
                & "   WHERE" _
                & "   HOYOU_EVENT_CODE =@HoyouEventCode" _
                & "   AND HOYOU_BUKA_CODE =@HoyouBukaCode"

            Dim db As New EBomDbClient
            Dim param As New THoyouExclusiveControlTantoVo
            param.HoyouEventCode = HoyouEventCode
            param.HoyouBukaCode = HoyouBukaCode

            Return db.QueryForObject(Of THoyouExclusiveControlTantoVo)(sql, param)

        End Function
        Public Function GetExclusiveControlTanto(ByVal HoyouEventCode As String, _
                                                   ByVal HoyouBukaCode As String, _
                                                   ByVal HoyouTanto As String) As THoyouExclusiveControlTantoVo _
                                                   Implements ExclusiveControlTantoDao.FindByPk
            Dim sql As String = _
                "SELECT " _
                & "   * " _
                & "   FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_EXCLUSIVE_CONTROL_TANTO WITH (NOLOCK, NOWAIT) " _
                & "   WHERE" _
                & "   HOYOU_EVENT_CODE =@HoyouEventCode" _
                & "   AND HOYOU_BUKA_CODE =@HoyouBukaCode" _
                & "   AND HOYOU_TANTO =@HoyouTanto"

            Dim db As New EBomDbClient
            Dim param As New THoyouExclusiveControlTantoVo
            param.HoyouEventCode = HoyouEventCode
            param.HoyouBukaCode = HoyouBukaCode
            param.HoyouTanto = HoyouTanto

            Return db.QueryForObject(Of THoyouExclusiveControlTantoVo)(sql, param)

        End Function

        '補用部品メニューのチェック用
        '   イベントコードで部品表が編集中か？
        Public Function GetExclusiveControlTanto(ByVal HoyouEventCode As String) As List(Of THoyouExclusiveControlTantoVo) _
                                                   Implements ExclusiveControlTantoDao.GetExclusiveControlTanto
            Dim sql As String = _
                "SELECT " _
                & "   T.*, R.KA_RYAKU_NAME " _
                & "   FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_EXCLUSIVE_CONTROL_TANTO T WITH (NOLOCK, NOWAIT) " _
                & "     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 R " _
                & "     ON T.HOYOU_BUKA_CODE=R.BU_CODE+R.KA_CODE " _
                & "   WHERE" _
                & "      HOYOU_EVENT_CODE =@HoyouEventCode" _
                & "   ORDER BY HOYOU_TANTO"

            Dim db As New EBomDbClient
            Dim param As New THoyouExclusiveControlTantoVo
            param.HoyouEventCode = HoyouEventCode

            Return db.QueryForList(Of THoyouExclusiveControlTantoVo)(sql, param)

        End Function

        '補用部品メニューのチェック用
        '   内線番号を取得
        Public Function GetHoyouSekkeiTantoNaisen(ByVal HoyouEventCode As String, _
                                                    ByVal HoyouBukaCode As String, _
                                                    ByVal HoyouTanto As String) As List(Of THoyouSekkeiTantoVo) _
                                                   Implements ExclusiveControlTantoDao.GetHoyouSekkeiTantoNaisen
            Dim sql As String = _
                "SELECT " _
            & "   COALESCE(TEL_NO,'') AS TEL_NO " _
            & "FROM  " _
            & "   " & MBOM_DB_NAME & ".dbo.T_HOYOU_SEKKEI_TANTO T WITH (NOLOCK, NOWAIT) " _
            & "WHERE " _
            & "   HOYOU_EVENT_CODE=@HoyouEventCode " _
            & "   AND HOYOU_BUKA_CODE=@HoyouBukaCode " _
            & "   AND HOYOU_TANTO =@HoyouTanto " _
            & "   AND HOYOU_TANTO_KAITEI_NO= " _
            & "   ( " _
            & "     SELECT MAX(CONVERT(INT,COALESCE(HOYOU_TANTO_KAITEI_NO,''))) AS HOYOU_TANTO_KAITEI_NO " _
            & "     FROM  " _
            & "         " & MBOM_DB_NAME & ".dbo.T_HOYOU_SEKKEI_TANTO WITH (NOLOCK, NOWAIT) " _
            & "     WHERE " _
            & "         HOYOU_EVENT_CODE=T.HOYOU_EVENT_CODE" _
            & "     AND HOYOU_BUKA_CODE=T.HOYOU_BUKA_CODE" _
            & "     AND HOYOU_TANTO=T.HOYOU_TANTO" _
            & "   ) " _
            & " GROUP BY HOYOU_BUKA_CODE, " _
            & "          HOYOU_TANTO, " _
            & "          HOYOU_TANTO_KAITEI_NO, " _
            & "          TANTO_FUYOU, " _
            & "          JYOUTAI, " _
            & "          TANTO_MEMO, " _
            & "          TEL_NO, " _
            & "          KACHOU_SYOUNIN_HI, " _
            & "          SAISYU_KOUSHINBI, " _
            & "          SAISYU_KOUSHINJIKAN, " _
            & "          TANTO_SYOUNIN_JYOUTAI, " _
            & "          TANTO_SYOUNIN_KA, " _
            & "          TANTO_SYOUNIN_HI, " _
            & "          TANTO_SYOUNIN_JIKAN, " _
            & "          KACHOU_SYOUNIN_JYOUTAI, " _
            & "          KACHOU_SYOUNIN_KA, " _
            & "          KACHOU_SYOUNIN_JIKAN "

            Dim db As New EBomDbClient
            Dim param As New THoyouSekkeiTantoVo
            param.HoyouEventCode = HoyouEventCode
            param.HoyouBukaCode = HoyouBukaCode
            param.HoyouTanto = HoyouTanto

            Return db.QueryForList(Of THoyouSekkeiTantoVo)(sql, param)

        End Function

        Public Sub InsetExclusiveControlTanto(ByVal newTExclusiveControlTantoVo As THoyouExclusiveControlTantoVo) _
                                                Implements ExclusiveControlTantoDao.InsetByPk

            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_HOYOU_EXCLUSIVE_CONTROL_TANTO ( " _
            & " HOYOU_EVENT_CODE, " _
            & " HOYOU_BUKA_CODE, " _
            & " HOYOU_TANTO, " _
            & " EDIT_USER_ID, " _
            & " EDIT_DATE, " _
            & " EDIT_TIME, " _
            & " CREATED_USER_ID, " _
            & " CREATED_DATE, " _
            & " CREATED_TIME, " _
            & " UPDATED_USER_ID, " _
            & " UPDATED_DATE, " _
            & " UPDATED_TIME " _
            & " ) " _
            & " VALUES ( " _
            & "'" & newTExclusiveControlTantoVo.HoyouEventCode & "', " _
            & "'" & newTExclusiveControlTantoVo.HoyouBukaCode & "', " _
            & "'" & newTExclusiveControlTantoVo.HoyouTanto & "', " _
            & "'" & newTExclusiveControlTantoVo.EditUserId & "', " _
            & "'" & newTExclusiveControlTantoVo.EditDate & "', " _
            & "'" & newTExclusiveControlTantoVo.EditTime & "', " _
            & "'" & newTExclusiveControlTantoVo.CreatedUserId & "', " _
            & "'" & newTExclusiveControlTantoVo.CreatedDate & "', " _
            & "'" & newTExclusiveControlTantoVo.CreatedTime & "', " _
            & "'" & newTExclusiveControlTantoVo.UpdatedUserId & "', " _
            & "'" & newTExclusiveControlTantoVo.UpdatedDate & "', " _
            & "'" & newTExclusiveControlTantoVo.UpdatedTime & "' " _
            & " ) "

            Using insert As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                insert.Open()
                insert.BeginTransaction()
                Dim errorcount As Integer = 0
                'insert.ExecuteNonQuery(sqlList(index))
                Try
                    '空なら何もしない'
                    insert.ExecuteNonQuery(sql)
                Catch ex As SqlClient.SqlException
                    'プライマリキー違反のみ無視させたい'
                    Dim prm As Integer = ex.Message.IndexOf("PRIMARY")
                    If prm < 0 Then
                        MsgBox(ex.Message)
                    End If
                End Try
                insert.Commit()
            End Using

        End Sub

        Public Sub UpdateExclusiveControlTanto(ByVal newTExclusiveControlTantoVo As THoyouExclusiveControlTantoVo) _
                                                Implements ExclusiveControlTantoDao.UpdateByPk

            Dim sql As String = _
            "UPDATE " & MBOM_DB_NAME & ".dbo.T_HOYOU_EXCLUSIVE_CONTROL_TANTO " _
            & " SET " _
            & " EDIT_USER_ID    = @EditUserId, " _
            & " EDIT_DATE       = @EditDate, " _
            & " EDIT_TIME       = @EditTime, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE    = @UpdatedDate, " _
            & " UPDATED_TIME    = @UpdatedTime " _
            & "     WHERE " _
            & "         HOYOU_EVENT_CODE = @HoyouEventCode" _
            & "     AND HOYOU_BUKA_CODE  = @HoyouBukaCode" _
            & "     AND HOYOU_TANTO   = @HoyouTanto"

            Dim param As New THoyouExclusiveControlTantoVo
            'KEYをセット
            param.HoyouEventCode = newTExclusiveControlTantoVo.HoyouEventCode
            param.HoyouBukaCode = newTExclusiveControlTantoVo.HoyouBukaCode
            param.HoyouTanto = newTExclusiveControlTantoVo.HoyouTanto
            '編集者情報
            param.EditUserId = newTExclusiveControlTantoVo.EditUserId
            param.EditDate = newTExclusiveControlTantoVo.EditDate
            param.EditTime = newTExclusiveControlTantoVo.EditTime
            '更新情報
            param.UpdatedUserId = newTExclusiveControlTantoVo.UpdatedUserId
            param.UpdatedDate = newTExclusiveControlTantoVo.UpdatedDate
            param.UpdatedTime = newTExclusiveControlTantoVo.UpdatedTime

            Dim db As New EBomDbClient
            db.Update(sql, param)
        End Sub

        Public Sub DeleteExclusiveControlTanto(ByVal newTExclusiveControlTantoVo As THoyouExclusiveControlTantoVo) _
                                                Implements ExclusiveControlTantoDao.DeleteByPk

            Dim sql As String = _
            " DELETE TECB " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_HOYOU_EXCLUSIVE_CONTROL_TANTO TECB " _
            & " WHERE " _
            & "       HOYOU_EVENT_CODE = @HoyouEventCode" _
            & "   AND HOYOU_BUKA_CODE  = @HoyouBukaCode" _
            & "   AND HOYOU_TANTO   = @HoyouTanto" _
            & "   AND EDIT_USER_ID       = @EditUserId"

            Dim param As New THoyouExclusiveControlTantoVo
            'KEYをセット
            param.HoyouEventCode = newTExclusiveControlTantoVo.HoyouEventCode
            param.HoyouBukaCode = newTExclusiveControlTantoVo.HoyouBukaCode
            param.HoyouTanto = newTExclusiveControlTantoVo.HoyouTanto
            param.EditUserId = newTExclusiveControlTantoVo.EditUserId 'ログインユーザーIDをセット

            Dim db As New EBomDbClient
            db.Delete(sql, param)

        End Sub

    End Class
End Namespace