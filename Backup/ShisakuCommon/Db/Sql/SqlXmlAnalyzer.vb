Imports System.Xml

Namespace Db.Sql
    ''' <summary>
    ''' XML化したSQLを解析するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SqlXmlAnalyzer
        Private ReadOnly xmlSql As String
        Private ReadOnly bindParamValue As Object
        Private ReadOnly evaluator As ISqlExpressionEvaluator
        Private _AnalyzedSql As String
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="xmlSql">XML化したSQL</param>
        ''' <param name="bindParamValue">埋め込みパラメータ値</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal xmlSql As String, ByVal bindParamValue As Object, ByVal evaluator As ISqlExpressionEvaluator)
            Me.xmlSql = xmlSql
            Me.bindParamValue = bindParamValue
            Me.evaluator = evaluator
        End Sub
        ''' <summary>解析したSQL結果</summary>
        Public ReadOnly Property AnalyzedSql() As String
            Get
                Return _AnalyzedSql
            End Get
        End Property

        ''' <summary>
        ''' 解析する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Analyze()
            Dim doc As New XmlDocument
            doc.LoadXml(xmlSql)
            Dim results As New List(Of String)
            Analyze(doc.ChildNodes(0), results)
            _AnalyzedSql = Join(results.ToArray)
        End Sub

        ''sql where if set
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="node"></param>
        ''' <param name="results"></param>
        ''' <remarks></remarks>
        Private Sub Analyze(ByVal node As XmlNode, ByVal results As List(Of String))
            For Each childNode As XmlNode In node
                If childNode.Name.ToLower = "where" Then
                    Dim whereResults As New List(Of String)
                    Analyze(childNode, whereResults)
                    If 0 < whereResults.Count Then
                        results.Add("WHERE " & JoinWhereParts(whereResults))
                    End If
                ElseIf childNode.Name.ToLower = "set" Then
                    Dim setResults As New List(Of String)
                    Analyze(childNode, setResults)
                    If 0 < setResults.Count Then
                        results.Add("SET " & JoinSetParts(setResults))
                    End If
                ElseIf childNode.Name.ToLower = "if" Then
                    If EvaluateIf(childNode) Then
                        Analyze(childNode, results)
                    End If
                ElseIf childNode.Name.ToLower = "#cdata-section" Then
                    results.Add(childNode.Value.Trim)
                ElseIf childNode.Name.ToLower = "#text" Then
                    results.Add(childNode.Value.Trim)
                Else
                    Throw New NotSupportedException(childNode.Name & " は未対応です.")
                End If
            Next
        End Sub

        ''' <summary>
        ''' ifタグの条件を評価して返す
        ''' </summary>
        ''' <param name="node">ifのXmlNode</param>
        ''' <returns>評価した結果</returns>
        ''' <remarks></remarks>
        Private Function EvaluateIf(ByVal node As XmlNode) As Boolean
            For Each attrNode As XmlNode In node.Attributes
                If attrNode.Name.ToLower = "test" Then
                    Return evaluator.Evaluate(attrNode.Value, bindParamValue)
                Else
                    Throw New NotSupportedException("<if>の属性 " & attrNode.Name & " は未対応です.")
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' Set句のカンマ区切りを考慮して結合して返す
        ''' </summary>
        ''' <param name="setParts">パート毎に分かれたSet句</param>
        ''' <returns>結合したSet句</returns>
        ''' <remarks></remarks>
        Private Function JoinSetParts(ByVal setParts As List(Of String)) As String
            Dim count As Integer = setParts.Count
            If 0 < count Then
                If 0 < setParts(0).Length AndAlso setParts(0).StartsWith(",") Then
                    setParts(0) = setParts(0).Substring(1).Trim
                End If
                If 0 < setParts(count - 1).Length AndAlso setParts(count - 1).EndsWith(",") Then
                    setParts(count - 1) = setParts(count - 1).Substring(0, setParts(count - 1).Length - 1).Trim
                End If
            End If
            Return Join(setParts.ToArray)
        End Function
        ''' <summary>
        ''' 論理演算子から始まっているかを返す
        ''' </summary>
        ''' <param name="str">判定文字列</param>
        ''' <param name="logicalStr">論理演算子</param>
        ''' <returns>始まっている場合、true</returns>
        ''' <remarks></remarks>
        Private Function StartsWithLogical(ByVal str As String, ByVal logicalStr As String) As Boolean
            Return logicalStr.Length < str.Length AndAlso str.ToLower.StartsWith(logicalStr.ToLower) AndAlso IsLogicalNextChar(str.Substring(logicalStr.Length, 1))
        End Function
        ''' <summary>
        ''' 論理演算子の次に来るべき文字かを返す
        ''' </summary>
        ''' <param name="c">判定文字</param>
        ''' <returns>次の文字なら、true</returns>
        ''' <remarks></remarks>
        Private Function IsLogicalNextChar(ByVal c As Char) As Boolean
            Return 0 <= (" (" & vbCrLf & vbTab).IndexOf(c)
        End Function
        ''' <summary>
        ''' 論理演算子で終わっているかを返す
        ''' </summary>
        ''' <param name="str">判定文字列</param>
        ''' <param name="logicalStr">論理演算子</param>
        ''' <returns>終わっている場合、true</returns>
        ''' <remarks></remarks>
        Private Function EndsWithLogical(ByVal str As String, ByVal logicalStr As String) As Boolean
            Return logicalStr.Length < str.Length AndAlso str.ToLower.EndsWith(logicalStr.ToLower) AndAlso IsLoicalPrevChar(str.Substring(str.Length - logicalStr.Length - 1, 1))
        End Function
        ''' <summary>
        ''' 論理演算子の前に来るべき文字かを返す
        ''' </summary>
        ''' <param name="c">判定文字</param>
        ''' <returns>前の文字なら、true</returns>
        ''' <remarks></remarks>
        Private Function IsLoicalPrevChar(ByVal c As Char) As Boolean
            Return 0 <= (" )" & vbCrLf & vbTab).IndexOf(c)
        End Function
        ''' <summary>
        ''' Where句のAnd,Or区切りを考慮して、結合して返す
        ''' </summary>
        ''' <param name="whereParts">パート毎に分かれたWhere句の</param>
        ''' <returns>結合したWhere句</returns>
        ''' <remarks></remarks>
        Private Function JoinWhereParts(ByVal whereParts As List(Of String)) As String
            Dim count As Integer = whereParts.Count
            If 0 < count Then
                If StartsWithLogical(whereParts(0), "AND") Then
                    whereParts(0) = whereParts(0).Substring(3).Trim
                ElseIf StartsWithLogical(whereParts(0), "OR") Then
                    whereParts(0) = whereParts(0).Substring(2).Trim
                End If
                If EndsWithLogical(whereParts(count - 1), "AND") Then
                    whereParts(count - 1) = whereParts(count - 1).Substring(0, whereParts(count - 1).Length - 3).Trim
                ElseIf EndsWithLogical(whereParts(count - 1), "OR") Then
                    whereParts(count - 1) = whereParts(count - 1).Substring(0, whereParts(count - 1).Length - 2).Trim
                End If
            End If
            Return Join(whereParts.ToArray)
        End Function
    End Class
End Namespace