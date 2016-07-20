Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 7桁型式テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac0230Dao : Inherits DaoEachTable(Of Rhac0230Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="sobiKaiteiNo">装備改訂No.</param>
        ''' <param name="katashikiScd7">7桁型式識別コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal kaihatsuFugo As String, _
                          ByVal sobiKaiteiNo As String, _
                          ByVal katashikiScd7 As String) As Rhac0230Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="sobiKaiteiNo">装備改訂No.</param>
        ''' <param name="katashikiScd7">7桁型式識別コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal kaihatsuFugo As String, _
                            ByVal sobiKaiteiNo As String, _
                            ByVal katashikiScd7 As String) As Integer

    End Interface
End Namespace

