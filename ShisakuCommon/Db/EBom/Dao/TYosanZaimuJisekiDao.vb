Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 予算書イベント別年月別財務実績情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanZaimuJisekiDao : Inherits DaoEachTable(Of TYosanZaimuJisekiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanCode">予算コード</param>
        ''' <param name="YosanZaimuJisekiYyyyMm">財務実績計上年月</param>
        ''' <param name="YosanZaimuHireiKoteiKbn">比例費／固定費区分</param>
        ''' <param name="YosanZaimuJisekiKbn">財務実績区分</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal yosanCode As String, ByVal YosanZaimuJisekiYyyyMm As String, ByVal YosanZaimuHireiKoteiKbn As String, ByVal YosanZaimuJisekiKbn As String) As TYosanZaimuJisekiVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanCode">予算コード</param>
        ''' <param name="YosanZaimuJisekiYyyyMm">財務実績計上年月</param>
        ''' <param name="YosanZaimuHireiKoteiKbn">比例費／固定費区分</param>
        ''' <param name="YosanZaimuJisekiKbn">財務実績区分</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal yosanCode As String, ByVal YosanZaimuJisekiYyyyMm As String, ByVal YosanZaimuHireiKoteiKbn As String, ByVal YosanZaimuJisekiKbn As String) As Integer
    End Interface
End Namespace