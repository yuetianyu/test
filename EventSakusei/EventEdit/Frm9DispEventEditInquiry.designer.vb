Namespace EventEdit
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm9DispEventEditInquiry
        Inherits System.Windows.Forms.Form

        'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            spreadDragDrop.ClearEvent()
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm9DispEventEditInquiry))
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.Label4 = New System.Windows.Forms.Label
            Me.LblDateNow = New System.Windows.Forms.Label
            Me.LblTimeNow = New System.Windows.Forms.Label
            Me.Label1 = New System.Windows.Forms.Label
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.LOGO = New System.Windows.Forms.Label
            Me.BtnBack = New System.Windows.Forms.Button
            Me.spdOptionSpec = New FarPoint.Win.Spread.FpSpread
            Me.spdOptionSpec_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.LblMessage = New System.Windows.Forms.Label
            Me.Panel4 = New System.Windows.Forms.Panel
            Me.LblMasterSelection = New System.Windows.Forms.Label
            Me.Panel5 = New System.Windows.Forms.Panel
            Me.RdoShisaku = New System.Windows.Forms.RadioButton
            Me.RdoEBom = New System.Windows.Forms.RadioButton
            Me.CmbOptionSpec = New System.Windows.Forms.ComboBox
            Me.LblOptionSpec = New System.Windows.Forms.Label
            Me.LblSearchCondition = New System.Windows.Forms.Label
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.spdOptionSpec, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdOptionSpec_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel3.SuspendLayout()
            Me.Panel4.SuspendLayout()
            Me.Panel5.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Controls.Add(Me.LblDateNow)
            Me.Panel1.Controls.Add(Me.LblTimeNow)
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(3, 3)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(403, 32)
            Me.Panel1.TabIndex = 53
            '
            'Label4
            '
            Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label4.ForeColor = System.Drawing.Color.Yellow
            Me.Label4.Location = New System.Drawing.Point(116, 1)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(180, 31)
            Me.Label4.TabIndex = 54
            Me.Label4.Text = "装備仕様照会"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'LblDateNow
            '
            Me.LblDateNow.AutoSize = True
            Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblDateNow.ForeColor = System.Drawing.Color.White
            Me.LblDateNow.Location = New System.Drawing.Point(314, 2)
            Me.LblDateNow.Name = "LblDateNow"
            Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
            Me.LblDateNow.TabIndex = 56
            Me.LblDateNow.Text = "YYYY/MM/DD"
            '
            'LblTimeNow
            '
            Me.LblTimeNow.AutoSize = True
            Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblTimeNow.ForeColor = System.Drawing.Color.White
            Me.LblTimeNow.Location = New System.Drawing.Point(338, 15)
            Me.LblTimeNow.Name = "LblTimeNow"
            Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
            Me.LblTimeNow.TabIndex = 55
            Me.LblTimeNow.Text = "HH:MM:DD"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.White
            Me.Label1.Location = New System.Drawing.Point(4, 4)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(113, 13)
            Me.Label1.TabIndex = 54
            Me.Label1.Text = "PG-ID：XXXXXXXX"
            '
            'Panel2
            '
            Me.Panel2.AutoSize = True
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.LOGO)
            Me.Panel2.Controls.Add(Me.BtnBack)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(3, 35)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(403, 32)
            Me.Panel2.TabIndex = 55
            '
            'LOGO
            '
            Me.LOGO.BackColor = System.Drawing.Color.White
            Me.LOGO.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LOGO.ForeColor = System.Drawing.Color.Yellow
            Me.LOGO.Image = Global.EventSakusei.My.Resources.Resources.ロゴ
            Me.LOGO.Location = New System.Drawing.Point(132, 2)
            Me.LOGO.Name = "LOGO"
            Me.LOGO.Size = New System.Drawing.Size(136, 26)
            Me.LOGO.TabIndex = 83
            Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'BtnBack
            '
            Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.BtnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnBack.Location = New System.Drawing.Point(334, 3)
            Me.BtnBack.Name = "BtnBack"
            Me.BtnBack.Size = New System.Drawing.Size(64, 24)
            Me.BtnBack.TabIndex = 5
            Me.BtnBack.Text = "戻る"
            Me.BtnBack.UseVisualStyleBackColor = True
            '
            'spdOptionSpec
            '
            Me.spdOptionSpec.AccessibleDescription = "spdParts5, Sheet1, Row 0, Column 0, Ｅ／Ｇ　D-４Ｓ仕様化"
            Me.spdOptionSpec.AllowUndo = False
            Me.spdOptionSpec.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.spdOptionSpec.BackColor = System.Drawing.SystemColors.Control
            Me.spdOptionSpec.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdOptionSpec.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdOptionSpec.Location = New System.Drawing.Point(3, 135)
            Me.spdOptionSpec.Name = "spdOptionSpec"
            Me.spdOptionSpec.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdOptionSpec.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdOptionSpec_Sheet1})
            Me.spdOptionSpec.Size = New System.Drawing.Size(406, 345)
            Me.spdOptionSpec.TabIndex = 4
            Me.spdOptionSpec.TabStop = False
            Me.spdOptionSpec.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdOptionSpec.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdOptionSpec_Sheet1
            '
            Me.spdOptionSpec_Sheet1.Reset()
            Me.spdOptionSpec_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdOptionSpec_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdOptionSpec_Sheet1.ColumnCount = 1
            Me.spdOptionSpec_Sheet1.RowCount = 50
            Me.spdOptionSpec_Sheet1.AllowNoteEdit = True
            Me.spdOptionSpec_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdOptionSpec_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdOptionSpec_Sheet1.Cells.Get(0, 0).Value = "Ｅ／Ｇ　D-４Ｓ仕様化"
            Me.spdOptionSpec_Sheet1.Cells.Get(1, 0).Value = "Ｅ／Ｇ　ＥＪ２０（不動可）：燃ポンは作動"
            Me.spdOptionSpec_Sheet1.Cells.Get(2, 0).Value = "Ｅ／Ｇ搭載位置合わせ（下げ＆後退）"
            Me.spdOptionSpec_Sheet1.Cells.Get(3, 0).Value = "Ｅ／Ｇ不動可（シルエット・重量合せ）：燃ポンは作動"
            Me.spdOptionSpec_Sheet1.Cells.Get(4, 0).Value = "Ｆ席 DP変更（ＨＰ、ＳｔｒｇＣｔｒ等）"
            Me.spdOptionSpec_Sheet1.Cells.Get(5, 0).Value = "Ｒ デフ Ｒ２０５化"
            Me.spdOptionSpec_Sheet1.Cells.Get(6, 0).Value = "Ｒ席 DP合わせ"
            Me.spdOptionSpec_Sheet1.Cells.Get(7, 0).Value = "Ｔ／Ｍ　ＦＨＩ　６ＭＴ仕様"
            Me.spdOptionSpec_Sheet1.Cells.Get(8, 0).Value = "Ｔ／Ｍ　専用６ＡＴ仕様（ｱｲｼﾝ製）化"
            Me.spdOptionSpec_Sheet1.Cells.Get(9, 0).Value = "Ｔ／Ｍ　専用６ＭＴ仕様（ｱｲｼﾝ製）化"
            Me.spdOptionSpec_Sheet1.Cells.Get(10, 0).Value = "Ｔ／Ｍ不動可（シルエット・重量合せ）"
            Me.spdOptionSpec_Sheet1.Cells.Get(11, 0).Value = "トレッド合わせ"
            Me.spdOptionSpec_Sheet1.Cells.Get(12, 0).Value = "ホイールベース合わせ（Ｒドア部短縮）"
            Me.spdOptionSpec_Sheet1.Cells.Get(13, 0).Value = "吸気（前方）対応"
            Me.spdOptionSpec_Sheet1.Cells.Get(14, 0).Value = "計測済ＥＧ搭載"
            Me.spdOptionSpec_Sheet1.Cells.Get(15, 0).Value = "電動パワステ化"
            Me.spdOptionSpec_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.SystemColors.Control
            Me.spdOptionSpec_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "装備仕様"
            Me.spdOptionSpec_Sheet1.ColumnHeader.Rows.Get(0).Height = 18.0!
            Me.spdOptionSpec_Sheet1.Columns.Get(0).AllowAutoFilter = True
            Me.spdOptionSpec_Sheet1.Columns.Get(0).AllowAutoSort = True
            Me.spdOptionSpec_Sheet1.Columns.Get(0).Label = "装備仕様"
            Me.spdOptionSpec_Sheet1.Columns.Get(0).Locked = False
            Me.spdOptionSpec_Sheet1.Columns.Get(0).Width = 350.0!
            Me.spdOptionSpec_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdOptionSpec_Sheet1.Rows.Default.Height = 16.0!
            Me.spdOptionSpec_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange
            Me.spdOptionSpec_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'Panel3
            '
            Me.Panel3.AutoSize = True
            Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel3.BackColor = System.Drawing.SystemColors.Control
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Controls.Add(Me.LblMessage)
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel3.Location = New System.Drawing.Point(3, 488)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(403, 25)
            Me.Panel3.TabIndex = 56
            '
            'LblMessage
            '
            Me.LblMessage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.LblMessage.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblMessage.ForeColor = System.Drawing.Color.Red
            Me.LblMessage.Location = New System.Drawing.Point(5, 1)
            Me.LblMessage.Name = "LblMessage"
            Me.LblMessage.Size = New System.Drawing.Size(397, 22)
            Me.LblMessage.TabIndex = 55
            Me.LblMessage.Text = "「装備仕様」を選択してください。"
            Me.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Panel4
            '
            Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel4.BackColor = System.Drawing.SystemColors.Control
            Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel4.Controls.Add(Me.LblMasterSelection)
            Me.Panel4.Controls.Add(Me.Panel5)
            Me.Panel4.Controls.Add(Me.CmbOptionSpec)
            Me.Panel4.Controls.Add(Me.LblOptionSpec)
            Me.Panel4.Controls.Add(Me.LblSearchCondition)
            Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel4.Location = New System.Drawing.Point(3, 67)
            Me.Panel4.Name = "Panel4"
            Me.Panel4.Size = New System.Drawing.Size(403, 66)
            Me.Panel4.TabIndex = 84
            '
            'LblMasterSelection
            '
            Me.LblMasterSelection.AutoSize = True
            Me.LblMasterSelection.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblMasterSelection.Location = New System.Drawing.Point(14, 26)
            Me.LblMasterSelection.Name = "LblMasterSelection"
            Me.LblMasterSelection.Size = New System.Drawing.Size(71, 12)
            Me.LblMasterSelection.TabIndex = 105
            Me.LblMasterSelection.Text = "マスター選択："
            '
            'Panel5
            '
            Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel5.Controls.Add(Me.RdoShisaku)
            Me.Panel5.Controls.Add(Me.RdoEBom)
            Me.Panel5.Location = New System.Drawing.Point(87, 20)
            Me.Panel5.Name = "Panel5"
            Me.Panel5.Size = New System.Drawing.Size(266, 20)
            Me.Panel5.TabIndex = 0
            Me.ToolTip1.SetToolTip(Me.Panel5, "参照するマスターを選択します。")
            '
            'RdoShisaku
            '
            Me.RdoShisaku.AutoSize = True
            Me.RdoShisaku.Location = New System.Drawing.Point(127, 2)
            Me.RdoShisaku.Name = "RdoShisaku"
            Me.RdoShisaku.Size = New System.Drawing.Size(127, 16)
            Me.RdoShisaku.TabIndex = 2
            Me.RdoShisaku.Text = "試作システムから参照"
            Me.RdoShisaku.UseVisualStyleBackColor = True
            '
            'RdoEBom
            '
            Me.RdoEBom.AutoSize = True
            Me.RdoEBom.Checked = True
            Me.RdoEBom.Location = New System.Drawing.Point(6, 2)
            Me.RdoEBom.Name = "RdoEBom"
            Me.RdoEBom.Size = New System.Drawing.Size(103, 16)
            Me.RdoEBom.TabIndex = 1
            Me.RdoEBom.TabStop = True
            Me.RdoEBom.Text = "E-BOMから参照"
            Me.RdoEBom.UseVisualStyleBackColor = True
            '
            'CmbOptionSpec
            '
            Me.CmbOptionSpec.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.CmbOptionSpec.FormattingEnabled = True
            Me.CmbOptionSpec.Items.AddRange(New Object() {"", "Ｅ／Ｇ　D-４Ｓ仕様化", "Ｅ／Ｇ　ＥＪ２０（不動可）：燃ポンは作動", "Ｅ／Ｇ搭載位置合わせ（下げ＆後退）", "Ｅ／Ｇ不動可（シルエット・重量合せ）：燃ポンは作動", "Ｆ席 DP変更（ＨＰ、ＳｔｒｇＣｔｒ等）", "Ｒ デフ Ｒ２０５化", "Ｒ席 DP合わせ", "Ｔ／Ｍ　ＦＨＩ　６ＭＴ仕様", "Ｔ／Ｍ　専用６ＡＴ仕様（ｱｲｼﾝ製）化", "Ｔ／Ｍ　専用６ＭＴ仕様（ｱｲｼﾝ製）化", "Ｔ／Ｍ不動可（シルエット・重量合せ）", "トレッド合わせ", "ホイールベース合わせ（Ｒドア部短縮）", "吸気（前方）対応", "計測済ＥＧ搭載", "電動パワステ化"})
            Me.CmbOptionSpec.Location = New System.Drawing.Point(87, 41)
            Me.CmbOptionSpec.Name = "CmbOptionSpec"
            Me.CmbOptionSpec.Size = New System.Drawing.Size(305, 20)
            Me.CmbOptionSpec.TabIndex = 3
            Me.ToolTip1.SetToolTip(Me.CmbOptionSpec, "あいまい検索（前方一致）が可能です。")
            '
            'LblOptionSpec
            '
            Me.LblOptionSpec.AutoSize = True
            Me.LblOptionSpec.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblOptionSpec.Location = New System.Drawing.Point(14, 44)
            Me.LblOptionSpec.Name = "LblOptionSpec"
            Me.LblOptionSpec.Size = New System.Drawing.Size(59, 12)
            Me.LblOptionSpec.TabIndex = 86
            Me.LblOptionSpec.Text = "装備仕様："
            '
            'LblSearchCondition
            '
            Me.LblSearchCondition.AutoSize = True
            Me.LblSearchCondition.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblSearchCondition.Location = New System.Drawing.Point(5, 5)
            Me.LblSearchCondition.Name = "LblSearchCondition"
            Me.LblSearchCondition.Size = New System.Drawing.Size(65, 12)
            Me.LblSearchCondition.TabIndex = 81
            Me.LblSearchCondition.Text = "《検索条件》"
            '
            'Frm9DispEventEditInquiry
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(409, 516)
            Me.Controls.Add(Me.Panel4)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.spdOptionSpec)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "Frm9DispEventEditInquiry"
            Me.Padding = New System.Windows.Forms.Padding(3)
            Me.Text = "新試作手配システム Ver 1.00"
            Me.TopMost = True
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            CType(Me.spdOptionSpec, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdOptionSpec_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel3.ResumeLayout(False)
            Me.Panel4.ResumeLayout(False)
            Me.Panel4.PerformLayout()
            Me.Panel5.ResumeLayout(False)
            Me.Panel5.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents LblDateNow As System.Windows.Forms.Label
        Friend WithEvents LblTimeNow As System.Windows.Forms.Label
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents BtnBack As System.Windows.Forms.Button
        Friend WithEvents spdOptionSpec As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdOptionSpec_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents LblMessage As System.Windows.Forms.Label
        Friend WithEvents Panel4 As System.Windows.Forms.Panel
        Friend WithEvents LblSearchCondition As System.Windows.Forms.Label
        Friend WithEvents LOGO As System.Windows.Forms.Label
        Friend WithEvents CmbOptionSpec As System.Windows.Forms.ComboBox
        Friend WithEvents LblOptionSpec As System.Windows.Forms.Label
        Friend WithEvents LblMasterSelection As System.Windows.Forms.Label
        Friend WithEvents Panel5 As System.Windows.Forms.Panel
        Friend WithEvents RdoShisaku As System.Windows.Forms.RadioButton
        Friend WithEvents RdoEBom As System.Windows.Forms.RadioButton
        Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    End Class
End Namespace
