Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 試作装備マスターテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class MShisakuSoubiDaoImpl : Inherits EBomDaoEachTable(Of MShisakuSoubiVo)
        Implements MShisakuSoubiDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.MShisakuSoubiVo))
            Dim vo As New MShisakuSoubiVo
            table.IsA(vo) _
            .PkField(vo.ShisakuSoubiKbn) _
            .PkField(vo.ShisakuRetuKoumokuCode)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <param name="shisakuRetuKoumokuCode">列項目コード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuSoubiKbn As String, _
                                  ByVal shisakuRetuKoumokuCode As String) As Vo.MShisakuSoubiVo Implements MShisakuSoubiDao.FindByPk
            Return FindByPkMain(shisakuSoubiKbn, shisakuRetuKoumokuCode)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <param name="shisakuRetuKoumokuCode">列項目コード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuSoubiKbn As String, _
                                    ByVal shisakuRetuKoumokuCode As String) As Integer Implements MShisakuSoubiDao.DeleteByPk
            Return DeleteByPkMain(shisakuSoubiKbn, shisakuRetuKoumokuCode)
        End Function
    End Class
End Namespace

