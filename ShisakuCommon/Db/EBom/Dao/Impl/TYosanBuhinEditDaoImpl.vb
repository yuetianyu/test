Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl

    ''' <summary>
    ''' 予算書部品情報テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class TYosanBuhinEditDaoImpl : Inherits EBomDaoEachTable(Of TYosanBuhinEditVo)
        Implements TYosanBuhinEditDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of TYosanBuhinEditVo))
            Dim vo As New TYosanBuhinEditVo
            table.IsA(vo).PkField(vo.YosanEventCode) _
                         .PkField(vo.BuhinhyoName) _
                         .PkField(vo.YosanBlockNo) _
                         .PkField(vo.BuhinNoHyoujiJun)

        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="BuhinName">部品表名</param>
        ''' <param name="yosanBlockNo">予算ブロック№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal yosanEventCode As String, ByVal BuhinName As String, _
                                 ByVal yosanBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As Vo.TYosanBuhinEditVo Implements TYosanBuhinEditDao.FindByPk
            Return FindByPkMain(yosanEventCode, BuhinName, yosanBlockNo, buhinNoHyoujiJun)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="yosanEventCode">予算イベントコード</param>
        ''' <param name="BuhinName">部品表名</param>
        ''' <param name="yosanBlockNo">予算ブロック№</param>
        ''' <param name="buhinNoHyoujiJun">部品番号表示順</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal yosanEventCode As String, ByVal BuhinName As String, _
                                   ByVal yosanBlockNo As String, ByVal buhinNoHyoujiJun As Int32) As Integer Implements TYosanBuhinEditDao.DeleteByPk
            Return DeleteByPkMain(yosanEventCode, BuhinName, yosanBlockNo, buhinNoHyoujiJun)
        End Function

    End Class
End Namespace
