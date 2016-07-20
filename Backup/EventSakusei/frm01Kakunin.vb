Imports ShisakuCommon
Imports ShisakuCommon.Ui

Public Class frm01Kakunin

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

    Private Sub CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CANCEL.Click
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
    Public Shared Function ConfirmOkCancel(ByVal message As String, Optional ByVal message2 As String = Nothing, _
                                           Optional ByVal ALtenkai As String = Nothing) As MsgBoxResult
        Return Confirm(message, message2, frm01Kakunin.OK.Text, frm01Kakunin.CANCEL.Text, ALtenkai)
    End Function

    ''' <summary>
    ''' 確認画面を表示する(はい/いいえ)
    ''' </summary>
    ''' <param name="message">メッセージ</param>
    ''' <param name="message2">メッセージ2行目</param>
    ''' <returns>MsgBoxResult</returns>
    ''' <remarks></remarks>
    Public Shared Function ConfirmYesNo(ByVal message As String, Optional ByVal message2 As String = Nothing) As MsgBoxResult
        If Confirm(message, message2, "はい", "いいえ") <> MsgBoxResult.Ok Then
            Return MsgBoxResult.No
        End If
        Return MsgBoxResult.Yes
    End Function

    ''' <summary>
    ''' 確認画面を表示する
    ''' </summary>
    ''' <param name="message">メッセージ</param>
    ''' <param name="message2">メッセージ2行目</param>
    ''' <returns>MsgBoxResult</returns>
    ''' <remarks></remarks>
    Private Shared Function Confirm(ByVal message As String, ByVal message2 As String, ByVal okLabel As String, _
                                    ByVal cancelLabel As String, _
                                    Optional ByVal ALtenkai As String = Nothing) As MsgBoxResult
        Using frm As New frm01Kakunin
            ResolveShowWidth(frm, message, message2)
            frm.OK.Text = okLabel
            frm.CANCEL.Text = cancelLabel
            frm.lblKakunin.Text = message
            If Not StringUtil.IsEmpty(message2) Then
                frm.lblKakunin2.Text = message2
            End If
            'AL再展開より呼び出された場合、Nothing以外となる。
            If Not StringUtil.Equals(ALtenkai, Nothing) Then
                frm.lblKakunin2.ForeColor = Color.Red
                frm.lblKakunin2.Font = New Font("MS UI Gothic", 12, FontStyle.Bold)
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
    Private Shared Sub ResolveShowWidth(ByVal frm As frm01Kakunin, ByVal message As String, ByVal message2 As String)
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

            'ボタンの位置も調整しましょう！！
            frm.OK.Left = frm.OK.Left * message.Length / 11
            frm.CANCEL.Left = frm.OK.Left + 87
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
        Dim frm As New frm01Kakunin
        frm.lblKakunin.Text = message
        If Not StringUtil.IsEmpty(message2) Then
            frm.lblKakunin2.Text = message2
        End If
        frm._aFormCloser = _aFormCloser
        frm.ShowDialog()
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
End Class