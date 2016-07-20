Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''試作手配出図織込情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuTehaiShutuzuOrikomiDao : Inherits DaoEachTable(Of TShisakuTehaiShutuzuOrikomiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="GyouId">行ID</param>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal ShisakuEventCode As String, ByVal ShisakuListCode As String _
                         , ByVal ShisakuListCodeKaiteiNo As String, ByVal ShisakuBlockNo As String _
                         , ByVal GyouId As String, ByVal BuhinNo As String) _
                         As TShisakuTehaiShutuzuOrikomiVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="GyouId">行ID</param>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal ShisakuEventCode As String, ByVal ShisakuListCode As String _
                          , ByVal ShisakuListCodeKaiteiNo As String, ByVal ShisakuBlockNo As String _
                         , ByVal GyouId As String, ByVal BuhinNo As String) _
                          As Integer
    End Interface
End Namespace