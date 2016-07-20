Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>新調達手配進捗情報の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class AsORPF57DaoImpl : Inherits EBomDaoEachTable(Of AsORPF57Vo)
        Implements AsORPF57Dao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.AsORPF57Vo))
            Dim vo As New AsORPF57Vo
            table.IsA(vo).PkField(vo.KOMZBA) _
            .PkField(vo.GRNO) _
            .PkField(vo.SRNO)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="KOMZBA">工事指令書№</param>
        ''' <param name="GRNO">グループ№</param>
        ''' <param name="SRNO">シリアル№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal KOMZBA As String, _
                                 ByVal GRNO As String, _
                                 ByVal SRNO As String) As AsORPF57Vo Implements AsORPF57Dao.FindByPk

            Return FindByPkMain(KOMZBA, GRNO, SRNO)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="KOMZBA">工事指令書№</param>
        ''' <param name="GRNO">グループ№</param>
        ''' <param name="SRNO">シリアル№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal KOMZBA As String, _
                                   ByVal GRNO As String, _
                                   ByVal SRNO As String) As Integer Implements AsORPF57Dao.DeleteByPk
            Return DeleteByPkMain(KOMZBA, GRNO, SRNO)
        End Function

    End Class
End Namespace

