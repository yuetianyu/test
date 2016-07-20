Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Common.mdlConstraint
Imports EBom.Data
Imports System.Reflection
Imports System.Text
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.Kosei.Vo.Helper
Imports ShisakuCommon.Ui.Spd


''' <summary>
''' 権限マスター一覧
''' </summary>
''' <remarks></remarks>
Public Class KengenMasert01
#Region " メンバー変数 "
    ''' <summary>ビュー</summary>
    Private m_view As frm51KengenMaster01

    ''' <summary>FpSpread 共通</summary>
    Private m_spCom As SpreadCommon

    ''' <summary>検索条件</summary>
    Private m_condition As KengenSearchConditionVo

#End Region

#Region " コンストラクタ "
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="f"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal f As frm51KengenMaster01)
        m_view = f
        m_condition = New KengenSearchConditionVo()
    End Sub
#End Region

#Region " ビュー初期化 "
    ''' <summary>
    ''' ビューの初期化    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InitView() As RESULT
        m_view.spdInfo_Sheet1.SetRowSizeable(m_view.spdInfo_Sheet1.Rows.Count, False)
        ShisakuFormUtil.setTitleVersion(m_view)
        ShisakuFormUtil.SetDateTimeNow(m_view.LblDateNow, m_view.LblTimeNow)
        ShisakuFormUtil.SetIdAndBuka(m_view.LblCurrUserId, m_view.LblCurrBukaName)
        m_view.spdInfo.FocusRenderer = New FarPoint.Win.Spread.MarqueeFocusIndicatorRenderer(Color.Blue, 1)
        Dim dtUserId As DataTable = GetUserIdData()
        FormUtil.ComboBoxBind(m_view.cmbUserId, dtUserId, "USER_ID", "USER_ID")
        Dim dtShain As DataTable = GetShainData()
        FormUtil.ComboBoxBind(m_view.cmbUserName, dtShain, "SHAIN_NAME", "SHAIN_NAME")
        Dim dtBuka As DataTable = GetBukaData()
        FormUtil.ComboBoxBind(m_view.cmbBuka, dtBuka, "BUKA", "BUKA")
        Dim dtSite As DataTable = GetSiteData()
        FormUtil.ComboBoxBind(m_view.cmbSite, dtSite, "SITE_INFO", "SITE_INFO")
        Dim dtCompetent As DataTable = GetCompetentData()
        FormUtil.ComboBoxBind(m_view.cmbCompetent, dtCompetent, "COMPETENT", "COMPETENT")
        SetSpreadInit()
        SetDataList()
    End Function
#End Region

#Region " SPREAD初期化を設定する "
    ''' <summary>
    ''' SPREAD初期化を設定する    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetSpreadInit()
        'hidden列を追加する（部課コード）     
        m_view.spdInfo_Sheet1.Columns.Add(m_view.spdInfo_Sheet1.ColumnCount, 1)
        m_view.spdInfo_Sheet1.Columns(m_view.spdInfo_Sheet1.ColumnCount - 1).Visible = False
        '列の項目を設定する       
        m_view.spdInfo_Sheet1.Columns(0).DataField = "USER_ID"
        m_view.spdInfo_Sheet1.Columns(1).DataField = "SHAIN_NAME"
        m_view.spdInfo_Sheet1.Columns(2).DataField = "KA_RYAKU_NAME"
        m_view.spdInfo_Sheet1.Columns(3).DataField = "SITE_INFO"
        m_view.spdInfo_Sheet1.Columns(4).DataField = "COMPETENT"
        m_view.spdInfo_Sheet1.Columns(5).DataField = "MENU"
        m_view.spdInfo_Sheet1.Columns(6).DataField = "IBENTO"
        m_view.spdInfo_Sheet1.Columns(7).DataField = "MASTER"
        m_view.spdInfo_Sheet1.Columns(8).DataField = "SHONIN"
        '2014/02/05　日程管理ツール用権限を追加
        m_view.spdInfo_Sheet1.Columns(9).DataField = "NITTEI"
        '2014/02/05　現調管理機能追加の為
        m_view.spdInfo_Sheet1.Columns(10).DataField = "ORDERSHEET1"
        m_view.spdInfo_Sheet1.Columns(11).DataField = "ORDERSHEET2"

        m_view.spdInfo_Sheet1.Columns(12).DataField = "BUKA_CODE"

    End Sub
#End Region

#Region "spreadでデータ表示した後で　spread 列の設定"
    ''' <summary>
    ''' spreadでデータ表示した後で　spread 列の設定
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetSpdColProperty()
        m_view.spdInfo.ActiveSheet.RowHeader.Columns(0).Width = 35
        m_view.spdInfo_Sheet1.SetColumnWidth(0, 80)
        m_view.spdInfo_Sheet1.SetColumnWidth(1, 135)
        m_view.spdInfo_Sheet1.SetColumnWidth(2, 75)
        m_view.spdInfo_Sheet1.SetColumnWidth(3, 50)
        m_view.spdInfo_Sheet1.SetColumnWidth(4, 45)
        m_view.spdInfo_Sheet1.SetColumnWidth(5, 90)
        m_view.spdInfo_Sheet1.SetColumnWidth(6, 60)
        m_view.spdInfo_Sheet1.SetColumnWidth(7, 95)
        m_view.spdInfo_Sheet1.SetColumnWidth(8, 60)
        m_view.spdInfo_Sheet1.SetColumnWidth(9, 100)
        m_view.spdInfo_Sheet1.SetColumnWidth(10, 80)
        m_view.spdInfo_Sheet1.SetColumnWidth(11, 80)
        m_view.spdInfo_Sheet1.Columns(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        m_view.spdInfo_Sheet1.Columns(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
        m_view.spdInfo_Sheet1.Columns(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        m_view.spdInfo_Sheet1.Columns(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        m_view.spdInfo_Sheet1.Columns(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        m_view.spdInfo_Sheet1.Columns(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        m_view.spdInfo_Sheet1.Columns(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        m_view.spdInfo_Sheet1.Columns(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        m_view.spdInfo_Sheet1.Columns(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        m_view.spdInfo_Sheet1.Columns(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        m_view.spdInfo_Sheet1.Columns(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        m_view.spdInfo_Sheet1.Columns(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        m_view.spdInfo_Sheet1.Columns(0).Resizable = False
        m_view.spdInfo_Sheet1.Columns(1).Resizable = False
        m_view.spdInfo_Sheet1.Columns(2).Resizable = False
        m_view.spdInfo_Sheet1.Columns(3).Resizable = False
        m_view.spdInfo_Sheet1.Columns(4).Resizable = False
        m_view.spdInfo_Sheet1.Columns(5).Resizable = False
        m_view.spdInfo_Sheet1.Columns(6).Resizable = False
        m_view.spdInfo_Sheet1.Columns(7).Resizable = False
        m_view.spdInfo_Sheet1.Columns(8).Resizable = False
        m_view.spdInfo_Sheet1.Columns(9).Resizable = False
        m_view.spdInfo_Sheet1.Columns(10).Resizable = False
        m_view.spdInfo_Sheet1.Columns(11).Resizable = False
        m_view.spdInfo.FocusRenderer = New FarPoint.Win.Spread.MarqueeFocusIndicatorRenderer(Color.Blue, 1)
    End Sub
#End Region

#Region " ユーザーID、ユーザー名、部課、サイト、区分を取得して、コンボボックス "
    ''' <summary>
    ''' ユーザーマスタ・ユーザーIDを取得する。    ''' </summary>
    ''' <returns>全部ユーザーのデータテーブル</returns>
    ''' <remarks></remarks>
    Private Function GetUserIdData() As DataTable
        Dim dtData As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            db.Open()
            Dim sql As New StringBuilder
            db.Fill(DataSqlCommon.GetAllUserMasSql(), dtData)
        End Using
        Return dtData
    End Function
    ''' <summary>
    ''' 社員外部インターフェース、社員マスター情報より社員名を取得する。    '''※同一ユーザーは除く。
    ''' </summary>
    ''' <returns>全部社員のデータテーブル</returns>
    ''' <remarks></remarks>
    Private Function GetShainData()
        Dim dtData As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            db.Open()
            Dim sql As New StringBuilder
            db.Fill(DataSqlCommon.Get_Shain_Sql(), dtData)
        End Using
        Return dtData
    End Function
    ''' <summary>
    ''' 社員外部インターフェース、社員マスター情報より部課を設定。    '''※同一番号は除く。
    ''' </summary>
    ''' <returns>全部部課の略称のデータテーブル</returns>
    ''' <remarks></remarks>
    Private Function GetBukaData() As DataTable
        Dim dtData As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            'Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            db.Open()
            Dim sql As New StringBuilder
            db.Fill(DataSqlCommon.GetBukaSql, dtData)
        End Using
        Return dtData
    End Function
    ''' <summary>
    ''' ユーザーマスタよりサイトを設定。    ''' ※同一番号は除く。    ''' </summary>
    ''' <returns>全部サイトのデータテーブル</returns>
    ''' <remarks></remarks>
    Private Function GetSiteData() As DataTable
        Dim dtData As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            db.Open()
            Dim sql As New StringBuilder
            db.Fill(DataSqlCommon.GetAllUserMasSiteSql, dtData)
        End Using
        Return dtData
    End Function
    ''' <summary>
    ''' ユーザーマスタより区分を設定。    ''' ※同一番号は除く。    ''' </summary>
    ''' <returns>全部区分のデータテーブル</returns>
    ''' <remarks></remarks>
    Private Function GetCompetentData() As DataTable
        Dim dtData As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            db.Open()
            Dim sql As New StringBuilder
            db.Fill(DataSqlCommon.GetAllUserMasCompetentSql, dtData)
        End Using
        Return dtData
    End Function
#End Region

#Region " 検索して、取得するデータを表示される "
    ''' <summary>
    ''' 取得するデータを表示される
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDataList()
        SetCondition()
        m_view.spdInfo_Sheet1.DataSource = GetDataListInfo()
        SetSpdColProperty()
    End Sub
#End Region

#Region " 検索して、データを取得する "
    ''' <summary>
    ''' 検索条件より　データを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataListInfo() As DataTable
        Dim dtData As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            db.Open()
            Dim sql As New StringBuilder
            db.Fill(GetKengenMasDisplaySql(m_condition), dtData)
        End Using
        Return dtData
    End Function
#End Region

#Region " 検索して、検索条件を納入 "
    ''' <summary>
    ''' 検索条件の納入
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetCondition()
        m_condition.ShainNo = m_view.cmbUserId.Text.Trim
        m_condition.ShainName = m_view.cmbUserName.Text
        m_condition.Buka = m_view.cmbBuka.Text
        m_condition.Site = m_view.cmbSite.Text
        m_condition.Competent = m_view.cmbCompetent.Text
    End Sub
#End Region

#Region " spreadで　行をダブルクリックする "
    ''' <summary>
    ''' spreadで　行をダブルクリックして　更新画面を移動される
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub GotoUpdate(ByVal e As FarPoint.Win.Spread.CellClickEventArgs)
        Dim DlgNewForm As New frm51KengenMaster02
        DlgNewForm.lblUserCode.Text = m_view.spdInfo_Sheet1.Cells(e.Row, 0).Text
        DlgNewForm.lblUserName.Text = m_view.spdInfo_Sheet1.Cells(e.Row, 1).Text
        DlgNewForm.lblBuka.Text = m_view.spdInfo_Sheet1.Cells(e.Row, 2).Text
        DlgNewForm.lblSITE.Text = m_view.spdInfo_Sheet1.Cells(e.Row, 3).Text
        DlgNewForm.lblUserKubun.Text = m_view.spdInfo_Sheet1.Cells(e.Row, 4).Text
        DlgNewForm.lblUserKubunName.Text = ShisakuComFunc.GetUserKunbunName(m_view.spdInfo_Sheet1.Cells(e.Row, 4).Text)
        DlgNewForm.MenuName = m_view.spdInfo_Sheet1.Cells(e.Row, 5).Text
        DlgNewForm.BukaCode = m_view.spdInfo_Sheet1.Cells(e.Row, 8).Text
        m_view.cmbUserId.Focus()

        DlgNewForm.ShowDialog()
        SetDataList()
    End Sub
#End Region

#Region " 権限マスターの一覧のデータ取得SQL "
    ''' <summary>
    ''' 権限マスターの一覧のデータ取得するSQL RHAC0660
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKengenMasDisplaySql(ByVal condition As KengenSearchConditionVo) As String
        Dim databaseName As String = ComFunc.GetDatabaseName(g_kanrihyoIni(ShisakuGlobal.DB_KEY_EBOM))
        Dim sql As New System.Text.StringBuilder()
        With sql
            .Remove(0, .Length)
            .AppendLine("SELECT ")
            .AppendLine("    " + EBOM_DB_NAME + ".dbo.M_USER.USER_ID, ")
            .AppendLine("    SHAIN.SHAIN_NAME, ")
            .AppendLine("    SHAIN.BUKA_CODE, ")
            .AppendLine("    KA.KA_RYAKU_NAME, ")
            .AppendLine("    " + EBOM_DB_NAME + ".dbo.M_USER.SITE_INFO, ")
            .AppendLine("    " + EBOM_DB_NAME + ".dbo.M_USER.COMPETENT,  ")
            .AppendFormat(" (CASE WHEN AUTHORITY1.AUTHORITY_KBN = " & MAuthorityUserVoHelper.MenuAuthorityKbn.SEKKEI & _
                          " THEN '{0}' WHEN AUTHORITY1.AUTHORITY_KBN = " & MAuthorityUserVoHelper.MenuAuthorityKbn.SHISAKU & _
                          " THEN '{1}' WHEN AUTHORITY1.AUTHORITY_KBN = " & MAuthorityUserVoHelper.MenuAuthorityKbn.SHISAKU1KA & _
                          " THEN '{2}' WHEN AUTHORITY1.AUTHORITY_KBN = " & MAuthorityUserVoHelper.MenuAuthorityKbn.KENGEN_NASHI & _
                          " THEN ''  ", _
                          ShisakuGlobal.PROGRAM_NAME_MENU(1), ShisakuGlobal.PROGRAM_NAME_MENU(2), ShisakuGlobal.PROGRAM_NAME_MENU(3))
            .AppendFormat("  WHEN " + EBOM_DB_NAME + ".dbo.M_USER.COMPETENT = 1 THEN '{0}' WHEN (" + EBOM_DB_NAME + ".dbo.M_USER.COMPETENT = 2 AND SHAIN.BUKA_CODE='{2}') THEN '{1}'    ", _
                          ShisakuGlobal.PROGRAM_NAME_MENU(1), ShisakuGlobal.PROGRAM_NAME_MENU(2), ShisakuGlobal.SkeBukaCode)
            .AppendFormat(" ELSE '' END) AS MENU,")
            .AppendLine(" (CASE WHEN AUTHORITY2.AUTHORITY_KBN = " & MAuthorityUserVoHelper.AuthorityKbn.KENGEN_ARI & " THEN '" & MAuthorityUserVoHelper.AuthorityKbnMoji.KENGEN_ARI & "'")
            .AppendLine(" WHEN AUTHORITY2.AUTHORITY_KBN = " & MAuthorityUserVoHelper.AuthorityKbn.KENGEN_NASHI & " THEN '" & MAuthorityUserVoHelper.AuthorityKbnMoji.KENGEN_NASHI & "'")
            .AppendLine(" ELSE '' END) AS IBENTO, ")
            .AppendLine(" (CASE WHEN AUTHORITY3.AUTHORITY_KBN = " & MAuthorityUserVoHelper.AuthorityKbn.KENGEN_ARI & " THEN '" & MAuthorityUserVoHelper.AuthorityKbnMoji.KENGEN_ARI & "'")
            .AppendLine(" WHEN AUTHORITY2.AUTHORITY_KBN = " & MAuthorityUserVoHelper.AuthorityKbn.KENGEN_NASHI & " THEN '" & MAuthorityUserVoHelper.AuthorityKbnMoji.KENGEN_NASHI & "'")
            .AppendLine(" ELSE '' END) AS MASTER, ")
            '2012/01/24 承認アラート表示機能追加
            .AppendLine(" CASE ISNULL(BUKA.SHISAKU_BUKA_CODE,'')")
            .AppendLine(" WHEN '' THEN '' ")
            .AppendLine(" ELSE BUKA.KA_RYAKU_NAME END AS SHONIN, ")

            '------------------------------------------------------------------------------------------------
            '2014/02/05 日程管理ツール権限追加
            .AppendLine(" CASE ")
            .AppendLine("     WHEN AUTHORITY5.AUTHORITY_KBN = " & MAuthorityUserVoHelper.AuthorityKbn.KENGEN_ARI & " AND ")
            .AppendLine("          AUTHORITY5.KINO_ID_2 = " & MAuthorityUserVoHelper.NitteiKinoId.NITTEI_KINO2ID_ADMIN & " THEN '" & MAuthorityUserVoHelper.NitteiKinoIdName.NITTEI_KINO2ID_ADMIN_NAME & " " & MAuthorityUserVoHelper.AuthorityKbnMoji.KENGEN_ARI & "'")
            .AppendLine("     WHEN AUTHORITY5.AUTHORITY_KBN = " & MAuthorityUserVoHelper.AuthorityKbn.KENGEN_ARI & " AND ")
            .AppendLine("          AUTHORITY5.KINO_ID_2 = " & MAuthorityUserVoHelper.NitteiKinoId.NITTEI_KINO2ID_COMPARE & " THEN '" & MAuthorityUserVoHelper.NitteiKinoIdName.NITTEI_KINO2ID_COMPARE_NAME & " " & MAuthorityUserVoHelper.AuthorityKbnMoji.KENGEN_ARI & "'")
            .AppendLine("     WHEN AUTHORITY5.AUTHORITY_KBN = " & MAuthorityUserVoHelper.AuthorityKbn.KENGEN_ARI & " AND ")
            .AppendLine("          AUTHORITY5.KINO_ID_2 = " & MAuthorityUserVoHelper.NitteiKinoId.NITTEI_KINO2ID_UPDATE & " THEN '" & MAuthorityUserVoHelper.NitteiKinoIdName.NITTEI_KINO2ID_UPDATE_NAME & " " & MAuthorityUserVoHelper.AuthorityKbnMoji.KENGEN_ARI & "'")
            .AppendLine("     ELSE '' ")
            .AppendLine(" END AS NITTEI ,")
            '------------------------------------------------------------------------------------------------
            '2015/02/05 オーダーシート表示機能追加
            .AppendLine(" CASE ")
            .AppendLine("     WHEN AUTHORITY6.AUTHORITY_KBN = " & MAuthorityUserVoHelper.AuthorityKbn.KENGEN_ARI & " AND ")
            .AppendLine("          AUTHORITY6.KINO_ID_2 = '" & MAuthorityUserVoHelper.OrderKinoId.ORDER_KINO2ID_USE & "' THEN '" & MAuthorityUserVoHelper.OrderKinoIdName.ORDER_KINO2ID_USE_NAME & " " & MAuthorityUserVoHelper.AuthorityKbnMoji.KENGEN_ARI & "'")
            .AppendLine("     ELSE '' ")
            .AppendLine(" END AS ORDERSHEET1 ,")
            .AppendLine(" CASE ")
            .AppendLine("     WHEN AUTHORITY7.AUTHORITY_KBN = " & MAuthorityUserVoHelper.AuthorityKbn.KENGEN_ARI & " AND ")
            .AppendLine("          AUTHORITY7.KINO_ID_2 = '" & MAuthorityUserVoHelper.OrderKinoId.ORDER_KINO2ID_EDIT & "' THEN '" & MAuthorityUserVoHelper.OrderKinoIdName.ORDER_KINO2ID_EDIT_NAME & " " & MAuthorityUserVoHelper.AuthorityKbnMoji.KENGEN_ARI & "'")
            .AppendLine("     ELSE '' ")
            .AppendLine(" END AS ORDERSHEET2 ")
            '------------------------------------------------------------------------------------------------

            .AppendLine("FROM ")
            .AppendLine("    " + EBOM_DB_NAME + ".dbo.M_USER ")
            'メニュー
            .AppendLine("LEFT JOIN ")
            .AppendLine("    " + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER AS AUTHORITY1  ")
            'メニュー
            .AppendLine("    ON AUTHORITY1.APP_NO='" & MAuthorityUserVoHelper.AppNo.TRIAL_MANUFACTURE & "'")
            .AppendLine("    AND AUTHORITY1.USER_ID=" + EBOM_DB_NAME + ".dbo.M_USER.USER_ID")
            .AppendLine("    AND AUTHORITY1.KINO_ID_1='" & MAuthorityUserVoHelper.KinoId1.Menu & "'")
            '承認押下            
            .AppendLine("LEFT JOIN ")
            .AppendLine("    " + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER AS AUTHORITY2  ")
            .AppendLine("    ON AUTHORITY2.APP_NO='" & MAuthorityUserVoHelper.AppNo.TRIAL_MANUFACTURE & "'")
            .AppendLine("    AND AUTHORITY2.USER_ID=" + EBOM_DB_NAME + ".dbo.M_USER.USER_ID")
            .AppendLine("    AND AUTHORITY2.KINO_ID_1='" & MAuthorityUserVoHelper.KinoId1.SHISAKU_BUHIN & "' ")
            .AppendLine("    AND AUTHORITY2.KINO_ID_2='" & MAuthorityUserVoHelper.KinoId2.Shounin & "' ")
            '車系／開発符号マスター
            .AppendLine("LEFT JOIN ")
            .AppendLine("    " + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER AS AUTHORITY3  ")
            .AppendLine("    ON AUTHORITY3.APP_NO='" & MAuthorityUserVoHelper.AppNo.TRIAL_MANUFACTURE & "'")
            .AppendLine("    AND AUTHORITY3.USER_ID=" + EBOM_DB_NAME + ".dbo.M_USER.USER_ID")
            .AppendLine("    AND AUTHORITY3.KINO_ID_1='" & MAuthorityUserVoHelper.KinoId1.KAIHATU_FUGOU & "'")
            .AppendLine("    AND AUTHORITY3.KINO_ID_2='" & MAuthorityUserVoHelper.KinoId2.Shiyou & "'")
            '社員
            .AppendLine("LEFT JOIN ")
            .AppendFormat("    ({0})  AS SHAIN ", DataSqlCommon.Get_All_Syain_Sql)
            .AppendLine("ON ")
            .AppendLine("    " + EBOM_DB_NAME + ".dbo.M_USER.USER_ID = SHAIN.SHAIN_NO ")
            '課略称
            .AppendFormat("LEFT JOIN " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 KA ", databaseName)
            .AppendLine("ON ")
            .AppendLine("    LEFT(SHAIN.BUKA_CODE,2) = KA.BU_CODE AND RIGHT(SHAIN.BUKA_CODE,2)=KA.KA_CODE AND SHAIN.SITE_KBN=KA.SITE_KBN ")
            '試作メニューログインマスター
            .AppendFormat("LEFT JOIN " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN  ", databaseName)
            .AppendLine("ON ")
            .AppendLine("    " + EBOM_DB_NAME + ".dbo.M_USER.USER_ID = " & MBOM_DB_NAME & ".dbo.M_SHISAKU_LOGIN.SEKKEI_SHAIN_NO ")
            '2012/01/24 承認アラート表示機能追加
            ''承認設計課
            .AppendLine("LEFT JOIN ")
            .AppendLine("    " + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER AS AUTHORITY4  ")
            .AppendLine("    ON AUTHORITY4.APP_NO='" & MAuthorityUserVoHelper.AppNo.TRIAL_MANUFACTURE & "'")
            .AppendLine("    AND AUTHORITY4.USER_ID=" + EBOM_DB_NAME + ".dbo.M_USER.USER_ID")
            .AppendLine("    AND AUTHORITY4.KINO_ID_1='" & MAuthorityUserVoHelper.KinoId1.SHONIN & "'")
            .AppendLine("    AND AUTHORITY4.AUTHORITY_KBN='1'")

            '------------------------------------------------------------------------------------------------
            '2014/02/05 日程管理ツール権限追加
            ' 日程管理ツールの権限のありなし
            .AppendLine("LEFT JOIN ")
            .AppendLine("    " + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER AS AUTHORITY5  ")
            .AppendLine("    ON AUTHORITY5.APP_NO='" & MAuthorityUserVoHelper.AppNo.TRIAL_MANUFACTURE & "'")
            .AppendLine("    AND AUTHORITY5.USER_ID=" + EBOM_DB_NAME + ".dbo.M_USER.USER_ID")
            .AppendLine("    AND AUTHORITY5.KINO_ID_1='" & MAuthorityUserVoHelper.KinoId1.NITTEI & "'")
            .AppendLine("    AND AUTHORITY5.AUTHORITY_KBN='1'")
            '------------------------------------------------------------------------------------------------

            '2015/02/05 オーダーシート表示機能追加
            ''
            .AppendLine("LEFT JOIN ")
            .AppendLine("    " + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER AS AUTHORITY6  ")
            .AppendLine("    ON AUTHORITY6.APP_NO='" & MAuthorityUserVoHelper.AppNo.TRIAL_MANUFACTURE & "'")
            .AppendLine("    AND AUTHORITY6.USER_ID=" + EBOM_DB_NAME + ".dbo.M_USER.USER_ID")
            .AppendLine("    AND AUTHORITY6.KINO_ID_1='" & MAuthorityUserVoHelper.KinoId1.ORDER_SHEET & "'")
            .AppendLine("    AND AUTHORITY6.KINO_ID_2='" & MAuthorityUserVoHelper.KinoId2.SHIYOU & "' ")
            .AppendLine("    AND AUTHORITY6.AUTHORITY_KBN='1'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("    " + MBOM_DB_NAME + ".dbo.M_AUTHORITY_USER AS AUTHORITY7  ")
            .AppendLine("    ON AUTHORITY7.APP_NO='" & MAuthorityUserVoHelper.AppNo.TRIAL_MANUFACTURE & "'")
            .AppendLine("    AND AUTHORITY7.USER_ID=" + EBOM_DB_NAME + ".dbo.M_USER.USER_ID")
            .AppendLine("    AND AUTHORITY7.KINO_ID_1='" & MAuthorityUserVoHelper.KinoId1.ORDER_SHEET & "'")
            .AppendLine("    AND AUTHORITY7.KINO_ID_2='" & MAuthorityUserVoHelper.KinoId2.HENSHUU & "' ")
            .AppendLine("    AND AUTHORITY7.AUTHORITY_KBN='1'")

            '------------------------------------------------------------------------------------------------
            '課略称
            .AppendLine("LEFT JOIN (" & KengenMasert02.GetBUKAMASTERSQL & ") AS BUKA ")
            .AppendLine("ON ")
            .AppendLine("  AUTHORITY4.KINO_ID_3 = BUKA.SHISAKU_BUKA_CODE ")


            .AppendLine("    WHERE  1=1 ")
            If Not condition.ShainNo = String.Empty Then
                .AppendFormat("    AND   " + EBOM_DB_NAME + ".dbo.M_USER.USER_ID LIKE '{0}%'", condition.ShainNo)
            End If
            If Not condition.ShainName = String.Empty Then
                .AppendFormat("    AND   SHAIN.SHAIN_NAME LIKE '{0}%'", condition.ShainName)
            End If
            If Not condition.Buka = String.Empty Then
                .AppendFormat("    AND   KA.KA_RYAKU_NAME LIKE '{0}%'", condition.Buka)
            End If
            If Not condition.Site = String.Empty Then
                .AppendFormat("    AND   " + EBOM_DB_NAME + ".dbo.M_USER.SITE_INFO LIKE '{0}%'", condition.Site)
            End If
            If Not condition.Competent = String.Empty Then
                .AppendFormat("    AND   " + EBOM_DB_NAME + ".dbo.M_USER.COMPETENT LIKE '{0}%'", condition.Competent)
            End If
            .AppendLine(" ORDER BY " + EBOM_DB_NAME + ".dbo.M_USER.USER_ID")
        End With
        Return sql.ToString()
    End Function
#End Region


End Class
