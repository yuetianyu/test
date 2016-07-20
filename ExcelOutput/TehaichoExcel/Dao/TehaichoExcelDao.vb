Imports ShisakuCommon.Db.EBom.Vo
Imports ExcelOutput.TehaichoExcel.Vo

Namespace TehaichoExcel.Dao
    Public Interface TehaichoExcelDao

        ''' <summary>
        ''' 試作手配基本情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>試作手配基本情報</returns>
        ''' <remarks></remarks>
        Function FindByTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVoHelper)

        ''' <summary>
        ''' 試作手配号車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作手配号車情報</returns>
        ''' <remarks></remarks>
        Function FindByTehaiGousya(ByVal shisakuEventCode As String) As List(Of TShisakuTehaiGousyaVo)

        ''' <summary>
        ''' 試作イベント情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>試作イベント情報</returns>
        ''' <remarks></remarks>
        Function FindByEvent(ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' 試作ベース車情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>試作ベース車情報</returns>
        ''' <remarks></remarks>
        Function FindByEventBase(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuEventBaseVo)


        ''' <summary>
        ''' 試作設計ブロック情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <returns>ユニット区分</returns>
        ''' <remarks></remarks>
        Function FindByShisakuBlockNo(ByVal shisakuEventCode As String, _
                                      ByVal shisakuBukaCode As String, _
                                      ByVal shisakuBlockNo As String) As String

        Function FindByShisakuBlockNoForUnitKbn() As Hashtable




#Region "AS/400の取得"

        ''' <summary>
        ''' 試作部品表ファイル情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <param name="shisakuListCodekaiteiNo">リストコード改訂No</param>
        ''' <returns>試作部品表ファイル情報</returns>
        ''' <remarks></remarks>
        Function FindByBuhinFile(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                                 ByVal shisakuListCodekaiteiNo As String, ByVal ketugouNo As String) As AsPRPF02Vo

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

        ''' <summary>
        ''' 試作リストコード情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <returns>リストコード情報</returns>
        ''' <remarks></remarks>
        Function FindByListCode(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As TShisakuListcodeVo


#End Region


    End Interface
End Namespace