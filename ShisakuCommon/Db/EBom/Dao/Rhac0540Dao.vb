Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''12桁型式テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac0540Dao : Inherits DaoEachTable(Of Rhac0540Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="sobiKaiteiNo">装備改訂No.</param>
        ''' <param name="katashikiScd7">7桁型式識別コード</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opKaiteiNo">OP改訂No.</param>
        ''' <param name="opCode">OPコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal kaihatsuFugo As String, _
                          ByVal sobiKaiteiNo As String, _
                          ByVal katashikiScd7 As String, _
                          ByVal shimukechiCode As String, _
                          ByVal opKaiteiNo As String, _
                          ByVal opCode As String) As Rhac0540Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="sobiKaiteiNo">装備改訂No.</param>
        ''' <param name="katashikiScd7">7桁型式識別コード</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opKaiteiNo">OP改訂No.</param>
        ''' <param name="opCode">OPコード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal kaihatsuFugo As String, _
                            ByVal sobiKaiteiNo As String, _
                            ByVal katashikiScd7 As String, _
                            ByVal shimukechiCode As String, _
                            ByVal opKaiteiNo As String, _
                            ByVal opCode As String) As Integer

    End Interface
End Namespace

