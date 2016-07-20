Namespace XVLView.Logic

    ''' <summary>
    ''' 3Dビューワ専用Vo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ViewerImgeVo

        '開発符号
        Private _KaihatsuFugo As String
        '部品番号
        Private _BuhinNo As String
        '部品名称
        Private _BuhinName As String
        '補助名称
        Private _HojyoName As String
        '質量
        Private _ShitsuRyo As Double

        ''' <summary>
        ''' 開発符号
        ''' </summary>
        ''' <remarks></remarks>
        Public Property KaihatsuFugo() As String
            Get
                Return _KaihatsuFugo
            End Get
            Set(ByVal value As String)
                _KaihatsuFugo = value
            End Set
        End Property

        ''' <summary>
        ''' 部品番号
        ''' </summary>
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
        ''' 部品名称
        ''' </summary>
        ''' <remarks></remarks>
        Public Property BuhinName() As String
            Get
                Return _BuhinName
            End Get
            Set(ByVal value As String)
                _BuhinName = value
            End Set
        End Property

        ''' <summary>
        ''' 補助名称
        ''' </summary>
        ''' <remarks></remarks>
        Public Property HojyoName() As String
            Get
                Return _HojyoName
            End Get
            Set(ByVal value As String)
                _HojyoName = value
            End Set
        End Property

        ''' <summary>
        ''' 質量
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ShitsuRyo() As Double
            Get
                Return _ShitsuRyo
            End Get
            Set(ByVal value As Double)
                _ShitsuRyo = value
            End Set
        End Property

    End Class

End Namespace

