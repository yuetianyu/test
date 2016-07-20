
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Class Block4UpdateBlockNoHyoujiJunVo : Inherits TShisakuSekkeiBlockVo

        Private _ShisakuBlockNoHyoujiJunNew As Nullable(Of Int32)
        Private _ShisakuBukaCodeNew As String

        Public Property ShisakuBlockNoHyoujiJunNew() As Nullable(Of Int32)
            Get
                Return _ShisakuBlockNoHyoujiJunNew
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuBlockNoHyoujiJunNew = value
            End Set
        End Property
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
