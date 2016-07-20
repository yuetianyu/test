Imports ShisakuCommon
Imports ShisakuCommon.Ui

Public Class frm04Kakunin

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

    Private Sub CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CANCEL.Click
        result = MsgBoxResult.Cancel
        Me.Close()
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        result = MsgBoxResult.Ok
        Me.Close()
    End Sub

    ''' <summary>
    ''' 確認画面を表示する(OK/Cancel)
    ''' </summary>
    ''' <param name="message">メッセージ</param>
    ''' <param name="message2">メッセージ2行目</param>
    ''' <returns>MsgBoxResult</returns>
    ''' <remarks></remarks>
    Public Shared Function ConfirmOkCancel(ByVal message As String, _
                                           ByVal blockNo() As String, _
                                           ByVal kaiteiNo() As String, _
                                           ByVal jyotai() As String, _
                                           ByVal tanto() As String, ByVal i As Long, _
                                           Optional ByVal message2 As String = Nothing) As MsgBoxResult
        Return Confirm(message, message2, frm04Kakunin.OK.Text, frm04Kakunin.CANCEL.Text, _
                       blockNo, kaiteiNo, jyotai, tanto, i)
    End Function

    ''' <summary>
    ''' 確認画面を表示する
    ''' </summary>
    ''' <param name="message">メッセージ</param>
    ''' <param name="message2">メッセージ2行目</param>
    ''' <returns>MsgBoxResult</returns>
    ''' <remarks></remarks>
    Private Shared Function Confirm(ByVal message As String, ByVal message2 As String, ByVal okLabel As String, ByVal cancelLabel As String, _
                                    ByVal blockNo() As String, _
                                    ByVal kaiteiNo() As String, _
                                    ByVal jyotai() As String, _
                                    ByVal tanto() As String, ByVal i As Long) As MsgBoxResult
        Using frm As New frm04Kakunin
            frm.OK.Text = okLabel
            frm.CANCEL.Text = cancelLabel
            frm.lblKakunin.Text = message
            If Not StringUtil.IsEmpty(message2) Then
                frm.lblKakunin2.Text = message2
            End If
            ' DataGridView にデータを追加 
            For cnt As Long = 0 To i
                frm.DGView1.Rows.Add(blockNo(cnt), kaiteiNo(cnt), jyotai(cnt), tanto(cnt))
            Next

            frm.ShowDialog()
            Return frm.result
        End Using
    End Function

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
        Dim frm As New frm04Kakunin
        frm.lblKakunin.Text = message
        If Not StringUtil.IsEmpty(message2) Then
            frm.lblKakunin2.Text = message2
        End If
        frm._aFormCloser = _aFormCloser
        frm.ShowDialog()
    End Sub

    Private Sub frm04Kakunin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If result <> MsgBoxResult.Ok Then
            result = MsgBoxResult.Cancel
        End If
        If _aFormCloser Is Nothing Then
            Return
        End If
        _aFormCloser.FormClose(result = MsgBoxResult.Ok)
    End Sub

    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)
        ShisakuFormUtil.setTitleVersion(Me)

    End Sub
End Class