Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon

Namespace ShisakuBuhinEditBlock.Dao
    Public Interface BuhinEditBaseDao

        ''' <summary>
        ''' 設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shsiakuBlockNoKaiteiNo">試作ブロックNo改訂№</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlock(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, ByVal shsiakuBlockNoKaiteiNo As String) As List(Of TShisakuSekkeiBlockVo)

        ''' <summary>
        ''' 全設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>設計ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlockAll(ByVal shisakuEventCode As String) As List(Of TShisakuSekkeiBlockVo)

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

        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="instlHinban">INSTL品番</param>
        ''' <param name="instlHinbanKbn">INSTL品番区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEdit2(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBlockNo As String, _
                                 ByVal instlHinban As String, _
                                 ByVal instlHinbanKbn As String, _
                                 ByVal InstlDataKbn As String) As List(Of TShisakuBuhinEditVo)
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
                                      ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockInstlVo)
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
        Function FindByBuhinEditInstl2(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal instlHinban As String, _
                                      ByVal instlHinbanKbn As String, _
                                      ByVal InstlDataKbn As String) As List(Of TShisakuBuhinEditInstlVo)
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
        ''' <remarks></remarks>
        Sub InsertBySekkeiBuhinEdit(ByVal shisakuEventCode As String, _
                                    ByVal shisakuBukaCode As String, _
                                    ByVal shisakuBlockNo As String, _
                                    ByVal shisakuBlockNoKaiteiNo As String, _
                                    ByVal koseiMatrix As BuhinKoseiMatrix, _
                                    ByVal JikyuUmu As String, _
                                    ByVal TsukurikataTenkaiFlg As String, _
                                    ByVal loginInfo As LoginInfo)
        ''↑↑2014/07/24 Ⅰ.2.管理項目追加_al) (TES)張 CHG END


        ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 bb) (TES)張 CHG START
        'Sub InsertBySekkeiBuhinEditInstl(ByVal shisakuEventCode As String, _
        '                               ByVal shisakuBukaCode As String, _
        '                               ByVal shisakuBlockNo As String, _
        '                               ByVal shisakuBlockNoKaiteiNo As String, _
        '                               ByVal koseiMatrix As BuhinKoseiMatrix)
        ''' <summary>
        ''' 部品表編集INSTLと部品編集ベースINSTLにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <remarks></remarks>
        Sub InsertBySekkeiBuhinEditInstl(ByVal shisakuEventCode As String, _
                                         ByVal shisakuBukaCode As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal shisakuBlockNoKaiteiNo As String, _
                                         ByVal koseiMatrix As BuhinKoseiMatrix, _
                                         ByVal login As LoginInfo)
        ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 bb) (TES)張 CHG END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_ai) (TES)張 CHG BEGIN
        'Sub InsertBySekkeiBuhinEditEvent(ByVal shisakuEventCode As String, _
        '                                 ByVal shisakuBukaCode As String, _
        '                                 ByVal shisakuBlockNo As String, _
        '                                 ByVal shisakuBlockNoKaiteiNo As String, _
        '                                 ByVal koseiMatrix As BuhinKoseiMatrix, _
        '                                 ByVal JikyuUmu As String)
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
        ''' <remarks></remarks>
        Sub InsertBySekkeiBuhinEditEvent(ByVal shisakuEventCode As String, _
                                         ByVal shisakuBukaCode As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal shisakuBlockNoKaiteiNo As String, _
                                         ByVal koseiMatrix As BuhinKoseiMatrix, _
                                         ByVal JikyuUmu As String, _
                                         ByVal TsukurikataTenkaiFlg As String)
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_ai) (TES)張 CHG END


        ''' <summary>
        ''' 部品表編集INSTLと部品編集ベースINSTLにINSERTする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <param name="JikyuUmu">自給品の有無</param>
        ''' <param name="koseiMatrix">構成マトリクス</param>
        ''' <remarks></remarks>
        Sub InsertBySekkeiBuhinEditInstlEvent(ByVal shisakuEventCode As String, _
                                              ByVal shisakuBukaCode As String, _
                                              ByVal shisakuBlockNo As String, _
                                              ByVal shisakuBlockNoKaiteiNo As String, _
                                              ByVal koseiMatrix As BuhinKoseiMatrix, _
                                              ByVal JikyuUmu As String)


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
                                     ByVal koseiMatrix As BuhinKoseiMatrix)

        ''' <summary>
        ''' 購担/取引先を取得する
        ''' </summary>
        ''' <param name="buhinNo">部品No</param>
        ''' <returns>購担と取引先</returns>
        ''' <remarks></remarks>
        Function FindByKoutanTorihikisaki(ByVal buhinNo As String) As TShisakuBuhinEditVo

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