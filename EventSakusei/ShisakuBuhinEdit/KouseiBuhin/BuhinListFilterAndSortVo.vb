Namespace KouseiBuhin
    ''' <summary>
    ''' 「部品一覧」　列が自動ソートされた、列の自動フィルタリングが実行された条件
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinListFilterAndSortVo
#Region " メンバー変数 "
        ''' <summary>ブロック</summary>
        Private m_block_No As String

        ''' <summary>レベル</summary>
        Private m_level As String

        ''' <summary>集計コード</summary>
        Private m_shukei_code As String

        ''' <summary>海外集計コード</summary>
        Private m_sia_shukei_code As String

        ''' <summary>部品番号</summary>
        Private m_buhin_no As String

        ''' <summary>員数</summary>
        Private m_insu As String

        ''' <summary>部品名称</summary>
        Private m_buhin_name As String

        ''' <summary>部品ノート</summary>
        Private m_buhin_note As String

        ''' <summary>ソート順項目</summary>
        Private m_sortItem As String

        ''' <summary>ソート順</summary>
        Private m_sortAscending As Boolean

#End Region

#Region " ブロック、レベル、集計コード、海外集計コード、部品番号、員数、部品名称、部品ノート、ソート順項目、ソート順 "
        ''' <summary>
        ''' ブロック
        ''' </summary>
        ''' <value>ブロック</value>
        ''' <returns>ブロック</returns>
        ''' <remarks></remarks>
        Public Property BlockNo() As String
            Get
                Return m_block_No
            End Get
            Set(ByVal value As String)
                m_block_No = value
            End Set
        End Property

        ''' <summary>
        ''' レベル
        ''' </summary>
        ''' <value>レベル</value>
        ''' <returns>レベル</returns>
        ''' <remarks></remarks>
        Public Property Level() As String
            Get
                Return m_level
            End Get
            Set(ByVal value As String)
                m_level = value
            End Set
        End Property

        ''' <summary>
        ''' 集計コード
        ''' </summary>
        ''' <value>集計コード</value>
        ''' <returns>集計コード</returns>
        ''' <remarks></remarks>
        Public Property ShukeiCode() As String
            Get
                Return m_shukei_Code
            End Get
            Set(ByVal value As String)
                m_shukei_code = value
            End Set
        End Property

        ''' <summary>
        ''' 海外集計コード
        ''' </summary>
        ''' <value>海外集計コード</value>
        ''' <returns>海外集計コード</returns>
        ''' <remarks></remarks>
        Public Property SiaShukeiCode() As String
            Get
                Return m_sia_shukei_code
            End Get
            Set(ByVal value As String)
                m_sia_shukei_code = value
            End Set
        End Property

        ''' <summary>
        ''' 部品番号
        ''' </summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        ''' <remarks></remarks>
        Public Property BuhinNo() As String
            Get
                Return m_buhin_no
            End Get
            Set(ByVal value As String)
                m_buhin_no = value
            End Set
        End Property

        ''' <summary>
        ''' 員数
        ''' </summary>
        ''' <value>員数</value>
        ''' <returns>員数</returns>
        ''' <remarks></remarks>
        Public Property Insu() As String
            Get
                Return m_insu
            End Get
            Set(ByVal value As String)
                m_insu = value
            End Set
        End Property

        ''' <summary>
        ''' 部品名称
        ''' </summary>
        ''' <value>部品名称</value>
        ''' <returns>部品名称</returns>
        ''' <remarks></remarks>
        Public Property BuhinName() As String
            Get
                Return m_buhin_name
            End Get
            Set(ByVal value As String)
                m_buhin_name = value
            End Set
        End Property

        ''' <summary>
        ''' 部品ノート
        ''' </summary>
        ''' <value>部品ノート</value>
        ''' <returns>部品ノート</returns>
        ''' <remarks></remarks>
        Public Property BuhinNote() As String
            Get
                Return m_buhin_note
            End Get
            Set(ByVal value As String)
                m_buhin_note = value
            End Set
        End Property

        ''' <summary>
        ''' ソート順項目
        ''' </summary>
        ''' <value>ソート順項目</value>
        ''' <returns>ソート順項目</returns>
        ''' <remarks></remarks>
        Public Property SortItem() As String
            Get
                Return m_sortItem
            End Get
            Set(ByVal value As String)
                m_sortItem = value
            End Set
        End Property

        ''' <summary>
        ''' ソート順が昇順で行われたかどうか
        ''' </summary>
        ''' <value>ソート順</value>
        ''' <returns>ソート順</returns>
        ''' <remarks></remarks>
        Public Property SortAscending() As Boolean
            Get
                Return m_sortAscending
            End Get
            Set(ByVal value As Boolean)
                m_sortAscending = value
            End Set
        End Property
#End Region
    End Class
End Namespace