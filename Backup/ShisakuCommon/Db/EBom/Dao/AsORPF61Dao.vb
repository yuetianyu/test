Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>
    '''現調品手配進捗情報の簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface AsORPF61Dao : Inherits DaoEachTable(Of AsORPF61Vo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="GRNO">グループＮＯ</param>
        ''' <param name="SRNO">シリアルＮＯ</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal GRNO As String, _
                          ByVal SRNO As String) As AsORPF61Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="GRNO">グループＮＯ</param>
        ''' <param name="SRNO">シリアルＮＯ</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal GRNO As String, _
                            ByVal SRNO As String) As Integer

    End Interface
End Namespace