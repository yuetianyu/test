Namespace ShisakuBuhinKaiteiBlock
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Frm60DispShisakuBuhinKaiteiBlock
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
            Dim BevelBorder1 As FarPoint.Win.BevelBorder = New FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered)
            Dim ButtonCellType1 As FarPoint.Win.Spread.CellType.ButtonCellType = New FarPoint.Win.Spread.CellType.ButtonCellType
            Dim BevelBorder2 As FarPoint.Win.BevelBorder = New FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered)
            Dim ButtonCellType2 As FarPoint.Win.Spread.CellType.ButtonCellType = New FarPoint.Win.Spread.CellType.ButtonCellType
            Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim CheckBoxCellType1 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType
            Dim TextCellType3 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType4 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType5 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType6 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType7 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType8 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType9 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType10 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType11 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim CheckBoxCellType2 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType
            Dim TextCellType12 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType13 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType14 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim CheckBoxCellType3 As FarPoint.Win.Spread.CellType.CheckBoxCellType = New FarPoint.Win.Spread.CellType.CheckBoxCellType
            Dim TextCellType15 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType16 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim TextCellType17 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm60DispShisakuBuhinKaiteiBlock))
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.LblCurrBukaName = New System.Windows.Forms.Label
            Me.LblCurrUserId = New System.Windows.Forms.Label
            Me.LblDateNow = New System.Windows.Forms.Label
            Me.Label2 = New System.Windows.Forms.Label
            Me.LblTimeNow = New System.Windows.Forms.Label
            Me.Label4 = New System.Windows.Forms.Label
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.spdParts = New FarPoint.Win.Spread.FpSpread
            Me.spdParts_Sheet1 = New FarPoint.Win.Spread.SheetView
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.lblSaisyuKoushinbi = New System.Windows.Forms.Label
            Me.Panel5 = New System.Windows.Forms.Panel
            Me.Label3 = New System.Windows.Forms.Label
            Me.rbtDate02 = New System.Windows.Forms.RadioButton
            Me.rbtDate01 = New System.Windows.Forms.RadioButton
            Me.cmbBlockNo02 = New System.Windows.Forms.ComboBox
            Me.cmbBlockNo01 = New System.Windows.Forms.ComboBox
            Me.cmbBuka = New System.Windows.Forms.ComboBox
            Me.cmbEventCode = New System.Windows.Forms.ComboBox
            Me.btnExcelExport = New System.Windows.Forms.Button
            Me.dtpSaisyuKoushinbi02 = New System.Windows.Forms.DateTimePicker
            Me.dtpSaisyuKoushinbi01 = New System.Windows.Forms.DateTimePicker
            Me.Label1 = New System.Windows.Forms.Label
            Me.Label5 = New System.Windows.Forms.Label
            Me.btnDisplayK = New System.Windows.Forms.Button
            Me.btnDisplayT = New System.Windows.Forms.Button
            Me.ToolStripPanel1 = New System.Windows.Forms.ToolStripPanel
            Me.Label21 = New System.Windows.Forms.Label
            Me.lblKaihatuFugou = New System.Windows.Forms.Label
            Me.Label12 = New System.Windows.Forms.Label
            Me.btnCall = New System.Windows.Forms.Button
            Me.lblBuhinNo = New System.Windows.Forms.Label
            Me.Panel4 = New System.Windows.Forms.Panel
            Me.LOGO = New System.Windows.Forms.Label
            Me.btnEND = New System.Windows.Forms.Button
            Me.btnBACK = New System.Windows.Forms.Button
            Me.Panel6 = New System.Windows.Forms.Panel
            Me.Label10 = New System.Windows.Forms.Label
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.Panel1.SuspendLayout()
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel2.SuspendLayout()
            Me.Panel5.SuspendLayout()
            Me.Panel4.SuspendLayout()
            Me.Panel6.SuspendLayout()
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
            Me.Panel1.Size = New System.Drawing.Size(980, 32)
            Me.Panel1.TabIndex = 54
            '
            'LblCurrBukaName
            '
            Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
            Me.LblCurrBukaName.Location = New System.Drawing.Point(754, 14)
            Me.LblCurrBukaName.Name = "LblCurrBukaName"
            Me.LblCurrBukaName.Size = New System.Drawing.Size(130, 13)
            Me.LblCurrBukaName.TabIndex = 77
            '
            'LblCurrUserId
            '
            Me.LblCurrUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblCurrUserId.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblCurrUserId.ForeColor = System.Drawing.Color.White
            Me.LblCurrUserId.Location = New System.Drawing.Point(754, 1)
            Me.LblCurrUserId.Name = "LblCurrUserId"
            Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
            Me.LblCurrUserId.TabIndex = 76
            '
            'LblDateNow
            '
            Me.LblDateNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblDateNow.AutoSize = True
            Me.LblDateNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblDateNow.ForeColor = System.Drawing.Color.White
            Me.LblDateNow.Location = New System.Drawing.Point(891, 1)
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
            Me.Label2.Size = New System.Drawing.Size(113, 13)
            Me.Label2.TabIndex = 59
            Me.Label2.Text = "PG-ID：XXXXXXXX"
            '
            'LblTimeNow
            '
            Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblTimeNow.AutoSize = True
            Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.LblTimeNow.ForeColor = System.Drawing.Color.White
            Me.LblTimeNow.Location = New System.Drawing.Point(915, 14)
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
            Me.Label4.Location = New System.Drawing.Point(170, 1)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(590, 31)
            Me.Label4.TabIndex = 54
            Me.Label4.Text = "試作部品表 改訂通知（ブロック）"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Panel3
            '
            Me.Panel3.AutoSize = True
            Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel3.BackColor = System.Drawing.SystemColors.Control
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel3.Location = New System.Drawing.Point(0, 666)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(980, 2)
            Me.Panel3.TabIndex = 71
            '
            'spdParts
            '
            Me.spdParts.AccessibleDescription = "spdParts, Sheet1, Row 0, Column 0, "
            Me.spdParts.AllowDragFill = True
            Me.spdParts.AllowUserFormulas = True
            Me.spdParts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.spdParts.BackColor = System.Drawing.SystemColors.Control
            Me.spdParts.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentCell
            Me.spdParts.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.spdParts.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.Location = New System.Drawing.Point(3, 160)
            Me.spdParts.Name = "spdParts"
            Me.spdParts.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.spdParts.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdParts_Sheet1})
            Me.spdParts.Size = New System.Drawing.Size(973, 475)
            Me.spdParts.TabIndex = 8
            Me.spdParts.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Floating
            Me.spdParts.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
            Me.spdParts.VisualStyles = FarPoint.Win.VisualStyles.Off
            '
            'spdParts_Sheet1
            '
            Me.spdParts_Sheet1.Reset()
            Me.spdParts_Sheet1.SheetName = "Sheet1"
            'Formulas and custom names must be loaded with R1C1 reference style
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
            Me.spdParts_Sheet1.ColumnCount = 22
            Me.spdParts_Sheet1.ColumnHeader.RowCount = 3
            Me.spdParts_Sheet1.RowCount = 100
            Me.spdParts_Sheet1.AllowNoteEdit = True
            Me.spdParts_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
            Me.spdParts_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
            Me.spdParts_Sheet1.AutoGenerateColumns = False
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "イベントコード"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "設計課"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "ブロック不要"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).ColumnSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "状態"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "状態"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "ブロック№"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "改訂"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "ユニット"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 8).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "ブロック名称"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "担当者"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "TEL"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 11).RowSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "最終更新日"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 12).BackColor = System.Drawing.SystemColors.Control
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 12).ColumnSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 12).TabStop = True
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "担当"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 14).ColumnSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 14).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "詳細"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 17).ColumnSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 17).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "課長主査"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 19).ColumnSpan = 3
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "詳細"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "承認者"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(0, 21).Value = "承認日"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 12).ColumnSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 12).Value = "承認"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 13).Value = "承認"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 14).Value = "所属"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 15).Value = "承認者"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 16).Value = "承認日"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 17).ColumnSpan = 2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 17).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 17).Value = "承認"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 19).Value = "所属"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 20).Value = "承認者"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(1, 21).Value = "承認日"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 12).Border = BevelBorder1
            ButtonCellType1.ButtonColor = System.Drawing.SystemColors.ActiveBorder
            ButtonCellType1.ButtonColor2 = System.Drawing.SystemColors.ButtonFace
            ButtonCellType1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
            ButtonCellType1.ShadowSize = 3
            ButtonCellType1.Text = "全て選択"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 12).CellType = ButtonCellType1
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 12).Locked = True
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 12).Value = "0"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 13).Value = "状態"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 14).Value = " "
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 15).Value = " "
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 16).Value = " "
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 17).Border = BevelBorder2
            ButtonCellType2.ButtonColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            ButtonCellType2.ButtonColor2 = System.Drawing.SystemColors.ButtonFace
            ButtonCellType2.ShadowSize = 3
            ButtonCellType2.Text = "全て選択"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 17).CellType = ButtonCellType2
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 17).Locked = True
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 17).Value = "0"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 18).Value = " 状態"
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 19).Value = " "
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 20).Value = " "
            Me.spdParts_Sheet1.ColumnHeader.Cells.Get(2, 21).Value = " "
            Me.spdParts_Sheet1.ColumnHeader.Rows.Get(0).Height = 24.0!
            Me.spdParts_Sheet1.ColumnHeader.Rows.Get(2).Height = 24.0!
            Me.spdParts_Sheet1.Columns.Get(0).CellType = TextCellType1
            Me.spdParts_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(0).Locked = True
            Me.spdParts_Sheet1.Columns.Get(0).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(0).Tag = "NO"
            Me.spdParts_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(0).Width = 100.0!
            Me.spdParts_Sheet1.Columns.Get(1).CellType = TextCellType2
            Me.spdParts_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(1).Locked = True
            Me.spdParts_Sheet1.Columns.Get(1).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(1).Tag = "NO"
            Me.spdParts_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(1).Width = 56.0!
            Me.spdParts_Sheet1.Columns.Get(2).CellType = CheckBoxCellType1
            Me.spdParts_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(2).Locked = True
            Me.spdParts_Sheet1.Columns.Get(2).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(2).Tag = "FUYOU"
            Me.spdParts_Sheet1.Columns.Get(3).CellType = TextCellType3
            Me.spdParts_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(3).Locked = True
            Me.spdParts_Sheet1.Columns.Get(3).Tag = "JYOUTAI1"
            Me.spdParts_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(3).VisualStyles = FarPoint.Win.VisualStyles.[Auto]
            Me.spdParts_Sheet1.Columns.Get(4).CellType = TextCellType4
            Me.spdParts_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(4).Locked = False
            Me.spdParts_Sheet1.Columns.Get(4).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(4).Tag = "JYOUTAI2"
            Me.spdParts_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(4).VisualStyles = FarPoint.Win.VisualStyles.[Auto]
            Me.spdParts_Sheet1.Columns.Get(5).CellType = TextCellType5
            Me.spdParts_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(5).Locked = True
            Me.spdParts_Sheet1.Columns.Get(5).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(5).Tag = "NO"
            Me.spdParts_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(5).Width = 56.0!
            Me.spdParts_Sheet1.Columns.Get(6).CellType = TextCellType6
            Me.spdParts_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(6).Locked = True
            Me.spdParts_Sheet1.Columns.Get(6).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(6).Tag = "KAITEI"
            Me.spdParts_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(6).Width = 30.0!
            Me.spdParts_Sheet1.Columns.Get(7).CellType = TextCellType7
            Me.spdParts_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(7).Locked = True
            Me.spdParts_Sheet1.Columns.Get(7).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(7).Tag = "UNIT"
            Me.spdParts_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(7).Width = 30.0!
            Me.spdParts_Sheet1.Columns.Get(8).CellType = TextCellType8
            Me.spdParts_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(8).Locked = True
            Me.spdParts_Sheet1.Columns.Get(8).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(8).Tag = "MEISHOU"
            Me.spdParts_Sheet1.Columns.Get(8).Width = 125.0!
            Me.spdParts_Sheet1.Columns.Get(9).CellType = TextCellType9
            Me.spdParts_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(9).Locked = True
            Me.spdParts_Sheet1.Columns.Get(9).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(9).Tag = "TANTOU"
            Me.spdParts_Sheet1.Columns.Get(9).Width = 92.0!
            Me.spdParts_Sheet1.Columns.Get(10).CellType = TextCellType10
            Me.spdParts_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(10).Locked = True
            Me.spdParts_Sheet1.Columns.Get(10).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(10).Tag = "TEL"
            Me.spdParts_Sheet1.Columns.Get(10).Width = 40.0!
            Me.spdParts_Sheet1.Columns.Get(11).CellType = TextCellType11
            Me.spdParts_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Me.spdParts_Sheet1.Columns.Get(11).Locked = True
            Me.spdParts_Sheet1.Columns.Get(11).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(11).TabStop = True
            Me.spdParts_Sheet1.Columns.Get(11).Tag = "UPDATETIME"
            Me.spdParts_Sheet1.Columns.Get(11).Width = 125.0!
            Me.spdParts_Sheet1.Columns.Get(12).CellType = CheckBoxCellType2
            Me.spdParts_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(12).Label = "0"
            Me.spdParts_Sheet1.Columns.Get(12).Locked = False
            Me.spdParts_Sheet1.Columns.Get(12).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(12).Tag = "SP_T_SYOUNIN"
            Me.spdParts_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
            Me.spdParts_Sheet1.Columns.Get(12).Width = 66.0!
            Me.spdParts_Sheet1.Columns.Get(14).CellType = TextCellType12
            Me.spdParts_Sheet1.Columns.Get(14).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(14).Label = " "
            Me.spdParts_Sheet1.Columns.Get(14).Locked = True
            Me.spdParts_Sheet1.Columns.Get(14).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(14).Tag = "TANTO_SYOZOKU"
            Me.spdParts_Sheet1.Columns.Get(14).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdParts_Sheet1.Columns.Get(15).CellType = TextCellType13
            Me.spdParts_Sheet1.Columns.Get(15).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(15).Label = " "
            Me.spdParts_Sheet1.Columns.Get(15).Locked = True
            Me.spdParts_Sheet1.Columns.Get(15).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(15).Tag = "TANTO_SYOUNINSYA"
            Me.spdParts_Sheet1.Columns.Get(15).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdParts_Sheet1.Columns.Get(15).Width = 92.0!
            Me.spdParts_Sheet1.Columns.Get(16).CellType = TextCellType14
            Me.spdParts_Sheet1.Columns.Get(16).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(16).Label = " "
            Me.spdParts_Sheet1.Columns.Get(16).Locked = True
            Me.spdParts_Sheet1.Columns.Get(16).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(16).Tag = "TANTO_SYOUNINBI"
            Me.spdParts_Sheet1.Columns.Get(16).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdParts_Sheet1.Columns.Get(16).Width = 120.0!
            Me.spdParts_Sheet1.Columns.Get(17).CellType = CheckBoxCellType3
            Me.spdParts_Sheet1.Columns.Get(17).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(17).Label = "0"
            Me.spdParts_Sheet1.Columns.Get(17).Locked = False
            Me.spdParts_Sheet1.Columns.Get(17).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(17).Tag = "KACHOU_SYOUNIN"
            Me.spdParts_Sheet1.Columns.Get(17).Width = 66.0!
            Me.spdParts_Sheet1.Columns.Get(19).CellType = TextCellType15
            Me.spdParts_Sheet1.Columns.Get(19).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(19).Label = " "
            Me.spdParts_Sheet1.Columns.Get(19).Locked = True
            Me.spdParts_Sheet1.Columns.Get(19).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(19).Tag = "KACHOU_SYOZOKU"
            Me.spdParts_Sheet1.Columns.Get(19).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdParts_Sheet1.Columns.Get(20).CellType = TextCellType16
            Me.spdParts_Sheet1.Columns.Get(20).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(20).Label = " "
            Me.spdParts_Sheet1.Columns.Get(20).Locked = True
            Me.spdParts_Sheet1.Columns.Get(20).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(20).Tag = "KACHOU_SYOUNINSYA"
            Me.spdParts_Sheet1.Columns.Get(20).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdParts_Sheet1.Columns.Get(20).Width = 92.0!
            Me.spdParts_Sheet1.Columns.Get(21).CellType = TextCellType17
            Me.spdParts_Sheet1.Columns.Get(21).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Me.spdParts_Sheet1.Columns.Get(21).Label = " "
            Me.spdParts_Sheet1.Columns.Get(21).Locked = True
            Me.spdParts_Sheet1.Columns.Get(21).Resizable = False
            Me.spdParts_Sheet1.Columns.Get(21).Tag = "KACHOU_SYOUNINBI"
            Me.spdParts_Sheet1.Columns.Get(21).VisualStyles = FarPoint.Win.VisualStyles.[On]
            Me.spdParts_Sheet1.Columns.Get(21).Width = 120.0!
            Me.spdParts_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode
            Me.spdParts_Sheet1.RowHeader.Columns.Default.Resizable = False
            Me.spdParts_Sheet1.RowHeader.DefaultStyle.Locked = False
            Me.spdParts_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
            Me.spdParts_Sheet1.Rows.Default.Height = 16.0!
            Me.spdParts_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
            '
            'Panel2
            '
            Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel2.BackColor = System.Drawing.SystemColors.Control
            Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel2.Controls.Add(Me.lblSaisyuKoushinbi)
            Me.Panel2.Controls.Add(Me.Panel5)
            Me.Panel2.Controls.Add(Me.cmbBlockNo02)
            Me.Panel2.Controls.Add(Me.cmbBlockNo01)
            Me.Panel2.Controls.Add(Me.cmbBuka)
            Me.Panel2.Controls.Add(Me.cmbEventCode)
            Me.Panel2.Controls.Add(Me.btnExcelExport)
            Me.Panel2.Controls.Add(Me.dtpSaisyuKoushinbi02)
            Me.Panel2.Controls.Add(Me.dtpSaisyuKoushinbi01)
            Me.Panel2.Controls.Add(Me.Label1)
            Me.Panel2.Controls.Add(Me.Label5)
            Me.Panel2.Controls.Add(Me.btnDisplayK)
            Me.Panel2.Controls.Add(Me.btnDisplayT)
            Me.Panel2.Controls.Add(Me.ToolStripPanel1)
            Me.Panel2.Controls.Add(Me.Label21)
            Me.Panel2.Controls.Add(Me.lblKaihatuFugou)
            Me.Panel2.Controls.Add(Me.Label12)
            Me.Panel2.Controls.Add(Me.btnCall)
            Me.Panel2.Controls.Add(Me.lblBuhinNo)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel2.Location = New System.Drawing.Point(0, 62)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(980, 92)
            Me.Panel2.TabIndex = 83
            '
            'lblSaisyuKoushinbi
            '
            Me.lblSaisyuKoushinbi.AutoSize = True
            Me.lblSaisyuKoushinbi.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblSaisyuKoushinbi.Location = New System.Drawing.Point(214, 36)
            Me.lblSaisyuKoushinbi.Name = "lblSaisyuKoushinbi"
            Me.lblSaisyuKoushinbi.Size = New System.Drawing.Size(96, 16)
            Me.lblSaisyuKoushinbi.TabIndex = 157
            Me.lblSaisyuKoushinbi.Text = "課長承認日："
            '
            'Panel5
            '
            Me.Panel5.Controls.Add(Me.Label3)
            Me.Panel5.Controls.Add(Me.rbtDate02)
            Me.Panel5.Controls.Add(Me.rbtDate01)
            Me.Panel5.Location = New System.Drawing.Point(19, 32)
            Me.Panel5.Name = "Panel5"
            Me.Panel5.Size = New System.Drawing.Size(197, 24)
            Me.Panel5.TabIndex = 163
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label3.Location = New System.Drawing.Point(3, 4)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(80, 16)
            Me.Label3.TabIndex = 2
            Me.Label3.Text = "日付指定："
            '
            'rbtDate02
            '
            Me.rbtDate02.AutoSize = True
            Me.rbtDate02.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.rbtDate02.Location = New System.Drawing.Point(132, 2)
            Me.rbtDate02.Name = "rbtDate02"
            Me.rbtDate02.Size = New System.Drawing.Size(65, 20)
            Me.rbtDate02.TabIndex = 8
            Me.rbtDate02.Text = "しない"
            Me.rbtDate02.UseVisualStyleBackColor = True
            '
            'rbtDate01
            '
            Me.rbtDate01.AutoSize = True
            Me.rbtDate01.Checked = True
            Me.rbtDate01.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.rbtDate01.Location = New System.Drawing.Point(84, 2)
            Me.rbtDate01.Name = "rbtDate01"
            Me.rbtDate01.Size = New System.Drawing.Size(52, 20)
            Me.rbtDate01.TabIndex = 7
            Me.rbtDate01.TabStop = True
            Me.rbtDate01.Text = "する"
            Me.rbtDate01.UseVisualStyleBackColor = True
            '
            'cmbBlockNo02
            '
            Me.cmbBlockNo02.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbBlockNo02.FormattingEnabled = True
            Me.cmbBlockNo02.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.cmbBlockNo02.Items.AddRange(New Object() {"", "SKE1", "設計"})
            Me.cmbBlockNo02.Location = New System.Drawing.Point(540, 6)
            Me.cmbBlockNo02.Name = "cmbBlockNo02"
            Me.cmbBlockNo02.Size = New System.Drawing.Size(69, 23)
            Me.cmbBlockNo02.TabIndex = 4
            Me.ToolTip1.SetToolTip(Me.cmbBlockNo02, "あいまい検索（前方一致）が可能です。")
            '
            'cmbBlockNo01
            '
            Me.cmbBlockNo01.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbBlockNo01.FormattingEnabled = True
            Me.cmbBlockNo01.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.cmbBlockNo01.Items.AddRange(New Object() {"", "SKE1", "設計"})
            Me.cmbBlockNo01.Location = New System.Drawing.Point(435, 6)
            Me.cmbBlockNo01.Name = "cmbBlockNo01"
            Me.cmbBlockNo01.Size = New System.Drawing.Size(69, 23)
            Me.cmbBlockNo01.TabIndex = 3
            Me.ToolTip1.SetToolTip(Me.cmbBlockNo01, "あいまい検索（前方一致）が可能です。")
            '
            'cmbBuka
            '
            Me.cmbBuka.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbBuka.FormattingEnabled = True
            Me.cmbBuka.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.cmbBuka.Items.AddRange(New Object() {"", "SKE1", "設計"})
            Me.cmbBuka.Location = New System.Drawing.Point(278, 6)
            Me.cmbBuka.Name = "cmbBuka"
            Me.cmbBuka.Size = New System.Drawing.Size(69, 23)
            Me.cmbBuka.TabIndex = 2
            Me.ToolTip1.SetToolTip(Me.cmbBuka, "あいまい検索（前方一致）が可能です。")
            '
            'cmbEventCode
            '
            Me.cmbEventCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbEventCode.FormattingEnabled = True
            Me.cmbEventCode.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.cmbEventCode.Items.AddRange(New Object() {"", "B", "G", "R", "S", "W", "Y", "Z"})
            Me.cmbEventCode.Location = New System.Drawing.Point(71, 6)
            Me.cmbEventCode.Name = "cmbEventCode"
            Me.cmbEventCode.Size = New System.Drawing.Size(137, 23)
            Me.cmbEventCode.TabIndex = 1
            '
            'btnExcelExport
            '
            Me.btnExcelExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnExcelExport.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnExcelExport.ImageAlign = System.Drawing.ContentAlignment.TopLeft
            Me.btnExcelExport.Location = New System.Drawing.Point(243, 61)
            Me.btnExcelExport.Name = "btnExcelExport"
            Me.btnExcelExport.Size = New System.Drawing.Size(110, 26)
            Me.btnExcelExport.TabIndex = 10
            Me.btnExcelExport.Text = "EXCEL出力"
            Me.ToolTip1.SetToolTip(Me.btnExcelExport, "一覧表の内容を「EXCEL」へ出力します。")
            Me.btnExcelExport.UseVisualStyleBackColor = True
            '
            'dtpSaisyuKoushinbi02
            '
            Me.dtpSaisyuKoushinbi02.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.dtpSaisyuKoushinbi02.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.dtpSaisyuKoushinbi02.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpSaisyuKoushinbi02.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.dtpSaisyuKoushinbi02.Location = New System.Drawing.Point(450, 34)
            Me.dtpSaisyuKoushinbi02.Name = "dtpSaisyuKoushinbi02"
            Me.dtpSaisyuKoushinbi02.Size = New System.Drawing.Size(105, 22)
            Me.dtpSaisyuKoushinbi02.TabIndex = 6
            '
            'dtpSaisyuKoushinbi01
            '
            Me.dtpSaisyuKoushinbi01.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.dtpSaisyuKoushinbi01.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.dtpSaisyuKoushinbi01.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
            Me.dtpSaisyuKoushinbi01.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.dtpSaisyuKoushinbi01.Location = New System.Drawing.Point(310, 34)
            Me.dtpSaisyuKoushinbi01.Name = "dtpSaisyuKoushinbi01"
            Me.dtpSaisyuKoushinbi01.Size = New System.Drawing.Size(105, 22)
            Me.dtpSaisyuKoushinbi01.TabIndex = 5
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label1.Location = New System.Drawing.Point(510, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(24, 16)
            Me.Label1.TabIndex = 161
            Me.Label1.Text = "～"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label5.Location = New System.Drawing.Point(420, 36)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(24, 16)
            Me.Label5.TabIndex = 159
            Me.Label5.Text = "～"
            '
            'btnDisplayK
            '
            Me.btnDisplayK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnDisplayK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnDisplayK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnDisplayK.Location = New System.Drawing.Point(822, 60)
            Me.btnDisplayK.Name = "btnDisplayK"
            Me.btnDisplayK.Size = New System.Drawing.Size(152, 24)
            Me.btnDisplayK.TabIndex = 152
            Me.btnDisplayK.Text = "課長承認詳細表示"
            Me.btnDisplayK.UseVisualStyleBackColor = False
            '
            'btnDisplayT
            '
            Me.btnDisplayT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnDisplayT.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnDisplayT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnDisplayT.Location = New System.Drawing.Point(659, 60)
            Me.btnDisplayT.Name = "btnDisplayT"
            Me.btnDisplayT.Size = New System.Drawing.Size(155, 24)
            Me.btnDisplayT.TabIndex = 151
            Me.btnDisplayT.Text = "担当承認詳細表示"
            Me.btnDisplayT.UseVisualStyleBackColor = False
            '
            'ToolStripPanel1
            '
            Me.ToolStripPanel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.ToolStripPanel1.Location = New System.Drawing.Point(0, 0)
            Me.ToolStripPanel1.Name = "ToolStripPanel1"
            Me.ToolStripPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal
            Me.ToolStripPanel1.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
            Me.ToolStripPanel1.Size = New System.Drawing.Size(978, 0)
            '
            'Label21
            '
            Me.Label21.AutoSize = True
            Me.Label21.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label21.Location = New System.Drawing.Point(8, 8)
            Me.Label21.Name = "Label21"
            Me.Label21.Size = New System.Drawing.Size(64, 16)
            Me.Label21.TabIndex = 146
            Me.Label21.Text = "イベント："
            '
            'lblKaihatuFugou
            '
            Me.lblKaihatuFugou.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblKaihatuFugou.Location = New System.Drawing.Point(70, 6)
            Me.lblKaihatuFugou.Name = "lblKaihatuFugou"
            Me.lblKaihatuFugou.Size = New System.Drawing.Size(54, 20)
            Me.lblKaihatuFugou.TabIndex = 144
            Me.lblKaihatuFugou.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Label12
            '
            Me.Label12.AutoSize = True
            Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label12.Location = New System.Drawing.Point(214, 10)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(64, 16)
            Me.Label12.TabIndex = 119
            Me.Label12.Text = "設計課："
            '
            'btnCall
            '
            Me.btnCall.BackColor = System.Drawing.Color.LightCyan
            Me.btnCall.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnCall.Location = New System.Drawing.Point(11, 60)
            Me.btnCall.Name = "btnCall"
            Me.btnCall.Size = New System.Drawing.Size(79, 24)
            Me.btnCall.TabIndex = 9
            Me.btnCall.Text = "検索"
            Me.btnCall.UseVisualStyleBackColor = False
            '
            'lblBuhinNo
            '
            Me.lblBuhinNo.AutoSize = True
            Me.lblBuhinNo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblBuhinNo.Location = New System.Drawing.Point(359, 9)
            Me.lblBuhinNo.Name = "lblBuhinNo"
            Me.lblBuhinNo.Size = New System.Drawing.Size(76, 16)
            Me.lblBuhinNo.TabIndex = 103
            Me.lblBuhinNo.Text = "ブロック№："
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
            Me.Panel4.Size = New System.Drawing.Size(980, 30)
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
            Me.LOGO.Location = New System.Drawing.Point(382, 2)
            Me.LOGO.Name = "LOGO"
            Me.LOGO.Size = New System.Drawing.Size(157, 26)
            Me.LOGO.TabIndex = 83
            Me.LOGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnEND
            '
            Me.btnEND.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnEND.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnEND.Location = New System.Drawing.Point(5, 3)
            Me.btnEND.Name = "btnEND"
            Me.btnEND.Size = New System.Drawing.Size(149, 24)
            Me.btnEND.TabIndex = 10
            Me.btnEND.Text = "アプリケーション終了"
            Me.ToolTip1.SetToolTip(Me.btnEND, "新試作手配システムを終了します。")
            Me.btnEND.UseVisualStyleBackColor = True
            '
            'btnBACK
            '
            Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnBACK.Location = New System.Drawing.Point(912, 3)
            Me.btnBACK.Name = "btnBACK"
            Me.btnBACK.Size = New System.Drawing.Size(64, 24)
            Me.btnBACK.TabIndex = 9
            Me.btnBACK.Text = "戻る"
            Me.btnBACK.UseVisualStyleBackColor = True
            '
            'Panel6
            '
            Me.Panel6.AutoSize = True
            Me.Panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Panel6.BackColor = System.Drawing.SystemColors.Control
            Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel6.Controls.Add(Me.Label10)
            Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel6.Location = New System.Drawing.Point(0, 641)
            Me.Panel6.Name = "Panel6"
            Me.Panel6.Size = New System.Drawing.Size(980, 25)
            Me.Panel6.TabIndex = 85
            '
            'Label10
            '
            Me.Label10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Label10.ForeColor = System.Drawing.Color.Red
            Me.Label10.Location = New System.Drawing.Point(-1, 1)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(977, 22)
            Me.Label10.TabIndex = 55
            Me.Label10.Text = "検索条件を入力して「検索」ボタンをクリックして下さい。"
            Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Timer1
            '
            Me.Timer1.Enabled = True
            '
            'Frm60DispShisakuBuhinKaiteiBlock
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(980, 668)
            Me.Controls.Add(Me.Panel6)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel4)
            Me.Controls.Add(Me.spdParts)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.Panel1)
            Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "Frm60DispShisakuBuhinKaiteiBlock"
            Me.Text = "新試作手配システム Ver 1.00"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            CType(Me.spdParts, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.spdParts_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            Me.Panel5.ResumeLayout(False)
            Me.Panel5.PerformLayout()
            Me.Panel4.ResumeLayout(False)
            Me.Panel6.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents LblDateNow As System.Windows.Forms.Label
        Friend WithEvents LblTimeNow As System.Windows.Forms.Label
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents spdParts As FarPoint.Win.Spread.FpSpread
        Friend WithEvents spdParts_Sheet1 As FarPoint.Win.Spread.SheetView
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Panel4 As System.Windows.Forms.Panel
        Friend WithEvents btnEND As System.Windows.Forms.Button
        Friend WithEvents btnBACK As System.Windows.Forms.Button
        Friend WithEvents btnCall As System.Windows.Forms.Button
        Friend WithEvents lblBuhinNo As System.Windows.Forms.Label
        Friend WithEvents LOGO As System.Windows.Forms.Label
        Friend WithEvents Panel6 As System.Windows.Forms.Panel
        Friend WithEvents Label10 As System.Windows.Forms.Label
        Friend WithEvents Label12 As System.Windows.Forms.Label
        Friend WithEvents ToolStripPanel1 As System.Windows.Forms.ToolStripPanel
        Friend WithEvents btnDisplayK As System.Windows.Forms.Button
        Friend WithEvents btnDisplayT As System.Windows.Forms.Button
        Friend WithEvents Label21 As System.Windows.Forms.Label
        Friend WithEvents lblKaihatuFugou As System.Windows.Forms.Label
        Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
        Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
        Friend WithEvents lblSaisyuKoushinbi As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents dtpSaisyuKoushinbi01 As System.Windows.Forms.DateTimePicker
        Friend WithEvents dtpSaisyuKoushinbi02 As System.Windows.Forms.DateTimePicker
        Friend WithEvents btnExcelExport As System.Windows.Forms.Button
        Friend WithEvents cmbEventCode As System.Windows.Forms.ComboBox
        Friend WithEvents cmbBuka As System.Windows.Forms.ComboBox
        Friend WithEvents cmbBlockNo02 As System.Windows.Forms.ComboBox
        Friend WithEvents cmbBlockNo01 As System.Windows.Forms.ComboBox
        Friend WithEvents Panel5 As System.Windows.Forms.Panel
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents rbtDate02 As System.Windows.Forms.RadioButton
        Friend WithEvents rbtDate01 As System.Windows.Forms.RadioButton
    End Class
End Namespace
