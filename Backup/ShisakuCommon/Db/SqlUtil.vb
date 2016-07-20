Imports System.Reflection

Namespace Db
    Public Class SqlUtil

        ''' <summary>
        ''' Where句を作成して返す
        ''' </summary>
        ''' <param name="clause">検索条件となるVo</param>
        ''' <returns>Nullプロパティ以外のプロパティをもとに作成したWhere句</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeWhereClause(ByVal clause As Object) As String
            Return MakeWhereClause(clause, True)
        End Function

        ''' <summary>
        ''' Where句を作成して返す
        ''' </summary>
        ''' <param name="clause">検索条件となるVo</param>
        ''' <param name="ignoreNull">Nullプロパティを無視してWhere句を作成する場合、true</param>
        ''' <returns>プロパティをもとに作成したWhere句</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeWhereClause(ByVal clause As Object, ByVal ignoreNull As Boolean) As String
            Dim results As New List(Of String)
            If clause Is Nothing Then
                Return String.Empty
            End If
            Dim aType As Type = clause.GetType
            If ignoreNull Then
                For Each aProperty As PropertyInfo In aType.GetProperties()
                    Dim value As Object = aProperty.GetValue(clause, Nothing)
                    If value IsNot Nothing Then
                        results.Add(StringUtil.Decamelize(aProperty.Name) & " = @" & aProperty.Name)
                    End If
                Next
            Else
                For Each aProperty As PropertyInfo In aType.GetProperties()
                    results.Add(StringUtil.Decamelize(aProperty.Name) & " = @" & aProperty.Name)
                Next
            End If
            Return MakeWhereClause(results)
        End Function

        ''' <summary>
        ''' Where句を作成して返す
        ''' </summary>
        ''' <param name="clause">検索条件となるVo</param>
        ''' <param name="onlyFields">検索条件に含めるプロパティ情報</param>
        ''' <returns>onlyFieldsをもとに作成したWhere句</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeWhereClauseOnly(ByVal clause As Object, ByVal onlyFields As List(Of PropertyInfo)) As String
            Dim results As New List(Of String)
            For Each aProperty As PropertyInfo In onlyFields
                results.Add(StringUtil.Decamelize(aProperty.Name) & " = @" & aProperty.Name)
            Next
            Return MakeWhereClause(results)
        End Function

        ''' <summary>
        ''' Where句を作成して返す
        ''' </summary>
        ''' <param name="clauseList">条件式を保持した List</param>
        ''' <returns>Where句</returns>
        ''' <remarks></remarks>
        Private Shared Function MakeWhereClause(ByVal clauseList As List(Of String)) As String
            If clauseList.Count = 0 Then
                Return String.Empty
            End If
            Return " WHERE " & Join(clauseList.ToArray, " AND ")
        End Function

        ''' <summary>
        ''' Insert文の、INTO句とVALUES句を返す
        ''' </summary>
        ''' <param name="parameterObj">テーブルVO</param>
        ''' <returns>Insert文の、INTO句とVALUES句</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeInsertInto(ByVal parameterObj As Object) As String
            Dim columns As New List(Of String)
            Dim values As New List(Of String)
            Dim aType As Type = parameterObj.GetType
            For Each aProperty As PropertyInfo In aType.GetProperties()
                columns.Add(StringUtil.Decamelize(aProperty.Name))
                values.Add("@" & aProperty.Name)
            Next
            Return "(" & Join(columns.ToArray, ", ") & ") VALUES (" & Join(values.ToArray, ", ") & ")"
        End Function

        ''' <summary>
        ''' Update文のSET句とWHERE句を返す
        ''' </summary>
        ''' <param name="clauseValue">条件と値をもつテーブルVO</param>
        ''' <param name="clauseFields">検索条件となるプロパティ</param>
        ''' <returns>Update文のSET句とWHERE句</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeUpdateSetWhere(ByVal clauseValue As Object, ByVal clauseFields As List(Of PropertyInfo)) As String
            Dim sets As New List(Of String)
            Dim aType As Type = clauseValue.GetType
            For Each aProperty As PropertyInfo In aType.GetProperties()
                If clauseFields.IndexOf(aProperty) < 0 Then
                    sets.Add(StringUtil.Decamelize(aProperty.Name) & " = @" & aProperty.Name)
                End If
            Next
            Return Join(sets.ToArray, ", ") & MakeWhereClauseOnly(clauseValue, clauseFields)
        End Function
    End Class
End Namespace