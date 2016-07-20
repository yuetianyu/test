Namespace Db.EBom.Vo

    ''' <summary>
    ''' 予算設定部品表_履歴
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TYosanSetteiBuhinRirekiVo

        ''' <summary>試作イベントコード</summary>
        Private _ShisakuEventCode As String
        ''' <summary>予算リストコード</summary>
        Private _YosanListCode As String
        ''' <summary>予算部課コード</summary>
        Private _YosanBukaCode As String
        ''' <summary>予算ブロック</summary>
        Private _YosanBlockNo As String
        ''' <summary>予算部品番号</summary>
        Private _YosanBuhinNo As String
        ''' <summary>部品番号表示順</summary>
        Private _BuhinNoHyoujiJun As Nullable(Of Int32)
        ''' <summary>列ID</summary>
        Private _ColumnId As String
        ''' <summary>列名</summary>
        Private _ColumnName As String
        ''' <summary>変更日</summary>
        Private _UpdateBi As String
        ''' <summary>変更時間</summary>
        Private _UpdateJikan As String
        ''' <summary>変更前</summary>
        Private _Before As String
        ''' <summary>変更後</summary>
        Private _After As String
        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        ''' <summary>作成年月日</summary>
        Private _CreatedDate As String
        ''' <summary>作成時分秒</summary>
        Private _CreatedTime As String
        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        ''' <summary>更新年月日</summary>
        Private _UpdatedDate As String
        ''' <summary>更新時分秒</summary>
        Private _UpdatedTime As String



        ''' <summary>
        ''' 試作イベントコード
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>
        ''' 予算リストコード
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property YosanListCode() As String
            Get
                Return _YosanListCode
            End Get
            Set(ByVal value As String)
                _YosanListCode = value
            End Set
        End Property

        ''' <summary>
        ''' 予算部課コード
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property YosanBukaCode() As String
            Get
                Return _YosanBukaCode
            End Get
            Set(ByVal value As String)
                _YosanBukaCode = value
            End Set
        End Property

        ''' <summary>
        ''' 予算ブロック№
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property YosanBlockNo() As String
            Get
                Return _YosanBlockNo
            End Get
            Set(ByVal value As String)
                _YosanBlockNo = value
            End Set
        End Property

        ''' <summary>
        ''' 予算部品番号
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property YosanBuhinNo() As String
            Get
                Return _YosanBuhinNo
            End Get
            Set(ByVal value As String)
                _YosanBuhinNo = value
            End Set
        End Property

        ''' <summary>
        ''' 部品番号表示順
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BuhinNoHyoujiJun() As Nullable(Of Int32)
            Get
                Return _BuhinNoHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _BuhinNoHyoujiJun = value
            End Set
        End Property

        ''' <summary>
        ''' 列ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ColumnId() As String
            Get
                Return _ColumnId
            End Get
            Set(ByVal value As String)
                _ColumnId = value
            End Set
        End Property

        ''' <summary>
        ''' 列名
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ColumnName() As String
            Get
                Return _ColumnName
            End Get
            Set(ByVal value As String)
                _ColumnName = value
            End Set
        End Property

        ''' <summary>
        ''' 変更日
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UpdateBi() As String
            Get
                Return _UpdateBi
            End Get
            Set(ByVal value As String)
                _UpdateBi = value
            End Set
        End Property

        ''' <summary>
        ''' 変更時間
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UpdateJikan() As String
            Get
                Return _UpdateJikan
            End Get
            Set(ByVal value As String)
                _UpdateJikan = value
            End Set
        End Property

        ''' <summary>
        ''' 変更前
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Before() As String
            Get
                Return _Before
            End Get
            Set(ByVal value As String)
                _Before = value
            End Set
        End Property

        ''' <summary>
        ''' 変更後
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property After() As String
            Get
                Return _After
            End Get
            Set(ByVal value As String)
                _After = value
            End Set
        End Property

        ''' <summary>
        ''' 作成ユーザーID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property

        ''' <summary>
        ''' 作成年月日
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property

        ''' <summary>
        ''' 作成時分秒
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
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
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
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
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
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
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
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
