Imports FarPoint.Win.Spread

Namespace Ui.Spd.Action
    ''' <summary>
    ''' ExcelのCTRL+[↓]を実現するActionクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ExcelCtrlDownArrowAction : Inherits FarPoint.Win.Spread.Action

        Public Overrides Sub PerformAction(ByVal sender As Object)

            If Not TypeOf sender Is SpreadView Then
                Return
            End If
            Dim view As SpreadView = DirectCast(sender, SpreadView)
            Dim sheet As SheetView = view.GetSheetView

            If IsEmptyOrDownEmpty(sheet) Then
                For rowIndex As Integer = sheet.ActiveCell.Row.Index + 1 To sheet.RowCount - 1
                    If StringUtil.IsNotEmpty(sheet.Cells(rowIndex, sheet.ActiveCell.Column.Index).Value) Then
                        ActionUtil.SetActiveCellAndShow(view, sheet, rowIndex, sheet.ActiveCell.Column.Index)
                        Return
                    End If
                Next
            Else
                For rowIndex As Integer = sheet.ActiveCell.Row.Index + 1 To sheet.RowCount - 1
                    If StringUtil.IsEmpty(sheet.Cells(rowIndex, sheet.ActiveCell.Column.Index).Value) Then
                        ActionUtil.SetActiveCellAndShow(view, sheet, rowIndex - 1, sheet.ActiveCell.Column.Index)
                        Return
                    End If
                Next
            End If
            ActionUtil.SetActiveCellAndShow(view, sheet, sheet.RowCount - 1, sheet.ActiveCell.Column.Index)
        End Sub

        Private Function IsEmptyOrDownEmpty(ByVal sheet As SheetView) As Boolean
            If StringUtil.IsEmpty(sheet.ActiveCell.Value) Then
                Return True
            End If

            If sheet.ActiveCell.Row.Index + 1 < sheet.RowCount Then
                Return StringUtil.IsEmpty(sheet.Cells(sheet.ActiveCell.Row.Index + 1, sheet.ActiveCell.Column.Index).Value)
            End If

            Return False
        End Function
    End Class
End Namespace