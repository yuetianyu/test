Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>
    '''発注納入状況ファイルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface AsORPF32Dao : Inherits DaoEachTable(Of AsORPF32Vo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="SGISBA">作業依頼書ＮＯ</param>
        ''' <param name="KBBA">管理ＮＯ</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal SGISBA As String, _
                          ByVal KBBA As String) As AsORPF32Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="SGISBA">作業依頼書ＮＯ</param>
        ''' <param name="KBBA">管理ＮＯ</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal SGISBA As String, _
                            ByVal KBBA As String) As Integer

    End Interface
End Namespace