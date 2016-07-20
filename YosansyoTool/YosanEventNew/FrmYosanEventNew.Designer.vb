Namespace YosanEventNew

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FrmYosanEventNew
        Inherits System.Windows.Forms.Form

        'Form 重写 Dispose，以清理组件列表。
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

        'Windows 窗体设计器所必需的
        Private components As System.ComponentModel.IContainer

        '注意: 以下过程是 Windows 窗体设计器所必需的
        '可以使用 Windows 窗体设计器修改它。
        '不要使用代码编辑器修改它。
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmYosanEventNew))
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.LblLoginBukaName = New System.Windows.Forms.Label
            Me.LblDateNow = New System.Windows.Forms.Label
            Me.LblLoginUserId = New System.Windows.Forms.Label
            Me.LblTimeNow = New System.Windows.Forms.Label
            Me.LblCurrPGId = New System.Windows.Forms.Label
            Me.LblCurrPGName = New System.Windows.Forms.Label
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.LOGO = New System.Windows.Forms.Label
            Me.btnBack = New System.Windows.Forms.Button
            Me.btnRegister = New System.Windows.Forms.Button
            Me.Label1 = New System.Windows.Forms.Label
            Me.cmbKaihatsuFugo = New System.Windows.Forms.ComboBox
            Me.Label2 = New System.Windows.Forms.Label
            Me.Label3 = New System.Windows.Forms.Label
            Me.cmbEvent = New System.Windows.Forms.ComboBox
            Me.Label4 = New System.Windows.Forms.Label
            Me.Label5 = New System.Windows.Forms.Label
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.Label8 = New System.Windows.Forms.Label
            Me.Label10 = New System.Windows.Forms.Label
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.Label11 = New System.Windows.Forms.Label
            Me.cmbKubun = New System.Windows.Forms.ComboBox
            Me.Label12 = New System.Windows.Forms.Label
            Me.txtEventName = New System.Windows.Forms.TextBox
            Me.txtYosanCode = New System.Windows.Forms.TextBox
            Me.txtMainHenkoGaiyo = New System.Windows.Forms.TextBox
            Me.txtTsukurikataSeisakujyoken = New System.Windows.Forms.TextBox
            Me.txtSonota = New System.Windows.Forms.TextBox
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.Panel3.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.LblLoginBukaName)
            Me.Panel1.Controls.Add(Me.LblDateNow)
            Me.Panel1.Controls.Add(Me.LblLoginUserId)
            Me.Panel1.Controls.Add(Me.LblTimeNow)
            Me.Panel1.Controls.Add(Me.LblCurrPGId)
            Me.Panel1.Controls.Add(Me.LblCurrPGName)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(684, 37)
            Me.Panel1.TabIndex = 88
            '
            'LblLoginBukaName
            '
            Me.LblLoginBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblLoginBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblLoginBukaName.ForeColor = System.Drawing.Color.White
            Me.LblLoginBukaName.Location = New System.Drawing.Point(446, 21)
            Me.LblLoginBukaName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.LblLoginBukaName.Name = "LblLoginBukaName"
            Me.LblLoginBukaName.Size = New System.Drawing.Size(173, 16)
            Me.LblLoginBukaName.TabIndex = 59
            Me.LblLoginBukaName.Text = "(部課：SKE1         )"
            '
            'LblDateNow
            '
            Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblDateNow.AutoSize = True
            Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblDateNow.ForeColor = System.Drawing.Color.White
            Me.LblDateNow.Location = New System.Drawing.Point(592, 5)
            Me.LblDateNow.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.LblDateNow.Name = "LblDateNow"
            Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
            Me.LblDateNow.TabIndex = 56
            Me.LblDateNow.Text = "YYYY/MM/DD"
            '
            'LblLoginUserId
            '
            Me.LblLoginUserId.AccessibleRole = System.Windows.Forms.AccessibleRole.None
            Me.LblLoginUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblLoginUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblLoginUserId.ForeColor = System.Drawing.Color.White
            Me.LblLoginUserId.Location = New System.Drawing.Point(446, 5)
            Me.LblLoginUserId.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.LblLoginUserId.Name = "LblLoginUserId"
            Me.LblLoginUserId.Size = New System.Drawing.Size(173, 16)
            Me.LblLoginUserId.TabIndex = 58
            Me.LblLoginUserId.Text = "(ID    ：ABCDEFGH)"
            '
            'LblTimeNow
            '
            Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblTimeNow.AutoSize = True
            Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblTimeNow.ForeColor = System.Drawing.Color.White
            Me.LblTimeNow.Location = New System.Drawing.Point(616, 21)
            Me.LblTimeNow.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.LblTimeNow.Name = "LblTimeNow"
            Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
            Me.LblTimeNow.TabIndex = 55
            Me.LblTimeNow.Text = "HH:MM:DD"
            '
            'LblCurrPGId
            '
            Me.LblCurrPGId.AutoSize = True
            Me.LblCurrPGId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrPGId.ForeColor = System.Drawing.Color.White
            Me.LblCurrPGId.Location = New System.Drawing.Point(5, 5)
            Me.LblCurrPGId.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.LblCurrPGId.Name = "LblCurrPGId"
            Me.LblCurrPGId.Size = New System.Drawing.Size(113, 13)
            Me.LblCurrPGId.TabIndex = 54
            Me.LblCurrPGId.Text = "PG-ID：XXXXXXXX"
            '
            'LblCurrPGName
            '
            Me.LblCurrPGName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrPGName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrPGName.ForeColor = System.Drawing.Color.Yellow
            Me.LblCurrPGName.Location = New System.Drawing.Point(211, 5)
            Me.LblCurrPGName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.LblCurrPGName.Name = "LblCurrPGName"
            Me.LblCurrPGName.Size = New System.Drawing.Size(227, 27)
            Me.LblCurrPGName.TabIndex = 54
            Me.LblCurrPGName.Text = "予算書イベント新規作成"
            Me.LblCurrPGName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Panel2
            '
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.LOGO)
            Me.Panel2.Controls.Add(Me.btnBack)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 37)
            Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(684, 32)
            Me.Panel2.TabIndex = 94
            '
            'LOGO
            '
            Me.LOGO.BackColor = System.Drawing.Color.White
            Me.LOGO.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LOGO.ForeColor = System.Drawing.Color.Yellow
            Me.LOGO.Image = Global.YosansyoTool.My.Resources.Resources.ロゴ
            Me.LOGO.Location = New System.Drawing.Point(239, 2)
            Me.LOGO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.LOGO.Name = "LOGO"
            Me.LOGO.Size = New System.Drawing.Size(187, 32)
            Me.LOGO.TabIndex = 82
            Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnBack
            '
            Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBack.Location = New System.Drawing.Point(614, 2)
            Me.btnBack.Margin = New System.Windows.Forms.Padding(4)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(64, 24)
            Me.btnBack.TabIndex = 10
            Me.btnBack.TabStop = False
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = True
            '
            'btnRegister
            '
            Me.btnRegister.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnRegister.BackColor = System.Drawing.Color.PaleGreen
            Me.btnRegister.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnRegister.Location = New System.Drawing.Point(605, 72)
            Me.btnRegister.Margin = New System.Windows.Forms.Padding(4)
            Me.btnRegister.Name = "btnRegister"
            Me.btnRegister.Size = New System.Drawing.Size(79, 24)
            Me.btnRegister.TabIndex = 9
            Me.btnRegister.Text = "登録"
            Me.btnRegister.UseVisualStyleBackColor = False
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.Location = New System.Drawing.Point(17, 126)
            Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(75, 15)
            Me.Label1.TabIndex = 95
            Me.Label1.Text = "開発符号："
            '
            'cmbKaihatsuFugo
            '
            Me.cmbKaihatsuFugo.DropDownHeight = 150
            Me.cmbKaihatsuFugo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbKaihatsuFugo.FormattingEnabled = True
            Me.cmbKaihatsuFugo.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.cmbKaihatsuFugo.IntegralHeight = False
            Me.cmbKaihatsuFugo.Location = New System.Drawing.Point(136, 122)
            Me.cmbKaihatsuFugo.Margin = New System.Windows.Forms.Padding(4)
            Me.cmbKaihatsuFugo.MaxDropDownItems = 10
            Me.cmbKaihatsuFugo.MaxLength = 4
            Me.cmbKaihatsuFugo.Name = "cmbKaihatsuFugo"
            Me.cmbKaihatsuFugo.Size = New System.Drawing.Size(97, 23)
            Me.cmbKaihatsuFugo.TabIndex = 2
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label2.Location = New System.Drawing.Point(17, 162)
            Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(60, 15)
            Me.Label2.TabIndex = 97
            Me.Label2.Text = "イベント："
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label3.Location = New System.Drawing.Point(17, 194)
            Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(75, 15)
            Me.Label3.TabIndex = 98
            Me.Label3.Text = "イベント名："
            '
            'cmbEvent
            '
            Me.cmbEvent.DropDownHeight = 150
            Me.cmbEvent.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbEvent.FormattingEnabled = True
            Me.cmbEvent.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.cmbEvent.IntegralHeight = False
            Me.cmbEvent.Location = New System.Drawing.Point(136, 158)
            Me.cmbEvent.Margin = New System.Windows.Forms.Padding(4)
            Me.cmbEvent.MaxLength = 18
            Me.cmbEvent.Name = "cmbEvent"
            Me.cmbEvent.Size = New System.Drawing.Size(192, 23)
            Me.cmbEvent.TabIndex = 3
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label4.Location = New System.Drawing.Point(156, 334)
            Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(53, 15)
            Me.Label4.TabIndex = 104
            Me.Label4.Text = "その他："
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label5.Location = New System.Drawing.Point(68, 299)
            Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(141, 15)
            Me.Label5.TabIndex = 103
            Me.Label5.Text = "造り方及び製作条件："
            '
            'Panel3
            '
            Me.Panel3.AutoSize = True
            Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel3.BackColor = System.Drawing.SystemColors.Control
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Controls.Add(Me.Label8)
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel3.Location = New System.Drawing.Point(0, 371)
            Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(684, 31)
            Me.Panel3.TabIndex = 110
            '
            'Label8
            '
            Me.Label8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label8.ForeColor = System.Drawing.Color.Red
            Me.Label8.Location = New System.Drawing.Point(-1, 1)
            Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(684, 28)
            Me.Label8.TabIndex = 55
            Me.Label8.Text = "予算書イベントを入力後、登録ボタンをクリックしてください。"
            Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label10.Location = New System.Drawing.Point(106, 264)
            Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(103, 15)
            Me.Label10.TabIndex = 114
            Me.Label10.Text = "主な変更概要："
            '
            'Timer1
            '
            Me.Timer1.Enabled = True
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label11.Location = New System.Drawing.Point(131, 229)
            Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(78, 15)
            Me.Label11.TabIndex = 98
            Me.Label11.Text = "予算コード："
            '
            'cmbKubun
            '
            Me.cmbKubun.DropDownHeight = 150
            Me.cmbKubun.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbKubun.FormattingEnabled = True
            Me.cmbKubun.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.cmbKubun.IntegralHeight = False
            Me.cmbKubun.Items.AddRange(New Object() {"本体", "先開", "その他"})
            Me.cmbKubun.Location = New System.Drawing.Point(135, 87)
            Me.cmbKubun.Margin = New System.Windows.Forms.Padding(4)
            Me.cmbKubun.MaxDropDownItems = 10
            Me.cmbKubun.MaxLength = 10
            Me.cmbKubun.Name = "cmbKubun"
            Me.cmbKubun.Size = New System.Drawing.Size(97, 23)
            Me.cmbKubun.TabIndex = 1
            '
            'Label12
            '
            Me.Label12.AutoSize = True
            Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label12.Location = New System.Drawing.Point(16, 91)
            Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(45, 15)
            Me.Label12.TabIndex = 116
            Me.Label12.Text = "区分："
            '
            'txtEventName
            '
            Me.txtEventName.Location = New System.Drawing.Point(136, 194)
            Me.txtEventName.Margin = New System.Windows.Forms.Padding(4)
            Me.txtEventName.MaxLength = 100
            Me.txtEventName.Name = "txtEventName"
            Me.txtEventName.Size = New System.Drawing.Size(400, 22)
            Me.txtEventName.TabIndex = 4
            '
            'txtYosanCode
            '
            Me.txtYosanCode.Location = New System.Drawing.Point(217, 226)
            Me.txtYosanCode.Margin = New System.Windows.Forms.Padding(4)
            Me.txtYosanCode.MaxLength = 2
            Me.txtYosanCode.Name = "txtYosanCode"
            Me.txtYosanCode.Size = New System.Drawing.Size(85, 22)
            Me.txtYosanCode.TabIndex = 5
            '
            'txtMainHenkoGaiyo
            '
            Me.txtMainHenkoGaiyo.Location = New System.Drawing.Point(217, 261)
            Me.txtMainHenkoGaiyo.Margin = New System.Windows.Forms.Padding(4)
            Me.txtMainHenkoGaiyo.MaxLength = 256
            Me.txtMainHenkoGaiyo.Name = "txtMainHenkoGaiyo"
            Me.txtMainHenkoGaiyo.Size = New System.Drawing.Size(420, 22)
            Me.txtMainHenkoGaiyo.TabIndex = 6
            '
            'txtTsukurikataSeisakujyoken
            '
            Me.txtTsukurikataSeisakujyoken.Location = New System.Drawing.Point(217, 296)
            Me.txtTsukurikataSeisakujyoken.Margin = New System.Windows.Forms.Padding(4)
            Me.txtTsukurikataSeisakujyoken.MaxLength = 256
            Me.txtTsukurikataSeisakujyoken.Name = "txtTsukurikataSeisakujyoken"
            Me.txtTsukurikataSeisakujyoken.Size = New System.Drawing.Size(420, 22)
            Me.txtTsukurikataSeisakujyoken.TabIndex = 7
            '
            'txtSonota
            '
            Me.txtSonota.Location = New System.Drawing.Point(217, 327)
            Me.txtSonota.Margin = New System.Windows.Forms.Padding(4)
            Me.txtSonota.MaxLength = 256
            Me.txtSonota.Name = "txtSonota"
            Me.txtSonota.Size = New System.Drawing.Size(420, 22)
            Me.txtSonota.TabIndex = 8
            '
            'FrmYosanEventNew
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(684, 402)
            Me.Controls.Add(Me.txtYosanCode)
            Me.Controls.Add(Me.txtSonota)
            Me.Controls.Add(Me.txtTsukurikataSeisakujyoken)
            Me.Controls.Add(Me.txtMainHenkoGaiyo)
            Me.Controls.Add(Me.txtEventName)
            Me.Controls.Add(Me.cmbKubun)
            Me.Controls.Add(Me.Label12)
            Me.Controls.Add(Me.Label10)
            Me.Controls.Add(Me.btnRegister)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.cmbEvent)
            Me.Controls.Add(Me.Label11)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.cmbKaihatsuFugo)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Margin = New System.Windows.Forms.Padding(4)
            Me.MaximizeBox = False
            Me.Name = "FrmYosanEventNew"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            Me.Panel3.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents LblLoginBukaName As System.Windows.Forms.Label
        Friend WithEvents LblDateNow As System.Windows.Forms.Label
        Friend WithEvents LblLoginUserId As System.Windows.Forms.Label
        Friend WithEvents LblTimeNow As System.Windows.Forms.Label
        Friend WithEvents LblCurrPGId As System.Windows.Forms.Label
        Friend WithEvents LblCurrPGName As System.Windows.Forms.Label
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents LOGO As System.Windows.Forms.Label
        Friend WithEvents btnBack As System.Windows.Forms.Button
        Friend WithEvents btnRegister As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents cmbKaihatsuFugo As System.Windows.Forms.ComboBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents cmbEvent As System.Windows.Forms.ComboBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents Label11 As System.Windows.Forms.Label
        Friend WithEvents cmbKubun As System.Windows.Forms.ComboBox
        Friend WithEvents Label12 As System.Windows.Forms.Label
        Friend WithEvents txtEventName As System.Windows.Forms.TextBox
        Friend WithEvents txtYosanCode As System.Windows.Forms.TextBox
        Friend WithEvents txtMainHenkoGaiyo As System.Windows.Forms.TextBox
        Friend WithEvents txtTsukurikataSeisakujyoken As System.Windows.Forms.TextBox
        Friend WithEvents txtSonota As System.Windows.Forms.TextBox
    End Class

End Namespace
