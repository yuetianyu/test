<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm20SikyuuExport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm20SikyuuExport))
        Me.txtHosoku = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtContent = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtRiyuu = New System.Windows.Forms.TextBox
        Me.txtShisakuKaihatsuFugo = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtMakerName = New System.Windows.Forms.TextBox
        Me.txtDeptName = New System.Windows.Forms.TextBox
        Me.txtMakerCode = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbTorihikisakiCode = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.LblCurrBukaName = New System.Windows.Forms.Label
        Me.LblDateNow = New System.Windows.Forms.Label
        Me.LblCurrUserId = New System.Windows.Forms.Label
        Me.LblTimeNow = New System.Windows.Forms.Label
        Me.LblCurrPGId = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnBACK = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnExport = New System.Windows.Forms.Button
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtHosoku
        '
        Me.txtHosoku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHosoku.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtHosoku.Location = New System.Drawing.Point(603, 125)
        Me.txtHosoku.Margin = New System.Windows.Forms.Padding(4)
        Me.txtHosoku.MaxLength = 256
        Me.txtHosoku.Multiline = True
        Me.txtHosoku.Name = "txtHosoku"
        Me.txtHosoku.Size = New System.Drawing.Size(252, 48)
        Me.txtHosoku.TabIndex = 171
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.LightBlue
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Location = New System.Drawing.Point(603, 106)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(252, 20)
        Me.Label10.TabIndex = 184
        Me.Label10.Text = "補足情報（部品番号、文書No等）"
        '
        'txtContent
        '
        Me.txtContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContent.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtContent.Location = New System.Drawing.Point(314, 125)
        Me.txtContent.Margin = New System.Windows.Forms.Padding(4)
        Me.txtContent.MaxLength = 256
        Me.txtContent.Multiline = True
        Me.txtContent.Name = "txtContent"
        Me.txtContent.Size = New System.Drawing.Size(290, 48)
        Me.txtContent.TabIndex = 170
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.LightBlue
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Location = New System.Drawing.Point(314, 106)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(290, 20)
        Me.Label9.TabIndex = 182
        Me.Label9.Text = "件名"
        '
        'txtRiyuu
        '
        Me.txtRiyuu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRiyuu.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtRiyuu.Location = New System.Drawing.Point(86, 172)
        Me.txtRiyuu.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRiyuu.MaxLength = 256
        Me.txtRiyuu.Multiline = True
        Me.txtRiyuu.Name = "txtRiyuu"
        Me.txtRiyuu.Size = New System.Drawing.Size(769, 87)
        Me.txtRiyuu.TabIndex = 172
        '
        'txtShisakuKaihatsuFugo
        '
        Me.txtShisakuKaihatsuFugo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShisakuKaihatsuFugo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtShisakuKaihatsuFugo.Location = New System.Drawing.Point(86, 106)
        Me.txtShisakuKaihatsuFugo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtShisakuKaihatsuFugo.MaxLength = 4
        Me.txtShisakuKaihatsuFugo.Multiline = True
        Me.txtShisakuKaihatsuFugo.Name = "txtShisakuKaihatsuFugo"
        Me.txtShisakuKaihatsuFugo.Size = New System.Drawing.Size(133, 67)
        Me.txtShisakuKaihatsuFugo.TabIndex = 169
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.LightBlue
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Location = New System.Drawing.Point(218, 106)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 67)
        Me.Label8.TabIndex = 181
        Me.Label8.Text = "内容"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.LightBlue
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(7, 172)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 87)
        Me.Label7.TabIndex = 180
        Me.Label7.Text = "支給理由・使用目的"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.LightBlue
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.Label6.Location = New System.Drawing.Point(7, 106)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 67)
        Me.Label6.TabIndex = 179
        Me.Label6.Text = "開発コード/車種"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMakerName
        '
        Me.txtMakerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMakerName.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtMakerName.Location = New System.Drawing.Point(405, 71)
        Me.txtMakerName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMakerName.MaxLength = 20
        Me.txtMakerName.Name = "txtMakerName"
        Me.txtMakerName.Size = New System.Drawing.Size(199, 22)
        Me.txtMakerName.TabIndex = 168
        '
        'txtDeptName
        '
        Me.txtDeptName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeptName.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtDeptName.Location = New System.Drawing.Point(405, 50)
        Me.txtDeptName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDeptName.MaxLength = 20
        Me.txtDeptName.Name = "txtDeptName"
        Me.txtDeptName.Size = New System.Drawing.Size(199, 22)
        Me.txtDeptName.TabIndex = 167
        '
        'txtMakerCode
        '
        Me.txtMakerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMakerCode.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtMakerCode.Location = New System.Drawing.Point(97, 74)
        Me.txtMakerCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMakerCode.MaxLength = 4
        Me.txtMakerCode.Name = "txtMakerCode"
        Me.txtMakerCode.Size = New System.Drawing.Size(122, 22)
        Me.txtMakerCode.TabIndex = 166
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(318, 75)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 15)
        Me.Label5.TabIndex = 178
        Me.Label5.Text = "担当者名："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(318, 52)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 15)
        Me.Label3.TabIndex = 177
        Me.Label3.Text = "担当部署："
        '
        'cmbTorihikisakiCode
        '
        Me.cmbTorihikisakiCode.DropDownHeight = 150
        Me.cmbTorihikisakiCode.FormattingEnabled = True
        Me.cmbTorihikisakiCode.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.cmbTorihikisakiCode.IntegralHeight = False
        Me.cmbTorihikisakiCode.Location = New System.Drawing.Point(97, 49)
        Me.cmbTorihikisakiCode.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTorihikisakiCode.MaxLength = 30
        Me.cmbTorihikisakiCode.Name = "cmbTorihikisakiCode"
        Me.cmbTorihikisakiCode.Size = New System.Drawing.Size(218, 23)
        Me.cmbTorihikisakiCode.TabIndex = 165
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 78)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 15)
        Me.Label2.TabIndex = 176
        Me.Label2.Text = "メーカーコード："
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 52)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 15)
        Me.Label1.TabIndex = 175
        Me.Label1.Text = "取引先："
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
        Me.Panel2.Size = New System.Drawing.Size(863, 32)
        Me.Panel2.TabIndex = 174
        '
        'LblCurrBukaName
        '
        Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
        Me.LblCurrBukaName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblCurrBukaName.Location = New System.Drawing.Point(641, 17)
        Me.LblCurrBukaName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblCurrBukaName.Name = "LblCurrBukaName"
        Me.LblCurrBukaName.Size = New System.Drawing.Size(147, 15)
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
        Me.LblDateNow.Location = New System.Drawing.Point(772, 2)
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
        Me.LblCurrUserId.Location = New System.Drawing.Point(641, 2)
        Me.LblCurrUserId.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblCurrUserId.Name = "LblCurrUserId"
        Me.LblCurrUserId.Size = New System.Drawing.Size(147, 17)
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
        Me.LblTimeNow.Location = New System.Drawing.Point(792, 18)
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
        Me.Label4.Location = New System.Drawing.Point(235, 0)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(369, 30)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "支給先の選択及び補足情報の入力画面"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBACK
        '
        Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBACK.Location = New System.Drawing.Point(771, 63)
        Me.btnBACK.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBACK.Name = "btnBACK"
        Me.btnBACK.Size = New System.Drawing.Size(84, 27)
        Me.btnBACK.TabIndex = 173
        Me.btnBACK.Text = "戻る"
        Me.btnBACK.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(7, 271)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(84, 27)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "出力"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'Frm20SikyuuExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(863, 309)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.txtHosoku)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtContent)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtRiyuu)
        Me.Controls.Add(Me.txtShisakuKaihatsuFugo)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtMakerName)
        Me.Controls.Add(Me.txtDeptName)
        Me.Controls.Add(Me.txtMakerCode)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbTorihikisakiCode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnBACK)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Frm20SikyuuExport"
        Me.Text = "新試作手配システム Ver 1.00"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtHosoku As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtContent As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtRiyuu As System.Windows.Forms.TextBox
    Friend WithEvents txtShisakuKaihatsuFugo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtMakerName As System.Windows.Forms.TextBox
    Friend WithEvents txtDeptName As System.Windows.Forms.TextBox
    Friend WithEvents txtMakerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbTorihikisakiCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnBACK As System.Windows.Forms.Button
    Friend WithEvents LblCurrPGId As System.Windows.Forms.Label
    Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
    Friend WithEvents LblDateNow As System.Windows.Forms.Label
    Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
    Friend WithEvents LblTimeNow As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnExport As System.Windows.Forms.Button
End Class
