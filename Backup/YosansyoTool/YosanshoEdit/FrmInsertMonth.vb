Imports EBom.Common
Imports ShisakuCommon
Imports ShisakuCommon.Ui

Namespace YosanshoEdit

    Public Class FrmInsertMonth

#Region " プロパティ "
        Private _ResultOk As Boolean
        ''' <summary>
        ''' OKを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ResultOk() As Boolean
            Get
                Return _ResultOk
            End Get
        End Property

        Public ReadOnly Property Year() As String
            Get
                Return Me.txtYear.Text
            End Get
        End Property

        Public ReadOnly Property Ks() As String
            Get
                Return Me.cmbKs.Text
            End Get
        End Property
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            'ヘッダー部分の初期設定
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SettingDefaultProperty(Me.txtYear)

        End Sub
#End Region

        ''' <summary>
        ''' 登録ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
            If StringUtil.IsEmpty(Me.txtYear.Text) Then
                ComFunc.ShowErrMsgBox("年を入力してください。")
                Exit Sub
            End If
            If Not IsDate(Me.txtYear.Text & "/01/01") Then
                ComFunc.ShowErrMsgBox("年を入力してください。")
                Exit Sub
            End If
            If StringUtil.IsEmpty(Me.cmbKs.Text) Then
                ComFunc.ShowErrMsgBox("期を入力してください。")
                Exit Sub
            End If

            _ResultOk = True
            Me.Close()
        End Sub

        ''' <summary>
        ''' 戻るボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            _ResultOk = False
            Me.Close()
        End Sub
    End Class

End Namespace