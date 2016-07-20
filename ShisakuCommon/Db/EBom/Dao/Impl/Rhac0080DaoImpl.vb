Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 開発車機能ブロックテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0080DaoImpl : Inherits EBomDaoEachTable(Of Rhac0080Vo)
        Implements Rhac0080Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0080Vo))
            Dim vo As New Rhac0080Vo
            table.IsA(vo) _
            .PkField(vo.KaihatsuFugo) _
            .PkField(vo.BlockNoKino) _
            .PkField(vo.KaiteiNoKino)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="blockNoKino">ブロックNo.(機能)</param>
        ''' <param name="kaiteiNoKino">改訂No.(機能)</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal kaihatsuFugo As String, _
                                  ByVal blockNoKino As String, _
                                  ByVal kaiteiNoKino As String) As Vo.Rhac0080Vo Implements Rhac0080Dao.FindByPk
            Return FindByPkMain(kaihatsuFugo, blockNoKino,kaiteiNoKino)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="blockNoKino">ブロックNo.(機能)</param>
        ''' <param name="kaiteiNoKino">改訂No.(機能)</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal kaihatsuFugo As String, _
                                    ByVal blockNoKino As String, _
                                    ByVal kaiteiNoKino As String) As Integer Implements Rhac0080Dao.DeleteByPk
            Return DeleteByPkMain(kaihatsuFugo, blockNoKino, kaiteiNoKino)
        End Function
    End Class
End Namespace
