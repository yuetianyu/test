Imports EventSakusei
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports YosansyoTool.YosanshoEdit
Imports YosansyoTool.YosanEventNew
Imports YosansyoTool.YosanEventListExcel.Logic
Imports EBom.Common
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanEventListExcel

    Public Class FrmYosanEventListExcel

#Region " メンバー変数 "
        ''' <summary>ロジック</summary>
        Private m_EventList As DispYosanEventListExcel = Nothing
        ''' <summary> 完了通常フラグ（内部変数）</summary>
        Private m_Complete_Flg As Integer
        '''<summary>行のチェックボックス</summary>
        Private m_CheckFlg As String
        '''<summary>チェックした行の予算イベントコード</summary>
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
            m_EventList = New DispYosanEventListExcel(Me, LoginInfo.Now, frmYosanEventExcelShorichu)

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

                '区分リスト作成
                m_EventList.SetKbnCombo()
                '開発符号リスト作成
                m_EventList.SetKaihatsuFugoCombo()
                'イベントリスト作成
                m_EventList.SetEventCombo()
                '予算コードリスト作成
                m_EventList.SetYosanCodeCombo()

                '期間リスト作成
                m_EventList.SetKikanCombo()

                'スプレッドを初期化する
                m_EventList.InitializeSpread()

                'メッセージラベル
                LblMessage.Text = "集計開始ボタンをクリックしてください。"

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

#End Region

#Region "コンボボックス"

        ''' <summary>
        ''' 区分コンボボックスの値を変更後
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbKbn_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKbn.TextChanged
            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            '区分に該当する情報を開発符号コンボボックスへ設定する
            If StringUtil.IsNotEmpty(Me.cmbKbn.Text) Then
                m_EventList.SetKbnKaihatsuFugoCombo(Me.cmbKbn.Text)
            Else
                '開発符号リスト作成
                m_EventList.SetKaihatsuFugoCombo()
            End If

            Cursor.Current = Cursors.Default
        End Sub

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

            'イベントコンボボックスへ設定する
            If StringUtil.IsNotEmpty(Me.cmbKaihatsuFugo.Text) Then
                m_EventList.SetKaihatsuFugoEventCombo(Me.cmbKaihatsuFugo.Text)
            Else
                'イベントリスト作成
                m_EventList.SetEventCombo()
            End If

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

            '予算コードコンボボックスへ設定する
            If StringUtil.IsNotEmpty(Me.cmbEvent.Text) Then
                m_EventList.SetEventYosanCombo(Me.cmbEvent.Text)
            Else
                '予算コードリスト作成
                m_EventList.SetYosanCodeCombo()
            End If

            Cursor.Current = Cursors.Default
        End Sub

        ''' <summary>
        ''' コンボボックスに　delete　key　press
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Controls_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
            Handles cmbKbn.KeyDown, _
                cmbKaihatsuFugo.KeyDown, cmbEvent.KeyDown, cmbKikanFrom.KeyDown, cmbKikanTo.KeyDown, cmbYosanCode.KeyDown
            ShisakuFormUtil.DelKeyDown(sender, e)
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

        Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click

            'イベント情報一覧SPREADから該当する情報を表示する
            m_EventList.SetSpreadData(False)

        End Sub

        Private Sub btnYosanShukeiExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYosanShukeiExcelExport.Click
            Try

                '一覧が０件の場合スルー
                If spdParts_Sheet1.RowCount <= 0 Then
                    Return
                End If

                Dim MessageLine1 As String = "集計結果をExcelファイルに出力しますか？"
                Dim MessageLine2 As String = ""
                If frm01Kakunin.ConfirmOkCancel(MessageLine1, MessageLine2) <> MsgBoxResult.Ok Then
                    Return
                End If

                '画面を綺麗に、実行中のカーソルへ変更。
                Application.DoEvents()
                Cursor.Current = Cursors.WaitCursor

                Dim fileName As String = "集計結果EXCEL_" + Now.ToString("yyyyMMddHHmmss") + ".xls"
                Dim i As Integer = 0
                Dim eventVos As New List(Of TYosanEventVo)

                For row As Integer = 0 To spdParts_Sheet1.RowCount - 1

                    With spdParts_Sheet1
                        'チェックにチェック付きの行が処理対象
                        m_CheckFlg = .GetValue(row, _
                                                      DispYosanEventListExcel.GetTagIdx(spdParts_Sheet1, DispYosanEventListExcel.TAG_CHECK))
                        m_SelectEventCode = .GetValue(row, _
                                                      DispYosanEventListExcel.GetTagIdx(spdParts_Sheet1, DispYosanEventListExcel.TAG_YOSAN_EVENT_CODE))
                    End With

                    If StringUtil.Equals(m_CheckFlg, "False") Then
                        Continue For
                    End If

                    Dim eventVo As New TYosanEventVo
                    eventVo.YosanEventCode = m_SelectEventCode
                    eventVos.Add(eventVo)

                Next

                '2016/02/02 kabasawa'
                'Dim mEvents As New List(Of DispYosanEventListExcel)
                'For Each Vo As TYosanEventVo In eventVos
                '    ExcelExport(Vo, i, fileName, eventVos, mEvents)
                'Next
                'If mEvents.Count > 1 Then
                '    SumExcelExport(eventVos, fileName, mEvents)
                'End If
                'Process.Start(ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + fileName)


                '2016/02/02 kabasawa'
                '全体集計用'
                Dim mEvents As New List(Of DispYosanEventListExcel)
                Dim cse As New CreateSumExcelSheet()

                If eventVos.Count > 1 Then
                    cse.CreateSumSheet(eventVos)
                End If

                For Each Vo As TYosanEventVo In eventVos
                    cse.CreateSheet(Vo.YosanEventCode)
                Next

                'シートが移動できないので'
                If eventVos.Count > 1 Then
                    cse.SetSumSheet(eventVos)
                End If

                cse.SaveExcelFile(ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + fileName)

            Catch ex As Exception
                MsgBox("集計結果Excelの出力失敗しました。", MsgBoxStyle.Information, "エラー")
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub

    End Class

End Namespace
