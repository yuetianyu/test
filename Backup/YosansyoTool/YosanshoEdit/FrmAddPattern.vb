Imports EBom.Common
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanshoEdit

    Public Class FrmAddPattern

        Private _PatternNameList As Dictionary(Of String, List(Of String))
        Private _PatternVos As List(Of TYosanBuhinEditPatternVo)

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

        Public ReadOnly Property UnitKbn() As String
            Get
                Return Me.cmbUnitKbn.Text
            End Get
        End Property

        Public ReadOnly Property BuhinhyoName() As String
            Get
                Return Me.cmbBuhinhyoName.Text
            End Get
        End Property

        Public ReadOnly Property PatternName() As String
            Get
                Return Me.cmbPatternName.Text
            End Get
        End Property
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal tukurikatas As Dictionary(Of String, List(Of String)), _
                       ByVal patternVos As List(Of TYosanBuhinEditPatternVo), _
                       ByVal buhinhyoVos As List(Of TYosanBuhinSelectVo))
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            'ヘッダー部分の初期設定
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SettingDefaultProperty(Me.cmbPatternName)

            _PatternNameList = tukurikatas
            _PatternVos = patternVos
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
        ''' 部品表名変更時
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbBuhinhyoName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBuhinhyoName.SelectedIndexChanged
            BindLabelValuesToListBox(Me.cmbBuhinhyoName.Text)
        End Sub

        ''' <summary>
        ''' パターン名を作成
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub BindLabelValuesToListBox(ByVal unitKbn As String)
            Try
                Me.cmbPatternName.DataSource = Nothing
                'パターン名に値を追加
                Dim labelValues As New List(Of LabelValueVo)
                For Each item As TYosanBuhinEditPatternVo In _PatternVos
                    Dim vo As New LabelValueVo
                    If String.Equals(unitKbn, item.BuhinhyoName) Then
                        vo.Label = item.PatternName
                        vo.Value = vo.Label
                        labelValues.Add(vo)
                    End If
                Next
                Me.cmbPatternName.ValueMember = "Value"
                Me.cmbPatternName.DisplayMember = "Label"
                Me.cmbPatternName.DataSource = labelValues
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
            If StringUtil.IsEmpty(Me.cmbUnitKbn.Text) Then
                ComFunc.ShowErrMsgBox("ユニット区分を選択してください。")
                Exit Sub
            End If

            If StringUtil.IsEmpty(Me.cmbBuhinhyoName.Text) Then
                ComFunc.ShowErrMsgBox("部品表名を選択してください。")
                Exit Sub
            End If

            If StringUtil.IsEmpty(Me.cmbPatternName.Text) Then
                ComFunc.ShowErrMsgBox("パターン名を選択または入力してください。")
                Exit Sub
            End If

            For Each key As String In _PatternNameList.Keys
                If String.Equals(Me.cmbBuhinhyoName.Text, key) Then
                    For Each patternName As String In _PatternNameList(key)
                        If String.Equals(Me.cmbPatternName.Text, patternName) Then
                            ComFunc.ShowErrMsgBox("入力した部品表名、パターン名は既に追加されています。ご確認下さい。")
                            Exit Sub
                        End If
                    Next
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