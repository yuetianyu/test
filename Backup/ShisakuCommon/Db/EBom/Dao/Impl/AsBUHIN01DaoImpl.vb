Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>AS400部品マスタの簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class AsBUHIN01DaoImpl : Inherits EBomDaoEachTable(Of AsBUHIN01Vo)
        Implements AsBUHIN01Dao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.AsBUHIN01Vo))
            Dim vo As New AsBUHIN01Vo
            table.IsA(vo).PkField(vo.STSR) _
            .PkField(vo.RSKM) _
            .PkField(vo.DHSTBA) _
            .PkField(vo.GZZCP)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="STSR">設通シリーズ</param>
        ''' <param name="RSKM">量試区分</param>
        ''' <param name="DHSTBA">設通№</param>
        ''' <param name="GZZCP">集合図面－図面番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal STSR As String, _
                          ByVal RSKM As String, _
                          ByVal DHSTBA As String, _
                          ByVal GZZCP As String) As AsBUHIN01Vo Implements AsBUHIN01Dao.FindByPk

            Return FindByPkMain(STSR, RSKM, DHSTBA, GZZCP)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="STSR">設通シリーズ</param>
        ''' <param name="RSKM">量試区分</param>
        ''' <param name="DHSTBA">設通№</param>
        ''' <param name="GZZCP">集合図面－図面番号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal STSR As String, _
                          ByVal RSKM As String, _
                          ByVal DHSTBA As String, _
                          ByVal GZZCP As String) As Integer Implements AsBUHIN01Dao.DeleteByPk
            Return DeleteByPkMain(STSR, RSKM, DHSTBA, GZZCP)
        End Function

    End Class
End Namespace

