Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書部品編集員数情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanBuhinEditInsuRirekiDaoImpl : Inherits EBomDaoEachTable(Of TYosanBuhinEditInsuRirekiVo)
        Implements TYosanBuhinEditInsuRirekiDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanBuhinEditInsuRirekiVo))
            Dim vo As New TYosanBuhinEditInsuRirekiVo
            table.IsA(vo).PkField(vo.YosanEventCode) _
                         .PkField(vo.UnitKbn) _
                         .PkField(vo.RegisterDate) _
                         .PkField(vo.YosanBlockNo) _
                         .PkField(vo.BuhinNoHyoujiJun) _
                         .PkField(vo.PatternHyoujiJun)

        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="UnitKbn">ユニット区分</param>
        ''' <param name="registerDate">登録日</param>
        ''' <param name="yosanBlockNo">予算ブロック№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="PatternHyoujiJun">パターン表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal yosanEventCode As String, ByVal UnitKbn As String, ByVal registerDate As String, _
                                 ByVal yosanBlockNo As String, ByVal buhinNoHyoujiJun As Int32, ByVal PatternHyoujiJun As Int32) As Vo.TYosanBuhinEditInsuRirekiVo Implements TYosanBuhinEditInsuRirekiDao.FindByPk
            Return FindByPkMain(yosanEventCode, UnitKbn, registerDate, yosanBlockNo, buhinNoHyoujiJun, PatternHyoujiJun)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="UnitKbn">ユニット区分</param>
        ''' <param name="registerDate">登録日</param>
        ''' <param name="yosanBlockNo">予算ブロック№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="PatternHyoujiJun">パターン表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal yosanEventCode As String, ByVal UnitKbn As String, ByVal registerDate As String, _
                                   ByVal yosanBlockNo As String, ByVal buhinNoHyoujiJun As Int32, ByVal PatternHyoujiJun As Int32) As Integer Implements TYosanBuhinEditInsuRirekiDao.DeleteByPk
            Return DeleteByPkMain(yosanEventCode, UnitKbn, registerDate, yosanBlockNo, buhinNoHyoujiJun, PatternHyoujiJun)
        End Function

    End Class
End Namespace
