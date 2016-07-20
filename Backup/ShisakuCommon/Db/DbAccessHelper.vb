Imports EBom.Data
Imports System.Reflection
Imports ShisakuCommon.Db.Sql

Namespace Db
    Public Class DbAccessHelper

        'Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(GetType(DbAccessHelper))

        Private db As SqlAccess

        Public Sub New(ByVal db As SqlAccess)
            Me.db = db
        End Sub

        Public Function QueryForList(Of T)(ByVal sql As String, ByVal param As Object) As List(Of t)
            Dim dtData As New DataTable
            Dim sqlAnalyzer As New ShisakuSqlAnalyzer(sql, param)
            sqlAnalyzer.Analyze()
            'log.Debug(sqlAnalyzer.AnalyzedSql)
            ShisakuDbParameter.logDebug("SQL:" & sqlAnalyzer.AnalyzedSql)
            sqlAnalyzer.AddParametersTo(db)
            db.Fill(sqlAnalyzer.AnalyzedSql, dtData)
            Return ConvDataTableToList(Of T)(dtData)
        End Function

        Public Function Execute(ByVal sql As String, ByVal param As Object) As Integer
            Dim sqlAnalyzer As New ShisakuSqlAnalyzer(sql, param)
            sqlAnalyzer.Analyze()
            'log.Debug(sqlAnalyzer.AnalyzedSql)
            ShisakuDbParameter.logDebug("SQL:" & sqlAnalyzer.AnalyzedSql)
            sqlAnalyzer.AddParametersTo(db)
            Return db.ExecuteNonQuery(sqlAnalyzer.AnalyzedSql)
        End Function

        Public Sub ExecuteSetting(ByVal sql As String)
            ShisakuDbParameter.logDebug(sql)
            db.ExecuteNonQuery(sql)
        End Sub

        Private Shared Function ConvDataRowToVo(Of T)(ByVal aDataRow As DataRow, ByVal aDataColumns As DataColumnCollection) As T
            Dim aType As Type = GetType(T)
            Dim result As T = CType(Activator.CreateInstance(aType), T)
            If aType.IsValueType Or (aType.Equals(GetType(String))) Then
                result = aDataRow(0)
                Return result
            End If
            For Each aProperty As PropertyInfo In aType.GetProperties()
                Dim columnName As String = StringUtil.Decamelize(aProperty.Name)
                If aDataColumns.IndexOf(columnName) < 0 Then
                    Continue For
                End If
                Dim value As Object = aDataRow(columnName)
                If TypeOf value Is System.DBNull Then
                    aProperty.SetValue(result, Nothing, Nothing)
                    Continue For
                End If

                Dim propertyType As Type = VoUtil.GetTypeIfNullable(aProperty.PropertyType)
                If value.GetType Is propertyType Then
                    '2012/02/18 イベント登録の仕向けリストを取得する時にエラーになるので
                    '一時対応を止める
                    '2012/02/16 valueが空のときにエラーになるので'
                    'If Not StringUtil.IsEmpty(value) Then
                    aProperty.SetValue(result, value, Nothing)
                    'End If
                ElseIf propertyType Is GetType(String) Then
                    '2012/02/18 イベント登録の仕向けリストを取得する時にエラーになるので
                    '一時対応を止める
                    'If Not StringUtil.IsEmpty(value) Then
                    aProperty.SetValue(result, value.ToString, Nothing)
                    'End If
                    'ElseIf TypeOf value Is Decimal Then
                    '    Dim decValue As Decimal = Convert.ToDecimal(value)
                    '    '' Numeric( 4, 0) の項目は、VO上では Int32 にしているので、それを考慮
                    '    If Decimal.Truncate(decValue) = decValue Then
                    '        If propertyType Is GetType(Int32) Then
                    '            aProperty.SetValue(result, Decimal.ToInt32(decValue), Nothing)
                    '        ElseIf propertyType Is GetType(Int64) Then
                    '            aProperty.SetValue(result, Decimal.ToInt64(decValue), Nothing)
                    '        ElseIf propertyType Is GetType(Int16) Then
                    '            aProperty.SetValue(result, Decimal.ToInt16(decValue), Nothing)
                    '        Else
                    '            Throw New NotSupportedException(String.Format("DB値 {0} は小数を持ちませんが、型 {1} に変換出来ません.", decValue, propertyType.ToString))
                    '        End If
                    '    Else
                    '        aProperty.SetValue(result, value, Nothing)
                    '    End If

                    '2015/12/25 kabasawa'
                    'INT32等に対応(三鷹ツールから拝借)'
                ElseIf propertyType Is GetType(Decimal) OrElse propertyType Is GetType(Int32) OrElse propertyType Is GetType(Int64) OrElse propertyType Is GetType(Int16) Then
                    ' ルール1. DBがDoubleでも、精度を省みて、.NET上では、Decimalで扱う
                    ' ルール2. DBがNumeric(4,0)は、voPropertyがDecimal/Doubleだが、VO上はInt32にしている
                    If IsNumeric(value) Then
                        If propertyType Is GetType(Decimal) Then
                            aProperty.SetValue(result, Convert.ToDecimal(value), Nothing)
                        Else
                            Dim decValue As Decimal = Convert.ToDecimal(value)
                            If Decimal.Truncate(decValue) <> decValue Then
                                'エラー'
                                AssertInvalidPropertyTypeAndDbType(columnName, value, aProperty, propertyType)
                            End If
                            If propertyType Is GetType(Int32) Then
                                aProperty.SetValue(result, Convert.ToInt32(value), Nothing)
                            ElseIf propertyType Is GetType(Int64) Then
                                aProperty.SetValue(result, Convert.ToInt64(value), Nothing)
                            ElseIf propertyType Is GetType(Int16) Then
                                aProperty.SetValue(result, Convert.ToInt16(value), Nothing)
                            End If
                        End If
                    ElseIf StringUtil.IsNotEmpty(value) Then
                        AssertInvalidPropertyTypeAndDbType(columnName, value, aProperty, propertyType)
                    End If
                    'ここまで'
                Else
                    aProperty.SetValue(result, value, Nothing)
                End If
            Next
            Return result
        End Function

        Public Shared Function ConvDataTableToList(Of T)(ByVal aDataTable As DataTable) As List(Of t)
            Dim results As New List(Of t)
            For Each row As DataRow In aDataTable.Rows
                results.Add(ConvDataRowToVo(Of T)(row, aDataTable.Columns))
            Next row
            Return results
        End Function

        Private Shared Sub AssertInvalidPropertyTypeAndDbType(ByVal columnName As String, ByVal dbValue As Object, ByVal voProperty As PropertyInfo, ByVal voPropertyType As Type)

            Throw New ArgumentException(String.Format("列名 {0} の値は {1} だが、VO.{2} は {3} 型", columnName, StringUtil.ToString(dbValue), voProperty.Name, voPropertyType.Name))
        End Sub

    End Class
End Namespace