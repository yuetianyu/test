Imports EBom.Common

Imports ShisakuCommon
Imports ShisakuCommon.Ui




Public Class frmGousyabetsuShiyousyo

#Region "メンバ変数"

    Private mSubject As GousyabetsuShiyousyoSubject


#End Region

    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        Initialize()

    End Sub

    Private Sub Initialize()
        '↓↓2014/10/09 酒井 ADD BEGIN
        System.IO.Directory.CreateDirectory(ExcelGousyaTemplateDir)
        System.IO.Directory.CreateDirectory(ExcelGousyaOutPutDir)
        '↑↑2014/10/09 酒井 ADD END
        ShisakuFormUtil.Initialize(Me)
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)

        mSubject = New GousyabetsuShiyousyoSubject()
        'イベントコードコンボボックスのアイテム設定.
        mSubject.setEventCodeToComboBox(cmbEvent)
        'グループ名コンボボックスのアイテム設定.
        mSubject.setGroupItemtoComboBox(cmbGroupNo)
        '号車名チェックボックスリストのアイテムを設定.
        mSubject.setGousyatoComboBox(clistGousya)

        ''↓↓2014/08/04 Ⅰ.8.号車別仕様書 作成機能_d) (TES)張 ADD BEGIN
        'テンプレートコンボボックスのアイテムを設定
        mSubject.setTemplateToComboBox(cmbTemplate)
        ''↑↑2014/08/04 Ⅰ.8.号車別仕様書 作成機能_d) (TES)張 ADD END

    End Sub

    ''' <summary>
    ''' フォームロード.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmGousyabetsuShiyousyon_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ShisakuFormUtil.setTitleVersion(Me)
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)
            LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_GOUSYABETSU

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
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
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
    ''' Excel出力ボタン押下.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExcelOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelOutput.Click
        'チェックボックスリストのチェックを取得(表示位置,号車名)

        Dim iSelectGousya As New List(Of String)
        Dim iMessage As String = ""
        If clistGousya.Enabled Then
            For Each lItem In clistGousya.CheckedItems
                'チェック済み号車の取得.
                iSelectGousya.Add(lItem.ToString)
            Next
            iMessage = "号車が選択されていません。"

        Else
            'コントロールが非表示の場合はすべて選択状態とする.
            For Each lItem In clistGousya.Items
                iSelectGousya.Add(lItem.ToString)
            Next
            iMessage = "号車情報が存在しません。"

        End If
        '選択号車チェック.
        If iSelectGousya.Count = 0 Then
            MessageBox.Show(iMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        '↓↓2014/10/09 酒井 ADD BEGIN
        Application.DoEvents()
        Cursor.Current = Cursors.WaitCursor
        '↑↑2014/10/09 酒井 ADD END

        '↓↓2014/10/01 酒井 ADD BEGIN
        'mSubject.ExcelOutput(mSubject.EventCode, mSubject.GroupNo, iSelectGousya, cmbTemplate.Text)
        mSubject.ExcelOutput(mSubject.EventCode, mSubject.KaiteiNo, mSubject.GroupNo, iSelectGousya, cmbTemplate.Text)
        '↑↑2014/10/01 酒井 ADD END

        '↓↓2014/10/09 酒井 ADD BEGIN
        Cursor.Current = Cursors.Default
        '↑↑2014/10/09 酒井 ADD END

        '20141127 Tsunoda Add Start (要望対応:№113:森（淳）様より、「号車別仕様書出力後、"出力しました。"のﾒｯｾｰｼﾞをお願いします」）
        MsgBox("出力が完了しました", MsgBoxStyle.Information)
        '20141127 Tsunoda Add End

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
        '↓↓2014/10/01 酒井 ADD BEGIN
        mSubject.KaiteiNo = cmbEvent.Text.Split(":")(1)
        '↑↑2014/10/01 酒井 ADD END

        'コンボ、チェック付コンボの初期化.
        cmbGroupNo.Items.Clear()
        clistGousya.Items.Clear()

        mSubject.setGroupItemtoComboBox(cmbGroupNo)

        If cmbGroupNo.Items.Count = 0 Then
            mSubject.GroupNo = cmbGroupNo.Text
            mSubject.setGousyatoComboBox(clistGousya)
        End If

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