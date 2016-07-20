Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 図面テーブル群の簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac0702Dao : Inherits DaoEachTable(Of Rhac0702Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <param name="zumenKaiteiNo">図面改訂No.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal zumenNo As String, _
                          ByVal zumenKaiteiNo As String) As Rhac0702Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <param name="zumenKaiteiNo">図面改訂No.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal zumenNo As String, _
                            ByVal zumenKaiteiNo As String) As Integer

    End Interface


End Namespace
