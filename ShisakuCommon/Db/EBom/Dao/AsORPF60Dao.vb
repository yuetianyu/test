Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>
    '''新調達手配進捗情報の簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface AsORPF60Dao : Inherits DaoEachTable(Of AsORPF60Vo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="SGISBA">作業依頼書ＮＯ</param>
        ''' <param name="KBBA">管理ＮＯ</param>
        ''' <param name="CMBA">注文書ＮＯ</param>
        ''' <param name="NOKM">納入区分</param>
        ''' <param name="HAYM">発注年月日</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal SGISBA As String, _
                          ByVal KBBA As String, _
                          ByVal CMBA As String, _
                          ByVal NOKM As Char, _
                          ByVal HAYM As Integer) As AsORPF60Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="SGISBA">作業依頼書ＮＯ</param>
        ''' <param name="KBBA">管理ＮＯ</param>
        ''' <param name="CMBA">注文書ＮＯ</param>
        ''' <param name="NOKM">納入区分</param>
        ''' <param name="HAYM">発注年月日</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal SGISBA As String, _
                            ByVal KBBA As String, _
                            ByVal CMBA As String, _
                            ByVal NOKM As Char, _
                            ByVal HAYM As Integer) As Integer

    End Interface
End Namespace