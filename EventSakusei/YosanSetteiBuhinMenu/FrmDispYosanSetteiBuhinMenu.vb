Imports EBom.Common
Imports EBom.Data
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
Imports EventSakusei.ShisakuBuhinEditEvent.Dao
Imports EventSakusei.YosanSetteiBuhinMenu.Dao
Imports EventSakusei.YosanSetteiBuhinMenu.Logic.Impl
Imports EventSakusei.YosanSetteiBuhinSakusei
Imports EventSakusei.YosanSetteiBuhinEdit

Namespace YosanSetteiBuhinMenu
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FrmDispYosanSetteiBuhinMenu : Implements Observer

        Private m_spCom As SpreadCommon

        '---------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------
        '２次改修
        Public Sub ExclusiveControl()
            Dim eventVo As New TExclusiveControlEventVo
            Dim eventDao As ExclusiveControlEventDao = New ExclusiveControlEventImpl

            'キー情報が無いとエラーになるので事前にチェック（イレギュラー以外あるはず）
            eventVo = eventDao.FindByPk(subject.EventCode, subject.EditMode)
            If IsNothing(eventVo) Then
                'キー情報が無ければスルー。
            Else
                'KEY情報をセット
                eventVo.ShisakuEventCode = subject.EventCode    'イベントコード
                eventVo.EditMode = subject.EditMode             '編集モード
                'KEY情報を削除
                eventDao.DeleteByPk(eventVo)
            End If

        End Sub

        '---------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------
        '２次改修
        ''' <summary>
        ''' 排他制御イベント情報の更新処理
        ''' </summary>
        ''' <param name="StrEventCode">イベントコード</param>
        ''' <param name="StrEditMode">編集モード（1:手配担当,2:予算担当）</param>
        ''' <param name="isMode">「登録」の場合、true</param>
        ''' <remarks></remarks>
        Private Sub RegisterMain(ByVal StrEventCode As String, _
                                 ByVal StrEditMode As String, _
                                 ByVal isMode As Boolean)

            Dim aShisakuDate As New ShisakuDate
            Dim eventVo As New TExclusiveControlEventVo
            Dim eventDao As ExclusiveControlEventDao = New ExclusiveControlEventImpl

            'KEY情報
            eventVo.ShisakuEventCode = StrEventCode 'イベントコード
            eventVo.EditMode = StrEditMode         '編集モード
            '編集情報
            eventVo.EditUserId = LoginInfo.Now.UserId
            eventVo.EditDate = aShisakuDate.CurrentDateDbFormat
            eventVo.EditTime = aShisakuDate.CurrentTimeDbFormat
            '作成情報
            eventVo.CreatedUserId = LoginInfo.Now.UserId
            eventVo.CreatedDate = aShisakuDate.CurrentDateDbFormat
            eventVo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
            '更新情報
            eventVo.UpdatedUserId = LoginInfo.Now.UserId
            eventVo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
            eventVo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

            '追加モードの場合、インサートする。
            If isMode = True Then
                eventDao.InsetByPk(eventVo)
            Else
                eventDao.UpdateByPk(eventVo)
            End If

        End Sub

        Private Sub FrmDispYosanSetteiBuhinMenu_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
            '---------------------------
            '---------------------------
            '２次改修
            '排他情報をクリア
            ExclusiveControl()
            '---------------------------
            '---------------------------
        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            '---------------------------
            '---------------------------
            '２次改修
            '排他情報をクリア
            ExclusiveControl()
            '---------------------------
            '---------------------------

            Application.Exit()
            System.Environment.Exit(0)
        End Sub

        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            Me.Close()
        End Sub

        Private Sub spdParts_change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdParts.Change
            'リストコード情報を更新する'
            Dim UpMemo As ListCodeDao = New ListCodeDaoImpl
            If spdParts_Sheet1.Cells(e.Row, 1).Value.Equals(txtListCode.Text) Then
                UpMemo.UpdateListCode(txtListCode.Text, spdParts_Sheet1.Cells(e.Row, 8).Value)
            ElseIf spdParts_Sheet1.Cells(e.Row - 1, 1).Value.Equals(txtListCode.Text) Then
                UpMemo.UpdateListCode(txtListCode.Text, spdParts_Sheet1.Cells(e.Row - 1, 8).Value)
            End If
        End Sub

        ''' <summary>
        ''' スプレッドのEnterCellイベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub spdParts_EnterCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EnterCellEventArgs) Handles spdParts.EnterCell
            If e.Row = -1 OrElse e.Column = -1 Then
                Return
            End If

            ' 選択範囲の情報を表示します。
            txtListCode.Text = spdParts.ActiveSheet.GetText(e.Row, 0)

            'リストコード入力時は削除と手配帳編集ボタンを使用可能へ
            If txtListCode.Text <> "" Then
                SettingEnabled(BtnDelete, True)
                SettingEnabled(BtnTehaichoEdit, True)
            Else
                SettingEnabled(BtnDelete, False)
                SettingEnabled(BtnTehaichoEdit, False)
            End If
        End Sub

        '予算単価設定用 部品表作成'
        Private Sub btnTehaichoSakusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTehaichoSakusei.Click

            Using frmYosanSettei As New frmDispYosanSetteiBuhinSakusei(subject.EventCode)
                If frmYosanSettei.cmbGroupNo.Items.Count = 0 Then
                    ComFunc.ShowInfoMsgBox("作成対象がありません")
                    Return
                End If
                frmYosanSettei.ShowDialog()
            End Using
            'スプレッドを更新する'
            setSpreadData(subject.EventCode)
            setSpreadStyle()

        End Sub

        '予算設定部品表編集'
        Private Sub btnTehaichouEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTehaichoEdit.Click

            Using frmYosanBuhin As New frmDispYosanSetteiBuhinEdit(subject.EventCode, txtListCode.Text)

                '初期化正常完了で編集画面表示
                If frmYosanBuhin.InitComplete = True Then
                    Me.Hide()

                    frmYosanBuhin.ShowDialog()

                    Me.Show()
                End If
            End Using

        End Sub

        'スプレッドセルのダブルクリックイベント'
        Private Sub spdParts_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick
            'セルをダブルクリックした場合もメニューへ遷移させます。　6/24追加要望
            'ダブルクリック行のイベントコードをメニューへ渡します。

            If StringUtil.IsEmpty(txtListCode.Text) Then
                txtListCode.Text = spdParts.ActiveSheet.GetText(e.Row, 0)
            End If
            Using frmYosanBuhin As New frmDispYosanSetteiBuhinEdit(subject.EventCode, txtListCode.Text)

                '初期化正常完了で編集画面表示
                If frmYosanBuhin.InitComplete = True Then
                    Me.Hide()

                    frmYosanBuhin.ShowDialog()

                    Me.Show()
                End If
            End Using




            ''リストコードが空なら現在選択アクティブなセルのリストコードを入れる'
            'If StringUtil.IsEmpty(txtListCode.Text) Then
            '    txtListCode.Text = spdParts.ActiveSheet.GetText(e.Row, 1)
            'End If
            ''↓↓↓2014/12/24 試作１課フラグを渡す TES)張 CHG BEGIN
            ''Using frm19 As New frm19DispTehaichoMenu(subject.EventCode, txtListCode.Text)
            'Using frm19 As New frm19DispTehaichoMenu(subject.EventCode, txtListCode.Text, Me._isSisaku1KaFlg)
            '    '↑↑↑2014/12/24 試作１課フラグを渡す TES)張 CHG END
            '    Me.Hide()
            '    frm19.ShowDialog()
            '    Me.Show()
            'End Using

            'setSpreadData(subject.EventCode)
            'setSpreadStyle()

        End Sub

        Private Sub dtpShimekiribi_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpSyochiShimekiribi.ValueChanged
            subject.SyochiShimekiribi = dtpSyochiShimekiribi.Value
            subject.NotifyObservers()
        End Sub

        Private _WasUpdate As Boolean
        ''' <summary>
        ''' 保存、または、登録をしたかを返す
        ''' </summary>
        ''' <returns>保存・登録をした場合、true</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WasUpdate() As Boolean
            Get
                Return _WasUpdate
            End Get
        End Property

        Private ReadOnly ListObserver As spdListObserver
        Private subject As Logic.YosanSetteiBuhinMenu

        ''' <summary>
        ''' コンストラクタ(新規登録モード)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(Nothing, Nothing)
        End Sub

        ''' <summary>
        ''' コンストラクタ(編集モード)
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="editMode">編集モード（1:手配担当,2:予算担当）</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal editMode As String)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            subject = New Logic.YosanSetteiBuhinMenu(LoginInfo.Now, shisakuEventCode, editMode)
            subject.AddObserver(Me)

            Initialize()

            'スプレッドの初期化'
            SpreadUtil.Initialize(spdParts)

            subject.NotifyObservers()
        End Sub

        Private Sub Initialize()
            ShisakuFormUtil.setTitleVersion(Me)
            LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_08
            ShisakuFormUtil.SetDateTimeNow(Me.LblDateNow, Me.LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(Me.LblCurrUserId, Me.LblCurrBukaName)

            SpreadUtil.Initialize(spdParts)

            LblMessage.Text = "処理を選択してください。"

        End Sub

        'スプレッドの設定'
        Private Sub setSpreadData(ByVal shisakuEventCode As String)

            Dim dtList As DataTable = New DataTable()
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                If Not shisakuEventCode = String.Empty Then
                    db.AddParameter("@ShisakuEventCode", shisakuEventCode, DbType.AnsiString)
                Else
                    db.Close()
                    Return
                End If
                Dim ListDao As ListCodeDao = New ListCodeDaoImpl
                db.Fill(ListDao.GetListCode(shisakuEventCode), dtList)
                ' シートにデータセットを接続します。
                spdParts.ActiveSheet.DataSource = dtList
            End Using

            'リストコードの値によって文字を変化させる'
            For index As Integer = 0 To spdParts.ActiveSheet.RowCount - 1

                If spdParts_Sheet1.Cells(index, 5).Value = "1" Then
                    spdParts_Sheet1.Cells(index, 5).Value = "無"
                ElseIf Not StringUtil.IsEmpty(spdParts_Sheet1.Cells(index, 5).Value) Then
                    spdParts_Sheet1.Cells(index, 5).Value = "有"
                End If

                If spdParts_Sheet1.Cells(index, 6).Value = "1" Then
                    spdParts_Sheet1.Cells(index, 6).Value = "する"
                ElseIf Not StringUtil.IsEmpty(spdParts_Sheet1.Cells(index, 6).Value) Then
                    spdParts_Sheet1.Cells(index, 6).Value = "しない"
                End If
                If spdParts_Sheet1.Cells(index, 7).Value = "1" Then
                    spdParts_Sheet1.Cells(index, 7).Value = "する"
                ElseIf Not StringUtil.IsEmpty(spdParts_Sheet1.Cells(index, 7).Value) Then
                    spdParts_Sheet1.Cells(index, 7).Value = "しない"
                End If

            Next

        End Sub

        'スプレッドの初期設定'
        Private Sub setSpreadStyle()

            Dim sheet = spdParts_Sheet1
            sheet.SetColumnWidth(0, 110)
            sheet.Columns(0).Locked = True
            sheet.SetColumnWidth(1, 55)
            sheet.Columns(1).Locked = True
            sheet.SetColumnWidth(2, 70)
            sheet.Columns(2).Locked = True
            sheet.SetColumnWidth(3, 157)
            sheet.Columns(3).Locked = True
            sheet.SetColumnWidth(4, 50)
            sheet.Columns(4).Locked = True
            sheet.SetColumnWidth(5, 50)
            sheet.Columns(5).Locked = True
            sheet.SetColumnWidth(6, 50)
            sheet.Columns(6).Locked = True
            sheet.SetColumnWidth(7, 50)
            sheet.Columns(7).Locked = True
            sheet.SetColumnWidth(8, 256)
            sheet.Columns(8).Locked = False

            For index As Integer = 0 To spdParts.ActiveSheet.RowCount - 1
                '工事指令Noは真ん中に'
                spdParts_Sheet1.Cells(index, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                '自給品の消しこみは真ん中に'
                spdParts_Sheet1.Cells(index, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                '比較織込みは真ん中に'
                spdParts_Sheet1.Cells(index, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                '集計コードからの展開は真ん中に'
                spdParts_Sheet1.Cells(index, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            Next
        End Sub

        Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            lblEventCode.Text = subject.EventCode
            lblEventName.Text = subject.EventName
            lblStatus.Text = subject.StatusName

            If subject.SyochiShimekiribi IsNot Nothing Then
                dtpSyochiShimekiribi.Checked = True
                dtpSyochiShimekiribi.Value = subject.SyochiShimekiribi
            Else
                dtpSyochiShimekiribi.Checked = False
            End If
            lblShimekiribi.Text = subject.Shimekiribi

            If Not StringUtil.IsEmpty(subject.EventCode) Then
                setSpreadData(subject.EventCode)
                'タイトル行をロックしたい'
                setSpreadStyle()
            Else
                spdParts_Sheet1.ColumnHeader.Visible = False
            End If

            dtpSyochiShimekiribi.Visible = True
            dtpSyochiShimekiribi.Enabled = False

            ' 以下のコントロールも二次では使用しない. false固定
            SettingEnabled(BtnDelete, False)
            If StringUtil.IsEmpty(subject.EventCode) Then
                spdParts.Enabled = False
                spdParts_Sheet1.RowCount = 0
            End If
            SettingEnabled(BtnTehaichoEdit, False)

        End Sub

        ''' <summary>
        ''' テキストに全角文字が入力されているかを返す
        ''' </summary>
        ''' <param name="text">入力文字</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function text_validate(ByVal text As Object) As Boolean
            Dim moji As Integer = Len(text)
            Dim bytecount As Integer = System.Text.Encoding.GetEncoding("Shift-JIS").GetByteCount(text.ToString())
            If moji <> bytecount Then
                Return False
            End If
            Return True
        End Function

#Region "SpreadのColumnのTag"
        ''' <summary>リストコード</summary>
        Private ReadOnly TAG_LISTCODE As String = "LISTCODE_Column"
        ''' <summary>グループ</summary>
        Private ReadOnly TAG_GROUP As String = "JYOUTAI2_Column"
        ''' <summary>工事指令No</summary>
        Private ReadOnly TAG_KOUJISHISREINO As String = "NO_Column"
        ''' <summary>イベント名称</summary>
        Private ReadOnly TAG_EVENTNAME As String = "KAITEI_Column"
        ''' <summary>台数</summary>
        Private ReadOnly TAG_DAISU As String = "UNIT_Column"
        ''' <summary>改訂</summary>
        Private ReadOnly TAG_KAITEI As String = "MEISHOU_Column"
        ''' <summary>メモ欄</summary>
        Private ReadOnly TAG_MEMO As String = "TANTOU_Column"

#End Region

        Private buttonBackColors As New Dictionary(Of Button, Color)
        Private Sub SettingEnabled(ByVal aButton As Button, ByVal enabled As Boolean)
            If enabled Then
                aButton.ForeColor = Color.Black
                If buttonBackColors.ContainsKey(aButton) Then
                    aButton.BackColor = buttonBackColors(aButton)
                End If
                aButton.Enabled = True
            Else
                aButton.ForeColor = Color.Black
                If Not buttonBackColors.ContainsKey(aButton) Then
                    buttonBackColors.Add(aButton, aButton.BackColor)
                End If
                aButton.BackColor = Color.White
                aButton.Enabled = False
            End If
        End Sub

        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub

        Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
            If frm01Kakunin.ConfirmOkCancel("削除を実行しますか？") <> MsgBoxResult.Ok Then
                Return
            End If

            If subject.DeleteCheck(subject.ShisakuEventCode, txtListCode.Text) Then
                '何かメッセージがあったほうがいいかも'
                frm01Kakunin.lblKakunin.Text = "削除できません"
                frm01Kakunin.ShowDialog()

                Return
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            Try

                '削除処理'
                subject.PerformDelete(subject.ShisakuEventCode, txtListCode.Text)
                subject.NotifyObservers()
                'リストコードをクリアする。
                txtListCode.Text = Nothing
                '削除ボタン、手配帳編集ボタンを使用不可にする。
                SettingEnabled(BtnDelete, False)
                SettingEnabled(BtnTehaichoEdit, False)
            Catch ex As TableExclusionException
                ComFunc.ShowInfoMsgBox(ex.Message)
            Finally
                _WasUpdate = True ' 排他例外だったとしても、誰かが更新したから.
            End Try

            setSpreadData(subject.ShisakuEventCode)
            setSpreadStyle()

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default
        End Sub
    End Class
End Namespace
