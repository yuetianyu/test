<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm41KaiteiSourceSelector
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm41KaiteiSourceSelector))
        Me.lblEventName = New System.Windows.Forms.Label
        Me.lblBlockNo = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cbbKaiteiNo = New System.Windows.Forms.ComboBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnExcute = New System.Windows.Forms.Button
        Me.lblKaihatsuFugo = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblEventName
        '
        Me.lblEventName.AutoSize = True
        Me.lblEventName.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
        Me.lblEventName.Location = New System.Drawing.Point(47, 31)
        Me.lblEventName.Name = "lblEventName"
        Me.lblEventName.Size = New System.Drawing.Size(263, 15)
        Me.lblEventName.TabIndex = 0
        Me.lblEventName.Text = "既存車、移管車改修メンテナンス方法改善"
        '
        'lblBlockNo
        '
        Me.lblBlockNo.AutoSize = True
        Me.lblBlockNo.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
        Me.lblBlockNo.Location = New System.Drawing.Point(5, 50)
        Me.lblBlockNo.Name = "lblBlockNo"
        Me.lblBlockNo.Size = New System.Drawing.Size(40, 15)
        Me.lblBlockNo.TabIndex = 1
        Me.lblBlockNo.Text = "420A"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
        Me.Label3.Location = New System.Drawing.Point(61, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "改訂番号"
        '
        'cbbKaiteiNo
        '
        Me.cbbKaiteiNo.FormattingEnabled = True
        Me.cbbKaiteiNo.Location = New System.Drawing.Point(134, 69)
        Me.cbbKaiteiNo.Name = "cbbKaiteiNo"
        Me.cbbKaiteiNo.Size = New System.Drawing.Size(121, 20)
        Me.cbbKaiteiNo.TabIndex = 3
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = True
        Me.btnCancel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.btnCancel.Location = New System.Drawing.Point(47, 106)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(79, 25)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnExcute
        '
        Me.btnExcute.AutoSize = True
        Me.btnExcute.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.btnExcute.Location = New System.Drawing.Point(161, 106)
        Me.btnExcute.Name = "btnExcute"
        Me.btnExcute.Size = New System.Drawing.Size(110, 25)
        Me.btnExcute.TabIndex = 14
        Me.btnExcute.Text = "改訂コピー実行"
        Me.btnExcute.UseVisualStyleBackColor = True
        '
        'lblKaihatsuFugo
        '
        Me.lblKaihatsuFugo.AutoSize = True
        Me.lblKaihatsuFugo.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
        Me.lblKaihatsuFugo.Location = New System.Drawing.Point(5, 31)
        Me.lblKaihatsuFugo.Name = "lblKaihatsuFugo"
        Me.lblKaihatsuFugo.Size = New System.Drawing.Size(42, 15)
        Me.lblKaihatsuFugo.TabIndex = 16
        Me.lblKaihatsuFugo.Text = "TEST"
        '
        'Frm41KaiteiSourceSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(313, 135)
        Me.Controls.Add(Me.lblKaihatsuFugo)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnExcute)
        Me.Controls.Add(Me.cbbKaiteiNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblBlockNo)
        Me.Controls.Add(Me.lblEventName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm41KaiteiSourceSelector"
        Me.Text = "改訂コピー：改訂番号選択"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblEventName As System.Windows.Forms.Label
    Friend WithEvents lblBlockNo As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbbKaiteiNo As System.Windows.Forms.ComboBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnExcute As System.Windows.Forms.Button
    Friend WithEvents lblKaihatsuFugo As System.Windows.Forms.Label
End Class
