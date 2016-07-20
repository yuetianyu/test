<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm20ExcelKeyDataSelect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm20ExcelKeyDataSelect))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.pnlUpdate = New System.Windows.Forms.Panel
        Me.spdUpdate = New EBom.FpSpread.EBomSpread(Me.components)
        Me.spdUpdate_Sheet1 = New FarPoint.Win.Spread.SheetView
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.spdSearch = New EBom.FpSpread.EBomSpread(Me.components)
        Me.spdSearch_Sheet1 = New FarPoint.Win.Spread.SheetView
        Me.btnBACK = New System.Windows.Forms.Button
        Me.btnJikkou = New System.Windows.Forms.Button
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cbxTORIKOMISAKI_KUUHAKU = New System.Windows.Forms.RadioButton
        Me.cbxTORIKOMIMOTO_KUUHAKU = New System.Windows.Forms.RadioButton
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pnlUpdate.SuspendLayout()
        CType(Me.spdUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdUpdate_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.spdSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdSearch_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.btnBACK)
        Me.Panel1.Controls.Add(Me.btnJikkou)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(907, 356)
        Me.Panel1.TabIndex = 145
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.pnlUpdate)
        Me.GroupBox1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(17, 121)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(871, 97)
        Me.GroupBox1.TabIndex = 139
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "更新対象項目の設定"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
        Me.Label1.Location = New System.Drawing.Point(15, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 15)
        Me.Label1.TabIndex = 104
        Me.Label1.Text = "取り込み対象列"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
        Me.Label21.Location = New System.Drawing.Point(15, 22)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(124, 15)
        Me.Label21.TabIndex = 103
        Me.Label21.Text = "EXCELヘッダー選択"
        '
        'pnlUpdate
        '
        Me.pnlUpdate.AutoScroll = True
        Me.pnlUpdate.AutoScrollMargin = New System.Drawing.Size(1, 1)
        Me.pnlUpdate.AutoScrollMinSize = New System.Drawing.Size(1, 1)
        Me.pnlUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlUpdate.Controls.Add(Me.spdUpdate)
        Me.pnlUpdate.Location = New System.Drawing.Point(141, 20)
        Me.pnlUpdate.Name = "pnlUpdate"
        Me.pnlUpdate.Size = New System.Drawing.Size(722, 69)
        Me.pnlUpdate.TabIndex = 135
        '
        'spdUpdate
        '
        Me.spdUpdate.AccessibleDescription = "spdSearch, Sheet1, Row 0, Column 0, "
        Me.spdUpdate.AllowDeleteCell = True
        Me.spdUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.spdUpdate.FocusRendererColor = System.Drawing.Color.Empty
        Me.spdUpdate.FocusThicness = 1
        Me.spdUpdate.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdUpdate.Location = New System.Drawing.Point(3, 2)
        Me.spdUpdate.MoveEnterDirection = EBom.FpSpread.EBomSpread.MOVE_ENTER_DIRECTION.NEXT_COLUMN_WARP
        Me.spdUpdate.Name = "spdUpdate"
        Me.spdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.spdUpdate.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdUpdate_Sheet1})
        Me.spdUpdate.Size = New System.Drawing.Size(715, 63)
        Me.spdUpdate.TabIndex = 1
        Me.spdUpdate.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        '
        'spdUpdate_Sheet1
        '
        Me.spdUpdate_Sheet1.Reset()
        Me.spdUpdate_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spdUpdate_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spdUpdate_Sheet1.ColumnCount = 0
        Me.spdUpdate_Sheet1.ColumnHeader.RowCount = 0
        Me.spdUpdate_Sheet1.RowCount = 2
        Me.spdUpdate_Sheet1.RowHeader.ColumnCount = 0
        Me.spdUpdate_Sheet1.ActiveColumnIndex = -1
        Me.spdUpdate_Sheet1.AllowNoteEdit = True
        Me.spdUpdate_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.spdUpdate_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        Me.spdUpdate.SetActiveViewport(0, 0, -1)
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.pnlSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(17, 18)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(871, 97)
        Me.GroupBox2.TabIndex = 138
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "検索キー項目の設定"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
        Me.Label3.Location = New System.Drawing.Point(15, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 15)
        Me.Label3.TabIndex = 104
        Me.Label3.Text = "取り込み対象列"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
        Me.Label2.Location = New System.Drawing.Point(15, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(124, 15)
        Me.Label2.TabIndex = 103
        Me.Label2.Text = "EXCELヘッダー選択"
        '
        'pnlSearch
        '
        Me.pnlSearch.AutoScroll = True
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.spdSearch)
        Me.pnlSearch.Location = New System.Drawing.Point(142, 12)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(717, 67)
        Me.pnlSearch.TabIndex = 134
        '
        'spdSearch
        '
        Me.spdSearch.AccessibleDescription = "spdSearch, Sheet1, Row 0, Column 0, "
        Me.spdSearch.AllowDeleteCell = True
        Me.spdSearch.BackColor = System.Drawing.SystemColors.Control
        Me.spdSearch.FocusRendererColor = System.Drawing.Color.Empty
        Me.spdSearch.FocusThicness = 1
        Me.spdSearch.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdSearch.Location = New System.Drawing.Point(0, 0)
        Me.spdSearch.MoveEnterDirection = EBom.FpSpread.EBomSpread.MOVE_ENTER_DIRECTION.NEXT_COLUMN_WARP
        Me.spdSearch.Name = "spdSearch"
        Me.spdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.spdSearch.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdSearch_Sheet1})
        Me.spdSearch.Size = New System.Drawing.Size(715, 63)
        Me.spdSearch.TabIndex = 0
        Me.spdSearch.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        '
        'spdSearch_Sheet1
        '
        Me.spdSearch_Sheet1.Reset()
        Me.spdSearch_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spdSearch_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spdSearch_Sheet1.ColumnCount = 0
        Me.spdSearch_Sheet1.ColumnHeader.RowCount = 0
        Me.spdSearch_Sheet1.RowCount = 2
        Me.spdSearch_Sheet1.RowHeader.ColumnCount = 0
        Me.spdSearch_Sheet1.ActiveColumnIndex = -1
        Me.spdSearch_Sheet1.AllowNoteEdit = True
        Me.spdSearch_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.spdSearch_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        Me.spdSearch.SetActiveViewport(0, 0, -1)
        '
        'btnBACK
        '
        Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBACK.Location = New System.Drawing.Point(813, 307)
        Me.btnBACK.Name = "btnBACK"
        Me.btnBACK.Size = New System.Drawing.Size(63, 24)
        Me.btnBACK.TabIndex = 55
        Me.btnBACK.Text = "戻る"
        Me.btnBACK.UseVisualStyleBackColor = True
        '
        'btnJikkou
        '
        Me.btnJikkou.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnJikkou.BackColor = System.Drawing.SystemColors.Control
        Me.btnJikkou.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnJikkou.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnJikkou.Location = New System.Drawing.Point(681, 307)
        Me.btnJikkou.Name = "btnJikkou"
        Me.btnJikkou.Size = New System.Drawing.Size(109, 24)
        Me.btnJikkou.TabIndex = 98
        Me.btnJikkou.Text = "取り込み実行"
        Me.btnJikkou.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.BackColor = System.Drawing.SystemColors.Control
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(907, 0)
        Me.Panel4.TabIndex = 146
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cbxTORIKOMIMOTO_KUUHAKU)
        Me.Panel2.Controls.Add(Me.cbxTORIKOMISAKI_KUUHAKU)
        Me.Panel2.Location = New System.Drawing.Point(17, 231)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(232, 100)
        Me.Panel2.TabIndex = 140
        '
        'cbxTORIKOMISAKI_KUUHAKU
        '
        Me.cbxTORIKOMISAKI_KUUHAKU.AutoSize = True
        Me.cbxTORIKOMISAKI_KUUHAKU.Checked = True
        Me.cbxTORIKOMISAKI_KUUHAKU.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cbxTORIKOMISAKI_KUUHAKU.Location = New System.Drawing.Point(18, 21)
        Me.cbxTORIKOMISAKI_KUUHAKU.Name = "cbxTORIKOMISAKI_KUUHAKU"
        Me.cbxTORIKOMISAKI_KUUHAKU.Size = New System.Drawing.Size(130, 19)
        Me.cbxTORIKOMISAKI_KUUHAKU.TabIndex = 0
        Me.cbxTORIKOMISAKI_KUUHAKU.TabStop = True
        Me.cbxTORIKOMISAKI_KUUHAKU.Text = "空白ｾﾙのみ更新"
        Me.cbxTORIKOMISAKI_KUUHAKU.UseVisualStyleBackColor = True
        '
        'cbxTORIKOMIMOTO_KUUHAKU
        '
        Me.cbxTORIKOMIMOTO_KUUHAKU.AutoSize = True
        Me.cbxTORIKOMIMOTO_KUUHAKU.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cbxTORIKOMIMOTO_KUUHAKU.Location = New System.Drawing.Point(18, 59)
        Me.cbxTORIKOMIMOTO_KUUHAKU.Name = "cbxTORIKOMIMOTO_KUUHAKU"
        Me.cbxTORIKOMIMOTO_KUUHAKU.Size = New System.Drawing.Size(82, 19)
        Me.cbxTORIKOMIMOTO_KUUHAKU.TabIndex = 1
        Me.cbxTORIKOMIMOTO_KUUHAKU.TabStop = True
        Me.cbxTORIKOMIMOTO_KUUHAKU.Text = "全て更新"
        Me.cbxTORIKOMIMOTO_KUUHAKU.UseVisualStyleBackColor = True
        '
        'Frm20ExcelKeyDataSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(913, 362)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm20ExcelKeyDataSelect"
        Me.Padding = New System.Windows.Forms.Padding(3)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "過去発注情報引当"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnlUpdate.ResumeLayout(False)
        CType(Me.spdUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdUpdate_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        CType(Me.spdSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdSearch_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnJikkou As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnBACK As System.Windows.Forms.Button
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents pnlUpdate As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents spdSearch As EBom.FpSpread.EBomSpread
    Friend WithEvents spdSearch_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents spdUpdate As EBom.FpSpread.EBomSpread
    Friend WithEvents spdUpdate_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cbxTORIKOMIMOTO_KUUHAKU As System.Windows.Forms.RadioButton
    Friend WithEvents cbxTORIKOMISAKI_KUUHAKU As System.Windows.Forms.RadioButton

End Class
