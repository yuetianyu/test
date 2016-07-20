Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.YosanSetteiBuhinSakusei.Dao

Namespace YosanSetteiBuhinSakusei.Dao
    Public Interface YosanSetteiBuhinHikakuDao
        '比較織込み用のインターフェース'

#Region "取得する処理(Find)"

        ''' <summary>
        ''' ベース側の部品番号の存在チェック
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="Level">レベル</param>
        ''' <param name="BuhinNo">部品No</param>
        ''' <param name="BuhinNoKbn">部品番号試作区分</param>
        ''' <returns>試作部品表編集ベース情報</returns>
        ''' <remarks></remarks>
        Function FindByTsuikaHinban(ByVal shisakuEventCode As String, _
                                    ByVal shisakuBukaCode As String, _
                                    ByVal shisakuBlockNo As String, _
                                    ByVal Level As String, _
                                    ByVal BuhinNo As String, _
                                    ByVal BuhinNoKbn As String, _
                                    Optional ByVal kyoukyuSection As String = Nothing, _
                                    Optional ByVal BaseBuhinFlag As String = Nothing) As List(Of TShisakuBuhinEditBaseVo)

        Function FindByTsuikaHinban(ByVal param As TShisakuBuhinEditBaseVo) As List(Of TShisakuBuhinEditBaseVo)

        ''' <summary>
        ''' 部品編集号車情報TMP用の情報を取得する
        ''' </summary>
        ''' <param name="BuhinEditVo">試作編集情報</param>
        ''' <returns>部品編集号車情報TMP情報用の情報</returns>
        ''' <remarks></remarks>
        Function FindByGousyaTmp(ByVal BuhinEditVo As TShisakuBuhinEditVo) As List(Of TYosanSetteiGousyaTmpVo)

        ''' <summary>
        ''' 号車表示順の最後の番号を取得する
        ''' </summary>
        ''' <param name="GousyaVo">部品編集号車情報</param>
        ''' <returns>部品編集号車情報TMP情報</returns>
        ''' <remarks></remarks>
        Function FindByGousyaHyoujiJun(ByVal GousyaVo As TYosanSetteiGousyaTmpVo) As Integer

        ''' <summary>
        ''' 追加品番の存在チェック(TMP)
        ''' </summary>
        ''' <param name="BaseVo">部品編集ベース情報</param>
        ''' <returns>追加品番があればTrue</returns>
        ''' <remarks></remarks>
        Function FindByTsuikaHinbanTmp(ByVal BaseVo As TShisakuBuhinEditVo) As Boolean

        ''' <summary>
        ''' 部品表編集ベース情報の員数取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する員数のリスト</returns>
        ''' <remarks></remarks>
        Function FindByBaseInsu(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal buhinNoHyoujiJun As Integer) As List(Of TYosanSetteiGousyaTmpVo)

        ''' <summary>
        ''' 部品表編集情報の員数取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する員数のリスト</returns>
        ''' <remarks></remarks>
        Function FindByInsuSuryo(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal buhinNoHyoujiJun As Integer) As List(Of TYosanSetteiGousyaTmpVo)

        ''' <summary>
        ''' 員数の増減チェック
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="instlHinbanHyoujiJun">INSTL品番表示順</param>
        ''' <param name="Baseflg">検索先がベース情報ならTrue</param>
        ''' <returns>増えていればU、減っていればD、同じならE</returns>
        ''' <remarks></remarks>
        Function CheckInsuUpDown(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal buhinNoHyoujiJun As Integer, _
                                ByVal instlHinbanHyoujiJun As Integer, _
                                ByVal insuSuryo As Integer, _
                                ByVal baseflg As Boolean) As String

        ''' <summary>
        ''' 最後の部品番号表示順+1を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns>最後の部品番号表示順+1</returns>
        ''' <remarks></remarks>
        Function FindByNewBuhinNoHyoujiJun(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String) As Integer

        ''' <summary>
        ''' 最後の部品番号表示順+1を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns>最後の部品番号表示順+1</returns>
        ''' <remarks></remarks>
        Function FindByNewGousyaBuhinNoHyoujiJun(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String) As Integer



        ''' <summary>
        ''' 員数の合計(INSTL)
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>員数</returns>
        ''' <remarks></remarks>
        Function FindBySumInsu(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal buhinNoHyoujiJun As Integer) As Integer

        ''' <summary>
        ''' 員数の合計(INSTLBase)
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>員数</returns>
        ''' <remarks></remarks>
        Function FindBySumInsuTmp(ByVal shisakuEventCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal buhinNoHyoujiJun As Integer) As Integer

        ''' <summary>
        ''' 開発符号を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>開発符号</returns>
        ''' <remarks></remarks>
        Function FindByKaihatsuFugo(ByVal shisakuEventCode As String) As String

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する部品編集INSTL情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditGousya(ByVal shisakuEventCode As String, _
                                       ByVal shisakuBukaCode As String, _
                                       ByVal shisakuBlockNo As String, _
                                       ByVal shisakuBlockNoKaiteiNo As String, _
                                       ByVal BuhinNoHyoujiJun As Integer) As List(Of TYosanSetteiGousyaTmpVo)

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当するベースとなる部品編集INSTL情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditGousyaBase(ByVal shisakuEventCode As String, _
                                          ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, _
                                          ByVal BuhinNoHyoujiJun As Integer) As List(Of TYosanSetteiGousyaTmpVo)

        ''' <summary>
        ''' 号車のリストを取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuGroup">グループNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByGousyaList(ByVal shisakuEventCode As String, ByVal shisakuGroup As String)

        ''' <summary>
        ''' ベースの情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuGroup">グループNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBaseTmpVoList(ByVal shisakuEventCode As String, ByVal shisakuGroup As String, ByVal JikyuFlg As Boolean) As List(Of YosanSetteiBuhinEditTmpVo)

#End Region

#Region "更新する処理(Update)"

        ''' <summary>
        ''' 試作部品表編集情報(TMP)を更新する(比較結果->変更無し)
        ''' </summary>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <remarks></remarks>
        Sub UpdateByBuhinTmpNotChange(ByVal BuhinEditVo As TShisakuBuhinEditVo)

        ''' <summary>
        ''' 試作部品表号車編集情報(TMP)を更新する(比較結果->変更無し)
        ''' </summary>
        ''' <param name="GousyaTmpVo">部品編集号車情報</param>
        ''' <param name="gousyaNo">号車No</param>
        ''' <remarks></remarks>
        Sub UpdateByGousyaTmpNotChange(ByVal GousyaTmpVo As TYosanSetteiGousyaTmpVo, ByVal gousyaNo As Integer)

        ''' <summary>
        ''' 試作部品表編集情報(TMP)を更新する(集計コード展開)
        ''' </summary>
        ''' <param name="shisakuBuhinTmp">試作部品表編集情報(TMP)リスト</param>
        ''' <param name="SeihinKbn">製品区分</param>
        ''' <remarks></remarks>
        Sub UpdateByBuhinTmpSyukeiTenkai(ByVal shisakuBuhinTmp As List(Of TYosanSetteiBuhinTmpVo), ByVal SeihinKbn As String)

        ''' <summary>
        ''' 試作部品表編集情報(TMP)を更新する(比較結果->追加品番)
        ''' </summary>
        ''' <param name="shisakuBuhinTmp">試作部品表編集情報(TMP)</param>
        ''' <param name="SeihinKbn">製品区分</param>
        ''' <remarks></remarks>
        Sub UpdateByBuhinTmpTsuikaHinban(ByVal shisakuBuhinTmp As TShisakuBuhinEditVo, ByVal SeihinKbn As String)

        ''' <summary>
        ''' 試作部品表編集情報(TMP)を更新する(比較結果->員数増)
        ''' </summary>
        ''' <param name="BuhinTmp">試作部品表編集情報(TMP)</param>
        ''' <param name="SeihinKbn">製品区分</param>
        ''' <remarks></remarks>
        Sub UpdateByBuhinTmpInsuUp(ByVal BuhinTmp As TShisakuBuhinEditVo, ByVal SeihinKbn As String)

        ''' <summary>
        ''' 試作部品表号車編集情報(TMP)を更新する(比較結果->変更無し)
        ''' </summary>
        ''' <param name="GousyaTmpVo">部品編集号車情報</param>
        ''' <param name="gousyaNo">号車No</param>
        ''' <remarks></remarks>
        Sub UpdateByGousyaTmpInsuUp(ByVal GousyaTmpVo As TYosanSetteiGousyaTmpVo, ByVal gousyaNo As Integer, ByVal insu As Integer)

#End Region

#Region "追加する処理(Insert)"

        ''' <summary>
        ''' 試作部品表編集情報(TMP)を追加する(比較結果->変更無し)
        ''' </summary>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <remarks></remarks>
        Sub InsertByBuhinTmpNotChange(ByVal BuhinEditvo As TShisakuBuhinEditVo, ByVal seihinKbn As String, _
                                 ByVal shisakuListCode As String)

        ''' <summary>
        ''' 部品表編集号車情報テンポラリ情報を追加する(比較結果織込み用)
        ''' </summary>
        ''' <param name="gousyaTmpvo">部品表編集号車情報VO</param>
        ''' <remarks></remarks>
        Sub InsertByGousyaTMPHikakuKekka(ByVal gousyaTmpvo As TYosanSetteiGousyaTmpVo, ByVal gousyaHyoujijun As Integer)

        ''' <summary>
        ''' 試作部品表編集情報(TMP)を追加する(比較結果->追加品番)
        ''' </summary>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <remarks></remarks>
        Sub InsertByBuhinTmpTsuikaHinban(ByVal BuhinEditvo As TShisakuBuhinEditVo, ByVal seihinKbn As String, _
                                 ByVal shisakuListCode As String)

        ''' <summary>
        ''' 試作部品表編集情報(TMP)を追加する(比較結果->員数増)
        ''' </summary>
        ''' <param name="BuhinEditVo">部品編集情報</param>
        ''' <param name="seihinKbn">製品区分</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <remarks></remarks>
        Sub InsertByBuhinTmpInsuUp(ByVal BuhinEditvo As TShisakuBuhinEditVo, ByVal seihinKbn As String, _
                                 ByVal shisakuListCode As String)

        ''' <summary>
        ''' 部品表編集号車情報テンポラリ情報に追加する(員数増)
        ''' </summary>
        ''' <param name="gousyaTmpvo">部品表編集号車情報VO</param>
        ''' <remarks></remarks>
        Sub InsertByGousyaTMPInsuUp(ByVal gousyaTmpvo As TYosanSetteiGousyaTmpVo, ByVal gousyaHyoujijun As Integer, ByVal insu As Integer)

        ''' <summary>
        ''' 部品表編集情報テンポラリ情報を追加する
        ''' </summary>
        ''' <param name="shisakuBuhinEditvo">部品表編集VO</param>
        ''' <remarks></remarks>
        Sub InsertByBuhinEditTMPBase(ByVal shisakuBuhinEditvo As List(Of YosanSetteiBuhinEditTmpVo), ByVal seihinKbn As String)

        ''' <summary>
        ''' 部品表編集号車情報テンポラリ情報を追加する
        ''' </summary>
        ''' <param name="gousyaTmpvo">部品表編集号車情報VO</param>
        ''' <remarks></remarks>
        Sub InsertByGousyaTMPBase(ByVal gousyaTmpvo As List(Of YosanSetteiBuhinEditTmpVo))

        WriteOnly Property EventVo() As TShisakuEventVo
        Sub UpdateByBuhinTmpIkanshaKaishu(ByVal eventCode As String, ByVal listCode As String)

        ''' <summary>
        ''' ブロック一覧の取得
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function getBlockList(ByVal eventCode As String) As List(Of TShisakuSekkeiBlockVo)

        ''' <summary>
        ''' 部品表の取得
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindTShisakuBuhinEdit(ByVal eventCode As String, ByVal blockNo As String, ByVal kaiteiNo As String) As List(Of TShisakuBuhinEditVo)
#End Region

    End Interface
End Namespace