Namespace YosanSetteiBuhinEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmPartsPriceFractionDialog
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
            Me.Label1 = New System.Windows.Forms.Label
            Me.ComboBox1 = New System.Windows.Forms.ComboBox
            Me.Label2 = New System.Windows.Forms.Label
            Me.ComboBox2 = New System.Windows.Forms.ComboBox
            Me.btnOk = New System.Windows.Forms.Button
            Me.btnCancel = New System.Windows.Forms.Button
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(53, 12)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "処理方法"
            '
            'ComboBox1
            '
            Me.ComboBox1.FormattingEnabled = True
            Me.ComboBox1.Location = New System.Drawing.Point(12, 24)
            Me.ComboBox1.Name = "ComboBox1"
            Me.ComboBox1.Size = New System.Drawing.Size(123, 20)
            Me.ComboBox1.TabIndex = 1
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(12, 47)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(53, 12)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "処理単位"
            '
            'ComboBox2
            '
            Me.ComboBox2.FormattingEnabled = True
            Me.ComboBox2.Location = New System.Drawing.Point(14, 62)
            Me.ComboBox2.Name = "ComboBox2"
            Me.ComboBox2.Size = New System.Drawing.Size(121, 20)
            Me.ComboBox2.TabIndex = 3
            '
            'btnOk
            '
            Me.btnOk.Location = New System.Drawing.Point(14, 99)
            Me.btnOk.Name = "btnOk"
            Me.btnOk.Size = New System.Drawing.Size(75, 23)
            Me.btnOk.TabIndex = 4
            Me.btnOk.Text = "実行"
            Me.btnOk.UseVisualStyleBackColor = True
            '
            'btnCancel
            '
            Me.btnCancel.Location = New System.Drawing.Point(109, 99)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(75, 23)
            Me.btnCancel.TabIndex = 5
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'frmPartsPriceFractionDialog
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(201, 141)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnOk)
            Me.Controls.Add(Me.ComboBox2)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.ComboBox1)
            Me.Controls.Add(Me.Label1)
            Me.Name = "frmPartsPriceFractionDialog"
            Me.Text = "部品費端数処理"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
        Friend WithEvents btnOk As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button
    End Class


End Namespace
