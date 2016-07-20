Public Class DispKoseiVo
    Private _level As String
    Private _BuhinNo As String
    Private _BuhinNoOutput As String
    Private _BuhinName As String
    Private _InsuSuryo As String
    Private _ShukeiCode As String
    Private _SiaShukeiCode As String
    Private _MakerCode As String

    Public Property level() As String
        Get
            Return _level
        End Get
        Set(ByVal value As String)
            _level = value
        End Set
    End Property
    Public Property BuhinNo() As String
        Get
            Return _BuhinNo
        End Get
        Set(ByVal value As String)
            _BuhinNo = value
        End Set
    End Property

    Public Property BuhinNoOutput() As String
        Get
            Return _BuhinNoOutput
        End Get
        Set(ByVal value As String)
            _BuhinNoOutput = value
        End Set
    End Property
    Public Property BuhinName() As String
        Get
            Return _BuhinName
        End Get
        Set(ByVal value As String)
            _BuhinName = value
        End Set
    End Property

    Public Property InsuSuryo() As String
        Get
            Return _InsuSuryo
        End Get
        Set(ByVal value As String)
            _InsuSuryo = value
        End Set
    End Property

    Public Property ShukeiCode() As String
        Get
            Return _ShukeiCode
        End Get
        Set(ByVal value As String)
            _ShukeiCode = value
        End Set
    End Property

    Public Property SiaShukeiCode() As String
        Get
            Return _SiaShukeiCode
        End Get
        Set(ByVal value As String)
            _SiaShukeiCode = value
        End Set
    End Property

    Public Property MakerCode() As String
        Get
            Return _MakerCode
        End Get
        Set(ByVal value As String)
            _MakerCode = value
        End Set
    End Property

End Class
