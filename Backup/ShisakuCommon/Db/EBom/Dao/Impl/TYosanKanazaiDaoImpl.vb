Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書イベント別金材情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanKanazaiDaoImpl : Inherits EBomDaoEachTable(Of TYosanKanazaiVo)
        Implements TYosanKanazaiDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanKanazaiVo))
            Dim vo As New TYosanKanazaiVo
            table.IsA(vo).PkField(vo.YosanEventCode) _
                         .PkField(vo.KanazaiName) _
                         .PkField(vo.YosanTukurikataYyyyMm)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="KanazaiName">金材項目名</param>
        ''' <param name="YosanTukurikataYyyyMm">年月</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal yosanEventCode As String, ByVal KanazaiName As String, ByVal YosanTukurikataYyyyMm As String) As Vo.TYosanKanazaiVo Implements TYosanKanazaiDao.FindByPk
            Return FindByPkMain(yosanEventCode, KanazaiName, YosanTukurikataYyyyMm)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="KanazaiName">金材項目名</param>
        ''' <param name="YosanTukurikataYyyyMm">年月</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal yosanEventCode As String, ByVal KanazaiName As String, ByVal YosanTukurikataYyyyMm As String) As Integer Implements TYosanKanazaiDao.DeleteByPk
            Return DeleteByPkMain(yosanEventCode, KanazaiName, YosanTukurikataYyyyMm)
        End Function

    End Class
End Namespace
