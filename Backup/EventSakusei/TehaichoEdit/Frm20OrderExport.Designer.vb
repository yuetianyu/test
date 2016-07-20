<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm20OrderExport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm20OrderExport))
        Me.btnExport = New System.Windows.Forms.Button
        Me.txtBasho = New System.Windows.Forms.TextBox
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
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cmbTorihikisaki = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExport
        '
        Me.btnExport.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.btnExport.Location = New System.Drawing.Point(13, 121)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(84, 27)
        Me.btnExport.TabIndex = 4
        Me.btnExport.Text = "出力"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'txtBasho
        '
        Me.txtBasho.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtBasho.Location = New System.Drawing.Point(376, 48)
        Me.txtBasho.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBasho.MaxLength = 4
        Me.txtBasho.Name = "txtBasho"
        Me.txtBasho.Size = New System.Drawing.Size(150, 22)
        Me.txtBasho.TabIndex = 2
        '
        'cmbTantousha
        '
        Me.cmbTantousha.DropDownHeight = 150
        Me.cmbTantousha.FormattingEnabled = True
        Me.cmbTantousha.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.cmbTantousha.IntegralHeight = False
        Me.cmbTantousha.Location = New System.Drawing.Point(75, 84)
        Me.cmbTantousha.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTantousha.MaxLength = 20
        Me.cmbTantousha.Name = "cmbTantousha"
        Me.cmbTantousha.Size = New System.Drawing.Size(218, 23)
        Me.cmbTantousha.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(301, 52)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 15)
        Me.Label2.TabIndex = 176
        Me.Label2.Text = "納入場所："
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 87)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 15)
        Me.Label1.TabIndex = 175
        Me.Label1.Text = "担当者："
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
        Me.Panel2.Size = New System.Drawing.Size(644, 32)
        Me.Panel2.TabIndex = 174
        '
        'LblCurrBukaName
        '
        Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
        Me.LblCurrBukaName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblCurrBukaName.Location = New System.Drawing.Point(432, 19)
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
        Me.LblDateNow.Location = New System.Drawing.Point(554, 5)
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
        Me.LblCurrUserId.Location = New System.Drawing.Point(432, 5)
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
        Me.LblTimeNow.Location = New System.Drawing.Point(574, 19)
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
        Me.Label4.Location = New System.Drawing.Point(89, 3)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(389, 28)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "取引先の選択及び補足情報の入力画面"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBack.Location = New System.Drawing.Point(549, 46)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(84, 27)
        Me.btnBack.TabIndex = 5
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'cmbTorihikisaki
        '
        Me.cmbTorihikisaki.DropDownHeight = 150
        Me.cmbTorihikisaki.FormattingEnabled = True
        Me.cmbTorihikisaki.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.cmbTorihikisaki.IntegralHeight = False
        Me.cmbTorihikisaki.Location = New System.Drawing.Point(75, 47)
        Me.cmbTorihikisaki.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTorihikisaki.MaxLength = 30
        Me.cmbTorihikisaki.Name = "cmbTorihikisaki"
        Me.cmbTorihikisaki.Size = New System.Drawing.Size(218, 23)
        Me.cmbTorihikisaki.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 50)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 15)
        Me.Label3.TabIndex = 178
        Me.Label3.Text = "取引先："
        '
        'Frm20OrderExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 161)
        Me.Controls.Add(Me.cmbTorihikisaki)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.txtBasho)
        Me.Controls.Add(Me.cmbTantousha)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnBack)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Frm20OrderExport"
        Me.Text = "新試作手配システム Ver 1.00"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents txtBasho As System.Windows.Forms.TextBox
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
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents cmbTorihikisaki As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
