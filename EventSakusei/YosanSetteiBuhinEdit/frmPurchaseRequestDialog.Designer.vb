<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPurchaseRequestDialog
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
        Me.cmbGenchoEventCode = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbPhase = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbGenchoSystem = New System.Windows.Forms.RadioButton
        Me.rbYosanLayoutMulti = New System.Windows.Forms.RadioButton
        Me.rbPastBuyParts = New System.Windows.Forms.RadioButton
        Me.rbYosanLayout = New System.Windows.Forms.RadioButton
        Me.rbPartsPrice = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rbIsAll = New System.Windows.Forms.RadioButton
        Me.rbIsBlankCell = New System.Windows.Forms.RadioButton
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbGenchoEventCode
        '
        Me.cmbGenchoEventCode.Enabled = False
        Me.cmbGenchoEventCode.FormattingEnabled = True
        Me.cmbGenchoEventCode.Location = New System.Drawing.Point(111, 140)
        Me.cmbGenchoEventCode.Name = "cmbGenchoEventCode"
        Me.cmbGenchoEventCode.Size = New System.Drawing.Size(198, 20)
        Me.cmbGenchoEventCode.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(37, 143)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "イベントコード"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(12, 314)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 9
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(264, 314)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(37, 169)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 12)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "フェーズ"
        '
        'cmbPhase
        '
        Me.cmbPhase.Enabled = False
        Me.cmbPhase.FormattingEnabled = True
        Me.cmbPhase.Location = New System.Drawing.Point(111, 166)
        Me.cmbPhase.Name = "cmbPhase"
        Me.cmbPhase.Size = New System.Drawing.Size(198, 20)
        Me.cmbPhase.TabIndex = 6
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbGenchoSystem)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.rbYosanLayoutMulti)
        Me.GroupBox1.Controls.Add(Me.cmbPhase)
        Me.GroupBox1.Controls.Add(Me.rbPastBuyParts)
        Me.GroupBox1.Controls.Add(Me.rbYosanLayout)
        Me.GroupBox1.Controls.Add(Me.rbPartsPrice)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbGenchoEventCode)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(327, 209)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "購入希望単価"
        '
        'rbGenchoSystem
        '
        Me.rbGenchoSystem.AutoSize = True
        Me.rbGenchoSystem.Location = New System.Drawing.Point(21, 118)
        Me.rbGenchoSystem.Name = "rbGenchoSystem"
        Me.rbGenchoSystem.Size = New System.Drawing.Size(107, 16)
        Me.rbGenchoSystem.TabIndex = 4
        Me.rbGenchoSystem.Text = "試作開発管理表"
        Me.rbGenchoSystem.UseVisualStyleBackColor = True
        '
        'rbYosanLayoutMulti
        '
        Me.rbYosanLayoutMulti.AutoSize = True
        Me.rbYosanLayoutMulti.Location = New System.Drawing.Point(21, 96)
        Me.rbYosanLayoutMulti.Name = "rbYosanLayoutMulti"
        Me.rbYosanLayoutMulti.Size = New System.Drawing.Size(113, 16)
        Me.rbYosanLayoutMulti.TabIndex = 3
        Me.rbYosanLayoutMulti.Text = "割付予算×係数2"
        Me.rbYosanLayoutMulti.UseVisualStyleBackColor = True
        '
        'rbPastBuyParts
        '
        Me.rbPastBuyParts.AutoSize = True
        Me.rbPastBuyParts.Location = New System.Drawing.Point(21, 74)
        Me.rbPastBuyParts.Name = "rbPastBuyParts"
        Me.rbPastBuyParts.Size = New System.Drawing.Size(217, 16)
        Me.rbPastBuyParts.TabIndex = 2
        Me.rbPastBuyParts.Text = "過去購入部品優先度1(購入希望単価)"
        Me.rbPastBuyParts.UseVisualStyleBackColor = True
        '
        'rbYosanLayout
        '
        Me.rbYosanLayout.AutoSize = True
        Me.rbYosanLayout.Location = New System.Drawing.Point(21, 52)
        Me.rbYosanLayout.Name = "rbYosanLayout"
        Me.rbYosanLayout.Size = New System.Drawing.Size(118, 16)
        Me.rbYosanLayout.TabIndex = 1
        Me.rbYosanLayout.Text = "割付予算値スライド"
        Me.rbYosanLayout.UseVisualStyleBackColor = True
        '
        'rbPartsPrice
        '
        Me.rbPartsPrice.AutoSize = True
        Me.rbPartsPrice.Checked = True
        Me.rbPartsPrice.Location = New System.Drawing.Point(21, 30)
        Me.rbPartsPrice.Name = "rbPartsPrice"
        Me.rbPartsPrice.Size = New System.Drawing.Size(134, 16)
        Me.rbPartsPrice.TabIndex = 0
        Me.rbPartsPrice.TabStop = True
        Me.rbPartsPrice.Text = "パーツプライス値スライド"
        Me.rbPartsPrice.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbIsAll)
        Me.GroupBox2.Controls.Add(Me.rbIsBlankCell)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 227)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 70)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "設定範囲"
        '
        'rbIsAll
        '
        Me.rbIsAll.AutoSize = True
        Me.rbIsAll.Location = New System.Drawing.Point(6, 40)
        Me.rbIsAll.Name = "rbIsAll"
        Me.rbIsAll.Size = New System.Drawing.Size(74, 16)
        Me.rbIsAll.TabIndex = 8
        Me.rbIsAll.Text = "全てのセル"
        Me.rbIsAll.UseVisualStyleBackColor = True
        '
        'rbIsBlankCell
        '
        Me.rbIsBlankCell.AutoSize = True
        Me.rbIsBlankCell.Checked = True
        Me.rbIsBlankCell.Location = New System.Drawing.Point(6, 18)
        Me.rbIsBlankCell.Name = "rbIsBlankCell"
        Me.rbIsBlankCell.Size = New System.Drawing.Size(88, 16)
        Me.rbIsBlankCell.TabIndex = 7
        Me.rbIsBlankCell.TabStop = True
        Me.rbIsBlankCell.Text = "空白セルのみ"
        Me.rbIsBlankCell.UseVisualStyleBackColor = True
        '
        'frmPurchaseRequestDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(351, 346)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Name = "frmPurchaseRequestDialog"
        Me.Text = "単価自動設定機能：購入希望単価"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbGenchoEventCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbPhase As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbPartsPrice As System.Windows.Forms.RadioButton
    Friend WithEvents rbGenchoSystem As System.Windows.Forms.RadioButton
    Friend WithEvents rbYosanLayoutMulti As System.Windows.Forms.RadioButton
    Friend WithEvents rbPastBuyParts As System.Windows.Forms.RadioButton
    Friend WithEvents rbYosanLayout As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbIsAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbIsBlankCell As System.Windows.Forms.RadioButton
End Class
