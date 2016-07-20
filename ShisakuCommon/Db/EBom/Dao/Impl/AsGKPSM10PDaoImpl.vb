Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>AS400海外生産マスタの簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class AsGKPSM10PDaoImpl : Inherits EBomDaoEachTable(Of AsGKPSM10PVo)
        Implements AsGKPSM10PDao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.AsGKPSM10PVo))
            Dim vo As New AsGKPSM10PVo
            table.IsA(vo).PkField(vo.KOMZBA) _
            .PkField(vo.RNNO) _
            .PkField(vo.LTBN) _
            .PkField(vo.TRCD)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="KOMZBA">工事指令№</param>
        ''' <param name="RNNO">連番</param>
        ''' <param name="LTBN">リスト分類</param>
        ''' <param name="TRCD">取引先コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal KOMZBA As String, _
                          ByVal RNNO As Nullable(Of Int32), _
                          ByVal LTBN As String, _
                          ByVal TRCD As String) As AsGKPSM10PVo Implements AsGKPSM10PDao.FindByPk

            Return FindByPkMain(KOMZBA, RNNO, LTBN, TRCD)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="KOMZBA">工事指令№</param>
        ''' <param name="RNNO">連番</param>
        ''' <param name="LTBN">リスト分類</param>
        ''' <param name="TRCD">取引先コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal KOMZBA As String, _
                          ByVal RNNO As Nullable(Of Int32), _
                          ByVal LTBN As String, _
                          ByVal TRCD As String) As Integer Implements AsGKPSM10PDao.DeleteByPk
            Return DeleteByPkMain(KOMZBA, RNNO, LTBN, TRCD)
        End Function

    End Class
End Namespace

