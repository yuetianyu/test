Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書イベント別造り方情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanTukurikataDaoImpl : Inherits EBomDaoEachTable(Of TYosanTukurikataVo)
        Implements TYosanTukurikataDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanTukurikataVo))
            Dim vo As New TYosanTukurikataVo
            table.IsA(vo).PkField(vo.YosanEventCode) _
                         .PkField(vo.ShisakuSyubetu) _
                         .PkField(vo.UnitKbn) _
                         .PkField(vo.BuhinhyoName) _
                         .PkField(vo.PatternName) _
                         .PkField(vo.YosanTukurikataYyyyMm)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="ShisakuSyubetu">試作種別</param>
        ''' <param name="Unitkbn">ユニット区分</param>
        ''' <param name="BuhinhyoName">部品表名</param>
        ''' <param name="PatternName">パターン名</param>
        ''' <param name="YosanTukurikataYyyyMm">年月</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal yosanEventCode As String, ByVal ShisakuSyubetu As String, _
                                 ByVal Unitkbn As String, ByVal BuhinhyoName As String, _
                                 ByVal PatternName As String, ByVal YosanTukurikataYyyyMm As String) As Vo.TYosanTukurikataVo Implements TYosanTukurikataDao.FindByPk
            Return FindByPkMain(yosanEventCode, ShisakuSyubetu, BuhinhyoName, PatternName, YosanTukurikataYyyyMm)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="ShisakuSyubetu">試作種別</param>
        ''' <param name="UnitKbn">ユニット区分</param>
        ''' <param name="BuhinhyoName">部品表名</param>
        ''' <param name="PatternName">パターン名</param>
        ''' <param name="YosanTukurikataYyyyMm">年月</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal yosanEventCode As String, ByVal ShisakuSyubetu As String, _
                                   ByVal UnitKbn As String, ByVal BuhinhyoName As String, _
                                   ByVal PatternName As String, ByVal YosanTukurikataYyyyMm As String) As Integer Implements TYosanTukurikataDao.DeleteByPk
            Return DeleteByPkMain(yosanEventCode, ShisakuSyubetu, BuhinhyoName, PatternName, YosanTukurikataYyyyMm)
        End Function

    End Class
End Namespace
