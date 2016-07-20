Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Class Buhin4UpdateInstlHyoujiJunVo : Inherits TShisakuBuhinEditInstlVo
        Private _BuhinNo As String
        Private _BuhinNoKbn As String
        Private _NewInstlHinbanHyoujiJun As Nullable(Of Int32)

        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property

        Public Property BuhinNoKbn() As String
            Get
                Return _BuhinNoKbn
            End Get
            Set(ByVal value As String)
                _BuhinNoKbn = value
            End Set
        End Property

        Public Property NewInstlHinbanHyoujiJun() As Nullable(Of Int32)
            Get
                Return _NewInstlHinbanHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _NewInstlHinbanHyoujiJun = value
            End Set
        End Property
    End Class
End Namespace