Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl
    ''' <summary>
    ''' 値の大小比較を担うクラス
    ''' </summary>
    ''' <typeparam name="T">コントロールのValueの型</typeparam>
    ''' <remarks>指定の値 <![CDATA[<=]]> 入力した値 . であることをチェックする</remarks>
    Friend Class GreaterEqualValidator(Of T) : Inherits AbstractValidator(Of T)

        Private Const DEFAULT_MESSAGE As String = "{0} は、{1} と同じか、それより大きい値を入力して下さい."
        Private compareValue As Object

        Public Sub New(ByVal accessor As ControlAccessor(Of T), ByVal compareValue As Object, ByVal message As ValidatorMessage)
            MyBase.New(accessor, message)
            message.DefaultMessage = DEFAULT_MESSAGE
            Me.compareValue = compareValue
        End Sub

        Protected Overrides Function ErrorArg1() As String
            Return compareValue.ToString
        End Function

        Protected Overrides Function ValidateImpl(ByVal index As Integer) As Boolean
            Dim value As T = accessor.Value(index)
            If StringUtil.IsEmpty(value) Then
                Return True ' 未入力は評価しない
            End If
            If Not IsNumeric(value) Then
                Return True ' 数値以外も評価しない
            End If
            If TypeOf compareValue Is Int32 Then
                Return Convert.ToInt32(value) >= Convert.ToInt32(compareValue)
            ElseIf TypeOf compareValue Is Int64 Then
                Return Convert.ToInt64(value) >= Convert.ToInt64(compareValue)
            ElseIf TypeOf compareValue Is Int16 Then
                Return Convert.ToInt16(value) >= Convert.ToInt16(compareValue)
            End If
            Throw New NotSupportedException(compareValue.GetType.Name & " は未対応です.")
        End Function
    End Class
End Namespace