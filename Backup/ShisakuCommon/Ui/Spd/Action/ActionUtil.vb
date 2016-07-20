Imports FarPoint.Win.Spread

Namespace Ui.Spd.Action
    Public Class ActionUtil
        ''' <summary>
        ''' アクティブセルを移動し、そのセルが表示されるようにスクロールする
        ''' </summary>
        ''' <param name="view">SpreadView</param>
        ''' <param name="sheet">SheetView</param>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Public Shared Sub SetActiveCellAndShow(ByVal view As SpreadView, ByVal sheet As SheetView, ByVal rowIndex As Integer, ByVal columnIndex As Integer)

            sheet.SetActiveCell(rowIndex, columnIndex)
            view.ShowActiveCell(VerticalPosition.Nearest, HorizontalPosition.Nearest)
        End Sub
    End Class
End Namespace