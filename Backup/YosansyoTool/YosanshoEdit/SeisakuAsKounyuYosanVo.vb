Namespace YosanshoEdit

    Public Class SeisakuAsKounyuYosanVo
        ''' <summary>工事指令№</summary>
        Private _KojishireiNo As String
        ''' <summary>ユニット区分</summary>
        Private _UnitKbn As String
        ''' <summary>年月</summary>
        Private _KounyuYosanYyyyMm As Integer
        ''' <summary>部品比例費</summary>
        Private _BuhinHireiHiCount As Nullable(Of Decimal)
        ''' <summary>部品固定費</summary>
        Private _BuhinKoteiHiCount As Nullable(Of Decimal)

        ''' <summary>工事指令№</summary>
        ''' <value>工事指令№</value>
        ''' <returns>工事指令№</returns>
        Public Property KojishireiNo() As String
            Get
                Return _KojishireiNo
            End Get
            Set(ByVal value As String)
                _KojishireiNo = value
            End Set
        End Property
        ''' <summary>ユニット区分</summary>
        ''' <value>ユニット区分</value>
        ''' <returns>ユニット区分</returns>
        Public Property UnitKbn() As String
            Get
                Return _UnitKbn
            End Get
            Set(ByVal value As String)
                _UnitKbn = value
            End Set
        End Property
        ''' <summary>年月</summary>
        ''' <value>年月</value>
        ''' <returns>年月</returns>
        Public Property KounyuYosanYyyyMm() As Integer
            Get
                Return _KounyuYosanYyyyMm
            End Get
            Set(ByVal value As Integer)
                _KounyuYosanYyyyMm = value
            End Set
        End Property
        ''' <summary>部品比例費</summary>
        ''' <value>部品比例費</value>
        ''' <returns>部品比例費</returns>
        Public Property BuhinHireiHiCount() As Nullable(Of Decimal)
            Get
                Return _BuhinHireiHiCount
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _BuhinHireiHiCount = value
            End Set
        End Property
        ''' <summary>部品固定費</summary>
        ''' <value>部品固定費</value>
        ''' <returns>部品固定費</returns>
        Public Property BuhinKoteiHiCount() As Nullable(Of Decimal)
            Get
                Return _BuhinKoteiHiCount
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _BuhinKoteiHiCount = value
            End Set
        End Property

    End Class

End Namespace
