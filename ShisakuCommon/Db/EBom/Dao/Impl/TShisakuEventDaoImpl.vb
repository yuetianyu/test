Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作イベント情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuEventDaoImpl : Inherits EBomDaoEachTable(Of TShisakuEventVo)
        Implements TShisakuEventDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuEventVo))
            Dim vo As New TShisakuEventVo
            table.IsA(vo).PkField(vo.ShisakuEventCode)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String) As TShisakuEventVo Implements TShisakuEventDao.FindByPk
            Return FindByPkMain(shisakuEventCode)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String) As Integer Implements TShisakuEventDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode)
        End Function

    End Class
End Namespace
