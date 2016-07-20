''' <summary>
''' マスター一覧用検索条件
''' </summary>
''' <remarks></remarks>
Public Class KengenSearchConditionVo

#Region " メンバー変数 "
    ''' <summary>ユーザーID</summary>
    Private m_shainNo As String

    ''' <summary>ユーザー名</summary>
    Private m_shainName As String

    ''' <summary>部課</summary>
    Private m_buka As String

    ''' <summary>サイト</summary>
    Private m_site As String

    ''' <summary>区分</summary>
    Private m_competent As String
#End Region

#Region " ユーザーID "
    ''' <summary>
    ''' ユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShainNo() As String
        Get
            Return m_shainNo
        End Get
        Set(ByVal value As String)
            m_shainNo = value
        End Set
    End Property
#End Region

#Region " ユーザー名 "
    ''' <summary>
    ''' ユーザー名    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShainName() As String
        Get
            Return m_shainName
        End Get
        Set(ByVal value As String)
            m_shainName = value
        End Set
    End Property
#End Region

#Region " 部課 "
    ''' <summary>
    ''' 部課
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Buka() As String
        Get
            Return m_buka
        End Get
        Set(ByVal value As String)
            m_buka = value
        End Set
    End Property
#End Region

#Region " サイト "
    ''' <summary>
    ''' サイト    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Site() As String
        Get
            Return m_site
        End Get
        Set(ByVal value As String)
            m_site = value
        End Set
    End Property
#End Region

#Region " 区分 "
    ''' <summary>
    ''' 区分    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Competent() As String
        Get
            Return m_competent
        End Get
        Set(ByVal value As String)
            m_competent = value
        End Set
    End Property
#End Region
End Class
