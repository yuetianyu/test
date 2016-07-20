Imports EBom.Common
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanshoEdit

    Public Class FrmAddBuhin
        Private _BuhinhyoVos As List(Of TYosanBuhinSelectVo)

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

        Public ReadOnly Property BuhinhyoName() As String
            Get
                Return Me.cmbBuhinhyoName.Text
            End Get
        End Property
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal buhinhyoVos As List(Of TYosanBuhinSelectVo))
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            'ヘッダー部分の初期設定
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SettingDefaultProperty(Me.cmbBuhinhyoName)

            _BuhinhyoVos = buhinhyoVos

            '
            cmbBuhinhyoName.MaxLength = 40
            cmbBuhinhyoName.ImeMode = Windows.Forms.ImeMode.Off
            '作成
            BindLabelValuesToListBox()

        End Sub
#End Region

        ''' <summary>
        ''' 部品表名を作成
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub BindLabelValuesToListBox()
            Try
                Me.cmbBuhinhyoName.DataSource = Nothing
                'パターン名に値を追加
                Dim labelValues As New List(Of LabelValueVo)
                For Each item As TYosanBuhinSelectVo In _BuhinhyoVos
                    Dim vo As New LabelValueVo
                    vo.Label = item.BuhinhyoName
                    vo.Value = vo.Label
                    labelValues.Add(vo)
                Next
                Me.cmbBuhinhyoName.ValueMember = "Value"
                Me.cmbBuhinhyoName.DisplayMember = "Label"
                Me.cmbBuhinhyoName.DataSource = labelValues
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 登録ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
            If StringUtil.IsEmpty(Me.cmbBuhinhyoName.Text) Then
                ComFunc.ShowErrMsgBox("部品表名を選択または入力してください。")
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