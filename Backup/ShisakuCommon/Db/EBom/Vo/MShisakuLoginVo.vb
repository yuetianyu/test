Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作メニューログインマスター
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MShisakuLoginVo
        '' 設計社員番号  
        Private _SekkeiShainNo As String
        '' パスワード  
        Private _Password As String
        '' ユーザー区分  
        Private _UserKbn As String
        '' 試作メニュー設定  
        Private _ShisakuMenuSettei As String
        '' 権限設定  
        Private _KengenSettei As String
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

        ''' <summary>設計社員番号</summary>
        ''' <value>設計社員番号</value>
        ''' <returns>設計社員番号</returns>
        Public Property SekkeiShainNo() As String
            Get
                Return _SekkeiShainNo
            End Get
            Set(ByVal value As String)
                _SekkeiShainNo = value
            End Set
        End Property

        ''' <summary>パスワード</summary>
        ''' <value>パスワード</value>
        ''' <returns>パスワード</returns>
        Public Property Password() As String
            Get
                Return _Password
            End Get
            Set(ByVal value As String)
                _Password = value
            End Set
        End Property

        ''' <summary>ユーザー区分</summary>
        ''' <value>ユーザー区分</value>
        ''' <returns>ユーザー区分</returns>
        Public Property UserKbn() As String
            Get
                Return _UserKbn
            End Get
            Set(ByVal value As String)
                _UserKbn = value
            End Set
        End Property

        ''' <summary>試作メニュー設定</summary>
        ''' <value>試作メニュー設定</value>
        ''' <returns>試作メニュー設定</returns>
        Public Property ShisakuMenuSettei() As String
            Get
                Return _ShisakuMenuSettei
            End Get
            Set(ByVal value As String)
                _ShisakuMenuSettei = value
            End Set
        End Property

        ''' <summary>権限設定</summary>
        ''' <value>権限設定</value>
        ''' <returns>権限設定</returns>
        Public Property KengenSettei() As String
            Get
                Return _KengenSettei
            End Get
            Set(ByVal value As String)
                _KengenSettei = value
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
