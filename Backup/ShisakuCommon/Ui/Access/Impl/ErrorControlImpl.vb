Namespace Ui.Access.Impl
    Public Class ErrorControlImpl : Implements ErrorControl
        Private ReadOnly aControl As Control
        Public Sub New(ByVal aControl As Control)
            Me.aControl = aControl
        End Sub

        Public Sub Focus() Implements ErrorControl.Focus
            aControl.Focus()
        End Sub

        Public Sub SetBackColorOnError() Implements ErrorControl.SetBackColorOnError
            ShisakuFormUtil.onErro(aControl)
        End Sub

        Public Sub ClearBackColor() Implements ErrorControl.ClearBackColor
            ShisakuFormUtil.initlColor(aControl)
        End Sub
        Public Sub setBackColorOnWarning() Implements ErrorControl.SetBackColorOnWarning
            ShisakuFormUtil.onWarning(aControl)
        End Sub

    End Class
End Namespace