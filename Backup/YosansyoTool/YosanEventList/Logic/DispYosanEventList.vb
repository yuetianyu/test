Imports EBom.Common
Imports EBom.Data
Imports FarPoint.Win
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon.Util.LabelValue
Imports YosansyoTool.YosanEventList.Dao
Imports YosansyoTool.YosanEventList.Dao.Impl

Namespace YosanEventList.Logic

    Public Class DispYosanEventList

#Region " メンバー変数 "
        ''' <summary>ビュー</summary>
        Private m_view As FrmYosanEventList

        ''' <summary>Excel出力タイトル</summary>
        Private lstExcelTitle As New List(Of String)
        ''' <summary>Excel出力項目</summary>
        Private lstExcelItem As New List(Of String)

        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly aSystemDate As ShisakuDate
        Private ExclusiveEventDao As TYosanExclusiveControlEventDao
        Private EventDao As TYosanEventDao

        '''<summary>Excelの取り込みデータ</summary>
        Private _ExcelImportList As List(Of TYosanZaimuJisekiVo)
        '''<summary>単価情報Excelの取り込みデータ</summary>
        Private _ExcelBuhinTankaList As List(Of TYosanTankaVo)
#End Region

#Region "定数"
        '''' <summary>ステータス</summary>
        Private Const STATUS_00 As String = "00"
        Private Const STATUS_01 As String = "01"

        '''' <summary>上期下期</summary>
        Private Const UP_KS As String = "上期"
        Private Const DOWN_KS As String = "下期"

        '''' <summary>比例費／固定費区分</summary>
        Public Const YOSAN_ZAIMU_HIREI As String = "H"
        Public Const YOSAN_ZAIMU_KOTEI As String = "K"

        '''' <summary>国内海外区分</summary>
        Public Const KOKUNAI_KBN As String = "D"
        Public Const KAIGAI_KBN As String = "F"
#End Region

#Region "EXCEL取込情報"
        ''' <summary>Excel取込でのヘッダー行位置</summary>
        Private Const EXCEL_IMPORT_HEAD01_ROW As Integer = 4
        Private Const EXCEL_IMPORT_HEAD02_ROW As Integer = 5
        Private Const EXCEL_IMPORT_HEAD03_ROW As Integer = 6
        Private Const EXCEL_IMPORT_HEAD04_ROW As Integer = 7

        ''' <summary>Excel取込でのヘッダー・原価工場位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_GENKA_COL As Integer = 2
        Private Const EXCEL_IMPORT_HEAD_GENKA_LBL01 As String = "原価"
        Private Const EXCEL_IMPORT_HEAD_GENKA_LBL02 As String = "工場"
        ''' <summary>Excel取込でのヘッダー・工事区分位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_KOUJI_COL As Integer = 3
        Private Const EXCEL_IMPORT_HEAD_KOUJI_LBL01 As String = "工事"
        Private Const EXCEL_IMPORT_HEAD_KOUJI_LBL02 As String = "区分"
        ''' <summary>Excel取込でのヘッダー・予算コード位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_YOSAN_COL As Integer = 4
        Private Const EXCEL_IMPORT_HEAD_YOSAN_LBL01 As String = "予算"
        Private Const EXCEL_IMPORT_HEAD_YOSAN_LBL02 As String = "コード"
        ''' <summary>Excel取込でのヘッダー・直接工数位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_CHOKUKOU_COL As Integer = 5
        Private Const EXCEL_IMPORT_HEAD_CHOKUKOU_LBL As String = "直接工数"
        ''' <summary>Excel取込でのヘッダー・技術工数位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_GIJYUTUKOU_COL As Integer = 6
        Private Const EXCEL_IMPORT_HEAD_GIJYUTUKOU_LBL As String = "技術工数"
        ''' <summary>Excel取込でのヘッダー・材料費位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_ZAIRYOHI_COL As Integer = 12
        Private Const EXCEL_IMPORT_HEAD_ZAIRYOHI_LBL As String = "材料費"
        ''' <summary>Excel取込でのヘッダー・直材費受入位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_CHOKUZAIUKE_COL As Integer = 10
        Private Const EXCEL_IMPORT_HEAD_CHOKUZAIUKE_LBL As String = "直材費受入"
        ''' <summary>Excel取込でのヘッダー・材料検収位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_ZAIRYOKEN_COL As Integer = 7
        Private Const EXCEL_IMPORT_HEAD_ZAIRYOKEN_LBL As String = "材料検収"
        ''' <summary>Excel取込でのヘッダー・材料支給位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_ZAIRYOSHI_COL As Integer = 8
        Private Const EXCEL_IMPORT_HEAD_ZAIRYOSHI_LBL As String = "材料支給"
        ''' <summary>Excel取込でのヘッダー・主材払出位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_SYUZAIHARAI_COL As Integer = 9
        Private Const EXCEL_IMPORT_HEAD_SYUZAIHARAI_LBL As String = "主材払出"
        ''' <summary>Excel取込でのヘッダー・ＯＷ検収位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_OWKENSYU_COL As Integer = 10
        Private Const EXCEL_IMPORT_HEAD_OWKENSYU_LBL As String = "ＯＷ検収"
        ''' <summary>Excel取込でのヘッダー・ＯＷ支給位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_OWSHIKYU_COL As Integer = 11
        Private Const EXCEL_IMPORT_HEAD_OWSHIKYU_LBL As String = "ＯＷ支給"
        ''' <summary>Excel取込でのヘッダー・ＯＷ支給位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_KAIGAIKENSYU_COL As Integer = 12
        Private Const EXCEL_IMPORT_HEAD_KAIGAIKENSYU_LBL As String = "海外検収"
        ''' <summary>Excel取込でのヘッダー・１原振伝位置及びラベル</summary>
        Private Const EXCEL_IMPORT_HEAD_1GENFURIDEN_COL As Integer = 13
        Private Const EXCEL_IMPORT_HEAD_1GENFURIDEN_LBL As String = "１原振伝"
        'excelの以下のその他の項目もあとでチェック用に追記する。
        ''
        ''
        ''
        ''

        ''' <summary>Excel取込でのデータ開始位置</summary>
        Private Const EXCEL_IMPORT_DATA_START_ROW As Integer = 8
        Private Const EXCEL_IMPORT_DATA_START_COL As Integer = 2
        ''' <summary>Excel取込でのデータ列数</summary>
        Private Const EXCEL_IMPORT_DATA_COL_COUNT As Integer = 26

        ''' <summary>予算コードの列位置</summary>
        Private Const XLS_COL_YOSANCODE As Integer = 4
        ''' <summary>取込元の列位置</summary>
        Private Const XLS_COL_ZAIRYOUHI As Integer = 23
        Private Const XLS_COL_TOKUWARIHI As Integer = 24

        ''' <summary>Excel取込でのヘッダー・部品番号位置及びラベル</summary>
        Private Const BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINNO_COL As Integer = 1
        Private Const BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINNO_LBL As String = "部品番号"
        ''' <summary>Excel取込でのヘッダー・部品単価位置及びラベル</summary>
        Private Const BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINTANKA_COL As Integer = 2
        Private Const BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINTANKA_LBL As String = "部品単価"

        ''' <summary>Excel取込でのヘッダー位置</summary>
        Private Const BUHIN_TANKA_EXCEL_IMPORT_HEAD_ROW As Integer = 1
        ''' <summary>Excel取込でのデータ開始位置</summary>
        Private Const BUHIN_TANKA_EXCEL_IMPORT_DATA_START_ROW As Integer = 2
        ''' <summary>Excel取込でのデータ列数</summary>
        Private Const BUHIN_TANKA_EXCEL_IMPORT_DATA_COL_COUNT As Integer = 2

#End Region

#Region " 各列TAG "
        ''' <summary>区分</summary>
        Private Const TAG_YOSAN_EVENT_KBN_NAME As String = "YOSAN_EVENT_KBN_NAME"
        ''' <summary>開発符号</summary>
        Public Const TAG_YOSAN_KAIHATSU_FUGO As String = "YOSAN_KAIHATSU_FUGO"
        ''' <summary>イベント</summary>
        Public Const TAG_YOSAN_EVENT As String = "YOSAN_EVENT"
        ''' <summary>イベント名称</summary>
        Public Const TAG_YOSAN_EVENT_NAME As String = "YOSAN_EVENT_NAME"
        ''' <summary>予算コード</summary>
        Public Const TAG_YOSAN_CODE As String = "YOSAN_CODE"
        ''' <summary>予算期間FROM</summary>
        Private Const TAG_YOSAN_KIKAN_FROM As String = "YOSAN_KIKAN_FROM"
        ''' <summary>予算期間記号</summary>
        Private Const TAG_YOSAN_KIKAN_SYMBOL As String = "YOSAN_KIKAN_SYMBOL"
        ''' <summary>予算期間TO</summary>
        Private Const TAG_YOSAN_KIKAN_TO As String = "YOSAN_KIKAN_TO"
        ''' <summary>予算製作台数・完成車</summary>
        Private Const TAG_YOSAN_SEISAKUDAISU_KANSEISYA As String = "YOSAN_SEISAKUDAISU_KANSEISYA"
        ''' <summary>予算製作台数記号</summary>
        Private Const TAG_YOSAN_SEISAKUDAISU_SYMBOL As String = "YOSAN_SEISAKUDAISU_SYMBOL"
        ''' <summary>予算製作台数・WB車</summary>
        Private Const TAG_YOSAN_SEISAKUDAISU_WB As String = "YOSAN_SEISAKUDAISU_WB"
        ''' <summary>主な変更概要</summary>
        Public Const TAG_YOSAN_MAIN_HENKO_GAIYO As String = "YOSAN_MAIN_HENKO_GAIYO"
        ''' <summary>造り方及び製作条件</summary>
        Public Const TAG_YOSAN_TSUKURIKATA_SEISAKUJYOKEN As String = "YOSAN_TSUKURIKATA_SEISAKUJYOKEN"
        ''' <summary>その他</summary>
        Public Const TAG_YOSAN_SONOTA As String = "YOSAN_SONOTA"
        ''' <summary>予算イベントコード</summary>
        Public Const TAG_YOSAN_EVENT_CODE As String = "YOSAN_EVENT_CODE"
#End Region

#Region " 各DB列 "
        ''' <summary>区分</summary>
        Private Const DB_YOSAN_EVENT_KBN_NAME As String = "YOSAN_EVENT_KBN_NAME"
        ''' <summary>開発符号</summary>
        Private Const DB_YOSAN_KAIHATSU_FUGO As String = "YOSAN_KAIHATSU_FUGO"
        ''' <summary>イベント</summary>
        Private Const DB_YOSAN_EVENT As String = "YOSAN_EVENT"
        ''' <summary>イベント名称</summary>
        Private Const DB_YOSAN_EVENT_NAME As String = "YOSAN_EVENT_NAME"
        ''' <summary>予算コード</summary>
        Private Const DB_YOSAN_CODE As String = "YOSAN_CODE"
        ''' <summary>予算期間FROM年</summary>
        Private Const DB_YOSAN_KIKAN_FROM_YYYY As String = "YOSAN_KIKAN_FROM_YYYY"
        ''' <summary>予算期間FROM上期下期</summary>
        Private Const DB_YOSAN_KIKAN_FROM_KS As String = "YOSAN_KIKAN_FROM_KS"
        ''' <summary>予算期間TO年</summary>
        Private Const DB_YOSAN_KIKAN_TO_YYYY As String = "YOSAN_KIKAN_TO_YYYY"
        ''' <summary>予算期間TO上期下期</summary>
        Private Const DB_YOSAN_KIKAN_TO_KS As String = "YOSAN_KIKAN_TO_KS"
        ''' <summary>予算製作台数・完成車</summary>
        Private Const DB_YOSAN_SEISAKUDAISU_KANSEISYA As String = "YOSAN_SEISAKUDAISU_KANSEISYA"
        ''' <summary>予算製作台数記号</summary>
        Private Const DB_YOSAN_SEISAKUDAISU_SYMBOL As String = "YOSAN_SEISAKUDAISU_SYMBOL"
        ''' <summary>予算製作台数・WB車</summary>
        Private Const DB_YOSAN_SEISAKUDAISU_WB As String = "YOSAN_SEISAKUDAISU_WB"
        ''' <summary>主な変更概要</summary>
        Private Const DB_YOSAN_MAIN_HENKO_GAIYO As String = "YOSAN_MAIN_HENKO_GAIYO"
        ''' <summary>造り方及び製作条件</summary>
        Private Const DB_YOSAN_TSUKURIKATA_SEISAKUJYOKEN As String = "YOSAN_TSUKURIKATA_SEISAKUJYOKEN"
        ''' <summary>その他</summary>
        Private Const DB_YOSAN_SONOTA As String = "YOSAN_SONOTA"
        ''' <summary>予算イベントコード</summary>
        Private Const DB_YOSAN_EVENT_CODE As String = "YOSAN_EVENT_CODE"
#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="f"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal f As FrmYosanEventList, ByVal LoginInfo As LoginInfo)
            m_view = f
            aLoginInfo = LoginInfo
            aSystemDate = New ShisakuDate
            ExclusiveEventDao = New TYosanExclusiveControlEventDaoImpl
            EventDao = New TYosanEventDaoImpl
        End Sub
#End Region

#Region "ヘッダー部初期化 "
        ''' <summary>
        '''ヘッダー部を初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeHeader()
            'ヘッダー部分の初期設定
            ShisakuFormUtil.Initialize(m_view)
            ShisakuFormUtil.setTitleVersion(m_view)
            ShisakuFormUtil.SetIdAndBuka(m_view.LblLoginUserId, m_view.LblLoginBukaName)
            ShisakuFormUtil.SetDateTimeNow(m_view.LblDateNow, m_view.LblTimeNow)
            '画面のPG-IDが表示されます。
            m_view.LblCurrPGId.Text = "PG-ID :" + "YOSAN EVENT LIST"

            'コンボボックス初期設定
            ShisakuFormUtil.SettingDefaultProperty(m_view.cmbKaihatsuFugo)
            ShisakuFormUtil.SettingDefaultProperty(m_view.cmbEvent)
            ShisakuFormUtil.SettingDefaultProperty(m_view.cmbKikan)
        End Sub
#End Region

#Region "開発符号のコンボボックスを作成"

        ''' <summary>
        ''' 開発符号のコンボボックスを作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetKaihatsuFugoCombo()
            Try
                '開発符号コンボボックスに値を追加
                Dim dtKaihatsufugo As DataTable = GetKaihatsuFugoData()
                Dim vos As New List(Of LabelValueVo)
                For idx As Integer = 0 To dtKaihatsufugo.Rows.Count - 1
                    Dim vo As New LabelValueVo
                    vo.Label = dtKaihatsufugo.Rows(idx).Item(0)
                    vo.Value = vo.Label
                    vos.Add(vo)
                Next
                FormUtil.BindLabelValuesToComboBox(m_view.cmbKaihatsuFugo, vos, True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' E-BOM開発符号よりイベントコードを設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetKaihatsuFugoData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(DataSqlCommon.GetKaihatufugouComboSql("Fugo"), dtData)
            End Using
            Return dtData
        End Function

#End Region

#Region "期間のコンボボックスを作成"

        ''' <summary>
        ''' 期間のコンボボックスを作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetKikanCombo()
            Try
                '期間コンボボックスに値を追加
                Dim dtKikan As DataTable = GetKikanData()
                Dim vos As New List(Of LabelValueVo)
                Dim vo As LabelValueVo

                If dtKikan.Rows.Count > 0 Then

                    Dim minYyyy As String = dtKikan.Rows(0).Item(0).ToString
                    Dim maxYyyy As String = dtKikan.Rows(0).Item(1).ToString
                    Dim minKs As String = DOWN_KS
                    Dim maxKs As String = UP_KS

                    For idx As Integer = 0 To dtKikan.Rows.Count - 1
                        If StringUtil.IsNotEmpty(dtKikan.Rows(idx).Item(2)) Then
                            If String.Equals(dtKikan.Rows(idx).Item(2), UP_KS) Then
                                minKs = UP_KS
                            End If
                        End If
                        If StringUtil.IsNotEmpty(dtKikan.Rows(idx).Item(3)) Then
                            If String.Equals(dtKikan.Rows(idx).Item(3), UP_KS) Then
                                minKs = UP_KS
                            End If
                        End If
                        If StringUtil.IsNotEmpty(dtKikan.Rows(idx).Item(4)) Then
                            If String.Equals(dtKikan.Rows(idx).Item(4), DOWN_KS) Then
                                maxKs = DOWN_KS
                            End If
                        End If
                        If StringUtil.IsNotEmpty(dtKikan.Rows(idx).Item(5)) Then
                            If String.Equals(dtKikan.Rows(idx).Item(5), DOWN_KS) Then
                                maxKs = DOWN_KS
                            End If
                        End If
                    Next

                    'FROMYYYY
                    If String.Equals(minKs, UP_KS) Then
                        vo = New LabelValueVo
                        vo.Label = minYyyy & UP_KS
                        vo.Value = vo.Label
                        vos.Add(vo)
                        vo = New LabelValueVo
                        vo.Label = minYyyy & DOWN_KS
                        vo.Value = vo.Label
                        vos.Add(vo)
                    Else
                        vo = New LabelValueVo
                        vo.Label = minYyyy & DOWN_KS
                        vo.Value = vo.Label
                        vos.Add(vo)
                    End If

                    'FROMYYYY～TOYYYYに値が有ればコンボボックスへ値をセットする。
                    If StringUtil.IsNotEmpty(minYyyy) And StringUtil.IsNotEmpty(maxYyyy) Then

                        For idx As Integer = CInt(minYyyy) + 1 To CInt(maxYyyy) - 1
                            vo = New LabelValueVo
                            vo.Label = idx.ToString & UP_KS
                            vo.Value = vo.Label
                            vos.Add(vo)
                            vo = New LabelValueVo
                            vo.Label = idx.ToString & DOWN_KS
                            vo.Value = vo.Label
                            vos.Add(vo)
                        Next

                        'TOYYYY
                        If String.Equals(maxKs, DOWN_KS) Then
                            vo = New LabelValueVo
                            vo.Label = maxYyyy & UP_KS
                            vo.Value = vo.Label
                            vos.Add(vo)
                            vo = New LabelValueVo
                            vo.Label = maxYyyy & DOWN_KS
                            vo.Value = vo.Label
                            vos.Add(vo)
                        Else
                            vo = New LabelValueVo
                            vo.Label = maxYyyy & UP_KS
                            vo.Value = vo.Label
                            vos.Add(vo)
                        End If

                        FormUtil.BindLabelValuesToComboBox(m_view.cmbKikan, vos, True)

                    End If

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 予算書イベント情報より予算期間を設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetKikanData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(GetEventKikanSql, dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 予算期間を取得するSQL
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetEventKikanSql() As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("      B.MIN_YYYY AS MIN_YYYY, B.MAX_YYYY AS MAX_YYYY, ")
                .AppendLine("      CASE WHEN T.YOSAN_KIKAN_FROM_YYYY=B.MIN_YYYY THEN T.YOSAN_KIKAN_FROM_KS END AS MIN_KS_FROM, ")
                .AppendLine("      CASE WHEN  T.YOSAN_KIKAN_TO_YYYY=B.MIN_YYYY THEN T.YOSAN_KIKAN_TO_KS END AS MIN_KS_TO, ")
                .AppendLine("      CASE WHEN T.YOSAN_KIKAN_FROM_YYYY=B.MAX_YYYY THEN T.YOSAN_KIKAN_FROM_KS END AS MAX_KS_FROM, ")
                .AppendLine("      CASE WHEN  T.YOSAN_KIKAN_TO_YYYY=B.MAX_YYYY THEN T.YOSAN_KIKAN_TO_KS END AS MAX_KS_TO ")
                .AppendLine("FROM  (SELECT * FROM " + MBOM_DB_NAME + ".dbo.T_YOSAN_EVENT WHERE YOSAN_STATUS = '00') T, ")
                .AppendLine("      (SELECT CASE WHEN A.FROM_MIN_YYYY<A.TO_MIN_YYYY THEN A.FROM_MIN_YYYY ELSE A.TO_MIN_YYYY END AS MIN_YYYY, ")
                .AppendLine("              CASE WHEN A.FROM_MAX_YYYY>A.TO_MAX_YYYY THEN A.FROM_MAX_YYYY ELSE A.TO_MAX_YYYY END AS MAX_YYYY ")
                .AppendLine("         FROM ")
                .AppendLine("              (SELECT ")
                .AppendLine("                     MIN(YOSAN_KIKAN_FROM_YYYY) AS FROM_MIN_YYYY, ")
                .AppendLine("                     MAX(YOSAN_KIKAN_FROM_YYYY) AS FROM_MAX_YYYY, ")
                .AppendLine("                     MIN(YOSAN_KIKAN_TO_YYYY) AS TO_MIN_YYYY, ")
                .AppendLine("                     MAX(YOSAN_KIKAN_TO_YYYY) AS TO_MAX_YYYY ")
                .AppendLine("                FROM " + MBOM_DB_NAME + ".dbo.T_YOSAN_EVENT ")
                .AppendLine("               WHERE YOSAN_STATUS = '00') A) B")
            End With

            Return sql.ToString()
        End Function

#End Region

#Region "ボタンを使用可または使用不可とする。"

        ''' <summary>
        ''' 編集ボタンを使用不可／使用可設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetEditBtn(ByVal canDo As Boolean)
            m_view.btnEdit.Enabled = canDo
        End Sub

        ''' <summary>
        ''' 閲覧ボタンを使用不可／使用可設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetCallBtn(ByVal canDo As Boolean)
            m_view.btnCall.Enabled = canDo
        End Sub

        ''' <summary>
        ''' 削除ボタンを使用不可／使用可設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetDelBtn(ByVal canDo As Boolean)
            m_view.btnDel.Enabled = canDo
        End Sub

        ''' <summary>
        ''' 完了ボタンを使用不可／使用可設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetCompleteBtn(ByVal canDo As Boolean)
            m_view.btnComplete.Enabled = canDo
        End Sub

        ''' <summary>
        ''' 新規作成ボタンを使用不可／使用可設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetNewLock(ByVal canDo As Boolean)
            m_view.btnNew.Enabled = canDo
        End Sub

#End Region

#Region "完了ラベルを表示または非表示とする。"
        ''' <summary>
        ''' 完了ラベルを表示／非表示設定
        ''' </summary>
        ''' <param name="isDisplay"></param>
        ''' <remarks></remarks>
        Public Sub SetCompleteMsgLabel(ByVal isDisplay As Boolean)
            m_view.LblCompleteMsg.Visible = isDisplay
        End Sub
#End Region

#Region "スプレッド初期化"
        ''' <summary>
        ''' スプレッドを初期化する      
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeSpread(ByVal compleFlg As String)
            'Spreadの初期化
            SpreadUtil.Initialize(m_view.spdParts)

            'SPREADの列のタグ値を設定する
            SetSpdColTag()
            'SPREADの列のデータフィールドを設定する
            SetSpdDataField()
            'SPREADのデータソースを設定する
            SetSpreadData(compleFlg, True)
            'SPREADの列のセルの水平方向の配置を再設定する
            SetSpdColPro()
            'SPREADの非表示列を設定する
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_EVENT_CODE).Visible = False
            'SPREADの列をロックする
            For index As Integer = 0 To m_view.spdParts_Sheet1.Columns.Count - 1
                m_view.spdParts_Sheet1.Columns(index).Locked = True
            Next
        End Sub

        ''' <summary>
        ''' SPREADの列のタグ値を設定する        
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSpdColTag()
            Dim index As Integer = 0
            '区分
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_EVENT_KBN_NAME
            '開発符号
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KAIHATSU_FUGO
            'イベント
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_EVENT
            'イベント名称
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_EVENT_NAME
            '予算コード
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_CODE
            '予算期間FROM
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KIKAN_FROM
            '予算期間記号
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KIKAN_SYMBOL
            '予算期間TO
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_KIKAN_TO
            '予算製作台数・完成車
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_SEISAKUDAISU_KANSEISYA
            '予算製作台数記号
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_SEISAKUDAISU_SYMBOL
            '予算製作台数・WB車
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_SEISAKUDAISU_WB
            '主な変更概要
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_MAIN_HENKO_GAIYO
            '造り方及び製作条件
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_TSUKURIKATA_SEISAKUJYOKEN
            'その他
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_SONOTA
            '予算イベントコード
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_YOSAN_EVENT_CODE
        End Sub

        ''' <summary>
        ''' SPREADの列のデータフィールドを設定する        
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSpdDataField()
            '列の項目を設定する           
            For i As Integer = 0 To m_view.spdParts_Sheet1.ColumnCount - 1
                m_view.spdParts_Sheet1.Columns(i).DataField = m_view.spdParts_Sheet1.Columns(i).Tag
            Next
        End Sub

        ''' <summary>
        ''' SPREADで 列のセルの水平方向の配置を設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSpdColPro()
            '区分
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_EVENT_KBN_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            '開発符号
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_KAIHATSU_FUGO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            'イベント
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_EVENT).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            'イベント名称
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_EVENT_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            '予算コード
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_CODE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            '予算期間FROM
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_KIKAN_FROM).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            '予算期間記号
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_KIKAN_SYMBOL).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            '予算期間TO
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_KIKAN_TO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            '予算製作台数・完成車
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_SEISAKUDAISU_KANSEISYA).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            '予算製作台数記号
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_SEISAKUDAISU_SYMBOL).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            '予算製作台数・WB車
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_SEISAKUDAISU_WB).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            '主な変更概要
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_MAIN_HENKO_GAIYO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            '造り方及び製作条件
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_TSUKURIKATA_SEISAKUJYOKEN).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            'その他
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_SONOTA).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            '予算イベントコード
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_EVENT_CODE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
        End Sub

#End Region

#Region "スプレッドにデータ設定"
        ''' <summary>
        ''' スプレッドにデータを設定する
        ''' </summary>
        ''' <param name="compleFlg"></param>
        ''' <param name="initFlg"></param>
        ''' <remarks></remarks>
        Public Sub SetSpreadData(ByVal compleFlg As String, Optional ByVal initFlg As Boolean = False)
            Dim dtSrpead As DataTable
            If initFlg Then
                dtSrpead = GetSpreadList(compleFlg)
            Else
                dtSrpead = GetSpreadList(compleFlg, m_view.cmbKaihatsuFugo.Text, m_view.cmbEvent.Text, m_view.cmbKikan.Text)
            End If

            If m_view.spdParts_Sheet1.RowCount > 0 Then
                m_view.spdParts_Sheet1.RemoveRows(0, m_view.spdParts_Sheet1.RowCount)
            End If

            m_view.spdParts_Sheet1.RowCount = dtSrpead.Rows.Count

            For index = 0 To dtSrpead.Rows.Count - 1
                '区分
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_EVENT_KBN_NAME)).Value = dtSrpead.Rows(index)(DB_YOSAN_EVENT_KBN_NAME)
                '開発符号
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_KAIHATSU_FUGO)).Value = dtSrpead.Rows(index)(DB_YOSAN_KAIHATSU_FUGO)
                'イベント
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_EVENT)).Value = dtSrpead.Rows(index)(DB_YOSAN_EVENT)
                'イベント名称
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_EVENT_NAME)).Value = dtSrpead.Rows(index)(DB_YOSAN_EVENT_NAME)
                '予算コード
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_CODE)).Value = dtSrpead.Rows(index)(DB_YOSAN_CODE)
                '予算期間FROM
                Dim strFrom As String = String.Empty
                If StringUtil.IsNotEmpty(dtSrpead.Rows(index)(DB_YOSAN_KIKAN_FROM_YYYY)) And StringUtil.IsNotEmpty(dtSrpead.Rows(index)(DB_YOSAN_KIKAN_FROM_KS)) Then
                    strFrom = dtSrpead.Rows(index)(DB_YOSAN_KIKAN_FROM_YYYY) & "年" & dtSrpead.Rows(index)(DB_YOSAN_KIKAN_FROM_KS)
                End If
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_KIKAN_FROM)).Value = strFrom
                '予算期間TO
                Dim strTo As String = String.Empty
                If StringUtil.IsNotEmpty(dtSrpead.Rows(index)(DB_YOSAN_KIKAN_TO_YYYY)) And StringUtil.IsNotEmpty(dtSrpead.Rows(index)(DB_YOSAN_KIKAN_TO_KS)) Then
                    strTo = dtSrpead.Rows(index)(DB_YOSAN_KIKAN_TO_YYYY) & "年" & dtSrpead.Rows(index)(DB_YOSAN_KIKAN_TO_KS)
                End If
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_KIKAN_TO)).Value = strTo
                '予算期間記号
                If StringUtil.IsNotEmpty(strFrom) Or StringUtil.IsNotEmpty(strTo) Then
                    m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_KIKAN_SYMBOL)).Value = "～"
                End If
                '予算製作台数・完成車
                Dim strKansei As String = "0"
                If StringUtil.IsNotEmpty(dtSrpead.Rows(index)(DB_YOSAN_SEISAKUDAISU_KANSEISYA)) Then
                    strKansei = dtSrpead.Rows(index)(DB_YOSAN_SEISAKUDAISU_KANSEISYA)
                End If
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_SEISAKUDAISU_KANSEISYA)).Value = strKansei
                '予算製作台数・WB車
                Dim strWb As String = "0"
                If StringUtil.IsNotEmpty(dtSrpead.Rows(index)(DB_YOSAN_SEISAKUDAISU_WB)) Then
                    strWb = dtSrpead.Rows(index)(DB_YOSAN_SEISAKUDAISU_WB)
                End If
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_SEISAKUDAISU_WB)).Value = strWb
                If StringUtil.IsNotEmpty(strKansei) Or StringUtil.IsNotEmpty(strWb) Then
                    '予算製作台数記号
                    m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_SEISAKUDAISU_SYMBOL)).Value = "＋"
                End If
                '主な変更概要
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_MAIN_HENKO_GAIYO)).Value = dtSrpead.Rows(index)(DB_YOSAN_MAIN_HENKO_GAIYO)
                '造り方及び製作条件
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_TSUKURIKATA_SEISAKUJYOKEN)).Value = dtSrpead.Rows(index)(DB_YOSAN_TSUKURIKATA_SEISAKUJYOKEN)
                'その他
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_SONOTA)).Value = dtSrpead.Rows(index)(DB_YOSAN_SONOTA)
                '予算イベントコード
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_EVENT_CODE)).Value = dtSrpead.Rows(index)(DB_YOSAN_EVENT_CODE)
            Next
        End Sub

        ''' <summary>
        ''' スプレッドのデータを取得する
        ''' </summary>
        ''' <param name="compleFlg"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSpreadList(ByVal compleFlg As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetYosanEventList(compleFlg), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 予算イベント情報からデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetYosanEventList(ByVal compleFlg As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT ")
                If String.Equals(compleFlg, "0") Then
                    .AppendLine("WHERE YOSAN_STATUS = '" & STATUS_00 & "' OR YOSAN_STATUS IS NULL ")
                Else
                    .AppendLine("WHERE YOSAN_STATUS = '" & STATUS_01 & "' ")
                End If
                .AppendLine("ORDER BY YOSAN_EVENT_CODE")
            End With
            Return sql.ToString()
        End Function

        ''' <summary>
        ''' スプレッドのデータを取得する
        ''' </summary>
        ''' <param name="compleFlg"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSpreadList(ByVal compleFlg As String, ByVal kaihatsuFugo As String, ByVal eventPhase As String, ByVal kikan As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetYosanEventList(compleFlg, kaihatsuFugo, eventPhase, kikan), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 予算イベント情報からデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetYosanEventList(ByVal compleFlg As String, ByVal kaihatsuFugo As String, ByVal eventPhase As String, ByVal kikan As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT T.* ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT T, ")
                .AppendLine("(SELECT YOSAN_EVENT_CODE, ")
                .AppendLine("CONVERT(VARCHAR,YOSAN_KIKAN_FROM_YYYY)+''+CONVERT(VARCHAR,CASE WHEN YOSAN_KIKAN_FROM_KS = '上期' THEN 0 ELSE 1 END) AS FORM_KS, ")
                .AppendLine("CONVERT(VARCHAR,YOSAN_KIKAN_TO_YYYY)+''+CONVERT(VARCHAR,CASE WHEN YOSAN_KIKAN_TO_KS = '上期' THEN 0 ELSE 1 END) AS END_KS ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT ")
                If String.Equals(compleFlg, "0") Then
                    .AppendLine("WHERE YOSAN_STATUS = '" & STATUS_00 & "' OR YOSAN_STATUS IS NULL ")
                Else
                    .AppendLine("WHERE YOSAN_STATUS = '" & STATUS_01 & "' ")
                End If
                .AppendLine(") A ")
                .AppendLine("WHERE T.YOSAN_EVENT_CODE = A.YOSAN_EVENT_CODE ")
                If StringUtil.IsNotEmpty(kaihatsuFugo) Then
                    .AppendLine("AND T.YOSAN_KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                End If
                If StringUtil.IsNotEmpty(eventPhase) Then
                    .AppendLine("AND T.YOSAN_EVENT = '" & eventPhase & "' ")
                End If
                If StringUtil.IsNotEmpty(kikan) Then
                    If kikan.Length >= 6 Then
                        Dim strYyyy As String = kikan.Substring(0, 4)
                        Dim strKs As String = kikan.Substring(4, 2)
                        Dim strKikan As String
                        If String.Equals(strKs, UP_KS) Then
                            strKikan = strYyyy & "0"
                        Else
                            strKikan = strYyyy & "1"
                        End If
                        .AppendLine("AND A.FORM_KS <= '" & strKikan & "' ")
                        .AppendLine("AND A.END_KS >= '" & strKikan & "' ")
                    End If
                End If
                .AppendLine("ORDER BY T.YOSAN_EVENT_CODE")
            End With
            Return sql.ToString()
        End Function

#End Region

#Region "EXCEL出力ボタンを押したら処理"
        ''' <summary>
        ''' EXCELボタンを押す。
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ExcelBtnClick(ByVal strFlg As String)
            ResetColor()
            Dim spdExcel As New FarPoint.Win.Spread.FpSpread()
            spdExcel.Sheets.Count = 1
            'EXCEl列タイトルと項目を取得する
            SetTitleAndItemList()
            Dim sheetExcel As FarPoint.Win.Spread.SheetView = spdExcel.Sheets(0)
            sheetExcel.ColumnCount = lstExcelTitle.Count
            sheetExcel.AutoGenerateColumns = False
            '列タグの設定
            SetSheetTag(sheetExcel)
            '列タイトルの設定
            SetSheetTitle(sheetExcel)
            'データフィールドの設定
            SetSheetDataField(sheetExcel)
            'データの取得
            Dim dtExcelData As DataTable = GetExcelData()
            sheetExcel.DataSource = dtExcelData
            SetSpdExcelColPro(spdExcel)

            ExcelCommon.SaveExcelFile(" 予算書イベント一覧 " + Now.ToString("MMdd") + Now.ToString("HHmm"), spdExcel, "EventList")
        End Sub

        ''' <summary>
        ''' バックカラーを戻る
        ''' </summary>
        ''' <remarks></remarks>
        ''' 
        Private Sub ResetColor()
            For i As Integer = 0 To m_view.spdParts_Sheet1.RowCount - 1
                ShisakuFormUtil.initlColor(m_view.spdParts_Sheet1.Rows(i))
            Next
        End Sub

        ''' <summary>
        ''' EXCEl列タイトルと項目を取得する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetTitleAndItemList()
            lstExcelTitle = New List(Of String)
            lstExcelItem = New List(Of String)
            '1
            lstExcelTitle.Add("区分")
            lstExcelItem.Add("YOSAN_EVENT_KBN_NAME")
            '2
            lstExcelTitle.Add("開発符号")
            lstExcelItem.Add("YOSAN_KAIHATSU_FUGO")
            '3
            lstExcelTitle.Add("イベント")
            lstExcelItem.Add("YOSAN_EVENT")
            '4
            lstExcelTitle.Add("イベント名称")
            lstExcelItem.Add("YOSAN_EVENT_NAME")
            '5
            lstExcelTitle.Add("予算コード")
            lstExcelItem.Add("YOSAN_CODE")
            '6
            lstExcelTitle.Add("予算期間FROM")
            lstExcelItem.Add("YOSAN_KIKAN_FROM")
            '7
            lstExcelTitle.Add("予算期間記号")
            lstExcelItem.Add("YOSAN_KIKAN_SYMBOL")
            '8
            lstExcelTitle.Add("予算期間TO")
            lstExcelItem.Add("YOSAN_KIKAN_TO")
            '9
            lstExcelTitle.Add("予算製作台数・完成車")
            lstExcelItem.Add("YOSAN_SEISAKUDAISU_KANSEISYA")
            '10
            lstExcelTitle.Add("予算製作台数記号")
            lstExcelItem.Add("YOSAN_SEISAKUDAISU_SYMBOL")
            '11
            lstExcelTitle.Add("予算製作台数・WB車")
            lstExcelItem.Add("YOSAN_SEISAKUDAISU_WB")
            '12
            lstExcelTitle.Add("主な変更概要")
            lstExcelItem.Add("YOSAN_MAIN_HENKO_GAIYO")
            '13
            lstExcelTitle.Add("造り方及び製作条件")
            lstExcelItem.Add("YOSAN_TSUKURIKATA_SEISAKUJYOKEN")
            '14
            lstExcelTitle.Add("その他")
            lstExcelItem.Add("YOSAN_SONOTA")
            '15
            lstExcelTitle.Add("予算イベントコード")
            lstExcelItem.Add("YOSAN_EVENT_CODE")
        End Sub

        ''' <summary>
        '''　列タグを設定する
        ''' </summary>
        ''' <param name="sheetExcel"></param>
        ''' <remarks></remarks>
        Private Sub SetSheetTag(ByVal sheetExcel As FarPoint.Win.Spread.SheetView)
            For i As Integer = 0 To sheetExcel.ColumnCount - 1
                sheetExcel.Columns(i).Tag = lstExcelItem(i)
            Next
        End Sub

        ''' <summary>
        ''' 列タイトルを設定する
        ''' </summary>
        ''' <param name="sheetExcel"></param>
        ''' <remarks></remarks>
        Private Sub SetSheetTitle(ByVal sheetExcel As FarPoint.Win.Spread.SheetView)
            For i As Integer = 0 To sheetExcel.ColumnCount - 1
                sheetExcel.ColumnHeader.Columns(i).Label = lstExcelTitle(i)
            Next
        End Sub

        ''' <summary>
        ''' データフィールドを設定する
        ''' </summary>
        ''' <param name="sheetExcel"></param>
        ''' <remarks></remarks>
        Private Sub SetSheetDataField(ByVal sheetExcel As FarPoint.Win.Spread.SheetView)
            For i As Integer = 0 To sheetExcel.ColumnCount - 1
                sheetExcel.Columns(i).DataField = lstExcelItem(i)
            Next
        End Sub

        ''' <summary>
        ''' Excel出力用データを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetExcelData() As DataTable
            Dim dtData As New DataTable
            With m_view.spdParts_Sheet1
                For colIndex = 0 To .Columns.Count - 1
                    dtData.Columns.Add(.Columns.Item(colIndex).Tag, Type.GetType("System.String"))
                Next

                If .Rows.Count > 0 Then
                    For rowIndex = 0 To .Rows.Count - 1
                        Dim dr = dtData.NewRow
                        For colIndex = 0 To .Columns.Count - 1
                            If .GetValue(rowIndex, colIndex) IsNot Nothing Then
                                dr(colIndex) = .GetValue(rowIndex, colIndex)
                            End If
                        Next
                        dtData.Rows.Add(dr)
                    Next
                End If
            End With
            Return dtData
        End Function

        ''' <summary>
        ''' 列のセルの水平方向の配置を設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSpdExcelColPro(ByVal spdExcel As FarPoint.Win.Spread.FpSpread)
            Dim horiLeft() As String = New String() {lstExcelItem(0), lstExcelItem(1), lstExcelItem(2), lstExcelItem(3), _
                                                     lstExcelItem(4), lstExcelItem(5), lstExcelItem(6), lstExcelItem(7), _
                                                     lstExcelItem(9), lstExcelItem(11), lstExcelItem(12), lstExcelItem(13), lstExcelItem(14)}
            Dim horiRight() As String = New String() {lstExcelItem(8), lstExcelItem(10)}
            Dim spExcelCom = New SpreadCommon(spdExcel)
            For i As Integer = 0 To horiLeft.Length - 1
                spExcelCom.GetColFromTag(horiLeft(i)).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Next
            For i As Integer = 0 To horiRight.Length - 1
                spExcelCom.GetColFromTag(horiRight(i)).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Next

            spdExcel.Sheets(0).Columns(lstExcelItem(6)).Visible = False
            spdExcel.Sheets(0).Columns(lstExcelItem(9)).Visible = False
        End Sub

#End Region

#Region "パーツプライス取込ボタンを押したら処理"

#Region "Excel取込機能(メイン処理)"
        ''' <summary>
        '''Excel取込処理(メイン処理)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub PartsPriceImport()

            'ファイルパス取得
            Dim fileName As String = ImportFileDialog()

            If fileName.Equals(String.Empty) Then
                Return
            End If

            'シート名選択画面を表示して取込するシートを選択する。
            '   終了ボタン
            If SubSheetSelect(fileName) = "" Then
                Return
            End If

            'Excelファイルのフォーマットチェック処理
            If ImportBuhinTankaFileCheck(fileName) = False Then
                Return
            End If

            Dim isKokunai As Boolean
            Dim isKaigai As Boolean
            Using frmSelect As New FrmPartsPriceSelect
                frmSelect.ShowDialog()

                If frmSelect.ResultOk = False Then
                    Return
                End If

                isKokunai = frmSelect.IsKokunai
                isKaigai = frmSelect.IsKaigai
            End Using

            Cursor.Current = Cursors.WaitCursor

            '   処理中画面表示
            Dim SyorichuForm As New frm03Syorichu
            SyorichuForm.Label4.Text = "確認"
            SyorichuForm.lblKakunin.Text = ""
            SyorichuForm.lblKakunin2.Text = "取込中・・・"
            SyorichuForm.lblKakunin3.Text = ""
            SyorichuForm.Execute()
            SyorichuForm.Show()

            'Excelファイからのインポート処理
            If ImportBuhinTankaFile(fileName, isKokunai, isKaigai) = False Then
                SyorichuForm.Close()
                Return
            End If
            '予算書単価情報を追加
            If UpdateYosanTankaBy(_ExcelBuhinTankaList) = False Then
                SyorichuForm.Close()
                Return
            End If

            '処理中画面非表示
            SyorichuForm.Close()

            ComFunc.ShowInfoMsgBox("取込が完了しました。")

        End Sub
#End Region

#Region "Excel取込機能(Excelオープン)"
        ''' <summary>
        ''' Excel取込機能(Excel列チェック)
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <remarks></remarks>
        Private Function ImportBuhinTankaFileCheck(ByVal fileName As String) As Boolean

            If Not ShisakuComFunc.IsFileOpen(fileName) Then
                Using xls As New ShisakuExcel(fileName)
                    '選択したシート名をアクティブにする
                    xls.SetActiveSheet(ExcelImportSheetName)
                    '列項目エラーチェック
                    If BuhinTankaImportColCheck(xls) = False Then
                        'エラーメッセージ表示
                        Dim errMsg As String = "パーツプライス取込用のEXCELではありません。ご確認ください。"
                        ComFunc.ShowErrMsgBox(errMsg)
                        Return False
                    End If
                End Using
            End If

            Return True
        End Function
        ''' <summary>
        ''' Excel取込機能(Excel列情報取得)
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <remarks></remarks>
        Private Function ImportBuhinTankaFile(ByVal fileName As String, ByVal isKokunai As Boolean, ByVal isKaigai As Boolean) As Boolean
            _ExcelBuhinTankaList = New List(Of TYosanTankaVo)

            If Not ShisakuComFunc.IsFileOpen(fileName) Then
                Using xls As New ShisakuExcel(fileName)
                    '選択したシート名をアクティブにする
                    xls.SetActiveSheet(ExcelImportSheetName)
                    '列項目エラーチェック
                    If BuhinTankaImportCol(_ExcelBuhinTankaList, xls, isKokunai, isKaigai) = False Then
                        'エラーメッセージ表示
                        '_ExcelImportList.Clear()
                        Dim errMsg As String = "該当データがありません。ご確認ください。"
                        ComFunc.ShowErrMsgBox(errMsg)
                        Return False
                    End If
                End Using
            End If

            Return True
        End Function
#End Region

#Region "Excel取込機能(Excel列チェック)"
        ''' <summary>
        ''' Excel取込機能(Excel列チェック)
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function BuhinTankaImportColCheck(ByVal xls As ShisakuExcel) As Boolean
            'データ無しの場合
            Dim xlsEndRow As Integer = 0
            For i As Integer = xls.EndRow To BUHIN_TANKA_EXCEL_IMPORT_DATA_START_ROW Step -1
                If StringUtil.IsNotEmpty(xls.GetValue(BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINNO_COL, i)) Then
                    xlsEndRow = i
                    Exit For
                End If
            Next
            If xlsEndRow = 0 Then Return False

            '列数不満の場合
            Dim xlsEndCol As Integer = xls.EndCol
            If xlsEndCol < BUHIN_TANKA_EXCEL_IMPORT_DATA_COL_COUNT Then
                Return False
            End If

            'ヘッダーラベルチェック
            '   部品番号
            If Not StringUtil.Equals(xls.GetValue(BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINNO_COL, BUHIN_TANKA_EXCEL_IMPORT_HEAD_ROW), BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINNO_LBL) Then
                Return False
            End If
            '   部品単価
            If Not StringUtil.Equals(xls.GetValue(BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINTANKA_COL, BUHIN_TANKA_EXCEL_IMPORT_HEAD_ROW), BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINTANKA_LBL) Then
                Return False
            End If
          
            '必要な列のチェックを行う
            For RowIndex As Integer = BUHIN_TANKA_EXCEL_IMPORT_DATA_START_ROW To xlsEndRow
                '部品番号列のチェック
                Dim buhinNo As String = Nothing
                If StringUtil.IsNotEmpty(xls.GetValue(BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINNO_COL, RowIndex)) Then
                    buhinNo = xls.GetValue(BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINNO_COL, RowIndex).ToString.Trim
                    If Not ShisakuComFunc.IsInLength(buhinNo, 15) Then
                        Return False
                    End If
                End If
                If StringUtil.IsEmpty(buhinNo) Then
                    Continue For
                End If

                '部品単価列のチェック
                Dim tanka As String = Nothing
                If StringUtil.IsNotEmpty(xls.GetValue(BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINTANKA_COL, RowIndex)) Then
                    tanka = xls.GetValue(BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINTANKA_COL, RowIndex).ToString.Trim
                    If Not NumericCheck(tanka) Then
                        Return False
                    End If
                    If Not ShisakuComFunc.IsInLength(tanka, 16) Then
                        Return False
                    End If
                End If

            Next

            Return True

        End Function
#End Region

#Region "Excel取込機能"
        ''' <summary>
        ''' Excel取込機能(Excel列チェック)
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function BuhinTankaImportCol(ByVal aVoList As List(Of TYosanTankaVo), ByVal xls As ShisakuExcel, _
                                             ByVal isKokunai As Boolean, ByVal isKaigai As Boolean) As Boolean
            'データ無しの場合
            Dim xlsEndRow As Integer = 0
            For i As Integer = xls.EndRow To BUHIN_TANKA_EXCEL_IMPORT_DATA_START_ROW Step -1
                If StringUtil.IsNotEmpty(xls.GetValue(BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINNO_COL, i)) Then
                    xlsEndRow = i
                    Exit For
                End If
            Next
            If xlsEndRow = 0 Then Return False

            '必要な列のチェックを行う
            For RowIndex As Integer = BUHIN_TANKA_EXCEL_IMPORT_DATA_START_ROW To xlsEndRow
                Dim Vo As New TYosanTankaVo

                '部品番号列のチェック
                Dim buhinNo As String = Nothing
                If StringUtil.IsNotEmpty(xls.GetValue(BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINNO_COL, RowIndex)) Then
                    buhinNo = xls.GetValue(BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINNO_COL, RowIndex).ToString.Trim
                Else
                    '部品番号が無い行がある。スルーする。
                    Continue For
                End If

                '材料検収費列のチェック
                Dim tanka As String = Nothing
                If StringUtil.IsNotEmpty(xls.GetValue(BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINTANKA_COL, RowIndex)) Then
                    tanka = xls.GetValue(BUHIN_TANKA_EXCEL_IMPORT_HEAD_BUHINTANKA_COL, RowIndex).ToString.Trim
                End If

                Dim strKubun As String = Nothing
                If isKokunai Then
                    strKubun = KOKUNAI_KBN
                End If
                If isKaigai Then
                    strKubun = KAIGAI_KBN
                End If

                'If isExistYosanBuhinEdit(buhinNo) Then
                'EXCELの部品番号で「予算書部品編集情報」（KEYは部品番号 And ステータス="00"）にマッチの場合
                Vo.KokunaiKaigaiKbn = strKubun
                Vo.YosanBuhinNo = buhinNo
                Vo.YosanBuhinHiRyosan = tanka

                aVoList.Add(Vo)
                'End If
            Next

            'データ件数が０の場合FALSEを返す。
            If aVoList.Count = 0 Then
                Return False
            Else
                Return True
            End If

        End Function
#End Region

#Region "EXCELの部品番号で「予算書部品編集情報」から取得出来かどうかの判断"
        ''' <summary>
        ''' EXCELの部品番号で「予算書部品編集情報」から取得出来かどうかの判断
        ''' </summary>
        ''' <param name="buhinNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function isExistYosanBuhinEdit(ByVal buhinNo As String) As Boolean

            Dim eventListDao As YosanEventListDao = New YosanEventListDaoImpl
            Dim buhinEditList = eventListDao.FindYosanBuhinEdit(buhinNo)

            If buhinEditList.Count > 0 Then
                Return True
            End If
            Return False

        End Function
#End Region

#Region "予算書単価情報を上書"
        ''' <summary>
        ''' 予算書単価情報を上書
        ''' </summary>
        ''' <param name="aVoList"></param>
        ''' <remarks></remarks>
        Private Function UpdateYosanTankaBy(ByVal aVoList As List(Of TYosanTankaVo)) As Boolean
            Dim tankaDao As TYosanTankaDao = New TYosanTankaDaoImpl
            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    For Each Vo As TYosanTankaVo In aVoList
                        Vo.UpdatedUserId = aLoginInfo.UserId
                        Vo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                        Vo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                        Dim resultVo As TYosanTankaVo = tankaDao.FindByPk(Vo.KokunaiKaigaiKbn, _
                                                                          Vo.YosanBuhinNo)
                        If resultVo Is Nothing Then
                            Vo.CreatedUserId = aLoginInfo.UserId
                            Vo.CreatedDate = aSystemDate.CurrentDateDbFormat
                            Vo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                            'データ追加
                            tankaDao.InsertBy(Vo)
                        Else
                            Vo.CreatedUserId = resultVo.CreatedUserId
                            Vo.CreatedDate = resultVo.CreatedDate
                            Vo.CreatedTime = resultVo.CreatedTime
                            'データ上書
                            tankaDao.UpdateByPk(Vo)
                        End If
                    Next
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("予算書単価情報を上書出来ませんでした。")
                    Return False
                End Try

                db.Commit()
                Return True
            End Using
        End Function
#End Region

#End Region

#Region "財務報告実績取込ボタンを押したら処理"

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

            'シート名選択画面を表示して取込するシートを選択する。
            '   終了ボタン
            If SubSheetSelect(fileName) = "" Then
                Return
            End If

            'Excelファイルのフォーマットチェック処理
            If ImportExcelFileCheck(fileName) = False Then
                Return
            End If

            Dim strYyyymm As String = Nothing
            Dim strYosanCode(6) As String
            Dim strKubun(6) As String
            Using frmSelect As New FrmYosanCodeAndDateSelect
                frmSelect.ShowDialog()

                If frmSelect.ResultOk = False Then
                    Return
                End If

                strYyyymm = frmSelect.YyyyMm
                strYosanCode(0) = frmSelect.YosanCodeHireiMeta
                strYosanCode(1) = frmSelect.YosanCodeHireiKou
                strYosanCode(2) = frmSelect.YosanCodeHireiYusou
                strYosanCode(3) = frmSelect.YosanCodeHireiIkan
                strYosanCode(4) = frmSelect.YosanCodeHireiTrim
                strYosanCode(5) = frmSelect.YosanCodeKoteiTrim
                strYosanCode(6) = frmSelect.YosanCodeKoteiMeta
                strKubun(0) = "H"
                strKubun(1) = "H"
                strKubun(2) = "H"
                strKubun(3) = "H"
                strKubun(4) = "H"
                strKubun(5) = "K"
                strKubun(6) = "K"
            End Using

            '同年月で取込済かチェック処理
            If ImportCheck(strYyyymm) = False Then
                Return
            End If

            Cursor.Current = Cursors.WaitCursor

            '   処理中画面表示
            Dim SyorichuForm As New frm03Syorichu
            SyorichuForm.Label4.Text = "確認"
            SyorichuForm.lblKakunin.Text = ""
            SyorichuForm.lblKakunin2.Text = "取込中・・・"
            SyorichuForm.lblKakunin3.Text = ""
            SyorichuForm.Execute()
            SyorichuForm.Show()

            For i As Integer = 0 To 6
                If StringUtil.IsNotEmpty(strYosanCode(i)) Then
                    'Excelファイからのインポート処理
                    If ImportExcelFile(fileName, strYyyymm, strYosanCode(i), strKubun(i), i) = True Then
                        'SyorichuForm.Close()
                        'Return
                        '予算書イベント別年月別財務実績情報を追加
                        If InsertYosanZaimuJisekiBy(_ExcelImportList, strKubun(i), i) = False Then
                            SyorichuForm.Close()
                            Return
                        End If
                    End If
                End If
            Next

            '処理中画面非表示
            SyorichuForm.Close()

            ComFunc.ShowInfoMsgBox("取込が完了しました。")

        End Sub
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
            ofd.FileName = ShisakuCommon.ShisakuGlobal.YosanListExcelImportFile
            ofd.Title = "取込対象のファイルを選択してください"

            ' 起動ディレクトリを指定します
            ofd.InitialDirectory = ShisakuCommon.ShisakuGlobal.YosanListExcelImportDir

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

#Region "Excel取込機能(シート選択)"
        ''' <summary>
        ''' シート選択
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SubSheetSelect(ByVal fileName As String) As String

            '初期設定
            SubSheetSelect = ""

            'ExcelSheet選択画面
            Try
                Using frmExcelSheetSelect As New FrmExcelSheetSelect()

                    'ADO.NET の機能を使ってシート名を取得
                    'ADO でシート名を取得した場合は、Excelで表示している順番ではなくソート順になる。
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
                        SubSheetSelect = ExcelImportSheetName
                    Else
                        MsgBox("EXCEL取込を中断します。")
                        SubSheetSelect = ""
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

#Region "Excel取込機能(Excelオープン)"
        ''' <summary>
        ''' Excel取込機能(Excel列チェック)
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <remarks></remarks>
        Private Function ImportExcelFileCheck(ByVal fileName As String) As Boolean

            If Not ShisakuComFunc.IsFileOpen(fileName) Then
                Using xls As New ShisakuExcel(fileName)
                    '選択したシート名をアクティブにする
                    xls.SetActiveSheet(ExcelImportSheetName)
                    '列項目エラーチェック
                    If ImportColCheck(xls) = False Then
                        'エラーメッセージ表示
                        Dim errMsg As String = "財務実績報告用のEXCELではありません。ご確認ください。"
                        ComFunc.ShowErrMsgBox(errMsg)
                        Return False
                    End If
                End Using
            End If

            Return True
        End Function
        ''' <summary>
        ''' Excel取込機能(Excel列情報取得)
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <remarks></remarks>
        Private Function ImportExcelFile(ByVal fileName As String, ByVal yyyyMm As String, _
                                         ByVal yosanCode As String, ByVal kubun As String, _
                                         ByVal jisekiKbn As String) As Boolean
            _ExcelImportList = New List(Of TYosanZaimuJisekiVo)

            If Not ShisakuComFunc.IsFileOpen(fileName) Then
                Using xls As New ShisakuExcel(fileName)
                    '選択したシート名をアクティブにする
                    xls.SetActiveSheet(ExcelImportSheetName)
                    '列項目エラーチェック
                    If ImportCol(_ExcelImportList, xls, yyyyMm, yosanCode, kubun, jisekiKbn) = False Then
                        ''エラーメッセージ表示
                        _ExcelImportList.Clear()
                        'Dim errMsg As String = "該当データがありません。ご確認ください。"
                        'ComFunc.ShowErrMsgBox(errMsg)
                        Return False
                    End If
                End Using
            End If

            Return True
        End Function
#End Region

#Region "Excel取込機能(Excel列チェック)"
        ''' <summary>
        ''' Excel取込機能(Excel列チェック)
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ImportColCheck(ByVal xls As ShisakuExcel) As Boolean
            'データ無しの場合
            Dim xlsEndRow As Integer = 0
            For i As Integer = xls.EndRow To EXCEL_IMPORT_DATA_START_ROW Step -1
                If StringUtil.IsNotEmpty(xls.GetValue(XLS_COL_YOSANCODE, i)) Then
                    xlsEndRow = i
                    Exit For
                End If
            Next
            If xlsEndRow = 0 Then Return False

            '列数不満の場合
            Dim xlsEndCol As Integer = xls.EndCol
            If xlsEndCol < EXCEL_IMPORT_DATA_COL_COUNT + EXCEL_IMPORT_DATA_START_COL - 1 Then
                Return False
            End If

            'ヘッダーラベルチェック
            '   原価工場
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_GENKA_COL, EXCEL_IMPORT_HEAD02_ROW), EXCEL_IMPORT_HEAD_GENKA_LBL01) Then
                Return False
            End If
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_GENKA_COL, EXCEL_IMPORT_HEAD03_ROW), EXCEL_IMPORT_HEAD_GENKA_LBL02) Then
                Return False
            End If
            '   工事区分
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_KOUJI_COL, EXCEL_IMPORT_HEAD02_ROW), EXCEL_IMPORT_HEAD_KOUJI_LBL01) Then
                Return False
            End If
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_KOUJI_COL, EXCEL_IMPORT_HEAD03_ROW), EXCEL_IMPORT_HEAD_KOUJI_LBL02) Then
                Return False
            End If
            '   予算コード
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_YOSAN_COL, EXCEL_IMPORT_HEAD02_ROW), EXCEL_IMPORT_HEAD_YOSAN_LBL01) Then
                Return False
            End If
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_YOSAN_COL, EXCEL_IMPORT_HEAD03_ROW), EXCEL_IMPORT_HEAD_YOSAN_LBL02) Then
                Return False
            End If
            '   直接工数
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_CHOKUKOU_COL, EXCEL_IMPORT_HEAD03_ROW), EXCEL_IMPORT_HEAD_CHOKUKOU_LBL) Then
                Return False
            End If
            '   技術工数
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_GIJYUTUKOU_COL, EXCEL_IMPORT_HEAD03_ROW), EXCEL_IMPORT_HEAD_GIJYUTUKOU_LBL) Then
                Return False
            End If
            '   材料費
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_ZAIRYOHI_COL, EXCEL_IMPORT_HEAD01_ROW).ToString.Replace("　", ""), EXCEL_IMPORT_HEAD_ZAIRYOHI_LBL) Then
                Return False
            End If
            '   直材費受入
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_CHOKUZAIUKE_COL, EXCEL_IMPORT_HEAD02_ROW), EXCEL_IMPORT_HEAD_CHOKUZAIUKE_LBL) Then
                Return False
            End If
            '   材料検収
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_ZAIRYOKEN_COL, EXCEL_IMPORT_HEAD04_ROW), EXCEL_IMPORT_HEAD_ZAIRYOKEN_LBL) Then
                Return False
            End If
            '   材料支給
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_ZAIRYOSHI_COL, EXCEL_IMPORT_HEAD04_ROW), EXCEL_IMPORT_HEAD_ZAIRYOSHI_LBL) Then
                Return False
            End If
            '   主材払出
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_SYUZAIHARAI_COL, EXCEL_IMPORT_HEAD04_ROW), EXCEL_IMPORT_HEAD_SYUZAIHARAI_LBL) Then
                Return False
            End If
            '   ＯＷ検収
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_OWKENSYU_COL, EXCEL_IMPORT_HEAD04_ROW), EXCEL_IMPORT_HEAD_OWKENSYU_LBL) Then
                Return False
            End If
            '   ＯＷ支給
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_OWSHIKYU_COL, EXCEL_IMPORT_HEAD04_ROW), EXCEL_IMPORT_HEAD_OWSHIKYU_LBL) Then
                Return False
            End If
            '   海外検収
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_KAIGAIKENSYU_COL, EXCEL_IMPORT_HEAD04_ROW), EXCEL_IMPORT_HEAD_KAIGAIKENSYU_LBL) Then
                Return False
            End If
            '   １原振伝
            If Not StringUtil.Equals(xls.GetValue(EXCEL_IMPORT_HEAD_1GENFURIDEN_COL, EXCEL_IMPORT_HEAD04_ROW), EXCEL_IMPORT_HEAD_1GENFURIDEN_LBL) Then
                Return False
            End If

            '必要な列のチェックを行う
            For RowIndex As Integer = EXCEL_IMPORT_DATA_START_ROW To xlsEndRow
                Dim Vo As New TYosanZaimuJisekiVo

                '予算コード列のチェック
                Dim code As String = Nothing
                If StringUtil.IsNotEmpty(xls.GetValue(XLS_COL_YOSANCODE, RowIndex)) Then
                    code = xls.GetValue(XLS_COL_YOSANCODE, RowIndex).ToString.Trim
                    'If Not ShisakuComFunc.IsInLength(code, 2) Then
                    '    Return False
                    'End If
                End If
                If StringUtil.IsEmpty(code) Then
                    Continue For
                End If

                '材料費合計列のチェック
                Dim zairyouHi As String = Nothing
                If StringUtil.IsNotEmpty(xls.GetValue(XLS_COL_ZAIRYOUHI, RowIndex)) Then
                    zairyouHi = xls.GetValue(XLS_COL_ZAIRYOUHI, RowIndex).ToString.Trim
                    If Not IsNumeric(zairyouHi) Then
                        Return False
                    End If
                    If Not ShisakuComFunc.IsInLength(zairyouHi, 13) Then
                        Return False
                    End If
                End If
                '特割費列のチェック
                Dim tokuwariHi As String = Nothing
                If StringUtil.IsNotEmpty(xls.GetValue(XLS_COL_TOKUWARIHI, RowIndex)) Then
                    tokuwariHi = xls.GetValue(XLS_COL_TOKUWARIHI, RowIndex).ToString.Trim
                    If Not IsNumeric(tokuwariHi) Then
                        Return False
                    End If
                    If Not ShisakuComFunc.IsInLength(tokuwariHi, 13) Then
                        Return False
                    End If
                End If

            Next

            Return True

        End Function
#End Region

#Region "Excel取込機能"
        ''' <summary>
        ''' Excel取込機能(Excel列チェック)
        ''' </summary>
        ''' <param name="xls"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ImportCol(ByVal aVoList As List(Of TYosanZaimuJisekiVo), ByVal xls As ShisakuExcel, _
                                        ByVal yyyyMm As String, ByVal yosanCode As String, ByVal kubun As String, _
                                        ByVal jisekiKbn As String) As Boolean
            'データ無しの場合
            Dim xlsEndRow As Integer = 0
            For i As Integer = xls.EndRow To EXCEL_IMPORT_DATA_START_ROW Step -1
                If StringUtil.IsNotEmpty(xls.GetValue(XLS_COL_YOSANCODE, i)) Then
                    xlsEndRow = i
                    Exit For
                End If
            Next
            If xlsEndRow = 0 Then Return False

            Dim code As String = Nothing
            Dim zairyouHi As Decimal = 0
            Dim tokuwariHi As Decimal = 0
            '必要な列のチェックを行う
            For RowIndex As Integer = EXCEL_IMPORT_DATA_START_ROW To xlsEndRow
                Dim Vo As New TYosanZaimuJisekiVo

                '予算コード列の存在チェック
                If StringUtil.IsEmpty(xls.GetValue(XLS_COL_YOSANCODE, RowIndex)) Then
                    Continue For
                End If
                '   予算コードが３桁以外の行がある。スルーする。
                code = xls.GetValue(XLS_COL_YOSANCODE, RowIndex).ToString.Trim
                If Not StringUtil.Equals(code.Length, 3) Then
                    Continue For
                End If

                '材料費合計列のチェック
                If StringUtil.IsNotEmpty(xls.GetValue(XLS_COL_ZAIRYOUHI, RowIndex)) Then
                    zairyouHi = xls.GetValue(XLS_COL_ZAIRYOUHI, RowIndex).ToString.Trim
                Else
                    zairyouHi = 0
                End If
                '特割費列のチェック
                If StringUtil.IsNotEmpty(xls.GetValue(XLS_COL_TOKUWARIHI, RowIndex)) Then
                    tokuwariHi = xls.GetValue(XLS_COL_TOKUWARIHI, RowIndex).ToString.Trim
                Else
                    tokuwariHi = 0
                End If

                '予算コードを「,」で区切
                Dim arrCode As String() = yosanCode.Split(",")
                For i As Integer = 0 To arrCode.Length - 1
                    If StringUtil.IsEmpty(arrCode(i)) Then
                        Continue For
                    End If
                    If String.Equals(code.Substring(2, 1), arrCode(i)) Then
                        'EXCELの予算コード列の3ケタ目が比例費予算コード、固定費予算コードにマッチの場合
                        If isExistYosanEvent(Left(code, 2)) Then
                            'EXCELの予算コードで「予算書イベント情報」（KEYは予算コード And ステータス="00"）にマッチの場合
                            Vo.YosanCode = Left(code, 2)    '頭２桁
                            Vo.YosanZaimuJisekiYyyyMm = yyyyMm
                            Vo.YosanZaimuHireiKoteiKbn = kubun
                            Vo.YosanZaimuJisekiKbn = jisekiKbn
                            Vo.YosanZaimuImportQty = zairyouHi + tokuwariHi
                            Vo.YosanZaimuInputQty = zairyouHi + tokuwariHi

                            aVoList.Add(Vo)
                        End If
                    End If
                Next
            Next

            'データ件数が０の場合FALSEを返す。
            If aVoList.Count = 0 Then
                Return False
            Else
                Return True
            End If

        End Function
#End Region

#Region "EXCELの予算コードで「予算書イベント情報」から取得出来かどうかの判断"
        ''' <summary>
        ''' EXCELの予算コードで「予算書イベント情報」から取得出来かどうかの判断
        ''' </summary>
        ''' <param name="yosanCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function isExistYosanEvent(ByVal yosanCode As String) As Boolean
            Dim eventVo As New TYosanEventVo
            eventVo.YosanCode = yosanCode
            eventVo.YosanStatus = STATUS_00
            Dim count As Integer = EventDao.CountBy(eventVo)

            If count > 0 Then
                Return True
            End If
            Return False
        End Function
#End Region

#Region "予算書イベント別年月別財務実績情報に同年月で取込済かチェック処理"

        ''' <summary>
        ''' 予算書イベント別年月別財務実績情報に同年月で取込済かチェック処理
        ''' </summary>
        ''' <param name="strYyyymm"></param>
        ''' <remarks></remarks>
        Private Function ImportCheck(ByVal strYyyymm As String) As Boolean

            '計上年月で取込済かチェックする。
            Dim zaimuJisekiDao As YosanEventListDao = New YosanEventListDaoImpl
            Dim zaimuJisekiList = zaimuJisekiDao.FindYosanZaimuJiseki(strYyyymm)

            If zaimuJisekiList.Count > 0 Then

                If ComFunc.ShowInfoMsgBox("計上年月[ " & strYyyymm & " ]は登録済です。" & vbLf & vbLf & "再取込しますか？", _
                                       MessageBoxButtons.YesNo) = DialogResult.No Then
                    MsgBox("財務報告実績の取込を中断しました。")
                    Return False
                Else
                    'はいを選択した場合、前回取込情報を削除する。
                    Dim jisekiDao As TYosanZaimuJisekiDao = New TYosanZaimuJisekiDaoImpl
                    Dim jisekiVo As New TYosanZaimuJisekiVo
                    jisekiVo.YosanZaimuJisekiYyyyMm = strYyyymm
                    jisekiDao.DeleteBy(jisekiVo)
                End If

            End If

            Return True

        End Function
#End Region

#Region "予算書イベント別年月別財務実績情報を追加"
        ''' <summary>
        ''' 予算書イベント別年月別財務実績情報を追加
        ''' </summary>
        ''' <param name="aVoList"></param>
        ''' <remarks></remarks>
        Private Function InsertYosanZaimuJisekiBy(ByVal aVoList As List(Of TYosanZaimuJisekiVo), _
                                                  ByVal kubun As String, _
                                                  ByVal jisekiKbn As String) As Boolean
            Dim jisekiDao As TYosanZaimuJisekiDao = New TYosanZaimuJisekiDaoImpl
            Dim resultVo As TYosanZaimuJisekiVo
            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    For Each Vo As TYosanZaimuJisekiVo In aVoList
                        'Dim resultVo As TYosanZaimuJisekiVo = jisekiDao.FindByPk(Vo.YosanCode, _
                        '                                                         Vo.YosanZaimuJisekiYyyyMm)
                        'If resultVo IsNot Nothing Then
                        '    '存在したら削除
                        '    jisekiDao.DeleteBy(resultVo)
                        'End If

                        Vo.CreatedUserId = aLoginInfo.UserId
                        Vo.CreatedDate = aSystemDate.CurrentDateDbFormat
                        Vo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                        Vo.UpdatedUserId = aLoginInfo.UserId
                        Vo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                        Vo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                        resultVo = jisekiDao.FindByPk(Vo.YosanCode, Vo.YosanZaimuJisekiYyyyMm, kubun, jisekiKbn)
                        If resultVo IsNot Nothing Then
                            '実績値を更新
                            Vo.YosanZaimuImportQty = Vo.YosanZaimuImportQty + resultVo.YosanZaimuImportQty
                            Vo.YosanZaimuInputQty = Vo.YosanZaimuInputQty + resultVo.YosanZaimuImportQty
                            '存在したら更新
                            jisekiDao.UpdateByPk(Vo)
                        Else
                            'データ追加
                            jisekiDao.InsertBy(Vo)
                        End If
                    Next
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("予算書イベント別年月別財務実績情報を追加出来ませんでした。")
                    Return False
                End Try

                db.Commit()
                Return True
            End Using
        End Function
#End Region

#End Region

#Region "編集ボタンを押したら処理"
        ''' <summary>
        ''' 排他処理
        ''' </summary>
        ''' <param name="eventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DoExclusiveEvent(ByVal eventCode As String) As Boolean
            '予算イベントコードで「排他管理予算書イベント情報」を検索する。
            Dim exclusiveVo As New TYosanExclusiveControlEventVo
            exclusiveVo.YosanEventCode = eventCode
            Dim exclusiveVos As List(Of TYosanExclusiveControlEventVo) = ExclusiveEventDao.FindBy(exclusiveVo)
            '「排他管理予算書イベント情報」より「編集者職番」を取得する。
            If exclusiveVos IsNot Nothing AndAlso exclusiveVos.Count > 0 Then
                '「社員マスター」を検索、「社員名」を取得する。
                Dim aRhac2130Dao As Rhac2130Dao = New Rhac2130DaoImpl
                Dim user2130Vo As Rhac2130Vo
                Dim userName As String = String.Empty

                If String.Equals(exclusiveVos(0).EditUserId, aLoginInfo.UserId) Then
                    '「編集者職番」が存在しユーザーＩＤと同じコードなら
                    user2130Vo = aRhac2130Dao.FindByPk(aLoginInfo.UserId)
                    If user2130Vo IsNot Nothing Then
                        userName = user2130Vo.ShainName
                    End If
                    ComFunc.ShowInfoMsgBox("選択したイベントは下記の方が編集中に異常終了したもようです。編集できます。" & vbLf & vbLf & _
                                           "担当者[" & exclusiveVos(0).EditUserId & ":" & userName & "]")
                    Return True
                Else
                    '「編集者職番」が存在しユーザーＩＤと違うコードなら
                    user2130Vo = aRhac2130Dao.FindByPk(exclusiveVos(0).EditUserId)
                    If user2130Vo IsNot Nothing Then
                        userName = user2130Vo.ShainName
                    End If
                    ComFunc.ShowInfoMsgBox("選択したイベントは編集中です。" & vbLf & vbLf & _
                                           "担当者[" & exclusiveVos(0).EditUserId & ":" & userName & "]")
                    Return False
                End If
            Else
                '排他管理予算書イベント情報作成
                If InsertExclusiveEvent(eventCode) = False Then
                    Return False
                End If

                Return True
            End If
        End Function

        ''' <summary>
        ''' 排他管理予算書イベント情報作成
        ''' </summary>
        ''' <param name="eventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function InsertExclusiveEvent(ByVal eventCode As String) As Boolean

            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    Dim exclusiveVo As New TYosanExclusiveControlEventVo
                    exclusiveVo.YosanEventCode = eventCode
                    exclusiveVo.EditUserId = aLoginInfo.UserId
                    exclusiveVo.EditDate = aSystemDate.CurrentDateDbFormat
                    exclusiveVo.EditTime = aSystemDate.CurrentTimeDbFormat
                    exclusiveVo.CreatedUserId = aLoginInfo.UserId
                    exclusiveVo.CreatedDate = aSystemDate.CurrentDateDbFormat
                    exclusiveVo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                    exclusiveVo.UpdatedUserId = aLoginInfo.UserId
                    exclusiveVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                    exclusiveVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat
                    ExclusiveEventDao.InsertBy(exclusiveVo)
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("排他管理予算書イベント情報を作成出来ませんでした。")
                    Return False
                End Try
                db.Commit()
            End Using

            Return True
        End Function
#End Region

#Region "削除ボタンを押したら処理"
        ''' <summary>
        ''' 予算書イベント情報削除処理
        ''' </summary>
        ''' <param name="eventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DeleteYosanEvent(ByVal eventCode As String) As Boolean
            '予算イベントコードがブランクの場合、処理を終了
            If StringUtil.IsEmpty(eventCode) Then
                Return False
            End If

            '該当予算書イベント情報を取得
            Dim eventVo As TYosanEventVo = EventDao.FindByPk(eventCode)
            '予算書イベント情報を取得できない場合、処理を終了
            If eventVo Is Nothing Then
                Return False
            End If

            'エラーチェックを行う
            If String.Equals(eventVo.YosanStatus, STATUS_00) Then
                ComFunc.ShowInfoMsgBox("状態が編集中です。削除できません。")
                'エラー箇所へカーソルを設定し、バックカラーは赤。
                With m_view.spdParts_Sheet1
                    For rowIndex As Integer = 0 To .RowCount - 1
                        If String.Equals(.GetValue(rowIndex, GetTagIdx(m_view.spdParts_Sheet1, TAG_YOSAN_EVENT_CODE)), eventCode) Then
                            .Rows(rowIndex).BackColor = Color.Red
                        End If
                    Next
                End With
                Return False
            End If

            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    '予算書イベント情報(T_YOSAN_EVENT)を削除
                    EventDao.DeleteByPk(eventCode)
                    '予算書部品情報(T_YOSAN_BUHIN_EDIT)を削除
                    Dim buhinDao As TYosanBuhinEditDao = New TYosanBuhinEditDaoImpl
                    Dim buhinVo As TYosanBuhinEditVo = New TYosanBuhinEditVo
                    buhinVo.YosanEventCode = eventCode
                    buhinDao.DeleteBy(buhinVo)
                    '予算書部品編集員数情報(T_YOSAN_BUHIN_EDIT_INSU)を削除
                    Dim buhinEditInsuDao As TYosanBuhinEditInsuDao = New TYosanBuhinEditInsuDaoImpl
                    Dim buhinEditInsuVo As TYosanBuhinEditInsuVo = New TYosanBuhinEditInsuVo
                    buhinEditInsuVo.YosanEventCode = eventCode
                    buhinEditInsuDao.DeleteBy(buhinEditInsuVo)
                    '予算書部品編集ﾊﾟﾀｰﾝ情報(T_YOSAN_BUHIN_EDIT_PATTERN)を削除
                    Dim buhinEditPatternDao As TYosanBuhinEditPatternDao = New TYosanBuhinEditPatternDaoImpl
                    Dim buhinEditPatternVo As TYosanBuhinEditPatternVo = New TYosanBuhinEditPatternVo
                    buhinEditPatternVo.YosanEventCode = eventCode
                    buhinEditPatternDao.DeleteBy(buhinEditPatternVo)
                    '予算書部品履歴情報(T_YOSAN_BUHIN_EDIT_RIREKI)を削除
                    Dim rirekiDao As TYosanBuhinEditRirekiDao = New TYosanBuhinEditRirekiDaoImpl
                    Dim rirekiVo As TYosanBuhinEditRirekiVo = New TYosanBuhinEditRirekiVo
                    rirekiVo.YosanEventCode = eventCode
                    rirekiDao.DeleteBy(rirekiVo)
                    '予算書部品編集員数履歴情報(T_YOSAN_BUHIN_EDIT_INSU_RIREKI)を削除
                    Dim buhinEditInsuRirekiDao As TYosanBuhinEditInsuRirekiDao = New TYosanBuhinEditInsuRirekiDaoImpl
                    Dim buhinEditInsuRirekiVo As TYosanBuhinEditInsuRirekiVo = New TYosanBuhinEditInsuRirekiVo
                    buhinEditInsuRirekiVo.YosanEventCode = eventCode
                    buhinEditInsuRirekiDao.DeleteBy(buhinEditInsuRirekiVo)
                    '予算書部品編集ﾊﾟﾀｰﾝ履歴情報(T_YOSAN_BUHIN_EDIT_PATTERN_RIREKI)を削除
                    Dim buhinEditPatternRirekiDao As TYosanBuhinEditPatternRirekiDao = New TYosanBuhinEditPatternRirekiDaoImpl
                    Dim buhinEditPatternRirekiVo As TYosanBuhinEditPatternRirekiVo = New TYosanBuhinEditPatternRirekiVo
                    buhinEditPatternRirekiVo.YosanEventCode = eventCode
                    buhinEditPatternRirekiDao.DeleteBy(buhinEditPatternRirekiVo)
                    '予算書イベント別製作台数情報(T_YOSAN_SEISAKU_DAISU)を削除
                    Dim seisakuDaisuDao As TYosanSeisakuDaisuDao = New TYosanSeisakuDaisuDaoImpl
                    Dim seisakuDaisuVo As TYosanSeisakuDaisuVo = New TYosanSeisakuDaisuVo
                    seisakuDaisuVo.YosanEventCode = eventCode
                    seisakuDaisuDao.DeleteBy(seisakuDaisuVo)
                    '予算書イベント別造り方情報(T_YOSAN_TUKURIKATA)を削除
                    Dim tukurikataDao As TYosanTukurikataDao = New TYosanTukurikataDaoImpl
                    Dim tukurikataVo As TYosanTukurikataVo = New TYosanTukurikataVo
                    tukurikataVo.YosanEventCode = eventCode
                    tukurikataDao.DeleteBy(tukurikataVo)
                    '予算書イベント別金材情報(T_YOSAN_KANAZAI)を削除
                    Dim kanazaiDao As TYosanKanazaiDao = New TYosanKanazaiDaoImpl
                    Dim kanazaiVo As TYosanKanazaiVo = New TYosanKanazaiVo
                    kanazaiVo.YosanEventCode = eventCode
                    kanazaiDao.DeleteBy(kanazaiVo)
                    ''予算書イベント別年月別財務実績情報(T_YOSAN_ZAIMU_JISEKI)を削除
                    'Dim zaimuJisekiDao As TYosanZaimuJisekiDao = New TYosanZaimuJisekiDaoImpl
                    'Dim zaimuJisekiVo As TYosanZaimuJisekiVo = New TYosanZaimuJisekiVo
                    'zaimuJisekiVo.YosanEventCode = eventCode
                    'zaimuJisekiDao.DeleteBy(zaimuJisekiVo)

                    '予算書２次追加テーブル
                    '   T_YOSAN_BUHIN_SELECT
                    Dim aYosanBuhinSelectDao As TYosanBuhinSelectDao = New TYosanBuhinSelectDaoImpl
                    Dim aYosanBuhinSelectVo As TYosanBuhinSelectVo = New TYosanBuhinSelectVo
                    aYosanBuhinSelectVo.YosanEventCode = eventCode
                    aYosanBuhinSelectDao.DeleteBy(aYosanBuhinSelectVo)

                Catch ex As Exception
                    db.Rollback()
                    MsgBox("予算書イベントを削除出来ませんでした。")
                    Return False
                End Try
                db.Commit()
            End Using

            Return True
        End Function
#End Region

#Region "完了ボタンを押したら処理"
        ''' <summary>
        ''' ステータスを更新
        ''' </summary>
        ''' <param name="eventCode">選択したイベント</param>
        ''' <param name="CompleteFlg">完了通常区分</param>
        ''' <remarks></remarks>
        Public Function UpdateYosanStatus(ByVal eventCode As String, ByVal CompleteFlg As Integer) As Boolean
            '行のイベント情報を取得
            Dim eventVo As TYosanEventVo = EventDao.FindByPk(eventCode)
            If eventVo Is Nothing Then
                Return False
            End If

            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    If CompleteFlg = 0 Then
                        '「ステータス」に"01"
                        eventVo.YosanStatus = "01"
                        '「イベント完了日」にデータ作成日（システム日付）
                        eventVo.YosanEventKanryoubi = ConvInt8Date(aSystemDate.CurrentDateDbFormat)
                    Else
                        '「ステータス」に"00"
                        eventVo.YosanStatus = "00"
                    End If
                    eventVo.UpdatedUserId = aLoginInfo.UserId
                    eventVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                    eventVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                    '行のイベント情報を更新
                    EventDao.UpdateByPk(eventVo)
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("予算書イベント情報から、ステータスを更新出来ませんでした。")
                    Return False
                End Try

                db.Commit()
            End Using

            Return True
        End Function
#End Region

#Region "開発符号を変更したら処理"

        ''' <summary>
        ''' イベントのコンボボックスを作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetEventCombo(ByVal kaihatsufugo As String)
            Try
                'イベントコンボボックスに値を追加
                Dim dtPhaseName As DataTable = GetEventPhaseNameData(kaihatsufugo)
                Dim vos As New List(Of LabelValueVo)
                For idx As Integer = 0 To dtPhaseName.Rows.Count - 1
                    Dim vo As New LabelValueVo
                    vo.Label = dtPhaseName.Rows(idx).Item(0)
                    vo.Value = vo.Label
                    vos.Add(vo)
                Next
                FormUtil.BindLabelValuesToComboBox(m_view.cmbEvent, vos, True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' E-BOM開発符号よりイベントコードを設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetEventPhaseNameData(ByVal kaihatsufugo As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(GetEventPhaseNameSql(kaihatsufugo), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 試作車系／開発符号マスタ情報からデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetEventPhaseNameSql(ByVal kaihatsufugo As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT DISTINCT SHISAKU_EVENT_PHASE_NAME ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.M_SHISAKU_KAIHATUFUGO ")
                .AppendLine("WHERE SHISAKU_KAIHATSU_FUGO = '" & kaihatsufugo & "' ")
                .AppendLine("ORDER BY SHISAKU_EVENT_PHASE_NAME")
            End With
            Return sql.ToString()
        End Function

#End Region

#Region "スプレッドにセルをダブルクリックしたら処理"
        ''' <summary>
        ''' 予算イベント情報の特定列を更新
        ''' </summary>
        ''' <param name="tag"></param>
        ''' <param name="key"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UpdateYosanEventInfo(ByVal tag As String, ByVal key As String, ByVal value As String) As Boolean
            '行のイベント情報を取得
            Dim eventVo As TYosanEventVo = EventDao.FindByPk(key)
            If eventVo Is Nothing Then
                Return False
            End If

            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    'イベント名称
                    If String.Equals(tag, TAG_YOSAN_EVENT_NAME) Then
                        eventVo.YosanEventName = value
                    End If
                    '予算コード
                    If String.Equals(tag, TAG_YOSAN_CODE) Then
                        eventVo.YosanCode = value
                    End If
                    '主な変更概要
                    If String.Equals(tag, TAG_YOSAN_MAIN_HENKO_GAIYO) Then
                        eventVo.YosanMainHenkoGaiyo = value
                    End If
                    '造り方及び製作条件
                    If String.Equals(tag, TAG_YOSAN_TSUKURIKATA_SEISAKUJYOKEN) Then
                        eventVo.YosanTsukurikataSeisakujyoken = value
                    End If
                    'その他
                    If String.Equals(tag, TAG_YOSAN_SONOTA) Then
                        eventVo.YosanSonota = value
                    End If
                    eventVo.UpdatedUserId = aLoginInfo.UserId
                    eventVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                    eventVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                    '行のイベント情報を更新
                    EventDao.UpdateByPk(eventVo)
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("予算書イベント情報から、" & tag & "を更新出来ませんでした。")
                    Return False
                End Try

                db.Commit()
            End Using

            Return True
        End Function
#End Region

#Region "数値8桁で格納された日付をYYYY/MM/DD形式に変換"
        ''' <summary>
        ''' 数値8桁で格納された日付をYYYY/MM/DD形式に変換
        ''' 
        '''  ※　8桁未満数値の場合はString.Emptyを返す
        '''  ※  8桁で変換不可の場合はThrow 
        ''' 
        ''' </summary>
        ''' <param name="aIntDate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConvDateInt8(ByVal aIntDate As Integer) As String
            Dim strDate As String = aIntDate.ToString.Trim

            '変換対象外判定(99999999はとりあえず除外)
            If strDate.Equals(String.Empty) OrElse strDate.Equals("0") OrElse strDate.Equals("99999999") OrElse strDate.Length <= 7 Then
                Return strDate
            End If

            strDate = String.Format("{0}/{1}/{2}", strDate.Substring(0, 4), strDate.Substring(4, 2), strDate.Substring(6, 2))

            If IsDate(strDate) = False Then
                Throw New Exception(String.Format("日付型に変換できない8桁数値が見つかりました。管理者に連絡してください。(対象数値:{0})", aIntDate.ToString))
            End If

            Return strDate
        End Function
#End Region

#Region "文字型(YYYY/MM/DD)をInt8桁に変換 "
        ''' <summary>
        ''' 文字型(YYYY/MM/DD)をInt8桁に変換
        ''' </summary>
        ''' <param name="aStrDate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConvInt8Date(ByVal aStrDate As String) As Integer
            Dim wDate As Date = Nothing
            Dim intDate As Integer = Nothing

            If IsDate(aStrDate) Then
                wDate = Date.Parse(aStrDate)
                intDate = wDate.ToString("yyyyMMdd")
            Else
                intDate = 0
            End If

            Return intDate
        End Function
#End Region

#Region " 列インデックス取得 "
        ''' <summary>
        ''' 列タグを元に列インデックスを取得します.
        ''' </summary>
        ''' <param name="sheet">対象シート</param>
        ''' <param name="tag">列タグ</param>
        ''' <returns>列インデックス</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTagIdx(ByVal sheet As Spread.SheetView, ByVal tag As String) As Integer
            Dim col As Spread.Column = sheet.Columns(tag)

            If col Is Nothing Then
                Return -1
            End If

            Return col.Index
        End Function
#End Region

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

#Region "小数の数値をチェック"
        ''' <summary>
        ''' 小数の数値をチェック
        ''' </summary>
        ''' <param name="strNum"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function NumericCheck(ByVal strNum As String) As Boolean
            If StringUtil.IsEmpty(strNum) Then
                Return True
            End If

            Dim chars As Char() = strNum.ToCharArray
            For Each c As Char In chars
                If (c < CChar("0") OrElse c > CChar("9")) AndAlso c <> CChar(".") Then
                    Return False
                End If
            Next
            Return True
        End Function
#End Region

    End Class

End Namespace
