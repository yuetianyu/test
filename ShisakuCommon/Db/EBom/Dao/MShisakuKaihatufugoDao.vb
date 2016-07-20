Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 車系／開発符号マスターテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface MShisakuKaihatufugoDao : Inherits DaoEachTable(Of MShisakuKaihatufugoVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuShakeiCode">試作車系</param>
        ''' <param name="shisakuKaihatsuFugo">試作開発符号</param>
        ''' <param name="hyojijunNo">表示順 </param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuShakeiCode As String, _
                          ByVal shisakuKaihatsuFugo As String, _
                          ByVal hyojijunNo As Nullable(Of Int32)) As MShisakuKaihatufugoVo
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuShakeiCode">試作車系</param>
        ''' <param name="shisakuKaihatsuFugo">試作開発符号</param>
        ''' <param name="hyojijunNo">表示順 </param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuShakeiCode As String, _
                          ByVal shisakuKaihatsuFugo As String, _
                          ByVal hyojijunNo As Nullable(Of Int32)) As Integer

    End Interface
End Namespace

