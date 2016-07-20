Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports EBom.Data
Imports ShisakuCommon
Imports Microsoft.Win32
Imports FarPoint.Win.Spread.CellType
Imports Microsoft.Office.Interop
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd

''' <summary>
''' 部品構成表示画面
''' </summary>
''' <remarks></remarks>

Public Class frm6KaihatufugouMaster01

    Private isLoad As Boolean = True
    Public strShakeiComm As String = ""
    Public strFugoComm As String = ""
    Public strPhaseComm As String = ""
    Public strNameComm As String = ""
    Private m_spCom As SpreadCommon

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)

    End Sub

    Private Sub txtListCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub frm6KaihatufugouMaster01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.KeyPreview = True
            loadProcess()
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)
            isLoad = False

        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try
        cmbShakei.Focus()
    End Sub

    Public Sub loadProcess()
        spdParts.FocusRenderer = New FarPoint.Win.Spread.MarqueeFocusIndicatorRenderer(Color.Blue, 1)
        '日付の設定
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        'コンボボックスデータソースを取得
        getcmbData()
        'データソースを取得
        searchSpdData()
        'スプレッドスタイルの設定
        setspdStyle()
        'メッセージを表示する
        Label10.Text = T0002

    End Sub

    Private Sub setspdStyle()
        Dim info As New Spread.StyleInfo()
        Dim i As Integer = 0
        ' 列の幅を設定します。
        spdParts.ActiveSheet.RowHeader.Columns(0).Width = 35
        spdParts_Sheet1.SetColumnWidth(0, 39)
        spdParts_Sheet1.SetColumnWidth(1, 40)
        spdParts_Sheet1.SetColumnWidth(2, 72)
        spdParts_Sheet1.SetColumnWidth(3, 72)
        spdParts_Sheet1.SetColumnWidth(4, 200)
        spdParts_Sheet1.Columns(0).HorizontalAlignment = Spread.CellHorizontalAlignment.Center
        spdParts_Sheet1.Columns(1).HorizontalAlignment = Spread.CellHorizontalAlignment.Center
        spdParts_Sheet1.Columns(2).HorizontalAlignment = Spread.CellHorizontalAlignment.Center
        spdParts_Sheet1.Columns(3).HorizontalAlignment = Spread.CellHorizontalAlignment.Center
        spdParts_Sheet1.Columns(4).HorizontalAlignment = Spread.CellHorizontalAlignment.Left

        spdParts_Sheet1.Columns(0).Resizable = False
        spdParts_Sheet1.Columns(1).Resizable = False
        spdParts_Sheet1.Columns(2).Resizable = False
        spdParts_Sheet1.Columns(3).Resizable = False
        spdParts_Sheet1.Columns(4).Resizable = False

        spdParts_Sheet1.Columns(5).Visible = False
        Dim chkType As New CheckBoxCellType()
        m_spCom = New SpreadCommon(spdParts)
        spdParts_Sheet1.Columns(0).CellType = chkType
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            m_spCom.GetCell("Shakei", i).Locked = True
            m_spCom.GetCell("Fugo", i).Locked = True
            m_spCom.GetCell("Phase", i).Locked = True
            m_spCom.GetCell("Event", i).Locked = True
        Next
        spdParts_Sheet1.SetRowSizeable(spdParts_Sheet1.RowCount, False)

    End Sub

    Private Sub getcmbData()
        Dim dtShakei As DataTable = New DataTable()
        Dim dtFugo As DataTable = New DataTable()
        Dim dtPhase As DataTable = New DataTable()
        Dim dtName As DataTable = New DataTable()
        Dim dtRow As DataRow
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            db.Fill(DataSqlCommon.GetKaihatufugouComboSql("Shakei"), dtShakei)
            ' 車系コンボボックスにデータセットを接続します。
            dtRow = dtShakei.NewRow
            dtRow("Shakei") = ""
            dtShakei.Rows.InsertAt(dtRow, 0)
            cmbShakei.DataSource = dtShakei
            cmbShakei.DisplayMember = "Shakei"
            cmbShakei.ValueMember = "Shakei"

            db.Fill(DataSqlCommon.GetKaihatufugouComboSql("Fugo"), dtFugo)
            ' 符号コンボボックスにデータセットを接続します。
            dtRow = dtFugo.NewRow
            dtRow("Fugo") = ""
            dtFugo.Rows.InsertAt(dtRow, 0)
            cmbFugo.DataSource = dtFugo
            cmbFugo.DisplayMember = "Fugo"
            cmbFugo.ValueMember = "Fugo"

            db.Fill(DataSqlCommon.GetKaihatufugouComboSql("Phase"), dtPhase)
            ' フェーズコンボボックスにデータセットを接続します。
            dtRow = dtPhase.NewRow
            dtRow("Phase") = ""
            dtPhase.Rows.InsertAt(dtRow, 0)
            cmbPhase.DataSource = dtPhase
            cmbPhase.DisplayMember = "Phase"
            cmbPhase.ValueMember = "Phase"

            db.Fill(DataSqlCommon.GetKaihatufugouComboSql("Name"), dtName)
            ' イベントコンボボックスにデータセットを接続します。
            dtRow = dtName.NewRow
            dtRow("Name") = ""
            dtName.Rows.InsertAt(dtRow, 0)
            cmbName.DataSource = dtName
            cmbName.DisplayMember = "Name"
            cmbName.ValueMember = "Name"

        End Using
    End Sub

    Private Sub getspdData(ByVal strShakei As String, ByVal strFugo As String, ByVal strPhase As String, ByVal strName As String)
        Dim dtList As DataTable = New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            If Not strShakei = String.Empty Then
                db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            End If
            If Not strFugo = String.Empty Then
                db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
            End If
            If Not strPhase = String.Empty Then
                db.AddParameter("@SHISAKU_EVENT_PHASE", strPhase, DbType.AnsiString)
            End If
            If Not strName = String.Empty Then
                db.AddParameter("@SHISAKU_EVENT_PHASE_NAME", strName, DbType.AnsiString)
            End If
            db.Fill(DataSqlCommon.GetKaihatufugouListSql(strShakei, strFugo, strPhase, strName), dtList)
            ' シートにデータセットを接続します。
            spdParts.ActiveSheet.DataSource = dtList

        End Using
        ' 行列の移動を不可にします。
        spdParts.AllowColumnMove = False
        spdParts.AllowRowMove = False
    End Sub
    ' Excelファイルへシートのデータを保存します。
    Sub SaveExcelFile(ByVal filename As String)
        Dim ret As Boolean
        ret = spdParts.SaveExcel(filename)
    End Sub
    ' Spread XML ファイルへシートのデータを保存します。
    Sub SaveSpreadFile(ByVal filename As String)
        Dim ret As Boolean
        ret = spdParts.Save(filename, False)
    End Sub

    Private Sub spdParts_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick
        m_spCom = New SpreadCommon(spdParts)
        strShakeiComm = cmbShakei.Text
        strFugoComm = cmbFugo.Text
        strPhaseComm = cmbPhase.Text
        strNameComm = cmbName.Text
        frm6KaihatufugouMaster03.lblShakei.Text = m_spCom.GetValue("Shakei", e.Row)
        frm6KaihatufugouMaster03.lblFugo.Text = m_spCom.GetValue("Fugo", e.Row)

        frm6KaihatufugouMaster03.ShowDialog()
        loadProcess()
        cmbShakei.Text = strShakeiComm
        cmbFugo.Text = strFugoComm
        cmbPhase.Text = strPhaseComm
        cmbName.Text = strNameComm
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        frm01Kakunin.lblKakunin.Text = T0001
        frm01Kakunin.lblKakunin2.Text = ""
        frm6Para = "btnDel"
        frm01Kakunin.ShowDialog()
        Select Case frm6ParaModori
            Case "1"
                Me.Close()
            Case "2"
                Me.Close()
            Case ""
                frm01Kakunin.Close()
                Exit Sub
            Case Else
        End Select
        '削除確認
        If delCheck() = RESULT.OK Then
            delFromDB()
        Else
            ComFunc.ShowErrMsgBox(E0003)
        End If
    End Sub

    ''' <summary>
    ''' 一覧削除時のチェック
    ''' </summary>
    ''' <returns>Ture:チェックしている;False:チェックしていない</returns>
    ''' <remarks>一覧表でチェックボックスをチェックしているかどうかチェックする</remarks>
    Private Function delCheck()
        m_spCom = New SpreadCommon(spdParts)
        Dim i As Integer = 0
        Dim intCount As Integer = 0
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            If m_spCom.GetValue("Delete", i) = True Then
                intCount = intCount + 1
            End If
        Next
        If intCount = 0 Then
            Return RESULT.NG
        Else
            Return RESULT.OK
        End If
    End Function

    Private Sub delFromDB()
        Dim i As Integer = 0
        Dim strShakei As String = ""
        Dim strFugo As String = ""
        Dim strNo As String = ""
        Dim intNG As Integer = 0
        Dim intOK As Integer = 0
        Dim dtOK As New DataTable
        dtOK.Columns.Add("strShakei")
        dtOK.Columns.Add("strFugo")
        dtOK.Columns.Add("strNo")
        m_spCom = New SpreadCommon(spdParts)
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            ShisakuFormUtil.initlColor(m_spCom.GetCell("Delete", i))
            ShisakuFormUtil.initlColor(m_spCom.GetCell("Shakei", i))
            ShisakuFormUtil.initlColor(m_spCom.GetCell("Fugo", i))
            ShisakuFormUtil.initlColor(m_spCom.GetCell("Phase", i))
            ShisakuFormUtil.initlColor(m_spCom.GetCell("Event", i))

            If spdParts_Sheet1.Cells(i, 0).Value = True Then
                strShakei = m_spCom.GetValue("Shakei", i)
                strFugo = m_spCom.GetValue("Fugo", i)
                strNo = m_spCom.GetValue("Id", i)
                If checkExist(strShakei, strFugo, strNo) = RESULT.OK Then
                    '登録済み場合
                    intNG = intNG + 1
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Delete", i))
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Shakei", i))
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Fugo", i))
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Event", i))
                Else
                    dtOK.Rows.Add(strShakei, strFugo, strNo)
                End If
            End If
        Next
        If intNG > 0 Then
            ComFunc.ShowErrMsgBox(E0004)
        Else
            For i = 0 To dtOK.Rows.Count - 1
                deleteProcess(dtOK.Rows(i)("strShakei").ToString, dtOK.Rows(i)("strFugo").ToString, dtOK.Rows(i)("strNo").ToString)
            Next
            searchSpdData()
        End If
    End Sub

    Private Sub deleteProcess(ByVal strShakei As String, ByVal strFugo As String, ByVal strNo As String)
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            db.BeginTransaction()

            db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
            db.AddParameter("@HYOJIJUN_NO", strNo, DbType.AnsiString)

            db.ExecuteNonQuery(DataSqlCommon.GetKaihatufugouDeleteSql(strShakei, strFugo, strNo))
            db.Commit()
        End Using
    End Sub

    ''' <summary>
    ''' イベントマスタに存在チェック
    ''' </summary>
    ''' <returns>Ture:イベントに存在する(登録済みと判断する);False:イベントに存在しない(未登録と判断する)</returns>
    '''<param name="strShakei">キーの1－車系</param>
    '''<param name="strFugo">キーの2－符号</param>
    '''<param name="strHYOJIJUN_NO">キーの3－連番</param>
    ''' <remarks>該当データはイベントマスタに存在するかどうかのチェック</remarks>
    Private Function checkExist(ByVal strShakei As String, ByVal strFugo As String, ByVal strHYOJIJUN_NO As String)
        Dim dtList As DataTable = New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            If Not strShakei = String.Empty Then
                db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            End If
            If Not strFugo = String.Empty Then
                db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
            End If
            If Not strHYOJIJUN_NO = String.Empty Then
                db.AddParameter("@HYOJIJUN_NO", strHYOJIJUN_NO, DbType.AnsiString)
            End If
            db.Fill(DataSqlCommon.GetIbentoCheckSql(strShakei, strFugo, strHYOJIJUN_NO), dtList)
            If dtList.Rows.Count > 0 Then
                Return RESULT.OK '登録済みと判断する
            Else
                Return RESULT.NG
            End If

        End Using
    End Function

    Private Sub btnNEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNEW.Click
        strShakeiComm = cmbShakei.Text
        strFugoComm = cmbFugo.Text
        strPhaseComm = cmbPhase.Text
        strNameComm = cmbName.Text
        Dim b As New frm6KaihatufugouMaster02
        b.a = Me
        b.ShowDialog()
        cmbShakei.Text = strShakeiComm
        cmbFugo.Text = strFugoComm
        cmbPhase.Text = strPhaseComm
        cmbName.Text = strNameComm
    End Sub

    Private Sub btnExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelExport.Click
        'Spread spdExcel 作成
        Dim spdExcel As FarPoint.Win.Spread.FpSpread = New FarPoint.Win.Spread.FpSpread
        Dim dtRow As DataRow
        'Spread spdExcelのsheetがadd
        spdExcel.Sheets.Count = 1
        'Spread sheet 名
        Dim dtExcel As New DataTable
        Dim strShakei As String = cmbShakei.Text
        Dim strFugo As String = cmbFugo.Text
        Dim strPhase As String = cmbPhase.Text
        Dim strName As String = cmbName.Text
        Dim Shisakucom As New ExcelCommon()

        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            If Not strShakei = String.Empty Then
                db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            End If
            If Not strFugo = String.Empty Then
                db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
            End If
            If Not strPhase = String.Empty Then
                db.AddParameter("@SHISAKU_EVENT_PHASE", strPhase, DbType.AnsiString)
            End If
            If Not strName = String.Empty Then
                db.AddParameter("@SHISAKU_EVENT_PHASE_NAME", strName, DbType.AnsiString)
            End If
            db.Fill(DataSqlCommon.GetKaihatufugouExcelSql(strShakei, strFugo, strPhase, strName), dtExcel)

            dtRow = dtExcel.NewRow
            dtRow("SHISAKU_SHAKEI_CODE") = "車系"
            dtRow("SHISAKU_KAIHATSU_FUGO") = "開発符号"
            dtRow("SHISAKU_EVENT_PHASE") = "フェーズ"
            dtRow("SHISAKU_EVENT_PHASE_NAME") = "イベント"
            dtExcel.Rows.InsertAt(dtRow, 0)
            spdExcel.ActiveSheet.DataSource = dtExcel
            spdExcel.ActiveSheet.SetColumnWidth(0, 40)
            spdExcel.ActiveSheet.SetColumnWidth(1, 72)
            spdExcel.ActiveSheet.SetColumnWidth(2, 72)
            spdExcel.ActiveSheet.SetColumnWidth(3, 105)
            ExcelCommon.SaveExcelFile("車系／開発符号マスター " + Now.ToString("MMdd") + Now.ToString("HHmm"), spdExcel, "Shakei")
        End Using
    End Sub

    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
        Application.Exit()
        System.Environment.Exit(0)
    End Sub

    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        Me.Close()
    End Sub

    Private Sub searchSpdData()
        Dim strShakei = cmbShakei.Text
        Dim strFugo = cmbFugo.Text
        Dim strPhase = cmbPhase.Text
        Dim strName = cmbName.Text
        Dim strShakeiRow As String = ""
        Dim strFugoRow As String = ""
        Dim strNoRow As String = ""
        Dim i As Integer = 0
        m_spCom = New SpreadCommon(spdParts)
        If strShakei = String.Empty And strFugo = String.Empty And strPhase = String.Empty And strName = String.Empty Then
            getspdData("", "", "", "")
        Else
            getspdData(strShakei, strFugo, strPhase, strName)
        End If
        'スプレッドスタイルの設定
        setspdStyle()
        'イベント登録済みのデータを使用不可にする。
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            strShakeiRow = m_spCom.GetValue("Shakei", i)
            strFugoRow = m_spCom.GetValue("Fugo", i)
            strNoRow = m_spCom.GetValue("Id", i)
            If checkExist(strShakeiRow, strFugoRow, strNoRow) = RESULT.OK Then
                spdParts_Sheet1.Rows(i).Locked = True
            Else
                spdParts_Sheet1.Rows(i).Locked = False
            End If
        Next
    End Sub

    Private Sub cmb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbShakei.TextChanged, cmbPhase.TextChanged, cmbName.TextChanged, cmbFugo.TextChanged
        If isLoad = False Then
            searchSpdData()
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub

    Private Sub Controls_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbShakei.KeyDown, cmbPhase.KeyDown, cmbName.KeyDown, cmbFugo.KeyDown
        ShisakuFormUtil.DelKeyDown(sender, e)
    End Sub
End Class