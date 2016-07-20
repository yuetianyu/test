Namespace EventEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm10DispEventCopy
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
            Dim cultureInfo As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ja-JP", False)
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm10DispEventCopy))
            Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.LOGO = New System.Windows.Forms.Label
            Me.BtnBack = New System.Windows.Forms.Button
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.Label4 = New System.Windows.Forms.Label
            Me.LblCurrBukaName = New System.Windows.Forms.Label
            Me.LblDateNow = New System.Windows.Forms.Label
            Me.LblCurrUserId = New System.Windows.Forms.Label
            Me.LblTimeNow = New System.Windows.Forms.Label
            Me.Label1 = New System.Windows.Forms.Label
            Me.spdEvent = New FarPoint.Win.Spread.FpSpread
            Me.spdEvent_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.BtnWrite = New System.Windows.Forms.Button
            Me.LblEventCode = New System.Windows.Forms.Label
            Me.TxtEventCode = New System.Windows.Forms.TextBox
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.Panel2.SuspendLayout()
            Me.Panel1.SuspendLayout()
            CType(Me.spdEvent, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdEvent_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Panel2
            '
            Me.Panel2.AutoSize = True
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.LOGO)
            Me.Panel2.Controls.Add(Me.BtnBack)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 34)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(785, 32)
            Me.Panel2.TabIndex = 57
            '
            'LOGO
            '
            Me.LOGO.BackColor = System.Drawing.Color.White
            Me.LOGO.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LOGO.ForeColor = System.Drawing.Color.Yellow
            Me.LOGO.Image = Global.EventSakusei.My.Resources.Resources.ロゴ
            Me.LOGO.Location = New System.Drawing.Point(323, 2)
            Me.LOGO.Name = "LOGO"
            Me.LOGO.Size = New System.Drawing.Size(136, 26)
            Me.LOGO.TabIndex = 83
            Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'BtnBack
            '
            Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnBack.Location = New System.Drawing.Point(715, 3)
            Me.BtnBack.Name = "BtnBack"
            Me.BtnBack.Size = New System.Drawing.Size(64, 24)
            Me.BtnBack.TabIndex = 56
            Me.BtnBack.Text = "戻る"
            Me.BtnBack.UseVisualStyleBackColor = True
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Controls.Add(Me.LblCurrBukaName)
            Me.Panel1.Controls.Add(Me.LblDateNow)
            Me.Panel1.Controls.Add(Me.LblCurrUserId)
            Me.Panel1.Controls.Add(Me.LblTimeNow)
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(785, 34)
            Me.Panel1.TabIndex = 56
            '
            'Label4
            '
            Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label4.ForeColor = System.Drawing.Color.Yellow
            Me.Label4.Location = New System.Drawing.Point(323, 3)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(139, 31)
            Me.Label4.TabIndex = 54
            Me.Label4.Text = "イベント登録コピー"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'LblCurrBukaName
            '
            Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
            Me.LblCurrBukaName.Location = New System.Drawing.Point(558, 15)
            Me.LblCurrBukaName.Name = "LblCurrBukaName"
            Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
            Me.LblCurrBukaName.TabIndex = 59
            Me.LblCurrBukaName.Text = "(部課：SKE1         )"
            '
            'LblDateNow
            '
            Me.LblDateNow.AutoSize = True
            Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblDateNow.ForeColor = System.Drawing.Color.White
            Me.LblDateNow.Location = New System.Drawing.Point(694, 2)
            Me.LblDateNow.Name = "LblDateNow"
            Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
            Me.LblDateNow.TabIndex = 56
            Me.LblDateNow.Text = "YYYY/MM/DD"
            '
            'LblCurrUserId
            '
            Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
            Me.LblCurrUserId.Location = New System.Drawing.Point(558, 2)
            Me.LblCurrUserId.Name = "LblCurrUserId"
            Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
            Me.LblCurrUserId.TabIndex = 58
            Me.LblCurrUserId.Text = "(ID    ：ABCDEFGH)"
            '
            'LblTimeNow
            '
            Me.LblTimeNow.AutoSize = True
            Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblTimeNow.ForeColor = System.Drawing.Color.White
            Me.LblTimeNow.Location = New System.Drawing.Point(718, 15)
            Me.LblTimeNow.Name = "LblTimeNow"
            Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
            Me.LblTimeNow.TabIndex = 55
            Me.LblTimeNow.Text = "HH:MM:DD"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.White
            Me.Label1.Location = New System.Drawing.Point(4, 4)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(113, 13)
            Me.Label1.TabIndex = 54
            Me.Label1.Text = "PG-ID：XXXXXXXX"
            '
            'spdEvent
            '
            Me.spdEvent.AccessibleDescription = "spdEvent, Sheet1, Row 0, Column 0, MF1-D-T-001"
            Me.spdEvent.AllowColumnMove = True
            Me.spdEvent.AllowDragDrop = True
            Me.spdEvent.AllowDragFill = True
            Me.spdEvent.AllowUserFormulas = True
            Me.spdEvent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.spdEvent.BackColor = System.Drawing.SystemColors.Control
            Me.spdEvent.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdEvent.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdEvent.Location = New System.Drawing.Point(4, 118)
            Me.spdEvent.Name = "spdEvent"
            Me.spdEvent.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdEvent.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdEvent_Sheet1})
            Me.spdEvent.Size = New System.Drawing.Size(776, 359)
            Me.spdEvent.TabIndex = 60
            Me.spdEvent.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdEvent.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdEvent_Sheet1
            '
            Me.spdEvent_Sheet1.Reset()
            Me.spdEvent_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdEvent_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdEvent_Sheet1.ColumnCount = 10
            Me.spdEvent_Sheet1.RowCount = 10
            Me.spdEvent_Sheet1.AllowNoteEdit = True
            Me.spdEvent_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdEvent_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdEvent_Sheet1.Cells.Get(0, 0).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(0, 0).Value = "MF1-D-T-001"
            Me.spdEvent_Sheet1.Cells.Get(0, 1).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(0, 1).Value = "MF1"
            Me.spdEvent_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdEvent_Sheet1.Cells.Get(0, 2).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(0, 2).Value = "台車"
            Me.spdEvent_Sheet1.Cells.Get(0, 3).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(0, 3).Value = "T"
            Me.spdEvent_Sheet1.Cells.Get(0, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(0, 4).Value = "台車Ⅰ（トリム）"
            Me.spdEvent_Sheet1.Cells.Get(0, 5).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(0, 5).Value = "10+9"
            Me.spdEvent_Sheet1.Cells.Get(0, 6).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(0, 6).Value = "有"
            Me.spdEvent_Sheet1.Cells.Get(0, 7).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(0, 7).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
            Me.spdEvent_Sheet1.Cells.Get(0, 7).ParseFormatString = "yyyy/M/d"
            Me.spdEvent_Sheet1.Cells.Get(0, 7).Value = New Date(2010, 4, 20, 0, 0, 0, 0)
            Me.spdEvent_Sheet1.Cells.Get(0, 8).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(0, 8).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
            Me.spdEvent_Sheet1.Cells.Get(0, 8).ParseFormatString = "yyyy/M/d"
            Me.spdEvent_Sheet1.Cells.Get(0, 8).Value = New Date(2010, 4, 28, 0, 0, 0, 0)
            Me.spdEvent_Sheet1.Cells.Get(0, 9).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(0, 9).Value = "改訂受付中"
            Me.spdEvent_Sheet1.Cells.Get(1, 0).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(1, 0).Value = "MF1-D-M-001"
            Me.spdEvent_Sheet1.Cells.Get(1, 1).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(1, 1).Value = "MF1"
            Me.spdEvent_Sheet1.Cells.Get(1, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdEvent_Sheet1.Cells.Get(1, 2).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(1, 2).Value = "台車"
            Me.spdEvent_Sheet1.Cells.Get(1, 3).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(1, 3).Value = "M"
            Me.spdEvent_Sheet1.Cells.Get(1, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(1, 4).Value = "台車Ⅰ（メタル）"
            Me.spdEvent_Sheet1.Cells.Get(1, 5).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(1, 5).Value = "19+0"
            Me.spdEvent_Sheet1.Cells.Get(1, 6).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(1, 6).Value = "有"
            Me.spdEvent_Sheet1.Cells.Get(1, 7).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(1, 8).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(1, 9).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(2, 0).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(2, 0).Value = "MF1-D-S-002"
            Me.spdEvent_Sheet1.Cells.Get(2, 1).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(2, 1).Value = "MF1"
            Me.spdEvent_Sheet1.Cells.Get(2, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdEvent_Sheet1.Cells.Get(2, 2).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(2, 2).Value = "台車"
            Me.spdEvent_Sheet1.Cells.Get(2, 3).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(2, 3).Value = "S"
            Me.spdEvent_Sheet1.Cells.Get(2, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(2, 4).Value = "台車Ⅱ"
            Me.spdEvent_Sheet1.Cells.Get(2, 5).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(2, 5).Value = "0+20"
            Me.spdEvent_Sheet1.Cells.Get(2, 6).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(2, 6).Value = "有"
            Me.spdEvent_Sheet1.Cells.Get(2, 7).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(2, 7).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
            Me.spdEvent_Sheet1.Cells.Get(2, 7).ParseFormatString = "yyyy/M/d"
            Me.spdEvent_Sheet1.Cells.Get(2, 7).Value = New Date(2010, 5, 20, 0, 0, 0, 0)
            Me.spdEvent_Sheet1.Cells.Get(2, 8).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(2, 8).ParseFormatInfo = CType(cultureInfo.DateTimeFormat.Clone, System.Globalization.DateTimeFormatInfo)
            Me.spdEvent_Sheet1.Cells.Get(2, 8).ParseFormatString = "yyyy/M/d"
            Me.spdEvent_Sheet1.Cells.Get(2, 8).Value = New Date(2010, 6, 3, 0, 0, 0, 0)
            Me.spdEvent_Sheet1.Cells.Get(2, 9).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(2, 9).Value = "設計メンテ中"
            Me.spdEvent_Sheet1.Cells.Get(3, 0).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(3, 0).Value = "AS1-K-T-001"
            Me.spdEvent_Sheet1.Cells.Get(3, 1).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(3, 1).Value = "AS1"
            Me.spdEvent_Sheet1.Cells.Get(3, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdEvent_Sheet1.Cells.Get(3, 2).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(3, 2).Value = "開発完了確認車"
            Me.spdEvent_Sheet1.Cells.Get(3, 3).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(3, 3).Value = "T"
            Me.spdEvent_Sheet1.Cells.Get(3, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(3, 4).Value = "開発完了確認車（トリム）"
            Me.spdEvent_Sheet1.Cells.Get(3, 5).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(3, 5).Value = "70+21"
            Me.spdEvent_Sheet1.Cells.Get(3, 6).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(3, 6).Value = "有"
            Me.spdEvent_Sheet1.Cells.Get(3, 7).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(3, 8).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(3, 9).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(4, 0).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(4, 1).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(4, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdEvent_Sheet1.Cells.Get(4, 2).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(4, 3).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(4, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(4, 5).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(4, 6).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(4, 7).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(4, 8).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(4, 9).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(5, 0).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(5, 1).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(5, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdEvent_Sheet1.Cells.Get(5, 2).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(5, 3).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(5, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(5, 5).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(5, 6).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(5, 7).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(5, 8).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(5, 9).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(6, 0).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(6, 1).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(6, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdEvent_Sheet1.Cells.Get(6, 2).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(6, 3).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(6, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(6, 5).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(6, 6).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(6, 7).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(6, 8).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(6, 9).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(7, 0).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(7, 1).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(7, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdEvent_Sheet1.Cells.Get(7, 2).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(7, 3).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(7, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(7, 5).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(7, 6).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(7, 7).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(7, 8).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(7, 9).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(8, 0).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(8, 1).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(8, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdEvent_Sheet1.Cells.Get(8, 2).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(8, 3).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(8, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(8, 5).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(8, 6).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(8, 7).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(8, 8).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(8, 9).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(9, 0).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(9, 1).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(9, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdEvent_Sheet1.Cells.Get(9, 2).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(9, 3).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(9, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(9, 5).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(9, 6).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(9, 7).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(9, 8).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(9, 9).Locked = True
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.SystemColors.Control
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "イベントコード"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.SystemColors.Control
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "開発符号"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.SystemColors.Control
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "イベント"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.SystemColors.Control
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "T/M"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.SystemColors.Control
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "イベント名称"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.SystemColors.Control
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "台数"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.SystemColors.Control
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "発注"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.SystemColors.Control
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "設計展開日"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.SystemColors.Control
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "訂正処置〆切日"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.SystemColors.Control
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "状態"
            Me.spdEvent_Sheet1.ColumnHeader.Rows.Get(0).Height = 30.0!
            Me.spdEvent_Sheet1.Columns.Get(0).CellType = TextCellType1
            Me.spdEvent_Sheet1.Columns.Get(0).Label = "イベントコード"
            Me.spdEvent_Sheet1.Columns.Get(0).Locked = False
            Me.spdEvent_Sheet1.Columns.Get(0).Tag = "BUHIN_NO"
            Me.spdEvent_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
            Me.spdEvent_Sheet1.Columns.Get(0).Width = 96.0!
            Me.spdEvent_Sheet1.Columns.Get(1).AllowAutoFilter = True
            Me.spdEvent_Sheet1.Columns.Get(1).AllowAutoSort = True
            Me.spdEvent_Sheet1.Columns.Get(1).CellType = TextCellType2
            Me.spdEvent_Sheet1.Columns.Get(1).Label = "開発符号"
            Me.spdEvent_Sheet1.Columns.Get(1).Locked = False
            Me.spdEvent_Sheet1.Columns.Get(1).Tag = "HOJO_NAME"
            Me.spdEvent_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdEvent_Sheet1.Columns.Get(1).Width = 64.0!
            Me.spdEvent_Sheet1.Columns.Get(2).AllowAutoFilter = True
            Me.spdEvent_Sheet1.Columns.Get(2).AllowAutoSort = True
            Me.spdEvent_Sheet1.Columns.Get(2).CellType = TextCellType3
            Me.spdEvent_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdEvent_Sheet1.Columns.Get(2).Label = "イベント"
            Me.spdEvent_Sheet1.Columns.Get(2).Locked = False
            Me.spdEvent_Sheet1.Columns.Get(2).Tag = "UPDATED_DATETIME"
            Me.spdEvent_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdEvent_Sheet1.Columns.Get(2).Width = 115.0!
            Me.spdEvent_Sheet1.Columns.Get(3).AllowAutoFilter = True
            Me.spdEvent_Sheet1.Columns.Get(3).AllowAutoSort = True
            Me.spdEvent_Sheet1.Columns.Get(3).CellType = TextCellType4
            Me.spdEvent_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdEvent_Sheet1.Columns.Get(3).Label = "T/M"
            Me.spdEvent_Sheet1.Columns.Get(3).Locked = False
            Me.spdEvent_Sheet1.Columns.Get(3).Tag = "UPDATED_USER_ID"
            Me.spdEvent_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdEvent_Sheet1.Columns.Get(3).Width = 62.0!
            Me.spdEvent_Sheet1.Columns.Get(4).Label = "イベント名称"
            Me.spdEvent_Sheet1.Columns.Get(4).Locked = False
            Me.spdEvent_Sheet1.Columns.Get(4).Width = 220.0!
            Me.spdEvent_Sheet1.Columns.Get(5).Label = "台数"
            Me.spdEvent_Sheet1.Columns.Get(5).Locked = False
            Me.spdEvent_Sheet1.Columns.Get(5).Width = 51.0!
            Me.spdEvent_Sheet1.Columns.Get(6).Label = "発注"
            Me.spdEvent_Sheet1.Columns.Get(6).Locked = False
            Me.spdEvent_Sheet1.Columns.Get(6).Width = 38.0!
            Me.spdEvent_Sheet1.Columns.Get(7).Label = "設計展開日"
            Me.spdEvent_Sheet1.Columns.Get(7).Locked = False
            Me.spdEvent_Sheet1.Columns.Get(7).Width = 72.0!
            Me.spdEvent_Sheet1.Columns.Get(8).Label = "訂正処置〆切日"
            Me.spdEvent_Sheet1.Columns.Get(8).Locked = False
            Me.spdEvent_Sheet1.Columns.Get(8).Width = 105.0!
            Me.spdEvent_Sheet1.Columns.Get(9).AllowAutoFilter = True
            Me.spdEvent_Sheet1.Columns.Get(9).AllowAutoSort = True
            Me.spdEvent_Sheet1.Columns.Get(9).CellType = TextCellType5
            Me.spdEvent_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdEvent_Sheet1.Columns.Get(9).Label = "状態"
            Me.spdEvent_Sheet1.Columns.Get(9).Locked = False
            Me.spdEvent_Sheet1.Columns.Get(9).Tag = "UPDATED_USER_ID"
            Me.spdEvent_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdEvent_Sheet1.Columns.Get(9).Width = 85.0!
            Me.spdEvent_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdEvent_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdEvent_Sheet1.Rows.Default.Height = 16.0!
            Me.spdEvent_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'BtnWrite
            '
            Me.BtnWrite.BackColor = System.Drawing.Color.LightCyan
            Me.BtnWrite.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnWrite.Location = New System.Drawing.Point(113, 90)
            Me.BtnWrite.Name = "BtnWrite"
            Me.BtnWrite.Size = New System.Drawing.Size(79, 24)
            Me.BtnWrite.TabIndex = 61
            Me.BtnWrite.Text = "書込み"
            Me.ToolTip1.SetToolTip(Me.BtnWrite, "イベント情報をコピーします。")
            Me.BtnWrite.UseVisualStyleBackColor = False
            '
            'LblEventCode
            '
            Me.LblEventCode.AutoSize = True
            Me.LblEventCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblEventCode.Location = New System.Drawing.Point(4, 72)
            Me.LblEventCode.Name = "LblEventCode"
            Me.LblEventCode.Size = New System.Drawing.Size(93, 15)
            Me.LblEventCode.TabIndex = 63
            Me.LblEventCode.Text = "イベントコード："
            '
            'TxtEventCode
            '
            Me.TxtEventCode.BackColor = System.Drawing.Color.White
            Me.TxtEventCode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.TxtEventCode.ForeColor = System.Drawing.Color.Navy
            Me.TxtEventCode.Location = New System.Drawing.Point(7, 90)
            Me.TxtEventCode.Name = "TxtEventCode"
            Me.TxtEventCode.ReadOnly = True
            Me.TxtEventCode.Size = New System.Drawing.Size(100, 22)
            Me.TxtEventCode.TabIndex = 62
            '
            'Frm10DispEventCopy
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(785, 492)
            Me.Controls.Add(Me.LblEventCode)
            Me.Controls.Add(Me.TxtEventCode)
            Me.Controls.Add(Me.BtnWrite)
            Me.Controls.Add(Me.spdEvent)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "Frm10DispEventCopy"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel2.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            CType(Me.spdEvent, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdEvent_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
        Friend WithEvents LblDateNow As System.Windows.Forms.Label
        Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
        Friend WithEvents LblTimeNow As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents spdEvent As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdEvent_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents BtnWrite As System.Windows.Forms.Button
        Friend WithEvents LblEventCode As System.Windows.Forms.Label
        Friend WithEvents TxtEventCode As System.Windows.Forms.TextBox
        Friend WithEvents BtnBack As System.Windows.Forms.Button
        Friend WithEvents LOGO As System.Windows.Forms.Label
        Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    End Class
End Namespace
