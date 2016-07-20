<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm02Kakunin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm02Kakunin))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblHEAD = New System.Windows.Forms.Label
        Me.lblKakunin = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.lblKakunin2 = New System.Windows.Forms.Label
        Me.txtBlockInfo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.BackColor = System.Drawing.Color.Blue
        Me.Panel1.Controls.Add(Me.lblHEAD)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(287, 32)
        Me.Panel1.TabIndex = 54
        '
        'lblHEAD
        '
        Me.lblHEAD.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHEAD.ForeColor = System.Drawing.Color.Yellow
        Me.lblHEAD.Location = New System.Drawing.Point(12, 1)
        Me.lblHEAD.Name = "lblHEAD"
        Me.lblHEAD.Size = New System.Drawing.Size(263, 31)
        Me.lblHEAD.TabIndex = 54
        Me.lblHEAD.Text = "警告"
        Me.lblHEAD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblKakunin
        '
        Me.lblKakunin.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin.ForeColor = System.Drawing.Color.Red
        Me.lblKakunin.Location = New System.Drawing.Point(0, 34)
        Me.lblKakunin.Name = "lblKakunin"
        Me.lblKakunin.Size = New System.Drawing.Size(287, 26)
        Me.lblKakunin.TabIndex = 56
        Me.lblKakunin.Text = "以下のブロックで編集中です。"
        Me.lblKakunin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(94, 296)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 23)
        Me.btnClose.TabIndex = 57
        Me.btnClose.Text = "閉じる"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblKakunin2
        '
        Me.lblKakunin2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin2.Location = New System.Drawing.Point(0, 59)
        Me.lblKakunin2.Name = "lblKakunin2"
        Me.lblKakunin2.Size = New System.Drawing.Size(287, 26)
        Me.lblKakunin2.TabIndex = 58
        Me.lblKakunin2.Text = "ステータスの更新はできません。"
        Me.lblKakunin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBlockInfo
        '
        Me.txtBlockInfo.BackColor = System.Drawing.Color.White
        Me.txtBlockInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBlockInfo.Location = New System.Drawing.Point(23, 113)
        Me.txtBlockInfo.Multiline = True
        Me.txtBlockInfo.Name = "txtBlockInfo"
        Me.txtBlockInfo.ReadOnly = True
        Me.txtBlockInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBlockInfo.Size = New System.Drawing.Size(239, 177)
        Me.txtBlockInfo.TabIndex = 59
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 85)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(242, 26)
        Me.Label1.TabIndex = 60
        Me.Label1.Text = "ﾌﾞﾛｯｸ 設計 担当者      TEL"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frm02Kakunin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(287, 324)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBlockInfo)
        Me.Controls.Add(Me.lblKakunin2)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblKakunin)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm02Kakunin"
        Me.Text = "新試作手配システム Ver 1.00"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblHEAD As System.Windows.Forms.Label
    Friend WithEvents lblKakunin As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblKakunin2 As System.Windows.Forms.Label
    Friend WithEvents txtBlockInfo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
