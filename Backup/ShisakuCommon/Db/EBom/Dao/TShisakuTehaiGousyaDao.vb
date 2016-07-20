Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作設計ブロックメモ情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuTehaiGousyaDao : Inherits DaoEachTable(Of TShisakuTehaiGousyaVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, _
                          ByVal buhinNoHyoujiJun As String) As TShisakuTehaiGousyaVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, _
                          ByVal buhinNoHyoujiJun As String) As Integer
        ''' <summary>
        ''' 指定された号車情報がテーブルに存在するかチェック.
        ''' </summary>
        ''' <param name="shisakuEventCode">検索するイベントコード</param>
        ''' <param name="listCode">リストコード</param>
        ''' <param name="shisakuGousyaList">号車情報</param>
        ''' <returns>True:存在する False:存在しない</returns>
        ''' <remarks></remarks>
        Function ExistByShisakuGousya(ByVal shisakuEventCode As String, _
                                      ByVal listCode As String, _
                                      ByVal shisakuGousyaList As Dictionary(Of Integer, String)) As Boolean

    End Interface
End Namespace