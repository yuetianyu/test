Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>試作手配帳訂正通知情報（号車情報）の簡単なCRUDを集めたDAO</summary>
    Public Interface TShisakuTehaiTeiseiGousyaDao : Inherits DaoEachTable(Of TShisakuTehaiTeiseiGousyaVo)
        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="Flag">フラグ</param>
        ''' <param name="ShisakuGousyaHyoujiJun">試作号車表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuListCode As String, _
                          ByVal ShisakuListCodeKaiteiNo As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal Flag As String, _
                          ByVal ShisakuGousyaHyoujiJun As Nullable(Of Int32)) As TShisakuTehaiTeiseiGousyaVo

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="Flag">フラグ</param>
        ''' <param name="ShisakuGousyaHyoujiJun">試作号車表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuListCode As String, _
                          ByVal ShisakuListCodeKaiteiNo As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal Flag As String, _
                          ByVal ShisakuGousyaHyoujiJun As Nullable(Of Int32)) As Integer
    End Interface
End Namespace