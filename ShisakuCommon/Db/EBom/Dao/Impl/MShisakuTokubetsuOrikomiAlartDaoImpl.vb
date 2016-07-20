Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class MShisakuTokubetsuOrikomiAlartDaoImpl : Inherits EBomDaoEachTable(Of MShisakuTokubetsuOrikomiAlartVo)
        Implements MShisakuTokubetsuOrikomiAlartDao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.MShisakuTokubetsuOrikomiAlartVo))
            Dim vo As New MShisakuTokubetsuOrikomiAlartVo
            table.IsA(vo).PkField(vo.BlockNo) _
                         .PkField(vo.DaiKbnName) _
                         .PkField(vo.ChuKbnName) _
                         .PkField(vo.ShoKbnName)

        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="BlockNo">ブロックNo</param>
        ''' <param name="DaiKbnName">大区分名</param>
        ''' <param name="ChuKbnName">中区分名</param>
        ''' <param name="ShoKbnName">詳細</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal BlockNo As String, _
                                 ByVal DaiKbnName As String, _
                                 ByVal ChuKbnName As String, _
                                 ByVal ShoKbnName As String) As MShisakuTokubetsuOrikomiAlartVo Implements MShisakuTokubetsuOrikomiAlartDao.FindByPk
            Return FindByPkMain(BlockNo, DaiKbnName, ChuKbnName, ShoKbnName)
        End Function

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
                                 ByVal ShoKbnName As String) As Integer Implements MShisakuTokubetsuOrikomiAlartDao.DeleteByPk
            Return DeleteByPkMain(BlockNo, DaiKbnName, ChuKbnName, ShoKbnName)
        End Function

    End Class
End Namespace