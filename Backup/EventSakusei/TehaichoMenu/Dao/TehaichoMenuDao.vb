Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu.Vo

Namespace TehaichoMenu.Dao
    Public Interface TehaichoMenuDao

        ''' <summary>
        ''' 手配訂正通知情報の最終更新日を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="listcode">リストコード</param>
        ''' <param name="listcodeKaiteiNo">リストコード改訂№</param>
        ''' <returns>手配訂正通知の最終更新日</returns>
        ''' <remarks></remarks>
        Function FindByTehaiSaishin(ByVal eventcode As String, ByVal listcode As String, ByVal listcodeKaiteiNo As String) As TShisakuTehaiTeiseiKihonVo
        ''' <summary>
        ''' 手配基本情報の最終更新日を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="listcode">リストコード</param>
        ''' <param name="listcodeKaiteiNo">リストコード改訂№</param>
        ''' <returns>手配基本の最終更新日</returns>
        ''' <remarks></remarks>
        Function FindByKihonSaishin(ByVal eventcode As String, ByVal listcode As String, ByVal listcodeKaiteiNo As String) As TShisakuTehaiKihonVo


        ''' <summary>
        ''' リストコードを取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="listcode">リストコード</param>
        ''' <returns>リストコード</returns>
        ''' <remarks></remarks>
        Function FindByListCode(ByVal eventcode As String, ByVal listcode As String) As TShisakuListcodeVo

        ''' <summary>
        ''' 手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>手配基本情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByListKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVo)

        ''' <summary>
        ''' 訂正通知用手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>手配基本情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByListKihonTeisei(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVo)

        ''' <summary>
        ''' 手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <param name="unitKbn">ユニット区分</param>
        ''' <returns>手配基本情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByTehaiKihonKaiteiNo(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal kaiteiNo As String, ByVal unitKbn As String) As List(Of TShisakuTehaiKihonVoHelper)

        ''' <summary>
        ''' 手配基本情報を取得する（試作手配帳情報（号車グループ情報））
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <param name="unitKbn">ユニット区分</param>
        ''' <param name="shisakuGousyaGroup">グループ</param>
        ''' <returns>手配基本情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByTehaiKihonKaiteiNoToGroup(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal kaiteiNo As String, ByVal unitKbn As String, ByVal shisakuGousyaGroup As String) As List(Of TShisakuTehaiKihonVoHelper)

        ''' <summary>
        ''' 手配号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <returns>手配号車情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByTehaiGousyaKaiteiNo(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal kaiteiNo As String) As List(Of TShisakuTehaiGousyaVo)

        ''' <summary>
        ''' 手配号車情報を取得する（試作手配帳情報（号車グループ情報））
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <param name="ShisakuGousyaGroup">グループ</param>
        ''' <returns>手配号車情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByTehaiGousyaKaiteiNoToGroup(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal kaiteiNo As String, ByVal shisakuGousyaGroup As String) As List(Of TShisakuTehaiGousyaVo)

        ''' <summary>
        ''' ユニット区分を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>ユニット区分</returns>
        ''' <remarks></remarks>
        Function FindByUnitKbn(ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' 旧リストコードを更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="oldListCode">旧リストコード</param>
        ''' <param name="statusFlag">ステータス変更するか</param>
        ''' <remarks></remarks>
        Sub UpdateByOldListCode(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal oldListCode As String, ByVal statusFlag As Boolean)

        ''' <summary>
        ''' ステータスを更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub UpdateByStatus(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal Status As String)

        ''' <summary>
        ''' エラー区分を更新する
        ''' </summary>
        ''' <param name="errorVoList">エラーリスト</param>
        ''' <remarks></remarks>
        Sub UpdateByErrorKbn(ByVal errorVoList As List(Of TShisakuTehaiErrorVo))

        ''' <summary>
        ''' ベース車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Function FindByBase(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuEventBaseVo)

        ''' <summary>
        ''' ベース車情報を取得する（試作手配帳情報（号車グループ情報））
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuGousyaGroup">グループ</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Function FindByBaseToGroup(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuGousyaGroup As String) As List(Of TShisakuEventBaseVo)

        ''' <summary>
        ''' 全てのベース車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Function FindByBaseAll(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo)

        ''' <summary>
        ''' 社員名を返す
        ''' </summary>
        ''' <param name="shainId">社員ID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByShainName(ByVal shainId As String) As String


        ''' <summary>
        ''' 試作設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <returns>ユニット区分</returns>
        ''' <remarks></remarks>
        Function FindByShisakuBlockNo(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal shisakuBlockNoKaiteiNo As String) As String


#Region "発注用データを登録する"

        ''' <summary>
        ''' 発注用データ用手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Function FindByKetsugoNo(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVo)

        ''' <summary>
        ''' 発注用データ用結合Noを取得する
        ''' </summary>
        ''' <param name="ketugouHeader">結合Noの頭</param>
        ''' <remarks></remarks>
        Function FindByMaxKetsugoNo(ByVal ketugouHeader As String) As TShisakuTehaiKihonVo



        ''' <summary>
        ''' 発注用データを登録する
        ''' </summary>
        ''' <param name="tehaiKihonVo">手配基本情報</param>
        ''' <param name="ketugouNo">結合No</param>
        ''' <remarks></remarks>
        Sub UpdateKetsugoNo(ByVal tehaiKihonVo As TShisakuTehaiKihonVo, ByVal ketugouNo As String)
#End Region

#Region "エラーチェック"
        ''' <summary>
        ''' エラー情報を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByError(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

        ''' <summary>
        ''' エラー情報を追加する
        ''' </summary>
        ''' <param name="errorVoList">エラー情報のリスト</param>
        ''' <remarks></remarks>
        Sub InsertByError(ByVal errorVoList As List(Of TShisakuTehaiErrorVo))

        ''' <summary>
        ''' 新調達エラー情報のリストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>該当する新調達エラー情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByShinChoTatsuError(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TehaiMenuErrorExcelVo)

        ''' <summary>
        ''' 現調品エラー情報のリストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>該当する現調品エラー情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByGenchoError(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TehaiMenuErrorExcelVo)

        ''' <summary>
        ''' ３か月インフォメーションを取得する
        ''' </summary>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当する３ヶ月インフォメーション情報</returns>
        ''' <remarks></remarks>
        Function FindBy3Month(ByVal seihinKbn As String, ByVal buhinNo As String) As AsKPSM10PVo

        ''' <summary>
        ''' 海外生産情報を取得する
        ''' </summary>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当する海外生産情報</returns>
        ''' <remarks></remarks>
        Function FindByForign(ByVal seihinKbn As String, ByVal buhinNo As String) As AsGKPSM10PVo

        ''' <summary>
        ''' 新調達取引先情報の取得
        ''' </summary>
        ''' <param name="torihikisakiCode">取引先コード</param>
        ''' <returns>該当する新調達取引先情報</returns>
        ''' <remarks></remarks>
        Function FindByAsPAPF14(ByVal torihikisakiCode As String) As AsPAPF14Vo

        ''' <summary>
        ''' パーツプライリスト情報の取得
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>該当する新調達取引先情報</returns>
        ''' <remarks></remarks>
        Function FindByAsPARTSP(ByVal buhinNo As String) As AsPARTSPVo

        ''' <summary>
        ''' 最新のブロックNoのリストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByShisakuBlockList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuSekkeiBlockVo)

        ''' <summary>
        ''' 手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>手配基本情報のリスト</returns>
        ''' <remarks></remarks>
        Function FindByListKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuTehaiKihonVo)

#End Region

#Region "新調達への転送"

        ''' <summary>
        ''' 履歴の更新
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub UpdateByRireki(ByVal shisakuEventCode As String, _
                               ByVal shisakuListCode As String)

        ''' <summary>
        ''' 指定の改訂Noのリストコードを取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="listcode">リストコード</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <returns>リストコード</returns>
        ''' <remarks></remarks>
        Function FindByListCodeKaiteiNo(ByVal eventcode As String, ByVal listcode As String, ByVal kaiteiNo As String) As TShisakuListcodeVo

#End Region

#Region "再編集"

        ''' <summary>
        ''' 手配基本情報を編集前に戻す
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub RisetByTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

#End Region

#Region "改訂抽出"

        ''' <summary>
        ''' 試作部品編集情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>試作部品編集情報リスト</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEdit(ByVal shisakuEventCode As String) As List(Of TShisakuBuhinEditVo)

        ''' <summary>
        ''' 試作部品編集情報(ベース)を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">ブロックNo表示順</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>試作部品編集情報(ベース)リスト</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditBase(ByVal shisakuEventCode As String, _
                                     ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal shisakuBlockNoKaiteiNo As String, _
                                      ByVal buhinNoHyoujiJun As String) As List(Of TShisakuBuhinEditBaseVo)

        ''' <summary>
        ''' 試作部品編集・INSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">ブロックNo表示順</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>試作部品編集・INSTL情報リスト</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditInstl(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal shisakuBlockNoKaiteiNo As String, _
                                      ByVal buhinNoHyoujiJun As String) As List(Of TShisakuBuhinEditInstlVo)

        ''' <summary>
        ''' 試作部品編集・INSTL情報(ベース)を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="instlHinbanHyoujiJun">INSTL品番表示順</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>試作部品編集・INSTL情報(ベース)リスト</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditInstlBase(ByVal shisakuEventCode As String, _
                                          ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, _
                                          ByVal shisakuBlockNoKaiteiNo As String, _
                                          ByVal instlHinbanHyoujiJun As String) As List(Of TShisakuBuhinEditInstlBaseVo)


        Function FindByBuhinEditIkanshaKaishu(ByVal shisakuEventCode As String, ByVal blockNo As String, ByVal listCode As String) As List(Of TShisakuBuhinEditVoHelperExcel)

        ''' <summary>
        ''' 試作設計ブロック・INSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>試作設計ブロック・INSTL情報(ベース)リスト</returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlockInstl(ByVal shisakuEventCode As String, _
                                          ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, _
                                          ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuSekkeiBlockInstlVo)

        ''' <summary>
        ''' 部品編集改訂情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>部品編集改訂情報</returns>
        ''' <remarks></remarks>
        Function FindByKaiteiBuhinEdit(ByVal shisakuEventCode As String, _
                                          ByVal shisakuListCode As String, _
                                          ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuBuhinEditKaiteiVoHelper)
        'Function FindByKaiteiBuhinEdit(ByVal shisakuEventCode As String, _
        '                          ByVal shisakuListCode As String) As List(Of TShisakuBuhinEditKaiteiVo)

        ''' <summary>
        ''' 部品編集号車改訂情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>部品編集号車改訂情報</returns>
        ''' <remarks></remarks>
        Function FindByKaiteiBuhinEditGousya(ByVal shisakuEventCode As String, _
                                          ByVal shisakuListCode As String) As List(Of TShisakuBuhinEditGousyaKaiteiVo)

#End Region

    End Interface
End Namespace