<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm41TourokuKakunin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm41TourokuKakunin))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblKakunin = New System.Windows.Forms.Label
        Me.lblKakunin2 = New System.Windows.Forms.Label
        Me.lblKakunin3 = New System.Windows.Forms.Label
        Me.txtKakunin3 = New System.Windows.Forms.TextBox
        Me.BtnClose = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.BackColor = System.Drawing.Color.Blue
        Me.Panel1.Controls.Add(Me.BtnClose)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(341, 31)
        Me.Panel1.TabIndex = 54
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Yellow
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(341, 31)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "確認事項"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblKakunin
        '
        Me.lblKakunin.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin.ForeColor = System.Drawing.Color.Red
        Me.lblKakunin.Location = New System.Drawing.Point(0, 39)
        Me.lblKakunin.Name = "lblKakunin"
        Me.lblKakunin.Size = New System.Drawing.Size(341, 20)
        Me.lblKakunin.TabIndex = 56
        Me.lblKakunin.Text = "本ブロックは以下の点にご注意ください！"
        Me.lblKakunin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblKakunin2
        '
        Me.lblKakunin2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin2.Location = New System.Drawing.Point(0, 64)
        Me.lblKakunin2.Name = "lblKakunin2"
        Me.lblKakunin2.Size = New System.Drawing.Size(296, 19)
        Me.lblKakunin2.TabIndex = 58
        Me.lblKakunin2.Text = "以下の内容をご確認ください。"
        Me.lblKakunin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblKakunin3
        '
        Me.lblKakunin3.BackColor = System.Drawing.Color.Yellow
        Me.lblKakunin3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKakunin3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin3.Location = New System.Drawing.Point(0, 64)
        Me.lblKakunin3.Name = "lblKakunin3"
        Me.lblKakunin3.Size = New System.Drawing.Size(296, 141)
        Me.lblKakunin3.TabIndex = 59
        Me.lblKakunin3.Text = "過去に以下の問題が出ています。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "・部品の員数過多。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "・部品の得意先間違い。"
        Me.lblKakunin3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtKakunin3
        '
        Me.txtKakunin3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtKakunin3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKakunin3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKakunin3.Location = New System.Drawing.Point(0, 64)
        Me.txtKakunin3.Multiline = True
        Me.txtKakunin3.Name = "txtKakunin3"
        Me.txtKakunin3.ReadOnly = True
        Me.txtKakunin3.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtKakunin3.Size = New System.Drawing.Size(341, 175)
        Me.txtKakunin3.TabIndex = 60
        Me.txtKakunin3.Text = "過去に以下の問題が出ています。"
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.BackColor = System.Drawing.Color.Silver
        Me.BtnClose.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnClose.Location = New System.Drawing.Point(275, 4)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(64, 24)
        Me.BtnClose.TabIndex = 61
        Me.BtnClose.Text = "閉じる"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'frm41TourokuKakunin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(341, 241)
        Me.Controls.Add(Me.txtKakunin3)
        Me.Controls.Add(Me.lblKakunin3)
        Me.Controls.Add(Me.lblKakunin2)
        Me.Controls.Add(Me.lblKakunin)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm41TourokuKakunin"
        Me.Text = "新試作手配システム Ver 1.00"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblKakunin As System.Windows.Forms.Label
    Friend WithEvents lblKakunin2 As System.Windows.Forms.Label
    Friend WithEvents lblKakunin3 As System.Windows.Forms.Label
    Friend WithEvents txtKakunin3 As System.Windows.Forms.TextBox
    Friend WithEvents BtnClose As System.Windows.Forms.Button
End Class
