Imports ShisakuCommon.Db.EBom.Vo

Public Class BuhinEditInstlVoHelper : Inherits TShisakuBuhinEditInstlVo
    Private _ShisakuGousya As String

    ''' <summary>試作号車</summary>
    ''' <value>試作号車</value>
    ''' <returns>試作号車</returns>
    Public Property ShisakuGousya() As String
        Get
            Return _ShisakuGousya
        End Get
        Set(ByVal value As String)
            _ShisakuGousya = value
        End Set
    End Property



End Class
