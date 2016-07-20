Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Logic
    Public Class EventEditOptionVo : Inherits TShisakuEventSoubiVo

        '' 試作種別
        Private _ShisakuSyubetu As String
        '' 試作号車
        Private _ShisakuGousya As String

        ''' <summary>試作種別</summary>
        ''' <value>試作種別</value>
        ''' <returns>試作種別</returns>
        Public Property ShisakuSyubetu() As String
            Get
                Return _ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                _ShisakuSyubetu = value
            End Set
        End Property

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
End Namespace