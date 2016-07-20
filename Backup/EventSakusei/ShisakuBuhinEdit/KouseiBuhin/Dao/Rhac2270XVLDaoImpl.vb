Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon
Imports System.Text
Imports EventSakusei.ShisakuBuhinEdit.KouseiBuhin.Dao.Vo

Namespace KouseiBuhin.Dao


    Public Class Rhac2270XVLDaoImpl : Implements Rhac2270XVLDao

        ''' <summary>
        ''' RHAC2270のXVLファイルパス取得
        ''' </summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="BlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetXVLFileNameS(ByVal KaihatsuFugo As String, ByVal BlockNo As String) _
            As List(Of Rhac2270XVLVo) Implements Rhac2270XVLDao.GetXVLFileNameS

            Dim aArg As New Rhac2270XVLVo

            aArg.KaihatsuFugo = KaihatsuFugo
            aArg.BlockNo = BlockNo


            Dim iSql As New System.Text.StringBuilder
            With iSql
                .AppendLine("SELECT ")
                .AppendLine("    [XVL_FILE_NAME]")
                .AppendLine("    ,[CAD_DATA_EVENT_KBN]")
                .AppendLine("    ,[CAD_KAITEI_NO]")
                .AppendLine("    ,[BLOCK_NO]")
                .AppendLine("    ,[BUHIN_NO]")
                .AppendLine("FROM ")
                .AppendLine("    " & RHACLIBF_DB_NAME & ".[dbo].[RHAC2270]")
                .AppendLine("WHERE")
                .AppendLine("    KAIHATSU_FUGO = @KaihatsuFugo ")
                .AppendLine("    AND BLOCK_NO IN (@BlockNo) ")

                .AppendLine("ORDER BY ")
                'CADデータイベント区分の優先順位は G > ND > S3 > S2 > S1
                .AppendLine("    CASE [CAD_DATA_EVENT_KBN]")
                .AppendLine("      WHEN 'G'  THEN 1")
                .AppendLine("      WHEN 'ND' THEN 2")
                .AppendLine("      WHEN 'S3' THEN 3")
                .AppendLine("      WHEN 'S2' THEN 4")
                .AppendLine("      WHEN 'S1' THEN 5")
                .AppendLine("    END ASC")
                .AppendLine("    ,CAD_KAITEI_NO DESC")
            End With

            Dim db As New EBomDbClient
            Dim iFiles As New List(Of Rhac2270XVLVo)
            iFiles = db.QueryForList(Of Rhac2270XVLVo)(iSql.ToString, aArg)

            Return iFiles

        End Function
    End Class

End Namespace
