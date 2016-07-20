Imports ShisakuCommon

Namespace YosanSetteiBuhinEdit
    Public Class frmStandardSettingNameDialog

        Private _StandardSettingName As String

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

        End Sub

        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.Close()
        End Sub

        Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
            If StringUtil.IsEmpty(TextBox1.Text) Then
                MsgBox("標準設定名を入力してください。", MsgBoxStyle.OkOnly, "エラー")
                Exit Sub
            End If

            StandardSettingName = TextBox1.Text
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End Sub

        Public Property StandardSettingName() As String
            Get
                Return _StandardSettingName
            End Get
            Set(ByVal value As String)
                _StandardSettingName = value
            End Set
        End Property




    End Class
End Namespace