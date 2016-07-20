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

    Public Class ExclusiveControlBlockImpl : Inherits DaoEachFeatureImpl
        Implements ExclusiveControlBlockDao

        Public Function GetExclusiveControlBuka(ByVal ShisakuEventCode As String, _
                                                   ByVal ShisakuBukaCode As String) As TExclusiveControlBlockVo _
                                                   Implements ExclusiveControlBlockDao.FindByPkBuka
            Dim sql As String = _
                "SELECT " _
                & "   * " _
                & "   FROM " & MBOM_DB_NAME & ".dbo.T_EXCLUSIVE_CONTROL_BLOCK WITH (NOLOCK, NOWAIT) " _
                & "   WHERE" _
                & "   SHISAKU_EVENT_CODE =@ShisakuEventCode" _
                & "   AND SHISAKU_BUKA_CODE =@ShisakuBukaCode"

            Dim db As New EBomDbClient
            Dim param As New TExclusiveControlBlockVo
            param.ShisakuEventCode = ShisakuEventCode
            param.ShisakuBukaCode = ShisakuBukaCode

            Return db.QueryForObject(Of TExclusiveControlBlockVo)(sql, param)

        End Function
        Public Function GetExclusiveControlBlock(ByVal ShisakuEventCode As String, _
                                                   ByVal ShisakuBukaCode As String, _
                                                   ByVal ShisakuBlockNo As String) As TExclusiveControlBlockVo _
                                                   Implements ExclusiveControlBlockDao.FindByPk
            Dim sql As String = _
                "SELECT " _
                & "   * " _
                & "   FROM " & MBOM_DB_NAME & ".dbo.T_EXCLUSIVE_CONTROL_BLOCK WITH (NOLOCK, NOWAIT) " _
                & "   WHERE" _
                & "   SHISAKU_EVENT_CODE =@ShisakuEventCode" _
                & "   AND SHISAKU_BUKA_CODE =@ShisakuBukaCode" _
                & "   AND SHISAKU_BLOCK_NO =@ShisakuBlockNo"

            Dim db As New EBomDbClient
            Dim param As New TExclusiveControlBlockVo
            param.ShisakuEventCode = ShisakuEventCode
            param.ShisakuBukaCode = ShisakuBukaCode
            param.ShisakuBlockNo = ShisakuBlockNo

            Return db.QueryForObject(Of TExclusiveControlBlockVo)(sql, param)

        End Function

        '試作部品メニューのチェック用
        '   イベントコードで部品表が編集中か？
        Public Function GetExclusiveControlBlock(ByVal ShisakuEventCode As String) As List(Of TExclusiveControlBlockVo) _
                                                   Implements ExclusiveControlBlockDao.GetExclusiveControlBlock
            Dim sql As String = _
                "SELECT " _
                & "   BLOCK.*, R.KA_RYAKU_NAME " _
                & "   FROM " & MBOM_DB_NAME & ".dbo.T_EXCLUSIVE_CONTROL_BLOCK BLOCK WITH (NOLOCK, NOWAIT) " _
                & "     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 R " _
                & "     ON BLOCK.SHISAKU_BUKA_CODE=R.BU_CODE+R.KA_CODE " _
                & "   WHERE" _
                & "      SHISAKU_EVENT_CODE =@ShisakuEventCode" _
                & "   ORDER BY SHISAKU_BLOCK_NO"

            Dim db As New EBomDbClient
            Dim param As New TExclusiveControlBlockVo
            param.ShisakuEventCode = ShisakuEventCode

            Return db.QueryForList(Of TExclusiveControlBlockVo)(sql, param)

        End Function

        '試作部品メニューのチェック用
        '   内線番号を取得
        Public Function GetShisakuSekkeiBlockNaisen(ByVal ShisakuEventCode As String, _
                                                    ByVal ShisakuBukaCode As String, _
                                                    ByVal ShisakuBlockNo As String) As List(Of TShisakuSekkeiBlockVo) _
                                                   Implements ExclusiveControlBlockDao.GetShisakuSekkeiBlockNaisen
            Dim sql As String = _
                "SELECT " _
            & "   COALESCE(TEL_NO,'') AS TEL_NO " _
            & "FROM  " _
            & "   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK WITH (NOLOCK, NOWAIT) " _
            & "WHERE " _
            & "   SHISAKU_EVENT_CODE=@ShisakuEventCode " _
            & "   AND SHISAKU_BUKA_CODE=@ShisakuBukaCode " _
            & "   AND SHISAKU_BLOCK_NO =@ShisakuBlockNo " _
            & "   AND SHISAKU_BLOCK_NO_KAITEI_NO= " _
            & "   ( " _
            & "     SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & "     FROM  " _
            & "         " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) " _
            & "     WHERE " _
            & "         SHISAKU_EVENT_CODE=BLOCK.SHISAKU_EVENT_CODE" _
            & "     AND SHISAKU_BUKA_CODE=BLOCK.SHISAKU_BUKA_CODE" _
            & "     AND SHISAKU_BLOCK_NO=BLOCK.SHISAKU_BLOCK_NO" _
            & "   ) " _
            & " GROUP BY SHISAKU_BUKA_CODE, " _
            & "          SHISAKU_BLOCK_NO, " _
            & "          SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & "          BLOCK_FUYOU, " _
            & "          JYOUTAI, " _
            & "          UNIT_KBN, " _
            & "          SHISAKU_BLOCK_NAME, " _
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
            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = ShisakuEventCode
            param.ShisakuBukaCode = ShisakuBukaCode
            param.ShisakuBlockNo = ShisakuBlockNo

            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sql, param)

        End Function

        Public Sub InsetExclusiveControlBlock(ByVal newTExclusiveControlBlockVo As TExclusiveControlBlockVo) _
                                                Implements ExclusiveControlBlockDao.InsetByPk

            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_EXCLUSIVE_CONTROL_BLOCK ( " _
            & " SHISAKU_EVENT_CODE, " _
            & " SHISAKU_BUKA_CODE, " _
            & " SHISAKU_BLOCK_NO, " _
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
            & "'" & newTExclusiveControlBlockVo.ShisakuEventCode & "', " _
            & "'" & newTExclusiveControlBlockVo.ShisakuBukaCode & "', " _
            & "'" & newTExclusiveControlBlockVo.ShisakuBlockNo & "', " _
            & "'" & newTExclusiveControlBlockVo.EditUserId & "', " _
            & "'" & newTExclusiveControlBlockVo.EditDate & "', " _
            & "'" & newTExclusiveControlBlockVo.EditTime & "', " _
            & "'" & newTExclusiveControlBlockVo.CreatedUserId & "', " _
            & "'" & newTExclusiveControlBlockVo.CreatedDate & "', " _
            & "'" & newTExclusiveControlBlockVo.CreatedTime & "', " _
            & "'" & newTExclusiveControlBlockVo.UpdatedUserId & "', " _
            & "'" & newTExclusiveControlBlockVo.UpdatedDate & "', " _
            & "'" & newTExclusiveControlBlockVo.UpdatedTime & "' " _
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

        Public Sub UpdateExclusiveControlBlock(ByVal newTExclusiveControlBlockVo As TExclusiveControlBlockVo) _
                                                Implements ExclusiveControlBlockDao.UpdateByPk

            Dim sql As String = _
            "UPDATE " & MBOM_DB_NAME & ".dbo.T_EXCLUSIVE_CONTROL_BLOCK " _
            & " SET " _
            & " EDIT_USER_ID    = @EditUserId, " _
            & " EDIT_DATE       = @EditDate, " _
            & " EDIT_TIME       = @EditTime, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE    = @UpdatedDate, " _
            & " UPDATED_TIME    = @UpdatedTime " _
            & "     WHERE " _
            & "         SHISAKU_EVENT_CODE = @ShisakuEventCode" _
            & "     AND SHISAKU_BUKA_CODE  = @ShisakuBukaCode" _
            & "     AND SHISAKU_BLOCK_NO   = @ShisakuBlockNo"

            Dim param As New TExclusiveControlBlockVo
            'KEYをセット
            param.ShisakuEventCode = newTExclusiveControlBlockVo.ShisakuEventCode
            param.ShisakuBukaCode = newTExclusiveControlBlockVo.ShisakuBukaCode
            param.ShisakuBlockNo = newTExclusiveControlBlockVo.ShisakuBlockNo
            '編集者情報
            param.EditUserId = newTExclusiveControlBlockVo.EditUserId
            param.EditDate = newTExclusiveControlBlockVo.EditDate
            param.EditTime = newTExclusiveControlBlockVo.EditTime
            '更新情報
            param.UpdatedUserId = newTExclusiveControlBlockVo.UpdatedUserId
            param.UpdatedDate = newTExclusiveControlBlockVo.UpdatedDate
            param.UpdatedTime = newTExclusiveControlBlockVo.UpdatedTime

            Dim db As New EBomDbClient
            db.Update(sql, param)
        End Sub

        Public Sub DeleteExclusiveControlBlock(ByVal newTExclusiveControlBlockVo As TExclusiveControlBlockVo) _
                                                Implements ExclusiveControlBlockDao.DeleteByPk

            Dim sql As String = _
            " DELETE TECB " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_EXCLUSIVE_CONTROL_BLOCK TECB " _
            & " WHERE " _
            & "       SHISAKU_EVENT_CODE = @ShisakuEventCode" _
            & "   AND SHISAKU_BUKA_CODE  = @ShisakuBukaCode" _
            & "   AND SHISAKU_BLOCK_NO   = @ShisakuBlockNo" _
            & "   AND EDIT_USER_ID       = @EditUserId"

            Dim param As New TExclusiveControlBlockVo
            'KEYをセット
            param.ShisakuEventCode = newTExclusiveControlBlockVo.ShisakuEventCode
            param.ShisakuBukaCode = newTExclusiveControlBlockVo.ShisakuBukaCode
            param.ShisakuBlockNo = newTExclusiveControlBlockVo.ShisakuBlockNo
            param.EditUserId = newTExclusiveControlBlockVo.EditUserId 'ログインユーザーIDをセット

            Dim db As New EBomDbClient
            db.Delete(sql, param)

        End Sub


        '他部課に同ブロック№が存在するか？チェック用
        '
        Public Function GetOtherBukaBlock(ByVal ShisakuEventCode As String, _
                                          ByVal ShisakuBukaCode As String, _
                                          ByVal ShisakuBlockNo As String) As TShisakuSekkeiBlockVo _
                                          Implements ExclusiveControlBlockDao.GetOtherBukaBlock
            Dim sql As String = _
                "SELECT " _
            & "   COALESCE(SHISAKU_BUKA_CODE,'') AS SHISAKU_BUKA_CODE, " _
            & "   COALESCE(SHISAKU_BLOCK_NO,'') AS SHISAKU_BLOCK_NO, " _
            & "   COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'') AS SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & "   COALESCE(UNIT_KBN,'') AS UNIT_KBN, " _
            & "   COALESCE(SHISAKU_BLOCK_NAME,'') AS SHISAKU_BLOCK_NAME " _
            & "FROM  " _
            & "   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK WITH (NOLOCK, NOWAIT) " _
            & "WHERE " _
            & "   SHISAKU_EVENT_CODE    =  @ShisakuEventCode " _
            & "   AND SHISAKU_BUKA_CODE <> @ShisakuBukaCode " _
            & "   AND SHISAKU_BLOCK_NO  =  @ShisakuBlockNo " _
            & "   AND SHISAKU_BLOCK_NO_KAITEI_NO= " _
            & "   ( " _
            & "     SELECT MAX(CONVERT(INT,COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,''))) AS SHISAKU_BLOCK_NO_KAITEI_NO " _
            & "     FROM  " _
            & "         " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) " _
            & "     WHERE " _
            & "         SHISAKU_EVENT_CODE = BLOCK.SHISAKU_EVENT_CODE" _
            & "     AND SHISAKU_BUKA_CODE  = BLOCK.SHISAKU_BUKA_CODE" _
            & "     AND SHISAKU_BLOCK_NO   = BLOCK.SHISAKU_BLOCK_NO" _
            & "   ) " _
            & " GROUP BY SHISAKU_BUKA_CODE, " _
            & "          SHISAKU_BLOCK_NO, " _
            & "          SHISAKU_BLOCK_NO_KAITEI_NO, " _
            & "          BLOCK_FUYOU, " _
            & "          JYOUTAI, " _
            & "          UNIT_KBN, " _
            & "          SHISAKU_BLOCK_NAME, " _
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
            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = ShisakuEventCode
            param.ShisakuBukaCode = ShisakuBukaCode
            param.ShisakuBlockNo = ShisakuBlockNo

            Return db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql, param)

        End Function

    End Class
End Namespace