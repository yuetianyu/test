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
Imports EventSakusei.YosanSetteiBuhinEdit
Imports EventSakusei.YosanSetteiBuhinEdit.Logic

Namespace YosanSetteiBuhinEdit.Dao
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class YosanSetteiBuhinEditImpl

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
                .AppendLine(" YOSAN_TEHAI_KIGOU, ")
                .AppendLine(" YOSAN_TSUKURIKATA_SEISAKU, ")
                .AppendLine(" YOSAN_TSUKURIKATA_KATASHIYOU_1, ")
                .AppendLine(" YOSAN_TSUKURIKATA_KATASHIYOU_2, ")
                .AppendLine(" YOSAN_TSUKURIKATA_KATASHIYOU_3, ")
                .AppendLine(" YOSAN_TSUKURIKATA_TIGU, ")
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
                .AppendLine(" HENKATEN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN KIHON WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
                .AppendFormat(" AND YOSAN_LIST_CODE = '{0}' ", aShisakuListCode)
                .AppendLine("ORDER BY ")
                .AppendLine("YOSAN_BLOCK_NO, ")
                .AppendLine("YOSAN_SORT_JUN ")
            End With

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(sql.ToString, dtResult)
            End Using

            Return dtResult

        End Function

        ''' <summary>
        ''' 試作手配帳(基本情報)一覧を返す
        '''         (トランザクション用)
        ''' ※NmTDColBaseにより列名参照
        ''' 
        ''' </summary>
        ''' <returns>該当する一覧</returns>
        ''' <remarks></remarks>
        Public Shared Function FindAllBaseInfo(ByVal aDb As System.Data.SqlClient.SqlConnection, ByVal trans As SqlClient.SqlTransaction, _
                                                       ByVal aShisakuEventCode As String, ByVal aShisakuListCode As String) As DataTable
            Dim dtResult As DataTable = New DataTable
            Dim sql As New System.Text.StringBuilder
            With sql
                .AppendLine("SELECT ")
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
                .AppendLine(" YOSAN_TEHAI_KIGOU, ")
                .AppendLine(" YOSAN_TSUKURIKATA_SEISAKU, ")
                .AppendLine(" YOSAN_TSUKURIKATA_KATASHIYOU_1, ")
                .AppendLine(" YOSAN_TSUKURIKATA_KATASHIYOU_2, ")
                .AppendLine(" YOSAN_TSUKURIKATA_KATASHIYOU_3, ")
                .AppendLine(" YOSAN_TSUKURIKATA_TIGU, ")
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
                .AppendLine(" HENKATEN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN KIHON WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
                .AppendFormat(" AND YOSAN_LIST_CODE = '{0}' ", aShisakuListCode)
                .AppendLine("ORDER BY ")
                .AppendLine("YOSAN_BLOCK_NO, ")
                .AppendLine("YOSAN_SORT_JUN ")
            End With

            Using cmd As System.Data.SqlClient.SqlCommand = aDb.CreateCommand()
                cmd.CommandText = sql.ToString
                cmd.Transaction = trans
                Dim da As New System.Data.SqlClient.SqlDataAdapter
                da.SelectCommand = cmd
                da.Fill(dtResult)

            End Using
            Return dtResult

        End Function

#End Region


#Region "INSERT 予算設定部品表"
        ''' <summary>
        ''' INSERT 予算設定部品表
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function BulkYosanSetteiBuhin(ByVal aDb As SqlClient.SqlConnection, _
                                                ByVal trans As SqlClient.SqlTransaction, _
                                                ByVal vosAddData As List(Of TYosanSetteiBuhinVo)) As Boolean
            If vosAddData.Count = 0 Then Return True

            Try
                Using cmd As SqlClient.SqlCommand = aDb.CreateCommand()
                    Dim sql As New System.Text.StringBuilder
                    With sql
                        .AppendLine("DELETE " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN")
                        .AppendLine(" WHERE SHISAKU_EVENT_CODE = @ShisakuEventCode")
                        .AppendLine(" AND YOSAN_LIST_CODE = @YosanListCode")
                    End With
                    With cmd
                        .Transaction = trans
                        .CommandText = sql.ToString
                        .Parameters.Add("@ShisakuEventCode", SqlDbType.VarChar).Value = vosAddData(0).ShisakuEventCode
                        .Parameters.Add("@YosanListCode", SqlDbType.VarChar).Value = vosAddData(0).YosanListCode

                        .ExecuteNonQuery()
                    End With
                End Using


                Using addData As DataTable = NitteiDbComFunc.ConvListToDataTable(vosAddData)
                    Using bulkCopy As System.Data.SqlClient.SqlBulkCopy = New System.Data.SqlClient.SqlBulkCopy(aDb, System.Data.SqlClient.SqlBulkCopyOptions.KeepIdentity, trans)
                        NitteiDbComFunc.SetColumnMappings(bulkCopy, addData)
                        bulkCopy.BulkCopyTimeout = 0  ' in seconds
                        bulkCopy.DestinationTableName = MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN"
                        bulkCopy.WriteToServer(addData)
                        bulkCopy.Close()
                    End Using
                End Using
            Catch ex As Exception
                ComFunc.ShowErrMsgBox("予算設定部品表登録中にエラーが発生しました。error={0}", ex.Message)
                Return False
            End Try
            Return True

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
                .AppendLine("          ,YOSAN_LIST_CODE ")
                .AppendLine("    FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN WITH (NOLOCK, NOWAIT) ")
                .AppendFormat("    WHERE  SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
                .AppendFormat("       AND YOSAN_LIST_CODE = '{0}' ", aShisakuListCode)
                .AppendLine("    GROUP BY  ")
                .AppendLine("               SHISAKU_EVENT_CODE ")
                .AppendLine("          ,YOSAN_LIST_CODE ")
                .AppendLine("    ) ")
                .AppendLine("    SELECT MAX(YOSAN_GYOU_ID) AS YOSAN_GYOU_ID  ")
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
                .AppendFormat("          BASE.YOSAN_BUKA_CODE = '{0}' ", aShisakuBukaCode)
                .AppendFormat("      AND BASE.YOSAN_BLOCK_NO = '{0}'", aShisakuBlockNo)
                .AppendLine("    GROUP BY ")
                .AppendLine("             BASE.SHISAKU_EVENT_CODE ")
                .AppendLine("            ,BASE.YOSAN_LIST_CODE ")
                .AppendLine("            ,BASE.YOSAN_BUKA_CODE ")
                .AppendLine("            ,BASE.YOSAN_BLOCK_NO ")

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
                .AppendLine("SELECT *")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN KIHON WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat(" SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
                .AppendFormat(" AND YOSAN_LIST_CODE = '{0}' ", aShisakuListCode)
                .AppendLine("ORDER BY ")
                .AppendLine("YOSAN_BLOCK_NO, ")
                .AppendLine("YOSAN_SORT_JUN ")
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
                                                                 ByVal aShisakuBukaCode As String, _
                                                                 ByVal aShisakuBlockNo As String, _
                                                                 ByVal aBuhinNoHyoujiJun As String) As DataTable

            Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder
            With sql
                .AppendLine(" SELECT SHISAKU_EVENT_CODE,YOSAN_LIST_CODE, YOSAN_BLOCK_NO, YOSAN_BUKA_CODE, BUHIN_NO_HYOUJI_JUN ")
                .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN WITH (NOLOCK, NOWAIT) ")
                .AppendLine(" WHERE ")
                .AppendFormat("         SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
                .AppendFormat(" AND YOSAN_LIST_CODE = '{0}'  ", aShisakuListCode)
                .AppendFormat(" AND YOSAN_BLOCK_NO = '{0}' ", aShisakuBlockNo)
                .AppendFormat(" AND YOSAN_BUKA_CODE = '{0}' ", aShisakuBukaCode)
                .AppendFormat(" AND BUHIN_NO_HYOUJI_JUN = {0} ", aBuhinNoHyoujiJun)
            End With
            Dim dtResult As New DataTable
            aDb.Fill(sql.ToString, dtResult)

            Return dtResult

        End Function

        Private Shared _hshFindPkBaseInfo As New Hashtable

        Public Shared Sub FindPkBaseInfoBufClear()
            _hshFindPkBaseInfo.Clear()
        End Sub
        ''' <summary>
        ''' SELECT 試作手配帳(基本情報)の主キー検索(トランザクション用)
        '''         (トランザクション用)
        ''' ※NmTDColBaseにより列名参照
        ''' 
        ''' </summary>
        ''' <returns>該当する一覧</returns>
        ''' <remarks></remarks>
        Public Shared Function FindPkBaseInfo(ByVal aDb As SqlClient.SqlConnection, ByVal trans As SqlClient.SqlTransaction, _
                                                                 ByVal aShisakuEventCode As String, _
                                                                 ByVal aShisakuListCode As String, _
                                                                 ByVal aShisakuBukaCode As String, _
                                                                 ByVal aShisakuBlockNo As String, _
                                                                 ByVal aBuhinNoHyoujiJun As String) As TYosanSetteiBuhinVo

            Dim key As New System.Text.StringBuilder
            If _hshFindPkBaseInfo.Keys.Count = 0 Then
                Dim sql As System.Text.StringBuilder = New System.Text.StringBuilder
                With sql
                    .AppendLine(" SELECT * ")
                    .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_SETTEI_BUHIN WITH (NOLOCK, NOWAIT) ")
                    .AppendLine(" WHERE ")
                    .AppendFormat("  SHISAKU_EVENT_CODE = '{0}' ", aShisakuEventCode)
                    .AppendFormat(" AND YOSAN_LIST_CODE = '{0}'  ", aShisakuListCode)
                End With
                Dim dtResult As New DataTable

                Using cmd As SqlClient.SqlCommand = aDb.CreateCommand()
                    cmd.Transaction = trans
                    cmd.CommandText = sql.ToString
                    Dim da As New SqlClient.SqlDataAdapter
                    da.SelectCommand = cmd
                    da.Fill(dtResult)

                End Using
                Dim vos As List(Of TYosanSetteiBuhinVo) = NitteiDbComFunc.ConvDataTableToList(Of TYosanSetteiBuhinVo)(dtResult)
                For Each vo As TYosanSetteiBuhinVo In vos
                    With key
                        .Remove(0, .Length)
                        .AppendLine(vo.ShisakuEventCode)
                        .AppendLine(vo.YosanListCode)
                        .AppendLine(vo.YosanBukaCode)
                        .AppendLine(vo.YosanBlockNo)
                        .AppendLine(vo.BuhinNoHyoujiJun.ToString)
                    End With
                    _hshFindPkBaseInfo.Add(key.ToString, vo)
                Next

            End If
            With key
                .Remove(0, .Length)
                .AppendLine(aShisakuEventCode)
                .AppendLine(aShisakuListCode)
                .AppendLine(aShisakuBukaCode)
                .AppendLine(aShisakuBlockNo)
                .AppendLine(aBuhinNoHyoujiJun)
            End With

            If _hshFindPkBaseInfo.Contains(key.ToString) Then
                Return _hshFindPkBaseInfo.Item(key.ToString)
            Else
                Return Nothing
            End If

        End Function

#End Region

#Region "DELETE 試作手配帳(基本)"
        ''' <summary>
        ''' DELETE 予算設定部品表
        ''' </summary>
        ''' <param name="aDb"></param>
        ''' <param name="aDataRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DelTehaichoBasebuhinNo(ByVal aDb As SqlClient.SqlConnection, ByVal trans As SqlClient.SqlTransaction, _
                                                      ByVal aDataRow As DataRow) As Boolean

            Dim resultCnt As Integer = -1
            Using cmd As SqlClient.SqlCommand = aDb.CreateCommand()
                With cmd
                    .Transaction = trans
                    .CommandText = SqlYosanSetteiBuhinEdit.GetSqlDelTehaiBaseKey
                    With .Parameters
                        .Add("@ShisakuEventCode", SqlDbType.VarChar).Value = aDataRow(NmDTColBase.TD_SHISAKU_EVENT_CODE)
                        .Add("@YosanListCode", SqlDbType.VarChar).Value = aDataRow(NmDTColBase.TD_YOSAN_LIST_CODE)
                        .Add("@YosanBlockNo", SqlDbType.VarChar).Value = aDataRow(NmDTColBase.TD_YOSAN_BLOCK_NO)
                        .Add("@YosanBukaCode", SqlDbType.VarChar).Value = aDataRow(NmDTColBase.TD_YOSAN_BUKA_CODE)
                        .Add("@BuhinNoHyoujiJun", SqlDbType.VarChar).Value = aDataRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN)
                    End With

                    resultCnt = .ExecuteNonQuery()
                End With

            End Using

            '結果異常の場合は終了
            If resultCnt <> 1 Then
                Dim msg As String = String.Format("DELETE予算設定部品表で削除件数異常 {0} ﾌﾞﾛｯｸNo:{1} 部品番号表示順:{2} ", _
                                                        vbCrLf, aDataRow(NmDTColBase.TD_YOSAN_BLOCK_NO), aDataRow(NmDTColBase.TD_BUHIN_NO_HYOUJI_JUN))
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
        ''' <param name="aShisakuBlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DelTehaichoBaseBlockNo(ByVal aDb As SqlAccess, _
                                                                                            ByVal aShisakuEventCode As String, _
                                                                                            ByVal aShisakuListCode As String, _
                                                                                            ByVal aShisakuBukaCode As String, _
                                                                                            ByVal aShisakuBlockNo As String _
                                                                                            ) As Boolean

            Dim sql As String = SqlYosanSetteiBuhinEdit.GetSqlDelTehaiBaseBlockNo
            Dim resultCnt As Integer = -1

            aDb.ClearParameters()
            aDb.AddParameter("@ShisakuEventCode", aShisakuEventCode, DbType.AnsiString)
            aDb.AddParameter("@YosanListCode", aShisakuListCode, DbType.AnsiString)
            aDb.AddParameter("@YosanBukaCode", aShisakuBukaCode, DbType.AnsiString)
            aDb.AddParameter("@YosanBlockNo", aShisakuBlockNo, DbType.AnsiString)

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
        Public Shared Function UpdShisakuList(ByVal aDb As System.Data.SqlClient.SqlConnection, _
                                                ByVal trans As SqlClient.SqlTransaction, _
                                                ByVal aKoujiNo As String, _
                                                ByVal aShisakuEventCode As String, _
                                                ByVal aShisakuListCode As String) As Boolean
            Dim aDate As New ShisakuDate

            Dim resCnt As Integer
            Using cmd As System.Data.SqlClient.SqlCommand = aDb.CreateCommand()
                With cmd
                    .Transaction = trans
                    .CommandText = SqlYosanSetteiBuhinEdit.GetSqlUpdShisakuList_koujiNo
                    With .Parameters
                        '-更新対象項目
                        .Add("@STATUS", SqlDbType.VarChar).Value = "6A"
                        .Add("@YOSAN_KOUJI_NO", SqlDbType.VarChar).Value = aKoujiNo
                        .Add("@UPDATED_USER_ID", SqlDbType.VarChar).Value = LoginInfo.Now.UserId
                        .Add("@UPDATED_DATE", SqlDbType.VarChar).Value = aDate.CurrentDateDbFormat
                        .Add("@UPDATED_TIME", SqlDbType.VarChar).Value = aDate.CurrentTimeDbFormat
                        '-検索条件項目
                        .Add("@SHISAKU_EVENT_CODE", SqlDbType.VarChar).Value = aShisakuEventCode
                        .Add("@YOSAN_LIST_CODE", SqlDbType.VarChar).Value = aShisakuListCode
                    End With
                    resCnt = .ExecuteNonQuery()
                End With

            End Using

            If resCnt <> 1 Then
                Throw New Exception("予算リストコードTBLの更新で問題が発生しました(SQL実行件数異常)")
            End If

            Return True

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
            aDb.Fill(SqlYosanSetteiBuhinEdit.GetSqlFindAS_KPSM10P, dtResult)

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
            aDb.Fill(SqlYosanSetteiBuhinEdit.GetSqlFindAS_PARTSP, dtResult)

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
            aDb.Fill(SqlYosanSetteiBuhinEdit.GetSqlFindAS_GKPSM10P, dtResult)

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
            aDb.Fill(SqlYosanSetteiBuhinEdit.GetSqlFindAS_BUHIN01, dtResult)

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
            aKoseiDb.Fill(SqlYosanSetteiBuhinEdit.GetSqlFindValueDev, dtResult)

            Return dtResult

        End Function

#End Region


#End Region

    End Class

End Namespace
