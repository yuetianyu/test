Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''開発車機能ブロックテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac0080Dao : Inherits DaoEachTable(Of Rhac0080Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="blockNoKino">ブロックNo.(機能)</param>
        ''' <param name="kaiteiNoKino">改訂No.(機能)</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal kaihatsuFugo As String, _
                          ByVal blockNoKino As String, _
                          ByVal kaiteiNoKino As String) As Rhac0080Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="blockNoKino">ブロックNo.(機能)</param>
        ''' <param name="kaiteiNoKino">改訂No.(機能)</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal kaihatsuFugo As String, _
                            ByVal blockNoKino As String, _
                            ByVal kaiteiNoKino As String) As Integer

    End Interface
End Namespace

