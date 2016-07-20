Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 仕様書マスターテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0030DaoImpl : Inherits EBomDaoEachTable(Of Rhac0030Vo)
        Implements Rhac0030Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0030Vo))
            Dim vo As New Rhac0030Vo
            table.IsA(vo) _
            .PkField(vo.KaihatsuFugo) _
            .PkField(vo.SobiKaiteiNo) _
            .PkField(vo.LineOpKaiteiNo) _
            .PkField(vo.NaigaisoKaiteiNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">試作装備区分</param>
        ''' <param name="sobiKaiteiNo">列項目コード</param>
        ''' <param name="lineOpKaiteiNo">試作装備区分</param>
        ''' <param name="naigaisoKaiteiNo">列項目コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal kaihatsuFugo As String, _
                                  ByVal sobiKaiteiNo As String, _
                                  ByVal lineOpKaiteiNo As String, _
                                  ByVal naigaisoKaiteiNo As String) As Vo.Rhac0030Vo Implements Rhac0030Dao.FindByPk
            Return FindByPkMain(kaihatsuFugo, sobiKaiteiNo, lineOpKaiteiNo, naigaisoKaiteiNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">試作装備区分</param>
        ''' <param name="sobiKaiteiNo">列項目コード</param>
        ''' <param name="lineOpKaiteiNo">試作装備区分</param>
        ''' <param name="naigaisoKaiteiNo">列項目コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal kaihatsuFugo As String, _
                                    ByVal sobiKaiteiNo As String, _
                                    ByVal lineOpKaiteiNo As String, _
                                    ByVal naigaisoKaiteiNo As String) As Integer Implements Rhac0030Dao.DeleteByPk
            Return DeleteByPkMain(kaihatsuFugo, sobiKaiteiNo, lineOpKaiteiNo, naigaisoKaiteiNo)
        End Function
    End Class
End Namespace

