Imports FarPoint.Win.Spread

Namespace Ui.Access.Impl
    Public Class SpreadTextCellAccessor : Inherits AbstractSpreadControlAccessor(Of String)
        Public Sub New(ByVal spread As FpSpread, ByVal columnTag As String, ByVal name As String)
            MyBase.new(spread, columnTag, name)
        End Sub

        Public Overrides Function CompareTo(ByVal anotherAccessor As ControlAccessor(Of String), Optional ByVal index As Integer = -1) As Integer
            Return Value(index).CompareTo(anotherAccessor.Value(index))
        End Function

    End Class
End Namespace