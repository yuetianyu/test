Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作リストコード情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuListcodeDao : Inherits DaoEachTable(Of TShisakuListcodeVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListHyojijunNo">試作リスト表示順</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuGroupNo">試作グループ№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuListHyojijunNo As Nullable(Of Int32), _
                          ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, _
                          ByVal shisakuGroupNo As String) As TShisakuListcodeVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListHyojijunNo">試作リスト表示順</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuGroupNo">試作グループ№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal shisakuListHyojijunNo As Nullable(Of Int32), _
                            ByVal shisakuListCode As String, _
                            ByVal shisakuListCodeKaiteiNo As String, _
                            ByVal shisakuGroupNo As String) As Integer
    End Interface
End Namespace