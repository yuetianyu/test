Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl
    Public Class ValidateCollaboratorImpl : implements ValidateCollaborator
        Private ReadOnly _aValidator As BaseValidator
        Public Sub New(ByVal _aValidator As BaseValidator)
            Me._aValidator = _aValidator
        End Sub
        Public Function FromTo(ByVal fromAccessor As ControlAccessor(Of DateTime), _
                               ByVal toAccessor As ControlAccessor(Of DateTime), _
                               ByVal errorMessage As String) As ValidateCollaborator
            Return FromTo(Of DateTime)(fromAccessor, toAccessor, errorMessage)
        End Function
        Public Function FromTo(Of T)(ByVal fromAccessor As ControlAccessor(Of T), _
                                     ByVal toAccessor As ControlAccessor(Of T), _
                                     ByVal errorMessage As String) As ValidateCollaborator Implements ValidateCollaborator.FromTo
            Dim v As New FromToValidator(Of T)(fromAccessor, toAccessor, errorMessage)
            _aValidator.Add(v)
            Return Me
        End Function
    End Class
End Namespace