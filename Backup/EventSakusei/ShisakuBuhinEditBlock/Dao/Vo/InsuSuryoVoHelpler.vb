Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Dao
    Public Class InsuSuryoVoHelpler : Inherits TShisakuBuhinEditInstlVo
        '' INSTL品番
        Private _InstlHinban As String
        '' INSTL品番区分
        Private _InstlHinbanKbn As String

        ''' <summary>INSTL品番</summary>
        ''' <value>INSTL品番</value>
        ''' <returns>INSTL品番</returns>
        Public Property InstlHinban() As String
            Get
                Return _InstlHinban
            End Get
            Set(ByVal value As String)
                _InstlHinban = value
            End Set
        End Property

        ''' <summary>INSTL品番区分</summary>
        ''' <value>INSTL品番区分</value>
        ''' <returns>INSTL品番区分</returns>
        Public Property InstlHinbanKbn() As String
            Get
                Return _InstlHinbanKbn
            End Get
            Set(ByVal value As String)
                _InstlHinbanKbn = value
            End Set
        End Property

    End Class
End Namespace


