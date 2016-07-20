Imports System.ComponentModel
Imports EBom.Common
Imports ShisakuCommon
Imports System.Reflection

''' <summary>
''' DB関連共通メソッド
''' </summary>
''' <remarks></remarks>
Public Class NitteiDbComFunc

    ''' <summary>
    ''' テーブル(Propperty)の共通情報を設定し返す
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="aDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function setDefault(Of T)(ByVal aDate As ShisakuDate) As T
        Dim props As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
        Dim aType As Type = GetType(T)
        Dim result As T = CType(Activator.CreateInstance(aType), T)
        For Each aProperty As PropertyInfo In aType.GetProperties()
            Select Case aProperty.Name
                Case "CreatedUserId", "UpdatedUserId"
                    aProperty.SetValue(result, LoginInfo.Now.UserId, Nothing)
                Case "CreatedDate", "UpdatedDate"
                    aProperty.SetValue(result, aDate.CurrentDateDbFormat, Nothing)
                Case "CreatedTime", "UpdatedTime"
                    aProperty.SetValue(result, aDate.CurrentTimeDbFormat, Nothing)
            End Select
        Next
        Return result
    End Function

    ''' <summary>
    ''' ListからDataTableを作成
    ''' </summary>
    ''' <typeparam name="T">Listの型</typeparam>
    ''' <param name="data">データ</param>
    ''' <returns>データを格納したDataTable</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvListToDataTable(Of T)(ByVal data As IList(Of T)) As DataTable
        Dim props As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
        Dim table As New DataTable()
        For i As Integer = 0 To props.Count - 1
            Dim prop As PropertyDescriptor = props(i)
            Dim nm As String = Decamelize(prop.Name)
            If Nullable.GetUnderlyingType(prop.PropertyType) IsNot Nothing Then
                '型がNullable(Of )で定義されていた場合
                '　基底の型でカラムを作成する
                table.Columns.Add(nm, Nullable.GetUnderlyingType(prop.PropertyType))
            Else
                table.Columns.Add(nm, prop.PropertyType)
            End If
        Next
        Dim values As Object() = New Object(props.Count - 1) {}
        For Each item As T In data
            For i As Integer = 0 To values.Length - 1
                values(i) = props(i).GetValue(item)
            Next
            table.Rows.Add(values)
        Next
        Return table
    End Function

    ''' <summary>
    ''' キャメル記法を"_"記法して返す
    ''' </summary>
    ''' <param name="str">キャメル記法の文字列</param>
    ''' <returns>"_"記法の文字列</returns>
    ''' <remarks></remarks>
    Private Shared Function Decamelize(ByVal str As String) As String
        If str = String.Empty Then
            Return str
        End If
        Dim chars() As Char = str.ToCharArray
        Dim result As String = chars(0)
        For i As Integer = 1 To chars.Length - 1
            If "A" <= chars(i) And chars(i) <= "Z" Then
                result &= "_"
            ElseIf "0" <= chars(i) And chars(i) <= "9" Then
                Dim numberStr As String = chars(i)
                For j As Integer = i + 1 To chars.Length - 1
                    If "0" <= chars(j) And chars(j) <= "9" Then
                        numberStr &= chars(j)
                        i = j
                    End If
                Next
                result &= "_" & numberStr
                Continue For
            End If
            result &= chars(i)
        Next
        Return UCase(result)
    End Function

    ''' <summary>
    ''' データテーブルからリストを作成
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="aDataTable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvDataTableToList(Of T)(ByVal aDataTable As DataTable) As List(Of T)
        Dim results As New List(Of T)
        For Each row As DataRow In aDataTable.Rows
            results.Add(ConvDataRowToVo(Of T)(row, aDataTable.Columns))
        Next row
        Return results
    End Function

    ''' <summary>
    ''' レコードからVOを作成
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="aDataRow"></param>
    ''' <param name="aDataColumns"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ConvDataRowToVo(Of T)(ByVal aDataRow As DataRow, ByVal aDataColumns As DataColumnCollection) As t
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
                aProperty.SetValue(result, value, Nothing)
            ElseIf propertyType Is GetType(String) Then
                aProperty.SetValue(result, value.ToString, Nothing)
            ElseIf TypeOf value Is Decimal Then
                Dim decValue As Decimal = Convert.ToDecimal(value)
                If Decimal.Truncate(decValue) = decValue Then
                    If propertyType Is GetType(Int32) Then
                        aProperty.SetValue(result, Decimal.ToInt32(decValue), Nothing)
                    ElseIf propertyType Is GetType(Int64) Then
                        aProperty.SetValue(result, Decimal.ToInt64(decValue), Nothing)
                    ElseIf propertyType Is GetType(Int16) Then
                        aProperty.SetValue(result, Decimal.ToInt16(decValue), Nothing)
                    Else
                        Throw New NotSupportedException(String.Format("DB値 {0} は小数を持ちませんが、型 {1} に変換出来ません.", decValue, propertyType.ToString))
                    End If
                Else
                    aProperty.SetValue(result, value, Nothing)
                End If
            Else
                aProperty.SetValue(result, value, Nothing)
            End If
        Next
        Return result
    End Function

    ''' <summary>
    ''' SqlConnection用の接続文字列を戻します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetConnectString() As String
        Dim con As String = g_kanrihyoIni("mBOM_DB")
        Dim param As String = ""
        param = String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", _
                             GetParam(con, "SERVER"), _
                             GetParam(con, "DATABASE"), _
                             GetParam(con, "UID"), _
                             GetParam(con, "PWD"))
        Return param
    End Function

    ''' <summary>
    ''' 接続文字から指定された要素の値を返す
    ''' </summary>
    ''' <param name="connectionStr">接続文字列</param>
    ''' <param name="val">要素名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetParam(ByVal connectionStr As String, ByVal val As String) As String
        Dim token() As String = connectionStr.Split(";"c)
        Dim ret As String = String.Empty
        For Each s As String In token
            Dim dbToken() As String = s.Split("="c)
            If InStr(dbToken(0).ToUpper, val) > 0 Then
                If dbToken.Length > 1 Then
                    ret = dbToken(1).Trim()
                    Exit For
                End If
            End If
        Next
        Return ret
    End Function

End Class
