Namespace Db.EBom.Vo.Helper
    Public Class TShisakuBuhinKouseiVoHelper
        ''' <summary>作成回数</summary>
        Public Class SakuseiCount
            ''' <summary>初期値</summary>
            Public Const DEFAULT_VALUE As Integer = 1
        End Class

        ''' <summary>試作ブロック№改訂№</summary>
        Public Class ShisakuBlockNoKaiteiNo
            ''' <summary>初期値</summary>
            Public Const DEFAULT_VALUE As String = "000"
        End Class

        ''' <summary>差戻日</summary>
        Public Class SashimodoshiDate
            ''' <summary>初期値</summary>
            Public Const DEFAULT_VALUE As Integer = 0
        End Class

        ''' <summary>廃止年月日</summary>
        Public Class HaisiDate
            ''' <summary>現在、無期限で有効な場合の廃止年月日</summary>
            Public Const NOW_EFFECTIVE_DATE As Integer = 99999999
        End Class

        Dim vo As TShisakuBuhinKouseiVo
        Public Sub New(ByVal vo As TShisakuBuhinKouseiVo)
            Me.vo = vo
        End Sub
    End Class
End Namespace