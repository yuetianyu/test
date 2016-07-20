Imports System.Reflection

Public Class VoUtil
    ''' <summary>
    ''' 同名プロパティ同士の値をコピーする
    ''' </summary>
    ''' <param name="src">コピー元のVo</param>
    ''' <param name="dest">コピー先のVo</param>
    ''' <remarks>同名プロパティは</remarks>
    Public Shared Sub CopyProperties(ByVal src As Object, ByVal dest As Object)
        Dim destSetterProperties As New Dictionary(Of String, PropertyInfo)
        For Each aProperty As PropertyInfo In dest.GetType.GetProperties
            If aProperty.GetSetMethod Is Nothing Then
                Continue For
            End If
            If aProperty.GetSetMethod.GetParameters.Length = 0 Then
                Continue For
            End If
            destSetterProperties.Add(aProperty.Name, aProperty)
        Next
        For Each aProperty As PropertyInfo In src.GetType.GetProperties
            If aProperty.GetGetMethod Is Nothing Then
                Continue For
            End If
            If Not destSetterProperties.ContainsKey(aProperty.Name) Then
                Continue For
            End If

            Dim value As Object = aProperty.GetValue(src, Nothing)
            If value Is Nothing Then
                destSetterProperties(aProperty.Name).SetValue(dest, Nothing, Nothing)
                Continue For
            End If

            ' コピー先が文字列なら、コピー元を文字列変換
            ' コピー元が文字列なら、コピー先の型に変換
            ' それ以外は、コピー元の値をそのままコピー先へ

            If destSetterProperties(aProperty.Name).PropertyType Is GetType(String) Then
                destSetterProperties(aProperty.Name).SetValue(dest, value.ToString, Nothing)

            ElseIf TypeOf value Is String Then
                Dim destPropertyType As Type = GetTypeIfNullable(destSetterProperties(aProperty.Name).PropertyType)

                If destPropertyType Is GetType(Int32) Then
                    destSetterProperties(aProperty.Name).SetValue(dest, Convert.ToInt32(value), Nothing)
                ElseIf destPropertyType Is GetType(Int64) Then
                    destSetterProperties(aProperty.Name).SetValue(dest, Convert.ToInt64(value), Nothing)
                ElseIf destPropertyType Is GetType(DateTime) Then
                    destSetterProperties(aProperty.Name).SetValue(dest, Convert.ToDateTime(value), Nothing)
                ElseIf destPropertyType Is GetType(Decimal) Then
                    destSetterProperties(aProperty.Name).SetValue(dest, Convert.ToDecimal(value), Nothing)
                Else
                    destSetterProperties(aProperty.Name).SetValue(dest, value, Nothing)
                End If
            Else
                destSetterProperties(aProperty.Name).SetValue(dest, value, Nothing)
            End If
        Next
    End Sub
    ''' <summary>
    ''' Nullable(Of T) かを返す
    ''' </summary>
    ''' <param name="aType">判定するType</param>
    ''' <returns>結果</returns>
    ''' <remarks></remarks>
    Public Shared Function IsTypeNullable(ByVal aType As Type) As Boolean
        Return aType.Name.StartsWith("Nullable`")
    End Function
    ''' <summary>
    ''' 型を返す. Nullable(Of T) なら T の型を返す.
    ''' </summary>
    ''' <param name="aType">判定する Type</param>
    ''' <returns>型 (Nullable以外)</returns>
    ''' <remarks></remarks>
    Public Shared Function GetTypeIfNullable(ByVal aType As Type) As Type
        If IsTypeNullable(aType) Then
            Return aType.GetGenericArguments(0)
        End If
        Return aType
    End Function
    ''' <summary>
    ''' Voにキー番号を付与し、キー番号に沿ったPropertyInfoの一覧を返す
    ''' </summary>
    ''' <param name="vo">キー番号を付与するVo</param>
    ''' <returns>キー番号に沿ったPropertyInfoの一覧</returns>
    ''' <remarks></remarks>
    Friend Shared Function MarkVoAndGetKeyProperties(ByVal vo As Object) As Dictionary(Of Object, PropertyInfo)
        Dim map As New Dictionary(Of Object, PropertyInfo)
        Dim aType As Type = vo.GetType
        Dim count As Integer = 0
        For Each aProperty As PropertyInfo In aType.GetProperties()
            Dim aPropertyType As Type = VoUtil.GetTypeIfNullable(aProperty.PropertyType)
            Dim value As Object
            If aPropertyType Is GetType(Int32) Then
                value = count
            ElseIf aPropertyType Is GetType(String) Then
                value = CStr(count)
            ElseIf aPropertyType Is GetType(Decimal) Then
                value = CDec(count)
            ElseIf aPropertyType Is GetType(DateTime) Then
                value = DateAdd(DateInterval.Day, count, New DateTime())
            ElseIf aPropertyType Is GetType(Int64) Then
                value = count
            Else
                Throw New NotSupportedException("未対応のプロパティ型です. " & aProperty.PropertyType.ToString)
            End If
            aProperty.SetValue(vo, value, Nothing)
            map.Add(value, aProperty)
            count += 1
        Next
        Return map
    End Function
End Class
