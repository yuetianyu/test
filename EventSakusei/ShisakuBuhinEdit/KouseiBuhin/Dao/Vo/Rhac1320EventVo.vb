Namespace ShisakuBuhinEdit.KouseiBuhin.Dao.Vo

    ''' <summary>
    ''' イベント情報(RHAC1320)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac1320EventVo

        'イベントID
        Private _EventId As String

        ''' <summary>
        ''' イベントID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property EventId() As String
            Get
                Return _EventId
            End Get
            Set(ByVal value As String)
                _EventId = value
            End Set
        End Property

    End Class

End Namespace
