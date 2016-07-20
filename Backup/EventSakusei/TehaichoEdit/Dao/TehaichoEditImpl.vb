Imports System.Data
Imports System.Text
Imports EBom.Common
Imports EBom.Data
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.Sql
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports EventSakusei.TehaichoEdit
Imports EventSakusei.TehaichoEdit.Logic

Namespace TehaichoEdit.Dao
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TehaichoEditImpl

#Region "SELECT 集計コード一覧を返す"
        ''' <summary>
        ''' 集計コード一覧を返す
        ''' 
        ''' ※NmSyukeiにより列参照
        ''' 
        ''' </summary>
        ''' <returns>該当する一覧</returns>
        ''' <remarks></remarks>
        Public Shared Function FindAllSyukeiCodeInfo() As DataTable

            Dim dtResult As DataTable = New DataTable
            Dim sql As String = "SELECT SYUKEI_CODE,SYUKEI_NAME FROM " & MBOM_DB_NAME & ".dbo.M_SYUKEI_CODE WITH (NOLOCK, NOWAIT) "

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql, dtResult)
            End Using

            Return dtResult

        End Function

#End Region

#Region "SELECT 試作開発符号一覧を返す"
        ''' <summary>
        ''' 試作開発符号一覧を返す
        ''' 
        ''' ※NmSyukeiにより列参照
        ''' 
        ''' </summary>
        ''' <returns>該当する一覧</returns>
        ''' <remarks></remarks>
        Public Shared Function FindAllShisakuKaihatsuFugo(ByVal ShisakuEventCode As String) As String

            Dim dtResult As DataTable = New DataTable
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT SHISAKU_KAIHATSU_FUGO ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT WITH (NOLOCK, NOWAIT) ")
                .AppendFormat(" WHERE SHISAKU_EVENT_CODE = '{0}'", ShisakuEventCode)
            End With

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Dim ShisakuKaihatsuFugo As String = String.Empty

            If dtResult.Rows.Count >= 1 Then
                If IsDBNull(dtResult.Rows(0)("SHISAKU_KAIHATSU_FUGO")) = False Then
                    ShisakuKaihatsuFugo = dtResult.Rows(0)("SHISAKU_KAIHATSU_FUGO").ToString.Trim
                End If
            End If

            Return ShisakuKaihatsuFugo

        End Function

#End Region


#Region "SELECT 手配記号一覧を返す"
        ''' <summary>
        ''' SELECT 手配記号一覧を返す
        ''' 
        ''' ※ NmTDColTehaiKigouにより列参照
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindAllTehaiKigou() As DataTable

            Dim dtResult As DataTable = New DataTable
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT TIKG ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.AS_ARPF04 WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE BUKBN = 'B' ")
                .AppendLine(" ORDER BY TIKG ")
            End With

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Return dtResult

        End Function
#End Region


#Region "SELECT 作り方製作方法一覧を返す"
        ''' <summary>
        ''' SELECT 作り方製作方法一覧を返す
        ''' 
        ''' ※ NmTDColTehaiKigouにより列参照
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindAllTsukurikataSeisaku() As DataTable

            Dim dtResult As DataTable = New DataTable
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT TSUKURIKATA_NO,TSUKURIKATA_NAME ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_TSUKURIKATA WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE TSUKURIKATA_KBN = '1' ")
                .AppendLine(" ORDER BY TSUKURIKATA_NO ")
            End With

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Return dtResult

        End Function
#End Region
#Region "SELECT 作り方型仕様一覧を返す"
        ''' <summary>
        ''' SELECT 作り方型仕様一覧を返す
        ''' 
        ''' ※ NmTDColTehaiKigouにより列参照
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindAllTsukurikataKatashiyou() As DataTable

            Dim dtResult As DataTable = New DataTable
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT CAST(TSUKURIKATA_NO as varchar) + '.' + TSUKURIKATA_NAME AS TSUKURIKATA_NAME")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_TSUKURIKATA WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE TSUKURIKATA_KBN = '3' ")
                .AppendLine(" ORDER BY TSUKURIKATA_NO ")
            End With

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Return dtResult

        End Function
#End Region
#Region "SELECT 作り方治具一覧を返す"
        ''' <summary>
        ''' SELECT 作り方治具一覧を返す
        ''' 
        ''' ※ NmTDColTehaiKigouにより列参照
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindAllTsukurikataTigu() As DataTable

            Dim dtResult As DataTable = New DataTable
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT TSUKURIKATA_NO,TSUKURIKATA_NAME ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_TSUKURIKATA WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE TSUKURIKATA_KBN = '4' ")
                .AppendLine(" ORDER BY TSUKURIKATA_NO ")
            End With

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Return dtResult

        End Function
#End Region


#Region "SELECT ブロックNo一覧取得(RHAC0080)"
        ''' <summary>
        ''' SELECT ブロックNo一覧取得(RHAC0080)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindAllListBlockNo() As DataTable

            Dim dtResult As DataTable = New DataTable
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

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Return dtResult

        End Function
#End Region

#Region "SELECT 部課コード取得(RHAC0080→RHAC1560)"
        ''' <summary>
        ''' SELECT 部課コード取得(RHAC0080→RHAC1560)
        ''' </summary>
        ''' <param name="aBlockNo">ブロックNo</param>
        ''' <returns>部課コード:存在しない場合はZZZZZを返す</returns>
        ''' <remarks></remarks>
        Public Shared Function FindBukaCode(ByVal aBlockNo As String) As String
            Dim dtResult As DataTable = New DataTable
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine("    WITH BLOCK_TBL AS ")
                .AppendLine("    ( ")
                .AppendLine("    SELECT  ")
                .AppendLine("    TANTO_BUSHO, ")
                .AppendLine("    BLOCK_NO_KINO ")
                .AppendLine("    FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0080 WITH (NOLOCK, NOWAIT) ")
                .AppendLine("        WHERE  ")
                .AppendFormat("                      BLOCK_NO_KINO = '{0}' ", aBlockNo)
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

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Dim bukaCode As String = String.Empty

            If dtResult.Rows.Count >= 1 Then
                bukaCode = dtResult.Rows(0)("BUKA_CODE").ToString.Trim
            End If

            If bukaCode.Trim.Equals(String.Empty) Then
                bukaCode = "ZZZZ"
            End If

            Return bukaCode

        End Function
#End Region

#Region "SELECT 試作イベント 主キー検索"
        ''' <summary>
        ''' SELECT 試作イベント 主キー検索
        ''' 
        ''' ※ NmTdColShisakuEventにより列名参照
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindPkShisakuEvent(ByVal aShisakuEventCode As String) As DataTable

            Dim dtResult As DataTable = New DataTable
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT SHISAKU_EVENT_CODE,UNIT_KBN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT WITH (NOLOCK, NOWAIT) ")
                .AppendFormat(" WHERE SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
                .AppendLine(" ORDER BY UPDATED_DATE DESC ,UPDATED_TIME DESC ")
            End With

            '試作イベント
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Return dtResult

        End Function
#End Region

#Region "SELECT 取引先マスタ(RHAC0610)主キー検索"
        ''' <summary>
        ''' SELECT 取引先マスタ(RHAC0610)主キー検索
        ''' 
        ''' </summary>
        ''' <param name="aMakerCode"></param>
        ''' <returns>取引先名称</returns>
        ''' <remarks></remarks>
        Public Shared Function FindPkRhac0610(ByVal aMakerCode As String) As String
            Dim dtResult As DataTable = New DataTable
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT MAKER_CODE  ")
                .AppendLine("    ,MAKER_NAME ")
                .AppendLine(" FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC0610 WITH (NOLOCK, NOWAIT) ")
                .AppendFormat(" WHERE MAKER_CODE = '{0}' ", aMakerCode)
            End With

            '名称一覧取得
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Dim makerName As String = String.Empty

            If dtResult.Rows.Count >= 1 Then
                If IsDBNull(dtResult.Rows(0)("MAKER_NAME")) = False Then
                    makerName = dtResult.Rows(0)("MAKER_NAME").ToString.Trim
                End If
            End If

            Return makerName

        End Function
#End Region

#Region "SELECT 試作部品編集情報"
        ''' <summary>
        ''' SELECT 試作部品編集情報
        ''' </summary>
        ''' <param name="aBuhinNo"></param>
        ''' <param name="aBuhinNoKbn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindBuhiNoShisakuBuhinEditBase(ByVal aBuhinNo As String, ByVal aBuhinNoKbn As String) As DataTable
            Dim dtResult As DataTable = New DataTable
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
                .AppendFormat("         BUHIN_NO = '{0}'  ", aBuhinNo)
                .AppendFormat(" AND BUHIN_NO_KBN = '{0}' ", aBuhinNoKbn)
                .AppendLine(" ORDER BY ")
                .AppendLine("        UPDATED_DATE DESC , ")
                .AppendLine("        UPDATED_TIME DSC ")

            End With

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Return dtResult

        End Function
#End Region

#Region "SELECT 試作手配帳(基本情報)一覧を返す"
        ''' <summary>
        ''' 試作手配帳(基本情報)一覧を返す
        ''' 
        ''' ※NmTDColBaseにより列名参照
        ''' 
        ''' </summary>
        ''' <returns>該当する一覧</returns>
        ''' <remarks></remarks>
        Public Shared Function FindAllBaseInfo(ByVal aShisakuEventCode As String, ByVal aShisakuListCode As String) As DataTable

            Dim dtResult As DataTable = New DataTable
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
                .AppendLine("ZAIRYO_SUNPO_X, ")
                .AppendLine("ZAIRYO_SUNPO_Y, ")
                .AppendLine("ZAIRYO_SUNPO_Z, ")
                .AppendLine("ZAIRYO_SUNPO_XY, ")
                .AppendLine("ZAIRYO_SUNPO_XZ, ")
                .AppendLine("ZAIRYO_SUNPO_YZ, ")
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
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", aShisakuListCode)
                .AppendLine("    AND SHISAKU_LIST_CODE_KAITEI_NO =   ")
                .AppendLine("(  ")
                .AppendLine("    SELECT MAX(SHISAKU_LIST_CODE_KAITEI_NO) AS SHISAKU_LIST_CODE_KAITEI_NO   ")
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

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Return dtResult

        End Function

#End Region

#Region "SELECT 試作手配帳(基本情報)最大行ID・部品No表示順取得"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="aShisakuEventCode"></param>
        ''' <param name="aShisakuListCode"></param>
        ''' <param name="aShisakuBukaCode"></param>
        ''' <param name="aShisakuBlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindMaxIDBaseInfo(ByVal aShisakuEventCode As String, _
                                                                     ByVal aShisakuListCode As String, _
                                                                     ByVal aShisakuBukaCode As String, _
                                                                     ByVal aShisakuBlockNo As String _
                                                                     ) As DataTable

            Dim dtResult As DataTable = New DataTable
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
                .AppendFormat("    WHERE  SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
                .AppendFormat("       AND SHISAKU_LIST_CODE = '{0}' ", aShisakuListCode)
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
                .AppendFormat("          BASE.SHISAKU_BUKA_CODE = '{0}' ", aShisakuBukaCode)
                .AppendFormat("      AND BASE.SHISAKU_BLOCK_NO = '{0}'", aShisakuBlockNo)
                .AppendLine("    GROUP BY ")
                .AppendLine("             BASE.SHISAKU_EVENT_CODE ")
                .AppendLine("            ,BASE.SHISAKU_LIST_CODE ")
                .AppendLine("            ,BASE.SHISAKU_BUKA_CODE ")
                .AppendLine("            ,BASE.SHISAKU_BLOCK_NO ")

            End With

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Return dtResult

        End Function
#End Region

#Region "SELECT 試作手配帳(基本情報)一覧を返す(トランザクション用)"
        ''' <summary>
        ''' 試作手配帳(基本情報)一覧を返す
        '''         (トランザクション用)
        ''' ※NmTDColBaseにより列名参照
        ''' 
        ''' </summary>
        ''' <returns>該当する一覧</returns>
        ''' <remarks></remarks>
        Public Shared Function FindAllBaseInfo(ByVal aDb As SqlAccess, ByVal aShisakuEventCode As String, ByVal aShisakuListCode As String) As DataTable

            Dim dtResult As DataTable = New DataTable
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
                .AppendLine("ZAIRYO_SUNPO_X, ")
                .AppendLine("ZAIRYO_SUNPO_Y, ")
                .AppendLine("ZAIRYO_SUNPO_Z, ")
                .AppendLine("ZAIRYO_SUNPO_XY, ")
                .AppendLine("ZAIRYO_SUNPO_XZ, ")
                .AppendLine("ZAIRYO_SUNPO_YZ, ")
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
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}' ", aShisakuListCode)
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

            aDb.Fill(sql.ToString, dtResult)

            Return dtResult

        End Function

#End Region

#Region "SELECT 試作手配帳(基本情報)の主キー検索(トランザクション用)"
        ''' <summary>
        ''' SELECT 試作手配帳(基本情報)の主キー検索(トランザクション用)
        '''         (トランザクション用)
        ''' ※NmTDColBaseにより列名参照
        ''' 
        ''' </summary>
        ''' <returns>該当する一覧</returns>
        ''' <remarks></remarks>
        Public Shared Function FindPkBaseInfo(ByVal aDb As SqlAccess, _
                                                                 ByVal aShisakuEventCode As String, _
                                                                 ByVal aShisakuListCode As String, _
                                                                 ByVal aShisakuListKaiteiNo As String, _
                                                                 ByVal aShisakuBukaCode As String, _
                                                                 ByVal aShisakuBlockNo As String, _
                                                                 ByVal aBuhinNoHyoujiJun As String) As DataTable


            Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT SHISAKU_EVENT_CODE,SHISAKU_LIST_CODE, SHISAKU_LIST_CODE_KAITEI_NO,SHISAKU_BLOCK_NO,SHISAKU_BUKA_CODE,BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat("         SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}'  ", aShisakuListCode)
                .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}'", aShisakuListKaiteiNo)
                .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", aShisakuBlockNo)
                .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", aShisakuBukaCode)
                .AppendFormat(" AND BUHIN_NO_HYOUJI_JUN = {0} ", aBuhinNoHyoujiJun)
            End With
            Dim dtResult As New DataTable
            aDb.Fill(sql.ToString, dtResult)

            Return dtResult

        End Function

#End Region

#Region "SELECT 試作手配帳(号車情報)の主キー検索(トランザクション用)"
        '''' <summary>
        '''' SELECT 試作手配帳(号車情報)の主キー検索(トランザクション用)
        ''''         (トランザクション用)
        '''' ※NmTDColBaseにより列名参照
        '''' 
        '''' </summary>
        '''' <returns>該当する一覧</returns>
        '''' <remarks></remarks>
        'Public Shared Function FindPkGousyaInfo(ByVal aDb As SqlAccess, _
        '                                                         ByVal aShisakuEventCode As String, _
        '                                                         ByVal aShisakuListCode As String, _
        '                                                         ByVal aShisakuListKaiteiNo As String, _
        '                                                         ByVal aShisakuBukaCode As String, _
        '                                                         ByVal aShisakuBlockNo As String, _
        '                                                         ByVal aBuhinNoHyoujiJun As String) As DataTable

        '    Dim dtResult As DataTable = New DataTable
        '    Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder

        '    With sql
        '        .AppendLine(" SELECT *  ")
        '        .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA WITH (NOLOCK, NOWAIT) ")
        '        .AppendLine(" WHERE ")
        '        .AppendFormat("         SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
        '        .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}'  ", aShisakuListCode)
        '        .AppendFormat(" AND SHISAKU_LIST_CODE_KAITEI_NO = '{0}' ", aShisakuListKaiteiNo)
        '        .AppendFormat(" AND SHISAKU_BLOCK_NO = '{0}' ", aShisakuBlockNo)
        '        .AppendFormat(" AND SHISAKU_BUKA_CODE = '{0}' ", aShisakuBukaCode)
        '        .AppendFormat(" AND BUHIN_NO_HYOUJI_JUN = {0} ", aBuhinNoHyoujiJun)
        '    End With


        '    aDb.Fill(sql.ToString, dtResult)

        '    Return dtResult

        'End Function

#End Region

#Region "SELECT 試作手配帳使用号車名一覧を返す"
        ''' <summary>
        ''' 試作手配帳使用号車名一覧を返す
        ''' 
        '''※ NmTDColGousyaListにより列名参照
        ''' 
        ''' </summary>
        ''' <param name="aShisakuEventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindAllGousyaNameList(ByVal aShisakuEventCode As String, ByVal aShisakuListCode As String) As DataTable

            Dim dtResult As DataTable = New DataTable
            Dim sql As New System.Text.StringBuilder

            With sql
                .AppendLine(" SELECT ")
                .AppendLine(" TG.SHISAKU_EVENT_CODE ")
                .AppendLine(" ,TG.SHISAKU_GOUSYA_HYOUJI_JUN AS HYOJIJUN_NO ")
                .AppendLine(" ,TG.SHISAKU_GOUSYA ")
                .AppendLine(" ,TG.M_NOUNYU_SHIJIBI ")
                .AppendLine(" ,TG.T_NOUNYU_SHIJIBI ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA TG WITH (NOLOCK, NOWAIT) ")
                .AppendFormat(" WHERE TG.SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
                .AppendFormat(" AND TG.SHISAKU_LIST_CODE = '{0}' ", aShisakuListCode)
                .AppendLine(" AND TG.SHISAKU_LIST_CODE_KAITEI_NO = ( ")
                .AppendLine("    SELECT MAX(SHISAKU_LIST_CODE_KAITEI_NO) AS SHISAKU_LIST_CODE_KAITEI_NO ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA AS MAX_KIHON WITH (NOLOCK, NOWAIT) ")
                .AppendLine("    WHERE TG.SHISAKU_EVENT_CODE = SHISAKU_EVENT_CODE ")
                .AppendLine("    AND TG.SHISAKU_LIST_CODE = SHISAKU_LIST_CODE ) ")
                .AppendLine(" GROUP BY TG.SHISAKU_EVENT_CODE, TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA,TG.M_NOUNYU_SHIJIBI,TG.T_NOUNYU_SHIJIBI ")
                .AppendLine(" ORDER BY TG.SHISAKU_EVENT_CODE, TG.SHISAKU_GOUSYA_HYOUJI_JUN,TG.SHISAKU_GOUSYA ")
            End With

            '名称一覧取得
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Return dtResult

        End Function
#End Region

#Region "SELECT 試作手配帳(号車)一覧を返す"
        ''' <summary>
        ''' 試作手配帳(号車)一覧を返す
        ''' 
        ''' ※NmTDColGousyaにより列名参照
        ''' 
        ''' </summary>
        ''' <param name="aShisakuEventCode"></param>
        ''' <param name="aShisakuListCode"></param>
        ''' <param name="aDtNameListGousya">号車名称一覧データテーブル</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindAllGousyaInfo( _
                                                 ByVal aShisakuEventCode As String, _
                                                 ByVal aShisakuListCode As String, _
                                                 ByVal aDtNameListGousya As DataTable _
                                                 ) As DataTable

            Dim dtResult As DataTable = New DataTable

            '動的列部分SQL(号車名を列に)
            Dim sqlList As New StringBuilder()
            sqlList.Remove(0, sqlList.Length)

            '号車名称が列名にするクロス集計記述
            For Each dtRow As DataRow In aDtNameListGousya.Rows
                sqlList.AppendFormat(",SUM(CASE WHEN SHISAKU_GOUSYA = '{0}' THEN INSU_SURYO ELSE 0 END) AS '{0}' ", dtRow("SHISAKU_GOUSYA"))
            Next

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
                .AppendLine(sqlList.ToString)
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON AS KIHON ")
                .AppendLine("LEFT JOIN  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA AS GOUSYA  ")
                .AppendLine("ON          KIHON.SHISAKU_EVENT_CODE = GOUSYA.SHISAKU_EVENT_CODE   ")
                .AppendLine("    AND    KIHON.SHISAKU_LIST_CODE =  GOUSYA.SHISAKU_LIST_CODE  ")
                .AppendLine("    AND    KIHON.SHISAKU_LIST_CODE_KAITEI_NO = GOUSYA.SHISAKU_LIST_CODE_KAITEI_NO              ")
                .AppendLine("    AND KIHON.SHISAKU_BUKA_CODE = GOUSYA.SHISAKU_BUKA_CODE  ")
                .AppendLine("    AND KIHON.SHISAKU_BLOCK_NO = GOUSYA.SHISAKU_BLOCK_NO  ")
                .AppendLine("    AND KIHON.BUHIN_NO_HYOUJI_JUN = GOUSYA.BUHIN_NO_HYOUJI_JUN     ")
                .AppendFormat("WHERE KIHON.SHISAKU_EVENT_CODE = '{0}'  ", aShisakuEventCode)
                .AppendFormat("    AND KIHON.SHISAKU_LIST_CODE = '{0}'  ", aShisakuListCode)
                .AppendLine("     AND KIHON.SHISAKU_LIST_CODE_KAITEI_NO =   ")
                .AppendLine("(   ")
                .AppendLine("     SELECT MAX (SHISAKU_LIST_CODE_KAITEI_NO) AS SHISAKU_LIST_CODE_KAITEI_NO  ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_KIHON   ")
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



            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Return dtResult
        End Function

#End Region

#Region "SELECT 試作ブロック情報取得"

        ''' <summary>
        ''' 最新のブロックNo改訂Noをもつ試作設計ブロック情報を参照する
        ''' </summary>
        ''' <returns>試作設計ブロック情報</returns>
        ''' <remarks></remarks>
        Public Function FindNewestBlockBy(ByVal aShisakuEventCode As String, _
                                                            ByVal aShisakuBukaCode As String, _
                                                            ByVal aShisakuBlockNo As String _
                                                            ) As TShisakuSekkeiBlockVo _
        '    Dim blockDao As TShisakuSekkeiBlockDao = New TShisakuSekkeiBlockDaoImpl
            '    Dim param As New TShisakuSekkeiBlockVo
            '    param.ShisakuEventCode = aShisakuEventCode
            '    param.ShisakuBukaCode = aShisakuBukaCode
            '    param.ShisakuBlockNo = aShisakuBlockNo

            '    Dim blockVos As List(Of TShisakuSekkeiBlockVo) = blockDao.FindBy(param)
            '    If blockVos.Count = 0 Then
            '        Return Nothing
            '    End If
            '    '         blockVos.Sort(New BlockNoKaiteiNoComparer)
            '    Return blockVos(blockVos.Count - 1)
            Return Nothing
        End Function
#End Region

#Region "DELETE 試作手配帳(基本)"
        ''' <summary>
        ''' DELETE 試作手配帳(基本)
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <param name="aDataRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DelTehaichoBasebuhinNo(ByVal aDb As SqlAccess, ByVal aDataRow As DataRow) As Boolean

            Dim sql As String = SqlTehaichoEdit.GetSqlDelTehaiBaseKey
            Dim resultCnt As Integer = -1

            aDb.ClearParameters()
            aDb.AddParameter("@ShisakuEventCode", aDataRow(NmDTColBase.TD_SHISAKU_EVENT_CODE), DbType.AnsiString)
            aDb.AddParameter("@ShisakuListCode", aDataRow(NmDTColBase.TD_SHISAKU_LIST_CODE), DbType.AnsiString)
            aDb.AddParameter("@ShisakuListCodeKaiteiNo", aDataRow(NmDTColBase.TD_SHISAKU_LIST_CODE_KAITEI_NO), DbType.AnsiString)
            aDb.AddParameter("@ShisakuBlockNo", aDataRow(NmDTColBase.TD_SHISAKU_BLOCK_NO), DbType.AnsiString)
            aDb.AddParameter("@ShisakuBukaCode", aDataRow(NmDTColBase.TD_SHISAKU_BUKA_CODE), DbType.AnsiString)
            aDb.AddParameter("@BuhinNoHyoujiJun", aDataRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN), DbType.AnsiString)

            resultCnt = aDb.ExecuteNonQuery(sql)

            '結果異常の場合は終了
            If resultCnt <> 1 Then
                Dim msg As String = String.Format("DELETE試作手配帳(基本)で削除件数異常 {0} ﾌﾞﾛｯｸNo:{1} 部品番号表示順:{2} ", _
                                                        vbCrLf, aDataRow(NmDTColBase.TD_SHISAKU_BLOCK_NO), aDataRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN))
                Throw New Exception(msg)
                Return False
            End If

            Return True

        End Function

#End Region

#Region "DELETE 試作手配帳(号車)主キー検索"
        ''' <summary>
        ''' DELETE 試作手配帳(号車)主キー
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <param name="aDataRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DelTehaichoGousyabuhinNoKey(ByVal aDb As SqlAccess, ByVal aDataRow As DataRow) As Boolean

            Dim sql As String = SqlTehaichoEdit.GetSqlDelTehaiGousyaKey
            Dim resultCnt As Integer = -1

            aDb.ClearParameters()
            aDb.AddParameter("@ShisakuEventCode", aDataRow(NmDTColGousya.TD_SHISAKU_EVENT_CODE), DbType.AnsiString)
            aDb.AddParameter("@ShisakuListCode", aDataRow(NmDTColGousya.TD_SHISAKU_LIST_CODE), DbType.AnsiString)
            aDb.AddParameter("@ShisakuListCodeKaiteiNo", aDataRow(NmDTColGousya.TD_SHISAKU_LIST_CODE_KAITEI_NO), DbType.AnsiString)
            aDb.AddParameter("@ShisakuBlockNo", aDataRow(NmDTColGousya.TD_SHISAKU_BLOCK_NO), DbType.AnsiString)
            aDb.AddParameter("@ShisakuBukaCode", aDataRow(NmDTColGousya.TD_SHISAKU_BUKA_CODE), DbType.AnsiString)
            aDb.AddParameter("@BuhinNoHyoujiJun", aDataRow(NmDTColGousya.TD_BUHIN_NO_HYOUJI_JUN), DbType.AnsiString)

            resultCnt = aDb.ExecuteNonQuery(sql)

            '結果異常の場合は終了
            If Not resultCnt = 0 AndAlso Not resultCnt = 1 Then
                Dim msg As String = String.Format("DELETE試作手配帳(号車)で削除件数異常 {0} ﾌﾞﾛｯｸNo:{1} 部品番号表示順:{2} ", _
                                                        vbCrLf, aDataRow(aDataRow(NmDTColBase.TD_SHISAKU_BLOCK_NO)), aDataRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN))
                Throw New Exception(msg)
                Return False
            End If

            Return True

        End Function

#End Region

#Region "DELETE 試作手配帳(基本)ブロックNo単位"
        ''' <summary>
        ''' DELETE 試作手配帳(基本)リストコード
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <param name="aShisakuEventCode"></param>
        ''' <param name="aShisakuListCode"></param>
        ''' <param name="aShisakuListKaiteiNo"></param>
        ''' <param name="aShisakuBlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DelTehaichoBaseBlockNo(ByVal aDb As SqlAccess, _
                                                                                            ByVal aShisakuEventCode As String, _
                                                                                            ByVal aShisakuListCode As String, _
                                                                                            ByVal aShisakuListKaiteiNo As String, _
                                                                                            ByVal aShisakuBukaCode As String, _
                                                                                            ByVal aShisakuBlockNo As String _
                                                                                            ) As Boolean

            Dim sql As String = SqlTehaichoEdit.GetSqlDelTehaiBaseBlockNo
            Dim resultCnt As Integer = -1

            aDb.ClearParameters()
            aDb.AddParameter("@ShisakuEventCode", aShisakuEventCode, DbType.AnsiString)
            aDb.AddParameter("@ShisakuListCode", aShisakuListCode, DbType.AnsiString)
            aDb.AddParameter("@ShisakuListCodeKaiteiNo", aShisakuListKaiteiNo, DbType.AnsiString)
            aDb.AddParameter("@ShisakuBukaCode", aShisakuBukaCode, DbType.AnsiString)
            aDb.AddParameter("@ShisakuBlockNo", aShisakuBlockNo, DbType.AnsiString)

            resultCnt = aDb.ExecuteNonQuery(sql)

            '結果異常の場合は終了
            If resultCnt < 0 Then
                Dim msg As String = String.Format("DELETE試作手配帳(号車)で削除件数異常  件数:{0}  {1} イベントコード:{2} リストコード:{3} ", _
                                                       resultCnt, vbCrLf, aShisakuEventCode, aShisakuListCode)
                Throw New Exception(msg)
                Return False
            End If

            Return True
        End Function

#End Region

#Region "DELETE 試作手配帳(号車)ブロックNo単位"
        ''' <summary>
        ''' DELETE 試作手配帳(号車)リストコード
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <param name="aShisakuEventCode"></param>
        ''' <param name="aShisakuListCode"></param>
        ''' <param name="aShisakuListKaiteiNo"></param>
        ''' <param name="aShisakuBlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DelTehaichoGousyaBlockNo(ByVal aDb As SqlAccess, _
                                                                                            ByVal aShisakuEventCode As String, _
                                                                                            ByVal aShisakuListCode As String, _
                                                                                            ByVal aShisakuListKaiteiNo As String, _
                                                                                            ByVal aShisakuBukaCode As String, _
                                                                                            ByVal aShisakuBlockNo As String _
                                                                                            ) As Boolean

            Dim sql As String = SqlTehaichoEdit.GetSqlDelTehaiGousyaBlockNo
            Dim resultCnt As Integer = -1

            aDb.ClearParameters()
            aDb.AddParameter("@ShisakuEventCode", aShisakuEventCode, DbType.AnsiString)
            aDb.AddParameter("@ShisakuListCode", aShisakuListCode, DbType.AnsiString)
            aDb.AddParameter("@ShisakuListCodeKaiteiNo", aShisakuListKaiteiNo, DbType.AnsiString)
            '         aDb.AddParameter("@ShisakuBukaCode", aShisakuBukaCode, DbType.AnsiString)
            aDb.AddParameter("@ShisakuBlockNo", aShisakuBlockNo, DbType.AnsiString)

            resultCnt = aDb.ExecuteNonQuery(sql)

            '結果異常の場合は終了
            If resultCnt < 0 Then
                Dim msg As String = String.Format("DELETE試作手配帳(号車)で削除件数異常  件数:{0}  {1} イベントコード:{2} リストコード:{3} ", _
                                                       resultCnt, vbCrLf, aShisakuEventCode, aShisakuListCode)
                Throw New Exception(msg)
                Return False
            End If

            Return True
        End Function

#End Region

#Region "UPDATE 保存機能(試作リストコード情報更新)"
        ''' <summary>
        ''' UPDATE 保存機能(試作リストコード情報更新)
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function UpdShisakuList(ByVal aDb As SqlAccess, _
                                                        ByVal aKoujiNo As String, _
                                                        ByVal aShisakuEventCode As String, _
                                                        ByVal aShisakuListCode As String, _
                                                        ByVal aShisakuListKaiteiNo As String) As Boolean
            Dim aDate As New ShisakuDate


            '-更新対象項目
            aDb.ClearParameters()
            aDb.AddParameter("@STATUS", "6A", DbType.String)
            aDb.AddParameter("@SHISAKU_KOUJI_NO", aKoujiNo, DbType.String)
            aDb.AddParameter("@UPDATED_USER_ID", LoginInfo.Now.UserId, DbType.String)
            aDb.AddParameter("@UPDATED_DATE", aDate.CurrentDateDbFormat, DbType.String)
            aDb.AddParameter("@UPDATED_TIME", aDate.CurrentTimeDbFormat, DbType.String)
            '-検索条件項目
            aDb.AddParameter("@SHISAKU_EVENT_CODE", aShisakuEventCode, DbType.String)
            aDb.AddParameter("@SHISAKU_LIST_CODE", aShisakuListCode, DbType.String)
            aDb.AddParameter("@SHISAKU_LIST_CODE_KAITEI_NO", aShisakuListKaiteiNo, DbType.String)

            Dim resCnt As Integer = aDb.ExecuteNonQuery(SqlTehaichoEdit.GetSqlUpdShisakuList_koujiNo)

            If resCnt <> 1 Then
                Throw New Exception("試作リストコードTBLの更新で問題が発生しました(SQL実行件数異常)")
            End If

            Return True

        End Function

#End Region

#Region "UPDATE試作手配(基本)(イベントコード、リストコード)"
        Public Shared Function UpdShisakuTehaiBaseEventListKey(ByVal aDb As SqlAccess, _
                                                        ByVal aUnitKbn As String, _
                                                        ByVal aSeihinKbn As String, _
                                                        ByVal aShisakuEventCode As String, _
                                                        ByVal aShisakuListCode As String, _
                                                        ByVal aShisakuListKaiteiNo As String) As Boolean
            Dim aDate As New ShisakuDate

            '-更新対象項目
            aDb.ClearParameters()
            aDb.AddParameter("@UnitKbn", aUnitKbn, DbType.String)
            aDb.AddParameter("@ShisakuSeihinKbn", aSeihinKbn, DbType.String)
            aDb.AddParameter("@UpdatedUserId", LoginInfo.Now.UserId, DbType.String)
            aDb.AddParameter("@UpdatedDate", aDate.CurrentDateDbFormat, DbType.String)
            aDb.AddParameter("@UpdatedTime", aDate.CurrentTimeDbFormat, DbType.String)
            '-検索条件項目
            aDb.AddParameter("@ShisakuEventCode", aShisakuEventCode, DbType.String)
            aDb.AddParameter("@ShisakuListCode", aShisakuListCode, DbType.String)
            aDb.AddParameter("@ShisakuListCodeKaiteiNo", aShisakuListKaiteiNo, DbType.String)

            Dim resCnt As Integer = aDb.ExecuteNonQuery(SqlTehaichoEdit.GetSqlUpdTehaiBaseEventListKey)

            If resCnt < 0 Then
                Throw New Exception("試作手配(基本)(イベントコード、リストコード)TBLの更新で問題が発生しました(SQL実行件数異常)")
            End If

            Return True

        End Function
#End Region

#Region "UPDATE 試作手配帳(基本)"
        ''' <summary>
        ''' UPDATE 試作手配帳(基本)
        ''' </summary>
        ''' <param name="aHeader"></param>
        ''' <param name="aDb"></param>
        ''' <param name="aSheet"></param>
        ''' <param name="aRow">スプレッド行位置</param>
        ''' <param name="aSortJun">SORT_NOに格納</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function UpdTehaiBase(ByVal aHeader As TehaichoEditHeader, _
                                                        ByVal aDb As SqlAccess, _
                                                        ByVal aSheet As FarPoint.Win.Spread.SheetView, _
                                                        ByVal aRow As Integer, _
                                                        ByVal aSortJun As Integer, _
                                                        ByVal tehaichoVo As TShisakuTehaiKihonVo) As Boolean

            Try
                Dim aDate As New ShisakuDate

                '-更新対象項目
                aDb.ClearParameters()
                aDb.AddParameter("@SortJun", aSortJun, DbType.Int32)
                aDb.AddParameter("@Rireki", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_RIREKI)), DbType.String)
                aDb.AddParameter("@GyouId", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_GYOU_ID)), DbType.String)
                aDb.AddParameter("@SenyouMark", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SENYOU_MARK)), DbType.String)
                aDb.AddParameter("@Level", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_LEVEL)), DbType.String)
                aDb.AddParameter("@BuhinNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO)), DbType.String)
                aDb.AddParameter("@BuhinNoKbn", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO_KBN)), DbType.String)
                aDb.AddParameter("@BuhinNoKaiteiNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO_KAITEI_NO)), DbType.String)
                aDb.AddParameter("@EdaBan", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_EDA_BAN)), DbType.String)
                aDb.AddParameter("@BuhinName", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NAME)), DbType.String)
                aDb.AddParameter("@ShukeiCode", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHUKEI_CODE)), DbType.String)
                aDb.AddParameter("@TehaiKigou", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TEHAI_KIGOU)), DbType.String)
                aDb.AddParameter("@Koutan", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KOUTAN)), DbType.String)
                aDb.AddParameter("@TorihikisakiCode", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)), DbType.String)
                aDb.AddParameter("@Nouba", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_NOUBA)), DbType.String)
                aDb.AddParameter("@KyoukuSection", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KYOUKU_SECTION)), DbType.String)

                '納入指示日
                Dim intNounyuDate As Integer
                intNounyuDate = TehaichoEditLogic.ConvInt8Date(aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)))
                aDb.AddParameter("@NounyuShijibi", intNounyuDate, DbType.Int32)

                '合計数量
                Dim strTotal As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TOTAL_INSU_SURYO))

                If strTotal Is Nothing = False Then
                    If strTotal.Equals("**") Then
                        strTotal = "-1"
                    ElseIf strTotal.Equals(String.Empty) = False Then
                        If IsNumeric(strTotal) = False Then
                            Throw New Exception(String.Format("合計数量項目の数値変換で問題が発生しました。合計数量:{0}", strTotal))
                        End If
                    Else
                        strTotal = "0"
                    End If
                Else
                    strTotal = "0"
                End If

                aDb.AddParameter("@TotalInsuSuryo", Integer.Parse(strTotal), DbType.Int32)
                aDb.AddParameter("@Saishiyoufuka", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SAISHIYOUFUKA)), DbType.String)

                '出図予定日
                Dim intShutuzuYoteiDate As Integer
                intShutuzuYoteiDate = TehaichoEditLogic.ConvInt8Date(aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE)))
                aDb.AddParameter("@ShutuzuYoteiDate", intShutuzuYoteiDate, DbType.Int32)

                '出図実績_日付
                Dim intShutuzuJisekiDate As Integer
                intShutuzuJisekiDate = TehaichoEditLogic.ConvInt8Date(aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE)))
                aDb.AddParameter("@ShutuzuJisekiDate", intShutuzuJisekiDate, DbType.Int32)
                '出図実績_改訂№
                aDb.AddParameter("@ShutuzuJisekiKaiteiNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)), DbType.String)
                '出図実績_設通№
                aDb.AddParameter("@ShutuzuJisekiStsrDhstba", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)), DbType.String)

                '最終織込設変情報_日付
                Dim intSaisyuSetsuhenDate As Integer
                intSaisyuSetsuhenDate = TehaichoEditLogic.ConvInt8Date(aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE)))
                aDb.AddParameter("@SaisyuSetsuhenDate", intSaisyuSetsuhenDate, DbType.Int32)
                '最終織込設変情報_改訂№
                aDb.AddParameter("@SaisyuSetsuhenKaiteiNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)), DbType.String)
                '最終織込設変情報_設通№
                aDb.AddParameter("@StsrDhstba", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA)), DbType.String)

                '材料寸法_X(mm)
                Dim strX As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X))
                If strX Is Nothing = False AndAlso strX.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strX) = False Then
                        Throw New Exception(String.Format("数値変換で問題が発生しました。{0}", strX))
                    End If
                Else
                    strX = "0"
                End If
                aDb.AddParameter("@ZairyoSunpoX", Decimal.Parse(strX), DbType.Decimal)

                '材料寸法_Y(mm)
                Dim strY As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y))
                If strY Is Nothing = False AndAlso strY.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strY) = False Then
                        Throw New Exception(String.Format("数値変換で問題が発生しました。{0}", strY))
                    End If
                Else
                    strY = "0"
                End If
                aDb.AddParameter("@ZairyoSunpoY", Decimal.Parse(strY), DbType.Decimal)

                '材料寸法_Z(mm)
                Dim strZ As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z))
                If strZ Is Nothing = False AndAlso strZ.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strZ) = False Then
                        Throw New Exception(String.Format("数値変換で問題が発生しました。{0}", strZ))
                    End If
                Else
                    strZ = "0"
                End If
                aDb.AddParameter("@ZairyoSunpoZ", Decimal.Parse(strZ), DbType.Decimal)

                '材料寸法_X+Y(mm)
                Dim strXy As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY))
                If strXy Is Nothing = False AndAlso strXy.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strXy) = False Then
                        Throw New Exception(String.Format("数値変換で問題が発生しました。{0}", strXy))
                    End If
                Else
                    strXy = "0"
                End If
                aDb.AddParameter("@ZairyoSunpoXy", Decimal.Parse(strXy), DbType.Decimal)

                '材料寸法_X+Z(mm)
                Dim strXz As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ))
                If strXz Is Nothing = False AndAlso strXz.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strXz) = False Then
                        Throw New Exception(String.Format("数値変換で問題が発生しました。{0}", strXz))
                    End If
                Else
                    strXz = "0"
                End If
                aDb.AddParameter("@ZairyoSunpoXz", Decimal.Parse(strXz), DbType.Decimal)

                '材料寸法_Y+Z(mm)
                Dim strYz As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ))
                If strYz Is Nothing = False AndAlso strYz.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strYz) = False Then
                        Throw New Exception(String.Format("数値変換で問題が発生しました。{0}", strYz))
                    End If
                Else
                    strYz = "0"
                End If
                aDb.AddParameter("@ZairyoSunpoYz", Decimal.Parse(strYz), DbType.Decimal)


                ''↓↓2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
                aDb.AddParameter("@TsukurikataSeisaku", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_SEISAKU)), DbType.String)
                aDb.AddParameter("@TsukurikataKatashiyou1", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_1)), DbType.String)
                aDb.AddParameter("@TsukurikataKatashiyou2", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_2)), DbType.String)
                aDb.AddParameter("@TsukurikataKatashiyou3", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_3)), DbType.String)
                aDb.AddParameter("@TsukurikataTigu", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_TIGU)), DbType.String)
                Dim intTsukurikataNounyu As Integer
                intTsukurikataNounyu = TehaichoEditLogic.ConvInt8Date(aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_NOUNYU)))
                aDb.AddParameter("@TsukurikataNounyu", intTsukurikataNounyu, DbType.Int32)
                aDb.AddParameter("@TsukurikataKibo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_KIBO)), DbType.String)
                ''↑↑2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD END

                aDb.AddParameter("@ZaishituKikaku1", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1)), DbType.String)
                aDb.AddParameter("@ZaishituKikaku2", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2)), DbType.String)
                aDb.AddParameter("@ZaishituKikaku3", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3)), DbType.String)
                aDb.AddParameter("@ZaishituMekki", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI)), DbType.String)
                aDb.AddParameter("@ShisakuBankoSuryo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO)), DbType.String)
                aDb.AddParameter("@ShisakuBankoSuryoU", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U)), DbType.String)

                '↓↓↓2014/12/29 メタル項目を追加 TES)張 ADD BEGIN
                '材料情報・製品長
                Dim strLength As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH))
                If strLength Is Nothing = False AndAlso strLength.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strLength) = False Then
                        Throw New Exception(String.Format("製品長項目の数値変換で問題が発生しました。合計数量:{0}", strTotal))
                    End If
                Else
                    strLength = "0"
                End If
                aDb.AddParameter("@MaterialInfoLength", Integer.Parse(strLength), DbType.Int32)
                '材料情報・製品幅
                Dim strWidth As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH))
                If strWidth Is Nothing = False AndAlso strWidth.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strWidth) = False Then
                        Throw New Exception(String.Format("製品幅項目の数値変換で問題が発生しました。合計数量:{0}", strTotal))
                    End If
                Else
                    strWidth = "0"
                End If
                aDb.AddParameter("@MaterialInfoWidth", Integer.Parse(strWidth), DbType.Int32)
                '材料情報・発注対象
                Dim isOrderTarget As Boolean = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET))
                If isOrderTarget Then
                    aDb.AddParameter("@MaterialInfoOrderTarget", "1", DbType.String)
                Else
                    aDb.AddParameter("@MaterialInfoOrderTarget", "0", DbType.String)
                End If
                '材料情報・発注対象最終更新日
                aDb.AddParameter("@MaterialInfoOrderTargetDate", aSheet.GetNote(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET)), DbType.String)
                '材料情報・発注済
                Dim isOrderChk As Boolean = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK))
                If isOrderChk Then
                    aDb.AddParameter("@MaterialInfoOrderChk", "1", DbType.String)
                Else
                    aDb.AddParameter("@MaterialInfoOrderChk", "0", DbType.String)
                End If
                '材料情報・発注済最終更新日
                aDb.AddParameter("@MaterialInfoOrderChkDate", aSheet.GetNote(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)), DbType.String)

                '2015/02/13　保存時に最終更新日の更新はいらない？
                '材料情報・発注済最終更新年月日
                'If tehaichoVo.MaterialInfoOrderChk IsNot Nothing Then
                '    If (StringUtil.Equals(tehaichoVo.MaterialInfoOrderChk, "1") And isOrderChk = False) Or _
                '       (StringUtil.Equals(tehaichoVo.MaterialInfoOrderChk, "0") And isOrderChk = True) Then
                '        'aDb.AddParameter("@MaterialInfoOrderChkDate", aDate.CurrentDateDbFormat, DbType.String)
                '        aDb.AddParameter("@MaterialInfoOrderChkDate", tehaichoVo.MaterialInfoOrderChkDate, DbType.String)
                '    Else
                '        aDb.AddParameter("@MaterialInfoOrderChkDate", tehaichoVo.MaterialInfoOrderChkDate, DbType.String)
                '    End If
                'Else
                '    aDb.AddParameter("@MaterialInfoOrderChkDate", DBNull.Value, DbType.String)
                'End If

                'データ項目・改訂№
                aDb.AddParameter("@DataItemKaiteiNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO)), DbType.String)
                'データ項目・エリア名
                aDb.AddParameter("@DataItemAreaName", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME)), DbType.String)
                'データ項目・セット名
                aDb.AddParameter("@DataItemSetName", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME)), DbType.String)
                'データ項目・改訂情報
                aDb.AddParameter("@DataItemKaiteiInfo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_INFO)), DbType.String)
                'データ項目・データ支給チェック欄
                Dim isDataProvision As Boolean = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION))
                If isDataProvision Then
                    aDb.AddParameter("@DataItemDataProvision", "1", DbType.String)
                Else
                    aDb.AddParameter("@DataItemDataProvision", "0", DbType.String)
                End If
                'データ項目・データ支給チェック欄最終更新日
                aDb.AddParameter("@DataItemDataProvisionDate", aSheet.GetNote(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION)), DbType.String)

                '2015/02/13　保存時に最終更新日の更新はいらない？
                ''データ項目・データ支給チェック欄最終更新年月日
                'If tehaichoVo.DataItemDataProvision IsNot Nothing Then
                '    If (StringUtil.Equals(tehaichoVo.DataItemDataProvision, "1") And isDataProvision = False) Or _
                '        (StringUtil.Equals(tehaichoVo.DataItemDataProvision, "0") And isDataProvision = True) Then
                '        'aDb.AddParameter("@DataItemDataProvisionDate", aDate.CurrentDateDbFormat, DbType.String)
                '        aDb.AddParameter("@DataItemDataProvisionDate", tehaichoVo.DataItemDataProvisionDate, DbType.String)
                '    Else
                '        aDb.AddParameter("@DataItemDataProvisionDate", tehaichoVo.DataItemDataProvisionDate, DbType.String)
                '    End If
                'Else
                '    aDb.AddParameter("@DataItemDataProvisionDate", DBNull.Value, DbType.String)
                'End If

                '変化点
                If String.Equals(aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_HENKATEN)), "削") Then
                    aDb.AddParameter("@Henkaten", "3", DbType.String)
                Else
                    aDb.AddParameter("@Henkaten", tehaichoVo.Henkaten, DbType.String)
                End If

                '↑↑↑2014/12/29 メタル項目を追加 TES)張 ADD END

                '部品費
                Dim strBuhinHi As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BUHINN_HI))

                If strBuhinHi Is Nothing = False AndAlso strBuhinHi.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strBuhinHi) = False Then
                        Throw New Exception(String.Format("部品費項目の数値変換で問題が発生しました。合計数量:{0}", strTotal))
                    End If
                Else
                    strBuhinHi = "0"
                End If
                aDb.AddParameter("@ShisakuBuhinnHi", Integer.Parse(strBuhinHi), DbType.String)

                '型費
                Dim strKatahi As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_KATA_HI))
                If strKatahi Is Nothing = False AndAlso strKatahi.Equals(String.Empty) = False Then
                    If IsNumeric(strKatahi) = False Then
                        Throw New Exception(String.Format("型費項目の数値変換で問題が発生しました。合計数量:{0}", strTotal))
                    End If
                Else
                    strKatahi = "0"
                End If
                aDb.AddParameter("@ShisakuKataHi", Integer.Parse(strKatahi), DbType.Int32)

                aDb.AddParameter("@MakerCode", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_MAKER_CODE)), DbType.String)
                aDb.AddParameter("@Bikou", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BIKOU)), DbType.String)
                aDb.AddParameter("@BuhinNoOya", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO_OYA)), DbType.String)
                aDb.AddParameter("@BuhinNoKbnOya", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO_KBN_OYA)), DbType.String)

                aDb.AddParameter("@ShisakuSeihinKbn", aHeader.seihinKbn, DbType.String)
                aDb.AddParameter("@AutoOrikomiKaiteiNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_AUTO_ORIKOMI_KAITEI_NO)), DbType.String)
                aDb.AddParameter("@UpdatedUserId", LoginInfo.Now.UserId, DbType.String)
                aDb.AddParameter("@UpdatedDate", aDate.CurrentDateDbFormat, DbType.String)
                aDb.AddParameter("@UpdatedTime", aDate.CurrentTimeDbFormat, DbType.String)
                '条件項目
                aDb.AddParameter("@ShisakuEventCode", aHeader.shisakuEventCode, DbType.String)
                aDb.AddParameter("@ShisakuListCode", aHeader.shisakuListCode, DbType.String)
                aDb.AddParameter("@ShisakuListCodeKaiteiNo", aHeader.shisakuListCodeKaiteiNo, DbType.String)
                aDb.AddParameter("@ShisakuBlockNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), DbType.String)
                aDb.AddParameter("@ShisakuBukaCode", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)), DbType.String)
                aDb.AddParameter("@BuhinNoHyoujiJun", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)), DbType.String)

                Dim resCnt As Integer = aDb.ExecuteNonQuery(SqlTehaichoEdit.GetSqlUpdTehaichoBase)

                If resCnt <> 1 Then
                    Throw New Exception("試作手配帳（基本）TBLの更新で問題が発生しました(SQL実行件数異常)")
                    Return False
                End If

                Return True

            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("下記ブロック、行ＩＤの保存中にエラーが発生しました{0}{1}", vbCrLf, _
                                                    "ブロック ： " & aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)) & vbCrLf & _
                                                    "行ＩＤ　 ： " & aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_GYOU_ID)) & vbCrLf, ex.Message))
                Return False
            Finally
                Cursor.Current = Cursors.Default
            End Try
        End Function
#End Region

#Region "INSERT 試作手配帳(基本)"
        ''' <summary>
        ''' INSERT 試作手配帳(基本)
        ''' </summary>
        ''' <param name="aHeader"></param>
        ''' <param name="aDb"></param>
        ''' <param name="aSheet"></param>
        ''' <param name="aRow">スプレッド行位置</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function InsTehaiBase(ByVal aHeader As TehaichoEditHeader, _
                                                        ByVal aUnitKbn As String, _
                                                        ByVal aDb As SqlAccess, _
                                                        ByVal aSheet As FarPoint.Win.Spread.SheetView, _
                                                        ByVal aRow As Integer, _
                                                        ByVal aSortJun As Integer) As Boolean

            Try
                Dim aDate As New ShisakuDate

                '-更新対象項目
                aDb.ClearParameters()
                aDb.AddParameter("@ShisakuEventCode", aHeader.shisakuEventCode, DbType.String)
                aDb.AddParameter("@ShisakuListCode", aHeader.shisakuListCode, DbType.String)
                aDb.AddParameter("@ShisakuListCodeKaiteiNo", aHeader.shisakuListCodeKaiteiNo, DbType.String)
                aDb.AddParameter("@ShisakuBlockNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)), DbType.String)
                aDb.AddParameter("@ShisakuBukaCode", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)), DbType.String)
                aDb.AddParameter("@BuhinNoHyoujiJun", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)), DbType.String)

                aDb.AddParameter("@SortJun", aSortJun, DbType.Int32)
                aDb.AddParameter("@Rireki", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_RIREKI)), DbType.String)
                aDb.AddParameter("@GyouId", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_GYOU_ID)), DbType.String)
                aDb.AddParameter("@SenyouMark", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SENYOU_MARK)), DbType.String)
                aDb.AddParameter("@Level", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_LEVEL)), DbType.String)

                aDb.AddParameter("@UnitKbn", aUnitKbn, DbType.String)

                aDb.AddParameter("@BuhinNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO)), DbType.String)
                aDb.AddParameter("@BuhinNoKbn", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO_KBN)), DbType.String)
                aDb.AddParameter("@BuhinNoKaiteiNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO_KAITEI_NO)), DbType.String)
                aDb.AddParameter("@EdaBan", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_EDA_BAN)), DbType.String)
                aDb.AddParameter("@BuhinName", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NAME)), DbType.String)
                aDb.AddParameter("@ShukeiCode", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHUKEI_CODE)), DbType.String)

                aDb.AddParameter("@GencyoCkdKbn", "", DbType.String)
                aDb.AddParameter("@TehaiKigou", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TEHAI_KIGOU)), DbType.String)
                aDb.AddParameter("@Koutan", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KOUTAN)), DbType.String)
                aDb.AddParameter("@TorihikisakiCode", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TORIHIKISAKI_CODE)), DbType.String)
                aDb.AddParameter("@Nouba", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_NOUBA)), DbType.String)
                aDb.AddParameter("@KyoukuSection", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_KYOUKU_SECTION)), DbType.String)

                '納入指示日
                Dim intNounyuDate As Integer
                intNounyuDate = TehaichoEditLogic.ConvInt8Date(aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_NOUNYU_SHIJIBI)))
                aDb.AddParameter("@NounyuShijibi", intNounyuDate, DbType.Int32)

                '合計数量
                Dim strTotal As String = aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TOTAL_INSU_SURYO))

                If strTotal.Equals(String.Empty) = False Then
                    If IsNumeric(strTotal) = False Then
                        Throw New Exception(String.Format("合計数量項目の数値変換で問題が発生しました。合計数量:{0}", strTotal))
                    End If
                Else
                    strTotal = "0"
                End If

                aDb.AddParameter("@TotalInsuSuryo", Integer.Parse(strTotal), DbType.Int32)
                aDb.AddParameter("@Saishiyoufuka", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SAISHIYOUFUKA)), DbType.String)

                '出図予定日
                Dim intShutuzuYoteiDate As Integer
                intShutuzuYoteiDate = TehaichoEditLogic.ConvInt8Date(aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHUTUZU_YOTEI_DATE)))
                aDb.AddParameter("@ShutuzuYoteiDate", intShutuzuYoteiDate, DbType.Int32)

                '出図実績_日付
                Dim intShutuzuJisekiDate As Integer
                intShutuzuJisekiDate = TehaichoEditLogic.ConvInt8Date(aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE)))
                aDb.AddParameter("@ShutuzuJisekiDate", intShutuzuJisekiDate, DbType.Int32)
                '出図実績_改訂№
                aDb.AddParameter("@ShutuzuJisekiKaiteiNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)), DbType.String)
                '出図実績_設通№
                aDb.AddParameter("@ShutuzuJisekiStsrDhstba", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA)), DbType.String)

                '最終織込設変情報_日付
                Dim intSaisyuSetsuhenDate As Integer
                intSaisyuSetsuhenDate = TehaichoEditLogic.ConvInt8Date(aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_DATE)))
                aDb.AddParameter("@SaisyuSetsuhenDate", intSaisyuSetsuhenDate, DbType.Int32)
                '最終織込設変情報_改訂№
                aDb.AddParameter("@SaisyuSetsuhenKaiteiNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)), DbType.String)
                '最終織込設変情報_設通№
                aDb.AddParameter("@StsrDhstba", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_STSR_DHSTBA)), DbType.String)

                '材料寸法_X(mm)
                Dim strX As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_X))
                If strX Is Nothing = False AndAlso strX.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strX) = False Then
                        Throw New Exception(String.Format("数値変換で問題が発生しました。:{0}", strX))
                    End If
                Else
                    strX = "0"
                End If
                aDb.AddParameter("@ZairyoSunpoX", Decimal.Parse(strX), DbType.Decimal)

                '材料寸法_Y(mm)
                Dim strY As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Y))
                If strY Is Nothing = False AndAlso strY.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strY) = False Then
                        Throw New Exception(String.Format("数値変換で問題が発生しました。:{0}", strY))
                    End If
                Else
                    strY = "0"
                End If
                aDb.AddParameter("@ZairyoSunpoY", Decimal.Parse(strY), DbType.Decimal)

                '材料寸法_Z(mm)
                Dim strZ As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_Z))
                If strZ Is Nothing = False AndAlso strZ.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strZ) = False Then
                        Throw New Exception(String.Format("数値変換で問題が発生しました。:{0}", strZ))
                    End If
                Else
                    strZ = "0"
                End If
                aDb.AddParameter("@ZairyoSunpoZ", Decimal.Parse(strZ), DbType.Decimal)

                '材料寸法_X+Y(mm)
                Dim strXy As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XY))
                If strXy Is Nothing = False AndAlso strXy.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strXy) = False Then
                        Throw New Exception(String.Format("数値変換で問題が発生しました。:{0}", strXy))
                    End If
                Else
                    strXy = "0"
                End If
                aDb.AddParameter("@ZairyoSunpoXy", Decimal.Parse(strXy), DbType.Decimal)

                '材料寸法_X+Z(mm)
                Dim strXz As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_XZ))
                If strXz Is Nothing = False AndAlso strXz.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strXz) = False Then
                        Throw New Exception(String.Format("数値変換で問題が発生しました。:{0}", strXz))
                    End If
                Else
                    strXz = "0"
                End If
                aDb.AddParameter("@ZairyoSunpoXz", Decimal.Parse(strXz), DbType.Decimal)

                '材料寸法_Y+Z(mm)
                Dim strYz As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAIRYO_SUNPO_YZ))
                If strYz Is Nothing = False AndAlso strYz.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strYz) = False Then
                        Throw New Exception(String.Format("数値変換で問題が発生しました。:{0}", strYz))
                    End If
                Else
                    strYz = "0"
                End If
                aDb.AddParameter("@ZairyoSunpoYz", Decimal.Parse(strYz), DbType.Decimal)


                ''↓↓2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
                aDb.AddParameter("@TsukurikataSeisaku", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_SEISAKU)), DbType.String)
                aDb.AddParameter("@TsukurikataKatashiyou1", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_1)), DbType.String)
                aDb.AddParameter("@TsukurikataKatashiyou2", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_2)), DbType.String)
                aDb.AddParameter("@TsukurikataKatashiyou3", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_3)), DbType.String)
                aDb.AddParameter("@TsukurikataTigu", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_TIGU)), DbType.String)
                Dim intTsukurikataNounyu As Integer
                intTsukurikataNounyu = TehaichoEditLogic.ConvInt8Date(aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_NOUNYU)))
                aDb.AddParameter("@TsukurikataNounyu", intTsukurikataNounyu, DbType.Int32)
                aDb.AddParameter("@TsukurikataKibo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_TSUKURIKATA_KIBO)), DbType.String)
                ''↑↑2014/08/27 Ⅰ.2.管理項目追加 酒井 ADD END

                aDb.AddParameter("@ZaishituKikaku1", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_1)), DbType.String)
                aDb.AddParameter("@ZaishituKikaku2", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_2)), DbType.String)
                aDb.AddParameter("@ZaishituKikaku3", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAISHITU_KIKAKU_3)), DbType.String)
                aDb.AddParameter("@ZaishituMekki", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_ZAISHITU_MEKKI)), DbType.String)
                aDb.AddParameter("@ShisakuBankoSuryo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO)), DbType.String)
                aDb.AddParameter("@ShisakuBankoSuryoU", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BANKO_SURYO_U)), DbType.String)

                '↓↓↓2014/12/29 メタル項目を追加 TES)張 ADD BEGIN
                '材料情報・製品長
                Dim strLength As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_MATERIAL_INFO_LENGTH))
                If strLength Is Nothing = False AndAlso strLength.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strLength) = False Then
                        Throw New Exception(String.Format("製品長項目の数値変換で問題が発生しました。合計数量:{0}", strTotal))
                    End If
                Else
                    strLength = "0"
                End If
                aDb.AddParameter("@MaterialInfoLength", Integer.Parse(strLength), DbType.Int32)
                '材料情報・製品幅
                Dim strWidth As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_MATERIAL_INFO_WIDTH))
                If strWidth Is Nothing = False AndAlso strWidth.ToString.Trim.Equals(String.Empty) = False Then
                    If IsNumeric(strWidth) = False Then
                        Throw New Exception(String.Format("製品幅項目の数値変換で問題が発生しました。合計数量:{0}", strTotal))
                    End If
                Else
                    strWidth = "0"
                End If
                aDb.AddParameter("@MaterialInfoWidth", Integer.Parse(strWidth), DbType.Int32)
                '材料情報・発注対象
                Dim isOrderTarget As Boolean = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET))
                If isOrderTarget Then
                    aDb.AddParameter("@MaterialInfoOrderTarget", "1", DbType.String)
                Else
                    aDb.AddParameter("@MaterialInfoOrderTarget", "0", DbType.String)
                End If
                '材料情報・発注対象最終更新年月日
                aDb.AddParameter("@MaterialInfoOrderTargetDate", DBNull.Value, DbType.String)
                '材料情報・発注済
                Dim isOrderChk As Boolean = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK))
                If isOrderChk Then
                    aDb.AddParameter("@MaterialInfoOrderChk", "1", DbType.String)
                Else
                    aDb.AddParameter("@MaterialInfoOrderChk", "0", DbType.String)
                End If
                '材料情報・発注済最終更新年月日
                aDb.AddParameter("@MaterialInfoOrderChkDate", DBNull.Value, DbType.String)
                'データ項目・改訂№
                aDb.AddParameter("@DataItemKaiteiNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_NO)), DbType.String)
                'データ項目・エリア名
                aDb.AddParameter("@DataItemAreaName", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME)), DbType.String)
                'データ項目・セット名
                aDb.AddParameter("@DataItemSetName", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME)), DbType.String)
                'データ項目・改訂情報
                aDb.AddParameter("@DataItemKaiteiInfo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_DATA_ITEM_KAITEI_INFO)), DbType.String)
                'データ項目・データ支給チェック欄
                Dim isDataProvision As Boolean = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION))
                If isDataProvision Then
                    aDb.AddParameter("@DataItemDataProvision", "1", DbType.String)
                Else
                    aDb.AddParameter("@DataItemDataProvision", "0", DbType.String)
                End If
                'データ項目・データ支給チェック欄最終更新年月日
                aDb.AddParameter("@DataItemDataProvisionDate", DBNull.Value, DbType.String)
                '↑↑↑2014/12/29 メタル項目を追加 TES)張 ADD END

                '部品費
                Dim strBuhinHi As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BUHINN_HI))
                If strBuhinHi Is Nothing = False AndAlso strBuhinHi.Equals(String.Empty) = False Then
                    If IsNumeric(strBuhinHi) = False Then
                        Throw New Exception(String.Format("部品費項目の数値変換で問題が発生しました。合計数量:{0}", strTotal))
                    End If
                Else
                    strBuhinHi = "0"
                End If
                aDb.AddParameter("@ShisakuBuhinnHi", Integer.Parse(strBuhinHi), DbType.String)

                '型費
                Dim strKatahi As String = aSheet.GetValue(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_KATA_HI))
                If strKatahi Is Nothing = False AndAlso strKatahi.Equals(String.Empty) = False Then
                    If IsNumeric(strKatahi) = False Then
                        Throw New Exception(String.Format("型費項目の数値変換で問題が発生しました。合計数量:{0}", strTotal))
                    End If
                Else
                    strKatahi = "0"
                End If
                aDb.AddParameter("@ShisakuKataHi", strKatahi, DbType.Int32)

                aDb.AddParameter("@MakerCode", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_MAKER_CODE)), DbType.String)
                aDb.AddParameter("@Bikou", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BIKOU)), DbType.String)
                aDb.AddParameter("@BuhinNoOya", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO_OYA)), DbType.String)
                aDb.AddParameter("@BuhinNoKbnOya", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO_KBN_OYA)), DbType.String)

                aDb.AddParameter("@ShisakuSeihinKbn", aHeader.seihinKbn, DbType.String)
                aDb.AddParameter("@AutoOrikomiKaiteiNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_AUTO_ORIKOMI_KAITEI_NO)), DbType.String)

                aDb.AddParameter("@CreatedUserId", LoginInfo.Now.UserId, DbType.String)
                aDb.AddParameter("@CreatedDate", aDate.CurrentDateDbFormat, DbType.String)
                aDb.AddParameter("@CreatedTime", aDate.CurrentTimeDbFormat, DbType.String)
                aDb.AddParameter("@UpdatedUserId", LoginInfo.Now.UserId, DbType.String)
                aDb.AddParameter("@UpdatedDate", aDate.CurrentDateDbFormat, DbType.String)
                aDb.AddParameter("@UpdatedTime", aDate.CurrentTimeDbFormat, DbType.String)

                Dim resCnt As Integer = aDb.ExecuteNonQuery(SqlTehaichoEdit.GetSqlInsTehaiBase)

                If resCnt <> 1 Then
                    Throw New Exception("試作手配帳（基本）TBLの追加で問題が発生しました(SQL実行件数異常)")
                    Return False
                End If

                Return True
            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("下記ブロック、行ＩＤの保存中にエラーが発生しました{0}{1}", vbCrLf, _
                                                    "ブロック ： " & aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)) & vbCrLf & _
                                                    "行ＩＤ　 ： " & aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_GYOU_ID)) & vbCrLf, ex.Message))
                Return False
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Function
#End Region

#Region "INSERT 試作手配帳(号車)"
        ''' <summary>
        ''' INSERT 試作手配帳(号車)
        ''' </summary>
        ''' <param name="aHeader"></param>
        ''' <param name="aDb"></param>
        ''' <param name="aSheet"></param>
        ''' <param name="aRow">スプレッドの行位置</param>
        ''' <param name="aSortJun">SORT_JUNに格納</param>
        ''' <param name="aDtGousyaName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function InsTehaiGousya(ByVal aHeader As TehaichoEditHeader, _
                                                        ByVal aDb As SqlAccess, _
                                                        ByVal aSheet As FarPoint.Win.Spread.SheetView, _
                                                        ByVal aRow As Integer, _
                                                        ByVal aSortJun As Integer, _
                                                        ByVal aDtGousyaName As DataTable) As Boolean
            Try
                Dim aDate As New ShisakuDate
                Dim intGousyaCol As Integer = TehaichoEditNames.START_COLUMMN_GOUSYA_NAME
                Dim hshGoushaHyoujiJun As New Hashtable

                For i As Integer = 0 To aDtGousyaName.Rows.Count - 1

                    Dim hyoujiJun As Integer = Integer.Parse(aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_HYOJIJUN_NO))
                    Dim strinsu As String = aSheet.GetText(aRow, intGousyaCol + i)
                    Dim intInsu As Integer = 0

                    '員数値判定
                    If strinsu.Equals("**") Then
                        intInsu = -1
                    ElseIf IsNumeric(strinsu) Then
                        intInsu = Integer.Parse(strinsu)
                    Else
                        Continue For
                    End If

                    If intInsu = 0 Then
                        Continue For
                    End If
                    Dim dumHyoujiJun As Integer = 0
                    If StringUtil.Equals("DUMMY", aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME)) Then
                        'DUMMYの登録はDUMMY号車の番号を探すことから始まる'
                        Dim key As New System.Text.StringBuilder
                        With key
                            .AppendLine(aHeader.shisakuEventCode)
                            .AppendLine(aHeader.shisakuListCode)
                        End With
                        If Not hshGoushaHyoujiJun.Contains(key.ToString) Then
                            Dim dtResult As DataTable = New DataTable
                            Dim sql As New System.Text.StringBuilder
                            With sql
                                .AppendLine(" SELECT DISTINCT SHISAKU_GOUSYA_HYOUJI_JUN  ")
                                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_TEHAI_GOUSYA WITH (NOLOCK, NOWAIT) ")
                                .AppendLine(" WHERE ")
                                .AppendFormat("         SHISAKU_EVENT_CODE = '{0}' ", aHeader.shisakuEventCode)
                                .AppendFormat(" AND SHISAKU_LIST_CODE = '{0}'  ", aHeader.shisakuListCode)
                                .AppendLine(" AND SHISAKU_GOUSYA = 'DUMMY' ")
                            End With

                            '名称一覧取得
                            'aDb.ClearParameters()
                            'aDb.AddParameter("@ShisakuEventCode", aHeader.shisakuEventCode, DbType.AnsiString)
                            'aDb.AddParameter("@ShisakuListCode", aHeader.shisakuListCode, DbType.String)
                            aDb.Fill(sql.ToString, dtResult)
                            If dtResult.Rows.Count > 0 Then
                                hshGoushaHyoujiJun.Add(key.ToString, dtResult.Rows(0).Item(0))
                            Else
                                hshGoushaHyoujiJun.Add(key.ToString, 0)
                            End If

                        End If
                        dumHyoujiJun = hshGoushaHyoujiJun.Item(key.ToString)
                    End If

                    aDb.ClearParameters()

                    '-更新対象項目
                    aDb.AddParameter("@ShisakuEventCode", aHeader.shisakuEventCode, DbType.String)
                    aDb.AddParameter("@ShisakuListCode", aHeader.shisakuListCode, DbType.String)
                    aDb.AddParameter("@ShisakuListCodeKaiteiNo", aHeader.shisakuListCodeKaiteiNo, DbType.String)
                    aDb.AddParameter("@ShisakuBukaCode", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_SHISAKU_BUKA_CODE)), DbType.String)
                    aDb.AddParameter("@ShisakuBlockNo", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagGousya.TAG_SHISAKU_BLOCK_NO)), DbType.String)
                    aDb.AddParameter("@BuhinNoHyoujiJun", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_BUHIN_NO_HYOUJI_JUN)), DbType.String)
                    aDb.AddParameter("@SortJun", aSortJun, DbType.Int32)
                    aDb.AddParameter("@GyouId", aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagBase.TAG_GYOU_ID)), DbType.String)
                    If StringUtil.Equals("DUMMY", aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME)) Then
                        If dumHyoujiJun = 0 Then
                            aDb.AddParameter("@ShisakuGousyaHyoujiJun", hyoujiJun + 2, DbType.Int32)
                        Else
                            aDb.AddParameter("@ShisakuGousyaHyoujiJun", dumHyoujiJun, DbType.Int32)
                        End If

                    Else
                        aDb.AddParameter("@ShisakuGousyaHyoujiJun", aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_HYOJIJUN_NO), DbType.Int32)
                    End If

                    aDb.AddParameter("@ShisakuGousya", aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME), DbType.String)

                    'メタル納入指示日
                    Dim intMdate As Integer = IIf(IsDBNull(aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI)), 0, aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI))
                    aDb.AddParameter("@MNounyuShijibi", intMdate, DbType.Int32)

                    'トリム納入指示日
                    Dim intTdate As Integer = IIf(IsDBNull(aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI)), 0, aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI))
                    aDb.AddParameter("@TNounyuShijibi", intTdate, DbType.Int32)
                    '
                    aDb.AddParameter("@InsuSuryo", intInsu, DbType.Int32)
                    aDb.AddParameter("@UpdatedUserId", LoginInfo.Now.UserId, DbType.String)
                    aDb.AddParameter("@UpdatedDate", aDate.CurrentDateDbFormat, DbType.String)
                    aDb.AddParameter("@UpdatedTime", aDate.CurrentTimeDbFormat, DbType.String)
                    aDb.AddParameter("@CreatedUserId", LoginInfo.Now.UserId, DbType.String)
                    aDb.AddParameter("@CreatedDate", aDate.CurrentDateDbFormat, DbType.String)
                    aDb.AddParameter("@CreatedTime", aDate.CurrentTimeDbFormat, DbType.String)

                    Dim resCnt As Integer = aDb.ExecuteNonQuery(SqlTehaichoEdit.GetSqlInsTehaichoGousya)

                    If resCnt <> 1 Then
                        Throw New Exception("試作手配帳（号車）TBLの追加で問題が発生しました(SQL実行件数異常)")
                        Return False
                    End If

                Next

                Return True

            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("下記ブロック、行ＩＤの保存中にエラーが発生しました{0}{1}", vbCrLf, _
                                                    "ブロック ： " & aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagGousya.TAG_SHISAKU_BLOCK_NO)) & vbCrLf & _
                                                    "行ＩＤ　 ： " & aSheet.GetText(aRow, TehaichoEditLogic.GetTagIdx(aSheet, NmSpdTagGousya.TAG_GYOU_ID)) & vbCrLf, ex.Message))
                Return False
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Function

#End Region

#Region "UPDATE 試作手配帳(号車)"
        ''' <summary>
        ''' UPDATE 試作手配帳(号車)
        ''' </summary>
        ''' <param name="aHeader"></param>
        ''' <param name="aDb"></param>
        ''' <param name="aDtGousyaName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function UpdTehaiGousya(ByVal aHeader As TehaichoEditHeader, _
                                              ByVal aDb As SqlAccess, _
                                              ByVal aDtGousyaName As DataTable) As Boolean
            Try
                Dim aDate As New ShisakuDate

                For i As Integer = 0 To aDtGousyaName.Rows.Count - 1
                    '号車名がブランク、またはDUMMYの場合スルー
                    Dim gousyaName As String = aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME)
                    If StringUtil.IsEmpty(gousyaName) Then
                        Continue For
                    ElseIf gousyaName = "DUMMY" Then
                        Continue For
                    End If

                    aDb.ClearParameters()

                    '-更新対象項目
                    aDb.AddParameter("@ShisakuEventCode", aHeader.shisakuEventCode, DbType.String)
                    aDb.AddParameter("@ShisakuListCode", aHeader.shisakuListCode, DbType.String)
                    aDb.AddParameter("@ShisakuListCodeKaiteiNo", aHeader.shisakuListCodeKaiteiNo, DbType.String)
                    aDb.AddParameter("@ShisakuGousya", aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_SHISAKU_GOUSYA_NAME), DbType.String)

                    'メタル納入指示日
                    Dim intMdate As Integer = IIf(IsDBNull(aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI)), 0, aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_M_NOUNYU_SHIJIBI))
                    aDb.AddParameter("@MNounyuShijibi", intMdate, DbType.Int32)

                    'トリム納入指示日
                    Dim intTdate As Integer = IIf(IsDBNull(aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI)), 0, aDtGousyaName.Rows(i)(NmDTColGousyaNameList.TD_T_NOUNYU_SHIJIBI))
                    aDb.AddParameter("@TNounyuShijibi", intTdate, DbType.Int32)
                    '
                    aDb.AddParameter("@UpdatedUserId", LoginInfo.Now.UserId, DbType.String)
                    aDb.AddParameter("@UpdatedDate", aDate.CurrentDateDbFormat, DbType.String)
                    aDb.AddParameter("@UpdatedTime", aDate.CurrentTimeDbFormat, DbType.String)

                    Dim resCnt As Integer = aDb.ExecuteNonQuery(SqlTehaichoEdit.GetSqlUpdTehaichoGousya)

                    If resCnt <> 1 Then
                        Throw New Exception("試作手配帳（号車）TBLの更新で問題が発生しました(SQL実行件数異常)")
                        Return False
                    End If

                Next

                Return True

            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("UpdTehaiGousyaの処理中にエラーが発生しました{0}{1}", vbCrLf, ex.Message))
                Return False
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Function

#End Region

#Region "手配情報付加関連"

#Region "SELECT 3ヶ月インフォメーション"
        ''' <summary>
        ''' SELECT 3ヶ月インフォメーション
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <param name="aBuhinNo">部品番号</param>
        ''' <param name="aSeihinKubun">製品区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindKPSM10P(ByVal aDb As SqlAccess, ByVal aBuhinNo As String, ByVal aSeihinKubun As String) As DataTable
            Dim dtResult As New DataTable

            Dim NewBuhinNo As String = aBuhinNo
            If StringUtil.Equals(Left(aBuhinNo, 1), "-") Then
                '-をスペースに置き換える'
                NewBuhinNo = Replace(aBuhinNo, "-", " ")
            End If

            aDb.ClearParameters()
            aDb.AddParameter("@Smkm", aSeihinKubun, DbType.AnsiString)
            aDb.AddParameter("@Buba15", NewBuhinNo, DbType.AnsiString)
            aDb.Fill(SqlTehaichoEdit.GetSqlFindAS_KPSM10P, dtResult)

            Return dtResult

        End Function

#End Region

#Region "SELECT パーツプライスリスト"
        ''' <summary>
        ''' SELECT パーツプライスリスト
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <param name="aBuhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindPARTSP(ByVal aDb As SqlAccess, ByVal aBuhinNo As String) As DataTable
            Dim dtResult As New DataTable

            Dim NewBuhinNo As String
            If aBuhinNo.Length >= 13 Then
                Dim str As String = ""
                If StringUtil.Equals(Left(aBuhinNo, 1), "-") Then
                    str = Replace(aBuhinNo, "-", " ")
                    NewBuhinNo = Left(str, 13)
                Else
                    NewBuhinNo = Left(aBuhinNo, 13)
                End If
            Else
                If StringUtil.Equals(Left(aBuhinNo, 1), "-") Then
                    NewBuhinNo = Replace(aBuhinNo, "-", " ")
                Else
                    NewBuhinNo = aBuhinNo
                End If
            End If


            aDb.ClearParameters()
            aDb.AddParameter("@Buba13", NewBuhinNo, DbType.AnsiString)
            aDb.Fill(SqlTehaichoEdit.GetSqlFindAS_PARTSP, dtResult)

            Return dtResult

        End Function

#End Region

#Region "SELECT 海外生産マスタ"
        ''' <summary>
        ''' SELECT 海外生産マスタ
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <param name="aBuhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindGKPSM10P(ByVal aDb As SqlAccess, ByVal aBuhinNo As String, ByVal aSeihinKubun As String) As DataTable
            Dim dtResult As New DataTable
            Dim NewBuhinNo As String
            If StringUtil.Equals(Left(aBuhinNo, 1), "-") Then
                NewBuhinNo = Replace(aBuhinNo, "-", " ")
            Else
                NewBuhinNo = aBuhinNo
            End If

            aDb.ClearParameters()
            aDb.AddParameter("@Smkm", aSeihinKubun, DbType.AnsiString)
            aDb.AddParameter("@Buba15", NewBuhinNo, DbType.AnsiString)
            aDb.Fill(SqlTehaichoEdit.GetSqlFindAS_GKPSM10P, dtResult)

            Return dtResult

        End Function

#End Region

#Region "SELECT 部品マスタ"
        ''' <summary>
        ''' SELECT 部品マスタ
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <param name="aBuhinNo10">10桁部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindBUHIN01(ByVal aDb As SqlAccess, ByVal aBuhinNo10 As String) As DataTable
            Dim dtResult As New DataTable

            Dim buhinNo10 As String = String.Empty
            If StringUtil.Equals(Left(aBuhinNo10, 1), "-") Then
                buhinNo10 = Replace(aBuhinNo10, "-", " ")
            Else
                buhinNo10 = aBuhinNo10
            End If

            aDb.ClearParameters()
            'ワイルドカードは使わない'
            'aDb.AddParameter("@Gzzcp10", aBuhinNo10 & "%", DbType.AnsiString)
            aDb.AddParameter("@Gzzcp10", buhinNo10, DbType.AnsiString)
            aDb.Fill(SqlTehaichoEdit.GetSqlFindAS_BUHIN01, dtResult)

            Return dtResult

        End Function

#End Region

#Region "SELECT 属性管理(開発符号毎)"
        ''' <summary>
        ''' SELECT 属性管理(開発符号毎)
        ''' </summary>
        ''' <param name="aKoseiDb"></param>
        ''' <param name="aBuhinNo10">10桁部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FindValueDev(ByVal aKoseiDb As SqlAccess, ByVal aKaihatsuFugo As String, ByVal aBuhinNo10 As String) As DataTable
            Dim dtResult As New DataTable

            Dim buhinNo10 As String = String.Empty

            aKoseiDb.ClearParameters()
            '開発符号は使用しない'
            'aKoseiDb.AddParameter("@KaihatsuFugo", aKaihatsuFugo, DbType.AnsiString)
            'ワイルドカードは使わない'
            aKoseiDb.AddParameter("@BuhinNo10", aBuhinNo10, DbType.AnsiString)
            'aKoseiDb.AddParameter("@BuhinNo10", aBuhinNo10 & "%", DbType.AnsiString)
            aKoseiDb.Fill(SqlTehaichoEdit.GetSqlFindValueDev, dtResult)

            Return dtResult

        End Function

#End Region


#End Region

    End Class

End Namespace
