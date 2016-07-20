Imports FarPoint.Win
Imports FarPoint.Win.Spread.CellType
Imports System.Reflection
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
Imports EBom.Data
Imports EBom.Common
Imports EventSakusei.EventEdit.Ui
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Vo.Helper

Namespace EventEdit
    ''' <summary>
    ''' 製作一覧発行
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Frm9DispSeisakuIchiranSelectKaiteiNo
#Region "local member"
        ''' <summary>FpSpread共通メソッド</summary>
        ''' 
        Private m_spCom As SpreadCommon

        Private selectHakouNo As String = ""
        Private selectKaiteiNo As String = ""
        Private selectStatus As String = ""

        Private strHakouNo As String = ""
        Private strKaiteiNo As String = ""

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
        Public Sub Execute(ByVal strHakouNo As String, ByVal strKaiteiNo As String)

            Me.strHakouNo = strHakouNo
            Me.strKaiteiNo = strKaiteiNo

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

            SpreadUtil.Initialize(spdParts)

            LblMessage.Text = "製作一覧を選択して「取込」ボタンをクリックして下さい。"

        End Sub

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

            Dim seisakuDao As SeisakuIchiranDao = New SeisakuIchiranDaoImpl()
            Dim seisakuList = seisakuDao.GetSeisakuIchiranHdSpreadList(Nothing, strHakouNo, "", "", "", strKaiteiNo)

            SeisakuIchiranTorikomiSpreadUtil.setRowCount(spdParts, seisakuList.Count)
            updateSpreadData(seisakuList)

            'ボタン非表示
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

#End Region

#Region "ボタンアクション"

        ''' <summary>
        ''' 「戻る」ボタン
        ''' </summary>
        ''' <param name="sender">Form</param>
        ''' <param name="e">ボタンクリックのイベント</param>
        ''' <remarks></remarks>
        Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
            'クリア
            strHakouNo = ""
            strKaiteiNo = ""
            '閉じる
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
            Dim lngOkCancel As Long = 0
            Dim strOk As String = ""

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
        ''' バックカラーを戻す
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ResetColor()
            For i As Integer = 0 To spdParts_Sheet1.RowCount - 1
                ShisakuFormUtil.initlColor(spdParts_Sheet1.Rows(i))
            Next
        End Sub

        Private Sub spdParts_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles spdParts.CellClick
            'クリア
            selectHakouNo = ""
            selectKaiteiNo = ""
            selectStatus = ""

            'カラムヘッダーかどうか。
            If e.ColumnHeader = False Then
                '' 選択範囲の情報を表示します。
                'cmbHakouNo.Text = m_spCom.GetValue(TAG_HAKOU_NO, e.Row)
                '   発行№と改訂№と開発符号とイベント名とステータスを保持
                selectHakouNo = m_spCom.GetValue(TAG_HAKOU_NO, e.Row)
                selectKaiteiNo = m_spCom.GetValue(TAG_KAITEI_NO, e.Row)
                selectStatus = m_spCom.GetValue(TAG_STATUS, e.Row)
                'ボタンを使用可能にする。
                If StringUtil.IsNotEmpty(selectHakouNo) Then
                    btnSeisakuIchiranTorikomiEnable()
                Else
                    btnSeisakuIchiranTorikomiDisable()
                End If
            End If
        End Sub

    End Class
End Namespace
