Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作部品構成情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuBuhinKouseiVo
        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 作成回数
        Private _SakuseiCount As Nullable(of Int32)
        '' 試作部課コード
        Private _ShisakuBukaCode As String
        '' 試作ブロック№
        Private _ShisakuBlockNo As String
        '' 試作ブロック№改訂№
        Private _ShisakuBlockNoKaiteiNo As String
        '' 試作号車
        Private _ShisakuGousya As String
        ''INSTL品番
        Private _InstlHinban As String
        ''INSTL品番試作区分
        Private _InstlHinbanKbn As String
        '' 部品番号(親)
        Private _BuhinNoOya As String
        '' 部品番号区分（親）
        Private _BuhinNoKbnOya As String
        '' 部品番号(子)
        Private _BuhinNoKo As String
        '' 部品番号区分（子）
        Private _BuhinNoKbnKo As String
        '' 構成改訂No.
        Private _KaiteiNo As String
        '' 図面番号
        Private _ZumenNo As String
        '' 見出番号
        Private _MidashiNo As String
        '' 見出番号･種類
        Private _MidashiNoShurui As String
        '' 見出補助番号
        Private _MidashiNoHojo As Nullable(of Int32)
        '' 員数
        Private _InsuSuryo As Nullable(of Int32)
        '' 国内集計コード
        Private _ShukeiCode As String
        '' 海外SIA集計コード
        Private _SiaShukeiCode As String
        '' 現調CKD区分
        Private _GencyoCkdKbn As String
        '' 供給セクション 2012/01/23 追加
        Private _KyoukuSection As String
        '' 承認区分
        Private _ShoninKbn As String
        '' 採用年月日
        Private _SaiyoDate As Nullable(of Int32)
        '' 廃止年月日
        Private _HaisiDate As Nullable(of Int32)
        '' 差戻し年月日
        Private _SashimodoshiDate As Nullable(of Int32)
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

        ''' <summary>試作イベントコード</summary>
        ''' <value>試作イベントコード</value>
        ''' <returns>試作イベントコード</returns>
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>作成回数</summary>
        ''' <value>作成回数</value>
        ''' <returns>作成回数</returns>
        Public Property SakuseiCount() As Nullable(of Int32)
            Get
                Return _SakuseiCount
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SakuseiCount = value
            End Set
        End Property

        ''' <summary>試作部課コード</summary>
        ''' <value>試作部課コード</value>
        ''' <returns>試作部課コード</returns>
        Public Property ShisakuBukaCode() As String
            Get
                Return _ShisakuBukaCode
            End Get
            Set(ByVal value As String)
                _ShisakuBukaCode = value
            End Set
        End Property

        ''' <summary>試作ブロック№</summary>
        ''' <value>試作ブロック№</value>
        ''' <returns>試作ブロック№</returns>
        Public Property ShisakuBlockNo() As String
            Get
                Return _ShisakuBlockNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNo = value
            End Set
        End Property

        ''' <summary>試作ブロック№改訂№</summary>
        ''' <value>試作ブロック№改訂№</value>
        ''' <returns>試作ブロック№改訂№</returns>
        Public Property ShisakuBlockNoKaiteiNo() As String
            Get
                Return _ShisakuBlockNoKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShisakuBlockNoKaiteiNo = value
            End Set
        End Property

        ''' <summary> 試作号車</summary>
        ''' <value> 試作号車</value>
        ''' <returns> 試作号車</returns>
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

        ''' <summary> INSTL品番</summary>
        ''' <value> INSTL品番</value>
        ''' <returns> INSTL品番</returns>
        Public Property InstlHinban() As String
            Get
                Return _InstlHinban
            End Get
            Set(ByVal value As String)
                _InstlHinban = value
            End Set
        End Property

        ''' <summary> INSTL品番試作区分</summary>
        ''' <value> INSTL品番試作区分</value>
        ''' <returns> INSTL品番試作区分</returns>
        Public Property InstlHinbanKbn() As String
            Get
                Return _InstlHinbanKbn
            End Get
            Set(ByVal value As String)
                _InstlHinbanKbn = value
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

        ''' <summary>部品番号区分（親）</summary>
        ''' <value>部品番号区分（親）</value>
        ''' <returns>部品番号区分（親）</returns>
        Public Property BuhinNoKbnOya() As String
            Get
                Return _BuhinNoKbnOya
            End Get
            Set(ByVal value As String)
                _BuhinNoKbnOya = value
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

        ''' <summary>部品番号区分（子）</summary>
        ''' <value>部品番号区分（子）</value>
        ''' <returns>部品番号区分（子）</returns>
        Public Property BuhinNoKbnKo() As String
            Get
                Return _BuhinNoKbnKo
            End Get
            Set(ByVal value As String)
                _BuhinNoKbnKo = value
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
        Public Property MidashiNoHojo() As Nullable(of Int32)
            Get
                Return _MidashiNoHojo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _MidashiNoHojo = value
            End Set
        End Property

        ''' <summary>員数</summary>
        ''' <value>員数</value>
        ''' <returns>員数</returns>
        Public Property InsuSuryo() As Nullable(of Int32)
            Get
                Return _InsuSuryo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _InsuSuryo = value
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

        ''' <summary>供給セクション</summary>
        ''' <value>供給セクション</value>
        ''' <returns>供給セクション</returns>
        Public Property KyoukuSection() As String
            '2012/01/23 供給セクション追加
            Get
                Return _KyoukuSection
            End Get
            Set(ByVal value As String)
                _KyoukuSection = value
            End Set
        End Property

        ''' <summary>承認区分</summary>
        ''' <value>承認区分</value>
        ''' <returns>承認区分</returns>
        Public Property ShoninKbn() As String
            Get
                Return _ShoninKbn
            End Get
            Set(ByVal value As String)
                _ShoninKbn = value
            End Set
        End Property

        ''' <summary>採用年月日</summary>
        ''' <value>採用年月日</value>
        ''' <returns>採用年月日</returns>
        Public Property SaiyoDate() As Nullable(of Int32)
            Get
                Return _SaiyoDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SaiyoDate = value
            End Set
        End Property

        ''' <summary>廃止年月日</summary>
        ''' <value>廃止年月日</value>
        ''' <returns>廃止年月日</returns>
        Public Property HaisiDate() As Nullable(of Int32)
            Get
                Return _HaisiDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _HaisiDate = value
            End Set
        End Property

        ''' <summary>差戻し年月日</summary>
        ''' <value>差戻し年月日</value>
        ''' <returns>差戻し年月日</returns>
        Public Property SashimodoshiDate() As Nullable(of Int32)
            Get
                Return _SashimodoshiDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SashimodoshiDate = value
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
