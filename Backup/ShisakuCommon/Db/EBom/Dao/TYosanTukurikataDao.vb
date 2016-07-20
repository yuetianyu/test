Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 予算書イベント別造り方情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanTukurikataDao : Inherits DaoEachTable(Of TYosanTukurikataVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="ShisakuSyubetu">試作種別</param>
        ''' <param name="UnitKbn">ユニット区分</param>
        ''' <param name="BuhinName">部品表名</param>
        ''' <param name="PatternName">パターン名</param>
        ''' <param name="YosanTukurikataYyyyMm">年月</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal yosanEventCode As String, ByVal ShisakuSyubetu As String, _
                          ByVal UnitKbn As String, ByVal BuhinName As String, _
                          ByVal PatternName As String, ByVal YosanTukurikataYyyyMm As String) As TYosanTukurikataVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="ShisakuSyubetu">試作種別</param>
        ''' <param name="UnitKbn">ユニット区分</param>
        ''' <param name="BuhinName">部品表名</param>
        ''' <param name="PatternName">パターン名</param>
        ''' <param name="YosanTukurikataYyyyMm">年月</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal yosanEventCode As String, ByVal ShisakuSyubetu As String, _
                            ByVal UnitKbn As String, ByVal BuhinName As String, _
                            ByVal PatternName As String, ByVal YosanTukurikataYyyyMm As String) As Integer
    End Interface
End Namespace