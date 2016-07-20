Namespace YosanBuhinEdit.KouseiBuhin
    ''' <summary>
    ''' 「区分一覧」　列が自動ソートされた、列の自動フィルタリングが実行された条件
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BlockListFilterAndSortVo
#Region " メンバー変数 "
        ''' <summary>ブロック</summary>
        Private m_block As String

        ''' <summary>ブロック名称</summary>
        Private m_blockName As String

        ''' <summary>ソート順項目</summary>
        Private m_sortItem As String

        ''' <summary>ソート順</summary>
        Private m_sortAscending As Boolean

#End Region
#Region " ブロック、ブロック名称、ソート順項目、ソート順 "
        ''' <summary>
        ''' ブロック
        ''' </summary>
        ''' <value>ブロック</value>
        ''' <returns>ブロック</returns>
        ''' <remarks></remarks>
        Public Property Block() As String
            Get
                Return m_block
            End Get
            Set(ByVal value As String)
                m_block = value
            End Set
        End Property

        ''' <summary>
        ''' ブロック名称
        ''' </summary>
        ''' <value>ブロック名称</value>
        ''' <returns>ブロック名称</returns>
        ''' <remarks></remarks>
        Public Property BlockName() As String
            Get
                Return m_blockName
            End Get
            Set(ByVal value As String)
                m_blockName = value
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