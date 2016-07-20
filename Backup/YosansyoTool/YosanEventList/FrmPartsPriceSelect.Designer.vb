Namespace YosanEventList
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FrmPartsPriceSelect
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
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.Label1 = New System.Windows.Forms.Label
            Me.LblCurrPGName = New System.Windows.Forms.Label
            Me.btnBack = New System.Windows.Forms.Button
            Me.btnImport = New System.Windows.Forms.Button
            Me.lblYyyymm = New System.Windows.Forms.Label
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.rdoKaigai = New System.Windows.Forms.RadioButton
            Me.rdoKokunai = New System.Windows.Forms.RadioButton
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.SuspendLayout()
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
            Me.Panel1.Size = New System.Drawing.Size(257, 29)
            Me.Panel1.TabIndex = 93
            '
            'Label1
            '
            Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.Yellow
            Me.Label1.Location = New System.Drawing.Point(33, 2)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(192, 27)
            Me.Label1.TabIndex = 60
            Me.Label1.Text = "パーツプライス取込の選択"
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
            Me.LblCurrPGName.Size = New System.Drawing.Size(0, 27)
            Me.LblCurrPGName.TabIndex = 54
            Me.LblCurrPGName.Text = "予算書イベント一覧"
            Me.LblCurrPGName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnBack
            '
            Me.btnBack.Location = New System.Drawing.Point(163, 106)
            Me.btnBack.Margin = New System.Windows.Forms.Padding(4)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(80, 27)
            Me.btnBack.TabIndex = 92
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = True
            '
            'btnImport
            '
            Me.btnImport.Location = New System.Drawing.Point(13, 106)
            Me.btnImport.Margin = New System.Windows.Forms.Padding(4)
            Me.btnImport.Name = "btnImport"
            Me.btnImport.Size = New System.Drawing.Size(80, 27)
            Me.btnImport.TabIndex = 91
            Me.btnImport.Text = "取込"
            Me.btnImport.UseVisualStyleBackColor = True
            '
            'lblYyyymm
            '
            Me.lblYyyymm.AutoSize = True
            Me.lblYyyymm.Location = New System.Drawing.Point(28, 57)
            Me.lblYyyymm.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.lblYyyymm.Name = "lblYyyymm"
            Me.lblYyyymm.Size = New System.Drawing.Size(71, 12)
            Me.lblYyyymm.TabIndex = 89
            Me.lblYyyymm.Text = "国内／海外："
            Me.lblYyyymm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Panel2
            '
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.rdoKaigai)
            Me.Panel2.Controls.Add(Me.rdoKokunai)
            Me.Panel2.Location = New System.Drawing.Point(102, 47)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(123, 31)
            Me.Panel2.TabIndex = 94
            '
            'rdoKaigai
            '
            Me.rdoKaigai.AutoSize = True
            Me.rdoKaigai.Location = New System.Drawing.Point(70, 7)
            Me.rdoKaigai.Name = "rdoKaigai"
            Me.rdoKaigai.Size = New System.Drawing.Size(47, 16)
            Me.rdoKaigai.TabIndex = 0
            Me.rdoKaigai.TabStop = True
            Me.rdoKaigai.Text = "海外"
            Me.rdoKaigai.UseVisualStyleBackColor = True
            '
            'rdoKokunai
            '
            Me.rdoKokunai.AutoSize = True
            Me.rdoKokunai.Location = New System.Drawing.Point(4, 7)
            Me.rdoKokunai.Name = "rdoKokunai"
            Me.rdoKokunai.Size = New System.Drawing.Size(47, 16)
            Me.rdoKokunai.TabIndex = 0
            Me.rdoKokunai.TabStop = True
            Me.rdoKokunai.Text = "国内"
            Me.rdoKokunai.UseVisualStyleBackColor = True
            '
            'FrmPartsPriceSelect
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(257, 150)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.btnBack)
            Me.Controls.Add(Me.btnImport)
            Me.Controls.Add(Me.lblYyyymm)
            Me.Name = "FrmPartsPriceSelect"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents LblCurrPGName As System.Windows.Forms.Label
        Friend WithEvents btnBack As System.Windows.Forms.Button
        Friend WithEvents btnImport As System.Windows.Forms.Button
        Friend WithEvents lblYyyymm As System.Windows.Forms.Label
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents rdoKaigai As System.Windows.Forms.RadioButton
        Friend WithEvents rdoKokunai As System.Windows.Forms.RadioButton
    End Class
End Namespace
