Namespace ShisakuBuhinMenu.Dao
    Public interface BuhinDao
        '''' <summary>
        '''' INSTL情報のINSTL品番を構成している部品 RHAC0532 を取得
        '''' </summary>
        '''' <param name="shisakuEventCode">試作イベントコード</param>
        '''' <returns>部品 RHAC0532 情報</returns>
        '''' <remarks></remarks>
        'Function Find0532ByShisakuInstl(ByVal shisakuEventCode As String) As List(Of BuhinResultVo)

        ''' <summary>
        ''' 試作の構成情報から、部品 RHAC0532 を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="sakuseiCount">作成回数</param>
        ''' <returns>部品 RHAC0532 情報</returns>
        ''' <remarks></remarks>
        Function Find0532ByShisakuKousei(ByVal shisakuEventCode As String, ByVal sakuseiCount As Integer) As List(Of BuhinResultVo)

        ''' <summary>
        ''' 試作の構成情報から、部品 RHAC0533 を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="sakuseiCount">作成回数</param>
        ''' <returns>部品 RHAC0533 情報</returns>
        ''' <remarks></remarks>
        Function Find0533ByShisakuKousei(ByVal shisakuEventCode As String, ByVal sakuseiCount As Integer) As List(Of BuhinResultVo)

        ''' <summary>
        ''' 試作の構成情報から、部品 RHAC0530 を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="sakuseiCount">作成回数</param>
        ''' <returns>部品 RHAC0530 情報</returns>
        ''' <remarks></remarks>
        Function Find0530ByShisakuKousei(ByVal shisakuEventCode As String, ByVal sakuseiCount As Integer) As List(Of BuhinResultVo)

    End Interface
End NameSpace