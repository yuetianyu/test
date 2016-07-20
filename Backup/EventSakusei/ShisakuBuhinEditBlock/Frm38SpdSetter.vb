Imports ShisakuCommon.Util

Namespace ShisakuBuhinEditBlock
    Public Interface Frm38SpdSetter(Of T As Observable)
        ''' <summary>
        ''' Subjectを差し替える
        ''' </summary>
        ''' <param name="subject">Subject</param>
        ''' <remarks></remarks>
        Sub SupersedeSubject(ByVal subject As T)
    End Interface
End Namespace