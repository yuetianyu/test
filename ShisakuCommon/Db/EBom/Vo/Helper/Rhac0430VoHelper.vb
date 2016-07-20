Namespace Db.EBom.Vo.Helper
    Public Class Rhac0430VoHelper
        Private vo As Rhac0430Vo

        ''' <summary>内外装区分</summary>
        Public Class NaigaisoKbn
            ''' <summary>内装</summary>
            Public Const Naiso As String = "0"
            ''' <summary>外装</summary>
            Public Const Gaiso As String = "1"
        End Class

        Public Sub New(ByVal vo As Rhac0430Vo)
            Me.vo = vo
        End Sub
    End Class
End Namespace