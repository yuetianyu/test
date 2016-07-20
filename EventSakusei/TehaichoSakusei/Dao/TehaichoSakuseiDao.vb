Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoSakusei.Dao
Imports EventSakusei.TehaichoSakusei.Vo

Namespace TehaichoSakusei.Dao
    Public Interface TehaichoSakuseiDao
        '2012/02/22 UpdateByShisakuEvent追加

        '手配帳作成画面用インターフェース'
#Region "手配帳初期表示用"
        ''' <summary>
        ''' グループNoのリストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード </param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByGroupNoList(ByVal shisakuEventCode As String) As List(Of TShisakuEventKanseiVo)

        ''' <summary>
        ''' グループNoを取得する(単体用)
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード </param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByGroupNo(ByVal shisakuEventCode As String) As TShisakuEventKanseiVo

        ''' <summary>
        ''' イベント名称と開発符号を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード </param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByEventName(ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' 製品区分のリストを取得する
        ''' </summary>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByAsSNKM() As List(Of AsSKMSVo)

        ''' <summary>
        ''' 製品区分を取得する
        ''' </summary>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByAsSNKMOne() As AsSKMSVo
#End Region




#Region "取得する処理(Find)"

        ''' <summary>
        ''' 最新の表示順Noを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード </param>
        ''' <returns>最新の表示順No</returns>
        ''' <remarks></remarks>
        Function FindByHyoujijunNo(ByVal shisakuEventCode As String) As TShisakuListcodeVo

        ''' <summary>
        ''' 最新のリストコードを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード </param>
        ''' <param name="shisakuKoujiShireiNo">工事指令No</param>
        ''' <returns>最新のリストコード</returns>
        ''' <remarks></remarks>
        Function FindByListCode(ByVal shisakuEventCode As String, ByVal shisakuKoujiShireiNo As String) As TShisakuListcodeVo

        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakEventCode">試作イベントコード</param>
        ''' <param name="shisakuGroup">試作グループNo</param>
        ''' <param name="JikyuFlg">自給品の消しこみフラグ</param>
        ''' <returns>該当するBlockINSTL情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByBuhin(ByVal shisakEventCode As String, _
                             ByVal shisakuGroup As String, _
                             ByVal JikyuFlg As Boolean) As List(Of TehaichoBuhinEditTmpVo)

        ''' <summary>
        ''' ベース車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGroup">試作グループNo</param>
        ''' <returns>該当する情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByBaseList(ByVal shisakuEventCode As String, _
                                ByVal shisakuGroup As String) As List(Of TShisakuEventBaseVo)

        ''' <summary>
        ''' 設計ブロックINSTL+課長情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <returns>該当する情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindBySekkeiList(ByVal shisakuEventCode As String, _
                                ByVal shisakuGousya As String) As List(Of SekkeiBlockInstlVoHelper)

        ''' <summary>
        ''' 設計ブロックINSTL+課長情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>該当する情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindBySekkeiList(ByVal shisakuEventCode As String, _
                                  ByVal shisakuGousya As String, _
                                  ByVal shisakuBukaCode As String, _
                                  ByVal shisakuBlockNo As String, _
                                  ByVal shisakuBlockNoKaiteiNo As String) As List(Of SekkeiBlockInstlVoHelper)

        ''' <summary>
        ''' 設計ブロック情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>該当する情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlockList(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockVo)

        ''' <summary>
        ''' 部品編集+員数を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="instlHinbanHyoujiJun">INSTL品番表示順</param>
        ''' <returns>該当する情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditInsu(ByVal shisakuEventCode As String, _
                                     ByVal shisakuGousya As String, _
                                     ByVal shisakuBukaCode As String, _
                                     ByVal shisakuBlockNo As String, _
                                     ByVal shisakuBlockNoKaiteiNo As String, _
                                     ByVal instlHinbanHyoujiJun As Integer, _
                                     ByVal JikyuFlg As Boolean) As List(Of TehaichoBuhinEditTmpVo)

        ''' <summary>
        ''' 部品編集+員数を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="instlHinbanHyoujiJun">INSTL品番表示順</param>
        ''' <returns>該当する情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditInsu(ByVal shisakuEventCode As String, _
                                     ByVal shisakuBukaCode As String, _
                                     ByVal shisakuBlockNo As String, _
                                     ByVal shisakuBlockNoKaiteiNo As String, _
                                     ByVal instlHinbanHyoujiJun As Integer, _
                                     ByVal JikyuFlg As Boolean) As List(Of TehaichoBuhinEditTmpVo)





        ''' <summary>
        ''' 部品号車情報を取得する
        ''' </summary>
        ''' <param name="shisakEventCode">試作イベントコード</param>
        ''' <param name="shisakuGroup">試作グループNo</param>
        ''' <param name="JikyuFlg">自給品の消しこみフラグ</param>
        ''' <returns>該当するBlockINSTL情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByBuhinTmpGosya(ByVal shisakEventCode As String, ByVal shisakuGroup As String, ByVal JikyuFlg As Boolean) As List(Of TShisakuBuhinEditGousyaTmpVo)

        ''' <summary>
        ''' 種別を取得する
        ''' </summary>
        ''' <returns>該当する種別のリスト</returns>
        ''' <remarks></remarks>
        Function FindBySyubetu(ByVal shisakEventCode As String, ByVal shisakuGroup As String) As List(Of TShisakuEventBaseVo)

        ''' <summary>
        ''' 親品番を取得する
        ''' </summary>
        ''' <param name="BuhinNo">部品番号(子)</param>
        ''' <returns>該当す親品番ト</returns>
        ''' <remarks></remarks>
        Function FindByBuhinNoOya(ByVal kaihatsuFugo As String, ByVal BuhinNo As String) As String

        ''' <summary>
        ''' メーカーコードを取得する
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>メーカーコード</returns>
        ''' <remarks></remarks>
        Function FindByMakerCode(ByVal kaihatsuFugo As String, ByVal BuhinNo As String) As String

        ''' <summary>
        ''' 専用品かどうか存在チェックする（手配帳作成用）
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <returns>あればTrue</returns>
        ''' <remarks></remarks>
        Function FindBySenyouCheckSakusei(ByVal BuhinNo As String, ByVal seihinKbn As String, _
                                   ByVal aKPSM As List(Of AsKPSM10PVo), _
                                   ByVal aPARTS As List(Of AsPARTSPVo), _
                                   ByVal aGKPSM As List(Of AsGKPSM10PVo)) As Boolean

        ''' <summary>
        ''' 専用品かどうか存在チェックする
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <returns>あればTrue</returns>
        ''' <remarks></remarks>
        Function FindBySenyouCheck(ByVal BuhinNo As String, ByVal seihinKbn As String) As Boolean

        ''' <summary>
        ''' 部品編集情報TMP用情報を取得する
        ''' </summary>
        ''' <param name="shiskuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>部品編集情報TMPのリスト</returns>
        ''' <remarks></remarks>
        Function FindByBuhinTmp(ByVal shiskuEventCode As String, _
                                ByVal shisakuListCode As String) As List(Of TShisakuBuhinEditTmpVo)


        ''' <summary>
        ''' 部品編集情報TMP用情報を取得する
        ''' </summary>
        ''' <param name="shiskuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>部品編集情報TMPのリスト</returns>
        ''' <remarks></remarks>
        Function FindByBuhinHikakuTmp(ByVal shiskuEventCode As String, _
                                      ByVal shisakuListCode As String) As List(Of TShisakuBuhinEditTmpVo)


        ''' <summary>
        ''' ユニット区分を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>ユニット区分</returns>
        ''' <remarks></remarks>
        Function FindByUnitKbn(ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' 部品編集情報TMP情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns>部品編集情報TMP情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinTmpList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditTmpVo)

        ''' <summary>
        ''' 部品編集号車情報TMP情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>部品編集号車情報TMP情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinGousyaTmpList(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditGousyaTmpVo)

        ''' <summary>
        ''' 購担/取引先を取得する（手配帳作成用）
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Function FindByKoutanTorihikisakiSakusei(ByVal buhinNo As String, _
                                                 ByVal aAsKpsm10p As List(Of AsKPSM10PVo), _
                                                 ByVal aAsPartsp As List(Of AsPARTSPVo), _
                                                 ByVal aAsGkpsm10p As List(Of AsGKPSM10PVo), _
                                                 ByVal aAsBuhin01 As List(Of AsBUHIN01Vo), _
                                                 ByVal aTValueDev As List(Of TValueDevVo), _
                                                 ByVal aRhac0610 As List(Of Rhac0610Vo)) As TShisakuBuhinEditTmpVo
        ''' <summary>
        ''' 購担/取引先を取得する
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Function FindByKoutanTorihikisaki(ByVal buhinNo As String) As TShisakuBuhinEditTmpVo

        ''' <summary>
        ''' 親の部品番号候補を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="level">レベル</param>
        ''' <returns>親の部品番号候補</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditBuhinNoOya(ByVal shisakuEventCode As String, _
                                          ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, _
                                          ByVal shisakuBlockNoKaiteiNo As String, _
                                          ByVal level As Integer, _
                                          ByVal buhinNoKo As String, _
                                          ByVal kaihatsuFugo As String) As TShisakuBuhinEditVo


        ''' <summary>
        ''' 図面Noを取得する（手配帳作成用）
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Function FindByZumenNoSakusei(ByVal buhinNo As String, _
                                      ByVal aAsBuchin01 As List(Of AsBUHIN01Vo)) As String

        ''' <summary>
        ''' 図面Noを取得する
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Function FindByZumenNo(ByVal buhinNo As String) As String

        ''' <summary>
        ''' 合計員数数量を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>合計員数数量</returns>
        ''' <remarks></remarks>
        Function FindByTotalInsuSuryo(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Integer) As Integer

        ''' <summary>
        ''' 親の部品番号と試作区分と取引先コードを取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="Level">レベル</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当する親品番と試作区分と取引先コード</returns>
        ''' <remarks></remarks>
        Function FindByBuhinNoOyaKbn(ByVal kaihatsuFugo As String, _
                                     ByVal BuhinNo As String, _
                                     ByVal shisakuEventCode As String, _
                                     ByVal shisakuBukaCode As String, _
                                     ByVal shisakuBlockNo As String, _
                                     ByVal shisakuBlockNoKaiteiNo As String, _
                                     ByVal Level As Integer, _
                                     ByVal shukeiCode As String, _
                                     ByVal siaShukeiCode As String) As TShisakuBuhinEditTmpVo


        '''' <summary>
        '''' マージ可能な手配基本情報を取得する
        '''' </summary>
        '''' <param name="shisakuEventCode">イベントコード</param>
        '''' <param name="shisakuListCode">リストコード</param>
        '''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        '''' <param name="shisakuBukaCode">部課コード</param>
        '''' <param name="shisakuBlockNo">ブロックNo</param>
        '''' <param name="Level">レベル</param>
        '''' <param name="BuhinNo">部品番号</param>
        '''' <param name="shukeiCode">集計コード</param>
        '''' <param name="tehaiKigou">手配記号</param>
        '''' <param name="kyoukuSection">供給セクション</param>
        '''' <returns>該当する手配基本情報</returns>
        '''' <remarks></remarks>
        'Function FindByMergeTehaiKihon(ByVal shisakuEventCode As String, _
        '                               ByVal shisakuListCode As String, _
        '                               ByVal shisakuListCodeKaiteiNo As String, _
        '                               ByVal shisakuBukaCode As String, _
        '                               ByVal shisakuBlockNo As String, _
        '                               ByVal level As Integer, _
        '                               ByVal buhinNo As String, _
        '                               ByVal shukeiCode As String, _
        '                               ByVal tehaiKigou As String, _
        '                               ByVal kyoukuSection As String) As TShisakuTehaiKihonVo


        ''' <summary>
        ''' ダミー列用に配号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns>該当する手配号車情報</returns>
        ''' <remarks></remarks>
        Function FindByDummyTehaiGousya(ByVal shisakuEventCode As String, _
                                         ByVal shisakuListCode As String, _
                                         ByVal shisakuListCodeKaiteiNo As String, _
                                         ByVal shisakuBukaCode As String, _
                                         ByVal shisakuBlockNo As String) As TShisakuTehaiGousyaVo


        ''' <summary>
        ''' ブロックNoのリストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード </param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByBlockNoList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuSekkeiBlockVo)

        ''' <summary>
        ''' ブロックNo毎に部品編集TMPと部品編集号車TMPの合体リストを取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード </param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByTehaiMergeList(ByVal shisakuEventCode As String, _
                                      ByVal shisakuListCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal shisakuBlockNoKaiteiNo As String) As List(Of TehaichoBuhinEditTmpVo)

        ''' <summary>
        ''' 号車毎の開発符号を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuGousyaHyoujiJun">号車表示順</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Function FindByGousyaKaihatsuFugo(ByVal shisakuEventCode As String, ByVal shisakuGousyaHyoujiJun As Integer) As TShisakuEventBaseVo

        ''' <summary>
        ''' 手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByTehaiKihonList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVo)

        ''' <summary>
        ''' 部品編集TMP情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditTmp(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuBuhinEditTmpVo)

        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakEventCode">試作イベントコード</param>
        ''' <param name="shisakuGroup">試作グループNo</param>
        ''' <param name="shisakuGousya">号車</param>
        ''' <returns>該当するBlockINSTL情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByBuhin3D(ByVal shisakEventCode As String, _
                               ByVal shisakuGroup As String, _
                               ByVal shisakuGousya As Dictionary(Of Integer, String)) As List(Of TehaichoBuhinEditTmpVo)

#End Region


#Region "更新する処理(Update)"

        ''' <summary>
        ''' 完成車情報の工指Noを更新する
        ''' </summary>
        ''' <remarks></remarks>
        Sub UpdateByKoushiNo(ByVal shisakuEventCode As String, ByVal shisakuGroup As String, ByVal shsiakuKoushiNo As String)

        ''' <summary>
        ''' 部品情報号車情報(TMP)を更新する
        ''' </summary>
        ''' <param name="gousyaTmpVo">号車TMP用情報</param>
        ''' <remarks></remarks>
        Sub UpdateByBuhinEditGousyaTmp(ByVal gousyaTmpVo As TShisakuBuhinEditGousyaTmpVo)


        ''' <summary>
        ''' 手配基本情報を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="totalInsuSuryo">合計員数数量</param>
        ''' <remarks></remarks>
        Sub UpdateByTehaiKihon(ByVal shisakuEventCode As String, _
                               ByVal shisakuListCode As String, _
                               ByVal shisakuListCodeKaiteNo As String, _
                               ByVal shisakuBukaCode As String, _
                               ByVal shisakuBlockNo As String, _
                               ByVal buhinNoHyoujiJun As String, _
                               ByVal totalInsuSuryo As Integer)

        ''' <summary>
        ''' 手配号車情報の部品番号表示順を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">検索用部品番号表示順</param>
        ''' <param name="mergebuhinNoHyoujiJun">更新後の部品番号表示順</param>
        ''' <remarks></remarks>
        Sub UpdateByTehaiGousyaBuhinNoHyoujiJun(ByVal shisakuEventCode As String, _
                                               ByVal shisakuListCode As String, _
                                               ByVal shisakuListCodeKaiteNo As String, _
                                               ByVal shisakuBukaCode As String, _
                                               ByVal shisakuBlockNo As String, _
                                               ByVal buhinNoHyoujiJun As String, _
                                               ByVal mergebuhinNoHyoujiJun As String)

        ''' <summary>
        ''' 手配号車情報の行IDを更新する
        ''' </summary>
        ''' <param name="tehaiGousyaVoList">手配号車情報リスト</param>
        ''' <remarks></remarks>
        Sub UpdateByTehaiGousyaGyouID(ByVal tehaiGousyaVoList As List(Of TShisakuTehaiGousyaVo))

        ''' <summary>
        ''' 手配号車情報の員数を合計する
        ''' </summary>
        ''' <param name="tehaiGousyaVo">手配号車情報リスト</param>
        ''' <remarks></remarks>
        Sub UpdateByTehaiGousyaInsuTotal(ByVal tehaiGousyaVo As TShisakuTehaiGousyaVo)

        ''' <summary>
        ''' 行IDを振る
        ''' </summary>
        ''' <param name="tehaiKihonVoList">手配基本情報リスト</param>
        ''' <remarks></remarks>
        Sub UpdateByGyouId(ByVal tehaiKihonVoList As List(Of TShisakuTehaiKihonVo))

        ''' <summary>
        ''' 専用マークを更新する
        ''' </summary>
        ''' <param name="buhinEditTmpVoList">部品編集TMP情報</param>
        ''' <remarks></remarks>
        Sub UpdateBySenyouMark(ByVal buhinEditTmpVoList As List(Of TShisakuBuhinEditTmpVo))

        ''' <summary>
        ''' TMPの行IDを振りなおす
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub UpdateByTmpGyouId(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

        ''' <summary>
        ''' 試作イベントの手配帳作成日を更新
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Sub UpdateByShisakuEvent(ByVal shisakuEventCode As String)

#End Region

#Region "挿入する処理(Insert)"

        ''' <summary>
        ''' 部品表編集情報テンポラリ情報を追加する
        ''' </summary>
        ''' <param name="shisakuBuhinEditvo">部品表編集VO</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="KaihatsuFugo">開発符号</param>
        ''' <param name="SyukeiSuru">集計コードからの展開をするフラグ</param>
        ''' <remarks></remarks>
        Sub InsertByBuhinEditTMP(ByVal shisakuBuhinEditvo As List(Of TehaichoBuhinEditTmpVo), _
                                 ByVal seihinKbn As String, _
                                 ByVal shisakuListCode As String, _
                                 ByVal KaihatsuFugo As String, _
                                 ByVal SyukeiSuru As Boolean, _
                                 ByVal aAsKpsm10p As List(Of AsKPSM10PVo), _
                                 ByVal aAsPartsp As List(Of AsPARTSPVo), _
                                 ByVal aAsGkpsm10p As List(Of AsGKPSM10PVo))

        ''' <summary>
        ''' 部品表編集号車情報テンポラリ情報を追加する
        ''' </summary>
        ''' <param name="gousyaTmpvo">部品表編集号車情報VO</param>
        ''' <remarks></remarks>
        Sub InsertByGousyaTMP(ByVal gousyaTmpvo As List(Of TehaichoBuhinEditTmpVo))

        ''' <summary>
        ''' 手配基本情報を追加する
        ''' </summary>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="MergeList">マージした情報</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="unitKbn">ユニット区分</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiKihon2(ByVal MergeList As List(Of TehaichoBuhinEditTmpVo), _
                               ByVal seihinKbn As String, _
                               ByVal shisakuListCode As String, _
                               ByVal unitKbn As String, _
                               ByVal aAsBuhin01 As List(Of AsBUHIN01Vo), _
                               ByVal aAsKpsm10p As List(Of AsKPSM10PVo), _
                               ByVal aAsPartsp As List(Of AsPARTSPVo), _
                               ByVal aAsGkpsm10p As List(Of AsGKPSM10PVo), _
                               ByVal aTValueDev As List(Of TValueDevVo), _
                               ByVal aRhac0610 As List(Of Rhac0610Vo))

        ''' <summary>
        ''' 手配号車情報を追加する
        ''' </summary>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="MergeList">マージした情報</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiGousya2(ByVal MergeList As List(Of TehaichoBuhinEditTmpVo), _
                               ByVal shisakuListCode As String)

        ''' <summary>
        ''' 手配号車情報を追加する
        ''' </summary>
        ''' <param name="gousyaVo">手配号車情報</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiGousya(ByVal gousyaVo As TShisakuTehaiGousyaVo)

        ''' <summary>
        ''' 手配帳改訂ブロック情報を追加する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiKaiteiBlock(ByVal shisakuEventCode As String, _
                                     ByVal shisakuListCode As String, _
                                     ByVal shisakuBukaCode As String, _
                                     ByVal shisakuBlockNo As String, _
                                     ByVal shisakuBlockNoKaiteiNo As String)

        ''' <summary>
        ''' ダミー列の追加
        ''' </summary>
        ''' <param name="DummyVo">手配号車情報</param>
        ''' <remarks></remarks>
        Sub InserByDummyTehaiGousya(ByVal DummyVo As TShisakuTehaiGousyaVo)



#End Region

#Region "削除する処理(Delete)"

        ''' <summary>
        ''' 部品表編集情報(TMP)を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Sub DeleteByBuhinTmp(ByVal shisakuEventCode As String)


        ''' <summary>
        ''' 部品表号車情報(TMP)を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Sub DeleteByGousyaTmp(ByVal shisakuEventCode As String)

        ''' <summary>
        ''' 部品表号車情報(TMP)を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Sub DeleteByKaiteiBlock(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)


#End Region



    End Interface
End Namespace