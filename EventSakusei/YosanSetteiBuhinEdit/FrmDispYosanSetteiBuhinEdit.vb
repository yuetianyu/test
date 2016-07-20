Imports ShisakuCommon.Ui.Access
Imports ShisakuCommon.Db
Imports EBom.Common
Imports EBom.Excel
Imports ShisakuCommon.Util
Imports ShisakuCommon
Imports EventSakusei.EventEdit.Export2Excel
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Valid
Imports FarPoint.Win
Imports EventSakusei.YosanSetteiBuhinEdit.Dao
Imports EventSakusei.YosanSetteiBuhinEdit.Logic
Imports EventSakusei.NokiIkkatuSettei
Imports System.Text.RegularExpressions
Imports ShisakuCommon.Ui.Spd
Imports System.Text
Imports Microsoft.VisualBasic.FileIO
Imports EventSakusei.YosanSetteiBuhinEdit.Ui
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.CellType
Imports Microsoft.Office.Interop
Imports System.IO
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanSetteiBuhinEdit

#Region "手配帳編集クラス"
    ''' <summary>
    ''' 手配帳編集クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class frmDispYosanSetteiBuhinEdit
#Region "プライベート変数"
        '''<summary>初期化完了フラグ</summary>>
        Private _InitComplete As Boolean = False
        '''<summary>入力有無判定に使用</summary>>
        Private _aInputWatcher As InputWatcher
        ''' <summary>入力サポート欄とスプレッドの関係を定義</summary>
        Private _inputSupport As YosanSetteiBuhinEditInputSupport
        ''' <summary>フォーム表示時に呼び出されるID</summary>
        Private m_initSwitchId As String = String.Empty
        ''' <summary>画面制御ロジック</summary>
        Private _TehaiEditLogic As YosanSetteiBuhinEditLogic
        ''' <summary>マウスクリック行列位置保存</summary>
        Private _clickCellPosition As Spread.Model.CellRange
        ''' <summary>基本情報シート</summary>
        Private kihonSheet As SheetView
        ''' <summary>号車情報シート</summary>
        Private GousyaSheet As SheetView

        Private Const LARGER_WIDTH = 200
        Private _shisakuEventCode As String
        Private _shisakuListCode As String
        Private ZAI As String = "D"

        ''' <summary>貼り付けイベントはchangeイベントにかからないのでこれで取得</summary>
        Private WithEvents datamodel As FarPoint.Win.Spread.Model.DefaultSheetDataModel

#Region "スプレッド拡大縮小用 "

        'SPREADのサイズ変更用に使用する。
        '１は標準サイズ
        Private Kakudai1 As Decimal = 1
        Private Kakudai2 As Decimal = 1

#End Region

#End Region

#Region "コンストラクタ"

        'Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            Me._shisakuEventCode = shisakuEventCode
            Me._shisakuListCode = shisakuListCode

            Me.Refresh()
            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            '画面制御ロジック
            _TehaiEditLogic = New YosanSetteiBuhinEditLogic(shisakuEventCode, shisakuListCode, Me)

            '初期化メイン
            Initialize()
        End Sub

#End Region

#Region "プロパティ"
#Region "初期化完了確認"
        ''' <summary>
        ''' 初期化完了確認
        ''' 
        ''' 初期化正常実行でTRUEを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property InitComplete()
            Get
                Return _InitComplete
            End Get
        End Property
#End Region
#End Region

#Region "初期化"

#Region "初期化メイン"
        ''' <summary>
        ''' 初期化メイン
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Initialize()
            'Try

            Cursor.Current = Cursors.WaitCursor

            Dim sheet As FarPoint.Win.Spread.SheetView = spdBase_Sheet1
            NmSpdTagBase.initTagName(sheet)

            'ヘッダー部を初期化する
            _TehaiEditLogic.InitializeHeader()
            'スプレッダーを初期化する
            _TehaiEditLogic.InitializeSpread()

            '入力監視初期化
            InitializeWatcher()
            ' コンボボックス類初期化
            InitComboControl()
            '初期データ表示スプレッド(基本)
            DispSpreadBase()
            'スプレッドの読取専用列設定
            _TehaiEditLogic.LockColSpread()

            'マウス右クリック対処
            YosanSetteiBuhinEditSpreadUtil.AddEventCellRightClick(Me, spdBase, _inputSupport)

            'Enterキーの動作を、「編集」から「次行へ移動」にする
            ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyEnterIsNextRow(spdBase)

            'Shift + Enterキーの動作を、「前行へ移動」にする
            ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyShiftEnterIsPreviousRow(spdBase)

            '初期化完了
            _InitComplete = True

        End Sub

#End Region

#Region "スプレッドのCellType"

        Private KihonCellType As TextCellType

        Private Sub SetCellTypeKihon()
            kihonSheet = spdBase.Sheets(0)

            For index As Integer = 0 To kihonSheet.Columns.Count - 1
                kihonSheet.Columns(index).CellType = GetKihonCellType()
            Next

            '基本的に全角は入力禁止'
        End Sub

        ''' <summary>
        ''' INSTL品番Noセルを返す
        ''' </summary>
        ''' <returns>INSTL品番Noセル</returns>
        ''' <remarks></remarks>
        Public Function GetKihonCellType() As TextCellType
            If KihonCellType Is Nothing Then
                KihonCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                'KihonCellType.MaxLength = 3
            End If
            Return KihonCellType
        End Function

#End Region

#Region "コンボボックス類初期化"
        ''' <summary>
        ''' コンボボックス類初期化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitComboControl()

            'ヘッダー新規ブロックNoリストコンボ
            _TehaiEditLogic.InitCbNewBlockList()

            _TehaiEditLogic.AddCbTsukurikataSeisaku()
            _TehaiEditLogic.AddCbTsukurikataKatashiyou()
            _TehaiEditLogic.AddCbTsukurikataTigu()

        End Sub
#End Region

#Region "コントロール監視初期化"
        ''' <summary>
        ''' コントロール監視初期化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeWatcher()

            _inputSupport = New YosanSetteiBuhinEditInputSupport(txtInputSupport, spdBase)
            _aInputWatcher = New InputWatcher

            _aInputWatcher.Add(txtKoujiNo)
            _aInputWatcher.Add(spdBase)

            '両スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
            Dim imBase As New FarPoint.Win.Spread.InputMap
            imBase = spdBase.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imBase.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

            Dim imGousya As New FarPoint.Win.Spread.InputMap
            imGousya = spdBase.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imGousya.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

        End Sub
#End Region

#Region "スプレッド基本情報のデータ表示"
        ''' <summary>
        ''' スプレッド基本情報の表示
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DispSpreadBase()
            Try

                '基本情報表示ロジック
                If _TehaiEditLogic.SetSpreadBase() = False Then
                    Throw New Exception()
                End If

                '入力監視フラグクリア
                _aInputWatcher.Clear()

            Catch ex As Exception
                Dim msg As String
                msg = String.Format("基本情報のデータ表示で問題が発生しました(ERR={0})", ex.Message)
                ComFunc.ShowErrMsgBox(msg)
                'エラー発生でフォームを閉じる
                frm41SaiKakunin.Close()
                frm41SaiKakuninMin.Close()
                Me.Close()
            Finally

            End Try
        End Sub

#End Region

#End Region

#Region "イベント"

#Region "フォームロード"

        Private Sub frmDispYosanSetteiBuhinEdit_Activated1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        End Sub
        ''' <summary>
        ''' フォームロード
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub frmDispYosanSetteiBuhinEdit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            datamodel = CType(spdBase.ActiveSheet.Models.Data, FarPoint.Win.Spread.Model.DefaultSheetDataModel)
            Me.Focus()

        End Sub
#End Region

#Region "フォームDisposed"
        Private Sub frmDispYosanSetteiBuhinEdit_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed

            '手配帳ロジックを閉じる
            If Not _TehaiEditLogic Is Nothing Then
                _TehaiEditLogic.close()
            End If

            frm41SaiKakunin.Close()
            frm41SaiKakuninMin.Close()

        End Sub
#End Region

#Region "ボタン(カラー設定)"
        ''' <summary>
        ''' ボタン(カラー設定)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub frmDispYosanSetteiBuhinEdit_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCOLOR.Click

            Dim cr As FarPoint.Win.Spread.Model.CellRange = spdBase_Sheet1.GetSelection(0)
            Dim row As Integer = 0
            If cr.Row < 2 Then
                row = 3
            Else
                row = cr.Row
            End If

            frmZColorMarker.ShowUnderButton(spdBase_Sheet1, btnCOLOR)

            If paraCOLOR <> String.Empty Then

                YosanSetteiBuhinEditLogic.SetColor(spdBase)

                'ペイント後、色情報をクリアする。
                paraCOLOR = String.Empty
            End If

            'フォームのKeyPreviewを切り替える
            '何故かこうしないとコピーができなくなる'
            Me.KeyPreview = Not Me.KeyPreview
            Me.KeyPreview = Not Me.KeyPreview
        End Sub

#End Region

#Region "ボタン(カラーを戻す)"
        ''' <summary>
        ''' ボタン(カラーを戻す)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnColorCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColorCLEAR.Click

            'カラーマーキングをクリアします。
            '実装時は「LblTaitol.Text」で判断せず、別の変数を利用してください。
            '　※カラムを指定してバックカラーの色をリセットしていますが
            '　　カラム数を増減させるとエラーになるので、何か別な方法（簡単にシートの色をデフォルトにする）があるか調査してください。
            '  ※「Cells.Item(3, 0,」の指定はSPREADの名細部に個別に色を付けているので気をつけて！！
            '　※処理もわかりやすく書いてね！！　By　柳沼

            '開始行
            Dim startRow As Integer = YosanSetteiBuhinEditLogic.GetTitleRowsIn(spdBase_Sheet1)

            spdBase_Sheet1.Cells.Item(startRow, 0, spdBase_Sheet1.RowCount - 1, spdBase_Sheet1.Columns.Count - 1).ResetBackColor()

        End Sub

#End Region

#Region "ボタン(ブロックNo行挿入）"
        ''' <summary>
        ''' ボタン（ブロックNo行挿入）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnInsBlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsBlock.Click
            Try

                _TehaiEditLogic.InsertLineBlockNo()

            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("ブロックNo行挿入でエラーが発生しました。({0})", ex.Message))
            End Try

        End Sub

#End Region

#Region "ボタン(戻る)"

        ''' <summary>
        ''' ボタン(戻る)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            '変更が発生した場合だけ、警告を表示する。
            '※項目を一つでも修正したら、戻るボタン押下時に警告を出す。
            '※何も変更していない場合は、警告は無し。

            If Not _aInputWatcher.WasUpdate Then
                '↓↓2014/10/16 酒井 ADD BEGIN
                frm41SaiKakunin.Close()
                frm41SaiKakuninMin.Close()
                '↑↑2014/10/16 酒井 ADD END
                Me.Close()
                Return
            End If
            If frm01Kakunin.ConfirmOkCancel("変更を更新せずに終了しますか？") = MsgBoxResult.Ok Then
                frm41SaiKakunin.Close()
                frm41SaiKakuninMin.Close()
                Me.Close()
                Return
            End If

        End Sub

#Region "ボタン(拡大)"
        ''' <summary>
        ''' ボタン(拡大)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripPurasu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPurasu.Click
            Kakudai1 += 0.2
            If Kakudai1 > 2 Then
                MsgBox("これ以上拡大できません。", MsgBoxStyle.Information, "アラーム")
                Kakudai1 = 2
            End If
            spdBase.ActiveSheet.ZoomFactor = Kakudai1
        End Sub
#End Region

#Region "ボタン(縮小)"

        ''' <summary>
        ''' ボタン(縮小)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripMainasu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMainasu.Click
            Kakudai1 -= 0.2
            If Kakudai1 <= 0 Then
                MsgBox("これ以上縮小できません。", MsgBoxStyle.Information, "アラーム")
                Kakudai1 = 0.2
            End If
            spdBase.ActiveSheet.ZoomFactor = Kakudai1
        End Sub

#End Region

#Region "ボタン(標準サイズ)"
        ''' <summary>
        ''' ボタン(標準サイズ)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripHyoujyun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripHyoujyun.Click
            Kakudai1 = 1 'Nomalへ戻す
            spdBase.ActiveSheet.ZoomFactor = Kakudai1
        End Sub

#End Region

#End Region

#Region "ボタン(保存)"
        ''' <summary>
        ''' ボタン（保存）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnHozon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHozon.Click

            Try
                '2/25　森さん要望により以下のチェックは外す。無条件で保存できる。　Ｂｙ柳沼
                'If _aInputWatcher.WasUpdate = False Then
                '    ComFunc.ShowInfoMsgBox("編集された情報はありません。")
                '    Exit Sub
                'End If

                '保存確認フォームを表示
                Dim result As Integer = frm00Kakunin.Confirm("確認", "保存を実行しますか。", _
                                               "", "OK", "CANCEL")

                If result = MsgBoxResult.Ok Then  'OK
                    Dim watch As New Stopwatch

                    watch.Start()

                    Cursor.Current = Cursors.WaitCursor
                    Me.Refresh()

                    If _TehaiEditLogic.Save() = True Then

                        watch.Stop()
                        Console.WriteLine(String.Format("保存実行時間:{0}ms ", watch.ElapsedMilliseconds))

                        '変更フラグクリア
                        _aInputWatcher.Clear()
                        ComFunc.ShowInfoMsgBox("保存が正常に完了しました", MessageBoxButtons.OK)

                        frm41SaiKakunin.Close()
                        frm41SaiKakuninMin.Close()
                        Me.Close()

                    End If

                Else
                    Return
                End If
                Cursor.Current = Cursors.Default
            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("保存中にエラーが発生しました{0}{1}", vbCrLf, ex.Message))
            Finally
                Cursor.Current = Cursors.Default
            End Try
        End Sub

#End Region

#Region "ボタン(Excel出力)"
        ''' <summary>
        ''' ボタン(Excel出力)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelExport.Click

            Cursor.Current = Cursors.WaitCursor
            Dim msg As String

            Try
                '出力処理
                _TehaiEditLogic.ExcelOutput()
            Catch ex As ApplicationException
                msg = "Excel出力で問題が発生しました。既にファイルが開いている可能性があります。"
                ComFunc.ShowErrMsgBox(msg)

            Catch ex As Exception
                msg = String.Format("Excel出力で問題が発生しました(ERR={0})", ex.Message)
                ComFunc.ShowErrMsgBox(msg)

            Finally

                Cursor.Current = Cursors.Default

            End Try

        End Sub

#End Region

#Region "ボタン(Excel取込)"
        ''' <summary>
        ''' ボタン(Excel取込)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnExcelImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            '2016/01/25 kabasawa'
            'いらない'

            ''Cursor.Current = Cursors.WaitCursor

            ''Try
            '_TehaiEditLogic.Import()

            'Dim startRow As Integer = YosanSetteiBuhinEditLogic.GetTitleRowsIn(spdBase_Sheet1)
            'spdBase_Sheet1.SetActiveCell(startRow, 1)

        End Sub
#End Region

#Region "スプレッドChange基本"
        ''' <summary>
        ''' スプレッドChange基本
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBase_Change(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdBase.Change

            Dim actCol As Integer = spdBase_Sheet1.ActiveColumn.Index
            Dim actRow As Integer = spdBase_Sheet1.ActiveRow.Index
            Dim actColTag As String = spdBase_Sheet1.Columns(actCol).Tag.ToString

            Try

                'セル変更対応処理
                _TehaiEditLogic.ChangeSpreadValueReflect(actRow, actCol, actColTag, txtInputSupport.Text, "")

            Catch ex As Exception
                MsgBox("値変更処理で問題が発生しました")
                frm41SaiKakunin.Close()
                frm41SaiKakuninMin.Close()
                Me.Close()
            Finally
            End Try

        End Sub
#End Region

#Region "スプレッド基本マウスクリックイベント情報取得"

        Private Sub spdBase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBase.Click
            Dim sv As New FarPoint.Win.Spread.SpreadView(Me.spdBase)
            sv = Me.spdBase.GetRootWorkbook()
        End Sub

        ''' <summary>
        '''  スプレッド基本マウスクリックイベント情報取得
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBase_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles spdBase.MouseDown
            Dim sv As New FarPoint.Win.Spread.SpreadView(Me.spdBase)
            sv = Me.spdBase.GetRootWorkbook()

            'セル行列位置取得
            _clickCellPosition = sv.GetCellFromPixel(e.X, e.Y)

            If _clickCellPosition.Row = -1 Then
                _clickCellPosition = sv.GetRowHeaderCellFromPixel(e.X, e.Y)

            Else
                _clickCellPosition = Nothing
            End If

        End Sub
#End Region

#Region "コンボボックスチェンジ(選択ブロックNoにフォーカス)"
        ''' <summary>
        ''' コンボボックスチェンジ(選択ブロックNoにフォーカス)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbBlockNo_Change(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBlockNo.SelectedIndexChanged

            _TehaiEditLogic.Spread_BlockNo_FocusChange(cmbBlockNo.Text)

        End Sub

#End Region

#Region "チェンジ 入力サポート欄"
        ''' <summary>
        ''' チェンジ 入力サポート欄
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub txtInputSupport_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInputSupport.TextChanged

            Dim sheet As Spread.SheetView

            sheet = spdBase_Sheet1

            'フォーカスがない場合はチェンジイベントを実行しない
            If txtInputSupport.Focused Then
                Dim backText As String = txtInputSupport.Text

                '値に変更無い場合、イベントは起動しない。　2011/03/09　柳沼
                If sheet.Cells.Item(sheet.ActiveRow.Index, sheet.ActiveColumn.Index).Value <> backText Then
                    _TehaiEditLogic.ChangeSpreadValueReflect(sheet.ActiveRow.Index, _
                                                                                  sheet.ActiveColumn.Index, _
                                                                                  sheet.Columns(sheet.ActiveColumn.Index).Tag, _
                                                                                  txtInputSupport.Text, "Support")

                    'チェンジイベント中に取引先名称をスプレッドにセットするとTxtInputに入力した
                    '取引先コードが3桁に桁落ちするのでこのコードで保持した値をセットしている
                    If backText.Equals(txtInputSupport.Text) = False Then
                        txtInputSupport.Text = backText
                    End If
                End If

            End If

        End Sub

#End Region

#Region "キー押下処理"
        ''' <summary>
        ''' キー押下処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub frmDispYosanSetteiBuhinEdit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
            Dim downKey As Object
            Dim sheet As Spread.SheetView = spdBase_Sheet1
            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

            downKey = e.KeyCode()

            Select Case downKey
                Case Keys.C
                    If selection Is Nothing Then
                        Exit Sub
                    End If

                    '基本情報列タイトルの色をチェック
                    For i As Integer = 0 To sheet.ColumnCount - 1
                        '青色か？
                        If sheet.Cells(0, i, 1, i).ForeColor = Color.Blue Then
                            Exit Sub
                        End If
                    Next

                    'コントロールキーとCキーが押された
                    If (e.Modifiers And Keys.Control) = Keys.Control Then
                        'If selection.Column <= -1 Then
                        '    ComFunc.ShowInfoMsgBox("行選択でのコピーは出来ません")
                        '    Return
                        'End If

                        If selection.Row <= -1 Then
                            ComFunc.ShowInfoMsgBox("列選択でのコピーは出来ません")
                            Return
                        End If

                        '書式バックアップ
                        Dim listBln As List(Of Boolean()) = YosanSetteiBuhinEditLogic.GetEditCellInfo(sheet)
                        '書式を一時的に全て保存編集対象にする
                        YosanSetteiBuhinEditLogic.SetUndoCellFormat(sheet)
                        'コピー
                        'sheet.ClipboardCopy()

                        ' 選択範囲を取得
                        Dim cr As FarPoint.Win.Spread.Model.CellRange() = sheet.GetSelections()
                        If cr.Length > 0 Then
                            Dim data As [String] = Nothing
                            If cr(0).Row = -1 Then
                                ' 列単位で選択されている場合
                                For i As Integer = 0 To sheet.RowCount - 1
                                    If sheet.GetRowVisible(i) = True Then
                                        data += sheet.GetClipValue(i, cr(0).Column, 1, cr(0).ColumnCount)
                                    End If
                                Next
                            Else
                                ' セル単位で選択されている場合
                                Dim count As Integer = 0
                                For i As Integer = 0 To cr(0).RowCount - 1
                                    If sheet.GetRowVisible(cr(0).Row + i) = True Then
                                        'If count < 1 Then
                                        '    count = count + 1
                                        '    data += sheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)
                                        'Else
                                        '    MsgBox("複数の行に渡ってのコピーをすることはできません")
                                        '    YosanSetteiBuhinEditLogic.SetUndoCellFormat(sheet, listBln)
                                        '    Return
                                        'End If
                                        data += sheet.GetClipValue(cr(0).Row + i, cr(0).Column, 1, cr(0).ColumnCount)

                                    End If
                                Next
                            End If
                            ' クリップボードに設定します
                            Clipboard.SetData(DataFormats.Text, data)
                        End If

                        '書式を戻す
                        YosanSetteiBuhinEditLogic.SetUndoCellFormat(sheet, listBln)
                    End If

                Case Keys.X

                    '2012/02/21 協議の結果一時封印する'
                    'If selection Is Nothing Then
                    '    Exit Sub
                    'End If
                    ''基本情報列タイトルの色をチェック
                    'For i As Integer = 0 To sheet.ColumnCount - 1
                    '    '青色か？
                    '    If sheet.Cells(0, i, 1, i).ForeColor = Color.Blue Then
                    '        Exit Sub
                    '    End If
                    'Next

                    ''コントロールキーとXキーが押された
                    'If (e.Modifiers And Keys.Control) = Keys.Control Then

                    '    If selection.Column <= -1 Then
                    '        ComFunc.ShowInfoMsgBox("行選択での切取りは出来ません")
                    '        Return
                    '    End If

                    '    If selection.Row <= -1 Then
                    '        ComFunc.ShowInfoMsgBox("列選択での切取りは出来ません")
                    '        Return
                    '    End If


                    '    '切取りされる行のブロックを保存ブロック対象として追加する
                    '    _TehaiEditLogic.SetEditRowProc(_TehaiEditLogic.IsBaseSpread, selection.Row, _
                    '                                   selection.Column, selection.RowCount, selection.ColumnCount)
                    'End If

                Case Keys.V

                    If selection Is Nothing Then
                        Exit Sub
                    End If
                    '基本情報列タイトルの色をチェック
                    For i As Integer = 0 To sheet.ColumnCount - 1
                        '青色か？
                        If sheet.Cells(0, i, 1, i).ForeColor = Color.Blue Then
                            Exit Sub
                        End If
                    Next

                    'コントロールキーとVキーが押された
                    If (e.Modifiers And Keys.Control) = Keys.Control Then
                        'If selection.Column <= -1 Then
                        '    ComFunc.ShowInfoMsgBox("行選択での貼付けは出来ません")
                        '    Return
                        'End If

                        If selection.Row <= -1 Then
                            ComFunc.ShowInfoMsgBox("列選択での貼付けは出来ません")
                            Return
                        End If


                        Dim listClip As New List(Of String())

                        listClip = YosanSetteiBuhinEditLogic.GetClipbordList

                        'ペーストをコードで記述すると安藤が出来ない！！
                        'spdBase_Sheet1.ClipboardPaste(Spread.ClipboardPasteOptions.Values)

                        '2012/02/01'
                        'スプレッド自身に貼り付けさせる'
                        Dim im As New Spread.InputMap
                        spdBase.ClipboardOptions = Spread.ClipboardOptions.NoHeaders
                        im.Put(New Spread.Keystroke(Keys.V, Keys.Control), Spread.SpreadActions.ClipboardPasteValues)

                        If Not listClip Is Nothing Then

                            Dim rowCount As Integer = listClip.Count - 1
                            Dim colCount As Integer = listClip(0).Length

                            '単一コピーの場合() '
                            If rowCount = 1 Then
                                For col As Integer = 0 To selection.ColumnCount - 1
                                    For rowindex As Integer = 0 To selection.RowCount - 1
                                        If Not sheet.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                            '隠された行にはペーストしない'
                                            If sheet.Rows(selection.Row + rowindex).Visible Then

                                                sheet.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                                'sheet.Cells(selection.Row + rowindex, selection.Column + col).ForeColor = Color.Blue
                                                'sheet.Cells(selection.Row + rowindex, selection.Column + col).CellType = listClip(0)(0).GetType

                                                'sheet.Cells(selection.Row + rowindex, selection.Column + col).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                                                '変更されたセルの列に対応した処理を行う
                                                _TehaiEditLogic.ChangeSpreadValueReflect(selection.Row + rowindex, selection.Column + col, sheet.Columns(sheet.ActiveColumn.Index).Tag, _
                                                                                  txtInputSupport.Text, "")
                                            End If
                                        End If
                                    Next
                                Next
                            ElseIf rowCount > 1 Then
                                '複数コピーの場合'
                                'For col As Integer = 0 To selection.ColumnCount - 1
                                '    For rowindex As Integer = 0 To selection.RowCount - 1
                                '        If Not sheet.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                                '            '隠された行にはペーストしない'
                                '            If sheet.Rows(selection.Row + rowindex).Visible Then
                                '                sheet.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                                '            Else
                                '                '非表示なら'

                                '            End If
                                '        End If
                                '    Next
                                'Next
                            End If

                            'If (selection.Column + colCount) >= sheet.ColumnCount - 1 Then
                            '    ComFunc.ShowInfoMsgBox("貼り付けようとしている範囲がスプレッド表の最大列を超えています")
                            '    Return
                            'End If

                            If (selection.Row + rowCount) >= sheet.RowCount - 1 Then
                                ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                Return
                            End If


                        End If

                    End If

                Case Keys.Delete
                    'デリートキー押下時'

                    If sheet.SelectionCount <> 1 Then
                        Return
                    End If

                    ClearRange(sheet)

                    '選択行特定'
                    'ParaActRowIdx = sheet.ActiveRowIndex
                    ''選択列特定'
                    'ParaActColIdx = sheet.ActiveColumnIndex

                    'For rowIndex As Integer = ParaActRowIdx To selection.RowCount + ParaActRowIdx - 1
                    '    For colIndex As Integer = ParaActColIdx To selection.ColumnCount + ParaActColIdx - 1
                    '        '中身の削除'
                    '        spdBase_Sheet1.Cells(rowIndex, colIndex).ResetValue()
                    '    Next
                    'Next

                    e.Handled = True

                Case Keys.Right

                    ' 選択セルの場所を特定します。
                    ParaActRowIdx = sheet.ActiveRowIndex
                    ' 選択セルの場所の右隣を特定します。
                    ParaActColIdx = sheet.ActiveColumnIndex + 1
                    '最大カラム数を取得します。
                    Dim colMax As Integer = sheet.ColumnCount

                Case Keys.Left

                    ' 選択セルの場所を特定します。
                    ParaActRowIdx = sheet.ActiveRowIndex
                    ' 選択セルの場所の右隣を特定します。
                    ParaActColIdx = sheet.ActiveColumnIndex - 1
                    '最大カラム数を取得します。
                    Dim colMax As Integer = sheet.ColumnCount

                Case Keys.Enter

                    '選択セルの行を取得'
                    ParaActRowIdx = sheet.ActiveRowIndex
                    '選択セルの列を取得'
                    ParaActColIdx = sheet.ActiveColumnIndex
                    '選択セルの移動先がフィルタ状態かチェック'
                    If Not sheet.Rows(ParaActRowIdx + 1).Visible Then
                        For rowindex As Integer = ParaActRowIdx + 1 To sheet.RowCount - 1
                            If sheet.Rows(rowindex).Visible Then
                                sheet.SetActiveCell(rowindex - 1, ParaActColIdx)
                                Exit For
                            End If
                        Next
                    End If
            End Select

        End Sub

#End Region

#Region "DELETEKeyの処理"
        ''' <summary>
        ''' セルの中身の削除
        ''' </summary>
        ''' <param name="asheetView"></param>
        ''' <remarks></remarks>
        Private Sub ClearRange(ByVal asheetView As FarPoint.Win.Spread.SheetView)
            Dim datamodel As FarPoint.Win.Spread.Model.DefaultSheetDataModel
            '削除範囲の設定'
            Dim fromRow As Integer
            Dim rowCount As Integer
            Dim fromCol As Integer
            Dim colCount As Integer

            datamodel = asheetView.Models.Data


            '開始行<=終了行になるように調整'
            If asheetView.Models.Selection.AnchorRow < asheetView.Models.Selection.LeadRow Then
                fromRow = asheetView.Models.Selection.AnchorRow
                rowCount = asheetView.Models.Selection.LeadRow - fromRow + 1
            Else
                fromRow = asheetView.Models.Selection.LeadRow
                rowCount = asheetView.Models.Selection.AnchorRow - fromRow + 1
            End If

            '開始列<=終了列になるように調整'
            If asheetView.Models.Selection.AnchorColumn < asheetView.Models.Selection.LeadColumn Then
                fromCol = asheetView.Models.Selection.AnchorColumn
                colCount = asheetView.Models.Selection.LeadColumn - fromCol + 1
            Else
                fromCol = asheetView.Models.Selection.LeadColumn
                colCount = asheetView.Models.Selection.AnchorColumn - fromCol + 1
            End If

            'タイトル行は消さない'
            If fromRow < 3 Then
                fromRow = 3
            End If

            Dim flag As Boolean = True
            '基本スプレッドを表示している場合以下のとおり。
            '選択範囲に履歴が存在する行があればDELETE処理しない'
            For i As Integer = 0 To rowCount
                If asheetView.Cells(fromRow, 0).Value = "*" Then
                    flag = False
                End If
            Next

            '削除開始カラムが履歴、ブロック、行ＩＤに重なる場合、専用マークの位置をセットする。
            '使用不可の項目なので削除できてしまってはまずい。　2011/03/11　柳沼
            If flag Then
                If (fromCol + colCount) >= 4 Then
                    If fromCol <= 4 Then
                        colCount = colCount - (4 - fromCol)
                        If colCount <> 0 Then
                            fromCol = 4

                            For Row As Integer = fromRow To fromRow + rowCount - 1
                                If spdBase_Sheet1.Rows(Row).Visible Then
                                    For col As Integer = fromCol To fromCol + colCount - 1
                                        asheetView.ClearRange(Row, col, 1, 1, True)
                                    Next
                                End If
                            Next

                        End If
                    Else
                        For Row As Integer = fromRow To fromRow + rowCount - 1
                            '2012/03/07'
                            'フィルタが掛かってない行が対象'
                            If spdBase_Sheet1.Rows(Row).Visible Then
                                For col As Integer = fromCol To fromCol + colCount - 1
                                    asheetView.ClearRange(Row, col, 1, 1, True)
                                Next
                            End If
                        Next
                    End If
                End If
            End If

            '削除した場所のブロック№を発生させる。
            Dim wBlockNo As String = Nothing
            '2012/03/08 -1しないとカウントがおかしい'
            For i As Integer = fromRow To rowCount + fromRow - 1
                'For i As Integer = fromRow To rowCount + fromRow

                If StringUtil.IsEmpty(wBlockNo) Then

                    _TehaiEditLogic.listEditBlock() = asheetView.Cells(i, 2).Value

                Else
                    If wBlockNo <> asheetView.Cells(i, 2).Value Then

                        _TehaiEditLogic.listEditBlock() = asheetView.Cells(i, 2).Value

                    End If
                End If

                wBlockNo = asheetView.Cells(i, 2).Value

            Next

        End Sub

#End Region

#End Region

#Region "ツールバーボタン"

#Region "ボタン（コピー）"
        ''' <summary>
        ''' ボタン（コピー）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripCopy.Click
            ToolCopy()
        End Sub
#End Region

#Region "ボタン（切り取り）"
        ''' <summary>
        ''' ボタン（切り取り）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            ToolCut()
        End Sub

#End Region

#Region "ボタン（貼りつけ）"
        ''' <summary>
        ''' ボタン（貼りつけ）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPaste.Click

            ToolPaste()

        End Sub
#End Region

#Region "ボタン（もとに戻す）"
        ''' <summary>
        ''' ボタン（もとに戻す）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripUndo.Click

            Try

                _TehaiEditLogic.ToolUndo()

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("正常に元に戻せませんでした。")
            End Try
        End Sub

#End Region

#Region "ボタン（やり直す）"
        ''' <summary>
        ''' ボタン（やり直す）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripRedo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripRedo.Click
            Try

                _TehaiEditLogic.ToolRedo()

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("正常にやり直せませんでした。")
            End Try

        End Sub

#End Region

#Region "ボタン（範囲列非表示）"
        ''' <summary>
        ''' ボタン（範囲列非表示）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripColHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripColHide.Click
            _TehaiEditLogic.ToolColVisibleSwitch(False)
        End Sub

#End Region

#Region "ボタン（範囲列再表示）"
        ''' <summary>
        ''' ボタン（範囲列再表示）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripRangeVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripRangeVisible.Click
            _TehaiEditLogic.ToolColVisibleSwitch(True)
        End Sub

#End Region

#Region "ボタン（全列表示）"
        ''' <summary>
        ''' ボタン（全列表示）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripAllVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripAllVisible.Click

            _TehaiEditLogic.ToolStripAllVisible()

        End Sub

#End Region

#Region "ボタン（フィルタ設定）"

        ''' <summary>
        ''' フィルタ設定
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripDispFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSetFilter.Click
            Try
                Cursor = Cursors.WaitCursor

                _TehaiEditLogic.SetFilter()

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("フィルタの設定でエラーが発生しました")
            Finally
                Cursor = Cursors.Default
            End Try

        End Sub

#End Region

#Region "ボタン（フィルタ解除）"
        ''' <summary>
        ''' フィルタ解除
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripResetFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripResetFilter.Click
            Try
                Cursor = Cursors.WaitCursor

                _TehaiEditLogic.ResetFilter()

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("フィルタの解除でエラーが発生しました")
            Finally
                Cursor = Cursors.Default
            End Try

        End Sub

#End Region

#Region "ボタン（行高拡大）"
        ''' <summary>
        ''' ボタン（行高拡大）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripHeightUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripHeightUp.Click
            _TehaiEditLogic.RowHeightExp()
        End Sub

#End Region

#Region "ボタン（行高縮小）"
        ''' <summary>
        ''' ボタン（行高縮小）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripHeightDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripHeightDown.Click
            _TehaiEditLogic.RowHeightReduce()
        End Sub

#End Region

#End Region

#Region "右ショートカットメニュー(切取り)"
        ''' <summary>
        ''' 右ショートカットメニュー(切取り)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolMenuCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            ToolCut()
        End Sub

#End Region

#Region "右ショートカットメニュー(コピー)"
        ''' <summary>
        ''' 右ショートカットメニュー(コピー)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolMenuCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolMenuCopy.Click
            ToolCopy()
        End Sub

#End Region

#Region "右ショートカットメニュー(貼付け)"
        ''' <summary>
        ''' 右ショートカットメニュー(貼付け)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolMenuPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolMenuPaste.Click
            ToolPaste()
        End Sub

#End Region

#Region "右クリックショートカットメニュー(行挿入)"
        ''' <summary>
        '''マウスクリック 行挿入
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolMenuRowInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolMenuRowInsert.Click

            Try
                Dim clickRowNo As Integer = 0
                Dim sheet As Spread.SheetView = spdBase_Sheet1
                Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

                'マウスクリック位置情報有
                If Not _clickCellPosition Is Nothing AndAlso _clickCellPosition.Row <> -1 Then

                    'スプレッド行挿入処理へ
                    _TehaiEditLogic.InsertSpread(selection.Row, selection.RowCount)
                    _clickCellPosition = Nothing

                Else
                    MsgBox("ContextMenuが行見出し位置のみで表示されるように調整中")
                End If

            Catch ex As Exception
                MsgBox(String.Format("行挿入時にシステムエラーが発生しました:{0}", ex.Message))
            End Try
        End Sub

#End Region

#Region "右クリックショートカットメニュー(行削除)"
        ''' <summary>
        '''右クリックショートカットメニュー(行削除)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolMenuRowDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolMenuRowDelete.Click

            Try

                Dim sheet As Spread.SheetView = spdBase_Sheet1
                Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

                'クリック位置情報有無
                If Not _clickCellPosition Is Nothing AndAlso _clickCellPosition.Row <> -1 Then
                    '削除される行のブロックを保存ブロック対象として追加する
                    _TehaiEditLogic.AddEditBlockList(selection)

                    '行削除処理
                    Me.spdBase_Sheet1.Rows.Remove(selection.Row, selection.RowCount)

                    _clickCellPosition = Nothing
                Else
                    MsgBox("ContextMenuが行見出し位置のみで表示されるように調整中")
                End If

            Catch ex As Exception
                MsgBox(String.Format("行削除時にシステムエラーが発生しました:{0}", ex.Message))
            End Try

        End Sub
#End Region

#Region "切取りイベント処理"
        ''' <summary>
        ''' 切取りイベント処理
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ToolCut()
            Try

                If spdBase_Sheet1.GetSelection(0) Is Nothing Then
                    ComFunc.ShowInfoMsgBox("セル範囲が指定されていません。")
                    Exit Sub
                End If

                _TehaiEditLogic.ToolCut()

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("切取りが正常に行えませんでした。")
            End Try
        End Sub
#End Region

#Region "コピーイベント処理"
        ''' <summary>
        ''' コピーイベント処理
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ToolCopy()
            Try

                If spdBase_Sheet1.GetSelection(0) Is Nothing Then
                    ComFunc.ShowInfoMsgBox("セル範囲が指定されていません。")
                    Exit Sub
                End If

                _TehaiEditLogic.ToolCopy()

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("コピーが正常に行えませんでした。")
            End Try

        End Sub

#End Region

#Region "貼りつけイベント処理"

        Private Sub ToolPaste()
            Try

                If spdBase_Sheet1.GetSelection(0) Is Nothing Then
                    ComFunc.ShowInfoMsgBox("セル範囲が指定されていません。")
                    Exit Sub
                End If

                _TehaiEditLogic.ToolPaste(spdBase_Sheet1)

            Catch ex As Exception
                ComFunc.ShowErrMsgBox("貼りつけが正常に行えませんでした。")
            End Try
        End Sub
#End Region

        ''' <summary>
        ''' セルのIME設定(基本情報)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBase_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBase.EditModeOn
            'アクティブなセルのIMEを制御する'
            Dim sheet As Spread.SheetView = spdBase_Sheet1
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            If ParaActColIdx = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_SEISAKU).Index _
                 Or ParaActColIdx = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1).Index _
                 Or ParaActColIdx = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2).Index _
                 Or ParaActColIdx = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3).Index _
                 Or ParaActColIdx = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_TIGU).Index _
                 Or ParaActColIdx = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KIBO).Index _
                 Or ParaActColIdx = sheet.Columns(NmSpdTagBase.TAG_YOSAN_BUHIN_NOTE).Index _
                 Or ParaActColIdx = sheet.Columns(NmSpdTagBase.TAG_YOSAN_BIKOU).Index _
                 Or ParaActColIdx = sheet.Columns(NmSpdTagBase.TAG_YOSAN_KONKYO_INYOU_MIX_BUHIN_HI).Index _
                 Or ParaActColIdx = sheet.Columns(NmSpdTagBase.TAG_YOSAN_KONKYO_KOUHOU).Index Then
                'IMEを使用可能にする。
                spdBase.EditingControl.ImeMode = Windows.Forms.ImeMode.NoControl
                spdBase.ImeMode = Windows.Forms.ImeMode.NoControl

            Else
                'IMEを使用不可能にする。
                spdBase.EditingControl.ImeMode = Windows.Forms.ImeMode.Disable
                spdBase.ImeMode = Windows.Forms.ImeMode.Disable
            End If
        End Sub

        ''' <summary>
        ''' ツールの過去発注情報引当クリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 過去発注情報引当ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 過去発注情報引当ToolStripMenuItem.Click
            '↓↓2014/09/30 酒井 ADD BEGIN
            Dim file As Stream = Nothing
            While (1 = 1)
                '↑↑2014/09/30 酒井 ADD END
                'ファイル選択
                With OpenFileDialog1
                    .Filter = "Excelファイル(*.xls)|*.xls;"
                    .FilterIndex = 1
                    .CheckFileExists = True
                    .CheckPathExists = True
                    .FileName = "過去発注情報ワークシート.xls"

                    'キャンセルした場合
                    If .ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                        If file IsNot Nothing Then
                            file.Close()
                            file.Dispose()
                        End If
                        Exit Sub
                    End If
                End With

                ' 選択ファイルをパラメタとしてFrm20ExcelCellSelect画面を開く()
                Try
                    file = OpenFileDialog1.OpenFile
                Catch ex As Exception
                    ComFunc.ShowErrMsgBox("ファイルを開くことが出来ません。ファイルをExcelなどで開いている場合は終了してください。")
                    Continue While
                End Try
                Dim buhinNoCol As Integer = 0
                Dim sheet As Spread.SheetView = spdBase_Sheet1
                Using cellSelect As New FrmExcelCellSelect(file)
                    '↓↓2014/09/30 酒井 ADD BEGIN
                    Do
                        '↑↑2014/09/30 酒井 ADD END
                        'IF 「キャンセル」 Then End Sub
                        If cellSelect.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                            cellSelect.EBomSpread1.Dispose()
                            '↓↓2014/09/30 酒井 ADD BEGIN
                            'Exit Sub
                            file.Close()
                            file.Dispose()
                            Continue While
                            '↑↑2014/09/30 酒井 ADD END
                        End If

                        '選択ファイル、選択セルをパラメタとしてFrm20ExcelKeyDataSelect画面を開く
                        Using keyDataSelect As New FrmExcelKeyDataSelect(file, cellSelect.activeSheet, Me.spdBase)
                            'IF 「キャンセル」 Then End Sub
                            If keyDataSelect.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                                '↓↓2014/09/30 酒井 ADD BEGIN
                                'cellSelect.EBomSpread1.Dispose()
                                'Exit Sub
                                file.Close()
                                file.Dispose()
                                Continue Do
                                '↑↑2014/09/30 酒井 ADD END
                            End If
                            With cellSelect.activeSheet
                                Dim MaxrowCellSelect As Long = 0
                                '指定EXCELの選択行まで'
                                For index = cellSelect.activeSheet.RowCount - 1 To cellSelect.activeSheet.ActiveRowIndex + 1 Step -1
                                    Dim FlgMaxrow As Boolean = False
                                    '選択した検索キー列'
                                    For indexcol = 0 To keyDataSelect.spdSearch_Sheet1.Columns.Count - 1
                                        If StringUtil.IsNotEmpty(keyDataSelect.spdSearch_Sheet1.Cells(0, indexcol).Text) Then
                                            Dim Sheetcol As Integer = Integer.Parse(keyDataSelect.spdSearch_Sheet1.Cells(0, indexcol).Value)
                                            If StringUtil.IsNotEmpty(cellSelect.activeSheet.Cells(index, Sheetcol).Value) Then
                                                MaxrowCellSelect = index
                                                FlgMaxrow = True
                                                Exit For
                                            End If
                                        End If
                                    Next
                                    If FlgMaxrow Then
                                        Exit For
                                    End If
                                Next

                                Dim MaxrowSpdBase As Long = 0
                                For indexSheetrow = sheet.RowCount - 1 To YosanSetteiBuhinEditLogic.GetTitleRowsIn(sheet) + 1 Step -1
                                    Dim FlgMaxrow As Boolean = False
                                    For indexcol = 0 To keyDataSelect.spdSearch_Sheet1.Columns.Count - 1
                                        If StringUtil.IsNotEmpty(keyDataSelect.spdSearch_Sheet1.Cells(0, indexcol).Text) Then
                                            Dim exlcol As Integer = Integer.Parse(keyDataSelect.spdSearch_Sheet1.Columns.Item(indexcol).Tag)
                                            If StringUtil.IsNotEmpty(sheet.Cells(indexSheetrow - 1, exlcol).Value) Then
                                                MaxrowSpdBase = indexSheetrow
                                                FlgMaxrow = True
                                                Exit For
                                            End If
                                        End If
                                    Next
                                    If FlgMaxrow Then
                                        Exit For
                                    End If
                                Next


                                Dim lstSelectCol As New ArrayList
                                For indexcol = 0 To keyDataSelect.spdSearch_Sheet1.Columns.Count - 1
                                    If StringUtil.IsNotEmpty(keyDataSelect.spdSearch_Sheet1.Cells(0, indexcol).Text) Then
                                        lstSelectCol.Add(indexcol)
                                    End If
                                Next
                                Dim lstUpdateCol As New ArrayList
                                For indexcol = 0 To keyDataSelect.spdUpdate_Sheet1.Columns.Count - 1
                                    If StringUtil.IsNotEmpty(keyDataSelect.spdUpdate_Sheet1.Cells(0, indexcol).Text) Then
                                        lstUpdateCol.Add(indexcol)
                                    End If
                                Next

                                For index = cellSelect.activeSheet.ActiveRowIndex + 1 To MaxrowCellSelect
                                    For indexSheetrow = YosanSetteiBuhinEditLogic.GetTitleRowsIn(sheet) + 1 To MaxrowSpdBase
                                        'Dim flag As Boolean = False
                                        Dim flag As Boolean = True
                                        For Each indexcol In lstSelectCol
                                            Dim exlcol As Integer = Integer.Parse(keyDataSelect.spdSearch_Sheet1.Columns.Item(indexcol).Tag)
                                            Dim Sheetcol As Integer = Integer.Parse(keyDataSelect.spdSearch_Sheet1.Cells(0, indexcol).Value)
                                            ''キー項目が一致しないの場合
                                            If cellSelect.activeSheet.Cells(index, Sheetcol).Text <> sheet.Cells(indexSheetrow - 1, exlcol).Text Then
                                                flag = False
                                                Exit For
                                            Else
                                                'flag = True
                                                'Exit For
                                            End If
                                        Next
                                        'キー項目が一致の場合
                                        If flag Then
                                            For Each indexcol In lstUpdateCol
                                                Dim exlcol As Integer = Integer.Parse(keyDataSelect.spdUpdate_Sheet1.Columns.Item(indexcol).Tag)
                                                Dim Sheetcol As Integer = Integer.Parse(keyDataSelect.spdUpdate_Sheet1.Cells(0, indexcol).Value)
                                                If keyDataSelect.cbxTORIKOMISAKI_KUUHAKU.Checked And StringUtil.IsEmpty(sheet.Cells(indexSheetrow - 1, exlcol).Value) Then
                                                    sheet.Cells(indexSheetrow - 1, exlcol).Value = cellSelect.activeSheet.Cells(index, Sheetcol).Value
                                                    sheet.Cells(indexSheetrow - 1, exlcol).ForeColor = Color.Blue
                                                    sheet.Cells(indexSheetrow - 1, exlcol).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

                                                ElseIf keyDataSelect.cbxTORIKOMIMOTO_KUUHAKU.Checked Then
                                                    sheet.Cells(indexSheetrow - 1, exlcol).Value = cellSelect.activeSheet.Cells(index, Sheetcol).Value
                                                    sheet.Cells(indexSheetrow - 1, exlcol).ForeColor = Color.Blue
                                                    sheet.Cells(indexSheetrow - 1, exlcol).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                                                End If

                                            Next
                                        End If
                                    Next
                                Next

                            End With

                            Dim progPath As String = My.Application.Info.DirectoryPath
                            Dim filePath As String = progPath & "\YOSAN_SETTEI_IMPORT.ini"

                            'INIファイル読み込み
                            Dim WriteINT As PrivateProfile = New PrivateProfile(filePath)
                            For index = 0 To keyDataSelect.spdSearch_Sheet1.Columns.Count - 1
                                If keyDataSelect.spdSearch_Sheet1.Cells(0, index).Value Is Nothing Then
                                    WriteINT.Write("KENSAKU", keyDataSelect.spdSearch_Sheet1.Columns(index).Tag, "")
                                Else
                                    WriteINT.Write("KENSAKU", keyDataSelect.spdSearch_Sheet1.Columns(index).Tag, keyDataSelect.spdSearch_Sheet1.Cells(0, index).Value)
                                End If
                            Next
                            For index = 0 To keyDataSelect.spdUpdate_Sheet1.Columns.Count - 1
                                If keyDataSelect.spdUpdate_Sheet1.Cells(0, index).Value Is Nothing Then
                                    WriteINT.Write("KOUSHIN", keyDataSelect.spdUpdate_Sheet1.Columns(index).Tag, "")
                                Else
                                    WriteINT.Write("KOUSHIN", keyDataSelect.spdUpdate_Sheet1.Columns(index).Tag, keyDataSelect.spdUpdate_Sheet1.Cells(0, index).Value)
                                End If
                            Next
                            If keyDataSelect.cbxTORIKOMISAKI_KUUHAKU.Checked Then
                                WriteINT.Write("OPTION", "TORIKOMISAKI_KUUHAKU", "1")
                            Else
                                WriteINT.Write("OPTION", "TORIKOMISAKI_KUUHAKU", "0")
                            End If
                            If keyDataSelect.cbxTORIKOMIMOTO_KUUHAKU.Checked Then
                                WriteINT.Write("OPTION", "TORIKOMIMOTO_KUUHAKU", "1")
                            Else
                                WriteINT.Write("OPTION", "TORIKOMIMOTO_KUUHAKU", "0")
                            End If
                        End Using
                        cellSelect.EBomSpread1.Dispose()
                        '↓↓2014/09/30 酒井 ADD BEGIN
                        'Exit Do
                        file.Close()
                        file.Dispose()
                        Exit Sub
                    Loop
                    '↑↑2014/09/30 酒井 ADD END
                End Using
                '↓↓2014/09/30 酒井 ADD BEGIN
            End While
            '↑↑2014/09/30 酒井 ADD END
        End Sub

        Private Sub spdBase_ComboCloseUp(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles spdBase.ComboCloseUp
            Dim sheet As Spread.SheetView = spdBase_Sheet1

            Dim isKatashiyou As Boolean = e.Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1).Index Or _
            e.Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2).Index Or _
            e.Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3).Index
            ' 幅を元に戻る
            If isKatashiyou Then
                e.EditingControl.Width = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1).Width
            End If
        End Sub

        Private Sub spdBase_ComboDropDown(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles spdBase.ComboDropDown
            Dim sheet As Spread.SheetView = spdBase_Sheet1

            Dim isKatashiyou As Boolean = e.Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1).Index Or _
                    e.Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2).Index Or _
                    e.Column = sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3).Index

            ' （型仕様1～3コンボ）のコンボドロップダウン表示イベント
            If isKatashiyou Then
                Dim katashiyou1 As String = sheet.Cells(e.Row, sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1).Index).Value
                Dim katashiyou2 As String = sheet.Cells(e.Row, sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2).Index).Value
                Dim katashiyou3 As String = sheet.Cells(e.Row, sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3).Index).Value
                Dim dropCell = spdBase_Sheet1.Cells(e.Row, e.Column)
                Dim fc As FpCombo = e.EditingControl

                ' セル幅を全角１５文字程度に拡張する。
                'dropCell.Column.Width = LARGER_WIDTH
                fc.Width = LARGER_WIDTH

                ' 型仕様1～3の内、他で選択済みの項目を選択不可にする。
                Select Case e.Column
                    Case sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_1).Index
                        If Not String.IsNullOrEmpty(katashiyou2) Then
                            fc.ItemData.Remove(katashiyou2)
                            fc.List.Remove(katashiyou2)
                        End If
                        If Not String.IsNullOrEmpty(katashiyou3) Then
                            fc.ItemData.Remove(katashiyou3)
                            fc.List.Remove(katashiyou3)
                        End If

                    Case sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_2).Index
                        If Not String.IsNullOrEmpty(katashiyou1) Then
                            fc.ItemData.Remove(katashiyou1)
                            fc.List.Remove(katashiyou1)
                        End If
                        If Not String.IsNullOrEmpty(katashiyou3) Then
                            fc.ItemData.Remove(katashiyou3)
                            fc.List.Remove(katashiyou3)
                        End If
                    Case sheet.Columns(NmSpdTagBase.TAG_YOSAN_TSUKURIKATA_KATASHIYOU_3).Index
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

#Region "追加"

        ''' <summary>
        ''' 右クリックメニュー開くとき
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cMenuSpdRowCntrl_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cMenuSpdRowCntrl.Opening
            '制御が必要'
            Dim sheet As FarPoint.Win.Spread.SheetView = spdBase_Sheet1

            PurchaseRequestToolStripMenuItem.Enabled = False
            YosanLayOutToolStripMenuItem.Enabled = False
            部品費端数処理ToolStripMenuItem.Enabled = False

            Dim column As Integer = sheet.ActiveColumnIndex

            Select Case column
                Case sheet.Columns(NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI).Index
                    '単価自動設定機能-割付予算'
                    YosanLayOutToolStripMenuItem.Enabled = True
                    部品費端数処理ToolStripMenuItem.Enabled = True

                Case sheet.Columns(NmSpdTagBase.TAG_YOSAN_KOUNYU_KIBOU_BUHIN_HI).Index
                    '単価自動設定機能-購入希望単価'
                    PurchaseRequestToolStripMenuItem.Enabled = True
                    部品費端数処理ToolStripMenuItem.Enabled = True

            End Select



        End Sub


        ''' <summary>
        ''' ツール機能-MIXコストの最新化
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub MIXコスト最新化ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MIXコスト最新化ToolStripMenuItem.Click
            Dim frm As New frm03Syorichu
            frm.lblKakunin.Text = "MIXコスト最新化中..."
            frm.lblKakunin2.Text = ""
            frm.lblKakunin3.Text = ""
            frm.Show()
            _TehaiEditLogic.ReadMixCost()
            frm.Close()

        End Sub



        ''' <summary>
        ''' 単価自動設定機能：割付予算クリックイベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub YosanLayOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YosanLayOutToolStripMenuItem.Click
            'ダイアログから情報を入手する'
            Dim genchoEventCode As String = ""
            Dim phaseNo As String = ""
            Dim layOutNo As Integer = -1
            Dim isAll As Boolean = False

            Dim frm As New frmYosanLayoutDialog
            If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                genchoEventCode = frm.GenchoEventCode
                phaseNo = frm.PhaseNo
                layOutNo = frm.LayoutNo
                isAll = frm.IsAll
            Else
                'OK以外は何もしない'
                Exit Sub
            End If

            '割付予算の数値で分岐'
            Select Case layOutNo
                Case 0
                    'パーツプライス値スライド'
                    _TehaiEditLogic.SetPartsPrice(isAll)
                Case 1
                    'MIXコスト×係数1'
                    _TehaiEditLogic.SetMixCostMultiplication(isAll)
                Case 2
                    '過去購入部品優先度1(割付値)
                    _TehaiEditLogic.SetBuyParts(isAll)
                Case 3
                    'ファンクションマスタ'
                    _TehaiEditLogic.SetFunctionCost(isAll)
                Case 4
                    '試作開発管理表'
                    _TehaiEditLogic.ReadGencho(genchoEventCode, phaseNo, isAll)
            End Select
        End Sub

        ''' <summary>
        ''' 単価自動設定：購入希望単価イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub PurchaseRequestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseRequestToolStripMenuItem.Click
            'ダイアログから情報を入手する'
            Dim genchoEventCode As String = ""
            Dim phaseNo As String = ""
            Dim layOutNo As Integer = -1
            Dim isAll As Boolean = False

            Dim frm As New frmPurchaseRequestDialog
            If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                genchoEventCode = frm.GenchoEventCode
                phaseNo = frm.PhaseNo
                layOutNo = frm.LayoutNo
                isAll = frm.IsAll
            Else
                'OK以外は何もしない'
                Exit Sub
            End If

            '割付予算の数値で分岐'
            Select Case layOutNo
                Case 0
                    'パーツプライス値スライド'
                    _TehaiEditLogic.SetPartsPrice(isAll)
                Case 1
                    '割付予算値スライド'
                    _TehaiEditLogic.SetYosanLayout(isAll)
                Case 2
                    '過去購入部品優先度1(購入希望単価)
                    _TehaiEditLogic.SetBuyParts(isAll)
                Case 3
                    '割付予算×係数2'
                    _TehaiEditLogic.SetYosanLayoutMulti(isAll)
                Case 4
                    '試作開発管理表'
                    _TehaiEditLogic.ReadGencho(genchoEventCode, phaseNo, isAll)
            End Select
        End Sub


#Region "ツールボタン(ソート)"
        ''' <summary>
        ''' ソートを行う
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Sort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sort.Click
            '2016/01/08 kabasawa'
            '協議の結果不要(一応とっておく)'

            ''ソート用の画面を開く'
            'Dim frm As New frmSortDialog(SortList)
            'If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            '    '状態を受け取る'
            '    Dim Conditions1 As Integer = frm.ColumnIndex
            '    Dim order1 As Boolean = frm.IsOrder

            '    _TehaiEditLogic.SheetSort(Conditions1, order1)


            'End If

        End Sub

        ''' <summary>
        ''' ソート用のリストを返す。
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SortList() As List(Of String)
            '固定で取得できないシートなのでいちいち指定する必要あり。'
            '列が増減するたびに対応必須！'
            ''
            Dim resultList As New List(Of String)

            resultList.Add("FLG")
            resultList.Add("部課コード")
            resultList.Add("ブロック№")
            resultList.Add("行ID")
            resultList.Add("レベル")
            resultList.Add("部品番号表示順")
            Return resultList
        End Function


#End Region

#Region "最新化"
        ''' <summary>
        ''' 最新化
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 最新化ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 最新化ToolStripMenuItem.Click


            If MsgBox("最新化のため、保存処理を行います。" & vbCr & "よろしいですか？", MsgBoxStyle.YesNo, "確認") = MsgBoxResult.Yes Then

                If _TehaiEditLogic.Save() Then
                    '保存できたらにする。'
                    Dim frm As New frm03Syorichu
                    frm.lblKakunin.Text = "比較最新化中..."
                    frm.lblKakunin2.Text = ""
                    frm.lblKakunin3.Text = ""
                    frm.Show()

                    _TehaiEditLogic.Compare()
                    frm.Close()
                End If

            End If

        End Sub
#End Region

        ''' <summary>
        ''' 過去購入部品取得
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub コスト検索ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles コスト検索ToolStripMenuItem.Click
            Dim frm As New frmCostSearchDialog
            Dim flag As String = ""
            If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                flag = frm.Flag
            Else
                'OK以外は何もしない'
                Exit Sub
            End If

            Dim frm2 As New frm03Syorichu
            frm2.lblKakunin.Text = "過去購入コスト検索中..."
            frm2.lblKakunin2.Text = ""
            frm2.lblKakunin3.Text = ""
            frm2.Show()
            _TehaiEditLogic.PastPurchase(flag)
            frm2.Close()

        End Sub

        ''' <summary>
        ''' 過去購入部品列の表示と非表示を変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ToolStripGroupPastPurchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripGroupPastPurchase.Click
            If ToolStripGroupPastPurchase.Checked Then
                ToolStripGroupPastPurchase.Text = "過去購入部品列非表示"
            Else
                ToolStripGroupPastPurchase.Text = "過去購入部品列表示"
            End If

            _TehaiEditLogic.HiddenPastParchase(ToolStripGroupPastPurchase.Checked)

        End Sub

#Region "部品費端数処理"

        ''' <summary>
        ''' 部品費端数処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 部品費端数処理ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 部品費端数処理ToolStripMenuItem.Click
            Dim frm As New frmPartsPriceFractionDialog(spdBase_Sheet1)
            Dim pros As String = ""
            Dim unit As String = ""
            Dim flag As Integer = 0
            If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                pros = frm.Pros
                unit = frm.Unit
            Else
                'OK以外は何もしない'
                Exit Sub
            End If
            If Not _TehaiEditLogic.FractionTest(pros, unit) Then
                Dim frmKakunin As New frmFractionKakunin
                If frmKakunin.ShowDialog = Windows.Forms.DialogResult.OK Then
                    flag = frmKakunin.Result
                End If
            End If
            If flag = 2 Then
                Exit Sub
            End If

            _TehaiEditLogic.Fraction(pros, unit, flag)

        End Sub

#End Region

#Region "ファンクション取引先単価マスタ設定"

        ''' <summary>
        ''' ファンクション取引先単価マスタ設定
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ファンクション取引先単価マスタ設定ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ファンクション取引先単価マスタ設定ToolStripMenuItem.Click
            'これは別画面？'
            'T_YOSAN_SETTEI_BUHIN_FUNCTIONに対してのIO'
            Dim frm As New frmYosanFunction()
            frm.ShowDialog()

        End Sub

#End Region

#Region "部品費マルチ設定"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 部品費マルチ設定ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 部品費マルチ設定ToolStripMenuItem.Click
            Dim frm As New PartsMultiSetting(Me.spdBase_Sheet1)
            If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                'OKもらったら何か受け取る。'
            Else
                'OK以外は何もしない'
                Exit Sub
            End If
        End Sub

#End Region

#End Region


        Private Sub datamodel_Changed(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs) Handles datamodel.Changed
            'Console.WriteLine(String.Format("Row={0} Column={1} Value={2} Type={3}", e.Row, e.Column, spdBase.ActiveSheet.GetValue(e.Row, e.Column), e.Type))


            ''
            _TehaiEditLogic.AutoMixPrice(e.Row)

            'For i As Integer = 0 To e.RowCount - 1
            '    For j As Integer = 0 To e.ColumnCount - 1
            '        If StringUtil.Equals(spdBase_Sheet1.Columns(e.Column + j).Tag, NmSpdTagBase.TAG_YOSAN_WARITUKE_BUHIN_HI) Then
            '            _TehaiEditLogic.AutoMixPrice(i + e.Row)
            '        End If
            '    Next
            'Next

        End Sub


       
       
    End Class

#End Region

End Namespace

