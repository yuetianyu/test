
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 試作イベント完成車履歴情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuEventKanseiRirekiDaoImpl : Inherits EBomDaoEachTable(Of TShisakuEventKanseiRirekiVo)
        Implements TShisakuEventKanseiRirekiDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuEventKanseiRirekiVo))
            Dim vo As New TShisakuEventKanseiRirekiVo
            table.IsA(vo) _
            .PkField(vo.ShisakuEventCode) _
            .PkField(vo.HyojijunNo) _
            .PkField(vo.ColumnId) _
            .PkField(vo.ColumnName) _
            .PkField(vo.UpdateBi) _
            .PkField(vo.UpdateJikan)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <param name="columnId">カラムID</param>
        ''' <param name="columnName">カラム名称</param>
        ''' <param name="updateBi">更新日</param>
        ''' <param name="updateJikan">更新時間</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, _
                                 ByVal hyojijunNo As String, ByVal columnId As String, ByVal columnName As String, _
     ByVal updateBi As String, ByVal updateJikan As String) As Vo.TShisakuEventKanseiRirekiVo Implements TShisakuEventKanseiRirekiDao.FindByPk
            Return FindByPkMain(shisakuEventCode, hyojijunNo, columnId, columnName, updateBi, updateJikan)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, _
                                   ByVal hyojijunNo As String, ByVal columnId As String, ByVal columnName As String, _
       ByVal updateBi As String, ByVal updateJikan As String) As Integer Implements TShisakuEventKanseiRirekiDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, hyojijunNo, columnId, columnName, updateBi, updateJikan)
        End Function

    End Class
End Namespace