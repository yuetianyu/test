Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.EventEdit.Dao


Namespace EventEdit.Logic
    Public Class NumberingShisakuRetuKoumokuCode
        Private ReadOnly mSoubiDao As EventEditMShisakuSoubiDao = New EventEditMShisakuSoubiDaoImpl
        Private ReadOnly shisakuSoubiKbn As String
        Private current As Integer

        Public Sub New(ByVal shisakuSoubiKbn As String)
            Me.shisakuSoubiKbn = shisakuSoubiKbn
        End Sub
        Private _countAll As Nullable(Of Integer)
        Private Function CountAll() As Integer
            If shisakuSoubiKbn Is Nothing Then
                Return 3
            End If

            If _countAll Is Nothing Then
                Dim vo As New MShisakuSoubiVo
                vo.ShisakuSoubiKbn = shisakuSoubiKbn
                _countAll = mSoubiDao.MaxShisakuRetuKoumokuCode(vo)
            End If
            Return _countAll
        End Function
        Public Function NextShisakuRetuKoumokuCode()
            current += 1
            Return CountAll() + current
        End Function
    End Class
End Namespace