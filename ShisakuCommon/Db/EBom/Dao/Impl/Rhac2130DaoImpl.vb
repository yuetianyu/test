Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 社員マスター情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac2130DaoImpl : Inherits EBomDaoEachTable(Of Rhac2130Vo)
        Implements Rhac2130Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Rhac2130Vo))
            Dim vo As New Rhac2130Vo
            table.IsA(vo) _
            .PkField(vo.ShainNo) 
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shainNo">社員番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shainNo As String) As Vo.Rhac2130Vo Implements Rhac2130Dao.FindByPk
            Return FindByPkMain(shainNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shainNo">社員番号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shainNo As String) As Integer Implements Rhac2130Dao.DeleteByPk
            Return DeleteByPkMain(shainNo)
        End Function

    End Class
End Namespace


