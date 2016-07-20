<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm41DispKosei
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
        Dim LineBorder1 As FarPoint.Win.LineBorder = New FarPoint.Win.LineBorder(System.Drawing.Color.Navy)
        Dim LineBorder2 As FarPoint.Win.LineBorder = New FarPoint.Win.LineBorder(System.Drawing.Color.Navy)
        Dim LineBorder3 As FarPoint.Win.LineBorder = New FarPoint.Win.LineBorder(System.Drawing.Color.Navy)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm41DispKosei))
        Me.spdResult_Sheet1 = New FarPoint.Win.Spread.SheetView
        Me.spdResult = New FarPoint.Win.Spread.FpSpread
        CType(Me.spdResult_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'spdResult_Sheet1
        '
        Me.spdResult_Sheet1.Reset()
        Me.spdResult_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spdResult_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spdResult_Sheet1.ColumnCount = 6
        Me.spdResult_Sheet1.AllowNoteEdit = True
        Me.spdResult_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
        Me.spdResult_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
        Me.spdResult_Sheet1.Cells.Get(0, 0).BackColor = System.Drawing.Color.Navy
        Me.spdResult_Sheet1.Cells.Get(0, 0).ParseFormatString = "G"
        Me.spdResult_Sheet1.Cells.Get(0, 1).BackColor = System.Drawing.Color.Navy
        Me.spdResult_Sheet1.Cells.Get(0, 1).Locked = True
        Me.spdResult_Sheet1.Cells.Get(0, 1).NoteStyle = FarPoint.Win.Spread.NoteStyle.PopupNote
        Me.spdResult_Sheet1.Cells.Get(0, 1).ParseFormatString = "G"
        Me.spdResult_Sheet1.Cells.Get(0, 1).Value = "構成データがありません"
        Me.spdResult_Sheet1.Cells.Get(1, 0).Border = LineBorder1
        Me.spdResult_Sheet1.ColumnHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Numbers
        Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 0).VisualStyles = FarPoint.Win.VisualStyles.Off
        Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 1).VisualStyles = FarPoint.Win.VisualStyles.Off
        Me.spdResult_Sheet1.ColumnHeader.Cells.Get(0, 5).VisualStyles = FarPoint.Win.VisualStyles.Off
        Me.spdResult_Sheet1.ColumnHeader.Rows.Get(0).Height = 0.0!
        Me.spdResult_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.Navy
        Me.spdResult_Sheet1.Columns.Get(0).Border = LineBorder2
        Me.spdResult_Sheet1.Columns.Get(0).ForeColor = System.Drawing.Color.White
        Me.spdResult_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
        Me.spdResult_Sheet1.Columns.Get(0).Locked = True
        Me.spdResult_Sheet1.Columns.Get(0).Width = 104.0!
        Me.spdResult_Sheet1.Columns.Get(1).BackColor = System.Drawing.Color.Navy
        Me.spdResult_Sheet1.Columns.Get(1).ForeColor = System.Drawing.Color.White
        Me.spdResult_Sheet1.Columns.Get(1).Locked = True
        Me.spdResult_Sheet1.Columns.Get(1).Width = 134.0!
        Me.spdResult_Sheet1.Columns.Get(2).BackColor = System.Drawing.Color.Navy
        Me.spdResult_Sheet1.Columns.Get(2).ForeColor = System.Drawing.Color.White
        Me.spdResult_Sheet1.Columns.Get(2).Width = 26.0!
        Me.spdResult_Sheet1.Columns.Get(3).BackColor = System.Drawing.Color.Navy
        Me.spdResult_Sheet1.Columns.Get(3).ForeColor = System.Drawing.Color.White
        Me.spdResult_Sheet1.Columns.Get(3).Width = 20.0!
        Me.spdResult_Sheet1.Columns.Get(4).BackColor = System.Drawing.Color.Navy
        Me.spdResult_Sheet1.Columns.Get(4).ForeColor = System.Drawing.Color.White
        Me.spdResult_Sheet1.Columns.Get(4).Width = 20.0!
        Me.spdResult_Sheet1.Columns.Get(5).BackColor = System.Drawing.Color.Navy
        Me.spdResult_Sheet1.Columns.Get(5).ForeColor = System.Drawing.Color.White
        Me.spdResult_Sheet1.Columns.Get(5).Width = 44.0!
        Me.spdResult_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.DarkBlue
        Me.spdResult_Sheet1.DefaultStyle.Border = LineBorder3
        Me.spdResult_Sheet1.DefaultStyle.Locked = True
        Me.spdResult_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.spdResult_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
        Me.spdResult_Sheet1.RowHeader.Cells.Get(3, 0).VisualStyles = FarPoint.Win.VisualStyles.Off
        Me.spdResult_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.spdResult_Sheet1.RowHeader.Columns.Get(0).Width = 0.0!
        Me.spdResult_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.spdResult_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
        Me.spdResult_Sheet1.Rows.Default.Height = 16.0!
        Me.spdResult_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'spdResult
        '
        Me.spdResult.AccessibleDescription = "spdResult, Sheet1, Row 0, Column 0, 1234∟1234567890"
        Me.spdResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdResult.BackColor = System.Drawing.SystemColors.Control
        Me.spdResult.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdResult.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdResult.Location = New System.Drawing.Point(-1, 0)
        Me.spdResult.Name = "spdResult"
        Me.spdResult.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.spdResult.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdResult_Sheet1})
        Me.spdResult.Size = New System.Drawing.Size(367, 421)
        Me.spdResult.TabIndex = 78
        Me.spdResult.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdResult.VisualStyles = FarPoint.Win.VisualStyles.Off
        '
        'Frm41DispKosei
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(366, 402)
        Me.Controls.Add(Me.spdResult)
        Me.Font = New System.Drawing.Font("メイリオ", 9.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm41DispKosei"
        Me.TopMost = True
        CType(Me.spdResult_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spdResult_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents spdResult As FarPoint.Win.Spread.FpSpread
End Class
