Imports FarPoint.Win.Spread.CellType
Imports FarPoint.Win.Spread
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Ui.Spd

Namespace ShisakuBuhinEditBlock.Ui
    ''' <summary>
    ''' 試作部品編集・改訂編集（ブロック）画面で使用するSpread関係のユーティリティ集
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BlockSpreadUtil
        ''' <summary>スプレッドの初期表示行</summary>
        Public Const SPREAD_DEFAULT_ROW_COUNT As Integer = 100
        ''' <summary>
        ''' 試作部品編集・改訂編集画面で使用するTextCellTypeの初期設定を行う
        ''' </summary>
        ''' <param name="aCellType">設定先のCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub SettingDefaultProperty(ByVal aCellType As TextCellType)
            aCellType.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.Arrows
            '' 小文字を大文字にする
            aCellType.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            aCellType.CharacterSet = CharacterSet.Ascii
        End Sub

        ''' <summary>
        ''' 試作部品編集・改訂編集画面で使用するDateTimeCellTypeの初期設定を行う
        ''' </summary>
        ''' <param name="aCellType">設定先のCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub SettingDefaultProperty(ByVal aCellType As DateTimeCellType)
            aCellType.DropDownButton = True
        End Sub

        ''' <summary>
        ''' 試作部品編集・改訂編集画面で使用するCheckBoxCellTypeの初期設定を行う
        ''' </summary>
        ''' <param name="aCellType">設定先のCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub SettingDefaultProperty(ByVal aCellType As CheckBoxCellType)
            '' nop 今は無し
        End Sub

        ''' <summary>
        ''' 試作部品編集・改訂編集画面のSpreadのタイトル行数を返す
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <returns>タイトル行数</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTitleRows(ByVal spreadSheet As SheetView) As Integer
            If spreadSheet.RowCount > 0 Then
                Return spreadSheet.Cells(0, 0).RowSpan
            Else : Return 0
            End If

        End Function

        ''' <summary>
        ''' 列全体にCellTypeを設定する
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="tag">設定先の列タグ名</param>
        ''' <param name="cellType">設定するCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub BindCellTypeToColumn(ByVal spreadSheet As SheetView, ByVal tag As String, ByVal cellType As ICellType)

            BindCellTypeToColumn(spreadSheet, spreadSheet.Columns(tag).Index, cellType)
        End Sub
        ''' <summary>
        ''' 列全体にCellTypeを設定する
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="columnNo">設定先の列No</param>
        ''' <param name="cellType">設定するCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub BindCellTypeToColumn(ByVal spreadSheet As SheetView, ByVal columnNo As Integer, ByVal cellType As ICellType)
            Dim cells As New List(Of ICellType)
            Dim titleRows As Integer = GetTitleRows(spreadSheet)
            For row As Integer = 0 To titleRows - 1
                '' GetStyleInfo() : 列や行に設定されたCellTypeでも参照出来る
                cells.Add(spreadSheet.GetStyleInfo(row, columnNo).CellType)
            Next

            spreadSheet.Columns(columnNo).CellType = cellType

            For row As Integer = 0 To cells.Count - 1
                If cells(row) Is Nothing Then
                    spreadSheet.Cells(row, columnNo).CellType = New TextCellType
                Else
                    spreadSheet.Cells(row, columnNo).CellType = cells(row)
                End If
            Next
        End Sub
        ''' <summary>
        ''' 列全体に新しいコンボボックスを設定する
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="tag">設定先の列タグ名</param>
        ''' <param name="labelValues">ラベルと値の一覧</param>
        ''' <param name="containsBlank">空白を含める場合、true</param>
        ''' <remarks></remarks>
        Public Shared Sub BindNewComboBoxToColumn(ByVal spreadSheet As SheetView, ByVal tag As String, ByVal labelValues As List(Of LabelValueVo), Optional ByVal containsBlank As Boolean = True)

            BindCellTypeToColumn(spreadSheet, tag, SpreadUtil.CreateComboBoxCellType(labelValues, containsBlank))
        End Sub

        ''' <summary>
        ''' 列全体に新しいチェックボックスを設定する
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="tag">設定先の列タグ名</param>
        ''' <param name="textTrue">選択時の文言</param>
        ''' <param name="textFalse">未選択時の文言</param>
        ''' <remarks></remarks>
        Public Shared Sub BindNewCheckBoxToColumn(ByVal spreadSheet As SheetView, ByVal tag As String, ByVal textTrue As String, ByVal textFalse As String)
            BindCellTypeToColumn(spreadSheet, tag, SpreadUtil.CreateCheckBoxCellType(textTrue, textFalse))
        End Sub

        ''' <summary>
        ''' 試作部品編集・改訂編集画面のSpread初期化
        ''' </summary>
        ''' <param name="aSpread">対象Spread</param>
        ''' <remarks></remarks>
        Public Shared Sub InitializeFrm9(ByVal aSpread As FpSpread)

            SpreadUtil.Initialize(aSpread)
            '
            Dim my_font As New Font("MS Gothic", 9)
            aSpread.ActiveSheet.Columns.Get(1).Font = my_font

            If aSpread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Dim sheet As SheetView = aSpread.Sheets(0)

            sheet.RowCount = GetTitleRows(sheet)
            sheet.RowCount = GetTitleRows(sheet) + SPREAD_DEFAULT_ROW_COUNT
        End Sub

        ''' <summary>
        ''' 試作部品編集・改訂編集画面のSpread行数再設定
        ''' </summary>
        ''' <param name="aSpread">対象Spread</param>
        ''' <param name="rowsCount">行数</param>
        ''' <remarks></remarks>
        Public Shared Sub setRowCount(ByVal aSpread As FpSpread, ByVal rowsCount As Integer)
            Dim sheet As SheetView = aSpread.Sheets(0)
            sheet.RowCount = rowsCount
        End Sub
    End Class
End Namespace

