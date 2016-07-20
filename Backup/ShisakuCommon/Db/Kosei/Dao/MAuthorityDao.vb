Imports ShisakuCommon.Db.Kosei.Vo

Namespace Db.Kosei.Dao
    ''' <summary>
    ''' 権限テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface MAuthorityDao : Inherits DaoEachTable(Of MAuthorityVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="menuDaiKbn">メニュー大区分</param>
        ''' <param name="menuKbn">メニュー区分</param>
        ''' <param name="userKbn">ユーザ区分</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal menuDaiKbn As Nullable(Of Int32), ByVal menuKbn As Nullable(Of Int32) _
                          , ByVal userKbn As Nullable(Of Int32)) As MAuthorityVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="menuDaiKbn">メニュー大区分</param>
        ''' <param name="menuKbn">メニュー区分</param>
        ''' <param name="userKbn">ユーザ区分</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal menuDaiKbn As Nullable(Of Int32), ByVal menuKbn As Nullable(Of Int32) _
                          , ByVal userKbn As Nullable(Of Int32)) As Integer


    End Interface
End Namespace
