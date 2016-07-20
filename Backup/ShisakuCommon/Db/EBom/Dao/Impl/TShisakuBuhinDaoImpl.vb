
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 試作部品情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuBuhinDaoImpl : Inherits EBomDaoEachTable(Of TShisakuBuhinVo)
        Implements TShisakuBuhinDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuBuhinVo))
            Dim vo As New TShisakuBuhinVo
            table.IsA(vo) _
            .PkField(vo.ShisakuEventCode) _
            .PkField(vo.ShisakuBukaCode) _
            .PkField(vo.ShisakuBlockNo) _
            .PkField(vo.ShisakuBlockNoKaiteiNo) _
            .PkField(vo.ShisakuGousya) _
            .PkField(vo.BuhinNo) _
            .PkField(vo.BuhinNoKbn) _
            .PkField(vo.BuhinNoKaiteiNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="buhinNoKbn">部品番号区分</param>
        ''' <param name="buhinNoKaiteiNo">部品番号改訂No.</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>       
        Public Function FindByPk(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBlockNo As String, _
                                 ByVal shisakuBlockNoKaiteiNo As String, _
                                 ByVal shisakuGousya As String, _
                                 ByVal buhinNo As String, _
                                 ByVal buhinNoKbn As String, _
                                 ByVal buhinNoKaiteiNo As String) As Vo.TShisakuBuhinVo Implements TShisakuBuhinDao.FindByPk
            Return FindByPkMain(shisakuEventCode, _
                                shisakuBukaCode, _
                                shisakuBlockNo, _
                                shisakuBlockNoKaiteiNo, _
                                shisakuGousya, _
                                buhinNo, _
                                buhinNoKbn, _
                                buhinNoKaiteiNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="shisakuBlockNoKaiteiNo">試作ブロック№改訂№</param>
        ''' <param name="shisakuGousya">試作号車</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="buhinNoKbn">部品番号区分</param>
        ''' <param name="buhinNoKaiteiNo">部品番号改訂No.</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal shisakuBlockNoKaiteiNo As String, _
                                   ByVal shisakuGousya As String, _
                                   ByVal buhinNo As String, _
                                   ByVal buhinNoKbn As String, _
                                   ByVal buhinNoKaiteiNo As String) As Integer Implements TShisakuBuhinDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, _
                                  shisakuBukaCode, _
                                  shisakuBlockNo, _
                                  shisakuBlockNoKaiteiNo, _
                                  shisakuGousya, _
                                  buhinNo, _
                                  buhinNoKbn, _
                                  buhinNoKaiteiNo)
        End Function

    End Class
End Namespace