Namespace Db.EBom.Vo.Helper
    Public Class TShisakuBuhinVoHelper

        ''' <summary>試作ブロック№改訂№</summary>
        Public Class ShisakuBlockNoKaiteiNo
            ''' <summary>初期値</summary>
            Public Const DEFAULT_VALUE As String = "000"
        End Class

        Private vo As TShisakuBuhinVo
        Public Sub New(ByVal vo As TShisakuBuhinVo)
            Me.vo = vo
        End Sub
    End Class
End Namespace