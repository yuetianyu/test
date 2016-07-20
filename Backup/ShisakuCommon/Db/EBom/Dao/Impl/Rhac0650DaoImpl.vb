Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 社員外部インターフェーステーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0650DaoImpl : Inherits EBomDaoEachTable(Of Rhac0650Vo)
        Implements Rhac0650Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0650Vo))
            Dim vo As New Rhac0650Vo
            table.IsA(vo).PkField(vo.ShainNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shainNo">社員番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal shainNo As String) As Vo.Rhac0650Vo Implements Rhac0650Dao.FindByPk
            Return FindByPkMain(shainNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shainNo">社員番号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal shainNo As String) As Integer Implements Rhac0650Dao.DeleteByPk
            Return DeleteByPkMain(shainNo)
        End Function
    End Class
End Namespace

