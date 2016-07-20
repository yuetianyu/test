Imports ShisakuCommon.Db.Kosei.Vo

Namespace Db.Kosei.Dao.Impl

    ''' <summary>
    ''' ユーザIDテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class MUserDaoImpl : Inherits KoseiDaoEachTable(Of MUserVo)
        Implements MUserDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of MUserVo))
            Dim vo As New MUserVo
            table.IsA(vo).PkField(vo.UserId)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="userId">ユーザID</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal userId As String) As MUserVo Implements MUserDao.FindByPk
            Return FindByPkMain(userId)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="userId">ユーザID</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal userId As String) As Integer Implements MUserDao.DeleteByPk
            Return DeleteByPkMain(userId)
        End Function

    End Class
End Namespace