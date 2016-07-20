Imports ShisakuCommon.Ui.Spd.Action
Imports FarPoint.Win.Spread.CellType
Imports FarPoint.Win.Spread.Model
Imports ShisakuCommon.Util.LabelValue
Imports FarPoint.Win.Spread
Imports ShisakuCommon.Ui.Spd.Action.Undo

Namespace Ui.Spd


    ''' <summary>
    ''' どの画面からでも使用できるSpread関連のユーティリティ集
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpreadUtil
        'Spreadのフィルタボタンをクリックすると、(NonBlanks)と表示されているが日本語表記にする
        Public Const FILTER_NON_BLANKS_STRING As String = "(空白以外のセル)"
        'Spreadのフィルタボタンをクリックすると、(All)と表示されているが日本語表記にする
        Public Const FILTER_ALL_STRING As String = "(すべて)"
        'Spreadのフィルタボタンをクリックすると、(Blanks)と表示されているが日本語表記にする
        Public Const FILTER_BLANKS_STRING As String = "(空白)"
        ''' <summary>
        ''' セルValueをコンボボックスの値か、無ければnullにする
        ''' </summary>
        ''' <param name="spreadSheet"></param>
        ''' <param name="row"></param>
        ''' <param name="tag"></param>
        ''' <remarks></remarks>
        Public Shared Sub ApplyComboBoxValueToCell(ByVal spreadSheet As SheetView, ByVal row As Integer, ByVal tag As String)
            Dim columnIndex As Integer = spreadSheet.Columns(tag).Index
            Dim value As Object = spreadSheet.Cells(row, columnIndex).Value
            If value Is Nothing Then
                Return
            End If
            If TypeOf spreadSheet.Cells(row, columnIndex).CellType Is ComboBoxCellType Then
                Dim cellType As ComboBoxCellType = CType(spreadSheet.Cells(row, columnIndex).CellType, ComboBoxCellType)
                For Each data As String In cellType.ItemData
                    If data IsNot Nothing AndAlso data.Equals(value) Then
                        Return
                    End If
                Next
            End If
            spreadSheet.Cells(row, columnIndex).Value = Nothing
        End Sub

        ''' <summary>
        ''' ComboBoxのCellTypeにラベルと値の一覧を設定する
        ''' </summary>
        ''' <param name="aComboBoxCellType">ComboBoxのCellType</param>
        ''' <param name="labelValues">ラベルと値の一覧</param>
        ''' <param name="containsBlank">ブランク行を表示する場合、true（初期値:true）</param>
        ''' <remarks></remarks>
        Public Shared Sub BindLabelValuesToComboBoxCellType(ByVal aComboBoxCellType As ComboBoxCellType, ByVal labelValues As List(Of LabelValueVo), Optional ByVal containsBlank As Boolean = True)
            If labelValues Is Nothing OrElse labelValues.Count = 0 Then
                Return
            End If
            Dim labels As New List(Of String)
            Dim values As New List(Of String)
            If containsBlank Then
                labels.Add(String.Empty)
                values.Add(Nothing)
            End If
            For Each vo As LabelValueVo In labelValues
                labels.Add(vo.Label)
                values.Add(vo.Value)
            Next
            aComboBoxCellType.Items = labels.ToArray
            aComboBoxCellType.ItemData = values.ToArray
            aComboBoxCellType.EditorValue = EditorValue.ItemData
        End Sub

        ''' <summary>
        ''' セル用コンボボックスを作成して返す
        ''' </summary>
        ''' <param name="labelValues">コンボボックス内容</param>
        ''' <param name="containsBlank">ブランク行を表示する場合、true（初期値:true）</param>
        ''' <returns>セル用コンボボックス</returns>
        ''' <remarks></remarks>
        Public Shared Function CreateComboBoxCellType(ByVal labelValues As List(Of LabelValueVo), Optional ByVal containsBlank As Boolean = True) As ComboBoxCellType
            Dim aCellType As New ComboBoxCellType
            BindLabelValuesToComboBoxCellType(aCellType, labelValues, containsBlank)
            Return aCellType
        End Function

        ''' <summary>
        ''' セルに新しいコンボボックスを設定する
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="row">設定先の行No</param>
        ''' <param name="tag">設定先の列タグ名</param>
        ''' <param name="labelValues">ラベルと値の一覧</param>
        ''' <param name="containsBlank">空白を含める場合、true</param>
        ''' <remarks></remarks>
        Public Shared Sub BindNewComboBoxToCell(ByVal spreadSheet As SheetView, ByVal row As Integer, ByVal tag As String, ByVal labelValues As List(Of LabelValueVo), Optional ByVal containsBlank As Boolean = True)
            BindCellTypeToCell(spreadSheet, row, tag, CreateComboBoxCellType(labelValues, containsBlank))
        End Sub

        ''' <summary>
        ''' セルにCellTypeを設定する
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="row">設定先の行No</param>
        ''' <param name="tag">設定先の列タグ名</param>
        ''' <param name="aCellType">設定するCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub BindCellTypeToCell(ByVal spreadSheet As SheetView, ByVal row As Integer, ByVal tag As String, ByVal aCellType As ICellType)
            Dim columnIndex As Integer = spreadSheet.Columns(tag).Index
            spreadSheet.Cells(row, columnIndex).CellType = aCellType
        End Sub

        ''' <summary>
        ''' セルに設定したCellTypeを除去する
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="row">設定先の行No</param>
        ''' <param name="tag">設定先の列タグ名</param>
        ''' <remarks></remarks>
        Public Shared Sub UnbindCellTypeToCell(ByVal spreadSheet As SheetView, ByVal row As Integer, ByVal tag As String)
            BindCellTypeToCell(spreadSheet, row, tag, Nothing)
        End Sub


        Private Shared EMPTY_COMBO_BOX_CELL_TYPE As ComboBoxCellType = New ComboBoxCellType
        ''' <summary>
        ''' Emptyのコンボボックスセルにする
        ''' </summary>
        ''' <param name="spreadSheet">Spreadシート</param>
        ''' <param name="row">行</param>
        ''' <param name="tag">列Tag</param>
        ''' <remarks></remarks>
        Public Shared Sub BindEmptyComboBoxToCell(ByVal spreadSheet As SheetView, ByVal row As Integer, ByVal tag As String)
            BindCellTypeToCell(spreadSheet, row, tag, EMPTY_COMBO_BOX_CELL_TYPE)
        End Sub

        ''' <summary>
        ''' CheckBoxのCellTypeに選択時/未選択時の文言を設定する
        ''' </summary>
        ''' <param name="aCellType">設定先のCellType</param>
        ''' <param name="textTrue">選択時の文言</param>
        ''' <param name="textFalse">未選択時の文言</param>
        ''' <remarks></remarks>
        Public Shared Sub BindTextToCheckBoxCellType(ByVal aCellType As CheckBoxCellType, ByVal textTrue As String, ByVal textFalse As String)
            aCellType.ThreeState = False
            aCellType.TextFalse = textFalse
            aCellType.TextTrue = textTrue
        End Sub
        ''' <summary>
        ''' CheckBoxのCellTypeを作成して返す
        ''' </summary>
        ''' <param name="textTrue">選択時の文言</param>
        ''' <param name="textFalse">未選択時の文言</param>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function CreateCheckBoxCellType(ByVal textTrue As String, ByVal textFalse As String) As CheckBoxCellType
            Dim aCellType As New CheckBoxCellType
            BindTextToCheckBoxCellType(aCellType, textTrue, textFalse)
            Return aCellType
        End Function

        ''' <summary>
        ''' セルに新しいCheckBoxを設定する
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="row">設定先の行No</param>
        ''' <param name="tag">設定先の列タグ名</param>
        ''' <param name="textTrue">選択時の文言</param>
        ''' <param name="textFalse">未選択時の文言</param>
        ''' <remarks></remarks>
        Public Shared Sub BindNewCheckBoxToCell(ByVal spreadSheet As SheetView, ByVal row As Integer, ByVal tag As String, ByVal textTrue As String, ByVal textFalse As String)
            Dim aCellType As CheckBoxCellType = CreateCheckBoxCellType(textTrue, textFalse)
            SpreadUtil.BindCellTypeToCell(spreadSheet, row, tag, aCellType)
        End Sub

        ''' <summary>
        ''' 手入力以外にも、CTRL+Vや、CTRL+Zで、変更された時に発生する SheetDataModelEventHandler を登録する
        ''' </summary>
        ''' <param name="aSpread">Spreadインスタンス</param>
        ''' <param name="ChangedEvent">SheetDataModelEventHandler</param>
        ''' <remarks></remarks>
        Public Shared Sub AddHandlerSheetDataModelChanged(ByVal aSpread As FarPoint.Win.Spread.FpSpread, ByVal ChangedEvent As FarPoint.Win.Spread.Model.SheetDataModelEventHandler)
            If aSpread.Sheets.Count = 0 Then
                Throw New ArgumentException("Spreadにシートが無いとイベントの追加は出来ません")
            End If
            AddHandlerSheetDataModelChanged(aSpread.Sheets(0), ChangedEvent)
        End Sub
        ''' <summary>
        ''' 手入力以外にも、CTRL+Vや、CTRL+Zで、変更された時に発生する SheetDataModelEventHandler を登録する
        ''' </summary>
        ''' <param name="aSheet">Sheetインスタンス</param>
        ''' <param name="ChangedEvent">SheetDataModelEventHandler</param>
        ''' <remarks></remarks>
        Public Shared Sub AddHandlerSheetDataModelChanged(ByVal aSheet As FarPoint.Win.Spread.SheetView, ByVal ChangedEvent As FarPoint.Win.Spread.Model.SheetDataModelEventHandler)
            If Not TypeOf aSheet.Models.Data Is FarPoint.Win.Spread.Model.DefaultSheetDataModel Then
                Throw New NotSupportedException(aSheet.Models.Data.GetType.Name & " には対応出来ません")
            End If
            Dim dataModel As FarPoint.Win.Spread.Model.DefaultSheetDataModel = CType(aSheet.Models.Data, FarPoint.Win.Spread.Model.DefaultSheetDataModel)
            AddHandler dataModel.Changed, ChangedEvent
        End Sub
        ''' <summary>
        ''' 手入力以外にも、CTRL+Vや、CTRL+Zで、変更された時に発生する SheetDataModelEventHandler を登録する
        ''' </summary>
        ''' <param name="aSpread">Spreadインスタンス</param>
        ''' <param name="ChangedEvent">SheetDataModelEventHandler</param>
        ''' <remarks></remarks>
        Public Shared Sub RemoveHandlerSheetDataModelChanged(ByVal aSpread As FarPoint.Win.Spread.FpSpread, ByVal ChangedEvent As FarPoint.Win.Spread.Model.SheetDataModelEventHandler)
            If aSpread.Sheets.Count = 0 Then
                Throw New ArgumentException("Spreadにシートが無いとイベントの除去は出来ません")
            End If
            RemoveHandlerSheetDataModelChanged(aSpread.Sheets(0), ChangedEvent)
        End Sub
        ''' <summary>
        ''' 手入力以外にも、CTRL+Vや、CTRL+Zで、変更された時に発生する SheetDataModelEventHandler を登録する
        ''' </summary>
        ''' <param name="aSheet">Sheetインスタンス</param>
        ''' <param name="ChangedEvent">SheetDataModelEventHandler</param>
        ''' <remarks></remarks>
        Public Shared Sub RemoveHandlerSheetDataModelChanged(ByVal aSheet As FarPoint.Win.Spread.SheetView, ByVal ChangedEvent As FarPoint.Win.Spread.Model.SheetDataModelEventHandler)
            If Not TypeOf aSheet.Models.Data Is FarPoint.Win.Spread.Model.DefaultSheetDataModel Then
                Throw New NotSupportedException(aSheet.Models.Data.GetType.Name & " には対応出来ません")
            End If
            Dim dataModel As FarPoint.Win.Spread.Model.DefaultSheetDataModel = CType(aSheet.Models.Data, FarPoint.Win.Spread.Model.DefaultSheetDataModel)
            RemoveHandler dataModel.Changed, ChangedEvent
        End Sub
        ''' <summary>
        ''' CTRL+Xの動作を、「書式も切り取る」から「データだけ切り取る」にする
        ''' </summary>
        ''' <param name="aSpread">設定するSpread</param>
        ''' <remarks></remarks>
        Public Shared Sub BindKeyCtrlXIsCutDataOnly(ByVal aSpread As FarPoint.Win.Spread.FpSpread)
            Dim im As FarPoint.Win.Spread.InputMap = GetCellCursorModeInputMap(aSpread)
            '' CTRL+X は書式を切り取らないようにする
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.X, Keys.Control), FarPoint.Win.Spread.SpreadActions.ClipboardCutDataOnly)
        End Sub
        ''' <summary>
        ''' CTRL+Vの動作を、「書式も貼り付ける」から「データだけ貼り付ける」にする
        ''' </summary>
        ''' <param name="aSpread">設定するSpread</param>
        ''' <remarks></remarks>
        Public Shared Sub BindKeyCtrlVIsPasteDataOnly(ByVal aSpread As FarPoint.Win.Spread.FpSpread)
            Dim im As FarPoint.Win.Spread.InputMap = GetCellCursorModeInputMap(aSpread)
            '' CTRL+V は値だけを張り付ける
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.V, Keys.Control), FarPoint.Win.Spread.SpreadActions.ClipboardPasteValues)
        End Sub
        ''' <summary>
        ''' F2キーの動作を、「削除して編集」から「編集」にする
        ''' </summary>
        ''' <param name="aSpread">設定するSpread</param>
        ''' <remarks></remarks>
        Public Shared Sub BindKeyF2IsEdit(ByVal aSpread As FarPoint.Win.Spread.FpSpread)
            Dim im As FarPoint.Win.Spread.InputMap = GetCellCursorModeInputMap(aSpread)
            '' F2 で編集モード（元々はClearCell）
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
            Dim im2 As FarPoint.Win.Spread.InputMap = GetEditModeInputMap(aSpread)
            '' F2 で編集モード終了（元々はClearCell）
            im2.Put(New FarPoint.Win.Spread.Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        End Sub
        ''' <summary>
        ''' F4キーの動作を、「編集」から「ComboBoxリストを表示」にする
        ''' </summary>
        ''' <param name="aSpread"></param>
        ''' <remarks></remarks>
        Public Shared Sub BindKeyF4IsComboShowList(ByVal aSpread As FarPoint.Win.Spread.FpSpread)
            Dim im As FarPoint.Win.Spread.InputMap = GetCellCursorModeInputMap(aSpread)
            '' F4 でリスト表示（元々はShowSubEditor）
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        End Sub
        ''' <summary>
        ''' Enterキーの動作を、「編集」から「次行へ移動」にする
        ''' </summary>
        ''' <param name="aSpread"></param>
        ''' <remarks></remarks>
        Public Shared Sub BindKeyEnterIsNextRow(ByVal aSpread As FarPoint.Win.Spread.FpSpread)
            Dim im As FarPoint.Win.Spread.InputMap = GetCellCursorModeInputMap(aSpread)
            '' Enter で次行へ移動（元々はStartEditing）
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
            Dim im2 As FarPoint.Win.Spread.InputMap = GetEditModeInputMap(aSpread)
            '' Enter で次行へ移動（元々はStopEditing）
            im2.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        End Sub

        ''' <summary>
        ''' Shift + Enterキーの動作を、「前行へ移動」にする
        ''' </summary>
        ''' <param name="aSpread"></param>
        ''' <remarks></remarks>
        Public Shared Sub BindKeyShiftEnterIsPreviousRow(ByVal aSpread As FarPoint.Win.Spread.FpSpread)
            Dim im As FarPoint.Win.Spread.InputMap = GetCellCursorModeInputMap(aSpread)
            '' Shift + Enter で前行へ移動（元々はStartEditing）
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
            Dim im2 As FarPoint.Win.Spread.InputMap = GetEditModeInputMap(aSpread)
            '' Shift + Enter で前行へ移動（元々はStopEditing）
            im2.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        End Sub

        ''' <summary>
        ''' セルカーソル時のInputMapを返す
        ''' </summary>
        ''' <param name="aSpread">Spread</param>
        ''' <returns>InputMap</returns>
        ''' <remarks></remarks>
        Private Shared Function GetCellCursorModeInputMap(ByVal aSpread As FpSpread) As InputMap

            Return aSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
        End Function

        ''' <summary>
        ''' 編集モード時のInputMapを返す
        ''' </summary>
        ''' <param name="aSpread">Spread</param>
        ''' <returns>InputMap</returns>
        ''' <remarks></remarks>
        Private Shared Function GetEditModeInputMap(ByVal aSpread As FpSpread) As InputMap

            Return aSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        End Function

        ''' <summary>
        ''' 試作システムSpread全体に共通する設定を適用する
        ''' </summary>
        ''' <param name="aSpread">試作システムのSpread</param>
        ''' <remarks></remarks>
        Public Shared Sub Initialize(ByVal aSpread As FpSpread)
            If aSpread.Sheets.Count = 0 Then
                Throw New ArgumentException("シートを持たないSpreadです.")
            End If
            Dim sheet As SheetView = aSpread.Sheets(0)

            ''文字再入力Clear,DoubleClickすれば、文字最後からの入力になる
            aSpread.EditModeReplace = True

            ''セルカーソルの色と動き
            aSpread.FocusRenderer = New FarPoint.Win.Spread.MarqueeFocusIndicatorRenderer(Color.Blue, 1)

            ''カレントセルだけコンボボックスの下矢印を表示する
            aSpread.ButtonDrawMode = ButtonDrawModes.CurrentCell

            '' XP風の描画
            aSpread.VisualStyles = FarPoint.Win.VisualStyles.On

            '' IME不可
            aSpread.ImeMode = Windows.Forms.ImeMode.Disable

            '' スクロールバーを移動中（ドラッグ中）にシートも移動する表示を行う
            aSpread.ScrollBarTrackPolicy = ScrollBarTrackPolicy.Both

            '' キーマップの変更
            BindKeyCtrlXIsCutDataOnly(aSpread)
            BindKeyCtrlVIsPasteDataOnly(aSpread)
            BindKeyF2IsEdit(aSpread)
            BindKeyF4IsComboShowList(aSpread)
            BindKeyEnterIsNextRow(aSpread)
            BindKeyShiftEnterIsPreviousRow(aSpread)

            BindKeyDeleteIsDataClear(aSpread)

            BindKeyCtrlUpArrowIsNextDataCell(aSpread)
            BindKeyCtrlDownArrowIsNextDataCell(aSpread)
            BindKeyCtrlLeftArrowIsNextDataCell(aSpread)
            BindKeyCtrlRightArrowIsNextDataCell(aSpread)

            ''Spreadのフィルタボタンをクリックすると(空白)、(すべて)、(空白以外のセル)と日本語表記
            SetFilterString(aSpread)

            '' 行の高さ変更可
            sheet.SetRowSizeable(sheet.RowCount, True)
            '' 列の幅変更可
            sheet.SetColumnSizeable(sheet.ColumnCount, True)

        End Sub
        ''' <summary>
        ''' 全ての列をロックする
        ''' </summary>
        ''' <param name="sheet">ロックするシート</param>
        ''' <remarks></remarks>
        Public Shared Sub LockAllColumns(ByVal sheet As SheetView)
            For i As Integer = 0 To sheet.ColumnCount - 1
                sheet.Columns(i).Locked = True
            Next
        End Sub
        ''' <summary>
        ''' 全ての列をアンロックする
        ''' </summary>
        ''' <param name="sheet">アンロックするシート</param>
        ''' <remarks></remarks>
        Public Shared Sub UnlockAllColumns(ByVal sheet As SheetView)
            For i As Integer = 0 To sheet.ColumnCount - 1
                sheet.Columns(i).Locked = False
            Next
        End Sub

        ''' <summary>
        '''  'Spreadのフィルタボタンをクリックすると、(All)、(Blanks)、(NonBlanks)と表示されているが日本語表記にする
        ''' </summary>
        ''' <param name="aSpread">試作システムのSpread</param>
        ''' <remarks></remarks>
        Public Shared Sub SetFilterString(ByVal aSpread As FarPoint.Win.Spread.FpSpread)
            If Not aSpread.ActiveSheet.RowFilter Is Nothing Then
                aSpread.ActiveSheet.RowFilter.AllString = FILTER_ALL_STRING
                aSpread.ActiveSheet.RowFilter.BlanksString = FILTER_BLANKS_STRING
                aSpread.ActiveSheet.RowFilter.NonBlanksString = FILTER_NON_BLANKS_STRING
            End If
        End Sub

        ''' <summary>
        '''Spreadのシートにタイトルをもつ場合のタイトル行数を返す
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <returns>タイトル行数</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTitleRowsIn(ByVal spreadSheet As SheetView) As Integer
            If 1 < spreadSheet.StartingRowNumber Then
                Throw New InvalidOperationException("想定外の値です. sheet.StartingRowNumber=" & CStr(spreadSheet.StartingRowNumber))
            End If
            Return 1 - spreadSheet.StartingRowNumber
        End Function

        ''' <summary>
        ''' Deleteの動作を、「データだけ削除」にする
        ''' </summary>
        ''' <param name="aSpread">設定するSpread</param>
        ''' <remarks></remarks>
        Public Shared Sub BindKeyDeleteIsDataClear(ByVal aSpread As FarPoint.Win.Spread.FpSpread)
            ' Original の UndoAction を定義
            Const DELETE_DATA_ONLY As String = "DeleteDataOnly"
            aSpread.GetActionMap.Put(DELETE_DATA_ONLY, New DeleteDataOnlyUndoAction)

            Dim im As FarPoint.Win.Spread.InputMap = SpreadUtil.GetCellCursorModeInputMap(aSpread)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Delete, Keys.None), DELETE_DATA_ONLY)
        End Sub

        ''' <summary>
        ''' CTRL+[↑]の動作を、「ExcelのCTRL+[↑]」にする
        ''' </summary>
        ''' <param name="aSpread">設定するSpread</param>
        ''' <remarks></remarks>
        Public Shared Sub BindKeyCtrlUpArrowIsNextDataCell(ByVal aSpread As FarPoint.Win.Spread.FpSpread)
            ' Original の Action を定義
            Const UP_DATA_CELL As String = "UpDataCell"
            aSpread.GetActionMap.Put(UP_DATA_CELL, New ExcelCtrlUpArrowAction)

            Dim im As FarPoint.Win.Spread.InputMap = SpreadUtil.GetCellCursorModeInputMap(aSpread)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Up, Keys.Control), UP_DATA_CELL)
        End Sub

        ''' <summary>
        ''' CTRL+[↓]の動作を、「ExcelのCTRL+[↓]」にする
        ''' </summary>
        ''' <param name="aSpread">設定するSpread</param>
        ''' <remarks></remarks>
        Public Shared Sub BindKeyCtrlDownArrowIsNextDataCell(ByVal aSpread As FarPoint.Win.Spread.FpSpread)
            ' Original の Action を定義
            Const DOWN_DATA_CELL As String = "DownDataCell"
            aSpread.GetActionMap.Put(DOWN_DATA_CELL, New ExcelCtrlDownArrowAction)

            Dim im As FarPoint.Win.Spread.InputMap = SpreadUtil.GetCellCursorModeInputMap(aSpread)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Down, Keys.Control), DOWN_DATA_CELL)
        End Sub

        ''' <summary>
        ''' CTRL+[←]の動作を、「ExcelのCTRL+[←]」にする
        ''' </summary>
        ''' <param name="aSpread">設定するSpread</param>
        ''' <remarks></remarks>
        Public Shared Sub BindKeyCtrlLeftArrowIsNextDataCell(ByVal aSpread As FarPoint.Win.Spread.FpSpread)
            ' Original の Action を定義
            Const PREVIOUS_DATA_CELL As String = "PreviousDataCell"
            aSpread.GetActionMap.Put(PREVIOUS_DATA_CELL, New ExcelCtrlLeftArrowAction)

            Dim im As FarPoint.Win.Spread.InputMap = SpreadUtil.GetCellCursorModeInputMap(aSpread)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Left, Keys.Control), PREVIOUS_DATA_CELL)
        End Sub

        ''' <summary>
        ''' CTRL+[→]の動作を、「ExcelのCTRL+[→]」にする
        ''' </summary>
        ''' <param name="aSpread">設定するSpread</param>
        ''' <remarks></remarks>
        Public Shared Sub BindKeyCtrlRightArrowIsNextDataCell(ByVal aSpread As FarPoint.Win.Spread.FpSpread)
            ' Original の Action を定義
            Const NEXT_DATA_CELL As String = "NextDataCell"
            aSpread.GetActionMap.Put(NEXT_DATA_CELL, New ExcelCtrlRightArrowAction)

            Dim im As FarPoint.Win.Spread.InputMap = SpreadUtil.GetCellCursorModeInputMap(aSpread)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Right, Keys.Control), NEXT_DATA_CELL)
        End Sub

        ''' <summary>
        ''' 列全体にCellTypeを設定する(先頭行の列タイトルを考慮)
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="tag">設定先の列タグ名</param>
        ''' <param name="cellType">設定するCellType</param>
        ''' <param name="withoutTitleRow">列タイトルが先頭行にあり、先頭行のCellType設定を除外する場合、true</param>
        ''' <remarks></remarks>
        Public Shared Sub BindCellTypeToColumn(ByVal spreadSheet As SheetView, ByVal tag As String, ByVal cellType As ICellType, Optional ByVal withoutTitleRow As Boolean = True)

            BindCellTypeToColumn(spreadSheet, spreadSheet.Columns(tag).Index, cellType, withoutTitleRow)
        End Sub

        ''' <summary>
        ''' 列全体にCellTypeを設定する(先頭行の列タイトルを考慮)
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="columnNo">設定先の列No</param>
        ''' <param name="cellType">設定するCellType</param>
        ''' <param name="withoutTitleRow">列タイトルが先頭行にあり、先頭行のCellType設定を除外する場合、true</param>
        ''' <remarks></remarks>
        Public Shared Sub BindCellTypeToColumn(ByVal spreadSheet As SheetView, ByVal columnNo As Integer, ByVal cellType As ICellType, Optional ByVal withoutTitleRow As Boolean = True)

            If Not withoutTitleRow Then
                spreadSheet.Columns(columnNo).CellType = cellType
                Return
            End If

            Dim cells As New List(Of ICellType)
            Dim titleRows As Integer = GetTitleRowsIn(spreadSheet)
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
        ''' 行選択かを返す
        ''' </summary>
        ''' <param name="selection">選択範囲</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsSelectedRow(ByVal selection As CellRange) As Boolean

            Return selection.Column = -1 AndAlso selection.ColumnCount = -1
        End Function

        ''' <summary>
        ''' 列選択かを返す
        ''' </summary>
        ''' <param name="selection">選択範囲</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsSelectedColumn(ByVal selection As CellRange) As Boolean

            Return selection.Row = -1 AndAlso selection.RowCount = -1
        End Function

    End Class
End Namespace