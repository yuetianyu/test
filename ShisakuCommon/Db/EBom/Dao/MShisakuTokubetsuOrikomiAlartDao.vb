Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>作り方項目マスタの簡単なCRUDを集めたDAO</summary>

    Public Interface MShisakuTokubetsuOrikomiAlartDao : Inherits DaoEachTable(Of MShisakuTokubetsuOrikomiAlartVo)

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="BlockNo">ブロックNo</param>
        ''' <param name="DaiKbnName">大区分名</param>
        ''' <param name="ChuKbnName">中区分名</param>
        ''' <param name="ShoKbnName">詳細</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal BlockNo As String, _
                                 ByVal DaiKbnName As String, _
                                 ByVal ChuKbnName As String, _
                                 ByVal ShoKbnName As String) As MShisakuTokubetsuOrikomiAlartVo

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="BlockNo">ブロックNo</param>
        ''' <param name="DaiKbnName">大区分名</param>
        ''' <param name="ChuKbnName">中区分名</param>
        ''' <param name="ShoKbnName">詳細</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal BlockNo As String, _
                                 ByVal DaiKbnName As String, _
                                 ByVal ChuKbnName As String, _
                                 ByVal ShoKbnName As String) As Integer
    End Interface

End Namespace