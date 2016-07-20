Imports EBom.Common
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui.Access
Imports ShisakuCommon.Db

Imports EventSakusei.TehaichoEdit
Imports EventSakusei.TehaichoMenu
Imports EventSakusei.ShisakuBuhinEditEvent.Dao
Imports EventSakusei.TehaichoEdit.Dao
Imports EventSakusei.TehaichoMenu.Dao
Imports EventSakusei.TehaichoMenu.Impl

Namespace TehaichoMenu


    ''' <summary>
    ''' 部品構成表示画面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class frm19DispTehaichoMenu

        Private subject As TehaichoMenu.Logic.Frm19TehaichoMenu
        Private aList As TehaichoMenu.Dao.TehaichoMenuDao = New TehaichoMenu.Impl.TehaichoMenuDaoImpl
        Private aListVo As New TShisakuListcodeVo
        Private errorController As New ErrorController()
        Private ReadOnly exclusionShisakuListCode As New TShisakuEventExclusion

        ''' <summary>号車名称リストデータテーブル (NmTDColGousyaNameListで列名参照)</summary>
        Private _dtGousyaNameList As DataTable
        '↓↓↓2014/12/24 試作１課フラグを渡す TES)張 ADD BEGIN
        Private _isSisaku1KaFlg As Integer
        '↑↑↑2014/12/24 試作１課フラグを渡す TES)張 ADD END

        '↓↓↓2014/12/24 試作１課フラグを渡す TES)張 CHG BEGIN
        'Public Sub New(ByVal eventcode As String, ByVal listcode As String)
        ''' <summary>
        ''' 初期処理
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="listcode">リストコード</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal eventcode As String, ByVal listcode As String, Optional ByVal isSisaku1KaFlg As Integer = 0)
            '↑↑↑2014/12/24 試作１課フラグを渡す TES)張 CHG END

            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            '開発符号とイベント名称設定'
            subject = New TehaichoMenu.Logic.Frm19TehaichoMenu(LoginInfo.Now, eventcode, listcode)
            '↓↓↓2014/12/24 試作１課フラグを渡す TES)張 ADD BEGIN
            Me._isSisaku1KaFlg = isSisaku1KaFlg
            '↑↑↑2014/12/24 試作１課フラグを渡す TES)張 ADD END
            Initialize()

            changeButton(eventcode, subject.ListCode)

        End Sub

        ''' <summary>
        ''' 初期設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Initialize()
            ShisakuFormUtil.setTitleVersion(Me)
            Label1.Text = "PG-ID :" + PROGRAM_ID_19
            ShisakuFormUtil.SetIdAndBuka(Me.Label5, Me.Label6)

            lblStatus.Text = ""
            txtLISTcd.Enabled = False
            btnHensyu.Enabled = False
            btnKaiteiChusyutu.Enabled = False
            'btnSabun.Enabled = False
            btnRirekiSansyou.Enabled = False
            btnErrCheck.Enabled = False
            btnHacchuSakusei.Enabled = False
            btnHachuExcel.Enabled = False
            btnTensou.Enabled = False
            Button1.Enabled = False
            btnShinChotatsuRireki.Enabled = False
            btnSaiHensyu.Enabled = False
            '↓↓↓2014/12/24 試作１課フラグを渡す TES)張 ADD BEGIN
            If Me._isSisaku1KaFlg = SHISAKU1KA_SISAKU Then
                '非表示項目を設定
                Me.MsTool1.Visible = False
                Me.GroupBox1.Visible = False
                Me.Panel4.Visible = False
                '表示幅または位置を設定
                Me.Height = 255
            End If
            '↑↑↑2014/12/24 試作１課フラグを渡す TES)張 ADD END

            InitializeValidatorTehai()

        End Sub

        ''' <summary>
        ''' ボタンとラベルの変更
        ''' </summary>
        ''' <param name="eventcode">イベントコード</param>
        ''' <param name="listcode">リストコード</param>
        ''' <remarks></remarks>
        Private Sub changeButton(ByVal eventcode As String, ByVal listcode As String)

            aListVo = aList.FindByListCode(eventcode, listcode)
            subject.ListcodeKaiteiNo = aListVo.ShisakuListCodeKaiteiNo
            subject.Status = aListVo.Status

            If aListVo.ShisakuListCodeKaiteiNo = "000" Then
                setButtonDefault()
            ElseIf aListVo.ShisakuListCodeKaiteiNo = "001" Then
                lblStatus.Text = "転送済み"
                setButtonDefault()
                setButtonKaitei()
            Else
                lblStatus.Text = "転送済み"
                setButtonDefault()
                setButtonKaitei2()
            End If

            If aListVo.Status = "60" Then
                lblStatus.Text = "エラーチェック済(エラー無し)"
                setButtonErrChk()
            ElseIf aListVo.Status = "61" Then
                lblStatus.Text = "エラーチェック済(エラー有り)"
                setButtonErrChk()
            ElseIf aListVo.Status = "62" Then
                lblStatus.Text = "発注データ登録後"
                setButtonReg()
            ElseIf aListVo.Status = "63" Then
                lblStatus.Text = "転送済み"
                setButtonDefault()
                setButtonKaitei()
            ElseIf aListVo.Status = "6A" Then
                '保存時にはステータスは表示しない'
                lblStatus.Text = ""
                setButtonSave()
            End If

            If StringUtil.Equals(aListVo.AutoOrikomiFlag, "1") Then
                btnKaiteiChusyutu.Enabled = False
                btnKaiteiChusyutu.BackColor = Color.White
                'btnSabun.Enabled = False
                'btnSabun.BackColor = Color.White
            End If

            '最終抽出日・時間'
            Dim aDate As New ShisakuDate
            If Not aListVo.SaishinChusyutubi = 0 Then
                Dim HI As String
                Dim Jikan As String

                HI = Mid(aListVo.SaishinChusyutubi.ToString, 1, 4) + "/" + Mid(aListVo.SaishinChusyutubi.ToString, 5, 2) + "/" + Mid(aListVo.SaishinChusyutubi.ToString, 7, 2)

                If aListVo.SaishinChusyutujikan.ToString.Length = 5 Then
                    Jikan = Mid(aListVo.SaishinChusyutujikan.ToString, 1, 1) + ":" + Mid(aListVo.SaishinChusyutujikan.ToString, 2, 2) + ":" + Mid(aListVo.SaishinChusyutujikan.ToString, 4, 2)
                Else
                    Jikan = Mid(aListVo.SaishinChusyutujikan.ToString, 1, 2) + ":" + Mid(aListVo.SaishinChusyutujikan.ToString, 3, 2) + ":" + Mid(aListVo.SaishinChusyutujikan.ToString, 5, 2)
                End If
                Label2.Text = HI + " " + Jikan
            Else
                Label2.Text = "無し"
            End If
            txtLISTcd.Text = aListVo.OldListCode
            If aListVo.ZenkaiKaiteibi = 0 Then
                lblZenkaiKaiteiBi.Text = "前回改訂日：無し"
                lblKaiteiNo.Text = aListVo.ShisakuListCodeKaiteiNo
            Else
                lblZenkaiKaiteiBi.Text = "前回改訂日： " + Date.Parse(Format(aListVo.ZenkaiKaiteibi, "0000/00/00"))
                lblKaiteiNo.Text = aListVo.ShisakuListCodeKaiteiNo
            End If

            lblIbentoName.Text = aListVo.ShisakuEventName
            '工事指令No'
            Label14.Text = aListVo.ShisakuKoujiShireiNo
            lblListCode.Text = listcode
            '旧リストコード'
            txtLISTcd.Text = aListVo.OldListCode
            KaiteiNo = ""

        End Sub

#Region "ボタンの使用許可"

        ''' <summary>
        ''' 初回表示時のボタン
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub setButtonDefault()
            '編集ボタン'
            btnHensyu.Enabled = True
            btnHensyu.BackColor = Color.PaleGreen
            '改訂抽出ボタン'
            btnKaiteiChusyutu.Enabled = False
            btnKaiteiChusyutu.BackColor = Color.White
            ''改訂差分ボタン'
            'btnSabun.Enabled = False
            'btnSabun.BackColor = Color.White
            '改訂抽出履歴参照ボタン'
            btnRirekiSansyou.Enabled = False
            btnRirekiSansyou.BackColor = Color.White
            'エラーチェック’
            btnErrCheck.Enabled = False
            btnErrCheck.BackColor = Color.White
            '発注データ登録ボタン'
            btnHacchuSakusei.Enabled = False
            btnHacchuSakusei.BackColor = Color.White
            '訂正通知Excel出力ボタン'
            btnHachuExcel.Enabled = False
            btnHachuExcel.BackColor = Color.White
            '再編集ボタン'
            btnSaiHensyu.Enabled = False
            btnSaiHensyu.BackColor = Color.White
            '新調達履歴ボタン'
            btnShinChotatsuRireki.Enabled = False
            btnShinChotatsuRireki.BackColor = Color.White
            '訂正通知履歴ボタン'
            BtnTeiseiTutsuchiRireki.Enabled = False
            BtnTeiseiTutsuchiRireki.BackColor = Color.White
            '更新ボタン'
            Button1.Enabled = False
            Button1.BackColor = Color.White
            '新調達への転送ボタン'
            btnTensou.Enabled = False
            btnTensou.BackColor = Color.White

            '履歴ボタン'
            'Button2.Enabled = False
        End Sub

        ''' <summary>
        ''' 保存後のボタン
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub setButtonSave()
            btnHensyu.Enabled = True
            btnHensyu.BackColor = Color.PaleGreen

            'btnKaiteiChusyutu.Enabled = False
            'btnRirekiSansyou.Enabled = False
            btnErrCheck.Enabled = True
            btnErrCheck.BackColor = Nothing

            'btnHacchuSakusei.Enabled = False
            'btnHachuExcel.Enabled = False
            btnTensou.Enabled = False
            btnTensou.BackColor = Color.White

            btnSaiHensyu.Enabled = False
            btnSaiHensyu.BackColor = Color.White

            '更新ボタン'
            Button1.Enabled = False
            Button1.BackColor = Color.White

            '履歴ボタン'
            'Button2.Enabled = False
        End Sub

        ''' <summary>
        ''' エラーチェック後のボタン
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub setButtonErrChk()
            btnHensyu.Enabled = False
            btnHensyu.BackColor = Color.White

            btnErrCheck.Enabled = False
            btnErrCheck.BackColor = Color.White

            btnHacchuSakusei.Enabled = True
            btnHacchuSakusei.BackColor = Nothing

            'btnHachuExcel.Enabled = False
            btnTensou.Enabled = False
            btnTensou.BackColor = Color.White

            btnSaiHensyu.Enabled = True
            btnSaiHensyu.BackColor = Nothing

            '更新ボタン'
            Button1.Enabled = False
            Button1.BackColor = Color.White

            '履歴ボタン'
            'Button2.Enabled = False
        End Sub

        ''' <summary>
        ''' 発注データ登録後のボタン
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub setButtonReg()
            btnHensyu.Enabled = False
            btnHensyu.BackColor = Color.White

            btnTensou.Enabled = True
            btnTensou.BackColor = Nothing

            btnSaiHensyu.Enabled = True
            btnSaiHensyu.BackColor = Nothing

            txtLISTcd.Enabled = True

            '更新ボタン'
            Button1.Enabled = True
            Button1.BackColor = Color.LightCyan

            '履歴ボタン'
            'Button2.Enabled = False
        End Sub

        ''' <summary>
        ''' 改訂が001のボタン
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub setButtonKaitei()
            btnKaiteiChusyutu.Enabled = True
            btnKaiteiChusyutu.BackColor = Color.LightCyan
            'btnSabun.Enabled = True
            'btnSabun.BackColor = Color.Orange
            '訂正通知ボタン'
            btnHachuExcel.Enabled = False
            btnHachuExcel.BackColor = Color.White

            '新調達への転送「履歴」ボタン'
            btnShinChotatsuRireki.Enabled = True
            btnShinChotatsuRireki.BackColor = Nothing

            'BtnTeiseiTutsuchiRireki.Enabled = True
            'BtnTeiseiTutsuchiRireki.BackColor = Nothing

            'btnShinChotatsuRireki.Enabled = True
            'btnShinChotatsuRireki.BackColor = Nothing

            txtLISTcd.Enabled = True

            Button1.Enabled = True
            Button1.BackColor = Color.LightCyan

            If subject.Status = "60" Or subject.Status = "61" Or subject.Status = "62" Then
                btnKaiteiChusyutu.Enabled = False
                btnKaiteiChusyutu.BackColor = Color.White
                'btnSabun.Enabled = False
                'btnSabun.BackColor = Color.White
            End If

            '訂正通知は発注データ登録後'
            If StringUtil.Equals(subject.Status, "62") Then
                btnHachuExcel.Enabled = True
                btnHachuExcel.BackColor = Nothing
                'BtnTeiseiTutsuchiRireki.Enabled = True
                'BtnTeiseiTutsuchiRireki.BackColor = Nothing
            End If


        End Sub

        ''' <summary>
        ''' 改訂が002以降のボタン
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub setButtonKaitei2()

            If StringUtil.Equals(aListVo.AutoOrikomiFlag, "1") Then
                btnKaiteiChusyutu.Enabled = False
                btnKaiteiChusyutu.BackColor = Color.White
                'btnSabun.Enabled = False
                'btnSabun.BackColor = Color.White
            Else
                btnKaiteiChusyutu.Enabled = True
                btnKaiteiChusyutu.BackColor = Color.LightCyan
                'btnSabun.Enabled = True
                'btnSabun.BackColor = Color.Orange
            End If


            btnRirekiSansyou.Enabled = True
            btnRirekiSansyou.BackColor = Color.LightCyan

            btnHachuExcel.Enabled = False
            btnHachuExcel.BackColor = Color.White

            BtnTeiseiTutsuchiRireki.Enabled = True
            BtnTeiseiTutsuchiRireki.BackColor = Nothing

            btnShinChotatsuRireki.Enabled = True
            btnShinChotatsuRireki.BackColor = Nothing

            txtLISTcd.Enabled = True

            Button1.Enabled = True
            Button1.BackColor = Color.LightCyan

            If subject.Status = "60" Or subject.Status = "61" Or subject.Status = "62" Then
                btnKaiteiChusyutu.Enabled = False
                btnKaiteiChusyutu.BackColor = Color.White
                'btnSabun.Enabled = False
                'btnSabun.BackColor = Color.White
            End If

            If StringUtil.Equals(subject.Status, "62") Then
                btnHachuExcel.Enabled = True
                btnHachuExcel.BackColor = Nothing
            End If

        End Sub

#End Region

        ''' <summary>
        ''' 戻るボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            Me.Close()
        End Sub

        '---------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------
        '２次改修
        Public Sub ExclusiveControl()
            Dim eventVo As New TExclusiveControlEventVo
            Dim eventDao As ExclusiveControlEventDao = New ExclusiveControlEventImpl

            'キー情報が無いとエラーになるので事前にチェック（イレギュラー以外あるはず）
            eventVo = eventDao.FindByPk(aListVo.ShisakuEventCode, TehaiTantoMode)   'このメニューに遷移しているということは手配担当モードのはず
            If IsNothing(eventVo) Then
                'キー情報が無ければスルー。
            Else
                'KEY情報をセット
                eventVo.ShisakuEventCode = aListVo.ShisakuEventCode
                eventVo.EditMode = TehaiTantoMode
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
        ''' <param name="isMode">「登録」の場合、true</param>
        ''' <remarks></remarks>
        Private Sub RegisterMain(ByVal StrEventCode As String, _
                                 ByVal isMode As Boolean)

            Dim aShisakuDate As New ShisakuDate
            Dim eventVo As New TExclusiveControlEventVo
            Dim eventDao As ExclusiveControlEventDao = New ExclusiveControlEventImpl

            'KEY情報
            eventVo.ShisakuEventCode = StrEventCode
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

        ''' <summary>
        ''' アプリケーション終了ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
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

        ''' <summary>
        ''' 手配帳編集ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnHensyu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHensyu.Click

            '↓↓↓2014/12/24 試作１課フラグを渡す TES)張 CHG BEGIN
            'Using frm20 As New frm20DispTehaichoEdit(aListVo.ShisakuEventCode, aListVo.ShisakuListCode)
            Using frm20 As New frm20DispTehaichoEdit(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, Me._isSisaku1KaFlg)
                '↑↑↑2014/12/24 試作１課フラグを渡す TES)張 CHG END
                '初期化正常完了で編集画面表示
                If frm20.InitComplete = True Then
                    Me.Hide()

                    frm20.ShowDialog()

                End If
            End Using
            Me.Show()
            'ここで再表示のための処理'
            changeButton(aListVo.ShisakuEventCode, aListVo.ShisakuListCode)

        End Sub

        ''' <summary>
        ''' 改訂抽出ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnKaiteiChusyutu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKaiteiChusyutu.Click
            Cursor.Current = Cursors.WaitCursor

            frm1KaiteiCyushutu.setEvent(aListVo.ShisakuEventCode, lblListCode.Text, aListVo.ShisakuListCodeKaiteiNo, KaiteiNo, subject, aListVo.ShisakuEventName)
            frm1KaiteiCyushutu.ShowDialog()

            Dim result As Boolean = frm1KaiteiCyushutu.getResult

            If result Then
                Try
                    'エクセル出力'
                    Cursor.Current = Cursors.WaitCursor
                    Dim ExcelKaiteiChusyutu As New Excel.ExportKaiteChushutuExcel(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, aListVo.ShisakuListCodeKaiteiNo, aListVo.ShisakuEventName, 0)
                    MsgBox("改訂抽出が終了しました。", MsgBoxStyle.Information, "正常終了")
                Catch ex As SqlClient.SqlException

                    'MsgBox(ex.Message)
                    MsgBox("Excelの書込みに失敗しました。", MsgBoxStyle.Information, "エラー")
                End Try

            End If

            changeButton(aListVo.ShisakuEventCode, lblListCode.Text)

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

        End Sub

        ''' <summary>
        ''' 履歴参照ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnRirekiSansyou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRirekiSansyou.Click
            Using frm25 As New frm25DispTehaichoEditKaitei(aListVo.ShisakuEventCode, lblListCode.Text)
                frm25.ShowDialog(Me)
                frm25.Dispose()
            End Using
            If _CanselFlg Then
                _CanselFlg = False
                Return
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            'Try
            '    db.BeginTransaction()
            '    '改訂抽出'
            '    subject.KaiteiChushutu(aListVo.ShisakuEventCode, lblListCode.Text, aListVo.ShisakuListCodeKaiteiNo, KaiteiNo)

            '    db.Commit()
            'Catch ex As SqlClient.SqlException
            '    db.Rollback()

            'End Try

            '2012/01/21
            'エクセル出力'
            Try
                Dim ExcelKaiteiChusyutu As New Excel.ExportKaiteChushutuExcel(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, KaiteiNo, aListVo.ShisakuEventName, 1)
            Catch ex As Exception
                MsgBox("Excelの書込みに失敗しました。", MsgBoxStyle.Information, "エラー")
            End Try




            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

            'ラベルの更新'
            changeButton(aListVo.ShisakuEventCode, lblListCode.Text)

        End Sub

        ''' <summary>
        ''' 訂正通知データEXCEL出力ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnHachuExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHachuExcel.Click
            If frm01Kakunin.ConfirmOkCancel("訂正通知データEXCEL出力を行います。") <> MsgBoxResult.Ok Then
                Return
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            subject.TeiseiTsuti(aListVo.ShisakuEventCode, lblListCode.Text, aListVo.ShisakuListCodeKaiteiNo)
            '2012/01/21
            'こっちは最新
            Try
                Dim ExcelError As New Excel.ExportTeiseiExcel(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, aListVo.ShisakuListCodeKaiteiNo, 0)
            Catch ex As Exception
                MsgBox("Excelの書込みに失敗しました。", MsgBoxStyle.Information, "エラー")
            End Try

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

            'ラベルの更新'
            changeButton(aListVo.ShisakuEventCode, lblListCode.Text)

        End Sub

        ''' <summary>
        ''' 訂正通知データ履歴ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub BtnTeiseiTutsuchiRireki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTeiseiTutsuchiRireki.Click
            Using frm29 As New frm29DispTehaichoEditHachuExcel(aListVo.ShisakuEventCode, lblListCode.Text, "0")
                frm29.ShowDialog(Me)
                frm29.Dispose()
            End Using
            If _CanselFlg Then
                _CanselFlg = False
                Return
            End If
            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            '2012/01/21
            Try
                Dim ExcelError As New Excel.ExportTeiseiExcel(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, _KaiteiNo, 1)
            Catch ex As Exception
                MsgBox("Excelの書込みに失敗しました。", MsgBoxStyle.Information, "エラー")
            End Try

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

            'ラベルの更新'
            changeButton(aListVo.ShisakuEventCode, lblListCode.Text)
        End Sub

        ''' <summary>
        ''' 発注データ登録ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnHacchuSakusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHacchuSakusei.Click

            '１回目だけ（改訂０００）ポップアップ画面を表示する。
            If lblKaiteiNo.Text = "000" Then
                Using frmHacchuSakusei As New frmHacchuSakusei
                    frmHacchuSakusei.ShowDialog(Me)
                    frmHacchuSakusei.Dispose()
                End Using

                If _CanselFlg Then
                    _CanselFlg = False
                    Return
                End If
            Else
                '改訂が001以降は登録済みのコードを再設定する。
                OldListCode = txtLISTcd.Text
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            Try
                '発注データ処理'
                subject.HacchuDateRegister(aListVo.ShisakuEventCode, lblListCode.Text, OldListCode)
            Catch ex As SqlClient.SqlException
                MsgBox(ex.Message)
                Return
            End Try

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

            'ラベルの更新'
            changeButton(aListVo.ShisakuEventCode, lblListCode.Text)

        End Sub

#Region "使うのか不明"
        'Private Sub frm19DispTehaichouMenu_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        '    Select Case frm19ParaModori
        '        Case "H"
        '            'ステータス更新
        '            lblStatus.Text = "発注データ登録済み"
        '            btnHacchuSakusei.ForeColor = Color.White
        '            'btnHacchuSakusei.BackColor = Color.White
        '            btnHacchuSakusei.Enabled = False
        '        Case "M"
        '            'ステータス更新
        '            lblStatus.Text = ""
        '            btnHacchuSakusei.ForeColor = Color.Black
        '            'btnHacchuSakusei.BackColor = Color.Yellow
        '            btnHacchuSakusei.Enabled = True
        '    End Select

        '    '初期表示時はこの配置'


        'End Sub
#End Region

        ''' <summary>
        ''' 再編集ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnSaiHensyu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaiHensyu.Click
            If frm01Kakunin.ConfirmOkCancel("再編集を可能にします") <> MsgBoxResult.Ok Then
                Return
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            Try
                'ステータスを更新する'
                aList.UpdateByStatus(aListVo.ShisakuEventCode, lblListCode.Text, "")
            Catch ex As SqlClient.SqlException
                MsgBox(ex.Message)
                Return
            End Try

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

            changeButton(aListVo.ShisakuEventCode, lblListCode.Text)
        End Sub

        ''' <summary>
        ''' エラーチェックボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnErrCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnErrCheck.Click
            If frm01Kakunin.ConfirmOkCancel("エラーチェックを行います。") <> MsgBoxResult.Ok Then
                Return
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            Dim flag As Boolean
            flag = subject.ErrorCheck(aListVo.ShisakuEventCode, aListVo.ShisakuListCode)
            '2012/01/21

            Try
                Dim ExcelError As New Excel.ExportTehaiMenuErrorExcel(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, Me.lblKaiteiNo.Text.ToString)

            Catch ex As Exception
                MsgBox("Excelの書込みに失敗しました。", MsgBoxStyle.Information, "エラー")
                Dim impl As TehaichoMenu.Dao.TehaichoMenuDao = New TehaichoMenu.Impl.TehaichoMenuDaoImpl
                If flag Then
                    impl.UpdateByStatus(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, "61")
                Else
                    impl.UpdateByStatus(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, "60")
                End If
                Exit Sub
            Finally
                Dim impl As TehaichoMenu.Dao.TehaichoMenuDao = New TehaichoMenu.Impl.TehaichoMenuDaoImpl
                If flag Then
                    impl.UpdateByStatus(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, "61")
                Else
                    impl.UpdateByStatus(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, "60")
                End If
            End Try

            MsgBox("手配帳のエラーチェックが終了しました。", MsgBoxStyle.Information, "正常終了")

            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

            'ラベルの更新'
            changeButton(aListVo.ShisakuEventCode, lblListCode.Text)

        End Sub

        ''' <summary>
        ''' 社員名の取得
        ''' </summary>
        ''' <param name="userId">ユーザーID</param>
        ''' <returns>社員名</returns>
        ''' <remarks></remarks>
        Private Function ResolveUserName(ByVal userId As String) As String

            Dim dao As New ShisakuDaoImpl
            Dim userVo As Rhac0650Vo = dao.FindUserById(userId)

            If userVo Is Nothing Then
                Return userId
            End If
            Return userVo.ShainName
        End Function

        ''' <summary>
        ''' 新調達への転送ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnTensou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTensou.Click

            '試作リスト一覧のユニット区分が"M"かつ訂正通知EXCEL出力が未か、
            '訂正通知EXCEL出力が出力済だが出力後に手配帳編集、保存されていた場合、エラーを表示する。
            If Not StringUtil.Equals(aListVo.ShisakuListCodeKaiteiNo, "000") And aListVo.UnitKbn = "M" Then
                Dim aTeiseiVo As New TShisakuTehaiTeiseiKihonVo
                Dim aKihonVo As New TShisakuTehaiKihonVo

                aTeiseiVo = aList.FindByTehaiSaishin(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, aListVo.ShisakuListCodeKaiteiNo)
                aKihonVo = aList.FindByKihonSaishin(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, aListVo.ShisakuListCodeKaiteiNo)
                If StringUtil.IsEmpty(aTeiseiVo) Then
                    ComFunc.ShowErrMsgBox("訂正通知EXCELが未出力です。" & vbLf & vbLf & _
                                          "出力後、[新調達への転送]ボタンをクリックしてください。")
                    Return
                Else
                    If aTeiseiVo.UpdatedDate & aTeiseiVo.UpdatedTime < aKihonVo.UpdatedDate & aKihonVo.UpdatedTime Then
                        ComFunc.ShowErrMsgBox("訂正通知EXCEL出力後、手配帳の編集が行われています。" & vbLf & vbLf & _
                                              "再度[訂正通知EXCEL]を出力後、[新調達への転送]ボタンをクリックしてください。")
                        Return
                    End If
                End If
            End If

            If frm01Kakunin.ConfirmOkCancel("新調達への転送を行います。") <> MsgBoxResult.Ok Then
                Return
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            'Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor

            Try
                '転送処理をする'
                subject.Tensou(aListVo)

            Catch ex As SqlClient.SqlException
                subject.BackStatus(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, "62")
            Finally
                Try
                    Dim ExcelShinChotatsu As New Excel.ExportShinchotatsu(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, aListVo.ShisakuListCodeKaiteiNo, 0)

                    '2012/03/08'
                    '裏にメッセージが隠れることがあるのでとりあえず１秒間待機させる'
                    System.Threading.Thread.Sleep(1000)

                    MsgBox("新調達への転送（EXCEL出力）が終了しました。", MsgBoxStyle.Information, "正常終了")
                Catch ex As Exception
                    MsgBox("Excelの書込みに失敗しました。", MsgBoxStyle.Information, "エラー")
                End Try

                '画面を綺麗に、実行中のカーソルを元に戻す。
                Cursor.Current = Cursors.Default

                changeButton(aListVo.ShisakuEventCode, lblListCode.Text)

            End Try

        End Sub

        ''' <summary>
        ''' 新調達への転送(履歴)ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnShisanChotatsuRireki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShinChotatsuRireki.Click
            Using frm29 As New frm29DispTehaichoEditHachuExcel(aListVo.ShisakuEventCode, lblListCode.Text, "1")
                frm29.ShowDialog(Me)
                frm29.Dispose()
            End Using
            If _CanselFlg Then
                _CanselFlg = False
                Return
            End If

            '画面を綺麗に、実行中のカーソルへ変更。
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor
            Try
                Dim ExcelShinChotatsu As New Excel.ExportShinchotatsu(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, KaiteiNo, 1)
            Catch ex As Exception
                MsgBox("Excelの書込みに失敗しました。", MsgBoxStyle.Information, "エラー")
            End Try


            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default

            changeButton(aListVo.ShisakuEventCode, lblListCode.Text)
        End Sub

        ''' <summary>
        ''' 旧リストコード更新ボタン
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            errorController.ClearBackColor()
            Try
                validatorTehaiMenu.AssertValidate()
                '旧リストコード更新'
                'ここはステータスを更新しない'
                aList.UpdateByOldListCode(aListVo.ShisakuEventCode, lblListCode.Text, txtLISTcd.Text, False)
            Catch ex As IllegalInputException
                errorController.SetBackColorOnError(ex.ErrorControls)
                errorController.FocusAtFirstControl(ex.ErrorControls)
                ''エラーメッセージ
                ComFunc.ShowErrMsgBox(ex.Message)
                Return
            End Try
        End Sub

        Private validatorTehaiMenu As Validator

        ''' <summary>
        ''' 旧リストコードエラーチェック
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub InitializeValidatorTehai()

            validatorTehaiMenu = New Validator

            Dim OldListCodeRequired As New Validator("リストコードを入力してください")
            OldListCodeRequired.Add(txtLISTcd).Required()

            validatorTehaiMenu.Add(OldListCodeRequired)

        End Sub


#Region "Frm29で設定された改訂Noを取得する"

        Private _KaiteiNo As String

        ''' <summary>
        ''' 改訂No
        ''' </summary>
        ''' <value>改訂No</value>
        ''' <returns>改訂No</returns>
        ''' <remarks></remarks>
        Public Property KaiteiNo() As String
            Get
                Return _KaiteiNo
            End Get
            Set(ByVal value As String)
                _KaiteiNo = value
            End Set
        End Property

        Private _OldListCode As String

        ''' <summary>
        ''' 旧リストコード
        ''' </summary>
        ''' <value>旧リストコード</value>
        ''' <returns>旧リストコード</returns>
        ''' <remarks></remarks>
        Public Property OldListCode() As String
            Get
                Return _OldListCode
            End Get
            Set(ByVal value As String)
                _OldListCode = value
            End Set
        End Property

        Private _CanselFlg As Boolean

        ''' <summary>
        ''' キャンセルフラグ
        ''' </summary>
        ''' <value>キャンセルフラグ</value>
        ''' <returns>キャンセルフラグ</returns>
        ''' <remarks></remarks>
        Public Property CanselFlg() As Boolean
            Get
                Return _CanselFlg
            End Get
            Set(ByVal value As Boolean)
                _CanselFlg = value
            End Set
        End Property


#End Region

#Region "入力制限"

        ''' <summary>
        ''' 半角英数を全角英数に変更
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub txtLISTcd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLISTcd.KeyPress
            e.KeyChar = UCase(e.KeyChar)
        End Sub

#End Region

        '２次改修追加機能
        Private Sub msBaseKaiteiChuSyutuTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msBaseKaiteiChuSyutuTool.Click

            'EXCELベースで抽出'
            Application.DoEvents()
            Cursor.Current = Cursors.WaitCursor
            subject.KaiteiChushutuBase(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, aListVo.ShisakuListCodeKaiteiNo, KaiteiNo)
            Cursor.Current = Cursors.Default

            Try
                'エクセル出力'
                Dim ExcelKaiteiChusyutu As New Excel.ExportKaiteChushutuExcel(aListVo.ShisakuEventCode, aListVo.ShisakuListCode, aListVo.ShisakuListCodeKaiteiNo, aListVo.ShisakuEventName, 0)
                MsgBox("EXCEL抽出（Baseと最新比較）が終了しました。", MsgBoxStyle.Information, "正常終了")
            Catch ex As SqlClient.SqlException

                'MsgBox(ex.Message)
                MsgBox("Excelの書込みに失敗しました。", MsgBoxStyle.Information, "エラー")
            End Try

        End Sub

        Private Sub btnSabun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSabun.Click
            Import()
        End Sub


#Region "Excel取込機能(メイン処理)"
        ''' <summary>
        '''Excel取込処理(メイン処理)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Import()

            'ファイルパス取得
            Dim fileName As String = ImportFileDialog()

            If fileName.Equals(String.Empty) Then
                Return
            End If

            Cursor.Current = Cursors.WaitCursor

            'シート名選択画面を表示して取込するシートを選択する。
            '   終了ボタン
            If subSheetSelect(fileName) = "" Then
                Exit Sub
            End If

            'Excelファイからのインポート処理
            If importExcelFile(fileName) = False Then
                Return
            End If

            ''編集対象ブロックを設定
            'ExcelImpAddEditBlock()

            ''Excelファイル上で削除された行を探索し編集対象ブロックに追加する(要するにExcel上で削除されると色々厄介なのです）
            'Me.DataMatchDeleteRec()

            ''クローズ処理イベント取得用クラス
            'Dim closer As frm00Kakunin.IFormCloser = New CopyFormCloser(Me, fileName)

            ''取込内容確認フォームを表示
            'frm00Kakunin.ConfirmShow("EXCEL取込の確認", "EXCELの情報を確認してください。", _
            '                               "EXCELのデータを反映しますか？", "EXCELを反映", "取込前に戻す", closer)
        End Sub

#End Region



#Region "Excel取込機能(シート選択)"
        Private Function subSheetSelect(ByVal fileName As String) As String

            '初期設定
            subSheetSelect = ""

            'ExcelSheet選択画面
            Try
                Using frmExcelSheetSelect As New frm20ExcelSheetSelect()

                    'ADO.NET の機能を使ってシート名を取得
                    'ADO でシート名を取得した場合は、Excelで表示している順番ではなくソート順になる。
                    'Dim xlsFileName As String = System.IO.Path.GetFullPath(fileName)
                    Dim con As System.Data.OleDb.OleDbConnection = New System.Data.OleDb.OleDbConnection( _
                          "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fileName & _
                                ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
                    con.Open()
                    Dim sgt As DataTable = con.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, _
                                 New Object() {Nothing, Nothing, Nothing, "TABLE"})

                    For i As Integer = 0 To sgt.Rows.Count - 1
                        Dim sheetName As String = sgt.Rows(i)!TABLE_NAME.ToString
                        If sheetName.EndsWith("$") Or sheetName.EndsWith("'") Then

                            '余分な文字を取る。
                            sheetName = sheetName.Replace("$", "")
                            sheetName = sheetName.Replace("'", "")

                            '画面のリストボックスに値をセットする。
                            frmExcelSheetSelect.LBexcelSheetSelect.Items.Add(sheetName)
                        End If
                    Next i
                    con.Close()

                    If frmExcelSheetSelect.ShowDialog = Windows.Forms.DialogResult.OK Then
                        subSheetSelect = ExcelImportSheetName
                    Else
                        MsgBox("EXCEL取込を中断します。")
                        subSheetSelect = ""
                        Exit Function
                    End If
                End Using
            Catch
                MsgBox("ExcelSheet選択処理で問題が発生しました")
                Exit Function
            Finally
                Cursor.Current = Cursors.Default
            End Try

        End Function

#End Region
#Region "Excel取込機能(ファイルダイアログ)"
        ''' <summary>
        ''' Excel取込機能(ファイルダイアログ)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ImportFileDialog() As String
            Dim fileName As String
            Dim systemDrive As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            Dim ofd As New OpenFileDialog()

            ' ファイル名を指定します
            ofd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
            ofd.Title = "取込対象のファイルを選択してください"

            ' 起動ディレクトリを指定します
            ofd.InitialDirectory = systemDrive

            '  [ファイルの種類] ボックスに表示される選択肢を指定します
            ofd.Filter = "Excel files(*.xls)|*.xls"

            'ダイアログ選択有無
            If ofd.ShowDialog() = DialogResult.OK Then
                fileName = ofd.FileName
            Else
                Return String.Empty
            End If

            ofd.Dispose()

            Return fileName

        End Function
#End Region
#Region "Excel取込機能(Excelオープン)"
        ''' <summary>
        ''' Excel取込機能(Excelオープン)
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <remarks></remarks>
        Private Function importExcelFile(ByVal fileName As String) As Boolean
            'Dim dtImpGousyaList As DataTable

            'Dim dtBase As DataTable = TehaichoEditImpl.FindAllBaseInfo _
            '            (aListVo.ShisakuEventCode, aListVo.ShisakuListCode)


            ''試作手配号車一覧取得
            'Dim dtGousya As DataTable = TehaichoEditImpl.FindAllGousyaInfo _
            '            (aListVo.ShisakuEventCode, aListVo.ShisakuListCode, _dtGousyaNameList)

            'For i As Integer = 0 To dtGousya.Rows.Count - 1

            '    Dim gousyaInsuTotal As Integer = 0

            '    '号車変動列更新
            '    For j As Integer = 0 To _dtGousyaNameList.Rows.Count - 1


            '    Next

            'Next


            Dim fileNameOut As String
            'ファイル名に使用するイベント名を取得する為にここで取得
            Dim impl As TehaichoMenuDao = New TehaichoMenuDaoImpl
            Dim aEventVo As New TShisakuEventVo
            aEventVo = impl.FindByUnitKbn(aListVo.ShisakuEventCode)

            '出力ファイル名生成
            Using sfd As New SaveFileDialog()
                sfd.FileName = ShisakuCommon.ShisakuGlobal.ExcelOutPut
                sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                '--------------------------------------------------
                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                '--------------------------------------------------
                '[Excel出力系 F]
                'イベント情報'
                fileNameOut = aEventVo.ShisakuKaihatsuFugo + aListVo.ShisakuEventName + " 手配帳改訂抽出 " + Now.ToString("MMdd") + Now.ToString("HHmm") + ".xls"
                fileNameOut = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + StringUtil.ReplaceInvalidCharForFileName(fileNameOut)    '2012/02/08 Excel出力ディレクトリ指定対応
            End Using



            If Not ShisakuComFunc.IsFileOpen(fileName) Then

                Dim CyushutuImpl As KaiteiChusyutuDao = New KaiteiChusyutuDaoImpl
                Dim ShisakuTehaiKihonVoList As New List(Of TShisakuTehaiKihonVo)
                Dim ShisakuTehaiGousyaVoList As New List(Of TShisakuTehaiGousyaVo)

                Dim strBLockCheck As String = ""
                Dim strHoldBlockNo As String = ""
                Dim lngCnt As Long = 7
                SetColumnNo()

                Using xls As New ShisakuExcel(fileName)

                    Using xls2 As New ShisakuExcel(fileNameOut)
                        xls2.OpenBook(fileNameOut)
                        xls2.ClearWorkBook()
                        xls2.SetFont("ＭＳ Ｐゴシック", 11)

                        Dim TITLE_ROW As Integer = 5
                        Dim START_ROW As Integer = 7
                        Dim TITLE_ROW_2 As Integer = 6

                        'イベントコード'
                        xls2.MergeCells(1, 1, 4, 1, True)
                        xls2.SetValue(1, 1, "イベントコード : ")
                        xls2.MergeCells(5, 1, 9, 1, True)
                        xls2.SetValue(5, 1, aListVo.ShisakuEventCode)
                        'イベント名称'
                        xls2.MergeCells(1, 2, 4, 2, True)
                        xls2.SetValue(1, 2, "イベント名称 : ")
                        xls2.MergeCells(5, 2, 9, 2, True)
                        xls2.SetValue(5, 2, aEventVo.ShisakuKaihatsuFugo + " " + aListVo.ShisakuEventName)
                        '工事指令No'
                        xls2.MergeCells(1, 3, 4, 3, True)
                        xls2.SetValue(1, 3, "工事指令No : ")
                        xls2.MergeCells(5, 3, 9, 3, True)
                        xls2.SetValue(5, 3, aListVo.ShisakuKoujiShireiNo)
                        '抽出日時'
                        xls2.MergeCells(1, 4, 4, 4, True)
                        xls2.SetValue(1, 4, "抽出日時")
                        xls2.MergeCells(5, 4, 9, 4, True)

                        Dim chusyutubi As String
                        Dim chusyutujikan As String

                        chusyutubi = Mid(aListVo.SaishinChusyutubi.ToString, 1, 4) + "/" + Mid(aListVo.SaishinChusyutubi.ToString, 5, 2) + "/" + Mid(aListVo.SaishinChusyutubi.ToString, 7, 2)

                        '-----------------------------------------------------------
                        '   日付が取得できなければシステム日付を設定する。
                        If aListVo.SaishinChusyutubi.ToString = "0" Then
                            chusyutubi = DateTime.Now.ToString("yyyy/MM/dd")
                        End If
                        '-----------------------------------------------------------

                        If aListVo.SaishinChusyutujikan.ToString.Length < 6 Then
                            chusyutujikan = Mid(aListVo.SaishinChusyutujikan.ToString, 1, 1) + ":" + Mid(aListVo.SaishinChusyutujikan.ToString, 2, 2) + ":" + Mid(aListVo.SaishinChusyutujikan.ToString, 4, 2)
                        Else
                            chusyutujikan = Mid(aListVo.SaishinChusyutujikan.ToString, 1, 2) + ":" + Mid(aListVo.SaishinChusyutujikan.ToString, 3, 2) + ":" + Mid(aListVo.SaishinChusyutujikan.ToString, 5, 2)
                        End If

                        '-----------------------------------------------------------
                        '   時間が取得できなければシステム時間を設定する。
                        If aListVo.SaishinChusyutujikan.ToString = "0" Then
                            chusyutujikan = DateTime.Now.ToString("HH:mm:ss")
                        End If
                        '-----------------------------------------------------------

                        xls2.SetValue(5, 4, chusyutubi + " " + chusyutujikan)

                        'ヘッダー情報
                        xls2.SetRowHeight(TITLE_ROW, TITLE_ROW, 104)
                        'フラグ'
                        xls2.MergeCells(COLUMN_FLAG, TITLE_ROW, COLUMN_FLAG, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_FLAG, TITLE_ROW, COLUMN_FLAG, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_FLAG, TITLE_ROW, "フラグ  ")
                        'ブロックNo'
                        xls2.MergeCells(COLUMN_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_BLOCK_NO, TITLE_ROW, COLUMN_BLOCK_NO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_BLOCK_NO, TITLE_ROW, "ブロックNo")
                        '改訂No'
                        xls2.MergeCells(COLUMN_KAITEI_NO, TITLE_ROW, COLUMN_KAITEI_NO, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_KAITEI_NO, TITLE_ROW, COLUMN_KAITEI_NO, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_KAITEI_NO, TITLE_ROW, "改訂No")
                        '専用マーク'
                        xls2.MergeCells(COLUMN_SENYOU_MARK, TITLE_ROW, COLUMN_SENYOU_MARK, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_SENYOU_MARK, TITLE_ROW, COLUMN_SENYOU_MARK, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_SENYOU_MARK, TITLE_ROW, "専用マーク")
                        'レベル'
                        xls2.MergeCells(COLUMN_LEVEL, TITLE_ROW, COLUMN_LEVEL, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_LEVEL, TITLE_ROW, COLUMN_LEVEL, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_LEVEL, TITLE_ROW, "レベル")
                        '部品番号'
                        xls2.MergeCells(COLUMN_BUHIN_NO, TITLE_ROW, COLUMN_BUHIN_NO, TITLE_ROW_2, True)
                        xls2.SetValue(COLUMN_BUHIN_NO, TITLE_ROW, "部品番号")
                        '試作区分'
                        xls2.MergeCells(COLUMN_SHISAKU_KBN, TITLE_ROW, COLUMN_SHISAKU_KBN, TITLE_ROW_2, True)
                        xls2.SetValue(COLUMN_SHISAKU_KBN, TITLE_ROW, "試作区分")
                        '改訂'
                        xls2.MergeCells(COLUMN_KAITEI, TITLE_ROW, COLUMN_KAITEI, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_KAITEI, TITLE_ROW, COLUMN_KAITEI, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_KAITEI, TITLE_ROW, "改訂")
                        '枝番'
                        xls2.MergeCells(COLUMN_EDA_BAN, TITLE_ROW, COLUMN_EDA_BAN, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_EDA_BAN, TITLE_ROW, COLUMN_EDA_BAN, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_EDA_BAN, TITLE_ROW, "枝番")
                        '部品名称'
                        xls2.MergeCells(COLUMN_BUHIN_NAME, TITLE_ROW, COLUMN_BUHIN_NAME, TITLE_ROW_2, True)
                        xls2.SetValue(COLUMN_BUHIN_NAME, TITLE_ROW, "部品名称")
                        '集計コード'
                        xls2.MergeCells(COLUMN_SHUKEI_CODE, TITLE_ROW, COLUMN_SHUKEI_CODE, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_SHUKEI_CODE, TITLE_ROW, COLUMN_SHUKEI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_SHUKEI_CODE, TITLE_ROW, "集計コード")
                        '購担'
                        xls2.MergeCells(COLUMN_KOUTAN, TITLE_ROW, COLUMN_KOUTAN, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_KOUTAN, TITLE_ROW, COLUMN_KOUTAN, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_KOUTAN, TITLE_ROW, "購担")
                        '取引先コード'
                        xls2.MergeCells(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, COLUMN_TORIHIKISAKI_CODE, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_TORIHIKISAKI_CODE, TITLE_ROW, "取引先コード")
                        '取引先名称'
                        xls2.MergeCells(COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, COLUMN_TORIHIKISAKI_NAME, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_TORIHIKISAKI_NAME, TITLE_ROW, "取引先名称")
                        '合計員数数量'
                        xls2.MergeCells(COLUMN_TOTAL_INSU, TITLE_ROW, COLUMN_TOTAL_INSU, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_TOTAL_INSU, TITLE_ROW, COLUMN_TOTAL_INSU, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_TOTAL_INSU, TITLE_ROW, "員数")
                        '再使用不可'
                        xls2.MergeCells(COLUMN_SAISHIYOUFUKA, TITLE_ROW, COLUMN_SAISHIYOUFUKA, TITLE_ROW_2, True)
                        xls2.SetOrientation(COLUMN_SAISHIYOUFUKA, TITLE_ROW, COLUMN_SAISHIYOUFUKA, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                        xls2.SetValue(COLUMN_SAISHIYOUFUKA, TITLE_ROW, "再使用不可")
                        '供給セクション'
                        xls2.MergeCells(COLUMN_KYOUKU_SECTION, TITLE_ROW, COLUMN_KYOUKU_SECTION, TITLE_ROW_2, True)
                        xls2.SetValue(COLUMN_KYOUKU_SECTION, TITLE_ROW, "供給セクション")
                        '出荷予定日'
                        xls2.MergeCells(COLUMN_SHUKKAYOTEIBI, TITLE_ROW, COLUMN_SHUKKAYOTEIBI, TITLE_ROW_2, True)
                        xls2.SetValue(COLUMN_SHUKKAYOTEIBI, TITLE_ROW, "出図予定日")
                        '↓↓2014/09/24 酒井 ADD BEGIN
                        xls2.MergeCells(COLUMN_TSUKURIKATA_SEISAKU, TITLE_ROW, COLUMN_TSUKURIKATA_KIBO, TITLE_ROW, True)
                        xls2.SetValue(COLUMN_TSUKURIKATA_SEISAKU, TITLE_ROW, "作り方")
                        xls2.SetValue(COLUMN_TSUKURIKATA_SEISAKU, TITLE_ROW_2, "製作方法")
                        xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, TITLE_ROW_2, "型仕様1")
                        xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, TITLE_ROW_2, "型仕様2")
                        xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, TITLE_ROW_2, "型仕様3")
                        xls2.SetValue(COLUMN_TSUKURIKATA_TIGU, TITLE_ROW_2, "治具")
                        xls2.SetValue(COLUMN_TSUKURIKATA_NOUNYU, TITLE_ROW_2, "納入見通し")
                        xls2.SetValue(COLUMN_TSUKURIKATA_KIBO, TITLE_ROW_2, "部品製作規模・概要")
                        '↑↑2014/09/24 酒井 ADD END

                        '材質'
                        xls2.MergeCells(COLUMN_ZAISHITU_KIKAKU_1, TITLE_ROW, COLUMN_ZAISHITU_MEKKI, TITLE_ROW, True)
                        xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_1, TITLE_ROW, "材質")
                        '材質規格１'
                        xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_1, TITLE_ROW_2, "規格１")
                        '材質規格２'
                        xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_2, TITLE_ROW_2, "規格2")
                        '材質規格３'
                        xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_3, TITLE_ROW_2, "規格3")
                        '材質メッキ'
                        xls2.SetValue(COLUMN_ZAISHITU_MEKKI, TITLE_ROW_2, "メッキ")
                        '板厚'
                        xls2.MergeCells(COLUMN_SHISAKU_BANKO_SURYO, TITLE_ROW, COLUMN_SHISAKU_BANKO_SURYO_U, TITLE_ROW, True)
                        xls2.SetValue(COLUMN_SHISAKU_BANKO_SURYO, TITLE_ROW, "板厚")
                        '板厚数量'
                        xls2.SetValue(COLUMN_SHISAKU_BANKO_SURYO, TITLE_ROW_2, "板厚")
                        '板厚数量U'
                        xls2.SetValue(COLUMN_SHISAKU_BANKO_SURYO_U, TITLE_ROW_2, "u")
                        '試作部品費'
                        xls2.MergeCells(COLUMN_SHISAKU_BUHIN_HI, TITLE_ROW, COLUMN_SHISAKU_BUHIN_HI, TITLE_ROW_2, True)
                        xls2.SetValue(COLUMN_SHISAKU_BUHIN_HI, TITLE_ROW, "試作部品費")
                        '試作型費'
                        xls2.MergeCells(COLUMN_SHISAKU_KATA_HI, TITLE_ROW, COLUMN_SHISAKU_KATA_HI, TITLE_ROW_2, True)
                        xls2.SetValue(COLUMN_SHISAKU_KATA_HI, TITLE_ROW, "試作型費")
                        'NOTE'
                        xls2.MergeCells(COLUMN_NOTE, TITLE_ROW, COLUMN_NOTE, TITLE_ROW_2, True)
                        xls2.SetValue(COLUMN_NOTE, TITLE_ROW, "NOTE")
                        '備考'
                        xls2.MergeCells(COLUMN_BIKOU, TITLE_ROW, COLUMN_BIKOU, TITLE_ROW_2, True)
                        xls2.SetValue(COLUMN_BIKOU, TITLE_ROW, "備考")

                        '号車
                        For k As Integer = COLUMN_BIKOU + 1 To xls.EndCol
                            xls2.MergeCells(k, TITLE_ROW, k, TITLE_ROW_2, True)
                            xls2.SetOrientation(k, TITLE_ROW, k, TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
                            xls2.SetValue(k, TITLE_ROW, xls.GetValue(k, TITLE_ROW))
                        Next

                        For i As Integer = 0 To xls.EndRow

                            'ブロック、部品番号がある列のみ対象
                            If StringUtil.IsNotEmpty(xls.GetValue(2, 7 + i)) And _
                                StringUtil.IsNotEmpty(xls.GetValue(6, 7 + i)) Then

                                '削除
                                If xls.GetValue(1, 7 + i) = "削除" Then
                                    'フラグ'
                                    xls2.SetValue(COLUMN_FLAG, lngCnt, COLUMN_FLAG, lngCnt, xls.GetValue(COLUMN_FLAG, 7 + i))
                                    'ブロックNo'
                                    xls2.SetValue(COLUMN_BLOCK_NO, lngCnt, COLUMN_BLOCK_NO, lngCnt, xls.GetValue(COLUMN_BLOCK_NO, 7 + i))
                                    '改訂No'
                                    xls2.SetValue(COLUMN_KAITEI_NO, lngCnt, COLUMN_KAITEI_NO, lngCnt, xls.GetValue(COLUMN_KAITEI_NO, 7 + i))
                                    '専用マーク'
                                    xls2.SetValue(COLUMN_SENYOU_MARK, lngCnt, COLUMN_SENYOU_MARK, lngCnt, xls.GetValue(COLUMN_SENYOU_MARK, 7 + i))
                                    'レベル'
                                    xls2.SetValue(COLUMN_LEVEL, lngCnt, COLUMN_LEVEL, lngCnt, xls.GetValue(COLUMN_LEVEL, 7 + i))
                                    '部品番号'
                                    xls2.SetValue(COLUMN_BUHIN_NO, lngCnt, COLUMN_BUHIN_NO, lngCnt, xls.GetValue(COLUMN_BUHIN_NO, 7 + i))
                                    '試作区分'
                                    xls2.SetValue(COLUMN_SHISAKU_KBN, lngCnt, COLUMN_SHISAKU_KBN, lngCnt, xls.GetValue(COLUMN_SHISAKU_KBN, 7 + i))
                                    '改訂'
                                    xls2.SetValue(COLUMN_KAITEI, lngCnt, COLUMN_KAITEI, lngCnt, xls.GetValue(COLUMN_KAITEI, 7 + i))
                                    '枝番'
                                    xls2.SetValue(COLUMN_EDA_BAN, lngCnt, COLUMN_EDA_BAN, lngCnt, xls.GetValue(COLUMN_EDA_BAN, 7 + i))
                                    '改訂No'
                                    xls2.SetValue(COLUMN_KAITEI_NO, lngCnt, COLUMN_KAITEI_NO, lngCnt, xls.GetValue(COLUMN_KAITEI_NO, 7 + i))
                                    '部品名称'
                                    xls2.SetValue(COLUMN_BUHIN_NAME, lngCnt, COLUMN_BUHIN_NAME, lngCnt, xls.GetValue(COLUMN_BUHIN_NAME, 7 + i))
                                    '集計コード'
                                    xls2.SetValue(COLUMN_SHUKEI_CODE, lngCnt, COLUMN_SHUKEI_CODE, lngCnt, xls.GetValue(COLUMN_SHUKEI_CODE, 7 + i))
                                    '購担'
                                    xls2.SetValue(COLUMN_KOUTAN, lngCnt, COLUMN_KOUTAN, lngCnt, xls.GetValue(COLUMN_KOUTAN, 7 + i))
                                    '取引先コード'
                                    xls2.SetValue(COLUMN_TORIHIKISAKI_CODE, lngCnt, COLUMN_TORIHIKISAKI_CODE, lngCnt, xls.GetValue(COLUMN_TORIHIKISAKI_CODE, 7 + i))
                                    '取引先名称'
                                    xls2.SetValue(COLUMN_TORIHIKISAKI_NAME, lngCnt, COLUMN_TORIHIKISAKI_NAME, lngCnt, xls.GetValue(COLUMN_TORIHIKISAKI_NAME, 7 + i))
                                    '合計員数数量'
                                    xls2.SetValue(COLUMN_TOTAL_INSU, lngCnt, COLUMN_TOTAL_INSU, lngCnt, xls.GetValue(COLUMN_TOTAL_INSU, 7 + i))
                                    '再使用不可'
                                    xls2.SetValue(COLUMN_SAISHIYOUFUKA, lngCnt, COLUMN_SAISHIYOUFUKA, lngCnt, xls.GetValue(COLUMN_SAISHIYOUFUKA, 7 + i))
                                    '供給セクション'
                                    xls2.SetValue(COLUMN_KYOUKU_SECTION, lngCnt, COLUMN_KYOUKU_SECTION, lngCnt, xls.GetValue(COLUMN_KYOUKU_SECTION, 7 + i))
                                    '出荷予定日'
                                    xls2.SetValue(COLUMN_SHUKKAYOTEIBI, lngCnt, COLUMN_SHUKKAYOTEIBI, lngCnt, xls.GetValue(COLUMN_SHUKKAYOTEIBI, 7 + i))
                                    '↓↓2014/09/24 酒井 ADD BEGIN
                                    xls2.SetValue(COLUMN_TSUKURIKATA_SEISAKU, lngCnt, COLUMN_TSUKURIKATA_SEISAKU, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_SEISAKU, 7 + i))
                                    xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, lngCnt, COLUMN_TSUKURIKATA_KATASHIYOU_1, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, 7 + i))
                                    xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, lngCnt, COLUMN_TSUKURIKATA_KATASHIYOU_2, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, 7 + i))
                                    xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, lngCnt, COLUMN_TSUKURIKATA_KATASHIYOU_3, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, 7 + i))
                                    xls2.SetValue(COLUMN_TSUKURIKATA_TIGU, lngCnt, COLUMN_TSUKURIKATA_TIGU, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_TIGU, 7 + i))
                                    xls2.SetValue(COLUMN_TSUKURIKATA_NOUNYU, lngCnt, COLUMN_TSUKURIKATA_NOUNYU, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_NOUNYU, 7 + i))
                                    xls2.SetValue(COLUMN_TSUKURIKATA_KIBO, lngCnt, COLUMN_TSUKURIKATA_KIBO, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KIBO, 7 + i))
                                    '↑↑2014/09/24 酒井 ADD END

                                    '材質規格１'
                                    xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_1, lngCnt, COLUMN_ZAISHITU_KIKAKU_1, lngCnt, xls.GetValue(COLUMN_ZAISHITU_KIKAKU_1, 7 + i))
                                    '材質規格２'
                                    xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_2, lngCnt, COLUMN_ZAISHITU_KIKAKU_2, lngCnt, xls.GetValue(COLUMN_ZAISHITU_KIKAKU_2, 7 + i))
                                    '材質規格３'
                                    xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_3, lngCnt, COLUMN_ZAISHITU_KIKAKU_3, lngCnt, xls.GetValue(COLUMN_ZAISHITU_KIKAKU_3, 7 + i))
                                    '材質メッキ'
                                    xls2.SetValue(COLUMN_ZAISHITU_MEKKI, lngCnt, COLUMN_ZAISHITU_MEKKI, lngCnt, xls.GetValue(COLUMN_ZAISHITU_MEKKI, 7 + i))
                                    '板厚数量'
                                    xls2.SetValue(COLUMN_SHISAKU_BANKO_SURYO, lngCnt, COLUMN_SHISAKU_BANKO_SURYO, lngCnt, xls.GetValue(COLUMN_SHISAKU_BANKO_SURYO, 7 + i))
                                    '板厚数量U'
                                    xls2.SetValue(COLUMN_SHISAKU_BANKO_SURYO_U, lngCnt, COLUMN_SHISAKU_BANKO_SURYO_U, lngCnt, xls.GetValue(COLUMN_SHISAKU_BANKO_SURYO_U, 7 + i))
                                    '試作部品費'
                                    xls2.SetValue(COLUMN_SHISAKU_BUHIN_HI, lngCnt, COLUMN_SHISAKU_BUHIN_HI, lngCnt, xls.GetValue(COLUMN_SHISAKU_BUHIN_HI, 7 + i))
                                    '試作型費'
                                    xls2.SetValue(COLUMN_SHISAKU_KATA_HI, lngCnt, COLUMN_SHISAKU_KATA_HI, lngCnt, xls.GetValue(COLUMN_SHISAKU_KATA_HI, 7 + i))
                                    'NOTE'
                                    xls2.SetValue(COLUMN_NOTE, lngCnt, COLUMN_NOTE, lngCnt, xls.GetValue(COLUMN_NOTE, 7 + i))
                                    '備考'
                                    xls2.SetValue(COLUMN_BIKOU, lngCnt, COLUMN_BIKOU, lngCnt, xls.GetValue(COLUMN_BIKOU, 7 + i))

                                    '員数
                                    For k As Integer = COLUMN_BIKOU + 1 To xls.EndCol
                                        xls2.SetValue(k, lngCnt, k, lngCnt, xls.GetValue(k, 7 + i))
                                    Next

                                    lngCnt = lngCnt + 1

                                Else
                                    'Exit For
                                    '追加
                                    If xls.GetValue(1, 7 + i) <> "変更前" Then
                                        'For j As Integer = 0 To dtBase.Rows.Count - 1

                                        '    If xls.GetValue(2, 7 + i) = dtBase.Rows(j)(NmDTColBase.TD_SHISAKU_BLOCK_NO) _
                                        '        And xls.GetValue(6, 7 + i) = dtBase.Rows(j)(NmDTColBase.TD_BUHIN_NO) Then
                                        '        strBLockCheck = xls.GetValue(2, 7 + i)
                                        '        Exit For
                                        '    End If

                                        'Next

                                        ShisakuTehaiKihonVoList = CyushutuImpl.FindByKihonSabun(aListVo.ShisakuEventCode, _
                                                                                                 aListVo.ShisakuListCode, _
                                                                                                 aListVo.ShisakuListCodeKaiteiNo, _
                                                                                                 xls.GetValue(2, 7 + i), _
                                                                                                 xls.GetValue(6, 7 + i))

                                        'If StringUtil.IsEmpty(strBLockCheck) Then
                                        If StringUtil.IsNotEmpty(ShisakuTehaiKihonVoList) Then
                                            'カウントが0の場合は追加扱い
                                            If ShisakuTehaiKihonVoList.Count = 0 Then
                                                'フラグ'
                                                xls2.SetValue(COLUMN_FLAG, lngCnt, COLUMN_FLAG, lngCnt, "追加")
                                                'ブロックNo'
                                                xls2.SetValue(COLUMN_BLOCK_NO, lngCnt, COLUMN_BLOCK_NO, lngCnt, xls.GetValue(COLUMN_BLOCK_NO, 7 + i))
                                                '改訂No'
                                                xls2.SetValue(COLUMN_KAITEI_NO, lngCnt, COLUMN_KAITEI_NO, lngCnt, xls.GetValue(COLUMN_KAITEI_NO, 7 + i))
                                                '専用マーク'
                                                xls2.SetValue(COLUMN_SENYOU_MARK, lngCnt, COLUMN_SENYOU_MARK, lngCnt, xls.GetValue(COLUMN_SENYOU_MARK, 7 + i))
                                                'レベル'
                                                xls2.SetValue(COLUMN_LEVEL, lngCnt, COLUMN_LEVEL, lngCnt, xls.GetValue(COLUMN_LEVEL, 7 + i))
                                                '部品番号'
                                                xls2.SetValue(COLUMN_BUHIN_NO, lngCnt, COLUMN_BUHIN_NO, lngCnt, xls.GetValue(COLUMN_BUHIN_NO, 7 + i))
                                                '試作区分'
                                                xls2.SetValue(COLUMN_SHISAKU_KBN, lngCnt, COLUMN_SHISAKU_KBN, lngCnt, xls.GetValue(COLUMN_SHISAKU_KBN, 7 + i))
                                                '改訂'
                                                xls2.SetValue(COLUMN_KAITEI, lngCnt, COLUMN_KAITEI, lngCnt, xls.GetValue(COLUMN_KAITEI, 7 + i))
                                                '枝番'
                                                xls2.SetValue(COLUMN_EDA_BAN, lngCnt, COLUMN_EDA_BAN, lngCnt, xls.GetValue(COLUMN_EDA_BAN, 7 + i))
                                                '改訂No'
                                                xls2.SetValue(COLUMN_KAITEI_NO, lngCnt, COLUMN_KAITEI_NO, lngCnt, xls.GetValue(COLUMN_KAITEI_NO, 7 + i))
                                                '部品名称'
                                                xls2.SetValue(COLUMN_BUHIN_NAME, lngCnt, COLUMN_BUHIN_NAME, lngCnt, xls.GetValue(COLUMN_BUHIN_NAME, 7 + i))
                                                '集計コード'
                                                xls2.SetValue(COLUMN_SHUKEI_CODE, lngCnt, COLUMN_SHUKEI_CODE, lngCnt, xls.GetValue(COLUMN_SHUKEI_CODE, 7 + i))
                                                '購担'
                                                xls2.SetValue(COLUMN_KOUTAN, lngCnt, COLUMN_KOUTAN, lngCnt, xls.GetValue(COLUMN_KOUTAN, 7 + i))
                                                '取引先コード'
                                                xls2.SetValue(COLUMN_TORIHIKISAKI_CODE, lngCnt, COLUMN_TORIHIKISAKI_CODE, lngCnt, xls.GetValue(COLUMN_TORIHIKISAKI_CODE, 7 + i))
                                                '取引先名称'
                                                xls2.SetValue(COLUMN_TORIHIKISAKI_NAME, lngCnt, COLUMN_TORIHIKISAKI_NAME, lngCnt, xls.GetValue(COLUMN_TORIHIKISAKI_NAME, 7 + i))
                                                '合計員数数量'
                                                xls2.SetValue(COLUMN_TOTAL_INSU, lngCnt, COLUMN_TOTAL_INSU, lngCnt, xls.GetValue(COLUMN_TOTAL_INSU, 7 + i))
                                                '再使用不可'
                                                xls2.SetValue(COLUMN_SAISHIYOUFUKA, lngCnt, COLUMN_SAISHIYOUFUKA, lngCnt, xls.GetValue(COLUMN_SAISHIYOUFUKA, 7 + i))
                                                '供給セクション'
                                                xls2.SetValue(COLUMN_KYOUKU_SECTION, lngCnt, COLUMN_KYOUKU_SECTION, lngCnt, xls.GetValue(COLUMN_KYOUKU_SECTION, 7 + i))
                                                '出荷予定日'
                                                xls2.SetValue(COLUMN_SHUKKAYOTEIBI, lngCnt, COLUMN_SHUKKAYOTEIBI, lngCnt, xls.GetValue(COLUMN_SHUKKAYOTEIBI, 7 + i))
                                                '↓↓2014/09/24 酒井 ADD BEGIN
                                                xls2.SetValue(COLUMN_TSUKURIKATA_SEISAKU, lngCnt, COLUMN_TSUKURIKATA_SEISAKU, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_SEISAKU, 7 + i))
                                                xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, lngCnt, COLUMN_TSUKURIKATA_KATASHIYOU_1, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, 7 + i))
                                                xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, lngCnt, COLUMN_TSUKURIKATA_KATASHIYOU_2, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, 7 + i))
                                                xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, lngCnt, COLUMN_TSUKURIKATA_KATASHIYOU_3, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, 7 + i))
                                                xls2.SetValue(COLUMN_TSUKURIKATA_TIGU, lngCnt, COLUMN_TSUKURIKATA_TIGU, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_TIGU, 7 + i))
                                                xls2.SetValue(COLUMN_TSUKURIKATA_NOUNYU, lngCnt, COLUMN_TSUKURIKATA_NOUNYU, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_NOUNYU, 7 + i))
                                                xls2.SetValue(COLUMN_TSUKURIKATA_KIBO, lngCnt, COLUMN_TSUKURIKATA_KIBO, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KIBO, 7 + i))
                                                '↑↑2014/09/24 酒井 ADD END
                                                '材質規格１'
                                                xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_1, lngCnt, COLUMN_ZAISHITU_KIKAKU_1, lngCnt, xls.GetValue(COLUMN_ZAISHITU_KIKAKU_1, 7 + i))
                                                '材質規格２'
                                                xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_2, lngCnt, COLUMN_ZAISHITU_KIKAKU_2, lngCnt, xls.GetValue(COLUMN_ZAISHITU_KIKAKU_2, 7 + i))
                                                '材質規格３'
                                                xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_3, lngCnt, COLUMN_ZAISHITU_KIKAKU_3, lngCnt, xls.GetValue(COLUMN_ZAISHITU_KIKAKU_3, 7 + i))
                                                '材質メッキ'
                                                xls2.SetValue(COLUMN_ZAISHITU_MEKKI, lngCnt, COLUMN_ZAISHITU_MEKKI, lngCnt, xls.GetValue(COLUMN_ZAISHITU_MEKKI, 7 + i))
                                                '板厚数量'
                                                xls2.SetValue(COLUMN_SHISAKU_BANKO_SURYO, lngCnt, COLUMN_SHISAKU_BANKO_SURYO, lngCnt, xls.GetValue(COLUMN_SHISAKU_BANKO_SURYO, 7 + i))
                                                '板厚数量U'
                                                xls2.SetValue(COLUMN_SHISAKU_BANKO_SURYO_U, lngCnt, COLUMN_SHISAKU_BANKO_SURYO_U, lngCnt, xls.GetValue(COLUMN_SHISAKU_BANKO_SURYO_U, 7 + i))
                                                '試作部品費'
                                                xls2.SetValue(COLUMN_SHISAKU_BUHIN_HI, lngCnt, COLUMN_SHISAKU_BUHIN_HI, lngCnt, xls.GetValue(COLUMN_SHISAKU_BUHIN_HI, 7 + i))
                                                '試作型費'
                                                xls2.SetValue(COLUMN_SHISAKU_KATA_HI, lngCnt, COLUMN_SHISAKU_KATA_HI, lngCnt, xls.GetValue(COLUMN_SHISAKU_KATA_HI, 7 + i))
                                                'NOTE'
                                                xls2.SetValue(COLUMN_NOTE, lngCnt, COLUMN_NOTE, lngCnt, xls.GetValue(COLUMN_NOTE, 7 + i))
                                                '備考'
                                                xls2.SetValue(COLUMN_BIKOU, lngCnt, COLUMN_BIKOU, lngCnt, xls.GetValue(COLUMN_BIKOU, 7 + i))

                                                '員数
                                                For k As Integer = COLUMN_BIKOU + 1 To xls.EndCol
                                                    xls2.SetValue(k, lngCnt, k, lngCnt, xls.GetValue(k, 7 + i))
                                                Next

                                                lngCnt = lngCnt + 1

                                            Else
                                                '同一レコードが有る場合は員数チェック
                                                Dim flgCheck As String = ""

                                                '員数リスト作成
                                                ShisakuTehaiGousyaVoList = CyushutuImpl.FindByGousyaInsu(aListVo.ShisakuEventCode, _
                                                             aListVo.ShisakuListCode, _
                                                             aListVo.ShisakuListCodeKaiteiNo, _
                                                             xls.GetValue(2, 7 + i), _
                                                             ShisakuTehaiKihonVoList(0).BuhinNoHyoujiJun)

                                                For k As Integer = COLUMN_BIKOU + 1 To xls.EndCol

                                                    'DUMMY列,ブランク列は除外。
                                                    If xls.GetValue(k, TITLE_ROW) <> "DUMMY" And _
                                                        StringUtil.IsNotEmpty(xls.GetValue(k, TITLE_ROW)) Then
                                                        '
                                                        Dim intMotoInsu As Integer = 0
                                                        If StringUtil.Equals(xls.GetValue(k, 7 + i), "**") Then
                                                            intMotoInsu = -1
                                                        Else
                                                            If StringUtil.IsNotEmpty(xls.GetValue(k, 7 + i)) Then
                                                                If Validation.IsNumeric(xls.GetValue(k, 7 + i)) Then
                                                                    intMotoInsu = CInt(xls.GetValue(k, 7 + i))
                                                                Else
                                                                    intMotoInsu = 0
                                                                End If
                                                                'intMotoInsu = CInt(xls.GetValue(k, 7 + i))
                                                            End If
                                                        End If
                                                        '号車が一致するか？
                                                        For l As Integer = 0 To ShisakuTehaiGousyaVoList.Count - 1
                                                            If StringUtil.Equals(xls.GetValue(k, TITLE_ROW), _
                                                                                 ShisakuTehaiGousyaVoList(l).ShisakuGousya) Then
                                                                '号車が一致したので員数をチェックする。員数が不一致なら変更とみなす。
                                                                If Not StringUtil.Equals(intMotoInsu, ShisakuTehaiGousyaVoList(l).InsuSuryo) Then
                                                                    flgCheck = "UPD"
                                                                Else
                                                                    flgCheck = "OK"
                                                                End If
                                                                Exit For
                                                            End If
                                                        Next
                                                        '号車が一致しなくても改訂抽出EXCELの員数が０以外なら変更とみなす。
                                                        If StringUtil.Equals(flgCheck, "") And intMotoInsu <> 0 Then
                                                            flgCheck = "UPD"
                                                            Exit For
                                                        End If
                                                    End If

                                                Next

                                                'For k As Integer = COLUMN_BIKOU + 1 To xls.EndCol
                                                '    Dim strGosya As String = xls.GetValue(k, TITLE_ROW)
                                                '    Dim strInsu As String = xls.GetValue(k, 7 + i)
                                                '    'DUMMY列,ブランク列は除外。
                                                '    If strGosya <> "DUMMY" And _
                                                '        StringUtil.IsNotEmpty(strGosya) Then

                                                '        ShisakuTehaiGousyaVoList = CyushutuImpl.FindByGousyaInsu(aListVo.ShisakuEventCode, _
                                                '                     aListVo.ShisakuListCode, _
                                                '                     aListVo.ShisakuListCodeKaiteiNo, _
                                                '                     strBlockNo, _
                                                '                     ShisakuTehaiKihonVoList(0).BuhinNoHyoujiJun, _
                                                '                     strGosya)
                                                '        '
                                                '        Dim intMotoInsu As Integer = 0
                                                '        If StringUtil.Equals(strInsu, "**") Then
                                                '            intMotoInsu = -1
                                                '        Else
                                                '            If StringUtil.IsNotEmpty(strInsu) Then
                                                '                'If Validation.IsNumeric(strInsu) Then
                                                '                '    intMotoInsu = CInt(strInsu)
                                                '                'Else
                                                '                '    intMotoInsu = 0
                                                '                'End If
                                                '                intMotoInsu = CInt(strInsu)
                                                '            End If
                                                '        End If
                                                '        If ShisakuTehaiGousyaVoList.Count > 0 Then
                                                '            If intMotoInsu <> ShisakuTehaiGousyaVoList(0).InsuSuryo Then
                                                '                flgCheck = "UPD"
                                                '                Exit For
                                                '            End If
                                                '        Else
                                                '            If intMotoInsu <> 0 Then
                                                '                flgCheck = "UPD"
                                                '                Exit For
                                                '            End If
                                                '        End If
                                                '    End If

                                                'Next

                                                '変化点がある場合は変更前、変更後を表示する。
                                                If flgCheck = "UPD" Then
                                                    '--------------------------------------------------------------------
                                                    '変更前
                                                    'フラグ'
                                                    xls2.SetValue(COLUMN_FLAG, lngCnt, COLUMN_FLAG, lngCnt, "変更前")
                                                    'ブロックNo'
                                                    xls2.SetValue(COLUMN_BLOCK_NO, lngCnt, COLUMN_BLOCK_NO, lngCnt, xls.GetValue(COLUMN_BLOCK_NO, 7 + i))
                                                    '改訂No'
                                                    xls2.SetValue(COLUMN_KAITEI_NO, lngCnt, COLUMN_KAITEI_NO, lngCnt, xls.GetValue(COLUMN_KAITEI_NO, 7 + i))
                                                    '専用マーク'
                                                    xls2.SetValue(COLUMN_SENYOU_MARK, lngCnt, COLUMN_SENYOU_MARK, lngCnt, xls.GetValue(COLUMN_SENYOU_MARK, 7 + i))
                                                    'レベル'
                                                    xls2.SetValue(COLUMN_LEVEL, lngCnt, COLUMN_LEVEL, lngCnt, xls.GetValue(COLUMN_LEVEL, 7 + i))
                                                    '部品番号'
                                                    xls2.SetValue(COLUMN_BUHIN_NO, lngCnt, COLUMN_BUHIN_NO, lngCnt, xls.GetValue(COLUMN_BUHIN_NO, 7 + i))
                                                    '試作区分'
                                                    xls2.SetValue(COLUMN_SHISAKU_KBN, lngCnt, COLUMN_SHISAKU_KBN, lngCnt, xls.GetValue(COLUMN_SHISAKU_KBN, 7 + i))
                                                    '改訂'
                                                    xls2.SetValue(COLUMN_KAITEI, lngCnt, COLUMN_KAITEI, lngCnt, xls.GetValue(COLUMN_KAITEI, 7 + i))
                                                    '枝番'
                                                    xls2.SetValue(COLUMN_EDA_BAN, lngCnt, COLUMN_EDA_BAN, lngCnt, xls.GetValue(COLUMN_EDA_BAN, 7 + i))
                                                    '改訂No'
                                                    xls2.SetValue(COLUMN_KAITEI_NO, lngCnt, COLUMN_KAITEI_NO, lngCnt, xls.GetValue(COLUMN_KAITEI_NO, 7 + i))
                                                    '部品名称'
                                                    xls2.SetValue(COLUMN_BUHIN_NAME, lngCnt, COLUMN_BUHIN_NAME, lngCnt, xls.GetValue(COLUMN_BUHIN_NAME, 7 + i))
                                                    '集計コード'
                                                    xls2.SetValue(COLUMN_SHUKEI_CODE, lngCnt, COLUMN_SHUKEI_CODE, lngCnt, xls.GetValue(COLUMN_SHUKEI_CODE, 7 + i))
                                                    '購担'
                                                    xls2.SetValue(COLUMN_KOUTAN, lngCnt, COLUMN_KOUTAN, lngCnt, xls.GetValue(COLUMN_KOUTAN, 7 + i))
                                                    '取引先コード'
                                                    xls2.SetValue(COLUMN_TORIHIKISAKI_CODE, lngCnt, COLUMN_TORIHIKISAKI_CODE, lngCnt, xls.GetValue(COLUMN_TORIHIKISAKI_CODE, 7 + i))
                                                    '取引先名称'
                                                    xls2.SetValue(COLUMN_TORIHIKISAKI_NAME, lngCnt, COLUMN_TORIHIKISAKI_NAME, lngCnt, xls.GetValue(COLUMN_TORIHIKISAKI_NAME, 7 + i))
                                                    '合計員数数量'
                                                    xls2.SetValue(COLUMN_TOTAL_INSU, lngCnt, COLUMN_TOTAL_INSU, lngCnt, xls.GetValue(COLUMN_TOTAL_INSU, 7 + i))
                                                    '再使用不可'
                                                    xls2.SetValue(COLUMN_SAISHIYOUFUKA, lngCnt, COLUMN_SAISHIYOUFUKA, lngCnt, xls.GetValue(COLUMN_SAISHIYOUFUKA, 7 + i))
                                                    '供給セクション'
                                                    xls2.SetValue(COLUMN_KYOUKU_SECTION, lngCnt, COLUMN_KYOUKU_SECTION, lngCnt, xls.GetValue(COLUMN_KYOUKU_SECTION, 7 + i))
                                                    '出荷予定日'
                                                    xls2.SetValue(COLUMN_SHUKKAYOTEIBI, lngCnt, COLUMN_SHUKKAYOTEIBI, lngCnt, xls.GetValue(COLUMN_SHUKKAYOTEIBI, 7 + i))

                                                    '↓↓2014/09/24 酒井 ADD BEGIN
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_SEISAKU, lngCnt, COLUMN_TSUKURIKATA_SEISAKU, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_SEISAKU, 7 + i))
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, lngCnt, COLUMN_TSUKURIKATA_KATASHIYOU_1, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, 7 + i))
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, lngCnt, COLUMN_TSUKURIKATA_KATASHIYOU_2, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, 7 + i))
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, lngCnt, COLUMN_TSUKURIKATA_KATASHIYOU_3, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, 7 + i))
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_TIGU, lngCnt, COLUMN_TSUKURIKATA_TIGU, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_TIGU, 7 + i))
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_NOUNYU, lngCnt, COLUMN_TSUKURIKATA_NOUNYU, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_NOUNYU, 7 + i))
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_KIBO, lngCnt, COLUMN_TSUKURIKATA_KIBO, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KIBO, 7 + i))
                                                    '↑↑2014/09/24 酒井 ADD END

                                                    '材質規格１'
                                                    xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_1, lngCnt, COLUMN_ZAISHITU_KIKAKU_1, lngCnt, xls.GetValue(COLUMN_ZAISHITU_KIKAKU_1, 7 + i))
                                                    '材質規格２'
                                                    xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_2, lngCnt, COLUMN_ZAISHITU_KIKAKU_2, lngCnt, xls.GetValue(COLUMN_ZAISHITU_KIKAKU_2, 7 + i))
                                                    '材質規格３'
                                                    xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_3, lngCnt, COLUMN_ZAISHITU_KIKAKU_3, lngCnt, xls.GetValue(COLUMN_ZAISHITU_KIKAKU_3, 7 + i))
                                                    '材質メッキ'
                                                    xls2.SetValue(COLUMN_ZAISHITU_MEKKI, lngCnt, COLUMN_ZAISHITU_MEKKI, lngCnt, xls.GetValue(COLUMN_ZAISHITU_MEKKI, 7 + i))
                                                    '板厚数量'
                                                    xls2.SetValue(COLUMN_SHISAKU_BANKO_SURYO, lngCnt, COLUMN_SHISAKU_BANKO_SURYO, lngCnt, xls.GetValue(COLUMN_SHISAKU_BANKO_SURYO, 7 + i))
                                                    '板厚数量U'
                                                    xls2.SetValue(COLUMN_SHISAKU_BANKO_SURYO_U, lngCnt, COLUMN_SHISAKU_BANKO_SURYO_U, lngCnt, xls.GetValue(COLUMN_SHISAKU_BANKO_SURYO_U, 7 + i))
                                                    '試作部品費'
                                                    xls2.SetValue(COLUMN_SHISAKU_BUHIN_HI, lngCnt, COLUMN_SHISAKU_BUHIN_HI, lngCnt, xls.GetValue(COLUMN_SHISAKU_BUHIN_HI, 7 + i))
                                                    '試作型費'
                                                    xls2.SetValue(COLUMN_SHISAKU_KATA_HI, lngCnt, COLUMN_SHISAKU_KATA_HI, lngCnt, xls.GetValue(COLUMN_SHISAKU_KATA_HI, 7 + i))
                                                    'NOTE'
                                                    xls2.SetValue(COLUMN_NOTE, lngCnt, COLUMN_NOTE, lngCnt, xls.GetValue(COLUMN_NOTE, 7 + i))
                                                    '備考'
                                                    xls2.SetValue(COLUMN_BIKOU, lngCnt, COLUMN_BIKOU, lngCnt, xls.GetValue(COLUMN_BIKOU, 7 + i))
                                                    '員数
                                                    Dim intInsu As Integer = 0
                                                    Dim flgInsu As String = ""
                                                    ''員数リスト作成
                                                    'ShisakuTehaiGousyaVoList = CyushutuImpl.FindByGousyaInsu(aListVo.ShisakuEventCode, _
                                                    '             aListVo.ShisakuListCode, _
                                                    '             aListVo.ShisakuListCodeKaiteiNo, _
                                                    '             xls.GetValue(2, 7 + i), _
                                                    '             ShisakuTehaiKihonVoList(0).BuhinNoHyoujiJun)

                                                    For k As Integer = COLUMN_BIKOU + 1 To xls.EndCol

                                                        If ShisakuTehaiGousyaVoList.Count > 0 Then

                                                            '号車が一致するか？
                                                            For l As Integer = 0 To ShisakuTehaiGousyaVoList.Count - 1
                                                                If StringUtil.Equals(xls.GetValue(k, TITLE_ROW), _
                                                                                     ShisakuTehaiGousyaVoList(l).ShisakuGousya) Then

                                                                    '号車が一致したので員数を出力する。
                                                                    If ShisakuTehaiGousyaVoList(l).InsuSuryo < 0 Then
                                                                        xls2.SetValue(k, lngCnt, k, lngCnt, "**")
                                                                        flgInsu = "**"
                                                                    Else
                                                                        If ShisakuTehaiGousyaVoList(l).InsuSuryo <> 0 Then
                                                                            xls2.SetValue(k, lngCnt, k, lngCnt, _
                                                                                          ShisakuTehaiGousyaVoList(l).InsuSuryo)
                                                                            intInsu = intInsu + ShisakuTehaiGousyaVoList(l).InsuSuryo
                                                                        End If
                                                                    End If

                                                                    Exit For

                                                                End If
                                                            Next
                                                        End If

                                                    Next
                                                    '合計員数数量'
                                                    If flgInsu = "**" Then
                                                        xls2.SetValue(COLUMN_TOTAL_INSU, lngCnt, COLUMN_TOTAL_INSU, lngCnt, "**")
                                                    Else
                                                        xls2.SetValue(COLUMN_TOTAL_INSU, lngCnt, COLUMN_TOTAL_INSU, lngCnt, intInsu)
                                                    End If

                                                    'カウント
                                                    lngCnt = lngCnt + 1

                                                    '--------------------------------------------------------------------
                                                    '変更後
                                                    'フラグ'
                                                    xls2.SetValue(COLUMN_FLAG, lngCnt, COLUMN_FLAG, lngCnt, "変更後")
                                                    'ブロックNo'
                                                    xls2.SetValue(COLUMN_BLOCK_NO, lngCnt, COLUMN_BLOCK_NO, lngCnt, xls.GetValue(COLUMN_BLOCK_NO, 7 + i))
                                                    '改訂No'
                                                    xls2.SetValue(COLUMN_KAITEI_NO, lngCnt, COLUMN_KAITEI_NO, lngCnt, xls.GetValue(COLUMN_KAITEI_NO, 7 + i))
                                                    '専用マーク'
                                                    xls2.SetValue(COLUMN_SENYOU_MARK, lngCnt, COLUMN_SENYOU_MARK, lngCnt, xls.GetValue(COLUMN_SENYOU_MARK, 7 + i))
                                                    'レベル'
                                                    xls2.SetValue(COLUMN_LEVEL, lngCnt, COLUMN_LEVEL, lngCnt, xls.GetValue(COLUMN_LEVEL, 7 + i))
                                                    '部品番号'
                                                    xls2.SetValue(COLUMN_BUHIN_NO, lngCnt, COLUMN_BUHIN_NO, lngCnt, xls.GetValue(COLUMN_BUHIN_NO, 7 + i))
                                                    '試作区分'
                                                    xls2.SetValue(COLUMN_SHISAKU_KBN, lngCnt, COLUMN_SHISAKU_KBN, lngCnt, xls.GetValue(COLUMN_SHISAKU_KBN, 7 + i))
                                                    '改訂'
                                                    xls2.SetValue(COLUMN_KAITEI, lngCnt, COLUMN_KAITEI, lngCnt, xls.GetValue(COLUMN_KAITEI, 7 + i))
                                                    '枝番'
                                                    xls2.SetValue(COLUMN_EDA_BAN, lngCnt, COLUMN_EDA_BAN, lngCnt, xls.GetValue(COLUMN_EDA_BAN, 7 + i))
                                                    '改訂No'
                                                    xls2.SetValue(COLUMN_KAITEI_NO, lngCnt, COLUMN_KAITEI_NO, lngCnt, xls.GetValue(COLUMN_KAITEI_NO, 7 + i))
                                                    '部品名称'
                                                    xls2.SetValue(COLUMN_BUHIN_NAME, lngCnt, COLUMN_BUHIN_NAME, lngCnt, xls.GetValue(COLUMN_BUHIN_NAME, 7 + i))
                                                    '集計コード'
                                                    xls2.SetValue(COLUMN_SHUKEI_CODE, lngCnt, COLUMN_SHUKEI_CODE, lngCnt, xls.GetValue(COLUMN_SHUKEI_CODE, 7 + i))
                                                    '購担'
                                                    xls2.SetValue(COLUMN_KOUTAN, lngCnt, COLUMN_KOUTAN, lngCnt, xls.GetValue(COLUMN_KOUTAN, 7 + i))
                                                    '取引先コード'
                                                    xls2.SetValue(COLUMN_TORIHIKISAKI_CODE, lngCnt, COLUMN_TORIHIKISAKI_CODE, lngCnt, xls.GetValue(COLUMN_TORIHIKISAKI_CODE, 7 + i))
                                                    '取引先名称'
                                                    xls2.SetValue(COLUMN_TORIHIKISAKI_NAME, lngCnt, COLUMN_TORIHIKISAKI_NAME, lngCnt, xls.GetValue(COLUMN_TORIHIKISAKI_NAME, 7 + i))
                                                    '合計員数数量'
                                                    xls2.SetValue(COLUMN_TOTAL_INSU, lngCnt, COLUMN_TOTAL_INSU, lngCnt, xls.GetValue(COLUMN_TOTAL_INSU, 7 + i))
                                                    '再使用不可'
                                                    xls2.SetValue(COLUMN_SAISHIYOUFUKA, lngCnt, COLUMN_SAISHIYOUFUKA, lngCnt, xls.GetValue(COLUMN_SAISHIYOUFUKA, 7 + i))
                                                    '供給セクション'
                                                    xls2.SetValue(COLUMN_KYOUKU_SECTION, lngCnt, COLUMN_KYOUKU_SECTION, lngCnt, xls.GetValue(COLUMN_KYOUKU_SECTION, 7 + i))
                                                    '出荷予定日'
                                                    xls2.SetValue(COLUMN_SHUKKAYOTEIBI, lngCnt, COLUMN_SHUKKAYOTEIBI, lngCnt, xls.GetValue(COLUMN_SHUKKAYOTEIBI, 7 + i))

                                                    '↓↓2014/09/24 酒井 ADD BEGIN
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_SEISAKU, lngCnt, COLUMN_TSUKURIKATA_SEISAKU, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_SEISAKU, 7 + i))
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, lngCnt, COLUMN_TSUKURIKATA_KATASHIYOU_1, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KATASHIYOU_1, 7 + i))
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, lngCnt, COLUMN_TSUKURIKATA_KATASHIYOU_2, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KATASHIYOU_2, 7 + i))
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, lngCnt, COLUMN_TSUKURIKATA_KATASHIYOU_3, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KATASHIYOU_3, 7 + i))
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_TIGU, lngCnt, COLUMN_TSUKURIKATA_TIGU, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_TIGU, 7 + i))
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_NOUNYU, lngCnt, COLUMN_TSUKURIKATA_NOUNYU, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_NOUNYU, 7 + i))
                                                    xls2.SetValue(COLUMN_TSUKURIKATA_KIBO, lngCnt, COLUMN_TSUKURIKATA_KIBO, lngCnt, xls.GetValue(COLUMN_TSUKURIKATA_KIBO, 7 + i))
                                                    '↑↑2014/09/24 酒井 ADD END

                                                    '材質規格１'
                                                    xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_1, lngCnt, COLUMN_ZAISHITU_KIKAKU_1, lngCnt, xls.GetValue(COLUMN_ZAISHITU_KIKAKU_1, 7 + i))
                                                    '材質規格２'
                                                    xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_2, lngCnt, COLUMN_ZAISHITU_KIKAKU_2, lngCnt, xls.GetValue(COLUMN_ZAISHITU_KIKAKU_2, 7 + i))
                                                    '材質規格３'
                                                    xls2.SetValue(COLUMN_ZAISHITU_KIKAKU_3, lngCnt, COLUMN_ZAISHITU_KIKAKU_3, lngCnt, xls.GetValue(COLUMN_ZAISHITU_KIKAKU_3, 7 + i))
                                                    '材質メッキ'
                                                    xls2.SetValue(COLUMN_ZAISHITU_MEKKI, lngCnt, COLUMN_ZAISHITU_MEKKI, lngCnt, xls.GetValue(COLUMN_ZAISHITU_MEKKI, 7 + i))
                                                    '板厚数量'
                                                    xls2.SetValue(COLUMN_SHISAKU_BANKO_SURYO, lngCnt, COLUMN_SHISAKU_BANKO_SURYO, lngCnt, xls.GetValue(COLUMN_SHISAKU_BANKO_SURYO, 7 + i))
                                                    '板厚数量U'
                                                    xls2.SetValue(COLUMN_SHISAKU_BANKO_SURYO_U, lngCnt, COLUMN_SHISAKU_BANKO_SURYO_U, lngCnt, xls.GetValue(COLUMN_SHISAKU_BANKO_SURYO_U, 7 + i))
                                                    '試作部品費'
                                                    xls2.SetValue(COLUMN_SHISAKU_BUHIN_HI, lngCnt, COLUMN_SHISAKU_BUHIN_HI, lngCnt, xls.GetValue(COLUMN_SHISAKU_BUHIN_HI, 7 + i))
                                                    '試作型費'
                                                    xls2.SetValue(COLUMN_SHISAKU_KATA_HI, lngCnt, COLUMN_SHISAKU_KATA_HI, lngCnt, xls.GetValue(COLUMN_SHISAKU_KATA_HI, 7 + i))
                                                    'NOTE'
                                                    xls2.SetValue(COLUMN_NOTE, lngCnt, COLUMN_NOTE, lngCnt, xls.GetValue(COLUMN_NOTE, 7 + i))
                                                    '備考'
                                                    xls2.SetValue(COLUMN_BIKOU, lngCnt, COLUMN_BIKOU, lngCnt, xls.GetValue(COLUMN_BIKOU, 7 + i))

                                                    intInsu = 0
                                                    flgInsu = ""
                                                    '員数
                                                    For k As Integer = COLUMN_BIKOU + 1 To xls.EndCol
                                                        xls2.SetValue(k, lngCnt, k, lngCnt, xls.GetValue(k, 7 + i))

                                                        If StringUtil.IsNotEmpty(xls.GetValue(k, 7 + i)) Then
                                                            Dim strInsu As String = CStr(xls.GetValue(k, 7 + i))

                                                            If strInsu = "**" Then
                                                                flgInsu = "**"
                                                            Else
                                                                If Validation.IsNumeric(xls.GetValue(k, 7 + i)) Then
                                                                    intInsu = intInsu + xls.GetValue(k, 7 + i)
                                                                End If
                                                            End If
                                                        End If

                                                    Next
                                                    '合計員数数量'
                                                    If flgInsu = "**" Then
                                                        xls2.SetValue(COLUMN_TOTAL_INSU, lngCnt, COLUMN_TOTAL_INSU, lngCnt, "**")
                                                    Else
                                                        xls2.SetValue(COLUMN_TOTAL_INSU, lngCnt, COLUMN_TOTAL_INSU, lngCnt, intInsu)
                                                    End If

                                                    'カウント
                                                    lngCnt = lngCnt + 1
                                                End If

                                            End If
                                        End If
                                    End If

                                End If

                                'If i = 0 Then
                                '    strHoldBlockNo = xls.GetValue(2, 7 + i)
                                'End If


                                'If i = 0 Or strHoldBlockNo <> xls.GetValue(2, 7 + i) Then
                                '    strBLockCheck = ""
                                '    For j As Integer = 0 To dtBase.Rows.Count - 1

                                '        If xls.GetValue(2, 7 + i) = dtBase.Rows(j)(NmDTColBase.TD_SHISAKU_BLOCK_NO) Then
                                '            strBLockCheck = xls.GetValue(2, 7 + i)
                                '            Exit For
                                '        End If

                                '    Next

                                '    strHoldBlockNo = xls.GetValue(2, 7 + i)
                                'End If


                                'If StringUtil.IsEmpty(strBLockCheck) Then
                                '    'フラグ'
                                '    xls2.SetValue(1, lngCnt, 1, lngCnt, xls.GetValue(1, 7 + i))
                                '    'ブロックNo'
                                '    xls2.SetValue(2, lngCnt, 2, lngCnt, xls.GetValue(2, 7 + i))
                                '    '改訂No'
                                '    xls2.SetValue(3, lngCnt, 3, lngCnt, xls.GetValue(3, 7 + i))

                                '    lngCnt = lngCnt + 1

                                'End If

                                ''出力処理へ
                                'xls2.SetValue(COLUMN_BLOCK_NO, START_ROW + rowIndex, aKihonList(index).ShisakuBlockNo)
                                ''行ID'
                                'xls2.SetValue(COLUMN_GYOU_ID, START_ROW + rowIndex, aKihonList(index).GyouId)
                                ''専用マーク'
                                'xls2.SetValue(COLUMN_SENYOU_MARK, START_ROW + rowIndex, aKihonList(index).SenyouMark)

                            End If

                        Next

                        '列の幅を自動調整する'
                        xls2.AutoFitCol(COLUMN_FLAG, xls2.EndCol())

                        'A3横で印刷できるように変更'
                        xls2.PrintPaper(fileNameOut, 2, "A3")
                        xls2.PrintOrientation(fileNameOut, 2, 1, False)
                        xls2.SetActiveSheet(1)
                        xls2.Save()

                    End Using

                End Using

            End If

            Process.Start(fileNameOut)

            ComFunc.ShowInfoMsgBox("Excel出力が完了しました", MessageBoxButtons.OK)


            Return True

        End Function

#End Region

#Region "明細部各列名の設定"
        '' フラグ
        Private COLUMN_FLAG As Integer
        '' ブロックNo
        Private COLUMN_BLOCK_NO As Integer
        '' 改訂No
        Private COLUMN_KAITEI_NO As Integer
        '' 専用マーク
        Private COLUMN_SENYOU_MARK As Integer
        '' レベル
        Private COLUMN_LEVEL As Integer
        '' 部品番号
        Private COLUMN_BUHIN_NO As Integer
        '' 試作区分
        Private COLUMN_SHISAKU_KBN As Integer
        '' 改訂
        Private COLUMN_KAITEI As Integer
        '' 枝番
        Private COLUMN_EDA_BAN As Integer
        '' 部品名称
        Private COLUMN_BUHIN_NAME As Integer
        '' 集計コード
        Private COLUMN_SHUKEI_CODE As Integer
        '' 購担
        Private COLUMN_KOUTAN As Integer
        '' 取引先コード
        Private COLUMN_TORIHIKISAKI_CODE As Integer
        '' 取引先名称
        Private COLUMN_TORIHIKISAKI_NAME As Integer
        '' 合計員数
        Private COLUMN_TOTAL_INSU As Integer
        '' 再使用不可
        Private COLUMN_SAISHIYOUFUKA As Integer
        '' 供給セクション
        Private COLUMN_KYOUKU_SECTION As Integer
        '' 出荷予定日
        Private COLUMN_SHUKKAYOTEIBI As Integer
        '↓↓2014/09/24 酒井 ADD BEGIN
        Private COLUMN_TSUKURIKATA_SEISAKU As Integer
        Private COLUMN_TSUKURIKATA_KATASHIYOU_1 As Integer
        Private COLUMN_TSUKURIKATA_KATASHIYOU_2 As Integer
        Private COLUMN_TSUKURIKATA_KATASHIYOU_3 As Integer
        Private COLUMN_TSUKURIKATA_TIGU As Integer
        Private COLUMN_TSUKURIKATA_NOUNYU As Integer
        Private COLUMN_TSUKURIKATA_KIBO As Integer
        Private COLUMN_BASE_BUHIN_FLG As Integer
        '↑↑2014/09/24 酒井 ADD END
        '' 材質規格１
        Private COLUMN_ZAISHITU_KIKAKU_1 As Integer
        '' 材質規格２
        Private COLUMN_ZAISHITU_KIKAKU_2 As Integer
        '' 材質規格３
        Private COLUMN_ZAISHITU_KIKAKU_3 As Integer
        '' 材質メッキ
        Private COLUMN_ZAISHITU_MEKKI As Integer
        '' 板厚
        Private COLUMN_SHISAKU_BANKO_SURYO As Integer
        '' 板厚U
        Private COLUMN_SHISAKU_BANKO_SURYO_U As Integer
        '' 試作部品費
        Private COLUMN_SHISAKU_BUHIN_HI As Integer
        '' 試作型費
        Private COLUMN_SHISAKU_KATA_HI As Integer
        '' NOTE
        Private COLUMN_NOTE As Integer
        '' 備考
        Private COLUMN_BIKOU As Integer
        '' 員数
        Private COLUMN_INSU As Integer

#End Region

        ''' <summary>
        ''' 各カラム名に数値を設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetColumnNo()
            Dim column As Integer = 1
            COLUMN_FLAG = EzUtil.Increment(column)
            COLUMN_BLOCK_NO = EzUtil.Increment(column)
            COLUMN_KAITEI_NO = EzUtil.Increment(column)
            COLUMN_SENYOU_MARK = EzUtil.Increment(column)
            COLUMN_LEVEL = EzUtil.Increment(column)
            COLUMN_BUHIN_NO = EzUtil.Increment(column)
            COLUMN_SHISAKU_KBN = EzUtil.Increment(column)
            COLUMN_KAITEI = EzUtil.Increment(column)
            COLUMN_EDA_BAN = EzUtil.Increment(column)
            COLUMN_BUHIN_NAME = EzUtil.Increment(column)
            COLUMN_SHUKEI_CODE = EzUtil.Increment(column)
            COLUMN_KOUTAN = EzUtil.Increment(column)
            COLUMN_TORIHIKISAKI_CODE = EzUtil.Increment(column)
            COLUMN_TORIHIKISAKI_NAME = EzUtil.Increment(column)
            COLUMN_TOTAL_INSU = EzUtil.Increment(column)
            COLUMN_SAISHIYOUFUKA = EzUtil.Increment(column)
            COLUMN_KYOUKU_SECTION = EzUtil.Increment(column)
            COLUMN_SHUKKAYOTEIBI = EzUtil.Increment(column)
            '↓↓2014/09/24 酒井 ADD BEGIN
            COLUMN_TSUKURIKATA_SEISAKU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU_1 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU_2 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KATASHIYOU_3 = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_TIGU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_NOUNYU = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_KIBO = EzUtil.Increment(column)
            'COLUMN_BASE_BUHIN_FLG  = EzUtil.Increment(column)
            '↑↑2014/09/24 酒井 ADD END
            COLUMN_ZAISHITU_KIKAKU_1 = EzUtil.Increment(column)
            COLUMN_ZAISHITU_KIKAKU_2 = EzUtil.Increment(column)
            COLUMN_ZAISHITU_KIKAKU_3 = EzUtil.Increment(column)
            COLUMN_ZAISHITU_MEKKI = EzUtil.Increment(column)
            COLUMN_SHISAKU_BANKO_SURYO = EzUtil.Increment(column)
            COLUMN_SHISAKU_BANKO_SURYO_U = EzUtil.Increment(column)
            COLUMN_SHISAKU_BUHIN_HI = EzUtil.Increment(column)
            COLUMN_SHISAKU_KATA_HI = EzUtil.Increment(column)
            COLUMN_NOTE = EzUtil.Increment(column)
            COLUMN_BIKOU = EzUtil.Increment(column)
            COLUMN_INSU = EzUtil.Increment(column)

        End Sub

    End Class

    Public NotInheritable Class Validation

#Region "　IsNumeric メソッド (+1)　"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        '''     文字列が数値であるかどうかを返します。</summary>
        ''' <param name="stTarget">
        '''     検査対象となる文字列。</param>
        ''' <returns>
        '''     指定した文字列が数値であれば True。それ以外は False。</returns>
        ''' -----------------------------------------------------------------------------
        Public Overloads Shared Function IsNumeric(ByVal stTarget As String) As Boolean
            Return Double.TryParse( _
                stTarget, _
                System.Globalization.NumberStyles.Any, _
                Nothing, _
                0.0# _
            )
        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        '''     オブジェクトが数値であるかどうかを返します。</summary>
        ''' <param name="oTarget">
        '''     検査対象となるオブジェクト。</param>
        ''' <returns>
        '''     指定したオブジェクトが数値であれば True。それ以外は False。</returns>
        ''' -----------------------------------------------------------------------------
        Public Overloads Shared Function IsNumeric(ByVal oTarget As Object) As Boolean
            Return IsNumeric(oTarget.ToString())
        End Function

#End Region

    End Class
End Namespace

