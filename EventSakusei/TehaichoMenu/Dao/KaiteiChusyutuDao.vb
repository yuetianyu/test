Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu.Vo
Imports EventSakusei.ShisakuBuhinKaiteiBlock.Dao

Namespace TehaichoMenu.Dao
    Public Interface KaiteiChusyutuDao

#Region "取得する処理"

        ''' <summary>
        ''' 最新の部品編集情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="isBase">ベースか？</param>
        ''' <returns>最新の部品編集情報</returns>
        ''' <remarks></remarks>
        Function FindByNewBuhinEditList(ByVal eventcode As String, ByVal shisakuListCode As String, _
                                        ByVal shouninNichiji As Long, Optional ByVal isBase As Boolean = False, _
                                              Optional ByVal isIkansha As Boolean = False) As List(Of TShisakuBuhinEditVoSekkeiHelper)

        ''' <summary>
        ''' 最新の部品編集情報を取得する(課長承認なし)
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="isBase">ベースか？</param>
        ''' <returns>最新の部品編集情報</returns>
        ''' <remarks></remarks>
        Function FindByNewBuhinEditListSaishin(ByVal eventcode As String, ByVal shisakuListCode As String, Optional ByVal isBase As Boolean = False) As List(Of TShisakuBuhinEditVoSekkeiHelper)

        ''' <summary>
        ''' 最新の部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="buhinNoHyoujiJun"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByNewBuhinEdit(ByVal shisakuEventCode As String, _
                                    ByVal shisakuBukaCode As String, _
                                    ByVal shisakuBlockNo As String, _
                                    ByVal buhinNoHyoujiJun As Integer) As TShisakuBuhinEditVo


        ''' <summary>
        ''' 手配改訂情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>該当する手配改訂情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditKaiteiNo(ByVal shisakuEventCode As String, _
                                         ByVal shisakuListCode As String, _
                                         Optional ByVal isIkansha As Boolean = False) As List(Of TShisakuTehaiKaiteiBlockVoHelper)
        'Function FindByBuhinEditKaiteiNo(ByVal shisakuEventCode As String, _
        '                         ByVal shisakuListCode As String, _
        '                         ByVal shouninNichiji As ShisakuDate) As List(Of TShisakuTehaiKaiteiBlockVoHelper)

        ''' <summary>
        ''' 前回の部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="level">レベル</param>
        ''' <param name="shukeiCode">集計コード</param>
        ''' <param name="siaShukeiCode">海外集計コード</param>
        ''' <param name="shisakuKbn">試作区分</param>
        ''' <param name="kyoukuSection">供給セクション</param>
        ''' <param name="saishiyoufuka">再使用不可</param>
        ''' <param name="KBFlag">今回前回フラグ(今回ならTrue)</param>
        ''' <returns>該当する部品編集情報</returns>
        ''' <remarks></remarks>
        Function FindByBeforeBuhinEdit(ByVal shisakuEventCode As String, _
                                       ByVal shisakuBukaCode As String, _
                                       ByVal shisakuBlockNo As String, _
                                       ByVal level As Integer, _
                                       ByVal shukeiCode As String, _
                                       ByVal siaShukeiCode As String, _
                                       ByVal buhinNo As String, _
                                       ByVal shisakuKbn As String, _
                                       ByVal kyoukuSection As String, _
                                       ByVal saishiyoufuka As String, _
                                       ByVal KBFlag As Boolean) As TShisakuBuhinEditVoSekkeiHelper

        ''' <summary>
        ''' 前回の部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="level">レベル</param>
        ''' <param name="shukeiCode">集計コード</param>
        ''' <param name="siaShukeiCode">海外集計コード</param>
        ''' <param name="shisakuKbn">試作区分</param>
        ''' <param name="KBFlag">今回前回フラグ(今回ならTrue)</param>
        ''' <returns>該当する部品編集情報</returns>
        ''' <remarks></remarks>
        Function FindByBeforeBuhinEditFuyou(ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String, _
                                            ByVal level As Integer, _
                                            ByVal shukeiCode As String, _
                                            ByVal siaShukeiCode As String, _
                                            ByVal buhinNo As String, _
                                            ByVal shisakuKbn As String, _
                                            ByVal KBFlag As Boolean) As TShisakuBuhinEditVoSekkeiHelper

        ''' <summary>
        ''' 前回のブロック情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="KBFlag">今回前回フラグ(今回ならTrue)</param>
        ''' <returns>該当するブロック情報</returns>
        ''' <remarks></remarks>
        Function FindByBeforeBlock(ByVal shisakuEventCode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal KBFlag As Boolean) As TShisakuBuhinEditVoSekkeiHelper

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakueventcode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="KBFlag">今回か前回か(今回ならTrue)</param>
        ''' <returns>部品編集INSTL情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditGousya(ByVal shisakueventcode As String, _
                                       ByVal shisakuBukaCode As String, _
                                       ByVal shisakuBlockNo As String, _
                                       ByVal shisakuBlockNoKaiteiNo As String, _
                                       ByVal buhinNoHyoujiJun As Integer, _
                                       ByVal KBFlag As Boolean) As List(Of TShisakuBuhinEditVoSekkeiHelper)


        ''' <summary>
        ''' 前回の改訂Noで該当する部品編集情報のリストを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="shisakuBlockNoKaiteiNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByZenkaiBuhinEditList(ByVal shisakuEventCode As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String, _
                                           ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVo)

        ''' <summary>
        ''' 号車のリストを取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByGousyaList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuEventBaseVo)



        ''' <summary>
        ''' 部品編集改訂号車ベース情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する部品編集号車ベース情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditGousyaBase(ByVal shisakuEventCode As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String, _
                                           ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuBuhinEditGousyaKaiteiVo)



        ''' <summary>
        ''' 手配帳改訂抽出ブロック情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>該当する手配帳改訂抽出ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindByTehaiKaiteiBlock(ByVal shisakuEventCode As String, _
                                        ByVal shisakuBukaCode As String, _
                                        ByVal shisakuBlockNo As String, _
                                        ByVal shisakuBlockNoKaiteiNo As String) As TShisakuTehaiKaiteiBlockVo

        ''' <summary>
        ''' 手配帳改訂抽出ブロック情報を取得する(前回)
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>該当する手配帳改訂抽出ブロック情報</returns>
        ''' <remarks></remarks>
        ''' 
        Function FindByTehaikaiteiBlockZenkai(ByVal shisakuEventCode As String) As List(Of TShisakuBuhinEditVo)

        ''' <summary>
        ''' 前回部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>部品編集INSTLベース情報</returns>
        ''' <remarks></remarks>
        Function FindByZenkaiBuhinEditInstl(ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String, _
                                            ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuBuhinEditInstlVo)

        ''' <summary>
        ''' 号車改訂情報の員数を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <returns>号車改訂情報の員数</returns>
        ''' <remarks></remarks>
        Function FindByGousyaKaiteiInsu(ByVal shisakuEventCode As String, _
                                        ByVal shisakuBukaCode As String, _
                                        ByVal shisakuBlockNo As String, _
                                        ByVal shisakuBlockNoKaiteiNo As String, _
                                        ByVal buhinNoHyoujiJun As Integer, _
                                        ByVal shisakuGousya As String) As Integer


        ''' <summary>
        ''' 手配改訂ブロック情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns>手配改訂ブロック情報</returns>
        ''' <remarks></remarks>
        Function TehaiKaiteiBlock(ByVal shisakuEventCode As String, _
                                  ByVal shisakuListCode As String, _
                                  ByVal shisakuBukaCode As String, _
                                  ByVal shisakuBlockNo As String) As TShisakuTehaiKaiteiBlockVo


        ''' <summary>
        ''' ブロックNoごとの部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="shisakuBlockNoKaiteiNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByTehaiKaiteiBuhinEditList(ByVal shisakuEventCode As String, _
                                                ByVal shisakuBukaCode As String, _
                                                ByVal shisakuBlockNo As String, _
                                                ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVo)

        ''' <summary>
        ''' 手配改訂ブロック情報をつかって部品編集情報を取得する
        ''' </summary>
        ''' <param name="kaiteiBlockList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByZenkaiTehaikaiteiBlock(ByVal kaiteiBlockList As TShisakuTehaiKaiteiBlockVoHelper, _
                                              ByVal goushaList As Hashtable, _
                                              Optional ByVal isIkansha As Boolean = False) As List(Of TShisakuBuhinEditVoSekkeiHelper)
        Function FindByTShisakuTehaiGousya(ByVal ShisakuEventCode As String, ByVal shisakuListCode As String) As Hashtable


        ''' <summary>
        ''' 部品編集改訂情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="KBFlag">今回か前回か(今回ならTrue)</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditVoSekkeiHelper(ByVal shisakuEventCode As String, ByVal KBFlag As String, Optional ByVal isIkansha As Boolean = False) As List(Of TShisakuBuhinEditVoSekkeiHelper)


        ''' <summary>
        ''' 部品編集改訂情報（BASE）を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditBase(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuBuhinEditVoSekkeiHelper)



#End Region

#Region "取得する処理(ベース)"

        ''' <summary>
        ''' 部品編集ベース情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <returns>部品編集ベース情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditBase(ByVal eventcode As String) As List(Of TShisakuBuhinEditBaseVo)

        ''' <summary>
        ''' 部品編集INSTLベース情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>部品編集INSTLベース情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditInstlBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuBuhinEditInstlBaseVo)

#End Region

#Region "チェックする処理"

        ''' <summary>
        ''' 部品編集ベース情報をチェック
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する部品編集ベース情報</returns>
        ''' <remarks></remarks>
        Function CheckByBuhinEditBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Integer) As TShisakuBuhinEditBaseVo

        ''' <summary>
        ''' 部品編集情報をチェック
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する部品編集情報</returns>
        ''' <remarks></remarks>
        Function CheckByBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Integer) As TShisakuBuhinEditVo

        ''' <summary>
        ''' 前回部品編集情報をチェック
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する部品編集ベース情報</returns>
        ''' <remarks></remarks>
        Function CheckByZeinkaiBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer) As TShisakuBuhinEditVo

#End Region

#Region "挿入する処理(Insert)"


        ''' <summary>
        ''' 部品編集改訂号車情報を追加する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂Ｎｏ</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">今回試作ブロック改訂No</param>
        ''' <param name="zenkaiShisakuBlockNoKaiteiNo">前回試作ブロック改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="shisakuGousyaHyoujiJun">試作号車表示順</param>
        ''' <param name="shisakuGousya">試作号車表示順</param>
        ''' <param name="insuSuryo">員数数量</param>
        ''' <param name="Flag">フラグ</param>
        ''' <remarks></remarks>
        Sub InsertByBuhinEditGousya(ByVal shisakuEventCode As String, _
                                    ByVal shisakuListCode As String, _
                                    ByVal shisakuListCodeKaiteiNo As String, _
                                    ByVal shisakuBukaCode As String, _
                                    ByVal shisakuBlockNo As String, _
                                    ByVal ShisakuBlockNoKaiteiNo As String, _
                                    ByVal zenkaiShisakuBlockNoKaiteiNo As String, _
                                    ByVal buhinNoHyoujiJun As Integer, _
                                    ByVal shisakuGousyaHyoujiJun As Integer, _
                                    ByVal shisakuGousya As String, _
                                    ByVal insuSuryo As Integer, _
                                    ByVal Flag As String)


        ''' <summary>
        ''' 部品編集改訂情報を追加する
        ''' </summary>
        ''' <param name="buhinEditVo">部品編集情報</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂No</param>
        ''' <param name="zenkaiBlockKaiteiNo">前回ブロックNo改訂No</param>
        ''' <param name="Flag">フラグ</param>
        ''' <remarks></remarks>
        Sub InsertByBuhinEditKaitei(ByVal buhinEditVo As TShisakuBuhinEditVo, _
                                    ByVal shisakuListCode As String, _
                                    ByVal shisakuListCodeKaiteiNo As String, _
                                    ByVal Flag As String, _
                                    ByVal zenkaiBlockKaiteiNo As String)


        ''' <summary>
        ''' 部品編集改訂情報を追加する(削除)
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="zenkaiBlockNoKaiteiNo">前回のブロックNo改訂No</param>
        ''' <param name="zenkaibuhinNoHyoujiJun">前回の部品番号表示順</param>
        ''' <param name="zenkaiBuhinEditVo">部品編集ベース情報</param>
        ''' <param name="Flag">フラグ</param>
        ''' <remarks></remarks>
        Sub InsertByBuhinEditKaiteiDel(ByVal shisakuEventCode As String, _
                                       ByVal shisakuListCode As String, _
                                       ByVal shisakuListCodeKaiteiNo As String, _
                                       ByVal shisakuBukaCode As String, _
                                       ByVal shisakuBlockNo As String, _
                                       ByVal shisakuBlockNoKaiteiNo As String, _
                                       ByVal zenkaiBlockNoKaiteiNo As String, _
                                       ByVal zenkaibuhinNoHyoujiJun As Integer, _
                                       ByVal zenkaiBuhinEditVo As TShisakuBuhinEditVo, _
                                       ByVal Flag As String)


        ''' <summary>
        ''' 部品編集号車改訂情報を追加する(削除)
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <param name="zenkaiShisakuBlockNoKaiteiNo">前回試作ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="zenkaishisakuGousyaHyoujiJun">前回試作号車表示順</param>
        ''' <param name="zenkaishisakuGousya">前回試作号車</param>
        ''' <param name="zenkaiInsuSuryo">前回員数数量</param>
        ''' <param name="Flag">フラグ</param>
        ''' <remarks></remarks>
        Sub InsertByBuhinEditGousyaKaiteiDel(ByVal shisakuEventCode As String, _
                                             ByVal shisakuListCode As String, _
                                             ByVal shisakuListCodeKaiteiNo As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal shisakuBlockNoKaiteiNo As String, _
                                             ByVal zenkaiShisakuBlockNoKaiteiNo As String, _
                                             ByVal buhinNoHyoujiJun As Integer, _
                                             ByVal zenkaishisakuGousyaHyoujiJun As Integer, _
                                             ByVal zenkaishisakuGousya As String, _
                                             ByVal zenkaiInsuSuryo As Integer, _
                                             ByVal Flag As String)

        ''' <summary>
        ''' 手配帳改訂抽出ブロック情報を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="konkaiBlockNoKaiteiNo">今回ブロックNo改訂No</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiKaitei(ByVal shisakuEventCode As String, _
                                ByVal shisakuListCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal konkaiBlockNoKaiteiNo As String)

        ''' <summary>
        ''' 部品編集改訂情報を追加する(削除前回)
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="zenkaibuhinEditVo">前回部品編集情報</param>
        ''' <param name="Flag">フラグ</param>
        ''' <remarks></remarks>
        Sub InsertByZenkaiBuhinEditKaiteiDel(ByVal shisakuEventCode As String, _
                                             ByVal shisakuListCode As String, _
                                             ByVal shisakuListCodeKaiteiNo As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal shisakuBlockNoKaiteiNo As String, _
                                             ByVal buhinNoHyoujiJun As String, _
                                             ByVal zenkaibuhinEditVo As TShisakuBuhinEditVo, _
                                             ByVal Flag As String)

        ''' <summary>
        ''' 変更前、変更後の追加処理
        ''' </summary>
        ''' <param name="buhinEditBaseVo">前回部品編集情報</param>
        ''' <param name="buhinEditVo">部品編集情報</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂No</param>
        ''' <remarks></remarks>
        Sub InsertByChange(ByVal buhinEditBaseVo As TShisakuBuhinEditVo, ByVal buhinEditVo As TShisakuBuhinEditVo, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String)


        ''' <summary>
        ''' 手配帳形式にするために追加
        ''' </summary>
        ''' <param name="BuhinEditVoList"></param>
        ''' <remarks></remarks>
        Sub InsertByBuhinEditTehaiKaitei(ByVal BuhinEditVoList As List(Of TShisakuBuhinEditVoSekkeiHelper), ByVal zenkaiFlag As Boolean)

        ''' <summary>
        ''' 手配帳形式にするために追加
        ''' </summary>
        ''' <param name="BuhinEditVoList"></param>
        ''' <remarks></remarks>
        Sub InsertByBuhinEditGousyaKaitei(ByVal BuhinEditVoList As List(Of TShisakuBuhinEditVoSekkeiHelper), ByVal zenkaiFlag As Boolean)


#End Region

#Region "更新する処理"
        '2012/02/22 UpdateByShisakuEvent追加


        ''' <summary>
        ''' 最新抽出日を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <remarks></remarks>
        Sub UpdateBySaishinChusyutu(ByVal shisakuEventCode As String, _
                                    ByVal shisakuListCode As String, _
                                    ByVal kaiteiNo As String, _
                                    ByVal chushutsuBi As Integer, _
                                    ByVal chushutsuJikan As Integer)

        ''' <summary>
        ''' 手配帳改訂抽出ブロック情報を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="konkaiBlockNoKaiteiNo">今回ブロックNo改訂No</param>
        ''' <remarks></remarks>
        Sub UpdateByTehaiKaitei(ByVal shisakuEventCode As String, _
                                ByVal shisakuListCode As String, _
                                ByVal shisakuBukaCode As String, _
                                ByVal shisakuBlockNo As String, _
                                ByVal konkaiBlockNoKaiteiNo As String)

        ''' <summary>
        ''' 試作イベントの最終改訂抽出日を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Sub UpdateByShisakuEvent(ByVal shisakuEventCode As String, ByVal chushutsuNichiji As Integer)

#End Region

#Region "削除する"

        ''' <summary>
        ''' 部品編集改訂を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByBuhinEditKaitei(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String)

        ''' <summary>
        ''' 部品編集改訂を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByBuhinEditKaiteiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String)

        ''' <summary>
        ''' 今回と前回の手配帳改定情報を削除
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <remarks></remarks>
        Sub DelteByTmpEdit(ByVal shisakuEventCode As String)



#End Region

#Region "自動織込み用"

        ''' <summary>
        ''' 最新の改訂ブロック情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>最新の改訂ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindByKaiteiBlockList(ByVal eventcode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKaiteiBlockVo)

        ''' <summary>
        ''' 今回の改訂部品情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns>今回の改訂ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditKaiteiList(ByVal eventcode As String, _
                                           ByVal shisakuListCode As String, _
                                           ByVal shisakuListCodeKaiteiNo As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditKaiteiVo)

        ''' <summary>
        ''' ブロック単位で手配帳基本情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns>手配帳基本情報</returns>
        ''' <remarks></remarks>
        Function FindByTehaiKihonBlockList(ByVal eventcode As String, _
                                           ByVal shisakuListCode As String, _
                                           ByVal shisakuListCodeKaiteiNo As String, _
                                           ByVal shisakuBukaCode As String, _
                                           ByVal shisakuBlockNo As String) As List(Of TShisakuTehaiKihonVo)

        ''' <summary>
        ''' 最新のリストコード情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <returns>最新のリストコード情報</returns>
        ''' <remarks></remarks>
        Function FindByListCode(ByVal eventcode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As TShisakuListcodeVo

        ''' <summary>
        ''' ブロック情報を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlock(ByVal eventcode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As TShisakuSekkeiBlockVo

        ''' <summary>
        ''' 合計員数数量を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <param name="shisakuGroup">グループ</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Function GetTotalInsuSuryo(ByVal eventcode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal shisakuBlockNoKaiteiNo As String, _
                                   ByVal shisakuGroup As String, _
                                   ByVal buhinNoHyoujiJun As Integer) As Integer

        ''' <summary>
        ''' 最後の行IDと部品番号表示順を取得する
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindByLatestGyouIdBuhinNoHyoujiJun(ByVal eventcode As String, _
                                                    ByVal shisakuListCode As String, _
                                                    ByVal shisakuListKaiteiNo As String, _
                                                    ByVal shisakuBukaCode As String, _
                                                    ByVal shisakuBlockNo As String) As TShisakuTehaiKihonVo

        ''' <summary>
        ''' 自動織込みできるか手配基本情報を取得する
        ''' </summary>
        ''' <param name="kihonVo"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByOrikomiKihon(ByVal kihonVo As TShisakuBuhinEditKaiteiVo, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiKihonVo)

        ''' <summary>
        ''' 手配号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByTehaiGousyaList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String, ByVal shiakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuTehaiGousyaVo)


        ''' <summary>
        ''' 号車の員数を取得する
        ''' </summary>
        ''' <param name="shisakuEvetnCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByGousyaInsuList(ByVal shisakuEvetnCode As String, _
                                      ByVal shisakuListCode As String, _
                                      ByVal shisakuListCodeKaiteiNo As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal shisakuBlockNoKaiteiNo As String, _
                                      ByVal buhinNoHyoujiJun As Integer, _
                                      ByVal flag As String) As List(Of TShisakuTehaiGousyaVo)

        ''' <summary>
        ''' 手配基本情報と手配号車情報を削除
        ''' </summary>
        ''' <param name="kihonVo">手配基本情報</param>
        ''' <remarks></remarks>
        Sub DelteByKihonGousya(ByVal kihonVo As TShisakuTehaiKihonVo)

        ''' <summary>
        ''' 手配基本情報と手配号車情報を削除(ブロック単位)
        ''' </summary>
        ''' <param name="buhinVo">部品編集改訂情報</param>
        ''' <remarks></remarks>
        Sub DelteByKihonGousyaBlock(ByVal buhinVo As TShisakuBuhinEditKaiteiVo)

        ''' <summary>
        ''' 手配号車情報を削除
        ''' </summary>
        ''' <param name="kihonVo">手配号車情報</param>
        ''' <remarks></remarks>
        Sub DelteByGousya(ByVal kihonVo As TShisakuTehaiGousyaVo)

        ''' <summary>
        ''' 手配基本情報を追加
        ''' </summary>
        ''' <param name="kihonVo">手配基本情報</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiKihon(ByVal kihonVo As TShisakuTehaiKihonVo)

        ''' <summary>
        ''' 手配号車情報を追加
        ''' </summary>
        ''' <param name="gousyaVo">手配号車情報</param>
        ''' <remarks></remarks>
        Sub InsertByTehaiGousya(ByVal gousyaVo As TShisakuTehaiGousyaVo)

        ''' <summary>
        ''' 手配基本情報を追加
        ''' </summary>
        ''' <param name="kihonVo">手配基本情報</param>
        ''' <remarks></remarks>
        Sub UpdateByTehaiKihon(ByVal kihonVo As TShisakuTehaiKihonVo)

        ''' <summary>
        ''' 手配号車情報を追加
        ''' </summary>
        ''' <param name="gousyaVo">手配号車情報</param>
        ''' <remarks></remarks>
        Sub UpdateByTehaiGousya(ByVal gousyaVo As TShisakuTehaiGousyaVo)

        ''' <summary>
        ''' リストコード情報の更新
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <remarks></remarks>
        Sub UpdateByListCode(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String)

        ''' <summary>
        ''' 部品編集改訂を更新
        ''' </summary>
        ''' <param name="buhinVo">手配基本情報</param>
        ''' <remarks></remarks>
        Sub UpdateByOrikomi(ByVal buhinVo As TShisakuBuhinEditKaiteiVo)

        ''' <summary>
        ''' DUMMY列を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <remarks></remarks>
        Function FindByDummy(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Integer) As TShisakuTehaiGousyaVo

        ''' <summary>
        ''' DUMMY列を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <remarks></remarks>
        Function FindByDummy2(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As TShisakuTehaiGousyaVo

        ''' <summary>
        ''' DUMMY列を取得
        ''' 2013/12/02　DUMMY列の最大値を求めないと号車列と被って落ちることがあるので対策として追加。
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Function FindByDummy2222(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As TShisakuTehaiGousyaVo

        ''' <summary>
        ''' 号車表示順最大値を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <remarks></remarks>
        Function FindByMaXGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As TShisakuTehaiGousyaVo



        ''' <summary>
        ''' バックアップ用に手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByAllTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiKihonVo)

        ''' <summary>
        ''' バックアップ用に手配号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByAllTehaiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiGousyaVo)


        ''' <summary>
        ''' バックアップの手配基本情報を追加する
        ''' </summary>
        ''' <param name="tehaiKihonList">手配基本情報</param>
        ''' <remarks></remarks>
        Sub InsertByAllTehaiKihon(ByVal tehaiKihonList As List(Of TShisakuTehaiKihonVo))

        ''' <summary>
        ''' バックアップの手配基本情報を追加する
        ''' </summary>
        ''' <param name="tehaiGousyaList">手配号車情報</param>
        ''' <remarks></remarks>
        Sub InsertByAllTehaiGousya(ByVal tehaiGousyaList As List(Of TShisakuTehaiGousyaVo))

        ''' <summary>
        ''' バックアップ用に手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <remarks></remarks>
        Sub DeleteByAllTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String)

        ''' <summary>
        ''' バックアップ用に手配号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <remarks></remarks>
        Sub DeleteByAllTehaiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String)

#End Region


        ''' <summary>
        ''' 試作手配ブロック情報を取得用
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBlockNo">ブロック№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByNewBlockList(ByVal eventcode As String, _
                                   ByVal shisakuListCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal syouninNichiji As Long) As List(Of ShisakuBuhinBlockVo)


        ''' <summary>
        ''' 改訂抽出EXCELと手配帳との差分用に号車員数情報を取得する。
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="buhinNoHyoujiJun"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByGousyaInsu(ByVal shisakuEventCode As String, _
                                         ByVal shisakuListCode As String, _
                                         ByVal shisakuListCodeKaiteiNo As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuTehaiGousyaVo)

        ''' <summary>
        ''' 改訂抽出EXCELと手配帳との差分用に基本情報を取得する。
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuListCode"></param>
        ''' <param name="shisakuListCodeKaiteiNo"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <param name="buhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByKihonSabun(ByVal shisakuEventCode As String, _
                                         ByVal shisakuListCode As String, _
                                         ByVal shisakuListCodeKaiteiNo As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal buhinNo As String) As List(Of TShisakuTehaiKihonVo)


        ''' <summary>
        ''' ダミーを除く号車表示順の最大値を取得
        ''' </summary>
        ''' <param name="shisakueventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リスト改訂№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByMaxGousyaHyoujijunNotDummy(ByVal shisakuEventCode As String, _
                                                  ByVal shisakuListCode As String, _
                                                  ByVal shisakuListCodeKaiteiNo As String) As Integer

        ''' <summary>
        ''' ダミー列情報の号車表示順を更新
        ''' </summary>
        ''' <param name="shisakueventcode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リスト改訂№</param>
        ''' <param name="shisakuGousyaHyoujijun"></param>
        ''' <remarks></remarks>
        Sub UpdateByMaxGousyaHyoujijunDummy(ByVal shisakuEventCode As String, _
                                            ByVal shisakuListCode As String, _
                                            ByVal shisakuListCodeKaiteiNo As String, _
                                            ByVal shisakuGousyaHyoujijun As Integer)

        Sub SetTehaiKaiteiBlock(ByVal BlockNoList As List(Of TShisakuTehaiKaiteiBlockVo))

        ''' <summary>
        ''' 手配号車情報を取得する（納期取得用）
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByTehaiGousyaNouki(ByVal shisakuEventCode As String, _
                                        ByVal shisakuListCode As String, _
                                        ByVal shisakuListCodeKaiteiNo As String, _
                                        ByVal shisakuBukaCode As String, _
                                        ByVal shisakuBlockNo As String, _
                                        ByVal shisakuGousya As String) As TShisakuTehaiGousyaVo


    End Interface
End Namespace