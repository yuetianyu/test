Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>集計コードマスタの簡単なCRUDを集めたDAO</summary>
    Public Interface MSyukeiCodeDao : Inherits DaoEachTable(Of MSyukeiCodeVo)
        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="SyukeiCode">集計コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal SyukeiCode As String) As MSyukeiCodeVo

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="SyukeiCode">集計コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal SyukeiCode As String) As Integer
    End Interface
End Namespace