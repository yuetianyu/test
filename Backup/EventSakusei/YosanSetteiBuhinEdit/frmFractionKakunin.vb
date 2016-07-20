Imports ShisakuCommon.Ui

Namespace YosanSetteiBuhinEdit

    Public Class frmFractionKakunin
        Private _result As Integer

        Public ReadOnly Property Result() As Integer
            Get
                Return _result
            End Get
        End Property


        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
        End Sub

        Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
            _result = 0
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            _result = 2
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub


        Private Sub btnIgnoring_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIgnoring.Click
            _result = 1
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub
    End Class

End Namespace
