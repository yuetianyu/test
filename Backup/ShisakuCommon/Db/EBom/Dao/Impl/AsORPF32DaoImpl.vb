Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>発注納入状況ファイルの簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class AsORPF32DaoImpl : Inherits EBomDaoEachTable(Of AsORPF32Vo)
        Implements AsORPF32Dao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.AsORPF32Vo))
            Dim vo As New AsORPF32Vo
            table.IsA(vo).PkField(vo.SGISBA) _
            .PkField(vo.KBBA)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="SGISBA">作業依頼書№</param>
        ''' <param name="KBBA">管理№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal SGISBA As String, _
                          ByVal KBBA As String) As AsORPF32Vo Implements AsORPF32Dao.FindByPk

            Return FindByPkMain(SGISBA, KBBA)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="SGISBA">作業依頼書№</param>
        ''' <param name="KBBA">管理№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal SGISBA As String, _
                          ByVal KBBA As String) As Integer Implements AsORPF32Dao.DeleteByPk
            Return DeleteByPkMain(SGISBA, KBBA)
        End Function

    End Class
End Namespace

