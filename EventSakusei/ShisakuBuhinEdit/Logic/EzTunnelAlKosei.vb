Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect

Namespace ShisakuBuhinEdit.Logic
    ''' <summary>
    ''' 部品表画面・A/L画面に公開する抜け穴interface
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface EzTunnelAlKosei
        ''' <summary>
        ''' INSTL列の「構成の情報」を返す
        ''' </summary>
        ''' <param name="columnIndex">INSTL列index</param>
        ''' <returns>構成の情報</returns>
        ''' <remarks></remarks>
        Function GetStructureResult(ByVal columnIndex) As StructureResult
    End Interface
End NameSpace