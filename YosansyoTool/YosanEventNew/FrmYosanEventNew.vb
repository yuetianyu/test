Imports EBom.Data
Imports EBom.Common
Imports EventSakusei
Imports ShisakuCommon
Imports ShisakuCommon.Db
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Util
Imports ShisakuCommon.Util.LabelValue
Imports YosansyoTool.YosanEventNew.Logic

Namespace YosanEventNew

    Public Class FrmYosanEventNew : Implements Observer

#Region " メンバー変数 "
        ''' <summary>ロジック</summary>
        Private m_EventNew As DispYosanEventNew = Nothing
        ''' <summary>ヘッダーロジック</summary>
        Private m_EventNewHead As DispYosanEventNewHeader = Nothing

        Private IsSuspendEventValueChanged As Boolean
        '''<summary>入力有無判定に使用</summary>>
        Private _aInputWatcher As InputWatcher

#End Region

#Region " プロパテイー "
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
#End Region

        ''' <summary>
        ''' エラー項目の制御クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private errorController As New ErrorController()

#Region " コンストラクタ "
        Public Sub New(ByVal eventCode As String)

            Application.DoEvents()

            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)

            m_EventNew = New DispYosanEventNew(Me, eventCode, LoginInfo.Now)
            m_EventNewHead = m_EventNew.HeaderSubject
            m_EventNew.AddObserver(Me)
            m_EventNewHead.AddObserver(Me)

            _aInputWatcher = New InputWatcher

            InitializeWatcher()
            m_EventNew.InitView()

            m_EventNew.NotifyObservers()

        End Sub

        ''' <summary>
        ''' 入力監視を初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeWatcher()
            _aInputWatcher.Add(cmbKubun)
            _aInputWatcher.Add(cmbKaihatsuFugo)
            _aInputWatcher.Add(cmbEvent)
            _aInputWatcher.Add(txtEventName)
            _aInputWatcher.Add(txtYosanCode)
            _aInputWatcher.Add(txtMainHenkoGaiyo)
            _aInputWatcher.Add(txtTsukurikataSeisakujyoken)
            _aInputWatcher.Add(txtSonota)
        End Sub
#End Region

#Region " 表示値更新 "
        ''' <summary>
        ''' 表示値を更新する
        ''' </summary>
        ''' <param name="observable">呼び出し元のObservable</param>
        ''' <param name="arg">引数</param>
        ''' <remarks></remarks>
        Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            If observable Is m_EventNewHead Then
                UpdateHeader(observable, arg)
            Else
                m_EventNewHead.NotifyObservers(arg)
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
                FormUtil.BindLabelValuesToComboBox(cmbKubun, m_EventNewHead.KubunLabelValues, True)
                'マスタに存在する値だけセットされれば良いのか？ 
                FormUtil.SetComboBoxSelectedValue(cmbKubun, m_EventNewHead.Kubun)
                cmbKubun.Text = m_EventNewHead.Kubun

                FormUtil.BindLabelValuesToComboBox(cmbKaihatsuFugo, m_EventNewHead.KaihatsuFugoLabelValues, True)
                'マスタに存在する値だけセットされれば良いのか？ 
                FormUtil.SetComboBoxSelectedValue(cmbKaihatsuFugo, m_EventNewHead.KaihatsuFugo)
                cmbKaihatsuFugo.Text = m_EventNewHead.KaihatsuFugo

                FormUtil.BindLabelValuesToComboBox(cmbEvent, m_EventNewHead.EventCodeLabelValues, True)
                'マスタに存在する値だけセットされれば良いのか？ 
                FormUtil.SetComboBoxSelectedValue(cmbEvent, m_EventNewHead.YosanEvent)
                cmbEvent.Text = m_EventNewHead.YosanEvent

                'FormUtil.BindLabelValuesToComboBox(cmbKubun, m_EventNewHead.KubunLabelValues, True)
                'マスタに存在する値だけセットされれば良いのか？ 
                FormUtil.SetComboBoxSelectedValue(cmbKubun, m_EventNewHead.Kubun)
                cmbKubun.Text = m_EventNewHead.Kubun

                txtEventName.Text = m_EventNewHead.EventName
                txtYosanCode.Text = m_EventNewHead.yosanCode
                txtMainHenkoGaiyo.Text = m_EventNewHead.yosanMainHenkoGaiyo
                txtTsukurikataSeisakujyoken.Text = m_EventNewHead.yosanTsukurikataSeisakujyoken
                txtSonota.Text = m_EventNewHead.yosanSonota

            Finally
                IsSuspendEventValueChanged = False
            End Try

        End Sub
#End Region

#Region "Subject(Header)へ変更を通知"

        ''' <summary>
        ''' 区分
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub cmbKubun_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKubun.Leave
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.Kubun = cmbKubun.Text
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' 開発符号
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub cmbKaihatsuFugo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKaihatsuFugo.Leave
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.KaihatsuFugo = cmbKaihatsuFugo.Text

            m_EventNewHead.NotifyObservers()

        End Sub

        ''' <summary>
        ''' イベント
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub cmbEvent_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEvent.Leave
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.YosanEventPhase = cmbEvent.SelectedValue
            m_EventNewHead.YosanEvent = cmbEvent.Text
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' イベント名
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub txtEventName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEventName.Leave
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.EventName = txtEventName.Text
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' 予算コード
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub txtYosanCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtYosanCode.Leave
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.yosanCode = txtYosanCode.Text
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' 主な変更概要
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub txtTsukurikataSeisakujyoken_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTsukurikataSeisakujyoken.Leave
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.yosanTsukurikataSeisakujyoken = txtTsukurikataSeisakujyoken.Text
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' 造り方及び製作条件
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub txtMainHenkoGaiyo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMainHenkoGaiyo.Leave
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.yosanMainHenkoGaiyo = txtMainHenkoGaiyo.Text
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' その他
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub txtSonota_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSonota.Leave
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.yosanSonota = txtSonota.Text
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' 区分
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub cmbKubun_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKubun.SelectedValueChanged
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.Kubun = cmbKubun.Text
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' 開発符号
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub cmbKaihatsuFugo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKaihatsuFugo.SelectedValueChanged
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.KaihatsuFugo = cmbKaihatsuFugo.Text

            cmbEvent.Text = ""

            'イベント
            m_EventNewHead.EventCodeLabelValues = m_EventNewHead.GetLabelValues_Event(m_EventNewHead.KaihatsuFugo)

            m_EventNewHead.NotifyObservers()

        End Sub

        ''' <summary>
        ''' イベント
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub cmbEvent_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEvent.SelectedValueChanged
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.YosanEvent = cmbEvent.SelectedValue
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' イベント名
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub txtEventName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.EventName = txtEventName.Text
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' 予算コード
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub txtYosanCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.yosanCode = txtYosanCode.Text
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' 主な変更概要
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub txtMainHenkoGaiyo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.yosanMainHenkoGaiyo = txtMainHenkoGaiyo.Text
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' 造り方及び製作条件
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub txtTsukurikataSeisakujyoken_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.yosanTsukurikataSeisakujyoken = txtTsukurikataSeisakujyoken.Text
            m_EventNewHead.NotifyObservers()
        End Sub

        ''' <summary>
        ''' その他
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub txtSonota_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If m_EventNewHead Is Nothing OrElse IsSuspendEventValueChanged Then
                Return
            End If
            m_EventNewHead.yosanSonota = txtSonota.Text
            m_EventNewHead.NotifyObservers()
        End Sub

#End Region
        ''' <summary>
        ''' 画面初期化
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Frm46DispShisakuBuhinList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                Initialize()
                _aInputWatcher.Clear()
            Catch ex As Exception
                Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
                Throw
            End Try
        End Sub

        Private Sub Initialize()

            '画面制御
            If m_EventNew.IsAddMode Then
                '新規の場合
                btnRegister.Text = "登録"
                LblCurrPGName.Text = "イベント新規作成"
                cmbEvent.Enabled = True
                cmbKaihatsuFugo.Enabled = True
                'cmbKanseisha.Text = ""
                'cmbWb.Text = ""
                cmbKubun.Text = ""
                cmbKaihatsuFugo.Text = ""
                cmbEvent.Text = ""
                txtEventName.Text = ""
                txtYosanCode.Text = ""
                txtMainHenkoGaiyo.Text = ""
                txtTsukurikataSeisakujyoken.Text = ""
                txtSonota.Text = ""
            Else
                '編集の場合
                btnRegister.Text = "保存"
                LblCurrPGName.Text = "イベント編集"
                cmbEvent.Enabled = False
                cmbKaihatsuFugo.Enabled = False
            End If

        End Sub

        ''' <summary>
        ''' 戻るボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

            If Not _aInputWatcher.WasUpdate Then
                Me.Close()
                Return
            End If

            Dim sMessage As String = "画面を閉じますか？"

            If m_EventNew.IsAddMode Then
                sMessage = "入力を破棄して、" & sMessage
            Else
                sMessage = "変更を破棄して、" & sMessage
            End If

            If frm01Kakunin.ConfirmOkCancel(sMessage, "") = MsgBoxResult.Ok Then
                If m_EventNew.RegistError Then
                    _WasSaveRegister = False
                End If
                Me.Close()
                Return
            End If

        End Sub

        ''' <summary>
        ''' 登録ボタンの処理．
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click

            If Not Validator() Then
                Return
            End If

            If m_EventNew.IsAddMode() Then

                Try
                    '入力した開発符号、リストコードは既に登録されするかどうかをチェック
                    If m_EventNew.IsExist() Then
                        ''エラーメッセージ
                        ComFunc.ShowErrMsgBox("予算イベントコードが重複しています。")
                        Return
                    End If
                Catch ex As Exception
                    ComFunc.ShowInfoMsgBox(ex.Message)
                    Return
                End Try

                If frm01Kakunin.ConfirmOkCancel("登録を実行しますか？") <> MsgBoxResult.Ok Then
                    Return
                End If

                Try
                    '通常登録処理
                    m_EventNew.Register()
                Catch ex As TableExclusionException
                    ComFunc.ShowInfoMsgBox(ex.Message)
                Finally
                    _WasSaveRegister = True ' 排他例外だったとしても、誰かが更新したから
                End Try

                If Not m_EventNew.RegistError Then
                    ComFunc.ShowInfoMsgBox("登録が完了しました。")
                    Me.Close()
                End If
            Else
                If frm01Kakunin.ConfirmOkCancel("保存を実行しますか？") <> MsgBoxResult.Ok Then
                    Return
                End If

                Try
                    '通常更新処理
                    m_EventNew.Save()
                Catch ex As TableExclusionException
                    ComFunc.ShowInfoMsgBox(ex.Message)
                Finally
                    _WasSaveRegister = True ' 排他例外だったとしても、誰かが更新したから
                End Try

                If Not m_EventNew.RegistError Then
                    ComFunc.ShowInfoMsgBox("保存が完了しました。")
                    Me.Close()
                End If
            End If

        End Sub

        ''' <summary>
        ''' 登録／保存時の入力チェック
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function Validator() As Boolean

            cmbKaihatsuFugo.BackColor = Nothing
            cmbEvent.BackColor = Nothing
            cmbKubun.BackColor = Nothing
            txtEventName.BackColor = Nothing
            txtYosanCode.BackColor = Nothing
            txtMainHenkoGaiyo.BackColor = Nothing
            txtTsukurikataSeisakujyoken.BackColor = Nothing
            txtSonota.BackColor = Nothing

            If m_EventNew.IsAddMode Then

                '区分コンボボックス
                If String.IsNullOrEmpty(cmbKubun.Text) Then
                    cmbKubun.BackColor = Color.Red
                    cmbKubun.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("区分を入力して下さい。")
                    Return False
                End If
                If LenB(cmbKubun.Text) > 10 Then
                    cmbKubun.BackColor = Color.Red
                    cmbKubun.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("区分は半角10文字、全角5文字以内で入力して下さい。")
                    Return False
                End If

                '開発符号コンボボックス
                If String.IsNullOrEmpty(cmbKaihatsuFugo.Text) Then
                    cmbKaihatsuFugo.BackColor = Color.Red
                    cmbKaihatsuFugo.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("開発符号を選択して下さい。")
                    Return False
                End If
                Dim flag As Boolean = False
                For Each vo As LabelValueVo In m_EventNewHead.KaihatsuFugoLabelValues
                    If vo.Label.Equals(cmbKaihatsuFugo.Text) Then
                        flag = True
                    End If
                Next
                If flag = False Then
                    cmbKaihatsuFugo.BackColor = Color.Red
                    cmbKaihatsuFugo.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("開発符号を選択して下さい。。")
                    Return False
                End If

                'イベントコンボボックス
                If String.IsNullOrEmpty(cmbEvent.Text) Then
                    cmbEvent.BackColor = Color.Red
                    cmbEvent.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("イベントを入力して下さい。")
                    Return False
                End If
                If LenB(cmbEvent.Text) > 20 Then
                    cmbEvent.BackColor = Color.Red
                    cmbEvent.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("イベントは半角20文字、全角10文字以内で入力して下さい。")
                    Return False
                End If
                'If cmbEvent.Text.Length > 18 Then
                '    cmbEvent.BackColor = Color.Red
                '    cmbEvent.Focus()
                '    ''エラーメッセージ
                '    ComFunc.ShowErrMsgBox("イベントは、最大18桁半角英数字を入力して下さい。")
                '    Return False
                'End If

                'イベント名称テキストボックス
                If String.IsNullOrEmpty(txtEventName.Text) Then
                    txtEventName.BackColor = Color.Red
                    txtEventName.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("イベント名称を入力して下さい。")
                    Return False
                End If
                If LenB(txtEventName.Text) > 100 Then
                    txtEventName.BackColor = Color.Red
                    txtEventName.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("イベント名称は半角100文字、全角50文字以内で入力して下さい。")
                    Return False
                End If

                '予算コードテキストボックス
                If String.IsNullOrEmpty(txtYosanCode.Text) Then
                    txtYosanCode.BackColor = Color.Red
                    txtYosanCode.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("予算コードを入力して下さい。")
                    Return False
                End If
                If LenB(txtYosanCode.Text) > 2 Then
                    txtYosanCode.BackColor = Color.Red
                    txtYosanCode.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("予算コードは半角2文字以内で入力して下さい。")
                    Return False
                End If

                '主な変更概要テキストボックス
                If LenB(txtMainHenkoGaiyo.Text) > 256 Then
                    txtMainHenkoGaiyo.BackColor = Color.Red
                    txtMainHenkoGaiyo.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("主な変更概要は半角256文字、全角128文字以内で入力して下さい。")
                    Return False
                End If

                '造り方及び製作条件テキストボックス
                If LenB(txtTsukurikataSeisakujyoken.Text) > 256 Then
                    txtTsukurikataSeisakujyoken.BackColor = Color.Red
                    txtTsukurikataSeisakujyoken.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("造り方及び製作条件は半角256文字、全角128文字以内で入力して下さい。")
                    Return False
                End If

                'その他テキストボックス
                If LenB(txtSonota.Text) > 256 Then
                    txtSonota.BackColor = Color.Red
                    txtSonota.Focus()
                    ''エラーメッセージ
                    ComFunc.ShowErrMsgBox("その他は半角256文字、全角128文字以内で入力して下さい。")
                    Return False
                End If

            End If

            Return True

        End Function

#Region "　LenB メソッド　"

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     半角 1 バイト、全角 2 バイトとして、指定された文字列のバイト数を返します。</summary>
        ''' <param name="stTarget">
        '''     バイト数取得の対象となる文字列。</param>
        ''' <returns>
        '''     半角 1 バイト、全角 2 バイトでカウントされたバイト数。</returns>
        ''' -----------------------------------------------------------------------------------------
        Public Shared Function LenB(ByVal stTarget As String) As Integer
            Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(stTarget)
        End Function
#End Region

        ''' <summary>フォームクローズ</summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub FrmEventNew_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
            Try
                'm_EventNew.DeleteExclusiveEvent()
            Catch ex As Exception
                Me.Close()  '' フォーム表示中の例外なので、自身を閉じて例外throw
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' タイマーコントロール
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub

    End Class

End Namespace
