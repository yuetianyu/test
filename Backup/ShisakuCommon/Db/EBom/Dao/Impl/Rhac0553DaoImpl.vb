Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 部品テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0553DaoImpl : Inherits EBomDaoEachTable(Of Rhac0553Vo)
        Implements Rhac0553Dao

        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0553Vo))
            Dim vo As New Rhac0553Vo
            table.IsA(vo) _
            .PkField(vo.KaihatsuFugo) _
            .PkField(vo.BuhinNoOya) _
            .PkField(vo.BuhinNoKo) _
            .PkField(vo.KaiteiNo)

        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="kaiteiNo">改訂No.</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal kaihatsuFugo As String, _
                          ByVal buhinNoOya As String, _
                          ByVal buhinNoKo As String, _
                          ByVal kaiteiNo As String) As Rhac0553Vo Implements Rhac0553Dao.FindByPk
            Return FindByPkMain(kaihatsuFugo, buhinNoOya, buhinNoKo, kaiteiNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="kaiteiNo">改訂No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal kaihatsuFugo As String, _
                            ByVal buhinNoOya As String, _
                            ByVal buhinNoKo As String, _
                            ByVal kaiteiNo As String) As Integer Implements Rhac0553Dao.DeleteByPk
            Return DeleteByPkMain(kaihatsuFugo, buhinNoOya, buhinNoKo, kaiteiNo)
        End Function


    End Class
End Namespace