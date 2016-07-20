Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.Model

Namespace EventEdit.Ui
    ' TODO サンプルソースを切り張りしただけのクラス. リファクタリングする事
    Public Class EventSpreadDragDrop
        Private ReadOnly spreadDrag As FpSpread
        Private ReadOnly spreadDrop As FpSpread
        Private ReadOnly sheetDrag As SheetView
        Private ReadOnly sheetDrop As SheetView

        Private Class BackUpProperties
            Friend AllowDrop As Boolean
            Friend ColumnSplitBoxPolicy As FarPoint.Win.Spread.SplitBoxPolicy
            Friend RowSplitBoxPolicy As FarPoint.Win.Spread.SplitBoxPolicy
        End Class

        Private backup As New BackUpProperties

        Private selectingRange As FarPoint.Win.Spread.Model.CellRange
        Private selectingValue As String

        Public Sub New(ByVal spreadDrag As FpSpread, ByVal spreadDrop As FpSpread)
            Me.spreadDrag = spreadDrag
            Me.spreadDrop = spreadDrop
            If spreadDrag.Sheets.Count = 0 Then
                Throw New ArgumentException("Drag元のSpreadにシートがありません.")
            End If
            Me.sheetDrag = spreadDrag.Sheets(0)
            If spreadDrop.Sheets.Count = 0 Then
                Throw New ArgumentException("Drop先のSpreadにシートがありません.")
            End If
            Me.sheetDrop = spreadDrop.Sheets(0)

            backup.AllowDrop = spreadDrop.AllowDrop
            backup.ColumnSplitBoxPolicy = spreadDrop.ColumnSplitBoxPolicy
            backup.RowSplitBoxPolicy = spreadDrop.RowSplitBoxPolicy

            ' セルブロックのドラッグ＆ドロップを可能に設定します。
            spreadDrag.AllowDragDrop = True
            ' セルブロックのドロップを可能に設定します。
            spreadDrop.AllowDrop = True

            '分割ボックスを無効にします。
            spreadDrag.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
            spreadDrag.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
            spreadDrop.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
            spreadDrop.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never

            AddHandler spreadDrag.MouseDown, AddressOf SpreadDrag_MouseDown
            AddHandler spreadDrag.MouseUp, AddressOf SpreadDrag_MouseUp

            AddHandler spreadDrop.DragDrop, AddressOf SpreadDrop_DragDrop
            AddHandler spreadDrop.DragEnter, AddressOf SpreadDrop_DragEnter
            AddHandler spreadDrop.DragOver, AddressOf SpreadDrop_DragOver
        End Sub

        Private Sub SpreadDrag_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            ' ドラッグ アンド ドロップ操作の効果を指定します。
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Try
                    spreadDrag.DoDragDrop(selectingValue, DragDropEffects.Copy Or DragDropEffects.Move)
                Catch Exception As Exception
                End Try
            End If
        End Sub

        Private Sub SpreadDrag_MouseUp(ByVal sender As Object, _
                   ByVal e As System.Windows.Forms.MouseEventArgs)
            If sheetDrag.GetSelections.Length = 0 Then
                MessageBox.Show("選択されたセルがありません。")
                Return
            End If
            ' 現在選択しているセル範囲を取得します。
            selectingRange = sheetDrag.GetSelection(0)
            ' 指定した範囲内のテキストを取得します。
            selectingValue = sheetDrag.GetClip(selectingRange.Row, selectingRange.Column, selectingRange.RowCount, selectingRange.ColumnCount)
            '' takas-ho 縦長の選択を、横長の形式に変換
            selectingValue = selectingValue.Replace(vbCrLf, vbTab)
        End Sub

        Private Sub SpreadDrop_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
            ' ドラッグ アンド ドロップ操作の効果を指定します。
            If e.Data.GetDataPresent(DataFormats.Text) Then
                e.Effect = DragDropEffects.Move
            End If
        End Sub

        Private Sub SpreadDrop_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
            ' ドロップしたときの処理を記述します。
            Dim sel As FarPoint.Win.Spread.Model.CellRange

            ' 選択範囲を取得します。
            sel = sheetDrop.GetSelection(0)

            ' 指定した範囲内にテキストを設定します。
            sheetDrop.SetClip(sel.Row, sel.Column, sel.RowCount, sel.ColumnCount, selectingValue)

            ' 指定したセル範囲の選択状態を解除します。
            sheetDrop.RemoveSelection(selectingRange.Row, selectingRange.Column, selectingRange.RowCount, selectingRange.ColumnCount)
        End Sub

        Private Sub SpreadDrop_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)

            Dim bufRowFrozenRowCount As Integer = sheetDrop.FrozenRowCount
            Try
                sheetDrop.FrozenRowCount = 0

                ' クライアント座標を算出します。
                Dim p As Point = spreadDrop.PointToClient(New Point(e.X, e.Y))

                ' 指定したポイントから　CellRange オブジェクトを取得します。
                Dim range As CellRange = spreadDrop.GetCellFromPixel(0, 0, p.X, p.Y)

                ' 行数及び列数のあるときは
                If range.ColumnCount <> -1 And range.RowCount <> -1 Then
                    ' 現在選択しているセル範囲のあるときは
                    Dim sel As CellRange = sheetDrop.GetSelection(0)
                    If sel IsNot Nothing Then
                        ' 現在のセル範囲の選択状態を解除します。
                        sheetDrop.RemoveSelection(sel.Row, sel.Column, sel.RowCount, sel.ColumnCount)
                    End If
                    '' takas-ho タイトル行だけ
                    If Not (range.Row = 0 Or range.Row = 1 Or range.Row = 2) Then
                        Return
                    End If
                    'If range.Row <> 0 Then
                    '    Return
                    'End If
                    ' 指定したセル範囲を選択範囲に追加します。
                    '' takas-ho Drag元で縦長に選択した場合、Drop先は横長にする
                    sheetDrop.AddSelection(range.Row, range.Column, selectingRange.ColumnCount, selectingRange.RowCount)
                End If
            Finally
                sheetDrop.FrozenRowCount = bufRowFrozenRowCount
            End Try
        End Sub

        Public Sub ClearEvent()
            RemoveHandler spreadDrag.MouseDown, AddressOf SpreadDrag_MouseDown
            RemoveHandler spreadDrag.MouseUp, AddressOf SpreadDrag_MouseUp

            RemoveHandler spreadDrop.DragDrop, AddressOf SpreadDrop_DragDrop
            RemoveHandler spreadDrop.DragEnter, AddressOf SpreadDrop_DragEnter
            RemoveHandler spreadDrop.DragOver, AddressOf SpreadDrop_DragOver

            spreadDrop.AllowDrop = backup.AllowDrop
            spreadDrop.ColumnSplitBoxPolicy = backup.ColumnSplitBoxPolicy
            spreadDrop.RowSplitBoxPolicy = backup.RowSplitBoxPolicy
        End Sub
    End Class
End Namespace