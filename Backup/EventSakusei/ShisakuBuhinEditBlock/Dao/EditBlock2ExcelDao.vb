Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Dao
    ''' <summary>
    ''' Excel出力を集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface EditBlock2ExcelDao : Inherits DaoEachFeature



        ''' <summary>
        ''' Combox ブロック改訂No.
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNo">試作ブロック№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindKaiteiNoBy(ByVal eventCode As String, _
                             ByVal bukaCode As String, _
                             ByVal blockNo As String) _
                             As List(Of LabelValueVo)

        ''' <summary>
        ''' 試作A/L情報ヘッダ開発符号、試作部品イベント名称、担当者、Telを読む
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNo">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindHeadInfoWithSekkeiBlockBy(ByVal eventCode As String, _
                             ByVal bukaCode As String, _
                             ByVal blockNo As String, _
                             ByVal blockKaiteNo As String) _
                             As EditBlock2ExcelTitle3BodyVo

        ''' <summary>
        ''' 試作イベントベース車項目No.対応表の項目名を返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <param name="isBase">ベースか</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindWithTitle1NameBy(ByVal eventCode As String, _
                                      ByVal bukaCode As String, _
                                      ByVal blockNoList As List(Of String), _
                                      ByVal blockKaiteNo As String, _
                                      ByVal isBase As Boolean) _
                                      As List(Of EditBlock2ExcelTitle1Vo)
        ''' <summary>
        ''' 試作イベント装備仕様項目名を返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <param name="isBase">ベースか</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindWithTitle2NameBy(ByVal eventCode As String, _
                                      ByVal bukaCode As String, _
                                      ByVal blockNoList As List(Of String), _
                                      ByVal blockKaiteNo As String, _
                                      ByVal isBase As Boolean) _
                                      As List(Of EditBlock2ExcelTitle2Vo)
        ''' <summary>
        ''' 試作イベントメーモを返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindWithTitle3NameBy(ByVal eventCode As String, _
                            ByVal bukaCode As String, _
                            ByVal blockNoList As List(Of String), _
                            ByVal blockKaiteNo As String) _
                            As List(Of TShisakuSekkeiBlockMemoVo)
        ''↓↓2014/08/07 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_i) (TES)張 CHG BEGIN
        ''Function FindWithTitle4NameBy(ByVal eventCode As String, _
        ''                              ByVal bukaCode As String, _
        ''                              ByVal blockNo As String, _
        ''                              ByVal blockKaiteNo As String, _
        ''                              ByVal isBase As Boolean) _
        ''                              As List(Of TShisakuSekkeiBlockInstlVo)
        ''' <summary>
        ''' 試作イベントINSTL品番を返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <param name="isBase">ベースか</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindWithTitle4NameBy(ByVal eventCode As String, _
                                      ByVal bukaCode As String, _
                                      ByVal blockNoList As List(Of String), _
                                      ByVal blockKaiteNo As String, _
                                      ByVal isBase As Boolean) _
                                      As List(Of EditBlock2ExcelTitle4Vo)
        ''↑↑2014/08/07 Ⅰ.10.全ブロックEXCEL出力での出力内容変更_i) (TES)張 CHG END



        ''' <summary>
        ''' 試作イベント全て号車を返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindAllGouSya(ByVal eventCode As String) As List(Of TShisakuEventBaseVo)


        ''' <summary>
        ''' 号車の試作イベントベース車項目No.対応のデータVoを返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNo">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindWithTitle1BodyDataBy(ByVal eventCode As String, _
                             ByVal bukaCode As String, _
                             ByVal blockNo As String, _
                             ByVal blockKaiteNo As String, _
                             ByVal strGousya As String) _
                             As EditBlock2ExcelTitle1BodyVo
        ''' <summary>
        ''' 号車の試作イベント装備仕様のデータVoを返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNo">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindWithTitle2BodyDataBy(ByVal eventCode As String, _
                             ByVal bukaCode As String, _
                             ByVal blockNo As String, _
                             ByVal blockKaiteNo As String, _
                             ByVal strGousya As String) _
                             As List(Of EditBlock2ExcelTitle2BodyVo)
        ''' <summary>
        ''' 号車の試作イベントメーモのデータVoを返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindWithTitle3BodyDataBy(ByVal eventCode As String, _
                            ByVal bukaCode As String, _
                            ByVal blockNoList As List(Of String), _
                            ByVal blockKaiteNo As String, _
                            ByVal strGousya As String) _
                            As List(Of TShisakuSekkeiBlockMemoVo)
        ''' <summary>
        ''' 車の試作イベントINSTL品番のデータVoを返す
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNoList">試作ブロック№</param>
        ''' <param name="blockKaiteNo">試作ブロック№改訂№</param>
        ''' <param name="strGousya">号車</param>
        ''' <param name="isBase">ベースか？</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindWithTitle4BodyDataBy(ByVal eventCode As String, _
                                          ByVal bukaCode As String, _
                                          ByVal blockNoList As List(Of String), _
                                          ByVal blockKaiteNo As String, _
                                          ByVal strGousya As String, _
                                          Optional ByVal isBase As Boolean = False) _
                                          As List(Of TShisakuSekkeiBlockInstlVo)
        ''' <summary>
        ''' ブロックが新規作成されたかどうかを返す
        ''' 設計ブロックINSTLにあるかどうか
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="bukaCode">部課コード</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <returns>True:新規 False:既存</returns>
        ''' <remarks>ブロック一覧の「指定ブロックExcel出力」の履歴プルダウンに「ベース」を出力するかどうかの判定に利用</remarks>
        Function IsNewCreatedBlock(ByVal eventCode As String, _
                            ByVal bukaCode As String, _
                            ByVal blockNo As String) As Boolean

        ''' <summary>
        ''' 社員名を返す
        ''' </summary>
        ''' <param name="shainId">社員ID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByShainName(ByVal shainId As String) As String

#Region "比較用"
        ''' <summary>
        ''' 試作INSTL品番比較用を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <param name="isBase">ベースか？</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByInstlHinbanCondition(ByVal shisakuEventCode As String, _
                                            ByVal shisakuBukaCode As String, _
                                            ByVal shisakuBlockNo As String, _
                                            ByVal ShisakuBlockNoKaiteiNo As String, _
                                            Optional ByVal isBase As Boolean = False) _
                                            As List(Of TShisakuSekkeiBlockInstlVo)

        ''' <summary>
        ''' 部品編集情報ベースを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <param name="shisakuBukaCode"></param>
        ''' <param name="shisakuBlockNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditBaseCondtion(ByVal shisakuEventCode As String, _
                                                   ByVal shisakuBukaCode As String, _
                                                   ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditBaseVo)



#End Region



#Region "エクセル出力でのみ使用"


        ''' <summary>
        ''' 部品編集情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns>部品編集情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEdit(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBlockNo As String, _
                                 ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVoHelperExcel)

        ''' <summary>
        ''' 部品編集ベース情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns>部品編集情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditBase(ByVal shisakuEventCode As String, _
                                     ByVal shisakuBukaCode As String, _
                                     ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVoHelperExcel)


        ''' <summary>
        ''' 部課コードに該当するブロックNoを取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <returns>ブロック情報</returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlockNo(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String) As List(Of TShisakuSekkeiBlockVo)

        ''' <summary>
        ''' 部課略名を取得
        ''' </summary>
        ''' <param name="syainId">イベントコード</param>
        ''' <returns>部課略名</returns>
        ''' <remarks></remarks>
        Function FindByKaRyakuName(ByVal syainId As String) As String

        ''' <summary>
        ''' イベント情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Function FindByEvent(ByVal shisakuEventCode As String) As TShisakuEventVo


        ''' <summary>
        ''' 部課コードに該当する部品編集情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <returns>部品編集情報</returns>
        ''' <remarks></remarks>
        Function FindByAllBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVoHelperExcel)

        ''↓↓2014/09/18 酒井 ADD BEGIN
        Function FindByAllBlockBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String) As List(Of TShisakuBuhinEditVoHelperExcel)
        ''↑↑2014/09/18 酒井 ADD END

        ''' <summary>
        ''' 号車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <returns>号車情報</returns>
        ''' <remarks></remarks>
        Function FindByAllGousyaExcel(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of EditBlockExcelBlockInstlVoHelper)


        ''' <summary>
        ''' 試作ベース車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBase(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo)

        ''' <summary>
        ''' INSTL品番がどのテーブルに属するか返す
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>INSTL品番が属するテーブル名</returns>
        ''' <remarks></remarks>
        Function FindByBuhinTable(ByVal buhinNo As String) As String

        Function FindByBlockGroup(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String) As List(Of TShisakuBuhinEditVoHelperExcel)

        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByAllBuhinEdit(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVoHelperExcel)

        ''' <summary>
        ''' 号車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByAllGousyaExcel(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal shisakuBlockNoKaiteiNo As String) As List(Of EditBlockExcelBlockInstlVoHelper)

        ''' <summary>
        ''' 部品編集情報を取得する(ベース情報)
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByAllBuhinEditBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVoHelperExcel)

        ''' <summary>
        ''' 号車情報を取得(ベース情報)
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByAllGousyaExcelBase(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String) As List(Of EditBlockExcelBlockInstlVoHelper)

#End Region

    End Interface

End Namespace