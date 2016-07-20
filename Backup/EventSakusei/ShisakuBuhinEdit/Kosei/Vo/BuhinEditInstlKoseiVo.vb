Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Kosei.Vo
    Public Class BuhinEditInstlKoseiVo : Inherits TShisakuBuhinEditVo
        Private _InsuSuryo As Integer

        ''' <summary>員数数量</summary>
        ''' <value>員数数量</value>
        ''' <returns>員数数量</returns>
        Public Property InsuSuryo() As Integer
            Get
                Return _InsuSuryo
            End Get
            Set(ByVal value As Integer)
                _InsuSuryo = value
            End Set
        End Property

    End Class
End Namespace