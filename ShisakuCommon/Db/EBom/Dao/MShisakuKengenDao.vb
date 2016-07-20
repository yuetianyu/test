Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao
    ''' <summary>
    ''' 試作システム権限マスターテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface MShisakuKengenDao : Inherits DaoEachTable(Of MShisakuKengenVo)
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuProgramId1">試作プログラムID </param>
        ''' <param name="shisakuKinoId1">試作機能ID01</param>
        ''' <param name="shisakuKinoId2">試作機能ID02</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal shisakuProgramId1 As String, _
                          ByVal shisakuKinoId1 As String, _
                          ByVal shisakuKinoId2 As String) As MShisakuKengenVo
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuProgramId1">試作プログラムID </param>
        ''' <param name="shisakuKinoId1">試作機能ID01</param>
        ''' <param name="shisakuKinoId2">試作機能ID02</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal shisakuProgramId1 As String, _
                            ByVal shisakuKinoId1 As String, _
                            ByVal shisakuKinoId2 As String) As Integer

    End Interface
End Namespace


