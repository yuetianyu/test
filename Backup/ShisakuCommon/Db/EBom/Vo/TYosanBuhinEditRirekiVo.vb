Namespace Db.EBom.Vo
    ''' <summary>予算書部品履歴情報</summary>
    Public Class TYosanBuhinEditRirekiVo
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
        ''' <summary>レベル</summary>
        Private _YosanLevel As Nullable(Of Int32)
        ''' <summary>国内集計コード</summary>
        Private _YosanShukeiCode As String
        ''' <summary>海外SIA集計コード</summary>
        Private _YosanSiaShukeiCode As String
        ''' <summary>取引先コード</summary>
        Private _YosanMakerCode As String
        ''' <summary>取引先名称</summary>
        Private _YosanMakerName As String
        ''' <summary>部品番号</summary>
        Private _YosanBuhinNo As String
        ''' <summary>部品番号試作区分</summary>
        Private _YosanBuhinNoKbn As String
        ''' <summary>部品名称</summary>
        Private _YosanBuhinName As String
        ''' <summary>供給セクション</summary>
        Private _YosanKyoukuSection As String
        ''' <summary>員数</summary>
        Private _YosanInsu As Nullable(Of Int32)
        ''' <summary>変更概要</summary>
        Private _YosanHenkoGaiyo As String
        ''' <summary>部品費（量産）</summary>
        Private _YosanBuhinHiRyosan As Nullable(Of Decimal)
        ''' <summary>部品費（部品表）</summary>
        Private _YosanBuhinHiBuhinhyo As Nullable(Of Decimal)
        ''' <summary>部品費（特記）</summary>
        Private _YosanBuhinHiTokki As Nullable(Of Decimal)
        ''' <summary>型費</summary>
        Private _YosanKataHi As Nullable(Of Decimal)
        ''' <summary>治具費</summary>
        Private _YosanJiguHi As Nullable(Of Decimal)
        ''' <summary>工数</summary>
        Private _YosanKosu As Nullable(Of Decimal)
        ''' <summary>発注実績(割付MIX値全体と平均値)</summary>
        Private _YosanHachuJisekiMix As Nullable(Of Decimal)
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
        ''' <summary>レベル</summary>
        ''' <value>レベル</value>
        ''' <returns>レベル</returns>
        Public Property YosanLevel() As Nullable(Of Int32)
            Get
                Return _YosanLevel
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanLevel = value
            End Set
        End Property
        ''' <summary>国内集計コード</summary>
        ''' <value>国内集計コード</value>
        ''' <returns>国内集計コード</returns>
        Public Property YosanShukeiCode() As String
            Get
                Return _YosanShukeiCode
            End Get
            Set(ByVal value As String)
                _YosanShukeiCode = value
            End Set
        End Property
        ''' <summary>海外SIA集計コード</summary>
        ''' <value>海外SIA集計コード</value>
        ''' <returns>海外SIA集計コード</returns>
        Public Property YosanSiaShukeiCode() As String
            Get
                Return _YosanSiaShukeiCode
            End Get
            Set(ByVal value As String)
                _YosanSiaShukeiCode = value
            End Set
        End Property
        ''' <summary>取引先コード</summary>
        ''' <value>取引先コード</value>
        ''' <returns>取引先コード</returns>
        Public Property YosanMakerCode() As String
            Get
                Return _YosanMakerCode
            End Get
            Set(ByVal value As String)
                _YosanMakerCode = value
            End Set
        End Property
        ''' <summary>取引先名称</summary>
        ''' <value>取引先名称</value>
        ''' <returns>取引先名称</returns>
        Public Property YosanMakerName() As String
            Get
                Return _YosanMakerName
            End Get
            Set(ByVal value As String)
                _YosanMakerName = value
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
        ''' <summary>部品番号試作区分</summary>
        ''' <value>部品番号試作区分</value>
        ''' <returns>部品番号試作区分</returns>
        Public Property YosanBuhinNoKbn() As String
            Get
                Return _YosanBuhinNoKbn
            End Get
            Set(ByVal value As String)
                _YosanBuhinNoKbn = value
            End Set
        End Property
        ''' <summary>部品名称</summary>
        ''' <value>部品名称</value>
        ''' <returns>部品名称</returns>
        Public Property YosanBuhinName() As String
            Get
                Return _YosanBuhinName
            End Get
            Set(ByVal value As String)
                _YosanBuhinName = value
            End Set
        End Property
        ''' <summary>供給セクション</summary>
        ''' <value>供給セクション</value>
        ''' <returns>供給セクション</returns>
        Public Property YosanKyoukuSection() As String
            Get
                Return _YosanKyoukuSection
            End Get
            Set(ByVal value As String)
                _YosanKyoukuSection = value
            End Set
        End Property
        ''' <summary>員数</summary>
        ''' <value>員数</value>
        ''' <returns>員数</returns>
        Public Property YosanInsu() As Nullable(Of Int32)
            Get
                Return _YosanInsu
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanInsu = value
            End Set
        End Property
        ''' <summary>変更概要</summary>
        ''' <value>変更概要</value>
        ''' <returns>変更概要</returns>
        Public Property YosanHenkoGaiyo() As String
            Get
                Return _YosanHenkoGaiyo
            End Get
            Set(ByVal value As String)
                _YosanHenkoGaiyo = value
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
        ''' <summary>部品費（部品表）</summary>
        ''' <value>部品費（部品表）</value>
        ''' <returns>部品費（部品表）</returns>
        Public Property YosanBuhinHiBuhinhyo() As Nullable(Of Decimal)
            Get
                Return _YosanBuhinHiBuhinhyo
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanBuhinHiBuhinhyo = value
            End Set
        End Property
        ''' <summary>部品費（特記）</summary>
        ''' <value>部品費（特記）</value>
        ''' <returns>部品費（特記）</returns>
        Public Property YosanBuhinHiTokki() As Nullable(Of Decimal)
            Get
                Return _YosanBuhinHiTokki
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanBuhinHiTokki = value
            End Set
        End Property
        ''' <summary>型費</summary>
        ''' <value>型費</value>
        ''' <returns>型費</returns>
        Public Property YosanKataHi() As Nullable(Of Decimal)
            Get
                Return _YosanKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanKataHi = value
            End Set
        End Property
        ''' <summary>治具費</summary>
        ''' <value>治具費</value>
        ''' <returns>治具費</returns>
        Public Property YosanJiguHi() As Nullable(Of Decimal)
            Get
                Return _YosanJiguHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanJiguHi = value
            End Set
        End Property
        ''' <summary>工数</summary>
        ''' <value>工数</value>
        ''' <returns>工数</returns>
        Public Property YosanKosu() As Nullable(Of Decimal)
            Get
                Return _YosanKosu
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanKosu = value
            End Set
        End Property
        ''' <summary>発注実績(割付 MIX値全体と平均値)</summary>
        ''' <value>発注実績(割付 MIX値全体と平均値)</value>
        ''' <returns>発注実績(割付 MIX値全体と平均値)</returns>
        Public Property YosanHachuJisekiMix() As Nullable(Of Decimal)
            Get
                Return _YosanHachuJisekiMix
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanHachuJisekiMix = value
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