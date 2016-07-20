Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl
    ''' <summary>
    ''' 文字列指定範囲内評価を担うクラス
    ''' </summary>
    ''' <typeparam name="T">コントロールのValueの型</typeparam>
    ''' <remarks>入力した最小値・最大値に対する、値の範囲内チェックを行う</remarks>
    Friend Class RangeLengthByteValidator(Of T) : Inherits AbstractValidator(Of T)

        Private Const DEFAULT_MESSAGE As String = "{0} は半角文字で {1} 文字以上、{2} 文字以下です"

        Private ReadOnly minLengthB As Integer
        Private ReadOnly maxLengthB As Integer

        Public Sub New(ByVal accessor As ControlAccessor(Of T), _
                       ByVal minLengthB As Integer, _
                       ByVal maxLengthB As Integer, _
                       ByVal message As ValidatorMessage)
            MyBase.New(accessor, message)
            message.DefaultMessage = DEFAULT_MESSAGE
            Me.minLengthB = minLengthB
            Me.maxLengthB = maxLengthB
        End Sub

        Protected Overrides Function ErrorArg1() As String
            Return minLengthB.ToString
        End Function

        Protected Overrides Function ErrorArg2() As String
            Return maxLengthB.ToString
        End Function

        Protected Overrides Function ValidateImpl(ByVal index As Integer) As Boolean

            Dim buf As T = accessor.Value(index)

            Dim byteLength As Integer
            If buf Is Nothing Then
                byteLength = 0
            Else
                '指定した文字配列をエンコードするために必要なバイト数を計算します
                byteLength = System.Text.Encoding.GetEncoding("shift-jis").GetByteCount(buf.ToString)
            End If

            '指定最小桁に満たない場合は終わり
            If byteLength < Me.minLengthB Then
                Return False
            End If

            '指定最大桁より長い場合は終わり
            If Me.maxLengthB < byteLength Then
                Return False
            End If

            '範囲内はOK
            Return True

        End Function
    End Class

End Namespace
