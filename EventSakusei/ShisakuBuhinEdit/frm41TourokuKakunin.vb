Imports ShisakuCommon
Imports ShisakuCommon.Ui

Public Class frm41TourokuKakunin

    Private Sub BtnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub frm41TourokuKakunin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'バージョンをセット
        ShisakuFormUtil.setTitleVersion(Me)
    End Sub
End Class