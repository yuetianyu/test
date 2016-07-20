Imports EBom.Data
Imports EBom.Common
Imports FarPoint.Win
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports System.Text
Imports FarPoint.Win.Spread
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic.Matrix
Imports YosansyoTool.YosanBuhinEdit.Logic.Detect
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic
Imports YosansyoTool.YosanBuhinEdit.Kosei.Ui
Imports YosansyoTool.YosanBuhinEdit.Kosei
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoEdit.Dao
Imports YosansyoTool.YosanBuhinEdit.Logic
Imports EventSakusei

Namespace YosanBuhinEdit.KouseiBuhin

    Public Class Frm41KouseiBuhinSelector

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

            Public _BuhinList As List(Of YosanBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo)

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
        Private _KouseiBuhinSelectorLogic As Logic.KouseiBuhinSelectorSubject

        Private yosanEventCode As String
        Private yosanBukaCode As String
        Private yosanTantoKey As String
        Private yosanTanto As String

        Private ReadOnly koseiSubject As BuhinEditKoseiSubject
        Private ReadOnly detector As DetectLatestStructure

        ''' <summary>E-BOM部品情報フォーム</summary>
        Private _frmDispYosanBuhinEdit20 As Frm41DispYosanBuhinEdit20
        Private koseiObserver As SpdKoseiObserver

        ''' <summary>仕様項目に対する、左位置記憶用変数</summary>
        Private mLeftCol As Integer

        ''' <summary>構成展開状態の確認用フラグ</summary>
        Private isStructureOpen As Boolean
#End Region

        '#Region "プロパティ"
        '        ''' <summary>
        '        ''' 「取込」押下かを返す
        '        ''' </summary>
        '        Public ReadOnly Property ResultOk() As Boolean
        '            Get
        '                Return _resultOk
        '            End Get
        '        End Property
        '#End Region

#Region "コンストラクタ"

        Public Sub New(ByVal yosanEventCode As String, ByVal yosanBukaCode As String, ByVal yosanTanto As String, _
                       ByVal koseiSubject As BuhinEditKoseiSubject, ByVal frm41Disp20 As Frm41DispYosanBuhinEdit20)

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            '画面制御ロジック
            _KouseiBuhinSelectorLogic = New Logic.KouseiBuhinSelectorSubject(yosanEventCode, Me)

            '初期化メイン
            Initialize()

            '親画面のフォームから
            _frmDispYosanBuhinEdit20 = frm41Disp20
            Me.koseiSubject = koseiSubject
            Dim frm As Frm41DispYosanBuhinEdit20 = _frmDispYosanBuhinEdit20
            koseiObserver = New SpdKoseiObserver(frm.spdParts, koseiSubject)

            Me.instlRowCount = instlRowCount

            Me.yosanEventCode = yosanEventCode
            Me.yosanBukaCode = yosanBukaCode
            Me.yosanTantoKey = yosanTanto & "_KEY"
            Me.yosanTanto = yosanTanto

            '選択コンボボックスに値を追加
            '   以下の値を設定する。 クラス化する。
            '   １：システム大区分、２：システム区分、３：ブロック
            cmbSelect.Items.Clear()
            cmbSelect.Items.Add("")
            cmbSelect.Items.Add(HOYOU_SELECT_EBOM_SYSTEM_DAI)
            cmbSelect.Items.Add(HOYOU_SELECT_EBOM_SYSTEM)
            cmbSelect.Items.Add(HOYOU_SELECT_MBOM_SHISAKUTEHAI)
            cmbSelect.Items.Add(HOYOU_SELECT_VER1)
            cmbSelect.Items.Add(HOYOU_SELECT_VER2)

            _KouseiBuhinSelectorLogic.setPattern(koseiSubject.PatternInfos)

            '自給品有無
            cmbJikyuhinUmu.Items.Add("無")
            cmbJikyuhinUmu.Items.Add("有")

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
                _KouseiBuhinSelectorLogic.SetKaihatsuFugoCombo()

                cmbKuKe.Text = ""
                chkKuke.Checked = False
                chkKuke.Enabled = False

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

            _KouseiBuhinSelectorLogic.doSearchEvent(cmbKaihatsuFugo.Text, cmbSelect.Text, cmbKuKe.Text, chkKuke.Checked)
            If cmbSelect.Text = "" OrElse cmbSelect.Text = HOYOU_SELECT_EBOM_BLOCK Then
                Label3.Text = "<<     >>"
            ElseIf cmbSelect.Text = HOYOU_SELECT_MBOM_SHISAKUTEHAI Then
                Label3.Text = "<< " & "号車一覧" & " >>"
            Else
                Label3.Text = "<< " & cmbSelect.Text & "一覧 >>"
            End If

            '号車用メニューなので非表示
            ToolStripMenuItem1.Visible = False

        End Sub

        ''' <summary>
        ''' 検索方法
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbSelect_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSelect.SelectedValueChanged

            Dim strKaihatsuFugo As String = cmbKaihatsuFugo.Text
            Dim strSelect As String = cmbSelect.Text
            Dim strShisakuEventCode As String = cmbShisakuEventCode.Text
            Dim strShisakuEventName As String = cmbShisakuEventName.Text

            Dim strUnitKbn As String = cmbKuKe.Text
            Dim strUnitKbnChk As Boolean = chkKuke.Checked

            Application.DoEvents()
            '
            _KouseiBuhinSelectorLogic.doSearchEvent(strKaihatsuFugo, strSelect, cmbKuKe.Text, chkKuke.Checked)
            '通常を選択可能にする
            rbtNomal.Enabled = True
            rbtNomal.Checked = True

            If strSelect = "" OrElse strSelect = HOYOU_SELECT_EBOM_BLOCK Then
                btnBlock.Visible = True
                Label3.Text = "<<     >>"
            ElseIf strSelect = HOYOU_SELECT_MBOM_SHISAKUTEHAI Then
                lblEventPhase.Text = "試作イベント"
                btnBlock.Visible = True
                ToolStripMenuItem1.Visible = True
                Label3.Text = "<< " & "号車一覧" & " >>"
                If StringUtil.IsNotEmpty(strKaihatsuFugo) And StringUtil.Equals(strSelect, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then

                    If strKaihatsuFugo <> "FM5" Then

                        '試作イベントコードコンボボックスに値を追加
                        Dim dtShisakuEventCode As DataTable = GetShisakuEventCodeAndNameData(strKaihatsuFugo)
                        FormUtil.ComboBoxBind(cmbShisakuEventCode, dtShisakuEventCode, "SHISAKU_EVENT_CODE", "DISPLAYSTRING")

                    End If

                    '単品しか選択できなくする
                    rbtTanpin.Checked = True
                    rbtNomal.Enabled = False
                End If
            ElseIf strSelect = HOYOU_SELECT_VER1 Then
                lblEventPhase.Text = "イベント"

                btnBlock.Visible = False
                Dim dtShisakuEventCode As DataTable = GetGenchoEventCodeAndNameData(strKaihatsuFugo)
                FormUtil.ComboBoxBind(cmbShisakuEventCode, dtShisakuEventCode, "GENCHO_EVENT_CODE", "DISPLAYSTRING")

            ElseIf strSelect = HOYOU_SELECT_VER2 Then
                lblEventPhase.Text = "イベント＆フェーズ"

                btnBlock.Visible = False
                Dim dtShisakuEventCode As DataTable = GetGenchoEventCodeAndNameVer2Data(strKaihatsuFugo)
                FormUtil.ComboBoxBind(cmbShisakuEventCode, dtShisakuEventCode, "GENCHO_EVENT_CODE", "DISPLAYSTRING")
            Else
                btnBlock.Visible = True
                ToolStripMenuItem1.Visible = False
                Label3.Text = "<< " & cmbSelect.Text & "一覧 >>"
            End If

            '開発符号、区分が選択されていたらブロック名称リスト作成
            If StringUtil.IsNotEmpty(strKaihatsuFugo) And StringUtil.IsNotEmpty(strSelect) Then
                _KouseiBuhinSelectorLogic.SetBlockNameCombo(strUnitKbn, strUnitKbnChk)
            End If

        End Sub

        Private Sub cmbKuKe_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbKuKe.SelectedValueChanged
            Dim strKuKe As String = cmbKuKe.Text
            If strKuKe.Equals("M") OrElse strKuKe.Equals("T") Then
                chkKuke.Enabled = True
            ElseIf strKuKe.Equals("S") OrElse strKuKe.Equals("") Then
                chkKuke.Enabled = False
                chkKuke.Checked = False
            End If

            Dim strKaihatsuFugo As String = cmbKaihatsuFugo.Text
            Dim strSelect As String = cmbSelect.Text
            Dim strShisakuEventCode As String = cmbShisakuEventCode.Text
            Dim strShisakuEventName As String = cmbShisakuEventName.Text

            Dim strUnitKbn As String = cmbKuKe.Text
            Dim strUnitKbnChk As Boolean = chkKuke.Checked

            '未選択の場合スルー
            If StringUtil.IsEmpty(strSelect) Then
                Exit Sub
            End If

            '開発符号、区分が選択されていたらブロック名称リスト作成
            If StringUtil.IsNotEmpty(strKaihatsuFugo) And StringUtil.IsNotEmpty(strSelect) Then
                '
                _KouseiBuhinSelectorLogic.doSearchEvent(strKaihatsuFugo, strSelect, cmbKuKe.Text, chkKuke.Checked)
                '
                _KouseiBuhinSelectorLogic.SetBlockNameCombo(strUnitKbn, strUnitKbnChk)
            End If
        End Sub

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
        ''' 試作イベント情報より試作イベント名称を設定。
        ''' </summary>
        ''' <returns>試作イベント名称のデータテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetGenchoEventCodeAndNameData(ByVal strKaihatsuFugo As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                db.Fill(GetGenchoEventCodeAndNameSql(strKaihatsuFugo), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' Ver2イベント情報よりイベント名称を設定。
        ''' </summary>
        ''' <returns>Ver2イベント名称のデータテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetGenchoEventCodeAndNameVer2Data(ByVal strKaihatsuFugo As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                db.Fill(GetGenchoEventCodeAndNameVer2Sql(strKaihatsuFugo), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' Ver2イベント情報よりフェーズを設定。
        ''' </summary>
        ''' <returns>Ver2イベント名称のデータテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetGenchoEventPhaseData(ByVal strEventCode As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                db.Fill(GetGenchoEventPhaseVer2Sql(strEventCode), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 試作手配データ検索コンボボックス用イベント取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetGenchoEventCodeAndNameSql(ByVal KaihatsuFugo As String) As String


            Dim flg As String = ""
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("	GENCHO_EVENT_CODE ")
                .AppendLine("	,(GENCHO_EVENT_CODE + '：' + GENCHO_EVENT_NAME) AS DISPLAYSTRING")

                .AppendLine("FROM ")
                .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_GENCHO_EVENT ")

                If StringUtil.IsNotEmpty(KaihatsuFugo) Then
                    .AppendLine(" WHERE GENCHO_KAIHATSU_FUGO = '" & KaihatsuFugo & "'")
                End If

                ''設計展開以降　及び　中止　以外　の情報のみ取得
                '.AppendLine(" AND 21 <= STATUS AND STATUS <> 25 AND STATUS <>26 ")

                .AppendLine(" GROUP BY GENCHO_EVENT_CODE,(GENCHO_EVENT_CODE + '：' + GENCHO_EVENT_NAME) ")
                .AppendLine(" ORDER BY GENCHO_EVENT_CODE ")
            End With
            Return sql.ToString()

        End Function

        ''' <summary>
        ''' Ver2データ検索コンボボックス用イベント取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetGenchoEventCodeAndNameVer2Sql(ByVal KaihatsuFugo As String) As String

            Dim flg As String = ""
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("	GENCHO_EVENT_CODE ")
                .AppendLine("	,(GENCHO_EVENT_CODE + '：' + GENCHO_EVENT_NAME) AS DISPLAYSTRING")

                .AppendLine("FROM ")
                .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_FUNC_EVENT ")

                If StringUtil.IsNotEmpty(KaihatsuFugo) Then
                    .AppendLine(" WHERE GENCHO_KAIHATSU_FUGO = '" & KaihatsuFugo & "'")
                End If

                ''設計展開以降　及び　中止　以外　の情報のみ取得
                '.AppendLine(" AND 21 <= STATUS AND STATUS <> 25 AND STATUS <>26 ")

                .AppendLine(" GROUP BY GENCHO_EVENT_CODE,(GENCHO_EVENT_CODE + '：' + GENCHO_EVENT_NAME) ")
                .AppendLine(" ORDER BY GENCHO_EVENT_CODE ")
            End With
            Return sql.ToString()

        End Function


        ''' <summary>
        ''' Ver2データ検索コンボボックス用フェーズ取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetGenchoEventPhaseVer2Sql(ByVal eventCode As String) As String

            Dim flg As String = ""
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("	PHASE_NO ")
                .AppendLine("	,(STR(PHASE_NO) + '：' + PHASE_NAME) AS DISPLAYSTRING")

                .AppendLine("FROM ")
                .AppendLine("	" + MBOM_DB_NAME + ".dbo.T_FUNC_EVENT_PHASE ")

                If StringUtil.IsNotEmpty(eventCode) Then
                    .AppendLine(" WHERE GENCHO_EVENT_CODE = '" & eventCode & "'")
                End If

                .AppendLine(" GROUP BY PHASE_NO,(STR(PHASE_NO) + '：' + PHASE_NAME) ")
                .AppendLine(" ORDER BY PHASE_NO ")
            End With
            Return sql.ToString()

        End Function

        ''' <summary>
        ''' 試作用コンボボックス
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbShisakuEventCode_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbShisakuEventCode.SelectedValueChanged

            '初期設定
            Dim strKaihatsuFugo As String = cmbKaihatsuFugo.Text
            Dim strShisakuEventCode As String = cmbShisakuEventCode.SelectedValue.ToString
            Dim strShisakuEventName As String = ""
            Dim strKuke As String = cmbKuKe.Text
            Dim strChkKuke As Boolean

            Dim strSelect As String = cmbSelect.Text

            'If StringUtil.Equals(strSelect, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Then
            strKuke = cmbKuKe.Text
            strChkKuke = chkKuke.Checked
            'ElseIf StringUtil.Equals(strSelect, HOYOU_SELECT_VER1) Then
            '    strKuke = ""
            '    strChkKuke = False
            'End If

            _KouseiBuhinSelectorLogic.doSearchGousya(strKaihatsuFugo, strShisakuEventCode, strShisakuEventName, _
                                                 yosanEventCode, yosanBukaCode, yosanTantoKey, yosanTanto, strKuke, strChkKuke, strSelect)

            If StringUtil.IsNotEmpty(strShisakuEventCode) Then
                Label3.Text = "<< 号車一覧 >>"
                cmbShisakuEventName.Text = ""
            ElseIf cmbShisakuEventName.Text = "" Then
                Label3.Text = "<<     >>"
            End If

            If strSelect = HOYOU_SELECT_VER2 And StringUtil.IsNotEmpty(strShisakuEventCode) Then
                Dim dtEventPhase As DataTable = GetGenchoEventPhaseData(strShisakuEventCode)
                FormUtil.ComboBoxBind(cmbPhase, dtEventPhase, "PHASE_NO", "DISPLAYSTRING")
            End If

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
            Dim strShisakuEventCode As String = cmbShisakuEventCode.SelectedValue.ToString
            Dim strShisakuEventName As String = ""
            Dim strKuke As String = cmbKuKe.Text
            Dim strChkKuke As Boolean = chkKuke.Checked

            Dim strSelect As String = cmbSelect.Text
            '
            _KouseiBuhinSelectorLogic.doSearchGousya(strKaihatsuFugo, strShisakuEventCode, strShisakuEventName, _
                                                     yosanEventCode, yosanBukaCode, yosanTantoKey, yosanTanto, strKuke, strChkKuke, strSelect)

            If StringUtil.IsNotEmpty(strShisakuEventName) Then
                Label3.Text = "<< 号車一覧 >>"
                cmbShisakuEventCode.Text = ""
            ElseIf cmbShisakuEventCode.Text = "" Then
                Label3.Text = "<<     >>"
            End If

        End Sub

        ''' <summary>
        ''' ブロックボタンクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlock.Click
            'イベントコンボボックスを１つに統合した為、取得方法変更
            If Not StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) And _
                Not StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_VER1) And _
                Not StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_VER2) Then
                'EBOMデータ処理
                _KouseiBuhinSelectorLogic.selectBlockNo(cmbKaihatsuFugo.Text, cmbSelect.Text)

            ElseIf StringUtil.IsNotEmpty(cmbShisakuEventCode.SelectedValue.ToString) Then
                '手配データ処理
                _KouseiBuhinSelectorLogic.selectShisakuBlockNo(cmbKaihatsuFugo.Text, cmbShisakuEventCode.SelectedValue.ToString, "")

            End If

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


            'If StringUtil.IsNotEmpty(cmbKaihatsuFugo.Text) Then

            '    If spdBlock_Sheet1.RowCount > 0 Then
            '        _KouseiBuhinSelectorLogic.doSearchBlockName(cmbKaihatsuFugo.Text, cmbBlockName.Text)
            '    End If

            'End If
        End Sub

        ''' <summary>
        ''' 検索ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnTenkai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTenkai.Click

            Dim strSelect As String = cmbSelect.Text
            Dim strPhaseNo As Integer = 0

            If StringUtil.IsNotEmpty(cmbPhase.SelectedValue) Then
                strPhaseNo = CInt(cmbPhase.SelectedValue)
            End If
            '
            Dim selection As FarPoint.Win.Spread.Model.CellRange() = spdBlock_Sheet1.GetSelections()
            If selection.Length = 0 Then
                MessageBox.Show("ブロックを指定してください。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            '待機状態
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Or _
                StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_VER1) Or _
                StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_VER2) Then
                _KouseiBuhinSelectorLogic.ExecuteShisakuBuhinInfo(cmbKaihatsuFugo.Text, _
                                                                  cmbShisakuEventCode.SelectedValue, _
                                                                  strSelect, _
                                                                  strPhaseNo)
            Else
                'バッチを使用しない方法
                _KouseiBuhinSelectorLogic.ExecuteBuhinFinalInfo()
            End If

            _KouseiBuhinSelectorLogic.CheckBuhinIn3Ddata(cmbKaihatsuFugo.Text)

            '抽出完了メッセージ
            ComFunc.ShowInfoMsgBox("部品番号の抽出が完了しました。")

            pbBuhinBango.Value = 1
            _resultOk = True

            '元に戻す
            Me.Cursor = Cursors.Default
        End Sub

        ''' <summary>
        ''' 機能仕様表示選択ツールメニュークリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub 機能仕様表示選択AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 機能仕様表示選択AToolStripMenuItem.Click
            '補用イベントコード、補用部課コード、補用担当ＫＥＹ、補用担当
            Using frm As New Frm41EventKanseiSelector(yosanEventCode, yosanBukaCode, yosanTantoKey, yosanTanto)
                frm.ShowDialog()
                If frm.ResultOk Then
                    'OKの時は装備仕様情報を再表示する。
                    '   初期設定
                    Dim strKaihatsuFugo As String = cmbKaihatsuFugo.Text
                    Dim strShisakuEventCode As String = cmbShisakuEventCode.SelectedValue.ToString
                    Dim strShisakuEventName As String = ""
                    Dim strKuke As String = cmbKuKe.Text
                    Dim strChkKuke As Boolean = chkKuke.Checked

                    Dim strSelect As String = cmbSelect.Text

                    _KouseiBuhinSelectorLogic.doSearchGousya(strKaihatsuFugo, strShisakuEventCode, strShisakuEventName, _
                                                             yosanEventCode, yosanBukaCode, yosanTantoKey, yosanTanto, strKuke, strChkKuke, strSelect)
                End If
            End Using

        End Sub

        ''' <summary>
        ''' 部品追加ボタンクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub BtnHanei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHanei.Click
            Dim SyorichuForm As New frm03Syorichu

            Try

                Dim patternListVos As New List(Of TYosanBuhinEditPatternVo)
                For rowindex As Integer = 0 To spdPattern_Sheet1.RowCount - 1
                    If StringUtil.Equals(spdPattern_Sheet1.Cells(rowindex, 0).Value, True) Then
                        Dim patternListVo As New TYosanBuhinEditPatternVo
                        patternListVo.PatternHyoujiJun = rowindex
                        patternListVo.PatternName = spdPattern_Sheet1.Cells(rowindex, 1).Value
                        patternListVos.Add(patternListVo)
                    End If
                Next
                If StringUtil.Equals(patternListVos.Count, 0) Then
                    MessageBox.Show("員数のセット先（パターン名）を指定してください。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    spdPattern.ActiveSheet.SetActiveCell(0, 1)
                    spdPattern.ActiveSheet.SetActiveCell(0, 0)
                    'パターン名が未セットの場合、表示させる。
                    pnlPattern.Visible = True
                    '   ボタンのテキストを変更する。
                    btnPattern.Text = "パターン名非表示"
                    Exit Sub
                End If

                '開発符号コンボボックスに値を追加
                Dim vos As New List(Of YosanBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo)

                For rowindex As Integer = 0 To spdBuhin_Sheet1.RowCount - 1

                    If StringUtil.Equals(spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Select).Value, True) Then

                        Dim vo As New YosanBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo

                        'カラム位置は後で定数を利用するなど見直す。　→　見直した　2014.04.15 S.Tokizaki

                        '部課コード
                        vo.BukaCode = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BukaCode).Value
                        'ブロック№
                        vo.BlockNo = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BlockNo).Value
                        'レベル
                        vo.Level = CInt(spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Level).Value)
                        '国内集計コード
                        vo.ShukeiCode = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_ShukeiCode).Value
                        '海外集計コード
                        vo.SiaShukeiCode = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_SiaShukeiCode).Value
                        '部品番号
                        vo.BuhinNo = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BuhinNo).Value.ToString.Trim
                        '員数
                        vo.Insu = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Insu).Value
                        '部品名称
                        vo.BuhinName = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BuhinName).Value
                        ''ＮＯＴＥ
                        'vo.Note = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Note).Value.ToString.TrimEnd
                        ''選択方法
                        'vo.SelectionMethod = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_SelectionMethod).Value

                        '供給セクション
                        '   試作手配システムから参照する場合はSKE1

                        Dim aKyokyu As String

                        If StringUtil.IsNotEmpty(cmbShisakuEventCode.Text) Or _
                                StringUtil.IsNotEmpty(cmbShisakuEventName.Text) Then
                            If StringUtil.Equals(spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_KyoKyu).Value, KYOKYU_SECTION_SHISAKU) Then
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
                Next

                If vos.Count = 0 Then
                    MessageBox.Show("部品番号を指定してください。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim MessageLine1 As String = "部品情報を部品編集画面へ追加しますか？"
                Dim MessageLine2 As String = ""
                ''"号車一覧"の場合
                'If cmbSelect.Text = HOYOU_SELECT_MBOM_SHISAKUTEHAI Then
                '    MessageLine2 = MessageLine1
                '    MessageLine1 = "支給品の漏れがありませんか？"
                'End If
                If EventSakusei.frm01Kakunin.ConfirmOkCancel(MessageLine1, MessageLine2) <> MsgBoxResult.Ok Then
                    Return
                End If

                '-----------------------------------------------------------------------------------------------------
                '   処理中画面表示
                SyorichuForm.lblKakunin.Text = "部品情報を部品追加中です。"
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

                Dim isEbomRead As Boolean = True
                If StringUtil.IsNotEmpty(cmbShisakuEventCode.Text) Then
                    '手配システムのデータリードの場合は、FALSE
                    isEbomRead = False
                End If

                'ラジオボタンの、構成・単品の選択状態を取得格納
                Dim isNormal As Boolean = rbtNomal.Checked

                '部品表にデータを貼り付ける。
                koseiObserver.buhinListToSpread(d_MyBuhinList._BuhinList _
                                                , yosanEventCode _
                                                , aJikyu _
                                                , cmbKaihatsuFugo.Text _
                                                , isEbomRead _
                                                , isNormal _
                                                , patternListVos _
                                                , koseiSubject.PatternInfos _
                                                , _frmDispYosanBuhinEdit20)

                '-----------------------------------------------------------------------------------------------------
                '   処理中画面非表示
                SyorichuForm.Close()
                '-----------------------------------------------------------------------------------------------------

            Catch ex As Exception
                '-----------------------------------------------------------------------------------------------------
                '   処理中画面非表示
                SyorichuForm.Close()
                '-----------------------------------------------------------------------------------------------------
                ComFunc.ShowErrMsgBox(ex.Message)
                'Console.WriteLine("Error:" & ex.Message)
            End Try

        End Sub

        ''' <summary>
        ''' ブロック入替ボタンクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBlockReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlockReplace.Click
            Try

                Dim patternListVos As New List(Of TYosanBuhinEditPatternVo)
                For rowindex As Integer = 0 To spdPattern_Sheet1.RowCount - 1
                    If StringUtil.Equals(spdPattern_Sheet1.Cells(rowindex, 0).Value, True) Then
                        Dim patternListVo As New TYosanBuhinEditPatternVo
                        patternListVo.PatternHyoujiJun = rowindex
                        patternListVo.PatternName = spdPattern_Sheet1.Cells(rowindex, 1).Value
                        patternListVos.Add(patternListVo)
                    End If
                Next
                If StringUtil.Equals(patternListVos.Count, 0) Then
                    MessageBox.Show("員数のセット先（パターン名）を指定してください。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    'パターン名が未セットの場合、表示させる。
                    pnlPattern.Visible = True
                    '   ボタンのテキストを変更する。
                    btnPattern.Text = "パターン名非表示"
                    Exit Sub
                End If

                '-----------------------------------------------------------------------------------------------------
                '   処理中画面表示
                Dim SyorichuForm As New frm03Syorichu
                SyorichuForm.lblKakunin.Text = "部品情報をブロック入替中です。"
                SyorichuForm.lblKakunin2.Text = ""
                SyorichuForm.Execute()
                SyorichuForm.Show()

                Application.DoEvents()

                'VOS作成
                Dim vos As New List(Of YosanBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo)

                '初期設定
                Dim i As Integer = 0
                Dim j As Integer = 0
                Dim Shisakudate As ShisakuDate
                Dim buhinstruct As YosansyoTool.YosanBuhinEdit.Logic.TehaichoEditBuhinStructure
                Dim newBuhinMatrix As New BuhinKoseiMatrix
                Dim kaihatsuFugo As String = cmbKaihatsuFugo.Text
                '自給品の有無
                Dim Jikyu As String = ""
                If StringUtil.Equals(cmbJikyuhinUmu.Text, "無") Then
                    Jikyu = "0"
                Else
                    Jikyu = "1"
                End If

                Shisakudate = New ShisakuDate

                buhinstruct = New YosansyoTool.YosanBuhinEdit.Logic.TehaichoEditBuhinStructure(yosanEventCode, "ListCodeDummy", _
                                                             "BlockNoDummy", "BukaCodeDummy", Shisakudate)

                '自分が0レベルでひとつ前のレベルも0の場合、0レベル以下全て選択されたものとみなしINSTLの構成を取得する。
                Dim CurrentLevel As Integer = 0
                '親レベルの部課コード、ブロックを取得する。
                Dim CurrentBukaCode As String = ""
                Dim CurrentBlockNo As String = ""

                Dim NextLevel As Integer = 0
                Dim aKyokyu As String = ""

                For rowindex As Integer = 0 To spdBuhin_Sheet1.RowCount - 1
                    Dim vo As New YosanBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo

#If 1 Then
                    'If StringUtil.Equals(spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Select).Value, True) Then

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
                    vo.BuhinNo = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BuhinNo).Value.ToString.Trim
                    '員数
                    vo.Insu = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Insu).Value
                    '部品名称
                    vo.BuhinName = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BuhinName).Value
                    ''ＮＯＴＥ
                    'vo.Note = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Note).Value.ToString.TrimEnd
                    ''選択方法
                    'vo.SelectionMethod = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_SelectionMethod).Value

                    '供給セクション
                    '   試作手配システムから参照する場合はSKE1

                    aKyokyu = ""

                    If StringUtil.IsNotEmpty(cmbShisakuEventCode.Text) Or _
                            StringUtil.IsNotEmpty(cmbShisakuEventName.Text) Then
                        If StringUtil.Equals(spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_KyoKyu).Value, KYOKYU_SECTION_SHISAKU) Then
                            aKyokyu = KYOKYU_SECTION_SHISAKU
                        Else
                            aKyokyu = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_KyoKyu).Value
                        End If
                    Else
                        aKyokyu = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_KyoKyu).Value
                    End If

                    vo.Kyokyu = aKyokyu

                    '部課コード
                    vo.BukaCode = spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_BukaCode).Value

                    vos.Add(vo)
                    'End If
#Else
''''
                    If StringUtil.Equals(spdBuhin_Sheet1.Cells(rowindex, 5).Value, True) Then

                        'カラム位置は後で定数を利用するなど見直す。

                        'ブロック№
                        vo.BlockNo = spdBuhin_Sheet1.Cells(rowindex, 0).Value
                        'レベル
                        vo.Level = CInt(spdBuhin_Sheet1.Cells(rowindex, 1).Value)
                        '国内集計コード
                        vo.ShukeiCode = spdBuhin_Sheet1.Cells(rowindex, 2).Value
                        '海外集計コード
                        vo.SiaShukeiCode = spdBuhin_Sheet1.Cells(rowindex, 3).Value
                        '部品番号
                        vo.BuhinNo = spdBuhin_Sheet1.Cells(rowindex, 6).Value
                        '員数
                        vo.Insu = spdBuhin_Sheet1.Cells(rowindex, 7).Value
                        '部品名称
                        vo.BuhinName = spdBuhin_Sheet1.Cells(rowindex, 8).Value
                        ''ＮＯＴＥ
                        'vo.Note = spdBuhin_Sheet1.Cells(rowindex, 9).Value.ToString.TrimEnd
                        ''選択方法
                        'vo.SelectionMethod = spdBuhin_Sheet1.Cells(rowindex, 10).Value
                        '供給セクション
                        '   試作手配システムから参照する場合はSKE1
                        If StringUtil.IsNotEmpty(cmbShisakuEventCode.Text) Or _
                                StringUtil.IsNotEmpty(cmbShisakuEventName.Text) Then
                            If StringUtil.Equals(spdBuhin_Sheet1.Cells(rowindex, 11).Value, KYOKYU_SECTION_SHISAKU) Then
                                vo.Kyokyu =  KYOKYU_SECTION_SHISAKU
                            Else
                                vo.Kyokyu = spdBuhin_Sheet1.Cells(rowindex, 11).Value
                            End If
                        Else
                            vo.Kyokyu = spdBuhin_Sheet1.Cells(rowindex, 11).Value
                        End If

                        '部課コード
                        vo.bukacode = spdBuhin_Sheet1.Cells(rowindex, 12).Value

                        vos.Add(vo)
                    End If
#End If

                    'EBOMからのデータ取得時のみこの動作を行う
                    If Not StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) And _
                        Not StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_VER1) And _
                        Not StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_VER2) Then

                        '   部品のLevelが０ならば、マトリックスを作る
                        If vo.Level = 0 Then

                            newBuhinMatrix = buhinstruct.GetKouseiMatrix(vo.BuhinNo, "", 0, kaihatsuFugo)

                        End If


                        '自分が0レベルでひとつ前のレベルも0の場合、0レベル以下全て選択されたものとみなしINSTLの構成を取得する。
                        CurrentLevel = vo.Level
                        '親レベルの部課コード、ブロックを取得する。
                        CurrentBukaCode = vo.BukaCode
                        CurrentBlockNo = vo.BlockNo
                        NextLevel = 0

                        If i < spdBuhin_Sheet1.RowCount - 1 Then
                            NextLevel = CInt(spdBuhin_Sheet1.Cells(rowindex + 1, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Level).Value)
                        End If

                        If CurrentLevel = 0 And NextLevel = 0 Then

                            Console.WriteLine()
                            '部品構成をSPREADへ表示する。
                            'If newBuhinMatrix IsNot Nothing OrElse newBuhinMatrix.Records.Count > 0 Then
                            If newBuhinMatrix IsNot Nothing Then
                                For Each k As Integer In newBuhinMatrix.GetInputRowIndexes

                                    '自分は除く
                                    If Not StringUtil.Equals(newBuhinMatrix(k).YosanLevel, 0) And _
                                       StringUtil.IsNotEmpty(newBuhinMatrix(k).YosanLevel) Then

                                        '自給品有無＝無の場合　国内または国外集計コードＪは読み飛ばす。
                                        If Jikyu = "1" Or _
                                            Jikyu = "0" And newBuhinMatrix(k).YosanShukeiCode.TrimEnd <> "" _
                                                        And newBuhinMatrix(k).YosanShukeiCode.TrimEnd <> "J" Or _
                                            Jikyu = "0" And newBuhinMatrix(k).YosanShukeiCode.TrimEnd = "" _
                                                        And newBuhinMatrix(k).YosanSiaShukeiCode.TrimEnd <> "" _
                                                        And newBuhinMatrix(k).YosanSiaShukeiCode.TrimEnd <> "J" Then

                                            'カウント
                                            j = j + 1

                                            '員数を求める。
                                            Dim lstInsu As List(Of Integer) = newBuhinMatrix.GetInputInsuColumnIndexes
                                            If lstInsu.Count <= 0 Then
                                                Continue For
                                            End If
                                            Dim getInsu As Integer = 0
                                            For l As Integer = 0 To lstInsu.Count - 1
                                                '号車員数ゲット
                                                If newBuhinMatrix.InsuSuryo(k, lstInsu(l)) <> 0 Then
                                                    getInsu = newBuhinMatrix.InsuSuryo(k, lstInsu(l))
                                                    Exit For
                                                End If
                                            Next
                                            Dim strInsu As String = ""
                                            If StringUtil.Equals(getInsu, -1) Then
                                                strInsu = "**"
                                            Else
                                                strInsu = CStr(getInsu)
                                            End If

                                            Dim voKo As New YosanBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo

                                            'voに設定
                                            '   親と同じ部課、ブロックをセット
                                            voKo.BukaCode = CurrentBukaCode
                                            voKo.BlockNo = CurrentBlockNo
                                            '
                                            voKo.BuhinNo = newBuhinMatrix(k).YosanBuhinNo
                                            If StringUtil.IsEmpty(newBuhinMatrix(k).YosanLevel) Then
                                                voKo.Level = ""
                                            Else
                                                voKo.Level = newBuhinMatrix(k).YosanLevel
                                            End If
                                            voKo.ShukeiCode = newBuhinMatrix(k).YosanShukeiCode
                                            voKo.SiaShukeiCode = newBuhinMatrix(k).YosanSiaShukeiCode
                                            voKo.ShukeiCode = newBuhinMatrix(k).YosanShukeiCode
                                            voKo.SiaShukeiCode = newBuhinMatrix(k).YosanSiaShukeiCode
                                            voKo.BuhinNo = newBuhinMatrix(k).YosanBuhinNo
                                            voKo.BuhinName = newBuhinMatrix(k).YosanBuhinName
                                            voKo.Kyokyu = newBuhinMatrix(k).YosanKyoukuSection

                                            ' 員数
                                            voKo.Insu = newBuhinMatrix(k).YosanInsu

                                            '追加
                                            vos.Add(voKo)

                                        End If

                                    End If

                                Next

                            End If

                        End If
                    End If
                    '---　ここで手配システムの動作と、EBOM取得からの動作は切り分ける

                    'カウント
                    i = i + 1
                    System.GC.Collect()
                Next

                'If vos.Count = 0 Then
                '    MessageBox.Show("部品番号を指定してください。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    Exit Sub
                'End If

                MetaruBlockImportUpdate(vos, patternListVos, koseiSubject.PatternInfos)

                '   処理中画面非表示
                SyorichuForm.Close()

                'Dim closer As frm00Kakunin.IFormCloser = New CopyFormCloser(Me, vos)

                'frm00Kakunin.ConfirmShow("部品検索の確認", "取込情報を確認してください。", "データを反映して宜しいですか？", "取込情報を反映", "取込前に戻す", closer)

                '_frmDispYosanBuhinEdit20.Refresh()

                Me.Close()

            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' spread部分列対応の情報を得る
        ''' </summary>
        ''' <remarks></remarks>
        Private Function GetSpreadData()
            Dim vos As New List(Of TYosanBuhinEditVo)
            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispYosanBuhinEdit20.spdParts_Sheet1
            Dim startSpdRow As Integer = GetTitleRowsIn(sheet)

            For rowIndex As Integer = startSpdRow To sheet.RowCount - 1
                Dim blockNo As String = sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_BLOCK_NO))
                If StringUtil.IsEmpty(blockNo) Then
                    Exit For
                End If

                Dim vo As New TYosanBuhinEditVo
                'ブロックNo
                vo.YosanBlockNo = blockNo
                'レベル
                Dim yosanLevel As String = sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_LEVEL))
                If StringUtil.IsNotEmpty(yosanLevel) Then
                    vo.YosanLevel = CInt(yosanLevel)
                End If
                '部品番号
                vo.YosanBuhinNo = sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_BUHIN_NO))
                '変更概要
                vo.YosanHenkoGaiyo = sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_HENKO_GAIYO))
                '部品費(量産)
                Dim yosanBuhinHiRyosan As String = sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN))
                If StringUtil.IsNotEmpty(yosanBuhinHiRyosan) Then
                    vo.YosanBuhinHiRyosan = CDec(yosanBuhinHiRyosan)
                End If
                '部品費(部品表)
                Dim yosanBuhinHiBuhinhyo As String = sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_BUHIN_HI_BUHINHYO))
                If StringUtil.IsNotEmpty(yosanBuhinHiBuhinhyo) Then
                    vo.YosanBuhinHiBuhinhyo = CDec(yosanBuhinHiBuhinhyo)
                End If
                '部品費(特記)
                Dim yosanBuhinHiTokki As String = sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_BUHIN_HI_TOKKI))
                If StringUtil.IsNotEmpty(yosanBuhinHiTokki) Then
                    vo.YosanBuhinHiTokki = CDec(yosanBuhinHiTokki)
                End If
                '型費
                Dim yosanKataHi As String = sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_KATA_HI))
                If StringUtil.IsNotEmpty(yosanKataHi) Then
                    vo.YosanKataHi = CDec(yosanKataHi)
                End If
                '治具費
                Dim yosanJiguHi As String = sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_JIGU_HI))
                If StringUtil.IsNotEmpty(yosanJiguHi) Then
                    vo.YosanJiguHi = CDec(yosanJiguHi)
                End If
                '工数
                Dim yosanKosu As String = sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_KOSU))
                If StringUtil.IsNotEmpty(yosanKosu) Then
                    vo.YosanKosu = CDec(yosanKosu)
                End If
                '発注実績(割付&MIX値全体と平均値)
                Dim yosanHachuJisekiMix As String = sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_HACHU_JISEKI_MIX))
                If StringUtil.IsNotEmpty(yosanHachuJisekiMix) Then
                    vo.YosanHachuJisekiMix = CDec(yosanHachuJisekiMix)
                End If

                vos.Add(vo)
            Next

            Return vos
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

#End Region

#End Region

#Region "ブロック更新時の処理"
        ''' <summary>
        ''' ブロック更新時の処理
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub MetaruBlockImportUpdate(ByVal vos As List(Of YosanBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo), _
                                            ByVal patternListVos As List(Of TYosanBuhinEditPatternVo), _
                                            ByVal patternVos As List(Of TYosanBuhinEditPatternVo))
            Dim voList As List(Of TYosanBuhinEditVo) = GetSpreadData()

            Dim sheet As FarPoint.Win.Spread.SheetView = _frmDispYosanBuhinEdit20.spdParts_Sheet1
            Dim startSpdRow As Integer = GetTitleRowsIn(sheet)

            ''ここで取込前のバックアップを実行する。
            '' 取込前バックアップ
            'importBackup()

            '_frmDispYosanBuhinEdit20.Refresh()

            '削除した後の行数
            Dim rowCount = sheet.RowCount
            Dim aBlock As String = String.Empty
            Dim insertIndex As Integer = 0
            Dim rowNo As Integer = 0
            'ブロックに該当する部品情報を全て部品表編集画面にセット
            For Each vo As YosanBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo In vos
                Dim blockNo As String = vo.BlockNo
                Dim level As Integer = vo.Level
                Dim buhinNo As String = vo.BuhinNo

                If Not StringUtil.Equals(aBlock, blockNo) Then
                    For rowIndex As Integer = startSpdRow To sheet.RowCount - 1
                        If StringUtil.IsEmpty(sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_BLOCK_NO))) Then
                            Exit For
                        End If
                        '同一ブロックがあったら該当ブロックの部品情報を全て削除
                        If String.Equals(blockNo, sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_BLOCK_NO))) Then
                            'sheet.RemoveRows(rowIndex, 1)
                            koseiObserver.RemoveRows(rowIndex, 1)
                            rowCount = rowCount - 1
                            rowIndex = rowIndex - 1
                        ElseIf blockNo < sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_BLOCK_NO)) Then
                            insertIndex = rowIndex
                            Exit For
                        Else
                            rowNo = rowNo + 1
                        End If
                    Next
                End If

                aBlock = blockNo

                '初回のみ、以外かチェックし、初回の場合先頭行をセットする。
                If insertIndex = 0 And rowNo = 0 Then
                    insertIndex = 4
                ElseIf insertIndex = 0 And rowNo <> 0 Then
                    insertIndex = rowNo + 4
                    '   下記はどちらがいいのか？？
                    sheet.AddRows(insertIndex, 1)
                    koseiSubject.InsertRow(insertIndex - startSpdRow, 1)
                    'koseiObserver.InsertRows(insertIndex, 1)
                    rowCount = rowCount + 1
                    '
                Else
                    '   下記はどちらがいいのか？？
                    sheet.AddRows(insertIndex, 1)
                    koseiSubject.InsertRow(insertIndex - startSpdRow, 1)
                    'koseiObserver.InsertRows(insertIndex, 1)
                    rowCount = rowCount + 1
                    '
                End If

                SetEditRowProc(True, insertIndex, 0, 1, sheet.ColumnCount)

                '設計課
                If StringUtil.IsNotEmpty(vo.BukaCode) Then
                    sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_BUKA_CODE).Index, vo.BukaCode)
                End If
                'ブロック№
                If StringUtil.IsNotEmpty(blockNo) Then
                    sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_BLOCK_NO).Index, blockNo)
                End If
                'レベル
                If StringUtil.IsNotEmpty(level) Then
                    sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_LEVEL).Index, level)
                End If
                '国内集計
                If StringUtil.IsNotEmpty(vo.ShukeiCode) Then
                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_SHUKEI_CODE), vo.ShukeiCode)
                End If
                '海外集計
                If StringUtil.IsNotEmpty(vo.SiaShukeiCode) Then
                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_SIA_SHUKEI_CODE), vo.SiaShukeiCode)
                End If

                '取引先コード
                If StringUtil.IsNotEmpty(vo.TorihikisakiCode) Then
                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_MAKER_CODE), vo.TorihikisakiCode)
                End If
                '取引先名称
                If StringUtil.IsNotEmpty(vo.TorihikisakiName) Then
                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_MAKER_NAME), vo.TorihikisakiName)
                End If
                '部品番号
                If StringUtil.IsNotEmpty(buhinNo) Then
                    sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_BUHIN_NO).Index, buhinNo)
                End If
                '部品番号試作区分
                '部品名称
                If StringUtil.IsNotEmpty(vo.BuhinName) Then
                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_BUHIN_NAME), vo.BuhinName)
                End If
                '供給セクション
                If StringUtil.IsNotEmpty(vo.Kyokyu) Then
                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_KYOUKU_SECTION), vo.Kyokyu)
                End If
                ' 員数
                Dim intCnt As Integer = 0
                If patternVos.Count <> 0 Then
                    For patternRowindex As Integer = 0 To patternVos.Count - 1
                        sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_KYOUKU_SECTION) + 1 + patternRowindex, "")
                        'Dim patternHyoujiJun As Integer = patternVos(patternRowindex).PatternHyoujiJun
                        Dim patternHyoujiJun As Integer = intCnt
                        For voCnt As Integer = 0 To patternListVos.Count - 1
                            If StringUtil.Equals(patternHyoujiJun, patternListVos(voCnt).PatternHyoujiJun) Then
                                If StringUtil.IsNotEmpty(vo.Insu) Then
                                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_KYOUKU_SECTION) + 1 + patternRowindex, vo.Insu)
                                End If
                            End If
                        Next
                        intCnt = intCnt + 1
                    Next
                End If

                For Each yosanVo As TYosanBuhinEditVo In voList
                    '同一部品の入力値を保留
                    If StringUtil.Equals(blockNo, yosanVo.YosanBlockNo) AndAlso _
                       StringUtil.Equals(level, yosanVo.YosanLevel) AndAlso _
                       StringUtil.Equals(buhinNo, yosanVo.YosanBuhinNo) Then
                        '変更概要
                        If StringUtil.IsNotEmpty(yosanVo.YosanHenkoGaiyo) Then
                            sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_HENKO_GAIYO).Index, yosanVo.YosanHenkoGaiyo)
                        End If
                        '部品費(量産)
                        If StringUtil.IsNotEmpty(yosanVo.YosanBuhinHiRyosan) Then
                            sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_BUHIN_HI_RYOSAN).Index, yosanVo.YosanBuhinHiRyosan)
                        End If
                        '部品費(部品表)
                        If StringUtil.IsNotEmpty(yosanVo.YosanBuhinHiBuhinhyo) Then
                            sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_BUHIN_HI_BUHINHYO).Index, yosanVo.YosanBuhinHiBuhinhyo)
                        End If
                        '部品費(特記)
                        If StringUtil.IsNotEmpty(yosanVo.YosanBuhinHiTokki) Then
                            sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_BUHIN_HI_TOKKI).Index, yosanVo.YosanBuhinHiTokki)
                        End If
                        '型費
                        If StringUtil.IsNotEmpty(yosanVo.YosanKataHi) Then
                            sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_KATA_HI).Index, yosanVo.YosanKataHi)
                        End If
                        '治具費
                        If StringUtil.IsNotEmpty(yosanVo.YosanJiguHi) Then
                            sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_JIGU_HI).Index, yosanVo.YosanJiguHi)
                        End If
                        '工数
                        If StringUtil.IsNotEmpty(yosanVo.YosanKosu) Then
                            sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_KOSU).Index, yosanVo.YosanKosu)
                        End If
                        '発注実績(割付&MIX値全体と平均値)
                        If StringUtil.IsNotEmpty(yosanVo.YosanHachuJisekiMix) Then
                            sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_HACHU_JISEKI_MIX).Index, yosanVo.YosanHachuJisekiMix)
                        End If

                        Exit For
                    End If
                Next

                insertIndex = insertIndex + 1
                koseiSubject.NotifyObservers()
            Next
        End Sub
#End Region

#Region "EXCEL取込機能(確認フォームクローズ用クラス)"
        ''' <summary>
        ''' 確認フォームクローズ用
        ''' </summary>
        ''' <remarks></remarks>
        Private Class CopyFormCloser : Implements frm00Kakunin.IFormCloser
            Private voList As New List(Of YosanBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo)
            Private _frmEdit20 As Frm41KouseiBuhinSelector = Nothing

            Public Sub New(ByVal frmEdit20 As Frm41KouseiBuhinSelector, ByVal vos As List(Of YosanBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo))
                _frmEdit20 = frmEdit20
                voList = vos
            End Sub
            ''' <summary>
            ''' フォームを閉じる時の処理
            ''' </summary>
            ''' <param name="IsOk">OKが押された場合、true</param>
            ''' <remarks></remarks>
            Public Sub FormClose(ByVal IsOk As Boolean) Implements frm00Kakunin.IFormCloser.FormClose

                If IsOk Then
                    'バックアップを削除
                    _frmEdit20.importBacupRemove()
                    Return
                Else
                    'リストア処理
                    _frmEdit20.importRestore(voList)
                End If

            End Sub
        End Class

#End Region

#Region "Spreadのシートにタイトルをもつ場合のタイトル行数を返す"
        ''' <summary>
        '''Spreadのシートにタイトルをもつ場合のタイトル行数を返す
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <returns>タイトル行数</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTitleRowsIn(ByVal spreadSheet As FarPoint.Win.Spread.SheetView) As Integer
            If 1 < spreadSheet.StartingRowNumber Then
                Throw New InvalidOperationException("想定外の値です. sheet.StartingRowNumber=" & CStr(spreadSheet.StartingRowNumber))
            End If
            Return 1 - spreadSheet.StartingRowNumber
        End Function
#End Region

#Region "対象シートの全範囲消去"
        ''' <summary>
        ''' 対象シートの全範囲消去
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <remarks></remarks>
        Public Shared Sub SpreadAllClear(ByVal sheet As FarPoint.Win.Spread.SheetView)
            Dim row As Integer = sheet.RowCount
            Dim col As Integer = sheet.Columns.Count

            'タイトルの次行からが開始位置
            Dim startRow As Integer = GetTitleRowsIn(sheet)

            If sheet Is Nothing Then
                Throw New Exception("消去指示されたスプレッドシートはNothingが格納されている")
                Return
            End If

            sheet.ClearRange(startRow, 0, row, col, False)

        End Sub
#End Region

#Region " 列インデックス取得 "
        ''' <summary>
        ''' 列タグを元に列インデックスを取得します.
        ''' </summary>
        ''' <param name="sheet">対象シート</param>
        ''' <param name="tag">列タグ</param>
        ''' <returns>列インデックス</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTagIdx(ByVal sheet As Spread.SheetView, ByVal tag As String) As Integer

            Dim col As Spread.Column = sheet.Columns(tag)

            If col Is Nothing Then
                Return -1
            End If

            Return col.Index
        End Function

#End Region

#Region "スプレッド編集行対応(保存対象ブロックNoとして記録・対象セルの色等書式設定"
        ''' <summary>
        ''' スプレッド編集行対応
        ''' 
        ''' ・保存対象ブロックNoとして記録
        ''' ・対象セルの色等書式設定
        ''' 
        ''' </summary>
        ''' <param name="isBase">表示スプレッドが基本の場合はTRUEをセットする</param>
        ''' <param name="aRow">行位置</param>
        ''' <param name="aCol">列位置</param>
        ''' <param name="aRowCount">行件数</param>
        ''' <param name="aColCount">列件数</param>
        ''' <remarks></remarks>
        Public Sub SetEditRowProc(ByVal isBase As Boolean, _
                                                    ByVal aRow As Integer, _
                                                    ByVal aCol As Integer, _
                                            Optional ByVal aRowCount As Integer = 1, _
                                            Optional ByVal aColCount As Integer = 1)

            Dim sheet As SheetView = Nothing
            Dim hidSheet As SheetView = Nothing

            If isBase = True Then
                sheet = _frmDispYosanBuhinEdit20.spdParts_Sheet1
            End If

            'セル編集モード時にコピーした場合、以下を行う。
            If aRowCount = 0 Then
                aRowCount = 1
            End If

            '行コピー時に以下を行う'
            If aCol = -1 Then
                aCol = 0
            End If

            '編集されたセルは太文字・青文字にする
            sheet.Cells(aRow, aCol, aRow + aRowCount - 1, aCol + aColCount - 1).ForeColor = Color.Blue
            sheet.Cells(aRow, aCol, aRow + aRowCount - 1, aCol + aColCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

            '基本・号車共通列を書式設定
            If aCol <= GetTagIdx(sheet, SpdKoseiObserver.TAG_BUHIN_NO_KBN) Then
                Dim endCol As Integer = aCol + aColCount - 1
                '共通列を超える場合は共通列の最大位置にする
                If endCol > GetTagIdx(sheet, SpdKoseiObserver.TAG_BUHIN_NO_KBN) Then
                    endCol = GetTagIdx(sheet, SpdKoseiObserver.TAG_BUHIN_NO_KBN)
                End If

                '同期対象スプレッドの文字種も変更 
                If sheet.Rows.Count >= aRow + aRowCount - 1 Then
                    sheet.Cells(aRow, aCol, aRow + aRowCount - 1, endCol).ForeColor = Color.Blue
                    sheet.Cells(aRow, aCol, aRow + aRowCount - 1, endCol).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                End If
            End If

        End Sub
#End Region

#Region "取込機能(バックアップ削除)"
        ''' <summary>
        ''' 取込機能(バックアップ削除)
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub importBacupRemove()
            Dim spdParts As FpSpread = _frmDispYosanBuhinEdit20.spdParts

            'バックアップ基本シート削除
            spdParts.Sheets.Remove(spdParts.Sheets(1))

        End Sub
#End Region

#Region "取込機能(リストア)"
        ''' <summary>
        ''' 取込機能(リストア)
        ''' </summary>
        ''' <param name="vos"></param>
        ''' <remarks></remarks>
        Private Sub importRestore(ByVal vos As List(Of YosanBuhinEdit.KouseiBuhin.Dao.Vo.BuhinListVo))

            Dim spdParts As FpSpread = _frmDispYosanBuhinEdit20.spdParts

            'ヘッダー初期化
            _frmDispYosanBuhinEdit20.Refresh()

            'スプレッド情報リストア
            Dim befBaseName As String = _frmDispYosanBuhinEdit20.spdParts_Sheet1.SheetName

            '取込した基本シート削除
            spdParts.Sheets.Remove(spdParts.Sheets(0))
            'ﾊﾞｯｸｱｯﾌﾟ用シートをを正規データ用に変更
            spdParts.Sheets(0).SheetName = befBaseName
            '元シートのアドレスにアクセスされないように上書きしておく
            _frmDispYosanBuhinEdit20.spdParts_Sheet1 = spdParts.Sheets(0)
            '画面描画
            _frmDispYosanBuhinEdit20.Refresh()

            '非表示を解除
            spdParts.Sheets(0).Visible = True

        End Sub
#End Region

#Region "取込機能(バックアップ)"
        ''' <summary>
        ''' Excel取込機能(バックアップ)
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub importBackup()

            _frmDispYosanBuhinEdit20.Refresh()

            Dim spdBase As FpSpread = _frmDispYosanBuhinEdit20.spdParts

            '基本スプレッドバックアップ
            Dim baseCopyIndex As Integer = -1
            baseCopyIndex = spdBase.Sheets.Add(CopySheet(_frmDispYosanBuhinEdit20.spdParts_Sheet1))
            spdBase.Sheets(baseCopyIndex).SheetName = "BackupBuhin"

            'バックアップシートの非表示
            spdBase.Sheets(baseCopyIndex).Visible = False

            _frmDispYosanBuhinEdit20.Refresh()

        End Sub

#End Region


#Region "マウス右クリック操作"

        Private Sub ContextMenuStrip1_Opening_1(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
            Dim sheet As Spread.SheetView = spdBuhin.ActiveSheet

            If sheet.ActiveRowIndex >= 0 And rbtNomal.Checked = True Then
                LessToolStripMenuItem.Enabled = True
                LessToolStripMenuItem.Visible = True
            Else
                LessToolStripMenuItem.Enabled = False
                LessToolStripMenuItem.Visible = False
            End If

            If sheet.ActiveRowIndex >= 0 Then
                View3DMenuItem.Enabled = True
                View3DMenuItem.Visible = True
            Else
                View3DMenuItem.Enabled = False
                View3DMenuItem.Visible = False
            End If
        End Sub

        Private Sub LessToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LessToolStripMenuItem.Click

            If Not StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) And _
                Not StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_VER1) And _
                Not StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_VER2) Then
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

#End Region

#Region "スプレッド操作"

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

                    If StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Or _
                        StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_VER1) Or _
                        StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_VER2) Then
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

                    If StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_MBOM_SHISAKUTEHAI) Or _
                        StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_VER1) Or _
                        StringUtil.Equals(cmbSelect.Text, HOYOU_SELECT_VER2) Then
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
        ''' 仕様項目のリーブセル
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdBuhinShiyou_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles spdBuhinShiyou.LeaveCell

            If spdBuhinShiyou_Sheet1.ActiveColumnIndex = 0 Then
                spdBuhinShiyou_Sheet1.Cells(Logic.KouseiBuhinSelectorSubject.spd_BuhinShiyou_startRow, _
                                            1, _
                                            Logic.KouseiBuhinSelectorSubject.spd_BuhinShiyou_startRow, _
                                            spdBuhinShiyou_Sheet1.ColumnCount - 1).Value = ""
            End If

            '再描画停止
            spdBuhinShiyou.SuspendLayout()

            ' 車型のコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setShiyouJyouhouListSyagata()
            'ｸﾞﾚｰﾄﾞのコンボボックスを生成する。
            _KouseiBuhinSelectorLogic.setShiyouJyouhouListGrade()
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

            spdBuhinShiyou.SetViewportLeftColumn(1, mLeftCol)

            '           スクロールを右端に移動させた上、画面上の項目をクリックした場合、
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

#Region "スプレッドシートのコピー"
        ''' <summary>
        ''' スプレッドシートのコピー
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CopySheet(ByVal sheet As FarPoint.Win.Spread.SheetView) As FarPoint.Win.Spread.SheetView

            Dim newSheet As FarPoint.Win.Spread.SheetView = Nothing

            If Not IsNothing(sheet) Then

                newSheet = FarPoint.Win.Serializer.LoadObjectXml(GetType(FarPoint.Win.Spread.SheetView), FarPoint.Win.Serializer.GetObjectXml(sheet, "CopySheet"), "CopySheet")

            End If

            Return newSheet

        End Function

#End Region

#End Region

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

        Private Sub btnBlockAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlockAll.Click
            For idx As Integer = 0 To spdBlock_Sheet1.RowCount - 1
                spdBlock_Sheet1.AddSelection(idx, 1, 1, 1)
            Next
        End Sub

        Private Sub btnPattern_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPattern.Click
            If StringUtil.Equals(pnlPattern.Visible, True) Then

                pnlPattern.Visible = False
                btnPattern.Text = "パターン名表示"
            Else
                spdPattern.ActiveSheet.SetActiveCell(0, 1)
                spdPattern.ActiveSheet.SetActiveCell(0, 0)

                pnlPattern.Visible = True
                btnPattern.Text = "パターン名非表示"
            End If
        End Sub

        Private Sub 全チェックToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 全チェックToolStripMenuItem.Click
            For rowindex As Integer = 0 To spdBuhin_Sheet1.RowCount - 1
                spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Select).Value = True
            Next
        End Sub

        Private Sub 全解除ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 全解除ToolStripMenuItem.Click
            For rowindex As Integer = 0 To spdBuhin_Sheet1.RowCount - 1
                spdBuhin_Sheet1.Cells(rowindex, _KouseiBuhinSelectorLogic.spd_Buhin_Col_Select).Value = False
            Next
        End Sub

    End Class

End Namespace

