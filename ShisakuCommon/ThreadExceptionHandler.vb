Imports EBom.Common

Public Class ThreadExceptionHandler

    Public Sub HandleSetting()
        AddHandler Application.ThreadException, AddressOf HandleThreadException
    End Sub

    ''' <summary>
    ''' 未処理例外をキャッチするイベントハンドラ。
    ''' メインスレッド用。(WindowsForm専用)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Shared Sub HandleThreadException(ByVal sender As Object, ByVal e As System.Threading.ThreadExceptionEventArgs)

        OutPutLogErr(e.Exception, "試作手配システム")

        g_log.WriteException(e.Exception)

        'ComFunc.ShowErrMsgBox("予期せぬエラーが発生しました. 処理を続行出来ません.\n\nエラー内容 : {0}", e.Exception.Message)
        ComFunc.ShowErrMsgBox("タイムアウトが発生しました。 時間を置いて再度実行してください。.\n\n内容 : {0}", e.Exception.Message)
    End Sub


    ''' <summary>
    ''' エラーログをイベントビュアーに出力
    ''' </summary>
    ''' <param name="e">Exceptionオブジェクト</param>
    ''' <param name="sourceName">ソース名</param>
    ''' <remarks></remarks>
    Private Shared Sub OutPutLogErr(ByVal e As Exception, ByVal sourceName As String)

        Try
            'ソースが存在していない時は、作成する
            If Not System.Diagnostics.EventLog.SourceExists(sourceName) Then
                'ログ名を空白にすると、"Application"となる

                System.Diagnostics.EventLog.CreateEventSource(sourceName, "")
            End If
            'テスト用にイベントログエントリに付加するデータを適当に作る
            Dim myData() As Byte = {}
            Dim msg As String = "例外：" & vbNewLine & e.Message & vbNewLine & "例外スタックトレース" & vbNewLine & e.StackTrace & vbNewLine
            If e.InnerException IsNot Nothing Then
                msg = msg & "InnerException:" & vbNewLine & e.InnerException.Message & vbNewLine & "InnerExceptionスタックトレース:" & vbNewLine & e.InnerException.StackTrace
            End If
            'イベントログにエントリを書き込む
            'ここではエントリの種類をエラー、イベントIDを1、分類を1000とする
            System.Diagnostics.EventLog.WriteEntry(sourceName, msg, System.Diagnostics.EventLogEntryType.Error, 1, 1000, myData)
        Catch ignore As Exception
            '' ここの例外は無視 by T.Homma
        End Try
    End Sub

End Class
