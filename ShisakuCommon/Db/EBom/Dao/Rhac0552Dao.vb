Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''部品構成(図面テーブル群)テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac0552Dao : Inherits DaoEachTable(Of Rhac0552Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="kaiteiNo">構成改訂No.</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal buhinNoOya As String, _
                          ByVal buhinNoKo As String, _
                          ByVal kaiteiNo As String) As Rhac0552Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="kaiteiNo">構成改訂No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal buhinNoOya As String, _
                          ByVal buhinNoKo As String, _
                          ByVal kaiteiNo As String) As Integer

    End Interface
End Namespace

