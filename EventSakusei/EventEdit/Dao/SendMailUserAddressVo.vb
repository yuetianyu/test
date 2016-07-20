Namespace EventEdit.Dao
    ''' <summary>メール送信ユーザーアドレス情報</summary>
    Public Class SendMailUserAddressVo
        ''' <summary>イベントコード</summary>
        Private _EventCode As String
        ''' <summary>ユーザーID</summary>
        Private _UserId As String
        ''' <summary>ユーザー名</summary>
        Private _UserName As String
        ''' <summary>メールアドレス</summary>
        Private _MailAddress As String

        ''' <summary>イベントコード</summary>
        ''' <value>イベントコード</value>
        ''' <returns>イベントコード</returns>
        Public Property EventCode() As String
            Get
                Return _EventCode
            End Get
            Set(ByVal value As String)
                _EventCode = value
            End Set
        End Property
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
        ''' <summary>ユーザー名</summary>
        ''' <value>ユーザー名</value>
        ''' <returns>ユーザー名</returns>
        Public Property UserName() As String
            Get
                Return _UserName
            End Get
            Set(ByVal value As String)
                _UserName = value
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
    End Class
End Namespace