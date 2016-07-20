Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    ''' <summary>
    ''' 製品区分テーブルの簡単なCRUDを集めたDAO
    ''' </summary>
    ''' <remarks>実装クラス</remarks>
    Public Class AsSKMSDaoImpl : Inherits EBomDaoEachTable(Of AsSKMSVo)
        Implements AsSKMSDao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of AsSKMSVo))
            Dim vo As New AsSKMSVo
            table.IsA(vo) _
            .PkField(vo.BUKbn) _
            .PkField(vo.Snkm)
        End Sub

        Public Function DeleteByPk(ByVal BUKbn As String, _
                                   ByVal Snkm As String) As Integer Implements AsSKMSDao.DeleteByPk
            Return DeleteByPkMain(BUKbn, Snkm)
        End Function

        Public Function FindByPk(ByVal BUKbn As String, _
                                 ByVal Snkm As String) As Vo.AsSKMSVo Implements AsSKMSDao.FindByPk
            Return FindByPkMain(BUKbn, Snkm)
        End Function
    End Class
End Namespace