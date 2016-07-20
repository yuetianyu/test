Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Interface IShisakuEventDao : Inherits DaoEachFeature

        ''' <summary>
        ''' ExcelのHeaderのDB抽出部分
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <returns>ExcelのHeader資料</returns>
        ''' <remarks></remarks>
        Function GetShisakuBuhinMenuHead(ByVal eventCode As String) As ShisakuSekkeiBlockHeadVo

        ''' <summary>
        ''' 試作イベント情報よりアラート情報を取得する。
        ''' </summary>
        ''' <param name="eventCode">試作イベントコード</param>
        ''' <returns>試作イベント情報</returns>
        ''' <remarks></remarks>
        Function GetShisakuEvent(ByVal eventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' アラート情報の更新
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="blockAlertFlg">ブロックアラートフラグ</param>
        ''' <param name="blockAlertKind">ブロックアラート種類</param>
        ''' <remarks></remarks>
        Sub UpdAlertInfo(ByVal eventCode As String, ByVal blockAlertFlg As String, ByVal blockAlertKind As String)

        ''' <summary>
        ''' ブロックチェックアラー情報よりアラート情報を取得する。
        ''' </summary>
        ''' <param name="blockNo">ブロック№</param>
        ''' <param name="blockAlertFlg">ブロックアラート種類</param>
        ''' <returns>試作イベント情報</returns>
        ''' <remarks></remarks>
        Function GetBlockCheckAlertInfo(ByVal blockNo As String, _
                                        ByVal blockAlertFlg As String) As MBlockCheckInformationVo

        ''' <summary>
        ''' お知らせ通知情報の更新
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="infoMailFlg">お知らせ通知フラグ</param>
        ''' <remarks></remarks>
        Sub UpdInfoMail(ByVal eventCode As String, ByVal infoMailFlg As String)

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_e) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 作り方情報の更新
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="TsukurikataFlg">作り方フラグ</param>
        ''' <remarks></remarks>
        Sub UpdTsukurikataFlg(ByVal eventCode As String, ByVal TsukurikataFlg As String)
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_e) (TES)張 ADD END

        ''↓↓2014/07/24 Ⅰ.2.管理項目追加_bi) (TES)張 ADD BEGIN
        Sub UpdTsukurikataTenkaiFlg(ByVal eventCode As String, ByVal TsukurikataTenkaiFlg As String)
        ''↑↑2014/07/24 Ⅰ.2.管理項目追加_bi) (TES)張 ADD END
        ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 au) (TES)施 ADD BEGIN
        Sub UpdEbomKanshi(ByVal eventCode As String, ByVal EbomKanshiFlg As String)
        ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 au) (TES)施 ADDEND	

        '/*** 20140911 CHANGE START ***/
        ''' <summary>
        ''' 試作イベント情報のステータスを更新する。
        ''' </summary>
        ''' <param name="param">試作イベント情報</param>
        ''' <remarks></remarks>
        Sub UpdStatus(ByVal param As TShisakuEventVo)
        '/*** 20140911 CHANGE END ***/
    End Interface
End Namespace

