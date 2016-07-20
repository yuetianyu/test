Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' AS400パーツプライスリストマスタの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface AsPARTSPDao : Inherits DaoEachTable(Of AsPARTSPVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="BUBA13">部品番号１３</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal BUBA13 As String) As AsPARTSPVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="BUBA13">部品番号１３</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal BUBA13 As String) As Integer

    End Interface
End Namespace