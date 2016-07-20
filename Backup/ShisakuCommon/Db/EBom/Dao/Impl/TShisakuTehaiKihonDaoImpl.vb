Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作手配帳情報（基本情報）テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuTehaiKihonDaoImpl : Inherits EBomDaoEachTable(Of TShisakuTehaiKihonVo)
        Implements TShisakuTehaiKihonDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuTehaiKihonVo))
            Dim vo As New TShisakuTehaiKihonVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
                         .PkField(vo.ShisakuListCode) _
                         .PkField(vo.ShisakuListCodeKaiteiNo) _
                         .PkField(vo.ShisakuBukaCode) _
                         .PkField(vo.ShisakuBlockNo) _
                         .PkField(vo.BuhinNoHyoujiJun)

        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As Vo.TShisakuTehaiKihonVo Implements TShisakuTehaiKihonDao.FindByPk
            Return FindByPkMain(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, shisakuBukaCode, shisakuBlockNo, buhinNoHyoujiJun)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, _
                          ByVal shisakuListCodeKaiteiNo As String, ByVal shisakuBukaCode As String, _
                          ByVal shisakuBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As Integer Implements TShisakuTehaiKihonDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, shisakuListCode, shisakuListCodeKaiteiNo, shisakuBukaCode, shisakuBlockNo, buhinNoHyoujiJun)
        End Function

    End Class
End Namespace
