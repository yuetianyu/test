Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>試作部品編集・号車情報（TMP）の簡単なCRUDを集めたDAO</summary>
    Public Interface TShisakuBuhinEditGousyaTmp3dDao : Inherits DaoEachTable(Of TShisakuBuhinEditGousyaTmp3dVo)
        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="GUID">GUID</param>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="GyouId">行ＩＤ</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal Guid As String, _
                          ByVal ShisakuEventCode As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal ShisakuBlockNoKaiteiNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal ShisakuGousyaHyoujiJun As Nullable(Of Int32), _
                          ByVal GyouId As String) As TShisakuBuhinEditGousyaTmp3dVo

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="GUID">GUID</param>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="GyouId">行ＩＤ</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal Guid As String, _
                            ByVal ShisakuEventCode As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String, _
                          ByVal ShisakuBlockNoKaiteiNo As String, _
                          ByVal BuhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal ShisakuGousyaHyoujiJun As Nullable(Of Int32), _
                          ByVal GyouId As String) As Integer
    End Interface
End Namespace