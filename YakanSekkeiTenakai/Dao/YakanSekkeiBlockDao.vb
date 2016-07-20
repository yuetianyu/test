Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports YakanSekkeiTenakai.Vo

Namespace Dao
    Public Interface YakanSekkeiBlockDao
        ''' <summary>
        ''' 試作イベントベース車情報を元に、「試作イベントベース車情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>該当した「試作イベントベース車情報」</returns>
        ''' <remarks></remarks>
        Function FindByShisakuEventBase(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo)

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「RHAC2210」を抽出して返す
        ''' </summary>
        ''' <param name="KaihatsuFugo">開発符号</param>
        ''' <param name="SobiKaiteiNo">装備改訂№</param>
        ''' <param name="KatashikiScd7">7桁型式識別コード</param>
        ''' <param name="ShimukechiCode">仕向地コード</param>
        ''' <param name="OpCode">OPコード</param>
        ''' <returns>該当した「RHAC2210」</returns>
        ''' <remarks></remarks>
        Function FindRHAC2210(ByVal KaihatsuFugo As String, _
                                  ByVal SobiKaiteiNo As String, _
                                  ByVal KatashikiScd7 As String, _
                                  ByVal ShimukechiCode As String, _
                                  ByVal OpCode As String) As List(Of Rhac2210Vo)

        ''↓↓2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        Function FindRHAC0532(ByVal BuhinNo As String) As Rhac0532Vo
        Function FindRHAC0533(ByVal BuhinNo As String) As Rhac0533Vo
        Function FindRHAC0530(ByVal BuhinNo As String) As Rhac0530Vo
        ''↑↑2014/08/27 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END

        '/*** 20140923 CHANGE START（高速化対応） ***/
        Function FindRHAC0532AndRHAC0700(ByVal BuhinNo As String) As YakanSekkeiGetForCreateTsuchishoAndZairyouVo
        Function FindRHAC0533AndRHAC0700(ByVal BuhinNo As String) As YakanSekkeiGetForCreateTsuchishoAndZairyouVo
        Function FindRHAC0530AndRHAC0700(ByVal BuhinNo As String) As YakanSekkeiGetForCreateTsuchishoAndZairyouVo
        '/*** 20140923 CHANGE END ***/

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="SobiKaiteiNo">装備改訂№</param>
        ''' <param name="KaihatsuFugo">開発符号</param>
        ''' <param name="KatashikiScd7">7桁型式識別コード</param>
        ''' <param name="ShimukechiCode">仕向地コード</param>
        ''' <param name="OpCode">OPコード</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindByShisakuEvent(ByVal shisakuEventCode As String, _
                                  ByVal SobiKaiteiNo As String, _
                                  ByVal KaihatsuFugo As String, _
                                  ByVal KatashikiScd7 As String, _
                                  ByVal ShimukechiCode As String, _
                                  ByVal OpCode As String) As List(Of SekkeiBlockAlResultVo)

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindAlByShisakuEventBase(ByVal shisakuEventCode As String, Optional ByVal HyojiJunNo As String = "") As List(Of SekkeiBlockAlResultVo)

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks>高速化対応のため部品は取得しない</remarks>
        Function FindAlByShisakuEventBaseNoBuhin(ByVal shisakuEventCode As String, Optional ByVal HyojiJunNo As String = "") As List(Of SekkeiBlockAlResultVo)

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="BlokNo">ブロックNo</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindAlByShisakuEventBaseByBlock(ByVal shisakuEventCode As String, ByVal BlokNo As String) As List(Of SekkeiBlockAlResultVo)

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindAlByShisakuEventBase2(ByVal shisakuEventCode As String, Optional ByVal HyojiJunNo As String = "") As List(Of SekkeiBlockAlResultVo)
        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindAlByShisakuEventBase2NoBuhin(ByVal shisakuEventCode As String, Optional ByVal HyojiJunNo As String = "") As List(Of SekkeiBlockAlResultVo)

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="BlokNo">ブロックNo</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindAlByShisakuEventBase2ByBlock(ByVal shisakuEventCode As String, ByVal BlokNo As String) As List(Of SekkeiBlockAlResultVo)
        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' 　　ベース車情報に指定した仕様情報№－１で抽出する。
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindAlByShisakuEventBaseOLD(ByVal shisakuEventCode As String, Optional ByVal HyojiJunNo As String = "") As List(Of SekkeiBlockAlResultVo)
        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' 　　ベース車情報に指定した仕様情報№－１で抽出する。
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindAlByShisakuEventBaseOLDNoBuhin(ByVal shisakuEventCode As String, Optional ByVal HyojiJunNo As String = "") As List(Of SekkeiBlockAlResultVo)

        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' 　　ベース車情報に指定した仕様情報№－１で抽出する。
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="BlokNo">ブロックNo</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindAlByShisakuEventBaseOLDByBlock(ByVal shisakuEventCode As String, ByVal BlokNo As String) As List(Of SekkeiBlockAlResultVo)
        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' 　　ベース車情報に指定した仕様情報№－１で抽出する。
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindAlByShisakuEventBaseOLD2(ByVal shisakuEventCode As String, Optional ByVal HyojiJunNo As String = "") As List(Of SekkeiBlockAlResultVo)
        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' 　　ベース車情報に指定した仕様情報№－１で抽出する。
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindAlByShisakuEventBaseOLD2NoBuhin(ByVal shisakuEventCode As String, Optional ByVal HyojiJunNo As String = "") As List(Of SekkeiBlockAlResultVo)


        ''' <summary>
        ''' 試作イベントベース車情報を元に、「A/Lの素情報」を抽出して返す
        ''' 　　ベース車情報に指定した仕様情報№－１で抽出する。
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="BlokNo">ブロックNo</param>
        ''' <returns>該当した「A/Lの素情報」</returns>
        ''' <remarks></remarks>
        Function FindAlByShisakuEventBaseOLD2ByBlock(ByVal shisakuEventCode As String, ByVal BlokNo As String) As List(Of SekkeiBlockAlResultVo)

        ''' <summary>
        ''' 試作イベントベース車情報を元に、試作の構成情報（試作部品編集INSTL情報）をもつ「試作設計ブロックINSTL情報」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="HyojiJunNo">表示順</param>
        ''' <returns>該当した「試作設計ブロックINSTL情報」</returns>
        ''' <remarks></remarks>
        Function FindShisakuBlockInstlByShisakuEventBase(ByVal shisakuEventCode As String, ByVal unitKbn As String, ByVal HyojiJunNo As String) As List(Of YakanSekkeiBlockInstlResultVo)

        ''' <summary>
        ''' 試作イベントベース車情報を元に、試作の構成情報（試作部品編集INSTL情報）をもつ「試作設計ブロックINSTL情報」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="unitKbn">ユニット区分</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <returns>該当した「試作設計ブロックINSTL情報」</returns>
        ''' <remarks></remarks>
        Function FindShisakuBlockInstlByShisakuEventBaseByBlock(ByVal shisakuEventCode As String, ByVal unitKbn As String, ByVal blockNo As String) As List(Of YakanSekkeiBlockInstlResultVo)

        ''' <summary>
        ''' ブロックを元に、「開発車機能ブロック」マスタから「ユニット区分」を返す
        ''' </summary>
        ''' <param name="BlockNo">ブロック</param>
        ''' <returns>該当した「ユニット区分」</returns>
        ''' <remarks></remarks>
        Function FindShisakuBlockUnit(ByVal BlockNo As String, ByVal KaihatsuFugo As String) As Rhac0080Vo

        ''' <summary>
        ''' ブロックを元に、試作設計ブロックテーブルから「ユニット区分」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="BlockNo">ブロック</param>
        ''' <returns>該当した「ユニット区分」</returns>
        ''' <remarks></remarks>
        Function FindShisakuBlockUnitFromSekkeiBlock(ByVal shisakuEventCode As String, ByVal BlockNo As String) As TShisakuSekkeiBlockEbomKanshiVo

        ''' <summary>
        ''' 「試作設計ブロックINSTL情報」から最大の「INSTL品番表示順」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <returns>該当した「Instl品番表示順」</returns>
        ''' <remarks></remarks>
        Function FindShisakuBlockInstlHyoujiJun(ByVal shisakuEventCode As String, _
                                             ByVal ShisakuBlockNo As String) As TShisakuSekkeiBlockInstlEbomKanshiVo

        ''' <summary>
        ''' 「試作設計ブロックINSTL情報」から最大の「INSTL品番表示順」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="InstlHinban">INSTL品番</param>
        ''' <returns>該当した「Instl品番表示順」</returns>
        ''' <remarks></remarks>
        Function FindShisakuBlockInstlHyoujiJun2(ByVal shisakuEventCode As String, _
                                             ByVal ShisakuBlockNo As String, _
                                             ByVal InstlHinban As String) As TShisakuSekkeiBlockInstlEbomKanshiVo

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindSekkeiBlock(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBlockNo As String, _
                                 ByVal shisakuBlockNoKaiteiNo As String) As TShisakuSekkeiBlockEbomKanshiVo

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="instlDataKbn">INSTLデータ区分</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindSekkeiBlockInstl(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal shisakuBlockNoKaiteiNo As String, _
                                      ByVal shisakuGousya As String, _
                                      ByVal instlDataKbn As String, _
                                      ByVal instlHinban As String, _
                                      ByVal instlHinbanKbn As String) As TShisakuSekkeiBlockInstlEbomKanshiVo



#Region "2012/01/23 親品番が自給品のデータを削除させる"

        ''' <summary>
        ''' 「試作イベント情報」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作イベント情報</returns>
        ''' <remarks></remarks>
        Function FindByShisakuEvent(ByVal shisakuEventCode As String) As TShisakuEventEbomKanshiVo

        ''' <summary>
        ''' 「試作設計ブロック情報」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindByShisakuBlockAll(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockEbomKanshiVo)

        ''' <summary>
        ''' 「試作設計ブロックINSTL情報」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns>設計ブロックINSTL情報</returns>
        ''' <remarks></remarks>
        Function FindByShisakuSekkeiBlockInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockInstlEbomKanshiVo)

        ''' <summary>
        ''' 「部品編集INSTL情報」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns>部品編集INSTL情報</returns>
        ''' <remarks></remarks>
        Function FindByShisakuBuhinEditInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal instlHinbanHyoujiJun As Integer) As List(Of TShisakuBuhinEditInstlEbomKanshiVo)

        ''' <summary>
        ''' 「試作設計ブロックINSTL情報と試作設計ブロック情報」を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <remarks></remarks>
        Sub DeleteByShisakuBuhinEditInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String)

        ''' <summary>
        ''' 「試作設計ブロック情報」を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindByShisakuBlockAllGroupByBlockNo(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockEbomKanshiVo)

#End Region

    End Interface
End Namespace