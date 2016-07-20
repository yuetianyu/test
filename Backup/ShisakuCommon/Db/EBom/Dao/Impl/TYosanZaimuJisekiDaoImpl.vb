Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書イベント別年月別財務実績情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanZaimuJisekiDaoImpl : Inherits EBomDaoEachTable(Of TYosanZaimuJisekiVo)
        Implements TYosanZaimuJisekiDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanZaimuJisekiVo))
            Dim vo As New TYosanZaimuJisekiVo
            table.IsA(vo).PkField(vo.YosanCode) _
                         .PkField(vo.YosanZaimuJisekiYyyyMm) _
                         .PkField(vo.YosanZaimuHireiKoteiKbn) _
                         .PkField(vo.YosanZaimuJisekiKbn)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanCode">予算コード</param>
        ''' <param name="YosanZaimuJisekiYyyyMm">財務実績計上年月</param>
        ''' <param name="YosanZaimuHireiKoteiKbn">比例費／固定費区分</param>
        ''' <param name="YosanZaimuJisekiKbn">財務実績区分</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal yosanCode As String, ByVal YosanZaimuJisekiYyyyMm As String, ByVal YosanZaimuHireiKoteiKbn As String, ByVal YosanZaimuJisekiKbn As String) As Vo.TYosanZaimuJisekiVo Implements TYosanZaimuJisekiDao.FindByPk
            Return FindByPkMain(yosanCode, YosanZaimuJisekiYyyyMm, YosanZaimuHireiKoteiKbn, YosanZaimuJisekiKbn)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanCode">予算コード</param>
        ''' <param name="YosanZaimuJisekiYyyyMm">財務実績計上年月</param>
        ''' <param name="YosanZaimuHireiKoteiKbn">比例費／固定費区分</param>
        ''' <param name="YosanZaimuJisekiKbn">財務実績区分</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal yosanCode As String, ByVal YosanZaimuJisekiYyyyMm As String, ByVal YosanZaimuHireiKoteiKbn As String, ByVal YosanZaimuJisekiKbn As String) As Integer Implements TYosanZaimuJisekiDao.DeleteByPk
            Return DeleteByPkMain(yosanCode, YosanZaimuJisekiYyyyMm, YosanZaimuHireiKoteiKbn, YosanZaimuJisekiKbn)
        End Function

    End Class
End Namespace
