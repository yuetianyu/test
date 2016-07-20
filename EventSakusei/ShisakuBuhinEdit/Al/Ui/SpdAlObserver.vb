Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports FarPoint.Win.Spread.Model
Imports ShisakuCommon.Ui.Access
Imports FarPoint.Win.Spread.CellType
Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports FarPoint.Win.Spread
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Ui
Imports EventSakusei.ShisakuBuhinEdit.Ui
Imports ShisakuCommon.Ui.Spd
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util.OptionFilter
Imports System.Text
Imports EventSakusei.EventEdit.Dao
Imports EventSakusei.ExportShisakuEventInfoExcel.Dao
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl


Namespace ShisakuBuhinEdit.Al.Ui

    ''' <summary>
    ''' A/L表示のSpreadに関するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpdAlObserver : Implements Observer

#Region "各列のTAG"
        ''↓↓2014/08/14 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 CHG BEGIN
        Public Const TAG_CHECK_FLAG As String = "CHECK_FLAG"
        'Private Const TAG_GOSHA As String = "GOSHA"
        Public Const TAG_GOSHA As String = "GOSHA"
        ''↑↑2014/08/14 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 CHG END
#End Region
        Private BASE_COLOR As System.Drawing.Color = Color.DarkGray
        Private BASE_ForeCOLOR As System.Drawing.Color = Color.Black
        Private Const TITLE_ROW_INDEX As Integer = -1
        Private Const DYNAMIC_INFO_START_COLUMN As Integer = 2
        Private Const EVENT_TITLE_MIDASHI_ROW As Integer = 0
        Private Const EVENT_TITLE_ROW As Integer = 1
        Private Const OPTION_TITLE_MIDASHI_ROW As Integer = 0
        Private Const OPTION_TITLE_ROW As Integer = 1
        Private Const ALL_MIDASHI_ROW_INDEX As Integer = 0
        Private Const MEMO_MIDASHI_ROW_INDEX As Integer = 0
        Private Const MEMO_ROW_INDEX As Integer = 3 '2⇒行位置変更
        Private Const INSTL_HINBAN_MIDASHI_ROW_INDEX As Integer = 0
        Private Const INSTL_HINBAN_NO_ROW_INDEX As Integer = 1
        Public Const INSTL_DATA_KBN_ROW_INDEX As Integer = 2
        Public Const INSTL_HINBAN_ROW_INDEX As Integer = 3
        Public Const INSTL_HINBAN_KBN_ROW_INDEX As Integer = 4
        Public Const CHECK_FLG_COLUMN As Integer = 0
        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        Private ReadOnly subject As BuhinEditAlSubject
        Public ReadOnly titleRows As Integer
        Private validatorRegister As SpreadValidator
        Private validatorSave As SpreadValidator
        Private borderFactory As New SpreadBorderFactory

        Private toolTip As SpreadToolTip

        Private ReadOnly aMemoColumns As MemoColumns
        Private ReadOnly anInstlColumns As InstlColumns

        Private IsSuspend_SpreadChangeEvent As Boolean

        '直前の員数を保持する配列'
        Private InsuArray()() As Integer

        Private instlShowFlag As Boolean

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="spread">スプレッド</param>
        ''' <param name="subject">部品AL情報</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal spread As FpSpread, ByVal subject As BuhinEditAlSubject, ByVal instlShowFlag As Boolean)
            Me.spread = spread
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)
            Me.sheet.SheetName = "AL"
            Me.subject = subject

            Me.titleRows = BuhinEditSpreadUtil.GetTitleRows(sheet)

            Me.toolTip = New SpreadToolTip(spread)
            Me.aMemoColumns = New MemoColumns(sheet, borderFactory, AddressOf GetStartMemoSpreadColumn)
            Me.anInstlColumns = New InstlColumns(sheet, borderFactory, AddressOf GetStartInstlSpreadColumn)

            InsuArray = New Integer(subject.GetInputRowIndexes.Count + 100)() {}

            Me.instlShowFlag = instlShowFlag
            If instlShowFlag Then
                subject.AddObserver(Me)
            Else
                subject.DeleteObservers()
                subject.AddObserver(Me)
            End If
        End Sub

        ''' <summary>
        ''' イベント情報タイトルセルの設定を行う
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SettingEventTitleCell(ByVal aCell As Cell, Optional ByVal IsMostRight As Boolean = False)
            aCell.Border = IIf(IsMostRight, borderFactory.GetUnderLineAndRightWLine(), borderFactory.GetUnderLine())
            aCell.Locked = True
            aCell.BackColor = System.Drawing.SystemColors.Control
            aCell.RowSpan = titleRows - EVENT_TITLE_ROW
            aCell.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Bottom
            aCell.CellType = GetEventTitleCellType()
        End Sub

        ''' <summary>
        ''' 装備仕様内容の見出しセルの設定を行う
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SettingOptionTitleMidashiCell(ByVal aCell As Cell, ByVal value As String, ByVal ColumnSpan As Integer, ByVal isMostRight As Boolean)
            aCell.Value = value
            If isMostRight Then
                aCell.Border = borderFactory.GetUnderLineAndRightWLine()
            Else
                aCell.Border = borderFactory.GetUnderLineAndRightLine()
            End If
            aCell.Locked = True
            aCell.RowSpan = 1
            aCell.ColumnSpan = ColumnSpan
            aCell.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        End Sub

        ''' <summary>
        ''' 装備仕様内容のセルの設定を行う
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SettingOptionTitleCell(ByVal aCell As Cell, Optional ByVal IsMostRight As Boolean = False)
            aCell.Border = IIf(IsMostRight, borderFactory.GetUnderLineAndRightWLine(), borderFactory.GetUnderLine())
            aCell.Locked = True
            aCell.BackColor = System.Drawing.SystemColors.Control
            aCell.RowSpan = titleRows - 1
            aCell.CellType = GetSoubiTitleCellType()
        End Sub

        ''' <summary>
        ''' イベント情報タイトルの見出しセルの設定を行う
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SettingEventTitleMidashiCell(ByVal aCell As Cell, ByVal value As String, ByVal ColumnSpan As Integer, ByVal isMostRight As Boolean)
            aCell.Value = value
            If isMostRight Then
                aCell.Border = borderFactory.GetRightWLine
            End If
            aCell.Locked = True
            aCell.RowSpan = 1
            aCell.ColumnSpan = ColumnSpan
            aCell.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        End Sub
#Region "各列のCellType"

        Private eventTitleCellType As TextCellType
        Private SoubiTitleCellType As TextCellType
        Private MemoCellType As TextCellType
        Private MemoTitleCellType As TextCellType
        Private InstlHinbanMidashiCellType As TextCellType
        Private InstlHinbanNoCellType As TextCellType
        Private InstlHinbanCellType As TextCellType
        Private InstlKbnCellType As TextCellType
        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥) (TES)張 ADD BEGIN
        Private InstlDataKbnCellType As TextCellType
        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥) (TES)張 ADD END
        Private InstlInsuCellType As TextCellType

        ''' <summary>
        ''' イベント情報タイトルセルを返す
        ''' </summary>
        ''' <returns>イベント情報タイトルセル</returns>
        ''' <remarks></remarks>
        Public Function GetEventTitleCellType() As TextCellType
            If eventTitleCellType Is Nothing Then
                eventTitleCellType = BuhinEditSpreadUtil.NewGeneralTextCellType()
                eventTitleCellType.MaxLength = 100
            End If
            Return eventTitleCellType
        End Function
        ''' <summary>
        ''' 装備仕様内容タイトルセルを返す
        ''' </summary>
        ''' <returns>装備仕様内容タイトルセル</returns>
        ''' <remarks></remarks>
        Public Function GetSoubiTitleCellType() As TextCellType
            If SoubiTitleCellType Is Nothing Then
                SoubiTitleCellType = BuhinEditSpreadUtil.NewGeneralTextCellType()
                SoubiTitleCellType.MaxLength = 122
                SoubiTitleCellType.TextOrientation = FarPoint.Win.TextOrientation.TextTopDown
            End If
            Return SoubiTitleCellType
        End Function
        ''' <summary>
        ''' メモ欄セルを返す
        ''' </summary>
        ''' <returns>メモ欄セル</returns>
        ''' <remarks></remarks>
        Public Function GetMemoCellType() As TextCellType
            If MemoCellType Is Nothing Then
                MemoCellType = BuhinEditSpreadUtil.NewGeneralTextCellType()
                MemoCellType.MaxLength = 2
            End If
            Return MemoCellType
        End Function
        ''' <summary>
        ''' メモ欄タイトルセルを返す
        ''' </summary>
        ''' <returns>メモ欄タイトルセル</returns>
        ''' <remarks></remarks>
        Public Function GetMemoTitleCellType() As TextCellType
            If MemoTitleCellType Is Nothing Then
                MemoTitleCellType = BuhinEditSpreadUtil.NewGeneralTextCellType()
                MemoTitleCellType.MaxLength = 122
                MemoTitleCellType.TextOrientation = FarPoint.Win.TextOrientation.TextTopDown
                MemoTitleCellType.CharacterSet = CharacterSet.AllIME
            End If
            Return MemoTitleCellType
        End Function
        ''' <summary>
        ''' INSTL品番見出しセルを返す
        ''' </summary>
        ''' <returns>INSTL品番見出しセル</returns>
        ''' <remarks></remarks>
        Public Function GetInstlHinbanMidashiCellType() As TextCellType
            If InstlHinbanMidashiCellType Is Nothing Then
                InstlHinbanMidashiCellType = BuhinEditSpreadUtil.NewGeneralTextCellType()
                InstlHinbanMidashiCellType.MaxLength = 100
                InstlHinbanMidashiCellType.CharacterSet = CharacterSet.Ascii
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
                InstlHinbanNoCellType = BuhinEditSpreadUtil.NewGeneralTextCellType()
                'ここの長さはメモにも適用されてる'
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
                InstlHinbanCellType = BuhinEditSpreadUtil.NewGeneralTextCellType()
                InstlHinbanCellType.MaxLength = 13
                'InstlHinbanCellType.MaxLength = 122
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
                InstlKbnCellType = BuhinEditSpreadUtil.NewGeneralTextCellType()
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
                InstlDataKbnCellType = BuhinEditSpreadUtil.NewGeneralTextCellType()
                'InstlDataKbnCellType.MaxLength = 1
                ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(3)) (TES)張 CHG BEGIN
                InstlDataKbnCellType.MaxLength = 2
                ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(3)) (TES)張 CHG END
            End If
            Return InstlDataKbnCellType
        End Function

#End Region

        ''' <summary>
        ''' 初期化する（一度だけ実行する事を想定）
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Initialize()

            ' デモ用の列を除去
            Dim demoDynamicColumnCount As Integer = 7
            RemoveColumnsInternal(DYNAMIC_INFO_START_COLUMN, demoDynamicColumnCount)
            Dim demoMemoColumnCount As Integer = 6
            RemoveColumnsInternal(DYNAMIC_INFO_START_COLUMN, demoMemoColumnCount)

            Dim demoInstlColumnCount As Integer = 26
            sheet.ColumnCount = sheet.ColumnCount - demoInstlColumnCount

            BuhinEditSpreadUtil.InitializeFrm41(spread)

            Dim index As Integer = 0

            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_CHECK_FLAG
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GOSHA
            sheet.Columns(TAG_GOSHA).Locked = True
            sheet.FrozenColumnCount = 2


            sheet.Rows(ALL_MIDASHI_ROW_INDEX).BackColor = System.Drawing.SystemColors.Control
            sheet.Rows(INSTL_HINBAN_NO_ROW_INDEX).BackColor = Color.White
            sheet.Rows(INSTL_HINBAN_ROW_INDEX).BackColor = Color.White
            sheet.Rows(INSTL_HINBAN_KBN_ROW_INDEX).BackColor = Color.White
            sheet.Rows(INSTL_DATA_KBN_ROW_INDEX).BackColor = System.Drawing.SystemColors.Control
            '諸悪の根源'
            '行の情報がいると邪魔'
            sheet.Rows(INSTL_HINBAN_MIDASHI_ROW_INDEX).CellType = GetInstlHinbanMidashiCellType()
            sheet.Rows(INSTL_HINBAN_NO_ROW_INDEX).CellType = GetInstlHinbanNoCellType()
            sheet.Rows(INSTL_HINBAN_ROW_INDEX).CellType = GetInstlHinbanCellType()
            sheet.Rows(INSTL_HINBAN_KBN_ROW_INDEX).CellType = GetInstlKbnCellType()
            sheet.Rows(INSTL_DATA_KBN_ROW_INDEX).CellType = GetInstlDataKbnCellType()




            sheet.Rows(ALL_MIDASHI_ROW_INDEX).Locked = True

            'IMEを使用可能にする。
            spread.ImeMode = Windows.Forms.ImeMode.NoControl

            ShowColumnIfChange()

            aMemoColumns.Initialize(subject.MemoColumnCount)

            ''デフォルト行で足りるか見る 樺澤'
            If subject.InstlHinbanCount() < 100 Then
                anInstlColumns.Initialize(100)
            Else
                'これだとぴったりになるのでとりあえず3列追加しておく'
                Dim overCount As Integer = subject.InstlHinbanCount + 3
                anInstlColumns.Initialize(overCount)
            End If

            'AddHandler spread.VisibleChanged, AddressOf Spread_VisibleChangedEventHandlable
            '' 通常の Spread_Changed()では、CTRL+V/CTRL+ZでChengedイベントが発生しない
            ''（編集モードではない状態で変更された場合は発生しない仕様とのこと。）
            '' CTRL+V/CTRL+Zでもイベントが発生するハンドラを設定する
            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
        End Sub

        ''' <summary>
        ''' 登録処理における入力チェックに問題がない事を実証する
        ''' </summary>
        ''' <remarks>問題が有れば、IllegalInputException</remarks>
        Public Sub AssertValidateRegister()

            AssertValidateSave()

        End Sub

        ''' <summary>
        ''' 登録処理におけるInst品番に色記号が入っていないかチェック
        ''' </summary>
        ''' <remarks>問題が有れば、IllegalInputException</remarks>
        Public Sub AssertValidateHinban()
            ''2015/08/17 追加 E.Ubukata Ver 2.10.8
            '' 色記号入力チェック追加
            Dim errorControls As New List(Of ErrorControl)
            For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                Dim key As String = subject.InstlHinban(columnIndex)
                If key.Substring(key.Length - 2, 2).Equals("##") Then
                    Dim spreadColumn As Integer = ConvAlInstlToSpreadColumn(columnIndex)
                    errorControls.AddRange(ValidatorUtil.MakeErrorControls(spread, INSTL_HINBAN_ROW_INDEX, spreadColumn))
                End If
            Next
            If 0 < errorControls.Count Then
                Throw New IllegalInputException("色記号を入力してください。", errorControls.ToArray)
            End If
        End Sub


        ''' <summary>
        ''' INSTL品番とINSTL品番区分とのエラー項目コントロールを作成する
        ''' </summary>
        ''' <param name="columnIndexes">列index</param>
        ''' <returns>エラー項目コントロール</returns>
        ''' <remarks></remarks>
        Private Function MakeErrorControlsInstlHinbanAndKbn(ByVal ParamArray columnIndexes As Integer()) As ErrorControl()

            Dim spreadRows As List(Of Integer) = New List(Of Integer)(New Integer() {INSTL_HINBAN_ROW_INDEX, INSTL_HINBAN_KBN_ROW_INDEX})
            Dim spreadColumns As List(Of Integer) = ConvAlInstlToSpreadColumn(New List(Of Integer)(columnIndexes))
            Return ValidatorUtil.MakeErrorControls(spread, spreadRows, spreadColumns)
        End Function

        ''' <summary>
        ''' Subjectの行indexを、Spreadの行indexにして返す
        ''' </summary>
        ''' <param name="rowIndexes">Subjectの行index</param>
        ''' <returns>Spreadの行index</returns>
        ''' <remarks></remarks>
        Private Function ConvRowIndexToSpreadRowIndex(ByVal rowIndexes As ICollection(Of Integer)) As Integer()
            Dim result As New List(Of Integer)
            For Each rowIndex As Integer In rowIndexes
                result.Add(rowIndex + titleRows)
            Next
            Return result.ToArray
        End Function

        ''' <summary>
        ''' 保存処理における入力チェックに問題がない事を実証する
        ''' </summary>
        ''' <remarks>問題が有れば、IllegalInputException</remarks>
        Public Sub AssertValidateSave()

            Const srcMemoTag As String = "MEMO_CHECK!!"

            Dim memoTitleValidator As New SpreadValidator(spread)
            memoTitleValidator.AddTextCell(srcMemoTag, "メモ").MaxLengthByte(122)

            For Each columnIndex As Integer In subject.GetInputMemoTitleColumnIndexes
                Dim spreadColumn As Integer = ConvAlMemoToSpreadColumn(columnIndex)
                sheet.Columns(spreadColumn).Tag = srcMemoTag
                Try
                    memoTitleValidator.AssertValidate(MEMO_ROW_INDEX)
                Finally
                    sheet.Columns(spreadColumn).Tag = Nothing
                End Try
            Next

            Dim memoTekiyouValidator As New SpreadValidator(spread)
            memoTekiyouValidator.AddTextCell(srcMemoTag, "適用").MaxLengthByte(2)

            For Each rowIndex As Integer In subject.GetInputRowIndexes
                Dim spreadRow As Integer = rowIndex + titleRows
                For Each columnIndex As Integer In subject.GetInputMemoTekiyouColumnIndexes(rowIndex)
                    Dim spreadColumn As Integer = ConvAlMemoToSpreadColumn(columnIndex)
                    sheet.Columns(spreadColumn).Tag = srcMemoTag
                    Try
                        memoTekiyouValidator.AssertValidate(spreadRow)
                    Finally
                        sheet.Columns(spreadColumn).Tag = Nothing
                    End Try
                Next
            Next


            Const srcTag As String = "INSTL_CHECK!!"

            Dim insuRange As New SpreadValidator(spread)
            insuRange.AddTextCell(srcTag).Range(1, 99)
            Dim insuAsterisc As New SpreadValidator(spread)
            insuAsterisc.AddTextCell(srcTag).InArray(BuhinEditInsu.GREASE_FORM_VALUE)

            Dim validator As BaseValidator = ValidatorUtil.NewValidatorByOr("員数を正しく入力して下さい。", insuRange, insuAsterisc)
            For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                Dim spreadColumn As Integer = ConvAlInstlToSpreadColumn(columnIndex)

                sheet.Columns(spreadColumn).Tag = srcTag
                Try
                    For Each rowNo As Integer In subject.GetInputRowIndexes
                        '2012/03/06 何故か色を付けると-の数値が沸いてくる'
                        If rowNo > -1 Then
                            Dim spreadRow As Integer = rowNo + titleRows
                            validator.AssertValidate(spreadRow)
                        End If
                    Next
                Finally
                    sheet.Columns(spreadColumn).Tag = Nothing
                End Try
            Next

            For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                If StringUtil.IsEmpty(subject.InstlHinban(columnIndex)) AndAlso Not StringUtil.IsEmpty(subject.InstlHinbanKbn(columnIndex)) Then
                    Dim spreadColumn As Integer = ConvAlInstlToSpreadColumn(columnIndex)
                    Throw New IllegalInputException("INSTL品番を入力して下さい。", ValidatorUtil.MakeErrorControls(spread, INSTL_HINBAN_ROW_INDEX, spreadColumn))
                End If
            Next

            '該当イベント取得
            Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
            Dim eventVo As TShisakuEventVo
            eventVo = eventDao.FindByPk(subject.shisakuEventCode)

            Dim existPair As New Dictionary(Of String, Integer)
            For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                Dim tmp As String
                If StringUtil.IsEmpty(subject.InstlDataKbn(columnIndex)) Then
                    tmp = "0"
                Else
                    tmp = subject.InstlDataKbn(columnIndex)
                End If

                '該当イベントが「移管車改修」の場合
                Dim key As String
                If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                    key = EzUtil.MakeKey(subject.InstlHinban(columnIndex), subject.InstlHinbanKbn(columnIndex), tmp, StringUtil.Evl(subject.BaseInstlFlg(columnIndex), "0"))
                Else
                    key = EzUtil.MakeKey(subject.InstlHinban(columnIndex), subject.InstlHinbanKbn(columnIndex), tmp)
                End If
                If existPair.ContainsKey(key) Then
                    Throw New IllegalInputException("INSTL品番が他の列にも入力されています。", MakeErrorControlsInstlHinbanAndKbn(existPair(key), columnIndex))
                End If
                existPair.Add(key, columnIndex)
            Next


        End Sub


        ''' <summary>
        ''' メモ欄を追加する
        ''' </summary>
        ''' <param name="columnCount">追加列数</param>
        ''' <remarks></remarks>
        Public Sub AddMemoColumn(ByVal columnCount As Integer)
            IsSuspend_SpreadChangeEvent = True
            Try
                aMemoColumns.Add(columnCount)



                subject.MemoColumnCount = aMemoColumns.Count
            Finally
                IsSuspend_SpreadChangeEvent = False
            End Try
        End Sub

        Private Delegate Function GetStartColumn() As Integer

        ''' <summary>
        ''' メモ列の挿入・初期化
        ''' </summary>
        ''' <remarks>
        ''' MemoColumnCountを隠蔽する<br/>
        ''' けれど「CellTypeは行に設定済み」とか、結構依存している. 上手に切り分けが出来ていない.
        ''' </remarks>
        Private Class MemoColumns
            Public Const MAX_COLUMN_COUNT As Integer = 20
            Private ReadOnly sheet As SheetView
            Private ReadOnly borderFactory As SpreadBorderFactory
            Private ReadOnly startColumnDelegate As GetStartColumn
            Private _count As Integer = 0

            Public ReadOnly Property Count() As Integer
                Get
                    Return _count
                End Get
            End Property

            Public Sub New(ByVal sheet As SheetView, ByVal borderFactory As SpreadBorderFactory, ByVal startColumnDelegate As GetStartColumn)
                Me.sheet = sheet
                Me.borderFactory = borderFactory
                Me.startColumnDelegate = startColumnDelegate
            End Sub

            ''' <summary>
            ''' 列を初期設定する
            ''' </summary>
            ''' <param name="columnCount">メモ列の初期列数</param>
            ''' <remarks></remarks>
            Public Sub Initialize(ByVal columnCount As Integer)
                Insert(0, columnCount)
            End Sub
            ''' <summary>
            ''' 列を追加する
            ''' </summary>
            ''' <param name="columnCount">追加列数</param>
            ''' <remarks></remarks>
            Public Sub Add(ByVal columnCount As Integer)
                Insert(_count, columnCount)
            End Sub
            ''' <summary>
            ''' 列を挿入する
            ''' </summary>
            ''' <param name="baseColumnIndex">基点となるメモ列index (0 start)</param>
            ''' <param name="columnCount">挿入列数</param>
            ''' <remarks></remarks>
            Public Sub Insert(ByVal baseColumnIndex As Integer, ByVal columnCount As Integer)
                UpdateColumn(baseColumnIndex, columnCount, False)
            End Sub
            ''' <summary>
            ''' 列を削除する
            ''' </summary>
            ''' <param name="baseColumnIndex">基点となるメモ列index (0 start)</param>
            ''' <param name="columnCount">削除列数</param>
            ''' <remarks></remarks>
            Public Sub Remove(ByVal baseColumnIndex As Integer, ByVal columnCount As Integer)
                UpdateColumn(baseColumnIndex, columnCount, True)
            End Sub
            ''' <summary>
            ''' 列の挿入・削除を行う
            ''' </summary>
            ''' <param name="baseColumnIndex">基点となるメモ列index (0 start)</param>
            ''' <param name="columnCount">挿入列数or削除列数</param>
            ''' <param name="isRemove">削除の場合、true</param>
            ''' <remarks></remarks>
            Private Sub UpdateColumn(ByVal baseColumnIndex As Integer, ByVal columnCount As Integer, ByVal isRemove As Boolean)

                Dim startColumnIndex As Integer = startColumnDelegate()
                Dim spreadStartColumn As Integer = startColumnIndex + baseColumnIndex
                If Me._count < baseColumnIndex Then
                    Throw New ArgumentOutOfRangeException("baseColumnIndex", baseColumnIndex, Me._count & " より大きい値は、指定出来ません")
                End If

                Dim oldMemoColumnCount As Integer = Me._count

                If isRemove Then
                    columnCount = Math.Min(oldMemoColumnCount, baseColumnIndex + columnCount) - baseColumnIndex
                    RemoveColumns(spreadStartColumn, columnCount)
                Else

                    columnCount = Math.Min(oldMemoColumnCount + columnCount, MAX_COLUMN_COUNT) - oldMemoColumnCount
                    If columnCount = 0 Then
                        Return  ' メモ列は20列まで
                    End If

                    If 0 < oldMemoColumnCount Then
                        ' 一番右の列の二重線をクリア
                        SettingMostRightColumnBorder(startColumnIndex + oldMemoColumnCount - 1, True)
                    End If
                    InsertColumns(spreadStartColumn, columnCount)

                    ' ** 挿入した列に新規設定 **
                    For columnIndex As Integer = 0 To columnCount - 1
                        Dim spreadColumn As Integer = spreadStartColumn + columnIndex
                        ' 列
                        With sheet.Columns(spreadColumn)
                            '.CellType = GetMemoColumnCellType()
                            .Width = 20.0!
                            '.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                        End With
                        ' セル（メモ長い方）
                        '初回は適用されているけど２回目以降ここの設定が反映されてない'
                        With sheet.Cells(MEMO_ROW_INDEX, spreadColumn)
                            .RowSpan = 2 '3⇒行位置変更
                            .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
                            .CellType = GetMemoTitleCellType()
                            ''↓↓2014/09/05 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD BEGIN
                            .BackColor = Color.White
                            .ForeColor = Color.Black
                            .Locked = False
                            ''↑↑2014/09/05 Ⅰ.3.設計編集 ベース車改修専用化 酒井 ADD END
                        End With

                        'メモ欄の適用を入力可能にする。　2011/03/03　Ｂｙ柳沼
                        '但し号車が入力されている行だけ。
                        For rowIndex As Integer = 5 To sheet.RowCount - 1
                            With sheet.Cells(rowIndex, spreadColumn)
                                .CellType = GetMemoColumnCellType()
                            End With

                            ''↓↓2014/08/27 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 CHG BEGIN
                            'If StringUtil.IsEmpty(sheet.Cells(rowIndex, 0).Value) Then
                            If StringUtil.IsEmpty(sheet.Cells(rowIndex, sheet.Columns(TAG_GOSHA).Index).Value) Then
                                ''↑↑2014/08/27 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 CHG END
                                sheet.Cells(rowIndex, spreadColumn).Locked = True
                            Else
                                sheet.Cells(rowIndex, spreadColumn).Locked = False
                            End If
                        Next

                    Next
                    '削除されても消えないように線を引く'
                    sheet.Rows(MEMO_ROW_INDEX).Border = borderFactory.GetUnderLine()

                End If

                Dim scaleColumnCount As Integer = IIf(isRemove, -columnCount, columnCount)

                ' 一番右の列だけ、右側に二重線
                SettingMostRightColumnBorder(startColumnIndex + oldMemoColumnCount + scaleColumnCount - 1, False)
                ' ** メモ欄のタイトル **
                'With sheet.Cells(MEMO_MIDASHI_ROW_INDEX, startColumnIndex)
                '    .RowSpan = 2
                '    ' .CellTypeは行に設定済み
                '    .Border = borderFactory.GetUnderLineAndRightWLine
                '    .ColumnSpan = oldMemoColumnCount + scaleColumnCount
                '    .HorizontalAlignment = CellHorizontalAlignment.Center
                '    .VerticalAlignment = CellVerticalAlignment.Center
                '    .Value = "メモ欄"
                'End With

                If oldMemoColumnCount + scaleColumnCount = 0 Then
                    With sheet.Cells(INSTL_HINBAN_MIDASHI_ROW_INDEX, startColumnIndex)
                        ' .CellTypeは行に設定済み
                        .Border = borderFactory.GetUnderLine()
                        .ColumnSpan = oldMemoColumnCount + scaleColumnCount
                        .Value = "INSTL品番"
                    End With
                Else
                    ' ** メモ欄のタイトル **
                    With sheet.Cells(MEMO_MIDASHI_ROW_INDEX, startColumnIndex)
                        .RowSpan = 3 '2⇒行位置変更
                        ' .CellTypeは行に設定済み
                        .Border = borderFactory.GetUnderLineAndRightWLine
                        .ColumnSpan = oldMemoColumnCount + scaleColumnCount
                        .HorizontalAlignment = CellHorizontalAlignment.Center
                        .VerticalAlignment = CellVerticalAlignment.Center
                        .Value = "メモ欄"
                    End With
                End If






                Me._count += scaleColumnCount
            End Sub

            Private Sub SettingMostRightColumnBorder(ByVal mostRightColumn As Integer, ByVal isClear As Boolean)
                If isClear Then
                    With sheet.Columns(mostRightColumn)
                        .Border = Nothing
                    End With
                    With sheet.Cells(MEMO_ROW_INDEX, mostRightColumn)
                        .Border = borderFactory.GetUnderLine
                    End With
                Else
                    With sheet.Columns(mostRightColumn)
                        .Border = borderFactory.GetRightWLine
                    End With
                    With sheet.Cells(MEMO_ROW_INDEX, mostRightColumn)
                        .Border = borderFactory.GetUnderLineAndRightWLine
                    End With
                End If
            End Sub

            Private Sub SettingMostLeftColumnBorder(ByVal mostLeftColumn As Integer, ByVal isClear As Boolean)
                If isClear Then
                    With sheet.Columns(mostLeftColumn)
                        .Border = Nothing
                    End With
                    With sheet.Cells(MEMO_ROW_INDEX, mostLeftColumn)
                        .Border = borderFactory.GetUnderLine
                    End With
                Else
                    With sheet.Columns(mostLeftColumn)
                        .Border = borderFactory.GetLeftWLine
                    End With
                    With sheet.Cells(MEMO_ROW_INDEX, mostLeftColumn)
                        .Border = borderFactory.GetUnderLineAndLeftWLine
                    End With
                End If
            End Sub

            Private MemoTitleCellType As TextCellType
            ''' <summary>
            ''' メモセルを返す
            ''' </summary>
            ''' <returns>メモセル</returns>
            ''' <remarks></remarks>
            Public Function GetMemoTitleCellType() As TextCellType
                '縦書きにならないのは何故？'
                'If MemoTitleCellType Is Nothing Then
                '    MemoTitleCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                '    MemoTitleCellType.MaxLength = 15
                '    MemoTitleCellType.TextOrientation = FarPoint.Win.TextOrientation.TextVertical
                '    MemoTitleCellType.CharacterSet = CharacterSet.AllIME
                'End If
                MemoTitleCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                MemoTitleCellType.MaxLength = 122
                MemoTitleCellType.TextOrientation = FarPoint.Win.TextOrientation.TextTopDown
                MemoTitleCellType.CharacterSet = CharacterSet.AllIME
                'MemoTitleCellType.CharacterSet = CharacterSet.Ascii
                Return MemoTitleCellType
            End Function

            Private MemoColumnCellType As TextCellType
            ''' <summary>
            ''' メモ列CellTypeを返す
            ''' </summary>
            ''' <returns>メモ列CellType</returns>
            ''' <remarks></remarks>
            Private Function GetMemoColumnCellType() As TextCellType
                'If MemoColumnCellType Is Nothing Then
                '    '樺澤'
                '    MemoColumnCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                '    MemoColumnCellType.TextOrientation = FarPoint.Win.TextOrientation.TextHorizontal
                '    MemoColumnCellType.MaxLength = 2
                '    MemoColumnCellType.CharacterSet = CharacterSet.AllIME
                'End If
                MemoColumnCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                MemoColumnCellType.TextOrientation = FarPoint.Win.TextOrientation.TextHorizontal
                MemoColumnCellType.MaxLength = 2
                'MemoColumnCellType.CharacterSet = CharacterSet.AllIME
                Return MemoColumnCellType
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

        ''' <summary>
        ''' INSTL品番列の挿入・初期化
        ''' </summary>
        ''' <remarks>
        ''' InstlColumnCountを隠蔽する<br/>
        ''' けれど「CellTypeは行に設定済み」とか、結構依存している. 上手に切り分けが出来ない. 困った.
        ''' </remarks>
        Private Class InstlColumns
            Public Const MAX_COLUMN_COUNT As Integer = 1000
            Private ReadOnly sheet As SheetView
            Private ReadOnly borderFactory As SpreadBorderFactory
            Private _count As Integer
            Private startColumnDelegate As GetStartColumn

            Public ReadOnly Property Count() As Integer
                Get
                    Return _count
                End Get
            End Property

            Public Sub New(ByVal sheet As SheetView, ByVal borderFactory As SpreadBorderFactory, ByVal startColumnDelegate As GetStartColumn)
                Me.sheet = sheet
                Me.borderFactory = borderFactory
                Me.startColumnDelegate = startColumnDelegate
            End Sub

            ''' <summary>
            ''' INSTL品番列を初期設定する
            ''' </summary>
            ''' <param name="columnCount">INSTL品番列の初期列数</param>
            ''' <remarks></remarks>
            Public Sub Initialize(ByVal columnCount As Integer)
                Insert(0, columnCount)
            End Sub
            ''' <summary>
            ''' INSTL品番列を削除する
            ''' </summary>
            ''' <param name="baseColumnIndex">基点となるINSTL品番列index (0 start)</param>
            ''' <param name="removeCount">削除列数</param>
            ''' <remarks></remarks>
            Public Sub Remove(ByVal baseColumnIndex As Integer, ByVal removeCount As Integer)
                UpdateColumn(baseColumnIndex, removeCount, True)
            End Sub
            ''' <summary>
            ''' INSTL品番列を挿入する
            ''' </summary>
            ''' <param name="baseColumnIndex">基点となるINSTL品番列index (0 start)</param>
            ''' <param name="columnCount">挿入列数</param>
            ''' <remarks></remarks>
            Public Sub Insert(ByVal baseColumnIndex As Integer, ByVal columnCount As Integer)
                UpdateColumn(baseColumnIndex, columnCount, False)
            End Sub
            ''' <summary>
            ''' 列の挿入・削除を行う
            ''' </summary>
            ''' <param name="baseColumnIndex">基点となるINSTL品番列index (0 start)</param>
            ''' <param name="columnCount">挿入列数or削除列数</param>
            ''' <param name="isRemove">削除の場合、true</param>
            ''' <remarks></remarks>
            Private Sub UpdateColumn(ByVal baseColumnIndex As Integer, ByVal columnCount As Integer, ByVal isRemove As Boolean)

                Dim startColumnIndex As Integer = startColumnDelegate()
                Dim insertStartColumn As Integer = startColumnIndex + baseColumnIndex
                If Me._count < baseColumnIndex Then

                    Throw New ArgumentOutOfRangeException("baseColumnIndex", baseColumnIndex, Me._count & " より大きい値は、指定出来ません")
                End If
                Dim oldInstlColumnCount As Integer = Me._count
                If isRemove Then
                    For c As Integer = 0 To columnCount - 1
                        RemoveColumns(insertStartColumn, 1)
                    Next

                    'RemoveColumns(insertStartColumn, columnCount)
                Else
                    columnCount = Math.Min(oldInstlColumnCount + columnCount, MAX_COLUMN_COUNT) - oldInstlColumnCount
                    If columnCount = 0 Then
                        Return  ' INSTL列は1000列まで
                    End If
                    InsertColumns(insertStartColumn, columnCount)

                    ' ** 挿入した列に新規設定 **
                    For columnIndex As Integer = 0 To columnCount - 1
                        Dim spreadColumn As Integer = insertStartColumn + columnIndex
                        ' 列
                        With sheet.Columns(spreadColumn)
                            .CellType = GetInstlInsuCellType()
                            .Width = 20.0!
                            .HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
                            '列のロックとセルのロックは別なので変更の必要があるかも'
                            .Locked = True
                        End With
                        ' セル（INSTL品番）
                        With sheet.Cells(INSTL_HINBAN_ROW_INDEX, spreadColumn)
                            ' .CellTypeは行に設定済み
                            .Locked = False
                            .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
                        End With
                        ' セル（INSTL品番区分）
                        With sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, spreadColumn)
                            ' .CellTypeは行に設定済み
                            .Locked = False
                            .VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top
                            ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
                            '.RowSpan = 2
                            ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END
                        End With
                    Next
                    sheet.Rows(INSTL_HINBAN_KBN_ROW_INDEX).Border = borderFactory.GetUnderLine
                End If

                Dim scaleColumnCount As Integer = IIf(isRemove, -columnCount, columnCount)

                ' ** 挿入した列と、その右の既存の列に設定 **
                For spreadColumn As Integer = startColumnIndex + baseColumnIndex To startColumnIndex + oldInstlColumnCount + scaleColumnCount - 1
                    ' セル（"A"とか"AB"とか）
                    With sheet.Cells(INSTL_HINBAN_NO_ROW_INDEX, spreadColumn)
                        ' .CellTypeは行に設定済み
                        .Locked = True
                        .BackColor = System.Drawing.SystemColors.Control
                        .Border = borderFactory.GetUnderLine()
                        .Value = EzUtil.ConvIndexToAlphabet(spreadColumn - startColumnIndex)
                    End With
                Next

                With sheet.Cells(INSTL_HINBAN_MIDASHI_ROW_INDEX, startColumnIndex)
                    ' .CellTypeは行に設定済み
                    .Border = borderFactory.GetUnderLine()
                    .ColumnSpan = oldInstlColumnCount + scaleColumnCount
                    .Value = "INSTL品番"
                End With

                Me._count += scaleColumnCount
            End Sub

            Private InstlInsuCellType As TextCellType
            ''' <summary>
            ''' INSTL品番員数を返す
            ''' </summary>
            ''' <returns>INSTL品番員数セル</returns>
            ''' <remarks></remarks>
            Private Function GetInstlInsuCellType() As TextCellType
                If InstlInsuCellType Is Nothing Then
                    InstlInsuCellType = BuhinEditSpreadUtil.NewGeneralTextCellType()
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

        ''' <summary>
        ''' Spreadの変更イベントハンドラ
        ''' </summary>
        ''' <param name="sender">イベントハンドラに従う</param>
        ''' <param name="e">イベントハンドラに従う</param>
        ''' <remarks></remarks>
        Private Sub Spread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            Cursor.Current = Cursors.WaitCursor
            If IsSuspend_SpreadChangeEvent Then
                Return
            End If
            OnChange(e.Row, e.Column)
            '画面を綺麗に、実行中のカーソルを元に戻す。
            Cursor.Current = Cursors.Default
        End Sub

        ''' <summary>
        ''' INSTL品番列のデータをクリアする
        ''' </summary>
        ''' <param name="dataOnly">データだけをクリアする場合、true</param>
        ''' <remarks></remarks>
        Public Sub ClearInstlColumns(Optional ByVal dataOnly As Boolean = True)
            SpreadUtil.RemoveHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
            Try
                sheet.ColumnCount = GetStartInstlSpreadColumn() + anInstlColumns.Count
                sheet.ClearRange(INSTL_HINBAN_ROW_INDEX, GetStartInstlSpreadColumn, sheet.RowCount - INSTL_HINBAN_ROW_INDEX, sheet.ColumnCount, dataOnly)
            Finally
                SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
            End Try
        End Sub

        ''' <summary>
        ''' Sheet全体の背景色を元に戻す
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearSheetBackColor()
            ' メモの列名部分
            '　メモ欄があるときだけ
            If aMemoColumns.Count > 0 Then
                With sheet.Cells(MEMO_ROW_INDEX, GetStartMemoSpreadColumn, MEMO_ROW_INDEX + 2 - 1, GetStartMemoSpreadColumn() + aMemoColumns.Count - 1)
                    .ResetBackColor()
                    '.ResetForeColor()
                    '.ResetFont()
                End With
            End If
            ' INSTL品番の列名部分
            With sheet.Cells(INSTL_HINBAN_ROW_INDEX, GetStartInstlSpreadColumn, 3, sheet.ColumnCount - GetStartInstlSpreadColumn())
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
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearSheetBackColorAll()
            ' メモの列名部分
            '　メモ欄があるときだけ
            If aMemoColumns.Count > 0 Then
                With sheet.Cells(MEMO_ROW_INDEX, GetStartMemoSpreadColumn, MEMO_ROW_INDEX + 2 - 1, GetStartMemoSpreadColumn() + aMemoColumns.Count - 1)
                    .ResetBackColor()
                    .ResetForeColor()
                    .ResetFont()
                End With
            End If
            ' INSTL品番の列名部分
            With sheet.Cells(INSTL_HINBAN_ROW_INDEX, GetStartInstlSpreadColumn, 3, sheet.ColumnCount - GetStartInstlSpreadColumn())
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

        Private onChangedColumnIdx As Integer
        ''' <summary>
        ''' 入力データ変更時の処理
        ''' </summary>
        ''' <param name="row">Spread行index</param>
        ''' <param name="column">spread列index</param>
        ''' <remarks></remarks>
        Private Sub OnChange(ByVal row As Integer, ByVal column As Integer)
            If row < 0 Then
                Exit Sub
            End If

            onChangedColumnIdx = column

            Dim rowNo As Integer = row - titleRows
            Dim value As Object = sheet.Cells(row, column).Value
            Select Case Convert.ToString(sheet.Columns(column).Tag)
                Case TAG_GOSHA

                    ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD BEGIN
                Case TAG_CHECK_FLAG
                    subject.CheckFlg(row - titleRows) = sheet.Cells(row, column).Value
                    ''↑↑2014/09/01 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD END

                Case Else
                    If IsMemoSpreadColumn(column) Then
                        Dim columnIndex As Integer = ConvSpreadColumnToAlMemo(column)

                        If row = MEMO_ROW_INDEX + 1 Or row = MEMO_ROW_INDEX Then
                            subject.MemoTitle(columnIndex) = StringUtil.ToString(value)
                        Else ' 適用
                            subject.MemoTekiyou(rowNo, columnIndex) = StringUtil.ToString(value)
                        End If

                    ElseIf IsInstlSpreadColumn(column) Then
                        Dim columnIndex As Integer = ConvSpreadColumnToAlInstl(column)
                        If row = INSTL_HINBAN_ROW_INDEX Then
                            If Not StringUtil.IsEmpty(subject.InstlHinban(columnIndex)) Or Not StringUtil.IsEmpty(StringUtil.ToString(value)) Then

                                If Not StringUtil.IsEmpty(subject.InstlHinban(columnIndex)) And _
                                        StringUtil.IsEmpty(value) Then
                                    MsgBox("INSTL品番の削除は列削除で行ってください。", MsgBoxStyle.OkOnly, "ERROR")
                                    sheet.Cells(row, column).Value = subject.InstlHinban(columnIndex)
                                Else

                                    'INSTL品番
                                    subject.InstlHinban(columnIndex) = StringUtil.ToString(value)
                                    'INSTL品番が入力されたら背景色をリセット'
                                    sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).ResetBackColor()

                                    ''2015/08/18 追加 E.Ubukata Ver 2.10.8
                                    '' 文字色の初期化（エラー検出で赤背景、白文字になっているため背景色をリセットしたら文字も元に戻す）
                                    If sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).ForeColor = Color.White Then
                                        If sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).Font.Bold Then
                                            sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).ForeColor = Color.Blue
                                        Else
                                            sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).ResetForeColor()
                                        End If
                                    End If


                                    ''↓↓2014/07/30 1 ベース部品表作成表機能増強_Ⅶ) (ダニエル上海)柳沼 ADD BEGIN
                                    If StringUtil.IsNotEmpty(sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, column).Value) Then
                                        subject.InstlHinbanKbn(columnIndex) = sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, column).Value

                                        ''2015/08/18 変更 E.Ubukata Ver 2.10.8
                                        '' INSTL品番区分の背景色に変更
                                        'INSTL品番区分が入力されたら背景色をリセット'
                                        'sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).ResetBackColor()
                                        sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).ResetBackColor()

                                        ''2015/08/18 追加 E.Ubukata Ver 2.10.8
                                        '' 文字色の初期化（エラー検出で赤背景、白文字になっているため背景色をリセットしたら文字も元に戻す）
                                        If sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).ForeColor = Color.White Then
                                            If sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).Font.Bold Then
                                                sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).ForeColor = Color.Blue
                                            Else
                                                sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).ResetForeColor()
                                            End If
                                        End If
                                    End If
                                    ''↑↑2014/07/30 1 ベース部品表作成表機能増強_Ⅶ) (ダニエル上海)柳沼 ADD END

                                End If
                            End If
                        ElseIf row = INSTL_HINBAN_KBN_ROW_INDEX Then
                            If Not StringUtil.IsEmpty(subject.InstlHinban(columnIndex)) Then
                                'DELETEキーで反応してしまうから'
                                If Not StringUtil.IsEmpty(subject.InstlHinbanKbn(columnIndex)) Or Not StringUtil.IsEmpty(StringUtil.ToString(value)) Then
                                    ' 2012-01-11 ここに#3-1対応を
                                    '試作区分の入力時にチェックイベント（全角入力）を実行する
                                    If Not SpellCheck(sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).Value) Then
                                        MsgBox("試作区分に全角文字が入力されています。", MsgBoxStyle.OkOnly, "ERROR")
                                        sheet.Cells(row, column).Value = subject.InstlHinbanKbn(columnIndex)
                                    Else
                                        subject.InstlHinbanKbn(columnIndex) = StringUtil.ToString(value)
                                        'INSTL品番区分が入力されたら背景色をリセット'
                                        sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).ResetBackColor()
                                        '2012/03/09 文字色もリセットしておかないと白文字のままになる'
                                        sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).ResetForeColor()
                                        sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).ResetFont()
                                    End If
                                End If
                            End If
                        Else ' 員数
                            subject.InsuSuryo(rowNo, columnIndex) = StringUtil.ToString(value)

                            'Try
                            '    If Not UpdateFlag Then
                            '        InsuArray(rowNo)(columnIndex) = Integer.Parse(value)
                            '    End If
                            'Catch ex As NullReferenceException
                            '    InsuArray(rowNo) = New Integer(subject.GetInputInsuColumnIndexes(INSTL_HINBAN_NO_ROW_INDEX).Count + 100) {}
                            'Finally
                            '    If InsuArray(rowNo)(columnIndex) = Integer.Parse(value) Then
                            '        '変更なし'

                            '    Else
                            '        '変更あり'
                            '        AddInsuArray(rowNo, columnIndex, value)
                            '        If UpdateFlag Then
                            '            SetInstlColumnColor(columnIndex)
                            '        End If
                            '    End If
                            'End Try

                        End If
                    End If

            End Select
            subject.NotifyObservers(rowNo)
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

            r0552 = impl.FindByBuhinRhac0552(sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).Value)

            '量産EBOMに存在するINSTL品番か確認'
            If r0552 Is Nothing Then

                r0553 = impl.FindByBuhinRhac0553AL(sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).Value)

                If r0553 Is Nothing Then
                    r0551 = impl.FindByBuhinRhac0551(sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).Value)
                    If r0551 Is Nothing Then
                        'sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).BackColor = Color.Yellow
                    Else
                        'sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).BackColor = Color.Red
                    End If
                Else
                    'sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).BackColor = Color.Red
                End If
            Else
                ''
                'sheet.Cells(INSTL_HINBAN_ROW_INDEX, ConvAlInstlToSpreadColumn(columnIndex)).BackColor = Color.Red
            End If
        End Sub



        ''' <summary>
        ''' メモ列のSpread列indexかを返す
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Function IsMemoSpreadColumn(ByVal spreadColumn As Integer) As Boolean
            Return GetStartMemoSpreadColumn() <= spreadColumn AndAlso spreadColumn < GetStartInstlSpreadColumn()
        End Function

        ''' <summary>
        ''' メモ列が始まるSpread列indexを返す
        ''' </summary>
        ''' <returns>Spread列index</returns>
        ''' <remarks></remarks>
        Private Function GetStartMemoSpreadColumn() As Integer
            Return DYNAMIC_INFO_START_COLUMN + subject.DynamicColumnCount
        End Function

        ''' <summary>
        ''' INSTL品番列のSpread列indexかを返す
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Function IsInstlSpreadColumn(ByVal spreadColumn As Integer) As Boolean

            Return GetStartInstlSpreadColumn() <= spreadColumn
        End Function

        ''' <summary>
        ''' INSTL品番列が始まるSpread列indexを返す
        ''' </summary>
        ''' <returns>Spread列index</returns>
        ''' <remarks></remarks>
        Private Function GetStartInstlSpreadColumn() As Integer

            Return GetStartMemoSpreadColumn() + aMemoColumns.Count
        End Function

        ''' <summary>
        ''' Spread表示を(再)表示する
        ''' </summary>
        ''' <param name="observable">表示指示元のObservable</param>
        ''' <param name="arg">引数</param>
        ''' <remarks></remarks>
        Public Sub Update(ByVal observable As Observable, ByVal arg As Object) Implements Observer.Update

            'If IsMemoSpreadColumn(onChangedColumnIdx) Then
            '    EmptyRowLock()
            '    Exit Sub
            'End If

            If arg Is Nothing Then
                ShowColumnIfChange()
                Update(observable, TITLE_ROW_INDEX)
                For Each rowNo As Integer In subject.GetInputRowIndexes
                    Update(observable, rowNo)
                Next

                UpdateFlag = True
                'EmptyRowLock()
                'ここでメモのロックの解除できる？'
                ''↓↓2014/09/05 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
                'For columnIndex As Integer = 0 To subject.MemoColumnCount
                For columnIndex As Integer = 0 To subject.MemoColumnCount - 1
                    ''↑↑2014/09/05 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END

                    ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅶ (3)) (ダニエル上海)柳沼 ADD BEGIN
                    'For rowIndex As Integer = 5 To sheet.RowCount - 1
                    For rowIndex As Integer = Me.titleRows To sheet.RowCount - 1
                        ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅶ (3)) (ダニエル上海)柳沼 ADD END
                        ''↓↓2014/08/27 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 CHG BEGIN
                        'If StringUtil.IsEmpty(sheet.Cells(rowIndex, 0).Value) Then
                        If StringUtil.IsEmpty(sheet.Cells(rowIndex, sheet.Columns(TAG_GOSHA).Index).Value) Then
                            ''↑↑2014/08/27 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 CHG END
                            sheet.Cells(rowIndex, ConvAlMemoToSpreadColumn(columnIndex)).Locked = True
                        Else
                            sheet.Cells(rowIndex, ConvAlMemoToSpreadColumn(columnIndex)).Locked = False
                        End If
                    Next
                Next

                ''↓↓2014/08/13 1 ベース部品表作成表機能増強_Ⅶ (3)) (ダニエル上海)柳沼 ADD BEGIN
                'sheet.FrozenRowCount = 5
                sheet.FrozenRowCount = Me.titleRows
                ''↑↑2014/08/13 1 ベース部品表作成表機能増強_Ⅶ (3)) (ダニエル上海)柳沼 ADD END
            ElseIf IsNumeric(arg) Then
                Dim rowNo As Integer = CInt(arg)
                Dim row As Integer = titleRows + rowNo
                ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(8)) (TES)張 ADD BEGIN
                Dim strGetValue As String() = {"10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25"}
                Dim strSetValue As String() = {"MA", "MB", "MC", "MD", "ME", "MF", "MG", "MH", "MI", "MJ", "MK", "ML", "MM", "MN", "MO", "MP"}
                ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(8)) (TES)張 ADD END
                If rowNo <= TITLE_ROW_INDEX Then
                    For Each columnIndex As Integer In subject.GetInputMemoTitleColumnIndexes
                        sheet.Cells(MEMO_ROW_INDEX, ConvAlMemoToSpreadColumn(columnIndex)).Value = subject.MemoTitle(columnIndex)
                    Next
                    For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                        Dim spreadColumn As Integer = ConvAlInstlToSpreadColumn(columnIndex)
                        sheet.Cells(INSTL_HINBAN_ROW_INDEX, spreadColumn).Value = subject.InstlHinban(columnIndex)
                        sheet.Cells(INSTL_HINBAN_KBN_ROW_INDEX, spreadColumn).Value = subject.InstlHinbanKbn(columnIndex)
                        ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(8)) (TES)張 ADD BEGIN
                        ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(8)) 酒井 ADD BEGIN
                        'If subject.InstlDataKbn(columnIndex) = "0" Then
                        If StringUtil.IsEmpty(subject.InstlDataKbn(columnIndex)) Or subject.InstlDataKbn(columnIndex) = "0" Then
                            ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(8)) 酒井 ADD END
                            sheet.Cells(INSTL_DATA_KBN_ROW_INDEX, spreadColumn).Value = ""
                            ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(8)) 酒井 ADD BEGIN
                            'ElseIf subject.InstlDataKbn(columnIndex) >= "10" AndAlso subject.InstlDataKbn(columnIndex) <= "25" Then
                        ElseIf CInt(subject.InstlDataKbn(columnIndex)) >= "10" AndAlso CInt(subject.InstlDataKbn(columnIndex)) <= "25" Then
                            ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(8)) 酒井 ADD END
                            For index = 0 To strGetValue.Length - 1
                                If strGetValue(index).Equals(subject.InstlDataKbn(columnIndex)) Then
                                    sheet.Cells(INSTL_DATA_KBN_ROW_INDEX, spreadColumn).Value = strSetValue(index)
                                End If
                            Next
                        Else
                            sheet.Cells(INSTL_DATA_KBN_ROW_INDEX, spreadColumn).Value = "M" & subject.InstlDataKbn(columnIndex)
                        End If
                        ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(8)) (TES)張 ADD END
                        ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(8)) 酒井 ADD BEGIN
                        'sheet.Cells(INSTL_DATA_KBN_ROW_INDEX, spreadColumn).Value = subject.InstlDataKbn(columnIndex)
                        ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(8)) 酒井 ADD END
                        ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_k) (TES)張 ADD BEGIN
                        '該当イベント取得
                        Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                        Dim eventVo As TShisakuEventVo
                        eventVo = eventDao.FindByPk(subject.shisakuEventCode)
                        '該当イベントが「移管車改修」の場合
                        If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                            If subject.BaseInstlFlg(columnIndex) = 1 Then
                                sheet.Columns(spreadColumn).BackColor = BASE_COLOR
                                sheet.Columns(spreadColumn).ForeColor = BASE_ForeCOLOR
                                sheet.Columns(spreadColumn).Locked = True
                                ''↓↓2014/08/21 Ⅰ.3.設計編集 ベース改修専用化_k) 酒井 ADD BEGIN
                                '結合されたヘッダー行のみ色を戻す
                                sheet.Cells(INSTL_HINBAN_MIDASHI_ROW_INDEX, spreadColumn).ResetBackColor()
                                sheet.Cells(INSTL_HINBAN_MIDASHI_ROW_INDEX, spreadColumn).ForeColor = Color.Black
                                'ResetBackcolor済み列のみ、セル指定で設定する
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
                                ''↑↑2014/08/21 Ⅰ.3.設計編集 ベース改修専用化_k) 酒井 ADD END
                            End If
                        End If
                        ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_k) (TES)張 ADD END
                    Next
                    sheet.Rows(INSTL_HINBAN_ROW_INDEX).Locked = False
                    sheet.Rows(INSTL_HINBAN_KBN_ROW_INDEX).Locked = False
                    ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(8)) (TES)張 ADD BEGIN
                    sheet.Rows(INSTL_DATA_KBN_ROW_INDEX).Locked = True
                    ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥(8)) (TES)張 ADD END
                Else
                    sheet.Cells(row, sheet.Columns(TAG_GOSHA).Index).Value = subject.ShisakuGosha(rowNo)
                    '2012-01-11 改修 #1-2
                    '試作種別Dは背景色をグレーに 
                    If subject.ShisakuSyubetu(rowNo) = "D" Then
                        sheet.Rows(row).BackColor = Color.DimGray
                        sheet.Rows(row).ForeColor = Color.LightGray
                    End If
                    If Not StringUtil.IsEmpty(subject.ShisakuGosha(rowNo)) Then
                        '号車、装備仕様列が入力出来てしまうので以下をTrueにしてみた。
                        'その他のロジックは無効にした。
                        'テスト結果、ちゃんと号車、装備仕様列が入力出来なくなり、
                        'INSTL品番列、メモ欄は入力出来ているようだ。
                        'そうなると無効にしたロジックは何をしていたのか・・・
                        'とりあえず様子を見てＯＫならこのままで行く。　2011/03/03　Ｂｙ柳沼
                        sheet.Rows(row).Locked = False

                        ''↓↓2014/09/05 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD BEGIN
                        'ベースINSTL品番列の員数欄はロック解除しないため、戻す
                        '該当イベント取得
                        Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                        Dim eventVo As TShisakuEventVo
                        eventVo = eventDao.FindByPk(subject.shisakuEventCode)
                        '該当イベントが「移管車改修」の場合
                        If eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" Then
                            For Each columnIndex As Integer In subject.GetInputInstlHinbanColumnIndexes
                                Dim spreadColumn As Integer = ConvAlInstlToSpreadColumn(columnIndex)
                                If subject.BaseInstlFlg(columnIndex) = 1 Then
                                    sheet.Cells(row, spreadColumn).Locked = True
                                End If
                            Next
                        End If
                        ''↑↑2014/09/05 Ⅰ.3.設計編集 ベース改修専用化 酒井 ADD END

                        '------------------------------------------------------------------------------
                        '以下の行から・・・
                        '------------------------------------------------------------------------------
                        ''これだけだと号車の列も入力できるので'
                        'sheet.Rows(row).Locked = False
                        ''号車のロックをかける'
                        'sheet.Columns(TAG_GOSHA).Locked = True
                        ''表示されている装備情報列にロックをかける TODO 処理をメソッドにするべきかも　樺澤'
                        ''For indexC As Integer = 0 To subject.OptionColumnCount - 1
                        ''    Dim columnNo As Integer = DYNAMIC_INFO_START_COLUMN + indexC
                        ''    For indexR As Integer = INSTL_HINBAN_KBN_ROW_INDEX + indexR To subject.GetInputRowIndexes
                        ''    Next
                        ''    sheet.Columns(columnNo).Locked = True
                        ''Next
                        'For index As Integer = 0 To subject.EventColumnCount - 1
                        '    Dim columnNo As Integer = DYNAMIC_INFO_START_COLUMN + index
                        '    sheet.Columns(columnNo).Locked = True
                        'Next
                    End If

                    '-------------------------------------------------------------------
                    '２次改修
                    '   カラムID取得用に初期設定
                    Dim i As Integer
                    Dim ColumnId As String
                    Dim ColumnIchi As Integer
                    Dim KanseiRirekiList As List(Of TShisakuEventKanseiRirekiVo)
                    Dim eventCompleteCarDao As EventEditCompleteCarRirekiDao = New EventEditCompleteCarRirekiDaoImpl()
                    '-------------------------------------------------------------------
                    '２次改修
                    '   装備仕様取得用に初期設定
                    Dim SoubiRirekiList As List(Of TShisakuEventSoubiRirekiVo)
                    Dim eventOptionDao As EventEditOptionRirekiDao = New EventEditOptionRirekiDaoImpl()
                    '-------------------------------------------------------------------

                    For columnIndex As Integer = 0 To subject.DynamicColumnCount - 1
                        '---------------------------------------------------------------------
                        '２次改修
                        '初期設定
                        Dim strMsg As String = ""
                        Dim j As Integer = 0
                        Dim strBefore As String = ""
                        Dim strAfter As String = ""

                        '   カラムID取得
                        ColumnId = subject.DynamicInfoColumnId(rowNo, columnIndex)

                        If ColumnId <> "1" And ColumnId <> "2" Then
                            '   試作イベント完成車履歴情報を検索し該当データを取得
                            KanseiRirekiList = eventCompleteCarDao.GetShisakuEventCompleteCarRirekiList( _
                                                                                subject.shisakuEventCode, _
                                                                                rowNo, ColumnId)
                            '   試作イベント完成車履歴情報を検索し該当データがあればSPREADにセット
                            If KanseiRirekiList.Count > 0 Then

                                For i = 0 To KanseiRirekiList.Count - 1

                                    If StringUtil.IsNotEmpty(KanseiRirekiList.Item(i).Before) Then
                                        strBefore = KanseiRirekiList.Item(i).Before
                                    Else
                                        strBefore = "空白"
                                    End If
                                    If StringUtil.IsNotEmpty(KanseiRirekiList.Item(i).After) Then
                                        strAfter = KanseiRirekiList.Item(i).After
                                    Else
                                        strAfter = "空白"
                                    End If

                                    If j > 0 Then
                                        strMsg += vbCrLf & vbCrLf
                                    End If
                                    '更新日、変更前、変更後をセット
                                    strMsg += Right(KanseiRirekiList.Item(i).UpdateBi, 5) & " : " _
                                            & strBefore & " ⇒ " & strAfter

                                    '1を加算
                                    j += 1

                                Next
                                '変更情報を該当セルにセット
                                'sheet.Cells(row, sheet.Columns(GetTagDynamicInfo(columnIndex)).Index).BackColor = Color.Red
                                sheet.Cells(row, sheet.Columns(GetTagDynamicInfo(columnIndex)).Index).NoteStyle = NoteStyle.PopupNote
                                sheet.Cells(row, sheet.Columns(GetTagDynamicInfo(columnIndex)).Index).Note = strMsg
                            End If
                        Else
                            '---------------------------------------------------------------------
                            '---------------------------------------------------------------------
                            '２次改修
                            '   カラム位置取得
                            ColumnIchi = subject.DynamicInfoColumnIchi(rowNo, columnIndex)
                            '   試作イベント装備仕様履歴情報を検索し該当データを取得
                            SoubiRirekiList = eventOptionDao.GetShisakuEventOptionRirekiList( _
                                                                                subject.shisakuEventCode, _
                                                                                rowNo, ColumnIchi, ColumnId)
                            '   試作イベント完成車履歴情報を検索し該当データがあればSPREADにセット
                            If SoubiRirekiList.Count > 0 Then

                                For i = 0 To SoubiRirekiList.Count - 1

                                    If StringUtil.IsNotEmpty(SoubiRirekiList.Item(i).ShisakuTekiyouBefore) Then
                                        strBefore = SoubiRirekiList.Item(i).ShisakuTekiyouBefore
                                    Else
                                        strBefore = "空白"
                                    End If
                                    If StringUtil.IsNotEmpty(SoubiRirekiList.Item(i).ShisakuTekiyouAfter) Then
                                        strAfter = SoubiRirekiList.Item(i).ShisakuTekiyouAfter
                                    Else
                                        strAfter = "空白"
                                    End If

                                    If j > 0 Then
                                        strMsg += vbCrLf & vbCrLf
                                    End If
                                    '更新日、変更前、変更後をセット
                                    strMsg += Right(SoubiRirekiList.Item(i).UpdateBi, 5) & " : " _
                                            & strBefore & " ⇒ " & strAfter

                                    '1を加算
                                    j += 1

                                Next
                                '変更情報を該当セルにセット
                                'sheet.Cells(row, sheet.Columns(GetTagDynamicInfo(columnIndex)).Index).BackColor = Color.Red
                                sheet.Cells(row, sheet.Columns(GetTagDynamicInfo(columnIndex)).Index).NoteStyle = NoteStyle.PopupNote
                                sheet.Cells(row, sheet.Columns(GetTagDynamicInfo(columnIndex)).Index).Note = strMsg
                            End If
                        End If

                        sheet.Cells(row, sheet.Columns(GetTagDynamicInfo(columnIndex)).Index).Value = subject.DynamicInfo(rowNo, columnIndex)

                    Next

                    For Each columnIndex As Integer In subject.GetInputMemoTekiyouColumnIndexes(rowNo)
                        sheet.Cells(row, ConvAlMemoToSpreadColumn(columnIndex)).Value = subject.MemoTekiyou(rowNo, columnIndex)
                    Next
                    For Each columnIndex As Integer In subject.GetInputInsuColumnIndexes(rowNo)
                        sheet.Cells(row, ConvAlInstlToSpreadColumn(columnIndex)).Value = subject.InsuSuryo(rowNo, columnIndex)
                    Next
                End If
                OnRowLock(row)
            End If
        End Sub

        ''' <summary>
        ''' ロックをする
        ''' </summary>
        ''' <param name="row">行Index</param>
        ''' <param name="IsLocked">ロックするならTrue</param>
        ''' <remarks></remarks>
        Private Sub LockRowByRule(ByVal row As Integer, ByVal IsLocked As Boolean)
            'For column As Integer = GetStartMemoSpreadColumn() To sheet.Columns.Count - 1
            '    sheet.Cells(row, column).Locked = IsLocked
            'Next
            For column As Integer = 0 To GetStartMemoSpreadColumn() - 1
                sheet.Cells(row, column).Locked = IsLocked
            Next
        End Sub

        ''' <summary>
        ''' ロックする
        ''' </summary>
        ''' <param name="row">行Index</param>
        ''' <remarks></remarks>
        Private Sub OnRowLock(ByVal row As Integer)
            LockRowByRule(row, True)
        End Sub

        ''↓↓2014/08/27 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 CHG BEGIN
        'Private Sub EmptyRowLock()
        '    '号車が入力された行以外の行にロックを掛けてみる'
        '    For rowindex As Integer = 0 To subject.GetInputRowIndexes.Count + 100
        '        If StringUtil.IsEmpty(subject.ShisakuGosha(rowindex)) Then
        '            sheet.Rows(titleRows + rowindex).Locked = True
        '        End If
        '    Next
        'End Sub
        ''' <summary>
        ''' 号車が入力されていない行にロックを掛ける
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub EmptyRowLock()
            '号車が入力された行以外の行にロックを掛けてみる'
            For rowindex As Integer = titleRows To sheet.RowCount - 1
                If Not StringUtil.IsEmpty(sheet.Cells(rowindex, sheet.Columns(TAG_GOSHA).Index).Value) Then
                    sheet.Cells(rowindex, sheet.Columns(TAG_CHECK_FLAG).Index).Locked = False
                Else
                    sheet.Rows(rowindex).Locked = True
                End If
            Next
        End Sub
        ''↑↑2014/08/27 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 CHG END

        Public Sub setInstlHinbanCell(ByVal columnIndex As Integer)
            sheet.Cells(INSTL_HINBAN_ROW_INDEX, columnIndex).CellType = GetInstlHinbanCellType()
            'sheet.Cells(MEMO_ROW_INDEX, ConvAlMemoToSpreadColumn(columnIndex)).CellType = GetMemoTitleCellType()
        End Sub

        Public Sub setMemoCell(ByVal columnIndex As Integer)
            sheet.Cells(MEMO_ROW_INDEX, columnIndex).CellType = GetMemoTitleCellType()
            'sheet.Cells(MEMO_ROW_INDEX, ConvAlMemoToSpreadColumn(columnIndex)).CellType = GetMemoTitleCellType()
        End Sub


        ''' <summary>
        ''' Spread列indexを、(Subject)メモ列indexにして返す
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <returns>(Subject)メモ列index</returns>
        ''' <remarks></remarks>
        Private Function ConvSpreadColumnToAlMemo(ByVal spreadColumn As Integer) As Integer
            Return spreadColumn - GetStartMemoSpreadColumn()
        End Function
        ''' <summary>
        ''' メモ列indexから、Spreadの列indexを算出する
        ''' </summary>
        ''' <param name="columnIndex">メモ列index</param>
        ''' <returns>Spreadの列index</returns>
        ''' <remarks></remarks>
        Private Function ConvAlMemoToSpreadColumn(ByVal columnIndex As Integer) As Integer

            Return GetStartMemoSpreadColumn() + columnIndex
        End Function
        ''' <summary>
        ''' Spread列indexを、(Subject)INSTL列indexにして返す
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <returns>(Subject)INSTL列index</returns>
        ''' <remarks></remarks>
        Public Function ConvSpreadColumnToAlInstl(ByVal spreadColumn As Integer) As Integer
            ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD BEGIN
            'Private Function ConvSpreadColumnToAlInstl(ByVal spreadColumn As Integer) As Integer
            ''↑↑2014/09/01 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD END
            Return spreadColumn - (GetStartInstlSpreadColumn())
        End Function

        ''' <summary>
        ''' INSTL情報の列indexから、Spreadの列indexを算出する
        ''' </summary>
        ''' <param name="columnIndex">INSTL情報の列index</param>
        ''' <returns>Spreadの列index</returns>
        ''' <remarks></remarks>
        Public Function ConvAlInstlToSpreadColumn(ByVal columnIndex As Integer) As Integer
            ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD BEGIN
            'Private Function ConvAlInstlToSpreadColumn(ByVal columnIndex As Integer) As Integer
            ''↑↑2014/09/01 Ⅰ.3.設計編集 ベース改修専用化_ap) 酒井 ADD END

            Return GetStartInstlSpreadColumn() + columnIndex
        End Function
        ''' <summary>
        ''' INSTL情報の列indexから、Spreadの列indexを算出する
        ''' </summary>
        ''' <param name="columnIndexes">INSTL情報の列index</param>
        ''' <returns>Spreadの列index</returns>
        ''' <remarks></remarks>
        Private Function ConvAlInstlToSpreadColumn(ByVal columnIndexes As ICollection(Of Integer)) As ICollection(Of Integer)
            Dim result As New List(Of Integer)
            For Each columnIndex As Integer In columnIndexes
                result.Add(ConvAlInstlToSpreadColumn(columnIndex))
            Next
            Return result
        End Function

        Private Function GetTagDynamicInfo(ByVal columnIndex As Integer) As String
            Return String.Format("INFO{0}", columnIndex)
        End Function
        Private Function GetTagMemo(ByVal columnIndex As Integer) As String
            Return String.Format("MEMO{0}", columnIndex)
        End Function

        Private bakShowColumnBag As New BuhinEditAlShowColumnBag
        ''' <summary>
        ''' 表示列設定が変わっていたら、Spread表示列を変更する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ShowColumnIfChange()

            If bakShowColumnBag Is subject.ShowColumnBag Then
                Return
            End If

            IsSuspend_SpreadChangeEvent = True
            Try
                RemoveColumnsInternal(DYNAMIC_INFO_START_COLUMN, bakShowColumnBag.SoubiVos.Count)
                RemoveColumnsInternal(DYNAMIC_INFO_START_COLUMN, bakShowColumnBag.SoubiShiyouVos.Count)

                If 0 < subject.OptionColumnCount Then
                    InsertColumnsInternal(DYNAMIC_INFO_START_COLUMN, subject.OptionColumnCount)

                    ' 見出し
                    Dim IsBasicTitleMostRight As Boolean = (subject.OptionColumnCount = subject.BasicOptionColumnCount)
                    SettingOptionTitleMidashiCell(sheet.Cells(OPTION_TITLE_MIDASHI_ROW, DYNAMIC_INFO_START_COLUMN), "基本装備仕様", subject.BasicOptionColumnCount, IsBasicTitleMostRight)
                    If Not IsBasicTitleMostRight Then
                        SettingOptionTitleMidashiCell(sheet.Cells(OPTION_TITLE_MIDASHI_ROW, DYNAMIC_INFO_START_COLUMN + subject.BasicOptionColumnCount), "特別装備仕様", subject.SpecialOptionColumnCount, True)
                    End If

                    For index As Integer = 0 To subject.OptionColumnCount - 1
                        Dim columnNo As Integer = DYNAMIC_INFO_START_COLUMN + index

                        '大区分、中区分の有無チェック
                        Dim strDaiKbn As String = ""
                        Dim strChuKbn As String = ""
                        '中区分を括弧で囲んでみる。
                        If StringUtil.IsNotEmpty(subject.OptionTitleDai(index)) Then
                            strDaiKbn = "・"
                        End If
                        If StringUtil.IsNotEmpty(subject.OptionTitleChu(index)) Then
                            strChuKbn = "・"
                        End If
                        sheet.Cells(OPTION_TITLE_ROW, columnNo).Value = subject.OptionTitleDai(index) _
                                                                      & strDaiKbn & subject.OptionTitleChu(index) _
                                                                      & strChuKbn & subject.OptionTitle(index)
                        'sheet.Cells(OPTION_TITLE_ROW, columnNo).Value = subject.OptionTitle(index)



                        sheet.Columns(columnNo).Locked = True
                        sheet.Columns(columnNo).Width = 20.0!
                        SettingOptionTitleCell(sheet.Cells(OPTION_TITLE_ROW, columnNo))
                    Next
                    ' 一番右のセル＆列にだけ右側二重線
                    Dim columnMostRight As Integer = DYNAMIC_INFO_START_COLUMN + subject.OptionColumnCount - 1
                    SettingOptionTitleCell(sheet.Cells(OPTION_TITLE_ROW, columnMostRight), True)
                    sheet.Columns(columnMostRight).Border = borderFactory.GetRightWLine()
                End If


                If 0 = subject.EventColumnCount Then
                    sheet.Cells(0, sheet.Columns(TAG_GOSHA).Index).Border = borderFactory.GetUnderLineAndRightWLine()
                    sheet.Columns(TAG_GOSHA).Border = borderFactory.GetRightWLine()
                Else
                    sheet.Cells(0, sheet.Columns(TAG_GOSHA).Index).Border = borderFactory.GetUnderLine()
                    sheet.Columns(TAG_GOSHA).Border = Nothing
                End If
                If 0 < subject.EventColumnCount Then
                    InsertColumnsInternal(DYNAMIC_INFO_START_COLUMN, subject.EventColumnCount)

                    ' 見出し
                    Dim columnCountBase As Integer = subject.GetEventColumnCountBase
                    Dim columnCountBaseTenkai As Integer = subject.GetEventColumnCountBaseTenkai
                    Dim IsBaseTitleMostRight As Boolean = (subject.EventColumnCount = columnCountBase + columnCountBaseTenkai)
                    If columnCountBase <> 0 Then
                        SettingEventTitleMidashiCell(sheet.Cells(EVENT_TITLE_MIDASHI_ROW, DYNAMIC_INFO_START_COLUMN), _
                                                     "ベース車", columnCountBase, IsBaseTitleMostRight)
                    End If
                    If columnCountBaseTenkai <> 0 Then
                        SettingEventTitleMidashiCell(sheet.Cells(EVENT_TITLE_MIDASHI_ROW, DYNAMIC_INFO_START_COLUMN + columnCountBase), _
                                                     "設計展開ベース車", columnCountBaseTenkai, IsBaseTitleMostRight)
                    End If
                    If Not IsBaseTitleMostRight Then
                        SettingEventTitleMidashiCell(sheet.Cells(EVENT_TITLE_MIDASHI_ROW, DYNAMIC_INFO_START_COLUMN + columnCountBase + columnCountBaseTenkai), _
                                                     "完成車", subject.EventColumnCount - columnCountBase - columnCountBaseTenkai, True)
                    End If

                    For index As Integer = 0 To subject.EventColumnCount - 1
                        Dim columnNo As Integer = DYNAMIC_INFO_START_COLUMN + index

                        Dim strEgTmMemo As String = ""
                        strEgTmMemo = getEventMemo(subject.shisakuEventCode, _
                                                   subject.ShowColumnBag.SoubiVos(index).ShisakuSoubiHyoujiJun)

                        If StringUtil.IsNotEmpty(strEgTmMemo) Then
                            sheet.Cells(EVENT_TITLE_ROW, columnNo).Value = strEgTmMemo
                        Else
                            sheet.Cells(EVENT_TITLE_ROW, columnNo).Value = subject.EventTitle(index)
                        End If

                        sheet.Columns(columnNo).Locked = True
                        sheet.Columns(columnNo).Width = 45.0!
                        SettingEventTitleCell(sheet.Cells(EVENT_TITLE_ROW, columnNo))
                    Next
                    ' 一番右のセル＆列にだけ右側二重線
                    Dim columnMostRight As Integer = DYNAMIC_INFO_START_COLUMN + subject.EventColumnCount - 1
                    SettingEventTitleCell(sheet.Cells(EVENT_TITLE_ROW, columnMostRight), True)
                    sheet.Columns(columnMostRight).Border = borderFactory.GetRightWLine()
                End If

                For columnIndex As Integer = 0 To subject.DynamicColumnCount - 1
                    sheet.Columns(DYNAMIC_INFO_START_COLUMN + columnIndex).Tag = GetTagDynamicInfo(columnIndex)
                Next

                toolTip.Clear()
                toolTip.Add(New ToolTipImpl(GetStartInstlSpreadColumn))
            Finally
                IsSuspend_SpreadChangeEvent = False
            End Try
            bakShowColumnBag = subject.ShowColumnBag
        End Sub

        ''' <summary>
        ''' 試作区分のToolTip表示実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class ToolTipImpl : Implements SpreadToolTip.AnEvent
            Private _instlStartColumnIndex As Integer
            Public Sub New(ByVal instlStartColumnIndex As Integer)
                Me._instlStartColumnIndex = instlStartColumnIndex
            End Sub
            Public Sub TextTipFetch(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.TextTipFetchEventArgs) Implements SpreadToolTip.AnEvent.TextTipFetch
                If INSTL_HINBAN_KBN_ROW_INDEX = e.Row Then
                    If _instlStartColumnIndex <= e.Column Then
                        e.TipText = "試作区分５桁（試作限定での取扱）"
                        e.ShowTip = True
                    End If
                End If
            End Sub
        End Class

        Private Sub RemoveColumnsInternal(ByVal baseColumnNo As Integer, ByVal removeCount As Integer)
            If removeCount = 0 Then
                Return
            End If
            sheet.RemoveColumns(baseColumnNo, removeCount)
        End Sub

        Private Sub InsertColumnsInternal(ByVal baseColumnNo As Integer, ByVal insertCount As Integer)
            If insertCount = 0 Then
                Return
            End If
            sheet.AddColumns(baseColumnNo, insertCount)
        End Sub

        ''' <summary>
        ''' 列挿入・列削除が可能かを返す
        ''' </summary>
        ''' <param name="selection">選択範囲</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Function CanInsertColumnRemoveColumn(ByVal selection As CellRange) As Boolean
            Return IsMemoSpreadColumn(selection.Column) OrElse IsInstlSpreadColumn(selection.Column)
        End Function

        ''' <summary>
        ''' 空列を挿入する
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <param name="insertCount">挿入行数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumns(ByVal spreadColumn As Integer, ByVal insertCount As Integer)
            If insertCount = 0 Then
                Return
            End If
            IsSuspend_SpreadChangeEvent = True
            Try
                If IsMemoSpreadColumn(spreadColumn) Then
                    Dim subjectColumn As Integer = ConvSpreadColumnToAlMemo(spreadColumn)
                    aMemoColumns.Insert(subjectColumn, insertCount)
                    subject.InsertColumnInMemo(subjectColumn, insertCount)
                ElseIf IsInstlSpreadColumn(spreadColumn) Then
                    Dim subjectColumn As Integer = ConvSpreadColumnToAlInstl(spreadColumn)
                    anInstlColumns.Insert(subjectColumn, insertCount)
                    subject.InsertColumnInInstl(subjectColumn, insertCount)
                Else
                    Throw New NotSupportedException("メモ欄・INSTL品番欄以外の列挿入は不可")
                End If
                subject.NotifyObservers()
            Finally
                IsSuspend_SpreadChangeEvent = False
            End Try
        End Sub

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_s) (TES)張 CHG BEGIN
        ' Public Sub RemoveColumns(ByVal spreadColumn As Integer, ByVal removeCount As Integer)
        ''' <summary>
        ''' 列を削除する
        ''' </summary>
        ''' <param name="spreadColumn">Spread列index</param>
        ''' <param name="removeCount">削除行数</param>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumns(ByVal spreadColumn As Integer, ByVal removeCount As Integer, ByVal shisakuEventCode As String)
            If removeCount = 0 Then
                Return
            End If
            IsSuspend_SpreadChangeEvent = True

            'For index As Integer = spreadColumn To spreadColumn + removeCount - 1
            Try
                If IsMemoSpreadColumn(spreadColumn) Then
                    Dim subjectColumn As Integer = ConvSpreadColumnToAlMemo(spreadColumn)
                    aMemoColumns.Remove(subjectColumn, removeCount)
                    subject.RemoveColumnInMemo(subjectColumn, removeCount)
                ElseIf IsInstlSpreadColumn(spreadColumn) Then
                    '該当イベント取得
                    Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
                    Dim eventVo As TShisakuEventVo
                    eventVo = eventDao.FindByPk(shisakuEventCode)

                    ''↓↓2014/08/21 Ⅰ.3.設計編集 ベース改修専用化_s) 酒井 CHG BEGIN
                    'If (eventVo.BlockAlertKind = 2 And subject.BaseInstlFlg(spreadColumn) = 1) Then
                    If Not (eventVo.BlockAlertKind = 2 And eventVo.KounyuShijiFlg = "0" And subject.BaseInstlFlg(ConvSpreadColumnToAlInstl(spreadColumn)) = 1) Then
                        ''↑↑2014/08/21 Ⅰ.3.設計編集 ベース改修専用化_s) 酒井 CHG END
                        Dim subjectColumn As Integer = ConvSpreadColumnToAlInstl(spreadColumn)
                        anInstlColumns.Remove(subjectColumn, removeCount)
                        subject.RemoveColumnInInstl(subjectColumn, removeCount)
                    End If
                Else
                    Throw New NotSupportedException("メモ欄・INSTL品番欄以外の列削除は不可")
                End If
                subject.NotifyObservers()
            Finally
                IsSuspend_SpreadChangeEvent = False
            End Try
            'Next

        End Sub
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_s) (TES)張 CHG END

        ''' <summary>
        ''' INSTL列の全削除
        ''' </summary>
        ''' <param name="flag">バックアップに対しては０,そうでないなら１</param>
        ''' <remarks></remarks>
        Public Sub RemoveInstlColumnsAll(ByVal flag As Integer, Optional ByVal baseFlg As Boolean = False)
            '↓↓2014/10/23 酒井 ADD BEGIN
            'Public Sub RemoveInstlColumnsAll(ByVal flag As Integer)
            '↑↑2014/10/23 酒井 ADD END
            IsSuspend_SpreadChangeEvent = True




            '↓↓2014/10/23 酒井 ADD BEGIN
            'anInstlColumns.Remove(0, subject.GetInputInstlHinbanColumnIndexes.Count)
            If baseFlg Then
                'ベース情報は消さない
                '20141126 Tsunoda
                'ベース情報の判定が曖昧だった為ベース位置・数を明示的に取得する様変更を行なった
                Dim baseColEndIndex As Integer = 0

                For Each index As Integer In subject.GetInputInstlHinbanColumnIndexes
                    'フラグの存在する物がベース情報
                    If subject.BaseInstlFlg(index) = "1" Then
                        baseColEndIndex += 1
                    End If
                Next

                Dim cnt As Integer = subject.GetInputInstlHinbanColumnIndexes.Count
                For c As Integer = 0 To cnt - baseColEndIndex
                    anInstlColumns.Remove(baseColEndIndex, 1)
                Next

            Else
                anInstlColumns.Remove(0, subject.GetInputInstlHinbanColumnIndexes.Count)
            End If
            '↑↑2014/10/23 酒井 ADD END





            If flag = 0 Then
                ''構成側は消さない
                subject.RemoveColumnInInstlEventCopy(0, subject.GetInputInstlHinbanColumnIndexes.Count)
            Else
                '↓↓2014/10/23 酒井 ADD BEGIN
                ''構成側も消す'
                'subject.RemoveColumnInInstl(0, subject.GetInputInstlHinbanColumnIndexes.Count)
                If baseFlg Then
                    'ベース情報は消さない
                    '20141126 Tsunoda
                    'ベース情報の判定が曖昧だった為ベース位置・数を明示的に取得する様変更を行なった
                    Dim baseColEndIndex As Integer = 0
                    For Each index As Integer In subject.GetInputInstlHinbanColumnIndexes

                        If subject.BaseInstlFlg(index) = "1" Then
                            baseColEndIndex += 1
                        End If
                    Next

                    Dim cnt As Integer = subject.GetInputInstlHinbanColumnIndexes.Count

                    For c As Integer = 0 To cnt - baseColEndIndex
                        subject.RemoveColumnInInstl(baseColEndIndex, 1)
                    Next
                Else
                    '構成側も消す'
                    subject.RemoveColumnInInstl(0, subject.GetInputInstlHinbanColumnIndexes.Count)
                End If
                '↑↑2014/10/23 酒井 ADD END
            End If





            subject.NotifyObservers()
            IsSuspend_SpreadChangeEvent = False





            '↓↓2014/10/23 酒井 ADD BEGIN
            'subject.InsertColumnInInstl(0, subject.GetInputInstlHinbanColumnIndexes.Count)
            If baseFlg Then
                '元の処理は、RemoveAllしているので、Countは0。この処理の意図が不明。
            Else
                subject.InsertColumnInInstl(0, subject.GetInputInstlHinbanColumnIndexes.Count)
            End If
            '↑↑2014/10/23 酒井 ADD END
        End Sub

        ''' <summary>
        ''' E/Gメモ、T/Mメモのラベル取得
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="strLavelCode">ラベル識別コード</param>
        ''' <remarks></remarks>
        Private Function getEventMemo(ByVal shisakuEventCode As String, ByVal strLavelCode As String) As String

            ' イベント情報取得
            Dim eventVO As New List(Of TShisakuEventVo)
            Dim eventInfo = New ExportShisakuEventInfoExcelDaoImpl
            eventVO = eventInfo.GetEvent(shisakuEventCode)
            '改訂№を文字タイプへ変更
            Dim KaiteiNo As String = CStr(eventVO.Item(0).SeisakuichiranHakouNoKai)
            If StringUtil.Equals(KaiteiNo.Length, 1) Then
                KaiteiNo = KaiteiNo.PadLeft(2, "0"c)
            End If
            '製作一覧HD情報取得
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
            tSeisakuHakouHdVo = _
                    getSeisakuIchiranHd.GetSeisakuIchiranHd(eventVO.Item(0).SeisakuichiranHakouNo, KaiteiNo)
            'メモヘッダー情報取得
            '   製作一覧ヘッダー情報からメモヘッダーを取得
            If StringUtil.IsNotEmpty(tSeisakuHakouHdVo) Then
                If StringUtil.Equals(strLavelCode, _
                                     TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO1) Then
                    Return tSeisakuHakouHdVo.KanseiEgMemo1
                End If
                If StringUtil.Equals(strLavelCode, _
                                     TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO2) Then
                    Return tSeisakuHakouHdVo.KanseiEgMemo2
                End If
                If StringUtil.Equals(strLavelCode, _
                                     TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO1) Then
                    Return tSeisakuHakouHdVo.KanseiTmMemo1
                End If
                If StringUtil.Equals(strLavelCode, _
                                     TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO2) Then
                    Return tSeisakuHakouHdVo.KanseiTmMemo2
                End If
            End If
            Return ""

        End Function


        ''' <summary>
        ''' イベント品番コピー
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <remarks></remarks>
        Public Sub EventCopy(ByVal shisakuEventCode As String)
            '受け取ったイベントコードを元にALの中身を差し替える'
            'イベントコードとブロックNoを元に設計ブロックINSTL情報を取得する'
            Dim selDao As Selector.Dao.DispEventBuhinCopySelectorDao = New Selector.Dao.DispEventBuhinCopySelectorDaoImpl
            Dim sbiList As New List(Of TShisakuSekkeiBlockInstlVo)

            '設計ブロックINSTL情報の取得'
            sbiList = selDao.FindBySekkeiBlockInstl(shisakuEventCode, subject.BlockNo)

            For index As Integer = 0 To sbiList.Count - 1

            Next

            subject.NotifyObservers()

        End Sub

        '' 初期表示処理が終わったか？
        Private _UpdateFlag As Boolean = False

        ''' <summary>
        ''' アップデートフラグ
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UpdateFlag() As Boolean
            Get
                Return _UpdateFlag
            End Get
            Set(ByVal value As Boolean)
                _UpdateFlag = value
            End Set
        End Property

        ''' <summary>
        ''' 文字列チェック
        ''' </summary>
        ''' <param name="value">対象文字列</param>
        ''' <returns>ALL半角ならTrue</returns>
        ''' <remarks></remarks>
        Private Function SpellCheck(ByVal value As String) As Boolean
            '文字列の長さ
            Dim valueLength As Integer = value.Length
            Dim Enc As Encoding = Encoding.GetEncoding("Shift_JIS")

            For i As Integer = 0 To valueLength - 1
                Dim c As String = Mid(value, i + 1, 1)

                '半角か全角かチェック'
                If Not StringUtil.IsEmpty(c) Then
                    If Enc.GetByteCount(c) = 1 Then

                    Else
                        Return False
                    End If
                End If
            Next
            Return True
        End Function

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_o) (TES)張 ADD BEGIN
        Public Sub BaseInfoColumnVisible()
            For Each index As Integer In subject.GetInputInstlHinbanColumnIndexes()
                If subject.BaseInstlFlg(index) = 1 Then
                    Dim spreadColumn As Integer = ConvAlInstlToSpreadColumn(index)
                    sheet.Columns(spreadColumn).Visible = True
                End If
            Next
        End Sub
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_o) (TES)張 ADD END

        ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_p) (TES)張 ADD BEGIN
        Public Sub BaseInfoColumnDisable()
            For Each index As Integer In subject.GetInputInstlHinbanColumnIndexes()
                If subject.BaseInstlFlg(index) = 1 Then
                    Dim spreadColumn As Integer = ConvAlInstlToSpreadColumn(index)
                    sheet.Columns(spreadColumn).Visible = False
                End If
            Next
        End Sub
        ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_p) (TES)張 ADD END

        ''↓↓2014/08/14 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 ADD BEGIN
        Public Sub CheckBoxColumnDisable()
            sheet.Columns(TAG_CHECK_FLAG).Visible = False
        End Sub

        Public Sub CheckBoxColumnVisible()
            sheet.Columns(TAG_CHECK_FLAG).Visible = True
        End Sub

        Public Sub InstlColumnDisable()
            For columnIndex As Integer = 0 To anInstlColumns.Count - 1
                sheet.Columns(ConvAlInstlToSpreadColumn(columnIndex)).Visible = False
            Next
        End Sub

        Public Sub setTitleRowSpan()
            For rowIndex As Integer = 0 To INSTL_HINBAN_KBN_ROW_INDEX
                For columnIndex As Integer = 0 To sheet.ColumnCount - 1
                    sheet.Cells(rowIndex, columnIndex).RowSpan = 1
                Next
                If rowIndex <> EVENT_TITLE_ROW Then
                    sheet.Rows(rowIndex).Visible = False
                End If
            Next
        End Sub

        Public Sub setRowColor()
            For rowIndex As Integer = 0 To sheet.RowCount - 1
                If rowIndex Mod 2 = 0 Then
                    sheet.Rows(rowIndex).BackColor = Color.LightBlue
                Else
                    sheet.Rows(rowIndex).BackColor = Color.White
                End If
                ''↓↓2014/09/04 Ⅰ.3.設計編集 ベース車改修専用化_bz) 酒井 ADD BEGIN
                sheet.Rows(rowIndex).ForeColor = Color.Black
                ''↑↑2014/09/04 Ⅰ.3.設計編集 ベース車改修専用化_bz) 酒井 ADD END
            Next
        End Sub
        ''↑↑2014/08/14 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 ADD END

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
        ''' <remarks></remarks>
        Public Sub ResetFilter()

            Dim startRow As Integer = 4

            '基本情報列タイトル色戻し
            For i As Integer = 0 To sheet.ColumnCount - 1
                Dim objFont As System.Drawing.Font = sheet.Cells(INSTL_HINBAN_ROW_INDEX, i).Font

                '太字にされているセルは編集済なので色は戻さない
                If Not objFont Is Nothing AndAlso objFont.Bold = True Then
                    Continue For
                End If

                '色を戻す
                sheet.Cells(EVENT_TITLE_MIDASHI_ROW, i, INSTL_HINBAN_ROW_INDEX, i).ForeColor = Nothing

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
            Dim startRow As Integer = 5

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
            sheet.Cells(EVENT_TITLE_MIDASHI_ROW, colNo, INSTL_HINBAN_ROW_INDEX, colNo).ForeColor = Color.Blue

            For i As Integer = startRow To sheet.Rows.Count - 1

                Dim findFlag As Boolean = False

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