Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Dao

    Public Interface EventEditCompleteCarRirekiDao : Inherits DaoEachFeature

        ''' <summary>
        ''' 試作イベント完成車情報の一覧を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作イベント完成車情報の一覧</returns>
        ''' <remarks></remarks>
        Function GetShisakuEventCompleteCarList(ByVal shisakuEventCode As String) As List(Of TShisakuEventKanseiVo)

        ''' <summary>
        ''' 試作イベント完成車情報を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <returns>試作イベント完成車情報</returns>
        ''' <remarks></remarks>
        Function FindShisakuEventCompleteCar(ByVal shisakuEventCode As String, _
                                     ByVal hyojiJunNo As Integer) As TShisakuEventKanseiVo

        ''' <summary>
        ''' 試作イベント完成車情報を返す
        ''' 登録時のＤＢより
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <returns>試作イベント完成車情報</returns>
        ''' <remarks></remarks>
        Function FindShisakuEventCompleteCarKaitei(ByVal shisakuEventCode As String, _
                                     ByVal hyojiJunNo As Integer) As TShisakuEventKanseiKaiteiVo

        ''' <summary>
        ''' 試作イベント完成車履歴情報を作成する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベント完成車</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <param name="columnId">列ID</param>
        ''' <param name="columnName">列名</param>
        ''' <param name="before">変更前</param>
        ''' <param name="after">変更後</param>
        ''' <remarks></remarks>
        Sub InsertShisakuEventCompleteCar(ByVal shisakuEventCode As String, _
                                          ByVal hyojiJunNo As Integer, _
                                          ByVal ColumnId As String, _
                                          ByVal ColumnName As String, _
                                          ByVal before As String, _
                                          ByVal after As String)

        ''' <summary>
        ''' 試作イベント完成車履歴情報の一覧を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <param name="columnId">列ID</param>
        ''' <returns>試作イベント完成車履歴情報の一覧</returns>
        ''' <remarks></remarks>
        Function GetShisakuEventCompleteCarRirekiList( _
                                             ByVal shisakuEventCode As String, _
                                             ByVal hyojiJunNo As Integer, _
                                             ByVal columnId As String) As List(Of TShisakuEventKanseiRirekiVo)

        ''' <summary>
        ''' 試作イベントベース車情報を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <returns>試作イベントベース車情報</returns>
        ''' <remarks></remarks>
        Function FindShisakuEventBaseSeisakuIchiranCar(ByVal shisakuEventCode As String, _
                                     ByVal hyojiJunNo As Integer) As TShisakuEventBaseSeisakuIchiranVo

        ''' <summary>
        ''' 試作イベントベース車情報（改訂）を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <returns>試作イベントベース車情報</returns>
        ''' <remarks></remarks>
        Function FindShisakuEventBaseKaiteiCar(ByVal shisakuEventCode As String, _
                                     ByVal hyojiJunNo As Integer) As TShisakuEventBaseKaiteiVo

    End Interface

End Namespace