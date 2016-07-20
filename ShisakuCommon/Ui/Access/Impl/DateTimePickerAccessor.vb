

Namespace Ui.Access.Impl
    Friend Class DateTimePickerAccessor : Inherits AbstractControlAccessor(Of DateTime)
        Private ReadOnly aDateTimePicker As DateTimePicker
        Public Sub New(ByVal aDateTimePicker As DateTimePicker, ByVal name As String)
            MyBase.New(aDateTimePicker, name)
            Me.aDateTimePicker = aDateTimePicker
        End Sub

        Public Overrides Function CompareTo(ByVal anotherAccessor As ControlAccessor(Of Date), Optional ByVal index As Integer = -1) As Integer
            Return Value(index).CompareTo(anotherAccessor.Value(index))
        End Function

        Public Overrides ReadOnly Property Value(Optional ByVal index As Integer = -1) As DateTime
            Get
                Return aDateTimePicker.Value
            End Get
        End Property
    End Class
End Namespace