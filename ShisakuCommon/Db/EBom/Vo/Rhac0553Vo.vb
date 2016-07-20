Namespace Db.EBom.Vo
    ''' <summary>
    ''' 部品構成(FM5以降)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0553Vo
        '' 開発符号
        Private _KaihatsuFugo As String
        '' 部品番号(親)
        Private _BuhinNoOya As String
        '' 部品番号(子)
        Private _BuhinNoKo As String
        '' 改訂No
        Private _KaiteiNo As String
        '' 表示No
        Private _HyoujiNo As Integer
        '' 図面No
        Private _ZumenNo As String
        '' 見出し番号
        Private _MidashiNo As String
        '' 見出し番号種類
        Private _MidashiNoShurui As String
        '' 員数数量
        Private _InsuSuryo As Integer
        '' 集計コード
        Private _ShukeiCode As String
        '' 海外集計コード
        Private _SiaShukeiCode As String
        '' 現調CKD区分
        Private _GencyoCkdKbn As String
        '' 参考表示コード
        Private _SankoHyojiCode As String
        '' 承認日
        Private _ShoninDate As Integer
        '' 採用日
        Private _SaiyoDate As Integer
        '' 廃止日
        Private _HaisiDate As Integer
        '' ノート
        Private _Note As String
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

        ''' <summary>部品番号(親)</summary>
        ''' <value>部品番号(親)</value>
        ''' <returns>部品番号(親)</returns>
        Public Property BuhinNoOya() As String
            Get
                Return _BuhinNoOya
            End Get
            Set(ByVal value As String)
                _BuhinNoOya = value
            End Set
        End Property

        ''' <summary>部品番号(子)</summary>
        ''' <value>部品番号(子)</value>
        ''' <returns>部品番号(子)</returns>
        Public Property BuhinNoKo() As String
            Get
                Return _BuhinNoKo
            End Get
            Set(ByVal value As String)
                _BuhinNoKo = value
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

        ''' <summary>表示番号</summary>
        ''' <value>表示番号</value>
        ''' <returns>表示番号</returns>
        Public Property HyoujiNo() As String
            Get
                Return _HyoujiNo
            End Get
            Set(ByVal value As String)
                _HyoujiNo = value
            End Set
        End Property

        ''' <summary>図面番号</summary>
        ''' <value>図面番号</value>
        ''' <returns>図面番号</returns>
        Public Property ZumenNo() As String
            Get
                Return _ZumenNo
            End Get
            Set(ByVal value As String)
                _ZumenNo = value
            End Set
        End Property

        ''' <summary>見出し番号</summary>
        ''' <value>見出し番号</value>
        ''' <returns>見出し番号</returns>
        Public Property MidashiNo() As String
            Get
                Return _MidashiNo
            End Get
            Set(ByVal value As String)
                _MidashiNo = value
            End Set
        End Property

        ''' <summary>見出し番号種類</summary>
        ''' <value>見出し番号種類</value>
        ''' <returns>見出し番号種類</returns>
        Public Property MidashiNoShurui() As String
            Get
                Return _MidashiNoShurui
            End Get
            Set(ByVal value As String)
                _MidashiNoShurui = value
            End Set
        End Property

        ''' <summary>員数数量</summary>
        ''' <value>員数数量</value>
        ''' <returns>員数数量</returns>
        Public Property InsuSuryo() As Integer
            Get
                Return _InsuSuryo
            End Get
            Set(ByVal value As Integer)
                _InsuSuryo = value
            End Set
        End Property

        ''' <summary>集計コード</summary>
        ''' <value>集計コード</value>
        ''' <returns>集計コード</returns>
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

        ''' <summary>参考表示コード</summary>
        ''' <value>参考表示コード</value>
        ''' <returns>参考表示コード</returns>
        Public Property SankoHyojiCode() As String
            Get
                Return _SankoHyojiCode
            End Get
            Set(ByVal value As String)
                _SankoHyojiCode = value
            End Set
        End Property

        ''' <summary>承認日</summary>
        ''' <value>承認日</value>
        ''' <returns>承認日</returns>
        Public Property ShoninDate() As Integer
            Get
                Return _ShoninDate
            End Get
            Set(ByVal value As Integer)
                _ShoninDate = value
            End Set
        End Property

        ''' <summary>採用日</summary>
        ''' <value>採用日</value>
        ''' <returns>採用日</returns>
        Public Property SaiyoDate() As Integer
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Integer)
                _SaiyoDate = value
            End Set
        End Property

        ''' <summary>廃止日</summary>
        ''' <value>廃止日</value>
        ''' <returns>廃止日</returns>
        Public Property HaisiDate() As Integer
            Get
                Return _HaisiDate
            End Get
            Set(ByVal value As Integer)
                _HaisiDate = value
            End Set
        End Property

        ''' <summary>ノート</summary>
        ''' <value>ノート</value>
        ''' <returns>ノート</returns>
        Public Property Note() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                _Note = value
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