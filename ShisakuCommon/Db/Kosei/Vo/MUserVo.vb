Namespace Db.Kosei.Vo
    ''' <summary>
    ''' ユーザー
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MUserVo
        '' ユーザID  
        Private _UserId As String
        '' 有効期限  
        Private _TermValidity As Nullable(of DateTime)
        '' サイト区分  
        Private _SiteInfo As Nullable(of Int32)
        '' ユーザフラグ  
        Private _UserFlg As Nullable(of Int32)
        '' ユーザ権限  
        Private _Competent As Nullable(of Int32)
        '' 作成ユーザーID
        Private _CreatedUserId As String
        '' 作成日
        Private _CreatedDate As String
        '' 作成時
        Private _CreatedTime As String
        '' 更新ユーザーID
        Private _UpdatedUserId As String
        '' 更新日
        Private _UpdatedDate As String
        '' 更新時間
        Private _UpdatedTime As String

        ''' <summary>ユーザID</summary>
        ''' <value>ユーザID</value>
        ''' <returns>ユーザID</returns>
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property

        ''' <summary>有効期限</summary>
        ''' <value>有効期限</value>
        ''' <returns>有効期限</returns>
        Public Property TermValidity() As Nullable(of DateTime)
            Get
                Return _TermValidity
            End Get
            Set(ByVal value As Nullable(of DateTime))
                _TermValidity = value
            End Set
        End Property

        ''' <summary>サイト区分</summary>
        ''' <value>サイト区分</value>
        ''' <returns>サイト区分</returns>
        Public Property SiteInfo() As Nullable(of Int32)
            Get
                Return _SiteInfo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SiteInfo = value
            End Set
        End Property

        ''' <summary>ユーザフラグ</summary>
        ''' <value>ユーザフラグ</value>
        ''' <returns>ユーザフラグ</returns>
        Public Property UserFlg() As Nullable(of Int32)
            Get
                Return _UserFlg
            End Get
            Set(ByVal value As Nullable(of Int32))
                _UserFlg = value
            End Set
        End Property

        ''' <summary>ユーザ権限</summary>
        ''' <value>ユーザ権限</value>
        ''' <returns>ユーザ権限</returns>
        Public Property Competent() As Nullable(of Int32)
            Get
                Return _Competent
            End Get
            Set(ByVal value As Nullable(of Int32))
                _Competent = value
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
