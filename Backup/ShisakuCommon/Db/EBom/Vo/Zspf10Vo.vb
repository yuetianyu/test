Namespace Db.EBom.Vo
    Public Class Zspf10Vo

        ''' <summary>部品No.</summary>
        Private _Gzzcp As String
        ''' <summary>タイトル図面</summary>
        Private _Tzzmbap As String
        ''' <summary>設通No</summary>
        Private _Dhstba As String
        ''' <summary>受領登録日</summary>
        Private _Kxjrdt As String
        ''' <summary>図面改訂</summary>
        Private _Tzkdbap As String
        ''' <summary>開発符号</summary>
        Private _Prfgp As String
        ''' <summary>改訂理由(件名)</summary>
        Private _Kdri As String
        ''' <summary>設通シリーズ</summary>
        Private _Stsr As String

        ''' <summary>部品No.</summary>
        ''' <value>部品No.</value>
        ''' <returns>部品No.</returns>
        Public Property Gzzcp() As String
            Get
                Return _Gzzcp
            End Get
            Set(ByVal value As String)
                _Gzzcp = value
            End Set
        End Property
        ''' <summary>タイトル図面</summary>
        ''' <value>タイトル図面</value>
        ''' <returns>タイトル図面</returns>
        Public Property Tzzmbap() As String
            Get
                Return _Tzzmbap
            End Get
            Set(ByVal value As String)
                _Tzzmbap = value
            End Set
        End Property

        ''' <summary>受領登録日</summary>
        ''' <value>受領登録日</value>
        ''' <returns>受領登録日</returns>
        Public Property Kxjrdt() As String
            Get
                Return _Kxjrdt
            End Get
            Set(ByVal value As String)
                _Kxjrdt = value
            End Set
        End Property
        ''' <summary>設通No.</summary>
        ''' <value>設通No.</value>
        ''' <returns>設通No.</returns>
        Public Property Dhstba() As String
            Get
                Return _Dhstba
            End Get
            Set(ByVal value As String)
                _Dhstba = value
            End Set
        End Property
        ''' <summary>図面改訂No.</summary>
        ''' <value>図面改訂No.</value>
        ''' <returns>図面改訂No.</returns>
        Public Property Tzkdbap() As String
            Get
                Return _Tzkdbap
            End Get
            Set(ByVal value As String)
                _Tzkdbap = value
            End Set
        End Property

        ''' <summary>開発符号</summary>
        ''' <value>開発符号</value>
        ''' <returns>開発符号</returns>
        Public Property Prfgp() As String
            Get
                Return _Prfgp
            End Get
            Set(ByVal value As String)
                _Prfgp = value
            End Set
        End Property

        ''' <summary>改訂理由(件名)</summary>
        ''' <value>改訂理由(件名)</value>
        ''' <returns>改訂理由(件名)</returns>
        Public Property Kdri() As String
            Get
                Return _Kdri
            End Get
            Set(ByVal value As String)
                _Kdri = value
            End Set
        End Property

        ''' <summary>設通シリーズ</summary>
        ''' <value>設通シリーズ</value>
        ''' <returns>設通シリーズ</returns>
        Public Property Stsr() As String
            Get
                Return _Stsr
            End Get
            Set(ByVal value As String)
                _Stsr = value
            End Set
        End Property



    End Class
End Namespace