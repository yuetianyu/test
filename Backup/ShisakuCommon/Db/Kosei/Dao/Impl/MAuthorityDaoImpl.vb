Imports ShisakuCommon.Db.Kosei.Vo

Namespace Db.Kosei.Dao.Impl

    ''' <summary>
    ''' 権限テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class MAuthorityDaoImpl : Inherits KoseiDaoEachTable(Of MAuthorityVo)
        Implements MAuthorityDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of MAuthorityVo))
            Dim vo As New MAuthorityVo
            table.IsA(vo).PkField(vo.MenuDaiKbn).PkField(vo.MenuKbn).PkField(vo.UserKbn)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="menuDaiKbn">メニュー大区分</param>
        ''' <param name="menuKbn">メニュー区分</param>
        ''' <param name="userKbn">ユーザ区分</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal menuDaiKbn As Nullable(Of Int32), ByVal menuKbn As Nullable(Of Int32) _
                          , ByVal userKbn As Nullable(Of Int32)) As MAuthorityVo Implements MAuthorityDao.FindByPk
            Return FindByPkMain(menuDaiKbn, menuKbn, userKbn)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="menuDaiKbn">メニュー大区分</param>
        ''' <param name="menuKbn">メニュー区分</param>
        ''' <param name="userKbn">ユーザ区分</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal menuDaiKbn As Nullable(Of Int32), ByVal menuKbn As Nullable(Of Int32) _
                          , ByVal userKbn As Nullable(Of Int32)) As Integer Implements MAuthorityDao.DeleteByPk
            Return DeleteByPkMain(menuDaiKbn, menuKbn, userKbn)
        End Function

    End Class
End Namespace