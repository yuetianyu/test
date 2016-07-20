Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''試作装備マスターテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface MShisakuSoubiDao : Inherits DaoEachTable(Of MShisakuSoubiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <param name="shisakuRetuKoumokuCode">列項目コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuSoubiKbn As String, _
                          ByVal shisakuRetuKoumokuCode As String) As MShisakuSoubiVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <param name="shisakuRetuKoumokuCode">列項目コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuSoubiKbn As String, _
                          ByVal shisakuRetuKoumokuCode As String) As Integer

    End Interface
End Namespace

