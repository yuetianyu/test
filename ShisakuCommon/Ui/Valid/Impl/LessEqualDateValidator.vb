Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl
    ''' <summary>
    ''' 日付の大小比較チェックを担うクラス
    ''' </summary>
    ''' <remarks>入力した日付 <![CDATA[<=]]> 指定の日付. であることをチェックする</remarks>
    Friend Class LessEqualDateValidator : Inherits AbstractValidator(Of DateTime)

        Private Const DEFAULT_MESSAGE As String = "{0} は、{1} と同じか、それより古い日付を入力して下さい."
        Private compareValue As DateTime

        Public Sub New(ByVal accessor As ControlAccessor(Of DateTime), ByVal compareValue As DateTime, ByVal message As ValidatorMessage)
            MyBase.New(accessor, message)
            message.DefaultMessage = DEFAULT_MESSAGE
            Me.compareValue = compareValue
        End Sub

        Protected Overrides Function ErrorArg1() As String
            Return compareValue.ToString("yyyy/MM/dd")
        End Function

        Protected Overrides Function ValidateImpl(ByVal index As Integer) As Boolean
            Return accessor.Value(index) <= compareValue
        End Function


    End Class
End Namespace