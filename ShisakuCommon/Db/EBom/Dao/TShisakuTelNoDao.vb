Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>ユーザー電話番号情報の簡単なCRUDを集めたDAO</summary>
    Public Interface TShisakuTelNoDao : Inherits DaoEachTable(Of TShisakuTelNoVo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="userId">ユーザーID</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal userId As String) As TShisakuTelNoVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="userId">ユーザーID</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal userId As String) As Integer

    End Interface

End Namespace