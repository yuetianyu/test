Namespace TehaichoEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frm20BuhinhyoExcelOutput
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
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

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm20BuhinhyoExcelOutput))
            Me.cmbGroup = New System.Windows.Forms.ComboBox
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.Label4 = New System.Windows.Forms.Label
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.btnExport = New System.Windows.Forms.Button
            Me.btnBACK = New System.Windows.Forms.Button
            Me.出力グループ = New System.Windows.Forms.Label
            Me.spdGouSya = New FarPoint.Win.Spread.FpSpread
            Me.spdGouSya_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.ToolMenuCopy = New System.Windows.Forms.ToolStripMenuItem
            Me.ToolMenuPaste = New System.Windows.Forms.ToolStripMenuItem
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.spdGouSya, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdGouSya_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ContextMenuStrip1.SuspendLayout()
            Me.SuspendLayout()
            '
            'cmbGroup
            '
            Me.cmbGroup.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbGroup.FormattingEnabled = True
            Me.cmbGroup.Location = New System.Drawing.Point(104, 71)
            Me.cmbGroup.Name = "cmbGroup"
            Me.cmbGroup.Size = New System.Drawing.Size(122, 23)
            Me.cmbGroup.TabIndex = 1
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(263, 32)
            Me.Panel1.TabIndex = 54
            '
            'Label4
            '
            Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold)
            Me.Label4.ForeColor = System.Drawing.Color.Yellow
            Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label4.Location = New System.Drawing.Point(12, 1)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(238, 31)
            Me.Label4.TabIndex = 54
            Me.Label4.Text = "部品表EXCEL出力(最新)"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Panel2
            '
            Me.Panel2.AutoSize = True
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.btnExport)
            Me.Panel2.Controls.Add(Me.btnBACK)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 32)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(263, 33)
            Me.Panel2.TabIndex = 56
            '
            'btnExport
            '
            Me.btnExport.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
            Me.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnExport.Location = New System.Drawing.Point(103, 3)
            Me.btnExport.Margin = New System.Windows.Forms.Padding(4)
            Me.btnExport.Name = "btnExport"
            Me.btnExport.Size = New System.Drawing.Size(84, 24)
            Me.btnExport.TabIndex = 56
            Me.btnExport.Text = "出力"
            Me.btnExport.UseVisualStyleBackColor = True
            '
            'btnBACK
            '
            Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
            Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBACK.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.btnBACK.Location = New System.Drawing.Point(194, 3)
            Me.btnBACK.Name = "btnBACK"
            Me.btnBACK.Size = New System.Drawing.Size(64, 24)
            Me.btnBACK.TabIndex = 55
            Me.btnBACK.Text = "戻る"
            Me.btnBACK.UseVisualStyleBackColor = True
            '
            '出力グループ
            '
            Me.出力グループ.AutoSize = True
            Me.出力グループ.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.出力グループ.Location = New System.Drawing.Point(10, 74)
            Me.出力グループ.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.出力グループ.Name = "出力グループ"
            Me.出力グループ.Size = New System.Drawing.Size(92, 15)
            Me.出力グループ.TabIndex = 176
            Me.出力グループ.Text = "出力グループ："
            '
            'spdGouSya
            '
            Me.spdGouSya.AccessibleDescription = "spdGouSya, Sheet1, Row 0, Column 0, "
            Me.spdGouSya.AllowDragDrop = True
            Me.spdGouSya.AllowDragFill = True
            Me.spdGouSya.AllowUserFormulas = True
            Me.spdGouSya.BackColor = System.Drawing.SystemColors.Control
            Me.spdGouSya.EditModeReplace = True
            Me.spdGouSya.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
            Me.spdGouSya.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdGouSya.Location = New System.Drawing.Point(0, 100)
            Me.spdGouSya.Name = "spdGouSya"
            Me.spdGouSya.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdGouSya.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both
            Me.spdGouSya.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdGouSya_Sheet1})
            Me.spdGouSya.Size = New System.Drawing.Size(259, 340)
            Me.spdGouSya.TabIndex = 177
            Me.spdGouSya.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdGouSya.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdGouSya_Sheet1
            '
            Me.spdGouSya_Sheet1.Reset()
            Me.spdGouSya_Sheet1.SheetName = "Sheet1"
            Me.spdGouSya_Sheet1.ColumnCount = 2
            Me.spdGouSya_Sheet1.RowCount = 2
            Me.spdGouSya_Sheet1.ColumnHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Numbers
            Me.spdGouSya_Sheet1.FrozenColumnCount = 1
            Me.spdGouSya_Sheet1.FrozenRowCount = 1
            Me.spdGouSya_Sheet1.Models = CType(resources.GetObject("spdGouSya_Sheet1.Models"), FarPoint.Win.Spread.SheetView.DocumentModels)
            Me.spdGouSya_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(160, Byte), Integer))
            Me.spdGouSya_Sheet1.StartingRowNumber = 0
            Me.spdGouSya.SetViewportLeftColumn(0, 0, 1)
            Me.spdGouSya.SetViewportTopRow(0, 0, 1)
            Me.spdGouSya.SetActiveViewport(0, -1, -1)
            '
            'ContextMenuStrip1
            '
            Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolMenuCopy, Me.ToolMenuPaste})
            Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
            Me.ContextMenuStrip1.Size = New System.Drawing.Size(128, 48)
            '
            'ToolMenuCopy
            '
            Me.ToolMenuCopy.Name = "ToolMenuCopy"
            Me.ToolMenuCopy.Size = New System.Drawing.Size(127, 22)
            Me.ToolMenuCopy.Text = "コピー(&C)"
            '
            'ToolMenuPaste
            '
            Me.ToolMenuPaste.Name = "ToolMenuPaste"
            Me.ToolMenuPaste.Size = New System.Drawing.Size(127, 22)
            Me.ToolMenuPaste.Text = "貼りつけ(&P)"
            '
            'frm20BuhinhyoExcelOutput
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(263, 441)
            Me.Controls.Add(Me.spdGouSya)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.cmbGroup)
            Me.Controls.Add(Me.出力グループ)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.KeyPreview = True
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frm20BuhinhyoExcelOutput"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            CType(Me.spdGouSya, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdGouSya_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ContextMenuStrip1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents cmbGroup As System.Windows.Forms.ComboBox
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents btnBACK As System.Windows.Forms.Button
        Friend WithEvents btnExport As System.Windows.Forms.Button
        Friend WithEvents 出力グループ As System.Windows.Forms.Label
        Friend WithEvents spdGouSya As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdGouSya_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents ToolMenuCopy As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolMenuPaste As System.Windows.Forms.ToolStripMenuItem
    End Class

End Namespace