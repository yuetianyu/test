Imports EBom.Common
Imports ShisakuCommon
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Ui

Namespace ShisakuBuhinMenu
    Public Class Frm8DispShisakuBuhinMenuDATE
        Private _result As MsgBoxResult
        Private now As DateTime
        Private aValidator As Validator
        Private errorController As New ErrorController()

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            Dim aShisakuDate As New ShisakuDate
            now = DateUtil.TruncTime(aShisakuDate.CurrentDateTime)
            dtpKaiteiUketukebi.Value = now

            aValidator = New Validator
            aValidator.Add(dtpKaiteiUketukebi).GreaterEqual(now, "〆切日は過去の日付を入力できません。")
        End Sub

        Public ReadOnly Property Result() As MsgBoxResult
            Get
                Return _result
            End Get
        End Property

        Public ReadOnly Property Shimekiribi() As DateTime
            Get
                Return dtpKaiteiUketukebi.Value
            End Get
        End Property

        Private Sub btnCANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCANCEL.Click
            _result = MsgBoxResult.Cancel
            '自分を閉じる。
            Me.Close()
        End Sub

        Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
            Try
                errorController.ClearBackColor()
                aValidator.AssertValidate()
            Catch ex As IllegalInputException
                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)
                Return
            End Try
            _result = MsgBoxResult.Ok
            '自分を閉じる。
            Me.Close()
        End Sub

    End Class
End Namespace
