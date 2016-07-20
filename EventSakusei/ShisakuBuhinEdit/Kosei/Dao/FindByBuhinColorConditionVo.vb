Public Class FindByBuhinColorConditionVo

    Private _ShisakuEventCode As String
    Private _ShisakuBukaCode As String
    Private _ShisakuBlockNo As String
    Private _InstlHinban As String
    Private _BfBuhinNo As String
    Private _BuhinNo As String
    Private _kaihatsuFugo As String


    Public Sub New(ByVal shisakuEventCode As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal instlHinban As String, _
                                             ByVal bfBuhinNo As String, _
                                             ByVal kaihatsuFugo As String, _
                                             ByVal buhinNo As String)

        Me.ShisakuEventCode = shisakuEventCode
        Me.ShisakuBukaCode = shisakuBukaCode
        Me.ShisakuBlockNo = shisakuBlockNo
        Me.InstlHinban = instlHinban
        Me.BfBuhinNo = bfBuhinNo
        Me.kaihatsuFugo = kaihatsuFugo
        Me.BuhinNo = buhinNo

    End Sub
    Public Sub New(ByVal shisakuEventCode As String, _
                                             ByVal shisakuBukaCode As String, _
                                             ByVal shisakuBlockNo As String, _
                                             ByVal instlHinban As String, _
                                             ByVal bfBuhinNo As String, _
                                             ByVal buhinNo As String)

        Me.ShisakuEventCode = shisakuEventCode
        Me.ShisakuBukaCode = shisakuBukaCode
        Me.ShisakuBlockNo = shisakuBlockNo
        Me.InstlHinban = instlHinban
        Me.BfBuhinNo = bfBuhinNo
        Me.BuhinNo = buhinNo

    End Sub

    Public Sub New(ByVal shisakuEventCode As String, _
                                         ByVal shisakuBlockNo As String, _
                                         ByVal instlHinban As String, _
                                         ByVal bfBuhinNo As String, _
                                         ByVal buhinNo As String)

        Me.ShisakuEventCode = shisakuEventCode
        Me.ShisakuBlockNo = shisakuBlockNo
        Me.InstlHinban = instlHinban
        Me.BfBuhinNo = bfBuhinNo
        Me.BuhinNo = buhinNo

    End Sub

    Public Property ShisakuEventCode() As String
        Get
            Return _ShisakuEventCode
        End Get
        Set(ByVal value As String)
            _ShisakuEventCode = value
        End Set
    End Property

    Public Property ShisakuBukaCode() As String
        Get
            Return _ShisakuBukaCode
        End Get
        Set(ByVal value As String)
            _ShisakuBukaCode = value
        End Set
    End Property

    Public Property ShisakuBlockNo() As String
        Get
            Return _ShisakuBlockNo
        End Get
        Set(ByVal value As String)
            _ShisakuBlockNo = value
        End Set
    End Property

    Public Property InstlHinban() As String
        Get
            Return _InstlHinban
        End Get
        Set(ByVal value As String)
            _InstlHinban = value
        End Set
    End Property
    Public Property BfBuhinNo() As String
        Get
            Return _BfBuhinNo
        End Get
        Set(ByVal value As String)
            _BfBuhinNo = value
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


    Public Property KaihatsuFugo() As String
        Get
            Return _kaihatsuFugo
        End Get
        Set(ByVal value As String)
            _kaihatsuFugo = value
        End Set
    End Property
End Class
