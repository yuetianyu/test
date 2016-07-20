Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl
    ''' <summary>
    ''' Validateチェックの実装クラス
    ''' </summary>
    ''' <typeparam name="T">チェックする値の型</typeparam>
    ''' <remarks></remarks>
    Friend Class ValidateOperationImpl(Of T) : Implements ValidateOperation(Of T)
        Private ReadOnly _baseValidator As BaseValidator
        Private ReadOnly _accessor As ControlAccessor(Of T)
        Private _baseInfo As BaseValidatorInfo

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="baseValidator">Validateチェック管理</param>
        ''' <param name="accessor">チャック対象のWindowsコントロールアクセッサ</param>
        ''' <param name="baseInfo">Validateチェック管理情報</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal baseValidator As BaseValidator, ByVal accessor As ControlAccessor(Of T), ByVal baseInfo As BaseValidatorInfo)
            Me._baseValidator = baseValidator
            Me._accessor = accessor
            Me._baseInfo = baseInfo
        End Sub

        Public ReadOnly Property Accessor() As ControlAccessor(Of T) Implements ValidateOperation(Of T).Accessor
            Get
                Return _accessor
            End Get
        End Property

        ''' <summary>
        ''' 必須チェックをする
        ''' </summary>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function Required() As ValidateOperation(Of T) Implements ValidateOperation(Of T).Required
            Return Required(Nothing, False)
        End Function

        ''' <summary>
        ''' 必須チェックをする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function Required(ByVal errorMessage As String) As ValidateOperation(Of T) Implements ValidateOperation(Of T).Required
            Return Required(errorMessage, False)
        End Function

        ''' <summary>
        ''' 必須チェックをする
        ''' </summary>
        ''' <param name="allowEmpty">空文字入力を許可する場合、true</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function Required(ByVal allowEmpty As Boolean) As ValidateOperation(Of T) Implements ValidateOperation(Of T).Required
            Return Required(Nothing, allowEmpty)
        End Function

        ''' <summary>
        ''' 必須チェックをする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <param name="allowEmpty">空文字入力を許可する場合、true</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function Required(ByVal errorMessage As String, ByVal allowEmpty As Boolean) As ValidateOperation(Of T) Implements ValidateOperation(Of T).Required
            Dim v1 As New RequiredValidator(Of T)(_accessor, allowEmpty, New ValidatorMessage(_baseInfo, errorMessage))
            _baseValidator.Add(v1)
            Return Me
        End Function

        ''' <summary>
        ''' 入力禁止チェックをする
        ''' </summary>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function Empty() As ValidateOperation(Of T) Implements ValidateOperation(Of T).Empty
            Return Empty(Nothing, False)
        End Function
        ''' <summary>
        ''' 入力禁止チェックをする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function Empty(ByVal errorMessage As String) As ValidateOperation(Of T) Implements ValidateOperation(Of T).Empty
            Return Empty(errorMessage, False)
        End Function
        ''' <summary>
        ''' 入力禁止チェックをする
        ''' </summary>
        ''' <param name="nullOnly">null状態だけを未入力として判断する場合、true</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function Empty(ByVal nullOnly As Boolean) As ValidateOperation(Of T) Implements ValidateOperation(Of T).Empty
            Return Empty(Nothing, nullOnly)
        End Function
        ''' <summary>
        ''' 入力禁止チェックをする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <param name="nullOnly">null状態だけを未入力として判断する場合、true</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function Empty(ByVal errorMessage As String, ByVal nullOnly As Boolean) As ValidateOperation(Of T) Implements ValidateOperation(Of T).Empty
            Dim v1 As New EmptyValidator(Of T)(_accessor, nullOnly, New ValidatorMessage(_baseInfo, errorMessage))
            _baseValidator.Add(v1)
            Return Me
        End Function

        ''' <summary>
        ''' 文字種 数値チェックをする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function Numeric(Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T) Implements ValidateOperation(Of T).Numeric
            Dim v1 As New NumericValidator(Of T)(_accessor, New ValidatorMessage(_baseInfo, errorMessage))
            _baseValidator.Add(v1)
            Return Me
        End Function

        ''' <summary>
        ''' 指定日付と同じか、それより新しい日付であることをチェックする
        ''' </summary>
        ''' <param name="compareValue">比較する日付</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function GreaterEqual(ByVal compareValue As DateTime, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T) Implements ValidateOperation(Of T).GreaterEqual
            If Not TypeOf _accessor Is ControlAccessor(Of DateTime) Then
                Throw New InvalidOperationException("日付をもつコントロール以外は、日付と比較出来ません.")
            End If
            Dim v1 As New GreaterEqualDateValidator(_accessor, compareValue, New ValidatorMessage(_baseInfo, errorMessage))
            _baseValidator.Add(v1)
            Return Me
        End Function

        ''' <summary>
        ''' 指定値以上である事をチェックする
        ''' </summary>
        ''' <param name="compareValue">比較する値</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function GreaterEqual(ByVal compareValue As Object, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T) Implements ValidateOperation(Of T).GreaterEqual
            Dim v1 As New GreaterEqualValidator(Of T)(_accessor, compareValue, New ValidatorMessage(_baseInfo, errorMessage))
            _baseValidator.Add(v1)
            Numeric(errorMessage)
            Return Me
        End Function

        ''' <summary>
        ''' 指定日付と同じか、それより古い日付であることをチェックする
        ''' </summary>
        ''' <param name="compareValue">比較する日付</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function LessEqual(ByVal compareValue As DateTime, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T) Implements ValidateOperation(Of T).LessEqual
            If Not TypeOf _accessor Is ControlAccessor(Of DateTime) Then
                Throw New InvalidOperationException("日付をもつコントロール以外は、日付と比較出来ません.")
            End If
            Dim v1 As New LessEqualDateValidator(_accessor, compareValue, New ValidatorMessage(_baseInfo, errorMessage))
            _baseValidator.Add(v1)
            Return Me
        End Function

        ''' <summary>
        ''' 指定値以下である事をチェックする
        ''' </summary>
        ''' <param name="compareValue">比較する値</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function LessEqual(ByVal compareValue As Object, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T) Implements ValidateOperation(Of T).LessEqual
            Dim v1 As New LessEqualValidator(Of T)(_accessor, compareValue, New ValidatorMessage(_baseInfo, errorMessage))
            _baseValidator.Add(v1)
            Numeric(errorMessage)
            Return Me
        End Function

        ''' <summary>
        ''' 値が指定範囲内である事をチェックする
        ''' </summary>
        ''' <param name="minValue">最小値</param>
        ''' <param name="maxValue">最大値</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function Range(ByVal minValue As Object, ByVal maxValue As Object, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T) Implements ValidateOperation(Of T).Range
            _baseValidator.Add(New RangeValidator(Of T)(_accessor, minValue, maxValue, New ValidatorMessage(_baseInfo, errorMessage)))
            Numeric(errorMessage)
            Return Me
        End Function

        ''' <summary>
        ''' 文字列長が、指定範囲内である事をチェックする
        ''' </summary>
        ''' <param name="minLengthB">最小文字列長</param>
        ''' <param name="maxLengthB">最大文字列長</param>
        ''' <param name="errorMessage">エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function RangeLengthByte(ByVal minLengthB As Integer, ByVal maxLengthB As Integer, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T) Implements ValidateOperation(Of T).RangeLengthByte
            Dim v1 As New RangeLengthByteValidator(Of T)(Accessor, minLengthB, maxLengthB, New ValidatorMessage(_baseInfo, errorMessage))
            _baseValidator.Add(v1)
            Return Me
        End Function

        ''' <summary>
        ''' 文字列長が、指定長以内である事をチェックする
        ''' </summary>
        ''' <param name="maxLengthB">最大文字列長</param>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function MaxLengthByte(ByVal maxLengthB As Integer, Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T) Implements ValidateOperation(Of T).MaxLengthByte
            Dim v1 As New MaxLengthByteValidator(Of T)(Accessor, maxLengthB, New ValidatorMessage(_baseInfo, errorMessage))
            _baseValidator.Add(v1)
            Return Me
        End Function

        ''' <summary>
        ''' 半角文字列のみであることをチェックする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function Hankaku(Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T) Implements ValidateOperation(Of T).Hankaku
            Dim v1 As New HankakuValidator(Of T)(Accessor, New ValidatorMessage(_baseInfo, errorMessage))
            _baseValidator.Add(v1)
            Return Me
        End Function


        ''' <summary>
        ''' 全角文字列のみであることをチェックする
        ''' </summary>
        ''' <param name="errorMessage">個別エラーメッセージ</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function Zenkaku(Optional ByVal errorMessage As String = Nothing) As ValidateOperation(Of T) Implements ValidateOperation(Of T).Zenkaku
            Dim v1 As New ZenkakuValidator(Of T)(Accessor, New ValidatorMessage(_baseInfo, errorMessage))
            _baseValidator.Add(v1)
            Return Me
        End Function

        ''' <summary>
        ''' 指定値のいずれかである事をチェックする
        ''' </summary>
        ''' <param name="params">指定値</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Function InArray(ByVal ParamArray params As T()) As ValidateOperation(Of T) Implements ValidateOperation(Of T).InArray
            Dim v1 As New InArrayValidator(Of T)(Accessor, New ValidatorMessage(_baseInfo, Nothing), params)
            _baseValidator.Add(v1)
            Return Me
        End Function

    End Class
End Namespace