Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl
    ''' <summary>
    ''' 文字列長評価を担うクラス
    ''' </summary>
    ''' <typeparam name="T">コントロールのValueの型</typeparam>
    ''' <remarks>入力した文字列長の最大長チェックを行う</remarks>
    Friend Class MaxLengthByteValidator(Of T) : Inherits AbstractValidator(Of T)

        Private Const DEFAULT_MESSAGE As String = "{0} は半角文字で {1} 文字以内です"

        Private ReadOnly zeroToMax As RangeLengthByteValidator(Of T)
        Private ReadOnly maxLengthB As Integer

        Public Sub New(ByVal accessor As ControlAccessor(Of T), _
                       ByVal maxLengthB As Integer, _
                       ByVal message As ValidatorMessage)
            MyBase.New(accessor, message)
            message.DefaultMessage = DEFAULT_MESSAGE
            Me.maxLengthB = maxLengthB
            Dim dummy As New ValidatorMessage
            zeroToMax = New RangeLengthByteValidator(Of T)(accessor, 0, maxLengthB, dummy)
        End Sub

        Protected Overrides Function ErrorArg1() As String
            Return maxLengthB.ToString
        End Function

        Protected Overrides Function ValidateImpl(ByVal index As Integer) As Boolean
            Return zeroToMax.Validate(index)
        End Function

    End Class
End Namespace