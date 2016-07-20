Namespace Db.EBom.Vo
    ''' <summary>予算書部品表選択情報</summary>
    Public Class TYosanBuhinSelectVo
        ''' <summary>予算イベントコード</summary>

        Private _YosanEventCode As String
        Public Property YosanEventCode() As String
            Get
                Return _YosanEventCode
            End Get
            Set(ByVal value As String)
                _YosanEventCode = value
            End Set
        End Property
        ''' <summary>部品表名</summary>

        Private _BuhinhyoName As String
        Public Property BuhinhyoName() As String
            Get
                Return _BuhinhyoName
            End Get
            Set(ByVal value As String)
                _BuhinhyoName = value
            End Set
        End Property
        '' <summary>作成ユーザーID</summary>

        Private _CreatedUserId As String
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property
        ''' <summary>作成年月日</summary>

        Private _CreatedDate As String
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property
        ''' <summary>作成時分秒</summary>

        Private _CreatedTime As String
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property
        ''' <summary>
        ''' 更新ユーザーID
        ''' </summary>
        ''' <remarks></remarks>
        Private _UpdatedUserId As String
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property

        ''' <summary>
        ''' 更新年月日
        ''' </summary>
        ''' <remarks></remarks>
        Private _UpdatedDate As String
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property
        ''' <summary>
        ''' 更新時分秒
        ''' </summary>
        ''' <remarks></remarks>
        Private _UpdatedTime As String
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


