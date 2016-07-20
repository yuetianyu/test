Imports FarPoint.Win.Spread
Imports YosansyoTool.YosanshoEdit.Dao.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
Imports YosansyoTool.YosanshoEdit.Dao
Imports FarPoint.Win.Spread.CellType
Imports EBom.Common
Imports FarPoint.Win

Namespace YosanshoEdit.Logic

    Public Class DispYosanshoEdit

#Region " メンバー変数 "
        ''' <summary>ビュー</summary>
        Private m_view As FrmYosanshoEdit

        Private ReadOnly aYosanEventCode As String
        Private ReadOnly aMode As String
        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly aSystemDate As ShisakuDate

        Private IsSuspend_SpreadChangeEvent As Boolean

        Private _YosanEventVo As TYosanEventVo

        Private eventDao As TYosanEventDao
        Private exclusiveEventDao As TYosanExclusiveControlEventDao
        Private exclusiveBuhinDao As TYosanExclusiveControlBuhinDao
        Private editDao As YosanshoEditDao
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
#End Region

#Region " 定数"
        ''' <summary>ステータス</summary>
        Public Const STATUS_00 As String = "00"
        Public Const STATUS_01 As String = "01"
        ''' <summary>試作種別_完成車</summary>
        Public Const SHISAKU_SYUBETU_KANSEI As String = " "
        ''' <summary>試作種別_ホワイトボディ</summary>
        Public Const SHISAKU_SYUBETU_WB As String = "W"
        ''' <summary>ユニット区分_トリム</summary>
        Public Const UNIT_KBN_TRIM_CODE As String = "T"
        Public Const UNIT_KBN_TRIM_NAME As String = "ﾄﾘﾑ"
        ''' <summary>ユニット区分_メタル</summary>
        Public Const UNIT_KBN_METAL_CODE As String = "M"
        Public Const UNIT_KBN_METAL_NAME As String = "ﾒﾀﾙ"
        ''' <summary>上期下期</summary>
        Public Const UP_KS As String = "上期"
        Public Const DOWN_KS As String = "下期"
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

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="f"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal f As FrmYosanshoEdit, ByVal yosanEventCode As String, ByVal strMode As String, ByVal login As LoginInfo)
            m_view = f
            aYosanEventCode = yosanEventCode
            aMode = strMode
            aLoginInfo = login
            aSystemDate = New ShisakuDate

            eventDao = New TYosanEventDaoImpl
            exclusiveEventDao = New TYosanExclusiveControlEventDaoImpl
            exclusiveBuhinDao = New TYosanExclusiveControlBuhinDaoImpl
            editDao = New YosanshoEditDaoImpl
            exportDao = New YosanshoExportDaoImpl

            '予算イベントコードで「予算書イベント情報」を検索する。
            _YosanEventVo = eventDao.FindByPk(aYosanEventCode)
        End Sub
#End Region

#Region " プロパティ "
        ''' <summary>
        ''' 予算書イベント情報を返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property YosanEventVo() As TYosanEventVo
            Get
                Return _YosanEventVo
            End Get
        End Property
        ''' <summary>
        ''' 編集モードの場合、Trueを返す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEditMode() As Boolean
            Get
                Return aMode.Equals(EDIT_MODE)
            End Get
        End Property
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
            m_view.LblCurrPGId.Text = "PG-ID :" + "YOSANSHO EDIT"
        End Sub
#End Region

#Region "画面メイン初期化"
        ''' <summary>
        ''' 画面メイン初期化
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeMain()
            If _YosanEventVo IsNot Nothing Then
                'イベントコード
                m_view.lblEventCode.Text = _YosanEventVo.YosanEventCode
                '区分
                m_view.lblKubun.Text = _YosanEventVo.YosanEventKbnName
                'イベント
                m_view.lblEvent.Text = _YosanEventVo.YosanEvent
                'イベント名称
                m_view.lblEventName.Text = _YosanEventVo.YosanEventName
                '予算コード
                m_view.lblYosanCode.Text = _YosanEventVo.YosanCode
                'W/B係数
                m_view.txtWbKeisu.Text = IIf(StringUtil.IsNotEmpty(_YosanEventVo.YosanWbKeisu), _YosanEventVo.YosanWbKeisu, String.Empty)
            End If

            If IsEditMode Then
                '閲覧表示中のメッセージ非表示
                m_view.lblDispMode.Visible = False
                '登録ボタン使用可
                m_view.btnRegister.Enabled = True
                '（W/B）係数
                m_view.txtWbKeisu.Enabled = True
            Else
                '閲覧表示中のメッセージ表示
                m_view.lblDispMode.Visible = True
                '登録ボタン使用不可
                m_view.btnRegister.Enabled = False
                '（W/B）係数
                m_view.txtWbKeisu.Enabled = False
            End If

            '予算イベントコードで「排他管理予算書部品情報」を検索する。
            Dim exclusiveVos As List(Of TYosanExclusiveControlBuhinVo)
            exclusiveVos = FindExclusiveBuhin(aYosanEventCode, UNIT_KBN_METAL_CODE)
            'メタルのデータが存在の場合
            If exclusiveVos IsNot Nothing AndAlso exclusiveVos.Count > 0 Then
                m_view.lblMetalEditMsg.Visible = True
            Else
                m_view.lblMetalEditMsg.Visible = False
            End If
            exclusiveVos = FindExclusiveBuhin(aYosanEventCode, UNIT_KBN_TRIM_CODE)
            'トリムのデータが存在の場合
            If exclusiveVos IsNot Nothing AndAlso exclusiveVos.Count > 0 Then
                m_view.lblTrimEditMsg.Visible = True
            Else
                m_view.lblTrimEditMsg.Visible = False
            End If
        End Sub
#End Region

#Region "スプレッド初期化"
        ''' <summary>
        ''' スプレッドを初期化する      
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitializeSpread()
            ''Spreadの初期化
            InitializeSpread(m_view.spdZaimuJiseki)
            InitializeSpread(m_view.spdSeisakuDaisu)
            InitializeSpread(m_view.spdTukurikataKanseisha)
            InitializeSpread(m_view.spdTukurikataWBsha)
            InitializeSpread(m_view.spdKanazai)

            SpreadUtil.AddHandlerSheetDataModelChanged(m_view.spdSeisakuDaisu_Sheet1, AddressOf SeisakuDaisuSpread_ChangeEventHandlable)
            SpreadUtil.AddHandlerSheetDataModelChanged(m_view.spdKanazai_Sheet1, AddressOf KanazaiSpread_ChangeEventHandlable)
            SpreadUtil.AddHandlerSheetDataModelChanged(m_view.spdTukurikataKanseisha_Sheet1, AddressOf KanseiSpread_ChangeEventHandlable)
            SpreadUtil.AddHandlerSheetDataModelChanged(m_view.spdTukurikataWBsha_Sheet1, AddressOf WBSpread_ChangeEventHandlable)
            SpreadUtil.AddHandlerSheetDataModelChanged(m_view.spdZaimuJiseki_Sheet1, AddressOf ZaimujisekiSpread_ChangeEventHandlable)
        End Sub

        Private Sub InitializeSpread(ByVal aSpread As FpSpread)
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

#Region "スプレッド使用可／不可"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpreadEnabled()
            EnabledZaimuJisekiSpread()
            EnabledSeisakuDaisuSpread()
            EnabledTukurikataKanseishaSpread()
            EnabledTukurikataWBshaSpread()
            EnabledKanazaiSpread()
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpreadDisabled()
            DisabledZaimuJisekiSpread()
            DisabledSeisakuDaisuSpread()
            DisabledTukurikataKanseishaSpread()
            DisabledTukurikataWBshaSpread()
            DisabledKanazaiSpread()
        End Sub

        ''' <summary>
        ''' 集計値SPREAD使用可
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub EnabledZaimuJisekiSpread()
            For rowIndex As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To m_view.spdZaimuJiseki_Sheet1.RowCount - 1
                For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To m_view.spdZaimuJiseki_Sheet1.ColumnCount - 1
                    If rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX Then
                        m_view.spdZaimuJiseki_Sheet1.Cells(rowIndex, columnIndex).Locked = False
                    Else
                        m_view.spdZaimuJiseki_Sheet1.Cells(rowIndex, columnIndex).Locked = True
                    End If
                    If (columnIndex - SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 OrElse _
                       columnIndex = m_view.spdZaimuJiseki_Sheet1.ColumnCount - 1 Then
                        m_view.spdZaimuJiseki_Sheet1.Cells(rowIndex, columnIndex).Locked = True
                    End If
                Next
            Next
            '再計算ボタン
            m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_CALC_BUTTON_CELL_ROW_INDEX, SPD_ZAIMUJISEKI_CALC_BUTTON_CELL_COLUMN_INDEX).Locked = False
        End Sub

        ''' <summary>
        ''' 集計値SPREAD使用不可
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DisabledZaimuJisekiSpread()
            For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To m_view.spdZaimuJiseki_Sheet1.ColumnCount - 1
                If (columnIndex - SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 OrElse _
                   (columnIndex - SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 2) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).Locked = False
                End If
                For rowIndex As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To m_view.spdZaimuJiseki_Sheet1.RowCount - 1
                    m_view.spdZaimuJiseki_Sheet1.Cells(rowIndex, columnIndex).Locked = True
                Next
            Next
            '再計算ボタン
            m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_CALC_BUTTON_CELL_ROW_INDEX, SPD_ZAIMUJISEKI_CALC_BUTTON_CELL_COLUMN_INDEX).Locked = False
        End Sub

        ''' <summary>
        ''' 製作台数SPREAD使用可
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub EnabledSeisakuDaisuSpread()
            For columnIndex As Integer = SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN To m_view.spdSeisakuDaisu_Sheet1.ColumnCount - 1 Step 2
                m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).Locked = False
                m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).Locked = False
                m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).Locked = False
            Next
            '追加ボタン
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_ADD_BUTTON_CELL_ROW_INDEX, SPD_DAISU_ADD_BUTTON_CELL_COLUMN_INDEX).Locked = False
        End Sub

        ''' <summary>
        ''' 製作台数SPREAD使用不可
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DisabledSeisakuDaisuSpread()
            For columnIndex As Integer = SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN To m_view.spdSeisakuDaisu_Sheet1.ColumnCount - 1 Step 2
                m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).Locked = True
                m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).Locked = True
                m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).Locked = True
            Next
            '追加ボタン
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_ADD_BUTTON_CELL_ROW_INDEX, SPD_DAISU_ADD_BUTTON_CELL_COLUMN_INDEX).Locked = True
        End Sub

        ''' <summary>
        ''' 造り方完成車SPREAD使用可
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub EnabledTukurikataKanseishaSpread()
            For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataKanseisha_Sheet1.RowCount - 2
                For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To m_view.spdTukurikataKanseisha_Sheet1.ColumnCount - 2
                    m_view.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, columnIndex).Locked = False
                    If (columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                        m_view.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, columnIndex).Locked = True
                    End If
                Next
            Next
            '造り方追加ボタン
            m_view.spdTukurikataKanseisha_Sheet1.Cells(SPD_TUKURIKATA_ADD_BUTTON_CELL_ROW_INDEX, SPD_TUKURIKATA_ADD_BUTTON_CELL_COLUMN_INDEX).Locked = False
            '月挿入ボタン
            m_view.spdTukurikataKanseisha_Sheet1.Cells(m_view.spdTukurikataKanseisha_Sheet1.RowCount - 1, SPD_TUKURIKATA_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX).Locked = False
            m_view.spdTukurikataKanseisha_Sheet1.Cells(m_view.spdTukurikataKanseisha_Sheet1.RowCount - 1, m_view.spdTukurikataKanseisha_Sheet1.ColumnCount - 1).Locked = False
        End Sub

        ''' <summary>
        ''' 造り方完成車SPREAD使用不可
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DisabledTukurikataKanseishaSpread()
            For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To m_view.spdTukurikataKanseisha_Sheet1.ColumnCount - 2
                If (columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 OrElse _
                   (columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 2) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    m_view.spdTukurikataKanseisha_Sheet1.Cells(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).Locked = False
                End If
                For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataKanseisha_Sheet1.RowCount - 2
                    m_view.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, columnIndex).Locked = True
                Next
            Next
            '造り方追加ボタン
            m_view.spdTukurikataKanseisha_Sheet1.Cells(SPD_TUKURIKATA_ADD_BUTTON_CELL_ROW_INDEX, SPD_TUKURIKATA_ADD_BUTTON_CELL_COLUMN_INDEX).Locked = True
            '月挿入ボタン
            m_view.spdTukurikataKanseisha_Sheet1.Cells(m_view.spdTukurikataKanseisha_Sheet1.RowCount - 1, SPD_TUKURIKATA_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX).Locked = True
            m_view.spdTukurikataKanseisha_Sheet1.Cells(m_view.spdTukurikataKanseisha_Sheet1.RowCount - 1, m_view.spdTukurikataKanseisha_Sheet1.ColumnCount - 1).Locked = True
        End Sub

        ''' <summary>
        ''' 造り方ﾎﾜｲﾄﾎﾞﾃﾞｨSPREAD使用可
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub EnabledTukurikataWBshaSpread()
            For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataWBsha_Sheet1.RowCount - 2
                For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To m_view.spdTukurikataWBsha_Sheet1.ColumnCount - 2
                    m_view.spdTukurikataWBsha_Sheet1.Cells(rowIndex, columnIndex).Locked = False
                    If (columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                        m_view.spdTukurikataWBsha_Sheet1.Cells(rowIndex, columnIndex).Locked = True
                    End If
                Next
            Next
            '造り方追加ボタン
            m_view.spdTukurikataWBsha_Sheet1.Cells(SPD_TUKURIKATA_ADD_BUTTON_CELL_ROW_INDEX, SPD_TUKURIKATA_ADD_BUTTON_CELL_COLUMN_INDEX).Locked = False
            '月挿入ボタン
            m_view.spdTukurikataWBsha_Sheet1.Cells(m_view.spdTukurikataWBsha_Sheet1.RowCount - 1, SPD_TUKURIKATA_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX).Locked = False
            m_view.spdTukurikataWBsha_Sheet1.Cells(m_view.spdTukurikataWBsha_Sheet1.RowCount - 1, m_view.spdTukurikataWBsha_Sheet1.ColumnCount - 1).Locked = False
        End Sub

        ''' <summary>
        ''' 造り方ﾎﾜｲﾄﾎﾞﾃﾞｨSPREAD使用不可
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DisabledTukurikataWBshaSpread()
            For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To m_view.spdTukurikataWBsha_Sheet1.ColumnCount - 2
                If (columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 OrElse _
                   (columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 2) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    m_view.spdTukurikataWBsha_Sheet1.Cells(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).Locked = False
                End If
                For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataWBsha_Sheet1.RowCount - 2
                    m_view.spdTukurikataWBsha_Sheet1.Cells(rowIndex, columnIndex).Locked = True
                Next
            Next
            '造り方追加ボタン
            m_view.spdTukurikataWBsha_Sheet1.Cells(SPD_TUKURIKATA_ADD_BUTTON_CELL_ROW_INDEX, SPD_TUKURIKATA_ADD_BUTTON_CELL_COLUMN_INDEX).Locked = True
            '月挿入ボタン
            m_view.spdTukurikataWBsha_Sheet1.Cells(m_view.spdTukurikataWBsha_Sheet1.RowCount - 1, SPD_TUKURIKATA_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX).Locked = True
            m_view.spdTukurikataWBsha_Sheet1.Cells(m_view.spdTukurikataWBsha_Sheet1.RowCount - 1, m_view.spdTukurikataWBsha_Sheet1.ColumnCount - 1).Locked = True
        End Sub

        ''' <summary>
        ''' 金材SPREAD使用可
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub EnabledKanazaiSpread()
            For rowIndex As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX To m_view.spdKanazai_Sheet1.RowCount - 2
                m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).Locked = False
                For columnIndex As Integer = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX To m_view.spdKanazai_Sheet1.ColumnCount - 2
                    m_view.spdKanazai_Sheet1.Cells(rowIndex, columnIndex).Locked = False
                    If (columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                        m_view.spdKanazai_Sheet1.Cells(rowIndex, columnIndex).Locked = True
                    End If
                Next
            Next
            '金材追加ボタン
            m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_ADD_BUTTON_CELL_ROW_INDEX, SPD_KANAZAI_ADD_BUTTON_CELL_COLUMN_INDEX).Locked = False
            '月挿入ボタン
            m_view.spdKanazai_Sheet1.Cells(m_view.spdKanazai_Sheet1.RowCount - 1, SPD_KANAZAI_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX).Locked = False
            m_view.spdKanazai_Sheet1.Cells(m_view.spdKanazai_Sheet1.RowCount - 1, m_view.spdKanazai_Sheet1.ColumnCount - 1).Locked = False
        End Sub

        ''' <summary>
        ''' 金材SPREAD使用不可
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DisabledKanazaiSpread()
            For columnIndex As Integer = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX To m_view.spdKanazai_Sheet1.ColumnCount - 2
                If (columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 OrElse _
                   (columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 2) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                    m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).Locked = False
                End If
                For rowIndex As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX To m_view.spdKanazai_Sheet1.RowCount - 2
                    m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).Locked = True
                    m_view.spdKanazai_Sheet1.Cells(rowIndex, columnIndex).Locked = True
                Next
            Next
            '金材追加ボタン
            m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_ADD_BUTTON_CELL_ROW_INDEX, SPD_KANAZAI_ADD_BUTTON_CELL_COLUMN_INDEX).Locked = True
            '月挿入ボタン
            m_view.spdKanazai_Sheet1.Cells(m_view.spdKanazai_Sheet1.RowCount - 1, SPD_KANAZAI_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX).Locked = True
            m_view.spdKanazai_Sheet1.Cells(m_view.spdKanazai_Sheet1.RowCount - 1, m_view.spdKanazai_Sheet1.ColumnCount - 1).Locked = True
        End Sub
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

#Region "製作台数"

#Region "製作台数スプレッドにデータ設定"
        ''' <summary>
        ''' 製作台数スプレッドにデータを設定する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DispSpreadSeisakuDaisu()
            IsSuspend_SpreadChangeEvent = False

            Dim yosanSeisakuDaisuList As List(Of TYosanSeisakuDaisuVo)
            yosanSeisakuDaisuList = editDao.FindYosanSeisakuDaisuBy(aYosanEventCode)

            m_view.spdSeisakuDaisu_Sheet1.RowCount = SPD_DAISU_ROW_COUNT

            For colIndex = SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN To m_view.spdSeisakuDaisu_Sheet1.ColumnCount - 1 Step 2
                SetKojishireiColumnPro(colIndex)
            Next

            _KanseiDaisuList = New List(Of TYosanSeisakuDaisuVo)
            _WbDaisuList = New List(Of TYosanSeisakuDaisuVo)
            For Each daisuVo As TYosanSeisakuDaisuVo In yosanSeisakuDaisuList
                Dim colIndex As Integer = daisuVo.KoujiShireiNoHyojijunNo * 2 + SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN
                If colIndex >= m_view.spdSeisakuDaisu_Sheet1.ColumnCount Then
                    m_view.spdSeisakuDaisu_Sheet1.AddColumns(colIndex, 2)
                    SetKojishireiColumnPro(colIndex)
                End If
                'ユニット区分
                m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, colIndex).Value = UnitKbnName(daisuVo.UnitKbn)
                '工事指令No
                m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, colIndex).Value = daisuVo.KoujiShireiNo
                '台数
                If IsKanseisha(daisuVo.ShisakuSyubetu) Then
                    '完成車の場合
                    m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, colIndex).Value = daisuVo.DaisuSuryo
                    Dim newVo As New TYosanSeisakuDaisuVo
                    VoUtil.CopyProperties(daisuVo, newVo)
                    _KanseiDaisuList.Add(newVo)
                End If
                If IsWb(daisuVo.ShisakuSyubetu) Then
                    'WB車の場合
                    m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, colIndex).Value = daisuVo.DaisuSuryo
                    Dim newVo As New TYosanSeisakuDaisuVo
                    VoUtil.CopyProperties(daisuVo, newVo)
                    _WbDaisuList.Add(newVo)
                End If
            Next
            '４列未満の場合
            If _KanseiDaisuList.Count < SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT Then
                SetDefaultSeisakuDaisuList(SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT - _KanseiDaisuList.Count, _KanseiDaisuList, SHISAKU_SYUBETU_KANSEI)
            End If
            If _WbDaisuList.Count < SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT Then
                SetDefaultSeisakuDaisuList(SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT - _WbDaisuList.Count, _WbDaisuList, SHISAKU_SYUBETU_WB)
            End If

            '台数合計
            SetSpreadSeisakuDaisuPro()
            CalcTaisu()
            '工事指令Noヘッダー
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_KOTEI_COLUMN_COUNT).ColumnSpan = m_view.spdSeisakuDaisu_Sheet1.ColumnCount - SPD_DAISU_KOTEI_COLUMN_COUNT
            '固定列を設定
            m_view.spdSeisakuDaisu_Sheet1.FrozenColumnCount = SPD_DAISU_KOTEI_COLUMN_COUNT

            IsSuspend_SpreadChangeEvent = True
        End Sub

        ''' <summary>
        ''' デフォルトで４列とする
        ''' </summary>
        ''' <param name="addCount"></param>
        ''' <param name="aList"></param>
        ''' <remarks></remarks>
        Private Sub SetDefaultSeisakuDaisuList(ByVal addCount As Integer, ByVal aList As List(Of TYosanSeisakuDaisuVo), ByVal shisakuSyubetu As String)
            For index = 0 To addCount - 1
                Dim newVo As New TYosanSeisakuDaisuVo
                newVo.YosanEventCode = aYosanEventCode
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
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Locked = True
            'm_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).CellType = GetDaisuCellType()
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).Locked = True
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Locked = True
            'm_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).CellType = GetDaisuCellType()
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).Locked = True
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).HorizontalAlignment = CellHorizontalAlignment.Left

        End Sub

        ''' <summary>
        ''' SPREADで工事指令№セルの配置を設定する。
        ''' </summary>
        ''' <param name="columnIndex"></param>
        ''' <remarks></remarks>
        Private Sub SetKojishireiColumnPro(ByVal columnIndex As Integer)
            m_view.spdSeisakuDaisu_Sheet1.Columns(columnIndex).Width = m_view.spdSeisakuDaisu_Sheet1.Columns(SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN).Width
            m_view.spdSeisakuDaisu_Sheet1.Columns(columnIndex + 1).Width = m_view.spdSeisakuDaisu_Sheet1.Columns(SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN + 1).Width
            'ユニット区分セル
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).Locked = True
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).VerticalAlignment = CellVerticalAlignment.Center

            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).ColumnSpan = 2
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).BackColor = Color.Khaki
            '工事指令№セル
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).Locked = False
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).CellType = GetKojishireiCellType()
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).ColumnSpan = 2
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).BackColor = Color.White
            '台数セル
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).Locked = False
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).CellType = GetDaisuCellType()
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).BackColor = Color.White
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).Locked = True
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).VerticalAlignment = CellVerticalAlignment.Center
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).Value = "台"
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).BackColor = Color.White
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).Locked = False
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).CellType = GetDaisuCellType()
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).BackColor = Color.White
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).Locked = True
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).VerticalAlignment = CellVerticalAlignment.Center
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).Value = "台"
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).BackColor = Color.White
        End Sub

        ''' <summary>
        ''' 台数合計
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CalcTaisu()
            IsSuspend_SpreadChangeEvent = False

            Dim taisuKanseiShaCount As Integer = 0
            Dim taisuWbShaCount As Integer = 0
            For columnIndex As Integer = SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN To m_view.spdSeisakuDaisu_Sheet1.ColumnCount - 1 Step 2
                '完成車
                If StringUtil.IsNotEmpty(m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).Value) Then
                    taisuKanseiShaCount = taisuKanseiShaCount + CInt(m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).Value)
                End If
                If taisuKanseiShaCount = 0 Then
                    m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = Nothing
                Else
                    m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = taisuKanseiShaCount
                End If

                'WB車
                If StringUtil.IsNotEmpty(m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).Value) Then
                    taisuWbShaCount = taisuWbShaCount + CInt(m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).Value)
                End If
                If taisuWbShaCount = 0 Then
                    m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = Nothing
                Else
                    m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = taisuWbShaCount
                End If
            Next

            IsSuspend_SpreadChangeEvent = True
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
        ''' <summary>
        ''' 製作台数スプレッドの列追加
        ''' </summary>
        ''' <param name="unitKbn"></param>
        ''' <param name="kojishireiNo"></param>
        ''' <remarks></remarks>
        Public Sub AddYosanSeisakuDaisu(ByVal unitKbn As String, ByVal kojishireiNo As String)
            IsSuspend_SpreadChangeEvent = False

            Dim colIndex As Integer = m_view.spdSeisakuDaisu_Sheet1.ColumnCount
            m_view.spdSeisakuDaisu_Sheet1.AddColumns(colIndex, 2)
            SetKojishireiColumnPro(colIndex)

            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, colIndex).Value = UnitKbnName(unitKbn)
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, colIndex).Value = kojishireiNo

            '工事指令Noヘッダー
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_KOTEI_COLUMN_COUNT).ColumnSpan = m_view.spdSeisakuDaisu_Sheet1.ColumnCount - SPD_DAISU_KOTEI_COLUMN_COUNT

            SetKojishireiColumnPro(colIndex)

            '完成車
            Dim addVo As New TYosanSeisakuDaisuVo
            addVo.YosanEventCode = aYosanEventCode
            addVo.ShisakuSyubetu = SHISAKU_SYUBETU_KANSEI
            addVo.KoujiShireiNoHyojijunNo = _KanseiDaisuList.Count
            addVo.UnitKbn = unitKbn
            addVo.KoujiShireiNo = kojishireiNo
            _KanseiDaisuList.Add(addVo)
            'WB車
            addVo = New TYosanSeisakuDaisuVo
            addVo.YosanEventCode = aYosanEventCode
            addVo.ShisakuSyubetu = SHISAKU_SYUBETU_WB
            addVo.KoujiShireiNoHyojijunNo = _WbDaisuList.Count
            addVo.UnitKbn = unitKbn
            addVo.KoujiShireiNo = kojishireiNo
            _WbDaisuList.Add(addVo)

            IsSuspend_SpreadChangeEvent = True
        End Sub
        ''' <summary>
        ''' 製作台数スプレッドの列削除
        ''' </summary>
        ''' <param name="columnIndex"></param>
        ''' <remarks></remarks>
        Public Sub RemoveYosanSeisakuDaisu(ByVal columnIndex As Integer)
            IsSuspend_SpreadChangeEvent = False

            m_view.spdSeisakuDaisu_Sheet1.RemoveColumns(columnIndex, 2)
            '工事指令Noヘッダー
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN).ColumnSpan = m_view.spdSeisakuDaisu_Sheet1.ColumnCount - SPD_DAISU_KOTEI_COLUMN_COUNT
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN).BackColor = Color.Khaki
            m_view.spdSeisakuDaisu_Sheet1.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN).Value = "工事指令№"

            Dim index As Integer = CInt((columnIndex - SPD_DAISU_KOTEI_COLUMN_COUNT) / 2)
            _KanseiDaisuList.RemoveAt(index)
            _WbDaisuList.RemoveAt(index)

            IsSuspend_SpreadChangeEvent = True
        End Sub
#End Region

#End Region

#Region "金材"

#Region "金材スプレッドにデータ設定"
        ''' <summary>
        ''' 金材スプレッドにデータ設定する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DispSpreadKanazai()
            IsSuspend_SpreadChangeEvent = False

            _KanazaiList = editDao.FindYosanKanazaiBy(aYosanEventCode)

            Dim vosByHyojijun As Dictionary(Of String, List(Of TYosanKanazaiVo)) = MakeKanazaiVosByHyojijun(_KanazaiList)

            If vosByHyojijun.Count > 0 Then
                m_view.spdKanazai_Sheet1.RemoveColumns(SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, 1)
                m_view.spdKanazai_Sheet1.AddRows(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, vosByHyojijun.Count)

                Dim indexX As Integer = 0
                Dim ks As String = String.Empty
                Dim columnIndex As Integer = 0

                For Each key As String In vosByHyojijun.Keys
                    Dim rowIndex As Integer = indexX + SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX
                    columnIndex = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX
                    SetKanazaiRowPro(rowIndex)
                    '金材項目名
                    m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).Value = key
                    '単価
                    m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).Value = vosByHyojijun(key)(0).KanazaiUnitPrice

                    For Each kanazaiVo As TYosanKanazaiVo In vosByHyojijun(key)
                        If indexX = 0 Then
                            '年期タイトル
                            If (columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                                '合計列追加
                                AddSpdMonthCountColumn(m_view.spdKanazai_Sheet1, columnIndex, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                                                       SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                                columnIndex = columnIndex + 1
                            End If

                            m_view.spdKanazai_Sheet1.AddColumns(columnIndex, 1)
                            '年
                            m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).Value = ChangeDBYearToSpreadYear(kanazaiVo.YosanTukurikataYyyyMm, kanazaiVo.YosanTukurikataKs)
                            '月
                            m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_MONTH_ROW_INDEX, columnIndex).Value = CInt(kanazaiVo.YosanTukurikataYyyyMm.Substring(4, 2)).ToString & "月"
                        End If
                        '月分布タイトル
                        SetMonthTitlePro(m_view.spdKanazai_Sheet1, columnIndex, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                                         SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                        '合計列の場合
                        If (columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                            SetMonthCellPro(m_view.spdKanazai_Sheet1, rowIndex, columnIndex, True)
                            columnIndex = columnIndex + 1
                        End If
                        '台数セル
                        m_view.spdKanazai_Sheet1.Cells(rowIndex, columnIndex).Value = kanazaiVo.DaisuSuryo
                        SetMonthCellPro(m_view.spdKanazai_Sheet1, rowIndex, columnIndex, False)
                        columnIndex = columnIndex + 1
                    Next

                    indexX = indexX + 1
                Next
                '合計列追加
                AddSpdMonthCountColumn(m_view.spdKanazai_Sheet1, columnIndex, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                                       SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                '月分布フッター
                m_view.spdKanazai_Sheet1.Cells(m_view.spdKanazai_Sheet1.RowCount - 1, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).ColumnSpan = m_view.spdKanazai_Sheet1.ColumnCount - SPD_KANAZAI_KOTEI_COLUMN_COUNT - 1

                '台数合計
                CalcKanazai()
            End If
            '固定列を設定
            m_view.spdKanazai_Sheet1.FrozenColumnCount = SPD_KANAZAI_KOTEI_COLUMN_COUNT
            '固定行を設定
            m_view.spdKanazai_Sheet1.FrozenRowCount = SPD_KANAZAI_KOTEI_ROW_COUNT

            IsSuspend_SpreadChangeEvent = True
        End Sub

        ''' <summary>
        ''' SPREADで行の配置を設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetKanazaiRowPro(ByVal rowIndex As Integer)
            '金材項目名セル
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).Locked = True
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '台数セル
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).Locked = True
            'm_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).CellType = GetDaisuCellType()
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX + 1).Locked = True
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX + 1).VerticalAlignment = CellVerticalAlignment.Center
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX + 1).Value = "台"
            '単価セル
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).Locked = False
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).CellType = GetTankaCellType()
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '計セル
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).Locked = True
            'm_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).CellType = GetKingakuCellType()
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center

            m_view.spdKanazai_Sheet1.Rows(rowIndex).BackColor = Color.White
            m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, m_view.spdKanazai_Sheet1.ColumnCount - 1).RowSpan = m_view.spdKanazai_Sheet1.RowCount - 1
        End Sub

        ''' <summary>
        ''' 台数合計
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CalcKanazai()
            IsSuspend_SpreadChangeEvent = False

            For rowIndex As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX To m_view.spdKanazai_Sheet1.RowCount - 2
                Dim daisuCount As Integer = 0
                Dim monthCount As Integer = 0
                For columnIndex As Integer = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX To m_view.spdKanazai_Sheet1.ColumnCount - 2
                    '合計列以外
                    If Not ((columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                        If StringUtil.IsNotEmpty(m_view.spdKanazai_Sheet1.GetValue(rowIndex, columnIndex)) Then
                            monthCount = monthCount + m_view.spdKanazai_Sheet1.GetValue(rowIndex, columnIndex)
                        End If
                    Else
                        If monthCount = 0 Then
                            m_view.spdKanazai_Sheet1.Cells(rowIndex, columnIndex).Value = Nothing
                        Else
                            m_view.spdKanazai_Sheet1.Cells(rowIndex, columnIndex).Value = monthCount
                        End If
                        daisuCount = daisuCount + monthCount

                        monthCount = 0
                    End If
                Next
                '台数合計
                If daisuCount = 0 Then
                    m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).Value = Nothing
                Else
                    m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).Value = daisuCount
                End If
                '計
                Dim tanka As Decimal? = CDec(IIf(StringUtil.IsNotEmpty(m_view.spdKanazai_Sheet1.GetValue(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX)), m_view.spdKanazai_Sheet1.GetValue(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX), Nothing))
                If tanka IsNot Nothing AndAlso daisuCount > 0 Then
                    m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).Value = tanka * daisuCount
                Else
                    m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).Value = Nothing
                End If
            Next

            IsSuspend_SpreadChangeEvent = True
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
                For rowIndex As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX To m_view.spdKanazai_Sheet1.RowCount - 1
                    aList.Add(m_view.spdKanazai_Sheet1.GetValue(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX))
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
        ''' <summary>
        ''' 金材スプレッドの行追加
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AddYosanKanazaiRow(ByVal kanazaiName As String)
            IsSuspend_SpreadChangeEvent = False

            Dim rowIndex As Integer = m_view.spdKanazai_Sheet1.RowCount - 1
            m_view.spdKanazai_Sheet1.AddRows(rowIndex, 1)
            SetKanazaiRowPro(rowIndex)
            '金材項目名をセット
            m_view.spdKanazai_Sheet1.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).Value = kanazaiName

            If m_view.spdKanazai_Sheet1.ColumnCount > SPD_KANAZAI_DEFAULT_COLUMN_COUNT Then
                Dim addVos As New List(Of TYosanKanazaiVo)
                For colIndex As Integer = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX To m_view.spdKanazai_Sheet1.ColumnCount - 2
                    '合計列を判断
                    If (colIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) > 0 Then
                        '台数セル
                        SetMonthCellPro(m_view.spdKanazai_Sheet1, rowIndex, colIndex, False)

                        Dim addVo As New TYosanKanazaiVo
                        addVo.YosanEventCode = aYosanEventCode
                        addVo.KanazaiName = kanazaiName
                        addVo.KanazaiHyoujiJun = rowIndex - SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX
                        Dim year As String = m_view.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_YEAR_ROW_INDEX, colIndex).ToString.Substring(0, 4)
                        Dim ks As String = m_view.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_YEAR_ROW_INDEX, colIndex).ToString.Substring(4, 2)
                        Dim month As String = m_view.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_MONTH_ROW_INDEX, colIndex).ToString.Replace("月", "")
                        Dim yearMonth As String = ChangeSpreadYearToDBYear(year, ks, CInt(month).ToString("00"))
                        addVo.YosanTukurikataYyyyMm = yearMonth
                        addVo.YosanTukurikataKs = ks
                        addVos.Add(addVo)
                    Else
                        '台数セル
                        SetMonthCellPro(m_view.spdKanazai_Sheet1, rowIndex, colIndex, True)
                    End If
                Next
                _KanazaiList.AddRange(addVos)
            End If

            IsSuspend_SpreadChangeEvent = True
        End Sub
        ''' <summary>
        ''' 金材スプレッドの行削除
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <param name="kanazaiName"></param>
        ''' <remarks></remarks>
        Public Sub RemoveYosanKanazaiRow(ByVal rowIndex As Integer, ByVal kanazaiName As String)
            IsSuspend_SpreadChangeEvent = False

            m_view.spdKanazai_Sheet1.RemoveRows(rowIndex, 1)

            Dim newList As New List(Of TYosanKanazaiVo)
            For Each Vo As TYosanKanazaiVo In _KanazaiList
                If Not String.Equals(Vo.KanazaiName, kanazaiName) Then
                    newList.Add(Vo)
                End If
            Next

            _KanazaiList = newList

            IsSuspend_SpreadChangeEvent = True
        End Sub
        ''' <summary>
        ''' 金材スプレッドの列挿入
        ''' </summary>
        ''' <param name="year"></param>
        ''' <param name="ks"></param>
        ''' <param name="columnIndex"></param>
        ''' <param name="delFlag"></param>
        ''' <remarks></remarks>
        Public Sub AddYosanKanazaiColumn(ByVal year As String, ByVal ks As String, ByVal columnIndex As Integer, ByVal delFlag As Boolean)
            IsSuspend_SpreadChangeEvent = False

            If delFlag Then
                m_view.spdKanazai_Sheet1.RemoveColumns(columnIndex, 1)
            End If
            m_view.spdKanazai_Sheet1.AddColumns(columnIndex, HALF_MONTH_DEFAULT_COUNT)

            Dim addVos As New List(Of TYosanKanazaiVo)
            For index As Integer = 0 To HALF_MONTH_DEFAULT_COUNT - 1
                Dim month As Integer
                Dim col As Integer = index + columnIndex
                SetMonthTitlePro(m_view.spdKanazai_Sheet1, col, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                                 SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                '年
                m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_YEAR_ROW_INDEX, col).Value = year & ks
                '月
                If String.Equals(ks, UP_KS) Then
                    month = CInt(4 + index)
                    m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_MONTH_ROW_INDEX, col).Value = month.ToString & "月"
                Else
                    If index < 3 Then
                        month = CInt(10 + index)
                        m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_MONTH_ROW_INDEX, col).Value = month.ToString & "月"
                    Else
                        month = CInt(1 + index - 3)
                        m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_MONTH_ROW_INDEX, col).Value = month.ToString & "月"
                    End If
                End If

                For rowIndex As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX To m_view.spdKanazai_Sheet1.RowCount - 2
                    '台数セル
                    SetMonthCellPro(m_view.spdKanazai_Sheet1, rowIndex, col, False)

                    Dim kanazaiName As String = m_view.spdKanazai_Sheet1.GetValue(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX)
                    Dim addVo As New TYosanKanazaiVo
                    addVo.YosanEventCode = aYosanEventCode
                    addVo.KanazaiName = kanazaiName
                    addVo.KanazaiHyoujiJun = rowIndex - SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX
                    Dim yearMonth As String = ChangeSpreadYearToDBYear(year, ks, CInt(month).ToString("00"))
                    addVo.YosanTukurikataYyyyMm = yearMonth
                    addVo.YosanTukurikataKs = ks
                    addVos.Add(addVo)
                Next
            Next
            _KanazaiList.AddRange(addVos)

            '合計列を挿入
            columnIndex = columnIndex + HALF_MONTH_DEFAULT_COUNT
            AddSpdMonthCountColumn(m_view.spdKanazai_Sheet1, columnIndex, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, _
                                   SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)

            '月分布ヘッダー
            m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).ColumnSpan = m_view.spdKanazai_Sheet1.ColumnCount - SPD_KANAZAI_KOTEI_COLUMN_COUNT - 1
            '月分布フッター
            m_view.spdKanazai_Sheet1.Cells(m_view.spdKanazai_Sheet1.RowCount - 1, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).ColumnSpan = m_view.spdKanazai_Sheet1.ColumnCount - SPD_KANAZAI_KOTEI_COLUMN_COUNT - 1

            '挿入列を拡大して表示する
            VisibleMonthColumns(m_view.spdKanazai_Sheet1, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, _
                                SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, columnIndex, SPD_KANAZAI_KOTEI_COLUMN_COUNT)

            IsSuspend_SpreadChangeEvent = True
        End Sub
        ''' <summary>
        ''' 金材スプレッドの列削除
        ''' </summary>
        ''' <param name="columnIndex"></param>
        ''' <param name="month"></param>
        ''' <remarks></remarks>
        Public Sub RemoveYosanKanazaiColumn(ByVal columnIndex As Integer, ByVal month As String)
            IsSuspend_SpreadChangeEvent = False

            m_view.spdKanazai_Sheet1.RemoveColumns(columnIndex - HALF_MONTH_DEFAULT_COUNT, (HALF_MONTH_DEFAULT_COUNT + 1))
            If m_view.spdKanazai_Sheet1.ColumnCount < SPD_KANAZAI_DEFAULT_COLUMN_COUNT Then
                m_view.spdKanazai_Sheet1.AddColumns(SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, 1)
                SetMonthTitlePro(m_view.spdKanazai_Sheet1, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                                 SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                '台数セル
                For rowIndex As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX To m_view.spdKanazai_Sheet1.RowCount - 2
                    SetMonthCellPro(m_view.spdKanazai_Sheet1, rowIndex, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, True)
                Next
            End If
            '月分布ヘッダー
            m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).ColumnSpan = m_view.spdKanazai_Sheet1.ColumnCount - SPD_KANAZAI_KOTEI_COLUMN_COUNT - 1
            m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).BackColor = Color.Khaki
            m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).Value = "月分布"
            m_view.spdKanazai_Sheet1.Cells(SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '月分布フッター
            m_view.spdKanazai_Sheet1.Cells(m_view.spdKanazai_Sheet1.RowCount - 1, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).ColumnSpan = m_view.spdKanazai_Sheet1.ColumnCount - SPD_KANAZAI_KOTEI_COLUMN_COUNT - 1

            Dim year As String = month.Substring(0, 4)
            Dim ks As String = month.Remove(0, 4)
            Dim yearMonth As List(Of String) = GetDBYearMonth(year, ks)

            Dim newList As New List(Of TYosanKanazaiVo)
            For Each Vo As TYosanKanazaiVo In _KanazaiList
                If Not (yearMonth.Contains(Vo.YosanTukurikataYyyyMm) AndAlso String.Equals(Vo.YosanTukurikataKs, ks)) Then
                    newList.Add(Vo)
                End If
            Next
            _KanazaiList = newList

            '合計
            CalcKanazai()

            IsSuspend_SpreadChangeEvent = True
        End Sub
#End Region

#End Region

#Region "造り方"

#Region "造り方スプレッドにデータ設定"
        ''' <summary>
        ''' 造り方スプレッドにデータ設定する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DispSpreadTukurikata()

            '完成車
            _KanseiTukurikataList = DispSpreadTukurikata(SHISAKU_SYUBETU_KANSEI, m_view.spdTukurikataKanseisha_Sheet1)
            'ホワイトボディ
            _WbTukurikataList = DispSpreadTukurikata(SHISAKU_SYUBETU_WB, m_view.spdTukurikataWBsha_Sheet1)

        End Sub

        ''' <summary>
        ''' 完成車・ホワイトボディ
        ''' </summary>
        ''' <param name="shisakuSyubetu"></param>
        ''' <param name="aSheet"></param>
        ''' <remarks></remarks>
        Private Function DispSpreadTukurikata(ByVal shisakuSyubetu As String, ByVal aSheet As SheetView) As List(Of TYosanTukurikataVo)
            '2016/02/26 kabasawa'
            '部品表名が重複するとエラーになるのでパターン名を追加'

            IsSuspend_SpreadChangeEvent = False

            Dim aTukurikataList As List(Of TYosanTukurikataVo) = editDao.FindYosanTukurikataBy(aYosanEventCode, shisakuSyubetu)
            Dim vosByHyojijun As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo)))) = MakeTukurikataVosByHyojijun(aTukurikataList)

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
                        For Each patternName As String In vosByHyojijun(unitKbn)(buhinhyoName).Keys
                            aSheet.AddRows(rowIndex, 1)
                            SetTukurikataRowPro(aSheet, rowIndex)
                            columnIndex = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX

                            'Dim patternName As String = String.Empty

                            For Each tukurikataVo As TYosanTukurikataVo In vosByHyojijun(unitKbn)(buhinhyoName)(patternName)
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

                SetRowBackColorGray(aSheet)

                '台数合計
                CalcTukurikata(aSheet)
            End If

            '固定列を設定
            aSheet.FrozenColumnCount = SPD_TUKURIKATA_KOTEI_COLUMN_COUNT
            '固定行を設定
            aSheet.FrozenRowCount = SPD_TUKURIKATA_KOTEI_ROW_COUNT
            IsSuspend_SpreadChangeEvent = True

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
        Private Sub SetRowBackColorGray(ByVal aSheet As SheetView)
            '予算書部品編集ﾊﾟﾀｰﾝ情報取得
            Dim patternVos As List(Of TYosanBuhinEditPatternVo) = editDao.FindYosanBuhinEditPatternBy(aYosanEventCode)

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
            IsSuspend_SpreadChangeEvent = False

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

            IsSuspend_SpreadChangeEvent = True
        End Sub
#End Region


#Region "予算書部品表選択情報"
        ''' <summary>
        ''' 予算書部品表選択情報
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property YosanBuhinhyoList() As List(Of TYosanBuhinSelectVo)
            Get
                Dim aList As New List(Of String)
                Dim buhinhyoNameVos As List(Of TYosanBuhinSelectVo) = editDao.FindBuhinhyoNameBy(aYosanEventCode)

                Return buhinhyoNameVos
            End Get
        End Property
#End Region


#Region "造り方情報 "
        ''' <summary>
        ''' パターン情報
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property YosanBuhinEditPatternList() As List(Of TYosanBuhinEditPatternVo)
            Get
                Dim aList As New List(Of String)
                Dim patternNameVos As List(Of TYosanBuhinEditPatternVo) = editDao.FindPatternNameBy(aYosanEventCode)

                Return patternNameVos
            End Get
        End Property

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
        Public ReadOnly Property YosanTukurikataKanseiList() As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo))))
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
        Public ReadOnly Property YosanTukurikataWbList() As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo))))
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
        Private Function MakeTukurikataVosByHyojijun(ByVal aList As List(Of TYosanTukurikataVo)) As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo))))
            Dim vosByHyojijun As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo))))

            For Each vo As TYosanTukurikataVo In aList
                If Not vosByHyojijun.ContainsKey(vo.UnitKbn) Then
                    vosByHyojijun.Add(vo.UnitKbn, New Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo))))
                End If
                If Not vosByHyojijun(vo.UnitKbn).ContainsKey(vo.BuhinhyoName) Then
                    vosByHyojijun(vo.UnitKbn).Add(vo.BuhinhyoName, New Dictionary(Of String, List(Of TYosanTukurikataVo)))
                End If
                If Not vosByHyojijun(vo.UnitKbn)(vo.BuhinhyoName).ContainsKey(vo.PatternName) Then
                    vosByHyojijun(vo.UnitKbn)(vo.BuhinhyoName).Add(vo.PatternName, New List(Of TYosanTukurikataVo))
                End If
                vosByHyojijun(vo.UnitKbn)(vo.BuhinhyoName)(vo.PatternName).Add(vo)
            Next

            Return vosByHyojijun
        End Function
        ''' <summary>
        ''' 造り方スプレッドの行追加
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AddYosanTukurikataRow(ByVal aSheet As SheetView, ByVal unitKbn As String, _
                                         ByVal BuhinhyoName As String, ByVal patternName As String, _
                                         ByVal shisakuSyubetu As String)
            IsSuspend_SpreadChangeEvent = False

            Dim rowIndex As Integer = 0
            rowIndex = PatternNameInsertRowIndex(shisakuSyubetu, unitKbn)

            If rowIndex < SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX Then
                rowIndex = aSheet.RowCount - 1
            End If
            aSheet.AddRows(rowIndex, 1)
            SetTukurikataRowPro(aSheet, rowIndex)

            'ユニット区分をセット
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).Value = unitKbn
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).Value = UnitKbnName(unitKbn)
            '部品表名をセット
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX).Value = BuhinhyoName
            'パターン名
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX).Value = patternName
            '部品費と型費
            If IsKanseisha(shisakuSyubetu) Then
                SetSpreadTukurikataBuhinHi(True, False)
            Else
                SetSpreadTukurikataBuhinHi(False, True)
            End If

            '追加行のIndexをリセット
            ResetTukurikataInsertRowIndex(aSheet, shisakuSyubetu)

            Dim rowSpan As Integer = 0
            If PatternNameList(aSheet).ContainsKey(unitKbn) Then
                rowSpan = PatternNameList(aSheet)(unitKbn).Count
            End If
            aSheet.Cells(rowIndex - rowSpan + 1, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).RowSpan = rowSpan

            If aSheet.ColumnCount > SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT Then
                Dim addVos As New List(Of TYosanTukurikataVo)
                For colIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To aSheet.ColumnCount - 2
                    '合計列を判断
                    If (colIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) > 0 Then
                        '台数セル
                        SetMonthCellPro(aSheet, rowIndex, colIndex, False)

                        Dim addVo As New TYosanTukurikataVo
                        addVo.YosanEventCode = aYosanEventCode
                        addVo.ShisakuSyubetu = shisakuSyubetu
                        addVo.UnitKbn = unitKbn
                        addVo.BuhinhyoName = BuhinhyoName
                        addVo.PatternName = patternName
                        Dim year As String = aSheet.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, colIndex).ToString.Substring(0, 4)
                        Dim ks As String = aSheet.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, colIndex).ToString.Substring(4, 2)
                        Dim month As String = aSheet.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, colIndex).ToString.Replace("月", "")
                        Dim yearMonth As String = ChangeSpreadYearToDBYear(year, ks, CInt(month).ToString("00"))
                        addVo.YosanTukurikataYyyyMm = yearMonth
                        addVo.YosanTukurikataKs = ks
                        addVos.Add(addVo)
                    Else
                        '台数セル
                        SetMonthCellPro(aSheet, rowIndex, colIndex, True)
                    End If
                Next
                If IsKanseisha(shisakuSyubetu) Then
                    _KanseiTukurikataList.AddRange(addVos)
                Else
                    _WbTukurikataList.AddRange(addVos)
                End If
            End If

            SetRowBackColorGray(aSheet)

            IsSuspend_SpreadChangeEvent = True
        End Sub
        ''' <summary>
        ''' 造り方スプレッドの行削除
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <param name="unitKbn"></param>
        ''' <param name="patternName"></param>
        ''' <remarks></remarks>
        Public Sub RemoveYosanTukurikataRow(ByVal aSheet As SheetView, ByVal rowIndex As Integer, _
                                            ByVal unitKbn As String, ByVal buhinhyoName As String, _
                                            ByVal patternName As String, ByVal shisakuSyubetu As String)
            IsSuspend_SpreadChangeEvent = False

            aSheet.RemoveRows(rowIndex, 1)
            SetRowBackColorGray(aSheet)

            'unitKbn = UnitKbnCode(unitKbn)

            '追加行のIndexをリセット
            ResetTukurikataInsertRowIndex(aSheet, shisakuSyubetu)

            Dim rowSpan As Integer = 0
            If PatternNameList(aSheet).ContainsKey(unitKbn) Then
                rowSpan = PatternNameList(aSheet)(unitKbn).Count
            End If
            If rowSpan > 0 Then
                For index As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To aSheet.RowCount - 2
                    If String.Equals(unitKbn, aSheet.GetValue(index, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                        aSheet.Cells(index, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).RowSpan = rowSpan
                        Exit For
                    End If
                Next
            Else
                PatternNameInsertRowIndex(shisakuSyubetu, unitKbn) = aSheet.RowCount - 1
            End If

            Dim compareList As List(Of TYosanTukurikataVo)
            If IsKanseisha(shisakuSyubetu) Then
                compareList = _KanseiTukurikataList
            Else
                compareList = _WbTukurikataList
            End If

            Dim newList As New List(Of TYosanTukurikataVo)
            For Each Vo As TYosanTukurikataVo In compareList
                If Not (String.Equals(Vo.PatternName, patternName) AndAlso _
                        String.Equals(Vo.BuhinhyoName, buhinhyoName) AndAlso String.Equals(Vo.UnitKbn, unitKbn)) Then
                    newList.Add(Vo)
                End If
            Next
            If IsKanseisha(shisakuSyubetu) Then
                _KanseiTukurikataList = newList
            Else
                _WbTukurikataList = newList
            End If

            IsSuspend_SpreadChangeEvent = True
        End Sub
        ''' <summary>
        ''' 追加行のIndexをリセット
        ''' </summary>
        ''' <param name="sheet"></param>
        ''' <remarks></remarks>
        Private Sub ResetTukurikataInsertRowIndex(ByVal sheet As SheetView, ByVal shisakuSyubetu As String)
            Dim unitKbn As String = sheet.GetValue(SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)
            For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX + 1 To sheet.RowCount - 2
                If Not String.Equals(unitKbn, sheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                    PatternNameInsertRowIndex(shisakuSyubetu, unitKbn) = rowIndex
                    unitKbn = UnitKbnCode(sheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX))
                    Exit For
                End If
            Next
            PatternNameInsertRowIndex(shisakuSyubetu, unitKbn) = sheet.RowCount - 1
        End Sub
        ''' <summary>
        ''' 造り方スプレッドの列挿入
        ''' </summary>
        ''' <param name="year"></param>
        ''' <param name="ks"></param>
        ''' <param name="columnIndex"></param>
        ''' <param name="delFlag"></param>
        ''' <remarks></remarks>
        Public Sub AddYosanTukurikataColumn(ByVal aSheet As SheetView, ByVal year As String, ByVal ks As String, _
                                            ByVal columnIndex As Integer, ByVal delFlag As Boolean, ByVal shisakuSyubetu As String)
            IsSuspend_SpreadChangeEvent = False

            If delFlag Then
                aSheet.RemoveColumns(columnIndex, 1)
            End If
            aSheet.AddColumns(columnIndex, HALF_MONTH_DEFAULT_COUNT)

            Dim addVos As New List(Of TYosanTukurikataVo)
            For index As Integer = 0 To HALF_MONTH_DEFAULT_COUNT - 1
                Dim month As Integer
                Dim col As Integer = index + columnIndex
                SetMonthTitlePro(aSheet, col, SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX, _
                                  SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, SPD_TUKURIKATA_YEAR_ROW_INDEX, SPD_TUKURIKATA_MONTH_ROW_INDEX)
                '年
                aSheet.Cells(SPD_TUKURIKATA_YEAR_ROW_INDEX, col).Value = year & ks
                '月
                If String.Equals(ks, UP_KS) Then
                    month = CInt(4 + index)
                    aSheet.Cells(SPD_TUKURIKATA_MONTH_ROW_INDEX, col).Value = month.ToString & "月"
                Else
                    If index < 3 Then
                        month = CInt(10 + index)
                        aSheet.Cells(SPD_TUKURIKATA_MONTH_ROW_INDEX, col).Value = month.ToString & "月"
                    Else
                        month = CInt(1 + index - 3)
                        aSheet.Cells(SPD_TUKURIKATA_MONTH_ROW_INDEX, col).Value = month.ToString & "月"
                    End If
                End If

                For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To aSheet.RowCount - 2
                    '台数セル
                    SetMonthCellPro(aSheet, rowIndex, col, False)

                    Dim unitKbn As String = aSheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)
                    Dim buhinhyoName As String = aSheet.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)
                    Dim patternName As String = aSheet.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)

                    Dim addVo As New TYosanTukurikataVo
                    addVo.YosanEventCode = aYosanEventCode
                    addVo.ShisakuSyubetu = shisakuSyubetu
                    addVo.UnitKbn = unitKbn
                    addVo.BuhinhyoName = buhinhyoName
                    addVo.PatternName = patternName
                    Dim yearMonth As String = ChangeSpreadYearToDBYear(year, ks, CInt(month).ToString("00"))
                    addVo.YosanTukurikataYyyyMm = yearMonth
                    addVo.YosanTukurikataKs = ks
                    addVos.Add(addVo)
                Next
            Next
            If IsKanseisha(shisakuSyubetu) Then
                _KanseiTukurikataList.AddRange(addVos)
            Else
                _WbTukurikataList.AddRange(addVos)
            End If

            '合計列を挿入
            columnIndex = columnIndex + HALF_MONTH_DEFAULT_COUNT
            AddSpdMonthCountColumn(aSheet, columnIndex, SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, _
                                   SPD_TUKURIKATA_YEAR_ROW_INDEX, SPD_TUKURIKATA_MONTH_ROW_INDEX)

            '月分布ヘッダー
            aSheet.Cells(SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX).ColumnSpan = aSheet.ColumnCount - SPD_TUKURIKATA_KOTEI_COLUMN_COUNT - 1
            '月分布フッター
            aSheet.Cells(aSheet.RowCount - 1, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX).ColumnSpan = aSheet.ColumnCount - SPD_TUKURIKATA_KOTEI_COLUMN_COUNT - 1

            '挿入列を拡大して表示する
            VisibleMonthColumns(aSheet, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, _
                                SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, columnIndex, SPD_TUKURIKATA_KOTEI_COLUMN_COUNT)

            SetRowBackColorGray(aSheet)

            IsSuspend_SpreadChangeEvent = True
        End Sub
        ''' <summary>
        ''' 造り方スプレッドの列削除
        ''' </summary>
        ''' <param name="columnIndex"></param>
        ''' <param name="month"></param>
        ''' <remarks></remarks>
        Public Sub RemoveYosanTukurikataColumn(ByVal aSheet As SheetView, ByVal columnIndex As Integer, ByVal startRow As Integer, _
                                               ByVal month As String, ByVal shisakuSyubetu As String)
            IsSuspend_SpreadChangeEvent = False

            aSheet.RemoveColumns(columnIndex - HALF_MONTH_DEFAULT_COUNT, (HALF_MONTH_DEFAULT_COUNT + 1))
            If aSheet.ColumnCount < SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT Then
                aSheet.AddColumns(SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, 1)
                SetMonthTitlePro(aSheet, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX, _
                                  SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, SPD_TUKURIKATA_YEAR_ROW_INDEX, SPD_TUKURIKATA_MONTH_ROW_INDEX)
                SetRowBackColorGray(aSheet)
            End If
            '台数セル
            For rowIndex As Integer = startRow To aSheet.RowCount - 2
                SetMonthCellPro(aSheet, rowIndex, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, True)
            Next

            '月分布ヘッダー
            aSheet.Cells(SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX).ColumnSpan = aSheet.ColumnCount - SPD_TUKURIKATA_KOTEI_COLUMN_COUNT - 1
            aSheet.Cells(SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX).BackColor = Color.Khaki
            aSheet.Cells(SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX).Value = "月分布"
            aSheet.Cells(SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '月分布フッター
            aSheet.Cells(aSheet.RowCount - 1, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX).ColumnSpan = aSheet.ColumnCount - SPD_TUKURIKATA_KOTEI_COLUMN_COUNT - 1

            Dim compareList As List(Of TYosanTukurikataVo)
            If IsKanseisha(shisakuSyubetu) Then
                compareList = _KanseiTukurikataList
            Else
                compareList = _WbTukurikataList
            End If

            Dim year As String = month.Substring(0, 4)
            Dim ks As String = month.Remove(0, 4)
            Dim yearMonth As List(Of String) = GetDBYearMonth(year, ks)

            Dim newList As New List(Of TYosanTukurikataVo)
            For Each Vo As TYosanTukurikataVo In compareList
                If Not (yearMonth.Contains(Vo.YosanTukurikataYyyyMm) AndAlso String.Equals(Vo.YosanTukurikataKs, ks)) Then
                    newList.Add(Vo)
                End If
            Next
            If IsKanseisha(shisakuSyubetu) Then
                _KanseiTukurikataList = newList
            Else
                _WbTukurikataList = newList
            End If

            '合計
            CalcTukurikata(aSheet)

            IsSuspend_SpreadChangeEvent = True
        End Sub
#End Region

#End Region

#Region "集計値"
        ''' <summary>
        ''' 集計値スプレッドにデータ設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DispSpreadZaimuJiseki()
            For rowIndex As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To m_view.spdZaimuJiseki_Sheet1.RowCount - 1
                '計の列をフォーマット
                'm_view.spdZaimuJiseki_Sheet1.Cells(rowIndex, m_view.spdZaimuJiseki_Sheet1.ColumnCount - 1).CellType = GetKingaku11CellType()
                m_view.spdZaimuJiseki_Sheet1.Cells(rowIndex, m_view.spdZaimuJiseki_Sheet1.ColumnCount - 1).HorizontalAlignment = CellHorizontalAlignment.Right
                m_view.spdZaimuJiseki_Sheet1.Cells(rowIndex, m_view.spdZaimuJiseki_Sheet1.ColumnCount - 1).VerticalAlignment = CellVerticalAlignment.Center
            Next
            '「計（横計）」の列幅
            m_view.spdZaimuJiseki_Sheet1.Columns(m_view.spdZaimuJiseki_Sheet1.ColumnCount - 1).Width = 95
            '固定列を設定
            m_view.spdZaimuJiseki_Sheet1.FrozenColumnCount = SPD_ZAIMUJISEKI_KOTEI_COLUMN_COUNT

            '再計算
            DoCalc(True)
        End Sub

        ''' <summary>
        ''' 再計算ボタンの処理
        ''' </summary>
        ''' <param name="isInitShow">初期表示の場合はTrue</param>
        ''' <remarks></remarks>
        Public Sub DoCalc(ByVal isInitShow As Boolean)
            Dim colCount As Integer = m_view.spdZaimuJiseki_Sheet1.ColumnCount - SPD_ZAIMUJISEKI_KOTEI_COLUMN_COUNT - 1
            If colCount > 0 Then
                m_view.spdZaimuJiseki_Sheet1.RemoveColumns(SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, colCount)
            End If
            '集計値SPREADの月分布列に列を挿入
            AddZaimuJisekiMonthColumns()

            '造り方Spreadの部品費と型費をセット
            SetSpreadTukurikataBuhinHi(True, True)

            '集計値Spreadに表示値をセット
            SetSpreadZaimujisekiData(isInitShow)
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
        Private Sub AddZaimuJisekiMonthColumns()
            '最小の年、期
            Dim minYearKs As String = GetSpreadMinYearKs()
            '最大の年、期
            Dim maxYearKs As String = GetSpreadMaxYearKs()

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
                    AddSpdMonthCountColumn(m_view.spdZaimuJiseki_Sheet1, columnIndex, SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, _
                                           SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
                    SetMonthColumnProKingaku(m_view.spdZaimuJiseki_Sheet1, columnIndex, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
                                             SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
                    columnIndex = columnIndex + 1
                End If

                m_view.spdZaimuJiseki_Sheet1.AddColumns(columnIndex, 1)
                SetMonthColumnProKingaku(m_view.spdZaimuJiseki_Sheet1, columnIndex, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
                                         SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
                '年
                m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).Value = ChangeYearMonthToYearKs(yearMonths(index).Substring(0, 4), yearMonths(index).Substring(4))
                '月
                m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_MONTH_ROW_INDEX, columnIndex).Value = CInt(yearMonths(index).Substring(4, 2)).ToString & "月"
                columnIndex = columnIndex + 1
            Next
            '合計列追加
            AddSpdMonthCountColumn(m_view.spdZaimuJiseki_Sheet1, columnIndex, SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, _
                                   SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
            SetMonthColumnProKingaku(m_view.spdZaimuJiseki_Sheet1, columnIndex, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
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
        Private Function GetSpreadMinYearKs() As String
            Dim minYearKs As String = Nothing

            '完成車Spreadにデータが有れば
            If m_view.spdTukurikataKanseisha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                'YYYYX期を取得
                Dim kanseiMonth As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + HALF_MONTH_DEFAULT_COUNT)

                minYearKs = kanseiMonth
            End If
            'ﾎﾜｲﾄﾎﾞﾃﾞｨSpreadにデータが有れば
            If m_view.spdTukurikataWBsha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                'YYYYX期を取得
                Dim wbMonth As String = m_view.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + HALF_MONTH_DEFAULT_COUNT)

                If StringUtil.IsNotEmpty(minYearKs) Then
                    minYearKs = IIf(CompareYearKs(minYearKs, wbMonth) = True, minYearKs, wbMonth)
                Else
                    minYearKs = wbMonth
                End If
            End If
            '金材Spreadにデータが有れば
            If m_view.spdKanazai_Sheet1.ColumnCount - SPD_KANAZAI_DEFAULT_COLUMN_COUNT > 0 Then
                Dim kanazaiMonth As String = m_view.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_MONTH_ROW_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + HALF_MONTH_DEFAULT_COUNT)

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
            Dim zaimuJisekiMinYearKs As String = GetYosanZaimuJisekiYearMonth(m_view.lblYosanCode.Text, False)
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
        Private Function GetSpreadMaxYearKs() As String
            Dim maxYearKs As String = Nothing

            '完成車Spreadにデータが有れば
            If m_view.spdTukurikataKanseisha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                'YYYYX期を取得
                Dim kanseiMonth As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, m_view.spdTukurikataKanseisha_Sheet1.ColumnCount - 2)

                maxYearKs = kanseiMonth
            End If
            'ﾎﾜｲﾄﾎﾞﾃﾞｨSpreadにデータが有れば
            If m_view.spdTukurikataWBsha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                'YYYYX期を取得
                Dim wbMonth As String = m_view.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, m_view.spdTukurikataWBsha_Sheet1.ColumnCount - 2)

                If StringUtil.IsNotEmpty(maxYearKs) Then
                    maxYearKs = IIf(CompareYearKs(maxYearKs, wbMonth) = False, maxYearKs, wbMonth)
                Else
                    maxYearKs = wbMonth
                End If
            End If
            '金材Spreadにデータが有れば
            If m_view.spdKanazai_Sheet1.ColumnCount - SPD_KANAZAI_DEFAULT_COLUMN_COUNT > 0 Then
                Dim kanazaiMonth As String = m_view.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_MONTH_ROW_INDEX, m_view.spdKanazai_Sheet1.ColumnCount - 2)

                If StringUtil.IsNotEmpty(maxYearKs) Then
                    maxYearKs = IIf(CompareYearKs(maxYearKs, kanazaiMonth) = False, maxYearKs, kanazaiMonth)
                Else
                    maxYearKs = kanazaiMonth
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
            Dim zaimuJisekiMaxYearKs As String = GetYosanZaimuJisekiYearMonth(m_view.lblYosanCode.Text, True)
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

            Dim yearMonth = editDao.FindYosanZaimuJisekiYyyyMmBy(m_view.lblYosanCode.Text, isMax)
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

            For columnIndex As Integer = SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN To m_view.spdSeisakuDaisu_Sheet1.ColumnCount - 1 Step 2
                Dim kojishireiNo As String = m_view.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex)
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

            If strComp1.Length < 4 Then
                Return False
            End If
            If strComp2.Length < 4 Then
                Return True
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
        Public Sub SetSpreadTukurikataBuhinHi(ByVal updKansei As Boolean, ByVal updWb As Boolean)
            IsSuspend_SpreadChangeEvent = False

            Dim wbKeisu As Decimal = 1
            If StringUtil.IsNotEmpty(m_view.txtWbKeisu.Text) Then

                If IsNumeric(m_view.txtWbKeisu.Text) Then
                    wbKeisu = CDec(m_view.txtWbKeisu.Text)
                End If

            End If



            '予算部品表選択情報を取得する。
            Dim _BuhinhyoVos As List(Of TYosanBuhinSelectVo) = YosanBuhinhyoList

            '予算部品表選択情報分の部品表を出力する。
            For Each vo As TYosanBuhinSelectVo In _BuhinhyoVos

                '見通し部品費
                _MitoshiMetalList = GetMitoshiList(vo.BuhinhyoName)

                '造り方Spreadに部品費を設定
                For Each hiVo As YosanInsuBuhinHiVo In _MitoshiMetalList
                    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataKanseisha_Sheet1.RowCount - 2
                        '部品表名とパターン名
                        If String.Equals(hiVo.BuhinhyoName, m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)) AndAlso _
                           String.Equals(hiVo.PatternName, m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
                            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
                                m_view.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
                            End If
                            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
                                m_view.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
                            End If
                            Exit For
                        End If
                    Next
                    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataWBsha_Sheet1.RowCount - 2
                        '部品表名とパターン名
                        If String.Equals(hiVo.BuhinhyoName, m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)) AndAlso _
                           String.Equals(hiVo.PatternName, m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
                            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
                                m_view.spdTukurikataWBsha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi * wbKeisu
                            End If
                            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
                                m_view.spdTukurikataWBsha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
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
            '    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataKanseisha_Sheet1.RowCount - 2
            '        'ユニット区分とパターン名
            '        If String.Equals(UnitKbnName(hiVo.UnitKbn), m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) AndAlso _
            '           String.Equals(hiVo.PatternName, m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
            '                m_view.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
            '            End If
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
            '                m_view.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
            '            End If
            '            Exit For
            '        End If
            '    Next
            '    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataWBsha_Sheet1.RowCount - 2
            '        'ユニット区分とパターン名
            '        If String.Equals(UnitKbnName(hiVo.UnitKbn), m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) AndAlso _
            '           String.Equals(hiVo.PatternName, m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
            '                m_view.spdTukurikataWBsha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi * wbKeisu
            '            End If
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
            '                m_view.spdTukurikataWBsha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
            '            End If
            '            Exit For
            '        End If
            '    Next
            'Next

            ''見通しトリム部品費
            '_MitoshiTrimList = GetMitoshiList(UNIT_KBN_TRIM_CODE)

            ''造り方Spreadに部品費を設定(トリム)
            'For Each hiVo As YosanInsuBuhinHiVo In _MitoshiTrimList
            '    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataKanseisha_Sheet1.RowCount - 2
            '        'ユニット区分とパターン名→　部品表名とパターン名
            '        If String.Equals(hiVo.BuhinhyoName, m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)) AndAlso _
            '           String.Equals(hiVo.PatternName, m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
            '                m_view.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
            '            End If
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
            '                m_view.spdTukurikataKanseisha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
            '            End If
            '            Exit For
            '        End If
            '    Next
            '    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataWBsha_Sheet1.RowCount - 2
            '        'ユニット区分とパターン名→　部品表名とパターン名
            '        If String.Equals(hiVo.BuhinhyoName, m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)) AndAlso _
            '           String.Equals(hiVo.PatternName, m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
            '                m_view.spdTukurikataWBsha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi * wbKeisu
            '            End If
            '            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
            '                m_view.spdTukurikataWBsha_Sheet1.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
            '            End If
            '            Exit For
            '        End If
            '    Next
            'Next

            IsSuspend_SpreadChangeEvent = True
        End Sub

        ''' <summary>
        ''' 部品費情報取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetMitoshiList(ByVal unitKbn As String) As List(Of YosanInsuBuhinHiVo)

            Dim yosanBuhinEditVos As List(Of TYosanBuhinEditVo) = FindBuhinEditByUnitKbn(unitKbn)

            Dim yosanBuhinEditInsuVos As New List(Of TYosanBuhinEditInsuVo)
            Dim yosanBuhinEditPatternVos As New List(Of TYosanBuhinEditPatternVo)
            Dim instlVosByHyojijun As New Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo))
            '予算部品編集情報を取得できた場合
            If yosanBuhinEditVos.Count = 0 Then
                Return New List(Of YosanInsuBuhinHiVo)
            End If
            yosanBuhinEditInsuVos = FindBuhinEditInsuByUnitKbn(unitKbn)

            '予算部品編集パターン情報取得
            yosanBuhinEditPatternVos = FindBuhinEditPatternByUnitKbn(unitKbn)

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
        Private Function FindBuhinEditByUnitKbn(ByVal unitKbn As String) As List(Of TYosanBuhinEditVo)
            Return exportDao.FindYosanBuhinEdit(aYosanEventCode, unitKbn)
        End Function

        ''' <summary>
        ''' 部品表員数情報取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindBuhinEditInsuByUnitKbn(ByVal unitKbn As String) As List(Of TYosanBuhinEditInsuVo)
            Return exportDao.FindYosanBuhinEditInsu(aYosanEventCode, unitKbn)
        End Function

        ''' <summary>
        ''' 部品表パターン情報取得
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindBuhinEditPatternByUnitKbn(ByVal unitKbn As String) As List(Of TYosanBuhinEditPatternVo)
            Return exportDao.FindYosanBuhinEditPattern(aYosanEventCode, unitKbn)
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
        Private Sub SetSpreadZaimujisekiData(ByVal isInitShow As Boolean)
            IsSuspend_SpreadChangeEvent = False

            '比例費と固定費の見通し部品費（メタル／トリム）を取得
            GetMitoshiMetalAndTrimData()

            '比例費と固定費の財務実績を取得
            _ZaimujisekiList = editDao.FindYosanZaimuJisekiBy(m_view.lblYosanCode.Text)

            '比例費の見通し部品費以外を取得
            _EventMitoshiList = editDao.FindYosanEventMitoshiBy(aYosanEventCode)

            '比例費の見通し移管車＆生産部実績を取得
            _SeisanbuJisekiList = New Dictionary(Of String, Decimal)
            If Not isInitShow Then
                GetMitoshiSeisanbuJisekiData()
            End If

            '発注実績
            GetHatchuJisekiData()

            '集計値Spreadに比例費と固定費の見通しの部品費を設定
            For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To m_view.spdZaimuJiseki_Sheet1.ColumnCount - 2
                '合計列以外
                If Not ((columnIndex - SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                    Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view.spdZaimuJiseki_Sheet1.GetValue(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                       m_view.spdZaimuJiseki_Sheet1.GetValue(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                       CInt(m_view.spdZaimuJiseki_Sheet1.GetValue(SPD_ZAIMUJISEKI_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                    '比例費見通しのメタル部品費
                    For Each key As String In _HireiMetalHiList.Keys
                        If String.Equals(yearMonth, key) Then
                            m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, columnIndex).Value = _HireiMetalHiList(key)
                            Exit For
                        End If
                    Next
                    '比例費見通しのトリム部品費
                    For Each key As String In _HireiTrimHiList.Keys
                        If String.Equals(yearMonth, key) Then
                            m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX, columnIndex).Value = _HireiTrimHiList(key)
                            Exit For
                        End If
                    Next
                    '固定費見通しのメタル部品費
                    For Each key As String In _KoteiMetalHiList.Keys
                        If String.Equals(yearMonth, key) Then
                            m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, columnIndex).Value = _KoteiMetalHiList(key)
                            Exit For
                        End If
                    Next
                    '固定費見通しのトリム部品費
                    For Each key As String In _KoteiTrimHiList.Keys
                        If String.Equals(yearMonth, key) Then
                            m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX, columnIndex).Value = _KoteiTrimHiList(key)
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
                                    m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                                '鋼板材料
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU) Then
                                    m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                                '輸送費
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_YUSOU) Then
                                    m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                                '移管車＆生産部実績
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_SEISAN) Then
                                    m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                                'トリム部品費
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_TRIM) Then
                                    m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                            End If

                            '固定費の場合
                            If IsKotei(zaimuVo.YosanZaimuHireiKoteiKbn) Then
                                'メタル部品費
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_KOTEI_METAL) Then
                                    m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                                'トリム部品費
                                If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_KOTEI_TRIM) Then
                                    m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                                End If
                            End If
                        End If
                    Next
                    '見通し
                    For Each mitoshiVo As TYosanEventMitoshiVo In _EventMitoshiList
                        If String.Equals(yearMonth, mitoshiVo.YosanEventMitoshiYyyyMm) Then
                            '鋼板材料
                            If String.Equals(mitoshiVo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU) Then
                                m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX, columnIndex).Value = mitoshiVo.YosanEventMitoshiInputQty
                            End If
                            '輸送費
                            If String.Equals(mitoshiVo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_YUSOU) Then
                                m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX, columnIndex).Value = mitoshiVo.YosanEventMitoshiInputQty
                            End If
                            '移管車＆生産部実績
                            If String.Equals(mitoshiVo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_SEISAN) Then
                                m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX, columnIndex).Value = mitoshiVo.YosanEventMitoshiInputQty
                            End If
                        End If
                    Next
                    '初期表示以外の場合、移管車＆生産部実績値は金材SPREADのデータを積上げる
                    If Not isInitShow Then
                        For Each key As String In _SeisanbuJisekiList.Keys
                            If String.Equals(yearMonth, key) Then
                                m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX, columnIndex).Value = _SeisanbuJisekiList(key)
                                Exit For
                            End If
                        Next
                    End If
                    '発注実績
                    For Each unitKbn As String In _HireiHatchuJisekiList.Keys
                        For Each keyYearMonth As String In _HireiHatchuJisekiList(unitKbn).Keys
                            If String.Equals(yearMonth, keyYearMonth) Then
                                If IsUnitMetal(unitKbn) Then
                                    m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_METAL_ROWS_INDEX, columnIndex).Value = _HireiHatchuJisekiList(unitKbn)(keyYearMonth)
                                Else
                                    m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_TRIM_ROWS_INDEX, columnIndex).Value = _HireiHatchuJisekiList(unitKbn)(keyYearMonth)
                                End If
                                Exit For
                            End If
                        Next
                    Next
                    For Each unitKbn As String In _KoteiHatchuJisekiList.Keys
                        For Each keyYearMonth As String In _KoteiHatchuJisekiList(unitKbn).Keys
                            If String.Equals(yearMonth, keyYearMonth) Then
                                If IsUnitMetal(unitKbn) Then
                                    m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_METAL_ROWS_INDEX, columnIndex).Value = _KoteiHatchuJisekiList(unitKbn)(keyYearMonth)
                                Else
                                    m_view.spdZaimuJiseki_Sheet1.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_TRIM_ROWS_INDEX, columnIndex).Value = _KoteiHatchuJisekiList(unitKbn)(keyYearMonth)
                                End If
                                Exit For
                            End If
                        Next
                    Next
                End If
            Next

            IsSuspend_SpreadChangeEvent = True
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
            If m_view.spdTukurikataKanseisha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To m_view.spdTukurikataKanseisha_Sheet1.ColumnCount - 2
                    Dim hireiMetalBuhinHi As Decimal = 0
                    Dim hireiTrimBuhinHi As Decimal = 0
                    '合計列以外
                    If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                        Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                           m_view.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                           CInt(m_view.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                        For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataKanseisha_Sheet1.RowCount - 2
                            Dim unitkbn As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)
                            Dim buhinHi As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX)
                            Dim daisu As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, columnIndex)
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
            If m_view.spdTukurikataWBsha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To m_view.spdTukurikataWBsha_Sheet1.ColumnCount - 2
                    Dim hireiMetalBuhinHi As Decimal = 0
                    Dim hireiTrimBuhinHi As Decimal = 0
                    '合計列以外
                    If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                        Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                           m_view.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                           CInt(m_view.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                        For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataWBsha_Sheet1.RowCount - 2
                            Dim unitkbn As String = m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)
                            Dim buhinHi As String = m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX)
                            Dim daisu As String = m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, columnIndex)
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
            If m_view.spdTukurikataKanseisha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataKanseisha_Sheet1.RowCount - 2
                    For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To m_view.spdTukurikataKanseisha_Sheet1.ColumnCount - 2
                        '合計列以外
                        If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                            Dim daisu As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, columnIndex)
                            Dim buhinHi As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                            If StringUtil.IsNotEmpty(daisu) AndAlso CInt(daisu) > 0 AndAlso _
                               StringUtil.IsNotEmpty(buhinHi) AndAlso CDec(buhinHi) > 0 Then
                                Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                                   m_view.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                                   CInt(m_view.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                                'メタルの場合
                                If String.Equals(UNIT_KBN_METAL_CODE, m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    'If String.Equals(UNIT_KBN_METAL_NAME, m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    If CInt(yearMonth) < CInt(metalYearMonth) Then
                                        '最小年月を取得
                                        metalYearMonth = yearMonth
                                        metalBuhinHi = m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                                    End If
                                End If
                                'トリムの場合
                                If String.Equals(UNIT_KBN_TRIM_CODE, m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    'If String.Equals(UNIT_KBN_TRIM_NAME, m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
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
            If m_view.spdTukurikataWBsha_Sheet1.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
                For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataWBsha_Sheet1.RowCount - 2
                    For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To m_view.spdTukurikataWBsha_Sheet1.ColumnCount - 2
                        '合計列以外
                        If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                            Dim daisu As String = m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, columnIndex)
                            Dim buhinHi As String = m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                            If StringUtil.IsNotEmpty(daisu) AndAlso CInt(daisu) > 0 AndAlso _
                               StringUtil.IsNotEmpty(buhinHi) AndAlso CDec(buhinHi) > 0 Then
                                Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                                   m_view.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                                   CInt(m_view.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                                'メタルの場合
                                If String.Equals(UNIT_KBN_METAL_CODE, m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    'If String.Equals(UNIT_KBN_METAL_NAME, m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    If CInt(yearMonth) < CInt(metalYearMonth) Then
                                        '最小年月を取得
                                        metalYearMonth = yearMonth
                                        metalBuhinHi = m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                                    End If
                                End If
                                'トリムの場合
                                If String.Equals(UNIT_KBN_TRIM_CODE, m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    'If String.Equals(UNIT_KBN_TRIM_NAME, m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
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
            If m_view.spdKanazai_Sheet1.ColumnCount - SPD_KANAZAI_DEFAULT_COLUMN_COUNT > 0 Then
                For columnIndex As Integer = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX To m_view.spdKanazai_Sheet1.ColumnCount - 2
                    Dim hiCount As Decimal = 0
                    '合計列以外
                    If Not ((columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                        Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                           m_view.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                           CInt(m_view.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                        For rowIndex As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX To m_view.spdKanazai_Sheet1.RowCount - 2
                            Dim tanka As String = m_view.spdKanazai_Sheet1.GetValue(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX)
                            Dim daisu As String = m_view.spdKanazai_Sheet1.GetValue(rowIndex, columnIndex)
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
            For columnIndex As Integer = SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN To m_view.spdSeisakuDaisu_Sheet1.ColumnCount - 1 Step 2
                Dim kojishireiNo As String = m_view.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex)
                Dim unitKbn As String = UnitKbnCode(m_view.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex))
                'Dim daisuCount As Integer = 0
                ''完成車とＷＢ車の台数合計
                'If StringUtil.IsNotEmpty(m_view.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex)) Then
                '    daisuCount = daisuCount + CInt(m_view.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex))
                'End If
                'If StringUtil.IsNotEmpty(m_view.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex)) Then
                '    daisuCount = daisuCount + CInt(m_view.spdSeisakuDaisu_Sheet1.GetValue(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex))
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
            IsSuspend_SpreadChangeEvent = False

            '月分布列の合計
            For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To m_view.spdZaimuJiseki_Sheet1.ColumnCount - 2
                Dim columnCount As Decimal = 0
                For rowIndex As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To m_view.spdZaimuJiseki_Sheet1.RowCount - 1
                    If rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_HATCHU_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_MITOSHI_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_HATCHU_KEI_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX Then
                        '合計
                        m_view.spdZaimuJiseki_Sheet1.Cells(rowIndex, columnIndex).Value = columnCount

                        columnCount = 0
                        Continue For
                    End If
                    If StringUtil.IsNotEmpty(m_view.spdZaimuJiseki_Sheet1.GetValue(rowIndex, columnIndex)) Then
                        columnCount = columnCount + m_view.spdZaimuJiseki_Sheet1.GetValue(rowIndex, columnIndex)
                    End If
                Next
            Next

            '行の合計
            For rowIndex As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To m_view.spdZaimuJiseki_Sheet1.RowCount - 1
                Dim RowCount As Decimal = 0
                Dim monthCount As Decimal = 0
                For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To m_view.spdZaimuJiseki_Sheet1.ColumnCount - 2
                    '合計列以外
                    If Not ((columnIndex - SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                        If StringUtil.IsNotEmpty(m_view.spdZaimuJiseki_Sheet1.GetValue(rowIndex, columnIndex)) Then
                            monthCount = monthCount + m_view.spdZaimuJiseki_Sheet1.GetValue(rowIndex, columnIndex)
                        End If
                    Else
                        m_view.spdZaimuJiseki_Sheet1.Cells(rowIndex, columnIndex).Value = monthCount
                        RowCount = RowCount + monthCount

                        monthCount = 0
                    End If
                Next
                '合計
                Dim rowCountCol As Integer = m_view.spdZaimuJiseki_Sheet1.ColumnCount - 1
                m_view.spdZaimuJiseki_Sheet1.Cells(rowIndex, rowCountCol).Value = RowCount
            Next

            IsSuspend_SpreadChangeEvent = True
        End Sub
#End Region

#End Region

#Region "登録処理メイン"
        Public Sub Register()
            '予算書イベント別製作台数情報登録
            RegisterYosanSeisakuDaisu()
            '予算書イベント別金材情報登録
            RegisterYosanKanazai()
            '予算書イベント別造り方情報登録
            RegisterYosanTukurikata()
            '予算書イベント別年月別財務実績情報登録
            RegisterYosanZaimujiseki()
            '予算書イベント別見通情報登録
            RegisterYosanEventMitoshi()
            '予算書イベント情報更新
            RegisterUpdateYosanEvent()

        End Sub

        ''' <summary>
        ''' 予算書イベント別製作台数情報登録
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterYosanSeisakuDaisu()
            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    Dim daisuDao As TYosanSeisakuDaisuDao = New TYosanSeisakuDaisuDaoImpl
                    Dim delVo As New TYosanSeisakuDaisuVo
                    delVo.YosanEventCode = aYosanEventCode
                    daisuDao.DeleteBy(delVo)

                    Dim index As Integer = 0
                    For Each insertVo As TYosanSeisakuDaisuVo In _KanseiDaisuList
                        insertVo.YosanEventCode = aYosanEventCode
                        insertVo.ShisakuSyubetu = SHISAKU_SYUBETU_KANSEI
                        insertVo.KoujiShireiNoHyojijunNo = index
                        insertVo.CreatedUserId = aLoginInfo.UserId
                        insertVo.CreatedDate = aSystemDate.CurrentDateDbFormat
                        insertVo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                        insertVo.UpdatedUserId = aLoginInfo.UserId
                        insertVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                        insertVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                        daisuDao.InsertBy(insertVo)
                        index = index + 1
                    Next
                    index = 0
                    For Each insertVo As TYosanSeisakuDaisuVo In _WbDaisuList
                        insertVo.YosanEventCode = aYosanEventCode
                        insertVo.ShisakuSyubetu = SHISAKU_SYUBETU_WB
                        insertVo.KoujiShireiNoHyojijunNo = index
                        insertVo.CreatedUserId = aLoginInfo.UserId
                        insertVo.CreatedDate = aSystemDate.CurrentDateDbFormat
                        insertVo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                        insertVo.UpdatedUserId = aLoginInfo.UserId
                        insertVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                        insertVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                        daisuDao.InsertBy(insertVo)
                        index = index + 1
                    Next
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("予算書イベント別製作台数情報を登録出来ませんでした。")
                End Try
                db.Commit()
            End Using
        End Sub
        ''' <summary>
        ''' 予算書イベント別金材情報登録
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterYosanKanazai()
            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    Dim kanazaiDao As TYosanKanazaiDao = New TYosanKanazaiDaoImpl
                    Dim delVo As New TYosanKanazaiVo
                    delVo.YosanEventCode = aYosanEventCode
                    kanazaiDao.DeleteBy(delVo)

                    Dim index As Integer = 0
                    For Each key As String In YosanKanazaiList.Keys
                        For Each insertVo As TYosanKanazaiVo In YosanKanazaiList(key)
                            insertVo.YosanEventCode = aYosanEventCode
                            insertVo.KanazaiName = key
                            insertVo.KanazaiHyoujiJun = index

                            insertVo.CreatedUserId = aLoginInfo.UserId
                            insertVo.CreatedDate = aSystemDate.CurrentDateDbFormat
                            insertVo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                            insertVo.UpdatedUserId = aLoginInfo.UserId
                            insertVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                            insertVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                            kanazaiDao.InsertBy(insertVo)
                        Next

                        index = index + 1
                    Next
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("予算書イベント別金材情報を登録出来ませんでした。")
                End Try
                db.Commit()
            End Using
        End Sub

        ''' <summary>
        ''' 予算書イベント別造り方情報登録
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterYosanTukurikata()
            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    Dim tukurikataDao As TYosanTukurikataDao = New TYosanTukurikataDaoImpl
                    Dim delVo As New TYosanTukurikataVo
                    delVo.YosanEventCode = aYosanEventCode
                    tukurikataDao.DeleteBy(delVo)

                    Dim index As Integer = 0
                    For Each unitKbn As String In YosanTukurikataKanseiList.Keys
                        For Each buhinhyoName As String In YosanTukurikataKanseiList(unitKbn).Keys
                            For Each patternName As String In YosanTukurikataKanseiList(unitKbn)(buhinhyoName).Keys
                                For Each insertVo As TYosanTukurikataVo In YosanTukurikataKanseiList(unitKbn)(buhinhyoName)(patternName)
                                    insertVo.YosanEventCode = aYosanEventCode
                                    insertVo.ShisakuSyubetu = SHISAKU_SYUBETU_KANSEI
                                    insertVo.PatternHyoujiJun = index

                                    insertVo.CreatedUserId = aLoginInfo.UserId
                                    insertVo.CreatedDate = aSystemDate.CurrentDateDbFormat
                                    insertVo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                                    insertVo.UpdatedUserId = aLoginInfo.UserId
                                    insertVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                                    insertVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                                    tukurikataDao.InsertBy(insertVo)
                                Next
                                index = index + 1
                            Next
                        Next
                    Next

                    index = 0
                    For Each unitKbn As String In YosanTukurikataWbList.Keys
                        For Each buhinhyoName As String In YosanTukurikataWbList(unitKbn).Keys
                            For Each patternName As String In YosanTukurikataWbList(unitKbn)(buhinhyoName).Keys
                                For Each insertVo As TYosanTukurikataVo In YosanTukurikataWbList(unitKbn)(buhinhyoName)(patternName)
                                    insertVo.YosanEventCode = aYosanEventCode
                                    insertVo.ShisakuSyubetu = SHISAKU_SYUBETU_WB
                                    insertVo.PatternHyoujiJun = index

                                    insertVo.CreatedUserId = aLoginInfo.UserId
                                    insertVo.CreatedDate = aSystemDate.CurrentDateDbFormat
                                    insertVo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                                    insertVo.UpdatedUserId = aLoginInfo.UserId
                                    insertVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                                    insertVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                                    tukurikataDao.InsertBy(insertVo)
                                Next
                                index = index + 1
                            Next

                        Next
                    Next
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("予算書イベント別造り方情報を登録出来ませんでした。")
                End Try
                db.Commit()
            End Using
        End Sub

        ''' <summary>
        ''' 予算書イベント別年月別財務実績情報登録
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterYosanZaimujiseki()
            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    Dim zaimuDao As TYosanZaimuJisekiDao = New TYosanZaimuJisekiDaoImpl
                    Dim resultVo As TYosanZaimuJisekiVo
                    For Each vo As TYosanZaimuJisekiVo In _ZaimujisekiList
                        If vo.YosanZaimuInputQty.HasValue Then
                            resultVo = zaimuDao.FindByPk(vo.YosanCode, vo.YosanZaimuJisekiYyyyMm, _
                                                            vo.YosanZaimuHireiKoteiKbn, vo.YosanZaimuJisekiKbn)
                            If resultVo IsNot Nothing Then
                                '該当データが有れば更新
                                resultVo.YosanZaimuInputQty = vo.YosanZaimuInputQty

                                resultVo.UpdatedUserId = aLoginInfo.UserId
                                resultVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                                resultVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                                zaimuDao.UpdateByPk(resultVo)
                            Else
                                '該当データが無ければ登録
                                vo.CreatedUserId = aLoginInfo.UserId
                                vo.CreatedDate = aSystemDate.CurrentDateDbFormat
                                vo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                                vo.UpdatedUserId = aLoginInfo.UserId
                                vo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                                vo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                                zaimuDao.InsertBy(vo)
                            End If
                        End If
                    Next
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("予算書イベント別年月別財務実績情報を登録出来ませんでした。")
                End Try
                db.Commit()
            End Using
        End Sub

        ''' <summary>
        ''' 予算書イベント別見通情報登録
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterYosanEventMitoshi()
            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    Dim mitoshiDao As TYosanEventMitoshiDao = New TYosanEventMitoshiDaoImpl
                    Dim resultVo As TYosanEventMitoshiVo
                    For Each vo As TYosanEventMitoshiVo In _EventMitoshiList
                        If vo.YosanEventMitoshiInputQty.HasValue Then
                            resultVo = mitoshiDao.FindByPk(vo.YosanEventCode, vo.YosanEventMitoshiYyyyMm, vo.YosanEventMitoshiKbn)
                            If resultVo IsNot Nothing Then
                                '該当データが有れば更新
                                resultVo.YosanEventMitoshiInputQty = vo.YosanEventMitoshiInputQty

                                resultVo.UpdatedUserId = aLoginInfo.UserId
                                resultVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                                resultVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                                mitoshiDao.UpdateByPk(resultVo)
                            Else
                                '該当データが無ければ登録
                                vo.CreatedUserId = aLoginInfo.UserId
                                vo.CreatedDate = aSystemDate.CurrentDateDbFormat
                                vo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                                vo.UpdatedUserId = aLoginInfo.UserId
                                vo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                                vo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                                mitoshiDao.InsertBy(vo)
                            End If
                        End If
                    Next

                    For Each key As String In _SeisanbuJisekiList.Keys
                        If StringUtil.IsNotEmpty(_SeisanbuJisekiList(key)) AndAlso CDec(_SeisanbuJisekiList(key)) > 0 Then
                            resultVo = mitoshiDao.FindByPk(aYosanEventCode, key, ZAIMU_JISEKI_KBN_HIREI_SEISAN)
                            If resultVo IsNot Nothing Then
                                '該当データが有れば更新
                                resultVo.YosanEventMitoshiInputQty = CDec(_SeisanbuJisekiList(key))

                                resultVo.UpdatedUserId = aLoginInfo.UserId
                                resultVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                                resultVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                                mitoshiDao.UpdateByPk(resultVo)
                            Else
                                '該当データが無ければ登録
                                Dim insertVo As New TYosanEventMitoshiVo
                                insertVo.YosanEventCode = aYosanEventCode
                                insertVo.YosanEventMitoshiYyyyMm = key
                                insertVo.YosanEventMitoshiKbn = ZAIMU_JISEKI_KBN_HIREI_SEISAN
                                insertVo.YosanEventMitoshiInputQty = CDec(_SeisanbuJisekiList(key))

                                insertVo.CreatedUserId = aLoginInfo.UserId
                                insertVo.CreatedDate = aSystemDate.CurrentDateDbFormat
                                insertVo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                                insertVo.UpdatedUserId = aLoginInfo.UserId
                                insertVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                                insertVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                                mitoshiDao.InsertBy(insertVo)
                            End If
                        End If
                    Next
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("予算書イベント別見通情報を登録出来ませんでした。")
                End Try
                db.Commit()
            End Using
        End Sub

        ''' <summary>
        ''' 予算書イベント情報更新
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterUpdateYosanEvent()
            Using db As New EBomDbClient
                db.BeginTransaction()
                'Try
                Dim yearDateMin As String = GetSpreadMinYearKs()
                Dim yearDateMax As String = GetSpreadMaxYearKs()
                Dim yearFrom As String
                Dim ksFrom As String
                Dim yearTo As String
                Dim ksTo As String
                'データ有れば更新
                If Not (StringUtil.IsEmpty(yearDateMin) AndAlso StringUtil.IsEmpty(yearDateMax)) Then
                    If StringUtil.IsEmpty(yearDateMin) AndAlso StringUtil.IsNotEmpty(yearDateMax) Then
                        '最大年月のみ有れば
                        yearFrom = yearDateMax.Substring(0, 4)
                        ksFrom = yearDateMax.Substring(4)
                        yearTo = yearDateMax.Substring(0, 4)
                        ksTo = yearDateMax.Substring(4)
                    ElseIf StringUtil.IsNotEmpty(yearDateMin) AndAlso StringUtil.IsEmpty(yearDateMax) Then
                        '最小年月のみ有れば
                        yearFrom = yearDateMin.Substring(0, 4)
                        ksFrom = yearDateMin.Substring(4)
                        yearTo = yearDateMin.Substring(0, 4)
                        ksTo = yearDateMin.Substring(4)
                    Else
                        yearFrom = yearDateMin.Substring(0, 4)
                        ksFrom = yearDateMin.Substring(4)
                        yearTo = yearDateMax.Substring(0, 4)
                        ksTo = yearDateMax.Substring(4)
                    End If

                    'W/B係数
                    Dim WbKeisu As Decimal? = Nothing
                    If StringUtil.IsNotEmpty(m_view.txtWbKeisu.Text) Then
                        WbKeisu = CDec(m_view.txtWbKeisu.Text)
                    End If
                    '予算製作台数・完成車
                    Dim KanseiDaisu As Integer = 0
                    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataKanseisha_Sheet1.RowCount - 2
                        Dim unitKbn As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)
                        If IsUnitTrim(UnitKbnCode(unitKbn)) Then
                            Dim daisu As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX)
                            If StringUtil.IsNotEmpty(daisu) Then
                                KanseiDaisu = KanseiDaisu + CInt(daisu)
                            End If
                        End If
                    Next
                    '予算製作台数・W/B
                    Dim WbDaisu As Integer = 0
                    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To m_view.spdTukurikataWBsha_Sheet1.RowCount - 2
                        Dim unitKbn As String = m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)
                        If IsUnitTrim(UnitKbnCode(unitKbn)) Then
                            Dim daisu As Integer = m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX)
                            If StringUtil.IsNotEmpty(daisu) Then
                                WbDaisu = WbDaisu + CInt(daisu)
                            End If
                        End If
                    Next

                    Dim yosanEventDao As TYosanEventDao = New TYosanEventDaoImpl
                    Dim eventVo As TYosanEventVo = yosanEventDao.FindByPk(aYosanEventCode)
                    If eventVo IsNot Nothing Then
                        '該当データが有れば更新
                        '予算期間
                        eventVo.YosanKikanFromYyyy = yearFrom
                        eventVo.YosanKikanFromKs = ksFrom
                        eventVo.YosanKikanToYyyy = yearTo
                        eventVo.YosanKikanToKs = ksTo
                        '予算製作台数
                        eventVo.YosanSeisakudaisuKanseisya = KanseiDaisu
                        eventVo.YosanSeisakudaisuWb = WbDaisu
                        'W/B係数
                        eventVo.YosanWbKeisu = WbKeisu

                        eventVo.UpdatedUserId = aLoginInfo.UserId
                        eventVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                        eventVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                        yosanEventDao.UpdateByPk(eventVo)
                    Else
                        '該当データが無ければ登録
                        eventVo = New TYosanEventVo
                        eventVo.YosanEventCode = aYosanEventCode
                        eventVo.YosanEventKbnName = m_view.lblKubun.Text
                        eventVo.YosanKaihatsuFugo = aYosanEventCode.Substring(0, 3)
                        eventVo.YosanEvent = m_view.lblEvent.Text
                        eventVo.YosanEventName = m_view.lblEventName.Text
                        eventVo.YosanCode = m_view.lblYosanCode.Text
                        '予算期間
                        eventVo.YosanKikanFromYyyy = yearFrom
                        eventVo.YosanKikanFromKs = ksFrom
                        eventVo.YosanKikanToYyyy = yearTo
                        eventVo.YosanKikanToKs = ksTo
                        '予算製作台数
                        eventVo.YosanSeisakudaisuKanseisya = KanseiDaisu
                        eventVo.YosanSeisakudaisuWb = WbDaisu
                        'W/B係数
                        eventVo.YosanWbKeisu = WbKeisu

                        eventVo.CreatedUserId = aLoginInfo.UserId
                        eventVo.CreatedDate = aSystemDate.CurrentDateDbFormat
                        eventVo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                        eventVo.UpdatedUserId = aLoginInfo.UserId
                        eventVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                        eventVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                        yosanEventDao.InsertBy(eventVo)
                    End If
                End If

                'Catch ex As Exception
                '    db.Rollback()
                '    MsgBox("予算書イベント情報更新を登録出来ませんでした。")
                'End Try
                db.Commit()
            End Using
        End Sub

#End Region

#Region "排他管理"
        ''' <summary>
        ''' 排他管理予算書イベント情報削除
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DeleteExclusiveEvent()

            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    '予算イベントコードで「排他管理予算書イベント情報」を検索する。
                    Dim exclusiveVo As New TYosanExclusiveControlEventVo
                    exclusiveVo.YosanEventCode = aYosanEventCode
                    Dim exclusiveVos As List(Of TYosanExclusiveControlEventVo) = exclusiveEventDao.FindBy(exclusiveVo)

                    If exclusiveVos IsNot Nothing And exclusiveVos.Count > 0 Then
                        '該当データを削除する。
                        For Each Vo As TYosanExclusiveControlEventVo In exclusiveVos
                            exclusiveEventDao.DeleteBy(Vo)
                        Next
                    End If
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("排他管理予算書イベント情報を削除出来ませんでした。")
                End Try
                db.Commit()
            End Using

        End Sub

        ''' <summary>
        ''' 排他管理予算書部品情報作成
        ''' </summary>
        ''' <param name="eventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DoExclusiveBuhin(ByVal eventCode As String, ByVal unitKbn As String) As Boolean
            '予算イベントコードで「排他管理予算書部品情報」を検索する。
            Dim exclusiveVos As List(Of TYosanExclusiveControlBuhinVo) = FindExclusiveBuhin(eventCode, unitKbn)

            '「排他管理予算書部品情報」より「編集者職番」を取得する。
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
                If InsertExclusiveBuhin(eventCode, unitKbn) = False Then
                    Return False
                End If

                Return True
            End If
        End Function

        ''' <summary>
        ''' 排他管理予算書部品情報取得
        ''' </summary>
        ''' <param name="eventCode"></param>
        ''' <param name="unitKbn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FindExclusiveBuhin(ByVal eventCode As String, ByVal unitKbn As String) As List(Of TYosanExclusiveControlBuhinVo)
            Dim exclusiveVo As New TYosanExclusiveControlBuhinVo
            exclusiveVo.YosanEventCode = eventCode
            If StringUtil.IsNotEmpty(unitKbn) Then
                exclusiveVo.BuhinhyoName = unitKbn
            End If

            Return exclusiveBuhinDao.FindBy(exclusiveVo)
        End Function

        ''' <summary>
        ''' 排他管理予算書部品情報作成
        ''' </summary>
        ''' <param name="eventCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function InsertExclusiveBuhin(ByVal eventCode As String, ByVal unitKbn As String) As Boolean

            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    Dim exclusiveVo As New TYosanExclusiveControlBuhinVo
                    exclusiveVo.YosanEventCode = eventCode
                    exclusiveVo.BuhinhyoName = unitKbn
                    exclusiveVo.EditUserId = aLoginInfo.UserId
                    exclusiveVo.EditDate = aSystemDate.CurrentDateDbFormat
                    exclusiveVo.EditTime = aSystemDate.CurrentTimeDbFormat
                    exclusiveVo.CreatedUserId = aLoginInfo.UserId
                    exclusiveVo.CreatedDate = aSystemDate.CurrentDateDbFormat
                    exclusiveVo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                    exclusiveVo.UpdatedUserId = aLoginInfo.UserId
                    exclusiveVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                    exclusiveVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat
                    exclusiveBuhinDao.InsertBy(exclusiveVo)
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("排他管理予算書部品情報を作成出来ませんでした。")
                    Return False
                End Try
                db.Commit()
            End Using

            Return True
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
            VisibleSystemYearMonthColumns(m_view.spdZaimuJiseki_Sheet1, SPD_ZAIMUJISEKI_DEFAULT_COLUMN_COUNT, _
                                          SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_KOTEI_COLUMN_COUNT, _
                                          SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
            '金材SPREAD
            VisibleSystemYearMonthColumns(m_view.spdKanazai_Sheet1, SPD_KANAZAI_DEFAULT_COLUMN_COUNT, _
                                          SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_KOTEI_COLUMN_COUNT, _
                                          SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
            '造り方完成車SPREAD
            VisibleSystemYearMonthColumns(m_view.spdTukurikataKanseisha_Sheet1, SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT, _
                                          SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, SPD_TUKURIKATA_KOTEI_COLUMN_COUNT, _
                                          SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, SPD_TUKURIKATA_MONTH_ROW_INDEX)
            '造り方ﾎﾜｲﾄﾎﾞﾃﾞｨSPREAD
            VisibleSystemYearMonthColumns(m_view.spdTukurikataWBsha_Sheet1, SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT, _
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

            If Not sheet.Equals(m_view.spdZaimuJiseki_Sheet1) Then
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

#Region "Spreadの変更処理"
        ''' <summary>
        ''' 製作台数Spreadの変更イベントハンドラ
        ''' </summary>
        ''' <param name="sender">イベントハンドラに従う</param>
        ''' <param name="e">イベントハンドラに従う</param>
        ''' <remarks></remarks>
        Private Sub SeisakuDaisuSpread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            If Not IsSuspend_SpreadChangeEvent Then
                Return
            End If

            Dim rowIndex As Integer = e.Row
            Dim columnIndex As Integer = e.Column
            If rowIndex = -1 Or columnIndex = -1 Then
                Return
            End If
            Dim value As String = m_view.spdSeisakuDaisu_Sheet1.GetValue(rowIndex, columnIndex)

            Dim index As Integer = CInt((columnIndex - SPD_DAISU_KOTEI_COLUMN_COUNT) / 2)

            If columnIndex - SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN >= 0 Then
                '工事指令№
                If rowIndex = SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX Then
                    _KanseiDaisuList(index).KoujiShireiNo = value
                    _WbDaisuList(index).KoujiShireiNo = value
                End If
                '台数
                If rowIndex = SPD_DAISU_KANSEISHA_ROW_INDEX Then
                    _KanseiDaisuList(index).DaisuSuryo = IIf(StringUtil.IsNotEmpty(value), CInt(value), Nothing)
                End If
                If rowIndex = SPD_DAISU_WBSHA_ROW_INDEX Then
                    _WbDaisuList(index).DaisuSuryo = IIf(StringUtil.IsNotEmpty(value), CInt(value), Nothing)
                End If

                CalcTaisu()
            End If
        End Sub
        ''' <summary>
        ''' 金材Spreadの変更イベントハンドラ
        ''' </summary>
        ''' <param name="sender">イベントハンドラに従う</param>
        ''' <param name="e">イベントハンドラに従う</param>
        ''' <remarks></remarks>
        Private Sub KanazaiSpread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            If Not IsSuspend_SpreadChangeEvent Then
                Return
            End If

            Dim rowIndex As Integer = e.Row
            Dim columnIndex As Integer = e.Column
            If rowIndex = -1 Or columnIndex = -1 Then
                Return
            End If

            Dim kanazaiName As String = m_view.spdKanazai_Sheet1.GetValue(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX)
            Dim value As String = m_view.spdKanazai_Sheet1.GetValue(rowIndex, columnIndex)

            If rowIndex - SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX >= 0 Then
                '台数
                If (columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) > 0 Then
                    For Each Vo As TYosanKanazaiVo In _KanazaiList
                        Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                           m_view.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                           CInt(m_view.spdKanazai_Sheet1.GetValue(SPD_KANAZAI_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                        '同一キー
                        If String.Equals(Vo.YosanEventCode, aYosanEventCode) AndAlso _
                           String.Equals(Vo.KanazaiName, kanazaiName) AndAlso _
                           String.Equals(Vo.YosanTukurikataYyyyMm, yearMonth) Then
                            Vo.DaisuSuryo = IIf(StringUtil.IsNotEmpty(value), CInt(value), Nothing)
                            Exit For
                        End If
                    Next
                End If
                '単価
                If columnIndex = SPD_KANAZAI_TANKA_COLUMN_INDEX Then
                    For Each Vo As TYosanKanazaiVo In _KanazaiList
                        '同一金材項目名
                        If String.Equals(Vo.YosanEventCode, aYosanEventCode) AndAlso _
                           String.Equals(Vo.KanazaiName, kanazaiName) Then
                            Vo.KanazaiUnitPrice = IIf(StringUtil.IsNotEmpty(value), CDec(value), Nothing)
                        End If
                    Next
                End If

                CalcKanazai()
            End If
        End Sub
        ''' <summary>
        ''' 完成車Spreadの変更イベントハンドラ
        ''' </summary>
        ''' <param name="sender">イベントハンドラに従う</param>
        ''' <param name="e">イベントハンドラに従う</param>
        ''' <remarks></remarks>
        Private Sub KanseiSpread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            If Not IsSuspend_SpreadChangeEvent Then
                Return
            End If

            Dim rowIndex As Integer = e.Row
            Dim columnIndex As Integer = e.Column
            If rowIndex = -1 Or columnIndex = -1 Then
                Return
            End If

            Dim unitKbn As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)
            'Dim unitKbn As String = UnitKbnCode(m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX))
            Dim buhinhyoName As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)
            Dim patternName As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)
            Dim value As String = m_view.spdTukurikataKanseisha_Sheet1.GetValue(rowIndex, columnIndex)

            If rowIndex - SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX >= 0 Then
                '台数
                If (columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) > 0 Then
                    For Each Vo As TYosanTukurikataVo In _KanseiTukurikataList
                        Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                           m_view.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                           CInt(m_view.spdTukurikataKanseisha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                        '同一キー
                        If String.Equals(Vo.YosanEventCode, aYosanEventCode) AndAlso _
                           String.Equals(Vo.ShisakuSyubetu, SHISAKU_SYUBETU_KANSEI) AndAlso _
                           String.Equals(Vo.UnitKbn, unitKbn) AndAlso _
                           String.Equals(Vo.BuhinhyoName, buhinhyoName) AndAlso _
                           String.Equals(Vo.PatternName, patternName) AndAlso _
                           String.Equals(Vo.YosanTukurikataYyyyMm, yearMonth) Then
                            Vo.DaisuSuryo = IIf(StringUtil.IsNotEmpty(value), CInt(value), Nothing)
                            Exit For
                        End If
                    Next
                End If

                CalcTukurikata(m_view.spdTukurikataKanseisha_Sheet1)
            End If
        End Sub
        ''' <summary>
        ''' ﾎﾜｲﾄﾎﾞﾃﾞｨSpreadの変更イベントハンドラ
        ''' </summary>
        ''' <param name="sender">イベントハンドラに従う</param>
        ''' <param name="e">イベントハンドラに従う</param>
        ''' <remarks></remarks>
        Private Sub WBSpread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            If Not IsSuspend_SpreadChangeEvent Then
                Return
            End If

            Dim rowIndex As Integer = e.Row
            Dim columnIndex As Integer = e.Column
            If rowIndex = -1 Or columnIndex = -1 Then
                Return
            End If

            Dim unitKbn As String = m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)
            'Dim unitKbn As String = UnitKbnCode(m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX))
            Dim buhinhyoName As String = m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)
            Dim patternName As String = m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)
            Dim value As String = m_view.spdTukurikataWBsha_Sheet1.GetValue(rowIndex, columnIndex)

            If rowIndex - SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX >= 0 Then
                '台数
                If (columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) > 0 Then
                    For Each Vo As TYosanTukurikataVo In _WbTukurikataList
                        Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                           m_view.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                           CInt(m_view.spdTukurikataWBsha_Sheet1.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                        '同一キー
                        If String.Equals(Vo.YosanEventCode, aYosanEventCode) AndAlso _
                           String.Equals(Vo.ShisakuSyubetu, SHISAKU_SYUBETU_WB) AndAlso _
                           String.Equals(Vo.UnitKbn, unitKbn) AndAlso _
                           String.Equals(Vo.BuhinhyoName, buhinhyoName) AndAlso _
                           String.Equals(Vo.PatternName, patternName) AndAlso _
                           String.Equals(Vo.YosanTukurikataYyyyMm, yearMonth) Then
                            Vo.DaisuSuryo = IIf(StringUtil.IsNotEmpty(value), CInt(value), Nothing)
                            Exit For
                        End If
                    Next
                End If

                CalcTukurikata(m_view.spdTukurikataWBsha_Sheet1)
            End If
        End Sub
        ''' <summary>
        '''集計値Spreadの変更イベントハンドラ
        ''' </summary>
        ''' <param name="sender">イベントハンドラに従う</param>
        ''' <param name="e">イベントハンドラに従う</param>
        ''' <remarks></remarks>
        Private Sub ZaimujisekiSpread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            If Not IsSuspend_SpreadChangeEvent Then
                Return
            End If

            Dim rowIndex As Integer = e.Row
            Dim columnIndex As Integer = e.Column
            If rowIndex = -1 Or columnIndex = -1 Then
                Return
            End If

            Dim value As String = m_view.spdZaimuJiseki_Sheet1.GetValue(rowIndex, columnIndex)

            If rowIndex - SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX >= 0 Then
                '合計列以外
                If (columnIndex - SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) > 0 Then
                    Dim yearMonth As String = ChangeSpreadYearToDBYear(m_view.spdZaimuJiseki_Sheet1.GetValue(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                       m_view.spdZaimuJiseki_Sheet1.GetValue(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                       CInt(m_view.spdZaimuJiseki_Sheet1.GetValue(SPD_ZAIMUJISEKI_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                    Dim isExist As Boolean
                    If rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX OrElse _
                       rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX Then

                        isExist = False
                        For Each Vo As TYosanZaimuJisekiVo In _ZaimujisekiList
                            '比例費
                            'メタル部品費
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX Then
                                '同一キー
                                If String.Equals(Vo.YosanCode, m_view.lblYosanCode.Text) AndAlso _
                                   String.Equals(Vo.YosanZaimuHireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_METAL) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiYyyyMm, yearMonth) Then
                                    Vo.YosanZaimuInputQty = IIf(StringUtil.IsNotEmpty(value), CDec(value), Nothing)

                                    isExist = True
                                    Exit For
                                End If

                            End If
                            '鋼板材料
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX Then
                                '同一キー
                                If String.Equals(Vo.YosanCode, m_view.lblYosanCode.Text) AndAlso _
                                   String.Equals(Vo.YosanZaimuHireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiYyyyMm, yearMonth) Then
                                    Vo.YosanZaimuInputQty = IIf(StringUtil.IsNotEmpty(value), CDec(value), Nothing)

                                    isExist = True
                                    Exit For
                                End If
                            End If
                            '輸送費
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX Then
                                '同一キー
                                If String.Equals(Vo.YosanCode, m_view.lblYosanCode.Text) AndAlso _
                                   String.Equals(Vo.YosanZaimuHireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_YUSOU) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiYyyyMm, yearMonth) Then
                                    Vo.YosanZaimuInputQty = IIf(StringUtil.IsNotEmpty(value), CDec(value), Nothing)

                                    isExist = True
                                    Exit For
                                End If
                            End If
                            '移管車&生産部実績
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX Then
                                '同一キー
                                If String.Equals(Vo.YosanCode, m_view.lblYosanCode.Text) AndAlso _
                                   String.Equals(Vo.YosanZaimuHireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_SEISAN) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiYyyyMm, yearMonth) Then
                                    Vo.YosanZaimuInputQty = IIf(StringUtil.IsNotEmpty(value), CDec(value), Nothing)

                                    isExist = True
                                    Exit For
                                End If
                            End If
                            'トリム部品費
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX Then
                                '同一キー
                                If String.Equals(Vo.YosanCode, m_view.lblYosanCode.Text) AndAlso _
                                   String.Equals(Vo.YosanZaimuHireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_TRIM) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiYyyyMm, yearMonth) Then
                                    Vo.YosanZaimuInputQty = IIf(StringUtil.IsNotEmpty(value), CDec(value), Nothing)

                                    isExist = True
                                    Exit For
                                End If
                            End If

                            '固定費
                            'メタル型費
                            If rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX Then
                                '同一キー
                                If String.Equals(Vo.YosanCode, m_view.lblYosanCode.Text) AndAlso _
                                   String.Equals(Vo.YosanZaimuHireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_KOTEI_METAL) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiYyyyMm, yearMonth) Then
                                    Vo.YosanZaimuInputQty = IIf(StringUtil.IsNotEmpty(value), CDec(value), Nothing)

                                    isExist = True
                                    Exit For
                                End If
                            End If
                            'トリム型費
                            If rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX Then
                                '同一キー
                                If String.Equals(Vo.YosanCode, m_view.lblYosanCode.Text) AndAlso _
                                   String.Equals(Vo.YosanZaimuHireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_KOTEI_TRIM) AndAlso _
                                   String.Equals(Vo.YosanZaimuJisekiYyyyMm, yearMonth) Then
                                    Vo.YosanZaimuInputQty = IIf(StringUtil.IsNotEmpty(value), CDec(value), Nothing)

                                    isExist = True
                                    Exit For
                                End If
                            End If
                        Next
                        If Not isExist Then
                            Dim addVo As New TYosanZaimuJisekiVo
                            addVo.YosanCode = m_view.lblYosanCode.Text
                            addVo.YosanZaimuJisekiYyyyMm = yearMonth
                            addVo.YosanZaimuInputQty = IIf(StringUtil.IsNotEmpty(value), CDec(value), Nothing)
                            '比例費
                            'メタル部品費
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX Then
                                addVo.YosanZaimuHireiKoteiKbn = HIREI_KOTEI_KBN_HIREI
                                addVo.YosanZaimuJisekiKbn = ZAIMU_JISEKI_KBN_HIREI_METAL
                            End If
                            '鋼板材料
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX Then
                                addVo.YosanZaimuHireiKoteiKbn = HIREI_KOTEI_KBN_HIREI
                                addVo.YosanZaimuJisekiKbn = ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU
                            End If
                            '輸送費
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX Then
                                addVo.YosanZaimuHireiKoteiKbn = HIREI_KOTEI_KBN_HIREI
                                addVo.YosanZaimuJisekiKbn = ZAIMU_JISEKI_KBN_HIREI_YUSOU
                            End If
                            '移管車&生産部実績
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX Then
                                addVo.YosanZaimuHireiKoteiKbn = HIREI_KOTEI_KBN_HIREI
                                addVo.YosanZaimuJisekiKbn = ZAIMU_JISEKI_KBN_HIREI_SEISAN
                            End If
                            'トリム部品費
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX Then
                                addVo.YosanZaimuHireiKoteiKbn = HIREI_KOTEI_KBN_HIREI
                                addVo.YosanZaimuJisekiKbn = ZAIMU_JISEKI_KBN_HIREI_TRIM
                            End If

                            '固定費
                            'メタル型費
                            If rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX Then
                                addVo.YosanZaimuHireiKoteiKbn = HIREI_KOTEI_KBN_KOTEI
                                addVo.YosanZaimuJisekiKbn = ZAIMU_JISEKI_KBN_KOTEI_METAL
                            End If
                            'トリム型費
                            If rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX Then
                                addVo.YosanZaimuHireiKoteiKbn = HIREI_KOTEI_KBN_KOTEI
                                addVo.YosanZaimuJisekiKbn = ZAIMU_JISEKI_KBN_KOTEI_TRIM
                            End If

                            _ZaimujisekiList.Add(addVo)
                        End If
                    End If

                    If rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX OrElse _
                        rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX Then
                        isExist = False
                        For Each Vo As TYosanEventMitoshiVo In _EventMitoshiList
                            '鋼板材料
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX Then
                                '同一キー
                                If String.Equals(Vo.YosanEventCode, aYosanEventCode) AndAlso _
                                   String.Equals(Vo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU) AndAlso _
                                   String.Equals(Vo.YosanEventMitoshiYyyyMm, yearMonth) Then
                                    Vo.YosanEventMitoshiInputQty = IIf(StringUtil.IsNotEmpty(value), CDec(value), Nothing)

                                    isExist = True
                                    Exit For
                                End If
                            End If
                            '輸送費
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX Then
                                '同一キー
                                If String.Equals(Vo.YosanEventCode, m_view.lblYosanCode.Text) AndAlso _
                                   String.Equals(Vo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_YUSOU) AndAlso _
                                   String.Equals(Vo.YosanEventMitoshiYyyyMm, yearMonth) Then
                                    Vo.YosanEventMitoshiInputQty = IIf(StringUtil.IsNotEmpty(value), CDec(value), Nothing)

                                    isExist = True
                                    Exit For
                                End If
                            End If
                        Next
                        If Not isExist Then
                            Dim addVo As New TYosanEventMitoshiVo
                            addVo.YosanEventCode = aYosanEventCode
                            addVo.YosanEventMitoshiYyyyMm = yearMonth
                            addVo.YosanEventMitoshiInputQty = IIf(StringUtil.IsNotEmpty(value), CDec(value), Nothing)
                            '鋼板材料
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX Then
                                addVo.YosanEventMitoshiKbn = ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU
                            End If
                            '輸送費
                            If rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX Then
                                addVo.YosanEventMitoshiKbn = ZAIMU_JISEKI_KBN_HIREI_YUSOU
                            End If

                            _EventMitoshiList.Add(addVo)
                        End If
                    End If
                End If

                CalcZaimuJiseki()
            End If
        End Sub
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

#Region "試作種別"
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



#Region "予算書部品表選択情報更新"

        ''' <summary>
        ''' 予算書部品表選択情報更新
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AddBuhin(ByVal buhinhyoName As String)
            '予算書部品表選択情報更新
            RegisterYosanBuhinhyo(buhinhyoName)
        End Sub

        ''' <summary>
        ''' 予算書部品表選択情報更新
        ''' </summary>
        ''' <param name="buhinhyoName">部品表名</param>
        ''' <remarks></remarks>
        Private Sub RegisterYosanBuhinhyo(ByVal buhinhyoName As String)
            Using db As New EBomDbClient
                db.BeginTransaction()
                Try
                    Dim buhinhyoDao As TYosanBuhinSelectDao = New TYosanBuhinSelectDaoImpl

                    'デリート
                    Dim delVo As New TYosanBuhinSelectVo
                    delVo.YosanEventCode = aYosanEventCode
                    delVo.BuhinhyoName = buhinhyoName
                    buhinhyoDao.DeleteBy(delVo)

                    'インサート
                    Dim insertVo As New TYosanBuhinSelectVo
                    insertVo.YosanEventCode = aYosanEventCode
                    insertVo.BuhinhyoName = buhinhyoName

                    insertVo.CreatedUserId = aLoginInfo.UserId
                    insertVo.CreatedDate = aSystemDate.CurrentDateDbFormat
                    insertVo.CreatedTime = aSystemDate.CurrentTimeDbFormat
                    insertVo.UpdatedUserId = aLoginInfo.UserId
                    insertVo.UpdatedDate = aSystemDate.CurrentDateDbFormat
                    insertVo.UpdatedTime = aSystemDate.CurrentTimeDbFormat

                    buhinhyoDao.InsertBy(insertVo)
                Catch ex As Exception
                    db.Rollback()
                    MsgBox("予算書部品表選択情報を更新出来ませんでした。")
                End Try
                db.Commit()
            End Using
        End Sub



#End Region

    End Class

End Namespace
