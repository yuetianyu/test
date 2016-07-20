Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class MSyukeiCodeDaoImpl : Inherits EBomDaoEachTable(Of MSyukeiCodeVo)
        Implements MSyukeiCodeDao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.MSyukeiCodeVo))
            Dim vo As New MSyukeiCodeVo
            table.IsA(vo).PkField(vo.SyukeiCode)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="SyukeiCode">集計コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal SyukeiCode As String) As MSyukeiCodeVo Implements MSyukeiCodeDao.FindByPk

            Return FindByPkMain(SyukeiCode)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="SyukeiCode">集計コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal SyukeiCode As String) As Integer Implements MSyukeiCodeDao.DeleteByPk
            Return DeleteByPkMain(SyukeiCode)
        End Function

    End Class
End Namespace