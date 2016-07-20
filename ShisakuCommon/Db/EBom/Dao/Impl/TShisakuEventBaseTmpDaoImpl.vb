Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作イベントベース車展開情報（ＴＭＰ）テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuEventBaseTmpDaoImpl : Inherits EBomDaoEachTable(Of TShisakuEventBaseTmpVo)
        Implements TShisakuEventBaseTmpDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuEventBaseTmpVo))
            Dim vo As New TShisakuEventBaseTmpVo
            table.IsA(vo).PkField(vo.shisakuBukaCode) _
                         .PkField(vo.shisakuBlockNo) _
                         .PkField(vo.ShisakuEventCode) _
                         .PkField(vo.HyojijunNo)
        End Sub
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyoJiJun_No">表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuBukaCode As String, _
                                 ByVal shisakuBlockNo As String, _
                                 ByVal shisakuEventCode As String, _
                                 ByVal hyoJiJun_No As Integer) As TShisakuEventBaseTmpVo Implements TShisakuEventBaseTmpDao.FindByPk
            Return FindByPkMain(shisakuBukaCode, _
                                shisakuBlockNo, _
                                shisakuEventCode, _
                                hyoJiJun_No)
        End Function
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuBukaCode">試作部課コード</param>
        ''' <param name="shisakuBlockNo">試作ブロックNo</param>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyoJiJun_No">表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuBukaCode As String, _
                                   ByVal shisakuBlockNo As String, _
                                   ByVal shisakuEventCode As String, _
                                   ByVal hyoJiJun_No As Integer) As Integer Implements TShisakuEventBaseTmpDao.DeleteByPk
            Return DeleteByPkMain(shisakuBukaCode, _
                                  shisakuBlockNo, _
                                  shisakuEventCode, _
                                  hyoJiJun_No)
        End Function
    End Class
End Namespace