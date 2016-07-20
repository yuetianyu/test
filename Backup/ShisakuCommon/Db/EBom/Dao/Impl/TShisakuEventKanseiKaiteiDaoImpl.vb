
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 試作イベント完成車情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TShisakuEventKanseiKaiteiDaoImpl : Inherits EBomDaoEachTable(Of TShisakuEventKanseiKaiteiVo)
        Implements TShisakuEventKanseiKaiteiDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TShisakuEventKanseiKaiteiVo))
            Dim vo As New TShisakuEventKanseiKaiteiVo
            table.IsA(vo) _
            .PkField(vo.ShisakuEventCode) _
            .PkField(vo.HyojijunNo) 
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuEventCode As String, _
                                 ByVal hyojijunNo As String) As Vo.TShisakuEventKanseiKaiteiVo Implements TShisakuEventKanseiKaiteiDao.FindByPk
            Return FindByPkMain(shisakuEventCode, hyojijunNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="hyojijunNo">表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuEventCode As String, _
                                   ByVal hyojijunNo As String) As Integer Implements TShisakuEventKanseiKaiteiDao.DeleteByPk
            Return DeleteByPkMain(shisakuEventCode, hyojijunNo)
        End Function

    End Class
End Namespace