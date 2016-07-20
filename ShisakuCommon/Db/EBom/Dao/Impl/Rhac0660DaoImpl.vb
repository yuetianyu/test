Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 組織テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0660DaoImpl : Inherits EBomDaoEachTable(Of Rhac0660Vo)
        Implements Rhac0660Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0660Vo))
            Dim vo As New Rhac0660Vo
            table.IsA(vo) _
            .PkField(vo.BukaCode) _
            .PkField(vo.SiteKbn)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="siteKbn">サイト区分</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal bukaCode As String, _
                                  ByVal siteKbn As String) As Vo.Rhac0660Vo Implements Rhac0660Dao.FindByPk
            Return FindByPkMain(bukaCode, siteKbn)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="siteKbn">サイト区分</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal bukaCode As String, _
                                    ByVal siteKbn As String) As Integer Implements Rhac0660Dao.DeleteByPk
            Return DeleteByPkMain(bukaCode, siteKbn)
        End Function
    End Class
End Namespace

