Namespace YosanBuhinEdit.KouseiBuhin.Dao.Vo

    ''' <summary>
    ''' 部品番号リストVo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinListVo

        '部課コード
        Private _BukaCode As String

        'ブロック№
        Private _BlockNo As String

        'レベル
        Private _Level As Nullable(Of Int32)

        '国内集計コード
        Private _ShukeiCode As String

        '海外集計コード
        Private _SiaShukeiCode As String

        '部品番号
        Private _BuhinNo As String

        '員数
        Private _Insu As String

        '部品番号名称
        Private _BuhinName As String

        '取引先コード
        Private _TorihikisakiCode As String

        '取引先名称
        Private _TorihikisakiName As String

        'Note
        Private _Note As String

        '構成選択方法
        Private _SelectionMethod As String

        '供給セクション
        Private _Kyokyu As String

        ''' <summary>
        ''' 部課コード
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BukaCode() As String
            Get
                Return _BukaCode
            End Get
            Set(ByVal value As String)
                _BukaCode = value
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

        ''' <summary>レベル</summary>
        ''' <value>レベル</value>
        ''' <returns>レベル</returns>
        Public Property Level() As Nullable(Of Int32)
            Get
                Return _Level
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Level = value
            End Set
        End Property

        ''' <summary>国内集計コード</summary>
        ''' <value>国内集計コード</value>
        ''' <returns>国内集計コード</returns>
        Public Property ShukeiCode() As String
            Get
                Return _ShukeiCode
            End Get
            Set(ByVal value As String)
                _ShukeiCode = value
            End Set
        End Property

        ''' <summary>海外SIA集計コード</summary>
        ''' <value>海外SIA集計コード</value>
        ''' <returns>海外SIA集計コード</returns>
        Public Property SiaShukeiCode() As String
            Get
                Return _SiaShukeiCode
            End Get
            Set(ByVal value As String)
                _SiaShukeiCode = value
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

        ''' <summary>員数</summary>
        ''' <value>員数</value>
        ''' <returns>員数</returns>
        Public Property Insu() As String
            Get
                Return _Insu
            End Get
            Set(ByVal value As String)
                _Insu = value
            End Set
        End Property

        ''' <summary>
        ''' 部品番号名称
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
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
        ''' 取引先コード
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property TorihikisakiCode() As String
            Get
                Return _TorihikisakiCode
            End Get
            Set(ByVal value As String)
                _TorihikisakiCode = value
            End Set
        End Property

        ''' <summary>
        ''' 取引先名称
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property TorihikisakiName() As String
            Get
                Return _TorihikisakiName
            End Get
            Set(ByVal value As String)
                _TorihikisakiName = value
            End Set
        End Property

        ''' <summary>
        ''' Note
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Note() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                _Note = value
            End Set
        End Property

        ''' <summary>
        ''' SelectionMethod
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SelectionMethod() As String
            Get
                Return _SelectionMethod
            End Get
            Set(ByVal value As String)
                _SelectionMethod = value
            End Set
        End Property

        ''' <summary>
        ''' 供給セクション
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Kyokyu() As String
            Get
                Return _Kyokyu
            End Get
            Set(ByVal value As String)
                _Kyokyu = value
            End Set
        End Property

    End Class

End Namespace

