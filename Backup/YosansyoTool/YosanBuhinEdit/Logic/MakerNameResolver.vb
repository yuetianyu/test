Namespace YosanBuhinEdit.Logic
    ''' <summary>
    ''' 取引先名を解決することを担う interface
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface MakerNameResolver
        ''' <summary>
        ''' 取引先名称を返す
        ''' </summary>
        ''' <param name="makerCode">取引先コード</param>
        ''' <returns>取引先名称</returns>
        ''' <remarks></remarks>
        Function Resolve(ByVal makerCode As String) As String
    End Interface
End Namespace