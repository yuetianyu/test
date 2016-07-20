Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Class BuhinEditVoEx : Inherits TShisakuBuhinEditVo
        '' INSTL元データ区分
        Private _InstlDataKbn As String

        ''' <summary>INSTL元データ区分</summary>
        ''' <value>INSTL元データ区分</value>
        ''' <returns>INSTL元データ区分</returns>
        Public Property InstlDataKbn() As String
            Get
                Return _InstlDataKbn
            End Get
            Set(ByVal value As String)
                _InstlDataKbn = value
            End Set
        End Property

    End Class
End Namespace
