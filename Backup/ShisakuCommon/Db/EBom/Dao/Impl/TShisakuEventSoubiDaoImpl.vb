Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作イベント特別装備テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuEventSoubiDaoImpl : Inherits EBomDaoEachTable(Of TShisakuEventSoubiVo)
        Implements TShisakuEventSoubiDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuEventSoubiVo))
            Dim vo As New TShisakuEventSoubiVo
            table.IsA(vo) _
            .PkField(vo.ShisakuEventCode) _
            .PkField(vo.HyojijunNo) _
            .PkField(vo.ShisakuSoubiHyoujiNo) _
            .PkField(vo.ShisakuSoubiKbn) _
            .PkField(vo.ShisakuRetuKoumokuCode)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <param name="shisakuTokubetuSoubiHyoujiNo">試作特別装備表示順</param>
        ''' <param name="shisakuTokubetuSoubiKbn">試作特別装備区分</param>
        ''' <param name="shisakuRetuKoumokuCode">列項目コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, ByVal hyojijunNo As Nullable(Of Int32) _
                         , ByVal shisakuTokubetuSoubiHyoujiNo As Nullable(Of Int32), ByVal shisakuTokubetuSoubiKbn As String _
                         , ByVal shisakuRetuKoumokuCode As String) As Vo.TShisakuEventSoubiVo Implements TShisakuEventSoubiDao.FindByPk
            Return FindByPkMain(shisakuEventCode, hyojijunNo, _
                                shisakuTokubetuSoubiHyoujiNo, shisakuTokubetuSoubiKbn, shisakuRetuKoumokuCode)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <param name="shisakuTokubetuSoubiHyoujiNo">試作特別装備表示順</param>
        ''' <param name="shisakuTokubetuSoubiKbn">試作特別装備区分</param>
        ''' <param name="shisakuRetuKoumokuCode">列項目コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, ByVal hyojijunNo As Nullable(Of Int32) _
                         , ByVal shisakuTokubetuSoubiHyoujiNo As Nullable(Of Int32), ByVal shisakuTokubetuSoubiKbn As String _
                         , ByVal shisakuRetuKoumokuCode As String) As Integer Implements TShisakuEventSoubiDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, hyojijunNo, _
                               shisakuTokubetuSoubiHyoujiNo, shisakuTokubetuSoubiKbn, shisakuRetuKoumokuCode)
        End Function

    End Class
End Namespace



