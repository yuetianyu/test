Imports ShisakuCommon.Db

Namespace Soubi

    ''' <summary>
    ''' イベント装備情報用のDao
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface EventSoubiDao : Inherits DaoEachFeature
        ''' <summary>
        ''' イベント装備情報の装備名タイトル部のみを返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <returns>装備名タイトル部のみのレコード</returns>
        ''' <remarks></remarks>
        Function FindWithTitleNameBy(ByVal shisakuEventCode As String, Optional ByVal shisakuSoubiKbn As String = Nothing) As List(Of TShisakuEventSoubiNameVo)
        ''' <summary>
        ''' イベント装備情報に装備名を付けて返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <returns>装備名付きのレコード</returns>
        ''' <remarks></remarks>
        Function FindWithNameBy(ByVal shisakuEventCode As String, Optional ByVal shisakuSoubiKbn As String = Nothing) As List(Of TShisakuEventSoubiNameVo)

        ''' <summary>
        ''' イベント装備情報の装備名タイトル部のみを返す
        '''　機能仕様表示選択画面用に使用
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <returns>装備名タイトル部のみのレコード</returns>
        ''' <remarks></remarks>
        Function FindWithTitleNameBySoubi(ByVal shisakuEventCode As String, Optional ByVal shisakuSoubiKbn As String = Nothing) As List(Of TShisakuEventSoubiNameVo)
        ''' <summary>
        ''' イベント装備情報に装備名を付けて返す
        '''　機能仕様表示選択画面用に使用
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <returns>装備名付きのレコード</returns>
        ''' <remarks></remarks>
        Function FindWithNameBySoubi(ByVal shisakuEventCode As String, Optional ByVal shisakuSoubiKbn As String = Nothing) As List(Of TShisakuEventSoubiNameVo)

    End Interface
End Namespace