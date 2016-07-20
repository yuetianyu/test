Imports ShisakuCommon.Ui.Access
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.CellType
Imports FarPoint.Win.Spread.Model
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Util
Imports ShisakuCommon.Util.OptionFilter
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic.Matrix
Imports YosansyoTool.YosanBuhinEdit.Logic
Imports YosansyoTool.YosanBuhinEdit.Ui
Imports YosansyoTool.YosanBuhinEdit.KouseiBuhin.Dao
Imports YosansyoTool.YosanBuhinEdit.KouseiBuhin.Dao.Vo
Imports EventSakusei.TehaichoEdit.Dao
Imports FarPoint.Win

Namespace YosanBuhinEdit.Kosei.Ui
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

#Region "各列のTAG"
        Public Const TAG_BUKA_CODE As String = "YOSAN_BUKA_CODE"
        Public Const TAG_BLOCK_NO As String = "YOSAN_BLOCK_NO"
        Public Const TAG_LEVEL As String = "YOSAN_LEVEL"
        Public Const TAG_SHUKEI_CODE As String = "YOSAN_SHUKEI_CODE"
        Public Const TAG_SIA_SHUKEI_CODE As String = "YOSAN_SIA_SHUKEI_CODE"
        Public Const TAG_MAKER_CODE As String = "YOSAN_MAKER_CODE"
        Public Const TAG_MAKER_NAME As String = "YOSAN_MAKER_NAME"
        Public Const TAG_BUHIN_NO As String = "YOSAN_BUHIN_NO"
        Public Const TAG_BUHIN_NO_KBN As String = "YOSAN_BUHIN_NO_KBN"
        Public Const TAG_BUHIN_NAME As String = "YOSAN_BUHIN_NAME"
        Public Const TAG_KYOUKU_SECTION As String = "YOSAN_KYOUKU_SECTION"
        Public Const TAG_HENKO_GAIYO As String = "YOSAN_HENKO_GAIYO"
        Public Const TAG_BUHIN_HI_RYOSAN As String = "YOSAN_BUHIN_HI_RYOSAN"
        Public Const TAG_BUHIN_HI_BUHINHYO As String = "YOSAN_BUHIN_HI_BUHINHYO"
        Public Const TAG_BUHIN_HI_TOKKI As String = "YOSAN_BUHIN_HI_TOKKI"
        Public Const TAG_KATA_HI As String = "YOSAN_KATA_HI"
        Public Const TAG_JIGU_HI As String = "YOSAN_JIGU_HI"
        Public Const TAG_KOSU As String = "YOSAN_KOSU"
        Public Const TAG_HACHU_JISEKI_MIX As String = "YOSAN_HACHU_JISEKI_MIX"
#End Region

        '''<summary>シート名称</summary>>
        Private Const _SHEET_NAME As String = "KOSEI"

        Private Shared ReadOnly DEFAULT_LOCK_TAGS As String() = {}

        ''' <summary>INSTL列の初期表示列数</summary>
        Public Const DEFAULT_INSTL_COLUMN_COUNT As Integer = 1

        Private Const TITLE_ROW_INDEX As Integer = -1
        Private Const INSTL_INFO_START_COLUMN As Integer = 11
        Private Const ALL_MIDASHI_ROW_INDEX As Integer = 0

        'パターン欄
        Private Const INSTL_PATTERN_MIDASHI_ROW_INDEX As Integer = 0
        Private Const INSTL_PATTERN_NO_ROW_INDEX As Integer = 1
        Public Const INSTL_PATTERN_ROW_INDEX As Integer = 2

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
            Me.sheet.SheetName = _SHEET_NAME
            Me.subject = subject
            condVoList = New List(Of clsOptionFilterVo)

            Me.titleRows = BuhinEditSpreadUtil.GetTitleRows(sheet)

            InsuArray = New Integer(subject.GetInputRowIndexes.Count + 100)() {}

            subject.AddObserver(Me)
        End Sub

#Region "各CellType"
        Private TateTitleCellType As TextCellType
        Private InstlPatternMidashiCellType As TextCellType
        Private InstlHinbanNoCellType As TextCellType
        Private PatternMidashiCellType As TextCellType
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
                'TateTitleCellType.CharacterSet = CharacterSet.AllIME
            End If
            Return TateTitleCellType
        End Function
        ''' <summary>
        ''' 固定員数見出しセルを返す
        ''' </summary>
        ''' <returns>固定員数見出しセル</returns>
        ''' <remarks></remarks>
        Public Function GetKoteiInstlMidashiCellType() As TextCellType
            If InstlPatternMidashiCellType Is Nothing Then
                InstlPatternMidashiCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                InstlPatternMidashiCellType.TextOrientation = FarPoint.Win.TextOrientation.TextHorizontal
            End If
            Return InstlPatternMidashiCellType
        End Function
        ''' <summary>
        ''' 員数Noセルを返す
        ''' </summary>
        ''' <returns>員数Noセル</returns>
        ''' <remarks></remarks>
        Public Function GetInstlPatternNoCellType() As TextCellType
            If InstlHinbanNoCellType Is Nothing Then

                InstlHinbanNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                InstlHinbanNoCellType.MaxLength = 3
            End If
            Return InstlHinbanNoCellType
        End Function
        ''' <summary>
        ''' パターン列見出しセルを返す
        ''' </summary>
        ''' <returns>パターン列見出しセル</returns>
        ''' <remarks></remarks>
        Public Function GetPatternMidashiCellType() As TextCellType
            If PatternMidashiCellType Is Nothing Then
                PatternMidashiCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                PatternMidashiCellType.MaxLength = 256
                PatternMidashiCellType.TextOrientation = FarPoint.Win.TextOrientation.TextTopDown
            End If
            Return PatternMidashiCellType
        End Function
#End Region

#Region "初期化"
        ''' <summary>
        ''' 初期化する（一度だけ実行する事を想定）
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Initialize()

            BuhinEditSpreadUtil.InitializeFrm41(spread)

            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUKA_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BLOCK_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_LEVEL
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUKEI_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SIA_SHUKEI_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MAKER_CODE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_MAKER_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NO_KBN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KYOUKU_SECTION
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_HENKO_GAIYO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_HI_RYOSAN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_HI_BUHINHYO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_BUHIN_HI_TOKKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KATA_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_JIGU_HI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KOSU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_HACHU_JISEKI_MIX

            ' *** 行に設定 ***
            With sheet.Rows(ALL_MIDASHI_ROW_INDEX)
                .BackColor = System.Drawing.SystemColors.Control
                .Locked = True
            End With
            With sheet.Rows(INSTL_PATTERN_NO_ROW_INDEX)
                .BackColor = System.Drawing.SystemColors.Control
                .Locked = True
            End With
            With sheet.Rows(INSTL_PATTERN_ROW_INDEX)
                .BackColor = Color.White
                .Locked = False
            End With

            sheet.Rows(INSTL_PATTERN_MIDASHI_ROW_INDEX).CellType = GetKoteiInstlMidashiCellType()
            sheet.Rows(INSTL_PATTERN_NO_ROW_INDEX).CellType = GetInstlPatternNoCellType()
            sheet.Rows(INSTL_PATTERN_ROW_INDEX).CellType = GetPatternMidashiCellType()

            ' *** 縦書き表示のタイトル ***
            Dim TATE_TITLE_COLUMN_TAGS As String() = New String() {TAG_BUKA_CODE, TAG_BLOCK_NO, TAG_LEVEL, TAG_SHUKEI_CODE, TAG_SIA_SHUKEI_CODE, TAG_MAKER_CODE}
            For Each tag As String In TATE_TITLE_COLUMN_TAGS
                With sheet.Cells(ALL_MIDASHI_ROW_INDEX, sheet.Columns(tag).Index)
                    .CellType = GetTateTitleCellType()
                End With
            Next

            ' ** 供給セクション **
            With sheet.Columns(TAG_KYOUKU_SECTION)
                .Border = borderFactory.GetRightWLine
            End With
            With sheet.Cells(ALL_MIDASHI_ROW_INDEX, sheet.Columns(TAG_KYOUKU_SECTION).Index)
                .Border = borderFactory.GetUnderLineAndRightWLine
            End With
            ' ** 変更概要 **
            With sheet.Columns(TAG_HENKO_GAIYO)
                .Border = borderFactory.GetLeftWLine
            End With
            With sheet.Cells(ALL_MIDASHI_ROW_INDEX, sheet.Columns(TAG_HENKO_GAIYO).Index)
                .Border = borderFactory.GetUnderLineAndLeftWLine
            End With

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUKA_CODE, cellTypeFactory.BukaCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BLOCK_NO, cellTypeFactory.BlockNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_LEVEL, cellTypeFactory.LevelCellType)

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SHUKEI_CODE, cellTypeFactory.ShukeiCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_SIA_SHUKEI_CODE, cellTypeFactory.SiaShukeiCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MAKER_CODE, cellTypeFactory.MakerCodeCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_MAKER_NAME, cellTypeFactory.MakerNameCellType)

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO, cellTypeFactory.BuhinNoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NO_KBN, cellTypeFactory.BuhinNoKbnCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_NAME, cellTypeFactory.BuhinNameCellType)

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KYOUKU_SECTION, cellTypeFactory.KyoukuSectionCellType)

            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_HENKO_GAIYO, cellTypeFactory.HenkoGaiyoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_HI_RYOSAN, cellTypeFactory.BuhinHiRyosanCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_HI_BUHINHYO, cellTypeFactory.BuhinHiBuhinhyoCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_BUHIN_HI_TOKKI, cellTypeFactory.BuhinHiTokkiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KATA_HI, cellTypeFactory.KataHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_JIGU_HI, cellTypeFactory.JiguHiCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_KOSU, cellTypeFactory.KosuCellType)
            BuhinEditSpreadUtil.BindCellTypeToColumn(sheet, TAG_HACHU_JISEKI_MIX, cellTypeFactory.HachuJisekiMixCellType)

            anInstlColumns = New InstlColumns(sheet, borderFactory)
            anInstlColumns.InitializeColumnInstl(Math.Max(DEFAULT_INSTL_COLUMN_COUNT, subject.GetInputInstlPatternColumnCount))

            'IMEを使用可能にする。
            spread.ImeMode = Windows.Forms.ImeMode.NoControl

            InitializeValidator()

            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)

        End Sub

        Private Sub InitializeValidator()
            ' 保存時は、DB登録に欠かせないチェックだけ
            validatorSave = New SpreadValidator(spread)


            '変更概要２０文字以上がいる！とりあえずDBと同じバイトに変更してみる'
            validatorSave.Add(TAG_HENKO_GAIYO, "変更概要").MaxLengthByte(256)
            validatorSave.Add(TAG_BUHIN_NAME, "部品名称").MaxLengthByte(30)
            validatorSave.Add(TAG_BUHIN_NAME, "取引先名称").MaxLengthByte(102)

            ' 登録の入力チェック
            validatorRegister = New SpreadValidator(spread)

            'validatorRegister.Add(TAG_BUKA_CODE, "部課コード").Required()
            validatorRegister.Add(TAG_BLOCK_NO, "ブロック№").Required()
            validatorRegister.Add(TAG_LEVEL, "レベル").Required().Numeric()
            'validatorRegister.Add(TAG_MAKER_CODE, "取引先コード").Required()
            'validatorRegister.Add(TAG_MAKER_NAME, "取引先名称").Required()
            validatorRegister.Add(TAG_BUHIN_NO, "部品番号").Required()
            validatorRegister.Add(TAG_BUHIN_NAME, "部品名称").Required()

            validatorRegister.Add(TAG_SHUKEI_CODE, "国内集計").InArray(TShisakuBuhinEditVoHelper.ShukeiCode.ALL)
            validatorRegister.Add(TAG_SIA_SHUKEI_CODE, "海外集計").InArray(TShisakuBuhinEditVoHelper.SiaShukeiCode.ALL)
            validatorRegister.Add(ValidatorUtil.NewValidatorByOr("国内集計または海外集計のどちらかを入力してください。", _
                                                          ValidatorUtil.NewRequiredValidator(spread, TAG_SHUKEI_CODE), _
                                                          ValidatorUtil.NewRequiredValidator(spread, TAG_SIA_SHUKEI_CODE)))

            validatorRegister.Add(TAG_BUKA_CODE, "部課コード").MaxLengthByte(4)
            validatorRegister.Add(TAG_BLOCK_NO, "ブロック№").MaxLengthByte(4)
            validatorRegister.Add(TAG_LEVEL, "レベル").MaxLengthByte(1)
            validatorRegister.Add(TAG_MAKER_CODE, "取引先コード").MaxLengthByte(4)
            validatorRegister.Add(TAG_MAKER_NAME, "取引先名称").MaxLengthByte(102)
            validatorRegister.Add(TAG_BUHIN_NO, "部品番号").MaxLengthByte(15)
            validatorRegister.Add(TAG_BUHIN_NO_KBN, "部品番号試作区分").MaxLengthByte(5)
            validatorRegister.Add(TAG_BUHIN_NAME, "部品名称").MaxLengthByte(30)
            validatorRegister.Add(TAG_KYOUKU_SECTION, "供給セクション").MaxLengthByte(5)
            validatorRegister.Add(TAG_HENKO_GAIYO, "変更概要").MaxLengthByte(256)

            '登録の入力チェック(構成)'
            validatorKosei = New SpreadValidator(spread)
        End Sub
#End Region

#Region "員数列の挿入・初期化"
        ''' <summary>
        ''' 員数列の挿入・初期化
        ''' </summary>
        ''' <remarks>
        ''' InstlColumnCountを隠蔽する<br/>
        ''' けれど「CellTypeは行に設定済み」とか、結構依存している. 上手に切り分けが出来ない. 困った.
        ''' </remarks>
        Private Class InstlColumns
            Public Const MAX_COLUMN_COUNT As Integer = 1000
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
                'UpdateColumnInstl(0, DEFAULT_INSTL_COLUMN_COUNT, True)
            End Sub

            ''' <summary>
            ''' INSTL品番列を初期設定する
            ''' </summary>
            ''' <param name="columnCount">INSTL品番の初期列数</param>
            ''' <remarks></remarks>
            Public Sub InitializeColumnInstl(ByVal columnCount As Integer)
                Insert(0, columnCount)
            End Sub
            ''' <summary>
            ''' INSTL品番列を削除する
            ''' </summary>
            ''' <param name="baseColumnIndex">基点となるINSTL品番列index (0 start)</param>
            ''' <param name="removeCount">削除列数</param>
            ''' <remarks></remarks>
            Public Sub Remove(ByVal baseColumnIndex As Integer, ByVal removeCount As Integer)
                UpdateColumnInstl(baseColumnIndex, removeCount, True)
            End Sub
            ''' <summary>
            ''' INSTL品番列を挿入する
            ''' </summary>
            ''' <param name="baseColumnIndex">基点となるINSTL品番列index (0 start)</param>
            ''' <param name="columnCount">挿入列数</param>
            ''' <remarks></remarks>
            Public Sub Insert(ByVal baseColumnIndex As Integer, ByVal columnCount As Integer)
                UpdateColumnInstl(baseColumnIndex, columnCount, False)
            End Sub
            ''' <summary>
            ''' INSTL品番列を挿入する
            ''' </summary>
            ''' <param name="baseInstlColumnIndex">基点となるINSTL品番列index (0 start)</param>
            ''' <param name="columnCount">挿入列数</param>
            ''' <remarks></remarks>
            Public Sub UpdateColumnInstl(ByVal baseInstlColumnIndex As Integer, ByVal columnCount As Integer, ByVal isRemove As Boolean)

                Dim spreadStartColumn As Integer = INSTL_INFO_START_COLUMN + baseInstlColumnIndex
                If Me._instlColumnCount < baseInstlColumnIndex Then
                    Throw New ArgumentOutOfRangeException("baseInstlColumnIndex", baseInstlColumnIndex, Me._instlColumnCount & " より大きい値は、指定出来ません")
                End If
                Dim oldInstlColumnCount As Integer = Me._instlColumnCount
                If isRemove Then
                    For c As Integer = 0 To columnCount - 1
                        RemoveColumns(spreadStartColumn, 1)
                    Next
                Else
                    columnCount = Math.Min(oldInstlColumnCount + columnCount, MAX_COLUMN_COUNT) - oldInstlColumnCount
                    If columnCount = 0 Then
                        Return  ' INSTL列は1000列まで
                    End If
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
                        ' セル（パターン名）
                        With sheet.Cells(INSTL_PATTERN_ROW_INDEX, spreadColumn)
                            ' .CellTypeは行に設定済み
                            .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
                        End With
                    Next
                    sheet.Rows(INSTL_PATTERN_ROW_INDEX).Border = borderFactory.GetUnderLine
                End If

                Dim scaleColumnCount As Integer = IIf(isRemove, -columnCount, columnCount)

                ' ** 挿入した列と、その右の既存の列に設定 **
                For spreadColumn As Integer = INSTL_INFO_START_COLUMN + baseInstlColumnIndex To INSTL_INFO_START_COLUMN + oldInstlColumnCount + scaleColumnCount - 1
                    ' セル（"A"とか"AB"とか）
                    With sheet.Cells(INSTL_PATTERN_NO_ROW_INDEX, spreadColumn)
                        ' .CellTypeは行に設定済み
                        .Border = borderFactory.GetUnderLine()
                        .Value = EzUtil.ConvIndexToAlphabet(spreadColumn - INSTL_INFO_START_COLUMN)
                    End With
                Next

                With sheet.Cells(INSTL_PATTERN_MIDASHI_ROW_INDEX, INSTL_INFO_START_COLUMN)
                    ' .CellTypeは行に設定済み
                    .BackColor = System.Drawing.SystemColors.Control
                    .Border = borderFactory.GetTopWLine()
                    .ColumnSpan = oldInstlColumnCount + scaleColumnCount
                    .Value = "員数"
                End With

                Me._instlColumnCount += scaleColumnCount
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
#End Region

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

        Private Sub UpdatePatternName(ByVal columnIndex As Integer)

            Dim spreadColumn As Integer = ConvKoseiInstlToSpreadColumn(columnIndex)
            sheet.Cells(INSTL_PATTERN_ROW_INDEX, spreadColumn).Value = subject.PatternName(columnIndex)

        End Sub

        ''' <summary>
        ''' Spread表示を(再)表示する
        ''' </summary>
        ''' <param name="observable">表示指示元のObservable</param>
        ''' <param name="arg">引数</param>
        ''' <remarks></remarks>
        Public Sub Update(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update

            If arg Is Nothing Then
                ClearSheet(True)
                LockSheet(subject.IsViewerMode)
                Update(observable, TITLE_ROW_INDEX)

                AdjustSheetRowCount()

                For Each rowIndex As Integer In subject.GetInputRowIndexes()
                    Update(observable, rowIndex)
                Next

            ElseIf TypeOf arg Is BuhinEditKoseiSubject.NotifyInfo Then

                Dim info As BuhinEditKoseiSubject.NotifyInfo = DirectCast(arg, BuhinEditKoseiSubject.NotifyInfo)
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
                    'For Each columnIndex As Integer In subject.GetInsuColumnIndexes
                    For columnIndex As Integer = 0 To subject.GetInputInstlPatternColumnCount - 1
                        UpdatePatternName(columnIndex)
                    Next
                    sheet.Rows(INSTL_PATTERN_ROW_INDEX).Locked = False
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
                    If StringUtil.IsEmpty(subject.YosanLevel(rowIndex)) Then
                        sheet.Cells(row, sheet.Columns(TAG_LEVEL).Index).Value = ""
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_LEVEL).Index).Value = subject.YosanLevel(rowIndex)
                    End If
                    sheet.Cells(row, sheet.Columns(TAG_BLOCK_NO).Index).Value = subject.YosanBlockNo(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_BUKA_CODE).Index).Value = subject.YosanBukaCode(rowIndex)

                    sheet.Cells(row, sheet.Columns(TAG_SHUKEI_CODE).Index).Value = subject.YosanShukeiCode(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value = subject.YosanSiaShukeiCode(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MAKER_CODE).Index).Value = subject.YosanMakerCode(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_MAKER_NAME).Index).Value = subject.YosanMakerName(rowIndex)

                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO).Index).Value = subject.YosanBuhinNo(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NO_KBN).Index).Value = subject.YosanBuhinNoKbn(rowIndex)
                    sheet.Cells(row, sheet.Columns(TAG_BUHIN_NAME).Index).Value = subject.YosanBuhinName(rowIndex)

                    sheet.Cells(row, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value = subject.YosanKyoukuSection(rowIndex)

                    If subject.YosanHenkoGaiyo(rowIndex) Is Nothing Then
                        sheet.Cells(row, sheet.Columns(TAG_HENKO_GAIYO).Index).Value = subject.YosanHenkoGaiyo(rowIndex)
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_HENKO_GAIYO).Index).Value = Trim(subject.YosanHenkoGaiyo(rowIndex))
                    End If

                    If StringUtil.IsEmpty(subject.YosanBuhinHiRyosan(rowIndex)) Then
                        sheet.Cells(row, sheet.Columns(TAG_BUHIN_HI_RYOSAN).Index).Value = ""
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_BUHIN_HI_RYOSAN).Index).Value = subject.YosanBuhinHiRyosan(rowIndex)
                    End If
                    If StringUtil.IsEmpty(subject.YosanBuhinHiBuhinhyo(rowIndex)) Then
                        sheet.Cells(row, sheet.Columns(TAG_BUHIN_HI_BUHINHYO).Index).Value = ""
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_BUHIN_HI_BUHINHYO).Index).Value = subject.YosanBuhinHiBuhinhyo(rowIndex)
                    End If
                    If StringUtil.IsEmpty(subject.YosanBuhinHiTokki(rowIndex)) Then
                        sheet.Cells(row, sheet.Columns(TAG_BUHIN_HI_TOKKI).Index).Value = ""
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_BUHIN_HI_TOKKI).Index).Value = subject.YosanBuhinHiTokki(rowIndex)
                    End If
                    If StringUtil.IsEmpty(subject.YosanKataHi(rowIndex)) Then
                        sheet.Cells(row, sheet.Columns(TAG_KATA_HI).Index).Value = ""
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_KATA_HI).Index).Value = subject.YosanKataHi(rowIndex)
                    End If
                    If StringUtil.IsEmpty(subject.YosanJiguHi(rowIndex)) Then
                        sheet.Cells(row, sheet.Columns(TAG_JIGU_HI).Index).Value = ""
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_JIGU_HI).Index).Value = subject.YosanJiguHi(rowIndex)
                    End If
                    If StringUtil.IsEmpty(subject.YosanKosu(rowIndex)) Then
                        sheet.Cells(row, sheet.Columns(TAG_KOSU).Index).Value = ""
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_KOSU).Index).Value = subject.YosanKosu(rowIndex)
                    End If
                    If StringUtil.IsEmpty(subject.YosanHachuJisekiMix(rowIndex)) Then
                        sheet.Cells(row, sheet.Columns(TAG_HACHU_JISEKI_MIX).Index).Value = ""
                    Else
                        sheet.Cells(row, sheet.Columns(TAG_HACHU_JISEKI_MIX).Index).Value = subject.YosanHachuJisekiMix(rowIndex)
                    End If

                    'For columnIndex As Integer = 1 To subject.GetInputInstlPatternColumnCount
                    '    sheet.Cells(row, ConvKoseiInstlToSpreadColumn(columnIndex)).Value = subject.InsuSuryo(rowIndex, columnIndex)
                    'Next
                End If
            End If
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
                sheet.Cells(i, sheet.Columns(TAG_LEVEL).Index).ForeColor = Color.Blue '青色に
                sheet.Cells(i, sheet.Columns(TAG_LEVEL).Index).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                sheet.Cells(i, sheet.Columns(TAG_BUHIN_NO).Index).ForeColor = Color.Blue '青色に
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

#Region "行（列）indexのエクスチェンジ"
        ''' <summary>
        ''' Spread列indexを、(Subject)INSTL列indexにして返す
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <returns>(Subject)INSTL列index</returns>
        ''' <remarks></remarks>
        Public Function ConvSpreadColumnToKoseiInstl(ByVal spreadColumn As Integer) As Integer
            Return spreadColumn - INSTL_INFO_START_COLUMN
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
        Private Function ConvKoseiInstlToSpreadColumn(ByVal columnIndexes As ICollection(Of Integer)) As Integer()
            Dim result As New List(Of Integer)
            For Each columnIndex As Integer In columnIndexes
                result.Add(ConvKoseiInstlToSpreadColumn(columnIndex))
            Next
            Return result.ToArray
        End Function

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
#End Region

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

#Region " Sheetのクリア"
        ''' <summary>
        ''' Sheetのデータをクリアする(INSTL品番の列名部と全体の明細部)
        ''' </summary>
        ''' <param name="dataOnly">データだけをクリアする場合、true</param>
        ''' <remarks></remarks>
        Public Sub ClearSheet(Optional ByVal dataOnly As Boolean = True)
            SpreadUtil.RemoveHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
            Try
                ' パターン欄の列名部分
                sheet.ClearRange(INSTL_PATTERN_ROW_INDEX, INSTL_INFO_START_COLUMN, INSTL_PATTERN_ROW_INDEX, INSTL_INFO_START_COLUMN + subject.GetInputInstlPatternColumnCount - 1, dataOnly)
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
            ' パターン欄の列名部分
            With sheet.Cells(INSTL_PATTERN_ROW_INDEX, INSTL_INFO_START_COLUMN, INSTL_PATTERN_ROW_INDEX, INSTL_INFO_START_COLUMN + subject.GetInputInstlPatternColumnCount - 1)
                .ResetBackColor()
            End With
            ' 全体の明細部
            With sheet.Cells(titleRows, 0, sheet.RowCount - 1, sheet.ColumnCount - 1)
                .ResetBackColor()
            End With
        End Sub

        ''' <summary>
        ''' Sheet全体の背景色を元に戻す
        '''　全て
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearSheetBackColorAll()
            '' パターン欄の列名部分
            With sheet.Cells(INSTL_PATTERN_ROW_INDEX, INSTL_INFO_START_COLUMN, INSTL_PATTERN_ROW_INDEX, INSTL_INFO_START_COLUMN + subject.GetInputInstlPatternColumnCount - 1)
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
#End Region

        '#Region "スプレッド上で入力されたパラメータを判定・変換"
        '        ''' <summary>
        '        ''' 入力されたパラメータをチェック
        '        ''' </summary>
        '        ''' Spread上で入力された値を変更する
        '        ''' <param name="ChangedCell">チェックしたいセル</param>
        '        ''' <remarks></remarks>
        '        Public Sub Change_EditInsuu(ByVal ChangedCell As Cell)

        '            If IsInstlSpreadColumn(ChangedCell.Column.Index) Then
        '                ChangedCell.Column.Tag = "INSU_TAG"
        '            End If

        '            '   予め、列位置を取得
        '            Dim ColumnTag As String = ChangedCell.Column.Tag.ToString

        '            '
        '            '   列位置のTAGによって、処理を分別
        '            '       他にも、入力チェックを行いたければ、ここに追加するのが良いかと思いますよ
        '            '
        '            Select Case ColumnTag
        '                Case TAG_BUHIN_NO
        '                    Dim subjectIndex As Integer = ConvSpreadRowToSubjectIndex(ChangedCell.Row.Index)
        '                    subject.OnChangedBuhinNo(subjectIndex)
        '                Case TAG_MAKER_CODE
        '                    Dim aTextValue As String = ChangedCell.Value
        '                    Dim newmakerName As String = TehaichoEditImpl.FindPkRhac0610(aTextValue).Trim
        '                    sheet.SetValue(ChangedCell.Row.Index, sheet.Columns(TAG_MAKER_NAME).Index, newmakerName)
        '                Case TAG_BUHIN_NAME

        '                    Dim s As String = ChangedCell.Value
        '                    Dim t As FarPoint.Win.Spread.CellType.TextCellType

        '                    If s Is Nothing Then
        '                        s = ""
        '                    End If

        '                    ' 対象となるセルのインスタンスを生成し、MaxLengthプロパティの値を参照します

        '                    t = CType(sheet.GetCellType(ChangedCell.Row.Index, ChangedCell.Column.Index), FarPoint.Win.Spread.CellType.TextCellType)

        '                    Do

        '                        Dim StringByteLength As Integer = System.Text.Encoding.GetEncoding(932).GetByteCount(s)
        '                        If StringByteLength > t.MaxLength Then
        '                            s = s.Substring(0, s.Length - 1)
        '                        Else
        '                            If ChangedCell.Value <> s Then
        '                                ChangedCell.Value = s
        '                            End If
        '                            Exit Do
        '                        End If

        '                    Loop

        '            End Select

        '        End Sub
        '#End Region

        ''' <summary>
        ''' 入力データ変更時の処理
        ''' </summary>
        ''' <param name="row">Spread行index</param>
        ''' <param name="column">spread列index</param>
        ''' <remarks></remarks>
        Private Sub OnChange(ByVal row As Integer, ByVal column As Integer)

            If column < 0 Then  ' 行選択時にコレ
                Return
            End If

            Dim rowIndex As Integer = ConvSpreadRowToSubjectIndex(row)

            If Not IsDataRow(row) Then
                Dim columnIndex As Integer = ConvSpreadColumnToKoseiInstl(column)
                If Not IsDataRow(row) AndAlso columnIndex >= 0 Then
                    subject.PatternName(columnIndex) = sheet.Cells(INSTL_PATTERN_ROW_INDEX, column).Value
                End If
                Return
            End If

            Dim value As Object = sheet.Cells(row, column).Value

            Select Case Convert.ToString(sheet.Columns(column).Tag)
                Case TAG_BUKA_CODE
                    subject.YosanBukaCode(rowIndex) = value
                Case TAG_BLOCK_NO
                    subject.YosanBlockNo(rowIndex) = value
                Case TAG_LEVEL
                    subject.YosanLevel(rowIndex) = StringUtil.ToInteger(value)
                Case TAG_SHUKEI_CODE
                    subject.YosanShukeiCode(rowIndex) = value
                Case TAG_SIA_SHUKEI_CODE
                    subject.YosanSiaShukeiCode(rowIndex) = value
                Case TAG_MAKER_CODE
                    subject.YosanMakerCode(rowIndex) = value
                Case TAG_MAKER_NAME
                    subject.YosanMakerName(rowIndex) = value
                Case TAG_BUHIN_NO
                    subject.YosanBuhinNo(rowIndex) = value
                Case TAG_BUHIN_NO_KBN
                    subject.YosanBuhinNoKbn(rowIndex) = value
                Case TAG_BUHIN_NAME
                    subject.YosanBuhinName(rowIndex) = value
                Case TAG_KYOUKU_SECTION
                    subject.YosanKyoukuSection(rowIndex) = value
                Case TAG_HENKO_GAIYO
                    subject.YosanHenkoGaiyo(rowIndex) = value
                Case TAG_BUHIN_HI_RYOSAN
                    If StringUtil.IsNotEmpty(value) Then
                        subject.YosanBuhinHiRyosan(rowIndex) = CType(value, Decimal)
                    Else
                        subject.YosanBuhinHiRyosan(rowIndex) = StringUtil.ToInteger(value)
                    End If
                Case TAG_BUHIN_HI_BUHINHYO
                    If StringUtil.IsNotEmpty(value) Then
                        subject.YosanBuhinHiBuhinhyo(rowIndex) = CType(value, Decimal)
                    Else
                        subject.YosanBuhinHiBuhinhyo(rowIndex) = StringUtil.ToInteger(value)
                    End If
                Case TAG_BUHIN_HI_TOKKI
                    If StringUtil.IsNotEmpty(value) Then
                        subject.YosanBuhinHiTokki(rowIndex) = CType(value, Decimal)
                    Else
                        subject.YosanBuhinHiTokki(rowIndex) = StringUtil.ToInteger(value)
                    End If
                Case TAG_KATA_HI
                    If StringUtil.IsNotEmpty(value) Then
                        subject.YosanKataHi(rowIndex) = CType(value, Decimal)
                    Else
                        subject.YosanKataHi(rowIndex) = StringUtil.ToInteger(value)
                    End If
                Case TAG_JIGU_HI
                    If StringUtil.IsNotEmpty(value) Then
                        subject.YosanJiguHi(rowIndex) = CType(value, Decimal)
                    Else
                        subject.YosanJiguHi(rowIndex) = StringUtil.ToInteger(value)
                    End If
                Case TAG_KOSU
                    If StringUtil.IsNotEmpty(value) Then
                        subject.YosanKosu(rowIndex) = CType(value, Decimal)
                    Else
                        subject.YosanKosu(rowIndex) = StringUtil.ToInteger(value)
                    End If
                Case TAG_HACHU_JISEKI_MIX
                    If StringUtil.IsNotEmpty(value) Then
                        subject.YosanHachuJisekiMix(rowIndex) = CType(value, Decimal)
                    Else
                        subject.YosanHachuJisekiMix(rowIndex) = StringUtil.ToInteger(value)
                    End If
                Case Else
                    If Not IsInstlSpreadColumn(column) Then
                        Return
                    End If
                    Dim columnIndex As Integer = ConvSpreadColumnToKoseiInstl(column)

                    subject.InsuSuryo(rowIndex, columnIndex) = sheet.Cells(row, column).Value

                    'If IsInstlSpreadColumn(column) Then
                    '    Dim columnIndex As Integer = ConvSpreadColumnToKoseiInstl(column)
                    '    If row = INSTL_PATTERN_ROW_INDEX Then
                    '        subject.PatternName(columnIndex) = value
                    '    Else
                    '        subject.InsuSuryo(rowIndex, columnIndex) = StringUtil.ToString(value)
                    '    End If
                    'End If
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
            For index As Integer = spreadRows(0) To spreadRows(spreadRows.Length - 1)
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BUKA_CODE).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BLOCK_NO).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_LEVEL).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_SHUKEI_CODE).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_MAKER_CODE).Index).Value) Then
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
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BUHIN_NAME).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_HENKO_GAIYO).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BUHIN_HI_RYOSAN).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BUHIN_HI_BUHINHYO).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_BUHIN_HI_TOKKI).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_KATA_HI).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_JIGU_HI).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_KOSU).Index).Value) Then
                    Continue For
                End If
                If Not StringUtil.IsEmpty(sheet.Cells(index - removeCount, sheet.Columns(TAG_HACHU_JISEKI_MIX).Index).Value) Then
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

#Region "入力チェック"
        ''' <summary>
        ''' 登録処理における入力チェックに問題がない事を実証する
        ''' </summary>
        ''' <remarks>問題が有れば、IllegalInputException</remarks>
        Public Sub AssertValidateRegister()
            DeleteRowCheck()

            Dim spreadRows As Integer() = ConvSubjectIndexToSpreadRow(subject.GetInputRowIndexes())
            'Dim spreadColumns As Integer() = ConvKoseiInstlToSpreadColumn(subject.GetInsuColumnIndexes())
            Dim spreadColumns As Integer = subject.GetInputInstlPatternColumnCount()

            If spreadRows.Length = 0 Then
                ''2015/04/09 追加 E.Ubukata
                '' データ未入力時登録処理を行わない旨のメッセージを出したいため変更
                Throw New Exception("データ未入力です。")

                'Return
            End If

            ''2015/04/08 追加 E.Ubukata
            '' インストール品番の重複チェック
            Dim stCol As Integer = sheet.Columns(SpdKoseiObserver.TAG_KYOUKU_SECTION).Index + 1
            Dim edCol As Integer = sheet.Columns(SpdKoseiObserver.TAG_HENKO_GAIYO).Index - 1
            If stCol = edCol Then
                If StringUtil.IsEmpty(sheet.Cells(SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, stCol).Text) Then
                    Throw New IllegalInputException("名称が未入力のインストール品番が存在します。", ValidatorUtil.MakeErrorControls(spread, SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, stCol))
                End If
            End If
            For col1 As Integer = stCol To edCol - 1
                If StringUtil.IsEmpty(sheet.Cells(SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, col1).Text) Then
                    Throw New IllegalInputException("名称が未入力のインストール品番が存在します。", ValidatorUtil.MakeErrorControls(spread, SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, col1))
                End If
                For col2 As Integer = col1 + 1 To edCol
                    If sheet.Cells(SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, col1).Text = sheet.Cells(SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, col2).Text Then
                        Throw New IllegalInputException("同一名称のインストール品番が存在します。", ValidatorUtil.MakeErrorControls(spread, SpdKoseiObserver.INSTL_PATTERN_ROW_INDEX, col2))
                    End If
                Next

            Next


            For Each spreadRow As Integer In spreadRows
                Dim hasData As Boolean = False
                Dim buhinNo As String = sheet.GetValue(spreadRow, sheet.Columns(TAG_BUHIN_NO).Index)
                If StringUtil.IsEmpty(buhinNo) Then
                    Continue For
                End If
                For spreadColumn As Integer = 0 To spreadColumns - 1
                    '' subject.InsuSuryoは、文字を保持していないから、Sheetを直接参照する
                    If Not StringUtil.IsEmpty(sheet.Cells(spreadRow, spreadColumn).Value) Then
                        hasData = True
                        Exit For
                    End If
                Next

                If Not hasData Then
                    Throw New IllegalInputException("員数を入力して下さい。", ValidatorUtil.MakeErrorControls(spread, spreadRow, spreadColumns))
                End If
            Next

            AssertValidateSave(True)

            validatorRegister.AssertValidate(spreadRows)
        End Sub

        ''' <summary>
        ''' 登録処理における入力チェックに問題がない事を実証する（警告表示用）
        ''' </summary>
        ''' <remarks>問題が有れば、IllegalInputException</remarks>
        Public Sub AssertValidateRegisterWarning()
            validatorRegisterWarning.AssertValidate(ConvSubjectIndexToSpreadRow(subject.GetInputRowIndexes()))

        End Sub

        ''' <summary>
        ''' 保存処理における入力チェックに問題がない事を実証する
        ''' </summary>
        ''' <remarks>問題が有れば、IllegalInputException</remarks>
        Public Sub AssertValidateSave(Optional ByVal isCalledByRegister As Boolean = False)

            Const srcTag As String = "INSTL_CHECK!!"

            Dim insuRange As New SpreadValidator(spread)
            insuRange.AddTextCell(srcTag).Range(1, 99)    '入力範囲
            Dim insuAsterisc As New SpreadValidator(spread)
            insuAsterisc.AddTextCell(srcTag).InArray(BuhinEditInsu.GREASE_FORM_VALUE)

            Dim validator As BaseValidator = ValidatorUtil.NewValidatorByOr("員数を正しく入力して下さい。", insuRange, insuAsterisc)
            For Each columnIndex As Integer In subject.GetInsuColumnIndexes
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
#End Region

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

        ''' <summary>
        ''' 列挿入・列削除が可能かを返す
        ''' </summary>
        ''' <param name="selection">選択範囲</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Function CanInsertColumnRemoveColumn(ByVal selection As CellRange) As Boolean
            Return IsInstlSpreadColumn(selection.Column)
        End Function

        ''' <summary>
        ''' INSTL品番列のSpread列indexかを返す
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Function IsInstlSpreadColumn(ByVal spreadColumn As Integer) As Boolean
            'Return INSTL_INFO_START_COLUMN <= spreadColumn AndAlso spreadColumn < INSTL_INFO_START_COLUMN + anInstlColumns.InstlColumnCount
            Return INSTL_INFO_START_COLUMN <= spreadColumn AndAlso spreadColumn < INSTL_INFO_START_COLUMN + subject.GetInputInstlPatternColumnCount
        End Function

        ''' <summary>
        ''' 列を挿入する
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumns(ByVal spreadColumn As Integer, ByVal insertCount As Integer)
            If insertCount = 0 Then
                Return
            End If
            IsSuspend_SpreadChangeEvent = True
            Try
                If IsInstlSpreadColumn(spreadColumn) Then
                    Dim subjectColumn As Integer = ConvSpreadColumnToKoseiInstl(spreadColumn)
                    anInstlColumns.Insert(subjectColumn + 1, insertCount)
                    subject.InsertColumnInInstl(subjectColumn + 1, insertCount)
                Else
                    Throw New NotSupportedException("員数欄以外の列挿入は不可")
                End If
                subject.NotifyObservers()
            Finally
                IsSuspend_SpreadChangeEvent = False
            End Try
        End Sub

        ''' <summary>
        ''' 列を削除する
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumns(ByVal spreadColumn As Integer, ByVal removeCount As Integer)
            If removeCount = 0 Then
                Return
            End If
            IsSuspend_SpreadChangeEvent = True
            Try
                If IsInstlSpreadColumn(spreadColumn) Then
                    Dim subjectColumn As Integer = ConvSpreadColumnToKoseiInstl(spreadColumn)
                    anInstlColumns.Remove(subjectColumn, removeCount)
                    subject.RemoveColumnInInstl(subjectColumn, removeCount)
                Else
                    Throw New NotSupportedException("員数欄以外の列削除は不可")
                End If
                subject.NotifyObservers()
            Finally
                IsSuspend_SpreadChangeEvent = False
            End Try
        End Sub

#Region "Spreadのシートにタイトルをもつ場合のタイトル行数を返す"
        ''' <summary>
        '''Spreadのシートにタイトルをもつ場合のタイトル行数を返す
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <returns>タイトル行数</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTitleRowsIn(ByVal spreadSheet As FarPoint.Win.Spread.SheetView) As Integer
            If 1 < spreadSheet.StartingRowNumber Then
                Throw New InvalidOperationException("想定外の値です. sheet.StartingRowNumber=" & CStr(spreadSheet.StartingRowNumber))
            End If
            Return 1 - spreadSheet.StartingRowNumber
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

#Region "スプレッド編集行対応(保存対象ブロックNoとして記録・対象セルの色等書式設定"
        ''' <summary>
        ''' スプレッド編集行対応
        ''' 
        ''' ・保存対象ブロックNoとして記録
        ''' ・対象セルの色等書式設定
        ''' 
        ''' </summary>
        ''' <param name="isBase">表示スプレッドが基本の場合はTRUEをセットする</param>
        ''' <param name="aRow">行位置</param>
        ''' <param name="aCol">列位置</param>
        ''' <param name="aRowCount">行件数</param>
        ''' <param name="aColCount">列件数</param>
        ''' <remarks></remarks>
        Public Sub SetEditRowProc(ByVal isBase As Boolean, _
                                  ByVal aRow As Integer, _
                                  ByVal aCol As Integer, _
                                  ByVal frmDispYosanBuhinEdit20 As Frm41DispYosanBuhinEdit20, _
                                  Optional ByVal aRowCount As Integer = 1, _
                                  Optional ByVal aColCount As Integer = 1)

            Dim sheet As SheetView = Nothing
            Dim hidSheet As SheetView = Nothing

            If isBase = True Then
                sheet = frmDispYosanBuhinEdit20.spdParts_Sheet1
            End If

            'セル編集モード時にコピーした場合、以下を行う。
            If aRowCount = 0 Then
                aRowCount = 1
            End If

            '行コピー時に以下を行う'
            If aCol = -1 Then
                aCol = 0
            End If

            '編集されたセルは太文字・青文字にする
            sheet.Cells(aRow, aCol, aRow + aRowCount - 1, aCol + aColCount - 1).ForeColor = Color.Blue
            sheet.Cells(aRow, aCol, aRow + aRowCount - 1, aCol + aColCount - 1).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)

            '基本・号車共通列を書式設定
            If aCol <= GetTagIdx(sheet, SpdKoseiObserver.TAG_BUHIN_NO_KBN) Then
                Dim endCol As Integer = aCol + aColCount - 1
                '共通列を超える場合は共通列の最大位置にする
                If endCol > GetTagIdx(sheet, SpdKoseiObserver.TAG_BUHIN_NO_KBN) Then
                    endCol = GetTagIdx(sheet, SpdKoseiObserver.TAG_BUHIN_NO_KBN)
                End If

                '同期対象スプレッドの文字種も変更 
                If sheet.Rows.Count >= aRow + aRowCount - 1 Then
                    sheet.Cells(aRow, aCol, aRow + aRowCount - 1, endCol).ForeColor = Color.Blue
                    sheet.Cells(aRow, aCol, aRow + aRowCount - 1, endCol).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)
                End If
            End If

        End Sub
#End Region

        ''' <summary>
        ''' 抽出した部品情報をSPREADへセットする。
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub buhinListToSpread(ByVal buhinListVos As List(Of BuhinListVo), _
                                     ByVal hoyouEventCode As String, ByVal jikyu As String, _
                                     ByVal kaihatsuFugo As String, _
                                     ByVal isEbomRead As Boolean, _
                                     ByVal isNormal As Boolean, _
                                     ByVal patternListVos As List(Of TYosanBuhinEditPatternVo), _
                                     ByVal patternVos As List(Of TYosanBuhinEditPatternVo), _
                                     ByVal frmDispYosanBuhinEdit20 As Frm41DispYosanBuhinEdit20)

            Dim sheet As FarPoint.Win.Spread.SheetView = frmDispYosanBuhinEdit20.spdParts_Sheet1
            Dim startSpdRow As Integer = GetTitleRowsIn(sheet)

            Dim Shisakudate As ShisakuDate
            Dim buhinstruct As TehaichoEditBuhinStructure
            Dim newBuhinMatrix As New BuhinKoseiMatrix

            ''ここで取込前のバックアップを実行する。
            '' 取込前バックアップ
            'importBackup()

            '_frmDispYosanBuhinEdit20.Refresh()

            '削除した後の行数
            Dim rowCount = sheet.RowCount
            Dim aBlock As String = String.Empty
            Dim insertIndex As Integer = 0
            Dim rowNo As Integer = 0
            'ブロックに該当する部品情報を全て部品表編集画面にセット
            For i As Integer = 0 To buhinListVos.Count - 1
                Dim blockNo As String = buhinListVos(i).BlockNo

                If Not StringUtil.Equals(aBlock, blockNo) Then
                    For rowIndex As Integer = startSpdRow To sheet.RowCount - 1
                        If StringUtil.IsEmpty(sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_BLOCK_NO))) Then
                            Exit For
                        End If
                        If blockNo < sheet.GetText(rowIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_BLOCK_NO)) Then
                            insertIndex = rowIndex
                            Exit For
                        Else
                            ''2015/04/07 追加 E.Ubukata
                            insertIndex = rowIndex + 1

                            rowNo += 1
                        End If
                    Next
                End If

                aBlock = blockNo

                '初回のみ、以外かチェックし、初回の場合先頭行をセットする。
                If insertIndex = 0 And rowNo = 0 Then
                    insertIndex = 4
                ElseIf insertIndex = 0 And rowNo <> 0 Then
                    '   下記はどちらがいいのか？？
                    sheet.AddRows(insertIndex, 1)
                    subject.InsertRow(insertIndex - startSpdRow, 1)
                    'koseiObserver.InsertRows(insertIndex, 1)
                    rowCount = rowCount + 1
                Else
                    '   下記はどちらがいいのか？？
                    sheet.AddRows(insertIndex, 1)
                    subject.InsertRow(insertIndex - startSpdRow, 1)
                    'koseiObserver.InsertRows(insertIndex, 1)
                    rowCount = rowCount + 1
                End If

                SetEditRowProc(True, insertIndex, 0, frmDispYosanBuhinEdit20, 1, sheet.ColumnCount)

                '設計課
                If StringUtil.IsNotEmpty(buhinListVos(i).BukaCode) Then
                    sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_BUKA_CODE).Index, buhinListVos(i).BukaCode)
                End If
                'ブロック№
                If StringUtil.IsNotEmpty(blockNo) Then
                    sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_BLOCK_NO).Index, blockNo)
                End If
                'レベル
                If StringUtil.IsNotEmpty(buhinListVos(i).Level) Then
                    sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_LEVEL).Index, buhinListVos(i).Level)
                End If
                '国内集計
                If StringUtil.IsNotEmpty(buhinListVos(i).ShukeiCode) Then
                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_SHUKEI_CODE), buhinListVos(i).ShukeiCode)
                End If
                '海外集計
                If StringUtil.IsNotEmpty(buhinListVos(i).SiaShukeiCode) Then
                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_SIA_SHUKEI_CODE), buhinListVos(i).SiaShukeiCode)
                End If

                '取引先コード
                If StringUtil.IsNotEmpty(buhinListVos(i).TorihikisakiCode) Then
                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_MAKER_CODE), buhinListVos(i).TorihikisakiCode)
                End If
                '取引先名称
                If StringUtil.IsNotEmpty(buhinListVos(i).TorihikisakiName) Then
                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_MAKER_NAME), buhinListVos(i).TorihikisakiName)
                End If
                '部品番号
                If StringUtil.IsNotEmpty(buhinListVos(i).BuhinNo) Then
                    sheet.SetText(insertIndex, sheet.Columns(SpdKoseiObserver.TAG_BUHIN_NO).Index, buhinListVos(i).BuhinNo)
                End If
                '部品番号試作区分
                '部品名称
                If StringUtil.IsNotEmpty(buhinListVos(i).BuhinName) Then
                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_BUHIN_NAME), buhinListVos(i).BuhinName)
                End If
                '供給セクション
                If StringUtil.IsNotEmpty(buhinListVos(i).Kyokyu) Then
                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_KYOUKU_SECTION), buhinListVos(i).Kyokyu)
                End If
                ' 員数
                Dim intCnt As Integer = 0
                If patternVos.Count <> 0 Then
                    For patternRowindex As Integer = 0 To patternVos.Count - 1
                        sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_KYOUKU_SECTION) + 1 + patternRowindex, "")
                        Dim patternHyoujiJun As Integer = intCnt
                        For voCnt As Integer = 0 To patternListVos.Count - 1
                            If StringUtil.Equals(patternHyoujiJun, patternListVos(voCnt).PatternHyoujiJun) Then
                                If StringUtil.IsNotEmpty(buhinListVos(i).Insu) Then
                                    sheet.SetText(insertIndex, GetTagIdx(sheet, SpdKoseiObserver.TAG_KYOUKU_SECTION) + 1 + patternRowindex, buhinListVos(i).Insu)
                                End If
                            End If
                        Next
                        intCnt = intCnt + 1
                    Next
                End If


                '---　ここで手配システムの動作と、EBOM取得からの動作は切り分ける
                'EBOMからのデータ取得時のみこの動作を行う
                If isEbomRead And isNormal Then

                    '   部品のLevelが０ならば、マトリックスを作る
                    If buhinListVos(i).Level = 0 Then

                        Shisakudate = New ShisakuDate

                        buhinstruct = New TehaichoEditBuhinStructure(hoyouEventCode, "ListCodeDummy", _
                                                                     "BlockNoDummy", "BukaCodeDummy", Shisakudate)
                        newBuhinMatrix = buhinstruct.GetKouseiMatrix(buhinListVos(i).BuhinNo.ToString.TrimStart, "", 0, kaihatsuFugo)

                    End If

                End If
                '---　ここで手配システムの動作と、EBOM取得からの動作は切り分ける

                'EBOMからのデータ取得時のみこの動作を行う
                If isEbomRead And isNormal Then

                    '自分が0レベルでひとつ前のレベルも0の場合、0レベル以下全て選択されたものとみなしINSTLの構成を取得する。
                    Dim CurrentLevel As Integer = buhinListVos(i).Level
                    '親レベルの部課コード、ブロックを取得する。
                    Dim CurrentBukaCode As String = buhinListVos(i).BukaCode
                    Dim CurrentBlockNo As String = buhinListVos(i).BlockNo

                    Dim NextLevel As Integer = 0
                    If i < buhinListVos.Count - 1 Then
                        NextLevel = buhinListVos(i + 1).Level
                    End If

                    If CurrentLevel = 0 And NextLevel = 0 Then
                        Console.WriteLine()
                        '部品構成をSPREADへ表示する。
                        If newBuhinMatrix IsNot Nothing AndAlso newBuhinMatrix.Records.Count > 0 Then
                            For Each k As Integer In newBuhinMatrix.GetInputRowIndexes

                                '自分は除く
                                If Not StringUtil.Equals(newBuhinMatrix(k).YosanLevel, 0) And _
                                    StringUtil.IsNotEmpty(newBuhinMatrix(k).YosanLevel) Then

                                    '自給品有無＝無の場合　国内または国外集計コードＪは読み飛ばす。
                                    If jikyu = "1" Or _
                                        jikyu = "0" And newBuhinMatrix(k).YosanShukeiCode.TrimEnd <> "" _
                                                    And newBuhinMatrix(k).YosanShukeiCode.TrimEnd <> "J" Or _
                                        jikyu = "0" And newBuhinMatrix(k).YosanShukeiCode.TrimEnd = "" _
                                                    And newBuhinMatrix(k).YosanSiaShukeiCode.TrimEnd <> "" _
                                                    And newBuhinMatrix(k).YosanSiaShukeiCode.TrimEnd <> "J" Then

                                        'カウント
                                        insertIndex = insertIndex + 1

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
                                        'If rowindex + j + titleRows >= sheet.RowCount Then
                                        '    sheet.RowCount = rowindex + j + titleRows + 1
                                        'End If
                                        If rowCount >= sheet.RowCount Then
                                            sheet.AddRows(insertIndex, 1)
                                            subject.InsertRow(insertIndex - startSpdRow, 1)
                                            rowCount = rowCount + 1
                                            SetEditRowProc(True, insertIndex, 0, frmDispYosanBuhinEdit20, 1, sheet.ColumnCount)
                                        End If

                                        'セルに設定
                                        '   親と同じ部課、ブロックをセット
                                        sheet.Cells(insertIndex, sheet.Columns(TAG_BUKA_CODE).Index).Value = CurrentBukaCode
                                        sheet.Cells(insertIndex, sheet.Columns(TAG_BLOCK_NO).Index).Value = CurrentBlockNo
                                        '
                                        If StringUtil.IsEmpty(newBuhinMatrix(k).YosanLevel) Then
                                            sheet.Cells(insertIndex, sheet.Columns(TAG_LEVEL).Index).Value = ""
                                        Else
                                            sheet.Cells(insertIndex, sheet.Columns(TAG_LEVEL).Index).Value = newBuhinMatrix(k).YosanLevel
                                        End If
                                        'セルに設定
                                        If StringUtil.IsEmpty(newBuhinMatrix(k).YosanLevel) Then
                                            sheet.Cells(insertIndex, sheet.Columns(TAG_LEVEL).Index).Value = ""
                                        Else
                                            sheet.Cells(insertIndex, sheet.Columns(TAG_LEVEL).Index).Value = newBuhinMatrix(k).YosanLevel
                                        End If
                                        sheet.Cells(insertIndex, sheet.Columns(TAG_SHUKEI_CODE).Index).Value = newBuhinMatrix(k).YosanShukeiCode
                                        sheet.Cells(insertIndex, sheet.Columns(TAG_SIA_SHUKEI_CODE).Index).Value = newBuhinMatrix(k).YosanSiaShukeiCode
                                        sheet.Cells(insertIndex, sheet.Columns(TAG_BUHIN_NO).Index).Value = newBuhinMatrix(k).YosanBuhinNo
                                        sheet.Cells(insertIndex, sheet.Columns(TAG_BUHIN_NO_KBN).Index).Value = newBuhinMatrix(k).YosanBuhinNoKbn
                                        sheet.Cells(insertIndex, sheet.Columns(TAG_BUHIN_NAME).Index).Value = newBuhinMatrix(k).YosanBuhinName
                                        sheet.Cells(insertIndex, sheet.Columns(TAG_KYOUKU_SECTION).Index).Value = newBuhinMatrix(k).YosanKyoukuSection

                                        ''2015/05/07 追加 E.Ubukata
                                        sheet.Cells(insertIndex, sheet.Columns(TAG_MAKER_CODE).Index).Value = newBuhinMatrix(k).YosanMakerCode
                                        sheet.Cells(insertIndex, sheet.Columns(TAG_MAKER_NAME).Index).Value = newBuhinMatrix(k).YosanMakerName

                                        ' 員数
                                        If patternVos.Count <> 0 Then
                                            For patternRowindex As Integer = 0 To patternVos.Count - 1
                                                sheet.Cells(insertIndex, _
                                                            sheet.Columns(TAG_KYOUKU_SECTION).Index + 1 + patternRowindex).Value = ""
                                                Dim patternHyoujiJun As Integer = patternVos(patternRowindex).PatternHyoujiJun
                                                For voCnt As Integer = 0 To patternListVos.Count - 1
                                                    If StringUtil.Equals(patternHyoujiJun, patternListVos(voCnt).PatternHyoujiJun) Then
                                                        If StringUtil.IsNotEmpty(newBuhinMatrix(k).YosanInsu) Then
                                                            sheet.Cells(insertIndex, _
                                                                        sheet.Columns(TAG_KYOUKU_SECTION).Index + 1 + patternRowindex).Value _
                                                                        = newBuhinMatrix(k).YosanInsu
                                                            '文字色も変更する。
                                                            sheet.Cells(insertIndex, sheet.Columns(TAG_KYOUKU_SECTION).Index + 1 + patternRowindex).ForeColor = Color.Blue   '文字色を変更
                                                            sheet.Cells(insertIndex, sheet.Columns(TAG_KYOUKU_SECTION).Index + 1 + patternRowindex).Font = New Font("MS UI Gothic", 9, FontStyle.Bold)  '太文字に
                                                        End If
                                                    End If
                                                Next
                                            Next
                                        End If

                                    End If

                                End If

                            Next

                        End If

                    End If
                End If
                '---　ここで手配システムの動作と、EBOM取得からの動作は切り分ける



                insertIndex = insertIndex + 1
                subject.NotifyObservers()
            Next
        End Sub

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
                If sheet.Cells(ALL_MIDASHI_ROW_INDEX, i, INSTL_PATTERN_ROW_INDEX, i).ForeColor = Color.Blue Then
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
                sheet.Cells(ALL_MIDASHI_ROW_INDEX, sIndex, INSTL_PATTERN_ROW_INDEX, eIndex).ForeColor = Nothing
            Else
                '基本情報列タイトル色戻し
                For i As Integer = sIndex To eIndex
                    Dim objFont As System.Drawing.Font = sheet.Cells(INSTL_PATTERN_ROW_INDEX, i).Font
                    '太字にされているセルは編集済なので色は戻さない
                    If Not objFont Is Nothing AndAlso objFont.Bold = True Then
                        Continue For
                    End If
                    '色を戻す
                    sheet.Cells(ALL_MIDASHI_ROW_INDEX, i, INSTL_PATTERN_ROW_INDEX, i).ForeColor = Nothing
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
                Dim objFont As System.Drawing.Font = sheet.Cells(INSTL_PATTERN_ROW_INDEX, i).Font

                '太字にされているセルは編集済なので色は戻さない
                If Not objFont Is Nothing AndAlso objFont.Bold = True Then
                    Continue For
                End If

                '色を戻す
                sheet.Cells(ALL_MIDASHI_ROW_INDEX, i, INSTL_PATTERN_ROW_INDEX, i).ForeColor = Nothing

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
            sheet.Cells(ALL_MIDASHI_ROW_INDEX, colNo, INSTL_PATTERN_ROW_INDEX, colNo).ForeColor = Color.Blue

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

    End Class

End Namespace