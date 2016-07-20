Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Logic
    Public Class NumberingShisakuRetuKoumokuCode
        Private ReadOnly mSoubiDao As MShisakuSoubiDao
        Private ReadOnly shisakuSoubiKbn As String
        Private current As Integer

        Public Sub New(ByVal mSoubiDao As MShisakuSoubiDao, ByVal shisakuSoubiKbn As String)
            Me.mSoubiDao = mSoubiDao
            Me.shisakuSoubiKbn = shisakuSoubiKbn
        End Sub
        Private _countAll As Nullable(Of Integer)
        Private Function CountAll() As Integer
            If _countAll Is Nothing Then
                Dim vo As New MShisakuSoubiVo
                vo.ShisakuSoubiKbn = shisakuSoubiKbn
                _countAll = mSoubiDao.CountBy(vo)
            End If
            Return _countAll
        End Function
        Public Function NextShisakuRetuKoumokuCode()
            current += 1
            Return CountAll() + current
        End Function
    End Class
End Namespace