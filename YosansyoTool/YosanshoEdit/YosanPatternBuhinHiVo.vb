Namespace YosanshoEdit

    Public Class YosanPatternBuhinHiVo
        ''' <summary>予算イベントコード</summary>
        Private _YosanEventCode As String
        ''' <summary>ユニット区分</summary>
        Private _UnitKbn As String
        ''' <summary>パターン名</summary>
        Private _PatternName As String
        ''' <summary>部品費（量産）</summary>
        Private _YosanBuhinHiRyosan As Nullable(Of Decimal)
        ''' <summary>型費</summary>
        Private _YosanKataHi As Nullable(Of Decimal)
        ''' <summary>治具費</summary>
        Private _YosanJiguHi As Nullable(Of Decimal)

        ''' <summary>予算イベントコード</summary>
        ''' <value>予算イベントコード</value>
        ''' <returns>予算イベントコード</returns>
        Public Property YosanEventCode() As String
            Get
                Return _YosanEventCode
            End Get
            Set(ByVal value As String)
                _YosanEventCode = value
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
        ''' <summary>パターン名</summary>
        ''' <value>パターン名</value>
        ''' <returns>パターン名</returns>
        Public Property PatternName() As String
            Get
                Return _PatternName
            End Get
            Set(ByVal value As String)
                _PatternName = value
            End Set
        End Property
        ''' <summary>部品費（量産）</summary>
        ''' <value>部品費（量産）</value>
        ''' <returns>部品費（量産）</returns>
        Public Property YosanBuhinHiRyosan() As Nullable(Of Decimal)
            Get
                Return _YosanBuhinHiRyosan
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanBuhinHiRyosan = value
            End Set
        End Property
        ''' <summary>型費</summary>
        ''' <value>型費</value>
        ''' <returns>型費</returns>
        Public Property YosanKataHi() As Nullable(Of Decimal)
            Get
                Return _YosanKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanKataHi = value
            End Set
        End Property
        ''' <summary>治具費</summary>
        ''' <value>治具費</value>
        ''' <returns>治具費</returns>
        Public Property YosanJiguHi() As Nullable(Of Decimal)
            Get
                Return _YosanJiguHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanJiguHi = value
            End Set
        End Property

    End Class

End Namespace
