Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>試作部品編集・予算設定部品表_履歴の簡単なCRUDを集めたDAO</summary>
    Public Interface TYosanSetteiBuhinRirekiDao : Inherits DaoEachTable(Of TYosanSetteiBuhinRirekiVo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="YosanListCode">予算リストコード</param>
        ''' <param name="YosanBukaCode">予算部課コード</param>
        ''' <param name="YosanBlockNo">予算ブロック№</param>
        ''' <param name="YosanBuhinNo">予算部品番号</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>      
        ''' <param name="ColumnId">列ID</param>
        ''' <param name="ColumnName">列名</param>
        ''' <param name="UpdateBi">変更日</param>
        ''' <param name="UpdateJikan">変更時間</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal ShisakuEventCode As String, _
                          ByVal YosanListCode As String, _
                          ByVal YosanBukaCode As String, _
                          ByVal YosanBlockNo As String, _
                          ByVal YosanBuhinNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal ColumnId As String, _
                          ByVal ColumnName As String, _
                          ByVal UpdateBi As String, _
                          ByVal UpdateJikan As String) As TYosanSetteiBuhinRirekiVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="YosanListCode">予算リストコード</param>
        ''' <param name="YosanBukaCode">予算部課コード</param>
        ''' <param name="YosanBlockNo">予算ブロック№</param>
        ''' <param name="YosanBuhinNo">予算部品番号</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>       
        ''' <param name="ColumnId">列ID</param>
        ''' <param name="ColumnName">列名</param>
        ''' <param name="UpdateBi">変更日</param>
        ''' <param name="UpdateJikan">変更時間</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal ShisakuEventCode As String, _
                          ByVal YosanListCode As String, _
                          ByVal YosanBukaCode As String, _
                          ByVal YosanBlockNo As String, _
                          ByVal YosanBuhinNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal ColumnId As String, _
                          ByVal ColumnName As String, _
                          ByVal UpdateBi As String, _
                          ByVal UpdateJikan As String) As Integer
    End Interface
End Namespace


