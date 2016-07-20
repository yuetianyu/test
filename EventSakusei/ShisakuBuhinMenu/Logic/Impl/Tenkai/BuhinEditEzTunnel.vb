Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports EventSakusei.ShisakuBuhinMenu.Logic.Impl.Tenkai
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect

''' <summary>
''' 部品表画面・A/L画面に公開する抜け穴実装クラス
''' </summary>
''' <remarks></remarks>
Public Class BuhinEditEzTunnel
    Implements EzTunnelAlKosei

    Private alSubject As BuhinEditInstlSupplier
    Private koseiSubject As BuhinEditBaseSupplier

    Private Initializing As Boolean
    ''' <summary>
    ''' 初期設定する
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Initialize(ByVal alSubject As BuhinEditInstlSupplier, ByVal koseiSubject As BuhinEditBaseSupplier)
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
