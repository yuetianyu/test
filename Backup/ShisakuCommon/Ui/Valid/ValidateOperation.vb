Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid
    ''' <summary>
    ''' Validateチェックの各操作を宣言したinterface
    ''' </summary>
    ''' <typeparam name="T">チェックする値の型</typeparam>
    ''' <remarks></remarks>
    Public Interface ValidateOperation(Of T)
        ''' <summary>
        ''' Validateチェック対象となったWindowsコントロール用アクセッサ
        ''' </summary>
        ReadOnly Property Accessor() As ControlAccessor(Of T)

        ''' <summary>
        ''' 必須チェックをする
        ''' </summary>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function Required() As ValidateOperation(Of T)

        ''' <summary>
        ''' 必須チェックをする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function Required(ByVal errorMessage As String) As ValidateOperation(Of T)

        ''' <summary>
        ''' 必須チェックをする
        ''' </summary>
        ''' <param name="allowEmpty">空文字入力を許可する場合、true</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function Required(ByVal allowEmpty As Boolean) As ValidateOperation(Of T)

        ''' <summary>
        ''' 必須チェックをする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <param name="allowEmpty">空文字入力を許可する場合、true</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function Required(ByVal errorMessage As String, ByVal allowEmpty As Boolean) As ValidateOperation(Of T)

        ''' <summary>
        ''' 入力禁止チェックをする
        ''' </summary>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function Empty() As ValidateOperation(Of T)
        ''' <summary>
        ''' 入力禁止チェックをする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function Empty(ByVal errorMessage As String) As ValidateOperation(Of T)
        ''' <summary>
        ''' 入力禁止チェックをする
        ''' </summary>
        ''' <param name="nullOnly">null状態だけを未入力として判断する場合、true</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function Empty(ByVal nullOnly As Boolean) As ValidateOperation(Of T)
        ''' <summary>
        ''' 入力禁止チェックをする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <param name="nullOnly">null状態だけを未入力として判断する場合、true</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function Empty(ByVal errorMessage As String, ByVal nullOnly As Boolean) As ValidateOperation(Of T)

        ''' <summary>
        ''' 文字種 数値チェックをする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function Numeric(Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T)

        ''' <summary>
        ''' 指定日付と同じか、それより新しい日付であることをチェックする
        ''' </summary>
        ''' <param name="compareValue">比較する日付</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function GreaterEqual(ByVal compareValue As DateTime, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T)
        ''' <summary>
        ''' 指定値以上である事をチェックする
        ''' </summary>
        ''' <param name="compareValue">比較する値</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function GreaterEqual(ByVal compareValue As Object, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T)
        ''' <summary>
        ''' 指定日付と同じか、それより古い日付であることをチェックする
        ''' </summary>
        ''' <param name="compareValue">比較する日付</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function LessEqual(ByVal compareValue As DateTime, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T)
        ''' <summary>
        ''' 指定値以下である事をチェックする
        ''' </summary>
        ''' <param name="compareValue">比較する値</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function LessEqual(ByVal compareValue As Object, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T)

        ''' <summary>
        ''' 値が指定範囲内である事をチェックする
        ''' </summary>
        ''' <param name="minValue">最小値</param>
        ''' <param name="maxValue">最大値</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function Range(ByVal minValue As Object, ByVal maxValue As Object, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T)

        ''' <summary>
        ''' 文字列長が、指定範囲内である事をチェックする
        ''' </summary>
        ''' <param name="minLengthB">最小文字列長</param>
        ''' <param name="maxLengthB">最大文字列長</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function RangeLengthByte(ByVal minLengthB As Integer, ByVal maxLengthB As Integer, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T)

        ''' <summary>
        ''' 文字列長が、指定長以内である事をチェックする
        ''' </summary>
        ''' <param name="maxLengthB">最大文字列長</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function MaxLengthByte(ByVal maxLengthB As Integer, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T)

        ''' <summary>
        ''' 半角文字列のみであることをチェックする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function Hankaku(Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T)

        ''' <summary>
        ''' 全角文字列のみであることをチェックする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function Zenkaku(Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T)

        ''' <summary>
        ''' 指定値のいずれかである事をチェックする
        ''' </summary>
        ''' <param name="params">指定値</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Function InArray(ByVal ParamArray params As T()) As ValidateOperation(Of T)

    End Interface
End Namespace