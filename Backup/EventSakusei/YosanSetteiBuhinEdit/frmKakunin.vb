Imports ShisakuCommon.Ui

Namespace YosanSetteiBuhinEdit

    Public Class frmKakunin
        Private _result As MsgBoxResult
        Private _aFormCloser As IFormCloser

        Public ReadOnly Property Result() As MsgBoxResult
            Get
                Return _result
            End Get
        End Property

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

        Public Sub New(ByVal headerMsg As String, ByVal msg As String, ByVal msg2 As String, ByVal okBtnMsg As String, ByVal cancelBtnMsg As String)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)

            Me.lblHEAD.Text = headerMsg
            Me.lblKakunin.Text = msg
            Me.lblKakunin2.Text = msg2
            Me.btnHanei.Text = okBtnMsg
            Me.btnModosu.Text = cancelBtnMsg

        End Sub

        Private Sub btnHanei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHanei.Click
            _result = MsgBoxResult.Ok
            Me.Close()
        End Sub

        Private Sub btnModosu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModosu.Click
            Me.Close()
        End Sub

        Private Sub frm00Kakunin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
            If _result <> MsgBoxResult.Ok Then
                _result = MsgBoxResult.Cancel
            End If
            If _aFormCloser Is Nothing Then
                Return
            End If
            _aFormCloser.FormClose(_result = MsgBoxResult.Ok)
        End Sub

    End Class

End Namespace
