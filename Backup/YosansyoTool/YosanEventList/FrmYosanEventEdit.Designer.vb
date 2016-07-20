Namespace YosanEventList
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FrmYosanEventEdit
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmYosanEventEdit))
            Me.lblContent = New System.Windows.Forms.Label
            Me.txtContent = New System.Windows.Forms.TextBox
            Me.btnUpdate = New System.Windows.Forms.Button
            Me.btnBack = New System.Windows.Forms.Button
            Me.SuspendLayout()
            '
            'lblContent
            '
            Me.lblContent.Location = New System.Drawing.Point(19, 7)
            Me.lblContent.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.lblContent.Name = "lblContent"
            Me.lblContent.Size = New System.Drawing.Size(102, 37)
            Me.lblContent.TabIndex = 0
            Me.lblContent.Text = "イベント名："
            Me.lblContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'txtContent
            '
            Me.txtContent.Location = New System.Drawing.Point(97, 13)
            Me.txtContent.Margin = New System.Windows.Forms.Padding(4)
            Me.txtContent.Multiline = True
            Me.txtContent.Name = "txtContent"
            Me.txtContent.Size = New System.Drawing.Size(455, 22)
            Me.txtContent.TabIndex = 1
            Me.txtContent.Text = "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ" & _
                "あああああああああああえええええええええええええええええええええええええええええええええええええええ"
            '
            'btnUpdate
            '
            Me.btnUpdate.Location = New System.Drawing.Point(21, 82)
            Me.btnUpdate.Margin = New System.Windows.Forms.Padding(4)
            Me.btnUpdate.Name = "btnUpdate"
            Me.btnUpdate.Size = New System.Drawing.Size(80, 27)
            Me.btnUpdate.TabIndex = 2
            Me.btnUpdate.Text = "更新"
            Me.btnUpdate.UseVisualStyleBackColor = True
            '
            'btnBack
            '
            Me.btnBack.Location = New System.Drawing.Point(472, 82)
            Me.btnBack.Margin = New System.Windows.Forms.Padding(4)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(80, 27)
            Me.btnBack.TabIndex = 3
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = True
            '
            'FrmYosanEventEdit
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(574, 122)
            Me.Controls.Add(Me.btnBack)
            Me.Controls.Add(Me.btnUpdate)
            Me.Controls.Add(Me.txtContent)
            Me.Controls.Add(Me.lblContent)
            Me.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Margin = New System.Windows.Forms.Padding(4)
            Me.Name = "FrmYosanEventEdit"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents lblContent As System.Windows.Forms.Label
        Friend WithEvents txtContent As System.Windows.Forms.TextBox
        Friend WithEvents btnUpdate As System.Windows.Forms.Button
        Friend WithEvents btnBack As System.Windows.Forms.Button
    End Class
End Namespace
