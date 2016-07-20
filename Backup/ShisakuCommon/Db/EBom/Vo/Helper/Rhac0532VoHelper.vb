Namespace Db.EBom.Vo.Helper
    Public Class Rhac0532VoHelper

        ''' <summary>廃止年月日</summary>
        Public Class HaisiDate
            ''' <summary>現在、無期限で有効な場合の廃止年月日</summary>
            Public Const NOW_EFFECTIVE_DATE As Integer = 99999999
        End Class

        Private vo As Rhac0532Vo
        Public Sub New(ByVal vo As Rhac0532Vo)
            Me.vo = vo
        End Sub
    End Class
End Namespace