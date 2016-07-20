
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Class ShisakuSekkeiBlock4EbomVo : Inherits TShisakuSekkeiBlockVo

        Private _ShisakuBukaCodeNew As String

        Public Property ShisakuBukaCodeNew() As String
            Get
                Return _ShisakuBukaCodeNew
            End Get
            Set(ByVal value As String)
                _ShisakuBukaCodeNew = value
            End Set
        End Property

    End Class
End Namespace
