Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作部品編集・INSTL情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuBuhinEditInstlDao : Inherits DaoEachTable(Of TShisakuBuhinEditInstlVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="instlHinbanHyoujiJun">INSTL品番表示順</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, _
                          ByVal shisakuBlockNoKaiteiNo As String, _
                          ByVal buhinNoHyoujiJun As Nullable(Of Int32), _
                          ByVal instlHinbanHyoujiJun As Nullable(Of Int32)) As TShisakuBuhinEditInstlVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
          ''' <param name="instlHinbanHyoujiJun">INSTL品番表示順</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, _
                            ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal buhinNoHyoujiJun As Nullable(Of Int32), _
                            ByVal instlHinbanHyoujiJun As Nullable(Of Int32)) As Integer

    End Interface
End Namespace