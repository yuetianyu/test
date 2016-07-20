Imports ShisakuCommon.Db
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.Kosei.Vo

Namespace TehaichoEdit.Dao
    Public Interface TehaichoHeaderDao
        'ヘッダー情報に必要なDao'

        ''' <summary>
        ''' リストコードを取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>該当するリストコード情報</returns>
        ''' <remarks></remarks>
        Function FindByListCode(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As TShisakuListcodeVo

        ''' <summary>
        ''' イベント情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>イベント情報</returns>
        ''' <remarks></remarks>
        Function FindByEvent(ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' 手配基本情報を取得する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <returns>該当する手配基本情報</returns>
        ''' <remarks></remarks>
        Function FindByTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String) As List(Of TShisakuTehaiKihonVo)


        ''' <summary>
        ''' 集計コード一覧を返す
        '''         ''' </summary>
        ''' <returns>該当する一覧</returns>
        ''' <remarks></remarks>
        Function FindByShukeiCodeInfo() As List(Of TSyukeiCodeVo)

        ''' <summary>
        ''' 試作手配帳情報（号車グループ情報）を返す
        ''' </summary>
        ''' <returns>該当するレコード</returns>
        ''' <remarks></remarks>
        Function FindGousyaGroupBy(ByVal shisakuGousya As String, _
                                   ByVal shisakuEventCode As String, _
                                   ByVal shisakuListCode As String, _
                                   ByVal shisakuListCodeKaiteiNo As String) As TShisakuTehaiGousyaGroupVo

        ''' <summary>
        ''' 試作手配出図実績情報を返す
        ''' </summary>
        ''' <returns>該当するレコード</returns>
        ''' <remarks></remarks>
        Function FindShutuzuJisekiBy(ByVal shisakuEventCode As String, _
                                     ByVal shisakuListCode As String, _
                                     ByVal shisakuListCodeKaiteiNo As String, _
                                     ByVal blockNo As String, _
                                     ByVal buhinNo As String, _
                                     ByVal descJun As Boolean) As List(Of TShisakuTehaiShutuzuJisekiVo)

        ''' <summary>
        ''' 出図実績の最新と織込みの差を返す。
        ''' </summary>
        ''' <returns>該当するレコード</returns>
        ''' <remarks></remarks>
        Function FindShutuzuJisekiOrikomiSaBy(ByVal shisakuEventCode As String, _
                                     ByVal shisakuListCode As String, _
                                     ByVal shisakuListCodeKaiteiNo As String, _
                                     ByVal shisakuBlockNo As String, _
                                     ByVal gyouId As String, _
                                     ByVal buhinNo As String) As TShisakuTehaiShutuzuOrikomiVo

        ''' <summary>
        ''' 試作手配出図実績情報を返す（＋改訂№）
        ''' </summary>
        ''' <returns>該当するレコード</returns>
        ''' <remarks></remarks>
        Function FindShutuzuJisekiKaiteiBy(ByVal shisakuEventCode As String, _
                                     ByVal shisakuListCode As String, _
                                     ByVal shisakuListCodeKaiteiNo As String, _
                                     ByVal blockNo As String, _
                                     ByVal buhinNo As String, _
                                     ByVal kaiteiNo As String) As TShisakuTehaiShutuzuJisekiVo

        ''' <summary>
        ''' 試作手配出図実績手入力情報を返す
        ''' </summary>
        ''' <returns>該当するレコード</returns>
        ''' <remarks></remarks>
        Function FindShutuzuJisekiInputBy(ByVal shisakuEventCode As String, _
                                     ByVal shisakuListCode As String, _
                                     ByVal shisakuListCodeKaiteiNo As String, _
                                     ByVal blockNo As String, _
                                     ByVal buhinNo As String, _
                                     ByVal kaiteiNo As String, _
                                     ByVal maxKaitei As Boolean) As TShisakuTehaiShutuzuJisekiInputVo

#Region "追加項目"

        ''' <summary>
        ''' 部品番号をキーに最新の図面情報を返す。
        ''' </summary>
        ''' <param name="buhinNo">部品番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByT_ZUMEN(ByVal buhinNo As String) As List(Of Rhac0533Vo)

        ''' <summary>
        ''' ユーザーIDをキーにサイト区分を返す。
        ''' </summary>
        ''' <param name="userId">ユーザーID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetSiteInfo(ByVal userId As String) As MUserVo

        ''' <summary>
        ''' 部品Noリストから設通Noリスト作成
        ''' </summary>
        ''' <param name="BuhinNoList">部品Noのリスト</param>
        ''' <returns></returns>
        Function FindByBuhin(ByVal BuhinNoList As List(Of String)) As List(Of Zspf10Vo)

#End Region

#Region "試作手配出図実績へのIO"
        ''' <summary>
        ''' 出図実績情報を取得する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByTShisakuTehaiShutuzuJisseki(ByVal ShisakuEventCode As String, _
                                                   ByVal ShisakuListCode As String, _
                                                   ByVal ShisakuListCodeKaiteiNo As String) As List(Of TShisakuTehaiShutuzuJisekiVo)
#End Region

    End Interface
End Namespace