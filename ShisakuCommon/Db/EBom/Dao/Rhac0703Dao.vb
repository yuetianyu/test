Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    Public Interface Rhac0703Dao

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <param name="zumenkaiteiNo">図面改訂No.</param>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal zumenNo As String, _
                          ByVal zumenkaiteiNo As String, _
                          ByVal kaihatsuFugo As String) As Rhac0703Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <param name="zumenkaiteiNo">図面改訂No.</param>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal zumenNo As String, _
                            ByVal zumenkaiteiNo As String, _
                            ByVal kaihatsuFugo As String) As Integer

    End Interface
End Namespace