Imports EventSakusei
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports YosansyoTool.YosanshoEdit
Imports YosansyoTool.YosanEventNew
Imports YosansyoTool.YosanEventList.Logic
Imports EBom.Common
Imports YosansyoTool.YosanEventListExcel

Namespace YosanEventList

    Public Class FrmYosanEventList

#Region " メンバー変数 "
        ''' <summary>ロジック</summary>
        Private m_EventList As DispYosanEventList = Nothing
        ''' <summary> 完了通常フラグ（内部変数）</summary>
        Private m_Complete_Flg As Integer
        '''<summary>選択した行の予算イベントコード</summary>
        Private m_SelectEventCode As String
        '''<summary>コンボボックス制御</summary>
        Private m_DataUpdate As Boolean = True
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            '画面制御ロジック
            m_EventList = New DispYosanEventList(Me, LoginInfo.Now)

            '初期化メイン
            Initialize()
        End Sub
#End Region

#Region "画面初期化"
        ''' <summary>
        ''' 画面初期化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Initialize()
            Try

                Cursor.Current = Cursors.WaitCursor

                'ヘッダー部を初期化する
                m_EventList.InitializeHeader()

                '編集ボタン	使用不可。
                m_EventList.SetEditBtn(False)
                '閲覧ボタン	使用不可。
                m_EventList.SetCallBtn(False)
                '削除ボタン	使用不可。
                m_EventList.SetDelBtn(False)
                '完了ボタン	使用不可。
                m_EventList.SetCompleteBtn(False)

                '完了ラベル	非表示。
                m_EventList.SetCompleteMsgLabel(False)

                '開発符号リスト作成
                m_EventList.SetKaihatsuFugoCombo()

                '期間リスト作成
                m_EventList.SetKikanCombo()

                '0を設定する。（通常イベントを表すコード）
                m_Complete_Flg = 0

                'スプレッドを初期化する
                m_EventList.InitializeSpread(m_Complete_Flg)

                'メッセージラベル
                LblMessage.Text = "ダブルクリックで選択してください。"

            Catch ex As Exception
                Dim msg As String
                msg = String.Format("画面の初期化中に問題が発生しました(ERR={0})", ex.Message)
                ComFunc.ShowErrMsgBox(msg)
                Me.Close()
            Finally
                Cursor.Current = Cursors.Default
            End Try
        End Sub
#End Region

#Region "ボタン"

#Region "アプリケーション終了ボタン"
        ''' <summary>
        ''' アプリケーション終了ボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
            Application.Exit()
            System.Environment.Exit(0)
        End Sub
#End Region

#Region "戻るボタン"
        ''' <summary>
        ''' 戻るボタンボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Me.Close()
        End Sub
#End Region

#Region "Excel出力ボタン"
        ''' <summary>
        ''' Excel出力ボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelExport.Click
            Try
                Dim MessageLine1 As String = "Excelファイルに出力しますか？"
                Dim MessageLine2 As String = ""
                If frm01Kakunin.ConfirmOkCancel(MessageLine1, MessageLine2) <> MsgBoxResult.Ok Then
                    Return
                End If

                '画面を綺麗に、実行中のカーソルへ変更。
                Application.DoEvents()
                Cursor.Current = Cursors.WaitCursor

                'EXCEL出力処理
                m_EventList.ExcelBtnClick(m_Complete_Flg)

                '画面を綺麗に、実行中のカーソルを元に戻す。
                Cursor.Current = Cursors.Default
            Catch ex As Exception
                MsgBox(String.Format("Excel出力時にシステムエラーが発生しました:{0}", ex.Message))
            Finally
                Cursor.Current = Cursors.Default
            End Try
        End Sub
#End Region

#Region "財務報告実績取込ボタン"
        ''' <summary>
        ''' 財務報告実績取込ボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnPartsPriceImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPartsPriceImport.Click
            'EXCEL取込処理
            m_EventList.PartsPriceImport()

            Cursor.Current = Cursors.Default

        End Sub
#End Region

#Region "財務報告実績取込ボタン"
        ''' <summary>
        ''' 財務報告実績取込ボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnExcelImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelImport.Click
            'EXCEL取込処理
            m_EventList.Import()

            Cursor.Current = Cursors.Default

        End Sub
#End Region

#Region "編集ボタン"
        ''' <summary>
        ''' 編集ボタンの処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click

            '排他処理を行う
            If m_EventList.DoExclusiveEvent(m_SelectEventCode) = True Then
                '「予算書編集」画面へ遷移
                Dim frmEdit As New FrmYosanshoEdit(m_SelectEventCode, EDIT_MODE, LoginInfo.Now)
                frmEdit.ShowDialog()

                'イベント一覧画面の情報を再表示する。
                m_EventList.SetSpreadData(m_Complete_Flg)
            End If

        End Sub
#End Region

#Region "閲覧ボタン"
        ''' <summary>
        ''' 閲覧ボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click
            '「予算書編集」画面へ遷移
            Dim frmEdit As New FrmYosanshoEdit(m_SelectEventCode, VIEW_MODE, LoginInfo.Now)
            frmEdit.ShowDialog()

            'イベント一覧画面の情報を再表示する。
            m_EventList.SetSpreadData(m_Complete_Flg)
        End Sub
#End Region

#Region "新規作成ボタン"
        ''' <summary>
        ''' 新規作成ボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnNEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
            Dim f As New FrmYosanEventNew("")
            Me.Hide()
            f.ShowDialog()
            Me.Show()

            'イベント一覧画面の情報を再表示する。
            m_EventList.SetSpreadData(m_Complete_Flg, True)

            '編集ボタン	使用不可。
            m_EventList.SetEditBtn(False)
            '閲覧ボタン	使用不可。
            m_EventList.SetCallBtn(False)
            '削除ボタン	使用不可。
            m_EventList.SetDelBtn(False)
            '完了ボタン	使用不可。
            m_EventList.SetCompleteBtn(False)
            '完了ラベル	非表示。
            m_EventList.SetCompleteMsgLabel(False)

            m_SelectEventCode = String.Empty
        End Sub
#End Region

#Region "削除ボタン"
        ''' <summary>
        ''' 削除ボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click

            If frm01Kakunin.ConfirmOkCancel("削除を実行しますか？") <> MsgBoxResult.Ok Then
                Return
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            '削除処理
            If m_EventList.DeleteYosanEvent(m_SelectEventCode) = True Then
                'イベント一覧画面の情報を再表示する。
                m_EventList.SetSpreadData(m_Complete_Flg, True)

                '編集ボタン	使用不可。
                m_EventList.SetEditBtn(False)
                '閲覧ボタン	使用不可。
                m_EventList.SetCallBtn(False)
                '削除ボタン	使用不可。
                m_EventList.SetDelBtn(False)
                '完了ボタン	使用不可。
                m_EventList.SetCompleteBtn(False)
                '完了ラベル	非表示。
                m_EventList.SetCompleteMsgLabel(False)

                m_SelectEventCode = String.Empty
            End If

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub
#End Region

#Region "完了イベントボタン"
        ''' <summary>
        ''' 完了イベントボタンの処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnCompleEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompleteEvent.Click

            If m_Complete_Flg = 0 Then
                '完了通常フラグ（内部変数）に”1”を設定する
                m_Complete_Flg = 1

                'コンボボックスクリア
                Me.cmbKaihatsuFugo.Text = ""
                Me.cmbEvent.Text = ""

                'イベント一覧画面の情報を再表示する。
                m_EventList.SetSpreadData(m_Complete_Flg, True)

                '編集ボタン	使用不可。
                m_EventList.SetEditBtn(False)
                '閲覧ボタン	使用不可。
                m_EventList.SetCallBtn(False)
                '削除ボタン	使用不可。
                m_EventList.SetDelBtn(False)
                '完了ボタン	使用不可。
                m_EventList.SetCompleteBtn(False)
                '完了ラベル	表示。
                m_EventList.SetCompleteMsgLabel(True)

                Me.btnCompleteEvent.Text = "未完了イベント"
                Me.btnComplete.Text = "未完了"

                'イベント情報一覧SPREADの背景色をライトブルーに変更する。
                For index = 0 To spdParts_Sheet1.RowCount - 1
                    Me.spdParts_Sheet1.Rows(index).BackColor = Color.LightBlue
                Next
            Else
                '完了通常フラグ（内部変数）に”0”を設定する
                m_Complete_Flg = 0

                'コンボボックスクリア
                Me.cmbKaihatsuFugo.Text = ""
                Me.cmbEvent.Text = ""

                'イベント一覧画面の情報を再表示する。
                m_EventList.SetSpreadData(m_Complete_Flg, True)

                '編集ボタン	使用不可。
                m_EventList.SetEditBtn(False)
                '閲覧ボタン	使用不可。
                m_EventList.SetCallBtn(False)
                '削除ボタン	使用不可。
                m_EventList.SetDelBtn(False)
                '完了ボタン	使用不可。
                m_EventList.SetCompleteBtn(False)
                '完了ラベル	非表示。
                m_EventList.SetCompleteMsgLabel(False)

                Me.btnCompleteEvent.Text = "完了イベント"
                Me.btnComplete.Text = "完了"

                'イベント情報一覧SPREADの背景色をデフォルトに変更する。
                For index = 0 To spdParts_Sheet1.RowCount - 1
                    Me.spdParts_Sheet1.Rows(index).BackColor = Nothing
                Next
            End If

        End Sub
#End Region

#Region "完了ボタン"
        ''' <summary>
        ''' 完了ボタンの処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnComplete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComplete.Click

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            If m_Complete_Flg = 0 Then
                '確認メッセージを表示する
                If frm01Kakunin.ConfirmOkCancel("選択したイベントを完了にしてよろしいですか？") <> MsgBoxResult.Ok Then
                    Return
                End If
            Else
                '確認メッセージを表示する
                If frm01Kakunin.ConfirmOkCancel("選択したイベントを未完了にしてよろしいですか？") <> MsgBoxResult.Ok Then
                    Return
                End If
            End If

            '「予算書イベント情報」の「ステータス」を更新する。
            If m_EventList.UpdateYosanStatus(m_SelectEventCode, m_Complete_Flg) = True Then
                'イベント一覧画面の情報を再表示する。
                m_EventList.SetSpreadData(m_Complete_Flg, True)

                '編集ボタン	使用不可。
                m_EventList.SetEditBtn(False)
                '閲覧ボタン	使用不可。
                m_EventList.SetCallBtn(False)
                '削除ボタン	使用不可。
                m_EventList.SetDelBtn(False)
                '完了ボタン	使用不可。
                m_EventList.SetCompleteBtn(False)
                '完了ラベル	非表示。
                m_EventList.SetCompleteMsgLabel(False)

                m_SelectEventCode = String.Empty
            End If

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub
#End Region

#End Region

#Region "コンボボックス"

        ''' <summary>
        ''' 開発符号コンボボックスの値を変更後
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbKaihatsuFugo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbKaihatsuFugo.TextChanged
            If m_DataUpdate = False Then
                Return
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            '「試作車系／開発符号マスター」の試作イベントフェーズ名をイベントコンボボックスへ設定する
            m_EventList.SetEventCombo(Me.cmbKaihatsuFugo.Text)

            'イベント情報一覧SPREADから該当する情報を表示する
            m_EventList.SetSpreadData(m_Complete_Flg)

            Cursor.Current = Cursors.Default
        End Sub

        ''' <summary>
        ''' イベントコンボボックスの値を変更後
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbEvent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEvent.TextChanged
            If m_DataUpdate = False Then
                Return
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            'イベント情報一覧SPREADから該当する情報を表示する
            m_EventList.SetSpreadData(m_Complete_Flg)

            Cursor.Current = Cursors.Default
        End Sub

        ''' <summary>
        ''' 期間コンボボックスの値を変更後
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbKikan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbKikan.TextChanged
            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            'イベント情報一覧SPREADから該当する情報を表示する
            m_EventList.SetSpreadData(m_Complete_Flg)

            Cursor.Current = Cursors.Default
        End Sub

        ''' <summary>
        ''' コンボボックスに　delete　key　press
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Controls_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbKaihatsuFugo.KeyDown, cmbEvent.KeyDown, cmbKikan.KeyDown
            ShisakuFormUtil.DelKeyDown(sender, e)
        End Sub

        ''' <summary>
        ''' ボタン使用不可
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DisabledButtons()
            '「編集」ボタンを使用不可にする。
            m_EventList.SetEditBtn(False)
            '「閲覧」ボタンを使用不可にする。
            m_EventList.SetCallBtn(False)
            '「完了」ボタンを使用不可にする。
            m_EventList.SetCompleteBtn(False)
            '「削除」ボタンを使用不可にする。
            m_EventList.SetDelBtn(False)
        End Sub

#End Region

#Region "スプレッド"

        ''' <summary>
        ''' 行取得時開発符号、イベント自動取得処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub RowAvtive(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles spdParts.LeaveCell

            m_DataUpdate = False
            With spdParts_Sheet1
                '予算イベントコード
                m_SelectEventCode = .GetValue(e.NewRow, DispYosanEventList.GetTagIdx(spdParts_Sheet1, DispYosanEventList.TAG_YOSAN_EVENT_CODE))
            End With
            m_DataUpdate = True

            '「閲覧」ボタンを使用可能にする。
            m_EventList.SetCallBtn(True)
            '「完了」ボタンを使用可能にする。
            m_EventList.SetCompleteBtn(True)
            '完了通常フラグ（内部変数）が1の場合
            If m_Complete_Flg = 1 Then
                '「編集」ボタンを使用可能にする。
                m_EventList.SetEditBtn(False)
                '「削除」ボタンを使用可能にする。
                m_EventList.SetDelBtn(True)
            Else
                '「編集」ボタンを使用可能にする。
                m_EventList.SetEditBtn(True)
                '「削除」ボタンを使用不可能にする。
                m_EventList.SetDelBtn(False)
            End If

        End Sub

        ''' <summary>
        ''' イベント情報SPREADの特定セルをダブルクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick
            'ヘッダーの場合、処理を終了
            If e.ColumnHeader Then
                Exit Sub
            End If

            Dim activeColumn As Integer = e.Column
            Dim activeRow As Integer = e.Row
            Dim strResult As String

            With spdParts_Sheet1
                'クリックしたセルの値を取得

                'Dim strContent As String = .GetValue(activeRow, activeColumn)
                Dim strContent As String = .ActiveCell.Value.ToString

                Dim strTagName As String = ""

                Select Case .Columns(activeColumn).Tag
                    Case DispYosanEventList.TAG_YOSAN_EVENT_NAME
                        strTagName = "イベント名称"
                    Case DispYosanEventList.TAG_YOSAN_CODE
                        strTagName = "予算コード"
                    Case DispYosanEventList.TAG_YOSAN_MAIN_HENKO_GAIYO
                        strTagName = "主な変更概要"
                    Case DispYosanEventList.TAG_YOSAN_TSUKURIKATA_SEISAKUJYOKEN
                        strTagName = "造り方及び製作条件"
                    Case DispYosanEventList.TAG_YOSAN_SONOTA
                        strTagName = "その他"
                End Select


                If StringUtil.IsNotEmpty(strTagName) Then
                    '編集画面表示
                    Using frmEdit As New FrmYosanEventEdit(.Columns(activeColumn).Tag, strContent)
                        frmEdit.ShowDialog()

                        '戻るの場合、処理を終了
                        If frmEdit.ResultOk = False Then
                            Return
                        End If

                        strResult = frmEdit.ResultInput
                    End Using

                    If Not String.Equals(strResult, strContent) Then
                        '開発符号
                        Dim strKaihatsuFugo As String = .GetValue(activeRow, DispYosanEventList.GetTagIdx(spdParts_Sheet1, DispYosanEventList.TAG_YOSAN_KAIHATSU_FUGO))
                        'イベント
                        Dim strEvent As String = .GetValue(activeRow, DispYosanEventList.GetTagIdx(spdParts_Sheet1, DispYosanEventList.TAG_YOSAN_EVENT))
                        '「４．編集画面」の入力値とSPREADの値を比較して変更があれば確認画面を表示する
                        Dim result As Integer = frm00Kakunin.Confirm("確認", "下記イベントの" & strTagName & "を" & vbLf & "更新して宜しいですか？", _
                                                                     "開発符号：「" & strKaihatsuFugo & "」" & vbLf & "イベント　：「" & strEvent & "」", "OK", "CANCEL")
                        If result = MsgBoxResult.Cancel Then
                            Return
                        End If
                        '予算イベントコード
                        Dim strYosanEventCode As String = .GetValue(activeRow, DispYosanEventList.GetTagIdx(spdParts_Sheet1, DispYosanEventList.TAG_YOSAN_EVENT_CODE))
                        '「予算書イベント情報」を更新を行う
                        If m_EventList.UpdateYosanEventInfo(.Columns(activeColumn).Tag, strYosanEventCode, strResult) = True Then
                            'イベント情報一覧SPREADに「４．編集画面」の入力ボックスの値に置き換える
                            .SetValue(activeRow, activeColumn, strResult)
                        End If
                    Else
                        '「４．編集画面」の入力値とSPREADの値を比較して変更が無ければ確認画面を表示する
                        ComFunc.ShowInfoMsgBox(strTagName & "の値が修正されていません。" & vbLf & "更新せず、編集画面を閉じます。")
                    End If
                End If
            End With

        End Sub

#End Region

        ''' <summary>
        ''' タイマーコントロール
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub

        Private Sub btnYosanTally_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYosanTally.Click
            Me.Hide()

            Using frm As New FrmYosanEventListexcel()
                frm.ShowDialog()
            End Using
            Me.Show()

        End Sub
    End Class

End Namespace
