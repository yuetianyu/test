<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm5LoginMaster01
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。



    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm5LoginMaster01))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblCurrBukaName = New System.Windows.Forms.Label
        Me.LblDateNow = New System.Windows.Forms.Label
        Me.LblCurrUserId = New System.Windows.Forms.Label
        Me.LblCurrPGId = New System.Windows.Forms.Label
        Me.LblTimeNow = New System.Windows.Forms.Label
        Me.LblCurrPGName = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lblMsg = New System.Windows.Forms.Label
        Me.spdParts = New FarPoint.Win.Spread.FpSpread
        Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnExcelExport = New System.Windows.Forms.Button
        Me.btnEND = New System.Windows.Forms.Button
        Me.btnBACK = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cmbSite = New System.Windows.Forms.ComboBox
        Me.cmbDepart = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmbUserId = New System.Windows.Forms.ComboBox
        Me.cmbUserName = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.BackColor = System.Drawing.Color.Blue
        Me.Panel1.Controls.Add(Me.LblCurrBukaName)
        Me.Panel1.Controls.Add(Me.LblDateNow)
        Me.Panel1.Controls.Add(Me.LblCurrUserId)
        Me.Panel1.Controls.Add(Me.LblCurrPGId)
        Me.Panel1.Controls.Add(Me.LblTimeNow)
        Me.Panel1.Controls.Add(Me.LblCurrPGName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(593, 32)
        Me.Panel1.TabIndex = 54
        '
        'LblCurrBukaName
        '
        Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
        Me.LblCurrBukaName.Location = New System.Drawing.Point(368, 14)
        Me.LblCurrBukaName.Name = "LblCurrBukaName"
        Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrBukaName.TabIndex = 73
        Me.LblCurrBukaName.Text = "(部課：SKE1         )"
        '
        'LblDateNow
        '
        Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDateNow.AutoSize = True
        Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDateNow.ForeColor = System.Drawing.Color.White
        Me.LblDateNow.Location = New System.Drawing.Point(504, 1)
        Me.LblDateNow.Name = "LblDateNow"
        Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
        Me.LblDateNow.TabIndex = 60
        Me.LblDateNow.Text = "YYYY/MM/DD"
        '
        'LblCurrUserId
        '
        Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
        Me.LblCurrUserId.Location = New System.Drawing.Point(368, 1)
        Me.LblCurrUserId.Name = "LblCurrUserId"
        Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrUserId.TabIndex = 72
        Me.LblCurrUserId.Text = "(ID    ：ABCDEFGH)"
        '
        'LblCurrPGId
        '
        Me.LblCurrPGId.AutoSize = True
        Me.LblCurrPGId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrPGId.ForeColor = System.Drawing.Color.White
        Me.LblCurrPGId.Location = New System.Drawing.Point(3, 1)
        Me.LblCurrPGId.Name = "LblCurrPGId"
        Me.LblCurrPGId.Size = New System.Drawing.Size(119, 13)
        Me.LblCurrPGId.TabIndex = 59
        Me.LblCurrPGId.Text = "PG-ID：MASTER005"
        '
        'LblTimeNow
        '
        Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTimeNow.AutoSize = True
        Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTimeNow.ForeColor = System.Drawing.Color.White
        Me.LblTimeNow.Location = New System.Drawing.Point(528, 14)
        Me.LblTimeNow.Name = "LblTimeNow"
        Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
        Me.LblTimeNow.TabIndex = 59
        Me.LblTimeNow.Text = "HH:MM:DD"
        '
        'LblCurrPGName
        '
        Me.LblCurrPGName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrPGName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrPGName.ForeColor = System.Drawing.Color.Yellow
        Me.LblCurrPGName.Location = New System.Drawing.Point(194, 1)
        Me.LblCurrPGName.Name = "LblCurrPGName"
        Me.LblCurrPGName.Size = New System.Drawing.Size(158, 31)
        Me.LblCurrPGName.TabIndex = 54
        Me.LblCurrPGName.Text = "ログインマスター"
        Me.LblCurrPGName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.AutoSize = True
        Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.lblMsg)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 488)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(593, 25)
        Me.Panel3.TabIndex = 71
        '
        'lblMsg
        '
        Me.lblMsg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMsg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMsg.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(1, 1)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(589, 22)
        Me.lblMsg.TabIndex = 55
        Me.lblMsg.Text = "「ユーザーＩＤ」を入力してください。"
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdParts
        '
        Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, "
        Me.spdParts.AllowUserZoom = False
        Me.spdParts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdParts.AutoClipboard = False
        Me.spdParts.BackColor = System.Drawing.SystemColors.Control
        Me.spdParts.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.spdParts.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.spdParts.Location = New System.Drawing.Point(5, 140)
        Me.spdParts.Name = "spdParts"
        Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
        Me.spdParts.Size = New System.Drawing.Size(586, 342)
        Me.spdParts.TabIndex = 5
        Me.spdParts.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdParts.VisualStyles = FarPoint.Win.VisualStyles.Off
        '
        'spdParts_Sheet1
        '
        Me.spdParts_Sheet1.Reset()
        Me.spdParts_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spdParts_Sheet1.ColumnCount = 7
        Me.spdParts_Sheet1.RowCount = 0
        Me.spdParts_Sheet1.ActiveRowIndex = -1
        Me.spdParts_Sheet1.AllowNoteEdit = True
        Me.spdParts_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
        Me.spdParts_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "ユーザーＩＤ"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ユーザー名"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "部課"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "サイト"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "メニュー設定"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "権限設定"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "ユーザー区分"
        Me.spdParts_Sheet1.ColumnHeader.Rows.Get(0).Height = 18.0!
        Me.spdParts_Sheet1.Columns.Get(0).CellType = TextCellType1
        Me.spdParts_Sheet1.Columns.Get(0).Label = "ユーザーＩＤ"
        Me.spdParts_Sheet1.Columns.Get(0).Locked = True
        Me.spdParts_Sheet1.Columns.Get(0).Tag = "USER_ID"
        Me.spdParts_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
        Me.spdParts_Sheet1.Columns.Get(0).Width = 100.0!
        Me.spdParts_Sheet1.Columns.Get(1).CellType = TextCellType2
        Me.spdParts_Sheet1.Columns.Get(1).Label = "ユーザー名"
        Me.spdParts_Sheet1.Columns.Get(1).Locked = True
        Me.spdParts_Sheet1.Columns.Get(1).Tag = "SHAIN_NAME"
        Me.spdParts_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.spdParts_Sheet1.Columns.Get(1).Width = 130.0!
        Me.spdParts_Sheet1.Columns.Get(2).CellType = TextCellType3
        Me.spdParts_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdParts_Sheet1.Columns.Get(2).Label = "部課"
        Me.spdParts_Sheet1.Columns.Get(2).Locked = True
        Me.spdParts_Sheet1.Columns.Get(2).Tag = "BUKA_NAME"
        Me.spdParts_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.spdParts_Sheet1.Columns.Get(2).Width = 55.0!
        Me.spdParts_Sheet1.Columns.Get(3).Label = "サイト"
        Me.spdParts_Sheet1.Columns.Get(3).Locked = True
        Me.spdParts_Sheet1.Columns.Get(3).Tag = "SITE_KBN"
        Me.spdParts_Sheet1.Columns.Get(3).Width = 55.0!
        Me.spdParts_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdParts_Sheet1.Columns.Get(4).Label = "メニュー設定"
        Me.spdParts_Sheet1.Columns.Get(4).Locked = True
        Me.spdParts_Sheet1.Columns.Get(4).Tag = "SHISAKU_MENU_SETTEI"
        Me.spdParts_Sheet1.Columns.Get(4).Width = 85.0!
        Me.spdParts_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdParts_Sheet1.Columns.Get(5).Label = "権限設定"
        Me.spdParts_Sheet1.Columns.Get(5).Locked = True
        Me.spdParts_Sheet1.Columns.Get(5).Tag = "KENGEN_SETTEI"
        Me.spdParts_Sheet1.Columns.Get(5).Width = 85.0!
        Me.spdParts_Sheet1.Columns.Get(6).Label = "ユーザー区分"
        Me.spdParts_Sheet1.Columns.Get(6).Locked = True
        Me.spdParts_Sheet1.Columns.Get(6).Tag = "USER_KBN"
        Me.spdParts_Sheet1.Columns.Get(6).Width = 85.0!
        Me.spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
        Me.spdParts_Sheet1.RestrictColumns = True
        Me.spdParts_Sheet1.RestrictRows = True
        Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.spdParts_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.spdParts_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
        Me.spdParts_Sheet1.Rows.Default.Height = 16.0!
        Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        Me.spdParts.SetActiveViewport(0, -1, 0)
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.btnExcelExport)
        Me.Panel4.Controls.Add(Me.btnEND)
        Me.Panel4.Controls.Add(Me.btnBACK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 32)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(593, 33)
        Me.Panel4.TabIndex = 82
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Yellow
        Me.Label1.Image = Global.Master.My.Resources.Resources.ロゴ
        Me.Label1.Location = New System.Drawing.Point(219, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 26)
        Me.Label1.TabIndex = 83
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExcelExport
        '
        Me.btnExcelExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcelExport.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExcelExport.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnExcelExport.Location = New System.Drawing.Point(410, 6)
        Me.btnExcelExport.Name = "btnExcelExport"
        Me.btnExcelExport.Size = New System.Drawing.Size(110, 24)
        Me.btnExcelExport.TabIndex = 75
        Me.btnExcelExport.Text = "EXCEL出力"
        Me.ToolTip1.SetToolTip(Me.btnExcelExport, "一覧表の内容を「EXCEL」へ出力します。")
        Me.btnExcelExport.UseVisualStyleBackColor = True
        '
        'btnEND
        '
        Me.btnEND.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEND.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEND.Location = New System.Drawing.Point(5, 6)
        Me.btnEND.Name = "btnEND"
        Me.btnEND.Size = New System.Drawing.Size(149, 24)
        Me.btnEND.TabIndex = 50
        Me.btnEND.Text = "アプリケーション終了"
        Me.ToolTip1.SetToolTip(Me.btnEND, "新試作手配システムを終了します。")
        Me.btnEND.UseVisualStyleBackColor = True
        '
        'btnBACK
        '
        Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBACK.Location = New System.Drawing.Point(526, 6)
        Me.btnBACK.Name = "btnBACK"
        Me.btnBACK.Size = New System.Drawing.Size(64, 24)
        Me.btnBACK.TabIndex = 80
        Me.btnBACK.Text = "戻る"
        Me.btnBACK.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.cmbSite)
        Me.Panel2.Controls.Add(Me.cmbDepart)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.cmbUserId)
        Me.Panel2.Controls.Add(Me.cmbUserName)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 65)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(593, 73)
        Me.Panel2.TabIndex = 55
        '
        'cmbSite
        '
        Me.cmbSite.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSite.FormattingEnabled = True
        Me.cmbSite.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbSite.Location = New System.Drawing.Point(409, 45)
        Me.cmbSite.Name = "cmbSite"
        Me.cmbSite.Size = New System.Drawing.Size(98, 23)
        Me.cmbSite.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cmbSite, "１．群馬工場、２．三鷹工場です。")
        '
        'cmbDepart
        '
        Me.cmbDepart.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbDepart.FormattingEnabled = True
        Me.cmbDepart.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbDepart.Location = New System.Drawing.Point(303, 45)
        Me.cmbDepart.Name = "cmbDepart"
        Me.cmbDepart.Size = New System.Drawing.Size(100, 23)
        Me.cmbDepart.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbDepart, "あいまい検索（前方一致）が可能です。")
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(413, 27)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 15)
        Me.Label11.TabIndex = 88
        Me.Label11.Text = "サイト："
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(5, 5)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(83, 15)
        Me.Label12.TabIndex = 81
        Me.Label12.Text = "《検索条件》"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 15)
        Me.Label6.TabIndex = 82
        Me.Label6.Text = "ユーザーＩＤ："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(306, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(45, 15)
        Me.Label9.TabIndex = 86
        Me.Label9.Text = "部課："
        '
        'cmbUserId
        '
        Me.cmbUserId.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUserId.FormattingEnabled = True
        Me.cmbUserId.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbUserId.Location = New System.Drawing.Point(15, 45)
        Me.cmbUserId.Name = "cmbUserId"
        Me.cmbUserId.Size = New System.Drawing.Size(98, 23)
        Me.cmbUserId.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbUserId, "あいまい検索（前方一致）が可能です。")
        '
        'cmbUserName
        '
        Me.cmbUserName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUserName.FormattingEnabled = True
        Me.cmbUserName.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.cmbUserName.Location = New System.Drawing.Point(118, 45)
        Me.cmbUserName.Name = "cmbUserName"
        Me.cmbUserName.Size = New System.Drawing.Size(180, 23)
        Me.cmbUserName.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.cmbUserName, "あいまい検索（前方一致）が可能です。")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(115, 27)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 15)
        Me.Label8.TabIndex = 84
        Me.Label8.Text = "ユーザー名："
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'frm5LoginMaster01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(593, 513)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.spdParts)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frm5LoginMaster01"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LblCurrPGName As System.Windows.Forms.Label
    Friend WithEvents LblCurrPGId As System.Windows.Forms.Label
    Friend WithEvents LblDateNow As System.Windows.Forms.Label
    Friend WithEvents LblTimeNow As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
    Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
    Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmbSite As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDepart As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbUserId As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUserName As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnEND As System.Windows.Forms.Button
    Friend WithEvents btnBACK As System.Windows.Forms.Button
    Friend WithEvents btnExcelExport As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
