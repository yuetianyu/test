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
Public Class frm6KaihatufugouMaster03

    Dim arrOKList As New ArrayList
    Private m_spCom As SpreadCommon
    Private changKey As Integer

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)

    End Sub

    Private Sub txtListCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frm6KaihatufugouMaster03_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.KeyPreview = True
            'スプレッドクリア
            changKey = 0
            spdParts_Sheet1.Rows.Remove(0, spdParts_Sheet1.Rows.Count)
            spdParts_Sheet1.Rows.Add(0, 50)
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)
            spdParts_Sheet1.SetRowSizeable(spdParts_Sheet1.RowCount, False)
            spdParts.ActiveSheet.RowHeader.Columns(0).Width = 35
            Dim i As Integer = 0
            Dim strShakei As String = ""
            Dim strFugo As String = ""
            Dim strNo As String = ""
            'データのdisplay
            displayData()
            m_spCom = New SpreadCommon(spdParts)
            'イベント登録済みのデータを使用不可にする。
            For i = 0 To spdParts_Sheet1.Rows.Count - 1
                strShakei = lblShakei.Text.ToString().Trim()
                strFugo = lblFugo.Text.ToString().Trim()
                strNo = m_spCom.GetValue("Id", i)
                If checkExist(strShakei, strFugo, strNo) = RESULT.OK Then
                    m_spCom.GetCell("Phase", i).Locked = True
                    m_spCom.GetCell("Event", i).Locked = True
                    m_spCom.GetCell("Shakei", i).Locked = True
                    m_spCom.GetCell("Fugo", i).Locked = True
                Else
                    m_spCom.GetCell("Phase", i).Locked = False
                    m_spCom.GetCell("Event", i).Locked = False
                    m_spCom.GetCell("Shakei", i).Locked = False
                    m_spCom.GetCell("Fugo", i).Locked = False

                End If
            Next
            'スプレッドスタイルの設定
            setspdStyle()
        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try
    End Sub

    Private Sub setspdStyle()
        Dim info As New Spread.StyleInfo()
        spdParts_Sheet1.Rows.Count = 50
        ' 列の幅を設定します。
        spdParts_Sheet1.SetColumnWidth(1, 59)
        spdParts_Sheet1.SetColumnWidth(2, 130)
        spdParts_Sheet1.SetColumnWidth(3, 76)
        spdParts_Sheet1.SetColumnWidth(4, 76)
        spdParts_Sheet1.Columns(1).Resizable = False
        spdParts_Sheet1.Columns(2).Resizable = False
        spdParts_Sheet1.Columns(3).Resizable = False
        spdParts_Sheet1.Columns(4).Resizable = False
        spdParts_Sheet1.Columns(0).Visible = False
        'comboboxカラム設定
        getComboboxSp()
    End Sub


    Private Sub getComboboxSp()
        Dim dtShakei As DataTable = New DataTable()
        Dim dtFugo As DataTable = New DataTable()
        Dim i As Integer = 0
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            db.Fill(DataSqlCommon.GetKaihatufugouComboSql("Shakei"), dtShakei)
            Dim intCountShakei = dtShakei.Rows.Count
            Dim strArrShakei(intCountShakei) As String
            ' 車系コンボボックスにデータセットを接続します。
            strArrShakei(0) = ""
            For i = 0 To intCountShakei - 1
                strArrShakei(i + 1) = dtShakei.Rows(i)(0).ToString()
            Next
            db.Fill(DataSqlCommon.GetKaihatufugouComboSql("Fugo"), dtFugo)
            ' 符号コンボボックスにデータセットを接続します。
            Dim intCountFugo = dtFugo.Rows.Count
            Dim strArrFugo(intCountFugo) As String
            strArrFugo(0) = ""
            For i = 0 To intCountFugo - 1
                strArrFugo(i + 1) = dtFugo.Rows(i)(0).ToString()
            Next
            Dim comboTypeShakei As New ComboBoxCellType()
            Dim comboTypeFugo As New ComboBoxCellType()
            Dim txtTypePhase As New TextCellType()
            Dim txtTypeEvent As New TextCellType()
            txtTypePhase.CharacterSet = CharacterSet.AlphaNumeric
            txtTypePhase.CharacterCasing = CharacterCasing.Upper
            txtTypePhase.MaxLength = 1
            txtTypeEvent.CharacterSet = CharacterSet.KanjiOnlyIME
            txtTypeEvent.CharacterCasing = CharacterCasing.Upper
            txtTypeEvent.MaxLength = 10
            comboTypeShakei.Items = strArrShakei
            comboTypeFugo.Items = strArrFugo
            comboTypeShakei.AutoSearch = AutoSearch.MultipleCharacter
            comboTypeFugo.AutoSearch = AutoSearch.MultipleCharacter
            comboTypeShakei.Editable = True
            comboTypeFugo.Editable = True
            comboTypeShakei.CharacterCasing = CharacterCasing.Upper
            comboTypeFugo.CharacterCasing = CharacterCasing.Upper
            m_spCom = New SpreadCommon(spdParts)
            For i = 0 To 49
                m_spCom.GetCell("Shakei", i).CellType = comboTypeShakei
                m_spCom.GetCell("Fugo", i).CellType = comboTypeFugo
                m_spCom.GetCell("Phase", i).CellType = txtTypePhase
                m_spCom.GetCell("Event", i).CellType = txtTypeEvent
            Next
        End Using
    End Sub

    Private Sub displayData()
        Dim dtList As DataTable = New DataTable()
        Dim strShakei As String = lblShakei.Text
        Dim strFugo As String = lblFugo.Text
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            If Not strShakei = String.Empty Then
                db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
            End If
            If Not strFugo = String.Empty Then
                db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
            End If
            db.Fill(DataSqlCommon.GetKaihatufugouDisplaySql(strShakei, strFugo, "Update"), dtList)
            ' シートにデータセットを接続します。
            spdParts.ActiveSheet.DataSource = dtList
        End Using
    End Sub

    Private Sub btnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click
        frm01Kakunin.lblKakunin.Text = T0006
        frm01Kakunin.lblKakunin2.Text = ""
        frm6Para = "btnCall"
        frm01Kakunin.ShowDialog()
        Select Case frm6ParaModori
            Case "2" '更新ボタンを押すと
                'チェックOK
                If updateCheck() = RESULT.OK Then
                    '更新する
                    updateDB()
                End If
            Case "" 'キャンセルボタンを押すと
                'Me.Close()
        End Select
    End Sub

    ''' <summary>
    ''' 更新前入力などのチェック
    ''' </summary>
    ''' <returns>True:チェックOK;False:チェックNG</returns>
    ''' <remarks>更新前入力などのチェック</remarks>
    Private Function updateCheck()
        Dim strShakei As String = lblShakei.Text.ToString().Trim()
        Dim strFugo As String = lblFugo.Text.ToString().Trim()
        Dim strPhase As String = ""
        Dim strName As String = ""
        Dim strShakeiNew As String = ""
        Dim strFugoNew As String = ""
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
            ShisakuFormUtil.initlColor(m_spCom.GetCell("Shakei", i))
            ShisakuFormUtil.initlColor(m_spCom.GetCell("Fugo", i))
        Next
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            strPhase = m_spCom.GetValue("Phase", i)
            strName = m_spCom.GetValue("Event", i)
            strShakeiNew = m_spCom.GetValue("Shakei", i)
            strFugoNew = m_spCom.GetValue("Fugo", i)
            strNo = m_spCom.GetValue("Id", i)
            If strNo = String.Empty And strPhase = String.Empty And strName = String.Empty And strShakeiNew = String.Empty _
          And strFugoNew = String.Empty Then
                intCount = intCount + 1
            End If
        Next
        '一覧一つもないチェック
        If intCount = 50 Then
            ComFunc.ShowErrMsgBox(E0011)
            ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
            ShisakuFormUtil.onErro(m_spCom.GetCell("Event", i))
            ShisakuFormUtil.onErro(m_spCom.GetCell("Shakei", i))
            ShisakuFormUtil.onErro(m_spCom.GetCell("Fugo", i))
            Return RESULT.NG
        End If
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            strPhase = m_spCom.GetValue("Phase", i)
            strName = m_spCom.GetValue("Event", i)
            strShakeiNew = m_spCom.GetValue("Shakei", i)
            strFugoNew = m_spCom.GetValue("Fugo", i)
            strNo = m_spCom.GetValue("Id", i)
            If Not strNo = String.Empty Or Not strPhase = String.Empty Or Not strName = String.Empty _
            Or Not strShakeiNew = String.Empty Or Not strFugoNew = String.Empty Then
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
                '変更先の車系が入力されていて、フェーズが入力されていない場合。
                If Not strShakeiNew = String.Empty And strPhase = String.Empty Then
                    ComFunc.ShowErrMsgBox(E0013)
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                    spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))
                    Return RESULT.NG
                End If
                '変更先の車系が入力されていて、イベントが入力されていない場合。
                If Not strShakeiNew = String.Empty And strName = String.Empty Then
                    ComFunc.ShowErrMsgBox(E0012)
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Event", i))
                    spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Event"))
                    Return RESULT.NG
                End If
                '変更先の開発符号が入力されていて、フェーズが入力されていない場合。
                If Not strFugoNew = String.Empty And strPhase = String.Empty Then
                    ComFunc.ShowErrMsgBox(E0011)
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                    spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))
                    Return RESULT.NG
                End If
                '変更先の開発符号が入力されていて、イベントが入力されていない場合。
                If Not strFugoNew = String.Empty And strName = String.Empty Then
                    ComFunc.ShowErrMsgBox(E0012)
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Event", i))
                    spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Event"))
                    Return RESULT.NG
                End If
                'フェーズ桁数チェック
                If Not strPhase = String.Empty Then
                    If Not ShisakuComFunc.IsInLength(strPhase, 1) Then
                        ComFunc.ShowErrMsgBox(E0020)
                        ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                        spdParts.Focus()
                        spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))

                        Return RESULT.NG
                    End If
                End If
                'イベント桁数チェック
                If Not strName = String.Empty Then
                    If Not ShisakuComFunc.IsInLength(strName, 20) Then
                        ComFunc.ShowErrMsgBox(E0021)
                        ShisakuFormUtil.onErro(m_spCom.GetCell("Event", i))
                        spdParts.Focus()
                        spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Event"))

                        Return RESULT.NG
                    End If
                End If
                '重複チェック
                If Not strPhase = String.Empty And Not strName = String.Empty Then
                    strCom = strPhase + "_" + strName
                    If arrList.IndexOf(strCom) = -1 Then
                        arrList.Add(strCom)
                    Else
                        ComFunc.ShowErrMsgBox(E0014)
                        ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                        spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))
                        Return RESULT.NG
                    End If
                End If
                '車系桁数チェック
                If Not ShisakuComFunc.IsInLength(strShakeiNew, 2) Then
                    ComFunc.ShowErrMsgBox(E0022)
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Shakei", i))
                    spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Shakei"))
                    Return RESULT.NG
                End If
                '開発符号桁数チェック
                If Not ShisakuComFunc.IsInLength(strFugoNew, 4) Then
                    ComFunc.ShowErrMsgBox(E0023)
                    ShisakuFormUtil.onErro(m_spCom.GetCell("Fugo", i))
                    spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Fugo"))
                    Return RESULT.NG
                End If
                '存在チェック(両方ある)
                If Not strShakeiNew = String.Empty And Not strFugoNew = String.Empty Then
                    '登録操作
                    If checkProcess(strShakeiNew, strFugoNew, strName) = RESULT.OK Then
                        ComFunc.ShowErrMsgBox(E0015)
                        ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                        spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))
                        Return RESULT.NG
                    End If
                    If doubleDataExist(strShakeiNew, strFugoNew, strPhase, strName) = RESULT.OK Then
                        ComFunc.ShowErrMsgBox(E0025)
                        ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                        spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))
                        Return RESULT.NG
                    End If
                End If
                '存在チェック（車系ない、開発符号ある）
                If strShakeiNew = String.Empty And Not strFugoNew = String.Empty Then
                    '登録操作
                    If checkProcess(strShakei, strFugoNew, strName) = RESULT.OK Then
                        ComFunc.ShowErrMsgBox(E0015)
                        ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                        spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))
                        Return RESULT.NG
                    End If
                    If doubleDataExist(strShakei, strFugoNew, strPhase, strName) = RESULT.OK Then
                        ComFunc.ShowErrMsgBox(E0025)
                        ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                        spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))
                        Return RESULT.NG
                    End If
                End If
                '存在チェック（車系ある、開発符号ない）
                If Not strShakeiNew = String.Empty And strFugoNew = String.Empty Then
                    '登録操作
                    If checkProcess(strShakeiNew, strFugo, strName) = RESULT.OK Then
                        ComFunc.ShowErrMsgBox(E0015)
                        ShisakuFormUtil.onErro(m_spCom.GetCell("Phase", i))
                        spdParts_Sheet1.SetActiveCell(i, m_spCom.GetColIdxFromTag("Phase"))
                        Return RESULT.NG
                    End If
                    If doubleDataExist(strShakeiNew, strFugo, strPhase, strName) = RESULT.OK Then
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

    Private Sub updateDB()
        Dim strShakei As String = ""
        Dim strFugo As String = ""
        Dim strShakeiNew As String = ""
        Dim strFugoNew As String = ""
        Dim strNo As String = ""
        Dim strPhase As String = ""
        Dim strName As String = ""
        Dim i As Integer = 0
        Dim arrRow As New ArrayList
        strShakei = lblShakei.Text
        strFugo = lblFugo.Text
        m_spCom = New SpreadCommon(spdParts)
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            strPhase = m_spCom.GetValue("Phase", i)
            strName = m_spCom.GetValue("Event", i)
            strShakeiNew = m_spCom.GetValue("Shakei", i)
            strFugoNew = m_spCom.GetValue("Fugo", i)
            strNo = m_spCom.GetValue("Id", i)
            '行削除処理
            If Not strNo = String.Empty And strPhase = String.Empty And strName = String.Empty _
            And strShakeiNew = String.Empty And strFugoNew = String.Empty Then
                deleteProcess(strShakei, strFugo, strNo)
            Else
                '単純な更新処理
                If Not strNo = String.Empty And Not strPhase = String.Empty And Not strName = String.Empty And _
                strShakeiNew = String.Empty And strFugoNew = String.Empty Then
                    updateProcess(strShakei, strFugo, strPhase, strName, strNo)
                End If
                '登録・削除処理
                If Not strNo = String.Empty And Not strPhase = String.Empty And _
                   (Not strShakeiNew = String.Empty Or Not strFugoNew = String.Empty) Then
                    '両方ブランクではない
                    If (Not strShakeiNew = String.Empty And Not strFugoNew = String.Empty) Then
                        strNo = getNo(strShakeiNew, strFugoNew) + 1
                        '登録処理
                        insertProcess(strShakeiNew, strFugoNew, strPhase, strName, strNo)
                        If Not strNo = String.Empty Then
                            '削除処理
                            deleteProcess(strShakei, strFugo, m_spCom.GetValue("Id", i))
                        End If
                    End If
                    '車系はブランク、開発符号はブランクではない場合
                    If (strShakeiNew = String.Empty And Not strFugoNew = String.Empty) Then
                        strNo = getNo(strShakei, strFugoNew) + 1
                        '登録処理
                        insertProcess(strShakei, strFugoNew, strPhase, strName, strNo)
                        If Not strNo = String.Empty Then
                            '削除処理
                            deleteProcess(strShakei, strFugo, m_spCom.GetValue("Id", i))
                        End If
                    End If
                    '車系はブランクではない、開発符号はブランク場合
                    If (Not strShakeiNew = String.Empty And strFugoNew = String.Empty) Then
                        strNo = getNo(strShakeiNew, strFugo) + 1
                        '登録処理
                        insertProcess(strShakeiNew, strFugo, strPhase, strName, strNo)
                        If Not strNo = String.Empty Then
                            '削除処理
                            deleteProcess(strShakei, strFugo, m_spCom.GetValue("Id", i))
                        End If
                    End If
                End If
                If strNo = String.Empty And Not strPhase = String.Empty And Not strName = String.Empty And _
                   strShakeiNew = String.Empty And strFugoNew = String.Empty Then
                    strNo = getNo(strShakei, strFugo) + 1
                    '登録処理・古い
                    insertProcess(strShakei, strFugo, strPhase, strName, strNo)
                End If
                If strNo = String.Empty And Not strPhase = String.Empty And Not strName = String.Empty And _
                  Not strShakeiNew = String.Empty And Not strFugoNew = String.Empty Then
                    strNo = getNo(strShakeiNew, strFugoNew) + 1
                    '登録処理・新しい
                    insertProcess(strShakeiNew, strFugoNew, strPhase, strName, strNo)
                End If
            End If
        Next
        ComFunc.ShowInfoMsgBox(M0002)
        Me.Close()
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

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        frm01Kakunin.lblKakunin.Text = T0001
        frm01Kakunin.lblKakunin2.Text = ""
        frm6Para = "btnDel"
        frm01Kakunin.ShowDialog()
        Select Case frm6ParaModori
            Case ""
            Case "3"
                arrOKList.Clear()
                'チェックOK
                If deleteCheck() = RESULT.OK Then
                    '削除する
                    deleteDB()
                    ComFunc.ShowInfoMsgBox(M0003)
                    Me.Close()
                End If
        End Select
    End Sub

    ''' <summary>
    ''' 削除前イベントマスタに削除待ちのデータ存在チェック
    ''' </summary>
    ''' <returns>True:削除待ちのデータは一件以上イベントマスタに存在する;False:削除待ちのデータは全てイベントマスタに存在しない</returns>
    ''' <remarks> 削除前イベントマスタに削除待ちのデータ存在するかどうかチェックする(複数可能)</remarks>
    Private Function deleteCheck()
        Dim strShakei As String = ""
        Dim strFugo As String = ""
        Dim strNoRow As String = ""
        Dim arrNGList As New ArrayList
        Dim arrList As New ArrayList
        Dim i As Integer = 0
        For i = 0 To spdParts_Sheet1.Rows.Count - 1
            strShakei = lblShakei.Text.ToString().Trim()
            strFugo = lblFugo.Text.ToString().Trim()
            strNoRow = m_spCom.GetValue("Id", i)
            If Not strNoRow = String.Empty Then
                arrList.Add(strNoRow)
                '登録済み・削除できない
                If checkExist(strShakei, strFugo, strNoRow) = RESULT.OK Then
                    arrNGList.Add(strNoRow)
                Else
                    arrOKList.Add(strNoRow)
                End If
            End If
        Next
        If arrNGList.Count = arrList.Count Then
            'すべてNGの場合
            ComFunc.ShowErrMsgBox(E0019)
            Return RESULT.NG
        End If
        Return RESULT.OK
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

    Private Sub deleteDB()
        Dim strShakei As String = ""
        Dim strFugo As String = ""
        Dim strNo As String = ""
        Dim i As Integer = 0
        strShakei = lblShakei.Text
        strFugo = lblFugo.Text
        Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
            db.Open()
            db.BeginTransaction()
            For i = 0 To arrOKList.Count - 1
                strNo = arrOKList(i).ToString().Trim()
                db.AddParameter("@SHISAKU_SHAKEI_CODE", strShakei, DbType.AnsiString)
                db.AddParameter("@SHISAKU_KAIHATSU_FUGO", strFugo, DbType.AnsiString)
                db.AddParameter("@HYOJIJUN_NO", strNo, DbType.AnsiString)

                db.ExecuteNonQuery(DataSqlCommon.GetKaihatufugouDeleteSql(strShakei, strFugo, strNo))
            Next
            db.Commit()
        End Using
    End Sub

    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        If changKey = 0 Then
            Me.Close()
        Else
            frm01Kakunin.lblKakunin.Text = T0005
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
        frm01Kakunin.lblKakunin.Text = T0005
        frm01Kakunin.lblKakunin2.Text = ""
        frm6Para = "btnEND"
        frm01Kakunin.ShowDialog()
        Select Case frm6ParaModori
            Case "E"
                Application.Exit()
        End Select
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub

    Private Sub keyChang()
        changKey = 1
    End Sub

    Private Sub spdParts_Change(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdParts.Change
        keyChang()
    End Sub

    Private Sub spdParts_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdParts.KeyDown
        Dim isChanged = ShisakuFormUtil.DelKeyDown(sender, e)
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
        ElseIf e.NewColumn = m_spCom.GetColIdxFromTag("Shakei") Then
            spdParts.ImeMode = Windows.Forms.ImeMode.Disable
        ElseIf e.NewColumn = m_spCom.GetColIdxFromTag("Fugo") Then
            spdParts.ImeMode = Windows.Forms.ImeMode.Disable
        End If
    End Sub
End Class