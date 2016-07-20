Imports ShisakuCommon
Namespace YosanSetteiBuhinEdit.Dao

    Public Class SqlYosanSetteiBuhinEdit

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

#Region "SQL-SELECT 予算設定部品表一覧取得"
        ''' <summary>
        ''' SQL-予算設定部品表一覧を返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlAllBaseInfo() As String
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT * ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN KIHON WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendLine(" SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND YOSAN_LIST_CODE = @YosanListCode ")
                .AppendLine("ORDER BY ")
                .AppendLine("YOSAN_BLOCK_NO, ")
                .AppendLine("SORT_JUN ")
            End With
            Return sql.ToString
        End Function

#End Region

#Region "SQL-SELECT 予算設定部品表主キー検索"
        ''' <summary>
        ''' SQL-SELECT 予算設定部品表主キー検索
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlFindPkTehaiBase() As String

            Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT SHISAKU_EVENT_CODE,YOSAN_LIST_CODE, YOSAN_BLOCK_NO, YOSAN_BUKA_CODE, BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendLine("         SHISAKU_EVENT_CODE = '{0}' ")
                .AppendLine(" AND YOSAN_LIST_CODE = '{1}'  ")
                .AppendLine(" AND YOSAN_BLOCK_NO = '{2}' ")
                .AppendLine(" AND YOSAN_BUKA_CODE = '{3}' ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = {4} ")
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

#Region "SQL-SELECT 予算設定部品表(最大行ID取得)"
        ''' <summary>
        ''' SQL-SELECT 予算設定部品表(最大行ID取得)
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
                .AppendLine("          ,YOSAN_LIST_CODE ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE  SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine("       AND YOSAN_LIST_CODE =@YosanListCode ")
                .AppendLine("    GROUP BY  ")
                .AppendLine("               SHISAKU_EVENT_CODE ")
                .AppendLine("          ,YOSAN_LIST_CODE ")
                .AppendLine("    ) ")
                .AppendLine("    SELECT MAX(GYOU_ID) AS GYOU_ID  ")
                .AppendLine("             , MAX(BUHIN_NO_HYOUJI_JUN) AS BUHIN_NO_HYOUJI_JUN ")
                .AppendLine("             , BASE.SHISAKU_EVENT_CODE ")
                .AppendLine("             , BASE.YOSAN_LIST_CODE ")
                .AppendLine("             , BASE.YOSAN_BUKA_CODE ")
                .AppendLine("             , BASE.YOSAN_BLOCK_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN BASE WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    INNER JOIN  MAX_KIHON  MAX_BASE  WITH (NOLOCK, NOWAIT) ON ")
                .AppendLine("     BASE.SHISAKU_EVENT_CODE=MAX_BASE.SHISAKU_EVENT_CODE ")
                .AppendLine("    AND BASE.YOSAN_LIST_CODE=MAX_BASE.YOSAN_LIST_CODE ")
                .AppendLine("    WHERE ")
                .AppendLine("          BASE.YOSAN_BUKA_CODE=@YosanBukaCode ")
                .AppendLine("      AND BASE.YOSAN_BLOCK_NO = @YosanBlockNo")
                .AppendLine("    GROUP BY ")
                .AppendLine("             BASE.SHISAKU_EVENT_CODE ")
                .AppendLine("            ,BASE.YOSAN_LIST_CODE ")
                .AppendLine("            ,BASE.YOSAN_BUKA_CODE ")
                .AppendLine("            ,BASE.YOSAN_BLOCK_NO ")

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
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_LISTCODE ")
                .AppendLine("SHISAKU_EVENT_CODE,  ")
                .AppendLine("YOSAN_LIST_HYOJIJUN_NO,  ")
                .AppendLine("YOSAN_LIST_CODE,  ")
                .AppendLine("YOSAN_GROUP_NO,  ")
                .AppendLine("YOSAN_KOUJI_KBN,  ")
                .AppendLine("YOSAN_SEIHIN_KBN,  ")
                .AppendLine("YOSAN_KOUJI_SHIREI_NO,  ")
                .AppendLine("YOSAN_KOUJI_NO,  ")
                .AppendLine("YOSAN_EVENT_NAME,  ")
                .AppendLine("YOSAN_JIKYUHIN,  ")
                .AppendLine("YOSAN_HIKAKUKEKKA,  ")
                .AppendLine("YOSAN_SYUUKEI_CODE,  ")
                .AppendLine("YOSAN_DAISU,  ")
                .AppendLine("YOSAN_BUHIN_SAKUSEIBI,  ")
                .AppendLine("YOSAN_BUHIN_SAKUSEIJIKAN,  ")
                .AppendLine("STATUS,  ")
                .AppendLine("LAST_SAISHINBI,  ")
                .AppendLine("LAST_SAISHINJIKAN,  ")
                .AppendLine("LAST_WARITUKEBI,  ")
                .AppendLine("LAST_WARITUKEJIKAN,  ")
                .AppendLine("LAST_FUNCTION_TANKA_SETTEIBI,  ")
                .AppendLine("SLAST_FUNCTION_TANKA_SETTEIJIKAN,  ")
                .AppendLine("LAST_TANKA_AUTO_SETTEIBI,  ")
                .AppendLine("LAST_TANKA_AUTO_SETTEIJIKAN,  ")
                .AppendLine("LAST_EXCEL_IMPORTBI,  ")
                .AppendLine("LAST_EXCEL_IMPORTJIKAN,  ")
                .AppendLine("LAST_BUHIN_HI_HASUBI,  ")
                .AppendLine("LAST_BUHIN_HI_HASUJIKAN,  ")
                .AppendLine("YOSAN_MEMO,  ")
                .AppendLine("CREATED_USER_ID,  ")
                .AppendLine("CREATED_DATE,  ")
                .AppendLine("CREATED_TIME,  ")
                .AppendLine("UPDATED_USER_ID,  ")
                .AppendLine("UPDATED_DATE,  ")
                .AppendLine("UPDATED_TIME ")
                .AppendLine(" VALUES  ")
                .AppendLine(" @ShisakuEventCode,  ")
                .AppendLine(" @YosanListHyojijunNo,  ")
                .AppendLine(" @YosanListCode,  ")
                .AppendLine(" @YosanGroupNo,  ")
                .AppendLine(" @YosanKoujiKbn,  ")
                .AppendLine(" @YosanSeihinKbn,  ")
                .AppendLine(" @YosanKoujiShireiNo,  ")
                .AppendLine(" @YosanKoujiNo,  ")
                .AppendLine(" @YosanEventName,  ")
                .AppendLine(" @YosanJikyuhin,  ")
                .AppendLine(" @YosanHikakukekka,  ")
                .AppendLine(" @YosanSyuukeiCode,  ")
                .AppendLine(" @YosanDaisu,  ")
                .AppendLine(" @YosanBuhinSakuseibi,  ")
                .AppendLine(" @YosanBuhinSakuseijikan,  ")
                .AppendLine(" @Status,  ")
                .AppendLine(" @LastSaishinbi,  ")
                .AppendLine(" @LastSaishinjikan,  ")
                .AppendLine(" @LastWaritukebi,  ")
                .AppendLine(" @LastWaritukejikan,  ")
                .AppendLine(" @LastFunctionTankaSetteibi,  ")
                .AppendLine(" @LastFunctionTankaSetteijikan,  ")
                .AppendLine(" @LastTankaAutoSetteibi,  ")
                .AppendLine(" @LastTankaAutoSetteijikan,  ")
                .AppendLine(" @LastExcelImportbi,  ")
                .AppendLine(" @LastExcelImportjikan,  ")
                .AppendLine(" @LastBuhinHiHasubi,  ")
                .AppendLine(" @LastBuhinHiHasujikan,  ")
                .AppendLine(" @YosanMemo,  ")
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

#Region "SQL-INSERT 予算設定部品表"
        ''' <summary>
        ''' SQL-INSERT 予算設定部品表
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlInsTehaiBase() As String
            Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder

            With sql
                .AppendLine(" INSERT INTO " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN  ( ")
                .AppendLine(" SHISAKU_EVENT_CODE, ")
                .AppendLine(" YOSAN_LIST_CODE, ")
                .AppendLine(" YOSAN_BUKA_CODE, ")
                .AppendLine(" YOSAN_BLOCK_NO, ")
                .AppendLine(" BUHIN_NO_HYOUJI_JUN, ")
                .AppendLine(" YOSAN_SORT_JUN, ")

                .AppendLine(" YOSAN_GYOU_ID, ")
                .AppendLine(" YOSAN_LEVEL, ")
                .AppendLine(" YOSAN_SHUKEI_CODE, ")
                .AppendLine(" YOSAN_SIA_SHUKEI_CODE, ")
                .AppendLine(" YOSAN_BUHIN_NO, ")
                .AppendLine(" YOSAN_BUHIN_NAME, ")
                .AppendLine(" YOSAN_INSU, ")
                .AppendLine(" YOSAN_MAKER_CODE, ")
                .AppendLine(" YOSAN_KYOUKU_SECTION, ")
                .AppendLine(" YOSAN_KOUTAN, ")
                .AppendLine(" YOSAN_TSUKURIKATA_SEISAKU, ")
                .AppendLine(" YOSAN_TSUKURIKATA_KATASHIYOU_1, ")
                .AppendLine(" YOSAN_TSUKURIKATA_KATASHIYOU_2, ")
                .AppendLine(" YOSAN_TSUKURIKATA_KATASHIYOU_3, ")
                .AppendLine(" YOSAN_TSUKURIKATA_TIGU, ")
                .AppendLine(" YOSAN_TSUKURIKATA_NOUNYU, ")
                .AppendLine(" YOSAN_TSUKURIKATA_KIBO, ")
                .AppendLine(" YOSAN_SHISAKU_BUHIN_HI, ")
                .AppendLine(" YOSAN_SHISAKU_KATA_HI, ")
                .AppendLine(" YOSAN_BUHIN_NOTE, ")
                .AppendLine(" YOSAN_BIKOU, ")
                .AppendLine(" YOSAN_KONKYO_KOKUGAI_KBN, ")
                .AppendLine(" YOSAN_KONKYO_MIX_BUHIN_HI, ")
                .AppendLine(" YOSAN_KONKYO_INYOU_MIX_BUHIN_HI, ")
                .AppendLine(" YOSAN_KONKYO_KEISU_1, ")
                .AppendLine(" YOSAN_KONKYO_KOUHOU, ")
                .AppendLine(" YOSAN_WARITUKE_BUHIN_HI, ")
                .AppendLine(" YOSAN_WARITUKE_KEISU_2, ")
                .AppendLine(" YOSAN_WARITUKE_BUHIN_HI_TOTAL, ")
                .AppendLine(" YOSAN_WARITUKE_KATA_HI, ")
                .AppendLine(" YOSAN_KOUNYU_KIBOU_TANKA, ")
                .AppendLine(" YOSAN_KOUNYU_KIBOU_BUHIN_HI, ")
                .AppendLine(" YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL, ")
                .AppendLine(" YOSAN_KOUNYU_KIBOU_KATA_HI, ")
                .AppendLine(" KAKO_1_RYOSAN_TANKA       , ")
                .AppendLine(" KAKO_1_WARITUKE_BUHIN_HI  , ")
                .AppendLine(" KAKO_1_WARITUKE_KATA_HI   , ")
                .AppendLine(" KAKO_1_WARITUKE_KOUHOU    , ")
                .AppendLine(" KAKO_1_MAKER_BUHIN_HI     , ")
                .AppendLine(" KAKO_1_MAKER_KATA_HI      , ")
                .AppendLine(" KAKO_1_MAKER_KOUHOU       , ")
                .AppendLine(" KAKO_1_SHINGI_BUHIN_HI    , ")
                .AppendLine(" KAKO_1_SHINGI_KATA_HI     , ")
                .AppendLine(" KAKO_1_SHINGI_KOUHOU      , ")
                .AppendLine(" KAKO_1_KOUNYU_KIBOU_TANKA , ")
                .AppendLine(" KAKO_1_KOUNYU_TANKA       , ")
                .AppendLine(" KAKO_1_SHIKYU_HIN         , ")
                .AppendLine(" KAKO_1_KOUJI_SHIREI_NO    , ")
                .AppendLine(" KAKO_1_EVENT_NAME         , ")
                .AppendLine(" KAKO_1_HACHU_BI           , ")
                .AppendLine(" KAKO_1_KENSHU_BI          , ")
                .AppendLine(" KAKO_2_RYOSAN_TANKA       , ")
                .AppendLine(" KAKO_2_WARITUKE_BUHIN_HI  , ")
                .AppendLine(" KAKO_2_WARITUKE_KATA_HI   , ")
                .AppendLine(" KAKO_2_WARITUKE_KOUHOU    , ")
                .AppendLine(" KAKO_2_MAKER_BUHIN_HI     , ")
                .AppendLine(" KAKO_2_MAKER_KATA_HI      , ")
                .AppendLine(" KAKO_2_MAKER_KOUHOU       , ")
                .AppendLine(" KAKO_2_SHINGI_BUHIN_HI    , ")
                .AppendLine(" KAKO_2_SHINGI_KATA_HI     , ")
                .AppendLine(" KAKO_2_SHINGI_KOUHOU      , ")
                .AppendLine(" KAKO_2_KOUNYU_KIBOU_TANKA , ")
                .AppendLine(" KAKO_2_KOUNYU_TANKA       , ")
                .AppendLine(" KAKO_2_SHIKYU_HIN         , ")
                .AppendLine(" KAKO_2_KOUJI_SHIREI_NO    , ")
                .AppendLine(" KAKO_2_EVENT_NAME         , ")
                .AppendLine(" KAKO_2_HACHU_BI           , ")
                .AppendLine(" KAKO_2_KENSHU_BI          , ")
                .AppendLine(" KAKO_3_RYOSAN_TANKA       , ")
                .AppendLine(" KAKO_3_WARITUKE_BUHIN_HI  , ")
                .AppendLine(" KAKO_3_WARITUKE_KATA_HI   , ")
                .AppendLine(" KAKO_3_WARITUKE_KOUHOU    , ")
                .AppendLine(" KAKO_3_MAKER_BUHIN_HI     , ")
                .AppendLine(" KAKO_3_MAKER_KATA_HI      , ")
                .AppendLine(" KAKO_3_MAKER_KOUHOU       , ")
                .AppendLine(" KAKO_3_SHINGI_BUHIN_HI    , ")
                .AppendLine(" KAKO_3_SHINGI_KATA_HI     , ")
                .AppendLine(" KAKO_3_SHINGI_KOUHOU      , ")
                .AppendLine(" KAKO_3_KOUNYU_KIBOU_TANKA , ")
                .AppendLine(" KAKO_3_KOUNYU_TANKA       , ")
                .AppendLine(" KAKO_3_SHIKYU_HIN         , ")
                .AppendLine(" KAKO_3_KOUJI_SHIREI_NO    , ")
                .AppendLine(" KAKO_3_EVENT_NAME         , ")
                .AppendLine(" KAKO_3_HACHU_BI           , ")
                .AppendLine(" KAKO_3_KENSHU_BI          , ")
                .AppendLine(" KAKO_4_RYOSAN_TANKA       , ")
                .AppendLine(" KAKO_4_WARITUKE_BUHIN_HI  , ")
                .AppendLine(" KAKO_4_WARITUKE_KATA_HI   , ")
                .AppendLine(" KAKO_4_WARITUKE_KOUHOU    , ")
                .AppendLine(" KAKO_4_MAKER_BUHIN_HI     , ")
                .AppendLine(" KAKO_4_MAKER_KATA_HI      , ")
                .AppendLine(" KAKO_4_MAKER_KOUHOU       , ")
                .AppendLine(" KAKO_4_SHINGI_BUHIN_HI    , ")
                .AppendLine(" KAKO_4_SHINGI_KATA_HI     , ")
                .AppendLine(" KAKO_4_SHINGI_KOUHOU      , ")
                .AppendLine(" KAKO_4_KOUNYU_KIBOU_TANKA , ")
                .AppendLine(" KAKO_4_KOUNYU_TANKA       , ")
                .AppendLine(" KAKO_4_SHIKYU_HIN         , ")
                .AppendLine(" KAKO_4_KOUJI_SHIREI_NO    , ")
                .AppendLine(" KAKO_4_EVENT_NAME         , ")
                .AppendLine(" KAKO_4_HACHU_BI           , ")
                .AppendLine(" KAKO_4_KENSHU_BI          , ")
                .AppendLine(" KAKO_5_RYOSAN_TANKA       , ")
                .AppendLine(" KAKO_5_WARITUKE_BUHIN_HI  , ")
                .AppendLine(" KAKO_5_WARITUKE_KATA_HI   , ")
                .AppendLine(" KAKO_5_WARITUKE_KOUHOU    , ")
                .AppendLine(" KAKO_5_MAKER_BUHIN_HI     , ")
                .AppendLine(" KAKO_5_MAKER_KATA_HI      , ")
                .AppendLine(" KAKO_5_MAKER_KOUHOU       , ")
                .AppendLine(" KAKO_5_SHINGI_BUHIN_HI    , ")
                .AppendLine(" KAKO_5_SHINGI_KATA_HI     , ")
                .AppendLine(" KAKO_5_SHINGI_KOUHOU      , ")
                .AppendLine(" KAKO_5_KOUNYU_KIBOU_TANKA , ")
                .AppendLine(" KAKO_5_KOUNYU_TANKA       , ")
                .AppendLine(" KAKO_5_SHIKYU_HIN         , ")
                .AppendLine(" KAKO_5_KOUJI_SHIREI_NO    , ")
                .AppendLine(" KAKO_5_EVENT_NAME         , ")
                .AppendLine(" KAKO_5_HACHU_BI           , ")
                .AppendLine(" KAKO_5_KENSHU_BI          , ")
                .AppendLine(" AUD_FLAG, ")
                .AppendLine(" AUD_BI, ")
                .AppendLine(" HENKATEN, ")
                .AppendLine(" CREATED_USER_ID, ")
                .AppendLine(" CREATED_DATE, ")
                .AppendLine(" CREATED_TIME, ")
                .AppendLine(" UPDATED_USER_ID, ")
                .AppendLine(" UPDATED_DATE, ")
                .AppendLine(" UPDATED_TIME ")
                .AppendLine(" ) ")
                .AppendLine(" VALUES ( ")
                .AppendLine(" @ShisakuEventCode, ")
                .AppendLine(" @YosanListCode, ")
                .AppendLine(" @YosanBukaCode, ")
                .AppendLine(" @YosanBlockNo, ")
                .AppendLine(" @BuhinNoHyoujiJun, ")
                .AppendLine(" @YosanSortJun, ")
                .AppendLine(" @YosanGyouId, ")
                .AppendLine(" @YosanLevel, ")
                .AppendLine(" @YosanShukeiCode, ")
                .AppendLine(" @YosanSiaShukeiCode, ")
                .AppendLine(" @YosanBuhinNo, ")
                .AppendLine(" @YosanBuhinName, ")
                .AppendLine(" @YosanInsu, ")
                .AppendLine(" @YosanMakerCode, ")
                .AppendLine(" @YosanKyoukuSection, ")
                .AppendLine(" @YosanKoutan, ")
                .AppendLine(" @YosanTsukurikataSeisaku, ")
                .AppendLine(" @YosanTsukurikataKatashiyou1, ")
                .AppendLine(" @YosanTsukurikataKatashiyou2, ")
                .AppendLine(" @YosanTsukurikataKatashiyou3, ")
                .AppendLine(" @YosanTsukurikataTigu, ")
                .AppendLine(" @YosanTsukurikataKibo, ")
                .AppendLine(" @YosanShisakuBuhinHi, ")
                .AppendLine(" @YosanShisakuKataHi, ")
                .AppendLine(" @YosanBuhinNote, ")
                .AppendLine(" @YosanBikou, ")
                .AppendLine(" @YosanKonkyoKokugaiKbn, ")
                .AppendLine(" @YosanKonkyoMixBuhinHi, ")
                .AppendLine(" @YosanKonkyoInyouMixBuhinHi, ")
                .AppendLine(" @YosanKonkyoKeisu1, ")
                .AppendLine(" @YosanKonkyoKouhou, ")
                .AppendLine(" @YosanWaritukeBuhinHi, ")
                .AppendLine(" @YosanWaritukeKeisu2, ")
                .AppendLine(" @YosanWaritukeBuhinHiTotal, ")
                .AppendLine(" @YosanWaritukeKataHi, ")
                .AppendLine(" @YosanKounyuKibouTanka, ")
                .AppendLine(" @YosanKounyuKibouBuhinHi, ")
                .AppendLine(" @YosanKounyuKibouBuhinHiTotal, ")
                .AppendLine(" @YosanKounyuKibouKataHi, ")
                .AppendLine(" @Kako1RyosanTanka, ")
                .AppendLine(" @Kako1WaritukeBuhinHi, ")
                .AppendLine(" @Kako1WaritukeKataHi, ")
                .AppendLine(" @Kako1WaritukeKouhou, ")
                .AppendLine(" @Kako1MakerBuhinHi, ")
                .AppendLine(" @Kako1MakerKataHi, ")
                .AppendLine(" @Kako1MakerKouhou, ")
                .AppendLine(" @Kako1ShingiBuhinHi, ")
                .AppendLine(" @Kako1ShingiKataHi, ")
                .AppendLine(" @Kako1ShingiKouhou, ")
                .AppendLine(" @Kako1KounyuKibouTanka, ")
                .AppendLine(" @Kako1KounyuTanka, ")
                .AppendLine(" @Kako1ShikyuHin, ")
                .AppendLine(" @Kako1KoujiShireiNo, ")
                .AppendLine(" @Kako1EventName, ")
                .AppendLine(" @Kako1HachuBi, ")
                .AppendLine(" @Kako1KenshuBi, ")
                .AppendLine(" @Kako2RyosanTanka, ")
                .AppendLine(" @Kako2WaritukeBuhinHi, ")
                .AppendLine(" @Kako2WaritukeKataHi, ")
                .AppendLine(" @Kako2WaritukeKouhou, ")
                .AppendLine(" @Kako2MakerBuhinHi, ")
                .AppendLine(" @Kako2MakerKataHi, ")
                .AppendLine(" @Kako2MakerKouhou, ")
                .AppendLine(" @Kako2ShingiBuhinHi, ")
                .AppendLine(" @Kako2ShingiKataHi, ")
                .AppendLine(" @Kako2ShingiKouhou, ")
                .AppendLine(" @Kako2KounyuKibouTanka, ")
                .AppendLine(" @Kako2KounyuTanka, ")
                .AppendLine(" @Kako2ShikyuHin, ")
                .AppendLine(" @Kako2KoujiShireiNo, ")
                .AppendLine(" @Kako2EventName, ")
                .AppendLine(" @Kako2HachuBi, ")
                .AppendLine(" @Kako2KenshuBi, ")
                .AppendLine(" @Kako3RyosanTanka, ")
                .AppendLine(" @Kako3WaritukeBuhinHi, ")
                .AppendLine(" @Kako3WaritukeKataHi, ")
                .AppendLine(" @Kako3WaritukeKouhou, ")
                .AppendLine(" @Kako3MakerBuhinHi, ")
                .AppendLine(" @Kako3MakerKataHi, ")
                .AppendLine(" @Kako3MakerKouhou, ")
                .AppendLine(" @Kako3ShingiBuhinHi, ")
                .AppendLine(" @Kako3ShingiKataHi, ")
                .AppendLine(" @Kako3ShingiKouhou, ")
                .AppendLine(" @Kako3KounyuKibouTanka, ")
                .AppendLine(" @Kako3KounyuTanka, ")
                .AppendLine(" @Kako3ShikyuHin, ")
                .AppendLine(" @Kako3KoujiShireiNo, ")
                .AppendLine(" @Kako3EventName, ")
                .AppendLine(" @Kako3HachuBi, ")
                .AppendLine(" @Kako3KenshuBi, ")
                .AppendLine(" @Kako4RyosanTanka, ")
                .AppendLine(" @Kako4WaritukeBuhinHi, ")
                .AppendLine(" @Kako4WaritukeKataHi, ")
                .AppendLine(" @Kako4WaritukeKouhou, ")
                .AppendLine(" @Kako4MakerBuhinHi, ")
                .AppendLine(" @Kako4MakerKataHi, ")
                .AppendLine(" @Kako4MakerKouhou, ")
                .AppendLine(" @Kako4ShingiBuhinHi, ")
                .AppendLine(" @Kako4ShingiKataHi, ")
                .AppendLine(" @Kako4ShingiKouhou, ")
                .AppendLine(" @Kako4KounyuKibouTanka, ")
                .AppendLine(" @Kako4KounyuTanka, ")
                .AppendLine(" @Kako4ShikyuHin, ")
                .AppendLine(" @Kako4KoujiShireiNo, ")
                .AppendLine(" @Kako4EventName, ")
                .AppendLine(" @Kako4HachuBi, ")
                .AppendLine(" @Kako4KenshuBi, ")
                .AppendLine(" @Kako5RyosanTanka, ")
                .AppendLine(" @Kako5WaritukeBuhinHi, ")
                .AppendLine(" @Kako5WaritukeKataHi, ")
                .AppendLine(" @Kako5WaritukeKouhou, ")
                .AppendLine(" @Kako5MakerBuhinHi, ")
                .AppendLine(" @Kako5MakerKataHi, ")
                .AppendLine(" @Kako5MakerKouhou, ")
                .AppendLine(" @Kako5ShingiBuhinHi, ")
                .AppendLine(" @Kako5ShingiKataHi, ")
                .AppendLine(" @Kako5ShingiKouhou, ")
                .AppendLine(" @Kako5KounyuKibouTanka, ")
                .AppendLine(" @Kako5KounyuTanka, ")
                .AppendLine(" @Kako5ShikyuHin, ")
                .AppendLine(" @Kako5KoujiShireiNo, ")
                .AppendLine(" @Kako5EventName, ")
                .AppendLine(" @Kako5HachuBi, ")
                .AppendLine(" @Kako5KenshuBi, ")
                .AppendLine(" @AudFlag, ")
                .AppendLine(" @AudBi, ")
                .AppendLine(" @Henkaten, ")
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

#Region "SQL-UPDATE 予算リストコード (予算工事No)"
        ''' <summary>
        ''' SQL-UPDATE 予算リストコード (予算工事No)"
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlUpdShisakuList_koujiNo() As String
            Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder

            With sql
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_LISTCODE  ")
                .AppendLine("	SET  YOSAN_KOUJI_NO = @YOSAN_KOUJI_NO  ")
                .AppendLine(" ,STATUS = @STATUS ")
                .AppendLine("		,UPDATED_USER_ID  = @UPDATED_USER_ID ")
                .AppendLine("		,UPDATED_DATE	  = @UPDATED_DATE  ")
                .AppendLine("		,UPDATED_TIME	  = @UPDATED_TIME ")
                .AppendLine(" WHERE SHISAKU_EVENT_CODE = @SHISAKU_EVENT_CODE  ")
                .AppendLine("   AND YOSAN_LIST_CODE = @YOSAN_LIST_CODE  ")
            End With

            Return sql.ToString

        End Function

#End Region

#Region "SQL-UPDATE 予算設定部品表"
        ''' <summary>
        ''' SQL-UPDATE 予算設定部品表
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlUpdTehaichoBase() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" UPDATE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN ")
                .AppendLine(" SET ")
                .AppendLine("    YOSAN_SORT_JUN =   @YosanSortJun ")
                .AppendLine(",   YOSAN_GYOU_ID =   @YosanGyouId ")
                .AppendLine(",   YOSAN_LEVEL =   @YosanLevel ")
                .AppendLine(",   YOSAN_SHUKEI_CODE =   @YosanShukeiCode ")
                .AppendLine(",   YOSAN_SIA_SHUKEI_CODE =   @YosanSiaShukeiCode ")
                .AppendLine(",   YOSAN_BUHIN_NO =   @YosanBuhinNo ")
                .AppendLine(",   YOSAN_BUHIN_NAME =   @YosanBuhinName ")
                .AppendLine(",   YOSAN_INSU =   @YosanInsu ")
                .AppendLine(",   YOSAN_MAKER_CODE =   @YosanMakerCode ")
                .AppendLine(",   YOSAN_KYOUKU_SECTION =   @YosanKyoukuSection ")
                .AppendLine(",   YOSAN_KOUTAN =   @YosanKoutan ")
                .AppendLine(",   YOSAN_TSUKURIKATA_SEISAKU =   @YosanTsukurikataSeisaku ")
                .AppendLine(",   YOSAN_TSUKURIKATA_KATASHIYOU_1 =   @YosanTsukurikataKatashiyou1 ")
                .AppendLine(",   YOSAN_TSUKURIKATA_KATASHIYOU_2 =   @YosanTsukurikataKatashiyou2 ")
                .AppendLine(",   YOSAN_TSUKURIKATA_KATASHIYOU_3 =   @YosanTsukurikataKatashiyou3 ")
                .AppendLine(",   YOSAN_TSUKURIKATA_TIGU =   @YosanTsukurikataTigu ")
                .AppendLine(",   YOSAN_TSUKURIKATA_KIBO =   @YosanTsukurikataKibo ")
                .AppendLine(",   YOSAN_SHISAKU_BUHIN_HI =   @YosanShisakuBuhinHi ")
                .AppendLine(",   YOSAN_SHISAKU_KATA_HI = @YosanShisakuKataHi ")
                .AppendLine(",   YOSAN_BUHIN_NOTE = @YosanBuhinNote ")
                .AppendLine(",   YOSAN_BIKOU = @YosanBikou ")
                .AppendLine(",   YOSAN_KONKYO_KOKUGAI_KBN =   @YosanKonkyoKokugaiKbn ")
                .AppendLine(",   YOSAN_KONKYO_MIX_BUHIN_HI =   @YosanKonkyoMixBuhinHi ")
                .AppendLine(",   YOSAN_KONKYO_INYOU_MIX_BUHIN_HI =   @YosanKonkyoInyouMixBuhinHi ")
                .AppendLine(",   YOSAN_KONKYO_KEISU_1 =   @YosanKonkyoKeisu1 ")
                .AppendLine(",   YOSAN_KONKYO_KOUHOU =   @YosanKonkyoKouhou ")
                .AppendLine(",   YOSAN_WARITUKE_BUHIN_HI =   @YosanWaritukeBuhinHi ")
                .AppendLine(",   YOSAN_WARITUKE_KEISU_2 =   @YosanWaritukeKeisu2 ")
                .AppendLine(",   YOSAN_WARITUKE_BUHIN_HI_TOTAL =   @YosanWaritukeBuhinHiTotal ")
                .AppendLine(",   YOSAN_WARITUKE_KATA_HI =   @YosanWaritukeKataHi ")
                .AppendLine(",   YOSAN_KOUNYU_KIBOU_TANKA =   @YosanKounyuKibouTanka ")
                .AppendLine(",   YOSAN_KOUNYU_KIBOU_BUHIN_HI =   @YosanKounyuKibouBuhinHi ")
                .AppendLine(",   YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL =   @YosanKounyuKibouBuhinHiTotal ")
                .AppendLine(",   YOSAN_KOUNYU_KIBOU_KATA_HI =   @YosanKounyuKibouKataHi ")
                .AppendLine(",   AUD_FLAG =   @AudFlag ")
                .AppendLine(",   HENKATEN = @Henkaten ")
                .AppendLine(",   UPDATED_USER_ID =   @UpdatedUserId ")
                .AppendLine(",   UPDATED_DATE =   @UpdatedDate ")
                .AppendLine(",   UPDATED_TIME  =   @UpdatedTime ")
                .AppendLine(",   KAKO_1_RYOSAN_TANKA       = @Kako1RyosanTanka ")
                .AppendLine(",   KAKO_1_WARITUKE_BUHIN_HI  = @Kako1WaritukeBuhinHi ")
                .AppendLine(",   KAKO_1_WARITUKE_KATA_HI   = @Kako1WaritukekataHi ")
                .AppendLine(",   KAKO_1_WARITUKE_KOUHOU    = @Kako1WaritukeKouhou ")
                .AppendLine(",   KAKO_1_MAKER_BUHIN_HI     = @Kako1MakerBuhinHi ")
                .AppendLine(",   KAKO_1_MAKER_KATA_HI      = @Kako1MakerKataHi ")
                .AppendLine(",   KAKO_1_MAKER_KOUHOU       = @Kako1MakerKouhou ")
                .AppendLine(",   KAKO_1_SHINGI_BUHIN_HI    = @Kako1ShingiBuhinHi ")
                .AppendLine(",   KAKO_1_SHINGI_KATA_HI     = @Kako1ShingiKataHi ")
                .AppendLine(",   KAKO_1_SHINGI_KOUHOU      = @Kako1ShingiKouhou ")
                .AppendLine(",   KAKO_1_KOUNYU_KIBOU_TANKA = @Kako1KounyuKibouTanka ")
                .AppendLine(",   KAKO_1_KOUNYU_TANKA       = @Kako1KounyuTanka ")
                .AppendLine(",   KAKO_1_SHIKYU_HIN         = @Kako1ShikyuHin ")
                .AppendLine(",   KAKO_1_KOUJI_SHIREI_NO    = @Kako1KoujiShireiNo ")
                .AppendLine(",   KAKO_1_EVENT_NAME         = @Kako1EventName ")
                .AppendLine(",   KAKO_1_HACHU_BI           = @Kako1HachuBi ")
                .AppendLine(",   KAKO_1_KENSHU_BI          = @Kako1KenshuBi ")
                .AppendLine(",   KAKO_2_RYOSAN_TANKA       = @Kako2RyosanTanka ")
                .AppendLine(",   KAKO_2_WARITUKE_BUHIN_HI  = @Kako2WaritukeBuhinHi ")
                .AppendLine(",   KAKO_2_WARITUKE_KATA_HI   = @Kako2WaritukeKataHi ")
                .AppendLine(",   KAKO_2_WARITUKE_KOUHOU    = @Kako2WaritukeKouhou ")
                .AppendLine(",   KAKO_2_MAKER_BUHIN_HI     = @Kako2MakerBuhinHi ")
                .AppendLine(",   KAKO_2_MAKER_KATA_HI      = @Kako2MakerKataHi ")
                .AppendLine(",   KAKO_2_MAKER_KOUHOU       = @Kako2MakerKouhou ")
                .AppendLine(",   KAKO_2_SHINGI_BUHIN_HI    = @Kako2ShingiBuhinHi ")
                .AppendLine(",   KAKO_2_SHINGI_KATA_HI     = @Kako2ShingiKataHi ")
                .AppendLine(",   KAKO_2_SHINGI_KOUHOU      = @Kako2ShingiKouhou ")
                .AppendLine(",   KAKO_2_KOUNYU_KIBOU_TANKA = @Kako2KounyuKibouTanka ")
                .AppendLine(",   KAKO_2_KOUNYU_TANKA       = @Kako2KounyuTanka ")
                .AppendLine(",   KAKO_2_SHIKYU_HIN         = @Kako2ShikyuHin ")
                .AppendLine(",   KAKO_2_KOUJI_SHIREI_NO    = @Kako2KoujiShireiNo ")
                .AppendLine(",   KAKO_2_EVENT_NAME         = @Kako2EventName ")
                .AppendLine(",   KAKO_2_HACHU_BI           = @Kako2HachuBi ")
                .AppendLine(",   KAKO_2_KENSHU_BI          = @Kako2KenshuBi ")
                .AppendLine(",   KAKO_3_RYOSAN_TANKA       = @Kako3RyosanTanka ")
                .AppendLine(",   KAKO_3_WARITUKE_BUHIN_HI  = @Kako3WaritukeBuhinHi ")
                .AppendLine(",   KAKO_3_WARITUKE_KATA_HI   = @Kako3WaritukeKataHi ")
                .AppendLine(",   KAKO_3_WARITUKE_KOUHOU    = @Kako3WaritukeKouhou ")
                .AppendLine(",   KAKO_3_MAKER_BUHIN_HI     = @Kako3MakerBuhinHi ")
                .AppendLine(",   KAKO_3_MAKER_KATA_HI      = @Kako3MakerKataHi ")
                .AppendLine(",   KAKO_3_MAKER_KOUHOU       = @Kako3MakerKouhou ")
                .AppendLine(",   KAKO_3_SHINGI_BUHIN_HI    = @Kako3ShingiBuhinHi ")
                .AppendLine(",   KAKO_3_SHINGI_KATA_HI     = @Kako3ShingiKataHi ")
                .AppendLine(",   KAKO_3_SHINGI_KOUHOU      = @Kako3ShingiKouhou ")
                .AppendLine(",   KAKO_3_KOUNYU_KIBOU_TANKA = @Kako3KounyuKibouTanka ")
                .AppendLine(",   KAKO_3_KOUNYU_TANKA       = @Kako3KounyuTanka ")
                .AppendLine(",   KAKO_3_SHIKYU_HIN         = @Kako3ShikyuHin ")
                .AppendLine(",   KAKO_3_KOUJI_SHIREI_NO    = @Kako3KoujiShireiNo ")
                .AppendLine(",   KAKO_3_EVENT_NAME         = @Kako3EventName ")
                .AppendLine(",   KAKO_3_HACHU_BI           = @Kako3HachuBi ")
                .AppendLine(",   KAKO_3_KENSHU_BI          = @Kako3KenshuBi ")
                .AppendLine(",   KAKO_4_RYOSAN_TANKA       = @Kako4RyosanTanka ")
                .AppendLine(",   KAKO_4_WARITUKE_BUHIN_HI  = @Kako4WaritukeBuhinHi ")
                .AppendLine(",   KAKO_4_WARITUKE_KATA_HI   = @Kako4WaritukeKataHi ")
                .AppendLine(",   KAKO_4_WARITUKE_KOUHOU    = @Kako4WaritukeKouhou ")
                .AppendLine(",   KAKO_4_MAKER_BUHIN_HI     = @Kako4MakerBuhinHi ")
                .AppendLine(",   KAKO_4_MAKER_KATA_HI      = @Kako4MakerKataHi ")
                .AppendLine(",   KAKO_4_MAKER_KOUHOU       = @Kako4MakerKouhou ")
                .AppendLine(",   KAKO_4_SHINGI_BUHIN_HI    = @Kako4ShingiBuhinHi ")
                .AppendLine(",   KAKO_4_SHINGI_KATA_HI     = @Kako4ShingiKataHi ")
                .AppendLine(",   KAKO_4_SHINGI_KOUHOU      = @Kako4ShingiKouhou ")
                .AppendLine(",   KAKO_4_KOUNYU_KIBOU_TANKA = @Kako4KounyuKibouTanka ")
                .AppendLine(",   KAKO_4_KOUNYU_TANKA       = @Kako4KounyuTanka ")
                .AppendLine(",   KAKO_4_SHIKYU_HIN         = @Kako4ShikyuHin ")
                .AppendLine(",   KAKO_4_KOUJI_SHIREI_NO    = @Kako4KoujiShireiNo ")
                .AppendLine(",   KAKO_4_EVENT_NAME         = @Kako4EventName ")
                .AppendLine(",   KAKO_4_HACHU_BI           = @Kako4HachuBi ")
                .AppendLine(",   KAKO_4_KENSHU_BI          = @Kako4KenshuBi ")
                .AppendLine(",   KAKO_5_RYOSAN_TANKA       = @Kako5RyosanTanka ")
                .AppendLine(",   KAKO_5_WARITUKE_BUHIN_HI  = @Kako5WaritukeBuhinHi ")
                .AppendLine(",   KAKO_5_WARITUKE_KATA_HI   = @Kako5WaritukeKataHi ")
                .AppendLine(",   KAKO_5_WARITUKE_KOUHOU    = @Kako5WaritukeKouhou ")
                .AppendLine(",   KAKO_5_MAKER_BUHIN_HI     = @Kako5MakerBuhinHi ")
                .AppendLine(",   KAKO_5_MAKER_KATA_HI      = @Kako5MakerKataHi ")
                .AppendLine(",   KAKO_5_MAKER_KOUHOU       = @Kako5MakerKouhou ")
                .AppendLine(",   KAKO_5_SHINGI_BUHIN_HI    = @Kako5ShingiBuhinHi ")
                .AppendLine(",   KAKO_5_SHINGI_KATA_HI     = @Kako5ShingiKataHi ")
                .AppendLine(",   KAKO_5_SHINGI_KOUHOU      = @Kako5ShingiKouhou ")
                .AppendLine(",   KAKO_5_KOUNYU_KIBOU_TANKA = @Kako5KounyuKibouTanka ")
                .AppendLine(",   KAKO_5_KOUNYU_TANKA       = @Kako5KounyuTanka ")
                .AppendLine(",   KAKO_5_SHIKYU_HIN         = @Kako5ShikyuHin ")
                .AppendLine(",   KAKO_5_KOUJI_SHIREI_NO    = @Kako5KoujiShireiNo ")
                .AppendLine(",   KAKO_5_EVENT_NAME         = @Kako5EventName ")
                .AppendLine(",   KAKO_5_HACHU_BI           = @Kako5HachuBi ")
                .AppendLine(",   KAKO_5_KENSHU_BI          = @Kako5KenshuBi ")
                .AppendLine(" WHERE ")
                .AppendLine("         SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND YOSAN_LIST_CODE = @YosanListCode  ")
                .AppendLine(" AND YOSAN_BLOCK_NO = @YosanBlockNo ")
                .AppendLine(" AND YOSAN_BUKA_CODE = @YosanBukaCode ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")
            End With

            Return sql.ToString
        End Function

#End Region

#Region "SQL-DELETE 予算設定部品表(主キー削除)"
        ''' <summary>
        ''' SQL-DELETE 予算設定部品表
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlDelTehaiBaseKey() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine("  DELETE FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN")
                .AppendLine(" WHERE ")
                .AppendLine("         SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND YOSAN_LIST_CODE = @YosanListCode  ")
                .AppendLine(" AND YOSAN_BLOCK_NO = @YosanBlockNo ")
                .AppendLine(" AND YOSAN_BUKA_CODE = @YosanBukaCode ")
                .AppendLine(" AND BUHIN_NO_HYOUJI_JUN = @BuhinNoHyoujiJun ")

            End With

            Return sql.ToString
        End Function

#End Region

#Region "SQL-DELETE 予算設定部品表(ブロックNo単位削除)"
        ''' <summary>
        ''' SQL-DELETE 予算設定部品表(ブロックNo単位削除)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSqlDelTehaiBaseBlockNo() As String
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine("  DELETE FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN ")
                .AppendLine(" WHERE ")
                .AppendLine("     SHISAKU_EVENT_CODE = @ShisakuEventCode ")
                .AppendLine(" AND YOSAN_LIST_CODE = @YosanListCode  ")
                .AppendLine(" AND YOSAN_BUKA_CODE = @YosanBukaCode ")
                .AppendLine(" AND YOSAN_BLOCK_NO = @YosanBlockNo  ")

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

            End With

            Return sql.ToString

        End Function

#End Region

#End Region

    End Class

End Namespace