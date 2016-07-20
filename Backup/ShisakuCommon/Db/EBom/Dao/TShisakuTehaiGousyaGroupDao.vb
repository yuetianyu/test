Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>
    '''試作手配帳情報（号車グループ情報）の簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuTehaiGousyaGroupDao : Inherits DaoEachTable(Of TShisakuTehaiGousyaGroupVo)

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuGousya">試作号車号車</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuListCode As String, _
                          ByVal ShisakuListCodeKaiteiNo As String, _
                          ByVal ShisakuGousya As String) As TShisakuTehaiGousyaGroupVo
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuGousya">試作号車</param>
        ''' <returns>削除件数ド</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal ShisakuEventCode As String, _
                          ByVal ShisakuListCode As String, _
                          ByVal ShisakuListCodeKaiteiNo As String, _
                          ByVal ShisakuGousya As String) As Integer
  

    End Interface
End Namespace