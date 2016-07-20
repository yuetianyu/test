Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanSetteiBuhinMenu.Dao
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
        ''' 予算設定部品表変更履歴情報の削除
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuListCode">リストコード</param>
        ''' <remarks></remarks>
        Sub DeleteByYosanSetteiBuhinRireki(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

#End Region

    End Interface
End Namespace