Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作イベント完成車情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuEventKanseiKaiteiDao : Inherits DaoEachTable(Of TShisakuEventKanseiKaiteiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal hyojijunNo As String) As TShisakuEventKanseiKaiteiVo
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal hyojijunNo As String) As Integer

    End Interface
End Namespace


