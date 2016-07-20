Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon

Namespace EventEdit.Dao
    Public Class EventEditCopyDaoImpl
        Implements EventEditCopyDao

        Public Function FindByAll() As List(Of EventEditCopyResultVo) Implements EventEditCopyDao.FindByAll
            Dim sql As String = "SELECT E.*, S.SHISAKU_STATUS_NAME " _
                & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E WITH (NOLOCK, NOWAIT) LEFT JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS S " _
                    & "ON E.STATUS = S.SHISAKU_STATUS_CODE " _
                & "ORDER BY E.SHISAKU_EVENT_CODE, E.SHISAKU_KAIHATSU_FUGO"
            Dim db As New EBomDbClient
            Return db.QueryForList(Of EventEditCopyResultVo)(sql)
        End Function
    End Class
End Namespace