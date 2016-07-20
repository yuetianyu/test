Imports EBom.Common
Imports EventSakusei.TehaichoSakusei
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui.Access
Imports ShisakuCommon.Db

Imports EventSakusei.TehaichoSakusei.Logic
Imports EventSakusei.TehaichoSakusei.Dao
Imports ShisakuCommon.Ui.Valid


Namespace TehaichoSakusei

    Public Class frm18DispTehaichoSakusei : Implements Observer

        Private tehaiSubject As TehaichoSakusei.Logic.TehaichoSakusei
        Private IsSuspendTehaiValueChanged As Boolean
        Private ReadOnly aInputWatcher As InputWatcher


        Private Vo As List(Of TehaichoSakuseiVo)

        Public Sub New(ByVal eventCode As String)
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)

            tehaiSubject = New Logic.TehaichoSakusei(LoginInfo.Now, eventCode)

            tehaiSubject.AddObserver(Me)

            aInputWatcher = New InputWatcher

            Initialize()

            InitializeWatcher()

            InitializeValidatorTehai()

            tehaiSubject.NotifyObservers()

            aInputWatcher.Clear()

        End Sub

        '初期設定'
        Private Sub Initialize()
            ShisakuFormUtil.setTitleVersion(Me)
            LblPG_ID.Text = "PG-ID :" + PROGRAM_ID_18
            ShisakuFormUtil.SetDateTimeNow(Me.LblDateNow, Me.LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(Me.LblUserId, Me.LblBukaName)

            'グループNoの初期化'
            ShisakuFormUtil.SettingDefaultProperty(cmbGroupNo)

            '製作区分の初期化'
            ShisakuFormUtil.SettingDefaultProperty(cmbSeihinKbn)

            'ユニット区分の初期化'
            ShisakuFormUtil.SettingDefaultProperty(cmbUnitKbn)

            ShisakuFormUtil.SettingDefaultProperty(txtKoujiKbn)
            ShisakuFormUtil.SettingDefaultProperty(txtEventName)
            ShisakuFormUtil.SettingDefaultProperty(txtKoujiNo)
            ShisakuFormUtil.SettingDefaultProperty(txtKoujiShireiNo)

        End Sub

        Private Sub InitializeWatcher()

            aInputWatcher.Add(cmbGroupNo)
            aInputWatcher.Add(cmbSeihinKbn)
            aInputWatcher.Add(cmbUnitKbn)
            aInputWatcher.Add(txtEventName)
            aInputWatcher.Add(txtKoujiKbn)
            aInputWatcher.Add(txtKoujiNo)
            aInputWatcher.Add(txtKoujiShireiNo)
        End Sub

        ''' <summary>
        ''' 表示値を更新する
        ''' </summary>
        ''' <param name="observable">呼び出し元のObservable</param>
        ''' <param name="arg">引数</param>
        ''' <remarks></remarks>
        Public Sub UpdateObserver(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update

            UpdateHeader(observable, arg)

        End Sub

        Private Sub frm18DispTehaichoSakusei_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        End Sub

        ''' <summary>
        ''' ヘッダー部の表示値を更新する
        ''' </summary>
        ''' <param name="observable">呼び出し元Observable</param>
        ''' <param name="arg">引数</param>
        ''' <remarks></remarks>
        Private Sub UpdateHeader(ByVal observable As Observable, ByVal arg As Object)
            IsSuspendTehaiValueChanged = True
            Try
                FormUtil.BindLabelValuesToComboBox(cmbGroupNo, tehaiSubject.GroupNoLabelValues, False)
                FormUtil.SetComboBoxSelectedValue(cmbGroupNo, tehaiSubject.GroupNo)

                FormUtil.BindLabelValuesToComboBox(cmbSeihinKbn, tehaiSubject.SeihinKbnLabelValues, False)
                'FormUtil.SetComboBoxSelectedValue(cmbSeihinKbn, tehaiSubject.SeihinKbn)

                FormUtil.BindLabelValuesToComboBox(cmbUnitKbn, tehaiSubject.UnitKbnLabelValues, True)
                FormUtil.SetComboBoxSelectedValue(cmbUnitKbn, tehaiSubject.UnitKbn)

                txtEventName.Text = tehaiSubject.EventName

                txtKoujiKbn.Text = tehaiSubject.KoujiKbn
                txtKoujiShireiNo.Text = tehaiSubject.KoujiShireiNo
                txtKoujiNo.Text = tehaiSubject.KoujiNo

                'LockHeaderIfViewerChange()
            Finally
                IsSuspendTehaiValueChanged = False
            End Try

        End Sub

#Region "テキストの入力規制"

        ''' <summary>
        ''' テキストに全角文字が入力されているかを返す
        ''' </summary>
        ''' <param name="text">入力文字</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function text_validate(ByVal text As Char) As Boolean
            Dim moji As Integer = Len(text)
            Dim bytecount As Integer = System.Text.Encoding.GetEncoding("Shift-JIS").GetByteCount(text.ToString())
            If moji <> bytecount Then
                Return False
            End If
            Return True
        End Function

        '工事区分の入力規制'
        Private Sub txtKoujiKbn_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKoujiKbn.KeyPress
            If text_validate(e.KeyChar) Then
                e.Handled = True
            End If
        End Sub

        '工事Noの入力規制'
        Private Sub txtKoujiNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKoujiNo.KeyPress
            If text_validate(e.KeyChar) Then
                e.Handled = True
            End If
        End Sub

        '工事指令Noの入力規制'
        Private Sub txtKoujiShireiNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKoujiShireiNo.KeyPress
            If text_validate(e.KeyChar) Then
                e.Handled = True
            End If
        End Sub

#End Region

#Region "ボタンの処理"
        '戻るボタンの処理'
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Me.Close()
        End Sub

        '終了ボタンの処理'
        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            Application.Exit()
            System.Environment.Exit(0)
        End Sub

        Private errorController As New ErrorController()

        '作成ボタンの処理'
        Private Sub btnADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnADD.Click
            '各項目のチェックの前にデータを入れる'
            RbtnJIkyuhinSuru.BackColor = Nothing
            RbtnJIkyuhinShinai.BackColor = Nothing
            RbtnHikakuOrikomiShinai.BackColor = Nothing
            RbtnHikakuOrikomiSuru.BackColor = Nothing
            RbtnSyukeiTenkaiShinai.BackColor = Nothing
            RbtnSyukeiTenkaiSuru.BackColor = Nothing

            errorController.ClearBackColor()

            Try
                validatorTehai.AssertValidate()

            Catch ex As IllegalInputException
                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)

                Return
            End Try
            'ラジオボタンのチェック'
            If RbtnJIkyuhinSuru.Checked = False And RbtnJIkyuhinShinai.Checked = False Then
                RbtnJIkyuhinSuru.BackColor = Color.Red
                RbtnJIkyuhinShinai.BackColor = Color.Red
                RbtnJIkyuhinSuru.Focus()

                ComFunc.ShowErrMsgBox("自給品の消しこみのする/しないを選択してください")
                Return
            End If
            If RbtnHikakuOrikomiSuru.Checked = False And RbtnHikakuOrikomiShinai.Checked = False Then

                RbtnHikakuOrikomiSuru.BackColor = Color.Red
                RbtnHikakuOrikomiShinai.BackColor = Color.Red
                RbtnJIkyuhinSuru.Focus()

                ComFunc.ShowErrMsgBox("比較結果織込みのする/しないを選択してください")
                Return

            ElseIf RbtnHikakuOrikomiSuru.Checked = False Then
                If RbtnSyukeiTenkaiSuru.Checked = False And RbtnSyukeiTenkaiShinai.Checked = False Then

                    RbtnHikakuOrikomiSuru.BackColor = Color.Red
                    RbtnHikakuOrikomiShinai.BackColor = Color.Red
                    RbtnJIkyuhinSuru.Focus()

                    ComFunc.ShowErrMsgBox("集計コードからの展開のする/しないを選択してください ")
                    Return
                End If
            End If

            '各ラジオボタンの値を取得する'
            tehaiSubject.JikyuSuru = RbtnJIkyuhinSuru.Checked
            tehaiSubject.JikyuShinai = RbtnJIkyuhinShinai.Checked
            tehaiSubject.OrikomiSuru = RbtnHikakuOrikomiSuru.Checked
            tehaiSubject.OrikomiShinai = RbtnHikakuOrikomiShinai.Checked
            tehaiSubject.SyukeiSuru = RbtnSyukeiTenkaiSuru.Checked
            tehaiSubject.SyukeiShinai = RbtnSyukeiTenkaiShinai.Checked


            If frm01Kakunin.ConfirmOkCancel("手配帳を作成します") <> MsgBoxResult.Ok Then
                Return
            End If


            'frm01Kakunin.lblKakunin.Text = "手配帳を作成します。"
            'frm18Para = "btnADD"
            'frm01Kakunin.ShowDialog()
            'Select Case frm18ParaModori
            '    Case "1"
            '        Me.Close()
            '        Return
            'End Select
            tehaiSubject.Register()

            'Try
            '    '画面を綺麗に、実行中のカーソルへ変更。
            '    Application.DoEvents()
            '    Cursor.Current = Cursors.WaitCursor

            '    tehaiSubject.Register()

            'Catch ex As TableExclusionException
            '    ComFunc.ShowInfoMsgBox(ex.Message)
            'Finally
            '    _WasSaveRegister = True ' 排他例外だったとしても、誰かが更新したから
            'End Try

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

            '終わったら閉じる'
            Me.Close()
        End Sub

#End Region
        '?????'
        Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

        End Sub

        Private validatorTehai As Validator

        Private Sub InitializeValidatorTehai()

            validatorTehai = New Validator

            Dim GroupNoRequired As New Validator("グループNoを入力してください。")
            GroupNoRequired.Add(cmbGroupNo).Required()
            Dim KoujiKbnRequired As New Validator("工事区分を入力してください。")
            KoujiKbnRequired.Add(txtKoujiKbn).Required()
            Dim SeihinKbnRequired As New Validator("製品区分を入力してください。")
            SeihinKbnRequired.Add(cmbSeihinKbn).Required()
            Dim UnitKbnRequired As New Validator("ユニット区分を入力してください。")
            UnitKbnRequired.Add(cmbUnitKbn).Required()
            Dim KoujiShireiNoRequired As New Validator("工事指令Noを入力してください。")
            KoujiShireiNoRequired.Add(txtKoujiShireiNo).Required()
            Dim KoujiNoRequired As New Validator("工事Noを入力してください。")
            KoujiNoRequired.Add(txtKoujiNo).Required()
            Dim EventNameRequired As New Validator("イベント名称を入力してださい。")
            EventNameRequired.Add(txtEventName).Required()

            Dim KoujiKbnRange As New Validator()
            KoujiKbnRange.Add(txtKoujiKbn).RangeLengthByte(2, 3, "工事区分は2文字入力してください。")
            Dim KoujiShireiNoRange As New Validator()
            KoujiShireiNoRange.Add(txtKoujiShireiNo).RangeLengthByte(3, 4, "工事指令Noは3文字入力してください。")

            validatorTehai.Add(GroupNoRequired)
            validatorTehai.Add(KoujiKbnRequired)
            validatorTehai.Add(SeihinKbnRequired)
            validatorTehai.Add(UnitKbnRequired)
            validatorTehai.Add(KoujiShireiNoRequired)
            validatorTehai.Add(KoujiNoRequired)
            validatorTehai.Add(EventNameRequired)
            validatorTehai.Add(KoujiKbnRange)
            validatorTehai.Add(KoujiShireiNoRange)

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

        'チェックボタンの処理'
        Private Sub RbtnHikakuOrikomiSuru_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtnHikakuOrikomiSuru.CheckedChanged
            '「比較結果織込み」：する　　の場合は、自動的に「集計コードからの展開」：する　　にする。
            Select Case RbtnHikakuOrikomiSuru.Checked
                Case True
                    RbtnSyukeiTenkaiSuru.Checked = True
                    RbtnSyukeiTenkaiShinai.Checked = False
                    Panel6.Enabled = False
                Case False
            End Select
            '「比較結果織込み」：しない　の場合は、「集計コードからの展開」の選択ができる。
            Select Case RbtnHikakuOrikomiShinai.Checked
                Case True
                    RbtnSyukeiTenkaiSuru.Checked = False
                    RbtnSyukeiTenkaiShinai.Checked = False
                    Panel6.Enabled = True
                Case False
            End Select
        End Sub

        '自給品するの場合'
        Private Sub RbtnJikyuSuru_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtnJIkyuhinSuru.CheckedChanged
            Select Case RbtnJIkyuhinSuru.Checked
                Case True
                    RbtnJIkyuhinSuru.Checked = True
                    RbtnJIkyuhinShinai.Checked = False
                Case False
                    RbtnJIkyuhinSuru.Checked = False
                    RbtnJIkyuhinShinai.Checked = True
            End Select
            Select Case RbtnJIkyuhinShinai.Checked
                Case True
                    RbtnJIkyuhinSuru.Checked = False
                    RbtnJIkyuhinShinai.Checked = True
                Case False
                    RbtnJIkyuhinSuru.Checked = True
                    RbtnJIkyuhinShinai.Checked = False
            End Select
        End Sub

        Private Sub RbtnJikyuShinai_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtnJIkyuhinShinai.CheckedChanged
            Select Case RbtnJIkyuhinShinai.Checked
                Case True
                    RbtnJIkyuhinSuru.Checked = False
                    RbtnJIkyuhinShinai.Checked = True
                Case False
                    RbtnJIkyuhinSuru.Checked = True
                    RbtnJIkyuhinShinai.Checked = False
            End Select
            Select Case RbtnJIkyuhinSuru.Checked
                Case True
                    RbtnJIkyuhinSuru.Checked = True
                    RbtnJIkyuhinShinai.Checked = False
                Case False
                    RbtnJIkyuhinSuru.Checked = False
                    RbtnJIkyuhinShinai.Checked = True
            End Select
        End Sub


#Region "各チェンジイベント"

        'グループNoの処理'
        Private Sub cmbGroupNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroupNo.SelectedIndexChanged
            If Not tehaiSubject Is Nothing Then
                tehaiSubject.GroupNo = cmbGroupNo.SelectedValue
            End If
        End Sub

        '工事区分の処理'
        Private Sub txtKoujiKbn_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKoujiKbn.TextChanged
            If Not tehaiSubject Is Nothing Then
                tehaiSubject.KoujiKbn = txtKoujiKbn.Text
            End If
        End Sub

        '製品区分の処理'
        Private Sub cmbSeihinKbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSeihinKbn.SelectedIndexChanged
            If Not tehaiSubject Is Nothing Then
                cmbGroupNo.Text = cmbGroupNo.SelectedValue
                tehaiSubject.SeihinKbn = cmbSeihinKbn.Text
            End If
        End Sub

        'ユニット区分の処理'
        Private Sub cmbUnitKbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnitKbn.SelectedIndexChanged
            If Not tehaiSubject Is Nothing Then
                cmbGroupNo.Text = cmbGroupNo.SelectedValue
                tehaiSubject.UnitKbn = cmbUnitKbn.Text
            End If
        End Sub

        '工事指令Noの処理'
        Private Sub txtKoujiShireiNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKoujiShireiNo.TextChanged
            If Not tehaiSubject Is Nothing Then
                tehaiSubject.KoujiShireiNo = txtKoujiShireiNo.Text
            End If
        End Sub

        '工事Noの処理'
        Private Sub txtKoujiNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKoujiNo.TextChanged
            If Not tehaiSubject Is Nothing Then
                tehaiSubject.KoujiNo = txtKoujiNo.Text
            End If
        End Sub

        'イベント名称の処理'
        Private Sub txtEventName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEventName.TextChanged
            If Not tehaiSubject Is Nothing Then
                tehaiSubject.EventName = txtEventName.Text
            End If
        End Sub

        '時計の処理'
        Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub

#End Region


    End Class
End Namespace
