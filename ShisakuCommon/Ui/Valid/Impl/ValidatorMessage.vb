Namespace Ui.Valid.Impl
    ''' <summary>
    ''' Validateメッセージを担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class ValidatorMessage
        Private _baseInfo As BaseValidatorInfo
        Private _localMessage As String
        Public DefaultMessage As String
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(New BaseValidatorInfo, Nothing)
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="baseInfo">Validateチェック管理クラスの情報</param>
        ''' <param name="localMessage">個別メッセージ</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal baseInfo As BaseValidatorInfo, ByVal localMessage As String)
            Me._baseInfo = baseInfo
            Me._localMessage = localMessage
        End Sub

        ''' <summary>
        ''' 現在有効なエラーメッセージ
        ''' </summary>
        Public ReadOnly Property CurrentMessage()
            Get
                If _baseInfo.IsTransmitMessageChildren AndAlso _baseInfo.ErrorMessage IsNot Nothing Then
                    Return _baseInfo.ErrorMessage
                End If
                If _localMessage IsNot Nothing Then
                    Return _localMessage
                End If
                Return DefaultMessage
            End Get
        End Property
    End Class
End Namespace