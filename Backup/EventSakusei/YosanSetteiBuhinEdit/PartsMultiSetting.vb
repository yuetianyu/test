Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util.LabelValue
Imports FarPoint.Win.Spread
Imports ShisakuCommon
Imports FarPoint.Win.Spread.CellType
Imports System.Text
Imports Microsoft.VisualBasic.FileIO

Namespace YosanSetteiBuhinEdit

    ''' <summary>
    ''' 部品費マルチ設定
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PartsMultiSetting

#Region "定数"

        ''' <summary>標準設定表ファイル</summary>
        Private Const PARTS_COST_MULTI_SETTING_FILE As String = "PartsCostMultiSetting.csv"

        ''' <summary>割付予算根拠係数1(LabelValue用)</summary>
        Private Const LABEL_YOSAN_KONKYO_KEISU_1 As String = "部品費割付予算根拠：係数1"

        ''' <summary>割付予算部品費(LabelValue用)</summary>
        Private Const LABEL_YOSAN_WARITUKE_BUHIN_HI As String = "割付予算：部品費"

        ''' <summary>割付予算係数2(LabelValue用)</summary>
        Private Const LABEL_YOSAN_WARITUKE_KEISU_2 As String = "割付予算：係数2"

        ''' <summary>購入希望単価部品費(LabelValue用)</summary>
        Private Const LABEL_YOSAN_KOUNYU_KIBOU_BUHIN_HI As String = "購入希望単価：部品費"

#End Region

#Region "メンバ変数"

        ''' <summary>項目名リスト</summary>
        Private _ItemNameList As List(Of LabelValueVo)
        Private sheet As SheetView

        ''' <summary>
        ''' 直前に選んだボタン(0:未選択 1:が 2:に 3:を 4:の時に 5:で)
        ''' </summary>
        Private beforeBtn As Integer

        ''' <summary>
        ''' 今回選んだボタン(0:未選択 1:が 2:に 3:を 4:の時に 5:で)
        ''' </summary>
        Private nowBtn As Integer

#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal sheet As SheetView)

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            Me.sheet = sheet

            '設定する項目列コンボボックス設定'
            FormUtil.BindLabelValuesToComboBox(Me.cmbSettingColumn, GetLabelValues_CmbSettingColumn(), True)

            '項目名コンボボックス設定'
            FormUtil.BindLabelValuesToComboBox(Me.cmbItemName, GetLabelValues_CmbItemName(), True)

            '項目内容コンボボックス設定'
            'FormUtil.BindLabelValuesToComboBox(Me.cmbItemContents, GetLabelValues_CmbItemName(), False)

            '指示内容コンボボックス設定'
            FormUtil.BindLabelValuesToComboBox(Me.cmbDirections, GetLabelValues_CmbDirections(), True)

            'ボタン状態設定'
            InitButton()
            InitContorl()

        End Sub

#End Region

#Region "イベント"

#Region "接続語ボタンイベント"

        ''' <summary>
        ''' がボタン押下イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnGa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGa.Click
            InitButton()
            nowBtn = 1
            'が'
            btnGa.Enabled = True
            btnGa.BackColor = ChoiceColor()

            InitContorl()

        End Sub

        ''' <summary>
        ''' の時にボタン押下イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnNotokini_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotokini.Click
            InitButton()
            nowBtn = 4
            'の時に'
            btnNotokini.BackColor = ChoiceColor()
            btnNotokini.Enabled = True
            cmbItemName.SelectedIndex = -1
            tbInput.Text = ""
            'cmbItemContents.SelectedIndex = -1

            InitContorl()

        End Sub

        ''' <summary>
        ''' でボタン押下イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnDe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDe.Click
            InitButton()
            nowBtn = 5
            'で'
            btnDe.BackColor = ChoiceColor()
            btnDe.Enabled = True

            cmbItemName.SelectedIndex = -1
            tbInput.Text = ""
            'cmbItemContents.SelectedIndex = -1

            InitContorl()
        End Sub

        ''' <summary>
        ''' にボタン押下イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnNi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNi.Click
            InitButton()
            nowBtn = 2
            'に'
            btnNi.BackColor = ChoiceColor()
            btnNi.Enabled = True

            InitContorl()

        End Sub

        ''' <summary>
        ''' をボタン押下イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnWo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWo.Click
            InitButton()
            nowBtn = 3
            'を'
            btnWo.BackColor = ChoiceColor()
            btnWo.Enabled = True

            InitContorl()
        End Sub

#End Region

#Region "項目名コンボボックス変更イベント"

        ''' <summary>
        ''' 項目名コンボボックス変更イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbItemName_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbItemName.SelectedValueChanged
            '項目内容コンボボックス設定
            FormUtil.BindLabelValuesToComboBox(Me.cmbItemContents, GetLabelValues_CmbItemContents(cmbItemName.SelectedValue), True)

        End Sub
#End Region

#Region "追加ボタンイベント"

        ''' <summary>
        ''' 追加ボタン押下イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            'エラーチェック'
            If AddErrorCheck() Then
                '今回選択したボタンを前回選択したボタンに入れ込む'
                '問題なければテキストボックスに投入'
                SetText()

                beforeBtn = nowBtn
                nowBtn = 0
                'ボタン色設定'
                InitButton()
                InitContorl()

                '項目名、項目内容、手入力をクリア'
                cmbItemName.SelectedIndex = -1
                tbInput.Text = ""
                cmbItemContents.DataSource = Nothing
                cmbItemContents.Text = ""
                'cmbSettingColumn.SelectedIndex = -1
                cmbDirections.SelectedIndex = -1

                '前回が「を」の時、設定する項目列を活性化'
                If beforeBtn = 3 Then

                    cmbSettingColumn.Enabled = True

                    '実行用の文章'
                    Dim str As String = tbOutPut.Text

                    'スペースで区切って内容取得'
                    Dim values As String() = str.Split(" ")
                    Dim list As New List(Of String)
                    For Each v As String In values
                        list.Add(v)
                    Next

                    For index As Integer = 0 To list.Count - 1
                        If StringUtil.Equals(list(index), "に") Then
                            If StringUtil.Equals(list(index - 1), LABEL_YOSAN_KONKYO_KEISU_1) _
                            OrElse StringUtil.Equals(list(index - 1), LABEL_YOSAN_WARITUKE_BUHIN_HI) _
                            OrElse StringUtil.Equals(list(index - 1), LABEL_YOSAN_WARITUKE_KEISU_2) _
                            OrElse StringUtil.Equals(list(index - 1), LABEL_YOSAN_KOUNYU_KIBOU_BUHIN_HI) Then
                                cmbSettingColumn.Enabled = True
                                Exit For
                            End If
                        End If
                    Next

                End If

            End If
        End Sub

#End Region

#Region "標準ボタンイベント"

        ''' <summary>
        ''' 標準ボタン押下イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefault.Click
            '標準設定表ファイルの存在チェック'
            If Not System.IO.File.Exists(PARTS_COST_MULTI_SETTING_FILE) Then
                MsgBox("標準設定表が存在しません。", MsgBoxStyle.OkOnly, "エラー")
                Exit Sub
            End If

            Dim sr As New System.IO.StreamReader(PARTS_COST_MULTI_SETTING_FILE, System.Text.Encoding.GetEncoding("Shift-JIS"))
            Dim list As New List(Of String())

            Dim flg As Boolean = False

            While sr.Peek() > -1
                flg = True
                Dim str As String = sr.ReadLine
                If StringUtil.IsNotEmpty(str) Then
                    Dim str2 As String() = {str.Substring(0, str.IndexOf("：")), str.Substring(str.IndexOf("：") + 1, str.Length - str.IndexOf("：") - 1)}
                    list.Add(str2)
                Else
                    'MsgBox("標準設定表が存在しません。", MsgBoxStyle.OkOnly, "エラー")
                    'Exit Sub
                End If
            End While

            If Not flg Then
                MsgBox("標準設定表が存在しません。", MsgBoxStyle.OkOnly, "エラー")
                Exit Sub
            End If

            Dim frm As New frmStandardSetting(list)

            Dim value As String = ""
            If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                value = frm.Value
            Else
                Exit Sub
            End If
            tbOutPut.Text = value

            '状態を実行可能状態にする'
            beforeBtn = 3
            nowBtn = 0
            InitButton()
            InitContorl()
            cmbSettingColumn.Enabled = True
        End Sub


#End Region

#Region "実行ボタンイベント"

        ''' <summary>
        ''' 実行ボタン押下イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnExecution_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExecution.Click

            If ExecutionErrorCheck() Then
                '標準設定表へ保存() '
                '標準設定名：②項目名[タブ]が[タブ]③項目内容[タブ]の時に[タブ]③項目名[タブ]を[タブ]④指示内容'

                If MsgBox("標準設定表へ保存しますか？", MsgBoxStyle.YesNo, "確認") = MsgBoxResult.Yes Then
                    '標準設定名入力用ダイアログ'
                    Dim dlg As New frmStandardSettingNameDialog
                    Dim settingName As String = ""
                    If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                        settingName = dlg.StandardSettingName
                        SetPartMultiSettingCSV(settingName)
                    End If
                End If

                SetValues()

                Me.Close()
            End If

        End Sub

#End Region

#Region "キャンセルボタンイベント"

        ''' <summary>
        ''' キャンセルボタン押下イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
            cmbSettingColumn.SelectedIndex = -1
            cmbItemName.SelectedIndex = -1
            'cmbItemContents.Items.Clear()
            cmbItemContents.DataSource = Nothing
            cmbItemContents.Text = ""
            cmbDirections.SelectedIndex = -1
            tbInput.Text = ""
            tbOutPut.Text = ""
            rbAllCell.Checked = False
            rbBlackCellOnly.Checked = False

            beforeBtn = 0
            InitButton()

        End Sub

#End Region

#Region "戻るボタンイベント"

        ''' <summary>
        ''' 戻るボタン押下イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Me.Close()
        End Sub

#End Region

#End Region

#Region "エラーチェック"

        ''' <summary>
        ''' 追加ボタンのエラーチェック
        ''' </summary>
        ''' <returns>エラーあったらfalse</returns>
        ''' <remarks></remarks>
        Private Function AddErrorCheck() As Boolean
            InitErrColor()
            '「が」「に」「を」「の時に」「で」が未選択'
            If nowBtn = 0 Then
                'どれ選択していいかわからなくなるので色は変えない。'
                MsgBox("条件ボタンをクリックしてください。", MsgBoxStyle.OkOnly, "エラー")
                Return False
            End If


            '「が」が選択状態'
            If nowBtn = 1 Then
                '項目名未入力'
                If StringUtil.IsEmpty(cmbItemName.SelectedValue) Then
                    ErrMsgBox("項目名を入力してください。", cmbItemName)
                    Return False
                End If

                If StringUtil.IsEmpty(cmbItemContents.SelectedValue) _
                AndAlso StringUtil.IsEmpty(tbInput.Text) Then
                    '項目内容と手入力が未入力'
                    tbInput.BackColor = Color.Red
                    ErrMsgBox("手入力または項目内容を選択してください。", cmbItemContents)
                    Return False
                ElseIf StringUtil.IsNotEmpty(cmbItemContents.SelectedValue) _
                AndAlso StringUtil.IsNotEmpty(tbInput.Text) Then
                    '項目内容と手入力が入力'
                    tbInput.BackColor = Color.Red
                    ErrMsgBox("手入力または項目内容を選択してください。", cmbItemContents)
                    Return False
                End If
            End If

            If nowBtn = 2 Then
                '数値じゃない'

                If StringUtil.IsNotEmpty(cmbItemName.SelectedValue) Then
                    If Not TypeOf sheet.Columns(cmbItemName.SelectedValue.ToString).CellType Is CurrencyCellType _
                    AndAlso Not TypeOf sheet.Columns(cmbItemName.SelectedValue.ToString).CellType Is NumberCellType Then
                        ErrMsgBox("項目名には数値列の項目名を選択してください。", cmbItemName)
                        Return False
                    End If
                End If

                If StringUtil.IsEmpty(cmbSettingColumn.SelectedValue) _
                AndAlso StringUtil.IsEmpty(cmbItemName.SelectedValue) Then
                    cmbSettingColumn.BackColor = Color.Red
                    ErrMsgBox("設定する項目列または項目名を選択してください。", cmbItemName)
                    Return False
                ElseIf StringUtil.IsNotEmpty(cmbSettingColumn.SelectedValue) _
                AndAlso StringUtil.IsNotEmpty(cmbItemName.SelectedValue) Then
                    cmbSettingColumn.BackColor = Color.Red
                    ErrMsgBox("設定する項目列または項目名を選択してください。", cmbItemName)
                    Return False
                End If


            End If

            '「を」ボタン選択状態'
            If nowBtn = 3 Then
                If StringUtil.IsEmpty(cmbItemName.SelectedValue) _
                AndAlso StringUtil.IsEmpty(tbInput.Text) Then
                    '項目名と手入力が未入力'
                    tbInput.BackColor = Color.Red
                    ErrMsgBox("手入力または項目名を選択してください。", cmbItemName)
                    Return False
                ElseIf StringUtil.IsNotEmpty(cmbItemName.SelectedValue) _
                AndAlso StringUtil.IsNotEmpty(tbInput.Text) Then
                    tbInput.BackColor = Color.Red
                    '項目名と手入力が入力'
                    ErrMsgBox("手入力または項目名を選択してください。", cmbItemName)
                    Return False
                End If

                '数値じゃない'
                If StringUtil.IsNotEmpty(cmbItemName.SelectedValue) Then
                    If Not TypeOf sheet.Columns(cmbItemName.SelectedValue.ToString).CellType Is CurrencyCellType _
                    AndAlso Not TypeOf sheet.Columns(cmbItemName.SelectedValue.ToString).CellType Is NumberCellType Then
                        ErrMsgBox("項目名には数値列の項目名を選択してください。", cmbItemName)
                        Return False

                    End If
                End If

                If StringUtil.IsNotEmpty(tbInput.Text) Then
                    If Not IsNumeric(tbInput.Text) Then
                        '手入力が数値じゃない'
                        ErrMsgBox("手入力欄には数値を入力してください。", tbInput)
                        Return False
                    End If
                End If

                '指示内容が未入力'
                If StringUtil.IsEmpty(cmbDirections.SelectedValue) Then
                    ErrMsgBox("指示内容を選択してください。", cmbDirections)
                    Return False
                End If

                '今回選択が「を」、前回選択が「に」以外、指示内容が「かける」以外'
                If beforeBtn <> 2 Then
                    'If StringUtil.Equals(cmbDirections.SelectedValue, "1") Then
                    '    ErrMsgBox("指示内容に対する条件が不足しています。", cmbDirections)
                    '    Return False
                    'End If
                Else
                    '今回選択が「を」、前回選択が「に」、指示内容が「入力する」'
                    '～に～を入力するはあり'
                    'でも「に」の対象が項目名はダメ'
                    If StringUtil.Equals(cmbDirections.SelectedValue, "0") Then
                        Dim str As String() = tbOutPut.Text.Split(" ")

                        For index As Integer = 0 To str.Length - 1
                            If StringUtil.Equals(str(index), "に") Then
                                For Each vo As LabelValueVo In _ItemNameList
                                    If StringUtil.Equals(vo.Label, str(index - 1)) Then

                                        ErrMsgBox("指示内容には「かける」を選択してください。", cmbDirections)
                                        Return False

                                    End If
                                Next
                            End If

                        Next

                    End If

                End If
            End If

            Return True
        End Function

        ''' <summary>
        ''' 実行ボタンのエラーチェック
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ExecutionErrorCheck() As Boolean
            '設定する項目列'
            If cmbSettingColumn.Enabled Then
                cmbSettingColumn.BackColor = Color.White
            End If

            '全てのセル'
            rbAllCell.BackColor = Color.Empty
            '空白セル'
            rbBlackCellOnly.BackColor = Color.Empty
            InitErrColor()

            'エラーチェック'
            'If StringUtil.IsEmpty(cmbSettingColumn.SelectedValue) Then
            '    ErrMsgBox("設定する項目列を選択してください。", cmbSettingColumn)
            '    Return False
            'End If

            If Not rbAllCell.Checked _
            AndAlso Not rbBlackCellOnly.Checked Then
                rbAllCell.BackColor = Color.Red
                ErrMsgBox("設定範囲をチェックしてください。", rbBlackCellOnly)
                Return False
            End If

            Dim str As String = tbOutPut.Text
            '空'
            'If StringUtil.IsEmpty(str) Then
            '    ErrMsgBox("実行できません。", tbOutPut)
            '    Return False
            'End If

            '「を」がない'
            If str.IndexOf("を") = -1 Then
                ErrMsgBox("「を」が無い。", tbOutPut)
                Return False
            End If

            '最後が「が」'
            If StringUtil.Equals(str.Substring(str.Length - 1, 1), "が") Then
                ErrMsgBox("実行できません。", tbOutPut)
                Return False
            End If

            '文章内に設定する項目列が存在しない'
            Dim stringResult As String() = Split(str, " ")
            Dim flag As Boolean = False

            For i As Integer = 0 To stringResult.Length - 1
                If StringUtil.Equals(stringResult(i), LABEL_YOSAN_KONKYO_KEISU_1) Then
                    flag = True
                ElseIf StringUtil.Equals(stringResult(i), LABEL_YOSAN_WARITUKE_BUHIN_HI) Then
                    flag = True
                ElseIf StringUtil.Equals(stringResult(i), LABEL_YOSAN_WARITUKE_KEISU_2) Then
                    flag = True
                ElseIf StringUtil.Equals(stringResult(i), LABEL_YOSAN_KOUNYU_KIBOU_BUHIN_HI) Then
                    flag = True
                End If
            Next

            If Not flag Then
                If StringUtil.IsEmpty(cmbSettingColumn.SelectedValue) Then
                    ErrMsgBox("設定する項目列を選択してください。", cmbSettingColumn)
                    Return False
                End If
            End If

            '「を」の直前が項目名'
            'Dim stringResult As String() = Split(str, " ")

            'For i As Integer = 0 To stringResult.Length - 1
            '    If StringUtil.Equals(stringResult(i), "を") Then
            '        For Each vo As LabelValueVo In _ItemNameList
            '            If StringUtil.Equals(vo.Label, stringResult(i - 1)) Then
            '                ErrMsgBox("「を」の直前が項目名", tbOutPut)
            '                Return False
            '            End If
            '        Next

            '    End If
            'Next

            Return True
        End Function

        ''' <summary>
        ''' エラーメッセージ
        ''' </summary>
        ''' <param name="text"></param>
        ''' <param name="ctrl"></param>
        ''' <remarks></remarks>
        Private Sub ErrMsgBox(ByVal text As String, ByVal ctrl As System.Windows.Forms.Control)
            ctrl.BackColor = Color.Red
            MsgBox(text, MsgBoxStyle.OkOnly, "エラー")
        End Sub

        ''' <summary>
        ''' エラー色を戻す
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitErrColor()
            Select Case nowBtn
                Case 0

                Case 1
                    'が'
                    '設定する項目列'
                    cmbSettingColumn.BackColor = Color.White
                    '項目名'
                    cmbItemName.BackColor = Color.White
                    '項目内容'
                    cmbItemContents.BackColor = Color.White
                    '指示内容'
                    cmbDirections.BackColor = Color.Empty
                    '手入力'
                    tbInput.BackColor = Color.White
                    '設定テキストボックス'
                    tbOutPut.BackColor = Color.White
                    '全てのセル'
                    rbAllCell.BackColor = Color.Empty
                    '空白セル'
                    rbBlackCellOnly.BackColor = Color.Empty
                Case 2
                    'に'
                    '設定する項目列'
                    cmbSettingColumn.BackColor = Color.White
                    '項目名'
                    cmbItemName.BackColor = Color.White
                    '項目内容'
                    cmbItemContents.BackColor = Color.Empty
                    '指示内容'
                    cmbDirections.BackColor = Color.Empty
                    '手入力'
                    tbInput.BackColor = Color.Empty
                    '設定テキストボックス'
                    tbOutPut.BackColor = Color.White
                    '全てのセル'
                    rbAllCell.BackColor = Color.Empty
                    '空白セル'
                    rbBlackCellOnly.BackColor = Color.Empty
                Case 3
                    'を'
                    '設定する項目列'
                    cmbSettingColumn.BackColor = Color.White
                    '項目名'
                    cmbItemName.BackColor = Color.White
                    '項目内容'
                    cmbItemContents.BackColor = Color.Empty
                    '指示内容'
                    cmbDirections.BackColor = Color.White
                    '手入力'
                    tbInput.BackColor = Color.White
                    '設定テキストボックス'
                    tbOutPut.BackColor = Color.White
                    '全てのセル'
                    rbAllCell.BackColor = Color.Empty
                    '空白セル'
                    rbBlackCellOnly.BackColor = Color.Empty
                Case 4
                    'の時に'
                    '設定する項目列'
                    cmbSettingColumn.BackColor = Color.White
                    '項目名'
                    cmbItemName.BackColor = Color.Empty
                    '項目内容'
                    cmbItemContents.BackColor = Color.Empty
                    '指示内容'
                    cmbDirections.BackColor = Color.Empty
                    '手入力'
                    tbInput.BackColor = Color.Empty
                    '設定テキストボックス'
                    tbOutPut.BackColor = Color.White
                    '全てのセル'
                    rbAllCell.BackColor = Color.Empty
                    '空白セル'
                    rbBlackCellOnly.BackColor = Color.Empty
                Case 5
                    'で'
                    '設定する項目列'
                    cmbSettingColumn.BackColor = Color.White
                    '項目名'
                    cmbItemName.BackColor = Color.Empty
                    '項目内容'
                    cmbItemContents.BackColor = Color.Empty
                    '指示内容'
                    cmbDirections.BackColor = Color.Empty
                    '手入力'
                    tbInput.BackColor = Color.Empty
                    '設定テキストボックス'
                    tbOutPut.BackColor = Color.White
                    '全てのセル'
                    rbAllCell.BackColor = Color.Empty
                    '空白セル'
                    rbBlackCellOnly.BackColor = Color.Empty
            End Select
        End Sub

#End Region

#Region "ひとつ前のボタン状態で今回選択できるボタンの状態を設定する。"

        ''' <summary>
        ''' ボタン設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitButton()
            Select Case beforeBtn
                Case 0
                    '初期'
                    '「が」固定'
                    nowBtn = 1

                    btnGa.Enabled = True
                    btnGa.BackColor = ChoiceColor()

                    btnNi.Enabled = False
                    btnNi.BackColor = Color.Empty

                    btnWo.Enabled = False
                    btnWo.BackColor = Color.Empty

                    btnNotokini.Enabled = False
                    btnNotokini.BackColor = Color.Empty

                    btnDe.Enabled = False
                    btnDe.BackColor = Color.Empty

                    btnAdd.Enabled = True
                    btnExecution.Enabled = False

                Case 1
                    'が'
                    '「の時に」「で」が選択できる'
                    nowBtn = 0

                    btnGa.Enabled = False
                    btnGa.BackColor = Color.Empty

                    btnNi.Enabled = False
                    btnNi.BackColor = Color.Empty

                    btnWo.Enabled = False
                    btnWo.BackColor = Color.Empty

                    btnNotokini.Enabled = True
                    btnNotokini.BackColor = DefaultColor()

                    btnDe.Enabled = True
                    btnDe.BackColor = DefaultColor()

                    btnAdd.Enabled = True
                    btnExecution.Enabled = False

                Case 2
                    'に'
                    '「を」固定'
                    nowBtn = 3

                    btnGa.Enabled = False
                    btnGa.BackColor = Color.Empty
                    btnNi.Enabled = False
                    btnNi.BackColor = Color.Empty
                    btnWo.Enabled = True
                    btnWo.BackColor = ChoiceColor()
                    btnNotokini.Enabled = False
                    btnNotokini.BackColor = Color.Empty
                    btnDe.Enabled = False
                    btnDe.BackColor = Color.Empty

                    btnAdd.Enabled = True
                    btnExecution.Enabled = False

                Case 3
                    'を'
                    'ボタンは選択できない'
                    nowBtn = 0
                    btnGa.Enabled = False
                    btnGa.BackColor = Color.Empty
                    btnNi.Enabled = False
                    btnNi.BackColor = Color.Empty
                    btnWo.Enabled = False
                    btnWo.BackColor = Color.Empty
                    btnNotokini.Enabled = False
                    btnNotokini.BackColor = Color.Empty
                    btnDe.Enabled = False
                    btnDe.BackColor = Color.Empty

                    btnAdd.Enabled = False
                    btnExecution.Enabled = True

                Case 4
                    'の時に'
                    '「に」「を」が選択できる'
                    nowBtn = 0
                    btnGa.Enabled = False
                    btnGa.BackColor = Color.Empty
                    btnNi.Enabled = True
                    btnNi.BackColor = DefaultColor()
                    btnWo.Enabled = True
                    btnWo.BackColor = DefaultColor()
                    btnNotokini.Enabled = False
                    btnNotokini.BackColor = Color.Empty
                    btnDe.Enabled = False
                    btnDe.BackColor = Color.Empty

                    btnAdd.Enabled = True
                    btnExecution.Enabled = False

                Case 5
                    'で'
                    '「が」固定'
                    nowBtn = 1
                    btnGa.Enabled = True
                    btnGa.BackColor = ChoiceColor()
                    btnNi.Enabled = False
                    btnNi.BackColor = Color.Empty
                    btnWo.Enabled = False
                    btnWo.BackColor = Color.Empty
                    btnNotokini.Enabled = False
                    btnNotokini.BackColor = Color.Empty
                    btnDe.Enabled = False
                    btnDe.BackColor = Color.Empty

                    btnAdd.Enabled = True
                    btnExecution.Enabled = False

            End Select
        End Sub

#End Region

#Region "現在のボタン状態で今回選択できるコントロールの状態を設定する。"

        ''' <summary>
        ''' コントロール設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitContorl()

            Select Case nowBtn
                Case 0
                    '初期'
                    cmbSettingColumn.Enabled = True
                    cmbItemName.Enabled = False
                    cmbItemContents.Enabled = False
                    cmbDirections.Enabled = False
                    tbInput.Enabled = False
                    tbInput.BackColor = Color.Empty
                Case 1
                    'が'
                    cmbSettingColumn.Enabled = True
                    cmbItemName.Enabled = True
                    cmbItemContents.Enabled = True
                    cmbDirections.Enabled = False
                    tbInput.Enabled = True
                    tbInput.BackColor = Color.White

                Case 2
                    'に'
                    cmbSettingColumn.Enabled = True
                    cmbItemName.Enabled = True
                    cmbItemContents.Enabled = False
                    cmbDirections.Enabled = False
                    tbInput.Enabled = False
                    tbInput.BackColor = Color.Empty

                    cmbItemName.SelectedIndex = -1
                    cmbDirections.SelectedIndex = -1
                    tbInput.Text = ""

                Case 3
                    'を'
                    cmbSettingColumn.Enabled = True
                    cmbItemName.Enabled = True
                    cmbItemContents.Enabled = False
                    cmbDirections.Enabled = True
                    tbInput.Enabled = True
                    tbInput.BackColor = Color.White

                    cmbItemName.SelectedIndex = -1
                Case 4
                    'の時に'

                    cmbSettingColumn.Enabled = True
                    cmbItemName.Enabled = False
                    cmbItemContents.Enabled = False
                    cmbDirections.Enabled = False
                    tbInput.Enabled = False
                    tbInput.BackColor = Color.Empty

                Case 5
                    'で'
                    cmbSettingColumn.Enabled = True
                    cmbItemName.Enabled = False
                    cmbItemContents.Enabled = False
                    cmbDirections.Enabled = False
                    tbInput.Enabled = False
                    tbInput.BackColor = Color.Empty
            End Select
        End Sub

#End Region

#Region "現在状態をテキストに設定する。"

        ''' <summary>
        ''' 現在状態をテキストに設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetText()
            '現状を保管しておく。'
            Dim strBack As String = tbOutPut.Text
            Dim sb As New StringBuilder

            If nowBtn = 1 Then
                ' 項目名 が 項目内容or手入力'
                sb.Append(" ")
                sb.Append(cmbItemName.SelectedItem.label)
                sb.Append(" ")
                sb.Append("が")
                sb.Append(" ")
                If StringUtil.IsEmpty(cmbItemContents.SelectedValue) Then
                    sb.Append(tbInput.Text)
                Else
                    sb.Append(cmbItemContents.SelectedValue)
                End If
            End If

            If nowBtn = 2 Then
                ' 項目名 に'
                sb.Append(" ")
                If cmbSettingColumn.SelectedItem Is Nothing Then
                    sb.Append(cmbItemName.SelectedItem.Label)
                Else
                    If StringUtil.IsEmpty(cmbSettingColumn.SelectedItem.Label) Then
                        sb.Append(cmbItemName.SelectedItem.Label)
                    Else
                        sb.Append(cmbSettingColumn.SelectedItem.Label)
                    End If
                End If



                sb.Append(" ")
                sb.Append("に")
            End If

            If nowBtn = 3 Then
                ' 項目名or手入力 を指示内容'
                sb.Append(" ")
                If StringUtil.IsEmpty(cmbItemName.SelectedValue) Then
                    sb.Append(tbInput.Text)
                Else
                    sb.Append(cmbItemName.SelectedItem.label)
                End If
                sb.Append(" ")
                sb.Append("を")
                sb.Append(" ")
                sb.Append(cmbDirections.SelectedItem.label)
            End If

            If nowBtn = 4 Then
                ' の時に'
                sb.Append(" ")
                sb.Append("の時に")
            End If

            If nowBtn = 5 Then
                ' で'
                sb.Append(" ")
                sb.Append("で")
            End If

            tbOutPut.Text = strBack & sb.ToString
        End Sub

#End Region

#Region "実行本体"

        ''' <summary>
        ''' 実行用の関数を作成してシートに書き込む
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetValues()

            '実行用の文章'
            Dim str As String = tbOutPut.Text

            'スペースで区切って内容取得'
            Dim values As String() = str.Split(" ")
            Dim list As New List(Of String)
            For Each v As String In values
                list.Add(v)
            Next

            Dim tag As String = ""
            Dim tagFlag As Boolean = False  '設定する項目列指定のフラグ
            If StringUtil.IsNotEmpty(cmbSettingColumn.SelectedValue) Then
                tag = cmbSettingColumn.SelectedValue
                tagFlag = True
            End If

            'が：「=」'
            'に：'
            'を：'
            'の時に：'
            'で：「And」'

            For rowIndex As Integer = 3 To sheet.RowCount - 1
                '部品番号無いのに適用しても仕方ない'
                If StringUtil.IsEmpty(sheet.Cells(rowIndex, sheet.Columns(NmSpdTagBase.TAG_YOSAN_BUHIN_NO).Index).Value) Then
                    Continue For
                End If

                '行ごとに設定'
                Dim originalValue As String = "0" '元の値

                'A=0のチェック'
                If Not IsIfResult(rowIndex, list) Then
                    Continue For
                End If


                'この時点でタグが見つからない場合、文章中から探索する'
                Dim originalTag As String = ""
                If StringUtil.IsEmpty(tag) Then
                    '設定タグを作成'
                    For index As Integer = 0 To list.Count - 1
                        If StringUtil.Equals(list(index), "に") Then
                            If StringUtil.Equals(list(index - 1), LABEL_YOSAN_KONKYO_KEISU_1) Then
                                tag = NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1
                            ElseIf StringUtil.Equals(list(index - 1), LABEL_YOSAN_WARITUKE_BUHIN_HI) Then
                                tag = NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI
                            ElseIf StringUtil.Equals(list(index - 1), LABEL_YOSAN_WARITUKE_KEISU_2) Then
                                tag = NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2
                            ElseIf StringUtil.Equals(list(index - 1), LABEL_YOSAN_KOUNYU_KIBOU_BUHIN_HI) Then
                                tag = NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI
                            End If
                            Exit For
                        End If
                    Next
                    '元の値は文章中の設定する項目列になる'
                    originalValue = sheet.Cells(rowIndex, sheet.Columns(tag).Index).Value
                    tagFlag = True
                Else
                    '設定する項目列がコンボボックス側で設定されている場合'
                    '元の値は文章中の項目名から算出'

                    If tagFlag Then
                        originalValue = sheet.Cells(rowIndex, sheet.Columns(tag).Index).Value
                    Else
                        For index As Integer = 0 To list.Count - 1
                            If StringUtil.Equals(list(index), "に") Then
                                For Each vo As LabelValueVo In _ItemNameList
                                    If StringUtil.Equals(vo.Label, list(index - 1)) Then
                                        originalTag = vo.Value
                                        Exit For
                                    End If
                                Next
                                Exit For
                            End If
                        Next
                        originalValue = sheet.Cells(rowIndex, sheet.Columns(originalTag).Index).Value
                    End If

                End If

                '空白セルのみ'
                If rbBlackCellOnly.Checked Then
                    '空白じゃないなら何もしない。'
                    If StringUtil.IsNotEmpty(sheet.Cells(rowIndex, sheet.Columns(tag).Index).Value) Then
                        Continue For
                    End If
                End If

                'そもそも元の値が空'
                If StringUtil.IsEmpty(originalValue) Then
                    originalValue = "0"
                End If

                '入力もしくは乗算用の値を作成'
                Dim value As String = GetValue(rowIndex, list)

                '設定する値が空だと計算できない。'
                If StringUtil.IsEmpty(value) Then
                    Continue For
                End If

                '一番下が「入力する」か「かける」で判定'
                If StringUtil.Equals(list(list.Count - 1), "入力する") Then
                    sheet.Cells(rowIndex, sheet.Columns(tag).Index).Value = value
                    '編集されたセルは太文字・青文字にする
                    sheet.Cells(rowIndex, sheet.Columns(tag).Index).ForeColor = Color.Blue

                    sheet.Cells(rowIndex, sheet.Columns(tag).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                ElseIf StringUtil.Equals(list(list.Count - 1), "かける") Then
                    sheet.Cells(rowIndex, sheet.Columns(tag).Index).Value = Double.Parse(originalValue) * Double.Parse(value)
                    '編集されたセルは太文字・青文字にする
                    sheet.Cells(rowIndex, sheet.Columns(tag).Index).ForeColor = Color.Blue
                    sheet.Cells(rowIndex, sheet.Columns(tag).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                End If
            Next

        End Sub

        ''' <summary>
        ''' 条件文に全て合致するかチェック
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <param name="list"></param>
        ''' <returns>合致すればTRUE</returns>
        ''' <remarks></remarks>
        Private Function IsIfResult(ByVal rowIndex As Integer, ByVal list As List(Of String)) As Boolean

            'if条件作成'
            For index As Integer = 0 To list.Count - 1
                If StringUtil.Equals(list(index), "が") Then
                    'がの直上が項目名'
                    For Each vo As LabelValueVo In _ItemNameList
                        If StringUtil.Equals(vo.Label, list(index - 1)) Then
                            '指定セル位置=条件を意味する'
                            '「が」の直下が条件の値'

                            '比較元'
                            Dim compValue As String = sheet.Cells(rowIndex, sheet.Columns(vo.Value).Index).Value

                            If IsNumeric(list(index + 1)) _
                            AndAlso IsNumeric(compValue) Then
                                '数値型なら数値で合わせる'
                                If Double.Parse(compValue) <> Double.Parse(list(index + 1)) Then
                                    Return False
                                End If
                            Else
                                If Not StringUtil.Equals(compValue, list(index + 1)) Then
                                    Return False
                                End If

                            End If
                            Exit For
                        End If
                    Next
                End If

            Next
            Return True
        End Function

        ''' <summary>
        ''' 入力、かけるの値を取得
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <param name="list"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetValue(ByVal rowIndex As Integer, ByVal list As List(Of String)) As String
            Dim result As String = ""
            For index As Integer = 0 To list.Count - 1
                If StringUtil.Equals(list(index), "を") Then
                    '「を」の直上が入力用の値'
                    If IsNumeric(list(index - 1)) Then
                        '数値ならそのまま'
                        result = list(index - 1)
                    Else
                        '数値じゃないなら探す'
                        'をの直上が項目名'
                        For Each vo As LabelValueVo In _ItemNameList
                            '項目名の列位置を取得'
                            If StringUtil.Equals(vo.Label, list(index - 1)) Then
                                result = sheet.Cells(rowIndex, sheet.Columns(vo.Value).Index).Value
                                Exit For
                            End If
                        Next
                    End If

                    Exit For
                End If
            Next
            Return result
        End Function



#End Region

#Region "標準設定表へ書き込み"

        ''' <summary>
        ''' 標準設定表へ書き込み
        ''' </summary>
        ''' <param name="settingName"></param>
        ''' <remarks></remarks>
        Private Sub SetPartMultiSettingCSV(ByVal settingName As String)

            If Not System.IO.File.Exists(PARTS_COST_MULTI_SETTING_FILE) Then
                'ファイルがないので作成'
                'System.IO.File.Create(PARTS_COST_MULTI_SETTING_FILE)


                Dim sr As New System.IO.StreamWriter(PARTS_COST_MULTI_SETTING_FILE, False, System.Text.Encoding.GetEncoding("Shift-JIS"))
                'フィールドを書き込む'
                'わざわざスペースで区切ってるのにtab区切りにする必要はない。'
                sr.Write(settingName & "：" & tbOutPut.Text)
                '改行'
                sr.Write(vbCrLf)
                sr.Close()
            Else
                Dim list As New List(Of String)
                'テキストファイルのパス

                '書き込みと読み込みが可能なようにファイルを開く
                Dim fs As New System.IO.FileStream(PARTS_COST_MULTI_SETTING_FILE, _
                    System.IO.FileMode.Open, _
                    System.IO.FileAccess.ReadWrite, _
                    System.IO.FileShare.None)
                'FileStreamを基にして、StreamReaderを作成する
                Dim sr As New System.IO.StreamReader(fs, System.Text.Encoding.GetEncoding("Shift-JIS"))
                'FileStreamを基にして、StreamWriterを作成する
                Dim sw As New System.IO.StreamWriter(fs, System.Text.Encoding.GetEncoding("Shift-JIS"))

                Dim flg As Boolean = False
                While sr.Peek() > -1
                    Dim str As String = sr.ReadLine()
                    If StringUtil.IsNotEmpty(str) Then
                        If StringUtil.Equals(str.Substring(0, str.IndexOf("：")), settingName) Then
                            '同じ標準設定名があれば変更しちゃう。'
                            str = settingName & "：" & tbOutPut.Text
                            flg = True
                        End If
                        list.Add(str)
                    End If
                End While

                If Not flg Then
                    '上書き対象がいなかったので下に追加'
                    list.Add(settingName & "：" & tbOutPut.Text)
                End If

                '書き込み用の文字列作成'
                Dim writeValue As String = ""

                For Each s As String In list
                    writeValue = writeValue & s & vbCrLf
                Next


                '位置を先頭にする
                fs.Position = 0
                '長さを0にする
                fs.SetLength(0)
                '書き込む
                sw.Write(writeValue)
                sw.Flush()

                '閉じる
                sr.Close()
                '（StreamWriter.Closeは基になるStreamが閉じた後に呼び出すと、
                '　例外ObjectDisposedExceptionをスローする）
                If sw.BaseStream.CanWrite Then
                    sw.Close()
                End If
                fs.Close()

            End If

        End Sub


#End Region

#Region "コンボボックス設定"

#Region "設定する項目列コンボボックス設定"

        Private Function GetLabelValues_CmbSettingColumn() As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)
            '注意'
            'Labelの内容が項目名と絶対に一致しないこと！'
            ''
            Dim vo1 As New LabelValueVo
            vo1.Label = LABEL_YOSAN_KONKYO_KEISU_1
            vo1.Value = NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1

            Dim vo2 As New LabelValueVo
            vo2.Label = LABEL_YOSAN_WARITUKE_BUHIN_HI
            vo2.Value = NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI

            Dim vo3 As New LabelValueVo
            vo3.Label = LABEL_YOSAN_WARITUKE_KEISU_2
            vo3.Value = NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2

            Dim vo4 As New LabelValueVo
            vo4.Label = LABEL_YOSAN_KOUNYU_KIBOU_BUHIN_HI
            vo4.Value = NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI

            results.Add(vo1)
            results.Add(vo2)
            results.Add(vo3)
            results.Add(vo4)

            Return results
        End Function

#End Region

#Region "項目名コンボボックス設定"

        ''' <summary>
        ''' 項目名コンボボックス設定
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetLabelValues_CmbItemName() As List(Of LabelValueVo)

            _ItemNameList = New List(Of LabelValueVo)

            SettingItemNameLabelValueVo("国内集計", NmSpdTagBase.TAG_YOSAN_SHUKEI_CODE)
            SettingItemNameLabelValueVo("海外集計", NmSpdTagBase.TAG_YOSAN_SIA_SHUKEI_CODE)
            SettingItemNameLabelValueVo("部品番号", NmSpdTagBase.TAG_YOSAN_BUHIN_NO)
            SettingItemNameLabelValueVo("部品名称", NmSpdTagBase.TAG_YOSAN_BUHIN_NAME)
            SettingItemNameLabelValueVo("総数", NmSpdTagBase.TAG_YOSAN_INSU)
            SettingItemNameLabelValueVo("取引先コード", NmSpdTagBase.TAG_YOSAN_MAKER_CODE)
            SettingItemNameLabelValueVo("供給セクション", NmSpdTagBase.TAG_YOSAN_KYOUKU_SECTION)
            SettingItemNameLabelValueVo("購担", NmSpdTagBase.TAG_YOSAN_KOUTAN)
            SettingItemNameLabelValueVo("手配記号", NmSpdTagBase.TAG_YOSAN_TEHAI_KIGOU)
            SettingItemNameLabelValueVo("製作方法", NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_SEISAKU)
            SettingItemNameLabelValueVo("型仕様1", NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1)
            SettingItemNameLabelValueVo("型仕様2", NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2)
            SettingItemNameLabelValueVo("型仕様3", NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3)
            SettingItemNameLabelValueVo("治具", NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_TIGU)
            SettingItemNameLabelValueVo("部品製作規模・概要", NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KIBO)
            SettingItemNameLabelValueVo("試作部品費（円）", NmSpdTagBase.TAG_YOSAN_SHISAKU_BUHIN_HI)
            SettingItemNameLabelValueVo("試作型費（千円）", NmSpdTagBase.TAG_YOSAN_SHISAKU_KATA_HI)
            SettingItemNameLabelValueVo("NOTE", NmSpdTagBase.TAG_YOSAN_BUHIN_NOTE)
            SettingItemNameLabelValueVo("備考", NmSpdTagBase.TAG_YOSAN_BIKOU)
            SettingItemNameLabelValueVo("国外区分", NmSpdTagBase.TAG_YOSAN_KONKYO_KOKUGAI_KBN)
            SettingItemNameLabelValueVo("MIXｺｽﾄ部品費(円/セント)", NmSpdTagBase.TAG_YOSAN_KONKYO_MIX_BUHIN_HI)
            SettingItemNameLabelValueVo("引用元情報", NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI)
            SettingItemNameLabelValueVo("係数1", NmSpdTagBase.TAG_YOSAN_KONKYO_KEISU_1)
            SettingItemNameLabelValueVo("工法", NmSpdTagBase.TAG_YOSAN_KONKYO_KOUHOU)
            SettingItemNameLabelValueVo("割付予算根拠_部品費(円)", NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算根拠_係数2", NmSpdTagBase.TAG_YOSAN_WARITUKE_KEISU_2)

            SettingItemNameLabelValueVo("割付予算_量産単価①", NmSpdTagBase.TAG_KAKO_1_RYOSAN_TANKA)
            SettingItemNameLabelValueVo("割付予算_割付部品費(円)①", NmSpdTagBase.TAG_KAKO_1_WARITUKE_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_割付型費(円)①", NmSpdTagBase.TAG_KAKO_1_WARITUKE_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_割付工法①", NmSpdTagBase.TAG_KAKO_1_WARITUKE_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値部品費(円)①", NmSpdTagBase.TAG_KAKO_1_MAKER_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値型費(千円)①", NmSpdTagBase.TAG_KAKO_1_MAKER_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値工法①", NmSpdTagBase.TAG_KAKO_1_MAKER_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_審議値部品費(円)①", NmSpdTagBase.TAG_KAKO_1_SHINGI_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_審議値型費(千円)①", NmSpdTagBase.TAG_KAKO_1_SHINGI_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_審議値工法①", NmSpdTagBase.TAG_KAKO_1_SHINGI_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_購入希望単価(円)①", NmSpdTagBase.TAG_KAKO_1_KOUNYU_KIBOU_TANKA)
            SettingItemNameLabelValueVo("割付予算_購入単価(円)①", NmSpdTagBase.TAG_KAKO_1_KOUNYU_TANKA)
            SettingItemNameLabelValueVo("割付予算_支給品(円)①", NmSpdTagBase.TAG_KAKO_1_SHIKYU_HIN)
            SettingItemNameLabelValueVo("割付予算_工事指令№①", NmSpdTagBase.TAG_KAKO_1_KOUJI_SHIREI_NO)
            SettingItemNameLabelValueVo("割付予算_イベント名称①", NmSpdTagBase.TAG_KAKO_1_EVENT_NAME)
            SettingItemNameLabelValueVo("割付予算_発注日①", NmSpdTagBase.TAG_KAKO_1_HACHU_BI)
            SettingItemNameLabelValueVo("割付予算_検収日①", NmSpdTagBase.TAG_KAKO_1_KENSHU_BI)

            SettingItemNameLabelValueVo("割付予算_量産単価②", NmSpdTagBase.TAG_KAKO_2_RYOSAN_TANKA)
            SettingItemNameLabelValueVo("割付予算_割付部品費(円)②", NmSpdTagBase.TAG_KAKO_2_WARITUKE_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_割付型費(円)②", NmSpdTagBase.TAG_KAKO_2_WARITUKE_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_割付工法②", NmSpdTagBase.TAG_KAKO_2_WARITUKE_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値部品費(円)②", NmSpdTagBase.TAG_KAKO_2_MAKER_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値型費(千円)②", NmSpdTagBase.TAG_KAKO_2_MAKER_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値工法②", NmSpdTagBase.TAG_KAKO_2_MAKER_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_審議値部品費(円)②", NmSpdTagBase.TAG_KAKO_2_SHINGI_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_審議値型費(千円)②", NmSpdTagBase.TAG_KAKO_2_SHINGI_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_審議値工法②", NmSpdTagBase.TAG_KAKO_2_SHINGI_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_購入希望単価(円)②", NmSpdTagBase.TAG_KAKO_2_KOUNYU_KIBOU_TANKA)
            SettingItemNameLabelValueVo("割付予算_購入単価(円)②", NmSpdTagBase.TAG_KAKO_2_KOUNYU_TANKA)
            SettingItemNameLabelValueVo("割付予算_支給品(円)②", NmSpdTagBase.TAG_KAKO_2_SHIKYU_HIN)
            SettingItemNameLabelValueVo("割付予算_工事指令№②", NmSpdTagBase.TAG_KAKO_2_KOUJI_SHIREI_NO)
            SettingItemNameLabelValueVo("割付予算_イベント名称②", NmSpdTagBase.TAG_KAKO_2_EVENT_NAME)
            SettingItemNameLabelValueVo("割付予算_発注日②", NmSpdTagBase.TAG_KAKO_2_HACHU_BI)
            SettingItemNameLabelValueVo("割付予算_検収日②", NmSpdTagBase.TAG_KAKO_2_KENSHU_BI)

            SettingItemNameLabelValueVo("割付予算_量産単価③", NmSpdTagBase.TAG_KAKO_3_RYOSAN_TANKA)
            SettingItemNameLabelValueVo("割付予算_割付部品費(円)③", NmSpdTagBase.TAG_KAKO_3_WARITUKE_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_割付型費(円)③", NmSpdTagBase.TAG_KAKO_3_WARITUKE_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_割付工法③", NmSpdTagBase.TAG_KAKO_3_WARITUKE_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値部品費(円)③", NmSpdTagBase.TAG_KAKO_3_MAKER_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値型費(千円)③", NmSpdTagBase.TAG_KAKO_3_MAKER_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値工法③", NmSpdTagBase.TAG_KAKO_3_MAKER_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_審議値部品費(円)③", NmSpdTagBase.TAG_KAKO_3_SHINGI_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_審議値型費(千円)③", NmSpdTagBase.TAG_KAKO_3_SHINGI_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_審議値工法③", NmSpdTagBase.TAG_KAKO_3_SHINGI_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_購入希望単価(円)③", NmSpdTagBase.TAG_KAKO_3_KOUNYU_KIBOU_TANKA)
            SettingItemNameLabelValueVo("割付予算_購入単価(円)③", NmSpdTagBase.TAG_KAKO_3_KOUNYU_TANKA)
            SettingItemNameLabelValueVo("割付予算_支給品(円)③", NmSpdTagBase.TAG_KAKO_3_SHIKYU_HIN)
            SettingItemNameLabelValueVo("割付予算_工事指令№③", NmSpdTagBase.TAG_KAKO_3_KOUJI_SHIREI_NO)
            SettingItemNameLabelValueVo("割付予算_イベント名称③", NmSpdTagBase.TAG_KAKO_3_EVENT_NAME)
            SettingItemNameLabelValueVo("割付予算_発注日③", NmSpdTagBase.TAG_KAKO_3_HACHU_BI)
            SettingItemNameLabelValueVo("割付予算_検収日③", NmSpdTagBase.TAG_KAKO_3_KENSHU_BI)

            SettingItemNameLabelValueVo("割付予算_量産単価④", NmSpdTagBase.TAG_KAKO_4_RYOSAN_TANKA)
            SettingItemNameLabelValueVo("割付予算_割付部品費(円)④", NmSpdTagBase.TAG_KAKO_4_WARITUKE_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_割付型費(円)④", NmSpdTagBase.TAG_KAKO_4_WARITUKE_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_割付工法④", NmSpdTagBase.TAG_KAKO_4_WARITUKE_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値部品費(円)④", NmSpdTagBase.TAG_KAKO_4_MAKER_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値型費(千円)④", NmSpdTagBase.TAG_KAKO_4_MAKER_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値工法④", NmSpdTagBase.TAG_KAKO_4_MAKER_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_審議値部品費(円)④", NmSpdTagBase.TAG_KAKO_4_SHINGI_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_審議値型費(千円)④", NmSpdTagBase.TAG_KAKO_4_SHINGI_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_審議値工法④", NmSpdTagBase.TAG_KAKO_4_SHINGI_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_購入希望単価(円)④", NmSpdTagBase.TAG_KAKO_4_KOUNYU_KIBOU_TANKA)
            SettingItemNameLabelValueVo("割付予算_購入単価(円)④", NmSpdTagBase.TAG_KAKO_4_KOUNYU_TANKA)
            SettingItemNameLabelValueVo("割付予算_支給品(円)④", NmSpdTagBase.TAG_KAKO_4_SHIKYU_HIN)
            SettingItemNameLabelValueVo("割付予算_工事指令№④", NmSpdTagBase.TAG_KAKO_4_KOUJI_SHIREI_NO)
            SettingItemNameLabelValueVo("割付予算_イベント名称④", NmSpdTagBase.TAG_KAKO_4_EVENT_NAME)
            SettingItemNameLabelValueVo("割付予算_発注日④", NmSpdTagBase.TAG_KAKO_4_HACHU_BI)
            SettingItemNameLabelValueVo("割付予算_検収日④", NmSpdTagBase.TAG_KAKO_4_KENSHU_BI)

            SettingItemNameLabelValueVo("割付予算_量産単価⑤", NmSpdTagBase.TAG_KAKO_5_RYOSAN_TANKA)
            SettingItemNameLabelValueVo("割付予算_割付部品費(円)⑤", NmSpdTagBase.TAG_KAKO_5_WARITUKE_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_割付型費(円)⑤", NmSpdTagBase.TAG_KAKO_5_WARITUKE_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_割付工法⑤", NmSpdTagBase.TAG_KAKO_5_WARITUKE_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値部品費(円)⑤", NmSpdTagBase.TAG_KAKO_5_MAKER_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値型費(千円)⑤", NmSpdTagBase.TAG_KAKO_5_MAKER_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_ﾒｰｶｰ値工法⑤", NmSpdTagBase.TAG_KAKO_5_MAKER_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_審議値部品費(円)⑤", NmSpdTagBase.TAG_KAKO_5_SHINGI_BUHIN_HI)
            SettingItemNameLabelValueVo("割付予算_審議値型費(千円)⑤", NmSpdTagBase.TAG_KAKO_5_SHINGI_KATA_HI)
            SettingItemNameLabelValueVo("割付予算_審議値工法⑤", NmSpdTagBase.TAG_KAKO_5_SHINGI_KOUHOU)
            SettingItemNameLabelValueVo("割付予算_購入希望単価(円)⑤", NmSpdTagBase.TAG_KAKO_5_KOUNYU_KIBOU_TANKA)
            SettingItemNameLabelValueVo("割付予算_購入単価(円)⑤", NmSpdTagBase.TAG_KAKO_5_KOUNYU_TANKA)
            SettingItemNameLabelValueVo("割付予算_支給品(円)⑤", NmSpdTagBase.TAG_KAKO_5_SHIKYU_HIN)
            SettingItemNameLabelValueVo("割付予算_工事指令№⑤", NmSpdTagBase.TAG_KAKO_5_KOUJI_SHIREI_NO)
            SettingItemNameLabelValueVo("割付予算_イベント名称⑤", NmSpdTagBase.TAG_KAKO_5_EVENT_NAME)
            SettingItemNameLabelValueVo("割付予算_発注日⑤", NmSpdTagBase.TAG_KAKO_5_HACHU_BI)
            SettingItemNameLabelValueVo("割付予算_検収日⑤", NmSpdTagBase.TAG_KAKO_5_KENSHU_BI)

            SettingItemNameLabelValueVo("割付予算_部品費合計(円)", NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI_TOTAL)
            SettingItemNameLabelValueVo("割付予算_型費(千円)", NmSpdTagBase.TAG_YOSAN_WARITUKE_KATA_HI)
            SettingItemNameLabelValueVo("購入希望単価_購入希望単価(円)", NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_TANKA)
            SettingItemNameLabelValueVo("購入希望単価_部品費(円)", NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI)
            SettingItemNameLabelValueVo("購入希望単価_部品費合計(円)", NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI_TOTAL)
            SettingItemNameLabelValueVo("購入希望単価_型費(円)", NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_KATA_HI)

            Return _ItemNameList
        End Function

        ''' <summary>
        ''' 省略用
        ''' </summary>
        ''' <param name="label"></param>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Private Sub SettingItemNameLabelValueVo(ByVal label As String, ByVal value As String)
            Dim vo As New LabelValueVo
            vo.Label = label
            vo.Value = value
            _ItemNameList.Add(vo)
        End Sub



#End Region

#Region "指示内容コンボボックス設定"

        ''' <summary>
        ''' 指示内容コンボボックス設定
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetLabelValues_CmbDirections() As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)

            Dim vo1 As New LabelValueVo
            vo1.Label = "入力する"
            vo1.Value = "0"

            Dim vo2 As New LabelValueVo
            vo2.Label = "かける"
            vo2.Value = "1"


            results.Add(vo1)
            results.Add(vo2)

            Return results
        End Function

#End Region

#Region "項目内容コンボボックス設定"

        Private Function GetLabelValues_CmbItemContents(ByVal tag As String) As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)

            If StringUtil.IsNotEmpty(tag) Then
                Dim dic As New Dictionary(Of String, LabelValueVo)
                Dim keyList As New List(Of String)

                '前画面からもらってくるか？'
                For rowIndex As Integer = 3 To sheet.RowCount - 1
                    Dim vo As New LabelValueVo
                    Dim value As String = sheet.Cells(rowIndex, sheet.Columns(tag).Index).Value
                    If StringUtil.IsNotEmpty(value) Then

                        If Not dic.ContainsKey(value) Then
                            vo.Label = value
                            vo.Value = value

                            dic.Add(value, vo)
                            keyList.Add(value)
                        End If
                    End If
                Next

                keyList.Sort()

                For Each key As String In keyList
                    results.Add(dic(key))
                Next
            End If

            Return results
        End Function



#End Region

#End Region

#Region "色設定"

#Region "デフォルト色"

        Private Function DefaultColor() As Color
            Return Color.FromArgb(192, 255, 255)
        End Function

#End Region

#Region "選択状態色"

        Private Function ChoiceColor() As Color
            Return Color.FromArgb(255, 255, 128)
        End Function

#End Region

#End Region

    End Class
End Namespace