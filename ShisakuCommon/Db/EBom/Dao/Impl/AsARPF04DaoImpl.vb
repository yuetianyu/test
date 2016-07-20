Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    Public Class AsARPF04DaoImpl : Inherits EBomDaoEachTable(Of AsARPF04Vo)
        Implements AsARPF04Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of AsARPF04Vo))
            Dim vo As New AsARPF04Vo
            table.IsA(vo) _
            .PkField(vo.BUKbn) _
            .PkField(vo.Kgba) _
            .PkField(vo.Kgkm) _
            .PkField(vo.Tikg)
        End Sub

        Public Function DeleteByPk(ByVal BUKbn As Char, _
                                   ByVal Kgba As String, _
                                   ByVal Kgkm As Char, _
                                   ByVal Tikg As String) As Integer Implements AsARPF04Dao.DeleteByPk
            Return DeleteByPkMain(BUKbn, Kgba, Kgkm, Tikg)
        End Function

        Public Function FindByPk(ByVal BUKbn As Char, _
                                 ByVal Kgba As String, _
                                 ByVal Kgkm As Char, _
                                 ByVal Tikg As String) As Vo.AsARPF04Vo Implements AsARPF04Dao.FindByPk
            Return FindByPkMain(BUKbn, Kgba, Kgkm, Tikg)
        End Function

    End Class
End Namespace