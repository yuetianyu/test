Namespace Db.EBom.Vo.Helper
    Public Class TShisakuBuhinEditVoHelper

        ''' <summary>国内集計コード</summary>
        Public Class ShukeiCode
            ''' <summary>任意管理単位</summary>
            Public Const NINI_KANRI_TANI As String = "X"
            ''' <summary>納入部品,内製部品</summary>
            Public Const NAISEI_BUHIN As String = "A"
            ''' <summary>支給部品</summary>
            Public Const SHIKYU_BUHIN_E As String = "E"
            ''' <summary>支給部品</summary>
            Public Const SHIKYU_BUHIN_Y As String = "Y"
            ''' <summary>参考呼出部品</summary>
            Public Const REFERENCE_BUHIN As String = "R"
            ''' <summary>自給部品</summary>
            Public Const JIKYU_BUHIN As String = "J"
            ''' <summary>全て</summary>
            Public Shared ReadOnly ALL As String() = {NINI_KANRI_TANI, NAISEI_BUHIN, SHIKYU_BUHIN_E, SHIKYU_BUHIN_Y, REFERENCE_BUHIN, JIKYU_BUHIN}
        End Class
        ''' <summary>海外SIA集計コード</summary>
        Public Class SiaShukeiCode : Inherits ShukeiCode
        End Class
        ''' <summary>現調区分</summary>
        Public Class GencyoCkdKbn
            ''' <summary>現地調達部品</summary>
            Public Const GENCHI_CHOTATSU_BUHIN As String = "G"
            ''' <summary>CKD部品</summary>
            Public Const CKD_BUHIN As String = "C"
            ''' <summary>全て</summary>
            Public Shared ReadOnly ALL As String() = {GENCHI_CHOTATSU_BUHIN, CKD_BUHIN}
        End Class
        ''' <summary>再使用不可</summary>
        Public Class Saishiyoufuka
            ''' <summary>再使用不可</summary>
            Public Const YES As String = "A"
        End Class

        Dim vo As TShisakuBuhinEditVo
        Public Sub New(ByVal vo As TShisakuBuhinEditVo)
            Me.vo = vo
        End Sub
    End Class
End Namespace