Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao
    ''' <summary>作り方項目マスタの簡単なCRUDを集めたDAO</summary>

    Public Interface MShisakuTsukurikataDao : Inherits DaoEachTable(Of MShisakuTsukurikataVo)

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="TsukurikataKbn">作り方区分</param>
        ''' <param name="TsukurikataNo">作り方項目No</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Function FindByPk(ByVal TsukurikataKbn As String, ByVal TsukurikataNo As Nullable(Of Int32)) As MShisakuTsukurikataVo

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="TsukurikataKbn">作り方区分</param>
        ''' <param name="TsukurikataNo">作り方項目No</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal TsukurikataKbn As String, ByVal TsukurikataNo As Nullable(Of Int32)) As Integer
    End Interface

End Namespace