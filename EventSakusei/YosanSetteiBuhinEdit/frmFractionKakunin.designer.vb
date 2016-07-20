Namespace YosanSetteiBuhinEdit

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmFractionKakunin
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFractionKakunin))
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.lblHEAD = New System.Windows.Forms.Label
            Me.btnOK = New System.Windows.Forms.Button
            Me.lblKakunin = New System.Windows.Forms.Label
            Me.btnCancel = New System.Windows.Forms.Button
            Me.btnIgnoring = New System.Windows.Forms.Button
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.lblHEAD)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(287, 32)
            Me.Panel1.TabIndex = 54
            '
            'lblHEAD
            '
            Me.lblHEAD.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblHEAD.ForeColor = System.Drawing.Color.Yellow
            Me.lblHEAD.Location = New System.Drawing.Point(33, 1)
            Me.lblHEAD.Name = "lblHEAD"
            Me.lblHEAD.Size = New System.Drawing.Size(218, 31)
            Me.lblHEAD.TabIndex = 54
            Me.lblHEAD.Text = "確認"
            Me.lblHEAD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnOK
            '
            Me.btnOK.Location = New System.Drawing.Point(12, 83)
            Me.btnOK.Name = "btnOK"
            Me.btnOK.Size = New System.Drawing.Size(68, 23)
            Me.btnOK.TabIndex = 55
            Me.btnOK.Text = "OK"
            Me.btnOK.UseVisualStyleBackColor = True
            '
            'lblKakunin
            '
            Me.lblKakunin.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblKakunin.ForeColor = System.Drawing.Color.Red
            Me.lblKakunin.Location = New System.Drawing.Point(12, 36)
            Me.lblKakunin.Name = "lblKakunin"
            Me.lblKakunin.Size = New System.Drawing.Size(275, 33)
            Me.lblKakunin.TabIndex = 56
            Me.lblKakunin.Text = "処理結果で0になる行が存在します。"
            Me.lblKakunin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'btnCancel
            '
            Me.btnCancel.Location = New System.Drawing.Point(175, 83)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(100, 23)
            Me.btnCancel.TabIndex = 57
            Me.btnCancel.Text = "キャンセル"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'btnIgnoring
            '
            Me.btnIgnoring.Location = New System.Drawing.Point(94, 83)
            Me.btnIgnoring.Name = "btnIgnoring"
            Me.btnIgnoring.Size = New System.Drawing.Size(75, 23)
            Me.btnIgnoring.TabIndex = 58
            Me.btnIgnoring.Text = "0は無視"
            Me.btnIgnoring.UseVisualStyleBackColor = True
            '
            'frmFractionKakunin
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(287, 126)
            Me.Controls.Add(Me.btnIgnoring)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.lblKakunin)
            Me.Controls.Add(Me.btnOK)
            Me.Controls.Add(Me.Panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmFractionKakunin"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.TopMost = True
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents lblHEAD As System.Windows.Forms.Label
        Friend WithEvents btnOK As System.Windows.Forms.Button
        Friend WithEvents lblKakunin As System.Windows.Forms.Label
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents btnIgnoring As System.Windows.Forms.Button
    End Class

End Namespace
