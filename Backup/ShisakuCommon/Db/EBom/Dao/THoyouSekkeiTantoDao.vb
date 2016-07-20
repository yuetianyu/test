Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 補用設計担当情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface THoyouSekkeiTantoDao : Inherits DaoEachTable(Of THoyouSekkeiTantoVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTanto">補用担当</param>
        ''' <param name="hoyouTantoKaiteiNo">補用担当改訂№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal hoyouEventCode As String, _
                          ByVal hoyouBukaCode As String, _
                          ByVal hoyouTanto As String, _
                          ByVal hoyouTantoKaiteiNo As String) As THoyouSekkeiTantoVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTanto">補用担当</param>
        ''' <param name="hoyouTantoKaiteiNo">補用担当改訂№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal hoyouEventCode As String, _
                            ByVal hoyouBukaCode As String, _
                            ByVal hoyouTanto As String, _
                            ByVal hoyouTantoKaiteiNo As String) As Integer
    End Interface
End Namespace