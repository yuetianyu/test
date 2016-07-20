Namespace YosanBuhinEdit.Kosei.Logic.Tree
    Public Class BuhinSingleMatrix(Of K, B)

        ' RootNode
        Private _root As BuhinNode(Of K, B)
        ' 展開済みリスト
        Private _nodes As List(Of BuhinNode(Of K, B))

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(Nothing, New List(Of BuhinNode(Of K, B)))
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="root">基点のNode</param>
        ''' <param name="nodes">展開済みNodeリスト</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal root As BuhinNode(Of K, B), ByVal nodes As List(Of BuhinNode(Of K, B)))
            Me._root = root
            Me._nodes = nodes
        End Sub

        ''' <summary>RootNode</summary>
        ''' <value>RootNode</value>
        ''' <returns>RootNode</returns>
        Public Property Root() As BuhinNode(Of K, B)
            Get
                Return _root
            End Get
            Set(ByVal value As BuhinNode(Of K, B))
                _root = value
            End Set
        End Property

        ''' <summary>展開済みリスト</summary>
        ''' <value>展開済みリスト</value>
        ''' <returns>展開済みリスト</returns>
        Public Property Nodes() As List(Of BuhinNode(Of K, B))
            Get
                Return _nodes
            End Get
            Set(ByVal value As List(Of BuhinNode(Of K, B)))
                _nodes = value
            End Set
        End Property

        Public ReadOnly Property RootBuhinNo() As String
            Get
                Return _root.BuhinNo
            End Get
        End Property
    End Class
End Namespace