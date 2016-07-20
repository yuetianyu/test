Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports EventSakusei.ShisakuBuhinEdit.Al.Logic

Namespace ShisakuBuhinEdit.Logic

    ''' <summary>
    ''' 部品表画面・A/L画面に公開する抜け穴実装クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EzTunnelAlKoseiImpl
        Implements EzTunnelAlKosei

        Private alSubject As BuhinEditAlSubject
        Private koseiSubject As BuhinEditKoseiSubject

        Private Initializing As Boolean
        ''' <summary>
        ''' 初期設定する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Initialize(ByVal alSubject As BuhinEditAlSubject, ByVal koseiSubject As BuhinEditKoseiSubject)
            Me.alSubject = alSubject
            Me.koseiSubject = koseiSubject
        End Sub

        ''' <summary>
        ''' 構成の情報を返す
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>構成の情報</returns>
        ''' <remarks></remarks>
        Public Function GetStructureResult(ByVal columnIndex) As StructureResult Implements EzTunnelAlKosei.GetStructureResult
            If alSubject.StructureResult(columnIndex) Is Nothing Then
                alSubject.DetectStructureResult(columnIndex)
            End If
            Return alSubject.StructureResult(columnIndex)
        End Function

    End Class
End Namespace