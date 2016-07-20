Namespace ShisakuBuhinSakuseiList
    ''' <summary>
    ''' 「試作部品表作成一覧」　列が自動ソートされた、列の自動フィルタリングが実行された条件
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuBuhinSakuseiListFilterAndSortVo
#Region " メンバー変数 "
        ''' <summary>開発符号</summary>
        Private m_kaihatsuFugo As String

        ''' <summary>イベント</summary>
        Private m_eventPhaseName As String

        ''' <summary>ユニット区分</summary>
        Private m_unitKbn As String

        ''' <summary>状態</summary>
        Private m_statusName As String

        ''' <summary>ソート順項目</summary>
        Private m_sortItem As String

        ''' <summary>ソート順</summary>
        Private m_sortAscending As Boolean
#End Region
#Region " 開発符号、イベント、ユニット区分、状態、ソート順項目、ソート順 "
        ''' <summary>
        ''' 開発符号
        ''' </summary>
        ''' <value>開発符号</value>
        ''' <returns>開発符号</returns>
        ''' <remarks></remarks>
        Public Property KaihatsuFugo() As String
            Get
                Return m_kaihatsuFugo
            End Get
            Set(ByVal value As String)
                m_kaihatsuFugo = value
            End Set
        End Property

        ''' <summary>
        ''' イベント
        ''' </summary>
        ''' <value>イベント</value>
        ''' <returns>イベント</returns>
        ''' <remarks></remarks>
        Public Property EventPhaseName() As String
            Get
                Return m_eventPhaseName
            End Get
            Set(ByVal value As String)
                m_eventPhaseName = value
            End Set
        End Property

        ''' <summary>
        ''' ユニット区分
        ''' </summary>
        ''' <value>ユニット区分</value>
        ''' <returns>ユニット区分</returns>
        ''' <remarks></remarks>
        Public Property UnitKbn() As String
            Get
                Return m_unitKbn
            End Get
            Set(ByVal value As String)
                m_unitKbn = value
            End Set
        End Property

        ''' <summary>
        ''' 状態
        ''' </summary>
        ''' <value>状態</value>
        ''' <returns>状態</returns>
        ''' <remarks></remarks>
        Public Property StatusName() As String
            Get
                Return m_statusName
            End Get
            Set(ByVal value As String)
                m_statusName = value
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