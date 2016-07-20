Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 部品テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0551DaoImpl : Inherits EBomDaoEachTable(Of Rhac0551Vo)
        Implements Rhac0551Dao

        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0551Vo))
            Dim vo As New Rhac0551Vo
            table.IsA(vo) _
            .PkField(vo.BuhinNoOya) _
            .PkField(vo.BuhinNoKo) _
            .PkField(vo.KaiteiNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="kaiteiNo">改訂No.</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal buhinNoOya As String, _
                          ByVal buhinNoKo As String, _
                          ByVal kaiteiNo As String) As Rhac0551Vo Implements Rhac0551Dao.FindByPk
            Return FindByPkMain(buhinNoOya, buhinNoKo, kaiteiNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="kaiteiNo">改訂No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal buhinNoOya As String, _
                            ByVal buhinNoKo As String, _
                            ByVal kaiteiNo As String) As Integer Implements Rhac0551Dao.DeleteByPk
            Return DeleteByPkMain(buhinNoOya, buhinNoKo, kaiteiNo)

        End Function

    End Class
End Namespace