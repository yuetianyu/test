Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 図面テーブル群の簡単なCRUDを集めたDAO(パンダ)
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0700DaoImpl : Inherits EBomDaoEachTable(Of Rhac0700Vo)
        Implements Rhac0700Dao

        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Rhac0700Vo))
            Dim vo As New Rhac0700Vo
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
                                  ByVal zumenKaiteiNo As String) As Vo.Rhac0700Vo Implements Rhac0700Dao.FindByPk
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
                                    ByVal zumenKaiteiNo As String) As Integer Implements Rhac0700Dao.DeleteByPk
            Return DeleteByPkMain(zumenNo, zumenKaiteiNo)
        End Function

    End Class
End Namespace