Namespace YosanSetteiBuhinSakusei

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmDispYosanSetteiBuhinSakusei
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDispYosanSetteiBuhinSakusei))
            Me.Button1 = New System.Windows.Forms.Button
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.LOGO = New System.Windows.Forms.Label
            Me.btnBack = New System.Windows.Forms.Button
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.Label4 = New System.Windows.Forms.Label
            Me.LblBukaName = New System.Windows.Forms.Label
            Me.LblDateNow = New System.Windows.Forms.Label
            Me.LblUserId = New System.Windows.Forms.Label
            Me.LblTimeNow = New System.Windows.Forms.Label
            Me.LblPG_ID = New System.Windows.Forms.Label
            Me.btnADD = New System.Windows.Forms.Button
            Me.cmbGroupNo = New System.Windows.Forms.ComboBox
            Me.Label7 = New System.Windows.Forms.Label
            Me.Label10 = New System.Windows.Forms.Label
            Me.txtKoujiKbn = New System.Windows.Forms.TextBox
            Me.cmbSeihinKbn = New System.Windows.Forms.ComboBox
            Me.Label8 = New System.Windows.Forms.Label
            Me.Label9 = New System.Windows.Forms.Label
            Me.txtKoujiShireiNo = New System.Windows.Forms.TextBox
            Me.Label11 = New System.Windows.Forms.Label
            Me.txtKoujiNo = New System.Windows.Forms.TextBox
            Me.Label12 = New System.Windows.Forms.Label
            Me.txtEventName = New System.Windows.Forms.TextBox
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.Label15 = New System.Windows.Forms.Label
            Me.Label14 = New System.Windows.Forms.Label
            Me.Panel6 = New System.Windows.Forms.Panel
            Me.RbtnSyukeiTenkaiShinai = New System.Windows.Forms.RadioButton
            Me.RbtnSyukeiTenkaiSuru = New System.Windows.Forms.RadioButton
            Me.Panel5 = New System.Windows.Forms.Panel
            Me.RbtnHikakuOrikomiShinai = New System.Windows.Forms.RadioButton
            Me.RbtnHikakuOrikomiSuru = New System.Windows.Forms.RadioButton
            Me.Label13 = New System.Windows.Forms.Label
            Me.Panel4 = New System.Windows.Forms.Panel
            Me.RbtnJIkyuhinShinai = New System.Windows.Forms.RadioButton
            Me.RbtnJIkyuhinSuru = New System.Windows.Forms.RadioButton
            Me.Panel7 = New System.Windows.Forms.Panel
            Me.Label16 = New System.Windows.Forms.Label
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.Panel2.SuspendLayout()
            Me.Panel1.SuspendLayout()
            Me.Panel3.SuspendLayout()
            Me.Panel6.SuspendLayout()
            Me.Panel5.SuspendLayout()
            Me.Panel4.SuspendLayout()
            Me.Panel7.SuspendLayout()
            Me.SuspendLayout()
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(0, 0)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(75, 23)
            Me.Button1.TabIndex = 0
            '
            'Panel2
            '
            Me.Panel2.AutoSize = True
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.LOGO)
            Me.Panel2.Controls.Add(Me.btnBack)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 32)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(534, 32)
            Me.Panel2.TabIndex = 59
            '
            'LOGO
            '
            Me.LOGO.BackColor = System.Drawing.Color.White
            Me.LOGO.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LOGO.ForeColor = System.Drawing.Color.Yellow
            Me.LOGO.Image = Global.EventSakusei.My.Resources.Resources.ロゴ
            Me.LOGO.Location = New System.Drawing.Point(198, 2)
            Me.LOGO.Name = "LOGO"
            Me.LOGO.Size = New System.Drawing.Size(136, 26)
            Me.LOGO.TabIndex = 83
            Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnBack
            '
            Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBack.Location = New System.Drawing.Point(465, 3)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(64, 24)
            Me.btnBack.TabIndex = 54
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = True
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Controls.Add(Me.LblBukaName)
            Me.Panel1.Controls.Add(Me.LblDateNow)
            Me.Panel1.Controls.Add(Me.LblUserId)
            Me.Panel1.Controls.Add(Me.LblTimeNow)
            Me.Panel1.Controls.Add(Me.LblPG_ID)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(534, 32)
            Me.Panel1.TabIndex = 58
            '
            'Label4
            '
            Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label4.ForeColor = System.Drawing.Color.Yellow
            Me.Label4.Location = New System.Drawing.Point(128, 1)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(170, 31)
            Me.Label4.TabIndex = 54
            Me.Label4.Text = "予算単価設定表作成"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'LblBukaName
            '
            Me.LblBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblBukaName.ForeColor = System.Drawing.Color.White
            Me.LblBukaName.Location = New System.Drawing.Point(307, 16)
            Me.LblBukaName.Name = "LblBukaName"
            Me.LblBukaName.Size = New System.Drawing.Size(130, 13)
            Me.LblBukaName.TabIndex = 59
            Me.LblBukaName.Text = "(部課：SKE1         )"
            '
            'LblDateNow
            '
            Me.LblDateNow.AutoSize = True
            Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblDateNow.ForeColor = System.Drawing.Color.White
            Me.LblDateNow.Location = New System.Drawing.Point(443, 3)
            Me.LblDateNow.Name = "LblDateNow"
            Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
            Me.LblDateNow.TabIndex = 56
            Me.LblDateNow.Text = "YYYY/MM/DD"
            '
            'LblUserId
            '
            Me.LblUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblUserId.ForeColor = System.Drawing.Color.White
            Me.LblUserId.Location = New System.Drawing.Point(307, 3)
            Me.LblUserId.Name = "LblUserId"
            Me.LblUserId.Size = New System.Drawing.Size(130, 13)
            Me.LblUserId.TabIndex = 58
            Me.LblUserId.Text = "(ID    ：ABCDEFGH)"
            '
            'LblTimeNow
            '
            Me.LblTimeNow.AutoSize = True
            Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblTimeNow.ForeColor = System.Drawing.Color.White
            Me.LblTimeNow.Location = New System.Drawing.Point(467, 16)
            Me.LblTimeNow.Name = "LblTimeNow"
            Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
            Me.LblTimeNow.TabIndex = 55
            Me.LblTimeNow.Text = "HH:MM:DD"
            '
            'LblPG_ID
            '
            Me.LblPG_ID.AutoSize = True
            Me.LblPG_ID.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblPG_ID.ForeColor = System.Drawing.Color.White
            Me.LblPG_ID.Location = New System.Drawing.Point(4, 4)
            Me.LblPG_ID.Name = "LblPG_ID"
            Me.LblPG_ID.Size = New System.Drawing.Size(113, 13)
            Me.LblPG_ID.TabIndex = 54
            Me.LblPG_ID.Text = "PG-ID：XXXXXXXX"
            '
            'btnADD
            '
            Me.btnADD.BackColor = System.Drawing.Color.PaleGreen
            Me.btnADD.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnADD.Location = New System.Drawing.Point(450, 10)
            Me.btnADD.Name = "btnADD"
            Me.btnADD.Size = New System.Drawing.Size(79, 24)
            Me.btnADD.TabIndex = 11
            Me.btnADD.Text = "作成"
            Me.btnADD.UseVisualStyleBackColor = False
            '
            'cmbGroupNo
            '
            Me.cmbGroupNo.FormattingEnabled = True
            Me.cmbGroupNo.Items.AddRange(New Object() {"", "Ａ", "Ｄ"})
            Me.cmbGroupNo.Location = New System.Drawing.Point(8, 25)
            Me.cmbGroupNo.MaxLength = 3
            Me.cmbGroupNo.Name = "cmbGroupNo"
            Me.cmbGroupNo.Size = New System.Drawing.Size(98, 20)
            Me.cmbGroupNo.TabIndex = 1
            Me.ToolTip1.SetToolTip(Me.cmbGroupNo, "指定されたグループNo.の「手配帳」を作成します。")
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label7.Location = New System.Drawing.Point(6, 10)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(61, 12)
            Me.Label7.TabIndex = 89
            Me.Label7.Text = "グループ№："
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label10.Location = New System.Drawing.Point(125, 11)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(59, 12)
            Me.Label10.TabIndex = 92
            Me.Label10.Text = "工事区分："
            '
            'txtKoujiKbn
            '
            Me.txtKoujiKbn.BackColor = System.Drawing.Color.White
            Me.txtKoujiKbn.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtKoujiKbn.ForeColor = System.Drawing.Color.Navy
            Me.txtKoujiKbn.Location = New System.Drawing.Point(127, 26)
            Me.txtKoujiKbn.MaxLength = 2
            Me.txtKoujiKbn.Name = "txtKoujiKbn"
            Me.txtKoujiKbn.Size = New System.Drawing.Size(100, 19)
            Me.txtKoujiKbn.TabIndex = 2
            Me.txtKoujiKbn.Text = "41"
            '
            'cmbSeihinKbn
            '
            Me.cmbSeihinKbn.FormattingEnabled = True
            Me.cmbSeihinKbn.Items.AddRange(New Object() {"", "Ｌ", "Ｆ"})
            Me.cmbSeihinKbn.Location = New System.Drawing.Point(246, 26)
            Me.cmbSeihinKbn.MaxLength = 1
            Me.cmbSeihinKbn.Name = "cmbSeihinKbn"
            Me.cmbSeihinKbn.Size = New System.Drawing.Size(98, 20)
            Me.cmbSeihinKbn.TabIndex = 3
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label8.Location = New System.Drawing.Point(244, 11)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(59, 12)
            Me.Label8.TabIndex = 93
            Me.Label8.Text = "製品区分："
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label9.Location = New System.Drawing.Point(6, 58)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(71, 12)
            Me.Label9.TabIndex = 96
            Me.Label9.Text = "工事指令№："
            '
            'txtKoujiShireiNo
            '
            Me.txtKoujiShireiNo.BackColor = System.Drawing.Color.White
            Me.txtKoujiShireiNo.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtKoujiShireiNo.ForeColor = System.Drawing.Color.Navy
            Me.txtKoujiShireiNo.Location = New System.Drawing.Point(8, 73)
            Me.txtKoujiShireiNo.MaxLength = 3
            Me.txtKoujiShireiNo.Name = "txtKoujiShireiNo"
            Me.txtKoujiShireiNo.Size = New System.Drawing.Size(100, 19)
            Me.txtKoujiShireiNo.TabIndex = 5
            Me.txtKoujiShireiNo.Text = "RJY"
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label11.Location = New System.Drawing.Point(125, 58)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(47, 12)
            Me.Label11.TabIndex = 98
            Me.Label11.Text = "工事№："
            '
            'txtKoujiNo
            '
            Me.txtKoujiNo.BackColor = System.Drawing.Color.White
            Me.txtKoujiNo.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtKoujiNo.ForeColor = System.Drawing.Color.Navy
            Me.txtKoujiNo.Location = New System.Drawing.Point(127, 73)
            Me.txtKoujiNo.MaxLength = 3
            Me.txtKoujiNo.Name = "txtKoujiNo"
            Me.txtKoujiNo.Size = New System.Drawing.Size(100, 19)
            Me.txtKoujiNo.TabIndex = 6
            Me.txtKoujiNo.Text = "L3"
            '
            'Label12
            '
            Me.Label12.AutoSize = True
            Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label12.Location = New System.Drawing.Point(244, 58)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(71, 12)
            Me.Label12.TabIndex = 99
            Me.Label12.Text = "イベント名称："
            '
            'txtEventName
            '
            Me.txtEventName.BackColor = System.Drawing.Color.White
            Me.txtEventName.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtEventName.ForeColor = System.Drawing.Color.Navy
            Me.txtEventName.Location = New System.Drawing.Point(246, 73)
            Me.txtEventName.MaxLength = 20
            Me.txtEventName.Name = "txtEventName"
            Me.txtEventName.Size = New System.Drawing.Size(275, 19)
            Me.txtEventName.TabIndex = 7
            Me.txtEventName.Text = "MF1  台車Ⅰ（トリム）"
            '
            'Panel3
            '
            Me.Panel3.AutoSize = True
            Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel3.BackColor = System.Drawing.SystemColors.Control
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Controls.Add(Me.Label15)
            Me.Panel3.Controls.Add(Me.Label14)
            Me.Panel3.Controls.Add(Me.Panel6)
            Me.Panel3.Controls.Add(Me.Panel5)
            Me.Panel3.Controls.Add(Me.Label13)
            Me.Panel3.Controls.Add(Me.Panel4)
            Me.Panel3.Controls.Add(Me.Label12)
            Me.Panel3.Controls.Add(Me.btnADD)
            Me.Panel3.Controls.Add(Me.txtEventName)
            Me.Panel3.Controls.Add(Me.Label7)
            Me.Panel3.Controls.Add(Me.Label11)
            Me.Panel3.Controls.Add(Me.cmbGroupNo)
            Me.Panel3.Controls.Add(Me.txtKoujiNo)
            Me.Panel3.Controls.Add(Me.txtKoujiKbn)
            Me.Panel3.Controls.Add(Me.Label9)
            Me.Panel3.Controls.Add(Me.Label10)
            Me.Panel3.Controls.Add(Me.txtKoujiShireiNo)
            Me.Panel3.Controls.Add(Me.Label8)
            Me.Panel3.Controls.Add(Me.cmbSeihinKbn)
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel3.Location = New System.Drawing.Point(0, 64)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(534, 149)
            Me.Panel3.TabIndex = 101
            '
            'Label15
            '
            Me.Label15.AutoSize = True
            Me.Label15.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label15.Location = New System.Drawing.Point(373, 105)
            Me.Label15.Name = "Label15"
            Me.Label15.Size = New System.Drawing.Size(114, 12)
            Me.Label15.TabIndex = 105
            Me.Label15.Text = "集計コードからの展開："
            '
            'Label14
            '
            Me.Label14.AutoSize = True
            Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label14.Location = New System.Drawing.Point(202, 105)
            Me.Label14.Name = "Label14"
            Me.Label14.Size = New System.Drawing.Size(94, 12)
            Me.Label14.TabIndex = 103
            Me.Label14.Text = "比較結果織込み："
            '
            'Panel6
            '
            Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel6.Controls.Add(Me.RbtnSyukeiTenkaiShinai)
            Me.Panel6.Controls.Add(Me.RbtnSyukeiTenkaiSuru)
            Me.Panel6.Location = New System.Drawing.Point(375, 120)
            Me.Panel6.Name = "Panel6"
            Me.Panel6.Size = New System.Drawing.Size(125, 24)
            Me.Panel6.TabIndex = 10
            '
            'RbtnSyukeiTenkaiShinai
            '
            Me.RbtnSyukeiTenkaiShinai.AutoSize = True
            Me.RbtnSyukeiTenkaiShinai.Location = New System.Drawing.Point(61, 3)
            Me.RbtnSyukeiTenkaiShinai.Name = "RbtnSyukeiTenkaiShinai"
            Me.RbtnSyukeiTenkaiShinai.Size = New System.Drawing.Size(52, 16)
            Me.RbtnSyukeiTenkaiShinai.TabIndex = 1
            Me.RbtnSyukeiTenkaiShinai.TabStop = True
            Me.RbtnSyukeiTenkaiShinai.Text = "しない"
            Me.ToolTip1.SetToolTip(Me.RbtnSyukeiTenkaiShinai, "集計コードからの展開を行う場合、「する」へチェックしてください。")
            Me.RbtnSyukeiTenkaiShinai.UseVisualStyleBackColor = True
            '
            'RbtnSyukeiTenkaiSuru
            '
            Me.RbtnSyukeiTenkaiSuru.AutoSize = True
            Me.RbtnSyukeiTenkaiSuru.Location = New System.Drawing.Point(13, 3)
            Me.RbtnSyukeiTenkaiSuru.Name = "RbtnSyukeiTenkaiSuru"
            Me.RbtnSyukeiTenkaiSuru.Size = New System.Drawing.Size(42, 16)
            Me.RbtnSyukeiTenkaiSuru.TabIndex = 0
            Me.RbtnSyukeiTenkaiSuru.TabStop = True
            Me.RbtnSyukeiTenkaiSuru.Text = "する"
            Me.ToolTip1.SetToolTip(Me.RbtnSyukeiTenkaiSuru, "集計コードからの展開を行う場合、「する」へチェックしてください。")
            Me.RbtnSyukeiTenkaiSuru.UseVisualStyleBackColor = True
            '
            'Panel5
            '
            Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel5.Controls.Add(Me.RbtnHikakuOrikomiShinai)
            Me.Panel5.Controls.Add(Me.RbtnHikakuOrikomiSuru)
            Me.Panel5.Location = New System.Drawing.Point(204, 120)
            Me.Panel5.Name = "Panel5"
            Me.Panel5.Size = New System.Drawing.Size(125, 24)
            Me.Panel5.TabIndex = 9
            '
            'RbtnHikakuOrikomiShinai
            '
            Me.RbtnHikakuOrikomiShinai.AutoSize = True
            Me.RbtnHikakuOrikomiShinai.Location = New System.Drawing.Point(61, 3)
            Me.RbtnHikakuOrikomiShinai.Name = "RbtnHikakuOrikomiShinai"
            Me.RbtnHikakuOrikomiShinai.Size = New System.Drawing.Size(52, 16)
            Me.RbtnHikakuOrikomiShinai.TabIndex = 1
            Me.RbtnHikakuOrikomiShinai.TabStop = True
            Me.RbtnHikakuOrikomiShinai.Text = "しない"
            Me.ToolTip1.SetToolTip(Me.RbtnHikakuOrikomiShinai, "ベース部品表からの変更点のみ手配する場合、「する」へチェックしてください。")
            Me.RbtnHikakuOrikomiShinai.UseVisualStyleBackColor = True
            '
            'RbtnHikakuOrikomiSuru
            '
            Me.RbtnHikakuOrikomiSuru.AutoSize = True
            Me.RbtnHikakuOrikomiSuru.Location = New System.Drawing.Point(13, 3)
            Me.RbtnHikakuOrikomiSuru.Name = "RbtnHikakuOrikomiSuru"
            Me.RbtnHikakuOrikomiSuru.Size = New System.Drawing.Size(42, 16)
            Me.RbtnHikakuOrikomiSuru.TabIndex = 0
            Me.RbtnHikakuOrikomiSuru.TabStop = True
            Me.RbtnHikakuOrikomiSuru.Text = "する"
            Me.ToolTip1.SetToolTip(Me.RbtnHikakuOrikomiSuru, "ベース部品表からの変更点のみ手配する場合、「する」へチェックしてください。")
            Me.RbtnHikakuOrikomiSuru.UseVisualStyleBackColor = True
            '
            'Label13
            '
            Me.Label13.AutoSize = True
            Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label13.Location = New System.Drawing.Point(33, 105)
            Me.Label13.Name = "Label13"
            Me.Label13.Size = New System.Drawing.Size(97, 12)
            Me.Label13.TabIndex = 101
            Me.Label13.Text = "自給品の消しこみ："
            '
            'Panel4
            '
            Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel4.Controls.Add(Me.RbtnJIkyuhinShinai)
            Me.Panel4.Controls.Add(Me.RbtnJIkyuhinSuru)
            Me.Panel4.Location = New System.Drawing.Point(35, 120)
            Me.Panel4.Name = "Panel4"
            Me.Panel4.Size = New System.Drawing.Size(125, 24)
            Me.Panel4.TabIndex = 8
            '
            'RbtnJIkyuhinShinai
            '
            Me.RbtnJIkyuhinShinai.AutoSize = True
            Me.RbtnJIkyuhinShinai.Location = New System.Drawing.Point(61, 3)
            Me.RbtnJIkyuhinShinai.Name = "RbtnJIkyuhinShinai"
            Me.RbtnJIkyuhinShinai.Size = New System.Drawing.Size(52, 16)
            Me.RbtnJIkyuhinShinai.TabIndex = 1
            Me.RbtnJIkyuhinShinai.TabStop = True
            Me.RbtnJIkyuhinShinai.Text = "しない"
            Me.ToolTip1.SetToolTip(Me.RbtnJIkyuhinShinai, "自給品が不要の場合、「する」へチェックしてください。")
            Me.RbtnJIkyuhinShinai.UseVisualStyleBackColor = True
            '
            'RbtnJIkyuhinSuru
            '
            Me.RbtnJIkyuhinSuru.AutoSize = True
            Me.RbtnJIkyuhinSuru.Location = New System.Drawing.Point(13, 3)
            Me.RbtnJIkyuhinSuru.Name = "RbtnJIkyuhinSuru"
            Me.RbtnJIkyuhinSuru.Size = New System.Drawing.Size(42, 16)
            Me.RbtnJIkyuhinSuru.TabIndex = 0
            Me.RbtnJIkyuhinSuru.TabStop = True
            Me.RbtnJIkyuhinSuru.Text = "する"
            Me.ToolTip1.SetToolTip(Me.RbtnJIkyuhinSuru, "自給品が不要の場合、「する」へチェックしてください。")
            Me.RbtnJIkyuhinSuru.UseVisualStyleBackColor = True
            '
            'Panel7
            '
            Me.Panel7.AutoSize = True
            Me.Panel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel7.BackColor = System.Drawing.SystemColors.Control
            Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel7.Controls.Add(Me.Label16)
            Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel7.Location = New System.Drawing.Point(0, 213)
            Me.Panel7.Name = "Panel7"
            Me.Panel7.Size = New System.Drawing.Size(534, 25)
            Me.Panel7.TabIndex = 102
            '
            'Label16
            '
            Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.Label16.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label16.ForeColor = System.Drawing.Color.Red
            Me.Label16.Location = New System.Drawing.Point(3, 1)
            Me.Label16.Name = "Label16"
            Me.Label16.Size = New System.Drawing.Size(526, 22)
            Me.Label16.TabIndex = 55
            Me.Label16.Text = "「作成」ボタンををクリックしてください。"
            Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Timer1
            '
            Me.Timer1.Enabled = True
            '
            'frmDispYosanSetteiBuhinSakusei
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(534, 238)
            Me.Controls.Add(Me.Panel7)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "frmDispYosanSetteiBuhinSakusei"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel2.ResumeLayout(False)
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Panel3.ResumeLayout(False)
            Me.Panel3.PerformLayout()
            Me.Panel6.ResumeLayout(False)
            Me.Panel6.PerformLayout()
            Me.Panel5.ResumeLayout(False)
            Me.Panel5.PerformLayout()
            Me.Panel4.ResumeLayout(False)
            Me.Panel4.PerformLayout()
            Me.Panel7.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents btnBack As System.Windows.Forms.Button
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents LblBukaName As System.Windows.Forms.Label
        Friend WithEvents LblDateNow As System.Windows.Forms.Label
        Friend WithEvents LblUserId As System.Windows.Forms.Label
        Friend WithEvents LblTimeNow As System.Windows.Forms.Label
        Friend WithEvents LblPG_ID As System.Windows.Forms.Label
        Friend WithEvents btnADD As System.Windows.Forms.Button
        Friend WithEvents cmbGroupNo As System.Windows.Forms.ComboBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents txtKoujiKbn As System.Windows.Forms.TextBox
        Friend WithEvents cmbSeihinKbn As System.Windows.Forms.ComboBox
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents txtKoujiShireiNo As System.Windows.Forms.TextBox
        Friend WithEvents Label11 As System.Windows.Forms.Label
        Friend WithEvents txtKoujiNo As System.Windows.Forms.TextBox
        Friend WithEvents Label12 As System.Windows.Forms.Label
        Friend WithEvents txtEventName As System.Windows.Forms.TextBox
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents Panel4 As System.Windows.Forms.Panel
        Friend WithEvents RbtnJIkyuhinShinai As System.Windows.Forms.RadioButton
        Friend WithEvents RbtnJIkyuhinSuru As System.Windows.Forms.RadioButton
        Friend WithEvents Label14 As System.Windows.Forms.Label
        Friend WithEvents Panel5 As System.Windows.Forms.Panel
        Friend WithEvents RbtnHikakuOrikomiShinai As System.Windows.Forms.RadioButton
        Friend WithEvents RbtnHikakuOrikomiSuru As System.Windows.Forms.RadioButton
        Friend WithEvents Label13 As System.Windows.Forms.Label
        Friend WithEvents Label15 As System.Windows.Forms.Label
        Friend WithEvents Panel6 As System.Windows.Forms.Panel
        Friend WithEvents RbtnSyukeiTenkaiShinai As System.Windows.Forms.RadioButton
        Friend WithEvents RbtnSyukeiTenkaiSuru As System.Windows.Forms.RadioButton
        Friend WithEvents Panel7 As System.Windows.Forms.Panel
        Friend WithEvents Label16 As System.Windows.Forms.Label
        Friend WithEvents LOGO As System.Windows.Forms.Label
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    End Class
End Namespace

