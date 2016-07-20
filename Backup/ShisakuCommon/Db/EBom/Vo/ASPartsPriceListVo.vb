Namespace Db.EBom.Vo
    Public Class ASPartsPriceListVo
        Private _BuhinNo As String
        Private _KPrice As Nullable(Of Decimal)
        Private _SiaPrice As Nullable(Of Decimal)
        Private _Mark As String

        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property

        Public Property KPrice() As Nullable(Of Decimal)
            Get
                Return _KPrice
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _KPrice = value
            End Set
        End Property

        Public Property SiaPrice() As Nullable(Of Decimal)
            Get
                Return _SiaPrice
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _SiaPrice = value
            End Set
        End Property

        Public Property Mark() As String
            Get
                Return _Mark
            End Get
            Set(ByVal value As String)
                _Mark = value
            End Set
        End Property
    End Class

End Namespace
