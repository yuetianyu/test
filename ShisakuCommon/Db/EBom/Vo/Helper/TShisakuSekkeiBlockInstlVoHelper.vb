Namespace Db.EBom.Vo.Helper
    Public Class TShisakuSekkeiBlockInstlVoHelper

        ''' <summary>試作ブロック№改訂№</summary>
        Public Class ShisakuBlockNoKaiteiNo
            ''' <summary>初期値</summary>
            Public Const DEFAULT_VALUE As String = "000"
        End Class
        ''' <summary>INSTL品番表示順</summary>
        Public Class InstlHinbanHyoujiJun
            ''' <summary>開始値</summary>
            Public Const START_VALUE As Integer = 0
        End Class
        Private vo As TShisakuSekkeiBlockInstlVo
        Public Sub New(ByVal vo As TShisakuSekkeiBlockInstlVo)
            Me.vo = vo
        End Sub
    End Class
End Namespace