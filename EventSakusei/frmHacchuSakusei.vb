Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports EventSakusei.TehaichoMenu
Imports ShisakuCommon.Ui.Valid
Imports EBom.Common

Public Class frmHacchuSakusei

    Private result As MsgBoxResult
    Private _aFormCloser As IFormCloser

    ''' <summary>
    ''' #ConfirmShow の時の終了処理インターフェース
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IFormCloser
        ''' <summary>
        ''' フォームを閉じる時の処理
        ''' </summary>
        ''' <param name="IsOk">OKが押された場合、true</param>
        ''' <remarks></remarks>
        Sub FormClose(ByVal IsOk As Boolean)
    End Interface

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)
        ShisakuFormUtil.setTitleVersion(Me)
        InitializeValidatorOldListCode()

    End Sub

    Private Sub CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CANCEL.Click
        CType(Me.Owner, frm19DispTehaichoMenu).CanselFlg = True
        'パラメータクリア
        frm7Para = ""
        frm35Para = ""
        frm37Para = ""
        frm38Para = ""
        'frm41Para = ""
        'frm8Para = ""
        'frm9Para = ""
        'frm10Para = ""
        frm18Para = ""
        frm20Para = ""
        frm7ParaModori = ""
        frm35ParaModori = ""
        frm37ParaModori = ""
        frm38ParaModori = ""
        'frm41ParaModori = ""
        'frm8ParaModori = ""
        'frm9ParaModori = ""
        'frm10ParaModori = ""
        frm18ParaModori = ""
        frm19ParaModori = ""
        frm20ParaModori = ""
        frm21ParaModori = ""

        result = MsgBoxResult.Cancel
        Me.Close()
    End Sub

    Private errorController As New ErrorController()

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        errorController.ClearBackColor()
        Try
            validatorOldListCode.AssertValidate()
            '旧リストコードを手配帳編集メニューに渡す'
            CType(Me.Owner, frm19DispTehaichoMenu).OldListCode = txtOldListCode.Text
        Catch ex As IllegalInputException
            errorController.SetBackColorOnError(ex.ErrorControls)
            errorController.FocusAtFirstControl(ex.ErrorControls)
            ''エラーメッセージ
            ComFunc.ShowErrMsgBox(ex.Message)
            Return
        End Try

        result = MsgBoxResult.Ok



        Me.Close()

        Select Case frm7Para
            Case "btnCall"
                frm7ParaModori = "1"
            Case "btnNEW"
                frm7ParaModori = "2"
            Case "btnDel"
                frm7ParaModori = "3"
            Case Else
                frm7ParaModori = ""
        End Select

        Select Case frm35Para
            Case "btnCall"
                frm35ParaModori = "1"
            Case Else
                frm35ParaModori = ""
        End Select

        Select Case frm37Para
            Case "btnCall"
                frm37ParaModori = "1"
            Case Else
                frm37ParaModori = ""
        End Select

        Select Case frm38Para
            Case "btnCall"
                frm38ParaModori = "1"
            Case "btnSyounin"
                frm38ParaModori = "S"
            Case Else
                frm38ParaModori = ""
        End Select

        'Select Case frm41Para
        '    Case "btnADD"
        '        frm41ParaModori = "1"
        '    Case "btnCall"
        '        frm41ParaModori = "2"
        '    Case "btnTenkai"
        '        frm41ParaModori = "3"
        '    Case Else
        '        frm41ParaModori = ""
        'End Select

        'Select Case frm8Para
        '    Case "btnSekeiTenkai"
        '        frm8ParaModori = "1"
        '    Case "btnSasimodosi"
        '        frm8ParaModori = "2"
        '    Case "btnSasimodosi2"
        '        frm8ParaModori = "22"
        '    Case "btnSimekiri"
        '        frm8ParaModori = "3"
        '    Case "btnKanryou"
        '        frm8ParaModori = "4"
        '    Case "btnChushi"
        '        frm8ParaModori = "5"
        '    Case Else
        '        frm8ParaModori = ""
        'End Select
        'Select Case frm9Para
        '    Case "btnADD"
        '        frm9ParaModori = "1"
        '    Case "btnHozon"
        '        frm9ParaModori = "2"
        '    Case "btnBACK"
        '        frm9ParaModori = "B"
        '    Case "btnEND"
        '        frm9ParaModori = "E"
        '    Case Else
        '        frm9ParaModori = ""
        'End Select
        'Select Case frm10Para
        '    Case "btnCall"
        '        frm10ParaModori = "1"
        '    Case Else
        '        frm10ParaModori = ""
        'End Select

        Select Case frm18Para
            Case "btnADD"
                frm18ParaModori = "1"
            Case Else
                frm18ParaModori = ""
        End Select

        Select Case frm19Para
            Case "btnChusyutu"
                frm19ParaModori = "1"
            Case "btnHacchuSakusei"
                frm19ParaModori = "H"
            Case "btnSaiHensyu"
                frm19ParaModori = "M"
            Case Else
                frm19ParaModori = ""
        End Select

        Select Case frm20Para
            Case "btnHozon"
                frm20ParaModori = "1"
            Case "btnBACK"
                frm20ParaModori = "B"
            Case Else
                frm20ParaModori = ""
        End Select

        Select Case frm21Para
            Case "btnADD"
                frm21ParaModori = "1"
            Case Else
                frm21ParaModori = ""
        End Select

    End Sub

    ''' <summary>
    ''' 確認画面を表示する(OK/Cancel)
    ''' </summary>
    ''' <param name="message">メッセージ</param>
    ''' <param name="message2">メッセージ2行目</param>
    ''' <returns>MsgBoxResult</returns>
    ''' <remarks></remarks>
    Public Shared Function ConfirmOkCancel(ByVal message As String, Optional ByVal message2 As String = Nothing) As MsgBoxResult
        Return Confirm(message, message2, frmHacchuSakusei.OK.Text, frmHacchuSakusei.CANCEL.Text)
    End Function

    ''' <summary>
    ''' 確認画面を表示する(はい/いいえ)
    ''' </summary>
    ''' <param name="message">メッセージ</param>
    ''' <param name="message2">メッセージ2行目</param>
    ''' <returns>MsgBoxResult</returns>
    ''' <remarks></remarks>
    Public Shared Function ConfirmYesNo(ByVal message As String, Optional ByVal message2 As String = Nothing) As MsgBoxResult
        If Confirm(message, message2, "OK", "CANCEL") <> MsgBoxResult.Ok Then
            Return MsgBoxResult.No
        End If

        Return MsgBoxResult.Yes
    End Function

    ''' <summary>
    ''' 確認画面を表示する
    ''' </summary>
    ''' <param name="message">メッセージ</param>
    ''' <returns>MsgBoxResult</returns>
    ''' <remarks></remarks>
    Private Shared Function Confirm(ByVal message As String, ByVal message2 As String, ByVal okLabel As String, ByVal cancelLabel As String) As MsgBoxResult
        Using frm As New frmHacchuSakusei
            ResolveShowWidth(frm, message)
            frm.OK.Text = okLabel
            frm.CANCEL.Text = cancelLabel
            frm.lblKakunin.Text = message
            frm.ShowDialog()
            Return frm.result
        End Using
    End Function

    ''' <summary>
    ''' メッセージが長くて途中で途切れないよう、幅を拡げる
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="message">メッセージ</param>
    ''' <remarks></remarks>
    Private Shared Sub ResolveShowWidth(ByVal frm As frmHacchuSakusei, ByVal message As String)
        ' TODO 暫定処置。時間のある時に、もっと綺麗にしましょ
        Dim length As Integer

        length = message.Length * 18

        If frm.lblKakunin.Width < length Then
            frm.Width += length - frm.lblKakunin.Width
            frm.lblKakunin.Width = length
        End If
    End Sub

    ''' <summary>
    ''' 確認画面（モーダレス）を表示する
    ''' </summary>
    ''' <param name="message">メッセージ</param>
    ''' <param name="_aFormCloser">終了処理</param>
    ''' <remarks></remarks>
    Public Shared Sub ConfirmShow(ByVal message As String, ByVal _aFormCloser As IFormCloser)
        ConfirmShow(message, Nothing, _aFormCloser)
    End Sub
    ''' <summary>
    ''' 確認画面（モーダレス）を表示する
    ''' </summary>
    ''' <param name="message">メッセージ</param>
    ''' <param name="message2">メッセージ2行目</param>
    ''' <param name="_aFormCloser">終了処理</param>
    ''' <remarks></remarks>
    Public Shared Sub ConfirmShow(ByVal message As String, ByVal message2 As String, ByVal _aFormCloser As IFormCloser)
        Dim frm As New frmHacchuSakusei
        frm.lblKakunin.Text = message
        frm._aFormCloser = _aFormCloser
        frm.Show()
    End Sub

    Private Sub frm01Kakunin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If result <> MsgBoxResult.Ok Then
            result = MsgBoxResult.Cancel
        End If
        If _aFormCloser Is Nothing Then
            Return
        End If
        _aFormCloser.FormClose(result = MsgBoxResult.Ok)
    End Sub


#Region "エラーチェック"
    Private validatorOldListCode As Validator

    Private Sub InitializeValidatorOldListCode()
        validatorOldListCode = New Validator

        Dim OldListCodeRequired As New Validator("リストコードを入力してください")
        OldListCodeRequired.Add(txtOldListCode).Required()

        validatorOldListCode.Add(OldListCodeRequired)
    End Sub

#End Region

#Region "入力制限"

    '英語を大文字に変更'
    Private Sub txtOldListCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOldListCode.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    ''' <summary>
    ''' 旧リストコードに全角文字が入力されているかを返す
    ''' </summary>
    ''' <param name="OldListCode">旧リストコード</param>
    ''' <returns>判定結果</returns>
    ''' <remarks></remarks>
    Private Function txtOldListCode_validate(ByVal OldListCode As Char) As Boolean
        Dim moji As Integer = Len(OldListCode)
        Dim bytecount As Integer = System.Text.Encoding.GetEncoding("Shift-JIS").GetByteCount(OldListCode.ToString())
        If moji <> bytecount Then
            Return False
        End If
        Return True
    End Function



#End Region

End Class