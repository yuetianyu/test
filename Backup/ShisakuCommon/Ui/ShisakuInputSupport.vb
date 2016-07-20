Imports FarPoint.Win.Spread.CellType
Imports FarPoint.Win.Spread
Imports System
Imports ShisakuCommon.Ui.Spd

Namespace Ui

    ''' <summary>
    ''' 入力サポート欄と、Spreadの関係を制御するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuInputSupport

        Private ReadOnly txtSupport As TextBox
        Private ReadOnly defaultBackColor As Color
        Private ReadOnly spreads() As FpSpread

        ''' <summary>
        ''' Sheet上にタイトルを行を持っている事を表すプロパティ
        ''' </summary>
        ''' <remarks>デフォルトは、true</remarks>
        Public HasTitleInSheet As Boolean = True

        Private activeSpread As FpSpread
        Private activeSheet As SheetView
        Private activeCellType As ICellType

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="txtSupport">入力サポート欄</param>
        ''' <param name="spreads">制御するSpreadたち</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal txtSupport As TextBox, ByVal ParamArray spreads() As FpSpread)
            Me.txtSupport = txtSupport
            Me.spreads = spreads
            Me.defaultBackColor = txtSupport.BackColor

            AddHandler txtSupport.TextChanged, AddressOf TxtSupport_OnTextChanged

            AddHandler txtSupport.PreviewKeyDown, AddressOf TxtSupport_OnPreviewKeyDown
            AddHandler txtSupport.KeyDown, AddressOf TxtSupport_OnKeyDown

            For Each spd As FpSpread In spreads
                AddHandler spd.EnterCell, AddressOf Spread_OnEnterCell
                AddHandler spd.EditChange, AddressOf Spread_OnEditChange
                SpreadUtil.AddHandlerSheetDataModelChanged(spd, AddressOf Spread_OnChange)
            Next

            If spreads.Length = 1 Then
                ForceSetActiveSpread(spreads(0))
            End If
        End Sub

        Private Function GetActiveCellText() As String
            If IsActiveCellComboBox() Then
                Return activeSheet.ActiveCell.Text
            Else
                Return activeSheet.ActiveCell.Value
            End If
        End Function

        Private Sub SetToActiveCellText(ByVal text As String)
            If IsActiveCellComboBox() Then
                activeSheet.ActiveCell.Text = text
            Else
                activeSheet.ActiveCell.Value = text
            End If
        End Sub

        Private ReadOnly axisLocators As New Dictionary(Of FpSpread, SpreadCellAxisLocator)
        Private Function GetAxisLocator(ByVal spread As FpSpread) As SpreadCellAxisLocator
            If Not axisLocators.ContainsKey(spread) Then
                axisLocators.Add(spread, New SpreadCellAxisLocator(spread))
            End If
            Return axisLocators(spread)
        End Function

        ''' <summary>
        ''' 入力サポート欄の KeyDown イベント
        ''' </summary>
        ''' <param name="sender">KeyEventHandler に従う</param>
        ''' <param name="e">KeyEventHandler に従う</param>
        ''' <remarks>TxtSupport_OnPreviewKeyDown の次に動くイベント</remarks>
        Private Sub TxtSupport_OnKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
            If e.KeyValue = Keys.Tab Then
                e.SuppressKeyPress = True   ' Dingが鳴るので、殺す
            ElseIf e.KeyValue = Keys.Enter Then
                e.SuppressKeyPress = True   ' Dingが鳴るので、殺す
            End If
        End Sub

        ''' <summary>
        ''' 入力サポート欄の PreviewKeyDown イベント
        ''' </summary>
        ''' <param name="sender">PreviewKeyDownEventHandlerに従う</param>
        ''' <param name="e">PreviewKeyDownEventHandlerに従う</param>
        ''' <remarks>入力サポート欄で SHIFT+TAB と TAB と SHIFT+ENTER と ENTER を押した時に、入力セル左、右、上、下へ移動させたい。 </remarks>
        Private Sub TxtSupport_OnPreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs)

            If Not activeSheet Is Nothing Then
                Dim axis As SpreadCellAxisLocator.Axis = Nothing
                'Shift+Tab Key と Tab Key
                If e.KeyValue = Keys.Tab Then
                    e.IsInputKey = True ' WindowsのTABキー機能を殺し＆KeyDownイベントを発生させる
                    If e.Shift Then
                        If (0 < activeSheet.ActiveColumnIndex()) Then
                            axis = GetAxisLocator(activeSpread).GetAxis(activeSheet.ActiveRowIndex(), activeSheet.ActiveColumnIndex() - 1)
                        End If
                    Else
                        If (activeSheet.ActiveColumnIndex() + activeSheet.ActiveCell().ColumnSpan < activeSheet.Columns.Count) Then
                            axis = GetAxisLocator(activeSpread).GetAxis(activeSheet.ActiveRowIndex(), activeSheet.ActiveColumnIndex() + activeSheet.ActiveCell().ColumnSpan)
                        End If
                    End If
                    'Shift+Enter Key と Enter Key
                ElseIf e.KeyValue = Keys.Enter Then
                    e.IsInputKey = True ' WindowsのENTERキー機能を殺し＆KeyDownイベントを発生させる
                    If e.Shift Then
                        If (0 < activeSheet.ActiveRowIndex()) Then
                            axis = GetAxisLocator(activeSpread).GetAxis(activeSheet.ActiveRowIndex() - 1, activeSheet.ActiveColumnIndex())
                        End If
                    Else
                        If (activeSheet.ActiveRowIndex() + activeSheet.ActiveCell.RowSpan < activeSheet.Rows.Count) Then
                            axis = GetAxisLocator(activeSpread).GetAxis(activeSheet.ActiveRowIndex() + activeSheet.ActiveCell.RowSpan, activeSheet.ActiveColumnIndex())
                        End If
                    End If
                    '仕様変更のため上下左右も追加　樺澤'
                ElseIf e.KeyValue = Keys.Up Then
                    e.IsInputKey = True
                    If (0 < activeSheet.ActiveRowIndex()) Then
                        axis = GetAxisLocator(activeSpread).GetAxis(activeSheet.ActiveRowIndex() - 1, activeSheet.ActiveColumnIndex())
                    End If

                ElseIf e.KeyValue = Keys.Down Then
                    If (activeSheet.ActiveRowIndex() + activeSheet.ActiveCell.RowSpan < activeSheet.Rows.Count) Then
                        axis = GetAxisLocator(activeSpread).GetAxis(activeSheet.ActiveRowIndex() + activeSheet.ActiveCell.RowSpan, activeSheet.ActiveColumnIndex())
                    End If
                ElseIf e.KeyValue = Keys.Left Then
                    If (0 < activeSheet.ActiveColumnIndex()) Then
                        axis = GetAxisLocator(activeSpread).GetAxis(activeSheet.ActiveRowIndex(), activeSheet.ActiveColumnIndex() - 1)
                    End If
                ElseIf e.KeyValue = Keys.Right Then
                    If (activeSheet.ActiveColumnIndex() + activeSheet.ActiveCell().ColumnSpan < activeSheet.Columns.Count) Then
                        axis = GetAxisLocator(activeSpread).GetAxis(activeSheet.ActiveRowIndex(), activeSheet.ActiveColumnIndex() + activeSheet.ActiveCell().ColumnSpan)
                    End If
                End If
                If axis IsNot Nothing Then
                    SetActiveCell(axis.Row, axis.Column)
                End If
            End If
        End Sub

        ''' <summary>
        ''' セルを移動して、入力サポート欄を表示更新する
        ''' </summary>
        ''' <param name="row">行index</param>
        ''' <param name="column">列index</param>
        ''' <remarks></remarks>
        Public Sub SetActiveCell(ByVal row As Integer, ByVal column As Integer)

            activeSheet.SetActiveCell(row, column)
            activeSpread.ShowActiveCell(VerticalPosition.Nearest, HorizontalPosition.Nearest)
            ' セルカーソルは移動しているけど、なぜか Selection は以前のまま移動しないので、Selection も再設定する
            activeSheet.AddSelection(row, column, 1, 1)
            PerformPostMoveCell(row, column)
        End Sub

        ''' <summary>入力サポート欄の TextChangeイベントを無効にする場合、true</summary>
        Private disabledTextChange As Boolean
        ''' <summary>
        ''' 入力サポート欄の TextChange イベント
        ''' </summary>
        ''' <param name="sender">TextChangeEventHandlerに従う</param>
        ''' <param name="e">TextChangeEventHandlerに従う</param>
        ''' <remarks></remarks>
        Private Sub TxtSupport_OnTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If disabledTextChange Then
                Return
            End If
            SetToActiveCellText(txtSupport.Text)
        End Sub
        ''' <summary>
        ''' 入力サポート欄を、数値のみ入力可に設定する
        ''' </summary>
        ''' <param name="numericOnly">数値のみなら、true</param>
        ''' <remarks>ただし、クリップボードからの貼り付けは制御していない</remarks>
        Private Sub SetNumericOnlyToTxtSupport(ByVal numericOnly As Boolean)
            If numericOnly Then
                AddHandler txtSupport.KeyPress, AddressOf TxtSupport_OnKeyPress_NumericOnly
            Else
                RemoveHandler txtSupport.KeyPress, AddressOf TxtSupport_OnKeyPress_NumericOnly
            End If
        End Sub
        ''' <summary>
        ''' 数値のみを入力できるようにした KeyPressイベント
        ''' </summary>
        ''' <param name="sender">KeyPressEventHandlerに従う</param>
        ''' <param name="e">KeyPressHandlerに従う</param>
        ''' <remarks>ただし、クリップボードからの貼り付けは制御していない</remarks>
        Private Sub TxtSupport_OnKeyPress_NumericOnly(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
            ' vbBack:backspaceキーは除外する
            ' Deleteキーは、このイベントが動かないから、考慮しない
            If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso e.KeyChar <> vbBack Then
                e.Handled = True
            End If
        End Sub

        ''' <summary>
        ''' Spread の Change イベント
        ''' </summary>
        ''' <param name="sender">SheetDataModelEventHandler に従う</param>
        ''' <param name="e">SheetDataModelEventHandler に従う</param>
        ''' <remarks>CTRL+Vや、CTRL+Z用</remarks>
        Private Sub Spread_OnChange(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            If activeSheet Is Nothing OrElse activeSheet.Models.Data IsNot sender Then
                Return
            End If
            If IsDisableTxtSupport() Then
                Return
            End If

            disabledTextChange = True
            Try
                txtSupport.Text = GetActiveCellText()
            Finally
                disabledTextChange = False
            End Try
        End Sub
        ''' <summary>
        ''' Spreadの編集モードで発生する EditChange イベント
        ''' </summary>
        ''' <param name="sender">EditChangeEventHandlerに従う</param>
        ''' <param name="e">EditChangeEventHandlerに従う</param>
        ''' <remarks></remarks>
        Private Sub Spread_OnEditChange(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs)
            If Not TypeOf sender Is FpSpread Then
                Throw New InvalidProgramException
            End If
            disabledTextChange = True
            Try
                txtSupport.Text = GetActiveCellText()
            Finally
                disabledTextChange = False
            End Try
        End Sub

        ''' <summary>
        ''' Spreadの EnterCell イベント
        ''' </summary>
        ''' <param name="sender">EnterCellEventHandlerに従う</param>
        ''' <param name="e">EnterCellEventHandlerに従う</param>
        ''' <remarks></remarks>
        Private Sub Spread_OnEnterCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EnterCellEventArgs)
            If Not TypeOf sender Is FpSpread Then
                Throw New InvalidProgramException
            End If
            Dim newRow As Integer = e.Row
            Dim newColumn As Integer = e.Column

            activeSpread = CType(sender, FpSpread)
            activeSheet = activeSpread.ActiveSheet

            PerformPostMoveCell(newRow, newColumn)

        End Sub

        ''' <summary>
        ''' 強制的にActiveSpreadを変更する
        ''' </summary>
        ''' <param name="spread">ActiveSpreadとするSpread</param>
        ''' <remarks></remarks>
        Public Sub ForceSetActiveSpread(ByVal spread As FpSpread)
            activeSpread = spread
            activeSheet = activeSpread.ActiveSheet

            PerformPostMoveCell(activeSheet.ActiveRowIndex, activeSheet.ActiveColumnIndex)
        End Sub

        Private Sub PerformPostMoveCell(ByVal newRow As Integer, ByVal newColumn As Integer)

            Dim aCellType As ICellType = activeSheet.GetStyleInfo(newRow, newColumn).CellType
            activeCellType = aCellType

            disabledTextChange = True
            Try
                If IsActiveCellCheckBox() OrElse IsActiveCellComboBox() OrElse IsActiveCellDateTime() Then
                    ' チェックボックスorコンボボックスor日付は編集不可
                    DisableTextSupport()
                    Return
                End If
                Dim activeCellLocked As Boolean = activeSheet.GetStyleInfo(newRow, newColumn).Locked
                If HasTitleInSheet AndAlso newRow < SpreadUtil.GetTitleRowsIn(activeSheet) AndAlso activeCellLocked Then
                    ' タイトル欄は編集不可
                    DisableTextSupport()
                    Return
                End If

                txtSupport.Text = GetActiveCellText()
                txtSupport.ReadOnly = activeSheet.GetStyleInfo(newRow, newColumn).Locked
                txtSupport.CharacterCasing = False
                SetBackColorTextSupport()
                If IsActiveCellText() Then
                    Dim aTextCellType As TextCellType = CType(aCellType, TextCellType)
                    txtSupport.MaxLength = aTextCellType.MaxLength
                    If aTextCellType.CharacterSet = CharacterSet.AllIME _
                       OrElse aTextCellType.CharacterSet = CharacterSet.KanjiOnly _
                       OrElse aTextCellType.CharacterSet = CharacterSet.KanjiOnlyIME Then
                        txtSupport.ImeMode = ImeMode.On
                    Else
                        'txtSupport.ImeMode = ImeMode.Off
                        txtSupport.ImeMode = ImeMode.Disable
                    End If
                    SetNumericOnlyToTxtSupport(aTextCellType.CharacterSet = CharacterSet.Numeric)
                    txtSupport.CharacterCasing = aTextCellType.CharacterCasing

                ElseIf IsActiveCellCurrency() Then
                    Dim aCurrencyCellType As CurrencyCellType = CType(aCellType, CurrencyCellType)
                    txtSupport.MaxLength = Math.Max(aCurrencyCellType.MaximumValue.ToString.Length, aCurrencyCellType.MinimumValue.ToString.Length)
                    txtSupport.ImeMode = ImeMode.Off
                    SetNumericOnlyToTxtSupport(True)
                End If
            Finally
                disabledTextChange = False
            End Try
        End Sub

        Private Sub DisableTextSupport()

            txtSupport.Text = Nothing
            txtSupport.ReadOnly = True
            SetBackColorTextSupport()
        End Sub

        Private Function IsActiveCellText() As Boolean
            Return TypeOf activeCellType Is TextCellType
        End Function

        Private Function IsActiveCellCheckBox() As Boolean
            Return TypeOf activeCellType Is CheckBoxCellType
        End Function

        Private Function IsActiveCellComboBox() As Boolean
            Return TypeOf activeCellType Is ComboBoxCellType
        End Function

        Private Function IsActiveCellDateTime() As Boolean
            Return TypeOf activeCellType Is DateTimeCellType
        End Function

        Private Function IsActiveCellCurrency() As Boolean
            Return TypeOf activeCellType Is CurrencyCellType
        End Function

        ''' <summary>
        ''' 入力サポート欄の背景色を設定する
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetBackColorTextSupport()

            If IsDisableTxtSupport() Then
                txtSupport.BackColor = Color.LightGray
            Else
                txtSupport.BackColor = defaultBackColor
            End If
        End Sub

        ''' <summary>
        ''' 入力サポート欄が使用不可かを返す
        ''' </summary>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function IsDisableTxtSupport() As Boolean

            Return txtSupport.ReadOnly
        End Function
    End Class
End Namespace