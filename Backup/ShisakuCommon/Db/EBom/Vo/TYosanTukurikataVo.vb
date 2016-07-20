Namespace Db.EBom.Vo
    ''' <summary>予算書イベント別造り方情報</summary>
    Public Class TYosanTukurikataVo
        ''' <summary>予算イベントコード</summary>
        Private _YosanEventCode As String
        ''' <summary>試作種別</summary>
        Private _ShisakuSyubetu As String
        ''' <summary>ユニット区分</summary>
        Private _UnitKbn As String
        '''' <summary>部品表名</summary>
        Private _BuhinhyoName As String
        ''' <summary>パターン名</summary>
        Private _PatternName As String
        ''' <summary>パターン表示順</summary>
        Private _PatternHyoujiJun As Nullable(Of Int32)
        ''' <summary>年月</summary>
        Private _YosanTukurikataYyyyMm As String
        ''' <summary>期</summary>
        Private _YosanTukurikataKs As String
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
        ''' <summary>
        ''' 部品表名
        ''' </summary>
        ''' <remarks></remarks>
        Public Property BuhinhyoName() As String
            Get
                Return _BuhinhyoName
            End Get
            Set(ByVal value As String)
                _BuhinhyoName = value
            End Set
        End Property
        ''' <summary>パターン名</summary>
        ''' <value>パターン名</value>
        ''' <returns>パターン名</returns>
        Public Property PatternName() As String
            Get
                Return _PatternName
            End Get
            Set(ByVal value As String)
                _PatternName = value
            End Set
        End Property
        ''' <summary>パターン表示順</summary>
        ''' <value>パターン表示順</value>
        ''' <returns>パターン表示順</returns>
        Public Property PatternHyoujiJun() As Nullable(Of Int32)
            Get
                Return _PatternHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _PatternHyoujiJun = value
            End Set
        End Property
        ''' <summary>年月</summary>
        ''' <value>年月</value>
        ''' <returns>年月</returns>
        Public Property YosanTukurikataYyyyMm() As String
            Get
                Return _YosanTukurikataYyyyMm
            End Get
            Set(ByVal value As String)
                _YosanTukurikataYyyyMm = value
            End Set
        End Property
        ''' <summary>期</summary>
        ''' <value>期</value>
        ''' <returns>期</returns>
        Public Property YosanTukurikataKs() As String
            Get
                Return _YosanTukurikataKs
            End Get
            Set(ByVal value As String)
                _YosanTukurikataKs = value
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