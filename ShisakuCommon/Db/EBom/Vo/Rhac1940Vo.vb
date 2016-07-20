Namespace Db.EBom.Vo
    ''' <summary>
    ''' Rhac0533と紐付く属性
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac1940Vo
        '' 型代表部品番号
        Private _KataDaihyouBuhinNo As String
        '' 型改訂No
        Private _KataKaiteiNo As String
        '' 型費金額
        Private _KatahiKingaku As Decimal
        '' パレット
        Private _Pallet As Decimal
        '' 開発費
        Private _Kaihatsuhi As Decimal
        '' 海外型費金額
        Private _SiaKatahiKingaku As Decimal
        '' 海外パレット
        Private _SiaPallet As Decimal
        '' 海外開発費
        Private _SiaKaihatsuhi As Decimal
        '' 承認年月日
        Private _ShoninDate As Integer
        '' 採用年月日
        Private _SaiyoDate As Integer
        '' 廃止年月日
        Private _HaisiDate As Integer
        '' 作成AppNo
        Private _CreatedAppNo As String
        '' 更新AppNo
        Private _UpdatedAppNo As String
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

        ''' <summary>型代表部品番号</summary>
        ''' <value>型代表部品番号</value>
        ''' <returns>型代表部品番号</returns>
        Public Property KataDaihyouBuhinNo() As String
            Get
                Return _KataDaihyouBuhinNo
            End Get
            Set(ByVal value As String)
                _KataDaihyouBuhinNo = value
            End Set
        End Property

        ''' <summary>型改訂No</summary>
        ''' <value>型改訂No</value>
        ''' <returns>型改訂No</returns>
        Public Property KataKaiteiNo() As String
            Get
                Return _KataKaiteiNo
            End Get
            Set(ByVal value As String)
                _KataKaiteiNo = value
            End Set
        End Property

        ''' <summary>型費金額</summary>
        ''' <value>型費金額</value>
        ''' <returns>型費金額</returns>
        Public Property KatahiKingaku() As Decimal
            Get
                Return _KatahiKingaku
            End Get
            Set(ByVal value As Decimal)
                _KatahiKingaku = value
            End Set
        End Property

        ''' <summary>パレット</summary>
        ''' <value>パレット</value>
        ''' <returns>パレット</returns>
        Public Property Pallet() As Decimal
            Get
                Return _Pallet
            End Get
            Set(ByVal value As Decimal)
                _Pallet = value
            End Set
        End Property

        ''' <summary>開発費</summary>
        ''' <value>開発費</value>
        ''' <returns>開発費</returns>
        Public Property Kaihatsuhi() As Decimal
            Get
                Return _Kaihatsuhi
            End Get
            Set(ByVal value As Decimal)
                _Kaihatsuhi = value
            End Set
        End Property

        ''' <summary>海外型費金額</summary>
        ''' <value>海外型費金額</value>
        ''' <returns>海外型費金額</returns>
        Public Property SiaKatahiKingaku() As Decimal
            Get
                Return _SiaKatahiKingaku
            End Get
            Set(ByVal value As Decimal)
                _SiaKatahiKingaku = value
            End Set
        End Property

        ''' <summary>海外パレット</summary>
        ''' <value>海外パレット</value>
        ''' <returns>海外パレット</returns>
        Public Property SiaPallet() As Decimal
            Get
                Return _SiaPallet
            End Get
            Set(ByVal value As Decimal)
                _SiaPallet = value
            End Set
        End Property

        ''' <summary>海外開発費</summary>
        ''' <value>海外開発費</value>
        ''' <returns>海外開発費</returns>
        Public Property SiaKaihatsuhi() As Decimal
            Get
                Return _SiaKaihatsuhi
            End Get
            Set(ByVal value As Decimal)
                _SiaKaihatsuhi = value
            End Set
        End Property

        ''' <summary>承認日付</summary>
        ''' <value>承認日付</value>
        ''' <returns>承認日付</returns>
        Public Property ShoninDate() As Integer
            Get
                Return _ShoninDate
            End Get
            Set(ByVal value As Integer)
                _ShoninDate = value
            End Set
        End Property

        ''' <summary>採用年月日</summary>
        ''' <value>採用年月日</value>
        ''' <returns>採用年月日</returns>
        Public Property SaiyoDate() As Integer
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Integer)
                _SaiyoDate = value
            End Set
        End Property

        ''' <summary>廃止年月日</summary>
        ''' <value>廃止年月日</value>
        ''' <returns>廃止年月日</returns>
        Public Property HaisiDate() As Integer
            Get
                Return _HaisiDate
            End Get
            Set(ByVal value As Integer)
                _HaisiDate = value
            End Set
        End Property

        ''' <summary>作成AppNo</summary>
        ''' <value>作成AppNo</value>
        ''' <returns>作成AppNo</returns>
        Public Property CreatedAppNo() As String
            Get
                Return _CreatedAppNo
            End Get
            Set(ByVal value As String)
                _CreatedAppNo = value
            End Set
        End Property

        ''' <summary>更新AppNo</summary>
        ''' <value>更新AppNo</value>
        ''' <returns>更新AppNo</returns>
        Public Property UpdatedAppNo() As String
            Get
                Return _UpdatedAppNo
            End Get
            Set(ByVal value As String)
                _UpdatedAppNo = value
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