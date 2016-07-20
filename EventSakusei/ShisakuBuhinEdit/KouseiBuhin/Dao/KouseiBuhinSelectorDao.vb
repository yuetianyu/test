Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.KouseiBuhin.Dao
Imports EventSakusei.ShisakuBuhinEdit.KouseiBuhin.Dao.Vo

Namespace KouseiBuhin.Dao
    Public Interface KouseiBuhinSelectorDao

        ''' <summary>
        ''' EBOMデータに構成が存在するかどうか取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <returns>True:存在する False:存在しない</returns>
        ''' <remarks></remarks>
        Function FindByDataEbom(ByVal ShisakuEventCode As String) As Boolean
        ''' <summary>
        ''' 試作側にイベントが存在する場合そのイベントコードを取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <returns>イベントコードのリスト</returns>
        ''' <remarks></remarks>
        Function FindByDataShisaku(ByVal ShisakuEventCode As String) As List(Of String)

        ''' <summary>
        ''' システム大区分を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>システム大区分のリスト</returns>
        ''' <remarks></remarks>
        Function GetBySystemDaiKbnDataEbom(ByVal kaihatsuFugo As String) As List(Of SystemKbnVo)

        ''' <summary>
        ''' システム区分を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>システム区分のリスト</returns>
        ''' <remarks></remarks>
        Function GetBySystemKbnDataEbom(ByVal kaihatsuFugo As String) As List(Of SystemKbnVo)

        ''' <summary>
        ''' ブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <returns>ブロック何のリスト</returns>
        ''' <remarks></remarks>
        Function GetByBlockDataEbom(ByVal kaihatsuFugo As String) As List(Of BlockListVo)

        ''' <summary>
        ''' システム大区分に紐づくブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="selectKbn">選択したシステム区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetBySystemDaiKbnToBlockEbom(ByVal kaihatsuFugo As String, ByVal selectKbn As List(Of SystemKbnVo)) As List(Of BlockListVo)

        ''' <summary>
        ''' システム区分に紐づくブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="selectKbn">選択したシステム区分</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetBySystemKbnToBlockEbom(ByVal kaihatsuFugo As String, ByVal selectKbn As List(Of SystemKbnVo)) As List(Of BlockListVo)

        '''' <summary>
        '''' ブロック№に紐づく部品番号を取得する
        '''' </summary>
        '''' <param name="kaihatsuFugo">開発符号</param>
        '''' <param name="selectBlock">選択したブロック№</param>
        '''' <returns></returns>
        '''' <remarks></remarks>
        'Function GetByBlockToBuhinEbom(ByVal kaihatsuFugo As String, ByVal selectBlock As List(Of BlockListVo)) As List(Of BuhinListVo)

        ''' <summary>
        ''' ブロックを取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="blockNo">ブロック№</param>
        ''' <returns>ブロック何のリスト</returns>
        ''' <remarks></remarks>
        Function FindByBlockData(ByVal kaihatsuFugo As String, ByVal blockNo As String) As List(Of BlockListVo)

        ''' <summary>
        ''' 部品番号に該当する部品名称と取引先コードを取得する
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByBuhinMaker(ByVal BuhinNo As String) As BuhinListVo

        ''' <summary>
        ''' 開発車機能ブロックのデータ取得(RHAC0080)
        ''' </summary>
        ''' <param name="aKaihatsuFugo">開発符号</param>
        ''' <param name="aBlockNo">ブロック番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByKaiteiNoRhac0080(ByVal aKaihatsuFugo As String, ByVal aBlockNo As String) As List(Of Rhac0080Vo)

        ''' <summary>
        ''' 部品(図面テーブル群)のデータ取得(RHAC0532)
        ''' </summary>
        ''' <param name="aBuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByKaiteiNoRhac0532(ByVal aBuhinNo As String) As List(Of Rhac0532Vo)

        ''' <summary>
        ''' 開発部品情報のデータ取得(RHAC0533)
        ''' </summary>
        ''' <param name="aBuhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByKaiteiNoRhac0533(ByVal aBuhinNo As String) As List(Of Rhac0533Vo)

        ''' <summary>
        ''' 号車情報を取得する（イベントコードから）
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakuevent">試作イベントコードｏｒ名称</param>
        ''' <returns>ブロック何のリスト</returns>
        ''' <remarks></remarks>
        Function GetByGousyaDataMbomFromC(ByVal kaihatsuFugo As String, ByVal shisakuEvent As String) As List(Of GousyaListVo)

        ''' <summary>
        ''' 号車情報を取得する（イベント名称から）
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakuevent">試作イベントコードｏｒ名称</param>
        ''' <returns>ブロック何のリスト</returns>
        ''' <remarks></remarks>
        Function GetByGousyaDataMbomFromN(ByVal kaihatsuFugo As String, ByVal shisakuEvent As String) As List(Of GousyaListVo)

        ''' <summary>
        ''' 試作ブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakuEvent">試作イベントコードｏｒ名称</param>
        ''' <returns>ブロック何のリスト</returns>
        ''' <remarks></remarks>
        Function GetByBlockDataMbomFromC(ByVal kaihatsuFugo As String, _
                                    ByVal shisakuEvent As String) As List(Of BlockListVo)

        ''' <summary>
        ''' 試作ブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakuEvent">試作イベントコードｏｒ名称</param>
        ''' <returns>ブロック何のリスト</returns>
        ''' <remarks></remarks>
        Function GetByBlockDataMbomFromN(ByVal kaihatsuFugo As String, _
                                    ByVal shisakuEvent As String) As List(Of BlockListVo)

        ''' <summary>
        ''' システム大区分に紐づくブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakuevent">試作イベントコードｏｒ名称</param>
        ''' <param name="selectGousya">選択した号車</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByGousyaToBlockMbomFromC(ByVal kaihatsuFugo As String, _
                                             ByVal shisakuEvent As String, _
                                             ByVal selectGousya As List(Of SystemKbnVo)) As List(Of BlockListVo)

        ''' <summary>
        ''' システム大区分に紐づくブロック№を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakuevent">試作イベントコードｏｒ名称</param>
        ''' <param name="selectGousya">選択した号車</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByGousyaToBlockMbomFromN(ByVal kaihatsuFugo As String, _
                                             ByVal shisakuEvent As String, _
                                             ByVal selectGousya As List(Of SystemKbnVo)) As List(Of BlockListVo)

        ''' <summary>
        ''' 開発符号、ブロック№に紐づく部品情報を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakuevent">試作イベントコードｏｒ名称</param>
        ''' <param name="selectBlock">選択したブロック№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByShisakuBuhinToSpreadFromC(ByVal kaihatsuFugo As String, _
                                                ByVal shisakuEvent As String, _
                                                ByVal selectBlock As List(Of BlockListVo)) As List(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper)

        ''' <summary>
        ''' 開発符号、ブロック№に紐づく部品情報を取得する
        ''' </summary>
        ''' <param name="kaihatsuFugo">開発符号</param>
        ''' <param name="shisakuevent">試作イベントコードｏｒ名称</param>
        ''' <param name="selectBlock">選択したブロック№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByShisakuBuhinToSpreadFromN(ByVal kaihatsuFugo As String, _
                                                ByVal shisakuEvent As String, _
                                                ByVal selectBlock As List(Of BlockListVo)) As List(Of TehaichoMenu.Vo.TShisakuBuhinEditVoSekkeiHelper)

        ''' <summary>
        ''' 試作号車に紐づく部品情報を設定
        ''' </summary>
        ''' <param name="shisakueventCode">試作イベントコード</param>
        ''' <param name="selectBlock">選択したブロック№</param>
        ''' <param name="selectBase">選択した号車№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByShisakuBuhinToSpreadBase(ByVal shisakuEventCode As String, _
                                                ByVal selectBlock As List(Of BlockListVo), _
                                                ByVal selectBase As List(Of SystemKbnVo)) As List(Of TShisakuBuhinEditInstlVo)
        '↓↓2014/10/09 酒井 ADD BEGIN
        Function GetInitialKaihatsuFugo(ByVal shisakuEventCode As String) As TShisakuEventBaseVo
        '↑↑2014/10/09 酒井 ADD END
        ''' <summary>
        ''' イベントコード、担当に紐づく完成車装備情報を取得する
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="sekkeika">設計課</param>
        ''' <param name="tantoKey">担当KEY</param>
        ''' <param name="tanto">担当</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByTHoyouSekkeiTantoSoubi(ByVal eventCode As String, _
                                             ByVal sekkeika As String, _
                                             ByVal tantoKey As String, _
                                             ByVal tanto As String) As List(Of THoyouSekkeiTantoSoubiVo)

    End Interface
End Namespace