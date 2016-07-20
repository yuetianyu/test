Imports ShisakuCommon
Imports ShisakuCommon.Ui

Public Class frm41ShiyouKakunin

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

    End Sub

    Private Sub CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        result = MsgBoxResult.Ok
        Me.Close()
    End Sub

    ''' <summary>
    ''' 確認画面を表示する(OKのみ)
    ''' </summary>
    ''' <param name="message">メッセージ</param>
    ''' <param name="message2">メッセージ2行目</param>
    ''' <returns>MsgBoxResult</returns>
    ''' <remarks></remarks>
    Public Shared Function ConfirmOk(ByVal message As String, Optional ByVal message2 As String = Nothing) As MsgBoxResult
        Return Confirm(message, message2, frm41ShiyouKakunin.OK.Text)
    End Function

    ''' <summary>
    ''' 確認画面を表示する
    ''' </summary>
    ''' <param name="message">メッセージ</param>
    ''' <param name="message2">メッセージ2行目</param>
    ''' <returns>MsgBoxResult</returns>
    ''' <remarks></remarks>
    Private Shared Function Confirm(ByVal message As String, ByVal message2 As String, ByVal okLabel As String) As MsgBoxResult
        Using frm As New frm41ShiyouKakunin
            ResolveShowWidth(frm, message, message2)
            frm.OK.Text = okLabel
            frm.lblKakunin.Text = message
            If Not StringUtil.IsEmpty(message2) Then
                frm.lblKakunin2.Text = message2
            End If
            frm.ShowDialog()
            Return frm.result
        End Using
    End Function

    ''' <summary>
    ''' メッセージが長くて途中で途切れないよう、幅を拡げる
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="message">メッセージ</param>
    ''' <param name="message2">メッセージ2行目</param>
    ''' <remarks></remarks>
    Private Shared Sub ResolveShowWidth(ByVal frm As frm41ShiyouKakunin, ByVal message As String, ByVal message2 As String)
        ' TODO 暫定処置。時間のある時に、もっと綺麗にしましょ
        Dim length As Integer
        If message2 IsNot Nothing AndAlso message2.Length < message2.Length Then
            length = message2.Length * 18
        Else
            length = message.Length * 18
        End If
        If frm.lblKakunin.Width < length Then
            frm.Width += length - frm.lblKakunin.Width
            frm.lblKakunin.Width = length
            frm.lblKakunin2.Width = length
        End If
    End Sub

    ''' <summary>
    ''' 確認画面（モーダレス）を表示する
    ''' </summary>
    ''' <param name="message">メッセージ</param>
    ''' <param name="message2">メッセージ2行目</param>
    ''' <param name="_aFormCloser">終了処理</param>
    ''' <remarks></remarks>
    Public Shared Sub ConfirmShow(ByVal message As String, ByVal message2 As String, ByVal _aFormCloser As IFormCloser)
        Dim frm As New frm41ShiyouKakunin
        frm.lblKakunin.Text = message
        If Not StringUtil.IsEmpty(message2) Then
            frm.lblKakunin2.Text = message2
        End If
        frm._aFormCloser = _aFormCloser
        frm.Show()
    End Sub

    Private Sub frm41ShiyouKakunin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If result <> MsgBoxResult.Ok Then
            result = MsgBoxResult.Ok
        End If
        If _aFormCloser Is Nothing Then
            Return
        End If
        _aFormCloser.FormClose(result = MsgBoxResult.Ok)
    End Sub
End Class