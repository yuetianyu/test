Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon

Namespace EventEdit.Dao
    Public Class EventEditReferenceCarDaoImpl
        Implements EventEditReferenceCarDao

        Public Function FindKanseiBy(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventKanseiVo Implements EventEditReferenceCarDao.FindKanseiBy
            Dim sql As String = "SELECT * " _
                & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_KANSEI K WITH (NOLOCK, NOWAIT) " _
                & "WHERE EXISTS " _
                    & "(SELECT * " _
                    & "FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_SEISAKU_ICHIRAN WITH (NOLOCK, NOWAIT) " _
                    & "WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode " _
                        & "AND SHISAKU_GOUSYA = @ShisakuGousya " _
                        & "AND SHISAKU_EVENT_CODE = K.SHISAKU_EVENT_CODE " _
                        & "AND HYOJIJUN_NO = K.HYOJIJUN_NO " _
                        & ")"
            Dim param As New TShisakuEventBaseSeisakuIchiranVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuGousya = shisakuGousya
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventKanseiVo)(sql, param)
        End Function
    End Class
End Namespace