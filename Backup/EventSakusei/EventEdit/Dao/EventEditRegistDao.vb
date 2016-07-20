Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db

Namespace EventEdit.Dao
    Public Interface EventEditRegistDao : Inherits DaoEachFeature

        ''' <summary>
        ''' 最後のイベントコードを取得する
        ''' </summary>
        ''' <param name="shisakuKaihatsuFugo">試作開発符号</param>
        ''' <param name="shisakuEventPhase">試作イベントフェーズ</param>
        ''' <param name="unitKbn">ユニット区分</param>
        ''' <returns>該当するイベント情報</returns>
        ''' <remarks></remarks>
        Function FindMaxShisakuEventCode(ByVal shisakuKaihatsuFugo As String, ByVal shisakuEventPhase As String, ByVal unitKbn As String) As TShisakuEventVo

    End Interface
End Namespace