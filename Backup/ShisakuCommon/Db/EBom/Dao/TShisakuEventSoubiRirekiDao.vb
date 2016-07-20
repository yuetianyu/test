Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作イベント特別装備履歴情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuEventSoubiRirekiDao : Inherits DaoEachTable(Of TShisakuEventSoubiRirekiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <param name="shisakuTokubetuSoubiHyoujiNo">試作特別装備表示順</param>
        ''' <param name="shisakuTokubetuSoubiKbn">試作特別装備区分</param>
        ''' <param name="updateBi">更新日</param>
        ''' <param name="updateJikan">更新時間</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, ByVal hyojijunNo As Nullable(Of Int32) _
                         , ByVal shisakuTokubetuSoubiHyoujiNo As Nullable(Of Int32), ByVal shisakuTokubetuSoubiKbn As String _
                         , ByVal updateBi As String, Byval updateJikan As String) As TShisakuEventSoubiRirekiVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <param name="shisakuTokubetuSoubiHyoujiNo">試作特別装備表示順</param>
        ''' <param name="shisakuTokubetuSoubiKbn">試作特別装備区分</param>
        ''' <param name="updateBi">更新日</param>
        ''' <param name="updateJikan">更新時間</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, ByVal hyojijunNo As Nullable(Of Int32) _
                         , ByVal shisakuTokubetuSoubiHyoujiNo As Nullable(Of Int32), ByVal shisakuTokubetuSoubiKbn As String _
                         , ByVal updateBi As String, ByVal updateJikan As String) As Integer
    End Interface
End Namespace