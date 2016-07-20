Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl
    ''' <summary>
    ''' 必須入力チェックを担うクラス
    ''' </summary>
    ''' <typeparam name="T">コントロールのValueの型</typeparam>
    ''' <remarks></remarks>
    Friend Class RequiredValidator(Of T) : Inherits AbstractValidator(Of T)

        Private Const DEFAULT_MESSAGE As String = "{0} を入力して下さい."
        Private ReadOnly allowEmpty As Boolean

        Public Sub New(ByVal accessor As ControlAccessor(Of T), ByVal allowEmpty As Boolean, ByVal message As ValidatorMessage)
            MyBase.New(accessor, message)
            message.DefaultMessage = DEFAULT_MESSAGE
            Me.allowEmpty = allowEmpty
        End Sub

        Protected Overrides Function ValidateImpl(ByVal index As Integer) As Boolean
            If allowEmpty Then
                Return accessor.Value(index) IsNot Nothing
            End If
            Return Not StringUtil.IsEmpty(accessor.Value(index))
        End Function
    End Class
End Namespace