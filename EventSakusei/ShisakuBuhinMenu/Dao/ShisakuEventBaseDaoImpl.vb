Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text
Imports ShisakuCommon

Namespace ShisakuBuhinMenu.Dao
    '2012/01/12
    Public Class ShisakuEventBaseDaoImpl
        Implements ShisakuEventBaseDao

        Public Function FindShisakuEventBase(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo) Implements ShisakuEventBaseDao.FindShisakuEventBase
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE ")
                .AppendLine("WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode")
            End With
            Dim param As New ShisakuEventBaseResultVo
            param.ShisakuEventCode = shisakuEventCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventBaseVo)(sql.ToString(), param)
        End Function
        '↓↓2014/09/24 酒井 ADD BEGIN
        Public Function FindShisakuEventEbomKanshi(ByVal shisakuEventCode As String) As List(Of TShisakuEventEbomKanshiVo) Implements ShisakuEventBaseDao.FindShisakuEventEbomKanshi
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_EBOM_KANSHI ")
                .AppendLine("WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode")
            End With
            Dim param As New TShisakuEventEbomKanshiVo
            param.ShisakuEventCode = shisakuEventCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventEbomKanshiVo)(sql.ToString(), param)
        End Function
        '↑↑2014/09/24 酒井 ADD END
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuGousya"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindShisakuEventBaseByEventCodeAndGousyaForShisakuGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventBaseVo Implements ShisakuEventBaseDao.FindShisakuEventBaseByEventCodeAndGousyaForShisakuGousya

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT BASE_KAIHATSU_FUGO ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE ")
                .AppendLine("WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode")
                .AppendLine("AND SHISAKU_GOUSYA = @ShisakuGousya")
            End With
            Dim param As New TShisakuEventBaseVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuGousya = shisakuGousya
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventBaseVo)(sql.ToString(), param)

        End Function
        '↓↓2014/09/24 酒井 ADD BEGIN
        Public Function FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventEbomKanshiVo Implements ShisakuEventBaseDao.FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuGousya

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT BASE_KAIHATSU_FUGO ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_EBOM_KANSHI ")
                .AppendLine("WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode")
                .AppendLine("AND SHISAKU_GOUSYA = @ShisakuGousya")
            End With
            Dim param As New TShisakuEventEbomKanshiVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuGousya = shisakuGousya
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventEbomKanshiVo)(sql.ToString(), param)

        End Function
        '↑↑2014/09/24 酒井 ADD END
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuGousya"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindShisakuEventBaseByEventCodeAndGousyaForShisakuBaseGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventBaseVo Implements ShisakuEventBaseDao.FindShisakuEventBaseByEventCodeAndGousyaForShisakuBaseGousya

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT B.BASE_KAIHATSU_FUGO ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE A ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE B ")
                .AppendLine("    ON A.SHISAKU_BASE_EVENT_CODE = B.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND A.SHISAKU_BASE_GOUSYA = B.SHISAKU_GOUSYA ")
                .AppendLine("WHERE A.SHISAKU_EVENT_CODE = @ShisakuEventCode")
                .AppendLine("AND A.SHISAKU_BASE_GOUSYA = @ShisakuGousya")
            End With
            Dim param As New TShisakuEventBaseVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuGousya = shisakuGousya
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventBaseVo)(sql.ToString(), param)

        End Function
        '↓↓2014/09/24 酒井 ADD BEGIN
        Public Function FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuBaseGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventEbomKanshiVo Implements ShisakuEventBaseDao.FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuBaseGousya

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT B.BASE_KAIHATSU_FUGO ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE A ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_EBOM_KANSHI B ")
                .AppendLine("    ON A.SHISAKU_BASE_EVENT_CODE = B.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND A.SHISAKU_BASE_GOUSYA = B.SHISAKU_GOUSYA ")
                .AppendLine("WHERE A.SHISAKU_EVENT_CODE = @ShisakuEventCode")
                .AppendLine("AND A.SHISAKU_BASE_GOUSYA = @ShisakuGousya")
            End With
            Dim param As New TShisakuEventEbomKanshiVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuGousya = shisakuGousya
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventEbomKanshiVo)(sql.ToString(), param)

        End Function
        '↑↑2014/09/24 酒井 ADD END
        ''' <summary>
        ''' ベース車開発符号を取得
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindBaseKaihatsuFugo(ByVal buhinNo As String) As List(Of TShisakuEventBaseVo) Implements ShisakuEventBaseDao.FindBaseKaihatsuFugo
            Dim sql As String
            sql = "SELECT DISTINCT  D.BASE_KAIHATSU_FUGO     FROM (" & " " & _
                    "SELECT * FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT" & " " & _
                    "WHERE BUHIN_NO  =@BuhinNo) A " & " " & _
                    "INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL B ON  A.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE" & " " & _
                    "AND A.SHISAKU_BUKA_CODE=B.SHISAKU_BUKA_CODE" & " " & _
                    "AND A.SHISAKU_BLOCK_NO=B.SHISAKU_BLOCK_NO" & " " & _
                    "AND A.SHISAKU_BLOCK_NO_KAITEI_NO=B.SHISAKU_BLOCK_NO_KAITEI_NO" & " " & _
                    "AND A.BUHIN_NO_HYOUJI_JUN=B.BUHIN_NO_HYOUJI_JUN" & " " & _
                    "INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL  C " & " " & _
                    "ON A.SHISAKU_EVENT_CODE=C.SHISAKU_EVENT_CODE" & " " & _
                    "AND A.SHISAKU_BUKA_CODE=C.SHISAKU_BUKA_CODE" & " " & _
                    "AND A.SHISAKU_BLOCK_NO=C.SHISAKU_BLOCK_NO" & " " & _
                    "AND A.SHISAKU_BLOCK_NO_KAITEI_NO=C.SHISAKU_BLOCK_NO_KAITEI_NO" & " " & _
                    "AND B.INSTL_HINBAN_HYOUJI_JUN =C.INSTL_HINBAN_HYOUJI_JUN " & " " & _
                    "INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE  D  ON A.SHISAKU_EVENT_CODE=D.SHISAKU_EVENT_CODE" & " " & _
                    "AND A.SHISAKU_BLOCK_NO_KAITEI_NO=D.HYOJIJUN_NO" & " " & _
                    "AND D.SHISAKU_GOUSYA  =C.SHISAKU_GOUSYA" & " " & _
                    "  WHERE(B.INSU_SURYO > 0)" & " " & _
                    "AND C.INSU_SURYO  >0"
            Dim param As New TShisakuBuhinEditVo
            param.BuhinNo = buhinNo
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventBaseVo)(sql, param)
        End Function

    End Class
End Namespace