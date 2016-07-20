Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>現調品手配進捗情報の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class AsORPF61DaoImpl : Inherits EBomDaoEachTable(Of AsORPF61Vo)
        Implements AsORPF61Dao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.AsORPF61Vo))
            Dim vo As New AsORPF61Vo
            table.IsA(vo).PkField(vo.GRNO) _
            .PkField(vo.SRNO)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="GRNO">グループ№</param>
        ''' <param name="SRNO">シリアル№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal GRNO As String, _
                                 ByVal SRNO As String) As AsORPF61Vo Implements AsORPF61Dao.FindByPk

            Return FindByPkMain(GRNO, SRNO)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="GRNO">グループ№</param>
        ''' <param name="SRNO">シリアル№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal GRNO As String, _
                          ByVal SRNO As String) As Integer Implements AsORPF61Dao.DeleteByPk
            Return DeleteByPkMain(GRNO, SRNO)
        End Function

    End Class
End Namespace

