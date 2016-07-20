Namespace YosanshoEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FrmSelectPattern
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSelectPattern))
            Me.btnBack = New System.Windows.Forms.Button
            Me.listPatternName = New System.Windows.Forms.ListBox
            Me.Label1 = New System.Windows.Forms.Label
            Me.SuspendLayout()
            '
            'btnBack
            '
            Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
            Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBack.Location = New System.Drawing.Point(39, 93)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(82, 24)
            Me.btnBack.TabIndex = 104
            Me.btnBack.TabStop = False
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = True
            '
            'listPatternName
            '
            Me.listPatternName.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
            Me.listPatternName.FormattingEnabled = True
            Me.listPatternName.ItemHeight = 12
            Me.listPatternName.Items.AddRange(New Object() {"a", "b", "c", "d", "e"})
            Me.listPatternName.Location = New System.Drawing.Point(10, 23)
            Me.listPatternName.Name = "listPatternName"
            Me.listPatternName.Size = New System.Drawing.Size(140, 64)
            Me.listPatternName.TabIndex = 106
            '
            'Label1
            '
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
            Me.Label1.Location = New System.Drawing.Point(10, 7)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(114, 16)
            Me.Label1.TabIndex = 107
            Me.Label1.Text = "パターン名"
            '
            'FrmSelectPattern
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(162, 124)
            Me.ControlBox = False
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.listPatternName)
            Me.Controls.Add(Me.btnBack)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FrmSelectPattern"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "パターン名を選択"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents btnBack As System.Windows.Forms.Button
        Friend WithEvents listPatternName As System.Windows.Forms.ListBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
    End Class
End Namespace