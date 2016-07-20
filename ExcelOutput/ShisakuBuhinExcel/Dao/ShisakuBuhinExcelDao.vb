Imports ShisakuCommon.Db.EBom.Vo
Imports ExcelOutput.ShisakuBuhinExcel.Vo

Namespace ShisakuBuhinExcel.Dao
    Public Interface ShisakuBuhinExcelDao

        ''' <summary>
        ''' 試作部品表編集INSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作部品表編集INSTL情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditInstl(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String) As List(Of BuhinEditInstlVoHelper)

        ''' <summary>
        ''' 試作部品表編集情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作部品表編集情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String) As List(Of BuhinEditVoHelper)

        ''↓↓2014/09/19 酒井 ADD BEGIN
        Function FindByBuhinEditBase(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String) As List(Of BuhinEditVoHelper)
        ''↑↑2014/09/19 酒井 ADD END

        ''↓↓2014/09/18 酒井 ADD BEGIN
        Function FindByAllBuhinEdit(ByVal shisakuEventCode As String, ByVal blockNo As String) As List(Of BuhinEditVoHelper)
        ''↑↑2014/09/18 酒井 ADD END

        ''' <summary>
        ''' 試作設計ブロックINSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作設計ブロックINSTL情報</returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlockInstl(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockInstlVo)

        ''' <summary>
        ''' 試作イベント情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作イベント情報</returns>
        ''' <remarks></remarks>
        Function FindByEvent(ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' 試作ベース車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作ベース車情報</returns>
        ''' <remarks></remarks>
        Function FindByBase(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo)


        ''' <summary>
        ''' 合計員数数量を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>合計員数数量</returns>
        ''' <remarks></remarks>
        Function GetTotalInsuSuryo(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal buhinNoHyoujiJun As Integer) As Integer

        ''' <summary>
        ''' 号車と関わる部品の員数を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>員数数量</returns>
        ''' <remarks></remarks>
        Function GetGousyaBuhinInsuSuryo(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal buhinNoHyoujiJun As Integer, ByVal shisakuGousya As String) As List(Of TShisakuBuhinEditInstlVo)


        ''' <summary>
        ''' 試作設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <returns>ユニット区分</returns>
        ''' <remarks></remarks>
        Function FindByShisakuBlockNo(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal shisakuBlockNoKaiteiNo As String) As String
        Function FindByShisakuBlockNoForUnitKbn() As Hashtable

        Function FindByBlockGroup(ByVal shisakuEventCode As String) As List(Of TShisakuBuhinEditVo)

    End Interface
End Namespace