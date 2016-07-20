Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作ブロック設計課テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuBlockSekkeikaTmpDaoImpl : Inherits EBomDaoEachTable(Of TShisakuBlockSekkeikaTmpVo)
        Implements TShisakuBlockSekkeikaTmpDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuBlockSekkeikaTmpVo))
            Dim vo As New TShisakuBlockSekkeikaTmpVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
                         .PkField(vo.ShisakuBlockNo)

        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal ShisakuEventCode As String, _
                                 ByVal ShisakuBlockNo As String) As TShisakuBlockSekkeikaTmpVo Implements TShisakuBlockSekkeikaTmpDao.FindByPk
            Return FindByPkMain(ShisakuEventCode, _
                                ShisakuBlockNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="ShisakuEventCode">試作イベントコード</param>
        ''' <param name="ShisakuBlockNo">試作ブロック№</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal ShisakuEventCode As String, _
                                   ByVal ShisakuBlockNo As String) As Integer Implements TShisakuBlockSekkeikaTmpDao.DeleteByPk
            Return DeleteByPkMain(ShisakuEventCode, _
                                  ShisakuBlockNo)
        End Function

    End Class
End Namespace
