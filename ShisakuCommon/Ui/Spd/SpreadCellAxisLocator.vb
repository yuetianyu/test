Imports FarPoint.Win.Spread

Namespace Ui.Spd


    ''' <summary>
    ''' 指定のセルを表示している、表示セルの座標を管理するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpreadCellAxisLocator
        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView

        ''' <summary>座標</summary>
        Public Class Axis
            ''' <summary>行</summary>
            Public Row As Integer
            ''' <summary>列</summary>
            Public Column As Integer
            Friend Sub New(ByVal row As Integer, ByVal column As Integer)
                Me.Row = row
                Me.Column = column
            End Sub
        End Class

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="spread">Spread</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal spread As FpSpread)
            Me.spread = spread
            If 0 = spread.Sheets.Count Then
                Throw New ArgumentException("シートを持たないSpreadです.", "spread")
            End If
            sheet = spread.Sheets(0)

            AddHandler sheet.ColumnChanged, AddressOf Sheet_OnColumnChanged
            AddHandler sheet.RowChanged, AddressOf Sheet_OnRowChanged
        End Sub


        Private _cache As New Dictionary(Of Integer, Dictionary(Of Integer, Axis))
        ''' <summary>
        ''' セル座標から、表示セルの座標をもつ
        ''' </summary>
        ''' <param name="row">セル座標 row</param>
        ''' <param name="column">セル座標 column</param>
        ''' <value>表示セルの座標</value>
        ''' <returns>表示セルの座標</returns>
        ''' <remarks></remarks>
        Private Property cache(ByVal row As Integer, ByVal column As Integer) As Axis
            Get
                If Not _cache.ContainsKey(row) Then
                    _cache.Add(row, New Dictionary(Of Integer, Axis))
                End If
                If Not _cache(row).ContainsKey(column) Then
                    Return Nothing
                End If
                Return _cache(row)(column)
            End Get
            Set(ByVal value As Axis)
                If Not _cache.ContainsKey(row) Then
                    _cache.Add(row, New Dictionary(Of Integer, Axis))
                End If
                If _cache(row).ContainsKey(column) Then
                    _cache(row).Remove(column)
                End If
                _cache(row).Add(column, value)
            End Set
        End Property
        ''' <summary>
        ''' 表示セル情報をクリア
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ClearCache()
            _cache.Clear()
        End Sub

        ''' <summary>
        ''' 列の挿入削除のイベント
        ''' </summary>
        ''' <param name="sender">SheetViewEventHandler に従う</param>
        ''' <param name="e">SheetViewEventHandler に従う</param>
        ''' <remarks></remarks>
        Private Sub Sheet_OnRowChanged(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.SheetViewEventArgs)
            ClearCache()
        End Sub
        ''' <summary>
        ''' 行の挿入削除のイベント
        ''' </summary>
        ''' <param name="sender">SheetViewEventHandler に従う</param>
        ''' <param name="e">SheetViewEventHandler に従う</param>
        ''' <remarks></remarks>
        Private Sub Sheet_OnColumnChanged(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.SheetViewEventArgs)
            ClearCache()
        End Sub

        ''' <summary>
        ''' 表示セルの座標を返す
        ''' </summary>
        ''' <param name="row">セル座標 row</param>
        ''' <param name="column">セル座標 column</param>
        ''' <returns>表示セルの座標</returns>
        ''' <remarks></remarks>
        Public Function GetAxis(ByVal row As Integer, ByVal column As Integer) As Axis
            If sheet.ColumnCount <= column Then
                row = sheet.ColumnCount - 1
            End If
            If sheet.RowCount <= row Then
                row = sheet.RowCount - 1
            End If

            If cache(row, column) IsNot Nothing Then
                Return cache(row, column)
            End If

            For rowIndex As Integer = 0 To row
                For columnIndex As Integer = 0 To column
                    If cache(rowIndex, columnIndex) IsNot Nothing Then
                        Continue For
                    End If
                    Dim anAxis As New Axis(rowIndex, columnIndex)
                    For rowIndex2 As Integer = 0 To sheet.Cells(rowIndex, columnIndex).RowSpan - 1
                        For columnIndex2 As Integer = 0 To sheet.Cells(rowIndex, columnIndex).ColumnSpan - 1
                            cache(rowIndex + rowIndex2, columnIndex + columnIndex2) = anAxis
                        Next
                    Next
                Next
            Next
            Return cache(row, column)
        End Function

    End Class
End Namespace