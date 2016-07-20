Public Class EzUtil
    ''' <summary>
    ''' C/Java等の i++ を実現する
    ''' </summary>
    ''' <param name="i">i++する値</param>
    ''' <returns>i の値</returns>
    ''' <remarks></remarks>
    Public Shared Function Increment(ByRef i As Integer) As Integer
        Dim result As Integer = i
        i += 1
        Return result
    End Function

    ''' <summary>
    ''' Null値を考慮して同値かを返す
    ''' </summary>
    ''' <param name="a">値a</param>
    ''' <param name="b">値b</param>
    ''' <returns>Null同士、または同値ならtrue</returns>
    ''' <remarks></remarks>
    Public Shared Function IsEqualIfNull(ByVal a As Object, ByVal b As Object) As Boolean
        If a Is Nothing AndAlso b Is Nothing Then
            Return True
        ElseIf a Is Nothing OrElse b Is Nothing Then
            Return False
        End If
        Return a.Equals(b)
    End Function

    ''' <summary>
    ''' 値(null含む)が違うかを返す
    ''' </summary>
    ''' <param name="x">値x</param>
    ''' <param name="y">値y</param>
    ''' <returns>違う場合、true</returns>
    ''' <remarks></remarks>
    Public Shared Function IsNotEqualIfNull(ByVal x As Object, ByVal y As Object) As Boolean
        If x Is Nothing And y Is Nothing Then
            Return False
        ElseIf x Is Nothing Then
            Return True
        ElseIf y Is Nothing Then
            Return True
        End If
        Return Not x.Equals(y)
    End Function

    ''' <summary>
    ''' Empty値を含んでいるかを返す
    ''' </summary>
    ''' <param name="values">値[]</param>
    ''' <returns>含んでいる場合、true</returns>
    ''' <remarks></remarks>
    Public Shared Function ContainsEmpty(ByVal ParamArray values() As Object) As Boolean
        For Each obj As Object In values
            If TypeOf obj Is String Then
                If StringUtil.IsEmpty(Convert.ToString(obj)) Then
                    Return True
                End If
            ElseIf obj Is Nothing Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Nullを含んでいるかを返す
    ''' </summary>
    ''' <param name="values">値[]</param>
    ''' <returns>含んでいる場合、true</returns>
    ''' <remarks></remarks>
    Public Shared Function ContainsNull(ByVal ParamArray values As Object()) As Boolean
        For Each value As String In values
            If value Is Nothing Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Const ALPHABET_TABLE As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    ''' <summary>
    ''' 番号(0 start)をAlphabet表記にする
    ''' </summary>
    ''' <param name="index">0から始まる番号</param>
    ''' <returns>"A"とか"BB"とか</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvIndexToAlphabet(ByVal index) As String
        Dim chars As Char() = ALPHABET_TABLE.ToCharArray
        Dim value As Integer = index
        Dim len As Integer = chars.Length

        Dim result As String = ""

        value += 1
        Do
            Dim amari As Integer = (value - 1) Mod len
            result = chars(amari) & result
            value = Math.Truncate((value - 1) / len)
        Loop While 0 < value
        Return result
    End Function

    ''' <summary>
    ''' 指定キーの組み合わせで一意になる値を作成する
    ''' </summary>
    ''' <param name="keys">キー()</param>
    ''' <returns>一意のキー</returns>
    ''' <remarks></remarks>
    Public Shared Function MakeKey(ByVal ParamArray keys As String()) As String
        Return Join(keys, ";'"":")
    End Function
    ''' <summary>
    ''' IComparer(Of T)実装用に、Null値だったら最後にSortする値を返す
    ''' </summary>
    ''' <param name="x">値x</param>
    ''' <param name="y">値y</param>
    ''' <returns>Null値だったら最後にSortする値. それ以外は、Nothing</returns>
    ''' <remarks></remarks>
    Public Shared Function CompareNullsLast(ByVal x As Object, ByVal y As Object) As Integer?
        If x Is Nothing AndAlso y Is Nothing Then
            Return 0
        ElseIf x Is Nothing Then
            Return -1
        ElseIf y Is Nothing Then
            Return 1
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' パラメータがnullで無い事を保証する(nullの場合、例外を投げる)
    ''' </summary>
    ''' <param name="parameter">引数</param>
    ''' <param name="name">引数名</param>
    ''' <remarks></remarks>
    Public Shared Sub AssertParameterIsNotNull(ByVal parameter As Object, ByVal name As String)
        If parameter Is Nothing Then
            Throw New ArgumentNullException(name)
        End If
    End Sub

    ''' <summary>
    ''' コレクション同士をマージする
    ''' </summary>
    ''' <param name="a">コレクションA</param>
    ''' <param name="b">コレクションB</param>
    ''' <returns>マージした結果配列（コレクション）</returns>
    ''' <remarks></remarks>
    Public Shared Function MergeCollection(ByVal a As ICollection(Of Integer), ByVal b As ICollection(Of Integer)) As Integer()
        Dim result As New List(Of Integer)(a)
        For Each i As Integer In b
            If result.Contains(i) Then
                Continue For
            End If
            result.Add(i)
        Next
        Return result.ToArray
    End Function

End Class
