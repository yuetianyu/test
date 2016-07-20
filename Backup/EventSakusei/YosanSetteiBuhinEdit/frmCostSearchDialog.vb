Namespace YosanSetteiBuhinEdit

    Public Class frmCostSearchDialog

        Private _Flag As String

        Public Sub New()

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
            _Flag = ""
        End Sub

        Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If rbNewestHattchu.Checked Then
                _Flag = "0"
            End If
            If rbNewestKenshu.Checked Then
                _Flag = "1"
            End If
            If rbCost.Checked Then
                _Flag = "2"
            End If

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        Public ReadOnly Property Flag() As String
            Get
                Return _Flag
            End Get
        End Property


        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.Close()
        End Sub
    End Class

End Namespace

