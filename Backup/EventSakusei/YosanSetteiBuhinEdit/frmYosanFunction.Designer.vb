Namespace YosanSetteiBuhinEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmYosanFunction
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
            Dim CurrencyCellType1 As FarPoint.Win.Spread.CellType.CurrencyCellType = New FarPoint.Win.Spread.CellType.CurrencyCellType
            Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
            Me.EBomSpread1 = New FarPoint.Win.Spread.FpSpread
            Me.EBomSpread1_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.btnCancel = New System.Windows.Forms.Button
            Me.btnRegist = New System.Windows.Forms.Button
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            CType(Me.EBomSpread1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.EBomSpread1_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer1.Name = "SplitContainer1"
            Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.EBomSpread1)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
            Me.SplitContainer1.Panel2.Controls.Add(Me.btnRegist)
            Me.SplitContainer1.Size = New System.Drawing.Size(820, 543)
            Me.SplitContainer1.SplitterDistance = 506
            Me.SplitContainer1.TabIndex = 0
            '
            'EBomSpread1
            '
            Me.EBomSpread1.AccessibleDescription = "EBomSpread1, Sheet1, Row 0, Column 0, "
            'Me.EBomSpread1.AllowDeleteCell = True
            Me.EBomSpread1.BackColor = System.Drawing.SystemColors.Control
            Me.EBomSpread1.Dock = System.Windows.Forms.DockStyle.Fill
            'Me.EBomSpread1.FocusRendererColor = System.Drawing.Color.Empty
            'Me.EBomSpread1.FocusThicness = 1
            Me.EBomSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.EBomSpread1.Location = New System.Drawing.Point(0, 0)
            'Me.EBomSpread1.MoveEnterDirection = EBom.FpSpread.EBomSpread.MOVE_ENTER_DIRECTION.NEXT_COLUMN_WARP
            Me.EBomSpread1.Name = "EBomSpread1"
            Me.EBomSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.EBomSpread1.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.EBomSpread1_Sheet1})
            Me.EBomSpread1.Size = New System.Drawing.Size(820, 506)
            Me.EBomSpread1.TabIndex = 0
            Me.EBomSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            '
            'EBomSpread1_Sheet1
            '
            Me.EBomSpread1_Sheet1.Reset()
            Me.EBomSpread1_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.EBomSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.EBomSpread1_Sheet1.ColumnCount = 4
            Me.EBomSpread1_Sheet1.AllowNoteEdit = True
            Me.EBomSpread1_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.EBomSpread1_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.EBomSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "ファンクション品番"
            Me.EBomSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Tag = "YOSAN_MAKER_CODE"
            Me.EBomSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "取引先コード"
            Me.EBomSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Tag = "YOSAN_TANKA"
            Me.EBomSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "単価(円)"
            Me.EBomSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Tag = "YOSAN_BIKO"
            Me.EBomSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "備考"
            Me.EBomSpread1_Sheet1.Columns.Get(0).AllowAutoSort = True
            TextCellType1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
            TextCellType1.MaxLength = 5
            Me.EBomSpread1_Sheet1.Columns.Get(0).CellType = TextCellType1
            Me.EBomSpread1_Sheet1.Columns.Get(0).Label = "ファンクション品番"
            Me.EBomSpread1_Sheet1.Columns.Get(0).Tag = "YOSAN_FUNCTION_HINBAN"
            Me.EBomSpread1_Sheet1.Columns.Get(0).Width = 115.0!
            Me.EBomSpread1_Sheet1.Columns.Get(1).AllowAutoSort = True
            TextCellType2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
            TextCellType2.MaxLength = 4
            Me.EBomSpread1_Sheet1.Columns.Get(1).CellType = TextCellType2
            Me.EBomSpread1_Sheet1.Columns.Get(1).Label = "取引先コード"
            Me.EBomSpread1_Sheet1.Columns.Get(1).Tag = "YOSAN_MAKER_CODE"
            Me.EBomSpread1_Sheet1.Columns.Get(1).Width = 131.0!
            CurrencyCellType1.MaximumValue = New Decimal(New Integer() {276447231, 23283, 0, 131072})
            CurrencyCellType1.MinimumValue = New Decimal(New Integer() {0, 0, 0, 131072})
            Me.EBomSpread1_Sheet1.Columns.Get(2).CellType = CurrencyCellType1
            Me.EBomSpread1_Sheet1.Columns.Get(2).Label = "単価(円)"
            Me.EBomSpread1_Sheet1.Columns.Get(2).Tag = "YOSAN_TANKA"
            Me.EBomSpread1_Sheet1.Columns.Get(2).Width = 98.0!
            TextCellType3.MaxLength = 256
            Me.EBomSpread1_Sheet1.Columns.Get(3).CellType = TextCellType3
            Me.EBomSpread1_Sheet1.Columns.Get(3).Label = "備考"
            Me.EBomSpread1_Sheet1.Columns.Get(3).Tag = "YOSAN_BIKO"
            Me.EBomSpread1_Sheet1.Columns.Get(3).Width = 415.0!
            Me.EBomSpread1_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.EBomSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'btnCancel
            '
            Me.btnCancel.Location = New System.Drawing.Point(742, 3)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(75, 23)
            Me.btnCancel.TabIndex = 1
            Me.btnCancel.Text = "キャンセル"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'btnRegist
            '
            Me.btnRegist.Location = New System.Drawing.Point(661, 3)
            Me.btnRegist.Name = "btnRegist"
            Me.btnRegist.Size = New System.Drawing.Size(75, 23)
            Me.btnRegist.TabIndex = 0
            Me.btnRegist.Text = "登録"
            Me.btnRegist.UseVisualStyleBackColor = True
            '
            'frmYosanFunction
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(820, 543)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Name = "frmYosanFunction"
            Me.Text = "ファンクション/取引先単価設定画面"
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            Me.SplitContainer1.ResumeLayout(False)
            CType(Me.EBomSpread1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.EBomSpread1_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents btnRegist As System.Windows.Forms.Button
        Friend WithEvents EBomSpread1 As FarPoint.Win.Spread.FpSpread
        Friend WithEvents EBomSpread1_Sheet1 As FarPoint.Win.Spread.SheetView
    End Class
End Namespace