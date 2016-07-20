Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>新調達手配進捗情報の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class AsORPF60DaoImpl : Inherits EBomDaoEachTable(Of AsORPF60Vo)
        Implements AsORPF60Dao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.AsORPF60Vo))
            Dim vo As New AsORPF60Vo
            table.IsA(vo).PkField(vo.SGISBA) _
            .PkField(vo.KBBA) _
            .PkField(vo.CMBA) _
            .PkField(vo.NOKM) _
            .PkField(vo.HAYM)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="SGISBA">作業依頼書№</param>
        ''' <param name="KBBA">管理№</param>
        ''' <param name="CMBA">注文書№</param>
        ''' <param name="NOKM">納入区分</param>
        ''' <param name="HAYM">発注年月日</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal SGISBA As String, _
                                 ByVal KBBA As String, _
                                 ByVal CMBA As String, _
                                 ByVal NOKM As Char, _
                                 ByVal HAYM As Integer) As AsORPF60Vo Implements AsORPF60Dao.FindByPk

            Return FindByPkMain(SGISBA, KBBA, CMBA, NOKM, HAYM)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="SGISBA">作業依頼書№</param>
        ''' <param name="KBBA">管理№</param>
        ''' <param name="CMBA">注文書№</param>
        ''' <param name="NOKM">納入区分</param>
        ''' <param name="HAYM">発注年月日</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal SGISBA As String, _
                            ByVal KBBA As String, _
                            ByVal CMBA As String, _
                            ByVal NOKM As Char, _
                            ByVal HAYM As Integer) As Integer Implements AsORPF60Dao.DeleteByPk
            Return DeleteByPkMain(SGISBA, KBBA, CMBA, NOKM, HAYM)
        End Function

    End Class
End Namespace

