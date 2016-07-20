Imports FarPoint.Win.Spread

Namespace Ui.Access.Impl
    Public Class ErrorSpreadControlImpl : Implements ErrorControl
        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        Public ReadOnly rowIndex As Integer
        Public ReadOnly columnIndex As Integer
        Private ReadOnly columnTag As String
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="spread">Spread</param>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal spread As FpSpread, ByVal rowIndex As Integer, ByVal columnIndex As Integer)
            Me.New(spread, rowIndex, columnIndex, Nothing)
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="spread">Spread</param>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnTag">列Tag</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal spread As FpSpread, ByVal rowIndex As Integer, ByVal columnTag As String)
            Me.New(spread, rowIndex, Nothing, columnTag)
        End Sub
        Private Sub New(ByVal spread As FpSpread, ByVal rowIndex As Integer, ByVal columnIndex As Integer?, ByVal columnTag As String)
            Me.spread = spread
            Me.rowIndex = rowIndex
            Me.columnTag = columnTag
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            sheet = spread.Sheets(0)
            If columnIndex Is Nothing Then
                If columnTag Is Nothing Then
                    Throw New ArgumentException("columnIndex/columnTag が未指定です.")
                End If
                Me.columnIndex = sheet.Columns(columnTag).Index
            Else
                Me.columnIndex = CInt(columnIndex)
            End If
        End Sub

        ''' <summary>
        ''' フォーカスを当てる
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Focus() Implements ErrorControl.Focus
            If Not spread.Focused Then
                spread.Focus()
            End If
            sheet.SetActiveCell(rowIndex, columnIndex)
        End Sub

        ''' <summary>
        ''' 背景色をエラー色にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetBackColorOnError() Implements ErrorControl.SetBackColorOnError
            ShisakuFormUtil.onErro(sheet.Cells(rowIndex, columnIndex))
        End Sub
        ''' <summary>
        ''' 背景色をワーニング色にする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetBackColorOnWarning() Implements ErrorControl.SetBackColorOnWarning
            ShisakuFormUtil.onWarning(sheet.Cells(rowIndex, columnIndex))
        End Sub

        ''' <summary>
        ''' 背景色をクリアする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearBackColor() Implements ErrorControl.ClearBackColor
            '↓↓2014/10/29 酒井 ADD BEGIN
            'Ver6_2 1.95以降の修正内容の展開
            'ShisakuFormUtil.initlColor(sheet.Cells(rowIndex, columnIndex))
            If rowIndex < sheet.RowCount And columnIndex < sheet.ColumnCount Then
                ShisakuFormUtil.initlColor(sheet.Cells(rowIndex, columnIndex))
            End If
            '↑↑2014/10/29 酒井 ADD END
        End Sub
    End Class
End Namespace