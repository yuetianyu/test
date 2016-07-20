Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 補用イベント情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class THoyouEventDaoImpl : Inherits EBomDaoEachTable(Of THoyouEventVo)
        Implements THoyouEventDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of THoyouEventVo))
            Dim vo As New THoyouEventVo
            table.IsA(vo).PkField(vo.HoyouEventCode)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal hoyouEventCode As String) As THoyouEventVo Implements THoyouEventDao.FindByPk
            Return FindByPkMain(hoyouEventCode)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal hoyouEventCode As String) As Integer Implements THoyouEventDao.DeleteByPk
            Return DeleteByPkMain(hoyouEventCode)
        End Function

    End Class
End Namespace
