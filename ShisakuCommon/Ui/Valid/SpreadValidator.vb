Imports ShisakuCommon.Ui.Valid.Impl
Imports ShisakuCommon.Ui.Access
Imports FarPoint.Win.Spread.CellType
Imports FarPoint.Win.Spread
Imports ShisakuCommon.Ui.Access.Impl

Namespace Ui.Valid
    ''' <summary>
    ''' Spread用のValidatorクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpreadValidator : Inherits AbstractBaseValidator
        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="spread">Spread</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal spread As FpSpread)
            Me.New(spread, Nothing)
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="spread">Spread</param>
        ''' <param name="errorMessage">大元エラーメッセージ</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal spread As FpSpread, ByVal errorMessage As String)
            MyBase.New(errorMessage)
            Me.spread = spread
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            sheet = spread.Sheets(0)
        End Sub
        ''' <summary>
        ''' Spreadの列Validatorを追加する
        ''' </summary>
        ''' <param name="columnTag">列Tag</param>
        ''' <param name="name">列名</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Overloads Function Add(ByVal columnTag As String, Optional ByVal name As String = Nothing) As ValidateOperation(Of String)
            Dim accessor As ControlAccessor(Of String) = Axs(columnTag, name)
            Return Operation(Of String)(accessor)
        End Function
        ''' <summary>
        ''' SpreadのText列Validatorを追加する
        ''' </summary>
        ''' <param name="columnTag">列Tag</param>
        ''' <param name="name">列名</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Overloads Function AddTextCell(ByVal columnTag As String, Optional ByVal name As String = Nothing) As ValidateOperation(Of String)
            Return Operation(Of String)(New SpreadTextCellAccessor(spread, columnTag, name))
        End Function
        ''' <summary>
        ''' SpreadのComboBox列Validatorを追加する
        ''' </summary>
        ''' <param name="columnTag">列Tag</param>
        ''' <param name="name">列名</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Overloads Function AddComboBoxCell(ByVal columnTag As String, Optional ByVal name As String = Nothing) As ValidateOperation(Of String)
            Return Operation(Of String)(New SpreadComboBoxCellAccessor(spread, columnTag, name))
        End Function
        Public Function Axs(ByVal columnTag As String, ByVal name As String) As ControlAccessor(Of String)
            If IsTextCell(columnTag) Then
                Return New SpreadTextCellAccessor(spread, columnTag, name)
            End If
            If IsDefaultCell(columnTag) Then
                Return New SpreadTextCellAccessor(spread, columnTag, name)
            End If
            If IsComboBoxCell(columnTag) Then
                Return New SpreadComboBoxCellAccessor(spread, columnTag, name)
            End If
            Throw New NotSupportedException("列のCellType " & sheet.Columns(columnTag).CellType.GetType.Name & " 型には未対応です.")
        End Function
        ''' <summary>
        ''' 列がTextセルかを返す
        ''' </summary>
        ''' <param name="columnTag">列Tag</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function IsTextCell(ByVal columnTag As String) As Boolean
            Return TypeOf sheet.Columns(columnTag).CellType Is TextCellType
        End Function
        ''' <summary>
        ''' 列がComboBoxセルかを返す
        ''' </summary>
        ''' <param name="columnTag">列Tag</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function IsComboBoxCell(ByVal columnTag As String) As Boolean
            Return TypeOf sheet.Columns(columnTag).CellType Is ComboBoxCellType
        End Function
        ''' <summary>
        ''' 列が初期設定セルかを返す
        ''' </summary>
        ''' <param name="columnTag">列Tag</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function IsDefaultCell(ByVal columnTag As String) As Boolean
            Return sheet.Columns(columnTag).CellType Is Nothing
        End Function
    End Class
End Namespace