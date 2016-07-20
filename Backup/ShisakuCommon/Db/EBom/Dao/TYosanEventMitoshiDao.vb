Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 予算書イベント別見通情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanEventMitoshiDao : Inherits DaoEachTable(Of TYosanEventMitoshiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="YosanEventCode">予算イベントコード</param>
        ''' <param name="YosanEventMitoshiYyyyMm">見通し計上年月</param>
        ''' <param name="YosanEventMitoshiKbn">見通し計上年月</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal YosanEventCode As String, ByVal YosanEventMitoshiYyyyMm As String, ByVal YosanEventMitoshiKbn As String) As TYosanEventMitoshiVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="YosanEventCode">予算イベントコード</param>
        ''' <param name="YosanEventMitoshiYyyyMm">見通し計上年月</param>
        ''' <param name="YosanEventMitoshiKbn">見通し計上年月</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal YosanEventCode As String, ByVal YosanEventMitoshiYyyyMm As String, ByVal YosanEventMitoshiKbn As String) As Integer
    End Interface
End Namespace