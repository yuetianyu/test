Imports EBom.Common
Imports EBom.Data
Imports System.Reflection
Imports EBomMenu.Authentication
Imports EBom.Common.mdlConstraint
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEditSekkei


Public Class frm01Login

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.
    '承認チェック用部課コード
    Private _strBukaCode As String
    ''' <summary>
    ''' スタートモード（0:通常スタート　1:レジストリのRunからの起動）
    ''' </summary>
    ''' <remarks></remarks>
    Private _startMode As Integer
    Private Const START_MODE_NORMAL As Integer = 0
    Private Const START_MODE_AUTO As Integer = 1


    Private Sub frm01Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            '2012/02/13 レジストリからの起動は中止
            'ログオンスクリプトからの起動に変更の為
            '↓↓↓↓↓
            'パソコン起動時に実行されるようにレジストリに記述
            'SetCurrentVersionRun()
            DeleteCurrentVersionRun()

            '起動時のコマンドラインを受け取る
            Dim args() As String = Environment.GetCommandLineArgs()

            If args.Length = 1 Then
                _startMode = START_MODE_NORMAL
            Else
                _startMode = START_MODE_AUTO
            End If

            ToolStripManager.Renderer = New ToolStripProfessionalRenderer( _
                     New EBom.Controls.CustomProfessionalRenderer())

            'ログの初期化

            LoginLogic.InitLogging(Assembly.GetExecutingAssembly.GetName.Name)
#If DEBUG Then
            g_log.WriteInfo("**************************************************")
            g_log.WriteInfo("--ログイン！")

#End If
            InitApplication()

            'ここで要承認データ有無を確認
            Dim loginUserName As String
            Dim strBukaCode As String
            'ログインユーザを取得
            loginUserName = System.Environment.UserName


#If DEBUG Then
            'loginUserName = "0000005"
            'loginUserName = "test01"
            'loginUserName = "088267"
#End If
            loginUserName = "088267"
            'Dim isFirstTimeToday As Boolean = False

            '初回起動のチェック
            'isFirstTimeToday = IsFirstTiemToday()

            '------------------------------------------------------------------------------------------------
            '------------------------------------------------------------------------------------------------
            ''''バージョン０１　既存のバージョンチェックを実行してバージョンチェックを行う。
            'バージョンチェックを行う。
            '　※再起動を促す。
            'Dim objProcess As Process = New Process
            'objProcess.StartInfo.FileName = "C:\Progra~1\GS2-Presents\TrialManufacture\shisakuautostart.exe"
            'objProcess.StartInfo.Arguments = "1 2"
            'objProcess.StartInfo.UseShellExecute = False
            'objProcess.Start()


            '''''バージョン０２　既存のバージョンチェックのソースをコピー＆カスタマイズしてバージョンチェックを行う。
            '''''                バージョンが違ったらシステムを強制終了。
            'Dim iFlg As Integer
            'iFlg = 0
            'If compareCreatedTime() Then
            '    'System.Diagnostics.Process.Start("C:\Program Files\GS2-Presents\TrialManufacture\ShisakuSystem.exe", "s")
            '    iFlg = 1
            'End If
            'If iFlg = 0 Then
            '    MsgBox("試作手配システムのバージョンが最新ではありません。" & vbCrLf & vbCrLf & "[GJ1 TOOLS]より[試作手配システム]を実行して下さい。" & vbCrLf & "システムのバージョンが最新に更新されます。", MsgBoxStyle.Exclamation, "試作手配システム")
            '    Me.Close()
            '    Application.Exit()
            '    Return
            'End If
            '------------------------------------------------------------------------------------------------
            '------------------------------------------------------------------------------------------------


            strBukaCode = LoginLogic.IsExistsApprovalData(loginUserName)
            Me._strBukaCode = strBukaCode
            '            If Not strBukaCode Is Nothing Then
            '#If DEBUG Then
            '                g_log.WriteInfo("承認権限あり")
            '#End If
            '                Dim fMiShonin As New Frm31MiShoninEventList(_strBukaCode)
            '                Dim lt As List(Of Hashtable) = fMiShonin.FindMiShoninBlock()
            '                If lt.Count = 0 Then
            '#If DEBUG Then
            '                    g_log.WriteInfo("承認データ無し")
            '#End If
            '                    fMiShonin.Dispose()
            '                    If _startMode = START_MODE_AUTO Then
            '#If DEBUG Then
            '                        g_log.WriteInfo("AUTOモード・・・ゆえに終了")
            '#End If
            '                        Me.Dispose()
            '                        Application.Exit()
            '                        Return
            '                    End If
            '#If DEBUG Then
            '                    g_log.WriteInfo("NOMALモード")
            '#End If
            '                End If
            '            Else
            '#If DEBUG Then
            '                g_log.WriteInfo("承認権限無し")
            '#End If
            '                If _startMode = START_MODE_AUTO Then
            '#If DEBUG Then
            '                    g_log.WriteInfo("AUTOモード・・・ゆえに終了")
            '#End If
            '                    Me.Dispose()
            '                    Application.Exit()
            '                    Return
            '                End If
            '            End If

            '            If isOhtaTest Then
            '                '重工さんテスト環境
            '            Else
            '                '重工さん本番環境では以下を使用する。

            '                '認証
            '                Dim ret As Boolean
            '                If EBomAuthentication.Authenticate(ret) = RESULT.NG Then
            '                    Me.Close()
            '                    Application.Exit()
            '                    Return
            '                End If
            '                If Not ret Then
            '                    Me.Close()
            '                    Application.Exit()
            '                    Return
            '                End If

            '                If EBomAuthentication.Authenticate(ret) = RESULT.NG Then
            '                    ComFunc.ShowInfoMsgBox(EL001)
            '                    g_log.WriteWarning(EL001)
            '                    Me.Close()
            '                    Application.Exit()
            '                    Return
            '                End If
            '            End If

            'InitApplication()

            login()

        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try
    End Sub

#Region " アプリケーション初期化 "
    Private Shared Function InitApplication() As RESULT
        Try
            '環境変数確認


            Dim ebomEnv As String = ComFunc.ChkEBomEnv()

            If ebomEnv = String.Empty Then
                Return RESULT.NG
            End If

            '職番とSDISINIをログに出力する。
            g_log.WriteInfo("USER ID:'{0}'\n" & _
                            "SDISINI:'{1}'", _
                            System.Environment.UserName, ebomEnv)

            'Kanrihyo.ini 初期化

            If Not ComFunc.InitIni(g_kanrihyoIni, mdlConstraint.INI_KANRIHYO_FILE) = RESULT.OK Then
                Return RESULT.NG
            End If

            ''eBomSetup.ini(初期化)
            'If Not ComFunc.InitIni(g_ebomIni, mdlConstraint.INI_EBOM_SETUP_FILE) = RESULT.OK Then
            '    Return RESULT.NG
            'End If

            'If isOhtaTest Then
            '    '' 太田工場デバッグ用
            '    'DEBUG 時は SKATST 環境固定

            '    'g_kanrihyoIni("EBOM_DB") = "DRIVER={SQL Server};SERVER=DS-D498B90678C9\SQLEXPRESS;UID=sa;PWD=sa;DATABASE=RHACLIBF_SKATST;"
            '    'g_kanrihyoIni("KOSEI_DB") = "DRIVER={SQL Server};SERVER=DS-D498B90678C9\SQLEXPRESS;UID=sa;PWD=sa;DATABASE=EBOM_GJ1TST;"

            '    '現在のテスト用(設計展開などでテストができないので)'
            '    'g_kanrihyoIni("EBOM_DB") = "DRIVER={SQL Server};SERVER=fgnt43;UID=sa;PWD=@Fgnt43@;DATABASE=RHACLIBF_GJ1TST;"
            '    'g_kanrihyoIni("KOSEI_DB") = "DRIVER={SQL Server};SERVER=fgnt43;UID=sa;PWD=@Fgnt43@;DATABASE=EBOM_GJ1TST;"
            '    'g_kanrihyoIni("mBOM_DB") = "DRIVER={SQL Server};SERVER=fgnt43;UID=sa;PWD=@Fgnt43@;DATABASE=mBOM;"



            '    'g_kanrihyoIni("EBOM_DB") = "DRIVER={SQL Server};SERVER=FGNT-SQL1;UID=sa;PWD=@Fgnt13@;DATABASE=RHACLIBF;"
            '    'g_kanrihyoIni("KOSEI_DB") = "DRIVER={SQL Server};SERVER=FGNT-SQL1;UID=sa;PWD=@Fgnt13@;DATABASE=EBOM;"
            '    'g_kanrihyoIni("mBOM_DB") = "DRIVER={SQL Server};SERVER=FGNT-SQL1;UID=sa;PWD=@Fgnt13@;DATABASE=mBom;"

            'End If

            'g_log.WriteInfo("ebomsetup.csv RDBMS:  '{0}' - '{1}'\n" & _
            '                "kanrihyo.ini  EBom.DB:'{2}' - '{3}'\n" & _
            '                "kanrihyo.ini  WORK.DB:'{4}' - '{5}'", _
            '    ComFunc.GetServer(g_ebomIni("RDBMS")), _
            '    ComFunc.GetDatabaseName(g_ebomIni("RDBMS")), _
            '    ComFunc.GetServer(g_kanrihyoIni("EBOM_DB")), _
            '    ComFunc.GetDatabaseName(g_kanrihyoIni("EBOM_DB")), _
            '    ComFunc.GetServer(g_kanrihyoIni("KOSEI_DB")), _
            '    ComFunc.GetDatabaseName(g_kanrihyoIni("KOSEI_DB")))

            g_log.WriteInfo("kanrihyo.ini  EBom.DB:'{0}' - '{1}'\n" & _
                            "kanrihyo.ini  WORK.DB:'{2}' - '{3}'\n" & _
                            "kanrihyo.ini  mBOM.DB:'{4}' - '{5}'", _
                ComFunc.GetServer(g_kanrihyoIni("EBOM_DB")), _
                ComFunc.GetDatabaseName(g_kanrihyoIni("EBOM_DB")), _
                ComFunc.GetServer(g_kanrihyoIni("KOSEI_DB")), _
                ComFunc.GetDatabaseName(g_kanrihyoIni("KOSEI_DB")), _
                ComFunc.GetServer(g_kanrihyoIni("mBOM_DB")), _
                ComFunc.GetDatabaseName(g_kanrihyoIni("mBOM_DB")), _
                ComFunc.GetServer(g_kanrihyoIni("BRAKU_DB")), _
                ComFunc.GetDatabaseName(g_kanrihyoIni("BRAKU_DB")))

            RHACLIBF_DB_NAME = ComFunc.GetDatabaseName(g_kanrihyoIni("EBOM_DB"))
            EBOM_DB_NAME = ComFunc.GetDatabaseName(g_kanrihyoIni("KOSEI_DB"))
            'MBOM_DB_NAME = "mBOM_GJ1TST"
            MBOM_DB_NAME = ComFunc.GetDatabaseName(g_kanrihyoIni("mBOM_DB"))
            BRAKU_DB_NAME = ComFunc.GetDatabaseName(g_kanrihyoIni("BRAKU_DB"))

            'DB接続チェック
            If Not CanDbConnect() = RESULT.OK Then
                Return RESULT.NG
            End If

            Return RESULT.OK

        Catch ex As Exception
            ComFunc.ShowErrMsgBox(SYSERR_00001, _
                ex.Message, MethodBase.GetCurrentMethod.Name)
            g_log.WriteException(ex)
            Return RESULT.NG
        End Try
    End Function
#End Region

#Region " DB接続チェック "
    ''' <summary>
    ''' DB接続チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CanDbConnect() As RESULT
        Dim errCnStr As String = String.Empty       'エラー発生した接続文字列

        Try
            'EBOM DB
            errCnStr = g_kanrihyoIni("EBOM_DB")

            Using db As New SqlAccess(g_kanrihyoIni("EBOM_DB"))
                db.Open()
            End Using

            'WORK DB
            errCnStr = g_kanrihyoIni("KOSEI_DB")

            Using db As New SqlAccess(g_kanrihyoIni("KOSEI_DB"))
                db.Open()
            End Using

            'mBOM DB
            errCnStr = g_kanrihyoIni("mBOM_DB")

            Using db As New SqlAccess(g_kanrihyoIni("mBOM_DB"))
                db.Open()
            End Using


            'mBOM DB
            errCnStr = g_kanrihyoIni("BRAKU_DB")

            Using db As New SqlAccess(g_kanrihyoIni("BRAKU_DB"))
                db.Open()
            End Using

            Return RESULT.OK

        Catch ex As Exception
            ComFunc.ShowErrMsgBox("エラー {0} {1}", ex.Message, errCnStr)
            g_log.WriteException(ex)
            Return RESULT.NG
        End Try
    End Function
#End Region

    Public Sub login()

        Dim userId As String
        userId = retUserId
        If isOhtaTest Then
            '重工さんに用の設定
            'userId = "test01"
            userId = "088267"
            'userId = "0000005"
        End If

        userId = "088267"

        Dim loginClass = New LoginLogic()
        Dim checkResult = loginClass.checkLogin(userId)
        LoginInfo.Now.UserId = userId
        LoginInfo.Now.LoginAuthority = checkResult

        '柳沼修正　2012/10/09
        '   ここで部課コードをセットしておかないと承認した際にDBの部課コードが空になる。
        If LoginLogic.GetCurrUserInfo(LoginInfo.Now.UserId) = RESULT.OK Then
            'MsgBox(LoginInfo.Now.BukaCode)
        End If

        '承認が必要な部課があった場合（承認権限を有するユーザー）
        'ここで未承認のEvent一覧を表示する
        If Not StringUtil.IsEmpty(_strBukaCode) Then
            Dim fMiShonin As New Frm31MiShoninEventList(_strBukaCode)
            Dim lt As List(Of Hashtable) = fMiShonin.FindMiShoninBlock()
            If lt.Count = 0 Then
                fMiShonin.Dispose()
            Else
                Me.Hide()
                fMiShonin.ShowDialog()
                Me.Show()
            End If
        End If

        Select Case checkResult
            '↓↓↓2014/12/23 試作１課の権限チェックを追加 TES)張 ADD BEGIN
            Case LOGIN_AUTHORITY.USE_SISAKU_1KA
                Me.Hide()
                Dim f As New frm4NORMALmenu04
                If LoginLogic.GetCurrUserInfo(LoginInfo.Now.UserId) = RESULT.OK Then
                    ShisakuFormUtil.SetIdAndBuka(f.LblCurrUserId, f.LblCurrBukaName)
                End If
                f.Show()
                Me.Close()
                '↑↑↑2014/12/23 試作１課の権限チェックを追加 TES)張 ADD END
            Case LOGIN_AUTHORITY.USE_SISAKU
                Me.Hide()
                Dim f As New frm2NORMALmenu01
                If LoginLogic.GetCurrUserInfo(LoginInfo.Now.UserId) = RESULT.OK Then
                    ShisakuFormUtil.SetIdAndBuka(f.LblCurrUserId, f.LblCurrBukaName)
                End If
                f.Show()
                Me.Close()
            Case LOGIN_AUTHORITY.USE_SEKKEI
                Me.Hide()
                Dim f As New frm3NORMALmenu02
                If LoginLogic.GetCurrUserInfo(LoginInfo.Now.UserId) = RESULT.OK Then
                    ShisakuFormUtil.SetIdAndBuka(f.LblCurrUserId, f.LblCurrBukaName)
                End If
                f.Show()
                Me.Close()
            Case LOGIN_AUTHORITY.USE_NG
                Me.Close()
                Exit Sub
            Case Else
                ComFunc.ShowErrMsgBox(EL002)
                g_log.WriteWarning(EL002)
                Me.Close()
                Exit Sub
        End Select
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)
        ' Add any initialization after the InitializeComponent() call.
        Dim handler As New ThreadExceptionHandler
        handler.HandleSetting()
    End Sub
    ''' <summary>
    ''' パソコン起動時に実行するようにレジストリに書き込む
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCurrentVersionRun()
        Dim regkey As Microsoft.Win32.RegistryKey = _
            Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        regkey.SetValue(Application.ProductName, Application.ExecutablePath & " s")
        regkey.Close()
    End Sub
    ''' <summary>
    ''' レジストリの値を削除する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteCurrentVersionRun()
        Dim regkey As Microsoft.Win32.RegistryKey = _
            Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        'regkey.SetValue(Application.ProductName, Application.ExecutablePath & " s")
        regkey.DeleteValue(Application.ProductName, False)

        regkey.Close()
    End Sub

    ''' <summary>
    ''' バージョンチェックを行う。
    ''' </summary>
    ''' <remarks></remarks>
    Function compareCreatedTime() As Boolean

        Dim localFile As String
        Dim remoteFile As String

        Dim localFileLastWrite As Date
        Dim remoteFileLastWrite As Date

        localFile = "C:\Program Files\GS2-Presents\TrialManufacture\ShisakuSystem.exe"

        remoteFile = "\\Fgnt31\prog\raku2\TrialManufacture\ShisakuSystem.exe"

        '作成日時の取得（DateTime値を返す）
        '存在しないときは、UTCの 1601/01/01 9:00:00 を返す
        localFileLastWrite = System.IO.File.GetLastWriteTime(localFile)

        '作成日時の取得（DateTime値を返す）
        '存在しないときは、UTCの 1601/01/01 9:00:00 を返す
        remoteFileLastWrite = System.IO.File.GetLastWriteTime(remoteFile)


        If localFileLastWrite >= remoteFileLastWrite Then
            Return True
        Else
            Return False
        End If


    End Function

End Class
