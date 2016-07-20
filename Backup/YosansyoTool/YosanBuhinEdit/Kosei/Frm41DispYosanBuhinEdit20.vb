Imports FarPoint.Win
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.Model
Imports YosansyoTool.YosanshoEdit
Imports YosansyoTool.YosanBuhinEdit.KouseiBuhin
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon.Ui.Valid
Imports YosansyoTool.YosanBuhinEdit.Kosei.Ui
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic
Imports YosansyoTool.YosanBuhinEdit.Ui
Imports EBom.Common
Imports YosansyoTool.YosanBuhinEdit.Kosei.Dao
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanBuhinEdit.Kosei
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm41DispYosanBuhinEdit20

        'SPREADのサイズ変更用に使用する。
        '１は標準サイズ
        Private Kakudai As Decimal = 1

        'コピー＆ペースト用
        Private w_RowCount As Integer = 0
        Private w_ColumnCount As Integer = 0
        'ペーストでセルを変更した際のチェンジイベント
        Dim WithEvents datamodel As FarPoint.Win.Spread.Model.DefaultSheetDataModel

        'ヘッダーの行高の標準サイズ。
        Private w_HEAD As Integer = 164

        Private _dispMode As Integer

        Private ToolTipRange As FarPoint.Win.Spread.Model.CellRange

        Private copyRowCount As Integer
        Private copyColumn As Integer

#Region "フォーム"
        Private Sub Frm41DispYosanBuhinEdit20_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            ' ［Ctrl］+［C］キーを無効とします
            Dim im As New FarPoint.Win.Spread.InputMap
            im = spdParts.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)
            ' ［Ctrl］+［X］キーを無効とします'
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.X, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)
        End Sub

        'フォームのFormClosingイベントハンドラ
        Private Sub Frm41DispYosanBuhinEdit20_FormClosing(ByVal sender As System.Object, _
                ByVal e As System.Windows.Forms.FormClosingEventArgs) _
                Handles MyBase.FormClosing
            e.Cancel = True
        End Sub
#End Region

#Region "ツールボタン操作"
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

        Private Sub コピーCToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles コピーCToolStripButton.Click
            'オプション・コピーサブルーチンへ
            OptionCopy()
        End Sub

        Private Sub 貼り付けPToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 貼り付けPToolStripButton.Click
            'オプション・ペーストサブルーチンへ
            OptionPaste()
        End Sub

        Private Sub 切り取りUToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            'オプション・カットサブルーチンへ
            OptionCut()
        End Sub

        Private Sub UNdo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UNdo.Click
            Try
                ' 元に戻すことが出来るか確認します。
                If spdParts.UndoManager.CanUndo = True Then
                    spdParts.UndoManager.Undo()
                End If
            Catch Exception As Exception
                MessageBox.Show("元に戻す事が出来ません。")
                Exit Sub
            End Try
        End Sub

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

        Private Sub Sequence1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sequence1.Click
            Try
                ' 現在選択しているセル範囲を取得します。
                Dim cr = spdParts.ActiveSheet.GetSelection(0)
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

        Private Sub sequence2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sequence2.Click
            Try
                ' 現在選択しているセル範囲を取得します。
                Dim cr = spdParts.ActiveSheet.GetSelection(0)
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

        Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
            Try
                ' 現在選択しているセル範囲を取得します。
                Dim cr = spdParts.ActiveSheet.GetSelection(0)
                '最終列(備考列の列)を計算
                Dim w_Count As Integer = spdParts_Sheet1.Columns("BIKOU").Index
                '選択しているアクティブセル列の非表示列を表示します。
                For i As Integer = 0 To w_Count
                    spdParts.ActiveSheet.Columns(i).Visible = True
                Next
            Catch Exception As Exception
                MessageBox.Show("選択されたセルがありません。")
                Exit Sub
            End Try
        End Sub

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
                koseiObserver.SetFiltering()

            Catch ex As Exception
                MsgBox(String.Format("フィルタリング設定処理でシステムエラーが発生しました。({0})", ex.Message), MsgBoxStyle.Critical)
            Finally
                Cursor = Cursors.Default
            End Try

        End Sub
#End Region

#Region "ツールボタン(フィルタ解除)"
        '''' <summary>
        '''' ツールボタン(フィルタ解除)
        '''' </summary>
        '''' <param name="sender"></param>
        '''' <param name="e"></param>
        '''' <remarks></remarks>
        'Private Sub FilterCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '    koseiObserver.ResetFilter(spdParts_Sheet1.ActiveColumn.Index, spdParts_Sheet1.ActiveColumn.Index2)

        'End Sub

        ''' <summary>
        ''' ツールボタン(フィルタ解除(全部))
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub FilterCancelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterCancelAll.Click
            Try

                Cursor = Cursors.WaitCursor

                '全列のフィルタリングを解除
                koseiObserver.ResetFilterAll()

            Catch ex As Exception
                MsgBox(String.Format("フィルタリング解除でシステムエラーが発生しました。({0})", ex.Message), MsgBoxStyle.Critical)
            Finally
                Cursor = Cursors.Default
            End Try
        End Sub
#End Region

#Region "ツールボタン(ソート)"
        ''' <summary>
        ''' ソートを行う
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Sort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sort.Click
            'ソート用の画面を開く'
            Dim fms As New frm41Sort()
            fms.ShowDialog()
            '状態を受け取る'
            Dim Conditions1 As String = fms.ComboBox1.Text
            Dim order1 As Boolean = fms.RadioButton1.Checked

            '状態を渡してソート処理'
            koseiSubject.IsViewerMode = True
            koseiSubject.SortMatrix(Conditions1, order1)
            koseiSubject.IsViewerMode = False
            koseiSubject.NotifyObservers()

        End Sub
#End Region

        Private Sub HEADDW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HEADDW.Click
            w_HEAD -= 4
            If w_HEAD < 100 Then
                MsgBox("これ以上縮小できません。", MsgBoxStyle.Information, "アラーム")
                w_HEAD = 100
            End If
            'ヘッダーの行高を縮小します。
            spdParts.ActiveSheet.Rows(2).Height = w_HEAD
        End Sub

        Private Sub HEADUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HEADUP.Click
            w_HEAD += 4
            If w_HEAD > 250 Then
                MsgBox("これ以上拡大できません。", MsgBoxStyle.Information, "アラーム")
                w_HEAD = 250
            End If
            'ヘッダーの行高を縮小します。
            spdParts.ActiveSheet.Rows(2).Height = w_HEAD
        End Sub

#End Region

#Region "マウス右クリックメニュー操作"
        Private Sub ContextMenuStrip1_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
            '閲覧モードの場合は常に「全て」無効
            If _dispMode = VIEW_MODE Then
                コピーToolStripMenuItem.Enabled = False
                貼り付けToolStripMenuItem.Enabled = False
                挿入ToolStripMenuItem.Enabled = False
                削除ToolStripMenuItem.Enabled = False
                挿入貼り付けToolStripMenuItem.Enabled = False
                列挿入ToolStripMenuItem.Enabled = False
                列削除ToolStripMenuItem.Enabled = False
            Else
                'フィルタリング中かチェックする。
                'フィルタリング中なら行挿入、行削除を非表示にする。
                Dim wFilter As String = koseiObserver.FilterCheck()

                コピーToolStripMenuItem.Enabled = True
                貼り付けToolStripMenuItem.Enabled = True
                挿入ToolStripMenuItem.Enabled = False
                削除ToolStripMenuItem.Enabled = False
                挿入貼り付けToolStripMenuItem.Enabled = False
                列挿入ToolStripMenuItem.Enabled = False
                列削除ToolStripMenuItem.Enabled = False

                If spdParts_Sheet1.SelectionCount <> 1 Then
                    Return
                End If

                Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)

                If SpreadUtil.IsSelectedRow(selection) AndAlso spdParts_Sheet1.Cells(selection.Row, 0).Value Is Nothing Then
                    If StringUtil.IsEmpty(wFilter) Then
                        挿入ToolStripMenuItem.Enabled = True
                        削除ToolStripMenuItem.Enabled = True

                        If copyColumn = -1 Then
                            挿入貼り付けToolStripMenuItem.Enabled = True
                        Else
                            挿入貼り付けToolStripMenuItem.Enabled = False
                        End If
                    End If
                    コピーToolStripMenuItem.Enabled = False
                    貼り付けToolStripMenuItem.Enabled = False
                    Return
                End If

                If SpreadUtil.IsSelectedRow(selection) AndAlso koseiObserver.IsDataRow(selection.Row) Then
                    If StringUtil.IsEmpty(wFilter) Then
                        挿入ToolStripMenuItem.Enabled = True
                        削除ToolStripMenuItem.Enabled = True
                        コピーToolStripMenuItem.Enabled = True
                        貼り付けToolStripMenuItem.Enabled = True
                        If copyColumn = -1 Then
                            挿入貼り付けToolStripMenuItem.Enabled = True
                        Else
                            挿入貼り付けToolStripMenuItem.Enabled = False
                        End If
                    End If
                End If

                If SpreadUtil.IsSelectedColumn(selection) Then
                    If koseiObserver.CanInsertColumnRemoveColumn(selection) Then
                        列挿入ToolStripMenuItem.Enabled = True
                        列削除ToolStripMenuItem.Enabled = True
                    End If
                End If

                'フィルタリング中だったら・・・
                If Not StringUtil.IsEmpty(wFilter) Then
                    挿入ToolStripMenuItem.Enabled = False
                    削除ToolStripMenuItem.Enabled = False
                    コピーToolStripMenuItem.Enabled = False
                    貼り付けToolStripMenuItem.Enabled = False
                    挿入貼り付けToolStripMenuItem.Enabled = False
                    列挿入ToolStripMenuItem.Enabled = False
                    列削除ToolStripMenuItem.Enabled = False
                End If

                'タイトル行だったら
                Dim sheet As Spread.SheetView = spdParts.ActiveSheet
                Dim titleRows As Integer = BuhinEditSpreadUtil.GetTitleRows(sheet)
                If selection.Row < titleRows Then
                    挿入ToolStripMenuItem.Enabled = False
                    削除ToolStripMenuItem.Enabled = False
                    コピーToolStripMenuItem.Enabled = False
                    貼り付けToolStripMenuItem.Enabled = False
                    挿入貼り付けToolStripMenuItem.Enabled = False
                End If

            End If

        End Sub

        Private Sub コピーToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles コピーToolStripMenuItem.Click
            'オプション・コピーサブルーチンへ
            OptionCopy()
        End Sub

        Private Sub 貼り付けToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 貼り付けToolStripMenuItem.Click
            'オプション・ペーストサブルーチンへ
            OptionPaste()

        End Sub

        Private Sub 切り取りToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            'オプション・カットサブルーチンへ
            OptionCut()
        End Sub

        Private Sub 挿入ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 挿入ToolStripMenuItem.Click
            If spdParts_Sheet1.SelectionCount <> 1 Then
                Return
            End If

            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)
            Dim i As Integer = 0
            Dim wRowCount As Integer = selection.RowCount

            If SpreadUtil.IsSelectedRow(selection) Then
                koseiObserver.InsertRows(selection.Row, wRowCount)
            End If
        End Sub

        Private Sub 削除ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 削除ToolStripMenuItem.Click
            If spdParts_Sheet1.SelectionCount <> 1 Then
                Return
            End If

            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)
            If SpreadUtil.IsSelectedRow(selection) Then
                koseiObserver.RemoveRows(selection.Row, selection.RowCount)

                '
                '   削除後、最終行が空でない場合、末尾に1行追加
                '
                '   最終行のタブ区切り文字を取得
                '   して、TABとEnterを削除した結果がNULLならば何も入力されていないとする
                '
                Dim RowMax As Integer = spdParts_Sheet1.RowCount - 1
                Dim s As String = spdParts_Sheet1.GetClipValue(RowMax, 0, 1, -1)
                s = Replace(s, vbTab, "")
                s = Replace(s, vbCrLf, "")

                If s <> "" Then
                    spdParts_Sheet1.RowCount += 1
                End If
            End If

        End Sub

        ''' <summary>
        ''' 挿入貼り付け
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 挿入貼り付けToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 挿入貼り付けToolStripMenuItem.Click

            '行挿入と貼り付け'
            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)
            Dim wRowCount As Integer = selection.RowCount

            If SpreadUtil.IsSelectedRow(selection) Then
                koseiObserver.InsertRows(selection.Row, copyRowCount)
            End If
            '挿入後、貼り付けを行う'
            OptionPaste()

        End Sub

        Private Sub 列挿入ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 列挿入ToolStripMenuItem.Click
            If spdParts_Sheet1.SelectionCount <> 1 Then
                Return
            End If

            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)

            If SpreadUtil.IsSelectedColumn(selection) Then
                koseiObserver.InsertColumns(selection.Column, selection.ColumnCount)
            End If
        End Sub

        Private Sub 列削除ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 列削除ToolStripMenuItem.Click
            If spdParts_Sheet1.SelectionCount <> 1 Then
                Return
            End If
            '員数列が１列ならメッセージを表示して終了する。
            Dim sheet As Spread.SheetView = spdParts.ActiveSheet
            Dim insuCount As Integer = sheet.Columns(SpdKoseiObserver.TAG_HENKO_GAIYO).Index - sheet.Columns(SpdKoseiObserver.TAG_KYOUKU_SECTION).Index - 1
            If StringUtil.Equals(insuCount, 1) Then
                ComFunc.ShowErrMsgBox("パターン列は最低１列は必要です。削除できません。")
                Return
            End If

            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)

            If SpreadUtil.IsSelectedColumn(selection) Then
                '１次対応として複数列削除を禁止する'
                If selection.ColumnCount <> 1 Then
                    MsgBox("複数列の削除は出来ませんです。" & vbCr & "お手数お掛けしますが、１列ずつ削除をお願いします。", MsgBoxStyle.OkOnly, "エラー")
                    Exit Sub
                End If
                koseiObserver.RemoveColumns(selection.Column, selection.ColumnCount)
            End If
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
        ''' 貼りつけ処理
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub OptionPaste()

            '
            '   アンドゥ処理のため、Sendkeyswo使用
            '
            '
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

            'selectionが空の場合があるので'
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
                    Dim objFont As System.Drawing.Font = aSheet.Cells(aSheet.ActiveRowIndex, aSheet.ActiveColumnIndex).Font

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
            Else
                colCnt = 1
                col = aSheet.ActiveColumnIndex
            End If

            '無い場合は全て保存対象編集書式とするため全てTrueをセット
            If alistBln Is Nothing Then
                alistBln = New List(Of Boolean())
                If Not selection Is Nothing Then
                    For i As Integer = 0 To selection.RowCount - 1
                        Dim blnTbl() As Boolean = Nothing
                        ReDim Preserve blnTbl(colCnt - 1)

                        For j As Integer = 0 To colCnt - 1
                            blnTbl(j) = True
                        Next
                        alistBln.Add(blnTbl)
                    Next
                Else
                    Dim blnTbl() As Boolean = Nothing
                    ReDim Preserve blnTbl(colCnt - 1)

                    For j As Integer = 0 To colCnt - 1
                        blnTbl(j) = True
                    Next
                    alistBln.Add(blnTbl)
                End If
            End If

            '受け取ったListの内容で書式を設定
            If Not selection Is Nothing Then
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

        Private errorController As New ErrorController()

        Public Property EControl() As ErrorController
            Get
                Return errorController
            End Get
            Set(ByVal value As ErrorController)
                errorController = value
            End Set
        End Property

        Private ReadOnly yosanEventCode As String
        Private ReadOnly yosanBukaCode As String
        Private ReadOnly yosanTanto As String
        Private koseiObserver As SpdKoseiObserver
        Private ReadOnly koseiSubject As BuhinEditKoseiSubject
        Private ReadOnly inputSupport As ShisakuInputSupport
        Private ReadOnly _patterNameList As List(Of String)
        Private _frmDispYosanBuhinEdit00 As Frm41DispYosanBuhinEdit00

        Public Sub New(ByVal patterNameList As List(Of String), ByVal yosanEventCode As String, ByVal yosanBukaCode As String, ByVal yosanTanto As String, _
                       ByVal koseiSubject As BuhinEditKoseiSubject, ByVal dispMode As Integer, ByVal frm41Disp00 As Frm41DispYosanBuhinEdit00)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)

            Me.StartPosition = FormStartPosition.Manual
            Me._patterNameList = patterNameList
            Me.yosanEventCode = yosanEventCode
            Me.yosanBukaCode = yosanBukaCode
            Me.yosanTanto = yosanTanto
            Me.koseiSubject = koseiSubject
            _frmDispYosanBuhinEdit00 = frm41Disp00

            _dispMode = dispMode

            koseiObserver = New SpdKoseiObserver(spdParts, koseiSubject)
            inputSupport = New ShisakuInputSupport(TxtInputSupport, spdParts)

            'ユニットプライスの優先先指定用
            ToolStripCmbKokunaiKaigai.Items.Add("国内優先")
            ToolStripCmbKokunaiKaigai.Items.Add("海外優先")

            InitializeSpread()

        End Sub

        Private Sub InitializeSpread()
            koseiObserver.Initialize()
            ShisakuSpreadUtil.AddEventCellRightClick(spdParts, inputSupport)

            'スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
            Dim imBase As New FarPoint.Win.Spread.InputMap
            imBase = Me.spdParts.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imBase.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

            Dim imGousya As New FarPoint.Win.Spread.InputMap
            imGousya = Me.spdParts.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imGousya.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

            'スプレッドに対しペーストを行い変更を行った際のイベント処理です
            datamodel = CType(Me.spdParts.ActiveSheet.Models.Data, FarPoint.Win.Spread.Model.DefaultSheetDataModel)
        End Sub

#Region "チェックについて"
        Public Sub AssertValidateRegister()
            koseiObserver.AssertValidateRegister()
        End Sub
        Public Sub AssertValidateRegisterWarning()
            koseiObserver.AssertValidateRegisterWarning()
        End Sub
        Public Sub AssertValidateSave()
            koseiObserver.AssertValidateSave()
        End Sub

        ''' <summary>
        ''' 供給セクションのチェック
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AssertValidateKyoukuSection()
            koseiObserver.AssertValidateKoseiKyoukuSection()
        End Sub
#End Region

        Public Sub ClearSheetBackColor()
            koseiObserver.ClearSheetBackColor()
        End Sub
        Public Sub ClearSheetBackColorAll()
            koseiObserver.ClearSheetBackColorAll()
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

        ''' <summary>
        ''' スプレッドの表示を制御する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub viewLockForViewMode()

            ''コントロールをロックする
            Me.ToolStrip1.Enabled = True
            Me.コピーCToolStripButton.Enabled = False
            Me.貼り付けPToolStripButton.Enabled = False

            Me.BtnBuhinSelect.Enabled = False
            Me.TxtInputSupport.Enabled = False

            For i As Integer = 0 To spdParts_Sheet1.RowCount - 1
                For j As Integer = 0 To spdParts_Sheet1.ColumnCount - 1
                    spdParts_Sheet1.Cells(i, j).Locked = True
                Next
            Next
        End Sub

#Region "スプレッド操作"
        Private Sub spdParts_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdParts.Change

            Dim sheet As Spread.SheetView = spdParts.ActiveSheet
            ' 選択セルの場所を特定します。
            Dim ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            Dim ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス


            ''2015/04/08 追加 E.Ubukata
            '' 同一インストール品番入力チェック
            If ParaActRowIdx = SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX Then
                Dim stCol As Integer = sheet.Columns(SpdKoseiObserver.TAG_KYOUKU_SECTION).Index + 1
                Dim edCol As Integer = sheet.Columns(SpdKoseiObserver.TAG_HENKO_GAIYO).Index - 1
                For col As Integer = stCol To edCol
                    If col <> ParaActColIdx Then
                        If sheet.Cells(ParaActRowIdx, col).Text = sheet.Cells(ParaActRowIdx, ParaActColIdx).Text Then
                            ComFunc.ShowErrMsgBox("同一のインストール品番が既に存在します。")
                            Exit Sub
                        End If
                    End If
                Next
            End If


            ' 該当セルの文字色、文字太を変更する。
            sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle())

            'koseiObserver.Change_EditInsuu(sheet.ActiveCell)

        End Sub

        Private Sub spdParts_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdParts.EditModeOn
            Dim sheet As Spread.SheetView = spdParts_Sheet1
            ' 選択セルの場所を特定します。
            Dim ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            Dim ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            With sheet.Cells(ParaActRowIdx, ParaActColIdx)
                .Tag = .Text
            End With

            If ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_HENKO_GAIYO).Index OrElse _
               ParaActColIdx = sheet.Columns(SpdKoseiObserver.TAG_MAKER_NAME).Index OrElse _
               StringUtil.Equals(SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, ParaActRowIdx) Then
                'IMEを使用可能にする。
                spdParts.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
                spdParts.ImeMode = Windows.Forms.ImeMode.NoControl
            Else
                'IMEを使用不可能にする。
                spdParts.EditingControl.ImeMode = Windows.Forms.ImeMode.Disable
                spdParts.ImeMode = Windows.Forms.ImeMode.Disable
            End If
        End Sub

        Private Sub spdParts_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdParts.EditModeOff
            spdParts.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
            spdParts.ImeMode = Windows.Forms.ImeMode.NoControl

            'koseiObserver.Change_EditInsuu(spdParts.ActiveSheet.ActiveCell)

        End Sub

        Private Sub spdParts_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles spdParts.MouseMove
            Dim range As FarPoint.Win.Spread.Model.CellRange = spdParts.GetCellFromPixel(0, 0, e.X, e.Y)
            Dim col As Spread.Column = spdParts_Sheet1.Columns(range.Column)

            '   カーソル位置を求めて、変更がある（位置が変わった）時だけ動くようにしてみた。
            If StringUtil.IsEmpty(ToolTipRange) OrElse _
                ToolTipRange.Column <> range.Column OrElse ToolTipRange.Row <> range.Row Then
                Dim tipText As String = ""
                Select Case col.Tag
                    Case "SHUKEI_CODE", "SIA_SHUKEI_CODE"
                        tipText = "集計コードの一覧" & vbCrLf & vbCrLf & _
                                "集計コード　：　説明" & vbCrLf & _
                                SHUKEI_X & "　：　" & SHUKEI_X_NAME & vbCrLf & _
                                SHUKEI_A & "　：　" & SHUKEI_A_NAME & vbCrLf & _
                                SHUKEI_E & "　：　" & SHUKEI_E_NAME & vbCrLf & _
                                SHUKEI_R & "　：　" & SHUKEI_R_NAME & vbCrLf & _
                                SHUKEI_Y & "　：　" & SHUKEI_Y_NAME & vbCrLf & _
                                SHUKEI_J & "　：　" & SHUKEI_J_NAME
                End Select
                Me.ToolTip1.SetToolTip(spdParts, tipText)
            End If
            ToolTipRange = range

        End Sub

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

                        ' 選択範囲を取得
                        Dim cr As FarPoint.Win.Spread.Model.CellRange() = spdParts.ActiveSheet.GetSelections()
                        If cr.Length > 0 Then
                            copyRowCount = selection.RowCount
                            copyColumn = selection.Column
                            Dim data As [String] = Nothing
                            If cr(0).Row = -1 Then
                                ' 列単位で選択されている場合
                                For i As Integer = 0 To spdParts.ActiveSheet.RowCount - 1
                                    If spdParts.ActiveSheet.GetRowVisible(i) = True Then
                                        data += spdParts.ActiveSheet.GetClipValue(i, cr(0).Column, 1, cr(0).ColumnCount)
                                    End If
                                Next
                            Else
                                Dim count As Integer = 0
                                ' セル単位か行単位で選択されている場合
                                For i As Integer = 0 To cr(0).RowCount - 1

                                    If spdParts.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then
                                        If cr(0).Column = -1 AndAlso cr(0).ColumnCount = -1 Then
                                            ''
                                            count = count + 1
                                            data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, 0, 1, spdParts.ActiveSheet.ColumnCount)
                                        Else
                                            count = count + 1
                                            If spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount) = "" Then
                                                data += "  " + vbCrLf  'ブランクの場合には改行をセットする。
                                            Else
                                                data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                            End If
                                        End If
                                    End If
                                Next
                            End If

                            ' クリップボードに設定します
                            Clipboard.SetData(DataFormats.Text, data)
                        Else
                            Dim data As [String] = Nothing
                            data += spdParts.ActiveSheet.GetClipValue(sheet.ActiveRowIndex, sheet.ActiveColumnIndex, 1, 1)
                            Clipboard.SetData(DataFormats.Text, data)
                        End If

                        '書式を戻す
                        SetUndoCellFormat(sheet, listBln)

                    End If

                Case Keys.X
                Case Keys.V
                    '情報列タイトルの色をチェック
                    For i As Integer = 0 To sheet.ColumnCount - 1
                        '青色か？
                        If sheet.Cells(0, i, 2, i).ForeColor = Color.Blue Then
                            Exit Sub
                        End If
                    Next
                    'コントロールキーとVキーが押された
                    If (e.Modifiers And Keys.Control) = Keys.Control Then

                        Dim listClip As New List(Of String())

                        listClip = GetClipbordList()

                        If Not listClip Is Nothing Then

                            Dim selColumnCount As Integer = 0
                            Dim selCol As Integer = 0

                            If Not selection Is Nothing Then
                                If selection.Column = -1 AndAlso selection.ColumnCount - 1 Then
                                    selColumnCount = spdParts.ActiveSheet.ColumnCount
                                Else
                                    selColumnCount = selection.ColumnCount
                                    selCol = selection.Column
                                End If
                            Else
                                selCol = sheet.ActiveColumnIndex
                                selColumnCount = 1
                            End If

                            Dim rowCount As Integer = listClip.Count - 1
                            Dim colCount As Integer = listClip(0).Length


                            Dim MaxColOrg As Integer = selection.Column + colCount

                            '
                            '   貼り付け列が現在の最大値を超える場合、貼り付けできないので、
                            '   列数を増やして、増えた分は非表示にする
                            '
                            If MaxColOrg > sheet.ColumnCount Then

                                MaxColOrg = sheet.ColumnCount

                                Dim CRLF_Code As String = vbCrLf

                                Dim s As String = ""
                                For r As Integer = 0 To rowCount

                                    Dim TabCode As String = ""

                                    For c As Integer = 0 To listClip(r).Length - 1
                                        If selection.Column + c >= sheet.ColumnCount Then
                                            Exit For
                                        End If

                                        s = s & TabCode & listClip(r)(c).Trim
                                        TabCode = vbTab
                                    Next
                                    If r = rowCount Then
                                        CRLF_Code = ""
                                    End If

                                    s = s & CRLF_Code

                                Next

                                s = s.TrimEnd(vbCrLf)
                                Clipboard.SetDataObject(s)

                            End If

                            'スプレッド自身に貼り付けさせる'
                            Dim im As New InputMap
                            spdParts.ClipboardOptions = ClipboardOptions.NoHeaders
                            im.Put(New Keystroke(Keys.V, Keys.Control), SpreadActions.ClipboardPasteValues)

                            'セル編集モード時にコピーした場合、以下を行う。
                            If rowCount = 0 Then
                                rowCount = 1
                            End If

                            '行選択時
                            If Not selection Is Nothing Then
                                If selection.Column = -1 Then

                                    '貼りつけ対象のセルを編集済みとし書式を設定する
                                    Me.spdParts_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                                    Me.spdParts_Sheet1.Rows(selection.Row, selection.Row + rowCount - 1).ForeColor = Color.Blue

                                Else
                                    If (selection.Row + rowCount) >= sheet.RowCount - 1 Then

                                        sheet.RowCount = selection.Row + rowCount

                                    End If

                                    '貼りつけ対象のセルを編集済みとし書式を設定する
                                    Me.spdParts_Sheet1.Cells(selection.Row, selCol, selection.Row + rowCount - 1, _
                                                           MaxColOrg - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                    Me.spdParts_Sheet1.Cells(selection.Row, selCol, selection.Row + rowCount - 1, _
                                                    MaxColOrg - 1).ForeColor = Color.Blue

                                End If
                            Else
                                '貼りつけ対象のセルを編集済みとし書式を設定する
                                Me.spdParts_Sheet1.Cells(sheet.ActiveRowIndex, selCol, sheet.ActiveRowIndex, _
                                                       sheet.ActiveColumnIndex).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                Me.spdParts_Sheet1.Cells(sheet.ActiveRowIndex, selCol, sheet.ActiveRowIndex, _
                                                       sheet.ActiveColumnIndex).ForeColor = Color.Blue
                            End If
                        End If
                    End If
                Case Keys.Delete
                    '行選択ではDeleteは無効に
                    If Not selection Is Nothing Then
                        If selection.Column = -1 AndAlso selection.ColumnCount - 1 Then
                            e.Handled = True
                        End If
                        'フィルタが掛かってるところは削除対象外'
                        Dim row As Integer = selection.Row
                        Dim col As Integer = selection.Column
                        Dim colcount As Integer = selection.ColumnCount
                        Dim rowcount As Integer = selection.RowCount


                        For rowindex As Integer = row To row + rowcount - 1
                            'フィルタ中の行は対象外'
                            If sheet.Rows(rowindex).Visible Then
                                ''レベル０行も対象外'
                                'タイトル行も対象外'
                                If row > 3 Then
                                    For colindex As Integer = col To col + colcount - 1
                                        sheet.ClearRange(rowindex, colindex, 1, 1, True)
                                        '   フィルタで非表示行の値がクリアされてしまうので
                                        '   無効にする。
                                        e.Handled = True
                                    Next
                                End If
                            End If
                        Next
                    Else
                        Dim row As Integer = sheet.ActiveRowIndex
                        Dim col As Integer = sheet.ActiveColumnIndex
                        Dim colcount As Integer = 1
                        Dim rowcount As Integer = 1

                        For rowindex As Integer = row To row + rowcount - 1
                            'フィルタ中の行は対象外'
                            If sheet.Rows(rowindex).Visible Then
                                ''レベル０行も対象外'
                                'InputFlgもクリアする。
                                'koseiSubject.InputFlg(rowindex - 4) = ""

                                'タイトル行も対象外'
                                If row > 3 Then
                                    For colindex As Integer = col To col + colcount - 1
                                        sheet.ClearRange(rowindex, colindex, 1, 1, True)
                                    Next
                                End If
                            End If
                        Next
                        e.Handled = True
                    End If

            End Select
        End Sub

#End Region

#End Region

#Region "部品検索ボタン"
        Private Sub BtnBuhinSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBuhinSelect.Click

            Dim sheet As Spread.SheetView = spdParts.ActiveSheet

            Dim stCol As Integer = sheet.Columns(SpdKoseiObserver.TAG_KYOUKU_SECTION).Index + 1
            Dim edCol As Integer = sheet.Columns(SpdKoseiObserver.TAG_HENKO_GAIYO).Index - 1
            If stCol = edCol Then
                If StringUtil.IsEmpty(sheet.Cells(SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, stCol).Text) Then
                    ComFunc.ShowErrMsgBox("名称が未入力のインストール品番が存在します。")
                    Exit Sub
                End If
            End If

            For col1 As Integer = stCol To edCol - 1
                If StringUtil.IsEmpty(sheet.Cells(SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, col1).Text) Then
                    ComFunc.ShowErrMsgBox("名称が未入力のインストール品番が存在します。")
                    Exit Sub
                End If
                For col2 As Integer = col1 + 1 To edCol
                    If StringUtil.IsEmpty(sheet.Cells(SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, col2).Text) Then
                        ComFunc.ShowErrMsgBox("名称が未入力のインストール品番が存在します。")
                        Exit Sub
                    End If
                    If sheet.Cells(SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, col1).Text = sheet.Cells(SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, col2).Text Then
                        ComFunc.ShowErrMsgBox("同一のインストール品番が存在します。" & vbCrLf & "修正してください。")
                        Exit Sub
                    End If

                Next
            Next


            Dim myResult As New Frm41KouseiBuhinSelector.EditDialogResult
            Dim frmKouseiBuhin As New Frm41KouseiBuhinSelector(yosanEventCode, yosanBukaCode, yosanTanto, koseiSubject, Me)

            'ダイアログを開いて、実行結果を受け取る
            myResult = frmKouseiBuhin.Show(Me)

        End Sub
#End Region

        Private Sub btnPrtsUnitPrice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        End Sub

        Private Sub ToolStripBtnKoujiShireiNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripBtnKoujiShireiNo.Click
            If StringUtil.IsEmpty(Me.ToolStripTxtKoujiShireiNo.Text) Then
                ComFunc.ShowErrMsgBox("工事指令No.を入力してください。")
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim dao As ASCYNAA010Dao = New ASCYNAA010DaoImpl
            Dim vos As List(Of ASCYNAA010Vo) = dao.FindByKoujishirei(Me.ToolStripTxtKoujiShireiNo.Text.ToUpper)
            Dim hsWk As New Hashtable
            Dim MIMX As New List(Of ASCYNAA010Vo)
            For Each vo As ASCYNAA010Vo In vos
                If StringUtil.IsNotEmpty(vo.BUBA15) Then
                    'MIX部品費、MIX型費表示準備
                    If Not hsWk.Contains(vo.BUBA15) Then
                        hsWk.Add(vo.BUBA15, New List(Of ASCYNAA010Vo))
                    End If
                    CType(hsWk.Item(vo.BUBA15), List(Of ASCYNAA010Vo)).Add(vo)
                End If
            Next

            'MIX部品費、MIX型費表示レコード選択
            For Each buba15 As String In hsWk.Keys
                Dim wkMIMX As New List(Of ASCYNAA010Vo)
                For Each vo As ASCYNAA010Vo In CType(hsWk.Item(buba15), List(Of ASCYNAA010Vo))
                    If vo.MIMXKH > 0 Then
                        wkMIMX.Add(vo)
                    End If
                Next
                If wkMIMX.Count = 0 Then
                    Dim wkVo As ASCYNAA010Vo = Nothing
                    For Each vo As ASCYNAA010Vo In CType(hsWk.Item(buba15), List(Of ASCYNAA010Vo))
                        If wkVo Is Nothing Then
                            wkVo = vo
                        Else
                            If wkVo.MIMXBH < vo.MIMXBH Then
                                wkVo = vo
                            End If
                        End If
                    Next
                    MIMX.Add(wkVo)
                Else
                    Dim wkVo As ASCYNAA010Vo = Nothing
                    For Each vo As ASCYNAA010Vo In CType(hsWk.Item(buba15), List(Of ASCYNAA010Vo))
                        If wkVo Is Nothing Then
                            wkVo = vo
                        Else
                            If wkVo.MIMXKH < vo.MIMXKH Then
                                wkVo = vo
                            End If
                        End If
                    Next
                    MIMX.Add(wkVo)
                End If
            Next

            'MIX部品費表示
            Dim intCnt As Integer = 0
            For Each vo As ASCYNAA010Vo In MIMX

                '部品費が0以外
                If Not StringUtil.Equals(vo.MIMXBH, 0) Then

                    For row As Integer = SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX + 1 To spdParts_Sheet1.RowCount - 1
                        If StringUtil.Equals(spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_NO).Index).Value, _
                                             vo.BUBA15) Then
                            'データ数カウント
                            intCnt = intCnt + 1
                            spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_BUHINHYO).Index).Value = vo.MIMXBH
                            ' 該当セルの文字色、文字太を変更する。
                            spdParts_Sheet1.SetStyleInfo(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_BUHINHYO).Index, CreateNewStyle())
                            ''型費は不要　20150226　FHI森さんに確認
                            'spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_KATA_HI).Index).Value = vo.MIMXKH
                        End If
                    Next

                End If

            Next

            Dim strMsg As String = "発注実績取込が完了しました。"
            If intCnt > 0 Then
                ComFunc.ShowInfoMsgBox(strMsg, MessageBoxButtons.OK)
            End If

            Me.Cursor = Cursors.Default

        End Sub


        Private Sub ToolStripBtnKeisuUpd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripBtnKeisuUpd.Click
            If StringUtil.IsEmpty(Me.ToolStripTxtBuhinKeisu.Text) Then
                ComFunc.ShowErrMsgBox("係数を入力してください。")
                Exit Sub
            End If

            Dim d As Decimal
            If Double.TryParse(Me.ToolStripTxtBuhinKeisu.Text, d) Then
            Else
                ComFunc.ShowErrMsgBox("数字以外の文字は入力できません。")
                Me.ToolStripTxtBuhinKeisu.Text = ""
                Me.ToolStripTxtBuhinKeisu.Focus()
                Exit Sub
            End If

            Me.ToolStripTxtBuhinKeisu.Text = CDec(ToolStripTxtBuhinKeisu.Text).ToString("0.00")

            '連続したセルを選択している場合には、これで取得可能
            Dim SelectRange As FarPoint.Win.Spread.Model.CellRange
            '選択範囲を取得
            SelectRange = spdParts_Sheet1.GetSelection(0)

            If StringUtil.IsEmpty(SelectRange) Then
                ComFunc.ShowErrMsgBox("設定先を選択してください。")
                Exit Sub
            End If

            '選択範囲を変数に
            Dim topRow As Integer = SelectRange.Row
            Dim topCntRow As Integer = SelectRange.RowCount
            Dim topColumn As Integer = SelectRange.Column
            Dim topCntColumn As Integer = SelectRange.ColumnCount

            'データ列以外なら処理終了
            If SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX + 1 > topRow Then
                Exit Sub
            End If

            Dim intCnt As Integer = 0

            For row As Integer = topRow To topRow + SelectRange.RowCount - 1

                '選択範囲に対象セルが入っていたら処理続行
                If topColumn <= spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_TOKKI).Index And _
                    spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_TOKKI).Index <= topColumn + topCntColumn - 1 Then

                    If StringUtil.IsNotEmpty(spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN).Index).Value) OrElse _
                       spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN).Index).Value = "0.00" Then
                        'データ数カウント
                        intCnt = intCnt + 1
                        spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_TOKKI).Index).Value = _
                            spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN).Index).Value * Me.ToolStripTxtBuhinKeisu.Text
                        ' 該当セルの文字色、文字太を変更する。
                        spdParts_Sheet1.SetStyleInfo(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_TOKKI).Index, CreateNewStyle())

                    End If
                End If
            Next

            If intCnt > 0 Then
                ComFunc.ShowInfoMsgBox("係数設定が完了しました。", MessageBoxButtons.OK)
            ElseIf intCnt = 0 Then
                ComFunc.ShowErrMsgBox("係数設定先では無い、または既に設定済のセルが選択されています。" & vbLf _
                                       & vbLf & "係数設定は行われませんでした。", MessageBoxButtons.OK)
            Else
                ComFunc.ShowErrMsgBox("係数設定先では無い、または既に設定済のセルが選択されています。" & vbLf _
                                       & vbLf & "係数設定は行われませんでした。", MessageBoxButtons.OK)
            End If

            Me.Cursor = Cursors.Default
        End Sub

        Private Sub ToolStripBtnPartsPriceUpd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripBtnPartsPriceUpd.Click
            If StringUtil.IsEmpty(Me.ToolStripCmbKokunaiKaigai.Text) Then
                ComFunc.ShowErrMsgBox("国内優先/海外優先から選択してください。")
                Exit Sub
            End If

            '20150304　SBKS森さんより　「範囲指定は不要」とのことなのでコメントアウトする。

            ''連続したセルを選択している場合には、これで取得可能
            'Dim SelectRange As FarPoint.Win.Spread.Model.CellRange
            ''選択範囲を取得
            'SelectRange = spdParts_Sheet1.GetSelection(0)

            'If StringUtil.IsEmpty(SelectRange) Then
            '    ComFunc.ShowErrMsgBox("設定先を選択してください。")
            '    Exit Sub
            'End If

            ''選択範囲を変数に
            'Dim topRow As Integer = SelectRange.Row
            'Dim topCntRow As Integer = SelectRange.RowCount
            'Dim topColumn As Integer = SelectRange.Column
            'Dim topCntColumn As Integer = SelectRange.ColumnCount

            ''データ列以外なら処理終了
            'If SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX + 1 > topRow Then
            '    Exit Sub
            'End If

            Dim tankaDao As TYosanTankaDao = New TYosanTankaDaoImpl
            Dim KokuKai As String = ""
            Dim intCnt As Integer = 0
            Dim resultVo As TYosanTankaVo

            'For row As Integer = topRow To topRow + SelectRange.RowCount - 1
            For row As Integer = SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX + 1 To spdParts_Sheet1.RowCount - 1
                ''選択範囲に対象セルが入っていたら処理続行
                'If topColumn <= spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN).Index And _
                '    spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN).Index <= topColumn + topCntColumn - 1 Then

                For i As Integer = 1 To 2
                    'If StringUtil.IsEmpty(spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN).Index).Value) OrElse _
                    '    spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN).Index).Value = "0.00" Then

                    If i = 1 Then
                        If StringUtil.Equals(ToolStripCmbKokunaiKaigai.Text, "国内優先") Then
                            KokuKai = "D"
                        Else
                            KokuKai = "F"
                        End If
                    Else
                        If StringUtil.Equals(KokuKai, "D") Then
                            KokuKai = "F"
                        Else
                            KokuKai = "D"
                        End If
                    End If


                    'iが１のときは上書き、2のときは、値が無ければセット
                    If i = 1 OrElse _
                          i = 2 And StringUtil.IsEmpty(spdParts_Sheet1.Cells(row, _
                                            spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN).Index).Value) OrElse _
                          i = 2 And spdParts_Sheet1.Cells(row, _
                                            spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN).Index).Value = "0.00" Then

                        '部品番号がある行のみ
                        If StringUtil.IsNotEmpty(spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_NO).Index).Value) Then
                            '値を取得する
                            resultVo = tankaDao.FindByPk(KokuKai, _
                                                spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_NO).Index).Value)
                            If StringUtil.IsNotEmpty(resultVo) Then
                                '値をセットする。
                                spdParts_Sheet1.Cells(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN).Index).Value = resultVo.YosanBuhinHiRyosan
                                ' 該当セルの文字色、文字太を変更する。
                                spdParts_Sheet1.SetStyleInfo(row, spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN).Index, CreateNewStyle())
                                'データ数カウント
                                intCnt = intCnt + 1
                            End If
                        End If
                    End If

                    'End If
                Next

                'End If
            Next

            If intCnt > 0 Then
                ComFunc.ShowInfoMsgBox("パーツプライスの設定が完了しました。", MessageBoxButtons.OK)
            ElseIf intCnt = 0 Then
                ComFunc.ShowInfoMsgBox("指定した設定先に該当するパーツプライスが見つかりませんでした。", MessageBoxButtons.OK)
            Else
                ComFunc.ShowErrMsgBox("パーツプライスの定先では無い、または既に設定済のセルが選択されています。" & vbLf _
                                       & vbLf & "パーツプライスの設定は行われませんでした。", MessageBoxButtons.OK)
            End If

            Me.Cursor = Cursors.Default
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

            Dim rowIndex As Integer = e.Row
            Dim columnIndex As Integer = e.Column
            Dim MinColumnIndex As Integer = spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_KYOUKU_SECTION).Index
            Dim MaxColumnIndex As Integer = spdParts_Sheet1.Columns(SpdKoseiObserver.TAG_HENKO_GAIYO).Index
       
            If _dispMode = CInt(EDIT_MODE) Then
                If rowIndex = SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX AndAlso columnIndex > MinColumnIndex AndAlso columnIndex < MaxColumnIndex Then
                    'パターン名選択画面は表示せずに、パターン名を直接入力
                    If _patterNameList Is Nothing OrElse _patterNameList.Count = 0 Then
                        Return
                    End If

                    Dim value As String = spdParts_Sheet1.Cells(rowIndex, columnIndex).Value
                    Using frm As New FrmSelectPattern(_patterNameList)
                        frm.ShowDialog()
                        'パターン名を選択の場合
                        If frm.ResultOk Then
                            Dim patternName As String = frm.patternName()
                            spdParts_Sheet1.Cells(rowIndex, columnIndex).Value = patternName
                            ' 該当セルの文字色、文字太を変更する。
                            spdParts_Sheet1.SetStyleInfo(rowIndex, columnIndex, CreateNewStyle())
                        End If
                    End Using

                    spdParts_Sheet1.Cells(rowIndex, columnIndex).Locked = True
                End If
            End If

        End Sub
    End Class

End Namespace


