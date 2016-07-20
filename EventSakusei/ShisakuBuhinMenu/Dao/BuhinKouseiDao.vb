Namespace ShisakuBuhinMenu.Dao
    Public Interface BuhinKouseiDao
        ''' <summary>
        ''' INSTL情報のINSTL品番で、最新改訂の RHAC0552 を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>最新改訂のRHAC0552情報</returns>
        ''' <remarks></remarks>
        Function FindNew0552ByShisakuInslt(ByVal shisakuEventCode As String) As List(Of BuhinKouseiResultVo)

        ''' <summary>
        ''' INSTL情報のINSTL品番で、最新改訂の RHAC0553 を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>最新改訂のRHAC0553情報</returns>
        ''' <remarks></remarks>
        Function FindNew0553ByShisakuInslt(ByVal shisakuEventCode As String) As List(Of BuhinKouseiResultVo)

        ''' <summary>
        ''' INSTL情報のINSTL品番で、最新改訂の RHAC0551 を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>最新改訂のRHAC0551情報</returns>
        ''' <remarks></remarks>
        Function FindNew0551ByShisakuInslt(ByVal shisakuEventCode As String) As List(Of BuhinKouseiResultVo)

    End Interface
End NameSpace