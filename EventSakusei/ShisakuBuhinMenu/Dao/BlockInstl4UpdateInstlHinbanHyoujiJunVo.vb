
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Class BlockInstl4UpdateInstlHinbanHyoujiJunVo : Inherits TShisakuSekkeiBlockInstlVo

        Private _InstlHinbanHyoujiJunNew As Nullable(Of Int32)
        Private _ShisakuBukaCodeNew As String

        Public Property InstlHinbanHyoujiJunNew() As Nullable(Of Int32)
            Get
                Return _InstlHinbanHyoujiJunNew
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _InstlHinbanHyoujiJunNew = value
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

