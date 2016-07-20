Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 補用部品編集情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface THoyouBuhinEditDao : Inherits DaoEachTable(Of THoyouBuhinEditVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTanto">補用担当</param>
        ''' <param name="hoyouTantoKaiteiNo">補用担当改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal hoyouEventCode As String, _
                         ByVal hoyouBukaCode As String, _
                         ByVal hoyouTanto As String, _
                         ByVal hoyouTantoKaiteiNo As String, _
                         ByVal buhinNoHyoujiJun As Nullable(Of Int32)) As THoyouBuhinEditVo
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTanto">補用担当</param>
        ''' <param name="hoyouTantoKaiteiNo">補用担当改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal hoyouEventCode As String, _
                           ByVal hoyouBukaCode As String, _
                           ByVal hoyouTanto As String, _
                           ByVal hoyouTantoKaiteiNo As String, _
                           ByVal buhinNoHyoujiJun As Nullable(Of Int32)) As Integer

    End Interface

End Namespace
