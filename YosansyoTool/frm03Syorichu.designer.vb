<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm03Syorichu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm03Syorichu))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblKakunin = New System.Windows.Forms.Label
        Me.lblKakunin2 = New System.Windows.Forms.Label
        Me.lblKakunin3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.BackColor = System.Drawing.Color.Blue
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(268, 0)
        Me.Panel1.TabIndex = 54
        '
        'lblKakunin
        '
        Me.lblKakunin.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin.ForeColor = System.Drawing.Color.Black
        Me.lblKakunin.Location = New System.Drawing.Point(0, 54)
        Me.lblKakunin.Name = "lblKakunin"
        Me.lblKakunin.Size = New System.Drawing.Size(268, 20)
        Me.lblKakunin.TabIndex = 56
        Me.lblKakunin.Text = "試作イベント登録・編集画面を"
        Me.lblKakunin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblKakunin2
        '
        Me.lblKakunin2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin2.Location = New System.Drawing.Point(0, 81)
        Me.lblKakunin2.Name = "lblKakunin2"
        Me.lblKakunin2.Size = New System.Drawing.Size(268, 19)
        Me.lblKakunin2.TabIndex = 58
        Me.lblKakunin2.Text = "開いています。"
        Me.lblKakunin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblKakunin3
        '
        Me.lblKakunin3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin3.Location = New System.Drawing.Point(0, 131)
        Me.lblKakunin3.Name = "lblKakunin3"
        Me.lblKakunin3.Size = New System.Drawing.Size(268, 19)
        Me.lblKakunin3.TabIndex = 59
        Me.lblKakunin3.Text = "暫くお待ちください。。。"
        Me.lblKakunin3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Blue
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Yellow
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(268, 31)
        Me.Label4.TabIndex = 60
        Me.Label4.Text = "処理中"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frm03Syorichu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(268, 185)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblKakunin3)
        Me.Controls.Add(Me.lblKakunin2)
        Me.Controls.Add(Me.lblKakunin)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm03Syorichu"
        Me.Text = "補用部品手配システム Ver 1.00"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblKakunin As System.Windows.Forms.Label
    Friend WithEvents lblKakunin2 As System.Windows.Forms.Label
    Friend WithEvents lblKakunin3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
