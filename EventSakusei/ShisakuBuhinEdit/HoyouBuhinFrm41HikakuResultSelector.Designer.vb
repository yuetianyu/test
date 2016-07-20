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
        Me.components = New System.ComponentModel.Container
        Dim CheckBoxCellType1 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HoyouBuhinFrm41HikakuResultSelector))
        Me.spdResult = New EBom.FpSpread.EBomSpread(Me.components)
        Me.spdResult_Sheet1 = New FarPoint.Win.Spread.SheetView
        Me.btnNewKousei = New System.Windows.Forms.Button
        Me.btnCheckOnly = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        CType(Me.spdResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdResult_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'spdResult
        '
        Me.spdResult.AccessibleDescription = "spdResult, Sheet1, Row 0, Column 0, "
        Me.spdResult.AllowDeleteCell = True
        Me.spdResult.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.spdResult.FocusRendererColor = System.Drawing.Color.Empty
        Me.spdResult.FocusThicness = 1
        Me.spdResult.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdResult.Location = New System.Drawing.Point(32, 21)
        Me.spdResult.MoveEnterDirection = EBom.FpSpread.EBomSpread.MOVE_ENTER_DIRECTION.NEXT_COLUMN_WARP
        Me.spdResult.Name = "spdResult"
        Me.spdResult.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.spdResult.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdResult_Sheet1})
        Me.spdResult.Size = New System.Drawing.Size(447, 201)
        Me.spdResult.TabIndex = 0
        Me.spdResult.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        '
        'spdResult_Sheet1
        '
        Me.spdResult_Sheet1.Reset()
        Me.spdResult_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spdResult_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spdResult_Sheet1.ColumnCount = 5
        Me.spdResult_Sheet1.AllowNoteEdit = True
        Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "選択"
        Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "フラグ"
        Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "部品番号"
        Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "部品名称"
        Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "員数"
        Me.spdResult_Sheet1.Columns.Get(0).CellType = CheckBoxCellType1
        Me.spdResult_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        Me.spdResult_Sheet1.Columns.Get(0).Label = "選択"
        Me.spdResult_Sheet1.Columns.Get(0).Width = 58.0!
        Me.spdResult_Sheet1.Columns.Get(1).Label = "フラグ"
        Me.spdResult_Sheet1.Columns.Get(1).Width = 76.0!
        Me.spdResult_Sheet1.Columns.Get(2).Label = "部品番号"
        Me.spdResult_Sheet1.Columns.Get(2).Width = 91.0!
        Me.spdResult_Sheet1.Columns.Get(3).Label = "部品名称"
        Me.spdResult_Sheet1.Columns.Get(3).Width = 133.0!
        Me.spdResult_Sheet1.Columns.Get(4).Label = "員数"
        Me.spdResult_Sheet1.Columns.Get(4).Width = 69.0!
        Me.spdResult_Sheet1.RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank
        Me.spdResult_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.spdResult_Sheet1.RowHeader.Visible = False
        Me.spdResult_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnNewKousei
        '
        Me.btnNewKousei.Location = New System.Drawing.Point(32, 231)
        Me.btnNewKousei.Name = "btnNewKousei"
        Me.btnNewKousei.Size = New System.Drawing.Size(107, 23)
        Me.btnNewKousei.TabIndex = 1
        Me.btnNewKousei.Text = "最新構成を反映"
        Me.btnNewKousei.UseVisualStyleBackColor = True
        '
        'btnCheckOnly
        '
        Me.btnCheckOnly.Location = New System.Drawing.Point(381, 231)
        Me.btnCheckOnly.Name = "btnCheckOnly"
        Me.btnCheckOnly.Size = New System.Drawing.Size(98, 23)
        Me.btnCheckOnly.TabIndex = 2
        Me.btnCheckOnly.Text = "チェックのみ反映"
        Me.btnCheckOnly.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(381, 260)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(98, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'HoyouBuhinFrm41HikakuResultSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.ClientSize = New System.Drawing.Size(511, 300)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnCheckOnly)
        Me.Controls.Add(Me.btnNewKousei)
        Me.Controls.Add(Me.spdResult)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "HoyouBuhinFrm41HikakuResultSelector"
        Me.Text = "新試作手配システム Ver 1.00"
        CType(Me.spdResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdResult_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spdResult As EBom.FpSpread.EBomSpread
    Friend WithEvents spdResult_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnNewKousei As System.Windows.Forms.Button
    Friend WithEvents btnCheckOnly As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
