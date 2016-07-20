<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HoyouBuhinFrmVeiwerImge
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HoyouBuhinFrmVeiwerImge))
        Me.AxXVLPlayer = New AxXVLPlayer3Lib.AxXVLView
        Me.lblHinban = New System.Windows.Forms.Label
        CType(Me.AxXVLPlayer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxXVLPlayer
        '
        Me.AxXVLPlayer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxXVLPlayer.Enabled = True
        Me.AxXVLPlayer.Location = New System.Drawing.Point(12, 29)
        Me.AxXVLPlayer.Name = "AxXVLPlayer"
        Me.AxXVLPlayer.OcxState = CType(resources.GetObject("AxXVLPlayer.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxXVLPlayer.Size = New System.Drawing.Size(342, 259)
        Me.AxXVLPlayer.TabIndex = 0
        '
        'lblHinban
        '
        Me.lblHinban.BackColor = System.Drawing.SystemColors.Control
        Me.lblHinban.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHinban.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHinban.ForeColor = System.Drawing.Color.Black
        Me.lblHinban.Location = New System.Drawing.Point(0, 0)
        Me.lblHinban.Name = "lblHinban"
        Me.lblHinban.Size = New System.Drawing.Size(366, 25)
        Me.lblHinban.TabIndex = 1
        Me.lblHinban.Text = "99999XX999 XXX"
        Me.lblHinban.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'HoyouBuhinFrmVeiwerImge
        '
        Me.ClientSize = New System.Drawing.Size(366, 300)
        Me.Controls.Add(Me.lblHinban)
        Me.Controls.Add(Me.AxXVLPlayer)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "HoyouBuhinFrmVeiwerImge"
        CType(Me.AxXVLPlayer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AxXVLPlayer As AxXVLPlayer3Lib.AxXVLView
    Friend WithEvents lblHinban As System.Windows.Forms.Label

End Class
