
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 補用部品編集情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class THoyouBuhinEditDaoImpl : Inherits EBomDaoEachTable(Of THoyouBuhinEditVo)
        Implements THoyouBuhinEditDao
        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.THoyouBuhinEditVo))
            Dim vo As New THoyouBuhinEditVo
            table.IsA(vo) _
            .PkField(vo.hoyouEventCode) _
            .PkField(vo.hoyouBukaCode) _
            .PkField(vo.hoyoutanto) _
            .PkField(vo.hoyoutantoKaiteiNo) _
            .PkField(vo.BuhinNoHyoujiJun)
        End Sub
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTanto">補用担当</param>
        ''' <param name="hoyouTantoKaiteiNo">補用担当改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal hoyouEventCode As String, _
                         ByVal hoyouBukaCode As String, _
                         ByVal hoyouTanto As String, _
                         ByVal hoyouTantoKaiteiNo As String, _
                         ByVal buhinNoHyoujiJun As Nullable(Of Int32)) As Vo.ThoyouBuhinEditVo Implements ThoyouBuhinEditDao.FindByPk
            Return FindByPkMain(hoyouEventCode, _
                                hoyouBukaCode, _
                                hoyouTanto, _
                                hoyouTantoKaiteiNo, _
                                buhinNoHyoujiJun)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <param name="hoyouBukaCode">補用部課コード</param>
        ''' <param name="hoyouTanto">補用担当</param>
        ''' <param name="hoyouTantoKaiteiNo">補用担当改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal hoyouEventCode As String, _
                         ByVal hoyouBukaCode As String, _
                         ByVal hoyouTanto As String, _
                         ByVal hoyouTantoKaiteiNo As String, _
                         ByVal buhinNoHyoujiJun As Nullable(Of Int32)) As Integer Implements THoyouBuhinEditDao.DeleteByPk
            Return DeleteByPkMain(hoyouEventCode, _
                                hoyouBukaCode, _
                                hoyouTanto, _
                                hoyouTantoKaiteiNo, _
                                buhinNoHyoujiJun)
        End Function
    End Class
End Namespace