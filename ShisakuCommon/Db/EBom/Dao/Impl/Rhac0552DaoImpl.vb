Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 部品構成(図面テーブル群)テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0552DaoImpl : Inherits EBomDaoEachTable(Of Rhac0552Vo)
        Implements Rhac0552Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0552Vo))
            Dim vo As New Rhac0552Vo
            table.IsA(vo) _
            .PkField(vo.BuhinNoOya) _
            .PkField(vo.BuhinNoKo) _
            .PkField(vo.KaiteiNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="kaiteiNo">構成改訂No.</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal buhinNoOya As String, _
                                  ByVal buhinNoKo As String, _
                                  ByVal kaiteiNo As String) As Vo.Rhac0552Vo Implements Rhac0552Dao.FindByPk
            Return FindByPkMain(buhinNoOya, buhinNoKo, kaiteiNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="kaiteiNo">構成改訂No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal buhinNoOya As String, _
                                    ByVal buhinNoKo As String, _
                                    ByVal kaiteiNo As String) As Integer Implements Rhac0552Dao.DeleteByPk
            Return DeleteByPkMain(buhinNoOya, buhinNoKo, kaiteiNo)
        End Function
    End Class
End Namespace

