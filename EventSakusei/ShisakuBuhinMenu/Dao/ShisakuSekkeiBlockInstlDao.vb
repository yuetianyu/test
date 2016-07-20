Imports ShisakuCommon.Db.EBom.Vo
Namespace ShisakuBuhinMenu.Dao
    Public Interface ShisakuSekkeiBlockInstlDao

        ''' <summary>
        ''' 試作設計ブロックINSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番試作区分</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindShisakuSekkeiBlockInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal instlHinban As String, ByVal instlHinbanKbn As String, ByVal shisakuGousya As String) As TShisakuSekkeiBlockInstlVo

        ''↓↓2014/08/21 1 ベース部品表作成表機能増強 ADD BEGIN
        ''' <summary>
        ''' 試作設計ブロックINSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByEventCodeAndGousya(ByVal shisakuEventCode As String, ByVal shisakuGousya As String) As List(Of TShisakuSekkeiBlockInstlVo)

        ''' <summary>
        ''' 試作設計ブロックINSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByEventCode(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockInstlVo)
        ''↑↑2014/08/21 1 ベース部品表作成表機能増強 ADD END

    End Interface
End Namespace