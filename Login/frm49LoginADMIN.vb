Imports EBom.Common
Imports EBom.Data
Imports ShisakuCommon
Imports ShisakuCommon.Ui

''' <summary>
''' 管理者用のログイン
''' </summary>
''' <remarks></remarks>

Public Class frm49LoginADMIN
    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm49LoginADMIN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)

            'ログの初期化

            LoginLogic.InitLogging()

        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try
    End Sub
    ''' <summary>
    ''' ログインボタンを押す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        'Kanrihyo.ini 初期化

        If Not ComFunc.InitIni(g_kanrihyoIni, mdlConstraint.INI_KANRIHYO_FILE) = RESULT.OK Then
            ComFunc.ShowErrMsgBox(SYSERR_00001)
            Exit Sub
        End If

        'If isOhtaTest Then
        '    '' 太田工場デバッグ用
        '    'DEBUG 時は SKATST 環境固定
        '    'g_kanrihyoIni("EBOM_DB") = "DRIVER={SQL Server};SERVER=fgnt43;UID=sa;PWD=@Fgnt43@;DATABASE=RHACLIBF_GJ1TST;"
        '    'g_kanrihyoIni("KOSEI_DB") = "DRIVER={SQL Server};SERVER=fgnt43;UID=sa;PWD=@Fgnt43@;DATABASE=EBOM_GJ1TST;"
        '    'g_kanrihyoIni("mBOM_DB") = "DRIVER={SQL Server};SERVER=fgnt43;UID=sa;PWD=@Fgnt13@;DATABASE=mBOM;"

        '    'g_kanrihyoIni("EBOM_DB") = "DRIVER={SQL Server};SERVER=FGNT-SQL1;UID=sa;PWD=@Fgnt13@;DATABASE=RHACLIBF;"
        '    'g_kanrihyoIni("KOSEI_DB") = "DRIVER={SQL Server};SERVER=FGNT-SQL1;UID=sa;PWD=@Fgnt13@;DATABASE=EBOM;"
        '    'g_kanrihyoIni("mBOM_DB") = "DRIVER={SQL Server};SERVER=FGNT-SQL1;UID=sa;PWD=@Fgnt13@;DATABASE=mBom;"

        'End If

        RHACLIBF_DB_NAME = ComFunc.GetDatabaseName(g_kanrihyoIni("EBOM_DB"))
        EBOM_DB_NAME = ComFunc.GetDatabaseName(g_kanrihyoIni("KOSEI_DB"))
        MBOM_DB_NAME = ComFunc.GetDatabaseName(g_kanrihyoIni("mBOM_DB"))

        If Not IsValid() = RESULT.OK Then
            Exit Sub
        End If
        If Not UserCheck() = RESULT.OK Then
            Exit Sub
        End If
        Me.Hide()
        frm50ADMINmenu.Show()

    End Sub
    ''' <summary>
    ''' アプリケーション終了ボタンを押す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
        Application.Exit()
    End Sub
    ''' <summary>
    ''' miExit click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub miExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Application.Exit() ' アプリケーションの終了


    End Sub
    ''' <summary>
    ''' form 閉じる
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm49LoginADMIN_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        e.Cancel = True ' 終了処理のキャンセル
        Me.Visible = False ' フォームの非表示
        Application.Exit()
    End Sub

    ''' <summary>
    ''' time tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub
    ''' <summary>
    ''' ログインid、パスウード、権限のチェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Function UserCheck() As RESULT
        Dim dtLogin As New DataTable()
        Dim dtUser As New DataTable()
        Dim userId As String = txtLOGIN.Text
        Dim passWord As String = txtPW.Text
        If userId = ShisakuGlobal.ADMIN_USER_ID Then
            'アドミンユーザー
            If Not passWord = ShisakuGlobal.ADMIN_USER_PW Then
                ComFunc.ShowErrMsgBox(ShisakuMsg.E0001)
                Return RESULT.NG
            End If
            LoginInfo.Now.LoginAuthority = USERMAST_AUTHORITY.ADMIN
        Else 'アドミンユーザー以外の場合

            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_KOSEI))
                db.Open()
                If Not userId = String.Empty Then
                    db.AddParameter("@USER_ID", userId, DbType.AnsiString)
                End If
                db.Fill(DataSqlCommon.GetUserMasSql(), dtUser)
                'ログインIDが「ユーザーマスター」に登録されていない場合。
                If dtUser.Rows.Count < 1 Then
                    ComFunc.ShowErrMsgBox(EL003 + EL004)
                    Return RESULT.NG
                End If
            End Using
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                If Not userId = String.Empty Then
                    db.AddParameter("@SEKKEI_SHAIN_NO", userId, DbType.AnsiString)
                End If
                db.Fill(DataSqlCommon.GetLoginSql(), dtLogin)
                'ログインＩＤが「ログインマスター」に登録されていない。
                If dtLogin.Rows.Count < 1 Then
                    ComFunc.ShowErrMsgBox(ShisakuMsg.E0002)
                    Return RESULT.NG
                End If
                'ログインＩＤが「ログインマスター」に登録されているが、パスワードが一致しない場合。
                If Not dtLogin.Rows(0)("PASSWORD").Equals(passWord) Then
                    ComFunc.ShowErrMsgBox(ShisakuMsg.E0001)
                    Return RESULT.NG
                End If
                LoginInfo.Now.LoginAuthority = dtUser.Rows(0)("COMPETENT")
            End Using

        End If
        LoginInfo.Now.UserId = userId
        Return RESULT.OK
    End Function
    ''' <summary>
    ''' 画面項目の必要のチェック
    ''' </summary>
    ''' <returns>RESULT</returns>
    ''' <remarks></remarks>
    Private Function IsValid() As RESULT
        If txtLOGIN.Text.Trim = String.Empty Then
            ComFunc.ShowInfoMsgBox(String.Format(ShisakuMsg.I0001, "ログインＩＤ"))
            txtLOGIN.Focus()
            Return RESULT.CANCEL
        End If
        If txtPW.Text.Trim = String.Empty Then
            ComFunc.ShowInfoMsgBox(String.Format(ShisakuMsg.I0001, "パスワード"))
            txtPW.Focus()
            Return RESULT.CANCEL
        End If
        Return RESULT.OK
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPW_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPW.Enter
        LblMessage.Text = String.Format(ShisakuMsg.I0001, "パスワード")
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtLOGIN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLOGIN.Enter
        LblMessage.Text = String.Format(ShisakuMsg.I0001, "ログインＩＤ")
    End Sub



    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)
        ' Add any initialization after the InitializeComponent() call.
        Dim handler As New ThreadExceptionHandler
        handler.HandleSetting()
    End Sub

    Private Sub txtLOGIN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLOGIN.KeyDown
        If e.KeyValue = Keys.Enter Then
            btnLogin_Click(sender, e)
        End If
    End Sub

    Private Sub txtPW_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPW.KeyDown
        If e.KeyValue = Keys.Enter Then
            btnLogin_Click(sender, e)
        End If
    End Sub

    Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint

    End Sub
End Class