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
'Imports EventSakusei.ShisakuBuhinEdit.Kosei.Vo

Imports EventSakusei.ShisakuBuhinEdit.KouseiBuhin.Dao.Vo
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Dao

Namespace ShisakuBuhinEdit.Kosei.Ui
    ''' <summary>
    ''' 部品構成編集画面のSpreadに対する全般処理を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinSpdKoseiObserver : Implements Observer

        ''' <summary>自給品の表示非表示判断用</summary>
        Public Shared SPREAD_JIKYU As String = Nothing

        ''' <summary>構成追加行と追加数</summary>
        Public Shared SPREAD_ROW As Integer = 0
        Public Shared SPREAD_ROWCOUNT As Integer = 0

#Region "各列のTAG"
        Public Const TAG_LEVEL As String = "LEVEL"
        Public Const TAG_SHUKEI_CODE As String = "SHUKEI_CODE"
        Public Const TAG_SIA_SHUKEI_CODE As String = "SIA_SHUKEI_CODE"
        Public Const TAG_GENCYO_CKD_KBN As String = "GENCYO_CKD_KBN"
        Public Const TAG_MAKER_CODE As String = "MAKER_CODE"
        Public Const TAG_MAKER_NAME As String = "MAKER_NAME"
        Public Const TAG_BUHIN_NO As String = "BUHIN_NO"
        ''↓↓2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
        Private Const TAG_BUHIN_NO_KBN As String = "BUHIN_NO_KBN"
        Private Const TAG_BUHIN_NO_KAITEI_NO As String = "BUHIN_NO_KAITEI_NO"
        Private Const TAG_EDA_BAN As String = "EDA_BAN"
        ''↑↑2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD END
        Public Const TAG_BUHIN_NAME As String = "BUHIN_NAME"
        Public Const TAG_INSU_SURYO As String = "INSU_SURYO"
        Public Const TAG_KYOUKU_SECTION As String = "KYOUKU_SECTION"
        ''↓↓2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
        Private Const TAG_TSUKURIKATA_SEISAKU As String = "TAG_TSUKURIKATA_SEISAKU"
        Private Const TAG_TSUKURIKATA_KATASHIYOU1 As String = "TAG_TSUKURIKATA_KATASHIYOU1"
        Private Const TAG_TSUKURIKATA_KATASHIYOU2 As String = "TAG_TSUKURIKATA_KATASHIYOU2"
        Private Const TAG_TSUKURIKATA_KATASHIYOU3 As String = "TAG_TSUKURIKATA_KATASHIYOU3"
        Private Const TAG_TSUKURIKATA_TIGU As String = "TAG_TSUKURIKATA_TIGU"
        Private Const TAG_TSUKURIKATA_NOUNYU As String = "TAG_TSUKURIKATA_NOUNYU"
        Private Const TAG_TSUKURIKATA_KIBO As String = "TAG_TSUKURIKATA_KIBO"
        Private Const TAG_SHUTUZU_YOTEI_DATE As String = "SHUTUZU_YOTEI_DATE"
        Private Const TAG_ZAISHITU_KIKAKU1 As String = "ZAISHITU_KIKAKU1"
        Private Const TAG_ZAISHITU_KIKAKU2 As String = "ZAISHITU_KIKAKU2"
        Private Const TAG_ZAISHITU_KIKAKU3 As String = "ZAISHITU_KIKAKU3"
        Private Const TAG_ZAISHITU_MEKKI As String = "ZAISHITU_MEKKI"
        Private Const TAG_SHISAKU_BANKO_SURYO As String = "SHISAKU_BANKO_SURYO"
        Private Const TAG_SHISAKU_BANKO_SURYO_U As String = "SHISAKU_BANKO_SURYO_U"
        Private Const TAG_SHISAKU_BUHIN_HI As String = "SHISAKU_BUHIN_HI"
        Private Const TAG_SHISAKU_KATA_HI As String = "SHISAKU_KATA_HI"
        ''↑↑2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD END

        '↓↓↓2014/12/25 メタル項目を追加 TES)張 ADD BEGIN
        Public Const TAG_MATERIAL_INFO_LENGTH As String = "TAG_MATERIAL_INFO_LENGTH"
        Public Const TAG_MATERIAL_INFO_WIDTH As String = "TAG_MATERIAL_INFO_WIDTH"
        Public Const TAG_DATA_ITEM_KAITEI_NO As String = "TAG_DATA_ITEM_KAITEI_NO"
        Public Const TAG_DATA_ITEM_AREA_NAME As String = "TAG_DATA_ITEM_AREA_NAME"
        Public Const TAG_DATA_ITEM_SET_NAME As String = "TAG_DATA_ITEM_SET_NAME"
        Public Const TAG_DATA_ITEM_KAITEI_INFO As String = "TAG_DATA_ITEM_KAITEI_INFO"
        '↑↑↑2014/12/25 メタル項目を追加 TES)張 ADD END

        Public Const TAG_BUHIN_NOTE As String = "BUHIN_NOTE"
        Public Const TAG_MEMO_1 As String = "MEMO_1"
        Public Const TAG_MEMO_2 As String = "MEMO_2"
        Public Const TAG_MEMO_3 As String = "MEMO_3"
        Public Const TAG_MEMO_4 As String = "MEMO_4"
        Public Const TAG_MEMO_5 As String = "MEMO_5"
        Public Const TAG_MEMO_6 As String = "MEMO_6"
        Public Const TAG_MEMO_7 As String = "MEMO_7"
        Public Const TAG_MEMO_8 As String = "MEMO_8"
        Public Const TAG_MEMO_9 As String = "MEMO_9"
        Public Const TAG_MEMO_10 As String = "MEMO_10"
        '固定メモ欄
        Public Const TAG_MEMO_SHEET_1 As String = "MEMO_SHEET_1"
        Public Const TAG_MEMO_SHEET_2 As String = "MEMO_SHEET_2"
        Public Const TAG_MEMO_SHEET_3 As String = "MEMO_SHEET_3"
        Public Const TAG_MEMO_SHEET_4 As String = "MEMO_SHEET_4"
        Public Const TAG_MEMO_SHEET_5 As String = "MEMO_SHEET_5"
        Public Const TAG_MEMO_SHEET_6 As String = "MEMO_SHEET_6"
        Public Const TAG_MEMO_SHEET_7 As String = "MEMO_SHEET_7"
        Public Const TAG_MEMO_SHEET_8 As String = "MEMO_SHEET_8"
        Public Const TAG_MEMO_DOOR_TRIM_1 As String = "MEMO_DOOR_TRIM_1"
        Public Const TAG_MEMO_DOOR_TRIM_2 As String = "MEMO_DOOR_TRIM_2"
        Public Const TAG_MEMO_ROOF_TRIM_1 As String = "MEMO_ROOF_TRIM_1"
        Public Const TAG_MEMO_ROOF_TRIM_2 As String = "MEMO_ROOF_TRIM_2"
        Public Const TAG_MEMO_SUNROOF_TRIM_1 As String = "MEMO_SUNROOF_TRIM_1"

        Public Const TAG_BIKOU As String = "BIKOU"
        Public Const TAG_INPUT_FLG As String = "INPUT_FLG"
#End Region
        '''<summary>シート名称</summary>>
        Private Const _SHEET_NAME As String = "KOSEI"
        ''''<summary>シート取込バックアップ名称</summary>>
        'Private Const _spdParts_SHEET_NAME As String = "spdParts"


        Private Shared ReadOnly DEFAULT_LOCK_TAGS As String() = {TAG_INPUT_FLG}

        Private Const TITLE_ROW_INDEX As Integer = -1
        Private Const INSTL_INFO_START_COLUMN As Integer = 11

        Private Const ALL_MIDASHI_ROW_INDEX As Integer = 0
        Private Const INSTL_HINBAN_MIDASHI_ROW_INDEX As Integer = 0
        Private Const INSTL_HINBAN_NO_ROW_INDEX As Integer = 1
        Private Const INSTL_HINBAN_ROW_INDEX As Integer = 2
        Private Const INSTL_HINBAN_KBN_ROW_INDEX As Integer = 3

        'メモ欄
        Private Const MEMO_ROW_INDEX As Integer = 1
        Private Const MEMO_ROW_END_INDEX As Integer = 3
        Private Const MEMO_START_COLUMN As Integer = 11
        Private Const MEMO_END_COLUMN As Integer = 20

        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        'Private sheet As SheetView
        Private ReadOnly subject As HoyouBuhinBuhinEditKoseiSubject
        Private ReadOnly titleRows As Integer

        Private validatorRegister As SpreadValidator
        Private validatorRegisterWarning As SpreadValidator
        Private validatorRegiserShukei As SpreadValidator
        Private validatorRegiserSiaShukei As SpreadValidator
        Private validatorSave As SpreadValidator
        '構成用Validator'
        Private validatorKosei As SpreadValidator

        Private cellTypeFactory As New HoyouBuhinSpdKoseiCellTypeFactory
        Private borderFactory As New SpreadBorderFactory
        Private anInstlColumns As InstlColumns

        Private IsSuspend_SpreadChangeEvent As Boolean
        '直前の員数を保持する配列'
        Private InsuArray()() As Integer

        'フィルタ条件のリスト'
        Private condVoList As List(Of clsOptionFilterVo)

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="spread">部品表Spread</param>
        ''' <param name="subject">部品構成Subject</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal spread As FpSpread, ByVal subject As HoyouBuhinBuhinEditKoseiSubject)
            Me.spread = spread
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)
            'Me.sheet.SheetName = "KOSEI"
            Me.sheet.SheetName = _SHEET_NAME
            Me.subject = subject
            condVoList = New List(Of clsOptionFilterVo)

            Me.titleRows = BuhinEditSpreadUtil.GetTitleRows(sheet)

            InsuArray = New Integer(subject.GetInputRowIndexes.Count + 100)() {}

            subject.AddObserver(Me)
        End Sub

#Region "各CellType"
        Private TateTitleCellType As TextCellType
        Private InstlHinbanMidashiCellType As TextCellType
        Private InstlHinbanNoCellType As TextCellType
        Private InstlHinbanCellType As TextCellType
        Private InstlKbnCellType As TextCellType
        Private MemoMidashiCellType As TextCellType
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
        ''' メモ列見出しセルを返す
        ''' </summary>
        ''' <returns>メモ列見出しセル</returns>
        ''' <remarks></remarks>
        Public Function GetMemoMidashiCellType() As TextCellType
            If MemoMidashiCellType Is Nothing Then
                MemoMidashiCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                MemoMidashiCellType.MaxLength = 61
                MemoMidashiCellType.TextOrientation = FarPoint.Win.TextOrientation.TextTopDown
            End If
            Return MemoMidashiCellType
        End Function
        ''' <summary>
        ''' 固定メモ列見出しセルを返す
        ''' </summary>
        ''' <returns>固定メモ列見出しセル</returns>
        ''' <remarks></remarks>
        Public Function GetKoteiMemoMidashiCellType() As TextCellType
            If MemoMidashiCellType Is Nothing Then
                MemoMidashiCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                MemoMidashiCellType.TextOrientation = FarPoint.Win.TextOrientation.TextHorizontal
            End If
            Return MemoMidashiCellType
        End Function
#End Region

#Region "表示／非表示"

        Public Function MemoSheetOnOff(ByVal eventCode As String, ByVal bukaCode As String, _
                                       ByVal tanto As String)

            Dim MakeMemoDao As HoyouBuhinMakeStructureResultDao = New HoyouBuhinMakeStructureResultDaoImpl
            Dim BuhinEditVo As THoyouBuhinEditVo = MakeMemoDao.FindbyHoyouBuhin(eventCode, bukaCode, tanto)

            Dim SheetOnOff As String = "0"
            Dim DoorOnOff As String = "0"
            Dim RoofOnOff As String = "0"
            Dim SunRoofOnOff As String = "0"

            If StringUtil.IsNotEmpty(BuhinEditVo) Then

                If StringUtil.IsEmpty(BuhinEditVo.MemoSheet1) And StringUtil.IsEmpty(BuhinEditVo.MemoSheet2) And _
                    StringUtil.IsEmpty(BuhinEditVo.MemoSheet3) And StringUtil.IsEmpty(BuhinEditVo.MemoSheet4) And _
                    StringUtil.IsEmpty(BuhinEditVo.MemoSheet5) And StringUtil.IsEmpty(BuhinEditVo.MemoSheet6) And _
                    StringUtil.IsEmpty(BuhinEditVo.MemoSheet7) And StringUtil.IsEmpty(BuhinEditVo.MemoSheet8) Then
                    SheetOnOff = "0"
                Else
                    SheetOnOff = "1"
                End If
                If StringUtil.IsEmpty(BuhinEditVo.MemoDoorTrim1) And StringUtil.IsEmpty(BuhinEditVo.MemoDoorTrim2) Then
                    DoorOnOff = "0"
                Else
                    DoorOnOff = "1"
                End If
                If StringUtil.IsEmpty(BuhinEditVo.MemoRoofTrim1) And StringUtil.IsEmpty(BuhinEditVo.MemoRoofTrim2) Then
                    RoofOnOff = "0"
                Else
                    RoofOnOff = "1"
                End If
                If StringUtil.IsEmpty(BuhinEditVo.MemoSunroofTrim1) And StringUtil.IsEmpty(BuhinEditVo.MemoSunroofTrim1) Then
                    SunRoofOnOff = "0"
                Else
                    SunRoofOnOff = "1"
                End If

            End If

            '0はデータ無し、1はデータ有り
            Return SheetOnOff & DoorOnOff & RoofOnOff & SunRoofOnOff

        End Function


        ''' <summary>
        ''' SHEETを非表示にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SheetColumnDisable()
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_1).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_2).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_3).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_4).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_5).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_6).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_7).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_8).Index, False)
        End Sub

        ''' <summary>
        ''' SHEETを表示する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SheetColumnVisible()
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_1).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_2).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_3).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_4).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_5).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_6).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_7).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SHEET_8).Index, True)
        End Sub

        ''' <summary>
        ''' DOORを非表示にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DoorColumnDisable()
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_DOOR_TRIM_1).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_DOOR_TRIM_2).Index, False)
        End Sub

        ''' <summary>
        ''' DOORを表示する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DoorColumnVisible()
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_DOOR_TRIM_1).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_DOOR_TRIM_2).Index, True)
        End Sub

        ''' <summary>
        ''' ROOFを非表示にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RoofColumnDisable()
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_ROOF_TRIM_1).Index, False)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_ROOF_TRIM_2).Index, False)
        End Sub

        ''' <summary>
        ''' ROOFを表示する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RoofColumnVisible()
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_ROOF_TRIM_1).Index, True)
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_ROOF_TRIM_2).Index, True)
        End Sub

        ''' <summary>
        ''' SUNROOFを非表示にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SunRoofColumnDisable()
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SUNROOF_TRIM_1).Index, False)
        End Sub

        ''' <summary>
        ''' SUNROOFを表示する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SunRoofColumnVisible()
            spread.ActiveSheet.SetColumnVisible(sheet.Columns(TAG_MEMO_SUNROOF_TRIM_1).Index, True)
        End Sub


#End Region

        ''' <summary>
        ''' 初期化する（一度だけ実行する事を想定）
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Initialize()

            BuhinEditSpreadUtil.InitializeFrm41(spread)

            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_LEVEL
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUKEI_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SIA_SHUKEI_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GENCYO_CKD_KBN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MAKER_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MAKER_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO
            ''↓↓2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO_KBN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EDA_BAN
            ''↑↑2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD END
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_INSU_SURYO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KYOUKU_SECTION
            ''↓↓2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
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
            ''↑↑2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD END

            '↓↓↓2014/12/25 メタル項目を追加 TES)張 ADD BEGIN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MATERIAL_INFO_LENGTH
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MATERIAL_INFO_WIDTH
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_KAITEI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_AREA_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_SET_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_DATA_ITEM_KAITEI_INFO
            '↑↑↑2014/12/25 メタル項目を追加 TES)張 ADD END

            ''↓↓2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_BUHIN_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_KATA_HI
            ''↑↑2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD END
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NOTE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_2
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_3
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_4
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_5
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_6
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_7
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_8
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_9
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_10
            '固定メモ欄
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_SHEET_1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_SHEET_2
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_SHEET_3
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_SHEET_4
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_SHEET_5
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_SHEET_6
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_SHEET_7
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_SHEET_8
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_DOOR_TRIM_1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_DOOR_TRIM_2
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_ROOF_TRIM_1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_ROOF_TRIM_2
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MEMO_SUNROOF_TRIM_1

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BIKOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_INPUT_FLG

            ' *** 行に設定 ***
            With sheet.Rows(ALL_MIDASHI_ROW_INDEX)
                .BackColor = System.Drawing.SystemColors.Control
                .Locked = True
            End With
            With sheet.Rows(MEMO_ROW_INDEX)
                .BackColor = Color.White
                .Locked = False
            End With

            sheet.Rows(MEMO_ROW_INDEX).CellType = GetMemoMidashiCellType()

            ' *** 縦書き表示のタイトル ***
            ''↓↓2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 CHG BEGIN
            'Dim TATE_TITLE_COLUMN_TAGS As String() = New String() {TAG_LEVEL, TAG_SHUKEI_CODE, TAG_SIA_SHUKEI_CODE, TAG_GENCYO_CKD_KBN, TAG_MAKER_CODE, TAG_INSU_SURYO}
            Dim TATE_TITLE_COLUMN_TAGS As String() = New String() {TAG_LEVEL, TAG_SHUKEI_CODE, TAG_SIA_SHUKEI_CODE, TAG_GENCYO_CKD_KBN, TAG_MAKER_CODE, TAG_INSU_SURYO, TAG_BUHIN_NO_KBN, TAG_BUHIN_NO_KAITEI_NO, TAG_EDA_BAN}
            ''↑↑2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 CHG END
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
            ' ** 員数 **
            With sheet.Columns(TAG_INSU_SURYO)
                .Border = borderFactory.GetLeftLineAndRightWLine
            End With
            ' ** 部品番号ノート **
            With sheet.Columns(TAG_BUHIN_NOTE)
                .Border = borderFactory.GetRightWLine
            End With
            ' ** メモ_１０ **
            With sheet.Columns(TAG_MEMO_10)
                .Border = borderFactory.GetRightWLine
            End With
            ' ** 備考 **
            With sheet.Columns(TAG_BIKOU)
                .Border = borderFactory.GetLeftWLine
            End With

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_LEVEL, cellTypeFactory.LevelCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHUKEI_CODE, cellTypeFactory.ShukeiCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SIA_SHUKEI_CODE, cellTypeFactory.SiaShukeiCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_GENCYO_CKD_KBN, cellTypeFactory.GencyoCkdKbnCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MAKER_CODE, cellTypeFactory.MakerCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MAKER_NAME, cellTypeFactory.MakerNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO, cellTypeFactory.BuhinNoCellType)
            ''↓↓2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO_KBN, cellTypeFactory.BuhinNoKbnCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO_KAITEI_NO, cellTypeFactory.BuhinNoKaiteiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_EDA_BAN, cellTypeFactory.EdaBanCellType)
            ''↑↑2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD END
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NAME, cellTypeFactory.BuhinNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_INSU_SURYO, cellTypeFactory.InsuSuryoCellType)
            '
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KYOUKU_SECTION, cellTypeFactory.KyoukuSectionCellType)
            ''↓↓2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
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
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHISAKU_BUHIN_HI, cellTypeFactory.ShisakuBuhinHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHISAKU_KATA_HI, cellTypeFactory.ShisakuKataHiCellType)
            ''↑↑2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD END

            '↓↓↓2014/12/25 メタル項目を追加 TES)張 ADD BEGIN
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MATERIAL_INFO_LENGTH, cellTypeFactory.MaterialInfoLengthCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MATERIAL_INFO_WIDTH, cellTypeFactory.MaterialInfoWidthCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_KAITEI_NO, cellTypeFactory.DataItemKaiteiNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_AREA_NAME, cellTypeFactory.DataItemAreaNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_SET_NAME, cellTypeFactory.DataItemSetNameCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_DATA_ITEM_KAITEI_INFO, cellTypeFactory.DataItemKaiteiInfoCellType)
            '↑↑↑2014/12/25 メタル項目を追加 TES)張 ADD END

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BIKOU, cellTypeFactory.BikouCellType)
            '
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NOTE, cellTypeFactory.BuhinNoteCellType)
            '
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_1, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_2, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_3, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_4, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_5, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_6, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_7, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_8, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_9, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_10, cellTypeFactory.MemoCellType)

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_SHEET_1, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_SHEET_2, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_SHEET_3, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_SHEET_4, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_SHEET_5, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_SHEET_6, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_SHEET_7, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_SHEET_8, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_DOOR_TRIM_1, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_DOOR_TRIM_2, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_ROOF_TRIM_1, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_ROOF_TRIM_2, cellTypeFactory.MemoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MEMO_SUNROOF_TRIM_1, cellTypeFactory.MemoCellType)

            ''メモ欄の入力列
            'Dim TATE_MEMO_COLUMN_TAGS As String() = New String() {TAG_MEMO_1, TAG_MEMO_2, TAG_MEMO_3, TAG_MEMO_4, TAG_MEMO_5, _
            '                                                       TAG_MEMO_6, TAG_MEMO_7, TAG_MEMO_8, TAG_MEMO_9, TAG_MEMO_10}
            'For Each tag As String In TATE_MEMO_COLUMN_TAGS
            '    With sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(tag).Index, MEMO_ROW_INDEX, sheet.Columns(tag).Index)
            '        .CellType = cellTypeFactory.MemoTCellType
            '    End With
            'Next

            'IMEを使用可能にする。
            spread.ImeMode = Windows.Forms.ImeMode.NoControl

            InitializeValidator()

            'AddHandler spread.VisibleChanged, AddressOf Spread_VisibleChangedEventHandlable
            '' 通常の Spread_Changed()では、CTRL+V/CTRL+ZでChengedイベントが発生しない
            ''（編集モードではない状態で変更された場合は発生しない仕様とのこと。）
            '' CTRL+V/CTRL+Zでもイベントが発生するハンドラを設定する
            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)

        End Sub

        Private Sub InitializeValidator()
            ' 保存時は、DB登録に欠かせないチェックだけ
            validatorSave = New SpreadValidator(spread)


            '取引先名称２０文字以上がいる！とりあえずDBと同じバイトに変更してみる'
            validatorSave.Add(TAG_MAKER_NAME, "取引先名称").MaxLengthByte(102)
            '↓↓2014/10/02 酒井 ADD BEGIN
            'validatorSave.Add(TAG_BIKOU, "備考").MaxLengthByte(256)
            validatorSave.Add(TAG_BIKOU, "備考").MaxLengthByte(60)
            '↑↑2014/10/02 酒井 ADD END
            validatorSave.Add(TAG_BUHIN_NOTE, "部品ノート").MaxLengthByte(102)

            ' 登録の入力チェック
            validatorRegister = New SpreadValidator(spread)

            validatorRegister.Add(TAG_LEVEL, "レベル").Required().Numeric()
            validatorRegister.Add(TAG_LEVEL, "レベル").Required()
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


            validatorRegister.Add(TAG_INSU_SURYO, "員数").Required()

            '2012/01/23 供給セクション追加  ==>ここに何か実装する必要がある？
            'validatorRegister.Add(TAG_KYOUKU_SECTION, "供給セクション").InArray(TShisakuBuhinEditVoHelper.？？？.YES)
            '↓↓2014/10/02 酒井 ADD BEGIN
            'validatorRegister.Add(TAG_BIKOU, "備考").MaxLengthByte(256)
            validatorRegister.Add(TAG_BIKOU, "備考").MaxLengthByte(60)
            '↑↑2014/10/02 酒井 ADD END
            validatorRegister.Add(TAG_BUHIN_NOTE, "部品ノート").MaxLengthByte(102)

            validatorRegister.Add(TAG_MEMO_1, "メモ０１").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_2, "メモ０２").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_3, "メモ０３").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_4, "メモ０４").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_5, "メモ０５").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_6, "メモ０６").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_7, "メモ０７").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_8, "メモ０８").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_9, "メモ０９").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_10, "メモ１０").MaxLengthByte(2)

            validatorRegister.Add(TAG_MEMO_SHEET_1, "インナーベルト").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_SHEET_2, "革表皮ｏｒ布表皮").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_SHEET_3, "乗員検知センサー").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_SHEET_4, "サイドエアバッグ").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_SHEET_5, "シートヒーター").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_SHEET_6, "シートメモリー（ＥＣＵ）").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_SHEET_7, "重心付加パイプ").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_SHEET_8, "Ｈ．Ｐ測定").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_DOOR_TRIM_1, "ウェザーストリップのみＡＳＳＹ").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_DOOR_TRIM_2, "インリモ有").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_ROOF_TRIM_1, "ルーフPAD ASSY不要､PAD単品別納").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_ROOF_TRIM_2, "ルーフコード､フィーダーコードASSY").MaxLengthByte(2)
            validatorRegister.Add(TAG_MEMO_SUNROOF_TRIM_1, "ガラス無（ガラス組み付け用ビス付）").MaxLengthByte(2)
            ''↓↓2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
            validatorRegister.Add(TAG_TSUKURIKATA_KIBO, "部品製作規模・概要").MaxLengthByte(200)
            ''↑↑2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD END

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

        Public ObserverUpdating As Boolean = False

        ''' <summary>
        ''' Spread表示を(再)表示する
        ''' </summary>
        ''' <param name="observable">表示指示元のObservable</param>
        ''' <param name="arg">引数</param>
        ''' <remarks></remarks>
        Public Sub Update(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update
            '2014/10/06 追加 E.Ubukata
            ObserverUpdating = True

            If arg Is Nothing Then
                'ClearInstlColumns()
                ClearSheet(True)
                LockSheet(subject.IsViewerMode)
                Update(observable, TITLE_ROW_INDEX)

                AdjustSheetRowCount()

                For Each rowIndex As Integer In subject.GetInputRowIndexes()
                    Update(observable, rowIndex)
                Next

            ElseIf TypeOf arg Is HoyouBuhinBuhinEditKoseiSubject.NotifyInfo Then

                Dim info As HoyouBuhinBuhinEditKoseiSubject.NotifyInfo = DirectCast(arg, HoyouBuhinBuhinEditKoseiSubject.NotifyInfo)
                If info.IsTitle Then
                ElseIf info.RowIndexes IsNot Nothing Then
                    For Each rowIndex As Integer In info.RowIndexes
                        Update(observable, rowIndex)
                    Next
                End If

            ElseIf IsNumeric(arg) Then
                Dim rowIndex As Integer = CInt(arg)
                Dim row As Integer = ConvSubjectIndexToSpreadRow(rowIndex)

                If rowIndex <= TITLE_ROW_INDEX Then
                    'メモのタイトル列
                    sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_1).Index).Value = subject.Memo1T(0)
                    sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_2).Index).Value = subject.Memo2T(0)
                    sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_3).Index).Value = subject.Memo3T(0)
                    sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_4).Index).Value = subject.Memo4T(0)
                    sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_5).Index).Value = subject.Memo5T(0)
                    sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_6).Index).Value = subject.Memo6T(0)
                    sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_7).Index).Value = subject.Memo7T(0)
                    sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_8).Index).Value = subject.Memo8T(0)
                    sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_9).Index).Value = subject.Memo9T(0)
                    sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_10).Index).Value = subject.Memo10T(0)

                    ''↓↓2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
                    sheet.Cells(3, sheet.Columns(TAG_TSUKURIKATA_SEISAKU).Index).Value = "製作方法"
                    sheet.Cells(3, sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU1).Index).Value = "型仕様１"
                    sheet.Cells(3, sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU2).Index).Value = "型仕様２"
                    sheet.Cells(3, sheet.Columns(TAG_TSUKURIKATA_KATASHIYOU3).Index).Value = "型仕様３"
                    sheet.Cells(3, sheet.Columns(TAG_TSUKURIKATA_TIGU).Index).Value = "治具"
                    sheet.Cells(3, sheet.Columns(TAG_TSUKURIKATA_NOUNYU).Index).Value = "納入見通し"
                    sheet.Cells(3, sheet.Columns(TAG_TSUKURIKATA_KIBO).Index).Value = "部品製作規模・概要"
                    sheet.Cells(3, sheet.Columns(TAG_ZAISHITU_KIKAKU1).Index).Value = "規格１"
                    sheet.Cells(3, sheet.Columns(TAG_ZAISHITU_KIKAKU2).Index).Value = "規格２"
                    sheet.Cells(3, sheet.Columns(TAG_ZAISHITU_KIKAKU3).Index).Value = "規格３"
                    sheet.Cells(3, sheet.Columns(TAG_ZAISHITU_MEKKI).Index).Value = "メッキ"
                    sheet.Cells(3, sheet.Columns(TAG_SHISAKU_BANKO_SURYO).Index).Value = "板厚"
                    sheet.Cells(3, sheet.Columns(TAG_SHISAKU_BANKO_SURYO_U).Index).Value = "u"
                    ''↑↑2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD END

                    '↓↓↓2014/12/25 メタル項目を追加 TES)張 ADD BEGIN
                    sheet.Cells(2, sheet.Columns(TAG_MATERIAL_INFO_LENGTH).Index).Value = "製品サイズ項目"
                    sheet.Cells(2, sheet.Columns(TAG_DATA_ITEM_KAITEI_NO).Index).Value = "データ項目"
                    sheet.Cells(3, sheet.Columns(TAG_MATERIAL_INFO_LENGTH).Index).Value = "製品長(mm)"
                    sheet.Cells(3, sheet.Columns(TAG_MATERIAL_INFO_WIDTH).Index).Value = "製品幅(mm)"
                    sheet.Cells(3, sheet.Columns(TAG_DATA_ITEM_KAITEI_NO).Index).Value = "改訂№"
                    sheet.Cells(3, sheet.Columns(TAG_DATA_ITEM_AREA_NAME).Index).Value = "エリア名"
                    sheet.Cells(3, sheet.Columns(TAG_DATA_ITEM_SET_NAME).Index).Value = "セット名"
                    sheet.Cells(3, sheet.Columns(TAG_DATA_ITEM_KAITEI_INFO).Index).Value = "改訂情報"
                    '↑↑↑2014/12/25 メタル項目を追加 TES)張 ADD END
                Else
                    '--------------------------------------------------------------------------------------------
                    'レベル’
                    'sheet.Cells(row, sheet.Columns(TAG_LEVEL).Index).Value = subject.Level(rowIndex)
                    '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                    '               →補用部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                    If StringUtil.IsEmpty(subject.Level(rowIndex)) Then
                        sheet.Cells(row, sheet.Columns(TAG_LEVEL).Index).Value = ""
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_LEVEL).Index).Value = subject.Level(rowIndex)
                    End If
                    '--------------------------------------------------------------------------------------------
                    sheet.Cells(row, sheet.Columns(TAG_SHUKEI_CODE).Index).Value = subject.ShukeiCode(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value = subject.SiaShukeiCode(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).Value = subject.GencyoCkdKbn(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MAKER_CODE).Index).Value = subject.MakerCode(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MAKER_NAME).Index).Value = subject.MakerName(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO).Index).Value = subject.BuhinNo(rowIndex)
                    ''↓↓2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO_KBN).Index).Value = subject.BuhinNoKbn(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO_KAITEI_NO).Index).Value = subject.BuhinNoKaiteiNo(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_EDA_BAN).Index).Value = subject.EdaBan(rowIndex)
                    ''↑↑2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD END
                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NAME).Index).Value = Trim(subject.BuhinName(rowIndex))
                    If StringUtil.Equals(subject.InsuSuryo(rowIndex), "0") Then
                        sheet.Cells(row, sheet.Columns(TAG_INSU_SURYO).Index).Value = ""
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_INSU_SURYO).Index).Value = subject.InsuSuryo(rowIndex)
                    End If
                    '
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_1).Index).Value = subject.Memo1(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_2).Index).Value = subject.Memo2(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_3).Index).Value = subject.Memo3(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_4).Index).Value = subject.Memo4(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_5).Index).Value = subject.Memo5(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_6).Index).Value = subject.Memo6(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_7).Index).Value = subject.Memo7(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_8).Index).Value = subject.Memo8(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_9).Index).Value = subject.Memo9(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_10).Index).Value = subject.Memo10(rowIndex)
                    '
                    sheet.Cells(row, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value = subject.KyoukuSection(rowIndex)
                    ''↓↓2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
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
                    sheet.Cells(row, sheet.Columns(TAG_SHISAKU_BUHIN_HI).Index).Value = subject.ShisakuBuhinHi(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_SHISAKU_KATA_HI).Index).Value = subject.ShisakuKataHi(rowIndex)
                    ''↑↑2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD END

                    '↓↓↓2014/12/25 メタル項目を追加 TES)張 ADD BEGIN
                    sheet.Cells(row, sheet.Columns(TAG_MATERIAL_INFO_LENGTH).Index).Value = subject.MaterialInfoLength(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MATERIAL_INFO_WIDTH).Index).Value = subject.MaterialInfoWidth(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_DATA_ITEM_KAITEI_NO).Index).Value = subject.DataItemKaiteiNo(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_DATA_ITEM_AREA_NAME).Index).Value = subject.DataItemAreaName(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_DATA_ITEM_SET_NAME).Index).Value = subject.DataItemSetName(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_DATA_ITEM_KAITEI_INFO).Index).Value = subject.DataItemKaiteiInfo(rowIndex)
                    '↑↑↑2014/12/25 メタル項目を追加 TES)張 ADD END

                    sheet.Cells(row, sheet.Columns(TAG_BIKOU).Index).Value = subject.Bikou(rowIndex)
                    '

                    Dim BuhinNote As String = subject.BuhinNote(rowIndex)

                    If BuhinNote Is Nothing Then
                        BuhinNote = ""
                    End If

                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NOTE).Index).Value = BuhinNote.ToUpper
                    '
                    sheet.Cells(row, sheet.Columns(TAG_INPUT_FLG).Index).Value = subject.InputFlg(rowIndex)
                    '
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_SHEET_1).Index).Value = subject.MemoSheet1(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_SHEET_2).Index).Value = subject.MemoSheet2(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_SHEET_3).Index).Value = subject.MemoSheet3(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_SHEET_4).Index).Value = subject.MemoSheet4(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_SHEET_5).Index).Value = subject.MemoSheet5(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_SHEET_6).Index).Value = subject.MemoSheet6(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_SHEET_7).Index).Value = subject.MemoSheet7(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_SHEET_8).Index).Value = subject.MemoSheet8(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_DOOR_TRIM_1).Index).Value = subject.MemoDoorTrim1(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_DOOR_TRIM_2).Index).Value = subject.MemoDoorTrim2(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_ROOF_TRIM_1).Index).Value = subject.MemoRoofTrim1(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_ROOF_TRIM_2).Index).Value = subject.MemoRoofTrim2(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MEMO_SUNROOF_TRIM_1).Index).Value = subject.MemoSunroofTrim1(rowIndex)
                    '
                    '↓↓2014/10/22 酒井 ADD BEGIN
                    If subject.Level(rowIndex) = 0 Then
                        sheet.Cells(row, sheet.Columns(TAG_LEVEL).Index).Locked = True
                        sheet.Cells(row, sheet.Columns(TAG_INSU_SURYO).Index).Locked = True
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_LEVEL).Index).Locked = False
                        sheet.Cells(row, sheet.Columns(TAG_INSU_SURYO).Index).Locked = False
                    End If
                    '↑↑2014/10/22 酒井 ADD END
                End If
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

        ''' <summary>
        ''' Spread行indexを、(Subject)indexにして返す
        ''' </summary>
        ''' <param name="spreadRow">Spread行index</param>
        ''' <returns>(Subject)index</returns>
        ''' <remarks></remarks>
        Private Function ConvSpreadRowToSubjectIndex(ByVal spreadRow As Integer) As Integer
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
        ''' Spread行indexが、データ入力行かを返す
        ''' </summary>
        ''' <param name="spreadRow">Spread行index</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Function IsDataRow(ByVal spreadRow As Integer) As Boolean
            Return titleRows <= spreadRow
        End Function

        ''' <summary>
        ''' メモ列入力行かを返す
        ''' </summary>
        ''' <param name="spreadRow">Spread行index</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Function IsMemoTRow(ByVal spreadRow As Integer) As Boolean
            Return MEMO_ROW_INDEX <= spreadRow And MEMO_ROW_END_INDEX >= spreadRow
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
        ''' Sheetのデータをクリアする(INSTL品番の列名部と全体の明細部)
        ''' </summary>
        ''' <param name="dataOnly">データだけをクリアする場合、true</param>
        ''' <remarks></remarks>
        Public Sub ClearSheet(Optional ByVal dataOnly As Boolean = True)
            SpreadUtil.RemoveHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
            Try
                ' メモ欄の列名部分
                sheet.ClearRange(MEMO_ROW_INDEX, MEMO_START_COLUMN, MEMO_ROW_END_INDEX, MEMO_END_COLUMN, dataOnly)
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
            ' メモ欄の列名部分
            With sheet.Cells(MEMO_ROW_INDEX, MEMO_START_COLUMN, MEMO_ROW_END_INDEX, MEMO_END_COLUMN)
                .ResetBackColor()

                '.ResetForeColor()
                '.ResetFont()
            End With
            ' 全体の明細部
            With sheet.Cells(titleRows, 0, sheet.RowCount - 1, sheet.ColumnCount - 1)
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
            ' メモ欄の列名部分
            With sheet.Cells(MEMO_ROW_INDEX, MEMO_START_COLUMN, MEMO_ROW_END_INDEX, MEMO_END_COLUMN)
                .ResetBackColor()
                .ResetForeColor()
                .ResetFont()
            End With
            ' 全体の明細部
            With sheet.Cells(titleRows, 0, sheet.RowCount - 1, sheet.ColumnCount - 1)
                .ResetBackColor()
                .ResetForeColor()
                .ResetFont()
            End With
        End Sub

#Region "スプレッド上で入力されたパラメータを判定・変換"
        ''' <summary>
        ''' 入力されたパラメータをチェック
        ''' </summary>
        ''' Spread上で入力された値を変更する
        ''' <param name="ChangedCell">チェックしたいセル</param>
        ''' <remarks></remarks>
        ''' Created by S.Tokizaki 2014.04.22
        Public Sub Change_EditInsuu(ByVal ChangedCell As Cell)

            '   予め、列位置を取得

            Dim ColumnTag As String = ChangedCell.Column.Tag.ToString

            '
            '   列位置のTAGによって、処理を分別
            '       他にも、入力チェックを行いたければ、ここに追加するのが良いかと思いますよ
            '
            Select Case ColumnTag
                Case TAG_INSU_SURYO
                    ChangedCell.Value = ChangeInsu(ChangedCell.Value)

                Case TAG_BUHIN_NAME

                    Dim s As String = ChangedCell.Value
                    Dim t As FarPoint.Win.Spread.CellType.TextCellType

                    If s Is Nothing Then
                        s = ""
                    End If

                    ' 対象となるセルのインスタンスを生成し、MaxLengthプロパティの値を参照します

                    t = CType(sheet.GetCellType(ChangedCell.Row.Index, ChangedCell.Column.Index), FarPoint.Win.Spread.CellType.TextCellType)

                    Do


                        Dim StringByteLength As Integer = System.Text.Encoding.GetEncoding(932).GetByteCount(s)
                        If StringByteLength > t.MaxLength Then
                            s = s.Substring(0, s.Length - 1)
                        Else
                            If ChangedCell.Value <> s Then
                                ChangedCell.Value = s
                            End If
                            Exit Do
                        End If

                    Loop



            End Select


        End Sub
#End Region

#Region "員数フィルタ処理"
        ''' <summary>
        ''' 員数を下記のように変更する
        ''' 　　０              ：NULL
        ''' 　　１～９９９９    ：少数点以下を削除した値
        ''' 　　負の値 or **    ：「**」
        ''' 　　上記以外　　　　：NULL
        ''' </summary>
        ''' <param name="Value">元になる員数</param>
        ''' <returns>変更後の員数</returns>
        ''' <remarks></remarks>
        ''' Created by S.Tokizaki 2014.04.22
        Function ChangeInsu(ByVal Value As String) As String

            Dim Result As String = Value

            If IsNumeric(Result) Then

                '   少々強引な変換ではあるが、上記IsNumericにて、数値判断はしているのでこのような処理
                Dim tmpINT As Integer = Int(Result)

                Select Case tmpINT
                    Case 0                  '   ０ならば、値はNULL
                        Result = ""

                    Case 1 To 10000         '   １～１００００ならば
                        Result = tmpINT      '   小数点以下を排除した数値に入れなおす

                    Case Else               '   上記以外は負数なので、「**」を入れる
                        Result = "**"
                End Select


            Else
                '   「1/4」とかの分数や「1-1」などという値もここに来る 
                If Result <> "**" Then
                    Result = ""
                End If
            End If

            Return Result
        End Function
#End Region



        ''' <summary>
        ''' 入力データ変更時の処理
        ''' </summary>
        ''' <param name="row">Spread行index</param>
        ''' <param name="column">spread列index</param>
        ''' <remarks></remarks>
        Private Sub OnChange(ByVal row As Integer, ByVal column As Integer)
            If IsMemoTRow(row) Then
                Select Case Convert.ToString(sheet.Columns(column).Tag)
                    Case TAG_MEMO_1
                        subject.Memo1T(0) = sheet.Cells(MEMO_ROW_INDEX, column).Value
                    Case TAG_MEMO_2
                        subject.Memo2T(0) = sheet.Cells(MEMO_ROW_INDEX, column).Value
                    Case TAG_MEMO_3
                        subject.Memo3T(0) = sheet.Cells(MEMO_ROW_INDEX, column).Value
                    Case TAG_MEMO_4
                        subject.Memo4T(0) = sheet.Cells(MEMO_ROW_INDEX, column).Value
                    Case TAG_MEMO_5
                        subject.Memo5T(0) = sheet.Cells(MEMO_ROW_INDEX, column).Value
                    Case TAG_MEMO_6
                        subject.Memo6T(0) = sheet.Cells(MEMO_ROW_INDEX, column).Value
                    Case TAG_MEMO_7
                        subject.Memo7T(0) = sheet.Cells(MEMO_ROW_INDEX, column).Value
                    Case TAG_MEMO_8
                        subject.Memo8T(0) = sheet.Cells(MEMO_ROW_INDEX, column).Value
                    Case TAG_MEMO_9
                        subject.Memo9T(0) = sheet.Cells(MEMO_ROW_INDEX, column).Value
                    Case TAG_MEMO_10
                        subject.Memo10T(0) = sheet.Cells(MEMO_ROW_INDEX, column).Value
                End Select
                'アンダーラインが消えるので引き直す。
                With sheet.Rows(MEMO_ROW_INDEX)
                    .Border = borderFactory.GetUnderLine
                End With
                'メモの横線が消えるので引き直す。
                With sheet.Columns(TAG_BUHIN_NOTE)
                    .Border = borderFactory.GetRightWLine
                End With

                Return
            End If

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
                    subject.Level(rowIndex) = StringUtil.ToInteger(value)
                Case TAG_SHUKEI_CODE
                    subject.ShukeiCode(rowIndex) = value
                Case TAG_SIA_SHUKEI_CODE
                    subject.SiaShukeiCode(rowIndex) = value
                Case TAG_GENCYO_CKD_KBN
                    subject.GencyoCkdKbn(rowIndex) = value
                Case TAG_MAKER_CODE
                    subject.MakerCode(rowIndex) = value
                Case TAG_MAKER_NAME
                    subject.MakerName(rowIndex) = value
                Case TAG_BUHIN_NO

                    '   部品Noが変化したら、
                    '	インプットフラグは削除
                    If subject.BuhinNo(rowIndex) <> value Then
                        subject.InputFlg(rowIndex) = ""
                    End If


                    subject.BuhinNo(rowIndex) = value
                Case TAG_BUHIN_NAME
                    subject.BuhinName(rowIndex) = value
                Case TAG_INSU_SURYO

                    'subject.InsuSuryo(rowIndex) = value

                    '
                    '   Changed by S.Tokizaki 2014.04.22
                    '
                    subject.InsuSuryo(rowIndex) = ChangeInsu(value)

                    'If StringUtil.Equals(value, "-1") Then
                    '    subject.InsuSuryo(rowIndex) = "**"
                    'Else
                    '    subject.InsuSuryo(rowIndex) = value
                    'End If



                    'If StringUtil.Equals(value, "**") Then
                    '    subject.InsuSuryo(rowIndex) = -1
                    'Else
                    '    If StringUtil.IsEmpty(value) Then
                    '        subject.InsuSuryo(rowIndex) = 0
                    '    Else
                    '        subject.InsuSuryo(rowIndex) = CInt(value)
                    '    End If
                    'End If
                Case TAG_MEMO_1
                    subject.Memo1(rowIndex) = value
                Case TAG_MEMO_2
                    subject.Memo2(rowIndex) = value
                Case TAG_MEMO_3
                    subject.Memo3(rowIndex) = value
                Case TAG_MEMO_4
                    subject.Memo4(rowIndex) = value
                Case TAG_MEMO_5
                    subject.Memo5(rowIndex) = value
                Case TAG_MEMO_6
                    subject.Memo6(rowIndex) = value
                Case TAG_MEMO_7
                    subject.Memo7(rowIndex) = value
                Case TAG_MEMO_8
                    subject.Memo8(rowIndex) = value
                Case TAG_MEMO_9
                    subject.Memo9(rowIndex) = value
                Case TAG_MEMO_10
                    subject.Memo10(rowIndex) = value
                Case TAG_KYOUKU_SECTION
                    subject.KyoukuSection(rowIndex) = value
                Case TAG_BIKOU
                    subject.Bikou(rowIndex) = value
                Case TAG_BUHIN_NOTE
                    subject.BuhinNote(rowIndex) = value
                Case TAG_INPUT_FLG
                    subject.InputFlg(rowIndex) = value
                    '
                Case TAG_MEMO_SHEET_1
                    subject.MemoSheet1(rowIndex) = value
                Case TAG_MEMO_SHEET_2
                    subject.MemoSheet2(rowIndex) = value
                Case TAG_MEMO_SHEET_3
                    subject.MemoSheet3(rowIndex) = value
                Case TAG_MEMO_SHEET_4
                    subject.MemoSheet4(rowIndex) = value
                Case TAG_MEMO_SHEET_5
                    subject.MemoSheet5(rowIndex) = value
                Case TAG_MEMO_SHEET_6
                    subject.MemoSheet6(rowIndex) = value
                Case TAG_MEMO_SHEET_7
                    subject.MemoSheet7(rowIndex) = value
                Case TAG_MEMO_SHEET_8
                    subject.MemoSheet8(rowIndex) = value
                Case TAG_MEMO_DOOR_TRIM_1
                    subject.MemoDoorTrim1(rowIndex) = value
                Case TAG_MEMO_DOOR_TRIM_2
                    subject.MemoDoorTrim2(rowIndex) = value
                Case TAG_MEMO_ROOF_TRIM_1
                    subject.MemoRoofTrim1(rowIndex) = value
                Case TAG_MEMO_ROOF_TRIM_2
                    subject.MemoRoofTrim2(rowIndex) = value
                Case TAG_MEMO_SUNROOF_TRIM_1
                    subject.MemoSunroofTrim1(rowIndex) = value
                    ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化 (TES)張 ADD BEGIN
                Case TAG_BUHIN_NO_KBN
                    subject.BuhinNoKbn(rowIndex) = value
                Case TAG_BUHIN_NO_KAITEI_NO
                    subject.BuhinNoKaiteiNo(rowIndex) = value
                Case TAG_EDA_BAN
                    subject.EdaBan(rowIndex) = value
                Case TAG_SHUTUZU_YOTEI_DATE
                    subject.ShutuzuYoteiDate(rowIndex) = DateUtil.ConvDateValueToIneteger(value)
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
                Case TAG_ZAISHITU_KIKAKU1
                    subject.ZaishituKikaku1(rowIndex) = value
                Case TAG_ZAISHITU_KIKAKU2
                    subject.ZaishituKikaku2(rowIndex) = value
                Case TAG_ZAISHITU_KIKAKU3
                    subject.ZaishituKikaku3(rowIndex) = value
                Case TAG_ZAISHITU_MEKKI
                    subject.ZaishituMekki(rowIndex) = value
                Case TAG_SHISAKU_BANKO_SURYO
                    subject.ShisakuBankoSuryo(rowIndex) = value
                Case TAG_SHISAKU_BANKO_SURYO_U
                    subject.ShisakuBankoSuryoU(rowIndex) = value
                Case TAG_SHISAKU_BUHIN_HI
                    subject.ShisakuBuhinHi(rowIndex) = StringUtil.ToInteger(value)
                Case TAG_SHISAKU_KATA_HI
                    subject.ShisakuKataHi(rowIndex) = StringUtil.ToInteger(value)
                    ''↑↑2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化 (TES)張 ADD END

                    '↓↓↓2014/12/25 メタル項目を追加 TES)張 ADD BEGIN
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
                    '↑↑↑2014/12/25 メタル項目を追加 TES)張 ADD END
            End Select

            subject.NotifyObservers(rowIndex)

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

        ''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bg) (TES)張 ADD BEGIN
        Public Sub AssertValidateLevel()
            'エラーリスト定義'
            Dim errorControls As New List(Of ErrorControl)
            Dim rowindex As Integer
            Dim i As Integer = 0

            For Each rowindex In subject.GetInputRowIndexes

                Dim strLevel As String = sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_LEVEL).Index).Value
                If strLevel = "0" Then
                    i = i + 1
                    errorControls.AddRange(MakeErrorControlsLevelSection(rowindex + titleRows))
                    Dim strBuhinNo As String = sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).Value
                    If strBuhinNo = "DUMMY" Then
                        ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化_bg) 酒井 ADD BEGIN
                        errorControls.Clear()
                        errorControls.AddRange(MakeErrorControlsBuhinNoSection(rowindex + titleRows))
                        ''↑↑2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化_bg) 酒井 ADD END
                        Throw New IllegalInputException("FINAL部品の部品番号が「DUMMY」のため、反映できません。修正してください。", errorControls.ToArray)
                        Exit Sub
                    End If
                End If
            Next

            ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化_bg) 酒井 ADD BEGIN
            'If 1 = errorControls.Count Then
            If 1 = i Then
                ''↑↑2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化_bg) 酒井 ADD END
                errorControls.Clear()
                ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化_bg) 酒井 ADD BEGIN
            ElseIf i > 1 Then
                Throw New IllegalInputException("FINAL部品は２つ以上同時に作成できません。", errorControls.ToArray)
                ''↑↑2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化_bg) 酒井 ADD END
            ElseIf 0 = errorControls.Count Then
                errorControls.AddRange(MakeErrorControlsLevelSection(rowindex + titleRows + 1))
                Throw New IllegalInputException("レベル項目が正しくありません", errorControls.ToArray)
            Else
                Throw New IllegalInputException("レベル項目が正しくありません", errorControls.ToArray)
            End If
        End Sub
        ''↑↑2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bg) (TES)張 ADD END

        ''' <summary>
        ''' 登録処理における入力チェックに問題がない事を実証する
        ''' </summary>
        ''' <remarks>問題が有れば、IllegalInputException</remarks>
        Public Sub AssertValidateRegister()


            DeleteRowCheck()

            Dim spreadRows As Integer() = ConvSubjectIndexToSpreadRow(subject.GetInputRowIndexes())

            '
            If spreadRows.Length = 0 Then
                Return
            End If

            AssertValidateSave(True)

            validatorRegister.AssertValidate(spreadRows)
        End Sub

        ''' <summary>
        ''' 余計な行を削除させる
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DeleteRowCheck()
            'なぜか-3が最初に来ている。'
            Dim spreadRows As Integer() = ConvSubjectIndexToSpreadRow(subject.GetInputRowIndexes())
            Dim removeCount As Integer = 0

            '全体のセルをチェックする'
            If spreadRows.Length = 0 Then
                Return
            End If

            '
            '   編集されたセルの行を確保するバッファ(spreadRows)の最大件数までしか削除処理を行っていない
            '   spreadRowsの最後に格納された値が最終行(spreadRowsは予めソートされてから格納されるため)
            '
            '   Changed by S.Tokizaki 2014.04.22
            '
            'For index As Integer = spreadRows(0) To spreadRows.Length + 3
            For index As Integer = spreadRows(0) To spreadRows(spreadRows.Length - 1)
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
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BUHIN_NAME).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_INSU_SURYO).Index).Value) Then
                    Continue For
                End If
                '
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value) Then
                    Continue For
                End If

                IsSuspend_SpreadChangeEvent = True
                Try
                    sheet.RemoveRows(index - removeCount, 1)
                    subject.RemoveRow(ConvSpreadRowToSubjectIndex(index - removeCount), 1)
                Finally
                    IsSuspend_SpreadChangeEvent = False
                End Try

                removeCount = removeCount + 1

            Next
        End Sub

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
            insuRange.AddTextCell(srcTag).Range(0, 9999)    '入力範囲

            'Dim insuAsterisc As New SpreadValidator(spread)
            'insuAsterisc.AddTextCell(srcTag).InArray(BuhinEditInsu.GREASE_FORM_VALUE)

            'IsNumericチェックがうまく動いていないので、処理追加
            Dim errorControls As New List(Of ErrorControl)
            For Each rowindex As Integer In subject.GetInputRowIndexes
                '員数取得
                Dim strInsu As String = sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).Value
                '員数値入力内容チェック
                'If Not strInsu.Equals("**") = True AndAlso Not IsNumeric(strInsu) = True AndAlso Not strInsu.Equals(String.Empty) = True Then
                If strInsu = Nothing Then
                ElseIf strInsu.Equals("**") = True Then
                ElseIf IsNumeric(strInsu) = True Then
                    If Int(strInsu) > 9999 Or Int(strInsu) < 0 Then
                        errorControls.AddRange(MakeErrorControlsInsuSection(rowindex + titleRows))
                    End If
                Else
                    errorControls.AddRange(MakeErrorControlsInsuSection(rowindex + titleRows))
                End If
                'If IsNumeric(strInsu) Then
                '    If Int(strInsu) > 9999 Or Int(strInsu) <= 0 Then
                '        errorControls.AddRange(MakeErrorControlsInsuSection(rowindex + titleRows))
                '    End If
                'End If
            Next
            If 0 < errorControls.Count Then
                Throw New IllegalInputException("員数項目に不正な値が存在します。", errorControls.ToArray)
            End If

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
                subject.InsertRow(ConvSpreadRowToSubjectIndex(spreadRow), rowCount)
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

        ''' <summary>
        ''' 行を削除する
        ''' </summary>
        ''' <param name="spreadRow">Spread行index</param>
        ''' <param name="rowCount">削除行数</param>
        ''' <remarks></remarks>
        Public Sub RemoveRows(ByVal spreadRow As Integer, ByVal rowCount As Integer)
            IsSuspend_SpreadChangeEvent = True
            Try
                sheet.RemoveRows(spreadRow, rowCount)
                subject.RemoveRow(ConvSpreadRowToSubjectIndex(spreadRow), rowCount)
            Finally
                IsSuspend_SpreadChangeEvent = False
            End Try
        End Sub

        '''' <summary>
        '''' 取引先コードの未入力チェック
        '''' </summary>
        '''' <remarks></remarks>
        'Public Sub AssertValidateKoseiMakerCodeAndNameCheck()
        '    'エラーリスト定義'
        '    Dim errorControls As New List(Of ErrorControl)
        '    For Each rowindex As Integer In subject.GetInputRowIndexes
        '        Dim shukeiCode As String = sheet.Cells(rowindex, sheet.Columns(TAG_SHUKEI_CODE).Index).Value
        '        '集計コードX,Rか「X,R,J」の場合エラーチェックしない'
        '        If Not StringUtil.IsEmpty(shukeiCode) Then
        '            Select Case shukeiCode
        '                Case "A"
        '                    '取引先名称が空で無いならチェックしなくてもよい'
        '                    If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_NAME).Index).Value) Then
        '                        If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_CODE).Index).Value) Then
        '                            errorControls.AddRange(MakeErrorControlsMakerCode(rowindex))
        '                        End If
        '                    End If
        '                Case "E"
        '                    If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_NAME).Index).Value) Then
        '                        If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_CODE).Index).Value) Then
        '                            errorControls.AddRange(MakeErrorControlsMakerCode(rowindex))
        '                        End If
        '                    End If
        '                Case "Y"
        '                    If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_NAME).Index).Value) Then
        '                        If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_CODE).Index).Value) Then
        '                            errorControls.AddRange(MakeErrorControlsMakerCode(rowindex))
        '                        End If
        '                    End If
        '            End Select

        '        Else
        '            Dim siaShukeiCode As String = sheet.Cells(rowindex, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value
        '            If Not StringUtil.IsEmpty(siaShukeiCode) Then
        '                Select Case siaShukeiCode
        '                    Case "A"
        '                        If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_NAME).Index).Value) Then
        '                            If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_CODE).Index).Value) Then
        '                                errorControls.AddRange(MakeErrorControlsMakerCode(rowindex))
        '                            End If
        '                        End If
        '                    Case "E"
        '                        If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_NAME).Index).Value) Then
        '                            If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_CODE).Index).Value) Then
        '                                errorControls.AddRange(MakeErrorControlsMakerCode(rowindex))
        '                            End If
        '                        End If
        '                    Case "Y"
        '                        If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_NAME).Index).Value) Then
        '                            If StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_MAKER_CODE).Index).Value) Then
        '                                errorControls.AddRange(MakeErrorControlsMakerCode(rowindex))
        '                            End If
        '                        End If
        '                End Select
        '            End If
        '        End If
        '    Next

        '    If 0 < errorControls.Count Then
        '        Throw New IllegalInputException("構成情報が存在しない", errorControls.ToArray)
        '    End If
        'End Sub

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
                        Case "A"
                            '取引先名称が空で無いならチェックしなくてもよい'
                            If StringUtil.IsEmpty(sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value) Then
                                errorControls.AddRange(MakeErrorControlsKyoukuSection(rowindex + titleRows))
                            End If
                        Case "E"
                            If StringUtil.IsEmpty(sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value) Then
                                errorControls.AddRange(MakeErrorControlsKyoukuSection(rowindex + titleRows))
                            End If
                        Case "Y"
                            If StringUtil.IsEmpty(sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value) Then
                                errorControls.AddRange(MakeErrorControlsKyoukuSection(rowindex + titleRows))
                            End If
                    End Select

                Else
                    Dim siaShukeiCode As String = sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value
                    If Not StringUtil.IsEmpty(siaShukeiCode) Then
                        Select Case siaShukeiCode
                            Case "A"
                                If StringUtil.IsEmpty(sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value) Then
                                    errorControls.AddRange(MakeErrorControlsKyoukuSection(rowindex + titleRows))
                                End If
                            Case "E"
                                If StringUtil.IsEmpty(sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value) Then
                                    errorControls.AddRange(MakeErrorControlsKyoukuSection(rowindex + titleRows))
                                End If
                            Case "Y"
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
        ''' 員数セクションの未入力チェック
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AssertValidateKoseiInsuSection()
            'エラーリスト定義'
            Dim errorControls As New List(Of ErrorControl)
            For Each rowindex As Integer In subject.GetInputRowIndexes

                '員数取得
                Dim strInsu As String = sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).Value
                ''員数値入力内容チェック
                'If Not strInsu.Equals("**") = True AndAlso Not IsNumeric(strInsu) = True AndAlso strInsu.Equals(String.Empty) = False Then
                '    '    'errorControls.AddRange(MakeErrorControlsInsuSection(rowindex + titleRows))
                '    '    Throw New ImplementedException("員数項目に不正な値が存在します。", errorControls.ToArray)

                '    '    Exit Sub
                'ElseIf IsNumeric(strInsu) Then
                '    If Int(strInsu) > 99 Then
                '        errorControls.AddRange(MakeErrorControlsInsuSection(rowindex + titleRows))
                '    End If
                'End If
                '員数値入力内容チェック
                If strInsu = Nothing Then
                ElseIf strInsu.Equals("**") = True Then
                ElseIf IsNumeric(strInsu) = True Then
                    If Int(strInsu) > 99 Or Int(strInsu) <= 0 Then
                        errorControls.AddRange(MakeErrorControlsInsuSection(rowindex + titleRows))
                    End If
                Else
                    errorControls.AddRange(MakeErrorControlsInsuSection(rowindex + titleRows))
                End If
            Next

            If 0 < errorControls.Count Then
                Throw New IllegalInputException("員数項目が正しくありません", errorControls.ToArray)
            End If
        End Sub

        ''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bh) (TES)張 ADD BEGIN
        Private Function MakeErrorControlsLevelSection(ByVal rowIndex As Integer) As ErrorControl()
            Dim spreadRows As Integer = rowIndex
            Dim spreadColumns As Integer = sheet.Columns(TAG_LEVEL).Index
            Return ValidatorUtil.MakeErrorControls(spread, spreadRows, spreadColumns)
        End Function
        ''↑↑2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bh) (TES)張 ADD END

        ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化_bh) 酒井 ADD BEGIN
        Private Function MakeErrorControlsBuhinNoSection(ByVal rowIndex As Integer) As ErrorControl()
            Dim spreadRows As Integer = rowIndex
            Dim spreadColumns As Integer = sheet.Columns(TAG_BUHIN_NO).Index
            Return ValidatorUtil.MakeErrorControls(spread, spreadRows, spreadColumns)
        End Function
        ''↑↑2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化_bh) 酒井 ADD END
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
        ''' 員数セクションをエラーとしたErrorContorlを作成する
        ''' </summary>
        ''' <param name="rowIndex">行番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MakeErrorControlsInsuSection(ByVal rowIndex As Integer) As ErrorControl()
            Dim spreadRows As Integer = rowIndex
            Dim spreadColumns As Integer = sheet.Columns(TAG_INSU_SURYO).Index
            Return ValidatorUtil.MakeErrorControls(spread, spreadRows, spreadColumns)
        End Function

        ''' <summary>
        ''' 抽出した部品情報をSPREADへセットする。
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub buhinListToSpread(ByVal buhinListVos As List(Of BuhinListVo), _
                                     ByVal hoyouEventCode As String, ByVal jikyu As String, _
                                     ByVal kaihatsuFugo As String, _
                                     ByVal isEbomRead As Boolean, _
                                     ByVal isNormal As Boolean)

            Try

                Dim intRowStart As Integer = 0
                Dim intRowEnd As Integer = 0
                Dim i As Integer = 0
                Dim j As Integer = 0

                'E-BOMから部品構成を取得するか用。　←試作手配システムのINSTLの場合はどの様に取得すべきか・・・要検討。
                Dim zenInstlHinban As String = ""
                Dim zenLevel As String = ""
                Dim zenSelectionMethod As String = ""

                '入力行数を確認してスタート位置（＋１）を決める。
                'For Each rowindex As Integer In subject.GetInputRowIndexes
                '    intRowStart = intRowStart + 1
                'Next
                '初回か初回以外か？
                If subject.GetInputRowIndexes.Count = 1 Then

                    intRowStart = 0

                    '
                    '   上記判定だけでは、１行目に何か入力されていても１行目から（要は先頭）から
                    '   更新されていく（subject.GetInputRowIndexes.Countの戻り値が問題）
                    '
                    For aCol As Integer = 0 To sheet.ColumnCount - 1

                        Dim s As String = sheet.Cells(titleRows, aCol).Value
                        If Not (s Is Nothing) Then
                            If s <> "" Then
                                intRowStart = subject.GetMaxInputRowIndex + 1
                                Exit For
                            End If
                        End If
                    Next
                Else
                    '
                    '   subject.GetInputRowIndexes.Countは当てにならん
                    '
                    'intRowStart = subject.GetInputRowIndexes.Count


                    For intRowStart = sheet.RowCount - 1 To 0 Step -1
                        Dim s As String = sheet.GetClipValue(intRowStart, -1, intRowStart, -1)
                        s = Replace(s, vbTab, "")
                        s = Replace(s, vbCrLf, "")
                        If s <> "" Then

                            intRowStart = intRowStart - titleRows + 1

                            Exit For
                        End If

                    Next


                    'For row = intRowStart To sheet.RowCount - 1
                    '    Dim s As String = sheet.GetClipValue(row, -1, 1, -1)
                    '    s = Replace(s, vbTab, "")
                    '    s = Replace(s, vbCrLf, "")
                    '    If s <> "" Then
                    '        intRowStart = row - titleRows + 1
                    '    End If

                    'Next
                End If

                If intRowStart >= 0 Then

                    Dim Shisakudate As ShisakuDate
                    Dim buhinstruct As HoyouBuhinTehaichoEditBuhinStructure
                    Dim newBuhinMatrix As New HoyouBuhinBuhinKoseiMatrix

                    'END位置を指定
                    intRowEnd = intRowStart + buhinListVos.Count - 1

                    '部品情報をセット
                    For rowindex As Integer = intRowStart To intRowEnd

                        '
                        '   これから、埋め込むセルが最大行を超えていたら1行追加
                        '
                        If rowindex + j + titleRows >= sheet.RowCount Then
                            sheet.RowCount = rowindex + j + titleRows + 1
                        End If

                        '値をスプレッドへ
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).Value = buhinListVos(i).BuhinNo.ToString.TrimStart
                        '--------------------------------------------------------------------------------------------
                        'レベル’
                        'sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_LEVEL).Index).Value = buhinListVos(i).Level.Value
                        '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                        '               →補用部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                        If StringUtil.IsEmpty(buhinListVos(i).Level.Value) Then
                            sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_LEVEL).Index).Value = ""
                        Else
                            sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_LEVEL).Index).Value = buhinListVos(i).Level.Value
                        End If
                        '--------------------------------------------------------------------------------------------
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).Value = buhinListVos(i).ShukeiCode.ToString
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value = buhinListVos(i).SiaShukeiCode.ToString
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).Value = buhinListVos(i).BuhinName.ToString
                        If StringUtil.Equals(buhinListVos(i).Insu, "**") Then
                            sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).Value = buhinListVos(i).Insu
                        Else
                            sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).Value = buhinListVos(i).Insu
                        End If

                        '
                        '   部品NOTEに空文字が入っている場合があるので空白を削除
                        '   Change by S.Tokizaki 2014.05.23
                        '
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NOTE).Index).Value = buhinListVos(i).Note.ToString.TrimEnd

                        '供給セクション
                        If buhinListVos(i).Kyokyu = KYOKYU_SECTION_JIKKEN Then
                            sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value = KYOKYU_SECTION_SHISAKU
                        Else
                            sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value = buhinListVos(i).Kyokyu
                        End If

                        '選択方法
                        Select Case buhinListVos(i).SelectionMethod
                            Case HOYOU_NOMAL
                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Value = _
                                    KANA_HOYOU_NOMAL
                            Case HOYOU_NOMAL_ALL
                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Value = _
                                    KANA_HOYOU_NOMAL_ALL
                            Case HOYOU_NOMAL_LESS
                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Value = _
                                    KANA_HOYOU_NOMAL_LESS
                            Case HOYOU_TANPIN
                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Value = _
                                    KANA_HOYOU_TANPIN
                            Case HOYOU_NOMAL_SHISAKU
                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Value = _
                                    KANA_HOYOU_NOMAL_SHISAKU
                            Case HOYOU_NOMAL_ALL_SHISAKU
                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Value = _
                                    KANA_HOYOU_NOMAL_ALL_SHISAKU
                            Case HOYOU_NOMAL_LESS_SHISAKU
                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Value = _
                                    KANA_HOYOU_NOMAL_LESS_SHISAKU
                            Case HOYOU_TANPIN_SHISAKU
                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Value = _
                                    KANA_HOYOU_TANPIN_SHISAKU
                            Case Else
                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Value = _
                                    buhinListVos(i).SelectionMethod
                        End Select







                        '   現調区分は、以下のようにせず、マトリックスから頂戴する

                        '
                        '   ここで、現調区分を取得し、反映させる   
                        '       ※できれば、ブロック選択時にまとめて現調区分も保持したいのだが、
                        '       　バッチ処理にて取得するルートも有るので反映時に処理させる
                        '
                        Dim ShiyouJyouhouDao As EventSakusei.KouseiBuhin.Dao.ShiyouJyouhouDaoImpl = New EventSakusei.KouseiBuhin.Dao.ShiyouJyouhouDaoImpl



                        '--- 532から現調区分取得 ---
                        Dim aRhac0532 As List(Of Rhac0532Vo) = ShiyouJyouhouDao.GetByRHAC0532Values(buhinListVos(i).BuhinNo.ToString.TrimStart)

                        Dim aGencho As String = ""

                        If aRhac0532 IsNot Nothing Then
                            If aRhac0532.Count > 0 Then
                                aGencho = aRhac0532(0).GencyoCkdKbn.Trim
                            End If
                        End If




                        '20140619 ファイナル品番選択時での自動構成取得動作オミットに伴いコメントアウト

                        '---　ここで手配システムの動作と、EBOM取得からの動作は切り分ける
                        'EBOMからのデータ取得時のみこの動作を行う
                        If isEbomRead And isNormal Then

                            '   部品のLevelが０ならば、マトリックスを作る
                            If buhinListVos(i).Level = 0 Then

                                Shisakudate = New ShisakuDate

                                buhinstruct = New HoyouBuhinTehaichoEditBuhinStructure(hoyouEventCode, "ListCodeDummy", _
                                                                             "BlockNoDummy", "BukaCodeDummy", Shisakudate)
                                newBuhinMatrix = buhinstruct.GetKouseiMatrix(buhinListVos(i).BuhinNo.ToString.TrimStart, "", 0, kaihatsuFugo)


                            End If

                            '   マトリクスから該当部品の現調を引っ張る
                            For Each k As Integer In newBuhinMatrix.GetInputRowIndexes

                                If buhinListVos(i).BuhinNo.Trim = newBuhinMatrix(k).BuhinNo Then
                                    aGencho = newBuhinMatrix(k).GencyoCkdKbn
                                End If

                            Next
                        End If
                        '---　ここで手配システムの動作と、EBOM取得からの動作は切り分ける

                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).Value = aGencho



                        'ひとつ前の部品、LEVELを保持
                        zenInstlHinban = buhinListVos(i).BuhinNo.ToString.TrimStart
                        zenLevel = buhinListVos(i).Level.ToString
                        zenSelectionMethod = sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Value

                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_LEVEL).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_LEVEL).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NOTE).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NOTE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に


                        '20140619   ファイナルの非展開状態での対応を一旦オミット

                        '---　ここで手配システムの動作と、EBOM取得からの動作は切り分ける

                        'EBOMからのデータ取得時のみこの動作を行う
                        If isEbomRead And isNormal Then

                            '自分が0レベルでひとつ前のレベルも0の場合、0レベル以下全て選択されたものとみなしINSTLの構成を取得する。


                            Dim CurrentLevel As Integer = buhinListVos(i).Level
                            Dim NextLevel As Integer = 0
                            If i < buhinListVos.Count - 1 Then
                                NextLevel = buhinListVos(i + 1).Level
                            End If

                            If CurrentLevel = 0 And NextLevel = 0 Then
                                Console.WriteLine()
                                'End If

                                'If StringUtil.Equals(buhinListVos(i).Level.ToString, "0") And _
                                '   StringUtil.Equals(buhinListVos(i).SelectionMethod, HOYOU_NOMAL_ALL) _
                                '   Then


                                '
                                '   現調区分反映を構成展開した時も行うので、Level０の時は、必ずマトリックスを開く様にする
                                '
                                '部品構成取得
                                '   手配帳編集画面の部品展開ロジックを使用
                                '   手配帳編集画面から当機能へ持ってくるかは編集画面の改修内容を考慮して再検討する。
                                'Dim Shisakudate As ShisakuDate = New ShisakuDate
                                'Dim buhinstruct As New TehaichoEditBuhinStructure(hoyouEventCode, "ListCodeDummy", _
                                '                                                  "BlockNoDummy", "BukaCodeDummy", Shisakudate)
                                'Dim newBuhinMatrix As BuhinKoseiMatrix = buhinstruct.GetKouseiMatrix(buhinListVos(i).BuhinNo.ToString.TrimStart, "", 0, kaihatsuFugo)




                                '部品構成をSPREADへ表示する。
                                If newBuhinMatrix Is Nothing OrElse newBuhinMatrix.Records.Count > 0 Then
                                    For Each k As Integer In newBuhinMatrix.GetInputRowIndexes

                                        '自分は除く
                                        If Not StringUtil.Equals(newBuhinMatrix(k).Level, 0) Then

                                            '自給品有無＝無の場合　国内または国外集計コードＪは読み飛ばす。
                                            If jikyu = "1" Or _
                                                jikyu = "0" And newBuhinMatrix(k).ShukeiCode.TrimEnd <> "" _
                                                            And newBuhinMatrix(k).ShukeiCode.TrimEnd <> "J" Or _
                                                jikyu = "0" And newBuhinMatrix(k).ShukeiCode.TrimEnd = "" _
                                                            And newBuhinMatrix(k).SiaShukeiCode.TrimEnd <> "" _
                                                            And newBuhinMatrix(k).SiaShukeiCode.TrimEnd <> "J" Then

                                                'カウント
                                                j = j + 1

                                                '員数を求める。
                                                Dim lstInsu As List(Of Integer) = newBuhinMatrix.GetInputInsuColumnIndexes
                                                If lstInsu.Count <= 0 Then
                                                    Continue For
                                                End If
                                                Dim getInsu As Integer = 0
                                                For l As Integer = 0 To lstInsu.Count - 1
                                                    '号車員数ゲット
                                                    If newBuhinMatrix.InsuSuryo(k, lstInsu(l)) <> 0 Then
                                                        getInsu = newBuhinMatrix.InsuSuryo(k, lstInsu(l))
                                                        Exit For
                                                    End If
                                                Next
                                                Dim strInsu As String = ""
                                                If StringUtil.Equals(getInsu, -1) Then
                                                    strInsu = "**"
                                                Else
                                                    strInsu = CStr(getInsu)
                                                End If

                                                '
                                                '   これから、埋め込むセルが最大行を超えていたら1行追加
                                                '
                                                If rowindex + j + titleRows >= sheet.RowCount Then
                                                    sheet.RowCount = rowindex + j + titleRows + 1
                                                End If

                                                'セルに設定
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).Value = newBuhinMatrix(k).BuhinNo
                                                '--------------------------------------------------------------------------------------------
                                                'レベル’
                                                'sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_LEVEL).Index).Value = newBuhinMatrix(k).Level
                                                '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                                                '               →補用部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                                                If StringUtil.IsEmpty(newBuhinMatrix(k).Level) Then
                                                    sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_LEVEL).Index).Value = ""
                                                Else
                                                    sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_LEVEL).Index).Value = newBuhinMatrix(k).Level
                                                End If
                                                '--------------------------------------------------------------------------------------------
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).Value = newBuhinMatrix(k).ShukeiCode
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value = newBuhinMatrix(k).SiaShukeiCode
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).Value = newBuhinMatrix(k).BuhinName
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).Value = strInsu

                                                If newBuhinMatrix(k).KyoukuSection = KYOKYU_SECTION_JIKKEN Then
                                                    sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value = KYOKYU_SECTION_SHISAKU
                                                Else
                                                    sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value = newBuhinMatrix(k).KyoukuSection
                                                End If


                                                '
                                                '   部品NOTEに空文字が入っている場合があるので空白を削除
                                                '   Change by S.Tokizaki 2014.05.23
                                                '
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NOTE).Index).Value = newBuhinMatrix(k).BuhinNote.TrimEnd

                                                '
                                                '   現調区分も追加  S.Tokizaki  2014.05.08
                                                '
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).Value = newBuhinMatrix(k).GencyoCkdKbn


                                                '   選択方法（親と同じものをセット）
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Value = zenSelectionMethod
                                                '文字色も変更する。
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).ForeColor = Color.Blue   '文字色を変更
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_LEVEL).Index).ForeColor = Color.Blue   '文字色を変更
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_LEVEL).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).ForeColor = Color.Blue   '文字色を変更
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).ForeColor = Color.Blue   '文字色を変更
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).ForeColor = Color.Blue   '文字色を変更
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).ForeColor = Color.Blue   '文字色を変更
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).ForeColor = Color.Blue   '文字色を変更
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NOTE).Index).ForeColor = Color.Blue   '文字色を変更
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_BUHIN_NOTE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).ForeColor = Color.Blue   '文字色を変更
                                                sheet.Cells(rowindex + j + titleRows, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に


                                            End If

                                        End If

                                    Next

                                End If

                            End If
                        End If
                        '---　ここで手配システムの動作と、EBOM取得からの動作は切り分ける

                        'カウント
                        i = i + 1

                    Next


                    ''最後のレベルも0の場合、0レベル以下全て選択されたものとみなしひとつ前のINSTLで構成を取得する。
                    'If StringUtil.Equals(zenLevel, "0") Then

                    '    '部品構成取得
                    '    '   手配帳編集画面の部品展開ロジックを使用
                    '    '   手配帳編集画面から当機能へ持ってくるかは編集画面の改修内容を考慮して再検討する。
                    '    Dim Shisakudate As ShisakuDate = New ShisakuDate
                    '    Dim buhinstruct As New TehaichoEditBuhinStructure(hoyouEventCode, "ListCodeDummy", _
                    '                                                      "BlockNoDummy", "BukaCodeDummy", Shisakudate)
                    '    Dim newBuhinMatrix As BuhinKoseiMatrix = buhinstruct.GetKouseiMatrix(zenInstlHinban, "", 0)
                    '    '部品構成をSPREADへ表示する。
                    '    If newBuhinMatrix Is Nothing OrElse newBuhinMatrix.Records.Count > 0 Then

                    '        For k As Integer = 1 To newBuhinMatrix.Records.Count - 1
                    '            'カウント
                    '            j = j + 1

                    '            '員数を求める。
                    '            Dim lstInsu As List(Of Integer) = newBuhinMatrix.GetInputInsuColumnIndexes
                    '            If lstInsu.Count <= 0 Then
                    '                Continue For
                    '            End If
                    '            Dim getInsu As Integer = 0
                    '            For l As Integer = 0 To lstInsu.Count - 1
                    '                '号車員数ゲット
                    '                If newBuhinMatrix.InsuSuryo(k, lstInsu(l)) <> 0 Then
                    '                    getInsu = newBuhinMatrix.InsuSuryo(k, lstInsu(l))
                    '                    Exit For
                    '                End If
                    '            Next
                    '            Dim strInsu As String = ""
                    '            If StringUtil.Equals(getInsu, -1) Then
                    '                strInsu = "**"
                    '            Else
                    '                strInsu = CStr(getInsu)
                    '            End If
                    '            'セルに設定
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).Value = newBuhinMatrix.Record(k).BuhinNo
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_LEVEL).Index).Value = newBuhinMatrix.Record(k).Level
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).Value = newBuhinMatrix.Record(k).ShukeiCode
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value = newBuhinMatrix.Record(k).SiaShukeiCode
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).Value = newBuhinMatrix.Record(k).BuhinName
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_SAISHIYOUFUKA).Index).Value = strInsu
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_BUHIN_NOTE).Index).Value = newBuhinMatrix.Record(k).BuhinNote
                    '            '文字色も変更する。
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).ForeColor = Color.Blue   '文字色を変更
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_LEVEL).Index).ForeColor = Color.Blue   '文字色を変更
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_LEVEL).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).ForeColor = Color.Blue   '文字色を変更
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).ForeColor = Color.Blue   '文字色を変更
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).ForeColor = Color.Blue   '文字色を変更
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_SAISHIYOUFUKA).Index).ForeColor = Color.Blue   '文字色を変更
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_SAISHIYOUFUKA).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_BUHIN_NOTE).Index).ForeColor = Color.Blue   '文字色を変更
                    '            sheet.Cells(j + titleRows, sheet.Columns(TAG_BUHIN_NOTE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    '        Next

                    '    End If

                    'End If


                    '
                    '   途中で行を追加していった結果、最終行まで埋め尽くされてしまった場合、1行追加
                    '
                    Dim Row As Integer = sheet.RowCount - 1
                    Dim s As String = sheet.GetClipValue(Row, -1, 1, -1)
                    s = Replace(s, vbTab, "")
                    s = Replace(s, vbCrLf, "")
                    If s <> "" Then
                        sheet.RowCount += 1
                    End If

                End If

            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try

        End Sub

#Region " 「過去部品表　検索」画面から「補用部品表 編集」画面へのデータセット "

#Region "取込機能(バックアップ)"
        ''' <summary>
        ''' 取込機能(バックアップ)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub importBackup(ByVal buhinListVos As List(Of THoyouBuhinEditVo))

            Dim spdBase As FpSpread = spread

            'スプレッドバックアップ
            Dim baseCopyIndex As Integer = -1
            baseCopyIndex = spdBase.Sheets.Add(TehaichoEdit.TehaichoEditLogic.CopySheet(sheet))
            spdBase.Sheets(baseCopyIndex).SheetName = _SHEET_NAME

            '部品表にデータを貼り付ける。
            OldbuhinListToSpread(buhinListVos)

            'バックアップシートの非表示
            spdBase.Sheets(baseCopyIndex).Visible = False
        End Sub

#End Region
#Region " 「補用部品表 編集」画面へのデータセット "

        ''' <summary>
        ''' 抽出した部品情報をSPREADへセットする。
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub OldbuhinListToSpread(ByVal buhinListVos As List(Of THoyouBuhinEditVo))

            Try
                '全データ消去
                TehaichoEdit.TehaichoEditLogic.SpreadAllClear(sheet)

                '部品情報をセット
                For rowindex As Integer = 0 To buhinListVos.Count - 1
                    If rowindex = 0 Then
                        'メモのタイトル列
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_1).Index).Value = buhinListVos(rowindex).Memo1T.TrimEnd
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_2).Index).Value = buhinListVos(rowindex).Memo2T.TrimEnd
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_3).Index).Value = buhinListVos(rowindex).Memo3T.TrimEnd
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_4).Index).Value = buhinListVos(rowindex).Memo4T.TrimEnd
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_5).Index).Value = buhinListVos(rowindex).Memo5T.TrimEnd
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_6).Index).Value = buhinListVos(rowindex).Memo6T.TrimEnd
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_7).Index).Value = buhinListVos(rowindex).Memo7T.TrimEnd
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_8).Index).Value = buhinListVos(rowindex).Memo8T.TrimEnd
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_9).Index).Value = buhinListVos(rowindex).Memo9T.TrimEnd
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_10).Index).Value = buhinListVos(rowindex).Memo10T.TrimEnd

                        '文字色も変更する。
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_1).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_1).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_2).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_2).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_3).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_3).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_4).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_4).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_5).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_5).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_6).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_6).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_7).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_7).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_8).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_8).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_9).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_9).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_10).Index).ForeColor = Color.Blue   '文字色を変更
                        sheet.Cells(MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_10).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    End If
                    '値をスプレッドへ
                    'sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).Value = buhinListVos(rowindex).BuhinNo.ToString.TrimStart
                    'sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_LEVEL).Index).Value = buhinListVos(rowindex).Level.Value
                    'sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).Value = buhinListVos(rowindex).ShukeiCode.ToString
                    'sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value = buhinListVos(rowindex).SiaShukeiCode.ToString
                    'sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).Value = buhinListVos(rowindex).BuhinName.ToString

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).Value = buhinListVos(rowindex).BuhinNo
                    '--------------------------------------------------------------------------------------------
                    'レベル’
                    'sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_LEVEL).Index).Value = buhinListVos(rowindex).Level
                    '   2014/02/13　レベルがブランクの場合、ブランクでマージする。
                    '               →補用部品表編集画面で一時保存の場合レベルがブランクでも登録できてしまう。
                    If StringUtil.IsEmpty(buhinListVos(rowindex).Level) Then
                        sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_LEVEL).Index).Value = ""
                    Else
                        sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_LEVEL).Index).Value = buhinListVos(rowindex).Level
                    End If
                    '--------------------------------------------------------------------------------------------
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).Value = buhinListVos(rowindex).ShukeiCode
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value = buhinListVos(rowindex).SiaShukeiCode
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).Value = buhinListVos(rowindex).BuhinName

                    If StringUtil.Equals(buhinListVos(rowindex).InsuSuryo, "-1") Then
                        sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).Value = "**"
                    Else
                        sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).Value = buhinListVos(rowindex).InsuSuryo
                    End If


                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).Value = buhinListVos(rowindex).InsuSuryo

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BUHIN_NOTE).Index).Value = buhinListVos(rowindex).BuhinNote.TrimEnd
                    '供給セクション
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value = buhinListVos(rowindex).KyoukuSection

                    '選択方法
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Value = buhinListVos(rowindex).InputFlg

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).Value = buhinListVos(rowindex).GencyoCkdKbn
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MAKER_CODE).Index).Value = buhinListVos(rowindex).MakerCode
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MAKER_NAME).Index).Value = buhinListVos(rowindex).MakerName
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_1).Index).Value = buhinListVos(rowindex).Memo1
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_2).Index).Value = buhinListVos(rowindex).Memo2
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_3).Index).Value = buhinListVos(rowindex).Memo3
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_4).Index).Value = buhinListVos(rowindex).Memo4
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_5).Index).Value = buhinListVos(rowindex).Memo5
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_6).Index).Value = buhinListVos(rowindex).Memo6
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_7).Index).Value = buhinListVos(rowindex).Memo7
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_8).Index).Value = buhinListVos(rowindex).Memo8
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_9).Index).Value = buhinListVos(rowindex).Memo9
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_10).Index).Value = buhinListVos(rowindex).Memo10
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BIKOU).Index).Value = buhinListVos(rowindex).Bikou

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_1).Index).Value = buhinListVos(rowindex).MemoSheet1
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_2).Index).Value = buhinListVos(rowindex).MemoSheet2
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_3).Index).Value = buhinListVos(rowindex).MemoSheet3
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_4).Index).Value = buhinListVos(rowindex).MemoSheet4
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_5).Index).Value = buhinListVos(rowindex).MemoSheet5
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_6).Index).Value = buhinListVos(rowindex).MemoSheet6
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_7).Index).Value = buhinListVos(rowindex).MemoSheet7
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_8).Index).Value = buhinListVos(rowindex).MemoSheet8
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_DOOR_TRIM_1).Index).Value = buhinListVos(rowindex).MemoDoorTrim1
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_DOOR_TRIM_2).Index).Value = buhinListVos(rowindex).MemoDoorTrim2
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_ROOF_TRIM_1).Index).Value = buhinListVos(rowindex).MemoRoofTrim1
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_ROOF_TRIM_2).Index).Value = buhinListVos(rowindex).MemoRoofTrim2
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SUNROOF_TRIM_1).Index).Value = buhinListVos(rowindex).MemoSunroofTrim1

                    '文字色も変更する。
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_LEVEL).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_LEVEL).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_SHUKEI_CODE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_GENCYO_CKD_KBN).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MAKER_CODE).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MAKER_CODE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MAKER_NAME).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MAKER_NAME).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BUHIN_NO).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BUHIN_NAME).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_INSU_SURYO).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_KYOUKU_SECTION).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BUHIN_NOTE).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BUHIN_NOTE).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_1).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_1).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_2).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_2).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_3).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_3).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_4).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_4).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_5).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_5).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_6).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_6).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_7).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_7).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_8).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_8).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_9).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_9).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_10).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_10).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_1).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_1).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_2).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_2).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_3).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_3).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_4).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_4).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_5).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_5).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_6).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_6).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_7).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_7).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_8).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SHEET_8).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_DOOR_TRIM_1).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_DOOR_TRIM_1).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_DOOR_TRIM_2).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_DOOR_TRIM_2).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_ROOF_TRIM_1).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_ROOF_TRIM_1).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_ROOF_TRIM_2).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_ROOF_TRIM_2).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SUNROOF_TRIM_1).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_MEMO_SUNROOF_TRIM_1).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BIKOU).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_BIKOU).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に

                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).ForeColor = Color.Blue   '文字色を変更
                    sheet.Cells(rowindex + titleRows, sheet.Columns(TAG_INPUT_FLG).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                Next

            Catch ex As Exception
                Console.WriteLine("Error:" & ex.Message)
            End Try

        End Sub
#End Region

#Region "取込機能(バックアップ削除)"
        ''' <summary>
        ''' 取込機能(バックアップ削除)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub importBacupRemove()
            Dim spdBase As FpSpread = spread

            'バックアップ基本シート削除
            spdBase.Sheets.Remove(spdBase.Sheets(1))

            subject.DeleteObservers()
        End Sub
#End Region

#Region "取込機能(リストア)"
        ''' <summary>
        ''' 取込機能(リストア)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub importRestore()

            Dim spdBase As FpSpread = spread

            'sheetへのコピー
            CopyToSpread(spdBase.Sheets(1))

            'バックアップ削除
            importBacupRemove()
        End Sub
#End Region


#Region " 「補用部品表 編集」画面へのデータを戻す "

        ''' <summary>
        ''' SPREADのデータを戻す。
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub CopyToSpread(ByVal sheetBak As SheetView)

            'Try
            '全データ消去
            TehaichoEdit.TehaichoEditLogic.SpreadAllClear(sheet)

            'メモのタイトル列
            CopyToSpread_Sub(sheetBak, MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_1).Index)
            CopyToSpread_Sub(sheetBak, MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_2).Index)
            CopyToSpread_Sub(sheetBak, MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_3).Index)
            CopyToSpread_Sub(sheetBak, MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_4).Index)
            CopyToSpread_Sub(sheetBak, MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_5).Index)
            CopyToSpread_Sub(sheetBak, MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_6).Index)
            CopyToSpread_Sub(sheetBak, MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_7).Index)
            CopyToSpread_Sub(sheetBak, MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_8).Index)
            CopyToSpread_Sub(sheetBak, MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_9).Index)
            CopyToSpread_Sub(sheetBak, MEMO_ROW_INDEX, sheet.Columns(TAG_MEMO_10).Index)

            '部品情報をセット
            For row As Integer = MEMO_ROW_END_INDEX + 1 To sheetBak.RowCount - 1
                For col As Integer = 0 To sheetBak.Columns.Count - 1
                    CopyToSpread_Sub(sheetBak, row, col)
                Next
            Next

            'Catch ex As Exception
            '    Console.WriteLine("Error:" & ex.Message)
            'End Try

        End Sub

        ''' <summary>
        ''' SPREADをコピーする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub CopyToSpread_Sub(ByVal sheetBak As SheetView, ByVal arow As Integer, ByVal aIndex As Integer)

            '値をコピー
            If StringUtil.IsEmpty(sheetBak.Cells(arow, aIndex).Value) Then
                sheet.Cells(arow, aIndex).Value = ""
            Else
                sheet.Cells(arow, aIndex).Value = sheetBak.Cells(arow, aIndex).Value
            End If
            '文字色も変更する。
            sheet.Cells(arow, aIndex).ForeColor = sheetBak.Cells(arow, aIndex).ForeColor
            sheet.Cells(arow, aIndex).Font = sheetBak.Cells(arow, aIndex).Font

        End Sub
#End Region
#End Region


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

        ''↓↓2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD BEGIN
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
        ''↑↑2014/08/26 Ⅰ.3.設計編集 ベース車改修専用化_bx) (TES)張 ADD END
    End Class

End Namespace