Imports System.Reflection
Imports EBom.Data

Namespace Db.Sql
    ''' <summary>
    ''' "@Hoge"というバインド文字列に該当するプロパティ値を抽出する
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SqlBindAnalyzer
        ''' <summary>Int32/DateTime/Stringなどがパラメータだった時の、埋め込みパラメータ名</summary>
        Public Const VALUE_BIND_PARAM_NAME As String = "@Value"
        Private sql As String
        Private bindParamValue As Object
        Private parameter As New ShisakuDbParameter
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="sql">SQL(バインド文字有)</param>
        ''' <param name="bindParamValue">バインドする値、もしくはプロパティ値としてもつ値</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal sql As String, ByVal bindParamValue As Object)
            Me.sql = sql
            Me.bindParamValue = bindParamValue
        End Sub
        ''' <summary>
        ''' プロパティ名の文字か？を返す
        ''' </summary>
        ''' <param name="c">文字</param>
        ''' <returns>プロパティ名ならtrue</returns>
        ''' <remarks></remarks>
        Private Function IsPropertyNameChar(ByVal c As Char) As Boolean
            Return ("a" <= c AndAlso c <= "z") OrElse ("A" <= c AndAlso c <= "Z") OrElse ("0" <= c AndAlso c <= "9") OrElse 0 <= "_".IndexOf(c)
        End Function
        ''' <summary>
        ''' "@Value"に埋め込むパラメータ型かどうかを返す
        ''' </summary>
        ''' <returns>該当するパラメータ型なら、true</returns>
        ''' <remarks></remarks>
        Private Function IsValueBindParameter()
            If bindParamValue Is Nothing Then
                Return False
            End If
            Dim paramType As Type = bindParamValue.GetType
            Return paramType.IsValueType OrElse paramType.Equals(GetType(String))
        End Function

        ''' <summary>
        ''' プロパティ値を抽出する為に、SQL文を解析する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Analyze()
            Dim chars As Char() = (sql & " ").ToCharArray
            Dim dqing As Boolean
            Dim sqing As Boolean
            Dim bindName As String = String.Empty
            Dim bindNaming As Boolean
            parameter.Clear()

            Dim IsValueBindParam As Boolean = IsValueBindParameter()
            For Each c As Char In chars
                If bindNaming AndAlso IsPropertyNameChar(c) Then
                    bindName &= c
                Else
                    If bindNaming Then
                        Dim value As Object
                        If IsValueBindParam Then
                            If Not VALUE_BIND_PARAM_NAME.Equals(bindName) Then
                                Throw New ArgumentException(bindName & " に該当するプロパティはない.")
                            End If
                            value = bindParamValue
                        Else
                            Dim aProperty As PropertyInfo = bindParamValue.GetType.GetProperty(bindName.Substring(1))
                            If aProperty Is Nothing Then
                                Throw New ArgumentException(bindName & " に該当するプロパティはない. paramType=" & bindParamValue.GetType.Name)
                            End If
                            value = aProperty.GetValue(bindParamValue, Nothing)
                        End If
                        If Not parameter.ContainsName(bindName) Then
                            parameter.Add(bindName, value)
                        End If
                        bindName = String.Empty
                        bindNaming = False
                    End If
                    If c = """" Then
                        dqing = Not dqing
                    ElseIf c = "'" Then
                        sqing = Not sqing
                    ElseIf Not dqing AndAlso Not sqing AndAlso c = "@" Then
                        If bindParamValue Is Nothing Then
                            Throw New ArgumentException("埋め込みパラメータ名を検知したが、埋め込みパラメータ値がnull.")
                        End If
                        bindNaming = True
                        bindName = c
                    End If
                End If
            Next
        End Sub

        ''' <summary>
        ''' パラメータを SqlAccess に追加設定する
        ''' </summary>
        ''' <param name="db">パラメータを追加する SqlAccess</param>
        ''' <remarks></remarks>
        Public Sub AddAllTo(ByVal db As SqlAccess)
            parameter.AddAllTo(db)
        End Sub

        ''' <summary>
        ''' パラメータ情報を List(Of String) に追加する
        ''' </summary>
        ''' <param name="aList">パラメータ情報を追加する List</param>
        ''' <remarks></remarks>
        Public Sub AddAllTo(ByVal aList As List(Of String))
            parameter.AddAllTo(aList)
        End Sub

    End Class
End Namespace