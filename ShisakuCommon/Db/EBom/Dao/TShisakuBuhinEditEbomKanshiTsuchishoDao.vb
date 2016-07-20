Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作部品編集設計通知書番号(EBOM設変)テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuBuhinEditEbomKanshiTsuchishoDao : Inherits DaoEachTable(Of TShisakuBuhinEditEbomKanshiTsuchishoVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="zumenNo">図面No</param>
        ''' <param name="zumenKaiteiNo">図面改訂No</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, _
                          ByVal shisakuBlockNoKaiteiNo As String, _
                          ByVal buhinNoHyoujiJun As Integer, _
                          ByVal zumenNo As String, _
                          ByVal zumenKaiteiNo As String) As TShisakuBuhinEditEbomKanshiTsuchishoVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="zumenNo">図面No</param>
        ''' <param name="zumenKaiteiNo">図面改訂No</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, _
                            ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal buhinNoHyoujiJun As Integer, _
                            ByVal zumenNo As String, _
                            ByVal zumenKaiteiNo As String) As Integer
    End Interface
End Namespace