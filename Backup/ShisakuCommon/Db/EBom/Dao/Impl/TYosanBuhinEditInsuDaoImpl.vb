Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書部品編集員数情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanBuhinEditInsuDaoImpl : Inherits EBomDaoEachTable(Of TYosanBuhinEditInsuVo)
        Implements TYosanBuhinEditInsuDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanBuhinEditInsuVo))
            Dim vo As New TYosanBuhinEditInsuVo
            table.IsA(vo).PkField(vo.YosanEventCode) _
                         .PkField(vo.BuhinhyoName) _
                         .PkField(vo.YosanBlockNo) _
                         .PkField(vo.BuhinNoHyoujiJun) _
                         .PkField(vo.PatternHyoujiJun)

        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="BuhinhyoName">部品表名</param>
        ''' <param name="yosanBlockNo">予算ブロック№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="PatternHyoujiJun">パターン表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal yosanEventCode As String, ByVal BuhinhyoName As String, _
                                 ByVal yosanBlockNo As String, ByVal buhinNoHyoujiJun As Int32, ByVal PatternHyoujiJun As Int32) As Vo.TYosanBuhinEditInsuVo Implements TYosanBuhinEditInsuDao.FindByPk
            Return FindByPkMain(yosanEventCode, BuhinhyoName, yosanBlockNo, buhinNoHyoujiJun, PatternHyoujiJun)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="BuhinhyoName">部品表名</param>
        ''' <param name="yosanBlockNo">予算ブロック№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <param name="PatternHyoujiJun">パターン表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal yosanEventCode As String, ByVal BuhinhyoName As String, _
                                   ByVal yosanBlockNo As String, ByVal buhinNoHyoujiJun As Int32, ByVal PatternHyoujiJun As Int32) As Integer Implements TYosanBuhinEditInsuDao.DeleteByPk
            Return DeleteByPkMain(yosanEventCode, BuhinhyoName, yosanBlockNo, buhinNoHyoujiJun, PatternHyoujiJun)
        End Function

    End Class
End Namespace
