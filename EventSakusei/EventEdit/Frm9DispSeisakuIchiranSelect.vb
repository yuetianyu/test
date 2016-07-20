Imports FarPoint.Win
Imports FarPoint.Win.Spread.CellType
Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Ui.Spd
Imports EBom.Data
Imports EBom.Common
Imports System.Text
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Ui.Valid
Imports EventSakusei.EventEdit.Ui
Imports EventSakusei.EventEdit.Dao

Namespace EventEdit
    ''' <summary>
    ''' 製作一覧発行
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm9DispSeisakuIchiranSelect
#Region "local member"
        ''' <summary>FpSpread共通メソッド</summary>
        ''' 
        Private m_spCom As SpreadCommon

        ''' <summary>システム日付情報</summary>
        ''' 
        Private aDate As New ShisakuDate

        Private selectHakouNo As String = ""
        Private selectKaiteiNo As String = ""
        Private selectEventName As String = ""
        Private selectKaihatsuFugo As String = ""
        Private selectStatus As String = ""

        Private strHakouNo As String = ""
        Private strKaiteiNo As String = ""


        '製作一覧発行情報
        Private hdList As New List(Of TSeisakuHakouHdVo)

#End Region

#Region "Construct"
        ''' <summary>
        ''' Construct
        ''' </summary>
        Public Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ShisakuFormUtil.Initialize(Me)
        End Sub
#End Region

#Region "起動"
        ''' <summary>
        ''' 起動
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Execute()
            spdParts.FocusRenderer = New FarPoint.Win.Spread.MarqueeFocusIndicatorRenderer(Color.Blue, 1)
            m_spCom = New SpreadCommon(spdParts)
            'ヘッダーを設定する
            InitializeHeader()
            'スプレッドデータを初期化する
            initSpreadDataClr()
        End Sub
#End Region

#Region "初期化"
        ''' <summary>
        ''' 画面ヘーダ部分の初期化
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeHeader()

            ShisakuFormUtil.setTitleVersion(Me)
            LblCurrPGId.Text = "PG-ID :" + PROGRAM_ID_09
            ShisakuFormUtil.SetDateTimeNow(Me.LblDateNow, Me.LblTimeNow)
            ShisakuFormUtil.SetIdAndBuka(Me.LblCurrUserId, Me.LblCurrBukaName)

            SpreadUtil.Initialize(spdParts)

            LblMessage.Text = "発行№を選択して「取込」ボタンをクリックして下さい。"

            '発行№コンボボックスに値を追加
            cmbHakouNoSet()
            '開発符号コンボボックスに値を追加
            cmbKaihatsuFugoSet()
            'イベントコンボボックスに値を追加
            cmbEventSet()
            'イベント名コンボボックスに値を追加
            cmbEventNameSet()

            'クリア
            cmbHakouNo.Text = Nothing
            cmbKaihatsuFugo.Text = Nothing
            cmbEvent.Text = Nothing
            cmbEventName.Text = Nothing

        End Sub


#Region " 発行№のコンボボックスを作成 "
        ''' <summary>
        ''' 製作一覧情報よりイベントコードを設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetHakouNoData(ByVal flgStatus As Boolean) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                If StringUtil.Equals(flgStatus, True) Then
                    db.Fill(DataSqlCommon.GetHakouNoSql(), dtData)
                Else
                    db.Fill(DataSqlCommon.GetHakouNoSqlAll(), dtData)
                End If
            End Using
            Return dtData
        End Function
#End Region

#Region " 開発符号のコンボボックスを作成 "
        ''' <summary>
        ''' 製作一覧情報よりイベントコードを設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetKaihatsuFugoData(ByVal flgStatus As Boolean) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                If StringUtil.Equals(flgStatus, True) Then
                    db.Fill(DataSqlCommon.GetKaihatsuFugoSql(), dtData)
                Else
                    db.Fill(DataSqlCommon.GetKaihatsuFugoSqlAll(), dtData)
                End If
            End Using
            Return dtData
        End Function
#End Region

#Region " イベントのコンボボックスを作成 "
        ''' <summary>
        ''' 製作一覧情報よりイベントコードを設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetEventData(ByVal flgStatus As Boolean) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                If StringUtil.Equals(flgStatus, True) Then
                    db.Fill(DataSqlCommon.GetEventSql(), dtData)
                Else
                    db.Fill(DataSqlCommon.GetEventSqlAll(), dtData)
                End If
            End Using
            Return dtData
        End Function
#End Region

#Region " イベント名称のコンボボックスを作成 "
        ''' <summary>
        ''' 製作一覧情報よりイベントコードを設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetEventNameData(ByVal flgStatus As Boolean) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                Dim sql As New StringBuilder
                If StringUtil.Equals(flgStatus, True) Then
                    db.Fill(DataSqlCommon.GetEventNameSql(), dtData)
                Else
                    db.Fill(DataSqlCommon.GetEventNameSqlAll(), dtData)
                End If
            End Using
            Return dtData
        End Function
#End Region

        ''' <summary>
        ''' スプレッドデータを設定する
        ''' </summary>
        Private Sub initSpreadDataClr()
            SeisakuIchirantorikomiSpreadUtil.InitializeFrm9(spdParts)
            Dim sheet = spdParts_Sheet1
            Dim index As Integer = 0
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_HAKOU_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAIHATSU_FUGO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EVENT
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EVENT_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TOUROKU_BI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DAISU_KANSEI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DAISU_PURASU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DAISU_WB
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_STATUS
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_JYOTAI

            'スプレッドデータを設定する。
            initSpreadData()

        End Sub
#End Region

        ''' <summary>
        ''' スプレッドデータを設定する
        ''' </summary>
        Private Sub initSpreadData()

            '初期設定
            Dim strStatus As String = Nothing
            '
            Dim strHakouNo As String = ""
            Dim strKaihatsuFugo As String = ""
            Dim strEvent As String = ""
            Dim strEventName As String = ""

            '値を設定
            If StringUtil.IsNotEmpty(cmbHakouNo.Text) Then
                strHakouNo = cmbHakouNo.Text
            End If
            If StringUtil.IsNotEmpty(cmbKaihatsuFugo.Text) Then
                strKaihatsuFugo = cmbKaihatsuFugo.Text
            End If
            If StringUtil.IsNotEmpty(cmbEvent.Text) Then
                strEvent = cmbEvent.Text
            End If
            If StringUtil.IsNotEmpty(cmbEventName.Text) Then
                strEventName = cmbEventName.Text
            End If

            '最終更新日を数値に変換
            '   日付チェックするorしないのラジオボタンにより以下の様に変換する。
            If rbtStatus01.Checked = True Then
                strStatus = STATUS_B
            Else
                strStatus = Nothing
            End If

            Dim seisakuDao As SeisakuIchiranDao = New SeisakuIchiranDaoImpl()
            Dim seisakuList = seisakuDao.GetSeisakuIchiranHdSpreadList(strStatus, _
                                                                       strHakouNo, _
                                                                       strKaihatsuFugo, _
                                                                       strEvent, _
                                                                       strEventName, "")


            SeisakuIchiranTorikomiSpreadUtil.setRowCount(spdParts, seisakuList.Count)
            updateSpreadData(seisakuList)

            'ボタン／コンボボックス初期化
            btnSeisakuIchiranTorikomiDisable()

        End Sub


#Region "コンポーネント状態"
        ''' <summary>「取込」ボタンが利用できる</summary>
        Private Sub btnSeisakuIchiranTorikomiEnable()
            btnSeisakuIchiranTorikomi.BackColor = Color.LightGray
            btnSeisakuIchiranTorikomi.Enabled = True
        End Sub

        ''' <summary>「取込」ボタンが利用でできない</summary>
        Private Sub btnSeisakuIchiranTorikomiDisable()
            btnSeisakuIchiranTorikomi.BackColor = Color.White
            btnSeisakuIchiranTorikomi.Enabled = False
        End Sub

        ''' <summary>「発行№」コンボボックスの値を変更</summary>
        Private Sub cmbHakouNoSet()
            '発行№コンボボックスに値を追加
            Dim dtHakouNo As DataTable = GetHakouNoData(rbtStatus01.Checked)
            FormUtil.ComboBoxBind(cmbHakouNo, dtHakouNo, "HAKOU_NO", "HAKOU_NO")
        End Sub
        ''' <summary>「開発符号」コンボボックスの値を変更</summary>
        Private Sub cmbKaihatsuFugoSet()
            '開発符号コンボボックスに値を追加
            Dim dtKaihatsuFugo As DataTable = GetKaihatsuFugoData(rbtStatus01.Checked)
            FormUtil.ComboBoxBind(cmbKaihatsuFugo, dtKaihatsuFugo, "KAIHATSU_FUGO", "KAIHATSU_FUGO")
        End Sub
        ''' <summary>「イベント」コンボボックスの値を変更</summary>
        Private Sub cmbEventSet()
            'イベントコンボボックスに値を追加
            Dim dtEvent As DataTable = GetEventData(rbtStatus01.Checked)
            FormUtil.ComboBoxBind(cmbEvent, dtEvent, "SEISAKU_EVENT", "SEISAKU_EVENT")
        End Sub
        ''' <summary>「イベント名称」コンボボックスの値を変更</summary>
        Private Sub cmbEventNameSet()
            'イベント名称コンボボックスに値を追加
            Dim dtEventName As DataTable = GetEventNameData(rbtStatus01.Checked)
            FormUtil.ComboBoxBind(cmbEventName, dtEventName, "SEISAKU_EVENT_NAME", "SEISAKU_EVENT_NAME")
        End Sub
#End Region

#Region "ボタンアクション"
        ''' <summary>
        ''' 「アプリケーション終了」ボタン
        ''' </summary>
        ''' <param name="sender">Form</param>
        ''' <param name="e">ボタンクリックのイベント</param>
        ''' <remarks></remarks>
        Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
            Application.Exit()
            System.Environment.Exit(0)
        End Sub

        ''' <summary>
        ''' 「戻る」ボタン
        ''' </summary>
        ''' <param name="sender">Form</param>
        ''' <param name="e">ボタンクリックのイベント</param>
        ''' <remarks></remarks>
        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            Me.Close()
        End Sub

        ''' <summary>
        ''' 「取込」ボタン
        ''' </summary>
        ''' <param name="sender">Form</param>
        ''' <param name="e">ボタンクリックのイベント</param>
        ''' <remarks></remarks>
        Private Sub btnSeisakuIchiranTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeisakuIchiranTorikomi.Click

            '初期設定
            Dim strOk As String = ""
            Dim lngOkCancel As Long = 0

            '未承認の場合アラートを表示
            If StringUtil.Equals(selectStatus, STATUS_A_NAME) Then
                '確認メッセージ
                lngOkCancel = ComFunc.ShowInfoMsgBox("「未承認」の製作一覧です。取込を実行して宜しいですか？", _
                                                     MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1)
                If lngOkCancel = 6 Then 'Yesが押されたら6、Noが押されたら7。
                    strOk = "OK"
                End If
            Else
                strOk = "OK"
            End If

            If StringUtil.Equals(strOk, "OK") Then
                '発行№、改訂№を返す。
                strHakouNo = selectHakouNo
                strKaiteiNo = selectKaiteiNo
                '閉じる。
                Me.Close()
            Else
                ComFunc.ShowInfoMsgBox("取込を中断しました。", MessageBoxButtons.OK)
                btnSeisakuIchiranTorikomiDisable()
            End If

        End Sub

#End Region

#Region "SpreadのColumnのTag"
        ''' <summary>発行№</summary>
        Private ReadOnly TAG_HAKOU_NO As String = "HAKOU_NO_Column"
        ''' <summary>開発符号</summary>
        Private ReadOnly TAG_KAIHATSU_FUGO As String = "KAIHATSU_FUGO_Column"
        ''' <summary>イベント</summary>
        Private ReadOnly TAG_EVENT As String = "EVENT_Column"
        ''' <summary>イベント名</summary>
        Private ReadOnly TAG_EVENT_NAME As String = "EVENT_NAME_Column"
        ''' <summary>改訂№</summary>
        Private ReadOnly TAG_KAITEI_NO As String = "KAITEI_NO_Column"
        ''' <summary>登録日</summary>
        Private ReadOnly TAG_TOUROKU_BI As String = "TOUROKU_BI_Column"
        ''' <summary>台数・完成車</summary>
        Private ReadOnly TAG_DAISU_KANSEI As String = "DAISU_KANSEI_Column"
        ''' <summary>台数・プラス</summary>
        Private ReadOnly TAG_DAISU_PURASU As String = "DAISU_PURASU_Column"
        ''' <summary>台数・ホワイトボディ</summary>
        Private ReadOnly TAG_DAISU_WB As String = "DAISU_WB_Column"
        ''' <summary>ステータス</summary>
        Private ReadOnly TAG_STATUS As String = "STATUS_Column"
        ''' <summary>状態</summary>
        Private ReadOnly TAG_JYOTAI As String = "JYOTAI_Column"

#End Region

#Region "spread　一覧設定"
        ''' <summary>
        ''' spread　一覧を表示する
        ''' </summary>
        ''' <param name="seisakuList">製作一覧</param>
        ''' <remarks></remarks>
        Private Sub updateSpreadData(ByVal seisakuList As List(Of TSeisakuHakouHdVo))
            Dim i As Integer
            For i = 0 To seisakuList.Count - 1
                Dim seisakuIchiran = seisakuList.Item(i)
                UpdateSpreadRow(seisakuIchiran, i)
            Next
        End Sub
        ''' <summary>
        ''' spread　行を表示する
        ''' </summary>
        ''' <param name="seisakuIchiran">製作一覧情報</param>
        ''' <param name="rowNo">spread行の番号</param>
        ''' <remarks></remarks>
        Private Sub UpdateSpreadRow(ByVal seisakuIchiran As TSeisakuHakouHdVo, ByVal rowNo As Integer)
            Dim seisakuIchiranHdHelp = New TSeisakuIchiranHdVoHelper(seisakuIchiran)
            m_spCom.GetCell(TAG_HAKOU_NO, rowNo).Text = seisakuIchiran.HakouNo
            m_spCom.GetCell(TAG_KAIHATSU_FUGO, rowNo).Text = seisakuIchiran.KaihatsuFugo
            m_spCom.GetCell(TAG_EVENT, rowNo).Text = seisakuIchiran.SeisakuEvent
            m_spCom.GetCell(TAG_EVENT_NAME, rowNo).Text = seisakuIchiran.SeisakuEventName
            m_spCom.GetCell(TAG_KAITEI_NO, rowNo).Text = seisakuIchiran.KaiteiNo
            m_spCom.GetCell(TAG_TOUROKU_BI, rowNo).Text = seisakuIchiranHdHelp.TourokuBi
            m_spCom.GetCell(TAG_DAISU_KANSEI, rowNo).Text = seisakuIchiran.SeisakudaisuKanseisya
            m_spCom.GetCell(TAG_DAISU_PURASU, rowNo).Text = TSeisakuIchiranHdVoHelper.Daisu.PURASU
            m_spCom.GetCell(TAG_DAISU_WB, rowNo).Text = seisakuIchiran.SeisakudaisuWb

            'ステータス
            If seisakuIchiran.Status = STATUS_B Then
                m_spCom.GetCell(TAG_STATUS, rowNo).Text = TSeisakuIchiranHdVoHelper.StatusMoji.SYOUNIN_MOJI
            Else
                m_spCom.GetCell(TAG_STATUS, rowNo).Text = TSeisakuIchiranHdVoHelper.StatusMoji.MISYOUNIN_MOJI
            End If

            '状態
            If seisakuIchiran.ChushiFlg = JYOTAI_A Then
                m_spCom.GetCell(TAG_JYOTAI, rowNo).Text = TSeisakuIchiranHdVoHelper.JyoutaiMoji.CHUSHI_MOJI
            Else
                m_spCom.GetCell(TAG_JYOTAI, rowNo).Text = TSeisakuIchiranHdVoHelper.JyoutaiMoji.OK_MOJI
            End If

            '全ての項目を使用不可（ロック）する）
            m_spCom.GetCell(TAG_HAKOU_NO, rowNo).Locked = True
            m_spCom.GetCell(TAG_KAIHATSU_FUGO, rowNo).Locked = True
            m_spCom.GetCell(TAG_EVENT, rowNo).Locked = True
            m_spCom.GetCell(TAG_EVENT_NAME, rowNo).Locked = True
            m_spCom.GetCell(TAG_KAITEI_NO, rowNo).Locked = True
            m_spCom.GetCell(TAG_TOUROKU_BI, rowNo).Locked = True
            m_spCom.GetCell(TAG_DAISU_KANSEI, rowNo).Locked = True
            m_spCom.GetCell(TAG_DAISU_PURASU, rowNo).Locked = True
            m_spCom.GetCell(TAG_DAISU_WB, rowNo).Locked = True
            m_spCom.GetCell(TAG_STATUS, rowNo).Locked = True
            m_spCom.GetCell(TAG_JYOTAI, rowNo).Locked = True

        End Sub

#End Region


        ''' <summary>
        ''' 発行№
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property HakouNo() As String
            Get
                Return strHakouNo
            End Get
        End Property

        ''' <summary>
        ''' 改訂№
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property KaiteiNo() As String
            Get
                Return strKaiteiNo
            End Get
        End Property

        ''' <summary>
        ''' 発行№からrowNoを取得する
        ''' </summary>
        ''' <param name="txt">ブロックNo</param>
        ''' <returns>rowNo</returns>
        ''' <remarks></remarks>
        Private Function rowNoFind(ByVal txt As String) As Integer
            '引数に入る値txtBlockNo.Text()
            Dim result As Integer = -1
            For i As Integer = 0 To spdParts_Sheet1.RowCount() - 1
                For j As Integer = 0 To spdParts_Sheet1.ColumnCount() - 1
                    If txt.Equals(spdParts_Sheet1.Cells(i, j).Value) Then
                        result = i
                    End If
                Next
            Next
            Return result
        End Function

        Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            ShisakuFormUtil.SetDateTimeNow(LblDateNow, LblTimeNow)
        End Sub

        Private Sub rbtStatus01_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtStatus01.Click
            cmbHakouNoSet()
            cmbHakouNo.Text = Nothing
            cmbKaihatsuFugoSet()
            cmbKaihatsuFugo.Text = Nothing
            cmbEventSet()
            cmbEvent.Text = Nothing
            cmbEventNameSet()
            cmbEventName.Text = Nothing
            'スプレッドデータを初期化する
            initSpreadDataClr()
        End Sub
        Private Sub rbtStatus02_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtStatus02.Click
            cmbHakouNoSet()
            cmbHakouNo.Text = Nothing
            cmbKaihatsuFugoSet()
            cmbKaihatsuFugo.Text = Nothing
            cmbEventSet()
            cmbEvent.Text = Nothing
            cmbEventNameSet()
            cmbEventName.Text = Nothing
            'スプレッドデータを初期化する
            initSpreadDataClr()
        End Sub

        Private Sub spdParts_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellClick
            'クリア
            selectHakouNo = ""
            selectKaiteiNo = ""
            selectKaihatsuFugo = ""
            selectEventName = ""
            selectStatus = ""
            'カラムヘッダーかどうか。
            If e.ColumnHeader = False Then
                '' 選択範囲の情報を表示します。
                'cmbHakouNo.Text = m_spCom.GetValue(TAG_HAKOU_NO, e.Row)
                '   発行№と改訂№と開発符号とイベント名とステータスを保持
                selectHakouNo = m_spCom.GetValue(TAG_HAKOU_NO, e.Row)
                selectKaiteiNo = m_spCom.GetValue(TAG_KAITEI_NO, e.Row)
                selectKaihatsuFugo = m_spCom.GetValue(TAG_KAIHATSU_FUGO, e.Row)
                selectEventName = m_spCom.GetValue(TAG_EVENT_NAME, e.Row)
                selectStatus = m_spCom.GetValue(TAG_STATUS, e.Row)
                'ボタンを使用可能にする。
                If StringUtil.IsNotEmpty(selectHakouNo) Then
                    btnSeisakuIchiranTorikomiEnable()
                Else
                    btnSeisakuIchiranTorikomiDisable()
                End If
            End If

        End Sub

        ''' <summary>
        ''' バックカラーを戻す
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ResetColor()
            For i As Integer = 0 To spdParts_Sheet1.RowCount - 1
                ShisakuFormUtil.initlColor(spdParts_Sheet1.Rows(i))
            Next
        End Sub

        ''' <summary>
        ''' 発行のエラーチェック チェックエラーがあると　バックカラーは赤
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CallCheck() As RESULT
            Dim rowIndex As Integer = 0
            For i As Integer = 0 To spdParts_Sheet1.RowCount - 1
                If m_spCom.GetValue(TAG_HAKOU_NO, i).Equals(cmbHakouNo.Text) Then
                    rowIndex = i
                    Exit For
                End If
            Next
            Dim status As String = m_spCom.GetValue(TAG_STATUS, rowIndex)

            If status Is Nothing Then
                status = ""
            End If
            '発行№が存在しないならエラー
            If status.Equals("") Then
                ShisakuFormUtil.onErro(spdParts_Sheet1.Rows(rowIndex))
                ComFunc.ShowErrMsgBox("発行№が存在しません！！")
                Return RESULT.NG
            End If
        End Function

        ''' <summary>
        ''' 発行№を選択。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub cmbHakouNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbHakouNo.TextChanged
            'スプレッドデータを初期化する
            initSpreadDataClr()
        End Sub

        ''' <summary>
        ''' イベントを選択。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub cmbEvent_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEvent.TextChanged
            'スプレッドデータを初期化する
            initSpreadDataClr()
        End Sub

        ''' <summary>
        ''' イベント名称を選択。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub cmbEventName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEventName.TextChanged
            'スプレッドデータを初期化する
            initSpreadDataClr()
        End Sub

        ''' <summary>
        ''' 開発符号を選択。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub cmbKaihatsuFugo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKaihatsuFugo.TextChanged
            'スプレッドデータを初期化する
            initSpreadDataClr()
        End Sub

    End Class
End Namespace
