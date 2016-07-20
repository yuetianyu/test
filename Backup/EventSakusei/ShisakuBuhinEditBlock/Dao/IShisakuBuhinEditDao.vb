Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util.LabelValue
Imports EventSakusei.EventEdit.Dao

Namespace ShisakuBuhinEditBlock.Dao
    Public Interface IShisakuBuhinEditDao : Inherits DaoEachFeature

        ''' <summary>
        ''' 部品編集情報と部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditAndInstl(ByVal shisakuEventCode As String, _
                                         ByVal shisakuBukaCode As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuBuhinEditVoHelper)

        ''' <summary>
        ''' 部品編集ベース情報と部品編集INSTLベース情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditAndInstlBase(ByVal shisakuEventCode As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String) As List(Of TShisakuBuhinEditVoHelper)

        ''' <summary>
        ''' 部品編集INSTL情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">ブロックNo改訂No</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditInstl(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal shisakuBlockNoKaiteiNo As String) As List(Of TShisakuSekkeiBlockInstlVo)
        ''' <summary>
        ''' 部品編集INSTLベース情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinEditInstlBase(ByVal shisakuEventCode As String, _
                                          ByVal shisakuBukaCode As String, _
                                          ByVal shisakuBlockNo As String) As List(Of TShisakuSekkeiBlockInstlVo)

        ''' <summary>
        ''' 部品編集情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBLockNo">試作ブロックNo</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロックNo改訂No</param>
        ''' <param name="isBase">ベースか？</param>
        ''' <returns>部品編集情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinEdit(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBLockNo As String, _
                                 ByVal shisakuBlockNoKaiteiNo As String, _
                                 ByVal isBase As Boolean) As List(Of TShisakuBuhinEditVo)

        ''↓↓2014/08/11 2集計コード R/Yのブロック間紐付け b) (TES)施 ADD BEGIN
        ''' <summary>
        ''' EBOM設計展開情報から、国内集計コードRまたは海外集計コードRの部品を取得
        ''' </summary>
        ''' <param name="shisakuEventCode"></param>
        ''' <returns></returns>
        ''' <remarks>  ''2014/08/11 2集計コード R/Yのブロック間紐付け b) (TES)施 ADD </remarks>
        Function FindCodeRBuhinByEventCode(ByVal shisakuEventCode As String) As List(Of TShisakuBuhinEditVo)

        ''' <summary>
        '''1)のLEVELが2以上の場合、T_SHISAKU_BUHIN_EDITからLEVELが直上となる、直近の部品を親部品番号とする	
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード </param>
        ''' <param name="ShisakuBukaCode">試作部課コード </param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns></returns>
        ''' <remarks>  ''2014/08/11 2集計コード R/Yのブロック間紐付け b) (TES)施 ADD </remarks>
        Function FindOyaBuhinByHighLevel(ByVal shisakuEventCode As String, _
                                           ByVal ShisakuBukaCode As String, _
                                           ByVal ShisakuBlockNo As String, _
                                           ByVal ShisakuBlockNoKaiteiNo As String, _
                                           ByVal BuhinNoHyoujiJun As Int32?, _
                                           ByVal Level As Integer) As List(Of TShisakuBuhinEditVo)
        ''↓↓2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD BEGIN
        'Function FindOyaBuhinByHighLevel(ByVal shisakuEventCode As String, _
        '                                   ByVal ShisakuBukaCode As String, _
        '                                   ByVal ShisakuBlockNo As String, _
        '                                   ByVal ShisakuBlockNoKaiteiNo As String, _
        '                                   ByVal BuhinNoHyoujiJun As Int32?) As List(Of TShisakuBuhinEditVo)
        ''↑↑2014/08/26 2集計コード R/Yのブロック間紐付け b) 酒井 ADD END

        ''' <summary>
        '''1)のLEVELが1の場合、T_SHISAKU_BUHIN_EDIT_INSTLから員数ゼロでないINSTL品番を親部品番号とする
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード </param>
        ''' <param name="ShisakuBukaCode">試作部課コード </param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="ShisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="BuhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns></returns>
        ''' <remarks>  ''2014/08/11 2集計コード R/Yのブロック間紐付け b) (TES)施 ADD </remarks>
        Function FindOyaBuhinByLevelOne(ByVal shisakuEventCode As String, _
                                           ByVal ShisakuBukaCode As String, _
                                           ByVal ShisakuBlockNo As String, _
                                           ByVal ShisakuBlockNoKaiteiNo As String, _
                                           ByVal BuhinNoHyoujiJun As Int32?) As List(Of TShisakuBuhinEditVo)
        ''' <summary>
        ''' 部品番号、かつ集計コードYの部品を取得
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindBuhinWithCodeYByInstl(ByVal buhinNo As String, ByVal eventCode As String) As List(Of TShisakuBuhinEditVo)
        ''' <summary>
        ''' 部品番号、かつ集計コードYの部品を取得
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindBuhinWithCodeYAndLevel(ByVal buhinNo As String, ByVal eventCode As String) As List(Of TShisakuBuhinEditVo)
        ''' <summary>
        ''' 子部品の備考と引き取りコード更新
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="bikou">更新備考</param>
        ''' <param name="makerCode">引き取りコード</param>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function UpdateKoBuhinBikou(ByVal buhinNo As String, ByVal bikou As String, ByVal makerCode As String, ByVal eventCode As String)
        ''↑↑2014/08/11 2集計コード R/Yのブロック間紐付け b) (TES)施 ADD END


    End Interface
End Namespace

