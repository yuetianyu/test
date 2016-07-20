Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEdit.Sort


''' <summary>
''' ソート機能画面
''' </summary>
''' <remarks></remarks>
Public Class frm41Sort : Implements Observer

    Private ReadOnly aInputWatcher As InputWatcher
    Private sortSubject As SortSubject
    Private IsSuspendTehaiValueChanged As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        ShisakuFormUtil.Initialize(Me)

        sortSubject = New SortSubject()

        sortSubject.AddObserver(Me)

        aInputWatcher = New InputWatcher

        Initialize()

        InitializeWatcher()

        sortSubject.NotifyObservers()

        aInputWatcher.Clear()
    End Sub

    ''' <summary>
    ''' 初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Initialize()

    End Sub

    ''' <summary>
    ''' コンボボックスの監視
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeWatcher()
        aInputWatcher.Add(ComboBox1)
        'aInputWatcher.Add(ComboBox2)
        'aInputWatcher.Add(ComboBox3)
    End Sub

    ''' <summary>
    ''' 表示値を更新する
    ''' </summary>
    ''' <param name="observable">呼び出し元のObservable</param>
    ''' <param name="arg">引数</param>
    ''' <remarks></remarks>
    Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update

        UpdateHeader(observable, arg)

    End Sub

    ''' <summary>
    ''' ヘッダー部の表示値を更新する
    ''' </summary>
    ''' <param name="observable">呼び出し元Observable</param>
    ''' <param name="arg">引数</param>
    ''' <remarks></remarks>
    Private Sub UpdateHeader(ByVal observable As Observable, ByVal arg As Object)
        IsSuspendTehaiValueChanged = True
        Try
            FormUtil.BindLabelValuesToComboBox(ComboBox1, sortSubject.ColumnName1LabelValues, False)
            'FormUtil.BindLabelValuesToComboBox(ComboBox2, sortSubject.ColumnName2LabelValues, False)
            'FormUtil.BindLabelValuesToComboBox(ComboBox3, sortSubject.ColumnName3LabelValues, False)

        Finally
            IsSuspendTehaiValueChanged = False
        End Try

    End Sub

#Region "ラジオボタンの処理"

    ''' <summary>
    ''' 最優先用ラジオボタン１
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case RadioButton1.Checked
            Case True
                RadioButton1.Checked = True
                RadioButton2.Checked = False
            Case False
                RadioButton1.Checked = False
                RadioButton2.Checked = True
        End Select
    End Sub

    ''' <summary>
    ''' 最優先用ラジオボタン2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case RadioButton2.Checked
            Case True
                RadioButton2.Checked = True
                RadioButton1.Checked = False
            Case False
                RadioButton2.Checked = False
                RadioButton1.Checked = True
        End Select
    End Sub

    ''' <summary>
    ''' ２番目優先用ラジオボタン1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Select Case RadioButton3.Checked
        '    Case True
        '        RadioButton3.Checked = True
        '        RadioButton4.Checked = False
        '    Case False
        '        RadioButton3.Checked = False
        '        RadioButton4.Checked = True
        'End Select
    End Sub

    ''' <summary>
    ''' ２番目優先用ラジオボタン2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Select Case RadioButton3.Checked
        '    Case True
        '        RadioButton4.Checked = True
        '        RadioButton3.Checked = False
        '    Case False
        '        RadioButton4.Checked = False
        '        RadioButton3.Checked = True
        'End Select
    End Sub

    ''' <summary>
    ''' ３番目優先用ラジオボタン1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadioButton5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Select Case RadioButton5.Checked
        '    Case True
        '        RadioButton5.Checked = True
        '        RadioButton6.Checked = False
        '    Case False
        '        RadioButton5.Checked = False
        '        RadioButton6.Checked = True
        'End Select
    End Sub

    ''' <summary>
    ''' ３番目優先用ラジオボタン2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadioButton6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Select Case RadioButton6.Checked
        '    Case True
        '        RadioButton6.Checked = True
        '        RadioButton5.Checked = False
        '    Case False
        '        RadioButton6.Checked = False
        '        RadioButton5.Checked = True
        'End Select
    End Sub

#End Region

#Region "ボタンイベント"

    ''' <summary>
    ''' キャンセルボタンのイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub


    ''' <summary>
    ''' OKボタンのイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        '各コンボボックスの中身とラジオボタンの状態でソートを行う'
        Me.Close()
        '最優先条件が空の場合何もしないで閉じる'
        If StringUtil.IsEmpty(ComboBox1.Text) Then
            Me.Close()
        End If
    End Sub

#End Region

End Class