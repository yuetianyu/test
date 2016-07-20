Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl
    Friend Class FromToValidator(Of T) : Implements IValidator
        Private Const DEFAULT_MESSAGE As String = "{1} は {0} より大きい値を指定して下さい."
        Private ReadOnly fromAccessor As ControlAccessor(Of T)
        Private ReadOnly toAccessor As ControlAccessor(Of T)
        Private ReadOnly message As String
        Private _errorMessage As String
        Public Sub New(ByVal fromAccessor As ControlAccessor(Of T), _
                       ByVal toAccessor As ControlAccessor(Of T), _
                       Optional ByVal message As String = DEFAULT_MESSAGE)
            Me.fromAccessor = fromAccessor
            Me.toAccessor = toAccessor
            Me.message = message
        End Sub

        Public Function Validate(ByVal ParamArray indexes As Integer()) As Boolean Implements IValidator.Validate
            _errorMessage = Nothing
            If toAccessor.CompareTo(fromAccessor) < 0 Then
                _errorMessage = String.Format(message, fromAccessor.Name, toAccessor.Name)
                Return False
            End If
            Return True
        End Function

        Public ReadOnly Property ErrorControls() As ErrorControl() Implements IValidator.ErrorControls
            Get
                Return New ErrorControl() {fromAccessor.NewErrorControl, toAccessor.NewErrorControl}
            End Get
        End Property

        Public ReadOnly Property ErrorMessage() As String Implements IValidator.ErrorMessage
            Get
                Return _errorMessage
            End Get
        End Property


    End Class
End Namespace