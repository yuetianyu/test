Namespace Db.EBom.Vo

    ''' <summary>
    ''' 部品原価
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0580Vo

        ''' <summary>部品番号</summary>
        Private _BuhinNo As String

        ''' <summary>原価区分</summary>
        Private _GenkaKbn As String

        ''' <summary>原価改訂№</summary>
        Private _GenkaKaiteiNo As String

        ''' <summary>原価補助区分</summary>
        Private _GenkaHojoKbn As String

        ''' <summary>原価</summary>
        Private _GenkaKingaku As Decimal

        ''' <summary>メーカーコード</summary>
        Private _MakerCode As String

        ''' <summary>採用年月日</summary>
        Private _SaiyoDate As Integer

        ''' <summary>廃止年月日</summary>
        Private _HaisiDate As Integer

        ''' <summary>旧部品番号</summary>
        Private _BuhinNoOld As String

        ' 作成ユーザーID
        Private _createdUserId As String
        ' 作成年月日
        Private _createdDate As String
        ' 作成時分秒
        Private _createdTime As String
        ' 更新ユーザーID
        Private _updatedUserId As String
        ' 更新年月日
        Private _updatedDate As String
        ' 更新時分秒
        Private _updatedTime As String




        ''' <summary>部品番号</summary>
        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property

        ''' <summary>原価区分</summary>
        Public Property GenkaKbn() As String
            Get
                Return _GenkaKbn
            End Get
            Set(ByVal value As String)
                _GenkaKbn = value
            End Set
        End Property

        ''' <summary>原価改訂№</summary>
        Public Property GenkaKaiteiNo() As String
            Get
                Return _GenkaKaiteiNo
            End Get
            Set(ByVal value As String)
                _GenkaKaiteiNo = value
            End Set
        End Property

        ''' <summary>原価補助区分</summary>
        Public Property GenkaHojoKbn() As String
            Get
                Return _GenkaHojoKbn
            End Get
            Set(ByVal value As String)
                _GenkaHojoKbn = value
            End Set
        End Property


        ''' <summary>原価</summary>
        Public Property GenkaKingaku() As Decimal
            Get
                Return _GenkaKingaku
            End Get
            Set(ByVal value As Decimal)
                _GenkaKingaku = value
            End Set
        End Property


        ''' <summary>メーカーコード</summary>
        Public Property MakerCode() As String
            Get
                Return _MakerCode
            End Get
            Set(ByVal value As String)
                _MakerCode = value
            End Set
        End Property

        ''' <summary>採用年月日</summary>
        Public Property SaiyoDate() As Integer
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Integer)
                _SaiyoDate = value
            End Set
        End Property

        ''' <summary>廃止年月日</summary>
        Public Property HaisiDate() As Integer
            Get
                Return _HaisiDate
            End Get
            Set(ByVal value As Integer)
                _HaisiDate = value
            End Set
        End Property

        ''' <summary>旧部品番号</summary>
        Public Property BuhinNoOld() As String
            Get
                Return _BuhinNoOld
            End Get
            Set(ByVal value As String)
                _BuhinNoOld = value
            End Set
        End Property


        ''' <summary>作成ユーザーID</summary>
        Public Property CreatedUserId() As String
            Get
                Return _createdUserId
            End Get
            Set(ByVal value As String)
                _createdUserId = value
            End Set
        End Property

        ''' <summary>作成年月日</summary>
        Public Property CreatedDate() As String
            Get
                Return _createdDate
            End Get
            Set(ByVal value As String)
                _createdDate = value
            End Set
        End Property

        ''' <summary>作成時分秒</summary>
        Public Property CreatedTime() As String
            Get
                Return _createdTime
            End Get
            Set(ByVal value As String)
                _createdTime = value
            End Set
        End Property


        ''' <summary>更新ユーザーID</summary>
        Public Property UpdatedUserId() As String
            Get
                Return _updatedUserId
            End Get
            Set(ByVal value As String)
                _updatedUserId = value
            End Set
        End Property

        ''' <summary>更新年月日</summary>
        Public Property UpdatedDate() As String
            Get
                Return _updatedDate
            End Get
            Set(ByVal value As String)
                _updatedDate = value
            End Set
        End Property

        ''' <summary>更新時分秒</summary>
        Public Property UpdatedTime() As String
            Get
                Return _updatedTime
            End Get
            Set(ByVal value As String)
                _updatedTime = value
            End Set
        End Property



    End Class

End Namespace