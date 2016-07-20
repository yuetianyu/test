Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>AS400パーツプライスリストマスタの簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class AsKPSM10PDaoImpl : Inherits EBomDaoEachTable(Of AsKPSM10PVo)
        Implements AsKPSM10PDao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.AsKPSM10PVo))
            Dim vo As New AsKPSM10PVo
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
                          ByVal TRCD As String) As AsKPSM10PVo Implements AsKPSM10PDao.FindByPk

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
                          ByVal TRCD As String) As Integer Implements AsKPSM10PDao.DeleteByPk
            Return DeleteByPkMain(KOMZBA, RNNO, LTBN, TRCD)
        End Function

    End Class
End Namespace

