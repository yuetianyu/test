Imports ShisakuCommon.Ui

Namespace YosanshoEdit

    Public Class FrmInsertMonthKakunin

        Private result As MsgBoxResult

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

        Private Sub frm00Kakunin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
            If result <> MsgBoxResult.Ok Then
                result = MsgBoxResult.Cancel
            End If
        End Sub

        ''' <summary>
        ''' 確認画面を表示する
        ''' </summary>
        ''' <param name="msg">メッセージ1行目</param>
        ''' <returns>MsgBoxResult</returns>
        ''' <remarks></remarks>
        Public Shared Function Confirm(ByVal msg As String) As MsgBoxResult
            Using frm As New FrmInsertMonthKakunin
                frm.lblKakunin2.Text = msg
                frm.ShowDialog()
                Return frm.result
            End Using
        End Function

    End Class

End Namespace