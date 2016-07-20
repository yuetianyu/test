Namespace Db.EBom.Vo.Helper
    Public Class TShisakuEventSoubiVoHelper
        ''' <summary>表示順</summary>
        Public Class HyojijunNo
            ''' <summary>タイトル欄</summary>
            Public Const TITLE As Integer = -1
        End Class
        ''' <summary>試作装備区分</summary>
        Public Class ShisakuSoubiKbn
            ''' <summary>基本装備仕様</summary>
            Public Const BASIC_OPTION As String = "1"
            ''' <summary>試作車特別装備</summary>
            Public Const SPECIAL_OPTION As String = "2"
        End Class

        Private vo As TShisakuEventSoubiVo
        Public Sub New(ByVal vo As TShisakuEventSoubiVo)
            Me.vo = vo
        End Sub
    End Class
End Namespace