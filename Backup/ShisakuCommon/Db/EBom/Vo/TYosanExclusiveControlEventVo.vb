Namespace Db.EBom.Vo
    ''' <summary>排他管理予算書イベント情報</summary>
    Public Class TYosanExclusiveControlEventVo
        ''' <summary>予算イベントコード</summary>
        Private _YosanEventCode As String
        ''' <summary>編集者職番</summary>
        Private _EditUserId As String
        ''' <summary>編集開始日</summary>
        Private _EditDate As String
        ''' <summary>編集開始時間</summary>
        Private _EditTime As String
        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        ''' <summary>作成日</summary>
        Private _CreatedDate As String
        ''' <summary>作成時</summary>
        Private _CreatedTime As String
        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        ''' <summary>更新日</summary>
        Private _UpdatedDate As String
        ''' <summary>更新時間</summary>
        Private _UpdatedTime As String

        ''' <summary>予算イベントコード</summary>
        ''' <value>予算イベントコード</value>
        ''' <returns>予算イベントコード</returns>
        Public Property YosanEventCode() As String
            Get
                Return _YosanEventCode
            End Get
            Set(ByVal value As String)
                _YosanEventCode = value
            End Set
        End Property
        ''' <summary>編集者職番</summary>
        ''' <value>編集者職番</value>
        ''' <returns>編集者職番</returns>
        Public Property EditUserId() As String
            Get
                Return _EditUserId
            End Get
            Set(ByVal value As String)
                _EditUserId = value
            End Set
        End Property
        ''' <summary>編集開始日</summary>
        ''' <value>編集開始日</value>
        ''' <returns>編集開始日</returns>
        Public Property EditDate() As String
            Get
                Return _EditDate
            End Get
            Set(ByVal value As String)
                _EditDate = value
            End Set
        End Property
        ''' <summary>編集開始時間</summary>
        ''' <value>編集開始時間</value>
        ''' <returns>編集開始時間</returns>
        Public Property EditTime() As String
            Get
                Return _EditTime
            End Get
            Set(ByVal value As String)
                _EditTime = value
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