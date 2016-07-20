Namespace ShisakuBuhinMenu.Logic
    Public Interface Command
        ''' <summary>
        ''' 処理を実行する
        ''' </summary>
        ''' <remarks></remarks>
        Sub Perform()
        ''' <summary>
        ''' 新しいステータスを返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetNewStatus() As String
    End Interface
End Namespace