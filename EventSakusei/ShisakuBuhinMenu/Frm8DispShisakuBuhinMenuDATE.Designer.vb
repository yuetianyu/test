Namespace ShisakuBuhinMenu
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm8DispShisakuBuhinMenuDATE
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm8DispShisakuBuhinMenuDATE))
            Me.dtpKaiteiUketukebi = New System.Windows.Forms.DateTimePicker
            Me.btnCANCEL = New System.Windows.Forms.Button
            Me.btnOK = New System.Windows.Forms.Button
            Me.Label1 = New System.Windows.Forms.Label
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.Label4 = New System.Windows.Forms.Label
            Me.lblKakunin = New System.Windows.Forms.Label
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'dtpKaiteiUketukebi
            '
            Me.dtpKaiteiUketukebi.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.dtpKaiteiUketukebi.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.dtpKaiteiUketukebi.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpKaiteiUketukebi.Location = New System.Drawing.Point(112, 70)
            Me.dtpKaiteiUketukebi.Name = "dtpKaiteiUketukebi"
            Me.dtpKaiteiUketukebi.Size = New System.Drawing.Size(112, 22)
            Me.dtpKaiteiUketukebi.TabIndex = 67
            '
            'btnCANCEL
            '
            Me.btnCANCEL.Location = New System.Drawing.Point(149, 103)
            Me.btnCANCEL.Name = "btnCANCEL"
            Me.btnCANCEL.Size = New System.Drawing.Size(75, 23)
            Me.btnCANCEL.TabIndex = 69
            Me.btnCANCEL.Text = "CANCEL"
            Me.btnCANCEL.UseVisualStyleBackColor = True
            '
            'btnOK
            '
            Me.btnOK.Location = New System.Drawing.Point(46, 103)
            Me.btnOK.Name = "btnOK"
            Me.btnOK.Size = New System.Drawing.Size(75, 23)
            Me.btnOK.TabIndex = 68
            Me.btnOK.Text = "ＯＫ"
            Me.btnOK.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.Location = New System.Drawing.Point(43, 67)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(68, 26)
            Me.Label1.TabIndex = 72
            Me.Label1.Text = "〆切日："
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(279, 32)
            Me.Panel1.TabIndex = 73
            '
            'Label4
            '
            Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label4.ForeColor = System.Drawing.Color.Yellow
            Me.Label4.Location = New System.Drawing.Point(88, 1)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(106, 31)
            Me.Label4.TabIndex = 54
            Me.Label4.Text = "確認"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblKakunin
            '
            Me.lblKakunin.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblKakunin.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblKakunin.ForeColor = System.Drawing.Color.Red
            Me.lblKakunin.Location = New System.Drawing.Point(0, 32)
            Me.lblKakunin.Name = "lblKakunin"
            Me.lblKakunin.Size = New System.Drawing.Size(279, 26)
            Me.lblKakunin.TabIndex = 74
            Me.lblKakunin.Text = "設計処置〆切りを実行しますか？"
            Me.lblKakunin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Frm8DispShisakuBuhinMenuDATE
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(279, 138)
            Me.Controls.Add(Me.lblKakunin)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.btnCANCEL)
            Me.Controls.Add(Me.btnOK)
            Me.Controls.Add(Me.dtpKaiteiUketukebi)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "Frm8DispShisakuBuhinMenuDATE"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents dtpKaiteiUketukebi As System.Windows.Forms.DateTimePicker
        Friend WithEvents btnCANCEL As System.Windows.Forms.Button
        Friend WithEvents btnOK As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents lblKakunin As System.Windows.Forms.Label
    End Class
End Namespace
