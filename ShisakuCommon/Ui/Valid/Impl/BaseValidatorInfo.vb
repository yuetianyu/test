Namespace Ui.Valid.Impl
    ''' <summary>
    ''' Validateチェック管理クラスの情報
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class BaseValidatorInfo
        ''' <summary>エラーメッセージを子要素に引き継ぐ</summary>
        Friend IsTransmitMessageChildren As Boolean
        ''' <summary>エラーメッセージ</summary>
        Friend ErrorMessage As String
    End Class
End Namespace