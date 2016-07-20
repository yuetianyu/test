Imports EBom.Common
Imports ShisakuCommon
Imports ShisakuCommon.Ui

Namespace YosanshoEdit

    Public Class FrmAddKanazai

        Private _KanazaiList As List(Of String)

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

        Public ReadOnly Property kanazaiName() As String
            Get
                Return Me.txtKanazaiName.Text
            End Get
        End Property
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal kanazais As List(Of String))
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            'ヘッダー部分の初期設定
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SettingDefaultProperty(Me.txtKanazaiName)

            _KanazaiList = kanazais
        End Sub
#End Region

        ''' <summary>
        ''' 登録ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
            If StringUtil.IsEmpty(Me.txtKanazaiName.Text) Then
                ComFunc.ShowErrMsgBox("金材項目名を入力してください。")
                Exit Sub
            End If

            For Each kanazaiName As String In _KanazaiList
                If String.Equals(Me.txtKanazaiName.Text, kanazaiName) Then
                    ComFunc.ShowErrMsgBox("入力した金材項目名は既に追加されています。ご確認下さい。")
                    Exit Sub
                End If
            Next

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
