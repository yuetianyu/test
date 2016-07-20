Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''仕様・装備項目(承認済み)テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac0120Dao : Inherits DaoEachTable(Of Rhac0120Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shiyosobiCode">仕様・装備項目コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shiyosobiCode As String) As Rhac0120Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shiyosobiCode">仕様・装備項目コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shiyosobiCode As String) As Integer

    End Interface
End Namespace

