Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 補用設計担当装備情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class THoyouSekkeiTantoSoubiDaoImpl : Inherits EBomDaoEachTable(Of THoyouSekkeiTantoSoubiVo)
        Implements THoyouSekkeiTantoSoubiDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of THoyouSekkeiTantoSoubiVo))
            Dim vo As New THoyouSekkeiTantoSoubiVo
            table.IsA(vo).PkField(vo.HoyouEventCode) _
                         .PkField(vo.HoyouBukaCode) _
                         .PkField(vo.HoyouTantoKey) _
                         .PkField(vo.HoyouTanto) _
                         .PkField(vo.HoyouSoubiHyoujiJun)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTantoKey">補用担当ＫＥＹ</param>
        ''' <param name="hoyouTanto">補用担当</param>
        ''' <param name="hoyouSoubiHyoujiJun">項目№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal hoyouEventCode As String, _
                                 ByVal hoyouBukaCode As String, _
                                 ByVal hoyouTantoKey As String, _
                                 ByVal hoyouTanto As String, _
                                 ByVal hoyouSoubiHyoujiJun As String) As THoyouSekkeiTantoSoubiVo Implements THoyouSekkeiTantoSoubiDao.FindByPk
            Return FindByPkMain(hoyouEventCode, _
                                hoyouBukaCode, _
                                hoyouTantoKey, _
                                hoyouTanto, _
                                hoyouSoubiHyoujiJun)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTantoKey">補用担当ＫＥＹ</param>
        ''' <param name="hoyouTanto">補用担当</param>
        ''' <param name="hoyouSoubiHyoujiJun">項目№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal hoyouEventCode As String, _
                                   ByVal hoyouBukaCode As String, _
                                   ByVal hoyouTantoKey As String, _
                                   ByVal hoyouTanto As String, _
                                   ByVal hoyouSoubiHyoujiJun As String) As Integer Implements THoyouSekkeiTantoSoubiDao.DeleteByPk
            Return DeleteByPkMain(hoyouEventCode, _
                                  hoyouBukaCode, _
                                  hoyouTantoKey, _
                                  hoyouTanto, _
                                  hoyouSoubiHyoujiJun)
        End Function

    End Class
End Namespace
