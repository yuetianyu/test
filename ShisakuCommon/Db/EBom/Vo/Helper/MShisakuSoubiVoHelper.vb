Namespace Db.EBom.Vo.Helper
    Public Class MShisakuSoubiVoHelper
        ''' <summary>試作装備区分</summary>
        Public Class ShisakuSoubiKbn
            ''' <summary>基本装備仕様</summary>
            Public Const BASIC_OPTION As String = "1"
            ''' <summary>試作車特別装備</summary>
            Public Const SPECIAL_OPTION As String = "2"
        End Class

        Private vo As MShisakuSoubiVo
        Public Sub New(ByVal vo As MShisakuSoubiVo)
            Me.vo = vo
        End Sub
    End Class
End Namespace