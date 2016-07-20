Imports FarPoint.Win.Spread.CellType
Imports EventSakusei.EventEdit.Ui
Imports FarPoint.Win.Spread
Imports ShisakuCommon.Ui.Spd

Namespace ShisakuBuhinEdit.Ui
    Public Class BuhinEditSpreadUtil
        ''' <summary>スプレッドの初期表示行</summary>
        Public Const SPREAD_DEFAULT_ROW_COUNT As Integer = EventSpreadUtil.SPREAD_DEFAULT_ROW_COUNT

        ''' <summary>
        ''' イベント情報画面のSpreadのタイトル行数を返す
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <returns>タイトル行数</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTitleRows(ByVal spreadSheet As SheetView) As Integer
            Return SpreadUtil.GetTitleRowsIn(spreadSheet)
        End Function

        ''' <summary>
        ''' 部品表編集画面のSpread初期化
        ''' </summary>
        ''' <param name="aSpread">対象Spread</param>
        ''' <remarks></remarks>
        Public Shared Sub InitializeFrm41(ByVal aSpread As FpSpread)

            SpreadUtil.Initialize(aSpread)

            If aSpread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Dim sheet As SheetView = aSpread.Sheets(0)

            sheet.RowCount = GetTitleRows(sheet)
            sheet.RowCount = GetTitleRows(sheet) + SPREAD_DEFAULT_ROW_COUNT
        End Sub

        ''' <summary>
        ''' 列全体にCellTypeを設定する
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="tag">設定先の列タグ名</param>
        ''' <param name="cellType">設定するCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub BindCellTypeToColumn(ByVal spreadSheet As SheetView, ByVal tag As String, ByVal cellType As ICellType)

            SpreadUtil.BindCellTypeToColumn(spreadSheet, tag, cellType)
        End Sub
        ''' <summary>
        ''' 列全体にCellTypeを設定する
        ''' </summary>
        ''' <param name="spreadSheet">SheetView</param>
        ''' <param name="columnNo">設定先の列No</param>
        ''' <param name="cellType">設定するCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub BindCellTypeToColumn(ByVal spreadSheet As SheetView, ByVal columnNo As Integer, ByVal cellType As ICellType)
            SpreadUtil.BindCellTypeToColumn(spreadSheet, columnNo, cellType)
        End Sub

        ''' <summary>
        ''' 通常使われるテキストCellTypeを作成して返す
        ''' </summary>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function NewGeneralTextCellType() As TextCellType
            Return ShisakuSpreadUtil.NewGeneralTextCellType()
        End Function

        ''' <summary>
        ''' 通常使われる日時CellTypeを作成して返す
        ''' </summary>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function NewGeneralDateTimeCellType() As DateTimeCellType
            Return ShisakuSpreadUtil.NewGeneralDateTimeCellType()
        End Function

        ''' <summary>
        ''' 通常使われるコンボボックスCellTypeを作成して返す
        ''' </summary>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function NewGeneralComboBoxCellType() As ComboBoxCellType
            Return ShisakuSpreadUtil.NewGeneralComboBoxCellType()
        End Function

        ''' <summary>
        ''' 通常使われるチェックボックスCellTypeを作成して返す
        ''' </summary>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function NewGeneralCheckBoxCellType() As CheckBoxCellType
            Return ShisakuSpreadUtil.NewGeneralCheckBoxCellType()
        End Function
    End Class
End Namespace