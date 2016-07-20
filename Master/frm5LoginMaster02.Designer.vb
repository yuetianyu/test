<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm5LoginMaster02
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
        Dim ComboBoxCellType1 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType
        Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Dim ComboBoxCellType2 As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType
        Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm5LoginMaster02))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblCurrBukaName = New System.Windows.Forms.Label
        Me.LblDateNow = New System.Windows.Forms.Label
        Me.LblCurrUserId = New System.Windows.Forms.Label
        Me.LblCurrPGId = New System.Windows.Forms.Label
        Me.LblTimeNow = New System.Windows.Forms.Label
        Me.LblCurrPGName = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lblMsg = New System.Windows.Forms.Label
        Me.spdParts = New FarPoint.Win.Spread.FpSpread
        Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnEND = New System.Windows.Forms.Button
        Me.btnBACK = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.lblUserkbn = New System.Windows.Forms.Label
        Me.lblSITE = New System.Windows.Forms.Label
        Me.lblBuka = New System.Windows.Forms.Label
        Me.lblUserName = New System.Windows.Forms.Label
        Me.btnCall = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblPsdKakunin = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblPsd = New System.Windows.Forms.TextBox
        Me.lblUserCode = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.BackColor = System.Drawing.Color.Blue
        Me.Panel1.Controls.Add(Me.LblCurrBukaName)
        Me.Panel1.Controls.Add(Me.LblDateNow)
        Me.Panel1.Controls.Add(Me.LblCurrUserId)
        Me.Panel1.Controls.Add(Me.LblCurrPGId)
        Me.Panel1.Controls.Add(Me.LblTimeNow)
        Me.Panel1.Controls.Add(Me.LblCurrPGName)
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
        Me.LblCurrBukaName.Location = New System.Drawing.Point(349, 14)
        Me.LblCurrBukaName.Name = "LblCurrBukaName"
        Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrBukaName.TabIndex = 73
        Me.LblCurrBukaName.Text = "(部課：SKE1         )"
        '
        'LblDateNow
        '
        Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDateNow.AutoSize = True
        Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDateNow.ForeColor = System.Drawing.Color.White
        Me.LblDateNow.Location = New System.Drawing.Point(485, 1)
        Me.LblDateNow.Name = "LblDateNow"
        Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
        Me.LblDateNow.TabIndex = 60
        Me.LblDateNow.Text = "YYYY/MM/DD"
        '
        'LblCurrUserId
        '
        Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
        Me.LblCurrUserId.Location = New System.Drawing.Point(349, 1)
        Me.LblCurrUserId.Name = "LblCurrUserId"
        Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrUserId.TabIndex = 72
        Me.LblCurrUserId.Text = "(ID    ：ABCDEFGH)"
        '
        'LblCurrPGId
        '
        Me.LblCurrPGId.AutoSize = True
        Me.LblCurrPGId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrPGId.ForeColor = System.Drawing.Color.White
        Me.LblCurrPGId.Location = New System.Drawing.Point(3, 1)
        Me.LblCurrPGId.Name = "LblCurrPGId"
        Me.LblCurrPGId.Size = New System.Drawing.Size(119, 13)
        Me.LblCurrPGId.TabIndex = 59
        Me.LblCurrPGId.Text = "PG-ID：MASTER005"
        '
        'LblTimeNow
        '
        Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTimeNow.AutoSize = True
        Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTimeNow.ForeColor = System.Drawing.Color.White
        Me.LblTimeNow.Location = New System.Drawing.Point(509, 14)
        Me.LblTimeNow.Name = "LblTimeNow"
        Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
        Me.LblTimeNow.TabIndex = 59
        Me.LblTimeNow.Text = "HH:MM:DD"
        '
        'LblCurrPGName
        '
        Me.LblCurrPGName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrPGName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrPGName.ForeColor = System.Drawing.Color.Yellow
        Me.LblCurrPGName.Location = New System.Drawing.Point(194, 1)
        Me.LblCurrPGName.Name = "LblCurrPGName"
        Me.LblCurrPGName.Size = New System.Drawing.Size(139, 31)
        Me.LblCurrPGName.TabIndex = 54
        Me.LblCurrPGName.Text = "ログインマスター"
        Me.LblCurrPGName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.AutoSize = True
        Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.lblMsg)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 274)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(574, 25)
        Me.Panel3.TabIndex = 71
        '
        'lblMsg
        '
        Me.lblMsg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMsg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMsg.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(1, 1)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(570, 22)
        Me.lblMsg.TabIndex = 55
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdParts
        '
        Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, メニュー設定"
        Me.spdParts.AllowDragDrop = True
        Me.spdParts.AllowDragFill = True
        Me.spdParts.AllowUserFormulas = True
        Me.spdParts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdParts.BackColor = System.Drawing.SystemColors.Control
        Me.spdParts.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdParts.Location = New System.Drawing.Point(165, 113)
        Me.spdParts.Name = "spdParts"
        Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
        Me.spdParts.Size = New System.Drawing.Size(305, 59)
        Me.spdParts.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.spdParts, "本ユーザーに変更の権限を付与します。")
        Me.spdParts.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdParts.VisualStyles = FarPoint.Win.VisualStyles.Off
        '
        'spdParts_Sheet1
        '
        Me.spdParts_Sheet1.Reset()
        Me.spdParts_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spdParts_Sheet1.ColumnCount = 2
        Me.spdParts_Sheet1.RowCount = 2
        Me.spdParts_Sheet1.AllowNoteEdit = True
        Me.spdParts_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
        Me.spdParts_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
        Me.spdParts_Sheet1.Cells.Get(0, 0).CellType = TextCellType1
        Me.spdParts_Sheet1.Cells.Get(0, 0).Locked = True
        Me.spdParts_Sheet1.Cells.Get(0, 0).Value = "メニュー設定"
        ComboBoxCellType1.ButtonAlign = FarPoint.Win.ButtonAlign.Right
        ComboBoxCellType1.Editable = True
        ComboBoxCellType1.Items = New String() {"", "あり", "なし"}
        Me.spdParts_Sheet1.Cells.Get(0, 1).CellType = ComboBoxCellType1
        Me.spdParts_Sheet1.Cells.Get(0, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(0, 1).Tag = "menuSettei"
        Me.spdParts_Sheet1.Cells.Get(0, 1).Value = ""
        Me.spdParts_Sheet1.Cells.Get(1, 0).CellType = TextCellType2
        Me.spdParts_Sheet1.Cells.Get(1, 0).Locked = True
        Me.spdParts_Sheet1.Cells.Get(1, 0).Value = "機能設定"
        ComboBoxCellType2.AutoCompleteCustomSource.AddRange(New String() {"あり", "なし"})
        ComboBoxCellType2.ButtonAlign = FarPoint.Win.ButtonAlign.Right
        ComboBoxCellType2.Editable = True
        ComboBoxCellType2.Items = New String() {"", "あり", "なし"}
        Me.spdParts_Sheet1.Cells.Get(1, 1).CellType = ComboBoxCellType2
        Me.spdParts_Sheet1.Cells.Get(1, 1).Locked = False
        Me.spdParts_Sheet1.Cells.Get(1, 1).Tag = "kinoSettei"
        Me.spdParts_Sheet1.Cells.Get(1, 1).Value = ""
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "内容"
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.SystemColors.Control
        Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "権限"
        Me.spdParts_Sheet1.ColumnHeader.Rows.Get(0).Height = 18.0!
        Me.spdParts_Sheet1.Columns.Get(0).CellType = TextCellType3
        Me.spdParts_Sheet1.Columns.Get(0).Label = "内容"
        Me.spdParts_Sheet1.Columns.Get(0).Locked = True
        Me.spdParts_Sheet1.Columns.Get(0).Tag = "BUHIN_NO"
        Me.spdParts_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
        Me.spdParts_Sheet1.Columns.Get(0).Width = 216.0!
        Me.spdParts_Sheet1.Columns.Get(1).Label = "権限"
        Me.spdParts_Sheet1.Columns.Get(1).Locked = False
        Me.spdParts_Sheet1.Columns.Get(1).Width = 85.0!
        Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.spdParts_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.spdParts_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
        Me.spdParts_Sheet1.RowHeader.Visible = False
        Me.spdParts_Sheet1.Rows.Default.Height = 16.0!
        Me.spdParts_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange
        Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.btnEND)
        Me.Panel2.Controls.Add(Me.btnBACK)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 32)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(574, 32)
        Me.Panel2.TabIndex = 73
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Yellow
        Me.Label1.Image = Global.Master.My.Resources.Resources.ロゴ
        Me.Label1.Location = New System.Drawing.Point(218, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 26)
        Me.Label1.TabIndex = 83
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnEND
        '
        Me.btnEND.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEND.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEND.Location = New System.Drawing.Point(5, 3)
        Me.btnEND.Name = "btnEND"
        Me.btnEND.Size = New System.Drawing.Size(149, 24)
        Me.btnEND.TabIndex = 57
        Me.btnEND.Text = "アプリケーション終了"
        Me.btnEND.UseVisualStyleBackColor = True
        Me.btnEND.Visible = False
        '
        'btnBACK
        '
        Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBACK.Location = New System.Drawing.Point(502, 3)
        Me.btnBACK.Name = "btnBACK"
        Me.btnBACK.Size = New System.Drawing.Size(64, 24)
        Me.btnBACK.TabIndex = 56
        Me.btnBACK.Text = "戻る"
        Me.btnBACK.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(77, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 15)
        Me.Label8.TabIndex = 75
        Me.Label8.Text = "ユーザー："
        '
        'Panel4
        '
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.BackColor = System.Drawing.SystemColors.Control
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.lblUserkbn)
        Me.Panel4.Controls.Add(Me.lblSITE)
        Me.Panel4.Controls.Add(Me.lblBuka)
        Me.Panel4.Controls.Add(Me.lblUserName)
        Me.Panel4.Controls.Add(Me.btnCall)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.spdParts)
        Me.Panel4.Controls.Add(Me.lblPsdKakunin)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.lblPsd)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.lblUserCode)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 64)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(574, 210)
        Me.Panel4.TabIndex = 74
        '
        'lblUserkbn
        '
        Me.lblUserkbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUserkbn.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUserkbn.Location = New System.Drawing.Point(545, 175)
        Me.lblUserkbn.Name = "lblUserkbn"
        Me.lblUserkbn.Size = New System.Drawing.Size(21, 22)
        Me.lblUserkbn.TabIndex = 89
        Me.lblUserkbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUserkbn.Visible = False
        '
        'lblSITE
        '
        Me.lblSITE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSITE.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSITE.Location = New System.Drawing.Point(330, 27)
        Me.lblSITE.Name = "lblSITE"
        Me.lblSITE.Size = New System.Drawing.Size(75, 22)
        Me.lblSITE.TabIndex = 88
        Me.lblSITE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBuka
        '
        Me.lblBuka.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBuka.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBuka.Location = New System.Drawing.Point(255, 27)
        Me.lblBuka.Name = "lblBuka"
        Me.lblBuka.Size = New System.Drawing.Size(75, 22)
        Me.lblBuka.TabIndex = 87
        Me.lblBuka.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUserName
        '
        Me.lblUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUserName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUserName.Location = New System.Drawing.Point(255, 5)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(150, 22)
        Me.lblUserName.TabIndex = 86
        Me.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCall
        '
        Me.btnCall.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCall.BackColor = System.Drawing.Color.LightCyan
        Me.btnCall.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCall.Location = New System.Drawing.Point(487, 6)
        Me.btnCall.Name = "btnCall"
        Me.btnCall.Size = New System.Drawing.Size(79, 24)
        Me.btnCall.TabIndex = 4
        Me.btnCall.Text = "更新"
        Me.btnCall.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(77, 110)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 15)
        Me.Label11.TabIndex = 83
        Me.Label11.Text = "権限の設定："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(76, 88)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(86, 15)
        Me.Label9.TabIndex = 82
        Me.Label9.Text = "ＰＷ再入力："
        '
        'lblPsdKakunin
        '
        Me.lblPsdKakunin.BackColor = System.Drawing.Color.White
        Me.lblPsdKakunin.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPsdKakunin.ForeColor = System.Drawing.Color.Navy
        Me.lblPsdKakunin.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.lblPsdKakunin.Location = New System.Drawing.Point(165, 85)
        Me.lblPsdKakunin.MaxLength = 10
        Me.lblPsdKakunin.Name = "lblPsdKakunin"
        Me.lblPsdKakunin.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.lblPsdKakunin.Size = New System.Drawing.Size(90, 22)
        Me.lblPsdKakunin.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(76, 60)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 15)
        Me.Label6.TabIndex = 80
        Me.Label6.Text = "ＰＷ："
        '
        'lblPsd
        '
        Me.lblPsd.BackColor = System.Drawing.Color.White
        Me.lblPsd.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPsd.ForeColor = System.Drawing.Color.Navy
        Me.lblPsd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.lblPsd.Location = New System.Drawing.Point(165, 57)
        Me.lblPsd.MaxLength = 10
        Me.lblPsd.Name = "lblPsd"
        Me.lblPsd.Size = New System.Drawing.Size(90, 22)
        Me.lblPsd.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.lblPsd, "本ユーザーのパスワードを設定してください。")
        '
        'lblUserCode
        '
        Me.lblUserCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUserCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUserCode.Location = New System.Drawing.Point(166, 5)
        Me.lblUserCode.Name = "lblUserCode"
        Me.lblUserCode.Size = New System.Drawing.Size(90, 22)
        Me.lblUserCode.TabIndex = 85
        Me.lblUserCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 160
        '
        'frm5LoginMaster02
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(574, 299)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frm5LoginMaster02"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LblCurrPGName As System.Windows.Forms.Label
    Friend WithEvents LblCurrPGId As System.Windows.Forms.Label
    Friend WithEvents LblDateNow As System.Windows.Forms.Label
    Friend WithEvents LblTimeNow As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
    Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
    Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblPsdKakunin As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblPsd As System.Windows.Forms.TextBox
    Friend WithEvents btnCall As System.Windows.Forms.Button
    Friend WithEvents btnBACK As System.Windows.Forms.Button
    Friend WithEvents btnEND As System.Windows.Forms.Button
    Friend WithEvents lblSITE As System.Windows.Forms.Label
    Friend WithEvents lblBuka As System.Windows.Forms.Label
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents lblUserCode As System.Windows.Forms.Label
    Friend WithEvents lblUserkbn As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
