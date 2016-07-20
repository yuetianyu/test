Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    Public Class TShisakuTelNoDaoImpl : Inherits EBomDaoEachTable(Of TShisakuTelNoVo)
        Implements TShisakuTelNoDao

        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuTelNoVo))
            Dim vo As New TShisakuTelNoVo
            table.IsA(vo).PkField(vo.UserId)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="UserId">ユーザーID</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal UserId As String) As TShisakuTelNoVo Implements TShisakuTelNoDao.FindByPk
            Return FindByPkMain(UserId)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="UserId">ユーザーID</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal UserId As String) As Integer Implements TShisakuTelNoDao.DeleteByPk
            Return DeleteByPkMain(UserId)
        End Function

    End Class
End Namespace