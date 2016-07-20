Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作手配出図実績手入力情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuTehaiShutuzuJisekiInputDaoImpl : Inherits EBomDaoEachTable(Of TShisakuTehaiShutuzuJisekiInputVo)
        Implements TShisakuTehaiShutuzuJisekiInputDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table"></param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuTehaiShutuzuJisekiInputVo))
            Dim vo As New TShisakuTehaiShutuzuJisekiInputVo
            table.IsA(vo) _
            .PkField(vo.ShisakuEventCode) _
            .PkField(vo.ShisakuListCode) _
            .PkField(vo.ShisakuListCodeKaiteiNo) _
            .PkField(vo.ShisakuBlockNo) _
            .PkField(vo.BuhinNo) _
            .PkField(vo.ShutuzuJisekiKaiteiNo)

        End Sub

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="ShutuzuJisekiKaiteiNo">出図実績_改訂№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal ShisakuEventCode As String, _
                                   ByVal ShisakuListCode As String, _
                                   ByVal ShisakuListCodeKaiteiNo As String, _
                                   ByVal ShisakuBlockNo As String, _
                                   ByVal BuhinNo As String, _
                                   ByVal ShutuzuJisekiKaiteiNo As String _
                                   ) As Integer Implements TShisakuTehaiShutuzuJisekiInputDao.DeleteByPk
            Return DeleteByPkMain(ShisakuEventCode, ShisakuListCode, ShisakuListCodeKaiteiNo, ShisakuBlockNo, BuhinNo, ShutuzuJisekiKaiteiNo)
        End Function

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <param name="shisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <param name="buhinNo">部品番号</param>
        ''' <param name="ShutuzuJisekiKaiteiNo">出図実績_改訂№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal ShisakuEventCode As String, _
                                  ByVal ShisakuListCode As String, _
                                  ByVal ShisakuListCodeKaiteiNo As String, _
                                  ByVal ShisakuBlockNo As String, _
                                  ByVal BuhinNo As String, _
                                  ByVal ShutuzuJisekiKaiteiNo As String _
                                  ) As Vo.TShisakuTehaiShutuzuJisekiInputVo Implements TShisakuTehaiShutuzuJisekiInputDao.FindByPk
            Return FindByPkMain(ShisakuEventCode, ShisakuListCode, ShisakuListCodeKaiteiNo, ShisakuBlockNo, BuhinNo, ShutuzuJisekiKaiteiNo)
        End Function

    End Class
End Namespace
