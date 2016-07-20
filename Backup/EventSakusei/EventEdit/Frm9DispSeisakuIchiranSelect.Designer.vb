Namespace EventEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm9DispSeisakuIchiranSelect
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
            Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType6 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType7 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType8 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType9 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType10 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm9DispSeisakuIchiranSelect))
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.LblCurrBukaName = New System.Windows.Forms.Label
            Me.LblCurrUserId = New System.Windows.Forms.Label
            Me.LblDateNow = New System.Windows.Forms.Label
            Me.LblCurrPGId = New System.Windows.Forms.Label
            Me.LblTimeNow = New System.Windows.Forms.Label
            Me.Label4 = New System.Windows.Forms.Label
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.spdParts = New FarPoint.Win.Spread.FpSpread
            Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.cmbEventName = New System.Windows.Forms.ComboBox
            Me.Label2 = New System.Windows.Forms.Label
            Me.cmbEvent = New System.Windows.Forms.ComboBox
            Me.Label5 = New System.Windows.Forms.Label
            Me.cmbKaihatsuFugo = New System.Windows.Forms.ComboBox
            Me.Label1 = New System.Windows.Forms.Label
            Me.Panel5 = New System.Windows.Forms.Panel
            Me.Label3 = New System.Windows.Forms.Label
            Me.rbtStatus02 = New System.Windows.Forms.RadioButton
            Me.rbtStatus01 = New System.Windows.Forms.RadioButton
            Me.cmbHakouNo = New System.Windows.Forms.ComboBox
            Me.btnSeisakuIchiranTorikomi = New System.Windows.Forms.Button
            Me.ToolStripPanel1 = New System.Windows.Forms.ToolStripPanel
            Me.Label21 = New System.Windows.Forms.Label
            Me.lblKaihatuFugou = New System.Windows.Forms.Label
            Me.Panel4 = New System.Windows.Forms.Panel
            Me.LOGO = New System.Windows.Forms.Label
            Me.btnEND = New System.Windows.Forms.Button
            Me.btnBACK = New System.Windows.Forms.Button
            Me.Panel6 = New System.Windows.Forms.Panel
            Me.LblMessage = New System.Windows.Forms.Label
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.Panel1.SuspendLayout()
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel2.SuspendLayout()
            Me.Panel5.SuspendLayout()
            Me.Panel4.SuspendLayout()
            Me.Panel6.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.LblCurrBukaName)
            Me.Panel1.Controls.Add(Me.LblCurrUserId)
            Me.Panel1.Controls.Add(Me.LblDateNow)
            Me.Panel1.Controls.Add(Me.LblCurrPGId)
            Me.Panel1.Controls.Add(Me.LblTimeNow)
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(969, 32)
            Me.Panel1.TabIndex = 54
            '
            'LblCurrBukaName
            '
            Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
            Me.LblCurrBukaName.Location = New System.Drawing.Point(743, 14)
            Me.LblCurrBukaName.Name = "LblCurrBukaName"
            Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
            Me.LblCurrBukaName.TabIndex = 77
            '
            'LblCurrUserId
            '
            Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
            Me.LblCurrUserId.Location = New System.Drawing.Point(743, 1)
            Me.LblCurrUserId.Name = "LblCurrUserId"
            Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
            Me.LblCurrUserId.TabIndex = 76
            '
            'LblDateNow
            '
            Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblDateNow.AutoSize = True
            Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblDateNow.ForeColor = System.Drawing.Color.White
            Me.LblDateNow.Location = New System.Drawing.Point(880, 1)
            Me.LblDateNow.Name = "LblDateNow"
            Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
            Me.LblDateNow.TabIndex = 60
            Me.LblDateNow.Text = "YYYY/MM/DD"
            '
            'LblCurrPGId
            '
            Me.LblCurrPGId.AutoSize = True
            Me.LblCurrPGId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrPGId.ForeColor = System.Drawing.Color.White
            Me.LblCurrPGId.Location = New System.Drawing.Point(3, 1)
            Me.LblCurrPGId.Name = "LblCurrPGId"
            Me.LblCurrPGId.Size = New System.Drawing.Size(113, 13)
            Me.LblCurrPGId.TabIndex = 59
            Me.LblCurrPGId.Text = "PG-ID：XXXXXXXX"
            '
            'LblTimeNow
            '
            Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblTimeNow.AutoSize = True
            Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblTimeNow.ForeColor = System.Drawing.Color.White
            Me.LblTimeNow.Location = New System.Drawing.Point(904, 14)
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
            Me.Label4.Location = New System.Drawing.Point(0, 1)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(966, 31)
            Me.Label4.TabIndex = 54
            Me.Label4.Text = "製作一覧選択・取込"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Panel3
            '
            Me.Panel3.AutoSize = True
            Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel3.BackColor = System.Drawing.SystemColors.Control
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel3.Location = New System.Drawing.Point(0, 666)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(969, 2)
            Me.Panel3.TabIndex = 71
            '
            'spdParts
            '
            Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, "
            Me.spdParts.AllowDragFill = True
            Me.spdParts.AllowUserFormulas = True
            Me.spdParts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.spdParts.BackColor = System.Drawing.SystemColors.Control
            Me.spdParts.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentCell
            Me.spdParts.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.Location = New System.Drawing.Point(3, 132)
            Me.spdParts.Name = "spdParts"
            Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
            Me.spdParts.Size = New System.Drawing.Size(962, 503)
            Me.spdParts.TabIndex = 8
            Me.spdParts.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Floating
            Me.spdParts.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdParts_Sheet1
            '
            Me.spdParts_Sheet1.Reset()
            Me.spdParts_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdParts_Sheet1.ColumnCount = 11
            Me.spdParts_Sheet1.ColumnHeader.RowCount = 2
            Me.spdParts_Sheet1.RowCount = 100
            Me.spdParts_Sheet1.AllowNoteEdit = True
            Me.spdParts_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdParts_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdParts_Sheet1.AutoGenerateColumns = False
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).RowSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "発行№"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Locked = False
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).RowSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "開発符号"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).RowSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "イベント"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).RowSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "イベント名"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).RowSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "改訂№"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).RowSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "登録日"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "台数"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = " "
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).RowSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "ステータス"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).RowSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "状態"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 6).Value = "完成車"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 7).Value = "＋"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 8).Value = "W/B"
            Me.spdParts_Sheet1.ColumnHeader.Rows.Get(0).Height = 24.0!
            Me.spdParts_Sheet1.Columns.Get(0).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(0).CellType = TextCellType1
            Me.spdParts_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(0).Locked = True
            Me.spdParts_Sheet1.Columns.Get(0).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(0).Tag = "HAKOU_NO"
            Me.spdParts_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(0).Width = 120.0!
            Me.spdParts_Sheet1.Columns.Get(1).CellType = TextCellType2
            Me.spdParts_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(1).Locked = True
            Me.spdParts_Sheet1.Columns.Get(1).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(1).Tag = "KAIHATSU_FUGO"
            Me.spdParts_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(2).CellType = TextCellType3
            Me.spdParts_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(2).Locked = True
            Me.spdParts_Sheet1.Columns.Get(2).Tag = "Event"
            Me.spdParts_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(2).VisualStyles = FarPoint.Win.VisualStyles.[Auto]
            Me.spdParts_Sheet1.Columns.Get(2).Width = 120.0!
            Me.spdParts_Sheet1.Columns.Get(3).CellType = TextCellType4
            Me.spdParts_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(3).Locked = True
            Me.spdParts_Sheet1.Columns.Get(3).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(3).Tag = "EventName"
            Me.spdParts_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(3).VisualStyles = FarPoint.Win.VisualStyles.[Auto]
            Me.spdParts_Sheet1.Columns.Get(3).Width = 200.0!
            Me.spdParts_Sheet1.Columns.Get(4).CellType = TextCellType5
            Me.spdParts_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(4).Locked = True
            Me.spdParts_Sheet1.Columns.Get(4).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(4).Tag = "KaiteiNo"
            Me.spdParts_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(4).Width = 56.0!
            Me.spdParts_Sheet1.Columns.Get(5).CellType = TextCellType6
            Me.spdParts_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(5).Locked = True
            Me.spdParts_Sheet1.Columns.Get(5).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(5).Tag = "TourokuBi"
            Me.spdParts_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(5).Width = 100.0!
            Me.spdParts_Sheet1.Columns.Get(6).CellType = TextCellType7
            Me.spdParts_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(6).Label = "完成車"
            Me.spdParts_Sheet1.Columns.Get(6).Locked = True
            Me.spdParts_Sheet1.Columns.Get(6).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(6).Tag = "DaisuKansei"
            Me.spdParts_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(6).Width = 50.0!
            Me.spdParts_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(7).Label = "＋"
            Me.spdParts_Sheet1.Columns.Get(7).Width = 20.0!
            Me.spdParts_Sheet1.Columns.Get(8).CellType = TextCellType8
            Me.spdParts_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(8).Label = "W/B"
            Me.spdParts_Sheet1.Columns.Get(8).Locked = True
            Me.spdParts_Sheet1.Columns.Get(8).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(8).Tag = "DaisuWb"
            Me.spdParts_Sheet1.Columns.Get(8).Width = 50.0!
            Me.spdParts_Sheet1.Columns.Get(9).CellType = TextCellType9
            Me.spdParts_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(9).Locked = True
            Me.spdParts_Sheet1.Columns.Get(9).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(9).Tag = "Status"
            Me.spdParts_Sheet1.Columns.Get(9).Width = 70.0!
            Me.spdParts_Sheet1.Columns.Get(10).CellType = TextCellType10
            Me.spdParts_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(10).Locked = True
            Me.spdParts_Sheet1.Columns.Get(10).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(10).Tag = "Jyotai"
            Me.spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdParts_Sheet1.RowHeader.DefaultStyle.Locked = False
            Me.spdParts_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
            Me.spdParts_Sheet1.Rows.Default.Height = 16.0!
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'Panel2
            '
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.cmbEventName)
            Me.Panel2.Controls.Add(Me.Label2)
            Me.Panel2.Controls.Add(Me.cmbEvent)
            Me.Panel2.Controls.Add(Me.Label5)
            Me.Panel2.Controls.Add(Me.cmbKaihatsuFugo)
            Me.Panel2.Controls.Add(Me.Label1)
            Me.Panel2.Controls.Add(Me.Panel5)
            Me.Panel2.Controls.Add(Me.cmbHakouNo)
            Me.Panel2.Controls.Add(Me.btnSeisakuIchiranTorikomi)
            Me.Panel2.Controls.Add(Me.ToolStripPanel1)
            Me.Panel2.Controls.Add(Me.Label21)
            Me.Panel2.Controls.Add(Me.lblKaihatuFugou)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 62)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(969, 64)
            Me.Panel2.TabIndex = 83
            '
            'cmbEventName
            '
            Me.cmbEventName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbEventName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbEventName.FormattingEnabled = True
            Me.cmbEventName.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.cmbEventName.Items.AddRange(New Object() {"", "B", "G", "R", "S", "W", "Y", "Z"})
            Me.cmbEventName.Location = New System.Drawing.Point(340, 30)
            Me.cmbEventName.Name = "cmbEventName"
            Me.cmbEventName.Size = New System.Drawing.Size(305, 23)
            Me.cmbEventName.TabIndex = 169
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label2.Location = New System.Drawing.Point(264, 33)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(80, 16)
            Me.Label2.TabIndex = 170
            Me.Label2.Text = "イベント名："
            '
            'cmbEvent
            '
            Me.cmbEvent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbEvent.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbEvent.FormattingEnabled = True
            Me.cmbEvent.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.cmbEvent.Items.AddRange(New Object() {"", "B", "G", "R", "S", "W", "Y", "Z"})
            Me.cmbEvent.Location = New System.Drawing.Point(340, 6)
            Me.cmbEvent.Name = "cmbEvent"
            Me.cmbEvent.Size = New System.Drawing.Size(212, 23)
            Me.cmbEvent.TabIndex = 167
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label5.Location = New System.Drawing.Point(264, 9)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(64, 16)
            Me.Label5.TabIndex = 168
            Me.Label5.Text = "イベント："
            '
            'cmbKaihatsuFugo
            '
            Me.cmbKaihatsuFugo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbKaihatsuFugo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbKaihatsuFugo.FormattingEnabled = True
            Me.cmbKaihatsuFugo.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.cmbKaihatsuFugo.Items.AddRange(New Object() {"", "B", "G", "R", "S", "W", "Y", "Z"})
            Me.cmbKaihatsuFugo.Location = New System.Drawing.Point(98, 30)
            Me.cmbKaihatsuFugo.Name = "cmbKaihatsuFugo"
            Me.cmbKaihatsuFugo.Size = New System.Drawing.Size(65, 23)
            Me.cmbKaihatsuFugo.TabIndex = 165
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.Location = New System.Drawing.Point(22, 32)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(80, 16)
            Me.Label1.TabIndex = 166
            Me.Label1.Text = "開発符号："
            '
            'Panel5
            '
            Me.Panel5.Controls.Add(Me.Label3)
            Me.Panel5.Controls.Add(Me.rbtStatus02)
            Me.Panel5.Controls.Add(Me.rbtStatus01)
            Me.Panel5.Location = New System.Drawing.Point(659, 3)
            Me.Panel5.Name = "Panel5"
            Me.Panel5.Size = New System.Drawing.Size(250, 24)
            Me.Panel5.TabIndex = 163
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label3.Location = New System.Drawing.Point(3, 4)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(76, 16)
            Me.Label3.TabIndex = 2
            Me.Label3.Text = "ステータス："
            '
            'rbtStatus02
            '
            Me.rbtStatus02.AutoSize = True
            Me.rbtStatus02.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.rbtStatus02.Location = New System.Drawing.Point(184, 2)
            Me.rbtStatus02.Name = "rbtStatus02"
            Me.rbtStatus02.Size = New System.Drawing.Size(55, 20)
            Me.rbtStatus02.TabIndex = 8
            Me.rbtStatus02.Text = "全て"
            Me.rbtStatus02.UseVisualStyleBackColor = True
            '
            'rbtStatus01
            '
            Me.rbtStatus01.AutoSize = True
            Me.rbtStatus01.Checked = True
            Me.rbtStatus01.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.rbtStatus01.Location = New System.Drawing.Point(79, 2)
            Me.rbtStatus01.Name = "rbtStatus01"
            Me.rbtStatus01.Size = New System.Drawing.Size(88, 20)
            Me.rbtStatus01.TabIndex = 7
            Me.rbtStatus01.TabStop = True
            Me.rbtStatus01.Text = "承認済み"
            Me.rbtStatus01.UseVisualStyleBackColor = True
            '
            'cmbHakouNo
            '
            Me.cmbHakouNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.cmbHakouNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbHakouNo.FormattingEnabled = True
            Me.cmbHakouNo.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.cmbHakouNo.Items.AddRange(New Object() {"", "B", "G", "R", "S", "W", "Y", "Z"})
            Me.cmbHakouNo.Location = New System.Drawing.Point(98, 6)
            Me.cmbHakouNo.Name = "cmbHakouNo"
            Me.cmbHakouNo.Size = New System.Drawing.Size(160, 23)
            Me.cmbHakouNo.TabIndex = 1
            '
            'btnSeisakuIchiranTorikomi
            '
            Me.btnSeisakuIchiranTorikomi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnSeisakuIchiranTorikomi.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
            Me.btnSeisakuIchiranTorikomi.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnSeisakuIchiranTorikomi.ImageAlign = System.Drawing.ContentAlignment.TopLeft
            Me.btnSeisakuIchiranTorikomi.Location = New System.Drawing.Point(659, 30)
            Me.btnSeisakuIchiranTorikomi.Name = "btnSeisakuIchiranTorikomi"
            Me.btnSeisakuIchiranTorikomi.Size = New System.Drawing.Size(110, 26)
            Me.btnSeisakuIchiranTorikomi.TabIndex = 10
            Me.btnSeisakuIchiranTorikomi.Text = "取込"
            Me.ToolTip1.SetToolTip(Me.btnSeisakuIchiranTorikomi, "製作一覧を取込みます。")
            Me.btnSeisakuIchiranTorikomi.UseVisualStyleBackColor = False
            '
            'ToolStripPanel1
            '
            Me.ToolStripPanel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.ToolStripPanel1.Location = New System.Drawing.Point(0, 0)
            Me.ToolStripPanel1.Name = "ToolStripPanel1"
            Me.ToolStripPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal
            Me.ToolStripPanel1.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
            Me.ToolStripPanel1.Size = New System.Drawing.Size(967, 0)
            '
            'Label21
            '
            Me.Label21.AutoSize = True
            Me.Label21.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label21.Location = New System.Drawing.Point(22, 8)
            Me.Label21.Name = "Label21"
            Me.Label21.Size = New System.Drawing.Size(64, 16)
            Me.Label21.TabIndex = 146
            Me.Label21.Text = "発行№："
            '
            'lblKaihatuFugou
            '
            Me.lblKaihatuFugou.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblKaihatuFugou.Location = New System.Drawing.Point(70, 6)
            Me.lblKaihatuFugou.Name = "lblKaihatuFugou"
            Me.lblKaihatuFugou.Size = New System.Drawing.Size(54, 20)
            Me.lblKaihatuFugou.TabIndex = 144
            Me.lblKaihatuFugou.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Panel4
            '
            Me.Panel4.AutoSize = True
            Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel4.Controls.Add(Me.LOGO)
            Me.Panel4.Controls.Add(Me.btnEND)
            Me.Panel4.Controls.Add(Me.btnBACK)
            Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel4.Location = New System.Drawing.Point(0, 32)
            Me.Panel4.Name = "Panel4"
            Me.Panel4.Size = New System.Drawing.Size(969, 30)
            Me.Panel4.TabIndex = 84
            '
            'LOGO
            '
            Me.LOGO.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LOGO.BackColor = System.Drawing.Color.White
            Me.LOGO.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LOGO.ForeColor = System.Drawing.Color.Yellow
            Me.LOGO.Image = Global.EventSakusei.My.Resources.Resources.ロゴ
            Me.LOGO.Location = New System.Drawing.Point(407, 2)
            Me.LOGO.Name = "LOGO"
            Me.LOGO.Size = New System.Drawing.Size(146, 26)
            Me.LOGO.TabIndex = 83
            Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnEND
            '
            Me.btnEND.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnEND.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnEND.Location = New System.Drawing.Point(5, 3)
            Me.btnEND.Name = "btnEND"
            Me.btnEND.Size = New System.Drawing.Size(149, 24)
            Me.btnEND.TabIndex = 10
            Me.btnEND.Text = "アプリケーション終了"
            Me.ToolTip1.SetToolTip(Me.btnEND, "製作一覧システムを終了します。")
            Me.btnEND.UseVisualStyleBackColor = True
            Me.btnEND.Visible = False
            '
            'btnBACK
            '
            Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBACK.Location = New System.Drawing.Point(874, 3)
            Me.btnBACK.Name = "btnBACK"
            Me.btnBACK.Size = New System.Drawing.Size(91, 24)
            Me.btnBACK.TabIndex = 9
            Me.btnBACK.Text = "キャンセル"
            Me.btnBACK.UseVisualStyleBackColor = True
            '
            'Panel6
            '
            Me.Panel6.AutoSize = True
            Me.Panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel6.BackColor = System.Drawing.SystemColors.Control
            Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel6.Controls.Add(Me.LblMessage)
            Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel6.Location = New System.Drawing.Point(0, 641)
            Me.Panel6.Name = "Panel6"
            Me.Panel6.Size = New System.Drawing.Size(969, 25)
            Me.Panel6.TabIndex = 85
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
            Me.LblMessage.Name = "LblMessage"
            Me.LblMessage.Size = New System.Drawing.Size(966, 22)
            Me.LblMessage.TabIndex = 55
            Me.LblMessage.Text = "発行№を選択して「取込」ボタンをクリックして下さい。"
            Me.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Timer1
            '
            Me.Timer1.Enabled = True
            '
            'Frm9DispSeisakuIchiranSelect
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(969, 668)
            Me.Controls.Add(Me.Panel6)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel4)
            Me.Controls.Add(Me.spdParts)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.Panel1)
            Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "Frm9DispSeisakuIchiranSelect"
            Me.Text = "製作一覧システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            Me.Panel5.ResumeLayout(False)
            Me.Panel5.PerformLayout()
            Me.Panel4.ResumeLayout(False)
            Me.Panel6.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents LblCurrPGId As System.Windows.Forms.Label
        Friend WithEvents LblDateNow As System.Windows.Forms.Label
        Friend WithEvents LblTimeNow As System.Windows.Forms.Label
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Panel4 As System.Windows.Forms.Panel
        Friend WithEvents btnEND As System.Windows.Forms.Button
        Friend WithEvents btnBACK As System.Windows.Forms.Button
        Friend WithEvents LOGO As System.Windows.Forms.Label
        Friend WithEvents Panel6 As System.Windows.Forms.Panel
        Friend WithEvents LblMessage As System.Windows.Forms.Label
        Friend WithEvents ToolStripPanel1 As System.Windows.Forms.ToolStripPanel
        Friend WithEvents Label21 As System.Windows.Forms.Label
        Friend WithEvents lblKaihatuFugou As System.Windows.Forms.Label
        Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
        Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
        Friend WithEvents btnSeisakuIchiranTorikomi As System.Windows.Forms.Button
        Friend WithEvents cmbHakouNo As System.Windows.Forms.ComboBox
        Friend WithEvents Panel5 As System.Windows.Forms.Panel
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents rbtStatus02 As System.Windows.Forms.RadioButton
        Friend WithEvents rbtStatus01 As System.Windows.Forms.RadioButton
        Friend WithEvents cmbEventName As System.Windows.Forms.ComboBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents cmbEvent As System.Windows.Forms.ComboBox
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents cmbKaihatsuFugo As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace
