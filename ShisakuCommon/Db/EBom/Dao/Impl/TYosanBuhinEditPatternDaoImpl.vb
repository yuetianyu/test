Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書部品編集員数情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanBuhinEditPatternDaoImpl : Inherits EBomDaoEachTable(Of TYosanBuhinEditPatternVo)
        Implements TYosanBuhinEditPatternDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanBuhinEditPatternVo))
            Dim vo As New TYosanBuhinEditPatternVo
            table.IsA(vo).PkField(vo.YosanEventCode) _
                         .PkField(vo.BuhinhyoName) _
                         .PkField(vo.PatternHyoujiJun)

        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="BuhinhyoName">部品表名</param>
        ''' <param name="PatternHyoujiJun">パターン表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal yosanEventCode As String, ByVal BuhinhyoName As String, ByVal PatternHyoujiJun As Int32) As Vo.TYosanBuhinEditPatternVo Implements TYosanBuhinEditPatternDao.FindByPk
            Return FindByPkMain(yosanEventCode, BuhinhyoName, PatternHyoujiJun)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="BuhinhyoName">部品表名</param>
        ''' <param name="PatternHyoujiJun">パターン表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal yosanEventCode As String, ByVal BuhinhyoName As String, ByVal PatternHyoujiJun As Int32) As Integer Implements TYosanBuhinEditPatternDao.DeleteByPk
            Return DeleteByPkMain(yosanEventCode, BuhinhyoName, PatternHyoujiJun)
        End Function

    End Class
End Namespace
