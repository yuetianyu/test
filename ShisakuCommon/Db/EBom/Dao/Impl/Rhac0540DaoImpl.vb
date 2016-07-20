Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 12桁型式テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac0540DaoImpl : Inherits EBomDaoEachTable(Of Rhac0540Vo)
        Implements Rhac0540Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.Rhac0540Vo))
            Dim vo As New Rhac0540Vo
            table.IsA(vo) _
            .PkField(vo.KaihatsuFugo) _
            .PkField(vo.SobiKaiteiNo) _
            .PkField(vo.KatashikiScd7) _
            .PkField(vo.ShimukechiCode) _
            .PkField(vo.OpKaiteiNo) _
            .PkField(vo.OpCode)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="sobiKaiteiNo">装備改訂No.</param>
        ''' <param name="katashikiScd7">7桁型式識別コード</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opKaiteiNo">OP改訂No.</param>
        ''' <param name="opCode">OPコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk1(ByVal kaihatsuFugo As String, _
                                  ByVal sobiKaiteiNo As String, _
                                  ByVal katashikiScd7 As String, _
                                  ByVal shimukechiCode As String, _
                                  ByVal opKaiteiNo As String, _
                                  ByVal opCode As String) As Vo.Rhac0540Vo Implements Rhac0540Dao.FindByPk
            Return FindByPkMain(kaihatsuFugo, sobiKaiteiNo, katashikiScd7, shimukechiCode, opKaiteiNo, opCode)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="sobiKaiteiNo">装備改訂No.</param>
        ''' <param name="katashikiScd7">7桁型式識別コード</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opKaiteiNo">OP改訂No.</param>
        ''' <param name="opCode">OPコード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk1(ByVal kaihatsuFugo As String, _
                                    ByVal sobiKaiteiNo As String, _
                                    ByVal katashikiScd7 As String, _
                                    ByVal shimukechiCode As String, _
                                    ByVal opKaiteiNo As String, _
                                    ByVal opCode As String) As Integer Implements Rhac0540Dao.DeleteByPk
            Return DeleteByPkMain(kaihatsuFugo, sobiKaiteiNo, katashikiScd7, shimukechiCode, opKaiteiNo, opCode)
        End Function
    End Class
End Namespace

