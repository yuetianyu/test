Imports FarPoint.Win
Imports ShisakuCommon.Util.OptionFilter

Namespace Util.OptionFilter

    ''' <summary>
    ''' SPREAD オプションフィルター
    ''' </summary>
    ''' <remarks></remarks>
    Public Class OptionFilter
        Inherits Spread.BaseFilterItem

#Region " メンバー変数 "
        ''' <summary>対象のシートオブジェクト</summary>
        Private m_sheet As Spread.SheetView

        ''' <summary>フィルタ名</summary>
        Private m_displayName As String

        ''' <summary>抽出条件</summary>
        Private m_condition As New OptionFilterEntity()

        ''' <summary>フィルタ対象の列インデックス</summary>
        Private m_filterColIdx As Integer = -1

        ''' <summary>SPREADのヘッダー行数</summary>
        Private m_Head As Integer

        ''' <summary>フィルタ候補使用空白文字列</summary>
        Private Const CONST_SPACE_NAME = "(空白)"

#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="sheet">対象のSPREAD</param>
        ''' <param name="displayName">フィルタ表示名称</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal sheet As Spread.SheetView, ByVal displayName As String, ByVal w_Head As Integer)
            m_sheet = sheet
            m_displayName = displayName

            m_Head = w_Head
        End Sub
#End Region

#Region " フィルタ表示名称表示 "
        ''' <summary>
        ''' フィルタ名称表示
        ''' </summary>
        ''' <param name="columnIndex"></param>
        ''' <param name="filteredInRowList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ShowInDropDown(ByVal columnIndex As Integer, ByVal filteredInRowList() As Integer) As Boolean
            'フィルタ候補ドロップダウンが表示された時の列インデックスを保持しておく.
            '他の列でフィルタが掛けられた時にフィルタ条件フォームを表示させないよう判定するためである.
            m_filterColIdx = columnIndex

            Return True
        End Function
#End Region

#Region " フィルタ候補一覧生成 "
        ''' <summary>
        ''' フィルタ候補一覧生成
        ''' </summary>
        ''' <remarks></remarks>
        Private Function CreateNominateList(ByVal colIdx As Integer) As List(Of String)
            Dim nominate As New Dictionary(Of String, String)
            Dim retNominate As New List(Of String)
            Dim intNominate As New Dictionary(Of Integer, Integer)
            'タイトル行を除     く　By柳沼
            '手配帳も同様のロジックを使用する場合、ヘッダー行数をパラメータで渡す必要がアリアン酢。
            For rowIdx As Integer = m_Head To m_sheet.RowCount - 1

                '非表示行は対象としない
                If m_sheet.Rows(rowIdx).Visible = False Then
                    Continue For
                End If

                '値が存在しない場合は次へ
                If m_sheet.GetValue(rowIdx, colIdx) Is Nothing Then
                    Continue For
                End If

                '余白を消す'
                Dim value As String = m_sheet.GetValue(rowIdx, colIdx).ToString().Trim()

                If nominate.ContainsKey(value) Then
                    Continue For
                End If

                'If value = "" Then
                '    Continue For
                'End If

                nominate.Add(value, value)

            Next

            'ディクショナリ配列をKeyValuePairのリストとして取得し, 値でソートを行う.
            Dim lst As New List(Of KeyValuePair(Of String, String))(nominate)
            lst.Sort(AddressOf CompareNominateValue)



            '文字列のリストへ変換
            For Each kvp As KeyValuePair(Of String, String) In lst
                retNominate.Add(kvp.Value)
            Next

            Dim workNominate As New List(Of String)

            workNominate.Add(CONST_SPACE_NAME)

            '先頭に（空白）を追加及びEmpty要素を排除
            For i As Integer = 0 To retNominate.Count - 1
                If retNominate(i).Equals(String.Empty) Then
                    Continue For
                End If

                workNominate.Add(retNominate(i))
            Next

            'ここの処理を変更してみる'
            '取引先コードの列いくつだったっけ？'
            'If colIdx = 15 Then
            '    Dim trcdList As New List(Of Integer)
            '    For index As Integer = 1 To workNominate.Count - 1
            '        trcdList.Add(Integer.Parse(workNominate(index)))
            '    Next
            '    'Integer型でソートしてStringで出す'
            '    trcdList.Sort()
            '    Dim trcdWorkNominate As New List(Of String)
            '    '(空白)追加'
            '    trcdWorkNominate.Add(CONST_SPACE_NAME)
            '    For index2 As Integer = 0 To trcdList.Count - 1
            '        Dim trcd As String

            '        trcd = Right("0000" + trcdList(index2).ToString, 4)
            '        trcdWorkNominate.Add(trcd)
            '    Next
            '    Return trcdWorkNominate
            'End If

            '取引先コードの場合はブレイク
            'If colIdx = 4 Then
            '    Return workNominate
            'End If


            If workNominate.Count = 1 Then
                Return workNominate
            End If

            If workNominate(1).Length > 2 Then
                Return workNominate
            End If

            If workNominate(1) >= "0" And workNominate(1) <= "99" _
              Or workNominate(1) = "**" Then
            Else
                Return workNominate
            End If

            'こっちは合計員数と号車の員数'
            Try
                If Not StringUtil.Equals(workNominate(1), "**") Then
                    Dim a As Integer = Integer.Parse(workNominate(1))
                End If

                Dim intList As New List(Of Integer)
                For index As Integer = 1 To workNominate.Count - 1
                    If StringUtil.Equals(workNominate(index), "**") Then
                        intList.Add(-1)
                    Else
                        intList.Add(Integer.Parse(workNominate(index)))
                    End If
                Next
                intList.Sort()

                Dim intWorkNominate As New List(Of String)
                intWorkNominate.Add(CONST_SPACE_NAME)
                For index2 As Integer = 0 To intList.Count - 1
                    If intList(index2) = -1 Then
                        intWorkNominate.Add("**")
                    Else
                        intWorkNominate.Add(intList(index2).ToString)
                    End If
                Next

                Return intWorkNominate
            Catch ex As Exception
                Return workNominate
            End Try





            Return workNominate
        End Function
#End Region

#Region " 検索候補値ソート "
        ''' <summary>
        ''' 検索候補値ソート
        ''' </summary>
        ''' <typeparam name="TKey"></typeparam>
        ''' <typeparam name="TValue"></typeparam>
        ''' <param name="kvp1"></param>
        ''' <param name="kvp2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CompareNominateValue(Of TKey, TValue As IComparable(Of TValue))( _
                                                ByVal kvp1 As KeyValuePair(Of TKey, TValue), _
                                                ByVal kvp2 As KeyValuePair(Of TKey, TValue)) As Integer
            Return kvp1.Value.CompareTo(kvp2.Value)
        End Function
#End Region

#Region " フィルタ実行 "
        ''' <summary>
        ''' フィルタ実行
        ''' </summary>
        ''' <param name="columnIndex">列インデックス</param>
        ''' <returns>フィルタ条件に一致した行インデックス配列</returns>
        ''' <remarks>
        ''' このメソッドが呼び出される前にフィルタが解除される模様...
        ''' 制御できる方法が見つからないので現在は条件フォームを表示している間は
        ''' フィルタが解除された状態が表示されている...
        ''' </remarks>
        Public Overrides Function Filter(ByVal columnIndex As Integer) As Integer()
            Dim matchList As New ArrayList()                '条件に一致する行リスト
            Dim returnList As Integer() = Nothing           '条件に一致する行配列(戻り値用)

            'フィルタを設定した列のみ
            If m_filterColIdx = columnIndex Then
                '(このメソッドは他の列のフィルタが掛けられた場合にも呼び出されることに注意.)
                'オプションフィルタダイアログの表示
                Using f As New frmOptionFilter(Me.CreateNominateList(columnIndex), m_condition)
                    f.ShowDialog()
                End Using
            End If

            'キャンセルされた場合などでフィルタ条件が存在しない場合は全てを一致したこととする.
            'ただし, これだとまだフィルタを掛けていない列に対してキャンセルした場合に
            'フィルタが掛かっている判定となって問題があるが, 回避方法が分かりません.
            'グローバル変数にフィルタ条件なしでキャンセルされた旨を保持し, 
            'フィルタ終了イベントにてフィルタ解除を行えばできそうだが, 機能をカプセル化できなくなるで
            'なんか嫌だ...
            If Not m_condition.IsData() Then
                For rowIdx As Integer = 0 To m_sheet.RowCount - 1
                    matchList.Add(rowIdx)
                Next
            Else

                'タイトル行をリストへ　By柳沼
                '手配帳も同様のロジックを使用する場合、ヘッダー行数をパラメータで渡す必要がアリアン酢。
                For rowIdx As Integer = 0 To m_Head - 1
                    matchList.Add(rowIdx)
                Next

                'フィルタ条件に一致する行インデックスを格納
                For rowIdx As Integer = m_Head To m_sheet.RowCount - 1
                    ''値がNULLの場合は次の行へ 2 /9 空白条件対応によりコメントアウト
                    'If m_sheet.GetValue(rowIdx, columnIndex) Is Nothing Then
                    '    Continue For
                    'End If

                    Dim spreadValue As Object = m_sheet.GetValue(rowIdx, columnIndex)   'SPREAD値
                    Dim preAndOr As String = String.Empty                               '論理演算(前行保持用)
                    Dim preCondition() As Boolean                                       '論理演算(全行保持用)
                    Dim preConditionCnt As Integer = 0                                  '論理演算(全行保持用ｶｳﾝﾀ)

                    'Nothingもしくはスペース格納はEmptyで置き換え
                    If spreadValue Is Nothing OrElse spreadValue.ToString.Trim.Equals(String.Empty) Then
                        spreadValue = String.Empty
                    End If

                    '配列初期化
                    ReDim preCondition(preConditionCnt)

                    '条件1～5の判定
                    For i As Integer = 0 To MAX_FILTER_CONDITION - 1
                        Dim conditionValue As Object = m_condition.Value(i)

                        '条件が入力されていない場合は次へ
                        If m_condition.Value(i) = String.Empty Then
                            Continue For
                        End If

                        '数字や日付などの大小判定が正しく行われるようにするため, 型変換を行う.
                        '変換できない場合は, 文字列として比較を行うようにする.
                        Try
                            'ここで'
                            '値で見るとダメ'
                            If TypeOf (m_sheet.Columns(columnIndex).CellType) Is Spread.CellType.CurrencyCellType Then
                                spreadValue = Decimal.Parse(spreadValue)
                            End If

                            conditionValue = Convert.ChangeType(conditionValue, Type.GetTypeCode(spreadValue.GetType()))



                            If conditionValue.Equals(CONST_SPACE_NAME) Then
                                conditionValue = String.Empty
                            End If

                        Catch ex As Exception
                            spreadValue = spreadValue.ToString()
                        End Try

                        '前条件に応じて AND OR を切り換える.
                        If preAndOr = CONDITION_AND Then
                            preCondition(preConditionCnt) = preCondition(preConditionCnt) And _
                                Me.IsCondition(i, conditionValue, spreadValue)
                        Else
                            preCondition(preConditionCnt) = preCondition(preConditionCnt) Or _
                                Me.IsCondition(i, conditionValue, spreadValue)
                        End If

                        '前行の条件として保持
                        preAndOr = m_condition.AndOr(i)

                        '次の条件がORの場合, ここまでの結果を保持するため
                        '新しく前条件までの結果の配列を生成する.
                        If preAndOr = CONDITION_OR Then
                            preConditionCnt = preConditionCnt + 1
                            ReDim Preserve preCondition(preConditionCnt)
                        End If
                    Next

                    '条件を満たしている場合
                    For i As Integer = 0 To preConditionCnt
                        If preCondition(i) Then
                            matchList.Add(rowIdx)
                            Exit For
                        End If
                    Next i
                Next rowIdx
            End If

            '条件に一致した行が存在する場合は配列にコピーします
            If matchList.Count > 0 Then
                returnList = New Integer(matchList.Count - 1) {}
                matchList.CopyTo(returnList)
                matchList.Clear()
            End If

            '初期値に戻す.
            m_filterColIdx = -1

            '条件に一致した全ての行インデックスを返します
            Return returnList
        End Function
#End Region

#Region " 抽出条件比較処理 "
        ''' <summary>
        ''' 抽出条件比較処理
        ''' </summary>
        ''' <param name="conditionIdx">条件インデックス</param>
        ''' <param name="conditionValue">条件値</param>
        ''' <param name="spreadValue">SPREAD値</param>
        ''' <returns>条件に一致する場合 True を返します.</returns>
        ''' <remarks></remarks>
        Private Function IsCondition(ByVal conditionIdx As Integer, ByVal conditionValue As Object, ByVal spreadValue As Object) As Boolean
            Select Case m_condition.Condition(conditionIdx)
                Case FILTER_CONDITION.EQUALS
                    Return (conditionValue = spreadValue)

                Case FILTER_CONDITION.NOT_EQUALS
                    Return (conditionValue <> spreadValue)

                Case FILTER_CONDITION.BIGGER
                    IsCondition = (spreadValue > conditionValue)

                Case FILTER_CONDITION.MORE_THAN
                    IsCondition = (spreadValue >= conditionValue)

                Case FILTER_CONDITION.SMALLER
                    IsCondition = (spreadValue < conditionValue)

                Case FILTER_CONDITION.LESS_THAN
                    IsCondition = (spreadValue <= conditionValue)

                Case FILTER_CONDITION.BEGIN_TO
                    IsCondition = (InStr(1, spreadValue, conditionValue) = 1)

                Case FILTER_CONDITION.NOT_BEGIN_TO
                    IsCondition = (InStr(1, spreadValue, conditionValue) <> 1)

                Case FILTER_CONDITION.FINISH_TO
                    If Len(spreadValue) > Len(conditionValue) Then
                        IsCondition = (conditionValue = Mid(spreadValue, Len(spreadValue) - (Len(conditionValue) - 1)))
                    Else
                        IsCondition = False
                    End If

                Case FILTER_CONDITION.NOT_FINISH_TO
                    If Len(spreadValue) > Len(conditionValue) Then
                        IsCondition = Not (conditionValue = Mid(spreadValue, Len(spreadValue) - (Len(conditionValue) - 1)))
                    Else
                        IsCondition = False
                    End If

                Case FILTER_CONDITION.INCLUDES
                    IsCondition = (InStr(1, spreadValue, conditionValue) > 0)

                Case FILTER_CONDITION.NOT_INCLUDES
                    IsCondition = Not (InStr(1, spreadValue, conditionValue) > 0)

                Case Else
                    Return False
            End Select
        End Function
#End Region

#Region " フィルタ表示名称 "
        ''' <summary>
        ''' フィルタ表示名称
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides ReadOnly Property DisplayName() As String
            Get
                Return m_displayName
            End Get
        End Property
#End Region

    End Class
End Namespace
