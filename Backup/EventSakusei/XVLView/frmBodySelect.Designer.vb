<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBodySelect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBodySelect))
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbBody = New System.Windows.Forms.ComboBox
        Me.cmbKaihatuFugo = New System.Windows.Forms.ComboBox
        Me.lblKaihatuFugo = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(94, 81)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(104, 28)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(204, 81)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(104, 28)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "BODY"
        '
        'cmbBody
        '
        Me.cmbBody.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBody.DropDownWidth = 214
        Me.cmbBody.FormattingEnabled = True
        Me.cmbBody.Location = New System.Drawing.Point(69, 46)
        Me.cmbBody.Name = "cmbBody"
        Me.cmbBody.Size = New System.Drawing.Size(241, 20)
        Me.cmbBody.Sorted = True
        Me.cmbBody.TabIndex = 3
        '
        'cmbKaihatuFugo
        '
        Me.cmbKaihatuFugo.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.cmbKaihatuFugo.FormattingEnabled = True
        Me.cmbKaihatuFugo.Location = New System.Drawing.Point(69, 13)
        Me.cmbKaihatuFugo.Name = "cmbKaihatuFugo"
        Me.cmbKaihatuFugo.Size = New System.Drawing.Size(63, 20)
        Me.cmbKaihatuFugo.TabIndex = 5
        '
        'lblKaihatuFugo
        '
        Me.lblKaihatuFugo.AutoSize = True
        Me.lblKaihatuFugo.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.lblKaihatuFugo.Location = New System.Drawing.Point(12, 17)
        Me.lblKaihatuFugo.Name = "lblKaihatuFugo"
        Me.lblKaihatuFugo.Size = New System.Drawing.Size(53, 12)
        Me.lblKaihatuFugo.TabIndex = 4
        Me.lblKaihatuFugo.Text = "開発符号"
        '
        'frmBodySelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(322, 115)
        Me.Controls.Add(Me.cmbKaihatuFugo)
        Me.Controls.Add(Me.lblKaihatuFugo)
        Me.Controls.Add(Me.cmbBody)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmBodySelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ボディー選択"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbBody As System.Windows.Forms.ComboBox
    Friend WithEvents cmbKaihatuFugo As System.Windows.Forms.ComboBox
    Friend WithEvents lblKaihatuFugo As System.Windows.Forms.Label
End Class
