Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports System.Text
Imports ShisakuCommon

Namespace ShisakuBuhinEditBlock.Dao
    Public Class SekkeiBlockDaoImpl : Implements SekkeiBlockDao

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Public Function FindAlByShisakuEventBaseIroAri(ByVal shisakuEventCode As String, ByVal tantoId As String, Optional ByVal HyojiJunNo As String = "") As List(Of SekkeiBlockAlResultVo) Implements SekkeiBlockDao.FindAlByShisakuEventBase
            Dim sql As New StringBuilder()
            '★指定された仕様情報№のブロックを抽出。
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT AL.BLOCK_NO, ")
                .AppendLine(" CASE WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+MAX( AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS FF_BUHIN_NO, ")
                .AppendLine(" CASE WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS BF_BUHIN_NO, ")
                .AppendLine(" KA.BU_CODE, ")
                .AppendLine(" MAX(KA.KA_CODE) AS KA_CODE,")
                .AppendLine(" BASE.SHISAKU_GOUSYA, ")
                .AppendLine(" BLOCKM.TANTO_BUSHO AS BUKA_CODE, ")
                .AppendLine(" BLOCK_NAME, ")
                .AppendLine(" MAX(AL2.TOP_COLOR_KAITEI_NO) AS TOP_COLOR_KAITEI_NO, ")
                .AppendLine(" MAX(COLORM.FUKA_NO) AS FUKA_NO, ")
                .AppendLine(" MAX(AL.SOBI_KAITEI_NO) AS SOBI_KAITEI_NO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 AL WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine("     INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_TMP BASE ")
                .AppendLine("         ON AL.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And AL.KATASHIKI_SCD_7 = BASE.BASE_KATASHIKI_SCD_7 ")
                .AppendLine("              And AL.SHIMUKECHI_CODE = BASE.BASE_SHIMUKE ")
                .AppendLine("              And AL.OP_CODE = BASE.BASE_OP ")
                .AppendLine("              AND AL.F_BUHIN_NO <>  '         0     ' ")
                .AppendLine("              AND LEFT(AL.BLOCK_NO,3) <>  '999' ")
                .AppendLine("     INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 SHIYO WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON SHIYO.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And SHIYO.SHIYOSHO_SEQNO = ")
                .AppendLine("                 RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 AL2 WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON AL2.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("             And AL2.KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("             And AL2.COL_NO = AL.COL_NO ")
                .AppendLine("             And AL2.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("             And AL2.F_BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("             AND (AL2.COLOR_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                 OR AL2.COLOR_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("             AND AL2.TOP_COLOR_KAITEI_NO = (SELECT MAX(TOP_COLOR_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                 WHERE(KAIHATSU_FUGO = AL2.KAIHATSU_FUGO) ")
                .AppendLine("                  AND KATASHIKI_SCD_7 = AL2.KATASHIKI_SCD_7 ")
                .AppendLine("                  AND COL_NO = AL2.COL_NO ")
                .AppendLine("                  AND BLOCK_NO = AL2.BLOCK_NO ")
                .AppendLine("                  AND F_BUHIN_NO = AL2.F_BUHIN_NO ")
                .AppendLine("                  AND COLOR_CODE = AL2.COLOR_CODE ) ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 BUHINM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUHINM.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("         And BUHINM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO ")
                .AppendLine("         And BUIM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And BUIM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON COLORM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("         And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("         And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("         AND (COLORM.COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("            OR COLORM.COLOR_CODE =  BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("         And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                     And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("                                     And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("                                     And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("                                     AND (COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                                         OR COLOR_CODE =  BASE.BASE_NAISOUSYOKU)) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 BLOCKM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.BLOCK_NO_KINO = AL.BLOCK_NO ")
                .AppendLine("         And BLOCKM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         AND LEFT(BLOCKM. BLOCK_NO_KINO, 3) <> '999' ")
                .AppendLine("         And BLOCKM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 BLOCKNAME WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKNAME.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 KA WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.TANTO_BUSHO = KA.KA_RYAKU_NAME ")
                .AppendLine(" WHERE BASE.SHISAKU_EVENT_CODE ='" & shisakuEventCode & "' AND ")
                .AppendLine("       BASE.TANTO_ID ='" & tantoId & "' AND ")
                If HyojiJunNo <> "" Then
                    .AppendLine(String.Concat(String.Concat(" BASE.HYOJIJUN_NO = ", HyojiJunNo), " AND "))
                End If
                .AppendLine("     BLOCKM.TANTO_BUSHO IS NOT NULL AND ")
                .AppendLine("     AL.SHIYOSA_KAITEI_NO = (SELECT MAX(SHIYOSA_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                        And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                        And COL_NO = AL.COL_NO ")
                .AppendLine("                        And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                        AND SOBI_KAITEI_NO = AL.SOBI_KAITEI_NO ) ")
                '2012/07/19 NULLも取得するように変更'
                '.AppendLine("     AND NOT ( SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' ")
                '.AppendLine("        AND AL2.TOP_COLOR_KAITEI_NO IS NULL ")
                '.AppendLine("        AND COLORM.FUKA_NO IS NULL ) ")
                .AppendLine("     AND AL.SOBI_KAITEI_NO = SHIYO.SOBI_KAITEI_NO ")
                '2012/02/15 AL.SOBI_KAITEI_NO＝AL内の最大値（ただし仕様書テーブルの装備改訂Noを超えないものに変更
                '13日の修正では仮に仕様書テーブルの装備改訂Noを持つものがあってもそれより小さい改訂Noも取得されてしまう。
                '2012/02/13 AL.SOBI_KAITEI_NO＝仕様書テーブルの装備改訂Noの最大値ではなく
                '           AL.SOBI_KAITEI_NO＜＝仕様書テーブルの装備改訂Noの最大値に変更
                '           仕様書テーブルの装備改訂Noと一致したものが無い場合、さらに小さいものを探す
                .AppendLine("     AND AL.SOBI_KAITEI_NO = ")
                '.AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 ")
                '.AppendLine("              WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                '.AppendLine("                And SHIYOSHO_SEQNO =  ")
                '.AppendLine("                  RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3)) ")
                .AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("                  WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                 And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                                 And COL_NO = AL.COL_NO ")
                .AppendLine("                                 And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                                 AND SOBI_KAITEI_NO <= (")
                .AppendLine("                                               SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                                       WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                                                         And SHIYOSHO_SEQNO =  ")
                .AppendLine("                                                                         RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3)")
                .AppendLine("                                 )")
                .AppendLine("         )")
                '-----------------------------------------
                .AppendLine(" GROUP BY ")
                .AppendLine("     AL.BLOCK_NO, ")
                .AppendLine("     KA.BU_CODE, ")
                .AppendLine("     AL.F_BUHIN_NO, ")
                .AppendLine("     BASE.SHISAKU_GOUSYA, ")
                .AppendLine("     BLOCKM.TANTO_BUSHO, ")
                .AppendLine("     BLOCK_NAME ")
                .AppendLine(" ORDER BY ")
                .AppendLine("     BLOCK_NO, ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     FF_BUHIN_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of SekkeiBlockAlResultVo)(sql.ToString())
        End Function
        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Public Function FindAlByShisakuEventBaseIroAriByBlock(ByVal shisakuEventCode As String, ByVal tantoId As String, ByVal blockNo As String) As List(Of SekkeiBlockAlResultVo) Implements SekkeiBlockDao.FindAlByShisakuEventBaseByBlock
            Dim sql As New StringBuilder()
            '★指定された仕様情報№のブロックを抽出。
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT AL.BLOCK_NO, ")
                '2012/02/16 色の有無だけではなく、桁も参照するようにしてみる
                .AppendLine(" CASE WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL) OR NOT SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+MAX( AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS FF_BUHIN_NO, ")
                .AppendLine(" CASE WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL) OR NOT SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS BF_BUHIN_NO, ")
                .AppendLine(" KA.BU_CODE, ")
                .AppendLine(" MAX(KA.KA_CODE) AS KA_CODE,")
                .AppendLine(" BASE.SHISAKU_GOUSYA, ")
                .AppendLine(" BLOCKM.TANTO_BUSHO AS BUKA_CODE, ")
                .AppendLine(" BLOCK_NAME, ")
                .AppendLine(" MAX(AL2.TOP_COLOR_KAITEI_NO) AS TOP_COLOR_KAITEI_NO, ")
                .AppendLine(" MAX(COLORM.FUKA_NO) AS FUKA_NO, ")
                .AppendLine(" MAX(AL.SOBI_KAITEI_NO) AS SOBI_KAITEI_NO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 AL WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine("     INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_TMP BASE ")
                .AppendLine("         ON AL.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And AL.KATASHIKI_SCD_7 = BASE.BASE_KATASHIKI_SCD_7 ")
                .AppendLine("              And AL.SHIMUKECHI_CODE = BASE.BASE_SHIMUKE ")
                .AppendLine("              And AL.OP_CODE = BASE.BASE_OP ")
                .AppendLine("              AND AL.F_BUHIN_NO <>  '         0     ' ")
                .AppendLine("              AND LEFT(AL.BLOCK_NO,3) <>  '999' ")
                .AppendLine("     INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 SHIYO WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON SHIYO.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And SHIYO.SHIYOSHO_SEQNO = ")
                .AppendLine("                 RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 AL2 WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON AL2.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("             And AL2.KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("             And AL2.COL_NO = AL.COL_NO ")
                .AppendLine("             And AL2.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("             And AL2.F_BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("             AND (AL2.COLOR_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                 OR AL2.COLOR_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("             AND AL2.TOP_COLOR_KAITEI_NO = (SELECT MAX(TOP_COLOR_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                 WHERE(KAIHATSU_FUGO = AL2.KAIHATSU_FUGO) ")
                .AppendLine("                  AND KATASHIKI_SCD_7 = AL2.KATASHIKI_SCD_7 ")
                .AppendLine("                  AND COL_NO = AL2.COL_NO ")
                .AppendLine("                  AND BLOCK_NO = AL2.BLOCK_NO ")
                .AppendLine("                  AND F_BUHIN_NO = AL2.F_BUHIN_NO ")
                .AppendLine("                  AND COLOR_CODE = AL2.COLOR_CODE ) ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 BUHINM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUHINM.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("         And BUHINM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO ")
                .AppendLine("         And BUIM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And BUIM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON COLORM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("         And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("         And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine(" 		  AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine("         AND (COLORM.COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("            OR COLORM.COLOR_CODE =  BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("         And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                     And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("                                     And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("                                     And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("                                     		 AND KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine("                                     AND (COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                                         OR COLOR_CODE =  BASE.BASE_NAISOUSYOKU)) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 BLOCKM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.BLOCK_NO_KINO = AL.BLOCK_NO ")
                .AppendLine("         And BLOCKM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         AND LEFT(BLOCKM. BLOCK_NO_KINO, 3) <> '999' ")
                .AppendLine("         And BLOCKM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 BLOCKNAME WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKNAME.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 KA WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.TANTO_BUSHO = KA.KA_RYAKU_NAME ")
                .AppendLine(" WHERE BASE.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' AND ")
                .AppendLine("       BASE.TANTO_ID ='" & tantoId & "' AND ")
                .AppendLine(String.Concat(String.Concat(" AL.BLOCK_NO = '", blockNo), "' AND "))
                .AppendLine("     BLOCKM.TANTO_BUSHO IS NOT NULL AND ")
                .AppendLine("     AL.SHIYOSA_KAITEI_NO = (SELECT MAX(SHIYOSA_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                        And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                        And COL_NO = AL.COL_NO ")
                .AppendLine("                        And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                        AND SOBI_KAITEI_NO = AL.SOBI_KAITEI_NO ) ")
                '2012/02/16 ##の部品を除外する理由は無い
                '.AppendLine("     AND NOT ( SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' ")
                '.AppendLine("        AND AL2.TOP_COLOR_KAITEI_NO IS NULL ")
                '.AppendLine("        AND COLORM.FUKA_NO IS NULL ) ")
                .AppendLine("     AND AL.SOBI_KAITEI_NO = SHIYO.SOBI_KAITEI_NO ")
                '2012/02/15 AL.SOBI_KAITEI_NO＝AL内の最大値（ただし仕様書テーブルの装備改訂Noを超えないものに変更
                '13日の修正では仮に仕様書テーブルの装備改訂Noを持つものがあってもそれより小さい改訂Noも取得されてしまう。
                '2012/02/13 AL.SOBI_KAITEI_NO＝仕様書テーブルの装備改訂Noの最大値ではなく
                '           AL.SOBI_KAITEI_NO＜＝仕様書テーブルの装備改訂Noの最大値に変更
                '           仕様書テーブルの装備改訂Noと一致したものが無い場合、さらに小さいものを探す
                .AppendLine("     AND AL.SOBI_KAITEI_NO = ")
                '.AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 ")
                '.AppendLine("              WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                '.AppendLine("                And SHIYOSHO_SEQNO =  ")
                '.AppendLine("                  RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3)) ")
                .AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("                  WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                 And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                                 And COL_NO = AL.COL_NO ")
                .AppendLine("                                 And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                                 AND SOBI_KAITEI_NO <= (")
                .AppendLine("                                               SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                                       WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                                                         And SHIYOSHO_SEQNO =  ")
                .AppendLine("                                                                         RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3)")
                .AppendLine("                                 )")
                .AppendLine("         )")
                '-----------------------------------------
                .AppendLine(" GROUP BY ")
                .AppendLine("     AL.BLOCK_NO, ")
                .AppendLine("     KA.BU_CODE, ")
                .AppendLine("     AL.F_BUHIN_NO, ")
                .AppendLine("     BASE.SHISAKU_GOUSYA, ")
                .AppendLine("     BLOCKM.TANTO_BUSHO, ")
                .AppendLine("     BLOCK_NAME ")
                .AppendLine(" ORDER BY ")
                .AppendLine("     BLOCK_NO, ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     FF_BUHIN_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of SekkeiBlockAlResultVo)(sql.ToString())
        End Function

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Public Function FindAlByShisakuEventBaseIroAri2(ByVal shisakuEventCode As String, ByVal tantoId As String, Optional ByVal HyojiJunNo As String = "") As List(Of SekkeiBlockAlResultVo) Implements SekkeiBlockDao.FindAlByShisakuEventBase2
            Dim sql As New StringBuilder()
            '★指定された仕様情報№のブロックを抽出。
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT AL.BLOCK_NO, ")
                .AppendLine(" CASE WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+MAX( AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS FF_BUHIN_NO, ")
                .AppendLine(" CASE WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS BF_BUHIN_NO, ")
                .AppendLine(" KA.BU_CODE, ")
                .AppendLine(" MAX(KA.KA_CODE) AS KA_CODE,")
                .AppendLine(" BASE.SHISAKU_GOUSYA, ")
                .AppendLine(" BLOCKM.TANTO_BUSHO AS BUKA_CODE, ")
                .AppendLine(" BLOCK_NAME, ")
                .AppendLine(" MAX(AL2.TOP_COLOR_KAITEI_NO) AS TOP_COLOR_KAITEI_NO, ")
                .AppendLine(" MAX(COLORM.FUKA_NO) AS FUKA_NO, ")
                .AppendLine(" MAX(AL.SOBI_KAITEI_NO) AS SOBI_KAITEI_NO, ")
                .AppendLine(" MAX(SHIYOUSA.KUMIAWASE_CODE_SO ) AS KUMIAWASE_CODE_SO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 AL WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine("     INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_TMP BASE ")
                .AppendLine("         ON AL.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And AL.KATASHIKI_SCD_7 = BASE.BASE_KATASHIKI_SCD_7 ")
                .AppendLine("              And AL.SHIMUKECHI_CODE = BASE.BASE_SHIMUKE ")
                .AppendLine("              And AL.OP_CODE = BASE.BASE_OP ")
                .AppendLine("              AND AL.F_BUHIN_NO <>  '         0     ' ")
                .AppendLine("              AND LEFT(AL.BLOCK_NO,3) <>  '999' ")
                .AppendLine("     INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 SHIYO WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON SHIYO.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And SHIYO.SHIYOSHO_SEQNO = ")
                .AppendLine("                 RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3) ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0800 BUIBUI WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUIBUI.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("             And BUIBUI.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("             And BUIBUI.SHIYOSA_KAITEI_NO = AL.SHIYOSA_KAITEI_NO ")
                .AppendLine("             And BUIBUI.KUMIAWASE_CODE_SO = AL.KUMIAWASE_CODE_SO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0560 SHIYOUSA WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON SHIYOUSA.KAIHATSU_FUGO = BUIBUI.KAIHATSU_FUGO ")
                .AppendLine("             And SHIYOUSA.BUI_CODE = BUIBUI.BUI_CODE ")
                .AppendLine("             And SHIYOUSA.SHIYOSA_KAITEI_NO = BUIBUI.SHIYOSA_KAITEI_NO ")
                .AppendLine("             AND (SHIYOUSA.SHIYOSOBI_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                OR SHIYOUSA.SHIYOSOBI_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("             And SHIYOUSA.KUMIAWASE_CODE_SO = BUIBUI.KUMIAWASE_CODE_SO ")
                .AppendLine("             AND (SHIYOUSA.BGN_SOBI_KAITEI_NO <= AL.SOBI_KAITEI_NO ")
                .AppendLine("             AND SHIYOUSA.END_SOBI_KAITEI_NO >= AL.SOBI_KAITEI_NO) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 AL2 WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON AL2.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("             And AL2.KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("             And AL2.COL_NO = AL.COL_NO ")
                .AppendLine("             And AL2.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("             And AL2.F_BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("             AND (AL2.COLOR_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                 OR AL2.COLOR_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("             AND AL2.TOP_COLOR_KAITEI_NO = (SELECT MAX(TOP_COLOR_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                 WHERE(KAIHATSU_FUGO = AL2.KAIHATSU_FUGO) ")
                .AppendLine("                  AND KATASHIKI_SCD_7 = AL2.KATASHIKI_SCD_7 ")
                .AppendLine("                  AND COL_NO = AL2.COL_NO ")
                .AppendLine("                  AND BLOCK_NO = AL2.BLOCK_NO ")
                .AppendLine("                  AND F_BUHIN_NO = AL2.F_BUHIN_NO ")
                .AppendLine("                  AND COLOR_CODE = AL2.COLOR_CODE ) ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 BUHINM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUHINM.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("         And BUHINM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO ")
                .AppendLine("         And BUIM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And BUIM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON COLORM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("         And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("         And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("         AND (COLORM.COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("            OR COLORM.COLOR_CODE =  BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("         And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                     And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("                                     And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("                                     And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("                                     AND (COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                                         OR COLOR_CODE =  BASE.BASE_NAISOUSYOKU)) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 BLOCKM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.BLOCK_NO_KINO = AL.BLOCK_NO ")
                .AppendLine("         And BLOCKM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         AND LEFT(BLOCKM. BLOCK_NO_KINO, 3) <> '999' ")
                .AppendLine("         And BLOCKM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 BLOCKNAME WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKNAME.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 KA WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.TANTO_BUSHO = KA.KA_RYAKU_NAME ")
                .AppendLine(" WHERE BASE.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' AND ")
                .AppendLine("       BASE.TANTO_ID ='" & tantoId & "' AND ")
                If HyojiJunNo <> "" Then
                    .AppendLine(String.Concat(String.Concat(" BASE.HYOJIJUN_NO = ", HyojiJunNo), " AND "))
                End If
                .AppendLine("     BLOCKM.TANTO_BUSHO IS NOT NULL AND  ")
                .AppendLine("     AL.SHIYOSA_KAITEI_NO = (SELECT MAX(SHIYOSA_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                        And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                        And COL_NO = AL.COL_NO ")
                .AppendLine("                        And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                        AND SOBI_KAITEI_NO = AL.SOBI_KAITEI_NO ) ")
                '2012/07/19 ##かつ色が取れない部品は無理'
                '.AppendLine("     AND NOT ( SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' ")
                '.AppendLine("        AND AL2.TOP_COLOR_KAITEI_NO IS NULL ")
                '.AppendLine("        AND COLORM.FUKA_NO IS NULL ) ")
                ''
                .AppendLine("     AND AL.SOBI_KAITEI_NO = SHIYO.SOBI_KAITEI_NO ")
                '2012/02/15 AL.SOBI_KAITEI_NO＝AL内の最大値（ただし仕様書テーブルの装備改訂Noを超えないものに変更
                '13日の修正では仮に仕様書テーブルの装備改訂Noを持つものがあってもそれより小さい改訂Noも取得されてしまう。
                '2012/02/13 AL.SOBI_KAITEI_NO＝仕様書テーブルの装備改訂Noの最大値ではなく
                '           AL.SOBI_KAITEI_NO＜＝仕様書テーブルの装備改訂Noの最大値に変更
                '           仕様書テーブルの装備改訂Noと一致したものが無い場合、さらに小さいものを探す
                .AppendLine("     AND AL.SOBI_KAITEI_NO = ")
                '.AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 ")
                '.AppendLine("              WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                '.AppendLine("                And SHIYOSHO_SEQNO =  ")
                '.AppendLine("                  RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3)) ")
                .AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("                  WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                 And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                                 And COL_NO = AL.COL_NO ")
                .AppendLine("                                 And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                                 AND SOBI_KAITEI_NO <= (")
                .AppendLine("                                               SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                                       WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                                                         And SHIYOSHO_SEQNO =  ")
                .AppendLine("                                                                         RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3)")
                .AppendLine("                                 )")
                .AppendLine("         )")
                '-----------------------------------------
                .AppendLine(" GROUP BY ")
                .AppendLine("     AL.BLOCK_NO, ")
                .AppendLine("     KA.BU_CODE, ")
                .AppendLine("     AL.F_BUHIN_NO, ")
                .AppendLine("     BASE.SHISAKU_GOUSYA, ")
                .AppendLine("     BLOCKM.TANTO_BUSHO, ")
                .AppendLine("     BLOCK_NAME ")
                .AppendLine(" ORDER BY ")
                .AppendLine("     BLOCK_NO, ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     FF_BUHIN_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of SekkeiBlockAlResultVo)(sql.ToString())
        End Function
        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="BlockNo">ブロックNo</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Public Function FindAlByShisakuEventBaseIroAri2ByBlock(ByVal shisakuEventCode As String, ByVal tantoId As String, ByVal BlockNo As String) As List(Of SekkeiBlockAlResultVo) Implements SekkeiBlockDao.FindAlByShisakuEventBase2ByBlock
            Dim sql As New StringBuilder()
            '★指定された仕様情報№のブロックを抽出。
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT AL.BLOCK_NO, ")
                '2012/02/16 色の有無だけではなく、桁も参照するようにしてみる
                .AppendLine(" CASE WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL) OR NOT SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+MAX( AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS FF_BUHIN_NO, ")
                .AppendLine(" CASE WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL) OR NOT SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS BF_BUHIN_NO, ")
                .AppendLine(" KA.BU_CODE, ")
                .AppendLine(" MAX(KA.KA_CODE) AS KA_CODE,")
                .AppendLine(" BASE.SHISAKU_GOUSYA, ")
                .AppendLine(" BLOCKM.TANTO_BUSHO AS BUKA_CODE, ")
                .AppendLine(" BLOCK_NAME, ")
                .AppendLine(" MAX(AL2.TOP_COLOR_KAITEI_NO) AS TOP_COLOR_KAITEI_NO, ")
                .AppendLine(" MAX(COLORM.FUKA_NO) AS FUKA_NO, ")
                .AppendLine(" MAX(AL.SOBI_KAITEI_NO) AS SOBI_KAITEI_NO, ")
                .AppendLine(" MAX(SHIYOUSA.KUMIAWASE_CODE_SO ) AS KUMIAWASE_CODE_SO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 AL WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine("     INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_TMP BASE ")
                .AppendLine("         ON AL.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And AL.KATASHIKI_SCD_7 = BASE.BASE_KATASHIKI_SCD_7 ")
                .AppendLine("              And AL.SHIMUKECHI_CODE = BASE.BASE_SHIMUKE ")
                .AppendLine("              And AL.OP_CODE = BASE.BASE_OP ")
                .AppendLine("              AND AL.F_BUHIN_NO <>  '         0     ' ")
                .AppendLine("              AND LEFT(AL.BLOCK_NO,3) <>  '999' ")
                .AppendLine("     INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 SHIYO WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON SHIYO.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And SHIYO.SHIYOSHO_SEQNO = ")
                .AppendLine("                 RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3) ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0800 BUIBUI WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUIBUI.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("             And BUIBUI.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("             And BUIBUI.SHIYOSA_KAITEI_NO = AL.SHIYOSA_KAITEI_NO ")
                .AppendLine("             And BUIBUI.KUMIAWASE_CODE_SO = AL.KUMIAWASE_CODE_SO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0560 SHIYOUSA WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON SHIYOUSA.KAIHATSU_FUGO = BUIBUI.KAIHATSU_FUGO ")
                .AppendLine("             And SHIYOUSA.BUI_CODE = BUIBUI.BUI_CODE ")
                .AppendLine("             And SHIYOUSA.SHIYOSA_KAITEI_NO = BUIBUI.SHIYOSA_KAITEI_NO ")
                .AppendLine("             AND (SHIYOUSA.SHIYOSOBI_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                OR SHIYOUSA.SHIYOSOBI_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("             And SHIYOUSA.KUMIAWASE_CODE_SO = BUIBUI.KUMIAWASE_CODE_SO ")
                .AppendLine("             AND (SHIYOUSA.BGN_SOBI_KAITEI_NO <= AL.SOBI_KAITEI_NO ")
                .AppendLine("             AND SHIYOUSA.END_SOBI_KAITEI_NO >= AL.SOBI_KAITEI_NO) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 AL2 WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON AL2.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("             And AL2.KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("             And AL2.COL_NO = AL.COL_NO ")
                .AppendLine("             And AL2.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("             And AL2.F_BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("             AND (AL2.COLOR_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                 OR AL2.COLOR_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("             AND AL2.TOP_COLOR_KAITEI_NO = (SELECT MAX(TOP_COLOR_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                 WHERE(KAIHATSU_FUGO = AL2.KAIHATSU_FUGO) ")
                .AppendLine("                  AND KATASHIKI_SCD_7 = AL2.KATASHIKI_SCD_7 ")
                .AppendLine("                  AND COL_NO = AL2.COL_NO ")
                .AppendLine("                  AND BLOCK_NO = AL2.BLOCK_NO ")
                .AppendLine("                  AND F_BUHIN_NO = AL2.F_BUHIN_NO ")
                .AppendLine("                  AND COLOR_CODE = AL2.COLOR_CODE ) ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 BUHINM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUHINM.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("         And BUHINM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO ")
                .AppendLine("         And BUIM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And BUIM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON COLORM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("         And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("         And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("         		 AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine("         AND (COLORM.COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("            OR COLORM.COLOR_CODE =  BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("         And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                     And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("                                     And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("                                     And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("                                     AND KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine("                                     AND (COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                                         OR COLOR_CODE =  BASE.BASE_NAISOUSYOKU)) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 BLOCKM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.BLOCK_NO_KINO = AL.BLOCK_NO ")
                .AppendLine("         And BLOCKM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         AND LEFT(BLOCKM. BLOCK_NO_KINO, 3) <> '999' ")
                .AppendLine("         And BLOCKM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 BLOCKNAME WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKNAME.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 KA ")
                .AppendLine("         ON BLOCKM.TANTO_BUSHO = KA.KA_RYAKU_NAME ")
                .AppendLine(" WHERE BASE.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' AND ")
                .AppendLine("       BASE.TANTO_ID ='" & tantoId & "' AND ")
                .AppendLine(String.Concat(String.Concat(" AL.BLOCK_NO = '", BlockNo), "' AND "))
                .AppendLine("     BLOCKM.TANTO_BUSHO IS NOT NULL AND  ")
                .AppendLine("     AL.SHIYOSA_KAITEI_NO = (SELECT MAX(SHIYOSA_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                        And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                        And COL_NO = AL.COL_NO ")
                .AppendLine("                        And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                        AND SOBI_KAITEI_NO = AL.SOBI_KAITEI_NO ) ")
                '.AppendLine("     AND NOT ( SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' ")
                '.AppendLine("        AND AL2.TOP_COLOR_KAITEI_NO IS NULL ")
                '.AppendLine("        AND COLORM.FUKA_NO IS NULL ) ")
                .AppendLine("     AND AL.SOBI_KAITEI_NO = SHIYO.SOBI_KAITEI_NO ")
                '2012/02/15 AL.SOBI_KAITEI_NO＝AL内の最大値（ただし仕様書テーブルの装備改訂Noを超えないものに変更
                '13日の修正では仮に仕様書テーブルの装備改訂Noを持つものがあってもそれより小さい改訂Noも取得されてしまう。
                '2012/02/13 AL.SOBI_KAITEI_NO＝仕様書テーブルの装備改訂Noの最大値ではなく
                '           AL.SOBI_KAITEI_NO＜＝仕様書テーブルの装備改訂Noの最大値に変更
                '           仕様書テーブルの装備改訂Noと一致したものが無い場合、さらに小さいものを探す
                .AppendLine("     AND AL.SOBI_KAITEI_NO = ")
                '.AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 ")
                '.AppendLine("              WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                '.AppendLine("                And SHIYOSHO_SEQNO =  ")
                '.AppendLine("                  RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3)) ")
                .AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("                  WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                 And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                                 And COL_NO = AL.COL_NO ")
                .AppendLine("                                 And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                                 AND SOBI_KAITEI_NO <= (")
                .AppendLine("                                               SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                                       WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                                                         And SHIYOSHO_SEQNO =  ")
                .AppendLine("                                                                         RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3)")
                .AppendLine("                                 )")
                .AppendLine("         )")
                '-----------------------------------------
                .AppendLine(" GROUP BY ")
                .AppendLine("     AL.BLOCK_NO, ")
                .AppendLine("     KA.BU_CODE, ")
                .AppendLine("     AL.F_BUHIN_NO, ")
                .AppendLine("     BASE.SHISAKU_GOUSYA, ")
                .AppendLine("     BLOCKM.TANTO_BUSHO, ")
                .AppendLine("     BLOCK_NAME ")
                .AppendLine(" ORDER BY ")
                .AppendLine("     BLOCK_NO, ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     FF_BUHIN_NO ")
            End With


            Dim db As New EBomDbClient

            Return db.QueryForList(Of SekkeiBlockAlResultVo)(sql.ToString())
        End Function

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' 　　ベース車情報に指定した仕様情報№－１で抽出する。
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Public Function FindAlByShisakuEventBaseIroAriOLD(ByVal shisakuEventCode As String, ByVal tantoId As String, Optional ByVal HyojiJunNo As String = "") As List(Of SekkeiBlockAlResultVo) Implements SekkeiBlockDao.FindAlByShisakuEventBaseOLD
            Dim sql As New StringBuilder()
            '★指定された仕様情報№の-1のブロックを抽出。
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT AL.BLOCK_NO, ")
                .AppendLine(" CASE WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+MAX( AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS FF_BUHIN_NO, ")
                .AppendLine(" CASE WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS BF_BUHIN_NO, ")
                .AppendLine(" KA.BU_CODE, ")
                .AppendLine(" MAX(KA.KA_CODE) AS KA_CODE,")
                .AppendLine(" BASE.SHISAKU_GOUSYA, ")
                .AppendLine(" BLOCKM.TANTO_BUSHO AS BUKA_CODE, ")
                .AppendLine(" BLOCK_NAME, ")
                .AppendLine(" MAX(AL2.TOP_COLOR_KAITEI_NO) AS TOP_COLOR_KAITEI_NO, ")
                .AppendLine(" MAX(COLORM.FUKA_NO) AS FUKA_NO, ")
                .AppendLine(" MAX(AL.SOBI_KAITEI_NO) AS SOBI_KAITEI_NO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 AL WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine("     INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_TMP BASE ")
                .AppendLine("         ON AL.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And AL.KATASHIKI_SCD_7 = BASE.BASE_KATASHIKI_SCD_7 ")
                .AppendLine("              And AL.SHIMUKECHI_CODE = BASE.BASE_SHIMUKE ")
                .AppendLine("              And AL.OP_CODE = BASE.BASE_OP ")
                .AppendLine("              AND AL.F_BUHIN_NO <>  '         0     ' ")
                .AppendLine("              AND LEFT(AL.BLOCK_NO,3) <>  '999' ")
                .AppendLine("     INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 SHIYO WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON SHIYO.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And SHIYO.SHIYOSHO_SEQNO = ")
                .AppendLine("                 RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-1 AS VarChar),3) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 AL2 WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON AL2.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("             And AL2.KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("             And AL2.COL_NO = AL.COL_NO ")
                .AppendLine("             And AL2.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("             And AL2.F_BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("             AND (AL2.COLOR_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                 OR AL2.COLOR_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("             AND AL2.TOP_COLOR_KAITEI_NO = (SELECT MAX(TOP_COLOR_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 ")
                .AppendLine("                 WHERE(KAIHATSU_FUGO = AL2.KAIHATSU_FUGO) ")
                .AppendLine("                  AND KATASHIKI_SCD_7 = AL2.KATASHIKI_SCD_7 ")
                .AppendLine("                  AND COL_NO = AL2.COL_NO ")
                .AppendLine("                  AND BLOCK_NO = AL2.BLOCK_NO ")
                .AppendLine("                  AND F_BUHIN_NO = AL2.F_BUHIN_NO ")
                .AppendLine("                  AND COLOR_CODE = AL2.COLOR_CODE ) ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 BUHINM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUHINM.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("         And BUHINM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO ")
                .AppendLine("         And BUIM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And BUIM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON COLORM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("         And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("         And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("         AND (COLORM.COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("            OR COLORM.COLOR_CODE =  BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("         And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                     And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("                                     And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("                                     And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("                                     AND (COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                                         OR COLOR_CODE =  BASE.BASE_NAISOUSYOKU)) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 BLOCKM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.BLOCK_NO_KINO = AL.BLOCK_NO ")
                .AppendLine("         And BLOCKM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         AND LEFT(BLOCKM. BLOCK_NO_KINO, 3) <> '999' ")
                .AppendLine("         And BLOCKM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 BLOCKNAME WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKNAME.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 KA WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.TANTO_BUSHO = KA.KA_RYAKU_NAME ")
                .AppendLine(" WHERE BASE.SHISAKU_EVENT_CODE = @Value AND ")
                .AppendLine("       BASE.TANTO_ID ='" & tantoId & "' AND ")
                If HyojiJunNo <> "" Then
                    .AppendLine(String.Concat(String.Concat(" BASE.HYOJIJUN_NO = ", HyojiJunNo), " AND "))
                End If
                .AppendLine("     BLOCKM.TANTO_BUSHO IS NOT NULL AND  ")
                .AppendLine("     AL.SHIYOSA_KAITEI_NO = (SELECT MAX(SHIYOSA_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                        And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                        And COL_NO = AL.COL_NO ")
                .AppendLine("                        And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                        AND SOBI_KAITEI_NO = AL.SOBI_KAITEI_NO ) ")
                '2012/07/19 色なしも取得'
                '.AppendLine("     AND NOT ( SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' ")
                '.AppendLine("        AND AL2.TOP_COLOR_KAITEI_NO IS NULL ")
                '.AppendLine("        AND COLORM.FUKA_NO IS NULL ) ")
                .AppendLine("     AND AL.SOBI_KAITEI_NO = SHIYO.SOBI_KAITEI_NO ")
                '2012/02/15 AL.SOBI_KAITEI_NO＝AL内の最大値（ただし仕様書テーブルの装備改訂Noを超えないものに変更
                '13日の修正では仮に仕様書テーブルの装備改訂Noを持つものがあってもそれより小さい改訂Noも取得されてしまう。
                '2012/02/13 AL.SOBI_KAITEI_NO＝仕様書テーブルの装備改訂Noの最大値ではなく
                '           AL.SOBI_KAITEI_NO＜＝仕様書テーブルの装備改訂Noの最大値に変更
                '           仕様書テーブルの装備改訂Noと一致したものが無い場合、さらに小さいものを探す
                .AppendLine("     AND AL.SOBI_KAITEI_NO = ")
                '.AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 ")
                '.AppendLine("              WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                '.AppendLine("                And SHIYOSHO_SEQNO =  ")
                '.AppendLine("                  RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-1 AS VarChar),3)) ")
                .AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("                  WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                 And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                                 And COL_NO = AL.COL_NO ")
                .AppendLine("                                 And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                                 AND SOBI_KAITEI_NO <= (")
                .AppendLine("                                               SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                                       WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                                                         And SHIYOSHO_SEQNO =  ")
                .AppendLine("                                                                         RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3)")
                .AppendLine("                                 )")
                .AppendLine("         )")
                '-----------------------------------------
                .AppendLine(" GROUP BY ")
                .AppendLine("     AL.BLOCK_NO, ")
                .AppendLine("     KA.BU_CODE, ")
                .AppendLine("     AL.F_BUHIN_NO, ")
                .AppendLine("     BASE.SHISAKU_GOUSYA, ")
                .AppendLine("     BLOCKM.TANTO_BUSHO, ")
                .AppendLine("     BLOCK_NAME ")
                .AppendLine(" ORDER BY ")
                .AppendLine("     BLOCK_NO, ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     FF_BUHIN_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of SekkeiBlockAlResultVo)(sql.ToString(), shisakuEventCode)
        End Function
        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' 　　ベース車情報に指定した仕様情報№－１で抽出する。
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Public Function FindAlByShisakuEventBaseIroAriOLDByBlock(ByVal shisakuEventCode As String, ByVal tantoId As String, ByVal blockNo As String) As List(Of SekkeiBlockAlResultVo) Implements SekkeiBlockDao.FindAlByShisakuEventBaseOLDByBlock
            Dim sql As New StringBuilder()
            '★指定された仕様情報№の-1のブロックを抽出。
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT AL.BLOCK_NO, ")
                '2012/02/16 色の有無だけではなく、桁も参照するようにしてみる
                .AppendLine(" CASE WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL) OR NOT SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+MAX( AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS FF_BUHIN_NO, ")
                .AppendLine(" CASE WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL) OR NOT SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS BF_BUHIN_NO, ")
                .AppendLine(" KA.BU_CODE, ")
                .AppendLine(" MAX(KA.KA_CODE) AS KA_CODE,")
                .AppendLine(" BASE.SHISAKU_GOUSYA, ")
                .AppendLine(" BLOCKM.TANTO_BUSHO AS BUKA_CODE, ")
                .AppendLine(" BLOCK_NAME, ")
                .AppendLine(" MAX(AL2.TOP_COLOR_KAITEI_NO) AS TOP_COLOR_KAITEI_NO, ")
                .AppendLine(" MAX(COLORM.FUKA_NO) AS FUKA_NO, ")
                .AppendLine(" MAX(AL.SOBI_KAITEI_NO) AS SOBI_KAITEI_NO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 AL WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine("     INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_TMP BASE ")
                .AppendLine("         ON AL.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And AL.KATASHIKI_SCD_7 = BASE.BASE_KATASHIKI_SCD_7 ")
                .AppendLine("              And AL.SHIMUKECHI_CODE = BASE.BASE_SHIMUKE ")
                .AppendLine("              And AL.OP_CODE = BASE.BASE_OP ")
                .AppendLine("              AND AL.F_BUHIN_NO <>  '         0     ' ")
                .AppendLine("              AND LEFT(AL.BLOCK_NO,3) <>  '999' ")
                .AppendLine("     INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 SHIYO WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON SHIYO.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And SHIYO.SHIYOSHO_SEQNO = ")
                .AppendLine("                 RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-1 AS VarChar),3) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 AL2 WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON AL2.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("             And AL2.KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("             And AL2.COL_NO = AL.COL_NO ")
                .AppendLine("             And AL2.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("             And AL2.F_BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("             AND (AL2.COLOR_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                 OR AL2.COLOR_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("             AND AL2.TOP_COLOR_KAITEI_NO = (SELECT MAX(TOP_COLOR_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                 WHERE(KAIHATSU_FUGO = AL2.KAIHATSU_FUGO) ")
                .AppendLine("                  AND KATASHIKI_SCD_7 = AL2.KATASHIKI_SCD_7 ")
                .AppendLine("                  AND COL_NO = AL2.COL_NO ")
                .AppendLine("                  AND BLOCK_NO = AL2.BLOCK_NO ")
                .AppendLine("                  AND F_BUHIN_NO = AL2.F_BUHIN_NO ")
                .AppendLine("                  AND COLOR_CODE = AL2.COLOR_CODE ) ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 BUHINM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUHINM.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("         And BUHINM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO ")
                .AppendLine("         And BUIM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And BUIM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON COLORM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("         And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("         And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine(" 		  AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine("         AND (COLORM.COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("            OR COLORM.COLOR_CODE =  BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("         And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                     And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("                                     And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("                                     And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine(" 		                              AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine("                                     AND (COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                                         OR COLOR_CODE =  BASE.BASE_NAISOUSYOKU)) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 BLOCKM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.BLOCK_NO_KINO = AL.BLOCK_NO ")
                .AppendLine("         And BLOCKM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         AND LEFT(BLOCKM. BLOCK_NO_KINO, 3) <> '999' ")
                .AppendLine("         And BLOCKM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 BLOCKNAME WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKNAME.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 KA WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.TANTO_BUSHO = KA.KA_RYAKU_NAME ")
                .AppendLine(" WHERE BASE.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' AND ")
                .AppendLine("       BASE.TANTO_ID ='" & tantoId & "' AND ")
                .AppendLine(String.Concat(String.Concat(" AL.BLOCK_NO = '", blockNo), "' AND "))
                .AppendLine("     BLOCKM.TANTO_BUSHO IS NOT NULL AND  ")
                .AppendLine("     AL.SHIYOSA_KAITEI_NO = (SELECT MAX(SHIYOSA_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                        And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                        And COL_NO = AL.COL_NO ")
                .AppendLine("                        And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                        AND SOBI_KAITEI_NO = AL.SOBI_KAITEI_NO ) ")
                '.AppendLine("     AND NOT ( SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' ")
                '.AppendLine("        AND AL2.TOP_COLOR_KAITEI_NO IS NULL ")
                '.AppendLine("        AND COLORM.FUKA_NO IS NULL ) ")
                .AppendLine("     AND AL.SOBI_KAITEI_NO = SHIYO.SOBI_KAITEI_NO ")
                '2012/02/15 AL.SOBI_KAITEI_NO＝AL内の最大値（ただし仕様書テーブルの装備改訂Noを超えないものに変更
                '13日の修正では仮に仕様書テーブルの装備改訂Noを持つものがあってもそれより小さい改訂Noも取得されてしまう。
                '2012/02/13 AL.SOBI_KAITEI_NO＝仕様書テーブルの装備改訂Noの最大値ではなく
                '           AL.SOBI_KAITEI_NO＜＝仕様書テーブルの装備改訂Noの最大値に変更
                '           仕様書テーブルの装備改訂Noと一致したものが無い場合、さらに小さいものを探す
                .AppendLine("     AND AL.SOBI_KAITEI_NO = ")
                '.AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 ")
                '.AppendLine("              WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                '.AppendLine("                And SHIYOSHO_SEQNO =  ")
                '.AppendLine("                  RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-1 AS VarChar),3)) ")
                .AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("                  WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                 And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                                 And COL_NO = AL.COL_NO ")
                .AppendLine("                                 And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                                 AND SOBI_KAITEI_NO <= (")
                .AppendLine("                                               SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                                       WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                                                         And SHIYOSHO_SEQNO =  ")
                .AppendLine("                                                                         RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3)")
                .AppendLine("                                 )")
                .AppendLine("         )")
                '-----------------------------------------
                .AppendLine(" GROUP BY ")
                .AppendLine("     AL.BLOCK_NO, ")
                .AppendLine("     KA.BU_CODE, ")
                .AppendLine("     AL.F_BUHIN_NO, ")
                .AppendLine("     BASE.SHISAKU_GOUSYA, ")
                .AppendLine("     BLOCKM.TANTO_BUSHO, ")
                .AppendLine("     BLOCK_NAME ")
                .AppendLine(" ORDER BY ")
                .AppendLine("     BLOCK_NO, ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     FF_BUHIN_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of SekkeiBlockAlResultVo)(sql.ToString())
        End Function

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' 　　ベース車情報に指定した仕様情報№－１で抽出する。
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Public Function FindAlByShisakuEventBaseIroAriOLD2(ByVal shisakuEventCode As String, ByVal tantoId As String, Optional ByVal HyojiJunNo As String = "") As List(Of SekkeiBlockAlResultVo) Implements SekkeiBlockDao.FindAlByShisakuEventBaseOLD2
            Dim sql As New StringBuilder()
            '★指定された仕様情報№の-1のブロックを抽出。
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT AL.BLOCK_NO, ")
                .AppendLine(" CASE WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+MAX( AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS FF_BUHIN_NO, ")
                .AppendLine(" CASE WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS BF_BUHIN_NO, ")
                .AppendLine(" KA.BU_CODE, ")
                .AppendLine(" MAX(KA.KA_CODE) AS KA_CODE,")
                .AppendLine(" BASE.SHISAKU_GOUSYA, ")
                .AppendLine(" BLOCKM.TANTO_BUSHO AS BUKA_CODE, ")
                .AppendLine(" BLOCK_NAME, ")
                .AppendLine(" MAX(AL2.TOP_COLOR_KAITEI_NO) AS TOP_COLOR_KAITEI_NO, ")
                .AppendLine(" MAX(COLORM.FUKA_NO) AS FUKA_NO, ")
                .AppendLine(" MAX(AL.SOBI_KAITEI_NO) AS SOBI_KAITEI_NO, ")
                .AppendLine(" MAX(SHIYOUSA.KUMIAWASE_CODE_SO ) AS KUMIAWASE_CODE_SO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 AL WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine("     INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_TMP BASE ")
                .AppendLine("         ON AL.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And AL.KATASHIKI_SCD_7 = BASE.BASE_KATASHIKI_SCD_7 ")
                .AppendLine("              And AL.SHIMUKECHI_CODE = BASE.BASE_SHIMUKE ")
                .AppendLine("              And AL.OP_CODE = BASE.BASE_OP ")
                .AppendLine("              AND AL.F_BUHIN_NO <>  '         0     ' ")
                .AppendLine("              AND LEFT(AL.BLOCK_NO,3) <>  '999' ")
                .AppendLine("     INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 SHIYO WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON SHIYO.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And SHIYO.SHIYOSHO_SEQNO = ")
                .AppendLine("                 RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-1 AS VarChar),3) ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0800 BUIBUI WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUIBUI.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("             And BUIBUI.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("             And BUIBUI.SHIYOSA_KAITEI_NO = AL.SHIYOSA_KAITEI_NO ")
                .AppendLine("             And BUIBUI.KUMIAWASE_CODE_SO = AL.KUMIAWASE_CODE_SO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0560 SHIYOUSA WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON SHIYOUSA.KAIHATSU_FUGO = BUIBUI.KAIHATSU_FUGO ")
                .AppendLine("             And SHIYOUSA.BUI_CODE = BUIBUI.BUI_CODE ")
                .AppendLine("             And SHIYOUSA.SHIYOSA_KAITEI_NO = BUIBUI.SHIYOSA_KAITEI_NO ")
                .AppendLine("             AND (SHIYOUSA.SHIYOSOBI_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                OR SHIYOUSA.SHIYOSOBI_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("             And SHIYOUSA.KUMIAWASE_CODE_SO = BUIBUI.KUMIAWASE_CODE_SO ")
                .AppendLine("             AND (SHIYOUSA.BGN_SOBI_KAITEI_NO <= AL.SOBI_KAITEI_NO ")
                .AppendLine("             AND SHIYOUSA.END_SOBI_KAITEI_NO >= AL.SOBI_KAITEI_NO) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 AL2 WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON AL2.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("             And AL2.KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("             And AL2.COL_NO = AL.COL_NO ")
                .AppendLine("             And AL2.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("             And AL2.F_BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("             AND (AL2.COLOR_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                 OR AL2.COLOR_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("             AND AL2.TOP_COLOR_KAITEI_NO = (SELECT MAX(TOP_COLOR_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 ")
                .AppendLine("                 WHERE(KAIHATSU_FUGO = AL2.KAIHATSU_FUGO) ")
                .AppendLine("                  AND KATASHIKI_SCD_7 = AL2.KATASHIKI_SCD_7 ")
                .AppendLine("                  AND COL_NO = AL2.COL_NO ")
                .AppendLine("                  AND BLOCK_NO = AL2.BLOCK_NO ")
                .AppendLine("                  AND F_BUHIN_NO = AL2.F_BUHIN_NO ")
                .AppendLine("                  AND COLOR_CODE = AL2.COLOR_CODE ) ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 BUHINM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUHINM.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("         And BUHINM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO ")
                .AppendLine("         And BUIM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And BUIM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON COLORM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("         And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("         And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("         AND (COLORM.COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("            OR COLORM.COLOR_CODE =  BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("         And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                     And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("                                     And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("                                     And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("                                     AND (COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                                         OR COLOR_CODE =  BASE.BASE_NAISOUSYOKU)) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 BLOCKM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.BLOCK_NO_KINO = AL.BLOCK_NO ")
                .AppendLine("         And BLOCKM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         AND LEFT(BLOCKM. BLOCK_NO_KINO, 3) <> '999' ")
                .AppendLine("         And BLOCKM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 BLOCKNAME WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKNAME.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 KA WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.TANTO_BUSHO = KA.KA_RYAKU_NAME ")
                .AppendLine(" WHERE BASE.SHISAKU_EVENT_CODE = @Value AND ")
                .AppendLine("       BASE.TANTO_ID ='" & tantoId & "' AND ")
                If HyojiJunNo <> "" Then
                    .AppendLine(String.Concat(String.Concat(" BASE.HYOJIJUN_NO = ", HyojiJunNo), " AND "))
                End If
                .AppendLine("     BLOCKM.TANTO_BUSHO IS NOT NULL AND  ")
                .AppendLine("     AL.SHIYOSA_KAITEI_NO = (SELECT MAX(SHIYOSA_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                        And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                        And COL_NO = AL.COL_NO ")
                .AppendLine("                        And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                        AND SOBI_KAITEI_NO = AL.SOBI_KAITEI_NO ) ")
                '2012/07/19 色なしでも取得する'
                '.AppendLine("     AND NOT ( SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' ")
                '.AppendLine("        AND AL2.TOP_COLOR_KAITEI_NO IS NULL ")
                '.AppendLine("        AND COLORM.FUKA_NO IS NULL ) ")

                .AppendLine("     AND AL.SOBI_KAITEI_NO = SHIYO.SOBI_KAITEI_NO ")
                '2012/02/15 AL.SOBI_KAITEI_NO＝AL内の最大値（ただし仕様書テーブルの装備改訂Noを超えないものに変更
                '13日の修正では仮に仕様書テーブルの装備改訂Noを持つものがあってもそれより小さい改訂Noも取得されてしまう。
                '2012/02/13 AL.SOBI_KAITEI_NO＝仕様書テーブルの装備改訂Noの最大値ではなく
                '           AL.SOBI_KAITEI_NO＜＝仕様書テーブルの装備改訂Noの最大値に変更
                '           仕様書テーブルの装備改訂Noと一致したものが無い場合、さらに小さいものを探す
                .AppendLine("     AND AL.SOBI_KAITEI_NO = ")
                '.AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 ")
                '.AppendLine("              WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                '.AppendLine("                And SHIYOSHO_SEQNO =  ")
                '.AppendLine("                  RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-1 AS VarChar),3)) ")
                .AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("                  WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                 And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                                 And COL_NO = AL.COL_NO ")
                .AppendLine("                                 And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                                 AND SOBI_KAITEI_NO <= (")
                .AppendLine("                                               SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                                       WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                                                         And SHIYOSHO_SEQNO =  ")
                .AppendLine("                                                                         RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3)")
                .AppendLine("                                 )")
                .AppendLine("         )")
                '-----------------------------------------
                .AppendLine(" GROUP BY ")
                .AppendLine("     AL.BLOCK_NO, ")
                .AppendLine("     KA.BU_CODE, ")
                .AppendLine("     AL.F_BUHIN_NO, ")
                .AppendLine("     BASE.SHISAKU_GOUSYA, ")
                .AppendLine("     BLOCKM.TANTO_BUSHO, ")
                .AppendLine("     BLOCK_NAME ")
                .AppendLine(" ORDER BY ")
                .AppendLine("     BLOCK_NO, ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     FF_BUHIN_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of SekkeiBlockAlResultVo)(sql.ToString(), shisakuEventCode)
        End Function
        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' 　　ベース車情報に指定した仕様情報№－１で抽出する。
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Public Function FindAlByShisakuEventBaseIroAriOLD2ByBlock(ByVal shisakuEventCode As String, ByVal tantoId As String, ByVal blockNo As String) As List(Of SekkeiBlockAlResultVo) Implements SekkeiBlockDao.FindAlByShisakuEventBaseOLD2ByBlock
            Dim sql As New StringBuilder()
            '★指定された仕様情報№の-1のブロックを抽出。
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT AL.BLOCK_NO, ")
                '2012/02/16 色の有無だけではなく、桁も参照するようにしてみる
                .AppendLine(" CASE WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL) OR NOT SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+MAX( AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS FF_BUHIN_NO, ")
                .AppendLine(" CASE WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NULL) OR NOT SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(COLORM.FUKA_NO) ")
                .AppendLine("      WHEN (MAX(AL2.FUKA_NO) IS NOT NULL AND MAX(COLORM.FUKA_NO) IS NOT NULL) AND SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' THEN LEFT(AL.F_BUHIN_NO,10)+ MAX(AL2.FUKA_NO) ")
                .AppendLine(" End ")
                .AppendLine(" AS BF_BUHIN_NO, ")
                .AppendLine(" KA.BU_CODE, ")
                .AppendLine(" MAX(KA.KA_CODE) AS KA_CODE,")
                .AppendLine(" BASE.SHISAKU_GOUSYA, ")
                .AppendLine(" BLOCKM.TANTO_BUSHO AS BUKA_CODE, ")
                .AppendLine(" BLOCK_NAME, ")
                .AppendLine(" MAX(AL2.TOP_COLOR_KAITEI_NO) AS TOP_COLOR_KAITEI_NO, ")
                .AppendLine(" MAX(COLORM.FUKA_NO) AS FUKA_NO, ")
                .AppendLine(" MAX(AL.SOBI_KAITEI_NO) AS SOBI_KAITEI_NO, ")
                .AppendLine(" MAX(SHIYOUSA.KUMIAWASE_CODE_SO ) AS KUMIAWASE_CODE_SO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 AL WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine("     INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_TMP BASE ")
                .AppendLine("         ON AL.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And AL.KATASHIKI_SCD_7 = BASE.BASE_KATASHIKI_SCD_7 ")
                .AppendLine("              And AL.SHIMUKECHI_CODE = BASE.BASE_SHIMUKE ")
                .AppendLine("              And AL.OP_CODE = BASE.BASE_OP ")
                .AppendLine("              AND AL.F_BUHIN_NO <>  '         0     ' ")
                .AppendLine("              AND LEFT(AL.BLOCK_NO,3) <>  '999' ")
                .AppendLine("     INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 SHIYO WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON SHIYO.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("              And SHIYO.SHIYOSHO_SEQNO = ")
                .AppendLine("                 RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-1 AS VarChar),3) ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0800 BUIBUI WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUIBUI.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("             And BUIBUI.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("             And BUIBUI.SHIYOSA_KAITEI_NO = AL.SHIYOSA_KAITEI_NO ")
                .AppendLine("             And BUIBUI.KUMIAWASE_CODE_SO = AL.KUMIAWASE_CODE_SO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0560 SHIYOUSA WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON SHIYOUSA.KAIHATSU_FUGO = BUIBUI.KAIHATSU_FUGO ")
                .AppendLine("             And SHIYOUSA.BUI_CODE = BUIBUI.BUI_CODE ")
                .AppendLine("             And SHIYOUSA.SHIYOSA_KAITEI_NO = BUIBUI.SHIYOSA_KAITEI_NO ")
                .AppendLine("             AND (SHIYOUSA.SHIYOSOBI_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                OR SHIYOUSA.SHIYOSOBI_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("             And SHIYOUSA.KUMIAWASE_CODE_SO = BUIBUI.KUMIAWASE_CODE_SO ")
                .AppendLine("             AND (SHIYOUSA.BGN_SOBI_KAITEI_NO <= AL.SOBI_KAITEI_NO ")
                .AppendLine("             AND SHIYOUSA.END_SOBI_KAITEI_NO >= AL.SOBI_KAITEI_NO) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 AL2 WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON AL2.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("             And AL2.KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("             And AL2.COL_NO = AL.COL_NO ")
                .AppendLine("             And AL2.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("             And AL2.F_BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("             AND (AL2.COLOR_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                 OR AL2.COLOR_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("             AND AL2.TOP_COLOR_KAITEI_NO = (SELECT MAX(TOP_COLOR_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 ")
                .AppendLine("                 WHERE(KAIHATSU_FUGO = AL2.KAIHATSU_FUGO) ")
                .AppendLine("                  AND KATASHIKI_SCD_7 = AL2.KATASHIKI_SCD_7 ")
                .AppendLine("                  AND COL_NO = AL2.COL_NO ")
                .AppendLine("                  AND BLOCK_NO = AL2.BLOCK_NO ")
                .AppendLine("                  AND F_BUHIN_NO = AL2.F_BUHIN_NO ")
                .AppendLine("                  AND COLOR_CODE = AL2.COLOR_CODE ) ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0532 BUHINM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUHINM.BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("         And BUHINM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 BUIM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BUIM.ZUMEN_NO = BUHINM.ZUMEN_NO ")
                .AppendLine("         And BUIM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And BUIM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLORM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON COLORM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And COLORM.COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("         And COLORM.DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("         And COLORM.DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("		  AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE")
                .AppendLine("         AND (COLORM.COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("            OR COLORM.COLOR_CODE =  BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("         And COLORM.DESIGN_GROUP_KAITEI_NO = (SELECT MAX(DESIGN_GROUP_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                     And COLOR_SETTEI_KBN = BUIM.COLOR_SETTEI_KBN ")
                .AppendLine("                                     And DESIGN_GROUP_CODE = BUIM.DESIGN_GROUP_CODE ")
                .AppendLine("                                     And DESIGN_BUI_CODE = BUIM.DESIGN_BUI_CODE ")
                .AppendLine("                   		          AND COLORM.KYOTEN_CODE = BUIM.KYOTEN_CODE ")
                .AppendLine("                                     AND (COLOR_CODE =  BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                                         OR COLOR_CODE =  BASE.BASE_NAISOUSYOKU)) ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 BLOCKM WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.BLOCK_NO_KINO = AL.BLOCK_NO ")
                .AppendLine("         And BLOCKM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         AND LEFT(BLOCKM. BLOCK_NO_KINO, 3) <> '999' ")
                .AppendLine("         And BLOCKM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 BLOCKNAME WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKNAME.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 KA WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON BLOCKM.TANTO_BUSHO = KA.KA_RYAKU_NAME ")
                .AppendLine(" WHERE BASE.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' AND ")
                .AppendLine("       BASE.TANTO_ID ='" & tantoId & "' AND ")
                .AppendLine(String.Concat(String.Concat(" AL.BLOCK_NO = '", blockNo), "' AND "))
                .AppendLine("     BLOCKM.TANTO_BUSHO IS NOT NULL AND  ")
                .AppendLine("     AL.SHIYOSA_KAITEI_NO = (SELECT MAX(SHIYOSA_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("         WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                        And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                        And COL_NO = AL.COL_NO ")
                .AppendLine("                        And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                        AND SOBI_KAITEI_NO = AL.SOBI_KAITEI_NO ) ")
                '.AppendLine("     AND NOT ( SUBSTRING(AL.F_BUHIN_NO,11,2) = '##' ")
                '.AppendLine("        AND AL2.TOP_COLOR_KAITEI_NO IS NULL ")
                '.AppendLine("        AND COLORM.FUKA_NO IS NULL ) ")
                .AppendLine("     AND AL.SOBI_KAITEI_NO = SHIYO.SOBI_KAITEI_NO ")
                '2012/02/15 AL.SOBI_KAITEI_NO＝AL内の最大値（ただし仕様書テーブルの装備改訂Noを超えないものに変更
                '13日の修正では仮に仕様書テーブルの装備改訂Noを持つものがあってもそれより小さい改訂Noも取得されてしまう。
                '2012/02/13 AL.SOBI_KAITEI_NO＝仕様書テーブルの装備改訂Noの最大値ではなく
                '           AL.SOBI_KAITEI_NO＜＝仕様書テーブルの装備改訂Noの最大値に変更
                '           仕様書テーブルの装備改訂Noと一致したものが無い場合、さらに小さいものを探す
                .AppendLine("     AND AL.SOBI_KAITEI_NO = ")
                '.AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 ")
                '.AppendLine("              WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                '.AppendLine("                And SHIYOSHO_SEQNO =  ")
                '.AppendLine("                  RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-1 AS VarChar),3)) ")
                .AppendLine("         (SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("                  WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                 And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("                                 And COL_NO = AL.COL_NO ")
                .AppendLine("                                 And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("                                 AND SOBI_KAITEI_NO <= (")
                .AppendLine("                                               SELECT MAX(SOBI_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0030 WITH (NOLOCK, NOWAIT)")
                .AppendLine("                                                       WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("                                                                         And SHIYOSHO_SEQNO =  ")
                .AppendLine("                                                                         RIGHT('000' + CAST(CAST(BASE.BASE_SHIYOUJYOUHOU_NO as numeric(3))-0 AS VarChar),3)")
                .AppendLine("                                 )")
                .AppendLine("         )")
                '-----------------------------------------
                .AppendLine(" GROUP BY ")
                .AppendLine("     AL.BLOCK_NO, ")
                .AppendLine("     KA.BU_CODE, ")
                .AppendLine("     AL.F_BUHIN_NO, ")
                .AppendLine("     BASE.SHISAKU_GOUSYA, ")
                .AppendLine("     BLOCKM.TANTO_BUSHO, ")
                .AppendLine("     BLOCK_NAME ")
                .AppendLine(" ORDER BY ")
                .AppendLine("     BLOCK_NO, ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     FF_BUHIN_NO ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForList(Of SekkeiBlockAlResultVo)(sql.ToString())
        End Function

        ''' <summary>
        ''' ブロックを元に、「開発車機能ブロック」マスタから「ユニット区分」を返す
        ''' </summary>
        ''' <param name="BlockNo">ブロック№</param>
        ''' <param name="KaihatsuFugo">開発符号</param>
        ''' <returns>該当した「ユニット区分」</returns>
        ''' <remarks></remarks>
        Public Function FindShisakuBlockUnit(ByVal BlockNo As String, ByVal KaihatsuFugo As String) As Rhac0080Vo Implements SekkeiBlockDao.FindShisakuBlockUnit
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("BLOCK_NO_KINO, MAX(MT_KBN) AS MT_KBN ")
                .AppendLine("FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 WITH (NOLOCK, NOWAIT) ")
                .AppendLine("WHERE ")
                .AppendLine("KAIHATSU_FUGO = @KaihatsuFugo ")
                .AppendLine("AND BLOCK_NO_KINO = @BlockNoKino ")      '2012/02/16 開発符号を条件に追加
                .AppendLine("GROUP BY BLOCK_NO_KINO ")
                .AppendLine("HAVING ")
                .AppendLine("(BLOCK_NO_KINO <> '') ")
                .AppendLine("ORDER BY ")
                .AppendLine("BLOCK_NO_KINO ")
            End With
            Dim param As New Rhac0080Vo
            param.KaihatsuFugo = KaihatsuFugo
            param.BlockNoKino = BlockNo

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of Rhac0080Vo)(sql.ToString(), param)
        End Function

        ''' <summary>
        ''' ブロックを元に、試作設計ブロックテーブルから「ユニット区分」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="BlockNo">ブロック</param>
        ''' <returns>該当した「ユニット区分」</returns>
        ''' <remarks></remarks>
        Public Function FindShisakuBlockUnitFromSekkeiBlock(ByVal shisakuEventCode As String, ByVal BlockNo As String) As TShisakuSekkeiBlockVo Implements SekkeiBlockDao.FindShisakuBlockUnitFromSekkeiBlock
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("UNIT_KBN ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK WITH (NOLOCK, NOWAIT) ")
                .AppendLine("WHERE ")
                .AppendLine("SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine("AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine("GROUP BY SHISAKU_EVENT_CODE,SHISAKU_BLOCK_NO,UNIT_KBN ")
            End With
            Dim param As New TShisakuSekkeiBlockVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = BlockNo

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql.ToString(), param)
        End Function

        ''' <summary>
        ''' 「試作設計ブロックINSTL情報」から最大の「INSTL品番表示順」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <returns>該当した「Instl品番表示順」</returns>
        ''' <remarks></remarks>
        Public Function FindShisakuBlockInstlHyoujiJun(ByVal shisakuEventCode As String, _
                                             ByVal ShisakuBlockNo As String) As TShisakuSekkeiBlockInstlVo Implements SekkeiBlockDao.FindShisakuBlockInstlHyoujiJun
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("MAX(INSTL_HINBAN_HYOUJI_JUN) AS INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("WHERE ")
                .AppendLine("SHISAKU_EVENT_CODE = @ShisakuEventCode AND ")
                .AppendLine("SHISAKU_BLOCK_NO = @ShisakuBlockNo AND ")
                .AppendLine("SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo  ")
                .AppendLine("GROUP BY SHISAKU_EVENT_CODE, SHISAKU_BUKA_CODE, SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO ")
            End With
            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = "000"
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuSekkeiBlockInstlVo)(sql.ToString(), param)
        End Function

        ''' <summary>
        ''' 「試作設計ブロックINSTL情報」から最大の「INSTL品番表示順」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <returns>該当した「Instl品番表示順」</returns>
        ''' <remarks></remarks>
        Public Function FindShisakuBlockInstlHyoujiJun2(ByVal shisakuEventCode As String, _
                                             ByVal ShisakuBlockNo As String, _
                                             ByVal InstlHinban As String) As TShisakuSekkeiBlockInstlVo Implements SekkeiBlockDao.FindShisakuBlockInstlHyoujiJun2
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("WHERE ")
                .AppendLine("SHISAKU_EVENT_CODE = @ShisakuEventCode AND ")
                .AppendLine("SHISAKU_BLOCK_NO = @ShisakuBlockNo AND ")
                .AppendLine("SHISAKU_BLOCK_NO_KAITEI_NO = @ShisakuBlockNoKaiteiNo AND ")
                .AppendLine("INSTL_HINBAN = @InstlHinban  ")
            End With
            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBlockNo = ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = "000"
            param.InstlHinban = InstlHinban
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuSekkeiBlockInstlVo)(sql.ToString(), param)
        End Function

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「試作イベントベース車情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>該当した「試作イベントベース車情報」</returns>
        ''' <remarks></remarks>
        Function FindByShisakuEventBase(ByVal shisakuEventCode As String, ByVal tantoId As String) As List(Of TShisakuEventBaseVo) Implements SekkeiBlockDao.FindByShisakuEventBase
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_TMP ")
                .AppendLine("WHERE ")
                .AppendLine("SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND TANTO_ID ='" & tantoId & "' ")
                .AppendLine("ORDER BY SHISAKU_EVENT_CODE, HYOJIJUN_NO ")
            End With
            Dim param As New TShisakuEventBaseVo
            param.ShisakuEventCode = shisakuEventCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuEventBaseVo)(sql.ToString(), param)
        End Function

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「RHAC2210」を抽出して返す
        ''' </summary>
        ''' <param name="KaihatsuFugo">開発符号</param>
        ''' <param name="SobiKaiteiNo">装備改訂№</param>
        ''' <param name="KatashikiScd7">7桁型式識別コード</param>
        ''' <param name="ShimukechiCode">仕向地コード</param>
        ''' <param name="OpCode">OPコード</param>
        ''' <returns>該当した「RHAC2210」</returns>
        ''' <remarks></remarks>
        Function FindRHAC2210(ByVal KaihatsuFugo As String, _
                                  ByVal SobiKaiteiNo As String, _
                                  ByVal KatashikiScd7 As String, _
                                  ByVal ShimukechiCode As String, _
                                  ByVal OpCode As String) As List(Of Rhac2210Vo) Implements SekkeiBlockDao.FindRHAC2210
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)")
                .AppendLine("WHERE ")
                .AppendLine("KAIHATSU_FUGO = @KaihatsuFugo AND ")
                .AppendLine("SOBI_KAITEI_NO = @SobiKaiteiNo AND ")
                .AppendLine("KATASHIKI_SCD_7 = @KatashikiScd7 AND ")
                .AppendLine("SHIMUKECHI_CODE = @ShimukechiCode AND ")
                .AppendLine("OP_CODE = @OpCode ")
            End With
            Dim param As New Rhac2210Vo
            param.KaihatsuFugo = KaihatsuFugo
            param.SobiKaiteiNo = SobiKaiteiNo
            param.KatashikiScd7 = KatashikiScd7
            param.ShimukechiCode = ShimukechiCode
            param.OpCode = OpCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of Rhac2210Vo)(sql.ToString(), param)
        End Function

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="tantoId">担当ID</param>
        ''' <param name="SobiKaiteiNo">装備改訂№</param>
        ''' <param name="KaihatsuFugo">開発符号</param>
        ''' <param name="KatashikiScd7">7桁型式識別コード</param>
        ''' <param name="ShimukechiCode">仕向地コード</param>
        ''' <param name="OpCode">OPコード</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindByShisakuEvent(ByVal shisakuEventCode As String, _
                                    ByVal tantoId As String, _
                                  ByVal SobiKaiteiNo As String, _
                                  ByVal KaihatsuFugo As String, _
                                  ByVal KatashikiScd7 As String, _
                                  ByVal ShimukechiCode As String, _
                                  ByVal OpCode As String) As List(Of SekkeiBlockAlResultVo) Implements SekkeiBlockDao.FindByShisakuEvent
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT AL.BLOCK_NO, ")
                .AppendLine(" CASE WHEN AL2.FUKA_NO IS NULL AND COLOR.FUKA_NO IS NULL THEN AL.F_BUHIN_NO ")
                .AppendLine("      WHEN AL2.FUKA_NO IS NOT NULL AND COLOR.FUKA_NO IS NULL THEN LEFT(AL.F_BUHIN_NO,10)+ AL2.FUKA_NO ")
                .AppendLine("      WHEN AL2.FUKA_NO IS NULL AND COLOR.FUKA_NO IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ COLOR.FUKA_NO ")
                .AppendLine("      WHEN AL2.FUKA_NO IS NOT NULL AND COLOR.FUKA_NO IS NOT NULL THEN LEFT(AL.F_BUHIN_NO,10)+ AL2.FUKA_NO ")
                .AppendLine(" End ")
                .AppendLine(" AS FF_BUHIN_NO, ")
                .AppendLine(" AL.F_BUHIN_NO AS BF_BUHIN_NO, ")
                .AppendLine(" KA.BU_CODE, ")
                .AppendLine(" KA.KA_CODE, ")
                .AppendLine(" BASE.SHISAKU_GOUSYA, ")
                .AppendLine(" BLOCKM.TANTO_BUSHO AS BUKA_CODE, ")
                .AppendLine(" BLOCK_NAME, ")
                .AppendLine(" AL2.TOP_COLOR_KAITEI_NO , ")
                .AppendLine(" Color.FUKA_NO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 AL WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210)) ")
                .AppendLine("     INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT_BASE_TMP BASE ")
                .AppendLine("         ON AL.KAIHATSU_FUGO = BASE.BASE_KAIHATSU_FUGO ")
                .AppendLine("      And AL.KATASHIKI_SCD_7 = BASE.BASE_KATASHIKI_SCD_7 ")
                .AppendLine("      AND AL.SHIMUKECHI_CODE = BASE.BASE_SHIMUKE ")
                .AppendLine("      AND AL.OP_CODE = BASE.BASE_OP ")
                .AppendLine("      AND AL.F_BUHIN_NO <>  '         0     ' ")
                .AppendLine("      AND AL.SOBI_KAITEI_NO = BASE.BASE_SOBI_KAITEI_NO ")
                .AppendLine("      AND LEFT(AL.BLOCK_NO,3) <>  '999' ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 AL2 WITH (NOLOCK, NOWAIT)")
                .AppendLine("         ON AL2.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("      And AL2.KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("      And AL2.COL_NO = AL.COL_NO ")
                .AppendLine("      And AL2.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("      And AL2.F_BUHIN_NO = AL.F_BUHIN_NO ")
                .AppendLine("      AND (AL2.COLOR_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("         OR AL2.COLOR_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("      AND AL2.TOP_COLOR_KAITEI_NO = (SELECT MAX(TOP_COLOR_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2220 WITH (NOLOCK, NOWAIT)")
                .AppendLine("         WHERE(KAIHATSU_FUGO = AL2.KAIHATSU_FUGO) ")
                .AppendLine("                            AND KATASHIKI_SCD_7 = AL2.KATASHIKI_SCD_7 ")
                .AppendLine("                            AND COL_NO = AL2.COL_NO")
                .AppendLine("                            AND BLOCK_NO = AL2.BLOCK_NO ")
                .AppendLine("                            AND F_BUHIN_NO = AL2.F_BUHIN_NO ")
                .AppendLine("                            AND COLOR_CODE = AL2.COLOR_CODE ) ")
                .AppendLine("     LEFT OUTER JOIN ")
                .AppendLine("         " & RHACLIBF_DB_NAME & ".dbo.RHAC0790 DEZA WITH (NOLOCK, NOWAIT) ON ")
                .AppendLine("             DEZA.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And DEZA.ZUMEN_NO = AL.F_BUHIN_NO ")
                .AppendLine("         And DEZA.SOBI_KAITEI_NO = AL.SOBI_KAITEI_NO ")
                .AppendLine("     LEFT OUTER JOIN ")
                .AppendLine("         " & RHACLIBF_DB_NAME & ".dbo.RHAC0460 COLOR WITH (NOLOCK, NOWAIT) ON ")
                .AppendLine("             Color.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         And Color.SOBI_KAITEI_NO = AL.SOBI_KAITEI_NO ")
                .AppendLine("         And Color.SHIYOSOBI_CODE = DEZA.SHIYOSOBI_CODE ")
                .AppendLine("         And Color.COLOR_SETTEI_KBN = DEZA.COLOR_SETTEI_KBN ")
                .AppendLine("         And Color.DESIGN_GROUP_CODE = DEZA.DESIGN_GROUP_CODE ")
                .AppendLine("         And Color.DESIGN_GROUP_KAITEI_NO = DEZA.DESIGN_GROUP_KAITEI_NO ")
                .AppendLine("         And Color.DESIGN_BUI_CODE = DEZA.DESIGN_BUI_CODE ")
                .AppendLine("         AND (COLOR.COLOR_CODE = BASE.BASE_GAISOUSYOKU ")
                .AppendLine("                OR COLOR.COLOR_CODE = BASE.BASE_NAISOUSYOKU) ")
                .AppendLine("     LEFT OUTER JOIN ")
                .AppendLine("         " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 BLOCKM WITH (NOLOCK, NOWAIT) ON ")
                .AppendLine("             BLOCKM.BLOCK_NO_KINO = AL.BLOCK_NO ")
                .AppendLine("         And BLOCKM.KAIHATSU_FUGO = AL.KAIHATSU_FUGO ")
                .AppendLine("         AND LEFT(BLOCKM. BLOCK_NO_KINO, 3) <> '999' ")
                .AppendLine("         And BLOCKM.HAISI_DATE = 99999999 ")
                .AppendLine("     LEFT OUTER JOIN ")
                .AppendLine("         " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 BLOCKNAME WITH (NOLOCK, NOWAIT) ON ")
                .AppendLine("             BLOCKNAME.BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("     LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 KA ")
                .AppendLine("         ON BLOCKM.TANTO_BUSHO = KA.KA_RYAKU_NAME ")
                .AppendLine("              ,(SELECT '0' AS DIV UNION ALL SELECT '1') C ")
                .AppendLine(" WHERE BASE.SHISAKU_EVENT_CODE = @shisakuEventCode AND ")
                .AppendLine("       BASE.TANTO_ID ='" & tantoId & "' AND ")
                .AppendLine("       BASE.BASE_SOBI_KAITEI_NO = @SobiKaiteiNo AND ")
                .AppendLine("       BASE.BASE_KAIHATSU_FUGO = @KaihatsuFugo AND ")
                .AppendLine("       BASE.BASE_KATASHIKI_SCD_7 = @KatashikiScd7 AND ")
                .AppendLine("       BASE.BASE_SHIMUKE = @ShimukechiCode AND ")
                .AppendLine("       BASE.BASE_OP = @OpCode AND ")
                .AppendLine("       BLOCKM.TANTO_BUSHO IS NOT NULL AND ")
                .AppendLine("       AL.SHIYOSA_KAITEI_NO = (SELECT MAX(SHIYOSA_KAITEI_NO) FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC2210 WITH (NOLOCK, NOWAIT, INDEX(PK_RHAC2210))")
                .AppendLine("            WHERE(KAIHATSU_FUGO = AL.KAIHATSU_FUGO) ")
                .AppendLine("             And KATASHIKI_SCD_7 = AL.KATASHIKI_SCD_7 ")
                .AppendLine("             And COL_NO = AL.COL_NO ")
                .AppendLine("             And BLOCK_NO = AL.BLOCK_NO ")
                .AppendLine("             AND SOBI_KAITEI_NO = AL.SOBI_KAITEI_NO ) ")
                .AppendLine(" GROUP BY ")
                .AppendLine("     AL.BLOCK_NO, ")
                .AppendLine("     AL.F_BUHIN_NO, ")
                .AppendLine("     KA.BU_CODE, ")
                .AppendLine("     KA.KA_CODE, ")
                .AppendLine("     BASE.SHISAKU_GOUSYA, ")
                .AppendLine("     BLOCKM.TANTO_BUSHO, ")
                .AppendLine("     BLOCK_NAME, ")
                .AppendLine("     AL2.TOP_COLOR_KAITEI_NO, ")
                .AppendLine("     AL2.FUKA_NO, ")
                .AppendLine("     Color.FUKA_NO ")
                .AppendLine(" ORDER BY ")
                .AppendLine("     BLOCK_NO, ")
                .AppendLine("     SHISAKU_GOUSYA, ")
                .AppendLine("     FF_BUHIN_NO ")
            End With
            Dim param As New SekkeiBlockAlResultVo
            param.shisakuEventCode = shisakuEventCode
            param.SobiKaiteiNo = SobiKaiteiNo
            param.KaihatsuFugo = KaihatsuFugo
            param.KatashikiScd7 = KatashikiScd7
            param.ShimukechiCode = ShimukechiCode
            param.OpCode = OpCode
            Dim db As New EBomDbClient
            Return db.QueryForList(Of SekkeiBlockAlResultVo)(sql.ToString(), param)
        End Function


        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindSekkeiBlock(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBlockNo As String, _
                                 ByVal shisakuBlockNoKaiteiNo As String) As TShisakuSekkeiBlockVo Implements SekkeiBlockDao.FindSekkeiBlock
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE BLOCK.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' AND ")
                .AppendLine("       BLOCK.SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' AND ")
                .AppendLine("       BLOCK.SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' AND ")
                .AppendLine("       BLOCK.SHISAKU_BLOCK_NO_KAITEI_NO = '" & shisakuBlockNoKaiteiNo & "' ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuSekkeiBlockVo)(sql.ToString())
        End Function

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="instlDataKbn">INSTLデータ区分</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindSekkeiBlockInstl(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal shisakuBlockNoKaiteiNo As String, _
                                      ByVal shisakuGousya As String, _
                                      ByVal instlDataKbn As String, _
                                      ByVal instlHinban As String, _
                                      ByVal instlHinbanKbn As String) As TShisakuSekkeiBlockInstlVo Implements SekkeiBlockDao.FindSekkeiBlockInstl
            Dim sql As New StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL BLOCK WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE BLOCK.SHISAKU_EVENT_CODE = '" & shisakuEventCode & "' AND ")
                .AppendLine("       BLOCK.SHISAKU_BUKA_CODE = '" & shisakuBukaCode & "' AND ")
                .AppendLine("       BLOCK.SHISAKU_BLOCK_NO = '" & shisakuBlockNo & "' AND ")
                .AppendLine("       BLOCK.SHISAKU_BLOCK_NO_KAITEI_NO = '" & shisakuBlockNoKaiteiNo & "' AND")
                .AppendLine("       BLOCK.SHISAKU_GOUSYA = '" & shisakuGousya & "' AND")
                .AppendLine("       BLOCK.INSTL_HINBAN = '" & instlHinban & "' AND")
                .AppendLine("       BLOCK.INSTL_HINBAN_KBN = '" & instlHinbanKbn & "' AND")
                .AppendLine("       BLOCK.INSTL_DATA_KBN = '" & instlDataKbn & "' ")
            End With
            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuSekkeiBlockInstlVo)(sql.ToString())
        End Function


#Region "親品番が自給品のデータを削除する"

        ''' <summary>
        ''' 「試作イベント情報」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作イベント情報</returns>
        ''' <remarks></remarks>
        Function FindByShisakuEvent(ByVal shisakuEventCode As String) As TShisakuEventVo Implements SekkeiBlockDao.FindByShisakuEvent
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT JIKYU_UMU ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @Value ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForObject(Of TShisakuEventVo)(sb.ToString(), shisakuEventCode)
        End Function

        ''' <summary>
        ''' 「試作設計ブロック情報」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindByShisakuBlockAll(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockVo) Implements SekkeiBlockDao.FindByShisakuBlockAll
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT SHISAKU_EVENT_CODE, SHISAKU_BUKA_CODE, ")
                .AppendLine(" SHISAKU_BLOCK_NO, SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @Value ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sb.ToString(), shisakuEventCode)
        End Function
        ''' <summary>
        ''' 「試作設計ブロック情報」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindByShisakuBlockAllGroupByBlockNo(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockVo) Implements SekkeiBlockDao.FindByShisakuBlockAllGroupByBlockNo
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT  ")
                .AppendLine(" SHISAKU_BLOCK_NO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @Value ")
                .AppendLine(" GROUP BY SHISAKU_BLOCK_NO ")
                .AppendLine(" ORDER BY SHISAKU_BLOCK_NO ")
            End With

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockVo)(sb.ToString(), shisakuEventCode)
        End Function

        ''' <summary>
        ''' 「試作設計ブロックINSTL情報」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>設計ブロックINSTL情報</returns>
        ''' <remarks></remarks>
        Function FindByShisakuSekkeiBlockInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockInstlVo) Implements SekkeiBlockDao.FindByShisakuSekkeiBlockInstl
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
            End With

            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuSekkeiBlockInstlVo)(sb.ToString(), param)
        End Function

        ''' <summary>
        ''' 「部品編集INSTL情報」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns>部品編集INSTL情報</returns>
        ''' <remarks></remarks>
        Function FindByShisakuBuhinEditInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal instlHinbanHyoujiJun As Integer) As List(Of TShisakuBuhinEditInstlVo) Implements SekkeiBlockDao.FindByShisakuBuhinEditInstl
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" SELECT B.* ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL A ")
                .AppendLine(" INNER JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL B ")
                .AppendLine(" ON A.SHISAKU_EVENT_CODE = B.SHISAKU_EVENT_CODE ")
                .AppendLine(" AND A.SHISAKU_BUKA_CODE = B.SHISAKU_BUKA_CODE ")
                .AppendLine(" AND A.SHISAKU_BLOCK_NO = B.SHISAKU_BLOCK_NO ")
                .AppendLine(" AND A.SHISAKU_BLOCK_NO_KAITEI_NO = B.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine(" AND A.INSTL_HINBAN_HYOUJI_JUN = B.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine(" WHERE ")
                .AppendLine(" A.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND A.SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND A.SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND A.SHISAKU_BLOCK_NO_KAITEI_NO = '000' ")
                .AppendLine(" AND A.INSTL_HINBAN_HYOUJI_JUN = @InstlHinbanHyoujiJun ")
            End With

            Dim param As New TShisakuBuhinEditInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo
            param.InstlHinbanHyoujiJun = instlHinbanHyoujiJun

            Dim db As New EBomDbClient
            Return db.QueryForList(Of TShisakuBuhinEditInstlVo)(sb.ToString(), param)
        End Function

        ''' <summary>
        ''' 「試作設計ブロックINSTL情報と試作設計ブロック情報」を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <remarks></remarks>
        Sub DeleteByShisakuBuhinEditInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) Implements SekkeiBlockDao.DeleteByShisakuBuhinEditInstl
            Dim sb As New StringBuilder
            With sb
                .Remove(0, .Length)
                .AppendLine(" DELETE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
            End With
            Dim param As New TShisakuSekkeiBlockInstlVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuBukaCode = shisakuBukaCode
            param.ShisakuBlockNo = shisakuBlockNo

            Dim db As New EBomDbClient
            db.Delete(sb.ToString(), param)

            Dim sb1 As New StringBuilder

            With sb1
                .Remove(0, .Length)
                .AppendLine(" DELETE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
            End With
            Dim param2 As New TShisakuSekkeiBlockVo
            param2.ShisakuEventCode = shisakuEventCode
            param2.ShisakuBukaCode = shisakuBukaCode
            param2.ShisakuBlockNo = shisakuBlockNo

            Dim db2 As New EBomDbClient

            db2.Delete(sb1.ToString(), param2)
        End Sub


#End Region

    End Class
End Namespace