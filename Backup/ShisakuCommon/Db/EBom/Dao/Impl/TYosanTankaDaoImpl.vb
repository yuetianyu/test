Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書イベント別年月別財務実績情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanTankaDaoImpl : Inherits EBomDaoEachTable(Of TYosanTankaVo)
        Implements TYosanTankaDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanTankaVo))
            Dim vo As New TYosanTankaVo
            table.IsA(vo).PkField(vo.KokunaiKaigaiKbn) _
                         .PkField(vo.YosanBuhinNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="kokunaiKaigaiKbn">国内海外区分</param>
        ''' <param name="yosanBuhinNo">部品番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal kokunaiKaigaiKbn As String, ByVal yosanBuhinNo As String) As Vo.TYosanTankaVo Implements TYosanTankaDao.FindByPk
            Return FindByPkMain(kokunaiKaigaiKbn, yosanBuhinNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="kokunaiKaigaiKbn">国内海外区分</param>
        ''' <param name="yosanBuhinNo">部品番号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal kokunaiKaigaiKbn As String, ByVal yosanBuhinNo As String) As Integer Implements TYosanTankaDao.DeleteByPk
            Return DeleteByPkMain(kokunaiKaigaiKbn, yosanBuhinNo)
        End Function

    End Class
End Namespace
