Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作設計ブロックINSTL情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuSekkeiBlockInstlDao : Inherits DaoEachTable(Of TShisakuSekkeiBlockInstlVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <param name="InstlDataKbn">INSTL元データ区分</param>
        ''' <param name="BaseInstlFlg">ベース情報フラグ</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, _
                          ByVal shisakuBlockNoKaiteiNo As String, _
                          ByVal shisakuGousya As String, _
                          ByVal instlHinban As String, _
                          ByVal instlHinbanKbn As String, _
                          ByVal InstlDataKbn As String, _
                          ByVal BaseInstlFlg As String) As TShisakuSekkeiBlockInstlVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <param name="InstlDataKbn">INSTL元データ区分</param>
        ''' <param name="BaseInstlFlg">ベース情報フラグ</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, _
                            ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal shisakuGousya As String, _
                            ByVal instlHinban As String, _
                            ByVal instlHinbanKbn As String, _
                            ByVal InstlDataKbn As String, _
                            ByVal BaseInstlFlg As String) As Integer
    End Interface
End Namespace