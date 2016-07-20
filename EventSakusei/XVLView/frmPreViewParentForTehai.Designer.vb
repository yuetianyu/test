<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreViewParentForTehai
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPreViewParentForTehai))
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlRedraw = New System.Windows.Forms.Panel
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.miWin = New System.Windows.Forms.ToolStripMenuItem
        Me.miWinCascade = New System.Windows.Forms.ToolStripMenuItem
        Me.miWinTileHorizontal = New System.Windows.Forms.ToolStripMenuItem
        Me.miWinTileVertical = New System.Windows.Forms.ToolStripMenuItem
        Me.MsTool1 = New System.Windows.Forms.ToolStripMenuItem
        Me.msBuhinKouseiHyoujiTool = New System.Windows.Forms.ToolStripMenuItem
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnBody = New System.Windows.Forms.Button
        Me.LOGO = New System.Windows.Forms.Label
        Me.btnBack = New System.Windows.Forms.Button
        Me.MenuStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlRedraw
        '
        Me.pnlRedraw.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlRedraw.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.pnlRedraw.Location = New System.Drawing.Point(12, 51)
        Me.pnlRedraw.Name = "pnlRedraw"
        Me.pnlRedraw.Size = New System.Drawing.Size(85, 0)
        Me.pnlRedraw.TabIndex = 156
        Me.pnlRedraw.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miWin, Me.MsTool1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1014, 24)
        Me.MenuStrip1.TabIndex = 159
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'miWin
        '
        Me.miWin.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miWinCascade, Me.miWinTileHorizontal, Me.miWinTileVertical})
        Me.miWin.Name = "miWin"
        Me.miWin.Size = New System.Drawing.Size(77, 20)
        Me.miWin.Text = "ウィンドウ(&W)"
        '
        'miWinCascade
        '
        Me.miWinCascade.Name = "miWinCascade"
        Me.miWinCascade.Size = New System.Drawing.Size(174, 22)
        Me.miWinCascade.Text = "重ねて表示(&C)"
        '
        'miWinTileHorizontal
        '
        Me.miWinTileHorizontal.Name = "miWinTileHorizontal"
        Me.miWinTileHorizontal.Size = New System.Drawing.Size(174, 22)
        Me.miWinTileHorizontal.Text = "左右に並べて表示(&H)"
        '
        'miWinTileVertical
        '
        Me.miWinTileVertical.Name = "miWinTileVertical"
        Me.miWinTileVertical.Size = New System.Drawing.Size(174, 22)
        Me.miWinTileVertical.Text = "上下に並べて表示(&V)"
        '
        'MsTool1
        '
        Me.MsTool1.BackColor = System.Drawing.SystemColors.Control
        Me.MsTool1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msBuhinKouseiHyoujiTool})
        Me.MsTool1.Name = "MsTool1"
        Me.MsTool1.Size = New System.Drawing.Size(61, 20)
        Me.MsTool1.Text = "ツール(&T)"
        Me.MsTool1.Visible = False
        '
        'msBuhinKouseiHyoujiTool
        '
        Me.msBuhinKouseiHyoujiTool.Name = "msBuhinKouseiHyoujiTool"
        Me.msBuhinKouseiHyoujiTool.Size = New System.Drawing.Size(187, 22)
        Me.msBuhinKouseiHyoujiTool.Text = "部品構成表示ツール(&D)"
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnBody)
        Me.Panel2.Controls.Add(Me.LOGO)
        Me.Panel2.Controls.Add(Me.btnBack)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 24)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1014, 32)
        Me.Panel2.TabIndex = 160
        '
        'btnBody
        '
        Me.btnBody.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBody.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBody.Location = New System.Drawing.Point(877, 3)
        Me.btnBody.Name = "btnBody"
        Me.btnBody.Size = New System.Drawing.Size(66, 23)
        Me.btnBody.TabIndex = 83
        Me.btnBody.Text = "BODY"
        Me.btnBody.UseVisualStyleBackColor = True
        '
        'LOGO
        '
        Me.LOGO.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LOGO.BackColor = System.Drawing.Color.White
        Me.LOGO.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LOGO.ForeColor = System.Drawing.Color.Yellow
        Me.LOGO.Image = CType(resources.GetObject("LOGO.Image"), System.Drawing.Image)
        Me.LOGO.Location = New System.Drawing.Point(400, 1)
        Me.LOGO.Name = "LOGO"
        Me.LOGO.Size = New System.Drawing.Size(168, 28)
        Me.LOGO.TabIndex = 82
        Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.SystemColors.Control
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBack.Location = New System.Drawing.Point(946, 3)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(64, 24)
        Me.btnBack.TabIndex = 54
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'frmPreViewParentForTehai
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 673)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.pnlRedraw)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "frmPreViewParentForTehai"
        Me.Text = "3Dツール"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents pnlRedraw As System.Windows.Forms.Panel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents miWin As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miWinCascade As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miWinTileHorizontal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miWinTileVertical As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MsTool1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msBuhinKouseiHyoujiTool As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnBody As System.Windows.Forms.Button
    Friend WithEvents LOGO As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
End Class
