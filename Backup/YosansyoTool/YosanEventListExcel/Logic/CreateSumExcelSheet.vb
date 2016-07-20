Imports ShisakuCommon.Db.EBom.Vo
Imports YosansyoTool.YosanshoEdit.Dao
Imports YosansyoTool.YosanshoEdit.Dao.Impl
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.CellType
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports FarPoint.Win
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports YosansyoTool.YosanshoEdit
Imports YosansyoTool.YosanEventListExcel.Dao.Impl

Namespace YosanEventListExcel

    ''' <summary>
    ''' 集計用画面から呼び出されるEXCELシート作成クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CreateSumExcelSheet


#Region "定数"
        '''' <summary>上期下期</summary>
        Private Const UP_KS As String = "上期"
        Private Const DOWN_KS As String = "下期"

#Region "製作台数用"

        ''' <summary>試作種別_完成車</summary>
        Private Const SHISAKU_SYUBETU_KANSEI As String = " "
        ''' <summary>試作種別_ホワイトボディ</summary>
        Private Const SHISAKU_SYUBETU_WB As String = "W"

#Region "行"
        ''' <summary>工事指令№ヘッダーの行番号</summary>
        Private Const SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX As Integer = 30
        ''' <summary>ユニット区分の行番号</summary>
        Private Const SPD_DAISU_UNITKBN_ROW_INDEX As Integer = 31
        ''' <summary>完成車台数行の行番号</summary>
        Private Const SPD_DAISU_KANSEISHA_ROW_INDEX As Integer = 33
        ''' <summary>WB車台数行の行番号</summary>
        Private Const SPD_DAISU_WBSHA_ROW_INDEX As Integer = 34
        ''' <summary>工事指令№データの行番号</summary>
        Private Const SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX As Integer = 32

#End Region

#Region "列"

        ''' <summary>製作台数の列数</summary>
        Private Const SPD_DAISU_COLUMN_COUNT As Integer = 11
        ''' <summary>工事指令№列開始の列番号</summary>
        Private Const SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN As Integer = 3
        ''' <summary>spread固定列のカウント</summary>
        Private Const SPD_DAISU_KOTEI_COLUMN_COUNT As Integer = 3
        ''' <summary>工事指令№列のカウント</summary>
        Private Const SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT As Integer = 4
        ''' <summary>台数合計列の列番号</summary>
        Private Const SPD_DAISU_DAISU_COUNT_COLUMN_INDEX As Integer = 1

#End Region


#End Region

#Region "金材"
        ''' <summary>半期の月数</summary>
        Public Const HALF_MONTH_DEFAULT_COUNT As Integer = 6

#End Region

#Region " 金材Spreadの定義 "

#Region "列"

        ''' <summary>spread固定列のカウント</summary>
        Public Const SPD_KANAZAI_KOTEI_COLUMN_COUNT As Integer = 5
        ''' <summary>spread列のカウント</summary>
        Public Const SPD_KANAZAI_DEFAULT_COLUMN_COUNT As Integer = 7

        ''' <summary>月分布列開始の列番号</summary>
        Public Const SPD_KANAZAI_MONTH_COLUMNS_START_INDEX As Integer = 6
        ''' <summary>月挿入ボタンの列番号</summary>
        Public Const SPD_KANAZAI_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX As Integer = 5
        ''' <summary>金材追加ボタンの列番号</summary>
        Public Const SPD_KANAZAI_ADD_BUTTON_CELL_COLUMN_INDEX As Integer = 2
        ''' <summary>金材項目名の列番号</summary>
        Public Const SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX As Integer = 1
        ''' <summary>台数の列番号</summary>
        Public Const SPD_KANAZAI_DAISU_COLUMN_INDEX As Integer = 3
        ''' <summary>単価の列番号</summary>
        Public Const SPD_KANAZAI_TANKA_COLUMN_INDEX As Integer = 4
        ''' <summary>計の列番号</summary>
        Public Const SPD_KANAZAI_KEI_COLUMN_INDEX As Integer = 5

#End Region

#Region "行"
        ''' <summary>spread固定行のカウント</summary>
        Public Const SPD_KANAZAI_KOTEI_ROW_COUNT As Integer = 3
        ''' <summary>spread行のカウント</summary>
        Public Const SPD_KANAZAI_DEFAULT_ROW_COUNT As Integer = 4

#End Region

#End Region

#Region " 造り方Spreadの定義"

#Region "列"

        ''' <summary>spread固定列のカウント</summary>
        Private Const SPD_TUKURIKATA_KOTEI_COLUMN_COUNT As Integer = 8
        ''' <summary>spread列のカウント</summary>
        Private Const SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT As Integer = 10
        ''' <summary>月分布列開始の列番号</summary>
        Private Const SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX As Integer = 7
        ''' <summary>月挿入ボタンの列番号</summary>
        Private Const SPD_TUKURIKATA_ADDMONTH_BUTTON_LEFT_COLUMN_INDEX As Integer = 6
        ''' <summary>造り方追加ボタンの列番号</summary>
        Private Const SPD_TUKURIKATA_ADD_BUTTON_CELL_COLUMN_INDEX As Integer = 5
        ''' <summary>ユニット区分の列番号</summary>
        Private Const SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX As Integer = 1
        ''' <summary>部品表名の列番号</summary>
        Private Const SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX As Integer = 2
        ''' <summary>パターン名の列番号</summary>
        Private Const SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX As Integer = 3
        ''' <summary>部品費（円）列番号</summary>
        Private Const SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX As Integer = 4
        ''' <summary>型費（円）列番号</summary>
        Private Const SPD_TUKURIKATA_KATAHI_COLUMN_INDEX As Integer = 5
        ''' <summary>台数の列番号</summary>
        Private Const SPD_TUKURIKATA_DAISU_COLUMN_INDEX As Integer = 6

#End Region

#Region "行"

        ''' <summary>spread固定行のカウント</summary>
        Public Const SPD_TUKURIKATA_KOTEI_ROW_COUNT As Integer = 3
        ''' <summary>spread行のカウント</summary>
        Public Const SPD_TUKURIKATA_DEFAULT_ROW_COUNT As Integer = 4

        ''' <summary>パターン名開始の行番号</summary>
        Public Const SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX As Integer = 40
        ''' <summary>造り方追加ボタンの行番号</summary>
        Public Const SPD_TUKURIKATA_ADD_BUTTON_CELL_ROW_INDEX As Integer = 0
        ''' <summary>月分布ヘッダーの行番号</summary>
        Public Const SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX As Integer = 37
        ''' <summary>年の行番号</summary>
        Public Const SPD_TUKURIKATA_YEAR_ROW_INDEX As Integer = 38



#End Region


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

#End Region

#Region " 集計値Spreadの定義"

#Region "列"
        ''' <summary>spread固定列のカウント</summary>
        Public Const SPD_ZAIMUJISEKI_KOTEI_COLUMN_COUNT As Integer = 3
        ''' <summary>spread列のカウント</summary>
        Public Const SPD_ZAIMUJISEKI_DEFAULT_COLUMN_COUNT As Integer = 4

        ''' <summary>月分布列開始の列番号</summary>
        Public Const SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX As Integer = 5

        ''' <summary>再計算ボタンの列番号</summary>
        Public Const SPD_ZAIMUJISEKI_CALC_BUTTON_CELL_COLUMN_INDEX As Integer = 3


#End Region

#Region "行"
        ''' <summary>spread行のカウント</summary>
        Public Const SPD_ZAIMUJISEKI_DEFAULT_ROW_COUNT As Integer = 27
        ''' <summary>比例費開始の行番号</summary>
        Public Const SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX As Integer = 4
        Public Const SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX As Integer = 5
        Public Const SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX As Integer = 6
        Public Const SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX As Integer = 7
        Public Const SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX As Integer = 8
        Public Const SPD_ZAIMUJISEKI_HIREI_MITOSHI_KEI_ROWS_INDEX As Integer = 9
        Public Const SPD_ZAIMUJISEKI_HIREI_HATCHU_METAL_ROWS_INDEX As Integer = 10
        Public Const SPD_ZAIMUJISEKI_HIREI_HATCHU_TRIM_ROWS_INDEX As Integer = 11
        Public Const SPD_ZAIMUJISEKI_HIREI_HATCHU_KEI_ROWS_INDEX As Integer = 12
        Public Const SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX As Integer = 13
        Public Const SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX As Integer = 14
        Public Const SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX As Integer = 15
        Public Const SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX As Integer = 16
        Public Const SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX As Integer = 17
        Public Const SPD_ZAIMUJISEKI_HIREI_ZAIMU_KEI_ROWS_INDEX As Integer = 18
        ''' <summary>固定費開始の行番号</summary>
        Public Const SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX As Integer = 19
        Public Const SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX As Integer = 20
        Public Const SPD_ZAIMUJISEKI_KOTEI_MITOSHI_KEI_ROWS_INDEX As Integer = 21
        Public Const SPD_ZAIMUJISEKI_KOTEI_HATCHU_METAL_ROWS_INDEX As Integer = 22
        Public Const SPD_ZAIMUJISEKI_KOTEI_HATCHU_TRIM_ROWS_INDEX As Integer = 23
        Public Const SPD_ZAIMUJISEKI_KOTEI_HATCHU_KEI_ROWS_INDEX As Integer = 24
        Public Const SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX As Integer = 25
        Public Const SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX As Integer = 26
        Public Const SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX As Integer = 27
        ''' <summary>再計算ボタンの行番号</summary>
        Public Const SPD_ZAIMUJISEKI_CALC_BUTTON_CELL_ROW_INDEX As Integer = 1
        ''' <summary>月分布ヘッダーの行番号</summary>
        Public Const SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX As Integer = 1
        ''' <summary>年の行番号</summary>
        Public Const SPD_ZAIMUJISEKI_YEAR_ROW_INDEX As Integer = 2
        ''' <summary>月の行番号</summary>
        Public Const SPD_ZAIMUJISEKI_MONTH_ROW_INDEX As Integer = 3

#End Region


#End Region


#End Region

#Region "メンバ変数"

#Region ""
        Private spread As FpSpread
        Private sheet As FarPoint.Win.Spread.SheetView
        Private editDao As YosanshoEditDao
        Private yosanEventCode As String
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


        Private ReadOnly aSystemDate As ShisakuDate

        Private activeSheetIndex As Integer

        Private _YosanEventVo As TYosanEventVo
        Private EventDao As TYosanEventDao
        Private _MitoshiMetalList As List(Of YosanInsuBuhinHiVo)

        ''' <summary>
        ''' シート名毎に行の開始位置を格納するDictionary
        ''' </summary>
        ''' <remarks></remarks>
        Private startRowDictinary As Dictionary(Of String, Dictionary(Of Integer, Integer))

        ''' <summary>造り方(完成車)のデータ列数</summary>
        Private tukurikataDataColumnCount As Integer
        ''' <summary>造り方(ホワイトボディ)のデータ列数</summary>
        Private tukurikataWBDataColumnCount As Integer
        ''' <summary>金材のデータ列数</summary>
        Private kanazaiDataColumnCount As Integer

        Private minYearKs As String
        Private maxYearKs As String
        Private exportDao As YosanshoExportDao
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

        Private yearMonths As List(Of String)

        ''' <summary>key(シート名),value(key(行位置,value(key(yyyyMM),value(関数のリスト)))</summary>
        Private yearMonthFormulaSumEvent As Dictionary(Of String, Dictionary(Of Integer, Dictionary(Of String, List(Of String))))

#End Region

#Region "造り方Spreadの行"

        '造り方Spreadの開始位置' 

        ''' <summary>月の行番号(完成車)</summary>
        Private SPD_TUKURIKATA_MONTH_ROW_INDEX As Integer
        ''' <summary>データ入力最後の行番号(完成車)</summary>
        Private SPD_TUKURIKATA_DATA_END_ROW_INDEX As Integer

        ''' <summary>月の行番号(ホワイトボディ)</summary>
        Private SPD_TUKURIKATA_WB_MONTH_ROW_INDEX As Integer
#End Region

#Region "金材Spreadの開始行"
        ''' <summary>金材項目名開始の行番号</summary>
        Private SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX As Integer

        ''' <summary>金材追加ボタンの行番号</summary>
        Private SPD_KANAZAI_ADD_BUTTON_CELL_ROW_INDEX As Integer = 0
        ''' <summary>月分布ヘッダーの行番号</summary>
        Private SPD_KANAZAI_MONTH_TITLE_ROW_INDEX As Integer = 0
        ''' <summary>年の行番号</summary>
        Private SPD_KANAZAI_YEAR_ROW_INDEX As Integer = 1
        ''' <summary>月の行番号</summary>
        Private SPD_KANAZAI_MONTH_ROW_INDEX As Integer = 2

        ''' <summary>金材データ開始の行番号</summary>
        Private SPD_KANAZAI_KANAZAINAME_ROWS_DATA_START_INDEX As Integer

#End Region

#Region "全体集計用"

        Private minYearKsSum As String
        Private maxYearKsSum As String


        Private aTukurikataListKanseiSum As List(Of TYosanTukurikataVo)
        Private aTukurikataListWbSum As List(Of TYosanTukurikataVo)

        Private yosanSeisakuDaisuSumList As List(Of TYosanSeisakuDaisuVo)

        Private daisuDic As Dictionary(Of String, String)

        Private patternDic As Dictionary(Of String, String)

        Private kanazaiListSum As List(Of TYosanKanazaiVo)

        Private kanazaiDaisuDic As Dictionary(Of String, String)

        ''' <summary>
        ''' キー(行index)キー(yyyyMM)、参照位置
        ''' </summary>
        ''' <remarks></remarks>
        Private zaimuDataDic As Dictionary(Of Integer, Dictionary(Of String, List(Of String)))

#End Region

#End Region

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            spread = New FpSpread
            aSystemDate = New ShisakuDate
            editDao = New YosanshoEditDaoImpl
            EventDao = New TYosanEventDaoImpl
            exportDao = New YosanshoExportDaoImpl
            activeSheetIndex = -1
            yearMonthFormulaSumEvent = New Dictionary(Of String, Dictionary(Of Integer, Dictionary(Of String, List(Of String))))

            minYearKsSum = "9999年下期"
            maxYearKsSum = "0000年上期"
            aTukurikataListKanseiSum = New List(Of TYosanTukurikataVo)
            aTukurikataListWbSum = New List(Of TYosanTukurikataVo)
            yosanSeisakuDaisuSumList = New List(Of TYosanSeisakuDaisuVo)
            daisuDic = New Dictionary(Of String, String)
            patternDic = New Dictionary(Of String, String)
            kanazaiListSum = New List(Of TYosanKanazaiVo)
            kanazaiDaisuDic = New Dictionary(Of String, String)
            zaimuDataDic = New Dictionary(Of Integer, Dictionary(Of String, List(Of String)))

            For index As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX
                zaimuDataDic.Add(index, New Dictionary(Of String, List(Of String)))
            Next


        End Sub

        ''' <summary>
        ''' シートに作成(個別イベント)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub CreateSheet(ByVal yosanEventCode As String)
            Me.yosanEventCode = yosanEventCode

            '新しくシート作成する。'
            spread.Sheets.Add(New FarPoint.Win.Spread.SheetView)
            activeSheetIndex = activeSheetIndex + 1

            spread.ActiveSheetIndex = activeSheetIndex
            spread.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)  '太文字に
            sheet = spread.ActiveSheet
            sheet.SheetName() = "集計表" & yosanEventCode
            sheet.ReferenceStyle = Model.ReferenceStyle.R1C1
            sheet.ColumnCount = 20000 '16384列がexcel2010の最大らしい・・・

            If Not yearMonthFormulaSumEvent.ContainsKey(sheet.SheetName) Then
                yearMonthFormulaSumEvent.Add(sheet.SheetName, New Dictionary(Of Integer, Dictionary(Of String, List(Of String))))

                For index As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX
                    yearMonthFormulaSumEvent(sheet.SheetName).Add(index, New Dictionary(Of String, List(Of String)))
                Next

            End If

            minYearKs = "9999年下期"
            maxYearKs = "0000年上期"

            '初期データ表示スプレッド(製作台数)
            SeisakuDaisu()
            '初期データ表示スプレッド(造り方)
            Tukurikata()
            ''初期データ表示スプレッド(金材)
            Kanazai()
            ''初期データ表示スプレッド(集計値)
            ZaimuJiseki()


            minYearKsSum = IIf(CompareYearKs(minYearKsSum, minYearKs) = True, minYearKsSum, minYearKs)
            maxYearKsSum = IIf(CompareYearKs(maxYearKsSum, maxYearKs) = False, maxYearKsSum, maxYearKs)

            '部品表シートを作成'
            CreateSheetBuhinEdit()
        End Sub

        ''' <summary>
        ''' 複数イベント指定されているなら作成する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub CreateSumSheet(ByVal eventVos As List(Of TYosanEventVo))
            '新しくシート作成する。'
            spread.Sheets.Add(New FarPoint.Win.Spread.SheetView)
            activeSheetIndex = activeSheetIndex + 1

            spread.ActiveSheetIndex = activeSheetIndex
            sheet = spread.ActiveSheet
            sheet.SheetName() = "Excel出力イベント"
            sheet.ReferenceStyle = Model.ReferenceStyle.R1C1
            sheet.ColumnCount = 20000 '16384列がexcel2010の最大らしい・・・

            'イベント情報を出力'
            CreateEventSheet(eventVos)


            'こっちは作るだけ'
            spread.Sheets.Add(New FarPoint.Win.Spread.SheetView)
            activeSheetIndex = activeSheetIndex + 1

            spread.ActiveSheetIndex = activeSheetIndex
            sheet = spread.ActiveSheet
            sheet.SheetName() = "集計表結果"
            sheet.ReferenceStyle = Model.ReferenceStyle.R1C1

        End Sub

#Region "EXCEL出力"

        ''' <summary>
        ''' Excel出力機能(Spreadの機能によりExcelファイルを出力)
        ''' </summary>
        ''' <param name="filename"></param>
        ''' <remarks></remarks>
        Public Sub SaveExcelFile(ByVal filename As String)

            Dim fIO As New System.IO.FileInfo(filename)

            '号車スプレッドを基本情報スプレッドに一旦統合
            'goushaIndex = spreadBase.Sheets.Add(CopySheet(_frmDispTehaiEdit.spdGouSya_Sheet1))
            'spreadBase.Sheets(goushaIndex).SheetName = _TITLE_GOUSYA
            '_frmDispTehaiEdit.Refresh()

            Try
                '基本スプレッド出力
                If spread.SaveExcel(filename) = False Then
                    Throw New Exception("基本情報Excel出力でエラーが発生しました")
                End If

                If Not ShisakuComFunc.IsFileOpen(filename) Then
                    Using xls As New ShisakuExcel(filename)

                        '号車引数列を基本情報シートの末列にコピー
                        'InsertGousyaCol(xls)
                        'xls.SetActiveSheet(1)
                        'xls.SetSheetName(_TITLE_BASE)
                        'Me.SetTitleLineExcel(xls, _TITLE_BASE)

                        ''出図履歴含む処理を実行
                        ''   sExcel:False＝出図履歴含む
                        'If StringUtil.Equals(sExcel, False) Then
                        '    setShutuzuRireki(xls)
                        'End If

                        '2012/02/02'
                        'A4横で印刷できるように変更'
                        xls.PrintPaper(filename, 1, "A4")
                        xls.PrintOrientation(filename, 1, 1, False)
                        '保存
                        xls.Save()
                    End Using

                End If
                Process.Start(filename)

            Finally

                ''一時的に統合した号車情報スプレッドを削除
                'spreadBase.Sheets.Remove(spreadBase.Sheets(goushaIndex))

            End Try



        End Sub


#End Region

#Region "個別シートの作成"

#Region "製作台数"

        ''' <summary>
        ''' 製作台数
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SeisakuDaisu()
            '製作台数'
            Dim yosanSeisakuDaisuList As List(Of TYosanSeisakuDaisuVo)
            yosanSeisakuDaisuList = editDao.FindYosanSeisakuDaisuBy(yosanEventCode)

            'タイトル部分の作成'
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = "≪製作台数≫"
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).RowSpan = 2
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).ColumnSpan = 2
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX + 2, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).Value = "台数"
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX + 2, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).BackColor = Color.FromArgb(204, 255, 255) ' RGB(204, 255, 255)

            sheet.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX + 1, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = "完成車"
            sheet.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX + 2, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = "ﾎﾜｲﾄﾎﾞﾃﾞｨ"

            '工事指令№を設定
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN).Value = "工事指令№"
            '工事指令№背景色
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN).BackColor = Color.FromArgb(255, 255, 153) 'RGB(255, 255, 153)


            _KanseiDaisuList = New List(Of TYosanSeisakuDaisuVo)
            _WbDaisuList = New List(Of TYosanSeisakuDaisuVo)

            Dim dataColumnCount As Integer = 0

            For Each daisuVo As TYosanSeisakuDaisuVo In yosanSeisakuDaisuList
                'Dim colIndex As Integer = daisuVo.KoujiShireiNoHyojijunNo * 2 + SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN
                Dim colIndex As Integer = daisuVo.KoujiShireiNoHyojijunNo + SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN
                If colIndex >= sheet.ColumnCount - 1 Then
                    sheet.AddColumns(colIndex, 2)
                    SetKojishireiColumnPro(colIndex)
                End If
                SetKojishireiColumnPro(colIndex)
                'ユニット区分
                sheet.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, colIndex).Value = UnitKbnName(daisuVo.UnitKbn)

                '工事指令No
                sheet.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, colIndex).Value = daisuVo.KoujiShireiNo

                '台数
                If IsKanseisha(daisuVo.ShisakuSyubetu) Then
                    '完成車の場合
                    sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, colIndex).Value = daisuVo.DaisuSuryo
                    Dim newVo As New TYosanSeisakuDaisuVo
                    VoUtil.CopyProperties(daisuVo, newVo)
                    _KanseiDaisuList.Add(newVo)
                End If
                If IsWb(daisuVo.ShisakuSyubetu) Then
                    'WB車の場合
                    sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, colIndex).Value = daisuVo.DaisuSuryo
                    Dim newVo As New TYosanSeisakuDaisuVo
                    VoUtil.CopyProperties(daisuVo, newVo)
                    _WbDaisuList.Add(newVo)
                End If

                If dataColumnCount < colIndex Then
                    dataColumnCount = colIndex
                End If

            Next
            '４列未満の場合
            If _KanseiDaisuList.Count < SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT Then
                SetDefaultSeisakuDaisuList(yosanEventCode, SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT - _KanseiDaisuList.Count, _KanseiDaisuList, SHISAKU_SYUBETU_KANSEI)
            End If
            If _WbDaisuList.Count < SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT Then
                SetDefaultSeisakuDaisuList(yosanEventCode, SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT - _WbDaisuList.Count, _WbDaisuList, SHISAKU_SYUBETU_WB)
            End If

            '台数合計
            SetSpreadSeisakuDaisuPro()
            CalcTaisu(dataColumnCount)
            '工事指令Noヘッダー
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_KOTEI_COLUMN_COUNT).ColumnSpan = dataColumnCount - SPD_DAISU_KOTEI_COLUMN_COUNT + 1

            Dim cbs As New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine, Color.Black)
            Dim cBorder As New FarPoint.Win.ComplexBorder(cbs, cbs, cbs, cbs)

            '表に加線
            If dataColumnCount = 0 Then
                Dim cr As New FarPoint.Win.Spread.Model.CellRange(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX, 5, SPD_DAISU_KOTEI_COLUMN_COUNT)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            Else
                Dim cr As New FarPoint.Win.Spread.Model.CellRange(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX, 5, dataColumnCount)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            End If




            For Each daisuVo As TYosanSeisakuDaisuVo In yosanSeisakuDaisuList
                Dim colIndex As Integer = daisuVo.KoujiShireiNoHyojijunNo + SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN
                'ユニット区分
                ''集計表10B-N-01'!R[-51]C[3]
                daisuVo.UnitKbn = "'" & sheet.SheetName & "'!R" & SPD_DAISU_UNITKBN_ROW_INDEX + 1 & "C" & colIndex + 1

                '工事指令No
                ''集計表10B-N-01'!R[-51]C[3]
                daisuVo.KoujiShireiNo = "'" & sheet.SheetName & "'!R" & SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX + 1 & "C" & colIndex + 1

                '台数
                If IsKanseisha(daisuVo.ShisakuSyubetu) Then
                    '完成車の場合
                    sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, colIndex).Value = daisuVo.DaisuSuryo
                    daisuVo.CreatedUserId = "'" & sheet.SheetName & "'!R" & SPD_DAISU_KANSEISHA_ROW_INDEX + 1 & "C" & colIndex + 1
                End If
                If IsWb(daisuVo.ShisakuSyubetu) Then
                    'WB車の場合
                    sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, colIndex).Value = daisuVo.DaisuSuryo
                    daisuVo.CreatedUserId = "'" & sheet.SheetName & "'!R" & SPD_DAISU_WBSHA_ROW_INDEX + 1 & "C" & colIndex + 1
                End If
            Next

            yosanSeisakuDaisuSumList.AddRange(yosanSeisakuDaisuList)

        End Sub

        ''' <summary>
        ''' SPREADで工事指令№セルの配置を設定する。
        ''' </summary>
        ''' <param name="columnIndex"></param>
        ''' <remarks></remarks>
        Private Sub SetKojishireiColumnPro(ByVal columnIndex As Integer)
            sheet.Columns(columnIndex).Width = sheet.Columns(SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN).Width
            sheet.Columns(columnIndex + 1).Width = sheet.Columns(SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN + 1).Width
            'ユニット区分セル
            'sheet.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).Locked = True
            sheet.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            sheet.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).VerticalAlignment = CellVerticalAlignment.Center

            'sheet.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).ColumnSpan = 2
            sheet.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex).BackColor = Color.Khaki
            '工事指令№セル
            'sheet.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).Locked = False
            sheet.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).CellType = GetKojishireiCellType()
            sheet.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            sheet.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            'sheet.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).ColumnSpan = 2
            sheet.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex).BackColor = Color.White
            '台数セル
            'sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).Locked = False
            'sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).CellType = GetDaisuCellType()
            sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Right
            sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex).BackColor = Color.White
            'sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).Locked = True
            'sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            'sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).VerticalAlignment = CellVerticalAlignment.Center
            'sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).Value = "台"
            'sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex + 1).BackColor = Color.White
            'sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).Locked = False
            'sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).CellType = GetDaisuCellType()
            sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Right
            sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex).BackColor = Color.White
            'sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).Locked = True
            'sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            'sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).VerticalAlignment = CellVerticalAlignment.Center
            'sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).Value = "台"
            'sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex + 1).BackColor = Color.White
        End Sub


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
            'sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Locked = True
            sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).Locked = True
            sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            'sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Locked = True
            sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            'sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).Locked = True
            sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).HorizontalAlignment = CellHorizontalAlignment.Left

        End Sub

        Private DaisuCellType As NumberCellType
        ''' <summary>
        ''' 台数セルを返す
        ''' </summary>
        ''' <returns>台数セル</returns>
        ''' <remarks></remarks>
        Public Function GetDaisuCellType() As NumberCellType
            DaisuCellType = ShisakuSpreadUtil.NewGeneralNumberCellType
            DaisuCellType.MinimumValue = 0
            DaisuCellType.MaximumValue = 9999
            DaisuCellType.DecimalPlaces = 0
            Return DaisuCellType
        End Function

        ''' <summary>
        ''' 台数合計
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CalcTaisu(ByVal daisuCount As Integer)

            Dim taisuKanseiShaCount As Integer = 0
            Dim taisuWbShaCount As Integer = 0

            If daisuCount = 0 Then
                Exit Sub
            End If

            sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).Formula = "SUM(R" & SPD_DAISU_KANSEISHA_ROW_INDEX + 1 & "C" & SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT & ":R" & SPD_DAISU_KANSEISHA_ROW_INDEX + 1 & "C" & daisuCount + 1 & ")&" & """台"""
            sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).Formula = "SUM(R" & SPD_DAISU_WBSHA_ROW_INDEX + 1 & "C" & SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT & ":R" & SPD_DAISU_WBSHA_ROW_INDEX + 1 & "C" & daisuCount + 1 & ")&" & """台"""

        End Sub

#End Region

#Region "金材"

        ''' <summary>
        ''' 金材
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Kanazai()

            _KanazaiList = editDao.FindYosanKanazaiBy(yosanEventCode)

            'タイトル部作成'
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).Value = "≪金材≫"
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).ColumnSpan = 5
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).RowSpan = 2

            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).ColumnSpan = 2

            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).Value = "月分布"
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).BackColor = Color.FromArgb(255, 255, 153)
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 1, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).BackColor = Color.FromArgb(255, 255, 153)
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).BackColor = Color.FromArgb(255, 255, 153)



            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_DAISU_COLUMN_INDEX).Value = "台数"
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_DAISU_COLUMN_INDEX).BackColor = Color.FromArgb(204, 255, 255)
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_TANKA_COLUMN_INDEX).Value = "単価"
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_TANKA_COLUMN_INDEX).BackColor = Color.FromArgb(255, 255, 153)
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_KEI_COLUMN_INDEX).Value = "計"
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_KEI_COLUMN_INDEX).BackColor = Color.FromArgb(204, 255, 255)


            Dim vosByHyojijun As Dictionary(Of String, List(Of TYosanKanazaiVo)) = MakeKanazaiVosByHyojijun(_KanazaiList)
            Dim borderStartRow As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX

            Dim dataEndRow As Integer = 0
            Dim monthCount As Integer = 0
            If vosByHyojijun.Count > 0 Then
                'sheet.RemoveColumns(SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, 1)
                'sheet.AddRows(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, vosByHyojijun.Count)

                Dim indexX As Integer = 0
                Dim ks As String = String.Empty
                Dim columnIndex As Integer = 0

                For Each key As String In vosByHyojijun.Keys
                    Dim rowIndex As Integer = indexX + SPD_KANAZAI_KANAZAINAME_ROWS_DATA_START_INDEX
                    columnIndex = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX

                    SetKanazaiRowPro(rowIndex)

                    '金材項目名
                    sheet.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).Value = key
                    '単価
                    sheet.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).Value = vosByHyojijun(key)(0).KanazaiUnitPrice

                    For Each kanazaiVo As TYosanKanazaiVo In vosByHyojijun(key)
                        If indexX = 0 Then
                            '年期タイトル
                            If (columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then



                                '合計列追加
                                'AddSpdMonthCountColumn(sheet, columnIndex, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                                '                       SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                                'columnIndex = columnIndex + 1
                            End If


                            If columnIndex >= sheet.ColumnCount Then
                                sheet.Columns.Add(columnIndex - 1, 1)
                            End If
                            'sheet.AddColumns(columnIndex, 1)
                            '年
                            Dim yearKs As String = ChangeDBYearToSpreadYear(kanazaiVo.YosanTukurikataYyyyMm, kanazaiVo.YosanTukurikataKs)
                            sheet.Cells(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).Value = yearKs
                            '月
                            sheet.Cells(SPD_KANAZAI_MONTH_ROW_INDEX, columnIndex).Value = CInt(kanazaiVo.YosanTukurikataYyyyMm.Substring(4, 2)).ToString & "月"
                            monthCount = monthCount + 1
                            kanazaiDataColumnCount = kanazaiDataColumnCount + 1

                            minYearKs = IIf(CompareYearKs(minYearKs, yearKs) = True, minYearKs, yearKs)
                            maxYearKs = IIf(CompareYearKs(maxYearKs, yearKs) = False, maxYearKs, yearKs)
                        End If
                        '月分布タイトル
                        SetMonthTitlePro(sheet, columnIndex, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                                         SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                        '合計列の場合
                        'If (columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                        '    SetMonthCellPro(sheet, rowIndex, columnIndex, True)
                        '    columnIndex = columnIndex + 1
                        'End If
                        '台数セル
                        sheet.Cells(rowIndex, columnIndex).Value = kanazaiVo.DaisuSuryo

                        Dim daisuKey As String = EzUtil.MakeKey(kanazaiVo.YosanEventCode, key, kanazaiVo.YosanTukurikataYyyyMm)

                        If Not kanazaiDaisuDic.ContainsKey(daisuKey) Then
                            kanazaiDaisuDic.Add(daisuKey, "'" & sheet.SheetName & "'!R" & rowIndex + 1 & "C" & columnIndex + 1)
                        End If



                        SetMonthCellPro(sheet, rowIndex, columnIndex, False)
                        columnIndex = columnIndex + 1
                        dataEndRow = rowIndex
                    Next

                    indexX = indexX + 1
                Next
                '合計列追加
                'AddSpdMonthCountColumn(sheet, columnIndex, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                '                       SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                '月分布フッター
                'sheet.Cells(sheet.RowCount - 1, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).ColumnSpan = sheet.ColumnCount - SPD_KANAZAI_KOTEI_COLUMN_COUNT - 1

                '台数合計
                CalcKanazai(dataEndRow, monthCount)
            End If

            Dim cbs As New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine, Color.Black)
            Dim cBorder As New FarPoint.Win.ComplexBorder(cbs, cbs, cbs, cbs)

            '表に加線
            monthCount = monthCount + SPD_KANAZAI_MONTH_COLUMNS_START_INDEX
            If monthCount > SPD_KANAZAI_MONTH_COLUMNS_START_INDEX Then

                '年期セルをマージする'
                For columnIndex As Integer = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX To monthCount - 1 Step 6
                    sheet.Cells(borderStartRow + 1, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
                    sheet.Cells(borderStartRow + 1, columnIndex).ColumnSpan = 6
                Next
                sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).ColumnSpan = monthCount - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX

                Dim cr As New FarPoint.Win.Spread.Model.CellRange(borderStartRow, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX, dataEndRow - borderStartRow + 1, monthCount - 1)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            Else
                Dim cr As New FarPoint.Win.Spread.Model.CellRange(borderStartRow, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX, 3, monthCount)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            End If

            kanazaiListSum.AddRange(_KanazaiList)

            ''固定列を設定
            'sheet.FrozenColumnCount = SPD_KANAZAI_KOTEI_COLUMN_COUNT
            ''固定行を設定
            'sheet.FrozenRowCount = SPD_KANAZAI_KOTEI_ROW_COUNT
        End Sub

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
        ''' SPREADで行の配置を設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetKanazaiRowPro(ByVal rowIndex As Integer)
            '金材項目名セル
            'sheet.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).Locked = True
            sheet.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Left
            sheet.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            sheet.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).ColumnSpan = 2
            '台数セル
            'sheet.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).Locked = True
            'sheet.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).CellType = GetDaisuCellType()
            sheet.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            sheet.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            'sheet.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX + 1).Locked = True
            'sheet.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            'sheet.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX + 1).VerticalAlignment = CellVerticalAlignment.Center
            'sheet.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX + 1).Value = "台"
            '単価セル
            'sheet.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).Locked = False
            sheet.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).CellType = GetTankaCellType()
            sheet.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            sheet.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '計セル
            'sheet.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).Locked = True
            'sheet.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).CellType = GetKingakuCellType()
            sheet.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            sheet.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center

            sheet.Rows(rowIndex).BackColor = Color.White
            sheet.Cells(SPD_KANAZAI_MONTH_TITLE_ROW_INDEX, sheet.ColumnCount - 1).RowSpan = sheet.RowCount - 1
        End Sub

        ''' <summary>
        ''' 台数合計
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CalcKanazai(ByVal dataEndRow As Integer, ByVal monthCount As Integer)

            For rowIndex As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_DATA_START_INDEX To dataEndRow
                Dim daisuCount As Integer = 0
                'For columnIndex As Integer = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX To sheet.ColumnCount - 2
                '    '合計列以外
                '    If Not ((columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                '        If StringUtil.IsNotEmpty(sheet.GetValue(rowIndex, columnIndex)) Then
                '            monthCount = monthCount + sheet.GetValue(rowIndex, columnIndex)
                '        End If
                '    Else
                '        If monthCount = 0 Then
                '            sheet.Cells(rowIndex, columnIndex).Value = Nothing
                '        Else
                '            sheet.Cells(rowIndex, columnIndex).Value = monthCount
                '        End If
                '        daisuCount = daisuCount + monthCount

                '        monthCount = 0
                '    End If
                'Next
                ''台数合計
                'If daisuCount = 0 Then
                '    sheet.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).Value = Nothing
                'Else
                '    sheet.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).Value = daisuCount
                'End If
                sheet.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).Formula = "SUM(R" & rowIndex + 1 & "C" & SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1 & ":" & "R" & rowIndex + 1 & "C" & SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + monthCount & ")&""台"""

                '計
                Dim tanka As Decimal? = CDec(IIf(StringUtil.IsNotEmpty(sheet.GetValue(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX)), sheet.GetValue(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX), Nothing))

                If tanka IsNot Nothing Then
                    'sheet.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).Value = tanka * daisuCount
                    sheet.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).Formula = "RC[-1]*LEFT(RC[-2],FIND(""台"",RC[-2])-1)"
                Else
                    sheet.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).Value = Nothing
                End If
            Next

        End Sub



#End Region

#Region "造り方"

        ''' <summary>
        ''' 造り方
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Tukurikata()

            Dim dataStartRowIndex As Integer = SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX
            tukurikataDataColumnCount = 0
            tukurikataWBDataColumnCount = 0

            '完成車
            SPD_TUKURIKATA_MONTH_ROW_INDEX = dataStartRowIndex + 2
            _KanseiTukurikataList = DispSpreadTukurikata(SHISAKU_SYUBETU_KANSEI, dataStartRowIndex, tukurikataDataColumnCount)
            If dataStartRowIndex = SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX Then
                dataStartRowIndex = dataStartRowIndex + 5
            Else
                dataStartRowIndex = dataStartRowIndex + 2
            End If

            '２行あける'
            'ホワイトボディ
            SPD_TUKURIKATA_WB_MONTH_ROW_INDEX = dataStartRowIndex
            _WbTukurikataList = DispSpreadTukurikata(SHISAKU_SYUBETU_WB, dataStartRowIndex, tukurikataWBDataColumnCount)

            '金材の開始位置を設定'
            If dataStartRowIndex = SPD_TUKURIKATA_WB_MONTH_ROW_INDEX Then
                SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX = dataStartRowIndex + 5
            Else
                SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX = dataStartRowIndex + 2
            End If


            SPD_KANAZAI_ADD_BUTTON_CELL_ROW_INDEX = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX
            SPD_KANAZAI_MONTH_TITLE_ROW_INDEX = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX
            SPD_KANAZAI_YEAR_ROW_INDEX = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 1
            SPD_KANAZAI_MONTH_ROW_INDEX = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2
            SPD_KANAZAI_KANAZAINAME_ROWS_DATA_START_INDEX = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 3
        End Sub

        ''' <summary>
        ''' 完成車・ホワイトボディ
        ''' </summary>
        ''' <param name="shisakuSyubetu"></param>
        ''' <remarks></remarks>
        Private Function DispSpreadTukurikata(ByVal shisakuSyubetu As String, ByRef dataStartRowIndex As Integer, ByRef dataCount As Integer) As List(Of TYosanTukurikataVo)

            Dim bodyTitle As String = ""
            Dim monthColumnCount As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX
            Dim borderStartRow As Integer = dataStartRowIndex
            Dim dataRowCount As Integer = 3

            Dim aTukurikataList As List(Of TYosanTukurikataVo) = editDao.FindYosanTukurikataBy(yosanEventCode, shisakuSyubetu)
            'ユニット区分、部品表名、パターン名'
            Dim vosByHyojijun As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo)))) = MakeTukurikataVosByHyojijun(aTukurikataList, shisakuSyubetu)

            If StringUtil.Equals(SHISAKU_SYUBETU_WB, shisakuSyubetu) Then
                bodyTitle = "ホワイトボディ"
            Else
                bodyTitle = "完成車"
            End If

            'タイトル部作成'
            sheet.Cells(dataStartRowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).Value = "月分布"

            sheet.Cells(dataStartRowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).Value = "≪造り方≫"
            sheet.Cells(dataStartRowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).RowSpan = 2
            sheet.Cells(dataStartRowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).ColumnSpan = 6

            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).Value = bodyTitle
            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).ColumnSpan = 3
            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).BackColor = Color.FromArgb(255, 255, 153)

            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = "部品費（円）"
            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = "型費（千円）"
            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).Value = "台数"

            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).BackColor = Color.FromArgb(204, 255, 255)
            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).BackColor = Color.FromArgb(204, 255, 255)
            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).BackColor = Color.FromArgb(204, 255, 255)

            If vosByHyojijun.Count > 0 Then

                Dim indexX As Integer = 0
                Dim ks As String = String.Empty
                Dim columnIndex As Integer = 0
                Dim rowIndex As Integer = dataStartRowIndex + 3
                Dim startRow As Integer
                For Each unitKbn As String In vosByHyojijun.Keys
                    'ユニット開始の行番号
                    startRow = rowIndex

                    For Each buhinhyoName As String In vosByHyojijun(unitKbn).Keys
                        For Each patternName As String In vosByHyojijun(unitKbn)(buhinhyoName).Keys
                            SetTukurikataRowPro(sheet, rowIndex)
                            columnIndex = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX


                            Dim vosCount As Integer = 0
                            For Each tukurikataVo As TYosanTukurikataVo In vosByHyojijun(unitKbn)(buhinhyoName)(patternName)
                                If indexX = 0 Then

                                    If sheet.ColumnCount <= columnIndex Then
                                        sheet.AddColumns(columnIndex - 1, 1)
                                    End If

                                    '年
                                    Dim yearKs As String = ChangeDBYearToSpreadYear(tukurikataVo.YosanTukurikataYyyyMm, tukurikataVo.YosanTukurikataKs)

                                    sheet.Cells(dataStartRowIndex + 1, columnIndex).Value = yearKs
                                    '月
                                    sheet.Cells(dataStartRowIndex + 2, columnIndex).Value = CInt(tukurikataVo.YosanTukurikataYyyyMm.Substring(4, 2)).ToString & "月"
                                    monthColumnCount = monthColumnCount + 1
                                    dataCount = dataCount + 1

                                    minYearKs = IIf(CompareYearKs(minYearKs, yearKs) = True, minYearKs, yearKs)
                                    maxYearKs = IIf(CompareYearKs(maxYearKs, yearKs) = False, maxYearKs, yearKs)
                                End If
                                SetMonthTitlePro(sheet, columnIndex, dataStartRowIndex + 3, _
                                                 SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, dataStartRowIndex + 1, dataStartRowIndex + 2)
                                '台数セル
                                sheet.Cells(rowIndex, columnIndex).Value = tukurikataVo.DaisuSuryo

                                Dim key As String = EzUtil.MakeKey(shisakuSyubetu, unitKbn, tukurikataVo.YosanEventCode, buhinhyoName, patternName, tukurikataVo.YosanTukurikataYyyyMm)

                                If Not daisuDic.ContainsKey(key) Then
                                    daisuDic.Add(key, "'" & sheet.SheetName & "'!R" & rowIndex + 1 & "C" & columnIndex + 1)
                                End If

                                SetMonthCellPro(sheet, rowIndex, columnIndex, False)
                                columnIndex = columnIndex + 1
                                '
                                patternName = tukurikataVo.PatternName
                                vosCount = vosCount + 1
                            Next

                            'ユニット区分
                            sheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).Value = unitKbn
                            '部品表名
                            sheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX).Value = buhinhyoName

                            Dim buhinKey As String = EzUtil.MakeKey(shisakuSyubetu, unitKbn, yosanEventCode, buhinhyoName, patternName, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)
                            If Not patternDic.ContainsKey(buhinKey) Then
                                patternDic.Add(buhinKey, "'" & sheet.SheetName & "'!R" & rowIndex + 1 & "C" & SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX + 1)
                            End If

                            'パターン名
                            sheet.Cells(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX).Value = patternName
                            Dim patternKey As String = EzUtil.MakeKey(shisakuSyubetu, unitKbn, yosanEventCode, buhinhyoName, patternName, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)
                            If Not patternDic.ContainsKey(patternKey) Then
                                patternDic.Add(patternKey, "'" & sheet.SheetName & "'!R" & rowIndex + 1 & "C" & SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX + 1)
                            End If

                            '部品費'
                            Dim buhinhiKey As String = EzUtil.MakeKey(shisakuSyubetu, unitKbn, yosanEventCode, buhinhyoName, patternName, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX)
                            If Not patternDic.ContainsKey(buhinhiKey) Then
                                patternDic.Add(buhinhiKey, "'" & sheet.SheetName & "'!R" & rowIndex + 1 & "C" & SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX + 1)
                            End If

                            '型費'
                            Dim katahiKey As String = EzUtil.MakeKey(shisakuSyubetu, unitKbn, yosanEventCode, buhinhyoName, patternName, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                            If Not patternDic.ContainsKey(katahiKey) Then
                                patternDic.Add(katahiKey, "'" & sheet.SheetName & "'!R" & rowIndex + 1 & "C" & SPD_TUKURIKATA_KATAHI_COLUMN_INDEX + 1)
                            End If

                            rowIndex = rowIndex + 1
                            indexX = indexX + 1
                            dataRowCount = dataRowCount + 1
                        Next
                    Next

                    sheet.Cells(startRow, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).RowSpan = vosByHyojijun(unitKbn).Count

                    'ユニット単位で追加行の行番号
                    PatternNameInsertRowIndex(shisakuSyubetu, unitKbn) = rowIndex
                Next

                '台数合計
                CalcTukurikata(sheet, dataStartRowIndex, rowIndex, monthColumnCount)
                dataStartRowIndex = rowIndex

            End If

            sheet.Cells(borderStartRow, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).ColumnSpan = monthColumnCount - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX
            sheet.Cells(borderStartRow, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).BackColor = Color.FromArgb(255, 255, 153)
            sheet.Cells(borderStartRow + 1, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).BackColor = Color.FromArgb(255, 255, 153)
            sheet.Cells(borderStartRow + 2, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).BackColor = Color.FromArgb(255, 255, 153)

            Dim cbs As New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine, Color.Black)
            Dim cBorder As New FarPoint.Win.ComplexBorder(cbs, cbs, cbs, cbs)

            '表に加線
            If monthColumnCount > SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX Then

                '年期セルをマージする'
                For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To monthColumnCount - 1 Step 6
                    sheet.Cells(borderStartRow + 1, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
                    sheet.Cells(borderStartRow + 1, columnIndex).ColumnSpan = 6
                Next

                Dim cr As New FarPoint.Win.Spread.Model.CellRange(borderStartRow, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX, dataRowCount, monthColumnCount - 1)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            Else
                Dim cr As New FarPoint.Win.Spread.Model.CellRange(borderStartRow, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX, dataRowCount, monthColumnCount)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            End If

            If StringUtil.Equals(SHISAKU_SYUBETU_WB, shisakuSyubetu) Then
                aTukurikataListWbSum.AddRange(aTukurikataList)
            Else
                aTukurikataListKanseiSum.AddRange(aTukurikataList)
            End If


            Return aTukurikataList
        End Function

        ''' <summary>
        ''' 表示順Noによって造り方情報を返すDictionaryを作成する
        ''' </summary>
        ''' <param name="aList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MakeTukurikataVosByHyojijun(ByVal aList As List(Of TYosanTukurikataVo), ByVal shisakuSyubetu As String) As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo))))
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
        ''' SPREADで行の配置を設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetTukurikataRowPro(ByVal aSheet As SheetView, ByVal rowIndex As Integer)
            'ユニットセル
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).Locked = True
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Left
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '部品番号名セル
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX).Locked = True
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Left
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            'パターン名セル
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX).Locked = True
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Left
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '部品費（円）セル
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Locked = True
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).CellType = GetKingaku11CellType()
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '型費（円）セル
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Locked = True
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).CellType = GetKingaku11CellType()
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            '台数セル
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).Locked = True
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).CellType = GetDaisuCellType()
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).HorizontalAlignment = CellHorizontalAlignment.Right
            aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).VerticalAlignment = CellVerticalAlignment.Center
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).Locked = True
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).HorizontalAlignment = CellHorizontalAlignment.Left
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).VerticalAlignment = CellVerticalAlignment.Center
            'aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).Value = "台"

            aSheet.Rows(rowIndex).BackColor = Color.White
            aSheet.Cells(SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX, aSheet.ColumnCount - 1).RowSpan = aSheet.RowCount - 1
        End Sub

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
        ''' 台数合計
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CalcTukurikata(ByVal aSheet As SheetView, ByVal startRowIndex As Integer, ByVal endRowIndex As Integer, ByVal monthColumnCount As Integer)

            For rowIndex As Integer = startRowIndex + 3 To endRowIndex - 1
                '台数合計
                aSheet.Cells(rowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).Formula = "SUM(" & "R" & rowIndex + 1 & "C" & SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1 & ":" & "R" & rowIndex + 1 & "C" & monthColumnCount + 1 & ") & ""台"""
            Next

        End Sub

#End Region

#Region "集計値"

        ''' <summary>
        ''' 集計値
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ZaimuJiseki()
            '集計値SPREADの月分布列に列を挿入
            AddZaimuJisekiMonthColumns(yosanEventCode)

            ''造り方Spreadの部品費と型費をセット
            SetSpreadTukurikataBuhinHi(yosanEventCode, True, True)

            ''集計値Spreadに表示値をセット
            SetSpreadZaimujisekiData(yosanEventCode, True)
            ''合計
            CalcZaimuJiseki()

            ''システム日付の直近の年期(月分布列)を拡大表示
            'VisibleSystemYearMonthColumns()
        End Sub

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
                    '完成車'
                    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To SPD_TUKURIKATA_WB_MONTH_ROW_INDEX - 2
                        '部品表名とパターン名
                        If String.Equals(hiVo.BuhinhyoName, sheet.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)) AndAlso _
                           String.Equals(hiVo.PatternName, sheet.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
                            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
                                sheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
                            End If
                            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
                                sheet.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
                            End If
                            Exit For
                        End If
                    Next

                    'ホワイトボディ'
                    For rowIndex As Integer = SPD_TUKURIKATA_WB_MONTH_ROW_INDEX + 3 To SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX
                        '部品表名とパターン名
                        If String.Equals(hiVo.BuhinhyoName, sheet.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)) AndAlso _
                           String.Equals(hiVo.PatternName, sheet.GetValue(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)) Then
                            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_HIREI) Then
                                sheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi * wbKeisu
                            End If
                            If String.Equals(hiVo.HireiKoteiKbn, HIREI_KOTEI_KBN_KOTEI) Then
                                sheet.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = hiVo.YosanBuhinHi
                            End If
                            Exit For
                        End If
                    Next
                Next

            Next

        End Sub

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


        ''' <summary>
        ''' 集計値SPREADの月分布列に列を挿入
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub AddZaimuJisekiMonthColumns(ByVal eventCode As String)

            _YosanEventVo = EventDao.FindByPk(eventCode)
            yearMonths = New List(Of String)
            '最小の年、期
            Dim minYearKs As String = GetSpreadMinYearKs(_YosanEventVo.YosanCode)
            '最大の年、期
            Dim maxYearKs As String = GetSpreadMaxYearKs(_YosanEventVo.YosanCode)

            'データ無
            If StringUtil.IsEmpty(minYearKs) AndAlso StringUtil.IsEmpty(maxYearKs) Then
                Return
            End If

            '2016/02/18 kabasawa'
            'まとめて集計するときに利用する'
            '_minYearKs = minYearKs
            '_maxYearKs = maxYearKs

            'YYYYMM
            'Dim yearMonths As New List(Of String)

            yearMonths = CreateYearMonthList(minYearKs, maxYearKs)


            If yearMonths.Count + SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX > sheet.ColumnCount Then
                sheet.AddColumns(sheet.ColumnCount - 1, sheet.ColumnCount - 1 + yearMonths.Count)
            End If

            '集計値SPREADの月分布列に列を挿入
            Dim columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX
            For index As Integer = 0 To yearMonths.Count - 1
                ''合計列追加
                'If (columnIndex - SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                '    AddSpdMonthCountColumn(sheet, columnIndex, SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, _
                '                           SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
                '    SetMonthColumnProKingaku(sheet, columnIndex, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
                '                             SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
                '    columnIndex = columnIndex + 1
                'End If

                'sheet.AddColumns(columnIndex, 1)
                SetMonthColumnProKingaku(sheet, columnIndex, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
                                         SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
                '年
                sheet.Cells(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).Value = ChangeYearMonthToYearKs(yearMonths(index).Substring(0, 4), yearMonths(index).Substring(4))
                '月
                sheet.Cells(SPD_ZAIMUJISEKI_MONTH_ROW_INDEX, columnIndex).Value = CInt(yearMonths(index).Substring(4, 2)).ToString & "月"
                columnIndex = columnIndex + 1

                For row As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX
                    If Not zaimuDataDic(row).ContainsKey(yearMonths(index)) Then
                        zaimuDataDic(row).Add(yearMonths(index), New List(Of String))
                    End If
                Next

            Next
            '合計列追加
            'AddSpdMonthCountColumn(sheet, columnIndex, SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, _
            '                       SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
            SetMonthColumnProKingaku(sheet, columnIndex, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
                                     SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)

            'タイトル部の作成'
            CreateZaimuTitle()

        End Sub

        ''' <summary>
        ''' タイトル作成
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CreateZaimuTitle()
            'タイトル部分の作成'
            sheet.Cells(SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, 1).Value = "≪集計値≫"
            sheet.Cells(SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, 1).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, 1).RowSpan = 3
            sheet.Cells(SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, 1).ColumnSpan = 4

            sheet.Cells(SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX).Value = "月分布"
            sheet.Cells(SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX).ColumnSpan = yearMonths.Count

            sheet.Cells(SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + yearMonths.Count).Value = "合計"
            sheet.Cells(SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + yearMonths.Count).BackColor = Color.Azure


            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, 1).Value = "比例費"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, 1).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, 1).RowSpan = 15

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, 2).Value = "見通し"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, 2).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, 2).RowSpan = 6

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_METAL_ROWS_INDEX, 2).Value = "発注実績"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_METAL_ROWS_INDEX, 2).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_METAL_ROWS_INDEX, 2).RowSpan = 3

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX, 2).Value = "財務実績"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX, 2).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX, 2).RowSpan = 6


            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, 3).Value = "メタル部品費"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX, 3).Value = "トリム部品費"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX, 3).Value = "鋼板材料"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX, 3).Value = "輸送費"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX, 3).Value = "移管車＆生産部実績"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_KEI_ROWS_INDEX, 3).Value = "計"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_KEI_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_KEI_ROWS_INDEX, 3).ColumnSpan = 2


            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_METAL_ROWS_INDEX, 3).Value = "メタル部品費"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_METAL_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_METAL_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_TRIM_ROWS_INDEX, 3).Value = "トリム部品費"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_TRIM_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_TRIM_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_KEI_ROWS_INDEX, 3).Value = "計"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_KEI_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_KEI_ROWS_INDEX, 3).ColumnSpan = 2


            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX, 3).Value = "メタル部品費"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX, 3).Value = "トリム部品費"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX, 3).Value = "鋼板材料"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX, 3).Value = "輸送費"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX, 3).Value = "移管車＆生産部実績"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_KEI_ROWS_INDEX, 3).Value = "計"
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_KEI_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_KEI_ROWS_INDEX, 3).ColumnSpan = 2



            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, 1).Value = "固定費"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, 1).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, 1).RowSpan = 9

            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, 2).Value = "見通し"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, 2).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, 2).RowSpan = 3

            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_METAL_ROWS_INDEX, 2).Value = "発注実績"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_METAL_ROWS_INDEX, 2).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_METAL_ROWS_INDEX, 2).RowSpan = 3

            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX, 2).Value = "財務実績"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX, 2).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX, 2).RowSpan = 3

            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, 3).Value = "メタル部品費"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX, 3).Value = "トリム部品費"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_KEI_ROWS_INDEX, 3).Value = "計"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_KEI_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_KEI_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_METAL_ROWS_INDEX, 3).Value = "メタル部品費"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_METAL_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_METAL_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_TRIM_ROWS_INDEX, 3).Value = "トリム部品費"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_TRIM_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_TRIM_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_KEI_ROWS_INDEX, 3).Value = "計"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_KEI_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_KEI_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX, 3).Value = "メタル部品費"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX, 3).Value = "トリム部品費"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX, 3).ColumnSpan = 2

            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX, 3).Value = "計"
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX, 3).BackColor = Color.Azure
            sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX, 3).ColumnSpan = 2


            Dim cbs As New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine, Color.Black)
            Dim cBorder As New FarPoint.Win.ComplexBorder(cbs, cbs, cbs, cbs)
            If yearMonths.Count > 0 Then
                Dim cr As New FarPoint.Win.Spread.Model.CellRange(SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, 1, SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX - SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX + 1, 5 + yearMonths.Count)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            Else
                Dim cr As New FarPoint.Win.Spread.Model.CellRange(SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX, 1, SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX - SPD_ZAIMUJISEKI_MONTH_TITLE_ROW_INDEX + 1, 5)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            End If


            'xls.MergeCells(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 2, True)
            'xls.SetValue(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex + 3, "比例比")
            'xls.MergeCells(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex + 3, ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex + 17, True)
            'xls.SetValue(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex + 18, "固定費")
            'xls.MergeCells(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex + 18, ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex + 26, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 3, "見通し")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 3, ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 8, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 9, "発注実績")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 9, ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 11, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 12, "財務実績")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 12, ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 17, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 18, "見通し")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 18, ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 20, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 21, "発注実績")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 21, ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 23, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 24, "財務実績")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 24, ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 26, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 3, "メタル部品費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 3, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 3, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 4, "トリム部品費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 4, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 4, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 5, "鋼板材料")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 5, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 5, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 6, "輸送費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 6, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 6, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 7, "移管車＆生産部実績")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 7, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 7, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 8, "計")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 8, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 8, True)
            'xls.SetAlignment(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 8, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 8, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 9, "メタル部品費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 9, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 9, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 10, "トリム部品費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 10, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 10, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 11, "計")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 11, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 11, True)
            'xls.SetAlignment(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 11, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 11, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 12, "メタル部品費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 12, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 12, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 13, "鋼板材料")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 13, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 13, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 14, "輸送費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 14, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 14, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 15, "移管車＆生産部実績")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 15, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 15, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 16, "トリム部品費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 16, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 16, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 17, "計")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 17, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 17, True)
            'xls.SetAlignment(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 17, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 17, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 18, "メタル部品費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 18, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 18, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 19, "トリム部品費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 19, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 19, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 20, "計")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 20, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 20, True)
            'xls.SetAlignment(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 20, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 20, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 21, "メタル部品費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 21, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 21, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 22, "トリム部品費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 22, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 22, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 23, "計")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 23, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 23, True)
            'xls.SetAlignment(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 23, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 23, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 24, "メタル部品費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 24, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 24, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 25, "トリム部品費")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 25, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 25, True)
            'xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 26, "計")
            'xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 26, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 26, True)
            'xls.SetAlignment(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 26, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 26, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)

            'xls.SetBackColor(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 26, RGB(204, 255, 255))
            'xls.SetLine(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 26, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

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
            'シートから探すのはイヤ'

            'Dim minYearKs As String = Nothing
            ''完成車Spreadにデータが有れば
            'If tukurikataDataColumnCount > 0 Then
            '    'If sheet.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
            '    'YYYYX期を取得
            '    Dim kanseiMonth As String = sheet.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + HALF_MONTH_DEFAULT_COUNT)

            '    minYearKs = kanseiMonth
            'End If
            ''ﾎﾜｲﾄﾎﾞﾃﾞｨSpreadにデータが有れば
            'If tukurikataWBDataColumnCount > 0 Then
            '    'If sheet.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
            '    'YYYYX期を取得
            '    Dim wbMonth As String = sheet.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + HALF_MONTH_DEFAULT_COUNT)

            '    If StringUtil.IsNotEmpty(minYearKs) Then
            '        minYearKs = IIf(CompareYearKs(minYearKs, wbMonth) = True, minYearKs, wbMonth)
            '    Else
            '        minYearKs = wbMonth
            '    End If
            'End If
            ''金材Spreadにデータが有れば
            'If kanazaiDataColumnCount > 0 Then
            '    'If sheet.ColumnCount - SPD_KANAZAI_DEFAULT_COLUMN_COUNT > 0 Then
            '    Dim kanazaiMonth As String = sheet.GetValue(SPD_KANAZAI_MONTH_ROW_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + HALF_MONTH_DEFAULT_COUNT)

            '    If StringUtil.IsNotEmpty(minYearKs) Then
            '        minYearKs = IIf(CompareYearKs(minYearKs, kanazaiMonth) = True, minYearKs, kanazaiMonth)
            '    Else
            '        minYearKs = kanazaiMonth
            '    End If
            'End If

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
            'Dim maxYearKs As String = Nothing

            ''完成車Spreadにデータが有れば
            'If sheet.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
            '    'YYYYX期を取得
            '    Dim kanseiMonth As String = sheet.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, sheet.ColumnCount - 2)

            '    maxYearKs = kanseiMonth
            'End If
            ''ﾎﾜｲﾄﾎﾞﾃﾞｨSpreadにデータが有れば
            'If sheet.ColumnCount - SPD_TUKURIKATA_DEFAULT_COLUMN_COUNT > 0 Then
            '    'YYYYX期を取得
            '    Dim wbMonth As String = sheet.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, sheet.ColumnCount - 2)

            '    If StringUtil.IsNotEmpty(maxYearKs) Then
            '        maxYearKs = IIf(CompareYearKs(maxYearKs, wbMonth) = False, maxYearKs, wbMonth)
            '    Else
            '        maxYearKs = wbMonth
            '    End If
            'End If
            ''金材Spreadにデータが有れば
            'If sheet.ColumnCount - SPD_KANAZAI_DEFAULT_COLUMN_COUNT > 0 Then
            '    Dim kanazaiMonth As String = sheet.GetValue(SPD_KANAZAI_MONTH_ROW_INDEX, sheet.ColumnCount - 2)

            '    If StringUtil.IsNotEmpty(maxYearKs) Then
            '        maxYearKs = IIf(CompareYearKs(maxYearKs, kanazaiMonth) = False, maxYearKs, kanazaiMonth)
            '    Else
            '        maxYearKs = kanazaiMonth
            '    End If
            'End If

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
            Dim zaimuJisekiMaxYearKs As String = GetYosanZaimuJisekiYearMonth(yosanCode, True)
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
        ''' 年、期を比較する。strComp1＜strComp2だら、trueを返す。
        ''' </summary>
        ''' <param name="strComp1"></param>
        ''' <param name="strComp2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CompareYearKs(ByVal strComp1 As String, ByVal strComp2 As String) As Boolean
            'strComp1＜strComp2だら、trueを返す。その以外、falseを返す。
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

        ''' <summary>
        ''' 「製作台数」の工事指令№で「T_SEISAKU_AS_KOUNYU_YOSAN」の「割り振る先の年月」の年月を取得
        ''' </summary>
        ''' <param name="isMax"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSeisakuAsKounyuYosanYearMonth(ByVal isMax As Boolean) As String
            Dim rtnYearKs As String = Nothing

            For columnIndex As Integer = SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN To sheet.ColumnCount - 1 Step 2
                Dim kojishireiNo As String = sheet.GetValue(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex)
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
        ''' SPREADで列の配置を設定する。
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetMonthColumnProKingaku(ByVal sheet As SheetView, ByVal columnIndex As Integer, _
                                      ByVal startRow As Integer, ByVal startCol As Integer, ByVal yearRow As Integer, ByVal monthRow As Integer)
            '列幅をセット
            sheet.Columns(columnIndex).Width = 95
            '年セル
            'sheet.Cells(yearRow, columnIndex).Locked = True
            sheet.Cells(yearRow, columnIndex).BackColor = Color.Azure
            sheet.Cells(yearRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            sheet.Cells(yearRow, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            '月セル
            'sheet.Cells(monthRow, columnIndex).Locked = True
            sheet.Cells(monthRow, columnIndex).BackColor = Color.Azure
            sheet.Cells(monthRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            sheet.Cells(monthRow, columnIndex).VerticalAlignment = CellVerticalAlignment.Center

            '拡大、縮小ボタンを水平方向の配置
            If (columnIndex - startCol + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 OrElse _
               (columnIndex - startCol) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                sheet.Cells(yearRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Center
            End If

            '金額セル
            For rowIndex As Integer = startRow To SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX
                If rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX OrElse _
                   rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX Then
                    '比例費の見通し材料、輸送
                    'sheet.Cells(rowIndex, columnIndex).Locked = False
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
                    'sheet.Cells(rowIndex, columnIndex).Locked = False
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
                    'sheet.Cells(rowIndex, columnIndex).Locked = True
                    'sheet.Cells(rowIndex, columnIndex).CellType = GetKingaku11CellType()
                    sheet.Cells(rowIndex, columnIndex).BackColor = Color.Azure
                Else
                    'sheet.Cells(rowIndex, columnIndex).Locked = True
                    'sheet.Cells(rowIndex, columnIndex).CellType = GetKingaku11CellType()
                    sheet.Cells(rowIndex, columnIndex).BackColor = Color.WhiteSmoke
                End If
                sheet.Cells(rowIndex, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Right
                sheet.Cells(rowIndex, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            Next

        End Sub


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
            For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To yearMonths.Count + SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX - 1
                '合計列以外
                'If Not ((columnIndex - SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                Dim yearMonth As String = ChangeSpreadYearToDBYear(sheet.GetValue(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                   sheet.GetValue(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                   CInt(sheet.GetValue(SPD_ZAIMUJISEKI_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))


                For rowindex As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX
                    zaimuDataDic(rowindex)(yearMonth).Add("('" & sheet.SheetName & "'!R" & rowindex + 1 & "C" & columnIndex + 1 & ")")
                Next

                '20160224 kabasawa'
                '関数で実験'
                '比例見通しメタル'
                If yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX).ContainsKey(yearMonth) Then
                    Dim formula As String = ""
                    For Each str As String In yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX)(yearMonth)
                        formula = formula & str & "+"
                    Next
                    sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, columnIndex).Formula = Left(formula, formula.Length - 1)
                End If
                '比例見通しトリム'
                If yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX).ContainsKey(yearMonth) Then
                    Dim formula As String = ""
                    For Each str As String In yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX)(yearMonth)
                        formula = formula & str & "+"
                    Next
                    sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX, columnIndex).Formula = Left(formula, formula.Length - 1)
                End If
                '固定見通しメタル'
                If yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX).ContainsKey(yearMonth) Then
                    Dim formula As String = ""
                    For Each str As String In yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX)(yearMonth)
                        formula = formula & str & "+"
                    Next
                    sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, columnIndex).Formula = Left(formula, formula.Length - 1)
                End If
                '固定見通しトリム'
                If yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX).ContainsKey(yearMonth) Then
                    Dim formula As String = ""
                    For Each str As String In yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX)(yearMonth)
                        formula = formula & str & "+"
                    Next
                    sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX, columnIndex).Formula = Left(formula, formula.Length - 1)
                End If



                '財務実績
                For Each zaimuVo As TYosanZaimuJisekiVo In _ZaimujisekiList
                    If String.Equals(yearMonth, zaimuVo.YosanZaimuJisekiYyyyMm) Then
                        '比例費の場合
                        If IsHirei(zaimuVo.YosanZaimuHireiKoteiKbn) Then
                            'メタル部品費
                            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_METAL) Then
                                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                            End If
                            '鋼板材料
                            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU) Then
                                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                            End If
                            '輸送費
                            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_YUSOU) Then
                                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                            End If
                            '移管車＆生産部実績
                            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_SEISAN) Then
                                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                            End If
                            'トリム部品費
                            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_TRIM) Then
                                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                            End If
                        End If

                        '固定費の場合
                        If IsKotei(zaimuVo.YosanZaimuHireiKoteiKbn) Then
                            'メタル部品費
                            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_KOTEI_METAL) Then
                                sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                            End If
                            'トリム部品費
                            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_KOTEI_TRIM) Then
                                sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                            End If
                        End If
                    End If
                Next
                '見通し
                For Each mitoshiVo As TYosanEventMitoshiVo In _EventMitoshiList
                    If String.Equals(yearMonth, mitoshiVo.YosanEventMitoshiYyyyMm) Then
                        '鋼板材料
                        If String.Equals(mitoshiVo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU) Then
                            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX, columnIndex).Value = mitoshiVo.YosanEventMitoshiInputQty
                        End If
                        '輸送費
                        If String.Equals(mitoshiVo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_YUSOU) Then
                            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX, columnIndex).Value = mitoshiVo.YosanEventMitoshiInputQty
                        End If
                        '移管車＆生産部実績
                        If String.Equals(mitoshiVo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_SEISAN) Then
                            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX, columnIndex).Value = mitoshiVo.YosanEventMitoshiInputQty
                        End If
                    End If
                Next
                '初期表示以外の場合、移管車＆生産部実績値は金材SPREADのデータを積上げる
                If Not isInitShow Then
                    For Each key As String In _SeisanbuJisekiList.Keys
                        If String.Equals(yearMonth, key) Then
                            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX, columnIndex).Value = _SeisanbuJisekiList(key)
                            Exit For
                        End If
                    Next
                End If
                '発注実績
                For Each unitKbn As String In _HireiHatchuJisekiList.Keys
                    For Each keyYearMonth As String In _HireiHatchuJisekiList(unitKbn).Keys
                        If String.Equals(yearMonth, keyYearMonth) Then
                            If IsUnitMetal(unitKbn) Then
                                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_METAL_ROWS_INDEX, columnIndex).Value = _HireiHatchuJisekiList(unitKbn)(keyYearMonth)
                            Else
                                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_TRIM_ROWS_INDEX, columnIndex).Value = _HireiHatchuJisekiList(unitKbn)(keyYearMonth)
                            End If
                            Exit For
                        End If
                    Next
                Next
                For Each unitKbn As String In _KoteiHatchuJisekiList.Keys
                    For Each keyYearMonth As String In _KoteiHatchuJisekiList(unitKbn).Keys
                        If String.Equals(yearMonth, keyYearMonth) Then
                            If IsUnitMetal(unitKbn) Then
                                sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_METAL_ROWS_INDEX, columnIndex).Value = _KoteiHatchuJisekiList(unitKbn)(keyYearMonth)
                            Else
                                sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_TRIM_ROWS_INDEX, columnIndex).Value = _KoteiHatchuJisekiList(unitKbn)(keyYearMonth)
                            End If
                            Exit For
                        End If
                    Next
                Next
                'End If
            Next

            For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To yearMonths.Count + SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX - 1 Step 6
                '列マージする。'
                sheet.Cells(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).ColumnSpan = 6
                sheet.Cells(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.General
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
            If tukurikataDataColumnCount > 0 Then
                For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To tukurikataDataColumnCount + SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX - 1
                    Dim hireiMetalBuhinHi As Decimal = 0
                    Dim hireiTrimBuhinHi As Decimal = 0
                    '合計列以外
                    'If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then

                    Dim year As String = sheet.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4)
                    Dim ks As String = sheet.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4)
                    Dim month As String = CInt(sheet.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00")

                    Dim yearMonth As String = ChangeSpreadYearToDBYear(year, ks, month)

                    For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To SPD_TUKURIKATA_WB_MONTH_ROW_INDEX - 2


                        'ユニット区分'
                        Dim unitkbn As String = sheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)
                        '部品費'
                        Dim buhinHi As String = sheet.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX)
                        '台数'
                        Dim daisu As String = sheet.GetValue(rowIndex, columnIndex)

                        Dim str As String = "(R" & rowIndex + 1 & "C" & columnIndex + 1 & "*R" & rowIndex + 1 & "C" & SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX + 1 & ")"


                        'メタルの場合
                        If String.Equals(UNIT_KBN_METAL_CODE, unitkbn) Then
                            'If String.Equals(UNIT_KBN_METAL_NAME, unitkbn) Then
                            If StringUtil.IsNotEmpty(daisu) AndAlso _
                               StringUtil.IsNotEmpty(buhinHi) Then
                                hireiMetalBuhinHi = hireiMetalBuhinHi + CInt(daisu) * CDec(buhinHi)
                            End If

                            If Not yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX).ContainsKey(yearMonth) Then
                                yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX).Add(yearMonth, New List(Of String))
                            End If

                            yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX)(yearMonth).Add(str)
                        End If
                        'トリムの場合
                        If String.Equals(UNIT_KBN_TRIM_CODE, unitkbn) Then
                            'If String.Equals(UNIT_KBN_TRIM_NAME, unitkbn) Then
                            If StringUtil.IsNotEmpty(daisu) AndAlso _
                               StringUtil.IsNotEmpty(buhinHi) Then
                                hireiTrimBuhinHi = hireiTrimBuhinHi + CInt(daisu) * CDec(buhinHi)
                            End If

                            If Not yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX).ContainsKey(yearMonth) Then
                                yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX).Add(yearMonth, New List(Of String))
                            End If
                            yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX)(yearMonth).Add(str)
                        End If


                    Next
                    'これは合計値なのでセル参照して関数を突っ込む形に切り替える'

                    _HireiMetalHiList.Add(yearMonth, hireiMetalBuhinHi)
                    _HireiTrimHiList.Add(yearMonth, hireiTrimBuhinHi)
                    'End If
                Next
            End If
            'ﾎﾜｲﾄﾎﾞﾃﾞｨSpreadの部品費を集計
            If tukurikataWBDataColumnCount > 0 Then
                For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To tukurikataWBDataColumnCount + SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX - 1
                    Dim hireiMetalBuhinHi As Decimal = 0
                    Dim hireiTrimBuhinHi As Decimal = 0
                    '合計列以外
                    'If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                    Dim yearMonth As String = ChangeSpreadYearToDBYear(sheet.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                       sheet.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                       CInt(sheet.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))



                    For rowIndex As Integer = SPD_TUKURIKATA_WB_MONTH_ROW_INDEX + 3 To SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX - 2
                        Dim unitkbn As String = sheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)
                        Dim buhinHi As String = sheet.GetValue(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX)
                        Dim daisu As String = sheet.GetValue(rowIndex, columnIndex)


                        Dim str As String = "(R" & rowIndex + 1 & "C" & columnIndex + 1 & "*R" & rowIndex + 1 & "C" & SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX + 1 & ")"

                        'メタルの場合
                        If String.Equals(UNIT_KBN_METAL_CODE, unitkbn) Then
                            'If String.Equals(UNIT_KBN_METAL_NAME, unitkbn) Then
                            If StringUtil.IsNotEmpty(daisu) AndAlso _
                               StringUtil.IsNotEmpty(buhinHi) Then
                                hireiMetalBuhinHi = hireiMetalBuhinHi + CInt(daisu) * CDec(buhinHi)
                            End If

                            If Not yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX).ContainsKey(yearMonth) Then
                                yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX).Add(yearMonth, New List(Of String))
                            End If

                            yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX)(yearMonth).Add(str)
                        End If
                        'トリムの場合
                        If String.Equals(UNIT_KBN_TRIM_CODE, unitkbn) Then
                            'If String.Equals(UNIT_KBN_TRIM_NAME, unitkbn) Then
                            If StringUtil.IsNotEmpty(daisu) AndAlso _
                               StringUtil.IsNotEmpty(buhinHi) Then
                                hireiTrimBuhinHi = hireiTrimBuhinHi + CInt(daisu) * CDec(buhinHi)
                            End If


                            If Not yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX).ContainsKey(yearMonth) Then
                                yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX).Add(yearMonth, New List(Of String))
                            End If
                            yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX)(yearMonth).Add(str)

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
                    'End If
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
            If tukurikataDataColumnCount > 0 Then
                For rowIndex As Integer = SPD_TUKURIKATA_PATTERNNAME_ROWS_START_INDEX To SPD_TUKURIKATA_WB_MONTH_ROW_INDEX - 3
                    For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To tukurikataDataColumnCount + SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX - 1
                        '合計列以外
                        'If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                        Dim daisu As String = sheet.GetValue(rowIndex, columnIndex)
                        Dim buhinHi As String = sheet.GetValue(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                        If StringUtil.IsNotEmpty(daisu) AndAlso CInt(daisu) > 0 AndAlso _
                           StringUtil.IsNotEmpty(buhinHi) AndAlso CDec(buhinHi) > 0 Then
                            Dim yearMonth As String = ChangeSpreadYearToDBYear(sheet.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                               sheet.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                               CInt(sheet.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))


                            Dim str As String = "(R" & rowIndex + 1 & "C" & columnIndex + 1 & "*R" & rowIndex + 1 & "C" & SPD_TUKURIKATA_KATAHI_COLUMN_INDEX + 1 & ")"


                            'メタルの場合
                            If String.Equals(UNIT_KBN_METAL_CODE, sheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                'If String.Equals(UNIT_KBN_METAL_NAME, sheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                If CInt(yearMonth) < CInt(metalYearMonth) Then
                                    '最小年月を取得
                                    metalYearMonth = yearMonth
                                    metalBuhinHi = sheet.GetValue(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                                End If

                                If Not yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX).ContainsKey(yearMonth) Then
                                    yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX).Add(yearMonth, New List(Of String))
                                End If

                                yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX)(yearMonth).Add(str)

                            End If
                            'トリムの場合
                            If String.Equals(UNIT_KBN_TRIM_CODE, sheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                'If String.Equals(UNIT_KBN_TRIM_NAME, sheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                If CInt(yearMonth) < CInt(trimYearMonth) Then
                                    '最小年月を取得
                                    trimYearMonth = yearMonth
                                    trimBuhinHi = CDec(buhinHi)
                                End If

                                If Not yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX).ContainsKey(yearMonth) Then
                                    yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX).Add(yearMonth, New List(Of String))
                                End If
                                yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX)(yearMonth).Add(str)

                            End If
                        End If
                        'End If
                    Next
                Next
            End If
            'ﾎﾜｲﾄﾎﾞﾃﾞｨSpreadの部品費を集計
            If tukurikataWBDataColumnCount > 0 Then
                For rowIndex As Integer = SPD_TUKURIKATA_WB_MONTH_ROW_INDEX + 3 To SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX - 2
                    For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + tukurikataWBDataColumnCount - 1
                        '合計列以外
                        If Not ((columnIndex - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                            Dim daisu As String = sheet.GetValue(rowIndex, columnIndex)
                            Dim buhinHi As String = sheet.GetValue(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                            If StringUtil.IsNotEmpty(daisu) AndAlso CInt(daisu) > 0 AndAlso _
                               StringUtil.IsNotEmpty(buhinHi) AndAlso CDec(buhinHi) > 0 Then
                                Dim yearMonth As String = ChangeSpreadYearToDBYear(sheet.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                                   sheet.GetValue(SPD_TUKURIKATA_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                                   CInt(sheet.GetValue(SPD_TUKURIKATA_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))


                                Dim str As String = "(R" & rowIndex + 1 & "C" & columnIndex + 1 & "*R" & rowIndex + 1 & "C" & SPD_TUKURIKATA_KATAHI_COLUMN_INDEX + 1 & ")"

                                'メタルの場合
                                If String.Equals(UNIT_KBN_METAL_CODE, sheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    'If String.Equals(UNIT_KBN_METAL_NAME, sheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    If CInt(yearMonth) < CInt(metalYearMonth) Then
                                        '最小年月を取得
                                        metalYearMonth = yearMonth
                                        metalBuhinHi = sheet.GetValue(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                                    End If

                                    If Not yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX).ContainsKey(yearMonth) Then
                                        yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX).Add(yearMonth, New List(Of String))
                                    End If

                                    yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX)(yearMonth).Add(str)

                                End If
                                'トリムの場合
                                If String.Equals(UNIT_KBN_TRIM_CODE, sheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    'If String.Equals(UNIT_KBN_TRIM_NAME, sheet.GetValue(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX)) Then
                                    If CInt(yearMonth) < CInt(trimYearMonth) Then
                                        '最小年月を取得
                                        trimYearMonth = yearMonth
                                        trimBuhinHi = CDec(buhinHi)
                                    End If

                                    If Not yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX).ContainsKey(yearMonth) Then
                                        yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX).Add(yearMonth, New List(Of String))
                                    End If
                                    yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX)(yearMonth).Add(str)

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
            If sheet.ColumnCount - SPD_KANAZAI_DEFAULT_COLUMN_COUNT > 0 Then
                For columnIndex As Integer = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX To sheet.ColumnCount - 2
                    Dim hiCount As Decimal = 0
                    '合計列以外
                    If Not ((columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                        Dim yearMonth As String = ChangeSpreadYearToDBYear(sheet.GetValue(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                           sheet.GetValue(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                           CInt(sheet.GetValue(SPD_KANAZAI_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))
                        For rowIndex As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX To sheet.RowCount - 2
                            Dim tanka As String = sheet.GetValue(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX)
                            Dim daisu As String = sheet.GetValue(rowIndex, columnIndex)
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
            For columnIndex As Integer = SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN To sheet.ColumnCount - 1 Step 2
                Dim kojishireiNo As String = sheet.GetValue(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, columnIndex)
                Dim unitKbn As String = UnitKbnCode(sheet.GetValue(SPD_DAISU_UNITKBN_ROW_INDEX, columnIndex))
                'Dim daisuCount As Integer = 0
                ''完成車とＷＢ車の台数合計
                'If StringUtil.IsNotEmpty(sheet.GetValue(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex)) Then
                '    daisuCount = daisuCount + CInt(sheet.GetValue(SPD_DAISU_KANSEISHA_ROW_INDEX, columnIndex))
                'End If
                'If StringUtil.IsNotEmpty(sheet.GetValue(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex)) Then
                '    daisuCount = daisuCount + CInt(sheet.GetValue(SPD_DAISU_WBSHA_ROW_INDEX, columnIndex))
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

#End Region

#Region "集計値合計"
        ''' <summary>
        ''' 集計値Spread合計
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CalcZaimuJiseki()
            '無いものは集計しない'
            If yearMonths.Count = 0 Then
                Exit Sub
            End If

            '月分布列の合計
            For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + yearMonths.Count
                'Dim columnCount As Decimal = 0
                'For rowIndex As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To sheet.RowCount - 1
                '    If rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_KEI_ROWS_INDEX OrElse _
                '       rowIndex = SPD_ZAIMUJISEKI_HIREI_HATCHU_KEI_ROWS_INDEX OrElse _
                '       rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_KEI_ROWS_INDEX OrElse _
                '       rowIndex = SPD_ZAIMUJISEKI_KOTEI_MITOSHI_KEI_ROWS_INDEX OrElse _
                '       rowIndex = SPD_ZAIMUJISEKI_KOTEI_HATCHU_KEI_ROWS_INDEX OrElse _
                '       rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX Then
                '        '合計
                '        sheet.Cells(rowIndex, columnIndex).Value = columnCount

                '        columnCount = 0
                '        Continue For
                '    End If
                '    If StringUtil.IsNotEmpty(sheet.GetValue(rowIndex, columnIndex)) Then
                '        columnCount = columnCount + sheet.GetValue(rowIndex, columnIndex)
                '    End If
                'Next
                '比例費'
                '見通し計'
                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_KEI_ROWS_INDEX, columnIndex).Formula = "SUM(R[-4]C:R[-1]C)"
                '発注実績計'
                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_KEI_ROWS_INDEX, columnIndex).Formula = "SUM(R[-2]C:R[-1]C)"
                '財務実績計'
                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_KEI_ROWS_INDEX, columnIndex).Formula = "SUM(R[-5]C:R[-1]C)"

                '固定費'
                '見通し計'
                sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_KEI_ROWS_INDEX, columnIndex).Formula = "SUM(R[-2]C:R[-1]C)"
                '発注実績計'
                sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_KEI_ROWS_INDEX, columnIndex).Formula = "SUM(R[-2]C:R[-1]C)"
                '財務実績計'
                sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX, columnIndex).Formula = "SUM(R[-2]C:R[-1]C)"

            Next

            '行の合計
            For rowIndex As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX
                'Dim RowCount As Decimal = 0
                'Dim monthCount As Decimal = 0
                'For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To sheet.ColumnCount - 2
                '    '合計列以外
                '    If Not ((columnIndex - SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0) Then
                '        If StringUtil.IsNotEmpty(sheet.GetValue(rowIndex, columnIndex)) Then
                '            monthCount = monthCount + sheet.GetValue(rowIndex, columnIndex)
                '        End If
                '    Else
                '        sheet.Cells(rowIndex, columnIndex).Value = monthCount
                '        RowCount = RowCount + monthCount

                '        monthCount = 0
                '    End If
                'Next
                ''合計
                'Dim rowCountCol As Integer = sheet.ColumnCount - 1
                'sheet.Cells(rowIndex, rowCountCol).Value = RowCount

                If rowIndex = SPD_ZAIMUJISEKI_HIREI_MITOSHI_KEI_ROWS_INDEX OrElse _
                   rowIndex = SPD_ZAIMUJISEKI_HIREI_HATCHU_KEI_ROWS_INDEX OrElse _
                   rowIndex = SPD_ZAIMUJISEKI_HIREI_ZAIMU_KEI_ROWS_INDEX OrElse _
                   rowIndex = SPD_ZAIMUJISEKI_KOTEI_MITOSHI_KEI_ROWS_INDEX OrElse _
                   rowIndex = SPD_ZAIMUJISEKI_KOTEI_HATCHU_KEI_ROWS_INDEX OrElse _
                   rowIndex = SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX Then
                    Continue For
                End If

                sheet.Cells(rowIndex, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX + yearMonths.Count).Formula = "SUM(RC[-" & yearMonths.Count & "]:RC[-1])"

            Next

        End Sub
#End Region


#End Region

#Region "セルタイプ"


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
            'sheet.Cells(yearRow, columnIndex).Locked = True
            'sheet.Cells(yearRow, columnIndex).BackColor = Color.Khaki
            sheet.Cells(yearRow, columnIndex).BackColor = Color.FromArgb(255, 255, 153)

            sheet.Cells(yearRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            sheet.Cells(yearRow, columnIndex).VerticalAlignment = CellVerticalAlignment.Center
            '拡大、縮小ボタンを水平方向の配置
            If (columnIndex - startCol + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                sheet.Cells(yearRow, columnIndex - 1).HorizontalAlignment = CellHorizontalAlignment.Center
                sheet.Cells(yearRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Center
            End If
            '月セル
            'sheet.Cells(monthRow, columnIndex).Locked = True
            'sheet.Cells(monthRow, columnIndex).BackColor = Color.Khaki
            sheet.Cells(monthRow, columnIndex).BackColor = Color.FromArgb(255, 255, 153)

            sheet.Cells(monthRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
            sheet.Cells(monthRow, columnIndex).VerticalAlignment = CellVerticalAlignment.Center

            'sheet.Cells(sheet.RowCount - 1, columnIndex).Locked = True
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
            'sheet.Cells(rowIndex, columnIndex).Locked = locked
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
            styleinfo.Font = New Font("MS Pゴシック", 9, FontStyle.Bold)  '太文字に
            'styleinfo.Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
            styleinfo.BackColor = Color.Azure
            styleinfo.VerticalAlignment = CellVerticalAlignment.Center
            'styleinfo.Locked = True
            '年
            If HALF_MONTH_DEFAULT_COUNT <= columnIndex Then
                sheet.Cells(yearRow, columnIndex - HALF_MONTH_DEFAULT_COUNT).ColumnSpan = HALF_MONTH_DEFAULT_COUNT
                '該当セルの文字太を変更する。
                sheet.SetStyleInfo(yearRow, columnIndex - HALF_MONTH_DEFAULT_COUNT, styleinfo)
            Else
                sheet.Cells(yearRow, columnIndex).ColumnSpan = HALF_MONTH_DEFAULT_COUNT
                '該当セルの文字太を変更する。
                sheet.SetStyleInfo(yearRow, columnIndex, styleinfo)
            End If


            '◀◀縮小
            'sheet.Cells(yearRow, columnIndex - 1).CellType = GetSmallerMonthCellType()
            'sheet.Cells(yearRow, columnIndex - 1).HorizontalAlignment = CellHorizontalAlignment.Center

            'sheet.AddColumns(columnIndex, 1)
            columnIndex = columnIndex + 1
            SetMonthTitlePro(sheet, columnIndex, startRow, startCol, yearRow, monthRow)

            '拡大▶▶
            'sheet.Cells(yearRow, columnIndex).CellType = GetBiggerMonthCellType()
            'sheet.Cells(yearRow, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Center
            '月
            If HALF_MONTH_DEFAULT_COUNT <= columnIndex Then
                sheet.Cells(monthRow, columnIndex).Value = sheet.GetValue(yearRow, columnIndex - HALF_MONTH_DEFAULT_COUNT)
            Else
                sheet.Cells(monthRow, columnIndex).Value = sheet.GetValue(yearRow, columnIndex)
            End If



            '該当セルの文字太を変更する。
            sheet.SetStyleInfo(monthRow, columnIndex, styleinfo)

            '台数セル
            For rowIndex As Integer = startRow To SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX
                SetMonthCellPro(sheet, rowIndex, columnIndex, True)
            Next
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
                'sheet.Cells(titleRow, startCol).Locked = True
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
            'sheet.Cells(titleRow, startCol + HALF_MONTH_DEFAULT_COUNT).Locked = True
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
                'sheet.Cells(titleRow, startCol).Locked = True
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

            'If Not sheet.Equals(sheet) Then
            '    '月分布フッター太線なし
            '    sheet.Cells(sheet.RowCount - 1, startCol).Border = New FarPoint.Win.ComplexBorder( _
            '                                       New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
            '                                       New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
            '                                       New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
            '                                       New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None))
            'End If
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
                'sheet.Cells(titleRow, columnIndex + 1).Locked = True
                sheet.Cells(titleRow, columnIndex + 1).BackColor = Color.Khaki
                sheet.Cells(titleRow, columnIndex + 1).Value = "月分布"
                sheet.Cells(titleRow, columnIndex + 1).VerticalAlignment = CellVerticalAlignment.Center
            End If
        End Sub
#End Region
#End Region

#Region "部品表シート作成"

        ''' <summary>
        ''' 部品表シート作成
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CreateSheetBuhinEdit()
            '作成可能か判断'
            Dim yosanBuhinEditVos As New List(Of TYosanBuhinEditVo)
            Dim yosanBuhinEditInsuVos As New List(Of TYosanBuhinEditInsuVo)
            Dim yosanBuhinEditPatternVos As New List(Of TYosanBuhinEditPatternVo)
            Dim instlVosByHyojijun As New Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo))
            Dim yosanEventListExcelDao As New YosanEventListExcelDaoImpl

            '予算部品表選択情報を取得する。
            Dim _BuhinhyoVos As List(Of TYosanBuhinSelectVo) = YosanBuhinhyoList(yosanEventCode)

            '予算部品表選択情報分の部品表を出力する。
            Dim yosanUnitKbn As String

            For Each vo As TYosanBuhinSelectVo In _BuhinhyoVos
                yosanUnitKbn = vo.BuhinhyoName
                yosanBuhinEditVos = yosanEventListExcelDao.FindYosanBuhinEdit(yosanEventCode, yosanUnitKbn)
                yosanBuhinEditInsuVos = yosanEventListExcelDao.FindYosanBuhinEditInsu(yosanEventCode, yosanUnitKbn)
                '予算部品編集情報を取得できた場合
                If 0 < yosanBuhinEditVos.Count Then
                    '予算部品編集パターン情報取得
                    yosanBuhinEditPatternVos = yosanEventListExcelDao.FindYosanBuhinEditPattern(yosanEventCode, yosanUnitKbn)
                    '予算部品編集員数情報取得
                    instlVosByHyojijun = MakeInstlVosByHyojijun(yosanBuhinEditVos, yosanBuhinEditInsuVos)
                    'sheet3
                    setYosanBuhinSheet(yosanEventCode, yosanBuhinEditVos, instlVosByHyojijun, yosanBuhinEditPatternVos, yosanUnitKbn)
                End If
            Next
        End Sub


#Region "Sheet部品表名 "
        ''' <summary>
        ''' Excel出力　部品表
        ''' </summary>
        ''' <param name="yosanBuhinEditVos">部品表</param>
        ''' <param name="yosanUnitKbn">部品表名</param>
        ''' <remarks></remarks>
        Private Sub setYosanBuhinSheet(ByVal eventCode As String, _
                                       ByVal yosanBuhinEditVos As List(Of TYosanBuhinEditVo), _
                                       ByVal instlVosByHyojijun As Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo)), _
                                       ByVal yosanBuhinEditPatternVos As List(Of TYosanBuhinEditPatternVo), _
                                       ByVal yosanUnitKbn As String)
            'Sheet名を設定
            '新しくシート作成する。'
            spread.Sheets.Add(New FarPoint.Win.Spread.SheetView)
            activeSheetIndex = activeSheetIndex + 1

            spread.ActiveSheetIndex = activeSheetIndex
            spread.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)  '太文字に
            sheet = spread.ActiveSheet
            sheet.SheetName() = "部品表_" & eventCode & "_" & yosanUnitKbn & "部品表"
            sheet.ReferenceStyle = Model.ReferenceStyle.R1C1

            '出力 部品表各カラム名に数値を設定
            SetYosanBuhinColumnNo(yosanBuhinEditPatternVos)
            '出力 部品表情報を設定
            setYosanBuhinSheetBody(yosanBuhinEditVos, instlVosByHyojijun, yosanBuhinEditPatternVos)

        End Sub

#Region "ここでしか使用しない定数"
        'sheet3/sheet4
        '設計課
        Private COLUMN_BUKA_CODE As Integer
        'ブロック
        Private COLUMN_BLOCK_NO As Integer
        'レベル
        Private COLUMN_LEVEL As Integer
        '国内集計
        Private COLUMN_SHUKEI_CODE As Integer
        '海外集計
        Private COLUMN_SIA_SHUKEI_CODE As Integer
        '取引先コード
        Private COLUMN_MAKER_CODE As Integer
        '取引先名称
        Private COLUMN_MAKER_NAME As Integer
        '部品番号
        Private COLUMN_BUHIN_NO As Integer
        '部品番号試作区分
        Private COLUMN_BUHIN_NO_KBN As Integer
        '部品名称
        Private COLUMN_BUHIN_NAME As Integer
        '供給セクション
        Private COLUMN_KYOUKU_SECTION As Integer
        '員数
        Private COLUMN_INSU As Integer
        '変更概要
        Private COLUMN_HENKO_GAIYO As Integer
        '部品費（量産）
        Private COLUMN_BUHIN_HI_RYOSAN As Integer
        '部品費（部品表）
        Private COLUMN_BUHIN_HI_BUHINHYO As Integer
        '部品費（特記）
        Private COLUMN_BUHIN_HI_TOKKI As Integer
        '型費
        Private COLUMN_KATA_HI As Integer
        '治具費
        Private COLUMN_JIGU_HI As Integer
        '工数
        Private COLUMN_KOSU As Integer
        '発注実績(割付&MIX値全体と平均値)
        Private COLUMN_HACHU_JISEKI_MIX As Integer


        '部品費MIX値
        Private COLUMN_BUHINHI_MIX As Integer
        '型費MIX値
        Private COLUMN_KATAHI_MIX As Integer

        '部品費MIX*員数
        Private COLUMN_BUHIN_INSU_MIX As Integer

        '型費MIX*員数
        Private COLUMN_KATA_INSU_MIX As Integer


#Region "行位置"
        Private Const BUHIN_DATA_ROW As Integer = 2
        Private Const BUHIN_TITLE_ROW As Integer = 1
#End Region


#End Region

        ''' <summary>
        ''' 各カラム名に数値を設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetYosanBuhinColumnNo(ByVal yosanBuhinEditPatternVos As List(Of TYosanBuhinEditPatternVo))
            Dim column As Integer = 0
            Dim count As Integer = yosanBuhinEditPatternVos.Count - 1

            '設計課
            COLUMN_BUKA_CODE = EzUtil.Increment(column)
            'ブロック
            COLUMN_BLOCK_NO = EzUtil.Increment(column)
            'レベル
            COLUMN_LEVEL = EzUtil.Increment(column)
            '国内集計
            COLUMN_SHUKEI_CODE = EzUtil.Increment(column)
            '海外集計
            COLUMN_SIA_SHUKEI_CODE = EzUtil.Increment(column)
            '取引先コード
            COLUMN_MAKER_CODE = EzUtil.Increment(column)
            '取引先名称
            COLUMN_MAKER_NAME = EzUtil.Increment(column)
            '部品番号
            COLUMN_BUHIN_NO = EzUtil.Increment(column)
            '部品番号試作区分
            COLUMN_BUHIN_NO_KBN = EzUtil.Increment(column)
            '部品名称
            COLUMN_BUHIN_NAME = EzUtil.Increment(column)
            '供給セクション
            COLUMN_KYOUKU_SECTION = EzUtil.Increment(column)
            '員数
            COLUMN_INSU = EzUtil.Increment(column)
            '変更概要
            COLUMN_HENKO_GAIYO = EzUtil.Increment(column) + count
            '部品費（量産）
            COLUMN_BUHIN_HI_RYOSAN = EzUtil.Increment(column) + count
            '部品費（部品表）
            COLUMN_BUHIN_HI_BUHINHYO = EzUtil.Increment(column) + count
            '部品費（特記）
            COLUMN_BUHIN_HI_TOKKI = EzUtil.Increment(column) + count
            '型費
            COLUMN_KATA_HI = EzUtil.Increment(column) + count
            '治具費
            COLUMN_JIGU_HI = EzUtil.Increment(column) + count
            '工数
            COLUMN_KOSU = EzUtil.Increment(column) + count
            '発注実績(割付&MIX値全体と平均値)
            COLUMN_HACHU_JISEKI_MIX = EzUtil.Increment(column) + count

            '部品費MIX値
            COLUMN_BUHINHI_MIX = EzUtil.Increment(column) + count
            '型費MIX値
            COLUMN_KATAHI_MIX = EzUtil.Increment(column) + count

            '部品費MIX値と員数の乗算'
            'COLUMN_BUHIN_INSU_MIX = EzUtil.Increment(column) + count
            '型費MIX値'


        End Sub

        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="yosanBuhinEditVos">予算書イベント情報</param>
        ''' <param name="instlVosByHyojijun"></param>
        ''' <param name="yosanBuhinEditPatternVos"></param>
        ''' <remarks></remarks>
        Public Sub setYosanBuhinSheetBody(ByVal yosanBuhinEditVos As List(Of TYosanBuhinEditVo), ByVal instlVosByHyojijun As Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo)), ByVal yosanBuhinEditPatternVos As List(Of TYosanBuhinEditPatternVo))

            'タイトル部分の作成'
            setYosanBuhinTitleRow()

            Dim rowCount As Integer = yosanBuhinEditVos.Count
            sheet.RowCount = rowCount + 10
            sheet.Rows(BUHIN_TITLE_ROW).Height = 200

            If rowCount <= 0 Then
                '員数ヘッダー
                sheet.Cells(COLUMN_INSU, BUHIN_TITLE_ROW - 1).Value = "員数"
                sheet.Cells(COLUMN_INSU, BUHIN_TITLE_ROW - 1).VerticalAlignment = CellVerticalAlignment.Center
                sheet.Cells(COLUMN_INSU, BUHIN_TITLE_ROW - 1).HorizontalAlignment = CellHorizontalAlignment.Center
                sheet.Cells(COLUMN_INSU, BUHIN_TITLE_ROW - 1).BackColor = Color.LightGray


                'xls.SetLine(COLUMN_INSU, BUHIN_TITLE_ROW - 1, COLUMN_INSU, BUHIN_TITLE_ROW - 1)
                ''列の幅を自動調整する
                'xls.AutoFitCol(COLUMN_BUKA_CODE, COLUMN_HACHU_JISEKI_MIX)

                Exit Sub
            End If

            Dim excelRowIndex As Integer = 0
            For index = 0 To rowCount - 1
                Dim tempyosanBuhinEditVo As TYosanBuhinEditVo = yosanBuhinEditVos(index)
                excelRowIndex = BUHIN_DATA_ROW + index
                '設計課'
                sheet.Cells(excelRowIndex, COLUMN_BUKA_CODE).Value = tempyosanBuhinEditVo.YosanBukaCode
                'ブロック'
                sheet.Cells(excelRowIndex, COLUMN_BLOCK_NO).Value = tempyosanBuhinEditVo.YosanBlockNo
                'レベル'
                sheet.Cells(excelRowIndex, COLUMN_LEVEL).Value = tempyosanBuhinEditVo.YosanLevel
                '国内集計
                sheet.Cells(excelRowIndex, COLUMN_SHUKEI_CODE).Value = tempyosanBuhinEditVo.YosanShukeiCode
                '海外集計
                sheet.Cells(excelRowIndex, COLUMN_SIA_SHUKEI_CODE).Value = tempyosanBuhinEditVo.YosanSiaShukeiCode
                '取引先コード
                sheet.Cells(excelRowIndex, COLUMN_MAKER_CODE).Value = tempyosanBuhinEditVo.YosanMakerCode
                '取引先名称
                sheet.Cells(excelRowIndex, COLUMN_MAKER_NAME).Value = tempyosanBuhinEditVo.YosanMakerName
                '部品番号'
                sheet.Cells(excelRowIndex, COLUMN_BUHIN_NO).Value = tempyosanBuhinEditVo.YosanBuhinNo
                '部品番号試作区分
                sheet.Cells(excelRowIndex, COLUMN_BUHIN_NO_KBN).Value = tempyosanBuhinEditVo.YosanBuhinNoKbn
                '部品名称
                sheet.Cells(excelRowIndex, COLUMN_BUHIN_NAME).Value = tempyosanBuhinEditVo.YosanBuhinName
                '供給セクション
                sheet.Cells(excelRowIndex, COLUMN_KYOUKU_SECTION).Value = tempyosanBuhinEditVo.YosanKyoukuSection
                '員数'
                sheet.Cells(excelRowIndex, COLUMN_INSU).Value = tempyosanBuhinEditVo.YosanInsu
                '変更概要
                sheet.Cells(excelRowIndex, COLUMN_HENKO_GAIYO).Value = tempyosanBuhinEditVo.YosanHenkoGaiyo
                '部品費（量産）'
                sheet.Cells(excelRowIndex, COLUMN_BUHIN_HI_RYOSAN).Value = tempyosanBuhinEditVo.YosanBuhinHiRyosan
                '部品費（部品表）'
                sheet.Cells(excelRowIndex, COLUMN_BUHIN_HI_BUHINHYO).Value = tempyosanBuhinEditVo.YosanBuhinHiBuhinhyo
                '部品費（特記）'
                sheet.Cells(excelRowIndex, COLUMN_BUHIN_HI_TOKKI).Value = tempyosanBuhinEditVo.YosanBuhinHiTokki
                '型費'
                sheet.Cells(excelRowIndex, COLUMN_KATA_HI).Value = tempyosanBuhinEditVo.YosanKataHi
                '治具費'
                sheet.Cells(excelRowIndex, COLUMN_JIGU_HI).Value = tempyosanBuhinEditVo.YosanJiguHi
                '工数'
                sheet.Cells(excelRowIndex, COLUMN_KOSU).Value = tempyosanBuhinEditVo.YosanKosu
                '発注実績(割付&MIX値全体と平均値)
                sheet.Cells(excelRowIndex, COLUMN_HACHU_JISEKI_MIX).Value = tempyosanBuhinEditVo.YosanHachuJisekiMix
                '員数'
                For insuIdx As Integer = 0 To instlVosByHyojijun.Item(tempyosanBuhinEditVo.BuhinNoHyoujiJun).Count - 1
                    Dim hyoujiJun As Integer = instlVosByHyojijun.Item(tempyosanBuhinEditVo.BuhinNoHyoujiJun)(insuIdx).PatternHyoujiJun
                    If instlVosByHyojijun.Item(tempyosanBuhinEditVo.BuhinNoHyoujiJun)(insuIdx).InsuSuryo = -1 Then
                        sheet.Cells(excelRowIndex, COLUMN_INSU + hyoujiJun).Value = "**"
                    Else
                        sheet.Cells(excelRowIndex, COLUMN_INSU + hyoujiJun).Value = instlVosByHyojijun.Item(tempyosanBuhinEditVo.BuhinNoHyoujiJun)(insuIdx).InsuSuryo
                    End If
                Next

                '2016/02/26'
                'MIX値を関数で出力'
                '特記最優先で量産か部品表のどちらか大きい方が出る'
                '=IF(R5C17>0,R5C17,IF(R5C16>R5C15,R5C16,R5C15))'
                sheet.Cells(excelRowIndex, COLUMN_BUHINHI_MIX).Formula = _
                "IF(R" & excelRowIndex + 1 & "C" & COLUMN_BUHIN_HI_TOKKI + 1 & ">0," & _
                "R" & excelRowIndex + 1 & "C" & COLUMN_BUHIN_HI_TOKKI + 1 & "," & _
                "IF(R" & excelRowIndex + 1 & "C" & COLUMN_BUHIN_HI_RYOSAN + 1 & ">" & "R" & excelRowIndex + 1 & "C" & COLUMN_BUHIN_HI_BUHINHYO + 1 & "," & _
                "R" & excelRowIndex + 1 & "C" & COLUMN_BUHIN_HI_RYOSAN + 1 & "," & _
                "R" & excelRowIndex + 1 & "C" & COLUMN_BUHIN_HI_BUHINHYO + 1 & "))"
                '型費+治具'
                sheet.Cells(excelRowIndex, COLUMN_KATAHI_MIX).Formula = _
                "R" & excelRowIndex + 1 & "C" & COLUMN_KATA_HI + 1 & "+" & _
                "R" & excelRowIndex + 1 & "C" & COLUMN_JIGU_HI + 1 & ""

                ''上のMIX値結果*員数(**は計算しない。)'
                ''=IF(R3C12="**",0,R3C22*R3C12)'
                'For insuIdx As Integer = 0 To instlVosByHyojijun.Item(tempyosanBuhinEditVo.BuhinNoHyoujiJun).Count - 1
                '    Dim hyoujiJun As Integer = instlVosByHyojijun.Item(tempyosanBuhinEditVo.BuhinNoHyoujiJun)(insuIdx).PatternHyoujiJun
                '    sheet.Cells(excelRowIndex, COLUMN_BUHIN_INSU_MIX + hyoujiJun).Formula = _
                '    "IF(R" & excelRowIndex + 1 & "C" & COLUMN_INSU + hyoujiJun + 1 & "=""**""," & _
                '    "0," & _
                '    "R" & excelRowIndex + 1 & "C" & COLUMN_BUHINHI_MIX + 1 & "*" & _
                '    "R" & excelRowIndex + 1 & "C" & COLUMN_INSU + hyoujiJun + 1 & ")"
                'Next


            Next
            Dim patternCount As Integer = yosanBuhinEditPatternVos.Count
            Dim endInsuColumn As Integer = COLUMN_INSU + patternCount - 1
            'パターン名ヘッダー
            For patternIdx As Integer = 0 To patternCount - 1
                sheet.Cells(BUHIN_TITLE_ROW, COLUMN_INSU + patternIdx).Value = yosanBuhinEditPatternVos(patternIdx).PatternName
            Next


            Dim cellType As New TextCellType
            cellType.TextOrientation = TextOrientation.TextTopDown
            For i As Integer = COLUMN_INSU To endInsuColumn
                sheet.Cells(BUHIN_TITLE_ROW, i).CellType = cellType
                sheet.Cells(BUHIN_TITLE_ROW, i).VerticalAlignment = CellVerticalAlignment.Center
                sheet.Cells(BUHIN_TITLE_ROW, i).HorizontalAlignment = CellHorizontalAlignment.Center
                sheet.Columns(i).Width = 35
            Next

            '員数ヘッダー
            'sheet.Cells(BUHIN_TITLE_ROW - 1, COLUMN_INSU).CellType = cellType
            sheet.Cells(BUHIN_TITLE_ROW - 1, COLUMN_INSU).Value = "員数"
            sheet.Cells(BUHIN_TITLE_ROW - 1, COLUMN_INSU).VerticalAlignment = CellVerticalAlignment.Center
            sheet.Cells(BUHIN_TITLE_ROW - 1, COLUMN_INSU).HorizontalAlignment = CellHorizontalAlignment.Center
            sheet.Cells(BUHIN_TITLE_ROW - 1, COLUMN_INSU).BackColor = Color.LightGray
            sheet.Cells(BUHIN_TITLE_ROW - 1, COLUMN_INSU).ColumnSpan = endInsuColumn - COLUMN_INSU + 1


            Dim cbs As New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine, Color.Black)

            Dim cbs2 As New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.Dashed, Color.Black)
            Dim cBorder As New FarPoint.Win.ComplexBorder(cbs, cbs, cbs, cbs)
            Dim cBorder2 As New FarPoint.Win.ComplexBorder(cbs, cbs2, cbs, cbs2)


            Dim cr As New FarPoint.Win.Spread.Model.CellRange(1, 0, excelRowIndex, COLUMN_KATAHI_MIX + 1)
            spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
            spread.ActiveSheet.SetInsideBorder(cr, cBorder2)

            'xls.SetLine(COLUMN_INSU, BUHIN_TITLE_ROW - 1, endInsuColumn, BUHIN_TITLE_ROW - 1)
            'xls.MergeCells(COLUMN_INSU, BUHIN_TITLE_ROW - 1, endInsuColumn, BUHIN_TITLE_ROW - 1, True)

            '表に加線
            'xls.SetLine(COLUMN_BUKA_CODE, BUHIN_DATA_ROW, COLUMN_HACHU_JISEKI_MIX, rowCount + BUHIN_DATA_ROW - 1)
            'xls.SetLine(COLUMN_BUKA_CODE, BUHIN_DATA_ROW, COLUMN_HACHU_JISEKI_MIX, rowCount + BUHIN_DATA_ROW - 1, XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlDot, XlBorderWeight.xlThin)
            ''設置テキストに表示
            'xls.SetAlignment(COLUMN_LEVEL, BUHIN_DATA_ROW, COLUMN_LEVEL, rowCount + BUHIN_DATA_ROW - 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            'xls.SetAlignment(COLUMN_INSU, BUHIN_DATA_ROW, endInsuColumn, rowCount + BUHIN_DATA_ROW - 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            'xls.SetAlignment(COLUMN_BUHIN_HI_RYOSAN, BUHIN_DATA_ROW, COLUMN_HACHU_JISEKI_MIX, rowCount + BUHIN_DATA_ROW - 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            ''#,##0
            'xls.SetNumberFormatLocal(COLUMN_BUHIN_HI_RYOSAN, BUHIN_DATA_ROW, COLUMN_HACHU_JISEKI_MIX, rowCount + BUHIN_DATA_ROW - 1, "#,##0;@")

            '列の幅を自動調整する
            'xls.AutoFitCol(COLUMN_BUKA_CODE, COLUMN_HACHU_JISEKI_MIX)
            '員数列幅
            'xls.SetColWidth(COLUMN_INSU, endInsuColumn, 4)
        End Sub

        ''' <summary>
        ''' タイトル行の作成
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub setYosanBuhinTitleRow()

            Dim cellType As New TextCellType
            cellType.TextOrientation = TextOrientation.TextTopDown

            For i As Integer = COLUMN_BUKA_CODE To COLUMN_MAKER_CODE
                sheet.Cells(BUHIN_TITLE_ROW, i).CellType = cellType
                sheet.Cells(BUHIN_TITLE_ROW, i).HorizontalAlignment = CellHorizontalAlignment.Center
                sheet.Cells(BUHIN_TITLE_ROW, i).VerticalAlignment = CellVerticalAlignment.Center
                'sheet.Cells(BUHIN_TITLE_ROW, i).BackColor = Color.LightGray
            Next

            For i As Integer = COLUMN_BUKA_CODE To COLUMN_KATAHI_MIX
                sheet.Cells(BUHIN_TITLE_ROW, i).HorizontalAlignment = CellHorizontalAlignment.Center
                sheet.Cells(BUHIN_TITLE_ROW, i).VerticalAlignment = CellVerticalAlignment.Center
                sheet.Cells(BUHIN_TITLE_ROW, i).BackColor = Color.LightGray
            Next

            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_BUKA_CODE).Value = "設計課"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_BLOCK_NO).Value = "ブロック"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_LEVEL).Value = "レベル"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_SHUKEI_CODE).Value = "国内集計"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_SIA_SHUKEI_CODE).Value = "海外集計"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_MAKER_CODE).Value = "取引先コード"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_MAKER_NAME).Value = "取引先名称"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_BUHIN_NO).Value = "部品番号"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_BUHIN_NO_KBN).Value = "部品番号試作区分"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_BUHIN_NAME).Value = "部品名称"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_KYOUKU_SECTION).Value = "供給セクション"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_INSU).Value = "員数"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_HENKO_GAIYO).Value = "変更概要"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_BUHIN_HI_RYOSAN).Value = "部品費（量産）"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_BUHIN_HI_BUHINHYO).Value = "部品費（部品表）"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_BUHIN_HI_TOKKI).Value = "部品費（特記）"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_KATA_HI).Value = "型費"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_JIGU_HI).Value = "治具費"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_KOSU).Value = "工数"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_HACHU_JISEKI_MIX).Value = "発注実績(割付&MIX値全体と平均値)"

            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_BUHINHI_MIX).Value = "部品費MIX値"
            sheet.Cells(BUHIN_TITLE_ROW, COLUMN_KATAHI_MIX).Value = "型費MIX値"


            '設計課～取引先名称ヘッダー
            'xls.SetOrientation(COLUMN_BUKA_CODE, BUHIN_TITLE_ROW, COLUMN_MAKER_CODE, BUHIN_TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            'xls.SetBackColor(COLUMN_BUKA_CODE, BUHIN_TITLE_ROW, COLUMN_HACHU_JISEKI_MIX, 2, RGB(192, 192, 192))
            'xls.SetLine(COLUMN_BUKA_CODE, BUHIN_TITLE_ROW, COLUMN_HACHU_JISEKI_MIX, BUHIN_TITLE_ROW)
        End Sub

#End Region

        ''' <summary>
        ''' 表示順NoによってINSTL情報を返すDictionaryを作成する
        ''' </summary>
        ''' <param name="editInstlVos">試作部品編集INSTL情報</param>
        ''' <returns>Dictionary</returns>
        ''' <remarks></remarks>
        Private Shared Function MakeInstlVosByHyojijun(ByVal editVos As List(Of TYosanBuhinEditVo), ByVal editInstlVos As List(Of TYosanBuhinEditInsuVo)) As Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo))

            Dim instlVosByHyojijun As New Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo))
            For Each vo As TYosanBuhinEditInsuVo In editInstlVos
                If Not instlVosByHyojijun.ContainsKey(vo.BuhinNoHyoujiJun) Then
                    instlVosByHyojijun.Add(vo.BuhinNoHyoujiJun, New List(Of TYosanBuhinEditInsuVo))
                End If
                instlVosByHyojijun(vo.BuhinNoHyoujiJun).Add(vo)
            Next
            Return instlVosByHyojijun
        End Function

#End Region

#Region "イベントシートの作成"

#Region "ここでしか使用しない変数"
        'sheet1
        '区分
        Private COLUMN_KBN_NAME As Integer
        '開発符号
        Private COLUMN_KAIHATSU_FUGO As Integer
        'イベント
        Private COLUMN_EVENT As Integer
        'イベント名称
        Private COLUMN_EVENT_NAME As Integer
        '予算コード
        Private COLUMN_CODE As Integer
        '予算期間FROM
        Private COLUMN_KIKAN_FROM As Integer
        '予算期間記号
        Private COLUMN_KIKAN_SYMBOL As Integer
        '予算期間TO
        Private COLUMN_KIKAN_TO As Integer
        '予算製作台数・完成車
        Private COLUMN_SEISAKUDAISU_KANSEISYA As Integer
        '予算製作台数記号
        Private COLUMN_SEISAKUDAISU_SYMBOL As Integer
        '予算製作台数・WB車
        Private COLUMN_SEISAKUDAISU_WB As Integer
        '主な変更概要
        Private COLUMN_MAIN_HENKO_GAIYO As Integer
        '造り方及び製作条件
        Private COLUMN_TSUKURIKATA_SEISAKUJYOKEN As Integer
        'その他
        Private COLUMN_SONOTA As Integer
        '予算イベントコード
        Private COLUMN_EVENT_CODE As Integer

        Private Const EVENT_TITLE_ROW As Integer = 1
        Private Const EVENT_DATA_ROW As Integer = 2

#End Region

        ''' <summary>
        ''' イベントシート作成
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CreateEventSheet(ByVal eventVos As List(Of TYosanEventVo))
            setYosanEventColumnNo()
            'タイトル部分の作成'
            setYosanEventTitleRow()
            'データ部分の作成'
            setYosanEventSheetBody(eventVos)
        End Sub

        ''' <summary>
        ''' 各カラム名に数値を設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub setYosanEventColumnNo()
            Dim column As Integer = 0

            COLUMN_KBN_NAME = EzUtil.Increment(column)
            COLUMN_KAIHATSU_FUGO = EzUtil.Increment(column)
            COLUMN_EVENT = EzUtil.Increment(column)
            COLUMN_EVENT_NAME = EzUtil.Increment(column)
            COLUMN_CODE = EzUtil.Increment(column)
            COLUMN_KIKAN_FROM = EzUtil.Increment(column)
            COLUMN_KIKAN_SYMBOL = EzUtil.Increment(column)
            COLUMN_KIKAN_TO = EzUtil.Increment(column)
            COLUMN_SEISAKUDAISU_KANSEISYA = EzUtil.Increment(column)
            COLUMN_SEISAKUDAISU_SYMBOL = EzUtil.Increment(column)
            COLUMN_SEISAKUDAISU_WB = EzUtil.Increment(column)
            COLUMN_MAIN_HENKO_GAIYO = EzUtil.Increment(column)
            COLUMN_TSUKURIKATA_SEISAKUJYOKEN = EzUtil.Increment(column)
            COLUMN_SONOTA = EzUtil.Increment(column)
            COLUMN_EVENT_CODE = EzUtil.Increment(column)

        End Sub

        ''' <summary>
        ''' タイトル行の作成
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub setYosanEventTitleRow()
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_KBN_NAME).Value = "区分"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_KAIHATSU_FUGO).Value = "開発符号"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_EVENT).Value = "イベント"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_EVENT_NAME).Value = "イベント名称"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_CODE).Value = "予算コード"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_KIKAN_FROM).Value = "予算期間FROM"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_KIKAN_SYMBOL).Value = "予算期間記号"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_KIKAN_TO).Value = "予算期間TO"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_SEISAKUDAISU_KANSEISYA).Value = "予算製作台数・完成車"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_SEISAKUDAISU_SYMBOL).Value = "予算製作台数記号"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_SEISAKUDAISU_WB).Value = "予算製作台数・WB車"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_MAIN_HENKO_GAIYO).Value = "主な変更概要"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_TSUKURIKATA_SEISAKUJYOKEN).Value = "造り方及び製作条件"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_SONOTA).Value = "その他"
            sheet.Cells(EVENT_TITLE_ROW, COLUMN_EVENT_CODE).Value = "予算イベントコード"

        End Sub

        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub setYosanEventSheetBody(ByVal eventVos As List(Of TYosanEventVo))

            Dim dataRow As Integer = EVENT_DATA_ROW
            Dim eventDao As TYosanEventDao = New TYosanEventDaoImpl
            Dim yosanEvent As TYosanEventVo


            For Each Vo As TYosanEventVo In eventVos
                yosanEvent = eventDao.FindByPk(Vo.YosanEventCode)
                '区分'
                sheet.Cells(dataRow, COLUMN_KBN_NAME).Value = yosanEvent.YosanEventKbnName
                '開発符号'
                sheet.Cells(dataRow, COLUMN_KAIHATSU_FUGO).Value = yosanEvent.YosanKaihatsuFugo
                'イベント'
                sheet.Cells(dataRow, COLUMN_EVENT).Value = yosanEvent.YosanEvent
                'イベント名称'
                sheet.Cells(dataRow, COLUMN_EVENT_NAME).Value = yosanEvent.YosanEventName
                '予算コード'
                sheet.Cells(dataRow, COLUMN_CODE).Value = yosanEvent.YosanCode
                '予算期間FROM'
                sheet.Cells(dataRow, COLUMN_KIKAN_FROM).Value = yosanEvent.YosanKikanFromYyyy
                '予算期間記号'
                sheet.Cells(dataRow, COLUMN_KIKAN_SYMBOL).Value = "～"
                '予算期間TO'
                sheet.Cells(dataRow, COLUMN_KIKAN_TO).Value = yosanEvent.YosanKikanToYyyy
                '予算製作台数・完成車'
                sheet.Cells(dataRow, COLUMN_SEISAKUDAISU_KANSEISYA).Value = yosanEvent.YosanSeisakudaisuKanseisya
                '予算製作台数記号'
                If StringUtil.IsNotEmpty(yosanEvent.YosanSeisakudaisuKanseisya) Or StringUtil.IsNotEmpty(yosanEvent.YosanSeisakudaisuWb) Then
                    sheet.Cells(dataRow, COLUMN_SEISAKUDAISU_SYMBOL).Value = "+"
                End If
                '予算製作台数・WB車'
                sheet.Cells(dataRow, COLUMN_SEISAKUDAISU_WB).Value = yosanEvent.YosanSeisakudaisuWb
                '主な変更概要'
                sheet.Cells(dataRow, COLUMN_MAIN_HENKO_GAIYO).Value = yosanEvent.YosanMainHenkoGaiyo
                '造り方及び製作条件
                sheet.Cells(dataRow, COLUMN_TSUKURIKATA_SEISAKUJYOKEN).Value = yosanEvent.YosanTsukurikataSeisakujyoken
                'その他'
                sheet.Cells(dataRow, COLUMN_SONOTA).Value = yosanEvent.YosanSonota
                '予算イベントコード'
                sheet.Cells(dataRow, COLUMN_EVENT_CODE).Value = yosanEvent.YosanEventCode
                '
                dataRow += 1
            Next

            'If StringUtil.IsNotEmpty(_tEventVos) Then

            'Else
            '    yosanEvent = eventDao.FindByPk(_eventCode)
            '    '区分'
            '    xls.SetValue(COLUMN_KBN_NAME, dataRow, yosanEvent.YosanEventKbnName)
            '    '開発符号'
            '    xls.SetValue(COLUMN_KAIHATSU_FUGO, dataRow, yosanEvent.YosanKaihatsuFugo)
            '    'イベント'
            '    xls.SetValue(COLUMN_EVENT, dataRow, yosanEvent.YosanEvent)
            '    'イベント名称'
            '    xls.SetValue(COLUMN_EVENT_NAME, dataRow, yosanEvent.YosanEventName)
            '    '予算コード'
            '    xls.SetValue(COLUMN_CODE, dataRow, yosanEvent.YosanCode)
            '    '予算期間FROM'
            '    xls.SetValue(COLUMN_KIKAN_FROM, dataRow, yosanEvent.YosanKikanFromYyyy)
            '    '予算期間記号'
            '    xls.SetValue(COLUMN_KIKAN_SYMBOL, dataRow, "～")
            '    '予算期間TO'
            '    xls.SetValue(COLUMN_KIKAN_TO, dataRow, yosanEvent.YosanKikanToYyyy)
            '    '予算製作台数・完成車'
            '    xls.SetValue(COLUMN_SEISAKUDAISU_KANSEISYA, dataRow, yosanEvent.YosanSeisakudaisuKanseisya)
            '    '予算製作台数記号'
            '    If StringUtil.IsNotEmpty(yosanEvent.YosanSeisakudaisuKanseisya) Or StringUtil.IsNotEmpty(yosanEvent.YosanSeisakudaisuWb) Then
            '        xls.SetValue(COLUMN_SEISAKUDAISU_SYMBOL, dataRow, "+")
            '    End If
            '    '予算製作台数・WB車'
            '    xls.SetValue(COLUMN_SEISAKUDAISU_WB, dataRow, yosanEvent.YosanSeisakudaisuWb)
            '    '主な変更概要'
            '    xls.SetValue(COLUMN_MAIN_HENKO_GAIYO, dataRow, yosanEvent.YosanMainHenkoGaiyo)
            '    '造り方及び製作条件
            '    xls.SetValue(COLUMN_TSUKURIKATA_SEISAKUJYOKEN, dataRow, yosanEvent.YosanTsukurikataSeisakujyoken)
            '    'その他'
            '    xls.SetValue(COLUMN_SONOTA, dataRow, yosanEvent.YosanSonota)
            '    '予算イベントコード'
            '    xls.SetValue(COLUMN_EVENT_CODE, dataRow, yosanEvent.YosanEventCode)
            'End If

            sheet.Cells(1, COLUMN_KIKAN_SYMBOL).HorizontalAlignment = CellHorizontalAlignment.Center
            sheet.Cells(1, COLUMN_KIKAN_SYMBOL).VerticalAlignment = CellVerticalAlignment.Center
            sheet.Columns(COLUMN_KIKAN_SYMBOL).Width = 20
            '設置テキストフォーマット
            'xls.SetAlignment(COLUMN_KIKAN_SYMBOL, 1, COLUMN_KIKAN_SYMBOL, 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            '設置レイヤーの幅
            'xls.SetColWidth(COLUMN_KIKAN_SYMBOL, 1, 10)
            '設置テキストフォーマット
            'xls.SetAlignment(COLUMN_SEISAKUDAISU_KANSEISYA, 1, COLUMN_SEISAKUDAISU_KANSEISYA, 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            'レイヤーの幅を設定
            'xls.SetColWidth(COLUMN_SEISAKUDAISU_KANSEISYA, 1, 20)
            '自動幅を設定
            'xls.AutoFitCol(COLUMN_KBN_NAME, xls.EndCol())

        End Sub

#End Region

#Region "年期からリスト作成"

        Private Function CreateYearMonthList(ByVal minYearKs As String, ByVal maxYearKs As String) As List(Of String)
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

            Return yearMonths

        End Function

#End Region


#Region "集計表シートの作成"

        ''' <summary>
        ''' 集計表シートの作成
        ''' </summary>
        ''' <param name="eventVos"></param>
        ''' <remarks></remarks>
        Public Sub SetSumSheet(ByVal eventVos As List(Of TYosanEventVo))
            '集計表に設定'
            spread.ActiveSheetIndex = 1
            sheet = spread.ActiveSheet

            If Not yearMonthFormulaSumEvent.ContainsKey(sheet.SheetName) Then
                yearMonthFormulaSumEvent.Add(sheet.SheetName, New Dictionary(Of Integer, Dictionary(Of String, List(Of String))))

                For index As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX
                    yearMonthFormulaSumEvent(sheet.SheetName).Add(index, New Dictionary(Of String, List(Of String)))
                Next

            End If


            '初期データ表示スプレッド(製作台数)
            SeisakuDaisuSum()
            '初期データ表示スプレッド(造り方)
            TukurikataSum()
            ''初期データ表示スプレッド(金材)
            KanazaiSum()
            ''初期データ表示スプレッド(集計値)
            ZaimuJisekiSum()

        End Sub


#Region "製作台数(集計用)"

        ''' <summary>
        ''' 製作台数(集計用)
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SeisakuDaisuSum()
            '製作台数'

            Dim sameDic As New Dictionary(Of String, String)
            Dim hyoujiJun As Integer = 0
            For Each vo As TYosanSeisakuDaisuVo In yosanSeisakuDaisuSumList
                '表示順が重複しないようにする。'
                If IsKanseisha(vo.ShisakuSyubetu) Then
                    vo.KoujiShireiNoHyojijunNo = hyoujiJun
                    hyoujiJun = hyoujiJun + 1
                End If

            Next
            hyoujiJun = 0
            For Each vo As TYosanSeisakuDaisuVo In yosanSeisakuDaisuSumList
                '表示順が重複しないようにする。'
                If IsWb(vo.ShisakuSyubetu) Then
                    vo.KoujiShireiNoHyojijunNo = hyoujiJun
                    hyoujiJun = hyoujiJun + 1
                End If
            Next

            'タイトル部分の作成'
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = "≪製作台数≫"
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).RowSpan = 2
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).ColumnSpan = 2
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX + 2, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).Value = "台数"
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX + 2, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX + 1).BackColor = Color.FromArgb(204, 255, 255) ' RGB(204, 255, 255)

            sheet.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX + 1, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = "完成車"
            sheet.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX + 2, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX).Value = "ﾎﾜｲﾄﾎﾞﾃﾞｨ"

            '工事指令№を設定
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN).Value = "工事指令№"
            '工事指令№背景色
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN).BackColor = Color.FromArgb(255, 255, 153) 'RGB(255, 255, 153)


            _KanseiDaisuList = New List(Of TYosanSeisakuDaisuVo)
            _WbDaisuList = New List(Of TYosanSeisakuDaisuVo)

            Dim dataColumnCount As Integer = 0

            For Each daisuVo As TYosanSeisakuDaisuVo In yosanSeisakuDaisuSumList
                Dim colIndex As Integer = daisuVo.KoujiShireiNoHyojijunNo + SPD_DAISU_KOJISHIREI_NO_COLUMNS_START_COLUMN

                If colIndex >= sheet.ColumnCount - 1 Then
                    sheet.AddColumns(colIndex, 2)

                End If

                SetKojishireiColumnPro(colIndex)
                'ユニット区分
                sheet.Cells(SPD_DAISU_UNITKBN_ROW_INDEX, colIndex).Formula = daisuVo.UnitKbn
                '工事指令No
                sheet.Cells(SPD_DAISU_KOJISHIREI_DATA_ROW_INDEX, colIndex).Formula = daisuVo.KoujiShireiNo


                '台数
                If IsKanseisha(daisuVo.ShisakuSyubetu) Then
                    '完成車の場合
                    sheet.Cells(SPD_DAISU_KANSEISHA_ROW_INDEX, colIndex).Formula = daisuVo.CreatedUserId
                End If
                If IsWb(daisuVo.ShisakuSyubetu) Then
                    'WB車の場合
                    sheet.Cells(SPD_DAISU_WBSHA_ROW_INDEX, colIndex).Formula = daisuVo.CreatedUserId
                End If

                If dataColumnCount < colIndex Then
                    dataColumnCount = colIndex
                End If

            Next

            '４列未満の場合
            '向こう側で作成済み'
            'If _KanseiDaisuList.Count < SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT Then
            '    SetDefaultSeisakuDaisuList(yosanEventCode, SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT - _KanseiDaisuList.Count, _KanseiDaisuList, SHISAKU_SYUBETU_KANSEI)
            'End If
            'If _WbDaisuList.Count < SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT Then
            '    SetDefaultSeisakuDaisuList(yosanEventCode, SPD_DAISU_KOJISHIREI_DEFAULT_COLUMN_COUNT - _WbDaisuList.Count, _WbDaisuList, SHISAKU_SYUBETU_WB)
            'End If

            '台数合計
            SetSpreadSeisakuDaisuPro()
            CalcTaisu(dataColumnCount)
            '工事指令Noヘッダー
            sheet.Cells(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_KOTEI_COLUMN_COUNT).ColumnSpan = dataColumnCount - SPD_DAISU_KOTEI_COLUMN_COUNT + 1

            Dim cbs As New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine, Color.Black)
            Dim cBorder As New FarPoint.Win.ComplexBorder(cbs, cbs, cbs, cbs)

            '表に加線
            If dataColumnCount = 0 Then
                Dim cr As New FarPoint.Win.Spread.Model.CellRange(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX, 5, SPD_DAISU_KOTEI_COLUMN_COUNT)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            Else
                Dim cr As New FarPoint.Win.Spread.Model.CellRange(SPD_DAISU_KOJISHIREI_TITLE_ROW_INDEX, SPD_DAISU_DAISU_COUNT_COLUMN_INDEX, 5, dataColumnCount)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            End If
        End Sub

#End Region

#Region "造り方(集計用)"
        ''' <summary>
        ''' 造り方(集計用)
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub TukurikataSum()
            Dim dataStartRowIndex As Integer = SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX
            tukurikataDataColumnCount = 0
            tukurikataWBDataColumnCount = 0

            '完成車
            SPD_TUKURIKATA_MONTH_ROW_INDEX = dataStartRowIndex + 2
            _KanseiTukurikataList = DispSpreadTukurikataSum(SHISAKU_SYUBETU_KANSEI, dataStartRowIndex, tukurikataDataColumnCount)
            If dataStartRowIndex = SPD_TUKURIKATA_MONTH_TITLE_ROW_INDEX Then
                dataStartRowIndex = dataStartRowIndex + 5
            Else
                dataStartRowIndex = dataStartRowIndex + 2
            End If

            '２行あける'
            'ホワイトボディ
            SPD_TUKURIKATA_WB_MONTH_ROW_INDEX = dataStartRowIndex
            _WbTukurikataList = DispSpreadTukurikataSum(SHISAKU_SYUBETU_WB, dataStartRowIndex, tukurikataWBDataColumnCount)

            '金材の開始位置を設定'
            If dataStartRowIndex = SPD_TUKURIKATA_WB_MONTH_ROW_INDEX Then
                SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX = dataStartRowIndex + 5
            Else
                SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX = dataStartRowIndex + 2
            End If


            SPD_KANAZAI_ADD_BUTTON_CELL_ROW_INDEX = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX
            SPD_KANAZAI_MONTH_TITLE_ROW_INDEX = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX
            SPD_KANAZAI_YEAR_ROW_INDEX = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 1
            SPD_KANAZAI_MONTH_ROW_INDEX = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2
            SPD_KANAZAI_KANAZAINAME_ROWS_DATA_START_INDEX = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 3
        End Sub

        ''' <summary>
        ''' 完成車・ホワイトボディ
        ''' </summary>
        ''' <param name="shisakuSyubetu"></param>
        ''' <remarks></remarks>
        Private Function DispSpreadTukurikataSum(ByVal shisakuSyubetu As String, ByRef dataStartRowIndex As Integer, ByRef dataCount As Integer) As List(Of TYosanTukurikataVo)

            Dim bodyTitle As String = ""
            Dim monthColumnCount As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX
            Dim borderStartRow As Integer = dataStartRowIndex
            Dim dataRowCount As Integer = 3


            Dim vosByHyojijun As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo)))))

            If StringUtil.Equals(SHISAKU_SYUBETU_WB, shisakuSyubetu) Then
                bodyTitle = "ホワイトボディ"
                vosByHyojijun = MakeTukurikataVosByHyojijunSum(aTukurikataListWbSum)
            Else
                bodyTitle = "完成車"
                vosByHyojijun = MakeTukurikataVosByHyojijunSum(aTukurikataListKanseiSum)
            End If

            'タイトル部作成'
            sheet.Cells(dataStartRowIndex, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).Value = "月分布"

            sheet.Cells(dataStartRowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).Value = "≪造り方≫"
            sheet.Cells(dataStartRowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).RowSpan = 2
            sheet.Cells(dataStartRowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).ColumnSpan = 6

            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).Value = bodyTitle
            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).ColumnSpan = 3
            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).BackColor = Color.FromArgb(255, 255, 153)

            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Value = "部品費（円）"
            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Value = "型費（千円）"
            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).Value = "台数"

            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).BackColor = Color.FromArgb(204, 255, 255)
            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).BackColor = Color.FromArgb(204, 255, 255)
            sheet.Cells(dataStartRowIndex + 2, SPD_TUKURIKATA_DAISU_COLUMN_INDEX).BackColor = Color.FromArgb(204, 255, 255)


            If vosByHyojijun.Count > 0 Then

                Dim indexX As Integer = 0
                Dim ks As String = String.Empty
                Dim columnIndex As Integer = 0
                Dim rowIndex As Integer = dataStartRowIndex + 3
                Dim startRow As Integer

                '範囲をリスト化する'
                Dim yyyyMMList As New List(Of String)
                Dim yyyyMMColumnDic As New Dictionary(Of String, Integer)

                Dim minYearKsT As String = "9999年下期"
                Dim maxYearKsT As String = "0000年上期"

                For Each unitKbn As String In vosByHyojijun.Keys
                    For Each eventCode As String In vosByHyojijun(unitKbn).Keys
                        For Each buhinhyoName As String In vosByHyojijun(unitKbn)(eventCode).Keys
                            For Each patternName As String In vosByHyojijun(unitKbn)(eventCode)(buhinhyoName).Keys
                                For Each tukurikataVo As TYosanTukurikataVo In vosByHyojijun(unitKbn)(eventCode)(buhinhyoName)(patternName)
                                    Dim yearKs As String = ChangeDBYearToSpreadYear(tukurikataVo.YosanTukurikataYyyyMm, tukurikataVo.YosanTukurikataKs)
                                    minYearKsT = IIf(CompareYearKs(minYearKsT, yearKs) = True, minYearKsT, yearKs)
                                    maxYearKsT = IIf(CompareYearKs(maxYearKsT, yearKs) = False, maxYearKsT, yearKs)
                                Next
                            Next
                        Next
                    Next
                Next

                yyyyMMList = CreateYearMonthList(minYearKsT, maxYearKsT)
                '年月作成作成'
                Dim col As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX
                For Each yyyyMM As String In yyyyMMList
                    Dim yearKs As String = ChangeYearMonthToYearKs(Left(yyyyMM, 4), Right(yyyyMM, 2))
                    Dim month As String = CInt(yyyyMM.Substring(4, 2)).ToString & "月"
                    sheet.Cells(dataStartRowIndex + 1, col).Value = yearKs
                    sheet.Cells(dataStartRowIndex + 2, col).Value = month

                    If Not yyyyMMColumnDic.ContainsKey(yyyyMM) Then
                        yyyyMMColumnDic.Add(yyyyMM, col)
                    End If

                    SetMonthTitlePro(sheet, col, dataStartRowIndex + 3, SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX, dataStartRowIndex + 1, dataStartRowIndex + 2)

                    monthColumnCount = monthColumnCount + 1
                    dataCount = dataCount + 1
                    col = col + 1
                Next

                For Each unitKbn As String In vosByHyojijun.Keys
                    'ユニット開始の行番号
                    startRow = rowIndex
                    Dim spanCount As Integer = 0

                    For Each eventCode As String In vosByHyojijun(unitKbn).Keys
                        For Each buhinhyoName As String In vosByHyojijun(unitKbn)(eventCode).Keys
                            For Each patternName As String In vosByHyojijun(unitKbn)(eventCode)(buhinhyoName).Keys
                                SetTukurikataRowPro(sheet, rowIndex)
                                columnIndex = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX

                                For Each tukurikataVo As TYosanTukurikataVo In vosByHyojijun(unitKbn)(eventCode)(buhinhyoName)(patternName)
                                    If yyyyMMColumnDic.ContainsKey(tukurikataVo.YosanTukurikataYyyyMm) Then

                                        '台数セル
                                        Dim key As String = EzUtil.MakeKey(shisakuSyubetu, unitKbn, tukurikataVo.YosanEventCode, buhinhyoName, patternName, tukurikataVo.YosanTukurikataYyyyMm)

                                        If daisuDic.ContainsKey(key) Then
                                            sheet.Cells(rowIndex, yyyyMMColumnDic(tukurikataVo.YosanTukurikataYyyyMm)).Formula = daisuDic(key)
                                        End If

                                    End If
                                    SetMonthCellPro(sheet, rowIndex, columnIndex, False)
                                    'columnIndex = columnIndex + 1
                                    '
                                    patternName = tukurikataVo.PatternName
                                Next

                                'イベントコード
                                sheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX - 1).Value = eventCode
                                'ユニット区分
                                sheet.Cells(rowIndex, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).Value = unitKbn


                                Dim buhinKey As String = EzUtil.MakeKey(shisakuSyubetu, unitKbn, eventCode, buhinhyoName, patternName, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX)
                                If patternDic.ContainsKey(buhinKey) Then
                                    '部品表名
                                    sheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX).Formula = patternDic(buhinKey)

                                    'patternDic.Add(buhinKey, "'" & sheet.SheetName & "'!R" & rowIndex + 1 & "C" & SPD_TUKURIKATA_BUHINHYO_COLUMN_INDEX + 1)
                                End If

                                'パターン名
                                Dim patternKey As String = EzUtil.MakeKey(shisakuSyubetu, unitKbn, eventCode, buhinhyoName, patternName, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX)
                                If patternDic.ContainsKey(patternKey) Then
                                    'パターン名
                                    sheet.Cells(rowIndex, SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX).Formula = patternDic(patternKey)

                                    'patternDic.Add(patternKey, "'" & sheet.SheetName & "'!R" & rowIndex + 1 & "C" & SPD_TUKURIKATA_PATTERNNAME_COLUMN_INDEX + 1)
                                End If


                                '部品費'
                                Dim buhinhiKey As String = EzUtil.MakeKey(shisakuSyubetu, unitKbn, eventCode, buhinhyoName, patternName, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX)
                                If patternDic.ContainsKey(buhinhiKey) Then
                                    sheet.Cells(rowIndex, SPD_TUKURIKATA_BUHINHI_COLUMN_INDEX).Formula = patternDic(buhinhiKey)
                                End If

                                '型費'
                                Dim katahiKey As String = EzUtil.MakeKey(shisakuSyubetu, unitKbn, eventCode, buhinhyoName, patternName, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX)
                                If patternDic.ContainsKey(katahiKey) Then
                                    sheet.Cells(rowIndex, SPD_TUKURIKATA_KATAHI_COLUMN_INDEX).Formula = patternDic(katahiKey)
                                End If

                                rowIndex = rowIndex + 1
                                indexX = indexX + 1
                                dataRowCount = dataRowCount + 1
                                spanCount = spanCount + 1
                            Next

                        Next

                        'ユニット単位で追加行の行番号
                        PatternNameInsertRowIndex(shisakuSyubetu, unitKbn) = rowIndex
                    Next

                    sheet.Cells(startRow, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX).RowSpan = spanCount
                Next

                '台数合計
                CalcTukurikata(sheet, dataStartRowIndex, rowIndex, monthColumnCount)
                dataStartRowIndex = rowIndex

            End If

            sheet.Cells(borderStartRow, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).ColumnSpan = monthColumnCount - SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX
            sheet.Cells(borderStartRow, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).BackColor = Color.FromArgb(255, 255, 153)
            sheet.Cells(borderStartRow + 1, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).BackColor = Color.FromArgb(255, 255, 153)
            sheet.Cells(borderStartRow + 2, SPD_TUKURIKATA_DAISU_COLUMN_INDEX + 1).BackColor = Color.FromArgb(255, 255, 153)

            Dim cbs As New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine, Color.Black)
            Dim cBorder As New FarPoint.Win.ComplexBorder(cbs, cbs, cbs, cbs)

            '表に加線
            If monthColumnCount > SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX Then

                '年期セルをマージする'
                For columnIndex As Integer = SPD_TUKURIKATA_MONTH_COLUMNS_START_INDEX To monthColumnCount - 1 Step 6
                    sheet.Cells(borderStartRow + 1, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
                    sheet.Cells(borderStartRow + 1, columnIndex).ColumnSpan = 6
                Next

                Dim cr As New FarPoint.Win.Spread.Model.CellRange(borderStartRow, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX, dataRowCount, monthColumnCount - 1)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            Else
                Dim cr As New FarPoint.Win.Spread.Model.CellRange(borderStartRow, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX, dataRowCount, monthColumnCount)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            End If

            Return Nothing
            'Return nothing aTukurikataList
        End Function

        ''' <summary>
        ''' 表示順Noによって造り方情報を返すDictionaryを作成する
        ''' </summary>
        ''' <param name="aList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MakeTukurikataVosByHyojijunSum(ByVal aList As List(Of TYosanTukurikataVo)) As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo)))))
            Dim vosByHyojijun As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo)))))

            For Each vo As TYosanTukurikataVo In aList
                If Not vosByHyojijun.ContainsKey(vo.UnitKbn) Then
                    vosByHyojijun.Add(vo.UnitKbn, New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo)))))
                End If

                If Not vosByHyojijun(vo.UnitKbn).ContainsKey(vo.YosanEventCode) Then
                    vosByHyojijun(vo.UnitKbn).Add(vo.YosanEventCode, New Dictionary(Of String, Dictionary(Of String, List(Of TYosanTukurikataVo))))
                End If
                If Not vosByHyojijun(vo.UnitKbn)(vo.YosanEventCode).ContainsKey(vo.BuhinhyoName) Then
                    vosByHyojijun(vo.UnitKbn)(vo.YosanEventCode).Add(vo.BuhinhyoName, New Dictionary(Of String, List(Of TYosanTukurikataVo)))
                End If
                If Not vosByHyojijun(vo.UnitKbn)(vo.YosanEventCode)(vo.BuhinhyoName).ContainsKey(vo.PatternName) Then
                    vosByHyojijun(vo.UnitKbn)(vo.YosanEventCode)(vo.BuhinhyoName).Add(vo.PatternName, New List(Of TYosanTukurikataVo))
                End If

                vosByHyojijun(vo.UnitKbn)(vo.YosanEventCode)(vo.BuhinhyoName)(vo.PatternName).Add(vo)

            Next

            Return vosByHyojijun
        End Function

#End Region

#Region "金材(集計用)"

        ''' <summary>
        ''' 金材(集計用)
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub KanazaiSum()

            'タイトル部作成'
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).Value = "≪金材≫"
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).ColumnSpan = 5
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).RowSpan = 2

            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).ColumnSpan = 2

            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).Value = "月分布"
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).BackColor = Color.FromArgb(255, 255, 153)
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 1, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).BackColor = Color.FromArgb(255, 255, 153)
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).BackColor = Color.FromArgb(255, 255, 153)

            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_DAISU_COLUMN_INDEX).Value = "台数"
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_DAISU_COLUMN_INDEX).BackColor = Color.FromArgb(204, 255, 255)
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_TANKA_COLUMN_INDEX).Value = "単価"
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_TANKA_COLUMN_INDEX).BackColor = Color.FromArgb(255, 255, 153)
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_KEI_COLUMN_INDEX).Value = "計"
            sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX + 2, SPD_KANAZAI_KEI_COLUMN_INDEX).BackColor = Color.FromArgb(204, 255, 255)


            Dim vosByHyojijun As Dictionary(Of String, Dictionary(Of String, List(Of TYosanKanazaiVo))) = MakeKanazaiVosByHyojijunSum(kanazaiListSum)
            Dim borderStartRow As Integer = SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX

            Dim dataEndRow As Integer = 0
            Dim monthCount As Integer = 0



            '範囲をリスト化する'
            Dim yyyyMMList As New List(Of String)
            Dim yyyyMMColumnDic As New Dictionary(Of String, Integer)

            Dim minYearKsT As String = "9999年下期"
            Dim maxYearKsT As String = "0000年上期"

            For Each eventCode As String In vosByHyojijun.Keys
                For Each kanazaiName As String In vosByHyojijun(eventCode).Keys
                    For Each vo As TYosanKanazaiVo In vosByHyojijun(eventCode)(kanazaiName)

                        Dim yearKs As String = ChangeDBYearToSpreadYear(vo.YosanTukurikataYyyyMm, vo.YosanTukurikataKs)
                        minYearKsT = IIf(CompareYearKs(minYearKsT, yearKs) = True, minYearKsT, yearKs)
                        maxYearKsT = IIf(CompareYearKs(maxYearKsT, yearKs) = False, maxYearKsT, yearKs)
                    Next
                Next
            Next

            yyyyMMList = CreateYearMonthList(minYearKsT, maxYearKsT)
            '年月作成作成'
            Dim col As Integer = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX
            For Each yyyyMM As String In yyyyMMList
                Dim yearKs As String = ChangeYearMonthToYearKs(Left(yyyyMM, 4), Right(yyyyMM, 2))
                Dim month As String = CInt(yyyyMM.Substring(4, 2)).ToString & "月"
                sheet.Cells(SPD_KANAZAI_YEAR_ROW_INDEX, col).Value = yearKs
                sheet.Cells(SPD_KANAZAI_MONTH_ROW_INDEX, col).Value = month

                If Not yyyyMMColumnDic.ContainsKey(yyyyMM) Then
                    yyyyMMColumnDic.Add(yyyyMM, col)
                    monthCount = monthCount + 1
                End If

                '月分布タイトル
                SetMonthTitlePro(sheet, col, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                                 SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)

                col = col + 1
            Next

            If vosByHyojijun.Count > 0 Then

                Dim indexX As Integer = 0
                Dim ks As String = String.Empty
                Dim columnIndex As Integer = 0

                For Each eventCode As String In vosByHyojijun.Keys
                    For Each kanazaiName As String In vosByHyojijun(eventCode).Keys
                        Dim rowIndex As Integer = indexX + SPD_KANAZAI_KANAZAINAME_ROWS_DATA_START_INDEX
                        columnIndex = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX

                        SetKanazaiRowPro(rowIndex)

                        'イベントコード
                        sheet.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX - 1).Value = eventCode
                        '金材項目名
                        sheet.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).Value = kanazaiName
                        '単価
                        sheet.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).Value = vosByHyojijun(eventCode)(kanazaiName)(0).KanazaiUnitPrice

                        For Each kanazaiVo As TYosanKanazaiVo In vosByHyojijun(eventCode)(kanazaiName)
                            'If indexX = 0 Then
                            '    '年期タイトル
                            '    If (columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                            '        '合計列追加
                            '        columnIndex = columnIndex + 1
                            '    End If

                            '    '年
                            '    Dim yearKs As String = ChangeDBYearToSpreadYear(kanazaiVo.YosanTukurikataYyyyMm, kanazaiVo.YosanTukurikataKs)
                            '    sheet.Cells(SPD_KANAZAI_YEAR_ROW_INDEX, columnIndex).Value = yearKs
                            '    '月
                            '    sheet.Cells(SPD_KANAZAI_MONTH_ROW_INDEX, columnIndex).Value = CInt(kanazaiVo.YosanTukurikataYyyyMm.Substring(4, 2)).ToString & "月"
                            '    monthCount = monthCount + 1
                            '    kanazaiDataColumnCount = kanazaiDataColumnCount + 1

                            '    'minYearKs = IIf(CompareYearKs(minYearKs, yearKs) = True, minYearKs, yearKs)
                            '    'maxYearKs = IIf(CompareYearKs(maxYearKs, yearKs) = False, maxYearKs, yearKs)
                            'End If
                            ''月分布タイトル
                            'SetMonthTitlePro(sheet, columnIndex, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                            '                 SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                            '合計列の場合
                            'If (columnIndex - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX + 1) Mod (HALF_MONTH_DEFAULT_COUNT + 1) = 0 Then
                            '    SetMonthCellPro(sheet, rowIndex, columnIndex, True)
                            '    columnIndex = columnIndex + 1
                            'End If

                            '台数セル
                            'sheet.Cells(rowIndex, columnIndex).Value = kanazaiVo.DaisuSuryo

                            Dim key As String = EzUtil.MakeKey(kanazaiVo.YosanEventCode, kanazaiName, kanazaiVo.YosanTukurikataYyyyMm)
                            If kanazaiDaisuDic.ContainsKey(key) Then
                                sheet.Cells(rowIndex, yyyyMMColumnDic(kanazaiVo.YosanTukurikataYyyyMm)).Formula = kanazaiDaisuDic(key)
                            End If

                            SetMonthCellPro(sheet, rowIndex, columnIndex, False)
                            columnIndex = columnIndex + 1
                            dataEndRow = rowIndex
                        Next
                        indexX = indexX + 1
                    Next


                Next
                '合計列追加
                'AddSpdMonthCountColumn(sheet, columnIndex, SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, _
                '                       SPD_KANAZAI_MONTH_COLUMNS_START_INDEX, SPD_KANAZAI_YEAR_ROW_INDEX, SPD_KANAZAI_MONTH_ROW_INDEX)
                '月分布フッター
                'sheet.Cells(sheet.RowCount - 1, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).ColumnSpan = sheet.ColumnCount - SPD_KANAZAI_KOTEI_COLUMN_COUNT - 1

                '台数合計
                CalcKanazai(dataEndRow, monthCount)
            End If

            Dim cbs As New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine, Color.Black)
            Dim cBorder As New FarPoint.Win.ComplexBorder(cbs, cbs, cbs, cbs)

            '表に加線
            monthCount = monthCount + SPD_KANAZAI_MONTH_COLUMNS_START_INDEX
            If monthCount > SPD_KANAZAI_MONTH_COLUMNS_START_INDEX Then

                '年期セルをマージする'
                For columnIndex As Integer = SPD_KANAZAI_MONTH_COLUMNS_START_INDEX To monthCount - 1 Step 6
                    sheet.Cells(borderStartRow + 1, columnIndex).HorizontalAlignment = CellHorizontalAlignment.Left
                    sheet.Cells(borderStartRow + 1, columnIndex).ColumnSpan = 6
                Next
                sheet.Cells(SPD_KANAZAI_KANAZAINAME_ROWS_START_INDEX, SPD_KANAZAI_MONTH_COLUMNS_START_INDEX).ColumnSpan = monthCount - SPD_KANAZAI_MONTH_COLUMNS_START_INDEX

                Dim cr As New FarPoint.Win.Spread.Model.CellRange(borderStartRow, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX, dataEndRow - borderStartRow + 1, monthCount - 1)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            Else
                Dim cr As New FarPoint.Win.Spread.Model.CellRange(borderStartRow, SPD_TUKURIKATA_UNITKBN_COLUMN_INDEX, 3, monthCount)
                spread.ActiveSheet.SetOutlineBorder(cr, cBorder)
                spread.ActiveSheet.SetInsideBorder(cr, cBorder)
            End If
        End Sub


        ''' <summary>
        ''' 表示順Noによって金材情報を返すDictionaryを作成する
        ''' </summary>
        ''' <param name="aList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MakeKanazaiVosByHyojijunSum(ByVal aList As List(Of TYosanKanazaiVo)) As Dictionary(Of String, Dictionary(Of String, List(Of TYosanKanazaiVo)))
            Dim vosByHyojijun As New Dictionary(Of String, Dictionary(Of String, List(Of TYosanKanazaiVo)))

            For Each vo As TYosanKanazaiVo In aList
                If Not vosByHyojijun.ContainsKey(vo.YosanEventCode) Then
                    vosByHyojijun.Add(vo.YosanEventCode, New Dictionary(Of String, List(Of TYosanKanazaiVo)))
                End If
                If Not vosByHyojijun(vo.YosanEventCode).ContainsKey(vo.KanazaiName) Then
                    vosByHyojijun(vo.YosanEventCode).Add(vo.KanazaiName, New List(Of TYosanKanazaiVo))
                End If
                vosByHyojijun(vo.YosanEventCode)(vo.KanazaiName).Add(vo)
            Next

            Return vosByHyojijun
        End Function

#End Region

#Region "財務実績(集計用)"

        ''' <summary>
        ''' 財務実績(集計用)
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ZaimuJisekiSum()
            '集計値SPREADの月分布列に列を挿入
            AddZaimuJisekiMonthColumnsSum()

            ''造り方Spreadの部品費と型費をセット
            '参照のみなのでいらない'
            'SetSpreadTukurikataBuhinHi(yosanEventCode, True, True)

            ''集計値Spreadに表示値をセット
            SetSpreadZaimujisekiDataSum()
            ''合計
            CalcZaimuJiseki()

            ''システム日付の直近の年期(月分布列)を拡大表示
            'VisibleSystemYearMonthColumns()
        End Sub

        ''' <summary>
        ''' 集計値SPREADの月分布列に列を挿入
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub AddZaimuJisekiMonthColumnsSum()


            yearMonths = CreateYearMonthList(minYearKsSum, maxYearKsSum)

            If yearMonths.Count + SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX > sheet.ColumnCount Then
                sheet.AddColumns(sheet.ColumnCount - 1, sheet.ColumnCount - 1 + yearMonths.Count)
            End If



            '集計値SPREADの月分布列に列を挿入
            Dim columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX
            For index As Integer = 0 To yearMonths.Count - 1
                SetMonthColumnProKingaku(sheet, columnIndex, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
                                         SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)
                '年
                sheet.Cells(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).Value = ChangeYearMonthToYearKs(yearMonths(index).Substring(0, 4), yearMonths(index).Substring(4))
                '月
                sheet.Cells(SPD_ZAIMUJISEKI_MONTH_ROW_INDEX, columnIndex).Value = CInt(yearMonths(index).Substring(4, 2)).ToString & "月"
                columnIndex = columnIndex + 1


                For row As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX
                    If Not zaimuDataDic(row).ContainsKey(yearMonths(index)) Then
                        zaimuDataDic(row).Add(yearMonths(index), New List(Of String))
                    End If
                Next

            Next
            SetMonthColumnProKingaku(sheet, columnIndex, SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, _
                                     SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX, SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, SPD_ZAIMUJISEKI_MONTH_ROW_INDEX)

            'タイトル部の作成'
            CreateZaimuTitle()

        End Sub

        ''' <summary>
        ''' 集計値Spreadに表示値をセット
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetSpreadZaimujisekiDataSum()

            '_YosanEventVo = EventDao.FindByPk(eventCode)

            '比例費と固定費の見通し部品費（メタル／トリム）を取得
            GetMitoshiMetalAndTrimData()

            '比例費と固定費の財務実績を取得
            '_ZaimujisekiList = editDao.FindYosanZaimuJisekiBy(_YosanEventVo.YosanCode)

            '比例費の見通し部品費以外を取得
            '_EventMitoshiList = editDao.FindYosanEventMitoshiBy(eventCode)

            '比例費の見通し移管車＆生産部実績を取得
            '_SeisanbuJisekiList = New Dictionary(Of String, Decimal)
            'If Not isInitShow Then
            '    GetMitoshiSeisanbuJisekiData()
            'End If

            '発注実績
            'GetHatchuJisekiData()

            '集計値Spreadに比例費と固定費の見通しの部品費を設定
            For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To yearMonths.Count + SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX - 1
                Dim yearMonth As String = ChangeSpreadYearToDBYear(sheet.GetValue(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(0, 4), _
                                                                   sheet.GetValue(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).ToString.Substring(4), _
                                                                   CInt(sheet.GetValue(SPD_ZAIMUJISEKI_MONTH_ROW_INDEX, columnIndex).ToString.Replace("月", "")).ToString("00"))

                For rowIndex As Integer = SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX To SPD_ZAIMUJISEKI_KOTEI_ZAIMU_KEI_ROWS_INDEX

                    Dim formula As String = ""
                    For Each f As String In zaimuDataDic(rowIndex)(yearMonth)
                        formula = formula & f & "+"
                    Next

                    If StringUtil.IsNotEmpty(formula) Then
                        sheet.Cells(rowIndex, columnIndex).Formula = Left(formula, formula.Length - 1)
                    End If
                Next

                '20160224 kabasawa'
                '関数で実験'
                '比例見通しメタル'
                If yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX).ContainsKey(yearMonth) Then
                    Dim formula As String = ""
                    For Each str As String In yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX)(yearMonth)
                        formula = formula & str & "+"
                    Next
                    If StringUtil.IsNotEmpty(formula) Then
                        sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_METAL_ROWS_INDEX, columnIndex).Formula = Left(formula, formula.Length - 1)
                    End If

                End If
                '比例見通しトリム'
                If yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX).ContainsKey(yearMonth) Then
                    Dim formula As String = ""
                    For Each str As String In yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX)(yearMonth)
                        formula = formula & str & "+"
                    Next
                    If StringUtil.IsNotEmpty(formula) Then
                        sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_TRIM_ROWS_INDEX, columnIndex).Formula = Left(formula, formula.Length - 1)
                    End If
                End If
                '固定見通しメタル'
                If yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX).ContainsKey(yearMonth) Then
                    Dim formula As String = ""
                    For Each str As String In yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX)(yearMonth)
                        formula = formula & str & "+"
                    Next
                    If StringUtil.IsNotEmpty(formula) Then
                        sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_METAL_ROWS_INDEX, columnIndex).Formula = Left(formula, formula.Length - 1)
                    End If
                End If
                '固定見通しトリム'
                If yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX).ContainsKey(yearMonth) Then
                    Dim formula As String = ""
                    For Each str As String In yearMonthFormulaSumEvent(sheet.SheetName)(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX)(yearMonth)
                        formula = formula & str & "+"
                    Next
                    If StringUtil.IsNotEmpty(formula) Then
                        sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_MITOSHI_TRIM_ROWS_INDEX, columnIndex).Formula = Left(formula, formula.Length - 1)
                    End If
                End If



                ''財務実績
                'For Each zaimuVo As TYosanZaimuJisekiVo In _ZaimujisekiList
                '    If String.Equals(yearMonth, zaimuVo.YosanZaimuJisekiYyyyMm) Then
                '        '比例費の場合
                '        If IsHirei(zaimuVo.YosanZaimuHireiKoteiKbn) Then
                '            'メタル部品費
                '            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_METAL) Then
                '                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_METAL_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                '            End If
                '            '鋼板材料
                '            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU) Then
                '                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_ZAIRYOU_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                '            End If
                '            '輸送費
                '            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_YUSOU) Then
                '                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_YUSOU_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                '            End If
                '            '移管車＆生産部実績
                '            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_SEISAN) Then
                '                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_SEISAN_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                '            End If
                '            'トリム部品費
                '            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_HIREI_TRIM) Then
                '                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_ZAIMU_TRIM_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                '            End If
                '        End If

                '        '固定費の場合
                '        If IsKotei(zaimuVo.YosanZaimuHireiKoteiKbn) Then
                '            'メタル部品費
                '            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_KOTEI_METAL) Then
                '                sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_METAL_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                '            End If
                '            'トリム部品費
                '            If String.Equals(zaimuVo.YosanZaimuJisekiKbn, ZAIMU_JISEKI_KBN_KOTEI_TRIM) Then
                '                sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_ZAIMU_TRIM_ROWS_INDEX, columnIndex).Value = zaimuVo.YosanZaimuInputQty
                '            End If
                '        End If
                '    End If
                'Next
                ''見通し
                'For Each mitoshiVo As TYosanEventMitoshiVo In _EventMitoshiList
                '    If String.Equals(yearMonth, mitoshiVo.YosanEventMitoshiYyyyMm) Then
                '        '鋼板材料
                '        If String.Equals(mitoshiVo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_ZAIRYOU) Then
                '            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_ZAIRYOU_ROWS_INDEX, columnIndex).Value = mitoshiVo.YosanEventMitoshiInputQty
                '        End If
                '        '輸送費
                '        If String.Equals(mitoshiVo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_YUSOU) Then
                '            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_YUSOU_ROWS_INDEX, columnIndex).Value = mitoshiVo.YosanEventMitoshiInputQty
                '        End If
                '        '移管車＆生産部実績
                '        If String.Equals(mitoshiVo.YosanEventMitoshiKbn, ZAIMU_JISEKI_KBN_HIREI_SEISAN) Then
                '            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX, columnIndex).Value = mitoshiVo.YosanEventMitoshiInputQty
                '        End If
                '    End If
                'Next
                ''初期表示以外の場合、移管車＆生産部実績値は金材SPREADのデータを積上げる
                ''If Not isInitShow Then
                ''    For Each key As String In _SeisanbuJisekiList.Keys
                ''        If String.Equals(yearMonth, key) Then
                ''            sheet.Cells(SPD_ZAIMUJISEKI_HIREI_MITOSHI_SEISAN_ROWS_INDEX, columnIndex).Value = _SeisanbuJisekiList(key)
                ''            Exit For
                ''        End If
                ''    Next
                ''End If
                ''発注実績
                'For Each unitKbn As String In _HireiHatchuJisekiList.Keys
                '    For Each keyYearMonth As String In _HireiHatchuJisekiList(unitKbn).Keys
                '        If String.Equals(yearMonth, keyYearMonth) Then
                '            If IsUnitMetal(unitKbn) Then
                '                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_METAL_ROWS_INDEX, columnIndex).Value = _HireiHatchuJisekiList(unitKbn)(keyYearMonth)
                '            Else
                '                sheet.Cells(SPD_ZAIMUJISEKI_HIREI_HATCHU_TRIM_ROWS_INDEX, columnIndex).Value = _HireiHatchuJisekiList(unitKbn)(keyYearMonth)
                '            End If
                '            Exit For
                '        End If
                '    Next
                'Next
                'For Each unitKbn As String In _KoteiHatchuJisekiList.Keys
                '    For Each keyYearMonth As String In _KoteiHatchuJisekiList(unitKbn).Keys
                '        If String.Equals(yearMonth, keyYearMonth) Then
                '            If IsUnitMetal(unitKbn) Then
                '                sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_METAL_ROWS_INDEX, columnIndex).Value = _KoteiHatchuJisekiList(unitKbn)(keyYearMonth)
                '            Else
                '                sheet.Cells(SPD_ZAIMUJISEKI_KOTEI_HATCHU_TRIM_ROWS_INDEX, columnIndex).Value = _KoteiHatchuJisekiList(unitKbn)(keyYearMonth)
                '            End If
                '            Exit For
                '        End If
                '    Next
                'Next
                'End If
            Next
            For columnIndex As Integer = SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX To yearMonths.Count + SPD_ZAIMUJISEKI_MONTH_COLUMNS_START_INDEX - 1 Step 6
                sheet.Cells(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).ColumnSpan = 6
                sheet.Cells(SPD_ZAIMUJISEKI_YEAR_ROW_INDEX, columnIndex).HorizontalAlignment = CellHorizontalAlignment.General
            Next

        End Sub

#End Region

#End Region

    End Class
End Namespace