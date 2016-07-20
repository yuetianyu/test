Namespace ShisakuBuhinEdit.KouseiBuhin.Dao.Vo

    ''' <summary>
    ''' 号車リストVo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GousyaListVo

        '種別
        Private _ShisakuSyubetu As String
        '号車
        Private _ShisakuGousya As String

        ''' <summary>
        ''' 種別
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ShisakuSyubetu() As String
            Get
                Return _ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                _ShisakuSyubetu = value
            End Set
        End Property

        ''' <summary>
        ''' 号車
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
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

