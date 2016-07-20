Imports System
Imports System.IO
''' <summary>
''' 文字列操作のユーティリティ
''' </summary>
''' <remarks></remarks>
Public Class StringUtil
    ''' <summary>
    ''' キャメル記法を"_"記法して返す
    ''' </summary>
    ''' <param name="str">キャメル記法の文字列</param>
    ''' <returns>"_"記法の文字列</returns>
    ''' <remarks></remarks>
    Public Shared Function Decamelize(ByVal str As String) As String
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
    ''' キャメル記法を"_"記法して返す(数字文字列はアンダーバーで区切らない)
    ''' </summary>
    ''' <param name="str">キャメル記法の文字列</param>
    ''' <returns>"_"記法の文字列</returns>
    ''' <remarks></remarks>
    Public Shared Function DecamelizeIgnoreNumber(ByVal str As String) As String
        If str = String.Empty Then
            Return str
            'SKMSは_を入れない'
        ElseIf str = "SKMS" Then
            Return str
        End If
        Dim chars() As Char = str.ToCharArray
        Dim result As String = chars(0)
        For i As Integer = 1 To chars.Length - 1
            If "A" <= chars(i) And chars(i) <= "Z" Then
                result &= "_"
            End If
            result &= chars(i)
        Next
        Return UCase(result)
    End Function

    ''' <summary>
    ''' Empty値、またはNull値か、を返す
    ''' </summary>
    ''' <param name="str">判定する文字列</param>
    ''' <returns>Empty値かNull値の場合、true</returns>
    ''' <remarks></remarks>
    Public Shared Function IsEmpty(ByVal str As Object) As Boolean
        Return str Is Nothing OrElse str.ToString.Trim.Length = 0
    End Function

    ''' <summary>
    ''' Empty値以外でかつ、Null値以外か、を返す
    ''' </summary>
    ''' <param name="str">判定する文字列</param>
    ''' <returns>Empty値以外でかつ、Null値以外の場合、true</returns>
    ''' <remarks></remarks>
    Public Shared Function IsNotEmpty(ByVal str As Object) As Boolean
        Return Not IsEmpty(str)
    End Function

    ''' <summary>
    ''' 数値文字列をInteger型にして返す
    ''' </summary>
    ''' <param name="number">数値文字列</param>
    ''' <returns>Integer型の値</returns>
    ''' <remarks></remarks>
    Public Shared Function ToInteger(ByVal number As String) As Nullable(Of Integer)
        If Not IsNumeric(number) Then
            Return Nothing
        End If
        Return CInt(number)
    End Function

    ''' <summary>
    ''' 数値を文字列にして返す
    ''' </summary>
    ''' <param name="obj">数値</param>
    ''' <returns>文字列</returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function ToString(ByVal obj As Object) As String
        If obj Is Nothing Then
            Return Nothing
        End If
        Return obj.ToString
    End Function

    ''' <summary>
    ''' NULL値だったら空文字変換 Null Value Logic
    ''' </summary>
    ''' <param name="obj">判定object</param>
    ''' <returns>対応した文字列</returns>
    ''' <remarks></remarks>
    Public Shared Function Nvl(ByVal obj As Object) As String
        Return Nvl(obj, String.Empty)
    End Function
    ''' <summary>
    ''' NULL値だったら変換 Null Value Logic
    ''' </summary>
    ''' <param name="obj">判定object</param>
    ''' <param name="nullVal">置き換え文字列</param>
    ''' <returns>対応した文字列</returns>
    ''' <remarks></remarks>
    Public Shared Function Nvl(ByVal obj As Object, ByVal nullVal As String) As String
        If obj Is Nothing Then
            Return nullVal
        End If
        Return obj.ToString
    End Function
    ''' <summary>
    ''' NULL値or空文字だったら変換  Empty Value Logic
    ''' </summary>
    ''' <param name="str">判定文字列</param>
    ''' <param name="emptyVal">置き換え文字列</param>
    ''' <returns>対応した文字列</returns>
    ''' <remarks></remarks>
    Public Shared Function Evl(ByVal str As String, ByVal emptyVal As String) As String
        If IsEmpty(str) Then
            Return emptyVal
        End If
        Return str
    End Function

    ''' <summary>
    ''' 文字列を List(Of String) にして返す
    ''' </summary>
    ''' <param name="elements">Listの要素たち</param>
    ''' <returns>List(Of String)</returns>
    ''' <remarks></remarks>
    Public Shared Function ToList(ByVal ParamArray elements() As String) As List(Of String)
        Dim results As New List(Of String)
        For Each element As String In elements
            results.Add(element)
        Next
        Return results
    End Function
    ''' <summary>
    ''' ファイル名に利用できない文字列を指定文字に変換して返す
    ''' デフォルトは""
    ''' </summary>
    ''' <param name="srcString">変換対象文字列</param>
    ''' <param name="replaceString">置換する文字</param>
    ''' <returns>変換対象文字列の中のファイル禁止文字を置換文字に置き換えた文字列</returns>
    ''' <remarks></remarks>
    Public Shared Function ReplaceInvalidCharForFileName(ByVal srcString As String, Optional ByVal replaceString As String = "") As String

        Dim valid As String = srcString
        Dim invalidch As Char() = Path.GetInvalidFileNameChars()

        For Each c As Char In invalidch
            valid = valid.Replace(c, replaceString)
        Next
        Return valid
    End Function

End Class
