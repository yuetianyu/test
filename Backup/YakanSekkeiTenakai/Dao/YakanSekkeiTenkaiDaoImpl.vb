Imports ShisakuCommon
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text
Imports ShisakuCommon.Db.EBom
Imports YakanSekkeiTenakai.Vo

''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 (TES)張 ADD
Namespace Dao

    Public Class YakanSekkeiTenkaiDaoImpl

        ''' <summary>
        ''' 夜間設計展開の初期化
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitYakanSekkeiTenkai()
            Dim db As New EBomDbClient
            db.BeginTransaction()
            'T_SHISAKU_SEKKEI_BLOCK_EBOM_KANSHIの試作ブロック№改訂№=000以外を削除する	
            db.Delete("DELETE FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_EBOM_KANSHI WHERE SHISAKU_BLOCK_NO_KAITEI_NO <>'000'")

            'T_SHISAKU_SEKKEI_BLOCK_EBOM_KANSHIの試作ブロック№改訂№=000を999に更新する	
            db.Update("UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_EBOM_KANSHI SET SHISAKU_BLOCK_NO_KAITEI_NO='999'")

            'T_SHISAKU_SEKKEI_BLOCK_INSTL_EBOM_KANSHIの試作ブロック№改訂№=000以外を削除する	
            db.Delete("DELETE FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL_EBOM_KANSHI WHERE SHISAKU_BLOCK_NO_KAITEI_NO <>'000'")

            ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
            'T_SHISAKU_SEKKEI_BLOCK_INSTL_EBOM_KANSHIの試作ブロック№改訂№=000を999に更新する																																													
            'db.Update("UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI SET SHISAKU_BLOCK_NO_KAITEI_NO='999'")
            db.Update("UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL_EBOM_KANSHI SET SHISAKU_BLOCK_NO_KAITEI_NO='999'")
            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 酒井 ADD END

            'T_SHISAKU_BUHIN_EDIT_EBOM_KANSHIの試作ブロック№改訂№=000以外を削除する	
            db.Delete("DELETE FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI WHERE SHISAKU_BLOCK_NO_KAITEI_NO <>'000'")

            'T_SHISAKU_BUHIN_EDIT_EBOM_KANSHIの試作ブロック№改訂№=000を999に更新する	
            db.Update("UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI SET SHISAKU_BLOCK_NO_KAITEI_NO='999'")

            'T_SHISAKU_BUHIN_EDIT_INSTL_EBOM_KANSHIの試作ブロック№改訂№=000以外を削除する		
            db.Delete("DELETE FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_EBOM_KANSHI WHERE SHISAKU_BLOCK_NO_KAITEI_NO <>'000'")

            'T_SHISAKU_BUHIN_EDIT_INSTL_EBOM_KANSHIの試作ブロック№改訂№=000を999に更新する	
            db.Update("UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_EBOM_KANSHI SET SHISAKU_BLOCK_NO_KAITEI_NO='999'")

            'T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_TSUCHISHOの試作ブロック№改訂№=000以外を削除する	
            db.Delete("DELETE FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_TSUCHISHO WHERE SHISAKU_BLOCK_NO_KAITEI_NO <>'000'")

            'T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_TSUCHISHOの試作ブロック№改訂№=000を999に更新する																																													
            db.Update("UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_TSUCHISHO SET SHISAKU_BLOCK_NO_KAITEI_NO='999'")

            ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
            'T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_ZAIRYOUの試作ブロック№改訂№=000以外を削除する	
            db.Delete("DELETE FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_ZAIRYOU WHERE SHISAKU_BLOCK_NO_KAITEI_NO <>'000'")

            'T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_ZAIRYOUの試作ブロック№改訂№=000を999に更新する																																													
            db.Update("UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_ZAIRYOU SET SHISAKU_BLOCK_NO_KAITEI_NO='999'")
            ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 酒井 ADD END
            db.Commit()

        End Sub

        ''' <summary>
        ''' 監視対象イベントコード取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetKanshiTaisyouEventCode() As List(Of TShisakuEventVo)
            ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
            'Public Function GetKanshiTaisyouEventCode() As List(Of TShisakuEventEbomKanshiVo)
            ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
                '.AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_EBOM_KANSHI EBOM ")
                '.AppendLine(" INNER JOIN ")
                '.AppendLine(MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT EVENT ")
                '.AppendLine(" ON ")
                '.AppendLine(" EVENT.SHISAKU_EVENT_CODE = EBOM.SHISAKU_EVENT_CODE ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT EVENT ")
                ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END
                .AppendLine(" WHERE ")
                .AppendLine(" EVENT.TENKAI_STATUS = 1 ")
                .AppendLine(" AND ")
                .AppendLine(" EVENT.EBOM_KANSHI_FLG = '1' ")
                ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
                '.AppendLine(" ORDER BY EBOM.SHISAKU_EVENT_CODE")
                .AppendLine(" ORDER BY EVENT.SHISAKU_EVENT_CODE")
                ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END
            End With

            Dim db As New EBomDbClient
            ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
            'Return db.QueryForList(Of TShisakuEventEbomKanshiVo)(sb.ToString)
            Return db.QueryForList(Of TShisakuEventVo)(sb.ToString)
            ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END

        End Function

        ''' <summary>
        ''' 試作部品編集情報(EBOM設変)を取得.
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetShisakuBuhinEditEbomKanshi(ByVal shisakuEventCode As String) As List(Of TShisakuBuhinEditEbomKanshiVo)

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
                '.AppendLine("SELECT SHISAKU_EVENT_CODE, ")
                '.AppendLine(" SHISAKU_BUKA_CODE, ")
                '.AppendLine(" SHISAKU_BLOCK_NO, ")
                '.AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                '.AppendLine(" BUHIN_NO, ")
                '.AppendLine(" MAX(SHISAKU_BLOCK_NO_KAITEI_NO) AS SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("SELECT *")
                ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 酒井 ADD END
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
                '.AppendLine(" GROUP BY SHISAKU_EVENT_CODE, ")
                '.AppendLine(" SHISAKU_BUKA_CODE, ")
                '.AppendLine(" SHISAKU_BLOCK_NO, ")
                '.AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                '.AppendLine(" BUHIN_NO ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 酒井 ADD END
                .AppendLine(" ORDER BY SHISAKU_EVENT_CODE, ")
                .AppendLine(" SHISAKU_BUKA_CODE, ")
                .AppendLine(" SHISAKU_BLOCK_NO, ")
                .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine(" BUHIN_NO ")
            End With

            Dim db As New EBomDbClient
            Dim iArg As New TShisakuBuhinEditEbomKanshiVo
            iArg.ShisakuEventCode = shisakuEventCode

            Return db.QueryForList(Of TShisakuBuhinEditEbomKanshiVo)(sb.ToString, iArg)

        End Function

        '/*** 20140923 CHANGE START（高速化対応） ***/
        ''' <summary>
        ''' 試作部品編集情報(EBOM設変)を取得（通知、材料作成用）.
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <returns></returns>
        ''' <remarks>取得項目を指定した</remarks>
        Public Function GetShisakuBuhinEditEbomKanshiForCreateTsuchishoAndZairyou(ByVal shisakuEventCode As String) _
            As List(Of YakanSekkeiGetForCreateTsuchishoAndZairyouTargetVo)

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine("SELECT SHISAKU_EVENT_CODE, ")
                .AppendLine(" SHISAKU_BUKA_CODE, ")
                .AppendLine(" SHISAKU_BLOCK_NO, ")
                .AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO,  ")
                .AppendLine(" BUHIN_NO, ")
                .AppendLine(" BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                .AppendLine(" ORDER BY SHISAKU_EVENT_CODE, ")
                .AppendLine(" SHISAKU_BUKA_CODE, ")
                .AppendLine(" SHISAKU_BLOCK_NO, ")
                .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine(" BUHIN_NO ")
            End With

            Dim db As New EBomDbClient
            Dim iArg As New YakanSekkeiGetForCreateTsuchishoAndZairyouTargetVo
            iArg.ShisakuEventCode = shisakuEventCode

            Return db.QueryForList(Of YakanSekkeiGetForCreateTsuchishoAndZairyouTargetVo)(sb.ToString, iArg)

        End Function
        '/*** 20140923 CHANGE END ***/

        ''' <summary>
        ''' 監視対象イベントコード取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetKanshiTaisyouEventCodeByKaiteiNo() As List(Of TShisakuSekkeiBlockEbomKanshiVo)

            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD BEGIN
                '.AppendLine("SELECT EBOM.SHISAKU_EVENT_CODE, ")
                '.AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                '.AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_EBOM_KANSHI EBOM ")
                '.AppendLine(" WHERE ")
                '.AppendLine(" EBOM.SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                '.AppendLine(" OR ")
                '.AppendLine(" EBOM.SHISAKU_BLOCK_NO_KAITEI_NO = '999' ")
                '.AppendLine(" GROUP BY EBOM.SHISAKU_EVENT_CODE, ")
                '.AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                '.AppendLine(" ORDER BY EBOM.SHISAKU_EVENT_CODE, ")
                '.AppendLine(" SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("SELECT SBEK.SHISAKU_EVENT_CODE ")
                .AppendLine(" FROM (SELECT SHISAKU_EVENT_CODE,SHISAKU_BLOCK_NO_KAITEI_NO FROM mBOM.dbo.T_SHISAKU_SEKKEI_BLOCK_EBOM_KANSHI GROUP BY SHISAKU_EVENT_CODE,SHISAKU_BLOCK_NO_KAITEI_NO ) as SBEK ")
                .AppendLine(" GROUP BY SBEK.SHISAKU_EVENT_CODE ")
                .AppendLine(" having count(*)>1 ")
                ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockEbomKanshiVo)(sb.ToString)

        End Function


        ''' <summary>
        ''' 該当する部品編集情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>部品編集情報</returns>
        ''' <remarks></remarks>
        Public Function FindByAllBuhinEdit(ByVal shisakuEventCode As String) As List(Of YakanSekkeiBlockVoHelperExcel)
            '↓↓2014/10/10 酒井 ADD BEGIN
            '        Public Function FindByAllBuhinEdit(ByVal shisakuEventCode As String, Optional ByVal shisakuGousya As String = "") As List(Of YakanSekkeiBlockVoHelperExcel)
            '↑↑2014/10/10 酒井 ADD END
            '↓↓2014/09/26 酒井 ADD BEGIN
            '        Public Function FindByAllBuhinEdit(ByVal shisakuEventCode As String) As List(Of YakanSekkeiBlockVoHelperExcel)
            '↑↑2014/09/26 酒井 ADD END
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine("BE.*, ")
                .AppendLine("BET.ZUMEN_KAITEI_NO, ")
                ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
                .AppendLine("BET.TSUCHISHO_NO, ")
                ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
                .AppendLine("BEZ.ZAIRYO_KIJUTSU, ")
                .AppendLine("BEZ.BANKO_SURYO, ")
                .AppendLine("SB.USER_ID, ")
                .AppendLine("SB.TEL_NO, ")
                .AppendLine("SBI.SHISAKU_GOUSYA, ")
                .AppendLine("B.HYOJIJUN_NO, ")
                .AppendLine("BEI.INSU_SURYO ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI BE WITH (NOLOCK, NOWAIT) ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_TSUCHISHO BET ")
                ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
                '.AppendLine("ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine("ON BET.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END
                .AppendLine("AND BET.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine("AND BET.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine("AND BET.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND BET.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_EBOM_KANSHI_ZAIRYOU BEZ ")
                .AppendLine("ON BEZ.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine("AND BEZ.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine("AND BEZ.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine("AND BEZ.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND BEZ.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_EBOM_KANSHI SB ")
                .AppendLine("ON SB.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine("AND SB.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine("AND SB.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine("AND SB.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND SB.BLOCK_FUYOU = '0' ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL_EBOM_KANSHI BEI ")
                .AppendLine("ON BEI.SHISAKU_EVENT_CODE = BE.SHISAKU_EVENT_CODE ")
                .AppendLine("AND BEI.SHISAKU_BUKA_CODE = BE.SHISAKU_BUKA_CODE ")
                .AppendLine("AND BEI.SHISAKU_BLOCK_NO = BE.SHISAKU_BLOCK_NO ")
                .AppendLine("AND BEI.SHISAKU_BLOCK_NO_KAITEI_NO = BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND BEI.BUHIN_NO_HYOUJI_JUN = BE.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL_EBOM_KANSHI SBI ")
                .AppendLine("ON SBI.SHISAKU_EVENT_CODE = BEI.SHISAKU_EVENT_CODE ")
                .AppendLine("AND SBI.SHISAKU_BUKA_CODE = BEI.SHISAKU_BUKA_CODE ")
                .AppendLine("AND SBI.SHISAKU_BLOCK_NO = BEI.SHISAKU_BLOCK_NO ")
                .AppendLine("AND SBI.SHISAKU_BLOCK_NO_KAITEI_NO = BEI.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("AND SBI.INSTL_HINBAN_HYOUJI_JUN = BEI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine("AND NOT ( SBI.INSU_SURYO IS NULL OR SBI.INSU_SURYO ='' ) ")
                .AppendLine("INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_EBOM_KANSHI B ")
                .AppendLine("ON B.SHISAKU_EVENT_CODE = SBI.SHISAKU_EVENT_CODE ")
                .AppendLine("AND B.SHISAKU_GOUSYA = SBI.SHISAKU_GOUSYA ")
                .AppendLine("WHERE ")
                .AppendLine("BE.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                '↓↓2014/10/10 酒井 ADD BEGIN
                ''↓↓2014/09/26 酒井 ADD BEGIN
                'If Not shisakuGousya = "" Then
                '    .AppendLine("AND SBI.SHISAKU_GOUSYA = '" & shisakuGousya & "'")
                'End If
                ''↑↑2014/09/26 酒井 ADD END
                '↑↑2014/10/10 酒井 ADD END
                '↓↓2014/09/30 酒井 ADD BEGIN
                '.AppendLine(" ORDER BY BE.SHISAKU_EVENT_CODE, BE.SHISAKU_BUKA_CODE, BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO, BE.BUHIN_NO_HYOUJI_JUN ")
                '↓↓2014/10/13 酒井 ADD BEGIN
                '                .AppendLine(" ORDER BY BE.SHISAKU_EVENT_CODE, BE.SHISAKU_BUKA_CODE, BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO, BE.BUHIN_NO_HYOUJI_JUN, SBI.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" ORDER BY BE.SHISAKU_EVENT_CODE, BE.SHISAKU_BUKA_CODE, BE.SHISAKU_BLOCK_NO, BE.BUHIN_NO, BE.BUHIN_NO_HYOUJI_JUN, SBI.INSTL_HINBAN_HYOUJI_JUN,B.HYOJIJUN_NO,BE.SHISAKU_BLOCK_NO_KAITEI_NO ")
                '↑↑2014/10/13 酒井 ADD END
                '↑↑2014/09/30 酒井 ADD END
            End With

            Dim db As New EBomDbClient
            Dim param As New YakanSekkeiBlockVoHelperExcel

            param.ShisakuEventCode = shisakuEventCode
            Return db.QueryForList(Of YakanSekkeiBlockVoHelperExcel)(sb.ToString, param)
        End Function
        '↓↓2014/09/26 酒井 ADD BEGIN
        Public Function FindByGousya(ByVal shisakuEventCode As String) As List(Of TShisakuEventEbomKanshiVo)
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT ")
                .AppendLine("* ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_EBOM_KANSHI WITH (NOLOCK, NOWAIT) ")
                .AppendLine("WHERE ")
                .AppendLine("SHISAKU_EVENT_CODE = @ShisakuEventCode ")
            End With

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventEbomKanshiVo

            param.ShisakuEventCode = shisakuEventCode
            Return db.QueryForList(Of TShisakuEventEbomKanshiVo)(sb.ToString, param)
        End Function
        '↑↑2014/09/26 酒井 ADD END

        ''' <summary>
        ''' イベント情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Public Function FindByEvent(ByVal shisakuEventCode As String) As TShisakuEventVo

            Dim sql As String = _
            " SELECT * " _
            & " FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " SHISAKU_EVENT_CODE = @ShisakuEventCode "

            Dim db As New EBomDbClient
            Dim param As New TShisakuEventVo
            param.ShisakuEventCode = shisakuEventCode

            Return db.QueryForObject(Of TShisakuEventVo)(sql, param)
        End Function

        ''' <summary>
        ''' 社員名を返す
        ''' </summary>
        ''' <param name="shainId">社員ID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByShainName(ByVal shainId As String) As String

            Dim sql As String = _
            " SELECT * " _
            & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0650 WITH (NOLOCK, NOWAIT) " _
            & " WHERE " _
            & " SHAIN_NO = @ShainNo " _
            & " AND SITE_KBN = @SiteKbn "

            Dim db As New EBomDbClient
            Dim param As New Rhac0650Vo
            param.ShainNo = shainId
            'とりあえず１にしておく'
            param.SiteKbn = "1"
            Dim result As String = ""

            Dim shain As New Rhac0650Vo
            Dim shain2 As New Rhac2130Vo
            shain = db.QueryForObject(Of Rhac0650Vo)(sql, param)

            If shain Is Nothing Then
                Dim sql2 As String = _
                " SELECT * " _
                & " FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0650 WITH (NOLOCK, NOWAIT) " _
                & " WHERE " _
                & " SHAIN_NO = @ShainNo " _
                & " AND SITE_KBN = @SiteKbn "

                Dim db2 As New EBomDbClient
                Dim param2 As New Rhac2130Vo
                param.ShainNo = shainId
                'とりあえず１にしておく'
                param2.SiteKbn = "1"

                shain2 = db.QueryForObject(Of Rhac2130Vo)(sql2, param2)

                If Not shain2 Is Nothing Then
                    result = shain2.ShainName
                End If
            Else
                result = shain.ShainName
            End If

            Return result
        End Function

        Public Const site_kbn = 1
        '担当承認課と課長承認課に部課コードではなく、課略名を表示させるための処理を追加'
        Public Function GetKa_Ryaku_Name(ByVal syoninka As String) As Rhac1560Vo
            Dim Bu_Code As String
            Dim Ka_Code As String
            Bu_Code = Left(syoninka, 2)
            Ka_Code = Right(syoninka, 2)
            Dim sql As String = _
                "SELECT " _
                & "   KA_RYAKU_NAME " _
                & "   FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 WITH (NOLOCK, NOWAIT) " _
                & "   WHERE" _
                & "   SITE_KBN =" & site_kbn _
                & "   AND BU_CODE =@BuCode" _
                & "   AND KA_CODE =@KaCode"

            Dim db As New EBomDbClient
            Dim param As New Rhac1560Vo
            param.BuCode = Bu_Code
            param.KaCode = Ka_Code

            '2012/03/23 課略名が取得できなかった場合は課コードをそのまま返す。
            'これは課マスターに存在せず、mBOMのSHISAKU_BUKA_CODEに入力された課名が
            'そのまま格納されている場合の対応
            '例）DGD1,DGD2,DGS5,HEV,TESTなど
            Dim vo As Rhac1560Vo
            vo = db.QueryForObject(Of Rhac1560Vo)(sql, param)
            If Not vo Is Nothing Then
                Return vo
            Else
                vo = New Rhac1560Vo
                vo.KaRyakuName = syoninka
                Return vo
            End If
        End Function

    End Class
End Namespace
