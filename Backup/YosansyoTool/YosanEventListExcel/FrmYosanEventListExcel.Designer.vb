﻿Namespace YosanEventListExcel

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FrmYosanEventListExcel
        Inherits System.Windows.Forms.Form

        'Form 重写 Dispose，以清理组件列表。
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

        'Windows 窗体设计器所必需的
        Private components As System.ComponentModel.IContainer

        '注意: 以下过程是 Windows 窗体设计器所必需的
        '可以使用 Windows 窗体设计器修改它。
        '不要使用代码编辑器修改它。
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim cultureInfo As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US", False)
            Dim CheckBoxCellType1 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType
            Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmYosanEventListExcel))
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.LblLoginBukaName = New System.Windows.Forms.Label
            Me.LblDateNow = New System.Windows.Forms.Label
            Me.LblLoginUserId = New System.Windows.Forms.Label
            Me.LblTimeNow = New System.Windows.Forms.Label
            Me.LblCurrPGId = New System.Windows.Forms.Label
            Me.LblCurrPGName = New System.Windows.Forms.Label
            Me.lblKaihatsuFugo = New System.Windows.Forms.Label
            Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.spdParts = New FarPoint.Win.Spread.FpSpread
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.lblEvent = New System.Windows.Forms.Label
            Me.lblKikan = New System.Windows.Forms.Label
            Me.cmbKaihatsuFugo = New System.Windows.Forms.ComboBox
            Me.cmbEvent = New System.Windows.Forms.ComboBox
            Me.cmbKikanFrom = New System.Windows.Forms.ComboBox
            Me.btnEdit = New System.Windows.Forms.Button
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.btnExcelExport = New System.Windows.Forms.Button
            Me.LOGO = New System.Windows.Forms.Label
            Me.btnBack = New System.Windows.Forms.Button
            Me.btnYosanShukeiExcelExport = New System.Windows.Forms.Button
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.LblMessage = New System.Windows.Forms.Label
            Me.cmbKbn = New System.Windows.Forms.ComboBox
            Me.Label1 = New System.Windows.Forms.Label
            Me.cmbYosanCode = New System.Windows.Forms.ComboBox
            Me.Label2 = New System.Windows.Forms.Label
            Me.cmbKikanTo = New System.Windows.Forms.ComboBox
            Me.Label3 = New System.Windows.Forms.Label
            Me.Panel1.SuspendLayout()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel2.SuspendLayout()
            Me.Panel3.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.LblLoginBukaName)
            Me.Panel1.Controls.Add(Me.LblDateNow)
            Me.Panel1.Controls.Add(Me.LblLoginUserId)
            Me.Panel1.Controls.Add(Me.LblTimeNow)
            Me.Panel1.Controls.Add(Me.LblCurrPGId)
            Me.Panel1.Controls.Add(Me.LblCurrPGName)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(1224, 30)
            Me.Panel1.TabIndex = 87
            '
            'LblLoginBukaName
            '
            Me.LblLoginBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblLoginBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblLoginBukaName.ForeColor = System.Drawing.Color.White
            Me.LblLoginBukaName.Location = New System.Drawing.Point(996, 17)
            Me.LblLoginBukaName.Name = "LblLoginBukaName"
            Me.LblLoginBukaName.Size = New System.Drawing.Size(130, 13)
            Me.LblLoginBukaName.TabIndex = 59
            Me.LblLoginBukaName.Text = "(部課：SKE1         )"
            '
            'LblDateNow
            '
            Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblDateNow.AutoSize = True
            Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblDateNow.ForeColor = System.Drawing.Color.White
            Me.LblDateNow.Location = New System.Drawing.Point(1132, 4)
            Me.LblDateNow.Name = "LblDateNow"
            Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
            Me.LblDateNow.TabIndex = 56
            Me.LblDateNow.Text = "YYYY/MM/DD"
            '
            'LblLoginUserId
            '
            Me.LblLoginUserId.AccessibleRole = System.Windows.Forms.AccessibleRole.None
            Me.LblLoginUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblLoginUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblLoginUserId.ForeColor = System.Drawing.Color.White
            Me.LblLoginUserId.Location = New System.Drawing.Point(996, 4)
            Me.LblLoginUserId.Name = "LblLoginUserId"
            Me.LblLoginUserId.Size = New System.Drawing.Size(130, 13)
            Me.LblLoginUserId.TabIndex = 58
            Me.LblLoginUserId.Text = "(ID    ：ABCDEFGH)"
            '
            'LblTimeNow
            '
            Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblTimeNow.AutoSize = True
            Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblTimeNow.ForeColor = System.Drawing.Color.White
            Me.LblTimeNow.Location = New System.Drawing.Point(1159, 17)
            Me.LblTimeNow.Name = "LblTimeNow"
            Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
            Me.LblTimeNow.TabIndex = 55
            Me.LblTimeNow.Text = "HH:MM:DD"
            '
            'LblCurrPGId
            '
            Me.LblCurrPGId.AutoSize = True
            Me.LblCurrPGId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrPGId.ForeColor = System.Drawing.Color.White
            Me.LblCurrPGId.Location = New System.Drawing.Point(4, 4)
            Me.LblCurrPGId.Name = "LblCurrPGId"
            Me.LblCurrPGId.Size = New System.Drawing.Size(113, 13)
            Me.LblCurrPGId.TabIndex = 54
            Me.LblCurrPGId.Text = "PG-ID：XXXXXXXX"
            '
            'LblCurrPGName
            '
            Me.LblCurrPGName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrPGName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrPGName.ForeColor = System.Drawing.Color.Yellow
            Me.LblCurrPGName.Location = New System.Drawing.Point(325, 2)
            Me.LblCurrPGName.Name = "LblCurrPGName"
            Me.LblCurrPGName.Size = New System.Drawing.Size(471, 27)
            Me.LblCurrPGName.TabIndex = 54
            Me.LblCurrPGName.Text = "予算書イベント情報EXCEL出力"
            Me.LblCurrPGName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblKaihatsuFugo
            '
            Me.lblKaihatsuFugo.AutoSize = True
            Me.lblKaihatsuFugo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblKaihatsuFugo.Location = New System.Drawing.Point(88, 77)
            Me.lblKaihatsuFugo.Name = "lblKaihatsuFugo"
            Me.lblKaihatsuFugo.Size = New System.Drawing.Size(75, 15)
            Me.lblKaihatsuFugo.TabIndex = 90
            Me.lblKaihatsuFugo.Text = "開発符号："
            '
            'spdParts_Sheet1
            '
            Me.spdParts_Sheet1.Reset()
            Me.spdParts_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdParts_Sheet1.ColumnCount = 17
            Me.spdParts_Sheet1.ColumnHeader.RowCount = 2
            Me.spdParts_Sheet1.ActiveSkin = New FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Silver, False, False, False, True, True)
            Me.spdParts_Sheet1.AllowNoteEdit = True
            Me.spdParts_Sheet1.AutoGenerateColumns = False
            Me.spdParts_Sheet1.Cells.Get(0, 7).Value = "YYYY年上期"
            Me.spdParts_Sheet1.Cells.Get(0, 8).Value = "～"
            Me.spdParts_Sheet1.Cells.Get(0, 9).Value = "YYYY年下期"
            Me.spdParts_Sheet1.Cells.Get(0, 10).ParseFormatInfo = CType(cultureInfo.NumberFormat.Clone, System.Globalization.NumberFormatInfo)
            CType(Me.spdParts_Sheet1.Cells.Get(0, 10).ParseFormatInfo, System.Globalization.NumberFormatInfo).CurrencyDecimalDigits = 0
            CType(Me.spdParts_Sheet1.Cells.Get(0, 10).ParseFormatInfo, System.Globalization.NumberFormatInfo).CurrencyNegativePattern = 1
            CType(Me.spdParts_Sheet1.Cells.Get(0, 10).ParseFormatInfo, System.Globalization.NumberFormatInfo).CurrencySymbol = "¥"
            CType(Me.spdParts_Sheet1.Cells.Get(0, 10).ParseFormatInfo, System.Globalization.NumberFormatInfo).NaNSymbol = "NaN (非数値)"
            CType(Me.spdParts_Sheet1.Cells.Get(0, 10).ParseFormatInfo, System.Globalization.NumberFormatInfo).NegativeInfinitySymbol = "-∞"
            CType(Me.spdParts_Sheet1.Cells.Get(0, 10).ParseFormatInfo, System.Globalization.NumberFormatInfo).NumberDecimalDigits = 0
            CType(Me.spdParts_Sheet1.Cells.Get(0, 10).ParseFormatInfo, System.Globalization.NumberFormatInfo).PercentNegativePattern = 1
            CType(Me.spdParts_Sheet1.Cells.Get(0, 10).ParseFormatInfo, System.Globalization.NumberFormatInfo).PercentPositivePattern = 1
            CType(Me.spdParts_Sheet1.Cells.Get(0, 10).ParseFormatInfo, System.Globalization.NumberFormatInfo).PositiveInfinitySymbol = "+∞"
            Me.spdParts_Sheet1.Cells.Get(0, 10).ParseFormatString = "n"
            Me.spdParts_Sheet1.Cells.Get(0, 10).Value = 10
            Me.spdParts_Sheet1.Cells.Get(0, 11).Value = "＋"
            Me.spdParts_Sheet1.Cells.Get(0, 12).ParseFormatInfo = CType(cultureInfo.NumberFormat.Clone, System.Globalization.NumberFormatInfo)
            CType(Me.spdParts_Sheet1.Cells.Get(0, 12).ParseFormatInfo, System.Globalization.NumberFormatInfo).CurrencyDecimalDigits = 0
            CType(Me.spdParts_Sheet1.Cells.Get(0, 12).ParseFormatInfo, System.Globalization.NumberFormatInfo).CurrencyNegativePattern = 1
            CType(Me.spdParts_Sheet1.Cells.Get(0, 12).ParseFormatInfo, System.Globalization.NumberFormatInfo).CurrencySymbol = "¥"
            CType(Me.spdParts_Sheet1.Cells.Get(0, 12).ParseFormatInfo, System.Globalization.NumberFormatInfo).NaNSymbol = "NaN (非数値)"
            CType(Me.spdParts_Sheet1.Cells.Get(0, 12).ParseFormatInfo, System.Globalization.NumberFormatInfo).NegativeInfinitySymbol = "-∞"
            CType(Me.spdParts_Sheet1.Cells.Get(0, 12).ParseFormatInfo, System.Globalization.NumberFormatInfo).NumberDecimalDigits = 0
            CType(Me.spdParts_Sheet1.Cells.Get(0, 12).ParseFormatInfo, System.Globalization.NumberFormatInfo).PercentNegativePattern = 1
            CType(Me.spdParts_Sheet1.Cells.Get(0, 12).ParseFormatInfo, System.Globalization.NumberFormatInfo).PercentPositivePattern = 1
            CType(Me.spdParts_Sheet1.Cells.Get(0, 12).ParseFormatInfo, System.Globalization.NumberFormatInfo).PositiveInfinitySymbol = "+∞"
            Me.spdParts_Sheet1.Cells.Get(0, 12).ParseFormatString = "n"
            Me.spdParts_Sheet1.Cells.Get(0, 12).Value = 12
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).RowSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "集計対象"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).ColumnSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "期間"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).ColumnSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "台数"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 1).Value = "状態"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 2).Value = "区分"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 3).Value = "開発符号"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 4).Value = "イベント"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 5).Value = "イベント名称"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 6).Value = "予算"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 7).Value = "From"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 8).Value = "～"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 9).Value = "To"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 10).Value = "完成車"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 11).Value = "＋"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 12).Value = "WB車"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 13).Value = "主な変更概要"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 14).Value = "造り方及び製作条件"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 15).Value = "その他"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 16).Value = "予算イベントコード"
            Me.spdParts_Sheet1.ColumnHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
            Me.spdParts_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault"
            Me.spdParts_Sheet1.ColumnHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
            Me.spdParts_Sheet1.ColumnHeader.Rows.Get(1).Height = 30.0!
            Me.spdParts_Sheet1.Columns.Get(0).CellType = CheckBoxCellType1
            Me.spdParts_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(0).Width = 35.0!
            Me.spdParts_Sheet1.Columns.Get(1).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(1).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(1).Label = "状態"
            Me.spdParts_Sheet1.Columns.Get(1).Locked = True
            Me.spdParts_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(1).Width = 62.0!
            Me.spdParts_Sheet1.Columns.Get(2).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(2).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(2).Label = "区分"
            Me.spdParts_Sheet1.Columns.Get(2).Locked = True
            Me.spdParts_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(2).Width = 70.0!
            Me.spdParts_Sheet1.Columns.Get(3).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(3).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(3).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(3).Label = "開発符号"
            Me.spdParts_Sheet1.Columns.Get(3).Locked = True
            Me.spdParts_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(3).Width = 65.0!
            Me.spdParts_Sheet1.Columns.Get(4).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(4).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(4).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(4).Label = "イベント"
            Me.spdParts_Sheet1.Columns.Get(4).Locked = True
            Me.spdParts_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(4).Width = 120.0!
            Me.spdParts_Sheet1.Columns.Get(5).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(5).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(5).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(5).Label = "イベント名称"
            Me.spdParts_Sheet1.Columns.Get(5).Locked = True
            Me.spdParts_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(5).Width = 200.0!
            Me.spdParts_Sheet1.Columns.Get(6).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(6).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(6).CellType = TextCellType1
            Me.spdParts_Sheet1.Columns.Get(6).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(6).Label = "予算"
            Me.spdParts_Sheet1.Columns.Get(6).Locked = True
            Me.spdParts_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(6).Width = 56.0!
            Me.spdParts_Sheet1.Columns.Get(7).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(7).Label = "From"
            Me.spdParts_Sheet1.Columns.Get(7).Locked = True
            Me.spdParts_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(7).Width = 70.0!
            Me.spdParts_Sheet1.Columns.Get(8).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(8).Label = "～"
            Me.spdParts_Sheet1.Columns.Get(8).Locked = True
            Me.spdParts_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(8).Width = 18.0!
            Me.spdParts_Sheet1.Columns.Get(9).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(9).Label = "To"
            Me.spdParts_Sheet1.Columns.Get(9).Locked = True
            Me.spdParts_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(9).Width = 70.0!
            Me.spdParts_Sheet1.Columns.Get(10).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(10).Label = "完成車"
            Me.spdParts_Sheet1.Columns.Get(10).Locked = True
            Me.spdParts_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(10).Width = 45.0!
            Me.spdParts_Sheet1.Columns.Get(11).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(11).Label = "＋"
            Me.spdParts_Sheet1.Columns.Get(11).Locked = True
            Me.spdParts_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(11).Width = 18.0!
            Me.spdParts_Sheet1.Columns.Get(12).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(12).Label = "WB車"
            Me.spdParts_Sheet1.Columns.Get(12).Locked = True
            Me.spdParts_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(12).Width = 45.0!
            Me.spdParts_Sheet1.Columns.Get(13).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(13).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(13).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(13).Label = "主な変更概要"
            Me.spdParts_Sheet1.Columns.Get(13).Locked = True
            Me.spdParts_Sheet1.Columns.Get(13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(13).Width = 110.0!
            Me.spdParts_Sheet1.Columns.Get(14).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(14).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(14).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(14).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(14).Label = "造り方及び製作条件"
            Me.spdParts_Sheet1.Columns.Get(14).Locked = True
            Me.spdParts_Sheet1.Columns.Get(14).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(14).Width = 140.0!
            Me.spdParts_Sheet1.Columns.Get(15).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(15).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(15).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(15).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(15).Label = "その他"
            Me.spdParts_Sheet1.Columns.Get(15).Locked = True
            Me.spdParts_Sheet1.Columns.Get(15).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(15).Width = 120.0!
            Me.spdParts_Sheet1.DataAutoSizeColumns = False
            Me.spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdParts_Sheet1.RowHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
            Me.spdParts_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
            Me.spdParts_Sheet1.RowHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'spdParts
            '
            Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, "
            Me.spdParts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.spdParts.BackColor = System.Drawing.SystemColors.Control
            Me.spdParts.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentCell
            Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.Location = New System.Drawing.Point(7, 158)
            Me.spdParts.Name = "spdParts"
            Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
            Me.spdParts.Size = New System.Drawing.Size(1212, 413)
            Me.spdParts.TabIndex = 95
            Me.spdParts.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            '
            'Timer1
            '
            Me.Timer1.Enabled = True
            '
            'lblEvent
            '
            Me.lblEvent.AutoSize = True
            Me.lblEvent.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblEvent.Location = New System.Drawing.Point(225, 77)
            Me.lblEvent.Name = "lblEvent"
            Me.lblEvent.Size = New System.Drawing.Size(60, 15)
            Me.lblEvent.TabIndex = 90
            Me.lblEvent.Text = "イベント："
            '
            'lblKikan
            '
            Me.lblKikan.AutoSize = True
            Me.lblKikan.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblKikan.Location = New System.Drawing.Point(452, 77)
            Me.lblKikan.Name = "lblKikan"
            Me.lblKikan.Size = New System.Drawing.Size(60, 15)
            Me.lblKikan.TabIndex = 90
            Me.lblKikan.Text = "期　 間："
            '
            'cmbKaihatsuFugo
            '
            Me.cmbKaihatsuFugo.FormattingEnabled = True
            Me.cmbKaihatsuFugo.Location = New System.Drawing.Point(91, 95)
            Me.cmbKaihatsuFugo.Name = "cmbKaihatsuFugo"
            Me.cmbKaihatsuFugo.Size = New System.Drawing.Size(125, 20)
            Me.cmbKaihatsuFugo.TabIndex = 2
            '
            'cmbEvent
            '
            Me.cmbEvent.FormattingEnabled = True
            Me.cmbEvent.Location = New System.Drawing.Point(228, 95)
            Me.cmbEvent.Name = "cmbEvent"
            Me.cmbEvent.Size = New System.Drawing.Size(125, 20)
            Me.cmbEvent.TabIndex = 3
            '
            'cmbKikanFrom
            '
            Me.cmbKikanFrom.FormattingEnabled = True
            Me.cmbKikanFrom.Location = New System.Drawing.Point(452, 95)
            Me.cmbKikanFrom.Name = "cmbKikanFrom"
            Me.cmbKikanFrom.Size = New System.Drawing.Size(125, 20)
            Me.cmbKikanFrom.TabIndex = 5
            '
            'btnEdit
            '
            Me.btnEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.btnEdit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnEdit.Location = New System.Drawing.Point(18, 128)
            Me.btnEdit.Name = "btnEdit"
            Me.btnEdit.Size = New System.Drawing.Size(79, 24)
            Me.btnEdit.TabIndex = 108
            Me.btnEdit.TabStop = False
            Me.btnEdit.Text = "候補表示"
            Me.btnEdit.UseVisualStyleBackColor = False
            '
            'Panel2
            '
            Me.Panel2.AutoSize = True
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.btnExcelExport)
            Me.Panel2.Controls.Add(Me.LOGO)
            Me.Panel2.Controls.Add(Me.btnBack)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 30)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(1224, 32)
            Me.Panel2.TabIndex = 107
            '
            'btnExcelExport
            '
            Me.btnExcelExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnExcelExport.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnExcelExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnExcelExport.Location = New System.Drawing.Point(1049, 3)
            Me.btnExcelExport.Name = "btnExcelExport"
            Me.btnExcelExport.Size = New System.Drawing.Size(99, 24)
            Me.btnExcelExport.TabIndex = 83
            Me.btnExcelExport.TabStop = False
            Me.btnExcelExport.Text = "EXCEL出力"
            Me.btnExcelExport.UseVisualStyleBackColor = True
            '
            'LOGO
            '
            Me.LOGO.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LOGO.BackColor = System.Drawing.Color.White
            Me.LOGO.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LOGO.ForeColor = System.Drawing.Color.Yellow
            Me.LOGO.Image = Global.YosansyoTool.My.Resources.Resources.ロゴ
            Me.LOGO.Location = New System.Drawing.Point(407, 1)
            Me.LOGO.Name = "LOGO"
            Me.LOGO.Size = New System.Drawing.Size(309, 26)
            Me.LOGO.TabIndex = 82
            Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnBack
            '
            Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBack.Location = New System.Drawing.Point(1154, 2)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(64, 24)
            Me.btnBack.TabIndex = 54
            Me.btnBack.TabStop = False
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = True
            '
            'btnYosanShukeiExcelExport
            '
            Me.btnYosanShukeiExcelExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnYosanShukeiExcelExport.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnYosanShukeiExcelExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnYosanShukeiExcelExport.Location = New System.Drawing.Point(113, 128)
            Me.btnYosanShukeiExcelExport.Name = "btnYosanShukeiExcelExport"
            Me.btnYosanShukeiExcelExport.Size = New System.Drawing.Size(163, 24)
            Me.btnYosanShukeiExcelExport.TabIndex = 57
            Me.btnYosanShukeiExcelExport.TabStop = False
            Me.btnYosanShukeiExcelExport.Text = "集計開始(EXCEL出力)"
            Me.btnYosanShukeiExcelExport.UseVisualStyleBackColor = True
            '
            'Panel3
            '
            Me.Panel3.AutoSize = True
            Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel3.BackColor = System.Drawing.SystemColors.Control
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Controls.Add(Me.LblMessage)
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel3.Location = New System.Drawing.Point(0, 603)
            Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(1224, 31)
            Me.Panel3.TabIndex = 111
            '
            'LblMessage
            '
            Me.LblMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblMessage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.LblMessage.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblMessage.ForeColor = System.Drawing.Color.Red
            Me.LblMessage.Location = New System.Drawing.Point(-1, 1)
            Me.LblMessage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.LblMessage.Name = "LblMessage"
            Me.LblMessage.Size = New System.Drawing.Size(1224, 28)
            Me.LblMessage.TabIndex = 55
            Me.LblMessage.Text = "予算書イベントを選択後、編集または閲覧ボタンをクリックしてください。"
            Me.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'cmbKbn
            '
            Me.cmbKbn.FormattingEnabled = True
            Me.cmbKbn.Location = New System.Drawing.Point(18, 95)
            Me.cmbKbn.Name = "cmbKbn"
            Me.cmbKbn.Size = New System.Drawing.Size(60, 20)
            Me.cmbKbn.TabIndex = 1
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.Location = New System.Drawing.Point(15, 77)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(45, 15)
            Me.Label1.TabIndex = 112
            Me.Label1.Text = "区分："
            '
            'cmbYosanCode
            '
            Me.cmbYosanCode.FormattingEnabled = True
            Me.cmbYosanCode.Location = New System.Drawing.Point(366, 95)
            Me.cmbYosanCode.Name = "cmbYosanCode"
            Me.cmbYosanCode.Size = New System.Drawing.Size(75, 20)
            Me.cmbYosanCode.TabIndex = 4
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label2.Location = New System.Drawing.Point(363, 77)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(78, 15)
            Me.Label2.TabIndex = 114
            Me.Label2.Text = "予算コード："
            '
            'cmbKikanTo
            '
            Me.cmbKikanTo.FormattingEnabled = True
            Me.cmbKikanTo.Location = New System.Drawing.Point(604, 95)
            Me.cmbKikanTo.Name = "cmbKikanTo"
            Me.cmbKikanTo.Size = New System.Drawing.Size(125, 20)
            Me.cmbKikanTo.TabIndex = 6
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label3.Location = New System.Drawing.Point(580, 97)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(22, 15)
            Me.Label3.TabIndex = 117
            Me.Label3.Text = "～"
            '
            'FrmYosanEventListExcel
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1224, 634)
            Me.Controls.Add(Me.cmbYosanCode)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.cmbKikanTo)
            Me.Controls.Add(Me.cmbKbn)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.cmbKaihatsuFugo)
            Me.Controls.Add(Me.btnYosanShukeiExcelExport)
            Me.Controls.Add(Me.btnEdit)
            Me.Controls.Add(Me.cmbEvent)
            Me.Controls.Add(Me.cmbKikanFrom)
            Me.Controls.Add(Me.spdParts)
            Me.Controls.Add(Me.lblKaihatsuFugo)
            Me.Controls.Add(Me.lblEvent)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.lblKikan)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FrmYosanEventListExcel"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel2.ResumeLayout(False)
            Me.Panel3.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents LblLoginBukaName As System.Windows.Forms.Label
        Friend WithEvents LblDateNow As System.Windows.Forms.Label
        Friend WithEvents LblLoginUserId As System.Windows.Forms.Label
        Friend WithEvents LblTimeNow As System.Windows.Forms.Label
        Friend WithEvents LblCurrPGId As System.Windows.Forms.Label
        Friend WithEvents LblCurrPGName As System.Windows.Forms.Label
        Friend WithEvents lblKaihatsuFugo As System.Windows.Forms.Label
        Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents lblEvent As System.Windows.Forms.Label
        Friend WithEvents lblKikan As System.Windows.Forms.Label
        Friend WithEvents cmbKaihatsuFugo As System.Windows.Forms.ComboBox
        Friend WithEvents cmbEvent As System.Windows.Forms.ComboBox
        Friend WithEvents cmbKikanFrom As System.Windows.Forms.ComboBox
        Friend WithEvents btnEdit As System.Windows.Forms.Button
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents LOGO As System.Windows.Forms.Label
        Friend WithEvents btnYosanShukeiExcelExport As System.Windows.Forms.Button
        Friend WithEvents btnBack As System.Windows.Forms.Button
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents LblMessage As System.Windows.Forms.Label
        Friend WithEvents cmbKbn As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents cmbYosanCode As System.Windows.Forms.ComboBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents cmbKikanTo As System.Windows.Forms.ComboBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents btnExcelExport As System.Windows.Forms.Button
    End Class

End Namespace
