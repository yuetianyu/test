Imports ShisakuCommon.Ui.Valid.Impl
Imports ShisakuCommon.Ui.Access.Impl

Namespace Ui.Valid
    ''' <summary>
    ''' System.Windows.Forms用のValidatorクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Validator : Inherits AbstractBaseValidator
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New()
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="errorMessage">エラー発生時のメッセージ</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal errorMessage As String)
            MyBase.New(errorMessage)
        End Sub
        ''' <summary>
        ''' TextBoxのValidatorを追加する
        ''' </summary>
        ''' <param name="aTextBox">TextBox</param>
        ''' <param name="name">名前</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Overloads Function Add(ByVal aTextBox As TextBox, Optional ByVal name As String = Nothing) As ValidateOperation(Of String)
            Dim accessor As New TextBoxAccessor(aTextBox, name)
            Return Operation(Of String)(accessor)
        End Function
        ''' <summary>
        ''' ComboBoxのValidatorを追加する
        ''' </summary>
        ''' <param name="aComboBox">ComboBox</param>
        ''' <param name="name">名前</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Overloads Function Add(ByVal aComboBox As ComboBox, Optional ByVal name As String = Nothing) As ValidateOperation(Of String)
            Dim accessor As New ComboBoxAccessor(aComboBox, name)
            Return Operation(Of String)(accessor)
        End Function
        ''' <summary>
        ''' DateTimePickerのValidatorを追加する
        ''' </summary>
        ''' <param name="aDateTimePicker">DateTimePicker</param>
        ''' <param name="name">名前</param>
        ''' <returns>操作</returns>
        ''' <remarks></remarks>
        Public Overloads Function Add(ByVal aDateTimePicker As DateTimePicker, Optional ByVal name As String = Nothing) As ValidateOperation(Of DateTime)
            Dim accessor As New DateTimePickerAccessor(aDateTimePicker, name)
            Return Operation(Of DateTime)(accessor)
        End Function
    End Class
End Namespace