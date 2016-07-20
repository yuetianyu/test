
Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl
    ''' <summary>
    ''' 指定範囲内評価を担うクラス
    ''' </summary>
    ''' <typeparam name="T">コントロールのValueの型</typeparam>
    ''' <remarks>入力した最小値・最大値に対する、値の範囲内チェックを行う</remarks>
    Friend Class RangeValidator(Of T) : Inherits AbstractValidator(Of T)

        Private Const DEFAULT_MESSAGE As String = "{0} は {1} 以上、{2} 以下を入力して下さい."

        Private ReadOnly minValue As Object
        Private ReadOnly maxValue As Object
        Private ReadOnly le As LessEqualValidator(Of T)
        Private ReadOnly ge As GreaterEqualValidator(Of T)

        Public Sub New(ByVal accessor As ControlAccessor(Of T), _
                       ByVal minValue As Object, _
                       ByVal maxValue As Object, _
                       ByVal message As ValidatorMessage)
            MyBase.New(accessor, message)
            message.DefaultMessage = DEFAULT_MESSAGE
            Dim dummy As New ValidatorMessage
            le = New LessEqualValidator(Of T)(accessor, maxValue, dummy)
            ge = New GreaterEqualValidator(Of T)(accessor, minValue, dummy)
            Me.minValue = minValue
            Me.maxValue = maxValue
        End Sub

        Protected Overrides Function ErrorArg1() As String
            Return minValue.ToString
        End Function

        Protected Overrides Function ErrorArg2() As String
            Return maxValue.ToString
        End Function

        Protected Overrides Function ValidateImpl(ByVal index As Integer) As Boolean
            Return ge.Validate(index) AndAlso le.Validate(index)
        End Function
    End Class

End Namespace
