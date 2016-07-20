﻿Imports System.Reflection

Namespace Db.Sql
    ''' <summary>
    ''' 式を評価するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SqlExpressionEvaluator : Implements ISqlExpressionEvaluator

        Private _result As Nullable(Of Boolean)
        ''' <summary>直前の評価結果</summary>
        ''' <returns>直前の評価結果</returns>
        Public ReadOnly Property Result() As Nullable(Of Boolean)
            Get
                Return _result
            End Get
        End Property

        Private Enum Kind
            Kigo
            Whitespace
            Moji
        End Enum

        ''' <summary>
        ''' Whitespaceかを返す
        ''' </summary>
        ''' <param name="c">判定文字</param>
        ''' <returns>Whitespaceの場合、true</returns>
        ''' <remarks></remarks>
        Private Shared Function IsWhitespace(ByVal c As Char) As Boolean
            Return 0 <= (" " & vbCrLf & vbTab).IndexOf(c)
        End Function
        ''' <summary>
        ''' 記号かを返す
        ''' </summary>
        ''' <param name="c">判定文字</param>
        ''' <returns>記号の場合、true</returns>
        ''' <remarks></remarks>
        Private Shared Function IsKigo(ByVal c As Char) As Boolean
            Return 0 <= "!=<>".IndexOf(c)
        End Function
        ''' <summary>
        ''' WhitespaceをTrimする
        ''' </summary>
        ''' <param name="str">文字列</param>
        ''' <returns>Trim後の文字列</returns>
        ''' <remarks></remarks>
        Private Shared Function TrimWhitespace(ByVal str As String) As String
            Dim chars As Char() = str.ToCharArray
            Dim result As String = String.Empty
            For Each c As Char In chars
                If Not IsWhitespace(c) Then
                    result += c
                End If
            Next
            Return result
        End Function

        ''' <summary>
        ''' 数式を変数名や等号不等号で区切った List にして返す
        ''' </summary>
        ''' <returns>区切った List</returns>
        ''' <remarks></remarks>
        Public Shared Function Bunkai(ByVal expression As String) As List(Of String)
            Dim drafts As New List(Of String)
            Dim index As Integer = -1
            Dim chars As Char() = expression.ToCharArray
            Dim lastKind As Nullable(Of Kind)
            For Each c As Char In chars
                Dim aKind As Kind = Kind.Moji
                If IsWhitespace(c) Then
                    aKind = Kind.Whitespace
                ElseIf IsKigo(c) Then
                    aKind = Kind.Kigo
                End If
                If lastKind Is Nothing OrElse aKind <> lastKind Then
                    drafts.Add(c)
                    index += 1
                Else
                    drafts(index) += c
                End If
                lastKind = aKind
            Next
            Dim results As New List(Of String)
            For Each result As String In drafts
                result = TrimWhitespace(result)
                If 0 < result.Length Then
                    results.Add(result)
                End If
            Next
            Return results
        End Function

        ''' <summary>
        ''' 数式を評価して結果を返す
        ''' </summary>
        ''' <returns>評価結果</returns>
        ''' <remarks></remarks>
        Public Function Evaluate(ByVal expressionStr As String, ByVal param As Object) As Boolean Implements ISqlExpressionEvaluator.Evaluate

            Dim expressions As List(Of String) = Bunkai(expressionStr)
            Dim aExpression As Expression = Expression.NewInstance(param, expressions)
            Return aExpression.Evaluate
        End Function
    End Class
End Namespace