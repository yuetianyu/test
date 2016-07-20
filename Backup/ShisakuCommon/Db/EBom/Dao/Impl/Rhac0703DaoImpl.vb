Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 図面テーブル群の簡単なCRUDを集めたDAO(FM5以降)
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0703DaoImpl : Inherits EBomDaoEachTable(Of Rhac0703Vo)
        Implements Rhac0703Dao

        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0703Vo))
            Dim vo As New Rhac0703Vo
            table.IsA(vo) _
            .PkField(vo.ZumenNo) _
            .PkField(vo.ZumenKaiteiNo) _
            .PkField(vo.KaihatsuFugo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <param name="zumenKaiteiNo">図面改訂No.</param>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal zumenNo As String, _
                                  ByVal zumenKaiteiNo As String, _
                                  ByVal kaihatsuFugo As String) As Vo.Rhac0703Vo Implements Rhac0703Dao.FindByPk
            Return FindByPkMain(zumenNo, zumenKaiteiNo, kaihatsuFugo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="zumenNo">図面番号</param>
        ''' <param name="zumenKaiteiNo">図面改訂No.</param>
        ''' 
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal zumenNo As String, _
                                    ByVal zumenKaiteiNo As String, _
                                    ByVal kaihatsuFugo As String) As Integer Implements Rhac0703Dao.DeleteByPk
            Return DeleteByPkMain(zumenNo, zumenKaiteiNo, kaihatsuFugo)
        End Function

    End Class
End Namespace