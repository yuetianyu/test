<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm41KouseiSourceSelector
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm41KouseiSourceSelector))
        Me.lblKakunin2 = New System.Windows.Forms.Label
        Me.spdParts = New FarPoint.Win.Spread.FpSpread
        Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
        Me.btnTenkai = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSabun = New System.Windows.Forms.Button
        CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblKakunin2
        '
        Me.lblKakunin2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin2.Location = New System.Drawing.Point(12, 20)
        Me.lblKakunin2.Name = "lblKakunin2"
        Me.lblKakunin2.Size = New System.Drawing.Size(521, 25)
        Me.lblKakunin2.TabIndex = 59
        Me.lblKakunin2.Text = "構成再展開"
        Me.lblKakunin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdParts
        '
        Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, "
        Me.spdParts.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.spdParts.BackColor = System.Drawing.SystemColors.Control
        Me.spdParts.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdParts.Location = New System.Drawing.Point(12, 58)
        Me.spdParts.Name = "spdParts"
        Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
        Me.spdParts.Size = New System.Drawing.Size(521, 222)
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
        Me.spdParts_Sheet1.ColumnCount = 4
        Me.spdParts_Sheet1.RowCount = 26
        Me.spdParts_Sheet1.AllowNoteEdit = True
        Me.spdParts_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
        Me.spdParts_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
        Me.spdParts_Sheet1.Cells.Get(0, 0).Value = "M1"
        Me.spdParts_Sheet1.Cells.Get(0, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(0, 1).Value = "50825FJ000"
        Me.spdParts_Sheet1.Cells.Get(1, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(1, 1).Value = "50825FJ000"
        Me.spdParts_Sheet1.Cells.Get(2, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(2, 1).Value = "50825FG000"
        Me.spdParts_Sheet1.Cells.Get(3, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(3, 1).Value = "50825FG010"
        Me.spdParts_Sheet1.Cells.Get(4, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(5, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(6, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(7, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(8, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(9, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(10, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(11, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(12, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(13, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(14, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(15, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(16, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(17, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(18, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(19, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(20, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(21, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(22, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(23, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(24, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(25, 1).Locked = False
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 3
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "ベースにする品番"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ベースにする品番"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "抽出元"
        Me.spdParts_Sheet1.Columns.Get(0).Label = "ベースにする品番"
        Me.spdParts_Sheet1.Columns.Get(0).Width = 25.0!
        Me.spdParts_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.spdParts_Sheet1.Columns.Get(1).Label = "ベースにする品番"
        Me.spdParts_Sheet1.Columns.Get(1).Tag = "HOJO_NAME"
        Me.spdParts_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.spdParts_Sheet1.Columns.Get(1).VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.spdParts_Sheet1.Columns.Get(1).Width = 100.0!
        Me.spdParts_Sheet1.Columns.Get(2).Width = 74.0!
        Me.spdParts_Sheet1.Columns.Get(3).Label = "抽出元"
        Me.spdParts_Sheet1.Columns.Get(3).Width = 250.0!
        Me.spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
        Me.spdParts_Sheet1.RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Letters
        Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.spdParts_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.spdParts_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
        Me.spdParts_Sheet1.Rows.Default.Height = 16.0!
        Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnTenkai
        '
        Me.btnTenkai.Location = New System.Drawing.Point(293, 316)
        Me.btnTenkai.Name = "btnTenkai"
        Me.btnTenkai.Size = New System.Drawing.Size(113, 34)
        Me.btnTenkai.TabIndex = 74
        Me.btnTenkai.Text = "展開"
        Me.btnTenkai.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(427, 316)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(106, 34)
        Me.btnCancel.TabIndex = 75
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSabun
        '
        Me.btnSabun.Location = New System.Drawing.Point(159, 316)
        Me.btnSabun.Name = "btnSabun"
        Me.btnSabun.Size = New System.Drawing.Size(113, 34)
        Me.btnSabun.TabIndex = 76
        Me.btnSabun.Text = "差分反映"
        Me.btnSabun.UseVisualStyleBackColor = True
        Me.btnSabun.Visible = False
        '
        'Frm41KouseiSourceSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(556, 362)
        Me.Controls.Add(Me.btnSabun)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnTenkai)
        Me.Controls.Add(Me.spdParts)
        Me.Controls.Add(Me.lblKakunin2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Frm41KouseiSourceSelector"
        Me.Text = "新試作手配システム Ver 1.00"
        CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblKakunin2 As System.Windows.Forms.Label
    Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnTenkai As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSabun As System.Windows.Forms.Button
End Class
