Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作部品構成情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuBuhinKouseiDao : Inherits DaoEachTable(Of TShisakuBuhinKouseiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番試作区分</param>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKbnOya">部品番号区分（親）</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="buhinNoKbnKo">部品番号区分（子）</param>
        ''' <param name="kaiteiNo">構成改訂No.</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, _
                          ByVal shisakuBlockNoKaiteiNo As String, _
                          ByVal shisakuGousya As String, _
                          ByVal instlHinban As String, _
                          ByVal instlHinbanKbn As String, _
                          ByVal buhinNoOya As String, _
                          ByVal buhinNoKbnOya As String, _
                          ByVal buhinNoKo As String, _
                          ByVal buhinNoKbnKo As String, _
                          ByVal kaiteiNo As String) As TShisakuBuhinKouseiVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番試作区分</param>
        ''' <param name="buhinNoOya">部品番号(親)</param>
        ''' <param name="buhinNoKbnOya">部品番号区分（親）</param>
        ''' <param name="buhinNoKo">部品番号(子)</param>
        ''' <param name="buhinNoKbnKo">部品番号区分（子）</param>
        ''' <param name="kaiteiNo">構成改訂No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, _
                            ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal shisakuGousya As String, _
                            ByVal instlHinban As String, _
                            ByVal instlHinbanKbn As String, _
                            ByVal buhinNoOya As String, _
                            ByVal buhinNoKbnOya As String, _
                            ByVal buhinNoKo As String, _
                            ByVal buhinNoKbnKo As String, _
                            ByVal kaiteiNo As String) As Integer

    End Interface
End Namespace


