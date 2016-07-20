Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' AS400部品マスタの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface AsBUHIN01Dao : Inherits DaoEachTable(Of AsBUHIN01Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="STSR">設通シリーズ</param>
        ''' <param name="RSKM">量試区分</param>
        ''' <param name="DHSTBA">設通№</param>
        ''' <param name="GZZCP">集合図面－図面番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal STSR As String, _
                          ByVal RSKM As String, _
                          ByVal DHSTBA As String, _
                          ByVal GZZCP As String) As AsBUHIN01Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="STSR">設通シリーズ</param>
        ''' <param name="RSKM">量試区分</param>
        ''' <param name="DHSTBA">設通№</param>
        ''' <param name="GZZCP">集合図面－図面番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal STSR As String, _
                            ByVal RSKM As String, _
                            ByVal DHSTBA As String, _
                            ByVal GZZCP As String) As Integer

    End Interface
End Namespace