Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作イベント特別装備履歴テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuEventSoubiRirekiDaoImpl : Inherits EBomDaoEachTable(Of TShisakuEventSoubiRirekiVo)
        Implements TShisakuEventSoubiRirekiDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuEventSoubiRirekiVo))
            Dim vo As New TShisakuEventSoubiRirekiVo
            table.IsA(vo) _
            .PkField(vo.ShisakuEventCode) _
            .PkField(vo.HyojijunNo) _
            .PkField(vo.ShisakuSoubiHyoujiNo) _
            .PkField(vo.ShisakuSoubiKbn) _
            .PkField(vo.UpdateBi) _
            .PkField(vo.UpdateJikan)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <param name="shisakuTokubetuSoubiHyoujiNo">試作特別装備表示順</param>
        ''' <param name="shisakuTokubetuSoubiKbn">試作特別装備区分</param>
        ''' <param name="updateBi">更新日</param>
        ''' <param name="updateJikan">更新時間</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, ByVal hyojijunNo As Nullable(Of Int32) _
                         , ByVal shisakuTokubetuSoubiHyoujiNo As Nullable(Of Int32), ByVal shisakuTokubetuSoubiKbn As String _
                         , ByVal updateBi As String, Byval updateJikan As String) As Vo.TShisakuEventSoubiRirekiVo Implements TShisakuEventSoubiRirekiDao.FindByPk
            Return FindByPkMain(shisakuEventCode, hyojijunNo, _
                                shisakuTokubetuSoubiHyoujiNo, shisakuTokubetuSoubiKbn,  updateBi, updateJikan)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <param name="shisakuTokubetuSoubiHyoujiNo">試作特別装備表示順</param>
        ''' <param name="shisakuTokubetuSoubiKbn">試作特別装備区分</param>
        ''' <param name="updateBi">更新日</param>
        ''' <param name="updateJikan">更新時間</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, ByVal hyojijunNo As Nullable(Of Int32) _
                         , ByVal shisakuTokubetuSoubiHyoujiNo As Nullable(Of Int32), ByVal shisakuTokubetuSoubiKbn As String _
                         , ByVal updateBi As String, ByVal updateJikan As String) As Integer Implements TShisakuEventSoubiRirekiDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, hyojijunNo, _
                               shisakuTokubetuSoubiHyoujiNo, shisakuTokubetuSoubiKbn, updateBi, updateJikan)
        End Function

    End Class
End Namespace



