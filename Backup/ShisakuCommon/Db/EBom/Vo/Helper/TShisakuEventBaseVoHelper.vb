Namespace Db.EBom.Vo.Helper
    Public Class TShisakuEventBaseVoHelper
        ''' <summary>設計展開選択</summary>
        Public Class SekkeiTenkaiKbn
            ''' <summary>受領後</summary>
            Public Const JURYO_GO As String = "1"
            ''' <summary>受領前</summary>
            Public Const JURYO_MAE As String = "0"
        End Class
        ''' <summary>設計展開選択(名称)</summary>
        Public Class SekkeiTenkaiKbnName
            ''' <summary>受領後</summary>
            Public Const JURYO_GO As String = "受領後"
            ''' <summary>受領前</summary>
            Public Const JURYO_MAE As String = "受領前"
        End Class
        ''' <summary>仕向</summary>
        Public Class BaseShimuke
            ''' <summary>国内</summary>
            Public Const KOKUNAI As String = ""
        End Class
        ''' <summary>仕向(名称)</summary>
        Public Class BaseShimukeName
            ''' <summary>国内</summary>
            Public Const KOKUNAI As String = "国内"
        End Class
        Private ReadOnly vo As TShisakuEventBaseVo
        Public Sub New(ByVal vo As TShisakuEventBaseVo)
            Me.vo = vo
        End Sub

        ''' <summary>
        ''' 設計展開区分が受領後かを返す
        ''' </summary>
        ''' <returns>受領後ならtrue</returns>
        ''' <remarks></remarks>
        Public Function IsSekkeiTenkaiKbnJuryoGo() As Boolean
            Return SekkeiTenkaiKbn.JURYO_GO.Equals(vo.SekkeiTenkaiKbn)
        End Function
    End Class
End Namespace