Namespace Db.EBom.Vo
    ''' <summary>
    ''' 課マスタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac1560Vo
        '' 部コード  
        Private _BuCode As String
        '' サイト区分  
        Private _SiteKbn As String
        '' 課コード  
        Private _KaCode As String
        '' 課名称  
        Private _KaName As String
        '' 課略称  
        Private _KaRyakuName As String
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

        ''' <summary>部コード</summary>
        ''' <value>部コード</value>
        ''' <returns>部コード</returns>
        Public Property BuCode() As String
            Get
                Return _BuCode
            End Get
            Set(ByVal value As String)
                _BuCode = value
            End Set
        End Property

        ''' <summary>サイト区分</summary>
        ''' <value>サイト区分</value>
        ''' <returns>サイト区分</returns>
        Public Property SiteKbn() As String
            Get
                Return _SiteKbn
            End Get
            Set(ByVal value As String)
                _SiteKbn = value
            End Set
        End Property

        ''' <summary>課コード</summary>
        ''' <value>課コード</value>
        ''' <returns>課コード</returns>
        Public Property KaCode() As String
            Get
                Return _KaCode
            End Get
            Set(ByVal value As String)
                _KaCode = value
            End Set
        End Property

        ''' <summary>課名称</summary>
        ''' <value>課名称</value>
        ''' <returns>課名称</returns>
        Public Property KaName() As String
            Get
                Return _KaName
            End Get
            Set(ByVal value As String)
                _KaName = value
            End Set
        End Property

        ''' <summary>課略称</summary>
        ''' <value>課略称</value>
        ''' <returns>課略称</returns>
        Public Property KaRyakuName() As String
            Get
                Return _KaRyakuName
            End Get
            Set(ByVal value As String)
                _KaRyakuName = value
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
