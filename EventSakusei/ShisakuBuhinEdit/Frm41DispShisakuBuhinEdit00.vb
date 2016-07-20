Imports EventSakusei.ShisakuBuhinEdit.Kosei
Imports ShisakuCommon.Db
Imports EBom.Common
Imports EventSakusei.ShisakuBuhinEdit.Al
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Util
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEditBlock
Imports EventSakusei.ShisakuBuhinEdit.Al.Dao
Imports EventSakusei.ShisakuBuhinEditBlock.Dao
Imports EventSakusei.ShisakuBuhinEditEvent.Dao
Imports EventSakusei.ShisakuBuhinMenu.Dao
Imports EventSakusei.ShisakuBuhinEdit.Al.Ui
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Ui
''↓↓2014/09/17 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
''↑↑2014/09/17 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END

Namespace ShisakuBuhinEdit

    Public Class Frm41DispShisakuBuhinEdit00 : Implements Observer
        ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_n) (TES)張 ADD BEGIN
        Private BASEINFO As String = "D"
        Private alObserver As SpdAlObserver
        Private koseiObserver As SpdKoseiObserver
        ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_n) (TES)張 ADD END
        Private Sub Frm41DispShisakuBuhinEdit00_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

            '-------------------------------------------
            '２次改修
            'ブロックのアラートを閉じる。
            Dim frmKakunin As New frm41TourokuKakunin
            frmKakunin.Close()
            ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 y) (TES)施 ADD BEGIN
            Dim frmKaiteiKakunin As New Frm41KaiteiKakunin(Me)
            frmKaiteiKakunin.Close()
            ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 y) (TES)施 ADD END
            '↓↓2014/10/16 酒井 ADD BEGIN
            frm41SaiKakunin.Close()
            frm41SaiKakuninMin.Close()
            '↑↑2014/10/16 酒井 ADD END
            '-------------------------------------------

            If _dispMode = EDIT_MODE Then
                '---------------------------
                '---------------------------
                '２次改修
                '排他情報をクリア
                ExclusiveControl()
                '---------------------------
                '---------------------------
            End If
        End Sub


        Private _preForm As Frm38DispShisakuBuhinEditBlock
        Private Sub Frm41DispShisakuBuhinEdit00_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'ToolStripManager.Renderer = _
            '     New ToolStripProfessionalRenderer( _
            '     New EBom.Controls.CustomProfessionalRenderer())

            koseiWindow.Show()
            '部品表を開いた時点で一時レイアウト調整
            Me.LayoutMdi(MdiLayout.TileVertical)

            alWindow.Show()

            '--------------------------------------
            '２次改修
            '   以下の処理は廃止する。
            ''閲覧モード表示時は出力しない
            'If _dispMode = EDIT_MODE Then
            '    alWindow.EventChange()
            'End If
            '--------------------------------------

            'レイアウト調整
            Me.LayoutMdi(MdiLayout.TileVertical)

            '一時保存データの場合、以下のメッセージを表示する。
            If Not StringUtil.IsEmpty(headerSubject.Memo) Then
                Using frm As New Frm41IchijiHozonKakunin
                    frm.ConfirmView("一時保存データです。", headerSubject.Memo)
                End Using
            End If

            '----------------------------------------
            '２次改修
            'ブロックのアラートを表示する。
            DispAlert()
            '----------------------------------------
            '↓↓2014/10/16 酒井 ADD BEGIN
            DispSai()
            '↑↑2014/10/16 酒井 ADD END
        End Sub

        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBack.Click

            '-------------------------------------------
            '２次改修
            'ブロックのアラートを閉じる。
            frm41TourokuKakunin.Close()
            ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 ac) (TES)施 ADD BEGIN
            ''↓↓2014/08/20 Ⅰ.11.改訂戻し機能 ac) 酒井 ADD BEGIN
            'Frm41KaiteiKakunin.Close()
            ''↑↑2014/08/20 Ⅰ.11.改訂戻し機能 ac) 酒井 ADD END	
            ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 ac) (TES)施 ADD END	
            '↓↓2014/10/16 酒井 ADD BEGIN
            frm41SaiKakunin.Close()
            frm41SaiKakuninMin.Close()
            '↑↑2014/10/16 酒井 ADD END
            '-------------------------------------------

            If _dispMode = EDIT_MODE Then
                If Not aInputWatcher.WasUpdate Then
                    ''---------------------------
                    ''---------------------------
                    ''２次改修
                    ''排他情報をクリア
                    'ExclusiveControl()
                    ''---------------------------
                    ''---------------------------
                    ''↓↓2014/08/20 Ⅰ.11.改訂戻し機能 ac) 酒井 ADD BEGIN
                    Frm41KaiteiKakunin.Close()
                    ''↑↑2014/08/20 Ⅰ.11.改訂戻し機能 ac) 酒井 ADD END	
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
                    ''↓↓2014/08/20 Ⅰ.11.改訂戻し機能 ac) 酒井 ADD BEGIN
                    Frm41KaiteiKakunin.Close()
                    ''↑↑2014/08/20 Ⅰ.11.改訂戻し機能 ac) 酒井 ADD END	
                    Me.Close()
                    Return
                End If
            Else
                ''↓↓2014/08/20 Ⅰ.11.改訂戻し機能 ac) 酒井 ADD BEGIN
                Frm41KaiteiKakunin.Close()
                ''↑↑2014/08/20 Ⅰ.11.改訂戻し機能 ac) 酒井 ADD END	
                Me.Close()
            End If

        End Sub

        Private Sub ExclusiveControl()
            Dim blockVo As New TExclusiveControlBlockVo
            Dim blockDao As ExclusiveControlBlockDao = New ExclusiveControlBlockImpl

            'キー情報が無いとエラーになるので事前にチェック（イレギュラー以外あるはず）
            blockVo = blockDao.FindByPk(_shisakuEventCode, _shisakuBukaCode, _shisakuBlockNo)
            If IsNothing(blockVo) Then
                'キー情報が無ければスルー。
            Else
                'KEY情報をセット
                blockVo.ShisakuEventCode = _shisakuEventCode
                blockVo.ShisakuBukaCode = _shisakuBukaCode
                blockVo.ShisakuBlockNo = _shisakuBlockNo
                blockVo.EditUserId = LoginInfo.Now.UserId 'ログインユーザーIDをセット
                'KEY情報を削除
                blockDao.DeleteByPk(blockVo)
            End If

        End Sub



        Private Sub btnCOLOR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCOLOR.Click

            ' 2012-01-11 #2-1
            ' 試作種別がDの場合、パレットを表示しない。
            'アクティブシートでは無理'

            If StringUtil.Equals(GetActiveSheet().SheetName, "AL") Then
                If subject.AlSubject.ShisakuSyubetu(GetActiveSheet().ActiveRow.Index - 5) = "D" Then
                    Return
                End If
            End If



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
            If IsActiveAlWindow() Then
                alWindow.ClearSheetBackColor()
                subject.AlSubject.setSpdChanged()
                subject.AlSubject.NotifyObservers()
            Else
                koseiWindow.ClearSheetBackColor()
                '↓↓2014/10/22 酒井 ADD BEGIN
                subject.KoseiSubject.CallSetChanged()
                subject.KoseiSubject.NotifyObservers()
                '↑↑2014/10/22 酒井 ADD END
            End If
        End Sub
#Region "２次改修・試作イベント情報のステータスをチェック。不正の場合処理中断"

        Public Function StatusCheck(ByVal strStatusMode As String) As Boolean

            '------------------------------------------------------------------------------------------------------
            '初期設定
            Dim tShisakuEventDaoImpl As ShisakuEventDao = New EventSakusei.ShisakuBuhinEditBlock.Dao.ShisakuEventDaoImpl
            Dim tShisakuEventVo As New TShisakuEventVo()
            '------------------------------------------------------------------------------------------------------

            '試作イベント情報からステータスを取得。
            tShisakuEventVo = tShisakuEventDaoImpl.FindByPk(_shisakuEventCode)

            If IsNothing(tShisakuEventVo) Then
                MsgBox("試作イベント情報が存在しません。")
                Return False
            Else
                Select Case strStatusMode
                    Case ShishakuHensyuMode
                        If tShisakuEventVo.Status = "21" Then
                            'MsgBox("編集モードです。試作イベント情報のステータスは・・・ＯＫ。")
                            Return True
                        Else
                            MsgBox("イベントのステータスが変更されました。メニュー画面に戻ります。")
                            Return False
                        End If
                    Case ShishakuKaiteiHensyuMode
                        If tShisakuEventVo.Status = "23" Then
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
            Dim blockNo As String = cmbBlockNo.Text

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

            Try
                errorController.ClearBackColor()

                validatorBlockCall.AssertValidate()
            Catch ex As IllegalInputException
                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)
                cmbBlockNo.Text = Me._shisakuBlockNo
                Return
            End Try

            If _dispMode = EDIT_MODE Then

                If aInputWatcher.WasUpdate Then
                    If frm01Kakunin.ConfirmOkCancel("登録されていません。", "変更を破棄しブロックを呼出しますか？") <> MsgBoxResult.Ok Then
                        cmbBlockNo.Text = Me._shisakuBlockNo
                        Return
                    End If
                End If

            End If

            'ブロック呼び出し前にチェックする'
            Dim impl As Al.Dao.BuhinEditAlDao = New Al.Dao.BuhinEditAlDaoImpl
            Dim sVo As New TShisakuSekkeiBlockVo
            sVo = impl.FindByNewBlockNo(_shisakuEventCode, _shisakuBukaCode, cmbBlockNo.Text)

            If Not _preForm.ShowFrm41ShisakuBuhinEditFromFrm41(cmbBlockNo.Text) Then
                cmbBlockNo.Text = Me._shisakuBlockNo
                Return
            End If


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

            '他のユーザーが編集中か確認する。
            Dim tExclusiveControlBlockDaoImpl As ExclusiveControlBlockDao = New ExclusiveControlBlockImpl
            Dim tExclusiveControlBlockVo As New TExclusiveControlBlockVo()

            '排他制御ブロック情報から試作イベントコード、部課コード、ブロック№が登録されているかチェック。
            tExclusiveControlBlockVo = tExclusiveControlBlockDaoImpl.FindByPk(_shisakuEventCode, _
                                                                              _shisakuBukaCode, _
                                                                              blockNo)

            'AL再展開中かもチェック。
            If IsNothing(tExclusiveControlBlockVo) Then
                tExclusiveControlBlockVo = tExclusiveControlBlockDaoImpl.FindByPk(_shisakuEventCode, _shisakuBukaCode, "ZZZZ")
                '他のユーザーがＡＬ再展開中なら警告を表示して終了。
                If Not IsNothing(tExclusiveControlBlockVo) Then
                    MsgBox("ＡＬ再展開中です。" & vbCrLf & vbCrLf & _
                           "暫くお待ちいただき再度実行をお願い致します。" & vbCrLf & vbCrLf & _
                            "担当者「" & tExclusiveControlBlockVo.EditUserId & _
                                     "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")
                    'ＡＬ再展開中なら処理中断
                    Exit Sub
                End If
            End If

            If IsNothing(tExclusiveControlBlockVo) Then
                'MsgBox("選択したブロックは誰も使用していません。")
                isExclusive = True '編集OK
                isReg = True '追加モード
            Else
                isReg = False '更新モード
                'レコードが有る場合、ログインユーザーと編集者コードを比較する。
                '担当者名を取得する'
                tanTousya = getDate.FindByShainName(tExclusiveControlBlockVo.EditUserId)
                '同じなら編集OK。
                If tExclusiveControlBlockVo.EditUserId = LoginInfo.Now.UserId Then
                    isExclusive = True '編集OK
                Else
                    '違うなら編集NG。
                    isExclusive = False '編集NG
                End If
            End If

            '排他チェック
            If isExclusive = True Then

                '編集ブロックが変更されたのでひとつ前の編集中の排他制御をクリアする。
                '但し、編集中のブロックが指定されている場合にはクリア処理はスルーする。
                If cmbBlockNo.Text <> _shisakuBlockNo Then
                    '---------------------------
                    '---------------------------
                    '２次改修
                    '排他情報をクリア
                    ExclusiveControl()
                    '---------------------------
                    '---------------------------
                End If


                '------------------------------------------------
                '------------------------------------------------
                '他のユーザーが編集していなければ排他制御ブロックテーブルにレコード更新。
                RegisterMain(_shisakuEventCode, _shisakuBukaCode, blockNo, isReg)

                '続けて処理を続行。
                '------------------------------------------------
                '------------------------------------------------

            Else
                '他のユーザーが編集していれば警告を表示して終了。
                MsgBox("選択したブロックは編集中です。" & vbCrLf & vbCrLf & _
                        "担当者「" & tExclusiveControlBlockVo.EditUserId & _
                                 "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")

                '元のブロックを設定して処理終了。
                cmbBlockNo.Text = _shisakuBlockNo
                Return

            End If

            '変更前のブロックを保持する。
            _shisakuBlockNo = cmbBlockNo.Text

            '------------------------------------------------------------------------------------------------------
            '------------------------------------------------------------------------------------------------------



            '-------------------------------------------
            '２次改修
            'ブロックのアラートを閉じる。
            frm41TourokuKakunin.Close()
            ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 ab) (TES)施 ADD BEGIN
            Frm41KaiteiKakunin.Close()
            ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 ab) (TES)施 ADD END	
            '↓↓2014/10/16 酒井 ADD BEGIN
            frm41SaiKakunin.Close()
            frm41SaiKakuninMin.Close()
            '↑↑2014/10/16 酒井 ADD END
            '-------------------------------------------






            Me._dispMode = EDIT_MODE

            alWindow.Close()
            alWindow.Dispose()

            koseiWindow.Close()
            koseiWindow.Dispose()
            '---------------------------------------------
            'ここで一度画面を破棄します。
            '---------------------------------------------

            subject = New Logic.BuhinEditSubject(_shisakuEventCode, _shisakuBukaCode, blockNo, LoginInfo.Now, KSMode)
            headerSubject = subject.HeaderSubject
            subject.AddObserver(Me)
            headerSubject.AddObserver(Me)

            ''↓↓2014/08/07 Ⅰ.3.設計編集 ベース車改修専用化_ap) (TES)張 CHG BEGIN
            'alWindow = New Frm41DispShisakuBuhinEdit10(_shisakuEventCode, subject.AlSubject, _dispMode)
            alWindow = New Frm41DispShisakuBuhinEdit10(_shisakuEventCode, subject.AlSubject, subject.KoseiSubject, _dispMode, "Frm41DispShisakuBuhinEdit00")
            ''↑↑2014/08/07 Ⅰ.3.設計編集 ベース車改修専用化_ap) (TES)張 CHG END
            alWindow.MdiParent = Me

            koseiWindow = New Frm41DispShisakuBuhinEdit20(_shisakuEventCode, subject.KoseiSubject, _dispMode)
            koseiWindow.MdiParent = Me

            ''↓↓2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_QA95 (TES)張 ADD BEGIN
            alObserver = alWindow.frmAlObserver
            koseiObserver = koseiWindow.frmKoseiObserver
            ''↑↑2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_QA95 (TES)張 ADD END

            aInputWatcher = New InputWatcher

            InitializeWatcher()
            InitializeHeader()
            InitializeValidator()


            'alWindow = New Frm41DispShisakuBuhinEdit10(_shisakuEventCode, subject.AlSubject)
            'alWindow.MdiParent = Me

            'koseiWindow = New Frm41DispShisakuBuhinEdit20(_shisakuEventCode, subject.KoseiSubject)
            'koseiWindow.MdiParent = Me


            BtnRegister.Enabled = True
            BtnSave.Enabled = True
            TxtTel.Enabled = True

            txtNaiyou.Enabled = True '２次改修で追加

            lblDispMode.Visible = False

            alWindow.ClearInstlColumns()

            koseiWindow.ClearInstlColumns()

            koseiWindow.Show()
            Me.LayoutMdi(MdiLayout.TileVertical)

            alWindow.Show()
            Me.LayoutMdi(MdiLayout.TileVertical)


            'subject.ApplyBlockNo()
            subject.NotifyObservers()

            'cmbBlockNo.Text = blockNo

            aInputWatcher.Clear()
            ''↓↓2014/09/02 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
            '文字色、大きさもデフォルトに戻す。
            'alWindow.ClearSheetBackColorAll()
            'koseiWindow.ClearSheetBackColorAll()
            ''↑↑2014/09/02 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END

            'イベント更新チェック
            alWindow.EventChange()
            '一時保存データの場合、以下のメッセージを表示する。
            If Not StringUtil.IsEmpty(headerSubject.Memo) Then
                Using frm As New Frm41IchijiHozonKakunin
                    frm.ConfirmView("一時保存データです。", headerSubject.Memo)
                End Using
            End If

            '画面表示時の日付時刻を保持しておく
            Dim alDao As New BuhinEditAlDaoImpl
            Dim tempVo As New TShisakuSekkeiBlockVo
            tempVo = alDao.FindByDBTimeStamp(_shisakuEventCode)
            _OpenDateTime = tempVo.UpdatedDate & tempVo.UpdatedTime

            aInputWatcher.Clear()

            '----------------------------------------
            '２次改修
            'ブロックのアラートを表示する。
            DispAlert()
            '----------------------------------------

            '↓↓2014/10/14 酒井 ADD BEGIN
            DispSai()
            '↑↑2014/10/14 酒井 ADD END
        End Sub

        Private Sub msBuhinKouseiHyoujiTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msBuhinKouseiHyoujiTool.Click

        End Sub


        Private ReadOnly _shisakuEventCode As String
        Private ReadOnly _shisakuBukaCode As String
        '----------------------------------------------------
        '----------------------------------------------------
        '２次改修
        'ブロックは変更されることがあるのでReadonlyを外す。
        'Private ReadOnly _shisakuBlockNo As String
        Private _shisakuBlockNo As String
        '----------------------------------------------------
        '----------------------------------------------------

        Private _dispMode As Integer

        Private subject As Logic.BuhinEditSubject
        Private headerSubject As Logic.BuhinEditHeaderSubject

        Private alWindow As Frm41DispShisakuBuhinEdit10
        Private koseiWindow As Frm41DispShisakuBuhinEdit20

        Private aInputWatcher As InputWatcher

        Private IsSuspendEventValueChanged As Boolean

        Private validatorBlockCall As Validator

        Private KSMode As Integer
        Private _OpenDateTime As String

        Private _strMode As String   '*****完了閲覧モード対応*****

        Public Sub New(ByVal frm As Frm38DispShisakuBuhinEditBlock, ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal dispMode As Integer, ByVal KSMode As Integer, ByVal strMode As String)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ShisakuFormUtil.Initialize(Me)

            Me._shisakuEventCode = shisakuEventCode
            Me._shisakuBukaCode = shisakuBukaCode
            Me._shisakuBlockNo = shisakuBlockNo
            Me._dispMode = dispMode
            Me._preForm = frm
            Me.KSMode = KSMode

            Me._strMode = strMode   '*****完了閲覧モード対応*****

            subject = New Logic.BuhinEditSubject(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, LoginInfo.Now, KSMode)
            headerSubject = subject.HeaderSubject
            subject.AddObserver(Me)
            headerSubject.AddObserver(Me)

            alWindow = New Frm41DispShisakuBuhinEdit10(shisakuEventCode, subject.AlSubject, subject.KoseiSubject, _dispMode, "Frm41DispShisakuBuhinEdit00", shisakuBukaCode, shisakuBlockNo)
            alWindow.MdiParent = Me

            koseiWindow = New Frm41DispShisakuBuhinEdit20(shisakuEventCode, subject.KoseiSubject, _dispMode)
            koseiWindow.MdiParent = Me

            alObserver = alWindow.frmAlObserver
            koseiObserver = koseiWindow.frmKoseiObserver
            '↓↓↓2014/12/25 ブロック№のMT区分を取得、材料の表示/非表示を切り替える機能を追加 TES)張 ADD BEGIN
            If Not subject.FindByMTKbnFromRhac0080(shisakuBlockNo).Equals("T") Then
                'MT区分（MT_KBN）が"T"の場合、試作部品構成編集スプレッドの材料情報を表示する。
                koseiWindow.SetZaishituColumnDisable()
            End If
            '↑↑↑2014/12/25 ブロック№のMT区分を取得、材料の表示/非表示を切り替える機能を追加 TES)張 ADD END

            aInputWatcher = New InputWatcher

            InitializeWatcher()
            InitializeHeader()
            InitializeValidator()

            '画面表示時の日付時刻を保持しておく
            Dim alDao As New BuhinEditAlDaoImpl
            Dim tempVo As New TShisakuSekkeiBlockVo
            tempVo = alDao.FindByDBTimeStamp(shisakuEventCode)
            If tempVo Is Nothing Then
                _OpenDateTime = ""
            Else
                If Not StringUtil.IsEmpty(tempVo.UpdatedDate) AndAlso Not StringUtil.IsEmpty(tempVo.UpdatedTime) Then
                    _OpenDateTime = tempVo.UpdatedDate & tempVo.UpdatedTime
                Else
                    _OpenDateTime = ""
                End If
            End If

            '2014/12/24 追加　移管車の場合のみ「ベース情報表示/非表示」ボタンを表示する
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo = eventDao.FindByPk(shisakuEventCode)
            If eventVo.BlockAlertKind = "2" And eventVo.KounyuShijiFlg IsNot Nothing Then
                Me.btnBaseInfo.Visible = True
            Else
                Me.btnBaseInfo.Visible = False
            End If



            subject.NotifyObservers()
            If _dispMode = VIEW_MODE Then
                alWindow.viewLockForViewMode()   '-----追加
                koseiWindow.viewLockForViewMode()   '----追加
            End If

            aInputWatcher.Clear()

        End Sub

        Private Sub InitializeWatcher()

            aInputWatcher.Add(TxtTel)
            aInputWatcher.Add(txtNaiyou) '２次改修で追加
            aInputWatcher.Add(alWindow.spdParts)
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
                LblEvent.Text = String.Format("{0}  {1}", headerSubject.ShisakuKaihatsuFugo, headerSubject.ShisakuEventName)
                If Not StringUtil.IsEmpty(headerSubject.ShisakuBukaName) Then
                    LblBukaName.Text = headerSubject.ShisakuBukaName
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

                FormUtil.BindLabelValuesToComboBox(cmbBlockNo, headerSubject.BlockNoLabelValues)
                FormUtil.SetComboBoxSelectedValue(cmbBlockNo, headerSubject.ShisakuBlockNo)
            Finally
                IsSuspendEventValueChanged = False
            End Try
        End Sub

        ''' <summary>
        ''' ヘッダー部を初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeHeader()

            ''↓↓2014/09/17 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
            '該当イベント取得
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(Me._shisakuEventCode)
            If Not eventVo.BlockAlertKind = 2 And Not eventVo.KounyuShijiFlg = "0" Then
                btnBaseInfo.Enabled = False
            End If
            ''↑↑2014/09/17 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END

            If _dispMode = VIEW_MODE Then
                BtnRegister.Enabled = False
                BtnSave.Enabled = False
                TxtTel.Enabled = False

                txtNaiyou.Enabled = False '２次改修で追加

                lblDispMode.Visible = True
                If _strMode = ShishakuKanryoViewMode Then    '*****完了閲覧モード対応*****
                    BtnCall.Visible = False
                End If
            Else
                BtnRegister.Enabled = True
                BtnSave.Enabled = True
                TxtTel.Enabled = True

                txtNaiyou.Enabled = True '２次改修で追加

                lblDispMode.Visible = False
            End If
            ShisakuFormUtil.setTitleVersion(Me)

            ShisakuFormUtil.SettingDefaultProperty(cmbBlockNo)
            cmbBlockNo.MaxLength = 4
        End Sub

        Private Sub InitializeValidator()
            validatorBlockCall = New Validator
            validatorBlockCall.Add(cmbBlockNo, "ブロック№").Required()
        End Sub

        Private Sub cmbBlockNo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBlockNo.SelectedValueChanged
            If headerSubject Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            headerSubject.ShisakuBlockNoSelectedValue = cmbBlockNo.SelectedValue
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
        Private Sub BtnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRegister.Click
            Try
                errorController.ClearBackColor()
                alWindow.AssertValidateRegister()
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

            ''2015/08/17 追加 E.Ubukata Ver 2.10.8
            '' 色記号入力チェック追加
            Try
                alWindow.AssertValidateHinban()
            Catch ex As IllegalInputException
                errorController.SetBackColorOnWarning(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                If frm01Kakunin.ConfirmOkCancel("色記号が入力されていません", "このまま登録しますか？") <> msgBoxResult.Ok Then
                    Return
                End If
            End Try
            Try
                koseiWindow.AssertValidateHinban()
            Catch ex As IllegalInputException
                errorController.SetBackColorOnWarning(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                If frm01Kakunin.ConfirmOkCancel("色記号が入力されていません", "このまま登録しますか？") <> msgBoxResult.Ok Then
                    Return
                End If
            End Try


            '2014/11/13 要望により、ワーニングを一時封印
            ''2015/08/18 解除 E.Ubukata  Ver 2.10.8
            '' ただしチェック内容は変更しています。
            Try
                koseiWindow.AssertValidateMakerCode()
            Catch ex As IllegalInputException
                errorController.SetBackColorOnWarning(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                If frm01Kakunin.ConfirmOkCancel("取引先コードが入力されていません", "このまま登録しますか？") <> msgBoxResult.Ok Then
                    Return
                End If
            End Try

            Try
                koseiWindow.AssertValidateKyoukuSection()

            Catch ex As IllegalInputException
                errorController.SetBackColorOnWarning(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                If frm01Kakunin.ConfirmOkCancel("供給セクションが入力されていません", "このまま登録しますか？") <> msgBoxResult.Ok Then
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

            If frm01Kakunin.ConfirmOkCancel("登録を実行しますか？") <> msgBoxResult.Ok Then
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
            'Dim alDao As New BuhinEditAlDaoImpl
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

            Try
                '↓↓2014/10/29 酒井 ADD BEGIN
                'Ver6_2 1.95以降の修正内容の展開
                koseiWindow.DeleteRowCheck()
                '↑↑2014/10/29 酒井 ADD END

                subject.Register()

                ''---------------------------
                ''---------------------------
                ''２次改修
                ''排他情報をクリア
                'ExclusiveControl()
                ''---------------------------
                ''---------------------------

                '-------------------------------------------
                '２次改修
                'ブロックのアラートを閉じる。
                frm41TourokuKakunin.Close()

                ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 aa) (TES)施 ADD BEGIN
                Frm41KaiteiKakunin.Close()
                ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 aa) (TES)施 ADD END	
                '↓↓2014/10/16 酒井 ADD BEGIN
                frm41SaiKakunin.Close()
                frm41SaiKakuninMin.Close()
                '↑↑2014/10/16 酒井 ADD END
                '-------------------------------------------

                Me.Close()
            Catch ex As TableExclusionException
                ComFunc.ShowInfoMsgBox(ex.Message)
            Finally
                _WasSaveRegister = True ' 排他例外だったとしても、誰かが更新したから
            End Try

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub

        Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
            Try
                errorController.ClearBackColor()

                alWindow.AssertValidateSave()
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

                '---------------------------------------------------------------------------------------------------------
                '---------------------------------------------------------------------------------------------------------
                '２次改修
                '排他処理見直しのため、以下の処理は無効にする。
                ''ここで同期チェック
                ''画面表示時の日付時刻を保持しておく
                'Dim alDao As New BuhinEditAlDaoImpl
                'Dim tempVo As New TShisakuSekkeiBlockVo

                'tempVo = alDao.FindByLastModifyDateTimeOfSekkeiBlock(_shisakuEventCode, _shisakuBukaCode, _shisakuBlockNo, _OpenDateTime)
                'If Not tempVo Is Nothing Then
                '    ComFunc.ShowErrMsgBox("データが更新されました。再度画面を開きなおし編集を行って下さい。")
                '    Return
                'End If

                Try
                    ShisakuCommon.Ui.CursorUtil.SetWaitCursor(1000)
                    subject.Save(frm.Result)

                    aInputWatcher.Clear()
                Catch ex As TableExclusionException
                    ComFunc.ShowInfoMsgBox(ex.Message)
                Finally
                    _WasSaveRegister = True ' 排他例外だったとしても、誰かが更新したから
                End Try

                '---------------------------------------------------------------------------------------------------------
                '---------------------------------------------------------------------------------------------------------
                '２次改修
                ''保存が完了したら画面表示時刻を再取得する。
                ''画面表示時の日付時刻を保持しておく
                'tempVo = alDao.FindByDBTimeStamp(_shisakuEventCode)
                '_OpenDateTime = tempVo.UpdatedDate & tempVo.UpdatedTime

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
        ''' AL作成機能のWindowがアクティブか？を返す
        ''' </summary>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function IsActiveAlWindow() As Boolean
            Return Me.ActiveMdiChild Is alWindow
        End Function

        ''' <summary>
        ''' 現在表示されているSpreadのSheetを返す
        ''' </summary>
        ''' <returns>表示されているSheet</returns>
        ''' <remarks></remarks>
        Private Function GetActiveSheet() As FarPoint.Win.Spread.SheetView
            If IsActiveAlWindow() Then
                Return alWindow.spdParts_Sheet1
            Else
                Return koseiWindow.spdParts_Sheet1
            End If
        End Function

        Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
            Dim blockNo As String = cmbBlockNo.Text

            If _dispMode = EDIT_MODE Then

                Try
                    errorController.ClearBackColor()

                    validatorBlockCall.AssertValidate()
                Catch ex As IllegalInputException
                    errorController.SetBackColorOnError(ex.ErrorControls)
                    errorController.FocusAtFirstControl(ex.ErrorControls)
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox(ex.Message)
                    cmbBlockNo.Text = Me._shisakuBlockNo
                    Return
                End Try

                If aInputWatcher.WasUpdate Then
                    If frm01Kakunin.ConfirmOkCancel("登録されていません。", "変更を破棄しブロックを呼出しますか？") <> MsgBoxResult.Ok Then
                        cmbBlockNo.Text = Me._shisakuBlockNo
                        Return
                    End If
                End If


                ''ブロック呼び出し前にチェックする'
                'Dim impl As Al.Dao.BuhinEditAlDao = New Al.Dao.BuhinEditAlDaoImpl
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

            alWindow.Close()
            alWindow.Dispose()
            subject = Nothing

            koseiWindow.Close()
            koseiWindow.Dispose()

            '---------------------------------------------
            'ここで一度画面を破棄します。
            '---------------------------------------------

            subject = New Logic.BuhinEditSubject(_shisakuEventCode, _shisakuBukaCode, blockNo, LoginInfo.Now, KSMode)
            headerSubject = subject.HeaderSubject
            subject.AddObserver(Me)
            headerSubject.AddObserver(Me)

            ''↓↓2014/08/07 Ⅰ.3.設計編集 ベース車改修専用化_ap) (TES)張 CHG BEGIN
            'alWindow = New Frm41DispShisakuBuhinEdit10(_shisakuEventCode, subject.AlSubject, _dispMode)
            alWindow = New Frm41DispShisakuBuhinEdit10(_shisakuEventCode, subject.AlSubject, subject.KoseiSubject, _dispMode, "Frm41DispShisakuBuhinEdit00")
            ''↑↑2014/08/07 Ⅰ.3.設計編集 ベース車改修専用化_ap) (TES)張 CHG END
            alWindow.MdiParent = Me

            koseiWindow = New Frm41DispShisakuBuhinEdit20(_shisakuEventCode, subject.KoseiSubject, _dispMode)
            koseiWindow.MdiParent = Me

            ''↓↓2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_QA95 (TES)張 ADD BEGIN
            alObserver = alWindow.frmAlObserver
            koseiObserver = koseiWindow.frmKoseiObserver
            ''↑↑2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_QA95 (TES)張 ADD END

            aInputWatcher = New InputWatcher

            InitializeWatcher()
            InitializeHeader()
            InitializeValidator()

            'alWindow = New Frm41DispShisakuBuhinEdit10(_shisakuEventCode, subject.AlSubject)
            'alWindow.MdiParent = Me

            'koseiWindow = New Frm41DispShisakuBuhinEdit20(_shisakuEventCode, subject.KoseiSubject)
            'koseiWindow.MdiParent = Me

            BtnRegister.Enabled = False
            BtnSave.Enabled = False
            TxtTel.Enabled = False

            txtNaiyou.Enabled = False '２次改修で追加

            lblDispMode.Visible = True

            alWindow.ClearInstlColumns()
            koseiWindow.ClearInstlColumns()


            'subject.ApplyBlockNo()
            subject.NotifyObservers()

            'cmbBlockNo.Text = blockNo

            aInputWatcher.Clear()
            ''↓↓2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
            '文字色、大きさもデフォルトに戻す。
            'alWindow.ClearSheetBackColorAll()
            'koseiWindow.ClearSheetBackColorAll()
            ''↑↑2014/09/08 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END


            alWindow.viewLockForViewMode()

            koseiWindow.viewLockForViewMode()

            koseiWindow.Show()
            Me.LayoutMdi(MdiLayout.TileVertical)

            alWindow.Show()
            Me.LayoutMdi(MdiLayout.TileVertical)

            aInputWatcher.Clear()


            '---------------------------
            '---------------------------
            '編集モードから閲覧モードに変更になったら排他情報をクリアする。
            '---------------------------
            '---------------------------
            '２次改修
            '排他情報をクリア
            ExclusiveControl()
            '---------------------------
            '---------------------------

            '---------------------------------------------
            '変更前のブロックを保持する。
            _shisakuBlockNo = cmbBlockNo.Text
            '---------------------------------------------
            '-------------------------------------------
            '２次改修
            'ブロックのアラートを閉じる。
            frm41TourokuKakunin.Close()
            ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 z) (TES)施 ADD BEGIN
            Frm41KaiteiKakunin.Close()
            ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 z) (TES)施 ADD END
            '↓↓2014/10/16 酒井 ADD BEGIN
            frm41SaiKakunin.Close()
            frm41SaiKakuninMin.Close()
            '↑↑2014/10/16 酒井 ADD END
            '-------------------------------------------
            '----------------------------------------
            '２次改修
            'ブロックのアラートを表示する。
            DispAlert()
            '----------------------------------------


        End Sub

        '---------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------
        '２次改修
        ''' <summary>
        ''' 排他制御ブロック情報の更新処理
        ''' </summary>
        ''' <param name="StrEventCode">イベントコード</param>
        ''' <param name="StrDept">部課コード</param>
        ''' <param name="StrBlockNo">ブロック№</param>
        ''' <param name="isMode">「登録」の場合、true</param>
        ''' <remarks></remarks>
        Private Sub RegisterMain(ByVal StrEventCode As String, _
                                 ByVal StrDept As String, _
                                 ByVal StrBlockNo As String, _
                                 ByVal isMode As Boolean)

            Dim aShisakuDate As New ShisakuDate
            Dim blockVo As New TExclusiveControlBlockVo
            Dim blockDao As ExclusiveControlBlockDao = New ExclusiveControlBlockImpl

            'KEY情報
            blockVo.ShisakuEventCode = StrEventCode
            blockVo.ShisakuBukaCode = StrDept
            blockVo.ShisakuBlockNo = StrBlockNo
            '編集情報
            blockVo.EditUserId = LoginInfo.Now.UserId
            blockVo.EditDate = aShisakuDate.CurrentDateDbFormat
            blockVo.EditTime = aShisakuDate.CurrentTimeDbFormat
            '作成情報
            blockVo.CreatedUserId = LoginInfo.Now.UserId
            blockVo.CreatedDate = aShisakuDate.CurrentDateDbFormat
            blockVo.CreatedTime = aShisakuDate.CurrentTimeDbFormat
            '更新情報
            blockVo.UpdatedUserId = LoginInfo.Now.UserId
            blockVo.UpdatedDate = aShisakuDate.CurrentDateDbFormat
            blockVo.UpdatedTime = aShisakuDate.CurrentTimeDbFormat

            '追加モードの場合、インサートする。
            If isMode = True Then
                blockDao.InsetByPk(blockVo)
            Else
                blockDao.UpdateByPk(blockVo)
            End If

        End Sub

        '----------------------------------------------------------------------------------------------------------------
        '２次改修
        Private Sub BtnKakunin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnKakunin.Click
            'ブロックのアラートを表示する。
            DispAlert()
        End Sub
        '----------------------------------------------------------------------------------------------------------------
        ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_n) (TES)張 ADD BEGIN
        Private Sub btnBaseInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaseInfo.Click

            If BASEINFO = "N" Then
                btnBaseInfo.Text = "ベース情報非表示"
                alObserver.BaseInfoColumnVisible()
                koseiObserver.BaseInfoColumnVisible()
                BASEINFO = "D"
            Else
                btnBaseInfo.Text = "ベース情報表示"
                alObserver.BaseInfoColumnDisable()
                koseiObserver.BaseInfoColumnDisable()
                BASEINFO = "N"
            End If

        End Sub
        ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_n) (TES)張 ADD END
        '----------------------------------------------------------------------------------------------------------------
        ''↓↓2014/08/04 Ⅰ.11.改訂戻し機能 x) (TES)施 ADD BEGIN
        Public Sub KaiteiCopyCancel()
            '全列削除を行う'	
            If Not (alObserver Is Nothing) Then
                alObserver.RemoveInstlColumnsAll(1)
            End If
            subject.AlSubject.test()
            subject.AlSubject.BackEvent()
            '構成を元に戻す'																															
            subject.AlSubject.NotifyObservers()
        End Sub

        ''↑↑2014/08/04 Ⅰ.11.改訂戻し機能 x) (TES)施 ADD END

        '----------------------------------------------------------------------------------------------------------------


        '２次改修
        Private Sub DispAlert()
            'ブロックのアラートを表示する。
            '試作イベントコードに該当するアラート情報を取得する。
            Dim EventDao As IShisakuEventDao = New EventSakusei.ShisakuBuhinMenu.Dao.ShisakuEventDaoImpl
            Dim EventAlertInfo = EventDao.GetShisakuEvent(_shisakuEventCode)

            'アラート情報を画面に表示する。
            If StringUtil.IsEmpty(EventAlertInfo.BlockAlertFlg) Then
                'ボタン表示しない
                BtnKakunin.Visible = False
            Else
                '0:表示しない、1：表示する。
                If EventAlertInfo.BlockAlertFlg = "1" Then
                    '初期設定
                    Dim strAlertFlg As String = ""

                    '1:フル組、2：移管車改修。
                    If EventAlertInfo.BlockAlertKind = "1" And EventAlertInfo.KounyuShijiFlg = "0" Then
                        strAlertFlg = "1"
                    Else
                        strAlertFlg = "2"
                    End If

                    '試作イベントコードに該当するアラート情報を画面に表示する。
                    Dim AlertInfo = EventDao.GetBlockCheckAlertInfo(_shisakuBlockNo, strAlertFlg)
                    If AlertInfo IsNot Nothing Then
                        If StringUtil.IsNotEmpty(AlertInfo.Naiyou) Then

                            '改行コードを把握する。


                            '始めの位置を探す
                            Dim foundIndex As Integer = AlertInfo.Naiyou.IndexOf(vbLf)
                            Dim searchWord As String = vbLf
                            Dim intIchi(100) As Integer
                            Dim strNaiyou As String
                            Dim i As Integer = 0


                            If foundIndex <> -1 Then
                                strNaiyou = Mid(AlertInfo.Naiyou, 1, foundIndex)
                            Else
                                strNaiyou = AlertInfo.Naiyou
                            End If

                            While 0 <= foundIndex

                                '見つかった位置を保存する。
                                i = foundIndex

                                '次の検索開始位置
                                Dim nextIndex As Integer = foundIndex + searchWord.Length
                                If nextIndex < AlertInfo.Naiyou.Length Then
                                    '次の位置を探す
                                    foundIndex = AlertInfo.Naiyou.IndexOf(vbLf, nextIndex)
                                Else
                                    '最後まで検索したときは終わる
                                    Exit While
                                End If

                                '次の検索位置まで値を設定する。
                                If foundIndex = -1 Then
                                    foundIndex = AlertInfo.Naiyou.Length
                                End If
                                strNaiyou += vbCrLf & Mid(AlertInfo.Naiyou, i + 1, foundIndex - i)

                            End While

                            frm41TourokuKakunin.txtKakunin3.Text = strNaiyou
                            frm41TourokuKakunin.Show()
                            'ボタン表示する
                            BtnKakunin.Visible = True
                        End If
                    Else
                        'ボタン表示しない
                        BtnKakunin.Visible = False
                    End If

                End If
            End If

        End Sub
        '----------------------------------------------------------------------------------------------------------------
        '↓↓2014/10/16 酒井 ADD BEGIN
        Private Sub DispSai()
            btnSai.Text = "仕様差異　非表示"
            btnSai.Enabled = True
            If frm41SaiKakunin.GetSai(Me._shisakuEventCode) Then
                frm41SaiKakunin.frm41 = Me
                frm41SaiKakunin.frm20 = Nothing
                frm41SaiKakuninMin.frm41 = Me
                frm41SaiKakuninMin.frm20 = Nothing
                frm41SaiKakunin.Show()
            End If
        End Sub
        Private Sub btnSai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSai.Click
            If btnSai.Text = "仕様差異　非表示" Then
                btnSai.Text = "仕様差異　表示"
                frm41SaiKakunin.Hide()
                '↓↓2014/10/29 酒井 ADD BEGIN
                frm41SaiKakuninMin.Location = New Point(frm41SaiKakunin.Left + frm41SaiKakunin.Width - frm41SaiKakuninMin.Width, frm41SaiKakunin.Top)
                '↑↑2014/10/29 酒井 ADD END
                frm41SaiKakuninMin.Show()
            Else
                btnSai.Text = "仕様差異　非表示"
                '↓↓2014/10/29 酒井 ADD BEGIN
                If frm41SaiKakuninMin.Left + frm41SaiKakuninMin.Width - frm41SaiKakunin.Width >= 0 Then
                    frm41SaiKakunin.Location = New Point(frm41SaiKakuninMin.Left + frm41SaiKakuninMin.Width - frm41SaiKakunin.Width, frm41SaiKakuninMin.Top)
                Else
                    frm41SaiKakunin.Location = New Point(0, frm41SaiKakuninMin.Top)
                End If
                '↑↑2014/10/29 酒井 ADD END
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

    End Class
End Namespace
