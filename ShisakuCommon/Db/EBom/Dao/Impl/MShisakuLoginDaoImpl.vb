
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 試作メニューログインマスターテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>

    Public Class MShisakuLoginDaoImpl : Inherits EBomDaoEachTable(Of MShisakuLoginVo)
        Implements MShisakuLoginDao
        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.MShisakuLoginVo))
            Dim vo As New MShisakuLoginVo
            table.IsA(vo).PkField(vo.SekkeiShainNo)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="sekkeiShainNo">設計社員番号</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal sekkeiShainNo As String) As Vo.MShisakuLoginVo Implements MShisakuLoginDao.FindByPk
            Return FindByPkMain(sekkeiShainNo)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="sekkeiShainNo">設計社員番号</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal sekkeiShainNo As String) As Integer Implements MShisakuLoginDao.DeleteByPk
            Return DeleteByPkMain(sekkeiShainNo)
        End Function

    End Class
End Namespace

