
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 試作部品編集情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuBuhinEditDaoImpl : Inherits EBomDaoEachTable(Of TShisakuBuhinEditVo)
        Implements TShisakuBuhinEditDao
        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuBuhinEditVo))
            Dim vo As New TShisakuBuhinEditVo
            table.IsA(vo) _
            .PkField(vo.ShisakuEventCode) _
            .PkField(vo.ShisakuBukaCode) _
            .PkField(vo.ShisakuBlockNo) _
            .PkField(vo.ShisakuBlockNoKaiteiNo) _
            .PkField(vo.BuhinNoHyoujiJun)
        End Sub
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, _
                         ByVal shisakuBukaCode As String, _
                         ByVal shisakuBlockNo As String, _
                         ByVal shisakuBlockNoKaiteiNo As String, _
                         ByVal buhinNoHyoujiJun As Nullable(Of Int32)) As Vo.TShisakuBuhinEditVo Implements TShisakuBuhinEditDao.FindByPk
            Return FindByPkMain(shisakuEventCode, _
                                shisakuBukaCode, _
                                shisakuBlockNo, _
                                shisakuBlockNoKaiteiNo, _
                                buhinNoHyoujiJun)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, _
                         ByVal shisakuBukaCode As String, _
                         ByVal shisakuBlockNo As String, _
                         ByVal shisakuBlockNoKaiteiNo As String, _
                         ByVal buhinNoHyoujiJun As Nullable(Of Int32)) As Integer Implements TShisakuBuhinEditDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, _
                                shisakuBukaCode, _
                                shisakuBlockNo, _
                                shisakuBlockNoKaiteiNo, _
                                buhinNoHyoujiJun)
        End Function
        ''' <summary>
        ''' 指定されたイベントコードがテーブルに存在するかチェック.
        ''' </summary>
        ''' <param name="shisakuEventCode">検索するイベントコード</param>
        ''' <returns>True:存在する False:存在しない</returns>
        ''' <remarks></remarks>
        Public Function ExistByEventCode(ByVal shisakuEventCode As String) As Boolean Implements TShisakuBuhinEditDao.ExistByEventCode
            Dim iArg As New TShisakuBuhinEditVo

            iArg.ShisakuEventCode = shisakuEventCode
            '検索
            Dim find As List(Of TShisakuBuhinEditVo) = FindBy(iArg)

            Return 0 < find.Count

        End Function
    End Class
End Namespace