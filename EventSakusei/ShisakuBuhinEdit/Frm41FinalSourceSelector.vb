'↓↓2014/07/29 3.設計編集 ベース車改修専用化_ao) TES)張 ADD
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util.LabelValue

Public Class Frm41FinalSourceSelector

    Private _result As Integer

    Public Sub New(ByVal lINSTL As List(Of String))

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ShisakuFormUtil.Initialize(Me)

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        'cmbベースにするFINAL品番にlINSTLを設定する。
        Dim labelValues As List(Of LabelValueVo) = New List(Of LabelValueVo)

        Dim val As Integer = 0
        For Each label As String In lINSTL
            Dim labelValue As LabelValueVo = New LabelValueVo(label, val)
            labelValues.Add(labelValue)
            val += 1
        Next

        FormUtil.BindLabelValuesToComboBox(cmbFinalSource, labelValues, True)

    End Sub

    Public ReadOnly Property Result() As Integer
        Get
            Return _result
        End Get
    End Property

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Me._result = 1
        Me.Close()
    End Sub

    Private Sub btnDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        If String.IsNullOrEmpty(cmbFinalSource.SelectedValue) Then
            MsgBox("ベースにするFINAL品番を選択して下さい。")
            Exit Sub
        End If

        Me._result = 2
        Me.Close()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me._result = 0
        Me.Close()
    End Sub

End Class
