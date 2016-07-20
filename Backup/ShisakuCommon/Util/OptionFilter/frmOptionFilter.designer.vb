<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptionFilter
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
        Me.btnOk = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.lblInfo1 = New System.Windows.Forms.Label
        Me.cboValue_1 = New System.Windows.Forms.ComboBox
        Me.cboCondition_1 = New System.Windows.Forms.ComboBox
        Me.pnlAndOr_1 = New System.Windows.Forms.FlowLayoutPanel
        Me.rdoOr_1 = New System.Windows.Forms.RadioButton
        Me.rdoAnd_1 = New System.Windows.Forms.RadioButton
        Me.cboValue_2 = New System.Windows.Forms.ComboBox
        Me.pnlAndOr_2 = New System.Windows.Forms.FlowLayoutPanel
        Me.rdoOr_2 = New System.Windows.Forms.RadioButton
        Me.rdoAnd_2 = New System.Windows.Forms.RadioButton
        Me.cboValue_3 = New System.Windows.Forms.ComboBox
        Me.pnlAndOr_3 = New System.Windows.Forms.FlowLayoutPanel
        Me.rdoOr_3 = New System.Windows.Forms.RadioButton
        Me.rdoAnd_3 = New System.Windows.Forms.RadioButton
        Me.cboValue_4 = New System.Windows.Forms.ComboBox
        Me.pnlAndOr_4 = New System.Windows.Forms.FlowLayoutPanel
        Me.rdoOr_4 = New System.Windows.Forms.RadioButton
        Me.rdoAnd_4 = New System.Windows.Forms.RadioButton
        Me.cboValue_5 = New System.Windows.Forms.ComboBox
        Me.cboCondition_2 = New System.Windows.Forms.ComboBox
        Me.cboCondition_3 = New System.Windows.Forms.ComboBox
        Me.cboCondition_4 = New System.Windows.Forms.ComboBox
        Me.cboCondition_5 = New System.Windows.Forms.ComboBox
        Me.lblLine1 = New System.Windows.Forms.Label
        Me.lblLine2 = New System.Windows.Forms.Label
        Me.btnClear = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlAndOr_1.SuspendLayout()
        Me.pnlAndOr_2.SuspendLayout()
        Me.pnlAndOr_3.SuspendLayout()
        Me.pnlAndOr_4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Location = New System.Drawing.Point(266, 277)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(90, 23)
        Me.btnOk.TabIndex = 19
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(362, 277)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 23)
        Me.btnCancel.TabIndex = 20
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblInfo1
        '
        Me.lblInfo1.AutoSize = True
        Me.lblInfo1.Location = New System.Drawing.Point(12, 9)
        Me.lblInfo1.Name = "lblInfo1"
        Me.lblInfo1.Size = New System.Drawing.Size(157, 12)
        Me.lblInfo1.TabIndex = 0
        Me.lblInfo1.Text = "《抽出条件を指定してください。》"
        '
        'cboValue_1
        '
        Me.cboValue_1.FormattingEnabled = True
        Me.cboValue_1.Location = New System.Drawing.Point(14, 41)
        Me.cboValue_1.Name = "cboValue_1"
        Me.cboValue_1.Size = New System.Drawing.Size(328, 20)
        Me.cboValue_1.TabIndex = 3
        '
        'cboCondition_1
        '
        Me.cboCondition_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCondition_1.FormattingEnabled = True
        Me.cboCondition_1.Location = New System.Drawing.Point(348, 41)
        Me.cboCondition_1.MaxDropDownItems = 13
        Me.cboCondition_1.Name = "cboCondition_1"
        Me.cboCondition_1.Size = New System.Drawing.Size(104, 20)
        Me.cboCondition_1.TabIndex = 4
        '
        'pnlAndOr_1
        '
        Me.pnlAndOr_1.Controls.Add(Me.rdoOr_1)
        Me.pnlAndOr_1.Controls.Add(Me.rdoAnd_1)
        Me.pnlAndOr_1.Location = New System.Drawing.Point(38, 64)
        Me.pnlAndOr_1.Name = "pnlAndOr_1"
        Me.pnlAndOr_1.Size = New System.Drawing.Size(119, 20)
        Me.pnlAndOr_1.TabIndex = 5
        '
        'rdoOr_1
        '
        Me.rdoOr_1.AutoSize = True
        Me.rdoOr_1.Checked = True
        Me.rdoOr_1.Location = New System.Drawing.Point(3, 3)
        Me.rdoOr_1.Name = "rdoOr_1"
        Me.rdoOr_1.Size = New System.Drawing.Size(39, 16)
        Me.rdoOr_1.TabIndex = 0
        Me.rdoOr_1.TabStop = True
        Me.rdoOr_1.Text = "OR"
        Me.rdoOr_1.UseVisualStyleBackColor = True
        '
        'rdoAnd_1
        '
        Me.rdoAnd_1.AutoSize = True
        Me.rdoAnd_1.Location = New System.Drawing.Point(48, 3)
        Me.rdoAnd_1.Name = "rdoAnd_1"
        Me.rdoAnd_1.Size = New System.Drawing.Size(47, 16)
        Me.rdoAnd_1.TabIndex = 1
        Me.rdoAnd_1.Text = "AND"
        Me.rdoAnd_1.UseVisualStyleBackColor = True
        '
        'cboValue_2
        '
        Me.cboValue_2.FormattingEnabled = True
        Me.cboValue_2.Location = New System.Drawing.Point(14, 89)
        Me.cboValue_2.Name = "cboValue_2"
        Me.cboValue_2.Size = New System.Drawing.Size(328, 20)
        Me.cboValue_2.TabIndex = 6
        '
        'pnlAndOr_2
        '
        Me.pnlAndOr_2.Controls.Add(Me.rdoOr_2)
        Me.pnlAndOr_2.Controls.Add(Me.rdoAnd_2)
        Me.pnlAndOr_2.Location = New System.Drawing.Point(38, 112)
        Me.pnlAndOr_2.Name = "pnlAndOr_2"
        Me.pnlAndOr_2.Size = New System.Drawing.Size(119, 20)
        Me.pnlAndOr_2.TabIndex = 8
        '
        'rdoOr_2
        '
        Me.rdoOr_2.AutoSize = True
        Me.rdoOr_2.Checked = True
        Me.rdoOr_2.Location = New System.Drawing.Point(3, 3)
        Me.rdoOr_2.Name = "rdoOr_2"
        Me.rdoOr_2.Size = New System.Drawing.Size(39, 16)
        Me.rdoOr_2.TabIndex = 0
        Me.rdoOr_2.TabStop = True
        Me.rdoOr_2.Text = "OR"
        Me.rdoOr_2.UseVisualStyleBackColor = True
        '
        'rdoAnd_2
        '
        Me.rdoAnd_2.AutoSize = True
        Me.rdoAnd_2.Location = New System.Drawing.Point(48, 3)
        Me.rdoAnd_2.Name = "rdoAnd_2"
        Me.rdoAnd_2.Size = New System.Drawing.Size(47, 16)
        Me.rdoAnd_2.TabIndex = 1
        Me.rdoAnd_2.Text = "AND"
        Me.rdoAnd_2.UseVisualStyleBackColor = True
        '
        'cboValue_3
        '
        Me.cboValue_3.FormattingEnabled = True
        Me.cboValue_3.Location = New System.Drawing.Point(14, 137)
        Me.cboValue_3.Name = "cboValue_3"
        Me.cboValue_3.Size = New System.Drawing.Size(328, 20)
        Me.cboValue_3.TabIndex = 9
        '
        'pnlAndOr_3
        '
        Me.pnlAndOr_3.Controls.Add(Me.rdoOr_3)
        Me.pnlAndOr_3.Controls.Add(Me.rdoAnd_3)
        Me.pnlAndOr_3.Location = New System.Drawing.Point(38, 160)
        Me.pnlAndOr_3.Name = "pnlAndOr_3"
        Me.pnlAndOr_3.Size = New System.Drawing.Size(119, 20)
        Me.pnlAndOr_3.TabIndex = 11
        '
        'rdoOr_3
        '
        Me.rdoOr_3.AutoSize = True
        Me.rdoOr_3.Checked = True
        Me.rdoOr_3.Location = New System.Drawing.Point(3, 3)
        Me.rdoOr_3.Name = "rdoOr_3"
        Me.rdoOr_3.Size = New System.Drawing.Size(39, 16)
        Me.rdoOr_3.TabIndex = 0
        Me.rdoOr_3.TabStop = True
        Me.rdoOr_3.Text = "OR"
        Me.rdoOr_3.UseVisualStyleBackColor = True
        '
        'rdoAnd_3
        '
        Me.rdoAnd_3.AutoSize = True
        Me.rdoAnd_3.Location = New System.Drawing.Point(48, 3)
        Me.rdoAnd_3.Name = "rdoAnd_3"
        Me.rdoAnd_3.Size = New System.Drawing.Size(47, 16)
        Me.rdoAnd_3.TabIndex = 1
        Me.rdoAnd_3.Text = "AND"
        Me.rdoAnd_3.UseVisualStyleBackColor = True
        '
        'cboValue_4
        '
        Me.cboValue_4.FormattingEnabled = True
        Me.cboValue_4.Location = New System.Drawing.Point(14, 185)
        Me.cboValue_4.Name = "cboValue_4"
        Me.cboValue_4.Size = New System.Drawing.Size(328, 20)
        Me.cboValue_4.TabIndex = 12
        '
        'pnlAndOr_4
        '
        Me.pnlAndOr_4.Controls.Add(Me.rdoOr_4)
        Me.pnlAndOr_4.Controls.Add(Me.rdoAnd_4)
        Me.pnlAndOr_4.Location = New System.Drawing.Point(38, 208)
        Me.pnlAndOr_4.Name = "pnlAndOr_4"
        Me.pnlAndOr_4.Size = New System.Drawing.Size(119, 20)
        Me.pnlAndOr_4.TabIndex = 14
        '
        'rdoOr_4
        '
        Me.rdoOr_4.AutoSize = True
        Me.rdoOr_4.Checked = True
        Me.rdoOr_4.Location = New System.Drawing.Point(3, 3)
        Me.rdoOr_4.Name = "rdoOr_4"
        Me.rdoOr_4.Size = New System.Drawing.Size(39, 16)
        Me.rdoOr_4.TabIndex = 0
        Me.rdoOr_4.TabStop = True
        Me.rdoOr_4.Text = "OR"
        Me.rdoOr_4.UseVisualStyleBackColor = True
        '
        'rdoAnd_4
        '
        Me.rdoAnd_4.AutoSize = True
        Me.rdoAnd_4.Location = New System.Drawing.Point(48, 3)
        Me.rdoAnd_4.Name = "rdoAnd_4"
        Me.rdoAnd_4.Size = New System.Drawing.Size(47, 16)
        Me.rdoAnd_4.TabIndex = 1
        Me.rdoAnd_4.Text = "AND"
        Me.rdoAnd_4.UseVisualStyleBackColor = True
        '
        'cboValue_5
        '
        Me.cboValue_5.FormattingEnabled = True
        Me.cboValue_5.Location = New System.Drawing.Point(14, 233)
        Me.cboValue_5.Name = "cboValue_5"
        Me.cboValue_5.Size = New System.Drawing.Size(328, 20)
        Me.cboValue_5.TabIndex = 15
        '
        'cboCondition_2
        '
        Me.cboCondition_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCondition_2.FormattingEnabled = True
        Me.cboCondition_2.Location = New System.Drawing.Point(348, 89)
        Me.cboCondition_2.MaxDropDownItems = 13
        Me.cboCondition_2.Name = "cboCondition_2"
        Me.cboCondition_2.Size = New System.Drawing.Size(104, 20)
        Me.cboCondition_2.TabIndex = 7
        '
        'cboCondition_3
        '
        Me.cboCondition_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCondition_3.FormattingEnabled = True
        Me.cboCondition_3.Location = New System.Drawing.Point(348, 137)
        Me.cboCondition_3.MaxDropDownItems = 13
        Me.cboCondition_3.Name = "cboCondition_3"
        Me.cboCondition_3.Size = New System.Drawing.Size(104, 20)
        Me.cboCondition_3.TabIndex = 10
        '
        'cboCondition_4
        '
        Me.cboCondition_4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCondition_4.FormattingEnabled = True
        Me.cboCondition_4.Location = New System.Drawing.Point(348, 185)
        Me.cboCondition_4.MaxDropDownItems = 13
        Me.cboCondition_4.Name = "cboCondition_4"
        Me.cboCondition_4.Size = New System.Drawing.Size(104, 20)
        Me.cboCondition_4.TabIndex = 13
        '
        'cboCondition_5
        '
        Me.cboCondition_5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCondition_5.FormattingEnabled = True
        Me.cboCondition_5.Location = New System.Drawing.Point(348, 233)
        Me.cboCondition_5.MaxDropDownItems = 13
        Me.cboCondition_5.Name = "cboCondition_5"
        Me.cboCondition_5.Size = New System.Drawing.Size(104, 20)
        Me.cboCondition_5.TabIndex = 16
        '
        'lblLine1
        '
        Me.lblLine1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLine1.BackColor = System.Drawing.Color.Gray
        Me.lblLine1.Location = New System.Drawing.Point(12, 30)
        Me.lblLine1.Name = "lblLine1"
        Me.lblLine1.Size = New System.Drawing.Size(442, 1)
        Me.lblLine1.TabIndex = 1
        '
        'lblLine2
        '
        Me.lblLine2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLine2.BackColor = System.Drawing.Color.White
        Me.lblLine2.Location = New System.Drawing.Point(12, 31)
        Me.lblLine2.Name = "lblLine2"
        Me.lblLine2.Size = New System.Drawing.Size(442, 1)
        Me.lblLine2.TabIndex = 2
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(14, 277)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(90, 23)
        Me.btnClear.TabIndex = 21
        Me.btnClear.Text = "条件クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(11, 265)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(442, 1)
        Me.Label1.TabIndex = 23
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.Gray
        Me.Label2.Location = New System.Drawing.Point(11, 264)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(442, 1)
        Me.Label2.TabIndex = 22
        '
        'frmOptionFilter
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(464, 312)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.lblLine2)
        Me.Controls.Add(Me.lblLine1)
        Me.Controls.Add(Me.cboCondition_5)
        Me.Controls.Add(Me.cboCondition_4)
        Me.Controls.Add(Me.cboCondition_3)
        Me.Controls.Add(Me.cboCondition_2)
        Me.Controls.Add(Me.cboValue_5)
        Me.Controls.Add(Me.cboValue_4)
        Me.Controls.Add(Me.pnlAndOr_4)
        Me.Controls.Add(Me.cboValue_3)
        Me.Controls.Add(Me.pnlAndOr_3)
        Me.Controls.Add(Me.cboValue_2)
        Me.Controls.Add(Me.pnlAndOr_1)
        Me.Controls.Add(Me.pnlAndOr_2)
        Me.Controls.Add(Me.cboCondition_1)
        Me.Controls.Add(Me.cboValue_1)
        Me.Controls.Add(Me.lblInfo1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOptionFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "フィルタリング"
        Me.pnlAndOr_1.ResumeLayout(False)
        Me.pnlAndOr_1.PerformLayout()
        Me.pnlAndOr_2.ResumeLayout(False)
        Me.pnlAndOr_2.PerformLayout()
        Me.pnlAndOr_3.ResumeLayout(False)
        Me.pnlAndOr_3.PerformLayout()
        Me.pnlAndOr_4.ResumeLayout(False)
        Me.pnlAndOr_4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblInfo1 As System.Windows.Forms.Label
    Friend WithEvents cboValue_1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboCondition_1 As System.Windows.Forms.ComboBox
    Friend WithEvents pnlAndOr_1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents rdoAnd_1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOr_1 As System.Windows.Forms.RadioButton
    Friend WithEvents cboValue_2 As System.Windows.Forms.ComboBox
    Friend WithEvents pnlAndOr_2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents rdoAnd_2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOr_2 As System.Windows.Forms.RadioButton
    Friend WithEvents cboValue_3 As System.Windows.Forms.ComboBox
    Friend WithEvents pnlAndOr_3 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents rdoAnd_3 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOr_3 As System.Windows.Forms.RadioButton
    Friend WithEvents cboValue_4 As System.Windows.Forms.ComboBox
    Friend WithEvents pnlAndOr_4 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents rdoAnd_4 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOr_4 As System.Windows.Forms.RadioButton
    Friend WithEvents cboValue_5 As System.Windows.Forms.ComboBox
    Friend WithEvents cboCondition_2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboCondition_3 As System.Windows.Forms.ComboBox
    Friend WithEvents cboCondition_4 As System.Windows.Forms.ComboBox
    Friend WithEvents cboCondition_5 As System.Windows.Forms.ComboBox
    Friend WithEvents lblLine1 As System.Windows.Forms.Label
    Friend WithEvents lblLine2 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
