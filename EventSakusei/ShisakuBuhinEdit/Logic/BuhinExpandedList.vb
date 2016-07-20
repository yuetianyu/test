Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Logic

    Public Class BuhinExpandedList
        ' Lv.0 のNode
        Private _roots As List(Of Node)
        ' Nodeの部品表展開済み一覧
        Private nodes As List(Of Node)
        ' NodeのIndex
        Private indexes As Dictionary(Of String, Node)
        Private Sub New(ByVal roots As List(Of Node), ByVal nodes As List(Of Node))
            Me.New(roots, nodes, Nothing)
        End Sub
        Private Sub New(ByVal roots As List(Of Node), ByVal nodes As List(Of Node), ByVal indexes As Dictionary(Of String, Node))
            Me._roots = roots
            Me.nodes = nodes
            Me.indexes = indexes
        End Sub

        Public ReadOnly Property Count() As Integer
            Get
                Return nodes.Count
            End Get
        End Property
        Public ReadOnly Property Values(ByVal index As Integer) As BuhinExpandedVo
            Get
                Return nodes(index).vo
            End Get
        End Property


        ''' <summary>
        ''' VOと親子関係をもつクラス
        ''' </summary>
        ''' <remarks></remarks>
        Public Class Node
            Public Vo As BuhinExpandedVo

            ''' <summary>親</summary>
            Public NodeParent As Node

            ''' <summary>子供たち</summary>
            Public NodeChildren As New List(Of Node)

            ''' <summary>Lv.</summary>
            Public Level As Integer

            ''' <summary>員数計</summary>
            Public InsuSummary As Integer?

            Public Sub New(ByVal vo As BuhinExpandedVo)
                Me.Vo = vo
            End Sub
            ''' <summary>
            ''' 子供を追加する
            ''' </summary>
            ''' <param name="child">子供Node</param>
            ''' <remarks></remarks>
            Public Sub AddChild(ByVal child As Node)
                NodeChildren.Add(child)
            End Sub
            Public Overridable ReadOnly Property BuhinNo() As String
                Get
                    Return Vo.BuhinNoKo
                End Get
            End Property
            Public Overridable ReadOnly Property BuhinNoOya() As String
                Get
                    Return Vo.BuhinNoOya
                End Get
            End Property
            Public Overridable ReadOnly Property InsuSuryo() As Integer?
                Get
                    Return Vo.InsuSuryo
                End Get
            End Property
            Public ReadOnly Property Key() As String
                Get
                    Return MakeKey(Vo.BuhinNoOya, Vo.BuhinNoKo)
                End Get
            End Property
        End Class
        Private Class RootNode : Inherits Node
            Public Sub New(ByVal hinban As String)
                MyBase.New(New BuhinExpandedVo)
                Vo.BuhinNoKo = hinban
                Vo.InsuSuryo = 1
                Vo.Level = 0
            End Sub
        End Class

        Private Class TShisakuBuhinKouseiVoComparerable : Implements IComparer(Of TShisakuBuhinKouseiVo)
            Public Function Compare(ByVal x As ShisakuCommon.Db.EBom.Vo.TShisakuBuhinKouseiVo, ByVal y As ShisakuCommon.Db.EBom.Vo.TShisakuBuhinKouseiVo) As Integer Implements System.Collections.Generic.IComparer(Of ShisakuCommon.Db.EBom.Vo.TShisakuBuhinKouseiVo).Compare
                Dim result As Integer = x.BuhinNoOya.CompareTo(y.BuhinNoOya)
                If result <> 0 Then
                    Return result
                End If
                Return StringUtil.Nvl(x.BuhinNoKo).CompareTo(StringUtil.Nvl(y.BuhinNoKo))
            End Function
        End Class
        Public Shared Function MakeKey(ByVal ParamArray keys As String()) As String
            Return Join(keys, "::")
        End Function

        Private Class LvOyaKoSort : Implements IComparer(Of BuhinExpandedVo)
            Public Function Compare(ByVal x As BuhinExpandedVo, ByVal y As BuhinExpandedVo) As Integer Implements System.Collections.Generic.IComparer(Of BuhinExpandedVo).Compare
                Dim result1 As Integer = CInt(x.Level).CompareTo(CInt(y.Level))
                If result1 <> 0 Then
                    Return result1
                End If
                Dim result2 As Integer = x.BuhinNoOya.CompareTo(y.BuhinNoOya)
                If result2 <> 0 Then
                    Return result2
                End If
                Return x.BuhinNoKo.CompareTo(y.BuhinNoKo)
            End Function
        End Class

        Private Class OyaKo(Of T)
            Private _indexes As New Dictionary(Of String, Dictionary(Of String, T))
            Private ReadOnly Property Parent(ByVal oya As String) As Dictionary(Of String, T)
                Get
                    If Not _indexes.ContainsKey(oya) Then
                        _indexes.Add(oya, New Dictionary(Of String, T))
                    End If
                    Return _indexes(oya)
                End Get
            End Property
            Default Public Property Item(ByVal oya As String, ByVal ko As String) As T
                Get
                    If Not Parent(oya).ContainsKey(ko) Then
                        Return Nothing
                    End If
                    Return Parent(oya)(ko)
                End Get
                Set(ByVal value As T)
                    If Parent(oya).ContainsKey(ko) Then
                        Parent(oya).Remove(ko)
                    End If
                    Parent(oya).Add(ko, value)
                End Set
            End Property
            Public ReadOnly Property Items(ByVal oya As String) As ICollection(Of T)
                Get
                    Return Parent(oya).Values
                End Get
            End Property
        End Class

        Public Shared Function NewBuhinExpandedList(ByVal fHinban As String, ByVal aList As List(Of TShisakuBuhinKouseiVo)) As BuhinExpandedList
            Dim paramList As List(Of BuhinExpandedVo) = Nothing
            If aList Is Nothing OrElse 0 = aList.Count Then
                Return NewBuhinExpandedList(fHinban, paramList)
            End If
            paramList = New List(Of BuhinExpandedVo)
            Dim counter As Integer = 0
            For Each vo As TShisakuBuhinKouseiVo In aList
                Dim newVo As New BuhinExpandedVo
                VoUtil.CopyProperties(vo, newVo)
                newVo.Level = EzUtil.Increment(counter)
                paramList.Add(newVo)
            Next
            Return NewBuhinExpandedList(fHinban, paramList)
        End Function
        Public Shared Function NewBuhinExpandedList(ByVal fHinban As String, ByVal aList As List(Of BuhinExpandedVo)) As BuhinExpandedList
            If aList Is Nothing OrElse 0 = aList.Count Then
                Throw New ArgumentException("構成情報がありません.")
            End If

            Dim sortedList As New List(Of BuhinExpandedVo)(aList)
            sortedList.Sort(New LvOyaKoSort)

            Dim indexes As New OyaKo(Of List(Of Node))
            Dim indexQueues As New OyaKo(Of Queue(Of Node))
            For Each vo As BuhinExpandedVo In sortedList
                Dim aNode As Node = New Node(vo)
                If indexes(vo.BuhinNoOya, vo.BuhinNoKo) Is Nothing Then
                    indexes(vo.BuhinNoOya, vo.BuhinNoKo) = New List(Of Node)
                    indexQueues(vo.BuhinNoOya, vo.BuhinNoKo) = New Queue(Of Node)
                End If
                indexes(vo.BuhinNoOya, vo.BuhinNoKo).Add(aNode)
                indexQueues(vo.BuhinNoOya, vo.BuhinNoKo).Enqueue(aNode)
            Next

            Dim root As New RootNode(sortedList(0).BuhinNoOya)
            MakeFromParent(indexQueues, root)

            Dim nodes As New List(Of Node)
            Dim oyaLevel As Integer = -1    ' rootの親は、Lv.-1という事で.
            MakeExpandedList(root, oyaLevel, 1, nodes)

            Return New BuhinExpandedList(New List(Of Node)(New Node() {root}), nodes)

        End Function

        Private Shared Sub MakeFromParent(ByVal indexQueues As OyaKo(Of Queue(Of Node)), ByVal aNode As Node)

            ' NodeのBuhinNoを親にもつNodeの一覧で For Each
            For Each children As Queue(Of Node) In indexQueues.Items(aNode.BuhinNo)
                Dim child As Node = children.Dequeue
                aNode.AddChild(child)
                MakeFromParent(indexQueues, child)
            Next
        End Sub

        Public Shared Function NewUniqueBuhinExpandedList(ByVal fHinban As String, ByVal aList As List(Of TShisakuBuhinKouseiVo)) As BuhinExpandedList
            Dim works As New List(Of TShisakuBuhinKouseiVo)(aList)
            works.Sort(New TShisakuBuhinKouseiVoComparerable)

            Dim nodes2 As New List(Of Node)
            Dim indexes As New Dictionary(Of String, Node)
            For Each vo As TShisakuBuhinKouseiVo In works
                Dim newVo As New BuhinExpandedVo
                VoUtil.CopyProperties(vo, newVo)
                Dim aNode As Node = New Node(newVo)
                nodes2.Add(aNode)
                indexes.Add(aNode.BuhinNo, aNode)
            Next

            Dim roots As New List(Of Node)
            For Each aNode As Node In nodes2
                If Not indexes.ContainsKey(aNode.BuhinNoOya) Then
                    Dim root As RootNode = New RootNode(aNode.BuhinNoOya)
                    indexes.Add(aNode.BuhinNoOya, root)
                    roots.Add(root)
                End If
                aNode.NodeParent = indexes(aNode.BuhinNoOya)
                aNode.NodeParent.AddChild(aNode)
            Next

            Dim nodes As New List(Of Node)
            Dim oyaLevel As Integer = -1    ' rootの親は、Lv.-1という事で.
            For Each root As Node In roots
                MakeExpandedList(root, oyaLevel, 1, nodes)
            Next

            Return New BuhinExpandedList(roots, nodes, indexes)
        End Function

        Private Shared Sub MakeExpandedList(ByVal aNode As Node, ByVal oyaLevel As Integer, ByVal oyaInsu As Integer, ByVal result As List(Of Node))
            result.Add(aNode)
            aNode.Level = oyaLevel + 1
            aNode.InsuSummary = oyaInsu * aNode.InsuSuryo
            aNode.Vo.Level = oyaLevel + 1
            aNode.Vo.InsuSummary = oyaInsu * aNode.InsuSuryo
            For Each child As Node In aNode.NodeChildren
                MakeExpandedList(child, aNode.Level, aNode.InsuSummary, result)
            Next
        End Sub

    End Class
End Namespace
