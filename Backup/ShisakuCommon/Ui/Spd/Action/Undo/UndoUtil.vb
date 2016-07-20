Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.Model

Namespace Ui.Spd.Action.Undo
    Public Class UndoUtil

        ''' <summary>
        ''' 領域を選択する
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub SelectRanges(ByVal sheet As SheetView, ByVal cellRanges As CellRange())

            sheet.ClearSelection()
            If 0 < cellRanges.Length Then
                sheet.SetActiveCell(Math.Max(cellRanges(0).Row, 0), Math.Max(cellRanges(0).Column, 0))
            End If
            For Each aCellRange As CellRange In cellRanges
                If Not aCellRange.IsValidRange(sheet, False) Then
                    Continue For
                End If
                sheet.AddSelection(aCellRange.Row, aCellRange.Column, aCellRange.RowCount, aCellRange.ColumnCount)
            Next
        End Sub
    End Class
End Namespace