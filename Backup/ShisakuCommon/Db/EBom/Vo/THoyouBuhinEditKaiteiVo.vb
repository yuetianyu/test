Namespace Db.EBom.Vo
    ''' <summary>補用部品編集情報（改訂抽出）</summary>
    Public Class THoyouBuhinEditKaiteiVo
        ''' <summary>補用イベントコード</summary>
        Private _HoyouEventCode As String
        ''' <summary>補用リストコード</summary>
        Private _HoyouListCode As String
        ''' <summary>補用リストコード改訂№</summary>
        Private _HoyouListCodeKaiteiNo As String
        ''' <summary>補用部課コード</summary>
        Private _HoyouBukaCode As String
        ''' <summary>補用担当</summary>
        Private _HoyouTanto As String
        ''' <summary>補用担当改訂№</summary>
        Private _HoyouTantoKaiteiNo As String
        ''' <summary>前回補用担当改訂№</summary>
        Private _ZenkaiHoyouTantoKaiteiNo As String
        ''' <summary>部品番号表示順</summary>
        Private _BuhinNoHyoujiJun As Nullable(Of Int32)
        ''' <summary>フラグ</summary>
        Private _Flag As String
        ''' <summary>レベル</summary>
        Private _Level As Nullable(Of Int32)
        ''' <summary>国内集計コード</summary>
        Private _ShukeiCode As String
        ''' <summary>海外SIA集計コード</summary>
        Private _SiaShukeiCode As String
        ''' <summary>現調CKD区分</summary>
        Private _GencyoCkdKbn As String
        ''' <summary>取引先コード</summary>
        Private _MakerCode As String
        ''' <summary>取引先名称</summary>
        Private _MakerName As String
        ''' <summary>部品番号</summary>
        Private _BuhinNo As String
        ''' <summary>部品名称</summary>
        Private _BuhinName As String
        ''' <summary>員数</summary>
        Private _InsuSuryo As Nullable(Of Int32)
        ''' <summary>供給セクション</summary>
        Private _KyoukuSection As String
        ''' <summary>部品ノート</summary>
        Private _BuhinNote As String
        ''' <summary>メモ０１</summary>
        Private _Memo_1 As String
        ''' <summary>メモ０２</summary>
        Private _Memo_2 As String
        ''' <summary>メモ０３</summary>
        Private _Memo_3 As String
        ''' <summary>メモ０４</summary>
        Private _Memo_4 As String
        ''' <summary>メモ０５</summary>
        Private _Memo_5 As String
        ''' <summary>メモ０６</summary>
        Private _Memo_6 As String
        ''' <summary>メモ０７</summary>
        Private _Memo_7 As String
        ''' <summary>メモ０８</summary>
        Private _Memo_8 As String
        ''' <summary>メモ０９</summary>
        Private _Memo_9 As String
        ''' <summary>メモ１０</summary>
        Private _Memo_10 As String
        ''' <summary>メモ０１タイトル</summary>
        Private _Memo_1_T As String
        ''' <summary>メモ０２タイトル</summary>
        Private _Memo_2_T As String
        ''' <summary>メモ０３タイトル</summary>
        Private _Memo_3_T As String
        ''' <summary>メモ０４タイトル</summary>
        Private _Memo_4_T As String
        ''' <summary>メモ０５タイトル</summary>
        Private _Memo_5_T As String
        ''' <summary>メモ０６タイトル</summary>
        Private _Memo_6_T As String
        ''' <summary>メモ０７タイトル</summary>
        Private _Memo_7_T As String
        ''' <summary>メモ０８タイトル</summary>
        Private _Memo_8_T As String
        ''' <summary>メモ０９タイトル</summary>
        Private _Memo_9_T As String
        ''' <summary>メモ１０タイトル</summary>
        Private _Memo_10_T As String
        ''' <summary>備考</summary>
        Private _Bikou As String
        ''' <summary>編集登録日</summary>
        Private _EditTourokubi As Nullable(Of Int32)
        ''' <summary>編集登録時間</summary>
        Private _EditTourokujikan As Nullable(Of Int32)
        ''' <summary>改訂判断フラグ</summary>
        Private _KaiteiHandanFlg As String
        ''' <summary>自動織込み改訂No</summary>
        Private _AutoOrikomiKaiteiNo As String
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

        ''' <summary>補用イベントコード</summary>
        ''' <value>補用イベントコード</value>
        ''' <returns>補用イベントコード</returns>
        Public Property HoyouEventCode() As String
            Get
                Return _HoyouEventCode
            End Get
            Set(ByVal value As String)
                _HoyouEventCode = value
            End Set
        End Property
        ''' <summary>補用リストコード</summary>
        ''' <value>補用リストコード</value>
        ''' <returns>補用リストコード</returns>
        Public Property HoyouListCode() As String
            Get
                Return _HoyouListCode
            End Get
            Set(ByVal value As String)
                _HoyouListCode = value
            End Set
        End Property
        ''' <summary>補用リストコード改訂№</summary>
        ''' <value>補用リストコード改訂№</value>
        ''' <returns>補用リストコード改訂№</returns>
        Public Property HoyouListCodeKaiteiNo() As String
            Get
                Return _HoyouListCodeKaiteiNo
            End Get
            Set(ByVal value As String)
                _HoyouListCodeKaiteiNo = value
            End Set
        End Property
        ''' <summary>補用部課コード</summary>
        ''' <value>補用部課コード</value>
        ''' <returns>補用部課コード</returns>
        Public Property HoyouBukaCode() As String
            Get
                Return _HoyouBukaCode
            End Get
            Set(ByVal value As String)
                _HoyouBukaCode = value
            End Set
        End Property
        ''' <summary>補用担当</summary>
        ''' <value>補用担当</value>
        ''' <returns>補用担当</returns>
        Public Property HoyouTanto() As String
            Get
                Return _HoyouTanto
            End Get
            Set(ByVal value As String)
                _HoyouTanto = value
            End Set
        End Property
        ''' <summary>補用担当改訂№</summary>
        ''' <value>補用担当改訂№</value>
        ''' <returns>補用担当改訂№</returns>
        Public Property HoyouTantoKaiteiNo() As String
            Get
                Return _HoyouTantoKaiteiNo
            End Get
            Set(ByVal value As String)
                _HoyouTantoKaiteiNo = value
            End Set
        End Property
        ''' <summary>前回補用担当改訂№</summary>
        ''' <value>前回補用担当改訂№</value>
        ''' <returns>前回補用担当改訂№</returns>
        Public Property ZenkaiHoyouTantoKaiteiNo() As String
            Get
                Return _ZenkaiHoyouTantoKaiteiNo
            End Get
            Set(ByVal value As String)
                _ZenkaiHoyouTantoKaiteiNo = value
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
        ''' <summary>フラグ</summary>
        ''' <value>フラグ</value>
        ''' <returns>フラグ</returns>
        Public Property Flag() As String
            Get
                Return _Flag
            End Get
            Set(ByVal value As String)
                _Flag = value
            End Set
        End Property
        ''' <summary>レベル</summary>
        ''' <value>レベル</value>
        ''' <returns>レベル</returns>
        Public Property Level() As Nullable(Of Int32)
            Get
                Return _Level
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Level = value
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
        ''' <summary>取引先コード</summary>
        ''' <value>取引先コード</value>
        ''' <returns>取引先コード</returns>
        Public Property MakerCode() As String
            Get
                Return _MakerCode
            End Get
            Set(ByVal value As String)
                _MakerCode = value
            End Set
        End Property
        ''' <summary>取引先名称</summary>
        ''' <value>取引先名称</value>
        ''' <returns>取引先名称</returns>
        Public Property MakerName() As String
            Get
                Return _MakerName
            End Get
            Set(ByVal value As String)
                _MakerName = value
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
        ''' <summary>部品名称</summary>
        ''' <value>部品名称</value>
        ''' <returns>部品名称</returns>
        Public Property BuhinName() As String
            Get
                Return _BuhinName
            End Get
            Set(ByVal value As String)
                _BuhinName = value
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
        ''' <summary>供給セクション</summary>
        ''' <value>供給セクション</value>
        ''' <returns>供給セクション</returns>
        Public Property KyoukuSection() As String
            Get
                Return _KyoukuSection
            End Get
            Set(ByVal value As String)
                _KyoukuSection = value
            End Set
        End Property
        ''' <summary>部品ノート</summary>
        ''' <value>部品ノート</value>
        ''' <returns>部品ノート</returns>
        Public Property BuhinNote() As String
            Get
                Return _BuhinNote
            End Get
            Set(ByVal value As String)
                _BuhinNote = value
            End Set
        End Property
        ''' <summary>メモ０１</summary>
        ''' <value>メモ０１</value>
        ''' <returns>メモ０１</returns>
        Public Property Memo_1() As String
            Get
                Return _Memo_1
            End Get
            Set(ByVal value As String)
                _Memo_1 = value
            End Set
        End Property
        ''' <summary>メモ０２</summary>
        ''' <value>メモ０２</value>
        ''' <returns>メモ０２</returns>
        Public Property Memo_2() As String
            Get
                Return _Memo_2
            End Get
            Set(ByVal value As String)
                _Memo_2 = value
            End Set
        End Property
        ''' <summary>メモ０３</summary>
        ''' <value>メモ０３</value>
        ''' <returns>メモ０３</returns>
        Public Property Memo_3() As String
            Get
                Return _Memo_3
            End Get
            Set(ByVal value As String)
                _Memo_3 = value
            End Set
        End Property
        ''' <summary>メモ０４</summary>
        ''' <value>メモ０４</value>
        ''' <returns>メモ０４</returns>
        Public Property Memo_4() As String
            Get
                Return _Memo_4
            End Get
            Set(ByVal value As String)
                _Memo_4 = value
            End Set
        End Property
        ''' <summary>メモ０５</summary>
        ''' <value>メモ０５</value>
        ''' <returns>メモ０５</returns>
        Public Property Memo_5() As String
            Get
                Return _Memo_5
            End Get
            Set(ByVal value As String)
                _Memo_5 = value
            End Set
        End Property
        ''' <summary>メモ０６</summary>
        ''' <value>メモ０６</value>
        ''' <returns>メモ０６</returns>
        Public Property Memo_6() As String
            Get
                Return _Memo_6
            End Get
            Set(ByVal value As String)
                _Memo_6 = value
            End Set
        End Property
        ''' <summary>メモ０７</summary>
        ''' <value>メモ０７</value>
        ''' <returns>メモ０７</returns>
        Public Property Memo_7() As String
            Get
                Return _Memo_7
            End Get
            Set(ByVal value As String)
                _Memo_7 = value
            End Set
        End Property
        ''' <summary>メモ０８</summary>
        ''' <value>メモ０８</value>
        ''' <returns>メモ０８</returns>
        Public Property Memo_8() As String
            Get
                Return _Memo_8
            End Get
            Set(ByVal value As String)
                _Memo_8 = value
            End Set
        End Property
        ''' <summary>メモ０９</summary>
        ''' <value>メモ０９</value>
        ''' <returns>メモ０９</returns>
        Public Property Memo_9() As String
            Get
                Return _Memo_9
            End Get
            Set(ByVal value As String)
                _Memo_9 = value
            End Set
        End Property
        ''' <summary>メモ１０</summary>
        ''' <value>メモ１０</value>
        ''' <returns>メモ１０</returns>
        Public Property Memo_10() As String
            Get
                Return _Memo_10
            End Get
            Set(ByVal value As String)
                _Memo_10 = value
            End Set
        End Property

        ''' <summary>メモ０１タイトル</summary>
        ''' <value>メモ０１タイトル</value>
        ''' <returns>メモ０１タイトル</returns>
        Public Property Memo_1_T() As String
            Get
                Return _Memo_1_T
            End Get
            Set(ByVal value As String)
                _Memo_1_T = value
            End Set
        End Property
        ''' <summary>メモ０２タイトル</summary>
        ''' <value>メモ０２タイトル</value>
        ''' <returns>メモ０２タイトル</returns>
        Public Property Memo_2_T() As String
            Get
                Return _Memo_2_T
            End Get
            Set(ByVal value As String)
                _Memo_2_T = value
            End Set
        End Property
        ''' <summary>メモ０３タイトル</summary>
        ''' <value>メモ０３タイトル</value>
        ''' <returns>メモ０３タイトル</returns>
        Public Property Memo_3_T() As String
            Get
                Return _Memo_3_T
            End Get
            Set(ByVal value As String)
                _Memo_3_T = value
            End Set
        End Property
        ''' <summary>メモ０４タイトル</summary>
        ''' <value>メモ０４タイトル</value>
        ''' <returns>メモ０４タイトル</returns>
        Public Property Memo_4_T() As String
            Get
                Return _Memo_4_T
            End Get
            Set(ByVal value As String)
                _Memo_4_T = value
            End Set
        End Property
        ''' <summary>メモ０５タイトル</summary>
        ''' <value>メモ０５タイトル</value>
        ''' <returns>メモ０５タイトル</returns>
        Public Property Memo_5_T() As String
            Get
                Return _Memo_5_T
            End Get
            Set(ByVal value As String)
                _Memo_5_T = value
            End Set
        End Property
        ''' <summary>メモ０６タイトル</summary>
        ''' <value>メモ０６タイトル</value>
        ''' <returns>メモ０６タイトル</returns>
        Public Property Memo_6_T() As String
            Get
                Return _Memo_6_T
            End Get
            Set(ByVal value As String)
                _Memo_6_T = value
            End Set
        End Property
        ''' <summary>メモ０７タイトル</summary>
        ''' <value>メモ０７タイトル</value>
        ''' <returns>メモ０７タイトル</returns>
        Public Property Memo_7_T() As String
            Get
                Return _Memo_7_T
            End Get
            Set(ByVal value As String)
                _Memo_7_T = value
            End Set
        End Property
        ''' <summary>メモ０８タイトル</summary>
        ''' <value>メモ０８タイトル</value>
        ''' <returns>メモ０８タイトル</returns>
        Public Property Memo_8_T() As String
            Get
                Return _Memo_8_T
            End Get
            Set(ByVal value As String)
                _Memo_8_T = value
            End Set
        End Property
        ''' <summary>メモ０９タイトル</summary>
        ''' <value>メモ０９タイトル</value>
        ''' <returns>メモ０９タイトル</returns>
        Public Property Memo_9_T() As String
            Get
                Return _Memo_9_T
            End Get
            Set(ByVal value As String)
                _Memo_9_T = value
            End Set
        End Property
        ''' <summary>メモ１０タイトル</summary>
        ''' <value>メモ１０タイトル</value>
        ''' <returns>メモ１０タイトル</returns>
        Public Property Memo_10_T() As String
            Get
                Return _Memo_10_T
            End Get
            Set(ByVal value As String)
                _Memo_10_T = value
            End Set
        End Property

        ''' <summary>備考</summary>
        ''' <value>備考</value>
        ''' <returns>備考</returns>
        Public Property Bikou() As String
            Get
                Return _Bikou
            End Get
            Set(ByVal value As String)
                _Bikou = value
            End Set
        End Property
        ''' <summary>編集登録日</summary>
        ''' <value>編集登録日</value>
        ''' <returns>編集登録日</returns>
        Public Property EditTourokubi() As Nullable(Of Int32)
            Get
                Return _EditTourokubi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _EditTourokubi = value
            End Set
        End Property
        ''' <summary>編集登録時間</summary>
        ''' <value>編集登録時間</value>
        ''' <returns>編集登録時間</returns>
        Public Property EditTourokujikan() As Nullable(Of Int32)
            Get
                Return _EditTourokujikan
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _EditTourokujikan = value
            End Set
        End Property
        ''' <summary>改訂判断フラグ</summary>
        ''' <value>改訂判断フラグ</value>
        ''' <returns>改訂判断フラグ</returns>
        Public Property KaiteiHandanFlg() As String
            Get
                Return _KaiteiHandanFlg
            End Get
            Set(ByVal value As String)
                _KaiteiHandanFlg = value
            End Set
        End Property
        ''' <summary>自動織込み改訂No</summary>
        ''' <value>自動織込み改訂No</value>
        ''' <returns>自動織込み改訂No</returns>
        Public Property AutoOrikomiKaiteiNo() As String
            Get
                Return _AutoOrikomiKaiteiNo
            End Get
            Set(ByVal value As String)
                _AutoOrikomiKaiteiNo = value
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