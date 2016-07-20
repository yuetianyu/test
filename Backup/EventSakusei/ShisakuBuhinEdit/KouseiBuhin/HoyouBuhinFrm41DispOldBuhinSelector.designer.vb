Namespace KouseiBuhin
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class HoyouBuhinFrm41DispOldBuhinSelector
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
            Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType6 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType7 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType8 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType9 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType10 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType11 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HoyouBuhinFrm41DispOldBuhinSelector))
            Me.LblCurrPGName = New System.Windows.Forms.Label
            Me.LblCurrBukaName = New System.Windows.Forms.Label
            Me.LblDateNow = New System.Windows.Forms.Label
            Me.LblCurrUserId = New System.Windows.Forms.Label
            Me.LblTimeNow = New System.Windows.Forms.Label
            Me.LblCurrPGId = New System.Windows.Forms.Label
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.miInsertMemo = New System.Windows.Forms.ToolStripMenuItem
            Me.msKinouHyouji = New System.Windows.Forms.ToolStripMenuItem
            Me.msEventCopy = New System.Windows.Forms.ToolStripMenuItem
            Me.lblBuhinBango = New System.Windows.Forms.Label
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.LOGO = New System.Windows.Forms.Label
            Me.btnEND = New System.Windows.Forms.Button
            Me.btnBack = New System.Windows.Forms.Button
            Me.spdParts = New FarPoint.Win.Spread.FpSpread
            Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.btnCall = New System.Windows.Forms.Button
            Me.lblBuhinNo = New System.Windows.Forms.Label
            Me.txtEventNo = New System.Windows.Forms.TextBox
            Me.Label1 = New System.Windows.Forms.Label
            Me.txtBuka = New System.Windows.Forms.TextBox
            Me.Label2 = New System.Windows.Forms.Label
            Me.txtTantoCd = New System.Windows.Forms.TextBox
            Me.Label3 = New System.Windows.Forms.Label
            Me.txtTantoName = New System.Windows.Forms.TextBox
            Me.Label4 = New System.Windows.Forms.Label
            Me.txtTantoKaiteiNo = New System.Windows.Forms.TextBox
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'LblCurrPGName
            '
            Me.LblCurrPGName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrPGName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrPGName.ForeColor = System.Drawing.Color.Blue
            Me.LblCurrPGName.Location = New System.Drawing.Point(174, 4)
            Me.LblCurrPGName.Name = "LblCurrPGName"
            Me.LblCurrPGName.Size = New System.Drawing.Size(605, 26)
            Me.LblCurrPGName.TabIndex = 54
            Me.LblCurrPGName.Text = "過去部品表　検索"
            Me.LblCurrPGName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'LblCurrBukaName
            '
            Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrBukaName.ForeColor = System.Drawing.Color.Blue
            Me.LblCurrBukaName.Location = New System.Drawing.Point(797, 17)
            Me.LblCurrBukaName.Name = "LblCurrBukaName"
            Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
            Me.LblCurrBukaName.TabIndex = 59
            Me.LblCurrBukaName.Text = "(部課：SKE1         )"
            '
            'LblDateNow
            '
            Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblDateNow.AutoSize = True
            Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblDateNow.ForeColor = System.Drawing.Color.Blue
            Me.LblDateNow.Location = New System.Drawing.Point(933, 4)
            Me.LblDateNow.Name = "LblDateNow"
            Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
            Me.LblDateNow.TabIndex = 56
            Me.LblDateNow.Text = "YYYY/MM/DD"
            '
            'LblCurrUserId
            '
            Me.LblCurrUserId.AccessibleRole = System.Windows.Forms.AccessibleRole.None
            Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrUserId.ForeColor = System.Drawing.Color.Blue
            Me.LblCurrUserId.Location = New System.Drawing.Point(797, 4)
            Me.LblCurrUserId.Name = "LblCurrUserId"
            Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
            Me.LblCurrUserId.TabIndex = 58
            Me.LblCurrUserId.Text = "(ID    ：ABCDEFGH)"
            '
            'LblTimeNow
            '
            Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblTimeNow.AutoSize = True
            Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblTimeNow.ForeColor = System.Drawing.Color.Blue
            Me.LblTimeNow.Location = New System.Drawing.Point(957, 17)
            Me.LblTimeNow.Name = "LblTimeNow"
            Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
            Me.LblTimeNow.TabIndex = 55
            Me.LblTimeNow.Text = "HH:MM:DD"
            '
            'LblCurrPGId
            '
            Me.LblCurrPGId.AutoSize = True
            Me.LblCurrPGId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrPGId.ForeColor = System.Drawing.Color.Blue
            Me.LblCurrPGId.Location = New System.Drawing.Point(4, 4)
            Me.LblCurrPGId.Name = "LblCurrPGId"
            Me.LblCurrPGId.Size = New System.Drawing.Size(113, 13)
            Me.LblCurrPGId.TabIndex = 54
            Me.LblCurrPGId.Text = "PG-ID：XXXXXXXX"
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.LightPink
            Me.Panel1.Controls.Add(Me.LblCurrPGName)
            Me.Panel1.Controls.Add(Me.LblCurrBukaName)
            Me.Panel1.Controls.Add(Me.LblDateNow)
            Me.Panel1.Controls.Add(Me.LblCurrUserId)
            Me.Panel1.Controls.Add(Me.LblTimeNow)
            Me.Panel1.Controls.Add(Me.LblCurrPGId)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(1025, 34)
            Me.Panel1.TabIndex = 84
            '
            'Timer1
            '
            Me.Timer1.Enabled = True
            Me.Timer1.Interval = 1000
            '
            'miInsertMemo
            '
            Me.miInsertMemo.Name = "miInsertMemo"
            Me.miInsertMemo.Size = New System.Drawing.Size(182, 22)
            Me.miInsertMemo.Text = "メモ欄挿入(&I)"
            Me.miInsertMemo.ToolTipText = "メモ欄を挿入します。"
            '
            'msKinouHyouji
            '
            Me.msKinouHyouji.Name = "msKinouHyouji"
            Me.msKinouHyouji.Size = New System.Drawing.Size(182, 22)
            Me.msKinouHyouji.Text = "機能仕様表示選択(&A)"
            '
            'msEventCopy
            '
            Me.msEventCopy.Name = "msEventCopy"
            Me.msEventCopy.Size = New System.Drawing.Size(182, 22)
            Me.msEventCopy.Text = "イベント品番コピー(&E)"
            '
            'lblBuhinBango
            '
            Me.lblBuhinBango.AutoSize = True
            Me.lblBuhinBango.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblBuhinBango.Location = New System.Drawing.Point(2, 117)
            Me.lblBuhinBango.Name = "lblBuhinBango"
            Me.lblBuhinBango.Size = New System.Drawing.Size(121, 12)
            Me.lblBuhinBango.TabIndex = 97
            Me.lblBuhinBango.Text = "<< 過去イベント一覧 >>"
            '
            'Panel2
            '
            Me.Panel2.AutoSize = True
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.LOGO)
            Me.Panel2.Controls.Add(Me.btnEND)
            Me.Panel2.Controls.Add(Me.btnBack)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 34)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(1025, 33)
            Me.Panel2.TabIndex = 103
            '
            'LOGO
            '
            Me.LOGO.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LOGO.BackColor = System.Drawing.Color.White
            Me.LOGO.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LOGO.ForeColor = System.Drawing.Color.Yellow
            Me.LOGO.Image = Global.EventSakusei.My.Resources.Resources.ロゴ
            Me.LOGO.Location = New System.Drawing.Point(367, 3)
            Me.LOGO.Name = "LOGO"
            Me.LOGO.Size = New System.Drawing.Size(212, 26)
            Me.LOGO.TabIndex = 82
            Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnEND
            '
            Me.btnEND.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnEND.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnEND.Location = New System.Drawing.Point(3, 3)
            Me.btnEND.Name = "btnEND"
            Me.btnEND.Size = New System.Drawing.Size(149, 24)
            Me.btnEND.TabIndex = 55
            Me.btnEND.Text = "アプリケーション終了"
            Me.btnEND.UseVisualStyleBackColor = True
            Me.btnEND.Visible = False
            '
            'btnBack
            '
            Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBack.Location = New System.Drawing.Point(957, 3)
            Me.btnBack.Name = "btnBack"
            Me.btnBack.Size = New System.Drawing.Size(64, 24)
            Me.btnBack.TabIndex = 9
            Me.btnBack.Text = "戻る"
            Me.btnBack.UseVisualStyleBackColor = True
            '
            'spdParts
            '
            Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, "
            Me.spdParts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.spdParts.BackColor = System.Drawing.SystemColors.Control
            Me.spdParts.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentCell
            Me.spdParts.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
            Me.spdParts.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.Location = New System.Drawing.Point(4, 132)
            Me.spdParts.Name = "spdParts"
            Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
            Me.spdParts.Size = New System.Drawing.Size(1018, 509)
            Me.spdParts.TabIndex = 104
            Me.spdParts.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdParts_Sheet1
            '
            Me.spdParts_Sheet1.Reset()
            Me.spdParts_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdParts_Sheet1.ColumnCount = 12
            Me.spdParts_Sheet1.RowCount = 0
            Me.spdParts_Sheet1.ActiveRowIndex = -1
            Me.spdParts_Sheet1.AllowNoteEdit = True
            Me.spdParts_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdParts_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "イベント"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "担当不要"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "申請部署"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "申請者CD"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "申請者"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "改訂"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "メモ"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "最終 更新日"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "担当承認"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "課長承認"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "kousei"
            Me.spdParts_Sheet1.ColumnHeader.Rows.Get(0).Height = 40.0!
            Me.spdParts_Sheet1.Columns.Get(0).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(0).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(0).CellType = TextCellType1
            Me.spdParts_Sheet1.Columns.Get(0).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(0).Label = "イベント"
            Me.spdParts_Sheet1.Columns.Get(0).Locked = False
            Me.spdParts_Sheet1.Columns.Get(0).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(0).Width = 70.0!
            Me.spdParts_Sheet1.Columns.Get(1).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(1).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(1).CellType = TextCellType2
            Me.spdParts_Sheet1.Columns.Get(1).Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
            Me.spdParts_Sheet1.Columns.Get(1).Label = "担当不要"
            Me.spdParts_Sheet1.Columns.Get(1).Locked = True
            Me.spdParts_Sheet1.Columns.Get(1).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(1).Width = 70.0!
            Me.spdParts_Sheet1.Columns.Get(2).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(2).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(2).CellType = TextCellType3
            Me.spdParts_Sheet1.Columns.Get(2).Label = "申請部署"
            Me.spdParts_Sheet1.Columns.Get(2).Locked = True
            Me.spdParts_Sheet1.Columns.Get(2).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(2).Width = 70.0!
            Me.spdParts_Sheet1.Columns.Get(3).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(3).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(3).CellType = TextCellType4
            Me.spdParts_Sheet1.Columns.Get(3).Label = "申請者CD"
            Me.spdParts_Sheet1.Columns.Get(3).Locked = True
            Me.spdParts_Sheet1.Columns.Get(3).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(3).Width = 70.0!
            Me.spdParts_Sheet1.Columns.Get(4).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(4).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(4).CellType = TextCellType5
            Me.spdParts_Sheet1.Columns.Get(4).Label = "申請者"
            Me.spdParts_Sheet1.Columns.Get(4).Locked = True
            Me.spdParts_Sheet1.Columns.Get(4).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(4).Width = 80.0!
            Me.spdParts_Sheet1.Columns.Get(5).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(5).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(5).CellType = TextCellType6
            Me.spdParts_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(5).Label = "改訂"
            Me.spdParts_Sheet1.Columns.Get(5).Locked = False
            Me.spdParts_Sheet1.Columns.Get(5).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(5).Width = 50.0!
            Me.spdParts_Sheet1.Columns.Get(6).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(6).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(6).CellType = TextCellType7
            Me.spdParts_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(6).Label = "メモ"
            Me.spdParts_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(6).Width = 300.0!
            Me.spdParts_Sheet1.Columns.Get(7).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(7).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(7).CellType = TextCellType8
            Me.spdParts_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Me.spdParts_Sheet1.Columns.Get(7).Label = "最終 更新日"
            Me.spdParts_Sheet1.Columns.Get(7).Locked = True
            Me.spdParts_Sheet1.Columns.Get(7).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(7).Width = 80.0!
            Me.spdParts_Sheet1.Columns.Get(8).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(8).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(8).CellType = TextCellType9
            Me.spdParts_Sheet1.Columns.Get(8).Label = "担当承認"
            Me.spdParts_Sheet1.Columns.Get(8).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(8).Width = 70.0!
            Me.spdParts_Sheet1.Columns.Get(9).AllowAutoFilter = True
            Me.spdParts_Sheet1.Columns.Get(9).AllowAutoSort = True
            Me.spdParts_Sheet1.Columns.Get(9).CellType = TextCellType10
            Me.spdParts_Sheet1.Columns.Get(9).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(9).Label = "課長承認"
            Me.spdParts_Sheet1.Columns.Get(9).Locked = True
            Me.spdParts_Sheet1.Columns.Get(9).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(9).Width = 100.0!
            Me.spdParts_Sheet1.Columns.Get(10).CellType = TextCellType11
            Me.spdParts_Sheet1.Columns.Get(10).Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts_Sheet1.Columns.Get(10).Label = "kousei"
            Me.spdParts_Sheet1.Columns.Get(10).Locked = True
            Me.spdParts_Sheet1.Columns.Get(10).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(10).Width = 0.0!
            Me.spdParts_Sheet1.Columns.Get(11).Locked = False
            Me.spdParts_Sheet1.Columns.Get(11).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(11).Width = 0.0!
            Me.spdParts_Sheet1.DataAutoSizeColumns = False
            Me.spdParts_Sheet1.FrozenColumnCount = 1
            Me.spdParts_Sheet1.RowHeader.AutoTextIndex = 0
            Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdParts_Sheet1.Rows.Default.Height = 16.0!
            Me.spdParts_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(160, Byte), Integer))
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            Me.spdParts.SetViewportLeftColumn(0, 0, 1)
            Me.spdParts.SetActiveViewport(0, -1, -1)
            '
            'btnCall
            '
            Me.btnCall.BackColor = System.Drawing.Color.LightCyan
            Me.btnCall.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnCall.Location = New System.Drawing.Point(474, 90)
            Me.btnCall.Name = "btnCall"
            Me.btnCall.Size = New System.Drawing.Size(79, 24)
            Me.btnCall.TabIndex = 2
            Me.btnCall.Text = "呼出し"
            Me.btnCall.UseVisualStyleBackColor = False
            '
            'lblBuhinNo
            '
            Me.lblBuhinNo.AutoSize = True
            Me.lblBuhinNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblBuhinNo.Location = New System.Drawing.Point(4, 71)
            Me.lblBuhinNo.Name = "lblBuhinNo"
            Me.lblBuhinNo.Size = New System.Drawing.Size(93, 15)
            Me.lblBuhinNo.TabIndex = 106
            Me.lblBuhinNo.Text = "イベントコード："
            '
            'txtEventNo
            '
            Me.txtEventNo.BackColor = System.Drawing.Color.White
            Me.txtEventNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtEventNo.ForeColor = System.Drawing.Color.Navy
            Me.txtEventNo.Location = New System.Drawing.Point(4, 92)
            Me.txtEventNo.Name = "txtEventNo"
            Me.txtEventNo.ReadOnly = True
            Me.txtEventNo.Size = New System.Drawing.Size(100, 22)
            Me.txtEventNo.TabIndex = 1
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.Location = New System.Drawing.Point(107, 73)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(75, 15)
            Me.Label1.TabIndex = 108
            Me.Label1.Text = "申請部署："
            '
            'txtBuka
            '
            Me.txtBuka.BackColor = System.Drawing.Color.White
            Me.txtBuka.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtBuka.ForeColor = System.Drawing.Color.Navy
            Me.txtBuka.Location = New System.Drawing.Point(110, 92)
            Me.txtBuka.Name = "txtBuka"
            Me.txtBuka.ReadOnly = True
            Me.txtBuka.Size = New System.Drawing.Size(70, 22)
            Me.txtBuka.TabIndex = 2
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label2.Location = New System.Drawing.Point(183, 73)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(80, 15)
            Me.Label2.TabIndex = 110
            Me.Label2.Text = "申請者CD："
            '
            'txtTantoCd
            '
            Me.txtTantoCd.BackColor = System.Drawing.Color.White
            Me.txtTantoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtTantoCd.ForeColor = System.Drawing.Color.Navy
            Me.txtTantoCd.Location = New System.Drawing.Point(186, 92)
            Me.txtTantoCd.Name = "txtTantoCd"
            Me.txtTantoCd.ReadOnly = True
            Me.txtTantoCd.Size = New System.Drawing.Size(70, 22)
            Me.txtTantoCd.TabIndex = 3
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label3.Location = New System.Drawing.Point(259, 72)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(75, 15)
            Me.Label3.TabIndex = 112
            Me.Label3.Text = "申請者名："
            '
            'txtTantoName
            '
            Me.txtTantoName.BackColor = System.Drawing.Color.White
            Me.txtTantoName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtTantoName.ForeColor = System.Drawing.Color.Navy
            Me.txtTantoName.Location = New System.Drawing.Point(262, 91)
            Me.txtTantoName.Name = "txtTantoName"
            Me.txtTantoName.ReadOnly = True
            Me.txtTantoName.Size = New System.Drawing.Size(130, 22)
            Me.txtTantoName.TabIndex = 4
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label4.Location = New System.Drawing.Point(395, 72)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(60, 15)
            Me.Label4.TabIndex = 114
            Me.Label4.Text = "改訂№："
            '
            'txtTantoKaiteiNo
            '
            Me.txtTantoKaiteiNo.BackColor = System.Drawing.Color.White
            Me.txtTantoKaiteiNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtTantoKaiteiNo.ForeColor = System.Drawing.Color.Navy
            Me.txtTantoKaiteiNo.Location = New System.Drawing.Point(398, 91)
            Me.txtTantoKaiteiNo.Name = "txtTantoKaiteiNo"
            Me.txtTantoKaiteiNo.ReadOnly = True
            Me.txtTantoKaiteiNo.Size = New System.Drawing.Size(70, 22)
            Me.txtTantoKaiteiNo.TabIndex = 5
            '
            'HoyouBuhinFrm41DispOldBuhinSelector
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1025, 643)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.txtTantoKaiteiNo)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.txtTantoName)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.txtTantoCd)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.txtBuka)
            Me.Controls.Add(Me.btnCall)
            Me.Controls.Add(Me.lblBuhinNo)
            Me.Controls.Add(Me.txtEventNo)
            Me.Controls.Add(Me.spdParts)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.lblBuhinBango)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "HoyouBuhinFrm41DispOldBuhinSelector"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents LblCurrPGName As System.Windows.Forms.Label
        Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
        Friend WithEvents LblDateNow As System.Windows.Forms.Label
        Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
        Friend WithEvents LblTimeNow As System.Windows.Forms.Label
        Friend WithEvents LblCurrPGId As System.Windows.Forms.Label
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents miInsertMemo As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents msKinouHyouji As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents msEventCopy As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents lblBuhinBango As System.Windows.Forms.Label
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents LOGO As System.Windows.Forms.Label
        Friend WithEvents btnEND As System.Windows.Forms.Button
        Friend WithEvents btnBack As System.Windows.Forms.Button
        Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
        Friend WithEvents btnCall As System.Windows.Forms.Button
        Friend WithEvents lblBuhinNo As System.Windows.Forms.Label
        Friend WithEvents txtEventNo As System.Windows.Forms.TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents txtBuka As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents txtTantoCd As System.Windows.Forms.TextBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents txtTantoName As System.Windows.Forms.TextBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents txtTantoKaiteiNo As System.Windows.Forms.TextBox
    End Class
End Namespace
