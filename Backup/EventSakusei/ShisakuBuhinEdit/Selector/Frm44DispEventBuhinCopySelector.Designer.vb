Namespace ShisakuBuhinEdit.Selector
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class  Frm44DispEventBuhinCopySelector
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm44DispEventBuhinCopySelector))
            Me.btnOK = New System.Windows.Forms.Button
            Me.btnCancel = New System.Windows.Forms.Button
            Me.spdEvent = New FarPoint.Win.Spread.FpSpread
            Me.spdEvent_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.cmbKaihatsuFugo = New System.Windows.Forms.ComboBox
            CType(Me.spdEvent, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdEvent_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'btnOK
            '
            Me.btnOK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnOK.Location = New System.Drawing.Point(518, 312)
            Me.btnOK.Name = "btnOK"
            Me.btnOK.Size = New System.Drawing.Size(79, 24)
            Me.btnOK.TabIndex = 0
            Me.btnOK.Text = "選択"
            Me.btnOK.UseVisualStyleBackColor = True
            '
            'btnCancel
            '
            Me.btnCancel.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
            Me.btnCancel.Location = New System.Drawing.Point(633, 312)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(79, 24)
            Me.btnCancel.TabIndex = 1
            Me.btnCancel.Text = "キャンセル"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'spdEvent
            '
            Me.spdEvent.AccessibleDescription = "spdEvent, Sheet1, Row 0, Column 0, "
            Me.spdEvent.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.spdEvent.BackColor = System.Drawing.SystemColors.Control
            Me.spdEvent.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdEvent.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdEvent.Location = New System.Drawing.Point(23, 39)
            Me.spdEvent.Name = "spdEvent"
            Me.spdEvent.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdEvent.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdEvent_Sheet1})
            Me.spdEvent.Size = New System.Drawing.Size(688, 267)
            Me.spdEvent.TabIndex = 73
            Me.spdEvent.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdEvent.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdEvent_Sheet1
            '
            Me.spdEvent_Sheet1.Reset()
            Me.spdEvent_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdEvent_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdEvent_Sheet1.ColumnCount = 5
            Me.spdEvent_Sheet1.RowCount = 11
            Me.spdEvent_Sheet1.AllowNoteEdit = True
            Me.spdEvent_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdEvent_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdEvent_Sheet1.Cells.Get(0, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(0, 4).Value = "全て選択"
            Me.spdEvent_Sheet1.Cells.Get(1, 4).Value = "開発符号"
            Me.spdEvent_Sheet1.Cells.Get(2, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(2, 4).Value = "仕様情報Ｎｏ"
            Me.spdEvent_Sheet1.Cells.Get(3, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(3, 4).Value = "アプライドNo"
            Me.spdEvent_Sheet1.Cells.Get(4, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(4, 4).Value = "型式"
            Me.spdEvent_Sheet1.Cells.Get(5, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(5, 4).Value = "仕向"
            Me.spdEvent_Sheet1.Cells.Get(6, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(6, 4).Value = "ＯＰ"
            Me.spdEvent_Sheet1.Cells.Get(7, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(7, 4).Value = "外装色"
            Me.spdEvent_Sheet1.Cells.Get(8, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(8, 4).Value = "内装色"
            Me.spdEvent_Sheet1.Cells.Get(9, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(9, 4).Value = "イベントコード"
            Me.spdEvent_Sheet1.Cells.Get(10, 4).Locked = True
            Me.spdEvent_Sheet1.Cells.Get(10, 4).Value = "号車"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "イベントコード"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "開発符号"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "イベント"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "ユニット区分"
            Me.spdEvent_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "イベント名称"
            Me.spdEvent_Sheet1.ColumnHeader.Rows.Get(0).Height = 36.0!
            Me.spdEvent_Sheet1.Columns.Get(0).Label = "イベントコード"
            Me.spdEvent_Sheet1.Columns.Get(0).Width = 104.0!
            Me.spdEvent_Sheet1.Columns.Get(4).CellType = TextCellType1
            Me.spdEvent_Sheet1.Columns.Get(4).Label = "イベント名称"
            Me.spdEvent_Sheet1.Columns.Get(4).Locked = True
            Me.spdEvent_Sheet1.Columns.Get(4).Tag = "HOJO_NAME"
            Me.spdEvent_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdEvent_Sheet1.Columns.Get(4).Width = 346.0!
            Me.spdEvent_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdEvent_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdEvent_Sheet1.RowHeader.DefaultStyle.Locked = False
            Me.spdEvent_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
            Me.spdEvent_Sheet1.Rows.Default.Height = 16.0!
            Me.spdEvent_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'cmbKaihatsuFugo
            '
            Me.cmbKaihatsuFugo.FormattingEnabled = True
            Me.cmbKaihatsuFugo.Location = New System.Drawing.Point(23, 13)
            Me.cmbKaihatsuFugo.Name = "cmbKaihatsuFugo"
            Me.cmbKaihatsuFugo.Size = New System.Drawing.Size(121, 20)
            Me.cmbKaihatsuFugo.TabIndex = 74
            '
            'Frm44DispEventBuhinCopySelector
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(737, 352)
            Me.Controls.Add(Me.cmbKaihatsuFugo)
            Me.Controls.Add(Me.spdEvent)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnOK)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "Frm44DispEventBuhinCopySelector"
            Me.Text = "新試作手配システム Ver 1.00"
            CType(Me.spdEvent, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdEvent_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents btnOK As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents spdEvent As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdEvent_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents cmbKaihatsuFugo As System.Windows.Forms.ComboBox
End Class
End Namespace