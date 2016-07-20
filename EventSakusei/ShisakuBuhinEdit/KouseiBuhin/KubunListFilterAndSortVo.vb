Namespace KouseiBuhin
    ''' <summary>
    ''' 「区分一覧」　列が自動ソートされた、列の自動フィルタリングが実行された条件
    ''' </summary>
    ''' <remarks></remarks>
    Public Class KubunListFilterAndSortVo
#Region " メンバー変数 "
        ''' <summary>区分</summary>
        Private m_kubun As String

        ''' <summary>区分名称</summary>
        Private m_kubunName As String

        ''' <summary>ソート順項目</summary>
        Private m_sortItem As String

        ''' <summary>ソート順</summary>
        Private m_sortAscending As Boolean

#End Region
#Region " 区分、区分名称、ソート順項目、ソート順 "
        ''' <summary>
        ''' 区分
        ''' </summary>
        ''' <value>区分</value>
        ''' <returns>区分</returns>
        ''' <remarks></remarks>
        Public Property Kubun() As String
            Get
                Return m_kubun
            End Get
            Set(ByVal value As String)
                m_kubun = value
            End Set
        End Property

        ''' <summary>
        ''' 区分名称
        ''' </summary>
        ''' <value>区分名称</value>
        ''' <returns>区分名称</returns>
        ''' <remarks></remarks>
        Public Property KubunName() As String
            Get
                Return m_kubunName
            End Get
            Set(ByVal value As String)
                m_kubunName = value
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