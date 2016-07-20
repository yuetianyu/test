Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''カラーテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac0430Dao : Inherits DaoEachTable(Of Rhac0430Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="colorCode">内外装区分</param>
        ''' <param name="naigaisoKbn">カラー名称</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal colorCode As String, _
                          ByVal naigaisoKbn As String) As Rhac0430Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="colorCode">内外装区分</param>
        ''' <param name="naigaisoKbn">カラー名称</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal colorCode As String, _
                            ByVal naigaisoKbn As String) As Integer

    End Interface
End Namespace

