Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text
Imports ShisakuCommon

Namespace ShisakuBuhinEditBlock.Dao
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

    End Class
End Namespace