
Namespace TehaichoEdit
    '工事指令書出力用のVOクラス'
    Public Class KojiShireiExcelVo
        ''担当者名
        Private _Tanto As String
        ''職番
        Private _TantoNo As String
        ''TEL
        Private _TantoTel As String
        ''依頼部署
        Private _IsIrai As Boolean
        ''指令部署
        Private _IsShire As Boolean
        ''件名
        Private _Kenmei As String
        ''車種
        Private _GoshaType As String
        ''目的
        Private _Mokuteki As String
        ''工事指令№
        Private _KojiNo As String
        ''記事
        Private _Kiji As String

        ''' <summary>担当者名</summary>
        ''' <value>担当者名</value>
        ''' <returns>担当者名</returns>
        Public Property Tanto() As String
            Get
                Return _Tanto
            End Get
            Set(ByVal value As String)
                _Tanto = value
            End Set
        End Property

        ''' <summary>職番</summary>
        ''' <value>職番</value>
        ''' <returns>職番</returns>
        Public Property TantoNo() As String
            Get
                Return _TantoNo
            End Get
            Set(ByVal value As String)
                _TantoNo = value
            End Set
        End Property

        ''' <summary>TEL</summary>
        ''' <value>TEL</value>
        ''' <returns>TEL</returns>
        Public Property TantoTel() As String
            Get
                Return _TantoTel
            End Get
            Set(ByVal value As String)
                _TantoTel = value
            End Set
        End Property

        ''' <summary>依頼部署</summary>
        ''' <value>依頼部署</value>
        ''' <returns>依頼部署</returns>
        Public Property IsIrai() As Boolean
            Get
                Return _IsIrai
            End Get
            Set(ByVal value As Boolean)
                _IsIrai = value
            End Set
        End Property

        ''' <summary>指令部署</summary>
        ''' <value>指令部署</value>
        ''' <returns>指令部署</returns>
        Public Property IsShire() As Boolean
            Get
                Return _IsShire
            End Get
            Set(ByVal value As Boolean)
                _IsShire = value
            End Set
        End Property

        ''' <summary>件名</summary>
        ''' <value>件名</value>
        ''' <returns>件名</returns>
        Public Property Kenmei() As String
            Get
                Return _Kenmei
            End Get
            Set(ByVal value As String)
                _Kenmei = value
            End Set
        End Property

        ''' <summary>車種</summary>
        ''' <value>車種</value>
        ''' <returns>車種</returns>
        Public Property GoshaType() As String
            Get
                Return _GoshaType
            End Get
            Set(ByVal value As String)
                _GoshaType = value
            End Set
        End Property

        ''' <summary>目的</summary>
        ''' <value>目的</value>
        ''' <returns>目的</returns>
        Public Property Mokuteki() As String
            Get
                Return _Mokuteki
            End Get
            Set(ByVal value As String)
                _Mokuteki = value
            End Set
        End Property

        ''' <summary>工事指令№</summary>
        ''' <value>工事指令№</value>
        ''' <returns>工事指令№</returns>
        Public Property KojiNo() As String
            Get
                Return _KojiNo
            End Get
            Set(ByVal value As String)
                _KojiNo = value
            End Set
        End Property

        ''' <summary>記事</summary>
        ''' <value>記事</value>
        ''' <returns>記事</returns>
        Public Property Kiji() As String
            Get
                Return _Kiji
            End Get
            Set(ByVal value As String)
                _Kiji = value
            End Set
        End Property
    End Class

End Namespace