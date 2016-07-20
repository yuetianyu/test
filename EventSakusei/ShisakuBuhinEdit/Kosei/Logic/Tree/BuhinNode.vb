Namespace ShisakuBuhinEdit.Kosei.Logic.Tree
    ''' <summary>
    ''' 部品情報と構成情報をもち親子関係を保持するインターフェース
    ''' </summary>
    ''' <typeparam name="K">構成情報の型</typeparam>
    ''' <typeparam name="B">部品情報の型</typeparam>
    ''' <remarks></remarks>
    Public Interface BuhinNode(Of K, B)
        ''' <summary>構成レコード</summary>
        Property KoseiVo() As K

        ''' <summary>部品レコード</summary>
        Property BuhinVo() As B

        ''' <summary>親</summary>
        Property NodeParent() As BuhinNode(Of K, B)

        ''' <summary>子供たち</summary>
        ReadOnly Property NodeChildren() As ICollection(Of BuhinNode(Of K, B))

        ''' <summary>子供たち</summary>
        ReadOnly Property NodeChild(ByVal index As Integer) As BuhinNode(Of K, B)

        ''' <summary>子供の数</summary>
        ReadOnly Property NodeChildrenCount() As Integer

        ''' <summary>Lv.</summary>
        Property Level() As Integer

        ''' <summary>員数計</summary>
        Property InsuSummary() As Integer?

        ''' <summary>
        ''' 子供を追加する
        ''' </summary>
        ''' <param name="child">子供Node</param>
        ''' <remarks></remarks>
        Sub AddChild(ByVal child As BuhinNode(Of K, B))

        ''' <summary>
        ''' クローンを返す
        ''' </summary>
        ''' <returns>新インスタンス</returns>
        ''' <remarks></remarks>
        Function Clone() As BuhinNode(Of K, B)

        ''' <summary>部品番号</summary>
        ReadOnly Property BuhinNo() As String

        ''' <summary>部品番号(親)</summary>
        ReadOnly Property BuhinNoOya() As String

        ''' <summary>員数</summary>
        ReadOnly Property InsuSuryo() As Integer?
    End Interface
End Namespace