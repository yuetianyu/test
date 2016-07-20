Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' AS400海外生産マスタの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface AsGKPSM10PDao : Inherits DaoEachTable(Of AsGKPSM10PVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="KOMZBA">工事指令№</param>
        ''' <param name="RNNO">連番</param>
        ''' <param name="LTBN">リスト分類</param>
        ''' <param name="TRCD">取引先コード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal KOMZBA As String, _
                          ByVal RNNO As Nullable(Of Int32), _
                          ByVal LTBN As String, _
                          ByVal TRCD As String) As AsGKPSM10PVo

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="KOMZBA">工事指令№</param>
        ''' <param name="RNNO">連番</param>
        ''' <param name="LTBN">リスト分類</param>
        ''' <param name="TRCD">取引先コード</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal KOMZBA As String, _
                            ByVal RNNO As Nullable(Of Int32), _
                            ByVal LTBN As String, _
                            ByVal TRCD As String) As Integer

    End Interface
End Namespace