﻿Namespace YosanshoEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FrmInsertMonth
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInsertMonth))
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.Label1 = New System.Windows.Forms.Label
            Me.LblCurrPGName = New System.Windows.Forms.Label
            Me.btnRegister = New System.Windows.Forms.Button
            Me.btnBack = New System.Windows.Forms.Button
            Me.lblMark = New System.Windows.Forms.Label
            Me.Label3 = New System.Windows.Forms.Label
            Me.Label4 = New System.Windows.Forms.Label
            Me.cmbKs = New System.Windows.Forms.ComboBox
            Me.Label10 = New System.Windows.Forms.Label
            Me.txtYear = New System.Windows.Forms.TextBox
            Me.Panel1.SuspendLayout()
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
            Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(392, 36)
            Me.Panel1.TabIndex = 89
            '
            'Label1
            '
            Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.Yellow
            Me.Label1.Location = New System.Drawing.Point(69, 2)
            Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(255, 34)
            Me.Label1.TabIndex = 60
            Me.Label1.Text = "月挿入"
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'LblCurrPGName
            '
            Me.LblCurrPGName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrPGName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrPGName.ForeColor = System.Drawing.Color.Yellow
            Me.LblCurrPGName.Location = New System.Drawing.Point(433, 2)
            Me.LblCurrPGName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.LblCurrPGName.Name = "LblCurrPGName"
            Me.LblCurrPGName.Size = New System.Drawing.Size(0, 34)
            Me.LblCurrPGName.TabIndex = 54
            Me.LblCurrPGName.Text = "予算書イベント一覧"
            Me.LblCurrPGName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnRegister
            '
            Me.btnRegister.BackColor = System.Drawing.Color.PaleGreen
            Me.btnRegister.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnRegister.Location = New System.Drawing.Point(46, 181)
            Me.btnRegister.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.btnRegister.Name = "btnRegister"
            Me.btnRegister.Size = New System.Drawing.Size(105, 30)
            Me.btnRegister.TabIndex = 3
            Me.btnRegister.Text = "登録"
            Me.btnRegister.UseVisualStyleBackColor = False
            '
            'btnBack
            '
            Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBack.Location = New System.Drawing.Point(237, 181)
            Me.btnBack.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(105, 30)
            Me.btnBack.TabIndex = 4
            Me.btnBack.TabStop = False
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = True
            '
            'lblMark
            '
            Me.lblMark.BackColor = System.Drawing.Color.Yellow
            Me.lblMark.Cursor = System.Windows.Forms.Cursors.Default
            Me.lblMark.ForeColor = System.Drawing.Color.Red
            Me.lblMark.Location = New System.Drawing.Point(13, 70)
            Me.lblMark.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.lblMark.Name = "lblMark"
            Me.lblMark.Size = New System.Drawing.Size(147, 15)
            Me.lblMark.TabIndex = 95
            Me.lblMark.Text = "≪造り方・完成車≫"
            Me.lblMark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(79, 101)
            Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(45, 15)
            Me.Label3.TabIndex = 96
            Me.Label3.Text = "年　 ："
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(79, 142)
            Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(45, 15)
            Me.Label4.TabIndex = 97
            Me.Label4.Text = "期　 ："
            '
            'cmbKs
            '
            Me.cmbKs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbKs.FormattingEnabled = True
            Me.cmbKs.ImeMode = System.Windows.Forms.ImeMode.Off
            Me.cmbKs.Items.AddRange(New Object() {"", "上期", "下期"})
            Me.cmbKs.Location = New System.Drawing.Point(132, 139)
            Me.cmbKs.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.cmbKs.MaxLength = 4
            Me.cmbKs.Name = "cmbKs"
            Me.cmbKs.Size = New System.Drawing.Size(93, 23)
            Me.cmbKs.TabIndex = 2
            '
            'Label10
            '
            Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label10.ForeColor = System.Drawing.Color.Red
            Me.Label10.Location = New System.Drawing.Point(-4, 229)
            Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(395, 28)
            Me.Label10.TabIndex = 179
            Me.Label10.Text = "年と期を入力して登録ボタンをクリックしてください。"
            Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'txtYear
            '
            Me.txtYear.ImeMode = System.Windows.Forms.ImeMode.Off
            Me.txtYear.Location = New System.Drawing.Point(132, 98)
            Me.txtYear.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.txtYear.MaxLength = 4
            Me.txtYear.Name = "txtYear"
            Me.txtYear.Size = New System.Drawing.Size(93, 22)
            Me.txtYear.TabIndex = 1
            '
            'FrmYosanInsertMonth
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(392, 257)
            Me.Controls.Add(Me.txtYear)
            Me.Controls.Add(Me.Label10)
            Me.Controls.Add(Me.cmbKs)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.lblMark)
            Me.Controls.Add(Me.btnBack)
            Me.Controls.Add(Me.btnRegister)
            Me.Controls.Add(Me.Panel1)
            Me.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.Name = "FrmYosanInsertMonth"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents LblCurrPGName As System.Windows.Forms.Label
        Friend WithEvents btnRegister As System.Windows.Forms.Button
        Friend WithEvents btnBack As System.Windows.Forms.Button
        Friend WithEvents lblMark As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents cmbKs As System.Windows.Forms.ComboBox
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents txtYear As System.Windows.Forms.TextBox
    End Class
End Namespace