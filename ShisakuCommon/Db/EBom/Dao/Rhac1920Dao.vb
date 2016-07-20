Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 属性テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac1920Dao

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="blkBuhinkaiteiNo">BLK部品改訂No</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal kaihatsuFugo As String, _
                          ByVal blockNo As String, _
                          ByVal buhinNo As String, _
                          ByVal blkBuhinKaiteiNo As String) As Rhac1920Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="blkBuhinkaiteiNo"></param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal kaihatsuFugo As String, _
                            ByVal blockNo As String, _
                            ByVal buhinNo As String, _
                            ByVal blkBuhinKaiteiNo As String) As Integer

    End Interface
End Namespace