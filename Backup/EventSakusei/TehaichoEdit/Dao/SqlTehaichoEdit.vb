Imports ShisakuCommon
Namespace TehaichoEdit.Dao

    Public Class SqlTehaichoEdit

#Region "SQL-SELECT 集計コード一覧"
        ''' <summary>
        ''' SQL-集計コード一覧
        ''' </summary>
        ''' <returns>該当する一覧</returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlAllListSyukeiMaster() As String

            Dim sql As String = _
                "SELECT SYUKEI_CODE ," _
                    & "SYUKEI_NAME  " _
                    & "FROM  " & MBOM_DB_NAME & ".dbo.M_SYUKEI_CODE WITH (NOLOCK, NOWAIT) "
            Return sql
        End Function
#End Region

        ''↓↓2015/01/19 試作イベント情報追加 TES)劉 ADD BEGIN
#Region "SQL-SELECT 試作開発符号一覧"
        ''' <summary>
        ''' SQL-SELECT 試作開発符号一覧
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlByShisakuEventCode() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT SHISAKU_KAIHATSU_FUGO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE =@ShisakuEventCode ")
            End With

            Return sql.ToString
        End Function
#End Region
        ''↑↑2015/01/19 試作イベント情報追加 TES)劉 ADD END

#Region "SQL-SELECT 手配記号一覧"
        ''' <summary>
        ''' SQL-SELECT 手配記号一覧
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlAllListTehaiKigou() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT TIKG ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ARPF04 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE BUKBN = 'B' ")
                .AppendLine(" ORDER BY TIKG ")
            End With

            Return sql.ToString
        End Function
#End Region


        ''↓↓2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
#Region "SQL-SELECT 作り方製作方法一覧"
        ''' <summary>
        ''' SQL-SELECT 作り方製作方法一覧
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlAllListTsukurikataSeisaku() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT TSUKURIKATA_NO,TSUKURIKATA_NAME ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_TSUKURIKATA WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE TSUKURIKATA_KBN = '1' ")
                .AppendLine(" ORDER BY TSUKURIKATA_NO ")
            End With

            Return sql.ToString
        End Function
#End Region
#Region "SQL-SELECT 作り方型仕様一覧"
        ''' <summary>
        ''' SQL-SELECT 作り方型仕様一覧
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlAllListTsukurikataKatashiyou() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                ''↓↓2014/09/02 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
                '.AppendLine(" SELECT TSUKURIKATA_NO,TSUKURIKATA_NAME ")
                .AppendLine(" SELECT CAST(TSUKURIKATA_NO as varchar) + '.' + TSUKURIKATA_NAME AS TSUKURIKATA_NAME")
                ''↑↑2014/09/02 Ⅰ.2.管理項目追加 酒井 ADD END
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_TSUKURIKATA WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE TSUKURIKATA_KBN = '3' ")
                .AppendLine(" ORDER BY TSUKURIKATA_NO ")
            End With

            Return sql.ToString
        End Function
#End Region
#Region "SQL-SELECT 作り方治具一覧"
        ''' <summary>
        ''' SQL-SELECT 作り方治具一覧
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlAllListTsukurikataTigu() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT TSUKURIKATA_NO,TSUKURIKATA_NAME ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_TSUKURIKATA WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE TSUKURIKATA_KBN = '4' ")
                .AppendLine(" ORDER BY TSUKURIKATA_NO ")
            End With

            Return sql.ToString
        End Function
#End Region
        ''↑↑2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD END

#Region "SQL-SELECT ブロックNo一覧取得(RHAC0080)"
        ''' <summary>
        ''' SQL-SELECT ブロックNo一覧取得(RHAC0080)
        ''' 
        ''' ※ブロックと担当部署名称をNmRhac0080から取得
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlAllListBlockNo() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine("SELECT ")
                .AppendLine("        BLOCK_NO_KINO ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE HAISI_DATE = 99999999 ")
                .AppendLine("      AND RTRIM(BLOCK_NO_KINO) <> '' ")
                .AppendLine(" GROUP BY  ")
                .AppendLine("     BLOCK_NO_KINO")
                .AppendLine("  ORDER BY ")
                .AppendLine("     BLOCK_NO_KINO")
            End With

            Return sql.ToString
        End Function

#End Region

#Region "SQL-SELECT 部課コード取得(RHAC0080→RHAC1560)"
        ''' <summary>
        ''' SQL-SELECT 部課コード取得(RHAC0080→RHAC1560)
        ''' 
        ''' ※ "BUKACODE"と"BLOCK_NO_KINO"で読取り
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlFindBukaCode()
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine("    WITH BLOCK_TBL AS ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT  ")
                .AppendLine("    TANTO_BUSHO, ")
                .AppendLine("    BLOCK_NO_KINO ")
                .AppendLine("    FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 WITH (NOLOCK, NOWAIT) ")
                .AppendLine("        WHERE  ")
                .AppendLine("                      BLOCK_NO_KINO = @BlockNoKino ")
                .AppendLine("               AND HAISI_DATE = 99999999 ")
                .AppendLine("    GROUP BY  ")
                .AppendLine("        TANTO_BUSHO, ")
                .AppendLine("        BLOCK_NO_KINO ")
                .AppendLine("    ) ")
                .AppendLine("    SELECT BU_CODE + KA_CODE AS BUKA_CODE, ")
                .AppendLine("           BLOCK.BLOCK_NO_KINO  ")
                .AppendLine("    FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 AS BUKA WITH (NOLOCK, NOWAIT)  ")
                .AppendLine("       INNER JOIN BLOCK_TBL AS BLOCK ")
                .AppendLine("    ON BUKA.KA_RYAKU_NAME = BLOCK.TANTO_BUSHO ")
            End With

            Return sql.ToString

        End Function
#End Region

#Region "SQL-SELECT 取引先マスタ(RHAC0610)主キー検索"
        Public Shared Function GetSqlFindPkRhac0610() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT MAKER_CODE  ")
                .AppendLine("    ,MAKER_NAME ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE MAKER_CODE =@MakerCode ")
            End With

            Return sql.ToString
        End Function
#End Region

#Region "SQL-SELECT 試作イベント 主キー検索"
        ''' <summary>
        ''' SQL-SELECT 試作イベント 主キー検索
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlShisakuEventKey() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT SHISAKU_EVENT_CODE,UNIT_KBN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" ORDER BY UPDATED_DATE DESC ,UPDATED_TIME DESC ")
            End With

            Return sql.ToString
        End Function

#End Region

#Region "SQL-SELECT 試作手配帳(基本情報)-一覧取得"
        ''' <summary>
        ''' SQL-試作手配帳(基本情報)一覧を返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlAllBaseInfo() As String
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT ")
                .AppendLine("SHISAKU_EVENT_CODE ,")
                .AppendLine("SHISAKU_LIST_CODE , ")
                .AppendLine("SHISAKU_LIST_CODE_KAITEI_NO , ")
                .AppendLine("SHISAKU_BUKA_CODE , ")
                .AppendLine("RIREKI, ")
                .AppendLine(" SHISAKU_BLOCK_NO, ")
                .AppendLine("GYOU_ID, ")
                .AppendLine("SENYOU_MARK, ")
                .AppendLine("LEVEL, ")
                .AppendLine("BUHIN_NO_HYOUJI_JUN , ")
                .AppendLine("UNIT_KBN, ")
                .AppendLine("BUHIN_NO, ")
                .AppendLine("BUHIN_NO_KBN, ")
                .AppendLine("BUHIN_NO_KAITEI_NO, ")
                .AppendLine("EDA_BAN, ")
                .AppendLine("BUHIN_NAME, ")
                .AppendLine("SHUKEI_CODE, ")
                .AppendLine("TEHAI_KIGOU, ")
                .AppendLine("KOUTAN, ")
                .AppendLine("TORIHIKISAKI_CODE, ")
                .AppendLine("NOUBA, ")
                .AppendLine("KYOUKU_SECTION, ")
                .AppendLine("NOUNYU_SHIJIBI, ")
                .AppendLine("TOTAL_INSU_SURYO, ")
                .AppendLine("SAISHIYOUFUKA, ")
                .AppendLine("SHUTUZU_YOTEI_DATE, ")
                .AppendLine("SHUTUZU_JISEKI_DATE, ")
                .AppendLine("SHUTUZU_JISEKI_KAITEI_NO, ")
                .AppendLine("SHUTUZU_JISEKI_STSR_DHSTBA, ")
                .AppendLine("SAISYU_SETSUHEN_DATE, ")
                .AppendLine("SAISYU_SETSUHEN_KAITEI_NO, ")
                .AppendLine("STSR_DHSTBA, ")
                .AppendLine("TSUKURIKATA_SEISAKU, ")
                .AppendLine("TSUKURIKATA_KATASHIYOU_1, ")
                .AppendLine("TSUKURIKATA_KATASHIYOU_2, ")
                .AppendLine("TSUKURIKATA_KATASHIYOU_3, ")
                .AppendLine("TSUKURIKATA_TIGU, ")
                .AppendLine("TSUKURIKATA_NOUNYU, ")
                .AppendLine("TSUKURIKATA_KIBO, ")
                .AppendLine("ZAISHITU_KIKAKU_1, ")
                .AppendLine("ZAISHITU_KIKAKU_2, ")
                .AppendLine("ZAISHITU_KIKAKU_3, ")
                .AppendLine("ZAISHITU_MEKKI, ")
                .AppendLine("SHISAKU_BANKO_SURYO, ")
                .AppendLine("SHISAKU_BANKO_SURYO_U, ")
                .AppendLine("MATERIAL_INFO_LENGTH, ")
                .AppendLine("MATERIAL_INFO_WIDTH, ")
                .AppendLine("ZAIRYO_SUNPO_X, ")
                .AppendLine("ZAIRYO_SUNPO_Y, ")
                .AppendLine("ZAIRYO_SUNPO_Z, ")
                .AppendLine("ZAIRYO_SUNPO_XY, ")
                .AppendLine("ZAIRYO_SUNPO_XZ, ")
                .AppendLine("ZAIRYO_SUNPO_YZ, ")
                .AppendLine("MATERIAL_INFO_ORDER_TARGET, ")
                .AppendLine("MATERIAL_INFO_ORDER_TARGET_DATE, ")
                .AppendLine("MATERIAL_INFO_ORDER_CHK, ")
                .AppendLine("MATERIAL_INFO_ORDER_CHK_DATE, ")
                .AppendLine("DATA_ITEM_KAITEI_NO, ")
                .AppendLine("DATA_ITEM_AREA_NAME, ")
                .AppendLine("DATA_ITEM_SET_NAME, ")
                .AppendLine("DATA_ITEM_KAITEI_INFO, ")
                .AppendLine("DATA_ITEM_DATA_PROVISION, ")
                .AppendLine("DATA_ITEM_DATA_PROVISION_DATE, ")
                .AppendLine("SHISAKU_BUHINN_HI, ")
                .AppendLine("SHISAKU_KATA_HI, ")
                .AppendLine("MAKER_CODE, ")
                .AppendLine("BIKOU, ")
                .AppendLine("BUHIN_NO_OYA, ")
                .AppendLine("BUHIN_NO_KBN_OYA, ")
                .AppendLine("HENKATEN, ")
                .AppendLine("AUTO_ORIKOMI_KAITEI_NO")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON  KIHON WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine("    AND SHISAKU_LIST_CODE_KAITEI_NO =   ")
                .AppendLine("(  ")
                .AppendLine("    SELECT MAX(CONVERT(INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ))) AS SHISAKU_LIST_CODE_KAITEI_NO   ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON AS MAX_KIHON WITH (NOLOCK, NOWAIT)  ")
                .AppendLine("    WHERE  MAX_KIHON.SHISAKU_EVENT_CODE = KIHON.SHISAKU_EVENT_CODE  ")
                .AppendLine("    AND  MAX_KIHON.SHISAKU_LIST_CODE   = KIHON.SHISAKU_LIST_CODE         ")
                .AppendLine("    AND  MAX_KIHON.SHISAKU_BUKA_CODE  = KIHON.SHISAKU_BUKA_CODE  ")
                .AppendLine("   ")
                .AppendLine(")    ")
                .AppendLine("ORDER BY ")
                .AppendLine("SHISAKU_BLOCK_NO, ")
                .AppendLine("SORT_JUN ")
            End With
            Return sql.ToString
        End Function

#End Region

#Region "SQL-SELECT 名称一覧取得(号車情報)"
        ''' <summary>
        '''　SQL SELECT 名称一覧取得(号車情報)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlNamedListGousya() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT ")
                .AppendLine(" TG.SHISAKU_EVENT_CODE ")
                .AppendLine(" ,TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO ")
                .AppendLine(" ,TG.SHISAKU_GOUSYA ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG WITH (NOLOCK, NOWAIT) ")

                ''2015/07/29 変更 E.Ubukata Ver 2.10.7
                ''処理速度が遅いためバインドパラメータ→定数へ変更
                '.AppendLine(" WHERE TG.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                '.AppendLine(" AND TG.SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine(" WHERE TG.SHISAKU_EVENT_CODE = '{0}' ")
                .AppendLine(" AND TG.SHISAKU_LIST_CODE = '{1}' ")

                .AppendLine(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine("    SELECT MAX(SHISAKU_LIST_CODE_KAITEI_NO) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA AS MAX_KIHON WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE TG.SHISAKU_EVENT_CODE = SHISAKU_EVENT_CODE ")
                .AppendLine("    AND TG.SHISAKU_LIST_CODE = SHISAKU_LIST_CODE ) ")
                .AppendLine(" GROUP BY TG.SHISAKU_EVENT_CODE, TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA ")
                .AppendLine(" ORDER BY TG.SHISAKU_EVENT_CODE, TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA ")

            End With


            Return sql.ToString

        End Function

#End Region

#Region "SQL-SELECT 試作手配帳(号車情報)一覧取得"
        ''' <summary>
        ''' SQL-SELECT 試作手配号車
        ''' </summary>
        ''' <param name="aListNameGousha"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlAllGousyaInfo(ByVal aListNameGousha As String) As String
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT ")
                .AppendLine("       KIHON.SHISAKU_EVENT_CODE  ")
                .AppendLine("      ,KIHON.SHISAKU_LIST_CODE  ")
                .AppendLine("      ,KIHON.SHISAKU_LIST_CODE_KAITEI_NO  ")
                .AppendLine("      ,KIHON.SHISAKU_BUKA_CODE  ")
                .AppendLine("      ,KIHON.SHISAKU_BLOCK_NO  ")
                .AppendLine("      ,KIHON.BUHIN_NO_HYOUJI_JUN  ")
                .AppendLine("      ,KIHON.SORT_JUN  ")
                .AppendLine("      ,KIHON.RIREKI  ")
                .AppendLine("      ,KIHON.GYOU_ID  ")
                .AppendLine("      ,KIHON.SENYOU_MARK  ")
                .AppendLine("      ,KIHON.LEVEL  ")
                .AppendLine("      ,KIHON.BUHIN_NO  ")
                .AppendLine("      ,KIHON.BUHIN_NO_KBN  ")
                .AppendLine("      ,KIHON.TOTAL_INSU_SURYO  ")
                .AppendLine(aListNameGousha)
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON AS KIHON WITH (NOLOCK, NOWAIT) ")
                .AppendLine("LEFT JOIN  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA AS GOUSYA WITH (NOLOCK, NOWAIT)  ")
                .AppendLine("ON          KIHON.SHISAKU_EVENT_CODE = GOUSYA.SHISAKU_EVENT_CODE   ")
                .AppendLine("    AND    KIHON.SHISAKU_LIST_CODE =  GOUSYA.SHISAKU_LIST_CODE  ")
                .AppendLine("    AND    KIHON.SHISAKU_LIST_CODE_KAITEI_NO = GOUSYA.SHISAKU_LIST_CODE_KAITEI_NO              ")
                .AppendLine("    AND KIHON.SHISAKU_BUKA_CODE = GOUSYA.SHISAKU_BUKA_CODE  ")
                .AppendLine("    AND KIHON.SHISAKU_BLOCK_NO = GOUSYA.SHISAKU_BLOCK_NO  ")
                .AppendLine("    AND KIHON.BUHIN_NO_HYOUJI_JUN = GOUSYA.BUHIN_NO_HYOUJI_JUN     ")
                '.AppendLine("WHERE KIHON.SHISAKU_EVENT_CODE = @ShisakuEventCode  ")
                '.AppendLine("    AND KIHON.SHISAKU_LIST_CODE = @ShisakuListCode  ")
                .AppendLine("WHERE KIHON.SHISAKU_EVENT_CODE = '{0}'  ")
                .AppendLine("    AND KIHON.SHISAKU_LIST_CODE = '{1}'  ")
                .AppendLine("     AND KIHON.SHISAKU_LIST_CODE_KAITEI_NO =   ")
                .AppendLine("(   ")
                .AppendLine("     SELECT MAX ( CONVERT ( INT,COALESCE ( SHISAKU_LIST_CODE_KAITEI_NO,'' ) ) ) AS SHISAKU_LIST_CODE_KAITEI_NO  ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON WITH (NOLOCK, NOWAIT)   ")
                .AppendLine("    WHERE SHISAKU_EVENT_CODE = KIHON.SHISAKU_EVENT_CODE  ")
                .AppendLine("         AND SHISAKU_LIST_CODE = KIHON.SHISAKU_LIST_CODE   ")
                .AppendLine(")       ")
                .AppendLine("GROUP BY   ")
                .AppendLine("        KIHON.SHISAKU_EVENT_CODE  ")
                .AppendLine("       ,KIHON.SHISAKU_LIST_CODE  ")
                .AppendLine("       ,KIHON.SHISAKU_LIST_CODE_KAITEI_NO  ")
                .AppendLine("       ,KIHON.SHISAKU_BUKA_CODE  ")
                .AppendLine("       ,KIHON.SHISAKU_BLOCK_NO  ")
                .AppendLine("       ,KIHON.BUHIN_NO_HYOUJI_JUN  ")
                .AppendLine("       ,KIHON.SORT_JUN  ")
                .AppendLine("       ,KIHON.RIREKI  ")
                .AppendLine("       ,KIHON.GYOU_ID  ")
                .AppendLine("       ,KIHON.SENYOU_MARK  ")
                .AppendLine("       ,KIHON.LEVEL  ")
                .AppendLine("       ,KIHON.BUHIN_NO  ")
                .AppendLine("       ,KIHON.BUHIN_NO_KBN  ")
                .AppendLine("      ,KIHON.TOTAL_INSU_SURYO  ")
                .AppendLine("ORDER BY    ")
                .AppendLine("SHISAKU_BLOCK_NO, ")
                .AppendLine("SORT_JUN ")
            End With

            Return sql.ToString
        End Function

#End Region

#Region "SQL-SELECT 試作手配帳(基本情報)主キー検索"
        ''' <summary>
        ''' SQL-SELECT 試作手配帳(基本情報)主キー検索
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlFindPkTehaiBase() As String

            Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT SHISAKU_EVENT_CODE,SHISAKU_LIST_CODE, SHISAKU_LIST_CODE_KAITEI_NO,SHISAKU_BLOCK_NO,SHISAKU_BUKA_CODE,BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                '.AppendLine("         SHISAKU_EVENT_CODE = @SHISAKU_EVENT_CODE ")
                '.AppendLine(" AND SHISAKU_LIST_CODE = @SHISAKU_LIST_CODE  ")
                '.AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @SHISAKU_LIST_CODE_KAITEI_NO")
                '.AppendLine(" AND SHISAKU_BLOCK_NO = @SHISAKU_BLOCK_NO ")
                '.AppendLine(" AND SHISAKU_BUKA_CODE = @SHISAKU_BUKA_CODE ")
                '.AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BUHIN_NO_HYOUJI_JUN ")
                .AppendLine("         SHISAKU_EVENT_CODE = '{0}' ")
                .AppendLine(" AND SHISAKU_LIST_CODE = '{1}'  ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{2}'")
                .AppendLine(" AND SHISAKU_BLOCK_NO = '{3}' ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = '{4}' ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = {5} ")
            End With

            Return sql.ToString

        End Function
#End Region

#Region "SQL-SELECT 試作手配帳(号車情報)主キー検索"
        ''' <summary>
        ''' SQL-SELECT 試作手配帳(号車情報)主キー検索
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlFindPkTehaiGousya() As String
            Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT *  ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendLine("         SHISAKU_EVENT_CODE = @SHISAKU_EVENT_CODE ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @SHISAKU_LIST_CODE  ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @SHISAKU_LIST_CODE_KAITEI_NO")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @SHISAKU_BLOCK_NO ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @SHISAKU_BUKA_CODE ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BUHIN_NO_HYOUJI_JUN ")
            End With

            Return sql.ToString
        End Function

        ''' <summary>
        ''' SQL-SELECT 試作手配帳(号車情報)DUMMY号車検索
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlFindPkTehaiGousyaDummy() As String
            Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT DISTINCT SHISAKU_GOUSYA_HYOUJI_JUN  ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendLine("         SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode  ")
                .AppendLine(" AND SHISAKU_GOUSYA = 'DUMMY' ")
            End With

            Return sql.ToString
        End Function
#End Region


#Region "SQL-SELECT 試作部品編集情報(主キー検索)"
        ''' <summary>
        ''' SQL-SELECT 試作部品編集情報(主キー検索)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlFindBuhinNoShisakuBuhinEdit()
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT  ")
                .AppendLine("    SHISAKU_EVENT_CODE , ")
                .AppendLine("    SHISAKU_BUKA_CODE , ")
                .AppendLine("    SHISAKU_BLOCK_NO , ")
                .AppendLine("    SHISAKU_BLOCK_NO_KAITEI_NO , ")
                .AppendLine("    BUHIN_NO_HYOUJI_JUN , ")
                .AppendLine("    ZENKAI_SHISAKU_BLOCK_NO_KAITEI_NO , ")
                .AppendLine("    LEVEL , ")
                .AppendLine("    SHUKEI_CODE , ")
                .AppendLine("    SIA_SHUKEI_CODE , ")
                .AppendLine("    GENCYO_CKD_KBN , ")
                .AppendLine("    MAKER_CODE , ")
                .AppendLine("    MAKER_NAME , ")
                .AppendLine("    BUHIN_NO , ")
                .AppendLine("    BUHIN_NO_KBN , ")
                .AppendLine("    BUHIN_NO_KAITEI_NO , ")
                .AppendLine("    EDA_BAN , ")
                .AppendLine("    BUHIN_NAME , ")
                .AppendLine("    SAISHIYOUFUKA , ")
                .AppendLine("    SHUTUZU_YOTEI_DATE , ")
                .AppendLine("    ZAISHITU_KIKAKU_1 , ")
                .AppendLine("    ZAISHITU_KIKAKU_2 , ")
                .AppendLine("    ZAISHITU_KIKAKU_3 , ")
                .AppendLine("    ZAISHITU_MEKKI , ")
                .AppendLine("    SHISAKU_BANKO_SURYO , ")
                .AppendLine("    SHISAKU_BANKO_SURYO_U , ")
                .AppendLine("    SHISAKU_BUHIN_HI , ")
                .AppendLine("    SHISAKU_KATA_HI , ")
                .AppendLine("    BIKOU , ")
                .AppendLine("    EDIT_TOUROKUBI , ")
                .AppendLine("    EDIT_TOUROKUJIKAN , ")
                .AppendLine("    KAITEI_HANDAN_FLG , ")
                .AppendLine("    SHISAKU_LIST_CODE , ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendLine("         BUHIN_NO = @BuhinNo  ")
                .AppendLine(" AND BUHIN_NO_KBN = @BuhinNoKbn ")
                .AppendLine(" ORDER BY ")
                .AppendLine("        UPDATED_DATE DESC , ")
                .AppendLine("        UPDATED_TIME DSC ")

            End With

            Return sql.ToString
        End Function
#End Region

#Region "SQL-SELECT 試作手配帳(最大行ID取得)"
        ''' <summary>
        ''' SQL-SELECT 試作手配帳(最大行ID取得)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlShisakuTehaiMaxGyouId() As String

            Dim sql As New System.Text.StringBuilder

            With sql
                .Remove(0, .Length)
                .AppendLine("    WITH  MAX_KIHON AS ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT  ")
                .AppendLine("               SHISAKU_EVENT_CODE ")
                .AppendLine("          ,SHISAKU_LIST_CODE ")
                .AppendLine("              ,MAX(SHISAKU_LIST_CODE_KAITEI_NO) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE  SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine("       AND SHISAKU_LIST_CODE =@ShisakuListCode ")
                .AppendLine("    GROUP BY  ")
                .AppendLine("               SHISAKU_EVENT_CODE ")
                .AppendLine("          ,SHISAKU_LIST_CODE ")
                .AppendLine("    ) ")
                .AppendLine("    SELECT MAX(GYOU_ID) AS GYOU_ID  ")
                .AppendLine("             , MAX(BUHIN_NO_HYOUJI_JUN) AS BUHIN_NO_HYOUJI_JUN ")
                .AppendLine("             , BASE.SHISAKU_EVENT_CODE ")
                .AppendLine("             , BASE.SHISAKU_LIST_CODE ")
                .AppendLine("             , BASE.SHISAKU_BUKA_CODE ")
                .AppendLine("             , BASE.SHISAKU_BLOCK_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON BASE WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    INNER JOIN  MAX_KIHON  MAX_BASE  WITH (NOLOCK, NOWAIT) ON ")
                .AppendLine("     BASE.SHISAKU_EVENT_CODE=MAX_BASE.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND BASE.SHISAKU_LIST_CODE=MAX_BASE.SHISAKU_LIST_CODE ")
                .AppendLine("    AND BASE.SHISAKU_LIST_CODE_KAITEI_NO=MAX_BASE.SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine("    WHERE ")
                .AppendLine("          BASE.SHISAKU_BUKA_CODE=@ShisakuBukaCode ")
                .AppendLine("      AND BASE.SHISAKU_BLOCK_NO = @ShisakuBlockNo")
                .AppendLine("    GROUP BY ")
                .AppendLine("             BASE.SHISAKU_EVENT_CODE ")
                .AppendLine("            ,BASE.SHISAKU_LIST_CODE ")
                .AppendLine("            ,BASE.SHISAKU_BUKA_CODE ")
                .AppendLine("            ,BASE.SHISAKU_BLOCK_NO ")

            End With

            Return sql.ToString
        End Function

#End Region

#Region "SQL-SELECT INSTL品番構成取得"
        ''' <summary>
        ''' SQL-SELECT INSTL品番構成取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlFindBuhinNoShisakuBuhinINSTL()
            Dim sql As New System.Text.StringBuilder

            With sql

                .AppendLine("    WITH MAX_BUHIN_EDIT_INSTL AS ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT      ")
                .AppendLine("            SHISAKU_EVENT_CODE, ")
                .AppendLine("            SHISAKU_BUKA_CODE, ")
                .AppendLine("            SHISAKU_BLOCK_NO, ")
                .AppendLine("            SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("            BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine("            INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("            INSU_SURYO AS TOTAL_INSU ")
                .AppendLine("    FROM  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL AS BUHIN_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE    BUHIN_INSTL.SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine("        AND  BUHIN_INSTL.SHISAKU_BUKA_CODE  = @ShisakuBukaCode ")
                .AppendLine("        AND  BUHIN_INSTL.SHISAKU_BLOCK_NO   = @ShisakuBlockNo ")
                .AppendLine("            AND  BUHIN_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO =  ")
                .AppendLine("            ( ")
                .AppendLine("            SELECT MAX(CONVERT(INT,COALESCE ( SHISAKU_BLOCK_NO_KAITEI_NO,'' ))) AS SHISAKU_BLOCK_NO_KAITEI_NO  ")
                .AppendLine("            FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT_INSTL AS MAX_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("            WHERE  MAX_INSTL.SHISAKU_EVENT_CODE = BUHIN_INSTL.SHISAKU_EVENT_CODE ")
                .AppendLine("                    AND  MAX_INSTL.SHISAKU_BUKA_CODE  = BUHIN_INSTL.SHISAKU_BUKA_CODE ")
                .AppendLine("                    AND  MAX_INSTL.SHISAKU_BLOCK_NO   = BUHIN_INSTL.SHISAKU_BLOCK_NO      ")
                .AppendLine("            ) ")
                .AppendLine("    ) ")
                .AppendLine("     ")
                .AppendLine("    SELECT  ")
                .AppendLine("            BUHIN_INSTL.SHISAKU_EVENT_CODE, ")
                .AppendLine("            BUHIN_INSTL.SHISAKU_BUKA_CODE, ")
                .AppendLine("            BUHIN_INSTL.SHISAKU_BLOCK_NO, ")
                .AppendLine("            BUHIN_INSTL.INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("            BUHIN_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("            BUHIN_INSTL.BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine("            BUHIN_EDIT.LEVEL , ")
                .AppendLine("            BUHIN_EDIT.BUHIN_NO , ")
                .AppendLine("            BUHIN_EDIT.BUHIN_NO_KBN, ")
                .AppendLine("            BUHIN_EDIT.BUHIN_NO_KAITEI_NO , ")
                .AppendLine("            BUHIN_EDIT.BUHIN_NAME , ")
                .AppendLine("            BUHIN_EDIT.SHUKEI_CODE , ")
                .AppendLine("            BUHIN_EDIT.SIA_SHUKEI_CODE , ")
                .AppendLine("            BUHIN_EDIT.MAKER_CODE , ")
                .AppendLine("            BUHIN_EDIT.SAISHIYOUFUKA , ")
                .AppendLine("            BUHIN_EDIT.SHUTUZU_YOTEI_DATE , ")
                .AppendLine("            BUHIN_EDIT.ZAISHITU_KIKAKU_1 , ")
                .AppendLine("            BUHIN_EDIT.ZAISHITU_KIKAKU_2 , ")
                .AppendLine("            BUHIN_EDIT.ZAISHITU_KIKAKU_3 , ")
                .AppendLine("            BUHIN_EDIT.ZAISHITU_MEKKI , ")
                .AppendLine("            BUHIN_EDIT.SHISAKU_BANKO_SURYO , ")
                .AppendLine("            BUHIN_EDIT.SHISAKU_BANKO_SURYO_U , ")
                .AppendLine("            BUHIN_EDIT.SHISAKU_BUHIN_HI, ")
                .AppendLine("            BUHIN_EDIT.SHISAKU_KATA_HI, ")
                .AppendLine("            BUHIN_EDIT.MAKER_NAME , ")
                .AppendLine("            BUHIN_INSTL.TOTAL_INSU, ")
                .AppendLine("            BLOCK_INSTL.INSU_SURYO , ")
                .AppendLine("        BLOCK_INSTL.INSTL_HINBAN_HYOUJI_JUN, ")
                .AppendLine("            BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO, ")
                .AppendLine("        BLOCK_INSTL.SHISAKU_GOUSYA, ")
                .AppendLine("        BLOCK_INSTL.INSTL_HINBAN, ")
                .AppendLine("        BLOCK_INSTL.INSTL_HINBAN_KBN ")
                .AppendLine("    FROM MAX_BUHIN_EDIT_INSTL AS BUHIN_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_BUHIN_EDIT AS BUHIN_EDIT WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    ON BUHIN_INSTL.SHISAKU_EVENT_CODE = BUHIN_EDIT.SHISAKU_EVENT_CODE ")
                .AppendLine("        AND BUHIN_INSTL.SHISAKU_BUKA_CODE  = BUHIN_EDIT.SHISAKU_BUKA_CODE ")
                .AppendLine("        AND BUHIN_INSTL.SHISAKU_BLOCK_NO   = BUHIN_EDIT.SHISAKU_BLOCK_NO ")
                .AppendLine("        AND BUHIN_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = BUHIN_EDIT.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("        AND BUHIN_INSTL.BUHIN_NO_HYOUJI_JUN = BUHIN_EDIT.BUHIN_NO_HYOUJI_JUN ")
                .AppendLine("    LEFT JOIN " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL AS BLOCK_INSTL WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    ON BUHIN_INSTL.SHISAKU_EVENT_CODE = BLOCK_INSTL.SHISAKU_EVENT_CODE ")
                .AppendLine("            AND BUHIN_INSTL.SHISAKU_BUKA_CODE  = BLOCK_INSTL.SHISAKU_BUKA_CODE ")
                .AppendLine("            AND BUHIN_INSTL.SHISAKU_BLOCK_NO   = BLOCK_INSTL.SHISAKU_BLOCK_NO ")
                .AppendLine("            AND BUHIN_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO = BLOCK_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO ")
                .AppendLine("            AND BUHIN_INSTL.INSTL_HINBAN_HYOUJI_JUN = BLOCK_INSTL.INSTL_HINBAN_HYOUJI_JUN ")
                .AppendLine("    WHERE BUHIN_EDIT.BUHIN_NO = @BuhinNo AND BUHIN_EDIT.BUHIN_NO_KBN= @BuhinNoKbn ")
                .AppendLine("    ORDER BY BUHIN_INSTL.SHISAKU_EVENT_CODE ,BUHIN_INSTL.SHISAKU_BUKA_CODE,BUHIN_INSTL.SHISAKU_BLOCK_NO,  ")
                .AppendLine("                     BUHIN_INSTL.SHISAKU_BLOCK_NO_KAITEI_NO,BLOCK_INSTL.SHISAKU_GOUSYA ")


            End With

            Return sql.ToString
        End Function

#End Region

#Region "SQL-INSERT 試作リストコード"
        ''' <summary>
        ''' SQL-INSERT 試作リストコード
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function InsShisakuListCode() As String
            Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder

            With sql
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE ")
                .AppendLine("SHISAKU_EVENT_CODE,  ")
                .AppendLine("SHISAKU_LIST_HYOJIJUN_NO,  ")
                .AppendLine("SHISAKU_LIST_CODE,  ")
                .AppendLine("SHISAKU_LIST_CODE_KAITEI_NO,  ")
                .AppendLine("SHISAKU_GROUP_NO,  ")
                .AppendLine("SHISAKU_KOUJI_KBN,  ")
                .AppendLine("SHISAKU_SEIHIN_KBN,  ")
                .AppendLine("SHISAKU_KOUJI_SHIREI_NO,  ")
                .AppendLine("SHISAKU_KOUJI_NO,  ")
                .AppendLine("SHISAKU_EVENT_NAME,  ")
                .AppendLine("SHISAKU_JIKYUHIN,  ")
                .AppendLine("SHISAKU_HIKAKUKEKKA,  ")
                .AppendLine("SHISAKU_SYUUKEI_CODE,  ")
                .AppendLine("SHISAKU_DAISU,  ")
                .AppendLine("SHISAKU_TEHAICHO_SAKUSEIBI,  ")
                .AppendLine("SHISAKU_TEHAICHO_SAKUSEIJIKAN,  ")
                .AppendLine("OLD_LIST_CODE,  ")
                .AppendLine("SHISAKU_DATA_TOUROKUBI,  ")
                .AppendLine("SHISAKU_DATA_TOUROKUJIKAN,  ")
                .AppendLine("RIREKI,  ")
                .AppendLine("SHISAKU_MEMO,  ")
                .AppendLine("SHISAKU_TENSOUBI,  ")
                .AppendLine("SHISAKU_TENSOUJIKAN,  ")
                .AppendLine("ZENKAI_KAITEIBI,  ")
                .AppendLine("SAISHIN_CHUSYUTUBI,  ")
                .AppendLine("SAISHIN_CHUSYUTUJIKAN,  ")
                .AppendLine("STATUS,  ")
                .AppendLine("CREATED_USER_ID,  ")
                .AppendLine("CREATED_DATE,  ")
                .AppendLine("CREATED_TIME,  ")
                .AppendLine("UPDATED_USER_ID,  ")
                .AppendLine("UPDATED_DATE,  ")
                .AppendLine("UPDATED_TIME ")
                .AppendLine(" VALUES  ")
                .AppendLine(" @ShisakuEventCode,  ")
                .AppendLine(" @ShisakuListHyojijunNo,  ")
                .AppendLine(" @ShisakuListCode,  ")
                .AppendLine(" @ShisakuListCodeKaiteiNo,  ")
                .AppendLine(" @ShisakuGroupNo,  ")
                .AppendLine(" @ShisakuKoujiKbn,  ")
                .AppendLine(" @ShisakuSeihinKbn,  ")
                .AppendLine(" @ShisakuKoujiShireiNo,  ")
                .AppendLine(" @ShisakuKoujiNo,  ")
                .AppendLine(" @ShisakuEventName,  ")
                .AppendLine(" @ShisakuJikyuhin,  ")
                .AppendLine(" @ShisakuHikakukekka,  ")
                .AppendLine(" @ShisakuSyuukeiCode,  ")
                .AppendLine(" @ShisakuDaisu,  ")
                .AppendLine(" @ShisakuTehaichoSakuseibi,  ")
                .AppendLine(" @ShisakuTehaichoSakuseijikan,  ")
                .AppendLine(" @OldListCode,  ")
                .AppendLine(" @ShisakuDataTourokubi,  ")
                .AppendLine(" @ShisakuDataTourokujikan,  ")
                .AppendLine(" @Rireki,  ")
                .AppendLine(" @ShisakuMemo,  ")
                .AppendLine(" @ShisakuTensoubi,  ")
                .AppendLine(" @ShisakuTensoujikan,  ")
                .AppendLine(" @ZenkaiKaiteibi,  ")
                .AppendLine(" @SaishinChusyutubi,  ")
                .AppendLine(" @SaishinChusyutujikan,  ")
                .AppendLine(" @Status,  ")
                .AppendLine(" @CreatedUserId,  ")
                .AppendLine(" @CreatedDate,  ")
                .AppendLine(" @CreatedTime,  ")
                .AppendLine(" @UpdatedUserId,  ")
                .AppendLine(" @UpdatedDate,  ")
                .AppendLine(" @UpdatedTime")

            End With

            Return sql.ToString

        End Function
#End Region

#Region "SQL-INSERT 試作手配帳(基本情報)"
        ''' <summary>
        ''' SQL-INSERT 試作手配帳(基本情報)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlInsTehaiBase() As String
            Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder

            With sql
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON  ( ")
                .AppendLine(" SHISAKU_EVENT_CODE, ")
                .AppendLine(" SHISAKU_LIST_CODE, ")
                .AppendLine(" SHISAKU_LIST_CODE_KAITEI_NO, ")
                .AppendLine(" SHISAKU_BUKA_CODE, ")
                .AppendLine(" SHISAKU_BLOCK_NO, ")
                .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")

                .AppendLine(" SORT_JUN, ")
                .AppendLine(" RIREKI, ")
                .AppendLine(" GYOU_ID, ")
                .AppendLine(" SENYOU_MARK, ")
                .AppendLine(" LEVEL, ")
                .AppendLine(" UNIT_KBN, ")
                .AppendLine(" BUHIN_NO, ")
                .AppendLine(" BUHIN_NO_KBN, ")
                .AppendLine(" BUHIN_NO_KAITEI_NO, ")
                .AppendLine(" EDA_BAN, ")
                .AppendLine(" BUHIN_NAME, ")
                .AppendLine(" SHUKEI_CODE, ")
                .AppendLine(" GENCYO_CKD_KBN, ")
                .AppendLine(" TEHAI_KIGOU, ")
                .AppendLine(" KOUTAN, ")
                .AppendLine(" TORIHIKISAKI_CODE, ")
                .AppendLine(" NOUBA, ")
                .AppendLine(" KYOUKU_SECTION, ")
                .AppendLine(" NOUNYU_SHIJIBI, ")
                .AppendLine(" TOTAL_INSU_SURYO, ")
                .AppendLine(" SAISHIYOUFUKA, ")
                .AppendLine(" SHUTUZU_YOTEI_DATE, ")
                .AppendLine(" SHUTUZU_JISEKI_DATE, ")
                .AppendLine(" SHUTUZU_JISEKI_KAITEI_NO, ")
                .AppendLine(" SHUTUZU_JISEKI_STSR_DHSTBA, ")
                .AppendLine(" SAISYU_SETSUHEN_DATE, ")
                .AppendLine(" SAISYU_SETSUHEN_KAITEI_NO, ")
                .AppendLine(" STSR_DHSTBA, ")
                .AppendLine(" TSUKURIKATA_SEISAKU, ")
                .AppendLine(" TSUKURIKATA_KATASHIYOU_1, ")
                .AppendLine(" TSUKURIKATA_KATASHIYOU_2, ")
                .AppendLine(" TSUKURIKATA_KATASHIYOU_3, ")
                .AppendLine(" TSUKURIKATA_TIGU, ")
                .AppendLine(" TSUKURIKATA_NOUNYU, ")
                .AppendLine(" TSUKURIKATA_KIBO, ")
                ''↑↑2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD END
                .AppendLine(" ZAISHITU_KIKAKU_1, ")
                .AppendLine(" ZAISHITU_KIKAKU_2, ")
                .AppendLine(" ZAISHITU_KIKAKU_3, ")
                .AppendLine(" ZAISHITU_MEKKI, ")
                .AppendLine(" SHISAKU_BANKO_SURYO, ")
                .AppendLine(" SHISAKU_BANKO_SURYO_U, ")
                '↓↓↓2014/12/29 メタル項目を追加 TES)張 ADD BEGIN
                .AppendLine(" MATERIAL_INFO_LENGTH, ")
                .AppendLine(" MATERIAL_INFO_WIDTH, ")
                .AppendLine(" ZAIRYO_SUNPO_X, ")
                .AppendLine(" ZAIRYO_SUNPO_Y, ")
                .AppendLine(" ZAIRYO_SUNPO_Z, ")
                .AppendLine(" ZAIRYO_SUNPO_XY, ")
                .AppendLine(" ZAIRYO_SUNPO_XZ, ")
                .AppendLine(" ZAIRYO_SUNPO_YZ, ")
                .AppendLine(" MATERIAL_INFO_ORDER_TARGET, ")
                .AppendLine(" MATERIAL_INFO_ORDER_TARGET_DATE, ")
                .AppendLine(" MATERIAL_INFO_ORDER_CHK, ")
                .AppendLine(" MATERIAL_INFO_ORDER_CHK_DATE, ")
                .AppendLine(" DATA_ITEM_KAITEI_NO, ")
                .AppendLine(" DATA_ITEM_AREA_NAME, ")
                .AppendLine(" DATA_ITEM_SET_NAME, ")
                .AppendLine(" DATA_ITEM_KAITEI_INFO, ")
                .AppendLine(" DATA_ITEM_DATA_PROVISION, ")
                .AppendLine(" DATA_ITEM_DATA_PROVISION_DATE, ")
                '↑↑↑2014/12/29 メタル項目を追加 TES)張 ADD END
                .AppendLine(" SHISAKU_BUHINN_HI, ")
                .AppendLine(" SHISAKU_KATA_HI, ")
                .AppendLine(" MAKER_CODE, ")
                .AppendLine(" BIKOU, ")
                .AppendLine(" BUHIN_NO_OYA, ")
                .AppendLine(" BUHIN_NO_KBN_OYA, ")
                .AppendLine(" SHISAKU_SEIHIN_KBN, ")
                .AppendLine(" CREATED_USER_ID, ")
                .AppendLine(" CREATED_DATE, ")
                .AppendLine(" CREATED_TIME, ")
                .AppendLine(" UPDATED_USER_ID, ")
                .AppendLine(" UPDATED_DATE, ")
                .AppendLine(" UPDATED_TIME ")
                .AppendLine(" ) ")
                .AppendLine(" VALUES ( ")
                .AppendLine(" @ShisakuEventCode, ")
                .AppendLine(" @ShisakuListCode, ")
                .AppendLine(" @ShisakuListCodeKaiteiNo, ")
                .AppendLine(" @ShisakuBukaCode, ")
                .AppendLine(" @ShisakuBlockNo, ")
                .AppendLine(" @BuhinNoHyoujiJun, ")
                .AppendLine(" @SortJun, ")
                .AppendLine(" @Rireki, ")
                .AppendLine(" @GyouId, ")
                .AppendLine(" @SenyouMark, ")
                .AppendLine(" @Level, ")
                .AppendLine(" @UnitKbn, ")
                .AppendLine(" @BuhinNo, ")
                .AppendLine(" @BuhinNoKbn, ")
                .AppendLine(" @BuhinNoKaiteiNo, ")
                .AppendLine(" @EdaBan, ")
                .AppendLine(" @BuhinName, ")
                .AppendLine(" @ShukeiCode, ")
                .AppendLine(" @GencyoCkdKbn, ")
                .AppendLine(" @TehaiKigou, ")
                .AppendLine(" @Koutan, ")
                .AppendLine(" @TorihikisakiCode, ")
                .AppendLine(" @Nouba, ")
                .AppendLine(" @KyoukuSection, ")
                .AppendLine(" @NounyuShijibi, ")
                .AppendLine(" @TotalInsuSuryo, ")
                .AppendLine(" @Saishiyoufuka, ")
                .AppendLine(" @ShutuzuYoteiDate, ")
                .AppendLine(" @ShutuzuJisekiDate, ")
                .AppendLine(" @ShutuzuJisekiKaiteiNo, ")
                .AppendLine(" @ShutuzuJisekiStsrDhstba, ")
                .AppendLine(" @SaisyuSetsuhenDate, ")
                .AppendLine(" @SaisyuSetsuhenKaiteiNo, ")
                .AppendLine(" @StsrDhstba, ")
                .AppendLine(" @TsukurikataSeisaku, ")
                .AppendLine(" @TsukurikataKatashiyou1, ")
                .AppendLine(" @TsukurikataKatashiyou2, ")
                .AppendLine(" @TsukurikataKatashiyou3, ")
                .AppendLine(" @TsukurikataTigu, ")
                .AppendLine(" @TsukurikataNounyu, ")
                .AppendLine(" @TsukurikataKibo, ")
                ''↑↑2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD END
                .AppendLine(" @ZaishituKikaku1, ")
                .AppendLine(" @ZaishituKikaku2, ")
                .AppendLine(" @ZaishituKikaku3, ")
                .AppendLine(" @ZaishituMekki, ")
                .AppendLine(" @ShisakuBankoSuryo, ")
                .AppendLine(" @ShisakuBankoSuryoU, ")
                '↓↓↓2014/12/29 メタル項目を追加 TES)張 ADD BEGIN
                .AppendLine(" @MaterialInfoLength, ")
                .AppendLine(" @MaterialInfoWidth, ")
                .AppendLine(" @ZairyoSunpoX, ")
                .AppendLine(" @ZairyoSunpoY, ")
                .AppendLine(" @ZairyoSunpoZ, ")
                .AppendLine(" @ZairyoSunpoXy, ")
                .AppendLine(" @ZairyoSunpoXz, ")
                .AppendLine(" @ZairyoSunpoYz, ")
                .AppendLine(" @MaterialInfoOrderTarget, ")
                .AppendLine(" @MaterialInfoOrderTargetDate, ")
                .AppendLine(" @MaterialInfoOrderChk, ")
                .AppendLine(" @MaterialInfoOrderChkDate, ")
                .AppendLine(" @DataItemKaiteiNo, ")
                .AppendLine(" @DataItemAreaName, ")
                .AppendLine(" @DataItemSetName, ")
                .AppendLine(" @DataItemKaiteiInfo, ")
                .AppendLine(" @DataItemDataProvision, ")
                .AppendLine(" @DataItemDataProvisionDate, ")
                '↑↑↑2014/12/29 メタル項目を追加 TES)張 ADD END
                .AppendLine(" @ShisakuBuhinnHi, ")
                .AppendLine(" @ShisakuKataHi, ")
                .AppendLine(" @MakerCode, ")
                .AppendLine(" @Bikou, ")
                .AppendLine(" @BuhinNoOya, ")
                .AppendLine(" @BuhinNoKbnOya, ")
                .AppendLine(" @ShisakuSeihinKbn, ")
                .AppendLine(" @CreatedUserId, ")
                .AppendLine(" @CreatedDate, ")
                .AppendLine(" @CreatedTime, ")
                .AppendLine(" @UpdatedUserId, ")
                .AppendLine(" @UpdatedDate, ")
                .AppendLine(" @UpdatedTime ")
                .AppendLine(" ) ")

            End With

            Return sql.ToString

        End Function


#End Region

#Region "SQL-INSERT 試作手配帳(号車情報)"
        ''' <summary>
        ''' SQL-INSERT 試作手配帳(号車情報)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlInsTehaichoGousya() As String
            Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder

            With sql
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ( ")
                .AppendLine(" SHISAKU_EVENT_CODE, ")
                .AppendLine(" SHISAKU_LIST_CODE, ")
                .AppendLine(" SHISAKU_LIST_CODE_KAITEI_NO, ")
                .AppendLine(" SHISAKU_BUKA_CODE, ")
                .AppendLine(" SHISAKU_BLOCK_NO, ")
                .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine(" SORT_JUN, ")
                .AppendLine(" GYOU_ID, ")
                .AppendLine(" SHISAKU_GOUSYA_HYOUJI_JUN, ")
                .AppendLine(" SHISAKU_GOUSYA, ")
                .AppendLine(" INSU_SURYO, ")
                .AppendLine(" M_NOUNYU_SHIJIBI, ")
                .AppendLine(" T_NOUNYU_SHIJIBI, ")
                .AppendLine(" CREATED_USER_ID, ")
                .AppendLine(" CREATED_DATE, ")
                .AppendLine(" CREATED_TIME, ")
                .AppendLine(" UPDATED_USER_ID, ")
                .AppendLine(" UPDATED_DATE, ")
                .AppendLine(" UPDATED_TIME ")
                .AppendLine(" ) ")
                .AppendLine(" VALUES ( ")
                .AppendLine(" @ShisakuEventCode, ")
                .AppendLine(" @ShisakuListCode, ")
                .AppendLine(" @ShisakuListCodeKaiteiNo, ")
                .AppendLine(" @ShisakuBukaCode, ")
                .AppendLine(" @ShisakuBlockNo, ")
                .AppendLine(" @BuhinNoHyoujiJun, ")
                .AppendLine(" @SortJun, ")
                .AppendLine(" @GyouId, ")
                .AppendLine(" @ShisakuGousyaHyoujiJun, ")
                .AppendLine(" @ShisakuGousya, ")
                .AppendLine(" @InsuSuryo, ")
                .AppendLine(" @MNounyuShijibi, ")
                .AppendLine(" @TNounyuShijibi, ")
                .AppendLine(" @CreatedUserId, ")
                .AppendLine(" @CreatedDate, ")
                .AppendLine(" @CreatedTime, ")
                .AppendLine(" @UpdatedUserId, ")
                .AppendLine(" @UpdatedDate, ")
                .AppendLine(" @UpdatedTime ")
                .AppendLine(" ) ")

            End With

            Return sql.ToString

        End Function

#End Region

#Region "SQL-UPDATE 試作リストコード (試作工事No)"
        ''' <summary>
        ''' SQL-UPDATE 試作リストコード (試作工事No)"
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlUpdShisakuList_koujiNo() As String
            Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder

            With sql
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_LISTCODE  ")
                .AppendLine("	SET  SHISAKU_KOUJI_NO = @SHISAKU_KOUJI_NO  ")
                .AppendLine(" ,STATUS = @STATUS ")
                .AppendLine("		,UPDATED_USER_ID  = @UPDATED_USER_ID ")
                .AppendLine("		,UPDATED_DATE	  = @UPDATED_DATE  ")
                .AppendLine("		,UPDATED_TIME	  = @UPDATED_TIME ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @SHISAKU_EVENT_CODE  ")
                .AppendLine("   AND SHISAKU_LIST_CODE = @SHISAKU_LIST_CODE  ")
                .AppendLine("   AND SHISAKU_LIST_CODE_KAITEI_NO = @SHISAKU_LIST_CODE_KAITEI_NO")
            End With

            Return sql.ToString

        End Function

#End Region

#Region "SQL-UPDATE 試作手配帳(基本情報)（イベントコード・リストコード）"
        ''' <summary>
        ''' SQL-UPDATE 試作手配帳(基本情報)（イベントコード・リストコード）
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlUpdTehaiBaseEventListKey() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine("UPDATE  ")
                .AppendLine("      " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON   ")
                .AppendLine(" SET   UNIT_KBN =@UnitKbn ")
                .AppendLine("       ,  SHISAKU_SEIHIN_KBN = @ShisakuSeihinKbn  ")
                .AppendLine("       ,   UPDATED_USER_ID =   @UpdatedUserId ")
                .AppendLine("       ,   UPDATED_DATE =   @UpdatedDate ")
                .AppendLine("       ,   UPDATED_TIME  =   @UpdatedTime ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE =@ShisakuEventCode ")
                .AppendLine("  AND  SHISAKU_LIST_CODE = @ShisakuListCode ")
                .AppendLine("  AND  SHISAKU_LIST_CODE_KAITEI_NO =  @ShisakuListCodeKaiteiNo")
            End With

            Return sql.ToString

        End Function
#End Region

#Region "SQL-UPDATE 試作手配帳(基本情報)"
        ''' <summary>
        ''' SQL-UPDATE 試作手配帳基本情報
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlUpdTehaichoBase() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" SET ")
                '.AppendLine("    SHISAKU_EVENT_CODE =   @ShisakuEventCode ")
                '.AppendLine(",   SHISAKU_LIST_CODE =   @ShisakuListCode ")
                '.AppendLine(",   SHISAKU_LIST_CODE_KAITEI_NO =   @ShisakuListCodeKaiteiNo ")
                '.AppendLine(",   SHISAKU_BUKA_CODE =   @ShisakuBukaCode ")
                '.AppendLine(",   SHISAKU_BLOCK_NO =   @ShisakuBlockNo ")
                '.AppendLine(",   BUHIN_NO_HYOUJI_JUN =   @BuhinNoHyoujiJun ")
                '.AppendLine(",   UNIT_KBN =   @UnitKbn ")

                .AppendLine("    SORT_JUN =   @SortJun ")
                .AppendLine(",   RIREKI =   @Rireki ")
                .AppendLine(",   GYOU_ID =   @GyouId ")
                .AppendLine(",   SENYOU_MARK =   @SenyouMark ")
                .AppendLine(",   LEVEL =   @Level ")

                .AppendLine(",   BUHIN_NO =   @BuhinNo ")
                .AppendLine(",   BUHIN_NO_KBN =   @BuhinNoKbn ")
                .AppendLine(",   BUHIN_NO_KAITEI_NO =   @BuhinNoKaiteiNo ")
                .AppendLine(",   EDA_BAN =   @EdaBan ")
                .AppendLine(",   BUHIN_NAME =   @BuhinName ")
                .AppendLine(",   SHUKEI_CODE =   @ShukeiCode ")

                .AppendLine(",   TEHAI_KIGOU =   @TehaiKigou ")
                .AppendLine(",   KOUTAN =   @Koutan ")
                .AppendLine(",   TORIHIKISAKI_CODE =   @TorihikisakiCode ")
                .AppendLine(",   NOUBA =   @Nouba ")
                .AppendLine(",   KYOUKU_SECTION =   @KyoukuSection ")
                .AppendLine(",   NOUNYU_SHIJIBI =   @NounyuShijibi ")
                .AppendLine(",   TOTAL_INSU_SURYO =   @TotalInsuSuryo ")
                .AppendLine(",   SAISHIYOUFUKA =   @Saishiyoufuka ")
                .AppendLine(",   SHUTUZU_YOTEI_DATE =   @ShutuzuYoteiDate ")

                .AppendLine(",   SHUTUZU_JISEKI_DATE = @ShutuzuJisekiDate ")
                .AppendLine(",   SHUTUZU_JISEKI_KAITEI_NO = @ShutuzuJisekiKaiteiNo ")
                .AppendLine(",   SHUTUZU_JISEKI_STSR_DHSTBA = @ShutuzuJisekiStsrDhstba ")
                .AppendLine(",   SAISYU_SETSUHEN_DATE = @SaisyuSetsuhenDate ")
                .AppendLine(",   SAISYU_SETSUHEN_KAITEI_NO = @SaisyuSetsuhenKaiteiNo ")
                .AppendLine(",   STSR_DHSTBA = @StsrDhstba ")

                .AppendLine(",   ZAIRYO_SUNPO_X = @ZairyoSunpoX ")
                .AppendLine(",   ZAIRYO_SUNPO_Y = @ZairyoSunpoY ")
                .AppendLine(",   ZAIRYO_SUNPO_Z = @ZairyoSunpoZ ")
                .AppendLine(",   ZAIRYO_SUNPO_XY = @ZairyoSunpoXy ")
                .AppendLine(",   ZAIRYO_SUNPO_XZ = @ZairyoSunpoXz ")
                .AppendLine(",   ZAIRYO_SUNPO_YZ = @ZairyoSunpoYz ")

                ''↓↓2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
                .AppendLine(",   TSUKURIKATA_SEISAKU =   @TsukurikataSeisaku ")
                .AppendLine(",   TSUKURIKATA_KATASHIYOU_1 =   @TsukurikataKatashiyou1 ")
                .AppendLine(",   TSUKURIKATA_KATASHIYOU_2 =   @TsukurikataKatashiyou2 ")
                .AppendLine(",   TSUKURIKATA_KATASHIYOU_3 =   @TsukurikataKatashiyou3 ")
                .AppendLine(",   TSUKURIKATA_TIGU =   @TsukurikataTigu ")
                .AppendLine(",   TSUKURIKATA_NOUNYU =   @TsukurikataNounyu ")
                .AppendLine(",   TSUKURIKATA_KIBO =   @TsukurikataKibo ")
                ''↑↑2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD END
                .AppendLine(",   ZAISHITU_KIKAKU_1 =   @ZaishituKikaku1 ")
                .AppendLine(",   ZAISHITU_KIKAKU_2 =   @ZaishituKikaku2 ")
                .AppendLine(",   ZAISHITU_KIKAKU_3 =   @ZaishituKikaku3 ")
                .AppendLine(",   ZAISHITU_MEKKI =   @ZaishituMekki ")
                .AppendLine(",   SHISAKU_BANKO_SURYO =   @ShisakuBankoSuryo ")
                .AppendLine(",   SHISAKU_BANKO_SURYO_U =   @ShisakuBankoSuryoU ")
                '↓↓↓2014/12/29 メタル項目を追加 TES)張 ADD BEGIN
                .AppendLine(",   MATERIAL_INFO_LENGTH = @MaterialInfoLength ")
                .AppendLine(",   MATERIAL_INFO_WIDTH = @MaterialInfoWidth ")
                .AppendLine(",   MATERIAL_INFO_ORDER_TARGET = @MaterialInfoOrderTarget ")
                .AppendLine(",   MATERIAL_INFO_ORDER_TARGET_DATE = @MaterialInfoOrderTargetDate ")
                .AppendLine(",   MATERIAL_INFO_ORDER_CHK = @MaterialInfoOrderChk ")
                .AppendLine(",   MATERIAL_INFO_ORDER_CHK_DATE = @MaterialInfoOrderChkDate ")
                .AppendLine(",   DATA_ITEM_KAITEI_NO = @DataItemKaiteiNo ")
                .AppendLine(",   DATA_ITEM_AREA_NAME = @DataItemAreaName ")
                .AppendLine(",   DATA_ITEM_SET_NAME = @DataItemSetName ")
                .AppendLine(",   DATA_ITEM_KAITEI_INFO = @DataItemKaiteiInfo ")
                .AppendLine(",   DATA_ITEM_DATA_PROVISION = @DataItemDataProvision ")
                .AppendLine(",   DATA_ITEM_DATA_PROVISION_DATE = @DataItemDataProvisionDate ")
                .AppendLine(",   HENKATEN = @Henkaten ")
                '↑↑↑2014/12/29 メタル項目を追加 TES)張 ADD END
                .AppendLine(",   SHISAKU_BUHINN_HI =   @ShisakuBuhinnHi ")
                .AppendLine(",   SHISAKU_KATA_HI =   @ShisakuKataHi ")
                .AppendLine(",   MAKER_CODE =   @MakerCode ")
                .AppendLine(",   BIKOU =   @Bikou ")
                .AppendLine(",   BUHIN_NO_OYA =   @BuhinNoOya ")
                .AppendLine(",   BUHIN_NO_KBN_OYA =   @BuhinNoKbnOya ")
                .AppendLine(",   SHISAKU_SEIHIN_KBN =   @ShisakuSeihinKbn ")
                .AppendLine(",   UPDATED_USER_ID =   @UpdatedUserId ")
                .AppendLine(",   UPDATED_DATE =   @UpdatedDate ")
                .AppendLine(",   UPDATED_TIME  =   @UpdatedTime ")
                .AppendLine(" WHERE ")
                .AppendLine("     SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode  ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
            End With

            Return sql.ToString
        End Function

#End Region

#Region "SQL-UPDATE 試作手配帳(号車情報)    メタル対応２次にて部課コード、ブロックNO、行ID無のMT納入指示日更新用にカスタマイズ"
        ''' <summary>
        ''' SQL-UPDATE 試作手配帳号車情報
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlUpdTehaichoGousya() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" SET ")
                .AppendLine("  　M_NOUNYU_SHIJIBI = @MNounyuShijibi ")
                .AppendLine(", 　T_NOUNYU_SHIJIBI = @TNounyuShijibi ")
                .AppendLine(",   UPDATED_USER_ID  = @UpdatedUserId ")
                .AppendLine(",   UPDATED_DATE     = @UpdatedDate ")
                .AppendLine(",   UPDATED_TIME     = @UpdatedTime ")
                .AppendLine(" WHERE ")
                .AppendLine("     SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode  ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo")
                .AppendLine(" AND SHISAKU_BLOCK_NO = '' ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = '' ")
                .AppendLine(" AND GYOU_ID = '' ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = 0 ")
                .AppendLine(" AND SHISAKU_GOUSYA = @ShisakuGousya ")

                '.AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                '.AppendLine(" SET ")
                '.AppendLine("  　SHISAKU_GOUSYA_HYOUJI_JUN = @ShisakuGousyaHyoujiJun ")
                '.AppendLine(" , 　SHISAKU_GOUSYA = @ShisakuGousya ")
                '.AppendLine(" , 　INSU_SURYO = @InsuSuryo ")
                '.AppendLine(" , 　M_NOUNYU_SHIJIBI = @MNounyuShijibi ")
                '.AppendLine(" , 　T_NOUNYU_SHIJIBI = @TNounyuShijibi ")
                '.AppendLine(",   UPDATED_USER_ID =   @UpdatedUserId ")
                '.AppendLine(",   UPDATED_DATE =   @UpdatedDate ")
                '.AppendLine(",   UPDATED_TIME  =   @UpdatedTime ")
                '.AppendLine(" WHERE ")
                '.AppendLine("         SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                '.AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode  ")
                '.AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo")
                '.AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                '.AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                '.AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
            End With

            Return sql.ToString

        End Function
#End Region

#Region "SQL-DELETE 試作手配帳基本情報(主キー削除)"
        ''' <summary>
        ''' SQL-DELETE 試作手配帳基本情報
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlDelTehaiBaseKey() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine("  DELETE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON")
                .AppendLine(" WHERE ")
                .AppendLine("         SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode  ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")

            End With

            Return sql.ToString
        End Function

#End Region

#Region "SQL-DELETE 試作手配帳号車情報(主キー削除)"
        ''' <summary>
        ''' SQL-DELETE 試作手配帳号車情報
        '''  とりあえずこれは使わない
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlDelTehaiGousyaKey() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine("  DELETE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendLine("         SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode  ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo ")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
            End With

            Return sql.ToString
        End Function

#End Region


#Region "SQL-DELETE 試作手配帳基本情報(ブロックNo単位削除)"
        ''' <summary>
        ''' SQL-DELETE 試作手配帳号車情報(ブロックNo単位削除)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlDelTehaiBaseBlockNo() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine("  DELETE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON ")
                .AppendLine(" WHERE ")
                .AppendLine("         SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode  ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo")
                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")

            End With

            Return sql.ToString
        End Function

#End Region

#Region "SQL-DELETE 試作手配帳号車情報(ブロックNo単位削除)"
        ''' <summary>
        ''' SQL-DELETE 試作手配帳号車情報
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlDelTehaiGousyaBlockNo() As String
            Dim sql As New System.Text.StringBuilder

            With sql

                .AppendLine("  DELETE FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA ")
                .AppendLine(" WHERE ")
                .AppendLine("         SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND SHISAKU_LIST_CODE = @ShisakuListCode  ")
                .AppendLine(" AND SHISAKU_LIST_CODE_KAITEI_NO = @ShisakuListCodeKaiteiNo")
                '                .AppendLine(" AND SHISAKU_BUKA_CODE = @ShisakuBukaCode ")
                .AppendLine(" AND SHISAKU_BLOCK_NO = @ShisakuBlockNo  ")

            End With

            Return sql.ToString
        End Function

#End Region

#Region "手配情報付加関連"
#Region "SQL-SELECT ３ヶ月インフォメーション"
        ''' <summary>
        ''' SQL-SELECT ３ヶ月インフォメーション
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlFindAS_KPSM10P() As String
            Dim sql As New System.Text.StringBuilder

            With sql

                .AppendLine(" SELECT ")
                .AppendLine("    KA , ")
                .AppendLine("    TRCD , ")
                .AppendLine("    KOMZBA, ")
                .AppendLine("    RNNO,  ")
                .AppendLine("    LTBN ,  ")
                .AppendLine("    TRCD , ")
                .AppendLine("    SNKM , ")
                .AppendLine("    BUBA_15  ")
                .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.AS_KPSM10P WITH (NOLOCK, NOWAIT)  ")
                .AppendLine(" WHERE SNKM =@Smkm ")
                .AppendLine("   AND BUBA_15 =  @Buba15 ")
                .AppendLine(" ORDER BY BUBA_15,UPDATED_DATE DESC , UPDATED_TIME DESC ")

            End With

            Return sql.ToString

        End Function

#End Region

#Region "SQL-SELCT パーツプライスリスト"
        ''' <summary>
        ''' SQL-SELCT パーツプライスリスト
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlFindAS_PARTSP() As String
            Dim sql As New System.Text.StringBuilder

            With sql

                .AppendLine(" SELECT  ")
                .AppendLine("    BUBA_13, ")
                .AppendLine("    KA , ")
                .AppendLine("    TRCD , ")
                .AppendLine("    KU_01 ,  ")
                .AppendLine("    BUNM  ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_PARTSP WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE BUBA_13 = @Buba13")
                .AppendLine(" ORDER BY BUBA_13, UPDATED_DATE DESC , UPDATED_TIME DESC ")

            End With

            Return sql.ToString
        End Function

#End Region

#Region "SQL-SELECT 海外生産マスタ"
        ''' <summary>
        ''' SQL-SELECT 海外生産マスタ
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlFindAS_GKPSM10P() As String
            Dim sql As New System.Text.StringBuilder

            With sql

                .AppendLine(" SELECT KOMZBA , ")
                .AppendLine("    RNNO , ")
                .AppendLine("    LTBN , ")
                .AppendLine("    TRCD , ")
                .AppendLine("    KA , ")
                .AppendLine("    TRCD , ")
                .AppendLine("    SNKM , ")
                .AppendLine("    BUBA_15  ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_GKPSM10P WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE SNKM = @Smkm ")
                .AppendLine("   AND BUBA_15 = @Buba15 ")
                .AppendLine(" ORDER BY BUBA_15,UPDATED_DATE DESC , UPDATED_TIME DESC ")

            End With

            Return sql.ToString

        End Function

#End Region

#Region "SQL-SELECT 部品マスタ"
        ''' <summary>
        ''' SQL-SELECT 部品マスタ
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlFindAS_BUHIN01() As String
            Dim sql As New System.Text.StringBuilder
            'ワイルドカードは使わない'
            With sql

                .AppendLine(" SELECT STSR ,   ")
                .AppendLine(" 		RSKM ,  ")
                .AppendLine("		DHSTBA ,   ")
                .AppendLine("		KDBA ,   ")
                .AppendLine("		KOTAN ,   ")
                .AppendLine("		MAKER ,	   ")
                .AppendLine("		GZZCP ,   ")
                .AppendLine("		SUBSTRING(GZZCP,1,10)	  ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_BUHIN01  WITH (NOLOCK, NOWAIT) ")
                .AppendLine("		WHERE GZZCP = @Gzzcp10	  ")
                .AppendLine("  ORDER BY GZZCP ,  UPDATED_DATE DESC , UPDATED_TIME DESC ")

            End With

            Return sql.ToString

        End Function

#End Region

#Region "SQL-SELECT 属性管理(開発符号毎)"
        ''' <summary>
        ''' SQL-SELECT 属性管理(開発符号毎)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlFindValueDev() As String
            Dim sql As New System.Text.StringBuilder
            'ワイルドカードは使わない'
            With sql

                .AppendLine(" SELECT     ")
                .AppendLine("		KAIHATSU_FUGO ,   ")
                .AppendLine("		BUHIN_NO ,   ")
                .AppendLine("		AN_LEVEL ,	   ")
                .AppendLine(" 		FHI_NOMINATE_KOTAN ,  ")
                .AppendLine("		TORIHIKISAKI_CODE    ")
                .AppendLine(" FROM T_VALUE_DEV WITH (NOLOCK, NOWAIT)   ")
                .AppendLine(" WHERE                          ")
                .AppendLine("         BUHIN_NO = @BuhinNo10	  ")
                .AppendLine(" AND    AN_LEVEL = '1'   ")
                .AppendLine(" ORDER BY BUHIN_NO,UPDATED_DATE DESC , UPDATED_TIME DESC ")
                '.AppendLine("            KAIHATSU_FUGO = @KaihatsuFugo  ")
                '.AppendLine("	AND	  BUHIN_NO = @BuhinNo10	  ")

            End With

            Return sql.ToString

        End Function

#End Region

#End Region

    End Class

End Namespace