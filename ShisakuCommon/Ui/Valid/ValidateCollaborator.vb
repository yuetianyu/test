Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid
    Public Interface ValidateCollaborator
        Function FromTo(Of T)(ByVal accessorA As ControlAccessor(Of T), ByVal accessorB As ControlAccessor(Of T), ByVal errorMessage As String) As ValidateCollaborator
    End Interface
End Namespace