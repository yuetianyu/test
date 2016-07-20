Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書イベント情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanEventDaoImpl : Inherits EBomDaoEachTable(Of TYosanEventVo)
        Implements TYosanEventDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanEventVo))
            Dim vo As New TYosanEventVo
            table.IsA(vo).PkField(vo.YosanEventCode) 

        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal yosanEventCode As String) As Vo.TYosanEventVo Implements TYosanEventDao.FindByPk
            Return FindByPkMain(yosanEventCode)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal yosanEventCode As String) As Integer Implements TYosanEventDao.DeleteByPk
            Return DeleteByPkMain(yosanEventCode)
        End Function

    End Class
End Namespace
