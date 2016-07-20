Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    '''仕様書テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac0030Dao : Inherits DaoEachTable(Of Rhac0030Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">試作装備区分</param>
        ''' <param name="sobiKaiteiNo">列項目コード</param>
        ''' <param name="lineOpKaiteiNo">試作装備区分</param>
        ''' <param name="naigaisoKaiteiNo">列項目コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal kaihatsuFugo As String, _
                          ByVal sobiKaiteiNo As String, _
                          ByVal lineOpKaiteiNo As String, _
                          ByVal naigaisoKaiteiNo As String) As Rhac0030Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">試作装備区分</param>
        ''' <param name="sobiKaiteiNo">列項目コード</param>
        ''' <param name="lineOpKaiteiNo">試作装備区分</param>
        ''' <param name="naigaisoKaiteiNo">列項目コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal kaihatsuFugo As String, _
                            ByVal sobiKaiteiNo As String, _
                            ByVal lineOpKaiteiNo As String, _
                            ByVal naigaisoKaiteiNo As String) As Integer

    End Interface
End Namespace