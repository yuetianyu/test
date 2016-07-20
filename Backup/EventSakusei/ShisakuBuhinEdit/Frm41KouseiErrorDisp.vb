Imports ShisakuCommon
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Public Class Frm41KouseiErrorDisp
    Private _errorInstlHinban As Hashtable
    Private tabError As System.Windows.Forms.TabControl
    Private tabPagesInstls As System.Windows.Forms.TabControl.TabPageCollection

    Const COL_START_POSITION_LEFT As Integer = 5
    Const COL_START_POSITION_RIGHT As Integer = 380
    Const ROW_START_POSITION As Integer = 50
    Const COL_WIDTH As Integer = 80
    Const ROW_HIGHT As Integer = 18
    Const COL_INTERVAL As Integer = 2
    Const ROW_INTERVAL As Integer = 2

    Private HEAD_COLOR As Color = Color.Aqua
    Private COLUMN_COLOR_1 As Color = Color.Cyan
    Private COLUMN_COLOR_2 As Color = Color.Cyan
    Private ERROR_COLOR As Color = Color.Red
    Private FONT_DISP As Font = New Font("MS UI Gothic", 9)



    Public Sub New(ByVal errorInstlHinban As Hashtable)

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ShisakuFormUtil.Initialize(Me)

        Me._errorInstlHinban = errorInstlHinban

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub

    Private Sub Frm41KouseiErrorDisp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ShisakuFormUtil.setTitleVersion(Me)

        Me.HEAD_COLOR = Me.Label3.BackColor
        Me.COLUMN_COLOR_1 = Me.Label1.BackColor
        Me.COLUMN_COLOR_2 = Me.Label2.BackColor

        '動的にコントロールを作る
        Me.tabError = New System.Windows.Forms.TabControl
        Me.tabError.Name = "tabErrorDisp1"


        'パネル情報　6,32 760,180


        For Each key As String In _errorInstlHinban.Keys
            Dim instlHinbanNo As String = key
            Dim tabPage As New System.Windows.Forms.TabPage
            Dim panelDisp As New System.Windows.Forms.Panel

            panelDisp.Name = "pnl" + instlHinbanNo
            panelDisp.Size = New System.Drawing.Size(760, 180)
            panelDisp.Location = New Point(6, 32)
            panelDisp.BorderStyle = BorderStyle.Fixed3D
            panelDisp.AutoScroll = True

            tabPage.Name = "tabPage" + instlHinbanNo
            tabPage.Text = instlHinbanNo
            tabPage.Font = FONT_DISP


            Me.tabError.TabPages.Add(tabPage)
            Dim errObjs As New Hashtable
            'Dim errObjs As New List(Of List(Of TShisakuBuhinEditVo))

            errObjs = _errorInstlHinban(instlHinbanNo)
            Dim rowCount As Integer = 0
            Dim errListOfShisaku As New List(Of TShisakuBuhinEditVo)
            Dim errListOfEbom As New List(Of TShisakuBuhinEditVo)
            If errObjs.ContainsKey("SHISAKU") Then
                errListOfShisaku = errObjs("SHISAKU")
                rowCount = errListOfShisaku.Count - 1
            End If
            If errObjs.ContainsKey("EBOM") Then
                errListOfEbom = errObjs("EBOM")
                If rowCount < errListOfEbom.Count - 1 Then
                    rowCount = errListOfEbom.Count - 1
                End If
            End If

            Me.Controls.Add(panelDisp)
            panelDisp.Parent = tabError.TabPages(tabPage.Name)

            For i As Integer = 0 To rowCount
                If i < errListOfEbom.Count Then
                    Dim err1 As New TShisakuBuhinEditVo

                    Dim err1lbl_ShukaiCode As New Windows.Forms.Label
                    Dim err1lbl_BuhinNo As New Windows.Forms.Label
                    Dim err1lbl_Level As New Windows.Forms.Label
                    Dim err1lbl_Insu As New Windows.Forms.Label


                    err1 = errListOfEbom(i)

                    'If err1.BuhinNo = "XXX" Then
                    '    Dim errUmmatchCount As New Windows.Forms.Label
                    '    errUmmatchCount.Name = "lbl_unmatch_" + instlHinbanNo + i.ToString
                    '    errUmmatchCount.BackColor = ERROR_COLOR

                    '    errUmmatchCount.BorderStyle = BorderStyle.Fixed3D
                    '    errUmmatchCount.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
                    '    errUmmatchCount.Location = New Point(COL_START_POSITION_LEFT, ROW_START_POSITION + (ROW_HIGHT + ROW_INTERVAL) * i)
                    '    errUmmatchCount.Location = New Point(COL_START_POSITION_RIGHT, ROW_START_POSITION + (ROW_HIGHT + ROW_INTERVAL) * i)
                    '    Me.Controls.Add(errUmmatchCount)
                    '    Me.Controls.Add(errUmmatchCount)
                    '    errUmmatchCount.Parent = panelDisp
                    '    Continue For
                    'End If
                    '----------------------------------------------------------------
                    err1lbl_Level.Name = "lblErr_Level_Left_" + instlHinbanNo + i.ToString + err1.BuhinNo

                    err1lbl_Level.Font = FONT_DISP

                    err1lbl_Level.Text = err1.Level

                    'If Not StringUtil.Equals(err1.Level, err2.Level) Then
                    '    err1lbl_Level.BackColor = ERROR_COLOR
                    'Else
                    err1lbl_Level.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    '    err2lbl_Level.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    'End If

                    err1lbl_Level.BorderStyle = BorderStyle.Fixed3D

                    err1lbl_Level.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
                    err1lbl_Level.Location = New Point(COL_START_POSITION_LEFT, ROW_START_POSITION + (ROW_HIGHT + ROW_INTERVAL) * i)
                    Me.Controls.Add(err1lbl_Level)
                    err1lbl_Level.Parent = panelDisp

                    '------------------------------------------------------------
                    err1lbl_ShukaiCode.Name = "lblErr_ShukaiCode_Left_" + instlHinbanNo + i.ToString + err1.BuhinNo

                    err1lbl_ShukaiCode.Font = FONT_DISP

                    err1lbl_ShukaiCode.Text = err1.ShukeiCode

                    'If Not StringUtil.Equals(err1.ShukeiCode, err2.ShukeiCode) Then
                    '    err1lbl_ShukaiCode.BackColor = ERROR_COLOR
                    '    err2lbl_ShukaiCode.BackColor = ERROR_COLOR
                    'Else
                    err1lbl_ShukaiCode.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    '    err2lbl_ShukaiCode.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    'End If

                    err1lbl_ShukaiCode.BorderStyle = BorderStyle.Fixed3D

                    err1lbl_ShukaiCode.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)

                    err1lbl_ShukaiCode.Location = New Point(COL_START_POSITION_LEFT + (COL_WIDTH + COL_INTERVAL) * 1, ROW_START_POSITION + (ROW_HIGHT + ROW_INTERVAL) * i)
                    Me.Controls.Add(err1lbl_ShukaiCode)
                    err1lbl_ShukaiCode.Parent = panelDisp

                    '----------------------------------------------------------------
                    err1lbl_BuhinNo.Name = "lblErr_BuhinNo_Left_" + instlHinbanNo + i.ToString + err1.BuhinNo

                    err1lbl_BuhinNo.Font = FONT_DISP

                    err1lbl_BuhinNo.Text = err1.BuhinNo

                    'If Not StringUtil.Equals(err1.BuhinNo, err2.BuhinNo) Then
                    '    err1lbl_BuhinNo.BackColor = ERROR_COLOR
                    '    err2lbl_BuhinNo.BackColor = ERROR_COLOR
                    'Else
                    err1lbl_BuhinNo.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    '    err2lbl_BuhinNo.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    'End If

                    err1lbl_BuhinNo.BorderStyle = BorderStyle.Fixed3D

                    err1lbl_BuhinNo.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
                    err1lbl_BuhinNo.Location = New Point(COL_START_POSITION_LEFT + (COL_WIDTH + COL_INTERVAL) * 2, ROW_START_POSITION + (ROW_HIGHT + ROW_INTERVAL) * i)

                    Me.Controls.Add(err1lbl_BuhinNo)
                    err1lbl_BuhinNo.Parent = panelDisp


                    '----------------------------------------------------------------
                    err1lbl_Insu.Name = "lblErr_Insu_Left_" + instlHinbanNo + i.ToString + err1.BuhinNo

                    err1lbl_Insu.Font = FONT_DISP

                    err1lbl_Insu.Text = err1.BuhinNote

                    'If Not StringUtil.Equals(err1.BuhinNote, err2.BuhinNote) Then
                    '    err1lbl_Insu.BackColor = ERROR_COLOR
                    '    err2lbl_Insu.BackColor = ERROR_COLOR
                    'Else
                    err1lbl_Insu.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    '    err2lbl_Insu.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    'End If

                    err1lbl_Insu.BorderStyle = BorderStyle.Fixed3D

                    err1lbl_Insu.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
                    err1lbl_Insu.Location = New Point(COL_START_POSITION_LEFT + (COL_WIDTH + COL_INTERVAL) * 3, ROW_START_POSITION + (ROW_HIGHT + ROW_INTERVAL) * i)
                    Me.Controls.Add(err1lbl_Insu)
                    err1lbl_Insu.Parent = panelDisp

                End If
                If i < errListOfShisaku.Count Then
                    Dim err2 As New TShisakuBuhinEditVo

                    Dim err2lbl_ShukaiCode As New Windows.Forms.Label
                    Dim err2lbl_BuhinNo As New Windows.Forms.Label
                    Dim err2lbl_Level As New Windows.Forms.Label
                    Dim err2lbl_Insu As New Windows.Forms.Label


                    err2 = errListOfShisaku(i)

                    'If err1.BuhinNo = "XXX" Then
                    '    Dim errUmmatchCount As New Windows.Forms.Label
                    '    errUmmatchCount.Name = "lbl_unmatch_" + instlHinbanNo + i.ToString
                    '    errUmmatchCount.BackColor = ERROR_COLOR

                    '    errUmmatchCount.BorderStyle = BorderStyle.Fixed3D
                    '    errUmmatchCount.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
                    '    errUmmatchCount.Location = New Point(COL_START_POSITION_LEFT, ROW_START_POSITION + (ROW_HIGHT + ROW_INTERVAL) * i)
                    '    errUmmatchCount.Location = New Point(COL_START_POSITION_RIGHT, ROW_START_POSITION + (ROW_HIGHT + ROW_INTERVAL) * i)
                    '    Me.Controls.Add(errUmmatchCount)
                    '    Me.Controls.Add(errUmmatchCount)
                    '    errUmmatchCount.Parent = panelDisp
                    '    Continue For
                    'End If
                    '----------------------------------------------------------------
                    err2lbl_Level.Name = "lblErr_Level_Right_" + instlHinbanNo + i.ToString + err2.BuhinNo

                    err2lbl_Level.Font = FONT_DISP

                    err2lbl_Level.Text = err2.Level

                    'If Not StringUtil.Equals(err1.Level, err2.Level) Then
                    '    err1lbl_Level.BackColor = ERROR_COLOR
                    '    err2lbl_Level.BackColor = ERROR_COLOR
                    'Else
                    '    err1lbl_Level.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    err2lbl_Level.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    'End If

                    err2lbl_Level.BorderStyle = BorderStyle.Fixed3D

                    err2lbl_Level.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
                    err2lbl_Level.Location = New Point(COL_START_POSITION_RIGHT, ROW_START_POSITION + (ROW_HIGHT + ROW_INTERVAL) * i)
                    Me.Controls.Add(err2lbl_Level)
                    err2lbl_Level.Parent = panelDisp

                    '------------------------------------------------------------
                    err2lbl_ShukaiCode.Name = "lblErr_ShukaiCode_Right_" + instlHinbanNo + i.ToString + err2.BuhinNo

                    err2lbl_ShukaiCode.Font = FONT_DISP

                    err2lbl_ShukaiCode.Text = err2.ShukeiCode

                    'If Not StringUtil.Equals(err1.ShukeiCode, err2.ShukeiCode) Then
                    '    err1lbl_ShukaiCode.BackColor = ERROR_COLOR
                    '    err2lbl_ShukaiCode.BackColor = ERROR_COLOR
                    'Else
                    '    err1lbl_ShukaiCode.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    err2lbl_ShukaiCode.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    'End If

                    err2lbl_ShukaiCode.BorderStyle = BorderStyle.Fixed3D

                    err2lbl_ShukaiCode.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)

                    err2lbl_ShukaiCode.Location = New Point(COL_START_POSITION_RIGHT + (COL_WIDTH + COL_INTERVAL) * 1, ROW_START_POSITION + (ROW_HIGHT + ROW_INTERVAL) * i)
                    Me.Controls.Add(err2lbl_ShukaiCode)
                    err2lbl_ShukaiCode.Parent = panelDisp

                    '----------------------------------------------------------------
                    err2lbl_BuhinNo.Name = "lblErr_BuhinNo_Right_" + instlHinbanNo + i.ToString + err2.BuhinNo

                    err2lbl_BuhinNo.Font = FONT_DISP

                    err2lbl_BuhinNo.Text = err2.BuhinNo

                    'If Not StringUtil.Equals(err1.BuhinNo, err2.BuhinNo) Then
                    '    err1lbl_BuhinNo.BackColor = ERROR_COLOR
                    '    err2lbl_BuhinNo.BackColor = ERROR_COLOR
                    'Else
                    '    err1lbl_BuhinNo.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    err2lbl_BuhinNo.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    'End If

                    err2lbl_BuhinNo.BorderStyle = BorderStyle.Fixed3D

                    err2lbl_BuhinNo.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
                    err2lbl_BuhinNo.Location = New Point(COL_START_POSITION_RIGHT + (COL_WIDTH + COL_INTERVAL) * 2, ROW_START_POSITION + (ROW_HIGHT + ROW_INTERVAL) * i)

                    Me.Controls.Add(err2lbl_BuhinNo)
                    err2lbl_BuhinNo.Parent = panelDisp


                    '----------------------------------------------------------------
                    err2lbl_Insu.Name = "lblErr_Insu_Right_" + instlHinbanNo + i.ToString + err2.BuhinNo

                    err2lbl_Insu.Font = FONT_DISP

                    err2lbl_Insu.Text = err2.BuhinNote

                    'If Not StringUtil.Equals(err1.BuhinNote, err2.BuhinNote) Then
                    '    err1lbl_Insu.BackColor = ERROR_COLOR
                    '    err2lbl_Insu.BackColor = ERROR_COLOR
                    'Else
                    '    err1lbl_Insu.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    err2lbl_Insu.BackColor = IIf(i Mod 2 = 0, COLUMN_COLOR_1, COLUMN_COLOR_2)
                    'End If

                    err2lbl_Insu.BorderStyle = BorderStyle.Fixed3D

                    err2lbl_Insu.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
                    err2lbl_Insu.Location = New Point(COL_START_POSITION_RIGHT + (COL_WIDTH + COL_INTERVAL) * 3, ROW_START_POSITION + (ROW_HIGHT + ROW_INTERVAL) * i)
                    Me.Controls.Add(err2lbl_Insu)
                    err2lbl_Insu.Parent = panelDisp

                End If


            Next

            'ヘッダーをパネルに追加
            Me.CreateHeaderLavel(panelDisp, instlHinbanNo)
        Next

        Me.tabError.Size = New System.Drawing.Size(780, 240)
        Me.tabError.Location = New Point(13, 90)

        Me.Controls.Add(tabError)

        'Me.tabError.Visible = False


    End Sub
    Private Sub CreateHeaderLavel(ByVal panelDisp As System.Windows.Forms.Panel, ByVal instlHinbanNo As String)

        Dim errEbomLabel As New Windows.Forms.Label
        Dim errShisakuLabel As New Windows.Forms.Label

        Dim errEbomLabelLevel As New Windows.Forms.Label
        Dim errEbomLabelShukaiCode As New Windows.Forms.Label
        Dim errEbomLabelBuhinNo As New Windows.Forms.Label
        Dim errEbomLabelInsu As New Windows.Forms.Label

        Dim errShisakuLabelLevel As New Windows.Forms.Label
        Dim errShisakuLabelShukaiCode As New Windows.Forms.Label
        Dim errShisakuLabelBuhinNo As New Windows.Forms.Label
        Dim errShisakuLabelInsu As New Windows.Forms.Label

        '_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        'ヘッダーの１列目
        '_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        '----------------------------------------------------------------
        'ヘッダー（左側-EBOM情報）
        '----------------------------------------------------------------
        errEbomLabel.Name = "lbl_errEbomLabel_" + instlHinbanNo
        errEbomLabel.Font = FONT_DISP
        errEbomLabel.Text = "E-BOM情報"
        errEbomLabel.BackColor = HEAD_COLOR
        errEbomLabel.BorderStyle = BorderStyle.Fixed3D
        errEbomLabel.Size = New System.Drawing.Size(COL_WIDTH + (COL_WIDTH + COL_INTERVAL) * 3, ROW_HIGHT)
        errEbomLabel.Location = New Point(COL_START_POSITION_LEFT, ROW_START_POSITION - (ROW_HIGHT + ROW_INTERVAL) * 2)

        Me.Controls.Add(errEbomLabel)
        errEbomLabel.Parent = panelDisp

        '----------------------------------------------------------------
        'ヘッダー（右側-試作システム情報）
        '----------------------------------------------------------------
        errShisakuLabel.Name = "lbl_errShisakuLabel_" + instlHinbanNo
        errShisakuLabel.Font = FONT_DISP
        errShisakuLabel.Text = "試作手配システム"
        errShisakuLabel.BackColor = HEAD_COLOR
        errShisakuLabel.BorderStyle = BorderStyle.Fixed3D
        errShisakuLabel.Size = New System.Drawing.Size(COL_WIDTH + (COL_WIDTH + COL_INTERVAL) * 3, ROW_HIGHT)
        errShisakuLabel.Location = New Point(COL_START_POSITION_RIGHT, ROW_START_POSITION - (ROW_HIGHT + ROW_INTERVAL) * 2)

        Me.Controls.Add(errShisakuLabel)
        errShisakuLabel.Parent = panelDisp


        '_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        'ヘッダーの２列目
        '_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        '----------------------------------------------------------------
        'ヘッダー２（左側-EBOM情報-レベル）
        '----------------------------------------------------------------
        errEbomLabelLevel.Name = "lbl_errEbomLabelLevel_" + instlHinbanNo
        errEbomLabelLevel.Font = FONT_DISP
        errEbomLabelLevel.Text = "レベル"
        errEbomLabelLevel.BackColor = HEAD_COLOR
        errEbomLabelLevel.BorderStyle = BorderStyle.Fixed3D
        errEbomLabelLevel.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
        errEbomLabelLevel.Location = New Point(COL_START_POSITION_LEFT, ROW_START_POSITION - (ROW_HIGHT + ROW_INTERVAL))

        Me.Controls.Add(errEbomLabelLevel)
        errEbomLabelLevel.Parent = panelDisp

        '----------------------------------------------------------------
        'ヘッダー２（左側-EBOM情報-集計コード）
        '----------------------------------------------------------------
        errEbomLabelShukaiCode.Name = "lbl_errEbomLabelShukaiCode_" + instlHinbanNo
        errEbomLabelShukaiCode.Font = FONT_DISP
        errEbomLabelShukaiCode.Text = "集計コード"
        errEbomLabelShukaiCode.BackColor = HEAD_COLOR
        errEbomLabelShukaiCode.BorderStyle = BorderStyle.Fixed3D
        errEbomLabelShukaiCode.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
        errEbomLabelShukaiCode.Location = New Point(COL_START_POSITION_LEFT + (COL_WIDTH + COL_INTERVAL) * 1, ROW_START_POSITION - (ROW_HIGHT + ROW_INTERVAL))

        Me.Controls.Add(errEbomLabelShukaiCode)
        errEbomLabelShukaiCode.Parent = panelDisp

        '----------------------------------------------------------------
        'ヘッダー２（左側-EBOM情報-部品番号）
        '----------------------------------------------------------------
        errEbomLabelBuhinNo.Name = "lbl_errEbomLabelBuhinNo_" + instlHinbanNo
        errEbomLabelBuhinNo.Font = FONT_DISP
        errEbomLabelBuhinNo.Text = "部品番号"
        errEbomLabelBuhinNo.BackColor = HEAD_COLOR
        errEbomLabelBuhinNo.BorderStyle = BorderStyle.Fixed3D
        errEbomLabelBuhinNo.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
        errEbomLabelBuhinNo.Location = New Point(COL_START_POSITION_LEFT + (COL_WIDTH + COL_INTERVAL) * 2, ROW_START_POSITION - (ROW_HIGHT + ROW_INTERVAL))

        Me.Controls.Add(errEbomLabelBuhinNo)
        errEbomLabelBuhinNo.Parent = panelDisp


        '----------------------------------------------------------------
        'ヘッダー２（左側-EBOM情報-員数）
        '----------------------------------------------------------------
        errEbomLabelInsu.Name = "lbl_errEbomLabelInsu_" + instlHinbanNo
        errEbomLabelInsu.Font = FONT_DISP
        errEbomLabelInsu.Text = "員数"
        errEbomLabelInsu.BackColor = HEAD_COLOR
        errEbomLabelInsu.BorderStyle = BorderStyle.Fixed3D
        errEbomLabelInsu.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
        errEbomLabelInsu.Location = New Point(COL_START_POSITION_LEFT + (COL_WIDTH + COL_INTERVAL) * 3, ROW_START_POSITION - (ROW_HIGHT + ROW_INTERVAL))

        Me.Controls.Add(errEbomLabelInsu)
        errEbomLabelInsu.Parent = panelDisp


        '----------------------------------------------------------------
        'ヘッダー２（右側-試作システム情報-レベル）
        '----------------------------------------------------------------
        errShisakuLabelLevel.Name = "lbl_errShisakuLabelLevel_" + instlHinbanNo
        errShisakuLabelLevel.Font = FONT_DISP
        errShisakuLabelLevel.Text = "レベル"
        errShisakuLabelLevel.BackColor = HEAD_COLOR
        errShisakuLabelLevel.BorderStyle = BorderStyle.Fixed3D
        errShisakuLabelLevel.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
        errShisakuLabelLevel.Location = New Point(COL_START_POSITION_RIGHT, ROW_START_POSITION - (ROW_HIGHT + ROW_INTERVAL))

        Me.Controls.Add(errShisakuLabelLevel)
        errShisakuLabelLevel.Parent = panelDisp

        '----------------------------------------------------------------
        'ヘッダー２（右側-試作システム情報-集計コード）
        '----------------------------------------------------------------
        errShisakuLabelShukaiCode.Name = "lbl_errShisakuLabelShukaiCode_" + instlHinbanNo
        errShisakuLabelShukaiCode.Font = FONT_DISP
        errShisakuLabelShukaiCode.Text = "集計コード"
        errShisakuLabelShukaiCode.BackColor = HEAD_COLOR
        errShisakuLabelShukaiCode.BorderStyle = BorderStyle.Fixed3D
        errShisakuLabelShukaiCode.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
        errShisakuLabelShukaiCode.Location = New Point(COL_START_POSITION_RIGHT + (COL_WIDTH + COL_INTERVAL) * 1, ROW_START_POSITION - (ROW_HIGHT + ROW_INTERVAL))

        Me.Controls.Add(errShisakuLabelShukaiCode)
        errShisakuLabelShukaiCode.Parent = panelDisp

        '----------------------------------------------------------------
        'ヘッダー２（右側-試作システム情報-部品番号）
        '----------------------------------------------------------------
        errShisakuLabelBuhinNo.Name = "lbl_errShisakuLabelBuhinNo_" + instlHinbanNo
        errShisakuLabelBuhinNo.Font = FONT_DISP
        errShisakuLabelBuhinNo.Text = "部品番号"
        errShisakuLabelBuhinNo.BackColor = HEAD_COLOR
        errShisakuLabelBuhinNo.BorderStyle = BorderStyle.Fixed3D
        errShisakuLabelBuhinNo.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
        errShisakuLabelBuhinNo.Location = New Point(COL_START_POSITION_RIGHT + (COL_WIDTH + COL_INTERVAL) * 2, ROW_START_POSITION - (ROW_HIGHT + ROW_INTERVAL))

        Me.Controls.Add(errShisakuLabelBuhinNo)
        errShisakuLabelBuhinNo.Parent = panelDisp


        '----------------------------------------------------------------
        'ヘッダー２（右側-試作システム情報-員数）
        '----------------------------------------------------------------
        errShisakuLabelInsu.Name = "lbl_errShisakuLabelInsu_" + instlHinbanNo
        errShisakuLabelInsu.Font = FONT_DISP
        errShisakuLabelInsu.Text = "員数"
        errShisakuLabelInsu.BackColor = HEAD_COLOR
        errShisakuLabelInsu.BorderStyle = BorderStyle.Fixed3D
        errShisakuLabelInsu.Size = New System.Drawing.Size(COL_WIDTH, ROW_HIGHT)
        errShisakuLabelInsu.Location = New Point(COL_START_POSITION_RIGHT + (COL_WIDTH + COL_INTERVAL) * 3, ROW_START_POSITION - (ROW_HIGHT + ROW_INTERVAL))

        Me.Controls.Add(errShisakuLabelInsu)
        errShisakuLabelInsu.Parent = panelDisp

    End Sub
    Private Sub bntClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntClose.Click
        Me.Close()
    End Sub
End Class