Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    ''' <summary>
    ''' 試作部品作成メニューExcel出力
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IShisakuSekkeiBlockDao : Inherits DaoEachFeature
        ''' <summary>
        ''' 試作部品作成メニューExcel出力（集計）
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns>集計一覧</returns>
        ''' <remarks></remarks>
        Function GetShisakuSekkeiBlockCount(ByVal eventCode As String) As List(Of ShisakuSekkeiBlockCountVo)

        ''' <summary>
        ''' 試作部品作成メニューExcel出力（明細）
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns>明細一覧</returns>
        ''' <remarks></remarks>
        Function GetShisakuSekkeiBlockMeisai(ByVal eventCode As String) As List(Of ShisakuSekkeiBlockMeisaiVo)




        ''' <summary>
        ''' 改訂№一番大きいの部課コードを得る
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetShisakuSekkeiBlockBukaCode(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String) As TShisakuSekkeiBlockVo


        ''' <summary>
        '''　改訂Noを得る
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetShisakuSekkeiKaiteiNo(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockVo)


    End Interface
End Namespace

