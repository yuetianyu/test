Namespace YosanSetteiBuhinEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmCostSearchDialog
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
            Me.rbNewestHattchu = New System.Windows.Forms.RadioButton
            Me.rbNewestKenshu = New System.Windows.Forms.RadioButton
            Me.rbCost = New System.Windows.Forms.RadioButton
            Me.btnSearch = New System.Windows.Forms.Button
            Me.btnCancel = New System.Windows.Forms.Button
            Me.SuspendLayout()
            '
            'rbNewestHattchu
            '
            Me.rbNewestHattchu.AutoSize = True
            Me.rbNewestHattchu.Location = New System.Drawing.Point(12, 12)
            Me.rbNewestHattchu.Name = "rbNewestHattchu"
            Me.rbNewestHattchu.Size = New System.Drawing.Size(151, 16)
            Me.rbNewestHattchu.TabIndex = 0
            Me.rbNewestHattchu.TabStop = True
            Me.rbNewestHattchu.Text = "発注最新順(発注日降順)"
            Me.rbNewestHattchu.UseVisualStyleBackColor = True
            '
            'rbNewestKenshu
            '
            Me.rbNewestKenshu.AutoSize = True
            Me.rbNewestKenshu.Location = New System.Drawing.Point(12, 34)
            Me.rbNewestKenshu.Name = "rbNewestKenshu"
            Me.rbNewestKenshu.Size = New System.Drawing.Size(151, 16)
            Me.rbNewestKenshu.TabIndex = 1
            Me.rbNewestKenshu.TabStop = True
            Me.rbNewestKenshu.Text = "検収最新順(検収日降順)"
            Me.rbNewestKenshu.UseVisualStyleBackColor = True
            '
            'rbCost
            '
            Me.rbCost.AutoSize = True
            Me.rbCost.Location = New System.Drawing.Point(12, 56)
            Me.rbCost.Name = "rbCost"
            Me.rbCost.Size = New System.Drawing.Size(152, 16)
            Me.rbCost.TabIndex = 2
            Me.rbCost.TabStop = True
            Me.rbCost.Text = "コスト低順(検収金額昇順)"
            Me.rbCost.UseVisualStyleBackColor = True
            '
            'btnSearch
            '
            Me.btnSearch.Location = New System.Drawing.Point(12, 78)
            Me.btnSearch.Name = "btnSearch"
            Me.btnSearch.Size = New System.Drawing.Size(75, 23)
            Me.btnSearch.TabIndex = 3
            Me.btnSearch.Text = "検索"
            Me.btnSearch.UseVisualStyleBackColor = True
            '
            'btnCancel
            '
            Me.btnCancel.Location = New System.Drawing.Point(141, 78)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(75, 23)
            Me.btnCancel.TabIndex = 4
            Me.btnCancel.Text = "キャンセル"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'frmCostSearchDialog
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(259, 116)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnSearch)
            Me.Controls.Add(Me.rbCost)
            Me.Controls.Add(Me.rbNewestKenshu)
            Me.Controls.Add(Me.rbNewestHattchu)
            Me.Name = "frmCostSearchDialog"
            Me.Text = "【検索条件】"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents rbNewestHattchu As System.Windows.Forms.RadioButton
        Friend WithEvents rbNewestKenshu As System.Windows.Forms.RadioButton
        Friend WithEvents rbCost As System.Windows.Forms.RadioButton
        Friend WithEvents btnSearch As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button
    End Class
End Namespace