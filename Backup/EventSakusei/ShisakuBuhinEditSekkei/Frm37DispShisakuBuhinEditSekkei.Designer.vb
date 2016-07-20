Namespace ShisakuBuhinEditSekkei
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm37DispShisakuBuhinEditSekkei
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm37DispShisakuBuhinEditSekkei))
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.LblCurrBukaName = New System.Windows.Forms.Label
            Me.LblCurrUserId = New System.Windows.Forms.Label
            Me.LblDateNow = New System.Windows.Forms.Label
            Me.Label2 = New System.Windows.Forms.Label
            Me.LblTimeNow = New System.Windows.Forms.Label
            Me.Label4 = New System.Windows.Forms.Label
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.Label10 = New System.Windows.Forms.Label
            Me.spdParts = New FarPoint.Win.Spread.FpSpread
            Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.lblSekkeika = New System.Windows.Forms.Label
            Me.Label21 = New System.Windows.Forms.Label
            Me.lblIbentoName = New System.Windows.Forms.Label
            Me.lblKaihatuFugou = New System.Windows.Forms.Label
            Me.Panel5 = New System.Windows.Forms.Panel
            Me.lblShindo_syounin2 = New System.Windows.Forms.Label
            Me.lblNokori_syounin2 = New System.Windows.Forms.Label
            Me.lblSyochi_syounin2 = New System.Windows.Forms.Label
            Me.Label28 = New System.Windows.Forms.Label
            Me.lblShindo_syounin1 = New System.Windows.Forms.Label
            Me.lblNokori_syounin1 = New System.Windows.Forms.Label
            Me.lblSyochi_syounin1 = New System.Windows.Forms.Label
            Me.Label24 = New System.Windows.Forms.Label
            Me.lblShindo_Kan = New System.Windows.Forms.Label
            Me.lblNokori_kan = New System.Windows.Forms.Label
            Me.lblSyochi_kan = New System.Windows.Forms.Label
            Me.Label19 = New System.Windows.Forms.Label
            Me.Label13 = New System.Windows.Forms.Label
            Me.Label9 = New System.Windows.Forms.Label
            Me.Label12 = New System.Windows.Forms.Label
            Me.Label8 = New System.Windows.Forms.Label
            Me.Label6 = New System.Windows.Forms.Label
            Me.lblBurokusu = New System.Windows.Forms.Label
            Me.Label1 = New System.Windows.Forms.Label
            Me.lblkanryou = New System.Windows.Forms.Label
            Me.Label14 = New System.Windows.Forms.Label
            Me.lblSyochikigen2 = New System.Windows.Forms.Label
            Me.lblSyochikigen = New System.Windows.Forms.Label
            Me.Label11 = New System.Windows.Forms.Label
            Me.btnCall = New System.Windows.Forms.Button
            Me.lblBuhinNo = New System.Windows.Forms.Label
            Me.txtSekeika = New System.Windows.Forms.TextBox
            Me.Panel4 = New System.Windows.Forms.Panel
            Me.LOGO = New System.Windows.Forms.Label
            Me.btnEND = New System.Windows.Forms.Button
            Me.btnBACK = New System.Windows.Forms.Button
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.Panel1.SuspendLayout()
            Me.Panel3.SuspendLayout()
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel2.SuspendLayout()
            Me.Panel5.SuspendLayout()
            Me.Panel4.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.AutoSize = True
            Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel1.BackColor = System.Drawing.Color.Blue
            Me.Panel1.Controls.Add(Me.LblCurrBukaName)
            Me.Panel1.Controls.Add(Me.LblCurrUserId)
            Me.Panel1.Controls.Add(Me.LblDateNow)
            Me.Panel1.Controls.Add(Me.Label2)
            Me.Panel1.Controls.Add(Me.LblTimeNow)
            Me.Panel1.Controls.Add(Me.Label4)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(643, 32)
            Me.Panel1.TabIndex = 54
            '
            'LblCurrBukaName
            '
            Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
            Me.LblCurrBukaName.Location = New System.Drawing.Point(417, 13)
            Me.LblCurrBukaName.Name = "LblCurrBukaName"
            Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
            Me.LblCurrBukaName.TabIndex = 75
            '
            'LblCurrUserId
            '
            Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
            Me.LblCurrUserId.Location = New System.Drawing.Point(417, 0)
            Me.LblCurrUserId.Name = "LblCurrUserId"
            Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
            Me.LblCurrUserId.TabIndex = 74
            '
            'LblDateNow
            '
            Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblDateNow.AutoSize = True
            Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblDateNow.ForeColor = System.Drawing.Color.White
            Me.LblDateNow.Location = New System.Drawing.Point(554, 1)
            Me.LblDateNow.Name = "LblDateNow"
            Me.LblDateNow.Size = New System.Drawing.Size(89, 13)
            Me.LblDateNow.TabIndex = 60
            Me.LblDateNow.Text = "YYYY/MM/DD"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label2.ForeColor = System.Drawing.Color.White
            Me.Label2.Location = New System.Drawing.Point(3, 1)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(131, 13)
            Me.Label2.TabIndex = 59
            Me.Label2.Text = "PG-ID：EVENT_EDIT37"
            '
            'LblTimeNow
            '
            Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblTimeNow.AutoSize = True
            Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblTimeNow.ForeColor = System.Drawing.Color.White
            Me.LblTimeNow.Location = New System.Drawing.Point(578, 14)
            Me.LblTimeNow.Name = "LblTimeNow"
            Me.LblTimeNow.Size = New System.Drawing.Size(65, 13)
            Me.LblTimeNow.TabIndex = 59
            Me.LblTimeNow.Text = "HH:MM:DD"
            '
            'Label4
            '
            Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label4.ForeColor = System.Drawing.Color.Yellow
            Me.Label4.Location = New System.Drawing.Point(115, 1)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(309, 31)
            Me.Label4.TabIndex = 54
            Me.Label4.Text = "試作部品表 編集・改訂編集（設計）"
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
            Me.Panel3.Location = New System.Drawing.Point(0, 512)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(643, 25)
            Me.Panel3.TabIndex = 71
            '
            'Label10
            '
            Me.Label10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label10.ForeColor = System.Drawing.Color.Red
            Me.Label10.Location = New System.Drawing.Point(1, 1)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(639, 22)
            Me.Label10.TabIndex = 55
            Me.Label10.Text = "ダブルクリック、または設計課選択し「呼出し」をクリックしてください。"
            Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'spdParts
            '
            Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, "
            Me.spdParts.AllowUserZoom = False
            Me.spdParts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.spdParts.AutoClipboard = False
            Me.spdParts.BackColor = System.Drawing.SystemColors.Control
            Me.spdParts.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.Location = New System.Drawing.Point(5, 178)
            Me.spdParts.Name = "spdParts"
            Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
            Me.spdParts.Size = New System.Drawing.Size(636, 328)
            Me.spdParts.TabIndex = 72
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
            Me.spdParts_Sheet1.ColumnHeader.RowCount = 2
            Me.spdParts_Sheet1.RowCount = 0
            Me.spdParts_Sheet1.ActiveRowIndex = -1
            Me.spdParts_Sheet1.AllowNoteEdit = True
            Me.spdParts_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdParts_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdParts_Sheet1.AutoGenerateColumns = False
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).RowSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "部課コード"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).RowSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "設計課"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Bottom
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).RowSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "ブロック数"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Bottom
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).ColumnSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "処置完了ブロック"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "担当承認"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "状況"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).ColumnSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "課長・主査承認"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 11).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 3).Value = "完了数"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 4).Value = "残り"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 5).Value = "状況"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 6).Value = "承認数"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 7).Value = "残り"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 8).Value = "状況"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 9).Value = "承認数"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 10).Value = "残り"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 11).Value = "状況"
            Me.spdParts_Sheet1.ColumnHeader.Rows.Get(0).Height = 18.0!
            Me.spdParts_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(0).Locked = True
            Me.spdParts_Sheet1.Columns.Get(0).Tag = "BUKA_CODE"
            Me.spdParts_Sheet1.Columns.Get(1).CellType = TextCellType1
            Me.spdParts_Sheet1.Columns.Get(1).Locked = True
            Me.spdParts_Sheet1.Columns.Get(1).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(1).Tag = "KA_RYAKU_NAME"
            Me.spdParts_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(1).Width = 45.0!
            Me.spdParts_Sheet1.Columns.Get(2).CellType = TextCellType2
            Me.spdParts_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Me.spdParts_Sheet1.Columns.Get(2).Locked = True
            Me.spdParts_Sheet1.Columns.Get(2).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(2).Tag = "TOTAL_BLOCK"
            Me.spdParts_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(2).Width = 70.0!
            Me.spdParts_Sheet1.Columns.Get(3).CellType = TextCellType3
            Me.spdParts_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Me.spdParts_Sheet1.Columns.Get(3).Label = "完了数"
            Me.spdParts_Sheet1.Columns.Get(3).Locked = True
            Me.spdParts_Sheet1.Columns.Get(3).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(3).Tag = "TOTAL_JYOUTAI"
            Me.spdParts_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(3).Width = 50.0!
            Me.spdParts_Sheet1.Columns.Get(4).CellType = TextCellType4
            Me.spdParts_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Me.spdParts_Sheet1.Columns.Get(4).Label = "残り"
            Me.spdParts_Sheet1.Columns.Get(4).Locked = True
            Me.spdParts_Sheet1.Columns.Get(4).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(4).Tag = "NOKORI_KAN"
            Me.spdParts_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(4).Width = 40.0!
            Me.spdParts_Sheet1.Columns.Get(5).CellType = TextCellType5
            Me.spdParts_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Me.spdParts_Sheet1.Columns.Get(5).Label = "状況"
            Me.spdParts_Sheet1.Columns.Get(5).Locked = True
            Me.spdParts_Sheet1.Columns.Get(5).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(5).Tag = "SHINDO_KAN"
            Me.spdParts_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(5).Width = 40.0!
            Me.spdParts_Sheet1.Columns.Get(6).CellType = TextCellType6
            Me.spdParts_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Me.spdParts_Sheet1.Columns.Get(6).Label = "承認数"
            Me.spdParts_Sheet1.Columns.Get(6).Locked = True
            Me.spdParts_Sheet1.Columns.Get(6).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(6).Tag = "TOTAL_SYOUNIN_JYOUTAI"
            Me.spdParts_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(6).Width = 50.0!
            Me.spdParts_Sheet1.Columns.Get(7).CellType = TextCellType7
            Me.spdParts_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Me.spdParts_Sheet1.Columns.Get(7).Label = "残り"
            Me.spdParts_Sheet1.Columns.Get(7).Locked = True
            Me.spdParts_Sheet1.Columns.Get(7).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(7).Tag = "NOKORI_SYOUNIN1"
            Me.spdParts_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(7).Width = 40.0!
            Me.spdParts_Sheet1.Columns.Get(8).CellType = TextCellType8
            Me.spdParts_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Me.spdParts_Sheet1.Columns.Get(8).Label = "状況"
            Me.spdParts_Sheet1.Columns.Get(8).Locked = True
            Me.spdParts_Sheet1.Columns.Get(8).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(8).Tag = "SHINDO_SYOUNIN1"
            Me.spdParts_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(8).Width = 40.0!
            Me.spdParts_Sheet1.Columns.Get(9).CellType = TextCellType9
            Me.spdParts_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Me.spdParts_Sheet1.Columns.Get(9).Label = "承認数"
            Me.spdParts_Sheet1.Columns.Get(9).Locked = True
            Me.spdParts_Sheet1.Columns.Get(9).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(9).Tag = "TOTAL_KACHOU_SYOUNIN_JYOUTAI"
            Me.spdParts_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(9).Width = 50.0!
            Me.spdParts_Sheet1.Columns.Get(10).CellType = TextCellType10
            Me.spdParts_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Me.spdParts_Sheet1.Columns.Get(10).Label = "残り"
            Me.spdParts_Sheet1.Columns.Get(10).Locked = True
            Me.spdParts_Sheet1.Columns.Get(10).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(10).Tag = "NOKORI_SYOUNIN2"
            Me.spdParts_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(10).Width = 40.0!
            Me.spdParts_Sheet1.Columns.Get(11).CellType = TextCellType11
            Me.spdParts_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Me.spdParts_Sheet1.Columns.Get(11).Label = "状況"
            Me.spdParts_Sheet1.Columns.Get(11).Locked = True
            Me.spdParts_Sheet1.Columns.Get(11).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(11).Tag = "SHINDO_SYOUNIN2"
            Me.spdParts_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(11).Width = 40.0!
            Me.spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdParts_Sheet1.RowHeader.DefaultStyle.Locked = False
            Me.spdParts_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
            Me.spdParts_Sheet1.Rows.Default.Height = 16.0!
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            Me.spdParts.SetActiveViewport(0, -1, 0)
            '
            'Panel2
            '
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.lblSekkeika)
            Me.Panel2.Controls.Add(Me.Label21)
            Me.Panel2.Controls.Add(Me.lblIbentoName)
            Me.Panel2.Controls.Add(Me.lblKaihatuFugou)
            Me.Panel2.Controls.Add(Me.Panel5)
            Me.Panel2.Controls.Add(Me.lblSyochikigen2)
            Me.Panel2.Controls.Add(Me.lblSyochikigen)
            Me.Panel2.Controls.Add(Me.Label11)
            Me.Panel2.Controls.Add(Me.btnCall)
            Me.Panel2.Controls.Add(Me.lblBuhinNo)
            Me.Panel2.Controls.Add(Me.txtSekeika)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 62)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(643, 110)
            Me.Panel2.TabIndex = 83
            '
            'lblSekkeika
            '
            Me.lblSekkeika.AutoSize = True
            Me.lblSekkeika.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblSekkeika.Location = New System.Drawing.Point(237, 56)
            Me.lblSekkeika.Name = "lblSekkeika"
            Me.lblSekkeika.Size = New System.Drawing.Size(0, 16)
            Me.lblSekkeika.TabIndex = 129
            Me.lblSekkeika.Visible = False
            '
            'Label21
            '
            Me.Label21.AutoSize = True
            Me.Label21.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label21.Location = New System.Drawing.Point(3, 8)
            Me.Label21.Name = "Label21"
            Me.Label21.Size = New System.Drawing.Size(64, 16)
            Me.Label21.TabIndex = 128
            Me.Label21.Text = "イベント："
            '
            'lblIbentoName
            '
            Me.lblIbentoName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblIbentoName.Location = New System.Drawing.Point(120, 6)
            Me.lblIbentoName.Name = "lblIbentoName"
            Me.lblIbentoName.Size = New System.Drawing.Size(203, 20)
            Me.lblIbentoName.TabIndex = 127
            Me.lblIbentoName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'lblKaihatuFugou
            '
            Me.lblKaihatuFugou.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblKaihatuFugou.Location = New System.Drawing.Point(67, 6)
            Me.lblKaihatuFugou.Name = "lblKaihatuFugou"
            Me.lblKaihatuFugou.Size = New System.Drawing.Size(51, 20)
            Me.lblKaihatuFugou.TabIndex = 125
            Me.lblKaihatuFugou.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Panel5
            '
            Me.Panel5.BackColor = System.Drawing.Color.LightYellow
            Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.Panel5.Controls.Add(Me.lblShindo_syounin2)
            Me.Panel5.Controls.Add(Me.lblNokori_syounin2)
            Me.Panel5.Controls.Add(Me.lblSyochi_syounin2)
            Me.Panel5.Controls.Add(Me.Label28)
            Me.Panel5.Controls.Add(Me.lblShindo_syounin1)
            Me.Panel5.Controls.Add(Me.lblNokori_syounin1)
            Me.Panel5.Controls.Add(Me.lblSyochi_syounin1)
            Me.Panel5.Controls.Add(Me.Label24)
            Me.Panel5.Controls.Add(Me.lblShindo_Kan)
            Me.Panel5.Controls.Add(Me.lblNokori_kan)
            Me.Panel5.Controls.Add(Me.lblSyochi_kan)
            Me.Panel5.Controls.Add(Me.Label19)
            Me.Panel5.Controls.Add(Me.Label13)
            Me.Panel5.Controls.Add(Me.Label9)
            Me.Panel5.Controls.Add(Me.Label12)
            Me.Panel5.Controls.Add(Me.Label8)
            Me.Panel5.Controls.Add(Me.Label6)
            Me.Panel5.Controls.Add(Me.lblBurokusu)
            Me.Panel5.Controls.Add(Me.Label1)
            Me.Panel5.Controls.Add(Me.lblkanryou)
            Me.Panel5.Controls.Add(Me.Label14)
            Me.Panel5.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Panel5.Location = New System.Drawing.Point(324, 3)
            Me.Panel5.Name = "Panel5"
            Me.Panel5.Size = New System.Drawing.Size(315, 102)
            Me.Panel5.TabIndex = 99
            '
            'lblShindo_syounin2
            '
            Me.lblShindo_syounin2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblShindo_syounin2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblShindo_syounin2.Location = New System.Drawing.Point(253, 78)
            Me.lblShindo_syounin2.Name = "lblShindo_syounin2"
            Me.lblShindo_syounin2.Size = New System.Drawing.Size(55, 18)
            Me.lblShindo_syounin2.TabIndex = 123
            Me.lblShindo_syounin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblNokori_syounin2
            '
            Me.lblNokori_syounin2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblNokori_syounin2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblNokori_syounin2.Location = New System.Drawing.Point(199, 78)
            Me.lblNokori_syounin2.Name = "lblNokori_syounin2"
            Me.lblNokori_syounin2.Size = New System.Drawing.Size(55, 18)
            Me.lblNokori_syounin2.TabIndex = 122
            Me.lblNokori_syounin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblSyochi_syounin2
            '
            Me.lblSyochi_syounin2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblSyochi_syounin2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblSyochi_syounin2.Location = New System.Drawing.Point(145, 78)
            Me.lblSyochi_syounin2.Name = "lblSyochi_syounin2"
            Me.lblSyochi_syounin2.Size = New System.Drawing.Size(55, 18)
            Me.lblSyochi_syounin2.TabIndex = 121
            Me.lblSyochi_syounin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label28
            '
            Me.Label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Label28.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label28.Location = New System.Drawing.Point(96, 78)
            Me.Label28.Name = "Label28"
            Me.Label28.Size = New System.Drawing.Size(55, 18)
            Me.Label28.TabIndex = 120
            Me.Label28.Text = "承認２"
            Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblShindo_syounin1
            '
            Me.lblShindo_syounin1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblShindo_syounin1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblShindo_syounin1.Location = New System.Drawing.Point(253, 61)
            Me.lblShindo_syounin1.Name = "lblShindo_syounin1"
            Me.lblShindo_syounin1.Size = New System.Drawing.Size(55, 18)
            Me.lblShindo_syounin1.TabIndex = 119
            Me.lblShindo_syounin1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblNokori_syounin1
            '
            Me.lblNokori_syounin1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblNokori_syounin1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblNokori_syounin1.Location = New System.Drawing.Point(199, 61)
            Me.lblNokori_syounin1.Name = "lblNokori_syounin1"
            Me.lblNokori_syounin1.Size = New System.Drawing.Size(55, 18)
            Me.lblNokori_syounin1.TabIndex = 118
            Me.lblNokori_syounin1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblSyochi_syounin1
            '
            Me.lblSyochi_syounin1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblSyochi_syounin1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblSyochi_syounin1.Location = New System.Drawing.Point(145, 61)
            Me.lblSyochi_syounin1.Name = "lblSyochi_syounin1"
            Me.lblSyochi_syounin1.Size = New System.Drawing.Size(55, 18)
            Me.lblSyochi_syounin1.TabIndex = 117
            Me.lblSyochi_syounin1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label24
            '
            Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Label24.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label24.Location = New System.Drawing.Point(96, 61)
            Me.Label24.Name = "Label24"
            Me.Label24.Size = New System.Drawing.Size(55, 18)
            Me.Label24.TabIndex = 116
            Me.Label24.Text = "承認１"
            Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblShindo_Kan
            '
            Me.lblShindo_Kan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblShindo_Kan.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblShindo_Kan.Location = New System.Drawing.Point(253, 44)
            Me.lblShindo_Kan.Name = "lblShindo_Kan"
            Me.lblShindo_Kan.Size = New System.Drawing.Size(55, 18)
            Me.lblShindo_Kan.TabIndex = 115
            Me.lblShindo_Kan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblNokori_kan
            '
            Me.lblNokori_kan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblNokori_kan.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblNokori_kan.Location = New System.Drawing.Point(199, 44)
            Me.lblNokori_kan.Name = "lblNokori_kan"
            Me.lblNokori_kan.Size = New System.Drawing.Size(55, 18)
            Me.lblNokori_kan.TabIndex = 114
            Me.lblNokori_kan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblSyochi_kan
            '
            Me.lblSyochi_kan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblSyochi_kan.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblSyochi_kan.Location = New System.Drawing.Point(145, 44)
            Me.lblSyochi_kan.Name = "lblSyochi_kan"
            Me.lblSyochi_kan.Size = New System.Drawing.Size(55, 18)
            Me.lblSyochi_kan.TabIndex = 113
            Me.lblSyochi_kan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label19
            '
            Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Label19.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label19.Location = New System.Drawing.Point(96, 44)
            Me.Label19.Name = "Label19"
            Me.Label19.Size = New System.Drawing.Size(55, 18)
            Me.Label19.TabIndex = 112
            Me.Label19.Text = "完了"
            Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label13
            '
            Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label13.Location = New System.Drawing.Point(2, 27)
            Me.Label13.Name = "Label13"
            Me.Label13.Size = New System.Drawing.Size(47, 18)
            Me.Label13.TabIndex = 111
            Me.Label13.Text = "完了"
            Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label9
            '
            Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label9.Location = New System.Drawing.Point(253, 27)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(55, 18)
            Me.Label9.TabIndex = 110
            Me.Label9.Text = "進度状況"
            Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label12
            '
            Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label12.Location = New System.Drawing.Point(199, 27)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(55, 18)
            Me.Label12.TabIndex = 109
            Me.Label12.Text = "残り"
            Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label8
            '
            Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label8.Location = New System.Drawing.Point(145, 27)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(55, 18)
            Me.Label8.TabIndex = 108
            Me.Label8.Text = "処置数"
            Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label6
            '
            Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label6.Location = New System.Drawing.Point(96, 27)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(55, 18)
            Me.Label6.TabIndex = 107
            Me.Label6.Text = "状態"
            Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblBurokusu
            '
            Me.lblBurokusu.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblBurokusu.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.lblBurokusu.Location = New System.Drawing.Point(253, 4)
            Me.lblBurokusu.Name = "lblBurokusu"
            Me.lblBurokusu.Size = New System.Drawing.Size(55, 24)
            Me.lblBurokusu.TabIndex = 106
            Me.lblBurokusu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label1
            '
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.Label1.Location = New System.Drawing.Point(157, 4)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(97, 24)
            Me.Label1.TabIndex = 105
            Me.Label1.Text = "総ブロック数："
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblkanryou
            '
            Me.lblkanryou.BackColor = System.Drawing.Color.LightYellow
            Me.lblkanryou.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblkanryou.Location = New System.Drawing.Point(2, 37)
            Me.lblkanryou.Name = "lblkanryou"
            Me.lblkanryou.Size = New System.Drawing.Size(93, 59)
            Me.lblkanryou.TabIndex = 101
            Me.lblkanryou.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'Label14
            '
            Me.Label14.AutoSize = True
            Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label14.Location = New System.Drawing.Point(3, 4)
            Me.Label14.Name = "Label14"
            Me.Label14.Size = New System.Drawing.Size(122, 13)
            Me.Label14.TabIndex = 9
            Me.Label14.Text = "<<進度状況　全体>>"
            '
            'lblSyochikigen2
            '
            Me.lblSyochikigen2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblSyochikigen2.ForeColor = System.Drawing.Color.Red
            Me.lblSyochikigen2.Location = New System.Drawing.Point(177, 27)
            Me.lblSyochikigen2.Name = "lblSyochikigen2"
            Me.lblSyochikigen2.Size = New System.Drawing.Size(150, 22)
            Me.lblSyochikigen2.TabIndex = 98
            Me.lblSyochikigen2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'lblSyochikigen
            '
            Me.lblSyochikigen.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblSyochikigen.ForeColor = System.Drawing.Color.Red
            Me.lblSyochikigen.Location = New System.Drawing.Point(79, 27)
            Me.lblSyochikigen.Name = "lblSyochikigen"
            Me.lblSyochikigen.Size = New System.Drawing.Size(129, 22)
            Me.lblSyochikigen.TabIndex = 97
            Me.lblSyochikigen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label11.ForeColor = System.Drawing.Color.Red
            Me.Label11.Location = New System.Drawing.Point(1, 30)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(80, 16)
            Me.Label11.TabIndex = 92
            Me.Label11.Text = "処置期限："
            '
            'btnCall
            '
            Me.btnCall.BackColor = System.Drawing.Color.LightCyan
            Me.btnCall.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnCall.Location = New System.Drawing.Point(129, 52)
            Me.btnCall.Name = "btnCall"
            Me.btnCall.Size = New System.Drawing.Size(79, 24)
            Me.btnCall.TabIndex = 84
            Me.btnCall.Text = "呼出し"
            Me.btnCall.UseVisualStyleBackColor = False
            '
            'lblBuhinNo
            '
            Me.lblBuhinNo.AutoSize = True
            Me.lblBuhinNo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblBuhinNo.Location = New System.Drawing.Point(1, 55)
            Me.lblBuhinNo.Name = "lblBuhinNo"
            Me.lblBuhinNo.Size = New System.Drawing.Size(64, 16)
            Me.lblBuhinNo.TabIndex = 83
            Me.lblBuhinNo.Text = "設計課："
            '
            'txtSekeika
            '
            Me.txtSekeika.BackColor = System.Drawing.Color.White
            Me.txtSekeika.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.txtSekeika.ForeColor = System.Drawing.Color.Navy
            Me.txtSekeika.Location = New System.Drawing.Point(68, 52)
            Me.txtSekeika.MaxLength = 4
            Me.txtSekeika.Name = "txtSekeika"
            Me.txtSekeika.Size = New System.Drawing.Size(55, 19)
            Me.txtSekeika.TabIndex = 82
            '
            'Panel4
            '
            Me.Panel4.AutoSize = True
            Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel4.Controls.Add(Me.LOGO)
            Me.Panel4.Controls.Add(Me.btnEND)
            Me.Panel4.Controls.Add(Me.btnBACK)
            Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel4.Location = New System.Drawing.Point(0, 32)
            Me.Panel4.Name = "Panel4"
            Me.Panel4.Size = New System.Drawing.Size(643, 30)
            Me.Panel4.TabIndex = 84
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
            Me.LOGO.Location = New System.Drawing.Point(250, 2)
            Me.LOGO.Name = "LOGO"
            Me.LOGO.Size = New System.Drawing.Size(142, 26)
            Me.LOGO.TabIndex = 84
            Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnEND
            '
            Me.btnEND.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnEND.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnEND.Location = New System.Drawing.Point(5, 3)
            Me.btnEND.Name = "btnEND"
            Me.btnEND.Size = New System.Drawing.Size(149, 24)
            Me.btnEND.TabIndex = 54
            Me.btnEND.Text = "アプリケーション終了"
            Me.ToolTip1.SetToolTip(Me.btnEND, "新試作手配システムを終了します。")
            Me.btnEND.UseVisualStyleBackColor = True
            '
            'btnBACK
            '
            Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBACK.Location = New System.Drawing.Point(576, 3)
            Me.btnBACK.Name = "btnBACK"
            Me.btnBACK.Size = New System.Drawing.Size(64, 24)
            Me.btnBACK.TabIndex = 55
            Me.btnBACK.Text = "戻る"
            Me.btnBACK.UseVisualStyleBackColor = True
            '
            'Timer1
            '
            Me.Timer1.Enabled = True
            '
            'Frm37DispShisakuBuhinEditSekkei
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(643, 537)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel4)
            Me.Controls.Add(Me.spdParts)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.Panel1)
            Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "Frm37DispShisakuBuhinEditSekkei"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Panel3.ResumeLayout(False)
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            Me.Panel5.ResumeLayout(False)
            Me.Panel5.PerformLayout()
            Me.Panel4.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents LblDateNow As System.Windows.Forms.Label
        Friend WithEvents LblTimeNow As System.Windows.Forms.Label
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Panel4 As System.Windows.Forms.Panel
        Friend WithEvents btnEND As System.Windows.Forms.Button
        Friend WithEvents btnBACK As System.Windows.Forms.Button
        Friend WithEvents btnCall As System.Windows.Forms.Button
        Friend WithEvents lblBuhinNo As System.Windows.Forms.Label
        Friend WithEvents txtSekeika As System.Windows.Forms.TextBox
        Friend WithEvents Label11 As System.Windows.Forms.Label
        Friend WithEvents lblSyochikigen2 As System.Windows.Forms.Label
        Friend WithEvents lblSyochikigen As System.Windows.Forms.Label
        Friend WithEvents Panel5 As System.Windows.Forms.Panel
        Friend WithEvents lblkanryou As System.Windows.Forms.Label
        Friend WithEvents Label14 As System.Windows.Forms.Label
        Friend WithEvents Label21 As System.Windows.Forms.Label
        Friend WithEvents lblIbentoName As System.Windows.Forms.Label
        Friend WithEvents lblKaihatuFugou As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label13 As System.Windows.Forms.Label
        Friend WithEvents Label9 As System.Windows.Forms.Label
        Friend WithEvents Label12 As System.Windows.Forms.Label
        Friend WithEvents Label8 As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents lblBurokusu As System.Windows.Forms.Label
        Friend WithEvents lblShindo_syounin2 As System.Windows.Forms.Label
        Friend WithEvents lblNokori_syounin2 As System.Windows.Forms.Label
        Friend WithEvents lblSyochi_syounin2 As System.Windows.Forms.Label
        Friend WithEvents Label28 As System.Windows.Forms.Label
        Friend WithEvents lblShindo_syounin1 As System.Windows.Forms.Label
        Friend WithEvents lblNokori_syounin1 As System.Windows.Forms.Label
        Friend WithEvents lblSyochi_syounin1 As System.Windows.Forms.Label
        Friend WithEvents Label24 As System.Windows.Forms.Label
        Friend WithEvents lblShindo_Kan As System.Windows.Forms.Label
        Friend WithEvents lblNokori_kan As System.Windows.Forms.Label
        Friend WithEvents lblSyochi_kan As System.Windows.Forms.Label
        Friend WithEvents Label19 As System.Windows.Forms.Label
        Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
        Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
        Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents LOGO As System.Windows.Forms.Label
        Friend WithEvents lblSekkeika As System.Windows.Forms.Label
    End Class
End Namespace
