Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 試作装備マスターテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0020DaoImpl : Inherits EBomDaoEachTable(Of Rhac0020Vo)
        Implements Rhac0020Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0020Vo))
            Dim vo As New Rhac0020Vo
            table.IsA(vo).PkField(vo.KaihatsuFugo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="saihatsuFugo">開発符号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal saihatsuFugo As String) As Vo.Rhac0020Vo Implements Rhac0020Dao.FindByPk
            Return FindByPkMain(saihatsuFugo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="saihatsuFugo">開発符号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal saihatsuFugo As String) As Integer Implements Rhac0020Dao.DeleteByPk
            Return DeleteByPkMain(saihatsuFugo)
        End Function
    End Class
End Namespace
