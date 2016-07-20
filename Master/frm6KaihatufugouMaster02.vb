Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports EBom.Data
Imports ShisakuCommon
Imports Microsoft.Win32
Imports FarPoint.Win.Spread.CellType
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd

''' <summary>
''' 部品構成表示画面
''' </summary>
''' <remarks></remarks>

Public Class frm6KaihatufugouMaster02
    Public a As frm6KaihatufugouMaster01
    Private isLoad As Boolean = True
    Private m_spCom As SpreadCommon
    Private changKey As Integer

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)

    End Sub

    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        If changKey = 0 Then
            Me.Close()
        Else
            frm01Kakunin.lblKakunin.Text = T0008
            frm01Kakunin.lblKakunin2.Text = ""
            frm6Para = "btnBACK"
            frm01Kakunin.ShowDialog()
            Select Case frm6ParaModori
                Case "B"
                    Me.Close()
            End Select
        End If
    End Sub

    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
        frm01Kakunin.lblKakunin.Text = T0008
        frm01Kakunin.lblKakunin2.Text = ""
        frm6Para = "btnEND"
        frm01Kakunin.ShowDialog()
        Select Case frm6ParaModori
            Case "E"
                Application.Exit()
                System.Environment.Exit(0)
        End Select
    End Sub

    Private Sub btnNEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNEW.Click
        frm01Kakunin.lblKakunin.Text = T0004
        frm01Kakunin.lblKakunin2.Text = ""
        frm6Para = "btnADD"
        frm01Kakunin.ShowDialog()
        Select Case frm6ParaModori
            Case ""
                frm01Kakunin.Close()
                Exit Sub
        End Select
        'チェックOK
        If insertCheck = RESULT.OK Then
            '登録する
            insertIntoDB()
            isLoad = False
            changKey = 0
            a.loadProcess()
            a.cmbShakei.Text = a.strShakeiComm
            a.cmbFugo.Text = a.strFugoComm
            a.cmbPhase.Text = a.strPhaseComm
            a.cmbName.Text = a.strNameComm
        Else
            spdParts.Focus()
        End If
    End Sub

    Private Sub insertIntoDB()
        Dim strShakei As String = ""
        Dim strFugo As String = ""
        Dim strNo As String = ""
        Dim strPhase As String = ""
        Dim strName As String = ""
        Dim dtList As New DataTable()
        Dim i As Integer = 0
        Dim arrRow As New ArrayList
        strShakei = cmbShakei.Text
        strFugo = cmbFugo.Text
        m_spCom = New SpreadCommon(spdParts)
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            strPhase = m_spCom.GetValue("Phase", i)
            strName = m_spCom.GetValue("Event", i)
            strNo = m_spCom.GetValue("Id", i)
            '更新処理
            If Not strNo = String.Empty Then
                If Not strPhase = String.Empty And Not strName = String.Empty Then
                    strNo = m_spCom.GetValue("Id", i)
                    updateProcess(strShakei, strFugo, strPhase, strName, strNo)
                End If
            End If
            '登録処理
            If strNo = String.Empty Then
                If Not strPhase = String.Empty And Not strName = String.Empty Then
                    strNo = getNo(strShakei, strFugo) + 1
                    insertProcess(strShakei, strFugo, strPhase, strName, strNo)
                End If
            End If
            '削除処理
            If Not strNo = String.Empty And strPhase = String.Empty And strName = String.Empty Then
                deleteProcess(strShakei, strFugo, strNo)
            End If
        Next
        ComFunc.ShowInfoMsgBox(M0001)
        isLoad = True
        loadProcess()
    End Sub


    Private Sub deleteProcess(ByVal strShakei As String, ByVal strFugo As String, ByVal strNo As String)
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            db.BeginTransaction()

            db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
            db.AddParameter("@HYOJIJUN_NO", strNo, DbType.AnsiString)

            db.ExecuteNonQuery(DataSqlCommon.GetKaihatufugouDeleteSql(strShakei, strFugo, strNo))
            db.Commit()
        End Using
    End Sub

    Private Sub updateProcess(ByVal strShakei As String, ByVal strFugo As String, ByVal strPhase As String, ByVal strName As String, ByVal strNo As String)
        Dim updatedDateTime As New ShisakuDate
        Dim strDateNow = updatedDateTime.CurrentDateDbFormat
        Dim strTimeNow = updatedDateTime.CurrentTimeDbFormat
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            db.BeginTransaction()

            db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
            db.AddParameter("@HYOJIJUN_NO", strNo, DbType.AnsiString)
            db.AddParameter("@SHISAKU_EVENT_PHASE", strPhase, DbType.AnsiString)
            db.AddParameter("@SHISAKU_EVENT_PHASE_NAME", strName, DbType.AnsiString)
            db.AddParameter("@UPDATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
            db.AddParameter("@UPDATED_DATE", strDateNow, DbType.AnsiString)
            db.AddParameter("@UPDATED_TIME", strTimeNow, DbType.AnsiString)

            db.ExecuteNonQuery(DataSqlCommon.GetKaihatufugouUpdateSql())
            db.Commit()
        End Using
    End Sub

    Private Sub insertProcess(ByVal strShakei As String, ByVal strFugo As String, ByVal strPhase As String, ByVal strName As String, ByVal strNo As String)
        Dim updatedDateTime As New ShisakuDate
        Dim strDateNow = updatedDateTime.CurrentDateDbFormat
        Dim strTimeNow = updatedDateTime.CurrentTimeDbFormat
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            db.BeginTransaction()

            db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
            db.AddParameter("@HYOJIJUN_NO", strNo, DbType.AnsiString)
            db.AddParameter("@SHISAKU_EVENT_PHASE", strPhase, DbType.AnsiString)
            db.AddParameter("@SHISAKU_EVENT_PHASE_NAME", strName, DbType.AnsiString)
            db.AddParameter("@CREATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
            db.AddParameter("@CREATED_DATE", strDateNow, DbType.AnsiString)
            db.AddParameter("@CREATED_TIME", strTimeNow, DbType.AnsiString)
            db.AddParameter("@UPDATED_USER_ID", LoginInfo.Now.UserId, DbType.AnsiString)
            db.AddParameter("@UPDATED_DATE", strDateNow, DbType.AnsiString)
            db.AddParameter("@UPDATED_TIME", strTimeNow, DbType.AnsiString)

            db.ExecuteNonQuery(DataSqlCommon.GetKaihatufugouInsertSql())
            db.Commit()
        End Using
    End Sub
    ''' <summary>
    ''' 登録の連番を取得する
    ''' </summary>
    ''' <returns>該当車系-符号下現在に最大の番号</returns>
    '''<param name="strShakei">キーの1－車系</param>
    '''<param name="strFugo">キーの2－符号</param>
    ''' <remarks>該当車系-符号下現在に最大の番号</remarks>
    Private Function getNo(ByVal strShakei As String, ByVal strFugo As String) As Integer
        Dim dtList As New DataTable
        Dim i As Integer = 0
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()

            db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)

            db.Fill(DataSqlCommon.GetKaihatufugouGetNoSql(), dtList)
            If dtList.Rows.Count > 0 Then
                If Not dtList.Rows(0)(0).ToString() = String.Empty Then
                    i = dtList.Rows(0)(0)
                    Return i
                Else
                    Return 0
                End If
            End If
        End Using

    End Function

    ''' <summary>
    ''' 登録前入力などのチェック
    ''' </summary>
    ''' <returns>True:チェックOK;False:チェックNG</returns>
    ''' <remarks>登録前入力などのチェック</remarks>
    Private Function insertCheck()
        Dim strShakei As String = cmbShakei.Text.ToString().Trim()
        Dim strFugo As String = cmbFugo.Text.ToString().Trim()
        Dim strPhase As String = ""
        Dim strName As String = ""
        Dim strNo As String = ""
        Dim intCount As Integer = 0
        Dim i As Integer = 0
        Dim arrList As New ArrayList
        Dim strList(49) As String
        Dim strCom As String = ""
        m_spCom = New SpreadCommon(spdParts)
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            ShisakuFormUtil.initlColor(m_spCom.GetCell("Phase", i))
            ShisakuFormUtil.initlColor(m_spCom.GetCell("Event", i))
        Next
        ShisakuFormUtil.initlColor(cmbShakei)
        ShisakuFormUtil.initlColor(cmbFugo)
        '車系ブランクチェック
        If strShakei = String.Empty Then
            ComFunc.ShowErrMsgBox(E0009)
            ShisakuFormUtil.onErro(cmbShakei)
            cmbShakei.Focus()
            Return RESULT.NG
        End If
        '車系桁数チェック
        If Not ShisakuComFunc.IsInLength(strShakei, 2) Then
            ComFunc.ShowErrMsgBox(E0022)
            ShisakuFormUtil.onErro(cmbShakei)
            cmbShakei.Focus()
            Return RESULT.NG
        End If
        '開発符号ブランクチェック
        If strFugo = String.Empty Then
            ComFunc.ShowErrMsgBox(E0010)
            ShisakuFormUtil.onErro(cmbFugo)
            cmbFugo.Focus()
            Return RESULT.NG
        End If
        '開発符号桁数チェック
        If Not ShisakuComFunc.IsInLength(strFugo, 4) Then
            ComFunc.ShowErrMsgBox(E0023)
            ShisakuFormUtil.onErro(cmbFugo)
            cmbFugo.Focus()
            Return RESULT.NG
        End If
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            strPhase = m_spCom.GetValue("Phase", i)
            strName = m_spCom.GetValue("Event", i)
            If Not strPhase = String.Empty Or Not strName = String.Empty Then
                intCount = intCount + 1
            End If
        Next
        '一覧一つもないチェック
        If intCount = 0 Then
            ComFunc.ShowErrMsgBox(E0011)
            ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", 0))
            ShisakuFormUtil.onErro(m_spCom.GetCell("Event", 0))
            spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))
            Return RESULT.NG
        End If
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            strPhase = m_spCom.GetValue("Phase", i)
            strName = m_spCom.GetValue("Event", i)
            strNo = m_spCom.GetValue("Id", i)
            If Not strPhase = String.Empty Or Not strName = String.Empty Then
                'フェーズを入力していて、イベントを入力していないチェック
                If Not strPhase = String.Empty And strName = String.Empty Then
                    ComFunc.ShowErrMsgBox(E0012)
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Event", i))
                    spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Event"))
                    Return RESULT.NG
                End If
                'フェーズを入力していてない、イベントを入力しているチェック
                If strPhase = String.Empty And Not strName = String.Empty Then
                    ComFunc.ShowErrMsgBox(E0013)
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                    spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))
                    Return RESULT.NG
                End If
                'フェーズ桁数チェック
                If Not ShisakuComFunc.IsInLength(strPhase, 1) Then
                    ComFunc.ShowErrMsgBox(E0020)
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                    spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))
                    Return RESULT.NG
                End If
                'イベント桁数チェック
                If Not ShisakuComFunc.IsInLength(strName, 20) Then
                    ComFunc.ShowErrMsgBox(E0021)
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Event", i))
                    spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Event"))
                    Return RESULT.NG
                End If
                '重複チェック
                If Not strPhase = String.Empty And Not strName = String.Empty Then
                    strCom = strPhase + "_" + strName
                    If arrList.IndexOf(strCom) = -1 Then
                        arrList.Add(strCom)
                    Else
                        ComFunc.ShowErrMsgBox(E0014)
                        ShisakuFormUtil.onErro(m_spCom.GetCell("Event", i))
                        spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Event"))
                        Return RESULT.NG
                    End If
                    If strNo = String.Empty And doubleDataExist(strShakei, strFugo, strPhase, strName) = RESULT.OK Then
                        ComFunc.ShowErrMsgBox(E0025)
                        ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                        spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))
                        Return RESULT.NG
                    End If
                End If
            End If
        Next
        Return RESULT.OK
    End Function

    ''' <summary>
    ''' 同じデータ存在チェック
    ''' </summary>
    ''' <returns>True:同じデータは存在する;False:同じデータは存在しない</returns>
    '''<param name="strShakei">キーの1－車系</param>
    '''<param name="strFugo">キーの2－符号</param>
    ''' <param name="strPhase">キーの3－フェーズ</param>
    ''' <param name="strName">キーの4－イベント</param>
    ''' <remarks>同じデータ存在チェックするかどうかチェックする(二つPCで同時に登録時エラー発生を防ぐため)</remarks>
    Private Function doubleDataExist(ByVal strShakei As String, ByVal strFugo As String, ByVal strPhase As String, ByVal strName As String) As Boolean
        Dim dtList As DataTable = New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            If Not strShakei = String.Empty Then
                db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            End If
            If Not strFugo = String.Empty Then
                db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
            End If
            If Not strPhase = String.Empty Then
                db.AddParameter("@SHISAKU_EVENT_PHASE", strPhase, DbType.AnsiString)
            End If
            If Not strName = String.Empty Then
                db.AddParameter("@SHISAKU_EVENT_PHASE_NAME", strName, DbType.AnsiString)
            End If
            db.Fill(DataSqlCommon.GetKaihatufugouDoubleCheckSql(), dtList)
            If dtList.Rows.Count > 0 Then
                Return RESULT.OK '登録済みと判断する
            Else
                Return RESULT.NG
            End If

        End Using
    End Function

    Private Sub frm6KaihatufugouMaster02_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.KeyPreview = True
            loadProcess()
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)
            isLoad = False
            changKey = 0
        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try
        cmbShakei.Focus()
    End Sub

    Private Sub loadProcess()
        'メッセージを表示する
        Label10.Text = T0003
        'コンボボックスデータソースを取得


        getcmbData()
        'スプレッドクリア
        spdParts_Sheet1.Rows.Remove(0, spdParts_Sheet1.Rows.Count)
        setSpdStyle()
    End Sub

    Private Sub getcmbData()
        Dim dtShakei As DataTable = New DataTable()
        Dim dtFugo As DataTable = New DataTable()
        Dim dtRow As DataRow
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            db.Fill(DataSqlCommon.GetKaihatufugouComboSql("Shakei"), dtShakei)
            ' 車系コンボボックスにデータセットを接続します。
            dtRow = dtShakei.NewRow
            dtRow("Shakei") = ""
            dtShakei.Rows.InsertAt(dtRow, 0)
            cmbShakei.DataSource = dtShakei
            cmbShakei.DisplayMember = "Shakei"
            cmbShakei.ValueMember = "Shakei"

            db.Fill(DataSqlCommon.GetKaihatufugouComboSql("Fugo"), dtFugo)
            ' 符号コンボボックスにデータセットを接続します。
            dtRow = dtFugo.NewRow
            dtRow("Fugo") = ""
            dtFugo.Rows.InsertAt(dtRow, 0)
            cmbFugo.DataSource = dtFugo
            cmbFugo.DisplayMember = "Fugo"
            cmbFugo.ValueMember = "Fugo"
        End Using
    End Sub

    Private Sub spdParts_TextTipFetch(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.TextTipFetchEventArgs) Handles spdParts.TextTipFetch
        Dim sheet As Spread.SheetView = spdParts.ActiveSheet
        Dim col As Spread.Column = Nothing
        col = sheet.Columns(e.Column)
        Select Case col.Tag
            Case "Phase"
                e.TipText = "半角１桁の記号を設定します。"
                e.ShowTip = True
            Case Else
                e.ShowTip = False
        End Select
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub

    Private Sub cmb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbShakei.TextChanged, cmbFugo.TextChanged
        keyChang()
        If isLoad = False Then
            Dim strShakei As String = cmbShakei.Text
            Dim strFugo As String = cmbFugo.Text
            Dim strNo As String = ""
            Dim i As Integer = 0
            m_spCom = New SpreadCommon(spdParts)
            If Not strShakei = String.Empty And Not strFugo = String.Empty Then
                searchSpdData()
            Else
                spdParts_Sheet1.Rows.Remove(0, spdParts_Sheet1.Rows.Count)
            End If
            setSpdStyle()
        End If
    End Sub

    Private Sub setSpdStyle()
        Dim i As Integer = 0
        spdParts_Sheet1.SetRowSizeable(spdParts_Sheet1.RowCount, False)
        m_spCom = New SpreadCommon(spdParts)
        m_spCom.GetColFromTag("Id").Visible = False
        ShisakuFormUtil.initlColor(cmbShakei)
        ShisakuFormUtil.initlColor(cmbFugo)
        spdParts_Sheet1.Rows.Count = 50
        spdParts.ActiveSheet.RowHeader.Columns(0).Width = 35
        m_spCom.GetColFromTag("Phase").Width = 70
        m_spCom.GetColFromTag("Event").Width = 130
        spdParts_Sheet1.Columns(1).Resizable = False
        spdParts_Sheet1.Columns(2).Resizable = False
        Dim txtTypePhase As New TextCellType()
        Dim txtTypeEvent As New TextCellType()
        txtTypePhase.CharacterSet = CharacterSet.AlphaNumeric
        txtTypePhase.CharacterCasing = CharacterCasing.Upper
        txtTypePhase.MaxLength = 1
        txtTypeEvent.CharacterSet = CharacterSet.KanjiOnlyIME
        txtTypeEvent.CharacterCasing = CharacterCasing.Upper
        txtTypeEvent.MaxLength = 10
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            m_spCom.GetCell("Phase", i).CellType = txtTypePhase
            m_spCom.GetCell("Event", i).CellType = txtTypeEvent
        Next
    End Sub

    Private Sub searchSpdData()
        Dim dtList As DataTable = New DataTable()
        Dim strShakei As String = cmbShakei.Text
        Dim strFugo As String = cmbFugo.Text
        Dim strNoRow As String = ""
        Dim i As Integer = 0
        m_spCom = New SpreadCommon(spdParts)
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            If Not strShakei = String.Empty Then
                db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            End If
            If Not strFugo = String.Empty Then
                db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
            End If
            db.Fill(DataSqlCommon.GetKaihatufugouDisplaySql(strShakei, strFugo, "Insert"), dtList)
            ' シートにデータセットを接続します。
            spdParts.ActiveSheet.DataSource = dtList
            '試作イベントマスタ登録済みデータをロックする
            For i = 0 To dtList.Rows.Count - 1
                strNoRow = m_spCom.GetValue("Id", i)
                If checkExist(strShakei, strFugo, strNoRow) = RESULT.OK Then
                    m_spCom.GetCell("Phase", i).Locked = True
                    m_spCom.GetCell("Event", i).Locked = True
                End If
            Next
        End Using
    End Sub

    ''' <summary>
    ''' 同じ車系-符号-イベントのチェック
    ''' </summary>
    ''' <returns>True:同じ車系-符号-イベントは存在する;False:同じ車系-符号-イベントは存在しない</returns>
    '''<param name="strShakei">キーの1－車系</param>
    '''<param name="strFugo">キーの2－符号</param>
    ''' <param name="strName">キーの3－イベント</param>
    ''' <remarks>同じ車系-符号-イベントは存在するかどうかのチェック</remarks>
    Private Function checkProcess(ByVal strShakei As String, ByVal strFugo As String, ByVal strName As String)
        Dim dtList As New DataTable
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()

            db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
            db.AddParameter("@SHISAKU_EVENT_PHASE_NAME", strName, DbType.AnsiString)

            db.Fill(DataSqlCommon.GetKaihatufugouCheckSql(), dtList)
            If dtList.Rows.Count > 0 Then
                '存在する
                Return RESULT.OK
            Else
                '存在しない
                Return RESULT.NG
            End If
        End Using
    End Function

    ''' <summary>
    ''' データベース上同じデータの存在チェック
    ''' </summary>
    ''' <returns>True:同じ車系-符号-イベントは存在する;False:同じ車系-符号-イベントは存在しない</returns>
    '''<param name="strShakei">キーの1－車系</param>
    '''<param name="strFugo">キーの2－符号</param>
    ''' <param name="strHYOJIJUN_NO">キーの3－連番</param>
    ''' <remarks>データベース上同じデータ存在するかどうかチェックする</remarks>
    Private Function checkExist(ByVal strShakei As String, ByVal strFugo As String, ByVal strHYOJIJUN_NO As String)
        Dim dtList As DataTable = New DataTable()
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            If Not strShakei = String.Empty Then
                db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            End If
            If Not strFugo = String.Empty Then
                db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
            End If
            If Not strHYOJIJUN_NO = String.Empty Then
                db.AddParameter("@HYOJIJUN_NO", strHYOJIJUN_NO, DbType.AnsiString)
            End If
            db.Fill(DataSqlCommon.GetIbentoCheckSql(strShakei, strFugo, strHYOJIJUN_NO), dtList)
            If dtList.Rows.Count > 0 Then
                Return RESULT.OK '登録済みと判断する
            Else
                Return RESULT.NG
            End If

        End Using
    End Function

    Private Sub keyChang()
        changKey = 1
    End Sub

    Private Sub spdParts_Change(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdParts.Change
        keyChang()
    End Sub

    Private Sub Controls_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdParts.KeyDown, cmbShakei.KeyDown, cmbFugo.KeyDown
        Dim isChanged As Boolean = ShisakuFormUtil.DelKeyDown(sender, e)
        If isChanged = True Then
            changKey = 1
        Else
            changKey = 0
        End If
    End Sub

    Private Sub spdParts_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles spdParts.LeaveCell
        If e.NewColumn = m_spCom.GetColIdxFromTag("Phase") Then
            spdParts.ImeMode = Windows.Forms.ImeMode.Disable
        ElseIf e.NewColumn = m_spCom.GetColIdxFromTag("Event") Then
            spdParts.ImeMode = Windows.Forms.ImeMode.Hiragana
        End If
    End Sub
End Class