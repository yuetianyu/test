Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 排他管理予算書部品情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface TYosanExclusiveControlBuhinDao : Inherits DaoEachTable(Of TYosanExclusiveControlBuhinVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        '''<param name="buhinName">部品表名</param>
        ''' <param name="editUserId">編集者職番</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal yosanEventCode As String, ByVal buhinName As String, ByVal editUserId As String) As TYosanExclusiveControlBuhinVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="buhinName">部品表名</param>
        ''' <param name="editUserId">編集者職番</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal yosanEventCode As String, ByVal buhinName As String, ByVal editUserId As String) As Integer
    End Interface
End Namespace