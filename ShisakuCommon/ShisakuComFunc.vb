Imports FarPoint.Win
Imports EBom.Common

''' <summary>
''' 新試作手配システム 共通関数
''' </summary>
''' <remarks></remarks>
Public Class ShisakuComFunc

#Region " IDと名は互いに転化さします  "
    Public Shared Function GetRelativeValue(ByVal param1 As String(), ByVal param2 As String(), ByVal value As String) As String
        Dim i As Integer
        For i = 0 To param1.Length - 1
            If value.Equals(param1(i)) Then
                Return param2(i)
            End If
        Next
        Return ""
    End Function
#End Region

#Region " 文字列長チェック"
    'Is in length
    Public Shared Function IsInLength(ByVal strLen As Object, ByVal iLen As Integer) As Boolean
        If strLen = String.Empty Then
            Return True
        End If
        Dim sarr As Byte() = System.Text.Encoding.Default.GetBytes(strLen)
        If sarr.Length > iLen Then
            Return False
        End If
        Return True
    End Function

    'Is length
    Public Shared Function IsLength(ByVal strLen As Object, ByVal iLen As Integer) As Boolean
        Dim sarr As Byte() = System.Text.Encoding.Default.GetBytes(strLen)
        If Not sarr.Length = iLen Then
            Return False
        End If
        Return True
    End Function
#End Region

#Region " ユーザー区分により区分名を取得する。"
    Public Shared Function GetUserKunbunName(ByVal kubunCd As String) As String
        Return GetRelativeValue(ShisakuGlobal.UserKubunCd, ShisakuGlobal.UserKubunName, kubunCd)
    End Function
#End Region

#Region " ユーザー区分により区分名を取得する。"
    Public Shared Function GetUserKunbunCd(ByVal kubunName As String) As String
        Return GetRelativeValue(ShisakuGlobal.UserKubunName, ShisakuGlobal.UserKubunCd, kubunName)
    End Function
#End Region


#Region "試作設計ブロック情報の「状態」項目　文字"
    Public Shared Function getBlockJyoutaiMoji(ByVal jyoutai As String) As String
        '2013/06/24 ＡＬ再展開用のステータスとして再抽出済みを追加
        '
        Dim jyoutaiMoji As String = ""
        Select Case jyoutai
            Case ShishakuSekkeiBlockStatusShouchiChuu
                jyoutai = "編集中"
            Case ShishakuSekkeiBlockStatusIchiji
                jyoutai = "一時保存中"
            Case ShishakuSekkeiBlockStatusSumi
                jyoutai = "登録済み"
            Case ShishakuSekkeiBlockStatusShouchiKanryou
                jyoutai = "完了"
            Case ShishakuSekkeiBlockStatusShounin1
                jyoutai = "担当承認"
            Case ShishakuSekkeiBlockStatusShounin2
                jyoutai = "課長・主査承認"
            Case ShishakuSekkeiBlockStatusSaiChuusyutuSumi
                jyoutai = "再抽出済み"
        End Select
        Return jyoutai
    End Function

#End Region

#Region "試作設計ブロック情報の「ブロック不要」項目　文字"
    Public Shared Function getBlockFuyouMoji(ByVal blockFuyou As String) As String
        Dim blockFuyouMoji As String = ""
        Select Case blockFuyou
            Case ShishakuSekkeiBlockHitsuyou
                blockFuyou = ""
            Case ShishakuSekkeiBlockFuyou
                blockFuyou = "不要"
        End Select
        Return blockFuyou
    End Function

#End Region

#Region "8桁数字が日にコンバートする"
    ''' <summary>
    ''' 8桁数字が日にコンバートする
    ''' </summary>
    ''' <param name="dateStr">日の8桁文字</param>
    ''' <returns>
    ''' yyyy/MM/dd
    ''' エラーの場合はブランクにします
    ''' </returns>
    ''' <remarks></remarks>
    Public Shared Function moji8Convert2Date(ByVal dateStr As String) As String
        Dim convertDate = ""
        If (Not dateStr Is Nothing) AndAlso ((Not dateStr = String.Empty) And (dateStr.Length = 8)) Then
            convertDate = CDate(DateTime.ParseExact(dateStr, "yyyyMMdd", Nothing))
        End If
        Return convertDate
    End Function
#End Region

#Region "6桁数字が時間にコンバートする"
    ''' <summary>
    ''' 6桁数字が時間にコンバートする
    ''' </summary>
    ''' <param name="timeStr">時間の6桁文字</param>
    ''' <returns>
    ''' HH:mm:ss
    ''' エラーの場合はブランクにします
    ''' </returns>
    ''' <remarks></remarks>
    Public Shared Function moji6Convert2Time(ByVal timeStr As String) As String
        Dim convertedTime As String = ""

        If (Not timeStr Is Nothing) AndAlso ((Not timeStr = String.Empty) And (timeStr.Length = 6)) Then
            convertedTime = TimeSerial(timeStr.Substring(0, 2), timeStr.Substring(2, 2), timeStr.Substring(4, 2))
        End If
        Return convertedTime
    End Function
#End Region

#Region "ファイルオーブン状態を判断します"
    ''' <summary>
    ''' ファイルオーブン状態を判断します
    ''' </summary>
    ''' <param name="fileName">目的ファイル名</param>
    ''' <returns>オーブン状態
    ''' オープンしている　　True
    ''' クロースしている　　False
    ''' </returns>
    ''' <remarks></remarks>
    Public Shared Function IsFileOpen(ByVal fileName As String) As Boolean
        Dim objProcess() As Process = Process.GetProcesses
        For Each p As Process In objProcess
            If p.MainWindowTitle.Contains(fileName) Then
                ComFunc.ShowInfoMsgBox("別のプロセスで使用されているため、プロセスはファイル '" & fileName & "' にアクセスできません。")
                Return True
            End If
        Next
        Return False
    End Function
#End Region


#Region "号車より開発符号、#、Wを除いて４桁（４桁未満は頭０付き）にして返す"
    ''' <summary>
    ''' 号車より開発符号、#、Wを除いて４桁（４桁未満は頭０付き）にして返す
    ''' </summary>
    ''' <param name="strKaihatshFugo">開発符号</param>
    ''' <param name="strGousya">号車</param>
    ''' <returns>
    ''' エラーの場合はブランクにします
    ''' </returns>
    ''' <remarks></remarks>
    Public Shared Function kaihatsuFugo4keta(ByVal strKaihatshFugo As String, ByVal strGousya As String, Optional ByVal strMaeZero As String = "0") As String
        Dim shisakuGousya As String = ""
        Dim getShisakuGousya As String = ""
        Dim ichi As Integer = 0
        Dim ichiW As Integer = 0

        strGousya = strGousya.Replace(strKaihatshFugo, "")
        ichi = strGousya.IndexOf("#")
        ichiW = strGousya.IndexOf("W")

        '指定文字移行のデータを取得する。
        If ichi >= 0 Then
            getShisakuGousya = Mid(strGousya, ichi + 2, 4)
        ElseIf ichiW >= 0 Then
            getShisakuGousya = Mid(strGousya, ichiW + 2, 4)
        Else
            getShisakuGousya = strGousya.Replace("#", "")
            getShisakuGousya = strGousya.Replace("W", "")
        End If
        If StringUtil.Equals(strMaeZero, "0") Then
            strGousya = getShisakuGousya.PadLeft(4, "0")
        Else
            strGousya = getShisakuGousya
        End If
        If StringUtil.IsNotEmpty(strGousya) Then
            shisakuGousya = strGousya
        End If
        Return shisakuGousya
    End Function
#End Region

#Region "　Right メソッド　"

    ''' -----------------------------------------------------------------------------------
    ''' <summary>
    '''     文字列の右端から指定された文字数分の文字列を返します。</summary>
    ''' <param name="stTarget">
    '''     取り出す元になる文字列。</param>
    ''' <param name="iLength">
    '''     取り出す文字数。</param>
    ''' <returns>
    '''     右端から指定された文字数分の文字列。
    '''     文字数を超えた場合は、文字列全体が返されます。</returns>
    ''' -----------------------------------------------------------------------------------
    Public Shared Function cRight(ByVal stTarget As String, ByVal iLength As Integer) As String
        If iLength <= stTarget.Length Then
            Return stTarget.Substring(stTarget.Length - iLength)
        End If

        Return stTarget
    End Function

#End Region

#Region "ExcelOutPutDirの存在チェックを行い、無ければ作成する。"
    ''' <summary>
    ''' ExcelOutPutDirの存在チェックを行い、無ければ作成する。
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub ExcelOutPutDirCheck()
        If System.IO.Directory.Exists(ExcelOutPutDir & "\") = False Then
            ' フォルダ (ディレクトリ) を作成する
            System.IO.Directory.CreateDirectory(ExcelOutPutDir & "\")
        End If
    End Sub
#End Region

End Class
