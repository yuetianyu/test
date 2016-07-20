Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Class Buhin4UpdateBuhinHyoujiJunVo : Inherits TShisakuBuhinEditVo

        Private _BuhinNoHyoujiJunNew As Nullable(Of Int32)

        Public Property BuhinNoHyoujiJunNew() As Nullable(Of Int32)
            Get
                Return _BuhinNoHyoujiJunNew
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _BuhinNoHyoujiJunNew = value
            End Set
        End Property

    End Class
End Namespace
