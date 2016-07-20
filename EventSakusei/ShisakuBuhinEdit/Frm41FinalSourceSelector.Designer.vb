<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm41FinalSourceSelector
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
        Me.lblFinalSource = New System.Windows.Forms.Label
        Me.cmbFinalSource = New System.Windows.Forms.ComboBox
        Me.btnCreate = New System.Windows.Forms.Button
        Me.btnDisplay = New System.Windows.Forms.Button
        Me.btnBack = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblFinalSource
        '
        Me.lblFinalSource.AutoSize = True
        Me.lblFinalSource.Location = New System.Drawing.Point(48, 25)
        Me.lblFinalSource.Name = "lblFinalSource"
        Me.lblFinalSource.Size = New System.Drawing.Size(118, 12)
        Me.lblFinalSource.TabIndex = 0
        Me.lblFinalSource.Text = "ベースにするFINAL品番"
        '
        'cmbFinalSource
        '
        Me.cmbFinalSource.FormattingEnabled = True
        Me.cmbFinalSource.Location = New System.Drawing.Point(190, 22)
        Me.cmbFinalSource.Name = "cmbFinalSource"
        Me.cmbFinalSource.Size = New System.Drawing.Size(186, 20)
        Me.cmbFinalSource.TabIndex = 1
        '
        'btnCreate
        '
        Me.btnCreate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.btnCreate.Location = New System.Drawing.Point(50, 78)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(79, 24)
        Me.btnCreate.TabIndex = 5
        Me.btnCreate.Text = "新規作成"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'btnDisplay
        '
        Me.btnDisplay.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.btnDisplay.Location = New System.Drawing.Point(178, 78)
        Me.btnDisplay.Name = "btnDisplay"
        Me.btnDisplay.Size = New System.Drawing.Size(79, 24)
        Me.btnDisplay.TabIndex = 6
        Me.btnDisplay.Text = "表示"
        Me.btnDisplay.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.btnBack.Location = New System.Drawing.Point(301, 78)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(79, 24)
        Me.btnBack.TabIndex = 7
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Frm41FinalSourceSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(435, 124)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnDisplay)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.cmbFinalSource)
        Me.Controls.Add(Me.lblFinalSource)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm41FinalSourceSelector"
        Me.ShowIcon = False
        Me.Text = "ベースにするFINAL品番選択画面"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblFinalSource As System.Windows.Forms.Label
    Friend WithEvents cmbFinalSource As System.Windows.Forms.ComboBox
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents btnDisplay As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
End Class
