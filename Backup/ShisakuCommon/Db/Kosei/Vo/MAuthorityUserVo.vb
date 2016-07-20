Namespace Db.Kosei.Vo
    ''' <summary>
    ''' 権限(ユーザー別)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MAuthorityUserVo
        '' アプリケーション管理No.  
        Private _AppNo As String
        '' 機能ID1  
        Private _KinoId1 As String
        '' 機能ID2  
        Private _KinoId2 As String
        '' 機能ID3  
        Private _KinoId3 As String
        '' ﾕｰｻﾞｰID  
        Private _UserId As String
        '' 権限区分  
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

        ''' <summary>アプリケーション管理No.</summary>
        ''' <value>アプリケーション管理No.</value>
        ''' <returns>アプリケーション管理No.</returns>
        Public Property AppNo() As String
            Get
                Return _AppNo
            End Get
            Set(ByVal value As String)
                _AppNo = value
            End Set
        End Property

        ''' <summary>機能ID1</summary>
        ''' <value>機能ID1</value>
        ''' <returns>機能ID1</returns>
        Public Property KinoId1() As String
            Get
                Return _KinoId1
            End Get
            Set(ByVal value As String)
                _KinoId1 = value
            End Set
        End Property

        ''' <summary>機能ID2</summary>
        ''' <value>機能ID2</value>
        ''' <returns>機能ID2</returns>
        Public Property KinoId2() As String
            Get
                Return _KinoId2
            End Get
            Set(ByVal value As String)
                _KinoId2 = value
            End Set
        End Property

        ''' <summary>機能ID3</summary>
        ''' <value>機能ID3</value>
        ''' <returns>機能ID3</returns>
        Public Property KinoId3() As String
            Get
                Return _KinoId3
            End Get
            Set(ByVal value As String)
                _KinoId3 = value
            End Set
        End Property

        ''' <summary>ﾕｰｻﾞｰID</summary>
        ''' <value>ﾕｰｻﾞｰID</value>
        ''' <returns>ﾕｰｻﾞｰID</returns>
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property

        ''' <summary>権限区分</summary>
        ''' <value>権限区分</value>
        ''' <returns>権限区分</returns>
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
