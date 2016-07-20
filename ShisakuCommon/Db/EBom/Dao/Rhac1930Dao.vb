Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 属性テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac1930Dao

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="BuhinKatakaiteiNo">部品型改訂No</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal kaihatsuFugo As String, _
                          ByVal buhinNo As String, _
                          ByVal buhinKataKaiteiNo As String) As Rhac1930Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="buhinKataKaiteiNo">部品型改訂No</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal kaihatsuFugo As String, _
                            ByVal buhinNo As String, _
                            ByVal buhinKataKaiteiNo As String) As Integer

    End Interface
End Namespace