Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Ui

Namespace YosanshoEdit

    Public Class FrmSelectPattern

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
        ''' <summary>
        ''' パターン名を返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property patternName() As String
            Get
                Return Me.listPatternName.SelectedValue
            End Get
        End Property
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal patterNameList As List(Of String))

            InitializeComponent()

            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)

            BindLabelValuesToListBox(patterNameList)
        End Sub
#End Region

#Region "パターン名のコンボボックスを作成"

        ''' <summary>
        ''' パターン名を作成
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub BindLabelValuesToListBox(ByVal patterNameList As List(Of String))
            Try
                'パターン名に値を追加
                Dim patternNameList As List(Of String) = patterNameList
                Dim labelValues As New List(Of LabelValueVo)
                For idx As Integer = 0 To patternNameList.Count - 1
                    Dim vo As New LabelValueVo
                    vo.Label = patternNameList(idx)
                    vo.Value = vo.Label
                    labelValues.Add(vo)
                Next
                Me.listPatternName.ValueMember = "Value"
                Me.listPatternName.DisplayMember = "Label"
                Me.listPatternName.DataSource = labelValues
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

        ''' <summary>
        ''' パターン名ダブルクリックの処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub listPatternName_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles listPatternName.MouseDoubleClick
            _ResultOk = True
            Me.Close()
        End Sub

#Region "戻るボタン"
        ''' <summary>
        ''' 戻るボタンボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            _ResultOk = False
            Me.Close()
        End Sub
#End Region

    End Class

End Namespace