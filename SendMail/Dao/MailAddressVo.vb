Namespace SendMail.Dao
    ''' <summary>
    ''' ユーザーメールアドレス情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MailAddressVo
        '' ユーザーＩＤ 
        Private _UserId As String
        '' メールアドレス 
        Private _MailAddress As String
        '' 所属 
        Private _Description As String

        ''' <summary>ユーザーＩＤ</summary>
        ''' <value>ユーザーＩＤ</value>
        ''' <returns>ユーザーＩＤ</returns>
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

        ''' <summary>所属</summary>
        ''' <value>所属</value>
        ''' <returns>所属</returns>
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property
    End Class
End Namespace
