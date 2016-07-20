

Namespace Ui.Access.Impl
    Friend Class TextBoxAccessor : Inherits AbstractControlAccessor(Of String)
        Private ReadOnly aTextBox As TextBox
        Public Sub New(ByVal aTextBox As TextBox, ByVal name As String)
            MyBase.New(aTextBox, name)
            Me.aTextBox = aTextBox
        End Sub

        Public Overrides Function CompareTo(ByVal anotherAccessor As ControlAccessor(Of String), Optional ByVal index As Integer = -1) As Integer
            Return Value.CompareTo(anotherAccessor.Value)
        End Function

        Public Overrides ReadOnly Property Value(Optional ByVal index As Integer = -1) As String
            Get
                Return aTextBox.Text
            End Get
        End Property
    End Class
End Namespace