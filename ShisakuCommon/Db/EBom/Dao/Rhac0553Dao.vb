Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao

''↓↓2014/07/23 Ⅰ.3.設計編集 ベース車改修専用化 (TES)張 ADD BEGIN
    'Public Interface Rhac0553Dao
    Public Interface Rhac0553Dao : Inherits DaoEachTable(Of Rhac0553Vo)
''↑↑2014/07/23 Ⅰ.3.設計編集 ベース車改修専用化 (TES)張 ADD END

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="kaiteiNo">改訂No.</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal kaihatsuFugo As String, _
                          ByVal buhinNoOya As String, _
                          ByVal buhinNoKo As String, _
                          ByVal kaiteiNo As String) As Rhac0553Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="kaiteiNo">改訂No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal kaihatsuFugo As String, _
                            ByVal buhinNoOya As String, _
                            ByVal buhinNoKo As String, _
                            ByVal kaiteiNo As String) As Integer

    End Interface
End Namespace