Imports FarPoint.Win.Spread

Namespace Ui.Access.Impl
    Public MustInherit Class AbstractSpreadControlAccessor(Of T) : Inherits AbstractControlAccessor(Of T)
        Protected ReadOnly spread As FpSpread
        Protected ReadOnly sheet As SheetView
        Protected ReadOnly columnTag As String
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="spread">Spread</param>
        ''' <param name="columnTag">列Tag</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal spread As FpSpread, ByVal columnTag As String, ByVal name As String)
            MyBase.new(spread, name)
            Me.spread = spread
            Me.columnTag = columnTag
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)
        End Sub

        Public Overrides Function NewErrorControl() As ErrorControl
            Return New ErrorSpreadControlImpl(spread, lastRow, sheet.Columns(columnTag).Index)
        End Function

        Private lastRow As Integer
        Public Overrides ReadOnly Property Value(Optional ByVal index As Integer = -1) As T
            Get
                lastRow = index
                Return sheet.Cells(index, sheet.Columns(columnTag).Index).Value
            End Get
        End Property


    End Class
End Namespace