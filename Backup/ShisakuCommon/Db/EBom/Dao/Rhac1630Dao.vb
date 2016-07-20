Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' カラー12桁型式適用テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac1630Dao : Inherits DaoEachTable(Of Rhac1630Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="sobiKaiteiNo">装備改訂No.</param>
        ''' <param name="colorCode">カラーコード</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opSpecCode">OPスペックコード</param>
        ''' <param name="katashikiScd7">7桁型式識別コード</param>
        ''' <param name="colNo">列No.</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal kaihatsuFugo As String, _
                          ByVal sobiKaiteiNo As String, _
                          ByVal colorCode As String, _
                          ByVal shimukechiCode As String, _
                          ByVal opSpecCode As String, _
                          ByVal katashikiScd7 As String, _
                          ByVal colNo As Nullable(Of Int32)) As Rhac1630Vo
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="sobiKaiteiNo">装備改訂No.</param>
        ''' <param name="colorCode">カラーコード</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opSpecCode">OPスペックコード</param>
        ''' <param name="katashikiScd7">7桁型式識別コード</param>
        ''' <param name="colNo">列No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal kaihatsuFugo As String, _
                            ByVal sobiKaiteiNo As String, _
                            ByVal colorCode As String, _
                            ByVal shimukechiCode As String, _
                            ByVal opSpecCode As String, _
                            ByVal katashikiScd7 As String, _
                            ByVal colNo As Nullable(Of Int32)) As Integer

    End Interface
End Namespace

