Imports EBom.Common
Imports EventSakusei
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.Model
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports YosansyoTool.YosanshoEdit.Logic
Imports YosansyoTool.YosanBuhinEdit
Imports FarPoint.Win

Namespace YosanshoEdit

    Public Class FrmYosanshoEdit

#Region " メンバー変数 "
        ''' <summary>ロジック</summary>
        Private m_EditLogic As DispYosanshoEdit = Nothing
        '''<summary>入力有無判定に使用</summary>>
        Private _aInputWatcher As InputWatcher
        Private _zaimuJisekiInputWatcher As InputWatcher

        Private _yosanEventCode As String
        Private m_ActiveSpread As String
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal yosanEventCode As String, ByVal strMode As String, ByVal login As LoginInfo)
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            Me._yosanEventCode = yosanEventCode

            '画面制御ロジック
            m_EditLogic = New DispYosanshoEdit(Me, yosanEventCode, strMode, login)

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
            'Try
            Cursor.Current = Cursors.WaitCursor

            'ヘッダー初期化
            m_EditLogic.InitializeHeader()
            '画面メイン初期化
            m_EditLogic.InitializeMain()
            'スプレッド初期化
            m_EditLogic.InitializeSpread()

            '初期データ表示スプレッド(製作台数)
            m_EditLogic.DispSpreadSeisakuDaisu()
            '初期データ表示スプレッド(金材)
            m_EditLogic.DispSpreadKanazai()
            '初期データ表示スプレッド(造り方)
            m_EditLogic.DispSpreadTukurikata()
            '初期データ表示スプレッド(集計値)
            m_EditLogic.DispSpreadZaimuJiseki()

            If m_EditLogic.IsEditMode Then
                'Spread使用可
                m_EditLogic.SetSpreadEnabled()
            Else
                'Spread使用不可
                m_EditLogic.SetSpreadDisabled()
            End If

            '入力監視初期化
            InitializeWatcher()
            'Catch ex As Exception
            '    Dim msg As String
            '    msg = String.Format("画面の初期化中に問題が発生しました(ERR={0})", ex.Message)
            '    ComFunc.ShowErrMsgBox(msg)
            '    Me.Close()
            'Finally
            Cursor.Current = Cursors.Default
            'End Try
        End Sub

        ''' <summary>
        ''' コントロール監視初期化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeWatcher()

            _aInputWatcher = New InputWatcher

            _aInputWatcher.Add(spdKanazai)
            _aInputWatcher.Add(spdSeisakuDaisu)
            _aInputWatcher.Add(spdTukurikataKanseisha)
            _aInputWatcher.Add(spdTukurikataWBsha)
            _aInputWatcher.Add(spdZaimuJiseki)

            '両スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
            Dim imKanazai As New FarPoint.Win.Spread.InputMap
            imKanazai = spdKanazai.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imKanazai.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

            Dim imSeisakuDaisu As New FarPoint.Win.Spread.InputMap
            imSeisakuDaisu = spdSeisakuDaisu.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imSeisakuDaisu.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

            Dim imTukurikataKanseisha As New FarPoint.Win.Spread.InputMap
            imTukurikataKanseisha = spdTukurikataKanseisha.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imTukurikataKanseisha.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

            Dim imTukurikataWBsha As New FarPoint.Win.Spread.InputMap
            imTukurikataWBsha = spdTukurikataWBsha.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imTukurikataWBsha.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

            Dim imZaimuJiseki As New FarPoint.Win.Spread.InputMap
            imZaimuJiseki = spdZaimuJiseki.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imZaimuJiseki.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)


            _zaimuJisekiInputWatcher = New InputWatcher

            _zaimuJisekiInputWatcher.Add(spdZaimuJiseki)
        End Sub
#End Region

#Region "ボタン"

#Region "戻るボタン"
        ''' <summary>
        ''' 戻るボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            If _aInputWatcher.WasUpdate Then
                If frm00Kakunin.Confirm("確認", "登録されていません。", "変更を破棄して終了しますか？", "OK", "CANCEL") = MsgBoxResult.Ok Then
                    Me.Close()
                    Return
                End If
            Else
                Me.Close()
                Return
            End If
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
                If frm01Kakunin.ConfirmOkCancel("Excelファイルに出力しますか？") <> MsgBoxResult.Ok Then
                    Return
                End If

                '画面を綺麗に、実行中のカーソルへ変更。
                Application.DoEvents()
                Cursor.Current = Cursors.WaitCursor

                Dim fileName As String = lblKubun.Text + " " + lblEvent.Text + " " + lblYosanCode.Text + "_" + Now.ToString("yyyyMMddHHmmss") + ".xls"
                Dim ExcelExport As New YosanShoExportExcel(_yosanEventCode, fileName, m_EditLogic, Me)

                '画面を綺麗に、実行中のカーソルを元に戻す。
                Cursor.Current = Cursors.Default

            Catch ex As Exception
                MsgBox("Excelの出力失敗しました。", MsgBoxStyle.Information, "エラー")
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub
#End Region

#Region "登録ボタン"
        ''' <summary>
        ''' 登録ボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
            'エラーチェックを行う
            If Me.spdTukurikataKanseisha_Sheet1.RowCount <= DispYosanshoEdit.SPD_TUKURIKATA_DEFAULT_ROW_COUNT Then
                ComFunc.ShowErrMsgBox("造り方完成車へパターン名を１件以上、登録してください。")
                Return
            End If
            If Me.spdTukurikataKanseisha_Sheet1.ColumnCount <= DispYosanshoEdit.SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT Then
                ComFunc.ShowErrMsgBox("造り方完成車へ月を挿入してください。")
                Return
            End If

            '登録処理
            m_EditLogic.Register()
            Me.Close()
        End Sub
#End Region

#Region "トリム部品表編集ボタン"
        ''' <summary>
        ''' トリム部品表編集ボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnTrimBuhinEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrimBuhinEdit.Click
            Me.Hide()
            DispFrmBuhinEdit(DispYosanshoEdit.UNIT_KBN_TRIM_CODE)
            Me.Show()
        End Sub
#End Region

#Region "メタル部品表編集ボタン"
        ''' <summary>
        ''' メタル部品表編集ボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnMetalBuhinEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMetalBuhinEdit.Click
            Me.Hide()
            DispFrmBuhinEdit(DispYosanshoEdit.UNIT_KBN_METAL_CODE)
            Me.Show()
        End Sub
#End Region

#Region "部品表編集画面表示処理"
        ''' <summary>
        ''' 部品表編集画面表示
        ''' </summary>
        ''' <param name="unitKbn"></param>
        ''' <remarks></remarks>
        Private Sub DispFrmBuhinEdit(ByVal unitKbn As String)

            Dim isEditMode As Boolean = True

            '閲覧モードの場合
            If Not m_EditLogic.IsEditMode Then
                '該当イベントのステータスが00:編集中の場合
                If StringUtil.Equals(m_EditLogic.YosanEventVo.YosanStatus, DispYosanshoEdit.STATUS_00) Then
                    If frm01Kakunin.ConfirmOkCancel("閲覧モードで開きますか？") = MsgBoxResult.Ok Then
                        isEditMode = False
                    End If
                Else
                    isEditMode = False
                End If
            End If

            If isEditMode Then
                '編集モードの場合、排他管理して、部品表編集画面を表示
                If m_EditLogic.DoExclusiveBuhin(_yosanEventCode, unitKbn) = True Then
                    Using frm As New Frm41DispYosanBuhinEdit00(GetPatternNameList(unitKbn), _yosanEventCode, unitKbn, LoginInfo.Now, EDIT_MODE, 0, "")
                        frm.ShowDialog()
                    End Using
                    m_EditLogic.SetSpreadTukurikataBuhinHi(True, True)
                End If
            Else
                '部品表編集画面を閲覧モードで表示
                Using frm As New Frm41DispYosanBuhinEdit00(GetPatternNameList(unitKbn), _yosanEventCode, unitKbn, LoginInfo.Now, VIEW_MODE, 0, "")
                    frm.ShowDialog()
                End Using
            End If

        End Sub
        ''' <summary>
        ''' 造り方完成車と造り方ﾎﾜｲﾄﾎﾞﾃﾞｨに登録されているパターン名取得
        ''' </summary>
        ''' <param name="unitKbn"></param>
        ''' <remarks></remarks>
        Private Function GetPatternNameList(ByVal unitKbn As String) As List(Of String)

            Dim patterNameList As New List(Of String)

            '完成車に登録されているパターン名取得
            Dim aPatternNameList As Dictionary(Of String, List(Of String)) = m_EditLogic.PatternNameList(spdTukurikataKanseisha_Sheet1)
            If aPatternNameList.ContainsKey(unitKbn) Then
                For Each patternName As String In aPatternNameList(unitKbn)
                    If Not patterNameList.Contains(patternName) Then
                        patterNameList.Add(patternName)
                    End If
                Next
            End If
            'ﾎﾜｲﾄﾎﾞﾃﾞｨに登録されているパターン名取得
            Dim bPatternNameList As Dictionary(Of String, List(Of String)) = m_EditLogic.PatternNameList(spdTukurikataWBsha_Sheet1)
            If bPatternNameList.ContainsKey(unitKbn) Then
                For Each patternName As String In bPatternNameList(unitKbn)
                    If Not patterNameList.Contains(patternName) Then
                        patterNameList.Add(patternName)
                    End If
                Next
            End If

            patterNameList.Sort()
            Return patterNameList

        End Function
#End Region

#End Region

#Region "マウス右クリックメニュー操作"
        ''' <summary>
        ''' マウス右クリックメニュー表示
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ContextMenuStrip1_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
            列削除ToolStripMenuItem.Visible = False
            行削除ToolStripMenuItem.Visible = False

            '編集モードの場合
            If m_EditLogic.IsEditMode Then
                Select Case m_ActiveSpread
                    Case "SeisakuDaisu"
                        If spdSeisakuDaisu_Sheet1.ColumnCount - DispYosanshoEdit.SPD_DAISU_KOTEI_COLUMN_COUNT - 2 > 0 Then
                            列削除ToolStripMenuItem.Visible = True
                        End If
                    Case "KanazaiRow"
                        行削除ToolStripMenuItem.Visible = True
                    Case "KanazaiColumn"
                        列削除ToolStripMenuItem.Visible = True
                    Case "KanseiRow"
                        行削除ToolStripMenuItem.Visible = True
                    Case "WbRow"
                        行削除ToolStripMenuItem.Visible = True
                    Case "KanseiColumn"
                        列削除ToolStripMenuItem.Visible = True
                    Case "WbColumn"
                        列削除ToolStripMenuItem.Visible = True
                    Case ""

                End Select
            End If
        End Sub

        ''' <summary>
        ''' 列削除
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 列削除ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 列削除ToolStripMenuItem.Click
            Select Case m_ActiveSpread
                Case "SeisakuDaisu"
                    '製作台数
                    If Me.spdSeisakuDaisu_Sheet1.SelectionCount <> 1 Then
                        Return
                    End If

                    Dim selection As CellRange = Me.spdSeisakuDaisu_Sheet1.GetSelection(0)
                    If frm00Kakunin.Confirm("確認", "工事例№[" & Me.spdSeisakuDaisu_Sheet1.GetText(selection.Row, selection.Column) & _
                                            "]の列を削除して宜しいですか？", "", "はい", "いいえ") = MsgBoxResult.Ok Then
                        m_EditLogic.RemoveYosanSeisakuDaisu(selection.Column)
                    End If
                Case "KanazaiColumn"
                    '金材
                    If Me.spdKanazai_Sheet1.SelectionCount <> 1 Then
                        Return
                    End If

                    Dim selection As CellRange = Me.spdKanazai_Sheet1.GetSelection(0)
                    If frm00Kakunin.Confirm("確認", "[" & Me.spdKanazai_Sheet1.GetText(selection.Row, selection.Column) & _
                                            "]の列を削除して宜しいですか？", "", "はい", "いいえ") = MsgBoxResult.Ok Then
                        m_EditLogic.RemoveYosanKanazaiColumn(selection.Column, Me.spdKanazai_Sheet1.GetText(selection.Row, selection.Column))
                    End If
                Case "KanseiColumn"
                    '完成車
                    If Me.spdTukurikataKanseisha_Sheet1.SelectionCount <> 1 Then
                        Return
                    End If

                    Dim selection As CellRange = Me.spdTukurikataKanseisha_Sheet1.GetSelection(0)
                    If frm00Kakunin.Confirm("確認", "[" & Me.spdTukurikataKanseisha_Sheet1.GetText(selection.Row, selection.Column) & _
                                            "]の列を削除して宜しいですか？", "", "はい", "いいえ") = MsgBoxResult.Ok Then
                        m_EditLogic.RemoveYosanTukurikataColumn(Me.spdTukurikataKanseisha_Sheet1, selection.Column, _
                                                                DispYosanshoEdit.SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX, _
                                                                Me.spdTukurikataKanseisha_Sheet1.GetText(selection.Row, selection.Column), _
                                                                DispYosanshoEdit.SHISAKU_SYUBETU_KANSEI)
                    End If
                Case "WbColumn"
                    'WB車
                    If Me.spdTukurikataWBsha_Sheet1.SelectionCount <> 1 Then
                        Return
                    End If

                    Dim selection As CellRange = Me.spdTukurikataWBsha_Sheet1.GetSelection(0)
                    If frm00Kakunin.Confirm("確認", "[" & Me.spdTukurikataWBsha_Sheet1.GetText(selection.Row, selection.Column) & _
                                            "]の列を削除して宜しいですか？", "", "はい", "いいえ") = MsgBoxResult.Ok Then
                        m_EditLogic.RemoveYosanTukurikataColumn(Me.spdTukurikataWBsha_Sheet1, selection.Column, _
                                                                DispYosanshoEdit.SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX, _
                                                                Me.spdTukurikataWBsha_Sheet1.GetText(selection.Row, selection.Column), _
                                                                DispYosanshoEdit.SHISAKU_SYUBETU_WB)
                    End If
                Case ""

            End Select
        End Sub

        ''' <summary>
        ''' 行削除
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 行削除ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 行削除ToolStripMenuItem.Click
            Select Case m_ActiveSpread
                Case "KanazaiRow"
                    '金材
                    If Me.spdKanazai_Sheet1.SelectionCount <> 1 Then
                        Return
                    End If

                    Dim selection As CellRange = Me.spdKanazai_Sheet1.GetSelection(0)
                    If frm00Kakunin.Confirm("確認", "[" & Me.spdKanazai_Sheet1.GetText(selection.Row, selection.Column) & _
                                            "]の行を削除して宜しいですか？", "", "はい", "いいえ") = MsgBoxResult.Ok Then
                        m_EditLogic.RemoveYosanKanazaiRow(selection.Row, Me.spdKanazai_Sheet1.GetText(selection.Row, selection.Column))
                    End If
                Case "KanseiRow"
                    '完成車
                    If Me.spdTukurikataKanseisha_Sheet1.SelectionCount <> 1 Then
                        Return
                    End If

                    Dim selection As CellRange = Me.spdTukurikataKanseisha_Sheet1.GetSelection(0)
                    If frm00Kakunin.Confirm("確認", "[" & Me.spdTukurikataKanseisha_Sheet1.GetText(selection.Row, selection.Column) & _
                                            "]の行を削除して宜しいですか？", "", "はい", "いいえ") = MsgBoxResult.Ok Then
                        m_EditLogic.RemoveYosanTukurikataRow(Me.spdTukurikataKanseisha_Sheet1, selection.Row, _
                                                             Me.spdTukurikataKanseisha_Sheet1.GetText(selection.Row, DispYosanshoEdit.SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX), _
                                                             Me.spdTukurikataKanseisha_Sheet1.GetText(selection.Row, DispYosanshoEdit.SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX), _
                                                             Me.spdTukurikataKanseisha_Sheet1.GetText(selection.Row, selection.Column), _
                                                             DispYosanshoEdit.SHISAKU_SYUBETU_KANSEI)
                    End If
                Case "WbRow"
                    'WB車
                    If Me.spdTukurikataWBsha_Sheet1.SelectionCount <> 1 Then
                        Return
                    End If

                    Dim selection As CellRange = Me.spdTukurikataWBsha_Sheet1.GetSelection(0)
                    If frm00Kakunin.Confirm("確認", "[" & Me.spdTukurikataWBsha_Sheet1.GetText(selection.Row, selection.Column) & _
                                            "]の行を削除して宜しいですか？", "", "はい", "いいえ") = MsgBoxResult.Ok Then
                        m_EditLogic.RemoveYosanTukurikataRow(Me.spdTukurikataWBsha_Sheet1, selection.Row, _
                                                             Me.spdTukurikataWBsha_Sheet1.GetText(selection.Row, DispYosanshoEdit.SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX), _
                                                             Me.spdTukurikataWBsha_Sheet1.GetText(selection.Row, DispYosanshoEdit.SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX), _
                                                             Me.spdTukurikataWBsha_Sheet1.GetText(selection.Row, selection.Column), _
                                                             DispYosanshoEdit.SHISAKU_SYUBETU_WB)
                    End If
                Case ""

            End Select
        End Sub

#End Region

#Region "Spreadのセルクリック"
        ''' <summary>
        ''' 集計値Spreadのセルクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdZaimuJiseki_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdZaimuJiseki.CellClick
            Dim rowIndex As Integer = e.Row
            Dim columnIndex As Integer = e.Column

            '右クリックされたセルをアクティブセルに設定する.
            spdZaimuJiseki_Sheet1.SetActiveCell(rowIndex, columnIndex)
            spdZaimuJiseki.ShowActiveCell(VerticalPosition.Nearest, HorizontalPosition.Nearest)
            spdZaimuJiseki_Sheet1.AddSelection(rowIndex, columnIndex, 1, 1)

            '再計算ボタンをクリック
            If rowIndex = DispYosanshoEdit.SPD_ZAIMUJISEKI_CALC_BUTTON_CELL_ROW_INDEX AndAlso _
               columnIndex = DispYosanshoEdit.SPD_ZAIMUJISEKI_CALC_BUTTON_CELL_COLUMN_INDEX Then
                If _zaimuJisekiInputWatcher.WasUpdate Then
                    If frm00Kakunin.Confirm("確認", "変更を破棄して", "再計算して宜しいですか？", "OK", "CANCEL") = MsgBoxResult.Ok Then
                        m_EditLogic.DoCalc(False)
                    End If
                Else
                    m_EditLogic.DoCalc(False)
                End If

                Return
            End If

            If rowIndex = DispYosanshoEdit.SPD_ZAIMUJISEKI_YEAR_ROW_INDEX AndAlso _
               Me.spdZaimuJiseki_Sheet1.ColumnCount > DispYosanshoEdit.SPD_ZAIMUJISEKI_DEFAULT_COLUMN_COUNT Then
                If (columnIndex - DispYosanshoEdit.SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 1) Mod (DispYosanshoEdit.HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    '拡大▶▶ボタンをクリック
                    m_EditLogic.VisibleMonthColumns(Me.spdZaimuJiseki_Sheet1, DispYosanshoEdit.SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
                                                    DispYosanshoEdit.SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, columnIndex, _
                                                    DispYosanshoEdit.SPD_ZAIMUJISEKI_KOTEI_COLUMN_COUNT)
                ElseIf (columnIndex + 2 - DispYosanshoEdit.SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX) Mod (DispYosanshoEdit.HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    '◀◀縮小ボタンをクリック
                    m_EditLogic.UnVisibleMonthColumns(Me.spdZaimuJiseki_Sheet1, DispYosanshoEdit.SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
                                                      DispYosanshoEdit.SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, columnIndex, _
                                                      DispYosanshoEdit.SPD_ZAIMUJISEKI_KOTEI_COLUMN_COUNT)
                End If

                Return
            End If

        End Sub

        ''' <summary>
        ''' 製作台数Spreadのセルクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdSeisakuDaisu_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdSeisakuDaisu.CellClick
            Dim rowIndex As Integer = e.Row
            Dim columnIndex As Integer = e.Column

            If Not m_EditLogic.IsEditMode Then
                Return
            End If

            '追加ボタンをクリック
            If rowIndex = DispYosanshoEdit.SPD_DAISU_ADD_BUTTON_CELL_ROW_INDEX AndAlso _
               columnIndex = DispYosanshoEdit.SPD_DAISU_ADD_BUTTON_CELL_COLUMN_INDEX Then
                Using frm As New FrmAddKojiShirei(m_EditLogic.YosanSeisakuDaisuList)
                    frm.ShowDialog()
                    '戻るの場合
                    If frm.ResultOk = False Then
                        Return
                    End If
                    '製作台数情報へ追加
                    m_EditLogic.AddYosanSeisakuDaisu(frm.UnitKbn, frm.KojishireiNo)
                End Using
            End If

            If e.Button = Windows.Forms.MouseButtons.Right Then
                '右クリックされたセルをアクティブセルに設定する.
                spdSeisakuDaisu_Sheet1.SetActiveCell(rowIndex, columnIndex)
                spdSeisakuDaisu.ShowActiveCell(VerticalPosition.Nearest, HorizontalPosition.Nearest)
                spdSeisakuDaisu_Sheet1.AddSelection(rowIndex, columnIndex, 1, 1)

                If rowIndex = DispYosanshoEdit.SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX AndAlso _
                   columnIndex >= DispYosanshoEdit.SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN Then
                    m_ActiveSpread = "SeisakuDaisu"
                Else
                    m_ActiveSpread = Nothing
                End If
            End If
        End Sub

        ''' <summary>
        ''' 金材Spreadのセルクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdKanazai_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdKanazai.CellClick
            Dim rowIndex As Integer = e.Row
            Dim columnIndex As Integer = e.Column

            '右クリックされたセルをアクティブセルに設定する.
            spdKanazai_Sheet1.SetActiveCell(rowIndex, columnIndex)
            spdKanazai.ShowActiveCell(VerticalPosition.Nearest, HorizontalPosition.Nearest)
            spdKanazai_Sheet1.AddSelection(rowIndex, columnIndex, 1, 1)

            '編集モードの場合、各ボタンの操作可能
            If m_EditLogic.IsEditMode Then
                '金材追加ボタンをクリック
                If rowIndex = DispYosanshoEdit.SPD_KANAZAI_ADD_BUTTON_CELL_ROW_INDEX AndAlso _
                   columnIndex = DispYosanshoEdit.SPD_KANAZAI_ADD_BUTTON_CELL_COLUMN_INDEX Then
                    Using frm As New FrmAddKanazai(m_EditLogic.KanazaiNameList)
                        frm.ShowDialog()
                        '戻るの場合
                        If frm.ResultOk = False Then
                            Return
                        End If
                        '金材情報へ追加
                        m_EditLogic.AddYosanKanazaiRow(frm.kanazaiName)
                    End Using

                    Return
                End If

                '月挿入ボタンをクリック
                If rowIndex = Me.spdKanazai_Sheet1.RowCount - 1 Then
                    If Me.spdKanazai_Sheet1.ColumnCount <= DispYosanshoEdit.SPD_KANAZAI_DEFAULT_COLUMN_COUNT Then
                        '初期挿入の場合
                        If columnIndex = DispYosanshoEdit.SPD_KANAZAI_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX OrElse _
                           columnIndex = Me.spdKanazai_Sheet1.ColumnCount - 1 Then
                            '月挿入画面表示
                            Using frm As New FrmInsertMonth()
                                frm.lblMark.Text = "≪金材≫"
                                frm.ShowDialog()
                                '戻るの場合
                                If frm.ResultOk = False Then
                                    Return
                                End If
                                '月挿入
                                m_EditLogic.AddYosanKanazaiColumn(frm.Year, frm.Ks, DispYosanshoEdit.SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, True)
                            End Using
                        End If
                    Else
                        '初期以降挿入の場合
                        Dim month As String = String.Empty
                        Dim year As Integer = 0
                        Dim ks As String = String.Empty
                        Dim insertCol As Integer = 0
                        If columnIndex = DispYosanshoEdit.SPD_KANAZAI_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX Then
                            month = Me.spdKanazai_Sheet1.GetValue(DispYosanshoEdit.SPD_KANAZAI_YEAR_ROW_INDEX, DispYosanshoEdit.SPD_KANAZAI_MONTH_COLUMNS_START_INDEX)

                            year = CInt(month.Substring(0, 4))
                            ks = month.Substring(4, 2)
                            If String.Equals(ks, DispYosanshoEdit.UP_KS) Then
                                ks = DispYosanshoEdit.DOWN_KS
                                year = year - 1
                            Else
                                ks = DispYosanshoEdit.UP_KS
                            End If
                            insertCol = columnIndex + 1
                        ElseIf columnIndex = Me.spdKanazai_Sheet1.ColumnCount - 1 Then
                            month = Me.spdKanazai_Sheet1.GetValue(DispYosanshoEdit.SPD_KANAZAI_YEAR_ROW_INDEX, Me.spdKanazai_Sheet1.ColumnCount - (DispYosanshoEdit.HALF_MONTH_DEFAULT_COUNT + 1))

                            year = CInt(month.Substring(0, 4))
                            ks = month.Substring(4, 2)
                            If String.Equals(ks, DispYosanshoEdit.UP_KS) Then
                                ks = DispYosanshoEdit.DOWN_KS
                            Else
                                ks = DispYosanshoEdit.UP_KS
                                year = year + 1
                            End If
                            insertCol = columnIndex
                        Else
                            Return
                        End If
                        '確認画面を表示
                        If FrmInsertMonthKakunin.Confirm("[" & year & "年" & ks & "]") = MsgBoxResult.Ok Then
                            '月挿入
                            m_EditLogic.AddYosanKanazaiColumn(year, ks, insertCol, False)
                        End If
                    End If

                    Return
                End If

                If e.Button = Windows.Forms.MouseButtons.Right Then
                    If columnIndex = DispYosanshoEdit.SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX AndAlso _
                       rowIndex >= DispYosanshoEdit.SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX AndAlso _
                       rowIndex < spdKanazai_Sheet1.RowCount - 1 Then
                        m_ActiveSpread = "KanazaiRow"
                    Else
                        m_ActiveSpread = Nothing
                    End If

                    If StringUtil.IsNotEmpty(m_ActiveSpread) Then
                        Return
                    End If

                    If rowIndex = DispYosanshoEdit.SPD_KANAZAI_MONTH_ROW_INDEX AndAlso _
                       Me.spdKanazai_Sheet1.ColumnCount > DispYosanshoEdit.SPD_KANAZAI_DEFAULT_COLUMN_COUNT AndAlso _
                       (columnIndex - DispYosanshoEdit.SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (DispYosanshoEdit.HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                        m_ActiveSpread = "KanazaiColumn"
                    Else
                        m_ActiveSpread = Nothing
                    End If
                End If
            End If

            If rowIndex = DispYosanshoEdit.SPD_KANAZAI_YEAR_ROW_INDEX AndAlso _
               Me.spdKanazai_Sheet1.ColumnCount > DispYosanshoEdit.SPD_KANAZAI_DEFAULT_COLUMN_COUNT Then
                If (columnIndex - DispYosanshoEdit.SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (DispYosanshoEdit.HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    '拡大▶▶ボタンをクリック
                    m_EditLogic.VisibleMonthColumns(Me.spdKanazai_Sheet1, DispYosanshoEdit.SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, _
                                                    DispYosanshoEdit.SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, columnIndex, _
                                                    DispYosanshoEdit.SPD_KANAZAI_KOTEI_COLUMN_COUNT)
                ElseIf (columnIndex + 2 - DispYosanshoEdit.SPD_KANAZAI_MONTH_COLUMNS_START_INDEX) Mod (DispYosanshoEdit.HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    '◀◀縮小ボタンをクリック
                    m_EditLogic.UnVisibleMonthColumns(Me.spdKanazai_Sheet1, DispYosanshoEdit.SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, _
                                                      DispYosanshoEdit.SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, columnIndex, _
                                                      DispYosanshoEdit.SPD_KANAZAI_KOTEI_COLUMN_COUNT)
                End If

                Return
            End If

        End Sub

        ''' <summary>
        ''' 造り方完成車Spreadのセルクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdspdTukurikataKanseisha_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdTukurikataKanseisha.CellClick
            Dim rowIndex As Integer = e.Row
            Dim columnIndex As Integer = e.Column

            '右クリックされたセルをアクティブセルに設定する.
            spdTukurikataKanseisha_Sheet1.SetActiveCell(rowIndex, columnIndex)
            spdTukurikataKanseisha.ShowActiveCell(VerticalPosition.Nearest, HorizontalPosition.Nearest)
            spdTukurikataKanseisha_Sheet1.AddSelection(rowIndex, columnIndex, 1, 1)

            TukurikataSpread_CellClick(spdTukurikataKanseisha_Sheet1, rowIndex, columnIndex, DispYosanshoEdit.SHISAKU_SYUBETU_KANSEI)

            '編集モードの場合、各ボタンの操作可能
            If m_EditLogic.IsEditMode Then
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    If columnIndex = DispYosanshoEdit.SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX AndAlso _
                       rowIndex >= DispYosanshoEdit.SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX AndAlso _
                       rowIndex < spdTukurikataKanseisha_Sheet1.RowCount - 1 Then
                        m_ActiveSpread = "KanseiRow"
                    Else
                        m_ActiveSpread = Nothing
                    End If

                    If StringUtil.IsNotEmpty(m_ActiveSpread) Then
                        Return
                    End If

                    If rowIndex = DispYosanshoEdit.SPD_TUKURIKATA_MONTH_ROW_INDEX AndAlso _
                       Me.spdTukurikataKanseisha_Sheet1.ColumnCount > DispYosanshoEdit.SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT AndAlso _
                       (columnIndex - DispYosanshoEdit.SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (DispYosanshoEdit.HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                        m_ActiveSpread = "KanseiColumn"
                    Else
                        m_ActiveSpread = Nothing
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' 造り方ﾎﾜｲﾄﾎﾞﾃﾞｨSpreadのセルクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdspdTukurikataWBsha_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdTukurikataWBsha.CellClick
            Dim rowIndex As Integer = e.Row
            Dim columnIndex As Integer = e.Column

            '右クリックされたセルをアクティブセルに設定する.
            spdTukurikataWBsha_Sheet1.SetActiveCell(rowIndex, columnIndex)
            spdTukurikataWBsha.ShowActiveCell(VerticalPosition.Nearest, HorizontalPosition.Nearest)
            spdTukurikataWBsha_Sheet1.AddSelection(rowIndex, columnIndex, 1, 1)

            TukurikataSpread_CellClick(spdTukurikataWBsha_Sheet1, rowIndex, columnIndex, DispYosanshoEdit.SHISAKU_SYUBETU_WB)

            '編集モードの場合、各ボタンの操作可能
            If m_EditLogic.IsEditMode Then
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    If columnIndex = DispYosanshoEdit.SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX AndAlso _
                       rowIndex >= DispYosanshoEdit.SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX AndAlso _
                       rowIndex < spdTukurikataWBsha_Sheet1.RowCount - 1 Then
                        m_ActiveSpread = "WbRow"
                    Else
                        m_ActiveSpread = Nothing
                    End If

                    If StringUtil.IsNotEmpty(m_ActiveSpread) Then
                        Return
                    End If

                    If rowIndex = DispYosanshoEdit.SPD_TUKURIKATA_MONTH_ROW_INDEX AndAlso _
                       Me.spdTukurikataWBsha_Sheet1.ColumnCount > DispYosanshoEdit.SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT AndAlso _
                       (columnIndex - DispYosanshoEdit.SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (DispYosanshoEdit.HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                        m_ActiveSpread = "WbColumn"
                    Else
                        m_ActiveSpread = Nothing
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' 造り方Spreadのセルクリック
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <remarks></remarks>
        Private Sub TukurikataSpread_CellClick(ByVal sheet As SheetView, ByVal rowIndex As Integer, ByVal columnIndex As Integer, ByVal shisakuSyubetu As String)

            '編集モードの場合、各ボタンの操作可能
            If m_EditLogic.IsEditMode Then
                '造り方追加ボタンをクリック
                If rowIndex = DispYosanshoEdit.SPD_TUKURIKATA_ADD_BUTTON_CELL_ROW_INDEX AndAlso _
                   columnIndex = DispYosanshoEdit.SPD_TUKURIKATA_ADD_BUTTON_CELL_COLUMN_INDEX Then
                    Using frm As New FrmAddPattern(m_EditLogic.PatternNameList(sheet), m_EditLogic.YosanBuhinEditPatternList, m_EditLogic.YosanBuhinhyoList)
                        If DispYosanshoEdit.IsKanseisha(shisakuSyubetu) Then
                            frm.lblMark.Text = "≪完成車≫"
                        Else
                            frm.lblMark.Text = "≪ＷＢ車≫"
                        End If
                        frm.ShowDialog()
                        '戻るの場合
                        If frm.ResultOk = False Then
                            Return
                        End If
                        '造り方情報へ追加
                        m_EditLogic.AddYosanTukurikataRow(sheet, frm.UnitKbn, frm.BuhinhyoName, frm.PatternName, shisakuSyubetu)
                    End Using

                    Return
                End If

                '月挿入ボタンをクリック
                If rowIndex = sheet.RowCount - 1 Then
                    If sheet.ColumnCount <= DispYosanshoEdit.SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT Then
                        '初期挿入の場合
                        If columnIndex = DispYosanshoEdit.SPD_TUKURIKATA_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX OrElse _
                           columnIndex = sheet.ColumnCount - 1 Then
                            '月挿入画面表示
                            Using frm As New FrmInsertMonth()
                                If DispYosanshoEdit.IsKanseisha(shisakuSyubetu) Then
                                    frm.lblMark.Text = "≪造り方・完成車≫"
                                Else
                                    frm.lblMark.Text = "≪造り方・ﾎﾜｲﾄﾎﾞﾃﾞｨ≫"
                                End If
                                frm.ShowDialog()
                                '戻るの場合
                                If frm.ResultOk = False Then
                                    Return
                                End If
                                '月挿入
                                m_EditLogic.AddYosanTukurikataColumn(sheet, frm.Year, frm.Ks, _
                                                                     DispYosanshoEdit.SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, _
                                                                     True, shisakuSyubetu)
                            End Using
                        End If
                    Else
                        '初期以降挿入の場合
                        Dim month As String = String.Empty
                        Dim year As Integer = 0
                        Dim ks As String = String.Empty
                        Dim insertCol As Integer = 0
                        If columnIndex = DispYosanshoEdit.SPD_TUKURIKATA_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX Then
                            month = sheet.GetValue(DispYosanshoEdit.SPD_TUKURIKATA_YEAR_ROW_INDEX, DispYosanshoEdit.SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX)

                            year = CInt(month.Substring(0, 4))
                            ks = month.Substring(4, 2)
                            If String.Equals(ks, DispYosanshoEdit.UP_KS) Then
                                ks = DispYosanshoEdit.DOWN_KS
                                year = year - 1
                            Else
                                ks = DispYosanshoEdit.UP_KS
                            End If
                            insertCol = columnIndex + 2
                        ElseIf columnIndex = sheet.ColumnCount - 1 Then
                            month = sheet.GetValue(DispYosanshoEdit.SPD_TUKURIKATA_YEAR_ROW_INDEX, sheet.ColumnCount - (DispYosanshoEdit.HALF_MONTH_DEFAULT_COUNT + 1))

                            year = CInt(month.Substring(0, 4))
                            ks = month.Substring(4, 2)
                            If String.Equals(ks, DispYosanshoEdit.UP_KS) Then
                                ks = DispYosanshoEdit.DOWN_KS
                            Else
                                ks = DispYosanshoEdit.UP_KS
                                year = year + 1
                            End If
                            insertCol = columnIndex
                        Else
                            Return
                        End If
                        '確認画面を表示
                        If FrmInsertMonthKakunin.Confirm("[" & year & "年" & ks & "]") = MsgBoxResult.Ok Then
                            '月挿入
                            m_EditLogic.AddYosanTukurikataColumn(sheet, year, ks, insertCol, False, shisakuSyubetu)
                        End If
                    End If

                    Return
                End If
            End If

            If rowIndex = DispYosanshoEdit.SPD_TUKURIKATA_YEAR_ROW_INDEX AndAlso _
                sheet.ColumnCount > DispYosanshoEdit.SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT Then
                If (columnIndex - DispYosanshoEdit.SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (DispYosanshoEdit.HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    '拡大▶▶ボタンをクリック
                    m_EditLogic.VisibleMonthColumns(sheet, DispYosanshoEdit.SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, _
                                                    DispYosanshoEdit.SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, columnIndex, _
                                                    DispYosanshoEdit.SPD_TUKURIKATA_KOTEI_COLUMN_COUNT)
                ElseIf (columnIndex + 2 - DispYosanshoEdit.SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX) Mod (DispYosanshoEdit.HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    '◀◀縮小ボタンをクリック
                    m_EditLogic.UnVisibleMonthColumns(sheet, DispYosanshoEdit.SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, _
                                                      DispYosanshoEdit.SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, columnIndex, _
                                                      DispYosanshoEdit.SPD_TUKURIKATA_KOTEI_COLUMN_COUNT)
                End If

                Return
            End If

        End Sub
#End Region

#Region "Spreadのキーダウン"

        Private Sub spdZaimuJiseki_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdZaimuJiseki.Change
            Dim sheet As Spread.SheetView = spdZaimuJiseki.ActiveSheet
            ' 選択セルの場所を特定します。
            Dim ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            Dim ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            ' 該当セルの文字色、文字太を変更する。
            Dim cellType = sheet.Cells(ParaActRowIdx, ParaActColIdx).CellType
            sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle(cellType))
        End Sub
        ''' <summary>
        ''' フォント色を青色に、文字を太くする。
        ''' </summary>
        ''' <remarks></remarks>
        Private Function CreateNewStyle(ByVal cellType As Object) As Spread.StyleInfo
            Dim styleinfo As New Spread.StyleInfo
            styleinfo.CellType = cellType
            styleinfo.ForeColor = Color.Blue '青色に
            styleinfo.Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
            '色、ロック、左寄せになるセルがあるので下記を実行する。
            styleinfo.BackColor = Color.White
            styleinfo.Locked = False
            styleinfo.HorizontalAlignment = CellHorizontalAlignment.Right
            Return styleinfo
        End Function
        ''' <summary>
        ''' 集計値Spread
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdZaimuJiseki_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdZaimuJiseki.KeyDown

            Spread_KeyDown(Me.spdZaimuJiseki_Sheet1, e)

        End Sub

        Private Sub spdSeisakuDaisu_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdSeisakuDaisu.Change
            Dim sheet As Spread.SheetView = spdSeisakuDaisu.ActiveSheet
            ' 選択セルの場所を特定します。
            Dim ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            Dim ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            ' 該当セルの文字色、文字太を変更する。
            Dim cellType = sheet.Cells(ParaActRowIdx, ParaActColIdx).CellType
            sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle(cellType))
        End Sub

        ''' <summary>
        ''' 製作台数Spread
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdSeisakuDaisu_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdSeisakuDaisu.KeyDown

            Spread_KeyDown(Me.spdSeisakuDaisu_Sheet1, e)

        End Sub

        Private Sub spdKanazai_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdKanazai.Change
            Dim sheet As Spread.SheetView = spdKanazai.ActiveSheet
            ' 選択セルの場所を特定します。
            Dim ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            Dim ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            ' 該当セルの文字色、文字太を変更する。
            Dim cellType = sheet.Cells(ParaActRowIdx, ParaActColIdx).CellType
            sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle(cellType))
        End Sub

        ''' <summary>
        ''' 金材Spread
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdKanazai_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdKanazai.KeyDown

            Spread_KeyDown(Me.spdKanazai_Sheet1, e)

        End Sub

        Private Sub spdTukurikataKanseisha_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdTukurikataKanseisha.Change
            Dim sheet As Spread.SheetView = spdTukurikataKanseisha.ActiveSheet
            ' 選択セルの場所を特定します。
            Dim ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            Dim ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            ' 該当セルの文字色、文字太を変更する。
            Dim cellType = sheet.Cells(ParaActRowIdx, ParaActColIdx).CellType
            sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle(cellType))
        End Sub

        ''' <summary>
        ''' 造り方完成車Spread
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdTukurikataKanseisha_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdTukurikataKanseisha.KeyDown

            Spread_KeyDown(Me.spdTukurikataKanseisha_Sheet1, e)

        End Sub

        Private Sub spdTukurikataWBsha_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdTukurikataWBsha.Change
            Dim sheet As Spread.SheetView = spdTukurikataWBsha.ActiveSheet
            ' 選択セルの場所を特定します。
            Dim ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            Dim ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            ' 該当セルの文字色、文字太を変更する。
            Dim cellType = sheet.Cells(ParaActRowIdx, ParaActColIdx).CellType
            sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle(cellType))
        End Sub

        ''' <summary>
        ''' 造り方ﾎﾜｲﾄﾎﾞﾃﾞｨSpread
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdTukurikataWBsha_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles spdTukurikataWBsha.KeyDown

            Spread_KeyDown(Me.spdTukurikataWBsha_Sheet1, e)

        End Sub

        ''' <summary>
        ''' Spreadのキーダウン
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <remarks></remarks>
        Private Sub Spread_KeyDown(ByVal sheet As SheetView, ByVal e As System.Windows.Forms.KeyEventArgs)
            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)
            Dim downKey As System.Windows.Forms.Keys = e.KeyCode
            Select Case downKey
                Case Keys.Delete
                    Dim row As Integer
                    Dim col As Integer
                    Dim colcount As Integer
                    Dim rowcount As Integer

                    If Not selection Is Nothing Then
                        If selection.Column = -1 AndAlso selection.ColumnCount - 1 Then
                            e.Handled = True
                        End If

                        row = selection.Row
                        col = selection.Column
                        colcount = selection.ColumnCount
                        rowcount = selection.RowCount
                    Else
                        row = sheet.ActiveRowIndex
                        col = sheet.ActiveColumnIndex
                        colcount = 1
                        rowcount = 1
                    End If

                    For rowIndex As Integer = row To row + rowcount - 1
                        For colIndex As Integer = col To col + colcount - 1
                            If sheet.Cells(rowIndex, colIndex).Locked = False Then
                                sheet.ClearRange(rowIndex, colIndex, 1, 1, True)
                                e.Handled = True
                            End If
                        Next
                    Next
            End Select
        End Sub
#End Region

#Region "Spreadの拡大、縮小"
        ''' <summary>
        ''' 集計値Spread
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnZaimuJiseki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZaimuJiseki.Click
            If StringUtil.Equals(btnZaimuJiseki.Text, "拡大>>") Then
                btnZaimuJiseki.Text = "<<縮小"
                pnlZaimuJiseki.Width = Me.Width - 15
                pnlZaimuJiseki.BringToFront()
                If StringUtil.Equals(btnSeisakuDaisu.Text, "<<縮小") Then
                    btnSeisakuDaisu.Text = "拡大>>"
                    pnlInputSpread.Location = New Point(657, 114)
                    pnlInputSpread.Width = 685
                    lblKanazai.Location = New Point(0, 110)
                    lblKanseisha.Location = New Point(0, 257)
                    lblWb.Location = New Point(0, 430)
                End If
            ElseIf StringUtil.Equals(btnZaimuJiseki.Text, "<<縮小") Then
                btnZaimuJiseki.Text = "拡大>>"
                pnlZaimuJiseki.Width = 656
            End If
        End Sub

        ''' <summary>
        ''' 製作台数Spread
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnSeisakuDaisu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeisakuDaisu.Click
            If StringUtil.Equals(btnSeisakuDaisu.Text, "拡大>>") Then
                btnSeisakuDaisu.Text = "<<縮小"
                'pnlSeisakuDaisu.Location = New Point(215, 113)
                'pnlSeisakuDaisu.Width = 1142
                'pnlSeisakuDaisu.BringToFront()
                pnlInputSpread.Location = New Point(0, 114)
                pnlInputSpread.Width = Me.Width - 15
                pnlInputSpread.BringToFront()
                lblKanazai.Location = New Point(657, 110)
                lblKanseisha.Location = New Point(657, 257)
                lblWb.Location = New Point(657, 430)
                If StringUtil.Equals(btnZaimuJiseki.Text, "<<縮小") Then
                    btnZaimuJiseki.Text = "拡大>>"
                    pnlZaimuJiseki.Width = 656
                End If
            ElseIf StringUtil.Equals(btnSeisakuDaisu.Text, "<<縮小") Then
                btnSeisakuDaisu.Text = "拡大>>"
                'pnlSeisakuDaisu.Location = New Point(658, 113)
                'pnlSeisakuDaisu.Width = 699
                pnlInputSpread.Location = New Point(657, 114)
                pnlInputSpread.Width = 685
                lblKanazai.Location = New Point(0, 110)
                lblKanseisha.Location = New Point(0, 257)
                lblWb.Location = New Point(0, 430)
            End If
        End Sub

        '''' <summary>
        '''' 金材Spread
        '''' </summary>
        '''' <param name="sender"></param>
        '''' <param name="e"></param>
        '''' <remarks></remarks>
        'Private Sub btnKanazai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKanazai.Click
        '    If StringUtil.Equals(btnKanazai.Text, "拡大>>") Then
        '        btnKanazai.Text = "<<縮小"
        '        pnlKanazai.Location = New Point(1, 238)
        '        pnlKanazai.Width = 1356
        '        pnlKanazai.BringToFront()
        '    ElseIf StringUtil.Equals(btnKanazai.Text, "<<縮小") Then
        '        btnKanazai.Text = "拡大>>"
        '        pnlKanazai.Location = New Point(658, 238)
        '        pnlKanazai.Width = 699
        '    End If
        'End Sub

        '''' <summary>
        '''' 造り方完成車Spread
        '''' </summary>
        '''' <param name="sender"></param>
        '''' <param name="e"></param>
        '''' <remarks></remarks>
        'Private Sub btnTukurikataKanseisha_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTukurikataKanseisha.Click
        '    If StringUtil.Equals(btnTukurikataKanseisha.Text, "拡大>>") Then
        '        btnTukurikataKanseisha.Text = "<<縮小"
        '        pnlTukurikata.Location = New Point(1, 385)
        '        pnlTukurikata.Width = 1356
        '        pnlTukurikata.Height = 345
        '        pnlTukurikata.BringToFront()
        '    ElseIf StringUtil.Equals(btnTukurikataKanseisha.Text, "<<縮小") Then
        '        btnTukurikataKanseisha.Text = "拡大>>"
        '        pnlTukurikata.Location = New Point(658, 385)
        '        pnlTukurikata.Width = 699
        '        pnlTukurikata.Height = 197
        '    End If
        'End Sub

        '''' <summary>
        '''' 造り方ﾎﾜｲﾄﾎﾞﾃﾞｨSpread
        '''' </summary>
        '''' <param name="sender"></param>
        '''' <param name="e"></param>
        '''' <remarks></remarks>
        'Private Sub btnTukurikataWBsha_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTukurikataWBsha.Click
        '    If StringUtil.Equals(btnTukurikataWBsha.Text, "拡大>>") Then
        '        btnTukurikataWBsha.Text = "<<縮小"
        '        pnlTukurikataWBsha.Location = New Point(1, 602)
        '        pnlTukurikataWBsha.Width = 1356
        '        pnlTukurikataWBsha.BringToFront()
        '    ElseIf StringUtil.Equals(btnTukurikataWBsha.Text, "<<縮小") Then
        '        btnTukurikataWBsha.Text = "拡大>>"
        '        pnlTukurikataWBsha.Location = New Point(658, 602)
        '        pnlTukurikataWBsha.Width = 699
        '    End If
        'End Sub
#End Region

#Region "タイマーコントロール"
        ''' <summary>
        ''' タイマーコントロール
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub
#End Region

#Region "フォーム閉じる"
        ''' <summary>
        ''' 画面を閉じるとき
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub FrmYosanshoEdit_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
            '排他更新削除
            m_EditLogic.DeleteExclusiveEvent()
        End Sub
#End Region

        Private Sub txtWbKeisu_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWbKeisu.KeyPress
            If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> "."c And e.KeyChar <> vbBack Then
                e.Handled = True
            End If
        End Sub

        ''' <summary>
        ''' （W／B）係数入力したら
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub txtWbKeisu_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWbKeisu.TextChanged
            m_EditLogic.SetSpreadTukurikataBuhinHi(False, True)
        End Sub

        Private Sub btnBuhinEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuhinEdit.Click
            Using frm As New FrmAddBuhin(m_EditLogic.YosanBuhinhyoList)
                frm.ShowDialog()
                '戻るの場合
                If frm.ResultOk = False Then
                    Return
                End If

                '予算書部品表選択情報へ追加
                m_EditLogic.AddBuhin(frm.BuhinhyoName)

                '部品表画面へ
                Me.Hide()
                'DispFrmBuhinEdit(DispYosanshoEdit.UNIT_KBN_TRIM_CODE)
                DispFrmBuhinEdit(frm.BuhinhyoName)
                Me.Show()

            End Using
        End Sub
    End Class

End Namespace
