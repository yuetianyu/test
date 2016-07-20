<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm2NORMALmenu01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm2NORMALmenu01))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblCurrBukaName = New System.Windows.Forms.Label
        Me.LblCurrUserId = New System.Windows.Forms.Label
        Me.LblTimeNow = New System.Windows.Forms.Label
        Me.LblDateNow = New System.Windows.Forms.Label
        Me.pgId = New System.Windows.Forms.Label
        Me.menuTitle = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnBuHin01 = New System.Windows.Forms.Button
        Me.btnBuHin02 = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnEND = New System.Windows.Forms.Button
        Me.btnBuHin03 = New System.Windows.Forms.Button
        Me.btnBuHin04 = New System.Windows.Forms.Button
        Me.btnTeCyou = New System.Windows.Forms.Button
        Me.btnKuruma = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnBuHin05 = New System.Windows.Forms.Button
        Me.btnBuHin06 = New System.Windows.Forms.Button
        Me.btnView3D = New System.Windows.Forms.Button
        Me.btnFindCost = New System.Windows.Forms.Button
        Me.btnOrderSheet = New System.Windows.Forms.Button
        Me.btnGousyabetsuShiyousyo = New System.Windows.Forms.Button
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
        Me.Panel1.Controls.Add(Me.pgId)
        Me.Panel1.Controls.Add(Me.menuTitle)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(570, 32)
        Me.Panel1.TabIndex = 54
        '
        'LblCurrBukaName
        '
        Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
        Me.LblCurrBukaName.Location = New System.Drawing.Point(345, 13)
        Me.LblCurrBukaName.Name = "LblCurrBukaName"
        Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrBukaName.TabIndex = 77
        Me.LblCurrBukaName.Text = "(部課：SKE1         )"
        '
        'LblCurrUserId
        '
        Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
        Me.LblCurrUserId.Location = New System.Drawing.Point(345, 0)
        Me.LblCurrUserId.Name = "LblCurrUserId"
        Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrUserId.TabIndex = 76
        Me.LblCurrUserId.Text = "(ID    ：ABCDEFGH)"
        '
        'LblTimeNow
        '
        Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTimeNow.AutoSize = True
        Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTimeNow.ForeColor = System.Drawing.Color.White
        Me.LblTimeNow.Location = New System.Drawing.Point(505, 13)
        Me.LblTimeNow.Name = "LblTimeNow"
        Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
        Me.LblTimeNow.TabIndex = 75
        Me.LblTimeNow.Text = "HH:MM:DD"
        '
        'LblDateNow
        '
        Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDateNow.AutoSize = True
        Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDateNow.ForeColor = System.Drawing.Color.White
        Me.LblDateNow.Location = New System.Drawing.Point(481, 0)
        Me.LblDateNow.Name = "LblDateNow"
        Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
        Me.LblDateNow.TabIndex = 74
        Me.LblDateNow.Text = "YYYY/MM/DD"
        '
        'pgId
        '
        Me.pgId.AutoSize = True
        Me.pgId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pgId.ForeColor = System.Drawing.Color.White
        Me.pgId.Location = New System.Drawing.Point(3, 1)
        Me.pgId.Name = "pgId"
        Me.pgId.Size = New System.Drawing.Size(103, 13)
        Me.pgId.TabIndex = 59
        Me.pgId.Text = "PG-ID：MENU002"
        '
        'menuTitle
        '
        Me.menuTitle.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.menuTitle.ForeColor = System.Drawing.Color.Yellow
        Me.menuTitle.Location = New System.Drawing.Point(182, 1)
        Me.menuTitle.Name = "menuTitle"
        Me.menuTitle.Size = New System.Drawing.Size(139, 31)
        Me.menuTitle.TabIndex = 54
        Me.menuTitle.Text = "【SKE1】　メニュー"
        Me.menuTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.AutoSize = True
        Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 321)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(570, 25)
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
        'btnBuHin01
        '
        Me.btnBuHin01.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBuHin01.Location = New System.Drawing.Point(76, 81)
        Me.btnBuHin01.Name = "btnBuHin01"
        Me.btnBuHin01.Size = New System.Drawing.Size(189, 34)
        Me.btnBuHin01.TabIndex = 1
        Me.btnBuHin01.Text = "試作部品表作成"
        Me.btnBuHin01.UseVisualStyleBackColor = True
        '
        'btnBuHin02
        '
        Me.btnBuHin02.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBuHin02.Location = New System.Drawing.Point(294, 81)
        Me.btnBuHin02.Name = "btnBuHin02"
        Me.btnBuHin02.Size = New System.Drawing.Size(189, 34)
        Me.btnBuHin02.TabIndex = 3
        Me.btnBuHin02.Text = "試作部品表　      編集"
        Me.btnBuHin02.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.btnEND)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 32)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(570, 32)
        Me.Panel2.TabIndex = 74
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Yellow
        Me.Label6.Image = Global.Login.My.Resources.Resources.ロゴ
        Me.Label6.Location = New System.Drawing.Point(208, 2)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 26)
        Me.Label6.TabIndex = 81
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnEND
        '
        Me.btnEND.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEND.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEND.Location = New System.Drawing.Point(4, 3)
        Me.btnEND.Name = "btnEND"
        Me.btnEND.Size = New System.Drawing.Size(149, 24)
        Me.btnEND.TabIndex = 8
        Me.btnEND.Text = "アプリケーション終了"
        Me.ToolTip1.SetToolTip(Me.btnEND, "新試作手配システムを終了します。")
        Me.btnEND.UseVisualStyleBackColor = True
        '
        'btnBuHin03
        '
        Me.btnBuHin03.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBuHin03.Location = New System.Drawing.Point(294, 121)
        Me.btnBuHin03.Name = "btnBuHin03"
        Me.btnBuHin03.Size = New System.Drawing.Size(189, 34)
        Me.btnBuHin03.TabIndex = 4
        Me.btnBuHin03.Text = "試作部品表　改訂編集"
        Me.btnBuHin03.UseVisualStyleBackColor = True
        '
        'btnBuHin04
        '
        Me.btnBuHin04.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBuHin04.Location = New System.Drawing.Point(294, 202)
        Me.btnBuHin04.Name = "btnBuHin04"
        Me.btnBuHin04.Size = New System.Drawing.Size(189, 34)
        Me.btnBuHin04.TabIndex = 6
        Me.btnBuHin04.Text = "試作部品表　リスト出力"
        Me.btnBuHin04.UseVisualStyleBackColor = True
        '
        'btnTeCyou
        '
        Me.btnTeCyou.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTeCyou.Location = New System.Drawing.Point(294, 240)
        Me.btnTeCyou.Name = "btnTeCyou"
        Me.btnTeCyou.Size = New System.Drawing.Size(189, 34)
        Me.btnTeCyou.TabIndex = 7
        Me.btnTeCyou.Text = "手配帳　　　　リスト出力"
        Me.btnTeCyou.UseVisualStyleBackColor = True
        '
        'btnKuruma
        '
        Me.btnKuruma.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKuruma.Location = New System.Drawing.Point(76, 240)
        Me.btnKuruma.Name = "btnKuruma"
        Me.btnKuruma.Size = New System.Drawing.Size(189, 34)
        Me.btnKuruma.TabIndex = 2
        Me.btnKuruma.Text = "車系/開発符号マスター"
        Me.btnKuruma.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'btnBuHin05
        '
        Me.btnBuHin05.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBuHin05.Location = New System.Drawing.Point(294, 161)
        Me.btnBuHin05.Name = "btnBuHin05"
        Me.btnBuHin05.Size = New System.Drawing.Size(189, 34)
        Me.btnBuHin05.TabIndex = 5
        Me.btnBuHin05.Text = "試作部品表　完了イベント"
        Me.btnBuHin05.UseVisualStyleBackColor = True
        '
        'btnBuHin06
        '
        Me.btnBuHin06.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBuHin06.Location = New System.Drawing.Point(76, 121)
        Me.btnBuHin06.Name = "btnBuHin06"
        Me.btnBuHin06.Size = New System.Drawing.Size(189, 34)
        Me.btnBuHin06.TabIndex = 75
        Me.btnBuHin06.Text = "試作部品表　改訂通知"
        Me.btnBuHin06.UseVisualStyleBackColor = True
        '
        'btnView3D
        '
        Me.btnView3D.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnView3D.Location = New System.Drawing.Point(76, 161)
        Me.btnView3D.Name = "btnView3D"
        Me.btnView3D.Size = New System.Drawing.Size(189, 34)
        Me.btnView3D.TabIndex = 76
        Me.btnView3D.Text = "3Dデータ表示"
        Me.btnView3D.UseVisualStyleBackColor = True
        '
        'btnFindCost
        '
        Me.btnFindCost.BackColor = System.Drawing.Color.Aquamarine
        Me.btnFindCost.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFindCost.Location = New System.Drawing.Point(76, 200)
        Me.btnFindCost.Name = "btnFindCost"
        Me.btnFindCost.Size = New System.Drawing.Size(189, 34)
        Me.btnFindCost.TabIndex = 77
        Me.btnFindCost.Text = "予算検索"
        Me.btnFindCost.UseVisualStyleBackColor = False
        '
        'btnOrderSheet
        '
        Me.btnOrderSheet.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOrderSheet.Location = New System.Drawing.Point(294, 281)
        Me.btnOrderSheet.Name = "btnOrderSheet"
        Me.btnOrderSheet.Size = New System.Drawing.Size(189, 34)
        Me.btnOrderSheet.TabIndex = 78
        Me.btnOrderSheet.Text = "現調部品手配システム"
        Me.btnOrderSheet.UseVisualStyleBackColor = True
        '
        'btnGousyabetsuShiyousyo
        '
        Me.btnGousyabetsuShiyousyo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnGousyabetsuShiyousyo.Location = New System.Drawing.Point(76, 280)
        Me.btnGousyabetsuShiyousyo.Name = "btnGousyabetsuShiyousyo"
        Me.btnGousyabetsuShiyousyo.Size = New System.Drawing.Size(189, 34)
        Me.btnGousyabetsuShiyousyo.TabIndex = 7
        Me.btnGousyabetsuShiyousyo.Text = "号車別仕様書作成"
        Me.btnGousyabetsuShiyousyo.UseVisualStyleBackColor = True
        '
        'frm2NORMALmenu01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(570, 346)
        Me.Controls.Add(Me.btnOrderSheet)
        Me.Controls.Add(Me.btnFindCost)
        Me.Controls.Add(Me.btnView3D)
        Me.Controls.Add(Me.btnBuHin06)
        Me.Controls.Add(Me.btnBuHin05)
        Me.Controls.Add(Me.btnGousyabetsuShiyousyo)
        Me.Controls.Add(Me.btnTeCyou)
        Me.Controls.Add(Me.btnKuruma)
        Me.Controls.Add(Me.btnBuHin04)
        Me.Controls.Add(Me.btnBuHin03)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnBuHin02)
        Me.Controls.Add(Me.btnBuHin01)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frm2NORMALmenu01"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents menuTitle As System.Windows.Forms.Label
    Friend WithEvents pgId As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnBuHin01 As System.Windows.Forms.Button
    Friend WithEvents btnBuHin02 As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnBuHin03 As System.Windows.Forms.Button
    Friend WithEvents btnBuHin04 As System.Windows.Forms.Button
    Friend WithEvents btnTeCyou As System.Windows.Forms.Button
    Friend WithEvents btnKuruma As System.Windows.Forms.Button
    Friend WithEvents btnEND As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents LblDateNow As System.Windows.Forms.Label
    Friend WithEvents LblTimeNow As System.Windows.Forms.Label
    Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
    Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnBuHin05 As System.Windows.Forms.Button
    Friend WithEvents btnBuHin06 As System.Windows.Forms.Button
    Friend WithEvents btnView3D As System.Windows.Forms.Button
    Friend WithEvents btnFindCost As System.Windows.Forms.Button
    Friend WithEvents btnOrderSheet As System.Windows.Forms.Button
    Friend WithEvents btnGousyabetsuShiyousyo As System.Windows.Forms.Button
End Class
