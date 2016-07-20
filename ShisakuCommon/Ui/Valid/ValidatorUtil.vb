Imports ShisakuCommon.Ui.Access.Impl
Imports ShisakuCommon.Ui.Access
Imports FarPoint.Win.Spread

Namespace Ui.Valid
    ''' <summary>
    ''' 入力チェックに関連したユーティリティクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ValidatorUtil
        ' 非公開
        Private Sub New()
        End Sub

        ''' <summary>
        ''' 入力必須設定だけを行った IValidator を返す(Spread用)
        ''' </summary>
        ''' <param name="spread">対象のSpread</param>
        ''' <param name="tag">列TAG名</param>
        ''' <returns>新しいIValidator</returns>
        ''' <remarks></remarks>
        Public Shared Function NewRequiredValidator(ByVal spread As FpSpread, ByVal tag As String) As BaseValidator
            Dim result As New SpreadValidator(spread)
            result.Add(tag).Required()
            Return result
        End Function

        ''' <summary>
        ''' 指定値のいずれかが必須の設定を行った IValidator を返す(Spread用)
        ''' </summary>
        ''' <param name="spread">対象のSpread</param>
        ''' <param name="tag">列TAG名</param>
        ''' <param name="values">指定値</param>
        ''' <returns>新しいIValidator</returns>
        ''' <remarks></remarks>
        Public Shared Function NewRequiredInArrayValidator(ByVal spread As FpSpread, ByVal tag As String, ByVal ParamArray values As String()) As BaseValidator
            Dim result As New SpreadValidator(spread)
            result.Add(tag).Required().InArray(values)
            Return result
        End Function

        ''' <summary>
        ''' 入力チェック IValidator を作成する
        ''' </summary>
        ''' <param name="errorMessage">エラーメッセージ</param>
        ''' <param name="validators">OR接続するIValidator</param>
        ''' <returns>新しいIValidator</returns>
        ''' <remarks></remarks>
        Public Shared Function NewValidatorByOr(ByVal errorMessage As String, ByVal ParamArray validators As IValidator()) As BaseValidator
            Dim result As New Validator(errorMessage)
            If 0 = validators.Length Then
                Return result
            End If
            result.Add(validators(0))
            For i As Integer = 1 To validators.Length - 1
                result.AddOr(validators(i))
            Next
            Return result
        End Function

        ''' <summary>
        ''' エラー項目を作成する
        ''' </summary>
        ''' <param name="spread">Spread</param>
        ''' <param name="spreadRow">行index</param>
        ''' <param name="spreadColumn">列index</param>
        ''' <returns>エラー項目</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeErrorControls(ByVal spread As FpSpread, ByVal spreadRow As Integer, ByVal spreadColumn As Integer) As ErrorControl()
            Return MakeErrorControls(spread, New List(Of Integer)(New Integer() {spreadRow}), New List(Of Integer)(New Integer() {spreadColumn}))
        End Function
        ''' <summary>
        ''' エラー項目を作成する
        ''' </summary>
        ''' <param name="spread">Spread</param>
        ''' <param name="spreadRow">行index</param>
        ''' <param name="spreadColumns">列index</param>
        ''' <returns>エラー項目</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeErrorControls(ByVal spread As FpSpread, ByVal spreadRow As Integer, ByVal spreadColumns As ICollection(Of Integer)) As ErrorControl()
            Return MakeErrorControls(spread, New List(Of Integer)(New Integer() {spreadRow}), spreadColumns)
        End Function
        ''' <summary>
        ''' エラー項目を作成する
        ''' </summary>
        ''' <param name="spread">Spread</param>
        ''' <param name="spreadRows">行index</param>
        ''' <param name="spreadColumn">列index</param>
        ''' <returns>エラー項目</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeErrorControls(ByVal spread As FpSpread, ByVal spreadRows As ICollection(Of Integer), ByVal spreadColumn As Integer) As ErrorControl()
            Return MakeErrorControls(spread, spreadRows, New List(Of Integer)(New Integer() {spreadColumn}))
        End Function
        ''' <summary>
        ''' エラー項目を作成する
        ''' </summary>
        ''' <param name="spread">Spread</param>
        ''' <param name="spreadRows">行index</param>
        ''' <param name="spreadColumns">列index</param>
        ''' <returns>エラー項目</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeErrorControls(ByVal spread As FpSpread, ByVal spreadRows As ICollection(Of Integer), ByVal spreadColumns As ICollection(Of Integer)) As ErrorControl()
            Dim results As New List(Of ErrorControl)
            For Each spreadRow As Integer In spreadRows
                For Each spreadColumn As Integer In spreadColumns
                    results.Add(New ErrorSpreadControlImpl(spread, spreadRow, spreadColumn))
                Next
            Next
            Return results.ToArray
        End Function
    End Class
End Namespace