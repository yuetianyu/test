Imports EBom.Common
Imports FarPoint.Win
Imports EBom.Data
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd


Public Class LoginMaster01

    Private m_view As New frm5LoginMaster01
    'FpSpread 共通
    Private m_spCom As SpreadCommon

#Region " コンストラクタ "

    Public Sub New(ByVal f As frm5LoginMaster01)
        m_view = f
        m_spCom = New SpreadCommon(m_view.spdParts)

    End Sub
#End Region

#Region "  'Login Master初期化 "

    Public Sub InitView()
        '画面のTitle VerNo.
        ShisakuFormUtil.setTitleVersion(m_view)
        '画面の時間が表示されます。
        ShisakuFormUtil.SetDateTimeNow(m_view.LblDateNow, m_view.LblTimeNow)
        '画面の時間が表示されます。
        ShisakuFormUtil.SetIdAndBuka(m_view.LblCurrUserId, m_view.LblCurrBukaName)
        '画面のPG-IDが表示されます。
        m_view.LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_03
        '画面のPG-Nameが表示されます。
        m_view.LblCurrPGName.Text = PROGRAM_NAME_03
        '画面のMessageが表示されます。(ダブルクリックで選択してください。)
        m_view.lblMsg.Text = T0007
        m_view.cmbUserId.Focus()

        'Combox中の全ユーザーId(権限1と2のユーザー)が取得
        Dim dtUserId As DataTable = GetUserIdData()
        FormUtil.ComboBoxBind(m_view.cmbUserId, dtUserId, "USER_ID", "USER_ID")
        'Combox中の全ユーザー名(権限1と2のユーザー)が取得
        Dim dtShain As DataTable = GetShainData()
        FormUtil.ComboBoxBind(m_view.cmbUserName, dtShain, "SHAIN_NAME", "SHAIN_NAME")
        'Combox中の全部門名が取得
        Dim dtBuka As DataTable = GetBukaData()
        FormUtil.ComboBoxBind(m_view.cmbDepart, dtBuka, "BUKA", "BUKA")
        'Combox中のサイトが取得
        Dim dtSite As DataTable = GetSiteData()
        FormUtil.ComboBoxBind(m_view.cmbSite, dtSite, "SITE_INFO", "SITE_INFO")

        'Spreadのデータが取得。
        SetDataList()

    End Sub
#End Region

#Region " ユーザーID、ユーザー名、部課、サイト、取得して、コンボボックス "

    ''' <summary>
    ''' ユーザーIDが取得して,コンボボックスに縛ります
    ''' </summary>
    ''' <returns>全てユーザーIDのデータテーブルが戻る</returns>
    ''' <remarks></remarks>
    Private Function GetUserIdData() As System.Data.DataTable

        'ユーザーIdデーターテーブル
        Dim dtUserId As New DataTable()

        'DB Access
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            'DB Open
            db.Open()
            'ユーザーIdが検索
            db.Fill(DataSqlCommon.GetAllUserMasSql(), dtUserId)
        End Using
        Return dtUserId
    End Function

    ''' <summary>
    ''' ユーザー名が取得して,コンボボックスに縛ります
    ''' </summary>
    ''' <returns>全てユーザー名のデータテーブルが戻る</returns>
    ''' <remarks></remarks>
    Private Function GetShainData() As System.Data.DataTable
        'ユーザー名データーテーブル
        Dim dtUserName As New DataTable()

        'DB Access
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            'DB Open
            db.Open()
            'ユーザー名が検索
            db.Fill(DataSqlCommon.Get_Shain_Sql(), dtUserName)
        End Using
        Return dtUserName
    End Function

    ''' <summary>
    ''' 部課が取得して,コンボボックスに縛ります
    ''' </summary>
    ''' <returns>全て部課のデータテーブルが戻る</returns>
    ''' <remarks></remarks>
    Private Function GetBukaData() As System.Data.DataTable
        '部課名データーテーブル
        Dim dtUserDepart As New DataTable()

        'DB Access
        'Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            'DB Open
            db.Open()
            '部課名が検索
            db.Fill(DataSqlCommon.GetBukaSql(), dtUserDepart)
        End Using
        Return dtUserDepart
    End Function

    ''' <summary>
    ''' サイトが取得して,コンボボックスに縛ります
    ''' </summary>
    ''' <returns>全てサイトのデータテーブルが戻る</returns>
    ''' <remarks></remarks>
    Private Function GetSiteData() As System.Data.DataTable
        'サイトデーターテーブル
        Dim dtSite As New DataTable()

        'DB Access
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            'DB Open
            db.Open()
            '部課名が検索
            db.Fill(DataSqlCommon.GetAllUserMasSiteSql(), dtSite)
        End Using
        Return dtSite
    End Function

#End Region

#Region " SPREAD初期化を設定する "

    Public Sub SetSpreadInit()

        m_view.spdParts_Sheet1.SetColumnWidth(0, 80)
        m_view.spdParts_Sheet1.SetColumnWidth(1, 150)
        m_view.spdParts_Sheet1.SetColumnWidth(2, 75)
        m_view.spdParts_Sheet1.SetColumnWidth(3, 56)
        m_view.spdParts_Sheet1.SetColumnWidth(4, 85)
        m_view.spdParts_Sheet1.SetColumnWidth(5, 85)
        m_view.spdParts_Sheet1.Columns(0).HorizontalAlignment = Spread.CellHorizontalAlignment.Center
        m_view.spdParts_Sheet1.Columns(1).HorizontalAlignment = Spread.CellHorizontalAlignment.Left
        m_view.spdParts_Sheet1.Columns(2).HorizontalAlignment = Spread.CellHorizontalAlignment.Center
        m_view.spdParts_Sheet1.Columns(3).HorizontalAlignment = Spread.CellHorizontalAlignment.Center
        m_view.spdParts_Sheet1.Columns(4).HorizontalAlignment = Spread.CellHorizontalAlignment.Center
        m_view.spdParts_Sheet1.Columns(5).HorizontalAlignment = Spread.CellHorizontalAlignment.Center
        m_view.spdParts_Sheet1.Columns(0).Resizable = False
        m_view.spdParts_Sheet1.Columns(1).Resizable = False
        m_view.spdParts_Sheet1.Columns(2).Resizable = False
        m_view.spdParts_Sheet1.Columns(3).Resizable = False
        m_view.spdParts_Sheet1.Columns(4).Resizable = False
        m_view.spdParts_Sheet1.Columns(5).Resizable = False

        m_view.spdParts_Sheet1.Columns(6).Visible = False
        m_view.spdParts.FocusRenderer = New FarPoint.Win.Spread.MarqueeFocusIndicatorRenderer(Color.Blue, 1)

    End Sub

#End Region

#Region " 検索して、データを取得する "
    Public Sub SetDataList()

        'Comboxの検索条件を取る
        Dim strUserId As String = ""
        Dim strUserName As String = ""
        Dim strUserDepart As String = ""
        Dim strSite As String = ""
        Dim strFlag As String = ""

        strUserId = m_view.cmbUserId.Text.Trim
        strUserName = m_view.cmbUserName.Text
        strUserDepart = m_view.cmbDepart.Text
        strSite = m_view.cmbSite.Text

        '画面Open時、全てデーターが表示されます(権限1と2)。
        m_view.spdParts_Sheet1.Columns(0).DataField = "USER_ID"
        m_view.spdParts_Sheet1.Columns(1).DataField = "SHAIN_NAME"
        m_view.spdParts_Sheet1.Columns(2).DataField = "BUKA"
        m_view.spdParts_Sheet1.Columns(3).DataField = "SITE_INFO"
        m_view.spdParts_Sheet1.Columns(4).DataField = "SHISAKU_MENU_SETTEI"
        m_view.spdParts_Sheet1.Columns(5).DataField = "KENGEN_SETTEI"
        m_view.spdParts_Sheet1.Columns(6).DataField = "COMPETENT"
        m_view.spdParts.ActiveSheet.RowHeader.Columns(0).Width = 35
        m_view.spdParts_Sheet1.SetRowSizeable(m_view.spdParts_Sheet1.RowCount, False)
        m_view.spdParts_Sheet1.DataSource = GetDataListInfo(strUserId, strUserName, strUserDepart, strSite)

        SetSpreadInit()
    End Sub

    Public Function GetDataListInfo(ByVal strUserId As String, ByVal strUserName As String, ByVal strUserDepart As String, ByVal strSite As String) As DataTable
        'データーテーブル
        Dim dtData As New DataTable
        'DB Access
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
            'DB Open
            db.Open()
            '検索条件
            If Not strUserId = String.Empty Then
                db.AddParameter("@USER_ID", strUserId, DbType.AnsiString)
            End If
            If Not strUserName = String.Empty Then
                db.AddParameter("@SHAIN_NO", strUserName, DbType.AnsiString)
            End If
            If Not strUserDepart = String.Empty Then
                db.AddParameter("@BUKA_CODE", strUserDepart, DbType.AnsiString)
            End If
            If Not strSite.ToString() = String.Empty Then
                db.AddParameter("@SITE_INFO", strSite, DbType.AnsiString)
            End If
            'Spreadのデータが検索
            db.Fill(DataSqlCommon.GetLoginMasDisplaySql(strUserId, strUserName, strUserDepart, strSite.ToString()), dtData)
        End Using

        Return dtData
    End Function
#End Region

#Region " 更新画面Open "
    Public Sub InitUpdateView()

        Dim strUserCode As String = ""
        Dim strUserName As String = ""
        Dim strBuka As String = ""
        Dim strSite As String = ""
        Dim strSettei As String = ""
        Dim strKengen_Settei As String = ""
        Dim strUserkbn As String = ""

        Dim strShisaku As String = ""
        Dim strKengen As String = ""

        '一行データを取る(ユーザーId , ユーザー名,部課,サイト,設定メニュー権限,権限,ユーザー区分)
        strUserCode = m_spCom.GetValue("USER_ID", m_view.spdParts.ActiveSheet.ActiveRowIndex)
        strUserName = m_spCom.GetValue("SHAIN_NAME", m_view.spdParts.ActiveSheet.ActiveRowIndex)
        strBuka = m_spCom.GetValue("BUKA_NAME", m_view.spdParts.ActiveSheet.ActiveRowIndex)
        strSite = m_spCom.GetValue("SITE_KBN", m_view.spdParts.ActiveSheet.ActiveRowIndex)
        strSettei = m_spCom.GetValue("SHISAKU_MENU_SETTEI", m_view.spdParts.ActiveSheet.ActiveRowIndex)
        strKengen_Settei = m_spCom.GetValue("KENGEN_SETTEI", m_view.spdParts.ActiveSheet.ActiveRowIndex)
        strUserkbn = m_spCom.GetValue("USER_KBN", m_view.spdParts.ActiveSheet.ActiveRowIndex)

        '更新画面
        Dim frm5LoginMaster02COPY As New frm5LoginMaster02
        frm5LoginMaster02COPY.lblUserCode.Text = strUserCode
        frm5LoginMaster02COPY.lblUserName.Text = strUserName
        frm5LoginMaster02COPY.lblBuka.Text = strBuka
        frm5LoginMaster02COPY.lblSITE.Text = strSite
        frm5LoginMaster02COPY.lblUserkbn.Text = strUserkbn

        'Spread Combox値の設定
        frm5LoginMaster02COPY.spdParts_Sheet1.Cells(0, 1).Value = strSettei
        frm5LoginMaster02COPY.spdParts_Sheet1.Cells(1, 1).Value = strKengen_Settei

        'パスワードを設定
        Dim loginMaster As New LoginMaster02()
        loginMaster.setPsd(frm5LoginMaster02COPY.lblUserCode.Text.Trim(), frm5LoginMaster02COPY)

        'Open更新画面
        frm5LoginMaster02COPY.ShowDialog()

        '更新した、Spreadのデータが表示されます。
        SetDataList()

    End Sub
#End Region

End Class
