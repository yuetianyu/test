Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書イベント別見通情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanEventMitoshiDaoImpl : Inherits EBomDaoEachTable(Of TYosanEventMitoshiVo)
        Implements TYosanEventMitoshiDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanEventMitoshiVo))
            Dim vo As New TYosanEventMitoshiVo
            table.IsA(vo).PkField(vo.YosanEventCode) _
                         .PkField(vo.YosanEventMitoshiYyyyMm) _
                         .PkField(vo.YosanEventMitoshiKbn)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="YosanEventCode">予算イベントコード</param>
        ''' <param name="YosanEventMitoshiYyyyMm">見通し計上年月</param>
        ''' <param name="YosanEventMitoshiKbn">見通し計上年月</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal YosanEventCode As String, ByVal YosanEventMitoshiYyyyMm As String, ByVal YosanEventMitoshiKbn As String) As Vo.TYosanEventMitoshiVo Implements TYosanEventMitoshiDao.FindByPk
            Return FindByPkMain(YosanEventCode, YosanEventMitoshiYyyyMm, YosanEventMitoshiKbn)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="YosanEventCode">予算イベントコード</param>
        ''' <param name="YosanEventMitoshiYyyyMm">見通し計上年月</param>
        ''' <param name="YosanEventMitoshiKbn">見通し計上年月</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal YosanEventCode As String, ByVal YosanEventMitoshiYyyyMm As String, ByVal YosanEventMitoshiKbn As String) As Integer Implements TYosanEventMitoshiDao.DeleteByPk
            Return DeleteByPkMain(YosanEventCode, YosanEventMitoshiYyyyMm, YosanEventMitoshiKbn)
        End Function

    End Class
End Namespace
