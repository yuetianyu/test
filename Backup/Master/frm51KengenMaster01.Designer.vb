<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm51KengenMaster01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm51KengenMaster01))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblCurrBukaName = New System.Windows.Forms.Label
        Me.LblDateNow = New System.Windows.Forms.Label
        Me.LblCurrUserId = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.LblTimeNow = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.spdInfo = New FarPoint.Win.Spread.FpSpread
        Me.spdInfo_Sheet1 = New FarPoint.Win.Spread.SheetView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cmbCompetent = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.cmbSite = New System.Windows.Forms.ComboBox
        Me.cmbBuka = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmbUserId = New System.Windows.Forms.ComboBox
        Me.cmbUserName = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnExcelExport = New System.Windows.Forms.Button
        Me.btnEND = New System.Windows.Forms.Button
        Me.btnBACK = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.spdInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdInfo_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
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
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.LblTimeNow)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1021, 32)
        Me.Panel1.TabIndex = 54
        '
        'LblCurrBukaName
        '
        Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
        Me.LblCurrBukaName.Location = New System.Drawing.Point(796, 14)
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
        Me.LblDateNow.Location = New System.Drawing.Point(932, 1)
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
        Me.LblCurrUserId.Location = New System.Drawing.Point(796, 1)
        Me.LblCurrUserId.Name = "LblCurrUserId"
        Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrUserId.TabIndex = 72
        Me.LblCurrUserId.Text = "(ID    ：ABCDEFGH)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 13)
        Me.Label2.TabIndex = 59
        Me.Label2.Text = "PG-ID：MASTER051"
        '
        'LblTimeNow
        '
        Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTimeNow.AutoSize = True
        Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTimeNow.ForeColor = System.Drawing.Color.White
        Me.LblTimeNow.Location = New System.Drawing.Point(956, 14)
        Me.LblTimeNow.Name = "LblTimeNow"
        Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
        Me.LblTimeNow.TabIndex = 59
        Me.LblTimeNow.Text = "HH:MM:DD"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Yellow
        Me.Label4.Location = New System.Drawing.Point(194, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(586, 31)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "権限マスター"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.AutoSize = True
        Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 494)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1021, 25)
        Me.Panel3.TabIndex = 71
        '
        'Label10
        '
        Me.Label10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(1, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1017, 22)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "ダブルクリックで選択してください。"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdInfo
        '
        Me.spdInfo.AccessibleDescription = "spdInfo, Sheet1, Row 0, Column 0, "
        Me.spdInfo.AllowUserZoom = False
        Me.spdInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdInfo.AutoClipboard = False
        Me.spdInfo.BackColor = System.Drawing.SystemColors.Control
        Me.spdInfo.Font = New System.Drawing.Font("MS Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdInfo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.spdInfo.Location = New System.Drawing.Point(5, 144)
        Me.spdInfo.Name = "spdInfo"
        Me.spdInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.spdInfo.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdInfo_Sheet1})
        Me.spdInfo.Size = New System.Drawing.Size(1014, 344)
        Me.spdInfo.TabIndex = 90
        Me.spdInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdInfo.VisualStyles = FarPoint.Win.VisualStyles.Off
        '
        'spdInfo_Sheet1
        '
        Me.spdInfo_Sheet1.Reset()
        Me.spdInfo_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spdInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spdInfo_Sheet1.ColumnCount = 12
        Me.spdInfo_Sheet1.RowCount = 0
        Me.spdInfo_Sheet1.ActiveRowIndex = -1
        Me.spdInfo_Sheet1.AllowNoteEdit = True
        Me.spdInfo_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
        Me.spdInfo_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
        Me.spdInfo_Sheet1.AutoGenerateColumns = False
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "ユーザーＩＤ"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ユーザー名"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "部課"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "サイト"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "区分"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "メニュー"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "承認押下"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "開発符号マスタ"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "承認通知"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "日程管理ツール"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 10).ColumnSpan = 2
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "現調品手配システム"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = " "
        Me.spdInfo_Sheet1.ColumnHeader.Rows.Get(0).Height = 18.0!
        Me.spdInfo_Sheet1.Columns.Get(0).CellType = TextCellType1
        Me.spdInfo_Sheet1.Columns.Get(0).Label = "ユーザーＩＤ"
        Me.spdInfo_Sheet1.Columns.Get(0).Locked = True
        Me.spdInfo_Sheet1.Columns.Get(0).Tag = "BUHIN_NO"
        Me.spdInfo_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
        Me.spdInfo_Sheet1.Columns.Get(0).Width = 100.0!
        Me.spdInfo_Sheet1.Columns.Get(1).CellType = TextCellType2
        Me.spdInfo_Sheet1.Columns.Get(1).Label = "ユーザー名"
        Me.spdInfo_Sheet1.Columns.Get(1).Locked = True
        Me.spdInfo_Sheet1.Columns.Get(1).Tag = "HOJO_NAME"
        Me.spdInfo_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.spdInfo_Sheet1.Columns.Get(1).Width = 130.0!
        Me.spdInfo_Sheet1.Columns.Get(2).CellType = TextCellType3
        Me.spdInfo_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdInfo_Sheet1.Columns.Get(2).Label = "部課"
        Me.spdInfo_Sheet1.Columns.Get(2).Locked = True
        Me.spdInfo_Sheet1.Columns.Get(2).Tag = "UPDATED_DATETIME"
        Me.spdInfo_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.spdInfo_Sheet1.Columns.Get(2).Width = 55.0!
        Me.spdInfo_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdInfo_Sheet1.Columns.Get(3).Label = "サイト"
        Me.spdInfo_Sheet1.Columns.Get(3).Locked = True
        Me.spdInfo_Sheet1.Columns.Get(3).Width = 55.0!
        Me.spdInfo_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdInfo_Sheet1.Columns.Get(4).Label = "区分"
        Me.spdInfo_Sheet1.Columns.Get(4).Locked = True
        Me.spdInfo_Sheet1.Columns.Get(4).Width = 55.0!
        Me.spdInfo_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdInfo_Sheet1.Columns.Get(5).Label = "メニュー"
        Me.spdInfo_Sheet1.Columns.Get(5).Locked = True
        Me.spdInfo_Sheet1.Columns.Get(5).Width = 58.0!
        Me.spdInfo_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdInfo_Sheet1.Columns.Get(6).Label = "承認押下"
        Me.spdInfo_Sheet1.Columns.Get(6).Locked = True
        Me.spdInfo_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdInfo_Sheet1.Columns.Get(7).Label = "開発符号マスタ"
        Me.spdInfo_Sheet1.Columns.Get(7).Locked = True
        Me.spdInfo_Sheet1.Columns.Get(7).Width = 116.0!
        Me.spdInfo_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdInfo_Sheet1.Columns.Get(8).Label = "承認通知"
        Me.spdInfo_Sheet1.Columns.Get(8).Locked = True
        Me.spdInfo_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdInfo_Sheet1.Columns.Get(9).Label = "日程管理ツール"
        Me.spdInfo_Sheet1.Columns.Get(9).Locked = True
        Me.spdInfo_Sheet1.Columns.Get(9).Width = 100.0!
        Me.spdInfo_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdInfo_Sheet1.Columns.Get(10).Label = "現調品手配システム"
        Me.spdInfo_Sheet1.Columns.Get(10).Locked = True
        Me.spdInfo_Sheet1.Columns.Get(10).Width = 80.0!
        Me.spdInfo_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdInfo_Sheet1.Columns.Get(11).Label = " "
        Me.spdInfo_Sheet1.Columns.Get(11).Locked = True
        Me.spdInfo_Sheet1.Columns.Get(11).Width = 80.0!
        Me.spdInfo_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
        Me.spdInfo_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.spdInfo_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.spdInfo_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
        Me.spdInfo_Sheet1.Rows.Default.Height = 16.0!
        Me.spdInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        Me.spdInfo.SetActiveViewport(0, -1, 0)
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.cmbCompetent)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.cmbSite)
        Me.Panel2.Controls.Add(Me.cmbBuka)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.cmbUserId)
        Me.Panel2.Controls.Add(Me.cmbUserName)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 62)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1021, 73)
        Me.Panel2.TabIndex = 83
        '
        'cmbCompetent
        '
        Me.cmbCompetent.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCompetent.FormattingEnabled = True
        Me.cmbCompetent.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbCompetent.Items.AddRange(New Object() {"", "1", "2"})
        Me.cmbCompetent.Location = New System.Drawing.Point(477, 45)
        Me.cmbCompetent.Name = "cmbCompetent"
        Me.cmbCompetent.Size = New System.Drawing.Size(60, 23)
        Me.cmbCompetent.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbCompetent, "１．設計課、２．試作計画１課です。")
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(475, 27)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(45, 15)
        Me.Label13.TabIndex = 90
        Me.Label13.Text = "区分："
        '
        'cmbSite
        '
        Me.cmbSite.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSite.FormattingEnabled = True
        Me.cmbSite.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbSite.Items.AddRange(New Object() {"", "1", "2"})
        Me.cmbSite.Location = New System.Drawing.Point(411, 45)
        Me.cmbSite.Name = "cmbSite"
        Me.cmbSite.Size = New System.Drawing.Size(60, 23)
        Me.cmbSite.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cmbSite, "１．群馬工場、２．三鷹工場です。")
        '
        'cmbBuka
        '
        Me.cmbBuka.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBuka.FormattingEnabled = True
        Me.cmbBuka.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbBuka.Items.AddRange(New Object() {"", "SKE1", "設計"})
        Me.cmbBuka.Location = New System.Drawing.Point(305, 45)
        Me.cmbBuka.Name = "cmbBuka"
        Me.cmbBuka.Size = New System.Drawing.Size(100, 23)
        Me.cmbBuka.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbBuka, "あいまい検索（前方一致）が可能です。")
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(409, 27)
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
        Me.Label9.Location = New System.Drawing.Point(303, 27)
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
        Me.cmbUserId.Items.AddRange(New Object() {"", "A123456", "A123457", "A123458", "A123459", "A123460", "A123461", "A123462", "A123463", "A123464", "A123465", "A123466"})
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
        Me.cmbUserName.Items.AddRange(New Object() {"", "××　×××", "××　××", "××　××", "××　××", "××　××", "××　××", "××　××", "××　××", "××　××", "××　××", "××　××"})
        Me.cmbUserName.Location = New System.Drawing.Point(119, 45)
        Me.cmbUserName.Name = "cmbUserName"
        Me.cmbUserName.Size = New System.Drawing.Size(180, 23)
        Me.cmbUserName.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.cmbUserName, "あいまい検索（前方一致）が可能です。")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(116, 27)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 15)
        Me.Label8.TabIndex = 84
        Me.Label8.Text = "ユーザー名："
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
        Me.Panel4.Size = New System.Drawing.Size(1021, 30)
        Me.Panel4.TabIndex = 84
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Yellow
        Me.Label1.Image = Global.Master.My.Resources.Resources.ロゴ
        Me.Label1.Location = New System.Drawing.Point(247, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 26)
        Me.Label1.TabIndex = 82
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExcelExport
        '
        Me.btnExcelExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcelExport.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExcelExport.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnExcelExport.Location = New System.Drawing.Point(838, 3)
        Me.btnExcelExport.Name = "btnExcelExport"
        Me.btnExcelExport.Size = New System.Drawing.Size(110, 24)
        Me.btnExcelExport.TabIndex = 57
        Me.btnExcelExport.Text = "EXCEL出力"
        Me.ToolTip1.SetToolTip(Me.btnExcelExport, "一覧表の内容を「EXCEL」へ出力します。")
        Me.btnExcelExport.UseVisualStyleBackColor = True
        '
        'btnEND
        '
        Me.btnEND.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEND.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEND.Location = New System.Drawing.Point(5, 3)
        Me.btnEND.Name = "btnEND"
        Me.btnEND.Size = New System.Drawing.Size(149, 24)
        Me.btnEND.TabIndex = 54
        Me.btnEND.Text = "アプリケーション終了"
        Me.ToolTip1.SetToolTip(Me.btnEND, "新試作手配システムを終了します。")
        Me.btnEND.UseVisualStyleBackColor = True
        '
        'btnBACK
        '
        Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBACK.Location = New System.Drawing.Point(954, 3)
        Me.btnBACK.Name = "btnBACK"
        Me.btnBACK.Size = New System.Drawing.Size(64, 24)
        Me.btnBACK.TabIndex = 55
        Me.btnBACK.Text = "戻る"
        Me.btnBACK.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'frm51KengenMaster01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1021, 519)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.spdInfo)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frm51KengenMaster01"
        Me.Text = "新試作手配システム Ver 1.00"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.spdInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdInfo_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LblDateNow As System.Windows.Forms.Label
    Friend WithEvents LblTimeNow As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
    Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
    Friend WithEvents spdInfo As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spdInfo_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmbSite As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBuka As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbUserId As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUserName As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnExcelExport As System.Windows.Forms.Button
    Friend WithEvents btnEND As System.Windows.Forms.Button
    Friend WithEvents btnBACK As System.Windows.Forms.Button
    Friend WithEvents cmbCompetent As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
