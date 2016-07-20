Imports EBom.Common
Imports ShisakuCommon.Db.Kosei.Vo.Helper

''' <summary>
''' データを取得するSQL
''' </summary>
''' <remarks></remarks>
Public Class DataSqlCommon

#Region " 製作イベント取得SQL "
    ''' <summary>
    ''' 製作イベントを取得するSQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMSeisakuEventSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    SHISAKU_EVENT AS VALUE, ")
            .AppendLine("    SHISAKU_EVENT AS DISPLAY")
            .AppendLine("FROM ")
            .AppendLine("    M_SEISAKU_EVENT ")
            .AppendLine("ORDER BY ")
            .AppendLine("    SEISAKU_EVENT_CODE ")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " 開発符号取得SQL "
    ''↓↓2014/07/23 Ⅰ.3.設計編集 ベース車改修専用化 (TES)張 ADD BEGIN
    ' Public Function GetDevSignSql() As String
    ''' <summary>
    ''' 開発符号を取得するSQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDevSignSql() As String
        ''↑↑2014/07/23 Ⅰ.3.設計編集 ベース車改修専用化 (TES)張 ADD END
        Dim sql As New System.Text.StringBuilder()
        '開発符号は開発符号表示設定（KAIHATSUFG_PLN）が0以外のデータが対象となる。        '　宮原さんより      
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    KAIHATSU_FUGO AS VALUE, ")
            .AppendLine("    KAIHATSU_FUGO AS DISPLAY ")
            .AppendLine("FROM ")
            .AppendLine("    " + RHACLIBF_DB_NAME + ".dbo.RHAC0020 ")
            .AppendLine("WHERE ")
            .AppendLine("    KAIHATSUFG_PLN <> '0' ")
            '.AppendLine("    KAIHATSUFG_PLN = '' ")
            .AppendLine("GROUP BY ")
            .AppendLine("    KAIHATSU_FUGO ")
            .AppendLine("ORDER BY ")
            .AppendLine("    SUBSTRING(KAIHATSU_FUGO, 1, 1) ASC, ")
            .AppendLine("    SUBSTRING(KAIHATSU_FUGO, 2, 1) DESC ")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " 開発符号-仕様情報№取得SQL "
    ''' <summary>
    ''' 開発符号指定後、仕様情報№を取得するSQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_RHAC0030_SpecInfoNo_Sql() As String
        Dim sql As New System.Text.StringBuilder()

        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    KAIHATSU_FUGO , ")
            .AppendLine("    SHIYOSHO_SEQNO AS VALUE, ")
            .AppendLine("    SHIYOSHO_SEQNO AS DISPLAY ")
            .AppendLine("FROM ")
            .AppendLine("    RHAC0030 ")
            .AppendLine("WHERE ")
            .AppendLine("    KAIHATSU_FUGO    = @KAIHATSU_FUGO ")
            .AppendLine("GROUP BY ")
            .AppendLine("    KAIHATSU_FUGO, ")
            .AppendLine("    SHIYOSHO_SEQNO ")
            .AppendLine("ORDER BY ")
            .AppendLine("    SUBSTRING(SHIYOSHO_SEQNO, 1, 1) ASC, ")
            .AppendLine("    SUBSTRING(SHIYOSHO_SEQNO, 2, 3) DESC ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 開発符号-仕様一連№-アプライド№取得SQL "
    ''' <summary>
    ''' 開発符号、仕様一連№指定後、アプライド№を取得するSQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_RHAC0230_ApuraidoNo_Sql() As String
        Dim sql As New System.Text.StringBuilder()

        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    RHAC0230.APPLIED_NO AS VALUE, ")
            .AppendLine("    RHAC0230.APPLIED_NO AS DISPLAY ")
            .AppendLine("FROM ")
            .AppendLine("    RHAC0230,RHAC0030 ")
            .AppendLine("WHERE ")
            .AppendLine("    RHAC0030.KAIHATSU_FUGO    = RHAC0230.KAIHATSU_FUGO AND ")
            .AppendLine("    RHAC0030.SOBI_KAITEI_NO   = RHAC0230.SOBI_KAITEI_NO AND ")
            .AppendLine("    RHAC0030.KAIHATSU_FUGO    = @KAIHATSU_FUGO AND ")
            .AppendLine("    SHIYOSHO_SEQNO            = @SHIYOSHO_SEQNO ")
            .AppendLine("GROUP BY ")
            .AppendLine("    RHAC0230.APPLIED_NO ")
            .AppendLine("ORDER BY ")
            .AppendLine("    RHAC0230.APPLIED_NO ASC ")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " 7桁型式符号取得SQL "
    ''' <summary>
    ''' 7桁型式符号を取得するSQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_RHAC0230_7ketaKatashikiFugo_Sql() As String
        Dim sql As New System.Text.StringBuilder()

        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    RHAC0230.KATASHIKI_FUGO_7 AS VALUE, ")
            .AppendLine("    RHAC0230.KATASHIKI_FUGO_7 AS DISPLAY ")
            .AppendLine("FROM ")
            .AppendLine("    RHAC0230,RHAC0030 ")
            .AppendLine("WHERE ")
            .AppendLine("    RHAC0030.KAIHATSU_FUGO    = RHAC0230.KAIHATSU_FUGO AND ")
            .AppendLine("    RHAC0030.SOBI_KAITEI_NO   = RHAC0230.SOBI_KAITEI_NO AND ")
            .AppendLine("    RHAC0030.KAIHATSU_FUGO    = @KAIHATSU_FUGO AND ")
            .AppendLine("    SHIYOSHO_SEQNO            = @SHIYOSHO_SEQNO AND")
            .AppendLine("    APPLIED_NO                = @APPLIED_NO ")
            .AppendLine("GROUP BY ")
            .AppendLine("    RHAC0230.KATASHIKI_FUGO_7 ")
            .AppendLine("ORDER BY ")
            .AppendLine("    RHAC0230.KATASHIKI_FUGO_7 ASC ")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " 仕向取得SQL "
    ''' <summary>
    ''' 仕向を取得するSQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_RHAC0540_Shimuke_Sql() As String
        Dim sql As New System.Text.StringBuilder()

        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    RHAC0540.SHIMUKECHI_CODE AS VALUE, ")
            .AppendLine("    RHAC0540.SHIMUKECHI_CODE AS DISPLAY ")
            .AppendLine("FROM ")
            .AppendLine("    RHAC0230,RHAC0540 ")
            .AppendLine("WHERE ")
            .AppendLine("    RHAC0540.KAIHATSU_FUGO    = RHAC0230.KAIHATSU_FUGO AND ")
            .AppendLine("    RHAC0540.SOBI_KAITEI_NO   = RHAC0230.SOBI_KAITEI_NO AND ")
            .AppendLine("    RHAC0540.KATASHIKI_SCD_7  = RHAC0230.KATASHIKI_SCD_7  AND ")
            .AppendLine("    RHAC0230.APPLIED_NO       = @APPLIED_NO AND ")
            .AppendLine("    RHAC0230.KATASHIKI_FUGO_7 = @KATASHIKI_FUGO_7 AND ")
            .AppendLine("    RHAC0540.KAIHATSU_FUGO    = @KAIHATSU_FUGO ")
            .AppendLine("GROUP BY ")
            .AppendLine("    RHAC0540.SHIMUKECHI_CODE ")
            .AppendLine("ORDER BY ")
            .AppendLine("    RHAC0540.SHIMUKECHI_CODE ASC ")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " OP取得SQL "
    ''' <summary>
    ''' OPを取得するSQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_RHAC0540_OP_Sql() As String
        Dim sql As New System.Text.StringBuilder()

        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    RHAC0540.OP_CODE AS VALUE, ")
            .AppendLine("    RHAC0540.OP_CODE AS DISPLAY ")
            .AppendLine("FROM ")
            .AppendLine("    RHAC0230,RHAC0540 ")
            .AppendLine("WHERE ")
            .AppendLine("    RHAC0540.KAIHATSU_FUGO    = RHAC0230.KAIHATSU_FUGO AND ")
            .AppendLine("    RHAC0540.SOBI_KAITEI_NO   = RHAC0230.SOBI_KAITEI_NO AND ")
            .AppendLine("    RHAC0540.KATASHIKI_SCD_7  = RHAC0230.KATASHIKI_SCD_7  AND ")
            .AppendLine("    RHAC0230.APPLIED_NO       = @APPLIED_NO AND ")
            .AppendLine("    RHAC0230.KATASHIKI_FUGO_7 = @KATASHIKI_FUGO_7 AND ")
            .AppendLine("    RHAC0540.KAIHATSU_FUGO    = @KAIHATSU_FUGO AND")
            .AppendLine("    RHAC0540.SHIMUKECHI_CODE  = @SHIMUKECHI_CODE ")
            .AppendLine("GROUP BY ")
            .AppendLine("    RHAC0540.OP_CODE ")
            .AppendLine("ORDER BY ")
            .AppendLine("    RHAC0540.OP_CODE ASC ")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " 外装色取得SQL "
    ''' <summary>
    ''' 外装色を取得するSQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_RHAC1630_GaisouSyoku_Sql() As String
        Dim sql As New System.Text.StringBuilder()

        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    RHAC1630.COLOR_CODE AS VALUE, ")
            .AppendLine("    RHAC1630.COLOR_CODE AS DISPLAY ")
            .AppendLine("FROM ")
            .AppendLine("    RHAC0230,RHAC0540,RHAC1630,RHAC0430 ")
            .AppendLine("WHERE ")
            .AppendLine("    RHAC0540.KAIHATSU_FUGO    = RHAC0230.KAIHATSU_FUGO AND ")
            .AppendLine("    RHAC0540.SOBI_KAITEI_NO   = RHAC0230.SOBI_KAITEI_NO AND ")
            .AppendLine("    RHAC0540.KATASHIKI_SCD_7  = RHAC0230.KATASHIKI_SCD_7  AND ")

            .AppendLine("    RHAC0540.KAIHATSU_FUGO    = RHAC1630.KAIHATSU_FUGO AND ")
            .AppendLine("    RHAC0540.SOBI_KAITEI_NO   = RHAC1630.SOBI_KAITEI_NO AND ")
            .AppendLine("    RHAC0540.KATASHIKI_SCD_7  = RHAC1630.KATASHIKI_SCD_7  AND ")
            .AppendLine("    RHAC0540.SHIMUKECHI_CODE  = RHAC1630.SHIMUKECHI_CODE  AND ")
            .AppendLine("    RHAC0540.COL_NO           = RHAC1630.COL_NO  AND ")
            .AppendLine("    RHAC0430.COLOR_CODE       = RHAC1630.COLOR_CODE  AND ")
            .AppendLine("    RHAC0430.NAIGAISO_KBN     = '1' AND ")

            .AppendLine("    RHAC0230.APPLIED_NO       = @APPLIED_NO AND ")
            .AppendLine("    RHAC0230.KATASHIKI_FUGO_7 = @KATASHIKI_FUGO_7 AND ")
            .AppendLine("    RHAC0540.KAIHATSU_FUGO    = @KAIHATSU_FUGO AND")
            .AppendLine("    RHAC0540.SHIMUKECHI_CODE  = @SHIMUKECHI_CODE AND")

            .AppendLine("    RHAC0540.OP_CODE  = @OP_CODE ")

            .AppendLine("GROUP BY ")
            .AppendLine("    RHAC1630.COLOR_CODE ")
            .AppendLine("ORDER BY ")
            .AppendLine("    RHAC1630.COLOR_CODE ASC ")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " 内装色取得SQL "
    ''' <summary>
    ''' 内装色を取得するSQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_RHAC1630_NaisouSyoku_Sql() As String
        Dim sql As New System.Text.StringBuilder()

        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    RHAC1630.COLOR_CODE AS VALUE, ")
            .AppendLine("    RHAC1630.COLOR_CODE AS DISPLAY ")
            .AppendLine("FROM ")
            .AppendLine("    RHAC0230,RHAC0540,RHAC1630,RHAC0430 ")
            .AppendLine("WHERE ")
            .AppendLine("    RHAC0540.KAIHATSU_FUGO    = RHAC0230.KAIHATSU_FUGO AND ")
            .AppendLine("    RHAC0540.SOBI_KAITEI_NO   = RHAC0230.SOBI_KAITEI_NO AND ")
            .AppendLine("    RHAC0540.KATASHIKI_SCD_7  = RHAC0230.KATASHIKI_SCD_7  AND ")

            .AppendLine("    RHAC0540.KAIHATSU_FUGO    = RHAC1630.KAIHATSU_FUGO AND ")
            .AppendLine("    RHAC0540.SOBI_KAITEI_NO   = RHAC1630.SOBI_KAITEI_NO AND ")
            .AppendLine("    RHAC0540.KATASHIKI_SCD_7  = RHAC1630.KATASHIKI_SCD_7  AND ")
            .AppendLine("    RHAC0540.SHIMUKECHI_CODE  = RHAC1630.SHIMUKECHI_CODE  AND ")
            .AppendLine("    RHAC0540.COL_NO           = RHAC1630.COL_NO  AND ")
            .AppendLine("    RHAC0430.COLOR_CODE       = RHAC1630.COLOR_CODE  AND ")
            .AppendLine("    RHAC0430.NAIGAISO_KBN     = '0' AND ")

            .AppendLine("    RHAC0230.APPLIED_NO       = @APPLIED_NO AND ")
            .AppendLine("    RHAC0230.KATASHIKI_FUGO_7 = @KATASHIKI_FUGO_7 AND ")
            .AppendLine("    RHAC0540.KAIHATSU_FUGO    = @KAIHATSU_FUGO AND")
            .AppendLine("    RHAC0540.SHIMUKECHI_CODE  = @SHIMUKECHI_CODE AND")

            .AppendLine("    RHAC0540.OP_CODE  = @OP_CODE ")

            .AppendLine("GROUP BY ")
            .AppendLine("    RHAC1630.COLOR_CODE ")
            .AppendLine("ORDER BY ")
            .AppendLine("    RHAC1630.COLOR_CODE ASC ")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " 部品番号取得SQL "
    ''' <summary>
    ''' 部品番号を取得するSQL RHAC0530
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBuhinSql() As String
        '取得条件は後で書く。       
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    BUHIN_NO AS VALUE, ")
            .AppendLine("    BUHIN_NO AS DISPLAY ")
            .AppendLine("FROM ")
            .AppendLine("    RHAC0530 ")
            .AppendLine("GROUP BY ")
            .AppendLine("    BUHIN_NO")
            .AppendLine("ORDER BY ")
            .AppendLine("    BUHIN_NO ASC ")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " ログインマスター "
    ''' <summary>
    ''' パスウードを取得するSQL M_SHISAKU_LOGIN
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetLoginSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    SEKKEI_SHAIN_NO, ")
            .AppendLine("    PASSWORD, ")
            .AppendLine("    SHISAKU_MENU_SETTEI, ")
            .AppendLine("    KENGEN_SETTEI ")
            .AppendLine("FROM ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN ")
            .AppendLine("WHERE ")
            .AppendLine("    SEKKEI_SHAIN_NO = @SEKKEI_SHAIN_NO")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " 指定ユーザーIDのユーザー取得SQL "
    ''' <summary>
    ''' ユーザー取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetUserMasSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	USER_ID, ")
            .AppendLine("	SITE_INFO, ")
            .AppendLine("	USER_FLG, ")
            .AppendLine("   COMPETENT ")
            .AppendLine("FROM ")
            .AppendLine("	" + EBOM_DB_NAME + ".dbo.M_USER ")
            .AppendLine("WHERE ")
            .AppendLine("	USER_ID = @USER_ID ")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " ユーザーマスタより　ユーザーID取得(コンボボックス用) "
    ''' <summary>
    ''' ユーザー取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllUserMasSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	USER_ID ")
            .AppendLine("FROM ")
            .AppendLine("	" + EBOM_DB_NAME + ".dbo.M_USER ")
            .AppendLine(" ORDER BY USER_ID ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 権限(ﾕｰｻﾞｰ別)取得SQL "
    Public Shared Function Get_AUTHORITY_USER_Sql(ByVal isMenu As Boolean) As String
        Dim databaseName As String = ComFunc.GetDatabaseName(g_kanrihyoIni(ShisakuGlobal.DB_KEY_EBOM))
        Dim sql As New System.Text.StringBuilder()

        '2011/07/03 柳沼緊急対応
        ' 　　　　　権限情報を取得に行く際、APP_NO=1000の条件が必要なので追加。
        ' 　　　　　固定で直したが後ほど変数に変える。

        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine(" DISTINCT	" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER.APP_NO, ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER.KINO_ID_1, ")
            .AppendLine("	KENGEN1.SHISAKU_PROGRAM_NAME_1 AS NAME1, ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER.KINO_ID_2, ")
            .AppendLine("	KENGEN2.SHISAKU_KINO_NAME_1 AS NAME2, ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER.KINO_ID_3, ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER.AUTHORITY_KBN  ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER ")
            .AppendFormat(" LEFT JOIN  " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KENGEN AS KENGEN1", databaseName)
            .AppendLine(" ON KENGEN1.SHISAKU_PROGRAM_ID_1=" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER.KINO_ID_1")
            .AppendFormat(" LEFT JOIN  " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KENGEN AS KENGEN2", databaseName)
            .AppendLine(" ON KENGEN2.SHISAKU_KINO_ID_1=" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER.KINO_ID_2")
            .AppendLine("    AND KENGEN2.SHISAKU_PROGRAM_ID_1<>'NITTEI'")
            .AppendLine("WHERE ")
            .AppendLine("	  " + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER.USER_ID = @USER_ID ")
            .AppendLine(" AND " + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER.APP_NO = '1000' ")
            If isMenu Then
                .AppendLine(" AND	" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER.KINO_ID_1 = 'MENU' ")
            Else
                .AppendLine(" AND	" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER.KINO_ID_1 <> 'MENU' ")
            End If
        End With

        Return sql.ToString()
    End Function

#End Region

#Region " 権限取得SQL "
    Public Shared Function GetMAuthoritySql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT * ")
            .AppendLine("FROM ")
            .AppendLine("	" + EBOM_DB_NAME + ".dbo.M_AUTHORITY ")
            .AppendLine("LEFT JOIN " + EBOM_DB_NAME + ".dbo.M_USER ON  " + EBOM_DB_NAME + ".dbo.M_AUTHORITY.user_kbn=" + EBOM_DB_NAME + ".dbo.M_USER.competent")
            .AppendLine("WHERE ")
            .AppendLine("	USER_ID = @USER_ID AND AUTHORITY_KBN=@AUTHORITY_KBN AND MENU_DAI_KBN=@MENU_DAI_KBN")
        End With

        Return sql.ToString()
    End Function

#End Region

#Region " 権限(ﾕｰｻﾞｰ別)取得SQL "
    Public Shared Function GetMAuthorityUserSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	USER_ID, ")
            .AppendLine("	APP_NO, ")
            .AppendLine("	KINO_ID_1, ")
            .AppendLine("	KINO_ID_2, ")
            .AppendLine("	KINO_ID_3, ")
            .AppendLine("	AUTHORITY_KBN ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER ")
            .AppendLine("WHERE ")
            .AppendLine("	USER_ID = @USER_ID ")
            .AppendLine("	AND KINO_ID_1 = @KINO_ID_1 ")
        End With

        Return sql.ToString()
    End Function

#End Region

#Region " 権限(ﾕｰｻﾞｰ別)車系/開発符号マスター権限取得SQL "
    Public Shared Function GetKurumaKengenSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	APP_NO, ")
            .AppendLine("	KINO_ID_1, ")
            .AppendLine("	KINO_ID_2, ")
            .AppendLine("	KINO_ID_3, ")
            .AppendLine("	AUTHORITY_KBN ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER ")
            .AppendLine("WHERE ")
            .AppendLine("	USER_ID = @USER_ID ")
            .AppendLine("	AND ")
            .AppendLine("	APP_NO = @APP_NO ")
            .AppendLine("	AND ")
            .AppendLine("	KINO_ID_1 = @KINO_ID_1 ")
            .AppendLine("	AND ")
            .AppendLine("	KINO_ID_2 = @KINO_ID_2 ")
            .AppendLine("	AND ")
            .AppendLine("	AUTHORITY_KBN = @AUTHORITY_KBN ")
        End With

        Return sql.ToString()
    End Function

#End Region
#Region " 社員外部インターフェース取得SQL "
    ''' <summary>
    ''' 社員外部インターフェースを取得するSQL RHAC0650
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Get_RHAC0650_Shain_Sql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    SHAIN_NO, ")
            .AppendLine("    BUKA_CODE, ")
            .AppendLine("    SITE_KBN, ")
            .AppendLine("    SHAIN_NAME, ")
            .AppendLine("    SHAIN_NAME_EIJI, ")
            .AppendLine("    NAISEN_NO ")
            .AppendLine("FROM ")
            .AppendLine("    RHAC0650 ")
            .AppendLine("WHERE ")
            .AppendLine("    SHAIN_NO = @SHAIN_NO")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 社員マスター情報取得SQL "
    ''' <summary>
    ''' 社員マスター情報を取得するSQL RHAC2130
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Get_RHAC2130_Shain_Sql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    SHAIN_NO, ")
            .AppendLine("    BUKA_CODE, ")
            .AppendLine("    SITE_KBN, ")
            .AppendLine("    SHAIN_NAME, ")
            .AppendLine("    SHAIN_NAME_EIJI, ")
            .AppendLine("    NAISEN_NO ")
            .AppendLine("FROM ")
            .AppendLine("    RHAC2130 ")
            .AppendLine("WHERE ")
            .AppendLine("    SHAIN_NO = @SHAIN_NO")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "社員外部インターフェース(RHAC0650)、社員マスター情報(RHAC2130)より社員名SQL (コンボボックス用)"
    ''' <summary>
    ''' 社員外部インターフェース(RHAC0650)、社員マスター情報(RHAC2130)より社員名を取得するSQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Get_Shain_Sql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("   SHAIN.SHAIN_NO, ")
            .AppendLine("   SHAIN.SHAIN_NAME, ")
            .AppendLine("   SHAIN.SHAIN_NAME_EIJI ")
            .AppendLine("FROM ")
            .AppendFormat(" ({0}) AS SHAIN," + EBOM_DB_NAME + ".dbo.M_USER ", Get_All_Syain_Sql())
            .AppendLine("WHERE  " + EBOM_DB_NAME + ".dbo.M_USER.USER_ID = SHAIN.SHAIN_NO ")
            .AppendLine(" ORDER BY SHAIN_NAME")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 組織取得SQL "
    ''' <summary>
    ''' 組織を取得するSQL RHAC0660
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSosikiSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    BUKA_CODE, ")
            .AppendLine("    SITE_KBN, ")
            .AppendLine("    BUKA_NAME ")
            .AppendLine("FROM ")
            .AppendLine("    RHAC0660 ")
            .AppendLine("WHERE ")
            .AppendLine("    BUKA_CODE = @BUKA_CODE")
            .AppendLine("    AND SITE_KBN=@SITE_KBN")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 社員外部インターフェース、社員マスター情報より部課取得SQL(コンボボックス用) "
    ''' <summary>
    ''' 社員外部インターフェース、社員マスター情報より部課を取得するSQL RHAC0660
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBukaSql() As String
        Dim databaseName As String = ComFunc.GetDatabaseName(g_kanrihyoIni(ShisakuGlobal.DB_KEY_EBOM))
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("  DISTINCT  KA.KA_RYAKU_NAME AS BUKA ")
            .AppendFormat("FROM RHAC1560 AS KA,({0}) AS SHAIN ", Get_All_Syain_Sql)
            .AppendLine("WHERE ")
            .AppendLine("    LEFT(SHAIN.BUKA_CODE,2) = KA.BU_CODE AND RIGHT(SHAIN.BUKA_CODE,2)=KA.KA_CODE AND SHAIN.SITE_KBN=KA.SITE_KBN ")
            .AppendLine("ORDER BY KA_RYAKU_NAME")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 車系/開発符号取得SQL "
    ''' <summary>
    ''' 車系/開発符号を取得するSQL M_SHISAKU_KAIHATUFUGO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKaihatufugouListSql(ByVal strShakei As String, ByVal strFugo As String, ByVal strPhase As String, ByVal strName As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    '0' FLAG, ")
            .AppendLine("    SHISAKU_SHAKEI_CODE, ")
            .AppendLine("    SHISAKU_KAIHATSU_FUGO, ")
            .AppendLine("    SHISAKU_EVENT_PHASE, ")
            .AppendLine("    SHISAKU_EVENT_PHASE_NAME, ")
            .AppendLine("    HYOJIJUN_NO ")
            .AppendLine("FROM ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KAIHATUFUGO ")
            .AppendLine("WHERE 1=1 ")
            If Not strShakei = String.Empty Then
                .AppendFormat("   AND SHISAKU_SHAKEI_CODE LIKE '{0}%'", strShakei)
            End If
            If Not strFugo = String.Empty Then
                .AppendFormat("   AND SHISAKU_KAIHATSU_FUGO LIKE '{0}%'", strFugo)
            End If
            If Not strPhase = String.Empty Then
                .AppendFormat("   AND SHISAKU_EVENT_PHASE LIKE '{0}%'", strPhase)
            End If
            If Not strName = String.Empty Then
                .AppendFormat("   AND SHISAKU_EVENT_PHASE_NAME LIKE '{0}%'", strName)
            End If
            .AppendLine("ORDER BY ")
            .AppendLine("    SHISAKU_SHAKEI_CODE, ")
            .AppendLine("    SHISAKU_KAIHATSU_FUGO, ")
            .AppendLine("    SHISAKU_EVENT_PHASE, ")
            .AppendLine("    SHISAKU_EVENT_PHASE_NAME ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 車系/開発符号EXCEL取得SQL "
    ''' <summary>
    ''' 車系/開発符号EXCELを取得するSQL M_SHISAKU_KAIHATUFUGO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKaihatufugouExcelSql(ByVal strShakei As String, ByVal strFugo As String, ByVal strPhase As String, ByVal strName As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    SHISAKU_SHAKEI_CODE, ")
            .AppendLine("    SHISAKU_KAIHATSU_FUGO, ")
            .AppendLine("    SHISAKU_EVENT_PHASE, ")
            .AppendLine("    SHISAKU_EVENT_PHASE_NAME ")
            .AppendLine("FROM ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KAIHATUFUGO ")
            .AppendLine("WHERE 1=1 ")
            If Not strShakei = String.Empty Then
                .AppendFormat("   AND SHISAKU_SHAKEI_CODE LIKE '{0}%'", strShakei)
            End If
            If Not strFugo = String.Empty Then
                .AppendFormat("   AND SHISAKU_KAIHATSU_FUGO LIKE '{0}%'", strFugo)
            End If
            If Not strPhase = String.Empty Then
                .AppendFormat("   AND SHISAKU_EVENT_PHASE LIKE '{0}%'", strPhase)
            End If
            If Not strName = String.Empty Then
                .AppendFormat("   AND SHISAKU_EVENT_PHASE_NAME LIKE '{0}%'", strName)
            End If
            .AppendLine("ORDER BY ")
            .AppendLine("    SHISAKU_SHAKEI_CODE, ")
            .AppendLine("    SHISAKU_KAIHATSU_FUGO, ")
            .AppendLine("    SHISAKU_EVENT_PHASE, ")
            .AppendLine("    SHISAKU_EVENT_PHASE_NAME ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 試作イベントチェック取得SQL "
    ''' <summary>
    ''' 試作イベントチェックするSQL T_SHISAKU_EVENT
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIbentoCheckSql(ByVal strShakei As String, ByVal strFugo As String, ByVal strNo As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    SHISAKU_EVENT_CODE ")
            .AppendLine("FROM ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT ")
            .AppendLine("WHERE 1=1 ")
            If Not strShakei = String.Empty Then
                .AppendLine("   AND SHISAKU_SHAKEI_CODE = @SHISAKU_SHAKEI_CODE")
            End If
            If Not strFugo = String.Empty Then
                .AppendLine("   AND SHISAKU_KAIHATSU_FUGO = @SHISAKU_KAIHATSU_FUGO")
            End If
            If Not strNo = String.Empty Then
                .AppendLine("   AND HYOJIJUN_NO = @HYOJIJUN_NO")
            End If
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 車系/開発符号COMBOBOX取得SQL "
    ''' <summary>
    ''' 車系/開発符号COMBOBOXを取得するSQL M_SHISAKU_KAIHATUFUGO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKaihatufugouComboSql(ByVal strField As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            Select Case strField
                Case "Shakei"
                    .AppendLine("    SHISAKU_SHAKEI_CODE Shakei ")
                Case "Fugo"
                    .AppendLine("    SHISAKU_KAIHATSU_FUGO Fugo ")
                Case "Phase"
                    .AppendLine("    SHISAKU_EVENT_PHASE Phase ")
                Case "Name"
                    .AppendLine("    SHISAKU_EVENT_PHASE_NAME Name ")
            End Select
            .AppendLine("FROM ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KAIHATUFUGO ")
            .AppendLine("GROUP BY ")
            Select Case strField
                Case "Shakei"
                    .AppendLine("    SHISAKU_SHAKEI_CODE ")
                    .AppendLine("ORDER BY ")
                    .AppendLine("    SHISAKU_SHAKEI_CODE ")
                Case "Fugo"
                    .AppendLine("    SHISAKU_KAIHATSU_FUGO ")
                    .AppendLine("ORDER BY ")
                    .AppendLine("    SHISAKU_KAIHATSU_FUGO ")
                Case "Phase"
                    .AppendLine("    SHISAKU_EVENT_PHASE ")
                    .AppendLine("ORDER BY ")
                    .AppendLine("    SHISAKU_EVENT_PHASE ")
                Case "Name"
                    .AppendLine("    SHISAKU_EVENT_PHASE_NAME ")
                    .AppendLine("ORDER BY ")
                    .AppendLine("    SHISAKU_EVENT_PHASE_NAME ")
            End Select

        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 車系/開発符号マスタ更新画面データ取得SQL "
    ''' <summary>
    ''' 車系/開発符号マスタ更新画面データ取得するSQL M_SHISAKU_KAIHATUFUGO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKaihatufugouDisplaySql(ByVal strShakei As String, ByVal strFugo As String, ByVal strFlag As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    HYOJIJUN_NO, ")
            .AppendLine("    SHISAKU_EVENT_PHASE, ")
            .AppendLine("    SHISAKU_EVENT_PHASE_NAME ")
            If strFlag = "Update" Then
                .AppendLine("    ,'' SHISAKU_EVENT_PHASE2, ")
                .AppendLine("    '' SHISAKU_EVENT_PHASE_NAME2 ")
            End If
            .AppendLine("FROM ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KAIHATUFUGO ")
            .AppendLine("WHERE 1=1 ")
            .AppendLine("   AND SHISAKU_SHAKEI_CODE = @SHISAKU_SHAKEI_CODE")
            .AppendLine("   AND SHISAKU_KAIHATSU_FUGO = @SHISAKU_KAIHATSU_FUGO")
            .AppendLine("ORDER BY ")
            .AppendLine("    SHISAKU_EVENT_PHASE, ")
            .AppendLine("    SHISAKU_EVENT_PHASE_NAME ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 車系/開発符号マスタ削除SQL "
    ''' <summary>
    ''' 車系/開発符号マスタ削除するSQL M_SHISAKU_KAIHATUFUGO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKaihatufugouDeleteSql(ByVal strShakei As String, ByVal strFugo As String, ByVal strHYOJIJUN_NO As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("DELETE FROM ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KAIHATUFUGO ")
            .AppendLine("WHERE ")
            .AppendLine("   SHISAKU_SHAKEI_CODE = @SHISAKU_SHAKEI_CODE ")
            .AppendLine("   AND SHISAKU_KAIHATSU_FUGO = @SHISAKU_KAIHATSU_FUGO ")
            .AppendLine("   AND HYOJIJUN_NO = @HYOJIJUN_NO ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 車系/開発符号マスタ登録SQL "
    ''' <summary>
    ''' 車系/開発符号マスタ登録するSQL M_SHISAKU_KAIHATUFUGO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKaihatufugouInsertSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("INSERT INTO ")
            .AppendLine(" " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KAIHATUFUGO( ")
            .AppendLine("    SHISAKU_SHAKEI_CODE, ")
            .AppendLine("    SHISAKU_KAIHATSU_FUGO, ")
            .AppendLine("    HYOJIJUN_NO, ")
            .AppendLine("    SHISAKU_EVENT_PHASE, ")
            .AppendLine("    SHISAKU_EVENT_PHASE_NAME, ")
            .AppendLine("    CREATED_USER_ID, ")
            .AppendLine("    CREATED_DATE, ")
            .AppendLine("    CREATED_TIME, ")
            .AppendLine("    UPDATED_USER_ID, ")
            .AppendLine("    UPDATED_DATE, ")
            .AppendLine("    UPDATED_TIME) ")
            .AppendLine(" VALUES( ")
            .AppendLine("    @SHISAKU_SHAKEI_CODE, ")
            .AppendLine("    @SHISAKU_KAIHATSU_FUGO, ")
            .AppendLine("    @HYOJIJUN_NO, ")
            .AppendLine("    @SHISAKU_EVENT_PHASE, ")
            .AppendLine("    @SHISAKU_EVENT_PHASE_NAME, ")
            .AppendLine("    @CREATED_USER_ID, ")
            .AppendLine("    @CREATED_DATE, ")
            .AppendLine("    @CREATED_TIME, ")
            .AppendLine("    @UPDATED_USER_ID, ")
            .AppendLine("    @UPDATED_DATE, ")
            .AppendLine("    @UPDATED_TIME) ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 車系/開発符号マスタ更新SQL "
    ''' <summary>
    ''' 車系/開発符号マスタ更新するSQL M_SHISAKU_KAIHATUFUGO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKaihatufugouUpdateSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("UPDATE ")
            .AppendLine(" " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KAIHATUFUGO SET ")
            .AppendLine("    SHISAKU_EVENT_PHASE=@SHISAKU_EVENT_PHASE, ")
            .AppendLine("    SHISAKU_EVENT_PHASE_NAME=@SHISAKU_EVENT_PHASE_NAME, ")
            .AppendLine("    UPDATED_USER_ID=@UPDATED_USER_ID, ")
            .AppendLine("    UPDATED_DATE=@UPDATED_DATE, ")
            .AppendLine("    UPDATED_TIME=@UPDATED_TIME ")
            .AppendLine(" WHERE ")
            .AppendLine("    SHISAKU_SHAKEI_CODE=@SHISAKU_SHAKEI_CODE ")
            .AppendLine("    AND SHISAKU_KAIHATSU_FUGO=@SHISAKU_KAIHATSU_FUGO ")
            .AppendLine("    AND HYOJIJUN_NO=@HYOJIJUN_NO ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 車系/開発符号マスタ連番取得SQL "
    ''' <summary>
    ''' 車系/開発符号マスタ連番取得するSQL M_SHISAKU_KAIHATUFUGO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKaihatufugouGetNoSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT MAX(HYOJIJUN_NO) HYOJIJUN_NO  ")
            .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KAIHATUFUGO ")
            .AppendLine(" WHERE ")
            .AppendLine("    SHISAKU_SHAKEI_CODE=@SHISAKU_SHAKEI_CODE ")
            .AppendLine("    AND SHISAKU_KAIHATSU_FUGO=@SHISAKU_KAIHATSU_FUGO ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 車系/開発符号マスタ存在チェック取得SQL "
    ''' <summary>
    ''' 車系/開発符号マスタ存在チェックするSQL M_SHISAKU_KAIHATUFUGO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKaihatufugouCheckSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT  ")
            .AppendLine("  SHISAKU_SHAKEI_CODE  ")
            .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KAIHATUFUGO ")
            .AppendLine(" WHERE ")
            .AppendLine("    SHISAKU_SHAKEI_CODE=@SHISAKU_SHAKEI_CODE ")
            .AppendLine("    AND SHISAKU_KAIHATSU_FUGO=@SHISAKU_KAIHATSU_FUGO ")
            .AppendLine("    AND SHISAKU_EVENT_PHASE_NAME=@SHISAKU_EVENT_PHASE_NAME ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 車系/開発符号マスタダブル存在チェック取得SQL "
    ''' <summary>
    ''' 車系/開発符号マスタダブル存在チェックするSQL M_SHISAKU_KAIHATUFUGO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKaihatufugouDoubleCheckSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT  ")
            .AppendLine("  SHISAKU_SHAKEI_CODE  ")
            .AppendLine("  FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KAIHATUFUGO ")
            .AppendLine(" WHERE ")
            .AppendLine("    SHISAKU_SHAKEI_CODE=@SHISAKU_SHAKEI_CODE ")
            .AppendLine("    AND SHISAKU_KAIHATSU_FUGO=@SHISAKU_KAIHATSU_FUGO ")
            .AppendLine("    AND SHISAKU_EVENT_PHASE=@SHISAKU_EVENT_PHASE ")
            .AppendLine("    AND SHISAKU_EVENT_PHASE_NAME=@SHISAKU_EVENT_PHASE_NAME ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " ログインマスタ取得SQL "
    Public Shared Function GetLoginMastUpdateInitlSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN.SEKKEI_SHAIN_NO, ")
            .AppendLine("    BUKA.SHAIN_NAME, ")
            .AppendLine("    BUKA.BUKA_NAME, ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN.PASSWORD, ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN.USER_KBN, ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN.SHISAKU_MENU_SETTEI, ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN.KENGEN_SETTEI ")

            .AppendLine("FROM ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN ")

            .AppendLine("LEFT JOIN (")
            .AppendLine("   SELECT ")
            .AppendLine("       RHAC0650.SHAIN_NO, ")
            .AppendLine("       RHAC0650.SHAIN_NAME, ")
            .AppendLine("       RHAC0650.BUKA_CODE, ")
            .AppendLine("       RHAC0650.SITE_KBN, ")
            .AppendLine("       RHAC0660.BUKA_NAME ")
            .AppendLine("   FROM ")
            .AppendLine("       RHAC0650 ")
            .AppendLine("   LEFT JOIN ")
            .AppendLine("       RHAC0660 ")
            .AppendLine("   ON ")
            .AppendLine("       RHAC0650.BUKA_CODE=RHAC0660.BUKA_CODE ")
            .AppendLine("   AND ")
            .AppendLine("      RHAC0650.SITE_KBN=RHAC0660.SITE_KBN ")
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       RHAC2130.SHAIN_NO, ")
            .AppendLine("       RHAC2130.SHAIN_NAME, ")
            .AppendLine("       RHAC2130.BUKA_CODE, ")
            .AppendLine("       RHAC2130.SITE_KBN, ")
            .AppendLine("       RHAC0660.BUKA_NAME ")
            .AppendLine("   FROM ")
            .AppendLine("       RHAC2130 ")
            .AppendLine("   LEFT JOIN ")
            .AppendLine("       RHAC0660 ")
            .AppendLine("   ON ")
            .AppendLine("       RHAC2130.BUKA_CODE=RHAC0660.BUKA_CODE ")
            .AppendLine("   AND ")
            .AppendLine("      RHAC2130.SITE_KBN=RHAC0660.SITE_KBN ")
            .AppendLine(") BUKA")
            .AppendLine("   ON ")
            .AppendLine("       " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN.SEKKEI_SHAIN_NO=BUKA.SHAIN_NO ")

            .AppendLine("WHERE ")
            .AppendLine("    SEKKEI_SHAIN_NO = @SEKKEI_SHAIN_NO")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " ログインマスタ更新SQL "
    Public Shared Function GetLoginMastUpdateSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("UPDATE ")
            .AppendLine("   " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN ")
            .AppendLine("SET ")
            .AppendLine("   PASSWORD=@PASSWORD, ")
            .AppendLine("   SHISAKU_MENU_SETTEI=@SHISAKU_MENU_SETTEI, ")
            .AppendLine("   KENGEN_SETTEI=@KENGEN_SETTEI, ")
            .AppendLine("   UPDATED_USER_ID=@UPDATED_USER_ID, ")
            .AppendLine("   UPDATED_DATE=@UPDATED_DATE, ")
            .AppendLine("   UPDATED_TIME=@UPDATED_TIME ")
            .AppendLine("WHERE ")
            .AppendLine("    SEKKEI_SHAIN_NO = @SEKKEI_SHAIN_NO")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " ログインマスタ追加SQL "
    Public Shared Function GetLoginMastInsertSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("INSERT INTO ")
            .AppendLine("   " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN ")
            .AppendLine("VALUES( ")
            .AppendLine("   @SEKKEI_SHAIN_NO, ")
            .AppendLine("   @PASSWORD, ")
            .AppendLine("   @USER_KBN, ")
            .AppendLine("   @SHISAKU_MENU_SETTEI, ")
            .AppendLine("   @KENGEN_SETTEI, ")
            .AppendLine("   @CREATED_USER_ID, ")
            .AppendLine("   @CREATED_DATE, ")
            .AppendLine("   @CREATED_TIME, ")
            .AppendLine("   @UPDATED_USER_ID, ")
            .AppendLine("   @UPDATED_DATE, ")
            .AppendLine("   @UPDATED_TIME ")
            .AppendLine(") ")

        End With
        Return sql.ToString()
    End Function
#End Region

#Region " ユーザーマスタより サイト取得SQL(コンボボックス用) "
    ''' <summary>
    ''' ユーザーマスタより サイト取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllUserMasSiteSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine(" DISTINCT SITE_INFO ")
            .AppendLine("FROM ")
            .AppendLine(" " + EBOM_DB_NAME + ".dbo.M_USER ")
            .AppendLine(" ORDER BY SITE_INFO")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " ユーザーマスタより 区分取得SQL(コンボボックス用) "
    ''' <summary>
    ''' ユーザーマスタより 区分取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllUserMasCompetentSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("   DISTINCT COMPETENT ")
            .AppendLine("FROM ")
            .AppendLine("   " + EBOM_DB_NAME + ".dbo.M_USER ")
            .AppendLine(" ORDER BY COMPETENT ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " LoginマスタSpreadDisplay取得SQL "
    ''' <summary>
    ''' LoginマスタSpreadDisplay取得するSQL RHAC0660
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetLoginMasDisplaySql(ByVal strUserId As String, ByVal strUserName As String, ByVal strUserDepart As String, ByVal strSite As String) As String
        Dim databaseName As String = ComFunc.GetDatabaseName(g_kanrihyoIni(ShisakuGlobal.DB_KEY_EBOM))
        Dim sql As New System.Text.StringBuilder()

        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    a.USER_ID, ")
            .AppendLine("    newtb.SHAIN_NAME, ")
            .AppendLine("    b.KA_RYAKU_NAME, ")
            .AppendLine("    a.SITE_INFO, ")
            .AppendLine("    (case when mas.SHISAKU_MENU_SETTEI='0' then 'なし' when mas.SHISAKU_MENU_SETTEI='1' then 'あり' else '' end) as SHISAKU_MENU_SETTEI,  ")
            .AppendLine("    (case when mas.KENGEN_SETTEI='0' then 'なし'  when mas.KENGEN_SETTEI='1' then 'あり' else '' end) as KENGEN_SETTEI,  ")
            .AppendLine("    a.COMPETENT ")
            .AppendLine("FROM ")
            .AppendLine("    " + EBOM_DB_NAME + ".dbo.M_USER a ")

            .AppendLine("LEFT JOIN ")
            .AppendFormat("({0}) as newtb ", Get_All_Syain_Sql())
            .AppendLine("ON ")
            .AppendLine("    a.user_id = newtb.SHAIN_NO ")

            .AppendFormat("LEFT JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN mas ", databaseName)
            .AppendLine("ON ")
            .AppendLine("    a.USER_ID = mas.SEKKEI_SHAIN_NO ")

            .AppendFormat("LEFT JOIN {0}.DBO.RHAC1560 b ", databaseName)
            .AppendLine("ON ")
            .AppendLine("    LEFT(newtb.BUKA_CODE,2) = b.BU_CODE  ")
            .AppendLine("AND  ")
            .AppendLine("    RIGHT(newtb.BUKA_CODE,2)=b.KA_CODE  ")
            .AppendLine("AND ")
            .AppendLine("    newtb.SITE_KBN=b.SITE_KBN ")
            .AppendLine("    WHERE 1=1 ")
            If Not strUserId = String.Empty Then
                .AppendLine("   AND a.USER_ID LIKE '" + strUserId + "%' ")
            End If
            If Not strUserName = String.Empty Then
                .AppendLine("   AND newtb.SHAIN_NAME LIKE '" + strUserName + "%' ")
            End If
            If Not strUserDepart = String.Empty Then
                .AppendLine("   AND b.KA_RYAKU_NAME LIKE '" + strUserDepart + "%' ")
            End If
            If Not strSite = String.Empty Then
                .AppendLine("   AND a.SITE_INFO LIKE '" + strSite + "%' ")
            End If
            .AppendLine(" ORDER BY a.USER_ID")

        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 権限(ﾕｰｻﾞｰ別)削除SQL "
    Public Shared Function DelAuthorityUserByIdSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("DELETE ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER ")
            .AppendLine("WHERE ")
            .AppendLine("	  APP_NO    = '1000' ")
            .AppendLine(" AND USER_ID   = @USER_ID ")
            .AppendLine(" AND KINO_ID_1 = @KINO_ID_1 ")
        End With
        Return sql.ToString()
    End Function
    Public Shared Function DelAuthorityUserByNotMenuSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("DELETE ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER ")
            .AppendLine("WHERE ")
            .AppendLine("	  APP_NO    = '1000' ")
            .AppendLine(" AND USER_ID   = @USER_ID ")
            .AppendLine(" AND KINO_ID_1 <> @KINO_ID_1 ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 権限(ﾕｰｻﾞｰ別)登録SQL "
    Public Shared Function InsAuthorityUserMenuSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("INSERT INTO " + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER")
            .AppendLine("	(APP_NO, ")
            .AppendLine("	KINO_ID_1, ")
            .AppendLine("	KINO_ID_2, ")
            .AppendLine("	KINO_ID_3, ")
            .AppendLine("	USER_ID, ")
            .AppendLine("	AUTHORITY_KBN, ")
            .AppendLine("   CREATED_USER_ID, ")
            .AppendLine("   CREATED_DATE, ")
            .AppendLine("   CREATED_TIME, ")
            .AppendLine("   UPDATED_USER_ID, ")
            .AppendLine("   UPDATED_DATE, ")
            .AppendLine("   UPDATED_TIME) ")
            .AppendLine("VALUES( ")
            .AppendLine("	'1000', ")
            .AppendLine("	@KINO_ID_1, ")
            .AppendLine("	@KINO_ID_2, ")
            .AppendLine("	'', ")
            .AppendLine("	@USER_ID, ")
            .AppendLine("	@AUTHORITY_KBN, ")
            .AppendLine("   @CREATED_USER_ID, ")
            .AppendLine("   @CREATED_DATE, ")
            .AppendLine("   @CREATED_TIME, ")
            .AppendLine("   @UPDATED_USER_ID, ")
            .AppendLine("   @UPDATED_DATE, ")
            .AppendLine("   @UPDATED_TIME ")
            .AppendLine(") ")

        End With
        Return sql.ToString()
    End Function
    Public Shared Function InsAuthorityUserSql(Optional ByVal strBukaCode As String = "", Optional ByVal strNitteiKanri As String = "") As String
        Dim databaseName As String = ComFunc.GetDatabaseName(g_kanrihyoIni(ShisakuGlobal.DB_KEY_EBOM))
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("INSERT INTO " + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER")
            .AppendLine("	(APP_NO, ")
            .AppendLine("	KINO_ID_1, ")
            .AppendLine("	KINO_ID_2, ")
            .AppendLine("	KINO_ID_3, ")
            .AppendLine("	USER_ID, ")
            .AppendLine("	AUTHORITY_KBN, ")
            .AppendLine("   CREATED_USER_ID, ")
            .AppendLine("   CREATED_DATE, ")
            .AppendLine("   CREATED_TIME, ")
            .AppendLine("   UPDATED_USER_ID, ")
            .AppendLine("   UPDATED_DATE, ")
            .AppendLine("   UPDATED_TIME) ")
            .AppendLine("SELECT  ")
            .AppendLine("	'1000', ")
            .AppendFormat("	(SELECT DISTINCT SHISAKU_PROGRAM_ID_1 FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KENGEN WHERE SHISAKU_PROGRAM_NAME_1=@NAME1), ", databaseName)
            '2014/02/05 日程管理ツールチェックを追加
            If strNitteiKanri = MAuthorityUserVoHelper.KinoId1.NITTEI Then
                .AppendFormat("	(SELECT DISTINCT SHISAKU_KINO_ID_2 FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KENGEN WHERE SHISAKU_KINO_NAME_1=@NAME2), ", databaseName)
            Else
                .AppendFormat("	(SELECT DISTINCT SHISAKU_KINO_ID_1 FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KENGEN WHERE SHISAKU_KINO_NAME_1=@NAME2), ", databaseName)
            End If
            If strBukaCode = "" Then
                .AppendLine("	'', ")
            Else
                .AppendLine("	@BUKA_CODE, ")
            End If
            .AppendLine("	@USER_ID, ")
            .AppendLine("	@AUTHORITY_KBN, ")
            .AppendLine("   @CREATED_USER_ID, ")
            .AppendLine("   @CREATED_DATE, ")
            .AppendLine("   @CREATED_TIME, ")
            .AppendLine("   @UPDATED_USER_ID, ")
            .AppendLine("   @UPDATED_DATE, ")
            .AppendLine("   @UPDATED_TIME ")

        End With
        Return sql.ToString()
    End Function
#End Region

#Region "　試作システム権限マスターで メニュー機能を取得する "
    ''' <summary>
    ''' 試作システム権限マスターで メニュー機能を取得する。M_SHISAKU_KENGEN
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Get_PROGRAM_MENU_Sql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    SHISAKU_PROGRAM_NAME_1 ")
            .AppendLine("FROM ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KENGEN ")
            .AppendLine("WHERE ")
            .AppendLine("    SHISAKU_PROGRAM_ID_1 = 'MENU'")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "　試作システム権限マスターで メニュー以外の機能を取得する "
    ''' <summary>
    ''' 試作システム権限マスターで メニュー以外の機能を取得する。M_SHISAKU_KENGEN
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Get_PROGRAM_Sql(ByVal menuName As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("   DISTINCT SHISAKU_PROGRAM_NAME_1 ")
            .AppendLine("FROM ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KENGEN ")
            .AppendLine("WHERE ")
            .AppendLine("    SHISAKU_PROGRAM_ID_1 <> 'MENU'")
            If menuName.Equals(PROGRAM_NAME_MENU(1)) Then
                .AppendFormat("  AND  SHISAKU_PROGRAM_ID_1 <> '{0}'", PROGRAM_ID(5))
            End If
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "　権限マスター機能ID01名を取得 "
    ''' <summary>
    ''' 権限マスター機能ID01名を取得。M_SHISAKU_KENGEN
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Get_KINO_Sql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    DISTINCT SHISAKU_KINO_NAME_1 ")
            .AppendLine("FROM ")
            .AppendLine("    " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KENGEN ")
            .AppendLine("WHERE ")
            .AppendLine("    SHISAKU_KINO_ID_1 IN ('APPROVAL','OPEN','EDIT')")
            .AppendLine("    AND SHISAKU_PROGRAM_NAME_1=@SHISAKU_PROGRAM_NAME_1")
            .AppendLine("ORDER BY SHISAKU_KINO_NAME_1")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 全部社員を取得する。RHAC0650、RHAC2130 "
    Public Shared Function Get_All_Syain_Sql() As String
        Dim databaseName As String = ComFunc.GetDatabaseName(g_kanrihyoIni(ShisakuGlobal.DB_KEY_EBOM))
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    A.SHAIN_NO, ")
            .AppendLine("    COALESCE( A.BUKA_CODE, B.BUKA_CODE) BUKA_CODE, ")
            .AppendLine("    COALESCE( A.SITE_KBN, B.SITE_KBN) SITE_KBN, ")
            .AppendLine("    COALESCE( A.SHAIN_NAME, B.SHAIN_NAME) SHAIN_NAME, ")
            .AppendLine("    COALESCE( A.SHAIN_NAME_EIJI, B.SHAIN_NAME_EIJI) SHAIN_NAME_EIJI, ")
            .AppendLine("    COALESCE( A.NAISEN_NO, B.NAISEN_NO) NAISEN_NO ")
            .AppendLine("FROM ")
            .AppendFormat("    {0}.DBO.RHAC0650 A ", databaseName)
            .AppendFormat("    LEFT JOIN {0}.DBO.RHAC2130 B ", databaseName)
            .AppendLine("      ON A.SHAIN_NO = B.SHAIN_NO ")
            .AppendLine("UNION ")
            .AppendLine("SELECT ")
            .AppendLine("    SHAIN_NO, ")
            .AppendLine("    BUKA_CODE, ")
            .AppendLine("    SITE_KBN, ")
            .AppendLine("    SHAIN_NAME, ")
            .AppendLine("    SHAIN_NAME_EIJI, ")
            .AppendLine("    NAISEN_NO ")
            .AppendLine("FROM ")
            .AppendFormat("    {0}.DBO.RHAC2130 C ", databaseName)
            .AppendLine("WHERE ")
            .AppendFormat("    NOT EXISTS (SELECT * FROM {0}.DBO.RHAC0650 WHERE SHAIN_NO = C.SHAIN_NO) ", databaseName)

        End With
        Return sql.ToString()

    End Function


#End Region

#Region "　指定の社員を取得 "
    ''' <summary>
    ''' 指定の社員を取得。    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Get_ShainByNo_Sql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    SHAIN.SHAIN_NO, ")
            .AppendLine("    SHAIN.BUKA_CODE, ")
            .AppendLine("    SHAIN.SITE_KBN, ")
            .AppendLine("    SHAIN.SHAIN_NAME, ")
            .AppendLine("    SHAIN.SHAIN_NAME_EIJI, ")
            .AppendLine("    SHAIN.NAISEN_NO ")
            .AppendFormat("FROM ({0}) AS SHAIN  ", Get_All_Syain_Sql)
            .AppendLine("WHERE ")
            .AppendLine("    SHAIN.SHAIN_NO=@SHAIN_NO ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "　部課略称を取得 "
    ''' <summary>
    ''' 部課略称を取得。    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Get_KaRyakuName_Sql(ByVal bukaCd As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    BU_CODE, ")
            .AppendLine("    SITE_KBN, ")
            .AppendLine("    KA_CODE, ")
            .AppendLine("    KA_NAME, ")
            .AppendLine("    KA_RYAKU_NAME ")
            .AppendLine("FROM RHAC1560 ")
            If Not bukaCd = String.Empty Then
                .AppendLine("WHERE ")
                .AppendLine("    BU_CODE=@BU_CODE ")
                .AppendLine("    AND SITE_KBN=@SITE_KBN ")
                .AppendLine("    AND KA_CODE=@KA_CODE ")
            End If
            .AppendLine("ORDER BY KA_RYAKU_NAME")
        End With
        Return sql.ToString()
    End Function
#End Region


#Region "IntからYYYY/MM/DD転化"
    Public Shared Function IntToDateFormatSql(ByVal item As String) As String
        Dim strRet As New System.Text.StringBuilder()
        With strRet
            .AppendFormat(" (SUBSTRING(LTRIM(STR({0})),1,4) + '/' + ", item)
            .AppendFormat(" SUBSTRING(LTRIM(STR({0})),5,2) + '/' + ", item)
            .AppendFormat(" SUBSTRING(LTRIM(STR({0})),7,2)) ", item)
        End With
        Return strRet.ToString
    End Function
#End Region


#Region "試作部品編集一覧35 と 試作部品編集改訂一覧36"
    ''' <summary>
    ''' 試作部品編集一覧35 と 試作部品編集改訂一覧36 一覧データを読む
    ''' </summary>
    ''' <param name="strMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBuhinEditDisplaySql(ByVal strMode As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("   t_Event.SHISAKU_EVENT_CODE ")
            .AppendLine("   ,t_Event.SHISAKU_KAIHATSU_FUGO ")
            .AppendLine("   ,t_Event.SHISAKU_EVENT_PHASE_NAME ")
            .AppendLine("   ,t_Event.UNIT_KBN ")
            .AppendLine("   ,t_Event.SHISAKU_EVENT_NAME ")

            .AppendLine("   ,ISNULL(LTRIM(STR(t_Event.SEISAKUDAISU_KANSEISYA)),0)+ '+' + ")
            .AppendLine("    ISNULL(LTRIM(STR(t_Event.SEISAKUDAISU_WB)),0)AS SHISAKU_DAISU ")

            If strMode = ShishakuHensyuMode Then
                .AppendLine("   ,SUBSTRING(LTRIM(STR(t_Event.KAITEI_SYOCHI_SHIMEKIRIBI)),1,4) + '/' + ")
                .AppendLine("    SUBSTRING(LTRIM(STR(t_Event.KAITEI_SYOCHI_SHIMEKIRIBI)),5,2) + '/' + ")
                .AppendLine("    SUBSTRING(LTRIM(STR(t_Event.KAITEI_SYOCHI_SHIMEKIRIBI)),7,2) AS KAITEI_SYOCHI_SHIMEKIRIBI ")

                .AppendLine("   ,DATEDIFF(DAY,GETDATE(),SUBSTRING(LTRIM(STR(t_Event.KAITEI_SYOCHI_SHIMEKIRIBI)),1,4) + '-' + ")
                .AppendLine("                 SUBSTRING(LTRIM(STR(t_Event.KAITEI_SYOCHI_SHIMEKIRIBI)),5,2) + '-' + ")
                .AppendLine("                 SUBSTRING(LTRIM(STR(t_Event.KAITEI_SYOCHI_SHIMEKIRIBI)),7,2)) AS DIFF_DATE ")
            ElseIf strMode = ShishakuKaiteiHensyuMode Then  '*****完了閲覧モード対応*****
                .AppendLine("   ,status.SHISAKU_STATUS_NAME + '(' +  ")
                .AppendLine("    SUBSTRING(LTRIM(STR(t_Event.SHIMEKIRIBI)),1,4) + '/' + ")
                .AppendLine("    SUBSTRING(LTRIM(STR(t_Event.SHIMEKIRIBI)),5,2) + '/' + ")
                .AppendLine("    SUBSTRING(LTRIM(STR(t_Event.SHIMEKIRIBI)),7,2) + '迄)' AS STATUS ")

                .AppendLine("   ,DATEDIFF(DAY,GETDATE(),SUBSTRING(LTRIM(STR(t_Event.SHIMEKIRIBI)),1,4) + '-' + ")
                .AppendLine("                 SUBSTRING(LTRIM(STR(t_Event.SHIMEKIRIBI)),5,2) + '-' + ")
                .AppendLine("                 SUBSTRING(LTRIM(STR(t_Event.SHIMEKIRIBI)),7,2)) AS DIFF_DATE ")
            Else
                .AppendLine("   ,status.SHISAKU_STATUS_NAME + '(' +  ")
                .AppendLine("    SUBSTRING(LTRIM(STR(t_Event.SHIMEKIRIBI)),1,4) + '/' + ")
                .AppendLine("    SUBSTRING(LTRIM(STR(t_Event.SHIMEKIRIBI)),5,2) + '/' + ")
                .AppendLine("    SUBSTRING(LTRIM(STR(t_Event.SHIMEKIRIBI)),7,2) + '迄)' AS STATUS ")

                .AppendLine("   ,DATEDIFF(DAY,GETDATE(),SUBSTRING(LTRIM(STR(t_Event.SHIMEKIRIBI)),1,4) + '-' + ")
                .AppendLine("                 SUBSTRING(LTRIM(STR(t_Event.SHIMEKIRIBI)),5,2) + '-' + ")
                .AppendLine("                 SUBSTRING(LTRIM(STR(t_Event.SHIMEKIRIBI)),7,2)) AS DIFF_DATE ")
            End If

            .AppendLine("FROM ")
            .AppendLine("   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT t_Event ")
            .AppendLine(" LEFT JOIN ")
            .AppendLine("   " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS statusDATA_KBN ")
            .AppendLine("     ON t_Event.DATA_KBN = statusDATA_KBN.SHISAKU_STATUS_CODE ")
            .AppendLine(" LEFT JOIN ")
            .AppendLine("   " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS status ")
            .AppendLine("     ON t_Event.STATUS = status.SHISAKU_STATUS_CODE ")
            If strMode = ShishakuHensyuMode Then
                .AppendLine("WHERE ")
                .AppendLine("   t_Event.STATUS='21' ")
            ElseIf strMode = ShishakuKaiteiHensyuMode Then  '*****完了閲覧モード対応*****
                .AppendLine("WHERE ")
                .AppendLine("   t_Event.STATUS='23' ")
            Else  '*****完了閲覧モード対応*****
                .AppendLine("WHERE ")
                .AppendLine("   t_Event.STATUS='24' ")
            End If
            '2012/03/07 新たに設けた展開ステータスを見て設計展開が済んでいない
            'イベントは表示させない。
            .AppendLine("   AND t_Event.TENKAI_STATUS=1 ")
            .AppendLine("ORDER BY ")
            .AppendLine("   t_Event.SHISAKU_EVENT_CODE, ")
            .AppendLine("   t_Event.SHISAKU_KAIHATSU_FUGO ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "試作部品作成メニューExcel出力（ヘッダー）"
    Public Shared Function GetShsakuSekkeiBlockHeader() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("   KAITEI_SYOCHI_SHIMEKIRIBI, ")
            .AppendLine("   COALESCE(sousuu,0) as sousuu, ")
            .AppendLine("   COALESCE(kanryou,0) as kanryou")
            .AppendLine("FROM  ")
            .AppendLine("   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT E")
            .AppendLine("LEFT JOIN  ")
            .AppendLine("   ( ")
            .AppendLine("       SELECT")
            .AppendLine("           SHISAKU_EVENT_CODE, ")
            .AppendLine("           COUNT(SHISAKU_EVENT_CODE) AS sousuu ")
            .AppendLine("       FROM")
            .AppendLine("           " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK")
            .AppendLine("       GROUP BY ")
            .AppendLine("           SHISAKU_EVENT_CODE")
            .AppendLine("   ) B1 ")
            .AppendLine("ON  ")
            .AppendLine("   B1.SHISAKU_EVENT_CODE=E.SHISAKU_EVENT_CODE")
            .AppendLine("LEFT JOIN  ")
            .AppendLine("   ( ")
            .AppendLine("       SELECT")
            .AppendLine("           SHISAKU_EVENT_CODE, ")
            .AppendLine("           COUNT(SHISAKU_EVENT_CODE) AS kanryou ")
            .AppendLine("       FROM")
            .AppendLine("           " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK")
            .AppendLine("       WHERE")
            .AppendLine("           KACHOU_SYOUNIN_JYOUTAI=@KACHOU_SYOUNIN_JYOUTAI")
            .AppendLine("       GROUP BY ")
            .AppendLine("           SHISAKU_EVENT_CODE")
            .AppendLine("   ) B2 ")
            .AppendLine("ON  ")
            .AppendLine("   B2.SHISAKU_EVENT_CODE=E.SHISAKU_EVENT_CODE")
            .AppendLine("WHERE")
            .AppendLine("   E.SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "試作部品作成メニューExcel出力（集計）"
    Public Shared Function GetShsakuSekkeiBlockCount() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("   SHISAKU_BUKA_CODE, ")
            .AppendLine("   COALESCE(( ")
            .AppendLine("       select COUNT(SHISAKU_BUKA_CODE) ")
            .AppendLine("       from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("       where SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE ")
            .AppendLine("       and BLOCK_FUYOU=@BLOCK_FUYOU ")
            .AppendLine("       and SHISAKU_BUKA_CODE=b.SHISAKU_BUKA_CODE ")
            .AppendLine("       group by SHISAKU_BUKA_CODE ")
            .AppendLine("   ),0) as totalBlock,")
            .AppendLine("   COALESCE(( ")
            .AppendLine("       select COUNT(SHISAKU_BUKA_CODE) ")
            .AppendLine("       from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("       where SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE ")
            .AppendLine("       and JYOUTAI=@JYOUTAI ")
            .AppendLine("       and BLOCK_FUYOU=@BLOCK_FUYOU ")
            .AppendLine("       and SHISAKU_BUKA_CODE=b.SHISAKU_BUKA_CODE ")
            .AppendLine("       group by SHISAKU_BUKA_CODE ")
            .AppendLine("   ),0) as totalJyouTai,")
            .AppendLine("   COALESCE(( ")
            .AppendLine("       select COUNT(SHISAKU_BUKA_CODE) ")
            .AppendLine("       from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("       where SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE ")
            .AppendLine("       and TANTO_SYOUNIN_JYOUTAI=@TANTO_SYOUNIN_JYOUTAI ")
            .AppendLine("       and BLOCK_FUYOU=@BLOCK_FUYOU ")
            .AppendLine("       and SHISAKU_BUKA_CODE=b.SHISAKU_BUKA_CODE ")
            .AppendLine("       group by SHISAKU_BUKA_CODE ")
            .AppendLine("   ),0) as totalSyouNinJyouTai,")
            .AppendLine("   COALESCE(( ")
            .AppendLine("       select COUNT(SHISAKU_BUKA_CODE) ")
            .AppendLine("       from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("       where SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE ")
            .AppendLine("       and KACHOU_SYOUNIN_JYOUTAI=@KACHOU_SYOUNIN_JYOUTAI ")
            .AppendLine("       and BLOCK_FUYOU=@BLOCK_FUYOU ")
            .AppendLine("       and SHISAKU_BUKA_CODE=b.SHISAKU_BUKA_CODE ")
            .AppendLine("       group by SHISAKU_BUKA_CODE ")
            .AppendLine("   ),0) as totalKaChouSyouNinJyouTai ")
            .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK b ")
            .AppendLine("GROUP BY SHISAKU_BUKA_CODE")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "試作部品作成メニューExcel出力（明細）"
    Public Shared Function GetShsakuSekkeiBlockMeisai() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("   COALESCE(SHISAKU_BUKA_CODE,'') AS SHISAKU_BUKA_CODE , ")
            .AppendLine("   COALESCE(SHISAKU_BLOCK_NO,'') AS SHISAKU_BLOCK_NO , ")
            .AppendLine("   COALESCE(SHISAKU_BLOCK_NO_KAITEI_NO,'') AS SHISAKU_BLOCK_NO_KAITEI_NO , ")
            .AppendLine("   COALESCE(BLOCK_FUYOU,'') AS BLOCK_FUYOU , ")
            .AppendLine("   COALESCE(JYOUTAI,'') AS JYOUTAI, ")
            .AppendLine("   COALESCE(UNIT_KBN,'') AS UNIT_KBN , ")
            .AppendLine("   COALESCE(SHISAKU_BLOCK_NAME,'') AS BLOCK_NAME , ")
            .AppendLine("   COALESCE(syain.SHAIN_NAME ,'') AS SYAINNAME , ")
            .AppendLine("   COALESCE(TEL_NO,'') AS TEL_NO , ")
            .AppendLine("   COALESCE(SAISYU_KOUSHINBI,'') AS SAISYU_KOUSHINBI, ")
            .AppendLine("   COALESCE(SAISYU_KOUSHINJIKAN,'') AS SAISYU_KOUSHINJIKAN, ")
            .AppendLine("   COALESCE(TANTO_SYOUNIN_JYOUTAI,'') AS TANTO_SYOUNIN_JYOUTAI, ")
            .AppendLine("   COALESCE(TANTO_SYOUNIN_KA,'') AS TANTO_SYOUNIN_KA, ")
            .AppendLine("   COALESCE(tanto.SHAIN_NAME,'') AS TANTONAME, ")
            .AppendLine("   COALESCE(TANTO_SYOUNIN_HI,'') AS TANTO_SYOUNIN_HI, ")
            .AppendLine("   COALESCE(KACHOU_SYOUNIN_JYOUTAI,'') AS KACHOU_SYOUNIN_JYOUTAI, ")
            .AppendLine("   COALESCE(KACHOU_SYOUNIN_KA,'') AS KACHOU_SYOUNIN_KA, ")
            .AppendLine("   COALESCE(kachou.SHAIN_NAME,'') AS KACHOUNAME, ")
            .AppendLine("   COALESCE(KACHOU_SYOUNIN_HI,'') AS KACHOU_SYOUNIN_HI ")
            .AppendLine("FROM  ")
            .AppendLine("   " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK block")
            .AppendLine("LEFT JOIN  ")
            .AppendLine("   (" + Get_All_Syain_Sql() + ") syain")
            .AppendLine("ON  ")
            .AppendLine("   block.USER_ID=syain.SHAIN_NO")
            .AppendLine("LEFT JOIN  ")
            .AppendLine("   (" + Get_All_Syain_Sql() + ") tanto")
            .AppendLine("ON  ")
            .AppendLine("   block.TANTO_SYOUNIN_SYA=tanto.SHAIN_NO")
            .AppendLine("LEFT JOIN  ")
            .AppendLine("   (" + Get_All_Syain_Sql() + ") kachou")
            .AppendLine("ON  ")
            .AppendLine("   block.KACHOU_SYOUNIN_SYA=kachou.SHAIN_NO")
            .AppendLine("WHERE ")
            .AppendLine("   SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE ")
            .AppendLine("ORDER BY  ")
            .AppendLine("   SHISAKU_BUKA_CODE,SHISAKU_BLOCK_NO")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "試作部品表編集・改定編集（設計）全体状況取得する"
    Public Shared Function GetZentaiJyoutai() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("select  ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE) as souburoku, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and JYOUTAI=@SHOUCHIKANRYOU and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE) as syochi_kan, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and (JYOUTAI<>@SHOUCHIKANRYOU or JYOUTAI is null ) and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE) as nokori_kan, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and TANTO_SYOUNIN_JYOUTAI=@SHOUNIN1 and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE) as syochi_shounin1, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and (TANTO_SYOUNIN_JYOUTAI<>@SHOUNIN1 or TANTO_SYOUNIN_JYOUTAI is null )  and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE) as nokori_shounin1, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and KACHOU_SYOUNIN_JYOUTAI=@SHOUNIN2 and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE) as syochi_shounin2,")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and (KACHOU_SYOUNIN_JYOUTAI<>@SHOUNIN2 or KACHOU_SYOUNIN_JYOUTAI is null ) and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE) as nokori_shounin2 ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "試作部品表編集・改定編集（設計）イベント情報を取得する"
    Public Shared Function GetIbento(ByVal strMode As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    SHISAKU_KAIHATSU_FUGO, ")
            .AppendLine("    SHISAKU_EVENT_NAME, ")
            .AppendLine("    isnull(LAST_KAITEI_CHUSYUTUBI,TEHAICHOU_SAKUSEIBI) as SKEIDATE, ")
            If strMode = ShishakuHensyuMode Then
                .AppendLine("    KAITEI_SYOCHI_SHIMEKIRIBI, ")
                .AppendLine("    DATEDIFF(day,getdate(),substring(ltrim(str(KAITEI_SYOCHI_SHIMEKIRIBI)),1,4) + '-' +  ")
                .AppendLine("    substring(ltrim(str(KAITEI_SYOCHI_SHIMEKIRIBI)),5,2) + '-' +   ")
                .AppendLine("    substring(ltrim(str(KAITEI_SYOCHI_SHIMEKIRIBI)),7,2)) AS DIFF_DATE   ")
            End If
            If strMode = ShishakuKaiteiHensyuMode Then
                .AppendLine("    SHIMEKIRIBI, ")
                .AppendLine("    DATEDIFF(day,getdate(),substring(ltrim(str(SHIMEKIRIBI)),1,4) + '-' +  ")
                .AppendLine("    substring(ltrim(str(SHIMEKIRIBI)),5,2) + '-' +   ")
                .AppendLine("    substring(ltrim(str(SHIMEKIRIBI)),7,2)) AS DIFF_DATE   ")
            End If
            If strMode = ShishakuKanryoViewMode Then  '*****完了閲覧モード対応*****
                .AppendLine("    SHIMEKIRIBI, ")
                .AppendLine("    DATEDIFF(day,getdate(),substring(ltrim(str(SHIMEKIRIBI)),1,4) + '-' +  ")
                .AppendLine("    substring(ltrim(str(SHIMEKIRIBI)),5,2) + '-' +   ")
                .AppendLine("    substring(ltrim(str(SHIMEKIRIBI)),7,2)) AS DIFF_DATE   ")
            End If
            .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_EVENT ")
            .AppendLine(" where SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "試作部品表編集・改定編集（設計）一覧情報を取得する"
    Public Shared Function GetSpreadList() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("select TEMP.SHISAKU_BUKA_CODE  as dept, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK where BLOCK_FUYOU=@HITSUYOU AND SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and")
            .AppendLine("    SHISAKU_BUKA_CODE=TEMP.SHISAKU_BUKA_CODE GROUP BY SHISAKU_BUKA_CODE) as totalblock, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK where BLOCK_FUYOU=@HITSUYOU AND SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and  ")
            .AppendLine("    JYOUTAI=@SHOUCHIKANRYOU AND SHISAKU_BUKA_CODE=TEMP.SHISAKU_BUKA_CODE GROUP BY SHISAKU_BUKA_CODE) as over1,  ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK where BLOCK_FUYOU=@HITSUYOU AND SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and  ")
            .AppendLine("    (JYOUTAI<>@SHOUCHIKANRYOU or JYOUTAI is null) AND SHISAKU_BUKA_CODE=TEMP.SHISAKU_BUKA_CODE GROUP BY SHISAKU_BUKA_CODE) as remain1,   ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK where BLOCK_FUYOU=@HITSUYOU AND SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and ")
            .AppendLine("    TANTO_SYOUNIN_JYOUTAI=@SHOUNIN1  AND SHISAKU_BUKA_CODE=TEMP.SHISAKU_BUKA_CODE GROUP BY SHISAKU_BUKA_CODE) as over2, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK where BLOCK_FUYOU=@HITSUYOU AND SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and  ")
            .AppendLine("    (TANTO_SYOUNIN_JYOUTAI<>@SHOUNIN1 or TANTO_SYOUNIN_JYOUTAI is null) AND SHISAKU_BUKA_CODE=TEMP.SHISAKU_BUKA_CODE GROUP BY SHISAKU_BUKA_CODE) as remain2,  ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK where BLOCK_FUYOU=@HITSUYOU AND SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and    ")
            .AppendLine("    KACHOU_SYOUNIN_JYOUTAI=@SHOUNIN2  AND SHISAKU_BUKA_CODE=TEMP.SHISAKU_BUKA_CODE GROUP BY SHISAKU_BUKA_CODE) as over3,    ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK where BLOCK_FUYOU=@HITSUYOU AND SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and    ")
            .AppendLine("    (KACHOU_SYOUNIN_JYOUTAI<>@SHOUNIN2 or KACHOU_SYOUNIN_JYOUTAI is null)  AND SHISAKU_BUKA_CODE=TEMP.SHISAKU_BUKA_CODE GROUP BY SHISAKU_BUKA_CODE) as remain3    ")
            .AppendLine(" FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK AS TEMP where SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE ")
            .AppendLine(" group by SHISAKU_BUKA_CODE  ")
            .AppendLine(" ORDER BY dept  ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "試作部品表編集・改定編集（ブロック）課別状況取得する"
    Public Shared Function GetKabetuJyoutai() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("select  ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and SHISAKU_BUKA_CODE=@SHISAKU_BUKA_CODE) as souburoku, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and JYOUTAI=@SHOUCHIKANRYOU and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and SHISAKU_BUKA_CODE=@SHISAKU_BUKA_CODE) as syochi_kan, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and (JYOUTAI<>@SHOUCHIKANRYOU or JYOUTAI is null ) and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and SHISAKU_BUKA_CODE=@SHISAKU_BUKA_CODE) as nokori_kan, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and TANTO_SYOUNIN_JYOUTAI=@SHOUNIN1 and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and SHISAKU_BUKA_CODE=@SHISAKU_BUKA_CODE) as syochi_shounin1, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and (TANTO_SYOUNIN_JYOUTAI<>@SHOUNIN1 or TANTO_SYOUNIN_JYOUTAI is null )  and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and SHISAKU_BUKA_CODE=@SHISAKU_BUKA_CODE) as nokori_shounin1, ")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and KACHOU_SYOUNIN_JYOUTAI=@SHOUNIN2 and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and SHISAKU_BUKA_CODE=@SHISAKU_BUKA_CODE) as syochi_shounin2,")
            .AppendLine("    (select count(SHISAKU_BLOCK_NO) as SHISAKU_BLOCK_NO from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    where BLOCK_FUYOU='0' and (KACHOU_SYOUNIN_JYOUTAI<>@SHOUNIN2 or KACHOU_SYOUNIN_JYOUTAI is null ) and SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE and SHISAKU_BUKA_CODE=@SHISAKU_BUKA_CODE) as nokori_shounin2 ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "試作部品表編集・改定編集（ブロック）一覧情報を取得する"
    Public Shared Function GetBlockSpreadList() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("select SHISAKU_BLOCK_NO_HYOUJI_JUN as BLOCK_JUN,BLOCK_FUYOU as BLOCK_FUYO,JYOUTAI as CODE1,  ")
            .AppendLine("    (SELECT SHISAKU_STATUS_NAME FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS WHERE SHISAKU_STATUS_CODE=TEMP.JYOUTAI) AS NAME1,")
            .AppendLine("    SHISAKU_BLOCK_NO as BLOCK_NO,SHISAKU_BLOCK_NO_KAITEI_NO as KAITEI,UNIT_KBN as UNIT,SHISAKU_BLOCK_NAME as BLOCK_NAME,")
            .AppendLine("    USER_ID as TANTOU,TEL_NO as TEL,SAISYU_KOUSHINBI as U_DATE,SAISYU_KOUSHINJIKAN as U_TIME,TANTO_SYOUNIN_JYOUTAI as CODE2,")
            .AppendLine("    (SELECT SHISAKU_STATUS_NAME FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS WHERE SHISAKU_STATUS_CODE=TEMP.TANTO_SYOUNIN_JYOUTAI) AS NAME2,")
            .AppendLine("    TANTO_SYOUNIN_KA as SP_T_SYOZOKU,TANTO_SYOUNIN_SYA as SP_T_SYOUNINSYA,TANTO_SYOUNIN_HI as SP_T_SYOUNINBI,")
            .AppendLine("    KACHOU_SYOUNIN_JYOUTAI as CODE3,(SELECT SHISAKU_STATUS_NAME FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_STATUS WHERE SHISAKU_STATUS_CODE=TEMP.KACHOU_SYOUNIN_JYOUTAI) AS NAME3,")
            .AppendLine("    KACHOU_SYOUNIN_KA as SP_K_SYOZOKU,KACHOU_SYOUNIN_SYA as SP_K_SYOUNINSYA,KACHOU_SYOUNIN_HI as SP_K_SYOUNINBI from " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK AS TEMP ")
            .AppendLine("    where SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE AND    ")
            .AppendLine("    SHISAKU_BUKA_CODE=@SHISAKU_BUKA_CODE AND SHISAKU_BLOCK_NO_KAITEI_NO= ")
            .AppendLine("    (SELECT MAX(SHISAKU_BLOCK_NO_KAITEI_NO) FROM " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine("    WHERE SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE  ")
            .AppendLine("    AND SHISAKU_BUKA_CODE=@SHISAKU_BUKA_CODE AND SHISAKU_BLOCK_NO=TEMP.SHISAKU_BLOCK_NO ) ORDER BY SHISAKU_BLOCK_NO")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "試作部品表編集・改定編集（ブロック）Excel出力 Get(試作イベントベース車情報 と 試作イベント完成車情報) "

    Public Shared Function GetBlockExcelHeader() As String

        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT       ")
            .AppendLine("  block_soubi.SHISAKU_EVENT_CODE,     ")
            .AppendLine("  block_instl.SHISAKU_BUKA_CODE,     ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO,     ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO_KAITEI_NO,     ")
            .AppendLine("  block_soubi.SHISAKU_SOUBI_HYOUJI_JUN     ")
            .AppendLine("FROM     ")
            .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL AS block_instl ")
            .AppendLine("LEFT JOIN      ")
            .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_SOUBI AS block_soubi ")
            .AppendLine("ON ")
            .AppendLine("  block_instl.SHISAKU_EVENT_CODE = block_soubi.SHISAKU_EVENT_CODE AND ")
            .AppendLine("  block_instl.SHISAKU_BUKA_CODE = block_soubi.SHISAKU_BUKA_CODE AND ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO = block_soubi.SHISAKU_BLOCK_NO AND ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO_KAITEI_NO = block_soubi.SHISAKU_BLOCK_NO_KAITEI_NO ")
            .AppendLine("WHERE ")
            .AppendLine("  block_instl.SHISAKU_EVENT_CODE = @SHISAKU_EVENT_CODE AND ")
            .AppendLine("  block_instl.SHISAKU_BUKA_CODE = @SHISAKU_BUKA_CODE AND ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO = @SHISAKU_BLOCK_NO AND ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO_KAITEI_NO = @SHISAKU_BLOCK_NO_KAITEI_NO  ")
            .AppendLine("ORDER BY  ")
            .AppendLine("  block_soubi.SHISAKU_SOUBI_HYOUJI_JUN ")

        End With
        Return sql.ToString()
    End Function


#End Region

#Region " ブロック状態更新SQL "



    ''' <summary>
    ''' ブロック状態更新する
    ''' </summary>
    ''' <param name="strField"></param>
    ''' <param name="strValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBlockJyotaiUpdateSql(ByVal strField As String, ByVal strValue As String) As String
        Dim sql As New System.Text.StringBuilder()
        'ブロック状態更新するSQL " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK
        With sql
            .Remove(0, .Length)
            .AppendLine("UPDATE ")
            .AppendLine(" " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK SET ")
            .AppendFormat("{0}='{1}',", strField, strValue)
            .AppendLine("    UPDATED_USER_ID=@UPDATED_USER_ID, ")
            .AppendLine("    UPDATED_DATE=@UPDATED_DATE, ")
            .AppendLine("    UPDATED_TIME=@UPDATED_TIME ")
            .AppendLine(" WHERE ")
            .AppendLine("    SHISAKU_EVENT_CODE=@SHISAKU_EVENT_CODE ")
            .AppendLine("    AND SHISAKU_BUKA_CODE=@SHISAKU_BUKA_CODE ")
            .AppendLine("    AND SHISAKU_BLOCK_NO_HYOUJI_JUN=@SHISAKU_BLOCK_NO_HYOUJI_JUN ")
            .AppendLine("    AND SHISAKU_BLOCK_NO=@SHISAKU_BLOCK_NO ")
            .AppendLine("    AND SHISAKU_BLOCK_NO_KAITEI_NO=@SHISAKU_BLOCK_NO_KAITEI_NO ")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "試作部品表編集・改定編集（ブロック）Excel出力 GetHeader1(試作イベントベース車情報 と 試作イベント完成車情報) "

    Public Shared Function GetBlockExcelHeader1() As String

        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT       ")
            '.AppendLine("  block_soubi.SHISAKU_EVENT_CODE,     ")
            '.AppendLine("  block_instl.SHISAKU_SEKKEIKA,     ")
            '.AppendLine("  block_instl.SHISAKU_BLOCK_NO,     ")
            '.AppendLine("  block_instl.SHISAKU_BLOCK_NO_KAITEI_NO,     ")
            .AppendLine("  block_soubi.SHISAKU_SOUBI_HYOUJI_JUN     ")
            .AppendLine("FROM     ")
            .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL AS block_instl ")
            .AppendLine("LEFT JOIN      ")
            .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_SOUBI AS block_soubi ")
            .AppendLine("ON ")
            .AppendLine("  block_instl.SHISAKU_EVENT_CODE = block_soubi.SHISAKU_EVENT_CODE AND ")
            .AppendLine("  block_instl.SHISAKU_BUKA_CODE = block_soubi.SHISAKU_BUKA_CODE AND ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO = block_soubi.SHISAKU_BLOCK_NO AND ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO_KAITEI_NO = block_soubi.SHISAKU_BLOCK_NO_KAITEI_NO ")
            .AppendLine("WHERE ")
            .AppendLine("  block_instl.SHISAKU_EVENT_CODE = @SHISAKU_EVENT_CODE AND ")
            .AppendLine("  block_instl.SHISAKU_BUKA_CODE = @SHISAKU_BUKA_CODE AND ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO = @SHISAKU_BLOCK_NO AND ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO_KAITEI_NO = @SHISAKU_BLOCK_NO_KAITEI_NO  ")
            .AppendLine("ORDER BY  ")
            .AppendLine("  block_soubi.SHISAKU_SOUBI_HYOUJI_JUN ")

        End With
        Return sql.ToString()
    End Function

#End Region

#Region "試作部品表編集・改定編集（ブロック）Excel出力 GetHeader2(試作イベントベース車情報 と 試作イベント完成車情報) "
    Public Shared Function GetBlockExcelHeader2() As String

        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT       ")
            .AppendLine("  block_soubi.SHISAKU_EVENT_CODE,     ")
            .AppendLine("  block_instl.SHISAKU_BUKA_CODE,     ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO,     ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO_KAITEI_NO,     ")
            .AppendLine("  block_soubi.SHISAKU_SOUBI_HYOUJI_JUN     ")
            .AppendLine("FROM     ")
            .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_INSTL AS block_instl ")
            .AppendLine("LEFT JOIN      ")
            .AppendLine("  " & MBOM_DB_NAME & ".dbo.T_SHISAKU_SEKKEI_BLOCK_SOUBI AS block_soubi ")
            .AppendLine("ON ")
            .AppendLine("  block_instl.SHISAKU_EVENT_CODE = block_soubi.SHISAKU_EVENT_CODE AND ")
            .AppendLine("  block_instl.SHISAKU_BUKA_CODE = block_soubi.SHISAKU_BUKA_CODE AND ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO = block_soubi.SHISAKU_BLOCK_NO AND ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO_KAITEI_NO = block_soubi.SHISAKU_BLOCK_NO_KAITEI_NO ")
            .AppendLine("WHERE ")
            .AppendLine("  block_instl.SHISAKU_EVENT_CODE = @SHISAKU_EVENT_CODE AND ")
            .AppendLine("  block_instl.SHISAKU_BUKA_CODE = @SHISAKU_BUKA_CODE AND ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO = @SHISAKU_BLOCK_NO AND ")
            .AppendLine("  block_instl.SHISAKU_BLOCK_NO_KAITEI_NO = @SHISAKU_BLOCK_NO_KAITEI_NO  ")
            .AppendLine("ORDER BY  ")
            .AppendLine("  block_soubi.SHISAKU_SOUBI_HYOUJI_JUN ")

        End With
        Return sql.ToString()
    End Function
#End Region



    '２次改修分
#Region " 試作設計ブロック情報より(コンボボックス用) "
    ''' <summary>
    ''' イベントコード取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBlockShisakuEventCodeSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	SHISAKU_EVENT_CODE ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine(" GROUP BY SHISAKU_EVENT_CODE")
            .AppendLine(" ORDER BY SHISAKU_EVENT_CODE ")
        End With
        Return sql.ToString()
    End Function

    ''' <summary>
    ''' 部課コード取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBlockBukaSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("   COALESCE(MAX(R.KA_RYAKU_NAME),MAX(SHISAKU_BUKA_CODE)) AS KA_RYAKU_NAME ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SHISAKU_SEKKEI_BLOCK BLOCK ")
            .AppendLine("LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 R ")
            .AppendLine(" ON BLOCK.SHISAKU_BUKA_CODE = R.BU_CODE+R.KA_CODE ")
            .AppendLine(" GROUP BY SHISAKU_BUKA_CODE")
            .AppendLine(" ORDER BY KA_RYAKU_NAME ")

        End With
        Return sql.ToString()
    End Function


    ''' <summary>
    ''' ブロック№取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBlockNoSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	SHISAKU_BLOCK_NO ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SHISAKU_SEKKEI_BLOCK ")
            .AppendLine(" GROUP BY SHISAKU_BLOCK_NO")
            .AppendLine(" ORDER BY SHISAKU_BLOCK_NO ")
        End With
        Return sql.ToString()
    End Function

#End Region



    '2012年下期案件分
    '製作一覧システム取込
#Region " 発行№の取得 "
    ''' <summary>
    ''' 発行№取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetHakouNoSqlAll() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	HAKOU_NO ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SEISAKU_HAKOU_HD HD")
            .AppendLine(" GROUP BY HAKOU_NO")
            .AppendLine(" ORDER BY HAKOU_NO")
        End With
        Return sql.ToString()
    End Function

    ''' <summary>
    ''' 発行№取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetHakouNoSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	HAKOU_NO ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SEISAKU_HAKOU_HD HD")
            .AppendLine("   WHERE STATUS  = '" & STATUS_B & "'")
            .AppendLine(" GROUP BY HAKOU_NO")
            .AppendLine(" ORDER BY HAKOU_NO")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " 開発符号の取得 "
    ''' <summary>
    ''' 開発符号取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKaihatsuFugoSqlAll() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	KAIHATSU_FUGO ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SEISAKU_HAKOU_HD HD")
            .AppendLine(" GROUP BY KAIHATSU_FUGO")
            .AppendLine(" ORDER BY KAIHATSU_FUGO")
        End With
        Return sql.ToString()
    End Function

    ''' <summary>
    ''' 開発符号取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKaihatsuFugoSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	KAIHATSU_FUGO ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SEISAKU_HAKOU_HD HD")
            .AppendLine("   WHERE STATUS  = '" & STATUS_B & "'")
            .AppendLine(" GROUP BY KAIHATSU_FUGO")
            .AppendLine(" ORDER BY KAIHATSU_FUGO")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region " イベントの取得 "

    ''' <summary>
    ''' イベント取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetEventSqlAll() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	SEISAKU_EVENT ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SEISAKU_HAKOU_HD HD")
            .AppendLine(" GROUP BY SEISAKU_EVENT")
            .AppendLine(" ORDER BY SEISAKU_EVENT")
        End With
        Return sql.ToString()
    End Function

    ''' <summary>
    ''' イベント取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetEventSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	SEISAKU_EVENT ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SEISAKU_HAKOU_HD HD")
            .AppendLine("   WHERE STATUS  = '" & STATUS_B & "'")
            .AppendLine(" GROUP BY SEISAKU_EVENT")
            .AppendLine(" ORDER BY SEISAKU_EVENT")
        End With
        Return sql.ToString()
    End Function

#End Region

#Region " イベント名称の取得 "

    ''' <summary>
    ''' イベント名称取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetEventNameSqlAll() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	SEISAKU_EVENT_NAME ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SEISAKU_HAKOU_HD HD")
            .AppendLine(" GROUP BY SEISAKU_EVENT_NAME")
            .AppendLine(" ORDER BY SEISAKU_EVENT_NAME")
        End With
        Return sql.ToString()
    End Function

    ''' <summary>
    ''' イベント名称取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetEventNameSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	SEISAKU_EVENT_NAME ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SEISAKU_HAKOU_HD HD")
            .AppendLine("   WHERE STATUS  = '" & STATUS_B & "'")
            .AppendLine(" GROUP BY SEISAKU_EVENT_NAME")
            .AppendLine(" ORDER BY SEISAKU_EVENT_NAME")
        End With
        Return sql.ToString()
    End Function

#End Region


    '20140225　追加

#Region "紐付け画面追加分"

    ''' <summary>
    ''' 開発符号取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>承認済みデータのみ取得</remarks>
    Public Shared Function GetHdKaihatsuFugoSqlStatusB() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	KAIHATSU_FUGO ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SEISAKU_HAKOU_HD HD")
            .AppendLine("WHERE STATUS = '" & STATUS_B & "'")
            .AppendLine(" GROUP BY KAIHATSU_FUGO")
            .AppendLine(" ORDER BY KAIHATSU_FUGO")
        End With
        Return sql.ToString()
    End Function

    ''' <summary>
    ''' イベント取得SQL
    ''' </summary>
    ''' <param name="p_KaihatsuFugo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetHdEventSqlStatusB(ByVal p_KaihatsuFugo As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	SEISAKU_EVENT ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SEISAKU_HAKOU_HD HD")
            .AppendLine("WHERE STATUS = '" & STATUS_B & "'")
            .AppendLine("AND KAIHATSU_FUGO = '" & p_KaihatsuFugo & "'")
            .AppendLine(" GROUP BY SEISAKU_EVENT")
            .AppendLine(" ORDER BY SEISAKU_EVENT")
        End With
        Return sql.ToString()
    End Function

    ''' <summary>
    ''' イベント名取得SQL
    ''' </summary>
    ''' <param name="p_KaihatsuFugo"></param>
    ''' <param name="p_Event"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetHdEventNameSqlStatusB(ByVal p_KaihatsuFugo As String, ByVal p_Event As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	SEISAKU_EVENT_NAME ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SEISAKU_HAKOU_HD HD")
            .AppendLine("WHERE STATUS = '" & STATUS_B & "'")
            .AppendLine("AND KAIHATSU_FUGO = '" & p_KaihatsuFugo & "'")
            .AppendLine("AND SEISAKU_EVENT = '" & p_Event & "'")
            .AppendLine(" GROUP BY SEISAKU_EVENT_NAME")
            .AppendLine(" ORDER BY SEISAKU_EVENT_NAME")
        End With
        Return sql.ToString()
    End Function
#End Region

#Region "試作Cost検索"

    ''' <summary>
    ''' 開発符号取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetHdKaihatsuFugoSql() As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	KAIHATSU_FUGO ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SEISAKU_HAKOU_HD HD")
            .AppendLine(" GROUP BY KAIHATSU_FUGO")
            .AppendLine(" ORDER BY KAIHATSU_FUGO")
        End With
        Return sql.ToString()
    End Function

#Region "ブロックリスト取得"

    ''' <summary>
    ''' ブロックリスト取得
    ''' </summary>
    ''' <param name="lst_KaihatsuFugo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBlockList(ByVal lst_KaihatsuFugo As List(Of String)) As String
        Dim sql As New System.Text.StringBuilder()
        Dim kaihatsuFugo As String = String.Empty
        Dim loopIndex As Integer

        For loopIndex = 0 To lst_KaihatsuFugo.Count - 1
            '条件生成
            kaihatsuFugo = kaihatsuFugo + ",'" + lst_KaihatsuFugo(loopIndex) + "' "
        Next loopIndex

        If kaihatsuFugo.Length > 0 Then
            '開発符号連結
            kaihatsuFugo = kaihatsuFugo.Substring(1)
        End If

        With sql
            .Remove(0, .Length)
            .AppendLine(" SELECT  RHAC80.BLOCK_NO_KINO AS ITEM_CODE ")
            .AppendLine("        ,RHAC80.BLOCK_NO_KINO + ' ' + RHAC40.BLOCK_NAME AS ITEM_NAME ")
            .AppendLine("   FROM ( ")
            .AppendLine("          SELECT KAIHATSU_FUGO ")
            .AppendLine("                ,BLOCK_NO_KINO ")
            .AppendLine("                ,MAX(KAITEI_NO_KINO) AS KAITEI_NO_KINO ")
            .AppendLine("         FROM " + RHACLIBF_DB_NAME + ".dbo.RHAC0080 ")
            .AppendLine("         WHERE KAIHATSU_FUGO IN (" + kaihatsuFugo + ") ")
            .AppendLine("         GROUP BY KAIHATSU_FUGO,BLOCK_NO_KINO ")
            .AppendLine("        ) KEY_DATA ")
            .AppendLine("        INNER JOIN " + RHACLIBF_DB_NAME + ".dbo.RHAC0080 RHAC80 ")
            .AppendLine("         ON KEY_DATA.KAIHATSU_FUGO = RHAC80.KAIHATSU_FUGO ")
            .AppendLine("        AND KEY_DATA.BLOCK_NO_KINO = RHAC80.BLOCK_NO_KINO ")
            .AppendLine("        AND KEY_DATA.KAITEI_NO_KINO = RHAC80.KAITEI_NO_KINO ")
            .AppendLine("        LEFT JOIN " + RHACLIBF_DB_NAME + ".dbo.RHAC0040 RHAC40 ")
            .AppendLine("         ON RHAC80.BLOCK_NO_KINO = RHAC40.BLOCK_NO ")
            .AppendLine("GROUP BY RHAC80.BLOCK_NO_KINO, RHAC40.BLOCK_NAME ")
            .AppendLine("ORDER BY RHAC80.BLOCK_NO_KINO ")
        End With
        Return sql.ToString()

    End Function


#End Region
#Region "ボディー名称取得SQL "
    ''' <summary>
    ''' ボディー名称取得SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBodyNameSql(Optional ByVal aKaihatsuFugo As String = "") As String
        Dim sql As New System.Text.StringBuilder()

        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    BODY_NAME AS VALUE, ")
            .AppendLine("    BODY_NAME AS DISPLAY ")
            .AppendLine("FROM ")
            .AppendLine("    " + MBOM_DB_NAME + ".dbo.M_SHISAKU_BODY_3D ")
            'If StringUtil.IsNotEmpty(aKaihatsuFugo) Then
            .AppendLine("WHERE ")
            .AppendLine("    KAIHATSU_FUGO = '" & aKaihatsuFugo & "' ")
            'End If
            .AppendLine("GROUP BY ")
            .AppendLine("    BODY_NAME ")
            .AppendLine("ORDER BY ")
            .AppendLine("    BODY_NAME ")
        End With

        Return sql.ToString()
    End Function
#End Region

#End Region

    ''↓↓2014/07/23 Ⅰ.3.設計編集 ベース車改修専用化 (TES)張 ADD BEGIN
#Region " ブロック名称取得SQL "
    ''' <summary>
    ''' ブロック名称を取得するSQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBlockNameSql(ByVal kaihatsuFugo As String) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT  ")
            .AppendLine("    T004.BLOCK_NO AS BLOCK_NO, ")
            .AppendLine("    T004.BLOCK_NAME AS BLOCK_NAME ")
            .AppendLine("FROM ")
            .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0080 AS T008 ")
            .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON T008.BLOCK_NO_KINO = T004.BLOCK_NO ")
            .AppendLine("WHERE ")
            .AppendLine("    T008.KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
            .AppendLine("AND T008.HAISI_DATE = 99999999 ")
            .AppendLine("GROUP BY ")
            .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
            .AppendLine("ORDER BY ")
            .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " ブロック名称取得SQL（ユニット区分で抽出） "
    ''' <summary>
    ''' ブロック名称を取得するSQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBlockNameSqlUnitChk(ByVal kaihatsuFugo As String, ByVal unitKbn As String, ByVal unitKbnChk As Boolean) As String
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT  ")
            .AppendLine("    T004.BLOCK_NO AS BLOCK_NO, ")
            .AppendLine("    T004.BLOCK_NAME AS BLOCK_NAME ")
            .AppendLine("FROM ")
            .AppendLine(RHACLIBF_DB_NAME & ".dbo.RHAC0080 AS T008 ")
            .AppendLine("    INNER JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC0040 AS T004 ON T008.BLOCK_NO_KINO = T004.BLOCK_NO ")
            .AppendLine("WHERE ")
            .AppendLine("    T008.KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
            If unitKbn.Equals("M") OrElse unitKbn.Equals("T") Then
                If unitKbnChk = False Then
                    .AppendLine("    AND T008.MT_KBN = '" & unitKbn & "' ")
                ElseIf unitKbnChk = True Then
                    .AppendLine("    AND ( T008.MT_KBN = '" & unitKbn & "' " + " OR T008.MT_KBN = '' ) ")
                End If
            End If
            .AppendLine("AND T008.HAISI_DATE = 99999999 ")
            .AppendLine("GROUP BY ")
            .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
            .AppendLine("ORDER BY ")
            .AppendLine("   T004.BLOCK_NO ,T004.BLOCK_NAME ")
        End With

        Return sql.ToString()
    End Function
#End Region

#Region " 補用部品検索画面用の取得SQL "
    ''' <summary>
    ''' 試作イベントコード取得SQL
    ''' </summary>
    ''' <param name="strKaihatsuFugo">開発符号</param>
    ''' <param name="strShisakuEventCode">試作イベントコード</param>
    ''' <param name="strShisakuEventName">試作イベント名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetShisakuEventCodeSql(ByVal strKaihatsuFugo As String, _
                                                  ByVal strShisakuEventCode As String, _
                                                  ByVal strShisakuEventName As String) As String

        Dim flg As String = ""
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	SHISAKU_EVENT_CODE ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SHISAKU_EVENT ")

            If StringUtil.IsNotEmpty(strKaihatsuFugo) Or _
                StringUtil.IsNotEmpty(strShisakuEventCode) Or _
                StringUtil.IsNotEmpty(strShisakuEventName) Then
                .AppendLine(" WHERE ")
            End If

            If StringUtil.IsNotEmpty(strKaihatsuFugo) Then
                .AppendLine(" SHISAKU_KAIHATSU_FUGO = '" & strKaihatsuFugo & "'")
                flg = "AND"
            End If
            If StringUtil.IsNotEmpty(strShisakuEventCode) Then
                .AppendLine(flg)
                .AppendLine(" SHISAKU_EVENT_CODE = '" & strShisakuEventCode & "'")
                flg = "AND"
            End If
            If StringUtil.IsNotEmpty(strShisakuEventName) Then
                .AppendLine(flg)
                .AppendLine(" SHISAKU_EVENT_NAME = '" & strShisakuEventName & "'")
            End If

            '設計展開以降　及び　中止　以外　の情報のみ取得
            .AppendLine(" AND 21 <= STATUS AND STATUS <> 25 AND STATUS <>26 ")


            .AppendLine(" GROUP BY SHISAKU_EVENT_CODE")
            .AppendLine(" ORDER BY SHISAKU_EVENT_CODE")
        End With
        Return sql.ToString()

    End Function

    ''' <summary>
    ''' 試作イベント名称取得SQL
    ''' </summary>
    ''' <param name="strKaihatsuFugo">開発符号</param>
    ''' <param name="strShisakuEventCode">試作イベントコード</param>
    ''' <param name="strShisakuEventName">試作イベント名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetShisakuEventNameSql(ByVal strKaihatsuFugo As String, _
                                                  ByVal strShisakuEventCode As String, _
                                                  ByVal strShisakuEventName As String) As String

        Dim flg As String = ""
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	SHISAKU_EVENT_NAME ")
            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SHISAKU_EVENT ")

            If StringUtil.IsNotEmpty(strKaihatsuFugo) Or _
                StringUtil.IsNotEmpty(strShisakuEventCode) Or _
                StringUtil.IsNotEmpty(strShisakuEventName) Then
                .AppendLine(" WHERE ")
            End If

            If StringUtil.IsNotEmpty(strKaihatsuFugo) Then
                .AppendLine(" SHISAKU_KAIHATSU_FUGO = '" & strKaihatsuFugo & "'")
                flg = "AND"
            End If
            If StringUtil.IsNotEmpty(strShisakuEventCode) Then
                .AppendLine(flg)
                .AppendLine(" SHISAKU_EVENT_CODE = '" & strShisakuEventCode & "'")
                flg = "AND"
            End If
            If StringUtil.IsNotEmpty(strShisakuEventName) Then
                .AppendLine(flg)
                .AppendLine(" SHISAKU_EVENT_NAME = '" & strShisakuEventName & "'")
            End If

            '設計展開以降　及び　中止　以外　の情報のみ取得
            .AppendLine(" AND 21 <= STATUS AND STATUS <> 25 AND STATUS <>26 ")

            .AppendLine(" GROUP BY SHISAKU_EVENT_NAME")
            .AppendLine(" ORDER BY SHISAKU_EVENT_NAME")
        End With
        Return sql.ToString()

    End Function

    ''' <summary>
    ''' 試作手配データ検索コンボボックス用イベント取得
    ''' </summary>
    ''' <param name="strKaihatsuFugo">開発符号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetShisakuEventCodeAndNameSql(ByVal strKaihatsuFugo As String) As String


        Dim flg As String = ""
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("	SHISAKU_EVENT_CODE ")
            .AppendLine("	,(SHISAKU_EVENT_CODE + '：' + SHISAKU_EVENT_NAME) AS DISPLAYSTRING")

            .AppendLine("FROM ")
            .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_SHISAKU_EVENT ")

            If StringUtil.IsNotEmpty(strKaihatsuFugo) Then
                .AppendLine(" WHERE SHISAKU_KAIHATSU_FUGO = '" & strKaihatsuFugo & "'")
            End If

            '設計展開以降　及び　中止　以外　の情報のみ取得
            .AppendLine(" AND 21 <= STATUS AND STATUS <> 25 AND STATUS <>26 ")

            .AppendLine(" GROUP BY SHISAKU_EVENT_CODE,(SHISAKU_EVENT_CODE + '：' + SHISAKU_EVENT_NAME) ")
            .AppendLine(" ORDER BY SHISAKU_EVENT_CODE ")
        End With
        Return sql.ToString()

    End Function

#End Region

    ''↑↑2014/07/23 Ⅰ.3.設計編集 ベース車改修専用化 (TES)張 ADD END

End Class
