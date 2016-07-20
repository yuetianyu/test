Imports EventSakusei.XVLView
Imports EventSakusei.XVLView.Logic

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class FrmVeiwerImge

    ''' <summary>画面制御ロジック</summary>
    Private _ViewerImgeLogic As Logic.ViewerImgeSubject

    '''<summary>初期化完了フラグ</summary>>
    Private _InitComplete As Boolean = False
    ''' <summary>XVL一時保存用</summary>
    Private Const folder As String = "C:\Program Files\GS2-Presents\TrialManufacture\work"
    'Private Const folder As String = "T:\新試作手配システム\XVLTMP\"

    Private file As String

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub
    Public Sub New(ByVal aFileName As String)
        'Public Sub New(ByVal KaihatsuFugo As String, ByVal BuhinNo As String, ByVal BuhinName As String, ByVal HojyoName As String)

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        'パラメータ退避
        'Dim ViewVo As New ViewerImgeVo
        'ViewVo.KaihatsuFugo = KaihatsuFugo
        'ViewVo.BuhinNo = BuhinNo
        'ViewVo.BuhinName = BuhinName
        'ViewVo.HojyoName = HojyoName

        '画面制御ロジック
        _ViewerImgeLogic = New Logic.ViewerImgeSubject(aFileName, Me)

        'Initialize()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#End Region

#Region "メソッド"

#Region "初期化メイン"

    ''' <summary>
    ''' 初期化メイン
    ''' </summary>
    ''' <param name="aFileNames">XVLファイルURL</param>
    ''' <param name="userId">ユーザーID</param>
    ''' <param name="shisakuEventCode">イベントコード</param>
    ''' <param name="gousya">号車名</param>
    ''' <remarks></remarks>
    Public Sub Initialize(ByVal aFileNames As List(Of String), ByVal userId As String, ByVal shisakuEventCode As String, ByVal gousya As String)

        Dim time As New Stopwatch
        time.Start()

        'フォルダはひとまず固定で作成する'
        file = folder & "\" & "userId" & "_" & shisakuEventCode & "_" & gousya & ".html"
        'file = folder & "userId" & "_" & shisakuEventCode & "_" & gousya & ".html"

        Try

            Cursor.Current = Cursors.WaitCursor

            _ViewerImgeLogic = Nothing
            '2014/04/18 kabasawa'
            'フォルダの作成'
            If Not System.IO.Directory.Exists(folder) Then
                System.IO.Directory.CreateDirectory(folder)
            End If

            'ファイルの削除'
            If System.IO.File.Exists(file) Then
                System.IO.File.Delete(file)
            End If

            Dim hStream As System.IO.FileStream = Nothing

            ' hStream が破棄されることを保証するために Try ～ Finally を使用する
            Try
                ' hStream が閉じられることを保証するために Try ～ Finally を使用する
                Try
                    ' 指定したパスのファイルを作成する
                    hStream = System.IO.File.Create(file)
                Finally
                    ' 作成時に返される FileStream を利用して閉じる
                    If Not hStream Is Nothing Then
                        hStream.Close()
                    End If
                End Try
            Finally
                ' hStream を破棄する
                If Not hStream Is Nothing Then
                    Dim cDisposable As System.IDisposable = hStream
                    cDisposable.Dispose()
                End If
            End Try

            ' HTMLファイルオープン
            Dim sw As IO.StreamWriter = _
                New IO.StreamWriter(file, False, _
                System.Text.Encoding.GetEncoding("SHIFT-JIS"))

            ' HTMLファイル書込(ヘッダー部)
            sw.WriteLine("<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01 Transitional//EN""")
            sw.WriteLine("""http://www.w3.org/TR/html4/loose.dtd"">")
            sw.WriteLine("<html>")
            sw.WriteLine("<head>")
            sw.WriteLine("<meta http-equiv=""Content-type""content=""text/html; charset=Shift_JIS"">")
            sw.WriteLine("<title>" & gousya & "</title>")
            sw.WriteLine("</head>")
            sw.WriteLine("<body>")
            sw.WriteLine("<object classid=""CLSID:A7307292-9343-4de6-80F9-2DCEBAA7CF9C""")
            sw.WriteLine("id=""xvlplayer""")
            sw.WriteLine("width=""100%"" height=""100%"">")
            If aFileNames.Count > 0 Then
                sw.WriteLine("<param name=""src"" value=""" & aFileNames(0) & """>")
                If aFileNames.Count > 1 Then
                    Dim a As New System.Text.StringBuilder
                    Dim count As Integer = 0
                    For index As Integer = 1 To aFileNames.Count - 1
                        a.Append(aFileNames(index) & ";")
                        'count = count + 1
                        'If count = 10 Then
                        '    sw.WriteLine("<param name=""Files"" value=""" & a.ToString.Substring(0, a.ToString.Length - 1) & """>")
                        '    a.Remove(0, a.Length)
                        '    count = 0
                        'End If
                    Next
                    sw.WriteLine("<param name=""Files"" value=""" & a.ToString.Substring(0, a.ToString.Length - 1) & """>")
                End If
            End If

            sw.WriteLine("<param name=""Options"" value="" VIEW_ACCELERATE(TRUE)"">")
            sw.WriteLine("<param name=""Mode"" value=""MODE_BEHAVIOR(0), MODE_VIEW_OP(0)"">")
            sw.WriteLine("<param name=""Displays"" value="" VIEW_DISPLAY_TYPE(1),VIEW_SHOW_POINT(FALSE),VIEW_SHOW_ISO_PT(FALSE),VIEW_SHOW_ISO_CV(FALSE),VIEW_SHOW_INFINITE_LINE(FALSE),VIEW_SHOW_INFINITE_PLANE(FALSE)"">")

            ' HTMLファイル書込(フッター部)
            sw.WriteLine("</object>")
            sw.WriteLine("</body>")
            sw.WriteLine("</html>")

            ' HTMLファイルクローズ
            sw.Close()
            'ここまで'

            'For Each lFilename In aFileNames

            '    If _ViewerImgeLogic Is Nothing Then
            '        '先頭のファイルを親として登録.
            '        _ViewerImgeLogic = New Logic.ViewerImgeSubject(lFilename, Me)
            '    Else
            '        '要素を追加する.
            '        _ViewerImgeLogic.IMPORT(lFilename)
            '    End If
            'Next

            '初期化完了
            _InitComplete = True
            WebBrowser1.Navigate(New Uri(file))

        Catch ex As Exception

            Dim msg As String
            msg = String.Format("画面の初期化中に問題が発生しました(ERR={0})", ex.Message)
            Me.Close()

        Finally
            Cursor.Current = Cursors.Default
        End Try
        '終了'
        Debug.Print("終了" & time.Elapsed.ToString)

    End Sub

#End Region
    ''' <summary>
    ''' 親部品に対して部品を追加.
    ''' </summary>
    ''' <param name="aFilename"></param>
    ''' <remarks></remarks>
    Public Sub Import(ByVal aFilename As String)
        _ViewerImgeLogic.IMPORT(aFilename)
    End Sub

#End Region

    Private Sub FrmVeiwerImge_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'MsgBox("load")
    End Sub

    Private Sub FrmVeiwerImge_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'MsgBox("shown")
        Me.Refresh()
    End Sub

    Private Sub AxXVLPlayer_OnStatus(ByVal sender As System.Object, ByVal e As AxXVLPlayer3Lib._IXVLViewEvents_OnStatusEvent)

    End Sub

    ''' <summary>
    ''' 画面閉じるとき
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmVeiwerImge_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteXVL()
    End Sub

    ''' <summary>
    ''' 当画面が保有しているファイルを削除
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DeleteXVL()
        If System.IO.File.Exists(file) Then
            System.IO.File.Delete(file)
        End If
    End Sub



End Class
