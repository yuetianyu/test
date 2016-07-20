Namespace ShisakuBuhinEdit.KouseiBuhin.Dao.Vo

    ''' <summary>
    ''' ブロックリストVo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BlockListVo

        'ブロック番号
        Private _BlockNo As String

        'ブロック名称
        Private _BlockName As String

        ''' <summary>
        ''' ブロック番号
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BlockNo() As String
            Get
                Return _BlockNo
            End Get
            Set(ByVal value As String)
                _BlockNo = value
            End Set
        End Property

        ''' <summary>
        ''' ブロック名称
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BlockName() As String
            Get
                Return _BlockName
            End Get
            Set(ByVal value As String)
                _BlockName = value
            End Set
        End Property

    End Class

End Namespace

