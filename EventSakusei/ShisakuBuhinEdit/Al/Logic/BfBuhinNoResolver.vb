Namespace ShisakuBuhinEdit.Al.Logic
    ''' <summary>
    ''' 基本F品番を解決する
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface BfBuhinNoResolver
        ''' <summary>
        ''' INSTL品番を基本F品番へ解決する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <returns>基本F品番</returns>
        ''' <remarks></remarks>
        Function Resolve(ByVal shisakuEventCode As String, ByVal instlHinban As String, ByVal instlHinbanKbn As String, ByVal colorUmu As String) As String
    End Interface
End Namespace