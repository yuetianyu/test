Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 排他制御イベント情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TExclusiveControlEventDaoImpl : Inherits EBomDaoEachTable(Of TExclusiveControlEventVo)
        Implements TExclusiveControlEventDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TExclusiveControlEventVo))
            Dim vo As New TExclusiveControlEventVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
            .PkField(vo.EditMode)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="editMode">編集モード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, _
                                 ByVal editMode As String) As TExclusiveControlEventVo Implements TExclusiveControlEventDao.FindByPk
            Return FindByPkMain(shisakuEventCode, editMode)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="editMode">編集モード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, _
                                 ByVal editMode As String) As Integer Implements TExclusiveControlEventDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, editMode)
        End Function

    End Class
End Namespace
