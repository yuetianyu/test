Namespace YosanSetteiBuhinEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmStandardSetting
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
            Dim TextCellType11 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType12 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
            Me.EBomSpread1 = New EBom.FpSpread.EBomSpread(Me.components)
            Me.EBomSpread1_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.btnBack = New System.Windows.Forms.Button
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
            Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
            Me.SplitContainer1.IsSplitterFixed = True
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
            Me.SplitContainer1.Panel2.Controls.Add(Me.btnBack)
            Me.SplitContainer1.Size = New System.Drawing.Size(716, 176)
            Me.SplitContainer1.SplitterDistance = 144
            Me.SplitContainer1.TabIndex = 0
            '
            'EBomSpread1
            '
            Me.EBomSpread1.AccessibleDescription = "EBomSpread1, Sheet1, Row 0, Column 0, "
            Me.EBomSpread1.AllowDeleteCell = True
            Me.EBomSpread1.BackColor = System.Drawing.SystemColors.Control
            Me.EBomSpread1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.EBomSpread1.FocusRendererColor = System.Drawing.Color.Empty
            Me.EBomSpread1.FocusThicness = 1
            Me.EBomSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.EBomSpread1.Location = New System.Drawing.Point(0, 0)
            Me.EBomSpread1.MoveEnterDirection = EBom.FpSpread.EBomSpread.MOVE_ENTER_DIRECTION.NEXT_COLUMN_WARP
            Me.EBomSpread1.Name = "EBomSpread1"
            Me.EBomSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.EBomSpread1.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.EBomSpread1_Sheet1})
            Me.EBomSpread1.Size = New System.Drawing.Size(716, 144)
            Me.EBomSpread1.TabIndex = 0
            Me.EBomSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            '
            'EBomSpread1_Sheet1
            '
            Me.EBomSpread1_Sheet1.Reset()
            Me.EBomSpread1_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.EBomSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.EBomSpread1_Sheet1.ColumnCount = 2
            Me.EBomSpread1_Sheet1.AllowNoteEdit = True
            Me.EBomSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Tag = "NAME"
            Me.EBomSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "標準設定名"
            Me.EBomSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Tag = "SETTING"
            Me.EBomSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "設定内容"
            Me.EBomSpread1_Sheet1.Columns.Get(0).CellType = TextCellType11
            Me.EBomSpread1_Sheet1.Columns.Get(0).Label = "標準設定名"
            Me.EBomSpread1_Sheet1.Columns.Get(0).Locked = True
            Me.EBomSpread1_Sheet1.Columns.Get(0).Tag = "NAME"
            Me.EBomSpread1_Sheet1.Columns.Get(0).Width = 141.0!
            Me.EBomSpread1_Sheet1.Columns.Get(1).CellType = TextCellType12
            Me.EBomSpread1_Sheet1.Columns.Get(1).Label = "設定内容"
            Me.EBomSpread1_Sheet1.Columns.Get(1).Locked = True
            Me.EBomSpread1_Sheet1.Columns.Get(1).Tag = "SETTING"
            Me.EBomSpread1_Sheet1.Columns.Get(1).Width = 519.0!
            Me.EBomSpread1_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.EBomSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'btnBack
            '
            Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBack.Location = New System.Drawing.Point(638, 3)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(75, 23)
            Me.btnBack.TabIndex = 0
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = True
            '
            'frmStandardSetting
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(716, 176)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Name = "frmStandardSetting"
            Me.Text = "標準設定表"
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            Me.SplitContainer1.ResumeLayout(False)
            CType(Me.EBomSpread1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.EBomSpread1_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents EBomSpread1 As EBom.FpSpread.EBomSpread
        Friend WithEvents EBomSpread1_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents btnBack As System.Windows.Forms.Button
    End Class

End Namespace
