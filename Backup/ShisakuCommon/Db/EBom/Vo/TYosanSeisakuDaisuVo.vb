Namespace Db.EBom.Vo
    ''' <summary>予算書イベント別製作台数情報</summary>
    Public Class TYosanSeisakuDaisuVo
        ''' <summary>予算イベントコード</summary>
        Private _YosanEventCode As String
        ''' <summary>試作種別</summary>
        Private _ShisakuSyubetu As String
        ''' <summary>工事指令№表示順</summary>
        Private _KoujiShireiNoHyojijunNo As Nullable(Of Int32)
        ''' <summary>ユニット区分</summary>
        Private _UnitKbn As String
        ''' <summary>工事指令№</summary>
        Private _KoujiShireiNo As String
        ''' <summary>台数</summary>
        Private _DaisuSuryo As Nullable(Of Int32)
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
        ''' <summary>試作種別</summary>
        ''' <value>試作種別</value>
        ''' <returns>試作種別</returns>
        Public Property ShisakuSyubetu() As String
            Get
                Return _ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                _ShisakuSyubetu = value
            End Set
        End Property
        ''' <summary>工事指令№表示順</summary>
        ''' <value>工事指令№表示順</value>
        ''' <returns>工事指令№表示順</returns>
        Public Property KoujiShireiNoHyojijunNo() As Nullable(Of Int32)
            Get
                Return _KoujiShireiNoHyojijunNo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _KoujiShireiNoHyojijunNo = value
            End Set
        End Property
        ''' <summary>ユニット区分</summary>
        ''' <value>ユニット区分</value>
        ''' <returns>ユニット区分</returns>
        Public Property UnitKbn() As String
            Get
                Return _UnitKbn
            End Get
            Set(ByVal value As String)
                _UnitKbn = value
            End Set
        End Property
        ''' <summary>工事指令№</summary>
        ''' <value>工事指令№</value>
        ''' <returns>工事指令№</returns>
        Public Property KoujiShireiNo() As String
            Get
                Return _KoujiShireiNo
            End Get
            Set(ByVal value As String)
                _KoujiShireiNo = value
            End Set
        End Property
        ''' <summary>台数</summary>
        ''' <value>台数</value>
        ''' <returns>台数</returns>
        Public Property DaisuSuryo() As Nullable(Of Int32)
            Get
                Return _DaisuSuryo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _DaisuSuryo = value
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