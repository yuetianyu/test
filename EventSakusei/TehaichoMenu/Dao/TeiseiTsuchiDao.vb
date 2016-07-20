Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu.Vo

Namespace TehaichoMenu.Dao

    Public Interface TeiseiTsuchiDao

#Region "取得する(FindBy)"

        ''' <summary>
        ''' 指定の改訂Noの手配基本情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <returns>指定の改訂Noの手配基本情報</returns>
        ''' <remarks></remarks>
        Function FindByBeforeTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiKihonVo)

        ''' <summary>
        ''' 最新の手配号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>最新の手配号車情報</returns>
        ''' <remarks></remarks>
        Function FindByTehaiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiGousyaVo)

        '''' <summary>
        '''' 指定の改訂Noの手配号車情報を取得する
        '''' </summary>
        '''' <param name="shisakuEventCode">イベントコード</param>
        '''' <param name="shisakuListCode">リストコード</param>
        '''' <returns>最新より１つ前の手配号車情報</returns>
        '''' <remarks></remarks>
        'Function FindByBeforeTehaiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiGousyaVo)

        ''↓↓2015/01/12 メタル改訂抽出修正) (TES)劉 CHG BEGIN
        ''' <summary>
        ''' 最新の手配情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="level">レベル</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <remarks></remarks>
        Function FindByNewTehaiKihon(ByVal shisakuEventCode As String, _
                                     ByVal shisakuListCode As String, _
                                     ByVal shisakuBukaCode As String, _
                                     ByVal shisakuBlockNo As String, _
                                     ByVal level As Integer, _
                                     ByVal buhinNo As String) As TShisakuTehaiKihonVo
        ''↑↑2015/01/12 メタル改訂抽出修正) (TES)劉 CHG END

        ''' <summary>
        ''' 最新の手配号車情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品No表示順</param>
        ''' <returns>該当する最新の手配号車情報</returns>
        ''' <remarks></remarks>
        Function FindByNewTehaiGousya(ByVal shisakuEventCode As String, _
                                      ByVal shisakuListCode As String, _
                                      ByVal shisakListCodeKaiteiNo As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String, _
                                      ByVal buhinNoHyoujiJun As Integer) As List(Of TShisakuTehaiGousyaVo)

        ''' <summary>
        ''' 指定の改訂Noの手配号車情報を取得する(追加用)
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品No表示順</param>
        ''' <returns>該当する最新の手配号車情報</returns>
        ''' <remarks></remarks>
        Function FindByBeforeGousya(ByVal shisakuEventCode As String, _
                                    ByVal shisakuListCode As String, _
                                    ByVal shisakuBukaCode As String, _
                                    ByVal shisakuBlockNo As String, _
                                    ByVal buhinNoHyoujiJun As String, _
                                    ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiGousyaVo)


        ''' <summary>
        ''' 全ての改訂Noを取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>該当する全てのリストコード</returns>
        ''' <remarks></remarks>
        Function FindByKaiteiNoList(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuListcodeVo)

        ''' <summary>
        ''' 最新の訂正手配基本情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>該当する全ての訂正基本情報</returns>
        ''' <remarks></remarks>
        Function FindByTeiseiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiTeiseiKihonVoHelper)

        ''' <summary>
        ''' 訂正手配号車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>該当する全ての訂正号車情報</returns>
        ''' <remarks></remarks>
        Function FindByTeiseiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiTeiseiGousyaVo)

        ''' <summary>
        ''' 指定の改訂Noの訂正手配号車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="kaiteiNo">改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当する全ての訂正号車情報</returns>
        ''' <remarks></remarks>
        Function FindByOldTeiseiGousya(ByVal shisakuEventCode As String, _
                                       ByVal shisakuListCode As String, _
                                       ByVal kaiteiNo As String, _
                                       ByVal shisakuBukaCode As String, _
                                       ByVal shisakuBlockNo As String, _
                                       ByVal buhinNoHyoujiJun As String) As TShisakuTehaiTeiseiGousyaVo


        '''' <summary>
        '''' AS/400の部品情報を取得
        '''' </summary>
        '''' <param name="shisakuEventCode">イベントコード</param>
        '''' <param name="shisakuListCode">リストコード</param>
        '''' <returns>該当する全ての部品情報</returns>
        '''' <remarks></remarks>
        'Function FindByAS400(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodekaiteiNo As String) As List(Of TeiseiTsuchiExcelVo)

        ''' <summary>
        ''' 試作ベース車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns>ベース車情報</returns>
        ''' <remarks></remarks>
        Function FindByBase(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo)

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

#End Region

#Region "更新する(UpdateBy)"

        ''' <summary>
        ''' リストコードの訂正通知抽出日と時間を更新する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub UpdateByTeiseiChusyutubi(ByVal shisakuEventCode As String, _
                               ByVal shisakuListCode As String)


#End Region

#Region "追加する(InsertBy)"

        ''' <summary>
        ''' 訂正基本情報を追加する(追加、手配記号変更、変更無し)
        ''' </summary>
        ''' <param name="newKihonVo">最新の手配基本情報</param>
        ''' <param name="flag">フラグ番号(追加:1, 削除:2, 変更前:3, 変更後:4, 変更(手配記号):5, 変更無し:ブランク)</param>
        ''' <remarks></remarks>
        Sub InsetByTeiseiKihon(ByVal newKihonVo As TShisakuTehaiKihonVo, ByVal flag As String)

        ''' <summary>
        ''' 訂正号車情報を追加する(追加、手配記号変更、変更無し)
        ''' </summary>
        ''' <param name="newGousyaListVo">最新の手配号車情報</param>
        ''' <param name="flag">フラグ番号</param>
        ''' <remarks></remarks>
        Sub InsetByTeiseiGousya(ByVal newGousyaListVo As List(Of TShisakuTehaiGousyaVo), ByVal flag As String)

        ''' <summary>
        ''' 訂正基本情報を追加する(削除,変更)
        ''' </summary>
        ''' <param name="shisakuEventCode">最新の試作イベントコード</param>
        ''' <param name="shisakuListCode">最新の試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">最新のリストコード改訂No</param>
        ''' <param name="beforeKihonVo">前回の手配基本情報</param>
        ''' <param name="flag">フラグ番号</param>
        ''' <remarks></remarks>
        Sub InsetByTeiseiKihonDel(ByVal shisakuEventCode As String, _
                                  ByVal shisakuListCode As String, _
                                  ByVal shisakuListCodeKaiteiNo As String, _
                                  ByVal beforeKihonVo As TShisakuTehaiKihonVo, ByVal flag As String)

        ''' <summary>
        ''' 訂正号車情報を追加する(削除,変更)
        ''' </summary>
        ''' <param name="shisakuEventCode">最新の試作イベントコード</param>
        ''' <param name="shisakuListCode">最新の試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">最新のリストコード改訂No</param>
        ''' <param name="beforeGousyaVo">前回の手配号車情報</param>
        ''' <param name="flag">フラグ番号</param>
        ''' <remarks></remarks>
        Sub InsetByTeiseiGousyaDel(ByVal shisakuEventCode As String, _
                                   ByVal shisakuListCode As String, _
                                   ByVal shisakuListCodeKaiteiNo As String, _
                                   ByVal beforeGousyaVo As TShisakuTehaiGousyaVo, ByVal flag As String)

        ''' <summary>
        ''' 員数増の際の号車追加
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="Flag">フラグ</param>
        ''' <param name="GyouId">行ID</param>
        ''' <param name="shisakuGousyaHyoujiJun">号車表示順</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="Insu">員数</param>
        ''' <param name="mNounyuShijibi">メタル納入指示日</param>
        ''' <param name="tNounyuShijibi">トリム納入指示日</param>
        ''' <remarks></remarks>
        Sub InsertByTeiseiGousyaAdd(ByVal shisakuEventCode As String, _
                                    ByVal shisakuListCode As String, _
                                    ByVal shisakuListCodeKaiteiNo As String, _
                                    ByVal shisakuBukaCode As String, _
                                    ByVal shisakuBlockNo As String, _
                                    ByVal buhinNoHyoujiJun As Integer, _
                                    ByVal Flag As String, _
                                    ByVal GyouId As String, _
                                    ByVal shisakuGousyaHyoujiJun As String, _
                                    ByVal shisakuGousya As String, _
                                    ByVal Insu As Integer, _
                                    ByVal mNounyuShijibi As Integer, _
                                    ByVal tNounyuShijibi As Integer)

#End Region

#Region "削除する(DeleteBy)"

        ''' <summary>
        ''' 前回の訂正基本情報を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByTeiseiKihon(ByVal shisakuEventCode As String, _
                                ByVal shisakuListCode As String, _
                                ByVal shisakuListCodeKaiteiNo As String)

        ''' <summary>
        ''' 前回の訂正号車情報を削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">リストコード改訂No</param>
        ''' <remarks></remarks>
        Sub DeleteByTeiseiGousya(ByVal shisakuEventCode As String, _
                                 ByVal shisakuListCode As String, _
                                 ByVal shisakuListCodeKaiteiNo As String)

#End Region

#Region "チェックする(CheckBy)"


        ''' <summary>
        ''' 手配記号を比較する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品No表示順</param>
        ''' <returns>あればOK</returns>
        ''' <remarks></remarks>
        Function CheckByTehaiKigou(ByVal shisakuEventCode As String, _
                               ByVal shisakuListCode As String, _
                               ByVal shisakuBukaCode As String, _
                               ByVal shisakuBlockNo As String, _
                               ByVal buhinNoHyoujiJun As String) As Boolean


        ''↓↓2015/01/12 メタル改訂抽出修正) (TES)劉 CHG BEGIN
        ''' <summary>
        ''' 直前の改訂と最新の改訂を比較して存在を確認する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂No</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="level">レベル</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns>あればTrue,なければFalse</returns>
        ''' <remarks></remarks>
        Function CheckByBefore(ByVal shisakuEventCode As String, _
                               ByVal shisakuListCode As String, _
                               ByVal shisakuBukaCode As String, _
                               ByVal shisakuBlockNo As String, _
                               ByVal level As Integer, _
                               ByVal buhinNo As String, _
                               ByVal shisakuListCodeKaiteiNo As String) As Boolean
        ''↑↑2015/01/12 メタル改訂抽出修正) (TES)劉 CHG END

#End Region

#Region "AS/400の取得"

        ''' <summary>
        ''' 試作部品表ファイル情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodekaiteiNo">リストコード改訂No</param>
        ''' <param name="ketugouNo">結合No</param>
        ''' <returns>試作部品表ファイル情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinFile(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, ByVal shisakuListCodekaiteiNo As String, ByVal ketugouNo As String) As AsPRPF02Vo

        ''' <summary>
        ''' 発注納入状況ファイル情報を取得
        ''' </summary>
        ''' <param name="KOBA">工事No</param>
        ''' <param name="KBBA">管理No(行ID)</param>
        ''' <param name="BNBA">ブロックNo</param>
        ''' <param name="KoujiShireiNo">工事指令No</param>
        ''' <returns>発注納入状況ファイル情報</returns>
        ''' <remarks></remarks>
        Function FindByORPF32(ByVal KOBA As String, ByVal KBBA As String, ByVal BNBA As String, ByVal KoujiShireiNo As String) As List(Of AsORPF32Vo)

        ''' <summary>
        ''' 新調達手配進捗ファイル情報を取得
        ''' </summary>
        ''' <param name="SGISBA">作業依頼書No</param>
        ''' <param name="KBBA">管理No</param>
        ''' <param name="CMBA">注文書No</param>
        ''' <param name="NOKM">納入区分</param>
        ''' <param name="HAYM">発注年月日</param>
        ''' <returns>新調達手配進捗ファイル情報</returns>
        ''' <remarks></remarks>
        Function FindByORPF60(ByVal SGISBA As String, ByVal KBBA As String, ByVal CMBA As String, _
                              ByVal NOKM As String, ByVal HAYM As String) As AsORPF60Vo

        ''' <summary>
        ''' 現調品発注控情報を取得
        ''' </summary>
        ''' <param name="LSCD">旧リストコード</param>
        ''' <param name="KBBA">管理No(行ID)</param>
        ''' <param name="GyouId">行ID</param>
        ''' <returns>現調品発注控情報</returns>
        ''' <remarks></remarks>
        Function FindByORPF57(ByVal LSCD As String, ByVal KBBA As String, ByVal GyouId As String) As List(Of AsORPF57Vo)

        ''' <summary>
        ''' 現調品手配進捗情報を取得
        ''' </summary>
        ''' <param name="GRNO">グループNo</param>
        ''' <param name="SRNO">シリアルNo</param>
        ''' <returns>現調品手配進捗情報</returns>
        ''' <remarks></remarks>
        Function FindByORPF61(ByVal GRNO As String, ByVal SRNO As String) As AsORPF61Vo


#End Region

    End Interface

End Namespace