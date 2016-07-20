Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    Public Interface AsPAPF14Dao
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="TORICD">取引先コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal TORICD As String) As AsPAPF14Vo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="TORICD">取引先コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal TORICD As String) As Integer

    End Interface
End Namespace