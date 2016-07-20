Imports ShisakuCommon.Ui

Public Class HoyouBuhinFrm00Kakunin
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

    Private Sub btnHanei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHanei.Click
        result = MsgBoxResult.Ok
        Me.Close()
    End Sub

    Private Sub btnModosu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModosu.Click
        Me.Close()
    End Sub

    Private Sub HoyouBuhinFrm00Kakunin_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If result <> MsgBoxResult.Ok Then
            result = MsgBoxResult.Cancel
        End If
        If _aFormCloser Is Nothing Then
            Return
        End If
        _aFormCloser.FormClose(result = MsgBoxResult.Ok)
    End Sub

    ''' <summary>
    ''' 確認画面を表示する
    ''' </summary>
    ''' <param name="headerMsg">ヘッダー部のメッセージ</param>
    ''' <param name="msg">メッセージ1行目</param>
    ''' <param name="msg2">メッセージ2行目</param>
    ''' <param name="okBtnMsg">OKボタンの文言</param>
    ''' <param name="cancelBtnMsg">Cancelボタンの文言</param>
    ''' <returns>MsgBoxResult</returns>
    ''' <remarks></remarks>
    Public Shared Function Confirm(ByVal headerMsg As String, ByVal msg As String, ByVal msg2 As String, ByVal okBtnMsg As String, ByVal cancelBtnMsg As String) As MsgBoxResult
        Using frm As New HoyouBuhinFrm00Kakunin
            frm.lblHEAD.Text = headerMsg
            frm.lblKakunin.Text = msg
            frm.lblKakunin2.Text = msg2
            frm.btnHanei.Text = okBtnMsg
            frm.btnModosu.Text = cancelBtnMsg
            frm.ShowDialog()
            Return frm.result
        End Using
    End Function

    ''' <summary>
    ''' 確認画面（モーダレス）を表示する
    ''' </summary>
    ''' <param name="headerMsg">ヘッダー部のメッセージ</param>
    ''' <param name="msg">メッセージ1行目</param>
    ''' <param name="msg2">メッセージ2行目</param>
    ''' <param name="okBtnMsg">OKボタンの文言</param>
    ''' <param name="cancelBtnMsg">Cancelボタンの文言</param>
    ''' <param name="_aFormCloser">終了処理</param>
    ''' <remarks></remarks>
    Public Shared Sub ConfirmShow(ByVal headerMsg As String, ByVal msg As String, ByVal msg2 As String, ByVal okBtnMsg As String, ByVal cancelBtnMsg As String, ByVal _aFormCloser As IFormCloser, _
                                  Optional ByVal avOwner As System.Windows.Forms.IWin32Window = Nothing)

        Dim frm As New HoyouBuhinFrm00Kakunin
        frm.lblHEAD.Text = headerMsg
        frm.lblKakunin.Text = msg
        frm.lblKakunin2.Text = msg2
        frm.btnHanei.Text = okBtnMsg
        frm.btnModosu.Text = cancelBtnMsg
        frm._aFormCloser = _aFormCloser
        'frm.Show()
        ' ====== 基底クラスのダイアログを開く(このクラスのShowDialogは殺している)


        If avOwner Is Nothing Then
            frm.Show()
        Else
            frm.Show(avOwner)   '親画面を閉じるとこの画面も閉じる
        End If


    End Sub
End Class