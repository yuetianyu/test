Namespace EventEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm9DispSeisakuIchiranSelectKaiteiNo
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
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.btnBACK = New System.Windows.Forms.Button
            Me.btnSeisakuIchiranTorikomi = New System.Windows.Forms.Button
            Me.ToolStripPanel1 = New System.Windows.Forms.ToolStripPanel
            Me.lblKaihatuFugou = New System.Windows.Forms.Label
            Me.Panel6 = New System.Windows.Forms.Panel
            Me.LblMessage = New System.Windows.Forms.Label
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.spdParts = New FarPoint.Win.Spread.FpSpread
            Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.Panel2.SuspendLayout()
            Me.Panel6.SuspendLayout()
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Panel3
            '
            Me.Panel3.AutoSize = True
            Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel3.BackColor = System.Drawing.SystemColors.Control
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel3.Location = New System.Drawing.Point(0, 222)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(969, 2)
            Me.Panel3.TabIndex = 71
            '
            'Panel2
            '
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.btnBACK)
            Me.Panel2.Controls.Add(Me.btnSeisakuIchiranTorikomi)
            Me.Panel2.Controls.Add(Me.ToolStripPanel1)
            Me.Panel2.Controls.Add(Me.lblKaihatuFugou)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 0)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(969, 40)
            Me.Panel2.TabIndex = 83
            '
            'btnBACK
            '
            Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBACK.Location = New System.Drawing.Point(873, 5)
            Me.btnBACK.Name = "btnBACK"
            Me.btnBACK.Size = New System.Drawing.Size(91, 24)
            Me.btnBACK.TabIndex = 145
            Me.btnBACK.Text = "キャンセル"
            Me.btnBACK.UseVisualStyleBackColor = True
            '
            'btnSeisakuIchiranTorikomi
            '
            Me.btnSeisakuIchiranTorikomi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnSeisakuIchiranTorikomi.BackColor = System.Drawing.SystemColors.Control
            Me.btnSeisakuIchiranTorikomi.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnSeisakuIchiranTorikomi.ImageAlign = System.Drawing.ContentAlignment.TopLeft
            Me.btnSeisakuIchiranTorikomi.Location = New System.Drawing.Point(5, 5)
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
            'lblKaihatuFugou
            '
            Me.lblKaihatuFugou.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblKaihatuFugou.Location = New System.Drawing.Point(70, 6)
            Me.lblKaihatuFugou.Name = "lblKaihatuFugou"
            Me.lblKaihatuFugou.Size = New System.Drawing.Size(54, 20)
            Me.lblKaihatuFugou.TabIndex = 144
            Me.lblKaihatuFugou.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Panel6
            '
            Me.Panel6.AutoSize = True
            Me.Panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel6.BackColor = System.Drawing.SystemColors.Control
            Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel6.Controls.Add(Me.LblMessage)
            Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel6.Location = New System.Drawing.Point(0, 197)
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
            Me.LblMessage.Text = "製作一覧を選択して「取込」ボタンをクリックして下さい。"
            Me.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Timer1
            '
            Me.Timer1.Enabled = True
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
            Me.spdParts.Location = New System.Drawing.Point(3, 46)
            Me.spdParts.Name = "spdParts"
            Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
            Me.spdParts.Size = New System.Drawing.Size(962, 145)
            Me.spdParts.TabIndex = 86
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
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "発行№"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Locked = False
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "開発符号"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "イベント"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "イベント名"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "改訂№"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "登録日"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "台数"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = " "
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "W/B"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "ステータス"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "状態"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 6).Value = "完成車"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 7).Value = "＋"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 8).Value = "ＷＢ車"
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
            Me.spdParts_Sheet1.Columns.Get(8).Label = "ＷＢ車"
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
            'Frm9DispSeisakuIchiranSelectKaiteiNo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(969, 224)
            Me.Controls.Add(Me.spdParts)
            Me.Controls.Add(Me.Panel6)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel3)
            Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.Name = "Frm9DispSeisakuIchiranSelectKaiteiNo"
            Me.Text = "製作一覧システム Ver 1.00"
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            Me.Panel6.ResumeLayout(False)
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Panel6 As System.Windows.Forms.Panel
        Friend WithEvents LblMessage As System.Windows.Forms.Label
        Friend WithEvents ToolStripPanel1 As System.Windows.Forms.ToolStripPanel
        Friend WithEvents lblKaihatuFugou As System.Windows.Forms.Label
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
        Friend WithEvents btnSeisakuIchiranTorikomi As System.Windows.Forms.Button
        Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents btnBACK As System.Windows.Forms.Button
    End Class
End Namespace
