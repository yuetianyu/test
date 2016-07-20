Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 属性テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac1930DaoImpl : Inherits EBomDaoEachTable(Of Rhac1930Vo)
        Implements Rhac1930Dao

        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac1930Vo))
            Dim vo As New Rhac1930Vo
            table.IsA(vo) _
            .PkField(vo.KaihatsuFugo) _
            .PkField(vo.BuhinNo) _
            .PkField(vo.BuhinKataKaiteiNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="buhinKataKaiteiNo">部品型改訂No</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal kaihatsuFugo As String, _
                          ByVal buhinNo As String, _
                          ByVal buhinKataKaiteiNo As String) As Rhac1930Vo Implements Rhac1930Dao.FindByPk
            Return FindByPkMain(kaihatsuFugo, buhinNo, buhinKataKaiteiNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="buhinKataKaiteiNo">部品型改訂No</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal kaihatsuFugo As String, _
                            ByVal buhinNo As String, _
                            ByVal buhinKataKaiteiNo As String) As Integer Implements Rhac1930Dao.DeleteByPk
            Return DeleteByPkMain(kaihatsuFugo, buhinNo, buhinKataKaiteiNo)
        End Function


    End Class
End Namespace