Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    Public Class TYosanBuhinSelectDaoImpl : Inherits EBomDaoEachTable(Of TYosanBuhinSelectVo)
        Implements TYosanBuhinSelectDao


        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table"></param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.TYosanBuhinSelectVo))
            Dim vo As New TYosanBuhinSelectVo
            table.IsA(vo) _
            .PkField(vo.YosanEventCode) _
            .PkField(vo.BuhinhyoName)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="YosanEventCode">予算イベントコード</param>
        ''' <param name="BuhinhyoName">部品表名</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal YosanEventCode As Object, ByVal BuhinhyoName As Object) As Integer Implements TYosanBuhinSelectDao.DeleteByPk
            Return DeleteByPkMain(YosanEventCode, BuhinhyoName)
        End Function
        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="YosanEventCode">予算イベントコード</param>
        ''' <param name="BuhinhyoName">部品表名</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal YosanEventCode As Object, ByVal BuhinhyoName As Object) As Vo.TYosanBuhinSelectVo Implements TYosanBuhinSelectDao.FindByPk
            Return FindByPkMain(YosanEventCode, BuhinhyoName)
        End Function
    End Class
End Namespace


