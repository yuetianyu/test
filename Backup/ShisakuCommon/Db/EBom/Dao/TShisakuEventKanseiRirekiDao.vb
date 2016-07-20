Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作イベント完成車履歴情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TShisakuEventKanseiRirekiDao : Inherits DaoEachTable(Of TShisakuEventKanseiRirekiVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <param name="columnId">カラムID</param>
        ''' <param name="columnName">カラム名称</param>
        ''' <param name="updateBi">更新日</param>
        ''' <param name="updateJikan">更新時間</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal hyojijunNo As String, ByVal columnId As String, ByVal columnName As String, _
     ByVal updateBi As String, ByVal updateJikan As String) As TShisakuEventKanseiRirekiVo
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <param name="columnId">カラムID</param>
        ''' <param name="columnName">カラム名称</param>
        ''' <param name="updateBi">更新日</param>
        ''' <param name="updateJikan">更新時間</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuEventCode As String, _
                            ByVal hyojijunNo As String,byval columnId as string, byval columnName as string, _
			    byval updateBi as string, byval updateJikan as string) As Integer

    End Interface
End Namespace


