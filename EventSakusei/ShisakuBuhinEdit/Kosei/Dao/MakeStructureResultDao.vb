Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Vo

Namespace ShisakuBuhinEdit.Kosei.Dao
    Public Interface MakeStructureResultDao
        ''' <summary>
        ''' 部品番号（親）を基点に構成情報を取得して返す(自給品有り)
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号</param>
        ''' <returns>一連の構成情報</returns>
        ''' <remarks></remarks>
        Function FindStructure0552ByBuhinNoOya(ByVal buhinNoOya As String) As List(Of Rhac0552Vo)

        ''' <summary>
        ''' 部品番号（親）を基点に構成を作り、子の部品情報を返す
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号</param>
        ''' <returns>一連の構成情報に紐付く部品情報</returns>
        ''' <remarks></remarks>
        Function FindStructure0552ByBuhinNoOyaAnd0532ForKo(ByVal buhinNoOya As String) As List(Of Rhac0532Vo)

        ''' <summary>
        ''' 最終改訂の部品情報を返す
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>最終改訂の部品情報</returns>
        ''' <remarks></remarks>
        Function FindLastestRhac0532MakerNameByBuhinNo(ByVal BuhinNo As String) As Rhac0532BuhinNoteVo

        ''' <summary>
        ''' 最終改訂の部品構成情報を返す
        ''' </summary>
        ''' <param name="BuhinNoKo">部品番号（子）</param>
        ''' <returns>最終改訂の部品構成情報</returns>
        ''' <remarks></remarks>
        Function FindLastestRhac0552ByBuhinNoKo(ByVal BuhinNoKo As String) As Rhac0552Vo

        ''' <summary>
        ''' 部品番号（親）を基点に構成を作り、子の部品情報の取引先情報を返す
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号（親）</param>
        ''' <returns>一連の構成情報に紐付く子の取引先情報</returns>
        ''' <remarks></remarks>
        Function FindStructure0552ByBuhinNoOyaAnd0610ForKo(ByVal BuhinNoOya As String) As List(Of Rhac0610Vo)

        ''' <summary>
        ''' 部品情報を取得する(RHAC0552)
        ''' </summary>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinRhac0552(ByVal BFBuhinNo As String) As Rhac0552Vo

        ''' <summary>
        ''' 部品情報をリストで取得する(RHAC0552)
        ''' 構成の１レベル目が取得できない場合、親情報のみ取得したいので
        ''' </summary>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinRhac0552LIST(ByVal BFBuhinNo As String) As List(Of Rhac0552Vo)


        'ここから追加 樺澤'

        ''' <summary>
        ''' 親の部品情報を返す
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>親の部品情報</returns>
        ''' <remarks></remarks>
        Function FindLastestRhac0532ByBuhinNoOya(ByVal BuhinNo As String) As Rhac0532Vo

        ''' <summary>
        ''' 部品の色を取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinColor0532(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal instlHinban As String, _
                                      ByVal bfBuhinNo As String, _
                                      ByVal buhinNo As String, Optional ByVal yakanFlg As Boolean = False) As String
        '↑↑2014/09/25 酒井 パラメタ追加
        ''' <summary>
        ''' 部品の色を取得する(0553)
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="instlHinban"></param>
        ''' <param name="bfBuhinNo"></param>
        ''' <param name="buhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinColor0533(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal instlHinban As String, _
                                       ByVal bfBuhinNo As String, _
                                       ByVal kaihatsuFugo As String, _
                                      ByVal buhinNo As String, Optional ByVal yakanFlg As Boolean = False) As String
        '↑↑2014/09/25 酒井 パラメタ追加
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="instlHinban"></param>
        ''' <param name="bfBuhinNo"></param>
        ''' <param name="buhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinColor0530(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal instlHinban As String, _
                                      ByVal bfBuhinNo As String, _
                                      ByVal buhinNo As String, Optional ByVal yakanFlg As Boolean = False) As String
        '↑↑2014/09/25 酒井 パラメタ追加

        ''' <summary>
        ''' 部品の色を取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinColor0532Kosei(ByVal shisakuEventCode As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String, _
                                           ByVal instlHinban As String, _
                                           ByVal bfBuhinNo As String, _
                                           ByVal buhinNo As String, Optional ByVal yakanFlg As Boolean = False) As String
        '↑↑2014/09/25 酒井 パラメタ追加

        ''' <summary>
        ''' 部品の色を取得する(0553)
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="instlHinban"></param>
        ''' <param name="bfBuhinNo"></param>
        ''' <param name="buhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinColor0533Kosei(ByVal shisakuEventCode As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String, _
                                           ByVal instlHinban As String, _
                                           ByVal bfBuhinNo As String, _
                                           ByVal kaihatsuFugo As String, _
                                           ByVal buhinNo As String, Optional ByVal yakanFlg As Boolean = False) As String
        '↑↑2014/09/25 酒井 パラメタ追加

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="instlHinban"></param>
        ''' <param name="bfBuhinNo"></param>
        ''' <param name="buhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinColor0530Kosei(ByVal shisakuEventCode As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String, _
                                           ByVal instlHinban As String, _
                                           ByVal bfBuhinNo As String, _
                                           ByVal buhinNo As String, Optional ByVal yakanFlg As Boolean = False) As String
        '↑↑2014/09/25 酒井 パラメタ追加

        ''' <summary>
        ''' 部品の色を取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinColor2220(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal instlHinban As String, _
                                      ByVal bfBuhinNo As String, _
                                      ByVal buhinNo As String, Optional ByVal yakanFlg As Boolean = False) As String
        '↑↑2014/09/25 酒井 パラメタ追加

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="instlHinban"></param>
        ''' <param name="instlHinbanKbn"></param>
        ''' <param name="kaiteiNo">改定No　  2014/08/04 Ⅰ.11.改訂戻し機能q) (TES)施 追加 </param> 
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindbyEditInstl(ByVal shisakuEventCode As String, _
         ByVal instlHinban As String, _
         ByVal instlHinbanKbn As String, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "", Optional ByVal eventCopyFlg As Boolean = False, Optional ByVal yakanFlg As Boolean = False) As List(Of TShisakuBuhinEditInstlVo)
        '↓↓2014/09/25 酒井 ADD BEGIN
        'Function FindbyEditInstl(ByVal shisakuEventCode As String, _
        'ByVal instlHinban As String, _
        'ByVal instlHinbanKbn As String, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "") As List(Of TShisakuBuhinEditInstlVo)
        Function FindbyEditInstlEbomKanshi(ByVal shisakuEventCode As String, _
 ByVal instlHinban As String, _
 ByVal instlHinbanKbn As String, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "", Optional ByVal eventCopyFlg As Boolean = False) As List(Of TShisakuBuhinEditInstlEbomKanshiVo)
        '↑↑2014/09/25 酒井 ADD END
        ''↓↓2014/09/23 酒井 ADD BEGIN`
        'Function FindbyEditInstl(ByVal shisakuEventCode As String, _
        'ByVal instlHinban As String, _
        'ByVal instlHinbanKbn As String, Optional ByVal KaiteiNo As String = "") As List(Of TShisakuBuhinEditInstlVo)
        ''↑↑2014/09/23 酒井 ADD END

        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="instlHinban"></param>
        ''' <param name="instlHinbanKbn"></param>
        ''' <param name="kaiteiNo">改定No　  2014/08/04 Ⅰ.11.改訂戻し機能 ｔ) (TES)施 追加 </param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindbyEdit(ByVal shisakuEventCode As String, _
                      ByVal instlHinban As String, _
                      ByVal instlHinbanKbn As String, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "", Optional ByVal eventCopyFlg As Boolean = False, Optional ByVal yakanFlg As Boolean = False) As List(Of TShisakuBuhinEditVo)
        '↓↓2014/09/25 酒井 ADD BEGIN
        'Function FindbyEdit(ByVal shisakuEventCode As String, _
        '     ByVal instlHinban As String, _
        '     ByVal instlHinbanKbn As String, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "") As List(Of TShisakuBuhinEditVo)
        Function FindbyEditEbomKanshi(ByVal shisakuEventCode As String, _
              ByVal instlHinban As String, _
              ByVal instlHinbanKbn As String, Optional ByVal KaiteiNo As String = "", Optional ByVal aInstlDataKbn As String = "", Optional ByVal aBaseInstlFlg As String = "", Optional ByVal eventCopyFlg As Boolean = False) As List(Of TShisakuBuhinEditEbomKanshiVo)
        '↑↑2014/09/25 酒井 ADD END
        ''↓↓2014/09/23 酒井 ADD BEGIN`
        'Function FindbyEdit(ByVal shisakuEventCode As String, _
        'ByVal instlHinban As String, _
        'ByVal instlHinbanKbn As String, Optional ByVal KaiteiNo As String = "") As List(Of TShisakuBuhinEditVo)
        ''↑↑2014/09/23 酒井 ADD END

        ''' <summary>
        ''' イベントコード以外の設計ブロックINSTLを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="instlHinban"></param>
        ''' <param name="instlHinbanKbn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindbySekkeiBlockInstlStructure(ByVal shisakuEventCode As String, _
                                                 ByVal instlHinban As String, _
                                                 ByVal instlHinbanKbn As String) As List(Of TShisakuSekkeiBlockInstlVo)

        ''' <summary>
        ''' イベントコードの設計ブロックINSTLを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="instlHinban"></param>
        ''' <param name="instlHinbanKbn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindbySekkeiBlockInstlStructureEventCopy(ByVal shisakuEventCode As String, _
                                                          ByVal instlHinban As String, _
                                                          ByVal instlHinbanKbn As String) As List(Of TShisakuSekkeiBlockInstlVo)




#Region "部品構成を取得する(FM5以降)"

        ''' <summary>
        ''' ベース車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Function FindByBase(ByVal shisakuEventCode As String, ByVal Gousya As String) As TShisakuEventBaseVo

        ''' <summary>
        ''' イベント情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Function FindByEvent(ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' 開発符号を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Function FindByKaihatsuFugo(ByVal shisakuEventCode As String) As TShisakuEventVo


        ''' <summary>
        ''' ブロックINSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Function FindByInstlBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal BFBuhinNo As String) As TShisakuEventBaseVo
        '↓↓2014/09/25 酒井 ADD BEGIN
        Function FindByInstlEbomKanshi(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal BFBuhinNo As String) As TShisakuEventEbomKanshiVo
        '↑↑2014/09/25 酒井 ADD END
        ''' <summary>
        ''' 部品情報を取得する(RHAC0553)
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinRhac0553(ByVal kaihatsuFugo As String, ByVal BFBuhinNo As String) As Rhac0553Vo

        ''' <summary>
        ''' 部品情報を取得する(RHAC0553)(A/L用)
        ''' </summary>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinRhac0553AL(ByVal BFBuhinNo As String) As Rhac0553Vo

        ''' <summary>
        ''' 部品番号（親）を基点に構成情報を取得して返す
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号</param>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>一連の構成情報</returns>
        ''' <remarks></remarks>
        Function FindStructure0553ByBuhinNoOya(ByVal buhinNoOya As String, ByVal kaihatsuFugo As String) As List(Of Rhac0553Vo)

        ''' <summary>
        ''' 部品番号（親）を基点に構成を作り、子の部品情報を返す
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号</param>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>一連の構成情報に紐付く部品情報</returns>
        ''' <remarks></remarks>
        Function FindStructure0553ByBuhinNoOyaAnd0533ForKo(ByVal buhinNoOya As String, ByVal kaihatsuFugo As String) As List(Of Rhac0533Vo)

        ''' <summary>
        ''' 最終改訂の部品情報を返す
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>最終改訂の部品情報</returns>
        ''' <remarks></remarks>
        Function FindLastestRhac0533MakerNameByBuhinNo(ByVal BuhinNo As String) As Rhac0533BuhinNoteVo

        ''' <summary>
        ''' 最終改訂の部品構成情報を返す
        ''' </summary>
        ''' <param name="BuhinNoKo">部品番号（子）</param>
        ''' <returns>最終改訂の部品構成情報</returns>
        ''' <remarks></remarks>
        Function FindLastestRhac0553ByBuhinNoKo(ByVal BuhinNoKo As String) As Rhac0553Vo

        ''' <summary>
        ''' 部品番号（親）を基点に構成を作り、子の部品情報の取引先情報を返す
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号（親）</param>
        ''' <returns>一連の構成情報に紐付く子の取引先情報</returns>
        ''' <remarks></remarks>
        Function FindStructure0553ByBuhinNoOyaAnd0610ForKo(ByVal BuhinNoOya As String) As List(Of Rhac0610Vo)

        ''' <summary>
        ''' 部品情報をリストで取得する(RHAC0553)
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinRhac0553LIST(ByVal kaihatsuFugo As String, ByVal BFBuhinNo As String) As List(Of Rhac0553Vo)

#End Region

#Region "部品構成を取得する(パンダ前)"

        ''' <summary>
        ''' 部品情報を取得する(RHAC0551)
        ''' </summary>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinRhac0551(ByVal BFBuhinNo As String) As Rhac0551Vo


        ''' <summary>
        ''' 部品番号（親）を基点に構成情報を取得して返す
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号</param>
        ''' <returns>一連の構成情報</returns>
        ''' <remarks></remarks>
        Function FindStructure0551ByBuhinNoOya(ByVal buhinNoOya As String) As List(Of Rhac0551Vo)


        ''' <summary>
        ''' 部品番号（親）を基点に構成を作り、子の部品情報を返す
        ''' </summary>
        ''' <param name="buhinNoOya">部品番号</param>
        ''' <returns>一連の構成情報に紐付く部品情報</returns>
        ''' <remarks></remarks>
        Function FindStructure0551ByBuhinNoOyaAnd0530ForKo(ByVal buhinNoOya As String) As List(Of Rhac0530Vo)


        ''' <summary>
        ''' 最終改訂の部品情報を返す
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>最終改訂の部品情報</returns>
        ''' <remarks></remarks>
        Function FindLastestRhac0530MakerNameByBuhinNo(ByVal BuhinNo As String) As Rhac0530MakerNameVo



        ''' <summary>
        ''' 最終改訂の部品構成情報を返す
        ''' </summary>
        ''' <param name="BuhinNoKo">部品番号（子）</param>
        ''' <returns>最終改訂の部品構成情報</returns>
        ''' <remarks></remarks>
        Function FindLastestRhac0551ByBuhinNoKo(ByVal BuhinNoKo As String) As Rhac0551Vo



        ''' <summary>
        ''' 部品番号（親）を基点に構成を作り、子の部品情報の取引先情報を返す
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号（親）</param>
        ''' <returns>一連の構成情報に紐付く子の取引先情報</returns>
        ''' <remarks></remarks>
        Function FindStructure0551ByBuhinNoOyaAnd0610ForKo(ByVal BuhinNoOya As String) As List(Of Rhac0610Vo)

        ''' <summary>
        ''' 親の部品情報を返す
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>親の部品情報</returns>
        ''' <remarks></remarks>
        Function FindLastestRhac0530ByBuhinNoOya(ByVal BuhinNo As String) As Rhac0530Vo



        ''' <summary>
        ''' 部品情報をリストで取得する(RHAC0551)
        ''' </summary>
        ''' <param name="BFBuhinNo">BF部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinRhac0551LIST(ByVal BFBuhinNo As String) As List(Of Rhac0551Vo)



#End Region


        ''' <summary>
        ''' パラ部品番号で、「試作部品ブロックINSTL」から「INSTL品番」、「INSTL品番試作区分」を返す
        ''' </summary>
        ''' <param name="bfBuhinNo">基本Ｆ品番</param>
        ''' <returns>該当した「INSTL品番」、「INSTL品番試作区分」</returns>
        ''' <remarks></remarks>
        Function FindShisakuBlockInstlbfBuhinNo(ByVal BFBuhinNo As String) As TShisakuSekkeiBlockInstlVo

        ''' <summary>
        ''' 列に該当する部品の構成を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="InstlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <returns>該当する部品編集INSTL情報リスト</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditInstlKosei(ByVal shisakuEventCode As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String, _
                                           ByVal InstlHinban As String, _
                                           ByVal instlHinbanKbn As String) As List(Of BuhinEditInstlKoseiVo)

        ''' <summary>
        ''' 部品番号に該当する部品名称と取引先コードを取得する
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinMaker(ByVal BuhinNo As String) As TShisakuBuhinEditVo

        ''' <summary>
        ''' 親の構成情報を取得する
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByRhac0532OyaKousei(ByVal BuhinNo As String) As Rhac0532Vo


    End Interface
End Namespace