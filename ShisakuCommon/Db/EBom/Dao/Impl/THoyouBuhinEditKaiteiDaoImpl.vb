
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 補用部品編集情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class THoyouBuhinEditKaiteiDaoImpl : Inherits EBomDaoEachTable(Of THoyouBuhinEditKaiteiVo)
        Implements THoyouBuhinEditKaiteiDao
        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.THoyouBuhinEditKaiteiVo))
            Dim vo As New THoyouBuhinEditKaiteiVo
            table.IsA(vo) _
            .PkField(vo.HoyouEventCode) _
            .PkField(vo.HoyouListCode) _
            .PkField(vo.HoyouListCodeKaiteiNo) _
            .PkField(vo.HoyouBukaCode) _
            .PkField(vo.HoyouTanto) _
            .PkField(vo.HoyouTantoKaiteiNo) _
            .PkField(vo.BuhinNoHyoujiJun) _
            .PkField(vo.Flag)
        End Sub
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="HoyouListCode">補用リストコード</param>
        ''' <param name="HoyouListCodeKaiteiNo">補用リストコード改訂№</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTanto">補用担当</param>
        ''' <param name="hoyouTantoKaiteiNo">補用担当改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="Flag">フラグ</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal hoyouEventCode As String, _
                          ByVal HoyouListCode As String, _
                          ByVal HoyouListCodeKaiteiNo As String, _
                         ByVal hoyouBukaCode As String, _
                         ByVal hoyouTanto As String, _
                         ByVal hoyouTantoKaiteiNo As String, _
                         ByVal buhinNoHyoujiJun As Nullable(Of Int32), _
                         ByVal Flag As String) As Vo.THoyouBuhinEditKaiteiVo Implements THoyouBuhinEditKaiteiDao.FindByPk
            Return FindByPkMain(hoyouEventCode, _
                                HoyouListCode, _
                                HoyouListCodeKaiteiNo, _
                                hoyouBukaCode, _
                                hoyouTanto, _
                                hoyouTantoKaiteiNo, _
                                buhinNoHyoujiJun, _
                                Flag)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="HoyouListCode">補用リストコード</param>
        ''' <param name="HoyouListCodeKaiteiNo">補用リストコード改訂№</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTanto">補用担当</param>
        ''' <param name="hoyouTantoKaiteiNo">補用担当改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="Flag">フラグ</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal hoyouEventCode As String, _
                          ByVal HoyouListCode As String, _
                          ByVal HoyouListCodeKaiteiNo As String, _
                         ByVal hoyouBukaCode As String, _
                         ByVal hoyouTanto As String, _
                         ByVal hoyouTantoKaiteiNo As String, _
                         ByVal buhinNoHyoujiJun As Nullable(Of Int32), _
                         ByVal Flag As String) As Integer Implements THoyouBuhinEditKaiteiDao.DeleteByPk
            Return DeleteByPkMain(hoyouEventCode, _
                                HoyouListCode, _
                                HoyouListCodeKaiteiNo, _
                                hoyouBukaCode, _
                                hoyouTanto, _
                                hoyouTantoKaiteiNo, _
                                buhinNoHyoujiJun, _
                                Flag)
        End Function
    End Class
End Namespace