Namespace YosanEventList
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FrmYosanCodeAndDateSelect
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmYosanCodeAndDateSelect))
            Me.lblYyyymm = New System.Windows.Forms.Label
            Me.btnImport = New System.Windows.Forms.Button
            Me.btnBack = New System.Windows.Forms.Button
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.Label1 = New System.Windows.Forms.Label
            Me.LblCurrPGName = New System.Windows.Forms.Label
            Me.txtYyyymm = New System.Windows.Forms.TextBox
            Me.Label2 = New System.Windows.Forms.Label
            Me.txtHireHi_Meta = New System.Windows.Forms.TextBox
            Me.Label3 = New System.Windows.Forms.Label
            Me.txtKoteHi_Trim = New System.Windows.Forms.TextBox
            Me.Label4 = New System.Windows.Forms.Label
            Me.txtHireHi_Kou = New System.Windows.Forms.TextBox
            Me.Label5 = New System.Windows.Forms.Label
            Me.txtHireHi_Yusou = New System.Windows.Forms.TextBox
            Me.Label6 = New System.Windows.Forms.Label
            Me.txtHireHi_Ikan = New System.Windows.Forms.TextBox
            Me.Label7 = New System.Windows.Forms.Label
            Me.txtKoteHi_Meta = New System.Windows.Forms.TextBox
            Me.Label8 = New System.Windows.Forms.Label
            Me.txtHireHi_Trim = New System.Windows.Forms.TextBox
            Me.Label9 = New System.Windows.Forms.Label
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'lblYyyymm
            '
            Me.lblYyyymm.AutoSize = True
            Me.lblYyyymm.Location = New System.Drawing.Point(19, 51)
            Me.lblYyyymm.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.lblYyyymm.Name = "lblYyyymm"
            Me.lblYyyymm.Size = New System.Drawing.Size(75, 15)
            Me.lblYyyymm.TabIndex = 0
            Me.lblYyyymm.Text = "計上年月："
            Me.lblYyyymm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'btnImport
            '
            Me.btnImport.Location = New System.Drawing.Point(13, 319)
            Me.btnImport.Margin = New System.Windows.Forms.Padding(4)
            Me.btnImport.Name = "btnImport"
            Me.btnImport.Size = New System.Drawing.Size(80, 27)
            Me.btnImport.TabIndex = 9
            Me.btnImport.Text = "取込"
            Me.btnImport.UseVisualStyleBackColor = True
            '
            'btnBack
            '
            Me.btnBack.Location = New System.Drawing.Point(419, 319)
            Me.btnBack.Margin = New System.Windows.Forms.Padding(4)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(80, 27)
            Me.btnBack.TabIndex = 10
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = True
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Controls.Add(Me.LblCurrPGName)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(513, 29)
            Me.Panel1.TabIndex = 88
            '
            'Label1
            '
            Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.Yellow
            Me.Label1.Location = New System.Drawing.Point(52, 2)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(410, 27)
            Me.Label1.TabIndex = 60
            Me.Label1.Text = "計上年月及び予算コードの選択"
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'LblCurrPGName
            '
            Me.LblCurrPGName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrPGName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrPGName.ForeColor = System.Drawing.Color.Yellow
            Me.LblCurrPGName.Location = New System.Drawing.Point(325, 2)
            Me.LblCurrPGName.Name = "LblCurrPGName"
            Me.LblCurrPGName.Size = New System.Drawing.Size(44, 27)
            Me.LblCurrPGName.TabIndex = 54
            Me.LblCurrPGName.Text = "予算書イベント一覧"
            Me.LblCurrPGName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'txtYyyymm
            '
            Me.txtYyyymm.ImeMode = System.Windows.Forms.ImeMode.Off
            Me.txtYyyymm.Location = New System.Drawing.Point(163, 48)
            Me.txtYyyymm.Margin = New System.Windows.Forms.Padding(4)
            Me.txtYyyymm.MaxLength = 7
            Me.txtYyyymm.Multiline = True
            Me.txtYyyymm.Name = "txtYyyymm"
            Me.txtYyyymm.Size = New System.Drawing.Size(100, 22)
            Me.txtYyyymm.TabIndex = 1
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(19, 81)
            Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(142, 15)
            Me.Label2.TabIndex = 0
            Me.Label2.Text = "比例費 メタル部品費："
            Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'txtHireHi_Meta
            '
            Me.txtHireHi_Meta.ImeMode = System.Windows.Forms.ImeMode.Off
            Me.txtHireHi_Meta.Location = New System.Drawing.Point(163, 78)
            Me.txtHireHi_Meta.Margin = New System.Windows.Forms.Padding(4)
            Me.txtHireHi_Meta.MaxLength = 100
            Me.txtHireHi_Meta.Name = "txtHireHi_Meta"
            Me.txtHireHi_Meta.Size = New System.Drawing.Size(300, 22)
            Me.txtHireHi_Meta.TabIndex = 2
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(18, 231)
            Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(127, 15)
            Me.Label3.TabIndex = 0
            Me.Label3.Text = "固定費 トリム型費："
            Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'txtKoteHi_Trim
            '
            Me.txtKoteHi_Trim.ImeMode = System.Windows.Forms.ImeMode.Off
            Me.txtKoteHi_Trim.Location = New System.Drawing.Point(162, 228)
            Me.txtKoteHi_Trim.Margin = New System.Windows.Forms.Padding(4)
            Me.txtKoteHi_Trim.MaxLength = 100
            Me.txtKoteHi_Trim.Name = "txtKoteHi_Trim"
            Me.txtKoteHi_Trim.Size = New System.Drawing.Size(300, 22)
            Me.txtKoteHi_Trim.TabIndex = 7
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
            Me.Label4.Location = New System.Drawing.Point(160, 288)
            Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(250, 12)
            Me.Label4.TabIndex = 0
            Me.Label4.Text = "※予算コードの末尾をカンマ(,)区切りで複数指定可"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'txtHireHi_Kou
            '
            Me.txtHireHi_Kou.ImeMode = System.Windows.Forms.ImeMode.Off
            Me.txtHireHi_Kou.Location = New System.Drawing.Point(163, 108)
            Me.txtHireHi_Kou.Margin = New System.Windows.Forms.Padding(4)
            Me.txtHireHi_Kou.MaxLength = 100
            Me.txtHireHi_Kou.Name = "txtHireHi_Kou"
            Me.txtHireHi_Kou.Size = New System.Drawing.Size(300, 22)
            Me.txtHireHi_Kou.TabIndex = 3
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(86, 111)
            Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(75, 15)
            Me.Label5.TabIndex = 89
            Me.Label5.Text = "鋼板材料："
            '
            'txtHireHi_Yusou
            '
            Me.txtHireHi_Yusou.ImeMode = System.Windows.Forms.ImeMode.Off
            Me.txtHireHi_Yusou.Location = New System.Drawing.Point(162, 138)
            Me.txtHireHi_Yusou.Margin = New System.Windows.Forms.Padding(4)
            Me.txtHireHi_Yusou.MaxLength = 100
            Me.txtHireHi_Yusou.Name = "txtHireHi_Yusou"
            Me.txtHireHi_Yusou.Size = New System.Drawing.Size(300, 22)
            Me.txtHireHi_Yusou.TabIndex = 4
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(86, 141)
            Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(60, 15)
            Me.Label6.TabIndex = 91
            Me.Label6.Text = "輸送費："
            '
            'txtHireHi_Ikan
            '
            Me.txtHireHi_Ikan.ImeMode = System.Windows.Forms.ImeMode.Off
            Me.txtHireHi_Ikan.Location = New System.Drawing.Point(162, 168)
            Me.txtHireHi_Ikan.Margin = New System.Windows.Forms.Padding(4)
            Me.txtHireHi_Ikan.MaxLength = 100
            Me.txtHireHi_Ikan.Name = "txtHireHi_Ikan"
            Me.txtHireHi_Ikan.Size = New System.Drawing.Size(300, 22)
            Me.txtHireHi_Ikan.TabIndex = 5
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(86, 171)
            Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(60, 15)
            Me.Label7.TabIndex = 93
            Me.Label7.Text = "移管車："
            '
            'txtKoteHi_Meta
            '
            Me.txtKoteHi_Meta.ImeMode = System.Windows.Forms.ImeMode.Off
            Me.txtKoteHi_Meta.Location = New System.Drawing.Point(161, 258)
            Me.txtKoteHi_Meta.Margin = New System.Windows.Forms.Padding(4)
            Me.txtKoteHi_Meta.MaxLength = 100
            Me.txtKoteHi_Meta.Name = "txtKoteHi_Meta"
            Me.txtKoteHi_Meta.Size = New System.Drawing.Size(300, 22)
            Me.txtKoteHi_Meta.TabIndex = 8
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Location = New System.Drawing.Point(23, 261)
            Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(123, 15)
            Me.Label8.TabIndex = 95
            Me.Label8.Text = "メタル型費（治具）："
            Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'txtHireHi_Trim
            '
            Me.txtHireHi_Trim.ImeMode = System.Windows.Forms.ImeMode.Off
            Me.txtHireHi_Trim.Location = New System.Drawing.Point(162, 198)
            Me.txtHireHi_Trim.Margin = New System.Windows.Forms.Padding(4)
            Me.txtHireHi_Trim.MaxLength = 100
            Me.txtHireHi_Trim.Name = "txtHireHi_Trim"
            Me.txtHireHi_Trim.Size = New System.Drawing.Size(300, 22)
            Me.txtHireHi_Trim.TabIndex = 6
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Location = New System.Drawing.Point(69, 201)
            Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(92, 15)
            Me.Label9.TabIndex = 97
            Me.Label9.Text = "トリム部品費："
            '
            'FrmYosanCodeAndDateSelect
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(513, 358)
            Me.ControlBox = False
            Me.Controls.Add(Me.txtHireHi_Trim)
            Me.Controls.Add(Me.Label9)
            Me.Controls.Add(Me.txtKoteHi_Meta)
            Me.Controls.Add(Me.Label8)
            Me.Controls.Add(Me.txtHireHi_Ikan)
            Me.Controls.Add(Me.Label7)
            Me.Controls.Add(Me.txtHireHi_Yusou)
            Me.Controls.Add(Me.Label6)
            Me.Controls.Add(Me.txtHireHi_Kou)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.btnBack)
            Me.Controls.Add(Me.btnImport)
            Me.Controls.Add(Me.txtKoteHi_Trim)
            Me.Controls.Add(Me.txtHireHi_Meta)
            Me.Controls.Add(Me.txtYyyymm)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.lblYyyymm)
            Me.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Margin = New System.Windows.Forms.Padding(4)
            Me.Name = "FrmYosanCodeAndDateSelect"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents lblYyyymm As System.Windows.Forms.Label
        Friend WithEvents btnImport As System.Windows.Forms.Button
        Friend WithEvents btnBack As System.Windows.Forms.Button
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents LblCurrPGName As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents txtYyyymm As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents txtHireHi_Meta As System.Windows.Forms.TextBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents txtKoteHi_Trim As System.Windows.Forms.TextBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents txtHireHi_Kou As System.Windows.Forms.TextBox
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents txtHireHi_Yusou As System.Windows.Forms.TextBox
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents txtHireHi_Ikan As System.Windows.Forms.TextBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents txtKoteHi_Meta As System.Windows.Forms.TextBox
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents txtHireHi_Trim As System.Windows.Forms.TextBox
        Friend WithEvents Label9 As System.Windows.Forms.Label
    End Class
End Namespace
