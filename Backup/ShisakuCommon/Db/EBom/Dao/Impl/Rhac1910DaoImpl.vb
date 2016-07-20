Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 属性テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac1910DaoImpl : Inherits EBomDaoEachTable(Of Rhac1910Vo)
        Implements Rhac1910Dao

        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac1910Vo))
            Dim vo As New Rhac1910Vo
            table.IsA(vo) _
            .PkField(vo.KaihatsuFugo) _
            .PkField(vo.BuhinNo) _
            .PkField(vo.KaiteiNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal kaihatsuFugo As String, _
                          ByVal buhinNo As String, _
                          ByVal kaiteiNo As String) As Rhac1910Vo Implements Rhac1910Dao.FindByPk
            Return FindByPkMain(kaihatsuFugo, buhinNo, kaiteiNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal kaihatsuFugo As String, _
                            ByVal buhinNo As String, _
                            ByVal kaiteiNo As String) As Integer Implements Rhac1910Dao.DeleteByPk
            Return DeleteByPkMain(kaihatsuFugo, buhinNo, kaiteiNo)
        End Function

    End Class
End Namespace