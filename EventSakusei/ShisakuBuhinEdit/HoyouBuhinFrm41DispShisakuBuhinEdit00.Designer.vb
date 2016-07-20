Namespace ShisakuBuhinEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class HoyouBuhinFrm41DispShisakuBuhinEdit00
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HoyouBuhinFrm41DispShisakuBuhinEdit00))
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.btnColorCLEAR = New System.Windows.Forms.Button
            Me.btnCOLOR = New System.Windows.Forms.Button
            Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
            Me.MsTool1 = New System.Windows.Forms.ToolStripMenuItem
            Me.msBuhinKouseiHyoujiTool = New System.Windows.Forms.ToolStripMenuItem
            Me.miWin = New System.Windows.Forms.ToolStripMenuItem
            Me.miWinCascade = New System.Windows.Forms.ToolStripMenuItem
            Me.miWinTileHorizontal = New System.Windows.Forms.ToolStripMenuItem
            Me.miWinTileVertical = New System.Windows.Forms.ToolStripMenuItem
            Me.BtnBack = New System.Windows.Forms.Button
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.TxtTel = New System.Windows.Forms.TextBox
            Me.LblTel = New System.Windows.Forms.Label
            Me.btnExcelExport = New System.Windows.Forms.Button
            Me.txtNaiyou = New System.Windows.Forms.TextBox
            Me.lblDispMode = New System.Windows.Forms.Label
            Me.btnView = New System.Windows.Forms.Button
            Me.LblUserName = New System.Windows.Forms.Label
            Me.LblBukaName = New System.Windows.Forms.Label
            Me.LblTanto = New System.Windows.Forms.Label
            Me.LblBuka = New System.Windows.Forms.Label
            Me.BtnSave = New System.Windows.Forms.Button
            Me.BtnCall = New System.Windows.Forms.Button
            Me.BtnRegister = New System.Windows.Forms.Button
            Me.LblEvent = New System.Windows.Forms.Label
            Me.cmbTanto = New System.Windows.Forms.ComboBox
            Me.lblBuhinNo = New System.Windows.Forms.Label
            Me.lblNaiyou2 = New System.Windows.Forms.Label
            Me.lblNaiyou = New System.Windows.Forms.Label
            Me.lblNaiyou1 = New System.Windows.Forms.Label
            Me.lblDispOnly = New System.Windows.Forms.Label
            Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
            Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
            Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
            Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.MenuStrip1.SuspendLayout()
            Me.Panel3.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel2
            '
            Me.Panel2.AutoSize = True
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Location = New System.Drawing.Point(12, 99)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(2, 2)
            Me.Panel2.TabIndex = 80
            '
            'btnColorCLEAR
            '
            Me.btnColorCLEAR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnColorCLEAR.BackColor = System.Drawing.Color.White
            Me.btnColorCLEAR.Location = New System.Drawing.Point(608, 1)
            Me.btnColorCLEAR.Name = "btnColorCLEAR"
            Me.btnColorCLEAR.Size = New System.Drawing.Size(24, 23)
            Me.btnColorCLEAR.TabIndex = 89
            Me.btnColorCLEAR.TabStop = False
            Me.ToolTip1.SetToolTip(Me.btnColorCLEAR, "セルの背景色をデフォルトに戻します。")
            Me.btnColorCLEAR.UseVisualStyleBackColor = False
            '
            'btnCOLOR
            '
            Me.btnCOLOR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnCOLOR.Image = Global.EventSakusei.My.Resources.Resources.COLOR
            Me.btnCOLOR.Location = New System.Drawing.Point(638, 1)
            Me.btnCOLOR.Name = "btnCOLOR"
            Me.btnCOLOR.Size = New System.Drawing.Size(24, 23)
            Me.btnCOLOR.TabIndex = 88
            Me.btnCOLOR.TabStop = False
            Me.ToolTip1.SetToolTip(Me.btnCOLOR, "選択セルの背景色を変更します。")
            Me.btnCOLOR.UseVisualStyleBackColor = False
            '
            'MenuStrip1
            '
            Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MsTool1, Me.miWin})
            Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
            Me.MenuStrip1.Name = "MenuStrip1"
            Me.MenuStrip1.Size = New System.Drawing.Size(905, 24)
            Me.MenuStrip1.TabIndex = 87
            Me.MenuStrip1.Text = "MenuStrip1"
            Me.MenuStrip1.Visible = False
            '
            'MsTool1
            '
            Me.MsTool1.BackColor = System.Drawing.SystemColors.Control
            Me.MsTool1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msBuhinKouseiHyoujiTool})
            Me.MsTool1.Name = "MsTool1"
            Me.MsTool1.Size = New System.Drawing.Size(74, 20)
            Me.MsTool1.Text = "ツール(&T)"
            Me.MsTool1.Visible = False
            '
            'msBuhinKouseiHyoujiTool
            '
            Me.msBuhinKouseiHyoujiTool.Name = "msBuhinKouseiHyoujiTool"
            Me.msBuhinKouseiHyoujiTool.Size = New System.Drawing.Size(203, 22)
            Me.msBuhinKouseiHyoujiTool.Text = "部品構成表示ツール(&D)"
            '
            'miWin
            '
            Me.miWin.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miWinCascade, Me.miWinTileHorizontal, Me.miWinTileVertical})
            Me.miWin.Name = "miWin"
            Me.miWin.Size = New System.Drawing.Size(102, 20)
            Me.miWin.Text = "ウィンドウ(&W)"
            Me.miWin.Visible = False
            '
            'miWinCascade
            '
            Me.miWinCascade.Name = "miWinCascade"
            Me.miWinCascade.Size = New System.Drawing.Size(191, 22)
            Me.miWinCascade.Text = "重ねて表示(&C)"
            '
            'miWinTileHorizontal
            '
            Me.miWinTileHorizontal.Name = "miWinTileHorizontal"
            Me.miWinTileHorizontal.Size = New System.Drawing.Size(191, 22)
            Me.miWinTileHorizontal.Text = "左右に並べて表示(&H)"
            '
            'miWinTileVertical
            '
            Me.miWinTileVertical.Name = "miWinTileVertical"
            Me.miWinTileVertical.Size = New System.Drawing.Size(191, 22)
            Me.miWinTileVertical.Text = "上下に並べて表示(&V)"
            '
            'BtnBack
            '
            Me.BtnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnBack.Location = New System.Drawing.Point(668, 1)
            Me.BtnBack.Name = "BtnBack"
            Me.BtnBack.Size = New System.Drawing.Size(64, 24)
            Me.BtnBack.TabIndex = 7
            Me.BtnBack.Text = "戻る"
            Me.BtnBack.UseVisualStyleBackColor = True
            '
            'Panel3
            '
            Me.Panel3.AutoSize = True
            Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel3.BackColor = System.Drawing.SystemColors.Control
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Controls.Add(Me.TxtTel)
            Me.Panel3.Controls.Add(Me.LblTel)
            Me.Panel3.Controls.Add(Me.btnExcelExport)
            Me.Panel3.Controls.Add(Me.txtNaiyou)
            Me.Panel3.Controls.Add(Me.lblDispMode)
            Me.Panel3.Controls.Add(Me.btnView)
            Me.Panel3.Controls.Add(Me.LblUserName)
            Me.Panel3.Controls.Add(Me.LblBukaName)
            Me.Panel3.Controls.Add(Me.LblTanto)
            Me.Panel3.Controls.Add(Me.LblBuka)
            Me.Panel3.Controls.Add(Me.BtnSave)
            Me.Panel3.Controls.Add(Me.BtnCall)
            Me.Panel3.Controls.Add(Me.btnColorCLEAR)
            Me.Panel3.Controls.Add(Me.BtnRegister)
            Me.Panel3.Controls.Add(Me.LblEvent)
            Me.Panel3.Controls.Add(Me.btnCOLOR)
            Me.Panel3.Controls.Add(Me.cmbTanto)
            Me.Panel3.Controls.Add(Me.BtnBack)
            Me.Panel3.Controls.Add(Me.lblBuhinNo)
            Me.Panel3.Controls.Add(Me.lblNaiyou2)
            Me.Panel3.Controls.Add(Me.lblNaiyou)
            Me.Panel3.Controls.Add(Me.lblNaiyou1)
            Me.Panel3.Controls.Add(Me.lblDispOnly)
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel3.Location = New System.Drawing.Point(0, 0)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(905, 57)
            Me.Panel3.TabIndex = 0
            '
            'TxtTel
            '
            Me.TxtTel.BackColor = System.Drawing.SystemColors.Control
            Me.TxtTel.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.TxtTel.Enabled = False
            Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.TxtTel.ForeColor = System.Drawing.Color.Black
            Me.TxtTel.Location = New System.Drawing.Point(542, 9)
            Me.TxtTel.MaxLength = 4
            Me.TxtTel.Name = "TxtTel"
            Me.TxtTel.Size = New System.Drawing.Size(45, 12)
            Me.TxtTel.TabIndex = 3
            Me.TxtTel.Text = "2012"
            Me.TxtTel.Visible = False
            '
            'LblTel
            '
            Me.LblTel.Enabled = False
            Me.LblTel.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblTel.Location = New System.Drawing.Point(509, 6)
            Me.LblTel.Name = "LblTel"
            Me.LblTel.Size = New System.Drawing.Size(37, 19)
            Me.LblTel.TabIndex = 142
            Me.LblTel.Text = "TEL："
            Me.LblTel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.LblTel.Visible = False
            '
            'btnExcelExport
            '
            Me.btnExcelExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnExcelExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.btnExcelExport.Enabled = False
            Me.btnExcelExport.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnExcelExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnExcelExport.Location = New System.Drawing.Point(821, 27)
            Me.btnExcelExport.Name = "btnExcelExport"
            Me.btnExcelExport.Size = New System.Drawing.Size(79, 24)
            Me.btnExcelExport.TabIndex = 151
            Me.btnExcelExport.Text = "Excel出力"
            Me.ToolTip1.SetToolTip(Me.btnExcelExport, "補用部品表情報を「EXCEL」へ出力します。")
            Me.btnExcelExport.UseVisualStyleBackColor = False
            Me.btnExcelExport.Visible = False
            '
            'txtNaiyou
            '
            Me.txtNaiyou.BackColor = System.Drawing.Color.White
            Me.txtNaiyou.Enabled = False
            Me.txtNaiyou.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtNaiyou.ForeColor = System.Drawing.Color.Navy
            Me.txtNaiyou.ImeMode = System.Windows.Forms.ImeMode.Hiragana
            Me.txtNaiyou.Location = New System.Drawing.Point(542, 28)
            Me.txtNaiyou.MaxLength = 100
            Me.txtNaiyou.Name = "txtNaiyou"
            Me.txtNaiyou.Size = New System.Drawing.Size(356, 19)
            Me.txtNaiyou.TabIndex = 4
            Me.txtNaiyou.Visible = False
            '
            'lblDispMode
            '
            Me.lblDispMode.AutoSize = True
            Me.lblDispMode.Enabled = False
            Me.lblDispMode.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblDispMode.ForeColor = System.Drawing.Color.Red
            Me.lblDispMode.Location = New System.Drawing.Point(321, 29)
            Me.lblDispMode.Name = "lblDispMode"
            Me.lblDispMode.Size = New System.Drawing.Size(109, 19)
            Me.lblDispMode.TabIndex = 146
            Me.lblDispMode.Text = "閲覧表示中"
            Me.lblDispMode.Visible = False
            '
            'btnView
            '
            Me.btnView.BackColor = System.Drawing.Color.LightCyan
            Me.btnView.Enabled = False
            Me.btnView.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnView.Location = New System.Drawing.Point(236, 28)
            Me.btnView.Name = "btnView"
            Me.btnView.Size = New System.Drawing.Size(79, 24)
            Me.btnView.TabIndex = 2
            Me.btnView.Text = "閲覧"
            Me.ToolTip1.SetToolTip(Me.btnView, "他担当へ遷移します。")
            Me.btnView.UseVisualStyleBackColor = False
            Me.btnView.Visible = False
            '
            'LblUserName
            '
            Me.LblUserName.Enabled = False
            Me.LblUserName.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblUserName.Location = New System.Drawing.Point(402, 6)
            Me.LblUserName.Name = "LblUserName"
            Me.LblUserName.Size = New System.Drawing.Size(101, 19)
            Me.LblUserName.TabIndex = 144
            Me.LblUserName.Text = "○尾△也"
            Me.LblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.LblUserName.Visible = False
            '
            'LblBukaName
            '
            Me.LblBukaName.Enabled = False
            Me.LblBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblBukaName.Location = New System.Drawing.Point(316, 6)
            Me.LblBukaName.Name = "LblBukaName"
            Me.LblBukaName.Size = New System.Drawing.Size(44, 19)
            Me.LblBukaName.TabIndex = 143
            Me.LblBukaName.Text = "SKE1"
            Me.LblBukaName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.LblBukaName.Visible = False
            '
            'LblTanto
            '
            Me.LblTanto.Enabled = False
            Me.LblTanto.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblTanto.Location = New System.Drawing.Point(359, 6)
            Me.LblTanto.Name = "LblTanto"
            Me.LblTanto.Size = New System.Drawing.Size(47, 19)
            Me.LblTanto.TabIndex = 141
            Me.LblTanto.Text = "担当者："
            Me.LblTanto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.LblTanto.Visible = False
            '
            'LblBuka
            '
            Me.LblBuka.Enabled = False
            Me.LblBuka.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblBuka.Location = New System.Drawing.Point(262, 6)
            Me.LblBuka.Name = "LblBuka"
            Me.LblBuka.Size = New System.Drawing.Size(59, 19)
            Me.LblBuka.TabIndex = 140
            Me.LblBuka.Text = "担当設計："
            Me.LblBuka.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.LblBuka.Visible = False
            '
            'BtnSave
            '
            Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnSave.BackColor = System.Drawing.Color.LightCyan
            Me.BtnSave.Enabled = False
            Me.BtnSave.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnSave.Location = New System.Drawing.Point(821, 1)
            Me.BtnSave.Name = "BtnSave"
            Me.BtnSave.Size = New System.Drawing.Size(79, 24)
            Me.BtnSave.TabIndex = 5
            Me.BtnSave.Text = "保存"
            Me.ToolTip1.SetToolTip(Me.BtnSave, "補用部品表情報を一時保存します。")
            Me.BtnSave.UseVisualStyleBackColor = False
            Me.BtnSave.Visible = False
            '
            'BtnCall
            '
            Me.BtnCall.BackColor = System.Drawing.Color.LightCyan
            Me.BtnCall.Enabled = False
            Me.BtnCall.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnCall.Location = New System.Drawing.Point(151, 28)
            Me.BtnCall.Name = "BtnCall"
            Me.BtnCall.Size = New System.Drawing.Size(79, 24)
            Me.BtnCall.TabIndex = 1
            Me.BtnCall.Text = "編集"
            Me.ToolTip1.SetToolTip(Me.BtnCall, "他担当へ遷移します。")
            Me.BtnCall.UseVisualStyleBackColor = False
            Me.BtnCall.Visible = False
            '
            'BtnRegister
            '
            Me.BtnRegister.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnRegister.BackColor = System.Drawing.Color.PaleGreen
            Me.BtnRegister.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnRegister.Location = New System.Drawing.Point(738, 1)
            Me.BtnRegister.Name = "BtnRegister"
            Me.BtnRegister.Size = New System.Drawing.Size(79, 24)
            Me.BtnRegister.TabIndex = 6
            Me.BtnRegister.Text = "反映"
            Me.ToolTip1.SetToolTip(Me.BtnRegister, "補用部品表情報を登録し以後の作業へ反映させます。")
            Me.BtnRegister.UseVisualStyleBackColor = False
            '
            'LblEvent
            '
            Me.LblEvent.Enabled = False
            Me.LblEvent.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblEvent.ForeColor = System.Drawing.Color.Black
            Me.LblEvent.Location = New System.Drawing.Point(4, 6)
            Me.LblEvent.Name = "LblEvent"
            Me.LblEvent.Size = New System.Drawing.Size(252, 19)
            Me.LblEvent.TabIndex = 130
            Me.LblEvent.Text = "MF1  台車Ⅰ（トリム）"
            Me.LblEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.LblEvent.Visible = False
            '
            'cmbTanto
            '
            Me.cmbTanto.Enabled = False
            Me.cmbTanto.FormattingEnabled = True
            Me.cmbTanto.Location = New System.Drawing.Point(65, 29)
            Me.cmbTanto.Name = "cmbTanto"
            Me.cmbTanto.Size = New System.Drawing.Size(80, 20)
            Me.cmbTanto.TabIndex = 0
            Me.cmbTanto.Visible = False
            '
            'lblBuhinNo
            '
            Me.lblBuhinNo.AutoSize = True
            Me.lblBuhinNo.Enabled = False
            Me.lblBuhinNo.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblBuhinNo.Location = New System.Drawing.Point(4, 32)
            Me.lblBuhinNo.Name = "lblBuhinNo"
            Me.lblBuhinNo.Size = New System.Drawing.Size(47, 12)
            Me.lblBuhinNo.TabIndex = 114
            Me.lblBuhinNo.Text = "担当者："
            Me.lblBuhinNo.Visible = False
            '
            'lblNaiyou2
            '
            Me.lblNaiyou2.Enabled = False
            Me.lblNaiyou2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblNaiyou2.ForeColor = System.Drawing.Color.Blue
            Me.lblNaiyou2.Location = New System.Drawing.Point(503, 29)
            Me.lblNaiyou2.Name = "lblNaiyou2"
            Me.lblNaiyou2.Size = New System.Drawing.Size(43, 19)
            Me.lblNaiyou2.TabIndex = 150
            Me.lblNaiyou2.Text = "]内容："
            Me.lblNaiyou2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.lblNaiyou2.Visible = False
            '
            'lblNaiyou
            '
            Me.lblNaiyou.Enabled = False
            Me.lblNaiyou.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblNaiyou.ForeColor = System.Drawing.Color.Blue
            Me.lblNaiyou.Location = New System.Drawing.Point(473, 28)
            Me.lblNaiyou.Name = "lblNaiyou"
            Me.lblNaiyou.Size = New System.Drawing.Size(35, 19)
            Me.lblNaiyou.TabIndex = 148
            Me.lblNaiyou.Text = "001"
            Me.lblNaiyou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblNaiyou.Visible = False
            '
            'lblNaiyou1
            '
            Me.lblNaiyou1.Enabled = False
            Me.lblNaiyou1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblNaiyou1.ForeColor = System.Drawing.Color.Blue
            Me.lblNaiyou1.Location = New System.Drawing.Point(440, 29)
            Me.lblNaiyou1.Name = "lblNaiyou1"
            Me.lblNaiyou1.Size = New System.Drawing.Size(39, 19)
            Me.lblNaiyou1.TabIndex = 149
            Me.lblNaiyou1.Text = "改訂["
            Me.lblNaiyou1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.lblNaiyou1.Visible = False
            '
            'lblDispOnly
            '
            Me.lblDispOnly.BackColor = System.Drawing.SystemColors.Info
            Me.lblDispOnly.Enabled = False
            Me.lblDispOnly.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblDispOnly.ForeColor = System.Drawing.Color.Red
            Me.lblDispOnly.Location = New System.Drawing.Point(11, 6)
            Me.lblDispOnly.Name = "lblDispOnly"
            Me.lblDispOnly.Size = New System.Drawing.Size(423, 41)
            Me.lblDispOnly.TabIndex = 152
            Me.lblDispOnly.Text = "検 索 専 用"
            Me.lblDispOnly.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblDispOnly.Visible = False
            '
            'ToolStripMenuItem2
            '
            Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
            Me.ToolStripMenuItem2.Size = New System.Drawing.Size(187, 22)
            Me.ToolStripMenuItem2.Text = "機能仕様表示選択(&A)"
            '
            'ToolStripMenuItem3
            '
            Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
            Me.ToolStripMenuItem3.Size = New System.Drawing.Size(187, 22)
            Me.ToolStripMenuItem3.Text = "部品構成呼出し(&B)"
            '
            'ToolStripMenuItem4
            '
            Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
            Me.ToolStripMenuItem4.Size = New System.Drawing.Size(187, 22)
            Me.ToolStripMenuItem4.Text = "部品構成一覧(&C)"
            '
            'ToolStripMenuItem5
            '
            Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
            Me.ToolStripMenuItem5.Size = New System.Drawing.Size(187, 22)
            Me.ToolStripMenuItem5.Text = "部品構成表示ツール(&D)"
            '
            'HoyouBuhinFrm41DispShisakuBuhinEdit00
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.ClientSize = New System.Drawing.Size(905, 477)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.MenuStrip1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.IsMdiContainer = True
            Me.Name = "HoyouBuhinFrm41DispShisakuBuhinEdit00"
            Me.RightToLeftLayout = True
            Me.Text = "新試作手配システム Ver 1.00 　[ 補用部品表 編集 ]"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            Me.MenuStrip1.ResumeLayout(False)
            Me.MenuStrip1.PerformLayout()
            Me.Panel3.ResumeLayout(False)
            Me.Panel3.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents BtnBack As System.Windows.Forms.Button
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents lblBuhinNo As System.Windows.Forms.Label
        Friend WithEvents TxtTel As System.Windows.Forms.TextBox
        Friend WithEvents cmbTanto As System.Windows.Forms.ComboBox
        Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
        Friend WithEvents MsTool1 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents LblEvent As System.Windows.Forms.Label
        Friend WithEvents msBuhinKouseiHyoujiTool As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnRegister As System.Windows.Forms.Button
        Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents miWin As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents miWinCascade As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents miWinTileHorizontal As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents miWinTileVertical As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents btnColorCLEAR As System.Windows.Forms.Button
        Friend WithEvents btnCOLOR As System.Windows.Forms.Button
        Friend WithEvents BtnCall As System.Windows.Forms.Button
        Friend WithEvents BtnSave As System.Windows.Forms.Button
        Friend WithEvents LblTanto As System.Windows.Forms.Label
        Friend WithEvents LblBuka As System.Windows.Forms.Label
        Friend WithEvents LblTel As System.Windows.Forms.Label
        Friend WithEvents LblUserName As System.Windows.Forms.Label
        Friend WithEvents LblBukaName As System.Windows.Forms.Label
        Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
        Friend WithEvents btnView As System.Windows.Forms.Button
        Friend WithEvents lblDispMode As System.Windows.Forms.Label
        Friend WithEvents lblNaiyou As System.Windows.Forms.Label
        Friend WithEvents txtNaiyou As System.Windows.Forms.TextBox
        Friend WithEvents lblNaiyou1 As System.Windows.Forms.Label
        Friend WithEvents lblNaiyou2 As System.Windows.Forms.Label
        Friend WithEvents btnExcelExport As System.Windows.Forms.Button
        Friend WithEvents lblDispOnly As System.Windows.Forms.Label
    End Class
End Namespace
