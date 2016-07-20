Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Merge

Namespace ShisakuBuhinEdit.Kosei.Logic.Matrix
    ''' <summary>
    ''' 試作部品構成情報と試作部品情報とで部品表を作成するメソッドクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface MakeShisakuBlockKey

        ''' <summary>
        ''' 試作部品構成情報と試作部品情報とで部品表を作成する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Function Compute(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As BuhinKoseiMatrix
    End Interface
End Namespace