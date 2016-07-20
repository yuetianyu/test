Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作部品編集情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuBuhinEditDao : Inherits DaoEachTable(Of TShisakuBuhinEditVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                         ByVal shisakuBukaCode As String, _
                         ByVal shisakuBlockNo As String, _
                         ByVal shisakuBlockNoKaiteiNo As String, _
                         ByVal buhinNoHyoujiJun As Nullable(Of Int32)) As TShisakuBuhinEditVo
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                           ByVal shisakuBukaCode As String, _
                           ByVal shisakuBlockNo As String, _
                           ByVal shisakuBlockNoKaiteiNo As String, _
                           ByVal buhinNoHyoujiJun As Nullable(Of Int32)) As Integer

        ''' <summary>
        ''' 指定されたイベントコードがテーブルに存在するかチェック.
        ''' </summary>
        ''' <param name="shisakuEventCode">検索するイベントコード</param>
        ''' <returns>True:存在する False:存在しない</returns>
        ''' <remarks></remarks>
        Function ExistByEventCode(ByVal shisakuEventCode As String) As Boolean

    End Interface

End Namespace
