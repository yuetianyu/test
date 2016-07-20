Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Ui.Access
Imports FarPoint.Win.Spread.CellType
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports FarPoint.Win.Spread.Model
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports FarPoint.Win.Spread
Imports EventSakusei.ShisakuBuhinEdit.Ui
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util.OptionFilter
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl


Namespace ShisakuBuhinEdit.Kosei.Ui
    ''' <summary>
    ''' 部品構成編集画面のSpreadに対する全般処理を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpdKoseiObserver : Implements Observer

        ''' <summary>自給品の表示非表示判断用</summary>
        Public Shared SPREAD_JIKYU As String = Nothing

        ''' <summary>構成追加行と追加数</summary>
        Public Shared SPREAD_ROW As Integer = 0
        Public Shared SPREAD_ROWCOUNT As Integer = 0

        '2014/10/06 追加 E.Ubukata
        ''' <summary>
        ''' Observerによるデータ変更フラグ
        ''' 　ペースト時にObserverによるデータクリアかどうか判定するために使用する
        ''' </summary>
        Public ObserverUpdating As Boolean = False


#Region "各列のTAG"
        Private Const TAG_LEVEL As String = "LEVEL"
        '↓↓2014/10/06 酒井 ADD BEGIN
        '補用品不具合展開
        '        Private Const TAG_SHUKEI_CODE As String = "SHUKEI_CODE"
        Public Const TAG_SHUKEI_CODE As String = "SHUKEI_CODE"
        '        Private Const TAG_SIA_SHUKEI_CODE As String = "SIA_SHUKEI_CODE"
        Public Const TAG_SIA_SHUKEI_CODE As String = "SIA_SHUKEI_CODE"
        '        Private Const TAG_GENCYO_CKD_KBN As String = "GENCYO_CKD_KBN"
        Public Const TAG_GENCYO_CKD_KBN As String = "GENCYO_CKD_KBN"
        'Private Const TAG_MAKER_CODE As String = "MAKER_CODE"
        Public Const TAG_MAKER_CODE As String = "MAKER_CODE"
        Public Const TAG_MAKER_NAME As String = "MAKER_NAME"
        'Private Const TAG_BUHIN_NO As String = "BUHIN_NO"
        Public Const TAG_BUHIN_NO As String = "BUHIN_NO"
        Private Const TAG_BUHIN_NO_KBN As String = "BUHIN_NO_KBN"
        Private Const TAG_BUHIN_NO_KAITEI_NO As String = "BUHIN_NO_KAITEI_NO"
        Private Const TAG_EDA_BAN As String = "EDA_BAN"
        'Private Const TAG_BUHIN_NAME As String = "BUHIN_NAME"
        Public Const TAG_BUHIN_NAME As String = "BUHIN_NAME"
        Private Const TAG_SAISHIYOUFUKA As String = "SAISHIYOUFUKA"
        '2012/01/23 供給セクション追加
        'Private Const TAG_KYOUKU_SECTION As String = "KYOUKU_SECTION"
        Public Const TAG_KYOUKU_SECTION As String = "KYOUKU_SECTION"
        Private Const TAG_SHUTUZU_YOTEI_DATE As String = "SHUTUZU_YOTEI_DATE"
        Private Const TAG_ZAISHITU_KIKAKU1 As String = "ZAISHITU_KIKAKU1"
        Private Const TAG_ZAISHITU_KIKAKU2 As String = "ZAISHITU_KIKAKU2"
        Private Const TAG_ZAISHITU_KIKAKU3 As String = "ZAISHITU_KIKAKU3"
        Private Const TAG_ZAISHITU_MEKKI As String = "ZAISHITU_MEKKI"

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_l) (TES)張 ADD BEGIN
        Public Const TAG_TSUKURIKATA_SEISAKU As String = "TAG_TSUKURIKATA_SEISAKU"
        Public Const TAG_TSUKURIKATA_KATASHIYOU1 As String = "TAG_TSUKURIKATA_KATASHIYOU1"
        Public Const TAG_TSUKURIKATA_KATASHIYOU2 As String = "TAG_TSUKURIKATA_KATASHIYOU2"
        Public Const TAG_TSUKURIKATA_KATASHIYOU3 As String = "TAG_TSUKURIKATA_KATASHIYOU3"
        Private Const TAG_TSUKURIKATA_TIGU As String = "TAG_TSUKURIKATA_TIGU"
        Private Const TAG_TSUKURIKATA_NOUNYU As String = "TAG_TSUKURIKATA_NOUNYU"
        Public Const TAG_TSUKURIKATA_KIBO As String = "TAG_TSUKURIKATA_KIBO"
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_l) (TES)張 ADD END

        Private Const TAG_SHISAKU_BANKO_SURYO As String = "SHISAKU_BANKO_SURYO"
        Private Const TAG_SHISAKU_BANKO_SURYO_U As String = "SHISAKU_BANKO_SURYO_U"

        '↓↓↓2014/12/24 メタル項目を追加 TES)張 ADD BEGIN
        Public Const TAG_MATERIAL_INFO_LENGTH As String = "TAG_MATERIAL_INFO_LENGTH"
        Public Const TAG_MATERIAL_INFO_WIDTH As String = "TAG_MATERIAL_INFO_WIDTH"
        Public Const TAG_DATA_ITEM_KAITEI_NO As String = "TAG_DATA_ITEM_KAITEI_NO"
        Public Const TAG_DATA_ITEM_AREA_NAME As String = "TAG_DATA_ITEM_AREA_NAME"
        Public Const TAG_DATA_ITEM_SET_NAME As String = "TAG_DATA_ITEM_SET_NAME"
        Public Const TAG_DATA_ITEM_KAITEI_INFO As String = "TAG_DATA_ITEM_KAITEI_INFO"
        '↑↑↑2014/12/24 メタル項目を追加 TES)張 ADD END

        Private Const TAG_SHISAKU_BUHIN_HI As String = "SHISAKU_BUHIN_HI"
        Private Const TAG_SHISAKU_KATA_HI As String = "SHISAKU_KATA_HI"
        Public Const TAG_BIKOU As String = "BIKOU"
        '2012/01/25 部品ノート追加
        Public Const TAG_BUHIN_NOTE As String = "BUHIN_NOTE"
#End Region
        Private BASE_COLOR As System.Drawing.Color = Color.DarkGray
        Private BASE_ForeCOLOR As System.Drawing.Color = Color.Black

        Private Shared ReadOnly DEFAULT_LOCK_TAGS As String() = {TAG_BUHIN_NO_KBN}

        ''' <summary>INSTL列の初期表示列数</summary>
        Public Const DEFAULT_INSTL_COLUMN_COUNT As Integer = 14    ' とりあえず、デモと同じ14にしておく

        Private Const TITLE_ROW_INDEX As Integer = -1
        Private Const INSTL_INFO_START_COLUMN As Integer = 11

        Private Const ALL_MIDASHI_ROW_INDEX As Integer = 0
        Private Const INSTL_HINBAN_MIDASHI_ROW_INDEX As Integer = 0
        Private Const INSTL_HINBAN_NO_ROW_INDEX As Integer = 1
        Private Const INSTL_DATA_KBN_ROW_INDEX As Integer = 2
        Private Const INSTL_HINBAN_ROW_INDEX As Integer = 3
        Private Const INSTL_HINBAN_KBN_ROW_INDEX As Integer = 4
        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        Private ReadOnly subject As BuhinEditKoseiSubject
        Private ReadOnly titleRows As Integer

        Private validatorRegister As SpreadValidator
        Private validatorRegisterWarning As SpreadValidator
        Private validatorRegiserShukei As SpreadValidator
        Private validatorRegiserSiaShukei As SpreadValidator
        Private validatorSave As SpreadValidator
        '構成用Validator'
        Private validatorKosei As SpreadValidator

        Private cellTypeFactory As New SpdKoseiCellTypeFactory
        Private borderFactory As New SpreadBorderFactory
        Private anInstlColumns As InstlColumns

        Private IsSuspend_SpreadChangeEvent As Boolean
        '直前の員数を保持する配列'
        Private InsuArray()() As Integer

        'フィルタ条件のリスト'
        Private condVoList As List(Of clsOptionFilterVo)

        Private toolTip As SpreadToolTip
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="spread">部品表Spread</param>
        ''' <param name="subject">部品構成Subject</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal spread As FpSpread, ByVal subject As BuhinEditKoseiSubject)
            Me.spread = spread
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)
            Me.sheet.SheetName = "KOSEI"
            Me.subject = subject
            condVoList = New List(Of clsOptionFilterVo)

            Me.titleRows = BuhinEditSpreadUtil.GetTitleRows(sheet)

            InsuArray = New Integer(subject.GetInputRowIndexes.Count + 100)() {}

            Me.toolTip = New SpreadToolTip(spread)

            subject.AddObserver(Me)
        End Sub

#Region "各CellType"
        Private TateTitleCellType As TextCellType
        Private InstlHinbanMidashiCellType As TextCellType
        Private InstlHinbanNoCellType As TextCellType
        Private InstlHinbanCellType As TextCellType
        Private InstlKbnCellType As TextCellType
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(4)) (TES)張 ADD BEGIN
        Private InstlDataKbnCellType As TextCellType
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(4)) (TES)張 ADD END
        ''' <summary>
        ''' 縦書き表示タイトルセルを返す
        ''' </summary>
        ''' <returns>縦書き表示タイトルセル</returns>
        ''' <remarks></remarks>
        Public Function GetTateTitleCellType() As TextCellType
            If TateTitleCellType Is Nothing Then

                TateTitleCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                TateTitleCellType.MaxLength = 100
                TateTitleCellType.TextOrientation = FarPoint.Win.TextOrientation.TextTopDown
            End If
            Return TateTitleCellType
        End Function
        ''' <summary>
        ''' INSTL品番見出しセルを返す
        ''' </summary>
        ''' <returns>INSTL品番見出しセル</returns>
        ''' <remarks></remarks>
        Public Function GetInstlHinbanMidashiCellType() As TextCellType
            If InstlHinbanMidashiCellType Is Nothing Then

                InstlHinbanMidashiCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                InstlHinbanMidashiCellType.MaxLength = 100
            End If
            Return InstlHinbanMidashiCellType
        End Function
        ''' <summary>
        ''' INSTL品番Noセルを返す
        ''' </summary>
        ''' <returns>INSTL品番Noセル</returns>
        ''' <remarks></remarks>
        Public Function GetInstlHinbanNoCellType() As TextCellType
            If InstlHinbanNoCellType Is Nothing Then

                InstlHinbanNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                InstlHinbanNoCellType.MaxLength = 3
            End If
            Return InstlHinbanNoCellType
        End Function
        ''' <summary>
        ''' INSTL品番セルを返す
        ''' </summary>
        ''' <returns>INSTL品番セル</returns>
        ''' <remarks></remarks>
        Public Function GetInstlHinbanCellType() As TextCellType
            If InstlHinbanCellType Is Nothing Then

                InstlHinbanCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                InstlHinbanCellType.MaxLength = 122
                InstlHinbanCellType.TextOrientation = FarPoint.Win.TextOrientation.TextTopDown
            End If
            Return InstlHinbanCellType
        End Function
        ''' <summary>
        ''' INSTL品番試作区分セルを返す
        ''' </summary>
        ''' <returns>INSTL品番試作区分セル</returns>
        ''' <remarks></remarks>
        Public Function GetInstlKbnCellType() As TextCellType
            If InstlKbnCellType Is Nothing Then

                InstlKbnCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                InstlKbnCellType.MaxLength = 5
                InstlKbnCellType.TextOrientation = FarPoint.Win.TextOrientation.TextTopDown
            End If
            Return InstlKbnCellType
        End Function
        ''' <summary>
        ''' INSTLデータ区分セルを返す
        ''' </summary>
        ''' <returns>INSTLデータ区分セル</returns>
        ''' <remarks></remarks>
        Public Function GetInstlDataKbnCellType() As TextCellType
            If InstlDataKbnCellType Is Nothing Then

                InstlDataKbnCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(6)) (TES)張 CHG BEGIN
                InstlDataKbnCellType.MaxLength = 2
                ''↑↑2014/08/21  1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(6)) (TES)張 CHG END
            End If
            Return InstlDataKbnCellType
        End Function
#End Region

        ''' <summary>
        ''' 初期化する（一度だけ実行する事を想定）
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Initialize()

            ' デモ用の「INSTL品番」列を除去
            Dim demoInstlColumnCount As Integer = 14
            RemoveColumns(INSTL_INFO_START_COLUMN, demoInstlColumnCount)

            BuhinEditSpreadUtil.InitializeFrm41(spread)

            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_LEVEL
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUKEI_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SIA_SHUKEI_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GENCYO_CKD_KBN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MAKER_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MAKER_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO_KBN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EDA_BAN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SAISHIYOUFUKA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KYOUKU_SECTION
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUTUZU_YOTEI_DATE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_SEISAKU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_KATASHIYOU1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_KATASHIYOU2
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_KATASHIYOU3
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_TIGU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_NOUNYU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TSUKURIKATA_KIBO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAISHITU_KIKAKU1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAISHITU_KIKAKU2
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAISHITU_KIKAKU3
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_ZAISHITU_MEKKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BANKO_SURYO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BANKO_SURYO_U

            '↓↓↓2014/12/24 メタル項目を追加 TES)張 ADD BEGIN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MATERIAL_INFO_LENGTH
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MATERIAL_INFO_WIDTH
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_AREA_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_SET_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_KAITEI_INFO
            '↑↑↑2014/12/24 メタル項目を追加 TES)張 ADD END

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NOTE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BIKOU

            ' *** 行に設定 ***
            With sheet.Rows(ALL_MIDASHI_ROW_INDEX)
                .BackColor = System.Drawing.SystemColors.Control
                .Locked = True
            End With
            With sheet.Rows(INSTL_HINBAN_NO_ROW_INDEX)
                .BackColor = System.Drawing.SystemColors.Control
                .Locked = True
            End With
            With sheet.Rows(INSTL_HINBAN_ROW_INDEX)
                .BackColor = Color.White
                .Locked = True
            End With
            With sheet.Rows(INSTL_HINBAN_KBN_ROW_INDEX)
                .BackColor = Color.White
                .Locked = True
            End With
            With sheet.Rows(INSTL_DATA_KBN_ROW_INDEX)
                .BackColor = System.Drawing.SystemColors.Control
                .Locked = True
            End With
            sheet.Rows(INSTL_HINBAN_MIDASHI_ROW_INDEX).CellType = GetInstlHinbanMidashiCellType()
            sheet.Rows(INSTL_HINBAN_NO_ROW_INDEX).CellType = GetInstlHinbanNoCellType()
            sheet.Rows(INSTL_HINBAN_ROW_INDEX).CellType = GetInstlHinbanCellType()
            sheet.Rows(INSTL_HINBAN_KBN_ROW_INDEX).CellType = GetInstlKbnCellType()
            sheet.Rows(INSTL_DATA_KBN_ROW_INDEX).CellType = GetInstlDataKbnCellType()

            ' *** 縦書き表示のタイトル ***
            Dim TATE_TITLE_COLUMN_TAGS As String() = New String() {TAG_LEVEL, TAG_SHUKEI_CODE, TAG_SIA_SHUKEI_CODE, TAG_GENCYO_CKD_KBN, TAG_MAKER_CODE, TAG_BUHIN_NO_KBN, TAG_BUHIN_NO_KAITEI_NO, TAG_EDA_BAN}
            For Each tag As String In TATE_TITLE_COLUMN_TAGS
                With sheet.Cells(ALL_MIDASHI_ROW_INDEX, sheet.Columns(tag).Index)
                    .CellType = GetTateTitleCellType()
                End With
            Next
            ' ** 部品名称 **
            With sheet.Columns(TAG_BUHIN_NAME)
                .Border = borderFactory.GetRightWLine
            End With
            With sheet.Cells(ALL_MIDASHI_ROW_INDEX, sheet.Columns(TAG_BUHIN_NAME).Index)
                .Border = borderFactory.GetUnderLineAndRightWLine
            End With
            ' ** 再使用不可 **
            With sheet.Columns(TAG_SAISHIYOUFUKA)
                .Border = borderFactory.GetLeftLine
            End With

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_LEVEL, cellTypeFactory.LevelCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHUKEI_CODE, cellTypeFactory.ShukeiCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SIA_SHUKEI_CODE, cellTypeFactory.SiaShukeiCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_GENCYO_CKD_KBN, cellTypeFactory.GencyoCkdKbnCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MAKER_CODE, cellTypeFactory.MakerCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MAKER_NAME, cellTypeFactory.MakerNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO, cellTypeFactory.BuhinNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO_KBN, cellTypeFactory.BuhinNoKbnCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO_KAITEI_NO, cellTypeFactory.BuhinNoKaiteiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_EDA_BAN, cellTypeFactory.EdaBanCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NAME, cellTypeFactory.BuhinNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SAISHIYOUFUKA, cellTypeFactory.SaishiyoufukaCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KYOUKU_SECTION, cellTypeFactory.KyoukuSectionCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHUTUZU_YOTEI_DATE, cellTypeFactory.ShutuzuYoteiDateCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_SEISAKU, TsukurikataSeisakuCellType())
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_KATASHIYOU1, TsukurikataKatashiyou1CellType())
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_KATASHIYOU2, TsukurikataKatashiyou2CellType())
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_KATASHIYOU3, TsukurikataKatashiyou3CellType())
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_TIGU, TsukurikataTiguCellType())
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_NOUNYU, cellTypeFactory.TsukurikataNounyuCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_TSUKURIKATA_KIBO, cellTypeFactory.TsukurikataKiboCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAISHITU_KIKAKU1, cellTypeFactory.ZaishituKikaku1CellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAISHITU_KIKAKU2, cellTypeFactory.ZaishituKikaku2CellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAISHITU_KIKAKU3, cellTypeFactory.ZaishituKikaku3CellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_ZAISHITU_MEKKI, cellTypeFactory.ZaishituMekkiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHISAKU_BANKO_SURYO, cellTypeFactory.ShisakuBankoSuryoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHISAKU_BANKO_SURYO_U, cellTypeFactory.ShisakuBankoSuryoUCellType)
            '↓↓↓2014/12/24 メタル項目を追加 TES)張 ADD BEGIN
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MATERIAL_INFO_LENGTH, cellTypeFactory.MaterialInfoLengthCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MATERIAL_INFO_WIDTH, cellTypeFactory.MaterialInfoWidthCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_KAITEI_NO, cellTypeFactory.DataItemKaiteiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_AREA_NAME, cellTypeFactory.DataItemAreaNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_SET_NAME, cellTypeFactory.DataItemSetNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_KAITEI_INFO, cellTypeFactory.DataItemKaiteiInfoCellType)
            '↑↑↑2014/12/24 メタル項目を追加 TES)張 ADD END
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHISAKU_BUHIN_HI, cellTypeFactory.ShisakuBuhinHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHISAKU_KATA_HI, cellTypeFactory.ShisakuKataHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BIKOU, cellTypeFactory.BikouCellType)
            '2012/01/25 部品ノート追加
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NOTE, cellTypeFactory.BuhinNoteCellType)

            anInstlColumns = New InstlColumns(sheet, borderFactory)
            anInstlColumns.InitializeColumnInstl(Math.Max(DEFAULT_INSTL_COLUMN_COUNT, GetMaxColumnIndexOfInpuInstlHinban() + 1))

            'IMEを使用可能にする。
            spread.ImeMode = Windows.Forms.ImeMode.NoControl

            InitializeValidator()

            toolTip.Clear()
            toolTip.Add(New ToolTipImpl(sheet))

            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)

        End Sub

        Private Sub InitializeValidator()
            ' 保存時は、DB登録に欠かせないチェックだけ
            validatorSave = New SpreadValidator(spread)


            '取引先名称２０文字以上がいる！とりあえずDBと同じバイトに変更してみる'
            validatorSave.Add(TAG_MAKER_NAME, "取引先名称").MaxLengthByte(102)
            validatorSave.Add(TAG_ZAISHITU_MEKKI, "メッキ").MaxLengthByte(6)
            validatorSave.Add(TAG_BIKOU, "備考").MaxLengthByte(60)
            validatorSave.Add(TAG_BUHIN_NOTE, "部品ノート").MaxLengthByte(102)
            validatorSave.Add(TAG_TSUKURIKATA_KIBO, "部品製作規模・概要").MaxLengthByte(200)

            ' 登録の入力チェック
            validatorRegister = New SpreadValidator(spread)

            validatorRegister.Add(TAG_LEVEL, "レベル").Required().Numeric()
            validatorRegister.Add(TAG_SHUKEI_CODE, "国内集計").InArray(TShisakuBuhinEditVoHelper.ShukeiCode.ALL)
            validatorRegister.Add(TAG_SIA_SHUKEI_CODE, "海外集計").InArray(TShisakuBuhinEditVoHelper.SiaShukeiCode.ALL)
            validatorRegister.Add(ValidatorUtil.NewValidatorByOr("国内集計または海外集計のどちらかを入力してください。", _
                                                          ValidatorUtil.NewRequiredValidator(spread, TAG_SHUKEI_CODE), _
                                                          ValidatorUtil.NewRequiredValidator(spread, TAG_SIA_SHUKEI_CODE)))
            validatorRegister.Add(TAG_GENCYO_CKD_KBN, "現調区分").InArray(TShisakuBuhinEditVoHelper.GencyoCkdKbn.ALL)
            '取引先名称のチェックを外してみる'
            'validatorRegister.Add(TAG_MAKER_NAME, "取引先名称").MaxLengthByte(40)
            validatorRegister.Add(TAG_BUHIN_NO, "部品番号").Required()
            validatorRegister.Add(TAG_BUHIN_NAME, "部品名称").Required()
            validatorRegister.Add(TAG_SAISHIYOUFUKA, "再使用不可").InArray(TShisakuBuhinEditVoHelper.Saishiyoufuka.YES)
            '2012/01/23 供給セクション追加  ==>ここに何か実装する必要がある？
            'validatorRegister.Add(TAG_KYOUKU_SECTION, "供給セクション").InArray(TShisakuBuhinEditVoHelper.？？？.YES)

            validatorRegister.Add(TAG_ZAISHITU_MEKKI, "メッキ").MaxLengthByte(6)
            validatorRegister.Add(TAG_BIKOU, "備考").MaxLengthByte(60)
            validatorRegister.Add(TAG_BUHIN_NOTE, "部品ノート").MaxLengthByte(102)
            ''↓↓2014/07/23 Ⅰ.2.管理項目追加_v) (TES)張 ADD BEGIN
            validatorRegister.Add(TAG_TSUKURIKATA_KIBO, "部品製作規模・概要").MaxLengthByte(200)
            ''↑↑2014/07/23 Ⅰ.2.管理項目追加_v) (TES)張 ADD END

            '登録の入力チェック(構成)'
            validatorKosei = New SpreadValidator(spread)



        End Sub

        Private Sub LockSheet(ByVal IsLock As Boolean)
            If IsLock Then
                SpreadUtil.LockAllColumns(sheet)
            Else
                SpreadUtil.UnlockAllColumns(sheet)
                For Each tag As String In DEFAULT_LOCK_TAGS
                    sheet.Columns(tag).Locked = True
                Next
            End If
        End Sub

        ''' <summary>
        ''' Spread表示を(再)表示する
        ''' </summary>
        ''' <param name="observable">表示指示元のObservable</param>
        ''' <param name="arg">引数</param>
        ''' <remarks></remarks>
        Public Sub Update(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update

            '2014/10/06 追加 E.Ubukata
            ObserverUpdating = True

 
            ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_m) (TES)張 ADD BEGIN
            '該当イベント取得
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(subject.EventCode)
            ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_m) (TES)張 ADD END

            If arg Is Nothing Then
                'ClearInstlColumns()
                ClearSheet(True)
                ''↓↓2014/08/21 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
                ClearSheetBackColorAll()
                ''↑↑2014/08/21 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
                LockSheet(subject.IsViewerMode)
                Update(observable, TITLE_ROW_INDEX)

                AdjustSheetRowCount()

                For Each rowIndex As Integer In subject.GetInputRowIndexes()
                    Update(observable, rowIndex)
                Next

                'ロック処理
                LockRowsIfLevelZero(eventVo, ConvSubjectIndexToSpreadRow(subject.GetInputRowIndexes))


            ElseIf TypeOf arg Is BuhinEditKoseiSubject.NotifyInfo Then

                Dim info As BuhinEditKoseiSubject.NotifyInfo = DirectCast(arg, BuhinEditKoseiSubject.NotifyInfo)
                If info.IsTitle Then
                    AdjustSheetInstlInsuColumn()
                    For Each columnIndex As Integer In info.ColumnIndexes
                        UpdateInstlHinban(columnIndex, eventVo)
                    Next
                ElseIf info.RowIndexes IsNot Nothing Then
                    For Each rowIndex As Integer In info.RowIndexes
                        Update(observable, rowIndex)
                    Next
                End If

            ElseIf IsNumeric(arg) Then
                Dim rowIndex As Integer = CInt(arg)
                Dim row As Integer = ConvSubjectIndexToSpreadRow(rowIndex)

                If rowIndex <= TITLE_ROW_INDEX Then
                    AdjustSheetInstlInsuColumn()
                    For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                        UpdateInstlHinban(columnIndex, eventVo)
                    Next
                Else
                    ' 員数
                    For Each columnIndex As Integer In subject.InsuColumnIndexes(rowIndex)
                        Dim column As Integer = ConvKoseiInstlToSpreadColumn(columnIndex)

                        '員数がないならエラーになるからスルーさせる'
                        Try
                            sheet.Cells(row, column).Value = subject.InsuSuryo(rowIndex, columnIndex)
                        Catch ex As Exception

                        End Try

                    Next

                    sheet.Cells(row, sheet.Columns(TAG_LEVEL).Index).Value = subject.Level(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_SHUKEI_CODE).Index).Value = subject.ShukeiCode(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value = subject.SiaShukeiCode(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).Value = subject.GencyoCkdKbn(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MAKER_CODE).Index).Value = subject.MakerCode(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MAKER_NAME).Index).Value = subject.MakerName(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO).Index).Value = subject.BuhinNo(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO_KBN).Index).Value = subject.BuhinNoKbn(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO_KAITEI_NO).Index).Value = subject.BuhinNoKaiteiNo(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_EDA_BAN).Index).Value = subject.EdaBan(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NAME).Index).Value = Trim(subject.BuhinName(rowIndex))
                    sheet.Cells(row, sheet.Columns(TAG_SAISHIYOUFUKA).Index).Value = subject.Saishiyoufuka(rowIndex)
                    '2012/01/23 供給セクション追加
                    sheet.Cells(row, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value = subject.KyoukuSection(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_SHUTUZU_YOTEI_DATE).Index).Value = DateUtil.ConvYyyymmddToDate(subject.ShutuzuYoteiDate(rowIndex))
                    sheet.Cells(row, sheet.Columns(TAG_TSUKURIKATA_SEISAKU).Index).Value = subject.TsukurikataSeisaku(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU1).Index).Value = subject.TsukurikataKatashiyou1(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU2).Index).Value = subject.TsukurikataKatashiyou2(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU3).Index).Value = subject.TsukurikataKatashiyou3(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_TSUKURIKATA_TIGU).Index).Value = subject.TsukurikataTigu(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_TSUKURIKATA_NOUNYU).Index).Value = DateUtil.ConvYyyymmddToDate(subject.TsukurikataNounyu(rowIndex))
                    sheet.Cells(row, sheet.Columns(TAG_TSUKURIKATA_KIBO).Index).Value = subject.TsukurikataKibo(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_ZAISHITU_KIKAKU1).Index).Value = subject.ZaishituKikaku1(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_ZAISHITU_KIKAKU2).Index).Value = subject.ZaishituKikaku2(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_ZAISHITU_KIKAKU3).Index).Value = subject.ZaishituKikaku3(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_ZAISHITU_MEKKI).Index).Value = subject.ZaishituMekki(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_SHISAKU_BANKO_SURYO).Index).Value = subject.ShisakuBankoSuryo(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_SHISAKU_BANKO_SURYO_U).Index).Value = subject.ShisakuBankoSuryoU(rowIndex)

                    '↓↓↓2014/12/24 メタル項目を追加 TES)張 ADD BEGIN
                    sheet.Cells(row, sheet.Columns(TAG_MATERIAL_INFO_LENGTH).Index).Value = subject.MaterialInfoLength(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MATERIAL_INFO_WIDTH).Index).Value = subject.MaterialInfoWidth(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_DATA_ITEM_KAITEI_NO).Index).Value = subject.DataItemKaiteiNo(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_DATA_ITEM_AREA_NAME).Index).Value = subject.DataItemAreaName(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_DATA_ITEM_SET_NAME).Index).Value = subject.DataItemSetName(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_DATA_ITEM_KAITEI_INFO).Index).Value = subject.DataItemKaiteiInfo(rowIndex)
                    '↑↑↑2014/12/24 メタル項目を追加 TES)張 ADD END

                    sheet.Cells(row, sheet.Columns(TAG_SHISAKU_BUHIN_HI).Index).Value = subject.ShisakuBuhinHi(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_SHISAKU_KATA_HI).Index).Value = subject.ShisakuKataHi(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_BIKOU).Index).Value = subject.Bikou(rowIndex)
                    '2012/01/25 部品ノート追加
                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NOTE).Index).Value = subject.BuhinNote(rowIndex)
                    ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_m) (TES)張 ADD BEGIN
                    '該当イベントが「移管車改修」の場合
                    If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                        If subject.BaseBuhinFlg(rowIndex) = 1 Then
                            sheet.Cells(row, sheet.Columns(TAG_LEVEL).Index).BackColor = BASE_COLOR
                            sheet.Cells(row, sheet.Columns(TAG_LEVEL).Index).ForeColor = BASE_ForeCOLOR
                            sheet.Cells(row, sheet.Columns(TAG_LEVEL).Index).Locked = True
                            sheet.Cells(row, sheet.Columns(TAG_SHUKEI_CODE).Index).BackColor = BASE_COLOR
                            sheet.Cells(row, sheet.Columns(TAG_SHUKEI_CODE).Index).ForeColor = BASE_ForeCOLOR
                            sheet.Cells(row, sheet.Columns(TAG_SHUKEI_CODE).Index).Locked = True
                            sheet.Cells(row, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).BackColor = BASE_COLOR
                            sheet.Cells(row, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).ForeColor = BASE_ForeCOLOR
                            sheet.Cells(row, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Locked = True
                            sheet.Cells(row, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).BackColor = BASE_COLOR
                            sheet.Cells(row, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).ForeColor = BASE_ForeCOLOR
                            sheet.Cells(row, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).Locked = True
                            sheet.Cells(row, sheet.Columns(TAG_MAKER_CODE).Index).BackColor = BASE_COLOR
                            sheet.Cells(row, sheet.Columns(TAG_MAKER_CODE).Index).ForeColor = BASE_ForeCOLOR
                            sheet.Cells(row, sheet.Columns(TAG_MAKER_CODE).Index).Locked = True
                            sheet.Cells(row, sheet.Columns(TAG_MAKER_NAME).Index).BackColor = BASE_COLOR
                            sheet.Cells(row, sheet.Columns(TAG_MAKER_NAME).Index).ForeColor = BASE_ForeCOLOR
                            sheet.Cells(row, sheet.Columns(TAG_MAKER_NAME).Index).Locked = True
                            sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO).Index).BackColor = BASE_COLOR
                            sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO).Index).ForeColor = BASE_ForeCOLOR
                            sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO).Index).Locked = True
                            sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO_KBN).Index).BackColor = BASE_COLOR
                            sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO_KBN).Index).ForeColor = BASE_ForeCOLOR
                            sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO_KBN).Index).Locked = True
                            sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO_KAITEI_NO).Index).BackColor = BASE_COLOR
                            sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO_KAITEI_NO).Index).ForeColor = BASE_ForeCOLOR
                            sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO_KAITEI_NO).Index).Locked = True
                            sheet.Cells(row, sheet.Columns(TAG_EDA_BAN).Index).BackColor = BASE_COLOR
                            sheet.Cells(row, sheet.Columns(TAG_EDA_BAN).Index).ForeColor = BASE_ForeCOLOR
                            sheet.Cells(row, sheet.Columns(TAG_EDA_BAN).Index).Locked = True
                            sheet.Cells(row, sheet.Columns(TAG_BUHIN_NAME).Index).BackColor = BASE_COLOR
                            sheet.Cells(row, sheet.Columns(TAG_BUHIN_NAME).Index).ForeColor = BASE_ForeCOLOR
                            sheet.Cells(row, sheet.Columns(TAG_BUHIN_NAME).Index).Locked = True
                        End If
                    End If
                End If
                ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_m) (TES)張 ADD END
            End If

            '2014/05/13 kabasawa'
            'AL画面からでもロック処理を行うため'
            LockRowsIfLevelZero(eventVo, ConvSubjectIndexToSpreadRow(subject.GetInputRowIndexes))

            If subject.KounyuShiji = "0" Then
                sheet.Columns(TAG_SAISHIYOUFUKA).Visible = False
            End If

            '2014/10/06 追加 E.Ubukata
            ObserverUpdating = False
        End Sub




        ''' <summary>
        ''' 部品構成情報を追加した行の背景色を変更する。
        ''' </summary>
        ''' <param name="rowIndex">開始行</param>
        ''' <param name="rowCount">構成行</param>
        ''' <remarks></remarks>
        Public Sub UpdateBackColor(ByVal rowIndex As Integer, ByVal rowCount As Integer)

            For i As Integer = rowIndex To rowCount + rowIndex
                'とりあえずレベルと部品番号だけ変えてみる。
                'sheet.Cells(i, sheet.Columns(TAG_LEVEL).Index).BackColor = Color.LightPink 'ライトピンクに
                sheet.Cells(i, sheet.Columns(TAG_LEVEL).Index).ForeColor = Color.Blue '青色に
                sheet.Cells(i, sheet.Columns(TAG_LEVEL).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                sheet.Cells(i, sheet.Columns(TAG_BUHIN_NO).Index).ForeColor = Color.Blue '青色に
                'sheet.Cells(i, sheet.Columns(TAG_BUHIN_NO).Index).BackColor = Color.LightPink 'ライトピンクに
                sheet.Cells(i, sheet.Columns(TAG_BUHIN_NO).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
            Next

        End Sub

        Public Shared Function getInstlDataKbn(ByVal val As String) As String
            Dim rtnVal As String = String.Empty

            ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(9)) (TES)張 ADD BEGIN
            Dim strGetValue As String() = {"10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25"}
            Dim strSetValue As String() = {"MA", "MB", "MC", "MD", "ME", "MF", "MG", "MH", "MI", "MJ", "MK", "ML", "MM", "MN", "MO", "MP"}
            If StringUtil.IsEmpty(val) Or val = "0" Then
                rtnVal = ""
            ElseIf CInt(val) >= 10 AndAlso CInt(val) <= 25 Then
                For index = 0 To strGetValue.Length - 1
                    If strGetValue(index).Equals(val) Then
                        rtnVal = strSetValue(index)
                    End If
                Next
            Else
                rtnVal = "M" & val
            End If
            Return rtnVal
        End Function

        Private Sub UpdateInstlHinban(ByVal columnIndex As Integer, ByVal eventVo As TShisakuEventVo)

            Dim spreadColumn As Integer = ConvKoseiInstlToSpreadColumn(columnIndex)
            ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(9)) (TES)張 ADD BEGIN
            Dim strGetValue As String() = {"10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25"}
            Dim strSetValue As String() = {"MA", "MB", "MC", "MD", "ME", "MF", "MG", "MH", "MI", "MJ", "MK", "ML", "MM", "MN", "MO", "MP"}

            sheet.Cells(INSTL_HINBAN_ROW_INDEX, spreadColumn).Value = subject.InstlHinban(columnIndex)
            sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, spreadColumn).Value = subject.InstlHinbanKbn(columnIndex)


            sheet.Cells(INSTL_DATA_KBN_ROW_INDEX, spreadColumn).Value = getInstlDataKbn(subject.InstlDataKbn(columnIndex))

            'If StringUtil.IsEmpty(subject.InstlDataKbn(columnIndex)) Or subject.InstlDataKbn(columnIndex) = "0" Then
            '    sheet.Cells(INSTL_DATA_KBN_ROW_INDEX, spreadColumn).Value = ""
            'ElseIf CInt(subject.InstlDataKbn(columnIndex)) >= 10 AndAlso CInt(subject.InstlDataKbn(columnIndex)) <= 25 Then
            '    For index = 0 To strGetValue.Length - 1
            '        If strGetValue(index).Equals(subject.InstlDataKbn(columnIndex)) Then
            '            sheet.Cells(INSTL_DATA_KBN_ROW_INDEX, spreadColumn).Value = strSetValue(index)
            '        End If
            '    Next
            'Else
            '    sheet.Cells(INSTL_DATA_KBN_ROW_INDEX, spreadColumn).Value = "M" & subject.InstlDataKbn(columnIndex)
            'End If


            sheet.Cells(INSTL_HINBAN_ROW_INDEX, spreadColumn).ResetBackColor()
            sheet.Cells(INSTL_HINBAN_ROW_INDEX, spreadColumn).ForeColor = Color.Black
            sheet.Columns(spreadColumn).ResetBackColor()
            sheet.Columns(spreadColumn).ResetForeColor()
            sheet.Columns(spreadColumn).ResetLocked()
            '該当イベントが「移管車改修」の場合
            If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                If subject.BaseInstlFlg(columnIndex) = 1 Then
                    sheet.Columns(spreadColumn).BackColor = BASE_COLOR
                    sheet.Columns(spreadColumn).ForeColor = BASE_ForeCOLOR
                    sheet.Columns(spreadColumn).Locked = True
                    '結合されたヘッダー行のみ色を戻す
                    sheet.Cells(INSTL_HINBAN_MIDASHI_ROW_INDEX, spreadColumn).ResetBackColor()
                    sheet.Cells(INSTL_HINBAN_MIDASHI_ROW_INDEX, spreadColumn).ForeColor = Color.Black
                    'ResetBackcolor済みセルのみ、特定セル指定で設定する
                    sheet.Cells(INSTL_HINBAN_NO_ROW_INDEX, spreadColumn).BackColor = BASE_COLOR
                    sheet.Cells(INSTL_DATA_KBN_ROW_INDEX, spreadColumn).BackColor = BASE_COLOR
                    sheet.Cells(INSTL_HINBAN_ROW_INDEX, spreadColumn).BackColor = BASE_COLOR
                    sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, spreadColumn).BackColor = BASE_COLOR
                    sheet.Cells(INSTL_HINBAN_NO_ROW_INDEX, spreadColumn).Locked = True
                    sheet.Cells(INSTL_DATA_KBN_ROW_INDEX, spreadColumn).Locked = True
                    sheet.Cells(INSTL_HINBAN_ROW_INDEX, spreadColumn).Locked = True
                    sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, spreadColumn).Locked = True
                    'ForeColorも同様とする
                    sheet.Cells(INSTL_HINBAN_NO_ROW_INDEX, spreadColumn).ForeColor = BASE_ForeCOLOR
                    sheet.Cells(INSTL_DATA_KBN_ROW_INDEX, spreadColumn).ForeColor = BASE_ForeCOLOR
                    sheet.Cells(INSTL_HINBAN_ROW_INDEX, spreadColumn).ForeColor = BASE_ForeCOLOR
                    sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, spreadColumn).ForeColor = BASE_ForeCOLOR
                End If
            End If
        End Sub

        ''' <summary>
        ''' INSTL品番・員数列の表示列数を調整する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub AdjustSheetInstlInsuColumn()
            '最大列番取得'
            Dim maxColumnIndex As Integer = GetMaxColumnIndexOfInpuInstlHinban()

            If anInstlColumns.InstlColumnCount <= maxColumnIndex Then
                anInstlColumns.InsertColumnInstl(anInstlColumns.InstlColumnCount, maxColumnIndex - anInstlColumns.InstlColumnCount + 1)
            End If
        End Sub

        ''' <summary>
        ''' Subjectの行数に合わせてシートの行数を調整する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub AdjustSheetRowCount()

            Dim rowCount As Integer = ConvSubjectIndexToSpreadRow(subject.GetMaxInputRowIndex) + 10
            If sheet.RowCount < rowCount Then
                sheet.RowCount = rowCount
            End If
        End Sub
        ''' <summary>INSTL列数</summary>
        Public ReadOnly Property InstlColumnCount() As Integer
            Get
                Return anInstlColumns.InstlColumnCount
            End Get
        End Property

        ''' <summary>
        ''' Spreadの先頭にあるレベル０＋集計コード(国内集計か海外集計のどちらか)がＸの行だけをロックする
        ''' </summary>
        ''' <param name="spreadRows">行index</param>
        ''' <remarks></remarks>
        Private Sub LockRowsIfLevelZero(ByVal eventVo As TShisakuEventVo, ByVal ParamArray spreadRows As Integer())
            ''↓↓2014/09/02 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
            'Private Sub LockRowsIfLevelZero(ByVal ParamArray spreadRows As Integer())
            ''↑↑2014/09/02 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
            Dim isStartedLockRows As Boolean = True
            For Each spreadRow As Integer In spreadRows
                If subject.IsViewerMode Then
                    ClearLockLevelZero(spreadRow)
                    Continue For
                End If

                '一度ロックを外す。
                '構成再展開及び最新化の場合は全ロックがかかったままこのクラスに突入するので一度ロックを外してから
                'ロックが掛かる条件にしたがってロックを掛ける。　Ｂｙ柳沼　2011/03/08
                LockLevelZero(spreadRow, False)

                'ロックが掛かる条件'
                'レベル0かつ部品番号とINSTL品番が一致すること'
                If Not StringUtil.IsEmpty(sheet.Cells(spreadRow, sheet.Columns(TAG_LEVEL).Index).Value) Then

                End If

                Dim L As String = sheet.Cells(spreadRow, sheet.Columns(TAG_LEVEL).Index).Value

                Dim level As Integer

                If Not StringUtil.IsEmpty(L) Then
                    level = Integer.Parse(L)
                    If level = 0 Then
                        For Each spreadCol As Integer In subject.GetInputInstlHinbanColumnIndexes
                            If StringUtil.Equals(sheet.Cells(spreadRow, sheet.Columns(TAG_BUHIN_NO).Index).Value, subject.InstlHinban(spreadCol)) Then
                                LockLevelZero(spreadRow, True)
                            End If
                        Next
                    Else
                        ''試作区分は問答無用でロック'
                        sheet.Cells(spreadRow, sheet.Columns(TAG_BUHIN_NO_KBN).Index).Locked = True
                    End If
                End If

                '試作区分は問答無用でロック'
                sheet.Cells(spreadRow, sheet.Columns(TAG_BUHIN_NO_KBN).Index).Locked = True

                ''↓↓2014/09/02 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
                '該当イベントが「移管車改修」の場合
                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                    If subject.BaseBuhinFlg(ConvSpreadRowToSubjectIndex(spreadRow)) = 1 Then
                        sheet.Cells(spreadRow, sheet.Columns(TAG_LEVEL).Index).Locked = True
                        sheet.Cells(spreadRow, sheet.Columns(TAG_BUHIN_NO).Index).Locked = True
                        sheet.Cells(spreadRow, sheet.Columns(TAG_BUHIN_NO_KBN).Index).Locked = True
                    End If
                    '↑↑2014/10/01 酒井 ADD END
                    'For i As Integer = 0 To (sheet.Columns(TAG_SAISHIYOUFUKA).Index) - INSTL_INFO_START_COLUMN - 1
                    For Each i As Integer In subject.instlTitle.GetInputInstlHinbanColumnIndexes()
                        If subject.instlTitle.BaseInstlFlg(i) = "1" Then
                            Dim column As Integer = ConvKoseiInstlToSpreadColumn(i)
                            sheet.Cells(spreadRow, column).Locked = True
                        End If
                    Next
                    ''↑↑2014/09/17 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
                    '↓↓2014/10/01 酒井 ADD BEGIN
                    '                End If
                    '↑↑2014/10/01 酒井 ADD END
                End If


                ''↑↑2014/09/02 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END

                'If isStartedLockRows AndAlso EzUtil.IsEqualIfNull(sheet.Cells(spreadRow, sheet.Columns(TAG_LEVEL).Index).Value, 0) _
                '    And (EzUtil.IsEqualIfNull(sheet.Cells(spreadRow, sheet.Columns(TAG_SHUKEI_CODE).Index).Value, TShisakuBuhinEditVoHelper.ShukeiCode.NINI_KANRI_TANI) _
                '        Or EzUtil.IsEqualIfNull(sheet.Cells(spreadRow, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value, TShisakuBuhinEditVoHelper.ShukeiCode.NINI_KANRI_TANI)) Then
                '    LockLevelZero(spreadRow, True)
                'Else
                '    isStartedLockRows = False
                '    LockLevelZero(spreadRow, False)
                'End If
            Next
        End Sub
        Private Sub ClearLockLevelZero(ByVal spreadRow As Integer)
            LockLevelZero(spreadRow, True)
        End Sub
        Private Sub LockLevelZero(ByVal spreadRow As Integer, ByVal isLevelZeroRow As Boolean)
            sheet.Cells(spreadRow, sheet.Columns(TAG_LEVEL).Index).Locked = isLevelZeroRow
            sheet.Cells(spreadRow, sheet.Columns(TAG_BUHIN_NO).Index).Locked = isLevelZeroRow
            sheet.Cells(spreadRow, sheet.Columns(TAG_BUHIN_NO_KBN).Index).Locked = isLevelZeroRow

            'TODO もっとスマートな方法があればそっちに変更'
            'INSTL品番の員数も制御する。
            For i As Integer = 0 To (sheet.Columns(TAG_SAISHIYOUFUKA).Index) - INSTL_INFO_START_COLUMN - 1
                Dim column As Integer = ConvKoseiInstlToSpreadColumn(i)
                sheet.Cells(spreadRow, column).Locked = isLevelZeroRow
            Next

        End Sub

        ''' <summary>
        ''' 材質を非表示にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ZaishituColumnDisable()
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_ZAISHITU_KIKAKU1).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_ZAISHITU_KIKAKU2).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_ZAISHITU_KIKAKU3).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_ZAISHITU_MEKKI).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_SHISAKU_BANKO_SURYO).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_SHISAKU_BANKO_SURYO_U).Index, False)
            '↓↓↓2014/12/25 メタル項目を追加 TES)張 ADD BEGIN
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MATERIAL_INFO_LENGTH).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MATERIAL_INFO_WIDTH).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_DATA_ITEM_KAITEI_NO).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_DATA_ITEM_AREA_NAME).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_DATA_ITEM_SET_NAME).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_DATA_ITEM_KAITEI_INFO).Index, False)
            '↑↑↑2014/12/25 メタル項目を追加 TES)張 ADD END
        End Sub

        ''' <summary>
        ''' 材質を表示する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ZaishituColumnVisible()
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_ZAISHITU_KIKAKU1).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_ZAISHITU_KIKAKU2).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_ZAISHITU_KIKAKU3).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_ZAISHITU_MEKKI).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_SHISAKU_BANKO_SURYO).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_SHISAKU_BANKO_SURYO_U).Index, True)
            '↓↓↓2014/12/25 メタル項目を追加 TES)張 ADD BEGIN
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MATERIAL_INFO_LENGTH).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MATERIAL_INFO_WIDTH).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_DATA_ITEM_KAITEI_NO).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_DATA_ITEM_AREA_NAME).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_DATA_ITEM_SET_NAME).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_DATA_ITEM_KAITEI_INFO).Index, True)
            '↑↑↑2014/12/25 メタル項目を追加 TES)張 ADD END
        End Sub

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_k) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 作り方を非表示にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub TsukurikataColumnDisable()
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_SEISAKU).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU1).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU2).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU3).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_TIGU).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_NOUNYU).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_KIBO).Index, False)
        End Sub
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_k) (TES)張 ADD END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_j) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 作り方を表示する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub TsukurikataColumnVisible()
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_SEISAKU).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU1).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU2).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU3).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_TIGU).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_NOUNYU).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_TSUKURIKATA_KIBO).Index, True)
        End Sub
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_j) (TES)張 ADD END

        ''' <summary>
        ''' 員数を非表示にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InzuuCoulumnDisable()
            'TODO もっとスマートな方法があればそっちに変更'
            'Visibleだと他のセルまで表示されないので'
            Dim j As Integer = sheet.GetLastNonEmptyRow(NonEmptyItemFlag.Data) - INSTL_HINBAN_ROW_INDEX + 1
            For i As Integer = 0 To (sheet.Columns(TAG_SAISHIYOUFUKA).Index) - INSTL_INFO_START_COLUMN - 1
                Dim column As Integer = ConvKoseiInstlToSpreadColumn(i)
                spread.ActiveSheet.SetColumnWidth(column, 0)
            Next
        End Sub
        ''' <summary>
        ''' 員数を表示する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InzuuCoulumnVisible()
            'TODO もっとスマートな方法があればそっちに変更'
            'Visibleだと他のセルまで表示されないので'
            Dim j As Integer = sheet.GetLastNonEmptyRow(NonEmptyItemFlag.Data) - INSTL_HINBAN_ROW_INDEX + 1
            For i As Integer = 0 To (sheet.Columns(TAG_SAISHIYOUFUKA).Index) - INSTL_INFO_START_COLUMN - 1
                Dim column As Integer = ConvKoseiInstlToSpreadColumn(i)
                spread.ActiveSheet.SetColumnWidth(column, 20)
            Next
        End Sub

        ''' <summary>
        ''' 初期表示に１回のみ実行：集計コード（国内／海外）が"J"の行を非表示にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub JikyuRowDisableSyoki()
            '自給品ボタン非表示の為、コメントアウト
            'Dim wCount As Integer = 0

            'If spread.ActiveSheet.Rows.Count < subject.Matrix.Records.Count Then
            '    spread.ActiveSheet.Rows.Count = subject.Matrix.Records.Count
            'Else
            '    spread.ActiveSheet.Rows.Count = 995
            'End If

            'For rowIndex As Integer = titleRows To spread.ActiveSheet.Rows.Count - 1
            '    If subject.ShukeiCode(rowIndex - titleRows) = "J" Or _
            '       StringUtil.IsEmpty(subject.ShukeiCode(rowIndex - titleRows)) _
            '                        AndAlso subject.SiaShukeiCode(rowIndex - titleRows) = "J" Then
            '        'subject.ShukeiCode(rowIndex - titleRows) <> "J" _
            '        '                 AndAlso subject.SiaShukeiCode(rowIndex - titleRows) = "J" Or _

            '        spread.ActiveSheet.SetRowVisible(rowIndex, False)

            '    Else
            '        '1行おきにバックカラーを変更する。
            '        Dim GuSu As Integer = wCount Mod 2
            '        If GuSu = 0 Then
            '            spread.ActiveSheet.Rows(rowIndex).BackColor = Color.WhiteSmoke
            '        Else
            '            spread.ActiveSheet.Rows(rowIndex).BackColor = Color.LightYellow
            '        End If
            '        '行ヘッダーの№を再設定
            '        spread.ActiveSheet.Rows(rowIndex).Label = wCount + 1
            '        '採番
            '        wCount += 1
            '    End If
            '    '再使用不可のセルは以下の色を設定
            '    sheet.Cells(rowIndex, sheet.Columns(TAG_SAISHIYOUFUKA).Index).BackColor = Color.PeachPuff
            'Next
        End Sub


        ''' <summary>
        ''' 集計コード（国内／海外）が"J"の行を非表示にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub JikyuRowDisable()
            '自給品ボタン非表示の為、コメントアウト
            'Dim wCount As Integer = 0

            'For rowIndex As Integer = titleRows To spread.ActiveSheet.Rows.Count - 1
            '    If subject.ShukeiCode(rowIndex - titleRows) = "J" Or _
            '       StringUtil.IsEmpty(subject.ShukeiCode(rowIndex - titleRows)) _
            '                        AndAlso subject.SiaShukeiCode(rowIndex - titleRows) = "J" Then
            '        'subject.ShukeiCode(rowIndex - titleRows) <> "J" _
            '        '                 AndAlso subject.SiaShukeiCode(rowIndex - titleRows) = "J" Or _

            '        spread.ActiveSheet.SetRowVisible(rowIndex, False)

            '    Else
            '        '1行おきにバックカラーを変更する。
            '        Dim GuSu As Integer = wCount Mod 2
            '        If GuSu = 0 Then
            '            spread.ActiveSheet.Rows(rowIndex).BackColor = Color.WhiteSmoke
            '        Else
            '            spread.ActiveSheet.Rows(rowIndex).BackColor = Color.LightYellow
            '        End If
            '        '行ヘッダーの№を再設定
            '        spread.ActiveSheet.Rows(rowIndex).Label = wCount + 1
            '        '採番
            '        wCount += 1
            '    End If
            '    '再使用不可のセルは以下の色を設定
            '    sheet.Cells(rowIndex, sheet.Columns(TAG_SAISHIYOUFUKA).Index).BackColor = Color.PeachPuff
            'Next
        End Sub

        ''' <summary>
        ''' 集計コード（国内／海外）が"J"の行を再表示にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub JikyuRowVisible()
            '自給品ボタン非表示の為、コメントアウト
            'For rowIndex As Integer = titleRows To spread.ActiveSheet.Rows.Count - 1
            '    '全ての行を表示する。
            '    spread.ActiveSheet.SetRowVisible(rowIndex, True)

            '    '1行おきにバックカラーを変更する。
            '    Dim GuSu As Integer = rowIndex Mod 2
            '    If GuSu = 0 Then
            '        spread.ActiveSheet.Rows(rowIndex).BackColor = Color.WhiteSmoke
            '    Else
            '        spread.ActiveSheet.Rows(rowIndex).BackColor = Color.LightYellow
            '    End If
            '    '行ヘッダーのラベルを再設定
            '    spread.ActiveSheet.Rows(rowIndex).Label = rowIndex - titleRows + 1
            '    '再使用不可のセルは以下の色を設定
            '    sheet.Cells(rowIndex, sheet.Columns(TAG_SAISHIYOUFUKA).Index).BackColor = Color.PeachPuff
            'Next
        End Sub

        ''' <summary>
        ''' 入力した列indexの最大列indexを返す
        ''' </summary>
        ''' <returns>最大列index</returns>
        ''' <remarks></remarks>
        Private Function GetMaxColumnIndexOfInpuInstlHinban() As Integer

            Dim columnIndexes As ICollection(Of Integer) = subject.GetInputInstlHinbanColumnIndexes
            If columnIndexes.Count = 0 Then
                Return -1
            End If
            Dim hoge As New List(Of Integer)(columnIndexes)
            hoge.Sort()
            Return hoge(hoge.Count - 1)
        End Function

        ''' <summary>
        ''' Spread列indexを、(Subject)INSTL列indexにして返す
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <returns>(Subject)INSTL列index</returns>
        ''' <remarks></remarks>
        Public Function ConvSpreadColumnToKoseiInstl(ByVal spreadColumn As Integer) As Integer
            Return spreadColumn - (INSTL_INFO_START_COLUMN)
        End Function
        ''' <summary>
        ''' INSTL情報の列indexから、Spreadの列indexを算出する
        ''' </summary>
        ''' <param name="columnIndex">INSTL情報の列index</param>
        ''' <returns>Spreadの列index</returns>
        ''' <remarks></remarks>
        Private Function ConvKoseiInstlToSpreadColumn(ByVal columnIndex As Integer) As Integer

            Return INSTL_INFO_START_COLUMN + columnIndex
        End Function
        ''' <summary>
        ''' INSTL情報の列indexから、Spreadの列indexを算出する
        ''' </summary>
        ''' <param name="columnIndexes">INSTL情報の列index</param>
        ''' <returns>Spreadの列index</returns>
        ''' <remarks></remarks>
        Private Function ConvKoseiInstlToSpreadColumn(ByVal columnIndexes As ICollection(Of Integer)) As ICollection(Of Integer)
            Dim result As New List(Of Integer)
            For Each columnIndex As Integer In columnIndexes
                result.Add(ConvKoseiInstlToSpreadColumn(columnIndex))
            Next
            Return result
        End Function

        ''' <summary>
        ''' Spread行indexを、(Subject)indexにして返す
        ''' </summary>
        ''' <param name="spreadRow">Spread行index</param>
        ''' <returns>(Subject)index</returns>
        ''' <remarks></remarks>
        Public Function ConvSpreadRowToSubjectIndex(ByVal spreadRow As Integer) As Integer
            '↓↓2014/10/06 酒井 ADD BEGIN
            '        Private Function ConvSpreadRowToSubjectIndex(ByVal spreadRow As Integer) As Integer
            '↑↑2014/10/06 酒井 ADD END
            Return spreadRow - titleRows
        End Function
        ''' <summary>
        ''' (Subject)indexを、Spread行indexにして返す
        ''' </summary>
        ''' <param name="subjectIndex">(Subject)index</param>
        ''' <returns>Spread行index</returns>
        ''' <remarks></remarks>
        Private Function ConvSubjectIndexToSpreadRow(ByVal subjectIndex As Integer) As Integer
            Return subjectIndex + titleRows
        End Function
        ''' <summary>
        ''' (Subject)indexを、Spread行indexにして返す
        ''' </summary>
        ''' <param name="rowIndexes">(Subject)index</param>
        ''' <returns>Spread行index</returns>
        ''' <remarks></remarks>
        Private Function ConvSubjectIndexToSpreadRow(ByVal rowIndexes As ICollection(Of Integer)) As Integer()
            Dim result As New List(Of Integer)
            For Each rowIndex As Integer In rowIndexes
                result.Add(ConvSubjectIndexToSpreadRow(rowIndex))
            Next
            Return result.ToArray
        End Function


        ''' <summary>
        ''' Spread列indexがINSTL列かを返す
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function IsInstlColumn(ByVal spreadColumn As Integer) As Boolean
            Return INSTL_INFO_START_COLUMN <= spreadColumn AndAlso spreadColumn < INSTL_INFO_START_COLUMN + anInstlColumns.InstlColumnCount
        End Function
        ''' <summary>
        ''' Spread行indexが、データ入力行かを返す
        ''' </summary>
        ''' <param name="spreadRow">Spread行index</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Function IsDataRow(ByVal spreadRow As Integer) As Boolean
            Return titleRows <= spreadRow
        End Function

        ''' <summary>
        ''' Spreadの変更イベントハンドラ
        ''' </summary>
        ''' <param name="sender">イベントハンドラに従う</param>
        ''' <param name="e">イベントハンドラに従う</param>
        ''' <remarks></remarks>
        Private Sub Spread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            If IsSuspend_SpreadChangeEvent Then
                Return
            End If

            OnChange(e.Row, e.Column)
        End Sub

        ''' <summary>
        ''' INSTL品番列のデータをクリアする
        ''' </summary>
        ''' <param name="dataOnly">データだけをクリアする場合、true</param>
        ''' <remarks></remarks>
        Public Sub ClearInstlColumns(Optional ByVal dataOnly As Boolean = True)
            SpreadUtil.RemoveHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
            Try
                '2014/12/17 データ区分行までクリアするよう変更
                'sheet.ClearRange(INSTL_HINBAN_ROW_INDEX, INSTL_INFO_START_COLUMN, sheet.GetLastNonEmptyRow(NonEmptyItemFlag.Data) - INSTL_HINBAN_ROW_INDEX + 1, INSTL_INFO_START_COLUMN + GetMaxColumnIndexOfInpuInstlHinban(), dataOnly)
                sheet.ClearRange(INSTL_DATA_KBN_ROW_INDEX, INSTL_INFO_START_COLUMN, sheet.GetLastNonEmptyRow(NonEmptyItemFlag.Data) - INSTL_HINBAN_ROW_INDEX + 1, INSTL_INFO_START_COLUMN + GetMaxColumnIndexOfInpuInstlHinban(), dataOnly)
            Finally
                SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
            End Try
        End Sub

        ''' <summary>
        ''' Sheetのデータをクリアする(INSTL品番の列名部と全体の明細部)
        ''' </summary>
        ''' <param name="dataOnly">データだけをクリアする場合、true</param>
        ''' <remarks></remarks>
        Public Sub ClearSheet(Optional ByVal dataOnly As Boolean = True)
            SpreadUtil.RemoveHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
            Try
                ' INSTL品番の列名部分
                '2014/12/17 データ区分行までクリアするよう変更
                'sheet.ClearRange(INSTL_HINBAN_ROW_INDEX, INSTL_INFO_START_COLUMN, INSTL_HINBAN_KBN_ROW_INDEX - INSTL_HINBAN_ROW_INDEX + 1, anInstlColumns.InstlColumnCount, dataOnly)
                sheet.ClearRange(INSTL_DATA_KBN_ROW_INDEX, INSTL_INFO_START_COLUMN, INSTL_HINBAN_KBN_ROW_INDEX - INSTL_HINBAN_ROW_INDEX + 2, anInstlColumns.InstlColumnCount, dataOnly)
                ' 全体の明細部
                sheet.ClearRange(titleRows, 0, sheet.GetLastNonEmptyRow(NonEmptyItemFlag.Data) - titleRows + 1, sheet.ColumnCount, dataOnly)
            Finally
                SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
            End Try
        End Sub

        ''' <summary>
        ''' Sheet全体の背景色を元に戻す
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearSheetBackColor()
            ' INSTL品番の列名部分
            ''↓↓2014/08/21 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
            'With sheet.Cells(INSTL_HINBAN_ROW_INDEX, INSTL_INFO_START_COLUMN, INSTL_HINBAN_KBN_ROW_INDEX - INSTL_HINBAN_ROW_INDEX + 1, anInstlColumns.InstlColumnCount)
            With sheet.Cells(INSTL_HINBAN_NO_ROW_INDEX, INSTL_INFO_START_COLUMN, INSTL_HINBAN_KBN_ROW_INDEX, INSTL_INFO_START_COLUMN + anInstlColumns.InstlColumnCount)
                .ResetLocked()
                ''↑↑2014/08/21 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
                .ResetBackColor()
                '.ResetForeColor()
                '.ResetFont()
            End With
            ' 全体の明細部
            With sheet.Cells(titleRows, 0, sheet.RowCount - 1, sheet.ColumnCount - 1)
                ''↓↓2014/08/21 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
                .ResetLocked()
                ''↑↑2014/08/21 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
                .ResetBackColor()
                '.ResetForeColor()
                '.ResetFont()
            End With
        End Sub

        ''' <summary>
        ''' Sheet全体の背景色を元に戻す
        '''　全て
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearSheetBackColorAll()
            ' INSTL品番の列名部分
            ''↓↓2014/08/21 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
            'With sheet.Cells(INSTL_HINBAN_ROW_INDEX, INSTL_INFO_START_COLUMN, INSTL_HINBAN_KBN_ROW_INDEX - INSTL_HINBAN_ROW_INDEX + 1, anInstlColumns.InstlColumnCount)
            With sheet.Cells(INSTL_HINBAN_NO_ROW_INDEX, INSTL_INFO_START_COLUMN, INSTL_HINBAN_KBN_ROW_INDEX, INSTL_INFO_START_COLUMN + anInstlColumns.InstlColumnCount)
                .ResetLocked()
                ''↑↑2014/08/21 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
                .ResetBackColor()
                .ResetForeColor()
                .ResetFont()
            End With
            ' 全体の明細部
            With sheet.Cells(titleRows, 0, sheet.RowCount - 1, sheet.ColumnCount - 1)
                ''↓↓2014/08/21 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
                .ResetLocked()
                ''↑↑2014/08/21 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END
                .ResetBackColor()
                .ResetForeColor()
                .ResetFont()
            End With
        End Sub

        ''' <summary>
        ''' 入力データ変更時の処理
        ''' </summary>
        ''' <param name="row">Spread行index</param>
        ''' <param name="column">spread列index</param>
        ''' <remarks></remarks>
        Private Sub OnChange(ByVal row As Integer, ByVal column As Integer)
            If Not IsDataRow(row) Then
                Return
            End If
            If column < 0 Then  ' 行選択時にコレ
                Return
            End If
            Dim rowIndex As Integer = ConvSpreadRowToSubjectIndex(row)
            Dim value As Object = sheet.Cells(row, column).Value

            Select Case Convert.ToString(sheet.Columns(column).Tag)
                Case TAG_LEVEL
                    'If subject.Level(rowIndex) <> 0 And subject.Level(rowIndex) <> StringUtil.ToInteger(value) Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.Level(rowIndex) = StringUtil.ToInteger(value)

                Case TAG_SHUKEI_CODE
                    'If subject.ShukeiCode(rowIndex) <> "" And subject.ShukeiCode(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.ShukeiCode(rowIndex) = value
                Case TAG_SIA_SHUKEI_CODE
                    'If subject.SiaShukeiCode(rowIndex) <> "" And subject.SiaShukeiCode(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.SiaShukeiCode(rowIndex) = value
                Case TAG_GENCYO_CKD_KBN
                    'If subject.GencyoCkdKbn(rowIndex) <> "" And subject.GencyoCkdKbn(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.GencyoCkdKbn(rowIndex) = value
                Case TAG_MAKER_CODE
                    'If subject.MakerCode(rowIndex) <> "" And subject.MakerCode(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.MakerCode(rowIndex) = value
                Case TAG_MAKER_NAME
                    'If subject.MakerName(rowIndex) <> "" And subject.MakerName(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.MakerName(rowIndex) = value
                Case TAG_BUHIN_NO
                    'If subject.BuhinNo(rowIndex) <> "" And subject.BuhinNo(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.BuhinNo(rowIndex) = value
                Case TAG_BUHIN_NO_KBN
                    'If subject.BuhinNoKbn(rowIndex) <> "" And subject.BuhinNoKbn(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.BuhinNoKbn(rowIndex) = value
                Case TAG_BUHIN_NO_KAITEI_NO
                    'If subject.BuhinNoKaiteiNo(rowIndex) <> "" And subject.BuhinNoKaiteiNo(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.BuhinNoKaiteiNo(rowIndex) = value
                Case TAG_EDA_BAN
                    'If subject.EdaBan(rowIndex) <> "" And subject.EdaBan(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.EdaBan(rowIndex) = value
                Case TAG_BUHIN_NAME
                    'If subject.BuhinName(rowIndex) <> "" And subject.BuhinName(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.BuhinName(rowIndex) = value
                Case TAG_SAISHIYOUFUKA
                    'If subject.Saishiyoufuka(rowIndex) <> "" And subject.Saishiyoufuka(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.Saishiyoufuka(rowIndex) = value
                Case TAG_KYOUKU_SECTION
                    '2012/01/23 供給セクション追加
                    subject.KyoukuSection(rowIndex) = value
                Case TAG_SHUTUZU_YOTEI_DATE
                    'If subject.ShutuzuYoteiDate(rowIndex) <> 0 And subject.ShutuzuYoteiDate(rowIndex) <> DateUtil.ConvDateValueToIneteger(value) Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.ShutuzuYoteiDate(rowIndex) = DateUtil.ConvDateValueToIneteger(value)
                Case TAG_ZAISHITU_KIKAKU1
                    'If subject.ZaishituKikaku1(rowIndex) <> "" And subject.ZaishituKikaku1(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.ZaishituKikaku1(rowIndex) = value
                Case TAG_ZAISHITU_KIKAKU2
                    'If subject.ZaishituKikaku2(rowIndex) <> "" And subject.ZaishituKikaku2(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.ZaishituKikaku2(rowIndex) = value
                Case TAG_ZAISHITU_KIKAKU3
                    'If subject.ZaishituKikaku3(rowIndex) <> "" And subject.ZaishituKikaku3(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.ZaishituKikaku3(rowIndex) = value
                Case TAG_ZAISHITU_MEKKI
                    'If subject.ZaishituMekki(rowIndex) <> "" And subject.ZaishituMekki(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.ZaishituMekki(rowIndex) = value
                Case TAG_SHISAKU_BANKO_SURYO
                    'If subject.ShisakuBankoSuryo(rowIndex) <> "" And subject.ShisakuBankoSuryo(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.ShisakuBankoSuryo(rowIndex) = value
                Case TAG_SHISAKU_BANKO_SURYO_U
                    'If subject.ShisakuBankoSuryoU(rowIndex) <> "" And subject.ShisakuBankoSuryoU(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.ShisakuBankoSuryoU(rowIndex) = value
                Case TAG_SHISAKU_BUHIN_HI
                    'If subject.ShisakuBuhinHi(rowIndex) > 0 And subject.ShisakuBuhinHi(rowIndex) <> StringUtil.ToInteger(value) Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.ShisakuBuhinHi(rowIndex) = StringUtil.ToInteger(value)
                Case TAG_SHISAKU_KATA_HI
                    'If subject.ShisakuKataHi(rowIndex) > 0 And subject.ShisakuKataHi(rowIndex) <> StringUtil.ToInteger(value) Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.ShisakuKataHi(rowIndex) = StringUtil.ToInteger(value)
                Case TAG_BIKOU
                    'If subject.Bikou(rowIndex) <> "" And subject.Bikou(rowIndex) <> value Then
                    '    sheet.Cells(row, column).ForeColor = Color.Blue   '文字色を変更
                    '    sheet.Cells(row, column).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    'End If
                    subject.Bikou(rowIndex) = value
                Case TAG_BUHIN_NOTE
                    subject.BuhinNote(rowIndex) = value
                    '20140818 Sakai Add
                Case TAG_TSUKURIKATA_SEISAKU
                    subject.TsukurikataSeisaku(rowIndex) = value
                Case TAG_TSUKURIKATA_KATASHIYOU1
                    subject.TsukurikataKatashiyou1(rowIndex) = value
                Case TAG_TSUKURIKATA_KATASHIYOU2
                    subject.TsukurikataKatashiyou2(rowIndex) = value
                Case TAG_TSUKURIKATA_KATASHIYOU3
                    subject.TsukurikataKatashiyou3(rowIndex) = value
                Case TAG_TSUKURIKATA_TIGU
                    subject.TsukurikataTigu(rowIndex) = value
                Case TAG_TSUKURIKATA_NOUNYU
                    subject.TsukurikataNounyu(rowIndex) = DateUtil.ConvDateValueToIneteger(value)
                Case TAG_TSUKURIKATA_KIBO
                    subject.TsukurikataKibo(rowIndex) = value
                    '↓↓↓2014/12/24 メタル項目を追加 TES)張 ADD BEGIN
                Case TAG_MATERIAL_INFO_LENGTH
                    subject.MaterialInfoLength(rowIndex) = StringUtil.ToInteger(value)
                Case TAG_MATERIAL_INFO_WIDTH
                    subject.MaterialInfoWidth(rowIndex) = StringUtil.ToInteger(value)
                Case TAG_DATA_ITEM_KAITEI_NO
                    subject.DataItemKaiteiNo(rowIndex) = value
                Case TAG_DATA_ITEM_AREA_NAME
                    subject.DataItemAreaName(rowIndex) = value
                Case TAG_DATA_ITEM_SET_NAME
                    subject.DataItemSetName(rowIndex) = value
                Case TAG_DATA_ITEM_KAITEI_INFO
                    subject.DataItemKaiteiInfo(rowIndex) = value
                    '↑↑↑2014/12/24 メタル項目を追加 TES)張 ADD END
                Case Else
                    If Not IsInstlColumn(column) Then
                        Return
                    End If

                    Dim columnIndex As Integer = ConvSpreadColumnToKoseiInstl(column)

                    subject.InsuSuryo(rowIndex, columnIndex) = value

            End Select

            subject.NotifyObservers(rowIndex)
        End Sub

        ''' <summary>
        ''' 員数配列に追加処理
        ''' </summary>
        ''' <param name="rowNo">行番号</param>
        ''' <param name="columnIndex">列番号</param>
        ''' <param name="insu">員数数量</param>
        ''' <remarks></remarks>
        Private Sub AddInsuArray(ByVal rowNo As Integer, ByVal columnIndex As Integer, ByVal insu As Integer)
            InsuArray(rowNo)(columnIndex) = insu
        End Sub

        ''' <summary>
        ''' INSTL品番列の背景色を変更
        ''' </summary>
        ''' <param name="columnIndex">列番号</param>
        ''' <remarks></remarks>
        Private Sub SetInstlColumnColor(ByVal columnIndex As Integer)

            Dim impl As Kosei.Dao.MakeStructureResultDao = New Kosei.Dao.MakeStructureResultDaoImpl
            Dim r0552 As New Rhac0552Vo
            Dim r0553 As New Rhac0553Vo
            Dim r0551 As New Rhac0551Vo

            r0552 = impl.FindByBuhinRhac0552(sheet.Cells(INSTL_HINBAN_ROW_INDEX, columnIndex).Value)

            '量産EBOMに存在するINSTL品番か確認'
            If r0552 Is Nothing Then

                r0553 = impl.FindByBuhinRhac0553AL(sheet.Cells(INSTL_HINBAN_ROW_INDEX, columnIndex).Value)

                If r0553 Is Nothing Then
                    r0551 = impl.FindByBuhinRhac0551(sheet.Cells(INSTL_HINBAN_ROW_INDEX, columnIndex).Value)
                    If r0551 Is Nothing Then
                        sheet.Cells(INSTL_HINBAN_ROW_INDEX, columnIndex).BackColor = Color.Yellow
                    Else
                        sheet.Cells(INSTL_HINBAN_ROW_INDEX, columnIndex).BackColor = Color.Red
                    End If
                Else
                    sheet.Cells(INSTL_HINBAN_ROW_INDEX, columnIndex).BackColor = Color.Red
                End If
            Else
                ''
                sheet.Cells(INSTL_HINBAN_ROW_INDEX, columnIndex).BackColor = Color.Red
            End If
        End Sub


        '' 初期表示処理が終わったか？
        Private _UpdateFlag As Boolean = False

        Public Property UpdateFlag() As Boolean
            Get
                Return _UpdateFlag
            End Get
            Set(ByVal value As Boolean)
                _UpdateFlag = value
            End Set
        End Property

        ''' <summary>
        ''' 列を削除する
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Private Sub RemoveColumns(ByVal spreadColumn As Integer, ByVal removeCount As Integer)
            If removeCount = 0 Then
                Return
            End If
            sheet.RemoveColumns(spreadColumn, removeCount)
        End Sub
        ''' <summary>
        ''' 列を挿入する
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Private Sub InsertColumns(ByVal spreadColumn As Integer, ByVal insertCount As Integer)
            If insertCount = 0 Then
                Return
            End If
            sheet.AddColumns(spreadColumn, insertCount)
        End Sub

        ''' <summary>
        ''' 登録処理における入力チェックに問題がない事を実証する
        ''' </summary>
        ''' <remarks>問題が有れば、IllegalInputException</remarks>
        Public Sub AssertValidateRegister()

            '↓↓2014/10/29 酒井 ADD BEGIN
            'Ver6_2 1.95以降の修正内容の展開
            'DeleteRowCheck()
            '↑↑2014/10/29 酒井 ADD END

            Dim ls As New ArrayList
            For Each row As Integer In subject._koseiMatrix.GetInputRowIndexes
                If StringUtil.IsEmpty(subject._koseiMatrix.Record(row).Bikou) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).BuhinName) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).BuhinNo) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).BuhinNoKaiteiNo) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).BuhinNoKbn) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).BuhinNote) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).EdaBan) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).EditTourokubi) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).EditTourokujikan) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).GencyoCkdKbn) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).InstlDataKbn) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).KyoukuSection) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).Level) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).MakerCode) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).MakerName) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).ShisakuBankoSuryo) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).ShisakuBankoSuryoU) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).ShisakuBuhinHi) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).ShisakuKataHi) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).ShukeiCode) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).ShutuzuYoteiDate) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).SiaShukeiCode) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).TsukurikataKatashiyou1) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).TsukurikataKatashiyou2) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).TsukurikataKatashiyou3) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).TsukurikataKibo) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).TsukurikataNounyu) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).TsukurikataSeisaku) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).TsukurikataTigu) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).ZaishituKikaku1) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).ZaishituKikaku2) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).ZaishituKikaku3) AndAlso _
                   StringUtil.IsEmpty(subject._koseiMatrix.Record(row).ZaishituMekki) Then
                    subject._koseiMatrix.RemoveRow(row)

                End If

            Next

            Dim spreadRows As Integer() = ConvSubjectIndexToSpreadRow(subject.GetInputRowIndexes())

            '2012/02/28 データが無くてもブロックは登録したい'
            If spreadRows.Length = 0 Then
                Return
            End If

            For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                Dim spreadColumn As Integer = ConvKoseiInstlToSpreadColumn(columnIndex)
                If Not IsInputtedInsuOnColumn(spreadRows, spreadColumn) Then
                    Throw New IllegalInputException("員数を入力して下さい。", ValidatorUtil.MakeErrorControls(spread, spreadRows, spreadColumn))
                End If
            Next

            AssertValidateSave(True)

            validatorRegister.AssertValidate(spreadRows)
        End Sub


        ''' <summary>
        ''' 余計な行を削除させる
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DeleteRowCheck()
            '↓↓2014/10/29 酒井 ADD BEGIN
            'Ver6_2 1.95以降の修正内容の展開
            'Private Sub DeleteRowCheck()
            '↑↑2014/10/29 酒井 ADD END
            Dim spreadRows As Integer() = ConvSubjectIndexToSpreadRow(subject.GetInputRowIndexes())
            Dim removeCount As Integer = 0

            '全体のセルをチェックする'
            If spreadRows.Length = 0 Then
                Return
            End If

            For index As Integer = spreadRows(0) To spreadRows.Length + 3

                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_LEVEL).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_SHUKEI_CODE).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_MAKER_CODE).Index).Value) Then
                    Continue For
                End If
                '2012/01/25 部品ノート追加
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BUHIN_NOTE).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BIKOU).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_MAKER_NAME).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BUHIN_NO).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BUHIN_NO_KBN).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BUHIN_NO_KBN).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BUHIN_NAME).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_EDA_BAN).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_SAISHIYOUFUKA).Index).Value) Then
                    Continue For
                End If
                '2012/01/23 供給セクション追加
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_SHISAKU_BANKO_SURYO).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_SHISAKU_BANKO_SURYO_U).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_SHISAKU_BUHIN_HI).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_SHISAKU_KATA_HI).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_SHUTUZU_YOTEI_DATE).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_ZAISHITU_KIKAKU1).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_ZAISHITU_KIKAKU2).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_ZAISHITU_KIKAKU3).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_ZAISHITU_MEKKI).Index).Value) Then
                    Continue For
                End If
                '↓↓↓2014/12/30 メタル項目を追加 TES)張 ADD BEGIN
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_TSUKURIKATA_SEISAKU).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU1).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU2).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU3).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_TSUKURIKATA_TIGU).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_TSUKURIKATA_NOUNYU).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_TSUKURIKATA_KIBO).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_MATERIAL_INFO_LENGTH).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_MATERIAL_INFO_WIDTH).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_DATA_ITEM_KAITEI_NO).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_DATA_ITEM_AREA_NAME).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_DATA_ITEM_SET_NAME).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_DATA_ITEM_KAITEI_INFO).Index).Value) Then
                    Continue For
                End If
                '↑↑↑2014/12/30 メタル項目を追加 TES)張 ADD END

                IsSuspend_SpreadChangeEvent = True
                Try
                    sheet.RemoveRows(index - removeCount, 1)
                    subject.RemoveRow(ConvSpreadRowToSubjectIndex(index - removeCount), 1)
                Finally
                    IsSuspend_SpreadChangeEvent = False
                End Try

                removeCount = removeCount + 1

                'RemoveRows(index, 1)
                'subject.RemoveRow(ConvSpreadRowToSubjectIndex(index), 1)
            Next
        End Sub


        ''' <summary>
        ''' 員数が指定列に入力されているかを返す
        ''' </summary>
        ''' <param name="spreadRowIndexes">行index</param>
        ''' <param name="spreadColumn">列index</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function IsInputtedInsuOnColumn(ByVal spreadRowIndexes As Integer(), ByVal spreadColumn As Integer) As Boolean

            For Each spreadRow As Integer In spreadRowIndexes
                '' subject.InsuSuryoは、文字を保持していないから、Sheetを直接参照する
                If Not StringUtil.IsEmpty(sheet.Cells(spreadRow, spreadColumn).Value) Then
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' 登録処理における入力チェックに問題がない事を実証する（警告表示用）
        ''' </summary>
        ''' <remarks>問題が有れば、IllegalInputException</remarks>
        Public Sub AssertValidateRegisterWarning()
            'validatorRegiserShukei.AssertValidate(ConvSubjectIndexToSpreadRow(subject.GetInputRowIndexes()))
            'validatorRegiserSiaShukei.AssertValidate(ConvSubjectIndexToSpreadRow(subject.GetInputRowIndexes()))
            validatorRegisterWarning.AssertValidate(ConvSubjectIndexToSpreadRow(subject.GetInputRowIndexes()))

        End Sub


        ''' <summary>
        ''' 保存処理における入力チェックに問題がない事を実証する
        ''' </summary>
        ''' <remarks>問題が有れば、IllegalInputException</remarks>
        Public Sub AssertValidateSave(Optional ByVal isCalledByRegister As Boolean = False)

            Const srcTag As String = "INSTL_CHECK!!"

            Dim insuRange As New SpreadValidator(spread)
            insuRange.AddTextCell(srcTag).Range(1, 99)
            Dim insuAsterisc As New SpreadValidator(spread)
            insuAsterisc.AddTextCell(srcTag).InArray(BuhinEditInsu.GREASE_FORM_VALUE)

            Dim validator As BaseValidator = ValidatorUtil.NewValidatorByOr("員数を正しく入力して下さい。", insuRange, insuAsterisc)
            For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                Dim spreadColumn As Integer = ConvKoseiInstlToSpreadColumn(columnIndex)

                sheet.Columns(spreadColumn).Tag = srcTag
                Try
                    For Each rowNo As Integer In subject.GetInputRowIndexes()
                        Dim spreadRow As Integer = ConvSubjectIndexToSpreadRow(rowNo)
                        validator.AssertValidate(spreadRow)
                    Next
                Finally
                    sheet.Columns(spreadColumn).Tag = Nothing
                End Try
            Next

            If Not isCalledByRegister Then
                For Each rowIndex As Integer In subject.GetInputRowIndexes()
                    validatorSave.AssertValidate(ConvSubjectIndexToSpreadRow(rowIndex))
                Next
            End If
        End Sub

        ''' <summary>
        ''' 空行を挿入する
        ''' </summary>
        ''' <param name="spreadRow">Spread行index</param>
        ''' <param name="rowCount">挿入行数</param>
        ''' <remarks></remarks>
        Public Sub InsertRows(ByVal spreadRow As Integer, ByVal rowCount As Integer)
            IsSuspend_SpreadChangeEvent = True
            Try
                sheet.AddRows(spreadRow, rowCount)
                '↓↓2014/10/29 酒井 ADD BEGIN
                'Ver6_2 1.95以降の修正内容の展開
                'subject.InsertRow(ConvSpreadRowToSubjectIndex(spreadRow), rowCount)
                subject.InsertRow(ConvSpreadRowToSubjectIndex(spreadRow), rowCount, True)
                '↑↑2014/10/29 酒井 ADD END
                ' 上の行のレベル
                subject.NotifyObservers()
            Finally
                IsSuspend_SpreadChangeEvent = False
            End Try
        End Sub

        ''' <summary>
        ''' 空行を挿入する（上の行の次レベルを設定する）
        ''' </summary>
        ''' <param name="spreadRow">Spread行index</param>
        ''' <param name="rowCount">挿入行数</param>
        ''' <remarks></remarks>
        Public Sub InsertRowsNext(ByVal spreadRow As Integer, ByVal rowCount As Integer)
            IsSuspend_SpreadChangeEvent = True
            Try
                sheet.AddRows(spreadRow, rowCount)
                subject.InsertRowNext(ConvSpreadRowToSubjectIndex(spreadRow), rowCount)
                ' 上の行のレベル
                subject.NotifyObservers()
            Finally
                IsSuspend_SpreadChangeEvent = False
            End Try
        End Sub

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 CHG BEGIN
        'Public Sub RemoveRows(ByVal spreadRow As Integer, ByVal rowCount As Integer)
        ''' <summary>
        ''' 行を削除する
        ''' </summary>
        ''' <param name="spreadRow">Spread行index</param>
        ''' <param name="rowCount">削除行数</param>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Public Sub RemoveRows(ByVal spreadRow As Integer, ByVal rowCount As Integer, ByVal shisakuEventCode As String)
            IsSuspend_SpreadChangeEvent = True
            Try
                '該当イベント取得
                Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                Dim eventVo As TShisakuEventVo
                eventVo = eventDao.FindByPk(shisakuEventCode)

                ''↓↓2014/09/12 Ⅰ.3.設計編集 ベース改修専用化 酒井 CHG BEGIN
                'If Not (eventVo.BlockAlertKind = 2 And subject.BaseBuhinFlg(spreadRow) = 1) Then
                If Not (eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" And subject.BaseBuhinFlg(ConvSpreadRowToSubjectIndex(spreadRow)) = 1) Then
                    '    ''↑↑2014/09/12 Ⅰ.3.設計編集 ベース改修専用化 酒井 CHG END
                    sheet.RemoveRows(spreadRow, rowCount)
                    subject.RemoveRow(ConvSpreadRowToSubjectIndex(spreadRow), rowCount)
                    'Else

                    'For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes()
                    '    If Not subject.BaseInstlFlg(columnIndex) = 1 Then
                    '        'subject.InsuSuryo(spreadRow, columnIndex) = 0
                    '        subject.InsuSuryo(ConvSpreadRowToSubjectIndex(spreadRow), columnIndex) = 0
                    '    End If
                    'Next
                End If
            Finally
                IsSuspend_SpreadChangeEvent = False
            End Try
        End Sub
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 CHG END

        ''' <summary>
        ''' 「最新化」メニューを有効にできるかを返す
        ''' </summary>
        ''' <param name="selection">選択Cell</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Function CanEnabledOptimizeMenu(ByVal selection As CellRange) As Boolean
            Return IsInstlColumn(selection.Column) AndAlso IsInstlColumn(selection.Column + selection.ColumnCount - 1) AndAlso selection.Row = INSTL_HINBAN_NO_ROW_INDEX AndAlso selection.RowCount = 1
        End Function

        ''' <summary>
        ''' 「最新化」した新しい部品表を返す
        ''' </summary>
        ''' <param name="selection">選択Cell</param>
        ''' <returns>最新化した部品表</returns>
        ''' <remarks></remarks>
        Public Function NewMatrixOptimize(ByVal selection As CellRange, ByVal isReuseStructure As Boolean, ByVal JikyuUmu As String) As BuhinKoseiMatrix
            Dim columnIndex As Integer = ConvSpreadColumnToKoseiInstl(selection.Column)
            Return subject.NewMatrixLatest(columnIndex, selection.ColumnCount, isReuseStructure, JikyuUmu)
        End Function

        ''' <summary>
        ''' 「構成展開」処理でINSTL品番の構成情報が存在する事を実証する
        ''' </summary>
        ''' <remarks>問題が有る場合、IllegalInputExceptionを投げる</remarks>
        Public Sub AssertValidateKoseiTenkai()
            Dim errorControls As New List(Of ErrorControl)
            For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                If subject.GetStructureResult(columnIndex) Is Nothing Then
                    'If subject.GetStructureResultAssert(columnIndex) Is Nothing Then
                    Throw New InvalidProgramException("anEzTunnel.DetectStructureResultsしたあとに nothing が返る事はあり得ない")
                End If
                If subject.GetStructureResult(columnIndex).IsExist Then
                    'If subject.GetStructureResultAssert(columnIndex).IsExist Then
                    Continue For
                End If
                errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
            Next
            If 0 < errorControls.Count Then
                Throw New IllegalInputException("構成情報が存在しない", errorControls.ToArray)
            End If
        End Sub
        ''' <summary>
        ''' 構成に変更が無いことを確認する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <remarks>問題が有る場合、IllegalInputExceptionを投げる</remarks>
        ''' 
        Public Sub AssertValidateKoseiInsuCheck(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String)
            'エラーリスト定義'
            Dim errorControls As New List(Of ErrorControl)
            Dim dbCountError As Boolean
            Dim instlBuniNo As New Hashtable
            Dim frm As Frm41KouseiErrorDisp

            'Dim errorList As New List(Of Hashtable)
            dbCountError = False

            'INSTL品番毎にEBomの構成を取り出してスプレッドの構成と比較'
            For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                Dim errObjs As New List(Of List(Of TShisakuBuhinEditVo))      'エラー用のオブジェクト作成
                '構成の存在チェック'
                If subject.GetStructureResult(columnIndex) Is Nothing Then
                    Throw New InvalidProgramException("anEzTunnel.DetectStructureResultsしたあとに nothing が返る事はあり得ない")
                End If
                'EBomに存在しなければそのまま'
                '試作区分がNULLじゃなければ部品編集を見る'
                If StringUtil.IsEmpty(subject.InstlHinbanKbn(columnIndex)) Then
                    If subject.GetStructureResult(columnIndex).IsEBom Then
                        'EBomに存在すれば構成を取得'
                        '構成を取り出す'
                        Dim CheckBuhinKoseiMatrix As New Kosei.Logic.Matrix.BuhinKoseiMatrix
                        'これ自体は他のカラムとはかかわらないのでずれが無い'
                        '2012/01/15'
                        CheckBuhinKoseiMatrix = subject.GetNewKoseiMatrix(subject.GetStructureResult(columnIndex))

                        CheckBuhinKoseiMatrix.InstlColumn(columnIndex) = subject.removeJikyu(CheckBuhinKoseiMatrix.InstlColumn(columnIndex))


                        'ALL NOTHINGの項目が存在したのでそれだけ消す'
                        Dim count As New List(Of Integer)
                        count = CheckBuhinKoseiMatrix.GetInputRowIndexes

                        For Each index As Integer In CheckBuhinKoseiMatrix.GetInputRowIndexes
                            If CheckBuhinKoseiMatrix(index).BuhinNo Is Nothing And CheckBuhinKoseiMatrix(index).Level Is Nothing Then
                                CheckBuhinKoseiMatrix.RemoveRow(index)
                            End If
                        Next

                        'サブジェクト上の構成を取得'
                        Dim BuhinKoseiList As New Kosei.Logic.Matrix.BuhinKoseiMatrix
                        'コピーする'
                        Dim rowCounti As Integer = 0
                        For Each rowindex As Integer In subject.GetInputRowIndexes
                            '員数が無い場合は除く'
                            If Not subject.InsuSuryo(rowindex, columnIndex) Is Nothing Then
                                Dim err As New List(Of TShisakuBuhinEditVo)
                                Dim err1 As New TShisakuBuhinEditVo
                                Dim err2 As New TShisakuBuhinEditVo

                                BuhinKoseiList.InsertRow(rowCounti)
                                BuhinKoseiList.Record(rowCounti).Level = subject.Level(rowindex)
                                BuhinKoseiList(rowCounti).Level = subject.Level(rowindex)
                                BuhinKoseiList.Record(rowCounti).BuhinNo = subject.BuhinNo(rowindex)
                                BuhinKoseiList(rowCounti).BuhinNo = subject.BuhinNo(rowindex)

                                BuhinKoseiList.InsertColumn(columnIndex)
                                Dim IsExistsError As Boolean = False

                                'DBのレコード件数を超える場合、構成が変わっているはずなのでエラー扱いにする。
                                If rowCounti > CheckBuhinKoseiMatrix.InstlColumn(0).Count - 1 Then
                                    errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                                    dbCountError = True
                                Else
                                    'レベルと部品番号と員数をチェックする'
                                    'レベルのチェック'
                                    If StringUtil.Equals(BuhinKoseiList.Record(rowCounti).Level, CheckBuhinKoseiMatrix.InstlColumn(0).Record(rowCounti).Level) Then
                                    Else
                                        'レベルが異なる'
                                        errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                                        err2.BuhinNo = BuhinKoseiList.Record(rowCounti).BuhinNo
                                        err1.BuhinNo = CheckBuhinKoseiMatrix.InstlColumn(0).Record(rowCounti).BuhinNo
                                        err2.Level = BuhinKoseiList.Record(rowCounti).Level
                                        err1.Level = CheckBuhinKoseiMatrix.InstlColumn(0).Record(rowCounti).Level
                                        'err1.BuhinNote = BuhinKoseiList.InsuSuryo(rowCounti, columnIndex)
                                        'err2.BuhinNote = CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(rowCounti).InsuSuryo
                                        IsExistsError = True
                                    End If
                                    '部品番号のチェック'
                                    If StringUtil.Equals(BuhinKoseiList.Record(rowCounti).BuhinNo, CheckBuhinKoseiMatrix.InstlColumn(0).Record(rowCounti).BuhinNo) Then
                                    Else
                                        '部品番号が異なる'
                                        errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                                        err2.BuhinNo = BuhinKoseiList.Record(rowCounti).BuhinNo
                                        err1.BuhinNo = CheckBuhinKoseiMatrix.InstlColumn(0).Record(rowCounti).BuhinNo
                                        err2.Level = BuhinKoseiList.Record(rowCounti).Level
                                        err1.Level = CheckBuhinKoseiMatrix.InstlColumn(0).Record(rowCounti).Level
                                        'err1.BuhinNote = BuhinKoseiList.InsuSuryo(rowCounti, columnIndex)
                                        'err2.BuhinNote = CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(rowCounti).InsuSuryo
                                        IsExistsError = True
                                    End If
                                    '員数のチェック'
                                    If StringUtil.Equals(subject.InsuSuryo(rowindex, columnIndex), "**") Then
                                        BuhinKoseiList.InsuSuryo(rowCounti, columnIndex) = -1
                                    Else
                                        BuhinKoseiList.InsuSuryo(rowCounti, columnIndex) = Integer.Parse(subject.InsuSuryo(rowindex, columnIndex))
                                    End If

                                    If BuhinKoseiList.InsuSuryo(rowCounti, columnIndex) Is Nothing And CheckBuhinKoseiMatrix.InsuSuryo(rowCounti, 0) Is Nothing Then

                                    Else
                                        If BuhinKoseiList.InsuSuryo(rowCounti, columnIndex) = CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(rowCounti).InsuSuryo Then
                                            '変更なければそのまま'
                                        Else
                                            '但し、双方がマイナス員数の場合にはエラー扱にしない。
                                            If BuhinKoseiList.InsuSuryo(rowCounti, columnIndex) < 0 And CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(rowCounti).InsuSuryo < 0 Then
                                            Else
                                                '変更あればエラー扱い'
                                                errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                                                err2.BuhinNo = BuhinKoseiList.Record(rowCounti).BuhinNo
                                                err1.BuhinNo = CheckBuhinKoseiMatrix.InstlColumn(0).Record(rowCounti).BuhinNo
                                                err2.Level = BuhinKoseiList.Record(rowCounti).Level
                                                err1.Level = CheckBuhinKoseiMatrix.InstlColumn(0).Record(rowCounti).Level
                                                err2.BuhinNote = BuhinKoseiList.InsuSuryo(rowCounti, columnIndex)
                                                err1.BuhinNote = CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(rowCounti).InsuSuryo

                                                IsExistsError = True
                                            End If
                                        End If
                                    End If
                                End If

                                If IsExistsError Then
                                    err.Add(err1)
                                    err.Add(err2)
                                    errObjs.Add(err)
                                    err1 = Nothing
                                    err2 = Nothing
                                    err = Nothing
                                End If
                            Else
                                Continue For
                            End If
                            rowCounti = rowCounti + 1
                        Next

                    End If
                Else
                    '部品編集を見る'
                    'Dim impl As Dao.MakeStructureResultDao = New Dao.MakeStructureResultDaoImpl
                    'Dim List As New List(Of BuhinEditInstlKoseiVo)
                End If
                If errObjs.Count > 0 Then
                    instlBuniNo(subject.InstlHinban(columnIndex)) = errObjs
                    errObjs = Nothing
                End If
            Next
            If 0 < errorControls.Count Then
                If dbCountError = False Then
                    frm = New Frm41KouseiErrorDisp(instlBuniNo)
                    frm.ShowDialog()
                Else
                    MsgBox("E-BOM登録情報と構成がアンマッチです。" + vbCr + "INSTL品番を変更してください。", MsgBoxStyle.Information, "構成エラー")
                End If

                Throw New IllegalInputException("構成情報が存在しない", errorControls.ToArray)
            End If


        End Sub

        ''' <summary>
        ''' 構成に変更が無いことを確認する
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <remarks>問題が有る場合、IllegalInputExceptionを投げる</remarks>
        Public Sub AssertValidateKoseiInsuCheck(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String, ByVal JikyuUmu As String)
            'エラーリスト定義'
            Dim errorControls As New List(Of ErrorControl)
            'Dim errorList As New List(Of Hashtable)
            Dim instlBuniNo As New Hashtable
            Dim frm As Frm41KouseiErrorDisp

            'INSTL品番毎にEBomの構成を取り出してスプレッドの構成と比較'
            For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                Dim errObjs As New List(Of List(Of TShisakuBuhinEditVo))      'エラー用のオブジェクト作成

                '構成の存在チェック'
                If subject.GetStructureResult(columnIndex) Is Nothing Then
                    Throw New InvalidProgramException("anEzTunnel.DetectStructureResultsしたあとに nothing が返る事はあり得ない")
                End If
                'EBomに存在しなければそのまま'
                '試作区分がNULLじゃなければ部品編集を見る'
                If StringUtil.IsEmpty(subject.InstlHinbanKbn(columnIndex)) Then
                    If subject.GetStructureResult(columnIndex).IsEBom Then
                        'EBomに存在すれば構成を取得'
                        '構成を取り出す'
                        Dim CheckBuhinKoseiMatrix As New Kosei.Logic.Matrix.BuhinKoseiMatrix
                        'これ自体は他のカラムとはかかわらないのでずれが無い'
                        '2012/01/15'
                        CheckBuhinKoseiMatrix = subject.GetNewKoseiMatrix(subject.GetStructureResult(columnIndex))

                        CheckBuhinKoseiMatrix.InstlColumn(columnIndex) = subject.removeJikyu(CheckBuhinKoseiMatrix.InstlColumn(columnIndex))

                        'ALL NOTHINGの項目が存在したのでそれだけ消す'
                        Dim count As New List(Of Integer)
                        count = CheckBuhinKoseiMatrix.GetInputRowIndexes

                        For Each index As Integer In CheckBuhinKoseiMatrix.GetInputRowIndexes
                            If CheckBuhinKoseiMatrix(index).BuhinNo Is Nothing And CheckBuhinKoseiMatrix(index).Level Is Nothing Then
                                CheckBuhinKoseiMatrix.RemoveRow(index)
                            Else

                            End If
                        Next

                        'サブジェクト上の構成を取得'
                        Dim jikyuFlag As Boolean = True
                        Dim BuhinKoseiList As New Kosei.Logic.Matrix.BuhinKoseiMatrix
                        Dim counter As Integer = 0
                        For Each rowindex As Integer In subject.GetInputRowIndexes

                            '員数が存在するもののみ取得'
                            If Not subject.InsuSuryo(rowindex, columnIndex) Is Nothing Then
                                BuhinKoseiList.InsertRow(counter)
                                BuhinKoseiList.Record(counter).Level = subject.Level(rowindex)
                                BuhinKoseiList.Record(counter).BuhinNo = subject.BuhinNo(rowindex)
                                BuhinKoseiList.Record(counter).KyoukuSection = subject.KyoukuSection(rowindex)
                                BuhinKoseiList.Record(counter).MakerCode = subject.MakerCode(rowindex)
                                '2012/02/02 集計コードと海外集計コードを追加
                                BuhinKoseiList.Record(counter).ShukeiCode = subject.ShukeiCode(rowindex)
                                BuhinKoseiList.Record(counter).SiaShukeiCode = subject.SiaShukeiCode(rowindex)
                                BuhinKoseiList.InsuVo(counter, columnIndex).InsuSuryo = subject.InsuSuryo(rowindex, columnIndex)
                                counter = counter + 1
                            End If
                        Next

                        '比較を開始する'
                        '順番が異なっていてもエラーにしない'
                        For Each bindex As Integer In BuhinKoseiList.GetInputRowIndexes
                            '''''このループは入力されたスプレッドのループ
                            Dim IsExistsError As Boolean = False

                            Dim err As New List(Of TShisakuBuhinEditVo)
                            Dim err1 As New TShisakuBuhinEditVo
                            Dim err2 As New TShisakuBuhinEditVo
                            For cindex As Integer = 0 To CheckBuhinKoseiMatrix.InstlColumn(columnIndex).Count - 1
                                ''同じbindexで同じ位置を指すのであればこの条件で比較可能か？
                                ''レベルが違う
                                If Not (BuhinKoseiList(bindex).Level = CheckBuhinKoseiMatrix(bindex).Level) Then
                                    err1.BuhinNote = CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(cindex).InsuSuryo
                                    IsExistsError = True
                                End If
                                ''部品番号が違う
                                If Not (StringUtil.Equals(BuhinKoseiList.Record(bindex).BuhinNo, CheckBuhinKoseiMatrix.InstlColumn(0).Record(bindex).BuhinNo)) Then
                                    err1.BuhinNote = CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(cindex).InsuSuryo
                                    IsExistsError = True
                                End If
                                ''集計コードが違う
                                If Not (EzUtil.IsEqualIfNull(BuhinKoseiList.Record(bindex).ShukeiCode, CheckBuhinKoseiMatrix.InstlColumn(0).Record(bindex).ShukeiCode)) Then
                                    err1.BuhinNote = CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(cindex).InsuSuryo
                                    IsExistsError = True
                                End If

                                '''''このループは部品構成のループ
                                'レベルと部品番号と供給セクションを比較'
                                If BuhinKoseiList(bindex).Level = CheckBuhinKoseiMatrix(cindex).Level _
                                 AndAlso StringUtil.Equals(BuhinKoseiList.Record(bindex).BuhinNo, CheckBuhinKoseiMatrix.InstlColumn(0).Record(cindex).BuhinNo) _
                                 AndAlso StringUtil.Equals(BuhinKoseiList.Record(bindex).KyoukuSection, CheckBuhinKoseiMatrix.InstlColumn(0).Record(cindex).KyoukuSection) Then
                                    '集計コードと海外集計コードが同一か比較'

                                    If EzUtil.IsEqualIfNull(BuhinKoseiList.Record(bindex).ShukeiCode, CheckBuhinKoseiMatrix.InstlColumn(0).Record(cindex).ShukeiCode) _
                                    AndAlso EzUtil.IsEqualIfNull(BuhinKoseiList.Record(bindex).SiaShukeiCode, CheckBuhinKoseiMatrix.InstlColumn(0).Record(cindex).SiaShukeiCode) Then
                                        '同一の部品なら員数をチェック'
                                        If BuhinKoseiList.InsuSuryo(bindex, columnIndex) Is Nothing And CheckBuhinKoseiMatrix.InsuSuryo(cindex, 0) Is Nothing Then
                                            '両方存在しない場合は比較対象外'
                                        Else
                                            If BuhinKoseiList.InsuSuryo(bindex, columnIndex) = CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(cindex).InsuSuryo Then
                                                '変更なければそのまま'
                                                If Not IsExistsError Then
                                                    Continue For
                                                End If
                                            Else
                                                '但し、双方がマイナス員数の場合にはエラー扱にしない。
                                                If BuhinKoseiList.InsuSuryo(bindex, columnIndex) < 0 And CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(cindex).InsuSuryo < 0 Then
                                                    If Not IsExistsError Then
                                                        Continue For
                                                    End If
                                                Else
                                                    '変更あればエラー扱い'
                                                    errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                                                    err1.BuhinNote = CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(cindex).InsuSuryo
                                                    IsExistsError = True
                                                End If
                                            End If
                                        End If
                                    Else
                                        'ここでエラーにすると大量に引っかかるので'
                                        errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                                        Debug.Print(BuhinKoseiList.Record(bindex).BuhinNo)
                                        Debug.Print(CheckBuhinKoseiMatrix.InstlColumn(0).Record(cindex).BuhinNo)

                                    End If
                                End If
                                If IsExistsError Then
                                    Exit For
                                End If
                            Next
                            If IsExistsError Then
                                err1.Level = CheckBuhinKoseiMatrix(bindex).Level
                                err1.BuhinNo = CheckBuhinKoseiMatrix(bindex).BuhinNo
                                err1.ShukeiCode = CheckBuhinKoseiMatrix(bindex).ShukeiCode
                                err1.BuhinNote = CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(bindex).InsuSuryo
                                err2.Level = BuhinKoseiList(bindex).Level
                                err2.BuhinNo = BuhinKoseiList(bindex).BuhinNo
                                err2.ShukeiCode = BuhinKoseiList(bindex).ShukeiCode
                                err2.BuhinNote = BuhinKoseiList.InsuSuryo(bindex, columnIndex)
                                err.Add(err1)
                                err.Add(err2)
                                errObjs.Add(err)
                                err1 = Nothing
                                err2 = Nothing
                                err = Nothing
                            End If

                        Next
                        If errObjs.Count > 0 Then
                            instlBuniNo(subject.InstlHinban(columnIndex)) = errObjs
                            errObjs = Nothing
                        End If



                        'コピーする'
                        'Dim rowCounti As Integer = 0
                        'For Each rowindex As Integer In subject.GetInputRowIndexes
                        '    '員数が無い場合は除く'
                        '    If Not subject.InsuSuryo(rowindex, columnIndex) Is Nothing Then
                        '        BuhinKoseiList.InsertRow(rowCounti)
                        '        BuhinKoseiList.Record(rowCounti).Level = subject.Level(rowindex)
                        '        BuhinKoseiList(rowCounti).Level = subject.Level(rowindex)
                        '        BuhinKoseiList.Record(rowCounti).BuhinNo = subject.BuhinNo(rowindex)
                        '        BuhinKoseiList(rowCounti).BuhinNo = subject.BuhinNo(rowindex)

                        '        BuhinKoseiList.InsertColumn(columnIndex)

                        '        'DBのレコード件数を超える場合、構成が変わっているはずなのでエラー扱いにする。
                        '        'If rowCounti > CheckBuhinKoseiMatrix.InstlColumn(0).Count - 1 Then
                        '        '    errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                        '        'Else
                        '        'レベルと部品番号と員数をチェックする'
                        '        'レベルのチェック'
                        '        If StringUtil.Equals(BuhinKoseiList.Record(rowCounti).Level, CheckBuhinKoseiMatrix.InstlColumn(0).Record(rowCounti).Level) Then
                        '            '部品番号のチェック'
                        '            If StringUtil.Equals(BuhinKoseiList.Record(rowCounti).BuhinNo, CheckBuhinKoseiMatrix.InstlColumn(0).Record(rowCounti).BuhinNo) Then
                        '                '員数のチェック'
                        '                If StringUtil.Equals(subject.InsuSuryo(rowindex, columnIndex), "**") Then
                        '                    BuhinKoseiList.InsuSuryo(rowCounti, columnIndex) = -1
                        '                Else
                        '                    BuhinKoseiList.InsuSuryo(rowCounti, columnIndex) = Integer.Parse(subject.InsuSuryo(rowindex, columnIndex))
                        '                End If

                        '                If BuhinKoseiList.InsuSuryo(rowCounti, columnIndex) Is Nothing And CheckBuhinKoseiMatrix.InsuSuryo(rowCounti, 0) Is Nothing Then

                        '                Else
                        '                    If BuhinKoseiList.InsuSuryo(rowCounti, columnIndex) = CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(rowCounti).InsuSuryo Then
                        '                        '変更なければそのまま'
                        '                    Else
                        '                        '但し、双方がマイナス員数の場合にはエラー扱にしない。
                        '                        If BuhinKoseiList.InsuSuryo(rowCounti, columnIndex) < 0 And CheckBuhinKoseiMatrix.InstlColumn(0).InsuVo(rowCounti).InsuSuryo < 0 Then
                        '                        Else
                        '                            '変更あればエラー扱い'
                        '                            errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                        '                        End If
                        '                    End If
                        '                End If
                        '            Else
                        '                '部品番号が異なる'
                        '                errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                        '            End If
                        '        Else
                        '            'レベルが異なる'
                        '            errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                        '        End If
                        '    End If

                        '    'Else
                        '    'Continue For
                        '    'End If
                        '    rowCounti = rowCounti + 1
                        'Next

                    End If
                End If
            Next
            If instlBuniNo.Count > 0 Then
                frm = New Frm41KouseiErrorDisp(instlBuniNo)
                frm.ShowDialog()

            End If
            If 0 < errorControls.Count Then

                Throw New IllegalInputException("構成情報が存在しない", errorControls.ToArray)
            End If


        End Sub

        ''' <summary>
        '''構成の員数に変更が無いことを確認する(ワーニング)
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBukaCode">部課コード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <remarks>問題が有る場合、IllegalInputExceptionを投げる</remarks>
        Public Sub AssertValidateKoseiInsuCheckWarning(ByVal shisakuEventCode As String, ByVal shisakuBukaCode As String, ByVal shisakuBlockNo As String)
            'エラーリスト定義'
            Dim errorControls As New List(Of ErrorControl)
            Dim impl As Dao.MakeStructureResultDao = New Dao.MakeStructureResultDaoImpl

            Dim BuhinKoseiList As New Kosei.Logic.Matrix.BuhinKoseiMatrix
            Dim List As New List(Of BuhinEditInstlKoseiVo)

            'INSTL品番毎にEBomの構成を取り出してスプレッドの構成と比較'
            For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                '構成の存在チェック'
                If subject.GetStructureResult(columnIndex) Is Nothing Then
                    Throw New InvalidProgramException("anEzTunnel.DetectStructureResultsしたあとに nothing が返る事はあり得ない")
                End If

                'EBomに存在すればそのまま'
                If Not subject.GetStructureResult(columnIndex).IsEBom Then
                    'EBomに存在しなければ構成を部品編集情報から取得'
                    'イベントコード, 部課コード, ブロックNo, columnIndex(INSTL品番表示順)で検索'
                    List = New List(Of BuhinEditInstlKoseiVo)
                    List = impl.FindByBuhinEditInstlKosei(shisakuEventCode, shisakuBukaCode, shisakuBlockNo, subject.InstlHinban(columnIndex), subject.InstlHinbanKbn(columnIndex))

                    '部品編集情報にはいたが試作区分が新規で入力されたかチェック'
                    'リスト件数が０の場合、エラーになるので０以上に制限してみた。　2011/03/03　Ｂｙ柳沼
                    If List.Count > 0 Then

                        If List(0).Level = 0 Then
                            If StringUtil.IsEmpty(List(0).BuhinNoKbn) Then
                                If Not StringUtil.IsEmpty(subject.InstlHinbanKbn(columnIndex)) Then
                                    '取得したデータ上では試作区分が無いのに、登録するデータ上には試作区分が存在する'
                                    'つまり新規のINSTLなのでチェックしない'
                                    Continue For
                                End If

                                'どちらも空ならチェック'
                                If StringUtil.IsEmpty(subject.InstlHinbanKbn(columnIndex)) And StringUtil.IsEmpty(List(0).BuhinNoKbn) Then
                                    '構成が無いならEBomにも部品編集にも存在しない新規の品番'
                                    If List.Count = 0 Then
                                        Continue For
                                    Else
                                        'サブジェクト上の構成を取得'
                                        BuhinKoseiList = New Kosei.Logic.Matrix.BuhinKoseiMatrix
                                        'コピーする'
                                        Dim rowCountw As Integer = 0
                                        For Each rowindex As Integer In subject.GetInputRowIndexes
                                            '員数が無い場合は除く'
                                            If Not subject.InsuSuryo(rowindex, columnIndex) Is Nothing Then
                                                BuhinKoseiList.InsertRow(rowCountw)
                                                BuhinKoseiList.InsertColumn(columnIndex)

                                                If StringUtil.Equals(subject.InsuSuryo(rowindex, columnIndex), "**") Then
                                                    BuhinKoseiList.InsuSuryo(rowCountw, columnIndex) = -1
                                                Else
                                                    BuhinKoseiList.InsuSuryo(rowCountw, columnIndex) = Integer.Parse(subject.InsuSuryo(rowindex, columnIndex))
                                                End If

                                                If BuhinKoseiList.InsuSuryo(rowCountw, columnIndex) = List(rowCountw).InsuSuryo Then
                                                    '変更なければそのまま'
                                                Else
                                                    '変更あればエラー扱い'
                                                    errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                                                End If
                                            Else
                                                Continue For
                                            End If

                                            rowCountw = rowCountw + 1
                                        Next

                                    End If
                                    '構成の数が違う場合、エラー扱いする'
                                    If BuhinKoseiList.GetInputRowIndexes.Count = List.Count Then

                                    Else
                                        errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                                    End If
                                End If

                            Else
                                '試作区分が同じ場合チェック'
                                If StringUtil.Equals(subject.InstlHinbanKbn(columnIndex), List(0).BuhinNoKbn) Then
                                    '構成が無いならEBomにも部品編集にも存在しない新規の品番'
                                    If List.Count = 0 Then
                                        Continue For
                                    Else
                                        'サブジェクト上の構成を取得'
                                        BuhinKoseiList = New Kosei.Logic.Matrix.BuhinKoseiMatrix
                                        'コピーする'
                                        Dim rowCountw2 As Integer = 0

                                        For Each rowindex As Integer In subject.GetInputRowIndexes
                                            If List.Count < rowCountw2 Then
                                                errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                                                Exit For
                                            End If
                                            '員数が無い場合は除く'
                                            If Not subject.InsuSuryo(rowindex, columnIndex) Is Nothing Then
                                                'BuhinKoseiList.Record(a).Level = subject.Level(rowindex)
                                                'BuhinKoseiList.Record(a).ShukeiCode = subject.ShukeiCode(rowindex)
                                                'BuhinKoseiList.Record(a).SiaShukeiCode = subject.SiaShukeiCode(rowindex)
                                                'BuhinKoseiList.Record(a).GencyoCkdKbn = subject.GencyoCkdKbn(rowindex)
                                                'BuhinKoseiList.Record(a).MakerCode = subject.MakerCode(rowindex)
                                                'BuhinKoseiList.Record(a).MakerName = subject.MakerName(rowindex)
                                                'BuhinKoseiList.Record(a).BuhinNo = subject.BuhinNo(rowindex)
                                                'BuhinKoseiList.Record(a).BuhinNoKaiteiNo = subject.BuhinNoKaiteiNo(rowindex)
                                                BuhinKoseiList.InsertRow(rowCountw2)
                                                BuhinKoseiList.InsertColumn(columnIndex)

                                                If StringUtil.Equals(subject.InsuSuryo(rowindex, columnIndex), "**") Then
                                                    BuhinKoseiList.InsuSuryo(rowCountw2, columnIndex) = -1
                                                Else
                                                    BuhinKoseiList.InsuSuryo(rowCountw2, columnIndex) = Integer.Parse(subject.InsuSuryo(rowindex, columnIndex))
                                                End If

                                                Try
                                                    If BuhinKoseiList.InsuSuryo(rowCountw2, columnIndex) = List(rowCountw2).InsuSuryo Then
                                                        '変更なければそのまま'
                                                    Else
                                                        '変更あればエラー扱い'
                                                        errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                                                    End If
                                                Catch ex As Exception
                                                    errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                                                End Try

                                            Else
                                                Continue For
                                            End If
                                            rowCountw2 = rowCountw2 + 1
                                        Next
                                    End If
                                    '構成の数が違う場合、エラー扱いする'
                                    If BuhinKoseiList.GetInputRowIndexes.Count = List.Count Then

                                    Else
                                        errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
                                    End If
                                Else
                                    '異なる場合はどうする？'
                                    'とりあえずスルーさせておく'
                                End If

                            End If
                        End If
                    End If
                End If
            Next
            If 0 < errorControls.Count Then

                Throw New IllegalInputException("構成情報が存在しない", errorControls.ToArray)
            End If


        End Sub

        ''' <summary>
        ''' 取引先コードの未入力チェック
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AssertValidateKoseiMakerCodeAndNameCheck()
            'エラーリスト定義'
            Dim errorControls As New List(Of ErrorControl)
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo = eventDao.FindByPk(subject.EventCode)

            For Each row As Integer In subject.GetInputRowIndexes
                Dim rowindex As Integer = row + titleRows
                Dim shukeiCode As String = sheet.Cells(rowindex, sheet.Columns(TAG_SHUKEI_CODE).Index).Value

                If Not (eventVo.BlockAlertKind = "2" And eventVo.KounyuShijiFlg = "0" And subject.BaseBuhinFlg(row) = 1) Then
                    '集計コードX,Rか「X,R,J」の場合エラーチェックしない'
                    If StringUtil.IsNotEmpty(shukeiCode) Then
                        Select Case shukeiCode
                            Case "A", "E", "Y"
                                '取引先名称が空で無いならチェックしなくてもよい'
                                'If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_NAME).Index).Value) Then
                                If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_CODE).Index).Value) Then
                                    errorControls.AddRange(MakeErrorControlsMakerCode(rowindex))
                                End If
                                'End If
                        End Select

                    Else
                        Dim siaShukeiCode As String = sheet.Cells(rowindex, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value
                        If StringUtil.IsNotEmpty(siaShukeiCode) Then
                            Select Case siaShukeiCode
                                Case "A", "E", "Y"
                                    'If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_NAME).Index).Value) Then
                                    If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_CODE).Index).Value) Then
                                        errorControls.AddRange(MakeErrorControlsMakerCode(rowindex))
                                    End If
                                    'End If
                            End Select
                        End If
                    End If

                End If

            Next

            If 0 < errorControls.Count Then
                Throw New IllegalInputException("構成情報が存在しない", errorControls.ToArray)
            End If
        End Sub

        '2012/01/27 供給セクションのチェックを追加

        ''' <summary>
        ''' 供給セクションの未入力チェック
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AssertValidateKoseiKyoukuSection()
            'エラーリスト定義'
            Dim errorControls As New List(Of ErrorControl)
            For Each rowindex As Integer In subject.GetInputRowIndexes
                Dim shukeiCode As String = sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).Value
                '集計コードA,E,Yの場合のみ未入力チェック'
                If Not StringUtil.IsEmpty(shukeiCode) Then
                    Select Case shukeiCode
                        Case "A", "E", "Y"
                            '取引先名称が空で無いならチェックしなくてもよい'
                            If StringUtil.IsEmpty(sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value) Then
                                errorControls.AddRange(MakeErrorControlsKyoukuSection(rowindex + titleRows))
                            End If
                    End Select

                Else
                    Dim siaShukeiCode As String = sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value
                    If Not StringUtil.IsEmpty(siaShukeiCode) Then
                        Select Case siaShukeiCode
                            Case "A", "E", "Y"
                                If StringUtil.IsEmpty(sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value) Then
                                    errorControls.AddRange(MakeErrorControlsKyoukuSection(rowindex + titleRows))
                                End If
                        End Select
                    End If
                End If
            Next

            If 0 < errorControls.Count Then
                Throw New IllegalInputException("構成情報が存在しない", errorControls.ToArray)
            End If
        End Sub

        ''' <summary>
        ''' 品番の色記号チェック
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AssertValidateKoseiHinban()
            ''2015/08/17 追加 E.Ubukata Ver 2.10.8
            '' 色記号入力チェック追加
            Dim errorControls As New List(Of ErrorControl)
            For Each rowindex As Integer In subject._koseiMatrix.GetInputRowIndexes
                Dim key As String = subject._koseiMatrix.Record(rowindex).BuhinNo
                If key.Substring(key.Length - 2, 2).Equals("##") Then
                    errorControls.AddRange(MakeErrorControlsHinban(rowindex + titleRows))
                End If
            Next
            If 0 < errorControls.Count Then
                Throw New IllegalInputException("色記号を入力してください。", errorControls.ToArray)
            End If

        End Sub



        ''' <summary>
        ''' 「最新化」処理でINSTL品番の構成情報が存在する事を実証する(単列)
        ''' </summary>
        ''' <param name="column">列の番号</param>
        ''' <remarks>問題が有る場合、IllegalInputExceptionを投げる</remarks>
        Public Sub AssertValidateNewChange(ByVal column As Integer)
            Dim errorControls As New List(Of ErrorControl)
            column = column - INSTL_INFO_START_COLUMN
            If subject.GetStructureResult(column) Is Nothing Then
                Throw New InvalidProgramException("anEzTunnel.DetectStructureResultsしたあとに nothing が返る事はあり得ない")
            End If
            If subject.GetStructureResult(column).IsExist Then
                Return
            End If
            errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(column))

            If 0 < errorControls.Count Then
                Throw New IllegalInputException("構成情報が存在しない", errorControls.ToArray)
            End If
        End Sub

        ''' <summary>
        ''' 「最新化」処理でINSTL品番の構成情報が存在する事を実証する(複数列)
        ''' </summary>
        ''' <param name="columnHead">選択列の先頭</param>
        ''' <param name="columnTail">選択列の末尾</param>
        ''' <remarks>問題が有る場合、IllegalInputExceptionを投げる</remarks>
        Public Sub AssertValidateNewChangeRange(ByVal columnHead As Integer, ByVal columnTail As Integer)
            Dim errorControls As New List(Of ErrorControl)
            '内部処理用にマイナスする'
            columnHead = columnHead - INSTL_INFO_START_COLUMN
            columnTail = columnTail - INSTL_INFO_START_COLUMN

            For columnIndex As Integer = columnHead To columnTail
                If subject.GetStructureResult(columnIndex) Is Nothing Then
                    Throw New InvalidProgramException("anEzTunnel.DetectStructureResultsしたあとに nothing が返る事はあり得ない")
                End If
                If subject.GetStructureResult(columnIndex).IsExist Then
                    Continue For
                End If
                errorControls.AddRange(MakeErrorControlsInstlHinbanAndKbn(columnIndex))
            Next
            If 0 < errorControls.Count Then
                Throw New IllegalInputException("構成情報が存在しない", errorControls.ToArray)
            End If
        End Sub

        ''' <summary>
        ''' INSTL品番と試作区分とをエラーとしたErrorControlを作成する
        ''' </summary>
        ''' <param name="columnIndexes">エラーにするSubject列index</param>
        ''' <returns>作成したErrorControl</returns>
        ''' <remarks></remarks>
        Private Function MakeErrorControlsInstlHinbanAndKbn(ByVal ParamArray columnIndexes As Integer()) As ErrorControl()

            Dim spreadRows As List(Of Integer) = New List(Of Integer)(New Integer() {INSTL_HINBAN_ROW_INDEX, INSTL_HINBAN_KBN_ROW_INDEX})
            Dim spreadColumns As List(Of Integer) = ConvKoseiInstlToSpreadColumn(New List(Of Integer)(columnIndexes))
            Return ValidatorUtil.MakeErrorControls(spread, spreadRows, spreadColumns)
        End Function

        ''' <summary>
        ''' 取引先コードをエラーとしたErrorContorlを作成する
        ''' </summary>
        ''' <param name="rowIndex">行番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MakeErrorControlsMakerCode(ByVal rowIndex As Integer) As ErrorControl()
            Dim spreadRows As Integer = rowIndex
            Dim spreadColumns As Integer = sheet.Columns(TAG_MAKER_CODE).Index
            Return ValidatorUtil.MakeErrorControls(spread, spreadRows, spreadColumns)
        End Function

        ''' <summary>
        ''' 供給セクションをエラーとしたErrorContorlを作成する
        ''' </summary>
        ''' <param name="rowIndex">行番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MakeErrorControlsKyoukuSection(ByVal rowIndex As Integer) As ErrorControl()
            Dim spreadRows As Integer = rowIndex
            Dim spreadColumns As Integer = sheet.Columns(TAG_KYOUKU_SECTION).Index
            Return ValidatorUtil.MakeErrorControls(spread, spreadRows, spreadColumns)
        End Function

        ''' <summary>
        ''' 供給セクションをエラーとしたErrorContorlを作成する
        ''' </summary>
        ''' <param name="rowIndex">行番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MakeErrorControlsHinban(ByVal rowIndex As Integer) As ErrorControl()
            Dim spreadRows As Integer = rowIndex
            Dim spreadColumns As Integer = sheet.Columns(TAG_BUHIN_NO).Index
            Return ValidatorUtil.MakeErrorControls(spread, spreadRows, spreadColumns)
        End Function

#Region "フィルタリング中かチェックする"
        ''' <summary>
        ''' フィルタ解除
        ''' </summary>
        ''' <remarks></remarks>
        Public Function FilterCheck()

            Dim rtnFilter As String = Nothing

            '基本情報列タイトルの色をチェック
            For i As Integer = 0 To sheet.ColumnCount - 1

                '青色か？
                If sheet.Cells(ALL_MIDASHI_ROW_INDEX, i, INSTL_HINBAN_ROW_INDEX, i).ForeColor = Color.Blue Then
                    rtnFilter = "F"
                End If

            Next

            Return rtnFilter

        End Function

#End Region


#Region "ボタン（フィルタ解除）"

        ''' <summary>
        ''' フィルタ解除
        ''' </summary>
        ''' <param name="sIndex">開始インデックス</param>
        ''' <param name="eIndex">終了インデックス</param>
        ''' <remarks></remarks>
        Public Sub ResetFilter(ByVal sIndex As Integer, ByVal eIndex As Integer)

            Dim startRow As Integer = 4

            If condVoList.Count = 0 Then
                Return
            End If

            '列番号に該当する全ての条件を削除する'
            Dim count As Integer = condVoList.Count

            '対象列のフィルタ条件を削除する'
            delconList(condVoList, sIndex)

            '一度、行フィルタ解除
            For i As Integer = startRow To sheet.Rows.Count - 1
                sheet.Rows(i).Visible = True
                sheet.RowHeader.Rows(i).ForeColor = Nothing
            Next

            '残りの条件すべてに該当したもの以外は解除'

            For i As Integer = startRow To sheet.Rows.Count - 1
                If condVoList.Count = 0 Then
                    Exit For
                End If
                Dim re As Boolean = False
                Dim countA As Integer = 0
                For index As Integer = 0 To condVoList.Count - 1
                    If StringUtil.Equals(condVoList(index).Condition, StringUtil.ToString(sheet.Cells(i, condVoList(index).ColumnIndex).Value)) Then
                        '条件に該当したものは'
                        re = True
                    Else
                        countA = countA + 1
                        Dim deb As String = ""
                    End If
                Next

                '条件に該当しなかったものは再度フィルタ'
                If countA <= condVoList.Count Then
                    sheet.Rows(i).Visible = False
                    sheet.RowHeader.Rows(i).ForeColor = Nothing
                End If
            Next

            '解除対象を除いた現在のシートの結果'

            '行フィルタ解除

            If sIndex = eIndex Then
                sheet.Cells(ALL_MIDASHI_ROW_INDEX, sIndex, INSTL_HINBAN_ROW_INDEX, eIndex).ForeColor = Nothing
            Else
                '基本情報列タイトル色戻し
                For i As Integer = sIndex To eIndex
                    Dim objFont As System.Drawing.Font = sheet.Cells(INSTL_HINBAN_ROW_INDEX, i).Font
                    '太字にされているセルは編集済なので色は戻さない
                    If Not objFont Is Nothing AndAlso objFont.Bold = True Then
                        Continue For
                    End If
                    '色を戻す
                    sheet.Cells(ALL_MIDASHI_ROW_INDEX, i, INSTL_HINBAN_ROW_INDEX, i).ForeColor = Nothing
                Next
            End If




            'スプレッドに対してのコピーショートカットキー（Ctrl +X)を無効に（コード上でコピーを処理する為必要な処置)
            'スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
            'スプレッドに対してのコピーショートカットキー（Ctrl +V)を無効に（コード上でコピーを処理する為必要な処置)

            If Not StringUtil.Equals(FilterCheck(), "F") Then
                Dim spreadVisible As FpSpread = spread
                Dim imSpread As New FarPoint.Win.Spread.InputMap
                imSpread = spreadVisible.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
                imSpread.Put(New FarPoint.Win.Spread.Keystroke(Keys.X, Keys.Control), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
                'スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
                imSpread = spreadVisible.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
                imSpread.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
                'スプレッドに対してのコピーショートカットキー（Ctrl +V)を無効に（コード上でコピーを処理する為必要な処置)
                imSpread = spreadVisible.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
                imSpread.Put(New FarPoint.Win.Spread.Keystroke(Keys.V, Keys.Control), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
            End If

        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="conList"></param>
        ''' <param name="index"></param>
        ''' <remarks></remarks>
        Private Sub delconList(ByVal conList As List(Of clsOptionFilterVo), ByVal index As Integer)
            For Each colindex As clsOptionFilterVo In conList
                If colindex.ColumnIndex = index Then
                    condVoList.Remove(colindex)
                    delconList(conList, index)
                    Exit For
                End If
            Next
        End Sub

        ''' <summary>
        ''' フィルタ解除
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ResetFilterAll()

            Dim startRow As Integer = 4
            '初期化する'
            condVoList = New List(Of clsOptionFilterVo)

            '基本情報列タイトル色戻し
            For i As Integer = 0 To sheet.ColumnCount - 1
                Dim objFont As System.Drawing.Font = sheet.Cells(INSTL_HINBAN_ROW_INDEX, i).Font

                '太字にされているセルは編集済なので色は戻さない
                If Not objFont Is Nothing AndAlso objFont.Bold = True Then
                    Continue For
                End If

                '色を戻す
                sheet.Cells(ALL_MIDASHI_ROW_INDEX, i, INSTL_HINBAN_ROW_INDEX, i).ForeColor = Nothing

            Next

            '行フィルタ解除
            For i As Integer = startRow To sheet.Rows.Count - 1
                sheet.Rows(i).Visible = True
                sheet.RowHeader.Rows(i).ForeColor = Nothing
            Next

            'スプレッドに対してのコピーショートカットキー（Ctrl +X)を無効に（コード上でコピーを処理する為必要な処置)
            'スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
            'スプレッドに対してのコピーショートカットキー（Ctrl +V)を無効に（コード上でコピーを処理する為必要な処置)
            Dim spreadVisible As FpSpread = spread
            Dim imSpread As New FarPoint.Win.Spread.InputMap
            imSpread = spreadVisible.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imSpread.Put(New FarPoint.Win.Spread.Keystroke(Keys.X, Keys.Control), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
            'スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
            imSpread = spreadVisible.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imSpread.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
            'スプレッドに対してのコピーショートカットキー（Ctrl +V)を無効に（コード上でコピーを処理する為必要な処置)
            imSpread = spreadVisible.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            imSpread.Put(New FarPoint.Win.Spread.Keystroke(Keys.V, Keys.Control), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)

        End Sub



#End Region



#Region "ボタン（フィルタ設定処理）"
        ''' <summary>
        ''' フィルタ設定処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetFiltering()

            Dim resultRow As Integer() = Nothing
            Dim colNo As Integer
            Dim startRow As Integer = 4

            Dim optionFilter As OptionFilter = New OptionFilter(sheet, String.Empty, startRow)

            'フィルタ対処列取得
            colNo = sheet.ActiveColumn.Index
            optionFilter.ShowInDropDown(colNo, Nothing)

            'フィルタ実行
            resultRow = optionFilter.Filter(colNo)

            '全て取得していればキャンセルとみなす
            If resultRow.Length = sheet.Rows.Count Then
                Exit Sub
            End If

            '列見出しを青にする
            sheet.Cells(ALL_MIDASHI_ROW_INDEX, colNo, INSTL_HINBAN_ROW_INDEX, colNo).ForeColor = Color.Blue

            For i As Integer = startRow To sheet.Rows.Count - 1

                Dim findFlag As Boolean = False
                Dim cVo As New clsOptionFilterVo

                'フィルタ条件該当データの中に現在ループ中のスプレッド行が該当するか判定
                For j As Integer = 0 To resultRow.Length - 1
                    If resultRow(j) = i Then
                        findFlag = True
                        Exit For
                    End If
                Next

                If findFlag = False Then
                    sheet.Rows(i).Visible = False
                    sheet.RowHeader.Rows(i).ForeColor = Nothing
                Else
                    '条件リストに追加'
                    cVo.ColumnIndex = colNo
                    cVo.Condition = sheet.Cells(i, colNo).Value
                    If Not StringUtil.IsEmpty(cVo.Condition) Then
                        '条件にいないこと'
                        If condVoList.Count = 0 Then
                            condVoList.Add(cVo)
                        Else
                            Dim a As Boolean = False
                            For index As Integer = 0 To condVoList.Count - 1
                                If StringUtil.Equals(condVoList(index).Condition, StringUtil.ToString(sheet.Cells(i, colNo).Value)) Then
                                    If condVoList(index).ColumnIndex = colNo Then
                                        a = True
                                    End If
                                End If
                            Next
                            '該当する条件が一件もなければ追加'
                            If Not a Then
                                condVoList.Add(cVo)
                            End If
                        End If
                    End If
                    sheet.RowHeader.Rows(i).ForeColor = Color.Blue
                End If
            Next

            'スプレッドに対してのコピーショートカットキー（Ctrl +X)を無効に（コード上でコピーを処理する為必要な処置)
            Dim spreadVisible As FpSpread = spread
            Dim im As New FarPoint.Win.Spread.InputMap
            im = spreadVisible.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.X, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)
            'スプレッドに対してのコピーショートカットキー（Ctrl +C)を無効に（コード上でコピーを処理する為必要な処置)
            im = spreadVisible.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.C, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)
            'スプレッドに対してのコピーショートカットキー（Ctrl +V)を無効に（コード上でコピーを処理する為必要な処置)
            im = spreadVisible.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.V, Keys.Control), FarPoint.Win.Spread.SpreadActions.None)

        End Sub

#End Region

        ''' <summary>
        ''' INSTL品番列の挿入・初期化
        ''' </summary>
        ''' <remarks>
        ''' InstlColumnCountを隠蔽する<br/>
        ''' けれど「CellTypeは行に設定済み」とか、結構依存している. 上手に切り分けが出来ない. 困った.
        ''' </remarks>
        Private Class InstlColumns
            Private ReadOnly sheet As SheetView
            Private ReadOnly borderFactory As SpreadBorderFactory
            Private _instlColumnCount As Integer = 0

            ''' <summary>INSTL列数</summary>
            Public ReadOnly Property InstlColumnCount() As Integer
                Get
                    Return _instlColumnCount
                End Get
            End Property

            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="sheet">Sheet</param>
            ''' <param name="borderFactory">罫線Factory</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal sheet As SheetView, ByVal borderFactory As SpreadBorderFactory)
                Me.sheet = sheet
                Me.borderFactory = borderFactory
            End Sub

            ''' <summary>
            ''' INSTL品番列を初期設定する
            ''' </summary>
            ''' <param name="columnCount">INSTL品番の初期列数</param>
            ''' <remarks></remarks>
            Public Sub InitializeColumnInstl(ByVal columnCount As Integer)
                InsertColumnInstl(0, columnCount)
            End Sub
            ''' <summary>
            ''' INSTL品番列を挿入する
            ''' </summary>
            ''' <param name="baseInstlColumnIndex">基点となるINSTL品番列index (0 start)</param>
            ''' <param name="columnCount">挿入列数</param>
            ''' <remarks></remarks>
            Public Sub InsertColumnInstl(ByVal baseInstlColumnIndex As Integer, ByVal columnCount As Integer)

                Dim spreadStartColumn As Integer = INSTL_INFO_START_COLUMN + baseInstlColumnIndex
                If Me._instlColumnCount < baseInstlColumnIndex Then
                    Throw New ArgumentOutOfRangeException("baseInstlColumnIndex", baseInstlColumnIndex, Me._instlColumnCount & " より大きい値は、指定出来ません")
                End If
                Dim oldInstlColumnCount As Integer = Me._instlColumnCount
                InsertColumns(spreadStartColumn, columnCount)

                ' ** 挿入した列に新規設定 **
                For columnIndex As Integer = 0 To columnCount - 1
                    Dim spreadColumn As Integer = spreadStartColumn + columnIndex
                    ' 列
                    With sheet.Columns(spreadColumn)
                        .CellType = GetInstlInsuCellType()
                        .Width = 20.0!
                        .HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                    End With
                    ' セル（INSTL品番）
                    With sheet.Cells(INSTL_HINBAN_ROW_INDEX, spreadColumn)
                        ' .CellTypeは行に設定済み
                        .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
                    End With
                    ' セル（INSTL品番区分）
                    With sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, spreadColumn)
                        ' .CellTypeは行に設定済み
                        .Border = borderFactory.GetUnderLine()
                        .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
                        .RowSpan = 1 ' ALと違う
                    End With
                    ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(7)) (TES)張 ADD BEGIN
                    ' セル（INSTL元データ区分）																		
                    With sheet.Cells(INSTL_DATA_KBN_ROW_INDEX, spreadColumn)
                        ' .CellTypeは行に設定済み																		

                        .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
                    End With
                    ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(7)) (TES)張 ADD END
                Next

                ' ** 挿入した列と、その右の既存の列に設定 **
                For spreadColumn As Integer = INSTL_INFO_START_COLUMN + baseInstlColumnIndex To INSTL_INFO_START_COLUMN + oldInstlColumnCount + columnCount - 1
                    ' セル（"A"とか"AB"とか）
                    With sheet.Cells(INSTL_HINBAN_NO_ROW_INDEX, spreadColumn)
                        ' .CellTypeは行に設定済み
                        .Border = borderFactory.GetUnderLine()
                        .Value = EzUtil.ConvIndexToAlphabet(spreadColumn - INSTL_INFO_START_COLUMN)
                    End With
                Next

                With sheet.Cells(INSTL_HINBAN_MIDASHI_ROW_INDEX, INSTL_INFO_START_COLUMN)
                    ' .CellTypeは行に設定済み
                    .BackColor = System.Drawing.SystemColors.Control
                    .Border = borderFactory.GetTopWLine() ' ALと違う
                    .ColumnSpan = oldInstlColumnCount + columnCount
                    .Value = "員数" ' ALと違う
                End With

                Me._instlColumnCount += columnCount
            End Sub

            Private InstlInsuCellType As TextCellType
            ''' <summary>
            ''' INSTL品番員数を返す
            ''' </summary>
            ''' <returns>INSTL品番員数セル</returns>
            ''' <remarks></remarks>
            Public Function GetInstlInsuCellType() As TextCellType
                If InstlInsuCellType Is Nothing Then

                    InstlInsuCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                    InstlInsuCellType.MaxLength = 2
                End If
                Return InstlInsuCellType
            End Function

            Private Sub RemoveColumns(ByVal baseColumnNo As Integer, ByVal removeCount As Integer)
                If removeCount = 0 Then
                    Return
                End If
                sheet.RemoveColumns(baseColumnNo, removeCount)
            End Sub
            Private Sub InsertColumns(ByVal baseColumnNo As Integer, ByVal insertCount As Integer)
                If insertCount = 0 Then
                    Return
                End If
                sheet.AddColumns(baseColumnNo, insertCount)
            End Sub

        End Class

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_q) (TES)張 ADD BEGIN
        Public Sub BaseInfoColumnVisible()
            For Each index As Integer In subject.GetInputInstlHinbanColumnIndexes()
                If subject.BaseInstlFlg(index) = 1 Then
                    Dim spreadColumn As Integer = ConvKoseiInstlToSpreadColumn(index)
                    sheet.Columns(spreadColumn).Visible = True
                End If
            Next
        End Sub
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_q) (TES)張 ADD END

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_r) (TES)張 ADD BEGIN
        Public Sub BaseInfoColumnDisable()
            For Each index As Integer In subject.GetInputInstlHinbanColumnIndexes()
                If subject.BaseInstlFlg(index) = 1 Then
                    Dim spreadColumn As Integer = ConvKoseiInstlToSpreadColumn(index)
                    sheet.Columns(spreadColumn).Visible = False
                End If
            Next
        End Sub
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_r) (TES)張 ADD END


        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_p) (TES)張 ADD BEGIN
        ''' <summary>作り方・製作方法</summary>
        ''' <returns>作り方・製作方法</returns>
        Private Function TsukurikataSeisakuCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.GetLabelValues_TsukurikataSeisaku)
            result.MaxLength = 16
            ''↓↓2014/09/08 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            '下記バグと思われる現象回避のため実装
            'ドロップダウンリスト表示→スクロールバー操作→（アイテム選択せずに）別のセルをクリック→エラー発生
            result.MaxDrop = subject.GetLabelValues_TsukurikataSeisaku.Count + 1
            ''↑↑2014/09/08 Ⅰ.2.管理項目追加 酒井 ADD END
            Return result
        End Function
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_p) (TES)張 ADD END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_r) (TES)張 ADD BEGIN
        ''' <summary>作り方・型仕様１</summary>
        ''' <returns>作り方・型仕様１</returns>
        Private Function TsukurikataKatashiyou1CellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.GetLabelValues_TsukurikataKatashiyou1)
            result.MaxLength = 16
            ''↓↓2014/09/08 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            '下記バグと思われる現象回避のため実装
            'ドロップダウンリスト表示→スクロールバー操作→（アイテム選択せずに）別のセルをクリック→エラー発生
            result.MaxDrop = subject.GetLabelValues_TsukurikataKatashiyou1.Count + 1
            ''↑↑2014/09/08 Ⅰ.2.管理項目追加 酒井 ADD END

            Return result
        End Function

        ''' <summary>作り方・型仕様２</summary>
        ''' <returns>作り方・型仕様２</returns>
        Private Function TsukurikataKatashiyou2CellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.GetLabelValues_TsukurikataKatashiyou2)
            result.MaxLength = 16
            ''↓↓2014/09/08 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            '下記バグと思われる現象回避のため実装
            'ドロップダウンリスト表示→スクロールバー操作→（アイテム選択せずに）別のセルをクリック→エラー発生
            result.MaxDrop = subject.GetLabelValues_TsukurikataKatashiyou2.Count + 1
            ''↑↑2014/09/08 Ⅰ.2.管理項目追加 酒井 ADD END
            Return result
        End Function

        ''' <summary>作り方・型仕様３</summary>
        ''' <returns>作り方・型仕様３</returns>
        Private Function TsukurikataKatashiyou3CellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.GetLabelValues_TsukurikataKatashiyou3)
            result.MaxLength = 16
            ''↓↓2014/09/08 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            '下記バグと思われる現象回避のため実装
            'ドロップダウンリスト表示→スクロールバー操作→（アイテム選択せずに）別のセルをクリック→エラー発生
            result.MaxDrop = subject.GetLabelValues_TsukurikataKatashiyou3.Count + 1
            ''↑↑2014/09/08 Ⅰ.2.管理項目追加 酒井 ADD END
            Return result
        End Function
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_r) (TES)張 ADD END

        ''↓↓2014/07/23 Ⅰ.2.管理項目追加_s) (TES)張 ADD BEGIN
        ''' <summary>作り方・冶具</summary>
        ''' <returns>作り方・冶具</returns>
        Private Function TsukurikataTiguCellType() As ComboBoxCellType
            Dim result As ComboBoxCellType = ShisakuSpreadUtil.NewGeneralComboBoxCellType()
            SpreadUtil.BindLabelValuesToComboBoxCellType(result, subject.GetLabelValues_TsukurikataTigu)
            result.MaxLength = 16
            ''↓↓2014/09/08 Ⅰ.2.管理項目追加 酒井 ADD BEGIN
            '下記バグと思われる現象回避のため実装
            'ドロップダウンリスト表示→スクロールバー操作→（アイテム選択せずに）別のセルをクリック→エラー発生
            result.MaxDrop = subject.GetLabelValues_TsukurikataTigu.Count + 1
            ''↑↑2014/09/08 Ⅰ.2.管理項目追加 酒井 ADD END
            Return result
        End Function
        ''↑↑2014/07/23 Ⅰ.2.管理項目追加_s) (TES)張 ADD END

        ''↓↓2014/08/22 ツールチップを追加 (TES)張 ADD BEGIN
        ''' <summary>
        ''' 作り方列のToolTip表示実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class ToolTipImpl : Implements SpreadToolTip.AnEvent
            Private ReadOnly _sheet As SheetView
            Public Sub New(ByVal sheet As SheetView)
                Me._sheet = sheet
            End Sub
            Public Sub TextTipFetch(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.TextTipFetchEventArgs) Implements SpreadToolTip.AnEvent.TextTipFetch
                If e.Column >= _sheet.Columns(TAG_TSUKURIKATA_SEISAKU).Index And _
                        e.Column <= _sheet.Columns(TAG_TSUKURIKATA_KIBO).Index Then
                    If e.Row > INSTL_HINBAN_KBN_ROW_INDEX Then
                        e.TipText = "作り方を入力してください"
                        e.ShowTip = True
                    Else
                        e.ShowTip = False
                    End If
                End If
            End Sub
        End Class
        ''↑↑2014/08/22 ツールチップを追加 (TES)張 ADD END

        ''↓↓2014/08/22 Ⅰ.3.設計編集 ベース改修専用化_ch) (TES)張 ADD BEGIN
        ''' <summary></summary>
        ''' <param name="spreadColumn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Function IsInputInstlColumn(ByVal spreadColumn As Integer) As Boolean
            Dim result As Integer = 0
            For Each i As Integer In subject.GetInputInstlHinbanColumnIndexes()
                result += 1
            Next
            Return INSTL_INFO_START_COLUMN <= spreadColumn AndAlso spreadColumn < INSTL_INFO_START_COLUMN + result

        End Function
        ''↑↑2014/08/22 Ⅰ.3.設計編集 ベース改修専用化_ch) (TES)張 ADD END

    End Class



End Namespace