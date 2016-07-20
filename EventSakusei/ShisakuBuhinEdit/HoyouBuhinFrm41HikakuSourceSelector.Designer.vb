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
        Me.txtGen = New System.Windows.Forms.TextBox
        Me.rdoBtnEbom = New System.Windows.Forms.RadioButton
        Me.rdoBtnBase = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.txtSen = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnBack = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtGen)
        Me.Panel1.Controls.Add(Me.rdoBtnEbom)
        Me.Panel1.Controls.Add(Me.rdoBtnBase)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(34, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(278, 81)
        Me.Panel1.TabIndex = 0
        '
        'txtGen
        '
        Me.txtGen.Location = New System.Drawing.Point(92, 48)
        Me.txtGen.Name = "txtGen"
        Me.txtGen.Size = New System.Drawing.Size(155, 19)
        Me.txtGen.TabIndex = 3
        '
        'rdoBtnEbom
        '
        Me.rdoBtnEbom.AutoSize = True
        Me.rdoBtnEbom.Location = New System.Drawing.Point(92, 27)
        Me.rdoBtnEbom.Name = "rdoBtnEbom"
        Me.rdoBtnEbom.Size = New System.Drawing.Size(98, 16)
        Me.rdoBtnEbom.TabIndex = 2
        Me.rdoBtnEbom.Text = "Ｅ－ＢＯＭ情報"
        Me.rdoBtnEbom.UseVisualStyleBackColor = True
        '
        'rdoBtnBase
        '
        Me.rdoBtnBase.AutoSize = True
        Me.rdoBtnBase.Checked = True
        Me.rdoBtnBase.Location = New System.Drawing.Point(92, 10)
        Me.rdoBtnBase.Name = "rdoBtnBase"
        Me.rdoBtnBase.Size = New System.Drawing.Size(88, 16)
        Me.rdoBtnBase.TabIndex = 1
        Me.rdoBtnBase.TabStop = True
        Me.rdoBtnBase.Text = "ベース部品表"
        Me.rdoBtnBase.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "比較元"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtSen)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Location = New System.Drawing.Point(34, 106)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(278, 62)
        Me.Panel2.TabIndex = 0
        '
        'txtSen
        '
        Me.txtSen.Location = New System.Drawing.Point(90, 10)
        Me.txtSen.Name = "txtSen"
        Me.txtSen.Size = New System.Drawing.Size(157, 19)
        Me.txtSen.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(166, 12)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "※比較先は最新Ｅ－ＢＯＭです。"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "比較先"
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(147, 178)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(75, 23)
        Me.btnBack.TabIndex = 1
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(237, 178)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'HoyouBuhinFrm41HikakuSourceSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.ClientSize = New System.Drawing.Size(343, 212)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "HoyouBuhinFrm41HikakuSourceSelector"
        Me.Text = "新試作手配システム Ver 1.00"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtGen As System.Windows.Forms.TextBox
    Friend WithEvents rdoBtnEbom As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBtnBase As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtSen As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
End Class
