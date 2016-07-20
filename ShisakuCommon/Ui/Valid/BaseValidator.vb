Namespace Ui.Valid
    ''' <summary>
    ''' Validateチェックを管理することを担うインターフェース
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface BaseValidator : Inherits IValidator
        ''' <summary>
        ''' Validateチェックを追加する
        ''' </summary>
        ''' <param name="aValidator"></param>
        ''' <remarks></remarks>
        Sub Add(ByVal aValidator As IValidator)

        ''' <summary>
        ''' Validateチェックに問題がない事を実証する
        ''' </summary>
        ''' <param name="indexes">インデックス（行など）</param>
        ''' <remarks>問題が有る場合は、例外で通知</remarks>
        Sub AssertValidate(ByVal ParamArray indexes As Integer())
    End Interface
End Namespace