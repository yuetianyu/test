Imports ShisakuCommon.Db.EBom.Vo
Namespace Db.EBom.Dao.Impl
    Public Class AsPAPF14DaoImpl : Inherits EBomDaoEachTable(Of AsPAPF14Vo)
        Implements AsPAPF14Dao

        ''' <summary>
        ''' PrimaryKey を設定する
        ''' </summary>
        ''' <param name="table">テーブルに対応するVOのインスタンス</param>
        ''' <remarks></remarks>
        Protected Overrides Sub SettingPkField(ByVal table As PkTable(Of AsPAPF14Vo))
            Dim vo As New AsPAPF14Vo
            table.IsA(vo) _
            .PkField(vo.TORICD)
        End Sub

        Public Function DeleteByPk(ByVal TORICD As String) As Integer Implements AsPAPF14Dao.DeleteByPk
            Return DeleteByPkMain(TORICD)
        End Function

        Public Function FindByPk(ByVal TORICD As String) As Vo.AsPAPF14Vo Implements AsPAPF14Dao.FindByPk
            Return FindByPkMain(TORICD)
        End Function

    End Class
End Namespace