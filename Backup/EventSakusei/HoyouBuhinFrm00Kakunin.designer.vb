<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HoyouBuhinFrm00Kakunin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HoyouBuhinFrm00Kakunin))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblHEAD = New System.Windows.Forms.Label
        Me.btnHanei = New System.Windows.Forms.Button
        Me.lblKakunin = New System.Windows.Forms.Label
        Me.btnModosu = New System.Windows.Forms.Button
        Me.lblKakunin2 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.BackColor = System.Drawing.Color.LightPink
        Me.Panel1.Controls.Add(Me.lblHEAD)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(242, 32)
        Me.Panel1.TabIndex = 54
        '
        'lblHEAD
        '
        Me.lblHEAD.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHEAD.ForeColor = System.Drawing.Color.Blue
        Me.lblHEAD.Location = New System.Drawing.Point(12, 1)
        Me.lblHEAD.Name = "lblHEAD"
        Me.lblHEAD.Size = New System.Drawing.Size(218, 31)
        Me.lblHEAD.TabIndex = 54
        Me.lblHEAD.Text = "EXCEL取込の確認"
        Me.lblHEAD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnHanei
        '
        Me.btnHanei.Location = New System.Drawing.Point(15, 123)
        Me.btnHanei.Name = "btnHanei"
        Me.btnHanei.Size = New System.Drawing.Size(99, 23)
        Me.btnHanei.TabIndex = 55
        Me.btnHanei.Text = "EXCELを反映"
        Me.btnHanei.UseVisualStyleBackColor = True
        '
        'lblKakunin
        '
        Me.lblKakunin.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin.ForeColor = System.Drawing.Color.Red
        Me.lblKakunin.Location = New System.Drawing.Point(0, 50)
        Me.lblKakunin.Name = "lblKakunin"
        Me.lblKakunin.Size = New System.Drawing.Size(242, 26)
        Me.lblKakunin.TabIndex = 56
        Me.lblKakunin.Text = "EXCELの情報を確認してください。"
        Me.lblKakunin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnModosu
        '
        Me.btnModosu.Location = New System.Drawing.Point(130, 123)
        Me.btnModosu.Name = "btnModosu"
        Me.btnModosu.Size = New System.Drawing.Size(100, 23)
        Me.btnModosu.TabIndex = 57
        Me.btnModosu.Text = "取込前に戻す"
        Me.btnModosu.UseVisualStyleBackColor = True
        '
        'lblKakunin2
        '
        Me.lblKakunin2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin2.Location = New System.Drawing.Point(0, 83)
        Me.lblKakunin2.Name = "lblKakunin2"
        Me.lblKakunin2.Size = New System.Drawing.Size(242, 26)
        Me.lblKakunin2.TabIndex = 58
        Me.lblKakunin2.Text = "EXCELのデータを反映しますか？"
        Me.lblKakunin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HoyouBuhinFrm00Kakunin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(242, 161)
        Me.Controls.Add(Me.lblKakunin2)
        Me.Controls.Add(Me.btnModosu)
        Me.Controls.Add(Me.lblKakunin)
        Me.Controls.Add(Me.btnHanei)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "HoyouBuhinFrm00Kakunin"
        Me.Text = "補用部品手配システム Ver 1.00"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblHEAD As System.Windows.Forms.Label
    Friend WithEvents btnHanei As System.Windows.Forms.Button
    Friend WithEvents lblKakunin As System.Windows.Forms.Label
    Friend WithEvents btnModosu As System.Windows.Forms.Button
    Friend WithEvents lblKakunin2 As System.Windows.Forms.Label
End Class
