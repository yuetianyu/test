Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 予算書イベント別金材情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanKanazaiDao : Inherits DaoEachTable(Of TYosanKanazaiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="KanazaiName">金材項目名</param>
        ''' <param name="YosanTukurikataYyyyMm">年月</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal yosanEventCode As String, ByVal KanazaiName As String, ByVal YosanTukurikataYyyyMm As String) As TYosanKanazaiVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="KanazaiName">金材項目名</param>
        ''' <param name="YosanTukurikataYyyyMm">年月</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal yosanEventCode As String, ByVal KanazaiName As String, ByVal YosanTukurikataYyyyMm As String) As Integer
    End Interface
End Namespace