Imports EBom.Common
Imports FarPoint.Win
Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui

''' <summary>
''' 部品構成表示画面
''' </summary>
''' <remarks></remarks>

Public Class frm5LoginMaster02

    Private changKey As Integer

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)

    End Sub

    Private Sub txtListCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frm5LoginMaster01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            ShisakuFormUtil.setTitleVersion(Me)

            '画面の時間が表示されます。
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
            '画面の時間が表示されます。
            ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)
            '画面のPG-IDが表示されます。
            LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_03
            '画面のPG-Nameが表示されます。
            LblCurrPGName.Text = PROGRAM_NAME_03

            lblPsd.Focus()

            changKey = 0
            lblMsg.Text = T0009
        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try
    End Sub
    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        If changKey = 0 Then
            Me.Close()
        Else

            frm01Kakunin.lblKakunin.Text = "変更を更新せずに終了しますか？"
            frm01Kakunin.lblKakunin2.Text = ""
            frm5Para = "btnBACK"
            frm01Kakunin.ShowDialog()
            Select Case frm5ParaModori
                Case "B"
                    Me.Close()
            End Select
        End If
    End Sub

    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
        frm01Kakunin.lblKakunin.Text = "変更を更新せずに終了しますか？"
        frm01Kakunin.lblKakunin2.Text = ""
        frm5Para = "btnEND"
        frm01Kakunin.ShowDialog()
        Select Case frm5ParaModori
            Case "E"
                Application.Exit()
        End Select
    End Sub

    Private Sub btnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click

        frm01Kakunin.lblKakunin.Text = "更新を実行しますか？"
        frm01Kakunin.lblKakunin2.Text = ""
        frm5Para = "btnCall"

        frm01Kakunin.ShowDialog()
        Select Case frm5ParaModori
            Case "1"
                Try
                    checkLoginMaster()
                    toUpdateLoginMaster()
                    Me.Close()
                Catch ignore As ShisakuException
                    '' ignore 入力値エラーの時の例外だから無視
                End Try
            Case "2"
                Me.Close()
            Case Else
        End Select
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frm01Kakunin.lblKakunin.Text = "削除を実行しますか？"
        frm01Kakunin.lblKakunin2.Text = ""
        frm5Para = "btnDel"
        frm01Kakunin.ShowDialog()
        Select Case frm5ParaModori
            Case "1"
                Me.Close()
            Case "2"
                Me.Close()
            Case Else
        End Select
    End Sub

#Region " 更新機能準備します "
    ''' <summary>
    ''' 追加或いは更新機能
    ''' </summary>
    ''' <returns>
    ''' ログインマスタに存在、DBに更新正常なら、RESULT.OK
    ''' ログインマスタに存在しません、DBに追加正常なら、RESULT.OK
    ''' DB処理エラーの場合、RESULT.NG
    ''' </returns>
    ''' <remarks></remarks>
    Private Function toUpdateLoginMaster() As RESULT
        Dim loginMaster = New LoginMaster02()

        Dim userId = Me.lblUserCode.Text.ToString()
        Dim userName = Me.lblUserName.Text.ToString()
        Dim psd = Me.lblPsd.Text.ToString()
        Dim userKbn = Me.lblUserkbn.Text.ToString()
        Dim menuSet = loginMaster.getSetteiId(spdParts_Sheet1.Cells(0, 1).Value)
        Dim kengenSet = loginMaster.getSetteiId(spdParts_Sheet1.Cells(1, 1).Value)

        Dim checkExist = loginMaster.isExist(userId)

        If (checkExist = RESULT.OK) Then
            Return loginMaster.update(userId, psd, menuSet, kengenSet)
        ElseIf (checkExist = RESULT.NO_DATA) Then
            '下の""の部分はUSER_KBNです、前画面から貰いました
            Return loginMaster.add(userId, psd, userKbn, menuSet, kengenSet)
        End If

    End Function
#End Region

#Region "コントロールの色を最初化"

    Private Sub initlColor()
        ShisakuFormUtil.initlColor(lblPsd)
        ShisakuFormUtil.initlColor(lblPsdKakunin)
        ShisakuFormUtil.initlColor(spdParts_Sheet1.Cells(0, 1))
        ShisakuFormUtil.initlColor(spdParts_Sheet1.Cells(1, 1))
    End Sub

#End Region

#Region " 画面入力物のチェック "

    Private Sub checkLoginMaster()
        initlColor()
        Dim loginMaster = New LoginMaster02()
        Dim menuSet = loginMaster.getSetteiId(spdParts_Sheet1.Cells(0, 1).Value)
        Dim kengenSet = loginMaster.getSetteiId(spdParts_Sheet1.Cells(1, 1).Value)
        Dim userKbn = lblUserkbn.Text.ToString()
        If userKbn = String.Empty Then
            ComFunc.ShowErrMsgBox(SYSERR_00001, "", "")
        End If
        If (Me.lblPsd.Text.ToString() = String.Empty) Then
            'エラーメセッジを出ます
            ShisakuFormUtil.onErro(lblPsd)
            ComFunc.ShowErrMsgBox(ShisakuMsg.E0005, "")
            lblPsd.Focus()
            Throw New ShisakuException()
        End If
        If (Me.lblPsdKakunin.Text.ToString() = String.Empty) Then
            'エラーメセッジを出ます            ShisakuFormUtil.onErro(lblPsdKakunin)
            ComFunc.ShowErrMsgBox(ShisakuMsg.E0006, "")
            lblPsdKakunin.Focus()
            Throw New ShisakuException()
        ElseIf (Not Me.lblPsd.Text.ToString() = Me.lblPsdKakunin.Text.ToString()) Then
            'エラーメセッジを出ます            ShisakuFormUtil.onErro(lblPsd)
            ShisakuFormUtil.onErro(lblPsdKakunin)
            lblPsd.Text = ""
            lblPsdKakunin.Text = ""
            ComFunc.ShowErrMsgBox(ShisakuMsg.E0007, "")
            lblPsd.Focus()
            Throw New ShisakuException()
        ElseIf (menuSet = String.Empty) Then
            'エラーメセッジを出ます
            ShisakuFormUtil.onErro(spdParts_Sheet1.Cells(0, 1))
            ComFunc.ShowErrMsgBox(ShisakuMsg.E0008, "")
            spdParts_Sheet1.SetActiveCell(0, 1)
            Throw New ShisakuException()
        ElseIf (kengenSet = String.Empty) Then
            'エラーメセッジを出ます            ShisakuFormUtil.onErro(spdParts_Sheet1.Cells(1, 1))
            ComFunc.ShowErrMsgBox(ShisakuMsg.E0008, "")
            spdParts_Sheet1.SetActiveCell(1, 1)
            Throw New ShisakuException()
        End If

    End Sub

#End Region

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
    End Sub

    Private Sub keyChang()
        changKey = 1
    End Sub

    Private Sub lblPsd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPsd.TextChanged
        keyChang()
    End Sub

    Private Sub lblPsdKakunin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPsdKakunin.TextChanged
        keyChang()
        lblMsg.Text = T0009
    End Sub

    Private Sub spdParts_Change(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdParts.Change
        keyChang()
    End Sub
End Class