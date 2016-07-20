<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HoyouBuhinFrm41DispShisakuBuhinEdit30
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HoyouBuhinFrm41DispShisakuBuhinEdit30))
        Me.chkListBoxGosya = New System.Windows.Forms.CheckedListBox
        Me.SuspendLayout()
        '
        'chkListBoxGosya
        '
        Me.chkListBoxGosya.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkListBoxGosya.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkListBoxGosya.FormattingEnabled = True
        Me.chkListBoxGosya.Location = New System.Drawing.Point(0, 0)
        Me.chkListBoxGosya.Name = "chkListBoxGosya"
        Me.chkListBoxGosya.Size = New System.Drawing.Size(284, 424)
        Me.chkListBoxGosya.TabIndex = 0
        '
        'HoyouBuhinFrm41DispShisakuBuhinEdit30
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(284, 418)
        Me.Controls.Add(Me.chkListBoxGosya)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "HoyouBuhinFrm41DispShisakuBuhinEdit30"
        Me.Text = "新試作手配システム Ver 1.00"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkListBoxGosya As System.Windows.Forms.CheckedListBox
End Class
