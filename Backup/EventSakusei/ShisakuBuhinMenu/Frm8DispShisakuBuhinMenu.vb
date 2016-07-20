Imports EBom.Common
Imports EBom.Data
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.EventEdit
Imports EventSakusei.TehaichoSakusei
Imports EventSakusei.TehaichoMenu
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports EventSakusei.ShisakuBuhinMenu.Export2Excel
Imports EventSakusei.ShisakuBuhinMenu.Logic.Impl
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports EventSakusei.ShisakuBuhinMenu.Ui
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
Imports EventSakusei.ShisakuBuhinEditEvent.Dao
Imports EventSakusei.ShisakuBuhinEditBlock.Dao


Namespace ShisakuBuhinMenu
    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm8DispShisakuBuhinMenu : Implements Observer

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

        Private Sub Frm8DispShisakuBuhinMenu_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
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
            ''---------------------------
            ''---------------------------
            ''２次改修
            ''排他情報をクリア
            'ExclusiveControl()
            ''---------------------------
            ''---------------------------

            Me.Close()
        End Sub

        Private Sub spdParts_change(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.ChangeEventArgs) Handles spdParts.Change

            'リストコード情報を更新する'
            Dim UpMemo As ListCodeDao = New ListCodeDaoImpl
            If spdParts_Sheet1.Cells(e.Row, 1).Value.Equals(txtListCode.Text) Then
                UpMemo.UpdateListCode(txtListCode.Text, spdParts_Sheet1.Cells(e.Row, 10).Value)
            ElseIf spdParts_Sheet1.Cells(e.Row - 1, 1).Value.Equals(txtListCode.Text) Then
                UpMemo.UpdateListCode(txtListCode.Text, spdParts_Sheet1.Cells(e.Row - 1, 10).Value)
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
            txtListCode.Text = spdParts.ActiveSheet.GetText(e.Row, 1)

            'Dim a As String = spdParts_Sheet1.Cells(e.Row, 10).Value

            'If spdParts_Sheet1.Cells(e.Row, 10).Value = a Then
            '    Dim UpMemo As New ListCodeDaoImpl
            '    UpMemo.UpdateListCode(spdParts_Sheet1.Cells(e.Row, 1).Value, spdParts_Sheet1.Cells(e.Row, 10).Value)
            'End If

            'リストコード入力時は削除と手配帳編集ボタンを使用可能へ
            If txtListCode.Text <> "" Then
                SettingEnabled(BtnDelete, True)
                SettingEnabled(BtnTehaichoEdit, True)
            Else
                SettingEnabled(BtnDelete, False)
                SettingEnabled(BtnTehaichoEdit, False)
            End If
        End Sub



        '手配帳作成'
        Private Sub btnTehaichoSakusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTehaichoSakusei.Click
            Using frm18 As New frm18DispTehaichoSakusei(subject.EventCode)
                If StringUtil.IsEmpty(frm18.cmbGroupNo.Text) Then
                    ComFunc.ShowInfoMsgBox("作成対象がありません")
                    Return
                End If
                frm18.ShowDialog()
            End Using
            'スプレッドを更新する'
            setSpreadData(subject.EventCode)
            setSpreadStyle()

        End Sub

        '手配帳編集'
        Private Sub btnTehaichouEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTehaichoEdit.Click
            Me.Hide()
            '↓↓↓2014/12/24 試作１課フラグを渡す TES)張 CHG BEGIN
            'Using frm19 As New frm19DispTehaichoMenu(subject.EventCode, txtListCode.Text)
            Using frm19 As New frm19DispTehaichoMenu(subject.EventCode, txtListCode.Text, Me._isSisaku1KaFlg)
                '↑↑↑2014/12/24 試作１課フラグを渡す TES)張 CHG END
                frm19.ShowDialog()
            End Using
            Me.Show()
            setSpreadData(subject.EventCode)
            setSpreadStyle()

        End Sub

        'スプレッドセルのダブルクリックイベント'
        Private Sub spdParts_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick
            'セルをダブルクリックした場合もメニューへ遷移させます。　6/24追加要望
            'ダブルクリック行のイベントコードをメニューへ渡します。

            'リストコードが空なら現在選択アクティブなセルのリストコードを入れる'
            If StringUtil.IsEmpty(txtListCode.Text) Then
                txtListCode.Text = spdParts.ActiveSheet.GetText(e.Row, 1)
            End If
            '↓↓↓2014/12/24 試作１課フラグを渡す TES)張 CHG BEGIN
            'Using frm19 As New frm19DispTehaichoMenu(subject.EventCode, txtListCode.Text)
            Using frm19 As New frm19DispTehaichoMenu(subject.EventCode, txtListCode.Text, Me._isSisaku1KaFlg)
                '↑↑↑2014/12/24 試作１課フラグを渡す TES)張 CHG END
                Me.Hide()
                frm19.ShowDialog()
                Me.Show()
            End Using

            setSpreadData(subject.EventCode)
            setSpreadStyle()

        End Sub

        Private Sub btnExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExcelExport.Click
            '画面を綺麗に、実行中のカーソルへ変更。
            'Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor
            Try
                Dim export As New ExportShisakuBuhinMenu2Excel(Me)
            Catch ex As Exception
                MsgBox("Excelの書込みに失敗しました。", MsgBoxStyle.Information, "エラー")
            End Try



            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

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

        Private Sub BtnEventRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEventRegister.Click

            '--------------------------------------------------------------------------------------
            '2012年度下期　製作一覧システム　機能追加
            '   新規作成モードの場合、以下の確認画面を表示する。
            '   　新規作成モード＝試作イベントコードがNothing

            Dim strHakouNo As String = ""
            Dim strKaiteiNo As String = ""

            '自画面非表示
            Me.Hide()

            If StringUtil.Equals(subject.ShisakuEventCode, Nothing) Then
                If frm01Kakunin.ConfirmYesNo("発行済み製作一覧を参照しますか？") = MsgBoxResult.Yes Then
                    '製作一覧　選択画面を表示する。
                    '製作一覧画面表示。
                    Using frm As New Frm9DispSeisakuIchiranSelect
                        frm.Execute()
                        frm.ShowDialog()
                        strHakouNo = frm.HakouNo
                        strKaiteiNo = frm.KaiteiNo
                    End Using
                End If
            Else
                '発行№の改訂がアップしているかチェック。
                '   試作イベントコードで発行№、改訂№を取得する。
                Dim EventDao As IShisakuEventDao = New EventSakusei.ShisakuBuhinMenu.Dao.ShisakuEventDaoImpl
                Dim EventInfo = EventDao.GetShisakuEvent(subject.ShisakuEventCode)
                '   発行№、ステータス（20：承認済み）で最新の改訂№を取得する。
                Dim HakouNo As TSeisakuHakouDao = New TSeisakuHakouDaoImpl
                Dim SeisakuInfo = HakouNo.GetTSeisakuHakouHdStatus(EventInfo.SeisakuichiranHakouNo, "20")
                '   製作一覧情報の改訂№とイベント情報の改訂№を比較し、上がっている場合には確認画面を表示する。
                If Not SeisakuInfo Is Nothing Then
                    If Convert.ToInt32(SeisakuInfo.KaiteiNo) > EventInfo.SeisakuichiranHakouNoKai Then


                        '二つ以上改訂が上がっているか？
                        Dim KaiteiNo As String = CStr(EventInfo.SeisakuichiranHakouNoKai)
                        If StringUtil.Equals(KaiteiNo.Length, 1) Then
                            KaiteiNo = KaiteiNo.PadLeft(2, "0"c)
                        End If
                        Dim KaiteiCount = HakouNo.GetTSeisakuHakouHdKaitei(EventInfo.SeisakuichiranHakouNo, KaiteiNo)

                        If KaiteiCount.Count > 1 Then
                            '製作一覧　選択画面を表示する。
                            '製作一覧画面表示。


                            Using frm As New Frm9DispSeisakuIchiranSelectKaiteiNo
                                frm.Execute(EventInfo.SeisakuichiranHakouNo, KaiteiNo)
                                frm.ShowDialog()
                                strHakouNo = frm.HakouNo
                                strKaiteiNo = frm.KaiteiNo
                            End Using
                        Else
                            If frm01Kakunin.ConfirmYesNo("最新情報へ更新しますか？", strHakouNo) = MsgBoxResult.Yes Then
                                '最新情報への更新処理を追加する？
                                '   または「Frm9DispEventEdit」へパラメータを送って自画面で更新処理を実施するか？検討。
                                '発行№と改訂№をセット
                                strHakouNo = SeisakuInfo.HakouNo
                                strKaiteiNo = SeisakuInfo.KaiteiNo
                            End If
                        End If

                    End If
                End If

            End If
                '--------------------------------------------------------------------------------------

                '新規作成画面表示。
                '   試作イベントコード、製作一覧発行№、製作一覧発行№改訂№
                Using frm As New Frm9DispEventEdit(subject.ShisakuEventCode, strHakouNo, strKaiteiNo)
                    frm.ShowDialog()
                    If frm.WasSaveRegister AndAlso Not StringUtil.IsEmpty(frm.ShisakuEventCode) Then
                        subject.ShisakuEventCode = frm.ShisakuEventCode
                        subject.Load()
                        subject.NotifyObservers()
                        _WasUpdate = True
                    End If
                End Using

                '自画面再表示
                Me.Show()
                System.GC.Collect()

                '--------------------------------------------------------------------------------
                '２次改修
                '   イベント編集画面から戻ってきたら関連するブロックの部品表が編集中かチェック
                '   編集中なら編集中のブロック一覧を表示して、ステータス変更ボタン使用不可
            BuhinEditCheck()
            '--------------------------------------------------------------------------------

        End Sub

        Private Sub BtnSekkeiTenkai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSekkeiTenkai.Click

            '----------------------------------------------------------------------------------
            '２次改修
            '   使用中のブロックが無いかチェックする。
            BuhinEditCheck()
            If BtnSekkeiTenkai.Enabled = False Then
                Return
            End If
            '----------------------------------------------------------------------------------

            Dim flg As Boolean = False
            Cursor.Current = Cursors.WaitCursor
            Using frm As New Frm8DispSekkeika(subject.ShisakuEventCode, flg)
                Cursor.Current = Cursors.Default
                frm.ShowDialog()
                If frm.ResultOk = False Then
                    MsgBox("設計展開をキャンセルしました。", MsgBoxStyle.Information, "キャンセル")
                    Exit Sub
                End If
            End Using

            If frm01Kakunin.ConfirmOkCancel("設計展開を実行しますか？") <> MsgBoxResult.Ok Then
                Return
            End If

            Threading.Thread.Sleep(2000)

            '画面を綺麗に、実行中のカーソルへ変更。
            'Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            Try '
                subject.PerformSekkeiTenkai(DateUtil.ConvYyyymmddToDate(Format((dtpSyochiShimekiribi.Value), "yyyyMMdd")))
                subject.NotifyObservers()
            Catch ex As TableExclusionException
                'エラーのときも元に戻す'
                Me.Cursor = Cursors.Default

                ComFunc.ShowInfoMsgBox(ex.Message)
            Finally
                _WasUpdate = True ' 排他例外だったとしても、誰かが更新したから.
            End Try

            MsgBox("設計展開が終了しました。", MsgBoxStyle.Information, "正常終了")

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub

        '削除ボタンの処理'
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

        Private Sub BtnSashimodosi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSashimodosi.Click

            '----------------------------------------------------------------------------------
            '２次改修
            '   使用中のブロックが無いかチェックする。
            BuhinEditCheck()
            If BtnSashimodosi.Enabled = False Then
                Return
            End If
            '----------------------------------------------------------------------------------


            If frm01Kakunin.ConfirmOkCancel("差戻しを実行しますか？") <> MsgBoxResult.Ok Then
                Return
            End If
            If frm01Kakunin.ConfirmOkCancel("設計情報が破棄されます。", "よろしいですか？") <> MsgBoxResult.Ok Then
                Return
            End If


            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            Try

                subject.PerformSashimodoshi()
                subject.NotifyObservers()
            Catch ex As TableExclusionException
                ComFunc.ShowInfoMsgBox(ex.Message)
            Finally
                _WasUpdate = True ' 排他例外だったとしても、誰かが更新したから.
            End Try

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub

        Private Sub BtnShimekiri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShimekiri.Click

            '----------------------------------------------------------------------------------
            '２次改修
            '   使用中のブロックが無いかチェックする。
            BuhinEditCheck()
            If BtnShimekiri.Enabled = False Then
                Return
            End If
            '----------------------------------------------------------------------------------

            Using frm As New Frm8DispShisakuBuhinMenuDATE
                frm.ShowDialog()
                If frm.Result <> MsgBoxResult.Ok Then
                    Return
                End If

                Try
                    '処理が長い時用にカーソルを砂時計にする'
                    Me.Cursor = Cursors.WaitCursor

                    subject.PerformSyochiShimekiri(frm.Shimekiribi)
                    subject.NotifyObservers()
                Catch ex As TableExclusionException
                    'エラーのときも元に戻す'
                    Me.Cursor = Cursors.Default
                    ComFunc.ShowInfoMsgBox(ex.Message)
                Finally
                    _WasUpdate = True ' 排他例外だったとしても、誰かが更新したから.
                End Try
                '処理が終わったら元に戻す'
                Me.Cursor = Cursors.Default
            End Using
        End Sub

        Private Sub BtnKanryou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnKanryou.Click

            '----------------------------------------------------------------------------------
            '２次改修
            '   使用中のブロックが無いかチェックする。
            BuhinEditCheck()
            If BtnKanryou.Enabled = False Then
                Return
            End If
            '----------------------------------------------------------------------------------

            If frm01Kakunin.ConfirmOkCancel("完了を実行しますか？") <> MsgBoxResult.Ok Then
                Return
            End If
            Try
                subject.PerformKanryo()
                subject.NotifyObservers()
            Catch ex As TableExclusionException
                ComFunc.ShowInfoMsgBox(ex.Message)
            Finally
                _WasUpdate = True ' 排他例外だったとしても、誰かが更新したから.
            End Try
        End Sub

        Private Sub BtnChushi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChushi.Click

            '----------------------------------------------------------------------------------
            '２次改修
            '   使用中のブロックが無いかチェックする。
            BuhinEditCheck()
            If BtnChushi.Enabled = False Then
                Return
            End If
            '----------------------------------------------------------------------------------

            If frm01Kakunin.ConfirmOkCancel("中止を実行しますか？") <> MsgBoxResult.Ok Then
                Return
            End If
            Try
                subject.PerformChushi()
                subject.NotifyObservers()
                'txtListCode.Text = ""
            Catch ex As TableExclusionException
                ComFunc.ShowInfoMsgBox(ex.Message)
            Finally
                _WasUpdate = True ' 排他例外だったとしても、誰かが更新したから.
            End Try
        End Sub

        Private ReadOnly ListObserver As spdListObserver
        Private subject As Logic.ShisakuBuhinMenu
        '↓↓↓2014/12/23 試作１課フラグを渡す TES)張 ADD BEGIN
        Private _isSisaku1KaFlg As Integer
        '↑↑↑2014/12/23 試作１課フラグを渡す TES)張 ADD END
        ''' <summary>
        ''' コンストラクタ(新規登録モード)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(Nothing, Nothing)
        End Sub
        '↓↓↓2014/12/23 試作１課フラグを渡す TES)張 CHG BEGIN
        'Public Sub New(ByVal shisakuEventCode As String)
        ''' <summary>
        ''' コンストラクタ(編集モード)
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="editMode">編集モード（1:手配担当,2:予算担当）</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal editMode As String, _
                       Optional ByVal isSisaku1KaFlg As Integer = 0)
            '↑↑↑2014/12/23 試作１課フラグを渡す TES)張 CHG END

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            subject = New Logic.ShisakuBuhinMenu(LoginInfo.Now, shisakuEventCode, editMode)
            subject.AddObserver(Me)

            '↓↓↓2014/12/23 試作１課フラグを渡す TES)張 ADD BEGIN
            Me._isSisaku1KaFlg = isSisaku1KaFlg
            '↑↑↑2014/12/23 試作１課フラグを渡す TES)張 ADD END
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

            '↓↓↓2014/12/23 試作１課フラグを渡す TES)張 ADD BEGIN
            If Me._isSisaku1KaFlg = SHISAKU1KA_SISAKU Then
                '非表示項目を設定
                Me.Panel4.Visible = False
                Me.BtnSekkeiTenkai.Visible = False
                Me.BtnSashimodosi.Visible = False
                Me.BtnShimekiri.Visible = False
                Me.BtnKanryou.Visible = False
                Me.BtnChushi.Visible = False
                Me.Label7.Visible = False
                Me.lblShimekiribi.Visible = False
                Me.BtnExcelExport.Visible = False
                Me.gbBlockMsg.Visible = False
                Me.gbTukurikata.Visible = False
                Me.gbOshirase.Visible = False
                Me.gbEbomKanshi.Visible = False
                Me.BtnTehaichoSakusei.Visible = False
                Me.BtnDelete.Visible = False
                '表示幅または位置を設定
                Me.GroupBox1.Height = 65
                Me.GroupBox1.Location = New Point(114, 107)
                Me.Panel5.Location = New Point(6, 142)
                Me.Height = 435
            End If
            '↑↑↑2014/12/23 試作１課フラグを渡す TES)張 ADD END
            '-----------------------------------------------------------------
            '２次改修
            '   ブロックメッセージ制御ラジオボタンを追加
            'rbMsgOn.Checked = True  'メッセージを出すにチェック
            'rbMsgFull.Checked = True    'フル組にチェック
            '-----------------------------------------------------------------
        End Sub

        'スプレッドの設定'
        Private Sub setSpreadData(ByVal shisakuEventCode As String)

            'BuhinMenuSpreadUtil.InitializeFrm8(spdParts)


            Dim dtList As DataTable = New DataTable()
            'm_spCom = New SpreadCommon(spdParts)
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
                If Not spdParts_Sheet1.RowCount = 0 Then
                    'タイトル行をロックする'
                    'spdParts_Sheet1.Rows(0).Locked = True
                    'txtListCode.Text = spdParts_Sheet1.Cells(0, 1).Value
                End If
            End Using

            'リストコードの値によって文字を変化させる'
            For index As Integer = 0 To spdParts.ActiveSheet.RowCount - 1

                If spdParts_Sheet1.Cells(index, 7).Value = "1" Then
                    spdParts_Sheet1.Cells(index, 7).Value = "無"
                    'spdParts_Sheet1.Cells(index, 7).Value = "する"
                ElseIf Not StringUtil.IsEmpty(spdParts_Sheet1.Cells(index, 7).Value) Then
                    spdParts_Sheet1.Cells(index, 7).Value = "有"
                    'spdParts_Sheet1.Cells(index, 7).Value = "しない"
                End If

                If spdParts_Sheet1.Cells(index, 8).Value = "1" Then
                    spdParts_Sheet1.Cells(index, 8).Value = "する"
                ElseIf Not StringUtil.IsEmpty(spdParts_Sheet1.Cells(index, 8).Value) Then
                    spdParts_Sheet1.Cells(index, 8).Value = "しない"
                End If
                If spdParts_Sheet1.Cells(index, 9).Value = "1" Then
                    spdParts_Sheet1.Cells(index, 9).Value = "する"
                ElseIf Not StringUtil.IsEmpty(spdParts_Sheet1.Cells(index, 9).Value) Then
                    spdParts_Sheet1.Cells(index, 9).Value = "しない"
                End If

            Next

        End Sub

        'スプレッドの初期設定'
        Private Sub setSpreadStyle()

            Dim sheet = spdParts_Sheet1
            sheet.SetColumnWidth(0, 60)
            sheet.Columns(0).Locked = True
            sheet.SetColumnWidth(1, 101)
            sheet.Columns(1).Locked = True
            sheet.SetColumnWidth(2, 64.0)
            sheet.Columns(2).Locked = True
            sheet.SetColumnWidth(3, 80)
            sheet.Columns(3).Locked = True
            sheet.SetColumnWidth(4, 157)
            sheet.Columns(4).Locked = True
            sheet.SetColumnWidth(5, 51)
            sheet.Columns(5).Locked = True
            sheet.SetColumnWidth(6, 38)
            sheet.Columns(6).Locked = True
            sheet.SetColumnWidth(7, 50)
            sheet.Columns(7).Locked = True
            sheet.SetColumnWidth(8, 50)
            sheet.Columns(8).Locked = True
            sheet.SetColumnWidth(9, 50)
            sheet.Columns(9).Locked = True
            sheet.SetColumnWidth(10, 256)
            sheet.Columns(10).Locked = False

            For index As Integer = 0 To spdParts.ActiveSheet.RowCount - 1
                '発注実績は真ん中に'
                spdParts_Sheet1.Cells(index, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                '工事指令Noは真ん中に'
                spdParts_Sheet1.Cells(index, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                '自給品の消しこみは真ん中に'
                spdParts_Sheet1.Cells(index, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                '比較織込みは真ん中に'
                spdParts_Sheet1.Cells(index, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                '集計コードからの展開は真ん中に'
                spdParts_Sheet1.Cells(index, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
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


                'リストコードの値によって文字を変化させる'
                'For index As Integer = 0 To spdParts.ActiveSheet.RowCount - 1
                '    If spdParts_Sheet1.Cells(index, 7).Value = "1" Then
                '        spdParts_Sheet1.Cells(index, 7).Value = "有"
                '    Else
                '        spdParts_Sheet1.Cells(index, 7).Value = "無"
                '    End If

                '    If spdParts_Sheet1.Cells(index, 8).Value = "1" Then
                '        spdParts_Sheet1.Cells(index, 8).Value = "有"
                '    Else
                '        spdParts_Sheet1.Cells(index, 8).Value = "無"
                '    End If
                '    If spdParts_Sheet1.Cells(index, 9).Value = "1" Then
                '        spdParts_Sheet1.Cells(index, 9).Value = "有"
                '    Else
                '        spdParts_Sheet1.Cells(index, 9).Value = "無"
                '    End If
                'Next
                setSpreadStyle()
            Else
                spdParts_Sheet1.ColumnHeader.Visible = False
            End If

            SettingEnabled(BtnEventRegister, subject.Enabled.EventRegister)
            dtpSyochiShimekiribi.Visible = subject.Enabled.ShowShimerkiribi
            dtpSyochiShimekiribi.Enabled = subject.Enabled.Shimekiribi
            SettingEnabled(BtnSekkeiTenkai, subject.Enabled.SekkeiTenkai)



            ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_c) (TES)張 ADD BEGIN
            Panel6.Enabled = subject.Enabled.SekkeiTenkai
            Panel7.Enabled = subject.Enabled.SekkeiTenkai
            ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース改修専用化_c) 酒井 ADD BEGIN
            Panel8.Enabled = subject.Enabled.SekkeiTenkai
            Panel10.Enabled = subject.Enabled.SekkeiTenkai
            ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース改修専用化_c) 酒井 ADD END
            ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_c) (TES)張 ADD END
            SettingEnabled(BtnSashimodosi, subject.Enabled.Sashimodoshi)
            SettingEnabled(BtnShimekiri, subject.Enabled.Shimekiri)
            SettingEnabled(BtnKanryou, subject.Enabled.Kanryo)
            SettingEnabled(BtnChushi, subject.Enabled.Chushi)
            '手配帳作成ボタン. 二次では使用しない. false固定

            SettingEnabled(BtnTehaichoSakusei, subject.Enabled.TehaichoSakusei)
            'SettingEnabled(BtnTehaichoSakusei, False)  
            SettingEnabled(BtnExcelExport, subject.Enabled.ExcelExport)

            ' 以下のコントロールも二次では使用しない. false固定
            SettingEnabled(BtnDelete, False)
            If StringUtil.IsEmpty(subject.EventCode) Then
                spdParts.Enabled = False
                spdParts_Sheet1.RowCount = 0
                'Else
                '    spdParts.Enabled = True
                '    '本当は可変、とりあえず10にしておく'
                '    spdParts_Sheet1.RowCount = 10
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
        ''' <summary>発注実績</summary>
        Private ReadOnly TAG_HACHUJISSEKI As String = "HACHUJISSEKI_Column"
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

        Private Sub BuhinEditCheck()

            '---------------------------------------------------------------------------------------
            '２次改修追加
            '   イベントコードに該当する部品表が編集中かチェックする。
            '   編集中の場合、ステータスの更新が出来ないようにする。
            '   編集中のブロック情報を表示する。
            '------------------------------------------------------------------------------------------------------
            '初期設定
            '------------------------------------------------------------------------------------------------------
            Dim tExclusiveControlBlockDaoImpl As ExclusiveControlBlockDao = New ExclusiveControlBlockImpl
            '排他制御ブロック情報から試作イベントコードが登録されているかチェック。
            Dim ExclusiveControlBlockList = tExclusiveControlBlockDaoImpl.GetExclusiveControlBlock(subject.ShisakuEventCode)
            '------------------------------------------------------------------------------------------------------
            '判断
            '------------------------------------------------------------------------------------------------------
            If ExclusiveControlBlockList.Count = 0 Then
                'MsgBox("選択したイベントの部品表は誰も使用していませんのでステータスの更新が可能です。")
                'イベント更新用のボタンをロックしない。
            Else

                '初期設定
                Dim i As Integer
                '   担当者名取得用
                Dim ExcelImpl As EditBlock2ExcelDao = New EditBlock2ExcelDaoImpl
                Dim BukaCode As String
                Dim BlockNo As String
                Dim UserName As String
                Dim TelNo As String

                '確認画面を表示
                Using frm As New frm02Kakunin


                    For i = 0 To ExclusiveControlBlockList.Count - 1

                        'Nullチェック
                        If StringUtil.IsEmpty(ExcelImpl.FindByShainName(ExclusiveControlBlockList.Item(i).EditUserId)) Then
                            UserName = ""
                        Else
                            UserName = ExcelImpl.FindByShainName(ExclusiveControlBlockList.Item(i).EditUserId)
                        End If
                        If StringUtil.IsEmpty(ExclusiveControlBlockList.Item(i).ShisakuBukaCode) Then
                            BukaCode = ""
                        Else

                            '課略称があるか？
                            If ExclusiveControlBlockList.Item(i).KaRyakuName = "" Then
                                BukaCode = ExclusiveControlBlockList.Item(i).ShisakuBukaCode
                            Else
                                BukaCode = ExclusiveControlBlockList.Item(i).KaRyakuName
                            End If

                        End If
                        If StringUtil.IsEmpty(ExclusiveControlBlockList.Item(i).ShisakuBlockNo) Then
                            BlockNo = ""
                        Else
                            BlockNo = ExclusiveControlBlockList.Item(i).ShisakuBlockNo
                        End If
                        If tExclusiveControlBlockDaoImpl.GetShisakuSekkeiBlockNaisen(subject.ShisakuEventCode, _
                                                                                              BukaCode, BlockNo).Count = 0 Then
                            TelNo = ""
                        Else
                            TelNo = tExclusiveControlBlockDaoImpl.GetShisakuSekkeiBlockNaisen(subject.ShisakuEventCode, _
                                                                                            BukaCode, BlockNo).Item(0).TelNo
                        End If

                        Dim intSpace As Integer = 12
                        Dim LenB As Integer
                        Dim s_jis As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
                        LenB = s_jis.GetByteCount(UserName)
                        intSpace = intSpace - LenB

                        '値を設定
                        If i > 0 Then frm.txtBlockInfo.Text += vbCrLf
                        If intSpace < 0 Then intSpace = 1
                        frm.txtBlockInfo.Text += BlockNo + Space(2) + BukaCode + Space(1) + _
                                                UserName + Space(intSpace) + TelNo

                    Next

                    frm.ShowDialog()

                End Using

                'イベント更新用のボタンをロックする。
                lblStatus.Text = "ブロック編集中"
                lblStatus.ForeColor = Color.Red
                dtpSyochiShimekiribi.Enabled = False
                SettingEnabled(BtnSekkeiTenkai, False)
                ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_d) (TES)張 ADD BEGIN
                Panel6.Enabled = False
                Panel7.Enabled = False
                ''↓↓2014/09/22 Ⅰ.3.設計編集 ベース改修専用化_d) 酒井 ADD BEGIN
                Panel8.Enabled = False
                Panel10.Enabled = False
                ''↑↑2014/09/22 Ⅰ.3.設計編集 ベース改修専用化_d) 酒井 ADD END
                ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_d) (TES)張 ADD END
                SettingEnabled(BtnSashimodosi, False)
                SettingEnabled(BtnShimekiri, False)
                SettingEnabled(BtnKanryou, False)
                SettingEnabled(BtnChushi, False)
            End If
            '---------------------------------------------------------------------------------------

        End Sub

        'フォームオープン後一度だけ実行するイベント
        Private Sub Frm8DispShisakuBuhinMenu_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
            BuhinEditCheck()

            '試作イベントコードに該当するアラート情報／お知らせ通知情報を取得する。
            Dim EventDao As IShisakuEventDao = New EventSakusei.ShisakuBuhinMenu.Dao.ShisakuEventDaoImpl
            Dim EventAlertInfo = EventDao.GetShisakuEvent(subject.ShisakuEventCode)

            If EventAlertInfo IsNot Nothing Then
                'アラート情報を画面に表示する。
                If StringUtil.IsEmpty(EventAlertInfo.BlockAlertFlg) Then
                    rbMsgOn.Checked = True
                    rbMsgFull.Checked = True
                Else
                    '0:表示しない、1：表示する。
                    If EventAlertInfo.BlockAlertFlg = "0" Then
                        rbMsgOff.Checked = True
                    Else
                        rbMsgOn.Checked = True
                    End If
                    '1:フル組、2：移管車改修。
                    If EventAlertInfo.BlockAlertKind = "2" Then
                        rbMsgIkan.Checked = True
                    Else
                        rbMsgFull.Checked = True
                    End If
                End If

                'お知らせ通知情報を画面に表示する。
                If StringUtil.IsEmpty(EventAlertInfo.InfoMailFlg) Then
                    rbMailOn.Checked = True
                Else
                    '0:しない、1：する
                    If EventAlertInfo.InfoMailFlg = "0" Then
                        rbMailOff.Checked = True
                    Else
                        rbMailOn.Checked = True
                    End If
                End If
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 at) (TES)施 ADD BEGIN



                If StringUtil.IsEmpty(EventAlertInfo.EbomKanshiFlg) Then
                    rbEbomKanshiOn.Checked = False
                Else
                    '0:しない、1：する																																				
                    If EventAlertInfo.EbomKanshiFlg = "0" Then
                        rbEbomKanshiOff.Checked = True
                    Else
                        rbEbomKanshiOn.Checked = True
                    End If
                End If

                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 at) (TES)施 ADDEND				


                '↓↓2014/07/23 Ⅰ.2.管理項目追加_b) (TES)張 ADD BEGIN
                '作り方グループフラグを画面に表示する。
                If StringUtil.IsEmpty(EventAlertInfo.TsukurikataFlg) Then
                    rbTsukurikataOff.Checked = True
                Else
                    '0:しない、1：する
                    If EventAlertInfo.TsukurikataFlg = "0" Then
                        rbTsukurikataOff.Checked = True
                        'cmbTsukurikata.Enabled = False
                    Else
                        rbTsukurikataOn.Checked = True
                        'cmbTsukurikata.Enabled=True
                    End If
                End If
                '作り方展開フラグを画面に表示する。
                If StringUtil.IsEmpty(EventAlertInfo.TsukurikataTenkaiFlg) Then
                    rbTsukurikataOff.Checked = True
                Else
                    '0:しない、1：する
                    If EventAlertInfo.TsukurikataTenkaiFlg = "0" Then
                        rbTsukurikataTenkaiOff.Checked = True
                    Else
                        rbTsukurikataTenkaiOn.Checked = True
                    End If
                End If
                '↑↑2014/07/23 Ⅰ.2.管理項目追加_b) (TES)張 ADD END

                MsgOnOff()
                UpdAlertInfo()      'アラート情報
                UpdInfoMailFlg()    'お知らせ通知情報
                '↓↓2014/07/23 Ⅰ.2.管理項目追加_b) (TES)張 ADD BEGIN
                UpdTsukurikataFlg()    '作り方情報
                UpdTsukurikataTenkaiFlg()    '作り方展開情報
                '↑↑2014/07/23 Ⅰ.2.管理項目追加_b) (TES)張 ADD END
            End If

        End Sub
        '---------------------------------------------------------------------------------------------
        '２次改修
        '   ブロック毎にメッセージ表示ＯＮ、ＯＦＦを制御する。
        'Private Sub rbMsgOn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMsgOn.CheckedChanged
        '    MsgOnOff()
        '    UpdAlertInfo()
        'End Sub

        'Private Sub rbMsgOff_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMsgOff.CheckedChanged
        '    MsgOnOff()
        '    UpdAlertInfo()
        'End Sub

        Private Sub rbMsgOn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbMsgOn.Click

            MsgOnOff()
            UpdAlertInfo()

        End Sub

        Private Sub rbMsgOff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbMsgOff.Click

            MsgOnOff()
            UpdAlertInfo()

        End Sub



        Private Sub MsgOnOff()
            'ONならメッセージ選択パネルを表示する。
            ''↓↓2014/08/21 Ⅰ.3.設計編集 ベース改修専用化_b) 酒井 CHG BEGIN
            'If rbMsgOn.Checked = True Then

            '2014/11/18 廃止
            'If rbMsgOn.Checked = True And subject.Enabled.SekkeiTenkai = True Then
            '    ''↑↑2014/08/21 Ⅰ.3.設計編集 ベース改修専用化_b) 酒井 CHG END
            '    ''↓↓2014/07/23 Ⅰ.3.設計編集 ベース改修専用化_b) (TES)張 CHG BEGIN
            '    'Panel7.Visible = True'
            '    Panel7.Enabled = True
            'Else
            '    'Panel7.Visible = False'
            '    Panel7.Enabled = False
            '    ''↑↑2014/07/23 Ⅰ.3.設計編集 ベース改修専用化_b) (TES)張 CHG END
            'End If

        End Sub

        Private Sub UpdAlertInfo()

            '試作イベントアラート情報を更新する
            Dim EventDao As IShisakuEventDao = New EventSakusei.ShisakuBuhinMenu.Dao.ShisakuEventDaoImpl
            Dim EventAlertInfo = EventDao.GetShisakuEvent(subject.ShisakuEventCode)
            Dim strOnOff As String
            Dim strMsg As String

            '0:表示しない、1：表示する。
            If rbMsgOn.Checked = True Then
                strOnOff = "1"
            Else
                strOnOff = "0"
            End If
            '1:フル組、2：移管車改修。
            If rbMsgFull.Checked = True Then
                strMsg = "1"
            Else
                strMsg = "2"
            End If
            'アラート情報を更新
            EventDao.UpdAlertInfo(subject.ShisakuEventCode, strOnOff, strMsg)

        End Sub

        'Private Sub rbMsgIkan_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbMsgIkan.CheckedChanged

        '    UpdAlertInfo()

        'End Sub

        'Private Sub rbMsgFull_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbMsgFull.CheckedChanged

        '    UpdAlertInfo()

        'End Sub

        Private Sub rbMsgIkan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbMsgIkan.Click

            UpdAlertInfo()

        End Sub

        Private Sub rbMsgFull_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbMsgFull.Click

            UpdAlertInfo()

        End Sub

        Private Sub rbMailOn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbMailOn.Click
            UpdInfoMailFlg()
        End Sub

        Private Sub rbMailOff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbMailOff.Click
            UpdInfoMailFlg()
        End Sub

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_f) (TES)張 ADD BEGIN
        Private Sub rbTsukurikataOn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbTsukurikataOn.Click
            UpdTsukurikataFlg()
        End Sub
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_f) (TES)張 ADD END
        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_g) (TES)張 ADD BEGIN
        Private Sub rbTsukurikataOff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbTsukurikataOff.Click
            UpdTsukurikataFlg()
        End Sub
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_g) (TES)張 ADD END

        ''↓↓2014/07/24 Ⅰ.2.管理項目追加_bj) (TES)張 ADD BEGIN
        Private Sub rbTsukurikataTenkaiOn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbTsukurikataTenkaiOn.Click
            UpdTsukurikataTenkaiFlg()
        End Sub
        ''↑↑2014/07/24 Ⅰ.2.管理項目追加_bj) (TES)張 ADD END
        ''↓↓2014/07/24 Ⅰ.2.管理項目追加_bk) (TES)張 ADD BEGIN
        Private Sub rbTsukurikataTenkaiOff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbTsukurikataTenkaiOff.Click
            UpdTsukurikataTenkaiFlg()
        End Sub
        ''↑↑2014/07/24 Ⅰ.2.管理項目追加_bk) (TES)張 ADD END

        Private Sub UpdInfoMailFlg()

            '試作イベントお知らせ通知情報を更新する
            Dim EventDao As IShisakuEventDao = New EventSakusei.ShisakuBuhinMenu.Dao.ShisakuEventDaoImpl
            Dim strOnOff As String

            '0:表示しない、1：表示する。
            If rbMailOn.Checked = True Then
                strOnOff = "1"
            Else
                strOnOff = "0"
            End If
            'お知らせ通知情報を更新
            EventDao.UpdInfoMail(subject.ShisakuEventCode, strOnOff)

        End Sub

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_c) (TES)張 ADD BEGIN
        Private Sub UpdTsukurikataFlg()

            '試作イベント作り方グループフラグを更新する
            Dim EventDao As IShisakuEventDao = New EventSakusei.ShisakuBuhinMenu.Dao.ShisakuEventDaoImpl
            Dim strOnOff As String

            '0:表示しない、1：表示する。
            If rbTsukurikataOn.Checked = True Then
                strOnOff = "1"
            Else
                strOnOff = "0"
            End If
            '作り方グループフラグを更新
            EventDao.UpdTsukurikataFlg(subject.ShisakuEventCode, strOnOff)

        End Sub
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_c) (TES)張 ADD END

        ''↓↓2014/07/24 Ⅰ.2.管理項目追加_bg) (TES)張 ADD BEGIN
        Private Sub UpdTsukurikataTenkaiFlg()

            '試作イベント作り方グループフラグを更新する
            Dim EventDao As IShisakuEventDao = New EventSakusei.ShisakuBuhinMenu.Dao.ShisakuEventDaoImpl
            Dim strOnOff As String

            '0:表示しない、1：表示する。
            If rbTsukurikataTenkaiOn.Checked = True Then
                strOnOff = "1"
            Else
                strOnOff = "0"
            End If
            '作り方展開フラグを更新
            EventDao.UpdTsukurikataTenkaiFlg(subject.ShisakuEventCode, strOnOff)

        End Sub
        ''↑↑2014/07/24 Ⅰ.2.管理項目追加_bg) (TES)張 ADD END

        ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 av (TES)施 ADD BEGIN
        Private Sub UpdEbomKanshi()
            '試作イベントアラート情報を更新する																																																									
            Dim EventDao As IShisakuEventDao = New EventSakusei.ShisakuBuhinMenu.Dao.ShisakuEventDaoImpl
            Dim strOnOff As String

            '0:しない、1：する。																																																									
            If rbEbomKanshiOn.Checked = True Then
                strOnOff = "1"
            Else
                strOnOff = "0"
            End If
            EventDao.UpdEbomKanshi(subject.ShisakuEventCode, strOnOff)
        End Sub
        ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 av) (TES)施 ADDEND	
        ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 aw) (TES)施 ADD BEGIN
        Private Sub rbEbomKanshiOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbEbomKanshiOff.Click
            UpdEbomKanshi()


        End Sub

        Private Sub rbEbomKanshiOn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbEbomKanshiOn.Click
            UpdEbomKanshi()

        End Sub
        ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 aw) (TES)施 ADDEND	

    End Class
End Namespace
