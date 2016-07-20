Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>AS400パーツプライスリストマスタの簡単なCRUDを集めたDAO</summary>
    ''' <remarks>実装クラス</remarks>
    Public Class AsPARTSPDaoImpl : Inherits EBomDaoEachTable(Of AsPARTSPVo)
        Implements AsPARTSPDao
        ''' <summary>PrimaryKey を設定する</summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of Vo.AsPARTSPVo))
            Dim vo As New AsPARTSPVo
            table.IsA(vo).PkField(vo.BUBA13)
        End Sub

        ''' <summary>テーブル値の検索結果を返す</summary>
        ''' <param name="BUBA13">部品番号１３</param>
        ''' <returns>該当レコード</returns>
        ''' <remarks></remarks>
        Public Function FindByPk(ByVal BUBA13 As String) As AsPARTSPVo Implements AsPARTSPDao.FindByPk
            Return FindByPkMain(BUBA13)
        End Function

        ''' <summary>該当レコードを削除する</summary>
        ''' <param name="BUBA13">部品番号１３</param>
        ''' <returns>削除件数</returns>
        ''' <remarks></remarks>
        Public Function DeleteByPk(ByVal BUBA13 As String) As Integer Implements AsPARTSPDao.DeleteByPk
            Return DeleteByPkMain(BUBA13)
        End Function

    End Class
End Namespace

