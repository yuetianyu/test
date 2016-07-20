Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl
    ''' <summary>
    ''' 入力チェック機能の基底クラス
    ''' </summary>
    ''' <typeparam name="T">コントロールのValueの型</typeparam>
    ''' <remarks></remarks>
    Friend MustInherit Class AbstractValidator(Of T) : Implements IValidator
        Protected ReadOnly accessor As ControlAccessor(Of T)
        Private ReadOnly message As ValidatorMessage
        Protected _errorMessage As String
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="accessor">チェック対象のWindowsコントロールアクセッサ</param>
        ''' <param name="message">Validateメッセージ</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal accessor As ControlAccessor(Of T), ByVal message As ValidatorMessage)
            Me.accessor = accessor
            Me.message = message
        End Sub

        ''' <summary>
        ''' 入力チェック処理の本体
        ''' </summary>
        ''' <param name="index">値のindex</param>
        ''' <returns>正常なら、true</returns>
        ''' <remarks></remarks>
        Protected MustOverride Function ValidateImpl(ByVal index As Integer) As Boolean

        Public Function Validate(ByVal ParamArray indexes As Integer()) As Boolean Implements IValidator.Validate
            Dim index As Integer
            If 0 < indexes.Length Then
                index = indexes(0)
            Else
                index = -1
            End If
            _errorMessage = Nothing
            If Not ValidateImpl(index) Then
                If ErrorArg0() Is Nothing Then
                    _errorMessage = message.CurrentMessage
                ElseIf ErrorArg1() Is Nothing Then
                    _errorMessage = String.Format(message.CurrentMessage, ErrorArg0)
                ElseIf ErrorArg2() Is Nothing Then
                    _errorMessage = String.Format(message.CurrentMessage, ErrorArg0, ErrorArg1)
                Else
                    _errorMessage = String.Format(message.CurrentMessage, ErrorArg0, ErrorArg1, ErrorArg2)
                End If
                Return False
            End If
            Return True
        End Function

        ''' <summary>
        ''' エラーメッセージ中の {0} 埋め込みパラメータ（初期値はコントロールの名称）
        ''' </summary>
        ''' <returns>{0} に埋め込む値</returns>
        ''' <remarks></remarks>
        Protected Overridable Function ErrorArg0() As String
            Return accessor.Name
        End Function
        ''' <summary>
        ''' エラーメッセージ中の {1} 埋め込みパラメータ
        ''' </summary>
        ''' <returns>{1} に埋め込む値</returns>
        ''' <remarks></remarks>
        Protected Overridable Function ErrorArg1() As String
            Return Nothing
        End Function
        ''' <summary>
        ''' エラーメッセージ中の {2} 埋め込みパラメータ
        ''' </summary>
        ''' <returns>{2} に埋め込む値</returns>
        ''' <remarks></remarks>
        Protected Overridable Function ErrorArg2() As String
            Return Nothing
        End Function

        Public ReadOnly Property ErrorControls() As ErrorControl() Implements IValidator.ErrorControls
            Get
                Return New ErrorControl() {accessor.NewErrorControl}
            End Get
        End Property

        Public ReadOnly Property ErrorMessage() As String Implements IValidator.ErrorMessage
            Get
                Return _errorMessage
            End Get
        End Property

    End Class
End Namespace