Namespace Db.EBom.Vo
    ''' <summary>
    ''' 部品構成(パンダ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0551Vo

        '' 部品番号(親) 
        Private _BuhinNoOya As String
        '' 部品番号(子) 
        Private _BuhinNoKo As String
        '' 構成改訂No. 
        Private _KaiteiNo As String
        '' 通知書No 
        Private _TsuchishoNo As String
        '' 見出番号 
        Private _MidashiNo As String
        '' 見出番号･種類 
        Private _MidashiNoShurui As String
        '' 見出補助番号 
        Private _MidashiNoHojo As Nullable(Of Int32)
        '' 員数 
        Private _InsuSuryo As Nullable(Of Int32)
        '' 採用年月日 
        Private _SaiyoDate As Nullable(Of Int32)
        '' 廃止年月日 
        Private _HaisiDate As Nullable(Of Int32)
        '' 国内集計コード 
        Private _ShukeiCode As String
        '' 海外SIA集計コード 
        Private _SiaShukeiCode As String
        '' 現調CKD区分 
        Private _GencyoCkdKbn As String
        '' 更新区分 
        Private _UpdateKbn As Nullable(Of Int32)
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

        ''' <summary>構成改訂No.</summary>
        ''' <value>構成改訂No.</value>
        ''' <returns>構成改訂No.</returns>
        Public Property KaiteiNo() As String
            Get
                Return _KaiteiNo
            End Get
            Set(ByVal value As String)
                _KaiteiNo = value
            End Set
        End Property

        ''' <summary>通知書番号</summary>
        ''' <value>通知書番号</value>
        ''' <returns>通知書番号</returns>
        Public Property TsuchishoNo() As String
            Get
                Return _TsuchishoNo
            End Get
            Set(ByVal value As String)
                _TsuchishoNo = value
            End Set
        End Property

        ''' <summary>見出番号</summary>
        ''' <value>見出番号</value>
        ''' <returns>見出番号</returns>
        Public Property MidashiNo() As String
            Get
                Return _MidashiNo
            End Get
            Set(ByVal value As String)
                _MidashiNo = value
            End Set
        End Property

        ''' <summary>見出番号･種類</summary>
        ''' <value>見出番号･種類</value>
        ''' <returns>見出番号･種類</returns>
        Public Property MidashiNoShurui() As String
            Get
                Return _MidashiNoShurui
            End Get
            Set(ByVal value As String)
                _MidashiNoShurui = value
            End Set
        End Property

        ''' <summary>見出補助番号</summary>
        ''' <value>見出補助番号</value>
        ''' <returns>見出補助番号</returns>
        Public Property MidashiNoHojo() As Nullable(Of Int32)
            Get
                Return _MidashiNoHojo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _MidashiNoHojo = value
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

        ''' <summary>採用年月日</summary>
        ''' <value>採用年月日</value>
        ''' <returns>採用年月日</returns>
        Public Property SaiyoDate() As Nullable(Of Int32)
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaiyoDate = value
            End Set
        End Property

        ''' <summary>廃止年月日</summary>
        ''' <value>廃止年月日</value>
        ''' <returns>廃止年月日</returns>
        Public Property HaisiDate() As Nullable(Of Int32)
            Get
                Return _HaisiDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _HaisiDate = value
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

        ''' <summary>海外SIA集計コード</summary>
        ''' <value>海外SIA集計コード</value>
        ''' <returns>海外SIA集計コード</returns>
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

        ''' <summary>更新区分</summary>
        ''' <value>更新区分</value>
        ''' <returns>更新区分</returns>
        Public Property UpdateKbn() As Nullable(Of Int32)
            Get
                Return _UpdateKbn
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _UpdateKbn = value
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