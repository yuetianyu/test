Imports EBom.Excel
Imports FarPoint.Win.Spread
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports YosansyoTool.YosanshoEdit.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao

Namespace YosanshoEdit.Logic
    Public Class YosanShoExportExcel

        'から行
        Private dataStartRowIndex As Integer
        Private _eventCode As String
        Private _view As FrmYosanshoEdit
        Private _logic As DispYosanshoEdit

#Region "明細部各列の番号指定"

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
#End Region

#Region "Sheet1/3/4-イベント/メタル部品表/トリム部品表定数"
        Private Const EVENT_TITLE_ROW As Integer = 1
        Private Const EVENT_DATA_ROW As Integer = 2
        Private Const BUHIN_TITLE_ROW As Integer = 2
        Private Const BUHIN_DATA_ROW As Integer = 3
#End Region

#Region "Sheet2-集計値定数"

        ''' <summary>集計値列開始の列番号</summary>
        Private Const ZAIMU_JISEKI_COLUMNS_START_COLUMN As Integer = 2
        ''' <summary>業務プロジェクト</summary>
        Private Const ZAIMU_JISEKI_ITEM_COLUMN As Integer = 3
        ''' <summary>業務業務明細</summary>
        Private Const ZAIMU_JISEKI_ITEM_DETAIL_COLUMN As Integer = 4
        ''' <summary>集計値月分布列開始の列番号</summary>
        Private Const ZAIMU_JISEKI_MONTH_COLUMNS_START_INDEX As Integer = 6
        ''' <summary>集計値データ行開始の番号</summary>
        Private Const ZAIMU_JISEKI_DATA_ROW_START_INDEX As Integer = 5

        ''' <summary>集計値行数</summary>
        Private Const ZAIMU_JISEKI_ROW_COUNT As Integer = 27

        ''' <summary>集計値spread台数開始の行番号</summary>
        Private Const ZAIMU_JISEKI_DAISU_ROW_START_INDEX As Integer = 3
        ''' <summary>集計値spread台数開始の列番号</summary>
        Private Const ZAIMU_JISEKI_DAISU_COLUMN_START_INDEX As Integer = 3

#End Region

#Region "Sheet2-製作台数定数"

        ''' <summary>製作台数試作種別列開始の列番号</summary>
        Private Const SEISAKU_DAISU_COLUMNS_START_COLUMN As Integer = 2
        ''' <summary>製作台数の台数列開始の列番号</summary>
        Private Const DAISU_COLUMN As Integer = 3
        ''' <summary>工事指令№列開始の列番号</summary>
        Private Const KOJISHIREI_NO_COLUMNS_START_COLUMN As Integer = 4

        ''' <summary>集計値行数</summary>
        Private Const SEISAKU_DAISU_ROW_COUNT As Integer = 5

        ''' <summary>集計値spread台数開始の行番号</summary>
        Private Const SEISAKU_DAISU_ROW_START_INDEX As Integer = 3
        ''' <summary>集計値spread試作種別の列番号</summary>
        Private Const SEISAKU_DAISU_SYUBETU_COLUMN_INDEX As Integer = 0
        ''' <summary>集計値spread台数の列番号</summary>
        Private Const SEISAKU_DAISU_DAISU_COLUMN_INDEX As Integer = 1
        ''' <summary>集計値spread台数開始の列番号</summary>
        Private Const SEISAKU_DAISU_COLUMN_START_INDEX As Integer = 3

#End Region

#Region "Sheet2-造り方定数"

        ''' <summary>造り方spread台数開始の行番号</summary>
        Private Const KANSEI_TUKURIKATA_ROW_START_INDEX As Integer = 3
        ''' <summary>造り方spreadユニット区分の列番号</summary>
        Private Const KANSEI_TUKURIKATA_SYUBETU_COLUMN_INDEX As Integer = 0
        ''' <summary>造り方spreadパターン名の列番号</summary>
        Private Const KANSEI_TUKURIKATA_COLUMN_INDEX As Integer = 2
        ''' <summary>造り方spread台数の列番号</summary>
        Private Const KANSEI_TUKURIKATA_COLUMN_DAISU_INDEX As Integer = 5
        ''' <summary>造り方spread開始の列番号</summary>
        Private Const KANSEI_TUKURIKATA_COLUMN_START_INDEX As Integer = 7
        ''' <summary>月分布列開始の列番号</summary>
        Private Const KANSEI_TUKURIKATA_MONTH_COLUMNS_START_INDEX As Integer = 8
        ''' <summary>ユニット区分の列番号</summary>
        Private Const KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX As Integer = 2
        ''' <summary>部品表名の列番号</summary>
        Private Const KANSEI_TUKURIKATA_BUHINHYONAME_COLUMN_INDEX As Integer = 3
        ''' <summary>パターン名の列番号</summary>
        Private Const KANSEI_TUKURIKATA_PATTERNNAME_COLUMN_INDEX As Integer = 4
        ''' <summary>部品費（円）の列番号</summary>
        Private Const KANSEI_BUHIN_HI_COLUMN_INDEX As Integer = 5
        ''' <summary>型費（千円）の列番号</summary>
        Private Const KANSEI_KATA_HI_COLUMN_INDEX As Integer = 6
        ''' <summary>台数の列番号</summary>
        Private Const DAISU_COLUMN_INDEX As Integer = 7

#End Region

#Region "Sheet2-金材定数"

        ''' <summary>金材spread台数開始の行番号</summary>
        Private Const KANAZAI_ROW_START_INDEX As Integer = 3
        ''' <summary>金材spread項目名の列番号</summary>
        Private Const SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX As Integer = 0
        ''' <summary>金材spread台数の列番号</summary>
        Private Const SPD_KANAZAI_DAISU_COLUMN_INDEX As Integer = 1
        ''' <summary>金材spread単価の列番号</summary>
        Private Const SPD_KANAZAI_TANKA_COLUMN_INDEX As Integer = 3
        ''' <summary>金材spread計の列番号</summary>
        Private Const SPD_KANAZAI_KEI_COLUMN_INDEX As Integer = 4
        ''' <summary>金材spread台数開始の列番号</summary>
        Private Const SPD_KANAZAI_DAISU_COLUMN_START_INDEX As Integer = 5
        ''' <summary>月分布列開始の列番号</summary>
        Private Const KANAZAI_MONTH_COLUMNS_START_INDEX As Integer = 7
        ''' <summary>金材項目名の列番号</summary>
        Private Const KANAZAI_KANAZAINAME_COLUMN_INDEX As Integer = 2
        ''' <summary>台数の列番号</summary>
        Private Const KANAZAI_DAISU_COLUMN_INDEX As Integer = 4
        ''' <summary>単価の列番号</summary>
        Private Const KANAZAI_TANKA_COLUMN_INDEX As Integer = 5
        ''' <summary>計の列番号</summary>
        Private Const KANAZAI_KEI_COLUMN_INDEX As Integer = 6
        Private Const HALF_MONTH_DEFAULT_COUNT As Integer = 6
        ''' <summary>固定列のカウント</summary>
        Private Const KANAZAI_KOTEI_COLUMN_COUNT As Integer = 5

#End Region

#Region "コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="eventCode"></param>
        ''' <param name="filePath"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal eventCode As String, ByVal filePath As String, ByVal editLogic As DispYosanshoEdit, ByVal f As FrmYosanshoEdit)

            Me._eventCode = eventCode
            Me._logic = editLogic
            Me._view = f

            Dim systemDrive As String = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir
            Dim fileName As String

            Dim yosanBuhinEditVos As New List(Of TYosanBuhinEditVo)
            Dim yosanBuhinEditInsuVos As New List(Of TYosanBuhinEditInsuVo)
            Dim yosanBuhinEditPatternVos As New List(Of TYosanBuhinEditPatternVo)
            Dim instlVosByHyojijun As New Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo))
            Dim yosanshoExportDao As New YosanshoExportDaoImpl

            Using sfd As New SaveFileDialog()
                sfd.FileName = filePath

                'EXCEL出力先フォルダチェック
                ShisakuCommon.ShisakuComFunc.ExcelOutPutDirCheck()
                sfd.InitialDirectory = systemDrive
                fileName = ShisakuCommon.ShisakuGlobal.ExcelOutPutDir + "\" + filePath
            End Using

            Dim yosanUnitKbn As String

            If Not ShisakuComFunc.IsFileOpen(fileName) Then
                Using xls As New ShisakuExcel(fileName)
                    xls.SetFont("ＭＳ Ｐゴシック", 11)

                    'sheet1
                    SetYosanEventSheet(xls)

                    'sheet2 Excel出力集計
                    SetYosanSyukeiSheet(xls)

                    '予算部品表選択情報を取得する。
                    Dim _BuhinhyoVos As List(Of TYosanBuhinSelectVo) = editLogic.YosanBuhinhyoList

                    '予算部品表選択情報分の部品表を出力する。
                    For Each vo As TYosanBuhinSelectVo In _BuhinhyoVos
                        yosanUnitKbn = vo.BuhinhyoName
                        yosanBuhinEditVos = yosanshoExportDao.FindYosanBuhinEdit(eventCode, yosanUnitKbn)
                        yosanBuhinEditInsuVos = yosanshoExportDao.FindYosanBuhinEditInsu(eventCode, yosanUnitKbn)
                        '予算部品編集情報を取得できた場合
                        If 0 < yosanBuhinEditVos.Count Then
                            '予算部品編集パターン情報取得
                            yosanBuhinEditPatternVos = yosanshoExportDao.FindYosanBuhinEditPattern(eventCode, yosanUnitKbn)
                            '予算部品編集員数情報取得
                            instlVosByHyojijun = MakeInstlVosByHyojijun(yosanBuhinEditVos, yosanBuhinEditInsuVos)
                        End If
                        'sheet3
                        setYosanBuhinSheet(xls, yosanBuhinEditVos, instlVosByHyojijun, yosanBuhinEditPatternVos, yosanUnitKbn)
                    Next
                    'yosanUnitKbn = DispYosanshoEdit.UNIT_KBN_METAL_CODE
                    'yosanBuhinEditVos = yosanshoExportDao.FindYosanBuhinEdit(eventCode, yosanUnitKbn)
                    'yosanBuhinEditInsuVos = yosanshoExportDao.FindYosanBuhinEditInsu(eventCode, yosanUnitKbn)
                    ''予算部品編集情報を取得できた場合
                    'If 0 < yosanBuhinEditVos.Count Then
                    '    '予算部品編集パターン情報取得
                    '    yosanBuhinEditPatternVos = yosanshoExportDao.FindYosanBuhinEditPattern(eventCode, yosanUnitKbn)
                    '    '予算部品編集員数情報取得
                    '    instlVosByHyojijun = MakeInstlVosByHyojijun(yosanBuhinEditVos, yosanBuhinEditInsuVos)
                    'End If
                    ''sheet3
                    'setYosanMetalSheet(xls, yosanBuhinEditVos, instlVosByHyojijun, yosanBuhinEditPatternVos)

                    'yosanUnitKbn = DispYosanshoEdit.UNIT_KBN_TRIM_CODE
                    'yosanBuhinEditVos = yosanshoExportDao.FindYosanBuhinEdit(eventCode, yosanUnitKbn)
                    'yosanBuhinEditInsuVos = yosanshoExportDao.FindYosanBuhinEditInsu(eventCode, yosanUnitKbn)
                    ''予算部品編集情報を取得できた場合
                    'If 0 < yosanBuhinEditVos.Count Then
                    '    '予算部品編集パターン情報取得
                    '    yosanBuhinEditPatternVos = yosanshoExportDao.FindYosanBuhinEditPattern(eventCode, yosanUnitKbn)
                    '    '予算部品編集員数情報取得
                    '    instlVosByHyojijun = MakeInstlVosByHyojijun(yosanBuhinEditVos, yosanBuhinEditInsuVos)
                    'End If
                    ''sheet4
                    'setYosanTorimu(xls, yosanBuhinEditVos, instlVosByHyojijun, yosanBuhinEditPatternVos)

                    'A4横で印刷できるように変更'
                    xls.PrintPaper(fileName, 1, "A4")
                    xls.PrintOrientation(fileName, 1, 1, False)
                    xls.SetActiveSheet(1)
                    xls.Save()
                End Using
                Process.Start(fileName)
            End If

        End Sub

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

#Region "Sheet1 "

        ''' <summary>
        ''' Excel出力　予算書イベント情報
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetYosanEventSheet(ByVal xls As ShisakuExcel)

            xls.SetActiveSheet(1)

            xls.SetSheetName("Excel出力イベント")

            setYosanEventColumnNo()

            'タイトル部分の作成'
            setYosanEventTitleRow(xls)

            'データ部分の作成'
            setYosanEventSheetBody(xls)

        End Sub

        ''' <summary>
        ''' 各カラム名に数値を設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub setYosanEventColumnNo()
            Dim column As Integer = 1

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
        ''' <param name="xls">目的のEXCELファイル</param>
        ''' <remarks></remarks>
        Private Sub setYosanEventTitleRow(ByVal xls As ShisakuExcel)

            xls.SetValue(COLUMN_KBN_NAME, EVENT_TITLE_ROW, "区分")
            xls.SetValue(COLUMN_KAIHATSU_FUGO, EVENT_TITLE_ROW, "開発符号")
            xls.SetValue(COLUMN_EVENT, EVENT_TITLE_ROW, "イベント")
            xls.SetValue(COLUMN_EVENT_NAME, EVENT_TITLE_ROW, "イベント名称")
            xls.SetValue(COLUMN_CODE, EVENT_TITLE_ROW, "予算コード")
            xls.SetValue(COLUMN_KIKAN_FROM, EVENT_TITLE_ROW, "予算期間FROM")
            xls.SetValue(COLUMN_KIKAN_SYMBOL, EVENT_TITLE_ROW, "予算期間記号")
            xls.SetValue(COLUMN_KIKAN_TO, EVENT_TITLE_ROW, "予算期間TO")
            xls.SetValue(COLUMN_SEISAKUDAISU_KANSEISYA, EVENT_TITLE_ROW, "予算製作台数・完成車")
            xls.SetValue(COLUMN_SEISAKUDAISU_SYMBOL, EVENT_TITLE_ROW, "予算製作台数記号")
            xls.SetValue(COLUMN_SEISAKUDAISU_WB, EVENT_TITLE_ROW, "予算製作台数・WB車")
            xls.SetValue(COLUMN_MAIN_HENKO_GAIYO, EVENT_TITLE_ROW, "主な変更概要")
            xls.SetValue(COLUMN_TSUKURIKATA_SEISAKUJYOKEN, EVENT_TITLE_ROW, "造り方及び製作条件")
            xls.SetValue(COLUMN_SONOTA, EVENT_TITLE_ROW, "その他")
            xls.SetValue(COLUMN_EVENT_CODE, EVENT_TITLE_ROW, "予算イベントコード")

        End Sub

        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub setYosanEventSheetBody(ByVal xls As ShisakuExcel)

            Dim eventDao As TYosanEventDao = New TYosanEventDaoImpl
            Dim yosanEvent As TYosanEventVo
            yosanEvent = eventDao.FindByPk(_eventCode)

            '区分'
            xls.SetValue(COLUMN_KBN_NAME, EVENT_DATA_ROW, yosanEvent.YosanEventKbnName)
            '開発符号'
            xls.SetValue(COLUMN_KAIHATSU_FUGO, EVENT_DATA_ROW, yosanEvent.YosanKaihatsuFugo)
            'イベント'
            xls.SetValue(COLUMN_EVENT, EVENT_DATA_ROW, yosanEvent.YosanEvent)
            'イベント名称'
            xls.SetValue(COLUMN_EVENT_NAME, EVENT_DATA_ROW, yosanEvent.YosanEventName)
            '予算コード'
            xls.SetValue(COLUMN_CODE, EVENT_DATA_ROW, yosanEvent.YosanCode)
            '予算期間FROM'
            xls.SetValue(COLUMN_KIKAN_FROM, EVENT_DATA_ROW, yosanEvent.YosanKikanFromYyyy)
            '予算期間記号'
            xls.SetValue(COLUMN_KIKAN_SYMBOL, EVENT_DATA_ROW, "～")
            '予算期間TO'
            xls.SetValue(COLUMN_KIKAN_TO, EVENT_DATA_ROW, yosanEvent.YosanKikanToYyyy)
            '予算製作台数・完成車'
            xls.SetValue(COLUMN_SEISAKUDAISU_KANSEISYA, EVENT_DATA_ROW, yosanEvent.YosanSeisakudaisuKanseisya)
            '予算製作台数記号'
            If StringUtil.IsNotEmpty(yosanEvent.YosanSeisakudaisuKanseisya) Or StringUtil.IsNotEmpty(yosanEvent.YosanSeisakudaisuWb) Then
                xls.SetValue(COLUMN_SEISAKUDAISU_SYMBOL, EVENT_DATA_ROW, "+")
            End If
            '予算製作台数・WB車'
            xls.SetValue(COLUMN_SEISAKUDAISU_WB, EVENT_DATA_ROW, yosanEvent.YosanSeisakudaisuWb)
            '主な変更概要'
            xls.SetValue(COLUMN_MAIN_HENKO_GAIYO, EVENT_DATA_ROW, yosanEvent.YosanMainHenkoGaiyo)
            '造り方及び製作条件
            xls.SetValue(COLUMN_TSUKURIKATA_SEISAKUJYOKEN, EVENT_DATA_ROW, yosanEvent.YosanTsukurikataSeisakujyoken)
            'その他'
            xls.SetValue(COLUMN_SONOTA, EVENT_DATA_ROW, yosanEvent.YosanSonota)
            '予算イベントコード'
            xls.SetValue(COLUMN_EVENT_CODE, EVENT_DATA_ROW, yosanEvent.YosanEventCode)

            '設置テキストフォーマット
            xls.SetAlignment(COLUMN_KIKAN_SYMBOL, 1, COLUMN_KIKAN_SYMBOL, 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            '設置レイヤーの幅
            xls.SetColWidth(COLUMN_KIKAN_SYMBOL, 1, 10)
            '設置テキストフォーマット
            xls.SetAlignment(COLUMN_SEISAKUDAISU_KANSEISYA, 1, COLUMN_SEISAKUDAISU_KANSEISYA, 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            'レイヤーの幅を設定
            xls.SetColWidth(COLUMN_SEISAKUDAISU_KANSEISYA, 1, 20)
            '自動幅を設定
            xls.AutoFitCol(COLUMN_KBN_NAME, xls.EndCol())

        End Sub

#End Region

#Region "Sheet2 "

        ''' <summary>
        ''' Excel出力集計
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub SetYosanSyukeiSheet(ByVal xls As ShisakuExcel)

            xls.SetActiveSheet(2)

            xls.SetSheetName("Excel出力集計")

            '出力開始行を設定
            dataStartRowIndex = 2

            '集計値spread出力
            setYosanZaimuJisekiBody(xls)

            '出力開始行を設定
            dataStartRowIndex = dataStartRowIndex + ZAIMU_JISEKI_ROW_COUNT + 2

            '製作台数情報出力
            setYosanDaisuSheetBody(xls)

            '出力開始行を設定
            dataStartRowIndex = dataStartRowIndex + SEISAKU_DAISU_ROW_COUNT + 2

            '造り方完成車情報出力
            setYosanTukurikataSheetBody(xls, _view.spdTukurikataKanseisha_Sheet1)

            dataStartRowIndex = dataStartRowIndex + _view.spdTukurikataKanseisha_Sheet1.RowCount + 1

            '造り方ホワイトボディ情報出力
            setYosanTukurikataSheetBody(xls, _view.spdTukurikataWBsha_Sheet1)

            dataStartRowIndex = dataStartRowIndex + _view.spdTukurikataWBsha_Sheet1.RowCount + 1

            '金材情報出力
            setYosanKanazaiSheetBody(xls)

            '列幅を設定
            xls.SetColWidth(1, xls.EndCol, 12)

        End Sub

#Region "集計値spread出力 "
        ''' <summary>
        ''' Excel出力 集計値　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub setYosanZaimuJisekiBody(ByVal xls As ShisakuExcel)
            Dim sheet As SheetView = _view.spdZaimuJiseki_Sheet1

            'ヘッダー出力
            setYosanZaimuJisekiTitleRow(xls)

            Dim excelRowIndex As Integer = dataStartRowIndex + 1
            Dim colCount As Integer = 0
            'から終わりまで行
            For rowIndex As Integer = ZAIMU_JISEKI_DAISU_ROW_START_INDEX - 2 To sheet.RowCount - 1
                Dim excelColIndex As Integer = ZAIMU_JISEKI_MONTH_COLUMNS_START_INDEX
                'から終わりまで列
                For columnIndex As Integer = ZAIMU_JISEKI_DAISU_COLUMN_START_INDEX To sheet.ColumnCount - 1
                    '出力データを遍歴する
                    xls.SetValue(excelColIndex, excelRowIndex, sheet.Cells(rowIndex, columnIndex).Value)
                    'セルの結合
                    If (excelColIndex + 1) Mod HALF_MONTH_DEFAULT_COUNT = 0 Then
                        '合併年列
                        xls.MergeCells(excelColIndex - HALF_MONTH_DEFAULT_COUNT + 1, dataStartRowIndex + 1, excelColIndex, dataStartRowIndex + 1, True)
                        columnIndex = columnIndex + 1
                    End If

                    excelColIndex = excelColIndex + 1
                Next
                If rowIndex = ZAIMU_JISEKI_DAISU_ROW_START_INDEX - 2 Then
                    '出力した列数
                    colCount = excelColIndex - 1
                End If

                excelRowIndex = excelRowIndex + 1
            Next

            '月分布を設定
            xls.SetValue(ZAIMU_JISEKI_MONTH_COLUMNS_START_INDEX, dataStartRowIndex, "月分布")
            '合併月分布の列
            xls.MergeCells(ZAIMU_JISEKI_MONTH_COLUMNS_START_INDEX, dataStartRowIndex, colCount - 1, dataStartRowIndex, True)
            '月分布背景色
            xls.SetBackColor(ZAIMU_JISEKI_MONTH_COLUMNS_START_INDEX, dataStartRowIndex, colCount, dataStartRowIndex + 2, RGB(204, 255, 255))
            '合計セルの結合
            xls.MergeCells(colCount, dataStartRowIndex, colCount, dataStartRowIndex + 2, True)
            '合計セル
            xls.SetValue(colCount, dataStartRowIndex, "合計")
            '表に加線
            xls.SetLine(ZAIMU_JISEKI_MONTH_COLUMNS_START_INDEX, dataStartRowIndex, colCount, dataStartRowIndex + ZAIMU_JISEKI_ROW_COUNT - 1)
            '設置テキストに表示
            xls.SetAlignment(ZAIMU_JISEKI_MONTH_COLUMNS_START_INDEX, dataStartRowIndex + 3, colCount, dataStartRowIndex + ZAIMU_JISEKI_ROW_COUNT - 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            '#,##0
            xls.SetNumberFormatLocal(ZAIMU_JISEKI_MONTH_COLUMNS_START_INDEX, ZAIMU_JISEKI_DATA_ROW_START_INDEX, colCount, dataStartRowIndex + ZAIMU_JISEKI_ROW_COUNT - 1, "#,##0;@")
        End Sub

        ''' <summary>
        ''' タイトル行の作成
        ''' </summary>
        ''' <param name="xls">目的のEXCELファイル</param>
        ''' <remarks></remarks>
        Private Sub setYosanZaimuJisekiTitleRow(ByVal xls As ShisakuExcel)

            'タイトル部分の作成'
            xls.SetValue(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex, "≪集計値≫")
            xls.MergeCells(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 2, True)
            xls.SetValue(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex + 3, "比例比")
            xls.MergeCells(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex + 3, ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex + 17, True)
            xls.SetValue(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex + 18, "固定費")
            xls.MergeCells(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex + 18, ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex + 26, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 3, "見通し")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 3, ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 8, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 9, "発注実績")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 9, ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 11, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 12, "財務実績")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 12, ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 17, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 18, "見通し")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 18, ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 20, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 21, "発注実績")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 21, ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 23, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 24, "財務実績")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 24, ZAIMU_JISEKI_ITEM_COLUMN, dataStartRowIndex + 26, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 3, "メタル部品費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 3, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 3, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 4, "トリム部品費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 4, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 4, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 5, "鋼板材料")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 5, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 5, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 6, "輸送費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 6, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 6, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 7, "移管車＆生産部実績")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 7, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 7, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 8, "計")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 8, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 8, True)
            xls.SetAlignment(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 8, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 8, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 9, "メタル部品費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 9, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 9, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 10, "トリム部品費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 10, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 10, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 11, "計")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 11, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 11, True)
            xls.SetAlignment(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 11, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 11, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 12, "メタル部品費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 12, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 12, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 13, "鋼板材料")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 13, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 13, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 14, "輸送費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 14, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 14, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 15, "移管車＆生産部実績")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 15, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 15, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 16, "トリム部品費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 16, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 16, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 17, "計")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 17, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 17, True)
            xls.SetAlignment(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 17, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 17, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 18, "メタル部品費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 18, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 18, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 19, "トリム部品費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 19, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 19, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 20, "計")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 20, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 20, True)
            xls.SetAlignment(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 20, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 20, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 21, "メタル部品費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 21, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 21, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 22, "トリム部品費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 22, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 22, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 23, "計")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 23, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 23, True)
            xls.SetAlignment(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 23, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 23, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 24, "メタル部品費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 24, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 24, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 25, "トリム部品費")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 25, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 25, True)
            xls.SetValue(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 26, "計")
            xls.MergeCells(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 26, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 26, True)
            xls.SetAlignment(ZAIMU_JISEKI_ITEM_DETAIL_COLUMN, dataStartRowIndex + 26, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 26, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)

            xls.SetBackColor(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 26, RGB(204, 255, 255))
            xls.SetLine(ZAIMU_JISEKI_COLUMNS_START_COLUMN, dataStartRowIndex, ZAIMU_JISEKI_ITEM_DETAIL_COLUMN + 1, dataStartRowIndex + 26, XlLineStyle.xlContinuous, XlBorderWeight.xlThin)

        End Sub
#End Region

#Region "製作台数spread出力 "
        ''' <summary>
        ''' Excel出力 製作台数　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub setYosanDaisuSheetBody(ByVal xls As ShisakuExcel)
            Dim sheet As SheetView = _view.spdSeisakuDaisu_Sheet1

            'ヘッダー出力
            setYosanDaisuTitleRow(xls)

            Dim excelRowIndex As Integer = dataStartRowIndex + 1
            Dim excelColIndex As Integer = 0
            'から終わりまで行
            For rowIndex As Integer = SEISAKU_DAISU_ROW_START_INDEX - 2 To sheet.RowCount - 1
                excelColIndex = KOJISHIREI_NO_COLUMNS_START_COLUMN
                'から終わりまで列
                For columnIndex As Integer = SEISAKU_DAISU_COLUMN_START_INDEX To sheet.ColumnCount - 1 Step 2
                    '出力データを遍歴する
                    If rowIndex >= SEISAKU_DAISU_ROW_START_INDEX Then
                        Dim daisu As String = Format(sheet.Cells(rowIndex, columnIndex).Value, "#,##0")
                        xls.SetValue(excelColIndex, excelRowIndex, daisu & " 台")
                    Else
                        xls.SetValue(excelColIndex, excelRowIndex, sheet.Cells(rowIndex, columnIndex).Value)
                    End If

                    excelColIndex = excelColIndex + 1
                Next

                If rowIndex >= SEISAKU_DAISU_ROW_START_INDEX Then
                    '試作種別
                    xls.SetValue(SEISAKU_DAISU_COLUMNS_START_COLUMN, excelRowIndex, sheet.Cells(rowIndex, SEISAKU_DAISU_SYUBETU_COLUMN_INDEX).Value)
                    '台数
                    xls.SetValue(DAISU_COLUMN, excelRowIndex, Format(sheet.Cells(rowIndex, SEISAKU_DAISU_DAISU_COLUMN_INDEX).Value, "#,##0") & " 台")
                End If

                excelRowIndex = excelRowIndex + 1
            Next

            '工事指令№を設定
            xls.SetValue(KOJISHIREI_NO_COLUMNS_START_COLUMN, dataStartRowIndex, "工事指令№")
            '合併工事指令№
            xls.MergeCells(KOJISHIREI_NO_COLUMNS_START_COLUMN, dataStartRowIndex, excelColIndex - 1, dataStartRowIndex, True)
            '工事指令№背景色
            xls.SetBackColor(KOJISHIREI_NO_COLUMNS_START_COLUMN, dataStartRowIndex, excelColIndex - 1, dataStartRowIndex + 1, RGB(255, 255, 153))
            '表に加線
            xls.SetLine(SEISAKU_DAISU_COLUMNS_START_COLUMN, dataStartRowIndex, excelColIndex - 1, dataStartRowIndex + SEISAKU_DAISU_ROW_COUNT - 1)
            '設置テキストに表示
            xls.SetAlignment(DAISU_COLUMN, dataStartRowIndex + 3, excelColIndex - 1, dataStartRowIndex + SEISAKU_DAISU_ROW_COUNT - 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
        End Sub

        ''' <summary>
        ''' タイトル行の作成
        ''' </summary>
        ''' <param name="xls">目的のEXCELファイル</param>
        ''' <remarks></remarks>
        Private Sub setYosanDaisuTitleRow(ByVal xls As ShisakuExcel)

            'タイトル部分の作成'
            xls.SetValue(SEISAKU_DAISU_COLUMNS_START_COLUMN, dataStartRowIndex, "≪製作台数≫")
            xls.MergeCells(SEISAKU_DAISU_COLUMNS_START_COLUMN, dataStartRowIndex, DAISU_COLUMN, dataStartRowIndex + 1, True)
            xls.SetValue(DAISU_COLUMN, dataStartRowIndex + 2, "台数")
            xls.SetBackColor(DAISU_COLUMN, dataStartRowIndex + 2, DAISU_COLUMN, dataStartRowIndex + 2, RGB(204, 255, 255))

        End Sub
#End Region

#Region "造り方spread出力 "
        ''' <summary>
        ''' Excel出力 造り方完成車情報　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Private Sub setYosanTukurikataSheetBody(ByVal xls As ShisakuExcel, ByVal sheet As SheetView)
            'ヘッダー出力
            setYosanTukurikataTitleRow(xls, sheet)

            Dim excelRowIndex As Integer = dataStartRowIndex + 1
            Dim colCount As Integer = 0
            Dim unitCount As Integer = 0
            'から終わりまで行
            For rowIndex As Integer = KANSEI_TUKURIKATA_ROW_START_INDEX - 2 To sheet.RowCount - 2
                Dim excelColIndex As Integer = KANSEI_TUKURIKATA_MONTH_COLUMNS_START_INDEX
                'から終わりまで列
                For columnIndex As Integer = KANSEI_TUKURIKATA_COLUMN_START_INDEX To sheet.ColumnCount - 1
                    '出力データを遍歴する
                    If rowIndex >= KANSEI_TUKURIKATA_ROW_START_INDEX Then
                        xls.SetValue(excelColIndex, excelRowIndex, Format(sheet.Cells(rowIndex, columnIndex).Value, "#,##0"))
                    Else
                        xls.SetValue(excelColIndex, excelRowIndex, sheet.Cells(rowIndex, columnIndex).Value)
                    End If
                    'セルの結合
                    If (excelColIndex - 1) Mod HALF_MONTH_DEFAULT_COUNT = 0 Then
                        '合併年列
                        xls.MergeCells(excelColIndex - HALF_MONTH_DEFAULT_COUNT + 1, dataStartRowIndex + 1, excelColIndex, dataStartRowIndex + 1, True)
                        columnIndex = columnIndex + 1
                    End If

                    excelColIndex = excelColIndex + 1
                Next
                If rowIndex = ZAIMU_JISEKI_DAISU_ROW_START_INDEX - 2 Then
                    '出力した列数
                    colCount = excelColIndex - 1
                End If
                If rowIndex >= KANSEI_TUKURIKATA_ROW_START_INDEX Then
                    'ユニット区分
                    xls.SetValue(KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, excelRowIndex, sheet.Cells(rowIndex, KANSEI_TUKURIKATA_SYUBETU_COLUMN_INDEX).Value)
                    '部品表名
                    xls.SetValue(KANSEI_TUKURIKATA_BUHINHYONAME_COLUMN_INDEX, excelRowIndex, sheet.Cells(rowIndex, 1).Value)
                    'パターン名
                    xls.SetValue(KANSEI_TUKURIKATA_PATTERNNAME_COLUMN_INDEX, excelRowIndex, sheet.Cells(rowIndex, KANSEI_TUKURIKATA_COLUMN_INDEX).Value)
                    '部品費（円）
                    xls.SetValue(KANSEI_BUHIN_HI_COLUMN_INDEX, excelRowIndex, sheet.Cells(rowIndex, 3).Value)
                    '型費（千円）
                    xls.SetValue(KANSEI_KATA_HI_COLUMN_INDEX, excelRowIndex, sheet.Cells(rowIndex, KANSEI_TUKURIKATA_PATTERNNAME_COLUMN_INDEX).Value)
                    '台数
                    xls.SetValue(DAISU_COLUMN_INDEX, excelRowIndex, Format(sheet.Cells(rowIndex, KANSEI_TUKURIKATA_COLUMN_DAISU_INDEX).Value, "#,##0") & " 台")
                End If
                '合併ユニット区分
                If Not StringUtil.Equals(xls.GetValue(KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, excelRowIndex), xls.GetValue(KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, excelRowIndex - 1)) Then
                    xls.MergeCells(KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, excelRowIndex - unitCount - 1, KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, excelRowIndex - 1, True)
                    unitCount = 0
                Else
                    unitCount = unitCount + 1
                End If

                excelRowIndex = excelRowIndex + 1
            Next
            '合併ユニット区分
            xls.MergeCells(KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, excelRowIndex - unitCount - 1, KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, excelRowIndex - 1, True)

            '月分布を設定
            xls.SetValue(KANSEI_TUKURIKATA_MONTH_COLUMNS_START_INDEX, dataStartRowIndex, "月分布")
            '合併月分布の列
            xls.MergeCells(KANSEI_TUKURIKATA_MONTH_COLUMNS_START_INDEX, dataStartRowIndex, colCount - 1, dataStartRowIndex, True)
            '月分布背景色
            xls.SetBackColor(KANSEI_TUKURIKATA_MONTH_COLUMNS_START_INDEX, dataStartRowIndex, colCount - 1, dataStartRowIndex + 2, RGB(255, 255, 153))
            '表に加線
            xls.SetLine(KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, dataStartRowIndex, colCount - 1, excelRowIndex - 1)
            '設置テキストに表示
            xls.SetAlignment(KANSEI_BUHIN_HI_COLUMN_INDEX, dataStartRowIndex + 3, colCount - 1, excelRowIndex - 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            '#,##0
            xls.SetNumberFormatLocal(KANSEI_BUHIN_HI_COLUMN_INDEX, dataStartRowIndex + 3, KANSEI_KATA_HI_COLUMN_INDEX, excelRowIndex - 1, "#,##0;@")
        End Sub

        ''' <summary>
        ''' タイトル行の作成
        ''' </summary>
        ''' <param name="xls">目的のEXCELファイル</param>
        ''' <remarks></remarks>
        Private Sub setYosanTukurikataTitleRow(ByVal xls As ShisakuExcel, ByVal sheet As SheetView)

            'タイトル部分の作成'
            xls.SetValue(KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, dataStartRowIndex + 1, "≪造り方≫")
            xls.MergeCells(KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, dataStartRowIndex, DAISU_COLUMN_INDEX, dataStartRowIndex + 1, True)
            If sheet.Equals(_view.spdTukurikataKanseisha_Sheet1) Then
                xls.SetValue(KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, dataStartRowIndex + 2, "完成車")
            Else
                xls.SetValue(KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, dataStartRowIndex + 2, "ホワイトボディ")
            End If
            xls.MergeCells(KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, dataStartRowIndex + 2, KANSEI_TUKURIKATA_PATTERNNAME_COLUMN_INDEX, dataStartRowIndex + 2, True)
            xls.SetBackColor(KANSEI_TUKURIKATA_UNITKBN_COLUMN_INDEX, dataStartRowIndex + 2, KANSEI_TUKURIKATA_PATTERNNAME_COLUMN_INDEX, dataStartRowIndex + 2, RGB(255, 255, 153))
            xls.SetValue(KANSEI_BUHIN_HI_COLUMN_INDEX, dataStartRowIndex + 2, "部品費（円）")
            xls.SetValue(KANSEI_KATA_HI_COLUMN_INDEX, dataStartRowIndex + 2, "型費（千円）")
            xls.SetValue(DAISU_COLUMN_INDEX, dataStartRowIndex + 2, "台数")
            xls.SetBackColor(KANSEI_BUHIN_HI_COLUMN_INDEX, dataStartRowIndex + 2, DAISU_COLUMN_INDEX, dataStartRowIndex + 2, RGB(204, 255, 255))

        End Sub
#End Region

#Region "金材spread出力 "
        ''' <summary>
        ''' Excel出力 金材　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <remarks></remarks>
        Public Sub setYosanKanazaiSheetBody(ByVal xls As ShisakuExcel)
            Dim sheet As SheetView = _view.spdKanazai_Sheet1

            'ヘッダー出力
            setYosanKanazaiTitleRow(xls)

            Dim excelRowIndex As Integer = dataStartRowIndex + 1
            Dim colCount As Integer = 0
            'から終わりまで行
            For rowIndex As Integer = KANAZAI_ROW_START_INDEX - 2 To sheet.RowCount - 2
                Dim excelColIndex As Integer = KANAZAI_MONTH_COLUMNS_START_INDEX
                'から終わりまで列
                For columnIndex As Integer = SPD_KANAZAI_DAISU_COLUMN_START_INDEX To sheet.ColumnCount - 1
                    ''出力データを遍歴する
                    If rowIndex >= KANAZAI_ROW_START_INDEX Then
                        xls.SetValue(excelColIndex, excelRowIndex, Format(sheet.Cells(rowIndex, columnIndex).Value, "#,##0"))
                    Else
                        xls.SetValue(excelColIndex, excelRowIndex, sheet.Cells(rowIndex, columnIndex).Value)
                    End If
                    'セルの結合
                    If excelColIndex Mod HALF_MONTH_DEFAULT_COUNT = 0 Then
                        '合併年列
                        xls.MergeCells(excelColIndex - HALF_MONTH_DEFAULT_COUNT + 1, dataStartRowIndex + 1, excelColIndex, dataStartRowIndex + 1, True)
                        columnIndex = columnIndex + 1
                    End If

                    excelColIndex = excelColIndex + 1
                Next
                If rowIndex = KANAZAI_ROW_START_INDEX - 2 Then
                    '出力した列数
                    colCount = excelColIndex - 1
                End If
                If rowIndex >= KANAZAI_ROW_START_INDEX Then
                    '金材名
                    xls.SetValue(KANAZAI_KANAZAINAME_COLUMN_INDEX, excelRowIndex, sheet.Cells(rowIndex, SPD_KANAZAI_KANAZAINAME_COLUMN_INDEX).Value)
                    xls.MergeCells(KANAZAI_KANAZAINAME_COLUMN_INDEX, excelRowIndex, KANAZAI_KANAZAINAME_COLUMN_INDEX + 1, excelRowIndex, True)
                    '台数
                    xls.SetValue(KANAZAI_DAISU_COLUMN_INDEX, excelRowIndex, Format(sheet.Cells(rowIndex, SPD_KANAZAI_DAISU_COLUMN_INDEX).Value, "#,##0") & " 台")
                    '単価
                    xls.SetValue(KANAZAI_TANKA_COLUMN_INDEX, excelRowIndex, sheet.Cells(rowIndex, SPD_KANAZAI_TANKA_COLUMN_INDEX).Value)
                    '計
                    xls.SetValue(KANAZAI_KEI_COLUMN_INDEX, excelRowIndex, sheet.Cells(rowIndex, SPD_KANAZAI_KEI_COLUMN_INDEX).Value)
                End If

                excelRowIndex = excelRowIndex + 1
            Next

            '月分布を設定
            xls.SetValue(KANAZAI_MONTH_COLUMNS_START_INDEX, dataStartRowIndex, "月分布")
            '合併月分布の列
            xls.MergeCells(KANAZAI_MONTH_COLUMNS_START_INDEX, dataStartRowIndex, colCount - 1, dataStartRowIndex, True)
            '月分布背景色
            xls.SetBackColor(KANAZAI_MONTH_COLUMNS_START_INDEX, dataStartRowIndex, colCount - 1, dataStartRowIndex + 2, RGB(255, 255, 153))
            '表に加線
            xls.SetLine(KANAZAI_KANAZAINAME_COLUMN_INDEX, dataStartRowIndex, colCount - 1, excelRowIndex - 1)
            '設置テキストに表示
            xls.SetAlignment(KANAZAI_DAISU_COLUMN_INDEX, dataStartRowIndex + 3, colCount - 1, excelRowIndex - 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            '#,##0
            xls.SetNumberFormatLocal(KANAZAI_TANKA_COLUMN_INDEX, dataStartRowIndex + 3, KANAZAI_KEI_COLUMN_INDEX, excelRowIndex - 1, "#,##0;@")
        End Sub

        ''' <summary>
        ''' タイトル行の作成
        ''' </summary>
        ''' <param name="xls">目的のEXCELファイル</param>
        ''' <remarks></remarks>
        Private Sub setYosanKanazaiTitleRow(ByVal xls As ShisakuExcel)

            'タイトル部分の作成'
            xls.SetValue(KANAZAI_KANAZAINAME_COLUMN_INDEX, dataStartRowIndex + 1, "≪金材≫")
            xls.MergeCells(KANAZAI_KANAZAINAME_COLUMN_INDEX, dataStartRowIndex, KANAZAI_KEI_COLUMN_INDEX, dataStartRowIndex + 1, True)
            xls.MergeCells(KANAZAI_KANAZAINAME_COLUMN_INDEX, dataStartRowIndex + 2, KANAZAI_KANAZAINAME_COLUMN_INDEX + 1, dataStartRowIndex + 2, True)
            xls.SetValue(KANAZAI_DAISU_COLUMN_INDEX, dataStartRowIndex + 2, "台数")
            xls.SetBackColor(KANAZAI_DAISU_COLUMN_INDEX, dataStartRowIndex + 2, KANAZAI_DAISU_COLUMN_INDEX, dataStartRowIndex + 2, RGB(204, 255, 255))
            xls.SetValue(KANAZAI_TANKA_COLUMN_INDEX, dataStartRowIndex + 2, "単価")
            xls.SetBackColor(KANAZAI_TANKA_COLUMN_INDEX, dataStartRowIndex + 2, KANAZAI_TANKA_COLUMN_INDEX, dataStartRowIndex + 2, RGB(255, 255, 153))
            xls.SetValue(KANAZAI_KEI_COLUMN_INDEX, dataStartRowIndex + 2, "計")
            xls.SetBackColor(KANAZAI_KEI_COLUMN_INDEX, dataStartRowIndex + 2, KANAZAI_KEI_COLUMN_INDEX, dataStartRowIndex + 2, RGB(204, 255, 255))

        End Sub
#End Region

#End Region

#Region "Sheet3 "
        ''' <summary>
        ''' Excel出力　メタル部品表
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="yosanBuhinEditVos">メタル部品表</param>
        ''' <remarks></remarks>
        Private Sub setYosanMetalSheet(ByVal xls As ShisakuExcel, _
                                       ByVal yosanBuhinEditVos As List(Of TYosanBuhinEditVo), ByVal instlVosByHyojijun As Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo)), ByVal yosanBuhinEditPatternVos As List(Of TYosanBuhinEditPatternVo))
            xls.SetActiveSheet(3)
            'Sheet名を設定
            xls.SetSheetName("Excel出力メタル部品表")
            '出力メタル部品表各カラム名に数値を設定
            SetYosanBuhinColumnNo(yosanBuhinEditPatternVos)
            '出力メタル部品表情報を設定
            setYosanBuhinSheetBody(xls, yosanBuhinEditVos, instlVosByHyojijun, yosanBuhinEditPatternVos)

        End Sub
#End Region

#Region "Sheet4 "
        ''' <summary>
        ''' Excel出力　トリム部品表
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="yosanBuhinEditVos">トリム部品表</param>
        ''' <remarks></remarks>
        Private Sub setYosanTorimu(ByVal xls As ShisakuExcel, _
                                   ByVal yosanBuhinEditVos As List(Of TYosanBuhinEditVo), ByVal instlVosByHyojijun As Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo)), ByVal yosanBuhinEditPatternVos As List(Of TYosanBuhinEditPatternVo))
            'Sheet名を設定
            xls.AddSheet("Excel出力トリム部品表")
            '出力メタル部品表各カラム名に数値を設定
            SetYosanBuhinColumnNo(yosanBuhinEditPatternVos)
            '出力メタル部品表情報を設定
            setYosanBuhinSheetBody(xls, yosanBuhinEditVos, instlVosByHyojijun, yosanBuhinEditPatternVos)

        End Sub
#End Region

#Region "Sheet3/Sheet4共同"
        ''' <summary>
        ''' 各カラム名に数値を設定
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetYosanBuhinColumnNo(ByVal yosanBuhinEditPatternVos As List(Of TYosanBuhinEditPatternVo))
            Dim column As Integer = 1
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

        End Sub

        ''' <summary>
        ''' Excel出力　シートのBodyの部分
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="yosanBuhinEditVos">予算書イベント情報</param>
        ''' <remarks></remarks>
        Public Sub setYosanBuhinSheetBody(ByVal xls As ShisakuExcel, ByVal yosanBuhinEditVos As List(Of TYosanBuhinEditVo), ByVal instlVosByHyojijun As Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo)), ByVal yosanBuhinEditPatternVos As List(Of TYosanBuhinEditPatternVo))

            'タイトル部分の作成'
            setYosanBuhinTitleRow(xls)

            Dim rowCount As Integer = yosanBuhinEditVos.Count

            If rowCount <= 0 Then
                '員数ヘッダー
                xls.SetValue(COLUMN_INSU, BUHIN_TITLE_ROW - 1, "員数")
                xls.SetAlignment(COLUMN_INSU, BUHIN_TITLE_ROW - 1, COLUMN_INSU, BUHIN_TITLE_ROW - 1, XlVAlign.xlVAlignCenter, XlVAlign.xlVAlignCenter)
                xls.SetBackColor(COLUMN_INSU, BUHIN_TITLE_ROW - 1, COLUMN_INSU, BUHIN_TITLE_ROW - 1, RGB(192, 192, 192))
                xls.SetLine(COLUMN_INSU, BUHIN_TITLE_ROW - 1, COLUMN_INSU, BUHIN_TITLE_ROW - 1)
                '列の幅を自動調整する
                xls.AutoFitCol(COLUMN_BUKA_CODE, COLUMN_HACHU_JISEKI_MIX)

                Exit Sub
            End If

            Dim excelRowIndex As Integer = 0
            For index = 0 To rowCount - 1
                Dim tempyosanBuhinEditVo As TYosanBuhinEditVo = yosanBuhinEditVos(index)
                excelRowIndex = BUHIN_DATA_ROW + index

                '設計課'
                xls.SetValue(COLUMN_BUKA_CODE, excelRowIndex, COLUMN_BUKA_CODE, excelRowIndex, tempyosanBuhinEditVo.YosanBukaCode)
                'ブロック'
                xls.SetValue(COLUMN_BLOCK_NO, excelRowIndex, COLUMN_BLOCK_NO, excelRowIndex, tempyosanBuhinEditVo.YosanBlockNo)
                'レベル'
                xls.SetValue(COLUMN_LEVEL, excelRowIndex, COLUMN_LEVEL, excelRowIndex, tempyosanBuhinEditVo.YosanLevel)
                '国内集計
                xls.SetValue(COLUMN_SHUKEI_CODE, excelRowIndex, COLUMN_SHUKEI_CODE, excelRowIndex, tempyosanBuhinEditVo.YosanShukeiCode)
                '海外集計
                xls.SetValue(COLUMN_SIA_SHUKEI_CODE, excelRowIndex, COLUMN_SIA_SHUKEI_CODE, excelRowIndex, tempyosanBuhinEditVo.YosanSiaShukeiCode)
                '取引先コード
                xls.SetValue(COLUMN_MAKER_CODE, excelRowIndex, COLUMN_MAKER_CODE, excelRowIndex, tempyosanBuhinEditVo.YosanMakerCode)
                '取引先名称
                xls.SetValue(COLUMN_MAKER_NAME, excelRowIndex, COLUMN_MAKER_NAME, excelRowIndex, tempyosanBuhinEditVo.YosanMakerName)
                '部品番号'
                xls.SetValue(COLUMN_BUHIN_NO, excelRowIndex, COLUMN_BUHIN_NO, excelRowIndex, tempyosanBuhinEditVo.YosanBuhinNo)
                '部品番号試作区分
                xls.SetValue(COLUMN_BUHIN_NO_KBN, excelRowIndex, COLUMN_BUHIN_NO_KBN, excelRowIndex, tempyosanBuhinEditVo.YosanBuhinNoKbn)
                '部品名称
                xls.SetValue(COLUMN_BUHIN_NAME, excelRowIndex, COLUMN_BUHIN_NAME, excelRowIndex, tempyosanBuhinEditVo.YosanBuhinName)
                '供給セクション
                xls.SetValue(COLUMN_KYOUKU_SECTION, excelRowIndex, COLUMN_KYOUKU_SECTION, excelRowIndex, tempyosanBuhinEditVo.YosanKyoukuSection)
                '員数'
                xls.SetValue(COLUMN_INSU, excelRowIndex, COLUMN_INSU, excelRowIndex, tempyosanBuhinEditVo.YosanInsu)
                '変更概要
                xls.SetValue(COLUMN_HENKO_GAIYO, excelRowIndex, COLUMN_HENKO_GAIYO, excelRowIndex, tempyosanBuhinEditVo.YosanHenkoGaiyo)
                '部品費（量産）'
                xls.SetValue(COLUMN_BUHIN_HI_RYOSAN, excelRowIndex, COLUMN_BUHIN_HI_RYOSAN, excelRowIndex, tempyosanBuhinEditVo.YosanBuhinHiRyosan)
                '部品費（部品表）'
                xls.SetValue(COLUMN_BUHIN_HI_BUHINHYO, excelRowIndex, COLUMN_BUHIN_HI_BUHINHYO, excelRowIndex, tempyosanBuhinEditVo.YosanBuhinHiBuhinhyo)
                '部品費（特記）'
                xls.SetValue(COLUMN_BUHIN_HI_TOKKI, excelRowIndex, COLUMN_BUHIN_HI_TOKKI, excelRowIndex, tempyosanBuhinEditVo.YosanBuhinHiTokki)
                '型費'
                xls.SetValue(COLUMN_KATA_HI, excelRowIndex, COLUMN_KATA_HI, excelRowIndex, tempyosanBuhinEditVo.YosanKataHi)
                '治具費'
                xls.SetValue(COLUMN_JIGU_HI, excelRowIndex, COLUMN_JIGU_HI, excelRowIndex, tempyosanBuhinEditVo.YosanJiguHi)
                '工数'
                xls.SetValue(COLUMN_KOSU, excelRowIndex, COLUMN_KOSU, excelRowIndex, tempyosanBuhinEditVo.YosanKosu)
                '発注実績(割付&MIX値全体と平均値)
                xls.SetValue(COLUMN_HACHU_JISEKI_MIX, excelRowIndex, COLUMN_HACHU_JISEKI_MIX, excelRowIndex, tempyosanBuhinEditVo.YosanHachuJisekiMix)
                '員数'
                For insuIdx As Integer = 0 To instlVosByHyojijun.Item(tempyosanBuhinEditVo.BuhinNoHyoujiJun).Count - 1
                    Dim hyoujiJun As Integer = instlVosByHyojijun.Item(tempyosanBuhinEditVo.BuhinNoHyoujiJun)(insuIdx).PatternHyoujiJun
                    If instlVosByHyojijun.Item(tempyosanBuhinEditVo.BuhinNoHyoujiJun)(insuIdx).InsuSuryo = -1 Then
                        xls.SetValue(COLUMN_INSU + hyoujiJun, excelRowIndex, "**")
                    Else
                        xls.SetValue(COLUMN_INSU + hyoujiJun, excelRowIndex, instlVosByHyojijun.Item(tempyosanBuhinEditVo.BuhinNoHyoujiJun)(insuIdx).InsuSuryo)
                    End If
                Next
            Next
            Dim patternCount As Integer = yosanBuhinEditPatternVos.Count
            Dim endInsuColumn As Integer = COLUMN_INSU + patternCount - 1
            'パターン名ヘッダー
            For patternIdx As Integer = 0 To patternCount - 1
                xls.SetValue(COLUMN_INSU + patternIdx, BUHIN_TITLE_ROW, yosanBuhinEditPatternVos(patternIdx).PatternName)
            Next
            xls.SetOrientation(COLUMN_INSU, BUHIN_TITLE_ROW, endInsuColumn, BUHIN_TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)
            xls.SetAlignment(COLUMN_INSU, BUHIN_TITLE_ROW, endInsuColumn, BUHIN_TITLE_ROW, XlVAlign.xlVAlignCenter, XlVAlign.xlVAlignCenter)
            '員数ヘッダー
            xls.SetValue(COLUMN_INSU, BUHIN_TITLE_ROW - 1, "員数")
            xls.SetAlignment(COLUMN_INSU, BUHIN_TITLE_ROW - 1, endInsuColumn, BUHIN_TITLE_ROW - 1, XlVAlign.xlVAlignCenter, XlVAlign.xlVAlignCenter)
            xls.SetBackColor(COLUMN_INSU, BUHIN_TITLE_ROW - 1, endInsuColumn, BUHIN_TITLE_ROW - 1, RGB(192, 192, 192))
            xls.SetLine(COLUMN_INSU, BUHIN_TITLE_ROW - 1, endInsuColumn, BUHIN_TITLE_ROW - 1)
            xls.MergeCells(COLUMN_INSU, BUHIN_TITLE_ROW - 1, endInsuColumn, BUHIN_TITLE_ROW - 1, True)
  
            '表に加線
            xls.SetLine(COLUMN_BUKA_CODE, BUHIN_DATA_ROW, COLUMN_HACHU_JISEKI_MIX, rowCount + BUHIN_DATA_ROW - 1)
            xls.SetLine(COLUMN_BUKA_CODE, BUHIN_DATA_ROW, COLUMN_HACHU_JISEKI_MIX, rowCount + BUHIN_DATA_ROW - 1, XlBordersIndex.xlInsideHorizontal, XlLineStyle.xlDot, XlBorderWeight.xlThin)
            '設置テキストに表示
            xls.SetAlignment(COLUMN_LEVEL, BUHIN_DATA_ROW, COLUMN_LEVEL, rowCount + BUHIN_DATA_ROW - 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            xls.SetAlignment(COLUMN_INSU, BUHIN_DATA_ROW, endInsuColumn, rowCount + BUHIN_DATA_ROW - 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            xls.SetAlignment(COLUMN_BUHIN_HI_RYOSAN, BUHIN_DATA_ROW, COLUMN_HACHU_JISEKI_MIX, rowCount + BUHIN_DATA_ROW - 1, XlHAlign.xlHAlignRight, XlVAlign.xlVAlignCenter)
            '#,##0
            xls.SetNumberFormatLocal(COLUMN_BUHIN_HI_RYOSAN, BUHIN_DATA_ROW, COLUMN_HACHU_JISEKI_MIX, rowCount + BUHIN_DATA_ROW - 1, "#,##0;@")

            '列の幅を自動調整する
            xls.AutoFitCol(COLUMN_BUKA_CODE, COLUMN_HACHU_JISEKI_MIX)
            '員数列幅
            xls.SetColWidth(COLUMN_INSU, endInsuColumn, 4)
        End Sub

        ''' <summary>
        ''' タイトル行の作成
        ''' </summary>
        ''' <param name="xls">目的のEXCELファイル</param>
        ''' <remarks></remarks>
        Private Sub setYosanBuhinTitleRow(ByVal xls As ShisakuExcel)

            xls.SetValue(COLUMN_BUKA_CODE, BUHIN_TITLE_ROW, "設計課")
            xls.SetValue(COLUMN_BLOCK_NO, BUHIN_TITLE_ROW, "ブロック")
            xls.SetValue(COLUMN_LEVEL, BUHIN_TITLE_ROW, "レベル")
            xls.SetValue(COLUMN_SHUKEI_CODE, BUHIN_TITLE_ROW, "国内集計")
            xls.SetValue(COLUMN_SIA_SHUKEI_CODE, BUHIN_TITLE_ROW, "海外集計")
            xls.SetValue(COLUMN_MAKER_CODE, BUHIN_TITLE_ROW, "取引先コード")
            xls.SetValue(COLUMN_MAKER_NAME, BUHIN_TITLE_ROW, "取引先名称")
            xls.SetValue(COLUMN_BUHIN_NO, BUHIN_TITLE_ROW, "部品番号")
            xls.SetValue(COLUMN_BUHIN_NO_KBN, BUHIN_TITLE_ROW, "部品番号試作区分")
            xls.SetValue(COLUMN_BUHIN_NAME, BUHIN_TITLE_ROW, "部品名称")
            xls.SetValue(COLUMN_KYOUKU_SECTION, BUHIN_TITLE_ROW, "供給セクション")
            xls.SetValue(COLUMN_INSU, BUHIN_TITLE_ROW - 1, "員数")
            xls.SetValue(COLUMN_HENKO_GAIYO, BUHIN_TITLE_ROW, "変更概要")
            xls.SetValue(COLUMN_BUHIN_HI_RYOSAN, BUHIN_TITLE_ROW, "部品費（量産）")
            xls.SetValue(COLUMN_BUHIN_HI_BUHINHYO, BUHIN_TITLE_ROW, "部品費（部品表）")
            xls.SetValue(COLUMN_BUHIN_HI_TOKKI, BUHIN_TITLE_ROW, "部品費（特記）")
            xls.SetValue(COLUMN_KATA_HI, BUHIN_TITLE_ROW, "型費")
            xls.SetValue(COLUMN_JIGU_HI, BUHIN_TITLE_ROW, "治具費")
            xls.SetValue(COLUMN_KOSU, BUHIN_TITLE_ROW, "工数")
            xls.SetValue(COLUMN_HACHU_JISEKI_MIX, BUHIN_TITLE_ROW, "発注実績(割付&MIX値全体と平均値)")

            '設計課～取引先名称ヘッダー
            xls.SetOrientation(COLUMN_BUKA_CODE, BUHIN_TITLE_ROW, COLUMN_MAKER_CODE, BUHIN_TITLE_ROW, ShisakuExcel.XlOrientation.xlVertical)

            xls.SetBackColor(COLUMN_BUKA_CODE, BUHIN_TITLE_ROW, COLUMN_HACHU_JISEKI_MIX, 2, RGB(192, 192, 192))
            xls.SetAlignment(COLUMN_BUKA_CODE, BUHIN_TITLE_ROW, COLUMN_HACHU_JISEKI_MIX, BUHIN_TITLE_ROW, XlVAlign.xlVAlignCenter, XlVAlign.xlVAlignCenter)
            xls.SetLine(COLUMN_BUKA_CODE, BUHIN_TITLE_ROW, COLUMN_HACHU_JISEKI_MIX, BUHIN_TITLE_ROW)
        End Sub
#End Region

#Region "Sheet部品表名 "
        ''' <summary>
        ''' Excel出力　部品表
        ''' </summary>
        ''' <param name="xls">目的Excelファイル</param>
        ''' <param name="yosanBuhinEditVos">部品表</param>
        ''' <param name="yosanUnitKbn">部品表名</param>
        ''' <remarks></remarks>
        Private Sub setYosanBuhinSheet(ByVal xls As ShisakuExcel, _
                                       ByVal yosanBuhinEditVos As List(Of TYosanBuhinEditVo), _
                                       ByVal instlVosByHyojijun As Dictionary(Of Integer, List(Of TYosanBuhinEditInsuVo)), _
                                       ByVal yosanBuhinEditPatternVos As List(Of TYosanBuhinEditPatternVo), _
                                       ByVal yosanUnitKbn As String)
            'Sheet名を設定
            xls.AddSheet("Excel出力" & yosanUnitKbn & "部品表")
            '出力 部品表各カラム名に数値を設定
            SetYosanBuhinColumnNo(yosanBuhinEditPatternVos)
            '出力 部品表情報を設定
            setYosanBuhinSheetBody(xls, yosanBuhinEditVos, instlVosByHyojijun, yosanBuhinEditPatternVos)

        End Sub
#End Region


    End Class

End Namespace