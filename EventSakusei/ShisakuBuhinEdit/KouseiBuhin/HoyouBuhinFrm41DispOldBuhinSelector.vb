Imports EBom.Data
Imports EBom.Common
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports ShisakuCommon.Ui.Spd
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports System.Text
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports FarPoint.Win
Imports FarPoint.Win.Spread.CellType
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Ui
Imports EventSakusei.ShisakuBuhinEdit.Kosei

Namespace KouseiBuhin

    Public Class HoyouBuhinFrm41DispOldBuhinSelector

        Private ToolTipRange As FarPoint.Win.Spread.Model.CellRange

        '''========================================================================
        ''' <summary>ダイアログ戻り値クラス</summary>
        ''' ---------------------------------------------------------------------
        ''' = ◆ 備考
        ''' ---------------------------------------------------------------------
        ''' <remarks>
        '''      <para>ダイアログの戻り値として使用します。</para>
        '''      <para>戻り値としてNothing判定ができるため便宜的にクラスにしています。</para>
        '''      <para>戻り値を単純にString型とすると、NullとNothing判定が難しいのです。</para>
        ''' </remarks>
        ''' ---------------------------------------------------------------------
        Public Class EditDialogResult
            '''' <summary>編集されたテキスト</summary>
            'Public _TextValue As String

            Public _BuhinList As List(Of ShisakuCommon.Db.EBom.Vo.THoyouBuhinEditVo)

            ''' <summary>コンストラクタ</summary>
            Sub New()
                Me._BuhinList = _BuhinList
            End Sub

        End Class

        '''========================================================================
        ''' <summary>ダイアログ戻り値格納メンバ</summary>
        '''========================================================================
        Private d_MyBuhinList As EditDialogResult
        Private d_MyEditText As EditDialogResult = Nothing

        ''' <summary>
        '''      <para>ダイアログ表示メソッド</para>
        ''' </summary>
        ''' <param name="avOwner">
        '''      <para>このダイアログのオーナーウィンドウ</para>
        ''' </param>
        ''' <returns>
        '''      <para>正常又はエラー場合、値が返ります。</para>
        '''      <para>キャンセルされたときNothingが返ります。</para>
        ''' </returns>
        ''' <remarks>
        '''      <para>元々あったShowメソッドを隠してしまいます</para>
        ''' </remarks>
        Public Shadows Function Show(Optional ByVal avOwner As System.Windows.Forms.IWin32Window = Nothing) As EditDialogResult

            ' ====== 編集テキスト用の新しいインスタンスを生成
            d_MyBuhinList = New EditDialogResult

            ' 変更前の設定
            MyBase.FormBorderStyle = FormBorderStyle.FixedDialog
            ' 最大化ボタン（使用不可）
            MyBase.MaximizeBox = False

            ' 変更後の設定
            ' サイズ変更可のウィンドウに変更する
            'MyBase.FormBorderStyle = FormBorderStyle.Sizable
            ' 最大化ボタン（使用可）
            'MyBase.MaximizeBox = True

            ' ====== 基底クラスのダイアログを開く(このクラスのShowDialogは殺している)
            If avOwner Is Nothing Then
                MyBase.Show()
            Else
                MyBase.Show(avOwner)
            End If

            ' ====== 編集された値を返す
            Return d_MyBuhinList
        End Function

#Region "メンバー変数"

        Private _resultOk As Boolean
        Private instlRowCount As Integer

        '''<summary>初期化完了フラグ</summary>>
        Private _InitComplete As Boolean = False

        ''' <summary>画面制御ロジック</summary>
        Private _OldBuhinSelectorLogic As Logic.HoyouBuhinOldBuhinSelectorSubject

        Private hoyouEventCode As String
        Private hoyouBukaCode As String
        Private hoyouTantoKey As String
        Private hoyouTanto As String

        Private ReadOnly koseiSubject As HoyouBuhinBuhinEditKoseiSubject
        Private ReadOnly detector As HoyouBuhinDetectLatestStructure

        ''' <summary>E-BOM部品情報フォーム</summary>
        Private _frmDispShisakuBuhinEdit20 As HoyouBuhinFrm41DispShisakuBuhinEdit20
        Private koseiObserver As HoyouBuhinSpdKoseiObserver

#End Region

#Region "プロパティ"
        ''' <summary>
        ''' 「取込」押下かを返す
        ''' </summary>
        Public ReadOnly Property ResultOk() As Boolean
            Get
                Return _resultOk
            End Get
        End Property
#End Region

#Region "コンストラクタ"

        Public Sub New(ByVal hoyouEventCode As String, ByVal hoyouBukaCode As String, ByVal hoyouTanto As String, _
                       ByVal koseiSubject As HoyouBuhinBuhinEditKoseiSubject, ByVal frm41Disp20 As HoyouBuhinFrm41DispShisakuBuhinEdit20)

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            '画面制御ロジック
            _OldBuhinSelectorLogic = New Logic.HoyouBuhinOldBuhinSelectorSubject(hoyouEventCode, Me)

            '初期化メイン
            Initialize()

            '親画面のフォームから
            _frmDispShisakuBuhinEdit20 = frm41Disp20
            Me.koseiSubject = koseiSubject
            Dim frm As HoyouBuhinFrm41DispShisakuBuhinEdit20 = _frmDispShisakuBuhinEdit20
            Me.koseiObserver = New HoyouBuhinSpdKoseiObserver(frm.spdParts, koseiSubject)

            Me.instlRowCount = instlRowCount

            Me.hoyouEventCode = hoyouEventCode
            Me.hoyouBukaCode = hoyouBukaCode
            Me.hoyouTantoKey = hoyouTanto & "_KEY"
            Me.hoyouTanto = hoyouTanto

        End Sub

#End Region

#Region "メソッド"

#Region "初期化メイン"

        ''' <summary>
        ''' 初期化メイン
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Initialize()
            Try

                Cursor.Current = Cursors.WaitCursor

                'ヘッダー部を初期化する
                _OldBuhinSelectorLogic.InitializeHeader()

                '初期化完了
                _InitComplete = True
                '呼出しボタン使用不可
                SetCallLock()

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

#Region "イベント"

        ''' <summary>
        ''' 時間動く機能
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub

        ''' <summary>
        ''' 戻る
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Me.Close()
        End Sub

        ''' <summary>
        ''' アプリケーション終了
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
            Application.Exit()
            System.Environment.Exit(0)
        End Sub

        Private Sub spdParts_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellClick
            If e.ColumnHeader Then
                Exit Sub
            End If
            txtEventNo.Text = spdParts_Sheet1.GetText(e.Row, 0)
            txtBuka.Text = spdParts_Sheet1.GetText(e.Row, 2)
            txtTantoCd.Text = spdParts_Sheet1.GetText(e.Row, 3)
            txtTantoName.Text = spdParts_Sheet1.GetText(e.Row, 4)
            txtTantoKaiteiNo.Text = spdParts_Sheet1.GetText(e.Row, 5)
            If StringUtil.IsNotEmpty(txtEventNo.Text) Then
                SetCallUnlock()
            Else
                SetCallLock()
            End If
        End Sub

        '''' <summary>
        '''' 列が自動ソートされたら　処理する
        '''' </summary>
        '''' <param name="sender"></param>
        '''' <param name="e"></param>
        '''' <remarks></remarks>
        'Private Sub spdParts_AutoSortedColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoSortedColumnEventArgs)
        '    _OldBuhinSelectorLogic.AutoSortedColumn(sender, e)
        'End Sub

        '''' <summary>
        '''' 列の自動フィルタリングが実行されたら　処理する
        '''' </summary>
        '''' <param name="sender"></param>
        '''' <param name="e"></param>
        '''' <remarks></remarks>
        'Private Sub spdParts_AutoFilteredColumn(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.AutoFilteredColumnEventArgs)
        '    _OldBuhinSelectorLogic.AutoFilteredColumn(sender, e)
        'End Sub

        ''' <summary>
        ''' ダブルクリックしたら　処理する
        ''' 補用設計担当情報から、「補用部品編集情報」を読み出し、
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub spdParts_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick
            'クリックされたセルが列ヘッダであるかどうかを表す値を取得します。
            If e.ColumnHeader Then
                Exit Sub
            End If
            ' バックカラーを戻る
            _OldBuhinSelectorLogic.ResetColor()

            Try

                '選択項目の確保
                Dim EventCode As String = spdParts_Sheet1.GetText(e.Row, spdParts_Sheet1.Columns("HOYOU_EVENT_CODE").Index())     '補用イベントコード
                Dim BukaCode As String = spdParts_Sheet1.GetText(e.Row, spdParts_Sheet1.Columns("HOYOU_BUKA_CODE").Index())      '補用部課コード
                Dim Tanto As String = spdParts_Sheet1.GetText(e.Row, spdParts_Sheet1.Columns("HOYOU_TANTO").Index())         '補用担当
                Dim KaiteiNo As String = spdParts_Sheet1.GetText(e.Row, spdParts_Sheet1.Columns("HOYOU_TANTO_KAITEI_NO").Index())      '補用担当改訂№

                OldBuhinCall(EventCode, BukaCode, Tanto, KaiteiNo)

                ''クローズ処理イベント取得用クラス
                'Dim closer As frm00Kakunin.IFormCloser = New CopyFormCloser(koseiObserver)

                ''取込内容確認フォームを表示
                'frm00Kakunin.ConfirmShow("過去の部品情報の確認", "部品編集情報を確認してください。", _
                '                               "「置き換え」ますか？", "「置き換え」", "元に戻す", closer)

                'Me.Close()  '処理終了

                'Dim MessageLine1 As String = "過去の部品情報を部品編集画面へ「置き換え」ますか？"
                'If frm01Kakunin.ConfirmOkCancel(MessageLine1) = MsgBoxResult.Ok Then
                '    'Me.Show()
                '    ' Excel取込機能(バックアップ削除)
                '    koseiObserver.importBacupRemove()
                '    Me.Close()  '処理終了
                'Else
                '    '-----------------------------------------------------------------------------------------------------
                '    '   処理中画面表示
                '    Dim SyorichuForm As New frm03Syorichu
                '    SyorichuForm.lblKakunin.Text = "編集中の部品情報を戻しています。"
                '    SyorichuForm.lblKakunin2.Text = ""
                '    SyorichuForm.Execute()
                '    SyorichuForm.Show()

                '    Application.DoEvents()
                '    '-----------------------------------------------------------------------------------------------------

                '    '-----------------------------------------------------------------------------------------------------
                '    '   メイン処理（バックアップデータを戻す処理）
                '    '-----------------------------------------------------------------------------------------------------
                '    ' 取込機能(リストア)
                '    koseiObserver.importRestore()

                '    '-----------------------------------------------------------------------------------------------------
                '    '   処理中画面非表示
                '    SyorichuForm.Close()
                '    '-----------------------------------------------------------------------------------------------------
                '    Me.Show()
                '    'Return
                'End If

            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
                Return
            End Try
            Me.Close()  '処理終了
        End Sub

        Private Sub OldBuhinCall(ByVal EventCode As String, ByVal BukaCode As String, _
                                 ByVal tanto As String, ByVal KaiteiNo As String)

            ' ダブルクリックされた「補用部品編集情報」を読み出す
            Dim vos As List(Of ShisakuCommon.Db.EBom.Vo.THoyouBuhinEditVo) _
                = _OldBuhinSelectorLogic.ChkHoyouBuhinEdit(EventCode, BukaCode, Tanto, KaiteiNo)

            If vos.Count = 0 Then
                MessageBox.Show("対象データが存在しません。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            'Me.Hide()
            '-----------------------------------------------------------------------------------------------------
            '   処理中画面表示
            Dim SyorichuForm1 As New frm03Syorichu
            SyorichuForm1.lblKakunin.Text = "過去の部品情報を置き換え中です。"
            SyorichuForm1.lblKakunin2.Text = ""
            SyorichuForm1.Execute()
            SyorichuForm1.Show()

            Application.DoEvents()
            '-----------------------------------------------------------------------------------------------------

            '-----------------------------------------------------------------------------------------------------
            '   メイン処理
            '-----------------------------------------------------------------------------------------------------
            d_MyBuhinList._BuhinList = vos
            'シートのバックアップ（＆　部品表にデータを貼り付ける。）
            koseiObserver.importBackup(d_MyBuhinList._BuhinList)

            '-----------------------------------------------------------------------------------------------------
            '   処理中画面非表示
            SyorichuForm1.Close()
            '-----------------------------------------------------------------------------------------------------
            '確認画面表示
            _frmDispShisakuBuhinEdit20.OpenShow()

        End Sub

#End Region

#End Region

        ''' <summary>
        ''' 呼出しボタンを使用不可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetCallLock()
            btnCall.ForeColor = Color.Black
            btnCall.BackColor = Color.White
            btnCall.Enabled = False
        End Sub
        ''' <summary>
        ''' 呼出しボタンを使用可とする
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetCallUnlock()
            btnCall.ForeColor = Color.Black
            btnCall.BackColor = Color.LightCyan
            btnCall.Enabled = True
        End Sub


        Private Sub ContextMenuStrip1_Opening_1(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
            Dim sheet As Spread.SheetView = spdParts.ActiveSheet

            'If sheet.ActiveRowIndex >= 0 And rbtNomal.Checked = True Then
            '    LessToolStripMenuItem.Enabled = True
            '    LessToolStripMenuItem.Visible = True
            'Else
            '    LessToolStripMenuItem.Enabled = False
            '    LessToolStripMenuItem.Visible = False
            'End If
        End Sub


        'Private Function buhinSelectChange() As Boolean

        '    buhinSelectChange = False

        '    Try
        '        Dim nYesNo As Integer = MsgBox("部品選択方法を切り替えますか？" & vbLf & vbLf & _
        '                                       "（チェックは無効になります。）", MsgBoxStyle.YesNo, "確認")
        '        'Yesだったら処理続行
        '        If nYesNo = MsgBoxResult.Yes Then
        '            '待機状態
        '            Me.Cursor = Cursors.WaitCursor
        '            Application.DoEvents()

        '            'If StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
        '            '    '_OldBuhinSelectorLogic.ExecuteShisakuBuhinInfo(cmbKaihatsuFugo.Text, _
        '            '    '                                                  cmbShisakuEventCode.Text, _
        '            '    '                                                  cmbShisakuEventName.Text)
        '            'Else
        '            _OldBuhinSelectorLogic.ExecuteBuhinInfo()
        '            'End If

        '            buhinSelectChange = True

        '        End If

        '    Catch ex As Exception
        '        buhinSelectChange = False
        '        Console.WriteLine("Error:" & ex.Message)
        '    End Try

        'End Function

        'Private Sub btnShiyouOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '    _OldBuhinSelectorLogic.ShiyouFormSeigyo("EBOM_B")

        'End Sub

        Private Sub btnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click

            ' バックカラーを戻る
            _OldBuhinSelectorLogic.ResetColor()

            Try

                OldBuhinCall(txtEventNo.Text, txtBuka.Text, txtTantoCd.Text, txtTantoKaiteiNo.Text)

            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
                Return
            End Try
            Me.Close()  '処理終了

        End Sub

    End Class

End Namespace

