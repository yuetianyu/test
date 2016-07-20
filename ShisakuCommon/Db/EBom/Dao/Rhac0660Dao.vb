Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''組織テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac0660Dao : Inherits DaoEachTable(Of Rhac0660Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="siteKbn">サイト区分</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal bukaCode As String, _
                          ByVal siteKbn As String) As Rhac0660Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="siteKbn">サイト区分</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal bukaCode As String, _
                            ByVal siteKbn As String) As Integer

    End Interface
End Namespace

