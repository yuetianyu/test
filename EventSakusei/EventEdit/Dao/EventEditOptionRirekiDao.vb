Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Dao

    Public Interface EventEditOptionRirekiDao : Inherits DaoEachFeature

        ''' <summary>
        ''' 試作イベント装備仕様情報の一覧を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作イベント装備仕様情報の一覧</returns>
        ''' <remarks></remarks>
        Function GetShisakuEventOptionList(ByVal shisakuEventCode As String) As List(Of TShisakuEventSoubiVo)

        ''' <summary>
        ''' 試作イベント装備仕様情報を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <param name="shisakuSoubiHyoujiNo">試作装備表示順</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <returns>試作イベント完成車情報</returns>
        ''' <remarks></remarks>
        Function FindShisakuEventOption(ByVal shisakuEventCode As String, _
                                        ByVal hyojiJunNo As Integer, _
                                        ByVal shisakuSoubiHyoujiNo As Integer, _
                                        ByVal shisakuSoubiKbn As String) As TShisakuEventSoubiVo

        ''' <summary>
        ''' 試作イベント装備仕様情報を返す
        ''' 登録時ＤＢより
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <param name="shisakuSoubiHyoujiNo">試作装備表示順</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <returns>試作イベント完成車情報</returns>
        ''' <remarks></remarks>
        Function FindShisakuEventOptionKaitei(ByVal shisakuEventCode As String, _
                                        ByVal hyojiJunNo As Integer, _
                                        ByVal shisakuSoubiHyoujiNo As Integer, _
                                        ByVal shisakuSoubiKbn As String) As TShisakuEventSoubiKaiteiVo

        ''' <summary>
        ''' 試作イベント装備仕様履歴情報を作成する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <param name="shisakuSoubiHyoujiNo">試作装備表示順</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <param name="shisakuRetuKoumokuCodeBefore">試作列項目コード変更前</param>
        ''' <param name="shisakuRetuKoumokuCodeAfter">試作列項目コード変更後</param>
        ''' <param name="shisakuTekiyouBefore">試作適用変更前</param>
        ''' <param name="shisakuTekiyouAfter">試作適用変更後</param>
        ''' <remarks></remarks>
        Sub InsertShisakuEventOption(ByVal shisakuEventCode As String, _
                                             ByVal hyojiJunNo As Integer, _
                                             ByVal shisakuSoubiHyoujiNo As Integer, _
                                             ByVal shisakuSoubiKbn As String, _
                                             ByVal shisakuRetuKoumokuCodeBefore As String, _
                                             ByVal shisakuRetuKoumokuCodeAfter As String, _
                                             ByVal shisakuTekiyouBefore As String, _
                                             ByVal shisakuTekiyouAfter As String)

        ''' <summary>
        ''' 試作イベント装備仕様履歴情報の一覧を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojiJunNo">表示順№</param>
        ''' <param name="shisakuSoubiHyoujiNo">試作装備表示順</param>
        ''' <param name="shisakusoubikbn">試作装備区分</param>
        ''' <returns>試作イベント装備仕様履歴情報の一覧</returns>
        ''' <remarks></remarks>
        Function GetShisakuEventOptionRirekiList( _
                                             ByVal shisakuEventCode As String, _
                                             ByVal hyojiJunNo As Integer, _
                                             ByVal shisakuSoubiHyoujiNo As Integer, _
                                             ByVal shisakuSoubiKbn As String) As List(Of TShisakuEventSoubiRirekiVo)

    End Interface

End Namespace