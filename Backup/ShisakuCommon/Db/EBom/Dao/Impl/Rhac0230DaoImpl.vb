Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 7桁型式テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0230DaoImpl : Inherits EBomDaoEachTable(Of Rhac0230Vo)
        Implements Rhac0230Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0230Vo))
            Dim vo As New Rhac0230Vo
            table.IsA(vo) _
            .PkField(vo.KaihatsuFugo) _
            .PkField(vo.SobiKaiteiNo) _
            .PkField(vo.KatashikiScd7)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="sobiKaiteiNo">装備改訂No.</param>
        ''' <param name="katashikiScd7">7桁型式識別コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal kaihatsuFugo As String, _
                                  ByVal sobiKaiteiNo As String, _
                                  ByVal katashikiScd7 As String) As Vo.Rhac0230Vo Implements Rhac0230Dao.FindByPk
            Return FindByPkMain(kaihatsuFugo, sobiKaiteiNo, katashikiScd7)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="sobiKaiteiNo">装備改訂No.</param>
        ''' <param name="katashikiScd7">7桁型式識別コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal kaihatsuFugo As String, _
                                    ByVal sobiKaiteiNo As String, _
                                    ByVal katashikiScd7 As String) As Integer Implements Rhac0230Dao.DeleteByPk
            Return DeleteByPkMain(kaihatsuFugo, sobiKaiteiNo, katashikiScd7)
        End Function
    End Class
End Namespace


