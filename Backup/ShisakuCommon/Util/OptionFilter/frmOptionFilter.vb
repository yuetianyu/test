Imports ShisakuCommon.Util.OptionFilter

''' <summary>
''' オプションフィルタ画面
''' </summary>
''' <remarks></remarks>
Public Class frmOptionFilter

#Region " 定数 "
    ''' <summary>検索値コンボボックスプレフィックス</summary>
    Private Const CTRL_VALUE As String = "cboValue_"

    ''' <summary>検索条件コンボボックスプレフィックス</summary>
    Private Const CTRL_CONDITION As String = "cboCondition_"

    ''' <summary>論理演算(AND)ラジオボタンプレフィックス</summary>
    Private Const CTRL_AND As String = "rdoAnd_"

    ''' <summary>論理演算(OR)ラジオボタンプレフィックス</summary>
    Private Const CTRL_OR As String = "rdoOr_"
#End Region

#Region " メンバー変数 "
    ''' <summary>抽出条件</summary>
    Private m_condition As OptionFilterEntity
#End Region

#Region " コンストラクタ "
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="nominate">検索値コンボボックスに表示する検索候補リスト</param>
    ''' <param name="condition">抽出条件</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nominate As List(Of String), ByVal condition As OptionFilterEntity)

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        m_condition = condition

        '検索候補値がない場合はここで終了
        If nominate Is Nothing Then
            Return
        End If

        For idx As Integer = 1 To MAX_FILTER_CONDITION
            Dim cboValue As ComboBox = Me.GetValueComboBox(idx)
            Dim cboCondition As ComboBox = Me.GetConditionComboBox(idx)

            For Each s As String In nominate
                cboValue.Items.Add(s)
            Next

            Me.CreateConditionComboBox(cboCondition)
        Next
    End Sub
#End Region

#Region " フォーム "
    ''' <summary>
    ''' フォーム ロード時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmOptionFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For idx As Integer = 1 To MAX_FILTER_CONDITION
            Me.GetValueComboBox(idx).Text = m_condition.Value(idx - 1)
            Me.GetConditionComboBox(idx).SelectedValue = m_condition.Condition(idx - 1)

            'AND/OR論理演算は4個のため
            If idx < MAX_FILTER_CONDITION Then
                If m_condition.AndOr(idx - 1) = CONDITION_AND Then
                    Me.GetAndRadioButton(idx).Checked = True
                Else
                    Me.GetOrRadioButton(idx).Checked = True
                End If
            End If
        Next
    End Sub
#End Region

#Region " 条件クリアボタン "
    ''' <summary>
    ''' 条件クリアボタン 押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        For i As Integer = 1 To MAX_FILTER_CONDITION
            Me.GetValueComboBox(i).Text = String.Empty
            Me.GetConditionComboBox(i).SelectedIndex = -1

            'AND/OR論理演算は4個のため
            If i < MAX_FILTER_CONDITION Then
                Me.GetOrRadioButton(i).Checked = True
            End If
        Next
    End Sub
#End Region

#Region " OKボタン "
    ''' <summary>
    ''' OKボタン 押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        For idx As Integer = 1 To MAX_FILTER_CONDITION
            '入力チェック
            If Not InputCheck(idx) Then
                Return
            End If

            m_condition.Value(idx - 1) = Me.GetValueComboBox(idx).Text.Trim()
            m_condition.Condition(idx - 1) = Me.GetConditionComboBox(idx).SelectedValue

            'AND/OR論理演算は4個のため
            If idx < MAX_FILTER_CONDITION Then
                m_condition.AndOr(idx - 1) = Me.GetSelectedAndOr(idx)
            End If
        Next

        '1つ以上入力されているかチェック
        If Not IsData() Then
            MsgBox("1以上抽出条件を入力してください。", vbInformation, "確認")
            Me.GetValueComboBox(1).Focus()
            Return
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
#End Region

#Region " キャンセルボタン "
    ''' <summary>
    ''' キャンセルボタン 押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
#End Region

#Region " コントロール検索 "
    ''' <summary>
    ''' コントロール検索
    ''' </summary>
    ''' <param name="prefix"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SearchControl(ByVal prefix As String, ByVal index As Integer) As Control
        Dim ctrls() As Control = _
            Me.Controls.Find(prefix & index.ToString(), True)

        If ctrls.Length = 0 Then
            Return Nothing
        End If

        Return ctrls(0)
    End Function
#End Region

#Region " 検索値コンボボックス取得 "
    ''' <summary>
    ''' 指定されたインデックス番号の検索値コンボボックスを返します.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetValueComboBox(ByVal index As Integer) As ComboBox
        Dim ctrl As Control = SearchControl(CTRL_VALUE, index)
        Return CType(ctrl, ComboBox)
    End Function
#End Region

#Region " 抽出条件コンボボックス取得 "
    ''' <summary>
    ''' 指定されたインデックス番号の抽出条件コンボボックスを返します.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetConditionComboBox(ByVal index As Integer) As ComboBox
        Dim ctrl As Control = SearchControl(CTRL_CONDITION, index)
        Return CType(ctrl, ComboBox)
    End Function
#End Region

#Region " 抽出条件コンボボックス生成 "
    ''' <summary>
    ''' 抽出条件コンボボックスの生成
    ''' </summary>
    ''' <param name="cbo"></param>
    ''' <remarks></remarks>
    Private Sub CreateConditionComboBox(ByVal cbo As ComboBox)
        Dim dt As New DataTable()

        dt.Columns.Add("VALUE", GetType(FILTER_CONDITION))
        dt.Columns.Add("DISPLAY", GetType(String))

        dt.Rows.Add(New Object() {FILTER_CONDITION.NONE, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.NONE)})
        dt.Rows.Add(New Object() {FILTER_CONDITION.EQUALS, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.EQUALS)})
        dt.Rows.Add(New Object() {FILTER_CONDITION.NOT_EQUALS, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.NOT_EQUALS)})
        dt.Rows.Add(New Object() {FILTER_CONDITION.BIGGER, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.BIGGER)})
        dt.Rows.Add(New Object() {FILTER_CONDITION.MORE_THAN, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.MORE_THAN)})
        dt.Rows.Add(New Object() {FILTER_CONDITION.SMALLER, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.SMALLER)})
        dt.Rows.Add(New Object() {FILTER_CONDITION.LESS_THAN, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.LESS_THAN)})
        dt.Rows.Add(New Object() {FILTER_CONDITION.BEGIN_TO, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.BEGIN_TO)})
        dt.Rows.Add(New Object() {FILTER_CONDITION.NOT_BEGIN_TO, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.NOT_BEGIN_TO)})
        dt.Rows.Add(New Object() {FILTER_CONDITION.FINISH_TO, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.FINISH_TO)})
        dt.Rows.Add(New Object() {FILTER_CONDITION.NOT_FINISH_TO, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.NOT_FINISH_TO)})
        dt.Rows.Add(New Object() {FILTER_CONDITION.INCLUDES, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.INCLUDES)})
        dt.Rows.Add(New Object() {FILTER_CONDITION.NOT_INCLUDES, OptionFilterCommon.GetOptionFilterConditionName(FILTER_CONDITION.NOT_INCLUDES)})

        cbo.ValueMember = "VALUE"
        cbo.DisplayMember = "DISPLAY"
        cbo.DataSource = dt
    End Sub
#End Region

#Region " 論理演算(AND)ラジオボタン取得 "
    ''' <summary>
    ''' 論理演算(AND)ラジオボタン取得
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAndRadioButton(ByVal index As Integer) As RadioButton
        Dim ctrl As Control = SearchControl(CTRL_AND, index)
        Return CType(ctrl, RadioButton)
    End Function
#End Region

#Region " 論理演算(OR)ラジオボタン取得 "
    ''' <summary>
    ''' 論理演算(OR)ラジオボタン取得
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOrRadioButton(ByVal index As Integer) As RadioButton
        Dim ctrl As Control = SearchControl(CTRL_OR, index)
        Return CType(ctrl, RadioButton)
    End Function
#End Region

#Region " 論理演算選択値取得 "
    ''' <summary>
    ''' 論理演算選択値取得
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSelectedAndOr(ByVal index As Integer) As String
        Dim rdoAnd As RadioButton = Me.GetAndRadioButton(index)
        Dim rdoOr As RadioButton = Me.GetOrRadioButton(index)

        If rdoAnd.Checked Then
            Return CONDITION_AND
        Else
            Return CONDITION_OR
        End If
    End Function
#End Region

#Region " 入力チェック "
    ''' <summary>
    ''' 入力チェック(行)
    ''' </summary>
    ''' <param name="idx"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InputCheck(ByVal idx As Integer) As Boolean
        If Not Trim(Me.GetValueComboBox(idx).Text) = "" Then
            '抽出方法
            If Trim(Me.GetConditionComboBox(idx).Text) = "" Then
                MsgBox("抽出条件を入力してください。", vbInformation, "確認")
                Me.GetConditionComboBox(idx).Focus()
                Return False
            End If
        End If

        InputCheck = True
    End Function

    ''' <summary>
    ''' 1つ以上入力されているかチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsData() As Boolean
        For idx As Integer = 1 To MAX_FILTER_CONDITION
            If Not Trim(Me.GetValueComboBox(idx).Text) = "" Then
                Return True
            End If
        Next

        Return False
    End Function
#End Region

#Region " 抽出条件の自動セット "

    Private Sub cboValue_1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboValue_1.LostFocus
        '値があるならデフォルトを設定
        If cboValue_1.Text <> "" Then
            If cboCondition_1.Text = "" Then
                cboCondition_1.Text = GetOptionFilterConditionName(0)
            End If
        Else
            cboCondition_1.Text = Nothing
        End If
    End Sub
    Private Sub cboValue_2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboValue_2.LostFocus
        '値があるならデフォルトを設定
        If cboValue_2.Text <> "" Then
            If cboCondition_2.Text = "" Then
                cboCondition_2.Text = GetOptionFilterConditionName(0)
            End If
        Else
            cboCondition_2.Text = Nothing
        End If
    End Sub
    Private Sub cboValue_3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboValue_3.LostFocus
        '値があるならデフォルトを設定
        If cboValue_3.Text <> "" Then
            If cboCondition_3.Text = "" Then
                cboCondition_3.Text = GetOptionFilterConditionName(0)
            End If
        Else
            cboCondition_3.Text = Nothing
        End If
    End Sub
    Private Sub cboValue_4_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboValue_4.LostFocus
        '値があるならデフォルトを設定
        If cboValue_4.Text <> "" Then
            If cboCondition_4.Text = "" Then
                cboCondition_4.Text = GetOptionFilterConditionName(0)
            End If
        Else
            cboCondition_4.Text = Nothing
        End If
    End Sub
    Private Sub cboValue_5_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboValue_5.LostFocus
        '値があるならデフォルトを設定
        If cboValue_5.Text <> "" Then
            If cboCondition_5.Text = "" Then
                cboCondition_5.Text = GetOptionFilterConditionName(0)
            End If
        Else
            cboCondition_5.Text = Nothing
        End If
    End Sub

#End Region

End Class
