'/*** 20140911 CHANGE START ***/
Namespace ShisakuBuhinMenu.Dao

    Public Class SekkeiBlockAlConditionVo

        'Public Sub New(ByVal shisakuEventCode As String, ByVal hyojiJunNo As Integer?)
        '    Me.ShisakuEventCode = shisakuEventCode
        '    Me.HyojiJunNo = hyojiJunNo
        'End Sub

        Public Sub New(ByVal shisakuEventCode As String, ByVal blockNo As String)
            Me.ShisakuEventCode = shisakuEventCode
            Me.BlockNo = blockNo
        End Sub

        Public Sub New(ByVal shisakuEventCode As String, ByVal hyojiJunNo As Integer?, ByVal unitKbn As String)
            Me.ShisakuEventCode = shisakuEventCode
            Me.HyojiJunNo = hyojiJunNo
            Me.UnitKbn = unitKbn
        End Sub

        Private _ShisakuEventCode As String
        Private _HyojiJunNo As Integer?
        Private _UnitKbn As String
        Private _BlockNo As String

        Public Property ShisakuEventCode()
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value)
                _ShisakuEventCode = value
            End Set
        End Property

        Public Property HyojiJunNo()
            Get
                Return _HyojiJunNo
            End Get
            Set(ByVal value)
                _HyojiJunNo = value
            End Set
        End Property

        Public Property UnitKbn()
            Get
                Return _UnitKbn
            End Get
            Set(ByVal value)
                _UnitKbn = value
            End Set
        End Property

        Public Property BlockNo()
            Get
                Return _BlockNo
            End Get
            Set(ByVal value)
                _BlockNo = value
            End Set
        End Property

    End Class
End Namespace
'/*** 20140911 CHANGE END ***/