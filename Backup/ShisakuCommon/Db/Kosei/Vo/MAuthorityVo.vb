Namespace Db.Kosei.Vo
    ''' <summary>
    ''' 権限
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MAuthorityVo
        '' メニュー大区分  
        Private _MenuDaiKbn As Nullable(of Int32)
        '' メニュー区分  
        Private _MenuKbn As Nullable(of Int32)
        '' ユーザ区分  
        Private _UserKbn As Nullable(of Int32)
        '' 権限  
        Private _AuthorityKbn As Nullable(of Int32)
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

        ''' <summary>メニュー大区分</summary>
        ''' <value>メニュー大区分</value>
        ''' <returns>メニュー大区分</returns>
        Public Property MenuDaiKbn() As Nullable(of Int32)
            Get
                Return _MenuDaiKbn
            End Get
            Set(ByVal value As Nullable(of Int32))
                _MenuDaiKbn = value
            End Set
        End Property

        ''' <summary>メニュー区分</summary>
        ''' <value>メニュー区分</value>
        ''' <returns>メニュー区分</returns>
        Public Property MenuKbn() As Nullable(of Int32)
            Get
                Return _MenuKbn
            End Get
            Set(ByVal value As Nullable(of Int32))
                _MenuKbn = value
            End Set
        End Property

        ''' <summary>ユーザ区分</summary>
        ''' <value>ユーザ区分</value>
        ''' <returns>ユーザ区分</returns>
        Public Property UserKbn() As Nullable(of Int32)
            Get
                Return _UserKbn
            End Get
            Set(ByVal value As Nullable(of Int32))
                _UserKbn = value
            End Set
        End Property

        ''' <summary>権限</summary>
        ''' <value>権限</value>
        ''' <returns>権限</returns>
        Public Property AuthorityKbn() As Nullable(of Int32)
            Get
                Return _AuthorityKbn
            End Get
            Set(ByVal value As Nullable(of Int32))
                _AuthorityKbn = value
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
