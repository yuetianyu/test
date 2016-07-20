Imports FarPoint.Win.Spread.UndoRedo
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.Model

Namespace Ui.Spd.Action.Undo
    ''' <summary>
    ''' 選択セルから、データだけを削除するUndoActionクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DeleteDataOnlyUndoAction : Inherits UndoAction

        Private oldValues As BeforeValue()

        Private _sheetView As SheetView
        Private _cellRanges As CellRange()

        Protected Overrides Function SaveUndoState() As Boolean
            ' ClipboardCutDataOnlyUndoAction も、このメソッドの中身は無いから、true固定で問題ないでしょう.
            Return True
        End Function

        Public Overrides Function Undo(ByVal sender As Object) As Boolean

            If Not TypeOf sender Is SpreadView Then
                Return False
            End If

            Dim spread As SpreadView = DirectCast(sender, SpreadView)
            Dim sheet As SheetView = spread.GetSheetView

            For Each values As BeforeValue In oldValues
                values.PerformUndo(sheet)
            Next

            SelectDeletingRange()

            Return True
        End Function

        Public Overrides Function PerformUndoAction(ByVal sender As Object) As Boolean

            If Not TypeOf sender Is SpreadView Then
                Return False
            End If
            Dim view As SpreadView = DirectCast(sender, SpreadView)

            If Not SaveStateIfNecessary(view) Then
                SelectDeletingRange()
            End If

            Dim cellRangeValues As New List(Of BeforeValue)
            For Each aCellRange As CellRange In _cellRanges
                If Not aCellRange.IsValidRange(_sheetView, False) Then
                    Continue For
                End If
                cellRangeValues.Add(New BeforeValue(_sheetView, aCellRange))
                DeleteIn(aCellRange)
            Next

            oldValues = cellRangeValues.ToArray
            Return True
        End Function

        ''' <summary>
        ''' 状態を保存する
        ''' </summary>
        ''' <param name="view">SheetView</param>
        ''' <returns>状態を保存した場合、true</returns>
        ''' <remarks></remarks>
        Private Function SaveStateIfNecessary(ByVal view As SpreadView) As Boolean

            If _sheetView Is Nothing AndAlso view IsNot Nothing Then
                _sheetView = view.GetSheetView
                If _sheetView Is Nothing Then
                    Throw New ArgumentNullException("SpreadView#GetSheetView", "SheetViewが取得できない")
                End If
                If _cellRanges Is Nothing Then
                    If 0 < _sheetView.SelectionCount Then
                        _cellRanges = _sheetView.GetSelections
                    Else
                        _cellRanges = New CellRange() {New CellRange(_sheetView.ActiveRowIndex, _sheetView.ActiveColumnIndex, 1, 1)}
                    End If
                    Return True
                End If
            End If
            Return False
        End Function

        ''' <summary>
        ''' 保存済みの状態から、選択領域を再選択する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SelectDeletingRange()

            UndoUtil.SelectRanges(_sheetView, _cellRanges)
        End Sub

        Private Sub DeleteIn(ByVal selection As CellRange)
            For rowIndex As Integer = 0 To selection.RowCount - 1
                For columnIndex As Integer = 0 To selection.ColumnCount - 1
                    If _sheetView.GetStyleInfo(selection.Row + rowIndex, selection.Column + columnIndex).Locked Then
                        Continue For
                    End If
                    _sheetView.Cells(selection.Row + rowIndex, selection.Column + columnIndex).Value = String.Empty
                Next
            Next
        End Sub

        Public Overrides Function ToString() As String
            ' TODO 用途不明のメソッド。何を実装すべきなのか。
            Return "DeleteUndoAction"
        End Function

        Private Class BeforeValue
            Private row As Integer
            Private column As Integer
            Private cells As List(Of List(Of String))

            Public Sub New(ByVal sheet As SheetView, ByVal selection As CellRange)
                row = selection.Row
                column = selection.Column
                cells = New List(Of List(Of String))
                For rowIndex As Integer = 0 To selection.RowCount - 1
                    cells.Add(New List(Of String))
                    For columnIndex As Integer = 0 To selection.ColumnCount - 1
                        If sheet.GetStyleInfo(selection.Row + rowIndex, selection.Column + columnIndex).Locked Then
                            cells(rowIndex).Add(Nothing)
                        Else
                            cells(rowIndex).Add(StringUtil.Nvl(sheet.Cells(selection.Row + rowIndex, selection.Column + columnIndex).Value))
                        End If
                    Next
                Next
            End Sub

            Public Sub PerformUndo(ByVal sheet As SheetView)
                For rowIndex As Integer = 0 To cells.Count - 1
                    For columnIndex As Integer = 0 To cells(rowIndex).Count - 1
                        If cells(rowIndex)(columnIndex) Is Nothing Then
                            Continue For
                        End If
                        sheet.Cells(row + rowIndex, column + columnIndex).Value = cells(rowIndex)(columnIndex)
                    Next
                Next
            End Sub
        End Class
    End Class
End Namespace