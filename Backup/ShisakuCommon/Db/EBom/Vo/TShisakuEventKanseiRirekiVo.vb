Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作イベント完成車履歴情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuEventKanseiRirekiVo
        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 表示順
        Private _HyojijunNo As Nullable(of Int32)
        '' 列ID
        Private _ColumnId As String
        '' 列名
        Private _ColumnName As String
        '' 更新日
        Private _UpdateBi As String
        '' 更新時間
        Private _UpdateJikan As String
        '' 更新前情報
        Private _Before As String
        '' 更新後情報
        Private _After As String
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

        ''' <summary>試作イベントコード</summary>
        ''' <value>試作イベントコード</value>
        ''' <returns>試作イベントコード</returns>
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>表示順</summary>
        ''' <value>表示順</value>
        ''' <returns>表示順</returns>
        Public Property HyojijunNo() As Nullable(of Int32)
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _HyojijunNo = value
            End Set
        End Property

        ''' <summary>列ID</summary>
        ''' <value>列ID</value>
        ''' <returns>列ID</returns>
        Public Property ColumnId() As String
            Get
                Return _ColumnId
            End Get
            Set(ByVal value As String)
                _ColumnId = value
            End Set
        End Property

        ''' <summary>列名</summary>
        ''' <value>列名</value>
        ''' <returns>列名</returns>
        Public Property ColumnName() As String
            Get
                Return _ColumnName
            End Get
            Set(ByVal value As String)
                _ColumnName = value
            End Set
        End Property

        ''' <summary>変更日</summary>
        ''' <value>変更日</value>
        ''' <returns>変更日</returns>
        Public Property UpdateBi() As String
            Get
                Return _UpdateBi
            End Get
            Set(ByVal value As String)
                _UpdateBi = value
            End Set
        End Property

        ''' <summary>変更時間</summary>
        ''' <value>変更時間</value>
        ''' <returns>変更時間</returns>
        Public Property UpdateJikan() As String
            Get
                Return _UpdateJikan
            End Get
            Set(ByVal value As String)
                _UpdateJikan = value
            End Set
        End Property

        ''' <summary>更新前情報</summary>
        ''' <value>更新前情報</value>
        ''' <returns>更新前情報</returns>
        Public Property Before() As String
            Get
                Return _Before
            End Get
            Set(ByVal value As String)
                _Before = value
            End Set
        End Property

        ''' <summary>更新後情報</summary>
        ''' <value>更新後情報</value>
        ''' <returns>更新後情報</returns>
        Public Property After() As String
            Get
                Return _After
            End Get
            Set(ByVal value As String)
                _After = value
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
