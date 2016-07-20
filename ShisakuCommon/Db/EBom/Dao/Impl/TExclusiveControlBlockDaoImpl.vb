Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 排他制御ブロック情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TExclusiveControlBlockDaoImpl : Inherits EBomDaoEachTable(Of TExclusiveControlBlockVo)
        Implements TExclusiveControlBlockDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TExclusiveControlBlockVo))
            Dim vo As New TExclusiveControlBlockVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
                         .PkField(vo.ShisakuBukaCode) _
                         .PkField(vo.ShisakuBlockNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, _
                                 ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBlockNo As String) As TExclusiveControlBlockVo Implements TExclusiveControlBlockDao.FindByPk
            Return FindByPkMain(shisakuEventCode, _
                                shisakuBukaCode, _
                                shisakuBlockNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロック№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, _
                                   ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String) As Integer Implements TExclusiveControlBlockDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, _
                                  shisakuBukaCode, _
                                  shisakuBlockNo)
        End Function

    End Class
End Namespace
