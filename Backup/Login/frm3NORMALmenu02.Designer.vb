<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm3NORMALmenu02
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm3NORMALmenu02))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblCurrBukaName = New System.Windows.Forms.Label
        Me.LblCurrUserId = New System.Windows.Forms.Label
        Me.LblTimeNow = New System.Windows.Forms.Label
        Me.LblDateNow = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblBuHin01 = New System.Windows.Forms.Button
        Me.lblBuHin02 = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnShisakuEvent = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnEND = New System.Windows.Forms.Button
        Me.lblTeCyou = New System.Windows.Forms.Button
        Me.lblBuHin03 = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnBuHin05 = New System.Windows.Forms.Button
        Me.btnSearch01 = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.BackColor = System.Drawing.Color.Blue
        Me.Panel1.Controls.Add(Me.LblCurrBukaName)
        Me.Panel1.Controls.Add(Me.LblCurrUserId)
        Me.Panel1.Controls.Add(Me.LblTimeNow)
        Me.Panel1.Controls.Add(Me.LblDateNow)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(574, 32)
        Me.Panel1.TabIndex = 54
        '
        'LblCurrBukaName
        '
        Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
        Me.LblCurrBukaName.Location = New System.Drawing.Point(349, 13)
        Me.LblCurrBukaName.Name = "LblCurrBukaName"
        Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrBukaName.TabIndex = 81
        Me.LblCurrBukaName.Text = "(部課：SKE1         )"
        '
        'LblCurrUserId
        '
        Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
        Me.LblCurrUserId.Location = New System.Drawing.Point(349, 0)
        Me.LblCurrUserId.Name = "LblCurrUserId"
        Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrUserId.TabIndex = 80
        Me.LblCurrUserId.Text = "(ID    ：ABCDEFGH)"
        '
        'LblTimeNow
        '
        Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTimeNow.AutoSize = True
        Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTimeNow.ForeColor = System.Drawing.Color.White
        Me.LblTimeNow.Location = New System.Drawing.Point(509, 13)
        Me.LblTimeNow.Name = "LblTimeNow"
        Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
        Me.LblTimeNow.TabIndex = 79
        Me.LblTimeNow.Text = "HH:MM:DD"
        '
        'LblDateNow
        '
        Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDateNow.AutoSize = True
        Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDateNow.ForeColor = System.Drawing.Color.White
        Me.LblDateNow.Location = New System.Drawing.Point(485, 0)
        Me.LblDateNow.Name = "LblDateNow"
        Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
        Me.LblDateNow.TabIndex = 78
        Me.LblDateNow.Text = "YYYY/MM/DD"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 13)
        Me.Label2.TabIndex = 59
        Me.Label2.Text = "PG-ID：MENU003"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Yellow
        Me.Label4.Location = New System.Drawing.Point(182, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 31)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "【設計】　メニュー"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.AutoSize = True
        Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 247)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(574, 25)
        Me.Panel3.TabIndex = 71
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(1, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(570, 22)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "ボタンをクリックしてください。"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBuHin01
        '
        Me.lblBuHin01.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBuHin01.Location = New System.Drawing.Point(76, 89)
        Me.lblBuHin01.Name = "lblBuHin01"
        Me.lblBuHin01.Size = New System.Drawing.Size(189, 34)
        Me.lblBuHin01.TabIndex = 1
        Me.lblBuHin01.Text = "試作部品表　      編集"
        Me.ToolTip1.SetToolTip(Me.lblBuHin01, "試作部品表を作成します。")
        Me.lblBuHin01.UseVisualStyleBackColor = True
        '
        'lblBuHin02
        '
        Me.lblBuHin02.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBuHin02.Location = New System.Drawing.Point(294, 89)
        Me.lblBuHin02.Name = "lblBuHin02"
        Me.lblBuHin02.Size = New System.Drawing.Size(189, 34)
        Me.lblBuHin02.TabIndex = 2
        Me.lblBuHin02.Text = "試作部品表　改訂編集"
        Me.ToolTip1.SetToolTip(Me.lblBuHin02, "改訂処置を行います。")
        Me.lblBuHin02.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnShisakuEvent)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.btnEND)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 32)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(574, 34)
        Me.Panel2.TabIndex = 74
        '
        'btnShisakuEvent
        '
        Me.btnShisakuEvent.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnShisakuEvent.ForeColor = System.Drawing.Color.Blue
        Me.btnShisakuEvent.Location = New System.Drawing.Point(460, 5)
        Me.btnShisakuEvent.Name = "btnShisakuEvent"
        Me.btnShisakuEvent.Size = New System.Drawing.Size(109, 24)
        Me.btnShisakuEvent.TabIndex = 83
        Me.btnShisakuEvent.Text = "試作イベント管理"
        Me.ToolTip1.SetToolTip(Me.btnShisakuEvent, "試作イベント情報を確認します。")
        Me.btnShisakuEvent.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Yellow
        Me.Label6.Image = Global.Login.My.Resources.Resources.ロゴ
        Me.Label6.Location = New System.Drawing.Point(218, 2)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 26)
        Me.Label6.TabIndex = 82
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnEND
        '
        Me.btnEND.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEND.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEND.Location = New System.Drawing.Point(3, 3)
        Me.btnEND.Name = "btnEND"
        Me.btnEND.Size = New System.Drawing.Size(149, 24)
        Me.btnEND.TabIndex = 6
        Me.btnEND.Text = "アプリケーション終了"
        Me.ToolTip1.SetToolTip(Me.btnEND, "新試作手配システムを終了します。")
        Me.btnEND.UseVisualStyleBackColor = True
        '
        'lblTeCyou
        '
        Me.lblTeCyou.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTeCyou.Location = New System.Drawing.Point(240, 208)
        Me.lblTeCyou.Name = "lblTeCyou"
        Me.lblTeCyou.Size = New System.Drawing.Size(189, 34)
        Me.lblTeCyou.TabIndex = 4
        Me.lblTeCyou.Text = "手配帳　　　　リスト出力"
        Me.ToolTip1.SetToolTip(Me.lblTeCyou, "手配帳をEXCEL出力します。")
        Me.lblTeCyou.UseVisualStyleBackColor = True
        Me.lblTeCyou.Visible = False
        '
        'lblBuHin03
        '
        Me.lblBuHin03.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBuHin03.Location = New System.Drawing.Point(76, 160)
        Me.lblBuHin03.Name = "lblBuHin03"
        Me.lblBuHin03.Size = New System.Drawing.Size(189, 34)
        Me.lblBuHin03.TabIndex = 3
        Me.lblBuHin03.Text = "試作部品表　リスト出力"
        Me.ToolTip1.SetToolTip(Me.lblBuHin03, "試作部品表をEXCEL出力します。")
        Me.lblBuHin03.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'btnBuHin05
        '
        Me.btnBuHin05.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBuHin05.Location = New System.Drawing.Point(443, 217)
        Me.btnBuHin05.Name = "btnBuHin05"
        Me.btnBuHin05.Size = New System.Drawing.Size(109, 24)
        Me.btnBuHin05.TabIndex = 5
        Me.btnBuHin05.Text = "終了イベント"
        Me.ToolTip1.SetToolTip(Me.btnBuHin05, "終了イベントを確認します。")
        Me.btnBuHin05.UseVisualStyleBackColor = True
        '
        'btnSearch01
        '
        Me.btnSearch01.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch01.Location = New System.Drawing.Point(296, 160)
        Me.btnSearch01.Name = "btnSearch01"
        Me.btnSearch01.Size = New System.Drawing.Size(189, 34)
        Me.btnSearch01.TabIndex = 82
        Me.btnSearch01.Text = "過去購入品コスト検索"
        Me.btnSearch01.UseVisualStyleBackColor = True
        '
        'frm3NORMALmenu02
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(574, 272)
        Me.Controls.Add(Me.btnSearch01)
        Me.Controls.Add(Me.btnBuHin05)
        Me.Controls.Add(Me.lblTeCyou)
        Me.Controls.Add(Me.lblBuHin03)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.lblBuHin02)
        Me.Controls.Add(Me.lblBuHin01)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frm3NORMALmenu02"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblBuHin01 As System.Windows.Forms.Button
    Friend WithEvents lblBuHin02 As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblTeCyou As System.Windows.Forms.Button
    Friend WithEvents lblBuHin03 As System.Windows.Forms.Button
    Friend WithEvents btnEND As System.Windows.Forms.Button
    Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
    Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
    Friend WithEvents LblTimeNow As System.Windows.Forms.Label
    Friend WithEvents LblDateNow As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnBuHin05 As System.Windows.Forms.Button
    Friend WithEvents btnShisakuEvent As System.Windows.Forms.Button
    Friend WithEvents btnSearch01 As System.Windows.Forms.Button
End Class
