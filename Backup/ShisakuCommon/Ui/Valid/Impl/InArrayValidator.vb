
Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl

    Friend Class InArrayValidator(Of T) : Inherits AbstractValidator(Of T)

        Private Const DEFAULT_MESSAGE As String = "{0} は、{1} を入力して下さい."

        Private params As T()
        Public Sub New(ByVal accessor As ControlAccessor(Of T), ByVal message As ValidatorMessage, ByVal ParamArray params As T())

            MyBase.New(accessor, message)
            message.DefaultMessage = DEFAULT_MESSAGE

            Me.params = params

        End Sub

        Protected Overrides Function ErrorArg1() As String
            If params.Length = 1 Then
                Return params(0).ToString
            End If
            Dim objs As New List(Of Object)
            For Each param As T In params
                objs.Add(param)
            Next
            Return Join(objs.ToArray, ", ") & " のいずれか"
        End Function

        Protected Overrides Function ValidateImpl(ByVal index As Integer) As Boolean

            '比較する文字列を格納
            Dim value As T = accessor.Value(index)

            If StringUtil.IsEmpty(value) Then
                Return True ' 未入力は無視(正常)
            End If

            For Each param As T In params
                If param.Equals(value) Then
                    Return True
                End If
            Next

            Return False
        End Function

    End Class

End Namespace
