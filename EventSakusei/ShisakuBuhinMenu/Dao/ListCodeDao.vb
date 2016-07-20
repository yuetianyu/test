Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Interface ListCodeDao : Inherits DaoEachFeature

        ''' <summary>
        ''' リストコードの取得
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <returns>リストコード一覧</returns>
        ''' <remarks></remarks>
        Function GetListCode(ByVal eventCode As String) As String

        ''' <summary>
        ''' リストコードの更新
        ''' </summary>
        ''' <param name="ListCode">リストコード</param>
        ''' <param name="Memo">メモ</param>
        ''' <remarks></remarks>
        Sub UpdateListCode(ByVal ListCode As String, ByVal Memo As String)

        ''' <summary>
        '''　発注実績の取得
        ''' </summary>
        ''' <param name="eventCode">イベントコード</param>
        ''' <param name="listCode">リストコード</param>
        ''' <returns>発注実績</returns>
        ''' <remarks></remarks>
        Function FindByHacchuJisseki(ByVal eventCode As String, ByVal listCode As String) As String

#Region "削除する"

        ''' <summary>
        ''' リストコードの削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByListCode(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

        ''' <summary>
        ''' 手配基本情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByTehaiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

        ''' <summary>
        ''' 手配号車情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByTehaiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

        ''' <summary>
        ''' 手配エラー情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByTehaiError(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

        ''' <summary>
        ''' 手配改訂ブロック情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByTehaiKaiteiBlock(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)
        '↓↓2014/10/02 酒井 ADD BEGIN
        Sub DeleteByTehaiKaiteiBlock2(ByVal shisakuEventCode As String)
        '↑↑2014/10/02 酒井 ADD END
        ''' <summary>
        ''' 手配訂正基本情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByTeiseiKihon(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

        ''' <summary>
        ''' 手配訂正号車情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByTehaiTeiseiGousya(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

        ''' <summary>
        ''' 基本情報TMPの削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Sub DeleteByKihonTmp(ByVal shisakuEventCode As String)

        ''' <summary>
        ''' 号車情報TMPの削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <remarks></remarks>
        Sub DeleteByGousyaTmp(ByVal shisakuEventCode As String)

        ''' <summary>
        ''' 部品編集改訂情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByBuhinEditKaitei(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

        ''' <summary>
        ''' 部品編集号車改訂情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByBuhinEditGousyaKaitei(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)


        ''' <summary>
        ''' 試作手配出図実績情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByTehaiShutuzuJiseki(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)
        ''' <summary>
        ''' 試作手配出図実績手入力情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByTehaiShutuzuJisekiInput(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)
        ''' <summary>
        ''' 試作手配出図織込情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByTehaiShutuzuOrikomi(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)
        ''' <summary>
        ''' 試作手配帳情報（号車グループ情報）の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByTehaiGousyaGroup(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

#End Region


    End Interface
End Namespace