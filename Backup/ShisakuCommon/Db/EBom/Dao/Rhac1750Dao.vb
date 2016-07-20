Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 12桁型式-F品番テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac1750Dao : Inherits DaoEachTable(Of Rhac1750Vo)
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
        Function FindByPk(ByVal kaihatsuFugo As String, _
                          ByVal sobiKaiteiNo As String, _
                          ByVal katashikiScd7 As String, _
                          ByVal shimukechiCode As String, _
                          ByVal colNo As Nullable(Of Int32), _
                          ByVal fBuhinNo As String, _
                          ByVal shiyosaKaiteiNo As String, _
                          ByVal buiCode As String, _
                          ByVal kumiawaseCodeSo As Nullable(Of Int32)) As Rhac1750Vo
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
        Function DeleteByPk(ByVal kaihatsuFugo As String, _
                            ByVal sobiKaiteiNo As String, _
                            ByVal katashikiScd7 As String, _
                            ByVal shimukechiCode As String, _
                            ByVal colNo As Nullable(Of Int32), _
                            ByVal fBuhinNo As String, _
                            ByVal shiyosaKaiteiNo As String, _
                            ByVal buiCode As String, _
                            ByVal kumiawaseCodeSo As Nullable(Of Int32)) As Integer

    End Interface
End Namespace

