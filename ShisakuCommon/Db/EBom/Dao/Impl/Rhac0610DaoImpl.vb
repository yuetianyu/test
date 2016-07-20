Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 取引先テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0610DaoImpl : Inherits EBomDaoEachTable(Of Rhac0610Vo)
        Implements Rhac0610Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Rhac0610Vo))
            Dim vo As New Rhac0610Vo
            table.IsA(vo).PkField(vo.MakerCode)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="makerCode">メーカーコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal makerCode As String) As Rhac0610Vo Implements Rhac0610Dao.FindByPk
            Return FindByPkMain(makerCode)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="makerCode">メーカーコード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal makerCode As String) As Integer Implements Rhac0610Dao.DeleteByPk
            Return DeleteByPkMain(makerCode)
        End Function

    End Class
End Namespace
