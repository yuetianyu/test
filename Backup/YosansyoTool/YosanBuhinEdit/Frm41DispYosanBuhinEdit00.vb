Imports YosansyoTool.YosanshoEdit.Logic
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports YosansyoTool.YosanBuhinEdit.Kosei
Imports ShisakuCommon.Db
Imports EBom.Common
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei
Imports ShisakuCommon.Db.EBom.Dao

Namespace YosanBuhinEdit

    Public Class Frm41DispYosanBuhinEdit00 : Implements Observer

#Region "フォームクローズ"
        ''' <summary>
        ''' フォームクローズ
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Frm41DispYosanBuhinEdit00_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

            If _dispMode = EDIT_MODE Then

                '排他情報をクリア
                ExclusiveControlByView(_yosanEventCode, _yosanUnitKbn)

            End If
        End Sub
#End Region

#Region "フォームロード"
        ''' <summary>
        ''' フォームロード
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Frm41DispShisakuBuhinEdit00_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            '最大化で表示する。
            koseiWindow.WindowState = FormWindowState.Maximized
            koseiWindow.Show()

            '一時保存データの場合、以下のメッセージを表示する。
            If Not StringUtil.IsEmpty(headerSubject.Memo) Then
                Using frm As New Frm41IchijiHozonKakunin
                    frm.ConfirmView("一時保存データです。", headerSubject.Memo)
                End Using
            End If

        End Sub
#End Region

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

#Region "戻るボタン"
        ''' <summary>
        ''' 戻るボタンの処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBack.Click

            If _dispMode = EDIT_MODE Then
                If Not aInputWatcher.WasUpdate Then
                    ExclusiveControlByView(_yosanEventCode, _yosanUnitKbn)
                    Me.Close()
                    Return
                End If
                If frm01Kakunin.ConfirmOkCancel("変更を更新せずに終了しますか？") = MsgBoxResult.Ok Then

                    Dim tantoNo As String = _yosanTanto

                    ExclusiveControlByView(_yosanEventCode, _yosanUnitKbn)

                    Me.Close()
                    Return
                End If
            Else
                Me.Close()
            End If

        End Sub
#End Region

#Region "登録ボタン"
        ''' <summary>
        ''' 登録ボタンの処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub BtnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRegister.Click

            Try
                errorController.ClearBackColor()
                koseiWindow.AssertValidateRegister()

            Catch ex As IllegalInputException

                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)

                koseiWindow.EControl = errorController

                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)

                Return
            Catch ex As Exception
                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)

                Return

            End Try

            'Try
            '    koseiWindow.AssertValidateKyoukuSection()

            'Catch ex As IllegalInputException
            '    errorController.SetBackColorOnWarning(ex.ErrorControls)
            '    errorController.FocusAtFirstControl(ex.ErrorControls)
            '    If frm01Kakunin.ConfirmOkCancel("供給セクションが入力されていません", "このまま登録しますか？") <> MsgBoxResult.Ok Then
            '        Return
            '    End If
            'End Try

            If frm01Kakunin.ConfirmOkCancel("登録を実行しますか？") <> MsgBoxResult.Ok Then
                Return
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            Try
                subject.Register()

                Me.Close()
            Catch ex As TableExclusionException
                ComFunc.ShowInfoMsgBox(ex.Message)
            Finally
                _WasSaveRegister = True ' 排他例外だったとしても、誰かが更新したから
            End Try

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub
#End Region

#Region "保存ボタン"
        ''' <summary>
        ''' 保存ボタンの処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
            Try
                errorController.ClearBackColor()
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
#End Region

#Region "カラーセットボタン"
        ''' <summary>
        ''' カラーセット
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
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
            End If
        End Sub
#End Region

#Region "カラークリアボタン"
        ''' <summary>
        ''' カラークリア
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnColorCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColorCLEAR.Click

            koseiWindow.ClearSheetBackColor()

        End Sub
#End Region

#Region "編集ボタン"
        ''' <summary>
        ''' 編集ボタンの処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub BtnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCall.Click
            Dim tantoNo As String = _yosanTanto

            Dim flgStatusCheck As Boolean = StatusCheck(cmbEventCode.Text)
            If flgStatusCheck = False Then
                ComFunc.ShowInfoMsgBox("このイベントの編集は〆切りました。" & vbLf & "ご不明な点はSKE1までご連絡ください。")
                Me.Close()
                Exit Sub
            End If

            If _dispMode = EDIT_MODE Then
                If aInputWatcher.WasUpdate Then
                    If frm01Kakunin.ConfirmOkCancel("登録されていません。", "変更を破棄してイベントを呼び出しますか？") <> MsgBoxResult.Ok Then
                        Return
                    End If
                End If
            End If

            '排他
            ExclusiveControlByCall(_yosanEventCode, _yosanUnitKbn)


            Me._dispMode = EDIT_MODE

            koseiWindow.Close()
            koseiWindow.Dispose()
            '---------------------------------------------
            'ここで一度画面を破棄します。
            '---------------------------------------------

            subject = New Logic.BuhinEditSubject(_yosanEventCode, _yosanUnitKbn, LoginInfo.Now, KSMode)
            headerSubject = subject.HeaderSubject
            subject.AddObserver(Me)
            headerSubject.AddObserver(Me)

            koseiWindow = New Frm41DispYosanBuhinEdit20(_patterNameList, _yosanEventCode, _yosanBukaCode, _yosanTanto, subject.KoseiSubject, _dispMode, Me)
            koseiWindow.MdiParent = Me
            koseiWindow.WindowState = FormWindowState.Maximized

            aInputWatcher = New InputWatcher

            InitializeWatcher()
            InitializeHeader()
            InitializeValidator()

            BtnRegister.Enabled = True
            BtnSave.Enabled = True
            TxtTel.Enabled = True

            lblDispMode.Visible = False
            lblDispOnly.Visible = False

            koseiWindow.Show()

            subject.NotifyObservers()

            aInputWatcher.Clear()
            '文字色、大きさもデフォルトに戻す。
            koseiWindow.ClearSheetBackColorAll()

            'イベント更新チェック
            '一時保存データの場合、以下のメッセージを表示する。
            If Not StringUtil.IsEmpty(headerSubject.Memo) Then
                Using frm As New Frm41IchijiHozonKakunin
                    frm.ConfirmView("一時保存データです。", headerSubject.Memo)
                End Using
            End If

            aInputWatcher.Clear()

        End Sub
#End Region

#Region "閲覧ボタン"
        ''' <summary>
        ''' 閲覧ボタンの処理
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click

            If _dispMode = EDIT_MODE Then
                If aInputWatcher.WasUpdate Then
                    If frm01Kakunin.ConfirmOkCancel("登録されていません。", "変更を破棄してイベントを呼び出しますか？") <> MsgBoxResult.Ok Then
                        Return
                    End If
                End If
            End If

            '排他
            ExclusiveControlByView(_yosanEventCode, _yosanUnitKbn)

            Me._dispMode = VIEW_MODE

            subject = Nothing

            koseiWindow.Close()
            koseiWindow.Dispose()

            '---------------------------------------------
            'ここで一度画面を破棄します。
            '---------------------------------------------

            subject = New Logic.BuhinEditSubject(_yosanEventCode, _yosanUnitKbn, LoginInfo.Now, KSMode)

            headerSubject = subject.HeaderSubject
            subject.AddObserver(Me)
            headerSubject.AddObserver(Me)

            koseiWindow = New Frm41DispYosanBuhinEdit20(_patterNameList, _yosanEventCode, _yosanBukaCode, _yosanTanto, subject.KoseiSubject, _dispMode, Me)
            koseiWindow.MdiParent = Me
            koseiWindow.WindowState = FormWindowState.Maximized

            aInputWatcher = New InputWatcher

            InitializeWatcher()
            InitializeHeader()
            InitializeValidator()

            BtnRegister.Enabled = False
            BtnSave.Enabled = False
            TxtTel.Enabled = False

            lblDispMode.Visible = True
            lblDispOnly.Visible = False

            subject.NotifyObservers()

            aInputWatcher.Clear()
            '文字色、大きさもデフォルトに戻す。
            koseiWindow.ClearSheetBackColorAll()

            koseiWindow.viewLockForViewMode()

            koseiWindow.Show()

            aInputWatcher.Clear()

        End Sub
#End Region

#Region "テキストボックス入力"
        ''' <summary>
        ''' イベント入力変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub cmbEventCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEventCode.TextChanged
            If headerSubject Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            headerSubject.YosanEventSelectedValue = cmbEventCode.SelectedValue
            If StringUtil.IsEmpty(cmbEventCode.Text) Then
                '使用不可
                BtnCall.Enabled = False
                btnView.Enabled = False
            Else
                '使用可能
                BtnCall.Enabled = True
                btnView.Enabled = True
            End If
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
#End Region

        Private ReadOnly _yosanEventCode As String
        Private ReadOnly _yosanUnitKbn As String

        Private _yosanBukaCode As String
        Private _yosanTanto As String
        Private _dispMode As Integer
        Private _patterNameList As List(Of String)
        Private subject As Logic.BuhinEditSubject
        Private headerSubject As Logic.BuhinEditHeaderSubject

        Private koseiWindow As Frm41DispYosanBuhinEdit20

        Private aInputWatcher As InputWatcher

        Private IsSuspendEventValueChanged As Boolean

        Private validatorTantoCall As Validator

        Private KSMode As Integer
        Private _strMode As String   '*****完了閲覧モード対応*****

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="patterNameList"></param>
        ''' <param name="yosanEventCode"></param>
        ''' <param name="yosanUnitKbn"></param>
        ''' <param name="loginInfo"></param>
        ''' <param name="dispMode"></param>
        ''' <param name="KSMode"></param>
        ''' <param name="strMode"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal patterNameList As List(Of String), ByVal yosanEventCode As String, ByVal yosanUnitKbn As String, ByVal loginInfo As LoginInfo, _
                       ByVal dispMode As Integer, ByVal KSMode As Integer, ByVal strMode As String)

            InitializeComponent()

            ShisakuFormUtil.Initialize(Me)
            Me._patterNameList = patterNameList
            Me._yosanEventCode = yosanEventCode
            Me._yosanUnitKbn = yosanUnitKbn

            Me._yosanBukaCode = LoginInfo.BukaCode
            Me._yosanTanto = LoginInfo.UserId

            Me._dispMode = dispMode
            Me.KSMode = KSMode
            Me._strMode = strMode   '*****完了閲覧モード対応*****

            '画面のイベントコードを設定
            cmbEventCode.Text = _yosanEventCode

            ''メタルとかトリムとか表示しています
            'If dispMode = CInt(EDIT_MODE) Then
            '    LblEditMsg.Visible = True
            '    If String.Equals(yosanUnitKbn, DispYosanshoEdit.UNIT_KBN_METAL_CODE) Then
            '        LblEditMsg.Text = "メタル部品表編集中"
            '    Else
            '        LblEditMsg.Text = "トリム部品表編集中"
            '    End If
            'Else
            '    LblEditMsg.Visible = False
            '    If String.Equals(yosanUnitKbn, DispYosanshoEdit.UNIT_KBN_METAL_CODE) Then
            '        lblDispMode.Text = "メタル部品表閲覧表示中"
            '    Else
            '        lblDispMode.Text = "トリム部品表閲覧表示中"
            '    End If
            'End If
            '部品行名を表示します。
            If dispMode = CInt(EDIT_MODE) Then
                LblEditMsg.Visible = True
                LblEditMsg.Text = "【 " & _yosanUnitKbn & " 】部品表編集中"
            Else
                LblEditMsg.Visible = False
                LblEditMsg.Text = "【 " & _yosanUnitKbn & " 】部品表閲覧表示中"
            End If


            subject = New Logic.BuhinEditSubject(yosanEventCode, _yosanUnitKbn, loginInfo, KSMode)
            headerSubject = subject.HeaderSubject
            subject.AddObserver(Me)
            headerSubject.AddObserver(Me)

            koseiWindow = New Frm41DispYosanBuhinEdit20(patterNameList, yosanEventCode, _yosanBukaCode, _yosanTanto, subject.KoseiSubject, _dispMode, Me)
            koseiWindow.MdiParent = Me

            aInputWatcher = New InputWatcher

            InitializeWatcher()
            InitializeHeader()
            InitializeValidator()

            subject.NotifyObservers()
            If _dispMode = VIEW_MODE Then
                koseiWindow.viewLockForViewMode()   '----追加
            End If

            aInputWatcher.Clear()

        End Sub
#End Region

        ''' <summary>
        ''' 排他チェック
        ''' </summary>
        ''' <param name="eventCode"></param>
        ''' <remarks></remarks>
        Public Sub ExclusiveControlByCall(ByVal eventCode As String, ByVal unitKbn As String)
            Dim controlBuhinVo As New TYosanExclusiveControlBuhinVo
            Dim controlBuhinDao As TYosanExclusiveControlBuhinDao = New TYosanExclusiveControlBuhinDaoImpl
            controlBuhinVo.YosanEventCode = eventCode
            controlBuhinVo.BuhinhyoName = unitKbn
            Dim controlBuhinVos As List(Of TYosanExclusiveControlBuhinVo) = controlBuhinDao.FindBy(controlBuhinVo)

            If controlBuhinVos IsNot Nothing AndAlso controlBuhinVos.Count > 0 Then
                '「社員マスター」を検索、「社員名」を取得する。
                Dim aRhac2130Dao As Rhac2130Dao = New Rhac2130DaoImpl
                Dim user2130Vo As Rhac2130Vo
                Dim userName As String = String.Empty

                If String.Equals(controlBuhinVos(0).EditUserId, LoginInfo.Now.UserId) Then
                    '「編集者職番」が存在しユーザーＩＤと同じコードなら
                    user2130Vo = aRhac2130Dao.FindByPk(LoginInfo.Now.UserId)
                    If user2130Vo IsNot Nothing Then
                        userName = user2130Vo.ShainName
                    End If
                    ComFunc.ShowInfoMsgBox("選択したイベントは下記の方が編集中に異常終了したもようです。編集できます。" & vbLf & vbLf & _
                                           "担当者[" & controlBuhinVos(0).EditUserId & ":" & userName & "]")
                Else
                    '「編集者職番」が存在しユーザーＩＤと違うコードなら
                    user2130Vo = aRhac2130Dao.FindByPk(controlBuhinVos(0).EditUserId)
                    If user2130Vo IsNot Nothing Then
                        userName = user2130Vo.ShainName
                    End If
                    ComFunc.ShowInfoMsgBox("選択したイベントは編集中です。" & vbLf & vbLf & _
                                           "担当者[" & controlBuhinVos(0).EditUserId & ":" & userName & "]")
                End If

                For Each Vo As TYosanExclusiveControlBuhinVo In controlBuhinVos
                    'KEY情報を削除
                    controlBuhinDao.DeleteBy(Vo)
                Next
            Else
                Dim aSystemDate As New ShisakuDate
                '排他管理予算書部品情報作成
                Using db As New EBomDbClient
                    db.BeginTransaction()
                    Try
                        Dim exclusiveVo As New TYosanExclusiveControlBuhinVo
                        exclusiveVo.YosanEventCode = eventCode
                        exclusiveVo.BuhinhyoName = unitKbn
                        exclusiveVo.EditUserId = LoginInfo.Now.UserId
                        exclusiveVo.EditDate = aSystemDate.CurrentDateDbFormat
                        exclusiveVo.EditTime = aSystemDate.CurrentTimeDbFormat
                        exclusiveVo.CreatedUserId = LoginInfo.Now.UserId
                        exclusiveVo.CreatedDate = aSystemDate.CurrentDateDbFormat
                        exclusiveVo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                        exclusiveVo.UpdatedUserId = LoginInfo.Now.UserId
                        exclusiveVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                        exclusiveVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat
                        controlBuhinDao.InsertBy(exclusiveVo)
                    Catch ex As Exception
                        db.Rollback()
                        MsgBox("排他管理予算書イベント情報を作成出来ませんでした。")
                    End Try
                    db.Commit()
                End Using
            End If
        End Sub

        ''' <summary>
        ''' 排他を解除
        ''' </summary>
        ''' <param name="EventCode">解除したいイベントコード</param>
        ''' <param name="unitKbn">ユニット区分</param>
        ''' <remarks></remarks>
        Private Sub ExclusiveControlByView(ByVal EventCode As String, ByVal unitKbn As String)

            Dim controlBuhinVo As New TYosanExclusiveControlBuhinVo
            Dim controlBuhinDao As TYosanExclusiveControlBuhinDao = New TYosanExclusiveControlBuhinDaoImpl
            Dim controlBuhinVos As List(Of TYosanExclusiveControlBuhinVo)

            controlBuhinVo.YosanEventCode = EventCode
            controlBuhinVo.BuhinhyoName = unitKbn
            controlBuhinVos = controlBuhinDao.FindBy(controlBuhinVo)
            If controlBuhinVos IsNot Nothing And controlBuhinVos.Count > 0 Then
                For Each Vo As TYosanExclusiveControlBuhinVo In controlBuhinVos
                    'KEY情報を削除
                    controlBuhinDao.DeleteBy(Vo)
                Next
            End If

        End Sub

        ''' <summary>
        ''' ステータスチェック
        ''' </summary>
        ''' <param name="eventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function StatusCheck(ByVal eventCode As String) As Boolean

            Dim eventDao As TYosanEventDao = New ShisakuCommon.Db.EBom.Dao.Impl.TYosanEventDaoImpl
            Dim eventVo As TYosanEventVo = eventDao.FindByPk(eventCode)

            If eventVo Is Nothing Then
                MsgBox("予算書イベント情報が存在しません。")
                Return True
            Else
                If String.Equals(eventVo.YosanStatus, "01") Then
                    Return False
                End If
            End If
            Return True

        End Function

        Private Sub InitializeWatcher()

            aInputWatcher.Add(TxtTel)
            aInputWatcher.Add(cmbEventCode)
            aInputWatcher.Add(koseiWindow.spdParts)
        End Sub

        Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            If observable Is headerSubject Then
                UpdateHeader(observable, arg)
            Else
                headerSubject.NotifyObservers(arg)
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
                LblEvent.Text = String.Format("{0}  {1}", headerSubject.YosanKaihatsuFugo, headerSubject.YosanEventName)
                If Not StringUtil.IsEmpty(headerSubject.YosanBukaName) Then
                    LblBukaName.Text = headerSubject.YosanBukaName
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

                lblDispMode.Visible = True
                lblDispOnly.Visible = False
                If _strMode = HoyouKanryoViewMode Then    '*****完了閲覧モード対応*****
                    BtnCall.Visible = False
                End If

            ElseIf _dispMode = EDIT_MODE Then
                BtnRegister.Enabled = True
                BtnSave.Enabled = True
                TxtTel.Enabled = True

                lblDispMode.Visible = False
                lblDispOnly.Visible = False

            ElseIf _dispMode = EXCEL_MODE Then   ''Excel出力専用モード
                lblDispMode.Visible = False
                lblDispOnly.Visible = True

                LblEvent.Visible = False
                LblBuka.Visible = False
                LblBukaName.Visible = False
                LblTanto.Visible = False
                LblUserName.Visible = False
                LblTel.Visible = False
                TxtTel.Visible = False

                lblEventCode.Visible = False
                cmbEventCode.Visible = False
                BtnCall.Visible = False
                btnView.Visible = False

                BtnRegister.Visible = False
                BtnSave.Visible = False

            End If
            ShisakuFormUtil.setTitleVersion(Me)

            If Not _dispMode = EXCEL_MODE Then   ''Excel出力専用モード　以外
                ShisakuFormUtil.SettingDefaultProperty(cmbEventCode)
                cmbEventCode.MaxLength = 12
            End If
        End Sub

        Private Sub InitializeValidator()
            validatorTantoCall = New Validator
            validatorTantoCall.Add(cmbEventCode, "イベントコード").Required()
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

    End Class
End Namespace
