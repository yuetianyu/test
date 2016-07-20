Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>の簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class MShisakuTsukurikataDaoImpl : Inherits EBomDaoEachTable(Of MShisakuTsukurikataVo)
        Implements MShisakuTsukurikataDao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.MShisakuTsukurikataVo))
            Dim vo As New MShisakuTsukurikataVo
            table.IsA(vo).PkField(vo.TsukurikataKbn) _
                         .PkField(vo.TsukurikataNo)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="TsukurikataKbn">作り方区分</param>
        ''' <param name="TsukurikataNo">作り方項目No</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal TsukurikataKbn As String, ByVal TsukurikataNo As Nullable(Of Int32)) As MShisakuTsukurikataVo Implements MShisakuTsukurikataDao.FindByPk
            Return FindByPkMain(TsukurikataKbn, TsukurikataNo)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="TsukurikataKbn">作り方区分</param>
        ''' <param name="TsukurikataNo">作り方項目No</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Function DeleteByPk(ByVal TsukurikataKbn As String, ByVal TsukurikataNo As Nullable(Of Int32)) As Integer Implements MShisakuTsukurikataDao.DeleteByPk
            Return DeleteByPkMain(TsukurikataKbn, TsukurikataNo)
        End Function

    End Class
End Namespace