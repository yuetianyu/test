Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書イベント別年月別財務実績情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuTehaiShutuzuOrikomiDaoImpl : Inherits EBomDaoEachTable(Of TShisakuTehaiShutuzuOrikomiVo)
        Implements TShisakuTehaiShutuzuOrikomiDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuTehaiShutuzuOrikomiVo))
            Dim vo As New TShisakuTehaiShutuzuOrikomiVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
                         .PkField(vo.ShisakuListCode) _
                         .PkField(vo.ShisakuListCodeKaiteiNo) _
                         .PkField(vo.ShisakuBlockNo) _
                         .PkField(vo.GyouId) _
                         .PkField(vo.BuhinNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="GyouId">行ID</param>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal ShisakuEventCode As String, _
                                 ByVal ShisakuListCode As String, _
                                 ByVal ShisakuListCodeKaiteiNo As String, _
                                 ByVal ShisakuBlockNo As String, _
                                 ByVal GyouId As String, _
                                 ByVal BuhinNo As String) _
                                 As Vo.TShisakuTehaiShutuzuOrikomiVo Implements TShisakuTehaiShutuzuOrikomiDao.FindByPk
            Return FindByPkMain(ShisakuEventCode, ShisakuListCode, ShisakuListCodeKaiteiNo, ShisakuBlockNo, GyouId, BuhinNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuListCode">試作リストコード</param>
        ''' <param name="ShisakuListCodeKaiteiNo">試作リストコード改訂№</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <param name="GyouId">行ID</param>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal ShisakuEventCode As String, _
                                  ByVal ShisakuListCode As String, _
                                  ByVal ShisakuListCodeKaiteiNo As String, _
                                  ByVal ShisakuBlockNo As String, _
                                 ByVal GyouId As String, _
                                  ByVal BuhinNo As String) _
                                  As Integer Implements TShisakuTehaiShutuzuOrikomiDao.DeleteByPk
            Return DeleteByPkMain(ShisakuEventCode, ShisakuListCode, ShisakuListCodeKaiteiNo, ShisakuBlockNo, GyouId, BuhinNo)
        End Function

    End Class
End Namespace
