Namespace Db.EBom.Vo
    ''' <summary>予算書単価情報</summary>
    Public Class TYosanTankaVo
        ''' <summary>国内海外区分</summary>
        Private _KokunaiKaigaiKbn As String
        ''' <summary>部品番号</summary>
        Private _YosanBuhinNo As String
        ''' <summary>部品費（量産）</summary>
        Private _YosanBuhinHiRyosan As Nullable(Of Decimal)
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

        ''' <summary>国内海外区分</summary>
        ''' <value>国内海外区分</value>
        ''' <returns>国内海外区分</returns>
        Public Property KokunaiKaigaiKbn() As String
            Get
                Return _KokunaiKaigaiKbn
            End Get
            Set(ByVal value As String)
                _KokunaiKaigaiKbn = value
            End Set
        End Property
        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public Property YosanBuhinNo() As String
            Get
                Return _YosanBuhinNo
            End Get
            Set(ByVal value As String)
                _YosanBuhinNo = value
            End Set
        End Property
        ''' <summary>部品費（量産）</summary>
        ''' <value>部品費（量産）</value>
        ''' <returns>部品費（量産）</returns>
        Public Property YosanBuhinHiRyosan() As Nullable(Of Decimal)
            Get
                Return _YosanBuhinHiRyosan
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanBuhinHiRyosan = value
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