Imports ShisakuCommon.Db.Kosei.Vo

Namespace Db.Kosei.Dao
    ''' <summary>
    ''' 権限(ユーザー別)テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface MAuthorityUserDao : Inherits DaoEachTable(Of MAuthorityUserVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="appNo">アプリケーション管理No.  </param>
        ''' <param name="kinoId1">機能ID1</param>
        ''' <param name="kinoId2">機能ID2</param>
        ''' <param name="kinoId3">機能ID3</param>
        ''' <param name="userId">ﾕｰｻﾞｰID </param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal appNo As String, _
                          ByVal kinoId1 As String, _
                          ByVal kinoId2 As String, _
                          ByVal kinoId3 As String, _
                          ByVal userId As String) As MAuthorityUserVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="appNo">アプリケーション管理No.  </param>
        ''' <param name="kinoId1">機能ID1</param>
        ''' <param name="kinoId2">機能ID2</param>
        ''' <param name="kinoId3">機能ID3</param>
        ''' <param name="userId">ﾕｰｻﾞｰID </param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal appNo As String, _
                            ByVal kinoId1 As String, _
                            ByVal kinoId2 As String, _
                            ByVal kinoId3 As String, _
                            ByVal userId As String) As Integer
    End Interface
End Namespace
