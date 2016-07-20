Namespace ShisakuBuhinSakuseiList

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FrmDispEditModeSelect
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDispEditModeSelect))
            Me.btnOk = New System.Windows.Forms.Button
            Me.btnCancel = New System.Windows.Forms.Button
            Me.lblMsg = New System.Windows.Forms.Label
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.rdoBtnYosan = New System.Windows.Forms.RadioButton
            Me.rdoBtnTehai = New System.Windows.Forms.RadioButton
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'btnOk
            '
            Me.btnOk.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
            Me.btnOk.Location = New System.Drawing.Point(24, 106)
            Me.btnOk.Margin = New System.Windows.Forms.Padding(4)
            Me.btnOk.Name = "btnOk"
            Me.btnOk.Size = New System.Drawing.Size(84, 27)
            Me.btnOk.TabIndex = 11
            Me.btnOk.Text = "OK"
            Me.btnOk.UseVisualStyleBackColor = True
            '
            'btnCancel
            '
            Me.btnCancel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnCancel.Location = New System.Drawing.Point(124, 106)
            Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(84, 27)
            Me.btnCancel.TabIndex = 12
            Me.btnCancel.Text = "CANCEL"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'lblMsg
            '
            Me.lblMsg.AutoSize = True
            Me.lblMsg.Location = New System.Drawing.Point(21, 9)
            Me.lblMsg.Name = "lblMsg"
            Me.lblMsg.Size = New System.Drawing.Size(187, 15)
            Me.lblMsg.TabIndex = 182
            Me.lblMsg.Text = "編集モードを選択してください。"
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.rdoBtnYosan)
            Me.Panel1.Controls.Add(Me.rdoBtnTehai)
            Me.Panel1.Location = New System.Drawing.Point(43, 37)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(152, 53)
            Me.Panel1.TabIndex = 183
            '
            'rdoBtnYosan
            '
            Me.rdoBtnYosan.AutoSize = True
            Me.rdoBtnYosan.Location = New System.Drawing.Point(4, 28)
            Me.rdoBtnYosan.Name = "rdoBtnYosan"
            Me.rdoBtnYosan.Size = New System.Drawing.Size(119, 19)
            Me.rdoBtnYosan.TabIndex = 5
            Me.rdoBtnYosan.Text = "予算担当モード"
            Me.rdoBtnYosan.UseVisualStyleBackColor = True
            '
            'rdoBtnTehai
            '
            Me.rdoBtnTehai.AutoSize = True
            Me.rdoBtnTehai.Location = New System.Drawing.Point(4, 3)
            Me.rdoBtnTehai.Name = "rdoBtnTehai"
            Me.rdoBtnTehai.Size = New System.Drawing.Size(119, 19)
            Me.rdoBtnTehai.TabIndex = 4
            Me.rdoBtnTehai.Text = "手配担当モード"
            Me.rdoBtnTehai.UseVisualStyleBackColor = True
            '
            'FrmDispEditModeSelect
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(235, 147)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.lblMsg)
            Me.Controls.Add(Me.btnOk)
            Me.Controls.Add(Me.btnCancel)
            Me.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Margin = New System.Windows.Forms.Padding(4)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FrmDispEditModeSelect"
            Me.TopMost = True
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents btnOk As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents lblMsg As System.Windows.Forms.Label
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents rdoBtnYosan As System.Windows.Forms.RadioButton
        Friend WithEvents rdoBtnTehai As System.Windows.Forms.RadioButton
    End Class

End Namespace
