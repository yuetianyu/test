Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic

Namespace ShisakuBuhinEdit.Logic

    ''' <summary>
    ''' 部品表画面・A/L画面に公開する抜け穴実装クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinEzTunnelAlKoseiImpl

        Private koseiSubject As HoyouBuhinBuhinEditKoseiSubject

        Private Initializing As Boolean
        ''' <summary>
        ''' 初期設定する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Initialize(ByVal koseiSubject As HoyouBuhinBuhinEditKoseiSubject)
            Me.koseiSubject = koseiSubject
        End Sub

    End Class
End Namespace