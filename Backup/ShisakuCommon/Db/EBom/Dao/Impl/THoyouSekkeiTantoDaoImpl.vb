Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 補用設計担当情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class THoyouSekkeiTantoDaoImpl : Inherits EBomDaoEachTable(Of THoyouSekkeiTantoVo)
        Implements THoyouSekkeiTantoDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of THoyouSekkeiTantoVo))
            Dim vo As New THoyouSekkeiTantoVo
            table.IsA(vo).PkField(vo.hoyouEventCode) _
                         .PkField(vo.hoyouBukaCode) _
                         .PkField(vo.hoyouTanto) _
                         .PkField(vo.hoyouTantoKaiteiNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTanto">補用担当</param>
        ''' <param name="hoyouTantoKaiteiNo">補用担当改訂№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal hoyouEventCode As String, _
                                 ByVal hoyouBukaCode As String, _
                                 ByVal hoyouTanto As String, _
                                 ByVal hoyouTantoKaiteiNo As String) As THoyouSekkeiTantoVo Implements THoyouSekkeiTantoDao.FindByPk
            Return FindByPkMain(hoyouEventCode, _
                                hoyouBukaCode, _
                                hoyouTanto, _
                                hoyouTantoKaiteiNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouBlockNo">補用担当</param>
        ''' <param name="hoyouBlockNoKaiteiNo">補用担当改訂№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal hoyouEventCode As String, _
                                   ByVal hoyouBukaCode As String, _
                                   ByVal hoyouBlockNo As String, _
                                   ByVal hoyouBlockNoKaiteiNo As String) As Integer Implements THoyouSekkeiTantoDao.DeleteByPk
            Return DeleteByPkMain(hoyouEventCode, _
                                  hoyouBukaCode, _
                                  hoyouBlockNo, _
                                  hoyouBlockNoKaiteiNo)
        End Function

    End Class
End Namespace
