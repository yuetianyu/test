''' <summary>
''' 日付操作のユーティリティ
''' </summary>
''' <remarks></remarks>
Public Class DateUtil
    ''' <summary>年月日の最大表現(Integer)</summary>
    Public Const MAX_VALUE_INTEGER As Integer = 99999999
    ''' <summary>年月日の最大表現(String)</summary>
    Public Const MAX_VALUE_STRING As String = "9999/12/31"
    ''' <summary>年月日の最大表現(DateTime)</summary>
    Public Shared ReadOnly MAX_VALUE_DATETIME As DateTime = CDate(MAX_VALUE_STRING)

    ''' <summary>
    ''' YYYY-MM-DD書式からYYYYMMDD書式に変換する
    ''' </summary>
    ''' <param name="hyphenYyyymmdd">YYYY/MM/DD書式の文字列</param>
    ''' <returns>YYYYMMDD書式の文字列</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvHyphenYyyymmddToYyyymmdd(ByVal hyphenYyyymmdd As String) As String
        If StringUtil.IsEmpty(hyphenYyyymmdd) Then
            Return Nothing
        End If
        Return Date.Parse(hyphenYyyymmdd).ToString("yyyyMMdd")
    End Function

    ''' <summary>
    ''' 日付を表す数値がEmptyかを返す
    ''' </summary>
    ''' <param name="yyyymmdd">日付を表す数値</param>
    ''' <returns>判定結果</returns>
    ''' <remarks></remarks>
    Private Shared Function IsEmpty(ByVal yyyymmdd As Nullable(Of Integer)) As Boolean
        Return yyyymmdd Is Nothing OrElse yyyymmdd = 0 OrElse yyyymmdd = 99999999
    End Function

    ''' <summary>
    ''' YYYYMMDD書式からYYYY/MM/DD書式に変換する
    ''' </summary>
    ''' <param name="yyyymmdd">YYYYMMDD書式の文字列</param>
    ''' <returns>YYYY/MM/DD書式の文字列</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvYyyymmddToSlashYyyymmdd(ByVal yyyymmdd As String) As String
        If StringUtil.IsEmpty(yyyymmdd) Then
            Return Nothing
        End If
        Return ConvYyyymmddToSlashYyyymmdd(CInt(yyyymmdd))
    End Function

    ''' <summary>
    ''' YYYYMMDD書式からYYYY/MM/DD書式に変換する
    ''' </summary>
    ''' <param name="yyyymmdd">YYYYMMDD書式の数値</param>
    ''' <returns>YYYY/MM/DD書式の文字列</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvYyyymmddToSlashYyyymmdd(ByVal yyyymmdd As Nullable(Of Integer)) As String
        If IsEmpty(yyyymmdd) Then
            Return Nothing
        End If
        If yyyymmdd = MAX_VALUE_INTEGER Then
            Return MAX_VALUE_STRING
        End If
        Return Format(yyyymmdd, "0000/00/00")
    End Function

    ''' <summary>
    ''' YYYYMMDD書式から日付型に変換する
    ''' </summary>
    ''' <param name="yyyymmdd">YYYYMMDD書式の文字列</param>
    ''' <returns>日付型</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvYyyymmddToDate(ByVal yyyymmdd As String) As Nullable(Of Date)
        Return ConvYyyymmddToDate(StringUtil.ToInteger(yyyymmdd))
    End Function

    ''' <summary>
    ''' YYYYMMDD書式から日付型に変換する
    ''' </summary>
    ''' <param name="yyyymmdd">YYYYMMDD書式の文字列</param>
    ''' <returns>日付型</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvYyyymmddToDate(ByVal yyyymmdd As Nullable(Of Integer)) As Nullable(Of Date)
        If IsEmpty(yyyymmdd) Then
            Return Nothing
        End If
        If yyyymmdd = MAX_VALUE_INTEGER Then
            Return MAX_VALUE_DATETIME
        End If
        Return Date.Parse(ConvYyyymmddToSlashYyyymmdd(yyyymmdd))
    End Function

    ''' <summary>
    ''' YYYY/MM/DD書式からYYYYMMDD書式に変換する
    ''' </summary>
    ''' <param name="slashYyyymmdd">YYYY/MM/DD書式の文字列</param>
    ''' <returns>YYYYMMDD書式の文字列</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvSlashYyyymmddToYyyymmdd(ByVal slashYyyymmdd As String) As String
        If StringUtil.IsEmpty(slashYyyymmdd) Then
            Return Nothing
        End If
        Return Date.Parse(slashYyyymmdd).ToString("yyyyMMdd")
    End Function

    ''' <summary>
    ''' YYYY/MM/DD書式からYYYYMMDD書式に変換する
    ''' </summary>
    ''' <param name="slashYyyymmdd">YYYY/MM/DD書式の文字列</param>
    ''' <returns>YYYYMMDD書式の数値</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvSlashYyyymmddToYyyymmddAsInteger(ByVal slashYyyymmdd As String) As Nullable(Of Integer)
        Dim result As String = ConvSlashYyyymmddToYyyymmdd(slashYyyymmdd)
        If StringUtil.IsEmpty(result) Then
            Return Nothing
        End If
        Return CInt(result)
    End Function

    ''' <summary>
    ''' 日時の年月日を YYYYMMDD 形式の数値で返す
    ''' </summary>
    ''' <param name="aDateTime">日時</param>
    ''' <returns>YYYYMMDD形式の数値</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvDateToIneteger(ByVal aDateTime As DateTime?) As Int32?
        If aDateTime Is Nothing Then
            Return Nothing
        End If
        If TruncTime(aDateTime).CompareTo(MAX_VALUE_DATETIME) = 0 Then
            Return MAX_VALUE_INTEGER
        End If
        Return Convert.ToInt32(Convert.ToDateTime(aDateTime).ToString("yyyyMMdd"))
    End Function

    ''' <summary>
    ''' 日時の時分秒を HHMMSS 形式の数値で返す
    ''' </summary>
    ''' <param name="aDateTime">日時</param>
    ''' <returns>HHMMSS形式の数値</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvTimeToIneteger(ByVal aDateTime As DateTime?) As Int32?
        If aDateTime Is Nothing Then
            Return Nothing
        End If
        Return Convert.ToInt32(Convert.ToDateTime(aDateTime).ToString("HHmmss"))
    End Function

    ''' <summary>
    ''' 日付を表す値を DateTime にして返す
    ''' </summary>
    ''' <param name="dateValue">日付を表す値</param>
    ''' <returns>DateTime型</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvDateValueToDateTime(ByVal dateValue As Object) As DateTime?
        If TypeOf dateValue Is DateTime Then
            Return Convert.ToDateTime(dateValue)
        ElseIf TypeOf dateValue Is String Then
            If IsDate(dateValue) Then
                Return Convert.ToDateTime(dateValue)
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' 日付を表す値をYYYYMMDD形式の数値で返す
    ''' </summary>
    ''' <param name="dateValue">日付を表す値</param>
    ''' <returns>YYYYMMDD形式の数値</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvDateValueToIneteger(ByVal dateValue As Object) As Int32?
        Dim aDate As DateTime? = ConvDateValueToDateTime(dateValue)
        If aDate Is Nothing Then
            Return Nothing
        End If
        Return ConvDateToIneteger(aDate)
    End Function

    ''' <summary>
    ''' 日時から、時分秒を切り捨てる
    ''' </summary>
    ''' <param name="aDateTime">日時</param>
    ''' <returns>日付</returns>
    ''' <remarks></remarks>
    Public Shared Function TruncTime(ByVal aDateTime As Date) As Date

        Return DateValue(aDateTime.ToString("yyyy/MM/dd"))
    End Function
End Class
