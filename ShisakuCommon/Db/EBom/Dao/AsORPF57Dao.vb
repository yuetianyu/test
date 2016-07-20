Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>
    '''現調品発注控ファイルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface AsORPF57Dao : Inherits DaoEachTable(Of AsORPF57Vo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="KOMZBA">工事指令書ＮＯ</param>
        ''' <param name="GRNO">グループＮＯ</param>
        ''' <param name="SRNO">シリアルＮＯ</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal KOMZBA As String, _
                          ByVal GRNO As String, _
                          ByVal SRNO As String) As AsORPF57Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="KOMZBA">工事指令書ＮＯ</param>
        ''' <param name="GRNO">グループＮＯ</param>
        ''' <param name="SRNO">シリアルＮＯ</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal KOMZBA As String, _
                            ByVal GRNO As String, _
                            ByVal SRNO As String) As Integer

    End Interface
End Namespace