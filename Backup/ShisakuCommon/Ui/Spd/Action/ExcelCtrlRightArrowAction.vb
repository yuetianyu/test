Imports FarPoint.Win.Spread

Namespace Ui.Spd.Action
    ''' <summary>
    ''' ExcelのCTRL+[→]を実現するActionクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ExcelCtrlRightArrowAction : Inherits FarPoint.Win.Spread.Action

        Public Overrides Sub PerformAction(ByVal sender As Object)

            If Not TypeOf sender Is SpreadView Then
                Return
            End If
            Dim view As SpreadView = DirectCast(sender, SpreadView)
            Dim sheet As SheetView = view.GetSheetView

            If IsEmptyOrRightEmpty(sheet) Then
                For addend As Integer = sheet.ActiveCell.Column.Index + 1 To sheet.ColumnCount - 1
                    If StringUtil.IsNotEmpty(sheet.Cells(sheet.ActiveCell.Row.Index, addend).Value) Then
                        ActionUtil.SetActiveCellAndShow(view, sheet, sheet.ActiveCell.Row.Index, addend)
                        Return
                    End If
                Next
            Else
                For addend As Integer = sheet.ActiveCell.Column.Index + 1 To sheet.ColumnCount - 1
                    If StringUtil.IsEmpty(sheet.Cells(sheet.ActiveCell.Row.Index, addend).Value) Then
                        ActionUtil.SetActiveCellAndShow(view, sheet, sheet.ActiveCell.Row.Index, addend - 1)
                        Return
                    End If
                Next
            End If
            ActionUtil.SetActiveCellAndShow(view, sheet, sheet.ActiveCell.Row.Index, sheet.ColumnCount - 1)
        End Sub

        Private Function IsEmptyOrRightEmpty(ByVal sheet As SheetView) As Boolean
            If StringUtil.IsEmpty(sheet.ActiveCell.Value) Then
                Return True
            End If

            If sheet.ActiveCell.Column.Index + 1 < sheet.ColumnCount Then
                Return StringUtil.IsEmpty(sheet.Cells(sheet.ActiveCell.Row.Index, sheet.ActiveCell.Column.Index + 1).Value)
            End If

            Return False
        End Function
    End Class
End Namespace