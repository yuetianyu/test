Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>試作手配帳改訂抽出ブロック情報の簡単なCRUDを集めたDAO</summary>
    Public Interface TShisakuTehaiKaiteiBlockDao : Inherits DaoEachTable(Of TShisakuTehaiKaiteiBlockVo)
        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuListCode As String, _
                          ByVal ShisakuListCodeKaiteiNo As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String) As TShisakuTehaiKaiteiBlockVo

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuBukaCode">試作部課コード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuListCode As String, _
                          ByVal ShisakuListCodeKaiteiNo As String, _
                          ByVal ShisakuBukaCode As String, _
                          ByVal ShisakuBlockNo As String) As Integer
    End Interface
End Namespace