Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作メニューログインマスターテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>

    Public Interface MShisakuLoginDao : Inherits DaoEachTable(Of MShisakuLoginVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="sekkeiShainNo">設計社員番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal sekkeiShainNo As String) As MShisakuLoginVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="sekkeiShainNo">設計社員番号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal sekkeiShainNo As String) As Integer


    End Interface
End Namespace

