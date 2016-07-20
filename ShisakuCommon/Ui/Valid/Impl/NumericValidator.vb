Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl
    ''' <summary>
    ''' 数値チェックを担うクラス
    ''' </summary>
    ''' <typeparam name="T">コントロールのValueの型</typeparam>
    ''' <remarks></remarks>
    Friend Class NumericValidator(Of T) : Inherits AbstractValidator(Of T)

        Private Const DEFAULT_MESSAGE As String = "{0} は、数値を入力して下さい."

        Public Sub New(ByVal accessor As ControlAccessor(Of T), ByVal message As ValidatorMessage)
            MyBase.New(accessor, message)
            message.DefaultMessage = DEFAULT_MESSAGE
        End Sub

        Protected Overrides Function ValidateImpl(ByVal index As Integer) As Boolean
            If Not StringUtil.IsEmpty(accessor.Value(index)) AndAlso Not IsNumeric(accessor.Value(index)) Then
                Return False
            End If
            Return True
        End Function
    End Class
End Namespace