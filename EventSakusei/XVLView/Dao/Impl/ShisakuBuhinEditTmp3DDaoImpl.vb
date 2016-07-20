Imports ShisakuCommon
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text


Namespace XVLView.Dao.Impl

    Public Class ShisakuBuhinEditTmp3DDaoImpl

        ''' <summary>
        ''' 3D情報を取得する.
        ''' </summary>
        ''' <param name="Guid"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByShisakuBuhinEditTmp3D(ByVal Guid As String) As List(Of tshisakuBuhinedittmp3dvo)
            Dim sql As New StringBuilder

            With sql
                .AppendLine("SELECT * ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT_TMP_3D  ")
                .AppendLine("WHERE")
                .AppendLine("    GUID = '" & Guid.ToString & "'")
                .AppendLine("ORDER BY ")
                .AppendLine("    SHISAKU_BLOCK_NO")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditTmp3dVo)(sql.ToString)
        End Function

        ''' <summary>
        ''' 3D情報を削除する.
        ''' </summary>
        ''' <param name="vo"></param>
        ''' <remarks></remarks>
        Public Sub DeleteByShisakuBuhinEditTmp3D(ByVal vo As TShisakuBuhinEditTmp3dVo)
            Dim sql As New StringBuilder

            With sql
                .AppendLine("DELETE ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT_TMP_3D  ")
                .AppendLine("WHERE")
                .AppendLine("    GUID = @Guid ")
                .AppendLine("    AND SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            End With

            Dim db As New EBomDbClient
            db.Delete(sql.ToString, vo)
        End Sub

    End Class

End Namespace

