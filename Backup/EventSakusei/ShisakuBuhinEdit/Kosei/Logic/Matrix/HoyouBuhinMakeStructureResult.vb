Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Merge

Namespace ShisakuBuhinEdit.Kosei.Logic.Matrix
    ''' <summary>
    ''' 「構成の情報」で部品表を作成するメソッドクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface HoyouBuhinMakeStructureResult

        '2012/01/16 引数追加

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Function Compute(ByVal aStructureResult As HoyouBuhinStructureResult, _
                         ByVal baseLevel As Integer?, _
                         ByVal kaihatsuFugo As String) As HoyouBuhinBuhinKoseiMatrix

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する(自給品有り)
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <param name="a0553Flag">どの操作から来たのか 0:設計展開,1:構成再展開、最新化、部品構成呼び出し、2:子部品呼び出し</param>
        ''' <param name="baseLevel">基点のレベル</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Function Compute(ByVal aStructureResult As HoyouBuhinStructureResult, _
                         ByVal a0553Flag As Integer, _
                         ByVal baseLevel As Integer?, _
                         ByVal kaihatsuFugo As String) As HoyouBuhinBuhinKoseiMatrix

        '''' <summary>
        '''' 「構成の情報」を元に部品表を作成する(構成再展開、最新化、一括構成呼び出し時)
        '''' </summary>
        '''' <param name="aStructureResult">構成の情報</param>
        '''' <param name="a0553Flag">どの操作から来たのか 0:設計展開,1:構成再展開、最新化、部品構成呼び出し、2:子部品呼び出し</param>
        '''' <param name="baseLevel">基点のレベル</param>
        '''' <returns>部品表</returns>
        '''' <remarks></remarks>
        'Function ComputeKosei(ByVal aStructureResult As HoyouBuhinStructureResult, ByVal a0553Flag As Integer, ByVal baseLevel As Integer?) As HoyouBuhinBuhinKoseiMatrix

        Function Compute2(ByVal aStructureResult As HoyouBuhinStructureResult, ByVal a0553Flag As Integer, ByVal baseLevel As Integer?) As HoyouBuhinBuhinKoseiMatrix

        Sub KaihatsuFugoSet(ByVal KaihatsuFugo As String)

        ''' <summary>
        ''' INSTLが入力されたときに頭だけ取得する
        ''' </summary>
        ''' <param name="aStructureResult"></param>
        ''' <param name="a0553Flag"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetBuhinKosei(ByVal aStructureResult As HoyouBuhinStructureResult, ByVal a0553Flag As Integer) As HoyouBuhinBuhinKoseiMatrix

    End Interface
End NameSpace