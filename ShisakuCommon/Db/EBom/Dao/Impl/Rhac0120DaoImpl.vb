Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 仕様・装備項目(承認済み)テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0120DaoImpl : Inherits EBomDaoEachTable(Of Rhac0120Vo)
        Implements Rhac0120Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0120Vo))
            Dim vo As New Rhac0120Vo
            table.IsA(vo).PkField(vo.ShiyosobiCode)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shiyosobiCode">仕様・装備項目コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal shiyosobiCode As String) As Vo.Rhac0120Vo Implements Rhac0120Dao.FindByPk
            Return FindByPkMain(shiyosobiCode)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shiyosobiCode">仕様・装備項目コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal shiyosobiCode As String) As Integer Implements Rhac0120Dao.DeleteByPk
            Return DeleteByPkMain(shiyosobiCode)
        End Function
    End Class
End Namespace
