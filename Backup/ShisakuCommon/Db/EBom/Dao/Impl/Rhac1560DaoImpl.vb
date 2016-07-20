Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 課マスタテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac1560DaoImpl : Inherits EBomDaoEachTable(Of Rhac1560Vo)
        Implements Rhac1560Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac1560Vo))
            Dim vo As New Rhac1560Vo
            table.IsA(vo) _
            .PkField(vo.BuCode) _
            .PkField(vo.SiteKbn) _
            .PkField(vo.KaCode)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="buCode">試作装備区分</param>
        ''' <param name="siteKbn">列項目コード</param>
        ''' <param name="kaCode">列項目コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal buCode As String, _
                                  ByVal siteKbn As String, _
                                  ByVal kaCode As String) As Vo.Rhac1560Vo Implements Rhac1560Dao.FindByPk
            Return FindByPkMain(buCode, siteKbn, kaCode)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="buCode">試作装備区分</param>
        ''' <param name="siteKbn">列項目コード</param>
        ''' <param name="kaCode">列項目コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal buCode As String, _
                                    ByVal siteKbn As String, _
                                    ByVal kaCode As String) As Integer Implements Rhac1560Dao.DeleteByPk
            Return DeleteByPkMain(buCode, siteKbn, kaCode)
        End Function
    End Class
End Namespace

