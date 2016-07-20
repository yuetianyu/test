

Namespace Ui.Access.Impl
    Friend Class ComboBoxAccessor : Inherits AbstractControlAccessor(Of String)
        Private ReadOnly aComboBox As ComboBox
        Public Sub New(ByVal aComboBox As ComboBox, ByVal name As String)
            MyBase.New(aComboBox, name)
            Me.aComboBox = aComboBox
        End Sub

        Public Overrides Function CompareTo(ByVal anotherAccessor As ControlAccessor(Of String), Optional ByVal index As Integer = -1) As Integer
            Return Value(index).CompareTo(anotherAccessor.Value(index))
        End Function

        Public Overrides ReadOnly Property Value(Optional ByVal index As Integer = -1) As String
            Get
                Return aComboBox.SelectedValue
            End Get
        End Property
    End Class
End Namespace