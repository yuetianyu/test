Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports FarPoint.Win.Spread.Model
Imports FarPoint.Win
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Ui
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon.Util.OptionFilter
Imports FarPoint.Win.Spread
Imports EventSakusei.ShisakuBuhinEdit.Ui
Imports ShisakuCommon

Imports EventSakusei.ShisakuBuhinEdit.KouseiBuhin
Imports EventSakusei.KouseiBuhin
Imports EventSakusei.ShisakuBuhinEdit.Selector
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports ShisakuCommon.Db.EBom.Vo
Imports EBom.Common

Namespace ShisakuBuhinEdit.Kosei
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinFrm41DispShisakuBuhinEdit20

        '        'Private Const _spdParts_SHEET_NAME As String = "spdParts"

        'SPREADのサイズ変更用に使用する。
        '１は標準サイズ
        Private Kakudai As Decimal = 1

        Private SHEET As String = "N"
        Private DOOR As String = "N"
        Private ROOF As String = "N"
        Private SUNROOF As String = "N"

        'コピー＆ペースト用
        Private w_RowCount As Integer = 0
        Private w_ColumnCount As Integer = 0
        Private isOpening As Boolean = True
        'ペーストでセルを変更した際のチェンジイベント
        Dim WithEvents datamodel As FarPoint.Win.Spread.Model.DefaultSheetDataModel

        'ヘッダーの行高の標準サイズ。
        Private w_HEAD As Integer = 164

        ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化 (TES)張 ADD BEGIN
        Private Const LARGER_WIDTH = 200
        ''↑↑2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化 (TES)張 ADD END

        Private _dispMode As Integer

        Private ToolTipRange As FarPoint.Win.Spread.Model.CellRange

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


        Private _UpdateFlag As Boolean = False

        Public WriteOnly Property UpdateFlag() As Boolean
            Set(ByVal value As Boolean)
                _UpdateFlag = value
            End Set
        End Property


        'フォームのFormClosingイベントハンドラ
        Private Sub HoyouBuhinHoyouBuhinFrm41DispShisakuBuhinEdit20_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
            'If Me.WindowState = FormWindowState.Maximized Then
            '    '最大化されているときは閉じない
            e.Cancel = True
            'End If
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

        Private errorController As New ErrorController()


        Public Property EControl() As ErrorController
            Get
                Return errorController
            End Get
            Set(ByVal value As ErrorController)
                errorController = value
            End Set
        End Property

        Private ReadOnly hoyouEventCode As String
        Private ReadOnly hoyouBukaCode As String
        Private ReadOnly hoyouTanto As String
        Private koseiObserver As HoyouBuhinSpdKoseiObserver
        Private ReadOnly koseiSubject As HoyouBuhinBuhinEditKoseiSubject
        Private ReadOnly inputSupport As ShisakuInputSupport
        Public _frmDispShisakuBuhinEdit00 As HoyouBuhinFrm41DispShisakuBuhinEdit00

        Public Sub New(ByVal hoyouEventCode As String, ByVal hoyouBukaCode As String, ByVal hoyouTanto As String, ByVal koseiSubject As HoyouBuhinBuhinEditKoseiSubject, ByVal dispMode As Integer, _
                         ByVal frm41Disp00 As HoyouBuhinFrm41DispShisakuBuhinEdit00, ByVal updateFlag As Boolean)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)
            ShisakuFormUtil.setTitleVersion(Me)

            Me.StartPosition = FormStartPosition.Manual

            Me.hoyouEventCode = hoyouEventCode
            Me.hoyouBukaCode = hoyouBukaCode
            Me.hoyouTanto = hoyouTanto
            Me.koseiSubject = koseiSubject
            Me._UpdateFlag = updateFlag

            'FINAL品番を取得
            'Me.finalBuhinNos = koseiSubject.GetFinalBuhinNos

            _frmDispShisakuBuhinEdit00 = frm41Disp00

            _dispMode = dispMode

            koseiObserver = New HoyouBuhinSpdKoseiObserver(spdParts, koseiSubject)
            inputSupport = New ShisakuInputSupport(TxtInputSupport, spdParts)

            InitializeSpread()

            '非表示
            Dim MemoOnOff As String = koseiObserver.MemoSheetOnOff(hoyouEventCode, hoyouBukaCode, hoyouTanto)

            If StringUtil.Equals(MemoOnOff.Substring(0, 1), "0") Then
                koseiObserver.SheetColumnDisable()
            Else
                SHEET = "D"
                ToolStripSheet.Checked = True
                koseiObserver.SheetColumnVisible()
            End If
            If StringUtil.Equals(MemoOnOff.Substring(1, 1), "0") Then
                koseiObserver.DoorColumnDisable()
            Else
                DOOR = "D"
                ToolStripDoor.Checked = True
                koseiObserver.DoorColumnVisible()
            End If
            If StringUtil.Equals(MemoOnOff.Substring(2, 1), "0") Then
                koseiObserver.RoofColumnDisable()
            Else
                ROOF = "D"
                ToolStripRoof.Checked = True
                koseiObserver.RoofColumnVisible()
            End If
            If StringUtil.Equals(MemoOnOff.Substring(3, 1), "0") Then
                koseiObserver.SunRoofColumnDisable()
            Else
                SUNROOF = "D"
                ToolStripSun.Checked = True
                koseiObserver.SunRoofColumnVisible()
            End If

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

        ''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bf) (TES)張 ADD BEGIN
        Public Sub AssertValidateLevel()
            koseiObserver.AssertValidateLevel()
        End Sub
        ''↑↑2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bf) (TES)張 ADD END

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

        ''' <summary>
        ''' 員数セクションのチェック
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AssertValidateInsuSection()
            koseiObserver.AssertValidateKoseiInsuSection()
        End Sub

        Public Sub ClearSheetBackColor()
            koseiObserver.ClearSheetBackColor()
        End Sub
        Public Sub ClearSheetBackColorAll()
            koseiObserver.ClearSheetBackColorAll()
        End Sub

        Private Sub ContextMenuStrip1_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

            '2013/05/14　
            '閲覧モードの場合は常に「全て」無効
            If _dispMode = VIEW_MODE Then
                コピーToolStripMenuItem.Enabled = False
                貼り付けToolStripMenuItem.Enabled = False
                挿入ToolStripMenuItem.Enabled = False
                挿入下位ToolStripMenuItem.Enabled = False
                削除ToolStripMenuItem.Enabled = False
                子部品展開ToolStripMenuItem.Enabled = False
                挿入貼り付けToolStripMenuItem.Enabled = False
            Else
                'フィルタリング中かチェックする。
                'フィルタリング中なら行挿入、行削除を非表示にする。
                Dim wFilter As String = koseiObserver.FilterCheck()

                '切り取りToolStripMenuItem1.Enabled = True
                コピーToolStripMenuItem.Enabled = True
                貼り付けToolStripMenuItem.Enabled = True
                挿入ToolStripMenuItem.Enabled = False
                挿入下位ToolStripMenuItem.Enabled = False
                削除ToolStripMenuItem.Enabled = False
                子部品展開ToolStripMenuItem.Enabled = False
                挿入貼り付けToolStripMenuItem.Enabled = False

                If spdParts_Sheet1.SelectionCount <> 1 Then
                    Return
                End If

                Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)

                If SpreadUtil.IsSelectedRow(selection) AndAlso spdParts_Sheet1.Cells(selection.Row, 0).Value Is Nothing Then
                    If StringUtil.IsEmpty(wFilter) Then
                        挿入ToolStripMenuItem.Enabled = True
                        挿入下位ToolStripMenuItem.Enabled = True
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

                    '０レベルチェックは不要。　2014/03/05
                    ''但しINSTL品番行の場合は表示しない。
                    If StringUtil.IsEmpty(wFilter) Then
                        挿入ToolStripMenuItem.Enabled = True
                        挿入下位ToolStripMenuItem.Enabled = True
                        削除ToolStripMenuItem.Enabled = True
                        '切り取りToolStripMenuItem1.Enabled = True
                        コピーToolStripMenuItem.Enabled = True
                        貼り付けToolStripMenuItem.Enabled = True
                        If copyColumn = -1 Then
                            挿入貼り付けToolStripMenuItem.Enabled = True
                        Else
                            挿入貼り付けToolStripMenuItem.Enabled = False
                        End If
                    End If
                End If

                Dim sheet As Spread.SheetView = spdParts.ActiveSheet
                ' 選択セルの場所を特定します。
                ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
                ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス
                If ParaActColIdx = 6 AndAlso koseiObserver.IsDataRow(selection.Row) Then
                    '但しINSTL品番行の場合は表示しない。

                    Dim aPara As String = spdParts_Sheet1.Cells(selection.Row, 0).Value
                    If aPara = "" Then
                        aPara = "0"
                    End If
                    If aPara <> 0 Then
                        If StringUtil.IsEmpty(wFilter) Then
                            子部品展開ToolStripMenuItem.Enabled = True
                        End If
                    End If
                End If

                'タイトル行だったら
                Dim titleRows As Integer = BuhinEditSpreadUtil.GetTitleRows(sheet)
                If selection.Row < titleRows Then
                    If selection.Row < titleRows Then
                        If selection.Column < sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_1).Index Or selection.Column > sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_10).Index Then
                            '切り取りToolStripMenuItem1.Enabled = False
                            コピーToolStripMenuItem.Enabled = False
                            貼り付けToolStripMenuItem.Enabled = False
                            挿入貼り付けToolStripMenuItem.Enabled = False
                        End If
                    End If
                End If


                'フィルタリング中だったら・・・
                If Not StringUtil.IsEmpty(wFilter) Then
                    挿入ToolStripMenuItem.Enabled = False
                    挿入下位ToolStripMenuItem.Enabled = False
                    削除ToolStripMenuItem.Enabled = False
                    '切り取りToolStripMenuItem1.Enabled = False
                    コピーToolStripMenuItem.Enabled = False
                    貼り付けToolStripMenuItem.Enabled = False
                    挿入貼り付けToolStripMenuItem.Enabled = False
                End If
                For rowindex As Integer = selection.Row To selection.Row + selection.RowCount - 1
                    If spdParts_Sheet1.Cells(rowindex, 0).Text = "0" Then
                        挿入ToolStripMenuItem.Enabled = False
                        削除ToolStripMenuItem.Enabled = False
                        挿入下位ToolStripMenuItem.Enabled = False
                        Exit For
                    End If
                Next
                If selection.Row > 0 Then
                    If spdParts_Sheet1.Cells(selection.Row - 1, 0).Text = "0" Then
                        挿入ToolStripMenuItem.Enabled = False
                        '挿入下位ToolStripMenuItem.Enabled = False
                    End If
                End If

            End If

        End Sub

        Private Sub 挿入ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 挿入ToolStripMenuItem.Click
            If spdParts_Sheet1.SelectionCount <> 1 Then
                Return
            End If

            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)
            Dim i As Integer = 0
            Dim wRowCount As Integer = selection.RowCount

            If spdParts_Sheet1.Cells(selection.Row - 1, 0).Text = "0" Then
                ComFunc.ShowErrMsgBox("レベルに0を設定することは出来ません。")
                Exit Sub
            End If

            '自給品非表示の場合、自給品行数を除いた行数を計算
            If HoyouBuhinSpdKoseiObserver.SPREAD_JIKYU = "N" Then
                wRowCount = IsSelectRows(selection.Row, selection.RowCount)
            End If

            If SpreadUtil.IsSelectedRow(selection) Then
                koseiObserver.InsertRows(selection.Row, wRowCount)
            End If
        End Sub

        Private Sub 挿入下位ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 挿入下位ToolStripMenuItem.Click
            If spdParts_Sheet1.SelectionCount <> 1 Then
                Return
            End If

            Dim selection As CellRange = spdParts_Sheet1.GetSelection(0)
            Dim i As Integer = 0
            Dim wRowCount As Integer = selection.RowCount

            If SpreadUtil.IsSelectedRow(selection) Then
                koseiObserver.InsertRowsNext(selection.Row, wRowCount)
            End If
        End Sub

        ''' <summary>
        ''' 自給品を除いた行数を返す
        ''' </summary>
        ''' <param name="selectionRow">行index</param>
        ''' <param name="selectionRowCount">行カウント数</param>
        ''' <returns>行数</returns>
        ''' <remarks></remarks>
        Private Function IsSelectRows(ByVal selectionRow As Integer, ByVal selectionRowCount As Integer)

            Return Nothing
        End Function

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
        ''' 確認画面Cloes時の追加処理
        ''' </summary>
        ''' <remarks></remarks>
        Private Interface IConfirmFormCloseAdd
            ''' <summary>
            ''' 追加処理
            ''' </summary>
            ''' <param name="IsOk">OKが押された場合、true</param>
            ''' <remarks></remarks>
            Sub Process(ByVal IsOk As Boolean)
        End Interface

        ''' <summary>
        ''' キャンセルされたら古い部品構成編集に戻すCloser実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class MatrixBackCloserIfNecessary : Implements frm01Kakunin.IFormCloser
            Private koseiSubject As HoyouBuhinBuhinEditKoseiSubject
            Private bakMatrix As HoyouBuhinBuhinKoseiMatrix
            Private aFormCloseAdd As IConfirmFormCloseAdd
            Public Sub New(ByVal koseiSubject As HoyouBuhinBuhinEditKoseiSubject, ByVal bakMatrix As HoyouBuhinBuhinKoseiMatrix, ByVal aFormCloseAdd As IConfirmFormCloseAdd)
                Me.koseiSubject = koseiSubject
                Me.bakMatrix = bakMatrix
                Me.aFormCloseAdd = aFormCloseAdd
            End Sub
            Public Sub FormClose(ByVal IsOk As Boolean) Implements frm01Kakunin.IFormCloser.FormClose
                If Not IsOk Then
                    koseiSubject.SupersedeMatrix(bakMatrix)
                End If
                koseiSubject.IsViewerMode = False
                koseiSubject.NotifyObservers()
                If aFormCloseAdd IsNot Nothing Then
                    aFormCloseAdd.Process(IsOk)
                End If
            End Sub
        End Class

        ''' <summary>
        ''' 新しい部品構成編集で良いかを確認し、確認後に適切な処置を行う
        ''' </summary>
        ''' <param name="newMatrix">新しい部品構成編集</param>
        ''' <param name="aFormCloseAdd">確認画面Close時の追加処理</param>
        ''' <remarks></remarks>
        Private Sub ConfirmNewMatrix(ByVal newMatrix As HoyouBuhinBuhinKoseiMatrix, Optional ByVal aFormCloseAdd As IConfirmFormCloseAdd = Nothing)

            Dim bakMatrix As HoyouBuhinBuhinKoseiMatrix = koseiSubject.Matrix

            ''↓↓2014/09/04 Ⅰ.3.設計編集 ベース車改修専用化_bg) 酒井 DEL BEGIN
            'koseiSubject.IsViewerMode = True
            ''↑↑2014/09/04 Ⅰ.3.設計編集 ベース車改修専用化_bg) 酒井 DEL END
            koseiSubject.SupersedeMatrix(newMatrix)
            koseiSubject.NotifyObservers()

            Dim closer As New MatrixBackCloserIfNecessary(koseiSubject, bakMatrix, aFormCloseAdd)

            'frm01Kakunin.ConfirmShow("確定します。宜しいですか？", closer)

        End Sub

        Private Sub HoyouBuhinHoyouBuhinFrm41DispShisakuBuhinEdit20_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'フィルタリングを全列に設定
            '       OptionFilterCommon.SetOptionFilter(spdParts.ActiveSheet, spdParts.ActiveSheet.ColumnCount - 1, 4)

            ' ［Ctrl］+［C］キーを無効とします
            Dim im As New FarPoint.Win.Spread.InputMap
            im = spdParts.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)
            ' ［Ctrl］+［X］キーを無効とします'
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.X, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)
            isOpening = False
            If _UpdateFlag Then
                With Me.spdParts.ActiveSheet
                    .Cells(4, 6).Locked = True
                End With
            End If

        End Sub

        Private Sub spdParts_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdParts.Change

            Dim sheet As Spread.SheetView = spdParts.ActiveSheet
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            ' 該当セルの文字色、文字太を変更する。
            sheet.SetStyleInfo(ParaActRowIdx, ParaActColIdx, CreateNewStyle())


            koseiObserver.Change_EditInsuu(sheet.ActiveCell)



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

        Private Sub コピーToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles コピーToolStripMenuItem.Click
            'オプション・コピーサブルーチンへ
            OptionCopy()
        End Sub

        Private Sub コピーCToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles コピーCToolStripButton.Click
            'オプション・コピーサブルーチンへ
            OptionCopy()
        End Sub

        Private Sub 貼り付けToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 貼り付けToolStripMenuItem.Click
            'オプション・ペーストサブルーチンへ
            OptionPaste()

        End Sub

        Private Sub 貼り付けPToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 貼り付けPToolStripButton.Click
            'オプション・ペーストサブルーチンへ
            OptionPaste()
        End Sub

        Private Sub 切り取りToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            'オプション・カットサブルーチンへ
            OptionCut()
        End Sub

        Private Sub 切り取りUToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            'オプション・カットサブルーチンへ
            OptionCut()
        End Sub

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

        Private Sub sequence2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sequence2.Click
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

        Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
            Try
                ' 現在選択しているセル範囲を取得します。
                cr = spdParts.ActiveSheet.GetSelection(0)
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

#Region "ツールボタン(フィルタ解除)"
        ''' <summary>
        ''' ツールボタン(フィルタ解除)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub FilterCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

            'Try
            '    Cursor = Cursors.WaitCursor
            '    '指定列のフィルタリングを解除
            koseiObserver.ResetFilter(spdParts_Sheet1.ActiveColumn.Index, spdParts_Sheet1.ActiveColumn.Index2)

            'Catch ex As Exception
            '    MsgBox(String.Format("フィルタリング解除でシステムエラーが発生しました。({0})", ex.Message), MsgBoxStyle.Critical)
            'Finally
            '    Cursor = Cursors.Default
            'End Try
        End Sub

        ''' <summary>
        ''' ツールボタン(フィルタ解除(全部))
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub FilterCancelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterCancelAll.Click

            ''全列のフィルタリングを解除
            'For colIdx As Integer = 0 To spdParts.ActiveSheet.ColumnCount - 1
            ' '   OptionFilterCommon.SetFilterCancel(spdParts.ActiveSheet, colIdx, 5)
            'Next

            Try

                Cursor = Cursors.WaitCursor

                '全列のフィルタリングを解除
                koseiObserver.ResetFilterAll()

            Catch ex As Exception
                MsgBox(String.Format("フィルタリング解除でシステムエラーが発生しました。({0})", ex.Message), MsgBoxStyle.Critical)
            Finally
                Cursor = Cursors.Default
            End Try
            '自給品ボタン非表示の為、コメントアウト
            ''自給品をいったん表示しフラグによって続けて非表示にする。'
            'koseiObserver.JikyuRowVisible()
            'If JIKYU = "N" Then
            '    koseiObserver.JikyuRowDisable()
            'End If

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
                koseiObserver.SetFiltering()

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


            '
            '   上記方法でないと、アンドゥができなくなる
            '

            'Dim Sheet As Spread.SheetView = Me.spdParts_Sheet1
            'Dim Selection As FarPoint.Win.Spread.Model.CellRange = Sheet.GetSelection(0)

            ''情報列タイトルの色をチェック
            'For i As Integer = 0 To Sheet.ColumnCount - 1

            '    '青色か？
            '    If Sheet.Cells(0, i, 2, i).ForeColor = Color.Blue Then
            '        Exit Sub
            '    End If
            'Next

            ''   クリップボードからデータを２次元配列で受ける
            'Dim listClip As New List(Of String())
            'listClip = GetClipbordList()

            ''   中身がNothingでなければ貼り付けをしていく
            'If Not listClip Is Nothing Then


            '    Dim selColumnCount As Integer = 0
            '    Dim selCol As Integer = 0

            '    If Not Selection Is Nothing Then
            '        If Selection.Column = -1 AndAlso Selection.ColumnCount - 1 Then
            '            selColumnCount = spdParts.ActiveSheet.ColumnCount
            '        Else
            '            selColumnCount = Selection.ColumnCount
            '            selCol = Selection.Column
            '        End If
            '    Else
            '        selCol = Sheet.ActiveColumnIndex
            '        selColumnCount = 1
            '    End If

            '    Dim rowCount As Integer = listClip.Count - 1
            '    Dim colCount As Integer = listClip(0).Length


            '    '
            '    '   以下の処理でSendKeyみたいなことを行って動作不安定ならば、直接データをセットする
            '    '

            '    '2012/02/01'
            '    'スプレッド自身に貼り付けさせる'
            '    Dim im As New InputMap
            '    spdParts.ClipboardOptions = ClipboardOptions.NoHeaders
            '    im.Put(New Keystroke(Keys.V, Keys.Control), SpreadActions.ClipboardPasteValues)

            '    'spdParts.ActiveSheet.ClipboardPaste(ClipboardOptions.NoHeaders)


            '    '
            '    '   貼り付けることで、行が足りなければ、追加   
            '    '
            '    If (Selection.Row + rowCount) >= Sheet.RowCount - 1 Then
            '        Sheet.RowCount = Selection.Row + rowCount
            '    End If


            '    '
            '    '   列位置の最大値チェック
            '    '
            '    If colCount > Sheet.ColumnCount Then
            '        colCount = Sheet.ColumnCount
            '    End If


            '    Dim S As String = ""

            '    '   システムクリップボードにあるデータを取得
            '    Dim iData As IDataObject = Clipboard.GetDataObject()

            '    '   テキスト形式データの判断
            '    '       ここの処理まで来ているって事で、DataFormatsはTextなのですが、念のため
            '    '
            '    If iData.GetDataPresent(DataFormats.Text) = False Then
            '    Else
            '        S = CType(iData.GetData(DataFormats.Text), String)
            '    End If

            '    '
            '    '   Excelのコピー時には、クリップボードに文字列で入ってくる。
            '    '   列はタブコード(VB_TAB)、行はエンター(vbCRLF)で区切られている。
            '    '   Spreadも同等なので、SetClipValueメソッドで直に書き込む
            '    '
            '    '   1件づつ書き込んでは、処理速度低下や、Validate処理が邪魔したりするのでこのようにした
            '    '   
            '    'spdParts_Sheet1.SetClipValue(Selection.Row, Selection.Column, -1, -1, S)


            '    'セル編集モード時にコピーした場合、以下を行う。
            '    If rowCount = 0 Then
            '        rowCount = 1
            '    End If

            '    '行選択時
            '    If Not Selection Is Nothing Then
            '        If Selection.Column = -1 Then

            '            '貼りつけ対象のセルを編集済みとし書式を設定する
            '            Me.spdParts_Sheet1.Rows(Selection.Row, Selection.Row + rowCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
            '            Me.spdParts_Sheet1.Rows(Selection.Row, Selection.Row + rowCount - 1).ForeColor = Color.Blue

            '        Else


            '            ''貼りつけ対象のセルを編集済みとし書式を設定する
            '            Me.spdParts_Sheet1.Cells(Selection.Row, selCol, Selection.Row + rowCount - 1, _
            '                                   Selection.Column + colCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

            '            Me.spdParts_Sheet1.Cells(Selection.Row, selCol, Selection.Row + rowCount - 1, _
            '                            selCol + colCount - 1).ForeColor = Color.Blue
            '        End If
            '    Else
            '        '貼りつけ対象のセルを編集済みとし書式を設定する
            '        Me.spdParts_Sheet1.Cells(Sheet.ActiveRowIndex, selCol, Sheet.ActiveRowIndex, _
            '                               Sheet.ActiveColumnIndex).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

            '        Me.spdParts_Sheet1.Cells(Sheet.ActiveRowIndex, selCol, Sheet.ActiveRowIndex, _
            '                               Sheet.ActiveColumnIndex).ForeColor = Color.Blue
            '    End If
            'End If

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
                        ''コピー
                        'sheet.ClipboardCopy()

                        ' 選択範囲を取得
                        Dim cr As FarPoint.Win.Spread.Model.CellRange() = spdParts.ActiveSheet.GetSelections()
                        If cr.Length > 0 Then
                            '2012/02/16挿入貼り付けの判定'
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
                                        '2012/01/28'
                                        'count = count + 1
                                        'data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                        If cr(0).Column = -1 AndAlso cr(0).ColumnCount = -1 Then
                                            ''
                                            count = count + 1
                                            data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, 0, 1, spdParts.ActiveSheet.ColumnCount)
                                        Else
                                            count = count + 1
                                            '2013/05/14 １列で空白行を含む複数行選択した時、貼り付けると詰まってしまう現象回避する。
                                            If spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount) = "" Then
                                                data += "  " + vbCrLf  'ブランクの場合には改行をセットする。
                                            Else
                                                data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                            End If
                                        End If

                                        'If count < 1 Then
                                        '    count = count + 1
                                        '    data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)

                                        'Else
                                        '    '2012/01/27 複数コピーを可能にする'
                                        '    count = count + 1
                                        '    data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                        '    'MsgBox("複数の行に渡ってのコピーをすることはできません")
                                        '    'SetUndoCellFormat(sheet, listBln)
                                        '    'Return
                                        'End If
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
                    '20110604 樺澤 コピーする範囲と削除する範囲がまだ不完全'

                    '2012/02/21 協議の結果一時封印'

                    ''情報列タイトルの色をチェック
                    'For i As Integer = 0 To sheet.ColumnCount - 1
                    '    '青色か？
                    '    If sheet.Cells(0, i, 2, i).ForeColor = Color.Blue Then
                    '        'Exit Sub
                    '    End If
                    'Next

                    ''コントロールキーとXキーが押された
                    'If (e.Modifiers And Keys.Control) = Keys.Control Then

                    '    '書式バックアップ
                    '    Dim listBln As List(Of Boolean()) = GetEditCellInfo(sheet)
                    '    '書式を一時的に全て保存編集対象にする
                    '    SetUndoCellFormat(sheet)
                    '    ''コピー
                    '    'sheet.ClipboardCopy()

                    '    ' 選択範囲を取得
                    '    Dim cr As FarPoint.Win.Spread.Model.CellRange() = spdParts.ActiveSheet.GetSelections()
                    '    If cr.Length > 0 Then
                    '        '2012/02/16挿入貼り付けの判定'
                    '        copyRowCount = selection.RowCount
                    '        copyColumn = selection.Column
                    '        Dim data As [String] = Nothing
                    '        If cr(0).Row = -1 Then
                    '            ' 列単位で選択されている場合
                    '            For i As Integer = 0 To spdParts.ActiveSheet.ColumnCount - 1
                    '                If spdParts.ActiveSheet.GetRowVisible(i) = True Then
                    '                    data += spdParts.ActiveSheet.GetClipValue(cr(0).Row, i, cr(0).Row, i)
                    '                    spdParts_Sheet1.ClearRange(cr(0).Row, cr(0).Column + i, 1, 1, True)
                    '                End If
                    '            Next
                    '        Else
                    '            Dim count As Integer = 0
                    '            ' セル単位で選択されている場合
                    '            'If cr(0).RowCount > 1 Then
                    '            '    MsgBox("複数の行に渡っての切り取りをすることはできません")
                    '            '    SetUndoCellFormat(sheet, listBln)
                    '            '    Return
                    '            'End If


                    '            For i As Integer = 0 To cr(0).RowCount - 1

                    '                If spdParts.ActiveSheet.GetRowVisible(cr(0).Row + i) = True Then
                    '                    data += spdParts.ActiveSheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                    '                    '5/18 樺澤
                    '                    '対象セルの中身を削除
                    '                    spdParts_Sheet1.ClearRange(cr(0).Row + i, cr(0).Column, 1, 1, True)
                    '                    koseiSubject.ClearRow(cr(0).Row + i, 1)
                    '                End If
                    '            Next
                    '        End If

                    '        ' クリップボードに設定します
                    '        Clipboard.SetData(DataFormats.Text, data)
                    '    End If

                    '    '書式を戻す
                    '    SetUndoCellFormat(sheet, listBln)

                    'End If

                Case Keys.V
                    '
                    '   コンテキストメニューの貼り付け処理でコールするサブルーチンをここで呼ぶ
                    '
                    'OptionPaste()

                    '情報列タイトルの色をチェック
                    For i As Integer = 0 To sheet.ColumnCount - 1
                        '青色か？
                        If sheet.Cells(0, i, 2, i).ForeColor = Color.Blue Then
                            Exit Sub
                        End If
                    Next

                    '行選択ではコントロールキーとVキーは無効に
                    'If selection.Column = -1 AndAlso selection.ColumnCount - 1 Then
                    '    e.Handled = True
                    'Else
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


                                'EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けようとしている範囲がスプレッド表の最大列を超えています")
                                'Return

                            End If


                            '2012/02/01'
                            'スプレッド自身に貼り付けさせる'
                            Dim im As New InputMap
                            spdParts.ClipboardOptions = ClipboardOptions.NoHeaders
                            im.Put(New Keystroke(Keys.V, Keys.Control), SpreadActions.ClipboardPasteValues)

                            '
                            '   上記方法でないと、アンドゥができなくなる
                            '




                            '１行コピーに対して複数行貼り付けができない'
                            '単一コピーの場合'
                            'If rowCount = 1 Then
                            '    For col As Integer = 0 To selColumnCount - 1
                            '        For rowindex As Integer = 0 To selection.RowCount - 1
                            '            If Not Me.spdParts_Sheet1.Cells(selection.Row + rowindex, selCol + col).Locked Then
                            '                '隠された行にはペーストしない'
                            '                If Me.spdParts_Sheet1.Rows(selection.Row + rowindex).Visible Then
                            '                    Me.spdParts_Sheet1.Cells(selection.Row + rowindex, selCol + col).Value = listClip(0)(0)
                            '                End If
                            '            End If
                            '        Next
                            '    Next
                            'ElseIf rowCount > 1 Then
                            '    '複数コピーの場合'
                            '    For col As Integer = 0 To selColumnCount - 1
                            '        For rowindex As Integer = 0 To selection.RowCount - 1

                            '            If Not Me.spdParts_Sheet1.Cells(selection.Row + rowindex, selCol + col).Locked Then
                            '                '隠された行にはペーストしない'
                            '                If Me.spdParts_Sheet1.Rows(selection.Row + rowindex).Visible Then
                            '                    Me.spdParts_Sheet1.Cells(selection.Row + rowindex, selCol + col).Value = listClip(rowindex)(col)
                            '                Else
                            '                    '非表示なら'
                            '                End If
                            '            End If
                            '        Next
                            '    Next
                            'End If

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

                                        'EBom.Common.ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                        'Return
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

                    'End If

                Case Keys.Delete
                    '行選択ではDeleteは無効に
                    If Not selection Is Nothing Then
                        If selection.Column = -1 AndAlso selection.ColumnCount - 1 Then
                            e.Handled = True
                        End If
                        '2012/03/08'
                        'フィルタが掛かってるところは削除対象外'
                        Dim row As Integer = selection.Row
                        Dim col As Integer = selection.Column
                        Dim colcount As Integer = selection.ColumnCount
                        Dim rowcount As Integer = selection.RowCount


                        For rowindex As Integer = row To row + rowcount - 1
                            'フィルタ中の行は対象外'
                            If sheet.Rows(rowindex).Visible Then
                                ''レベル０行も対象外'
                                'If koseiSubject.Level(rowindex - 4) <> 0 Then

                                '2014/04/18 kabasawa'
                                'subjectの行向けにここを通っているのなら'
                                '-4の結果が0以上、つまり４行目からでないとここは通る必要はない'
                                'subject上の0行目＝シート上の４行目'

                                '
                                '   削除されてもInputFlgは削除しない！！部品Noの変化の時だけ削除
                                '
                                'If rowindex > 3 And col = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_BUHIN_NO).Index Then
                                '    'InputFlgもクリアする。
                                '    koseiSubject.InputFlg(rowindex - 4) = ""
                                'End If

                                'タイトル行も対象外'
                                If row > 3 Then
                                    For colindex As Integer = col To col + colcount - 1
                                        sheet.ClearRange(rowindex, colindex, 1, 1, True)
                                        'koseiSubject.ClearCell(row, col)
                                        '------------------------------------------------------------------------
                                        '2012/07/25　柳沼
                                        '   フィルタで非表示行の値がクリアされてしまうので
                                        '   無効にする。
                                        e.Handled = True
                                        '------------------------------------------------------------------------
                                    Next
                                End If
                            End If
                            'End If
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
                                'If koseiSubject.Level(rowindex - 4) <> 0 Then

                                'InputFlgもクリアする。
                                koseiSubject.InputFlg(rowindex - 4) = ""

                                'タイトル行も対象外'
                                If row > 3 Then
                                    For colindex As Integer = col To col + colcount - 1
                                        sheet.ClearRange(rowindex, colindex, 1, 1, True)
                                        'koseiSubject.ClearCell(row, col)
                                    Next
                                End If
                            End If
                            'End If
                        Next
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

                'Console.WriteLine(CType(iData.GetData(DataFormats.Text), String))
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

            '2012/03/12 selectionが無い場合があるので'
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
            'If fm.ResultOk = False Then
            '    Exit Sub
            'End If
            '状態を受け取る'
            Dim Conditions1 As String = fms.ComboBox1.Text
            'Dim Conditions2 As String = fms.ComboBox2.Text
            'Dim Conditions3 As String = fms.ComboBox3.Text
            Dim order1 As Boolean = fms.RadioButton1.Checked
            'Dim order2 As Boolean = fms.RadioButton3.Checked
            'Dim order3 As Boolean = fms.RadioButton5.Checked

            '状態を渡してソート処理'
            koseiSubject.IsViewerMode = True
            koseiSubject.SortMatrix(Conditions1, order1)
            'koseiSubject.SortMatrix(Conditions1, order1, Conditions2, order2, Conditions3, order3)
            koseiSubject.IsViewerMode = False
            koseiSubject.NotifyObservers()

        End Sub
#End Region

        Private Sub 子部品展開ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 子部品展開ToolStripMenuItem.Click
            '子部品を展開する。
            Dim sheet As Spread.SheetView = spdParts.ActiveSheet

            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            Dim wBuhinNo As String = sheet.Cells(ParaActRowIdx, ParaActColIdx).Value

            'タイトル行を取得
            Dim titleRows As Integer = BuhinEditSpreadUtil.GetTitleRows(sheet)

            '部品構成情報を表示
            koseiSubject.BuhinNoBom(sheet.ActiveRowIndex - titleRows) = wBuhinNo

            'シートの変更後セルのフォントをデフォルトに戻してから、
            '部品構成情報を挿入した行の背景色を変更
            koseiObserver.ClearSheetBackColor()
            koseiObserver.UpdateBackColor(HoyouBuhinSpdKoseiObserver.SPREAD_ROW + titleRows - 1, HoyouBuhinSpdKoseiObserver.SPREAD_ROWCOUNT)

        End Sub

        Private Sub spdParts_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdParts.EditModeOn
            Dim sheet As Spread.SheetView = spdParts_Sheet1
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            '
            '   編集前の値をセルのTAGにキープ
            '   S.Tokizaki
            '
            With sheet.Cells(ParaActRowIdx, ParaActColIdx)
                .Tag = .Text
            End With



            '↓↓↓2014/12/25 メタル項目を追加 TES)張 CHG BEGIN
            'If ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MAKER_NAME).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_1).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_2).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_3).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_4).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_5).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_6).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_7).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_8).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_9).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_10).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_1).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_2).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_3).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_4).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_5).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_6).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_7).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_8).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_DOOR_TRIM_1).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_DOOR_TRIM_2).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_ROOF_TRIM_1).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_ROOF_TRIM_2).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SUNROOF_TRIM_1).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_BIKOU).Index _
            '  Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_BUHIN_NOTE).Index Then
            If ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MAKER_NAME).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_1).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_2).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_3).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_4).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_5).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_6).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_7).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_8).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_9).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_10).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_1).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_2).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_3).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_4).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_5).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_6).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_7).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SHEET_8).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_DOOR_TRIM_1).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_DOOR_TRIM_2).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_ROOF_TRIM_1).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_ROOF_TRIM_2).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_MEMO_SUNROOF_TRIM_1).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_BIKOU).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_BUHIN_NOTE).Index _
              Or ParaActColIdx = sheet.Columns(HoyouBuhinSpdKoseiObserver.TAG_DATA_ITEM_KAITEI_INFO).Index Then
                '↑↑↑2014/12/25 メタル項目を追加 TES)張 CHG END
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

            koseiObserver.Change_EditInsuu(spdParts.ActiveSheet.ActiveCell)

        End Sub
        ''' <summary>
        ''' スプレッドの表示を制御する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub viewLockForViewMode()

            ''コントロールをロックする
            '2013/05/14 便利ツールは使用できる。（ＡＬ画面では使用できるので合わせる。）
            '           但し、コピー＆ペーストは使用できない。
            Me.ToolStrip1.Enabled = True
            Me.コピーCToolStripButton.Enabled = False
            Me.貼り付けPToolStripButton.Enabled = False

            msBuhinKouseiIchiran.Enabled = False
            Me.ToolStripMenuItem.Enabled = True
            Me.msBuhinKouseiIchiran.Enabled = True
            Me.msBuhinKouseiYobidashi.Enabled = False
            Me.msKyoukuA.Enabled = False

            'Me.BtnKoseiTenkai.Enabled = False
            Me.BtnOldBuhin.Enabled = False
            Me.TxtInputSupport.Enabled = False
            Me.btnKouseiBuhin.Enabled = False

            For i As Integer = 0 To spdParts_Sheet1.RowCount - 1
                For j As Integer = 0 To spdParts_Sheet1.ColumnCount - 1
                    spdParts_Sheet1.Cells(i, j).Locked = True
                Next
            Next
        End Sub

        ''' <summary>
        ''' 供給セクションを振る
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub msKyoukuA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msKyoukuA.Click

            Dim result As DialogResult = MsgBox("集計コードAに対して供給セクション「9SX00」を振ります。よろしいですか？", MsgBoxStyle.OkCancel)

            If result = 1 Then
                koseiSubject.setKyouku()
                koseiSubject.NotifyObservers()
            End If


        End Sub

        Private copyRowCount As Integer
        Private copyColumn As Integer
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

        Private Sub btnKouseiBuhin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKouseiBuhin.Click

            'すでに画面が存在するか確認
            If _frmDispShisakuBuhinEdit00.ChkFrm("HoyouBuhinFrm41DispOldBuhinSelector") _
            Or _frmDispShisakuBuhinEdit00.ChkFrm("HoyouBuhinFrm00Kakunin") _
            Or _frmDispShisakuBuhinEdit00.ChkFrm("Frm41KouseiBuhinSelector") Then
                Return
            End If

            Dim myResult As New HoyouBuhinFrm41KouseiBuhinSelector.EditDialogResult

            Dim frmKouseiBuhin As New HoyouBuhinFrm41KouseiBuhinSelector(hoyouEventCode, hoyouBukaCode, hoyouTanto, koseiSubject, Me)
            'frmKouseiBuhin.Show()

            'ダイアログを開いて、実行結果を受け取る
            myResult = frmKouseiBuhin.Show(Me)

        End Sub

        Private Sub spdParts_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles spdParts.MouseMove
            Dim range As FarPoint.Win.Spread.Model.CellRange = spdParts.GetCellFromPixel(0, 0, e.X, e.Y)
            Dim col As Spread.Column = spdParts_Sheet1.Columns(range.Column)

            '2013/05/20　Windows7対応
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

        Private Sub BtnOldBuhin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOldBuhin.Click

            ''↓↓2014/08/11 Ⅰ.3.設計編集 ベース車改修専用化_bu) (TES)張 CHG BEGIN
            ''すでに画面が存在するか確認
            'If _frmDispShisakuBuhinEdit00.ChkFrm("Frm41DispOldBuhinSelector") _
            'Or _frmDispShisakuBuhinEdit00.ChkFrm("frm00Kakunin") _
            'Or _frmDispShisakuBuhinEdit00.ChkFrm("Frm41KouseiBuhinSelector") Then
            '    Return
            'End If

            'Dim myResult As New Frm41DispOldBuhinSelector.EditDialogResult
            'Dim frmKouseiBuhin As New Frm41DispOldBuhinSelector(hoyouEventCode, hoyouBukaCode, hoyouTanto, koseiSubject, Me)

            ''ダイアログを開いて、実行結果を受け取る
            'myResult = frmKouseiBuhin.Show(Me)

            Dim selectedStatus As String = String.Empty
            Dim textMoto As String = String.Empty
            Dim textSaki As String = String.Empty

            Using frm41HikakuSource As New HoyouBuhinFrm41HikakuSourceSelector(koseiSubject)
                frm41HikakuSource.ShowDialog()

                If frm41HikakuSource.ResultOk = False Then
                    Exit Sub
                Else
                    '比較元選択状態を取得
                    selectedStatus = frm41HikakuSource.SelectedStatus
                    '比較元テキスト内容を取得
                    textMoto = frm41HikakuSource.MotoText
                    '比較先テキスト内容を取得
                    textSaki = frm41HikakuSource.SakiText
                End If
            End Using

            '補用部品設計担当情報
            Dim tantoVo As New THoyouSekkeiTantoVo
            tantoVo.HoyouEventCode = hoyouEventCode
            tantoVo.HoyouBukaCode = hoyouBukaCode
            tantoVo.HoyouTanto = hoyouTanto
            tantoVo.HoyouTantoKaiteiNo = ""

            Dim motoBuhinKosei As New HoyouBuhinBuhinKoseiMatrix
            Dim sakiBuhinKosei As New HoyouBuhinBuhinKoseiMatrix

            '↓↓2014/10/13 酒井 ADD BEGIN
            Dim orgBuhinKosei As New HoyouBuhinBuhinKoseiMatrix
            orgBuhinKosei = koseiSubject.Matrix.Copy
            '↑↑2014/10/13 酒井 ADD END

            '比較元選択状態が「ベース部品表」の場合
            If selectedStatus = 0 Then
                'HoyouBuhinFrm41DispShisakuBuhinEdit20画面表示構成を取得
                motoBuhinKosei = koseiSubject.Matrix
            End If

            '比較先テキスト内容の部品番号でEBOM部品構成を取得
            Dim aDetectLatesStructure As New HoyouBuhinDetectLatestStructureImpl(tantoVo)

            '比較元選択状態が「EBOM」の場合
            If selectedStatus = 1 Then
                motoBuhinKosei = koseiSubject.GetNewKoseiMatrix(aDetectLatesStructure.Compute(textMoto, "", True, ""))
                ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                If _frmDispShisakuBuhinEdit00.JikyuFlg = False Then
                    motoBuhinKosei.RemoveJikyu()
                End If
                ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
            End If

            sakiBuhinKosei = koseiSubject.GetNewKoseiMatrix(aDetectLatesStructure.Compute(textSaki, "", True, ""))
            ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
            If _frmDispShisakuBuhinEdit00.JikyuFlg = False Then
                sakiBuhinKosei.RemoveJikyu()
            End If
            ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

            Dim ResultSet As New List(Of HoyouBuhinfrm41HikakuResultSelectorVo)
            Dim selectorVo As HoyouBuhinfrm41HikakuResultSelectorVo


            '比較元構成と比較先構成を比較し、比較結果にセット
            For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                Dim sakiVo As HoyouBuhinBuhinKoseiRecordVo = sakiBuhinKosei.Record(sakiIndex)
                Dim sakiInsuCount As Integer = 0
                'FINALの場合
                If CheckFinalBuhin(sakiVo.BuhinNo) Then
                    ''↓↓2014/09/15 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                    sakiBuhinKosei.Record(sakiIndex).InsuSuryo = "1"
                    ''↑↑2014/09/15 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                    Continue For
                End If
                If sakiVo.BuhinNo Is Nothing Or String.IsNullOrEmpty(sakiVo.BuhinNo) Then
                    Continue For
                End If

                For Each columnIndex As Integer In sakiBuhinKosei.GetInputInsuColumnIndexes
                    If sakiBuhinKosei.InsuSuryo(sakiIndex, columnIndex) > 0 Then
                        sakiInsuCount = sakiInsuCount + sakiBuhinKosei.InsuSuryo(sakiIndex, columnIndex)
                    End If
                Next
                ''↓↓2014/09/15 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                sakiBuhinKosei.Record(sakiIndex).InsuSuryo = sakiInsuCount
                ''↑↑2014/09/15 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

                Dim contain As Boolean = False
                For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                    Dim motoVo As HoyouBuhinBuhinKoseiRecordVo = motoBuhinKosei.Record(motoIndex)
                    Dim motoInsuCount As Integer = 0
                    'FINALの場合
                    If CheckFinalBuhin(motoVo.BuhinNo) Then
                        ''↓↓2014/09/15 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                        motoBuhinKosei.Record(motoIndex).InsuSuryo = "1"
                        ''↑↑2014/09/15 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                        Continue For
                    End If
                    If motoVo.BuhinNo Is Nothing Or String.IsNullOrEmpty(motoVo.BuhinNo) Then
                        Continue For
                    End If
                    ''↓↓2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                    '比較元選択状態が「ベース部品表」の場合
                    '比較元選択状態が「ベース部品表」の場合、員数が入っている項目が異なるため、以下の判定を追加した。
                    If Not selectedStatus = 0 Then
                        ''↑↑2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                        For Each columnIndex As Integer In motoBuhinKosei.GetInputInsuColumnIndexes
                            If motoBuhinKosei.InsuSuryo(motoIndex, columnIndex) > 0 Then
                                motoInsuCount = motoInsuCount + motoBuhinKosei.InsuSuryo(motoIndex, columnIndex)
                            End If
                        Next
                        ''↓↓2014/09/15 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                        motoBuhinKosei.Record(motoIndex).InsuSuryo = motoInsuCount
                        ''↑↑2014/09/15 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

                        ''↓↓2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                    End If
                    ''↑↑2014/09/19 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                    If StringUtil.Equals(sakiVo.BuhinNo, motoVo.BuhinNo) Then
                        contain = True
                        Exit For
                    End If
                Next
                '比較元構成に存在しない
                If contain = False Then
                    selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                    selectorVo.HoyouBuhinBuhinKoseiRecordVo = sakiVo
                    selectorVo.Insu = sakiInsuCount
                    selectorVo.Flag = "A"
                    selectorVo.Kubun = "比較先"
                    selectorVo.MotoGamen = "HoyouBuhinFrm41DispShisakuBuhinEdit20"
                    ResultSet.Add(selectorVo)
                End If
            Next
            '比較元構成と比較先構成を比較し、比較結果にセット
            For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                Dim motoVo As HoyouBuhinBuhinKoseiRecordVo = motoBuhinKosei.Record(motoIndex)
                Dim motoInsuCount As Integer = 0
                'FINALの場合
                If CheckFinalBuhin(motoVo.BuhinNo) Then
                    Continue For
                End If
                If motoVo.BuhinNo Is Nothing Or String.IsNullOrEmpty(motoVo.BuhinNo) Then
                    Continue For
                End If

                '比較元選択状態が「ベース部品表」の場合
                ''↓↓2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                If selectedStatus = 0 Then
                    '↓↓2014/10/22 酒井 ADD BEGIN
                    'motoInsuCount = koseiSubject.InsuSuryo(motoIndex)
                    If koseiSubject.InsuSuryo(motoIndex) = "**" Then
                        motoInsuCount = -1
                    Else
                        motoInsuCount = koseiSubject.InsuSuryo(motoIndex)
                    End If
                    '↑↑2014/10/22 酒井 ADD END
                Else
                    ''↑↑2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                    For Each columnIndex As Integer In motoBuhinKosei.GetInputInsuColumnIndexes
                        If motoBuhinKosei.InsuSuryo(motoIndex, columnIndex) > 0 Then
                            motoInsuCount = motoInsuCount + motoBuhinKosei.InsuSuryo(motoIndex, columnIndex)
                        End If
                    Next
                    ''↓↓2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                End If
                ''↑↑2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

                ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                Dim motoFlg As Boolean = False
                ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

                Dim contain As Boolean = False
                For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                    Dim sakiVo As HoyouBuhinBuhinKoseiRecordVo = sakiBuhinKosei.Record(sakiIndex)
                    Dim sakiInsuCount As Integer = 0
                    'FINALの場合
                    If CheckFinalBuhin(sakiVo.BuhinNo) Then
                        Continue For
                    End If
                    If sakiVo.BuhinNo Is Nothing Or String.IsNullOrEmpty(sakiVo.BuhinNo) Then
                        Continue For
                    End If

                    For Each columnIndex As Integer In sakiBuhinKosei.GetInputInsuColumnIndexes
                        If sakiBuhinKosei.InsuSuryo(sakiIndex, columnIndex) > 0 Then
                            sakiInsuCount = sakiInsuCount + sakiBuhinKosei.InsuSuryo(sakiIndex, columnIndex)
                        End If
                    Next

                    '比較元構成．部品番号＝比較先構成．部品番号の場合
                    If StringUtil.Equals(motoVo.BuhinNo, sakiVo.BuhinNo) Then
                        contain = True
                        ''↓↓2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                        'If motoInsuCount = sakiInsuCount And _
                        'motoVo.Level = sakiVo.Level And _
                        'StringUtil.Equals(motoVo.ShukeiCode, sakiVo.ShukeiCode) And _
                        'StringUtil.Equals(motoVo.SiaShukeiCode, sakiVo.SiaShukeiCode) And _
                        'StringUtil.Equals(motoVo.GencyoCkdKbn, sakiVo.GencyoCkdKbn) And _
                        'StringUtil.Equals(motoVo.MakerCode, sakiVo.MakerCode) And _
                        'StringUtil.Equals(motoVo.BuhinNoKbn, sakiVo.BuhinNoKbn) And _
                        'StringUtil.Equals(motoVo.BuhinNoKaiteiNo, sakiVo.BuhinNoKaiteiNo) And _
                        'StringUtil.Equals(motoVo.EdaBan, sakiVo.EdaBan) Then
                        If motoInsuCount = sakiInsuCount And _
                           motoVo.Level = sakiVo.Level And _
                           StringUtil.Equals(StringUtil.Nvl(motoVo.ShukeiCode), StringUtil.Nvl(sakiVo.ShukeiCode)) And _
                           StringUtil.Equals(StringUtil.Nvl(motoVo.SiaShukeiCode), StringUtil.Nvl(sakiVo.SiaShukeiCode)) And _
                           StringUtil.Equals(StringUtil.Nvl(motoVo.GencyoCkdKbn), StringUtil.Nvl(sakiVo.GencyoCkdKbn)) And _
                           StringUtil.Equals(StringUtil.Nvl(motoVo.MakerCode), StringUtil.Nvl(sakiVo.MakerCode)) And _
                           StringUtil.Equals(StringUtil.Nvl(motoVo.BuhinNoKbn), StringUtil.Nvl(sakiVo.BuhinNoKbn)) And _
                           StringUtil.Equals(StringUtil.Nvl(motoVo.BuhinNoKaiteiNo), StringUtil.Nvl(sakiVo.BuhinNoKaiteiNo)) And _
                           StringUtil.Equals(StringUtil.Nvl(motoVo.EdaBan), StringUtil.Nvl(sakiVo.EdaBan)) Then
                            ''↑↑2014/09/09 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                            '完全一致の場合
                            selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                            selectorVo.HoyouBuhinBuhinKoseiRecordVo = sakiVo
                            selectorVo.Insu = sakiInsuCount
                            selectorVo.Flag = ""
                            selectorVo.Kubun = ""
                            selectorVo.MotoGamen = "HoyouBuhinFrm41DispShisakuBuhinEdit20"
                            ResultSet.Add(selectorVo)
                        Else
                            ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                            If Not motoFlg Then
                                selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                                selectorVo.HoyouBuhinBuhinKoseiRecordVo = motoVo
                                selectorVo.Insu = motoInsuCount
                                selectorVo.Flag = "C"
                                selectorVo.Kubun = "比較元"
                                selectorVo.MotoGamen = "HoyouBuhinFrm41DispShisakuBuhinEdit20"
                                ResultSet.Add(selectorVo)
                                motoFlg = True
                            End If
                            ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                            '部品番号以外で不一致の場合
                            selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                            selectorVo.HoyouBuhinBuhinKoseiRecordVo = sakiVo
                            selectorVo.Insu = sakiInsuCount
                            selectorVo.Flag = "C"
                            selectorVo.Kubun = "比較先"
                            selectorVo.MotoGamen = "HoyouBuhinFrm41DispShisakuBuhinEdit20"
                            ResultSet.Add(selectorVo)
                            ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                            'selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                            'selectorVo.HoyouBuhinBuhinKoseiRecordVo = motoVo
                            'selectorVo.Insu = motoInsuCount
                            'selectorVo.Flag = "C"
                            'selectorVo.Kubun = "比較元"
                            'selectorVo.MotoGamen = "HoyouBuhinFrm41DispShisakuBuhinEdit20"
                            'ResultSet.Add(selectorVo)
                            ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                        End If
                        ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                        'moto:saki=1:2の対応
                        'Exit For
                        ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                    End If
                Next
                '比較先構成に存在しない
                If contain = False Then
                    selectorVo = New HoyouBuhinfrm41HikakuResultSelectorVo
                    selectorVo.HoyouBuhinBuhinKoseiRecordVo = motoVo
                    selectorVo.Insu = motoInsuCount
                    selectorVo.Flag = "D"
                    selectorVo.Kubun = "比較元"
                    selectorVo.MotoGamen = "HoyouBuhinFrm41DispShisakuBuhinEdit20"
                    ResultSet.Add(selectorVo)
                End If
            Next
            '↓↓2014/10/22 酒井 ADD BEGIN
            'A→D→Cの順で画面表示する
            Dim ResultSetSort As New List(Of HoyouBuhinfrm41HikakuResultSelectorVo)
            For Each result As HoyouBuhinfrm41HikakuResultSelectorVo In ResultSet
                If result.Flag = "A" Then
                    ResultSetSort.Add(result)
                End If
            Next
            For Each result As HoyouBuhinfrm41HikakuResultSelectorVo In ResultSet
                If result.Flag = "D" Then
                    ResultSetSort.Add(result)
                End If
            Next
            For Each result As HoyouBuhinfrm41HikakuResultSelectorVo In ResultSet
                If result.Flag = "C" Then
                    ResultSetSort.Add(result)
                End If
            Next
            For Each result As HoyouBuhinfrm41HikakuResultSelectorVo In ResultSet
                If result.Flag <> "A" And result.Flag <> "D" And result.Flag <> "C" Then
                    ResultSetSort.Add(result)
                End If
            Next
            'Using frm41HikakuResult As New HoyouBuhinFrm41HikakuResultSelector(ResultSet)
            Using frm41HikakuResult As New HoyouBuhinFrm41HikakuResultSelector(ResultSetSort)
                '↑↑2014/10/22 酒井 ADD END

                frm41HikakuResult.ShowDialog()

                If frm41HikakuResult.ResultOk = False Then
                    Exit Sub
                End If

                For Each resultVo As HoyouBuhinfrm41HikakuResultSelectorVo In frm41HikakuResult.ResultVos
                    'チェックあり
                    If resultVo.CheckedKbn = True Then
                        If resultVo.Kubun = "比較元" Then
                            '比較元構成の該当部品のLEVEL=1
                            For Each motoVo As HoyouBuhinBuhinKoseiRecordVo In motoBuhinKosei.Records
                                ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                                'If motoVo.Equals(resultVo.HoyouBuhinBuhinKoseiRecordVo) Then
                                If StringUtil.Equals(motoVo.BuhinNo, resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNo) And _
                                    motoVo.InsuSuryo = resultVo.HoyouBuhinBuhinKoseiRecordVo.InsuSuryo And _
                                       motoVo.Level = resultVo.HoyouBuhinBuhinKoseiRecordVo.Level And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.ShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.ShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.SiaShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.SiaShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.GencyoCkdKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.GencyoCkdKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.MakerCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.MakerCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.BuhinNoKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.BuhinNoKaiteiNo), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKaiteiNo)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.EdaBan), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.EdaBan)) Then
                                    ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                                    motoVo.Level = 1
                                End If
                            Next
                        ElseIf resultVo.Kubun = "比較先" Then
                            '比較先構成の該当部品のLEVEL=1
                            For Each sakiVo As HoyouBuhinBuhinKoseiRecordVo In sakiBuhinKosei.Records
                                ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                                'If sakiVo.Equals(resultVo.HoyouBuhinBuhinKoseiRecordVo) Then
                                If StringUtil.Equals(sakiVo.BuhinNo, resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNo) And _
                                    sakiVo.InsuSuryo = resultVo.HoyouBuhinBuhinKoseiRecordVo.InsuSuryo And _
                                       sakiVo.Level = resultVo.HoyouBuhinBuhinKoseiRecordVo.Level And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiVo.ShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.ShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiVo.SiaShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.SiaShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiVo.GencyoCkdKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.GencyoCkdKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiVo.MakerCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.MakerCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiVo.BuhinNoKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiVo.BuhinNoKaiteiNo), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKaiteiNo)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiVo.EdaBan), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.EdaBan)) Then
                                    ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                                    sakiVo.Level = 1
                                End If
                            Next
                        Else
                            '比較元構成の該当部品のLEVEL=1
                            For Each motoVo As HoyouBuhinBuhinKoseiRecordVo In motoBuhinKosei.Records
                                ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                                'If motoVo.Equals(resultVo.HoyouBuhinBuhinKoseiRecordVo) Then
                                If StringUtil.Equals(motoVo.BuhinNo, resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNo) And _
                                    motoVo.InsuSuryo = resultVo.HoyouBuhinBuhinKoseiRecordVo.InsuSuryo And _
                                       motoVo.Level = resultVo.HoyouBuhinBuhinKoseiRecordVo.Level And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.ShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.ShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.SiaShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.SiaShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.GencyoCkdKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.GencyoCkdKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.MakerCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.MakerCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.BuhinNoKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.BuhinNoKaiteiNo), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKaiteiNo)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoVo.EdaBan), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.EdaBan)) Then
                                    ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                                    motoVo.Level = 1
                                End If
                            Next
                            '比較先構成の該当部品を削除(.removecolumn)
                            For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                                ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                                'If sakiBuhinKosei.Record(sakiIndex).Equals(resultVo.HoyouBuhinBuhinKoseiRecordVo) Then
                                If StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNo, resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNo) And _
                                    sakiBuhinKosei.Record(sakiIndex).InsuSuryo = resultVo.HoyouBuhinBuhinKoseiRecordVo.InsuSuryo And _
                                       sakiBuhinKosei.Record(sakiIndex).Level = resultVo.HoyouBuhinBuhinKoseiRecordVo.Level And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).ShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.ShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).SiaShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.SiaShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).GencyoCkdKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.GencyoCkdKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).MakerCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.MakerCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).BuhinNoKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).BuhinNoKaiteiNo), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKaiteiNo)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).EdaBan), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.EdaBan)) Then
                                    ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                                    sakiBuhinKosei.RemoveRow(sakiIndex)
                                End If
                            Next
                        End If
                    Else
                        If resultVo.Kubun = "比較元" Then
                            '比較元構成.removecolumn
                            For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                                ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                                'If motoBuhinKosei.Record(motoIndex).Equals(resultVo.HoyouBuhinBuhinKoseiRecordVo) Then
                                If StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNo, resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNo) And _
                                    motoBuhinKosei.Record(motoIndex).InsuSuryo = resultVo.HoyouBuhinBuhinKoseiRecordVo.InsuSuryo And _
                                       motoBuhinKosei.Record(motoIndex).Level = resultVo.HoyouBuhinBuhinKoseiRecordVo.Level And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).ShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.ShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).SiaShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.SiaShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).GencyoCkdKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.GencyoCkdKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).MakerCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.MakerCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).BuhinNoKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).BuhinNoKaiteiNo), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKaiteiNo)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).EdaBan), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.EdaBan)) Then
                                    ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                                    motoBuhinKosei.RemoveRow(motoIndex)
                                End If
                            Next
                        ElseIf resultVo.Kubun = "比較先" Then
                            '比較先構成.removecolumn
                            For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                                ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                                'If sakiBuhinKosei.Record(sakiIndex).Equals(resultVo.HoyouBuhinBuhinKoseiRecordVo) Then
                                If StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNo, resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNo) And _
                                    sakiBuhinKosei.Record(sakiIndex).InsuSuryo = resultVo.HoyouBuhinBuhinKoseiRecordVo.InsuSuryo And _
                                       sakiBuhinKosei.Record(sakiIndex).Level = resultVo.HoyouBuhinBuhinKoseiRecordVo.Level And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).ShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.ShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).SiaShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.SiaShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).GencyoCkdKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.GencyoCkdKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).MakerCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.MakerCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).BuhinNoKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).BuhinNoKaiteiNo), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKaiteiNo)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).EdaBan), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.EdaBan)) Then
                                    ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                                    sakiBuhinKosei.RemoveRow(sakiIndex)
                                End If
                            Next
                        Else
                            '比較元構成.removecolumn
                            For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                                ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                                'If motoBuhinKosei.Record(motoIndex).Equals(resultVo.HoyouBuhinBuhinKoseiRecordVo) Then
                                If StringUtil.Equals(motoBuhinKosei.Record(motoIndex).BuhinNo, resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNo) And _
                                    motoBuhinKosei.Record(motoIndex).InsuSuryo = resultVo.HoyouBuhinBuhinKoseiRecordVo.InsuSuryo And _
                                       motoBuhinKosei.Record(motoIndex).Level = resultVo.HoyouBuhinBuhinKoseiRecordVo.Level And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).ShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.ShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).SiaShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.SiaShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).GencyoCkdKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.GencyoCkdKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).MakerCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.MakerCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).BuhinNoKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).BuhinNoKaiteiNo), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKaiteiNo)) And _
                                       StringUtil.Equals(StringUtil.Nvl(motoBuhinKosei.Record(motoIndex).EdaBan), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.EdaBan)) Then
                                    ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                                    motoBuhinKosei.RemoveRow(motoIndex)
                                End If
                            Next
                            '比較先構成.removecolumn
                            For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                                ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                                'If sakiBuhinKosei.Record(sakiIndex).Equals(resultVo.HoyouBuhinBuhinKoseiRecordVo) Then
                                If StringUtil.Equals(sakiBuhinKosei.Record(sakiIndex).BuhinNo, resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNo) And _
                                    sakiBuhinKosei.Record(sakiIndex).InsuSuryo = resultVo.HoyouBuhinBuhinKoseiRecordVo.InsuSuryo And _
                                       sakiBuhinKosei.Record(sakiIndex).Level = resultVo.HoyouBuhinBuhinKoseiRecordVo.Level And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).ShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.ShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).SiaShukeiCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.SiaShukeiCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).GencyoCkdKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.GencyoCkdKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).MakerCode), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.MakerCode)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).BuhinNoKbn), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKbn)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).BuhinNoKaiteiNo), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.BuhinNoKaiteiNo)) And _
                                       StringUtil.Equals(StringUtil.Nvl(sakiBuhinKosei.Record(sakiIndex).EdaBan), StringUtil.Nvl(resultVo.HoyouBuhinBuhinKoseiRecordVo.EdaBan)) Then
                                    ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                                    sakiBuhinKosei.RemoveRow(sakiIndex)
                                End If
                            Next
                        End If
                    End If
                Next
                '比較先構成のLEVEL=0の部品を削除（.removecolumn）
                For Each sakiIndex As Integer In sakiBuhinKosei.GetInputRowIndexes()
                    If sakiBuhinKosei.Record(sakiIndex).Level = 0 Then
                        sakiBuhinKosei.RemoveRow(sakiIndex)
                    End If
                    If sakiBuhinKosei.Record(sakiIndex).BuhinNo Is Nothing Then
                        sakiBuhinKosei.RemoveRow(sakiIndex)
                    End If
                Next
                '比較元構成のLEVEL=0の部品を削除（.removecolumn）
                For Each motoIndex As Integer In motoBuhinKosei.GetInputRowIndexes()
                    If motoBuhinKosei.Record(motoIndex).Level = 0 Then
                        motoBuhinKosei.RemoveRow(motoIndex)
                    End If
                Next

                '呼出し元画面構成のLEVEL=0以外の部品を削除（.removecolumn）
                '↓↓2014/10/22 酒井 ADD BEGIN
                'For Each orgIndex As Integer In orgBuhinKosei.GetInputRowIndexes()
                For orgIndex As Integer = orgBuhinKosei.Records.Count - 1 To 0 Step -1
                    '↑↑2014/10/22 酒井 ADD END
                    If orgBuhinKosei.Record(orgIndex).Level <> 0 Then
                        orgBuhinKosei.RemoveRow(orgIndex)
                    End If
                Next
                '↑↑2014/10/13 酒井 ADD END
                '空行を削除
                Dim index As Integer = 0
                For Each record As HoyouBuhinBuhinKoseiRecordVo In sakiBuhinKosei.Records
                    If record.BuhinNo Is Nothing Then
                        sakiBuhinKosei.RemoveRow(index)
                    Else
                        index = index + 1
                    End If
                Next
                index = 0
                For Each record As HoyouBuhinBuhinKoseiRecordVo In motoBuhinKosei.Records
                    If record.BuhinNo Is Nothing Then
                        motoBuhinKosei.RemoveRow(index)
                    Else
                        index = index + 1
                    End If
                Next
                '↓↓2014/10/13 酒井 ADD BEGIN
                index = 0
                For Each record As HoyouBuhinBuhinKoseiRecordVo In orgBuhinKosei.Records
                    If record.BuhinNo Is Nothing Then
                        orgBuhinKosei.RemoveRow(index)
                    Else
                        index = index + 1
                    End If
                Next
                '↑↑2014/10/13 酒井 ADD END

                '比較元構成に比較先構成を追加(.insert)
                '↓↓2014/10/13 酒井 ADD BEGIN
                'motoBuhinKosei.Insert(motoBuhinKosei.GetNewRowIndex, sakiBuhinKosei)
                'ConfirmNewMatrix(motoBuhinKosei)
                orgBuhinKosei.Insert(orgBuhinKosei.GetNewRowIndex, motoBuhinKosei)
                orgBuhinKosei.Insert(orgBuhinKosei.GetNewRowIndex, sakiBuhinKosei)
                ConfirmNewMatrix(orgBuhinKosei)
                '↑↑2014/10/13 酒井 ADD END
            End Using
            ''↑↑2014/08/11 Ⅰ.3.設計編集 ベース車改修専用化_bu) (TES)張 CHG END

        End Sub

        ''↓↓2014/08/22 Ⅰ.3.設計編集 ベース車改修専用化_bu) (TES)張 AND BEGIN
        ''' <summary>
        ''' Final品番とするかどうかを判断
        ''' </summary>
        ''' <param name="buhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CheckFinalBuhin(ByVal buhinNo As String) As Boolean

            If buhinNo Is Nothing Or String.IsNullOrEmpty(buhinNo) = True Then
                Return False
            End If

            Dim result As Rhac2210Vo = koseiSubject.GetFinalBuhinNo(buhinNo)
            Return result IsNot Nothing

        End Function
        ''↑↑2014/08/22 Ⅰ.3.設計編集 ベース車改修専用化_bu) (TES)張 AND END

#Region "過去部品取込機能(確認フォームクローズ用クラス)"

        ''' <summary>
        ''' 確認画面表示
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub OpenShow()
            '使用不可にする
            'HoyouBuhinFrm41DispShisakuBuhinEdit00()
            _frmDispShisakuBuhinEdit00.cmbTanto.Enabled = False
            _frmDispShisakuBuhinEdit00.BtnCall.Enabled = False
            _frmDispShisakuBuhinEdit00.btnView.Enabled = False
            _frmDispShisakuBuhinEdit00.BtnRegister.Enabled = False
            _frmDispShisakuBuhinEdit00.BtnSave.Enabled = False
            _frmDispShisakuBuhinEdit00.btnExcelExport.Enabled = False

            'HoyouBuhinFrm41DispShisakuBuhinEdit20()
            Me.BtnOldBuhin.Enabled = False
            Me.btnKouseiBuhin.Enabled = False

            'クローズ処理イベント取得用クラス
            Dim closer As HoyouBuhinFrm00Kakunin.IFormCloser = New CopyFormCloser(koseiObserver, _frmDispShisakuBuhinEdit00, Me)

            '取込内容確認フォームを表示
            HoyouBuhinFrm00Kakunin.ConfirmShow("過去の部品情報の確認", "部品編集情報を確認してください。", _
                                           "「置き換え」ますか？", "「置き換え」", "元に戻す", closer, Me)
        End Sub

        ''' <summary>
        ''' 確認フォームクローズ用
        ''' </summary>
        ''' <remarks></remarks>
        Private Class CopyFormCloser : Implements HoyouBuhinFrm00Kakunin.IFormCloser
            Private _koseiObserver As HoyouBuhinSpdKoseiObserver = Nothing
            Private _frmDispShisakuBuhinEdit00 As HoyouBuhinFrm41DispShisakuBuhinEdit00
            Private _frmDispShisakuBuhinEdit20 As HoyouBuhinFrm41DispShisakuBuhinEdit20

            Public Sub New(ByVal akoseiObserver As HoyouBuhinSpdKoseiObserver, _
                           ByVal frm41Disp00 As HoyouBuhinFrm41DispShisakuBuhinEdit00, _
                           ByVal frm41Disp20 As HoyouBuhinFrm41DispShisakuBuhinEdit20)
                _koseiObserver = akoseiObserver
                _frmDispShisakuBuhinEdit00 = frm41Disp00
                _frmDispShisakuBuhinEdit20 = frm41Disp20
            End Sub
            ''' <summary>
            ''' フォームを閉じる時の処理
            ''' </summary>
            ''' <param name="IsOk">OKが押された場合、true</param>
            ''' <remarks></remarks>
            Public Sub FormClose(ByVal IsOk As Boolean) Implements HoyouBuhinFrm00Kakunin.IFormCloser.FormClose

                If IsOk Then
                    'バックアップを削除
                    _koseiObserver.importBacupRemove()
                    'Return
                Else
                    'リストア処理
                    _koseiObserver.importRestore()
                End If

                '使用可にする
                'HoyouBuhinFrm41DispShisakuBuhinEdit00()
                _frmDispShisakuBuhinEdit00.cmbTanto.Enabled = True
                _frmDispShisakuBuhinEdit00.BtnCall.Enabled = True
                _frmDispShisakuBuhinEdit00.btnView.Enabled = True
                _frmDispShisakuBuhinEdit00.BtnRegister.Enabled = True
                _frmDispShisakuBuhinEdit00.BtnSave.Enabled = True
                _frmDispShisakuBuhinEdit00.btnExcelExport.Enabled = True

                'HoyouBuhinFrm41DispShisakuBuhinEdit20()
                _frmDispShisakuBuhinEdit20.BtnOldBuhin.Enabled = True
                _frmDispShisakuBuhinEdit20.btnKouseiBuhin.Enabled = True
            End Sub
        End Class

#End Region

        '全角文字ペースト判定処理
        Private Sub datamodel_Changed(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs) Handles datamodel.Changed
            Dim ByteLength As Integer

            '
            '   (1)ペースト時に、部品名称も全角チェックするように変更
            '   (2)対象の列を即値でなく、ヘッダーのタグを使用するように変更
            '   (3)セルの値をいちいち見ているので、変更
            '
            '   Changed by S.Tokizaki 2014.04.24
            '
            Select Case spdParts.ActiveSheet.Columns(e.Column).Tag

                Case HoyouBuhinSpdKoseiObserver.TAG_SHUKEI_CODE, _
                     HoyouBuhinSpdKoseiObserver.TAG_SIA_SHUKEI_CODE, _
                     HoyouBuhinSpdKoseiObserver.TAG_GENCYO_CKD_KBN, _
                     HoyouBuhinSpdKoseiObserver.TAG_MAKER_CODE, _
                     HoyouBuhinSpdKoseiObserver.TAG_BUHIN_NO, _
                     HoyouBuhinSpdKoseiObserver.TAG_BUHIN_NAME, _
                     HoyouBuhinSpdKoseiObserver.TAG_INSU_SURYO, _
                     HoyouBuhinSpdKoseiObserver.TAG_KYOUKU_SECTION

                    Dim Value As String = Me.spdParts.ActiveSheet.GetText(e.Row, e.Column)

                    If Trim(Value) <> "" Then
                        ByteLength = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Value)
                        If Len(Value) <> ByteLength Then
                            MsgBox("全角文字が含まれています。")
                            Me.spdParts.ActiveSheet.Cells(e.Row, e.Column).Text = ""
                        End If
                    End If

            End Select

            With spdParts.ActiveSheet
                If Not koseiObserver.ObserverUpdating AndAlso e.Column = 0 AndAlso StringUtil.IsNotEmpty(.GetValue(e.Row, e.Column)) AndAlso .GetValue(e.Row, e.Column).Equals("0") Then
                    EBom.Common.ComFunc.ShowErrMsgBox("レベルに0を設定することは出来ません。")
                    .SetValue(e.Row, e.Column, Nothing)
                    Exit Sub
                End If
            End With


        End Sub


        Private Sub ToolStripSheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSheet.Click
            If SHEET = "N" Then
                ToolStripSheet.Text = "SHEET非表示"
                ToolStripSheet.ToolTipText = "シート系メモを非表示にします"
                ToolStripSheet.Checked = True
                koseiObserver.SheetColumnVisible()
                SHEET = "D"
            Else
                ToolStripSheet.Text = "SHEET表示"
                ToolStripSheet.ToolTipText = "シート系メモを表示します"
                ToolStripSheet.Checked = False
                koseiObserver.SheetColumnDisable()
                SHEET = "N"
            End If

        End Sub
        Private Sub ToolStripDoor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDoor.Click
            If DOOR = "N" Then
                ToolStripDoor.Text = "DOOR非表示"
                ToolStripDoor.ToolTipText = "ドアトリム系メモを非表示にします"
                ToolStripDoor.Checked = True
                koseiObserver.DoorColumnVisible()
                DOOR = "D"
            Else
                ToolStripDoor.Text = "DOOR表示"
                ToolStripDoor.ToolTipText = "ドアトリム系メモを表示します"
                ToolStripDoor.Checked = False
                koseiObserver.DoorColumnDisable()
                DOOR = "N"
            End If
        End Sub
        Private Sub ToolStripRoof_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripRoof.Click
            If ROOF = "N" Then
                ToolStripRoof.Text = "ROOF非表示"
                ToolStripRoof.ToolTipText = "ルーフトリム系メモを非表示にします"
                ToolStripRoof.Checked = True
                koseiObserver.RoofColumnVisible()
                ROOF = "D"
            Else
                ToolStripRoof.Text = "ROOF表示"
                ToolStripRoof.ToolTipText = "ルーフトリム系メモを表示します"
                ToolStripRoof.Checked = False
                koseiObserver.RoofColumnDisable()
                ROOF = "N"
            End If
        End Sub
        Private Sub ToolStripSun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSun.Click
            If SUNROOF = "N" Then
                ToolStripSun.Text = "SUNROOF非表示"
                ToolStripSun.ToolTipText = "サンルーフ系メモを非表示にします"
                ToolStripSun.Checked = True
                koseiObserver.SunRoofColumnVisible()
                SUNROOF = "D"
            Else
                ToolStripSun.Text = "SUNROOF表示"
                ToolStripSun.ToolTipText = "サンルーフ系メモを表示します"
                ToolStripSun.Checked = False
                koseiObserver.SunRoofColumnDisable()
                SUNROOF = "N"
            End If
        End Sub

        ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化 (TES)張 ADD BEGIN
        Private Sub spdParts_ComboDropDown(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles spdParts.ComboDropDown
            Dim sheet As Spread.SheetView = spdParts_Sheet1

            Dim isKatashiyou As Boolean = e.Column = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU1).Index Or _
                    e.Column = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU2).Index Or _
                    e.Column = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU3).Index

            ' （型仕様1～3コンボ）のコンボドロップダウン表示イベント
            If isKatashiyou Then
                Dim katashiyou1 As String = sheet.Cells(e.Row, sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU1).Index).Value
                Dim katashiyou2 As String = sheet.Cells(e.Row, sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU2).Index).Value
                Dim katashiyou3 As String = sheet.Cells(e.Row, sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU3).Index).Value
                Dim dropCell = spdParts_Sheet1.Cells(e.Row, e.Column)
                Dim fc As FpCombo = e.EditingControl

                ' セル幅を全角１５文字程度に拡張する。
                'dropCell.Column.Width = LARGER_WIDTH
                fc.Width = LARGER_WIDTH

                ' 型仕様1～3の内、他で選択済みの項目を選択不可にする。
                Select Case e.Column
                    Case sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU1).Index
                        If Not String.IsNullOrEmpty(katashiyou2) Then
                            fc.ItemData.Remove(katashiyou2)
                            fc.List.Remove(katashiyou2)
                        End If
                        If Not String.IsNullOrEmpty(katashiyou3) Then
                            fc.ItemData.Remove(katashiyou3)
                            fc.List.Remove(katashiyou3)
                        End If

                    Case sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU2).Index
                        If Not String.IsNullOrEmpty(katashiyou1) Then
                            fc.ItemData.Remove(katashiyou1)
                            fc.List.Remove(katashiyou1)
                        End If
                        If Not String.IsNullOrEmpty(katashiyou3) Then
                            fc.ItemData.Remove(katashiyou3)
                            fc.List.Remove(katashiyou3)
                        End If
                    Case sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU3).Index
                        If Not String.IsNullOrEmpty(katashiyou1) Then
                            fc.ItemData.Remove(katashiyou1)
                            fc.List.Remove(katashiyou1)
                        End If
                        If Not String.IsNullOrEmpty(katashiyou2) Then
                            fc.ItemData.Remove(katashiyou2)
                            fc.List.Remove(katashiyou2)
                        End If
                    Case Else
                        ' なりえない
                End Select


            End If
        End Sub

        Private Sub spdParts_ComboCloseUp(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles spdParts.ComboCloseUp
            Dim sheet As Spread.SheetView = spdParts_Sheet1

            Dim isKatashiyou As Boolean = e.Column = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU1).Index Or _
                    e.Column = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU2).Index Or _
                    e.Column = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU3).Index

            ' 幅を元に戻る
            If isKatashiyou Then
                e.EditingControl.Width = sheet.Columns(SpdKoseiObserver.TAG_TSUKURIKATA_KATASHIYOU1).Width
            End If
        End Sub
        ''↑↑2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化 (TES)張 ADD END
    End Class




End Namespace


