Namespace Db.EBom.Vo.Helper
    Public Class Rhac1560VoHelper

        ''' <summary>サイト区分</summary>
        Public Class SiteKbn
            ''' <summary>三鷹</summary>
            Public Const Mitaka As String = "2"
            ''' <summary>群馬</summary>
            Public Const Gumma As String = "1"
        End Class
        Private ReadOnly vo As Rhac1560Vo
        Public Sub New(ByVal vo As Rhac1560Vo)
            Me.vo = vo
        End Sub
    End Class
End Namespace