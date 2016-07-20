Imports ShisakuCommon.Db.Kosei.Vo

Namespace Db.Kosei.Dao.Impl

    ''' <summary>
    ''' 権限(ユーザー別)テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class MAuthorityUserDaoImpl : Inherits KoseiDaoEachTable(Of MAuthorityUserVo)
        Implements MAuthorityUserDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of MAuthorityUserVo))
            Dim vo As New MAuthorityUserVo
            table.IsA(vo).PkField(vo.AppNo) _
                         .PkField(vo.KinoId1) _
                         .PkField(vo.KinoId2) _
                         .PkField(vo.KinoId3) _
                         .PkField(vo.UserId)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="appNo">アプリケーション管理No.  </param>
        ''' <param name="kinoId1">機能ID1</param>
        ''' <param name="kinoId2">機能ID2</param>
        ''' <param name="kinoId3">機能ID3</param>
        ''' <param name="userId">ﾕｰｻﾞｰID </param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal appNo As String, _
                                 ByVal kinoId1 As String, _
                                 ByVal kinoId2 As String, _
                                 ByVal kinoId3 As String, _
                                 ByVal userId As String) As MAuthorityUserVo Implements MAuthorityUserDao.FindByPk
            Return FindByPkMain(appNo, _
                                kinoId1, _
                                kinoId2, _
                                kinoId3, _
                                userId)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="appNo">アプリケーション管理No.  </param>
        ''' <param name="kinoId1">機能ID1</param>
        ''' <param name="kinoId2">機能ID2</param>
        ''' <param name="kinoId3">機能ID3</param>
        ''' <param name="userId">ﾕｰｻﾞｰID </param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal appNo As String, _
                                   ByVal kinoId1 As String, _
                                   ByVal kinoId2 As String, _
                                   ByVal kinoId3 As String, _
                                   ByVal userId As String) As Integer Implements MAuthorityUserDao.DeleteByPk
            Return DeleteByPkMain(appNo, _
                                  kinoId1, _
                                  kinoId2, _
                                  kinoId3, _
                                  userId)
        End Function

    End Class
End Namespace
