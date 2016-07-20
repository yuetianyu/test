Namespace ShisakuBuhinEdit.Selector
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class HoyouBuhinFrm41HikakuResultSelector
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
            Dim CheckBoxCellType1 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HoyouBuhinFrm41HikakuResultSelector))
            Me.btnHanei = New System.Windows.Forms.Button
            Me.btnCancel = New System.Windows.Forms.Button
            Me.spdResult = New FarPoint.Win.Spread.FpSpread
            Me.spdResult_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.btnCheckSaki = New System.Windows.Forms.Button
            Me.btnCheckMoto = New System.Windows.Forms.Button
            Me.btnCheckClear = New System.Windows.Forms.Button
            CType(Me.spdResult, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdResult_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'btnHanei
            '
            Me.btnHanei.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnHanei.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
            Me.btnHanei.Location = New System.Drawing.Point(29, 243)
            Me.btnHanei.Name = "btnHanei"
            Me.btnHanei.Size = New System.Drawing.Size(85, 24)
            Me.btnHanei.TabIndex = 1
            Me.btnHanei.Text = "反映"
            Me.btnHanei.UseVisualStyleBackColor = True
            '
            'btnCancel
            '
            Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
            Me.btnCancel.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
            Me.btnCancel.Location = New System.Drawing.Point(501, 243)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(85, 24)
            Me.btnCancel.TabIndex = 3
            Me.btnCancel.Text = "キャンセル"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            'spdResult
            '
            Me.spdResult.AccessibleDescription = "spdResult, Sheet1, Row 0, Column 0, "
            Me.spdResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.spdResult.BackColor = System.Drawing.SystemColors.Control
            Me.spdResult.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdResult.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdResult.Location = New System.Drawing.Point(29, 49)
            Me.spdResult.Name = "spdResult"
            Me.spdResult.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdResult.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdResult_Sheet1})
            Me.spdResult.Size = New System.Drawing.Size(557, 185)
            Me.spdResult.TabIndex = 74
            Me.spdResult.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdResult.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdResult_Sheet1
            '
            Me.spdResult_Sheet1.Reset()
            Me.spdResult_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdResult_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdResult_Sheet1.ColumnCount = 14
            Me.spdResult_Sheet1.AllowNoteEdit = True
            Me.spdResult_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdResult_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "選択"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "フラグ"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "区分"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "部品番号"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "部品名称"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "員数"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "レベル"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "国内集計"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "海外集計"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "原調区分"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "取引先コード"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "試作区分"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "改訂"
            Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "枝番"
            Me.spdResult_Sheet1.Columns.Get(0).CellType = CheckBoxCellType1
            Me.spdResult_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdResult_Sheet1.Columns.Get(0).Label = "選択"
            Me.spdResult_Sheet1.Columns.Get(1).Label = "フラグ"
            Me.spdResult_Sheet1.Columns.Get(1).Width = 61.0!
            Me.spdResult_Sheet1.Columns.Get(2).Label = "区分"
            Me.spdResult_Sheet1.Columns.Get(2).Width = 61.0!
            Me.spdResult_Sheet1.Columns.Get(3).Label = "部品番号"
            Me.spdResult_Sheet1.Columns.Get(3).Width = 106.0!
            Me.spdResult_Sheet1.Columns.Get(4).Label = "部品名称"
            Me.spdResult_Sheet1.Columns.Get(4).Width = 154.0!
            Me.spdResult_Sheet1.Columns.Get(5).Label = "員数"
            Me.spdResult_Sheet1.Columns.Get(5).Width = 59.0!
            Me.spdResult_Sheet1.Columns.Get(10).Label = "取引先コード"
            Me.spdResult_Sheet1.Columns.Get(10).Width = 83.0!
            Me.spdResult_Sheet1.FrozenColumnCount = 4
            Me.spdResult_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdResult_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdResult_Sheet1.RowHeader.DefaultStyle.Locked = False
            Me.spdResult_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
            Me.spdResult_Sheet1.Rows.Default.Height = 16.0!
            Me.spdResult_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            Me.spdResult.SetViewportLeftColumn(0, 0, 4)
            Me.spdResult.SetActiveViewport(0, 0, -1)
            '
            'btnCheckSaki
            '
            Me.btnCheckSaki.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
            Me.btnCheckSaki.Location = New System.Drawing.Point(29, 13)
            Me.btnCheckSaki.Name = "btnCheckSaki"
            Me.btnCheckSaki.Size = New System.Drawing.Size(119, 24)
            Me.btnCheckSaki.TabIndex = 75
            Me.btnCheckSaki.Text = "比較先を全選択"
            Me.btnCheckSaki.UseVisualStyleBackColor = True
            '
            'btnCheckMoto
            '
            Me.btnCheckMoto.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.btnCheckMoto.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
            Me.btnCheckMoto.Location = New System.Drawing.Point(271, 13)
            Me.btnCheckMoto.Name = "btnCheckMoto"
            Me.btnCheckMoto.Size = New System.Drawing.Size(119, 24)
            Me.btnCheckMoto.TabIndex = 75
            Me.btnCheckMoto.Text = "比較元を全選択"
            Me.btnCheckMoto.UseVisualStyleBackColor = True
            '
            'btnCheckClear
            '
            Me.btnCheckClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnCheckClear.BackColor = System.Drawing.SystemColors.Control
            Me.btnCheckClear.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
            Me.btnCheckClear.Location = New System.Drawing.Point(501, 13)
            Me.btnCheckClear.Name = "btnCheckClear"
            Me.btnCheckClear.Size = New System.Drawing.Size(85, 24)
            Me.btnCheckClear.TabIndex = 75
            Me.btnCheckClear.Text = "全て解除"
            Me.btnCheckClear.UseVisualStyleBackColor = False
            '
            'HoyouBuhinFrm41HikakuResultSelector
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(614, 283)
            Me.Controls.Add(Me.btnCheckClear)
            Me.Controls.Add(Me.btnCheckMoto)
            Me.Controls.Add(Me.btnCheckSaki)
            Me.Controls.Add(Me.spdResult)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnHanei)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "HoyouBuhinFrm41HikakuResultSelector"
            Me.Text = "新試作手配システム Ver 1.00"
            CType(Me.spdResult, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdResult_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents btnHanei As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents spdResult As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdResult_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents btnCheckSaki As System.Windows.Forms.Button
        Friend WithEvents btnCheckMoto As System.Windows.Forms.Button
        Friend WithEvents btnCheckClear As System.Windows.Forms.Button
    End Class
End Namespace