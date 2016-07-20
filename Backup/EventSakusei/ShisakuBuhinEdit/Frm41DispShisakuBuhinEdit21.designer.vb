Namespace ShisakuBuhinEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm41DispShisakuBuhinEdit21
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm41DispShisakuBuhinEdit21))
            Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.btnTorikeshi = New System.Windows.Forms.Button
            Me.btnSet = New System.Windows.Forms.Button
            Me.btnHyouji = New System.Windows.Forms.Button
            Me.Label2 = New System.Windows.Forms.Label
            Me.txtBuhinBango = New System.Windows.Forms.TextBox
            Me.cmbBuhinbangoKubun = New System.Windows.Forms.ComboBox
            Me.Label1 = New System.Windows.Forms.Label
            Me.cmbkaramu = New System.Windows.Forms.ComboBox
            Me.lblBuhinNo = New System.Windows.Forms.Label
            Me.btnSettei = New System.Windows.Forms.Button
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel1.Controls.Add(Me.btnTorikeshi)
            Me.Panel1.Controls.Add(Me.btnSet)
            Me.Panel1.Controls.Add(Me.btnHyouji)
            Me.Panel1.Controls.Add(Me.Label2)
            Me.Panel1.Controls.Add(Me.txtBuhinBango)
            Me.Panel1.Controls.Add(Me.cmbBuhinbangoKubun)
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Controls.Add(Me.cmbkaramu)
            Me.Panel1.Controls.Add(Me.lblBuhinNo)
            Me.Panel1.Controls.Add(Me.btnSettei)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(3, 3)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(219, 108)
            Me.Panel1.TabIndex = 75
            '
            'btnTorikeshi
            '
            Me.btnTorikeshi.Location = New System.Drawing.Point(130, 75)
            Me.btnTorikeshi.Name = "btnTorikeshi"
            Me.btnTorikeshi.Size = New System.Drawing.Size(50, 23)
            Me.btnTorikeshi.TabIndex = 137
            Me.btnTorikeshi.Text = "取消"
            Me.btnTorikeshi.UseVisualStyleBackColor = False
            '
            'btnSet
            '
            Me.btnSet.Location = New System.Drawing.Point(74, 75)
            Me.btnSet.Name = "btnSet"
            Me.btnSet.Size = New System.Drawing.Size(50, 23)
            Me.btnSet.TabIndex = 136
            Me.btnSet.Text = "セット"
            Me.btnSet.UseVisualStyleBackColor = False
            '
            'btnHyouji
            '
            Me.btnHyouji.Location = New System.Drawing.Point(18, 75)
            Me.btnHyouji.Name = "btnHyouji"
            Me.btnHyouji.Size = New System.Drawing.Size(50, 23)
            Me.btnHyouji.TabIndex = 135
            Me.btnHyouji.Text = "表示"
            Me.btnHyouji.UseVisualStyleBackColor = False
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label2.Location = New System.Drawing.Point(114, 35)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(41, 12)
            Me.Label2.TabIndex = 134
            Me.Label2.Text = "（区分）"
            '
            'txtBuhinBango
            '
            Me.txtBuhinBango.BackColor = System.Drawing.Color.White
            Me.txtBuhinBango.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtBuhinBango.ForeColor = System.Drawing.Color.Navy
            Me.txtBuhinBango.Location = New System.Drawing.Point(18, 50)
            Me.txtBuhinBango.Name = "txtBuhinBango"
            Me.txtBuhinBango.Size = New System.Drawing.Size(92, 19)
            Me.txtBuhinBango.TabIndex = 133
            Me.txtBuhinBango.Text = "1234567890123"
            '
            'cmbBuhinbangoKubun
            '
            Me.cmbBuhinbangoKubun.FormattingEnabled = True
            Me.cmbBuhinbangoKubun.Location = New System.Drawing.Point(116, 50)
            Me.cmbBuhinbangoKubun.Name = "cmbBuhinbangoKubun"
            Me.cmbBuhinbangoKubun.Size = New System.Drawing.Size(52, 20)
            Me.cmbBuhinbangoKubun.TabIndex = 132
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.Location = New System.Drawing.Point(16, 35)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(65, 12)
            Me.Label1.TabIndex = 131
            Me.Label1.Text = "（部品番号）"
            '
            'cmbkaramu
            '
            Me.cmbkaramu.FormattingEnabled = True
            Me.cmbkaramu.Location = New System.Drawing.Point(60, 6)
            Me.cmbkaramu.Name = "cmbkaramu"
            Me.cmbkaramu.Size = New System.Drawing.Size(37, 20)
            Me.cmbkaramu.TabIndex = 130
            '
            'lblBuhinNo
            '
            Me.lblBuhinNo.AutoSize = True
            Me.lblBuhinNo.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblBuhinNo.Location = New System.Drawing.Point(16, 9)
            Me.lblBuhinNo.Name = "lblBuhinNo"
            Me.lblBuhinNo.Size = New System.Drawing.Size(38, 12)
            Me.lblBuhinNo.TabIndex = 129
            Me.lblBuhinNo.Text = "カラム："
            '
            'btnSettei
            '
            Me.btnSettei.Location = New System.Drawing.Point(131, 3)
            Me.btnSettei.Name = "btnSettei"
            Me.btnSettei.Size = New System.Drawing.Size(75, 23)
            Me.btnSettei.TabIndex = 85
            Me.btnSettei.Text = "一括設定"
            Me.btnSettei.UseVisualStyleBackColor = False
            '
            'Frm41DispShisakuBuhinEdit21
            '
            Me.AllowDrop = True
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(225, 115)
            Me.Controls.Add(Me.Panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "Frm41DispShisakuBuhinEdit21"
            Me.Padding = New System.Windows.Forms.Padding(3)
            Me.Text = "部品構成呼び出し"
            Me.TopMost = True
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents btnSettei As System.Windows.Forms.Button
        Friend WithEvents cmbBuhinbangoKubun As System.Windows.Forms.ComboBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents cmbkaramu As System.Windows.Forms.ComboBox
        Friend WithEvents lblBuhinNo As System.Windows.Forms.Label
        Friend WithEvents txtBuhinBango As System.Windows.Forms.TextBox
        Friend WithEvents btnTorikeshi As System.Windows.Forms.Button
        Friend WithEvents btnSet As System.Windows.Forms.Button
        Friend WithEvents btnHyouji As System.Windows.Forms.Button
        Friend WithEvents Label2 As System.Windows.Forms.Label

    End Class
End Namespace
