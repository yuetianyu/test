Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 社員マスター情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface Rhac2130Dao : Inherits DaoEachTable(Of Rhac2130Vo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shainNo">社員番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shainNo As String) As Rhac2130Vo
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shainNo">社員番号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shainNo As String) As Integer

    End Interface
End Namespace

