Imports ShisakuCommon.Db.EBom.Vo
Namespace ShisakuBuhinMenu.Dao
    Public Interface ShisakuEventBaseDao

        '2012/01/12
        ''' <summary>
        ''' ベース車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Function FindShisakuEventBase(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo)
        '↓↓2014/09/24 酒井 ADD BEGIN
        Function FindShisakuEventEbomKanshi(ByVal shisakuEventCode As String) As List(Of TShisakuEventEbomKanshiVo)
        '↑↑2014/09/24 酒井 ADD END
        '2012/02/16
        ''' <summary>
        ''' ベース車の開発符号を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks>通常のベース車の号車から開発符号を取得する</remarks>
        Function FindShisakuEventBaseByEventCodeAndGousyaForShisakuGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventBaseVo
        '↓↓2014/09/24 酒井 ADD BEGIN
        Function FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventEbomKanshiVo
        '↑↑2014/09/24 酒井 ADD END
        '2012/02/16
        ''' <summary>
        ''' ベース車情報を開発符号を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks>試作側データのベースとなる号車から開発符号を取得する</remarks>
        Function FindShisakuEventBaseByEventCodeAndGousyaForShisakuBaseGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventBaseVo
        '↓↓2014/09/24 酒井 ADD BEGIN
        Function FindShisakuEventEbomKanshiByEventCodeAndGousyaForShisakuBaseGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As TShisakuEventEbomKanshiVo
        '↑↑2014/09/24 酒井 ADD END

        ''' <summary>
        ''' ベース車開発符号を取得
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindBaseKaihatsuFugo(ByVal buhinNo As String) As List(Of TShisakuEventBaseVo)

    End Interface
End Namespace