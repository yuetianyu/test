Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>試作部品表ファイルの簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class AsPRPF02DaoImpl : Inherits EBomDaoEachTable(Of AsPRPF02Vo)
        Implements AsPRPF02Dao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.AsPRPF02Vo))
            Dim vo As New AsPRPF02Vo
            table.IsA(vo).PkField(vo.OldListCode) _
            .PkField(vo.Zrkg)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="OLD_LIST_CODE">旧リストコード</param>
        ''' <param name="ZRKG">材料記号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal OLD_LIST_CODE As String, _
                          ByVal ZRKG As String) As AsPRPF02Vo Implements AsPRPF02Dao.FindByPk

            Return FindByPkMain(OLD_LIST_CODE, ZRKG)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="OLD_LIST_CODE">旧リストコード</param>
        ''' <param name="ZRKG">材料記号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal OLD_LIST_CODE As String, _
                          ByVal ZRKG As String) As Integer Implements AsPRPF02Dao.DeleteByPk
            Return DeleteByPkMain(OLD_LIST_CODE, ZRKG)
        End Function

    End Class
End Namespace

