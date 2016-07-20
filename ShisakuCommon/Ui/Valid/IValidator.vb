Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid
    ''' <summary>
    ''' 入力チェックのインターフェース
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IValidator

        ''' <summary>
        ''' 入力チェックを行う
        ''' </summary>
        ''' <param name="indexes">値のindex</param>
        ''' <returns>正常なら、true</returns>
        ''' <remarks></remarks>
        Function Validate(ByVal ParamArray indexes As Integer()) As Boolean

        ''' <summary>入力エラーだった場合、ErrorControl を返す</summary>
        ''' <returns>ErrorControl</returns>
        ReadOnly Property ErrorControls() As ErrorControl()

        ''' <summary>入力エラーだった場合、エラーメッセージを返す</summary>
        ''' <returns>エラーメッセージ</returns>
        ReadOnly Property ErrorMessage() As String
    End Interface
End Namespace