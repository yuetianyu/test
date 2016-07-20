Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text
Imports ShisakuCommon

Namespace ShisakuBuhinEditBlock.Dao
    '2012/01/12
    Public Class ShisakuSekkeiBlockInstlDaoImpl
        Implements ShisakuSekkeiBlockInstlDao

        ''' <summary>
        ''' 試作設計ブロックINSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番試作区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindShisakuSekkeiBlockInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal instlHinban As String, ByVal instlHinbanKbn As String, ByVal shisakuGousya As String) As TShisakuSekkeiBlockInstlVo Implements ShisakuSekkeiBlockInstlDao.FindShisakuSekkeiBlockInstl

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL")
                .AppendLine("WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo")
                .AppendLine(" AND INSTL_HINBAN = @InstlHinban")
                .AppendLine(" AND INSTL_HINBAN_KBN = @InstlHinbanKbn")
                .AppendLine(" AND SHISAKU_GOUSYA = @ShisakuGousya")
            End With
            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = shisakuBlockNoKaiteiNo
            param.InstlHinban = instlHinban
            param.InstlHinbanKbn = instlHinbanKbn
            param.ShisakuGousya = shisakuGousya
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuSekkeiBlockInstlVo)(sql.ToString(), param)

        End Function

    End Class
End Namespace