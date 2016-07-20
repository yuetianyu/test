Namespace YosanshoEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FrmInsertMonthKakunin
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInsertMonthKakunin))
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.lblHEAD = New System.Windows.Forms.Label
            Me.lblKakunin2 = New System.Windows.Forms.Label
            Me.lblKakunin1 = New System.Windows.Forms.Label
            Me.btnModosu = New System.Windows.Forms.Button
            Me.btnHanei = New System.Windows.Forms.Button
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
            Me.Panel1.Size = New System.Drawing.Size(277, 32)
            Me.Panel1.TabIndex = 55
            '
            'lblHEAD
            '
            Me.lblHEAD.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblHEAD.ForeColor = System.Drawing.Color.Yellow
            Me.lblHEAD.Location = New System.Drawing.Point(33, 1)
            Me.lblHEAD.Name = "lblHEAD"
            Me.lblHEAD.Size = New System.Drawing.Size(218, 31)
            Me.lblHEAD.TabIndex = 54
            Me.lblHEAD.Text = "確認"
            Me.lblHEAD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblKakunin2
            '
            Me.lblKakunin2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblKakunin2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblKakunin2.ForeColor = System.Drawing.Color.Red
            Me.lblKakunin2.Location = New System.Drawing.Point(52, 61)
            Me.lblKakunin2.Name = "lblKakunin2"
            Me.lblKakunin2.Size = New System.Drawing.Size(173, 40)
            Me.lblKakunin2.TabIndex = 60
            Me.lblKakunin2.Text = "[2016年上期]"
            Me.lblKakunin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblKakunin1
            '
            Me.lblKakunin1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblKakunin1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblKakunin1.ForeColor = System.Drawing.Color.Red
            Me.lblKakunin1.Location = New System.Drawing.Point(-5, 31)
            Me.lblKakunin1.Name = "lblKakunin1"
            Me.lblKakunin1.Size = New System.Drawing.Size(287, 43)
            Me.lblKakunin1.TabIndex = 59
            Me.lblKakunin1.Text = "下記の通り列を挿入します。"
            Me.lblKakunin1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnModosu
            '
            Me.btnModosu.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
            Me.btnModosu.Location = New System.Drawing.Point(161, 119)
            Me.btnModosu.Name = "btnModosu"
            Me.btnModosu.Size = New System.Drawing.Size(80, 23)
            Me.btnModosu.TabIndex = 62
            Me.btnModosu.Text = "いいえ"
            Me.btnModosu.UseVisualStyleBackColor = True
            '
            'btnHanei
            '
            Me.btnHanei.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
            Me.btnHanei.Location = New System.Drawing.Point(29, 119)
            Me.btnHanei.Name = "btnHanei"
            Me.btnHanei.Size = New System.Drawing.Size(80, 23)
            Me.btnHanei.TabIndex = 61
            Me.btnHanei.Text = "はい"
            Me.btnHanei.UseVisualStyleBackColor = True
            '
            'FrmInsertMonthKakunin
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(277, 165)
            Me.Controls.Add(Me.btnModosu)
            Me.Controls.Add(Me.btnHanei)
            Me.Controls.Add(Me.lblKakunin2)
            Me.Controls.Add(Me.lblKakunin1)
            Me.Controls.Add(Me.Panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "FrmInsertMonthKakunin"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents lblHEAD As System.Windows.Forms.Label
        Friend WithEvents lblKakunin2 As System.Windows.Forms.Label
        Friend WithEvents lblKakunin1 As System.Windows.Forms.Label
        Friend WithEvents btnModosu As System.Windows.Forms.Button
        Friend WithEvents btnHanei As System.Windows.Forms.Button
    End Class
End Namespace