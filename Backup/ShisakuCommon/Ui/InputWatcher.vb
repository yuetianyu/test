Imports FarPoint.Win.Spread
Imports ShisakuCommon.Ui.Spd

Namespace Ui
    ''' <summary>
    ''' 入力監視
    ''' </summary>
    ''' <remarks></remarks>
    Public Class InputWatcher

#Region "Local Member"
        ''' <summary>
        ''' 監視結果
        ''' True:変更する事が有り
        ''' False:変更する事が無い
        ''' </summary>
        ''' <remarks></remarks>
        Private isChanged As Boolean
        ''' <summary>
        ''' 監視されてもの－－TextBox
        ''' </summary>
        ''' <remarks></remarks>
        Private textBoxList As New List(Of TextBox)
        ''' <summary>
        ''' 監視されてもの－－ComboBox
        ''' </summary>
        ''' <remarks></remarks>
        Private comboBoxList As New List(Of ComboBox)
        ''' <summary>
        ''' 監視されてもの－－DateTimePicker
        ''' </summary>
        ''' <remarks></remarks>
        Private dateTimePickerList As New List(Of DateTimePicker)
        ''' <summary>
        ''' 監視されてもの－－Spread
        ''' </summary>
        ''' <remarks></remarks>
        Private spreadList As New List(Of FpSpread)
#End Region


        ''' <summary>
        ''' TextBox監視するコントロールを追加する
        ''' </summary>
        ''' <param name="textBox">TextBox</param>
        ''' <remarks></remarks>
        Public Sub Add(ByVal textBox As TextBox)
            If (Not textBox Is Nothing) Then
                AddHandler textBox.TextChanged, AddressOf Text_OnTextChanged
                textBoxList.Add(textBox)
            End If
        End Sub

        ''' <summary>
        ''' ComboBox監視するコントロールを追加する
        ''' </summary>
        ''' <param name="comboBox">ComboBox</param>
        ''' <remarks></remarks>
        Public Sub Add(ByVal comboBox As ComboBox)
            If (Not comboBox Is Nothing) Then
                AddHandler comboBox.SelectedValueChanged, AddressOf Text_OnTextChanged
                comboBoxList.Add(comboBox)
            End If
        End Sub

        ''' <summary>
        ''' DateTimePicker監視するコントロールを追加する
        ''' </summary>
        ''' <param name="dateTimePicker">DateTimePicker</param>
        ''' <remarks></remarks>
        Public Sub Add(ByVal dateTimePicker As DateTimePicker)
            If (Not dateTimePicker Is Nothing) Then
                AddHandler dateTimePicker.ValueChanged, AddressOf Text_OnTextChanged
                dateTimePickerList.Add(dateTimePicker)
            End If
        End Sub

        ''' <summary>
        ''' FpSpread監視するコントロールを追加する
        ''' </summary>
        ''' <param name="spread">FpSpread</param>
        ''' <remarks></remarks>
        Public Sub Add(ByVal spread As FpSpread)
            If (Not spread Is Nothing) Then
                SpreadUtil.AddHandlerSheetDataModelChanged(spread, AddressOf Text_OnTextChanged)
                spreadList.Add(spread)
            End If
        End Sub

        Private Sub Text_OnTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Me.isChanged = True
        End Sub

        ''' <summary>
        ''' 変更されている/されていない、のフラグをクリア
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Clear()
            Me.isChanged = False
        End Sub

        ''' <summary>
        ''' 変更されている/されていない、の判断
        ''' </summary>
        ''' <returns>変更されている/されていない</returns>
        ''' <remarks></remarks>
        Public Function WasUpdate() As Boolean
            Return Me.isChanged
        End Function

        ''' <summary>
        ''' コントロールを除去する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RemoveAll()
            RemoveTextBoxHandler()
            RemoveComboBoxHandler()
            RemoveDateTimePickerHandler()
            RemoveFpSpreadHandler()
        End Sub

        ''' <summary>
        ''' コントロールを除去する--TextBox
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RemoveTextBoxHandler()
            For Each textBox As TextBox In textBoxList
                RemoveHandler textBox.TextChanged, AddressOf Text_OnTextChanged
            Next
            textBoxList.Clear()
        End Sub
        ''' <summary>
        ''' コントロールを除去する--ComboBox
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RemoveComboBoxHandler()
            For Each comboBox As ComboBox In comboBoxList
                RemoveHandler comboBox.SelectedValueChanged, AddressOf Text_OnTextChanged
            Next
            comboBoxList.Clear()
        End Sub
        ''' <summary>
        ''' コントロールを除去する--DateTimePicker
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RemoveDateTimePickerHandler()
            For Each dateTimePicker As DateTimePicker In dateTimePickerList
                RemoveHandler dateTimePicker.ValueChanged, AddressOf Text_OnTextChanged
            Next
            dateTimePickerList.Clear()
        End Sub
        ''' <summary>
        ''' コントロールを除去する--FpSpread
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RemoveFpSpreadHandler()
            For Each spread As FpSpread In spreadList
                SpreadUtil.RemoveHandlerSheetDataModelChanged(spread, AddressOf Text_OnTextChanged)
            Next
            spreadList.Clear()
        End Sub

    End Class
End Namespace
