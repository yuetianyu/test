Namespace Db.EBom.Vo
    ''' <summary>
    ''' Rhac0553と紐付く属性
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac1910Vo
        '' 開発符号
        Private _KaihatsuFugo As String
        '' 部品番号
        Private _BuhinNo As String
        '' 改訂No
        Private _KaiteiNo As String
        '' 供与コード
        Private _KyoyoCode As String
        '' 供与モデル
        Private _KyoyoModel As String
        '' 集計コード
        Private _ShukeiCode As String
        '' 海外集計コード
        Private _SiaShukeiCode As String
        '' 現調CKD区分
        Private _GencyoCkdKbn As String
        '' コスト目標
        Private _CostMokuhyou As Decimal
        '' パートコスト
        Private _PartCost As Decimal
        '' 転進材
        Private _Tenshinzai As Decimal
        '' コスト表示区分
        Private _CostHyoujiKbn As String
        '' クォーテーション番号
        Private _QuotationNumber As String
        '' クォーテーションデータ
        Private _QuotationData As String
        '' 海外パートコスト
        Private _SiaPartCost As Decimal
        '' 海外転進材
        Private _SiaTenshinzai As Decimal
        '' 海外表示区分
        Private _SiaHyoujiKbn As String
        '' 海外クォーテーション番号
        Private _SiaQuotationNumber As String
        '' 海外クォーテーションデータ
        Private _SiaQuotationData As String
        '' マス目標
        Private _MassMokuhyou As Integer
        '' マス
        Private _Mass As Integer
        '' 金属マス
        Private _MetalMass As Integer
        '' マス表示区分
        Private _MassHyoujiKbn As String
        '' 取得部品番号
        Private _ShutokuBuhinNo As String
        '' 取得改訂No.
        Private _ShutokuKaiteiNo As String
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

        ''' <summary>開発符号</summary>
        ''' <value>開発符号</value>
        ''' <returns>開発符号</returns>
        Public Property KaihatsuFugo() As String
            Get
                Return _KaihatsuFugo
            End Get
            Set(ByVal value As String)
                _KaihatsuFugo = value
            End Set
        End Property

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property

        ''' <summary>改訂No</summary>
        ''' <value>改訂No</value>
        ''' <returns>改訂No</returns>
        Public Property KaiteiNo() As String
            Get
                Return _KaiteiNo
            End Get
            Set(ByVal value As String)
                _KaiteiNo = value
            End Set
        End Property

        ''' <summary>供与コード</summary>
        ''' <value>供与コード</value>
        ''' <returns>供与コード</returns>
        Public Property KyoyoCode() As String
            Get
                Return _KyoyoCode
            End Get
            Set(ByVal value As String)
                _KyoyoCode = value
            End Set
        End Property

        ''' <summary>供与モデル</summary>
        ''' <value>供与モデル</value>
        ''' <returns>供与モデル</returns>
        Public Property KyoyoModel() As String
            Get
                Return _KyoyoModel
            End Get
            Set(ByVal value As String)
                _KyoyoModel = value
            End Set
        End Property

        ''' <summary>国内集計コード</summary>
        ''' <value>国内集計コード</value>
        ''' <returns>国内集計コード</returns>
        Public Property ShukeiCode() As String
            Get
                Return _ShukeiCode
            End Get
            Set(ByVal value As String)
                _ShukeiCode = value
            End Set
        End Property

        ''' <summary>海外集計コード</summary>
        ''' <value>海外集計コード</value>
        ''' <returns>海外集計コード</returns>
        Public Property SiaShukeiCode() As String
            Get
                Return _SiaShukeiCode
            End Get
            Set(ByVal value As String)
                _SiaShukeiCode = value
            End Set
        End Property

        ''' <summary>現調CKD区分</summary>
        ''' <value>現調CKD区分</value>
        ''' <returns>現調CKD区分</returns>
        Public Property GencyoCkdKbn() As String
            Get
                Return _GencyoCkdKbn
            End Get
            Set(ByVal value As String)
                _GencyoCkdKbn = value
            End Set
        End Property

        ''' <summary>コスト目標</summary>
        ''' <value>コスト目標</value>
        ''' <returns>コスト目標</returns>
        Public Property CostMokuhyou() As Decimal
            Get
                Return _CostMokuhyou
            End Get
            Set(ByVal value As Decimal)
                _CostMokuhyou = value
            End Set
        End Property

        ''' <summary>パートコスト</summary>
        ''' <value>パートコスト</value>
        ''' <returns>パートコスト</returns>
        Public Property PartCost() As Decimal
            Get
                Return _PartCost
            End Get
            Set(ByVal value As Decimal)
                _PartCost = value
            End Set
        End Property

        ''' <summary>転進材</summary>
        ''' <value>転進材</value>
        ''' <returns>転進材</returns>
        Public Property Tenshinzai() As Decimal
            Get
                Return _Tenshinzai
            End Get
            Set(ByVal value As Decimal)
                _Tenshinzai = value
            End Set
        End Property

        ''' <summary>コスト表示区分</summary>
        ''' <value>コスト表示区分</value>
        ''' <returns>コスト表示区分</returns>
        Public Property CostHyoujiKbn() As String
            Get
                Return _CostHyoujiKbn
            End Get
            Set(ByVal value As String)
                _CostHyoujiKbn = value
            End Set
        End Property

        ''' <summary>クォーテーション番号</summary>
        ''' <value>クォーテーション番号</value>
        ''' <returns>クォーテーション番号</returns>
        Public Property QuotationNumber() As String
            Get
                Return _QuotationNumber
            End Get
            Set(ByVal value As String)
                _QuotationNumber = value
            End Set
        End Property

        ''' <summary>クォーテーションデータ</summary>
        ''' <value>クォーテーションデータ</value>
        ''' <returns>クォーテーションデータ</returns>
        Public Property QuotationData() As String
            Get
                Return _QuotationData
            End Get
            Set(ByVal value As String)
                _QuotationData = value
            End Set
        End Property

        ''' <summary>海外パートコスト</summary>
        ''' <value>海外パートコスト</value>
        ''' <returns>海外パートコスト</returns>
        Public Property SiaPartCost() As Decimal
            Get
                Return _SiaPartCost
            End Get
            Set(ByVal value As Decimal)
                _SiaPartCost = value
            End Set
        End Property

        ''' <summary>海外転進材</summary>
        ''' <value>海外転進材</value>
        ''' <returns>海外転進材</returns>
        Public Property SiaTenshinzai() As Decimal
            Get
                Return _SiaTenshinzai
            End Get
            Set(ByVal value As Decimal)
                _SiaTenshinzai = value
            End Set
        End Property

        ''' <summary>海外表示区分</summary>
        ''' <value>海外表示区分</value>
        ''' <returns>海外表示区分</returns>
        Public Property SiaHyoujiKbn() As String
            Get
                Return _SiaHyoujiKbn
            End Get
            Set(ByVal value As String)
                _SiaHyoujiKbn = value
            End Set
        End Property

        ''' <summary>海外クォーテーション番号</summary>
        ''' <value>海外クォーテーション番号</value>
        ''' <returns>海外クォーテーション番号</returns>
        Public Property SiaQuotationNumber() As String
            Get
                Return _SiaQuotationNumber
            End Get
            Set(ByVal value As String)
                _SiaQuotationNumber = value
            End Set
        End Property

        ''' <summary>海外クォーテーションデータ</summary>
        ''' <value>海外クォーテーションデータ</value>
        ''' <returns>海外クォーテーションデータ</returns>
        Public Property SiaQuotationData() As String
            Get
                Return _SiaQuotationData
            End Get
            Set(ByVal value As String)
                _SiaQuotationData = value
            End Set
        End Property

        ''' <summary>マス目標</summary>
        ''' <value>マス目標</value>
        ''' <returns>マス目標</returns>
        Public Property MassMokuhyou() As Integer
            Get
                Return _MassMokuhyou
            End Get
            Set(ByVal value As Integer)
                _MassMokuhyou = value
            End Set
        End Property

        ''' <summary>マス</summary>
        ''' <value>マス</value>
        ''' <returns>マス</returns>
        Public Property Mass() As Integer
            Get
                Return _Mass
            End Get
            Set(ByVal value As Integer)
                _Mass = value
            End Set
        End Property

        ''' <summary>金属マス</summary>
        ''' <value>金属マス</value>
        ''' <returns>金属マス</returns>
        Public Property MetalMass() As Integer
            Get
                Return _MetalMass
            End Get
            Set(ByVal value As Integer)
                _MetalMass = value
            End Set
        End Property

        ''' <summary>マス表示区分</summary>
        ''' <value>マス表示区分</value>
        ''' <returns>マス表示区分</returns>
        Public Property MassHyoujiKbn() As String
            Get
                Return _MassHyoujiKbn
            End Get
            Set(ByVal value As String)
                _MassHyoujiKbn = value
            End Set
        End Property

        ''' <summary>取得部品番号</summary>
        ''' <value>取得部品番号</value>
        ''' <returns>取得部品番号</returns>
        Public Property ShutokuBuhinNo() As String
            Get
                Return _ShutokuBuhinNo
            End Get
            Set(ByVal value As String)
                _ShutokuBuhinNo = value
            End Set
        End Property

        ''' <summary>取得改訂No.</summary>
        ''' <value>取得改訂No.</value>
        ''' <returns>取得改訂No.</returns>
        Public Property ShutokuKaiteiNo() As String
            Get
                Return _ShutokuKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShutokuKaiteiNo = value
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