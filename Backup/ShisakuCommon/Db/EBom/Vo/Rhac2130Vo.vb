Namespace Db.EBom.Vo
    ''' <summary>
    ''' 社員マスター情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac2130Vo
        '' 社員番号  
        Private _ShainNo As String
        '' 部課コード  
        Private _BukaCode As String
        '' サイト区分  
        Private _SiteKbn As String
        '' 社員名称  
        Private _ShainName As String
        '' 英字社員名称  
        Private _ShainNameEiji As String
        '' 内線番号  
        Private _NaisenNo As String
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

        ''' <summary>社員番号</summary>
        ''' <value>社員番号</value>
        ''' <returns>社員番号</returns>
        Public Property ShainNo() As String
            Get
                Return _ShainNo
            End Get
            Set(ByVal value As String)
                _ShainNo = value
            End Set
        End Property

        ''' <summary>部課コード</summary>
        ''' <value>部課コード</value>
        ''' <returns>部課コード</returns>
        Public Property BukaCode() As String
            Get
                Return _BukaCode
            End Get
            Set(ByVal value As String)
                _BukaCode = value
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

        ''' <summary>社員名称</summary>
        ''' <value>社員名称</value>
        ''' <returns>社員名称</returns>
        Public Property ShainName() As String
            Get
                Return _ShainName
            End Get
            Set(ByVal value As String)
                _ShainName = value
            End Set
        End Property

        ''' <summary>英字社員名称</summary>
        ''' <value>英字社員名称</value>
        ''' <returns>英字社員名称</returns>
        Public Property ShainNameEiji() As String
            Get
                Return _ShainNameEiji
            End Get
            Set(ByVal value As String)
                _ShainNameEiji = value
            End Set
        End Property

        ''' <summary>内線番号</summary>
        ''' <value>内線番号</value>
        ''' <returns>内線番号</returns>
        Public Property NaisenNo() As String
            Get
                Return _NaisenNo
            End Get
            Set(ByVal value As String)
                _NaisenNo = value
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
