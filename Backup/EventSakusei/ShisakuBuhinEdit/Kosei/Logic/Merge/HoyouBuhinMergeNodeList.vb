﻿Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Tree
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix

Namespace ShisakuBuhinEdit.Kosei.Logic.Merge
    ''' <summary>
    ''' BuhinNodeの展開済みリストをマージするメソッドクラス
    ''' </summary>
    ''' <typeparam name="K">構成情報の型</typeparam>
    ''' <typeparam name="B">部品情報の型</typeparam>
    ''' <remarks></remarks>
    Public Class HoyouBuhinMergeNodeList(Of K, B)

        Private ReadOnly anAccessor As IBuhinNodeAccessor(Of K, B)
        Private ReadOnly aMergeMatrix As HoyouBuhinMergeMatrix
        Private ReadOnly _matrix As HoyouBuhinBuhinKoseiMatrix

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="anAccessor">Nodeアクセッサ</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal anAccessor As IBuhinNodeAccessor(Of K, B))
            Me.new(anAccessor, New HoyouBuhinBuhinKoseiMatrix)
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="anAccessor">Nodeアクセッサ</param>
        ''' <param name="matrix">部品表Spreadを表すクラス</param>
        ''' <param name="CanMergeIfDefferenceOyaHinban">親品番違いでもマージする場合、true</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal anAccessor As IBuhinNodeAccessor(Of K, B), ByVal matrix As HoyouBuhinBuhinKoseiMatrix, Optional ByVal CanMergeIfDefferenceOyaHinban As Boolean = True)
            Me.anAccessor = anAccessor
            Me._matrix = matrix
            Me.aMergeMatrix = New HoyouBuhinMergeMatrix(matrix)
            Me.CanMergeIfDefferenceOyaHinban = CanMergeIfDefferenceOyaHinban
        End Sub

#Region "Record関連"
        ''' <summary>マージした部品表</summary>
        Public ReadOnly Property ResultMatrix() As HoyouBuhinBuhinKoseiMatrix
            Get
                Return _matrix
            End Get
        End Property

        ''' <summary>
        ''' 部品表の行情報
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        Public ReadOnly Property Record(ByVal rowIndex As Integer) As HoyouBuhinBuhinKoseiRecordVo
            Get
                Return _matrix.Record(rowIndex)
            End Get
        End Property

        ''' <summary>部品表の行数</summary>
        Public ReadOnly Property RecordCount() As Integer
            Get
                Return _matrix.InputRowCount
            End Get
        End Property

        ''' <summary>
        ''' 部品表の員数情報
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        Public ReadOnly Property InsuSuryo(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Integer?
            Get
                Return _matrix.InsuSuryo(rowIndex, columnIndex)
            End Get
        End Property
#End Region

        ''' <summary>
        ''' 親品番違いでマージするか？
        ''' </summary>
        Public Property CanMergeIfDefferenceOyaHinban() As Boolean
            Get
                Return aMergeMatrix.AProperty.CanMergeIfDefferenceOyaHinban
            End Get
            Set(ByVal value As Boolean)
                aMergeMatrix.AProperty.CanMergeIfDefferenceOyaHinban = value
            End Set
        End Property

        ''' <summary>
        ''' 展開済みリストをマージする
        ''' </summary>
        ''' <param name="nodeLists">展開済みリスト</param>
        ''' <remarks></remarks>
        Public Sub Compute(ByVal ParamArray nodeLists As List(Of BuhinNode(Of K, B))())
            Dim columnIndex As Integer = _matrix.GetNewInsuColumnIndex
            For Each nodeList As List(Of BuhinNode(Of K, B)) In nodeLists
                ComputeMain(nodeList, columnIndex)
                columnIndex += 1
            Next
        End Sub

        ''' <summary>
        ''' 1列ごとにマージする、マージのMain処理
        ''' </summary>
        ''' <param name="nodes">展開済みのNode</param>
        ''' <param name="columnIndex">マージ先の列index</param>
        ''' <remarks></remarks>
        Private Sub ComputeMain(ByVal nodes As List(Of BuhinNode(Of K, B)), ByVal columnIndex As Integer)
            aMergeMatrix.InitializeMergeTop()
            Dim aDataImpl As DataImpl = New DataImpl(anAccessor, nodes.ToArray)
            For index As Integer = 0 To nodes.Count - 1
                aDataImpl.Index = index
                aMergeMatrix.Compute(columnIndex, aDataImpl)
            Next
        End Sub

        ''' <summary>
        ''' BuhinNodeの展開済みリストに対する IData実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class DataImpl : Implements HoyouBuhinMergeMatrix.IData
            Private anAccessor As IBuhinNodeAccessor(Of K, B)
            Private nodes As BuhinNode(Of K, B)()
            ''' <summary>展開済みリストのindex</summary>
            Public Index As Integer = 0

            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="anAccessor">Nodeアクセッサ</param>
            ''' <param name="nodes">展開済みのNode</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal anAccessor As IBuhinNodeAccessor(Of K, B), ByVal nodes As BuhinNode(Of K, B)())
                Me.anAccessor = anAccessor
                Me.nodes = nodes
            End Sub

            Private ReadOnly Property node() As BuhinNode(Of K, B)
                Get
                    Return nodes(Index)
                End Get
            End Property

            Public Function GetRecordNewRow() As HoyouBuhinBuhinKoseiRecordVo Implements HoyouBuhinMergeMatrix.IData.GetRecordNewRow

                '設計展開用にリターンさせる'
                If node.BuhinVo Is Nothing Then
                    Return Nothing
                End If

                Dim result As New HoyouBuhinBuhinKoseiRecordVo
                result.Level = GetLevel()
                result.ShukeiCode = GetShukeiCode()
                result.SiaShukeiCode = GetSiaShukeiCode()
                result.GencyoCkdKbn = GetGencyoCkdKbn()
                result.MakerCode = GetMakerCode()
                result.MakerName = anAccessor.GetMakerNameFrom(node)
                result.BuhinNo = GetBuhinNo()
                result.BuhinName = anAccessor.GetBuhinNameFrom(node)
                '
                result.BuhinNote = anAccessor.GetBuhinNote(node)
                result.KyoukuSection = anAccessor.GetKyoukuSection(node)
                Return result
            End Function

            Public Function GetParent() As HoyouBuhinMergeMatrix.IData Implements HoyouBuhinMergeMatrix.IData.GetParent
                If node.NodeParent Is Nothing Then
                    Return Nothing
                End If
                Dim aDataImpl As New DataImpl(anAccessor, nodes)
                Dim nodeList As New List(Of BuhinNode(Of K, B))(nodes)
                aDataImpl.Index = nodeList.IndexOf(node.NodeParent)
                Return aDataImpl
            End Function

            Public Function GetLevel() As Integer? Implements HoyouBuhinMergeMatrix.IData.GetLevel
                Return node.Level
            End Function

            Public Function GetInsuVo() As BuhinKoseiInsuCellVo Implements HoyouBuhinMergeMatrix.IData.GetInsuVo
                Dim result As New BuhinKoseiInsuCellVo
                result.InsuSuryo = node.InsuSummary
                Return result
            End Function

            Public Function GetBuhinNo() As String Implements HoyouBuhinMergeMatrix.IData.GetBuhinNo
                Return anAccessor.GetBuhinNoFrom(node)
            End Function

            Public Function GetMakerCode() As String Implements HoyouBuhinMergeMatrix.IData.GetMakerCode
                Return anAccessor.GetMakerCodeFrom(node)
            End Function

            Public Function GetShukeiCode() As String Implements HoyouBuhinMergeMatrix.IData.GetShukeiCode
                If GetLevel() = 0 Then
                    Return anAccessor.GetBuhinShukeiCodeFrom(node)
                End If
                Return anAccessor.GetShukeiCodeFrom(node)
            End Function

            Public Function GetSiaShukeiCode() As String Implements HoyouBuhinMergeMatrix.IData.GetSiaShukeiCode
                If GetLevel() = 0 Then
                    Return anAccessor.GetBuhinSiaShukeiCodeFrom(node)
                End If
                Return anAccessor.GetSiaShukeiCodeFrom(node)
            End Function

            Public Function GetGencyoCkdKbn() As String Implements HoyouBuhinMergeMatrix.IData.GetGencyoCkdKbn
                If GetLevel() = 0 Then
                    Return anAccessor.GetBuhinGencyoCkdKbnFrom(node)
                End If
                Return anAccessor.GetGencyoCkdKbnFrom(node)
            End Function
            '
            Public Function GetKyoukuSection() As String Implements HoyouBuhinMergeMatrix.IData.GetKyoukuSection
                Return anAccessor.GetKyoukuSection(node)
            End Function
        End Class
    End Class
End Namespace