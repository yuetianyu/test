Namespace NokiIkkatuSettei
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm21DispNokiIkkatuSettei
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
            Dim ComboBoxCellType4 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType
            Dim DateTimeCellType10 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType
            Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim DateTimeCellType11 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType
            Dim DateTimeCellType12 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType
            Dim DateTimeCellType13 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType
            Dim ComboBoxCellType3 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType
            Dim DateTimeCellType9 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm21DispNokiIkkatuSettei))
            Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim DateTimeCellType14 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType
            Dim DateTimeCellType15 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType
            Dim DateTimeCellType16 As FarPoint.Win.Spread.CellType.DateTimeCellType = New FarPoint.Win.Spread.CellType.DateTimeCellType
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.Panel4 = New System.Windows.Forms.Panel
            Me.Label14 = New System.Windows.Forms.Label
            Me.btnBACK = New System.Windows.Forms.Button
            Me.btnADDKokunaihin = New System.Windows.Forms.Button
            Me.Panel6 = New System.Windows.Forms.Panel
            Me.TabControl1 = New System.Windows.Forms.TabControl
            Me.TabPage1 = New System.Windows.Forms.TabPage
            Me.chkShikyuBuhin = New System.Windows.Forms.CheckBox
            Me.spdMaker = New FarPoint.Win.Spread.FpSpread
            Me.cMenuSpdRowCntrl = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.行挿入IToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.行削除DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.spdMaker_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.Label1 = New System.Windows.Forms.Label
            Me.dtpShikyuBuhinDay = New System.Windows.Forms.DateTimePicker
            Me.lblIbentoName = New System.Windows.Forms.Label
            Me.ckbBetuSettei = New System.Windows.Forms.CheckBox
            Me.spdKonyu = New FarPoint.Win.Spread.FpSpread
            Me.spdKonyu_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.TabPage2 = New System.Windows.Forms.TabPage
            Me.chkShikyuBuhinGencho = New System.Windows.Forms.CheckBox
            Me.spdMakerGencho = New FarPoint.Win.Spread.FpSpread
            Me.spdMakerGencho_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.btnADDGencho = New System.Windows.Forms.Button
            Me.Label2 = New System.Windows.Forms.Label
            Me.dtpShikyuBuhinDayGencho = New System.Windows.Forms.DateTimePicker
            Me.Label4 = New System.Windows.Forms.Label
            Me.ckbBetuSetteiGenchou = New System.Windows.Forms.CheckBox
            Me.spdKonyuGencho = New FarPoint.Win.Spread.FpSpread
            Me.spdKonyuGencho_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.Panel4.SuspendLayout()
            Me.TabControl1.SuspendLayout()
            Me.TabPage1.SuspendLayout()
            CType(Me.spdMaker, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.cMenuSpdRowCntrl.SuspendLayout()
            CType(Me.spdMaker_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdKonyu, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdKonyu_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TabPage2.SuspendLayout()
            CType(Me.spdMakerGencho, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdMakerGencho_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdKonyuGencho, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdKonyuGencho_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Panel3
            '
            Me.Panel3.AutoSize = True
            Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel3.BackColor = System.Drawing.SystemColors.Control
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel3.Location = New System.Drawing.Point(0, 301)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(608, 2)
            Me.Panel3.TabIndex = 71
            '
            'Panel4
            '
            Me.Panel4.AutoSize = True
            Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.Panel4.Controls.Add(Me.Label14)
            Me.Panel4.Controls.Add(Me.btnBACK)
            Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel4.Location = New System.Drawing.Point(0, 0)
            Me.Panel4.Name = "Panel4"
            Me.Panel4.Size = New System.Drawing.Size(608, 30)
            Me.Panel4.TabIndex = 84
            '
            'Label14
            '
            Me.Label14.AutoSize = True
            Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label14.Location = New System.Drawing.Point(3, 6)
            Me.Label14.Name = "Label14"
            Me.Label14.Size = New System.Drawing.Size(131, 17)
            Me.Label14.TabIndex = 89
            Me.Label14.Text = "<<納期一括設定>>"
            '
            'btnBACK
            '
            Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBACK.Location = New System.Drawing.Point(541, 3)
            Me.btnBACK.Name = "btnBACK"
            Me.btnBACK.Size = New System.Drawing.Size(64, 24)
            Me.btnBACK.TabIndex = 55
            Me.btnBACK.Text = "戻る"
            Me.btnBACK.UseVisualStyleBackColor = True
            '
            'btnADDKokunaihin
            '
            Me.btnADDKokunaihin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnADDKokunaihin.BackColor = System.Drawing.Color.PaleGreen
            Me.btnADDKokunaihin.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnADDKokunaihin.Location = New System.Drawing.Point(515, 8)
            Me.btnADDKokunaihin.Name = "btnADDKokunaihin"
            Me.btnADDKokunaihin.Size = New System.Drawing.Size(79, 24)
            Me.btnADDKokunaihin.TabIndex = 88
            Me.btnADDKokunaihin.Text = "登録"
            Me.btnADDKokunaihin.UseVisualStyleBackColor = False
            '
            'Panel6
            '
            Me.Panel6.AutoSize = True
            Me.Panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel6.BackColor = System.Drawing.SystemColors.Control
            Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel6.Location = New System.Drawing.Point(0, 299)
            Me.Panel6.Name = "Panel6"
            Me.Panel6.Size = New System.Drawing.Size(608, 2)
            Me.Panel6.TabIndex = 85
            '
            'TabControl1
            '
            Me.TabControl1.Controls.Add(Me.TabPage1)
            Me.TabControl1.Controls.Add(Me.TabPage2)
            Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Top
            Me.TabControl1.Location = New System.Drawing.Point(0, 30)
            Me.TabControl1.Name = "TabControl1"
            Me.TabControl1.SelectedIndex = 0
            Me.TabControl1.Size = New System.Drawing.Size(608, 272)
            Me.TabControl1.TabIndex = 89
            Me.TabControl1.Tag = ""
            '
            'TabPage1
            '
            Me.TabPage1.Controls.Add(Me.chkShikyuBuhin)
            Me.TabPage1.Controls.Add(Me.spdMaker)
            Me.TabPage1.Controls.Add(Me.btnADDKokunaihin)
            Me.TabPage1.Controls.Add(Me.Label1)
            Me.TabPage1.Controls.Add(Me.dtpShikyuBuhinDay)
            Me.TabPage1.Controls.Add(Me.lblIbentoName)
            Me.TabPage1.Controls.Add(Me.ckbBetuSettei)
            Me.TabPage1.Controls.Add(Me.spdKonyu)
            Me.TabPage1.Location = New System.Drawing.Point(4, 21)
            Me.TabPage1.Name = "TabPage1"
            Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage1.Size = New System.Drawing.Size(600, 247)
            Me.TabPage1.TabIndex = 0
            Me.TabPage1.Text = "国内品"
            Me.TabPage1.UseVisualStyleBackColor = True
            '
            'chkShikyuBuhin
            '
            Me.chkShikyuBuhin.AutoSize = True
            Me.chkShikyuBuhin.Location = New System.Drawing.Point(6, 11)
            Me.chkShikyuBuhin.Name = "chkShikyuBuhin"
            Me.chkShikyuBuhin.Size = New System.Drawing.Size(160, 16)
            Me.chkShikyuBuhin.TabIndex = 98
            Me.chkShikyuBuhin.Text = "支給部品（手配記号 A,D）："
            Me.chkShikyuBuhin.UseVisualStyleBackColor = True
            '
            'spdMaker
            '
            Me.spdMaker.AccessibleDescription = "spdMaker, Sheet1, Row 0, Column 0, "
            Me.spdMaker.AllowDragDrop = True
            Me.spdMaker.AllowDragFill = True
            Me.spdMaker.AllowUserFormulas = True
            Me.spdMaker.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.spdMaker.BackColor = System.Drawing.SystemColors.Control
            Me.spdMaker.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentCell
            Me.spdMaker.ContextMenuStrip = Me.cMenuSpdRowCntrl
            Me.spdMaker.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdMaker.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdMaker.Location = New System.Drawing.Point(19, 58)
            Me.spdMaker.Name = "spdMaker"
            Me.spdMaker.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdMaker.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdMaker_Sheet1})
            Me.spdMaker.Size = New System.Drawing.Size(267, 181)
            Me.spdMaker.TabIndex = 97
            Me.spdMaker.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdMaker.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'cMenuSpdRowCntrl
            '
            Me.cMenuSpdRowCntrl.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.行挿入IToolStripMenuItem, Me.行削除DToolStripMenuItem})
            Me.cMenuSpdRowCntrl.Name = "ContextMenuStrip1"
            Me.cMenuSpdRowCntrl.Size = New System.Drawing.Size(123, 48)
            '
            '行挿入IToolStripMenuItem
            '
            Me.行挿入IToolStripMenuItem.Name = "行挿入IToolStripMenuItem"
            Me.行挿入IToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
            Me.行挿入IToolStripMenuItem.Text = "行挿入(&I)"
            '
            '行削除DToolStripMenuItem
            '
            Me.行削除DToolStripMenuItem.Name = "行削除DToolStripMenuItem"
            Me.行削除DToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
            Me.行削除DToolStripMenuItem.Text = "行削除(&D)"
            '
            'spdMaker_Sheet1
            '
            Me.spdMaker_Sheet1.Reset()
            Me.spdMaker_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdMaker_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdMaker_Sheet1.ColumnCount = 2
            Me.spdMaker_Sheet1.RowCount = 30
            Me.spdMaker_Sheet1.AllowNoteEdit = True
            Me.spdMaker_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdMaker_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdMaker_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "取引先"
            Me.spdMaker_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "納期指定日"
            Me.spdMaker_Sheet1.ColumnHeader.Rows.Get(0).Height = 16.0!
            ComboBoxCellType4.ButtonAlign = FarPoint.Win.ButtonAlign.Right
            ComboBoxCellType4.Editable = True
            Me.spdMaker_Sheet1.Columns.Get(0).CellType = ComboBoxCellType4
            Me.spdMaker_Sheet1.Columns.Get(0).Label = "取引先"
            Me.spdMaker_Sheet1.Columns.Get(0).Locked = False
            Me.spdMaker_Sheet1.Columns.Get(0).Tag = "TAG_MAKER"
            Me.spdMaker_Sheet1.Columns.Get(0).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdMaker_Sheet1.Columns.Get(0).Width = 111.0!
            DateTimeCellType10.Calendar = CType(resources.GetObject("DateTimeCellType10.Calendar"), System.Globalization.Calendar)
            DateTimeCellType10.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
            DateTimeCellType10.DateDefault = New Date(2010, 12, 27, 11, 16, 52, 0)
            DateTimeCellType10.DropDownButton = True
            DateTimeCellType10.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
            DateTimeCellType10.TimeDefault = New Date(2010, 12, 27, 11, 16, 52, 0)
            Me.spdMaker_Sheet1.Columns.Get(1).CellType = DateTimeCellType10
            Me.spdMaker_Sheet1.Columns.Get(1).Label = "納期指定日"
            Me.spdMaker_Sheet1.Columns.Get(1).Tag = "TAG_NOUKI_SHITEIBI"
            Me.spdMaker_Sheet1.Columns.Get(1).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdMaker_Sheet1.Columns.Get(1).Width = 99.0!
            Me.spdMaker_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdMaker_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdMaker_Sheet1.RowHeader.DefaultStyle.Locked = False
            Me.spdMaker_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
            Me.spdMaker_Sheet1.Rows.Default.Height = 16.0!
            Me.spdMaker_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.Location = New System.Drawing.Point(6, 35)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(71, 12)
            Me.Label1.TabIndex = 95
            Me.Label1.Text = "取引先指定："
            '
            'dtpShikyuBuhinDay
            '
            Me.dtpShikyuBuhinDay.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.dtpShikyuBuhinDay.Enabled = False
            Me.dtpShikyuBuhinDay.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.dtpShikyuBuhinDay.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpShikyuBuhinDay.Location = New System.Drawing.Point(180, 11)
            Me.dtpShikyuBuhinDay.Name = "dtpShikyuBuhinDay"
            Me.dtpShikyuBuhinDay.Size = New System.Drawing.Size(85, 19)
            Me.dtpShikyuBuhinDay.TabIndex = 93
            '
            'lblIbentoName
            '
            Me.lblIbentoName.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblIbentoName.ForeColor = System.Drawing.Color.Black
            Me.lblIbentoName.Location = New System.Drawing.Point(292, 33)
            Me.lblIbentoName.Name = "lblIbentoName"
            Me.lblIbentoName.Size = New System.Drawing.Size(252, 22)
            Me.lblIbentoName.TabIndex = 90
            Me.lblIbentoName.Text = "購入品（手配記号　空白、M）"
            Me.lblIbentoName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'ckbBetuSettei
            '
            Me.ckbBetuSettei.AutoSize = True
            Me.ckbBetuSettei.Location = New System.Drawing.Point(294, 11)
            Me.ckbBetuSettei.Name = "ckbBetuSettei"
            Me.ckbBetuSettei.Size = New System.Drawing.Size(140, 16)
            Me.ckbBetuSettei.TabIndex = 74
            Me.ckbBetuSettei.Text = "専用品、共用品別設定"
            Me.ckbBetuSettei.UseVisualStyleBackColor = True
            '
            'spdKonyu
            '
            Me.spdKonyu.AccessibleDescription = "spdKonyu, Sheet1, Row 0, Column 0, #1000"
            Me.spdKonyu.AllowDragDrop = True
            Me.spdKonyu.AllowDragFill = True
            Me.spdKonyu.AllowUserFormulas = True
            Me.spdKonyu.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.spdKonyu.BackColor = System.Drawing.SystemColors.Control
            Me.spdKonyu.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentCell
            Me.spdKonyu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdKonyu.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdKonyu.Location = New System.Drawing.Point(303, 58)
            Me.spdKonyu.Name = "spdKonyu"
            Me.spdKonyu.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdKonyu.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdKonyu_Sheet1})
            Me.spdKonyu.Size = New System.Drawing.Size(289, 181)
            Me.spdKonyu.TabIndex = 91
            Me.spdKonyu.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdKonyu.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdKonyu_Sheet1
            '
            Me.spdKonyu_Sheet1.Reset()
            Me.spdKonyu_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdKonyu_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdKonyu_Sheet1.ColumnCount = 4
            Me.spdKonyu_Sheet1.RowCount = 10
            Me.spdKonyu_Sheet1.AllowNoteEdit = True
            Me.spdKonyu_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdKonyu_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdKonyu_Sheet1.Cells.Get(0, 0).Value = "#1000"
            Me.spdKonyu_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "ブロック"
            Me.spdKonyu_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "納期"
            Me.spdKonyu_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "（専用品）"
            Me.spdKonyu_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "（共用品）"
            Me.spdKonyu_Sheet1.ColumnHeader.Rows.Get(0).Height = 16.0!
            Me.spdKonyu_Sheet1.Columns.Get(0).CellType = TextCellType3
            Me.spdKonyu_Sheet1.Columns.Get(0).Label = "ブロック"
            Me.spdKonyu_Sheet1.Columns.Get(0).Locked = True
            Me.spdKonyu_Sheet1.Columns.Get(0).Tag = "TAG_BLOCK"
            Me.spdKonyu_Sheet1.Columns.Get(0).Width = 63.0!
            DateTimeCellType11.Calendar = CType(resources.GetObject("DateTimeCellType11.Calendar"), System.Globalization.Calendar)
            DateTimeCellType11.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
            DateTimeCellType11.DateDefault = New Date(2010, 12, 23, 1, 58, 51, 0)
            DateTimeCellType11.DropDownButton = True
            DateTimeCellType11.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
            DateTimeCellType11.TimeDefault = New Date(2010, 12, 23, 1, 58, 51, 0)
            Me.spdKonyu_Sheet1.Columns.Get(1).CellType = DateTimeCellType11
            Me.spdKonyu_Sheet1.Columns.Get(1).Label = "納期"
            Me.spdKonyu_Sheet1.Columns.Get(1).Tag = "TAG_NOUKI"
            Me.spdKonyu_Sheet1.Columns.Get(1).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdKonyu_Sheet1.Columns.Get(1).Width = 89.0!
            DateTimeCellType12.Calendar = CType(resources.GetObject("DateTimeCellType12.Calendar"), System.Globalization.Calendar)
            DateTimeCellType12.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
            DateTimeCellType12.DateDefault = New Date(2010, 12, 27, 11, 3, 5, 0)
            DateTimeCellType12.DropDownButton = True
            DateTimeCellType12.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
            DateTimeCellType12.TimeDefault = New Date(2010, 12, 27, 11, 3, 5, 0)
            Me.spdKonyu_Sheet1.Columns.Get(2).CellType = DateTimeCellType12
            Me.spdKonyu_Sheet1.Columns.Get(2).Label = "（専用品）"
            Me.spdKonyu_Sheet1.Columns.Get(2).Tag = "TAG_SENYOU_NOUKI"
            Me.spdKonyu_Sheet1.Columns.Get(2).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdKonyu_Sheet1.Columns.Get(2).Width = 92.0!
            DateTimeCellType13.Calendar = CType(resources.GetObject("DateTimeCellType13.Calendar"), System.Globalization.Calendar)
            DateTimeCellType13.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
            DateTimeCellType13.DateDefault = New Date(2010, 12, 27, 11, 3, 5, 0)
            DateTimeCellType13.DropDownButton = True
            DateTimeCellType13.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
            DateTimeCellType13.TimeDefault = New Date(2010, 12, 27, 11, 3, 5, 0)
            Me.spdKonyu_Sheet1.Columns.Get(3).CellType = DateTimeCellType13
            Me.spdKonyu_Sheet1.Columns.Get(3).Label = "（共用品）"
            Me.spdKonyu_Sheet1.Columns.Get(3).Tag = "TAG_KYOUYOU_NOUKI"
            Me.spdKonyu_Sheet1.Columns.Get(3).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdKonyu_Sheet1.Columns.Get(3).Width = 89.0!
            Me.spdKonyu_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdKonyu_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdKonyu_Sheet1.RowHeader.DefaultStyle.Locked = False
            Me.spdKonyu_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
            Me.spdKonyu_Sheet1.Rows.Default.Height = 16.0!
            Me.spdKonyu_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'TabPage2
            '
            Me.TabPage2.Controls.Add(Me.chkShikyuBuhinGencho)
            Me.TabPage2.Controls.Add(Me.spdMakerGencho)
            Me.TabPage2.Controls.Add(Me.btnADDGencho)
            Me.TabPage2.Controls.Add(Me.Label2)
            Me.TabPage2.Controls.Add(Me.dtpShikyuBuhinDayGencho)
            Me.TabPage2.Controls.Add(Me.Label4)
            Me.TabPage2.Controls.Add(Me.ckbBetuSetteiGenchou)
            Me.TabPage2.Controls.Add(Me.spdKonyuGencho)
            Me.TabPage2.Location = New System.Drawing.Point(4, 21)
            Me.TabPage2.Name = "TabPage2"
            Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage2.Size = New System.Drawing.Size(600, 247)
            Me.TabPage2.TabIndex = 1
            Me.TabPage2.Text = "現調品"
            Me.TabPage2.UseVisualStyleBackColor = True
            '
            'chkShikyuBuhinGencho
            '
            Me.chkShikyuBuhinGencho.AutoSize = True
            Me.chkShikyuBuhinGencho.Location = New System.Drawing.Point(6, 11)
            Me.chkShikyuBuhinGencho.Name = "chkShikyuBuhinGencho"
            Me.chkShikyuBuhinGencho.Size = New System.Drawing.Size(150, 16)
            Me.chkShikyuBuhinGencho.TabIndex = 106
            Me.chkShikyuBuhinGencho.Text = "支給部品（手配記号 B）："
            Me.chkShikyuBuhinGencho.UseVisualStyleBackColor = True
            '
            'spdMakerGencho
            '
            Me.spdMakerGencho.AccessibleDescription = "spdMakerGencho, Sheet1, Row 0, Column 0, "
            Me.spdMakerGencho.AllowDragDrop = True
            Me.spdMakerGencho.AllowDragFill = True
            Me.spdMakerGencho.AllowUserFormulas = True
            Me.spdMakerGencho.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.spdMakerGencho.BackColor = System.Drawing.SystemColors.Control
            Me.spdMakerGencho.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentCell
            Me.spdMakerGencho.ContextMenuStrip = Me.cMenuSpdRowCntrl
            Me.spdMakerGencho.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdMakerGencho.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdMakerGencho.Location = New System.Drawing.Point(19, 58)
            Me.spdMakerGencho.Name = "spdMakerGencho"
            Me.spdMakerGencho.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdMakerGencho.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdMakerGencho_Sheet1})
            Me.spdMakerGencho.Size = New System.Drawing.Size(267, 181)
            Me.spdMakerGencho.TabIndex = 105
            Me.spdMakerGencho.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdMakerGencho.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdMakerGencho_Sheet1
            '
            Me.spdMakerGencho_Sheet1.Reset()
            Me.spdMakerGencho_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdMakerGencho_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdMakerGencho_Sheet1.ColumnCount = 2
            Me.spdMakerGencho_Sheet1.RowCount = 30
            Me.spdMakerGencho_Sheet1.AllowNoteEdit = True
            Me.spdMakerGencho_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdMakerGencho_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdMakerGencho_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "取引先"
            Me.spdMakerGencho_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "納期指定日"
            Me.spdMakerGencho_Sheet1.ColumnHeader.Rows.Get(0).Height = 16.0!
            ComboBoxCellType3.ButtonAlign = FarPoint.Win.ButtonAlign.Right
            ComboBoxCellType3.Editable = True
            Me.spdMakerGencho_Sheet1.Columns.Get(0).CellType = ComboBoxCellType3
            Me.spdMakerGencho_Sheet1.Columns.Get(0).Label = "取引先"
            Me.spdMakerGencho_Sheet1.Columns.Get(0).Locked = False
            Me.spdMakerGencho_Sheet1.Columns.Get(0).Tag = "TAG_MAKER"
            Me.spdMakerGencho_Sheet1.Columns.Get(0).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdMakerGencho_Sheet1.Columns.Get(0).Width = 111.0!
            DateTimeCellType9.Calendar = CType(resources.GetObject("DateTimeCellType9.Calendar"), System.Globalization.Calendar)
            DateTimeCellType9.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
            DateTimeCellType9.DateDefault = New Date(2010, 6, 23, 11, 16, 52, 0)
            DateTimeCellType9.DropDownButton = True
            DateTimeCellType9.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
            DateTimeCellType9.TimeDefault = New Date(2010, 6, 23, 11, 16, 52, 0)
            Me.spdMakerGencho_Sheet1.Columns.Get(1).CellType = DateTimeCellType9
            Me.spdMakerGencho_Sheet1.Columns.Get(1).Label = "納期指定日"
            Me.spdMakerGencho_Sheet1.Columns.Get(1).Tag = "TAG_NOUKI_SHITEIBI"
            Me.spdMakerGencho_Sheet1.Columns.Get(1).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdMakerGencho_Sheet1.Columns.Get(1).Width = 99.0!
            Me.spdMakerGencho_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdMakerGencho_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdMakerGencho_Sheet1.RowHeader.DefaultStyle.Locked = False
            Me.spdMakerGencho_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
            Me.spdMakerGencho_Sheet1.Rows.Default.Height = 16.0!
            Me.spdMakerGencho_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'btnADDGencho
            '
            Me.btnADDGencho.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnADDGencho.BackColor = System.Drawing.Color.PaleGreen
            Me.btnADDGencho.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnADDGencho.Location = New System.Drawing.Point(515, 8)
            Me.btnADDGencho.Name = "btnADDGencho"
            Me.btnADDGencho.Size = New System.Drawing.Size(79, 24)
            Me.btnADDGencho.TabIndex = 99
            Me.btnADDGencho.Text = "登録"
            Me.btnADDGencho.UseVisualStyleBackColor = False
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label2.Location = New System.Drawing.Point(6, 35)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(71, 12)
            Me.Label2.TabIndex = 104
            Me.Label2.Text = "取引先指定："
            '
            'dtpShikyuBuhinDayGencho
            '
            Me.dtpShikyuBuhinDayGencho.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.dtpShikyuBuhinDayGencho.Enabled = False
            Me.dtpShikyuBuhinDayGencho.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.dtpShikyuBuhinDayGencho.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpShikyuBuhinDayGencho.Location = New System.Drawing.Point(180, 11)
            Me.dtpShikyuBuhinDayGencho.Name = "dtpShikyuBuhinDayGencho"
            Me.dtpShikyuBuhinDayGencho.Size = New System.Drawing.Size(85, 19)
            Me.dtpShikyuBuhinDayGencho.TabIndex = 103
            '
            'Label4
            '
            Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label4.ForeColor = System.Drawing.Color.Black
            Me.Label4.Location = New System.Drawing.Point(292, 33)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(252, 22)
            Me.Label4.TabIndex = 100
            Me.Label4.Text = "購入品（手配記号　J）"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'ckbBetuSetteiGenchou
            '
            Me.ckbBetuSetteiGenchou.AutoSize = True
            Me.ckbBetuSetteiGenchou.Location = New System.Drawing.Point(294, 11)
            Me.ckbBetuSetteiGenchou.Name = "ckbBetuSetteiGenchou"
            Me.ckbBetuSetteiGenchou.Size = New System.Drawing.Size(140, 16)
            Me.ckbBetuSetteiGenchou.TabIndex = 98
            Me.ckbBetuSetteiGenchou.Text = "専用品、共用品別設定"
            Me.ckbBetuSetteiGenchou.UseVisualStyleBackColor = True
            '
            'spdKonyuGencho
            '
            Me.spdKonyuGencho.AccessibleDescription = "spdKonyuGencho, Sheet1, Row 0, Column 0, "
            Me.spdKonyuGencho.AllowDragDrop = True
            Me.spdKonyuGencho.AllowDragFill = True
            Me.spdKonyuGencho.AllowUserFormulas = True
            Me.spdKonyuGencho.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.spdKonyuGencho.BackColor = System.Drawing.SystemColors.Control
            Me.spdKonyuGencho.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentCell
            Me.spdKonyuGencho.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdKonyuGencho.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdKonyuGencho.Location = New System.Drawing.Point(303, 58)
            Me.spdKonyuGencho.Name = "spdKonyuGencho"
            Me.spdKonyuGencho.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdKonyuGencho.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdKonyuGencho_Sheet1})
            Me.spdKonyuGencho.Size = New System.Drawing.Size(289, 181)
            Me.spdKonyuGencho.TabIndex = 101
            Me.spdKonyuGencho.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdKonyuGencho.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdKonyuGencho_Sheet1
            '
            Me.spdKonyuGencho_Sheet1.Reset()
            Me.spdKonyuGencho_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdKonyuGencho_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdKonyuGencho_Sheet1.ColumnCount = 4
            Me.spdKonyuGencho_Sheet1.RowCount = 10
            Me.spdKonyuGencho_Sheet1.AllowNoteEdit = True
            Me.spdKonyuGencho_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdKonyuGencho_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdKonyuGencho_Sheet1.Cells.Get(0, 0).Value = "#1000"
            Me.spdKonyuGencho_Sheet1.Cells.Get(1, 0).Value = "#2000"
            Me.spdKonyuGencho_Sheet1.Cells.Get(2, 0).Value = "#3000"
            Me.spdKonyuGencho_Sheet1.Cells.Get(3, 0).Value = "#4000"
            Me.spdKonyuGencho_Sheet1.Cells.Get(4, 0).Value = "#5000"
            Me.spdKonyuGencho_Sheet1.Cells.Get(5, 0).Value = "#6000"
            Me.spdKonyuGencho_Sheet1.Cells.Get(6, 0).Value = "#7000"
            Me.spdKonyuGencho_Sheet1.Cells.Get(7, 0).Value = "#8000"
            Me.spdKonyuGencho_Sheet1.Cells.Get(8, 0).Value = "#9000"
            Me.spdKonyuGencho_Sheet1.Cells.Get(9, 0).Value = "一括設定"
            Me.spdKonyuGencho_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "ブロック"
            Me.spdKonyuGencho_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "納期"
            Me.spdKonyuGencho_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "（専用品）"
            Me.spdKonyuGencho_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "（共用品）"
            Me.spdKonyuGencho_Sheet1.ColumnHeader.Rows.Get(0).Height = 16.0!
            Me.spdKonyuGencho_Sheet1.Columns.Get(0).CellType = TextCellType4
            Me.spdKonyuGencho_Sheet1.Columns.Get(0).Label = "ブロック"
            Me.spdKonyuGencho_Sheet1.Columns.Get(0).Locked = True
            Me.spdKonyuGencho_Sheet1.Columns.Get(0).Tag = "TAG_BLOCK"
            Me.spdKonyuGencho_Sheet1.Columns.Get(0).Width = 63.0!
            DateTimeCellType14.Calendar = CType(resources.GetObject("DateTimeCellType14.Calendar"), System.Globalization.Calendar)
            DateTimeCellType14.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
            DateTimeCellType14.DateDefault = New Date(2010, 12, 22, 16, 26, 49, 0)
            DateTimeCellType14.DropDownButton = True
            DateTimeCellType14.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
            DateTimeCellType14.TimeDefault = New Date(2010, 12, 22, 16, 26, 49, 0)
            Me.spdKonyuGencho_Sheet1.Columns.Get(1).CellType = DateTimeCellType14
            Me.spdKonyuGencho_Sheet1.Columns.Get(1).Label = "納期"
            Me.spdKonyuGencho_Sheet1.Columns.Get(1).Tag = "TAG_NOUKI"
            Me.spdKonyuGencho_Sheet1.Columns.Get(1).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdKonyuGencho_Sheet1.Columns.Get(1).Width = 89.0!
            DateTimeCellType15.Calendar = CType(resources.GetObject("DateTimeCellType15.Calendar"), System.Globalization.Calendar)
            DateTimeCellType15.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
            DateTimeCellType15.DateDefault = New Date(2010, 6, 23, 11, 3, 5, 0)
            DateTimeCellType15.DropDownButton = True
            DateTimeCellType15.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
            DateTimeCellType15.TimeDefault = New Date(2010, 6, 23, 11, 3, 5, 0)
            Me.spdKonyuGencho_Sheet1.Columns.Get(2).CellType = DateTimeCellType15
            Me.spdKonyuGencho_Sheet1.Columns.Get(2).Label = "（専用品）"
            Me.spdKonyuGencho_Sheet1.Columns.Get(2).Tag = "TAG_SENYOU_NOUKI"
            Me.spdKonyuGencho_Sheet1.Columns.Get(2).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdKonyuGencho_Sheet1.Columns.Get(2).Width = 89.0!
            DateTimeCellType16.Calendar = CType(resources.GetObject("DateTimeCellType16.Calendar"), System.Globalization.Calendar)
            DateTimeCellType16.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText
            DateTimeCellType16.DateDefault = New Date(2010, 6, 23, 11, 3, 5, 0)
            DateTimeCellType16.DropDownButton = True
            DateTimeCellType16.MaximumTime = System.TimeSpan.Parse("23:59:59.9999999")
            DateTimeCellType16.TimeDefault = New Date(2010, 6, 23, 11, 3, 5, 0)
            Me.spdKonyuGencho_Sheet1.Columns.Get(3).CellType = DateTimeCellType16
            Me.spdKonyuGencho_Sheet1.Columns.Get(3).Label = "（共用品）"
            Me.spdKonyuGencho_Sheet1.Columns.Get(3).Tag = "TAG_KYOUYOU_NOUKI"
            Me.spdKonyuGencho_Sheet1.Columns.Get(3).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdKonyuGencho_Sheet1.Columns.Get(3).Width = 89.0!
            Me.spdKonyuGencho_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdKonyuGencho_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdKonyuGencho_Sheet1.RowHeader.DefaultStyle.Locked = False
            Me.spdKonyuGencho_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
            Me.spdKonyuGencho_Sheet1.Rows.Default.Height = 16.0!
            Me.spdKonyuGencho_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'Frm21DispNokiIkkatuSettei
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(608, 303)
            Me.Controls.Add(Me.TabControl1)
            Me.Controls.Add(Me.Panel6)
            Me.Controls.Add(Me.Panel4)
            Me.Controls.Add(Me.Panel3)
            Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "Frm21DispNokiIkkatuSettei"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "新試作手配システム Ver 1.00"
            Me.TopMost = True
            Me.Panel4.ResumeLayout(False)
            Me.Panel4.PerformLayout()
            Me.TabControl1.ResumeLayout(False)
            Me.TabPage1.ResumeLayout(False)
            Me.TabPage1.PerformLayout()
            CType(Me.spdMaker, System.ComponentModel.ISupportInitialize).EndInit()
            Me.cMenuSpdRowCntrl.ResumeLayout(False)
            CType(Me.spdMaker_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdKonyu, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdKonyu_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TabPage2.ResumeLayout(False)
            Me.TabPage2.PerformLayout()
            CType(Me.spdMakerGencho, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdMakerGencho_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdKonyuGencho, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdKonyuGencho_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents Panel4 As System.Windows.Forms.Panel
        Friend WithEvents btnBACK As System.Windows.Forms.Button
        Friend WithEvents Panel6 As System.Windows.Forms.Panel
        Friend WithEvents btnADDKokunaihin As System.Windows.Forms.Button
        Friend WithEvents Label14 As System.Windows.Forms.Label
        Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
        Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
        Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
        Friend WithEvents ckbBetuSettei As System.Windows.Forms.CheckBox
        Friend WithEvents lblIbentoName As System.Windows.Forms.Label
        Friend WithEvents spdKonyu As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdKonyu_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents dtpShikyuBuhinDay As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents spdMaker As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdMaker_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents spdMakerGencho As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdMakerGencho_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents btnADDGencho As System.Windows.Forms.Button
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents dtpShikyuBuhinDayGencho As System.Windows.Forms.DateTimePicker
        Friend WithEvents spdKonyuGencho As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdKonyuGencho_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents ckbBetuSetteiGenchou As System.Windows.Forms.CheckBox
        Friend WithEvents cMenuSpdRowCntrl As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents 行挿入IToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents 行削除DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents chkShikyuBuhin As System.Windows.Forms.CheckBox
        Friend WithEvents chkShikyuBuhinGencho As System.Windows.Forms.CheckBox
    End Class
End Namespace
