<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm8DispSekkeika
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm8DispSekkeika))
        Me.lblKakunin = New System.Windows.Forms.Label
        Me.spdParts = New FarPoint.Win.Spread.FpSpread
        Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
        Me.btnKoushin = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblKakunin
        '
        Me.lblKakunin.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin.Location = New System.Drawing.Point(12, 20)
        Me.lblKakunin.Name = "lblKakunin"
        Me.lblKakunin.Size = New System.Drawing.Size(549, 25)
        Me.lblKakunin.TabIndex = 59
        Me.lblKakunin.Text = "設計課更新"
        Me.lblKakunin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdParts
        '
        Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, "
        Me.spdParts.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.spdParts.BackColor = System.Drawing.SystemColors.Control
        Me.spdParts.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdParts.Location = New System.Drawing.Point(15, 61)
        Me.spdParts.Name = "spdParts"
        Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
        Me.spdParts.Size = New System.Drawing.Size(546, 217)
        Me.spdParts.TabIndex = 73
        Me.spdParts.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdParts.VisualStyles = FarPoint.Win.VisualStyles.Off
        '
        'spdParts_Sheet1
        '
        Me.spdParts_Sheet1.Reset()
        Me.spdParts_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spdParts_Sheet1.ColumnCount = 3
        Me.spdParts_Sheet1.RowCount = 26
        Me.spdParts_Sheet1.AllowNoteEdit = True
        Me.spdParts_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
        Me.spdParts_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
        Me.spdParts_Sheet1.Cells.Get(0, 0).Value = "265C　"
        Me.spdParts_Sheet1.Cells.Get(0, 1).Value = "BRAKE PIPE CTR"
        Me.spdParts_Sheet1.Cells.Get(1, 0).Value = "420A　"
        Me.spdParts_Sheet1.Cells.Get(1, 1).Value = "FUEL SYS"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "ブロックNo"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ブロック名称"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "設計課"
        Me.spdParts_Sheet1.Columns.Get(0).AllowAutoFilter = True
        Me.spdParts_Sheet1.Columns.Get(0).AllowAutoSort = True
        Me.spdParts_Sheet1.Columns.Get(0).Label = "ブロックNo"
        Me.spdParts_Sheet1.Columns.Get(0).Width = 99.0!
        Me.spdParts_Sheet1.Columns.Get(1).AllowAutoFilter = True
        Me.spdParts_Sheet1.Columns.Get(1).AllowAutoSort = True
        Me.spdParts_Sheet1.Columns.Get(1).Label = "ブロック名称"
        Me.spdParts_Sheet1.Columns.Get(1).Width = 150.0!
        Me.spdParts_Sheet1.Columns.Get(2).AllowAutoFilter = True
        Me.spdParts_Sheet1.Columns.Get(2).AllowAutoSort = True
        Me.spdParts_Sheet1.Columns.Get(2).Label = "設計課"
        Me.spdParts_Sheet1.Columns.Get(2).Width = 240.0!
        Me.spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
        Me.spdParts_Sheet1.RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Letters
        Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.spdParts_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.spdParts_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
        Me.spdParts_Sheet1.Rows.Default.Height = 16.0!
        Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnKoushin
        '
        Me.btnKoushin.Location = New System.Drawing.Point(321, 297)
        Me.btnKoushin.Name = "btnKoushin"
        Me.btnKoushin.Size = New System.Drawing.Size(113, 34)
        Me.btnKoushin.TabIndex = 74
        Me.btnKoushin.Text = "更新"
        Me.btnKoushin.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(455, 297)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(106, 34)
        Me.btnCancel.TabIndex = 75
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Frm8DispSekkeika
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(579, 350)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnKoushin)
        Me.Controls.Add(Me.spdParts)
        Me.Controls.Add(Me.lblKakunin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Frm8DispSekkeika"
        Me.Text = "新試作手配システム Ver 1.00"
        CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblKakunin As System.Windows.Forms.Label
    Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnKoushin As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
