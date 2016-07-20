Namespace ShisakuBuhinEdit.Ikkatsu
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm48DispShisakuBuhinEditIkkatsu
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
            Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim ComboBoxCellType1 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm48DispShisakuBuhinEditIkkatsu))
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.spdParts = New FarPoint.Win.Spread.FpSpread
            Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.Panel4 = New System.Windows.Forms.Panel
            Me.Label14 = New System.Windows.Forms.Label
            Me.btnBACK = New System.Windows.Forms.Button
            Me.btnDISP = New System.Windows.Forms.Button
            Me.Panel6 = New System.Windows.Forms.Panel
            Me.btnHyoujiKouseiTool = New System.Windows.Forms.Button
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel4.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel3
            '
            Me.Panel3.AutoSize = True
            Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel3.BackColor = System.Drawing.SystemColors.Control
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel3.Location = New System.Drawing.Point(0, 281)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(654, 2)
            Me.Panel3.TabIndex = 71
            '
            'spdParts
            '
            Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, "
            Me.spdParts.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.spdParts.BackColor = System.Drawing.SystemColors.Control
            Me.spdParts.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.Location = New System.Drawing.Point(12, 81)
            Me.spdParts.Name = "spdParts"
            Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
            Me.spdParts.Size = New System.Drawing.Size(630, 170)
            Me.spdParts.TabIndex = 72
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
            Me.spdParts_Sheet1.RowCount = 26
            Me.spdParts_Sheet1.AllowNoteEdit = True
            Me.spdParts_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdParts_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdParts_Sheet1.Cells.Get(0, 0).Value = "M1"
            Me.spdParts_Sheet1.Cells.Get(0, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(0, 1).Value = "50825CA000"
            Me.spdParts_Sheet1.Cells.Get(0, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(0, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(0, 3).Value = "50825FJ000"
            Me.spdParts_Sheet1.Cells.Get(1, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(1, 1).Value = "50825CA000"
            Me.spdParts_Sheet1.Cells.Get(1, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(1, 2).Value = "K3"
            Me.spdParts_Sheet1.Cells.Get(1, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(1, 3).Value = "50825FJ000"
            Me.spdParts_Sheet1.Cells.Get(2, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(2, 1).Value = "50825CA010"
            Me.spdParts_Sheet1.Cells.Get(2, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(2, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(2, 3).Value = "50825FG000"
            Me.spdParts_Sheet1.Cells.Get(3, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(3, 1).Value = "50825CA010"
            Me.spdParts_Sheet1.Cells.Get(3, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(3, 2).Value = "KAI2"
            Me.spdParts_Sheet1.Cells.Get(3, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(3, 3).Value = "50825FG010"
            Me.spdParts_Sheet1.Cells.Get(4, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(4, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(4, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(5, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(5, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(5, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(6, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(6, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(6, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(7, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(7, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(7, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(8, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(8, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(8, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(9, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(9, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(9, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(10, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(10, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(10, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(11, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(11, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(11, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(12, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(12, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(12, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(13, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(13, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(13, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(14, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(14, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(14, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(15, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(15, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(15, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(16, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(16, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(16, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(17, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(17, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(17, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(18, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(18, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(18, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(19, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(19, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(19, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(20, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(20, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(20, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(21, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(21, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(21, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(22, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(22, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(22, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(23, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(23, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(23, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(24, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(24, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(24, 3).Locked = False
            Me.spdParts_Sheet1.Cells.Get(25, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(25, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(25, 3).Locked = False
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "INST品番"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "INST品番"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "区分"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).ColumnSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "ベースにする品番"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "抽出元"
            Me.spdParts_Sheet1.Columns.Get(0).Label = "INST品番"
            Me.spdParts_Sheet1.Columns.Get(0).Width = 31.0!
            Me.spdParts_Sheet1.Columns.Get(1).CellType = TextCellType1
            Me.spdParts_Sheet1.Columns.Get(1).Label = "INST品番"
            Me.spdParts_Sheet1.Columns.Get(1).Tag = "HOJO_NAME"
            Me.spdParts_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(1).Width = 100.0!
            Me.spdParts_Sheet1.Columns.Get(2).CellType = TextCellType2
            Me.spdParts_Sheet1.Columns.Get(2).Label = "区分"
            Me.spdParts_Sheet1.Columns.Get(2).Tag = "HOJO_NAME"
            Me.spdParts_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(2).Width = 51.0!
            Me.spdParts_Sheet1.Columns.Get(3).CellType = TextCellType3
            Me.spdParts_Sheet1.Columns.Get(3).Label = "ベースにする品番"
            Me.spdParts_Sheet1.Columns.Get(3).Tag = "HOJO_NAME"
            Me.spdParts_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(3).VisualStyles = FarPoint.Win.VisualStyles.[Auto]
            Me.spdParts_Sheet1.Columns.Get(3).Width = 100.0!
            ComboBoxCellType1.ButtonAlign = FarPoint.Win.ButtonAlign.Right
            Me.spdParts_Sheet1.Columns.Get(5).CellType = ComboBoxCellType1
            Me.spdParts_Sheet1.Columns.Get(5).Label = "抽出元"
            Me.spdParts_Sheet1.Columns.Get(5).Tag = "HOJO_NAME"
            Me.spdParts_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(5).Width = 257.0!
            Me.spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdParts_Sheet1.RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Letters
            Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdParts_Sheet1.RowHeader.DefaultStyle.Locked = False
            Me.spdParts_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
            Me.spdParts_Sheet1.Rows.Default.Height = 16.0!
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
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
            Me.Panel4.Size = New System.Drawing.Size(654, 30)
            Me.Panel4.TabIndex = 84
            '
            'Label14
            '
            Me.Label14.AutoSize = True
            Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label14.Location = New System.Drawing.Point(12, 8)
            Me.Label14.Name = "Label14"
            Me.Label14.Size = New System.Drawing.Size(101, 17)
            Me.Label14.TabIndex = 89
            Me.Label14.Text = "<<一括設定>>"
            '
            'btnBACK
            '
            Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBACK.Location = New System.Drawing.Point(587, 3)
            Me.btnBACK.Name = "btnBACK"
            Me.btnBACK.Size = New System.Drawing.Size(64, 24)
            Me.btnBACK.TabIndex = 55
            Me.btnBACK.Text = "戻る"
            Me.btnBACK.UseVisualStyleBackColor = True
            '
            'btnDISP
            '
            Me.btnDISP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnDISP.BackColor = System.Drawing.SystemColors.Control
            Me.btnDISP.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnDISP.Location = New System.Drawing.Point(424, 33)
            Me.btnDISP.Name = "btnDISP"
            Me.btnDISP.Size = New System.Drawing.Size(79, 24)
            Me.btnDISP.TabIndex = 88
            Me.btnDISP.Text = "表示"
            Me.btnDISP.UseVisualStyleBackColor = False
            '
            'Panel6
            '
            Me.Panel6.AutoSize = True
            Me.Panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel6.BackColor = System.Drawing.SystemColors.Control
            Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel6.Location = New System.Drawing.Point(0, 279)
            Me.Panel6.Name = "Panel6"
            Me.Panel6.Size = New System.Drawing.Size(654, 2)
            Me.Panel6.TabIndex = 85
            '
            'btnHyoujiKouseiTool
            '
            Me.btnHyoujiKouseiTool.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnHyoujiKouseiTool.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
            Me.btnHyoujiKouseiTool.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnHyoujiKouseiTool.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnHyoujiKouseiTool.Location = New System.Drawing.Point(509, 33)
            Me.btnHyoujiKouseiTool.Name = "btnHyoujiKouseiTool"
            Me.btnHyoujiKouseiTool.Size = New System.Drawing.Size(133, 24)
            Me.btnHyoujiKouseiTool.TabIndex = 130
            Me.btnHyoujiKouseiTool.Text = "部品構成表示ツール"
            Me.btnHyoujiKouseiTool.UseVisualStyleBackColor = False
            '
            'Frm48DispShisakuBuhinEditIkkatsu
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(654, 283)
            Me.Controls.Add(Me.btnHyoujiKouseiTool)
            Me.Controls.Add(Me.Panel6)
            Me.Controls.Add(Me.Panel4)
            Me.Controls.Add(Me.btnDISP)
            Me.Controls.Add(Me.spdParts)
            Me.Controls.Add(Me.Panel3)
            Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "Frm48DispShisakuBuhinEditIkkatsu"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.TopMost = True
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel4.ResumeLayout(False)
            Me.Panel4.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents Panel4 As System.Windows.Forms.Panel
        Friend WithEvents btnBACK As System.Windows.Forms.Button
        Friend WithEvents Panel6 As System.Windows.Forms.Panel
        Friend WithEvents btnDISP As System.Windows.Forms.Button
        Friend WithEvents Label14 As System.Windows.Forms.Label
        Friend WithEvents btnHyoujiKouseiTool As System.Windows.Forms.Button
    End Class
End NameSpace