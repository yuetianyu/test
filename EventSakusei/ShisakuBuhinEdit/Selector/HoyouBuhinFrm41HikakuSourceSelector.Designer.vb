Namespace ShisakuBuhinEdit.Selector
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class HoyouBuhinFrm41HikakuSourceSelector
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HoyouBuhinFrm41HikakuSourceSelector))
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.txtMoto = New System.Windows.Forms.TextBox
            Me.rdoBtnEbom = New System.Windows.Forms.RadioButton
            Me.rdoBtnBase = New System.Windows.Forms.RadioButton
            Me.Label1 = New System.Windows.Forms.Label
            Me.txtSaki = New System.Windows.Forms.TextBox
            Me.Label3 = New System.Windows.Forms.Label
            Me.Label2 = New System.Windows.Forms.Label
            Me.btnBack = New System.Windows.Forms.Button
            Me.btnSearch = New System.Windows.Forms.Button
            Me.Label4 = New System.Windows.Forms.Label
            Me.Label5 = New System.Windows.Forms.Label
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.rdoBtnEbom)
            Me.Panel1.Controls.Add(Me.rdoBtnBase)
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Location = New System.Drawing.Point(52, 26)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(164, 50)
            Me.Panel1.TabIndex = 0
            '
            'txtMoto
            '
            Me.txtMoto.Location = New System.Drawing.Point(103, 82)
            Me.txtMoto.Name = "txtMoto"
            Me.txtMoto.Size = New System.Drawing.Size(157, 19)
            Me.txtMoto.TabIndex = 2
            '
            'rdoBtnEbom
            '
            Me.rdoBtnEbom.AutoSize = True
            Me.rdoBtnEbom.Location = New System.Drawing.Point(50, 30)
            Me.rdoBtnEbom.Name = "rdoBtnEbom"
            Me.rdoBtnEbom.Size = New System.Drawing.Size(89, 16)
            Me.rdoBtnEbom.TabIndex = 1
            Me.rdoBtnEbom.Text = "E-BOM 情報"
            Me.rdoBtnEbom.UseVisualStyleBackColor = True
            '
            'rdoBtnBase
            '
            Me.rdoBtnBase.AutoSize = True
            Me.rdoBtnBase.Checked = True
            Me.rdoBtnBase.Location = New System.Drawing.Point(50, 8)
            Me.rdoBtnBase.Name = "rdoBtnBase"
            Me.rdoBtnBase.Size = New System.Drawing.Size(88, 16)
            Me.rdoBtnBase.TabIndex = 0
            Me.rdoBtnBase.Text = "ベース部品表"
            Me.rdoBtnBase.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(3, 10)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(41, 12)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "比較元"
            '
            'txtSaki
            '
            Me.txtSaki.Location = New System.Drawing.Point(103, 132)
            Me.txtSaki.Name = "txtSaki"
            Me.txtSaki.Size = New System.Drawing.Size(157, 19)
            Me.txtSaki.TabIndex = 3
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
            Me.Label3.Location = New System.Drawing.Point(55, 163)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(153, 12)
            Me.Label3.TabIndex = 1
            Me.Label3.Text = "※比較先は最新E-BOMです。"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(55, 135)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(41, 12)
            Me.Label2.TabIndex = 0
            Me.Label2.Text = "比較先"
            '
            'btnBack
            '
            Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
            Me.btnBack.Location = New System.Drawing.Point(148, 197)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(79, 24)
            Me.btnBack.TabIndex = 5
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = True
            '
            'btnSearch
            '
            Me.btnSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.0!)
            Me.btnSearch.Location = New System.Drawing.Point(233, 197)
            Me.btnSearch.Name = "btnSearch"
            Me.btnSearch.Size = New System.Drawing.Size(79, 24)
            Me.btnSearch.TabIndex = 4
            Me.btnSearch.Text = "検索"
            Me.btnSearch.UseVisualStyleBackColor = True
            '
            'Label4
            '
            Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Label4.Location = New System.Drawing.Point(16, 14)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(296, 103)
            Me.Label4.TabIndex = 6
            '
            'Label5
            '
            Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Label5.Location = New System.Drawing.Point(16, 116)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(296, 67)
            Me.Label5.TabIndex = 7
            '
            'HoyouBuhinFrm41HikakuSourceSelector
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(329, 236)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.txtSaki)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.txtMoto)
            Me.Controls.Add(Me.btnSearch)
            Me.Controls.Add(Me.btnBack)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Label5)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "HoyouBuhinFrm41HikakuSourceSelector"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents txtMoto As System.Windows.Forms.TextBox
        Friend WithEvents rdoBtnEbom As System.Windows.Forms.RadioButton
        Friend WithEvents rdoBtnBase As System.Windows.Forms.RadioButton
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents txtSaki As System.Windows.Forms.TextBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents btnBack As System.Windows.Forms.Button
        Friend WithEvents btnSearch As System.Windows.Forms.Button
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
    End Class
End Namespace