
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Class ShisakuBlockSekkeikaTmp4EbomVo : Inherits TShisakuBlockSekkeikaTmpVo

        Private _ShisakuBukaCode4Ebom As String

        Public Property ShisakuBukaCode4Ebom() As String
            Get
                Return _ShisakuBukaCode4Ebom
            End Get
            Set(ByVal value As String)
                _ShisakuBukaCode4Ebom = value
            End Set
        End Property

    End Class
End Namespace