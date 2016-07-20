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

    Public Class HoyouBuhinFrm41KouseiBuhinSelector

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

            Public _BuhinList As List(Of ShisakuBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo)

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
            'MyBase.FormBorderStyle = FormBorderStyle.FixedDialog
            ' 最大化ボタン（使用不可）
            'MyBase.MaximizeBox = False

            ' 変更後の設定
            ' サイズ変更可のウィンドウに変更する
            MyBase.FormBorderStyle = FormBorderStyle.Sizable
            ' 最大化ボタン（使用可）
            MyBase.MaximizeBox = True

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
        Private _KouseiBuhinSelectorLogic As Logic.HoyouBuhinKouseiBuhinSelectorSubject

        Private hoyouEventCode As String
        Private hoyouBukaCode As String
        Private hoyouTantoKey As String
        Private hoyouTanto As String

        Private ReadOnly koseiSubject As HoyouBuhinBuhinEditKoseiSubject
        Private ReadOnly detector As HoyouBuhinDetectLatestStructure

        ''' <summary>E-BOM部品情報フォーム</summary>
        Private _frmDispShisakuBuhinEdit20 As HoyouBuhinFrm41DispShisakuBuhinEdit20
        Private koseiObserver As HoyouBuhinSpdKoseiObserver

        ''' <summary>仕様項目に対する、左位置記憶用変数</summary>
        Private mLeftCol As Integer

        ''' <summary>構成展開状態の確認用フラグ</summary>
        Private isStructureOpen As Boolean
        '↓↓2014/10/09 酒井 ADD BEGIN
        Private shisakuEventCode As String
        '↑↑2014/10/09 酒井 ADD END
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

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            '画面制御ロジック
            _KouseiBuhinSelectorLogic = New Logic.HoyouBuhinKouseiBuhinSelectorSubject(hoyouEventCode, Me)
            '↓↓2014/10/09 酒井 ADD BEGIN
            shisakuEventCode = frm41Disp20._frmDispShisakuBuhinEdit00._preForm.alSubject._ShisakuEventCode
            '↑↑2014/10/09 酒井 ADD END
            '初期化メイン
            Initialize()

            '親画面のフォームから
            _frmDispShisakuBuhinEdit20 = frm41Disp20
            Me.koseiSubject = koseiSubject
            Dim frm As HoyouBuhinFrm41DispShisakuBuhinEdit20 = _frmDispShisakuBuhinEdit20
            koseiObserver = New HoyouBuhinSpdKoseiObserver(frm.spdParts, koseiSubject)

            Me.instlRowCount = instlRowCount

            Me.hoyouEventCode = hoyouEventCode
            Me.hoyouBukaCode = hoyouBukaCode
            Me.hoyouTantoKey = hoyouTanto & "_KEY"
            Me.hoyouTanto = hoyouTanto

            '選択コンボボックスに値を追加
            '   以下の値を設定する。 クラス化する。
            '   １：システム大区分、２：システム区分、３：ブロック
            cmbSelect.Items.Clear()
            cmbSelect.Items.Add("")
            cmbSelect.Items.Add(HOYOU_SELECT_EBOM_SYSTEM_DAI)
            cmbSelect.Items.Add(HOYOU_SELECT_EBOM_SYSTEM)
            'cmbSelect.Items.Add(HOYOU_SELECT_EBOM_BLOCK)
            'cmbSelect.Items.Add(HOYOU_SELECT_EBOM_SOUBISHIYOU)
            cmbSelect.Items.Add(HOYOU_SELECT_MBOM_SHISAKUTEHAI)
            ''↓↓2014/08/15 Ⅰ.3.設計編集 ベース車改修専用化_bt) (TES)張 ADD BEGIN
            cmbSelect.Items.Add(HOYOU_SELECT_FINAL)
            ''↑↑2014/08/15 Ⅰ.3.設計編集 ベース車改修専用化_bt) (TES)張 ADD END

            '自給品有無
            cmbJikyuhinUmu.Items.Add("無")
            cmbJikyuhinUmu.Items.Add("有")

            '↓↓2014/10/09 酒井 ADD BEGIN
            '自身の開発符号に変更に伴い、SetKaihatsuFugoComboに移植
            '_KouseiBuhinSelectorLogic.SetInitialView(frm41Disp20._frmDispShisakuBuhinEdit00._preForm.alSubject._ShisakuEventCode)

            '選択中のブロックを展開
            If Not cmbKaihatsuFugo.SelectedValue = "" Then
                _KouseiBuhinSelectorLogic.ExecuteBuhinFinalInfo(frm41Disp20._frmDispShisakuBuhinEdit00._preForm.alSubject._blockNo)
                _KouseiBuhinSelectorLogic.CheckBuhinIn3Ddata(cmbKaihatsuFugo.Text)
            End If
            '↑↑2014/10/09 酒井 ADD END
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
                _KouseiBuhinSelectorLogic.InitializeHeader()
                '開発符号リスト作成
                _KouseiBuhinSelectorLogic.SetKaihatsuFugoCombo(Me.shisakuEventCode)
                ''開発符号のデフォルト設定
                '_KouseiBuhinSelectorLogic.setKaihatsuFugo()

                '初期化完了
                _InitComplete = True

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

        'リセットボタン指定時
        Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

            If StringUtil.IsEmpty(cmbKaihatsuFugo.Text) AndAlso StringUtil.IsEmpty(cmbSelect.Text) Then
                Return
            End If
            '初期化イベント
            _KouseiBuhinSelectorLogic.doSearchResetEvent()

            '20140617   リセットボタン動作では連動しない様変更
            '20140624   追加仕様にて、構成が展開されている状態時に付いては、再建策する動作を行う
            If isStructureOpen Then

                '検索ボタン処理
                btnTenkai_Click(sender, e)

                isStructureOpen = False

            End If
        End Sub

        ''' <summary>
        ''' 検索ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnTenkai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTenkai.Click

            'If cmbImportKbn.SelectedIndex = 0 Then
            '    MessageBox.Show("取込対象を指定してください。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    Exit Sub
            'End If
            '
            Dim selection As FarPoint.Win.Spread.Model.CellRange() = spdBlock_Sheet1.GetSelections()
            If selection.Length = 0 Then
                MessageBox.Show("ブロックを指定してください。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            '待機状態
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then

                '_KouseiBuhinSelectorLogic.ExecuteShisakuBuhinInfo(cmbKaihatsuFugo.Text, _
                '                                                  cmbShisakuEventCode.Text, _
                '                                                  cmbShisakuEventName.Text)

                _KouseiBuhinSelectorLogic.ExecuteShisakuBuhinInfo(cmbKaihatsuFugo.Text, _
                                                                  cmbShisakuEventCode.SelectedValue, _
                                                                  "")
            Else
                'バッチを使用しない方法
                '_KouseiBuhinSelectorLogic.ExecuteBuhinInfo()
                _KouseiBuhinSelectorLogic.ExecuteBuhinFinalInfo()
            End If

            '
            '   3Dデータ有無チェック
            '
            '   Add 2014.04.15 by S.Tokizaki
            '
            _KouseiBuhinSelectorLogic.CheckBuhinIn3Ddata(cmbKaihatsuFugo.Text)





            '抽出完了メッセージ
            ComFunc.ShowInfoMsgBox("部品番号の抽出が完了しました。")

            pbBuhinBango.Value = 1
            _resultOk = True

            '元に戻す
            Me.Cursor = Cursors.Default
        End Sub




        ''' <summary>
        ''' システム区分選択
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ListBoxKubun_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            'If StringUtil.IsNotEmpty(cmbSelect.Text) Then
            '    _KouseiBuhinSelectorLogic.selectBlockNo(cmbKaihatsuFugo.Text, cmbSelect.Text)
            'ElseIf StringUtil.IsNotEmpty(cmbShisakuEventName.Text) Then
            '    _KouseiBuhinSelectorLogic.selectShisakuBlockNo(cmbKaihatsuFugo.Text, cmbShisakuEventName.Text)
            'End If
        End Sub

        ''' <summary>
        ''' ブロック選択
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub ListBoxBlock_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            '_KouseiBuhinSelectorLogic.selectBuhinNo(cmbKaihatsuFugo.Text)
        End Sub

        ''' <summary>
        ''' 開発符号リスト
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbKaihatsuFugo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKaihatsuFugo.SelectedValueChanged

            '初期設定
            cmbSelect.Text = ""
            cmbShisakuEventCode.Text = ""
            cmbShisakuEventName.Text = ""

            '
            Application.DoEvents()

            If StringUtil.IsNotEmpty(cmbKaihatsuFugo.Text) And StringUtil.IsEmpty(cmbSelect.Text) Then
                'システム大区分をデフォルト表示
                cmbSelect.Text = HOYOU_SELECT_EBOM_SYSTEM_DAI
            End If

            _KouseiBuhinSelectorLogic.doSearchEvent(cmbKaihatsuFugo.Text, cmbSelect.Text)
            If cmbSelect.Text = "" OrElse cmbSelect.Text = HOYOU_SELECT_EBOM_BLOCK Then
                Label3.Text = "<<     >>"
            ElseIf cmbSelect.Text = HOYOU_SELECT_MBOM_SHISAKUTEHAI Then
                'Label3.Text = "<<     >>"
                Label3.Text = "<< " & "号車一覧" & " >>"
            Else
                Label3.Text = "<< " & cmbSelect.Text & "一覧 >>"
            End If

            '号車用メニューなので非表示
            ToolStripMenuItem1.Visible = False

        End Sub

        ''' <summary>
        ''' 試作イベント情報より試作イベントコードを設定。
        ''' </summary>
        ''' <param name="strKaihatsuFugo">開発符号</param>
        ''' <param name="strShisakuEventCode">試作イベントコード</param>
        ''' <param name="strShisakuEventName">試作イベント名称</param>
        ''' <returns>試作イベントコードのデータテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetShisakuEventCodeData(ByVal strKaihatsuFugo As String, _
                                                 ByVal strShisakuEventCode As String, _
                                                 ByVal strShisakuEventName As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                db.Fill(DataSqlCommon.GetShisakuEventCodeSql(strKaihatsuFugo, _
                                                             strShisakuEventCode, _
                                                             strShisakuEventName), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 試作イベント情報より試作イベント名称を設定。
        ''' </summary>
        ''' <param name="strKaihatsuFugo">開発符号</param>
        ''' <param name="strShisakuEventCode">試作イベントコード</param>
        ''' <param name="strShisakuEventName">試作イベント名称</param>
        ''' <returns>試作イベント名称のデータテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetShisakuEventNameData(ByVal strKaihatsuFugo As String, _
                                                 ByVal strShisakuEventCode As String, _
                                                 ByVal strShisakuEventName As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                db.Fill(DataSqlCommon.GetShisakuEventNameSql(strKaihatsuFugo, _
                                                             strShisakuEventCode, _
                                                             strShisakuEventName), dtData)
            End Using
            Return dtData
        End Function


        ''' <summary>
        ''' 試作イベント情報より試作イベント名称を設定。
        ''' </summary>
        ''' <param name="strKaihatsuFugo">開発符号</param>
        ''' <returns>試作イベント名称のデータテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetShisakuEventCodeAndNameData(ByVal strKaihatsuFugo As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                db.Fill(DataSqlCommon.GetShisakuEventCodeAndNameSql(strKaihatsuFugo), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 区分リスト
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbSelect_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSelect.SelectedValueChanged

            Dim strKaihatsuFugo As String = cmbKaihatsuFugo.Text
            Dim strSelect As String = cmbSelect.Text
            Dim strShisakuEventCode As String = cmbShisakuEventCode.Text
            Dim strShisakuEventName As String = cmbShisakuEventName.Text
            '
            Application.DoEvents()
            '
            _KouseiBuhinSelectorLogic.doSearchEvent(strKaihatsuFugo, strSelect)
            '通常を選択可能にする
            rbtNomal.Enabled = True
            rbtNomal.Checked = True

            If strSelect = "" OrElse strSelect = HOYOU_SELECT_EBOM_BLOCK Then
                Label3.Text = "<<     >>"
                ''↓↓2014/07/30 Ⅰ.3.設計編集 ベース車改修専用化_ay) (TES)張 ADD BEGIN
            ElseIf strSelect = HOYOU_SELECT_FINAL Then
                Label3.Text = "<<     >>"
                ''↑↑2014/07/30 Ⅰ.3.設計編集 ベース車改修専用化_ay) (TES)張 ADD END
            ElseIf strSelect = HOYOU_SELECT_MBOM_SHISAKUTEHAI Then
                ToolStripMenuItem1.Visible = True
                Label3.Text = "<< " & "号車一覧" & " >>"
                If StringUtil.IsNotEmpty(strKaihatsuFugo) And StringUtil.Equals(strSelect, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then

                    '20140619 FM5はデータ不備の状態があるため、検索が出来ない様コンボボックスの内容を作成しない。
                    If strKaihatsuFugo <> "FM5" Then

                        '試作イベントコードコンボボックスに値を追加
                        Dim dtShisakuEventCode As DataTable = GetShisakuEventCodeAndNameData(strKaihatsuFugo)
                        FormUtil.ComboBoxBind(cmbShisakuEventCode, dtShisakuEventCode, "SHISAKU_EVENT_CODE", "DISPLAYSTRING")

                    End If


                    ''試作イベントコードコンボボックスに値を追加
                    'Dim dtShisakuEventCode As DataTable = GetShisakuEventCodeData(strKaihatsuFugo, _
                    '                                                              strShisakuEventCode, _
                    '                                                              strShisakuEventName)
                    'FormUtil.ComboBoxBind(cmbShisakuEventCode, dtShisakuEventCode, "SHISAKU_EVENT_CODE", "SHISAKU_EVENT_CODE")


                    ''試作イベント名称コンボボックスに値を追加
                    'Dim dtShisakuEventName As DataTable = GetShisakuEventNameData(strKaihatsuFugo, _
                    '                                                              strShisakuEventCode, _
                    '                                                              strShisakuEventName)
                    'FormUtil.ComboBoxBind(cmbShisakuEventName, dtShisakuEventName, "SHISAKU_EVENT_NAME", "SHISAKU_EVENT_NAME")


                    '単品しか選択できなくする
                    rbtTanpin.Checked = True
                    rbtNomal.Enabled = False
                End If
            Else
                ToolStripMenuItem1.Visible = False
                Label3.Text = "<< " & cmbSelect.Text & "一覧 >>"
            End If

            '開発符号、区分が選択されていたらブロック名称リスト作成
            If StringUtil.IsNotEmpty(strKaihatsuFugo) And StringUtil.IsNotEmpty(strSelect) Then
                ''↓↓2014/07/30 Ⅰ.3.設計編集 ベース車改修専用化_ay) (TES)張 CHG BEGIN
                '_KouseiBuhinSelectorLogic.SetBlockNameCombo()
                If Not strSelect = HOYOU_SELECT_FINAL Then
                    _KouseiBuhinSelectorLogic.SetBlockNameCombo()
                End If
                ''↑↑2014/07/30 Ⅰ.3.設計編集 ベース車改修専用化_ay) (TES)張 CHG END
            End If

        End Sub

        ''' <summary>
        ''' 試作用コンボボックス
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbShisakuEventCode_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbShisakuEventCode.SelectedValueChanged

            '初期設定
            Dim strKaihatsuFugo As String = cmbKaihatsuFugo.Text
            'Dim strShisakuEventCode As String = cmbShisakuEventCode.Text
            'Dim strShisakuEventName As String = cmbShisakuEventName.Text

            '20140618 イベントコンボは、旧イベントコードのみ　⇒　新イベントコード:イベント名称での
            '表示に変更となった為、イベントコードはコンボボックスのValueから取得しセットする。
            'また、平行改修として、イベント名称コンボボックスは不要となった為、非表示にて値無しの
            '扱いでセットに変更となっている()
            Dim strShisakuEventCode As String = cmbShisakuEventCode.SelectedValue.ToString
            Dim strShisakuEventName As String = ""

            '
            _KouseiBuhinSelectorLogic.doSearchGousya(strKaihatsuFugo, strShisakuEventCode, strShisakuEventName, _
                                                     hoyouEventCode, hoyouBukaCode, hoyouTantoKey, hoyouTanto)
            If StringUtil.IsNotEmpty(strShisakuEventCode) Then
                Label3.Text = "<< 号車一覧 >>"
                cmbShisakuEventName.Text = ""
            ElseIf cmbShisakuEventName.Text = "" Then
                Label3.Text = "<<     >>"
            End If

        End Sub

        ''' <summary>
        ''' 試作用コンボボックス
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbShisakuEvent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            ''初期設定
            'Dim strKaihatsuFugo As String = cmbKaihatsuFugo.Text
            'Dim strShisakuEventCode As String = cmbShisakuEventCode.Text
            'Dim strShisakuEvent As String = cmbShisakuEvent.Text
            'Dim strShisakuEventName As String = cmbShisakuEventName.Text

            'If StringUtil.IsNotEmpty(strShisakuEvent) And Not StringUtil.Equals(strShisakuEvent, "System.Data.DataRowView") Then
            '    '試作イベントコードコンボボックスに値を追加
            '    Dim dtShisakuEventCode As DataTable = GetShisakuEventCodeData(strKaihatsuFugo, _
            '                                                                  strShisakuEventCode, _
            '                                                                  strShisakuEvent, _
            '                                                                  strShisakuEventName)
            '    FormUtil.ComboBoxBind(cmbShisakuEventCode, dtShisakuEventCode, "SHISAKU_EVENT_CODE", "SHISAKU_EVENT_CODE")
            '    '試作イベント名称コンボボックスに値を追加
            '    Dim dtShisakuEventName As DataTable = GetShisakuEventNameData(strKaihatsuFugo, _
            '                                                                  strShisakuEventCode, _
            '                                                                  strShisakuEvent, _
            '                                                                  strShisakuEventName)
            '    FormUtil.ComboBoxBind(cmbShisakuEventName, dtShisakuEventName, "SHISAKU_EVENT_NAME", "SHISAKU_EVENT_NAME")
            '    '
            '    cmbShisakuEventCode.Text = strShisakuEventCode
            '    cmbShisakuEventName.Text = strShisakuEventName
            'End If
        End Sub

        ''' <summary>
        ''' 試作用コンボボックス
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbShisakuEventName_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbShisakuEventName.SelectedValueChanged

            '初期設定
            Dim strKaihatsuFugo As String = cmbKaihatsuFugo.Text
            'Dim strShisakuEventCode As String = cmbShisakuEventCode.Text
            'Dim strShisakuEventName As String = cmbShisakuEventName.Text

            '20140618 イベントコンボは、旧イベントコードのみ　⇒　新イベントコード:イベント名称での
            '表示に変更となった為、イベントコードはコンボボックスのValueから取得しセットする。
            'また、平行改修として、イベント名称コンボボックスは不要となった為、非表示にて値無しの
            '扱いでセットに変更となっている()
            Dim strShisakuEventCode As String = cmbShisakuEventCode.SelectedValue.ToString
            Dim strShisakuEventName As String = ""

            '
            _KouseiBuhinSelectorLogic.doSearchGousya(strKaihatsuFugo, strShisakuEventCode, strShisakuEventName, _
                                                     hoyouEventCode, hoyouBukaCode, hoyouTantoKey, hoyouTanto)

            If StringUtil.IsNotEmpty(strShisakuEventName) Then
                Label3.Text = "<< 号車一覧 >>"
                cmbShisakuEventCode.Text = ""
            ElseIf cmbShisakuEventCode.Text = "" Then
                Label3.Text = "<<     >>"
            End If

        End Sub

#End Region

#End Region

        ''' <summary>
        ''' 反映ボタンクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub BtnHanei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHanei.Click

            Try

                '開発符号コンボボックスに値を追加
                Dim vos As New List(Of ShisakuBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo)

                For rowindex As Integer = spdBuhin_Sheet1.RowCount - 1 To 0 Step -1
                    If StringUtil.Equals(spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Select).Value, True) Then
                        Dim aBlockNo As String = spdBuhin_Sheet1.GetValue(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BlockNo)
                        Dim aBuhinNo As String = spdBuhin_Sheet1.GetValue(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BuhinNo)
                        If StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
                        Else
                            _KouseiBuhinSelectorLogic.ExecuteBuhinKouseiInfo(aBlockNo, aBuhinNo, rowindex, True)
                        End If
                    End If
                Next

                For rowindex As Integer = 0 To spdBuhin_Sheet1.RowCount - 1
                    If CInt(spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Level).Value) > 0 Then

                        If StringUtil.Equals(spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Select).Value, True) Then


                            Dim vo As New ShisakuBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo

                            'カラム位置は後で定数を利用するなど見直す。　→　見直した　2014.04.15 S.Tokizaki

                            'ブロック№
                            vo.BlockNo = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BlockNo).Value
                            'レベル
                            vo.Level = CInt(spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Level).Value)
                            '国内集計コード
                            vo.ShukeiCode = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_ShukeiCode).Value
                            '海外集計コード
                            vo.SiaShukeiCode = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_SiaShukeiCode).Value
                            '部品番号
                            vo.BuhinNo = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BuhinNo).Value
                            '員数
                            vo.Insu = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Insu).Value
                            '部品名称
                            vo.BuhinName = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BuhinName).Value
                            'ＮＯＴＥ
                            vo.Note = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Note).Value.ToString.TrimEnd
                            '選択方法
                            vo.SelectionMethod = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_SelectionMethod).Value

                            '供給セクション
                            '   試作手配システムから参照する場合はSKE1→研実へ変更

                            '2014/12/17 9SX00⇒9SH10へ変換する
                            Dim aKyokyu As String
                            If StringUtil.IsNotEmpty(cmbShisakuEventCode.Text) Or _
                                    StringUtil.IsNotEmpty(cmbShisakuEventName.Text) Then
                                If StringUtil.Equals(spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_KyoKyu).Value, KYOKYU_SECTION_JIKKEN) Then
                                    aKyokyu = KYOKYU_SECTION_SHISAKU
                                Else
                                    aKyokyu = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_KyoKyu).Value
                                End If
                            Else
                                aKyokyu = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_KyoKyu).Value
                            End If
                            vo.Kyokyu = aKyokyu

                            vos.Add(vo)
                        End If
                    End If


                Next

                If vos.Count = 0 Then
                    MessageBox.Show("部品番号を指定してください。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim MessageLine1 As String = "部品情報を部品編集画面へ反映しますか？"
                Dim MessageLine2 As String = ""
                '"号車一覧"の場合
                If cmbSelect.Text = HOYOU_SELECT_MBOM_SHISAKUTEHAI Then
                    MessageLine2 = MessageLine1
                    MessageLine1 = "支給品の漏れがありませんか？"
                End If
                If frm01Kakunin.ConfirmOkCancel(MessageLine1, MessageLine2) <> MsgBoxResult.Ok Then
                    Return
                End If

                '-----------------------------------------------------------------------------------------------------
                '   処理中画面表示
                Dim SyorichuForm As New frm03Syorichu
                SyorichuForm.lblKakunin.Text = "部品情報を反映中です。"
                SyorichuForm.lblKakunin2.Text = ""
                SyorichuForm.Execute()
                SyorichuForm.Show()

                Application.DoEvents()
                '-----------------------------------------------------------------------------------------------------

                '-----------------------------------------------------------------------------------------------------
                '   メイン処理
                '-----------------------------------------------------------------------------------------------------
                d_MyBuhinList._BuhinList = vos

                '自給品の有無
                Dim aJikyu As String = ""
                If StringUtil.Equals(cmbJikyuhinUmu.Text, "無") Then
                    aJikyu = "0"
                Else
                    aJikyu = "1"
                End If

                '20140619 EBOMデータ取得と手配システムデータ取得の切り分け判定追加
                Dim isEbomRead As Boolean = True
                If StringUtil.IsNotEmpty(cmbShisakuEventCode.Text) Then
                    '手配システムのデータリードの場合は、FALSE
                    isEbomRead = False
                End If

                'ラジオボタンの、構成・単品の選択状態を取得格納
                Dim isNormal As Boolean = rbtNomal.Checked

                '部品表にデータを貼り付ける。
                koseiObserver.buhinListToSpread(d_MyBuhinList._BuhinList _
                                                , hoyouEventCode _
                                                , aJikyu _
                                                , cmbKaihatsuFugo.Text _
                                                , isEbomRead _
                                                , isNormal)

                '-----------------------------------------------------------------------------------------------------
                '   処理中画面非表示
                SyorichuForm.Close()
                '-----------------------------------------------------------------------------------------------------

                'Me.Close()
            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try

        End Sub

        ''' <summary>
        ''' ブロックボタンクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlock.Click
            'If Not StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
            '    _KouseiBuhinSelectorLogic.selectBlockNo(cmbKaihatsuFugo.Text, cmbSelect.Text)
            'ElseIf StringUtil.IsNotEmpty(cmbShisakuEventCode.Text) Or StringUtil.IsNotEmpty(cmbShisakuEventName.Text) Then
            '    _KouseiBuhinSelectorLogic.selectShisakuBlockNo(cmbKaihatsuFugo.Text, cmbShisakuEventCode.Text, cmbShisakuEventName.Text)
            'End If

            'イベントコンボボックスを１つに統合した為、取得方法変更


            If Not StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
                'EBOMデータ処理
                _KouseiBuhinSelectorLogic.selectBlockNo(cmbKaihatsuFugo.Text, cmbSelect.Text)

            ElseIf StringUtil.IsNotEmpty(cmbShisakuEventCode.SelectedValue.ToString) Then
                '手配データ処理
                _KouseiBuhinSelectorLogic.selectShisakuBlockNo(cmbKaihatsuFugo.Text, cmbShisakuEventCode.SelectedValue.ToString, "")

            End If

        End Sub

        ''' <summary>
        ''' コンボボックスに　delete　key　press
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbBlockName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbBlockName.KeyDown

            'ShisakuFormUtil.DelKeyDown(sender, e)

        End Sub

        ''' <summary>
        ''' ブロックコンボボックスのキープレス
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbBlockName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbBlockName.KeyPress

            '
            '   シングルクォーテーションは使用させない(SQL文対処のため)
            '
            If e.KeyChar = "'" Then
                e.Handled = True
            End If

        End Sub

        ''' <summary>
        ''' 検索条件が入力後　または　選択後
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbBlockName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBlockName.TextChanged

            cmbBlockName.Text = Replace(cmbBlockName.Text, "'", "")       '   シングルクォーテーションを削除


            If StringUtil.IsNotEmpty(cmbKaihatsuFugo.Text) Then

                If spdBlock_Sheet1.RowCount > 0 Then
                    _KouseiBuhinSelectorLogic.doSearchBlockName(cmbKaihatsuFugo.Text, cmbBlockName.Text)
                End If

            End If
        End Sub

        ''' <summary>
        ''' 機能仕様表示選択ツールメニュークリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 機能仕様表示選択AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 機能仕様表示選択AToolStripMenuItem.Click
            '補用イベントコード、補用部課コード、補用担当ＫＥＹ、補用担当
            'Using frm As New Frm41EventKanseiSelector("TEST_EVENT", "SEKE", "TANTO_KEY", "TANTO")
            Using frm As New HoyouBuhinFrm41EventKanseiSelector(hoyouEventCode, hoyouBukaCode, hoyouTantoKey, hoyouTanto)
                frm.ShowDialog()
                If frm.ResultOk Then
                    'OKの時は装備仕様情報を再表示する。
                    '   初期設定
                    Dim strKaihatsuFugo As String = cmbKaihatsuFugo.Text
                    'Dim strShisakuEventCode As String = cmbShisakuEventCode.Text
                    'Dim strShisakuEventName As String = cmbShisakuEventName.Text

                    '20140618 イベントコンボは、旧イベントコードのみ　⇒　新イベントコード:イベント名称での
                    '表示に変更となった為、イベントコードはコンボボックスのValueから取得しセットする。
                    'また、平行改修として、イベント名称コンボボックスは不要となった為、非表示にて値無しの
                    '扱いでセットに変更となっている()
                    Dim strShisakuEventCode As String = cmbShisakuEventCode.SelectedValue.ToString
                    Dim strShisakuEventName As String = ""

                    _KouseiBuhinSelectorLogic.doSearchGousya(strKaihatsuFugo, strShisakuEventCode, strShisakuEventName, _
                                                             hoyouEventCode, hoyouBukaCode, hoyouTantoKey, hoyouTanto)
                End If
            End Using

        End Sub

        ''' <summary>
        ''' spreadにボタン、チェックボックス或いはradio button等クリックの処理
        ''' </summary>
        ''' <param name="sender">spread</param>
        ''' <param name="e">spreadのボタンクリックイベント</param>
        ''' <remarks></remarks>
        Private Sub Spread_OnButtonClicked(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles spdBuhin.ButtonClicked
            Dim cntCell = spdBuhin_Sheet1.Cells(e.Row, e.Column)

            Dim rowCount As Integer = spdBuhin_Sheet1.RowCount
            If rowCount = 0 Then
                MessageBox.Show("部品抽出後にクリックしてください。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            '
            '   列位置を示す値は、固定値はだめ
            '
#If 1 Then
            Dim aSelect As Boolean = spdBuhin_Sheet1.GetValue(e.Row, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Select)
            Dim aBlockNo As String = spdBuhin_Sheet1.GetValue(e.Row, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BlockNo)
            Dim aLevel As String = spdBuhin_Sheet1.GetValue(e.Row, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Level)
            Dim aBuhinNo As String = spdBuhin_Sheet1.GetValue(e.Row, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BuhinNo)
#Else
                    Dim aSelect As Boolean = spdBuhin_Sheet1.GetValue(e.Row, 5)
                    Dim aBlockNo As String = spdBuhin_Sheet1.GetValue(e.Row, 0)
                    Dim aLevel As String = spdBuhin_Sheet1.GetValue(e.Row, 1)
                    Dim aBuhinNo As String = spdBuhin_Sheet1.GetValue(e.Row, 6)
#End If



            '待機状態
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            '列位置によって処理を行う。
            Select Case e.Column

                '
                '   列位置を示す値は、固定値はだめ
                '  Changed 2014.04.16 by S.Tokizaki
                '
                'Case 4

                Case _KouseiBuhinSelectorLogic.spd_Buhin_Col_Tenkai   '構成展開ボタン

                    '20140624   部品展開動作有無フラグ更新
                    isStructureOpen = True

                    If StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
                    Else
                        _KouseiBuhinSelectorLogic.ExecuteBuhinKouseiInfo(aBlockNo, aBuhinNo, e.Row)
                    End If

                    pbBuhinBango.Value = 1
                    _resultOk = True

                    '
                    '   列位置を示す値は、固定値はだめ
                    '
                    'Case 5


                Case _KouseiBuhinSelectorLogic.spd_Buhin_Col_Select   '部品チェック

                    '20140624   部品展開動作有無フラグ更新
                    isStructureOpen = True

                    If StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
                        _KouseiBuhinSelectorLogic.ExecuteBuhinKouseiCheckShisaku(aBlockNo, aBuhinNo, e.Row)
                    Else
                        _KouseiBuhinSelectorLogic.ExecuteBuhinKouseiCheck(aBlockNo, aBuhinNo, e.Row)
                    End If

                    pbBuhinBango.Value = 1
                    _resultOk = True

            End Select

            '元に戻す
            Me.Cursor = Cursors.Default

        End Sub

        ''' <summary>
        ''' spreadのセルクリックの処理
        ''' </summary>
        ''' <param name="sender">spread</param>
        ''' <param name="e">spreadのボタンクリックイベント</param>
        ''' <remarks></remarks>
        Private Sub spdBuhin_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdBuhin.CellClick

            ''Dim rowCount As Integer = spdBuhin_Sheet1.RowCount
            ''If rowCount = 0 Then
            ''    MessageBox.Show("部品抽出後にクリックしてください。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ''    Exit Sub
            ''End If

            'Dim aSelect As Boolean = spdBuhin_Sheet1.GetValue(e.Row, 5)
            'Dim aBlockNo As String = spdBuhin_Sheet1.GetValue(e.Row, 0)
            'Dim aLevel As String = spdBuhin_Sheet1.GetValue(e.Row, 1)
            'Dim aBuhinNo As String = spdBuhin_Sheet1.GetValue(e.Row, 6)

            ' ''If StringUtil.Equals(aSelect, True) Or _
            ' ''    StringUtil.Equals(aSelect, False) And Not StringUtil.Equals(aLevel, "0") Then
            ' ''    Exit Sub
            ' ''End If

            ''列位置によって処理を行う。
            'Select Case e.Column
            '    Case 4  '構成展開ボタン
            '        '待機状態
            '        Me.Cursor = Cursors.WaitCursor
            '        Application.DoEvents()

            '        If StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
            '        Else
            '            _KouseiBuhinSelectorLogic.ExecuteBuhinKouseiInfo(aBlockNo, aBuhinNo, e.Row)
            '        End If

            '        ProgressBar1.Value = 1
            '        _resultOk = True

            '        '元に戻す
            '        Me.Cursor = Cursors.Default

            '    Case 5  '部品チェック

            '        ''待機状態
            '        ''Me.Cursor = Cursors.WaitCursor
            '        ''Application.DoEvents()

            '        'If StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
            '        'Else
            '        '    _KouseiBuhinSelectorLogic.ExecuteBuhinKouseiCheck(aBlockNo, aBuhinNo, e.Row)
            '        'End If

            '        'ProgressBar1.Value = 1
            '        '_resultOk = True

            '        ''元に戻す
            '        'Me.Cursor = Cursors.Default

            'End Select

        End Sub

        Private Sub ContextMenuStrip1_Opening_1(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
            Dim sheet As Spread.SheetView = spdBuhin.ActiveSheet

            If sheet.ActiveRowIndex >= 0 And rbtNomal.Checked = True Then
                LessToolStripMenuItem.Enabled = True
                LessToolStripMenuItem.Visible = True
            Else
                LessToolStripMenuItem.Enabled = False
                LessToolStripMenuItem.Visible = False
            End If

            '2014/02/28 追加 今泉
            If sheet.ActiveRowIndex >= 0 Then
                View3DMenuItem.Enabled = True
                View3DMenuItem.Visible = True
            Else
                View3DMenuItem.Enabled = False
                View3DMenuItem.Visible = False
            End If
        End Sub

        Private Sub LessToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LessToolStripMenuItem.Click

            If Not StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
                _KouseiBuhinSelectorLogic.selectBuhinNoCheckLess()
            ElseIf StringUtil.IsNotEmpty(cmbShisakuEventCode.Text) Or StringUtil.IsNotEmpty(cmbShisakuEventName.Text) Then
                _KouseiBuhinSelectorLogic.selectShisakuBuhinNoCheckLess()
            End If

        End Sub

        Private Sub View3DMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles View3DMenuItem.Click
            Dim strMessage As String

            strMessage = _KouseiBuhinSelectorLogic.Show3D(spdBuhin_Sheet1.ActiveRowIndex)

            If Not StringUtil.IsEmpty(strMessage) Then
                MessageBox.Show(strMessage, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Sub

        ''' <summary>
        ''' ラジオボタンノーマルクリック動作（現在未使用）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub rbtNomal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtNomal.Click
            ''ブロック、部品共に0なら処理終了
            'Dim selectionBlock As FarPoint.Win.Spread.Model.CellRange() = spdBlock_Sheet1.GetSelections()
            'If selectionBlock.Length = 0 Then
            '    Exit Sub
            'End If
            'Dim selectionBuhin As FarPoint.Win.Spread.Model.CellRange() = spdBuhin_Sheet1.GetSelections()
            'If selectionBuhin.Length = 0 Then
            '    Exit Sub
            'End If

            'If rbtNomal.Checked = True Then

            '    If buhinSelectChange() = False Then
            '        'Falseの場合チェックを戻す。
            '        rbtNomal.Checked = False
            '        rbtTanpin.Checked = True
            '    End If

            'End If
        End Sub

        ''' <summary>
        ''' ラジオボタン単品クリック動作（現在未使用）
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub rbtTanpin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtTanpin.Click
            ''ブロック、部品共に0なら処理終了
            'Dim selectionBlock As FarPoint.Win.Spread.Model.CellRange() = spdBlock_Sheet1.GetSelections()
            'If selectionBlock.Length = 0 Then
            '    Exit Sub
            'End If
            'Dim selectionBuhin As FarPoint.Win.Spread.Model.CellRange() = spdBuhin_Sheet1.GetSelections()
            'If selectionBuhin.Length = 0 Then
            '    Exit Sub
            'End If

            'If rbtTanpin.Checked = True Then

            '    If buhinSelectChange() = False Then
            '        'Falseの場合チェックを戻す。
            '        rbtTanpin.Checked = False
            '        rbtNomal.Checked = True
            '    End If

            'End If
        End Sub

        Private Function buhinSelectChange() As Boolean

            buhinSelectChange = False

            Try
                Dim nYesNo As Integer = MsgBox("部品選択方法を切り替えますか？" & vbLf & vbLf & _
                                               "（チェックは無効になります。）", MsgBoxStyle.YesNo, "確認")
                'Yesだったら処理続行
                If nYesNo = MsgBoxResult.Yes Then
                    '待機状態
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()

                    If StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
                        '_KouseiBuhinSelectorLogic.ExecuteShisakuBuhinInfo(cmbKaihatsuFugo.Text, _
                        '                                                  cmbShisakuEventCode.Text, _
                        '                                                  cmbShisakuEventName.Text)
                        _KouseiBuhinSelectorLogic.ExecuteShisakuBuhinInfo(cmbKaihatsuFugo.Text, _
                                                                          cmbShisakuEventCode.SelectedValue, _
                                                                          "")

                    Else
                        _KouseiBuhinSelectorLogic.ExecuteBuhinInfo()
                    End If

                    buhinSelectChange = True

                End If

            Catch ex As Exception
                buhinSelectChange = False
                Console.WriteLine("Error:" & ex.Message)
            End Try

        End Function

        ''' <summary>
        ''' 仕様項目の表示切替ボタンクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnShiyouOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShiyouOnOff.Click

            _KouseiBuhinSelectorLogic.ShiyouFormSeigyo("EBOM_B")

        End Sub

        ''' <summary>
        ''' 仕様項目のリーブセル
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBuhinShiyou_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles spdBuhinShiyou.LeaveCell



            If spdBuhinShiyou_Sheet1.ActiveColumnIndex = 0 Then
                spdBuhinShiyou_Sheet1.Cells(Logic.HoyouBuhinKouseiBuhinSelectorSubject.spd_BuhinShiyou_startRow, _
                                            1, _
                                            Logic.HoyouBuhinKouseiBuhinSelectorSubject.spd_BuhinShiyou_startRow, _
                                            spdBuhinShiyou_Sheet1.ColumnCount - 1).Value = ""
            End If


            '再描画停止
            spdBuhinShiyou.SuspendLayout()

            ' 車型のコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setShiyouJyouhouListSyagata()
            'ｸﾞﾚｰﾄﾞのコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setShiyouJyouhouListGrade()
            '仕向地・仕向けのコンボボックスを生成する。
            '_KouseiBuhinSelectorLogic.setShiyouJyouhouListShimukechiShimuke()
            '仕向地・ﾊﾝﾄﾞﾙのコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setShiyouJyouhouListShimukechiHandle()
            'E/G・排気量のコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setEgHaikiryouListShimukechiHandle()
            'E/G・形式のコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setEgKeishikiListShimukechiHandle()
            'E/G・過給器のコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setEgKakyukiListShimukechiHandle()
            'T/M・駆動方式のコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setTmKudouListShimukechiHandle()
            'T/M・変速機のコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setTmHensokukiListShimukechiHandle()
            '７桁型式のコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setKatashiki7ListShimukechiHandle()
            '仕向けコードのコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setKataShimukeListShimukechiHandle()
            'ＯＰコードのコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setKataOpListShimukechiHandle()

            'ＯＰ列を設定する。
            _KouseiBuhinSelectorLogic.setOpRetsuHandle()

            '20140619   元々の画面表示位置を復元する（左端の列を基点とした動作）
            spdBuhinShiyou.SetViewportLeftColumn(1, mLeftCol)

            '20140627   スクロールを右端に移動させた上、画面上の項目をクリックした場合、
            '           スクロール位置がずれる為、一番右のセルが見えている状態の場合は
            '           強制的に、右端にスクロールバーを移動させる処理を追加した。
            If spdBuhinShiyou.IsCellInView(0, 0, 1, spdBuhinShiyou_Sheet1.Columns.Count - 1) Then
                spdBuhinShiyou.ShowColumn(0, spdBuhinShiyou_Sheet1.Columns.Count - 1, Spread.HorizontalPosition.Right)
            End If

            '描画処理再開
            spdBuhinShiyou.ResumeLayout(True)
        End Sub

        ''' <summary>
        ''' 仕様項目のレフトチェンジ
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBuhinShiyou_LeftChange(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeftChangeEventArgs) Handles spdBuhinShiyou.LeftChange
            '左端位置の変更があった場合、位置復元の為、左端位置を記憶しておく
            Call setLeftColumn(e.OldLeft)
        End Sub

        ''' <summary>
        ''' 仕様項目のマウスムーブ
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBuhinShiyou_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles spdBuhinShiyou.MouseMove
            Dim range As FarPoint.Win.Spread.Model.CellRange = spdBuhinShiyou.GetCellFromPixel(0, 0, e.X, e.Y)
            Dim col As Spread.Column = spdBuhinShiyou_Sheet1.Columns(range.Column)

            '   カーソル位置を求めて、変更がある（位置が変わった）時だけ動くようにしてみた。
            If StringUtil.IsEmpty(ToolTipRange) OrElse _
                ToolTipRange.Column <> range.Column OrElse ToolTipRange.Row <> range.Row Then
                Dim tipText As String = ""
                Select Case col.Tag
                    Case DispShiyouJyouhouList.TAG_KATA_SHIMUKE
                        '"補用部品表検索の仕向地をHELP項目に設定"
                        tipText = _KouseiBuhinSelectorLogic.SetShimukeDataString()
                End Select
                Me.ToolTip1.SetToolTip(spdBuhinShiyou, tipText)
            End If
            ToolTipRange = range

        End Sub

        ''' <summary>
        ''' 仕様項目の列位置変更時での左端列格納処理
        ''' </summary>
        ''' <param name="oldLeftCol"></param>
        ''' <remarks></remarks>
        Private Sub setLeftColumn(ByVal oldLeftCol As Integer)
            mLeftCol = oldLeftCol
        End Sub

        ''↓↓2014/07/30 Ⅰ.3.設計編集 ベース車改修専用化_az) (TES)張 ADD BEGIN
        Private Sub btnFinal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinal.Click
            'If cmbImportKbn.SelectedIndex = 0 Then
            '    MessageBox.Show("取込対象を指定してください。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    Exit Sub
            'End If
            '
            If txtFinal.Text.Length < 5 Then
                MessageBox.Show("5桁以上入力してください。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            '待機状態
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'バッチを使用しない方法
            '_KouseiBuhinSelectorLogic.ExecuteBuhinInfo()
            _KouseiBuhinSelectorLogic.ExecuteBuhinFinalInfo()

            '
            '   3Dデータ有無チェック
            '
            '   Add 2014.04.15 by S.Tokizaki
            '
            _KouseiBuhinSelectorLogic.CheckBuhinIn3Ddata(cmbKaihatsuFugo.Text)

            '抽出完了メッセージ
            ComFunc.ShowInfoMsgBox("部品番号の抽出が完了しました。")

            pbBuhinBango.Value = 1
            _resultOk = True

            '元に戻す
            Me.Cursor = Cursors.Default

        End Sub
        ''↑↑2014/07/30 Ⅰ.3.設計編集 ベース車改修専用化_az) (TES)張 ADD END
    End Class

End Namespace

