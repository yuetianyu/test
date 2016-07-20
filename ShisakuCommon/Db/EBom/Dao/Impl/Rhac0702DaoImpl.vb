Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 図面テーブル群の簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0702DaoImpl : Inherits EBomDaoEachTable(Of Rhac0702Vo)
        Implements Rhac0702Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0702Vo))
            Dim vo As New Rhac0702Vo
            table.IsA(vo) _
            .PkField(vo.ZumenNo) _
            .PkField(vo.ZumenKaiteiNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <param name="zumenKaiteiNo">図面改訂No.</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal zumenNo As String, _
                                  ByVal zumenKaiteiNo As String) As Vo.Rhac0702Vo Implements Rhac0702Dao.FindByPk
            Return FindByPkMain(zumenNo, zumenKaiteiNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <param name="zumenKaiteiNo">図面改訂No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal zumenNo As String, _
                                    ByVal zumenKaiteiNo As String) As Integer Implements Rhac0702Dao.DeleteByPk
            Return DeleteByPkMain(zumenNo, zumenKaiteiNo)
        End Function

    End Class

End Namespace
