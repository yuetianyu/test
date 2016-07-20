Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 試作ステータスマスターテーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class MShisakuStatusDaoImpl : Inherits EBomDaoEachTable(Of MShisakuStatusVo)
        Implements MShisakuStatusDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.MShisakuStatusVo))
            Dim vo As New MShisakuStatusVo
            table.IsA(vo).PkField(vo.ShisakuStatusCode)
        End Sub

        ''' <summary>
        ''' テーブル値の検索結果を返す
        ''' </summary>
        ''' <param name="shisakuStatusCode">試作ステータスコード</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal shisakuStatusCode As String) As Vo.MShisakuStatusVo Implements MShisakuStatusDao.FindByPk
            Return FindByPkMain(shisakuStatusCode)
        End Function

        ''' <summary>
        ''' 該当レコードを削除する
        ''' </summary>
        ''' <param name="shisakuStatusCode">試作ステータスコード</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal shisakuStatusCode As String) As Integer Implements MShisakuStatusDao.DeleteByPk
            Return DeleteByPkMain(shisakuStatusCode)
        End Function

    End Class
End Namespace

