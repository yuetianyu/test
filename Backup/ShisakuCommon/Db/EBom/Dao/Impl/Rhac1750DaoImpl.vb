Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 12桁型式-F品番テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class Rhac1750DaoImpl : Inherits EBomDaoEachTable(Of Rhac1750Vo)
        Implements Rhac1750Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Rhac1750Vo))
            Dim vo As New Rhac1750Vo
            table.IsA(vo) _
            .PkField(vo.KaihatsuFugo) _
            .PkField(vo.SobiKaiteiNo) _
            .PkField(vo.KatashikiScd7) _
            .PkField(vo.ShimukechiCode) _
            .PkField(vo.ColNo) _
            .PkField(vo.FBuhinNo) _
            .PkField(vo.ShiyosaKaiteiNo) _
            .PkField(vo.BuiCode) _
            .PkField(vo.KumiawaseCodeSo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kaihatsuFugo">試作車系</param>
        ''' <param name="sobiKaiteiNo">試作開発符号</param>
        ''' <param name="katashikiScd7">表示順</param>
        ''' <param name="shimukechiCode">試作車系</param>
        ''' <param name="colNo">試作開発符号</param>
        ''' <param name="fBuhinNo">表示順</param>
        ''' <param name="shiyosaKaiteiNo">表示順</param>
        ''' <param name="buiCode">表示順</param>
        ''' <param name="kumiawaseCodeSo">表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal kaihatsuFugo As String, _
                                 ByVal sobiKaiteiNo As String, _
                                 ByVal katashikiScd7 As String, _
                                 ByVal shimukechiCode As String, _
                                 ByVal colNo As Nullable(Of Int32), _
                                 ByVal fBuhinNo As String, _
                                 ByVal shiyosaKaiteiNo As String, _
                                 ByVal buiCode As String, _
                                 ByVal kumiawaseCodeSo As Nullable(Of Int32)) As Vo.Rhac1750Vo Implements Rhac1750Dao.FindByPk
            Return FindByPkMain(kaihatsuFugo, _
                                sobiKaiteiNo, _
                                katashikiScd7, _
                                shimukechiCode, _
                                colNo, _
                                fBuhinNo, _
                                shiyosaKaiteiNo, _
                                buiCode, _
                                kumiawaseCodeSo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kaihatsuFugo">試作車系</param>
        ''' <param name="sobiKaiteiNo">試作開発符号</param>
        ''' <param name="katashikiScd7">表示順</param>
        ''' <param name="shimukechiCode">試作車系</param>
        ''' <param name="colNo">試作開発符号</param>
        ''' <param name="fBuhinNo">表示順</param>
        ''' <param name="shiyosaKaiteiNo">表示順</param>
        ''' <param name="buiCode">表示順</param>
        ''' <param name="kumiawaseCodeSo">表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal kaihatsuFugo As String, _
                                   ByVal sobiKaiteiNo As String, _
                                   ByVal katashikiScd7 As String, _
                                   ByVal shimukechiCode As String, _
                                   ByVal colNo As Nullable(Of Int32), _
                                   ByVal fBuhinNo As String, _
                                   ByVal shiyosaKaiteiNo As String, _
                                   ByVal buiCode As String, _
                                   ByVal kumiawaseCodeSo As Nullable(Of Int32)) As Integer Implements Rhac1750Dao.DeleteByPk
            Return DeleteByPkMain(kaihatsuFugo, _
                                  sobiKaiteiNo, _
                                  katashikiScd7, _
                                  shimukechiCode, _
                                  colNo, _
                                  fBuhinNo, _
                                  shiyosaKaiteiNo, _
                                  buiCode, _
                                  kumiawaseCodeSo)
        End Function

    End Class
End Namespace


