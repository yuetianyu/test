Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl
    ''' <summary>
    ''' 入力禁止の入力チェックを担うクラス
    ''' </summary>
    ''' <typeparam name="T">コントロールのValueの型</typeparam>
    ''' <remarks></remarks>
    Friend Class EmptyValidator(Of T) : Inherits AbstractValidator(Of T)

        Private Const DEFAULT_MESSAGE As String = "{0} は、入力出来ません."
        Private ReadOnly nullOnly As Boolean

        Public Sub New(ByVal accessor As ControlAccessor(Of T), ByVal nullOnly As Boolean, ByVal message As ValidatorMessage)
            MyBase.New(accessor, message)
            message.DefaultMessage = DEFAULT_MESSAGE
            Me.nullOnly = nullOnly
        End Sub

        Protected Overrides Function ValidateImpl(ByVal index As Integer) As Boolean
            If nullOnly Then
                Return accessor.Value(index) Is Nothing
            End If
            Return StringUtil.IsEmpty(accessor.Value(index))
        End Function
    End Class
End Namespace