Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作設計ブロック装備情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuSekkeiBlockSoubiDao : Inherits DaoEachTable(Of TShisakuSekkeiBlockSoubiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuSoubiHyoujiJun">項目№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, _
                          ByVal shisakuBlockNoKaiteiNo As String, _
                          ByVal shisakuSoubiHyoujiJun As String) As TShisakuSekkeiBlockSoubiVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuSoubiHyoujiJun">項目№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, _
                            ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal shisakuSoubiHyoujiJun As String) As Integer
    End Interface
End Namespace