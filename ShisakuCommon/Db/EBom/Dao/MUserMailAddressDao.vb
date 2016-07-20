Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>ユーザーメールアドレスマスタの簡単なCRUDを集めたDAO</summary>
    Public Interface MUserMailAddressDao : Inherits DaoEachTable(Of MUserMailAddressVo)
        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="UserId">ユーザーID</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal UserId As String) As MUserMailAddressVo

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="UserId">ユーザーID</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal UserId As String) As Integer
    End Interface
End Namespace