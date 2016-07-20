<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGousyabetsuShiyousyo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGousyabetsuShiyousyo))
        Me.grpSelector = New System.Windows.Forms.GroupBox
        Me.radioGousya = New System.Windows.Forms.RadioButton
        Me.radioGroup = New System.Windows.Forms.RadioButton
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.LOGO = New System.Windows.Forms.Label
        Me.btnBACK = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblCurrBukaName = New System.Windows.Forms.Label
        Me.LblCurrUserId = New System.Windows.Forms.Label
        Me.LblDateNow = New System.Windows.Forms.Label
        Me.LblCurrPGId = New System.Windows.Forms.Label
        Me.LblTimeNow = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.cmbGroupNo = New System.Windows.Forms.ComboBox
        Me.btnExcelOutput = New System.Windows.Forms.Button
        Me.clistGousya = New System.Windows.Forms.CheckedListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbEvent = New System.Windows.Forms.ComboBox
        Me.cmbTemplate = New System.Windows.Forms.ComboBox
        Me.lblTemplate = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.grpSelector.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpSelector
        '
        Me.grpSelector.Controls.Add(Me.radioGousya)
        Me.grpSelector.Controls.Add(Me.radioGroup)
        Me.grpSelector.Location = New System.Drawing.Point(14, 112)
        Me.grpSelector.Name = "grpSelector"
        Me.grpSelector.Size = New System.Drawing.Size(465, 41)
        Me.grpSelector.TabIndex = 1
        Me.grpSelector.TabStop = False
        '
        'radioGousya
        '
        Me.radioGousya.AutoSize = True
        Me.radioGousya.Location = New System.Drawing.Point(183, 16)
        Me.radioGousya.Name = "radioGousya"
        Me.radioGousya.Size = New System.Drawing.Size(47, 16)
        Me.radioGousya.TabIndex = 2
        Me.radioGousya.Text = "号車"
        Me.radioGousya.UseVisualStyleBackColor = True
        '
        'radioGroup
        '
        Me.radioGroup.AutoSize = True
        Me.radioGroup.Checked = True
        Me.radioGroup.Location = New System.Drawing.Point(6, 16)
        Me.radioGroup.Name = "radioGroup"
        Me.radioGroup.Size = New System.Drawing.Size(73, 16)
        Me.radioGroup.TabIndex = 1
        Me.radioGroup.TabStop = True
        Me.radioGroup.Text = "グループ№"
        Me.radioGroup.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.Controls.Add(Me.LOGO)
        Me.Panel4.Controls.Add(Me.btnBACK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 32)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(502, 30)
        Me.Panel4.TabIndex = 86
        '
        'LOGO
        '
        Me.LOGO.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LOGO.BackColor = System.Drawing.Color.White
        Me.LOGO.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LOGO.ForeColor = System.Drawing.Color.Yellow
        Me.LOGO.Image = Global.EventSakusei.My.Resources.Resources.ロゴ
        Me.LOGO.Location = New System.Drawing.Point(161, 0)
        Me.LOGO.Name = "LOGO"
        Me.LOGO.Size = New System.Drawing.Size(167, 26)
        Me.LOGO.TabIndex = 83
        Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBACK
        '
        Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBACK.Location = New System.Drawing.Point(427, 3)
        Me.btnBACK.Name = "btnBACK"
        Me.btnBACK.Size = New System.Drawing.Size(71, 24)
        Me.btnBACK.TabIndex = 9
        Me.btnBACK.Text = "戻る"
        Me.btnBACK.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.BackColor = System.Drawing.Color.Blue
        Me.Panel1.Controls.Add(Me.LblCurrBukaName)
        Me.Panel1.Controls.Add(Me.LblCurrUserId)
        Me.Panel1.Controls.Add(Me.LblDateNow)
        Me.Panel1.Controls.Add(Me.LblCurrPGId)
        Me.Panel1.Controls.Add(Me.LblTimeNow)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(502, 32)
        Me.Panel1.TabIndex = 85
        '
        'LblCurrBukaName
        '
        Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
        Me.LblCurrBukaName.Location = New System.Drawing.Point(276, 14)
        Me.LblCurrBukaName.Name = "LblCurrBukaName"
        Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrBukaName.TabIndex = 77
        Me.LblCurrBukaName.Text = "部課"
        '
        'LblCurrUserId
        '
        Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
        Me.LblCurrUserId.Location = New System.Drawing.Point(276, 1)
        Me.LblCurrUserId.Name = "LblCurrUserId"
        Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrUserId.TabIndex = 76
        Me.LblCurrUserId.Text = "ID"
        '
        'LblDateNow
        '
        Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDateNow.AutoSize = True
        Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDateNow.ForeColor = System.Drawing.Color.White
        Me.LblDateNow.Location = New System.Drawing.Point(413, 1)
        Me.LblDateNow.Name = "LblDateNow"
        Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
        Me.LblDateNow.TabIndex = 60
        Me.LblDateNow.Text = "YYYY/MM/DD"
        '
        'LblCurrPGId
        '
        Me.LblCurrPGId.AutoSize = True
        Me.LblCurrPGId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrPGId.ForeColor = System.Drawing.Color.White
        Me.LblCurrPGId.Location = New System.Drawing.Point(3, 1)
        Me.LblCurrPGId.Name = "LblCurrPGId"
        Me.LblCurrPGId.Size = New System.Drawing.Size(113, 13)
        Me.LblCurrPGId.TabIndex = 59
        Me.LblCurrPGId.Text = "PG-ID：XXXXXXXX"
        '
        'LblTimeNow
        '
        Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTimeNow.AutoSize = True
        Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTimeNow.ForeColor = System.Drawing.Color.White
        Me.LblTimeNow.Location = New System.Drawing.Point(437, 14)
        Me.LblTimeNow.Name = "LblTimeNow"
        Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
        Me.LblTimeNow.TabIndex = 59
        Me.LblTimeNow.Text = "HH:MM:DD"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Yellow
        Me.Label4.Location = New System.Drawing.Point(122, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(160, 31)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "号車別仕様書　作成"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel6
        '
        Me.Panel6.AutoSize = True
        Me.Panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel6.BackColor = System.Drawing.SystemColors.Control
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.Label10)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 336)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(502, 25)
        Me.Panel6.TabIndex = 88
        '
        'Label10
        '
        Me.Label10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(-1, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(499, 22)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "検索条件を入力して「検索」ボタンをクリックして下さい。"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.AutoSize = True
        Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 361)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(502, 2)
        Me.Panel3.TabIndex = 87
        '
        'cmbGroupNo
        '
        Me.cmbGroupNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGroupNo.FormattingEnabled = True
        Me.cmbGroupNo.Location = New System.Drawing.Point(81, 168)
        Me.cmbGroupNo.Name = "cmbGroupNo"
        Me.cmbGroupNo.Size = New System.Drawing.Size(62, 20)
        Me.cmbGroupNo.Sorted = True
        Me.cmbGroupNo.TabIndex = 89
        '
        'btnExcelOutput
        '
        Me.btnExcelOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcelOutput.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnExcelOutput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExcelOutput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcelOutput.Location = New System.Drawing.Point(416, 84)
        Me.btnExcelOutput.Name = "btnExcelOutput"
        Me.btnExcelOutput.Size = New System.Drawing.Size(84, 24)
        Me.btnExcelOutput.TabIndex = 95
        Me.btnExcelOutput.Text = "Excel出力"
        Me.btnExcelOutput.UseVisualStyleBackColor = False
        '
        'clistGousya
        '
        Me.clistGousya.CheckOnClick = True
        Me.clistGousya.FormattingEnabled = True
        Me.clistGousya.Location = New System.Drawing.Point(197, 168)
        Me.clistGousya.Name = "clistGousya"
        Me.clistGousya.Size = New System.Drawing.Size(266, 116)
        Me.clistGousya.TabIndex = 91
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 171)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 12)
        Me.Label1.TabIndex = 92
        Me.Label1.Text = "グループ№"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(162, 171)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 94
        Me.Label5.Text = "号車"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 97
        Me.Label3.Text = "イベント名"
        '
        'cmbEvent
        '
        Me.cmbEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEvent.FormattingEnabled = True
        Me.cmbEvent.Location = New System.Drawing.Point(81, 86)
        Me.cmbEvent.Name = "cmbEvent"
        Me.cmbEvent.Size = New System.Drawing.Size(311, 20)
        Me.cmbEvent.Sorted = True
        Me.cmbEvent.TabIndex = 96
        '
        'cmbTemplate
        '
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.FormattingEnabled = True
        Me.cmbTemplate.Location = New System.Drawing.Point(81, 295)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(398, 20)
        Me.cmbTemplate.Sorted = True
        Me.cmbTemplate.TabIndex = 96
        '
        'lblTemplate
        '
        Me.lblTemplate.AutoSize = True
        Me.lblTemplate.Location = New System.Drawing.Point(12, 298)
        Me.lblTemplate.Name = "lblTemplate"
        Me.lblTemplate.Size = New System.Drawing.Size(59, 12)
        Me.lblTemplate.TabIndex = 97
        Me.lblTemplate.Text = "テンプレート"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'frmGousyabetsuShiyousyo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 363)
        Me.Controls.Add(Me.lblTemplate)
        Me.Controls.Add(Me.cmbTemplate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbEvent)
        Me.Controls.Add(Me.btnExcelOutput)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.clistGousya)
        Me.Controls.Add(Me.cmbGroupNo)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grpSelector)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmGousyabetsuShiyousyo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmGousyabetsuShiyousyo"
        Me.grpSelector.ResumeLayout(False)
        Me.grpSelector.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpSelector As System.Windows.Forms.GroupBox
    Friend WithEvents radioGousya As System.Windows.Forms.RadioButton
    Friend WithEvents radioGroup As System.Windows.Forms.RadioButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents LOGO As System.Windows.Forms.Label
    Friend WithEvents btnBACK As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
    Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
    Friend WithEvents LblDateNow As System.Windows.Forms.Label
    Friend WithEvents LblCurrPGId As System.Windows.Forms.Label
    Friend WithEvents LblTimeNow As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents cmbGroupNo As System.Windows.Forms.ComboBox
    Friend WithEvents btnExcelOutput As System.Windows.Forms.Button
    Friend WithEvents clistGousya As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbEvent As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents lblTemplate As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
