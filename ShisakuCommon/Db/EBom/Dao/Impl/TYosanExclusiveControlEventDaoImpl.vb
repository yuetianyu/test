Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 排他管理予算書イベント情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanExclusiveControlEventDaoImpl : Inherits EBomDaoEachTable(Of TYosanExclusiveControlEventVo)
        Implements TYosanExclusiveControlEventDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanExclusiveControlEventVo))
            Dim vo As New TYosanExclusiveControlEventVo
            table.IsA(vo).PkField(vo.YosanEventCode) _
                         .PkField(vo.EditUserId)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="editUserId">編集者職番</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal yosanEventCode As String, ByVal editUserId As String) As Vo.TYosanExclusiveControlEventVo Implements TYosanExclusiveControlEventDao.FindByPk
            Return FindByPkMain(yosanEventCode, editUserId)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="editUserId">編集者職番</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal yosanEventCode As String, ByVal editUserId As String) As Integer Implements TYosanExclusiveControlEventDao.DeleteByPk
            Return DeleteByPkMain(yosanEventCode, editUserId)
        End Function

    End Class
End Namespace
