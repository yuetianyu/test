Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>
    '''試作部品表ファイルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface AsPRPF02Dao : Inherits DaoEachTable(Of AsPRPF02Vo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="OLD_LIST_CODE">旧リストコード</param>
        ''' <param name="ZRKG">材料記号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal OLD_LIST_CODE As String, _
                          ByVal ZRKG As String) As AsPRPF02Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="OLD_LIST_CODE">旧リストコード</param>
        ''' <param name="ZRKG">材料記号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal OLD_LIST_CODE As String, _
                            ByVal ZRKG As String) As Integer

    End Interface
End Namespace