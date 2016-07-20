Namespace ShisakuBuhinEditList
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm35DispShisakuBuhinEditList
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
            Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm35DispShisakuBuhinEditList))
            Me.lblBuhinNo = New System.Windows.Forms.Label
            Me.txtIbentoNo = New System.Windows.Forms.TextBox
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
            Me.LblCurrPGName = New System.Windows.Forms.Label
            Me.LblCurrBukaName = New System.Windows.Forms.Label
            Me.LblDateNow = New System.Windows.Forms.Label
            Me.LblCurrUserId = New System.Windows.Forms.Label
            Me.LblTimeNow = New System.Windows.Forms.Label
            Me.LblCurrPGId = New System.Windows.Forms.Label
            Me.btnBack = New System.Windows.Forms.Button
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.LOGO = New System.Windows.Forms.Label
            Me.btnEND = New System.Windows.Forms.Button
            Me.btnCall = New System.Windows.Forms.Button
            Me.spdParts = New FarPoint.Win.Spread.FpSpread
            Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.lblMsg = New System.Windows.Forms.Label
            Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel3.SuspendLayout()
            Me.SuspendLayout()
            '
            'lblBuhinNo
            '
            Me.lblBuhinNo.AutoSize = True
            Me.lblBuhinNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblBuhinNo.Location = New System.Drawing.Point(3, 81)
            Me.lblBuhinNo.Name = "lblBuhinNo"
            Me.lblBuhinNo.Size = New System.Drawing.Size(93, 15)
            Me.lblBuhinNo.TabIndex = 8
            Me.lblBuhinNo.Text = "イベントコード："
            '
            'txtIbentoNo
            '
            Me.txtIbentoNo.BackColor = System.Drawing.Color.White
            Me.txtIbentoNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtIbentoNo.ForeColor = System.Drawing.Color.Navy
            Me.txtIbentoNo.Location = New System.Drawing.Point(98, 76)
            Me.txtIbentoNo.Name = "txtIbentoNo"
            Me.txtIbentoNo.ReadOnly = True
            Me.txtIbentoNo.Size = New System.Drawing.Size(100, 22)
            Me.txtIbentoNo.TabIndex = 7
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
            Me.Panel1.Controls.Add(Me.LblCurrPGName)
            Me.Panel1.Controls.Add(Me.LblCurrBukaName)
            Me.Panel1.Controls.Add(Me.LblDateNow)
            Me.Panel1.Controls.Add(Me.LblCurrUserId)
            Me.Panel1.Controls.Add(Me.LblTimeNow)
            Me.Panel1.Controls.Add(Me.LblCurrPGId)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(3, 3)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(995, 34)
            Me.Panel1.TabIndex = 53
            '
            'LblCurrPGName
            '
            Me.LblCurrPGName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrPGName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrPGName.ForeColor = System.Drawing.Color.Yellow
            Me.LblCurrPGName.Location = New System.Drawing.Point(324, 3)
            Me.LblCurrPGName.Name = "LblCurrPGName"
            Me.LblCurrPGName.Size = New System.Drawing.Size(278, 31)
            Me.LblCurrPGName.TabIndex = 54
            Me.LblCurrPGName.Text = "試作部品表　編集一覧"
            Me.LblCurrPGName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'LblCurrBukaName
            '
            Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
            Me.LblCurrBukaName.Location = New System.Drawing.Point(767, 17)
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
            Me.LblDateNow.Location = New System.Drawing.Point(903, 4)
            Me.LblDateNow.Name = "LblDateNow"
            Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
            Me.LblDateNow.TabIndex = 56
            Me.LblDateNow.Text = "YYYY/MM/DD"
            '
            'LblCurrUserId
            '
            Me.LblCurrUserId.AccessibleRole = System.Windows.Forms.AccessibleRole.None
            Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
            Me.LblCurrUserId.Location = New System.Drawing.Point(767, 4)
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
            Me.LblTimeNow.Location = New System.Drawing.Point(927, 17)
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
            'btnBack
            '
            Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBack.Location = New System.Drawing.Point(926, 3)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(64, 24)
            Me.btnBack.TabIndex = 54
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = True
            '
            'Panel2
            '
            Me.Panel2.AutoSize = True
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.LOGO)
            Me.Panel2.Controls.Add(Me.btnEND)
            Me.Panel2.Controls.Add(Me.btnBack)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(3, 37)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(995, 32)
            Me.Panel2.TabIndex = 55
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
            Me.LOGO.Location = New System.Drawing.Point(371, 2)
            Me.LOGO.Name = "LOGO"
            Me.LOGO.Size = New System.Drawing.Size(179, 26)
            Me.LOGO.TabIndex = 82
            Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnEND
            '
            Me.btnEND.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnEND.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnEND.Location = New System.Drawing.Point(3, 3)
            Me.btnEND.Name = "btnEND"
            Me.btnEND.Size = New System.Drawing.Size(149, 24)
            Me.btnEND.TabIndex = 55
            Me.btnEND.Text = "アプリケーション終了"
            Me.ToolTip.SetToolTip(Me.btnEND, "新試作手配システムを終了します。")
            Me.btnEND.UseVisualStyleBackColor = True
            '
            'btnCall
            '
            Me.btnCall.BackColor = System.Drawing.Color.LightCyan
            Me.btnCall.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnCall.Location = New System.Drawing.Point(204, 76)
            Me.btnCall.Name = "btnCall"
            Me.btnCall.Size = New System.Drawing.Size(79, 24)
            Me.btnCall.TabIndex = 8
            Me.btnCall.Text = "呼出し"
            Me.ToolTip.SetToolTip(Me.btnCall, "試作部品表を呼出します。")
            Me.btnCall.UseVisualStyleBackColor = False
            '
            'spdParts
            '
            Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, "
            Me.spdParts.AllowDragFill = True
            Me.spdParts.AllowUserZoom = False
            Me.spdParts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.spdParts.AutoClipboard = False
            Me.spdParts.BackColor = System.Drawing.SystemColors.Control
            Me.spdParts.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.Location = New System.Drawing.Point(6, 106)
            Me.spdParts.Name = "spdParts"
            Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
            Me.spdParts.Size = New System.Drawing.Size(989, 445)
            Me.spdParts.TabIndex = 9
            Me.spdParts.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdParts_Sheet1
            '
            Me.spdParts_Sheet1.Reset()
            Me.spdParts_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdParts_Sheet1.ColumnCount = 9
            Me.spdParts_Sheet1.RowCount = 0
            Me.spdParts_Sheet1.ActiveRowIndex = -1
            Me.spdParts_Sheet1.AllowNoteEdit = True
            Me.spdParts_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdParts_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdParts_Sheet1.AutoGenerateColumns = False
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "イベントコード"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "開発符号"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "イベント"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "ユニット区分"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "イベント名称"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "台数"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "訂正処置〆切日"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "アラーム"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "DIFF_DATE"
            Me.spdParts_Sheet1.ColumnHeader.Rows.Get(0).Height = 30.0!
            Me.spdParts_Sheet1.Columns.Get(0).CellType = TextCellType1
            Me.spdParts_Sheet1.Columns.Get(0).Label = "イベントコード"
            Me.spdParts_Sheet1.Columns.Get(0).Locked = True
            Me.spdParts_Sheet1.Columns.Get(0).Tag = "SHISAKU_EVENT_CODE"
            Me.spdParts_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(0).Width = 91.0!
            Me.spdParts_Sheet1.Columns.Get(1).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(1).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(1).CellType = TextCellType2
            Me.spdParts_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(1).Label = "開発符号"
            Me.spdParts_Sheet1.Columns.Get(1).Locked = True
            Me.spdParts_Sheet1.Columns.Get(1).Tag = "SHISAKU_KAIHATSU_FUGO"
            Me.spdParts_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(1).Width = 71.0!
            Me.spdParts_Sheet1.Columns.Get(2).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(2).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(2).CellType = TextCellType3
            Me.spdParts_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(2).Label = "イベント"
            Me.spdParts_Sheet1.Columns.Get(2).Locked = True
            Me.spdParts_Sheet1.Columns.Get(2).Tag = "SHISAKU_EVENT_PHASE_NAME"
            Me.spdParts_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(2).Width = 115.0!
            Me.spdParts_Sheet1.Columns.Get(3).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(3).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(3).CellType = TextCellType4
            Me.spdParts_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(3).Label = "ユニット区分"
            Me.spdParts_Sheet1.Columns.Get(3).Locked = True
            Me.spdParts_Sheet1.Columns.Get(3).Tag = "UNIT_KBN"
            Me.spdParts_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(3).Width = 88.0!
            Me.spdParts_Sheet1.Columns.Get(4).Label = "イベント名称"
            Me.spdParts_Sheet1.Columns.Get(4).Locked = True
            Me.spdParts_Sheet1.Columns.Get(4).Tag = "SHISAKU_EVENT_NAME"
            Me.spdParts_Sheet1.Columns.Get(4).Width = 225.0!
            Me.spdParts_Sheet1.Columns.Get(5).Label = "台数"
            Me.spdParts_Sheet1.Columns.Get(5).Locked = True
            Me.spdParts_Sheet1.Columns.Get(5).Tag = "DAISU"
            Me.spdParts_Sheet1.Columns.Get(5).Width = 51.0!
            Me.spdParts_Sheet1.Columns.Get(6).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(6).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(6).CellType = TextCellType5
            Me.spdParts_Sheet1.Columns.Get(6).Label = "訂正処置〆切日"
            Me.spdParts_Sheet1.Columns.Get(6).Locked = True
            Me.spdParts_Sheet1.Columns.Get(6).Tag = "DATE"
            Me.spdParts_Sheet1.Columns.Get(6).Width = 97.0!
            Me.spdParts_Sheet1.Columns.Get(7).Label = "アラーム"
            Me.spdParts_Sheet1.Columns.Get(7).Locked = True
            Me.spdParts_Sheet1.Columns.Get(7).Tag = "ALARM"
            Me.spdParts_Sheet1.Columns.Get(7).Width = 141.0!
            Me.spdParts_Sheet1.Columns.Get(8).Label = "DIFF_DATE"
            Me.spdParts_Sheet1.Columns.Get(8).Locked = True
            Me.spdParts_Sheet1.Columns.Get(8).Tag = "DIFF_DATE"
            Me.spdParts_Sheet1.Columns.Get(8).Width = 80.0!
            Me.spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdParts_Sheet1.Rows.Default.Height = 16.0!
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            Me.spdParts.SetActiveViewport(0, -1, 0)
            '
            'Panel3
            '
            Me.Panel3.AutoSize = True
            Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel3.BackColor = System.Drawing.SystemColors.Control
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Controls.Add(Me.lblMsg)
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel3.Location = New System.Drawing.Point(3, 557)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(995, 25)
            Me.Panel3.TabIndex = 60
            '
            'lblMsg
            '
            Me.lblMsg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblMsg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.lblMsg.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblMsg.ForeColor = System.Drawing.Color.Red
            Me.lblMsg.Location = New System.Drawing.Point(5, 1)
            Me.lblMsg.Name = "lblMsg"
            Me.lblMsg.Size = New System.Drawing.Size(986, 22)
            Me.lblMsg.TabIndex = 55
            Me.lblMsg.Text = "「イベント」を選択してください。"
            Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Timer1
            '
            Me.Timer1.Enabled = True
            Me.Timer1.Interval = 1000
            '
            'Frm35DispShisakuBuhinEditList
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(1001, 585)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.spdParts)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.btnCall)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.lblBuhinNo)
            Me.Controls.Add(Me.txtIbentoNo)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "Frm35DispShisakuBuhinEditList"
            Me.Padding = New System.Windows.Forms.Padding(3)
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel3.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents lblBuhinNo As System.Windows.Forms.Label
        Friend WithEvents txtIbentoNo As System.Windows.Forms.TextBox
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
        Friend WithEvents LblCurrPGName As System.Windows.Forms.Label
        Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
        Friend WithEvents LblDateNow As System.Windows.Forms.Label
        Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
        Friend WithEvents LblTimeNow As System.Windows.Forms.Label
        Friend WithEvents btnBack As System.Windows.Forms.Button
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents btnCall As System.Windows.Forms.Button
        Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents lblMsg As System.Windows.Forms.Label
        Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents btnEND As System.Windows.Forms.Button
        Friend WithEvents LOGO As System.Windows.Forms.Label
        Friend WithEvents Timer1 As System.Windows.Forms.Timer

    End Class
End Namespace