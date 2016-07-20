<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm04Kakunin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm04Kakunin))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblHEAD = New System.Windows.Forms.Label
        Me.lblKakunin = New System.Windows.Forms.Label
        Me.lblKakunin2 = New System.Windows.Forms.Label
        Me.DGView1 = New System.Windows.Forms.DataGridView
        Me.BlockNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.KaiteiNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Jyoutai = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Tanto = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CANCEL = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        CType(Me.DGView1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(421, 32)
        Me.Panel1.TabIndex = 54
        '
        'lblHEAD
        '
        Me.lblHEAD.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHEAD.ForeColor = System.Drawing.Color.Yellow
        Me.lblHEAD.Location = New System.Drawing.Point(12, 1)
        Me.lblHEAD.Name = "lblHEAD"
        Me.lblHEAD.Size = New System.Drawing.Size(409, 31)
        Me.lblHEAD.TabIndex = 54
        Me.lblHEAD.Text = "試作部品表 編集・改訂編集（ブロック）"
        Me.lblHEAD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblKakunin
        '
        Me.lblKakunin.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin.ForeColor = System.Drawing.Color.Red
        Me.lblKakunin.Location = New System.Drawing.Point(0, 34)
        Me.lblKakunin.Name = "lblKakunin"
        Me.lblKakunin.Size = New System.Drawing.Size(421, 26)
        Me.lblKakunin.TabIndex = 56
        Me.lblKakunin.Text = "編集中のブロックが存在します。"
        Me.lblKakunin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblKakunin2
        '
        Me.lblKakunin2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKakunin2.Location = New System.Drawing.Point(0, 59)
        Me.lblKakunin2.Name = "lblKakunin2"
        Me.lblKakunin2.Size = New System.Drawing.Size(421, 26)
        Me.lblKakunin2.TabIndex = 58
        Me.lblKakunin2.Text = "ＡＬ再展開を実施しますか？"
        Me.lblKakunin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DGView1
        '
        Me.DGView1.AllowUserToAddRows = False
        Me.DGView1.AllowUserToDeleteRows = False
        Me.DGView1.BackgroundColor = System.Drawing.Color.White
        Me.DGView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DGView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.BlockNo, Me.KaiteiNo, Me.Jyoutai, Me.Tanto})
        Me.DGView1.GridColor = System.Drawing.Color.Cyan
        Me.DGView1.Location = New System.Drawing.Point(3, 88)
        Me.DGView1.Name = "DGView1"
        Me.DGView1.ReadOnly = True
        Me.DGView1.RowTemplate.Height = 21
        Me.DGView1.Size = New System.Drawing.Size(418, 202)
        Me.DGView1.TabIndex = 59
        '
        'BlockNo
        '
        Me.BlockNo.HeaderText = "ブロック№"
        Me.BlockNo.Name = "BlockNo"
        Me.BlockNo.ReadOnly = True
        Me.BlockNo.Width = 80
        '
        'KaiteiNo
        '
        Me.KaiteiNo.HeaderText = "改訂№"
        Me.KaiteiNo.Name = "KaiteiNo"
        Me.KaiteiNo.ReadOnly = True
        Me.KaiteiNo.Width = 70
        '
        'Jyoutai
        '
        Me.Jyoutai.HeaderText = "状態"
        Me.Jyoutai.Name = "Jyoutai"
        Me.Jyoutai.ReadOnly = True
        '
        'Tanto
        '
        Me.Tanto.HeaderText = "担当者"
        Me.Tanto.Name = "Tanto"
        Me.Tanto.ReadOnly = True
        '
        'CANCEL
        '
        Me.CANCEL.Location = New System.Drawing.Point(220, 296)
        Me.CANCEL.Name = "CANCEL"
        Me.CANCEL.Size = New System.Drawing.Size(75, 23)
        Me.CANCEL.TabIndex = 61
        Me.CANCEL.Text = "CANCEL"
        Me.CANCEL.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Location = New System.Drawing.Point(133, 296)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 60
        Me.OK.Text = "ＯＫ"
        Me.OK.UseVisualStyleBackColor = True
        '
        'frm04Kakunin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(421, 324)
        Me.Controls.Add(Me.CANCEL)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.DGView1)
        Me.Controls.Add(Me.lblKakunin2)
        Me.Controls.Add(Me.lblKakunin)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm04Kakunin"
        Me.Text = "新試作手配システム Ver 1.00"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        CType(Me.DGView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblHEAD As System.Windows.Forms.Label
    Friend WithEvents lblKakunin As System.Windows.Forms.Label
    Friend WithEvents lblKakunin2 As System.Windows.Forms.Label
    Friend WithEvents DGView1 As System.Windows.Forms.DataGridView
    Friend WithEvents CANCEL As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents BlockNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KaiteiNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Jyoutai As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tanto As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
