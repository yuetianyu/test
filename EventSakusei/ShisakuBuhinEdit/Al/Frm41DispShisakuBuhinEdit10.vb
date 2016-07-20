Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports EventSakusei.ShisakuBuhinEdit.Al.Ui
Imports EventSakusei.ShisakuBuhinEdit.Selector
Imports FarPoint.Win.Spread.Model
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
Imports FarPoint.Win
Imports FarPoint.Win.Spread
Imports EventSakusei.ShisakuBuhinEdit.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.ShisakuBuhinEditBlock

Namespace ShisakuBuhinEdit.Al
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm41DispShisakuBuhinEdit10
        'SPREADのサイズ変更用に使用する。
        '１は標準サイズ
        Private Kakudai As Decimal = 1

        'コピー＆ペースト用
        Private w_RowCount As Integer = 0
        Private w_ColumnCount As Integer = 0
        'ヘッダーの行高の標準サイズ。
        Private w_HEAD As Integer = 164

        Private _dispMode As Integer

        ''↓↓2014/09/04 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
        Public aMakeStructureResult As MakeStructureResultImpl
        ''↑↑2014/09/04 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

        ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
        Public JikyuFlg As Boolean
        ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
        '------------------------------------------------------------------
        '閉じるボタンを無効化するロジックです。
        Protected Overrides ReadOnly Property CreateParams() As  _
            System.Windows.Forms.CreateParams
            Get
                Const CS_NOCLOSE As Integer = &H200
                Dim cp As CreateParams = MyBase.CreateParams
                cp.ClassStyle = cp.ClassStyle Or CS_NOCLOSE

                Return cp
            End Get
        End Property

        'フォームのFormClosingイベントハンドラ
        Private Sub frm41DispShisakuBuhinHensyu10_FormClosing(ByVal sender As System.Object, _
                ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

            e.Cancel = True
        End Sub

        Private Sub miInsertMemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miInsertMemo.Click
            alObserver.AddMemoColumn(5)
            alSubject.NotifyObservers()
        End Sub

        Private Sub msKinouHyouji_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msKinouHyouji.Click
            Using frm As New Frm43DispShisakuBuhinEditSelector(shisakuEventCode, alSubject.ShowColumnBag)
                frm.ShowDialog()
                If frm.ResultOk Then
                    alSubject.ShowColumnBag = frm.Result
                    alSubject.NotifyObservers()
                End If
            End Using
        End Sub

        Private Sub ToolStripPurasu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPurasu.Click
            Kakudai += 0.2
            If Kakudai > 2 Then
                MsgBox("これ以上拡大できません。", MsgBoxStyle.Information, "アラーム")
                Kakudai = 2
            End If
            spdParts.ActiveSheet.ZoomFactor = Kakudai
        End Sub

        Private Sub ToolStripMainasu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMainasu.Click
            Kakudai -= 0.2
            If Kakudai <= 0 Then
                MsgBox("これ以上縮小できません。", MsgBoxStyle.Information, "アラーム")
                Kakudai = 0.2
            End If
            spdParts.ActiveSheet.ZoomFactor = Kakudai
        End Sub

        Private Sub ToolStripHyoujyun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripHyoujyun.Click
            Kakudai = 1 'Nomalへ戻す
            spdParts.ActiveSheet.ZoomFactor = Kakudai
        End Sub

        Private ReadOnly headerSubject As ShisakuBuhinEdit.Logic.BuhinEditHeaderSubject

        Private ReadOnly shisakuEventCode As String
        Private ReadOnly shisakuBukaCode As String
        Private ReadOnly shisakuBlockNo As String
        Private alObserver As SpdAlObserver
        Public ReadOnly Property frmAlObserver() As SpdAlObserver
            Get
                Return alObserver
            End Get
        End Property
        Public alSubject As BuhinEditAlSubject
        Private ReadOnly inputSupport As ShisakuInputSupport
        Private koseiSubject As Kosei.Logic.BuhinEditKoseiSubject
        Private motoGamen As String
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal alSubject As BuhinEditAlSubject, _
                       ByVal koseiSubject As Kosei.Logic.BuhinEditKoseiSubject, _
                       ByVal dispMode As Integer, ByVal motoGamen As String, Optional ByVal shisakuBukaCode As String = "", Optional ByVal shisakuBlockNo As String = "")

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)
            'ShisakuFormUtil.setTitleVersion(Me)
            'タイトル名を表示する。
            Me.Text = "[ 試作Ａ／Ｌ作成機能 ]"

            Me.StartPosition = FormStartPosition.Manual

            Me.shisakuEventCode = shisakuEventCode

            Me.shisakuBukaCode = shisakuBukaCode
            Me.shisakuBlockNo = shisakuBlockNo
            Me.alSubject = alSubject
            Me.koseiSubject = koseiSubject
            Me.motoGamen = motoGamen

            _dispMode = dispMode

            If motoGamen.Equals("HoyouBuhinFrm41DispShisakuBuhinEdit00") Then
                alObserver = New SpdAlObserver(spdParts, alSubject, False)
            Else
                alObserver = New SpdAlObserver(spdParts, alSubject, True)
            End If

            inputSupport = New ShisakuInputSupport(TxtInputSupport, spdParts)

            InitializeSpread()
            Initialize()
        End Sub

        Public Sub Initialize()
            '削除ToolStripMenuItem.Visible = False   ' 仕様書には書いていない機能. 無いと使い勝手が悪い.

            If motoGamen.Equals("HoyouBuhinFrm41DispShisakuBuhinEdit00") Then
                'ツールメニューを使用不可
                Memo_TSMI.Enabled = False
                'チェックボックス列を表示
                alObserver.CheckBoxColumnVisible()
                'INSTL品番列を非表示
                alObserver.InstlColumnDisable()
                'タイトル行の「行の幅」を１行分に縮める
                alObserver.setTitleRowSpan()
                '明細セルデフォルト背景色（グラデーション部）をピンクから薄い青に変更
                alObserver.setRowColor()
                '「全て選択」「全て解除」ボタンを表示
                btnAllCheck.Visible = True
                btnAllCheckClear.Visible = True
                Label22.Visible = False
                TxtInputSupport.Visible = False
                '空白行をロック
                alObserver.EmptyRowLock()
                For rowindex As Integer = 0 To alSubject.CheckFlg.Count - 1
                    If alSubject.CheckFlg(rowindex) = True Then
                        spdParts_Sheet1.Cells(rowindex + alObserver.titleRows, SpdAlObserver.CHECK_FLG_COLUMN).Value = True
                    End If
                Next
            Else
                'チェックボックス列を非表示
                alObserver.CheckBoxColumnDisable()
            End If

        End Sub

        Private Sub btnAllCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllCheck.Click

            For rowIndex As Integer = SpdAlObserver.INSTL_HINBAN_KBN_ROW_INDEX + 1 To spdParts_Sheet1.Rows.Count - 1
                If Not StringUtil.IsEmpty(spdParts_Sheet1.Cells(rowIndex, spdParts_Sheet1.Columns(SpdAlObserver.TAG_GOSHA).Index).Value) Then
                    spdParts_Sheet1.Cells(rowIndex, 0).Value = True
                End If
            Next

        End Sub

        Private Sub btnAllCheckClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllCheckClear.Click

            For rowIndex As Integer = SpdAlObserver.INSTL_HINBAN_KBN_ROW_INDEX + 1 To spdParts_Sheet1.Rows.Count - 1
                If spdParts_Sheet1.Cells(rowIndex, 0).Value = True Then
                    spdParts_Sheet1.Cells(rowIndex, 0).Value = False
                End If
            Next

        End Sub

        Private Sub InitializeSpread()
            alObserver.Initialize()
            ShisakuSpreadUtil.AddEventCellRightClick(spdParts, inputSupport)


            'スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
            Dim imBase As New FarPoint.Win.Spread.InputMap
            imBase = Me.spdParts.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imBase.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

            Dim imGousya As New FarPoint.Win.Spread.InputMap
            imGousya = Me.spdParts.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imGousya.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

            If motoGamen.Equals("HoyouBuhinFrm41DispShisakuBuhinEdit00") Then
                alSubject.NotifyObservers()
            End If
        End Sub

        Public Sub AssertValidateRegister()
            alObserver.AssertValidateRegister()
        End Sub

        Public Sub AssertValidateHinban()
            alObserver.AssertValidateHinban()
        End Sub

        Public Sub AssertValidateSave()
            alObserver.AssertValidateSave()
        End Sub

        ''' <summary>
        ''' INSTL品番列の表示値をクリアする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearInstlColumns()
            alObserver.ClearInstlColumns(True)
        End Sub


        Public Sub ClearSheetBackColor()
            alObserver.ClearSheetBackColor()
        End Sub
        Public Sub ClearSheetBackColorAll()
            alObserver.ClearSheetBackColorAll()
        End Sub


        Private Sub ContextMenuStrip1_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

            '2013/05/14　
            '閲覧モードの場合は常に「全て」無効
            If _dispMode = VIEW_MODE Then
                切り取りToolStripMenuItem1.Enabled = False
                コピーToolStripMenuItem.Enabled = False
                貼り付けToolStripMenuItem.Enabled = False
                挿入ToolStripMenuItem.Enabled = False
                削除ToolStripMenuItem.Enabled = False
            Else

                'フィルタリング中かチェックする。
                'フィルタリング中なら行挿入、行削除を非表示にする。
                Dim wFilter As String = alObserver.FilterCheck

                切り取りToolStripMenuItem1.Enabled = False
                コピーToolStripMenuItem.Enabled = False
                貼り付けToolStripMenuItem.Enabled = False
                挿入ToolStripMenuItem.Enabled = False
                削除ToolStripMenuItem.Enabled = False

                If spdParts_Sheet1.SelectionCount <> 1 Then
                    Return
                End If

                Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)

                If SpreadUtil.IsSelectedColumn(selection) Then
                    If alObserver.CanInsertColumnRemoveColumn(selection) Then
                        ''↓↓2014/09/18 酒井 ADD BEGIN
                        '該当イベント取得
                        Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                        Dim eventVo As TShisakuEventVo
                        eventVo = eventDao.FindByPk(shisakuEventCode)
                        If Not (eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" And alSubject.BaseInstlFlg(alObserver.ConvSpreadColumnToAlInstl(selection.Column)) = 1) Then
                            ''↑↑2014/09/18 酒井 ADD END
                            挿入ToolStripMenuItem.Enabled = True
                            削除ToolStripMenuItem.Enabled = True
                            ''↓↓2014/09/18 酒井 ADD BEGIN
                        End If
                        ''↑↑2014/09/18 酒井 ADD END
                    End If
                Else
                    If SpreadUtil.IsSelectedRow(selection) Then
                    Else
                        切り取りToolStripMenuItem1.Enabled = True
                        コピーToolStripMenuItem.Enabled = True
                        貼り付けToolStripMenuItem.Enabled = True
                    End If
                End If

                'フィルタリング中だったら・・・
                If Not StringUtil.IsEmpty(wFilter) Then
                    挿入ToolStripMenuItem.Enabled = False
                    削除ToolStripMenuItem.Enabled = False
                    切り取りToolStripMenuItem1.Enabled = False
                    コピーToolStripMenuItem.Enabled = False
                    貼り付けToolStripMenuItem.Enabled = False
                End If

                'タイトル行だったら
                Dim sheet As Spread.SheetView = spdParts.ActiveSheet
                Dim titleRows As Integer = BuhinEditSpreadUtil.GetTitleRows(sheet)
                If selection.Row < titleRows Then
                    切り取りToolStripMenuItem1.Enabled = False
                    コピーToolStripMenuItem.Enabled = False
                    貼り付けToolStripMenuItem.Enabled = False
                End If

                End If

        End Sub

        Private Sub 挿入IToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 挿入ToolStripMenuItem.Click
            If spdParts_Sheet1.SelectionCount <> 1 Then
                Return
            End If

            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)

            If SpreadUtil.IsSelectedColumn(selection) Then
                alObserver.InsertColumns(selection.Column, selection.ColumnCount)
            End If
        End Sub

        Private Sub 削除ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 削除ToolStripMenuItem.Click
            If spdParts_Sheet1.SelectionCount <> 1 Then
                Return
            End If

            ''2015/08/25 変更 E.Ubukata　Ver 2.11.0
            '' 複数列削除対応
            '' １列ずつで問題がないのであれば、複数選択列の後ろから１列ずつ処理すれば問題ないはず
            Dim selections As FarPoint.Win.Spread.Model.CellRange() = spdParts_Sheet1.GetSelections()
            Dim colList As New ArrayList
            For Each sel As FarPoint.Win.Spread.Model.CellRange In selections
                Dim col As Integer = sel.Column
                For i As Integer = 0 To sel.ColumnCount - 1
                    ''2015/08/28 追加 E.Ubukata　Ver 2.11.0
                    '' 移管車改修時にベースの直右に列を追加し何もせずに削除を行うと構成再展開で不具合が発生するため
                    '' ダミーの文字列を入力してから削除を行う（何か入力した後削除を行うと問題ないため）
                    ''   ※本来はKoseiMatrix(CoklumnBug)側を直すべき
                    If StringUtil.IsEmpty(spdParts_Sheet1.Cells(3, col).Value) Then
                        spdParts_Sheet1.Cells(3, col).Value = "A"
                    End If

                    colList.Add(col + i)
                Next
            Next
            'カラムをソート
            colList.Sort()
            '順序を反転
            colList.Reverse()
            '選択列の後ろから１列ずつ削除
            For Each col As Integer In colList
                ''2015/08/28 追加 E.Ubukata　Ver 2.11.0
                '' 移管車改修時にベースの直右に列を追加し何もせずに削除を行うと構成再展開で不具合が発生するため
                '' ダミーの文字列を入力してから削除を行う（何か入力した後削除を行うと問題ないため）
                ''   ※本来はKoseiMatrix(CoklumnBug)側を直すべき
                If StringUtil.IsEmpty(spdParts_Sheet1.Cells(3, col).Value) Then
                    spdParts_Sheet1.Cells(3, col).Value = "A"
                End If


                alObserver.RemoveColumns(col, 1, Me.shisakuEventCode)
            Next


            'Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)
            'If SpreadUtil.IsSelectedColumn(selection) Then
            '    '2014/05/15 kabasawa'
            '    '１次対応として複数列削除を禁止する'
            '    If selection.ColumnCount <> 1 Then
            '        MsgBox("複数列の削除はシステム不具合があり現在修正中です。" & vbCr & "お手数お掛けしますが、１列ずつ削除をお願いします。", MsgBoxStyle.OkOnly, "エラー")
            '        Exit Sub
            '    End If
            '    ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_s) (TES)張 CHG BEGIN
            '    'alObserver.RemoveColumns(selection.Column, selection.ColumnCount)
            '    alObserver.RemoveColumns(selection.Column, selection.ColumnCount, Me.shisakuEventCode)
            '    ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_s) (TES)張 CHG END
            'End If
        End Sub

        Private Sub Frm41DispShisakuBuhinEdit10_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'フィルタリングを全列に設定
            'OptionFilterCommon.SetOptionFilter(spdParts.ActiveSheet, spdParts.ActiveSheet.ColumnCount - 1, 5)

            'If Not StringUtil.IsEmpty(alSubject.EventUpdatedDate) _
            '    And Not StringUtil.IsEmpty(alSubject.SaisyuKoushinbi) Then
            '    If alSubject.EventUpdatedDate > alSubject.SaisyuKoushinbi Then
            '        If frm41ShiyouKakunin.ConfirmOk("イベントが更新されました。", "仕様表示画面で再選択してください。") _
            '             = MsgBoxResult.Ok Then
            '            Using frm As New Frm43DispShisakuBuhinEditSelector(shisakuEventCode, alSubject.ShowColumnBag)
            '                frm.ShowDialog()
            '                If frm.ResultOk Then
            '                    alSubject.ShowColumnBag = frm.Result
            '                    alSubject.NotifyObservers()
            '                End If
            '            End Using
            '        End If
            '    End If
            'End If

        End Sub


        Public Sub EventChange()
            '2012/03/07 「イベントが更新されました。」メッセージを表示する判定基準を
            'いべんとの更新日時から登録日時に変更
            If Not StringUtil.IsEmpty(alSubject.EventRegisterDate) _
            And Not StringUtil.IsEmpty(alSubject.SaisyuKoushinbi) Then

                If Integer.Parse(alSubject.EventRegisterDate) > Integer.Parse(alSubject.SaisyuKoushinbi) Then
                    '------------------------------------------------------------------------------------------------------
                    '２次改修
                    '   以下の画面は廃止とする。
                    'If frm41ShiyouKakunin.ConfirmOk("イベントが更新されました。", "仕様表示画面で再選択してください。") _
                    '     = MsgBoxResult.Ok Then
                    '    Using frm As New Frm43DispShisakuBuhinEditSelector(shisakuEventCode, alSubject.ShowColumnBag)
                    '        frm.ShowDialog()
                    '        If frm.ResultOk Then
                    '            alSubject.ShowColumnBag = frm.Result
                    '            alSubject.NotifyObservers()
                    '        End If
                    '    End Using
                    'End If
                    '------------------------------------------------------------------------------------------------------
                ElseIf Integer.Parse(alSubject.EventRegisterDate) = Integer.Parse(alSubject.SaisyuKoushinbi) Then
                    If Integer.Parse(alSubject.EventRegisterTime) > Integer.Parse(alSubject.SaisyuKoushinjikan) Then
                        '------------------------------------------------------------------------------------------------------
                        '２次改修
                        '   以下の画面は廃止とする。
                        'If frm41ShiyouKakunin.ConfirmOk("イベントが更新されました。", "仕様表示画面で再選択してください。") _
                        '= MsgBoxResult.Ok Then
                        '    Using frm As New Frm43DispShisakuBuhinEditSelector(shisakuEventCode, alSubject.ShowColumnBag)
                        '        frm.ShowDialog()
                        '        If frm.ResultOk Then
                        '            alSubject.ShowColumnBag = frm.Result
                        '            alSubject.NotifyObservers()
                        '        End If
                        '    End Using
                        'End If
                        '------------------------------------------------------------------------------------------------------
                    End If

                End If
            End If
        End Sub

        Private Sub コピーToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles コピーToolStripMenuItem.Click
            'オプション・コピーサブルーチンへ
            OptionCopy()
        End Sub

        Private Sub コピーCToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles コピーCToolStripButton.Click
            'オプション・コピーサブルーチンへ
            OptionCopy()
        End Sub

        ''' <summary>
        ''' 貼り付けツールストリップ
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 貼り付けToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 貼り付けToolStripMenuItem.Click
            'オプション・ペーストサブルーチンへ
            OptionPaste()
        End Sub

        ''' <summary>
        ''' 貼り付けボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 貼り付けPToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 貼り付けPToolStripButton.Click
            'オプション・ペーストサブルーチンへ
            OptionPaste()
        End Sub

        ''' <summary>
        ''' 切り取りツールストリップ
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 切り取りToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 切り取りToolStripMenuItem1.Click
            'オプション・カットサブルーチンへ
            OptionCut()
        End Sub

        ''' <summary>
        ''' 切り取りツールボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 切り取りUToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            'オプション・カットサブルーチンへ
            OptionCut()
        End Sub

        ''' <summary>
        ''' セル選択
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Sequence1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sequence1.Click
            Try
                ' 現在選択しているセル範囲を取得します。
                cr = spdParts.ActiveSheet.GetSelection(0)
                'アクティブセル列を非表示にします。
                'spdParts.ActiveSheet.Columns(cr.Column).Visible = False
                'spdParts.ActiveSheet.SetActiveCell(cr.Row, cr.Column + 1)
                '最終列を計算
                Dim w_Count As Integer = cr.ColumnCount + cr.Column - 1
                '選択しているアクティブセル列の非表示列を表示します。
                For i As Integer = cr.Column To w_Count
                    spdParts.ActiveSheet.Columns(i).Visible = False
                Next
            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try
        End Sub

        ''' <summary>
        ''' セル選択２
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Sequence2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sequence2.Click
            Try
                ' 現在選択しているセル範囲を取得します。
                cr = spdParts.ActiveSheet.GetSelection(0)
                '最終列を計算
                Dim w_Count As Integer = cr.ColumnCount + cr.Column - 1
                '選択しているアクティブセル列の非表示列を表示します。
                For i As Integer = cr.Column To w_Count
                    spdParts.ActiveSheet.Columns(i).Visible = True
                Next
            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try
        End Sub

        ''' <summary>
        ''' セル選択３
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Sequence3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sequence3.Click
            Try
                ' 現在選択しているセル範囲を取得します。
                cr = spdParts.ActiveSheet.GetSelection(0)
                '最終列(備考列の列)を計算
                Dim w_Count As Integer = spdParts_Sheet1.ColumnCount - 1
                '選択しているアクティブセル列の非表示列を表示します。
                For i As Integer = 0 To w_Count
                    spdParts.ActiveSheet.Columns(i).Visible = True
                Next
            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try
        End Sub

        ''' <summary>
        ''' 縮小ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub HEADDW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HEADDW.Click
            w_HEAD -= 4
            If w_HEAD < 100 Then
                MsgBox("これ以上縮小できません。", MsgBoxStyle.Information, "アラーム")
                w_HEAD = 100
            End If
            'ヘッダーの行高を縮小します。。
            spdParts.ActiveSheet.Rows(2).Height = w_HEAD
        End Sub

        ''' <summary>
        ''' 拡大ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub HEADUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HEADUP.Click
            w_HEAD += 4
            If w_HEAD > 250 Then
                MsgBox("これ以上拡大できません。", MsgBoxStyle.Information, "アラーム")
                w_HEAD = 250
            End If
            'ヘッダーの行高を縮小します。。
            spdParts.ActiveSheet.Rows(2).Height = w_HEAD
        End Sub

        ''' <summary>
        ''' 元に戻す
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub UNdo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UNdo.Click
            Try
                ' 元に戻すことが出来るか確認します。。
                If spdParts.UndoManager.CanUndo = True Then
                    spdParts.UndoManager.Undo()
                End If
            Catch Exception As Exception
                MessageBox.Show("元に戻す事が出来ません。")
                Exit Sub
            End Try
        End Sub

        ''' <summary>
        ''' やり直し
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub REdo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles REdo.Click
            Try
                ' やり直しが出来るか確認します。
                If spdParts.UndoManager.CanRedo = True Then
                    spdParts.UndoManager.Redo()
                End If
            Catch Exception As Exception
                MessageBox.Show("やり直しが出来ません。")
                Exit Sub
            End Try
        End Sub

        ''' <summary>
        ''' スプレッド変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdParts.Change

            Dim sheet As Spread.SheetView = spdParts.ActiveSheet
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            ' 該当セルの文字色、文字太を変更する。
            sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle())

            '何故かUpdateやらに行かない・・・'

        End Sub

        ''' <summary>
        ''' フォント色を青色に、文字を太くする。
        ''' </summary>
        ''' <remarks></remarks>
        Private Function CreateNewStyle() As Spread.StyleInfo
            Dim styleinfo As New Spread.StyleInfo
            styleinfo.ForeColor = Color.Blue '青色に
            styleinfo.Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
            Return styleinfo

        End Function

#Region "ツールボタン(フィルタ解除)"
        ''' <summary>
        ''' ツールボタン(フィルタ解除)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub FilterCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterCancel.Click

            ''全列のフィルタリングを解除
            'For colIdx As Integer = 0 To spdParts.ActiveSheet.ColumnCount - 1
            ' '   OptionFilterCommon.SetFilterCancel(spdParts.ActiveSheet, colIdx, 5)
            'Next

            Try

                Cursor = Cursors.WaitCursor

                '全列のフィルタリングを解除
                alObserver.ResetFilter()

            Catch ex As Exception
                MsgBox(String.Format("フィルタリング解除でシステムエラーが発生しました。({0})", ex.Message), MsgBoxStyle.Critical)
            Finally
                Cursor = Cursors.Default
            End Try

        End Sub
#End Region

#Region "ツールボタン(フィルタ設定)"
        ''' <summary>
        ''' ツールボタン(フィルタ設定)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub SetFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetFilter.Click

            Try
                Cursor = Cursors.WaitCursor

                'フィルタ設定
                alObserver.SetFiltering()

            Catch ex As Exception
                MsgBox(String.Format("フィルタリング設定処理でシステムエラーが発生しました。({0})", ex.Message), MsgBoxStyle.Critical)
            Finally
                Cursor = Cursors.Default
            End Try

        End Sub

#End Region

#Region "ツールボタン（コピーペーストボタン対応)"

#Region "コピーイベント"
        ''' <summary>
        ''' コピーイベント
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub OptionCopy()
            Try

                Dim spd As FarPoint.Win.Spread.FpSpread = Me.spdParts

                spd.Focus()

                System.Threading.Thread.Sleep(10)
                System.Windows.Forms.SendKeys.Flush()

                'スプレッドにCTRL+Cキーを送信
                System.Windows.Forms.SendKeys.Send("^c")
                ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！

            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try
        End Sub
#End Region

#Region "切取りイベント"
        ''' <summary>
        ''' 切取りイベント
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub OptionCut()

            Try
                Dim spd As FarPoint.Win.Spread.FpSpread = Me.spdParts

                spd.Focus()

                System.Threading.Thread.Sleep(10)

                System.Windows.Forms.SendKeys.Flush()

                'スプレッドにCTRL+xキーを送信
                System.Windows.Forms.SendKeys.Send("^x")
                ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！

            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try

        End Sub

#End Region

#Region "貼りつけイベント"
        ''' <summary>
        ''' 貼りつけイベント
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub OptionPaste()
            Try

                Dim spd As FarPoint.Win.Spread.FpSpread = Me.spdParts

                spd.Focus()

                System.Windows.Forms.SendKeys.Flush()
                System.Threading.Thread.Sleep(10)
                'スプレッドにCTRL+vキーを送信
                System.Windows.Forms.SendKeys.Send("^v")
                ' このコマンド以降でデバッグの為ブレイクポイントを設定するとフリーズするので注意！！！
            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try
        End Sub

#End Region

#Region "キー押下イベント"
        ''' <summary>
        ''' キー押下イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdParts.KeyDown
            Dim downKey As Object
            Dim sheet As Spread.SheetView = Me.spdParts_Sheet1
            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

            downKey = e.KeyCode()

            Select Case downKey

                Case Keys.C
                    '情報列タイトルの色をチェック
                    For i As Integer = 0 To sheet.ColumnCount - 1
                        '青色か？
                        If sheet.Cells(0, i, 2, i).ForeColor = Color.Blue Then
                            Exit Sub
                        End If
                    Next

                    'コントロールキーとCキーが押された
                    If (e.Modifiers And Keys.Control) = Keys.Control Then

                        '書式バックアップ
                        Dim listBln As List(Of Boolean()) = GetEditCellInfo(sheet)
                        '書式を一時的に全て保存編集対象にする
                        SetUndoCellFormat(sheet)
                        'コピー
                        sheet.ClipboardCopy()
                        '書式を戻す
                        SetUndoCellFormat(sheet, listBln)
                    End If

                Case Keys.V
                    '情報列タイトルの色をチェック
                    For i As Integer = 0 To sheet.ColumnCount - 1
                        '青色か？
                        If sheet.Cells(0, i, 2, i).ForeColor = Color.Blue Then
                            Exit Sub
                        End If
                    Next

                    '行選択ではコントロールキーとVキーは無効に
                    If Not selection Is Nothing Then
                        If selection.Column = -1 AndAlso selection.ColumnCount - 1 Then
                            e.Handled = True
                        Else

                            '行判定をとりあえず持たせてみる'
                            If selection.Row = -1 AndAlso selection.RowCount - 1 Then
                                e.Handled = True
                            End If

                            'コントロールキーとVキーが押された
                            If (e.Modifiers And Keys.Control) = Keys.Control Then

                                Dim listClip As New List(Of String())

                                listClip = GetClipbordList()

                                Dim value As String = ""

                                If Not listClip Is Nothing Then

                                    For col As Integer = 0 To selection.ColumnCount - 1
                                        For rowindex As Integer = 0 To selection.RowCount - 1
                                            If Not Me.spdParts_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                                Me.spdParts_Sheet1.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                            End If
                                        Next
                                    Next


                                    Dim rowCount As Integer = listClip.Count - 1
                                    Dim colCount As Integer = listClip(0).Length

                                    'セル編集モード時にコピーした場合、以下を行う。
                                    If rowCount = 0 Then
                                        rowCount = 1
                                    End If

                                    '行選択時
                                    If selection.Column = -1 Then

                                        '貼りつけ対象のセルを編集済みとし書式を設定する
                                        Me.spdParts_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                                        Me.spdParts_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).ForeColor = Color.Blue


                                    Else

                                        If (selection.Column + colCount) >= sheet.ColumnCount - 1 Then
                                            EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けようとしている範囲がスプレッド表の最大列を超えています")
                                            Return
                                        End If

                                        If (selection.Row + rowCount) >= sheet.RowCount - 1 Then
                                            EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                            Return
                                        End If

                                        '貼りつけ対象のセルを編集済みとし書式を設定する
                                        Me.spdParts_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                               selection.Column + colCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                        Me.spdParts_Sheet1.Cells(selection.Row, selection.Column, selection.Row + rowCount - 1, _
                                                        selection.Column + colCount - 1).ForeColor = Color.Blue
                                    End If

                                End If

                            End If
                        End If
                    Else
                        If (e.Modifiers And Keys.Control) = Keys.Control Then
                            Dim listClip As New List(Of String())

                            listClip = GetClipbordList()

                            Dim value As String = ""

                            If Not listClip Is Nothing Then

                                If Not Me.spdParts_Sheet1.Cells(sheet.ActiveRowIndex, sheet.ActiveColumnIndex).Locked Then
                                    Me.spdParts_Sheet1.Cells(sheet.ActiveRowIndex, sheet.ActiveColumnIndex).Value = listClip(0)(0)
                                End If

                                Dim rowCount As Integer = listClip.Count - 1
                                Dim colCount As Integer = listClip(0).Length

                                'セル編集モード時にコピーした場合、以下を行う。
                                If rowCount = 0 Then
                                    rowCount = 1
                                End If

                                '行選択時

                                If (sheet.ActiveColumnIndex) >= sheet.ColumnCount - 1 Then
                                    EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けようとしている範囲がスプレッド表の最大列を超えています")
                                    Return
                                End If

                                If (sheet.ActiveRowIndex) > sheet.RowCount - 1 Then
                                    EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                    Return
                                End If

                                '貼りつけ対象のセルを編集済みとし書式を設定する
                                Me.spdParts_Sheet1.Cells(sheet.ActiveRowIndex, sheet.ActiveColumnIndex, sheet.ActiveRowIndex, _
                                                         sheet.ActiveColumnIndex).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                Me.spdParts_Sheet1.Cells(sheet.ActiveRowIndex, sheet.ActiveColumnIndex, sheet.ActiveRowIndex, _
                                                         sheet.ActiveColumnIndex).ForeColor = Color.Blue
                            End If


                        End If

                    End If

                Case Keys.Delete
                    ' INSTL品番のセルではDeleteは無効に
                    ' 試作区分はDeleteはOK。
                    If Not selection Is Nothing Then
                        If SpdAlObserver.INSTL_HINBAN_ROW_INDEX = selection.Row And _
                        alObserver.IsInstlSpreadColumn(selection.Column) = True Then
                            e.Handled = True
                        End If

                        '行選択・列選択ではDeleteは無効に
                        If (selection.Column = -1 AndAlso selection.ColumnCount - 1) Then
                            e.Handled = True
                        End If

                        If (selection.Row = -1 AndAlso selection.RowCount - 1) Then
                            e.Handled = True
                        End If

                        '------------------------------------------------------------------------
                        '2012/07/25　柳沼
                        Dim row As Integer = selection.Row
                        Dim col As Integer = selection.Column
                        Dim colcount As Integer = selection.ColumnCount
                        Dim rowcount As Integer = selection.RowCount
                        For rowindex As Integer = row To row + rowcount - 1
                            'フィルタ中の行は対象外'
                            If sheet.Rows(rowindex).Visible Then
                                'タイトル行も対象外'
                                If row > SpdAlObserver.INSTL_HINBAN_KBN_ROW_INDEX Then
                                    For colindex As Integer = col To col + colcount - 1
                                        'ロックセルは対象外
                                        If sheet.GetStyleInfo(rowindex, colindex).Locked = True Then
                                            Continue For
                                        End If
                                        sheet.ClearRange(rowindex, colindex, 1, 1, True)
                                        '2012/07/25　柳沼
                                        '   フィルタで非表示行の値がクリアされてしまうので
                                        '   無効にする。
                                        e.Handled = True
                                    Next
                                End If
                            Else
                                e.Handled = True
                            End If
                        Next
                        '------------------------------------------------------------------------

                    Else
                        '------------------------------------------------------------------------
                        '2012/07/25　柳沼
                        Dim row As Integer = sheet.ActiveRowIndex
                        Dim col As Integer = sheet.ActiveColumnIndex
                        Dim colcount As Integer = 1
                        Dim rowcount As Integer = 1

                        For rowindex As Integer = row To row + rowcount - 1
                            'フィルタ中の行は対象外'
                            If sheet.Rows(rowindex).Visible Then
                                'タイトル行も対象外'
                                If row > SpdAlObserver.INSTL_HINBAN_KBN_ROW_INDEX Then
                                    For colindex As Integer = col To col + colcount - 1
                                        'ロックセルは対象外
                                        If sheet.GetStyleInfo(rowindex, colindex).Locked = True Then
                                            Continue For
                                        End If
                                        sheet.ClearRange(rowindex, colindex, 1, 1, True)
                                        'koseiSubject.ClearCell(row, col)
                                    Next
                                End If
                            End If
                        Next
                        '------------------------------------------------------------------------
                        e.Handled = True
                    End If
            End Select
        End Sub

#End Region

#Region "クリップボードの内容をstring()型のリストに格納し返す"
        Public Shared Function GetClipbordList() As List(Of String())
            Dim listStr As New List(Of String())

            'システムクリップボードにあるデータを取得します
            Dim iData As IDataObject = Clipboard.GetDataObject()

            Dim strRow() As String

            'テキスト形式データの判断
            If iData.GetDataPresent(DataFormats.Text) = False Then
                Return Nothing
            Else

                Console.WriteLine(CType(iData.GetData(DataFormats.Text), String))
                strRow = CType(iData.GetData(DataFormats.Text), String).Split(vbCrLf)

            End If

            For i As Integer = 0 To strRow.Length - 1
                Dim strChar() As String = strRow(i).Split(vbTab)
                listStr.Add(strChar)
            Next

            Return listStr

        End Function

#End Region

#Region "編集書済式有無のセル配列を返す"
        ''' <summary>
        ''' 編集済書式有無のセル配列を返す
        ''' </summary>
        ''' <param name="aSheet">対象シートをセットする</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEditCellInfo(ByVal aSheet As SheetView) As List(Of Boolean())

            Dim selection As FarPoint.Win.Spread.Model.CellRange = aSheet.GetSelection(0)
            Dim listBln As New List(Of Boolean())

            If Not selection Is Nothing Then
                For i As Integer = 0 To selection.RowCount - 1

                    Dim blnTbl() As Boolean = Nothing
                    Dim colCnt As Integer = 0
                    Dim col As Integer = 0

                    If selection.ColumnCount = -1 Then
                        colCnt = aSheet.ColumnCount
                        col = 0
                    Else
                        colCnt = selection.ColumnCount
                        col = selection.Column
                    End If

                    ReDim Preserve blnTbl(colCnt - 1)

                    For j As Integer = 0 To colCnt - 1
                        Dim objFont As System.Drawing.Font = aSheet.Cells(selection.Row + i, col + j).Font

                        '太字Cellを編集済セルと判定
                        If Not objFont Is Nothing AndAlso objFont.Bold = True Then
                            blnTbl(j) = True
                        Else
                            blnTbl(j) = False
                        End If

                    Next
                    listBln.Add(blnTbl)
                Next
            Else
                Dim blnTbl() As Boolean = Nothing
                Dim colCnt As Integer = 0
                Dim col As Integer = 0
                colCnt = 1
                col = aSheet.ActiveColumnIndex
                ReDim Preserve blnTbl(colCnt - 1)
                For j As Integer = 0 To colCnt - 1
                    Dim objFont As System.Drawing.Font = aSheet.Cells(aSheet.ActiveRowIndex, col).Font

                    '太字Cellを編集済セルと判定
                    If Not objFont Is Nothing AndAlso objFont.Bold = True Then
                        blnTbl(j) = True
                    Else
                        blnTbl(j) = False
                    End If

                Next
                listBln.Add(blnTbl)
            End If

            Return listBln

        End Function

#End Region

#Region "コピーの時に一時的に編集済書式を設定する、また設定した書式を元に戻す"
        ''' <summary>
        ''' コピーの時に一時的に書式を設定する、また設定した書式を元に戻す
        ''' 
        ''' この処理はCTRL+cでの貼りつけの場合書式と値がコピーされてしまうため、単純操作では
        ''' 貼付け先に編集済みの書式が設定出来ません。
        ''' この問題に対応する為に、編集済み書式をCTRL+Cを送信する前に設定し、
        ''' 送信後に元の書式にするという対応が必要になります。
        ''' 
        ''' そもそも、こんな面倒な事が必要な原因は
        ''' 
        ''' コードで"spdParts_Sheet1.ClipboardPaste"と単純に記述されて実行された操作は
        ''' Undo操作が一切対象外になるという事が原因です。
        ''' 
        ''' Undoを行うにはキーボードからCTRL+Xなどの操作をコードから行う必要があり
        ''' SendKeyの様なコードが記述されています。
        ''' 
        ''' 
        ''' </summary>
        ''' <param name="aSheet">対象シート</param>
        ''' <param name="alistBln">書式を全て編集済書式にするときは指定しない</param>
        ''' <remarks></remarks>
        Public Shared Sub SetUndoCellFormat(ByVal aSheet As SheetView, Optional ByVal alistBln As List(Of Boolean()) = Nothing)

            Dim selection As FarPoint.Win.Spread.Model.CellRange = aSheet.GetSelection(0)
            Dim colCnt As Integer = 0
            Dim col As Integer = 0
            If Not selection Is Nothing Then
                If selection.ColumnCount = -1 Then
                    colCnt = aSheet.ColumnCount
                    col = 0
                Else
                    colCnt = selection.ColumnCount
                    col = selection.Column
                End If
                '無い場合は全て保存対象編集書式とするため全てTrueをセット
                If alistBln Is Nothing Then
                    alistBln = New List(Of Boolean())

                    For i As Integer = 0 To selection.RowCount - 1

                        Dim blnTbl() As Boolean = Nothing
                        ReDim Preserve blnTbl(colCnt - 1)

                        For j As Integer = 0 To colCnt - 1
                            blnTbl(j) = True
                        Next
                        alistBln.Add(blnTbl)
                    Next

                End If

                '受け取ったListの内容で書式を設定
                For i As Integer = 0 To selection.RowCount - 1
                    For j As Integer = 0 To selection.ColumnCount - 1

                        If alistBln(i)(j) = False Then
                            aSheet.Cells(selection.Row + i, selection.Column + j).ForeColor = Nothing
                            aSheet.Cells(selection.Row + i, selection.Column + j).Font = Nothing
                        Else
                            aSheet.Cells(selection.Row + i, selection.Column + j).ForeColor = Color.Blue
                            aSheet.Cells(selection.Row + i, selection.Column + j).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                        End If

                    Next
                Next
            Else
                colCnt = 1
                col = aSheet.ActiveColumnIndex
                If alistBln Is Nothing Then
                    alistBln = New List(Of Boolean())

                    Dim blnTbl() As Boolean = Nothing
                    ReDim Preserve blnTbl(colCnt - 1)

                    For j As Integer = 0 To colCnt - 1
                        blnTbl(j) = True
                    Next
                    alistBln.Add(blnTbl)

                End If

                '受け取ったListの内容で書式を設定

                If alistBln(0)(0) = False Then
                    aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).ForeColor = Nothing
                    aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).Font = Nothing
                Else
                    aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).ForeColor = Color.Blue
                    aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                End If

            End If

        End Sub
#End Region

#End Region

        Private Sub spdParts_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdParts.EditModeOn
            Dim sheet As Spread.SheetView = spdParts_Sheet1
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ列インデックス

            If alObserver.IsInstlSpreadColumn(ParaActColIdx) = True Then
                '2012/01/09
                If ParaActRowIdx = 4 Then
                    '試作区分のみ半角カナ入力可能にする為、IMEを使用可能にする。
                    spdParts.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
                    spdParts.ImeMode = Windows.Forms.ImeMode.NoControl
                Else
                    'メモ欄とINSTL品番のセルタイプの切り分けを行う為、INSTL品番の欄の場合セルタイプを再設定
                    alObserver.setInstlHinbanCell(ParaActColIdx)
                    'IMEを使用不可能にする。
                    spdParts.EditingControl.ImeMode = Windows.Forms.ImeMode.Disable
                    spdParts.ImeMode = Windows.Forms.ImeMode.Disable
                End If
            Else
                'メモ欄とINSTL品番のセルタイプの切り分けを行う為、メモ欄の場合セルタイプを再設定
                If alObserver.IsMemoSpreadColumn(ParaActColIdx) Then
                    alObserver.setMemoCell(ParaActColIdx)
                End If
                'IMEを使用可能にする。
                spdParts.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
                spdParts.ImeMode = Windows.Forms.ImeMode.NoControl
            End If

        End Sub
        Private Sub spdParts_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdParts.EditModeOff
            spdParts.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
            spdParts.ImeMode = Windows.Forms.ImeMode.NoControl
        End Sub
        ''' <summary>
        ''' スプレッドの表示を非表示に制御する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub viewLockForViewMode()

            'コントロールをロックする
            Me.ToolStrip1.Enabled = True
            Me.Memo_TSMI.Enabled = True
            'Me.切り取りUToolStripButton.Enabled = False
            Me.コピーCToolStripButton.Enabled = False
            Me.貼り付けPToolStripButton.Enabled = False
            Me.TxtInputSupport.Enabled = False
            Me.msEventCopy.Enabled = False

            Me.FINAL作成ToolStripMenuItem.Enabled = False
            Me.改訂コピーToolStripMenuItem.Enabled = False
            For i As Integer = 0 To spdParts_Sheet1.RowCount - 1
                For j As Integer = 0 To spdParts_Sheet1.ColumnCount - 1
                    spdParts_Sheet1.Cells(i, j).Locked = True
                Next
            Next
        End Sub

        ''' <summary>
        ''' イベント品番コピー
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub msEventCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles msEventCopy.Click
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo = eventDao.FindByPk(shisakuEventCode)

            Using frm As New Frm44DispEventBuhinCopySelector(alSubject.BlockNo, alSubject.shisakuEventCode)

                If frm.EventCount Then
                    frm.ShowDialog()

                    If frm.result = MsgBoxResult.Ok Then

                        alSubject.backup()

                        ''全列削除を行う'
                        If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                            'ベースINSTL以外の全列削除を行う'
                            alObserver.RemoveInstlColumnsAll(1, True)
                        Else
                            '全列削除を行う'
                            alObserver.RemoveInstlColumnsAll(1)
                        End If

                        If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                            alSubject.EventCopy(frm.selectedShisakuEventCode, "", True, True, koseiSubject)
                        Else
                            alSubject.EventCopy(frm.selectedShisakuEventCode, "", True)
                        End If
                        alSubject.NotifyObservers()

                        Dim result As DialogResult = MsgBox("確定しますか？", MsgBoxStyle.OkCancel)

                        If result = 2 Then
                            '全列削除を行う'
                            alObserver.RemoveInstlColumnsAll(1)
                            alSubject.test()
                            alSubject.BackEvent()
                            '構成を元に戻す'
                            alSubject.NotifyObservers()


                        End If

                    End If

                Else
                    MsgBox("このブロックは他のイベントには存在しません。", MsgBoxStyle.OkOnly, "エラー")
                End If
            End Using
        End Sub

        ''' <summary>
        ''' セルのエンターイベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_EnterCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EnterCellEventArgs) Handles spdParts.EnterCell
            '試作品番区分をデフォルト半角入力に制御する為に実装
            Dim sheet As Spread.SheetView = spdParts_Sheet1
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            If alObserver.IsInstlSpreadColumn(ParaActColIdx) Then
                alObserver.setInstlHinbanCell(ParaActColIdx)
                'IMEを使用不可能にする。
                spdParts.ImeMode = Windows.Forms.ImeMode.Disable
            End If
            If alObserver.IsMemoSpreadColumn(ParaActColIdx) Then
                alObserver.setMemoCell(ParaActColIdx)
                spdParts.ImeMode = Windows.Forms.ImeMode.NoControl
            End If

        End Sub

        Private Sub spdParts_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles spdParts.LeaveCell
            e.Cancel = CheckValue()
        End Sub

        Private Sub spdParts_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles spdParts.Validating
            e.Cancel = CheckValue()
        End Sub
        Private Function CheckValue() As Boolean

            Dim sheet As Spread.SheetView = spdParts.ActiveSheet
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            '2013/05/23　入力値をチェック　１以外ならエラーを表示して値をクリアする。
            '   INSTL品番列

            Dim titleRows As Integer = BuhinEditSpreadUtil.GetTitleRows(sheet)
            If alObserver.IsInstlSpreadColumn(ParaActColIdx) = True And ParaActRowIdx > titleRows - 1 Then
                If sheet.Cells(ParaActRowIdx, ParaActColIdx).Value <> "1" And sheet.Cells(ParaActRowIdx, ParaActColIdx).Value <> "" Then
                    EBom.Common.ComFunc.ShowErrMsgBox("'1'を入力してください。")
                    ' セルの編集を開始します
                    spdParts.StartCellEditing(Nothing, False)
                    Return True
                End If
            End If

            Return False
        End Function

        Private Sub 追加ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 追加ToolStripMenuItem.Click
            JikyuFlg = koseiSubject.JikyuFlg

            Dim hoyouVos As New List(Of THoyouBuhinEditVo)
            Dim hoyouInstlVos As New List(Of TShisakuBuhinEditInstlVo)

            Dim wk As New ArrayList
            Dim instl As List(Of String) = New List(Of String)
            For Each columnIndex As Integer In alSubject.GetInputInstlHinbanColumnIndexes
                wk.Add(columnIndex)
            Next
            wk.Sort()
            For Each columnIndex As Integer In wk
                instl.Add(alSubject.InstlHinban(columnIndex) + ":" + alSubject.InstlHinbanKbn(columnIndex))
            Next

            Dim finalIndex As Integer = 0
            Dim baseInstlIndex As Integer = 0
            Dim trgInstlIndex As Integer

            Using frm41Final As New Frm41FinalSourceSelector(instl)
                frm41Final.ShowDialog()
                '「戻る」が選択された場合、何をしません
                If frm41Final.Result = 0 Then
                    Exit Sub
                End If

                trgInstlIndex = alSubject.InstlHinbanCount

                If frm41Final.Result = 1 Then
                    Dim hoyouVo As New THoyouBuhinEditVo
                    hoyouVo.HoyouEventCode = alSubject.shisakuEventCode
                    hoyouVo.HoyouBukaCode = ""
                    hoyouVo.HoyouTanto = alSubject.BlockNo
                    hoyouVo.HoyouTantoKaiteiNo = ""
                    hoyouVo.BuhinNoHyoujiJun = 0
                    hoyouVo.BuhinNoHyoujiJunZenkai = 0
                    hoyouVo.InstlHinbanHyoujiJun = alSubject.GetInputInstlHinbanColumnIndexes.Count + 1
                    hoyouVo.Level = 0
                    hoyouVo.ShukeiCode = "X"
                    hoyouVo.BuhinNo = "DUMMY"
                    hoyouVo.BuhinName = "DUMMY"
                    hoyouVo.InsuSuryo = "1"
                    hoyouVos.Add(hoyouVo)

                    Dim hoyouInstlVo As TShisakuBuhinEditInstlVo = New TShisakuBuhinEditInstlVo
                    hoyouInstlVo.ShisakuEventCode = alSubject.shisakuEventCode
                    hoyouInstlVo.ShisakuBukaCode = ""
                    hoyouInstlVo.ShisakuBlockNo = alSubject.BlockNo
                    hoyouInstlVo.ShisakuBlockNoKaiteiNo = ""
                    hoyouInstlVo.BuhinNoHyoujiJun = 0
                    hoyouInstlVo.InstlHinbanHyoujiJun = alSubject.GetInputInstlHinbanColumnIndexes.Count + 1
                    hoyouInstlVos.Add(hoyouInstlVo)


                    baseInstlIndex = trgInstlIndex
                End If

                If frm41Final.Result = 2 Then

                    finalIndex = frm41Final.cmbFinalSource.SelectedValue

                    Dim hyoujiNo As Integer = 0
                    For Each rowIndex As Integer In koseiSubject.GetInputRowIndexes
                        If koseiSubject.Matrix.InsuSuryo(rowIndex, finalIndex) <> 0 Then
                            Dim hoyouVo As New THoyouBuhinEditVo
                            Dim hoyouInstlVo As New TShisakuBuhinEditInstlVo
                            GetHoyouEditVo(hoyouVo, hoyouInstlVo, koseiSubject.Matrix, rowIndex, finalIndex, hyoujiNo)
                            hoyouVos.Add(hoyouVo)
                            hoyouInstlVos.Add(hoyouInstlVo)
                            hyoujiNo = hyoujiNo + 1
                        End If
                    Next
                    baseInstlIndex = finalIndex
                End If

            End Using

            alSubject.CheckFlg.Clear()
            Dim maxrow As Integer = 0
            For Each index In alSubject.GetInputRowIndexes
                If index > maxrow Then
                    maxrow = index
                End If
            Next
            For index = 0 To maxrow
                If Not alSubject.InsuSuryo(index, baseInstlIndex) Is Nothing Then
                    If alSubject.InsuSuryo(index, baseInstlIndex) = "1" Then
                        alSubject.CheckFlg.Add(True)
                        Continue For
                    End If
                End If
                alSubject.CheckFlg.Add(False)
            Next

            aMakeStructureResult = New MakeStructureResultImpl(shisakuEventCode, shisakuBukaCode, shisakuBlockNo)

            Using frm41 As New HoyouBuhinFrm41DispShisakuBuhinEdit00(Me, "", "", "", 0, 0, 0, hoyouVos, hoyouInstlVos, alSubject, koseiSubject, False)
                'Using frm41 As New HoyouBuhinFrm41DispShisakuBuhinEdit00(Me, shisakuEventCode, "", "", 0, 0, 0, hoyouVos, hoyouInstlVos, alSubject, koseiSubject)
                '自身のalsubjectをfrm41に渡して、Frm41DspShisakuBuhinEdit10の初期表示に設定する
                frm41.ShowDialog()

                'FINAL作成画面としてのFrm41DispShisakuBuhinEdit10.New（SpdAlObserver.New）にて、Observerが書き換えられているため、元に戻す。
                alSubject.DeleteObservers()
                alSubject.AddObserver(alObserver)

                If frm41.Register = True Then

                    For Each hoyouVo As HoyouBuhinBuhinKoseiRecordVo In frm41.HoyouKoseiMatrix.Records
                        ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD BEGIN
                        Dim koseiVo As New BuhinKoseiRecordVo
                        VoUtil.CopyProperties(hoyouVo, koseiVo)
                        ''↓↓2014/09/09 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD BEGIN
                        Dim insertindex As Integer = koseiSubject._koseiMatrix.Records.Count
                        If koseiVo.Level = 0 Then
                            For i As Integer = 0 To koseiSubject._koseiMatrix.Records.Count - 1
                                If Not koseiSubject._koseiMatrix.Record(i).Level = 0 Then
                                    insertindex = i
                                    Exit For
                                End If
                            Next
                        End If

                        '↓↓2014/10/21 酒井 ADD BEGIN
                        'HoyouAddから移植。ezSyncで値が変わってしまうため、差分のみ追加。
                        If koseiVo.Level = 0 Then
                            alSubject.InstlHinban(trgInstlIndex) = koseiVo.BuhinNo
                            alSubject.InstlHinbanKbn(trgInstlIndex) = koseiVo.BuhinNoKbn
                            'ezSyncでINSTLに対するデフォルト部品が作成されてしまうため、削除。
                            koseiSubject.RemoveRow(insertindex, 1)
                        End If
                        If hoyouVo.InsuSuryo = "**" Then
                            koseiSubject.AddKoseiRow(koseiVo, trgInstlIndex, -1, insertindex)
                        Else
                            koseiSubject.AddKoseiRow(koseiVo, trgInstlIndex, hoyouVo.InsuSuryo, insertindex)
                        End If
                    Next

                    '元のkoseimatrixの内、該当INSTLレコードをfrm41.HoyouKoseiMatrixで置き換えたものをnewMatrixに入れる	
                    Dim newMatrix As BuhinKoseiMatrix = koseiSubject.copy
                    alSubject.HoyouAdd(newMatrix)
                    alSubject.NotifyObservers()
                    '号車適用画面表示を取得し、試作AL作成画面に員数を設定する
                    For rowindex As Integer = 0 To alSubject.CheckFlg.Count - 1
                        If alSubject.CheckFlg(rowindex) = True Then
                            spdParts_Sheet1.Cells(rowindex + alObserver.titleRows, alObserver.ConvAlInstlToSpreadColumn(trgInstlIndex)).Value = 1
                        End If
                    Next

                End If
            End Using
        End Sub

        Private Sub 更新ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 更新ToolStripMenuItem.Click

            JikyuFlg = koseiSubject.JikyuFlg

            Dim hoyouVos As List(Of THoyouBuhinEditVo) = New List(Of THoyouBuhinEditVo)
            Dim hoyouInstlVos As List(Of TShisakuBuhinEditInstlVo) = New List(Of TShisakuBuhinEditInstlVo)

            Dim finalIndex As Integer = 0
            Dim baseInstlIndex As Integer = 0
            Dim trgInstlIndex As Integer
            Dim backActColIdx As Integer

            Dim sheet As Spread.SheetView = spdParts.ActiveSheet
            Dim alInstlHiban As String

            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(shisakuEventCode)

            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス
            backActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            'アクティブセルがINSTL品番列でなかった場合
            If alObserver.IsInstlSpreadColumn(ParaActColIdx) = False Then
                EBom.Common.ComFunc.ShowErrMsgBox("INSTL品番が選択されていません。")
                Exit Sub
            ElseIf eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" And alSubject.BaseInstlFlg(alObserver.ConvSpreadColumnToAlInstl(ParaActColIdx)) = "1" Then
                EBom.Common.ComFunc.ShowErrMsgBox("ベース情報は更新できません。")
                Exit Sub
            Else
                If String.IsNullOrEmpty(sheet.Cells(SpdAlObserver.INSTL_HINBAN_ROW_INDEX, ParaActColIdx).Value) Then
                    EBom.Common.ComFunc.ShowErrMsgBox("INSTL品番が選択されていません。")
                    Exit Sub
                Else
                    alInstlHiban = alSubject.InstlHinban(alObserver.ConvSpreadColumnToAlInstl(ParaActColIdx)) + ":" + alSubject.InstlHinbanKbn(alObserver.ConvSpreadColumnToAlInstl(ParaActColIdx))
                End If
            End If

            'アクティブセルを含む列のINSTL品番を取得
            For Each columnIndex As Integer In koseiSubject.GetInputInstlHinbanColumnIndexes
                Dim instlData As String = koseiSubject.InstlHinban(columnIndex) + ":" + koseiSubject.InstlHinbanKbn(columnIndex)
                If StringUtil.Equals(instlData, alInstlHiban) Then
                    finalIndex = columnIndex
                End If
            Next

            trgInstlIndex = alSubject.InstlHinbanCount

            Dim hyoujiNo As Integer = 0
            For Each rowIndex As Integer In koseiSubject.GetInputRowIndexes
                If koseiSubject.Matrix.InsuSuryo(rowIndex, finalIndex) <> 0 Then
                    Dim hoyouVo As New THoyouBuhinEditVo
                    Dim hoyouInstlVo As New TShisakuBuhinEditInstlVo
                    GetHoyouEditVo(hoyouVo, hoyouInstlVo, koseiSubject.Matrix, rowIndex, finalIndex, hyoujiNo)
                    hoyouVos.Add(hoyouVo)
                    hoyouInstlVos.Add(hoyouInstlVo)
                    hyoujiNo = hyoujiNo + 1
                End If
            Next

            baseInstlIndex = finalIndex

            alSubject.CheckFlg.Clear()
            Dim maxrow As Integer = 0
            For Each index In alSubject.GetInputRowIndexes
                If index > maxrow Then
                    maxrow = index
                End If
            Next
            For index = 0 To maxrow
                If Not alSubject.InsuSuryo(index, baseInstlIndex) Is Nothing Then
                    If alSubject.InsuSuryo(index, baseInstlIndex) = "1" Then
                        alSubject.CheckFlg.Add(True)
                        Continue For
                    End If
                End If
                alSubject.CheckFlg.Add(False)
            Next

            aMakeStructureResult = New MakeStructureResultImpl(shisakuEventCode, shisakuBukaCode, shisakuBlockNo)
            Using frm41 As New HoyouBuhinFrm41DispShisakuBuhinEdit00(Me, "", "", "", 0, 0, 0, hoyouVos, hoyouInstlVos, alSubject, koseiSubject, True)
                'Using frm41 As New HoyouBuhinFrm41DispShisakuBuhinEdit00(Me, shisakuEventCode, "", "", 0, 0, 0, hoyouVos, hoyouInstlVos, alSubject, koseiSubject)
                '自身のalsubjectをfrm41に渡して、Frm41DspShisakuBuhinEdit10の初期表示に設定する
                frm41.ShowDialog()

                alSubject.DeleteObservers()
                alSubject.AddObserver(alObserver)

                If frm41.Register = True Then
                    For Each hoyouVo As HoyouBuhinBuhinKoseiRecordVo In frm41.HoyouKoseiMatrix.Records
                        Dim koseiVo As New BuhinKoseiRecordVo
                        VoUtil.CopyProperties(hoyouVo, koseiVo)
                        Dim insertindex As Integer = koseiSubject._koseiMatrix.Records.Count
                        If koseiVo.Level = 0 Then
                            For i As Integer = 0 To koseiSubject._koseiMatrix.Records.Count - 1
                                If Not koseiSubject._koseiMatrix.Record(i).Level = 0 Then
                                    insertindex = i
                                    Exit For
                                End If
                            Next
                        End If
                        If koseiVo.Level = 0 Then
                            alSubject.InstlHinban(trgInstlIndex) = koseiVo.BuhinNo
                            alSubject.InstlHinbanKbn(trgInstlIndex) = koseiVo.BuhinNoKbn
                            koseiSubject.RemoveRow(insertindex, 1)
                        End If
                        If hoyouVo.InsuSuryo = "**" Then
                            koseiSubject.AddKoseiRow(koseiVo, trgInstlIndex, -1, insertindex)
                        Else
                            koseiSubject.AddKoseiRow(koseiVo, trgInstlIndex, hoyouVo.InsuSuryo, insertindex)
                        End If
                    Next
                    '×元のkoseimatrixの内、該当INSTLレコードをfrm41.HoyouKoseiMatrixで置き換えたものをnewMatrixに入れる	
                    '○一旦、「更新データ」が「末尾に追加された状態」のnewMatrixを使う。→※
                    Dim newMatrix As BuhinKoseiMatrix = koseiSubject.copy
                    alSubject.HoyouAdd(newMatrix)
                    alSubject.NotifyObservers()
                    '号車適用画面表示を取得し、試作AL作成画面に員数を設定する
                    For rowindex As Integer = 0 To alSubject.CheckFlg.Count - 1
                        If alSubject.CheckFlg(rowindex) = True Then
                            spdParts_Sheet1.Cells(rowindex + alObserver.titleRows, alObserver.ConvAlInstlToSpreadColumn(trgInstlIndex)).Value = 1
                        End If
                    Next
                    Dim backRowIndex As Integer
                    Dim trgRowIndex As Integer
                    For rowindex As Integer = 0 To koseiSubject._koseiMatrix.Records.Count - 1
                        If Not koseiSubject._koseiMatrix.Record(rowindex).Level = 0 Then
                            koseiSubject.InsuSuryo(rowindex, alObserver.ConvSpreadColumnToAlInstl(backActColIdx)) = koseiSubject.InsuSuryo(rowindex, trgInstlIndex)
                        Else
                            If koseiSubject.InsuSuryo(rowindex, alObserver.ConvSpreadColumnToAlInstl(backActColIdx)) <> 0 Then
                                '更新元INSTLのlevel=0の部品
                                backRowIndex = rowindex
                            ElseIf koseiSubject.InsuSuryo(rowindex, trgInstlIndex) <> 0 Then
                                '「更新レコード（追加状態）」INSTLのlevel=0の部品
                                trgRowIndex = rowindex
                            End If
                        End If
                    Next
                    VoUtil.CopyProperties(koseiSubject._koseiMatrix.Record(trgRowIndex), koseiSubject._koseiMatrix.Record(backRowIndex))
                    koseiSubject.NotifyObservers()
                    '※AL画面の「末尾に追加された状態」の「更新レコード」の員数を、元レコードにコピーして、末尾を削除する（UPDATEに見せる）。
                    For rowindex As Integer = 0 To alSubject.CheckFlg.Count - 1
                        spdParts_Sheet1.Cells(rowindex + alObserver.titleRows, backActColIdx).Value = spdParts_Sheet1.Cells(rowindex + alObserver.titleRows, alObserver.ConvAlInstlToSpreadColumn(trgInstlIndex)).Value
                    Next
                    alObserver.RemoveColumns(alObserver.ConvAlInstlToSpreadColumn(trgInstlIndex), 1, Me.shisakuEventCode)
                End If
            End Using
        End Sub

        Private Sub GetHoyouEditVo(ByRef hoyouVo As THoyouBuhinEditVo, ByRef hoyouInstlVo As TShisakuBuhinEditInstlVo, _
                                   ByVal Matrix As BuhinKoseiMatrix, ByVal rowIndex As Integer, ByVal columnIndex As Integer, _
                                   ByVal hyoujiNo As Integer)

            'イベントコード
            hoyouVo.HoyouEventCode = alSubject.shisakuEventCode
            '部課コード
            hoyouVo.HoyouBukaCode = Matrix.Record(rowIndex).ShisakuBukaCode
            'ブロックNo
            hoyouVo.HoyouTanto = Matrix.Record(rowIndex).ShisakuBlockNo
            'ブロックNo改訂No
            hoyouVo.HoyouTantoKaiteiNo = Matrix.Record(rowIndex).ShisakuBlockNoKaiteiNo
            '部品番号表示順
            hoyouVo.BuhinNoHyoujiJun = hyoujiNo
            '部品番号表示順前回順
            hoyouVo.BuhinNoHyoujiJunZenkai = Matrix.Record(rowIndex).BuhinNoHyoujiJun
            'レベル
            hoyouVo.Level = Matrix.Record(rowIndex).Level
            '国内集計コード
            hoyouVo.ShukeiCode = Matrix.Record(rowIndex).ShukeiCode
            '部品番号
            hoyouVo.BuhinNo = Matrix.Record(rowIndex).BuhinNo
            '部品名称
            hoyouVo.BuhinName = Matrix.Record(rowIndex).BuhinName
            '海外SIA集計コード  
            hoyouVo.SiaShukeiCode = Matrix.Record(rowIndex).SiaShukeiCode
            '現調CKD区分  
            hoyouVo.GencyoCkdKbn = Matrix.Record(rowIndex).GencyoCkdKbn
            '取引先コード  
            hoyouVo.MakerCode = Matrix.Record(rowIndex).MakerCode
            '取引先名称  
            hoyouVo.MakerName = Matrix.Record(rowIndex).MakerName
            '員数
            hoyouVo.InsuSuryo = Matrix.InsuSuryo(rowIndex, columnIndex)
            '供給セクション 
            hoyouVo.KyoukuSection = Matrix.Record(rowIndex).KyoukuSection
            '部品ノート
            hoyouVo.BuhinNote = Matrix.Record(rowIndex).BuhinNote
            '備考  
            hoyouVo.Bikou = Matrix.Record(rowIndex).Bikou
            '編集登録日  
            hoyouVo.EditTourokubi = Matrix.Record(rowIndex).EditTourokubi
            '編集登録時間  
            hoyouVo.EditTourokujikan = Matrix.Record(rowIndex).EditTourokujikan
            '改訂判断フラグ  
            hoyouVo.KaiteiHandanFlg = Matrix.Record(rowIndex).KaiteiHandanFlg
            '補用リストコード  
            hoyouVo.HoyouListCode = Matrix.Record(rowIndex).ShisakuListCode
            '作成ユーザーID
            hoyouVo.CreatedUserId = Matrix.Record(rowIndex).CreatedUserId
            '作成日
            hoyouVo.CreatedDate = Matrix.Record(rowIndex).CreatedDate
            '作成時
            hoyouVo.CreatedTime = Matrix.Record(rowIndex).CreatedTime
            '更新ユーザーID
            hoyouVo.UpdatedUserId = Matrix.Record(rowIndex).UpdatedUserId
            '更新日
            hoyouVo.UpdatedDate = Matrix.Record(rowIndex).UpdatedDate
            '更新時間
            hoyouVo.UpdatedTime = Matrix.Record(rowIndex).UpdatedTime
            '部品番号試作区分  
            hoyouVo.BuhinNoKbn = Matrix.Record(rowIndex).BuhinNoKbn
            '部品番号改訂No.  
            hoyouVo.BuhinNoKaiteiNo = Matrix.Record(rowIndex).BuhinNoKaiteiNo
            '枝番  
            hoyouVo.EdaBan = Matrix.Record(rowIndex).EdaBan
            '出図予定日  
            hoyouVo.ShutuzuYoteiDate = Matrix.Record(rowIndex).ShutuzuYoteiDate
            '材質・規格１  
            hoyouVo.ZaishituKikaku1 = Matrix.Record(rowIndex).ZaishituKikaku1
            '材質・規格２  
            hoyouVo.ZaishituKikaku2 = Matrix.Record(rowIndex).ZaishituKikaku2
            '材質・規格３  
            hoyouVo.ZaishituKikaku3 = Matrix.Record(rowIndex).ZaishituKikaku3
            '材質・メッキ  
            hoyouVo.ZaishituMekki = Matrix.Record(rowIndex).ZaishituMekki
            '板厚・板厚       
            hoyouVo.ShisakuBankoSuryo = Matrix.Record(rowIndex).ShisakuBankoSuryo
            '板厚・ｕ
            hoyouVo.ShisakuBankoSuryoU = Matrix.Record(rowIndex).ShisakuBankoSuryoU

            ''↓↓2014/12/26 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            '材料情報・製品長
            hoyouVo.MaterialInfoLength = Matrix.Record(rowIndex).MaterialInfoLength
            '' 材料情報・製品幅
            hoyouVo.MaterialInfoWidth = Matrix.Record(rowIndex).MaterialInfoWidth
            '' データ項目・改訂№
            hoyouVo.DataItemKaiteiNo = Matrix.Record(rowIndex).DataItemKaiteiNo
            '' データ項目・エリア名
            hoyouVo.DataItemAreaName = Matrix.Record(rowIndex).DataItemAreaName
            '' データ項目・セット名
            hoyouVo.DataItemSetName = Matrix.Record(rowIndex).DataItemSetName
            '' データ項目・改訂情報
            hoyouVo.DataItemKaiteiInfo = Matrix.Record(rowIndex).DataItemKaiteiInfo
            ''↑↑2014/12/24 メタル対応追加フィールド (DANIEL)柳沼 ADD END



            '試作部品費（円）  
            hoyouVo.ShisakuBuhinHi = Matrix.Record(rowIndex).ShisakuBuhinHi
            '試作型費（千円）  
            hoyouVo.ShisakuKataHi = Matrix.Record(rowIndex).ShisakuKataHi
            ' 作り方・製作方法
            hoyouVo.TsukurikataSeisaku = Matrix.Record(rowIndex).TsukurikataSeisaku
            ' 作り方・型仕様1
            hoyouVo.TsukurikataKatashiyou1 = Matrix.Record(rowIndex).TsukurikataKatashiyou1
            ' 作り方・型仕様2
            hoyouVo.TsukurikataKatashiyou2 = Matrix.Record(rowIndex).TsukurikataKatashiyou2
            ' 作り方・型仕様3
            hoyouVo.TsukurikataKatashiyou3 = Matrix.Record(rowIndex).TsukurikataKatashiyou3
            ' 作り方・治具
            hoyouVo.TsukurikataTigu = Matrix.Record(rowIndex).TsukurikataTigu
            ' 作り方・納入見通し
            hoyouVo.TsukurikataNounyu = Matrix.Record(rowIndex).TsukurikataNounyu
            ' 作り方・部品製作規模・概要
            hoyouVo.TsukurikataKibo = Matrix.Record(rowIndex).TsukurikataKibo
            ' 再使用不可  
            hoyouVo.Saishiyoufuka = Matrix.Record(rowIndex).Saishiyoufuka
            ' ベース情報フラグ
            ''↓↓2014/09/05 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
            'hoyouVo.BaseBuhinFlg = Matrix.Record(rowIndex).BaseBuhinFlg
            hoyouVo.BaseBuhinFlg = "0"
            ''↑↑2014/09/05 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
            'INSTL品番表示順
            hoyouVo.InstlHinbanHyoujiJun = Matrix.InsuVo(rowIndex, columnIndex).InstlHinbanHyoujiJun

            'イベントコード
            hoyouInstlVo.ShisakuEventCode = alSubject.shisakuEventCode
            '部課コード
            hoyouInstlVo.ShisakuBukaCode = Matrix.Record(rowIndex).ShisakuBukaCode
            'ブロックNo
            hoyouInstlVo.ShisakuBlockNo = Matrix.Record(rowIndex).ShisakuBlockNo
            'ブロックNo改訂No
            hoyouInstlVo.ShisakuBlockNoKaiteiNo = Matrix.Record(rowIndex).ShisakuBlockNoKaiteiNo
            '部品番号表示順
            hoyouInstlVo.BuhinNoHyoujiJun = hyoujiNo
            'INSTL品番表示順
            hoyouInstlVo.InstlHinbanHyoujiJun = Matrix.InsuVo(rowIndex, columnIndex).InstlHinbanHyoujiJun
            '員数
            hoyouInstlVo.InsuSuryo = Matrix.InsuSuryo(rowIndex, columnIndex)
            '作成日
            hoyouInstlVo.CreatedDate = Matrix.Record(rowIndex).CreatedDate
            '作成時
            hoyouInstlVo.CreatedTime = Matrix.Record(rowIndex).CreatedTime
            '作成ユーザーID
            hoyouInstlVo.CreatedUserId = Matrix.Record(rowIndex).CreatedUserId
            '最終更新日

            ''↓↓2014/09/05 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
            If Not Matrix.Record(rowIndex).UpdatedDate Is Nothing Then
                ''↑↑2014/09/05 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
                hoyouInstlVo.SaisyuKoushinbi = Integer.Parse(CStr(Matrix.Record(rowIndex).UpdatedDate).Replace("-", ""))
                ''↓↓2014/09/05 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
            End If
            ''↑↑2014/09/05 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END

            '更新日
            hoyouInstlVo.UpdatedDate = Matrix.Record(rowIndex).UpdatedDate
            '更新時間
            hoyouInstlVo.UpdatedTime = Matrix.Record(rowIndex).UpdatedTime
            '更新ユーザーID
            hoyouInstlVo.UpdatedUserId = Matrix.Record(rowIndex).UpdatedUserId

        End Sub

        Private Sub GetShisakuEditVo(ByRef shisakuVo As TShisakuBuhinEditVo, ByRef shisakuInstlVo As TShisakuBuhinEditInstlVo, ByVal hoyouVo As HoyouBuhinBuhinKoseiRecordVo)
            '' 試作イベントコード  
            shisakuVo.ShisakuEventCode = hoyouVo.HoyouEventCode
            '' 試作部課コード  
            shisakuVo.ShisakuBukaCode = hoyouVo.HoyouBukaCode
            '' 試作ブロック№  
            shisakuVo.ShisakuBlockNo = hoyouVo.HoyouTanto
            '' 試作ブロック№改訂№  
            shisakuVo.ShisakuBlockNoKaiteiNo = hoyouVo.HoyouTantoKaiteiNo
            '' 部品番号表示順  
            shisakuVo.BuhinNoHyoujiJun = hoyouVo.BuhinNoHyoujiJun
            '' レベル  
            shisakuVo.Level = hoyouVo.Level
            '' 国内集計コード  
            shisakuVo.ShukeiCode = hoyouVo.ShukeiCode
            '' 海外SIA集計コード  
            shisakuVo.SiaShukeiCode = hoyouVo.SiaShukeiCode
            '' 現調CKD区分  
            shisakuVo.GencyoCkdKbn = hoyouVo.GencyoCkdKbn
            '' 供給セクション
            shisakuVo.KyoukuSection = hoyouVo.KyoukuSection
            '' 取引先コード  
            shisakuVo.MakerCode = hoyouVo.MakerCode
            '' 取引先名称  
            shisakuVo.MakerName = hoyouVo.MakerName
            '' 部品番号  
            shisakuVo.BuhinNo = hoyouVo.BuhinNo
            '' 部品番号試作区分  
            shisakuVo.BuhinNoKbn = hoyouVo.BuhinNoKbn
            '' 部品番号改訂No.  
            shisakuVo.BuhinNoKaiteiNo = hoyouVo.BuhinNoKaiteiNo
            '' 枝番  
            shisakuVo.EdaBan = hoyouVo.EdaBan
            '' 部品名称  
            shisakuVo.BuhinName = hoyouVo.BuhinName
            '' 出図予定日  
            shisakuVo.ShutuzuYoteiDate = hoyouVo.ShutuzuYoteiDate
            '' 材質・規格１  
            shisakuVo.ZaishituKikaku1 = hoyouVo.ZaishituKikaku1
            '' 材質・規格２  
            shisakuVo.ZaishituKikaku2 = hoyouVo.ZaishituKikaku2
            '' 材質・規格３  
            shisakuVo.ZaishituKikaku3 = hoyouVo.ZaishituKikaku3
            '' 材質・メッキ  
            shisakuVo.ZaishituMekki = hoyouVo.ZaishituMekki
            '' 板厚・板厚       
            shisakuVo.ShisakuBankoSuryo = hoyouVo.ShisakuBankoSuryo
            '' 板厚・ｕ
            shisakuVo.ShisakuBankoSuryoU = hoyouVo.ShisakuBankoSuryoU


            ''↓↓2014/12/26 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
            '材料情報・製品長
            shisakuVo.MaterialInfoLength = hoyouVo.MaterialInfoLength
            '' 材料情報・製品幅
            shisakuVo.MaterialInfoWidth = hoyouVo.MaterialInfoWidth
            '' データ項目・改訂№
            shisakuVo.DataItemKaiteiNo = hoyouVo.DataItemKaiteiNo
            '' データ項目・エリア名
            shisakuVo.DataItemAreaName = hoyouVo.DataItemAreaName
            '' データ項目・セット名
            shisakuVo.DataItemSetName = hoyouVo.DataItemSetName
            '' データ項目・改訂情報
            shisakuVo.DataItemKaiteiInfo = hoyouVo.DataItemKaiteiInfo
            ''↑↑2014/12/26 メタル対応追加フィールド (DANIEL)柳沼 ADD END



            ' 再使用不可  
            shisakuVo.Saishiyoufuka = hoyouVo.Saishiyoufuka
            ' 作り方・製作方法
            shisakuVo.TsukurikataSeisaku = hoyouVo.TsukurikataSeisaku
            ' 作り方・型仕様1
            shisakuVo.TsukurikataKatashiyou1 = hoyouVo.TsukurikataKatashiyou1
            ' 作り方・型仕様2
            shisakuVo.TsukurikataKatashiyou2 = hoyouVo.TsukurikataKatashiyou2
            ' 作り方・型仕様3
            shisakuVo.TsukurikataKatashiyou3 = hoyouVo.TsukurikataKatashiyou3
            ' 作り方・治具
            shisakuVo.TsukurikataTigu = hoyouVo.TsukurikataTigu
            ' 作り方・納入見通し
            shisakuVo.TsukurikataNounyu = hoyouVo.TsukurikataNounyu
            ' 作り方・部品製作規模・概要
            shisakuVo.TsukurikataKibo = LeftB(hoyouVo.TsukurikataKibo, 200)
            ' ベース情報フラグ
            shisakuVo.BaseBuhinFlg = hoyouVo.BaseBuhinFlg
            '' 試作部品費（円）  
            shisakuVo.ShisakuBuhinHi = hoyouVo.ShisakuBuhinHi
            '' 試作型費（千円）  
            shisakuVo.ShisakuKataHi = hoyouVo.ShisakuKataHi
            '' 備考  
            shisakuVo.Bikou = hoyouVo.Bikou
            '' 編集登録日  
            shisakuVo.EditTourokubi = hoyouVo.EditTourokubi
            '' 編集登録時間  
            shisakuVo.EditTourokujikan = hoyouVo.EditTourokujikan
            '' 改訂判断フラグ  
            shisakuVo.KaiteiHandanFlg = hoyouVo.KaiteiHandanFlg
            '' 試作リストコード  
            shisakuVo.ShisakuListCode = hoyouVo.HoyouListCode
            '' 作成ユーザーID
            If String.IsNullOrEmpty(hoyouVo.CreatedUserId) Then
                shisakuVo.CreatedUserId = LoginInfo.Now.UserId
            Else
                shisakuVo.CreatedUserId = hoyouVo.CreatedUserId
            End If
            '' 作成日
            If String.IsNullOrEmpty(hoyouVo.CreatedDate) Then
                shisakuVo.CreatedDate = New ShisakuDate().CurrentDateDbFormat
            Else
                shisakuVo.CreatedDate = hoyouVo.CreatedDate
            End If
            '' 作成時
            If String.IsNullOrEmpty(hoyouVo.CreatedTime) Then
                shisakuVo.CreatedTime = New ShisakuDate().CurrentTimeDbFormat
            Else
                shisakuVo.CreatedTime = hoyouVo.CreatedTime
            End If
            '' 更新ユーザーID
            If String.IsNullOrEmpty(hoyouVo.UpdatedUserId) Then
                shisakuVo.UpdatedUserId = LoginInfo.Now.UserId
            Else
                shisakuVo.UpdatedUserId = hoyouVo.UpdatedUserId
            End If
            '' 更新日
            If String.IsNullOrEmpty(hoyouVo.UpdatedDate) Then
                shisakuVo.UpdatedDate = New ShisakuDate().CurrentDateDbFormat
            Else
                shisakuVo.UpdatedDate = hoyouVo.UpdatedDate
            End If
            '' 更新時間
            If String.IsNullOrEmpty(hoyouVo.UpdatedTime) Then
                shisakuVo.UpdatedTime = New ShisakuDate().CurrentTimeDbFormat
            Else
                shisakuVo.UpdatedTime = hoyouVo.UpdatedTime
            End If
            '' 部品ノート
            shisakuVo.BuhinNote = hoyouVo.BuhinNote

            'イベントコード
            shisakuInstlVo.ShisakuEventCode = hoyouVo.HoyouEventCode
            '部課コード
            shisakuInstlVo.ShisakuBukaCode = hoyouVo.HoyouBukaCode
            'ブロックNo
            shisakuInstlVo.ShisakuBlockNo = hoyouVo.HoyouTanto
            'ブロックNo改訂No
            shisakuInstlVo.ShisakuBlockNoKaiteiNo = hoyouVo.HoyouTantoKaiteiNo
            '部品番号表示順
            shisakuInstlVo.BuhinNoHyoujiJun = hoyouVo.BuhinNoHyoujiJun
            'INSTL品番表示順
            shisakuInstlVo.InstlHinbanHyoujiJun = hoyouVo.InstlHinbanHyoujiJun
            '員数
            shisakuInstlVo.InsuSuryo = hoyouVo.InsuSuryo
            '' 作成ユーザーID
            If String.IsNullOrEmpty(hoyouVo.CreatedUserId) Then
                shisakuInstlVo.CreatedUserId = LoginInfo.Now.UserId
            Else
                shisakuInstlVo.CreatedUserId = hoyouVo.CreatedUserId
            End If
            '' 作成日
            If String.IsNullOrEmpty(hoyouVo.CreatedDate) Then
                shisakuInstlVo.CreatedDate = New ShisakuDate().CurrentDateDbFormat
            Else
                shisakuInstlVo.CreatedDate = hoyouVo.CreatedDate
            End If
            '' 作成時
            If String.IsNullOrEmpty(hoyouVo.CreatedTime) Then
                shisakuInstlVo.CreatedTime = New ShisakuDate().CurrentTimeDbFormat
            Else
                shisakuInstlVo.CreatedTime = hoyouVo.CreatedTime
            End If
            '' 更新ユーザーID
            If String.IsNullOrEmpty(hoyouVo.UpdatedUserId) Then
                shisakuInstlVo.UpdatedUserId = LoginInfo.Now.UserId
            Else
                shisakuInstlVo.UpdatedUserId = hoyouVo.UpdatedUserId
            End If
            '' 更新日
            If String.IsNullOrEmpty(hoyouVo.UpdatedDate) Then
                shisakuInstlVo.UpdatedDate = New ShisakuDate().CurrentDateDbFormat
            Else
                shisakuInstlVo.UpdatedDate = hoyouVo.UpdatedDate
            End If
            '' 更新時間
            If String.IsNullOrEmpty(hoyouVo.UpdatedTime) Then
                shisakuInstlVo.UpdatedTime = New ShisakuDate().CurrentTimeDbFormat
            Else
                shisakuInstlVo.UpdatedTime = hoyouVo.UpdatedTime
            End If
            '最終更新日
            If String.IsNullOrEmpty(hoyouVo.UpdatedDate) Then
                shisakuInstlVo.SaisyuKoushinbi = Integer.Parse(New ShisakuDate().CurrentDateDbFormat.Replace("-", ""))
            Else
                shisakuInstlVo.SaisyuKoushinbi = Integer.Parse(hoyouVo.UpdatedDate.Replace("-", ""))
            End If
        End Sub
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_bd) (TES)張 ADD END
        Public Shared Function MidB(ByVal value As String, ByVal start As Integer, ByVal length As Integer) As String
            'バイト数に応じた部分文字列を取得する
            Dim encod As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
            Dim bytes As Byte() = encod.GetBytes(value.PadRight(start + length))
            Return encod.GetString(bytes, start - 1, length)
        End Function

        Public Shared Function LeftB(ByVal value As String, ByVal length As Integer) As String
            'バイト数に応じた部分文字列を取得する
            Dim encod As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
            Dim bytes As Byte() = encod.GetBytes(value.PadRight(length))
            Const start As Integer = 0
            Return encod.GetString(bytes, start, length)
        End Function

        Public Shared Function RightB(ByVal value As String, ByVal length As Integer) As String
            'バイト数に応じた部分文字列を取得する
            Dim encod As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
            Dim bytes As Byte() = encod.GetBytes(value.PadLeft(length))
            Dim start As Integer

            start = UBound(bytes) + 1 - length 'バイト数

            Return encod.GetString(bytes, start, length)

        End Function


        Public Shared Function LenB(ByVal vstr As String) As Integer

            If vstr = "" Then
                Return 0
            Else
                Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(vstr)
            End If

        End Function


        ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能_b) (TES)施 ADD BEGIN
        Private Sub 改訂コピーToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 改訂コピーToolStripMenuItem.Click
            Frm41KaiteiKakunin.Close()
            Dim shisakuEventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim shisakuEventVo As TShisakuEventVo = shisakuEventDao.FindByPk(Me.shisakuEventCode)
            Dim shisakuSekkeiDao As IShisakuSekkeiBlockDao = New ShisakuSekkeiBlockDaoImpl
            Dim shisakuSekkeiBlockVo As TShisakuSekkeiBlockVo = shisakuSekkeiDao.GetShisakuSekkeiBlockBukaCode(Me.shisakuEventCode, Me.alSubject.BlockNo)
            Using frmKaitei As New Frm41KaiteiSourceSelector(shisakuEventVo.ShisakuKaihatsuFugo, Me.shisakuEventCode, shisakuEventVo.ShisakuEventName, Me.alSubject.BlockNo, shisakuSekkeiBlockVo.ShisakuBukaCode)

                If frmKaitei.Count <> 0 Then
                    frmKaitei.ShowDialog()
                    If frmKaitei.result = MsgBoxResult.Ok Then
                        alSubject.backup()
                        '全列削除を行う'																																																																					
                        alObserver.RemoveInstlColumnsAll(1)
                        '20140820 Sakai Add
                        koseiSubject.SupersedeMatrix(New BuhinKoseiMatrix)
                        koseiSubject.NotifyObservers()
                        alSubject.EventCopy(Me.shisakuEventCode, frmKaitei.selectedKaiteiNo)
                        alSubject.NotifyObservers()
                        '↓↓2014/09/24 酒井 ADD BEGIN
                        'Dim kakuninFrm As Frm41KaiteiKakunin = New Frm41KaiteiKakunin((Me.MdiParent))
                        'kakuninFrm.Show()
                        '↑↑2014/09/24 酒井 ADD END
                        Frm41KaiteiKakunin._frm = Me.MdiParent
                        Frm41KaiteiKakunin.Show()
                    End If

                Else
                    MsgBox("このブロックには改訂が存在しません。", MsgBoxStyle.OkOnly, "エラー")
                End If
            End Using


        End Sub
        ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能_b) (TES)施 ADD END
    End Class
End Namespace
