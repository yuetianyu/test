Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' カラー12桁型式適用テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac1630DaoImpl : Inherits EBomDaoEachTable(Of Rhac1630Vo)
        Implements Rhac1630Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Rhac1630Vo))
            Dim vo As New Rhac1630Vo
            table.IsA(vo) _
            .PkField(vo.KaihatsuFugo) _
            .PkField(vo.SobiKaiteiNo) _
            .PkField(vo.ColorCode) _
            .PkField(vo.ShimukechiCode) _
            .PkField(vo.OpSpecCode) _
            .PkField(vo.KatashikiScd7) _
            .PkField(vo.ColNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="sobiKaiteiNo">装備改訂No.</param>
        ''' <param name="colorCode">カラーコード</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opSpecCode">OPスペックコード</param>
        ''' <param name="katashikiScd7">7桁型式識別コード</param>
        ''' <param name="colNo">列No.</param>
        ''' <returns>該当レコード</returns>       
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal kaihatsuFugo As String, _
                                 ByVal sobiKaiteiNo As String, _
                                 ByVal colorCode As String, _
                                 ByVal shimukechiCode As String, _
                                 ByVal opSpecCode As String, _
                                 ByVal katashikiScd7 As String, _
                                 ByVal colNo As Nullable(Of Int32)) As Vo.Rhac1630Vo Implements Rhac1630Dao.FindByPk
            Return FindByPkMain(kaihatsuFugo, sobiKaiteiNo, colorCode, shimukechiCode, opSpecCode, katashikiScd7, colNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="sobiKaiteiNo">装備改訂No.</param>
        ''' <param name="colorCode">カラーコード</param>
        ''' <param name="shimukechiCode">仕向地コード</param>
        ''' <param name="opSpecCode">OPスペックコード</param>
        ''' <param name="katashikiScd7">7桁型式識別コード</param>
        ''' <param name="colNo">列No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>         
        Public Function DeleteByPk(ByVal kaihatsuFugo As String, _
                                   ByVal sobiKaiteiNo As String, _
                                   ByVal colorCode As String, _
                                   ByVal shimukechiCode As String, _
                                   ByVal opSpecCode As String, _
                                   ByVal katashikiScd7 As String, _
                                   ByVal colNo As Nullable(Of Int32)) As Integer Implements Rhac1630Dao.DeleteByPk
            Return DeleteByPkMain(kaihatsuFugo, sobiKaiteiNo, colorCode, shimukechiCode, opSpecCode, katashikiScd7, colNo)
        End Function

    End Class
End Namespace


