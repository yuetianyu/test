<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm01Kakunin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm01Kakunin))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.OK = New System.Windows.Forms.Button
        Me.lblKakunin = New System.Windows.Forms.Label
        Me.CANCEL = New System.Windows.Forms.Button
        Me.lblKakunin2 = New System.Windows.Forms.Label
        Me.cmbKaiteiNo = New System.Windows.Forms.ComboBox
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.BackColor = System.Drawing.Color.Blue
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(268, 31)
        Me.Panel1.TabIndex = 54
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Yellow
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(268, 31)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "確認"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'OK
        '
        Me.OK.Location = New System.Drawing.Point(53, 115)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 55
        Me.OK.Text = "ＯＫ"
        Me.OK.UseVisualStyleBackColor = True
        '
        'lblKakunin
        '
        Me.lblKakunin.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin.ForeColor = System.Drawing.Color.Red
        Me.lblKakunin.Location = New System.Drawing.Point(0, 61)
        Me.lblKakunin.Name = "lblKakunin"
        Me.lblKakunin.Size = New System.Drawing.Size(268, 20)
        Me.lblKakunin.TabIndex = 56
        Me.lblKakunin.Text = "処理を続行して宜しいですか？"
        Me.lblKakunin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CANCEL
        '
        Me.CANCEL.Location = New System.Drawing.Point(140, 115)
        Me.CANCEL.Name = "CANCEL"
        Me.CANCEL.Size = New System.Drawing.Size(75, 23)
        Me.CANCEL.TabIndex = 57
        Me.CANCEL.Text = "CANCEL"
        Me.CANCEL.UseVisualStyleBackColor = True
        '
        'lblKakunin2
        '
        Me.lblKakunin2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin2.ForeColor = System.Drawing.Color.Black
        Me.lblKakunin2.Location = New System.Drawing.Point(0, 86)
        Me.lblKakunin2.Name = "lblKakunin2"
        Me.lblKakunin2.Size = New System.Drawing.Size(268, 19)
        Me.lblKakunin2.TabIndex = 58
        Me.lblKakunin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbKaiteiNo
        '
        Me.cmbKaiteiNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKaiteiNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKaiteiNo.FormattingEnabled = True
        Me.cmbKaiteiNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbKaiteiNo.Items.AddRange(New Object() {"", "SKE1", "設計"})
        Me.cmbKaiteiNo.Location = New System.Drawing.Point(105, 77)
        Me.cmbKaiteiNo.MaxLength = 2
        Me.cmbKaiteiNo.Name = "cmbKaiteiNo"
        Me.cmbKaiteiNo.Size = New System.Drawing.Size(58, 23)
        Me.cmbKaiteiNo.TabIndex = 59
        Me.cmbKaiteiNo.Visible = False
        '
        'frm01Kakunin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(268, 155)
        Me.Controls.Add(Me.cmbKaiteiNo)
        Me.Controls.Add(Me.lblKakunin2)
        Me.Controls.Add(Me.CANCEL)
        Me.Controls.Add(Me.lblKakunin)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm01Kakunin"
        Me.Text = "新試作手配システム Ver 1.00"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents lblKakunin As System.Windows.Forms.Label
    Friend WithEvents CANCEL As System.Windows.Forms.Button
    Friend WithEvents lblKakunin2 As System.Windows.Forms.Label
    Friend WithEvents cmbKaiteiNo As System.Windows.Forms.ComboBox
End Class
