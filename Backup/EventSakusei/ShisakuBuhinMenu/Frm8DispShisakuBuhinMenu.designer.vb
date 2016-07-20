Namespace ShisakuBuhinMenu
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm8DispShisakuBuhinMenu
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
            Dim cultureInfo As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ja-JP", False)
            Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm8DispShisakuBuhinMenu))
            Me.lblBuhinNo = New System.Windows.Forms.Label
            Me.Label38 = New System.Windows.Forms.Label
            Me.Label39 = New System.Windows.Forms.Label
            Me.Label40 = New System.Windows.Forms.Label
            Me.Label41 = New System.Windows.Forms.Label
            Me.Label42 = New System.Windows.Forms.Label
            Me.Label43 = New System.Windows.Forms.Label
            Me.Label44 = New System.Windows.Forms.Label
            Me.Label45 = New System.Windows.Forms.Label
            Me.Label46 = New System.Windows.Forms.Label
            Me.Label47 = New System.Windows.Forms.Label
            Me.Label48 = New System.Windows.Forms.Label
            Me.Label49 = New System.Windows.Forms.Label
            Me.Label50 = New System.Windows.Forms.Label
            Me.Label51 = New System.Windows.Forms.Label
            Me.Label52 = New System.Windows.Forms.Label
            Me.Label53 = New System.Windows.Forms.Label
            Me.Label54 = New System.Windows.Forms.Label
            Me.Label55 = New System.Windows.Forms.Label
            Me.Label56 = New System.Windows.Forms.Label
            Me.Label57 = New System.Windows.Forms.Label
            Me.Label58 = New System.Windows.Forms.Label
            Me.Label59 = New System.Windows.Forms.Label
            Me.Label60 = New System.Windows.Forms.Label
            Me.Label61 = New System.Windows.Forms.Label
            Me.Label62 = New System.Windows.Forms.Label
            Me.Label63 = New System.Windows.Forms.Label
            Me.Label64 = New System.Windows.Forms.Label
            Me.Label65 = New System.Windows.Forms.Label
            Me.Label66 = New System.Windows.Forms.Label
            Me.Label67 = New System.Windows.Forms.Label
            Me.Label68 = New System.Windows.Forms.Label
            Me.Label69 = New System.Windows.Forms.Label
            Me.Label70 = New System.Windows.Forms.Label
            Me.Label71 = New System.Windows.Forms.Label
            Me.Label72 = New System.Windows.Forms.Label
            Me.Label73 = New System.Windows.Forms.Label
            Me.Label74 = New System.Windows.Forms.Label
            Me.Label75 = New System.Windows.Forms.Label
            Me.Label76 = New System.Windows.Forms.Label
            Me.Label77 = New System.Windows.Forms.Label
            Me.Label78 = New System.Windows.Forms.Label
            Me.Label79 = New System.Windows.Forms.Label
            Me.Label80 = New System.Windows.Forms.Label
            Me.Label81 = New System.Windows.Forms.Label
            Me.Label82 = New System.Windows.Forms.Label
            Me.Label83 = New System.Windows.Forms.Label
            Me.Label84 = New System.Windows.Forms.Label
            Me.Label85 = New System.Windows.Forms.Label
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.Label4 = New System.Windows.Forms.Label
            Me.LblCurrBukaName = New System.Windows.Forms.Label
            Me.LblDateNow = New System.Windows.Forms.Label
            Me.LblCurrUserId = New System.Windows.Forms.Label
            Me.LblTimeNow = New System.Windows.Forms.Label
            Me.LblCurrPGId = New System.Windows.Forms.Label
            Me.Button1 = New System.Windows.Forms.Button
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.LOGO = New System.Windows.Forms.Label
            Me.btnBACK = New System.Windows.Forms.Button
            Me.GroupBox1 = New System.Windows.Forms.GroupBox
            Me.BtnExcelExport = New System.Windows.Forms.Button
            Me.dtpSyochiShimekiribi = New System.Windows.Forms.DateTimePicker
            Me.lblStatus = New System.Windows.Forms.Label
            Me.lblShimekiribi = New System.Windows.Forms.Label
            Me.Label11 = New System.Windows.Forms.Label
            Me.BtnSashimodosi = New System.Windows.Forms.Button
            Me.BtnChushi = New System.Windows.Forms.Button
            Me.BtnKanryou = New System.Windows.Forms.Button
            Me.BtnShimekiri = New System.Windows.Forms.Button
            Me.BtnSekkeiTenkai = New System.Windows.Forms.Button
            Me.Label8 = New System.Windows.Forms.Label
            Me.Label7 = New System.Windows.Forms.Label
            Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.LblMessage = New System.Windows.Forms.Label
            Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.BtnTehaichoSakusei = New System.Windows.Forms.Button
            Me.BtnDelete = New System.Windows.Forms.Button
            Me.Panel6 = New System.Windows.Forms.Panel
            Me.rbMsgOff = New System.Windows.Forms.RadioButton
            Me.rbMsgOn = New System.Windows.Forms.RadioButton
            Me.Panel7 = New System.Windows.Forms.Panel
            Me.rbMsgIkan = New System.Windows.Forms.RadioButton
            Me.rbMsgFull = New System.Windows.Forms.RadioButton
            Me.Panel9 = New System.Windows.Forms.Panel
            Me.rbMailOff = New System.Windows.Forms.RadioButton
            Me.rbMailOn = New System.Windows.Forms.RadioButton
            Me.Panel8 = New System.Windows.Forms.Panel
            Me.rbTsukurikataOn = New System.Windows.Forms.RadioButton
            Me.rbTsukurikataOff = New System.Windows.Forms.RadioButton
            Me.Panel10 = New System.Windows.Forms.Panel
            Me.rbTsukurikataTenkaiOn = New System.Windows.Forms.RadioButton
            Me.rbTsukurikataTenkaiOff = New System.Windows.Forms.RadioButton
            Me.Panel11 = New System.Windows.Forms.Panel
            Me.rbEbomKanshiOff = New System.Windows.Forms.RadioButton
            Me.rbEbomKanshiOn = New System.Windows.Forms.RadioButton
            Me.lblEventName = New System.Windows.Forms.Label
            Me.lblEventCode = New System.Windows.Forms.Label
            Me.Panel4 = New System.Windows.Forms.Panel
            Me.BtnEventRegister = New System.Windows.Forms.Button
            Me.Panel5 = New System.Windows.Forms.Panel
            Me.gbEbomKanshi = New System.Windows.Forms.GroupBox
            Me.BtnTehaichoEdit = New System.Windows.Forms.Button
            Me.Label9 = New System.Windows.Forms.Label
            Me.txtListCode = New System.Windows.Forms.TextBox
            Me.spdParts = New FarPoint.Win.Spread.FpSpread
            Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.gbBlockMsg = New System.Windows.Forms.GroupBox
            Me.gbOshirase = New System.Windows.Forms.GroupBox
            Me.gbTukurikata = New System.Windows.Forms.GroupBox
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.Panel3.SuspendLayout()
            Me.Panel6.SuspendLayout()
            Me.Panel7.SuspendLayout()
            Me.Panel9.SuspendLayout()
            Me.Panel8.SuspendLayout()
            Me.Panel10.SuspendLayout()
            Me.Panel11.SuspendLayout()
            Me.Panel4.SuspendLayout()
            Me.Panel5.SuspendLayout()
            Me.gbEbomKanshi.SuspendLayout()
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.gbBlockMsg.SuspendLayout()
            Me.gbOshirase.SuspendLayout()
            Me.gbTukurikata.SuspendLayout()
            Me.SuspendLayout()
            '
            'lblBuhinNo
            '
            Me.lblBuhinNo.AutoSize = True
            Me.lblBuhinNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblBuhinNo.Location = New System.Drawing.Point(29, 81)
            Me.lblBuhinNo.Name = "lblBuhinNo"
            Me.lblBuhinNo.Size = New System.Drawing.Size(93, 15)
            Me.lblBuhinNo.TabIndex = 8
            Me.lblBuhinNo.Text = "イベントコード："
            '
            'Label38
            '
            Me.Label38.Location = New System.Drawing.Point(0, 0)
            Me.Label38.Name = "Label38"
            Me.Label38.Size = New System.Drawing.Size(100, 23)
            Me.Label38.TabIndex = 0
            '
            'Label39
            '
            Me.Label39.Location = New System.Drawing.Point(0, 0)
            Me.Label39.Name = "Label39"
            Me.Label39.Size = New System.Drawing.Size(100, 23)
            Me.Label39.TabIndex = 0
            '
            'Label40
            '
            Me.Label40.Location = New System.Drawing.Point(0, 0)
            Me.Label40.Name = "Label40"
            Me.Label40.Size = New System.Drawing.Size(100, 23)
            Me.Label40.TabIndex = 0
            '
            'Label41
            '
            Me.Label41.Location = New System.Drawing.Point(0, 0)
            Me.Label41.Name = "Label41"
            Me.Label41.Size = New System.Drawing.Size(100, 23)
            Me.Label41.TabIndex = 0
            '
            'Label42
            '
            Me.Label42.Location = New System.Drawing.Point(0, 0)
            Me.Label42.Name = "Label42"
            Me.Label42.Size = New System.Drawing.Size(100, 23)
            Me.Label42.TabIndex = 0
            '
            'Label43
            '
            Me.Label43.Location = New System.Drawing.Point(0, 0)
            Me.Label43.Name = "Label43"
            Me.Label43.Size = New System.Drawing.Size(100, 23)
            Me.Label43.TabIndex = 0
            '
            'Label44
            '
            Me.Label44.Location = New System.Drawing.Point(0, 0)
            Me.Label44.Name = "Label44"
            Me.Label44.Size = New System.Drawing.Size(100, 23)
            Me.Label44.TabIndex = 0
            '
            'Label45
            '
            Me.Label45.Location = New System.Drawing.Point(0, 0)
            Me.Label45.Name = "Label45"
            Me.Label45.Size = New System.Drawing.Size(100, 23)
            Me.Label45.TabIndex = 0
            '
            'Label46
            '
            Me.Label46.Location = New System.Drawing.Point(0, 0)
            Me.Label46.Name = "Label46"
            Me.Label46.Size = New System.Drawing.Size(100, 23)
            Me.Label46.TabIndex = 0
            '
            'Label47
            '
            Me.Label47.Location = New System.Drawing.Point(0, 0)
            Me.Label47.Name = "Label47"
            Me.Label47.Size = New System.Drawing.Size(100, 23)
            Me.Label47.TabIndex = 0
            '
            'Label48
            '
            Me.Label48.Location = New System.Drawing.Point(0, 0)
            Me.Label48.Name = "Label48"
            Me.Label48.Size = New System.Drawing.Size(100, 23)
            Me.Label48.TabIndex = 0
            '
            'Label49
            '
            Me.Label49.Location = New System.Drawing.Point(0, 0)
            Me.Label49.Name = "Label49"
            Me.Label49.Size = New System.Drawing.Size(100, 23)
            Me.Label49.TabIndex = 0
            '
            'Label50
            '
            Me.Label50.Location = New System.Drawing.Point(0, 0)
            Me.Label50.Name = "Label50"
            Me.Label50.Size = New System.Drawing.Size(100, 23)
            Me.Label50.TabIndex = 0
            '
            'Label51
            '
            Me.Label51.Location = New System.Drawing.Point(0, 0)
            Me.Label51.Name = "Label51"
            Me.Label51.Size = New System.Drawing.Size(100, 23)
            Me.Label51.TabIndex = 0
            '
            'Label52
            '
            Me.Label52.Location = New System.Drawing.Point(0, 0)
            Me.Label52.Name = "Label52"
            Me.Label52.Size = New System.Drawing.Size(100, 23)
            Me.Label52.TabIndex = 0
            '
            'Label53
            '
            Me.Label53.Location = New System.Drawing.Point(0, 0)
            Me.Label53.Name = "Label53"
            Me.Label53.Size = New System.Drawing.Size(100, 23)
            Me.Label53.TabIndex = 0
            '
            'Label54
            '
            Me.Label54.Location = New System.Drawing.Point(0, 0)
            Me.Label54.Name = "Label54"
            Me.Label54.Size = New System.Drawing.Size(100, 23)
            Me.Label54.TabIndex = 0
            '
            'Label55
            '
            Me.Label55.Location = New System.Drawing.Point(0, 0)
            Me.Label55.Name = "Label55"
            Me.Label55.Size = New System.Drawing.Size(100, 23)
            Me.Label55.TabIndex = 0
            '
            'Label56
            '
            Me.Label56.Location = New System.Drawing.Point(0, 0)
            Me.Label56.Name = "Label56"
            Me.Label56.Size = New System.Drawing.Size(100, 23)
            Me.Label56.TabIndex = 0
            '
            'Label57
            '
            Me.Label57.Location = New System.Drawing.Point(0, 0)
            Me.Label57.Name = "Label57"
            Me.Label57.Size = New System.Drawing.Size(100, 23)
            Me.Label57.TabIndex = 0
            '
            'Label58
            '
            Me.Label58.Location = New System.Drawing.Point(0, 0)
            Me.Label58.Name = "Label58"
            Me.Label58.Size = New System.Drawing.Size(100, 23)
            Me.Label58.TabIndex = 0
            '
            'Label59
            '
            Me.Label59.Location = New System.Drawing.Point(0, 0)
            Me.Label59.Name = "Label59"
            Me.Label59.Size = New System.Drawing.Size(100, 23)
            Me.Label59.TabIndex = 0
            '
            'Label60
            '
            Me.Label60.Location = New System.Drawing.Point(0, 0)
            Me.Label60.Name = "Label60"
            Me.Label60.Size = New System.Drawing.Size(100, 23)
            Me.Label60.TabIndex = 0
            '
            'Label61
            '
            Me.Label61.Location = New System.Drawing.Point(0, 0)
            Me.Label61.Name = "Label61"
            Me.Label61.Size = New System.Drawing.Size(100, 23)
            Me.Label61.TabIndex = 0
            '
            'Label62
            '
            Me.Label62.Location = New System.Drawing.Point(0, 0)
            Me.Label62.Name = "Label62"
            Me.Label62.Size = New System.Drawing.Size(100, 23)
            Me.Label62.TabIndex = 0
            '
            'Label63
            '
            Me.Label63.Location = New System.Drawing.Point(0, 0)
            Me.Label63.Name = "Label63"
            Me.Label63.Size = New System.Drawing.Size(100, 23)
            Me.Label63.TabIndex = 0
            '
            'Label64
            '
            Me.Label64.Location = New System.Drawing.Point(0, 0)
            Me.Label64.Name = "Label64"
            Me.Label64.Size = New System.Drawing.Size(100, 23)
            Me.Label64.TabIndex = 0
            '
            'Label65
            '
            Me.Label65.Location = New System.Drawing.Point(0, 0)
            Me.Label65.Name = "Label65"
            Me.Label65.Size = New System.Drawing.Size(100, 23)
            Me.Label65.TabIndex = 0
            '
            'Label66
            '
            Me.Label66.Location = New System.Drawing.Point(0, 0)
            Me.Label66.Name = "Label66"
            Me.Label66.Size = New System.Drawing.Size(100, 23)
            Me.Label66.TabIndex = 0
            '
            'Label67
            '
            Me.Label67.Location = New System.Drawing.Point(0, 0)
            Me.Label67.Name = "Label67"
            Me.Label67.Size = New System.Drawing.Size(100, 23)
            Me.Label67.TabIndex = 0
            '
            'Label68
            '
            Me.Label68.Location = New System.Drawing.Point(0, 0)
            Me.Label68.Name = "Label68"
            Me.Label68.Size = New System.Drawing.Size(100, 23)
            Me.Label68.TabIndex = 0
            '
            'Label69
            '
            Me.Label69.Location = New System.Drawing.Point(0, 0)
            Me.Label69.Name = "Label69"
            Me.Label69.Size = New System.Drawing.Size(100, 23)
            Me.Label69.TabIndex = 0
            '
            'Label70
            '
            Me.Label70.Location = New System.Drawing.Point(0, 0)
            Me.Label70.Name = "Label70"
            Me.Label70.Size = New System.Drawing.Size(100, 23)
            Me.Label70.TabIndex = 0
            '
            'Label71
            '
            Me.Label71.Location = New System.Drawing.Point(0, 0)
            Me.Label71.Name = "Label71"
            Me.Label71.Size = New System.Drawing.Size(100, 23)
            Me.Label71.TabIndex = 0
            '
            'Label72
            '
            Me.Label72.Location = New System.Drawing.Point(0, 0)
            Me.Label72.Name = "Label72"
            Me.Label72.Size = New System.Drawing.Size(100, 23)
            Me.Label72.TabIndex = 0
            '
            'Label73
            '
            Me.Label73.Location = New System.Drawing.Point(0, 0)
            Me.Label73.Name = "Label73"
            Me.Label73.Size = New System.Drawing.Size(100, 23)
            Me.Label73.TabIndex = 0
            '
            'Label74
            '
            Me.Label74.Location = New System.Drawing.Point(0, 0)
            Me.Label74.Name = "Label74"
            Me.Label74.Size = New System.Drawing.Size(100, 23)
            Me.Label74.TabIndex = 0
            '
            'Label75
            '
            Me.Label75.Location = New System.Drawing.Point(0, 0)
            Me.Label75.Name = "Label75"
            Me.Label75.Size = New System.Drawing.Size(100, 23)
            Me.Label75.TabIndex = 0
            '
            'Label76
            '
            Me.Label76.Location = New System.Drawing.Point(0, 0)
            Me.Label76.Name = "Label76"
            Me.Label76.Size = New System.Drawing.Size(100, 23)
            Me.Label76.TabIndex = 0
            '
            'Label77
            '
            Me.Label77.Location = New System.Drawing.Point(0, 0)
            Me.Label77.Name = "Label77"
            Me.Label77.Size = New System.Drawing.Size(100, 23)
            Me.Label77.TabIndex = 0
            '
            'Label78
            '
            Me.Label78.Location = New System.Drawing.Point(0, 0)
            Me.Label78.Name = "Label78"
            Me.Label78.Size = New System.Drawing.Size(100, 23)
            Me.Label78.TabIndex = 0
            '
            'Label79
            '
            Me.Label79.Location = New System.Drawing.Point(0, 0)
            Me.Label79.Name = "Label79"
            Me.Label79.Size = New System.Drawing.Size(100, 23)
            Me.Label79.TabIndex = 0
            '
            'Label80
            '
            Me.Label80.Location = New System.Drawing.Point(0, 0)
            Me.Label80.Name = "Label80"
            Me.Label80.Size = New System.Drawing.Size(100, 23)
            Me.Label80.TabIndex = 0
            '
            'Label81
            '
            Me.Label81.Location = New System.Drawing.Point(0, 0)
            Me.Label81.Name = "Label81"
            Me.Label81.Size = New System.Drawing.Size(100, 23)
            Me.Label81.TabIndex = 0
            '
            'Label82
            '
            Me.Label82.Location = New System.Drawing.Point(0, 0)
            Me.Label82.Name = "Label82"
            Me.Label82.Size = New System.Drawing.Size(100, 23)
            Me.Label82.TabIndex = 0
            '
            'Label83
            '
            Me.Label83.Location = New System.Drawing.Point(0, 0)
            Me.Label83.Name = "Label83"
            Me.Label83.Size = New System.Drawing.Size(100, 23)
            Me.Label83.TabIndex = 0
            '
            'Label84
            '
            Me.Label84.Location = New System.Drawing.Point(0, 0)
            Me.Label84.Name = "Label84"
            Me.Label84.Size = New System.Drawing.Size(100, 23)
            Me.Label84.TabIndex = 0
            '
            'Label85
            '
            Me.Label85.Location = New System.Drawing.Point(0, 0)
            Me.Label85.Name = "Label85"
            Me.Label85.Size = New System.Drawing.Size(100, 23)
            Me.Label85.TabIndex = 0
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Controls.Add(Me.LblCurrBukaName)
            Me.Panel1.Controls.Add(Me.LblDateNow)
            Me.Panel1.Controls.Add(Me.LblCurrUserId)
            Me.Panel1.Controls.Add(Me.LblTimeNow)
            Me.Panel1.Controls.Add(Me.LblCurrPGId)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(3, 3)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(615, 32)
            Me.Panel1.TabIndex = 53
            '
            'Label4
            '
            Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label4.ForeColor = System.Drawing.Color.Yellow
            Me.Label4.Location = New System.Drawing.Point(189, 1)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(203, 31)
            Me.Label4.TabIndex = 54
            Me.Label4.Text = "試作部品表作成メニュー"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'LblCurrBukaName
            '
            Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
            Me.LblCurrBukaName.Location = New System.Drawing.Point(393, 16)
            Me.LblCurrBukaName.Name = "LblCurrBukaName"
            Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
            Me.LblCurrBukaName.TabIndex = 59
            Me.LblCurrBukaName.Text = "(部課：SKE1         )"
            '
            'LblDateNow
            '
            Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblDateNow.AutoSize = True
            Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblDateNow.ForeColor = System.Drawing.Color.White
            Me.LblDateNow.Location = New System.Drawing.Point(523, 3)
            Me.LblDateNow.Name = "LblDateNow"
            Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
            Me.LblDateNow.TabIndex = 56
            Me.LblDateNow.Text = "YYYY/MM/DD"
            '
            'LblCurrUserId
            '
            Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
            Me.LblCurrUserId.Location = New System.Drawing.Point(393, 3)
            Me.LblCurrUserId.Name = "LblCurrUserId"
            Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
            Me.LblCurrUserId.TabIndex = 58
            Me.LblCurrUserId.Text = "(ID    ：ABCDEFGH)"
            '
            'LblTimeNow
            '
            Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblTimeNow.AutoSize = True
            Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblTimeNow.ForeColor = System.Drawing.Color.White
            Me.LblTimeNow.Location = New System.Drawing.Point(547, 16)
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
            Me.LblCurrPGId.Location = New System.Drawing.Point(4, 4)
            Me.LblCurrPGId.Name = "LblCurrPGId"
            Me.LblCurrPGId.Size = New System.Drawing.Size(113, 13)
            Me.LblCurrPGId.TabIndex = 54
            Me.LblCurrPGId.Text = "PG-ID：XXXXXXXX"
            '
            'Button1
            '
            Me.Button1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Button1.Location = New System.Drawing.Point(3, 3)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(149, 24)
            Me.Button1.TabIndex = 54
            Me.Button1.Text = "アプリケーション終了"
            Me.ToolTip.SetToolTip(Me.Button1, "新試作手配システムを終了します。")
            Me.Button1.UseVisualStyleBackColor = True
            '
            'Panel2
            '
            Me.Panel2.AutoSize = True
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.LOGO)
            Me.Panel2.Controls.Add(Me.btnBACK)
            Me.Panel2.Controls.Add(Me.Button1)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(3, 35)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(615, 33)
            Me.Panel2.TabIndex = 55
            '
            'LOGO
            '
            Me.LOGO.BackColor = System.Drawing.Color.White
            Me.LOGO.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LOGO.ForeColor = System.Drawing.Color.Yellow
            Me.LOGO.Image = Global.EventSakusei.My.Resources.Resources.ロゴ
            Me.LOGO.Location = New System.Drawing.Point(218, 3)
            Me.LOGO.Name = "LOGO"
            Me.LOGO.Size = New System.Drawing.Size(136, 26)
            Me.LOGO.TabIndex = 83
            Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnBACK
            '
            Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBACK.Location = New System.Drawing.Point(547, 4)
            Me.btnBACK.Name = "btnBACK"
            Me.btnBACK.Size = New System.Drawing.Size(64, 24)
            Me.btnBACK.TabIndex = 55
            Me.btnBACK.Text = "戻る"
            Me.btnBACK.UseVisualStyleBackColor = True
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.BtnExcelExport)
            Me.GroupBox1.Controls.Add(Me.dtpSyochiShimekiribi)
            Me.GroupBox1.Controls.Add(Me.lblStatus)
            Me.GroupBox1.Controls.Add(Me.lblShimekiribi)
            Me.GroupBox1.Controls.Add(Me.Label11)
            Me.GroupBox1.Controls.Add(Me.BtnSashimodosi)
            Me.GroupBox1.Controls.Add(Me.BtnChushi)
            Me.GroupBox1.Controls.Add(Me.BtnKanryou)
            Me.GroupBox1.Controls.Add(Me.BtnShimekiri)
            Me.GroupBox1.Controls.Add(Me.BtnSekkeiTenkai)
            Me.GroupBox1.Controls.Add(Me.Label8)
            Me.GroupBox1.Controls.Add(Me.Label7)
            Me.GroupBox1.Location = New System.Drawing.Point(114, 145)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(381, 218)
            Me.GroupBox1.TabIndex = 1
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "イベント状況"
            '
            'BtnExcelExport
            '
            Me.BtnExcelExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnExcelExport.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnExcelExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnExcelExport.Location = New System.Drawing.Point(265, 168)
            Me.BtnExcelExport.Name = "BtnExcelExport"
            Me.BtnExcelExport.Size = New System.Drawing.Size(110, 44)
            Me.BtnExcelExport.TabIndex = 6
            Me.BtnExcelExport.Text = "設計処置状況EXCEL出力"
            Me.ToolTip.SetToolTip(Me.BtnExcelExport, "設計処置状況を「EXCEL」へ出力します。")
            Me.BtnExcelExport.UseVisualStyleBackColor = True
            '
            'dtpSyochiShimekiribi
            '
            Me.dtpSyochiShimekiribi.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.dtpSyochiShimekiribi.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.dtpSyochiShimekiribi.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpSyochiShimekiribi.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.dtpSyochiShimekiribi.Location = New System.Drawing.Point(28, 37)
            Me.dtpSyochiShimekiribi.Name = "dtpSyochiShimekiribi"
            Me.dtpSyochiShimekiribi.Size = New System.Drawing.Size(112, 22)
            Me.dtpSyochiShimekiribi.TabIndex = 0
            '
            'lblStatus
            '
            Me.lblStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblStatus.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblStatus.ForeColor = System.Drawing.Color.Black
            Me.lblStatus.Location = New System.Drawing.Point(224, 12)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(151, 30)
            Me.lblStatus.TabIndex = 71
            Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblShimekiribi
            '
            Me.lblShimekiribi.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.lblShimekiribi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblShimekiribi.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblShimekiribi.ForeColor = System.Drawing.Color.Black
            Me.lblShimekiribi.Location = New System.Drawing.Point(281, 109)
            Me.lblShimekiribi.Name = "lblShimekiribi"
            Me.lblShimekiribi.Size = New System.Drawing.Size(94, 22)
            Me.lblShimekiribi.TabIndex = 70
            Me.lblShimekiribi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label11.Location = New System.Drawing.Point(182, 12)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(45, 15)
            Me.Label11.TabIndex = 69
            Me.Label11.Text = "現在："
            '
            'BtnSashimodosi
            '
            Me.BtnSashimodosi.BackColor = System.Drawing.Color.LightCyan
            Me.BtnSashimodosi.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnSashimodosi.Location = New System.Drawing.Point(224, 65)
            Me.BtnSashimodosi.Name = "BtnSashimodosi"
            Me.BtnSashimodosi.Size = New System.Drawing.Size(130, 33)
            Me.BtnSashimodosi.TabIndex = 2
            Me.BtnSashimodosi.Text = "差戻し"
            Me.ToolTip.SetToolTip(Me.BtnSashimodosi, "展開中の情報を破棄し、展開前の状態に戻します。")
            Me.BtnSashimodosi.UseVisualStyleBackColor = False
            '
            'BtnChushi
            '
            Me.BtnChushi.BackColor = System.Drawing.Color.LightCyan
            Me.BtnChushi.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnChushi.Location = New System.Drawing.Point(26, 179)
            Me.BtnChushi.Name = "BtnChushi"
            Me.BtnChushi.Size = New System.Drawing.Size(192, 33)
            Me.BtnChushi.TabIndex = 5
            Me.BtnChushi.Text = "中止"
            Me.ToolTip.SetToolTip(Me.BtnChushi, "本イベントを中止します。")
            Me.BtnChushi.UseVisualStyleBackColor = False
            '
            'BtnKanryou
            '
            Me.BtnKanryou.BackColor = System.Drawing.Color.LightCyan
            Me.BtnKanryou.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnKanryou.Location = New System.Drawing.Point(26, 143)
            Me.BtnKanryou.Name = "BtnKanryou"
            Me.BtnKanryou.Size = New System.Drawing.Size(192, 33)
            Me.BtnKanryou.TabIndex = 4
            Me.BtnKanryou.Text = "完了"
            Me.ToolTip.SetToolTip(Me.BtnKanryou, "イベントを完了させ、改訂受付を終了します。")
            Me.BtnKanryou.UseVisualStyleBackColor = False
            '
            'BtnShimekiri
            '
            Me.BtnShimekiri.BackColor = System.Drawing.Color.LightCyan
            Me.BtnShimekiri.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnShimekiri.Location = New System.Drawing.Point(26, 104)
            Me.BtnShimekiri.Name = "BtnShimekiri"
            Me.BtnShimekiri.Size = New System.Drawing.Size(192, 33)
            Me.BtnShimekiri.TabIndex = 3
            Me.BtnShimekiri.Text = "設計処置〆切り（改訂受付）"
            Me.ToolTip.SetToolTip(Me.BtnShimekiri, "イニシャルの訂正処置を〆切り、改訂を受付けます。")
            Me.BtnShimekiri.UseVisualStyleBackColor = False
            '
            'BtnSekkeiTenkai
            '
            Me.BtnSekkeiTenkai.BackColor = System.Drawing.Color.LightCyan
            Me.BtnSekkeiTenkai.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnSekkeiTenkai.ForeColor = System.Drawing.Color.Black
            Me.BtnSekkeiTenkai.Location = New System.Drawing.Point(26, 65)
            Me.BtnSekkeiTenkai.Name = "BtnSekkeiTenkai"
            Me.BtnSekkeiTenkai.Size = New System.Drawing.Size(192, 33)
            Me.BtnSekkeiTenkai.TabIndex = 1
            Me.BtnSekkeiTenkai.Text = "設計展開"
            Me.ToolTip.SetToolTip(Me.BtnSekkeiTenkai, "設計メンテを開始します。")
            Me.BtnSekkeiTenkai.UseVisualStyleBackColor = False
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label8.Location = New System.Drawing.Point(14, 19)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(120, 15)
            Me.Label8.TabIndex = 64
            Me.Label8.Text = "設計処置〆切日："
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label7.Location = New System.Drawing.Point(224, 113)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(60, 15)
            Me.Label7.TabIndex = 64
            Me.Label7.Text = "〆切日："
            '
            'Panel3
            '
            Me.Panel3.AutoSize = True
            Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel3.BackColor = System.Drawing.SystemColors.Control
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Controls.Add(Me.LblMessage)
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel3.Location = New System.Drawing.Point(3, 612)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(615, 24)
            Me.Panel3.TabIndex = 67
            '
            'LblMessage
            '
            Me.LblMessage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.LblMessage.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.LblMessage.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblMessage.ForeColor = System.Drawing.Color.Red
            Me.LblMessage.Location = New System.Drawing.Point(0, 0)
            Me.LblMessage.Name = "LblMessage"
            Me.LblMessage.Size = New System.Drawing.Size(613, 22)
            Me.LblMessage.TabIndex = 55
            Me.LblMessage.Text = "「イベント情報登録・編集」ボタンをクリックしてください。"
            Me.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'BtnTehaichoSakusei
            '
            Me.BtnTehaichoSakusei.BackColor = System.Drawing.Color.PaleGreen
            Me.BtnTehaichoSakusei.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnTehaichoSakusei.Location = New System.Drawing.Point(108, 0)
            Me.BtnTehaichoSakusei.Name = "BtnTehaichoSakusei"
            Me.BtnTehaichoSakusei.Size = New System.Drawing.Size(381, 33)
            Me.BtnTehaichoSakusei.TabIndex = 0
            Me.BtnTehaichoSakusei.Text = "手配帳作成"
            Me.ToolTip.SetToolTip(Me.BtnTehaichoSakusei, "新しく手配帳を作成します。")
            Me.BtnTehaichoSakusei.UseVisualStyleBackColor = False
            '
            'BtnDelete
            '
            Me.BtnDelete.BackColor = System.Drawing.Color.HotPink
            Me.BtnDelete.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnDelete.Location = New System.Drawing.Point(247, 42)
            Me.BtnDelete.Name = "BtnDelete"
            Me.BtnDelete.Size = New System.Drawing.Size(79, 24)
            Me.BtnDelete.TabIndex = 2
            Me.BtnDelete.Text = "削除"
            Me.ToolTip.SetToolTip(Me.BtnDelete, "選択中の手配帳を削除します。")
            Me.BtnDelete.UseVisualStyleBackColor = False
            '
            'Panel6
            '
            Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel6.Controls.Add(Me.rbMsgOff)
            Me.Panel6.Controls.Add(Me.rbMsgOn)
            Me.Panel6.Location = New System.Drawing.Point(11, 17)
            Me.Panel6.Name = "Panel6"
            Me.Panel6.Size = New System.Drawing.Size(95, 20)
            Me.Panel6.TabIndex = 0
            Me.ToolTip.SetToolTip(Me.Panel6, "ONを選択すると「試作部品表編集画面」にメッセージを表示します。")
            '
            'rbMsgOff
            '
            Me.rbMsgOff.AutoSize = True
            Me.rbMsgOff.Location = New System.Drawing.Point(49, 3)
            Me.rbMsgOff.Name = "rbMsgOff"
            Me.rbMsgOff.Size = New System.Drawing.Size(45, 16)
            Me.rbMsgOff.TabIndex = 1
            Me.rbMsgOff.Text = "OFF"
            Me.ToolTip.SetToolTip(Me.rbMsgOff, "OFFを選択すると「試作部品表編集画面」にアラートを表示しません。")
            Me.rbMsgOff.UseVisualStyleBackColor = True
            '
            'rbMsgOn
            '
            Me.rbMsgOn.AutoSize = True
            Me.rbMsgOn.Checked = True
            Me.rbMsgOn.Location = New System.Drawing.Point(6, 3)
            Me.rbMsgOn.Name = "rbMsgOn"
            Me.rbMsgOn.Size = New System.Drawing.Size(39, 16)
            Me.rbMsgOn.TabIndex = 0
            Me.rbMsgOn.TabStop = True
            Me.rbMsgOn.Text = "ON"
            Me.ToolTip.SetToolTip(Me.rbMsgOn, "ONを選択すると「試作部品表編集画面」にアラートを表示します。")
            Me.rbMsgOn.UseVisualStyleBackColor = True
            '
            'Panel7
            '
            Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel7.Controls.Add(Me.rbMsgIkan)
            Me.Panel7.Controls.Add(Me.rbMsgFull)
            Me.Panel7.Location = New System.Drawing.Point(12, 43)
            Me.Panel7.Name = "Panel7"
            Me.Panel7.Size = New System.Drawing.Size(95, 40)
            Me.Panel7.TabIndex = 1
            Me.ToolTip.SetToolTip(Me.Panel7, "表示するアラートを選択してください。")
            '
            'rbMsgIkan
            '
            Me.rbMsgIkan.AutoSize = True
            Me.rbMsgIkan.Location = New System.Drawing.Point(6, 21)
            Me.rbMsgIkan.Name = "rbMsgIkan"
            Me.rbMsgIkan.Size = New System.Drawing.Size(83, 16)
            Me.rbMsgIkan.TabIndex = 1
            Me.rbMsgIkan.Text = "移管車改修"
            Me.ToolTip.SetToolTip(Me.rbMsgIkan, "表示するアラートを選択してください。")
            Me.rbMsgIkan.UseVisualStyleBackColor = True
            '
            'rbMsgFull
            '
            Me.rbMsgFull.AutoSize = True
            Me.rbMsgFull.Checked = True
            Me.rbMsgFull.Location = New System.Drawing.Point(6, 3)
            Me.rbMsgFull.Name = "rbMsgFull"
            Me.rbMsgFull.Size = New System.Drawing.Size(53, 16)
            Me.rbMsgFull.TabIndex = 0
            Me.rbMsgFull.TabStop = True
            Me.rbMsgFull.Text = "フル組"
            Me.ToolTip.SetToolTip(Me.rbMsgFull, "表示するアラートを選択してください。")
            Me.rbMsgFull.UseVisualStyleBackColor = True
            '
            'Panel9
            '
            Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel9.Controls.Add(Me.rbMailOff)
            Me.Panel9.Controls.Add(Me.rbMailOn)
            Me.Panel9.Location = New System.Drawing.Point(11, 17)
            Me.Panel9.Name = "Panel9"
            Me.Panel9.Size = New System.Drawing.Size(103, 20)
            Me.Panel9.TabIndex = 0
            Me.ToolTip.SetToolTip(Me.Panel9, "ONを選択すると「試作部品表編集画面」にメッセージを表示します。")
            '
            'rbMailOff
            '
            Me.rbMailOff.AutoSize = True
            Me.rbMailOff.Location = New System.Drawing.Point(49, 3)
            Me.rbMailOff.Name = "rbMailOff"
            Me.rbMailOff.Size = New System.Drawing.Size(52, 16)
            Me.rbMailOff.TabIndex = 1
            Me.rbMailOff.Text = "しない"
            Me.ToolTip.SetToolTip(Me.rbMailOff, "OFFを選択すると「試作部品表編集画面」にアラートを表示しません。")
            Me.rbMailOff.UseVisualStyleBackColor = True
            '
            'rbMailOn
            '
            Me.rbMailOn.AutoSize = True
            Me.rbMailOn.Checked = True
            Me.rbMailOn.Location = New System.Drawing.Point(6, 3)
            Me.rbMailOn.Name = "rbMailOn"
            Me.rbMailOn.Size = New System.Drawing.Size(42, 16)
            Me.rbMailOn.TabIndex = 0
            Me.rbMailOn.TabStop = True
            Me.rbMailOn.Text = "する"
            Me.ToolTip.SetToolTip(Me.rbMailOn, "ONを選択すると「試作部品表編集画面」にアラートを表示します。")
            Me.rbMailOn.UseVisualStyleBackColor = True
            '
            'Panel8
            '
            Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel8.Controls.Add(Me.rbTsukurikataOn)
            Me.Panel8.Controls.Add(Me.rbTsukurikataOff)
            Me.Panel8.Location = New System.Drawing.Point(12, 17)
            Me.Panel8.Name = "Panel8"
            Me.Panel8.Size = New System.Drawing.Size(95, 40)
            Me.Panel8.TabIndex = 1
            Me.ToolTip.SetToolTip(Me.Panel8, "表示するを選択すると「試作部品表編集画面」に作り方を表示します。")
            '
            'rbTsukurikataOn
            '
            Me.rbTsukurikataOn.AutoSize = True
            Me.rbTsukurikataOn.Location = New System.Drawing.Point(6, 21)
            Me.rbTsukurikataOn.Name = "rbTsukurikataOn"
            Me.rbTsukurikataOn.Size = New System.Drawing.Size(66, 16)
            Me.rbTsukurikataOn.TabIndex = 1
            Me.rbTsukurikataOn.Text = "表示する"
            Me.ToolTip.SetToolTip(Me.rbTsukurikataOn, "表示するを選択すると「試作部品表編集画面」に作り方を表示します。")
            Me.rbTsukurikataOn.UseVisualStyleBackColor = True
            '
            'rbTsukurikataOff
            '
            Me.rbTsukurikataOff.AutoSize = True
            Me.rbTsukurikataOff.Checked = True
            Me.rbTsukurikataOff.Location = New System.Drawing.Point(6, 3)
            Me.rbTsukurikataOff.Name = "rbTsukurikataOff"
            Me.rbTsukurikataOff.Size = New System.Drawing.Size(76, 16)
            Me.rbTsukurikataOff.TabIndex = 0
            Me.rbTsukurikataOff.TabStop = True
            Me.rbTsukurikataOff.Text = "表示しない"
            Me.ToolTip.SetToolTip(Me.rbTsukurikataOff, "表示するを選択すると「試作部品表編集画面」に作り方を表示します。")
            Me.rbTsukurikataOff.UseVisualStyleBackColor = True
            '
            'Panel10
            '
            Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel10.Controls.Add(Me.rbTsukurikataTenkaiOn)
            Me.Panel10.Controls.Add(Me.rbTsukurikataTenkaiOff)
            Me.Panel10.Location = New System.Drawing.Point(12, 63)
            Me.Panel10.Name = "Panel10"
            Me.Panel10.Size = New System.Drawing.Size(95, 40)
            Me.Panel10.TabIndex = 1
            Me.ToolTip.SetToolTip(Me.Panel10, "展開するを選択すると設計展開でベース車から作り方を展開します。")
            '
            'rbTsukurikataTenkaiOn
            '
            Me.rbTsukurikataTenkaiOn.AutoSize = True
            Me.rbTsukurikataTenkaiOn.Location = New System.Drawing.Point(6, 21)
            Me.rbTsukurikataTenkaiOn.Name = "rbTsukurikataTenkaiOn"
            Me.rbTsukurikataTenkaiOn.Size = New System.Drawing.Size(66, 16)
            Me.rbTsukurikataTenkaiOn.TabIndex = 1
            Me.rbTsukurikataTenkaiOn.Text = "展開する"
            Me.ToolTip.SetToolTip(Me.rbTsukurikataTenkaiOn, "展開するを選択すると設計展開でベース車から作り方を展開します。")
            Me.rbTsukurikataTenkaiOn.UseVisualStyleBackColor = True
            '
            'rbTsukurikataTenkaiOff
            '
            Me.rbTsukurikataTenkaiOff.AutoSize = True
            Me.rbTsukurikataTenkaiOff.Checked = True
            Me.rbTsukurikataTenkaiOff.Location = New System.Drawing.Point(6, 3)
            Me.rbTsukurikataTenkaiOff.Name = "rbTsukurikataTenkaiOff"
            Me.rbTsukurikataTenkaiOff.Size = New System.Drawing.Size(76, 16)
            Me.rbTsukurikataTenkaiOff.TabIndex = 0
            Me.rbTsukurikataTenkaiOff.TabStop = True
            Me.rbTsukurikataTenkaiOff.Text = "展開しない"
            Me.ToolTip.SetToolTip(Me.rbTsukurikataTenkaiOff, "展開するを選択すると設計展開でベース車から作り方を展開します。")
            Me.rbTsukurikataTenkaiOff.UseVisualStyleBackColor = True
            '
            'Panel11
            '
            Me.Panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel11.Controls.Add(Me.rbEbomKanshiOff)
            Me.Panel11.Controls.Add(Me.rbEbomKanshiOn)
            Me.Panel11.Location = New System.Drawing.Point(11, 17)
            Me.Panel11.Name = "Panel11"
            Me.Panel11.Size = New System.Drawing.Size(103, 20)
            Me.Panel11.TabIndex = 0
            Me.ToolTip.SetToolTip(Me.Panel11, "するを選択すると「EBOM設変監視」を開始します。")
            '
            'rbEbomKanshiOff
            '
            Me.rbEbomKanshiOff.AutoSize = True
            Me.rbEbomKanshiOff.Checked = True
            Me.rbEbomKanshiOff.Location = New System.Drawing.Point(49, 3)
            Me.rbEbomKanshiOff.Name = "rbEbomKanshiOff"
            Me.rbEbomKanshiOff.Size = New System.Drawing.Size(52, 16)
            Me.rbEbomKanshiOff.TabIndex = 1
            Me.rbEbomKanshiOff.TabStop = True
            Me.rbEbomKanshiOff.Text = "しない"
            Me.ToolTip.SetToolTip(Me.rbEbomKanshiOff, "するを選択すると「EBOM設変監視」を開始します。")
            Me.rbEbomKanshiOff.UseVisualStyleBackColor = True
            '
            'rbEbomKanshiOn
            '
            Me.rbEbomKanshiOn.AutoSize = True
            Me.rbEbomKanshiOn.Location = New System.Drawing.Point(6, 3)
            Me.rbEbomKanshiOn.Name = "rbEbomKanshiOn"
            Me.rbEbomKanshiOn.Size = New System.Drawing.Size(42, 16)
            Me.rbEbomKanshiOn.TabIndex = 0
            Me.rbEbomKanshiOn.Text = "する"
            Me.ToolTip.SetToolTip(Me.rbEbomKanshiOn, "するを選択すると「EBOM設変監視」を開始します。")
            Me.rbEbomKanshiOn.UseVisualStyleBackColor = True
            '
            'lblEventName
            '
            Me.lblEventName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblEventName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblEventName.ForeColor = System.Drawing.Color.Black
            Me.lblEventName.Location = New System.Drawing.Point(234, 78)
            Me.lblEventName.Name = "lblEventName"
            Me.lblEventName.Size = New System.Drawing.Size(275, 22)
            Me.lblEventName.TabIndex = 68
            Me.lblEventName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'lblEventCode
            '
            Me.lblEventCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblEventCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblEventCode.ForeColor = System.Drawing.Color.Black
            Me.lblEventCode.Location = New System.Drawing.Point(128, 78)
            Me.lblEventCode.Name = "lblEventCode"
            Me.lblEventCode.Size = New System.Drawing.Size(100, 22)
            Me.lblEventCode.TabIndex = 69
            Me.lblEventCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Panel4
            '
            Me.Panel4.Controls.Add(Me.BtnEventRegister)
            Me.Panel4.Location = New System.Drawing.Point(0, 106)
            Me.Panel4.Name = "Panel4"
            Me.Panel4.Size = New System.Drawing.Size(618, 38)
            Me.Panel4.TabIndex = 0
            '
            'BtnEventRegister
            '
            Me.BtnEventRegister.BackColor = System.Drawing.Color.PaleGreen
            Me.BtnEventRegister.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnEventRegister.Location = New System.Drawing.Point(112, 0)
            Me.BtnEventRegister.Name = "BtnEventRegister"
            Me.BtnEventRegister.Size = New System.Drawing.Size(381, 33)
            Me.BtnEventRegister.TabIndex = 0
            Me.BtnEventRegister.Text = "イベント情報登録・編集"
            Me.BtnEventRegister.UseVisualStyleBackColor = False
            '
            'Panel5
            '
            Me.Panel5.Controls.Add(Me.gbEbomKanshi)
            Me.Panel5.Controls.Add(Me.BtnTehaichoEdit)
            Me.Panel5.Controls.Add(Me.Label9)
            Me.Panel5.Controls.Add(Me.txtListCode)
            Me.Panel5.Controls.Add(Me.BtnTehaichoSakusei)
            Me.Panel5.Controls.Add(Me.spdParts)
            Me.Panel5.Controls.Add(Me.BtnDelete)
            Me.Panel5.Location = New System.Drawing.Point(6, 369)
            Me.Panel5.Name = "Panel5"
            Me.Panel5.Size = New System.Drawing.Size(624, 237)
            Me.Panel5.TabIndex = 2
            '
            'gbEbomKanshi
            '
            Me.gbEbomKanshi.Controls.Add(Me.Panel11)
            Me.gbEbomKanshi.Location = New System.Drawing.Point(492, 31)
            Me.gbEbomKanshi.Name = "gbEbomKanshi"
            Me.gbEbomKanshi.Size = New System.Drawing.Size(120, 45)
            Me.gbEbomKanshi.TabIndex = 72
            Me.gbEbomKanshi.TabStop = False
            Me.gbEbomKanshi.Text = "EBOM設変監視"
            Me.gbEbomKanshi.Visible = False
            '
            'BtnTehaichoEdit
            '
            Me.BtnTehaichoEdit.BackColor = System.Drawing.Color.PaleGreen
            Me.BtnTehaichoEdit.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnTehaichoEdit.Location = New System.Drawing.Point(108, 197)
            Me.BtnTehaichoEdit.Name = "BtnTehaichoEdit"
            Me.BtnTehaichoEdit.Size = New System.Drawing.Size(381, 33)
            Me.BtnTehaichoEdit.TabIndex = 4
            Me.BtnTehaichoEdit.Text = "手配帳編集"
            Me.BtnTehaichoEdit.UseVisualStyleBackColor = False
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label9.Location = New System.Drawing.Point(23, 47)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(79, 15)
            Me.Label9.TabIndex = 71
            Me.Label9.Text = "リストコード："
            '
            'txtListCode
            '
            Me.txtListCode.BackColor = System.Drawing.SystemColors.Control
            Me.txtListCode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtListCode.ForeColor = System.Drawing.Color.Navy
            Me.txtListCode.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.txtListCode.Location = New System.Drawing.Point(105, 44)
            Me.txtListCode.MaxLength = 15
            Me.txtListCode.Name = "txtListCode"
            Me.txtListCode.ReadOnly = True
            Me.txtListCode.Size = New System.Drawing.Size(136, 22)
            Me.txtListCode.TabIndex = 1
            Me.txtListCode.TabStop = False
            '
            'spdParts
            '
            Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, "
            Me.spdParts.AllowColumnMove = True
            Me.spdParts.AllowDragDrop = True
            Me.spdParts.AllowDragFill = True
            Me.spdParts.AllowUserFormulas = True
            Me.spdParts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.spdParts.BackColor = System.Drawing.SystemColors.Control
            Me.spdParts.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.Location = New System.Drawing.Point(0, 78)
            Me.spdParts.Name = "spdParts"
            Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
            Me.spdParts.Size = New System.Drawing.Size(612, 119)
            Me.spdParts.TabIndex = 3
            Me.spdParts.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdParts_Sheet1
            '
            Me.spdParts_Sheet1.Reset()
            Me.spdParts_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdParts_Sheet1.ColumnCount = 11
            Me.spdParts_Sheet1.RowCount = 10
            Me.spdParts_Sheet1.AllowNoteEdit = True
            Me.spdParts_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdParts_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdParts_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(0, 0).Locked = True
            Me.spdParts_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Cells.Get(0, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(0, 1).Value = "MF1-D-T01-RJY01"
            Me.spdParts_Sheet1.Cells.Get(0, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(0, 2).Value = "A"
            Me.spdParts_Sheet1.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(0, 3).Locked = True
            Me.spdParts_Sheet1.Cells.Get(0, 3).Value = "RJY"
            Me.spdParts_Sheet1.Cells.Get(0, 4).Locked = True
            Me.spdParts_Sheet1.Cells.Get(0, 4).Value = "MF1台車Ａグループ"
            Me.spdParts_Sheet1.Cells.Get(0, 5).Locked = True
            Me.spdParts_Sheet1.Cells.Get(0, 5).Value = "10+9"
            Me.spdParts_Sheet1.Cells.Get(0, 6).Locked = True
            Me.spdParts_Sheet1.Cells.Get(0, 6).ParseFormatInfo = CType(cultureInfo.NumberFormat.Clone, System.Globalization.NumberFormatInfo)
            CType(Me.spdParts_Sheet1.Cells.Get(0, 6).ParseFormatInfo, System.Globalization.NumberFormatInfo).NumberDecimalDigits = 0
            Me.spdParts_Sheet1.Cells.Get(0, 6).ParseFormatString = "n"
            Me.spdParts_Sheet1.Cells.Get(0, 6).Value = 2
            Me.spdParts_Sheet1.Cells.Get(0, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(0, 10).Locked = False
            Me.spdParts_Sheet1.Cells.Get(1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(1, 0).Locked = True
            Me.spdParts_Sheet1.Cells.Get(1, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(1, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(1, 3).Locked = True
            Me.spdParts_Sheet1.Cells.Get(1, 4).Locked = True
            Me.spdParts_Sheet1.Cells.Get(1, 5).Locked = True
            Me.spdParts_Sheet1.Cells.Get(1, 6).Locked = True
            Me.spdParts_Sheet1.Cells.Get(1, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(2, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(2, 0).Locked = True
            Me.spdParts_Sheet1.Cells.Get(2, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(2, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(2, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(2, 3).Locked = True
            Me.spdParts_Sheet1.Cells.Get(2, 4).Locked = True
            Me.spdParts_Sheet1.Cells.Get(2, 5).Locked = True
            Me.spdParts_Sheet1.Cells.Get(2, 6).Locked = True
            Me.spdParts_Sheet1.Cells.Get(2, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(3, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(3, 0).Locked = True
            Me.spdParts_Sheet1.Cells.Get(3, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(3, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(3, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(3, 3).Locked = True
            Me.spdParts_Sheet1.Cells.Get(3, 4).Locked = True
            Me.spdParts_Sheet1.Cells.Get(3, 5).Locked = True
            Me.spdParts_Sheet1.Cells.Get(3, 6).Locked = True
            Me.spdParts_Sheet1.Cells.Get(3, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(4, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(4, 0).Locked = True
            Me.spdParts_Sheet1.Cells.Get(4, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(4, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(4, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(4, 3).Locked = True
            Me.spdParts_Sheet1.Cells.Get(4, 4).Locked = True
            Me.spdParts_Sheet1.Cells.Get(4, 5).Locked = True
            Me.spdParts_Sheet1.Cells.Get(4, 6).Locked = True
            Me.spdParts_Sheet1.Cells.Get(4, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(5, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(5, 0).Locked = True
            Me.spdParts_Sheet1.Cells.Get(5, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(5, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(5, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(5, 3).Locked = True
            Me.spdParts_Sheet1.Cells.Get(5, 4).Locked = True
            Me.spdParts_Sheet1.Cells.Get(5, 5).Locked = True
            Me.spdParts_Sheet1.Cells.Get(5, 6).Locked = True
            Me.spdParts_Sheet1.Cells.Get(5, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(6, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(6, 0).Locked = True
            Me.spdParts_Sheet1.Cells.Get(6, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(6, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(6, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(6, 3).Locked = True
            Me.spdParts_Sheet1.Cells.Get(6, 4).Locked = True
            Me.spdParts_Sheet1.Cells.Get(6, 5).Locked = True
            Me.spdParts_Sheet1.Cells.Get(6, 6).Locked = True
            Me.spdParts_Sheet1.Cells.Get(6, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(7, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(7, 0).Locked = True
            Me.spdParts_Sheet1.Cells.Get(7, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(7, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(7, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(7, 3).Locked = True
            Me.spdParts_Sheet1.Cells.Get(7, 4).Locked = True
            Me.spdParts_Sheet1.Cells.Get(7, 5).Locked = True
            Me.spdParts_Sheet1.Cells.Get(7, 6).Locked = True
            Me.spdParts_Sheet1.Cells.Get(7, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Cells.Get(8, 0).Locked = True
            Me.spdParts_Sheet1.Cells.Get(8, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(8, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(8, 3).Locked = True
            Me.spdParts_Sheet1.Cells.Get(8, 4).Locked = True
            Me.spdParts_Sheet1.Cells.Get(8, 5).Locked = True
            Me.spdParts_Sheet1.Cells.Get(8, 6).Locked = True
            Me.spdParts_Sheet1.Cells.Get(9, 0).Locked = True
            Me.spdParts_Sheet1.Cells.Get(9, 1).Locked = True
            Me.spdParts_Sheet1.Cells.Get(9, 2).Locked = True
            Me.spdParts_Sheet1.Cells.Get(9, 3).Locked = True
            Me.spdParts_Sheet1.Cells.Get(9, 4).Locked = True
            Me.spdParts_Sheet1.Cells.Get(9, 5).Locked = True
            Me.spdParts_Sheet1.Cells.Get(9, 6).Locked = True
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "発注実績"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "リストコード"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "グループ"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "工事指令№"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "イベント名称"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "台数"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "改訂"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).ForeColor = System.Drawing.SystemColors.ControlText
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).Locked = False
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "自給品"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "比較"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "集計"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "メモ欄"
            Me.spdParts_Sheet1.ColumnHeader.Rows.Get(0).Height = 18.0!
            Me.spdParts_Sheet1.Columns.Get(0).CellType = TextCellType1
            Me.spdParts_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(0).Label = "発注実績"
            Me.spdParts_Sheet1.Columns.Get(0).Locked = False
            Me.spdParts_Sheet1.Columns.Get(0).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(0).Tag = "UPDATED_DATETIME"
            Me.spdParts_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(1).CellType = TextCellType2
            Me.spdParts_Sheet1.Columns.Get(1).Label = "リストコード"
            Me.spdParts_Sheet1.Columns.Get(1).Locked = False
            Me.spdParts_Sheet1.Columns.Get(1).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(1).Tag = "BUHIN_NO"
            Me.spdParts_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(1).Width = 101.0!
            Me.spdParts_Sheet1.Columns.Get(2).CellType = TextCellType3
            Me.spdParts_Sheet1.Columns.Get(2).Label = "グループ"
            Me.spdParts_Sheet1.Columns.Get(2).Locked = False
            Me.spdParts_Sheet1.Columns.Get(2).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(2).Tag = "HOJO_NAME"
            Me.spdParts_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(2).Width = 64.0!
            Me.spdParts_Sheet1.Columns.Get(3).CellType = TextCellType4
            Me.spdParts_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(3).Label = "工事指令№"
            Me.spdParts_Sheet1.Columns.Get(3).Locked = False
            Me.spdParts_Sheet1.Columns.Get(3).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(3).Tag = "UPDATED_DATETIME"
            Me.spdParts_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(3).Width = 80.0!
            Me.spdParts_Sheet1.Columns.Get(4).Label = "イベント名称"
            Me.spdParts_Sheet1.Columns.Get(4).Locked = False
            Me.spdParts_Sheet1.Columns.Get(4).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(4).Width = 157.0!
            Me.spdParts_Sheet1.Columns.Get(5).Label = "台数"
            Me.spdParts_Sheet1.Columns.Get(5).Locked = False
            Me.spdParts_Sheet1.Columns.Get(5).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(5).Width = 51.0!
            Me.spdParts_Sheet1.Columns.Get(6).Label = "改訂"
            Me.spdParts_Sheet1.Columns.Get(6).Locked = False
            Me.spdParts_Sheet1.Columns.Get(6).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(6).Width = 38.0!
            TextCellType5.MaxLength = 20
            Me.spdParts_Sheet1.Columns.Get(10).CellType = TextCellType5
            Me.spdParts_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(10).Label = "メモ欄"
            Me.spdParts_Sheet1.Columns.Get(10).Locked = True
            Me.spdParts_Sheet1.Columns.Get(10).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(10).Tag = "UPDATED_DATETIME"
            Me.spdParts_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(10).Width = 256.0!
            Me.spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdParts_Sheet1.Rows.Default.Height = 16.0!
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'Timer1
            '
            Me.Timer1.Enabled = True
            Me.Timer1.Interval = 1000
            '
            'gbBlockMsg
            '
            Me.gbBlockMsg.Controls.Add(Me.Panel7)
            Me.gbBlockMsg.Controls.Add(Me.Panel6)
            Me.gbBlockMsg.Location = New System.Drawing.Point(498, 145)
            Me.gbBlockMsg.Name = "gbBlockMsg"
            Me.gbBlockMsg.Size = New System.Drawing.Size(120, 90)
            Me.gbBlockMsg.TabIndex = 70
            Me.gbBlockMsg.TabStop = False
            Me.gbBlockMsg.Text = "ブロックアラート"
            '
            'gbOshirase
            '
            Me.gbOshirase.Controls.Add(Me.Panel9)
            Me.gbOshirase.Location = New System.Drawing.Point(498, 357)
            Me.gbOshirase.Name = "gbOshirase"
            Me.gbOshirase.Size = New System.Drawing.Size(120, 45)
            Me.gbOshirase.TabIndex = 71
            Me.gbOshirase.TabStop = False
            Me.gbOshirase.Text = "お知らせ通知"
            Me.gbOshirase.Visible = False
            '
            'gbTukurikata
            '
            Me.gbTukurikata.Controls.Add(Me.Panel10)
            Me.gbTukurikata.Controls.Add(Me.Panel8)
            Me.gbTukurikata.Location = New System.Drawing.Point(498, 241)
            Me.gbTukurikata.Name = "gbTukurikata"
            Me.gbTukurikata.Size = New System.Drawing.Size(120, 110)
            Me.gbTukurikata.TabIndex = 72
            Me.gbTukurikata.TabStop = False
            Me.gbTukurikata.Text = "作り方"
            '
            'Frm8DispShisakuBuhinMenu
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(621, 639)
            Me.Controls.Add(Me.gbTukurikata)
            Me.Controls.Add(Me.gbOshirase)
            Me.Controls.Add(Me.gbBlockMsg)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.lblBuhinNo)
            Me.Controls.Add(Me.lblEventName)
            Me.Controls.Add(Me.lblEventCode)
            Me.Controls.Add(Me.Panel4)
            Me.Controls.Add(Me.Panel5)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "Frm8DispShisakuBuhinMenu"
            Me.Padding = New System.Windows.Forms.Padding(3)
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.Panel3.ResumeLayout(False)
            Me.Panel6.ResumeLayout(False)
            Me.Panel6.PerformLayout()
            Me.Panel7.ResumeLayout(False)
            Me.Panel7.PerformLayout()
            Me.Panel9.ResumeLayout(False)
            Me.Panel9.PerformLayout()
            Me.Panel8.ResumeLayout(False)
            Me.Panel8.PerformLayout()
            Me.Panel10.ResumeLayout(False)
            Me.Panel10.PerformLayout()
            Me.Panel11.ResumeLayout(False)
            Me.Panel11.PerformLayout()
            Me.Panel4.ResumeLayout(False)
            Me.Panel5.ResumeLayout(False)
            Me.Panel5.PerformLayout()
            Me.gbEbomKanshi.ResumeLayout(False)
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.gbBlockMsg.ResumeLayout(False)
            Me.gbOshirase.ResumeLayout(False)
            Me.gbTukurikata.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents lblBuhinNo As System.Windows.Forms.Label
        Friend WithEvents Label38 As System.Windows.Forms.Label
        Friend WithEvents Label39 As System.Windows.Forms.Label
        Friend WithEvents Label40 As System.Windows.Forms.Label
        Friend WithEvents Label41 As System.Windows.Forms.Label
        Friend WithEvents Label42 As System.Windows.Forms.Label
        Friend WithEvents Label43 As System.Windows.Forms.Label
        Friend WithEvents Label44 As System.Windows.Forms.Label
        Friend WithEvents Label45 As System.Windows.Forms.Label
        Friend WithEvents Label46 As System.Windows.Forms.Label
        Friend WithEvents Label47 As System.Windows.Forms.Label
        Friend WithEvents Label48 As System.Windows.Forms.Label
        Friend WithEvents Label49 As System.Windows.Forms.Label
        Friend WithEvents Label50 As System.Windows.Forms.Label
        Friend WithEvents Label51 As System.Windows.Forms.Label
        Friend WithEvents Label52 As System.Windows.Forms.Label
        Friend WithEvents Label53 As System.Windows.Forms.Label
        Friend WithEvents Label54 As System.Windows.Forms.Label
        Friend WithEvents Label55 As System.Windows.Forms.Label
        Friend WithEvents Label56 As System.Windows.Forms.Label
        Friend WithEvents Label57 As System.Windows.Forms.Label
        Friend WithEvents Label58 As System.Windows.Forms.Label
        Friend WithEvents Label59 As System.Windows.Forms.Label
        Friend WithEvents Label60 As System.Windows.Forms.Label
        Friend WithEvents Label61 As System.Windows.Forms.Label
        Friend WithEvents Label62 As System.Windows.Forms.Label
        Friend WithEvents Label63 As System.Windows.Forms.Label
        Friend WithEvents Label64 As System.Windows.Forms.Label
        Friend WithEvents Label65 As System.Windows.Forms.Label
        Friend WithEvents Label66 As System.Windows.Forms.Label
        Friend WithEvents Label67 As System.Windows.Forms.Label
        Friend WithEvents Label68 As System.Windows.Forms.Label
        Friend WithEvents Label69 As System.Windows.Forms.Label
        Friend WithEvents Label70 As System.Windows.Forms.Label
        Friend WithEvents Label71 As System.Windows.Forms.Label
        Friend WithEvents Label72 As System.Windows.Forms.Label
        Friend WithEvents Label73 As System.Windows.Forms.Label
        Friend WithEvents Label74 As System.Windows.Forms.Label
        Friend WithEvents Label75 As System.Windows.Forms.Label
        Friend WithEvents Label76 As System.Windows.Forms.Label
        Friend WithEvents Label77 As System.Windows.Forms.Label
        Friend WithEvents Label78 As System.Windows.Forms.Label
        Friend WithEvents Label79 As System.Windows.Forms.Label
        Friend WithEvents Label80 As System.Windows.Forms.Label
        Friend WithEvents Label81 As System.Windows.Forms.Label
        Friend WithEvents Label82 As System.Windows.Forms.Label
        Friend WithEvents Label83 As System.Windows.Forms.Label
        Friend WithEvents Label84 As System.Windows.Forms.Label
        Friend WithEvents Label85 As System.Windows.Forms.Label
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents LblCurrPGId As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
        Friend WithEvents LblDateNow As System.Windows.Forms.Label
        Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
        Friend WithEvents LblTimeNow As System.Windows.Forms.Label
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents btnBACK As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents BtnShimekiri As System.Windows.Forms.Button
        Friend WithEvents BtnSekkeiTenkai As System.Windows.Forms.Button
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents BtnSashimodosi As System.Windows.Forms.Button
        Friend WithEvents BtnChushi As System.Windows.Forms.Button
        Friend WithEvents BtnKanryou As System.Windows.Forms.Button
        Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents LblMessage As System.Windows.Forms.Label
        Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents LOGO As System.Windows.Forms.Label
        Friend WithEvents Label11 As System.Windows.Forms.Label
        Friend WithEvents lblEventName As System.Windows.Forms.Label
        Friend WithEvents lblShimekiribi As System.Windows.Forms.Label
        Friend WithEvents lblEventCode As System.Windows.Forms.Label
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents dtpSyochiShimekiribi As System.Windows.Forms.DateTimePicker
        Friend WithEvents BtnExcelExport As System.Windows.Forms.Button
        Friend WithEvents Panel4 As System.Windows.Forms.Panel
        Friend WithEvents BtnEventRegister As System.Windows.Forms.Button
        Friend WithEvents Panel5 As System.Windows.Forms.Panel
        Friend WithEvents BtnTehaichoEdit As System.Windows.Forms.Button
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents txtListCode As System.Windows.Forms.TextBox
        Friend WithEvents BtnTehaichoSakusei As System.Windows.Forms.Button
        Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents BtnDelete As System.Windows.Forms.Button
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents gbBlockMsg As System.Windows.Forms.GroupBox
        Friend WithEvents Panel6 As System.Windows.Forms.Panel
        Friend WithEvents rbMsgOff As System.Windows.Forms.RadioButton
        Friend WithEvents rbMsgOn As System.Windows.Forms.RadioButton
        Friend WithEvents Panel7 As System.Windows.Forms.Panel
        Friend WithEvents rbMsgIkan As System.Windows.Forms.RadioButton
        Friend WithEvents rbMsgFull As System.Windows.Forms.RadioButton
        Friend WithEvents gbOshirase As System.Windows.Forms.GroupBox
        Friend WithEvents Panel9 As System.Windows.Forms.Panel
        Friend WithEvents rbMailOff As System.Windows.Forms.RadioButton
        Friend WithEvents rbMailOn As System.Windows.Forms.RadioButton
        Friend WithEvents gbTukurikata As System.Windows.Forms.GroupBox
        Friend WithEvents Panel8 As System.Windows.Forms.Panel
        Friend WithEvents rbTsukurikataOn As System.Windows.Forms.RadioButton
        Friend WithEvents rbTsukurikataOff As System.Windows.Forms.RadioButton
        Friend WithEvents Panel10 As System.Windows.Forms.Panel
        Friend WithEvents rbTsukurikataTenkaiOn As System.Windows.Forms.RadioButton
        Friend WithEvents rbTsukurikataTenkaiOff As System.Windows.Forms.RadioButton
        Friend WithEvents gbEbomKanshi As System.Windows.Forms.GroupBox
        Friend WithEvents Panel11 As System.Windows.Forms.Panel
        Friend WithEvents rbEbomKanshiOff As System.Windows.Forms.RadioButton
        Friend WithEvents rbEbomKanshiOn As System.Windows.Forms.RadioButton

    End Class
End Namespace
