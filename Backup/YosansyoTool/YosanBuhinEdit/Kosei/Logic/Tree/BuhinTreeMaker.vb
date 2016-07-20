Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanBuhinEdit.Kosei.Logic.Tree
    ''' <summary>
    ''' 部品情報・構成情報から展開済みリストを作成するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinTreeMaker
        ''' <summary>
        ''' EBomの構成情報・部品情報のアクセッサ
        ''' </summary>
        ''' <remarks></remarks>
        Private Class Rhac0552Accessor : Implements BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo).Accessor

            Public Function GetBuhinNoFrom(ByVal vo As Rhac0532Vo) As String Implements BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo).Accessor.GetBuhinNoFrom
                Return vo.BuhinNo
            End Function

            Public Function GetMidashiNoFrom(ByVal vo As Rhac0552Vo) As String Implements BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo).Accessor.GetMidashiNoFrom
                Return vo.MidashiNo
            End Function

            Public Function GetMidashiNoShuruiFrom(ByVal vo As Rhac0552Vo) As String Implements BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo).Accessor.GetMidashiNoShuruiFrom
                Return vo.MidashiNoShurui
            End Function

            Public Function GetBuhinNoKoFrom(ByVal vo As Rhac0552Vo) As String Implements BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo).Accessor.GetBuhinNoKoFrom
                Return vo.BuhinNoKo
            End Function

            Public Function GetBuhinNoOyaFrom(ByVal vo As Rhac0552Vo) As String Implements BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo).Accessor.GetBuhinNoOyaFrom
                Return vo.BuhinNoOya
            End Function

            Public Function GetInsuSuryoFrom(ByVal vo As Rhac0552Vo) As Integer? Implements BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo).Accessor.GetInsuSuryoFrom
                Return vo.InsuSuryo
            End Function

            Public Sub SetBuhinNoKoTo(ByVal vo As Rhac0552Vo, ByVal value As String) Implements BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo).Accessor.SetBuhinNoKoTo
                vo.BuhinNoKo = value
            End Sub

            Public Sub SetBuhinNoOyaTo(ByVal vo As Rhac0552Vo, ByVal value As String) Implements BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo).Accessor.SetBuhinNoOyaTo
                vo.BuhinNoOya = value
            End Sub

            Public Sub SetInsuSuryoTo(ByVal vo As Rhac0552Vo, ByVal value As Integer?) Implements BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo).Accessor.SetInsuSuryoTo
                vo.InsuSuryo = value
            End Sub
        End Class

        ''' <summary>
        ''' 試作システムの構成情報・部品情報のアクセッサ
        ''' </summary>
        ''' <remarks></remarks>
        Private Class ShisakuAccessor : Implements BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo).Accessor

            Public Function GetBuhinNoFrom(ByVal vo As TShisakuBuhinVo) As String Implements BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo).Accessor.GetBuhinNoFrom
                Return vo.BuhinNo
            End Function

            Public Function GetMidashiNoFrom(ByVal vo As TShisakuBuhinKouseiVo) As String Implements BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo).Accessor.GetMidashiNoFrom
                Return vo.MidashiNo
            End Function

            Public Function GetMidashiNoShuruiFrom(ByVal vo As TShisakuBuhinKouseiVo) As String Implements BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo).Accessor.GetMidashiNoShuruiFrom
                Return vo.MidashiNoShurui
            End Function

            Public Function GetBuhinNoKoFrom(ByVal vo As TShisakuBuhinKouseiVo) As String Implements BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo).Accessor.GetBuhinNoKoFrom
                Return vo.BuhinNoKo
            End Function

            Public Function GetBuhinNoOyaFrom(ByVal vo As TShisakuBuhinKouseiVo) As String Implements BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo).Accessor.GetBuhinNoOyaFrom
                Return vo.BuhinNoOya
            End Function

            Public Function GetInsuSuryoFrom(ByVal vo As TShisakuBuhinKouseiVo) As Integer? Implements BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo).Accessor.GetInsuSuryoFrom
                Return vo.InsuSuryo
            End Function

            Public Sub SetBuhinNoKoTo(ByVal vo As TShisakuBuhinKouseiVo, ByVal value As String) Implements BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo).Accessor.SetBuhinNoKoTo
                vo.BuhinNoKo = value
            End Sub

            Public Sub SetBuhinNoOyaTo(ByVal vo As TShisakuBuhinKouseiVo, ByVal value As String) Implements BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo).Accessor.SetBuhinNoOyaTo
                vo.BuhinNoOya = value
            End Sub

            Public Sub SetInsuSuryoTo(ByVal vo As TShisakuBuhinKouseiVo, ByVal value As Integer?) Implements BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo).Accessor.SetInsuSuryoTo
                vo.InsuSuryo = value
            End Sub
        End Class

        ''' <summary>
        ''' 部品構成情報から、Root品番を返す
        ''' </summary>
        ''' <param name="aList">部品構成情報</param>
        ''' <returns>Root品番(n件)</returns>
        ''' <remarks></remarks>
        Public Shared Function DetectRoots(ByVal aList As List(Of Rhac0552Vo)) As String()
            Return GetInstanceRhac0552.DetectRoots(aList)
        End Function
        ''' <summary>
        ''' 部品構成情報から、Root品番を返す
        ''' </summary>
        ''' <param name="aList">部品構成情報</param>
        ''' <returns>Root品番(n件)</returns>
        ''' <remarks></remarks>
        Public Shared Function DetectRoots(ByVal aList As List(Of TShisakuBuhinKouseiVo)) As String()
            Return GetInstanceTShisakuBuhinKousei.DetectRoots(aList)
        End Function

        ''' <summary>
        ''' 部品構成情報から、Tree構造を返す
        ''' </summary>
        ''' <param name="aList">部品構成情報</param>
        ''' <returns>Tree構造(n件)</returns>
        ''' <remarks></remarks>
        Public Shared Function NewRootBuhinNodes(ByVal aList As List(Of Rhac0552Vo)) As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)()
            Return GetInstanceRhac0552.NewRootBuhinNodes(aList)
        End Function
        ''' <summary>
        ''' 部品構成情報から、Tree構造を返す
        ''' </summary>
        ''' <param name="aList">部品構成情報</param>
        ''' <returns>Tree構造(n件)</returns>
        ''' <remarks></remarks>
        Public Shared Function NewRootBuhinNodes(ByVal aList As List(Of TShisakuBuhinKouseiVo)) As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)()
            Return GetInstanceTShisakuBuhinKousei.NewRootBuhinNodes(aList)
        End Function

        ''' <summary>
        ''' 部品情報を、Nodeとその子供たち全体に設定する
        ''' </summary>
        ''' <param name="buhinVos">部品情報</param>
        ''' <param name="structuredRoots">Tree構造済みのNodeたち</param>
        ''' <remarks></remarks>
        Public Shared Sub SetBuhinTo(ByVal buhinVos As List(Of Rhac0532Vo), ByVal ParamArray structuredRoots As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)())
            GetInstanceRhac0552.SetBuhinTo(buhinVos, structuredRoots)
        End Sub
        ''' <summary>
        ''' 部品情報を、Nodeとその子供たち全体に設定する
        ''' </summary>
        ''' <param name="buhinVos">部品情報</param>
        ''' <param name="structuredRoots">Tree構造済みのNodeたち</param>
        ''' <remarks></remarks>
        Public Shared Sub SetBuhinTo(ByVal buhinVos As List(Of TShisakuBuhinVo), ByVal ParamArray structuredRoots As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)())
            GetInstanceTShisakuBuhinKousei.SetBuhinTo(buhinVos, structuredRoots)
        End Sub

        ''' <summary>
        ''' 展開済みリストを作成して返す
        ''' </summary>
        ''' <param name="structuredRoot">Tree構造済み親Node</param>
        ''' <returns>展開済みリスト</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeExpandedList(ByVal structuredRoot As BuhinNode(Of Rhac0552Vo, Rhac0532Vo)) As List(Of BuhinNode(Of Rhac0552Vo, Rhac0532Vo))
            Return GetInstanceRhac0552.MakeExpandedList(structuredRoot)
        End Function
        ''' <summary>
        ''' 展開済みリストを作成して返す
        ''' </summary>
        ''' <param name="structuredRoot">Tree構造済み親Node</param>
        ''' <returns>展開済みリスト</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeExpandedList(ByVal structuredRoot As BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)) As List(Of BuhinNode(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo))
            Return GetInstanceTShisakuBuhinKousei.MakeExpandedList(structuredRoot)
        End Function

        ''' <summary>
        ''' 構成情報から、展開済みリストを返す
        ''' </summary>
        ''' <param name="koseiVos">構成情報</param>
        ''' <param name="buhinVos">部品情報</param>
        ''' <returns>展開済みリスト(n件)</returns>
        ''' <remarks></remarks>
        Public Shared Function NewSingleMatrices(ByVal koseiVos As List(Of Rhac0552Vo), ByVal buhinVos As List(Of Rhac0532Vo)) As BuhinSingleMatrix(Of Rhac0552Vo, Rhac0532Vo)()
            Return GetInstanceRhac0552.NewSingleMatrices(koseiVos, buhinVos)
        End Function

#Region "FM5以降～"

        ''' <summary>
        ''' EBomの構成情報・部品情報のアクセッサ
        ''' </summary>
        ''' <remarks></remarks>
        Private Class Rhac0553Accessor : Implements BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo).Accessor

            Public Function GetBuhinNoFrom(ByVal vo As Rhac0533Vo) As String Implements BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo).Accessor.GetBuhinNoFrom
                Return vo.BuhinNo
            End Function

            Public Function GetMidashiNoFrom(ByVal vo As Rhac0553Vo) As String Implements BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo).Accessor.GetMidashiNoFrom
                Return vo.MidashiNo
            End Function

            Public Function GetMidashiNoShuruiFrom(ByVal vo As Rhac0553Vo) As String Implements BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo).Accessor.GetMidashiNoShuruiFrom
                Return vo.MidashiNoShurui
            End Function

            Public Function GetBuhinNoKoFrom(ByVal vo As Rhac0553Vo) As String Implements BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo).Accessor.GetBuhinNoKoFrom
                Return vo.BuhinNoKo
            End Function

            Public Function GetBuhinNoOyaFrom(ByVal vo As Rhac0553Vo) As String Implements BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo).Accessor.GetBuhinNoOyaFrom
                Return vo.BuhinNoOya
            End Function

            Public Function GetInsuSuryoFrom(ByVal vo As Rhac0553Vo) As Integer? Implements BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo).Accessor.GetInsuSuryoFrom
                Return vo.InsuSuryo
            End Function

            Public Sub SetBuhinNoKoTo(ByVal vo As Rhac0553Vo, ByVal value As String) Implements BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo).Accessor.SetBuhinNoKoTo
                vo.BuhinNoKo = value
            End Sub

            Public Sub SetBuhinNoOyaTo(ByVal vo As Rhac0553Vo, ByVal value As String) Implements BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo).Accessor.SetBuhinNoOyaTo
                vo.BuhinNoOya = value
            End Sub

            Public Sub SetInsuSuryoTo(ByVal vo As Rhac0553Vo, ByVal value As Integer?) Implements BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo).Accessor.SetInsuSuryoTo
                vo.InsuSuryo = value
            End Sub
        End Class

        ''' <summary>
        ''' 構成情報から、展開済みリストを返す(FM5以降～)
        ''' </summary>
        ''' <param name="koseiVos">構成情報</param>
        ''' <param name="buhinVos">部品情報</param>
        ''' <returns>展開済みリスト(n件)</returns>
        ''' <remarks></remarks>
        Public Shared Function NewSingleMatrices(ByVal koseiVos As List(Of Rhac0553Vo), ByVal buhinVos As List(Of Rhac0533Vo)) As BuhinSingleMatrix(Of Rhac0553Vo, Rhac0533Vo)()
            Return GetInstanceRhac0553.NewSingleMatrices(koseiVos, buhinVos)
        End Function

        Private Shared instance0553 As BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo)
        ''' <summary>
        ''' EBom用の展開済みリスト作成クラスを返す
        ''' </summary>
        ''' <returns>唯一のインスタンス</returns>
        ''' <remarks></remarks>
        Private Shared Function GetInstanceRhac0553() As BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo)
            If instance0553 Is Nothing Then
                instance0553 = New BuhinTreeMaker(Of Rhac0553Vo, Rhac0533Vo)(New Rhac0553Accessor)
            End If
            Return instance0553
        End Function

#End Region

#Region "パンダ前"

        ''' <summary>
        ''' EBomの構成情報・部品情報のアクセッサ
        ''' </summary>
        ''' <remarks></remarks>
        Private Class Rhac0551Accessor : Implements BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo).Accessor

            Public Function GetBuhinNoFrom(ByVal vo As Rhac0530Vo) As String Implements BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo).Accessor.GetBuhinNoFrom
                Return vo.BuhinNo
            End Function

            Public Function GetMidashiNoFrom(ByVal vo As Rhac0551Vo) As String Implements BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo).Accessor.GetMidashiNoFrom
                Return vo.MidashiNo
            End Function

            Public Function GetMidashiNoShuruiFrom(ByVal vo As Rhac0551Vo) As String Implements BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo).Accessor.GetMidashiNoShuruiFrom
                Return vo.MidashiNoShurui
            End Function

            Public Function GetBuhinNoKoFrom(ByVal vo As Rhac0551Vo) As String Implements BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo).Accessor.GetBuhinNoKoFrom
                Return vo.BuhinNoKo
            End Function

            Public Function GetBuhinNoOyaFrom(ByVal vo As Rhac0551Vo) As String Implements BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo).Accessor.GetBuhinNoOyaFrom
                Return vo.BuhinNoOya
            End Function

            Public Function GetInsuSuryoFrom(ByVal vo As Rhac0551Vo) As Integer? Implements BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo).Accessor.GetInsuSuryoFrom
                Return vo.InsuSuryo
            End Function

            Public Sub SetBuhinNoKoTo(ByVal vo As Rhac0551Vo, ByVal value As String) Implements BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo).Accessor.SetBuhinNoKoTo
                vo.BuhinNoKo = value
            End Sub

            Public Sub SetBuhinNoOyaTo(ByVal vo As Rhac0551Vo, ByVal value As String) Implements BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo).Accessor.SetBuhinNoOyaTo
                vo.BuhinNoOya = value
            End Sub

            Public Sub SetInsuSuryoTo(ByVal vo As Rhac0551Vo, ByVal value As Integer?) Implements BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo).Accessor.SetInsuSuryoTo
                vo.InsuSuryo = value
            End Sub
        End Class

        ''' <summary>
        ''' 構成情報から、展開済みリストを返す
        ''' </summary>
        ''' <param name="koseiVos">構成情報</param>
        ''' <param name="buhinVos">部品情報</param>
        ''' <returns>展開済みリスト(n件)</returns>
        ''' <remarks></remarks>
        Public Shared Function NewSingleMatrices(ByVal koseiVos As List(Of Rhac0551Vo), ByVal buhinVos As List(Of Rhac0530Vo)) As BuhinSingleMatrix(Of Rhac0551Vo, Rhac0530Vo)()
            Return GetInstanceRhac0551.NewSingleMatrices(koseiVos, buhinVos)
        End Function

        Private Shared instance0551 As BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo)

        ''' <summary>
        ''' EBom用の展開済みリスト作成クラスを返す
        ''' </summary>
        ''' <returns>唯一のインスタンス</returns>
        ''' <remarks></remarks>
        Private Shared Function GetInstanceRhac0551() As BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo)
            If instance0551 Is Nothing Then
                instance0551 = New BuhinTreeMaker(Of Rhac0551Vo, Rhac0530Vo)(New Rhac0551Accessor)
            End If
            Return instance0551
        End Function

#End Region



        ''' <summary>
        ''' 構成情報から、展開済みリストを返す
        ''' </summary>
        ''' <param name="koseiVos">構成情報</param>
        ''' <param name="buhinVos">部品情報</param>
        ''' <returns>展開済みリスト(n件)</returns>
        ''' <remarks></remarks>
        Public Shared Function NewSingleMatrices(ByVal koseiVos As List(Of TShisakuBuhinKouseiVo), ByVal buhinVos As List(Of TShisakuBuhinVo)) As BuhinSingleMatrix(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)()
            Return GetInstanceTShisakuBuhinKousei.NewSingleMatrices(koseiVos, buhinVos)
        End Function

        Private Shared instance0552 As BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo)
        ''' <summary>
        ''' EBom用の展開済みリスト作成クラスを返す
        ''' </summary>
        ''' <returns>唯一のインスタンス</returns>
        ''' <remarks></remarks>
        Private Shared Function GetInstanceRhac0552() As BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo)
            If instance0552 Is Nothing Then
                instance0552 = New BuhinTreeMaker(Of Rhac0552Vo, Rhac0532Vo)(New Rhac0552Accessor)
            End If
            Return instance0552
        End Function



        Private Shared instanceKousei As BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)
        ''' <summary>
        ''' 試作システム用の展開済みリスト作成クラスを返す
        ''' </summary>
        ''' <returns>唯一のインスタンス</returns>
        ''' <remarks></remarks>
        Private Shared Function GetInstanceTShisakuBuhinKousei() As BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)
            If instanceKousei Is Nothing Then
                instanceKousei = New BuhinTreeMaker(Of TShisakuBuhinKouseiVo, TShisakuBuhinVo)(New ShisakuAccessor)
            End If
            Return instanceKousei
        End Function
    End Class

    ''' <summary>
    ''' 部品情報・構成情報から展開済みリストを作成するクラス
    ''' </summary>
    ''' <typeparam name="K">構成情報の型</typeparam>
    ''' <typeparam name="B">部品情報の型</typeparam>
    ''' <remarks></remarks>
    Friend Class BuhinTreeMaker(Of K, B)

        ''' <summary>
        ''' 部品情報と構成情報のアクセッサ
        ''' </summary>
        ''' <remarks></remarks>
        Friend Interface Accessor
            ''' <summary>
            ''' 部品情報から部品番号を返す
            ''' </summary>
            ''' <param name="vo">部品情報</param>
            ''' <returns>部品番号</returns>
            ''' <remarks></remarks>
            Function GetBuhinNoFrom(ByVal vo As B) As String
            ''' <summary>
            ''' 構成情報から見出番号を返す
            ''' </summary>
            ''' <param name="vo">構成情報</param>
            ''' <returns>見出番号</returns>
            ''' <remarks></remarks>
            Function GetMidashiNoFrom(ByVal vo As K) As String
            ''' <summary>
            ''' 構成情報から見出番号種類を返す
            ''' </summary>
            ''' <param name="vo">構成情報種類</param>
            ''' <returns>見出番号</returns>
            ''' <remarks></remarks>
            Function GetMidashiNoShuruiFrom(ByVal vo As K) As String
            ''' <summary>
            ''' 構成情報から部品番号(子)を返す
            ''' </summary>
            ''' <param name="vo">構成情報</param>
            ''' <returns>部品番号(子)</returns>
            ''' <remarks></remarks>
            Function GetBuhinNoKoFrom(ByVal vo As K) As String
            ''' <summary>
            ''' 構成情報から部品番号(親)を返す
            ''' </summary>
            ''' <param name="vo">構成情報</param>
            ''' <returns>部品番号(親)</returns>
            ''' <remarks></remarks>
            Function GetBuhinNoOyaFrom(ByVal vo As K) As String
            ''' <summary>
            ''' 構成情報から員数数量を返す
            ''' </summary>
            ''' <param name="vo">構成情報</param>
            ''' <returns>員数数量</returns>
            ''' <remarks></remarks>
            Function GetInsuSuryoFrom(ByVal vo As K) As Integer?
            ''' <summary>
            ''' 構成情報に部品番号(子)を設定する
            ''' </summary>
            ''' <param name="vo">構成情報</param>
            ''' <param name="value">部品番号(子)</param>
            ''' <remarks></remarks>
            Sub SetBuhinNoKoTo(ByVal vo As K, ByVal value As String)
            ''' <summary>
            ''' 構成情報に部品番号(親)を設定する
            ''' </summary>
            ''' <param name="vo">構成情報</param>
            ''' <param name="value">部品番号(親)</param>
            ''' <remarks></remarks>
            Sub SetBuhinNoOyaTo(ByVal vo As K, ByVal value As String)
            ''' <summary>
            ''' 構成情報に員数数量を設定する
            ''' </summary>
            ''' <param name="vo">構成情報</param>
            ''' <param name="value">員数数量</param>
            ''' <remarks></remarks>
            Sub SetInsuSuryoTo(ByVal vo As K, ByVal value As Integer?)
        End Interface

        Private axsor As Accessor

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="axsor">アクセッサ</param>
        ''' <remarks></remarks>
        Friend Sub New(ByVal axsor As Accessor)
            Me.axsor = axsor
        End Sub

        ''' <summary>
        ''' BuhinNode実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class NodeImpl : Implements BuhinNode(Of K, B)
            Private anAccessor As Accessor
            Private _koseiVo As K
            Private _buhinVo As B
            Private _NodeParent As NodeImpl
            Private _NodeChildren As New List(Of BuhinNode(Of K, B))
            Private _Level As Integer
            Private _InsuSummary As Integer?

            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="anAccessor">部品情報と構成情報のアクセッサ</param>
            ''' <param name="koseiVo">構成情報</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal anAccessor As Accessor, ByVal koseiVo As K)
                Me.anAccessor = anAccessor
                Me._koseiVo = koseiVo
            End Sub

            Public Sub AddChild(ByVal child As BuhinNode(Of K, B)) Implements BuhinNode(Of K, B).AddChild
                _NodeChildren.Add(child)
            End Sub

            Public Function Clone() As BuhinNode(Of K, B) Implements BuhinNode(Of K, B).Clone
                Dim result As New NodeImpl(anAccessor, _koseiVo)
                result._NodeParent = _NodeParent
                result._NodeChildren = _NodeChildren
                result._Level = _Level
                result._InsuSummary = _InsuSummary
                Return result
            End Function

            Public ReadOnly Property BuhinNo() As String Implements BuhinNode(Of K, B).BuhinNo
                Get
                    Return anAccessor.GetBuhinNoKoFrom(_koseiVo)
                End Get
            End Property

            Public ReadOnly Property BuhinNoOya() As String Implements BuhinNode(Of K, B).BuhinNoOya
                Get
                    Return anAccessor.GetBuhinNoOyaFrom(_koseiVo)
                End Get
            End Property

            Public Property InsuSummary() As Integer? Implements BuhinNode(Of K, B).InsuSummary
                Get
                    Return _InsuSummary
                End Get
                Set(ByVal value As Integer?)
                    _InsuSummary = value
                End Set
            End Property

            Public ReadOnly Property InsuSuryo() As Integer? Implements BuhinNode(Of K, B).InsuSuryo
                Get
                    Return anAccessor.GetInsuSuryoFrom(_koseiVo)
                End Get
            End Property

            Public ReadOnly Property NodeChildrenCount() As Integer Implements BuhinNode(Of K, B).NodeChildrenCount
                Get
                    Return _NodeChildren.Count
                End Get
            End Property

            Public Property Level() As Integer Implements BuhinNode(Of K, B).Level
                Get
                    Return _Level
                End Get
                Set(ByVal value As Integer)
                    _Level = value
                End Set
            End Property

            Public ReadOnly Property NodeChildren() As ICollection(Of BuhinNode(Of K, B)) Implements BuhinNode(Of K, B).NodeChildren
                Get
                    Return _NodeChildren
                End Get
            End Property

            Public ReadOnly Property NodeChild(ByVal index As Integer) As BuhinNode(Of K, B) Implements BuhinNode(Of K, B).NodeChild
                Get
                    Return _NodeChildren(index)
                End Get
            End Property

            Public Property BuhinVo() As B Implements BuhinNode(Of K, B).BuhinVo
                Get
                    Return _buhinVo
                End Get
                Set(ByVal value As B)
                    _buhinVo = value
                End Set
            End Property

            Public Property NodeParent() As BuhinNode(Of K, B) Implements BuhinNode(Of K, B).NodeParent
                Get
                    Return _NodeParent
                End Get
                Set(ByVal value As BuhinNode(Of K, B))
                    _NodeParent = value
                End Set
            End Property

            Public Property KoseiVo() As K Implements BuhinNode(Of K, B).KoseiVo
                Get
                    Return _koseiVo
                End Get
                Set(ByVal value As K)
                    _koseiVo = value
                End Set
            End Property
        End Class

        ''' <summary>
        ''' Root用のBuhinNode実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class RootNodeImpl : Inherits NodeImpl
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="anAccessor">部品情報と構成情報のアクセッサ</param>
            ''' <param name="hinban">Root品番</param>
            ''' <param name="insuSuryo">員数数量</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal anAccessor As Accessor, ByVal hinban As String, Optional ByVal insuSuryo As Integer = 1)
                MyBase.New(anAccessor, CType(Activator.CreateInstance(GetType(K)), K))
                anAccessor.SetBuhinNoKoTo(KoseiVo, hinban)
                anAccessor.SetInsuSuryoTo(KoseiVo, insuSuryo)
                Level = 0
                InsuSummary = insuSuryo
            End Sub
        End Class

        ''' <summary>
        ''' Root品番を探索する
        ''' </summary>
        ''' <param name="aList">構成情報の一覧</param>
        ''' <returns>見つかったRoot品番たち</returns>
        ''' <remarks></remarks>
        Public Function DetectRoots(ByVal aList As List(Of K)) As String()
            Dim vosByBuhinNoKo As New Dictionary(Of String, List(Of K))
            For Each vo As K In aList
                Dim buhinNoKo As String = axsor.GetBuhinNoKoFrom(vo)
                If Not vosByBuhinNoKo.ContainsKey(buhinNoKo) Then
                    vosByBuhinNoKo.Add(buhinNoKo, New List(Of K))
                End If
                vosByBuhinNoKo(buhinNoKo).Add(vo)
            Next

            Dim results As New List(Of String)
            Dim vos As New Dictionary(Of String, Integer)
            DetectRootsMain(aList, vosByBuhinNoKo, vos, results)
            Return results.ToArray
        End Function

        Private Sub DetectRootsMain(ByVal detectingList As List(Of K), _
                                      ByVal vosByBuhinNoKo As Dictionary(Of String, List(Of K)), _
                                      ByVal vos As Dictionary(Of String, Integer), _
                                      ByVal results As List(Of String))

            For Each vo As K In detectingList
                Dim buhinNoOya As String = axsor.GetBuhinNoOyaFrom(vo)
                If vos.ContainsKey(buhinNoOya) Then
                    Continue For
                End If
                vos.Add(buhinNoOya, 0)
                If Not vosByBuhinNoKo.ContainsKey(buhinNoOya) Then
                    results.Add(buhinNoOya)
                    Continue For
                End If
                DetectRootsMain(vosByBuhinNoKo(buhinNoOya), vosByBuhinNoKo, vos, results)
            Next
        End Sub

        ' TODO  Nodeを返すように変更したら、NewBuhinNodeとコラボ
        ''' <summary>
        ''' 構成情報から、Tree構造を返す
        ''' </summary>
        ''' <param name="koseiVos">構成情報</param>
        ''' <returns>Tree構造(n件)</returns>
        ''' <remarks></remarks>
        Public Function NewRootBuhinNodes(ByVal koseiVos As List(Of K)) As BuhinNode(Of K, B)()
            If koseiVos Is Nothing OrElse 0 = koseiVos.Count Then
                Throw New ArgumentException("構成情報がありません.", "koseiVos")
            End If

            TrimEndBuhinNoInKosei(koseiVos)

            ' 前提条件
            ' 親子だけをキーとする（構成改訂№は一意になっていること）

            ' n件のrootを導き出す
            Dim rootBuhinNos As String() = DetectRoots(koseiVos)

            ' vo.親に、voをぶら下げる。その時、vo.子が同じ、vo同士は除外しておく。
            Dim vosByOya As New Dictionary(Of String, List(Of K))
            For Each vo As K In koseiVos
                Dim buhinNoOya As String = axsor.GetBuhinNoOyaFrom(vo)
                If Not vosByOya.ContainsKey(buhinNoOya) Then
                    vosByOya.Add(buhinNoOya, New List(Of K))
                End If
                If ExistSameBuhinNoKo(vo, vosByOya(buhinNoOya)) Then
                    Continue For
                End If
                vosByOya(buhinNoOya).Add(vo)
            Next

            Dim results As New List(Of BuhinNode(Of K, B))
            ' rootから辿っていく。この時、voは一意で、Nodeのインスタンスは毎回新しくする。
            For Each rootBuhinNo As String In rootBuhinNos
                Dim root As New RootNodeImpl(axsor, rootBuhinNo)
                MakeFromParent(vosByOya, root)
                results.Add(root)
            Next
            Return results.ToArray
        End Function

        ''' <summary>
        ''' 構成情報の部品番号から末尾空白を取り除く
        ''' </summary>
        ''' <param name="koseiVos"></param>
        ''' <remarks></remarks>
        Private Sub TrimEndBuhinNoInKosei(ByVal koseiVos As List(Of K))

            For Each vo As K In koseiVos
                If axsor.GetBuhinNoOyaFrom(vo) IsNot Nothing Then
                    axsor.SetBuhinNoOyaTo(vo, axsor.GetBuhinNoOyaFrom(vo).TrimEnd)
                End If
                If axsor.GetBuhinNoKoFrom(vo) IsNot Nothing Then
                    axsor.SetBuhinNoKoTo(vo, axsor.GetBuhinNoKoFrom(vo).TrimEnd)
                End If
            Next
        End Sub

        Public Function NewBuhinNode(ByVal fHinban As String, ByVal koseiVos As List(Of K), ByVal buhinVos As List(Of B)) As BuhinNode(Of K, B)()
            Return Nothing
        End Function

        Private Sub MakeFromParent(ByVal vosByOya As Dictionary(Of String, List(Of K)), ByVal aNode As BuhinNode(Of K, B))
            If Not vosByOya.ContainsKey(aNode.BuhinNo) Then
                Return
            End If
            ' voは一意で、Nodeのインスタンスは毎回新しくする。
            For Each vo As K In vosByOya(aNode.BuhinNo)
                Dim child As New NodeImpl(axsor, vo)
                aNode.AddChild(child)
                MakeFromParent(vosByOya, child)
            Next
        End Sub

        ''' <summary>
        ''' 同じ部品番号(子)をもつVoが存在するかを返す
        ''' </summary>
        ''' <param name="vo">検査するvo値</param>
        ''' <param name="vos">探索するlist</param>
        ''' <returns>存在する場合、true</returns>
        ''' <remarks></remarks>
        Private Function ExistSameBuhinNoKo(ByVal vo As K, ByVal vos As List(Of K)) As Boolean

            For Each child As K In vos
                If axsor.GetBuhinNoKoFrom(child).Equals(axsor.GetBuhinNoKoFrom(vo)) Then
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' 部品情報を、Nodeとその子供たち全体に設定する
        ''' </summary>
        ''' <param name="buhinVos">部品情報</param>
        ''' <param name="structuredRoots">Tree構造済みのNodeたち</param>
        ''' <remarks></remarks>
        Public Sub SetBuhinTo(ByVal buhinVos As List(Of B), ByVal ParamArray structuredRoots As BuhinNode(Of K, B)())
            Dim voByBuhinNo As New Dictionary(Of String, B)
            For Each vo As B In buhinVos
                If vo Is Nothing Then
                    Continue For
                End If
                If voByBuhinNo.ContainsKey(axsor.GetBuhinNoFrom(vo)) Then
                    Continue For
                End If
                voByBuhinNo.Add(axsor.GetBuhinNoFrom(vo), vo)
            Next

            ' rootから辿っていく。この時、voは一意で、Nodeのインスタンスは毎回新しくする。
            ResolveBuhinVo(voByBuhinNo, structuredRoots)
        End Sub

        ''' <summary>
        ''' 部品情報を、Nodeに設定する
        ''' </summary>
        ''' <param name="buhinVoByNo">部品番号で部品情報を返すDictionary</param>
        ''' <param name="nodes">Node</param>
        ''' <remarks></remarks>
        Private Sub ResolveBuhinVo(ByVal buhinVoByNo As Dictionary(Of String, B), ByVal nodes As ICollection(Of BuhinNode(Of K, B)))
            For Each node As BuhinNode(Of K, B) In nodes

                If Not buhinVoByNo.ContainsKey(node.BuhinNo) Then
                    '設計展開できないのでエラーは返さない 樺澤 '
                    Return
                End If
                node.BuhinVo = buhinVoByNo(node.BuhinNo)
                ResolveBuhinVo(buhinVoByNo, node.NodeChildren)
            Next
        End Sub

        ''' <summary>
        ''' 構成情報を見出番号・見出番号種類でソートするIComparer実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class MidashiNoAndShuruiComparer : Implements IComparer(Of BuhinNode(Of K, B))
            Private ReadOnly anAccessor As Accessor
            Public Sub New(ByVal anAccessor As Accessor)
                Me.anAccessor = anAccessor
            End Sub

            Public Function Compare(ByVal x As BuhinNode(Of K, B), ByVal y As BuhinNode(Of K, B)) As Integer Implements IComparer(Of BuhinNode(Of K, B)).Compare
                Dim result1 As Integer? = EzUtil.CompareNullsLast(x.KoseiVo, y.KoseiVo)
                If result1 IsNot Nothing Then
                    Return CInt(result1)
                End If

                Dim result2 As Integer = CompareString(anAccessor.GetMidashiNoFrom(x.KoseiVo), anAccessor.GetMidashiNoFrom(y.KoseiVo))
                If result2 <> 0 Then
                    Return result2
                End If

                Dim result3 As Integer = CompareString(anAccessor.GetMidashiNoShuruiFrom(x.KoseiVo), anAccessor.GetMidashiNoShuruiFrom(y.KoseiVo))
                If result3 <> 0 Then
                    Return result3
                End If

                Return anAccessor.GetBuhinNoKoFrom(x.KoseiVo).CompareTo(anAccessor.GetBuhinNoKoFrom(y.KoseiVo))
            End Function

            Private Function CompareString(ByVal valueX As String, ByVal valueY As String) As Integer

                Dim result As Integer? = EzUtil.CompareNullsLast(valueX, valueY)
                If result IsNot Nothing Then
                    Return CInt(result)
                End If

                Return valueX.CompareTo(valueY)
            End Function
        End Class

        Private aMidashiNoComparer As MidashiNoAndShuruiComparer
        ''' <summary>
        ''' 当インスタンス内で一意となる「見出番号・見出番号種類でソート実装クラス」を返す
        ''' </summary>
        ''' <returns>見出番号・見出番号種類でソート実装クラス</returns>
        ''' <remarks></remarks>
        Private Function GetMidashiNoComparer() As MidashiNoAndShuruiComparer
            If aMidashiNoComparer Is Nothing Then
                aMidashiNoComparer = New MidashiNoAndShuruiComparer(axsor)
            End If
            Return aMidashiNoComparer
        End Function

        ''' <summary>
        ''' 展開済みリストを作成して返す
        ''' </summary>
        ''' <param name="structuredRoot">Tree構造済み親Node</param>
        ''' <param name="level">親Nodeのレベル</param>
        ''' <returns>展開済みリスト</returns>
        ''' <remarks></remarks>
        Public Function MakeExpandedList(ByVal structuredRoot As BuhinNode(Of K, B), Optional ByVal level As Integer = 0) As List(Of BuhinNode(Of K, B))
            Dim nodes As New List(Of BuhinNode(Of K, B))
            Dim oyaLevel As Integer = level - 1    ' rootの親は、Lv.-1という事で.
            MakeExpandedList(structuredRoot, oyaLevel, 1, nodes)

            Return nodes
        End Function

        ''' <summary>
        ''' 展開済みリストを作成する
        ''' </summary>
        ''' <param name="structuredNode">Tree構造済みのNode</param>
        ''' <param name="oyaLevel">structuredNodeの親レベル</param>
        ''' <param name="oyaInsu">structuredNodeの親員数</param>
        ''' <param name="result">展開済みリスト</param>
        ''' <remarks></remarks>
        Private Sub MakeExpandedList(ByVal structuredNode As BuhinNode(Of K, B), ByVal oyaLevel As Integer, ByVal oyaInsu As Integer, ByVal result As List(Of BuhinNode(Of K, B)))
            result.Add(structuredNode)
            structuredNode.Level = oyaLevel + 1
            structuredNode.InsuSummary = oyaInsu * structuredNode.InsuSuryo
            Dim nodeChildren As New List(Of BuhinNode(Of K, B))(structuredNode.NodeChildren)
            nodeChildren.Sort(GetMidashiNoComparer)
            For Each child As BuhinNode(Of K, B) In nodeChildren
                MakeExpandedList(child, structuredNode.Level, structuredNode.InsuSummary, result)
            Next
        End Sub

        ''' <summary>
        ''' 構成情報から、展開済みリストを返す
        ''' </summary>
        ''' <param name="koseiVos">構成情報</param>
        ''' <param name="buhinVos">部品情報</param>
        ''' <returns>展開済みリスト(n件)</returns>
        ''' <remarks></remarks>
        Public Function NewSingleMatrices(ByVal koseiVos As List(Of K), ByVal buhinVos As List(Of B)) As BuhinSingleMatrix(Of K, B)()

            Dim roots As BuhinNode(Of K, B)() = NewRootBuhinNodes(koseiVos)

            SetBuhinTo(buhinVos, roots)

            Dim results As New List(Of BuhinSingleMatrix(Of K, B))
            For Each root As BuhinNode(Of K, B) In roots
                results.Add(New BuhinSingleMatrix(Of K, B)(root, MakeExpandedList(root)))
            Next
            Return results.ToArray
        End Function
    End Class
End Namespace