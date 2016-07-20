Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>試作部品編集・予算設定部品表の簡単なCRUDを集めたDAO</summary>
    Public Interface TYosanSetteiBuhinDao : Inherits DaoEachTable(Of TYosanSetteiBuhinVo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="YosanListCode">予算リストコード</param>
        ''' <param name="YosanBukaCode">予算部課コード</param>
        ''' <param name="YosanBlockNo">予算ブロック№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>      
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal ShisakuEventCode As String, _
                          ByVal YosanListCode As String, _
                          ByVal YosanBukaCode As String, _
                          ByVal YosanBlockNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32)) As TYosanSetteiBuhinVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="YosanListCode">予算リストコード</param>
        ''' <param name="YosanBukaCode">予算部課コード</param>
        ''' <param name="YosanBlockNo">予算ブロック№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>       
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal ShisakuEventCode As String, _
                          ByVal YosanListCode As String, _
                          ByVal YosanBukaCode As String, _
                          ByVal YosanBlockNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32)) As Integer
    End Interface
End Namespace


