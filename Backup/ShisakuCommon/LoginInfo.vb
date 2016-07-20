''' <summary>
''' ログインユーザー情報クラス
''' </summary>
''' <remarks></remarks>
Public Class LoginInfo

    ''' <summary>ログイン中情報</summary>
    Public Shared ReadOnly Now As New LoginInfo

    '' ログインユーザー
    Private _UserId As String
    '' ログイン社員名称
    Private _ShainName As String
    '' ログイン社員部課コード
    Private _BukaCode As String
    '' ログイン社員部課名
    Private _bukaName As String
    '' ログイン社員部課略称名
    Private _bukaRyakuName As String
    '' ログインユーザーの権限
    Private _LoginAuthority As String
    '' ログインユーザーの試作メニュー設定（0：権限無し、1：権限有り）
    Private _MenuSettei As String
    '' ログインユーザーの権限設定（0：権限無し、1：権限有り）
    Private _KengenSettei As String

    ''' <summary>ログインユーザー</summary>
    ''' <value>ログインユーザー</value>
    ''' <returns>ログインユーザー</returns>
    Public Property UserId() As String
        Get
            Return _UserId
        End Get
        Set(ByVal value As String)
            _UserId = value
        End Set
    End Property

    ''' <summary>ログイン社員名称</summary>
    ''' <value>ログイン社員名称</value>
    ''' <returns>ログイン社員名称</returns>
    Public Property ShainName() As String
        Get
            Return _ShainName
        End Get
        Set(ByVal value As String)
            _ShainName = value
        End Set
    End Property

    ''' <summary>ログイン社員部課コード</summary>
    ''' <value>ログイン社員部課コード</value>
    ''' <returns>ログイン社員部課コード</returns>
    Public Property BukaCode() As String
        Get
            Return _BukaCode
        End Get
        Set(ByVal value As String)
            _BukaCode = value
        End Set
    End Property

    ''' <summary>ログイン社員部課名</summary>
    ''' <value>ログイン社員部課名</value>
    ''' <returns>ログイン社員部課名</returns>
    Public Property BukaName() As String
        Get
            Return _bukaName
        End Get
        Set(ByVal value As String)
            _bukaName = value
        End Set
    End Property

    ''' <summary>ログイン社員部課略称名</summary>
    ''' <value>ログイン社員部課略称名</value>
    ''' <returns>ログイン社員部課略称名</returns>
    Public Property BukaRyakuName() As String
        Get
            Return _bukaRyakuName
        End Get
        Set(ByVal value As String)
            _bukaRyakuName = value
        End Set
    End Property

    ''' <summary>ログインユーザーの権限</summary>
    ''' <value>ログインユーザーの権限</value>
    ''' <returns>ログインユーザーの権限</returns>
    Public Property LoginAuthority() As String
        Get
            Return _LoginAuthority
        End Get
        Set(ByVal value As String)
            _LoginAuthority = value
        End Set
    End Property

    ''' <summary>ログインユーザーの試作メニュー設定（0：権限無し、1：権限有り）</summary>
    ''' <value>ログインユーザーの試作メニュー設定（0：権限無し、1：権限有り）</value>
    ''' <returns>ログインユーザーの試作メニュー設定（0：権限無し、1：権限有り）</returns>
    Public Property MenuSettei() As String
        Get
            Return _MenuSettei
        End Get
        Set(ByVal value As String)
            _MenuSettei = value
        End Set
    End Property

    ''' <summary>ログインユーザーの権限設定（0：権限無し、1：権限有り）</summary>
    ''' <value>ログインユーザーの権限設定（0：権限無し、1：権限有り）</value>
    ''' <returns>ログインユーザーの権限設定（0：権限無し、1：権限有り）</returns>
    Public Property KengenSettei() As String
        Get
            Return _KengenSettei
        End Get
        Set(ByVal value As String)
            _KengenSettei = value
        End Set
    End Property
#Region "読み取り専用プロパティGet/Set"
    ''' <summary>ログイン社員部コード</summary>
    ''' <value>ログイン社員部コード</value>
    ''' <returns>ログイン社員部コード</returns>
    Public ReadOnly Property BuCode() As String
        Get
            If StringUtil.IsEmpty(_BukaCode) OrElse _BukaCode.Length < 4 Then
                Return Nothing
            Else
                Return _BukaCode.Substring(0, 2)
            End If
        End Get
    End Property
    ''' <summary>ログイン社員課コード</summary>
    ''' <value>ログイン社員課コード</value>
    ''' <returns>ログイン社員課コード</returns>
    Public ReadOnly Property KaCode() As String
        Get
            If StringUtil.IsEmpty(_BukaCode) OrElse _BukaCode.Length < 4 Then
                Return Nothing
            Else
                Return _BukaCode.Substring(2)
            End If
        End Get
    End Property
#End Region

End Class
