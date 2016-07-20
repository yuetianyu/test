Imports System.Reflection

Namespace Db.Sql
    Public Class Expression
        Private Class EvaluateFailure
        End Class

        Private Shared ReadOnly BIND_PARAM_VALUE_FAILURE As New EvaluateFailure

        Private param As Object
        Private term As String

        Private expressionA As Expression
        Private expressionB As Expression
        Private expressionC As Expression
        Private Sub New(ByVal param As Object, ByVal term As String)
            Me.term = term
            Me.param = param
        End Sub
        Public Sub New(ByVal param As Object, ByVal term1 As String, ByVal term2 As String, ByVal term3 As String)
            Me.New(New Expression(param, term1), New Expression(param, term2), New Expression(param, term3))
        End Sub
        Public Sub New(ByVal value1 As Expression, ByVal value2 As Expression, ByVal value3 As Expression)
            Me.expressionA = value1
            Me.expressionB = value2
            Me.expressionC = value3
            If value2.CanEvaluate Then
                Throw New ArgumentException(String.Format("'{0} {1} {2}' の式は評価出来ません", value1.term, value2.term, value3.term))
            End If
        End Sub
        Public Shared Function NewInstance(ByVal param As Object, ByVal terms As List(Of String)) As Expression
            Dim sikis As New List(Of Expression)
            For Each term As String In terms
                sikis.Add(New Expression(param, term))
            Next
            Return CollapseSiki(sikis)
        End Function
        Private Shared Function CollapseSiki(ByVal sikis As List(Of Expression)) As Expression
            If sikis.Count = 1 Then
                Return sikis(0)
            End If
            Dim argList As New List(Of Expression)
            Dim start As Integer = 0

            If IsLogical(sikis(start + 1)) Then
                If start + 3 < sikis.Count AndAlso Not IsLogical(sikis(start + 3)) Then
                    argList.Add(sikis(start))
                    argList.Add(sikis(start + 1))
                    start += 2
                End If
            End If
            If start + 2 < sikis.Count Then
                argList.Add(New Expression(sikis(start), sikis(start + 1), sikis(start + 2)))
                start += 3
            Else
                Throw New ArgumentException()
            End If
            For i As Integer = start To sikis.Count - 1
                argList.Add(sikis(i))
            Next
            Return CollapseSiki(argList)
        End Function
        Private Shared Function IsLogical(ByVal aExpression As Expression) As Boolean
            Dim term As String = aExpression.term.ToLower
            Return "and".Equals(term) Or "or".Equals(term)
        End Function
        Private Function CanEvaluate() As Boolean
            Return term = Nothing
        End Function
        Private ReadOnly Property Value() As Object
            Get
                If CanEvaluate() Then
                    Return Evaluate()
                ElseIf term.StartsWith("@") Then
                    If term.Equals("@Value") Then
                        If param Is Nothing OrElse param.GetType.IsValueType OrElse TypeOf param Is String Then
                            Return param
                        End If
                    End If
                    If param Is Nothing Then
                        Return BIND_PARAM_VALUE_FAILURE
                    End If
                    Dim aProperty As PropertyInfo = param.GetType.GetProperty(term.Substring(1))
                    If aProperty Is Nothing Then
                        Throw New ArgumentException("パラメータ名 " & term & " に相当するプロパティは、パラメータ値 " & param.GetType.Name & " に無い.")
                    End If
                    Return aProperty.GetValue(param, Nothing)
                End If
                If "true".Equals(term.ToLower) Then
                    Return True
                ElseIf "false".Equals(term.ToLower) Then
                    Return False
                ElseIf "null".Equals(term.ToLower) Then
                    Return Nothing
                End If
                Return term
            End Get
        End Property

        Public Function Evaluate() As Boolean
            If Not CanEvaluate() Then
                Dim result As Object = Value
                If TypeOf Value Is Boolean Then
                    Return Value
                Else
                    Throw New ArgumentException(term & " は評価出来ません.")
                End If
            End If

            Dim value2 As String = expressionB.Value.ToString
            If "and".Equals(value2.ToLower) Then
                Dim aValue As Object = expressionA.Value
                If aValue = False Then
                    Return False
                End If
                Return aValue And expressionC.Value
            ElseIf "or".Equals(value2.ToLower) Then
                Dim aValue As Object = expressionA.Value
                If aValue = True Then
                    Return True
                End If
                Return aValue Or expressionC.Value
            End If

            If Not "!=".Equals(value2) And Not "=".Equals(value2) Then
                Throw New NotSupportedException("演算子 " & value2 & " は未対応.")
            End If
            Dim leftValue As Object = expressionA.Value
            If leftValue Is BIND_PARAM_VALUE_FAILURE Then
                Return False
            End If
            Dim rightValue As Object = expressionC.Value
            If rightValue Is BIND_PARAM_VALUE_FAILURE Then
                Return False
            End If

            Dim values() As Object = {leftValue, rightValue}
            For i As Integer = 0 To 1
                If values(i) Is Nothing Then
                    If "!=".Equals(value2) Then
                        Return values(1 - i) IsNot Nothing
                    ElseIf "=".Equals(value2) Then
                        Return values(1 - i) Is Nothing
                    End If
                End If
            Next
            If "!=".Equals(value2) Then
                Return Not leftValue.ToString.Equals(rightValue.ToString)
            ElseIf "=".Equals(value2) Then
                Return leftValue.ToString.Equals(rightValue.ToString)
            End If
            Throw New NotSupportedException(String.Format("'{0} {1} {2}' この式は評価出来ません.", leftValue, value2, rightValue))
        End Function
    End Class
End Namespace