Imports ShisakuCommon
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text


Namespace XVLView.Dao.Impl

    Public Class ShisakuBuhinEditGousyaTmp3DDaoImpl

        ''' <summary>
        ''' 3D情報を取得する.
        ''' </summary>
        ''' <param name="vo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByShisakuBuhinEditGousyaTmp3D(ByVal vo As TShisakuBuhinEditGousyaTmp3dVo) As List(Of TShisakuBuhinEditGousyaTmp3dVo)
            Dim sql As New StringBuilder

            With sql
                .AppendLine("SELECT * ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP_3D  ")
                .AppendLine("WHERE")
                .AppendLine("    GUID = @Guid ")
                .AppendLine("    AND SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine("    AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine("    AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine("    AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo ")
                .AppendLine("    AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
                .AppendLine("    AND GYOU_ID = @GyouId")
                .AppendLine("    AND NOT INSU_SURYO = 0")
                .AppendLine("ORDER BY ")
                .AppendLine("    SHISAKU_BLOCK_NO")
            End With


            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditGousyaTmp3dVo)(sql.ToString, vo)
        End Function

        ''' <summary>
        ''' 3D情報を削除する.
        ''' </summary>
        ''' <param name="vo"></param>
        ''' <remarks></remarks>
        Public Sub DeleteByShisakuBuhinEditGousyaTmp3D(ByVal vo As TShisakuBuhinEditGousyaTmp3dVo)
            Dim sql As New StringBuilder

            With sql
                .AppendLine("DELETE ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".DBO.T_SHISAKU_BUHIN_EDIT_GOUSYA_TMP_3D  ")
                .AppendLine("WHERE")
                .AppendLine("    GUID = @Guid ")
                .AppendLine("    AND SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            End With

            Dim db As New EBomDbClient
            db.Delete(sql.ToString, vo)
        End Sub

    End Class

End Namespace

