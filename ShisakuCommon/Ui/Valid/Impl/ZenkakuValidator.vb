
Imports System.Text
Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl

    ''' <summary>
    ''' 文字列に対する半角混入チェック（全角文字列のみでTRUE)
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks>
    '''半角を、Shift_JIS での1バイト文字と定義する
    '''全角を、Shift_JIS での2バイト文字と定義する
    '''文字数とバイト数が同じなら、1バイト文字のみ
    '''文字数の2倍とバイト数が同じなら、2バイト文字のみ
    '''どちらでもないなら混在
    ''' </remarks>                 
    Friend Class ZenkakuValidator(Of T) : Inherits AbstractValidator(Of T)


        Private Const DEFAULT_MESSAGE As String = "{0} は、全角文字を入力して下さい."

        Public Sub New(ByVal accessor As ControlAccessor(Of T), ByVal message As ValidatorMessage)

            MyBase.New(accessor, message)
            message.DefaultMessage = DEFAULT_MESSAGE

        End Sub

        Protected Overrides Function ValidateImpl(ByVal index As Integer) As Boolean

            Dim buf As T = accessor.Value(index)

            Dim byteLength As Integer

            '指定した文字配列をエンコードするために必要なバイト数を計算します
            byteLength = System.Text.Encoding.GetEncoding("shift-jis").GetByteCount(buf.ToString)

            '文字列長 *2 とバイト長が同じ場合には全角のみと判定できる
            If byteLength = (buf.ToString.Length * 2) Then
                Return True
            End If

            'それ以外の評価値はFALSE
            Return False

        End Function
    End Class

End Namespace