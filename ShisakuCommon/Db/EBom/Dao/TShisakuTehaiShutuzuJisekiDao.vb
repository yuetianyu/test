Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>t
    ''' 試作手配出図実績情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuTehaiShutuzuJisekiDao : Inherits DaoEachTable(Of TShisakuTehaiShutuzuJisekiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="ShutuzuJisekiKaiteiNo">出図実績_改訂№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuListCode As String, _
                          ByVal ShisakuListCodeKaiteiNo As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal BuhinNo As String, _
                          ByVal ShutuzuJisekiKaiteiNo As String _
                             ) As TShisakuTehaiShutuzuJisekiVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="ShutuzuJisekiKaiteiNo">出図実績_改訂№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal ShisakuEventCode As String, _
                            ByVal ShisakuListCode As String, _
                            ByVal ShisakuListCodeKaiteiNo As String, _
                            ByVal ShisakuBlockNo As String, _
                            ByVal BuhinNo As String, _
                            ByVal ShutuzuJisekiKaiteiNo As String _
                              ) As Integer




    End Interface
End Namespace