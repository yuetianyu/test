Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text
Imports ShisakuCommon

Namespace ShisakuBuhinMenu.Dao
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

        ''↓↓2014/08/21 1 ベース部品表作成表機能増強 ADD BEGIN
        ''' <summary>
        ''' 試作設計ブロックINSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByEventCodeAndGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements ShisakuSekkeiBlockInstlDao.FindByEventCodeAndGousya

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_GOUSYA = @ShisakuGousya ")
                .AppendLine(" AND INSU_SURYO > 0 ")
                ''↓↓2014/08/26 1 ベース部品表作成表機能増強 酒井 ADD BEGIN
                '↓↓2014/10/01 酒井 ADD BEGIN
                '.AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = ")
                '.AppendLine(" (SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO) FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode AND SHISAKU_GOUSYA = @ShisakuGousya AND INSU_SURYO > 0 )")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO <>'  0' ")
                '↑↑2014/10/01 酒井 ADD END
                ''↑↑2014/08/26 1 ベース部品表作成表機能増強 酒井 ADD END
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO")
            End With

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuGousya = shisakuGousya

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sql.ToString(), param)

        End Function

        ''' <summary>
        ''' 試作設計ブロックINSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByEventCode(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements ShisakuSekkeiBlockInstlDao.FindByEventCode

            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                ''↓↓2014/09/16 1 ベース部品表作成表機能増強 酒井 ADD BEGIN
                '.AppendLine("SELECT SHISAKU_BLOCK_NO, ")
                '.AppendLine(" INSTL_HINBAN, ")
                '.AppendLine(" INSTL_HINBAN_KBN, ")
                '.AppendLine(" INSTL_DATA_KBN, ")
                '.AppendLine(" MAX(SHISAKU_BLOCK_NO_KAITEI_NO) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                '.AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                '.AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                '.AppendLine(" GROUP BY SHISAKU_BLOCK_NO, ")
                '.AppendLine(" INSTL_HINBAN, ")
                '.AppendLine(" INSTL_HINBAN_KBN, ")
                '.AppendLine(" INSTL_DATA_KBN ")
                '.AppendLine(" ORDER BY SHISAKU_BLOCK_NO, ")
                '.AppendLine(" INSTL_HINBAN, ")
                '.AppendLine(" INSTL_HINBAN_KBN, ")
                '.AppendLine(" INSTL_DATA_KBN ")
                .AppendLine("SELECT DISTINCT SHISAKU_EVENT_CODE, SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO, INSTL_HINBAN_HYOUJI_JUN, INSTL_HINBAN, INSTL_HINBAN_KBN, INSU_SURYO, INSTL_DATA_KBN, BASE_INSTL_FLG FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO=(SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO) FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WHERE SHISAKU_EVENT_CODE =@ShisakuEventCode ) ")
                ''↑↑2014/09/16 1 ベース部品表作成表機能増強 酒井 ADD END
            End With

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sql.ToString(), param)

        End Function
        ''↑↑2014/08/21 1 ベース部品表作成表機能増強 ADD END

    End Class
End Namespace