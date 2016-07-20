Imports EventSakusei.ShisakuBuhinEditBlock
Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Data
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEditSekkei.Dao
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Ui.Spd


Namespace ShisakuBuhinEditSekkei
    ''' <summary>
    ''' 試作部品表 編集・改訂編集(設計)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm37DispShisakuBuhinEditSekkei
        Friend strMode As String  'モード
        Friend strEventCode As String  'イベントコード
        Friend strTeihaiDate As String '手配帳作成日
        Private m_spCom As SpreadCommon

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)

        End Sub

        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            Me.Close()
        End Sub

        Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
            Application.Exit()
            System.Environment.Exit(0)
        End Sub

        Private Sub Frm37DispShisakuBuhinEditSekkei_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            spdParts.FocusRenderer = New FarPoint.Win.Spread.MarqueeFocusIndicatorRenderer(Color.Blue, 1)
            '初期起動時には以下のボタンを使用不可とする。
            '
            btnCall.ForeColor = Color.Black
            btnCall.BackColor = Color.White
            btnCall.Enabled = False
            ShisakuFormUtil.setTitleVersion(Me)
            '日付の設定
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
            '部課とIDの設定
            ShisakuFormUtil.SetIdAndBuka(LblCurrUserId, LblCurrBukaName)
            'Spreadの初期化
            SpreadUtil.Initialize(spdParts)
            '設計課の設定
            ShisakuFormUtil.SettingDefaultProperty(txtSekeika)
            txtSekeika.MaxLength = 4

            Inital()
        End Sub

        Private Sub Inital()
            '画面オープン時進度状況を設定する
            initShindo()
            '画面オープン時イベント情報を設定する
            initIbento()
            '*****完了閲覧モード対応*****
            If strMode = ShishakuKanryoViewMode Then
                txtSekeika.Enabled = False
            Else
                txtSekeika.Enabled = True
            End If
            'スプレッドデータを設定する
            initSpreadData()
            'スプレッド設定
            setspdStyle()
        End Sub

        Private Sub btnCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCall.Click

            '呼び出しのときの確認は不要です。　6/17
            'frm01Kakunin.lblKakunin.Text = "設計課を呼出します。"
            'frm37Para = "btnCall"
            'frm01Kakunin.ShowDialog()
            'Select Case frm37ParaModori
            '    Case "1"
            'Frm38DispShisakuBuhinEditBlock.ShowDialog()
            '    Case Else
            'End Select

            'CallShisakuBuhinEditBlock(spdParts_Sheet1.ActiveCell.Row.Index)

            '入力された値でブロック情報を取得する 樺澤'
            Me.Hide()
            CallShisakuBuhinEditBlocktxt(txtSekeika.Text)

            'ダブルクリック時と同じ処理にしてみる。　2001/07/25　By柳沼
            'Me.CallShisakuBuhinEditBlock(Integer.Parse(txtSekeika.Text))

            Me.Show()

            '---------------------------------------------------------------
            '２次改修で追加。
            '   排他処理でステータスが更新されていた場合、自画面も閉じる。
            '以下のパラメータの場合自分も閉じる。
            If frm37ParaModori = "close" Then
                Me.Close()
            End If
            '---------------------------------------------------------------

        End Sub

        'テキストに入力された値でブロック情報を取得する処理'
        Private Sub CallShisakuBuhinEditBlocktxt(ByVal txt As String)
            Using CallShisakuBuhinEditBlocktxt As New Frm38DispShisakuBuhinEditBlock
                Dim Bu_Code As String = ""
                Dim Ka_Code As String = ""
                Dim BuKaComonFunc As New ShisakuBuhinEditBlocktxtDaoImpl
                'モード
                CallShisakuBuhinEditBlocktxt.StrMode = strMode
                'イベントコード
                CallShisakuBuhinEditBlocktxt.StrEventCode = strEventCode
                '開発符号
                CallShisakuBuhinEditBlocktxt.StrFugo = lblKaihatuFugou.Text
                'イベント名
                CallShisakuBuhinEditBlocktxt.StrEventName = lblIbentoName.Text
                '処置期限
                CallShisakuBuhinEditBlocktxt.StrDate = lblSyochikigen.Text
                'あとXX日
                CallShisakuBuhinEditBlocktxt.StrPeriod = lblSyochikigen2.Text
                '部課コード
                'テキストに入力された値で部課コードを取得してくる
                Try
                    Dim test = BuKaComonFunc.GetBuKaCode(txt)
                    Bu_Code = test.BuCode
                    Ka_Code = test.KaCode

                    '--------------------------------------------------------------------------
                    '２次改修
                    '   課略称名か部課コードかチェックする。
                    '   部課略称で登録されている場合には略称をセット、
                    '   部課コードで登録されている場合には部課コードをセットする。
                    Dim shisakuSekkeiBlock As New ShisakuBuhinEditBlocktxtDaoImpl
                    Dim blockList = shisakuSekkeiBlock.GetBlock(strEventCode, txt)
                    If blockList.Count = 0 Then
                        CallShisakuBuhinEditBlocktxt.StrDept = Bu_Code + Ka_Code
                    Else
                        CallShisakuBuhinEditBlocktxt.StrDept = txt
                    End If
                    '--------------------------------------------------------------------------

                    'CallShisakuBuhinEditBlocktxt.StrDept = Bu_Code + Ka_Code


                    ''エラーチェック TODO エラー処理用が必要かも知れない
                    'If String.IsNullOrEmpty(Bu_Code) Or String.IsNullOrEmpty(Ka_Code) Then
                    '    'エラーメッセージを出す
                    '    MsgBox("設計課が存在しません。", MsgBoxStyle.OkOnly)
                    '    Return
                    'End If
                Catch Exception As NullReferenceException
                    'MsgBox("設計課が存在しません。", MsgBoxStyle.OkOnly)
                    'Return
                    CallShisakuBuhinEditBlocktxt.StrDept = txt
                End Try
                ''課略称
                CallShisakuBuhinEditBlocktxt.StrDeptNo = txt
                'SKE1最終抽出日
                CallShisakuBuhinEditBlocktxt.StrOutDate = strTeihaiDate
                'テキストボックスの設計課情報を次画面へ引き渡す。
                CallShisakuBuhinEditBlocktxt.Execute()
                CallShisakuBuhinEditBlocktxt.ShowDialog()
            End Using
            Inital()
        End Sub

        Private Sub CallShisakuBuhinEditBlock(ByVal rowNo As Integer)
            ''Me.Hide()
            Using frmShisakuBuhinEditBlock As New Frm38DispShisakuBuhinEditBlock
                Dim spreadComonFucn = New SpreadCommon(spdParts)
                'モード
                frmShisakuBuhinEditBlock.StrMode = strMode
                'イベントコード
                frmShisakuBuhinEditBlock.StrEventCode = strEventCode
                '開発符号
                frmShisakuBuhinEditBlock.StrFugo = lblKaihatuFugou.Text
                'イベント名
                frmShisakuBuhinEditBlock.StrEventName = lblIbentoName.Text
                '処置期限
                frmShisakuBuhinEditBlock.StrDate = lblSyochikigen.Text
                'あとXX日
                frmShisakuBuhinEditBlock.StrPeriod = lblSyochikigen2.Text
                '部課コード
                frmShisakuBuhinEditBlock.StrDept = spreadComonFucn.GetValue(TAG_BUKA_CODE, rowNo)
                ''課略称
                frmShisakuBuhinEditBlock.StrDeptNo = spreadComonFucn.GetValue(TAG_SEKKEIKA, rowNo)
                'SKE1最終抽出日
                frmShisakuBuhinEditBlock.StrOutDate = strTeihaiDate
                'ダブルクリックすると次画面へ遷移する。
                'ダブルクリック行の設計課情報を次画面へ引き渡す。
                frmShisakuBuhinEditBlock.Execute()
                frmShisakuBuhinEditBlock.ShowDialog()
            End Using
            Inital()
            ''Me.Show()
        End Sub

        '設計課入力欄に何か入力された場合の処理'
        Private Sub txtSekeika_Change() Handles txtSekeika.TextChanged
            '設計課入力時は選択ボタンを使用可能へ
            If txtSekeika.Text <> "" Then
                btnCall.ForeColor = Color.Black
                btnCall.BackColor = Color.LightCyan
                btnCall.Enabled = True
            Else
                btnCall.ForeColor = Color.Black
                btnCall.BackColor = Color.White
                btnCall.Enabled = False
            End If
        End Sub

        Private Sub spdParts_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellClick
            '2011/7/25　表示行番号も取得　By柳沼
            lblSekkeika.Text = e.Row
        End Sub

        Private Sub spdParts_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles spdParts.LeaveCell
            If e.NewRow = -1 OrElse e.NewColumn = -1 Then
                Return
            End If

            ' 選択範囲の情報を表示します。
            txtSekeika.Text = spdParts.ActiveSheet.GetText(e.NewRow, spdParts_Sheet1.Columns(TAG_SEKKEIKA).Index)

            '設計課入力時は選択ボタンを使用可能へ
            If txtSekeika.Text <> "" Then
                '
                btnCall.ForeColor = Color.Black
                btnCall.BackColor = Color.LightCyan
                btnCall.Enabled = True
            Else
                '
                btnCall.ForeColor = Color.Black
                btnCall.BackColor = Color.White
                btnCall.Enabled = False
            End If
        End Sub

        Private Sub txtSekeika_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSekeika.LostFocus
            '設計課入力時は選択ボタンを使用可能へ
            If txtSekeika.Text <> "" Then
                '
                btnCall.ForeColor = Color.Black
                btnCall.BackColor = Color.LightCyan
                btnCall.Enabled = True
            Else
                '
                btnCall.ForeColor = Color.Black
                btnCall.BackColor = Color.White
                btnCall.Enabled = False
            End If
        End Sub

        Private Sub spdParts_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellDoubleClick
            Me.Hide()
            Me.CallShisakuBuhinEditBlock(e.Row)
            Me.Show()
            '---------------------------------------------------------------
            '２次改修で追加。
            '   排他処理でステータスが更新されていた場合、自画面も閉じる。
            '以下のパラメータの場合自分も閉じる。
            If frm37ParaModori = "close" Then
                Me.Close()
            End If
            '---------------------------------------------------------------
        End Sub

#Region "SpreadのColumnのTag"
        ''' <summary>部課コード</summary>
        Private ReadOnly TAG_BUKA_CODE As String = "BUKA_CODE"
        ''' <summary>設計課</summary>
        Private ReadOnly TAG_SEKKEIKA As String = "KA_RYAKU_NAME"
        ''' <summary>ブロック数</summary>
        Private ReadOnly TAG_TOTAL_BLOCK As String = "TOTAL_BLOCK"
        ''' <summary>処置完了数</summary>
        Private ReadOnly TAG_TOTAL_JYOUTAI As String = "TOTAL_JYOUTAI"
        ''' <summary>処置残り</summary>
        Private ReadOnly TAG_NOKORI_KAN As String = "NOKORI_KAN"
        ''' <summary>処置情況</summary>
        Private ReadOnly TAG_SHINDO_KAN As String = "SHINDO_KAN"
        ''' <summary>担当承認完了数</summary>
        Private ReadOnly TAG_TOTAL_SYOUNIN_JYOUTAI As String = "TOTAL_SYOUNIN_JYOUTAI"
        ''' <summary>担当承認残り</summary>
        Private ReadOnly TAG_NOKORI_SYOUNIN1 As String = "NOKORI_SYOUNIN1"
        ''' <summary>担当承認情況</summary>
        Private ReadOnly TAG_SHINDO_SYOUNIN1 As String = "SHINDO_SYOUNIN1"
        ''' <summary>課長承認完了数</summary>
        Private ReadOnly TAG_TOTAL_KACHOU_SYOUNIN_JYOUTAI As String = "TOTAL_KACHOU_SYOUNIN_JYOUTAI"
        ''' <summary>課長承認残り</summary>
        Private ReadOnly TAG_NOKORI_SYOUNIN2 As String = "NOKORI_SYOUNIN2"
        ''' <summary>課長承認情況</summary>
        Private ReadOnly TAG_SHINDO_SYOUNIN2 As String = "SHINDO_SYOUNIN2"

#End Region

        'スプレッド初期化
        Private Sub setspdStyle()

            Dim font As New Font("MS Gothic", 11)
            spdParts_Sheet1.ActiveColumn.Font = font

            m_spCom = New SpreadCommon(spdParts)
            '横幅変更不可
            spdParts_Sheet1.SetRowSizeable(spdParts_Sheet1.RowCount, False)
            m_spCom.GetColFromTag(TAG_BUKA_CODE).DataField = TAG_BUKA_CODE
            m_spCom.GetColFromTag(TAG_SEKKEIKA).DataField = TAG_SEKKEIKA
            m_spCom.GetColFromTag(TAG_TOTAL_BLOCK).DataField = TAG_TOTAL_BLOCK
            m_spCom.GetColFromTag(TAG_TOTAL_JYOUTAI).DataField = TAG_TOTAL_JYOUTAI
            ''m_spCom.GetColFromTag("NOKORI_KAN").DataField = "remain1"

            m_spCom.GetColFromTag(TAG_TOTAL_SYOUNIN_JYOUTAI).DataField = TAG_TOTAL_SYOUNIN_JYOUTAI
            ''m_spCom.GetColFromTag("NOKORI_SYOUNIN1").DataField = "remain2"

            m_spCom.GetColFromTag(TAG_TOTAL_KACHOU_SYOUNIN_JYOUTAI).DataField = TAG_TOTAL_KACHOU_SYOUNIN_JYOUTAI
            ''m_spCom.GetColFromTag("NOKORI_SYOUNIN2").DataField = "remain3"

            m_spCom.GetColFromTag(TAG_BUKA_CODE).Font = font
            'm_spCom.GetColFromTag(TAG_SEKKEIKA).Font = font
            m_spCom.GetColFromTag(TAG_TOTAL_BLOCK).Font = font
            m_spCom.GetColFromTag(TAG_TOTAL_JYOUTAI).Font = font
            m_spCom.GetColFromTag(TAG_NOKORI_KAN).Font = font
            m_spCom.GetColFromTag(TAG_SHINDO_KAN).Font = font

            m_spCom.GetColFromTag(TAG_TOTAL_SYOUNIN_JYOUTAI).Font = font
            m_spCom.GetColFromTag(TAG_NOKORI_SYOUNIN1).Font = font
            m_spCom.GetColFromTag(TAG_SHINDO_SYOUNIN1).Font = font
            m_spCom.GetColFromTag(TAG_TOTAL_KACHOU_SYOUNIN_JYOUTAI).Font = font
            m_spCom.GetColFromTag(TAG_NOKORI_SYOUNIN2).Font = font
            m_spCom.GetColFromTag(TAG_SHINDO_SYOUNIN2).Font = font

            spdParts_Sheet1.SetColumnWidth(0, 40)
            spdParts_Sheet1.SetColumnWidth(1, 95)
            spdParts_Sheet1.SetColumnWidth(2, 50)
            spdParts_Sheet1.SetColumnWidth(3, 50)
            spdParts_Sheet1.SetColumnWidth(4, 40)
            spdParts_Sheet1.SetColumnWidth(5, 60)
            spdParts_Sheet1.SetColumnWidth(6, 50)
            spdParts_Sheet1.SetColumnWidth(7, 40)
            spdParts_Sheet1.SetColumnWidth(8, 60)
            spdParts_Sheet1.SetColumnWidth(9, 50)
            spdParts_Sheet1.SetColumnWidth(10, 40)
            spdParts_Sheet1.SetColumnWidth(11, 60)

            m_spCom.GetColFromTag(TAG_BUKA_CODE).Visible = False
            Dim i As Integer = 0
            For i = 0 To spdParts_Sheet1.Rows.Count - 1
                Dim totalBlock As Decimal = m_spCom.GetValue(TAG_TOTAL_BLOCK, i)
                Dim totalJyouTai As Decimal = m_spCom.GetValue(TAG_TOTAL_JYOUTAI, i)
                Dim totalSyouNinJyouTai As Decimal = m_spCom.GetValue(TAG_TOTAL_SYOUNIN_JYOUTAI, i)
                Dim totalKaChouSyouNinJyouTai As Decimal = m_spCom.GetValue(TAG_TOTAL_KACHOU_SYOUNIN_JYOUTAI, i)
                Dim jyouTaiText = 0
                Dim syouNinJyouTaiText = 0
                Dim kaChouSyouNinJyouTaiText = 0
                If totalBlock > 0 Then
                    jyouTaiText = Decimal.Round(totalJyouTai / totalBlock * 100, 0)
                    syouNinJyouTaiText = Decimal.Round(totalSyouNinJyouTai / totalBlock * 100, 0)
                    kaChouSyouNinJyouTaiText = Decimal.Round(totalKaChouSyouNinJyouTai / totalBlock * 100, 0)
                End If
                m_spCom.GetCell(TAG_SHINDO_KAN, i).Text = StrConv(jyouTaiText, VbStrConv.None) & "%"
                m_spCom.GetCell(TAG_SHINDO_SYOUNIN1, i).Text = StrConv(syouNinJyouTaiText, VbStrConv.None) & "%"
                m_spCom.GetCell(TAG_SHINDO_SYOUNIN2, i).Text = StrConv(kaChouSyouNinJyouTaiText, VbStrConv.None) & "%"

                m_spCom.GetCell(TAG_NOKORI_KAN, i).Text = StrConv(totalBlock - totalJyouTai, VbStrConv.None)
                m_spCom.GetCell(TAG_NOKORI_SYOUNIN1, i).Text = StrConv(totalBlock - totalSyouNinJyouTai, VbStrConv.None)
                m_spCom.GetCell(TAG_NOKORI_SYOUNIN2, i).Text = StrConv(totalBlock - totalKaChouSyouNinJyouTai, VbStrConv.None)

            Next i

        End Sub

        '画面オープン時進度状況を設定する
        Private Sub initShindo()
            Dim kabetuJyoutaiDao As New ShisakuBuhinEditBlockDaoImpl
            Dim kabetuJyoutai = kabetuJyoutaiDao.GetTotalJyoutai(strEventCode)

            If Not kabetuJyoutai Is Nothing Then
                Dim totalBlock As Decimal = kabetuJyoutai.TotalBlock
                Dim totalJyouTai As Decimal = kabetuJyoutai.TotalJyoutai
                Dim totalSyouNinJyouTai As Decimal = kabetuJyoutai.TotalSyouninJyoutai
                Dim totalKaChouSyouNinJyouTai As Decimal = kabetuJyoutai.TotalKachouSyouninJyoutai

                lblBurokusu.Text = StrConv(totalBlock, VbStrConv.Wide)
                lblSyochi_kan.Text = StrConv(totalJyouTai, VbStrConv.Wide)
                lblNokori_kan.Text = StrConv(totalBlock - totalJyouTai, VbStrConv.Wide)
                lblSyochi_syounin1.Text = StrConv(totalSyouNinJyouTai, VbStrConv.Wide)
                lblNokori_syounin1.Text = StrConv(totalBlock - totalSyouNinJyouTai, VbStrConv.Wide)
                lblSyochi_syounin2.Text = StrConv(totalKaChouSyouNinJyouTai, VbStrConv.Wide)
                lblNokori_syounin2.Text = StrConv(totalBlock - totalKaChouSyouNinJyouTai, VbStrConv.Wide)

                If Not (totalJyouTai = 0) Then
                    lblkanryou.Text = Decimal.Round(totalJyouTai / totalBlock * 100, 0).ToString() + "%"
                    lblShindo_Kan.Text = StrConv(Decimal.Round(totalJyouTai / totalBlock * 100, 0).ToString(), VbStrConv.Wide) + "%"
                    lblShindo_syounin1.Text = StrConv(Decimal.Round(totalSyouNinJyouTai / totalBlock * 100, 0).ToString(), VbStrConv.Wide) + "%"
                    lblShindo_syounin2.Text = StrConv(Decimal.Round(totalKaChouSyouNinJyouTai / totalBlock * 100, 0).ToString(), VbStrConv.Wide) + "%"

                    lblShindo_Kan.Font = font
                    lblShindo_syounin1.Font = font
                    lblShindo_syounin2.Font = font
                Else
                    lblkanryou.Text = "0%"
                    lblShindo_Kan.Text = "０%"
                    lblShindo_syounin1.Text = "０%"
                    lblShindo_syounin2.Text = "０%"
                End If

            End If
        End Sub

        '画面オープン時イベント情報を設定する
        Private Sub initIbento()
            'If Not ComFunc.InitIni(g_kanrihyoIni, mdlConstraint.INI_KANRIHYO_FILE) = RESULT.OK Then
            '    Return
            'End If
            Dim strKigen As String = Nothing
            Dim strDate As String = Nothing
            Dim dtList As DataTable = New DataTable()
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                If Not strEventCode = String.Empty Then
                    db.AddParameter("@SHISAKU_EVENT_CODE", strEventCode, DbType.AnsiString)
                End If
                db.Fill(DataSqlCommon.GetIbento(strMode), dtList)
            End Using
            If dtList.Rows.Count > 0 Then
                If (Not dtList.Rows(0)("SHISAKU_KAIHATSU_FUGO").Equals(DBNull.Value)) Then
                    lblKaihatuFugou.Text = dtList.Rows(0)("SHISAKU_KAIHATSU_FUGO").ToString()
                End If
                If (Not dtList.Rows(0)("SHISAKU_EVENT_NAME").Equals(DBNull.Value)) Then
                    lblIbentoName.Text = dtList.Rows(0)("SHISAKU_EVENT_NAME").ToString()
                End If
                If (strMode = ShishakuHensyuMode) AndAlso (Not dtList.Rows(0)("KAITEI_SYOCHI_SHIMEKIRIBI").Equals(DBNull.Value)) Then
                    strKigen = dtList.Rows(0)("KAITEI_SYOCHI_SHIMEKIRIBI")
                End If
                If (strMode = ShishakuKaiteiHensyuMode) AndAlso (Not dtList.Rows(0)("SHIMEKIRIBI").Equals(DBNull.Value)) Then
                    strKigen = dtList.Rows(0)("SHIMEKIRIBI")
                End If


                If Not strKigen = String.Empty Then
                    lblSyochikigen.Text = strKigen.Substring(0, 4) & "/" & strKigen.Substring(4, 2) & "/" & strKigen.Substring(6, 2)
                End If

                Dim wToday As Integer = Integer.Parse(DateTime.Now.ToString("yyyyMMdd"))

                If Not strKigen = String.Empty Then   '*****完了閲覧モード対応*****
                    Dim wSyochikigen As Integer = Integer.Parse(strKigen)
                    Dim kabetuJyoutaiDao As New ShisakuBuhinEditBlockDaoImpl
                    Dim wAto As Integer = kabetuJyoutaiDao.GetKadoubi(wToday, wSyochikigen).Kadobi

                    '残り日数を見てメッセージを変える。
                    If wAto > 0 Then
                        '期限が切れていない時
                        lblSyochikigen2.Text = "あと　" & wAto & "日"
                        lblSyochikigen2.Font = New Font("MS Gothic", 11, FontStyle.Regular)
                    Else
                        '期限が切れた時は以下のメッセージを出力する。
                        lblSyochikigen2.Text = "もう過ぎてます。"
                        lblSyochikigen2.Font = New Font("MS Gothic", 11, FontStyle.Bold)
                    End If
                    'If (Not dtList.Rows(0)("DIFF_DATE").Equals(DBNull.Value)) And dtList.Rows(0)("DIFF_DATE").ToString() >= 0 Then
                    '    '期限が切れていない時
                    '    lblSyochikigen2.Text = "あと　" & dtList.Rows(0)("DIFF_DATE").ToString() & "日"
                    '    lblSyochikigen2.Font = New Font("MS Gothic", 11, FontStyle.Regular)
                    'Else
                    '    '期限が切れた時は以下のメッセージを出力する。
                    '    lblSyochikigen2.Text = "もう過ぎてます"
                    '    lblSyochikigen2.Font = New Font("MS Gothic", 11, FontStyle.Bold)
                    'End If

                    If Not StringUtil.IsEmpty(dtList.Rows(0)("SKEIDATE")) Then
                        strDate = dtList.Rows(0)("SKEIDATE")
                        strTeihaiDate = strDate.Substring(0, 4) & "/" & strDate.Substring(4, 2) & "/" & strDate.Substring(6, 2)
                    End If

                End If

            End If
        End Sub

        'スプレッドデータを設定する
        Private Sub initSpreadData()
            'If Not ComFunc.InitIni(g_kanrihyoIni, mdlConstraint.INI_KANRIHYO_FILE) = RESULT.OK Then
            '    Return
            'End If
            Dim dtList As DataTable = New DataTable()
            m_spCom = New SpreadCommon(spdParts)
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                If Not strEventCode = String.Empty Then
                    db.AddParameter("@ShisakuEventCode", strEventCode, DbType.AnsiString)
                End If
                db.AddParameter("@BlockFuyou", TShisakuSekkeiBlockVoHelper.BlockFuyou.NECESSARY, DbType.AnsiString)
                db.AddParameter("@Jyoutai", TShisakuSekkeiBlockVoHelper.Jyoutai.FINISHED, DbType.AnsiString)
                db.AddParameter("@TantoSyouninJyoutai", TShisakuSekkeiBlockVoHelper.TantoJyoutai.APPROVAL, DbType.AnsiString)
                db.AddParameter("@KachouSyouninJyoutai", TShisakuSekkeiBlockVoHelper.KachouJyoutai.APPROVAL, DbType.AnsiString)
                Dim kabetuJyoutaiDao As New ShisakuBuhinEditBlockDaoImpl
                db.Fill(kabetuJyoutaiDao.GetKabetuJyoutai(strEventCode), dtList)
                ' シートにデータセットを接続します。
                spdParts.ActiveSheet.DataSource = dtList
            End Using
        End Sub

        Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub

    End Class
End Namespace
