<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm20KojiShireiExport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm20KojiShireiExport))
        Me.txtKoujiNo = New System.Windows.Forms.TextBox
        Me.btnExport = New System.Windows.Forms.Button
        Me.txtCarType = New System.Windows.Forms.TextBox
        Me.txtMokuteki = New System.Windows.Forms.TextBox
        Me.txtKenmei = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtStaffCode = New System.Windows.Forms.TextBox
        Me.txtTel = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbTantousha = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.LblCurrBukaName = New System.Windows.Forms.Label
        Me.LblDateNow = New System.Windows.Forms.Label
        Me.LblCurrUserId = New System.Windows.Forms.Label
        Me.LblTimeNow = New System.Windows.Forms.Label
        Me.LblCurrPGId = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnBack = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdoBtnShirei = New System.Windows.Forms.RadioButton
        Me.rdoBtnIrai = New System.Windows.Forms.RadioButton
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtKiji = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtKoujiNo
        '
        Me.txtKoujiNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKoujiNo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtKoujiNo.Location = New System.Drawing.Point(495, 157)
        Me.txtKoujiNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtKoujiNo.MaxLength = 7
        Me.txtKoujiNo.Multiline = True
        Me.txtKoujiNo.Name = "txtKoujiNo"
        Me.txtKoujiNo.Size = New System.Drawing.Size(202, 28)
        Me.txtKoujiNo.TabIndex = 9
        '
        'btnExport
        '
        Me.btnExport.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.btnExport.Location = New System.Drawing.Point(8, 219)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(84, 27)
        Me.btnExport.TabIndex = 11
        Me.btnExport.Text = "出力"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'txtCarType
        '
        Me.txtCarType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCarType.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtCarType.Location = New System.Drawing.Point(495, 130)
        Me.txtCarType.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCarType.MaxLength = 4
        Me.txtCarType.Multiline = True
        Me.txtCarType.Name = "txtCarType"
        Me.txtCarType.Size = New System.Drawing.Size(202, 28)
        Me.txtCarType.TabIndex = 7
        '
        'txtMokuteki
        '
        Me.txtMokuteki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMokuteki.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtMokuteki.Location = New System.Drawing.Point(91, 157)
        Me.txtMokuteki.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMokuteki.MaxLength = 256
        Me.txtMokuteki.Multiline = True
        Me.txtMokuteki.Name = "txtMokuteki"
        Me.txtMokuteki.Size = New System.Drawing.Size(321, 28)
        Me.txtMokuteki.TabIndex = 8
        '
        'txtKenmei
        '
        Me.txtKenmei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKenmei.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtKenmei.Location = New System.Drawing.Point(91, 130)
        Me.txtKenmei.Margin = New System.Windows.Forms.Padding(4)
        Me.txtKenmei.MaxLength = 256
        Me.txtKenmei.Multiline = True
        Me.txtKenmei.Name = "txtKenmei"
        Me.txtKenmei.Size = New System.Drawing.Size(321, 28)
        Me.txtKenmei.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.LightBlue
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Location = New System.Drawing.Point(411, 130)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 28)
        Me.Label8.TabIndex = 181
        Me.Label8.Text = "車種"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.LightBlue
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(7, 157)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 28)
        Me.Label7.TabIndex = 180
        Me.Label7.Text = "目的"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.LightBlue
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.Label6.Location = New System.Drawing.Point(7, 130)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 28)
        Me.Label6.TabIndex = 179
        Me.Label6.Text = "件名"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtStaffCode
        '
        Me.txtStaffCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStaffCode.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtStaffCode.Location = New System.Drawing.Point(411, 49)
        Me.txtStaffCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtStaffCode.MaxLength = 8
        Me.txtStaffCode.Name = "txtStaffCode"
        Me.txtStaffCode.Size = New System.Drawing.Size(150, 22)
        Me.txtStaffCode.TabIndex = 2
        '
        'txtTel
        '
        Me.txtTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTel.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtTel.Location = New System.Drawing.Point(91, 73)
        Me.txtTel.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTel.MaxLength = 4
        Me.txtTel.Name = "txtTel"
        Me.txtTel.Size = New System.Drawing.Size(150, 22)
        Me.txtTel.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(367, 52)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 15)
        Me.Label3.TabIndex = 177
        Me.Label3.Text = "職番："
        '
        'cmbTantousha
        '
        Me.cmbTantousha.DropDownHeight = 150
        Me.cmbTantousha.FormattingEnabled = True
        Me.cmbTantousha.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.cmbTantousha.IntegralHeight = False
        Me.cmbTantousha.Location = New System.Drawing.Point(91, 49)
        Me.cmbTantousha.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTantousha.MaxLength = 20
        Me.cmbTantousha.Name = "cmbTantousha"
        Me.cmbTantousha.Size = New System.Drawing.Size(250, 23)
        Me.cmbTantousha.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 77)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 15)
        Me.Label2.TabIndex = 176
        Me.Label2.Text = "TEL："
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 52)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 15)
        Me.Label1.TabIndex = 175
        Me.Label1.Text = "担当者名："
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.BackColor = System.Drawing.Color.Blue
        Me.Panel2.Controls.Add(Me.LblCurrBukaName)
        Me.Panel2.Controls.Add(Me.LblDateNow)
        Me.Panel2.Controls.Add(Me.LblCurrUserId)
        Me.Panel2.Controls.Add(Me.LblTimeNow)
        Me.Panel2.Controls.Add(Me.LblCurrPGId)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(704, 32)
        Me.Panel2.TabIndex = 174
        '
        'LblCurrBukaName
        '
        Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
        Me.LblCurrBukaName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblCurrBukaName.Location = New System.Drawing.Point(492, 19)
        Me.LblCurrBukaName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblCurrBukaName.Name = "LblCurrBukaName"
        Me.LblCurrBukaName.Size = New System.Drawing.Size(134, 13)
        Me.LblCurrBukaName.TabIndex = 68
        Me.LblCurrBukaName.Text = "(部課：SKE1         )"
        '
        'LblDateNow
        '
        Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDateNow.AutoSize = True
        Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.LblDateNow.ForeColor = System.Drawing.Color.White
        Me.LblDateNow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblDateNow.Location = New System.Drawing.Point(614, 5)
        Me.LblDateNow.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblDateNow.Name = "LblDateNow"
        Me.LblDateNow.Size = New System.Drawing.Size(79, 12)
        Me.LblDateNow.TabIndex = 66
        Me.LblDateNow.Text = "YYYY/MM/DD"
        '
        'LblCurrUserId
        '
        Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
        Me.LblCurrUserId.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblCurrUserId.Location = New System.Drawing.Point(492, 5)
        Me.LblCurrUserId.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblCurrUserId.Name = "LblCurrUserId"
        Me.LblCurrUserId.Size = New System.Drawing.Size(134, 13)
        Me.LblCurrUserId.TabIndex = 67
        Me.LblCurrUserId.Text = "(ID    ：ABCDEFGH)"
        '
        'LblTimeNow
        '
        Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTimeNow.AutoSize = True
        Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.LblTimeNow.ForeColor = System.Drawing.Color.White
        Me.LblTimeNow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblTimeNow.Location = New System.Drawing.Point(634, 19)
        Me.LblTimeNow.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblTimeNow.Name = "LblTimeNow"
        Me.LblTimeNow.Size = New System.Drawing.Size(59, 12)
        Me.LblTimeNow.TabIndex = 65
        Me.LblTimeNow.Text = "HH:MM:DD"
        '
        'LblCurrPGId
        '
        Me.LblCurrPGId.AutoSize = True
        Me.LblCurrPGId.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.LblCurrPGId.ForeColor = System.Drawing.Color.White
        Me.LblCurrPGId.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblCurrPGId.Location = New System.Drawing.Point(4, 5)
        Me.LblCurrPGId.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblCurrPGId.Name = "LblCurrPGId"
        Me.LblCurrPGId.Size = New System.Drawing.Size(99, 12)
        Me.LblCurrPGId.TabIndex = 61
        Me.LblCurrPGId.Text = "PG-ID：XXXXXXXX"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.Yellow
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(125, 3)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(389, 28)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "担当者の選択及び補足情報の入力画面"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBack.Location = New System.Drawing.Point(613, 46)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(84, 27)
        Me.btnBack.TabIndex = 12
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 15)
        Me.Label5.TabIndex = 182
        Me.Label5.Text = "出力選択："
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.rdoBtnShirei)
        Me.Panel1.Controls.Add(Me.rdoBtnIrai)
        Me.Panel1.Location = New System.Drawing.Point(91, 103)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(321, 28)
        Me.Panel1.TabIndex = 183
        '
        'rdoBtnShirei
        '
        Me.rdoBtnShirei.AutoSize = True
        Me.rdoBtnShirei.Location = New System.Drawing.Point(151, 3)
        Me.rdoBtnShirei.Name = "rdoBtnShirei"
        Me.rdoBtnShirei.Size = New System.Drawing.Size(85, 19)
        Me.rdoBtnShirei.TabIndex = 5
        Me.rdoBtnShirei.TabStop = True
        Me.rdoBtnShirei.Text = "指令部署"
        Me.rdoBtnShirei.UseVisualStyleBackColor = True
        '
        'rdoBtnIrai
        '
        Me.rdoBtnIrai.AutoSize = True
        Me.rdoBtnIrai.Location = New System.Drawing.Point(4, 3)
        Me.rdoBtnIrai.Name = "rdoBtnIrai"
        Me.rdoBtnIrai.Size = New System.Drawing.Size(85, 19)
        Me.rdoBtnIrai.TabIndex = 4
        Me.rdoBtnIrai.TabStop = True
        Me.rdoBtnIrai.Text = "依頼部署"
        Me.rdoBtnIrai.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.LightBlue
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.Label9.Location = New System.Drawing.Point(7, 184)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 28)
        Me.Label9.TabIndex = 179
        Me.Label9.Text = "記事"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtKiji
        '
        Me.txtKiji.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKiji.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtKiji.Location = New System.Drawing.Point(91, 184)
        Me.txtKiji.Margin = New System.Windows.Forms.Padding(4)
        Me.txtKiji.MaxLength = 256
        Me.txtKiji.Multiline = True
        Me.txtKiji.Name = "txtKiji"
        Me.txtKiji.Size = New System.Drawing.Size(606, 28)
        Me.txtKiji.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.LightBlue
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Location = New System.Drawing.Point(411, 157)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(85, 28)
        Me.Label10.TabIndex = 181
        Me.Label10.Text = "工事指令№"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Frm20KojiShireiExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(704, 254)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.txtKoujiNo)
        Me.Controls.Add(Me.txtCarType)
        Me.Controls.Add(Me.txtMokuteki)
        Me.Controls.Add(Me.txtKiji)
        Me.Controls.Add(Me.txtKenmei)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtStaffCode)
        Me.Controls.Add(Me.txtTel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbTantousha)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnBack)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Frm20KojiShireiExport"
        Me.Text = "新試作手配システム Ver 1.00"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtKoujiNo As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents txtCarType As System.Windows.Forms.TextBox
    Friend WithEvents txtMokuteki As System.Windows.Forms.TextBox
    Friend WithEvents txtKenmei As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtStaffCode As System.Windows.Forms.TextBox
    Friend WithEvents txtTel As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbTantousha As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents LblCurrPGId As System.Windows.Forms.Label
    Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
    Friend WithEvents LblDateNow As System.Windows.Forms.Label
    Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
    Friend WithEvents LblTimeNow As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoBtnShirei As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBtnIrai As System.Windows.Forms.RadioButton
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtKiji As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
