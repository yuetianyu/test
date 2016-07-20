Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon
Imports ShisakuCommon.Ui

Namespace YosanSetteiBuhinEdit

    ''' <summary>
    ''' 部品費端数処理
    ''' </summary>
    ''' <remarks></remarks>
    Public Class frmPartsPriceFractionDialog

        Private _Pros As String
        Private _Unit As String
        Private _Flag As Integer

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal sheet As FarPoint.Win.Spread.SheetView)

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            FormUtil.BindLabelValuesToComboBox(Me.ComboBox1, GetLabelValues_Process(), True)
            FormUtil.BindLabelValuesToComboBox(Me.ComboBox2, GetLabelValues_Unit(), True)

        End Sub


        Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
            If StringUtil.IsEmpty(ComboBox1.SelectedValue) Then
                MsgBox("処理方法を選択してください。", MsgBoxStyle.OkOnly, "エラー")
            End If
            If StringUtil.IsEmpty(ComboBox2.SelectedValue) Then
                MsgBox("処理単位を選択してください。", MsgBoxStyle.OkOnly, "エラー")
            End If

            _Pros = ComboBox1.SelectedValue
            _Unit = ComboBox2.SelectedValue

            Me.DialogResult = Windows.Forms.DialogResult.OK

            Me.Close()
        End Sub

        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.Close()
        End Sub


#Region "公開プロパティ"

        ''' <summary>
        ''' 処理方法
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Pros() As String
            Get
                Return _Pros
            End Get
        End Property

        ''' <summary>
        ''' 処理単位
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Unit() As String
            Get
                Return _Unit
            End Get
        End Property

        ''' <summary>
        ''' フラグ
        ''' </summary>
        ''' <value></value>
        ''' <returns>0:そのまま、1:0になるものは処理対象外、2:キャンセル</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Flag() As String
            Get
                Return _Flag
            End Get
        End Property

#End Region



#Region "処理方法のコンボボックス設定"

        Private Function GetLabelValues_Process() As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)

            Dim vo1 As New LabelValueVo
            vo1.Label = "切り上げ"
            vo1.Value = "0"

            Dim vo2 As New LabelValueVo
            vo2.Label = "切り捨て"
            vo2.Value = "1"

            Dim vo3 As New LabelValueVo
            vo3.Label = "四捨五入"
            vo3.Value = "2"

            results.Add(vo1)
            results.Add(vo2)
            results.Add(vo3)

            'results.Sort(New LabelValueComparer)
            Return results
        End Function

#End Region

#Region "処理単位のコンボボックス設定"

        Private Function GetLabelValues_Unit() As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)

            Dim vo1 As New LabelValueVo
            vo1.Label = "一"
            vo1.Value = "1"

            Dim vo2 As New LabelValueVo
            vo2.Label = "十"
            vo2.Value = "10"

            Dim vo3 As New LabelValueVo
            vo3.Label = "百"
            vo3.Value = "100"

            Dim vo4 As New LabelValueVo
            vo4.Label = "千"
            vo4.Value = "1000"

            Dim vo5 As New LabelValueVo
            vo5.Label = "万"
            vo5.Value = "10000"

            Dim vo6 As New LabelValueVo
            vo6.Label = "十万"
            vo6.Value = "100000"

            results.Add(vo1)
            results.Add(vo2)
            results.Add(vo3)
            results.Add(vo4)
            results.Add(vo5)
            results.Add(vo6)

            'results.Sort(New LabelValueComparer)
            Return results
        End Function

#End Region



    End Class
End Namespace