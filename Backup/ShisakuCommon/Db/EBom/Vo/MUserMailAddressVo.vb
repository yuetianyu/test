Namespace Db.EBom.Vo
    ''' <summary>ユーザーメールアドレスマスタ</summary>
    Public Class MUserMailAddressVo
        ''' <summary>ユーザーID</summary>
        Private _UserId As String
        ''' <summary>メールアドレス</summary>
        Private _MailAddress As String
        ''' <summary>メール区分</summary>
        Private _MailKbn As String
        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        ''' <summary>作成日</summary>
        Private _CreatedDate As String
        ''' <summary>作成時</summary>
        Private _CreatedTime As String
        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        ''' <summary>更新日</summary>
        Private _UpdatedDate As String
        ''' <summary>更新時間</summary>
        Private _UpdatedTime As String

        ''' <summary>ユーザーID</summary>
        ''' <value>ユーザーID</value>
        ''' <returns>ユーザーID</returns>
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property
        ''' <summary>メールアドレス</summary>
        ''' <value>メールアドレス</value>
        ''' <returns>メールアドレス</returns>
        Public Property MailAddress() As String
            Get
                Return _MailAddress
            End Get
            Set(ByVal value As String)
                _MailAddress = value
            End Set
        End Property
        ''' <summary>メール区分</summary>
        ''' <value>メール区分</value>
        ''' <returns>メール区分</returns>
        Public Property MailKbn() As String
            Get
                Return _MailKbn
            End Get
            Set(ByVal value As String)
                _MailKbn = value
            End Set
        End Property
        ''' <summary>作成ユーザーID</summary>
        ''' <value>作成ユーザーID</value>
        ''' <returns>作成ユーザーID</returns>
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property
        ''' <summary>作成日</summary>
        ''' <value>作成日</value>
        ''' <returns>作成日</returns>
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property
        ''' <summary>作成時</summary>
        ''' <value>作成時</value>
        ''' <returns>作成時</returns>
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property
        ''' <summary>更新ユーザーID</summary>
        ''' <value>更新ユーザーID</value>
        ''' <returns>更新ユーザーID</returns>
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property
        ''' <summary>更新日</summary>
        ''' <value>更新日</value>
        ''' <returns>更新日</returns>
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property
        ''' <summary>更新時間</summary>
        ''' <value>更新時間</value>
        ''' <returns>更新時間</returns>
        Public Property UpdatedTime() As String
            Get
                Return _UpdatedTime
            End Get
            Set(ByVal value As String)
                _UpdatedTime = value
            End Set
        End Property
    End Class
End Namespace