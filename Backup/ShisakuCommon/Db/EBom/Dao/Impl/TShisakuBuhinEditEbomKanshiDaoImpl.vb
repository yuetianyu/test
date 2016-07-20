
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 試作部品編集情報(EBOM設変)テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuBuhinEditEbomKanshiDaoImpl : Inherits EBomDaoEachTable(Of TShisakuBuhinEditEbomKanshiVo)
        Implements TShisakuBuhinEditEbomKanshiDao
        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuBuhinEditEbomKanshiVo))
            Dim vo As New TShisakuBuhinEditEbomKanshiVo
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
                         ByVal buhinNoHyoujiJun As Nullable(Of Int32)) As Vo.TShisakuBuhinEditEbomKanshiVo Implements TShisakuBuhinEditEbomKanshiDao.FindByPk
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
                         ByVal buhinNoHyoujiJun As Nullable(Of Int32)) As Integer Implements TShisakuBuhinEditEbomKanshiDao.DeleteByPk
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
        Public Function ExistByEventCode(ByVal shisakuEventCode As String) As Boolean Implements TShisakuBuhinEditEbomKanshiDao.ExistByEventCode
            Dim iArg As New TShisakuBuhinEditEbomKanshiVo

            iArg.ShisakuEventCode = shisakuEventCode
            '検索
            Dim find As List(Of TShisakuBuhinEditEbomKanshiVo) = FindBy(iArg)

            Return 0 < find.Count

        End Function
    End Class
End Namespace