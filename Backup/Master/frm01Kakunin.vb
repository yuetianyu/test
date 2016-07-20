Imports ShisakuCommon.Ui

Public Class frm01Kakunin
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ShisakuFormUtil.Initialize(Me)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'パラメータクリア
        frm51Para = ""
        frm5Para = ""
        frm6Para = ""
        '
        frm51ParaModori = ""
        frm5ParaModori = ""
        frm6ParaModori = ""
        Me.Close()
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Me.Close()

        Select Case frm51Para
            Case "btnCall"
                frm51ParaModori = "1"
            Case "btndel"
                frm51ParaModori = "2"
            Case "btnBACK"
                frm51ParaModori = "B"
            Case "btnEND"
                frm51ParaModori = "E"
            Case Else
                frm51ParaModori = ""
        End Select

        Select Case frm5Para
            Case "btnCall"
                frm5ParaModori = "1"
            Case "btndel"
                frm5ParaModori = "2"
            Case "btnBACK"
                frm5ParaModori = "B"
            Case "btnEND"
                frm5ParaModori = "E"
            Case Else
                frm5ParaModori = ""
        End Select

        Select Case frm6Para
            Case "btnNEW"
                frm6ParaModori = "1"
            Case "btnCall"
                frm6ParaModori = "2"
            Case "btnDel"
                frm6ParaModori = "3"
            Case "btnADD"
                frm6ParaModori = "4"
            Case "btnBACK"
                frm6ParaModori = "B"
            Case "btnEND"
                frm6ParaModori = "E"
            Case Else
                frm6ParaModori = ""
        End Select

    End Sub

    Private Sub frm01Kakunin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class