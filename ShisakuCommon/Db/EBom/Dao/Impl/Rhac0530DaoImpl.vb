Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 部品テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0530DaoImpl : Inherits EBomDaoEachTable(Of Rhac0530Vo)
        Implements Rhac0530Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0530Vo))
            Dim vo As New Rhac0530Vo
            table.IsA(vo) _
            .PkField(vo.BuhinNo) _
            .PkField(vo.KaiteiNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="kaiteiNo">改訂No.</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal buhinNo As String, _
                                  ByVal kaiteiNo As String) As Vo.Rhac0530Vo Implements Rhac0530Dao.FindByPk
            Return FindByPkMain(buhinNo, kaiteiNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="kaiteiNo">改訂No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal buhinNo As String, _
                                    ByVal kaiteiNo As String) As Integer Implements Rhac0530Dao.DeleteByPk
            Return DeleteByPkMain(buhinNo, kaiteiNo)
        End Function

    End Class
End Namespace