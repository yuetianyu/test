<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBodyMSTMainte
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
        Dim TextCellType15 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Dim TextCellType16 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Dim TextCellType17 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Dim TextCellType18 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Dim TextCellType19 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Dim TextCellType20 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Dim TextCellType21 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Me.lblKaihatuFugo = New System.Windows.Forms.Label
        Me.lblBodyName = New System.Windows.Forms.Label
        Me.cmbKaihatuFugo = New System.Windows.Forms.ComboBox
        Me.cmbBodyName = New System.Windows.Forms.ComboBox
        Me.btnFind = New System.Windows.Forms.Button
        Me.spdParts = New FarPoint.Win.Spread.FpSpread
        Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
        Me.btnExcelInput = New System.Windows.Forms.Button
        Me.btnExcelOutPut = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnRegist = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnReturn = New System.Windows.Forms.Button
        Me.btnEND = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblCurrBukaName = New System.Windows.Forms.Label
        Me.LblCurrUserId = New System.Windows.Forms.Label
        Me.LblTimeNow = New System.Windows.Forms.Label
        Me.LblDateNow = New System.Windows.Forms.Label
        Me.pgId = New System.Windows.Forms.Label
        Me.menuTitle = New System.Windows.Forms.Label
        Me.btnAllocate = New System.Windows.Forms.Button
        CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblKaihatuFugo
        '
        Me.lblKaihatuFugo.AutoSize = True
        Me.lblKaihatuFugo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKaihatuFugo.Location = New System.Drawing.Point(15, 74)
        Me.lblKaihatuFugo.Name = "lblKaihatuFugo"
        Me.lblKaihatuFugo.Size = New System.Drawing.Size(72, 16)
        Me.lblKaihatuFugo.TabIndex = 0
        Me.lblKaihatuFugo.Text = "開発符号"
        '
        'lblBodyName
        '
        Me.lblBodyName.AutoSize = True
        Me.lblBodyName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBodyName.Location = New System.Drawing.Point(162, 73)
        Me.lblBodyName.Name = "lblBodyName"
        Me.lblBodyName.Size = New System.Drawing.Size(72, 16)
        Me.lblBodyName.TabIndex = 1
        Me.lblBodyName.Text = "ボディー名"
        '
        'cmbKaihatuFugo
        '
        Me.cmbKaihatuFugo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKaihatuFugo.FormattingEnabled = True
        Me.cmbKaihatuFugo.Location = New System.Drawing.Point(93, 70)
        Me.cmbKaihatuFugo.Name = "cmbKaihatuFugo"
        Me.cmbKaihatuFugo.Size = New System.Drawing.Size(63, 24)
        Me.cmbKaihatuFugo.TabIndex = 1
        '
        'cmbBodyName
        '
        Me.cmbBodyName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBodyName.FormattingEnabled = True
        Me.cmbBodyName.Location = New System.Drawing.Point(240, 70)
        Me.cmbBodyName.Name = "cmbBodyName"
        Me.cmbBodyName.Size = New System.Drawing.Size(139, 24)
        Me.cmbBodyName.TabIndex = 5
        '
        'btnFind
        '
        Me.btnFind.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFind.Location = New System.Drawing.Point(399, 70)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(75, 24)
        Me.btnFind.TabIndex = 10
        Me.btnFind.Text = "検索"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'spdParts
        '
        Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, 開発符号"
        Me.spdParts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdParts.BackColor = System.Drawing.SystemColors.Control
        Me.spdParts.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentCell
        Me.spdParts.EditModeReplace = True
        Me.spdParts.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdParts.Location = New System.Drawing.Point(18, 138)
        Me.spdParts.Name = "spdParts"
        Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
        Me.spdParts.Size = New System.Drawing.Size(1005, 380)
        Me.spdParts.TabIndex = 25
        Me.spdParts.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdParts.VisualStyles = FarPoint.Win.VisualStyles.Off
        '
        'spdParts_Sheet1
        '
        Me.spdParts_Sheet1.Reset()
        Me.spdParts_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spdParts_Sheet1.ColumnCount = 7
        Me.spdParts_Sheet1.RowCount = 10
        Me.spdParts_Sheet1.AllowNoteEdit = True
        Me.spdParts_Sheet1.Cells.Get(0, 0).Value = "開発符号"
        Me.spdParts_Sheet1.Cells.Get(0, 1).Value = "ボディー名"
        Me.spdParts_Sheet1.Cells.Get(0, 2).Value = "ブロック番号"
        Me.spdParts_Sheet1.Cells.Get(0, 3).Value = "部品番号"
        Me.spdParts_Sheet1.Cells.Get(0, 4).Value = "XVLファイル名"
        Me.spdParts_Sheet1.Cells.Get(0, 5).Value = "CADデータイベント区分"
        Me.spdParts_Sheet1.Cells.Get(0, 6).Value = "改訂"
        Me.spdParts_Sheet1.Columns.Get(0).CellType = TextCellType15
        Me.spdParts_Sheet1.Columns.Get(0).Width = 63.0!
        Me.spdParts_Sheet1.Columns.Get(1).CellType = TextCellType16
        Me.spdParts_Sheet1.Columns.Get(1).Width = 170.0!
        Me.spdParts_Sheet1.Columns.Get(2).CellType = TextCellType17
        Me.spdParts_Sheet1.Columns.Get(2).Width = 81.0!
        Me.spdParts_Sheet1.Columns.Get(3).CellType = TextCellType18
        Me.spdParts_Sheet1.Columns.Get(3).Width = 134.0!
        Me.spdParts_Sheet1.Columns.Get(4).CellType = TextCellType19
        Me.spdParts_Sheet1.Columns.Get(4).Width = 350.0!
        Me.spdParts_Sheet1.Columns.Get(5).CellType = TextCellType20
        Me.spdParts_Sheet1.Columns.Get(5).Width = 133.0!
        Me.spdParts_Sheet1.Columns.Get(6).CellType = TextCellType21
        Me.spdParts_Sheet1.Columns.Get(6).Width = 33.0!
        Me.spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.[ReadOnly]
        Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnExcelInput
        '
        Me.btnExcelInput.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExcelInput.Location = New System.Drawing.Point(133, 108)
        Me.btnExcelInput.Name = "btnExcelInput"
        Me.btnExcelInput.Size = New System.Drawing.Size(109, 24)
        Me.btnExcelInput.TabIndex = 152
        Me.btnExcelInput.Text = "Excel取込"
        Me.btnExcelInput.UseVisualStyleBackColor = True
        '
        'btnExcelOutPut
        '
        Me.btnExcelOutPut.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExcelOutPut.Location = New System.Drawing.Point(18, 108)
        Me.btnExcelOutPut.Name = "btnExcelOutPut"
        Me.btnExcelOutPut.Size = New System.Drawing.Size(109, 24)
        Me.btnExcelOutPut.TabIndex = 153
        Me.btnExcelOutPut.Text = "Excel出力"
        Me.btnExcelOutPut.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(18, 534)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(109, 24)
        Me.btnDelete.TabIndex = 15
        Me.btnDelete.Text = "削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnRegist
        '
        Me.btnRegist.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRegist.Location = New System.Drawing.Point(912, 533)
        Me.btnRegist.Name = "btnRegist"
        Me.btnRegist.Size = New System.Drawing.Size(109, 24)
        Me.btnRegist.TabIndex = 20
        Me.btnRegist.Text = "登録"
        Me.btnRegist.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnReturn)
        Me.Panel2.Controls.Add(Me.btnEND)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 32)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1035, 33)
        Me.Panel2.TabIndex = 155
        '
        'btnReturn
        '
        Me.btnReturn.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReturn.Location = New System.Drawing.Point(946, 4)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(75, 24)
        Me.btnReturn.TabIndex = 156
        Me.btnReturn.Text = "戻る"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'btnEND
        '
        Me.btnEND.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEND.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEND.Location = New System.Drawing.Point(4, 4)
        Me.btnEND.Name = "btnEND"
        Me.btnEND.Size = New System.Drawing.Size(149, 24)
        Me.btnEND.TabIndex = 8
        Me.btnEND.Text = "アプリケーション終了"
        Me.btnEND.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.BackColor = System.Drawing.Color.Blue
        Me.Panel1.Controls.Add(Me.LblCurrBukaName)
        Me.Panel1.Controls.Add(Me.LblCurrUserId)
        Me.Panel1.Controls.Add(Me.LblTimeNow)
        Me.Panel1.Controls.Add(Me.LblDateNow)
        Me.Panel1.Controls.Add(Me.pgId)
        Me.Panel1.Controls.Add(Me.menuTitle)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1035, 32)
        Me.Panel1.TabIndex = 154
        '
        'LblCurrBukaName
        '
        Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
        Me.LblCurrBukaName.Location = New System.Drawing.Point(810, 13)
        Me.LblCurrBukaName.Name = "LblCurrBukaName"
        Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrBukaName.TabIndex = 77
        Me.LblCurrBukaName.Text = "(部課：SKE1         )"
        '
        'LblCurrUserId
        '
        Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
        Me.LblCurrUserId.Location = New System.Drawing.Point(810, 0)
        Me.LblCurrUserId.Name = "LblCurrUserId"
        Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrUserId.TabIndex = 76
        Me.LblCurrUserId.Text = "(ID    ：ABCDEFGH)"
        '
        'LblTimeNow
        '
        Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTimeNow.AutoSize = True
        Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTimeNow.ForeColor = System.Drawing.Color.White
        Me.LblTimeNow.Location = New System.Drawing.Point(970, 13)
        Me.LblTimeNow.Name = "LblTimeNow"
        Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
        Me.LblTimeNow.TabIndex = 75
        Me.LblTimeNow.Text = "HH:MM:DD"
        '
        'LblDateNow
        '
        Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDateNow.AutoSize = True
        Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDateNow.ForeColor = System.Drawing.Color.White
        Me.LblDateNow.Location = New System.Drawing.Point(946, 0)
        Me.LblDateNow.Name = "LblDateNow"
        Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
        Me.LblDateNow.TabIndex = 74
        Me.LblDateNow.Text = "YYYY/MM/DD"
        '
        'pgId
        '
        Me.pgId.AutoSize = True
        Me.pgId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pgId.ForeColor = System.Drawing.Color.White
        Me.pgId.Location = New System.Drawing.Point(3, 1)
        Me.pgId.Name = "pgId"
        Me.pgId.Size = New System.Drawing.Size(103, 13)
        Me.pgId.TabIndex = 59
        Me.pgId.Text = "PG-ID：MENU002"
        '
        'menuTitle
        '
        Me.menuTitle.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.menuTitle.ForeColor = System.Drawing.Color.Yellow
        Me.menuTitle.Location = New System.Drawing.Point(447, 1)
        Me.menuTitle.Name = "menuTitle"
        Me.menuTitle.Size = New System.Drawing.Size(139, 31)
        Me.menuTitle.TabIndex = 54
        Me.menuTitle.Text = "【SKE1】　メニュー"
        Me.menuTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAllocate
        '
        Me.btnAllocate.Font = New System.Drawing.Font("MS UI Gothic", 12.0!)
        Me.btnAllocate.Location = New System.Drawing.Point(248, 108)
        Me.btnAllocate.Name = "btnAllocate"
        Me.btnAllocate.Size = New System.Drawing.Size(109, 24)
        Me.btnAllocate.TabIndex = 156
        Me.btnAllocate.Text = "ファイル引当"
        Me.btnAllocate.UseVisualStyleBackColor = True
        '
        'FrmBodyMSTMainte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1035, 569)
        Me.Controls.Add(Me.btnAllocate)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnRegist)
        Me.Controls.Add(Me.btnExcelOutPut)
        Me.Controls.Add(Me.btnExcelInput)
        Me.Controls.Add(Me.spdParts)
        Me.Controls.Add(Me.btnFind)
        Me.Controls.Add(Me.cmbBodyName)
        Me.Controls.Add(Me.cmbKaihatuFugo)
        Me.Controls.Add(Me.lblBodyName)
        Me.Controls.Add(Me.lblKaihatuFugo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBodyMSTMainte"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ボディーマスタメンテ"
        CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblKaihatuFugo As System.Windows.Forms.Label
    Friend WithEvents lblBodyName As System.Windows.Forms.Label
    Friend WithEvents cmbKaihatuFugo As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBodyName As System.Windows.Forms.ComboBox
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents btnExcelInput As System.Windows.Forms.Button
    Friend WithEvents btnExcelOutPut As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnRegist As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnEND As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
    Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
    Friend WithEvents LblTimeNow As System.Windows.Forms.Label
    Friend WithEvents LblDateNow As System.Windows.Forms.Label
    Friend WithEvents pgId As System.Windows.Forms.Label
    Friend WithEvents menuTitle As System.Windows.Forms.Label
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents btnAllocate As System.Windows.Forms.Button
End Class
