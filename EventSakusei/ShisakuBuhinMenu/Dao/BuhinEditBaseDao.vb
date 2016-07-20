Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Vo

''↓↓2014/09/15 酒井 ADD START
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
''↑↑2014/09/15 酒井 ADD END

Namespace ShisakuBuhinMenu.Dao
    Public Interface BuhinEditBaseDao

        ''' <summary>
        ''' 設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlock(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockVo)

        ''' <summary>
        ''' 全設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlockAll(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockVo)

        ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD START
        Function FindBySekkeiBlockEbomKanshiAll(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockEbomKanshiVo)
        ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 酒井 ADD END

        ''↓↓2014/09/18 酒井 ADD START
        Function FindByEventCode(ByVal shisakuEventCode As String) As List(Of TShisakuBuhinEditVo)
        'Function FindByLevelZero(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVo)
        Function FindByLevelZero2(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditInstlVo)

        Function FindByNotLevelZero(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVo)
        'Function FindByEventCodeEbomKanshi(ByVal shisakuEventCode As String) As List(Of TShisakuBuhinEditVo)
        'Function FindByLevelZeroEbomKanshi(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVo)
        'Function FindByNotLevelZeroEbomKanshi(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVo)
        Sub UpdateLevelZeroBuhinNoHyoujiJunByBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer, ByVal updateBuhinNoHyoujiJun As Integer)
        Sub UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditInstl(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer, ByVal updateBuhinNoHyoujiJun As Integer)
        Sub UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer, ByVal updateBuhinNoHyoujiJun As Integer)
        Sub UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditInstlBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer, ByVal updateBuhinNoHyoujiJun As Integer)
        'Sub UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditEbomKanshi(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer, ByVal updateBuhinNoHyoujiJun As Integer)
        'Sub UpdateLevelZeroBuhinNoHyoujiJunByBuhinEditInstlEbomKanshi(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer, ByVal updateBuhinNoHyoujiJun As Integer)
        ''↑↑2014/09/18 酒井 ADD END
        '↓↓2014/10/30 酒井 ADD BEGIN
        Function FindByLevelZeroInstlHyoujiJun(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String, ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuBuhinEditInstlVo)
        '↑↑2014/10/30 酒井 ADD END

        '''' <summary>
        '''' 設計ブロックINSTL情報を取得
        '''' </summary>
        '''' <param name="shisakuEventCode">試作イベントコード</param>
        '''' <returns>設計ブロック情報</returns>
        '''' <remarks></remarks>
        'Function FindBySekkeiBlockInstl(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockInstlVo)

        ''' <summary>
        ''' 開発符号を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Function FindByKaihatsufugo(ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEdit(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVo)
        '↓↓2014/10/23 酒井 ADD BEGIN
        Function FindBuhin4UpdateBuhinHyoujiJun(ByVal shisakuEventCode As String) As List(Of Buhin4UpdateBuhinHyoujiJunVo)
        Function FindBuhin4UpdateBuhinHyoujiJunBase(ByVal shisakuEventCode As String) As List(Of Buhin4UpdateBuhinHyoujiJunVo)
        '↑↑2014/10/23 酒井 ADD END
        '↓↓2014/10/07 酒井 ADD BEGIN
        Function FindBuhin4UpdateInstlHyoujiJun(ByVal blockVo As TShisakuSekkeiBlockVo) As List(Of Buhin4UpdateInstlHyoujiJunVo)
        Function FindNewInstlHyoujiJun(ByVal Buhin4UpdateInstlHyoujiJunVos As List(Of Buhin4UpdateInstlHyoujiJunVo)) As List(Of Buhin4UpdateInstlHyoujiJunVo)
        Sub UpdateNewInstlHyoujiJun(ByVal Buhin4UpdateInstlHyoujiJunVos As List(Of Buhin4UpdateInstlHyoujiJunVo))
        Sub UpdateNewInstlHyoujiJun2(ByVal blockVo As TShisakuSekkeiBlockVo)
        '↑↑2014/10/07 酒井 ADD END
        '↓↓2014/10/08 酒井 ADD BEGIN
        Function FindEventInstlHyoujiJun(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuSekkeiBlockInstlVo)
        '↑↑2014/10/08 酒井 ADD END
        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEdit2(ByVal orgShisakuEventCode As String, _
                                 ByVal shisakuEventCode As String, _
                                 ByVal shisakuBlockNo As String, _
                                 ByVal instlHinban As String, _
                                 ByVal instlHinbanKbn As String, _
                                 ByVal InstlDataKbn As String, _
                                 ByVal instlHinbanHyojijyun As Integer, _
                                 Optional ByVal BaseInstlFlg As String = "") As List(Of TShisakuBuhinEditVo)
        '↓↓2014/09/30 酒井 ADD BEGIN
        'Function FindByBuhinEdit2(ByVal shisakuEventCode As String, _
        'ByVal shisakuBlockNo As String, _
        'ByVal instlHinban As String, _
        'ByVal instlHinbanKbn As String, _
        'ByVal InstlDataKbn As String) As List(Of TShisakuBuhinEditVo)
        '↑↑2014/09/30 酒井 ADD END
        ' <param name="shisakuBukaCode">部課コード</param>
        'Function FindByBuhinEdit2(ByVal shisakuEventCode As String, _
        '                         ByVal shisakuBukaCode As String, _
        '                         ByVal shisakuBlockNo As String, _
        '                         ByVal instlHinban As String, _
        '                         ByVal instlHinbanKbn As String) As List(Of TShisakuBuhinEditVo)



        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks>注意！！BuhinNoとBuhinNoKbnをSEKKEI_BLOCK_INSTLのINSTL_HINBANとINSTL_HINBAN_KBNに置き換えたもの</remarks>
        Function FindByBuhinEditKai(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVo)
        ' <param name="shisakuBukaCode">部課コード</param>
        'Function FindByBuhinEditKai(ByVal shisakuEventCode As String, _
        '                         ByVal shisakuBukaCode As String, _
        '                         ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVo)

        ''' <summary>
        ''' 設計ブロックINSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlockInstl(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockInstlVoEx)
        '''' <param name="shisakuBukaCode">部課コード</param>
        'Function FindBySekkeiBlockInstl(ByVal shisakuEventCode As String, _
        '                              ByVal shisakuBukaCode As String, _
        '                              ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockInstlVo)

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditInstl(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditInstlVo)
        ' <param name="shisakuBukaCode">部課コード</param>
        'Function FindByBuhinEditInstl(ByVal shisakuEventCode As String, _
        '                              ByVal shisakuBukaCode As String, _
        '                              ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditInstlVo)

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditInstl2(ByVal orgShisakuEventCode As String, _
                                      ByVal shisakuEventCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal instlHinban As String, _
                                      ByVal instlHinbanKbn As String, _
                                      ByVal InstlDataKbn As String, _
                                      ByVal instlHinbanHyojijyun As Integer, _
                                      Optional ByVal BaseInstlFlg As String = "") As List(Of TShisakuBuhinEditInstlVo)
        '↓↓2014/09/30 酒井 ADD BEGIN
        'Function FindByBuhinEditInstl2(ByVal shisakuEventCode As String, _
        'ByVal shisakuBlockNo As String, _
        'ByVal instlHinban As String, _
        'ByVal instlHinbanKbn As String, _
        'ByVal InstlDataKbn As String) As List(Of TShisakuBuhinEditInstlVo)
        '↑↑2014/09/30 酒井 ADD END
        ' <param name="shisakuBukaCode">部課コード</param>
        'Function FindByBuhinEditInstl2(ByVal shisakuEventCode As String, _
        '                              ByVal shisakuBukaCode As String, _
        '                              ByVal shisakuBlockNo As String, _
        '                              ByVal instlHinban As String, _
        '                              ByVal instlHinbanKbn As String) As List(Of TShisakuBuhinEditInstlVo)


        ''↓↓2014/07/24 Ⅰ.2.管理項目追加_al) (TES)張 CHG BEGIN
        'Sub InsertBySekkeiBuhinEdit(ByVal shisakuEventCode As String, _
        '                            ByVal shisakuBukaCode As String, _
        '                            ByVal shisakuBlockNo As String, _
        '                            ByVal shisakuBlockNoKaiteiNo As String, _
        '                            ByVal koseiMatrix As BuhinKoseiMatrix, _
        '                            ByVal JikyuUmu As String)

        '/*** 20140911 CHANGE START（引数追加） ***/
        ''' <summary>
        ''' 部品表編集と部品編集ベースにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <param name="TsukurikataTenkaiFlg">作り方フラグ</param>
        ''' <param name="aDate">試作日</param>
        ''' <param name="MaxUpdateBuhinNoHyoujijun">部品No毎の最大表示順</param>
        Sub InsertBySekkeiBuhinEdit(ByVal shisakuEventCode As String, _
                                    ByVal shisakuBukaCode As String, _
                                    ByVal shisakuBlockNo As String, _
                                    ByVal shisakuBlockNoKaiteiNo As String, _
                                    ByVal koseiMatrix As BuhinKoseiMatrix, _
                                    ByVal JikyuUmu As String, _
                                    ByVal TsukurikataTenkaiFlg As String, _
                                    ByVal loginInfo As LoginInfo, _
                                    ByVal aDate As ShisakuDate, Optional ByVal MaxUpdateBuhinNoHyoujijun As Integer = 0)
        ''↓↓2014/09/12 1 ベース部品表作成表機能増強 酒井ADD BEGIN
        Sub InsertBySekkeiBuhinEdit(ByVal shisakuEventCode As String, _
                            ByVal shisakuBukaCode As String, _
                            ByVal shisakuBlockNo As String, _
                            ByVal shisakuBlockNoKaiteiNo As String, _
                            ByVal koseiMatrix As BuhinKoseiMatrix, _
                            ByVal JikyuUmu As String, _
                            ByVal TsukurikataTenkaiFlg As String, _
                            ByVal loginInfo As LoginInfo)
        'Sub InsertBySekkeiBuhinEdit(ByVal shisakuEventCode As String, _
        'ByVal shisakuBukaCode As String, _
        'ByVal shisakuBlockNo As String, _
        'ByVal shisakuBlockNoKaiteiNo As String, _
        'ByVal koseiMatrix As BuhinKoseiMatrix, _
        'ByVal JikyuUmu As String, _
        'ByVal TsukurikataTenkaiFlg As String, _
        'ByVal loginInfo As LoginInfo)
        ''↑↑2014/09/12 1 ベース部品表作成表機能増強 酒井ADD END
        ''↑↑2014/07/24 Ⅰ.2.管理項目追加_al) (TES)張 CHG END

        '/*** 20140911 CHANGE END ***/

        ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 bb) (TES)張 CHG START
        'Sub InsertBySekkeiBuhinEditInstl(ByVal shisakuEventCode As String, _
        '                               ByVal shisakuBukaCode As String, _
        '                               ByVal shisakuBlockNo As String, _
        '                               ByVal shisakuBlockNoKaiteiNo As String, _
        '                               ByVal koseiMatrix As BuhinKoseiMatrix)


        '/*** 20140911 CHANGE START（引数追加） ***/
        ''' <summary>
        ''' 部品表編集INSTLと部品編集ベースINSTLにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="aDate">試作日</param>
        ''' <remarks></remarks>
        Sub InsertBySekkeiBuhinEditInstl(ByVal shisakuEventCode As String, _
                                         ByVal shisakuBukaCode As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal shisakuBlockNoKaiteiNo As String, _
                                         ByVal koseiMatrix As BuhinKoseiMatrix, _
                                         ByVal login As LoginInfo, _
                                         ByVal aDate As ShisakuDate, Optional ByVal MaxUpdateBuhinNoHyoujijun As Integer = 0, Optional ByVal MaxUpdateInstlHyoujijun As Integer = 0, Optional ByVal instlTitle As BuhinEditKoseiInstlTitle = Nothing)
        ''↓↓2014/09/12 1 ベース部品表作成表機能増強 酒井ADD BEGIN
        Sub InsertBySekkeiBuhinEditInstl(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBlockNo As String, _
                                 ByVal shisakuBlockNoKaiteiNo As String, _
                                 ByVal koseiMatrix As BuhinKoseiMatrix, _
                                 ByVal login As LoginInfo)
        'Sub InsertBySekkeiBuhinEditInstl(ByVal shisakuEventCode As String, _
        'ByVal shisakuBukaCode As String, _
        'ByVal shisakuBlockNo As String, _
        'ByVal shisakuBlockNoKaiteiNo As String, _
        'ByVal koseiMatrix As BuhinKoseiMatrix, _
        'ByVal login As LoginInfo)
        ''↑↑2014/09/12 1 ベース部品表作成表機能増強 酒井ADD END
        ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 bb) (TES)張 CHG END

        '/*** 20140911 CHANGE END ***/


        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ai) (TES)張 CHG BEGIN
        'Sub InsertBySekkeiBuhinEditEvent(ByVal shisakuEventCode As String, _
        '                                 ByVal shisakuBukaCode As String, _
        '                                 ByVal shisakuBlockNo As String, _
        '                                 ByVal shisakuBlockNoKaiteiNo As String, _
        '                                 ByVal koseiMatrix As BuhinKoseiMatrix, _
        '                                 ByVal JikyuUmu As String)


        '/*** 20140911 CHANGE START（引数追加） ***/
        ''' <summary>
        ''' 部品表編集と部品編集ベースにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <param name="TsukurikataTenkaiFlg">作り方フラグ</param>
        ''' <param name="aDate">試作日</param>
        ''' <param name="MaxUpdateBuhinNoHyoujijun">部品No毎の最大表示順</param>
        ''' <remarks></remarks>
        Sub InsertBySekkeiBuhinEditEvent(ByVal shisakuEventCode As String, _
                                         ByVal shisakuBukaCode As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal shisakuBlockNoKaiteiNo As String, _
                                         ByVal koseiMatrix As BuhinKoseiMatrix, _
                                         ByVal JikyuUmu As String, _
                                         ByVal TsukurikataTenkaiFlg As String, _
                                         ByVal aDate As ShisakuDate, Optional ByRef MaxUpdateBuhinNoHyoujijun As Integer = 0)
        ''↓↓2014/09/12 1 ベース部品表作成表機能増強 酒井ADD BEGIN        
        Sub InsertBySekkeiBuhinEditEvent(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBlockNo As String, _
                                 ByVal shisakuBlockNoKaiteiNo As String, _
                                 ByVal koseiMatrix As BuhinKoseiMatrix, _
                                 ByVal JikyuUmu As String, _
                                 ByVal TsukurikataTenkaiFlg As String)
        'Sub InsertBySekkeiBuhinEditEvent(ByVal shisakuEventCode As String, _
        'ByVal shisakuBukaCode As String, _
        'ByVal shisakuBlockNo As String, _
        'ByVal shisakuBlockNoKaiteiNo As String, _
        'ByVal koseiMatrix As BuhinKoseiMatrix, _
        'ByVal JikyuUmu As String, _
        'ByVal TsukurikataTenkaiFlg As String)
        ''↑↑2014/09/12 1 ベース部品表作成表機能増強 酒井ADD END
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ai) (TES)張 CHG END

        '/*** 20140911 CHANGE END ***/



        '/*** 20140911 CHANGE START（引数追加） ***/
        ''' <summary>
        ''' 部品表編集INSTLと部品編集ベースINSTLにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <param name="aDate">試作日</param>
        ''' <remarks></remarks>
        Sub InsertBySekkeiBuhinEditInstlEvent(ByVal shisakuEventCode As String, _
                                              ByVal shisakuBukaCode As String, _
                                              ByVal shisakuBlockNo As String, _
                                              ByVal shisakuBlockNoKaiteiNo As String, _
                                              ByVal koseiMatrix As BuhinKoseiMatrix, _
                                              ByVal JikyuUmu As String, _
                                              ByVal aDate As ShisakuDate, Optional ByVal paraInstlTitle As BuhinEditKoseiInstlTitle = Nothing)
        '↓↓2014/10/08 酒井 ADD BEGIN
        'Sub InsertBySekkeiBuhinEditInstlEvent(ByVal shisakuEventCode As String, _
        '                                      ByVal shisakuBukaCode As String, _
        '                                      ByVal shisakuBlockNo As String, _
        '                                      ByVal shisakuBlockNoKaiteiNo As String, _
        '                                      ByVal koseiMatrix As BuhinKoseiMatrix, _
        '                                      ByVal JikyuUmu As String, _
        '                                      ByVal aDate As ShisakuDate, Optional ByVal MaxUpdateInstlHyoujijun As Integer = 0)
        '↑↑2014/10/08 酒井 ADD END
        ''↓↓2014/09/12 1 ベース部品表作成表機能増強 酒井ADD BEGIN
        Sub InsertBySekkeiBuhinEditInstlEvent(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal shisakuBlockNoKaiteiNo As String, _
                                      ByVal koseiMatrix As BuhinKoseiMatrix, _
                                      ByVal JikyuUmu As String)
        'Sub InsertBySekkeiBuhinEditInstlEvent(ByVal shisakuEventCode As String, _
        'ByVal shisakuBukaCode As String, _
        'ByVal shisakuBlockNo As String, _
        'ByVal shisakuBlockNoKaiteiNo As String, _
        'ByVal koseiMatrix As BuhinKoseiMatrix, _
        'ByVal JikyuUmu As String)
        ''↑↑2014/09/12 1 ベース部品表作成表機能増強 酒井ADD END

        '/*** 20140911 CHANGE END ***/

        ''' <summary>
        ''' 部品表編集INSTLと部品編集ベースINSTLにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <remarks></remarks>
        Sub UpdateBySekkeiBlockInstl(ByVal shisakuEventCode As String, _
                                     ByVal shisakuBukaCode As String, _
                                     ByVal shisakuBlockNo As String, _
                                     ByVal koseiMatrix As BuhinKoseiMatrix, Optional ByRef MaxUpdateInstlHyoujijun As Integer = 0, Optional ByVal paraInstlTitle As BuhinEditKoseiInstlTitle = Nothing)
        '↓↓2014/09/30 酒井 ADD BEGIN
        'Sub UpdateBySekkeiBlockInstl(ByVal shisakuEventCode As String, _
        'ByVal shisakuBukaCode As String, _
        'ByVal shisakuBlockNo As String, _
        'ByVal koseiMatrix As BuhinKoseiMatrix, Optional ByRef MaxUpdateInstlHyoujijun As Integer = 0)
        '↑↑2014/09/30 酒井 ADD END
        ''↓↓2014/09/12 1 ベース部品表作成表機能増強 酒井ADD BEGIN
        'Sub UpdateBySekkeiBlockInstl(ByVal shisakuEventCode As String, _
        'ByVal shisakuBukaCode As String, _
        'ByVal shisakuBlockNo As String, _
        'ByVal koseiMatrix As BuhinKoseiMatrix)
        ''↑↑2014/09/12 1 ベース部品表作成表機能増強 酒井ADD END
        ''↓↓2014/09/12 1 ベース部品表作成表機能増強 酒井ADD BEGIN
        Sub UpdateBySekkeiBlockInstlEbom(ByVal shisakuEventCode As String, _
                                     ByVal shisakuBukaCode As String, _
                                     ByVal shisakuBlockNo As String, _
                                     ByVal koseiMatrix As BuhinKoseiMatrix, Optional ByRef MaxUpdateInstlHyoujijun As Integer = 0, Optional ByVal paraInstlTitle As BuhinEditKoseiInstlTitle = Nothing)
        '↓↓2014/10/08 酒井 ADD BEGIN
        'Sub UpdateBySekkeiBlockInstlEbom(ByVal shisakuEventCode As String, _
                             'ByVal shisakuBukaCode As String, _
        'ByVal shisakuBlockNo As String, _
        'ByVal koseiMatrix As BuhinKoseiMatrix, Optional ByRef MaxUpdateInstlHyoujijun As Integer = 0)
        '↑↑2014/10/08 酒井 ADD END
        ''↑↑2014/09/12 1 ベース部品表作成表機能増強 酒井ADD END
        ''↓↓2014/09/12 1 ベース部品表作成表機能増強 酒井ADD BEGIN
        Sub UpdateInstlHinbanHyoujiJunBySekkeiBlockInstl(ByVal deleteInstl As TShisakuSekkeiBlockInstlVo)
        Sub UpdateInstlHinbanHyoujiJunByBuhinEditInstl(ByVal deleteInstl As TShisakuSekkeiBlockInstlVo)
        Sub UpdateBuhinNoHyoujiJunByBuhinEditInstl(ByVal deleteBuhin As List(Of TShisakuBuhinEditVo))
        Sub UpdateBuhinNoHyoujiJunByBuhinEdit(ByVal deleteBuhin As List(Of TShisakuBuhinEditVo))
        ''↑↑2014/09/12 1 ベース部品表作成表機能増強 酒井ADD END
        ''' <summary>
        ''' 購担/取引先を取得する
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Function FindByKoutanTorihikisaki(ByVal buhinNo As String) As TShisakuBuhinEditVo


'/*** 20140911 CHANGE START ***/
        ''' <summary>
        ''' 購担/取引先を取得する()
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <param name="kotanTorihikisakiSelected">取得済みの購担と取引先</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Function FindByKoutanTorihikisakiUseDictionary(ByVal buhinNo As String, _
                                                            ByRef kotanTorihikisakiSelected As Dictionary(Of String, TShisakuBuhinEditVo)) As TShisakuBuhinEditVo
'/*** 20140911 CHANGE END ***/
        ''' <summary>
        ''' 設計ブロックINSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByInstlHinban(ByVal shisakuEventCode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal instlHinban As String, _
                                   ByVal instlHinbanKbn As String) As TShisakuSekkeiBlockInstlVo
        '↓↓2014/10/08 酒井 ADD BEGIN
        Function FindByInstlHinbanBaseEvent(ByVal shisakuEventCode As String, _
                           ByVal shisakuBukaCode As String, _
                           ByVal shisakuBlockNo As String, _
                           ByVal instlHinban As String, _
                           ByVal instlHinbanKbn As String) As TShisakuSekkeiBlockInstlVo

        '↑↑2014/10/08 酒井 ADD END
        ''↓↓2014/09/15 1 ベース部品表作成表機能増強 酒井ADD BEGIN
        '↓↓2014/10/10 酒井 ADD BEGIN
        'Function FindByInstlHinbanBaseEbom(ByVal shisakuEventCode As String, _
        '                           ByVal shisakuBukaCode As String, _
        '                           ByVal shisakuBlockNo As String, _
        '                           ByVal instlHinban As String, _
        '                           ByVal instlHinbanKbn As String) As TShisakuSekkeiBlockInstlVo
        Function FindByInstlHinbanBaseEbom(ByVal shisakuEventCode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal instlHinban As String, _
                                   ByVal instlHinbanKbn As String, _
                                   ByVal instlDataKbn As String, _
                                   ByVal baseInstlFlg As String) As TShisakuSekkeiBlockInstlVo
        '↑↑2014/10/10 酒井 ADD END
        ''↑↑2014/09/15 1 ベース部品表作成表機能増強 酒井ADD END

        ''' <summary>
        ''' 設計ブロック情報と設計ブロックINSTL情報を削除
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <remarks></remarks>
        Sub DeleteBySekkeiBlockAndInstl(ByVal shisakuEventCode As String, _
                                        ByVal shisakuBukaCode As String, _
                                        ByVal shisakuBlockNo As String)

        ''↓↓2014/08/21 1 ベース部品表作成表機能増強_d ADD BEGIN
        ''' <summary>
        ''' 試作部品編集情報と員数を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinKousei(ByVal shisakuEventCode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal shisakuBlockNoKaiteiNo As String, _
                                   ByVal instlHinbanHyoujiJun As String) As List(Of BuhinEditInstlKoseiVo)
        ''↓↓2014/09/16 1 ベース部品表作成表機能増強 酒井 ADD BEGIN
        'Function FindByBuhinKousei(ByVal shisakuEventCode As String, _
        'ByVal shisakuBlockNo As String, _
        'ByVal shisakuBlockNoKaiteiNo As String) As List(Of BuhinEditInstlKoseiVo)
        ''↑↑2014/09/16 1 ベース部品表作成表機能増強 酒井 ADD END
        ''↑↑2014/08/21 1 ベース部品表作成表機能増強_d ADD END

        ''↓↓2014/09/16 1 ベース部品表作成表機能増強 酒井 ADD BEGIN
        Sub DeleteByBuhinKousei(ByVal shisakuEventCode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal shisakuBlockNoKaiteiNo As String, _
                                   ByVal instlHinbanHyoujiJun As String, ByRef deleteBuhin As List(Of TShisakuBuhinEditVo))
        ''↑↑2014/09/16 1 ベース部品表作成表機能増強 酒井 ADD END
        '↓↓2014/10/23 酒井 ADD BEGIN
        Sub UpdateNewBuhinHyoujiJun(ByVal UpdateVo As Buhin4UpdateBuhinHyoujiJunVo)
        Sub UpdateNewBuhinHyoujiJunBase(ByVal UpdateVo As Buhin4UpdateBuhinHyoujiJunVo)
        '↑↑2014/10/23 酒井 ADD END
#Region "部品構成(パンダ前)"

        ''' <summary>
        ''' 部品構成情報を取得
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号(親)</param>
        ''' <returns>部品構成情報</returns>
        ''' <remarks></remarks>
        Function FindByRhac0551(ByVal BuhinNoOya As String) As List(Of Rhac0551Vo)

        ''' <summary>
        ''' 部品情報を取得
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Function FindByRhac0530(ByVal BuhinNo As String) As Rhac0530Vo

#End Region

#Region "部品構成(図面)"

        ''' <summary>
        ''' 部品構成情報を取得
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号(親)</param>
        ''' <returns>部品構成情報</returns>
        ''' <remarks></remarks>
        Function FindByRhac0552(ByVal BuhinNoOya As String) As List(Of Rhac0552Vo)

        ''' <summary>
        ''' 部品情報を取得
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Function FindByRhac0532(ByVal BuhinNo As String) As Rhac0532Vo

#End Region

#Region "部品構成(FM5以降)"

        ''' <summary>
        ''' 部品構成情報を取得
        ''' </summary>
        ''' <param name="BuhinNoOya">部品番号(親)</param>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>部品構成情報</returns>
        ''' <remarks></remarks>
        Function FindByRhac0553(ByVal BuhinNoOya As String, ByVal kaihatsuFugo As String) As List(Of Rhac0553Vo)

        ''' <summary>
        ''' 部品情報を取得
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Function FindByRhac0533(ByVal BuhinNo As String) As Rhac0533Vo

        ''' <summary>
        ''' 部品情報を取得
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>部品情報</returns>
        ''' <remarks></remarks>
        Function FindByRhac1910(ByVal kaihatsuFugo As String, ByVal BuhinNo As String) As Rhac1910Vo


#End Region

    End Interface
End Namespace