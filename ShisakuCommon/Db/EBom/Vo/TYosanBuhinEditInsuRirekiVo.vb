Namespace Db.EBom.Vo
    ''' <summary>予算書部品編集員数履歴情報</summary>
    Public Class TYosanBuhinEditInsuRirekiVo
        ''' <summary>予算イベントコード</summary>
        Private _YosanEventCode As String
        ''' <summary>ユニット区分</summary>
        Private _UnitKbn As String
        ''' <summary>登録日</summary>
        Private _RegisterDate As String
        ''' <summary>予算部課コード</summary>
        Private _YosanBukaCode As String
        ''' <summary>予算ブロック№</summary>
        Private _YosanBlockNo As String
        ''' <summary>部品番号表示順</summary>
        Private _BuhinNoHyoujiJun As Nullable(Of Int32)
        ''' <summary>パターン表示順</summary>
        Private _PatternHyoujiJun As Nullable(Of Int32)
        ''' <summary>員数</summary>
        Private _InsuSuryo As Nullable(Of Int32)
        ''' <summary>最終更新日</summary>
        Private _SaisyuKoushinbi As Nullable(Of Int32)
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
        ''' <summary>登録日</summary>
        ''' <value>登録日</value>
        ''' <returns>登録日</returns>
        Public Property RegisterDate() As String
            Get
                Return _RegisterDate
            End Get
            Set(ByVal value As String)
                _RegisterDate = value
            End Set
        End Property
        ''' <summary>予算部課コード</summary>
        ''' <value>予算部課コード</value>
        ''' <returns>予算部課コード</returns>
        Public Property YosanBukaCode() As String
            Get
                Return _YosanBukaCode
            End Get
            Set(ByVal value As String)
                _YosanBukaCode = value
            End Set
        End Property
        ''' <summary>予算ブロック№</summary>
        ''' <value>予算ブロック№</value>
        ''' <returns>予算ブロック№</returns>
        Public Property YosanBlockNo() As String
            Get
                Return _YosanBlockNo
            End Get
            Set(ByVal value As String)
                _YosanBlockNo = value
            End Set
        End Property
        ''' <summary>部品番号表示順</summary>
        ''' <value>部品番号表示順</value>
        ''' <returns>部品番号表示順</returns>
        Public Property BuhinNoHyoujiJun() As Nullable(Of Int32)
            Get
                Return _BuhinNoHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _BuhinNoHyoujiJun = value
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
        ''' <summary>員数</summary>
        ''' <value>員数</value>
        ''' <returns>員数</returns>
        Public Property InsuSuryo() As Nullable(Of Int32)
            Get
                Return _InsuSuryo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _InsuSuryo = value
            End Set
        End Property
        ''' <summary>最終更新日</summary>
        ''' <value>最終更新日</value>
        ''' <returns>最終更新日</returns>
        Public Property SaisyuKoushinbi() As Nullable(Of Int32)
            Get
                Return _SaisyuKoushinbi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaisyuKoushinbi = value
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