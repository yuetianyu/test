Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class MUserMailAddressDaoImpl : Inherits EBomDaoEachTable(Of MUserMailAddressVo)
        Implements MUserMailAddressDao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.MUserMailAddressVo))
            Dim vo As New MUserMailAddressVo
            table.IsA(vo).PkField(vo.UserId)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="UserId">ユーザーID</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal UserId As String) As MUserMailAddressVo Implements MUserMailAddressDao.FindByPk

            Return FindByPkMain(UserId)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="UserId">ユーザーID</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal UserId As String) As Integer Implements MUserMailAddressDao.DeleteByPk
            Return DeleteByPkMain(UserId)
        End Function

    End Class
End Namespace