Imports ShisakuCommon.Ui

Public Class frm02Kakunin

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frm02Kakunin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ShisakuFormUtil.setTitleVersion(Me)
    End Sub
End Class