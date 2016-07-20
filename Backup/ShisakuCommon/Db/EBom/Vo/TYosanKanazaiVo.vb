Namespace Db.EBom.Vo
    ''' <summary>予算書イベント別金材情報</summary>
    Public Class TYosanKanazaiVo
        ''' <summary>予算イベントコード</summary>
        Private _YosanEventCode As String
        ''' <summary>金材項目名</summary>
        Private _KanazaiName As String
        ''' <summary>金材項目表示順</summary>
        Private _KanazaiHyoujiJun As Nullable(Of Int32)
        ''' <summary>年月</summary>
        Private _YosanTukurikataYyyyMm As String
        ''' <summary>期</summary>
        Private _YosanTukurikataKs As String
        ''' <summary>台数</summary>
        Private _DaisuSuryo As Nullable(Of Int32)
        ''' <summary>単価</summary>
        Private _KanazaiUnitPrice As Nullable(Of Decimal)
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
        ''' <summary>金材項目名</summary>
        ''' <value>金材項目名</value>
        ''' <returns>金材項目名</returns>
        Public Property KanazaiName() As String
            Get
                Return _KanazaiName
            End Get
            Set(ByVal value As String)
                _KanazaiName = value
            End Set
        End Property
        ''' <summary>金材項目表示順</summary>
        ''' <value>金材項目表示順</value>
        ''' <returns>金材項目表示順</returns>
        Public Property KanazaiHyoujiJun() As Nullable(Of Int32)
            Get
                Return _KanazaiHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _KanazaiHyoujiJun = value
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
        ''' <summary>単価</summary>
        ''' <value>単価</value>
        ''' <returns>単価</returns>
        Public Property KanazaiUnitPrice() As Nullable(Of Decimal)
            Get
                Return _KanazaiUnitPrice
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _KanazaiUnitPrice = value
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