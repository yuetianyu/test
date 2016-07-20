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

Namespace ShisakuBuhinEditEvent.Dao

    Public Class ExclusiveControlEventImpl : Inherits DaoEachFeatureImpl
        Implements ExclusiveControlEventDao

        Public Function GetExclusiveControlEvent(ByVal ShisakuEventCode As String, ByVal editMode As String) As TExclusiveControlEventVo _
                                                   Implements ExclusiveControlEventDao.FindByPk
            Dim sql As String = _
                "SELECT " _
                & "   EDIT_USER_ID " _
                & "   FROM " & MBOM_DB_NAME & ".dbo.T_EXCLUSIVE_CONTROL_EVENT WITH (NOLOCK, NOWAIT) " _
                & "   WHERE" _
                & "   SHISAKU_EVENT_CODE =@ShisakuEventCode AND " _
                & "   EDIT_MODE =@EditMode"

            Dim db As New EBomDbClient
            Dim param As New TExclusiveControlEventVo
            param.ShisakuEventCode = ShisakuEventCode
            param.EditMode = editMode

            Return db.QueryForObject(Of TExclusiveControlEventVo)(sql, param)

        End Function

        Public Sub InsetExclusiveControlEvent(ByVal newTExclusiveControlEventVo As TExclusiveControlEventVo) _
                                                Implements ExclusiveControlEventDao.InsetByPk

            Dim sql As String = _
            " INSERT INTO " & MBOM_DB_NAME & ".dbo.T_EXCLUSIVE_CONTROL_EVENT ( " _
            & " SHISAKU_EVENT_CODE, " _
            & " EDIT_MODE, " _
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
            & "'" & newTExclusiveControlEventVo.ShisakuEventCode & "', " _
            & "'" & newTExclusiveControlEventVo.EditMode & "', " _
            & "'" & newTExclusiveControlEventVo.EditUserId & "', " _
            & "'" & newTExclusiveControlEventVo.EditDate & "', " _
            & "'" & newTExclusiveControlEventVo.EditTime & "', " _
            & "'" & newTExclusiveControlEventVo.CreatedUserId & "', " _
            & "'" & newTExclusiveControlEventVo.CreatedDate & "', " _
            & "'" & newTExclusiveControlEventVo.CreatedTime & "', " _
            & "'" & newTExclusiveControlEventVo.UpdatedUserId & "', " _
            & "'" & newTExclusiveControlEventVo.UpdatedDate & "', " _
            & "'" & newTExclusiveControlEventVo.UpdatedTime & "' " _
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

        Public Sub UpdateExclusiveControlEvent(ByVal newTExclusiveControlEventVo As TExclusiveControlEventVo) _
                                                Implements ExclusiveControlEventDao.UpdateByPk

            Dim sql As String = _
            "UPDATE " & MBOM_DB_NAME & ".dbo.T_EXCLUSIVE_CONTROL_EVENT " _
            & " SET " _
            & " EDIT_USER_ID    = @EditUserId, " _
            & " EDIT_DATE       = @EditDate, " _
            & " EDIT_TIME       = @EditTime, " _
            & " UPDATED_USER_ID = @UpdatedUserId, " _
            & " UPDATED_DATE    = @UpdatedDate, " _
            & " UPDATED_TIME    = @UpdatedTime " _
            & "     WHERE " _
            & "         SHISAKU_EVENT_CODE = @ShisakuEventCode" _
            & "     AND EDIT_MODE = @EditMode"

            Dim param As New TExclusiveControlEventVo
            'KEYをセット
            param.ShisakuEventCode = newTExclusiveControlEventVo.ShisakuEventCode   'イベントコード
            param.EditMode = newTExclusiveControlEventVo.EditMode                   '編集モード
            '編集者情報
            param.EditUserId = newTExclusiveControlEventVo.EditUserId
            param.EditDate = newTExclusiveControlEventVo.EditDate
            param.EditTime = newTExclusiveControlEventVo.EditTime
            '更新情報
            param.UpdatedUserId = newTExclusiveControlEventVo.UpdatedUserId
            param.UpdatedDate = newTExclusiveControlEventVo.UpdatedDate
            param.UpdatedTime = newTExclusiveControlEventVo.UpdatedTime

            Dim db As New EBomDbClient
            db.Update(sql, param)
        End Sub

        Public Sub DeleteExclusiveControlEvent(ByVal newTExclusiveControlEventVo As TExclusiveControlEventVo) _
                                                Implements ExclusiveControlEventDao.DeleteByPk

            Dim sql As String = _
            " DELETE TECB " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_EXCLUSIVE_CONTROL_EVENT TECB " _
            & " WHERE " _
            & "         SHISAKU_EVENT_CODE = @ShisakuEventCode" _
            & "     AND EDIT_MODE = @EditMode"

            Dim param As New TExclusiveControlEventVo
            'KEYをセット
            param.ShisakuEventCode = newTExclusiveControlEventVo.ShisakuEventCode   'イベントコード
            param.EditMode = newTExclusiveControlEventVo.EditMode                   '編集モード

            Dim db As New EBomDbClient
            db.Delete(sql, param)

        End Sub

    End Class
End Namespace