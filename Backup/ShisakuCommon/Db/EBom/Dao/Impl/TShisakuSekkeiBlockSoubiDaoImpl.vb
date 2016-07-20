Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作設計ブロック装備情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuSekkeiBlockSoubiDaoImpl : Inherits EBomDaoEachTable(Of TShisakuSekkeiBlockSoubiVo)
        Implements TShisakuSekkeiBlockSoubiDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuSekkeiBlockSoubiVo))
            Dim vo As New TShisakuSekkeiBlockSoubiVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
                         .PkField(vo.ShisakuBukaCode) _
                         .PkField(vo.ShisakuBlockNo) _
                         .PkField(vo.ShisakuBlockNoKaiteiNo) _
                         .PkField(vo.ShisakuSoubiHyoujiJun)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuSoubiHyoujiJun">項目№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBlockNo As String, _
                                 ByVal shisakuBlockNoKaiteiNo As String, _
                                 ByVal shisakuSoubiHyoujiJun As String) As TShisakuSekkeiBlockSoubiVo Implements TShisakuSekkeiBlockSoubiDao.FindByPk
            Return FindByPkMain(shisakuEventCode, _
                                shisakuBukaCode, _
                                shisakuBlockNo, _
                                shisakuBlockNoKaiteiNo, _
                                shisakuSoubiHyoujiJun)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuSoubiHyoujiJun">項目№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal shisakuBlockNoKaiteiNo As String, _
                                   ByVal shisakuSoubiHyoujiJun As String) As Integer Implements TShisakuSekkeiBlockSoubiDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, _
                                  shisakuBukaCode, _
                                  shisakuBlockNo, _
                                  shisakuBlockNoKaiteiNo, _
                                  shisakuSoubiHyoujiJun)
        End Function

    End Class
End Namespace
