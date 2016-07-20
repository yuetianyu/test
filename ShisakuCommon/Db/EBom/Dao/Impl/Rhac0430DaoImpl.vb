Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' カラーテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0430DaoImpl : Inherits EBomDaoEachTable(Of Rhac0430Vo)
        Implements Rhac0430Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0430Vo))
            Dim vo As New Rhac0430Vo
            table.IsA(vo) _
            .PkField(vo.ColorCode) _
            .PkField(vo.NaigaisoKbn)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="colorCode">内外装区分</param>
        ''' <param name="naigaisoKbn">カラー名称</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal colorCode As String, _
                                  ByVal naigaisoKbn As String) As Vo.Rhac0430Vo Implements Rhac0430Dao.FindByPk
            Return FindByPkMain(colorCode, naigaisoKbn)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="colorCode">内外装区分</param>
        ''' <param name="naigaisoKbn">カラー名称</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal colorCode As String, _
                                    ByVal naigaisoKbn As String) As Integer Implements Rhac0430Dao.DeleteByPk
            Return DeleteByPkMain(colorCode, naigaisoKbn)
        End Function
    End Class
End Namespace

