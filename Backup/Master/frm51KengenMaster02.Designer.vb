<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm51KengenMaster02
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm51KengenMaster02))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblCurrBukaName = New System.Windows.Forms.Label
        Me.LblDateNow = New System.Windows.Forms.Label
        Me.LblCurrUserId = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.LblTimeNow = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.LblMessage = New System.Windows.Forms.Label
        Me.spdInfo = New FarPoint.Win.Spread.FpSpread
        Me.spdInfo_Sheet1 = New FarPoint.Win.Spread.SheetView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnEND = New System.Windows.Forms.Button
        Me.btnBACK = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.lblUserKubunName = New System.Windows.Forms.Label
        Me.lblUserKubun = New System.Windows.Forms.Label
        Me.lblSITE = New System.Windows.Forms.Label
        Me.lblBuka = New System.Windows.Forms.Label
        Me.lblUserName = New System.Windows.Forms.Label
        Me.lblUserCode = New System.Windows.Forms.Label
        Me.cmbMENU = New System.Windows.Forms.ComboBox
        Me.btnCall = New System.Windows.Forms.Button
        Me.lblShiyouKengen = New System.Windows.Forms.Label
        Me.lblMENU = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.spdInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdInfo_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.LblTimeNow)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(587, 32)
        Me.Panel1.TabIndex = 54
        '
        'LblCurrBukaName
        '
        Me.LblCurrBukaName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblCurrBukaName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrBukaName.ForeColor = System.Drawing.Color.White
        Me.LblCurrBukaName.Location = New System.Drawing.Point(362, 14)
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
        Me.LblDateNow.Location = New System.Drawing.Point(498, 1)
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
        Me.LblCurrUserId.Location = New System.Drawing.Point(362, 1)
        Me.LblCurrUserId.Name = "LblCurrUserId"
        Me.LblCurrUserId.Size = New System.Drawing.Size(130, 13)
        Me.LblCurrUserId.TabIndex = 72
        Me.LblCurrUserId.Text = "(ID    ：ABCDEFGH)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 13)
        Me.Label2.TabIndex = 59
        Me.Label2.Text = "PG-ID：MASTER051"
        '
        'LblTimeNow
        '
        Me.LblTimeNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTimeNow.AutoSize = True
        Me.LblTimeNow.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTimeNow.ForeColor = System.Drawing.Color.White
        Me.LblTimeNow.Location = New System.Drawing.Point(522, 14)
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
        Me.Label4.Location = New System.Drawing.Point(194, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(152, 31)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "権限マスター"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.AutoSize = True
        Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.LblMessage)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 283)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(587, 25)
        Me.Panel3.TabIndex = 71
        '
        'LblMessage
        '
        Me.LblMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblMessage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMessage.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMessage.ForeColor = System.Drawing.Color.Red
        Me.LblMessage.Location = New System.Drawing.Point(1, 1)
        Me.LblMessage.Name = "LblMessage"
        Me.LblMessage.Size = New System.Drawing.Size(583, 22)
        Me.LblMessage.TabIndex = 55
        Me.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdInfo
        '
        Me.spdInfo.AccessibleDescription = "spdInfo, Sheet1, Row 0, Column 0, "
        Me.spdInfo.AllowColumnMove = True
        Me.spdInfo.AllowDragFill = True
        Me.spdInfo.AllowUserFormulas = True
        Me.spdInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdInfo.BackColor = System.Drawing.SystemColors.Control
        Me.spdInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.spdInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdInfo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.spdInfo.Location = New System.Drawing.Point(240, 109)
        Me.spdInfo.Name = "spdInfo"
        Me.spdInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.spdInfo.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spdInfo_Sheet1})
        Me.spdInfo.Size = New System.Drawing.Size(342, 101)
        Me.spdInfo.TabIndex = 2
        Me.spdInfo.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Fixed
        Me.spdInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spdInfo.VisualStyles = FarPoint.Win.VisualStyles.Off
        '
        'spdInfo_Sheet1
        '
        Me.spdInfo_Sheet1.Reset()
        Me.spdInfo_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spdInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spdInfo_Sheet1.ColumnCount = 3
        Me.spdInfo_Sheet1.RowCount = 0
        Me.spdInfo_Sheet1.ActiveRowIndex = -1
        Me.spdInfo_Sheet1.AllowNoteEdit = True
        Me.spdInfo_Sheet1.AlternatingRows.Get(0).BackColor = System.Drawing.Color.WhiteSmoke
        Me.spdInfo_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.Gainsboro
        Me.spdInfo_Sheet1.AutoGenerateColumns = False
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "画面"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "機能"
        Me.spdInfo_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "権限"
        Me.spdInfo_Sheet1.ColumnHeader.Rows.Get(0).Height = 18.0!
        Me.spdInfo_Sheet1.Columns.Get(0).Label = "画面"
        Me.spdInfo_Sheet1.Columns.Get(0).Locked = False
        Me.spdInfo_Sheet1.Columns.Get(0).Tag = ""
        Me.spdInfo_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General
        Me.spdInfo_Sheet1.Columns.Get(0).Width = 152.0!
        Me.spdInfo_Sheet1.Columns.Get(1).Label = "機能"
        Me.spdInfo_Sheet1.Columns.Get(1).Locked = False
        Me.spdInfo_Sheet1.Columns.Get(1).Width = 85.0!
        Me.spdInfo_Sheet1.Columns.Get(2).Label = "権限"
        Me.spdInfo_Sheet1.Columns.Get(2).Locked = False
        Me.spdInfo_Sheet1.Columns.Get(2).Width = 85.0!
        Me.spdInfo_Sheet1.RowHeader.Columns.Default.Resizable = False
        Me.spdInfo_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.spdInfo_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
        Me.spdInfo_Sheet1.RowHeader.Visible = False
        Me.spdInfo_Sheet1.Rows.Default.Height = 16.0!
        Me.spdInfo_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange
        Me.spdInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        Me.spdInfo.SetActiveViewport(0, -1, 0)
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
        Me.Panel2.Size = New System.Drawing.Size(587, 32)
        Me.Panel2.TabIndex = 11
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
        Me.btnEND.TabIndex = 51
        Me.btnEND.Text = "アプリケーション終了"
        Me.btnEND.UseVisualStyleBackColor = True
        Me.btnEND.Visible = False
        '
        'btnBACK
        '
        Me.btnBACK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBACK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBACK.Location = New System.Drawing.Point(518, 3)
        Me.btnBACK.Name = "btnBACK"
        Me.btnBACK.Size = New System.Drawing.Size(64, 24)
        Me.btnBACK.TabIndex = 50
        Me.btnBACK.Text = "戻る"
        Me.btnBACK.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(10, 11)
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
        Me.Panel4.Controls.Add(Me.lblUserKubunName)
        Me.Panel4.Controls.Add(Me.lblUserKubun)
        Me.Panel4.Controls.Add(Me.lblSITE)
        Me.Panel4.Controls.Add(Me.lblBuka)
        Me.Panel4.Controls.Add(Me.lblUserName)
        Me.Panel4.Controls.Add(Me.lblUserCode)
        Me.Panel4.Controls.Add(Me.cmbMENU)
        Me.Panel4.Controls.Add(Me.btnCall)
        Me.Panel4.Controls.Add(Me.lblShiyouKengen)
        Me.Panel4.Controls.Add(Me.lblMENU)
        Me.Panel4.Controls.Add(Me.spdInfo)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 64)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(587, 218)
        Me.Panel4.TabIndex = 10
        '
        'lblUserKubunName
        '
        Me.lblUserKubunName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUserKubunName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUserKubunName.Location = New System.Drawing.Point(133, 58)
        Me.lblUserKubunName.Name = "lblUserKubunName"
        Me.lblUserKubunName.Size = New System.Drawing.Size(100, 22)
        Me.lblUserKubunName.TabIndex = 94
        Me.lblUserKubunName.Text = "設計部署"
        Me.lblUserKubunName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUserKubun
        '
        Me.lblUserKubun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUserKubun.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUserKubun.Location = New System.Drawing.Point(102, 58)
        Me.lblUserKubun.Name = "lblUserKubun"
        Me.lblUserKubun.Size = New System.Drawing.Size(32, 22)
        Me.lblUserKubun.TabIndex = 93
        Me.lblUserKubun.Text = "1"
        Me.lblUserKubun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSITE
        '
        Me.lblSITE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSITE.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSITE.Location = New System.Drawing.Point(265, 28)
        Me.lblSITE.Name = "lblSITE"
        Me.lblSITE.Size = New System.Drawing.Size(76, 22)
        Me.lblSITE.TabIndex = 92
        Me.lblSITE.Text = "1234"
        Me.lblSITE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBuka
        '
        Me.lblBuka.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBuka.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBuka.Location = New System.Drawing.Point(191, 28)
        Me.lblBuka.Name = "lblBuka"
        Me.lblBuka.Size = New System.Drawing.Size(75, 22)
        Me.lblBuka.TabIndex = 91
        Me.lblBuka.Text = "SKE1"
        Me.lblBuka.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUserName
        '
        Me.lblUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUserName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUserName.Location = New System.Drawing.Point(191, 7)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(150, 22)
        Me.lblUserName.TabIndex = 90
        Me.lblUserName.Text = "×沼　×夫"
        Me.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUserCode
        '
        Me.lblUserCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUserCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUserCode.Location = New System.Drawing.Point(102, 7)
        Me.lblUserCode.Name = "lblUserCode"
        Me.lblUserCode.Size = New System.Drawing.Size(90, 22)
        Me.lblUserCode.TabIndex = 89
        Me.lblUserCode.Text = "ABCDEFGH"
        Me.lblUserCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbMENU
        '
        Me.cmbMENU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMENU.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMENU.FormattingEnabled = True
        Me.cmbMENU.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbMENU.Items.AddRange(New Object() {"", "ＳＫＥ１メニュー", "設計メニュー"})
        Me.cmbMENU.Location = New System.Drawing.Point(102, 87)
        Me.cmbMENU.Name = "cmbMENU"
        Me.cmbMENU.Size = New System.Drawing.Size(132, 23)
        Me.cmbMENU.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbMENU, "ユーザーのメニュー区分を指定してください。")
        '
        'btnCall
        '
        Me.btnCall.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCall.BackColor = System.Drawing.Color.LightCyan
        Me.btnCall.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCall.Location = New System.Drawing.Point(503, 5)
        Me.btnCall.Name = "btnCall"
        Me.btnCall.Size = New System.Drawing.Size(79, 24)
        Me.btnCall.TabIndex = 3
        Me.btnCall.Text = "更新"
        Me.btnCall.UseVisualStyleBackColor = False
        '
        'lblShiyouKengen
        '
        Me.lblShiyouKengen.AutoSize = True
        Me.lblShiyouKengen.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShiyouKengen.Location = New System.Drawing.Point(241, 90)
        Me.lblShiyouKengen.Name = "lblShiyouKengen"
        Me.lblShiyouKengen.Size = New System.Drawing.Size(121, 15)
        Me.lblShiyouKengen.TabIndex = 83
        Me.lblShiyouKengen.Text = "システム使用権限："
        '
        'lblMENU
        '
        Me.lblMENU.AutoSize = True
        Me.lblMENU.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMENU.Location = New System.Drawing.Point(8, 90)
        Me.lblMENU.Name = "lblMENU"
        Me.lblMENU.Size = New System.Drawing.Size(58, 15)
        Me.lblMENU.TabIndex = 82
        Me.lblMENU.Text = "メニュー："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 62)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 15)
        Me.Label6.TabIndex = 80
        Me.Label6.Text = "ユーザー区分："
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'frm51KengenMaster02
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(587, 308)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frm51KengenMaster02"
        Me.Text = "新試作手配システム Ver 1.00"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.spdInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdInfo_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LblDateNow As System.Windows.Forms.Label
    Friend WithEvents LblTimeNow As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents LblMessage As System.Windows.Forms.Label
    Friend WithEvents LblCurrBukaName As System.Windows.Forms.Label
    Friend WithEvents LblCurrUserId As System.Windows.Forms.Label
    Friend WithEvents spdInfo As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spdInfo_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblShiyouKengen As System.Windows.Forms.Label
    Friend WithEvents lblMENU As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnCall As System.Windows.Forms.Button
    Friend WithEvents cmbMENU As System.Windows.Forms.ComboBox
    Friend WithEvents btnBACK As System.Windows.Forms.Button
    Friend WithEvents btnEND As System.Windows.Forms.Button
    Friend WithEvents lblSITE As System.Windows.Forms.Label
    Friend WithEvents lblBuka As System.Windows.Forms.Label
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents lblUserCode As System.Windows.Forms.Label
    Friend WithEvents lblUserKubunName As System.Windows.Forms.Label
    Friend WithEvents lblUserKubun As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
