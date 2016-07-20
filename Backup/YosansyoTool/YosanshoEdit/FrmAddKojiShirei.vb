Imports EBom.Common
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanshoEdit

    Public Class FrmAddKojiShirei

        Private _Kojishireis As List(Of TYosanSeisakuDaisuVo)

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

        Public ReadOnly Property UnitKbn() As String
            Get
                Return Me.cmbKuKe.Text
            End Get
        End Property

        Public ReadOnly Property KojishireiNo() As String
            Get
                Return Me.txtKojiShirei.Text
            End Get
        End Property
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal kojishireis As List(Of TYosanSeisakuDaisuVo))
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            'ヘッダー部分の初期設定
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SettingDefaultProperty(Me.txtKojiShirei)

            _Kojishireis = kojishireis
        End Sub
#End Region

        ''' <summary>
        ''' 登録ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
            If StringUtil.IsEmpty(Me.cmbKuKe.Text) Then
                ComFunc.ShowErrMsgBox("ユニット区分を選択してください。")
                Exit Sub
            End If

            If StringUtil.IsEmpty(Me.txtKojiShirei.Text) Then
                ComFunc.ShowErrMsgBox("工事指令№を正しく入力してください。")
                Exit Sub
            End If

            For Each vo As TYosanSeisakuDaisuVo In _Kojishireis
                If String.Equals(Me.cmbKuKe.Text, vo.UnitKbn) AndAlso String.Equals(Me.txtKojiShirei.Text, vo.KoujiShireiNo) Then
                    ComFunc.ShowErrMsgBox("入力したユニット区分、工事指令№は既に追加されています。ご確認下さい。")
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