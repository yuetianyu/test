Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作設計ブロックメモ情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuSekkeiBlockMemoDao : Inherits DaoEachTable(Of TShisakuSekkeiBlockMemoVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="shisakuMemoHyoujiJun">試作メモ表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String _
                         , ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String _
                         , ByVal shisakuGousya As String, ByVal shisakuMemoHyoujiJun As Nullable(Of Int32)) _
                         As TShisakuSekkeiBlockMemoVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="shisakuMemoHyoujiJun">試作メモ表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String _
                         , ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String _
                         , ByVal shisakuGousya As String, ByVal shisakuMemoHyoujiJun As Nullable(Of Int32)) As Integer
    End Interface
End Namespace