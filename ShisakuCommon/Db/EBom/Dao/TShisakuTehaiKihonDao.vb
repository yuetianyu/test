Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作手配帳情報（基本情報）テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuTehaiKihonDao : Inherits DaoEachTable(Of TShisakuTehaiKihonVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As TShisakuTehaiKihonVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As Integer
    End Interface
End Namespace