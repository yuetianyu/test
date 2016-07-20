Namespace YosanSetteiBuhinEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class PartsMultiSetting
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PartsMultiSetting))
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.btnBack = New System.Windows.Forms.Button
            Me.Label4 = New System.Windows.Forms.Label
            Me.LblCurrPGId = New System.Windows.Forms.Label
            Me.Label1 = New System.Windows.Forms.Label
            Me.cmbSettingColumn = New System.Windows.Forms.ComboBox
            Me.Label2 = New System.Windows.Forms.Label
            Me.cmbItemName = New System.Windows.Forms.ComboBox
            Me.Label3 = New System.Windows.Forms.Label
            Me.cmbItemContents = New System.Windows.Forms.ComboBox
            Me.Label5 = New System.Windows.Forms.Label
            Me.cmbDirections = New System.Windows.Forms.ComboBox
            Me.btnDefault = New System.Windows.Forms.Button
            Me.Label6 = New System.Windows.Forms.Label
            Me.tbInput = New System.Windows.Forms.TextBox
            Me.btnGa = New System.Windows.Forms.Button
            Me.btnNi = New System.Windows.Forms.Button
            Me.btnNotokini = New System.Windows.Forms.Button
            Me.btnDe = New System.Windows.Forms.Button
            Me.btnAdd = New System.Windows.Forms.Button
            Me.tbOutPut = New System.Windows.Forms.TextBox
            Me.GroupBox1 = New System.Windows.Forms.GroupBox
            Me.rbAllCell = New System.Windows.Forms.RadioButton
            Me.rbBlackCellOnly = New System.Windows.Forms.RadioButton
            Me.btnExecution = New System.Windows.Forms.Button
            Me.btnClear = New System.Windows.Forms.Button
            Me.btnWo = New System.Windows.Forms.Button
            Me.Panel1.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.btnBack)
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Controls.Add(Me.LblCurrPGId)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(360, 32)
            Me.Panel1.TabIndex = 54
            '
            'btnBack
            '
            Me.btnBack.BackColor = System.Drawing.SystemColors.Control
            Me.btnBack.Location = New System.Drawing.Point(271, 6)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(75, 23)
            Me.btnBack.TabIndex = 1
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = False
            '
            'Label4
            '
            Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold)
            Me.Label4.ForeColor = System.Drawing.Color.Yellow
            Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Label4.Location = New System.Drawing.Point(4, 1)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(203, 31)
            Me.Label4.TabIndex = 54
            Me.Label4.Text = "マルチ設定ウィンドウ"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'LblCurrPGId
            '
            Me.LblCurrPGId.AutoSize = True
            Me.LblCurrPGId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!)
            Me.LblCurrPGId.ForeColor = System.Drawing.Color.White
            Me.LblCurrPGId.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.LblCurrPGId.Location = New System.Drawing.Point(4, 4)
            Me.LblCurrPGId.Name = "LblCurrPGId"
            Me.LblCurrPGId.Size = New System.Drawing.Size(0, 13)
            Me.LblCurrPGId.TabIndex = 54
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 56)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(90, 12)
            Me.Label1.TabIndex = 55
            Me.Label1.Text = "・設定する項目列"
            '
            'cmbSettingColumn
            '
            Me.cmbSettingColumn.FormattingEnabled = True
            Me.cmbSettingColumn.Location = New System.Drawing.Point(14, 71)
            Me.cmbSettingColumn.Name = "cmbSettingColumn"
            Me.cmbSettingColumn.Size = New System.Drawing.Size(179, 20)
            Me.cmbSettingColumn.TabIndex = 3
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(12, 94)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(47, 12)
            Me.Label2.TabIndex = 57
            Me.Label2.Text = "・項目名"
            '
            'cmbItemName
            '
            Me.cmbItemName.FormattingEnabled = True
            Me.cmbItemName.Location = New System.Drawing.Point(14, 109)
            Me.cmbItemName.Name = "cmbItemName"
            Me.cmbItemName.Size = New System.Drawing.Size(179, 20)
            Me.cmbItemName.TabIndex = 4
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(12, 132)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(59, 12)
            Me.Label3.TabIndex = 59
            Me.Label3.Text = "・項目内容"
            '
            'cmbItemContents
            '
            Me.cmbItemContents.FormattingEnabled = True
            Me.cmbItemContents.Location = New System.Drawing.Point(14, 147)
            Me.cmbItemContents.Name = "cmbItemContents"
            Me.cmbItemContents.Size = New System.Drawing.Size(179, 20)
            Me.cmbItemContents.TabIndex = 5
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(12, 170)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(59, 12)
            Me.Label5.TabIndex = 61
            Me.Label5.Text = "・指示内容"
            '
            'cmbDirections
            '
            Me.cmbDirections.FormattingEnabled = True
            Me.cmbDirections.Location = New System.Drawing.Point(14, 185)
            Me.cmbDirections.Name = "cmbDirections"
            Me.cmbDirections.Size = New System.Drawing.Size(179, 20)
            Me.cmbDirections.TabIndex = 6
            '
            'btnDefault
            '
            Me.btnDefault.Location = New System.Drawing.Point(246, 38)
            Me.btnDefault.Name = "btnDefault"
            Me.btnDefault.Size = New System.Drawing.Size(75, 23)
            Me.btnDefault.TabIndex = 2
            Me.btnDefault.Text = "標準"
            Me.btnDefault.UseVisualStyleBackColor = True
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(244, 94)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(47, 12)
            Me.Label6.TabIndex = 64
            Me.Label6.Text = "・手入力"
            '
            'tbInput
            '
            Me.tbInput.Location = New System.Drawing.Point(246, 110)
            Me.tbInput.MaxLength = 100
            Me.tbInput.Name = "tbInput"
            Me.tbInput.Size = New System.Drawing.Size(100, 19)
            Me.tbInput.TabIndex = 7
            '
            'btnGa
            '
            Me.btnGa.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
            Me.btnGa.Location = New System.Drawing.Point(12, 211)
            Me.btnGa.Name = "btnGa"
            Me.btnGa.Size = New System.Drawing.Size(32, 23)
            Me.btnGa.TabIndex = 8
            Me.btnGa.Text = "が"
            Me.btnGa.UseVisualStyleBackColor = False
            '
            'btnNi
            '
            Me.btnNi.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnNi.Enabled = False
            Me.btnNi.Location = New System.Drawing.Point(50, 211)
            Me.btnNi.Name = "btnNi"
            Me.btnNi.Size = New System.Drawing.Size(32, 23)
            Me.btnNi.TabIndex = 9
            Me.btnNi.Text = "に"
            Me.btnNi.UseVisualStyleBackColor = False
            '
            'btnNotokini
            '
            Me.btnNotokini.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnNotokini.Enabled = False
            Me.btnNotokini.Location = New System.Drawing.Point(126, 211)
            Me.btnNotokini.Name = "btnNotokini"
            Me.btnNotokini.Size = New System.Drawing.Size(53, 23)
            Me.btnNotokini.TabIndex = 11
            Me.btnNotokini.Text = "の時に"
            Me.btnNotokini.UseVisualStyleBackColor = False
            '
            'btnDe
            '
            Me.btnDe.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnDe.Enabled = False
            Me.btnDe.Location = New System.Drawing.Point(185, 211)
            Me.btnDe.Name = "btnDe"
            Me.btnDe.Size = New System.Drawing.Size(39, 23)
            Me.btnDe.TabIndex = 12
            Me.btnDe.Text = "で"
            Me.btnDe.UseVisualStyleBackColor = False
            '
            'btnAdd
            '
            Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnAdd.Location = New System.Drawing.Point(246, 211)
            Me.btnAdd.Name = "btnAdd"
            Me.btnAdd.Size = New System.Drawing.Size(75, 23)
            Me.btnAdd.TabIndex = 13
            Me.btnAdd.Text = "追加"
            Me.btnAdd.UseVisualStyleBackColor = False
            '
            'tbOutPut
            '
            Me.tbOutPut.BackColor = System.Drawing.SystemColors.Window
            Me.tbOutPut.Location = New System.Drawing.Point(12, 240)
            Me.tbOutPut.Multiline = True
            Me.tbOutPut.Name = "tbOutPut"
            Me.tbOutPut.ReadOnly = True
            Me.tbOutPut.Size = New System.Drawing.Size(309, 85)
            Me.tbOutPut.TabIndex = 71
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.rbAllCell)
            Me.GroupBox1.Controls.Add(Me.rbBlackCellOnly)
            Me.GroupBox1.Location = New System.Drawing.Point(12, 337)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(106, 68)
            Me.GroupBox1.TabIndex = 72
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "設定範囲"
            '
            'rbAllCell
            '
            Me.rbAllCell.AutoSize = True
            Me.rbAllCell.Location = New System.Drawing.Point(6, 40)
            Me.rbAllCell.Name = "rbAllCell"
            Me.rbAllCell.Size = New System.Drawing.Size(74, 16)
            Me.rbAllCell.TabIndex = 15
            Me.rbAllCell.TabStop = True
            Me.rbAllCell.Text = "全てのセル"
            Me.rbAllCell.UseVisualStyleBackColor = True
            '
            'rbBlackCellOnly
            '
            Me.rbBlackCellOnly.AutoSize = True
            Me.rbBlackCellOnly.Location = New System.Drawing.Point(6, 18)
            Me.rbBlackCellOnly.Name = "rbBlackCellOnly"
            Me.rbBlackCellOnly.Size = New System.Drawing.Size(88, 16)
            Me.rbBlackCellOnly.TabIndex = 14
            Me.rbBlackCellOnly.TabStop = True
            Me.rbBlackCellOnly.Text = "空白セルのみ"
            Me.rbBlackCellOnly.UseVisualStyleBackColor = True
            '
            'btnExecution
            '
            Me.btnExecution.BackColor = System.Drawing.Color.PaleGreen
            Me.btnExecution.Location = New System.Drawing.Point(118, 414)
            Me.btnExecution.Name = "btnExecution"
            Me.btnExecution.Size = New System.Drawing.Size(75, 23)
            Me.btnExecution.TabIndex = 16
            Me.btnExecution.Text = "実行"
            Me.btnExecution.UseVisualStyleBackColor = False
            '
            'btnClear
            '
            Me.btnClear.Location = New System.Drawing.Point(273, 414)
            Me.btnClear.Name = "btnClear"
            Me.btnClear.Size = New System.Drawing.Size(75, 23)
            Me.btnClear.TabIndex = 17
            Me.btnClear.Text = "クリア"
            Me.btnClear.UseVisualStyleBackColor = True
            '
            'btnWo
            '
            Me.btnWo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnWo.Enabled = False
            Me.btnWo.Location = New System.Drawing.Point(88, 211)
            Me.btnWo.Name = "btnWo"
            Me.btnWo.Size = New System.Drawing.Size(32, 23)
            Me.btnWo.TabIndex = 10
            Me.btnWo.Text = "を"
            Me.btnWo.UseVisualStyleBackColor = False
            '
            'PartsMultiSetting
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(360, 449)
            Me.Controls.Add(Me.btnWo)
            Me.Controls.Add(Me.btnClear)
            Me.Controls.Add(Me.btnExecution)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.tbOutPut)
            Me.Controls.Add(Me.btnAdd)
            Me.Controls.Add(Me.btnDe)
            Me.Controls.Add(Me.btnNotokini)
            Me.Controls.Add(Me.btnNi)
            Me.Controls.Add(Me.btnGa)
            Me.Controls.Add(Me.tbInput)
            Me.Controls.Add(Me.Label6)
            Me.Controls.Add(Me.btnDefault)
            Me.Controls.Add(Me.cmbDirections)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.cmbItemContents)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.cmbItemName)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.cmbSettingColumn)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.Panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "PartsMultiSetting"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents LblCurrPGId As System.Windows.Forms.Label
        Friend WithEvents btnBack As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents cmbSettingColumn As System.Windows.Forms.ComboBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents cmbItemName As System.Windows.Forms.ComboBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents cmbItemContents As System.Windows.Forms.ComboBox
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents cmbDirections As System.Windows.Forms.ComboBox
        Friend WithEvents btnDefault As System.Windows.Forms.Button
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents tbInput As System.Windows.Forms.TextBox
        Friend WithEvents btnGa As System.Windows.Forms.Button
        Friend WithEvents btnNi As System.Windows.Forms.Button
        Friend WithEvents btnNotokini As System.Windows.Forms.Button
        Friend WithEvents btnDe As System.Windows.Forms.Button
        Friend WithEvents btnAdd As System.Windows.Forms.Button
        Friend WithEvents tbOutPut As System.Windows.Forms.TextBox
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents rbAllCell As System.Windows.Forms.RadioButton
        Friend WithEvents rbBlackCellOnly As System.Windows.Forms.RadioButton
        Friend WithEvents btnExecution As System.Windows.Forms.Button
        Friend WithEvents btnClear As System.Windows.Forms.Button
        Friend WithEvents btnWo As System.Windows.Forms.Button
    End Class
End Namespace