Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書イベント別製作台数情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanSeisakuDaisuDaoImpl : Inherits EBomDaoEachTable(Of TYosanSeisakuDaisuVo)
        Implements TYosanSeisakuDaisuDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanSeisakuDaisuVo))
            Dim vo As New TYosanSeisakuDaisuVo
            table.IsA(vo).PkField(vo.YosanEventCode) _
                         .PkField(vo.ShisakuSyubetu) _
                         .PkField(vo.KoujiShireiNoHyojijunNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="ShisakuSyubetu">試作種別</param>
        ''' <param name="KoujiShireiNoHyojijunNo">工事指令№表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal yosanEventCode As String, ByVal ShisakuSyubetu As String, ByVal KoujiShireiNoHyojijunNo As Integer) As Vo.TYosanSeisakuDaisuVo Implements TYosanSeisakuDaisuDao.FindByPk
            Return FindByPkMain(yosanEventCode, ShisakuSyubetu, KoujiShireiNoHyojijunNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="ShisakuSyubetu">試作種別</param>
        ''' <param name="KoujiShireiNoHyojijunNo">工事指令№表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal yosanEventCode As String, ByVal ShisakuSyubetu As String, ByVal KoujiShireiNoHyojijunNo As Integer) As Integer Implements TYosanSeisakuDaisuDao.DeleteByPk
            Return DeleteByPkMain(yosanEventCode, ShisakuSyubetu, KoujiShireiNoHyojijunNo)
        End Function

    End Class
End Namespace
