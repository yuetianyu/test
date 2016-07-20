Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''課マスタテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac1560Dao : Inherits DaoEachTable(Of Rhac1560Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="buCode">試作装備区分</param>
        ''' <param name="siteKbn">列項目コード</param>
        ''' <param name="kaCode">列項目コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal buCode As String, _
                          ByVal siteKbn As String, _
                          ByVal kaCode As String) As Rhac1560Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="buCode">試作装備区分</param>
        ''' <param name="siteKbn">列項目コード</param>
        ''' <param name="kaCode">列項目コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal buCode As String, _
                            ByVal siteKbn As String, _
                            ByVal kaCode As String) As Integer

    End Interface
End Namespace

