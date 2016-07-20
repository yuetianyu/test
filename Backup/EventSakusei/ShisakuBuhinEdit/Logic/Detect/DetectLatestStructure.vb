Namespace ShisakuBuhinEdit.Logic.Detect
    ''' <summary>
    ''' 最新の構成情報のありか探索するメソッドクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface DetectLatestStructure
        ''' <summary>
        ''' 部品番号を構成する、最新の構成の情報を返す
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="buhinNoKbn">区分</param>
        ''' <returns>構成の情報</returns>
        ''' <remarks></remarks>
        Function Compute(ByVal buhinNo As String, ByVal buhinNoKbn As String, ByVal IsInstlHinban As Boolean) As StructureResult

        ''' <summary>
        ''' 部品番号を構成する、最新の構成の情報を返す
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="buhinNoKbn">区分</param>
        ''' <returns>構成の情報</returns>
        ''' <remarks></remarks>
        Function ComputeEK(ByVal buhinNo As String, ByVal buhinNoKbn As String, Optional ByVal KaihatsuFugo As String = "", Optional ByVal shisakuEventCode As String = "") As StructureResult

        ''' <summary>
        ''' 部品番号を構成する情報を返す
        ''' </summary>
        ''' <param name="buhinNo">部品</param>
        ''' <param name="buhinNoKbn">試作区分</param>
        ''' <param name="IsInstlHinban">INSTL品番か</param>
        ''' <param name="targetTable"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Compute(ByVal buhinNo As String, ByVal buhinNoKbn As String, ByVal IsInstlHinban As Boolean, ByVal targetTable As String, Optional ByVal KaihatsuFugo As String = "", Optional ByVal shisakuEventCode As String = "") As StructureResult

    End Interface
End Namespace