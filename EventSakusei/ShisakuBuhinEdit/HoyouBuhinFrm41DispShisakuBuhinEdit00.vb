Imports EventSakusei.ShisakuBuhinEdit.Kosei
Imports ShisakuCommon.Db
Imports EBom.Common
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEditBlock
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports EventSakusei.ShisakuBuhinEditEvent.Dao
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports EventSakusei.ShisakuBuhinEdit.Al.Dao
Imports EventSakusei.ShisakuBuhinEdit.Al
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports EventSakusei.ShisakuBuhinEdit.Selector
Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic

Namespace ShisakuBuhinEdit

    Public Class HoyouBuhinFrm41DispShisakuBuhinEdit00 : Implements Observer
        Private _UpdateFlag As Boolean = False

        Public WriteOnly Property UpdateFlag() As Boolean
            Set(ByVal value As Boolean)
                _UpdateFlag = value
            End Set
        End Property

        Private Sub HoyouBuhinFrm41DispShisakuBuhinEdit00_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

            ''-------------------------------------------
            ''２次改修
            ''ブロックのアラートを閉じる。
            'Dim frmKakunin As New frm41TourokuKakunin
            'frmKakunin.Close()
            ''-------------------------------------------

            If _dispMode = EDIT_MODE Then
                '---------------------------
                '---------------------------
                '２次改修
                '排他情報をクリア
                ExclusiveControl(_hoyouEventCode, _hoyouBukaCode, _hoyouTanto)
                '---------------------------
                '---------------------------
            End If
        End Sub


        ''↓↓2014/07/29 Ⅰ.3.設計編集 ベース車改修専用化_au) (TES)張 CHG BEGIN
        'Private _preForm As Frm38DispShisakuBuhinEditBlock
        ''↓↓2014/09/04 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
        'Private _preForm As Frm41DispShisakuBuhinEdit10
        Public _preForm As Frm41DispShisakuBuhinEdit10
        ''↑↑2014/09/04 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
        ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
        Public JikyuFlg As Boolean
        ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
        ''↑↑2014/07/29 Ⅰ.3.設計編集 ベース車改修専用化_au) (TES)張 CHG END
        Private Sub HoyouBuhinFrm41DispShisakuBuhinEdit00_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'ToolStripManager.Renderer = _
            '     New ToolStripProfessionalRenderer( _
            '     New EBom.Controls.CustomProfessionalRenderer())

            ''↓↓2014/07/29 Ⅰ.3.設計編集 ベース車改修専用化_bq) (TES)張 CHG BEGIN
            '最大化で表示する。
            'koseiWindow.WindowState = FormWindowState.Maximized
            'koseiWindow.Show()
            alWindow.Show()
            Me.LayoutMdi(MdiLayout.TileVertical)
            koseiWindow.Show()
            Me.LayoutMdi(MdiLayout.TileVertical)
            alWindow.Width = (Me.Width - 20) / 3
            koseiWindow.Width = (Me.Width - 20) / 3 * 2
            alWindow.Location = New Point(koseiWindow.Width, 0)
            ''↑↑2014/07/29 Ⅰ.3.設計編集 ベース車改修専用化_bq) (TES)張 CHG END

            ''部品表を開いた時点で一時レイアウト調整
            'Me.LayoutMdi(MdiLayout.TileVertical)

            'alWindow.Show()

            '--------------------------------------
            '２次改修
            '   以下の処理は廃止する。
            ''閲覧モード表示時は出力しない
            'If _dispMode = EDIT_MODE Then
            '    alWindow.EventChange()
            'End If
            '--------------------------------------

            ''レイアウト調整
            'Me.LayoutMdi(MdiLayout.TileVertical)

            '一時保存データの場合、以下のメッセージを表示する。
            If Not StringUtil.IsEmpty(headerSubject.Memo) Then
                Using frm As New Frm41IchijiHozonKakunin
                    frm.ConfirmView("一時保存データです。", headerSubject.Memo)
                End Using
            End If

        End Sub

        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBack.Click

            ''-------------------------------------------
            ''２次改修
            ''ブロックのアラートを閉じる。
            'frm41TourokuKakunin.Close()
            ''-------------------------------------------

            If _dispMode = EDIT_MODE Then
                If Not aInputWatcher.WasUpdate Then
                    ''---------------------------
                    ''---------------------------
                    ''２次改修
                    ''排他情報をクリア
                    'ExclusiveControl()
                    ''---------------------------
                    ''---------------------------
                    Me.Close()
                    Return
                End If
                If frm01Kakunin.ConfirmOkCancel("変更を更新せずに終了しますか？") = MsgBoxResult.Ok Then
                    ''---------------------------
                    ''---------------------------
                    ''２次改修
                    ''排他情報をクリア
                    'ExclusiveControl()
                    ''---------------------------
                    ''---------------------------
                    Me.Close()
                    Return
                End If
            Else
                Me.Close()
            End If

        End Sub



        ''' <summary>
        ''' 排他を解除
        ''' </summary>
        ''' <param name="EventCode">解除したいイベントコード</param>
        ''' <param name="BukaCode">解除したい部課コード</param>
        ''' <param name="Tanto">解除したい担当コード</param>
        '''          <remarks></remarks>
        ''' 
        Private Sub ExclusiveControl(ByVal EventCode As String, ByVal BukaCode As String, ByVal Tanto As String)



            Dim tantoVo As New THoyouExclusiveControlTantoVo
            Dim tantoDao As ExclusiveControlTantoDao = New ExclusiveControlTantoImpl

            'キー情報が無いとエラーになるので事前にチェック（イレギュラー以外あるはず）
            tantoVo = tantoDao.FindByPk(EventCode, BukaCode, Tanto)
            If IsNothing(tantoVo) Then
                'キー情報が無ければスルー。
            Else
                'KEY情報をセット
                tantoVo.HoyouEventCode = EventCode
                tantoVo.HoyouBukaCode = BukaCode
                tantoVo.HoyouTanto = Tanto
                tantoVo.EditUserId = LoginInfo.Now.UserId 'ログインユーザーIDをセット
                'KEY情報を削除
                tantoDao.DeleteByPk(tantoVo)
            End If

        End Sub

        Private Sub btnCOLOR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCOLOR.Click

            '2012/03/02 色を塗る行は場所によって異なるでこちら側で指定する'
            Dim cr As FarPoint.Win.Spread.Model.CellRange = GetActiveSheet.GetSelection(0)
            Dim row As Integer = 0
            'スプレッドが選択されていなければスルー。
            If StringUtil.IsNotEmpty(cr) Then
                If cr.Row < 1 Then
                    row = 2
                Else
                    row = cr.Row
                End If

                ''「色パレット」表示ボタンの近くに（真下の辺り）表示する。
                frmZColorMarker.ShowUnderButton(GetActiveSheet(), btnCOLOR)
                'frmZColorMarker.ShowUnderButton(GetActiveSheet(), row, btnCOLOR)
            End If
        End Sub


#Region " ウィンドウ "
        ''' <summary>
        ''' 重ねて表示
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub miWinCascade_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miWinCascade.Click
            Me.LayoutMdi(MdiLayout.Cascade)
        End Sub

        ''' <summary>
        ''' 左右に並べて表示
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub miWinTileHorizontal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miWinTileHorizontal.Click
            Me.LayoutMdi(MdiLayout.TileVertical)
        End Sub

        ''' <summary>
        ''' 上下に並べて表示
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub miWinTileVertical_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miWinTileVertical.Click
            Me.LayoutMdi(MdiLayout.TileHorizontal)
        End Sub
#End Region

        Private Sub btnColorCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColorCLEAR.Click

            koseiWindow.ClearSheetBackColor()

        End Sub
#Region "２次改修・試作イベント情報のステータスをチェック。不正の場合処理中断"

        Public Function StatusCheck(ByVal strStatusMode As String) As Boolean

            '------------------------------------------------------------------------------------------------------
            '初期設定
            Dim tHoyouEventDaoImpl As HoyouEventDao = New EventSakusei.ShisakuBuhinEditBlock.Dao.HoyouEventDaoImpl
            Dim tHoyouEventVo As New THoyouEventVo()
            '------------------------------------------------------------------------------------------------------

            '補用イベント情報からステータスを取得。
            tHoyouEventVo = tHoyouEventDaoImpl.FindByPk(_hoyouEventCode)

            If IsNothing(tHoyouEventVo) Then
                MsgBox("補用イベント情報が存在しません。")
                Return True
            Else
                Select Case strStatusMode
                    Case HoyouHensyuMode
                        If tHoyouEventVo.Status = "21" Then
                            'MsgBox("編集モードです。試作イベント情報のステータスは・・・ＯＫ。")
                            Return True
                        Else
                            MsgBox("イベントのステータスが変更されました。メニュー画面に戻ります。")
                            Return False
                        End If
                    Case HoyouKaiteiHensyuMode
                        If tHoyouEventVo.Status = "23" Then
                            'MsgBox("改訂編集モードです。試作イベント情報のステータスは・・・ＯＫ。")
                            Return True
                        Else
                            MsgBox("イベントのステータスが変更されました。メニュー画面に戻ります。")
                            Return False
                        End If
                    Case Else
                        MsgBox("完了閲覧モードです。")
                        Return True
                End Select
            End If

        End Function

#End Region

        Private Sub BtnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCall.Click
            Dim tantoNo As String = cmbTanto.Text

            '----------------------------------------------------------
            '２次改修
            '   試作イベント情報のステータスが正しいか確認する。
            '   ＮＧの場合、処理終了。
            '   編集モード：STATUS＝２１（設計メンテ中）
            '   改訂編集モード：STATUS＝２３（改訂受付中）
            '   完了閲覧モード：STATUS＝２４（完了）
            Dim flgStatusCheck As Boolean = StatusCheck(Me._strMode)
            If flgStatusCheck = False Then
                '以下のパラメータの場合、親画面も画面を閉じる。
                frm37ParaModori = "close"
                Me.Close()
                Exit Sub
            End If
            '----------------------------------------------------------

            If _dispMode = EDIT_MODE Then

                Try
                    errorController.ClearBackColor()

                    validatorTantoCall.AssertValidate()
                Catch ex As IllegalInputException
                    errorController.SetBackColorOnError(ex.ErrorControls)
                    errorController.FocusAtFirstControl(ex.ErrorControls)
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox(ex.Message)
                    cmbTanto.Text = Me._hoyouTanto
                    Return
                End Try

                If aInputWatcher.WasUpdate Then
                    If frm01Kakunin.ConfirmOkCancel("登録されていません。", "変更を破棄し別の担当者情報を呼出しますか？") <> MsgBoxResult.Ok Then
                        cmbTanto.Text = Me._hoyouTanto
                        Return
                    End If
                End If

            End If

            '担当者呼び出し前にチェックする'
            'Dim impl As Al.Dao.HoyouBuhinBuhinEditAlDao = New Al.Dao.HoyouBuhinBuhinEditAlDaoImpl
            'Dim sVo As New THoyouSekkeiTantoVo
            'sVo = impl.FindByNewTanto(_hoyouEventCode, _hoyouBukaCode, cmbTanto.Text)

            'If Not _preForm.ShowFrm41ShisakuBuhinEditFromFrm41(cmbTanto.Text) Then
            '    cmbTanto.Text = Me._hoyouTanto
            '    Return
            'End If

            '12/6/26　SKE1森様と打合せし、イベントコード編集中でも部品表の編集は可能とする様に決定。
            '           仕様変更があった場合、以下のコメントを解除する。
            ''------------------------------------------------------------------------------------------------------
            ''最初に試作イベントコードが編集中かチェックする。
            ''------------------------------------------------------------------------------------------------------
            ''２次改修

            '------------------------------------------------------------------------------------------------------
            '初期設定
            '他のユーザーが編集中か確認する。
            Dim tExclusiveControlEventDaoImpl As ExclusiveControlEventDao = New ExclusiveControlEventImpl
            Dim tExclusiveControlEventVo As New TExclusiveControlEventVo()
            Dim isExclusive As Boolean  'true=編集OK、false=編集NG
            Dim isReg As Boolean  'true=追加、false=更新
            Dim tanTousya As String = Nothing
            '担当者名取得用に。
            Dim getDate As New EditBlock2ExcelDaoImpl()
            '------------------------------------------------------------------------------------------------------

            '------------------------------------------------------------------------------------------------------
            '------------------------------------------------------------------------------------------------------
            '次に試作イベントコード、設計課、ブロックの部品表が編集中かチェックする。
            '新しく排他制御情報を作成する。
            '------------------------------------------------------------------------------------------------------
            '------------------------------------------------------------------------------------------------------
            '２次改修
            '担当者呼び出し前にチェックする'
            Dim impl As Al.Dao.HoyouBuhinBuhinEditAlDao = New Al.Dao.HoyouBuhinBuhinEditAlDaoImpl
            Dim sVo As New THoyouSekkeiTantoVo
            sVo = impl.FindByNewTanto(_hoyouEventCode, "", tantoNo) '部課が変更される場合があるので
            Dim hoyouBukaCode As String = sVo.HoyouBukaCode
            Dim HoyouTantoKaiteiNo As String = sVo.HoyouTantoKaiteiNo

            '他のユーザーが編集中か確認する。
            Dim tExclusiveControlTantoDaoImpl As ExclusiveControlTantoDao = New ExclusiveControlTantoImpl
            Dim tExclusiveControlTantoVo As New THoyouExclusiveControlTantoVo()

            '排他制御ブロック情報から試作イベントコード、部課コード、ブロック№が登録されているかチェック。
            tExclusiveControlTantoVo = tExclusiveControlTantoDaoImpl.FindByPk(_hoyouEventCode, _
                                                                              hoyouBukaCode, _
                                                                              tantoNo)

            If IsNothing(tExclusiveControlTantoVo) Then
                'MsgBox("選択したブロックは誰も使用していません。")
                isExclusive = True '編集OK
                isReg = True '追加モード
            Else
                isReg = False '更新モード
                'レコードが有る場合、ログインユーザーと編集者コードを比較する。
                '担当者名を取得する'
                tanTousya = getDate.FindByShainName(tExclusiveControlTantoVo.EditUserId)
                '同じなら編集OK。
                If tExclusiveControlTantoVo.EditUserId = LoginInfo.Now.UserId Then
                    isExclusive = True '編集OK
                Else
                    '違うなら編集NG。
                    isExclusive = False '編集NG
                End If
            End If

            '排他チェック
            If isExclusive = True Then
                '変更確認メッセージ
                'If Not _preForm.SetMsgBox(_hoyouEventCode, _
                '                          hoyouBukaCode, _
                '                          tantoNo, _
                '                          HoyouTantoKaiteiNo, _
                '                          1) Then
                '    'If Not SetMsgBox(Me.cmbBuka.Text, Me.txtTanto.Text, 1) Then
                '    Return
                'End If

                '編集担当者が変更されたのでひとつ前の編集中の排他制御をクリアする。
                '但し、編集中の担当者情報が指定されている場合にはクリア処理はスルーする。
                If cmbTanto.Text <> _hoyouTanto Then
                    '---------------------------
                    '---------------------------
                    '２次改修
                    '排他情報をクリア
                    ExclusiveControl(_hoyouEventCode, _hoyouBukaCode, _hoyouTanto)

                    '---------------------------
                    '---------------------------
                End If


                '------------------------------------------------
                '------------------------------------------------
                '他のユーザーが編集していなければ排他制御ブロックテーブルにレコード更新。
                'RegisterMain(_hoyouEventCode, _hoyouBukaCode, tantoNo, isReg)
                RegisterMain(_hoyouEventCode, hoyouBukaCode, tantoNo, isReg)

                '続けて処理を続行。
                '------------------------------------------------
                '------------------------------------------------

            Else
                '他のユーザーが編集していれば警告を表示して終了。
                MsgBox("選択した担当者情報は編集中です。" & vbCrLf & vbCrLf & _
                        "担当者「" & tExclusiveControlTantoVo.EditUserId & _
                                 "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")

                '元の担当者を設定して処理終了。
                cmbTanto.Text = _hoyouTanto
                Return

            End If

            '---------------------------------------------
            '変更後の担当を保持する。
            _hoyouBukaCode = hoyouBukaCode
            _hoyouTanto = cmbTanto.Text
            '---------------------------------------------

            '------------------------------------------------------------------------------------------------------
            '------------------------------------------------------------------------------------------------------



            ''-------------------------------------------
            ''２次改修
            ''ブロックのアラートを閉じる。
            'frm41TourokuKakunin.Close()
            ''-------------------------------------------






            Me._dispMode = EDIT_MODE

            'alWindow.Close()
            'alWindow.Dispose()

            koseiWindow.Close()
            koseiWindow.Dispose()
            '---------------------------------------------
            'ここで一度画面を破棄します。
            '---------------------------------------------

            ''↓↓2014/07/29 Ⅰ.3.設計編集 ベース車改修専用化_av) (TES)張 CHG BEGIN
            'subject = New Logic.HoyouBuhinBuhinEditSubject(_hoyouEventCode, _hoyouBukaCode, tantoNo, LoginInfo.Now, KSMode)
            subject = New Logic.HoyouBuhinBuhinEditSubject(_hoyouEventCode, _hoyouBukaCode, tantoNo, LoginInfo.Now, KSMode, _hoyouVos, _hoyouInstlVos, alSubject)
            ''↑↑2014/07/29 Ⅰ.3.設計編集 ベース車改修専用化_av) (TES)張 CHG END
            headerSubject = subject.HeaderSubject
            subject.AddObserver(Me)
            headerSubject.AddObserver(Me)

            'alWindow = New Frm41DispShisakuBuhinEdit10(_shisakuEventCode, subject.AlSubject, _dispMode)
            'alWindow.MdiParent = Me

            koseiWindow = New HoyouBuhinFrm41DispShisakuBuhinEdit20(_hoyouEventCode, _hoyouBukaCode, _hoyouTanto, subject.KoseiSubject, _dispMode, Me, _UpdateFlag)
            koseiWindow.MdiParent = Me
            koseiWindow.WindowState = FormWindowState.Maximized

            aInputWatcher = New InputWatcher

            InitializeWatcher()
            InitializeHeader()
            InitializeValidator()


            'alWindow = New Frm41DispShisakuBuhinEdit10(_shisakuEventCode, subject.AlSubject)
            'alWindow.MdiParent = Me

            'koseiWindow = New HoyouBuhinFrm41DispShisakuBuhinEdit20(_shisakuEventCode, subject.KoseiSubject)
            'koseiWindow.MdiParent = Me


            BtnRegister.Enabled = True
            BtnSave.Enabled = True
            TxtTel.Enabled = True

            txtNaiyou.Enabled = True

            lblDispMode.Visible = False
            lblDispOnly.Visible = False

            'alWindow.ClearInstlColumns()

            koseiWindow.Show()
            'Me.LayoutMdi(MdiLayout.TileVertical)

            'alWindow.Show()
            'Me.LayoutMdi(MdiLayout.TileVertical)

            'subject.ApplyBlockNo()
            subject.NotifyObservers()

            'cmbBlockNo.Text = blockNo

            aInputWatcher.Clear()
            '文字色、大きさもデフォルトに戻す。
            'alWindow.ClearSheetBackColorAll()
            koseiWindow.ClearSheetBackColorAll()

            'イベント更新チェック
            'alWindow.EventChange()
            '一時保存データの場合、以下のメッセージを表示する。
            If Not StringUtil.IsEmpty(headerSubject.Memo) Then
                Using frm As New Frm41IchijiHozonKakunin
                    frm.ConfirmView("一時保存データです。", headerSubject.Memo)
                End Using
            End If

            '画面表示時の日付時刻を保持しておく
            Dim alDao As New HoyouBuhinBuhinEditAlDaoImpl
            Dim tempVo As New THoyouSekkeiTantoVo
            tempVo = alDao.FindByDBTimeStamp(_hoyouEventCode)
            _OpenDateTime = tempVo.UpdatedDate & tempVo.UpdatedTime

            aInputWatcher.Clear()

            '----------------------------------------
            '２次改修
            'ブロックのアラートを表示する。
            DispAlert()
            '----------------------------------------

        End Sub

        Private ReadOnly _hoyouEventCode As String
        '----------------------------------------------------
        '----------------------------------------------------
        '２次改修
        '担当者が変更と「部課」も変更される場合があるのでReadonlyを外す。
        'Private ReadOnly _hoyouBukaCode As String
        Private _hoyouBukaCode As String
        '----------------------------------------------------
        '----------------------------------------------------
        '２次改修
        '担当者は変更されることがあるのでReadonlyを外す。
        'Private ReadOnly _shisakuBlockNo As String
        Private _hoyouTanto As String
        '----------------------------------------------------
        '----------------------------------------------------


        Private _dispMode As Integer

        Private subject As Logic.HoyouBuhinBuhinEditSubject
        Private headerSubject As Logic.HoyouBuhinBuhinEditHeaderSubject

        Private koseiWindow As HoyouBuhinFrm41DispShisakuBuhinEdit20
        Private alWindow As Frm41DispShisakuBuhinEdit10

        Private aInputWatcher As InputWatcher

        Private IsSuspendEventValueChanged As Boolean

        Private validatorTantoCall As Validator

        Private KSMode As Integer
        Private _OpenDateTime As String

        Private _strMode As String   '*****完了閲覧モード対応*****

        Private _hoyouVos As List(Of THoyouBuhinEditVo)
        Private _hoyouInstlVos As List(Of TShisakuBuhinEditInstlVo)

        Private alSubject As BuhinEditAlSubject

        Public Sub New(ByVal frm As Frm41DispShisakuBuhinEdit10, ByVal hoyouEventCode As String, _
                       ByVal hoyouBukaCode As String, ByVal hoyouTanto As String, _
                       ByVal dispMode As Integer, ByVal KSMode As Integer, ByVal strMode As String, _
                       ByVal hoyouVos As List(Of THoyouBuhinEditVo), _
                       ByVal hoyouInstlVos As List(Of TShisakuBuhinEditInstlVo), _
                       ByVal alSubject As BuhinEditAlSubject, _
                       ByVal koseiSubject As BuhinEditKoseiSubject, _
                       ByVal updateFlag As Boolean)
            JikyuFlg = frm.JikyuFlg
            InitializeComponent()

            ShisakuFormUtil.Initialize(Me)

            Me._hoyouEventCode = hoyouEventCode
            Me._hoyouBukaCode = hoyouBukaCode
            Me._hoyouTanto = hoyouTanto
            Me._dispMode = dispMode
            Me._preForm = frm
            Me.KSMode = KSMode
            Me._UpdateFlag = updateFlag


            Me._strMode = strMode   '*****完了閲覧モード対応*****
            Me._hoyouVos = hoyouVos
            Me._hoyouInstlVos = hoyouInstlVos
            Me.alSubject = alSubject
            subject = New Logic.HoyouBuhinBuhinEditSubject(hoyouEventCode, hoyouBukaCode, hoyouTanto, LoginInfo.Now, KSMode, hoyouVos, hoyouInstlVos, alSubject)
            headerSubject = subject.HeaderSubject
            subject.AddObserver(Me)
            headerSubject.AddObserver(Me)

            koseiWindow = New HoyouBuhinFrm41DispShisakuBuhinEdit20(hoyouEventCode, _hoyouBukaCode, _hoyouTanto, subject.KoseiSubject, _dispMode, Me, _UpdateFlag)
            koseiWindow.MdiParent = Me

            alSubject.setSpdChanged()
            alWindow = New Frm41DispShisakuBuhinEdit10(hoyouEventCode, alSubject, koseiSubject, _dispMode, "HoyouBuhinFrm41DispShisakuBuhinEdit00")
            alWindow.MdiParent = Me

            aInputWatcher = New InputWatcher

            InitializeWatcher()
            InitializeHeader()
            InitializeValidator()

            '画面表示時の日付時刻を保持しておく
            Dim alDao As New HoyouBuhinBuhinEditAlDaoImpl
            Dim tempVo As New THoyouSekkeiTantoVo
            tempVo = alDao.FindByDBTimeStamp(hoyouEventCode)
            If tempVo Is Nothing Then
                _OpenDateTime = ""
            Else
                If Not StringUtil.IsEmpty(tempVo.UpdatedDate) AndAlso Not StringUtil.IsEmpty(tempVo.UpdatedTime) Then
                    _OpenDateTime = tempVo.UpdatedDate & tempVo.UpdatedTime
                Else
                    _OpenDateTime = ""
                End If
            End If

            subject.NotifyObservers()
            If _dispMode = VIEW_MODE Then
                koseiWindow.viewLockForViewMode()   '----追加
            End If

            aInputWatcher.Clear()

        End Sub

        Private Sub InitializeWatcher()

            aInputWatcher.Add(TxtTel)
            aInputWatcher.Add(txtNaiyou)
            aInputWatcher.Add(koseiWindow.spdParts)
        End Sub

        Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            If observable Is headerSubject Then
                UpdateHeader(observable, arg)
            Else
                headerSubject.NotifyObservers(arg)
                subject.AlSubject.NotifyObservers(arg)
                subject.KoseiSubject.NotifyObservers(arg)
            End If
        End Sub

        ''' <summary>
        ''' ヘッダー部の表示値を更新する
        ''' </summary>
        ''' <param name="observable">呼び出し元Observable</param>
        ''' <param name="arg">引数</param>
        ''' <remarks></remarks>
        Private Sub UpdateHeader(ByVal observable As Observable, ByVal arg As Object)
            IsSuspendEventValueChanged = True
            Try
                LblEvent.Text = String.Format("{0}  {1}", headerSubject.HoyouKaihatsuFugo, headerSubject.HoyouEventName)
                If Not StringUtil.IsEmpty(headerSubject.HoyouBukaName) Then
                    LblBukaName.Text = headerSubject.HoyouBukaName
                Else
                    LblBukaName.Text = LoginInfo.Now.BukaRyakuName
                End If

                If Not StringUtil.IsEmpty(headerSubject.UserName) Then
                    LblUserName.Text = headerSubject.UserName
                Else
                    LblUserName.Text = LoginInfo.Now.ShainName
                End If

                If Not StringUtil.IsEmpty(headerSubject.TelNo) Then
                    TxtTel.Text = headerSubject.TelNo
                Else
                    TxtTel.Text = ""
                End If

                '-----------------------------------------------------
                '２次改修で追加
                If headerSubject.KaiteiNo = "000" Then
                    '改訂№が000（イニシャル時は）非表示
                    lblNaiyou.Visible = False  '改訂№
                    lblNaiyou1.Visible = False
                    lblNaiyou2.Visible = False
                    txtNaiyou.Visible = False
                Else
                    '改訂№が000（イニシャル時は）以上の場合は表示
                    lblNaiyou.Visible = True  '改訂№
                    lblNaiyou1.Visible = True
                    lblNaiyou2.Visible = True
                    txtNaiyou.Visible = True
                    '   改訂内容をセット
                    If Not StringUtil.IsEmpty(headerSubject.Naiyou) Then
                        txtNaiyou.Text = headerSubject.Naiyou
                    Else
                        txtNaiyou.Text = ""
                    End If
                    '   改訂№ををセット
                    If Not StringUtil.IsEmpty(headerSubject.KaiteiNo) Then
                        lblNaiyou.Text = headerSubject.KaiteiNo
                    Else
                        lblNaiyou.Text = ""
                    End If
                End If
                '-----------------------------------------------------

                FormUtil.BindLabelValuesToComboBox(cmbTanto, headerSubject.TantoNoLabelValues)
                FormUtil.SetComboBoxSelectedValue(cmbTanto, headerSubject.HoyouTantoNo)
            Finally
                IsSuspendEventValueChanged = False
            End Try
        End Sub

        ''' <summary>
        ''' ヘッダー部を初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeHeader()

            If _dispMode = VIEW_MODE Then
                BtnRegister.Enabled = False
                BtnSave.Enabled = False
                TxtTel.Enabled = False

                txtNaiyou.Enabled = False '２次改修で追加
                btnExcelExport.Visible = False

                lblDispMode.Visible = True
                lblDispOnly.Visible = False
                If _strMode = HoyouKanryoViewMode Then    '*****完了閲覧モード対応*****
                    BtnCall.Visible = False
                End If

            ElseIf _dispMode = EDIT_MODE Then
                BtnRegister.Enabled = True
                BtnSave.Enabled = True
                TxtTel.Enabled = True

                txtNaiyou.Enabled = True '２次改修で追加
                btnExcelExport.Visible = False

                lblDispMode.Visible = False
                lblDispOnly.Visible = False

            ElseIf _dispMode = EXCEL_MODE Then   ''Excel出力専用モード
                lblDispMode.Visible = False
                lblDispOnly.Visible = True

                btnExcelExport.Visible = True

                LblEvent.Visible = False
                LblBuka.Visible = False
                LblBukaName.Visible = False
                LblTanto.Visible = False
                LblUserName.Visible = False
                LblTel.Visible = False
                TxtTel.Visible = False

                lblBuhinNo.Visible = False
                cmbTanto.Visible = False
                BtnCall.Visible = False
                btnView.Visible = False

                lblNaiyou1.Visible = False
                lblNaiyou.Visible = False
                lblNaiyou2.Visible = False
                txtNaiyou.Visible = False

                'btnColorCLEAR.Visible = False
                'btnCOLOR.Visible = False
                BtnRegister.Visible = False
                BtnSave.Visible = False

            End If
            ShisakuFormUtil.setTitleVersion(Me)

            If Not _dispMode = EXCEL_MODE Then   ''Excel出力専用モード　以外
                ShisakuFormUtil.SettingDefaultProperty(cmbTanto)
                cmbTanto.MaxLength = 8
            End If
        End Sub

        Private Sub InitializeValidator()
            validatorTantoCall = New Validator
            validatorTantoCall.Add(cmbTanto, "担当者").Required()
        End Sub

        Private Sub cmbBlockNo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTanto.SelectedValueChanged
            If headerSubject Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            headerSubject.HoyouTantoNoSelectedValue = cmbTanto.SelectedValue
            headerSubject.NotifyObservers()
        End Sub

        'TELの入力規制'
        Private Sub txtTel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTel.KeyPress
            If TelNo_validate(e.KeyChar) Then
                e.Handled = True
            End If
        End Sub

        'TELの入力イベント'
        Private Sub txtTel_Changed() Handles TxtTel.TextChanged
            If Not headerSubject Is Nothing Then
                headerSubject.TelNo = TxtTel.Text
            End If
        End Sub

        '２次改修で追加
        '   内容の入力イベント'
        Private Sub txtNaiyou_Changed() Handles txtNaiyou.TextChanged
            If Not headerSubject Is Nothing Then
                headerSubject.Naiyou = txtNaiyou.Text
            End If
        End Sub

        Private _WasSaveRegister As Boolean
        ''' <summary>
        ''' 保存、または、登録をしたかを返す
        ''' </summary>
        ''' <returns>保存・登録をした場合、true</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WasSaveRegister() As Boolean
            Get
                Return _WasSaveRegister
            End Get
        End Property

        Private errorController As New ErrorController()
        ''↓↓2014/08/07 Ⅰ.3.設計編集 ベース車改修専用化_bd) (TES)張 ADD BEGIN
        Private _Register As Boolean = False
        Public ReadOnly Property Register() As Boolean
            Get
                Return _Register
            End Get
        End Property
        ''↑↑2014/08/07 Ⅰ.3.設計編集 ベース車改修専用化_bd) (TES)張 ADD END

        Private Sub BtnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRegister.Click
            ''↓↓2014/08/07 Ⅰ.3.設計編集 ベース車改修専用化_bd) (TES)張 ADD BEGIN
            ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bd) 酒井 DEL BEGIN
            'Me._Register = True
            ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bd) 酒井 DEL END
            ''↑↑2014/08/07 Ⅰ.3.設計編集 ベース車改修専用化_bd) (TES)張 ADD END
            ''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_be) (TES)張 ADD BEGIN
            Try
                errorController.ClearBackColor()

                koseiWindow.AssertValidateLevel()

            Catch ex As IllegalInputException

                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)

                koseiWindow.EControl = errorController

                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)

                Return
            End Try
            ''↑↑2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_be) (TES)張 ADD END

            Try
                errorController.ClearBackColor()
                'alWindow.AssertValidateRegister()
                koseiWindow.AssertValidateRegister()

            Catch ex As IllegalInputException

                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)

                koseiWindow.EControl = errorController

                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)

                '備考のエラーメッセージ
                'TODO 妥当な置き場所を探す
                'ComFunc.ShowErrMsgBox(EK001)

                Return
            End Try



            '構成は別で用意'
            Dim msgBoxResult As MsgBoxResult = Nothing
            'Try

            '    koseiWindow.AssertValidateKoseiRegister(_shisakuEventCode, _shisakuBukaCode, _shisakuBlockNo)

            'Catch ex As IllegalInputException
            '    errorController.SetBackColorOnError(ex.ErrorControls)
            '    errorController.FocusAtFirstControl(ex.ErrorControls)

            '    'MsgBox("E-BOM登録情報と構成がアンマッチです。" + vbCr + "INSTL品番を変更してください。", MsgBoxStyle.Information, "構成エラー")

            '    Return
            '    'errorController.ClearBackColor()
            'End Try

            '構成ワーニング'
            msgBoxResult = Nothing
            '2012/02/25 要望により、ワーニングも一時封印'
            'Try
            '    'INSTL品番表示順を使用してのValidaterのため列の挿入で判定できなくなるかも？'
            '    koseiWindow.AssertValidateKoseiWarningRegster(_shisakuEventCode, _shisakuBukaCode, _shisakuBlockNo)

            'Catch ex As IllegalInputException
            '    errorController.SetBackColorOnWarning(ex.ErrorControls)
            '    errorController.FocusAtFirstControl(ex.ErrorControls)

            '    If frm01Kakunin.ConfirmOkCancel("黄色部分の部品構成が変更されています。", "このまま登録しますか？") <> msgBoxResult.Ok Then
            '        Return
            '    End If

            'End Try

            ''取引先コード
            'Try
            '    koseiWindow.AssertValidateMakerCode()

            'Catch ex As IllegalInputException
            '    errorController.SetBackColorOnError(ex.ErrorControls)
            '    errorController.FocusAtFirstControl(ex.ErrorControls)
            '    If frm01Kakunin.ConfirmOkCancel("取引先コードが入力されていません", "このまま登録しますか？") <> msgBoxResult.Ok Then
            '        Return
            '    End If
            'End Try

            Try
                koseiWindow.AssertValidateKyoukuSection()

            Catch ex As IllegalInputException

                errorController.SetBackColorOnWarning(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                If frm01Kakunin.ConfirmOkCancel("供給セクションが入力されていません", "このまま登録しますか？") <> msgBoxResult.Ok Then
                    Return
                End If
            End Try

            Try
                koseiWindow.AssertValidateInsuSection()

            Catch ex As IllegalInputException
                errorController.SetBackColorOnWarning(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                If frm01Kakunin.ConfirmOkCancel("員数が１００を超えています", "このまま登録しますか？") <> msgBoxResult.Ok Then
                    Return
                End If
            End Try

            'Try
            '    koseiWindow.AssertValidateRegisterWarning()
            'Catch ex As IllegalInputException
            '    errorController.SetBackColorOnError(ex.ErrorControls)
            '    errorController.FocusAtFirstControl(ex.ErrorControls)
            '    koseiWindow.EControl = errorController
            '    ''確認メッセージ
            '    If ComFunc.ShowInfoMsgBox(ex.Message, MessageBoxButtons.YesNo) <> MsgBoxResult.Yes Then
            '        Return
            '    End If
            '    errorController.ClearBackColor()
            'End Try

            If frm01Kakunin.ConfirmOkCancel("反映を実行しますか？") <> msgBoxResult.Ok Then
                Return
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor


            '---------------------------------------------------------------------------------------------------------
            '---------------------------------------------------------------------------------------------------------
            '２次改修
            '排他処理見直しのため、以下の処理は無効にする。

            ''ここで同期チェック
            ''画面表示時の日付時刻を保持しておく
            'Dim alDao As New HoyouBuhinBuhinEditAlDaoImpl
            'Dim tempVo As New TShisakuSekkeiBlockVo

            'tempVo = alDao.FindByLastModifyDateTimeOfSekkeiBlock(_shisakuEventCode, _shisakuBukaCode, _shisakuBlockNo, _OpenDateTime)
            'If Not tempVo Is Nothing Then
            '    If Not StringUtil.IsEmpty(tempVo.UpdatedDate) Then
            '        ComFunc.ShowErrMsgBox("データが更新されました。再度画面を開きなおし編集を行って下さい。")
            '        Return
            '    End If
            'End If
            '---------------------------------------------------------------------------------------------------------
            '---------------------------------------------------------------------------------------------------------

            '↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bi) (TES)張 DEL BEGIN
            Try
                'subject.Register()

                ''---------------------------
                ''---------------------------
                ''２次改修
                ''排他情報をクリア
                'ExclusiveControl()
                ''---------------------------
                ''---------------------------

                ''-------------------------------------------
                ''２次改修
                ''ブロックのアラートを閉じる。
                'frm41TourokuKakunin.Close()
                ''-------------------------------------------

                ''↓↓2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bd) 酒井 ADD BEGIN
                Me._Register = True
                ''↑↑2014/08/29 Ⅰ.3.設計編集 ベース車改修専用化_bd) 酒井 ADD END
                Me.Close()
            Catch ex As TableExclusionException
                ComFunc.ShowInfoMsgBox(ex.Message)
            Finally
                _WasSaveRegister = True ' 排他例外だったとしても、誰かが更新したから
            End Try
            '↑↑2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bi) (TES)張 DEL END

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub

        Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
            Try
                errorController.ClearBackColor()

                'alWindow.AssertValidateSave()
                koseiWindow.AssertValidateSave()
            Catch ex As IllegalInputException
                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)
                Return
            End Try

            Using frm As New Frm41IchijiHozonKakunin
                frm.ConfirmOkCancel("一時保存しますか？")
                If Not frm.ResultOk Then
                    Return
                End If

                Try
                    ShisakuCommon.Ui.CursorUtil.SetWaitCursor(1000)
                    subject.Save(frm.Result)

                    aInputWatcher.Clear()
                Catch ex As TableExclusionException
                    ComFunc.ShowInfoMsgBox(ex.Message)
                Finally
                    _WasSaveRegister = True ' 排他例外だったとしても、誰かが更新したから
                End Try

            End Using
        End Sub

        ''' <summary>
        ''' TelNoに全角文字が入力されているかを返す
        ''' </summary>
        ''' <param name="telNo">telNo</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function TelNo_validate(ByVal telNo As Char) As Boolean
            Dim moji As Integer = Len(telNo)
            Dim bytecount As Integer = System.Text.Encoding.GetEncoding("Shift-JIS").GetByteCount(telNo.ToString())
            If moji <> bytecount Then
                Return False
            End If
            Return True
        End Function

        ''' <summary>
        ''' 現在表示されているSpreadのSheetを返す
        ''' </summary>
        ''' <returns>表示されているSheet</returns>
        ''' <remarks></remarks>
        Private Function GetActiveSheet() As FarPoint.Win.Spread.SheetView
            Return koseiWindow.spdParts_Sheet1
        End Function

        Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
            Dim tantoNo As String = cmbTanto.Text

            '   
            '   前回値をキープしないと、閲覧モードに遷移する前の担当者で排他できない
            '
            Dim HoyouTanto_Zenkai As String = _hoyouTanto
            Dim HoyouBukaCodeZenkai As String = _hoyouBukaCode

            If _dispMode = EDIT_MODE Then

                Try
                    errorController.ClearBackColor()

                    validatorTantoCall.AssertValidate()
                Catch ex As IllegalInputException
                    errorController.SetBackColorOnError(ex.ErrorControls)
                    errorController.FocusAtFirstControl(ex.ErrorControls)
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox(ex.Message)
                    cmbTanto.Text = Me._hoyouTanto
                    Return
                End Try

                If aInputWatcher.WasUpdate Then
                    If frm01Kakunin.ConfirmOkCancel("登録されていません。", "変更を破棄し別の担当者情報を呼出しますか？") <> MsgBoxResult.Ok Then
                        cmbTanto.Text = Me._hoyouTanto
                        Return
                    End If
                End If


                ''ブロック呼び出し前にチェックする'
                'Dim impl As Al.Dao.HoyouBuhinBuhinEditAlDao = New Al.Dao.HoyouBuhinBuhinEditAlDaoImpl
                'Dim sVo As New TShisakuSekkeiBlockVo
                'sVo = impl.FindByNewBlockNo(_shisakuEventCode, _shisakuBukaCode, cmbBlockNo.Text)

                'If Not sVo Is Nothing Then
                '    If StringUtil.Equals(sVo.KachouSyouninJyoutai, "36") Then
                '        If frm01Kakunin.ConfirmOkCancel("該当ブロックはNo.は承認されています。", "再編集しますか？") <> MsgBoxResult.Ok Then
                '            Return
                '        End If
                '    Else
                '        If StringUtil.Equals(sVo.TantoSyouninJyoutai, "35") Then
                '            If frm01Kakunin.ConfirmOkCancel("該当ブロックはNo.は承認されています。", "再編集しますか？") <> MsgBoxResult.Ok Then
                '                Return
                '            End If
                '        Else
                '            If StringUtil.Equals(sVo.Jyoutai, "34") Then
                '                If frm01Kakunin.ConfirmOkCancel("該当ブロックはNo.は完了しています。", "再編集しますか？") <> MsgBoxResult.Ok Then
                '                    Return
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
            End If

            Me._dispMode = VIEW_MODE

            'alWindow.Close()
            'alWindow.Dispose()
            subject = Nothing

            koseiWindow.Close()
            koseiWindow.Dispose()

            '---------------------------------------------
            'ここで一度画面を破棄します。
            '---------------------------------------------
            '２次改修
            '担当者呼び出し前にチェックする'
            Dim impl As Al.Dao.HoyouBuhinBuhinEditAlDao = New Al.Dao.HoyouBuhinBuhinEditAlDaoImpl
            Dim sVo As New THoyouSekkeiTantoVo
            sVo = impl.FindByNewTanto(_hoyouEventCode, "", tantoNo) '部課が変更される場合があるので
            Dim hoyouBukaCode As String = sVo.HoyouBukaCode
            'Dim HoyouTantoKaiteiNo As String = sVo.HoyouTantoKaiteiNo
            '---------------------------------------------
            '変更後の担当を保持する。
            _hoyouBukaCode = hoyouBukaCode
            _hoyouTanto = cmbTanto.Text
            '---------------------------------------------

            'subject = New Logic.BuhinEditSubject(_hoyouEventCode, _hoyouBukaCode, tantoNo, LoginInfo.Now, KSMode)
            'subject = New Logic.BuhinEditSubject(_hoyouEventCode, _hoyouBukaCode, _hoyouTanto, LoginInfo.Now, KSMode)
            'headerSubject = subject.HeaderSubject
            'subject.AddObserver(Me)
            'headerSubject.AddObserver(Me)

            'alWindow = New Frm41DispShisakuBuhinEdit10(_shisakuEventCode, subject.AlSubject, _dispMode)
            'alWindow.MdiParent = Me

            koseiWindow = New HoyouBuhinFrm41DispShisakuBuhinEdit20(_hoyouEventCode, _hoyouBukaCode, _hoyouTanto, subject.KoseiSubject, _dispMode, Me, _UpdateFlag)
            koseiWindow.MdiParent = Me
            koseiWindow.WindowState = FormWindowState.Maximized

            aInputWatcher = New InputWatcher

            InitializeWatcher()
            InitializeHeader()
            InitializeValidator()

            'alWindow = New Frm41DispShisakuBuhinEdit10(_shisakuEventCode, subject.AlSubject)
            'alWindow.MdiParent = Me

            'koseiWindow = New HoyouBuhinFrm41DispShisakuBuhinEdit20(_shisakuEventCode, subject.KoseiSubject)
            'koseiWindow.MdiParent = Me

            BtnRegister.Enabled = False
            BtnSave.Enabled = False
            TxtTel.Enabled = False

            txtNaiyou.Enabled = False '２次改修で追加

            lblDispMode.Visible = True
            lblDispOnly.Visible = False

            'subject.ApplyBlockNo()
            subject.NotifyObservers()

            'cmbBlockNo.Text = blockNo

            aInputWatcher.Clear()
            '文字色、大きさもデフォルトに戻す。
            'alWindow.ClearSheetBackColorAll()
            koseiWindow.ClearSheetBackColorAll()


            'alWindow.viewLockForViewMode()

            koseiWindow.viewLockForViewMode()

            koseiWindow.Show()
            'Me.LayoutMdi(MdiLayout.TileVertical)

            'alWindow.Show()
            'Me.LayoutMdi(MdiLayout.TileVertical)

            aInputWatcher.Clear()


            '---------------------------
            '---------------------------
            '編集モードから閲覧モードに変更になったら排他情報をクリアする。
            '---------------------------
            '---------------------------
            '２次改修
            '排他情報をクリア
            ExclusiveControl(_hoyouEventCode, HoyouBukaCodeZenkai, HoyouTanto_Zenkai)
            '---------------------------
            '---------------------------

            ''---------------------------------------------
            ''変更後の担当を保持する。
            '_hoyouBukaCode = hoyouBukaCode
            '_hoyouTanto = cmbTanto.Text
            ''---------------------------------------------
            ''-------------------------------------------
            ''２次改修
            ''ブロックのアラートを閉じる。
            'frm41TourokuKakunin.Close()
            ''-------------------------------------------
            ''----------------------------------------
            ''２次改修
            ''ブロックのアラートを表示する。
            'DispAlert()
            ''----------------------------------------


        End Sub

        '---------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' 排他制御ブロック情報の更新処理
        ''' </summary>
        ''' <param name="StrEventCode">イベントコード</param>
        ''' <param name="StrDept">部課コード</param>
        ''' <param name="StrtantoNo">担当</param>
        ''' <param name="isMode">「登録」の場合、true</param>
        ''' <remarks></remarks>
        Private Sub RegisterMain(ByVal StrEventCode As String, _
                                 ByVal StrDept As String, _
                                 ByVal StrTantoNo As String, _
                                 ByVal isMode As Boolean)

            Dim aShisakuDate As New ShisakuDate
            Dim tantoVo As New THoyouExclusiveControlTantoVo
            Dim tantoDao As ExclusiveControlTantoDao = New ExclusiveControlTantoImpl

            'KEY情報
            tantoVo.HoyouEventCode = StrEventCode
            tantoVo.HoyouBukaCode = StrDept
            tantoVo.HoyouTanto = StrTantoNo
            '編集情報
            tantoVo.EditUserId = LoginInfo.Now.UserId
            tantoVo.EditDate = aShisakuDate.CurrentDateDbFormat
            tantoVo.EditTime = aShisakuDate.CurrentTimeDbFormat
            '作成情報
            tantoVo.CreatedUserId = LoginInfo.Now.UserId
            tantoVo.CreatedDate = aShisakuDate.CurrentDateDbFormat
            tantoVo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
            '更新情報
            tantoVo.UpdatedUserId = LoginInfo.Now.UserId
            tantoVo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
            tantoVo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

            '追加モードの場合、インサートする。
            If isMode = True Then
                tantoDao.InsetByPk(tantoVo)
            Else
                tantoDao.UpdateByPk(tantoVo)
            End If

        End Sub

        '----------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------
        Private Sub DispAlert()
            ''ブロックのアラートを表示する。
            ''試作イベントコードに該当するアラート情報を取得する。
            'Dim EventDao As IShisakuEventDao = New EventSakusei.ShisakuBuhinMenu.Dao.ShisakuEventDaoImpl
            'Dim EventAlertInfo = EventDao.GetShisakuEvent(_hoyouEventCode)

            ''アラート情報を画面に表示する。
            'If StringUtil.IsEmpty(EventAlertInfo.BlockAlertFlg) Then
            '    'ボタン表示しない
            '    BtnKakunin.Visible = False
            'Else
            '    '0:表示しない、1：表示する。
            '    If EventAlertInfo.BlockAlertFlg = "1" Then
            '        '初期設定
            '        Dim strAlertFlg As String = ""

            '        '1:フル組、2：移管車改修。
            '        If EventAlertInfo.BlockAlertKind = "1" Then
            '            strAlertFlg = "1"
            '        Else
            '            strAlertFlg = "2"
            '        End If

            '        '試作イベントコードに該当するアラート情報を画面に表示する。
            '        Dim AlertInfo = EventDao.GetBlockCheckAlertInfo(_hoyouTanto, strAlertFlg)
            '        If AlertInfo IsNot Nothing Then
            '            If StringUtil.IsNotEmpty(AlertInfo.Naiyou) Then

            '                '改行コードを把握する。


            '                '始めの位置を探す
            '                Dim foundIndex As Integer = AlertInfo.Naiyou.IndexOf(vbLf)
            '                Dim searchWord As String = vbLf
            '                Dim intIchi(100) As Integer
            '                Dim strNaiyou As String
            '                Dim i As Integer = 0


            '                If foundIndex <> -1 Then
            '                    strNaiyou = Mid(AlertInfo.Naiyou, 1, foundIndex)
            '                Else
            '                    strNaiyou = AlertInfo.Naiyou
            '                End If

            '                While 0 <= foundIndex

            '                    '見つかった位置を保存する。
            '                    i = foundIndex

            '                    '次の検索開始位置
            '                    Dim nextIndex As Integer = foundIndex + searchWord.Length
            '                    If nextIndex < AlertInfo.Naiyou.Length Then
            '                        '次の位置を探す
            '                        foundIndex = AlertInfo.Naiyou.IndexOf(vbLf, nextIndex)
            '                    Else
            '                        '最後まで検索したときは終わる
            '                        Exit While
            '                    End If

            '                    '次の検索位置まで値を設定する。
            '                    If foundIndex = -1 Then
            '                        foundIndex = AlertInfo.Naiyou.Length
            '                    End If
            '                    strNaiyou += vbCrLf & Mid(AlertInfo.Naiyou, i + 1, foundIndex - i)

            '                End While

            '                'frm41TourokuKakunin.txtKakunin3.Text = strNaiyou
            '                'frm41TourokuKakunin.Show()
            '                ''ボタン表示する
            '                'BtnKakunin.Visible = True
            '            End If
            '        Else
            '            'ボタン表示しない
            '            BtnKakunin.Visible = False
            '        End If

            '    End If
            'End If

        End Sub
        '----------------------------------------------------------------------------------------------------------------

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        ''部品表の集計コードをHELPで閲覧
        'Private Sub BtnShuukei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShuukei.Click
        '    MsgBox("集計コード　：　説明" & vbCrLf & vbCrLf & _
        '      SHUKEI_X & "　：　" & SHUKEI_X_NAME & vbCrLf & _
        '      SHUKEI_A & "　：　" & SHUKEI_A_NAME & vbCrLf & _
        '      SHUKEI_E & "　：　" & SHUKEI_E_NAME & vbCrLf & _
        '      SHUKEI_R & "　：　" & SHUKEI_R_NAME & vbCrLf & _
        '      SHUKEI_Y & "　：　" & SHUKEI_Y_NAME & vbCrLf & _
        '      SHUKEI_J & "　：　" & SHUKEI_J_NAME, _
        '      MsgBoxStyle.OkOnly, _
        '      "集計コードの一覧")
        'End Sub

        'Excel出力
        Private Sub btnExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelExport.Click


            '   処理中画面表示
            With New frm03Syorichu

                .lblKakunin.Text = "Excel出力中です。"
                .lblKakunin2.Text = ""
                .Execute()
                .Show()

                Application.DoEvents()

                'Dim Result As DialogResult = ExcelCommon.SaveExcelFileDialog("補用部品表 検索件用", koseiWindow.spdParts, "実行結果")

                '   ダイアログ無し。無条件で出力

                'Dim FileName As String

                '
                '   エクセルファイルを吐き出す処理を一部統合(Spreadから保存と、そうでないパターンがあるので一部)
                '   
                'With New Management_Excel_Filename
                '    FileName = .HoyouBuhinKensaku()

                '    ExcelCommon.SaveExcelFile(FileName, koseiWindow.spdParts, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly)

                'End With


                .Close()

                'Return

                'Dim SpdInfo As FarPoint.Win.Spread.FpSpread = koseiWindow.spdParts

                'Try
                '    SpdInfo.ActiveSheet.Protect = False

                '    Application.DoEvents()

                '    SpdInfo.SaveExcel(FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly)

                '    Process.Start(FileName)


                '    .Close()

                '    ComFunc.ShowInfoMsgBox("Excel出力が完了しました")

                'Catch ex As Exception

                '    .Close()

                '    ComFunc.ShowInfoMsgBox("Excel出力に失敗しました")
                'Finally

                'End Try



            End With

        End Sub

#Region "画面の存在チェック"
        ''' <summary>
        ''' すでに画面が存在するか確認
        ''' </summary>
        ''' <param name="fName">画面.Name</param>
        ''' <remarks></remarks>
        ''' <returns>存在時：アクティブにしてTrue　存在時：False</returns>
        Public Function ChkFrm(ByVal fName As String) As Boolean

            '' 所有しているフォームをすべて取り出してタイトルを表示する
            'For Each frm As Form In New ArrayList(Application.OpenForms)
            '    If frm.Name = "Frm41DispOldBuhinSelector" Then
            '        MessageBox.Show(frm.Name)
            '    End If

            '    '自分以外の画面を閉じる
            '    'If frm IsNot Me Then
            '    '    frm.Close()
            '    'End If
            'Next

            ' 所有しているフォームをすべて取り出し、名称チェック
            For Each frm As Form In New ArrayList(Application.OpenForms)
                If frm.Name = fName Then
                    '画面を指定
                    frm.Activate()
                    Return True
                End If
            Next

            Return False
        End Function
#End Region

        ''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bj) (TES)張 ADD BEGIN
        Public ReadOnly Property HoyouKoseiMatrix() As HoyouBuhinBuhinKoseiMatrix
            Get
                Return subject.KoseiSubject.Matrix
            End Get
        End Property
        ''↑↑2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bj) (TES)張 ADD END

    End Class
End Namespace
