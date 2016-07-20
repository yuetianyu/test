
Namespace Util
    ''' <summary>
    ''' indexと情報を紐付けたクラス
    ''' </summary>
    ''' <typeparam name="T">情報の型</typeparam>
    ''' <remarks></remarks>
    Public Class IndexedList(Of T)
        Private _Records As New Dictionary(Of Integer, T)
        Private IsGenericValueType As Boolean
        ''' <summary>インスタンスを自動生成するか？を保持</summary>
        Public IsCreateGeneric As Boolean
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(Not GetType(T).IsInterface)
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="IsCreateGeneric">インスタンスを自動生成する場合、true(※既定値true)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal IsCreateGeneric As Boolean)
            Me.IsCreateGeneric = IsCreateGeneric
            IsGenericValueType = GetType(T).IsValueType

            If GetType(T) Is GetType(String) Then
                Throw New NotSupportedException("String型は未対応")
            End If
        End Sub

        ''' <summary>情報</summary>
        ''' <param name="index">index</param>
        ''' <value>情報</value>
        ''' <returns>情報</returns>
        ''' <remarks></remarks>
        Default Public Property Value(ByVal index As Integer) As T
            Get
                If Not _Records.ContainsKey(index) Then
                    If Not IsCreateGeneric Then
                        Return Nothing
                    End If
                    _Records.Add(index, CType(Activator.CreateInstance(GetType(T)), T))
                End If
                Return _Records(index)
            End Get
            Set(ByVal value As T)
                If _Records.ContainsKey(index) Then
                    _Records.Remove(index)
                End If
                _Records.Add(index, value)
            End Set
        End Property
        ''' <summary>情報の一覧</summary>
        ''' <returns>情報の一覧</returns>
        Public ReadOnly Property Values() As ICollection(Of T)
            Get
                Return _Records.Values
            End Get
        End Property
        ''' <summary>indexの一覧</summary>
        ''' <returns>indexの一覧</returns>
        Public ReadOnly Property Keys() As ICollection(Of Integer)
            Get
                Return _Records.Keys
            End Get
        End Property

        ''' <summary>
        ''' 情報を追加する
        ''' </summary>
        ''' <param name="index">index</param>
        ''' <param name="value">情報</param>
        ''' <remarks></remarks>
        Public Sub Add(ByVal index As Integer, ByVal value As T)
            _Records.Add(index, value)
        End Sub

        ''' <summary>
        ''' 情報をすべてクリアする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Clear()
            _Records.Clear()
        End Sub

        ''' <summary>
        ''' 情報を挿入する
        ''' </summary>
        ''' <param name="index">挿入index</param>
        ''' <param name="count">挿入数</param>
        ''' <remarks></remarks>
        Public Sub Insert(ByVal index As Integer, Optional ByVal count As Integer = 1)
            Dim newRecords As New Dictionary(Of Integer, T)
            For Each key As Integer In _Records.Keys
                newRecords.Add(Convert.ToInt32(IIf(key < index, key, key + count)), _Records(key))
            Next
            _Records = newRecords
        End Sub

        ''' <summary>
        ''' 情報を除去する
        ''' </summary>
        ''' <param name="index">除去index</param>
        ''' <param name="count">除去数</param>
        ''' <remarks></remarks>
        Public Sub Remove(ByVal index As Integer, Optional ByVal count As Integer = 1)
            Dim newRecords As New Dictionary(Of Integer, T)
            For Each key As Integer In _Records.Keys
                If index <= key AndAlso key < index + count Then
                    Continue For
                End If
                newRecords.Add(Convert.ToInt32(IIf(key < index, key, key - count)), _Records(key))
            Next
            _Records = newRecords
        End Sub

    End Class
End Namespace