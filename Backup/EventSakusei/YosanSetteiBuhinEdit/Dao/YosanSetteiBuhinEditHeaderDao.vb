Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanSetteiBuhinEdit.Dao
    Public Interface YosanSetteiBuhinEditHeaderDao
        'ヘッダー情報に必要なDao'

        ''' <summary>
        ''' リストコードを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>該当するリストコード情報</returns>
        ''' <remarks></remarks>
        Function FindByListCode(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As TYosanSetteiListcodeVo

        ''' <summary>
        ''' イベント情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Function FindByEvent(ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' 手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>該当する手配基本情報</returns>
        ''' <remarks></remarks>
        Function FindByTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TYosanSetteiBuhinVo)


        ''' <summary>
        ''' 集計コード一覧を返す
        '''         ''' </summary>
        ''' <returns>該当する一覧</returns>
        ''' <remarks></remarks>
        Function FindByShukeiCodeInfo() As List(Of TSyukeiCodeVo)

#Region "追加項目"

        ''' <summary>
        ''' 現調部品情報を取得する
        ''' </summary>
        ''' <param name="genchoEventCode">イベントコード</param>
        ''' <param name="phaseNo">フェーズ№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByTFuncBuhinShisaku(ByVal genchoEventCode As String, ByVal phaseNo As String) As List(Of TFuncBuhinShisakuVo)


        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByGenchoEventCode() As List(Of TFuncEventPhaseVo)


        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="genchoEventCode">現調イベントコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPhase(ByVal genchoEventCode As String) As List(Of TFuncEventPhaseVo)



        ''' <summary>
        ''' パーツプライスを取得(AS400)
        ''' </summary>
        ''' <param name="buhinNoList">部品番号リスト</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByPartsPrice(ByVal buhinNoList As List(Of String)) As List(Of ASPartsPriceListVo)

        ''' <summary>
        ''' パーツプライスを取得(RHAC2110)
        ''' </summary>
        ''' <param name="buhinNoList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindLatestLowPartsPriceBy(ByVal buhinNoList As List(Of String)) As List(Of Rhac2110Vo)

        ''' <summary>
        ''' 最新値を返す(RHAC0580)
        ''' </summary>
        ''' <param name="buhinNoList">部品番号</param>
        ''' <returns>取得結果[]</returns>
        ''' <remarks></remarks>
        Function FindLatestCostItBy(ByVal buhinNoList As List(Of String)) As List(Of Rhac0580Vo)

        ''' <summary>
        ''' 最新値を返す(T_VALUE_DEV)
        ''' </summary>
        ''' <param name="buhinNoList">部品番号</param>
        ''' <returns>取得結果[]</returns>
        ''' <remarks></remarks>
        Function FindLatestSekkeichiBy(ByVal buhinNoList As List(Of String)) As List(Of Rhac0580Vo)


        ''' <summary>
        ''' 予算設定部品情報を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="yosanListCode">試作リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByTYosanSetteiBuhin(ByVal shisakuEventCode As String, ByVal yosanListCode As String) As List(Of TYosanSetteiBuhinVo)


        ''' <summary>
        ''' 最新の部品編集情報を取得する（課長承認なし）
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="yosanListCode">予算リストコード</param>
        ''' <returns>最新の部品編集情報</returns>
        ''' <remarks></remarks>
        Function FindByNewBuhinEditListSaishin(ByVal eventcode As String, ByVal yosanListCode As String) As System.Collections.Generic.List(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper)

        ''' <summary>
        ''' 予算設定部品履歴情報を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="yosanListCode">予算リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByTYosanSetteiBuhinRireki(ByVal shisakuEventCode As String, ByVal yosanListCode As String) As List(Of TYosanSetteiBuhinRirekiVo)

        ''' <summary>
        ''' 予算設定部品情報を削除
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="yosanListCode">予算リストコード</param>
        ''' <param name="yosanBlockNo">予算ブロック№</param>
        ''' <remarks></remarks>
        Sub DeleteByTYosanSetteiBuhin(ByVal shisakuEventCode As String, ByVal yosanListCode As String, ByVal yosanBlockNo As String)


        ''' <summary>
        ''' 現調部品情報を取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByTYosanSetteiBuhinFunction() As List(Of TYosanSetteiBuhinFunctionVo)


#Region "過去購入部品用"

        ''' <summary>
        ''' 過去購入部品情報を取得する
        ''' </summary>
        ''' <param name="buhinNoList">部品番号のリスト</param>
        ''' <param name="flag">0:発注最新順（発注日降順）1:検収最新順（検収日降順）2:コスト低順（検収金額昇順）</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByPastPurchase(ByVal buhinNoList As List(Of String), ByVal flag As String) As List(Of TYosanSetteiBuhinVo)

#End Region



#End Region

    End Interface
End Namespace