

Namespace Ui.Access.Impl
    Public MustInherit Class AbstractControlAccessor(Of T) : Implements ControlAccessor(Of T)

        Private ReadOnly _control As Control
        Private ReadOnly _name As String
        Public Sub New(ByVal aControl As Control, ByVal name As String)
            Me._control = aControl
            Me._name = name
        End Sub

        Public Overridable ReadOnly Property Name() As String Implements ControlAccessor(Of T).Name
            Get
                Return _name
            End Get
        End Property

        Public Overridable ReadOnly Property Control() As Control Implements ControlAccessor(Of T).Control
            Get
                Return _control
            End Get
        End Property

        Public Overridable Function NewErrorControl() As ErrorControl Implements ControlAccessor(Of T).NewErrorControl
            Return New ErrorControlImpl(Control)
        End Function

        Public MustOverride Function CompareTo(ByVal anotherAccessor As ControlAccessor(Of T), Optional ByVal index As Integer = -1) As Integer Implements ControlAccessor(Of T).CompareTo

        Public MustOverride ReadOnly Property Value(Optional ByVal index As Integer = -1) As T Implements ControlAccessor(Of T).Value
    End Class
End Namespace