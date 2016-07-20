Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 試作イベントベース車展開情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuEventBaseDaoImpl : Inherits EBomDaoEachTable(Of TShisakuEventBaseVo)
        Implements TShisakuEventBaseDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TShisakuEventBaseVo))
            Dim vo As New TShisakuEventBaseVo
            table.IsA(vo).PkField(vo.ShisakuEventCode) _
                         .PkField(vo.HyojijunNo)
        End Sub
        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyoJiJun_No">表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, ByVal hyoJiJun_No As Integer) As TShisakuEventBaseVo Implements TShisakuEventBaseDao.FindByPk
            Return FindByPkMain(shisakuEventCode, _
                                hyoJiJun_No)
        End Function
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyoJiJun_No">表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, ByVal hyoJiJun_No As Integer) As Integer Implements TShisakuEventBaseDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, _
                                  hyoJiJun_No)
        End Function
    End Class
End Namespace