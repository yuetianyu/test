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
Imports EventSakusei.TehaichoEdit.Dao
Imports EventSakusei.TehaichoEdit.Logic
Imports EventSakusei.NokiIkkatuSettei
Imports System.Text.RegularExpressions
Imports ShisakuCommon.Ui.Spd
Imports System.Text
Imports Microsoft.VisualBasic.FileIO
Imports EventSakusei.TehaichoEdit.Ui
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.CellType
Imports System.IO
Imports ShisakuCommon.Db.EBom.Vo

Namespace TehaichoEdit

#Region "手配帳編集クラス"
    ''' <summary>
    ''' 手配帳編集クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class frm20DispTehaichoEdit
        '↓↓2014/10/16 酒井 ADD BEGIN
        'Public Class frm20DispTehaichoEdit
        '↑↑2014/10/16 酒井 ADD END
#Region "プライベート変数"
        '''<summary>初期化完了フラグ</summary>>
        Private _InitComplete As Boolean = False
        '''<summary>入力有無判定に使用</summary>>
        Private _aInputWatcher As InputWatcher
        ''' <summary>入力サポート欄とスプレッドの関係を定義</summary>
        Private _inputSupport As TehaichoeditInputSupport
        ''' <summary>フォーム表示時に呼び出されるID</summary>
        Private m_initSwitchId As String = String.Empty
        ''' <summary>画面制御ロジック</summary>
        Private _TehaiEditLogic As TehaichoEditLogic
        ''' <summary>マウスクリック行列位置保存</summary>
        Private _clickCellPosition As Spread.Model.CellRange
        ''' <summary>納期一括画面</summary>
        Private _frmNoukiIkkatsu As Frm21DispNokiIkkatuSettei
        ''' <summary>基本情報シート</summary>
        Private kihonSheet As SheetView
        ''' <summary>号車情報シート</summary>
        Private GousyaSheet As SheetView

        ''↓↓2014/09/02 Ⅰ.2.管理項目追加_bx) 酒井 ADD BEGIN
        Private Const LARGER_WIDTH = 200
        ''↑↑2014/09/02 Ⅰ.2.管理項目追加_bx) 酒井 ADD END

        '↓↓2014/10/16 酒井 ADD BEGIN
        Private _shisakuEventCode As String
        '↑↑2014/10/16 酒井 ADD END

        ''↓↓2015/01/15 追加 TES)劉 ADD BEGIN
        Private _shisakuListCode As String
        ''↑↑2015/01/15 追加 TES)劉 ADD END

        '↓↓↓2014/12/24 試作１課フラグを渡す TES)張 ADD BEGIN
        Private _isSisaku1KaFlg As Integer
        '↑↑↑2014/12/24 試作１課フラグを渡す TES)張 ADD END

        '↓↓↓2015/01/08 ツールバーの材質表示/非表示を追加 TES)張 ADD BEGIN
        Private ZAI As String = "D"
        '↑↑↑2015/01/08 ツールバーの材質表示/非表示を追加 TES)張 ADD END

#Region "スプレッド拡大縮小用 "

        'SPREADのサイズ変更用に使用する。
        '１は標準サイズ
        Private Kakudai1 As Decimal = 1
        Private Kakudai2 As Decimal = 1

#End Region

#End Region

#Region "コンストラクタ"

        '↓↓↓2014/12/24 試作１課フラグを渡す TES)張 CHG BEGIN
        'Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuListCode">試作リストコード</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuListCode As String, Optional ByVal isSisaku1KaFlg As Integer = 0)


            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            Me._shisakuEventCode = shisakuEventCode
            Me._isSisaku1KaFlg = isSisaku1KaFlg
            Me._shisakuListCode = shisakuListCode

            Me.Refresh()
            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            '納期一括設定画面を初期化しておく
            _frmNoukiIkkatsu = New Frm21DispNokiIkkatuSettei()

            '画面制御ロジック
            _TehaiEditLogic = New TehaichoEditLogic(shisakuEventCode, shisakuListCode, Me, _frmNoukiIkkatsu)

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
            '初期データ表示スプレッド(号車)
            DispSpreadGousya()
            'スプレッドの読取専用列設定
            _TehaiEditLogic.LockColSpread()

            'マウス右クリック対処
            TehaichoEditSpreadUtil.AddEventCellRightClick(Me, spdBase, _inputSupport)
            'マウス右クリック対処
            TehaichoEditSpreadUtil.AddEventCellRightClick(Me, spdGouSya, _inputSupport)

            '2011/02/28　柳沼修正
            'Enterキーの動作を、「編集」から「次行へ移動」にする
            ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyEnterIsNextRow(spdBase)
            ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyEnterIsNextRow(spdGouSya)

            '2011/02/28　柳沼修正
            'Shift + Enterキーの動作を、「前行へ移動」にする
            ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyShiftEnterIsPreviousRow(spdBase)
            ShisakuCommon.Ui.Spd.SpreadUtil.BindKeyShiftEnterIsPreviousRow(spdGouSya)

            '2012/03/07 樺澤修正'
            '各シートの各セルにCellTypeを設定する'
            Dim dumCellType As New TextCellType()
            dumCellType.MaxLength = 3
            spdGouSya_Sheet1.Columns(spdGouSya_Sheet1.ColumnCount - 1).CellType = dumCellType
            spdGouSya_Sheet1.Columns(spdGouSya_Sheet1.ColumnCount - 1).Width = 30

            '初期化完了
            _InitComplete = True

            'Catch ex As Exception

            '    Dim msg As String
            '    msg = String.Format("画面の初期化中に問題が発生しました(ERR={0})", ex.Message)
            '    ComFunc.ShowErrMsgBox(msg)
            '    Me.Close()

            'Finally
            '    Cursor.Current = Cursors.Default
            'End Try

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
            '集計コード
            _TehaiEditLogic.AddCbShukeiCode()
            '手配記号
            _TehaiEditLogic.AddCbTehaiKigou()
            '再使用不可項目
            _TehaiEditLogic.AddCbSaishiyouFuka()
            ''↓↓2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            _TehaiEditLogic.AddCbTsukurikataSeisaku()
            _TehaiEditLogic.AddCbTsukurikataKatashiyou()
            _TehaiEditLogic.AddCbTsukurikataTigu()
            ''↑↑2014/08/25 Ⅰ.2.管理項目追加 酒井 ADD END

        End Sub
#End Region

#Region "コントロール監視初期化"
        ''' <summary>
        ''' コントロール監視初期化
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeWatcher()

            _inputSupport = New TehaichoeditInputSupport(txtInputSupport, spdBase, spdGouSya)
            _aInputWatcher = New InputWatcher

            _aInputWatcher.Add(txtKoujiNo)
            _aInputWatcher.Add(spdBase)
            _aInputWatcher.Add(spdGouSya)

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
                '↓↓2014/10/16 酒井 ADD BEGIN
                frm41SaiKakunin.Close()
                frm41SaiKakuninMin.Close()
                '↑↑2014/10/16 酒井 ADD END
                Me.Close()
            Finally

            End Try
        End Sub

#End Region

#Region "スプレッド号車情報のデータ表示"
        ''' <summary>
        ''' スプレッド号車情報の表示
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DispSpreadGousya()

            '号車情報表示ロジック
            If _TehaiEditLogic.SetSpreadGousya = False Then
                Throw New Exception()
            End If

            '入力監視フラグクリア
            _aInputWatcher.Clear()
            'Try

            '    '号車情報表示ロジック
            '    If _TehaiEditLogic.SetSpreadGousya = False Then
            '        Throw New Exception()
            '    End If

            '    '入力監視フラグクリア
            '    _aInputWatcher.Clear()

            'Catch ex As Exception
            '    Dim msg As String
            '    msg = String.Format("号車情報のデータ表示で問題が発生しました(ERR={0})", ex.Message)
            '    ComFunc.ShowErrMsgBox(msg)
            '    'エラー発生でフォームを閉じる
            '    Me.Close()

            'Finally

            'End Try

        End Sub
#End Region

#End Region

#Region "メソッド"

#Region "基本が表示されているか返す"
        ''' <summary>
        ''' 基本が表示されているか返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function IsActiveBase() As Boolean
            Return spdBase.Visible
        End Function
#End Region

#Region "号車が表示されているか返す"
        ''' <summary>
        ''' 号車が表示されているか返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function IsActiveGousya() As Boolean
            Return spdGouSya.Visible
        End Function
#End Region

#Region "現在表示されているSpreadのSheetを返す"
        ''' <summary>
        ''' 現在表示されているSpreadのSheetを返す
        ''' </summary>
        ''' <returns>表示されているSheet</returns>
        ''' <remarks></remarks>
        Private Function GetActiveSheet() As FarPoint.Win.Spread.SheetView
            If IsActiveBase() Then
                Return spdBase_Sheet1
            ElseIf IsActiveGousya() Then
                Return spdGouSya_Sheet1
            End If
            Throw New NotImplementedException("未対応の状態です.")

        End Function

#End Region

#End Region

#Region "イベント"

#Region "フォームロード"
        ''' <summary>
        ''' フォームロード
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub frm20DispTehaichoEdit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Me.Focus()

            '↓↓2014/10/16 酒井 ADD BEGIN
            DispSai()
            '↑↑2014/10/16 酒井 ADD END
        End Sub
#End Region

#Region "フォームDisposed"
        Private Sub frm20DispTehaichoEdit_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed

            '一括フォームの開放
            If Not _frmNoukiIkkatsu Is Nothing Then
                _frmNoukiIkkatsu.Dispose()
            End If

            '手配帳ロジックを閉じる
            If Not _TehaiEditLogic Is Nothing Then
                _TehaiEditLogic.close()
            End If
            '↓↓2014/10/16 酒井 ADD BEGIN
            frm41SaiKakunin.Close()
            frm41SaiKakuninMin.Close()
            '↑↑2014/10/16 酒井 ADD END

        End Sub
#End Region

#Region "ボタン(カラー設定)"
        ''' <summary>
        ''' ボタン(カラー設定)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub frm20DispTehaichoEdit_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCOLOR.Click

            Dim cr As FarPoint.Win.Spread.Model.CellRange = GetActiveSheet.GetSelection(0)
            Dim row As Integer = 0
            If cr.Row < 2 Then
                row = 3
            Else
                row = cr.Row
            End If

            frmZColorMarker.ShowUnderButton(GetActiveSheet(), btnCOLOR)
            'frmZColorMarker.ShowUnderButton(GetActiveSheet(), row, btnCOLOR)

            If paraCOLOR <> String.Empty Then

                If LblTitle.Text.Equals(_TehaiEditLogic.TitleBase) Then
                    TehaichoEditLogic.SetColor(spdBase)
                End If
                If LblTitle.Text.Equals(_TehaiEditLogic.TitleGousya) Then
                    TehaichoEditLogic.SetColor(spdGouSya)
                End If

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
            Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(spdBase_Sheet1)

            If _TehaiEditLogic.IsBaseSpread Then
                spdBase_Sheet1.Cells.Item(startRow, 0, spdBase_Sheet1.RowCount - 1, spdBase_Sheet1.Columns.Count - 1).ResetBackColor()
            Else
                spdGouSya_Sheet1.Cells.Item(startRow, 0, spdGouSya_Sheet1.RowCount - 1, spdGouSya_Sheet1.Columns.Count - 1).ResetBackColor()
            End If

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

#Region "ボタン(基本)"
        ''' <summary>
        ''' ボタン(基本)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub BtnBase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBase.Click

            '既に基本の場合は切替しない
            If Me.spdBase.Visible = False Then
                '切り替え処理
                _TehaiEditLogic.VisibleButton(BtnBase)
            End If

        End Sub

#End Region

#Region "ボタン(号車)"
        ''' <summary>
        ''' ボタン(号車)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub BtnGouSya_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGouSya.Click

            '既に号車の場合は切替しない
            If Me.spdGouSya.Visible = False Then
                '切り替え処理
                _TehaiEditLogic.VisibleButton(BtnGouSya)
            End If

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
                '↓↓2014/10/16 酒井 ADD BEGIN
                frm41SaiKakunin.Close()
                frm41SaiKakuninMin.Close()
                '↑↑2014/10/16 酒井 ADD END
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
            If spdBase.Visible = True Then
                Kakudai1 += 0.2
                If Kakudai1 > 2 Then
                    MsgBox("これ以上拡大できません。", MsgBoxStyle.Information, "アラーム")
                    Kakudai1 = 2
                End If
                spdBase.ActiveSheet.ZoomFactor = Kakudai1
            Else
                Kakudai2 += 0.2
                If Kakudai2 > 2 Then
                    MsgBox("これ以上拡大できません。", MsgBoxStyle.Information, "アラーム")
                    Kakudai2 = 2
                End If
                spdGouSya.ActiveSheet.ZoomFactor = Kakudai2
            End If
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
            If spdBase.Visible = True Then
                Kakudai1 -= 0.2
                If Kakudai1 <= 0 Then
                    MsgBox("これ以上縮小できません。", MsgBoxStyle.Information, "アラーム")
                    Kakudai1 = 0.2
                End If
                spdBase.ActiveSheet.ZoomFactor = Kakudai1
            Else
                Kakudai2 -= 0.2
                If Kakudai2 <= 0 Then
                    MsgBox("これ以上縮小できません。", MsgBoxStyle.Information, "アラーム")
                    Kakudai2 = 0.2
                End If
                spdGouSya.ActiveSheet.ZoomFactor = Kakudai2
            End If
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
            If spdBase.Visible = True Then
                Kakudai1 = 1 'Nomalへ戻す
                spdBase.ActiveSheet.ZoomFactor = Kakudai1
            Else
                Kakudai2 = 1 'Nomalへ戻す
                spdGouSya.ActiveSheet.ZoomFactor = Kakudai2
            End If
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

                '表示シート・非表示シート間で共通列のデータ同期を行う
                _TehaiEditLogic.SheetDataSync()

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

                        '↓↓2014/10/16 酒井 ADD BEGIN
                        frm41SaiKakunin.Close()
                        frm41SaiKakuninMin.Close()
                        '↑↑2014/10/16 酒井 ADD END
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
                '確認フォームを表示
                Using frm As New frm20GousyaTenkaiKakunin("EXCEL出力選択", "出力方法を", _
                                              "選択してください。", _
                                              "出力", "キャンセル")
                    'ラジオボタンのラベルを設定
                    frm.RadioButton1.Text = "最新"
                    frm.RadioButton2.Text = "出図履歴含む"
                    '
                    frm.ShowDialog()

                    If frm.Result = MsgBoxResult.Ok Then  'OK
                        '表示シート・非表示シート間で共通列のデータ同期を行う
                        _TehaiEditLogic.SheetDataSync()
                        '出力方式
                        '出力処理
                        '   true:最新、False：出図履歴含む
                        _TehaiEditLogic.ExcelOutput(frm.RadioButton1.Checked)
                    End If
                End Using

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
        Private Sub btnExcelImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelImport.Click

            'Cursor.Current = Cursors.WaitCursor

            'Try
            _TehaiEditLogic.Import()

            Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(spdBase_Sheet1)
            spdBase_Sheet1.SetActiveCell(startRow, 1)
            spdGouSya_Sheet1.SetActiveCell(startRow, 1)

            'Catch ex As Exception

            '    Dim msg As String
            '    msg = String.Format("Excel取込で問題が発生しました(ERR={0})", ex.Message)
            '    ComFunc.ShowErrMsgBox(msg)
            'Finally
            '    Cursor.Current = Cursors.Default
            'End Try


        End Sub
#End Region

#Region "スプレッドChange基本"

        Private Sub spdBase_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdBase.CellDoubleClick

            Try
                '出力対象選択画面の選択された情報
                Dim selectedVos As List(Of TShisakuTehaiKihonVo)
                '
                Dim sheet As Spread.SheetView = spdBase_Sheet1

                Dim actColTag As String = sheet.Columns(e.Column).Tag.ToString

                Dim startRow As Integer = TehaichoEditLogic.GetTitleRowsIn(spdBase_Sheet1)
                'ヘッダーセルダブルクリック時はスルー
                If e.Row < startRow Then
                    Exit Sub
                End If

                '最新出図実績列の場合、処理続行
                If StringUtil.Equals(NmSpdTagBase.TAG_SHUTUZU_JISEKI_DATE, actColTag) Or _
                   StringUtil.Equals(NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO, actColTag) Or _
                   StringUtil.Equals(NmSpdTagBase.TAG_SHUTUZU_JISEKI_STSR_DHSTBA, actColTag) Then

                    'ダブルクリック行のブロック№、行ID、部品番号、代表品番を取得
                    Dim gyoId As String = _
                                sheet.GetText(e.Row, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagBase.TAG_GYOU_ID)).Trim
                    Dim blockNo As String = _
                                sheet.GetText(e.Row, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagBase.TAG_SHISAKU_BLOCK_NO)).Trim
                    Dim buhinNo As String = _
                                sheet.GetText(e.Row, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagBase.TAG_BUHIN_NO)).Trim
                    Dim shutuzuKaiteiNo As String = _
                                sheet.GetText(e.Row, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagBase.TAG_SHUTUZU_JISEKI_KAITEI_NO)).Trim

                    '最新出図が未セットの場合はスルー
                    If StringUtil.IsEmpty(shutuzuKaiteiNo) Then
                        ComFunc.ShowErrMsgBox("出図実績情報がありません。")
                        Exit Sub
                    End If

                    Dim daihyoHinban As String = String.Empty
                    If StringUtil.IsNotEmpty(_TehaiEditLogic.TehaiShutuzuJisekiKaitei(blockNo, buhinNo, shutuzuKaiteiNo)) Then
                        daihyoHinban = _TehaiEditLogic.TehaiShutuzuJisekiKaitei(blockNo, buhinNo, shutuzuKaiteiNo).KataDaihyouBuhinNo
                    End If

                    Dim lastKaiteiNo As String = _
                                sheet.GetText(e.Row, TehaichoEditLogic.GetTagIdx(sheet, NmSpdTagBase.TAG_SAISYU_SETSUHEN_KAITEI_NO)).Trim
                    '画面遷移
                    Using frmShutuzuKaiteiRireki As New Frm20ShutuzuKaiteiRireki(_TehaiEditLogic, gyoId, blockNo, buhinNo, lastKaiteiNo, daihyoHinban)
                        '
                        If frmShutuzuKaiteiRireki.InitComplete = True Then

                            frmShutuzuKaiteiRireki.ShowDialog()

                        End If
                        '戻るの場合、処理を終了
                        If frmShutuzuKaiteiRireki.ResultOk = False And frmShutuzuKaiteiRireki.ResultOkHanei = False Then
                            Return
                        End If

                        If frmShutuzuKaiteiRireki.ResultOk Then
                            'コメントを更新、手入力情報を更新
                            _TehaiEditLogic.UpdShutuzuKaiteiRireki(frmShutuzuKaiteiRireki, blockNo, buhinNo)
                        End If
                        If frmShutuzuKaiteiRireki.ResultOkHanei Then

                            'VOを返す
                            selectedVos = frmShutuzuKaiteiRireki.ResultList

                            If selectedVos.Count > 0 Then
                                '「T_SHISAKU_TEHAI_KIHON」情報を更新
                                If _TehaiEditLogic.UpdateDataSaishinOrikomi(selectedVos) = False Then
                                    Return
                                End If
                            End If

                        End If

                    End Using

                    Cursor.Current = Cursors.Default

                End If

            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("出図改訂履歴処理でエラーが発生しました。", vbCrLf, ex.Message))
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub



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
                '↓↓2014/10/16 酒井 ADD BEGIN
                frm41SaiKakunin.Close()
                frm41SaiKakuninMin.Close()
                '↑↑2014/10/16 酒井 ADD END
                Me.Close()
            Finally
            End Try

        End Sub
#End Region

#Region "スプレッドChange号車"
        ''' <summary>
        '''  スプレッドChange号車
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdGouSya_Change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdGouSya.Change

            Dim blockNo As String = spdGouSya_Sheet1.GetText(e.Row, _
                                    TehaichoEditLogic.GetTagIdx(spdGouSya_Sheet1, NmSpdTagGousya.TAG_SHISAKU_BLOCK_NO))

            Try

                Select Case e.Column
                    '号車列
                    Case Is >= TehaichoEditNames.START_COLUMMN_GOUSYA_NAME
                        '入力員数値を変換( -1 → **)
                        _TehaiEditLogic.ChangeInsuValue(e.Row)


                        Dim rireki As String = spdGouSya_Sheet1.GetText(e.Row, TehaichoEditLogic.GetTagIdx(spdGouSya_Sheet1, NmSpdTagGousya.TAG_RIREKI)).Trim

                        If rireki.Equals("*") Then
                            '員数差変更処理
                            _TehaiEditLogic.CalcInsuSa(e.Row, True)
                        Else
                            ''員数差非表示・基本スプレッドに合計員数設定
                            _TehaiEditLogic.CalcInsuSa(e.Row, False)

                        End If

                End Select

                Dim actCol As Integer = spdGouSya_Sheet1.ActiveColumn.Index
                Dim actRow As Integer = spdGouSya_Sheet1.ActiveRow.Index
                Dim actColTag As String = spdGouSya_Sheet1.Columns(actCol).Tag.ToString

                'セル変更対応処理
                _TehaiEditLogic.ChangeSpreadValueReflect(actRow, actCol, actColTag, txtInputSupport.Text, "")

            Catch ex As Exception
                MsgBox("値変更処理で問題が発生しました")
                '↓↓2014/10/16 酒井 ADD BEGIN
                frm41SaiKakunin.Close()
                frm41SaiKakuninMin.Close()
                '↑↑2014/10/16 酒井 ADD END
                Me.Close()
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

#Region "スプレッド号車マウスクリックイベント情報取得"


        ''' <summary>
        '''  スプレッド号車マウスクリックイベント情報取得
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdGousya_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles spdGouSya.MouseDown
            Dim sv As New FarPoint.Win.Spread.SpreadView(Me.spdGouSya)
            sv = Me.spdGouSya.GetRootWorkbook()

            'セル行列位置取得
            _clickCellPosition = sv.GetCellFromPixel(e.X, e.Y)

            If _clickCellPosition.Row = -1 Then
                _clickCellPosition = sv.GetRowHeaderCellFromPixel(e.X, e.Y)

            Else
                _clickCellPosition = Nothing
            End If

        End Sub
#End Region

#Region "スプレッドスクロール位置同期(基本→号車)"
        Private Sub spdBase_TopChange(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.TopChangeEventArgs) Handles spdBase.TopChange
            _TehaiEditLogic.rowNoScroll = e.NewTop
        End Sub
#End Region

#Region "スプレッドスクロール位置同期(号車→基本)"
        Private Sub spdGouSya_TopChange(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.TopChangeEventArgs) Handles spdGouSya.TopChange
            _TehaiEditLogic.rowNoScroll = e.NewTop
        End Sub
#End Region

#Region "メニュー納期一括設定クリック"
        ''' <summary>
        ''' 納期一括設定画面表示
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub msNoukiIkatuSettei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msNoukiIkatuSettei.Click
            Try

                '納期一括設定画面表示
                If _frmNoukiIkkatsu.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Me.Refresh()
                    _frmNoukiIkkatsu.Refresh()
                    Cursor.Current = Cursors.WaitCursor

                    '支給部品日付一括設定　(納期指定日の上書きを行わないので上から順に優先順位が高い）
                    _TehaiEditLogic.SetShikyuBuhinDate()

                    '納期情報(取引先)一括設定
                    _TehaiEditLogic.SetNoukiMaker()

                    '納期情報(購入品)一括設定
                    _TehaiEditLogic.SetKounyuNouki()

                    ComFunc.ShowInfoMsgBox("納期一括設定が完了しました。")

                End If

            Catch ex As Exception
                MsgBox("納期一括設定で問題が発生しました")
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub

#End Region

#Region "メニュー手配情報付加"
        ''' <summary>
        ''' メニュー手配情報付加"
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub msTehaiJyouhoufuka_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msTehaiJyouhoufuka.Click
            Try

                Using frmFuka As New frm20DispTehaichoEditFuka()

                    If frmFuka.ShowDialog = Windows.Forms.DialogResult.OK Then
                        Me.Refresh()
                        frmFuka.Refresh()
                        Cursor.Current = Cursors.WaitCursor

                        If _TehaiEditLogic.TeahiFuka(frmFuka) = True Then
                            ComFunc.ShowInfoMsgBox("付加情報設定が完了しました。")
                        End If

                    End If

                End Using

            Catch
                MsgBox("付加情報設定で問題が発生しました")
            Finally
                Cursor.Current = Cursors.Default
            End Try

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

            _TehaiEditLogic.Spread_BlockNo_FocusChange(Me.LblTitle.Text.Trim, cmbBlockNo.Text)

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

            If _TehaiEditLogic.IsBaseSpread Then
                sheet = spdBase_Sheet1
            Else
                sheet = spdGouSya_Sheet1
            End If

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
        Private Sub frm20DispTehaichoEdit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
            Dim downKey As Object
            Dim sheet As Spread.SheetView = _TehaiEditLogic.GetVisibleSheet
            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

            downKey = e.KeyCode()

            Select Case downKey
                Case Keys.F7
                    'F7で基本情報に切替 67
                    If _TehaiEditLogic.IsBaseSpread = False Then
                        _TehaiEditLogic.VisibleButton(BtnBase)
                    End If

                Case Keys.F8
                    'F8で号車情報に切替
                    If _TehaiEditLogic.IsBaseSpread = True Then

                        _TehaiEditLogic.VisibleButton(BtnGouSya)
                    End If


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
                        Dim listBln As List(Of Boolean()) = TehaichoEditLogic.GetEditCellInfo(sheet)
                        '書式を一時的に全て保存編集対象にする
                        TehaichoEditLogic.SetUndoCellFormat(sheet)
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
                                        '    TehaichoEditLogic.SetUndoCellFormat(sheet, listBln)
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
                        TehaichoEditLogic.SetUndoCellFormat(sheet, listBln)
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

                        listClip = TehaichoEditLogic.GetClipbordList

                        'ペーストをコードで記述すると安藤が出来ない！！
                        'spdBase_Sheet1.ClipboardPaste(Spread.ClipboardPasteOptions.Values)

                        '2012/02/01'
                        'スプレッド自身に貼り付けさせる'
                        Dim im As New Spread.InputMap
                        If Me.spdBase.Visible Then
                            spdBase.ClipboardOptions = Spread.ClipboardOptions.NoHeaders
                        Else
                            spdGouSya.ClipboardOptions = Spread.ClipboardOptions.NoHeaders
                        End If
                        im.Put(New Spread.Keystroke(Keys.V, Keys.Control), Spread.SpreadActions.ClipboardPasteValues)

                        If Not listClip Is Nothing Then

                            Dim rowCount As Integer = listClip.Count - 1
                            Dim colCount As Integer = listClip(0).Length

                            '単一コピーの場合'
                            'If rowCount = 1 Then
                            '    For col As Integer = 0 To selection.ColumnCount - 1
                            '        For rowindex As Integer = 0 To selection.RowCount - 1
                            '            If Not sheet.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                            '                '隠された行にはペーストしない'
                            '                If sheet.Rows(selection.Row + rowindex).Visible Then
                            '                    sheet.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                            '                End If
                            '            End If
                            '        Next
                            '    Next
                            'ElseIf rowCount > 1 Then
                            '    '複数コピーの場合'
                            '    For col As Integer = 0 To selection.ColumnCount - 1
                            '        For rowindex As Integer = 0 To selection.RowCount - 1
                            '            If Not sheet.Cells(selection.Row + rowindex, selection.Column + col).Locked Then
                            '                '隠された行にはペーストしない'
                            '                If sheet.Rows(selection.Row + rowindex).Visible Then
                            '                    sheet.Cells(selection.Row + rowindex, selection.Column + col).Value = listClip(0)(0)
                            '                Else
                            '                    '非表示なら'

                            '                End If
                            '            End If
                            '        Next
                            '    Next
                            'End If

                            'If (selection.Column + colCount) >= sheet.ColumnCount - 1 Then
                            '    ComFunc.ShowInfoMsgBox("貼り付けようとしている範囲がスプレッド表の最大列を超えています")
                            '    Return
                            'End If

                            If (selection.Row + rowCount) >= sheet.RowCount - 1 Then
                                ComFunc.ShowInfoMsgBox("貼り付けしようとしている範囲がスプレッド表の最大行を超えています")
                                Return
                            End If

                            '切取りされる行のブロックを保存ブロック対象として追加する
                            _TehaiEditLogic.SetEditRowProc(_TehaiEditLogic.IsBaseSpread, selection.Row, _
                                   selection.Column, rowCount, colCount)

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

                    '号車の列のみ左右の矢印キーで移動できる。
                    Dim gousyaMaxCount As Integer = TehaichoEdit.TehaichoEditNames.START_COLUMMN_GOUSYA_NAME
                    If IsActiveGousya() = True _
                       And gousyaMaxCount <= ParaActColIdx Then
                        '最大カラム数を以下の場合処理を行います。
                        If colMax > ParaActColIdx Then
                            '右へ移動します。
                            sheet.SetActiveCell(ParaActRowIdx, ParaActColIdx)
                        Else
                            '左へ移動後、再設定。かっこ悪いけどこれで。
                            sheet.SetActiveCell(ParaActRowIdx, ParaActColIdx - 2)
                            sheet.SetActiveCell(ParaActRowIdx, ParaActColIdx)
                        End If
                    End If


                Case Keys.Left

                    ' 選択セルの場所を特定します。
                    ParaActRowIdx = sheet.ActiveRowIndex
                    ' 選択セルの場所の右隣を特定します。
                    ParaActColIdx = sheet.ActiveColumnIndex - 1
                    '最大カラム数を取得します。
                    Dim colMax As Integer = sheet.ColumnCount

                    '号車の列のみ左右の矢印キーで移動できる。
                    Dim gousyaMaxCount As Integer = TehaichoEdit.TehaichoEditNames.START_COLUMMN_GOUSYA_NAME
                    If IsActiveGousya() = True _
                       And gousyaMaxCount - 1 <= ParaActColIdx Then
                        '最大カラム数を以下の場合処理を行います。
                        If ParaActColIdx > 0 Then
                            '左へ移動します。
                            sheet.SetActiveCell(ParaActRowIdx, ParaActColIdx)
                        End If
                    End If
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

            '表示スプレッドによって削除処理を切替える。
            If _TehaiEditLogic.IsGousyaSpread = True Then
                '号車スプレッドを表示している場合以下のとおり。

                '削除開始カラムが履歴、ブロック、行ＩＤに重なる場合、専用マークの位置をセットする。
                '使用不可の項目なので削除できてしまってはまずい。　2011/03/11　柳沼

                '員数差前までのセルの削除の場合。（カラム位置№９＝員数差）
                '号車員数のセルの削除の場合。（カラム位置№９＝員数差）
                If (fromCol + colCount) < 9 Or fromCol > 9 Then
                    If (fromCol + colCount) >= 4 Then
                        If fromCol <= 4 Then
                            colCount = colCount - (4 - fromCol)
                            If colCount <> 0 Then
                                fromCol = 4
                                datamodel.ClearData(fromRow, fromCol, rowCount, colCount)
                            End If
                        Else
                            datamodel.ClearData(fromRow, fromCol, rowCount, colCount)
                        End If
                    End If
                End If
                '号車員数のセルを間に挟んだ削除の場合。（カラム位置№９＝員数差）
                If fromCol < 9 And (fromCol + colCount) > 9 Then
                    '員数差前
                    If (fromCol + colCount) >= 4 Then
                        If fromCol <= 4 Then
                            colCount = colCount - (4 - fromCol)
                            If colCount <> 0 Then
                                fromCol = 4
                                'datamodel.ClearData(fromRow, fromCol, rowCount, 9 - fromCol)
                                For Row As Integer = fromRow To fromRow + rowCount - 1
                                    If spdBase_Sheet1.Rows(Row).Visible Then
                                        For col As Integer = fromCol To fromCol + colCount - 1
                                            asheetView.ClearRange(Row, col, 1, 1, True)
                                        Next
                                    End If
                                Next
                            End If
                        Else
                            'datamodel.ClearData(fromRow, fromCol, rowCount, 9 - fromCol)
                            For Row As Integer = fromRow To fromRow + rowCount - 1
                                If spdBase_Sheet1.Rows(Row).Visible Then
                                    For col As Integer = fromCol To fromCol + colCount - 1
                                        asheetView.ClearRange(Row, col, 1, 1, True)
                                    Next
                                End If
                            Next
                        End If
                    End If
                    '員数差以降
                    If (fromCol + colCount) >= 4 Then
                        If fromCol <= 4 Then
                            colCount = colCount - (4 - fromCol)
                            If colCount <> 0 Then
                                fromCol = 4
                                For Row As Integer = fromRow To fromRow + rowCount - 1
                                    If spdBase_Sheet1.Rows(Row).Visible Then
                                        For col As Integer = fromCol To fromCol + colCount - 1
                                            asheetView.ClearRange(Row, 10, 1, 1, True)
                                        Next
                                    End If
                                Next
                                'datamodel.ClearData(fromRow, 10, rowCount, colCount)
                            End If
                        Else
                            For Row As Integer = fromRow To fromRow + rowCount - 1
                                If spdBase_Sheet1.Rows(Row).Visible Then
                                    For col As Integer = fromCol To fromCol + colCount - 1
                                        asheetView.ClearRange(Row, 10, 1, 1, True)
                                    Next
                                End If
                            Next
                            'datamodel.ClearData(fromRow, 10, rowCount, colCount)
                        End If
                    End If
                End If
            Else
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

                                            '納入指示日の列を含むか？
                                            If _TehaiEditLogic.ChkNounyuShijibiCell(col) Then
                                                '納入指示日の値により背景色を変更
                                                _TehaiEditLogic.NounyuShijibiRowColorChange(Row)
                                            End If

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

                                        '納入指示日の列を含むか？
                                        If _TehaiEditLogic.ChkNounyuShijibiCell(col) Then
                                            '納入指示日の値により背景色を変更
                                            _TehaiEditLogic.NounyuShijibiRowColorChange(Row)
                                        End If

                                    Next
                                End If
                            Next
                        End If
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

                '履歴可否により員数差設定
                If asheetView.Cells(i, 0).Value = "*" Then
                    ''員数差表示
                    _TehaiEditLogic.CalcInsuSa(i, True)
                Else
                    ''員数差非表示・基本スプレッドに合計員数設定
                    _TehaiEditLogic.CalcInsuSa(i, False)
                End If

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
                Dim sheet As Spread.SheetView = _TehaiEditLogic.GetVisibleSheet
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

                Dim sheet As Spread.SheetView = _TehaiEditLogic.GetVisibleSheet
                Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

                'クリック位置情報有無
                If Not _clickCellPosition Is Nothing AndAlso _clickCellPosition.Row <> -1 Then
                    '削除される行のブロックを保存ブロック対象として追加する
                    _TehaiEditLogic.AddEditBlockList(selection)

                    '行削除処理
                    Me.spdBase_Sheet1.Rows.Remove(selection.Row, selection.RowCount)
                    Me.spdGouSya_Sheet1.Rows.Remove(selection.Row, selection.RowCount)

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

                _TehaiEditLogic.ToolPaste(_TehaiEditLogic.GetVisibleSheet)

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

            If ParaActColIdx = sheet.Columns(31).Index _
                 Or ParaActColIdx = sheet.Columns(32).Index _
                 Or ParaActColIdx = sheet.Columns(NmSpdTagBase.TAG_DATA_ITEM_KAITEI_INFO).Index Then
                '↑↑↑2014/12/26 メタル項目を追加 TES)張 CHG END
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
        ''' セルのIME設定(号車情報)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdGousya_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdGouSya.EditModeOn
            'アクティブなセルのIMEを制御する'
            Dim sheet As Spread.SheetView = spdGouSya_Sheet1
            ' 選択セルの場所を特定します。
            ParaActRowIdx = sheet.ActiveRowIndex                 'アクティブ行インデックス
            ParaActColIdx = sheet.ActiveColumnIndex              'アクティブ行インデックス

            'IMEを使用不可能にする。'
            spdGouSya.EditingControl.ImeMode = Windows.Forms.ImeMode.Disable
            spdGouSya.ImeMode = Windows.Forms.ImeMode.Disable
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
                Using cellSelect As New Frm20ExcelCellSelect(file)
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
                        Using keyDataSelect As New Frm20ExcelKeyDataSelect(file, cellSelect.activeSheet, Me.spdBase)
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
                                For index = cellSelect.activeSheet.RowCount - 1 To cellSelect.activeSheet.ActiveRowIndex + 1 Step -1
                                    Dim FlgMaxrow As Boolean = False
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
                                For indexSheetrow = sheet.RowCount - 1 To TehaichoEditLogic.GetTitleRowsIn(sheet) + 1 Step -1
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
                                    For indexSheetrow = TehaichoEditLogic.GetTitleRowsIn(sheet) + 1 To MaxrowSpdBase
                                        Dim flag As Boolean = True
                                        For Each indexcol In lstSelectCol
                                            Dim exlcol As Integer = Integer.Parse(keyDataSelect.spdSearch_Sheet1.Columns.Item(indexcol).Tag)
                                            Dim Sheetcol As Integer = Integer.Parse(keyDataSelect.spdSearch_Sheet1.Cells(0, indexcol).Value)
                                            ''キー項目が一致しないの場合
                                            If cellSelect.activeSheet.Cells(index, Sheetcol).Text <> sheet.Cells(indexSheetrow - 1, exlcol).Text Then
                                                flag = False
                                                Exit For
                                            End If
                                        Next
                                        'キー項目が一致の場合
                                        If flag Then
                                            For Each indexcol In lstUpdateCol
                                                Dim exlcol As Integer = Integer.Parse(keyDataSelect.spdUpdate_Sheet1.Columns.Item(indexcol).Tag)
                                                Dim Sheetcol As Integer = Integer.Parse(keyDataSelect.spdUpdate_Sheet1.Cells(0, indexcol).Value)
                                                If keyDataSelect.cbxTORIKOMISAKI_KUUHAKU.Checked And StringUtil.IsEmpty(sheet.Cells(indexSheetrow - 1, exlcol).Value) Then
                                                    sheet.Cells(indexSheetrow - 1, exlcol).Value = cellSelect.activeSheet.Cells(index, Sheetcol).Value
                                                ElseIf keyDataSelect.cbxTORIKOMIMOTO_KUUHAKU.Checked Then
                                                    sheet.Cells(indexSheetrow - 1, exlcol).Value = cellSelect.activeSheet.Cells(index, Sheetcol).Value
                                                End If
                                            Next
                                        End If
                                    Next
                                Next

                            End With

                            Dim progPath As String = My.Application.Info.DirectoryPath
                            Dim filePath As String = progPath & "\SHISAKU_TEHAI_IMPORT.ini"

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

            Dim isKatashiyou As Boolean = e.Column = sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_1).Index Or _
            e.Column = sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_2).Index Or _
            e.Column = sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_3).Index
            ' 幅を元に戻る
            If isKatashiyou Then
                e.EditingControl.Width = sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_1).Width
            End If
        End Sub

        Private Sub spdBase_ComboDropDown(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles spdBase.ComboDropDown
            Dim sheet As Spread.SheetView = spdBase_Sheet1

            Dim isKatashiyou As Boolean = e.Column = sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_1).Index Or _
                    e.Column = sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_2).Index Or _
                    e.Column = sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_3).Index

            ' （型仕様1～3コンボ）のコンボドロップダウン表示イベント
            If isKatashiyou Then
                Dim katashiyou1 As String = sheet.Cells(e.Row, sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_1).Index).Value
                Dim katashiyou2 As String = sheet.Cells(e.Row, sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_2).Index).Value
                Dim katashiyou3 As String = sheet.Cells(e.Row, sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_3).Index).Value
                Dim dropCell = spdBase_Sheet1.Cells(e.Row, e.Column)
                Dim fc As FpCombo = e.EditingControl

                ' セル幅を全角１５文字程度に拡張する。
                'dropCell.Column.Width = LARGER_WIDTH
                fc.Width = LARGER_WIDTH

                ' 型仕様1～3の内、他で選択済みの項目を選択不可にする。
                Select Case e.Column
                    Case sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_1).Index
                        If Not String.IsNullOrEmpty(katashiyou2) Then
                            fc.ItemData.Remove(katashiyou2)
                            fc.List.Remove(katashiyou2)
                        End If
                        If Not String.IsNullOrEmpty(katashiyou3) Then
                            fc.ItemData.Remove(katashiyou3)
                            fc.List.Remove(katashiyou3)
                        End If

                    Case sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_2).Index
                        If Not String.IsNullOrEmpty(katashiyou1) Then
                            fc.ItemData.Remove(katashiyou1)
                            fc.List.Remove(katashiyou1)
                        End If
                        If Not String.IsNullOrEmpty(katashiyou3) Then
                            fc.ItemData.Remove(katashiyou3)
                            fc.List.Remove(katashiyou3)
                        End If
                    Case sheet.Columns(NmSpdTagBase.TAG_TSUKURIKATA_KATASHIYOU_3).Index
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
        '↓↓2014/10/16 酒井 ADD BEGIN
        Private Sub DispSai()
            If frm41SaiKakunin.GetSai(Me._shisakuEventCode) Then
                frm41SaiKakunin.frm20 = Me
                frm41SaiKakunin.frm41 = Nothing
                frm41SaiKakuninMin.frm20 = Me
                frm41SaiKakuninMin.frm41 = Nothing
                btnSai.Text = "仕様差異　非表示"
                btnSai.Enabled = True
                frm41SaiKakunin.Show()
            End If
        End Sub
        Private Sub btnSai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSai.Click
            If btnSai.Text = "仕様差異　非表示" Then
                btnSai.Text = "仕様差異　表示"
                frm41SaiKakunin.Hide()
                frm41SaiKakuninMin.Show()
            Else
                btnSai.Text = "仕様差異　非表示"
                frm41SaiKakunin.Show()
                frm41SaiKakuninMin.Close()
            End If
        End Sub
        Public Sub UpdateBtnSai()
            If btnSai.Text = "仕様差異　非表示" Then
                btnSai.Text = "仕様差異　表示"
            Else
                btnSai.Text = "仕様差異　非表示"
            End If
        End Sub
        '↑↑2014/10/16 酒井 ADD END

        '↓↓↓2014/12/26 メタルブロック更新処理を追加 TES)張 ADD BEGIN
        Private Sub メタルブロック更新ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles メタルブロック更新ToolStripMenuItem.Click

            Try
                '確認フォームを表示
                Dim result As Integer = frm00Kakunin.Confirm("確認", "取り込みますか？", "", "はい", "いいえ")

                If result = MsgBoxResult.Ok Then  'OK
                    '取込処理
                    _TehaiEditLogic.MetaruBlockImport()
                Else
                    Return
                End If
            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("メタルブロック更新中にエラーが発生しました{0}{1}", vbCrLf, ex.Message))
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub
        '↑↑↑2014/12/26 メタルブロック更新処理を追加 TES)張 ADD END

        '↓↓↓2014/12/26 材料手配リスト作成処理を追加 TES)張 ADD BEGIN
        Private Sub 材料手配リスト作成ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 材料手配リスト作成ToolStripMenuItem.Click

            Dim msg As String

            Try
                Dim result As Integer = 0
                '材料情報・発注対象に☑が付いていている全データ分の「T_SHISAKU_TEHAI_KIHON」情報を抽出し、
                '「材料情報・発注対象最終更新年月日」がブランク（NULL）以外のデータをカウントする。
                Dim count As Integer = _TehaiEditLogic.CountTargetData()
                Dim checked As Boolean
                If count > 0 Then
                    '確認フォームを表示
                    Using frm As New frm20Kakunin("確認", "保存後、材料手配リストを作成しますか？", _
                                                  "出力済みの部品が " & count.ToString & " 件含まれています。", _
                                                  "はい", "いいえ")
                        frm.ShowDialog()
                        result = frm.Result
                        checked = frm.RadioButton1.Checked
                    End Using
                Else
                    '確認フォームを表示
                    result = frm00Kakunin.Confirm("確認", "保存後、" & vbCrLf & "材料手配リストを作成しますか？", _
                                                    "", "はい", "いいえ")
                End If

                '表示シート・非表示シート間で共通列のデータ同期を行う
                _TehaiEditLogic.SheetDataSync()

                If result = MsgBoxResult.Ok Then  'OK
                    Dim watch As New Stopwatch
                    watch.Start()

                    Cursor.Current = Cursors.WaitCursor
                    Me.Refresh()

                    If _TehaiEditLogic.Save() = True Then
                        watch.Stop()
                        Console.WriteLine(String.Format("保存実行時間:{0}ms ", watch.ElapsedMilliseconds))

                        ''変更フラグクリア
                        _aInputWatcher.Clear()
                    End If

                    '出力処理
                    _TehaiEditLogic.ZairyoListOutput(checked)

                    '変更フラグクリア
                    _aInputWatcher.Clear()

                Else
                    Return
                End If
                Cursor.Current = Cursors.Default
            Catch ex As ApplicationException
                msg = "Excel出力で問題が発生しました。既にファイルが開いている可能性があります。"
                ComFunc.ShowErrMsgBox(msg)
            Catch ex As Exception
                msg = String.Format("Excel出力で問題が発生しました(ERR={0})", ex.Message)
                ComFunc.ShowErrMsgBox(msg)
            Finally
                Cursor.Current = Cursors.Default
            End Try


            ''2015/05/08 追加 E.Ubukata
            ''画面クローズ処理
            ''　材料手配リスト(Excel)から非同期でテーブルへ更新が入るためクローズしておくことにする
            ''　その後の処理（Excelと画面の情報の同期）は運用でカバーしてもらうこととする
            Try
                '表示シート・非表示シート間で共通列のデータ同期を行う
                _TehaiEditLogic.SheetDataSync()

                Dim watch As New Stopwatch

                watch.Start()

                Cursor.Current = Cursors.WaitCursor
                Me.Refresh()

                If _TehaiEditLogic.Save() = True Then

                    watch.Stop()
                    Console.WriteLine(String.Format("保存実行時間:{0}ms ", watch.ElapsedMilliseconds))

                    '変更フラグクリア
                    _aInputWatcher.Clear()
                    ' ComFunc.ShowInfoMsgBox("保存が正常に完了しました", MessageBoxButtons.OK)

                    '↓↓2014/10/16 酒井 ADD BEGIN
                    frm41SaiKakunin.Close()
                    frm41SaiKakuninMin.Close()
                    '↑↑2014/10/16 酒井 ADD END
                    Me.Close()

                End If

                Cursor.Current = Cursors.Default
            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("保存中にエラーが発生しました{0}{1}", vbCrLf, ex.Message))
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub
        '↑↑↑2014/12/26 材料手配リスト作成処理を追加 TES)張 ADD END

        '↓↓↓2015/01/08 ツールバーの材質表示/非表示を追加 TES)張 ADD BEGIN
        Private Sub ToolStripGroupZaishitu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripGroupZaishitu.Click
            If ZAI = "N" Then
                ToolStripGroupZaishitu.Text = "材質非表示"
                ToolStripGroupZaishitu.ToolTipText = "材質を非表示にします"
                ToolStripGroupZaishitu.Checked = True
                _TehaiEditLogic.ZaishituColumnVisible()
                ZAI = "D"
            Else
                ToolStripGroupZaishitu.Text = "材質表示"
                ToolStripGroupZaishitu.ToolTipText = "材質を表示します"
                ToolStripGroupZaishitu.Checked = False
                _TehaiEditLogic.ZaishituColumnDisable()
                ZAI = "N"
            End If
        End Sub
        '↑↑↑2015/01/08 ツールバーの材質表示/非表示を追加 TES)張 ADD END

        '↓↓↓2015/02/13 材料の表示/非表示を切り替える機能を追加 daniel) ADD BEGIN
        Public Sub SetZaishituColumnDisable()
            ToolStripGroupZaishitu.Text = "材質表示"
            ToolStripGroupZaishitu.ToolTipText = "材質を表示します"
            ToolStripGroupZaishitu.Checked = False
            _TehaiEditLogic.ZaishituColumnDisable()
            ZAI = "N"
        End Sub
        '↑↑↑2015/02/13 材料の表示/非表示を切り替える機能を追加 daniel) ADD END

        '↓↓↓2015/01/09 設計メモ更新処理を追加 TES)張 ADD BEGIN
        Private Sub 設計メモ更新ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 設計メモ更新ToolStripMenuItem.Click

            Try
                '確認フォームを表示
                Dim result As Integer = frm00Kakunin.Confirm("確認", "設計メモを取り込みますか？", "", "はい", "いいえ")

                If result = MsgBoxResult.Ok Then  'OK
                    '取込処理
                    _TehaiEditLogic.SekkeiMemoImport()
                Else
                    Return
                End If
            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("設計メモ更新中にエラーが発生しました{0}{1}", vbCrLf, ex.Message))
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub
        '↑↑↑2015/01/09 設計メモ更新処理を追加 TES)張 ADD END

        ''↓↓↓2015/01/14 データ支給依頼書を追加 TES)劉 ADD BEGIN
        Private Sub データ支給依頼書ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles データ支給依頼書ToolStripMenuItem.Click

            '確認フォームを表示
            Dim result As Integer = frm00Kakunin.Confirm("確認", "保存後、" & vbCrLf & "データ支給依頼書を作成しますか？", "", "はい", "いいえ")

            '表示シート・非表示シート間で共通列のデータ同期を行う
            _TehaiEditLogic.SheetDataSync()

            If result = MsgBoxResult.Ok Then  'OK  
                Dim watch As New Stopwatch
                watch.Start()

                Cursor.Current = Cursors.WaitCursor
                Me.Refresh()

                If _TehaiEditLogic.Save() = True Then
                    watch.Stop()
                    Console.WriteLine(String.Format("保存実行時間:{0}ms ", watch.ElapsedMilliseconds))

                    ''変更フラグクリア
                    _aInputWatcher.Clear()
                End If
                Cursor.Current = Cursors.Default

                '基本情報SPREADよりデータを抽出する。
                Dim _ResultList As List(Of TShisakuTehaiKihonVo) = _TehaiEditLogic.GetDataProvisionOutputInfo()
                '出力対象選択画面の選択された情報
                Dim selectedVos As List(Of TShisakuTehaiKihonVo)

                '出力対象選択画面を表示
                Using frmDataSelect As New Frm20OutputDataSelect(_ResultList, _TehaiEditLogic, _shisakuEventCode, "DataProvision")
                    If frmDataSelect.InitComplete = True Then
                        'Me.Hide()
                        frmDataSelect.ShowDialog()
                    End If
                    '戻るの場合、処理を終了
                    If frmDataSelect.ResultOk = False Then
                        'Me.Show()
                        Return
                    End If

                    selectedVos = frmDataSelect.ResultList
                End Using

                '取引先、補足情報の入力画面を表示
                Using frmExport As New Frm20SikyuuExport(_shisakuEventCode, selectedVos, _TehaiEditLogic)
                    If frmExport.InitComplete = True Then
                        frmExport.ShowDialog()
                    End If
                    '戻るの場合、処理を終了
                    If frmExport.ResultOk = False Then
                        Return
                    End If

                    '「T_SHISAKU_TEHAI_KIHON」情報を更新
                    If _TehaiEditLogic.UpdateDataProvision(selectedVos) = False Then
                        'Me.Show()
                        Return
                    End If
                    '完了メッセージを表示
                    ComFunc.ShowInfoMsgBox("データ支給依頼書の出力が完了しました。", MessageBoxButtons.OK)

                    ''変更フラグクリア
                    _aInputWatcher.Clear()

                End Using

                Me.Refresh()
                'Me.Show()
            End If

        End Sub
        ''↑↑↑2015/01/14 データ支給依頼書を追加 TES)劉 ADD END

        ''↓↓↓2015/01/20 工事指令書を追加 TES)張 ADD BEGIN
        Private Sub 工事指令書ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 工事指令書ToolStripMenuItem.Click

            '確認フォームを表示
            Dim result As Integer = frm00Kakunin.Confirm("確認", "保存後、" & vbCrLf & "工事指令書を作成しますか？", "", "はい", "いいえ")

            '表示シート・非表示シート間で共通列のデータ同期を行う
            _TehaiEditLogic.SheetDataSync()

            If result = MsgBoxResult.Ok Then  'OK  
                Dim watch As New Stopwatch
                watch.Start()

                Cursor.Current = Cursors.WaitCursor
                Me.Refresh()

                If _TehaiEditLogic.Save() = True Then
                    watch.Stop()
                    Console.WriteLine(String.Format("保存実行時間:{0}ms ", watch.ElapsedMilliseconds))

                    ''変更フラグクリア
                    _aInputWatcher.Clear()
                End If
                Cursor.Current = Cursors.Default

                '基本情報SPREADよりデータを抽出する。
                Dim _ResultList As List(Of TShisakuTehaiKihonVo) = _TehaiEditLogic.GetKoujiShireiOutputInfo()
                '出力対象選択画面の選択された情報
                Dim selectedVos As List(Of TShisakuTehaiKihonVo)

                '出力対象選択画面を表示
                Using frmDataSelect As New Frm20OutputDataSelect(_ResultList, _TehaiEditLogic, _shisakuEventCode, "KojiShirei")
                    If frmDataSelect.InitComplete = True Then
                        'Me.Hide()
                        frmDataSelect.ShowDialog()
                    End If
                    '戻るの場合、処理を終了
                    If frmDataSelect.ResultOk = False Then
                        'Me.Show()
                        Return
                    End If

                    selectedVos = frmDataSelect.ResultList
                End Using

                '担当者、補足情報の入力画面を表示
                Using frmExport As New Frm20KojiShireiExport(_shisakuEventCode, selectedVos, _TehaiEditLogic)
                    If frmExport.InitComplete = True Then
                        frmExport.ShowDialog()
                    End If
                    '戻るの場合、処理を終了
                    If frmExport.ResultOk = False Then
                        'Me.Show()
                        Return
                    End If

                    '完了メッセージを表示
                    ComFunc.ShowInfoMsgBox("工事指令書の出力が完了しました。", MessageBoxButtons.OK)
                End Using

                Me.Refresh()
                'Me.Show()
            End If

        End Sub
        ''↑↑↑2015/01/20 工事指令書を追加 TES)張 ADD END

        ''↓↓↓2015/01/20 注文書を追加 TES)張 ADD BEGIN
        Private Sub 注文書ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 注文書ToolStripMenuItem.Click

            '確認フォームを表示
            Dim result As Integer = frm00Kakunin.Confirm("確認", "保存後、" & vbCrLf & "注文書を作成しますか？", "", "はい", "いいえ")

            '表示シート・非表示シート間で共通列のデータ同期を行う
            _TehaiEditLogic.SheetDataSync()

            If result = MsgBoxResult.Ok Then  'OK  
                Dim watch As New Stopwatch
                watch.Start()

                Cursor.Current = Cursors.WaitCursor
                Me.Refresh()

                If _TehaiEditLogic.Save() = True Then
                    watch.Stop()
                    Console.WriteLine(String.Format("保存実行時間:{0}ms ", watch.ElapsedMilliseconds))

                    ''変更フラグクリア
                    _aInputWatcher.Clear()
                End If
                Cursor.Current = Cursors.Default

                '基本情報SPREADよりデータを抽出する。
                Dim _ResultList As List(Of TShisakuTehaiKihonVo) = _TehaiEditLogic.GetOrderOutputInfo()
                '出力対象選択画面の選択された情報
                Dim selectedVos As List(Of TShisakuTehaiKihonVo)

                '出力対象選択画面を表示
                Using frmDataSelect As New Frm20OutputDataSelect(_ResultList, _TehaiEditLogic, _shisakuEventCode, "Order")
                    If frmDataSelect.InitComplete = True Then
                        'Me.Hide()
                        frmDataSelect.ShowDialog()
                    End If
                    '戻るの場合、処理を終了
                    If frmDataSelect.ResultOk = False Then
                        'Me.Show()
                        Return
                    End If

                    selectedVos = frmDataSelect.ResultList
                End Using

                '取引先、補足情報の入力画面を表示
                Using frmExport As New Frm20OrderExport(_shisakuEventCode, selectedVos, _TehaiEditLogic)
                    If frmExport.InitComplete = True Then
                        frmExport.ShowDialog()
                    End If
                    '戻るの場合、処理を終了
                    If frmExport.ResultOk = False Then
                        'Me.Show()
                        Return
                    End If

                    '完了メッセージを表示
                    ComFunc.ShowInfoMsgBox("注文書の出力が完了しました。", MessageBoxButtons.OK)
                End Using

                Me.Refresh()
                'Me.Show()
            End If

        End Sub
        ''↑↑↑2015/01/20 注文書を追加 TES)張 ADD END

        '↓↓↓2015/01/27 チェックをクリックした場合、メッセージ表示 TES)張 ADD BEGIN
        Private Sub spdBase_ButtonClicked(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles spdBase.ButtonClicked

            Dim aRow As Integer = e.Row
            Dim aCol As Integer = e.Column
            Dim actColTag As String = spdBase_Sheet1.Columns(aCol).Tag.ToString

            Select Case actColTag
                Case NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_TARGET
                    '発注対象
                    If spdBase_Sheet1.GetValue(aRow, aCol) = True Then
                        '確認フォームを表示
                        Dim result As Integer = frm00Kakunin.Confirm("確認", "発注対象を解除しますか？", "", "はい", "いいえ")
                        If result = MsgBoxResult.Ok Then
                            spdBase_Sheet1.SetValue(aRow, aCol, False)
                            '材料情報・発注済のチェックボックスは使用不可（チェック不可）にする。
                            spdBase_Sheet1.Cells(aRow, TehaichoEditLogic.GetTagIdx(spdBase_Sheet1, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Locked = True
                        Else
                            spdBase_Sheet1.SetValue(aRow, aCol, True)
                        End If
                    Else
                        spdBase_Sheet1.SetValue(aRow, aCol, True)
                        '材料情報・発注済のチェックボックスは使用可能（チェック可能）にする。
                        spdBase_Sheet1.Cells(aRow, TehaichoEditLogic.GetTagIdx(spdBase_Sheet1, NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK)).Locked = False
                    End If
                Case NmSpdTagBase.TAG_MATERIAL_INFO_ORDER_CHK
                    '発注済
                    If spdBase_Sheet1.GetValue(aRow, aCol) = True Then
                        '確認フォームを表示
                        Dim result As Integer = frm00Kakunin.Confirm("確認", "発注済を解除しますか？", "", "はい", "いいえ")
                        If result = MsgBoxResult.Ok Then
                            spdBase_Sheet1.SetValue(aRow, aCol, False)
                        Else
                            spdBase_Sheet1.SetValue(aRow, aCol, True)
                        End If
                    Else
                        '確認フォームを表示
                        Dim result As Integer = frm00Kakunin.Confirm("確認", "発注済に変更しますか？", "", "はい", "いいえ")
                        If result = MsgBoxResult.Ok Then
                            spdBase_Sheet1.SetValue(aRow, aCol, True)
                        Else
                            spdBase_Sheet1.SetValue(aRow, aCol, False)
                        End If
                    End If
                Case NmSpdTagBase.TAG_DATA_ITEM_DATA_PROVISION
                    'データ支給チェック欄
                    If spdBase_Sheet1.GetValue(aRow, aCol) = True Then
                        '確認フォームを表示
                        Dim result As Integer = frm00Kakunin.Confirm("確認", "データ支給済を解除しますか？", "", "はい", "いいえ")
                        If result = MsgBoxResult.Ok Then
                            spdBase_Sheet1.SetValue(aRow, aCol, False)
                        Else
                            spdBase_Sheet1.SetValue(aRow, aCol, True)
                        End If
                    Else
                        '確認フォームを表示
                        Dim result As Integer = frm00Kakunin.Confirm("確認", "データ支給済に変更しますか？", "", "はい", "いいえ")
                        If result = MsgBoxResult.Ok Then
                            spdBase_Sheet1.SetValue(aRow, aCol, True)
                        Else
                            spdBase_Sheet1.SetValue(aRow, aCol, False)
                        End If
                    End If

            End Select

        End Sub
        '↑↑↑2015/01/27 チェックを外した場合、メッセージ表示 TES)張 ADD END

        Private Sub 号車別納期設定ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 号車別納期設定ToolStripMenuItem.Click

            Using frmGousya As New frm20GousyaNoukiSettei(_TehaiEditLogic)
                '初期化正常完了で編集画面表示
                If frmGousya.InitComplete = True Then

                    frmGousya.ShowDialog()

                End If
                '戻るの場合、処理を終了
                If frmGousya.ResultOk = False Then
                    'Me.Show()
                    Return
                End If
            End Using

        End Sub

        Private Sub 材料寸法取得ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 材料寸法取得ToolStripMenuItem1.Click
            Try
                'マウスクリック位置情報有
                If Not _clickCellPosition Is Nothing AndAlso _clickCellPosition.Row <> -1 Then
                    '確認フォームを表示
                    Dim result As Integer = frm00Kakunin.Confirm("確認", "材料寸法を取得しますか？", "", "はい", "いいえ")

                    Dim sheet As Spread.SheetView = _TehaiEditLogic.GetVisibleSheet
                    Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

                    Dim buhinNo As String = spdBase_Sheet1.GetText(selection.Row, _
                                                                   TehaichoEditLogic.GetTagIdx(spdBase_Sheet1, NmSpdTagBase.TAG_BUHIN_NO))
                    Dim areaName As String = spdBase_Sheet1.GetText(selection.Row, _
                                                                   TehaichoEditLogic.GetTagIdx(spdBase_Sheet1, NmSpdTagBase.TAG_DATA_ITEM_AREA_NAME))
                    Dim setName As String = spdBase_Sheet1.GetText(selection.Row, _
                                                                   TehaichoEditLogic.GetTagIdx(spdBase_Sheet1, NmSpdTagBase.TAG_DATA_ITEM_SET_NAME))

                    If result = MsgBoxResult.Ok Then  'OK
                        '取得処理
                        _TehaiEditLogic.ZairyoSunpoImport(buhinNo, areaName, setName, selection.Row, selection.Column)
                    End If

                    _clickCellPosition = Nothing
                Else
                    MsgBox("ContextMenuが行見出し位置のみで表示されるように調整中")
                End If

            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("材料寸法取得中にエラーが発生しました{0}{1}", vbCrLf, ex.Message))
            Finally
                Cursor.Current = Cursors.Default
            End Try
        End Sub

        Private Sub 号車発注展開ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 号車発注展開ToolStripMenuItem.Click
            Try

                '確認フォームを表示
                Using frm As New frm20GousyaTenkaiKakunin("ユニット区分選択", "納入指示日のユニット区分を", _
                                              "選択してください。", _
                                              "ＯＫ", "キャンセル")
                    frm.ShowDialog()

                    If frm.Result = MsgBoxResult.Ok Then  'OK

                        Dim unitKbn As String = String.Empty
                        If frm.RadioButton1.Checked = True Then
                            unitKbn = "T"  'トリム
                        Else
                            unitKbn = "M"  'メタル
                        End If

                        'エラーチェック
                        '   １．号車別の納期設定がされていない場合はエラー
                        '   ２．員数が未入力の場合はエラー　？？？　未入力の場合は処理をスルーでＯＫかな？
                        If _TehaiEditLogic.FunctionGousyaTenkaiCheck(unitKbn) = False Then
                            Exit Sub
                        End If

                        '確認フォームを表示
                        If MsgBoxResult.Ok = frm00Kakunin.Confirm("確認", "納期自動分解を実行します。" & vbCrLf & "よろしいですか？", _
                                                        "", "はい", "いいえ") Then

                            Dim clickRowNo As Integer = 0
                            Dim sheet As Spread.SheetView = _TehaiEditLogic.GetVisibleSheet
                            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

                            'マウスクリック位置情報有
                            If Not _clickCellPosition Is Nothing AndAlso _clickCellPosition.Row <> -1 Then

                                '号車発注展開処理へ
                                _TehaiEditLogic.GousyaTenkaiSpread(selection.Row, unitKbn, selection.RowCount)
                                _clickCellPosition = Nothing

                            Else
                                MsgBox("ContextMenuが行見出し位置のみで表示されるように調整中")
                            End If

                        End If

                    End If

                End Using

            Catch ex As Exception
                MsgBox(String.Format("号車発注展開時にシステムエラーが発生しました:{0}", ex.Message))
            End Try
        End Sub

        Private Sub インポート用部品表EXCEL出力最新ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles インポート用部品表EXCEL出力最新ToolStripMenuItem.Click
            Try
                Dim _group As String = String.Empty

                Using frmGousya As New frm20BuhinhyoExcelOutput(_TehaiEditLogic)
                    '初期化正常完了で編集画面表示
                    If frmGousya.InitComplete = True Then

                        frmGousya.ShowDialog()

                    End If
                    '戻るの場合、処理を終了
                    If frmGousya.ResultOk = False Then
                        Return
                    End If

                    'グループを取得
                    If StringUtil.Equals(frmGousya.cmbGroupValue, "ALL") Then
                        _group = ""
                    Else
                        _group = frmGousya.cmbGroupValue
                    End If

                    '
                    '試作手配帳情報（号車グループ情報）へ追加
                    _TehaiEditLogic.AddGousyaGroup(frmGousya)

                End Using

                Dim watch As New Stopwatch
                watch.Start()

                Cursor.Current = Cursors.WaitCursor
                Me.Refresh()


                If _TehaiEditLogic.Save() = True Then
                    watch.Stop()
                    Console.WriteLine(String.Format("保存実行時間:{0}ms ", watch.ElapsedMilliseconds))

                    ''変更フラグクリア
                    _aInputWatcher.Clear()
                End If

                '新調達形式で手配帳を出力する。
                Try
                    'リストコードの取得'
                    Dim impl As TehaichoHeaderDao = New TehaichoHeaderDaoImpl
                    Dim listcodeVo As New TShisakuListcodeVo
                    listcodeVo = impl.FindByListCode(_shisakuEventCode, _shisakuListCode)

                    'EXCEL出力
                    Dim ExcelShinChotatsu As New Excel.BuhinExportShinchotatsu(listcodeVo.ShisakuEventCode, _
                                                                          listcodeVo.ShisakuListCode, _
                                                                          listcodeVo.ShisakuListCodeKaiteiNo, 0, _
                                                                          _group)

                    '裏にメッセージが隠れることがあるのでとりあえず１秒間待機させる'
                    System.Threading.Thread.Sleep(1000)

                    MsgBox("部品表EXCEL出力（新調達への転送形式）が終了しました。", MsgBoxStyle.Information, "正常終了")
                Catch ex As Exception
                    MsgBox("Excelの書込みに失敗しました。", MsgBoxStyle.Information, "エラー")
                End Try

                Cursor.Current = Cursors.Default
            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("部品表EXCEL（最新）出力中にエラーが発生しました{0}{1}", vbCrLf, ex.Message))
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub

        Private Sub 出図実績最新と織込の差部品一覧ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 出図実績最新と織込の差部品一覧ToolStripMenuItem.Click
            Try

                '基本情報SPREADよりデータを抽出する。
                Dim _ResultList As List(Of TShisakuTehaiKihonVo) = _TehaiEditLogic.GetTehaiShutuzuJisekiOrikomiInfo

                ''出力対象選択画面の選択された情報
                'Dim selectedVos As List(Of TShisakuTehaiKihonVo)

                Using frmShutuzuOrikomi As New Frm20ShutuzuOrikomi(_ResultList, _TehaiEditLogic)
                    '初期化正常完了で編集画面表示
                    If frmShutuzuOrikomi.InitComplete = True Then

                        frmShutuzuOrikomi.ShowDialog()

                    End If
                    '戻るの場合、処理を終了
                    If frmShutuzuOrikomi.ResultOk = False Then
                        Return
                    End If

                    '保存ボタンでも反映する場合は下記を復活させる
                    '------------------------------------------------------------------------------
                    ''VOを返す
                    'selectedVos = frmShutuzuOrikomi.ResultList

                    ''「T_SHISAKU_TEHAI_KIHON」情報を更新
                    'If _TehaiEditLogic.UpdateDataSaishinOrikomi(selectedVos) = False Then
                    '    Return
                    'End If
                    '------------------------------------------------------------------------------

                    '
                    '試作手配出図織込情報を更新
                    _TehaiEditLogic.UpdShutuzuOrikomi(frmShutuzuOrikomi)

                End Using

                Cursor.Current = Cursors.Default
            Catch ex As Exception
                ComFunc.ShowErrMsgBox(String.Format("部品表EXCEL（最新）出力中にエラーが発生しました{0}{1}", vbCrLf, ex.Message))
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Sub

        Private Sub 納期発注展開NToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 納期発注展開ToolStripMenuItem.Click

            Try

                '確認フォームを表示
                Using frm As New frm20GousyaTenkaiKakunin("ユニット区分選択", "納入指示日のユニット区分を", _
                                              "選択してください。", _
                                              "ＯＫ", "キャンセル")
                    frm.ShowDialog()

                    If frm.Result = MsgBoxResult.Ok Then  'OK

                        Dim unitKbn As String = String.Empty
                        If frm.RadioButton1.Checked = True Then
                            unitKbn = "T"  'トリム
                        Else
                            unitKbn = "M"  'メタル
                        End If

                        'エラーチェック
                        '   １．号車別の納期設定がされていない場合はエラー
                        '   ２．員数が未入力の場合はエラー　？？？　未入力の場合は処理をスルーでＯＫかな？
                        If _TehaiEditLogic.FunctionGousyaTenkaiCheck(unitKbn) = False Then
                            Exit Sub
                        End If

                        '確認フォームを表示
                        If MsgBoxResult.Ok = frm00Kakunin.Confirm("確認", "納期自動分解を実行します。" & vbCrLf & "よろしいですか？", _
                                                        "", "はい", "いいえ") Then

                            Dim clickRowNo As Integer = 0
                            Dim sheet As Spread.SheetView = _TehaiEditLogic.GetVisibleSheet
                            Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

                            'マウスクリック位置情報有
                            If Not _clickCellPosition Is Nothing AndAlso _clickCellPosition.Row <> -1 Then

                                '納期発注展開処理へ
                                _TehaiEditLogic.GousyaNoukiTenkaiSpread(selection.Row, unitKbn, selection.RowCount)
                                _clickCellPosition = Nothing

                            Else
                                MsgBox("ContextMenuが行見出し位置のみで表示されるように調整中")
                            End If

                        End If

                    End If

                End Using

            Catch ex As Exception
                MsgBox(String.Format("納期発注展開時にシステムエラーが発生しました:{0}", ex.Message))
            End Try

        End Sub

        Private Sub 図面表示ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 図面表示ToolStripMenuItem.Click
            'マウスクリック位置情報有
            If Not _clickCellPosition Is Nothing AndAlso _clickCellPosition.Row <> -1 Then

                Dim sheet As Spread.SheetView = _TehaiEditLogic.GetVisibleSheet
                Dim selection As FarPoint.Win.Spread.Model.CellRange = sheet.GetSelection(0)

                Dim buhinNo As String = spdBase_Sheet1.GetText(selection.Row, _
                                                               TehaichoEditLogic.GetTagIdx(spdBase_Sheet1, NmSpdTagBase.TAG_BUHIN_NO))

                If StringUtil.IsNotEmpty(buhinNo) Then  '部品№があれば
                    '図面表示
                    _TehaiEditLogic.SettsuLogon(buhinNo, LoginInfo.Now.UserId)
                End If

                _clickCellPosition = Nothing
            Else
                MsgBox("ContextMenuが行見出し位置のみで表示されるように調整中")
            End If

        End Sub

        Private Sub 出図取込更新ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 出図取込更新ToolStripMenuItem.Click
            If MsgBox("出図情報を取り込みますか？", MsgBoxStyle.YesNo, "確認") = MsgBoxResult.Yes Then

                ''orderSheetの取込機能拡張版をキックする'
                '_TehaiEditLogic.UpdateZumen()

                Try

                    '図面設通情報を抽出する。
                    Dim _ResultList As List(Of TShisakuTehaiShutuzuJisekiVo) = _TehaiEditLogic.UpdateZumen

                    Using frmShutuzuTorikomi As New Frm20ShutuzuTorikomi(_ResultList, _TehaiEditLogic)
                        '初期化正常完了で編集画面表示
                        If frmShutuzuTorikomi.InitComplete = True Then

                            frmShutuzuTorikomi.ShowDialog()

                        End If
                        '戻るの場合、処理を終了
                        If frmShutuzuTorikomi.ResultOk = False Then
                            Return
                        End If

                        '
                        '試作手配出図取込情報を更新
                        _TehaiEditLogic.InsertTShisakuTehaiShutuzuJisseki(_ResultList)
                        '
                        '試作手配基本情報を更新
                        '「T_SHISAKU_TEHAI_KIHON」情報を更新
                        If _TehaiEditLogic.UpdateDataSaishinShutuzu(_ResultList) = False Then
                            Return
                        End If

                    End Using

                    Cursor.Current = Cursors.Default
                Catch ex As Exception
                    ComFunc.ShowErrMsgBox(String.Format("部品表EXCEL（最新）出力中にエラーが発生しました{0}{1}", vbCrLf, ex.Message))
                Finally
                    Cursor.Current = Cursors.Default
                End Try

            End If
        End Sub
    End Class

#End Region

End Namespace

