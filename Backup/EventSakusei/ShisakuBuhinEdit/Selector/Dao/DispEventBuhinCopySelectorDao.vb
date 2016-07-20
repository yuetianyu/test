Imports ShisakuCommon.Db.EBom.Vo
Namespace ShisakuBuhinEdit.Selector.Dao
    Public Interface DispEventBuhinCopySelectorDao

        ''' <summary>
        ''' イベント情報を返す
        ''' </summary>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindEvent(ByVal blockNo As String, ByVal shisakuEventCode As String) As List(Of TShisakuEventVo)

        ''' <summary>
        ''' 設計ブロックINSTL情報を取得
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="KaiteiNo">改定No　2014/08/04 Ⅰ.11.改訂戻し機能 ｇ) (TES)施 追加</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindBySekkeiBlockInstl(ByVal shisakuEventCode As String, ByVal blockNo As String, Optional ByVal KaiteiNo As String = "") As List(Of TShisakuSekkeiBlockInstlVo)

        ''↓↓2014/09/23 酒井 ADD BEGIN
        Function FindByEventBase(ByVal shisakuEventCode As String) As List(Of TShisakuEventBaseVo)
        ''↑↑2014/09/23 酒井 ADD END  
        ''' <summary>
        ''' 開発符号を取得
        ''' </summary>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindEventkaiHatsuFugoList(ByVal blockNo As String, ByVal shisakuEventCode As String) As List(Of TShisakuEventVo)

        ''' <summary>
        ''' 開発符号を取得
        ''' </summary>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindBykaiHatsuFugo(ByVal blockNo As String, ByVal shisakuEventCode As String) As TShisakuEventVo

        ''' <summary>
        ''' イベント情報を返す
        ''' </summary>
        ''' <param name="blockNo">ブロックNo</param>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByEventKaiHatsuFugo(ByVal blockNo As String, ByVal shisakuEventCode As String, ByVal shisakuKaihatsuFugo As String) As List(Of TShisakuEventVo)

    End Interface

End Namespace
