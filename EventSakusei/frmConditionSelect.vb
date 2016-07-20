Imports EBom.Common

Imports ShisakuCommon
Imports ShisakuCommon.Ui




Public Class frmConditionSelect

#Region "メンバ変数"

    Private mSubject As ConditionSelectSubject


#End Region

    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        Initialize()

    End Sub

    Private Sub Initialize()

        ShisakuFormUtil.Initialize(Me)
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)

        mSubject = New ConditionSelectSubject()
        'イベントコードコンボボックスのアイテム設定.
        mSubject.setEventCodeToComboBox(cmbEvent)
        'グループ名コンボボックスのアイテム設定.
        mSubject.setGroupItemtoComboBox(cmbGroupNo)
        '号車名チェックボックスリストのアイテムを設定.
        mSubject.setGousyatoComboBox(clistGousya)

    End Sub

    ''' <summary>
    ''' フォームロード.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSelectCondition_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)

            'グループ№、号車チェックボックス判定.
            If radioGroup.Checked Then
                '号車チェックボックスリスト無効.
                clistGousya.Enabled = False
            Else
                '号車チェックボックスリスト有効.
                clistGousya.Enabled = True
            End If

        Catch ex As Exception
            Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
            Throw
        End Try
    End Sub

#Region "フォームボタン処理."

    ''' <summary>
    ''' 戻るボタン.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 検索ボタン押下.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click


        'チェックボックスリストのチェックを取得(表示位置,号車名)
        Dim iSelectGousya As New Dictionary(Of Integer, String)
        Dim iMessage As String = ""
        If clistGousya.Enabled Then
            For Each lItem In clistGousya.CheckedItems
                'チェック済み号車の取得.
                iSelectGousya.Add(clistGousya.Items.IndexOf(lItem), lItem.ToString)
            Next
            iMessage = "号車が選択されていません。"

        Else
            'コントロールが非表示の場合はすべて選択状態とする.
            For Each lItem In clistGousya.Items
                'チェック済み号車の取得.
                iSelectGousya.Add(clistGousya.Items.IndexOf(lItem), lItem.ToString)
            Next
            iMessage = "号車情報が存在しません。"

        End If
        '選択号車チェック.
        If iSelectGousya.Count = 0 Then
            MessageBox.Show(iMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        '抽出したデータを表示する.
        Using frm As New frmPreViewParentForTehai(mSubject.GUID, mSubject.EventCode, mSubject.GroupNo, iSelectGousya)
            'Me.Visible = False
            Try
                'エラー終了していた場合は中断
                If frm.IsDisposed Then Exit Try
                frm.IsMdiContainer = True
                frm.Initialize()
                'フォームを開く.
                frm.ShowDialog(Me)
            Finally

                Me.Visible = True

            End Try

        End Using

    End Sub

#End Region

    ''' <summary>
    ''' イベントコード変更時.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbEvent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEvent.SelectedIndexChanged
        'イベントコードの変更.
        mSubject.EventCode = cmbEvent.Text.Split(":")(0)

        'コンボ、チェック付コンボの初期化.
        cmbGroupNo.Items.Clear()
        clistGousya.Items.Clear()

        mSubject.setGroupItemtoComboBox(cmbGroupNo)

        mSubject.GroupNo = cmbGroupNo.Text
        mSubject.setGousyatoComboBox(clistGousya)


        'mSubject.CreateTempTable()
    End Sub

#Region "ラジオボタン変更処理."
    ''' <summary>
    ''' ラジオボタン（号車）クリック.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub radioGousya_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioGousya.Click
        clistGousya.Enabled = True
    End Sub
    ''' <summary>
    ''' ラジオボタン（グループ）クリック.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub radioGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioGroup.Click
        clistGousya.Enabled = False
    End Sub

    Private Sub grpSelector_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grpSelector.Click
    End Sub
    ''' <summary>
    ''' グループ変更処理.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbGroupNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGroupNo.SelectedIndexChanged
        mSubject.GroupNo = cmbGroupNo.Text
        mSubject.setGousyatoComboBox(clistGousya)
    End Sub

#End Region


End Class