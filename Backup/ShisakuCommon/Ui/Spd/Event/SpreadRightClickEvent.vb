Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.Model

Namespace Ui.Spd.Event
    ''' <summary>
    ''' セルを右クリックした時の動作を担うクラス
    ''' </summary>
    ''' <remarks>ShisakuSpreadUtil より利用可能</remarks>
    Friend Class SpreadRightClickEvent
        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        Private ReadOnly inputSupport As ShisakuInputSupport
        Public Sub New(ByVal spread As FpSpread)
            Me.New(spread, Nothing)
        End Sub
        Public Sub New(ByVal spread As FpSpread, ByVal inputSupport As ShisakuInputSupport)
            Me.spread = spread
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)

            Me.inputSupport = inputSupport
        End Sub
        Public Sub Spread_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

            If e.Button = Windows.Forms.MouseButtons.Right Then
                '右クリックされたセルをアクティブセルに設定する.
                If e.RowHeader Then
                    If sheet.GetSelections.Length = 1 Then
                        Dim selection As CellRange = sheet.GetSelection(0)
                        If SpreadUtil.IsSelectedRow(selection) AndAlso selection.Row <= e.Row And e.Row <= selection.Row + selection.RowCount - 1 Then
                            Return
                        End If
                    End If
                    sheet.ClearSelection()
                    sheet.AddSelection(e.Row, -1, 1, -1)
                    Return
                ElseIf e.ColumnHeader Then
                    If sheet.GetSelections.Length = 1 Then
                        Dim selection As CellRange = sheet.GetSelection(0)
                        If SpreadUtil.IsSelectedColumn(selection) AndAlso selection.Column <= e.Column And e.Column <= selection.Column + selection.ColumnCount - 1 Then
                            Return
                        End If
                    End If
                    sheet.ClearSelection()
                    sheet.AddSelection(-1, e.Column, -1, 1)
                    Return
                Else
                    If sheet.GetSelections.Length = 1 Then
                        Dim selection As CellRange = sheet.GetSelection(0)
                        If selection.Row <= e.Row And e.Row <= selection.Row + selection.RowCount - 1 AndAlso selection.Column <= e.Column And e.Column <= selection.Column + selection.ColumnCount - 1 Then
                            Return
                        End If
                    End If
                    If inputSupport IsNot Nothing Then
                        inputSupport.SetActiveCell(e.Row, e.Column)
                    End If
                End If
            End If
        End Sub
    End Class
End Namespace