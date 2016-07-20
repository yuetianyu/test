Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    Public Interface Rhac0530Dao : Inherits DaoEachTable(Of Rhac0530Vo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="kaiteiNo">改訂No.</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal buhinNo As String, _
                          ByVal kaiteiNo As String) As Rhac0530Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="kaiteiNo">改訂No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal buhinNo As String, _
                            ByVal kaiteiNo As String) As Integer


    End Interface
End Namespace