Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports Master
Imports EventSakusei
Imports ShisakuCommon.ShisakuGlobal
Imports ShisakuCommon
Imports ShisakuCommon.Ui

''' <summary>
''' 部品構成表示画面
''' </summary>
''' <remarks></remarks>

Public Class frm50ADMINmenu

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)

    End Sub

    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm50ADMINmenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
            'アドミンユーザー以外の場合、ログインマスターは使用不可能とする。            If LoginInfo.Now.LoginAuthority = USERMAST_AUTHORITY.ADMIN Then
                btnKengenMaster.Visible = True
                btnLoginMaster.Visible = True
            Else
                btnKengenMaster.Visible = True
                btnLoginMaster.Visible = False
            End If
            If LoginLogic.GetCurrUserInfo(LoginInfo.Now.UserId) = RESULT.OK Then
                ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)
            End If

        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try
    End Sub
    ''' <summary>
    ''' 権限マスタボタンを押す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKengenMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKengenMaster.Click
        Dim f As New frm51KengenMaster01
        Me.Hide()
        f.ShowDialog()
        Me.Show()
    End Sub
    ''' <summary>
    ''' ログインボタンを押す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLoginMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoginMaster.Click
        Dim f As New frm5LoginMaster01
        Me.Hide()
        f.ShowDialog()
        Me.Show()
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
    ''' timer tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub
    ''' <summary>
    ''' form 　閉じる
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm50ADMINmenu_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub

    ''' <summary>
    ''' ボディマスタ呼び出し
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBodyMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBodyMaster.Click
        Dim iBodyMst As New Master.FrmBodyMSTMainte()

        iBodyMst.Initialize_Form()

        iBodyMst.ShowDialog(Me)

    End Sub
End Class