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
Imports FarPoint.Win.Spread.CellType
Imports YosansyoTool.YosanshoEdit
Imports YosansyoTool.YosanshoEdit.Dao
Imports YosansyoTool.YosanshoEdit.Dao.Impl
Imports YosansyoTool.YosanEventListExcel.Dao
Imports YosansyoTool.YosanEventListExcel.Dao.Impl
Imports FarPoint.Win.Spread

Namespace YosanEventListExcel.Logic

    Public Class DispYosanEventListExcel

#Region " メンバー変数 "
        ''' <summary>ビュー</summary>
        Private m_view As FrmYosanEventListExcel

        ''' <summary>集計ビュー</summary>
        Private m_view_shukei As frmYosanEventExcelShorichu

        ''' <summary>Excel出力タイトル</summary>
        Private lstExcelTitle As New List(Of String)
        ''' <summary>Excel出力項目</summary>
        Private lstExcelItem As New List(Of String)

        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly aSystemDate As ShisakuDate
        Private ExclusiveEventDao As TYosanExclusiveControlEventDao
        Private EventDao As TYosanEventDao

        Private editDao As YosanshoEditDao

#End Region

#Region "定数"
        '''' <summary>ステータス</summary>
        Private Const STATUS_00 As String = "00"
        Private Const STATUS_01 As String = "01"
        Private Const STATUS_00_NAME As String = ""
        Private Const STATUS_01_NAME As String = "完了"

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

#Region " 各列TAG "
        ''' <summary>集計対象</summary>
        Public Const TAG_CHECK As String = "CHECK"
        ''' <summary>状態</summary>
        Private Const TAG_JYOUTAI As String = "JYOUTAI"
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
        ''' <summary>予算ステータス</summary>
        Private Const DB_YOSAN_STATUS As String = "YOSAN_STATUS"
#End Region

#Region "集計値EXCEL用"

        ''' <summary>比例費／固定費区分</summary>
        Public Const HIREI_KOTEI_KBN_HIREI As String = "H"
        Public Const HIREI_KOTEI_KBN_KOTEI As String = "K"
        ''' <summary>財務実績区分</summary>
        Public Const ZAIMU_JISEKI_KBN_HIREI_METAL As String = "0"
        Public Const ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU As String = "1"
        Public Const ZAIMU_JISEKI_KBN_HIREI_YUSOU As String = "2"
        Public Const ZAIMU_JISEKI_KBN_HIREI_SEISAN As String = "3"
        Public Const ZAIMU_JISEKI_KBN_HIREI_TRIM As String = "4"
        Public Const ZAIMU_JISEKI_KBN_KOTEI_TRIM As String = "5"
        Public Const ZAIMU_JISEKI_KBN_KOTEI_METAL As String = "6"

        ''' <summary>半期の月数</summary>
        Public Const HALF_MONTH_DEFAULT_COUNT As Integer = 6

        Private _YosanEventVo As TYosanEventVo
        Private exportDao As YosanshoExportDao

        '製作台数情報
        Private _KanseiDaisuList As List(Of TYosanSeisakuDaisuVo)
        Private _WbDaisuList As List(Of TYosanSeisakuDaisuVo)
        '金材情報
        Private _KanazaiList As List(Of TYosanKanazaiVo)
        '造り方情報
        Private _KanseiTukurikataList As List(Of TYosanTukurikataVo)
        Private _WbTukurikataList As List(Of TYosanTukurikataVo)

        Private _KanseiMetalInsertRowIndex As Integer = 0
        Private _KanseiTrimInsertRowIndex As Integer = 0
        Private _WbMetalInsertRowIndex As Integer = 0
        Private _WbTrimInsertRowIndex As Integer = 0

        'パターン名毎で部品費と型費
        Private _MitoshiMetalList As List(Of YosanInsuBuhinHiVo)
        Private _MitoshiTrimList As List(Of YosanInsuBuhinHiVo)

        '比例費メタルとトリム部品費
        Private _HireiMetalHiList As Dictionary(Of String, Decimal)
        Private _HireiTrimHiList As Dictionary(Of String, Decimal)
        '固定費メタルとトリム型費
        Private _KoteiMetalHiList As Dictionary(Of String, Decimal)
        Private _KoteiTrimHiList As Dictionary(Of String, Decimal)
        '財務実績
        Private _ZaimujisekiList As List(Of TYosanZaimuJisekiVo)
        '見通し
        Private _EventMitoshiList As List(Of TYosanEventMitoshiVo)
        Private _SeisanbuJisekiList As Dictionary(Of String, Decimal)
        '発注実績
        Private _HireiHatchuJisekiList As Dictionary(Of String, Dictionary(Of String, Decimal))
        Private _KoteiHatchuJisekiList As Dictionary(Of String, Dictionary(Of String, Decimal))

#Region "スプレッド初期化"
        ''' <summary>
        ''' スプレッドを初期化する      
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeShukeiSpread(ByVal fs As frmYosanEventExcelShorichu)

            '
            m_view_shukei = fs

            ''Spreadの初期化
            InitializeShukeiSpread(m_view_shukei.spdZaimuJiseki)
            InitializeShukeiSpread(m_view_shukei.spdSeisakuDaisu)
            InitializeShukeiSpread(m_view_shukei.spdTukurikataKanseisha)
            InitializeShukeiSpread(m_view_shukei.spdTukurikataWBsha)
            InitializeShukeiSpread(m_view_shukei.spdKanazai)
        End Sub

        Private Sub InitializeShukeiSpread(ByVal aSpread As FpSpread)
            ''セルカーソルの色と動き
            aSpread.FocusRenderer = New FarPoint.Win.Spread.MarqueeFocusIndicatorRenderer(Color.Blue, 1)

            '' XP風の描画
            aSpread.VisualStyles = FarPoint.Win.VisualStyles.On

            '' IME不可
            aSpread.ImeMode = Windows.Forms.ImeMode.Disable

            '' スクロールバーを移動中（ドラッグ中）にシートも移動する表示を行う
            aSpread.ScrollBarTrackPolicy = ScrollBarTrackPolicy.Both

            '' キーマップの変更
            SpreadUtil.BindKeyCtrlXIsCutDataOnly(aSpread)
            SpreadUtil.BindKeyCtrlVIsPasteDataOnly(aSpread)
            SpreadUtil.BindKeyF2IsEdit(aSpread)
            SpreadUtil.BindKeyF4IsComboShowList(aSpread)
            SpreadUtil.BindKeyEnterIsNextRow(aSpread)
            SpreadUtil.BindKeyShiftEnterIsPreviousRow(aSpread)

            SpreadUtil.BindKeyDeleteIsDataClear(aSpread)

            SpreadUtil.BindKeyCtrlUpArrowIsNextDataCell(aSpread)
            SpreadUtil.BindKeyCtrlDownArrowIsNextDataCell(aSpread)
            SpreadUtil.BindKeyCtrlLeftArrowIsNextDataCell(aSpread)
            SpreadUtil.BindKeyCtrlRightArrowIsNextDataCell(aSpread)
        End Sub
#End Region


#Region "試作種別"
        ''' <summary>試作種別_完成車</summary>
        Public Const SHISAKU_SYUBETU_KANSEI As String = " "
        ''' <summary>試作種別_ホワイトボディ</summary>
        Public Const SHISAKU_SYUBETU_WB As String = "W"
        ''' <summary>
        ''' 完成車かどうか
        ''' </summary>
        ''' <param name="shisakuSyubetu"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function IsKanseisha(ByVal shisakuSyubetu As String) As Boolean
            Return String.Equals(shisakuSyubetu, SHISAKU_SYUBETU_KANSEI)
        End Function

        ''' <summary>
        ''' ホワイトボディかどうか
        ''' </summary>
        ''' <param name="shisakuSyubetu"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function IsWb(ByVal shisakuSyubetu As String) As Boolean
            Return String.Equals(shisakuSyubetu, SHISAKU_SYUBETU_WB)
        End Function
#End Region

#Region "比例費固定費区分"
        ''' <summary>
        ''' 比例費かどうか
        ''' </summary>
        ''' <param name="hireiKoteiKbn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function IsHirei(ByVal hireiKoteiKbn As String) As Boolean
            Return String.Equals(hireiKoteiKbn, HIREI_KOTEI_KBN_HIREI)
        End Function

        ''' <summary>
        ''' 固定費かどうか
        ''' </summary>
        ''' <param name="hireiKoteiKbn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function IsKotei(ByVal hireiKoteiKbn As String) As Boolean
            Return String.Equals(hireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI)
        End Function
#End Region



#Region " 製作台数Spreadの定義 "

        ''' <summary>工事指令№ヘッダーの行番号</summary>
        Public Const SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX As Integer = 0
        ''' <summary>ユニット区分の行番号</summary>
        Public Const SPD_DAISU_UNITKBN_ROW_INDEX As Integer = 1
        ''' <summary>工事指令№データの行番号</summary>
        Public Const SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX As Integer = 2
        ''' <summary>完成車台数行の行番号</summary>
        Public Const SPD_DAISU_KANSEISHA_ROW_INDEX As Integer = 3
        ''' <summary>WB車台数行の行番号</summary>
        Public Const SPD_DAISU_WBSHA_ROW_INDEX As Integer = 4

        ''' <summary>追加ボタンの行番号</summary>
        Public Const SPD_DAISU_ADD_BUTTON_CELL_ROW_INDEX As Integer = 0
        ''' <summary>追加ボタンの列番号</summary>
        Public Const SPD_DAISU_ADD_BUTTON_CELL_COLUMN_INDEX As Integer = 1

        ''' <summary>台数合計列の列番号</summary>
        Public Const SPD_DAISU_DAISU_COUNT_COLUMN_INDEX As Integer = 1
        ''' <summary>工事指令№列開始の列番号</summary>
        Public Const SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN As Integer = 3

        ''' <summary>spread行のカウント</summary>
        Public Const SPD_DAISU_ROW_COUNT As Integer = 5
        ''' <summary>spread固定列のカウント</summary>
        Public Const SPD_DAISU_KOTEI_COLUMN_COUNT As Integer = 3

        ''' <summary>工事指令№列のカウント</summary>
        Public Const SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT As Integer = 4
#End Region

#Region " 金材Spreadの定義 "
        ''' <summary>spread固定列のカウント</summary>
        Public Const SPD_KANAZAI_KOTEI_COLUMN_COUNT As Integer = 5
        ''' <summary>spread固定行のカウント</summary>
        Public Const SPD_KANAZAI_KOTEI_ROW_COUNT As Integer = 3
        ''' <summary>spread行のカウント</summary>
        Public Const SPD_KANAZAI_DEFAULT_ROW_COUNT As Integer = 4
        ''' <summary>spread列のカウント</summary>
        Public Const SPD_KANAZAI_DEFAULT_COLUMN_COUNT As Integer = 7

        ''' <summary>金材項目名開始の行番号</summary>
        Public Const SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX As Integer = 3
        ''' <summary>月分布列開始の列番号</summary>
        Public Const SPD_KANAZAI_MONTH_COLUMNS_START_INDEX As Integer = 5

        ''' <summary>月挿入ボタンの列番号</summary>
        Public Const SPD_KANAZAI_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX As Integer = 4

        ''' <summary>金材追加ボタンの行番号</summary>
        Public Const SPD_KANAZAI_ADD_BUTTON_CELL_ROW_INDEX As Integer = 0
        ''' <summary>金材追加ボタンの列番号</summary>
        Public Const SPD_KANAZAI_ADD_BUTTON_CELL_COLUMN_INDEX As Integer = 1

        ''' <summary>金材項目名の列番号</summary>
        Public Const SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX As Integer = 0
        ''' <summary>台数の列番号</summary>
        Public Const SPD_KANAZAI_DAISU_COLUMN_INDEX As Integer = 1
        ''' <summary>単価の列番号</summary>
        Public Const SPD_KANAZAI_TANKA_COLUMN_INDEX As Integer = 3
        ''' <summary>計の列番号</summary>
        Public Const SPD_KANAZAI_KEI_COLUMN_INDEX As Integer = 4

        ''' <summary>月分布ヘッダーの行番号</summary>
        Public Const SPD_KANAZAI_MONTH_TITLE_ROW_INDEX As Integer = 0
        ''' <summary>年の行番号</summary>
        Public Const SPD_KANAZAI_YEAR_ROW_INDEX As Integer = 1
        ''' <summary>月の行番号</summary>
        Public Const SPD_KANAZAI_MONTH_ROW_INDEX As Integer = 2
#End Region

#Region " 造り方Spreadの定義"
        ''' <summary>spread固定列のカウント</summary>
        Public Const SPD_TUKURIKATA_KOTEI_COLUMN_COUNT As Integer = 7
        ''' <summary>spread固定行のカウント</summary>
        Public Const SPD_TUKURIKATA_KOTEI_ROW_COUNT As Integer = 3
        ''' <summary>spread行のカウント</summary>
        Public Const SPD_TUKURIKATA_DEFAULT_ROW_COUNT As Integer = 4
        ''' <summary>spread列のカウント</summary>
        Public Const SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT As Integer = 9

        ''' <summary>パターン名開始の行番号</summary>
        Public Const SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX As Integer = 3
        ''' <summary>月分布列開始の列番号</summary>
        Public Const SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX As Integer = 7

        ''' <summary>月挿入ボタンの列番号</summary>
        Public Const SPD_TUKURIKATA_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX As Integer = 5

        ''' <summary>造り方追加ボタンの行番号</summary>
        Public Const SPD_TUKURIKATA_ADD_BUTTON_CELL_ROW_INDEX As Integer = 0
        ''' <summary>造り方追加ボタンの列番号</summary>
        Public Const SPD_TUKURIKATA_ADD_BUTTON_CELL_COLUMN_INDEX As Integer = 4

        ''' <summary>ユニット区分の列番号</summary>
        Public Const SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX As Integer = 0
        ''' <summary>部品表名の列番号</summary>
        Public Const SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX As Integer = 1
        ''' <summary>パターン名の列番号</summary>
        Public Const SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX As Integer = 2
        ''' <summary>部品費（円）列番号</summary>
        Public Const SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX As Integer = 3
        ''' <summary>型費（円）列番号</summary>
        Public Const SPD_TUKURIKATA_KATAHI_COLUMN_INDEX As Integer = 4
        ''' <summary>台数の列番号</summary>
        Public Const SPD_TUKURIKATA_DAISU_COLUMN_INDEX As Integer = 5

        ''' <summary>月分布ヘッダーの行番号</summary>
        Public Const SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX As Integer = 0
        ''' <summary>年の行番号</summary>
        Public Const SPD_TUKURIKATA_YEAR_ROW_INDEX As Integer = 1
        ''' <summary>月の行番号</summary>
        Public Const SPD_TUKURIKATA_MONTH_ROW_INDEX As Integer = 2
#End Region

#Region " 集計値Spreadの定義"
        ''' <summary>spread固定列のカウント</summary>
        Public Const SPD_ZAIMUJISEKI_KOTEI_COLUMN_COUNT As Integer = 3
        ''' <summary>spread行のカウント</summary>
        Public Const SPD_ZAIMUJISEKI_DEFAULT_ROW_COUNT As Integer = 27
        ''' <summary>spread列のカウント</summary>
        Public Const SPD_ZAIMUJISEKI_DEFAULT_COLUMN_COUNT As Integer = 4

        ''' <summary>比例費開始の行番号</summary>
        Public Const SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX As Integer = 3
        Public Const SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX As Integer = 4
        Public Const SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX As Integer = 5
        Public Const SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX As Integer = 6
        Public Const SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX As Integer = 7
        Public Const SPD_ZAIMUJISEKI_HIREI_MITOSHI_KEI_ROWS_INDEX As Integer = 8
        Public Const SPD_ZAIMUJISEKI_HIREI_HATCHU_METAL_ROWS_INDEX As Integer = 9
        Public Const SPD_ZAIMUJISEKI_HIREI_HATCHU_TRIM_ROWS_INDEX As Integer = 10
        Public Const SPD_ZAIMUJISEKI_HIREI_HATCHU_KEI_ROWS_INDEX As Integer = 11
        Public Const SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX As Integer = 12
        Public Const SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX As Integer = 13
        Public Const SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX As Integer = 14
        Public Const SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX As Integer = 15
        Public Const SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX As Integer = 16
        Public Const SPD_ZAIMUJISEKI_HIREI_ZAIMU_KEI_ROWS_INDEX As Integer = 17
        ''' <summary>固定費開始の行番号</summary>
        Public Const SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX As Integer = 18
        Public Const SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX As Integer = 19
        Public Const SPD_ZAIMUJISEKI_KOTEI_MITOSHI_KEI_ROWS_INDEX As Integer = 20
        Public Const SPD_ZAIMUJISEKI_KOTEI_HATCHU_METAL_ROWS_INDEX As Integer = 21
        Public Const SPD_ZAIMUJISEKI_KOTEI_HATCHU_TRIM_ROWS_INDEX As Integer = 22
        Public Const SPD_ZAIMUJISEKI_KOTEI_HATCHU_KEI_ROWS_INDEX As Integer = 23
        Public Const SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX As Integer = 24
        Public Const SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX As Integer = 25
        Public Const SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX As Integer = 26
        ''' <summary>月分布列開始の列番号</summary>
        Public Const SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX As Integer = 3

        ''' <summary>再計算ボタンの行番号</summary>
        Public Const SPD_ZAIMUJISEKI_CALC_BUTTON_CELL_ROW_INDEX As Integer = 1
        ''' <summary>再計算ボタンの列番号</summary>
        Public Const SPD_ZAIMUJISEKI_CALC_BUTTON_CELL_COLUMN_INDEX As Integer = 2

        ''' <summary>月分布ヘッダーの行番号</summary>
        Public Const SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX As Integer = 0
        ''' <summary>年の行番号</summary>
        Public Const SPD_ZAIMUJISEKI_YEAR_ROW_INDEX As Integer = 1
        ''' <summary>月の行番号</summary>
        Public Const SPD_ZAIMUJISEKI_MONTH_ROW_INDEX As Integer = 2
#End Region

#Region "年月⇔年期の変換"
        ''' <summary>
        ''' YYYYMM→YYYYX期
        ''' </summary>
        ''' <param name="yearMonth"></param>
        ''' <param name="ks"></param>
        ''' <remarks></remarks>
        Private Function ChangeDBYearToSpreadYear(ByVal yearMonth As String, ByVal ks As String) As String
            Dim result As String = Nothing

            If String.Equals(ks, UP_KS) Then
                '上期の場合
                result = yearMonth.Substring(0, 4) & ks
            Else
                '下期の場合
                Dim month As Integer = CInt(yearMonth.Substring(4, 2))
                If month >= 10 Then
                    result = yearMonth.Substring(0, 4) & ks
                Else
                    result = (CInt(yearMonth.Substring(0, 4)) - 1).ToString & ks
                End If
            End If

            Return result
        End Function
        ''' <summary>
        ''' YYYYX期→YYYYMM
        ''' </summary>
        ''' <param name="year"></param>
        ''' <param name="ks"></param>
        ''' <param name="month"></param>
        ''' <remarks></remarks>
        Private Function ChangeSpreadYearToDBYear(ByVal year As String, ByVal ks As String, ByVal month As String) As String
            Dim result As String = Nothing

            If String.Equals(ks, UP_KS) Then
                '上期の場合
                result = year & month
            Else
                '下期の場合
                If month >= 10 Then
                    result = year & month
                Else
                    result = (CInt(year) + 1).ToString & month
                End If
            End If

            Return result
        End Function
        ''' <summary>
        ''' 期間内の各月の取得
        ''' </summary>
        ''' <param name="year"></param>
        ''' <param name="ks"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetDBYearMonth(ByVal year As String, ByVal ks As String) As List(Of String)
            Dim result As New List(Of String)

            If String.Equals(ks, UP_KS) Then
                '上期の場合
                For index As Integer = 4 To 9
                    result.Add(year & index.ToString("00"))
                Next
            Else
                '下期の場合
                For index As Integer = 10 To 12
                    result.Add(year & index.ToString("00"))
                Next
                For index As Integer = 1 To 3
                    result.Add((CInt(year) + 1).ToString & index.ToString("00"))
                Next
            End If

            Return result
        End Function

        ''' <summary>
        ''' 年期取得
        ''' </summary>
        ''' <param name="year"></param>
        ''' <param name="month"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ChangeYearMonthToYearKs(ByVal year As String, ByVal month As String) As String
            Dim ks As String
            If CInt(month) >= 4 AndAlso CInt(month) <= 9 Then
                ks = UP_KS
            Else
                ks = DOWN_KS
                If CInt(month) <= 3 Then
                    year = year - 1
                End If
            End If
            'YYYYX期
            Return year & ks
        End Function
#End Region

#Region "ユニット区分"
        ''' <summary>ユニット区分_トリム</summary>
        Public Const UNIT_KBN_TRIM_CODE As String = "T"
        Public Const UNIT_KBN_TRIM_NAME As String = "ﾄﾘﾑ"
        ''' <summary>ユニット区分_メタル</summary>
        Public Const UNIT_KBN_METAL_CODE As String = "M"
        Public Const UNIT_KBN_METAL_NAME As String = "ﾒﾀﾙ"

        ''' <summary>
        ''' ユニット名取得
        ''' </summary>
        ''' <param name="unitKbn"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private ReadOnly Property UnitKbnName(ByVal unitKbn As String) As String
            Get
                If String.Equals(unitKbn, UNIT_KBN_METAL_CODE) Then
                    Return UNIT_KBN_METAL_NAME
                Else
                    Return UNIT_KBN_TRIM_NAME
                End If
            End Get
        End Property

        ''' <summary>
        ''' ユニットコード取得
        ''' </summary>
        ''' <param name="unitKbn"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private ReadOnly Property UnitKbnCode(ByVal unitKbn As String) As String
            Get
                If String.Equals(unitKbn, UNIT_KBN_METAL_NAME) Then
                    Return UNIT_KBN_METAL_CODE
                Else
                    Return UNIT_KBN_TRIM_CODE
                End If
            End Get
        End Property

        ''' <summary>
        ''' メタルかどうか
        ''' </summary>
        ''' <param name="unitKbn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function IsUnitMetal(ByVal unitKbn As String) As Boolean
            Return String.Equals(unitKbn, UNIT_KBN_METAL_CODE)
        End Function

        ''' <summary>
        ''' トリムかどうか
        ''' </summary>
        ''' <param name="unitKbn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function IsUnitTrim(ByVal unitKbn As String) As Boolean
            Return String.Equals(unitKbn, UNIT_KBN_TRIM_CODE)
        End Function
#End Region

#Region "CellType"
        Private KojishireiCellType As TextCellType
        ''' <summary>
        ''' 工事指令№セルを返す
        ''' </summary>
        ''' <returns>工事指令№セル</returns>
        ''' <remarks></remarks>
        Public Function GetKojishireiCellType() As TextCellType
            KojishireiCellType = ShisakuSpreadUtil.NewGeneralTextCellType
            KojishireiCellType.MaxLength = 3
            KojishireiCellType.CharacterSet = CharacterSet.AlphaNumeric
            Return KojishireiCellType
        End Function

        Private TaisuCellType As NumberCellType
        ''' <summary>
        ''' 台数セルを返す
        ''' </summary>
        ''' <returns>台数セル</returns>
        ''' <remarks></remarks>
        Public Function GetDaisuCellType() As NumberCellType
            TaisuCellType = ShisakuSpreadUtil.NewGeneralNumberCellType
            TaisuCellType.MinimumValue = 0
            TaisuCellType.MaximumValue = 9999
            TaisuCellType.DecimalPlaces = 0
            Return TaisuCellType
        End Function

        Private TankaCellType As NumberCellType
        ''' <summary>
        ''' 単価セルを返す
        ''' </summary>
        ''' <returns>単価セル</returns>
        ''' <remarks></remarks>
        Public Function GetTankaCellType() As NumberCellType
            TankaCellType = ShisakuSpreadUtil.NewGeneralNumberCellType
            TankaCellType.MinimumValue = 0
            TankaCellType.MaximumValue = 999999999.99
            Return TankaCellType
        End Function

        Private Kingaku11CellType As NumberCellType
        ''' <summary>
        ''' 金額セルを返す
        ''' </summary>
        ''' <returns>金額セル</returns>
        ''' <remarks></remarks>
        Public Function GetKingaku11CellType() As NumberCellType
            Kingaku11CellType = ShisakuSpreadUtil.NewGeneralNumberCellType
            Kingaku11CellType.MinimumValue = 0
            Kingaku11CellType.MaximumValue = 99999999999
            Kingaku11CellType.DecimalPlaces = 0
            Return Kingaku11CellType
        End Function

        Private Kingaku10CellType As NumberCellType
        ''' <summary>
        ''' 金額セルを返す
        ''' </summary>
        ''' <returns>金額セル</returns>
        ''' <remarks></remarks>
        Public Function GetKingaku10CellType() As NumberCellType
            Kingaku10CellType = ShisakuSpreadUtil.NewGeneralNumberCellType
            Kingaku10CellType.MinimumValue = 0
            Kingaku10CellType.MaximumValue = 9999999999
            Kingaku10CellType.DecimalPlaces = 0
            Return Kingaku10CellType
        End Function

        Private KingakuMinusCellType As NumberCellType
        ''' <summary>
        ''' 金額セルを返す
        ''' </summary>
        ''' <returns>金額セル</returns>
        ''' <remarks></remarks>
        Public Function GetKingakuMinusCellType() As NumberCellType
            KingakuMinusCellType = ShisakuSpreadUtil.NewGeneralNumberCellType
            KingakuMinusCellType.MinimumValue = -9999999999
            KingakuMinusCellType.MaximumValue = 9999999999
            KingakuMinusCellType.DecimalPlaces = 0
            Return KingakuMinusCellType
        End Function

        Private BiggerMonthCellType As ButtonCellType
        ''' <summary>
        ''' 拡大▶▶ボタンセルを返す
        ''' </summary>
        ''' <returns>拡大▶▶ボタンセル</returns>
        ''' <remarks></remarks>
        Public Function GetBiggerMonthCellType() As ButtonCellType
            BiggerMonthCellType = New ButtonCellType
            BiggerMonthCellType.Text = "拡大▶▶"
            Return BiggerMonthCellType
        End Function

        Private SmallerMonthCellType As ButtonCellType
        ''' <summary>
        ''' ◀◀縮小ボタンセルを返す
        ''' </summary>
        ''' <returns>◀◀縮小セル</returns>
        ''' <remarks></remarks>
        Public Function GetSmallerMonthCellType() As ButtonCellType
            SmallerMonthCellType = New ButtonCellType
            SmallerMonthCellType.Text = "◀◀縮小"
            Return SmallerMonthCellType
        End Function
#End Region

#Region "月分布について"
        ''' <summary>
        ''' SPREADで列の配置を設定する。
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <param name="columnIndex"></param>
        ''' <param name="startRow"></param>
        ''' <param name="startCol"></param>
        ''' <param name="yearRow"></param>
        ''' <param name="monthRow"></param>
        ''' <remarks></remarks>
        Private Sub SetMonthTitlePro(ByVal sheet As SheetView, ByVal columnIndex As Integer, ByVal startRow As Integer, _
                                     ByVal startCol As Integer, ByVal yearRow As Integer, ByVal monthRow As Integer)
            '列幅をセット
            If (columnIndex - startCol + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) > 0 Then
                sheet.Columns(columnIndex).Width = 50
            Else
                sheet.Columns(columnIndex).Width = 60
            End If
            '年セル
            sheet.Cells(yearRow, columnIndex).Locked = True
            sheet.Cells(yearRow, columnIndex).BackColor = Color.Khaki
            sheet.Cells(yearRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            sheet.Cells(yearRow, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            '拡大、縮小ボタンを水平方向の配置
            If (columnIndex - startCol + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                sheet.Cells(yearRow, columnIndex - 1).HorizontalAlignment = CellHorizontalAlignment.Center
                sheet.Cells(yearRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Center
            End If
            '月セル
            sheet.Cells(monthRow, columnIndex).Locked = True
            sheet.Cells(monthRow, columnIndex).BackColor = Color.Khaki
            sheet.Cells(monthRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            sheet.Cells(monthRow, columnIndex).VerticalAlignment = CellVerticalAlignment.Center

            sheet.Cells(sheet.RowCount - 1, columnIndex).Locked = True
            sheet.Cells(sheet.RowCount - 1, columnIndex).BackColor = Color.White
        End Sub

        ''' <summary>
        ''' SPREADで列の配置を設定する。
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <param name="rowIndex"></param>
        ''' <param name="columnIndex"></param>
        ''' <param name="locked"></param>
        ''' <remarks></remarks>
        Private Sub SetMonthCellPro(ByVal sheet As SheetView, ByVal rowIndex As Integer, ByVal columnIndex As Integer, ByVal locked As Boolean)
            '台数セル
            sheet.Cells(rowIndex, columnIndex).BackColor = Color.White
            sheet.Cells(rowIndex, columnIndex).Locked = locked
            If Not locked Then
                sheet.Cells(rowIndex, columnIndex).CellType = GetDaisuCellType()
            End If
            sheet.Cells(rowIndex, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Right
            sheet.Cells(rowIndex, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
        End Sub

        ''' <summary>
        ''' 月合計列追加
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub AddSpdMonthCountColumn(ByVal sheet As SheetView, ByVal columnIndex As Integer, ByVal startRow As Integer, _
                                           ByVal startCol As Integer, ByVal yearRow As Integer, ByVal monthRow As Integer)
            Dim styleinfo As New Spread.StyleInfo
            styleinfo.Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
            styleinfo.BackColor = Color.Khaki
            styleinfo.VerticalAlignment = CellVerticalAlignment.Center
            styleinfo.Locked = True
            '年
            sheet.Cells(yearRow, columnIndex - HALF_MONTH_DEFAULT_COUNT).ColumnSpan = HALF_MONTH_DEFAULT_COUNT - 1
            '該当セルの文字太を変更する。
            sheet.SetStyleInfo(yearRow, columnIndex - HALF_MONTH_DEFAULT_COUNT, styleinfo)

            '◀◀縮小
            sheet.Cells(yearRow, columnIndex - 1).CellType = GetSmallerMonthCellType()
            sheet.Cells(yearRow, columnIndex - 1).HorizontalAlignment = CellHorizontalAlignment.Center

            sheet.AddColumns(columnIndex, 1)
            SetMonthTitlePro(sheet, columnIndex, startRow, startCol, yearRow, monthRow)

            '拡大▶▶
            sheet.Cells(yearRow, columnIndex).CellType = GetBiggerMonthCellType()
            sheet.Cells(yearRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Center
            '月
            sheet.Cells(monthRow, columnIndex).Value = sheet.GetValue(yearRow, columnIndex - HALF_MONTH_DEFAULT_COUNT)
            '該当セルの文字太を変更する。
            sheet.SetStyleInfo(monthRow, columnIndex, styleinfo)

            '台数セル
            For rowIndex As Integer = startRow To sheet.RowCount - 2
                SetMonthCellPro(sheet, rowIndex, columnIndex, True)
            Next
        End Sub

        ''' <summary>
        ''' システム日付の直近の年期(月分布列)を拡大表示
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub VisibleSystemYearMonthColumns()
            '集計値SPREAD
            VisibleSystemYearMonthColumns(m_view_shukei.spdZaimuJiseki_Sheet1, SPD_ZAIMUJISEKI_DEFAULT_COLUMN_COUNT, _
                                          SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_KOTEI_COLUMN_COUNT, _
                                          SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
            '金材SPREAD
            VisibleSystemYearMonthColumns(m_view_shukei.spdKanazai_Sheet1, SPD_KANAZAI_DEFAULT_COLUMN_COUNT, _
                                          SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_KOTEI_COLUMN_COUNT, _
                                          SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
            '造り方完成車SPREAD
            VisibleSystemYearMonthColumns(m_view_shukei.spdTukurikataKanseisha_Sheet1, SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT, _
                                          SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, SPD_TUKURIKATA_KOTEI_COLUMN_COUNT, _
                                          SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, SPD_TUKURIKATA_MONTH_ROW_INDEX)
            '造り方ﾎﾜｲﾄﾎﾞﾃﾞｨSPREAD
            VisibleSystemYearMonthColumns(m_view_shukei.spdTukurikataWBsha_Sheet1, SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT, _
                                          SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, SPD_TUKURIKATA_KOTEI_COLUMN_COUNT, _
                                          SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, SPD_TUKURIKATA_MONTH_ROW_INDEX)
        End Sub

        ''' <summary>
        ''' システム日付の直近の年期(月分布列)を拡大表示
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <param name="DefaultColCount"></param>
        ''' <param name="startCol"></param>
        ''' <param name="koteiColCount"></param>
        ''' <param name="titleRow"></param>
        ''' <param name="monthRow"></param>
        ''' <remarks></remarks>
        Private Sub VisibleSystemYearMonthColumns(ByVal sheet As SheetView, ByVal defaultColCount As Integer, ByVal startCol As Integer, _
                                                  ByVal koteiColCount As Integer, ByVal titleRow As Integer, _
                                                  ByVal monthRow As Integer)
            If sheet.ColumnCount = defaultColCount Then
                Return
            End If

            SetUnVisibleMonthColumns(sheet, startCol, titleRow, koteiColCount)

            Dim sysYear As String = aSystemDate.CurrentDateDbFormat.Replace("-", "").Substring(0, 4)
            Dim sysMonth As String = aSystemDate.CurrentDateDbFormat.Replace("-", "").Substring(4, 2)
            Dim sysKs As String = ChangeYearMonthToYearKs(sysYear, sysMonth)

            Dim endCol As Integer = 0
            Dim hasSystemKs As Boolean = False
            'システム日付が存在の場合
            For columnIndex As Integer = startCol To sheet.ColumnCount - 2
                '合計列
                If (columnIndex - startCol + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    '同期の場合
                    If String.Equals(sheet.GetValue(monthRow, columnIndex), sysKs) Then
                        endCol = columnIndex
                        VisibleMonthColumns(sheet, startCol, titleRow, endCol, koteiColCount)

                        hasSystemKs = True
                        Exit For
                    End If
                End If
            Next

            'システム日付が存在しないの場合
            If Not hasSystemKs Then
                If sheet.ColumnCount - koteiColCount = HALF_MONTH_DEFAULT_COUNT + 2 Then
                    '一つ期のみ
                    endCol = startCol + HALF_MONTH_DEFAULT_COUNT
                    VisibleMonthColumns(sheet, startCol, titleRow, endCol, koteiColCount)
                Else
                    '「最も近い期」を拡大表示
                    endCol = GetSpreadNearestColumnIndex(sheet, monthRow, startCol, koteiColCount, sysKs)
                    VisibleMonthColumns(sheet, startCol, titleRow, endCol, koteiColCount)
                End If
            End If

            '先頭の年月
            If endCol - HALF_MONTH_DEFAULT_COUNT = startCol Then
                '月分布ヘッダー
                sheet.Cells(titleRow, endCol).ColumnSpan = 1
                sheet.Cells(titleRow, startCol).ColumnSpan = sheet.ColumnCount - koteiColCount - 1
                sheet.Cells(titleRow, startCol).Locked = True
                sheet.Cells(titleRow, startCol).BackColor = Color.Khaki
                sheet.Cells(titleRow, startCol).Value = "月分布"
            End If
        End Sub

        ''' <summary>
        ''' 「最も近い期」の列Indexを取得
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <param name="monthRow"></param>
        ''' <param name="startCol"></param>
        ''' <param name="koteiColCount"></param>
        ''' <param name="yearKs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSpreadNearestColumnIndex(ByVal sheet As SheetView, ByVal monthRow As Integer, ByVal startCol As Integer, _
                                                     ByVal koteiColCount As Integer, ByVal yearKs As String) As Integer
            Dim sysYear As String = yearKs.Substring(0, 4)
            Dim sysKs As String = yearKs.Substring(4)

            Dim intLength As Integer = 9999
            Dim resultIndex As Integer = startCol + HALF_MONTH_DEFAULT_COUNT
            For columnIndex As Integer = startCol To sheet.ColumnCount - 2
                '合計列の場合
                If (columnIndex - startCol + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    'システム日付以外の場合
                    If Not String.Equals(yearKs, sheet.GetValue(monthRow, columnIndex)) Then
                        Dim year As String = sheet.GetValue(monthRow, columnIndex).ToString.Substring(0, 4)
                        Dim ks As String = sheet.GetValue(monthRow, columnIndex).ToString.Substring(4)
                        If String.Equals(year, sysYear) Then
                            Return columnIndex
                        End If
                        Dim tempLength As Integer
                        If String.Equals(ks, sysKs) Then
                            tempLength = (CInt(year) - CInt(sysYear)) * 2
                        Else
                            tempLength = (CInt(year) - CInt(sysYear)) * 2 - 1
                        End If
                        '最も近い期を取得
                        If Math.Abs(tempLength) < Math.Abs(intLength) Then
                            intLength = tempLength
                            resultIndex = columnIndex
                        Else
                            Return resultIndex
                        End If
                    End If
                End If
            Next

            Return resultIndex
        End Function

        ''' <summary>
        ''' 全て月分布列を縮小表示
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <remarks></remarks>
        Private Sub SetUnVisibleMonthColumns(ByVal sheet As SheetView, ByVal startCol As Integer, _
                                             ByVal titleRow As Integer, ByVal koteiColCount As Integer)
            For columnIndex As Integer = startCol To sheet.ColumnCount - 2
                '合計列以外
                If Not ((columnIndex - startCol + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                    '非表示
                    sheet.Columns(columnIndex).Visible = False
                Else
                    sheet.Columns(columnIndex).Visible = True
                End If
            Next
            '月分布ヘッダー
            sheet.Cells(titleRow, startCol + HALF_MONTH_DEFAULT_COUNT).ColumnSpan = sheet.ColumnCount - koteiColCount - HALF_MONTH_DEFAULT_COUNT - 1
            sheet.Cells(titleRow, startCol + HALF_MONTH_DEFAULT_COUNT).Locked = True
            sheet.Cells(titleRow, startCol + HALF_MONTH_DEFAULT_COUNT).BackColor = Color.Khaki
            sheet.Cells(titleRow, startCol + HALF_MONTH_DEFAULT_COUNT).Value = "月分布"
            sheet.Cells(titleRow, startCol + HALF_MONTH_DEFAULT_COUNT).VerticalAlignment = CellVerticalAlignment.Center
        End Sub

        ''' <summary>
        ''' 拡大▶▶
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <remarks></remarks>
        Public Sub VisibleMonthColumns(ByVal sheet As SheetView, ByVal startCol As Integer, _
                                       ByVal titleRow As Integer, ByVal columnIndex As Integer, ByVal koteiColCount As Integer)
            SetUnVisibleMonthColumns(sheet, startCol, titleRow, koteiColCount)

            sheet.Columns(columnIndex).Visible = False
            For index As Integer = 0 To HALF_MONTH_DEFAULT_COUNT - 1
                sheet.Columns(columnIndex - HALF_MONTH_DEFAULT_COUNT + index).Visible = True
            Next

            '拡大表示している部分の両端を太線で強調
            sheet.Columns(columnIndex - HALF_MONTH_DEFAULT_COUNT).Border = New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.MediumLine), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None))
            sheet.Columns(columnIndex - 1).Border = New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.MediumLine), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None))

            '先頭の年月
            If columnIndex - HALF_MONTH_DEFAULT_COUNT = startCol Then
                '月分布ヘッダー
                sheet.Cells(titleRow, columnIndex).ColumnSpan = 1
                sheet.Cells(titleRow, startCol).ColumnSpan = sheet.ColumnCount - koteiColCount - 1
                sheet.Cells(titleRow, startCol).Locked = True
                sheet.Cells(titleRow, startCol).BackColor = Color.Khaki
                sheet.Cells(titleRow, startCol).Value = "月分布"
                sheet.Cells(titleRow, startCol).VerticalAlignment = CellVerticalAlignment.Center
                '太線なし
                sheet.Cells(titleRow, startCol).Border = New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None))
            End If

            If Not sheet.Equals(m_view_shukei.spdZaimuJiseki_Sheet1) Then
                '月分布フッター太線なし
                sheet.Cells(sheet.RowCount - 1, startCol).Border = New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None))
            End If
        End Sub

        ''' <summary>
        ''' ◀◀縮小
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <remarks></remarks>
        Public Sub UnVisibleMonthColumns(ByVal sheet As SheetView, ByVal startCol As Integer, _
                                         ByVal titleRow As Integer, ByVal columnIndex As Integer, ByVal koteiColCount As Integer)
            sheet.Columns(columnIndex + 1).Visible = True
            For index As Integer = 0 To HALF_MONTH_DEFAULT_COUNT - 1
                sheet.Columns(columnIndex - index).Visible = False
            Next
            '先頭の年月
            If columnIndex - HALF_MONTH_DEFAULT_COUNT + 1 = startCol Then
                '月分布ヘッダー
                sheet.Cells(titleRow, startCol).ColumnSpan = 1
                sheet.Cells(titleRow, columnIndex + 1).ColumnSpan = sheet.ColumnCount - koteiColCount - HALF_MONTH_DEFAULT_COUNT - 1
                sheet.Cells(titleRow, columnIndex + 1).Locked = True
                sheet.Cells(titleRow, columnIndex + 1).BackColor = Color.Khaki
                sheet.Cells(titleRow, columnIndex + 1).Value = "月分布"
                sheet.Cells(titleRow, columnIndex + 1).VerticalAlignment = CellVerticalAlignment.Center
            End If
        End Sub
#End Region

#Region "製作台数"

#Region "製作台数スプレッドにデータ設定"
        ''' <summary>
        ''' 製作台数スプレッドにデータを設定する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DispSpreadSeisakuDaisu(ByVal eventCode As String)
            ''クリア
            'Dim colCount As Integer = m_view_shukei.spdSeisakuDaisu_Sheet1.ColumnCount _
            '                            - SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN _
            '                            - (SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT * 2)
            'If colCount > 0 Then
            '    m_view_shukei.spdSeisakuDaisu_Sheet1.RemoveColumns(SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN, colCount)
            'End If

            Dim yosanSeisakuDaisuList As List(Of TYosanSeisakuDaisuVo)
            yosanSeisakuDaisuList = editDao.FindYosanSeisakuDaisuBy(eventCode)

            m_view_shukei.spdSeisakuDaisu_Sheet1.RowCount = SPD_DAISU_ROW_COUNT

            For colIndex = SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN To m_view_shukei.spdSeisakuDaisu_Sheet1.ColumnCount - 1 Step 2
                SetKojishireiColumnPro(colIndex)
            Next

            _KanseiDaisuList = New List(Of TYosanSeisakuDaisuVo)
            _WbDaisuList = New List(Of TYosanSeisakuDaisuVo)
            For Each daisuVo As TYosanSeisakuDaisuVo In yosanSeisakuDaisuList
                Dim colIndex As Integer = daisuVo.KoujiShireiNoHyojijunNo * 2 + SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN
                If colIndex >= m_view_shukei.spdSeisakuDaisu_Sheet1.ColumnCount Then
                    m_view_shukei.spdSeisakuDaisu_Sheet1.AddColumns(colIndex, 2)
                    SetKojishireiColumnPro(colIndex)
                End If
                'ユニット区分
                m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, colIndex).Value = UnitKbnName(daisuVo.UnitKbn)
                '工事指令No
                m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, colIndex).Value = daisuVo.KoujiShireiNo
                '台数
                If IsKanseisha(daisuVo.ShisakuSyubetu) Then
                    '完成車の場合
                    m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, colIndex).Value = daisuVo.DaisuSuryo
                    Dim newVo As New TYosanSeisakuDaisuVo
                    VoUtil.CopyProperties(daisuVo, newVo)
                    _KanseiDaisuList.Add(newVo)
                End If
                If IsWb(daisuVo.ShisakuSyubetu) Then
                    'WB車の場合
                    m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, colIndex).Value = daisuVo.DaisuSuryo
                    Dim newVo As New TYosanSeisakuDaisuVo
                    VoUtil.CopyProperties(daisuVo, newVo)
                    _WbDaisuList.Add(newVo)
                End If
            Next
            '４列未満の場合
            If _KanseiDaisuList.Count < SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT Then
                SetDefaultSeisakuDaisuList(eventCode, SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT - _KanseiDaisuList.Count, _KanseiDaisuList, SHISAKU_SYUBETU_KANSEI)
            End If
            If _WbDaisuList.Count < SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT Then
                SetDefaultSeisakuDaisuList(eventCode, SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT - _WbDaisuList.Count, _WbDaisuList, SHISAKU_SYUBETU_WB)
            End If

            '台数合計
            SetSpreadSeisakuDaisuPro()
            CalcTaisu()
            '工事指令Noヘッダー
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_KOTEI_COLUMN_COUNT).ColumnSpan = m_view_shukei.spdSeisakuDaisu_Sheet1.ColumnCount - SPD_DAISU_KOTEI_COLUMN_COUNT
            '固定列を設定
            m_view_shukei.spdSeisakuDaisu_Sheet1.FrozenColumnCount = SPD_DAISU_KOTEI_COLUMN_COUNT

        End Sub

        ''' <summary>
        ''' デフォルトで４列とする
        ''' </summary>
        ''' <param name="addCount"></param>
        ''' <param name="aList"></param>
        ''' <remarks></remarks>
        Private Sub SetDefaultSeisakuDaisuList(ByVal eventCode As String, ByVal addCount As Integer, ByVal aList As List(Of TYosanSeisakuDaisuVo), ByVal shisakuSyubetu As String)
            For index = 0 To addCount - 1
                Dim newVo As New TYosanSeisakuDaisuVo
                newVo.YosanEventCode = eventCode
                newVo.ShisakuSyubetu = shisakuSyubetu
                newVo.KoujiShireiNoHyojijunNo = aList.Count
                If aList.Count = 0 Then
                    newVo.UnitKbn = UNIT_KBN_TRIM_CODE
                Else
                    If IsUnitTrim(aList(aList.Count - 1).UnitKbn) Then
                        newVo.UnitKbn = UNIT_KBN_METAL_CODE
                    Else
                        newVo.UnitKbn = UNIT_KBN_TRIM_CODE
                    End If
                End If
                aList.Add(newVo)
            Next
        End Sub

        ''' <summary>
        ''' SPREADで台数合計セルの配置を設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSpreadSeisakuDaisuPro()
            '台数合計セル
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Locked = True
            'm_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).CellType = GetDaisuCellType()
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).Locked = True
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Locked = True
            'm_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).CellType = GetDaisuCellType()
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).Locked = True
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).HorizontalAlignment = CellHorizontalAlignment.Left

        End Sub

        ''' <summary>
        ''' SPREADで工事指令№セルの配置を設定する。
        ''' </summary>
        ''' <param name="columnIndex"></param>
        ''' <remarks></remarks>
        Private Sub SetKojishireiColumnPro(ByVal columnIndex As Integer)
            m_view_shukei.spdSeisakuDaisu_Sheet1.Columns(columnIndex).Width = m_view_shukei.spdSeisakuDaisu_Sheet1.Columns(SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN).Width
            m_view_shukei.spdSeisakuDaisu_Sheet1.Columns(columnIndex + 1).Width = m_view_shukei.spdSeisakuDaisu_Sheet1.Columns(SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN + 1).Width
            'ユニット区分セル
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).Locked = True
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).VerticalAlignment = CellVerticalAlignment.Center

            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).ColumnSpan = 2
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).BackColor = Color.Khaki
            '工事指令№セル
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).Locked = False
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).CellType = GetKojishireiCellType()
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).ColumnSpan = 2
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).BackColor = Color.White
            '台数セル
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).Locked = False
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).CellType = GetDaisuCellType()
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).BackColor = Color.White
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).Locked = True
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).VerticalAlignment = CellVerticalAlignment.Center
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).Value = "台"
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).BackColor = Color.White
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).Locked = False
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).CellType = GetDaisuCellType()
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).BackColor = Color.White
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).Locked = True
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).VerticalAlignment = CellVerticalAlignment.Center
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).Value = "台"
            m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).BackColor = Color.White
        End Sub

        ''' <summary>
        ''' 台数合計
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CalcTaisu()

            Dim taisuKanseiShaCount As Integer = 0
            Dim taisuWbShaCount As Integer = 0
            For columnIndex As Integer = SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN To m_view_shukei.spdSeisakuDaisu_Sheet1.ColumnCount - 1 Step 2
                '完成車
                If StringUtil.IsNotEmpty(m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).Value) Then
                    taisuKanseiShaCount = taisuKanseiShaCount + CInt(m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).Value)
                End If
                If taisuKanseiShaCount = 0 Then
                    m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = Nothing
                Else
                    m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = taisuKanseiShaCount
                End If

                'WB車
                If StringUtil.IsNotEmpty(m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).Value) Then
                    taisuWbShaCount = taisuWbShaCount + CInt(m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).Value)
                End If
                If taisuWbShaCount = 0 Then
                    m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = Nothing
                Else
                    m_view_shukei.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = taisuWbShaCount
                End If
            Next

        End Sub
#End Region

#Region "製作台数情報 "
        ''' <summary>
        ''' 製作台数情報
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property YosanSeisakuDaisuList() As List(Of TYosanSeisakuDaisuVo)
            Get
                Dim allList As New List(Of TYosanSeisakuDaisuVo)
                For Each vo As TYosanSeisakuDaisuVo In _KanseiDaisuList
                    Dim newVo As New TYosanSeisakuDaisuVo
                    VoUtil.CopyProperties(vo, newVo)
                    allList.Add(newVo)
                Next
                For Each vo As TYosanSeisakuDaisuVo In _WbDaisuList
                    Dim newVo As New TYosanSeisakuDaisuVo
                    VoUtil.CopyProperties(vo, newVo)
                    allList.Add(newVo)
                Next

                Return allList
            End Get
        End Property

#End Region

#End Region

#Region "金材"

#Region "金材スプレッドにデータ設定"
        ''' <summary>
        ''' 金材スプレッドにデータ設定する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DispSpreadKanazai(ByVal eventCode As String)

            ''クリア
            'Dim colCount As Integer = m_view_shukei.spdKanazai_Sheet1.ColumnCount - 1 _
            '                            - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX - 1
            'If colCount > 0 Then
            '    m_view_shukei.spdKanazai_Sheet1.RemoveColumns(SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, colCount)
            '    m_view_shukei.spdKanazai_Sheet1.RowCount = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 1
            'End If

            _KanazaiList = editDao.FindYosanKanazaiBy(eventCode)

            Dim vosByHyojijun As Dictionary(Of String, List(Of TYosanKanazaiVo)) = MakeKanazaiVosByHyojijun(_KanazaiList)

            If vosByHyojijun.Count > 0 Then
                m_view_shukei.spdKanazai_Sheet1.RemoveColumns(SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, 1)
                m_view_shukei.spdKanazai_Sheet1.AddRows(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, vosByHyojijun.Count)

                Dim indexX As Integer = 0
                Dim ks As String = String.Empty
                Dim columnIndex As Integer = 0

                For Each key As String In vosByHyojijun.Keys
                    Dim rowIndex As Integer = indexX + SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX
                    columnIndex = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX
                    SetKanazaiRowPro(rowIndex)
                    '金材項目名
                    m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).Value = key
                    '単価
                    m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).Value = vosByHyojijun(key)(0).KanazaiUnitPrice

                    For Each kanazaiVo As TYosanKanazaiVo In vosByHyojijun(key)
                        If indexX = 0 Then
                            '年期タイトル
                            If (columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                                '合計列追加
                                AddSpdMonthCountColumn(m_view_shukei.spdKanazai_Sheet1, columnIndex, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                                                       SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                                columnIndex = columnIndex + 1
                            End If

                            m_view_shukei.spdKanazai_Sheet1.AddColumns(columnIndex, 1)
                            '年
                            m_view_shukei.spdKanazai_Sheet1.Cells(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).Value = ChangeDBYearToSpreadYear(kanazaiVo.YosanTukurikataYyyyMm, kanazaiVo.YosanTukurikataKs)
                            '月
                            m_view_shukei.spdKanazai_Sheet1.Cells(SPD_KANAZAI_MONTH_ROW_INDEX, columnIndex).Value = CInt(kanazaiVo.YosanTukurikataYyyyMm.Substring(4, 2)).ToString & "月"
                        End If
                        '月分布タイトル
                        SetMonthTitlePro(m_view_shukei.spdKanazai_Sheet1, columnIndex, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                                         SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                        '合計列の場合
                        If (columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                            SetMonthCellPro(m_view_shukei.spdKanazai_Sheet1, rowIndex, columnIndex, True)
                            columnIndex = columnIndex + 1
                        End If
                        '台数セル
                        m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, columnIndex).Value = kanazaiVo.DaisuSuryo
                        SetMonthCellPro(m_view_shukei.spdKanazai_Sheet1, rowIndex, columnIndex, False)
                        columnIndex = columnIndex + 1
                    Next

                    indexX = indexX + 1
                Next
                '合計列追加
                AddSpdMonthCountColumn(m_view_shukei.spdKanazai_Sheet1, columnIndex, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                                       SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                '月分布フッター
                m_view_shukei.spdKanazai_Sheet1.Cells(m_view_shukei.spdKanazai_Sheet1.RowCount - 1, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).ColumnSpan = m_view_shukei.spdKanazai_Sheet1.ColumnCount - SPD_KANAZAI_KOTEI_COLUMN_COUNT - 1

                '台数合計
                CalcKanazai()
            End If
            '固定列を設定
            m_view_shukei.spdKanazai_Sheet1.FrozenColumnCount = SPD_KANAZAI_KOTEI_COLUMN_COUNT
            '固定行を設定
            m_view_shukei.spdKanazai_Sheet1.FrozenRowCount = SPD_KANAZAI_KOTEI_ROW_COUNT

        End Sub

        ''' <summary>
        ''' SPREADで行の配置を設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetKanazaiRowPro(ByVal rowIndex As Integer)
            '金材項目名セル
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).Locked = True
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '台数セル
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).Locked = True
            'm_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).CellType = GetDaisuCellType()
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX + 1).Locked = True
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX + 1).VerticalAlignment = CellVerticalAlignment.Center
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX + 1).Value = "台"
            '単価セル
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).Locked = False
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).CellType = GetTankaCellType()
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '計セル
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).Locked = True
            'm_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).CellType = GetKingakuCellType()
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center

            m_view_shukei.spdKanazai_Sheet1.Rows(rowIndex).BackColor = Color.White
            m_view_shukei.spdKanazai_Sheet1.Cells(SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, m_view_shukei.spdKanazai_Sheet1.ColumnCount - 1).RowSpan = m_view_shukei.spdKanazai_Sheet1.RowCount - 1
        End Sub

        ''' <summary>
        ''' 台数合計
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CalcKanazai()

            For rowIndex As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX To m_view_shukei.spdKanazai_Sheet1.RowCount - 2
                Dim daisuCount As Integer = 0
                Dim monthCount As Integer = 0
                For columnIndex As Integer = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX To m_view_shukei.spdKanazai_Sheet1.ColumnCount - 2
                    '合計列以外
                    If Not ((columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                        If StringUtil.IsNotEmpty(m_view_shukei.spdKanazai_Sheet1.GetValue(rowIndex, columnIndex)) Then
                            monthCount = monthCount + m_view_shukei.spdKanazai_Sheet1.GetValue(rowIndex, columnIndex)
                        End If
                    Else
                        If monthCount = 0 Then
                            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, columnIndex).Value = Nothing
                        Else
                            m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, columnIndex).Value = monthCount
                        End If
                        daisuCount = daisuCount + monthCount

                        monthCount = 0
                    End If
                Next
                '台数合計
                If daisuCount = 0 Then
                    m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).Value = Nothing
                Else
                    m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).Value = daisuCount
                End If
                '計
                Dim tanka As Decimal? = CDec(IIf(StringUtil.IsNotEmpty(m_view_shukei.spdKanazai_Sheet1.GetValue(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX)), m_view_shukei.spdKanazai_Sheet1.GetValue(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX), Nothing))
                If tanka IsNot Nothing AndAlso daisuCount > 0 Then
                    m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).Value = tanka * daisuCount
                Else
                    m_view_shukei.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).Value = Nothing
                End If
            Next

        End Sub
#End Region

#Region "金材情報 "
        ''' <summary>
        ''' 金材項目名
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property KanazaiNameList() As List(Of String)
            Get
                Dim aList As New List(Of String)
                For rowIndex As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX To m_view_shukei.spdKanazai_Sheet1.RowCount - 1
                    aList.Add(m_view_shukei.spdKanazai_Sheet1.GetValue(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX))
                Next

                Return aList
            End Get
        End Property
        ''' <summary>
        ''' 金材情報
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property YosanKanazaiList() As Dictionary(Of String, List(Of TYosanKanazaiVo))
            Get
                Return MakeKanazaiVosByHyojijun(_KanazaiList)
            End Get
        End Property
        ''' <summary>
        ''' 表示順Noによって金材情報を返すDictionaryを作成する
        ''' </summary>
        ''' <param name="aList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MakeKanazaiVosByHyojijun(ByVal aList As List(Of TYosanKanazaiVo)) As Dictionary(Of String, List(Of TYosanKanazaiVo))
            Dim vosByHyojijun As New Dictionary(Of String, List(Of TYosanKanazaiVo))

            For Each vo As TYosanKanazaiVo In aList
                If Not vosByHyojijun.ContainsKey(vo.KanazaiName) Then
                    vosByHyojijun.Add(vo.KanazaiName, New List(Of TYosanKanazaiVo))
                End If
                vosByHyojijun(vo.KanazaiName).Add(vo)
            Next

            Return vosByHyojijun
        End Function

#End Region

#End Region

#Region "造り方"

#Region "造り方スプレッドにデータ設定"
        ''' <summary>
        ''' 造り方スプレッドにデータ設定する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DispSpreadTukurikata(ByVal eventCode As String)

            '完成車
            ''クリア
            'Dim colCount As Integer = m_view_shukei.spdTukurikataKanseisha_Sheet1.ColumnCount - 1 _
            '                            - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX - 1
            'If colCount > 0 Then
            '    m_view_shukei.spdTukurikataKanseisha_Sheet1.RemoveColumns(SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, colCount)
            '    m_view_shukei.spdTukurikataKanseisha_Sheet1.RowCount = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX + 1
            'End If
            _KanseiTukurikataList = DispSpreadTukurikata(eventCode, SHISAKU_SYUBETU_KANSEI, m_view_shukei.spdTukurikataKanseisha_Sheet1)

            'ホワイトボディ
            ''クリア
            'colCount = m_view_shukei.spdTukurikataWBsha_Sheet1.ColumnCount - 1 _
            '                            - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX - 1
            'If colCount > 0 Then
            '    m_view_shukei.spdTukurikataWBsha_Sheet1.RemoveColumns(SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, colCount)
            '    m_view_shukei.spdTukurikataWBsha_Sheet1.RowCount = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX + 1
            'End If

            _WbTukurikataList = DispSpreadTukurikata(eventCode, SHISAKU_SYUBETU_WB, m_view_shukei.spdTukurikataWBsha_Sheet1)

        End Sub

        ''' <summary>
        ''' 完成車・ホワイトボディ
        ''' </summary>
        ''' <param name="shisakuSyubetu"></param>
        ''' <param name="aSheet"></param>
        ''' <remarks></remarks>
        Private Function DispSpreadTukurikata(ByVal eventCode As String, ByVal shisakuSyubetu As String, ByVal aSheet As SheetView) As List(Of TYosanTukurikataVo)

            Dim aTukurikataList As List(Of TYosanTukurikataVo) = editDao.FindYosanTukurikataBy(eventCode, shisakuSyubetu)
            Dim vosByHyojijun As Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo))) = MakeTukurikataVosByHyojijun(aTukurikataList)

            If vosByHyojijun.Count > 0 Then
                aSheet.RemoveColumns(SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, 1)

                Dim indexX As Integer = 0
                Dim ks As String = String.Empty
                Dim columnIndex As Integer = 0
                Dim rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX
                Dim startRow As Integer
                For Each unitKbn As String In vosByHyojijun.Keys
                    'ユニット開始の行番号
                    startRow = rowIndex

                    For Each buhinhyoName As String In vosByHyojijun(unitKbn).Keys
                        aSheet.AddRows(rowIndex, 1)
                        SetTukurikataRowPro(aSheet, rowIndex)
                        columnIndex = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX

                        Dim patternName As String = String.Empty

                        For Each tukurikataVo As TYosanTukurikataVo In vosByHyojijun(unitKbn)(buhinhyoName)
                            If indexX = 0 Then
                                '年期タイトル
                                If (columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                                    '合計列追加
                                    AddSpdMonthCountColumn(aSheet, columnIndex, SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX, _
                                                           SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, SPD_TUKURIKATA_YEAR_ROW_INDEX, SPD_TUKURIKATA_MONTH_ROW_INDEX)
                                    columnIndex = columnIndex + 1
                                End If

                                aSheet.AddColumns(columnIndex, 1)
                                '年
                                aSheet.Cells(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).Value = ChangeDBYearToSpreadYear(tukurikataVo.YosanTukurikataYyyyMm, tukurikataVo.YosanTukurikataKs)
                                '月
                                aSheet.Cells(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).Value = CInt(tukurikataVo.YosanTukurikataYyyyMm.Substring(4, 2)).ToString & "月"
                            End If
                            SetMonthTitlePro(aSheet, columnIndex, SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX, _
                                             SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, SPD_TUKURIKATA_YEAR_ROW_INDEX, SPD_TUKURIKATA_MONTH_ROW_INDEX)
                            '合計列の場合
                            If (columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                                SetMonthCellPro(aSheet, rowIndex, columnIndex, True)
                                columnIndex = columnIndex + 1
                            End If
                            '台数セル
                            aSheet.Cells(rowIndex, columnIndex).Value = tukurikataVo.DaisuSuryo
                            SetMonthCellPro(aSheet, rowIndex, columnIndex, False)
                            columnIndex = columnIndex + 1
                            '
                            patternName = tukurikataVo.PatternName
                        Next
                        'ユニット区分
                        aSheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).Value = unitKbn
                        'aSheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).Value = UnitKbnName(unitKbn)
                        '部品表名
                        aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX).Value = buhinhyoName
                        'パターン名
                        aSheet.Cells(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX).Value = patternName

                        rowIndex = rowIndex + 1
                        indexX = indexX + 1
                    Next

                    aSheet.Cells(startRow, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).RowSpan = vosByHyojijun(unitKbn).Count

                    'ユニット単位で追加行の行番号
                    PatternNameInsertRowIndex(shisakuSyubetu, unitKbn) = rowIndex
                Next

                '合計列追加
                AddSpdMonthCountColumn(aSheet, columnIndex, SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX, _
                                       SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, SPD_TUKURIKATA_YEAR_ROW_INDEX, SPD_TUKURIKATA_MONTH_ROW_INDEX)
                '月分布フッター
                aSheet.Cells(aSheet.RowCount - 1, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX).ColumnSpan = aSheet.ColumnCount - SPD_TUKURIKATA_KOTEI_COLUMN_COUNT - 1

                SetRowBackColorGray(aSheet, eventCode)

                '台数合計
                CalcTukurikata(aSheet)
            End If

            '固定列を設定
            aSheet.FrozenColumnCount = SPD_TUKURIKATA_KOTEI_COLUMN_COUNT
            '固定行を設定
            aSheet.FrozenRowCount = SPD_TUKURIKATA_KOTEI_ROW_COUNT

            Return aTukurikataList
        End Function

        ''' <summary>
        ''' SPREADで行の配置を設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetTukurikataRowPro(ByVal aSheet As SheetView, ByVal rowIndex As Integer)
            'ユニットセル
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).Locked = True
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Left
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '部品番号名セル
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX).Locked = True
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Left
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            'パターン名セル
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX).Locked = True
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Left
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '部品費（円）セル
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Locked = True
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).CellType = GetKingaku11CellType()
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '型費（円）セル
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Locked = True
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).CellType = GetKingaku11CellType()
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '台数セル
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).Locked = True
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).CellType = GetDaisuCellType()
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).Locked = True
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).VerticalAlignment = CellVerticalAlignment.Center
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).Value = "台"

            aSheet.Rows(rowIndex).BackColor = Color.White
            aSheet.Cells(SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, aSheet.ColumnCount - 1).RowSpan = aSheet.RowCount - 1
        End Sub

        ''' <summary>
        ''' 予算部品のパターン名を存在するかどうか
        ''' </summary>
        ''' <remarks></remarks>
        Private Function HasPatternNameInBuhinEdit(ByVal patternName As String, _
                                                   ByVal buhinhyoName As String, _
                                                   ByVal patternVos As List(Of TYosanBuhinEditPatternVo)) As Boolean
            For Each Vo As TYosanBuhinEditPatternVo In patternVos
                'パターン名存在の場合
                If String.Equals(Vo.BuhinhyoName, buhinhyoName) AndAlso _
                   String.Equals(Vo.PatternName, patternName) Then
                    Return True
                End If
            Next

            Return False
        End Function

        ''' <summary>
        ''' 行の背景色をグレーで設定
        ''' </summary>
        ''' <param name="aSheet"></param>
        ''' <remarks></remarks>
        Private Sub SetRowBackColorGray(ByVal aSheet As SheetView, ByVal eventCode As String)
            '予算書部品編集ﾊﾟﾀｰﾝ情報取得
            Dim patternVos As List(Of TYosanBuhinEditPatternVo) = editDao.FindYosanBuhinEditPatternBy(eventCode)

            For rowIndex = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To aSheet.RowCount - 2
                '造り方情報に有って、予算部品のに存在しないパターン名の行をグレーアウト
                Dim unitKbn As String = UnitKbnCode(aSheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX))
                Dim patternName As String = aSheet.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)
                Dim buhinhyoName As String = aSheet.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)

                For columnIndex = SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX To aSheet.ColumnCount - 2
                    If Not HasPatternNameInBuhinEdit(patternName, buhinhyoName, patternVos) Then
                        aSheet.Cells(rowIndex, columnIndex).BackColor = Color.Gray
                    Else
                        aSheet.Cells(rowIndex, columnIndex).BackColor = Color.White
                    End If
                Next
            Next
        End Sub

        ''' <summary>
        ''' 台数合計
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CalcTukurikata(ByVal aSheet As SheetView)

            For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To aSheet.RowCount - 2
                Dim daisuCount As Integer = 0
                Dim monthCount As Integer = 0
                For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To aSheet.ColumnCount - 2
                    '合計列以外
                    If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                        If StringUtil.IsNotEmpty(aSheet.GetValue(rowIndex, columnIndex)) Then
                            monthCount = monthCount + aSheet.GetValue(rowIndex, columnIndex)
                        End If
                    Else
                        If monthCount = 0 Then
                            aSheet.Cells(rowIndex, columnIndex).Value = Nothing
                        Else
                            aSheet.Cells(rowIndex, columnIndex).Value = monthCount
                        End If
                        daisuCount = daisuCount + monthCount

                        monthCount = 0
                    End If
                Next
                '台数合計
                If daisuCount = 0 Then
                    aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).Value = Nothing
                Else
                    aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).Value = daisuCount
                End If
            Next

        End Sub
#End Region


#Region "予算書部品表選択情報"
        ''' <summary>
        ''' 予算書部品表選択情報
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property YosanBuhinhyoList(ByVal eventCode As String) As List(Of TYosanBuhinSelectVo)
            Get
                Dim buhinhyoNameVos As List(Of TYosanBuhinSelectVo) = editDao.FindBuhinhyoNameBy(eventCode)

                Return buhinhyoNameVos
            End Get
        End Property
#End Region


#Region "造り方情報 "

        ''' <summary>
        ''' パターン名
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PatternNameList(ByVal aSheet As SheetView) As Dictionary(Of String, List(Of String))
            Get
                Dim aList As New Dictionary(Of String, List(Of String))
                For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To aSheet.RowCount - 2
                    Dim buhinhyoName As String = aSheet.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)

                    If Not aList.ContainsKey(buhinhyoName) Then
                        aList.Add(buhinhyoName, New List(Of String))
                    End If
                    aList(buhinhyoName).Add(aSheet.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX))
                Next

                Return aList
            End Get
        End Property

        ''' <summary>
        ''' 造り方情報
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property YosanTukurikataKanseiList() As Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo)))
            Get
                Return MakeTukurikataVosByHyojijun(_KanseiTukurikataList)
            End Get
        End Property
        ''' <summary>
        ''' 造り方情報
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property YosanTukurikataWbList() As Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo)))
            Get
                Return MakeTukurikataVosByHyojijun(_WbTukurikataList)
            End Get
        End Property
        ''' <summary>
        ''' パターン名の追加行の行番号
        ''' </summary>
        ''' <remarks></remarks>
        Private Property PatternNameInsertRowIndex(ByVal shisakuSyubetu As String, ByVal unitKbn As String) As Integer
            Get
                If IsKanseisha(shisakuSyubetu) Then
                    If IsUnitMetal(unitKbn) Then
                        Return _KanseiMetalInsertRowIndex
                    Else
                        Return _KanseiTrimInsertRowIndex
                    End If
                Else
                    If IsUnitMetal(unitKbn) Then
                        Return _WbMetalInsertRowIndex
                    Else
                        Return _WbTrimInsertRowIndex
                    End If
                End If
            End Get
            Set(ByVal value As Integer)
                If IsKanseisha(shisakuSyubetu) Then
                    If IsUnitMetal(unitKbn) Then
                        _KanseiMetalInsertRowIndex = value
                    Else
                        _KanseiTrimInsertRowIndex = value
                    End If
                Else
                    If IsUnitMetal(unitKbn) Then
                        _WbMetalInsertRowIndex = value
                    Else
                        _WbTrimInsertRowIndex = value
                    End If
                End If
            End Set
        End Property
        ''' <summary>
        ''' 表示順Noによって造り方情報を返すDictionaryを作成する
        ''' </summary>
        ''' <param name="aList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MakeTukurikataVosByHyojijun(ByVal aList As List(Of TYosanTukurikataVo)) As Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo)))
            Dim vosByHyojijun As New Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo)))

            For Each vo As TYosanTukurikataVo In aList
                If Not vosByHyojijun.ContainsKey(vo.UnitKbn) Then
                    vosByHyojijun.Add(vo.UnitKbn, New Dictionary(Of String, List(Of TYosanTukurikataVo)))
                End If
                If Not vosByHyojijun(vo.UnitKbn).ContainsKey(vo.BuhinhyoName) Then
                    vosByHyojijun(vo.UnitKbn).Add(vo.BuhinhyoName, New List(Of TYosanTukurikataVo))
                End If
                'If Not vosByHyojijun(vo.BuhinhyoName).ContainsKey(vo.PatternName) Then
                '    vosByHyojijun(vo.BuhinhyoName).Add(vo.PatternName, New List(Of TYosanTukurikataVo))
                'End If
                vosByHyojijun(vo.UnitKbn)(vo.BuhinhyoName).Add(vo)
            Next

            Return vosByHyojijun
        End Function
#End Region

#End Region

#Region "集計値"
        ''' <summary>
        ''' 集計値スプレッドにデータ設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DispSpreadZaimuJiseki(ByVal eventCode As String)
            For rowIndex As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To m_view_shukei.spdZaimuJiseki_Sheet1.RowCount - 1
                '計の列をフォーマット
                'm_view_shukei.spdZaimuJiseki_Sheet1.Cells(rowIndex, m_view_shukei.spdZaimuJiseki_Sheet1.ColumnCount - 1).CellType = GetKingaku11CellType()
                m_view_shukei.spdZaimuJiseki_Sheet1.Cells(rowIndex, m_view_shukei.spdZaimuJiseki_Sheet1.ColumnCount - 1).HorizontalAlignment = CellHorizontalAlignment.Right
                m_view_shukei.spdZaimuJiseki_Sheet1.Cells(rowIndex, m_view_shukei.spdZaimuJiseki_Sheet1.ColumnCount - 1).VerticalAlignment = CellVerticalAlignment.Center
            Next
            '「計（横計）」の列幅
            m_view_shukei.spdZaimuJiseki_Sheet1.Columns(m_view_shukei.spdZaimuJiseki_Sheet1.ColumnCount - 1).Width = 95
            '固定列を設定
            m_view_shukei.spdZaimuJiseki_Sheet1.FrozenColumnCount = SPD_ZAIMUJISEKI_KOTEI_COLUMN_COUNT

            '再計算
            DoCalc(eventCode, True)
        End Sub

        ''' <summary>
        ''' 再計算ボタンの処理
        ''' </summary>
        ''' <param name="isInitShow">初期表示の場合はTrue</param>
        ''' <remarks></remarks>
        Public Sub DoCalc(ByVal eventCode As String, ByVal isInitShow As Boolean)
            Dim colCount As Integer = m_view_shukei.spdZaimuJiseki_Sheet1.ColumnCount - SPD_ZAIMUJISEKI_KOTEI_COLUMN_COUNT - 1
            If colCount > 0 Then
                m_view_shukei.spdZaimuJiseki_Sheet1.RemoveColumns(SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, colCount)
            End If
            '集計値SPREADの月分布列に列を挿入
            AddZaimuJisekiMonthColumns(eventCode)

            '造り方Spreadの部品費と型費をセット
            SetSpreadTukurikataBuhinHi(eventCode, True, True)

            '集計値Spreadに表示値をセット
            SetSpreadZaimujisekiData(eventCode, isInitShow)
            '合計
            CalcZaimuJiseki()

            'システム日付の直近の年期(月分布列)を拡大表示
            VisibleSystemYearMonthColumns()
        End Sub

#Region "集計値SPREADの月分布列に列を挿入"
        ''' <summary>
        ''' 集計値SPREADの月分布列に列を挿入
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub AddZaimuJisekiMonthColumns(ByVal eventCode As String)

            _YosanEventVo = EventDao.FindByPk(eventCode)

            '最小の年、期
            Dim minYearKs As String = GetSpreadMinYearKs(_YosanEventVo.YosanCode)
            '最大の年、期
            Dim maxYearKs As String = GetSpreadMaxYearKs(_YosanEventVo.YosanCode)

            'データ無
            If StringUtil.IsEmpty(minYearKs) AndAlso StringUtil.IsEmpty(maxYearKs) Then
                Return
            End If

            'YYYYMM
            Dim yearMonths As New List(Of String)

            If StringUtil.IsEmpty(minYearKs) AndAlso StringUtil.IsNotEmpty(maxYearKs) Then
                '最大年、期のみ取得
                Dim year As String = maxYearKs.Substring(0, 4)
                Dim ks As String = maxYearKs.Substring(4)
                yearMonths.AddRange(GetYearMonth(year, False, ks))
            ElseIf StringUtil.IsNotEmpty(minYearKs) AndAlso StringUtil.IsEmpty(maxYearKs) Then
                '最小年、期のみ取得
                Dim year As String = minYearKs.Substring(0, 4)
                Dim ks As String = minYearKs.Substring(4)
                yearMonths.AddRange(GetYearMonth(year, False, ks))
            Else
                '取得した最小年、期と最大年、期が一致の場合
                If String.Equals(minYearKs, maxYearKs) Then
                    Dim year As String = minYearKs.Substring(0, 4)
                    Dim ks As String = minYearKs.Substring(4)
                    yearMonths.AddRange(GetYearMonth(year, False, ks))
                Else
                    '取得した最小年と最大年が一致、期が不一致の場合
                    If String.Equals(minYearKs.Substring(0, 4), maxYearKs.Substring(0, 4)) Then
                        Dim year As String = minYearKs.Substring(0, 4)
                        yearMonths.AddRange(GetYearMonth(year, True))
                    Else
                        '取得した最小年、期と最大年、期が不一致の場合
                        For year As Integer = CInt(minYearKs.Substring(0, 4)) To CInt(maxYearKs.Substring(0, 4))
                            If year = CInt(minYearKs.Substring(0, 4)) Then
                                If String.Equals(minYearKs.Substring(4), UP_KS) Then
                                    yearMonths.AddRange(GetYearMonth(year, True))
                                Else
                                    yearMonths.AddRange(GetYearMonth(year, False, minYearKs.Substring(4)))
                                End If
                                Continue For
                            End If
                            If year = CInt(maxYearKs.Substring(0, 4)) Then
                                If String.Equals(maxYearKs.Substring(4), UP_KS) Then
                                    yearMonths.AddRange(GetYearMonth(year, False, maxYearKs.Substring(4)))
                                Else
                                    yearMonths.AddRange(GetYearMonth(year, True))
                                End If
                                Continue For
                            End If
                            yearMonths.AddRange(GetYearMonth(year, True))
                        Next
                    End If
                End If
            End If

            '集計値SPREADの月分布列に列を挿入
            Dim columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX
            For index As Integer = 0 To yearMonths.Count - 1
                '合計列追加
                If (columnIndex - SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    AddSpdMonthCountColumn(m_view_shukei.spdZaimuJiseki_Sheet1, columnIndex, SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, _
                                           SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
                    SetMonthColumnProKingaku(m_view_shukei.spdZaimuJiseki_Sheet1, columnIndex, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
                                             SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
                    columnIndex = columnIndex + 1
                End If

                m_view_shukei.spdZaimuJiseki_Sheet1.AddColumns(columnIndex, 1)
                SetMonthColumnProKingaku(m_view_shukei.spdZaimuJiseki_Sheet1, columnIndex, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
                                         SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
                '年
                m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).Value = ChangeYearMonthToYearKs(yearMonths(index).Substring(0, 4), yearMonths(index).Substring(4))
                '月
                m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_MONTH_ROW_INDEX, columnIndex).Value = CInt(yearMonths(index).Substring(4, 2)).ToString & "月"
                columnIndex = columnIndex + 1
            Next
            '合計列追加
            AddSpdMonthCountColumn(m_view_shukei.spdZaimuJiseki_Sheet1, columnIndex, SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, _
                                   SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
            SetMonthColumnProKingaku(m_view_shukei.spdZaimuJiseki_Sheet1, columnIndex, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
                                     SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
        End Sub

        ''' <summary>
        ''' SPREADで列の配置を設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetMonthColumnProKingaku(ByVal sheet As SheetView, ByVal columnIndex As Integer, _
                                      ByVal startRow As Integer, ByVal startCol As Integer, ByVal yearRow As Integer, ByVal monthRow As Integer)
            '列幅をセット
            sheet.Columns(columnIndex).Width = 95
            '年セル
            sheet.Cells(yearRow, columnIndex).Locked = True
            sheet.Cells(yearRow, columnIndex).BackColor = Color.Khaki
            sheet.Cells(yearRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            sheet.Cells(yearRow, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            '月セル
            sheet.Cells(monthRow, columnIndex).Locked = True
            sheet.Cells(monthRow, columnIndex).BackColor = Color.Khaki
            sheet.Cells(monthRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            sheet.Cells(monthRow, columnIndex).VerticalAlignment = CellVerticalAlignment.Center

            '拡大、縮小ボタンを水平方向の配置
            If (columnIndex - startCol + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 OrElse _
               (columnIndex - startCol) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                sheet.Cells(yearRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Center
            End If

            '金額セル
            For rowIndex As Integer = startRow To sheet.RowCount - 1
                If rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX OrElse _
                   rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX Then
                    '比例費の見通し材料、輸送
                    sheet.Cells(rowIndex, columnIndex).Locked = False
                    sheet.Cells(rowIndex, columnIndex).BackColor = Color.White
                    If (columnIndex - startCol + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) > 0 Then
                        sheet.Cells(rowIndex, columnIndex).CellType = GetKingaku10CellType()
                    End If
                ElseIf rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX Then
                    '財務実績
                    sheet.Cells(rowIndex, columnIndex).Locked = False
                    sheet.Cells(rowIndex, columnIndex).BackColor = Color.White
                    If (columnIndex - startCol + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) > 0 Then
                        sheet.Cells(rowIndex, columnIndex).CellType = GetKingakuMinusCellType()
                    End If
                ElseIf rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_HATCHU_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_MITOSHI_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_HATCHU_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX Then
                    '合計行
                    sheet.Cells(rowIndex, columnIndex).Locked = True
                    'sheet.Cells(rowIndex, columnIndex).CellType = GetKingaku11CellType()
                    sheet.Cells(rowIndex, columnIndex).BackColor = Color.Azure
                Else
                    sheet.Cells(rowIndex, columnIndex).Locked = True
                    'sheet.Cells(rowIndex, columnIndex).CellType = GetKingaku11CellType()
                    sheet.Cells(rowIndex, columnIndex).BackColor = Color.WhiteSmoke
                End If
                sheet.Cells(rowIndex, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Right
                sheet.Cells(rowIndex, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            Next

        End Sub

        ''' <summary>
        ''' 年月取得
        ''' </summary>
        ''' <param name="year"></param>
        ''' <param name="ks"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetYearMonth(ByVal year As String, ByVal isAllYear As Boolean, Optional ByVal ks As String = Nothing) As List(Of String)
            Dim result As New List(Of String)

            '上期と下期
            If isAllYear Then
                For index As Integer = 4 To 9
                    result.Add(year & index.ToString("00"))
                Next
                For index As Integer = 10 To 12
                    result.Add(year & index.ToString())
                Next
                For index As Integer = 1 To 3
                    result.Add((CInt(year) + 1).ToString & index.ToString("00"))
                Next
            Else
                If String.Equals(ks, UP_KS) Then
                    '上期のみ
                    For index As Integer = 4 To 9
                        result.Add(year & index.ToString("00"))
                    Next
                Else
                    '下期のみ
                    For index As Integer = 10 To 12
                        result.Add(year & index.ToString())
                    Next
                    For index As Integer = 1 To 3
                        result.Add((CInt(year) + 1).ToString & index.ToString("00"))
                    Next
                End If
            End If

            Return result
        End Function

        ''' <summary>
        ''' 最小の年、期を取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSpreadMinYearKs(ByVal yosanCode As String) As String
            Dim minYearKs As String = Nothing

            '完成車Spreadにデータが有れば
            If m_view_shukei.spdTukurikataKanseisha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                'YYYYX期を取得
                Dim kanseiMonth As String = m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + HALF_MONTH_DEFAULT_COUNT)

                minYearKs = kanseiMonth
            End If
            'ﾎﾜｲﾄﾎﾞﾃﾞｨSpreadにデータが有れば
            If m_view_shukei.spdTukurikataWBsha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                'YYYYX期を取得
                Dim wbMonth As String = m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + HALF_MONTH_DEFAULT_COUNT)

                If StringUtil.IsNotEmpty(minYearKs) Then
                    minYearKs = IIf(CompareYearKs(minYearKs, wbMonth) = True, minYearKs, wbMonth)
                Else
                    minYearKs = wbMonth
                End If
            End If
            '金材Spreadにデータが有れば
            If m_view_shukei.spdKanazai_Sheet1.ColumnCount - SPD_KANAZAI_DEFAULT_COLUMN_COUNT > 0 Then
                Dim kanazaiMonth As String = m_view_shukei.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_MONTH_ROW_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + HALF_MONTH_DEFAULT_COUNT)

                If StringUtil.IsNotEmpty(minYearKs) Then
                    minYearKs = IIf(CompareYearKs(minYearKs, kanazaiMonth) = True, minYearKs, kanazaiMonth)
                Else
                    minYearKs = kanazaiMonth
                End If
            End If

            '「製作台数」の工事指令№で「T_SEISAKU_AS_KOUNYU_YOSAN」の「割り振る先の年月」の最小年月を取得
            Dim kounyuYosanMinYearKs As String = GetSeisakuAsKounyuYosanYearMonth(False)
            If StringUtil.IsNotEmpty(kounyuYosanMinYearKs) Then
                If StringUtil.IsNotEmpty(minYearKs) Then
                    minYearKs = IIf(CompareYearKs(minYearKs, kounyuYosanMinYearKs) = True, minYearKs, kounyuYosanMinYearKs)
                Else
                    minYearKs = kounyuYosanMinYearKs
                End If
            End If

            '予算書イベント別年月別財務実績情報（T_YOSAN_ZAIMU_JISEKI）の最小年月を取得
            Dim zaimuJisekiMinYearKs As String = GetYosanZaimuJisekiYearMonth(yosanCode, False)
            If StringUtil.IsEmpty(zaimuJisekiMinYearKs) Then
                Return minYearKs
            End If
            If StringUtil.IsNotEmpty(minYearKs) Then
                minYearKs = IIf(CompareYearKs(minYearKs, zaimuJisekiMinYearKs) = True, minYearKs, zaimuJisekiMinYearKs)
            Else
                minYearKs = zaimuJisekiMinYearKs
            End If

            Return minYearKs
        End Function

        ''' <summary>
        ''' 最大の年、期を取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSpreadMaxYearKs(ByVal yosanCode As String) As String
            Dim maxYearKs As String = Nothing

            '完成車Spreadにデータが有れば
            If m_view_shukei.spdTukurikataKanseisha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                'YYYYX期を取得
                Dim kanseiMonth As String = m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, m_view_shukei.spdTukurikataKanseisha_Sheet1.ColumnCount - 2)

                maxYearKs = kanseiMonth
            End If
            'ﾎﾜｲﾄﾎﾞﾃﾞｨSpreadにデータが有れば
            If m_view_shukei.spdTukurikataWBsha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                'YYYYX期を取得
                Dim wbMonth As String = m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, m_view_shukei.spdTukurikataWBsha_Sheet1.ColumnCount - 2)

                If StringUtil.IsNotEmpty(maxYearKs) Then
                    maxYearKs = IIf(CompareYearKs(maxYearKs, wbMonth) = False, maxYearKs, wbMonth)
                Else
                    maxYearKs = wbMonth
                End If
            End If
            '金材Spreadにデータが有れば
            If m_view_shukei.spdKanazai_Sheet1.ColumnCount - SPD_KANAZAI_DEFAULT_COLUMN_COUNT > 0 Then
                '最終列に追加していないので'
                Dim kanazaiMonth As String = m_view_shukei.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_MONTH_ROW_INDEX, m_view_shukei.spdKanazai_Sheet1.ColumnCount - 2)

                If StringUtil.IsNotEmpty(kanazaiMonth) Then
                    If StringUtil.IsNotEmpty(maxYearKs) Then
                        maxYearKs = IIf(CompareYearKs(maxYearKs, kanazaiMonth) = False, maxYearKs, kanazaiMonth)
                    Else
                        maxYearKs = kanazaiMonth
                    End If
                End If


            End If

            '「製作台数」の工事指令№で「T_SEISAKU_AS_KOUNYU_YOSAN」の「割り振る先の年月」の最大年月を取得
            Dim kounyuYosanMaxYearKs As String = GetSeisakuAsKounyuYosanYearMonth(True)
            If StringUtil.IsNotEmpty(kounyuYosanMaxYearKs) Then
                If StringUtil.IsNotEmpty(maxYearKs) Then
                    maxYearKs = IIf(CompareYearKs(maxYearKs, kounyuYosanMaxYearKs) = False, maxYearKs, kounyuYosanMaxYearKs)
                Else
                    maxYearKs = kounyuYosanMaxYearKs
                End If
            End If

            '予算書イベント別年月別財務実績情報（T_YOSAN_ZAIMU_JISEKI）の最大年月を取得
            Dim zaimuJisekiMaxYearKs As String = GetYosanZaimuJisekiYearMonth(yosancode, True)
            If StringUtil.IsEmpty(zaimuJisekiMaxYearKs) Then
                Return maxYearKs
            End If
            If StringUtil.IsNotEmpty(maxYearKs) Then
                maxYearKs = IIf(CompareYearKs(maxYearKs, zaimuJisekiMaxYearKs) = False, maxYearKs, zaimuJisekiMaxYearKs)
            Else
                maxYearKs = zaimuJisekiMaxYearKs
            End If

            Return maxYearKs
        End Function

        ''' <summary>
        ''' 予算書イベント別年月別財務実績情報（T_YOSAN_ZAIMU_JISEKI）の財務実績計上年月を取得
        ''' </summary>
        ''' <param name="yosanCode"></param>
        ''' <param name="isMax"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetYosanZaimuJisekiYearMonth(ByVal yosanCode As String, ByVal isMax As Boolean) As String
            Dim rtnYearKs As String = Nothing

            Dim yearMonth = editDao.FindYosanZaimuJisekiYyyyMmBy(yosanCode, isMax)
            If StringUtil.IsNotEmpty(yearMonth) And yearMonth > 0 Then
                'YYYYMM->YYYYX期を変換
                Dim year As String = yearMonth.Substring(0, 4)
                Dim month As Integer = yearMonth.Substring(4, 2)

                rtnYearKs = ChangeYearMonthToYearKs(year, month)
            End If

            Return rtnYearKs
        End Function


        ''' <summary>
        ''' 「製作台数」の工事指令№で「T_SEISAKU_AS_KOUNYU_YOSAN」の「割り振る先の年月」の年月を取得
        ''' </summary>
        ''' <param name="isMax"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSeisakuAsKounyuYosanYearMonth(ByVal isMax As Boolean) As String
            Dim rtnYearKs As String = Nothing

            For columnIndex As Integer = SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN To m_view_shukei.spdSeisakuDaisu_Sheet1.ColumnCount - 1 Step 2
                Dim kojishireiNo As String = m_view_shukei.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex)
                If StringUtil.IsNotEmpty(kojishireiNo) Then
                    '工事指令№で「T_SEISAKU_AS_KOUNYU_YOSAN」の「割り振る先の年月」取得
                    Dim yearMonth = editDao.FindSeisakuAsKounyuYosanBy(kojishireiNo, isMax)
                    If yearMonth Is Nothing Or yearMonth = 0 Then
                        Continue For
                    Else
                        yearMonth = yearMonth.Substring(0, 6)
                    End If

                    If StringUtil.IsNotEmpty(rtnYearKs) Then
                        If isMax Then
                            If yearMonth > rtnYearKs Then
                                rtnYearKs = yearMonth
                            End If
                        Else
                            If yearMonth < rtnYearKs Then
                                rtnYearKs = yearMonth
                            End If
                        End If
                    Else
                        rtnYearKs = yearMonth
                    End If
                End If
            Next
            'YYYYMM->YYYYX期を変換
            If StringUtil.IsNotEmpty(rtnYearKs) Then
                Dim year As String = rtnYearKs.Substring(0, 4)
                Dim month As Integer = rtnYearKs.Substring(4, 2)

                rtnYearKs = ChangeYearMonthToYearKs(year, month)
            End If

            Return rtnYearKs
        End Function

        ''' <summary>
        ''' 年、期を比較する。strComp1＜strComp2だら、trueを返す。
        ''' </summary>
        ''' <param name="strComp1"></param>
        ''' <param name="strComp2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CompareYearKs(ByVal strComp1 As String, ByVal strComp2 As String) As Boolean
            'strComp1＜strComp2だら、trueを返す。その以外、falseを返す。

            If strComp2.Length < 4 Then
                Return False
            End If
            If CInt(strComp1.Substring(0, 4)) < CInt(strComp2.Substring(0, 4)) Then
                Return True
            End If
            If CInt(strComp2.Substring(0, 4)) < CInt(strComp1.Substring(0, 4)) Then
                Return False
            End If
            If String.Equals(strComp1.Substring(4), UP_KS) Then
                Return True
            End If
            If String.Equals(strComp2.Substring(4), UP_KS) Then
                Return False
            End If


            Return True
        End Function
#End Region

#Region "造り方Spreadの部品費と型費をセット"
        ''' <summary>
        ''' 造り方Spreadの部品費と型費
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property TukurikataBuhinHiAndKataHi() As List(Of YosanInsuBuhinHiVo)
            Get
                Dim allList As New List(Of YosanInsuBuhinHiVo)
                For Each vo As YosanInsuBuhinHiVo In _MitoshiMetalList
                    Dim newVo As New YosanInsuBuhinHiVo
                    VoUtil.CopyProperties(vo, newVo)
                    allList.Add(newVo)
                Next
                For Each vo As YosanInsuBuhinHiVo In _MitoshiTrimList
                    Dim newVo As New YosanInsuBuhinHiVo
                    VoUtil.CopyProperties(vo, newVo)
                    allList.Add(newVo)
                Next

                Return allList
            End Get
        End Property

        ''' <summary>
        ''' 造り方Spreadの部品費と型費をセット
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpreadTukurikataBuhinHi(ByVal eventCode As String, ByVal updKansei As Boolean, ByVal updWb As Boolean)

            _YosanEventVo = EventDao.FindByPk(eventCode)

            Dim wbKeisu As Decimal = 1
            If StringUtil.IsNotEmpty(_YosanEventVo.YosanWbKeisu) Then
                wbKeisu = CDec(_YosanEventVo.YosanWbKeisu)
            End If



            '予算部品表選択情報を取得する。
            Dim _BuhinhyoVos As List(Of TYosanBuhinSelectVo) = YosanBuhinhyoList(eventCode)

            '予算部品表選択情報分の部品表を出力する。
            For Each vo As TYosanBuhinSelectVo In _BuhinhyoVos

                '見通し部品費
                _MitoshiMetalList = GetMitoshiList(eventCode, vo.BuhinhyoName)

                '造り方Spreadに部品費を設定
                For Each hiVo As YosanInsuBuhinHiVo In _MitoshiMetalList
                    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view_shukei.spdTukurikataKanseisha_Sheet1.RowCount - 2
                        '部品表名とパターン名
                        If String.Equals(hiVo.BuhinhyoName, m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)) AndAlso _
                           String.Equals(hiVo.PatternName, m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
                            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
                                m_view_shukei.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
                            End If
                            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
                                m_view_shukei.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
                            End If
                            Exit For
                        End If
                    Next
                    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view_shukei.spdTukurikataWBsha_Sheet1.RowCount - 2
                        '部品表名とパターン名
                        If String.Equals(hiVo.BuhinhyoName, m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)) AndAlso _
                           String.Equals(hiVo.PatternName, m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
                            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
                                m_view_shukei.spdTukurikataWBsha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi * wbKeisu
                            End If
                            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
                                m_view_shukei.spdTukurikataWBsha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
                            End If
                            Exit For
                        End If
                    Next
                Next

            Next






            ''見通しメタル部品費
            '_MitoshiMetalList = GetMitoshiList(UNIT_KBN_METAL_CODE)

            ''造り方Spreadに部品費を設定(メタル)
            'For Each hiVo As YosanInsuBuhinHiVo In _MitoshiMetalList
            '    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view_shukei.spdTukurikataKanseisha_Sheet1.RowCount - 2
            '        'ユニット区分とパターン名
            '        If String.Equals(UnitKbnName(hiVo.UnitKbn), m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) AndAlso _
            '           String.Equals(hiVo.PatternName, m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
            '                m_view_shukei.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
            '            End If
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
            '                m_view_shukei.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
            '            End If
            '            Exit For
            '        End If
            '    Next
            '    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view_shukei.spdTukurikataWBsha_Sheet1.RowCount - 2
            '        'ユニット区分とパターン名
            '        If String.Equals(UnitKbnName(hiVo.UnitKbn), m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) AndAlso _
            '           String.Equals(hiVo.PatternName, m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
            '                m_view_shukei.spdTukurikataWBsha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi * wbKeisu
            '            End If
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
            '                m_view_shukei.spdTukurikataWBsha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
            '            End If
            '            Exit For
            '        End If
            '    Next
            'Next

            ''見通しトリム部品費
            '_MitoshiTrimList = GetMitoshiList(UNIT_KBN_TRIM_CODE)

            ''造り方Spreadに部品費を設定(トリム)
            'For Each hiVo As YosanInsuBuhinHiVo In _MitoshiTrimList
            '    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view_shukei.spdTukurikataKanseisha_Sheet1.RowCount - 2
            '        'ユニット区分とパターン名→　部品表名とパターン名
            '        If String.Equals(hiVo.BuhinhyoName, m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)) AndAlso _
            '           String.Equals(hiVo.PatternName, m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
            '                m_view_shukei.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
            '            End If
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
            '                m_view_shukei.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
            '            End If
            '            Exit For
            '        End If
            '    Next
            '    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view_shukei.spdTukurikataWBsha_Sheet1.RowCount - 2
            '        'ユニット区分とパターン名→　部品表名とパターン名
            '        If String.Equals(hiVo.BuhinhyoName, m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)) AndAlso _
            '           String.Equals(hiVo.PatternName, m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
            '                m_view_shukei.spdTukurikataWBsha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi * wbKeisu
            '            End If
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
            '                m_view_shukei.spdTukurikataWBsha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
            '            End If
            '            Exit For
            '        End If
            '    Next
            'Next

        End Sub

        ''' <summary>
        ''' 部品費情報取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetMitoshiList(ByVal eventCode As String, ByVal unitKbn As String) As List(Of YosanInsuBuhinHiVo)

            Dim yosanBuhinEditVos As List(Of TYosanBuhinEditVo) = FindBuhinEditByUnitKbn(eventCode, unitKbn)

            Dim yosanBuhinEditInsuVos As New List(Of TYosanBuhinEditInsuVo)
            Dim yosanBuhinEditPatternVos As New List(Of TYosanBuhinEditPatternVo)
            Dim instlVosByHyojijun As New Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo))
            '予算部品編集情報を取得できた場合
            If yosanBuhinEditVos.Count = 0 Then
                Return New List(Of YosanInsuBuhinHiVo)
            End If
            yosanBuhinEditInsuVos = FindBuhinEditInsuByUnitKbn(eventCode, unitKbn)

            '予算部品編集パターン情報取得
            yosanBuhinEditPatternVos = FindBuhinEditPatternByUnitKbn(eventCode, unitKbn)

            '予算部品編集員数情報取得
            instlVosByHyojijun = MakeInstlVosByHyojijun(yosanBuhinEditInsuVos)

            '部品費を取得
            Dim buhinHiVos As List(Of YosanInsuBuhinHiVo) = GetBuhinHiVos(unitKbn, yosanBuhinEditVos, yosanBuhinEditPatternVos, instlVosByHyojijun)

            Return buhinHiVos

        End Function

        ''' <summary>
        ''' 部品表情報取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindBuhinEditByUnitKbn(ByVal eventCode As String, ByVal unitKbn As String) As List(Of TYosanBuhinEditVo)
            Return exportDao.FindYosanBuhinEdit(eventCode, unitKbn)
        End Function

        ''' <summary>
        ''' 部品表員数情報取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindBuhinEditInsuByUnitKbn(ByVal eventCode As String, ByVal unitKbn As String) As List(Of TYosanBuhinEditInsuVo)
            Return exportDao.FindYosanBuhinEditInsu(eventCode, unitKbn)
        End Function

        ''' <summary>
        ''' 部品表パターン情報取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindBuhinEditPatternByUnitKbn(ByVal eventCode As String, ByVal unitKbn As String) As List(Of TYosanBuhinEditPatternVo)
            Return exportDao.FindYosanBuhinEditPattern(eventCode, unitKbn)
        End Function

        ''' <summary>
        ''' 表示順NoによってINSTL情報を返すDictionaryを作成する
        ''' </summary>
        ''' <param name="editInstlVos">試作部品編集INSTL情報</param>
        ''' <returns>Dictionary</returns>
        ''' <remarks></remarks>
        Private Function MakeInstlVosByHyojijun(ByVal editInstlVos As List(Of TYosanBuhinEditInsuVo)) As Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo))

            Dim instlVosByHyojijun As New Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo))
            For Each vo As TYosanBuhinEditInsuVo In editInstlVos
                If Not instlVosByHyojijun.ContainsKey(vo.BuhinNoHyoujiJun) Then
                    instlVosByHyojijun.Add(vo.BuhinNoHyoujiJun, New List(Of TYosanBuhinEditInsuVo))
                End If
                instlVosByHyojijun(vo.BuhinNoHyoujiJun).Add(vo)
            Next
            Return instlVosByHyojijun
        End Function

        ''' <summary>
        ''' 部品費取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetBuhinHiVos(ByVal unitKbn As String, _
                                       ByVal buhinEditVos As List(Of TYosanBuhinEditVo), _
                                       ByVal buhinEditPatternVos As List(Of TYosanBuhinEditPatternVo), _
                                       ByVal buhinEditInsuVos As Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo))) As List(Of YosanInsuBuhinHiVo)

            Dim result As New List(Of YosanInsuBuhinHiVo)

            For Each patternVo As TYosanBuhinEditPatternVo In buhinEditPatternVos
                Dim patternHyojijun As Integer = patternVo.PatternHyoujiJun
                Dim patternName As String = patternVo.PatternName

                Dim hireiBuhinHiCount As Decimal = 0
                Dim koteiBuhinHiCount As Decimal = 0
                For Each buhinVo As TYosanBuhinEditVo In buhinEditVos
                    '比例費
                    Dim hireiBuhinHi As Decimal = 0
                    If buhinVo.YosanBuhinHiTokki.HasValue AndAlso buhinVo.YosanBuhinHiTokki.Value > 0 Then
                        '部品費（特記）>0
                        hireiBuhinHi = buhinVo.YosanBuhinHiTokki.Value
                    Else
                        If buhinVo.YosanBuhinHiRyosan.HasValue AndAlso buhinVo.YosanBuhinHiRyosan.Value > 0 AndAlso _
                           (Not buhinVo.YosanBuhinHiBuhinhyo.HasValue OrElse buhinVo.YosanBuhinHiBuhinhyo.Value = 0) Then
                            '部品費（量産）>0
                            hireiBuhinHi = buhinVo.YosanBuhinHiRyosan.Value
                        End If
                        If buhinVo.YosanBuhinHiBuhinhyo.HasValue AndAlso buhinVo.YosanBuhinHiBuhinhyo.Value > 0 AndAlso _
                           (Not buhinVo.YosanBuhinHiRyosan.HasValue OrElse buhinVo.YosanBuhinHiRyosan.Value = 0) Then
                            '部品費（部品表）>0
                            hireiBuhinHi = buhinVo.YosanBuhinHiBuhinhyo.Value
                        End If
                        If buhinVo.YosanBuhinHiRyosan.HasValue AndAlso buhinVo.YosanBuhinHiRyosan.Value > 0 AndAlso _
                            buhinVo.YosanBuhinHiBuhinhyo.HasValue AndAlso buhinVo.YosanBuhinHiBuhinhyo.Value > 0 Then
                            If buhinVo.YosanBuhinHiRyosan.Value > buhinVo.YosanBuhinHiBuhinhyo.Value Then
                                '部品費（量産）>部品費（部品表）
                                hireiBuhinHi = buhinVo.YosanBuhinHiRyosan.Value
                            Else
                                '部品費（部品表）>部品費（量産）
                                hireiBuhinHi = buhinVo.YosanBuhinHiBuhinhyo.Value
                            End If
                        End If
                    End If
                    '固定費
                    Dim koteiBuhinHi As Decimal = 0
                    If buhinVo.YosanKataHi.HasValue AndAlso buhinVo.YosanKataHi > 0 Then
                        koteiBuhinHi = buhinVo.YosanKataHi.Value
                    End If
                    If buhinVo.YosanJiguHi.HasValue AndAlso buhinVo.YosanJiguHi > 0 Then
                        koteiBuhinHi = koteiBuhinHi + buhinVo.YosanJiguHi.Value
                    End If

                    Dim insu As Integer = 0
                    If buhinEditInsuVos.ContainsKey(buhinVo.BuhinNoHyoujiJun) Then
                        Dim insuVos As List(Of TYosanBuhinEditInsuVo) = buhinEditInsuVos(buhinVo.BuhinNoHyoujiJun)
                        For Each insuVo As TYosanBuhinEditInsuVo In insuVos
                            If String.Equals(insuVo.PatternHyoujiJun, patternHyojijun) Then
                                '員数に値が有る(0、ブランク以外(-1含む))
                                If insuVo.InsuSuryo.HasValue AndAlso insuVo.InsuSuryo.Value > 0 Then
                                    insu = insu + insuVo.InsuSuryo.Value
                                    '固定費合計
                                    koteiBuhinHiCount = koteiBuhinHiCount + koteiBuhinHi
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                    '比例費合計
                    If insu > 0 AndAlso hireiBuhinHi > 0 Then
                        hireiBuhinHiCount = hireiBuhinHiCount + insu * hireiBuhinHi
                    End If
                Next

                Dim buhinHiVo As New YosanInsuBuhinHiVo

                buhinHiVo.BuhinhyoName = patternVo.BuhinhyoName
                'buhinHiVo.UnitKbn = unitKbn

                buhinHiVo.PatternName = patternName
                buhinHiVo.YosanBuhinHi = hireiBuhinHiCount
                buhinHiVo.HireiKoteiKbn = HIREI_KOTEI_KBN_HIREI
                'パターン毎の部品費
                result.Add(buhinHiVo)
                buhinHiVo = New YosanInsuBuhinHiVo

                buhinHiVo.BuhinhyoName = patternVo.BuhinhyoName
                'buhinHiVo.UnitKbn = unitKbn

                buhinHiVo.PatternName = patternName
                buhinHiVo.YosanBuhinHi = koteiBuhinHiCount
                buhinHiVo.HireiKoteiKbn = HIREI_KOTEI_KBN_KOTEI
                'パターン毎の部品費
                result.Add(buhinHiVo)
            Next

            Return result
        End Function
#End Region

#Region "集計値Spreadに表示値をセット"
        ''' <summary>
        ''' 集計値Spreadに表示値をセット
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSpreadZaimujisekiData(ByVal eventCode As String, ByVal isInitShow As Boolean)

            _YosanEventVo = EventDao.FindByPk(eventCode)

            '比例費と固定費の見通し部品費（メタル／トリム）を取得
            GetMitoshiMetalAndTrimData()

            '比例費と固定費の財務実績を取得
            _ZaimujisekiList = editDao.FindYosanZaimuJisekiBy(_YosanEventVo.YosanCode)

            '比例費の見通し部品費以外を取得
            _EventMitoshiList = editDao.FindYosanEventMitoshiBy(eventCode)

            '比例費の見通し移管車＆生産部実績を取得
            _SeisanbuJisekiList = New Dictionary(Of String, Decimal)
            If Not isInitShow Then
                GetMitoshiSeisanbuJisekiData()
            End If

            '発注実績
            GetHatchuJisekiData()

            '集計値Spreadに比例費と固定費の見通しの部品費を設定
            For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To m_view_shukei.spdZaimuJiseki_Sheet1.ColumnCount - 2
                '合計列以外
                If Not ((columnIndex - SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                    Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view_shukei.spdZaimuJiseki_Sheet1.GetValue(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                       m_view_shukei.spdZaimuJiseki_Sheet1.GetValue(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                       CInt(m_view_shukei.spdZaimuJiseki_Sheet1.GetValue(SPD_ZAIMUJISEKI_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                    '比例費見通しのメタル部品費
                    For Each key As String In _HireiMetalHiList.Keys
                        If String.Equals(yearMonth, key) Then
                            m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, columnIndex).Value = _HireiMetalHiList(key)
                            Exit For
                        End If
                    Next
                    '比例費見通しのトリム部品費
                    For Each key As String In _HireiTrimHiList.Keys
                        If String.Equals(yearMonth, key) Then
                            m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX, columnIndex).Value = _HireiTrimHiList(key)
                            Exit For
                        End If
                    Next
                    '固定費見通しのメタル部品費
                    For Each key As String In _KoteiMetalHiList.Keys
                        If String.Equals(yearMonth, key) Then
                            m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, columnIndex).Value = _KoteiMetalHiList(key)
                            Exit For
                        End If
                    Next
                    '固定費見通しのトリム部品費
                    For Each key As String In _KoteiTrimHiList.Keys
                        If String.Equals(yearMonth, key) Then
                            m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX, columnIndex).Value = _KoteiTrimHiList(key)
                            Exit For
                        End If
                    Next
                    '財務実績
                    For Each zaimuVo As TYosanZaimuJisekiVo In _ZaimujisekiList
                        If String.Equals(yearMonth, zaimuVo.YosanZaimuJisekiYyyyMm) Then
                            '比例費の場合
                            If IsHirei(zaimuVo.YosanZaimuHireiKoteiKbn) Then
                                'メタル部品費
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_METAL) Then
                                    m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                                '鋼板材料
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU) Then
                                    m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                                '輸送費
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_YUSOU) Then
                                    m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                                '移管車＆生産部実績
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_SEISAN) Then
                                    m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                                'トリム部品費
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_TRIM) Then
                                    m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                            End If

                            '固定費の場合
                            If IsKotei(zaimuVo.YosanZaimuHireiKoteiKbn) Then
                                'メタル部品費
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_KOTEI_METAL) Then
                                    m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                                'トリム部品費
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_KOTEI_TRIM) Then
                                    m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                            End If
                        End If
                    Next
                    '見通し
                    For Each mitoshiVo As TYosanEventMitoshiVo In _EventMitoshiList
                        If String.Equals(yearMonth, mitoshiVo.YosanEventMitoshiYyyyMm) Then
                            '鋼板材料
                            If String.Equals(mitoshiVo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU) Then
                                m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX, columnIndex).Value = mitoshiVo.YosanEventMitoshiInputQty
                            End If
                            '輸送費
                            If String.Equals(mitoshiVo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_YUSOU) Then
                                m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX, columnIndex).Value = mitoshiVo.YosanEventMitoshiInputQty
                            End If
                            '移管車＆生産部実績
                            If String.Equals(mitoshiVo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_SEISAN) Then
                                m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX, columnIndex).Value = mitoshiVo.YosanEventMitoshiInputQty
                            End If
                        End If
                    Next
                    '初期表示以外の場合、移管車＆生産部実績値は金材SPREADのデータを積上げる
                    If Not isInitShow Then
                        For Each key As String In _SeisanbuJisekiList.Keys
                            If String.Equals(yearMonth, key) Then
                                m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX, columnIndex).Value = _SeisanbuJisekiList(key)
                                Exit For
                            End If
                        Next
                    End If
                    '発注実績
                    For Each unitKbn As String In _HireiHatchuJisekiList.Keys
                        For Each keyYearMonth As String In _HireiHatchuJisekiList(unitKbn).Keys
                            If String.Equals(yearMonth, keyYearMonth) Then
                                If IsUnitMetal(unitKbn) Then
                                    m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_METAL_ROWS_INDEX, columnIndex).Value = _HireiHatchuJisekiList(unitKbn)(keyYearMonth)
                                Else
                                    m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_TRIM_ROWS_INDEX, columnIndex).Value = _HireiHatchuJisekiList(unitKbn)(keyYearMonth)
                                End If
                                Exit For
                            End If
                        Next
                    Next
                    For Each unitKbn As String In _KoteiHatchuJisekiList.Keys
                        For Each keyYearMonth As String In _KoteiHatchuJisekiList(unitKbn).Keys
                            If String.Equals(yearMonth, keyYearMonth) Then
                                If IsUnitMetal(unitKbn) Then
                                    m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_METAL_ROWS_INDEX, columnIndex).Value = _KoteiHatchuJisekiList(unitKbn)(keyYearMonth)
                                Else
                                    m_view_shukei.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_TRIM_ROWS_INDEX, columnIndex).Value = _KoteiHatchuJisekiList(unitKbn)(keyYearMonth)
                                End If
                                Exit For
                            End If
                        Next
                    Next
                End If
            Next

        End Sub

        ''' <summary>
        ''' 比例費と固定費の見通し部品費（メタル／トリム）を取得
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub GetMitoshiMetalAndTrimData()
            '比例費
            _HireiMetalHiList = New Dictionary(Of String, Decimal)
            _HireiTrimHiList = New Dictionary(Of String, Decimal)
            '完成車Spreadの部品費を集計
            If m_view_shukei.spdTukurikataKanseisha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To m_view_shukei.spdTukurikataKanseisha_Sheet1.ColumnCount - 2
                    Dim hireiMetalBuhinHi As Decimal = 0
                    Dim hireiTrimBuhinHi As Decimal = 0
                    '合計列以外
                    If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then

                        Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                           m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                           CInt(m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                        For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view_shukei.spdTukurikataKanseisha_Sheet1.RowCount - 2

                            'ユニット区分'
                            Dim unitkbn As String = m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)
                            '部品費'
                            Dim buhinHi As String = m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX)
                            '台数'
                            Dim daisu As String = m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, columnIndex)

                            'メタルの場合
                            If String.Equals(UNIT_KBN_METAL_CODE, unitkbn) Then
                                'If String.Equals(UNIT_KBN_METAL_NAME, unitkbn) Then
                                If StringUtil.IsNotEmpty(daisu) AndAlso _
                                   StringUtil.IsNotEmpty(buhinHi) Then
                                    hireiMetalBuhinHi = hireiMetalBuhinHi + CInt(daisu) * CDec(buhinHi)
                                End If
                            End If
                            'トリムの場合
                            If String.Equals(UNIT_KBN_TRIM_CODE, unitkbn) Then
                                'If String.Equals(UNIT_KBN_TRIM_NAME, unitkbn) Then
                                If StringUtil.IsNotEmpty(daisu) AndAlso _
                                   StringUtil.IsNotEmpty(buhinHi) Then
                                    hireiTrimBuhinHi = hireiTrimBuhinHi + CInt(daisu) * CDec(buhinHi)
                                End If
                            End If
                        Next

                        _HireiMetalHiList.Add(yearMonth, hireiMetalBuhinHi)
                        _HireiTrimHiList.Add(yearMonth, hireiTrimBuhinHi)
                    End If
                Next
            End If
            'ﾎﾜｲﾄﾎﾞﾃﾞｨSpreadの部品費を集計
            If m_view_shukei.spdTukurikataWBsha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To m_view_shukei.spdTukurikataWBsha_Sheet1.ColumnCount - 2
                    Dim hireiMetalBuhinHi As Decimal = 0
                    Dim hireiTrimBuhinHi As Decimal = 0
                    '合計列以外
                    If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                        Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                           m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                           CInt(m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                        For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view_shukei.spdTukurikataWBsha_Sheet1.RowCount - 2
                            Dim unitkbn As String = m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)
                            Dim buhinHi As String = m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX)
                            Dim daisu As String = m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, columnIndex)
                            'メタルの場合
                            If String.Equals(UNIT_KBN_METAL_CODE, unitkbn) Then
                                'If String.Equals(UNIT_KBN_METAL_NAME, unitkbn) Then
                                If StringUtil.IsNotEmpty(daisu) AndAlso _
                                   StringUtil.IsNotEmpty(buhinHi) Then
                                    hireiMetalBuhinHi = hireiMetalBuhinHi + CInt(daisu) * CDec(buhinHi)
                                End If
                            End If
                            'トリムの場合
                            If String.Equals(UNIT_KBN_TRIM_CODE, unitkbn) Then
                                'If String.Equals(UNIT_KBN_TRIM_NAME, unitkbn) Then
                                If StringUtil.IsNotEmpty(daisu) AndAlso _
                                   StringUtil.IsNotEmpty(buhinHi) Then
                                    hireiTrimBuhinHi = hireiTrimBuhinHi + CInt(daisu) * CDec(buhinHi)
                                End If
                            End If
                        Next

                        'メタル部品費
                        If _HireiMetalHiList.ContainsKey(yearMonth) Then
                            '該当年月の集計値があれば、部品費を積上げ
                            _HireiMetalHiList(yearMonth) = _HireiMetalHiList(yearMonth) + hireiMetalBuhinHi
                        Else
                            '該当年月の集計値がなければ、部品費情報に追加
                            _HireiMetalHiList.Add(yearMonth, hireiMetalBuhinHi)
                        End If
                        'トリム部品費
                        If _HireiTrimHiList.ContainsKey(yearMonth) Then
                            '該当年月の集計値があれば、部品費を積上げ
                            _HireiTrimHiList(yearMonth) = _HireiTrimHiList(yearMonth) + hireiTrimBuhinHi
                        Else
                            '該当年月の集計値がなければ、部品費情報に追加
                            _HireiTrimHiList.Add(yearMonth, hireiTrimBuhinHi)
                        End If
                    End If
                Next
            End If

            '固定費
            _KoteiMetalHiList = New Dictionary(Of String, Decimal)
            _KoteiTrimHiList = New Dictionary(Of String, Decimal)
            Dim metalYearMonth As String = "999999"
            Dim metalBuhinHi As Decimal = 0
            Dim trimYearMonth As String = "999999"
            Dim trimBuhinHi As Decimal = 0
            '完成車Spreadの部品費を集計
            If m_view_shukei.spdTukurikataKanseisha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view_shukei.spdTukurikataKanseisha_Sheet1.RowCount - 2
                    For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To m_view_shukei.spdTukurikataKanseisha_Sheet1.ColumnCount - 2
                        '合計列以外
                        If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                            Dim daisu As String = m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, columnIndex)
                            Dim buhinHi As String = m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                            If StringUtil.IsNotEmpty(daisu) AndAlso CInt(daisu) > 0 AndAlso _
                               StringUtil.IsNotEmpty(buhinHi) AndAlso CDec(buhinHi) > 0 Then
                                Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                                   m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                                   CInt(m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                                'メタルの場合
                                If String.Equals(UNIT_KBN_METAL_CODE, m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    'If String.Equals(UNIT_KBN_METAL_NAME, m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    If CInt(yearMonth) < CInt(metalYearMonth) Then
                                        '最小年月を取得
                                        metalYearMonth = yearMonth
                                        metalBuhinHi = m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                                    End If
                                End If
                                'トリムの場合
                                If String.Equals(UNIT_KBN_TRIM_CODE, m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    'If String.Equals(UNIT_KBN_TRIM_NAME, m_view_shukei.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    If CInt(yearMonth) < CInt(trimYearMonth) Then
                                        '最小年月を取得
                                        trimYearMonth = yearMonth
                                        trimBuhinHi = CDec(buhinHi)
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next
            End If
            'ﾎﾜｲﾄﾎﾞﾃﾞｨSpreadの部品費を集計
            If m_view_shukei.spdTukurikataWBsha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view_shukei.spdTukurikataWBsha_Sheet1.RowCount - 2
                    For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To m_view_shukei.spdTukurikataWBsha_Sheet1.ColumnCount - 2
                        '合計列以外
                        If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                            Dim daisu As String = m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, columnIndex)
                            Dim buhinHi As String = m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                            If StringUtil.IsNotEmpty(daisu) AndAlso CInt(daisu) > 0 AndAlso _
                               StringUtil.IsNotEmpty(buhinHi) AndAlso CDec(buhinHi) > 0 Then
                                Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                                   m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                                   CInt(m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                                'メタルの場合
                                If String.Equals(UNIT_KBN_METAL_CODE, m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    'If String.Equals(UNIT_KBN_METAL_NAME, m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    If CInt(yearMonth) < CInt(metalYearMonth) Then
                                        '最小年月を取得
                                        metalYearMonth = yearMonth
                                        metalBuhinHi = m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                                    End If
                                End If
                                'トリムの場合
                                If String.Equals(UNIT_KBN_TRIM_CODE, m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    'If String.Equals(UNIT_KBN_TRIM_NAME, m_view_shukei.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    If CInt(yearMonth) < CInt(trimYearMonth) Then
                                        '最小年月を取得
                                        trimYearMonth = yearMonth
                                        trimBuhinHi = CDec(buhinHi)
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next
            End If
            _KoteiMetalHiList.Add(metalYearMonth, metalBuhinHi)
            _KoteiTrimHiList.Add(trimYearMonth, trimBuhinHi)
        End Sub

        ''' <summary>
        ''' 比例費の見通し移管車＆生産部実績を取得
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub GetMitoshiSeisanbuJisekiData()
            If m_view_shukei.spdKanazai_Sheet1.ColumnCount - SPD_KANAZAI_DEFAULT_COLUMN_COUNT > 0 Then
                For columnIndex As Integer = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX To m_view_shukei.spdKanazai_Sheet1.ColumnCount - 2
                    Dim hiCount As Decimal = 0
                    '合計列以外
                    If Not ((columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                        Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view_shukei.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                           m_view_shukei.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                           CInt(m_view_shukei.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                        For rowIndex As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX To m_view_shukei.spdKanazai_Sheet1.RowCount - 2
                            Dim tanka As String = m_view_shukei.spdKanazai_Sheet1.GetValue(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX)
                            Dim daisu As String = m_view_shukei.spdKanazai_Sheet1.GetValue(rowIndex, columnIndex)
                            If StringUtil.IsNotEmpty(daisu) AndAlso _
                               StringUtil.IsNotEmpty(tanka) Then
                                hiCount = hiCount + CInt(daisu) * CDec(tanka)
                            End If
                        Next

                        _SeisanbuJisekiList.Add(yearMonth, hiCount)
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' 発注実績を取得
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub GetHatchuJisekiData()
            _HireiHatchuJisekiList = New Dictionary(Of String, Dictionary(Of String, Decimal))
            _KoteiHatchuJisekiList = New Dictionary(Of String, Dictionary(Of String, Decimal))
            For columnIndex As Integer = SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN To m_view_shukei.spdSeisakuDaisu_Sheet1.ColumnCount - 1 Step 2
                Dim kojishireiNo As String = m_view_shukei.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex)
                Dim unitKbn As String = UnitKbnCode(m_view_shukei.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex))
                'Dim daisuCount As Integer = 0
                ''完成車とＷＢ車の台数合計
                'If StringUtil.IsNotEmpty(m_view_shukei.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex)) Then
                '    daisuCount = daisuCount + CInt(m_view_shukei.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex))
                'End If
                'If StringUtil.IsNotEmpty(m_view_shukei.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex)) Then
                '    daisuCount = daisuCount + CInt(m_view_shukei.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex))
                'End If
                If StringUtil.IsNotEmpty(kojishireiNo) Then
                    Dim vos As List(Of SeisakuAsKounyuYosanVo) = editDao.FindSeisakuAsKounyuYosanBy(kojishireiNo, unitKbn)
                    If vos.Count > 0 Then
                        'データを取得できた場合
                        If Not _HireiHatchuJisekiList.ContainsKey(unitKbn) Then
                            _HireiHatchuJisekiList.Add(unitKbn, New Dictionary(Of String, Decimal))
                        End If
                        If Not _KoteiHatchuJisekiList.ContainsKey(unitKbn) Then
                            _KoteiHatchuJisekiList.Add(unitKbn, New Dictionary(Of String, Decimal))
                        End If
                    End If
                    For Each Vo As SeisakuAsKounyuYosanVo In vos
                        If Vo.KounyuYosanYyyyMm = 0 Then
                            Continue For
                        End If
                        Dim yearMonth As String = Vo.KounyuYosanYyyyMm.ToString.Substring(0, 6)
                        If Not _HireiHatchuJisekiList(unitKbn).ContainsKey(yearMonth) Then
                            _HireiHatchuJisekiList(unitKbn).Add(yearMonth, 0)
                        End If
                        If Not _KoteiHatchuJisekiList(unitKbn).ContainsKey(yearMonth) Then
                            _KoteiHatchuJisekiList(unitKbn).Add(yearMonth, 0)
                        End If
                        '毎月に部品費を集計
                        If Vo.BuhinHireiHiCount.HasValue Then
                            _HireiHatchuJisekiList(unitKbn)(yearMonth) = _HireiHatchuJisekiList(unitKbn)(yearMonth) + Vo.BuhinHireiHiCount '* daisuCount
                        End If
                        If Vo.BuhinKoteiHiCount.HasValue Then
                            _KoteiHatchuJisekiList(unitKbn)(yearMonth) = _KoteiHatchuJisekiList(unitKbn)(yearMonth) + Vo.BuhinKoteiHiCount
                        End If
                    Next
                End If
            Next
        End Sub
#End Region

#Region "集計値合計"
        ''' <summary>
        ''' 集計値Spread合計
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CalcZaimuJiseki()

            '月分布列の合計
            For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To m_view_shukei.spdZaimuJiseki_Sheet1.ColumnCount - 2
                Dim columnCount As Decimal = 0
                For rowIndex As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To m_view_shukei.spdZaimuJiseki_Sheet1.RowCount - 1
                    If rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_HATCHU_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_MITOSHI_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_HATCHU_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX Then
                        '合計
                        m_view_shukei.spdZaimuJiseki_Sheet1.Cells(rowIndex, columnIndex).Value = columnCount

                        columnCount = 0
                        Continue For
                    End If
                    If StringUtil.IsNotEmpty(m_view_shukei.spdZaimuJiseki_Sheet1.GetValue(rowIndex, columnIndex)) Then
                        columnCount = columnCount + m_view_shukei.spdZaimuJiseki_Sheet1.GetValue(rowIndex, columnIndex)
                    End If
                Next
            Next

            '行の合計
            For rowIndex As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To m_view_shukei.spdZaimuJiseki_Sheet1.RowCount - 1
                Dim RowCount As Decimal = 0
                Dim monthCount As Decimal = 0
                For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To m_view_shukei.spdZaimuJiseki_Sheet1.ColumnCount - 2
                    '合計列以外
                    If Not ((columnIndex - SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                        If StringUtil.IsNotEmpty(m_view_shukei.spdZaimuJiseki_Sheet1.GetValue(rowIndex, columnIndex)) Then
                            monthCount = monthCount + m_view_shukei.spdZaimuJiseki_Sheet1.GetValue(rowIndex, columnIndex)
                        End If
                    Else
                        m_view_shukei.spdZaimuJiseki_Sheet1.Cells(rowIndex, columnIndex).Value = monthCount
                        RowCount = RowCount + monthCount

                        monthCount = 0
                    End If
                Next
                '合計
                Dim rowCountCol As Integer = m_view_shukei.spdZaimuJiseki_Sheet1.ColumnCount - 1
                m_view_shukei.spdZaimuJiseki_Sheet1.Cells(rowIndex, rowCountCol).Value = RowCount
            Next

        End Sub
#End Region

#End Region








#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="f"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal f As FrmYosanEventListExcel, ByVal LoginInfo As LoginInfo, ByVal fs As frmYosanEventExcelShorichu)
            m_view = f
            m_view_shukei = fs

            aLoginInfo = LoginInfo
            aSystemDate = New ShisakuDate
            ExclusiveEventDao = New TYosanExclusiveControlEventDaoImpl
            EventDao = New TYosanEventDaoImpl
            '集計値EXCEL用として定義
            editDao = New YosanshoEditDaoImpl
            exportDao = New YosanshoExportDaoImpl
        End Sub
#End Region

#Region "ヘッダー部初期化 "
        ''' <summary>
        '''ヘッダー部を初期化する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeHeader()
            'ヘッダー部分の初期設定
            ShisakuFormUtil.Initialize(m_view_shukei)
            ShisakuFormUtil.setTitleVersion(m_view)
            ShisakuFormUtil.SetIdAndBuka(m_view.LblLoginUserId, m_view.LblLoginBukaName)
            ShisakuFormUtil.SetDateTimeNow(m_view.LblDateNow, m_view.LblTimeNow)
            '画面のPG-IDが表示されます。
            m_view.LblCurrPGId.Text = "PG-ID :" + "YOSAN EVENT LIST"

            'コンボボックス初期設定
            ShisakuFormUtil.SettingDefaultProperty(m_view.cmbKbn)
            ShisakuFormUtil.SettingDefaultProperty(m_view.cmbKaihatsuFugo)
            ShisakuFormUtil.SettingDefaultProperty(m_view.cmbEvent)
            ShisakuFormUtil.SettingDefaultProperty(m_view.cmbYosanCode)
            ShisakuFormUtil.SettingDefaultProperty(m_view.cmbKikanFrom)
            ShisakuFormUtil.SettingDefaultProperty(m_view.cmbKikanTo)
        End Sub
#End Region

#Region "区分のコンボボックスを作成"

        ''' <summary>
        ''' 区分のコンボボックスを作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetKbnCombo()
            Try
                '区分コンボボックスに値を追加
                Dim dtKbn As DataTable = GetKbnData()
                Dim vos As New List(Of LabelValueVo)
                For idx As Integer = 0 To dtKbn.Rows.Count - 1
                    Dim vo As New LabelValueVo
                    vo.Label = dtKbn.Rows(idx).Item(0)
                    vo.Value = vo.Label
                    vos.Add(vo)
                Next
                FormUtil.BindLabelValuesToComboBox(m_view.cmbKbn, vos, True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' T_YOSAN_EVENTより区分を設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetKbnData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(GetKbnSql, dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 区分を取得するSQL
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetKbnSql() As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("      YOSAN_EVENT_KBN_NAME ")
                .AppendLine("FROM  T_YOSAN_EVENT ")
                .AppendLine("GROUP BY ")
                .AppendLine("      YOSAN_EVENT_KBN_NAME ")
                .AppendLine("ORDER BY ")
                .AppendLine("      YOSAN_EVENT_KBN_NAME ")
            End With

            Return sql.ToString()
        End Function

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
        ''' 開発符号を設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetKaihatsuFugoData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(GetKaihatufugouSql, dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 開発符号を取得するSQL
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetKaihatufugouSql() As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("      YOSAN_KAIHATSU_FUGO ")
                .AppendLine("FROM  T_YOSAN_EVENT ")
                .AppendLine("GROUP BY ")
                .AppendLine("      YOSAN_KAIHATSU_FUGO ")
                .AppendLine("ORDER BY ")
                .AppendLine("      YOSAN_KAIHATSU_FUGO ")
            End With

            Return sql.ToString()
        End Function

#End Region

#Region "イベントのコンボボックスを作成"

        ''' <summary>
        ''' イベントのコンボボックスを作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetEventCombo()
            Try
                'イベントコンボボックスに値を追加
                Dim dtEvent As DataTable = GetEventData()
                Dim vos As New List(Of LabelValueVo)
                For idx As Integer = 0 To dtEvent.Rows.Count - 1
                    Dim vo As New LabelValueVo
                    vo.Label = dtEvent.Rows(idx).Item(0)
                    vo.Value = vo.Label
                    vos.Add(vo)
                Next
                FormUtil.BindLabelValuesToComboBox(m_view.cmbEvent, vos, True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' イベントを設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetEventData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(GeteventSql, dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' イベントを取得するSQL
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetEventSql() As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("      YOSAN_EVENT ")
                .AppendLine("FROM  T_YOSAN_EVENT ")
                .AppendLine("GROUP BY ")
                .AppendLine("      YOSAN_EVENT ")
                .AppendLine("ORDER BY ")
                .AppendLine("      YOSAN_EVENT ")
            End With

            Return sql.ToString()
        End Function

#End Region

#Region "予算コードのコンボボックスを作成"

        ''' <summary>
        ''' 予算コードのコンボボックスを作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetYosanCodeCombo()
            Try
                '予算コードコンボボックスに値を追加
                Dim dtYosanCode As DataTable = GetYosanCodeData()
                Dim vos As New List(Of LabelValueVo)
                For idx As Integer = 0 To dtYosanCode.Rows.Count - 1
                    Dim vo As New LabelValueVo
                    vo.Label = dtYosanCode.Rows(idx).Item(0)
                    vo.Value = vo.Label
                    vos.Add(vo)
                Next
                FormUtil.BindLabelValuesToComboBox(m_view.cmbYosanCode, vos, True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 予算コードを設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetYosanCodeData() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(GetYosanCodeSql, dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 予算コードを取得するSQL
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetYosanCodeSql() As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT ")
                .AppendLine("      YOSAN_CODE ")
                .AppendLine("FROM  T_YOSAN_EVENT ")
                .AppendLine("GROUP BY ")
                .AppendLine("      YOSAN_CODE ")
                .AppendLine("ORDER BY ")
                .AppendLine("      YOSAN_CODE ")
            End With

            Return sql.ToString()
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

                        'From
                        FormUtil.BindLabelValuesToComboBox(m_view.cmbKikanFrom, vos, True)
                        'To
                        FormUtil.BindLabelValuesToComboBox(m_view.cmbKikanTo, vos, True)

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

#Region "スプレッド初期化"
        ''' <summary>
        ''' スプレッドを初期化する      
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeSpread()
            'Spreadの初期化
            SpreadUtil.Initialize(m_view.spdParts)

            'SPREADの列のタグ値を設定する
            SetSpdColTag()
            'SPREADの列のデータフィールドを設定する
            SetSpdDataField()
            'SPREADのデータソースを設定する
            SetSpreadData(True)
            'SPREADの列のセルの水平方向の配置を再設定する
            SetSpdColPro()
            'SPREADの非表示列を設定する
            m_view.spdParts_Sheet1.Columns(TAG_YOSAN_EVENT_CODE).Visible = False
            'SPREADの列をロックする
            For index As Integer = 1 To m_view.spdParts_Sheet1.Columns.Count - 1
                m_view.spdParts_Sheet1.Columns(index).Locked = True
            Next
        End Sub

        ''' <summary>
        ''' SPREADの列のタグ値を設定する        
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSpdColTag()
            Dim index As Integer = 0
            'チェック
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_CHECK
            '状態
            m_view.spdParts_Sheet1.Columns(EzUtil.Increment(index)).Tag = TAG_JYOUTAI
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
            'チェック
            m_view.spdParts_Sheet1.Columns(TAG_CHECK).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            '状態
            m_view.spdParts_Sheet1.Columns(TAG_JYOUTAI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
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
        ''' <param name="initFlg"></param>
        ''' <remarks></remarks>
        Public Sub SetSpreadData(Optional ByVal initFlg As Boolean = False)
            Dim dtSrpead As DataTable
            If initFlg Then
                dtSrpead = GetSpreadList()
            Else
                dtSrpead = GetSpreadList(m_view.cmbKbn.Text, _
                                         m_view.cmbKaihatsuFugo.Text, _
                                         m_view.cmbEvent.Text, _
                                         m_view.cmbYosanCode.Text, _
                                         m_view.cmbKikanFrom.Text, _
                                         m_view.cmbKikanTo.Text)
            End If

            If m_view.spdParts_Sheet1.RowCount > 0 Then
                m_view.spdParts_Sheet1.RemoveRows(0, m_view.spdParts_Sheet1.RowCount)
            End If

            m_view.spdParts_Sheet1.RowCount = dtSrpead.Rows.Count

            Dim chkType As New CheckBoxCellType()
            chkType.Caption = ""

            For index = 0 To dtSrpead.Rows.Count - 1
                '集計チェックボックス
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_CHECK)).CellType = chkType
                m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_CHECK)).Value = True
                '状態
                If StringUtil.Equals(dtSrpead.Rows(index)(DB_YOSAN_STATUS), STATUS_00) Then
                    m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_JYOUTAI)).Value = STATUS_00_NAME
                Else
                    m_view.spdParts_Sheet1.Cells(index, GetTagIdx(m_view.spdParts_Sheet1, TAG_JYOUTAI)).Value = STATUS_01_NAME
                End If
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
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSpreadList() As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetYosanEventListExcel(), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 予算イベント情報からデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetYosanEventListExcel() As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT * ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT ")
                .AppendLine("ORDER BY YOSAN_EVENT_CODE")
            End With
            Return sql.ToString()
        End Function

        ''' <summary>
        ''' スプレッドのデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSpreadList(ByVal kbn As String, _
                                       ByVal kaihatsuFugo As String, _
                                       ByVal eventPhase As String, _
                                       ByVal yosanCode As String, _
                                       ByVal kikanFrom As String, _
                                       ByVal kikanTo As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_EBOM))
                db.Open()
                db.Fill(GetYosanEventListExcel(kbn, kaihatsuFugo, eventPhase, yosanCode, kikanFrom, kikanTo), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 予算イベント情報からデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetYosanEventListExcel(ByVal kbn As String, _
                                       ByVal kaihatsuFugo As String, _
                                       ByVal eventPhase As String, _
                                       ByVal yosanCode As String, _
                                       ByVal kikanFrom As String, _
                                       ByVal kikanTo As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT T.* ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT T, ")
                .AppendLine("(SELECT YOSAN_EVENT_CODE, ")
                .AppendLine("CONVERT(VARCHAR,YOSAN_KIKAN_FROM_YYYY)+''+CONVERT(VARCHAR,CASE WHEN YOSAN_KIKAN_FROM_KS = '上期' THEN 0 ELSE 1 END) AS FORM_KS, ")
                .AppendLine("CONVERT(VARCHAR,YOSAN_KIKAN_TO_YYYY)+''+CONVERT(VARCHAR,CASE WHEN YOSAN_KIKAN_TO_KS = '上期' THEN 0 ELSE 1 END) AS END_KS ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT ")
                .AppendLine(") A ")
                .AppendLine("WHERE T.YOSAN_EVENT_CODE = A.YOSAN_EVENT_CODE ")
                If StringUtil.IsNotEmpty(kbn) Then
                    .AppendLine("AND T.YOSAN_EVENT_KBN_NAME = '" & kbn & "' ")
                End If
                If StringUtil.IsNotEmpty(kaihatsuFugo) Then
                    .AppendLine("AND T.YOSAN_KAIHATSU_FUGO = '" & kaihatsuFugo & "' ")
                End If
                If StringUtil.IsNotEmpty(eventPhase) Then
                    .AppendLine("AND T.YOSAN_EVENT = '" & eventPhase & "' ")
                End If
                If StringUtil.IsNotEmpty(yosanCode) Then
                    .AppendLine("AND T.YOSAN_CODE = '" & yosanCode & "' ")
                End If
                If StringUtil.IsNotEmpty(kikanFrom) Then
                    If kikanFrom.Length >= 6 Then
                        Dim strYyyy As String = kikanFrom.Substring(0, 4)
                        Dim strKs As String = kikanFrom.Substring(4, 2)
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
                If StringUtil.IsNotEmpty(kikanTo) Then
                    If kikanFrom.Length >= 6 Then
                        Dim strYyyy As String = kikanFrom.Substring(0, 4)
                        Dim strKs As String = kikanFrom.Substring(4, 2)
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
            '0
            lstExcelTitle.Add("状態")
            lstExcelItem.Add("JYOUTAI")
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
            Dim horiLeft() As String = New String() {lstExcelItem(8), lstExcelItem(0), lstExcelItem(1), lstExcelItem(2), lstExcelItem(3), _
                                                     lstExcelItem(4), lstExcelItem(5), lstExcelItem(6), lstExcelItem(7), _
                                                     lstExcelItem(10), lstExcelItem(12), lstExcelItem(13), lstExcelItem(14), lstExcelItem(15)}
            Dim horiRight() As String = New String() {lstExcelItem(9), lstExcelItem(11)}
            Dim spExcelCom = New SpreadCommon(spdExcel)
            For i As Integer = 0 To horiLeft.Length - 1
                spExcelCom.GetColFromTag(horiLeft(i)).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            Next
            For i As Integer = 0 To horiRight.Length - 1
                spExcelCom.GetColFromTag(horiRight(i)).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            Next
            '記号列は非表示
            spdExcel.Sheets(0).Columns(lstExcelItem(7)).Visible = False
            spdExcel.Sheets(0).Columns(lstExcelItem(10)).Visible = False
        End Sub

#End Region

#Region "区分を変更したら処理"

        ''' <summary>
        ''' 開発符号のコンボボックスを作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetKbnKaihatsuFugoCombo(ByVal kbn As String)
            Try
                'イベントコンボボックスに値を追加
                Dim dtKaihatsuFugo As DataTable = GetKbnKaihatsuFugoData(kbn)
                Dim vos As New List(Of LabelValueVo)
                For idx As Integer = 0 To dtKaihatsuFugo.Rows.Count - 1
                    Dim vo As New LabelValueVo
                    vo.Label = dtKaihatsuFugo.Rows(idx).Item(0)
                    vo.Value = vo.Label
                    vos.Add(vo)
                Next
                FormUtil.BindLabelValuesToComboBox(m_view.cmbKaihatsuFugo, vos, True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 予算書イベント情報より開発符号を設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetKbnKaihatsuFugoData(ByVal kbn As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(GetKbnKaihatsuFugoSql(kbn), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 予算書イベント情報からデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetKbnKaihatsuFugoSql(ByVal kbn As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT DISTINCT YOSAN_KAIHATSU_FUGO ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT ")
                .AppendLine("WHERE YOSAN_EVENT_KBN_NAME = '" & kbn & "' ")
                .AppendLine("ORDER BY YOSAN_KAIHATSU_FUGO")
            End With
            Return sql.ToString()
        End Function

#End Region

#Region "開発符号を変更したら処理"

        ''' <summary>
        ''' イベントのコンボボックスを作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetKaihatsuFugoEventCombo(ByVal kaihatsufugo As String)
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
        ''' 予算書イベント情報よりイベントを設定する。
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
        ''' 予算書イベント情報からデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetEventPhaseNameSql(ByVal kaihatsufugo As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT DISTINCT YOSAN_EVENT ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT ")
                .AppendLine("WHERE YOSAN_KAIHATSU_FUGO = '" & kaihatsufugo & "' ")
                .AppendLine("ORDER BY YOSAN_EVENT")
            End With
            Return sql.ToString()
        End Function

#End Region

#Region "イベントを変更したら処理"

        ''' <summary>
        ''' 予算コードのコンボボックスを作成
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetEventYosanCombo(ByVal eventName As String)
            Try
                'イベントコンボボックスに値を追加
                Dim dtEventYosan As DataTable = GetEventYosanData(eventName)
                Dim vos As New List(Of LabelValueVo)
                For idx As Integer = 0 To dtEventYosan.Rows.Count - 1
                    Dim vo As New LabelValueVo
                    vo.Label = dtEventYosan.Rows(idx).Item(0)
                    vo.Value = vo.Label
                    vos.Add(vo)
                Next
                FormUtil.BindLabelValuesToComboBox(m_view.cmbYosanCode, vos, True)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' 予算書イベント情報より予算コードを設定する。
        ''' </summary>
        ''' <returns>全データテーブル</returns>
        ''' <remarks></remarks>
        Private Function GetEventYosanData(ByVal eventName As String) As DataTable
            Dim dtData As New DataTable
            Using db As New SqlAccess(g_kanrihyoIni(DB_KEY_mBOM))
                db.Open()
                db.Fill(GetEventYosanSql(eventName), dtData)
            End Using
            Return dtData
        End Function

        ''' <summary>
        ''' 予算書イベント情報からデータを取得する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetEventYosanSql(ByVal eventName As String) As String
            Dim sql As New System.Text.StringBuilder()
            With sql
                .Remove(0, .Length)
                .AppendLine("SELECT DISTINCT YOSAN_CODE ")
                .AppendLine("FROM " & MBOM_DB_NAME & ".dbo.T_YOSAN_EVENT ")
                .AppendLine("WHERE YOSAN_EVENT = '" & eventName & "' ")
                .AppendLine("ORDER BY YOSAN_CODE")
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
