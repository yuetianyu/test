Imports YosansyoTool.YosanBuhinEdit.Logic.Detect

Namespace YosanBuhinEdit.Kosei.Logic.Matrix
    ''' <summary>
    ''' 「構成の情報」で部品表を作成するメソッドクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface MakeStructureResult

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Function Compute(ByVal aStructureResult As StructureResult, _
                         ByVal baseLevel As Integer?, _
                         ByVal kaihatsuFugo As String) As BuhinKoseiMatrix

        ''' <summary>
        ''' 「構成の情報」を元に部品表を作成する(自給品有り)
        ''' </summary>
        ''' <param name="aStructureResult">構成の情報</param>
        ''' <param name="a0553Flag">どの操作から来たのか 0:設計展開,1:構成再展開、最新化、部品構成呼び出し、2:子部品呼び出し</param>
        ''' <param name="baseLevel">基点のレベル</param>
        ''' <returns>部品表</returns>
        ''' <remarks></remarks>
        Function Compute(ByVal aStructureResult As StructureResult, _
                         ByVal a0553Flag As Integer, _
                         ByVal baseLevel As Integer?, _
                         ByVal kaihatsuFugo As String) As BuhinKoseiMatrix

        Function Compute2(ByVal aStructureResult As StructureResult, ByVal a0553Flag As Integer, ByVal baseLevel As Integer?) As BuhinKoseiMatrix

        Sub KaihatsuFugoSet(ByVal KaihatsuFugo As String)

        ''' <summary>
        ''' INSTLが入力されたときに頭だけ取得する
        ''' </summary>
        ''' <param name="aStructureResult"></param>
        ''' <param name="a0553Flag"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetBuhinKosei(ByVal aStructureResult As StructureResult, ByVal a0553Flag As Integer) As BuhinKoseiMatrix

    End Interface
End Namespace