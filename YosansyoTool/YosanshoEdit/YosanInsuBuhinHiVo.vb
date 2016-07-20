Namespace YosanshoEdit

    Public Class YosanInsuBuhinHiVo
        ''' <summary>部品表名</summary>
        Private _BuhinhyoName As String
        ''' <summary>パターン名</summary>
        Private _PatternName As String
        ''' <summary>比例費／固定費区分</summary>
        Private _HireiKoteiKbn As String
        ''' <summary>部品費</summary>
        Private _YosanBuhinHi As Nullable(Of Decimal)

        ''' <summary>部品表名</summary>
        ''' <value>部品表名</value>
        ''' <returns>部品表名</returns>
        Public Property BuhinhyoName() As String
            Get
                Return _BuhinhyoName
            End Get
            Set(ByVal value As String)
                _BuhinhyoName = value
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
        ''' <summary>比例費／固定費区分</summary>
        ''' <value>比例費／固定費区分</value>
        ''' <returns>比例費／固定費区分</returns>
        Public Property HireiKoteiKbn() As String
            Get
                Return _HireiKoteiKbn
            End Get
            Set(ByVal value As String)
                _HireiKoteiKbn = value
            End Set
        End Property
        ''' <summary>部品費</summary>
        ''' <value>部品費</value>
        ''' <returns>部品費</returns>
        Public Property YosanBuhinHi() As Nullable(Of Decimal)
            Get
                Return _YosanBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanBuhinHi = value
            End Set
        End Property
  

    End Class

End Namespace
