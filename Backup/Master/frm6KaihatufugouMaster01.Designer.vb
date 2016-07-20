<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm6KaihatufugouMaster01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm6KaihatufugouMaster01))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblCurrBukaName = New System.Windows.Forms.Label
        Me.LblDateNow = New System.Windows.Forms.Label
        Me.LblCurrUserId = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.LblTimeNow = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.spdParts = New FarPoint.Win.Spread.FpSpread
        Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label11 = New System.Windows.Forms.Label
        Me.cmbName = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmbFugo = New System.Windows.Forms.ComboBox
        Me.cmbPhase = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmbShakei = New System.Windows.Forms.ComboBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnEND = New System.Windows.Forms.Button
        Me.btnBACK = New System.Windows.Forms.Button
        Me.btnExcelExport = New System.Windows.Forms.Button
        Me.btnDel = New System.Windows.Forms.Button
        Me.btnNEW = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(608, 32)
        Me.Panel1.TabIndex = 54
        '
        'LblCurrBukaName
        '
        Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
        Me.LblCurrBukaName.Location = New System.Drawing.Point(380, 14)
        Me.LblCurrBukaName.Name = "LblCurrBukaName"
        Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrBukaName.TabIndex = 73
        '
        'LblDateNow
        '
        Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDateNow.AutoSize = True
        Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDateNow.ForeColor = System.Drawing.Color.White
        Me.LblDateNow.Location = New System.Drawing.Point(516, 1)
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
        Me.LblCurrUserId.Location = New System.Drawing.Point(380, 1)
        Me.LblCurrUserId.Name = "LblCurrUserId"
        Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrUserId.TabIndex = 72
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
        Me.Label2.Text = "PG-ID：MASTER006"
        '
        'LblTimeNow
        '
        Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTimeNow.AutoSize = True
        Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTimeNow.ForeColor = System.Drawing.Color.White
        Me.LblTimeNow.Location = New System.Drawing.Point(540, 14)
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
        Me.Label4.Location = New System.Drawing.Point(184, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(188, 31)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "車系／開発符号マスター"
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
        Me.Panel3.Size = New System.Drawing.Size(608, 25)
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
        Me.Label10.Location = New System.Drawing.Point(3, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(600, 22)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "「開発符号」を入力してください。"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdParts.Location = New System.Drawing.Point(16, 172)
        Me.spdParts.Name = "spdParts"
        Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
        Me.spdParts.Size = New System.Drawing.Size(580, 285)
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
        Me.spdParts_Sheet1.ColumnCount = 6
        Me.spdParts_Sheet1.RowCount = 0
        Me.spdParts_Sheet1.ActiveRowIndex = -1
        Me.spdParts_Sheet1.AllowNoteEdit = True
        Me.spdParts_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
        Me.spdParts_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "削除"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "車系"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "開発符号"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "フェーズ"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "イベント"
        Me.spdParts_Sheet1.ColumnHeader.Rows.Get(0).Height = 18.0!
        Me.spdParts_Sheet1.Columns.Get(0).CellType = TextCellType1
        Me.spdParts_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdParts_Sheet1.Columns.Get(0).Label = "削除"
        Me.spdParts_Sheet1.Columns.Get(0).Locked = False
        Me.spdParts_Sheet1.Columns.Get(0).Tag = "Delete"
        Me.spdParts_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
        Me.spdParts_Sheet1.Columns.Get(0).Width = 39.0!
        Me.spdParts_Sheet1.Columns.Get(1).Label = "車系"
        Me.spdParts_Sheet1.Columns.Get(1).Locked = True
        Me.spdParts_Sheet1.Columns.Get(1).Tag = "Shakei"
        Me.spdParts_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.spdParts_Sheet1.Columns.Get(1).Width = 100.0!
        Me.spdParts_Sheet1.Columns.Get(2).CellType = TextCellType2
        Me.spdParts_Sheet1.Columns.Get(2).Label = "開発符号"
        Me.spdParts_Sheet1.Columns.Get(2).Locked = True
        Me.spdParts_Sheet1.Columns.Get(2).Tag = "Fugo"
        Me.spdParts_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.spdParts_Sheet1.Columns.Get(2).Width = 125.0!
        Me.spdParts_Sheet1.Columns.Get(3).CellType = TextCellType3
        Me.spdParts_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdParts_Sheet1.Columns.Get(3).Label = "フェーズ"
        Me.spdParts_Sheet1.Columns.Get(3).Locked = True
        Me.spdParts_Sheet1.Columns.Get(3).Tag = "Phase"
        Me.spdParts_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.spdParts_Sheet1.Columns.Get(3).Width = 64.0!
        Me.spdParts_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdParts_Sheet1.Columns.Get(4).Label = "イベント"
        Me.spdParts_Sheet1.Columns.Get(4).Locked = True
        Me.spdParts_Sheet1.Columns.Get(4).Tag = "Event"
        Me.spdParts_Sheet1.Columns.Get(4).Width = 130.0!
        Me.spdParts_Sheet1.Columns.Get(5).Locked = True
        Me.spdParts_Sheet1.Columns.Get(5).Tag = "Id"
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
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.cmbName)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.cmbFugo)
        Me.Panel2.Controls.Add(Me.cmbPhase)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.cmbShakei)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 65)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(608, 73)
        Me.Panel2.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(286, 27)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 15)
        Me.Label11.TabIndex = 94
        Me.Label11.Text = "イベント："
        '
        'cmbName
        '
        Me.cmbName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbName.FormattingEnabled = True
        Me.cmbName.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.cmbName.Items.AddRange(New Object() {"", "台車", "開発完了確認車", "認証車"})
        Me.cmbName.Location = New System.Drawing.Point(289, 45)
        Me.cmbName.Name = "cmbName"
        Me.cmbName.Size = New System.Drawing.Size(130, 23)
        Me.cmbName.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cmbName, "あいまい検索（前方一致）が可能です。")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(116, 27)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 15)
        Me.Label8.TabIndex = 92
        Me.Label8.Text = "開発符号："
        '
        'cmbFugo
        '
        Me.cmbFugo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbFugo.FormattingEnabled = True
        Me.cmbFugo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbFugo.Items.AddRange(New Object() {"", "04T", "05S", "06R", "39T", "AS1", "EX7", "EY6", "EZ5", "FM5", "MF1", "MF1B", "PD5", "PE4", "UG4", "UH3", "UK1", "WN1", "WP9", "WQ8", "YR5", "YS4", "ZN4", "ZP3", "ZQ2", "ZR1"})
        Me.cmbFugo.Location = New System.Drawing.Point(119, 45)
        Me.cmbFugo.Name = "cmbFugo"
        Me.cmbFugo.Size = New System.Drawing.Size(98, 23)
        Me.cmbFugo.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.cmbFugo, "あいまい検索（前方一致）が可能です。")
        '
        'cmbPhase
        '
        Me.cmbPhase.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPhase.FormattingEnabled = True
        Me.cmbPhase.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbPhase.Items.AddRange(New Object() {"", "D", "K", "N"})
        Me.cmbPhase.Location = New System.Drawing.Point(223, 45)
        Me.cmbPhase.Name = "cmbPhase"
        Me.cmbPhase.Size = New System.Drawing.Size(60, 23)
        Me.cmbPhase.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbPhase, "イベントに対するコードです。")
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
        Me.Label6.Size = New System.Drawing.Size(45, 15)
        Me.Label6.TabIndex = 82
        Me.Label6.Text = "車系："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(220, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 15)
        Me.Label9.TabIndex = 86
        Me.Label9.Text = "フェーズ："
        '
        'cmbShakei
        '
        Me.cmbShakei.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShakei.FormattingEnabled = True
        Me.cmbShakei.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbShakei.Items.AddRange(New Object() {"", "B", "G", "R", "S", "W", "Y", "Z"})
        Me.cmbShakei.Location = New System.Drawing.Point(15, 45)
        Me.cmbShakei.Name = "cmbShakei"
        Me.cmbShakei.Size = New System.Drawing.Size(98, 23)
        Me.cmbShakei.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbShakei, "あいまい検索（前方一致）が可能です。")
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.btnEND)
        Me.Panel4.Controls.Add(Me.btnBACK)
        Me.Panel4.Controls.Add(Me.btnExcelExport)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 32)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(608, 33)
        Me.Panel4.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Yellow
        Me.Label1.Image = Global.Master.My.Resources.Resources.ロゴ
        Me.Label1.Location = New System.Drawing.Point(236, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 26)
        Me.Label1.TabIndex = 83
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnEND
        '
        Me.btnEND.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEND.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEND.Location = New System.Drawing.Point(6, 6)
        Me.btnEND.Name = "btnEND"
        Me.btnEND.Size = New System.Drawing.Size(149, 24)
        Me.btnEND.TabIndex = 8
        Me.btnEND.Text = "アプリケーション終了"
        Me.ToolTip1.SetToolTip(Me.btnEND, "新試作手配システムを終了します。")
        Me.btnEND.UseVisualStyleBackColor = True
        '
        'btnBACK
        '
        Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBACK.Location = New System.Drawing.Point(538, 6)
        Me.btnBACK.Name = "btnBACK"
        Me.btnBACK.Size = New System.Drawing.Size(64, 24)
        Me.btnBACK.TabIndex = 7
        Me.btnBACK.Text = "戻る"
        Me.btnBACK.UseVisualStyleBackColor = True
        '
        'btnExcelExport
        '
        Me.btnExcelExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcelExport.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExcelExport.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnExcelExport.Location = New System.Drawing.Point(422, 6)
        Me.btnExcelExport.Name = "btnExcelExport"
        Me.btnExcelExport.Size = New System.Drawing.Size(110, 24)
        Me.btnExcelExport.TabIndex = 6
        Me.btnExcelExport.Text = "EXCEL出力"
        Me.ToolTip1.SetToolTip(Me.btnExcelExport, "一覧表の内容を「EXCEL」へ出力します。")
        Me.btnExcelExport.UseVisualStyleBackColor = True
        '
        'btnDel
        '
        Me.btnDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDel.BackColor = System.Drawing.Color.HotPink
        Me.btnDel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDel.Location = New System.Drawing.Point(6, 464)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(79, 24)
        Me.btnDel.TabIndex = 86
        Me.btnDel.Text = "削除"
        Me.ToolTip1.SetToolTip(Me.btnDel, "削除へチェックされたものを一括で削除します。")
        Me.btnDel.UseVisualStyleBackColor = False
        '
        'btnNEW
        '
        Me.btnNEW.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNEW.BackColor = System.Drawing.Color.PaleGreen
        Me.btnNEW.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnNEW.Location = New System.Drawing.Point(525, 144)
        Me.btnNEW.Name = "btnNEW"
        Me.btnNEW.Size = New System.Drawing.Size(79, 24)
        Me.btnNEW.TabIndex = 87
        Me.btnNEW.Text = "新規登録"
        Me.btnNEW.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'frm6KaihatufugouMaster01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(608, 519)
        Me.Controls.Add(Me.btnNEW)
        Me.Controls.Add(Me.btnDel)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.spdParts)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frm6KaihatufugouMaster01"
        Me.Text = "新試作手配システム Ver 1.00"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmbPhase As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbShakei As System.Windows.Forms.ComboBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbFugo As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbName As System.Windows.Forms.ComboBox
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents btnNEW As System.Windows.Forms.Button
    Friend WithEvents btnExcelExport As System.Windows.Forms.Button
    Friend WithEvents btnEND As System.Windows.Forms.Button
    Friend WithEvents btnBACK As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
