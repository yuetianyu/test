Imports FarPoint.Win.Spread

Namespace Ui.Spd.Action
    ''' <summary>
    ''' ExcelのCTRL+[↑]を実現するActionクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ExcelCtrlUpArrowAction : Inherits FarPoint.Win.Spread.Action

        Public Overrides Sub PerformAction(ByVal sender As Object)

            If Not TypeOf sender Is SpreadView Then
                Return
            End If
            Dim view As SpreadView = DirectCast(sender, SpreadView)
            Dim sheet As SheetView = view.GetSheetView

            Dim activeCellRow As Integer = sheet.ActiveCell.Row.Index
            Dim activeCellColumn As Integer = sheet.ActiveCell.Column.Index
            If IsEmptyOrUpEmpty(sheet) Then
                For subtrahend As Integer = 1 To activeCellRow
                    If StringUtil.IsNotEmpty(sheet.Cells(activeCellRow - subtrahend, activeCellColumn).Value) Then
                        ActionUtil.SetActiveCellAndShow(view, sheet, activeCellRow - subtrahend, activeCellColumn)
                        Return
                    End If
                Next
            Else
                For subtrahend As Integer = 1 To activeCellRow
                    If StringUtil.IsEmpty(sheet.Cells(activeCellRow - subtrahend, activeCellColumn).Value) Then
                        ActionUtil.SetActiveCellAndShow(view, sheet, activeCellRow - subtrahend + 1, activeCellColumn)
                        Return
                    End If
                Next
            End If
            ActionUtil.SetActiveCellAndShow(view, sheet, 0, activeCellColumn)
        End Sub

        Private Function IsEmptyOrUpEmpty(ByVal sheet As SheetView) As Boolean
            If StringUtil.IsEmpty(sheet.ActiveCell.Value) Then
                Return True
            End If

            If 0 <= sheet.ActiveCell.Row.Index - 1 Then
                Return StringUtil.IsEmpty(sheet.Cells(sheet.ActiveCell.Row.Index - 1, sheet.ActiveCell.Column.Index).Value)
            End If

            Return False
        End Function
    End Class
End Namespace