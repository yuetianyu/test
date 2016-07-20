Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 属性テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>

    Public Class Rhac1940DaoImpl : Inherits EBomDaoEachTable(Of Rhac1940Vo)
        Implements Rhac1940Dao

        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac1940Vo))
            Dim vo As New Rhac1940Vo
            table.IsA(vo) _
            .PkField(vo.KataDaihyouBuhinNo) _
            .PkField(vo.KataKaiteiNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kataDaihyouBuhinNo">型代表部品番号</param>
        ''' <param name="kataKaiteiNo">型改訂No</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal kataDaihyouBuhinNo As String, _
                          ByVal kataKaiteiNo As String) As Rhac1940Vo Implements Rhac1940Dao.FindByPk
            Return FindByPkMain(kataDaihyouBuhinNo, kataKaiteiNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kataDaihyouBuhinNo">型代表部品番号</param>
        ''' <param name="kataKaiteiNo">型改訂No</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal kataDaihyouBuhinNo As String, _
                            ByVal kataKaiteiNo As String) As Integer Implements Rhac1940Dao.DeleteByPk
            Return DeleteByPkMain(kataDaihyouBuhinNo, kataKaiteiNo)
        End Function

    End Class
End Namespace