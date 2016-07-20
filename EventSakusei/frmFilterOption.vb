Imports ShisakuCommon.Ui

Public Class frmFilterOption

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)

    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click


        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

End Class