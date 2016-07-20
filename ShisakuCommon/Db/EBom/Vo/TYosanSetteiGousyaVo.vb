Namespace Db.EBom.Vo
    ''' <summary>
    ''' 製品区分マスター情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TYosanSetteiGousyaVo
        ''' <summary>試作イベントコード</summary>
        Private _ShisakuEventCode As String
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property
        ''' <summary>予算リストコード</summary>

        Private _YosanListCode As String
        Public Property YosanListCode() As String
            Get
                Return _YosanListCode
            End Get
            Set(ByVal value As String)
                _YosanListCode = value
            End Set
        End Property
        ''' <summary>予算部課コード</summary>

        Private _YosanBukaCode As String
        Public Property YosanBukaCode() As String
            Get
                Return _YosanBukaCode
            End Get
            Set(ByVal value As String)
                _YosanBukaCode = value
            End Set
        End Property
        ''' <summary>予算ブロック№</summary>

        Private _YosanBlockNo As String
        Public Property YosanBlockNo() As String
            Get
                Return _YosanBlockNo
            End Get
            Set(ByVal value As String)
                _YosanBlockNo = value
            End Set
        End Property
        ''' <summary>部品番号表示順</summary>

        Private _BuhinNoHyoujiJun As Nullable(Of Int32)
        Public Property BuhinNoHyoujiJun() As Nullable(Of Int32)
            Get
                Return _BuhinNoHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _BuhinNoHyoujiJun = value
            End Set
        End Property
        ''' <summary>ソート順</summary>

        Private _SortJun As Nullable(Of Int32)
        Public Property SortJun() As Nullable(Of Int32)
            Get
                Return _SortJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SortJun = value
            End Set
        End Property
        ''' <summary>行ID</summary>

        Private _GyouId As String
        Public Property GyouId() As String
            Get
                Return _GyouId
            End Get
            Set(ByVal value As String)
                _GyouId = value
            End Set
        End Property
        ''' <summary>試作号車表示順</summary>

        Private _YosanGousyaHyoujiJun As Nullable(Of Int32)
        Public Property YosanGousyaHyoujiJun() As Nullable(Of Int32)
            Get
                Return _YosanGousyaHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanGousyaHyoujiJun = value
            End Set
        End Property
        ''' <summary>試作号車</summary>

        Private _YosanGousya As String
        Public Property YosanGousya() As String
            Get
                Return _YosanGousya
            End Get
            Set(ByVal value As String)
                _YosanGousya = value
            End Set
        End Property
        ''' <summary>員数</summary>

        Private _InsuSuryo As Nullable(Of Int32)
        Public Property InsuSuryo() As Nullable(Of Int32)
            Get
                Return _InsuSuryo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _InsuSuryo = value
            End Set
        End Property
        ''' <summary>作成ユーザーID</summary>

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
        ''' <summary>更新ユーザーID</summary>

        Private _UpdatedUserId As String
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property
        ''' <summary>更新年月日</summary>

        Private _UpdatedDate As String
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property
        ''' <summary>更新時分秒</summary>

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