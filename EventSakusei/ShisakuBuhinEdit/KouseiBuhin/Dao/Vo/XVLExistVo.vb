Namespace ShisakuBuhinEdit.KouseiBuhin.Dao.Vo

    Public Class XVLExistVo

        '行位置
        Private _RowIndex As String

        'ブロック№
        Private _BlockNo As String

        '部品番号
        Private _BuhinNo As String

        'XVL有無
        Private _IsExistXVL As String

        ''' <summary>
        ''' 行位置
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property RowIndex() As Integer
            Get
                Return _RowIndex
            End Get
            Set(ByVal value As Integer)
                _RowIndex = value
            End Set
        End Property

        ''' <summary>
        ''' ブロック№
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
        ''' 部品番号
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property

        ''' <summary>
        ''' XVL有無
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IsExistXVL() As String
            Get
                Return _IsExistXVL
            End Get
            Set(ByVal value As String)
                _IsExistXVL = value
            End Set
        End Property

    End Class

End Namespace
