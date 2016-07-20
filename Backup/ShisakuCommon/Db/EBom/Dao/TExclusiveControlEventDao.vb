Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 排他制御イベント情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TExclusiveControlEventDao : Inherits DaoEachTable(Of TExclusiveControlEventVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuEventCode As String, _
                          ByVal editMode As String) As TExclusiveControlEventVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
	    Function DeleteByPk(ByVal shisakuEventCode As String, _
                          ByVal editMode As String) As Integer
    End Interface
End Namespace