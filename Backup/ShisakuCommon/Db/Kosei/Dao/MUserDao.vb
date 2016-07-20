Imports ShisakuCommon.Db.Kosei.Vo

Namespace Db.Kosei.Dao
    ''' <summary>
    ''' ユーザーテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface MUserDao : Inherits DaoEachTable(Of MUserVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="userId">ユーザID</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal userId As String) As MUserVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="userId">ユーザID</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal userId As String) As Integer


    End Interface
End Namespace

