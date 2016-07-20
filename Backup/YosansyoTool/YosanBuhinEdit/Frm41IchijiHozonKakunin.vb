Imports ShisakuCommon.Ui

Namespace YosanBuhinEdit
    Public Class Frm41IchijiHozonKakunin

        ' OKボタン押下か？
        Private _resultOk As Boolean
        ' 入力されたメモ
        Private _result As String

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)

        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CANCEL.Click
            Me.Close()
        End Sub

        Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
            _resultOk = True
            _result = Me.txtMemo.Text
            Me.Close()
        End Sub

        Public Sub ConfirmView(ByVal message As String, ByVal memo As String)
            Me._resultOk = False
            Me.lblKakunin.Text = message
            Me.OK.Left = (Me.Width - Me.OK.Width) / 2
            Me.CANCEL.Visible = False
            Me.txtMemo.ReadOnly = True
            Me.txtMemo.Text = memo
            Me.txtMemo.TabStop = False
            Me.ShowDialog()
        End Sub

        Public Sub ConfirmOkCancel(ByVal message As String)
            Me._resultOk = False
            Me.lblKakunin.Text = message
            Me.OK.Left = 34
            Me.CANCEL.Visible = True
            Me.txtMemo.ReadOnly = False
            Me.txtMemo.Text = ""
            Me.txtMemo.TabStop = True
            Me.ShowDialog()
        End Sub

        ''' <summary>OKボタン押下か？</summary>
        ''' <returns>OKボタン押下か？</returns>
        Public ReadOnly Property ResultOk() As Boolean
            Get
                Return _resultOk
            End Get
        End Property

        ''' <summary>入力されたメモ</summary>
        ''' <returns>入力されたメモ</returns>
        Public ReadOnly Property Result() As String
            Get
                Return _result
            End Get
        End Property
    End Class
End Namespace
