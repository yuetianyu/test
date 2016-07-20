Namespace Db.EBom.Vo
    ''' <summary>
    ''' 予算設定部品表情報を保持する。
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TYosanSetteiBuhinVo
        ''' <summary>試作イベントコード</summary>
        Private _ShisakuEventCode As String
        Public Property ShisakuEventCode() As String
            Get
                Return _ShisakuEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuEventCode = value
            End Set
        End Property

        ''' <summary>予算リストコード</summary>
        Private _YosanListCode As String
        Public Property YosanListCode() As String
            Get
                Return _YosanListCode
            End Get
            Set(ByVal value As String)
                _YosanListCode = value
            End Set
        End Property

        ''' <summary>予算部課コード</summary>
        Private _YosanBukaCode As String
        Public Property YosanBukaCode() As String
            Get
                Return _YosanBukaCode
            End Get
            Set(ByVal value As String)
                _YosanBukaCode = value
            End Set
        End Property

        ''' <summary>予算ブロック№</summary>
        Private _YosanBlockNo As String
        Public Property YosanBlockNo() As String
            Get
                Return _YosanBlockNo
            End Get
            Set(ByVal value As String)
                _YosanBlockNo = value
            End Set
        End Property

        ''' <summary>部品番号表示順</summary>
        Private _BuhinNoHyoujiJun As Nullable(Of Int32)
        Public Property BuhinNoHyoujiJun() As Nullable(Of Int32)
            Get
                Return _BuhinNoHyoujiJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _BuhinNoHyoujiJun = value
            End Set
        End Property

        ''' <summary>ソート順</summary>
        Private _YosanSortJun As Nullable(Of Int32)
        Public Property YosanSortJun() As Nullable(Of Int32)
            Get
                Return _YosanSortJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanSortJun = value
            End Set
        End Property

        ''' <summary>行ID</summary>
        Private _YosanGyouId As String
        Public Property YosanGyouId() As String
            Get
                Return _YosanGyouId
            End Get
            Set(ByVal value As String)
                _YosanGyouId = value
            End Set
        End Property

        ''' <summary>レベル</summary>
        Private _YosanLevel As Nullable(Of Int32)
        Public Property YosanLevel() As Nullable(Of Int32)
            Get
                Return _YosanLevel
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanLevel = value
            End Set
        End Property

        ''' <summary>国内集計コード</summary>
        Private _YosanShukeiCode As String
        Public Property YosanShukeiCode() As String
            Get
                Return _YosanShukeiCode
            End Get
            Set(ByVal value As String)
                _YosanShukeiCode = value
            End Set
        End Property

        ''' <summary>海外SIA集計コード</summary>
        Private _YosanSiaShukeiCode As String
        Public Property YosanSiaShukeiCode() As String
            Get
                Return _YosanSiaShukeiCode
            End Get
            Set(ByVal value As String)
                _YosanSiaShukeiCode = value
            End Set
        End Property

        ''' <summary>部品番号</summary>
        Private _YosanBuhinNo As String
        Public Property YosanBuhinNo() As String
            Get
                Return _YosanBuhinNo
            End Get
            Set(ByVal value As String)
                _YosanBuhinNo = value
            End Set
        End Property

        ''' <summary>部品名称</summary>
        Private _YosanBuhinName As String
        Public Property YosanBuhinName() As String
            Get
                Return _YosanBuhinName
            End Get
            Set(ByVal value As String)
                _YosanBuhinName = value
            End Set
        End Property

        ''' <summary>合計員数</summary>
        Private _YosanInsu As Nullable(Of Int32)
        Public Property YosanInsu() As Nullable(Of Int32)
            Get
                Return _YosanInsu
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanInsu = value
            End Set
        End Property

        ''' <summary>取引先コード</summary>
        Private _YosanMakerCode As String
        Public Property YosanMakerCode() As String
            Get
                Return _YosanMakerCode
            End Get
            Set(ByVal value As String)
                _YosanMakerCode = value
            End Set
        End Property

        ''' <summary>供給セクション</summary>
        Private _YosanKyoukuSection As String
        Public Property YosanKyoukuSection() As String
            Get
                Return _YosanKyoukuSection
            End Get
            Set(ByVal value As String)
                _YosanKyoukuSection = value
            End Set
        End Property

        ''' <summary>購担</summary>
        Private _YosanKoutan As String
        Public Property YosanKoutan() As String
            Get
                Return _YosanKoutan
            End Get
            Set(ByVal value As String)
                _YosanKoutan = value
            End Set
        End Property

        ''' <summary>手配記号</summary>
        Private _YosanTehaiKigou As String
        Public Property YosanTehaiKigou() As String
            Get
                Return _YosanTehaiKigou
            End Get
            Set(ByVal value As String)
                _YosanTehaiKigou = value
            End Set
        End Property

        ''' <summary>設計情報_作り方・製作方法</summary>
        Private _YosanTsukurikataSeisaku As String
        Public Property YosanTsukurikataSeisaku() As String
            Get
                Return _YosanTsukurikataSeisaku
            End Get
            Set(ByVal value As String)
                _YosanTsukurikataSeisaku = value
            End Set
        End Property

        ''' <summary>設計情報_作り方・型仕様_1</summary>
        Private _YosanTsukurikataKatashiyou1 As String
        Public Property YosanTsukurikataKatashiyou1() As String
            Get
                Return _YosanTsukurikataKatashiyou1
            End Get
            Set(ByVal value As String)
                _YosanTsukurikataKatashiyou1 = value
            End Set
        End Property

        ''' <summary>設計情報_作り方・型仕様_2</summary>
        Private _YosanTsukurikataKatashiyou2 As String
        Public Property YosanTsukurikataKatashiyou2() As String
            Get
                Return _YosanTsukurikataKatashiyou2
            End Get
            Set(ByVal value As String)
                _YosanTsukurikataKatashiyou2 = value
            End Set
        End Property

        ''' <summary>設計情報_作り方・型仕様_3</summary>
        Private _YosanTsukurikataKatashiyou3 As String
        Public Property YosanTsukurikataKatashiyou3() As String
            Get
                Return _YosanTsukurikataKatashiyou3
            End Get
            Set(ByVal value As String)
                _YosanTsukurikataKatashiyou3 = value
            End Set
        End Property

        ''' <summary>設計情報_作り方・治具</summary>
        Private _YosanTsukurikataTigu As String
        Public Property YosanTsukurikataTigu() As String
            Get
                Return _YosanTsukurikataTigu
            End Get
            Set(ByVal value As String)
                _YosanTsukurikataTigu = value
            End Set
        End Property

        ''' <summary>設計情報_作り方・部品製作規模・概要</summary>
        Private _YosanTsukurikataKibo As String
        Public Property YosanTsukurikataKibo() As String
            Get
                Return _YosanTsukurikataKibo
            End Get
            Set(ByVal value As String)
                _YosanTsukurikataKibo = value
            End Set
        End Property

        ''' <summary>設計情報_試作部品費（円）</summary>
        Private _YosanShisakuBuhinHi As Nullable(Of Int32)
        Public Property YosanShisakuBuhinHi() As Nullable(Of Int32)
            Get
                Return _YosanShisakuBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanShisakuBuhinHi = value
            End Set
        End Property

        ''' <summary>設計情報_試作型費（千円）</summary>
        Private _YosanShisakuKataHi As Nullable(Of Int32)
        Public Property YosanShisakuKataHi() As Nullable(Of Int32)
            Get
                Return _YosanShisakuKataHi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _YosanShisakuKataHi = value
            End Set
        End Property

        ''' <summary>設計情報_部品ノート</summary>
        Private _YosanBuhinNote As String
        Public Property YosanBuhinNote() As String
            Get
                Return _YosanBuhinNote
            End Get
            Set(ByVal value As String)
                _YosanBuhinNote = value
            End Set
        End Property

        ''' <summary>設計情報_備考</summary>
        Private _YosanBikou As String
        Public Property YosanBikou() As String
            Get
                Return _YosanBikou
            End Get
            Set(ByVal value As String)
                _YosanBikou = value
            End Set
        End Property

        ''' <summary>部品費根拠_国外区分</summary>
        Private _YosanKonkyoKokugaiKbn As String
        Public Property YosanKonkyoKokugaiKbn() As String
            Get
                Return _YosanKonkyoKokugaiKbn
            End Get
            Set(ByVal value As String)
                _YosanKonkyoKokugaiKbn = value
            End Set
        End Property

        ''' <summary>部品費根拠_MIXコスト部品費(円/ｾﾝﾄ)</summary>
        Private _YosanKonkyoMixBuhinHi As Nullable(Of Decimal)
        Public Property YosanKonkyoMixBuhinHi() As Nullable(Of Decimal)
            Get
                Return _YosanKonkyoMixBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanKonkyoMixBuhinHi = value
            End Set
        End Property

        ''' <summary>部品費根拠_引用元MIX値部品費</summary>
        Private _YosanKonkyoInyouMixBuhinHi As String
        Public Property YosanKonkyoInyouMixBuhinHi() As String
            Get
                Return _YosanKonkyoInyouMixBuhinHi
            End Get
            Set(ByVal value As String)
                _YosanKonkyoInyouMixBuhinHi = value
            End Set
        End Property

        ''' <summary>部品費根拠_係数１</summary>
        Private _YosanKonkyoKeisu1 As Nullable(Of Decimal)
        Public Property YosanKonkyoKeisu1() As Nullable(Of Decimal)
            Get
                Return _YosanKonkyoKeisu1
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanKonkyoKeisu1 = value
            End Set
        End Property

        ''' <summary>部品費根拠_工法</summary>
        Private _YosanKonkyoKouhou As String
        Public Property YosanKonkyoKouhou() As String
            Get
                Return _YosanKonkyoKouhou
            End Get
            Set(ByVal value As String)
                _YosanKonkyoKouhou = value
            End Set
        End Property

        ''' <summary>割付予算_部品費(円)</summary>
        Private _YosanWaritukeBuhinHi As Nullable(Of Decimal)
        Public Property YosanWaritukeBuhinHi() As Nullable(Of Decimal)
            Get
                Return _YosanWaritukeBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanWaritukeBuhinHi = value
            End Set
        End Property

        ''' <summary>割付予算_係数２</summary>
        Private _YosanWaritukeKeisu2 As Nullable(Of Decimal)
        Public Property YosanWaritukeKeisu2() As Nullable(Of Decimal)
            Get
                Return _YosanWaritukeKeisu2
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanWaritukeKeisu2 = value
            End Set
        End Property

        ''' <summary>①量産単価（円）</summary>
        Private _Kako1RyosanTanka As Nullable(Of Decimal)
        Public Property Kako1RyosanTanka() As Nullable(Of Decimal)
            Get
                Return _Kako1RyosanTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako1RyosanTanka = value
            End Set
        End Property

        ''' <summary>①割付部品費（円）</summary>
        Private _Kako1WaritukeBuhinHi As Nullable(Of Decimal)
        Public Property Kako1WaritukeBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako1WaritukeBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako1WaritukeBuhinHi = value
            End Set
        End Property

        ''' <summary>①割付型費（千円）</summary>
        Private _Kako1WaritukeKataHi As Nullable(Of Decimal)
        Public Property Kako1WaritukeKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako1WaritukeKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako1WaritukeKataHi = value
            End Set
        End Property

        ''' <summary>①割付工法</summary>
        Private _Kako1WaritukeKouhou As String
        Public Property Kako1WaritukeKouhou() As String
            Get
                Return _Kako1WaritukeKouhou
            End Get
            Set(ByVal value As String)
                _Kako1WaritukeKouhou = value
            End Set
        End Property

        ''' <summary>①ﾒｰｶｰ値部品費（円）</summary>
        Private _Kako1MakerBuhinHi As Nullable(Of Decimal)
        Public Property Kako1MakerBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako1MakerBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako1MakerBuhinHi = value
            End Set
        End Property

        ''' <summary>①ﾒｰｶｰ値型費（千円）</summary>
        Private _Kako1MakerKataHi As Nullable(Of Decimal)
        Public Property Kako1MakerKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako1MakerKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako1MakerKataHi = value
            End Set
        End Property

        ''' <summary>①ﾒｰｶｰ値工法</summary>
        Private _Kako1MakerKouhou As String
        Public Property Kako1MakerKouhou() As String
            Get
                Return _Kako1MakerKouhou
            End Get
            Set(ByVal value As String)
                _Kako1MakerKouhou = value
            End Set
        End Property

        ''' <summary>①審議値部品費（円）</summary>
        Private _Kako1ShingiBuhinHi As Nullable(Of Decimal)
        Public Property Kako1ShingiBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako1ShingiBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako1ShingiBuhinHi = value
            End Set
        End Property

        ''' <summary>①審議値型費（千円）</summary>
        Private _Kako1ShingiKataHi As Nullable(Of Decimal)
        Public Property Kako1ShingiKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako1ShingiKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako1ShingiKataHi = value
            End Set
        End Property

        ''' <summary>①審議値工法</summary>
        Private _Kako1ShingiKouhou As String
        Public Property Kako1ShingiKouhou() As String
            Get
                Return _Kako1ShingiKouhou
            End Get
            Set(ByVal value As String)
                _Kako1ShingiKouhou = value
            End Set
        End Property

        ''' <summary>①購入希望単価（円）</summary>
        Private _Kako1KounyuKibouTanka As Nullable(Of Decimal)
        Public Property Kako1KounyuKibouTanka() As Nullable(Of Decimal)
            Get
                Return _Kako1KounyuKibouTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako1KounyuKibouTanka = value
            End Set
        End Property

        ''' <summary>①購入単価（円）</summary>
        Private _Kako1KounyuTanka As Nullable(Of Decimal)
        Public Property Kako1KounyuTanka() As Nullable(Of Decimal)
            Get
                Return _Kako1KounyuTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako1KounyuTanka = value
            End Set
        End Property

        ''' <summary>①支給品（円）</summary>
        Private _Kako1ShikyuHin As Nullable(Of Decimal)
        Public Property Kako1ShikyuHin() As Nullable(Of Decimal)
            Get
                Return _Kako1ShikyuHin
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako1ShikyuHin = value
            End Set
        End Property

        ''' <summary>①工事指令№</summary>
        Private _Kako1KoujiShireiNo As String
        Public Property Kako1KoujiShireiNo() As String
            Get
                Return _Kako1KoujiShireiNo
            End Get
            Set(ByVal value As String)
                _Kako1KoujiShireiNo = value
            End Set
        End Property

        ''' <summary>①イベント名称</summary>
        Private _Kako1EventName As String
        Public Property Kako1EventName() As String
            Get
                Return _Kako1EventName
            End Get
            Set(ByVal value As String)
                _Kako1EventName = value
            End Set
        End Property

        ''' <summary>①発注日</summary>
        Private _Kako1HachuBi As Nullable(Of Integer)
        Public Property Kako1HachuBi() As Nullable(Of Integer)
            Get
                Return _Kako1HachuBi
            End Get
            Set(ByVal value As Nullable(Of Integer))
                _Kako1HachuBi = value
            End Set
        End Property

        ''' <summary>①検収日</summary>
        Private _Kako1KenshuBi As Nullable(Of Integer)
        Public Property Kako1KenshuBi() As Nullable(Of Integer)
            Get
                Return _Kako1KenshuBi
            End Get
            Set(ByVal value As Nullable(Of Integer))
                _Kako1KenshuBi = value
            End Set
        End Property

        ''' <summary>②量産単価（円）</summary>
        Private _Kako2RyosanTanka As Nullable(Of Decimal)
        Public Property Kako2RyosanTanka() As Nullable(Of Decimal)
            Get
                Return _Kako2RyosanTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako2RyosanTanka = value
            End Set
        End Property

        ''' <summary>②割付部品費（円）</summary>
        Private _Kako2WaritukeBuhinHi As Nullable(Of Decimal)
        Public Property Kako2WaritukeBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako2WaritukeBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako2WaritukeBuhinHi = value
            End Set
        End Property

        ''' <summary>②割付型費（千円）</summary>
        Private _Kako2WaritukeKataHi As Nullable(Of Decimal)
        Public Property Kako2WaritukeKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako2WaritukeKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako2WaritukeKataHi = value
            End Set
        End Property

        ''' <summary>②割付工法</summary>
        Private _Kako2WaritukeKouhou As String
        Public Property Kako2WaritukeKouhou() As String
            Get
                Return _Kako2WaritukeKouhou
            End Get
            Set(ByVal value As String)
                _Kako2WaritukeKouhou = value
            End Set
        End Property

        ''' <summary>②ﾒｰｶｰ値部品費（円）</summary>
        Private _Kako2MakerBuhinHi As Nullable(Of Decimal)
        Public Property Kako2MakerBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako2MakerBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako2MakerBuhinHi = value
            End Set
        End Property

        ''' <summary>②ﾒｰｶｰ値型費（千円）</summary>
        Private _Kako2MakerKataHi As Nullable(Of Decimal)
        Public Property Kako2MakerKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako2MakerKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako2MakerKataHi = value
            End Set
        End Property

        ''' <summary>②ﾒｰｶｰ値工法</summary>
        Private _Kako2MakerKouhou As String
        Public Property Kako2MakerKouhou() As String
            Get
                Return _Kako2MakerKouhou
            End Get
            Set(ByVal value As String)
                _Kako2MakerKouhou = value
            End Set
        End Property

        ''' <summary>②審議値部品費（円）</summary>
        Private _Kako2ShingiBuhinHi As Nullable(Of Decimal)
        Public Property Kako2ShingiBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako2ShingiBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako2ShingiBuhinHi = value
            End Set
        End Property

        ''' <summary>②審議値型費（千円）</summary>
        Private _Kako2ShingiKataHi As Nullable(Of Decimal)
        Public Property Kako2ShingiKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako2ShingiKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako2ShingiKataHi = value
            End Set
        End Property

        ''' <summary>②審議値工法</summary>
        Private _Kako2ShingiKouhou As String
        Public Property Kako2ShingiKouhou() As String
            Get
                Return _Kako2ShingiKouhou
            End Get
            Set(ByVal value As String)
                _Kako2ShingiKouhou = value
            End Set
        End Property

        ''' <summary>②購入希望単価（円）</summary>
        Private _Kako2KounyuKibouTanka As Nullable(Of Decimal)
        Public Property Kako2KounyuKibouTanka() As Nullable(Of Decimal)
            Get
                Return _Kako2KounyuKibouTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako2KounyuKibouTanka = value
            End Set
        End Property

        ''' <summary>②購入単価（円）</summary>
        Private _Kako2KounyuTanka As Nullable(Of Decimal)
        Public Property Kako2KounyuTanka() As Nullable(Of Decimal)
            Get
                Return _Kako2KounyuTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako2KounyuTanka = value
            End Set
        End Property

        ''' <summary>②支給品（円）</summary>
        Private _Kako2ShikyuHin As Nullable(Of Decimal)
        Public Property Kako2ShikyuHin() As Nullable(Of Decimal)
            Get
                Return _Kako2ShikyuHin
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako2ShikyuHin = value
            End Set
        End Property

        ''' <summary>②工事指令№</summary>
        Private _Kako2KoujiShireiNo As String
        Public Property Kako2KoujiShireiNo() As String
            Get
                Return _Kako2KoujiShireiNo
            End Get
            Set(ByVal value As String)
                _Kako2KoujiShireiNo = value
            End Set
        End Property

        ''' <summary>②イベント名称</summary>
        Private _Kako2EventName As String
        Public Property Kako2EventName() As String
            Get
                Return _Kako2EventName
            End Get
            Set(ByVal value As String)
                _Kako2EventName = value
            End Set
        End Property

        ''' <summary>②発注日</summary>
        Private _Kako2HachuBi As Nullable(Of Integer)
        Public Property Kako2HachuBi() As Nullable(Of Integer)
            Get
                Return _Kako2HachuBi
            End Get
            Set(ByVal value As Nullable(Of Integer))
                _Kako2HachuBi = value
            End Set
        End Property

        ''' <summary>②検収日</summary>
        Private _Kako2KenshuBi As Nullable(Of Integer)
        Public Property Kako2KenshuBi() As Nullable(Of Integer)
            Get
                Return _Kako2KenshuBi
            End Get
            Set(ByVal value As Nullable(Of Integer))
                _Kako2KenshuBi = value
            End Set
        End Property


        ''' <summary>③量産単価（円）</summary>
        Private _Kako3RyosanTanka As Nullable(Of Decimal)
        Public Property Kako3RyosanTanka() As Nullable(Of Decimal)
            Get
                Return _Kako3RyosanTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako3RyosanTanka = value
            End Set
        End Property

        ''' <summary>③割付部品費（円）</summary>
        Private _Kako3WaritukeBuhinHi As Nullable(Of Decimal)
        Public Property Kako3WaritukeBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako3WaritukeBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako3WaritukeBuhinHi = value
            End Set
        End Property

        ''' <summary>③割付型費（千円）</summary>
        Private _Kako3WaritukeKataHi As Nullable(Of Decimal)
        Public Property Kako3WaritukeKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako3WaritukeKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako3WaritukeKataHi = value
            End Set
        End Property

        ''' <summary>③割付工法</summary>
        Private _Kako3WaritukeKouhou As String
        Public Property Kako3WaritukeKouhou() As String
            Get
                Return _Kako3WaritukeKouhou
            End Get
            Set(ByVal value As String)
                _Kako3WaritukeKouhou = value
            End Set
        End Property

        ''' <summary>③ﾒｰｶｰ値部品費（円）</summary>
        Private _Kako3MakerBuhinHi As Nullable(Of Decimal)
        Public Property Kako3MakerBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako3MakerBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako3MakerBuhinHi = value
            End Set
        End Property

        ''' <summary>③ﾒｰｶｰ値型費（千円）</summary>
        Private _Kako3MakerKataHi As Nullable(Of Decimal)
        Public Property Kako3MakerKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako3MakerKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako3MakerKataHi = value
            End Set
        End Property

        ''' <summary>③ﾒｰｶｰ値工法</summary>
        Private _Kako3MakerKouhou As String
        Public Property Kako3MakerKouhou() As String
            Get
                Return _Kako3MakerKouhou
            End Get
            Set(ByVal value As String)
                _Kako3MakerKouhou = value
            End Set
        End Property

        ''' <summary>③審議値部品費（円）</summary>
        Private _Kako3ShingiBuhinHi As Nullable(Of Decimal)
        Public Property Kako3ShingiBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako3ShingiBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako3ShingiBuhinHi = value
            End Set
        End Property

        ''' <summary>③審議値型費（千円）</summary>
        Private _Kako3ShingiKataHi As Nullable(Of Decimal)
        Public Property Kako3ShingiKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako3ShingiKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako3ShingiKataHi = value
            End Set
        End Property

        ''' <summary>③審議値工法</summary>
        Private _Kako3ShingiKouhou As String
        Public Property Kako3ShingiKouhou() As String
            Get
                Return _Kako3ShingiKouhou
            End Get
            Set(ByVal value As String)
                _Kako3ShingiKouhou = value
            End Set
        End Property

        ''' <summary>③購入希望単価（円）</summary>
        Private _Kako3KounyuKibouTanka As Nullable(Of Decimal)
        Public Property Kako3KounyuKibouTanka() As Nullable(Of Decimal)
            Get
                Return _Kako3KounyuKibouTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako3KounyuKibouTanka = value
            End Set
        End Property

        ''' <summary>③購入単価（円）</summary>
        Private _Kako3KounyuTanka As Nullable(Of Decimal)
        Public Property Kako3KounyuTanka() As Nullable(Of Decimal)
            Get
                Return _Kako3KounyuTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako3KounyuTanka = value
            End Set
        End Property

        ''' <summary>③支給品（円）</summary>
        Private _Kako3ShikyuHin As Nullable(Of Decimal)
        Public Property Kako3ShikyuHin() As Nullable(Of Decimal)
            Get
                Return _Kako3ShikyuHin
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako3ShikyuHin = value
            End Set
        End Property

        ''' <summary>③工事指令№</summary>
        Private _Kako3KoujiShireiNo As String
        Public Property Kako3KoujiShireiNo() As String
            Get
                Return _Kako3KoujiShireiNo
            End Get
            Set(ByVal value As String)
                _Kako3KoujiShireiNo = value
            End Set
        End Property

        ''' <summary>③イベント名称</summary>
        Private _Kako3EventName As String
        Public Property Kako3EventName() As String
            Get
                Return _Kako3EventName
            End Get
            Set(ByVal value As String)
                _Kako3EventName = value
            End Set
        End Property

        ''' <summary>③発注日</summary>
        Private _Kako3HachuBi As Nullable(Of Integer)
        Public Property Kako3HachuBi() As Nullable(Of Integer)
            Get
                Return _Kako3HachuBi
            End Get
            Set(ByVal value As Nullable(Of Integer))
                _Kako3HachuBi = value
            End Set
        End Property

        ''' <summary>③検収日</summary>
        Private _Kako3KenshuBi As Nullable(Of Integer)
        Public Property Kako3KenshuBi() As Nullable(Of Integer)
            Get
                Return _Kako3KenshuBi
            End Get
            Set(ByVal value As Nullable(Of Integer))
                _Kako3KenshuBi = value
            End Set
        End Property


        ''' <summary>④量産単価（円）</summary>
        Private _Kako4RyosanTanka As Nullable(Of Decimal)
        Public Property Kako4RyosanTanka() As Nullable(Of Decimal)
            Get
                Return _Kako4RyosanTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako4RyosanTanka = value
            End Set
        End Property

        ''' <summary>④割付部品費（円）</summary>
        Private _Kako4WaritukeBuhinHi As Nullable(Of Decimal)
        Public Property Kako4WaritukeBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako4WaritukeBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako4WaritukeBuhinHi = value
            End Set
        End Property

        ''' <summary>④割付型費（千円）</summary>
        Private _Kako4WaritukeKataHi As Nullable(Of Decimal)
        Public Property Kako4WaritukeKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako4WaritukeKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako4WaritukeKataHi = value
            End Set
        End Property

        ''' <summary>④割付工法</summary>
        Private _Kako4WaritukeKouhou As String
        Public Property Kako4WaritukeKouhou() As String
            Get
                Return _Kako4WaritukeKouhou
            End Get
            Set(ByVal value As String)
                _Kako4WaritukeKouhou = value
            End Set
        End Property

        ''' <summary>④ﾒｰｶｰ値部品費（円）</summary>
        Private _Kako4MakerBuhinHi As Nullable(Of Decimal)
        Public Property Kako4MakerBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako4MakerBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako4MakerBuhinHi = value
            End Set
        End Property

        ''' <summary>④ﾒｰｶｰ値型費（千円）</summary>
        Private _Kako4MakerKataHi As Nullable(Of Decimal)
        Public Property Kako4MakerKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako4MakerKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako4MakerKataHi = value
            End Set
        End Property

        ''' <summary>④ﾒｰｶｰ値工法</summary>
        Private _Kako4MakerKouhou As String
        Public Property Kako4MakerKouhou() As String
            Get
                Return _Kako4MakerKouhou
            End Get
            Set(ByVal value As String)
                _Kako4MakerKouhou = value
            End Set
        End Property

        ''' <summary>④審議値部品費（円）</summary>
        Private _Kako4ShingiBuhinHi As Nullable(Of Decimal)
        Public Property Kako4ShingiBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako4ShingiBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako4ShingiBuhinHi = value
            End Set
        End Property

        ''' <summary>④審議値型費（千円）</summary>
        Private _Kako4ShingiKataHi As Nullable(Of Decimal)
        Public Property Kako4ShingiKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako4ShingiKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako4ShingiKataHi = value
            End Set
        End Property

        ''' <summary>④審議値工法</summary>
        Private _Kako4ShingiKouhou As String
        Public Property Kako4ShingiKouhou() As String
            Get
                Return _Kako4ShingiKouhou
            End Get
            Set(ByVal value As String)
                _Kako4ShingiKouhou = value
            End Set
        End Property

        ''' <summary>④購入希望単価（円）</summary>
        Private _Kako4KounyuKibouTanka As Nullable(Of Decimal)
        Public Property Kako4KounyuKibouTanka() As Nullable(Of Decimal)
            Get
                Return _Kako4KounyuKibouTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako4KounyuKibouTanka = value
            End Set
        End Property

        ''' <summary>④購入単価（円）</summary>
        Private _Kako4KounyuTanka As Nullable(Of Decimal)
        Public Property Kako4KounyuTanka() As Nullable(Of Decimal)
            Get
                Return _Kako4KounyuTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako4KounyuTanka = value
            End Set
        End Property

        ''' <summary>④支給品（円）</summary>
        Private _Kako4ShikyuHin As Nullable(Of Decimal)
        Public Property Kako4ShikyuHin() As Nullable(Of Decimal)
            Get
                Return _Kako4ShikyuHin
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako4ShikyuHin = value
            End Set
        End Property

        ''' <summary>④工事指令№</summary>
        Private _Kako4KoujiShireiNo As String
        Public Property Kako4KoujiShireiNo() As String
            Get
                Return _Kako4KoujiShireiNo
            End Get
            Set(ByVal value As String)
                _Kako4KoujiShireiNo = value
            End Set
        End Property

        ''' <summary>④イベント名称</summary>
        Private _Kako4EventName As String
        Public Property Kako4EventName() As String
            Get
                Return _Kako4EventName
            End Get
            Set(ByVal value As String)
                _Kako4EventName = value
            End Set
        End Property

        ''' <summary>④発注日</summary>
        Private _Kako4HachuBi As Nullable(Of Integer)
        Public Property Kako4HachuBi() As Nullable(Of Integer)
            Get
                Return _Kako4HachuBi
            End Get
            Set(ByVal value As Nullable(Of Integer))
                _Kako4HachuBi = value
            End Set
        End Property

        ''' <summary>④検収日</summary>
        Private _Kako4KenshuBi As Nullable(Of Integer)
        Public Property Kako4KenshuBi() As Nullable(Of Integer)
            Get
                Return _Kako4KenshuBi
            End Get
            Set(ByVal value As Nullable(Of Integer))
                _Kako4KenshuBi = value
            End Set
        End Property


        ''' <summary>⑤量産単価（円）</summary>
        Private _Kako5RyosanTanka As Nullable(Of Decimal)
        Public Property Kako5RyosanTanka() As Nullable(Of Decimal)
            Get
                Return _Kako5RyosanTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako5RyosanTanka = value
            End Set
        End Property

        ''' <summary>⑤割付部品費（円）</summary>
        Private _Kako5WaritukeBuhinHi As Nullable(Of Decimal)
        Public Property Kako5WaritukeBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako5WaritukeBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako5WaritukeBuhinHi = value
            End Set
        End Property

        ''' <summary>⑤割付型費（千円）</summary>
        Private _Kako5WaritukeKataHi As Nullable(Of Decimal)
        Public Property Kako5WaritukeKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako5WaritukeKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako5WaritukeKataHi = value
            End Set
        End Property

        ''' <summary>⑤割付工法</summary>
        Private _Kako5WaritukeKouhou As String
        Public Property Kako5WaritukeKouhou() As String
            Get
                Return _Kako5WaritukeKouhou
            End Get
            Set(ByVal value As String)
                _Kako5WaritukeKouhou = value
            End Set
        End Property

        ''' <summary>⑤ﾒｰｶｰ値部品費（円）</summary>
        Private _Kako5MakerBuhinHi As Nullable(Of Decimal)
        Public Property Kako5MakerBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako5MakerBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako5MakerBuhinHi = value
            End Set
        End Property

        ''' <summary>⑤ﾒｰｶｰ値型費（千円）</summary>
        Private _Kako5MakerKataHi As Nullable(Of Decimal)
        Public Property Kako5MakerKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako5MakerKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako5MakerKataHi = value
            End Set
        End Property

        ''' <summary>⑤ﾒｰｶｰ値工法</summary>
        Private _Kako5MakerKouhou As String
        Public Property Kako5MakerKouhou() As String
            Get
                Return _Kako5MakerKouhou
            End Get
            Set(ByVal value As String)
                _Kako5MakerKouhou = value
            End Set
        End Property

        ''' <summary>⑤審議値部品費（円）</summary>
        Private _Kako5ShingiBuhinHi As Nullable(Of Decimal)
        Public Property Kako5ShingiBuhinHi() As Nullable(Of Decimal)
            Get
                Return _Kako5ShingiBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako5ShingiBuhinHi = value
            End Set
        End Property

        ''' <summary>⑤審議値型費（千円）</summary>
        Private _Kako5ShingiKataHi As Nullable(Of Decimal)
        Public Property Kako5ShingiKataHi() As Nullable(Of Decimal)
            Get
                Return _Kako5ShingiKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako5ShingiKataHi = value
            End Set
        End Property

        ''' <summary>⑤審議値工法</summary>
        Private _Kako5ShingiKouhou As String
        Public Property Kako5ShingiKouhou() As String
            Get
                Return _Kako5ShingiKouhou
            End Get
            Set(ByVal value As String)
                _Kako5ShingiKouhou = value
            End Set
        End Property

        ''' <summary>⑤購入希望単価（円）</summary>
        Private _Kako5KounyuKibouTanka As Nullable(Of Decimal)
        Public Property Kako5KounyuKibouTanka() As Nullable(Of Decimal)
            Get
                Return _Kako5KounyuKibouTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako5KounyuKibouTanka = value
            End Set
        End Property

        ''' <summary>⑤購入単価（円）</summary>
        Private _Kako5KounyuTanka As Nullable(Of Decimal)
        Public Property Kako5KounyuTanka() As Nullable(Of Decimal)
            Get
                Return _Kako5KounyuTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako5KounyuTanka = value
            End Set
        End Property

        ''' <summary>⑤支給品（円）</summary>
        Private _Kako5ShikyuHin As Nullable(Of Decimal)
        Public Property Kako5ShikyuHin() As Nullable(Of Decimal)
            Get
                Return _Kako5ShikyuHin
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _Kako5ShikyuHin = value
            End Set
        End Property

        ''' <summary>⑤工事指令№</summary>
        Private _Kako5KoujiShireiNo As String
        Public Property Kako5KoujiShireiNo() As String
            Get
                Return _Kako5KoujiShireiNo
            End Get
            Set(ByVal value As String)
                _Kako5KoujiShireiNo = value
            End Set
        End Property

        ''' <summary>⑤イベント名称</summary>
        Private _Kako5EventName As String
        Public Property Kako5EventName() As String
            Get
                Return _Kako5EventName
            End Get
            Set(ByVal value As String)
                _Kako5EventName = value
            End Set
        End Property

        ''' <summary>⑤発注日</summary>
        Private _Kako5HachuBi As Nullable(Of Integer)
        Public Property Kako5HachuBi() As Nullable(Of Integer)
            Get
                Return _Kako5HachuBi
            End Get
            Set(ByVal value As Nullable(Of Integer))
                _Kako5HachuBi = value
            End Set
        End Property

        ''' <summary>⑤検収日</summary>
        Private _Kako5KenshuBi As Nullable(Of Integer)
        Public Property Kako5KenshuBi() As Nullable(Of Integer)
            Get
                Return _Kako5KenshuBi
            End Get
            Set(ByVal value As Nullable(Of Integer))
                _Kako5KenshuBi = value
            End Set
        End Property



        ''' <summary>割付予算_部品費合計(円)</summary>
        Private _YosanWaritukeBuhinHiTotal As Nullable(Of Decimal)
        Public Property YosanWaritukeBuhinHiTotal() As Nullable(Of Decimal)
            Get
                Return _YosanWaritukeBuhinHiTotal
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanWaritukeBuhinHiTotal = value
            End Set
        End Property

        ''' <summary>割付予算_型費(千円)</summary>
        Private _YosanWaritukeKataHi As Nullable(Of Decimal)
        Public Property YosanWaritukeKataHi() As Nullable(Of Decimal)
            Get
                Return _YosanWaritukeKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanWaritukeKataHi = value
            End Set
        End Property

        ''' <summary>購入希望_購入希望単価(円)</summary>
        Private _YosanKounyuKibouTanka As Nullable(Of Decimal)
        Public Property YosanKounyuKibouTanka() As Nullable(Of Decimal)
            Get
                Return _YosanKounyuKibouTanka
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanKounyuKibouTanka = value
            End Set
        End Property

        ''' <summary>購入希望_部品費(円)</summary>
        Private _YosanKounyuKibouBuhinHi As Nullable(Of Decimal)
        Public Property YosanKounyuKibouBuhinHi() As Nullable(Of Decimal)
            Get
                Return _YosanKounyuKibouBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanKounyuKibouBuhinHi = value
            End Set
        End Property

        ''' <summary>購入希望_部品費合計(円)</summary>
        Private _YosanKounyuKibouBuhinHiTotal As Nullable(Of Decimal)
        Public Property YosanKounyuKibouBuhinHiTotal() As Nullable(Of Decimal)
            Get
                Return _YosanKounyuKibouBuhinHiTotal
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanKounyuKibouBuhinHiTotal = value
            End Set
        End Property

        ''' <summary>購入希望_型費(円)</summary>
        Private _YosanKounyuKibouKataHi As Nullable(Of Decimal)
        Public Property YosanKounyuKibouKataHi() As Nullable(Of Decimal)
            Get
                Return _YosanKounyuKibouKataHi
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _YosanKounyuKibouKataHi = value
            End Set
        End Property

        ''' <summary>追加変更削除フラグ</summary>
        Private _AudFlag As String
        Public Property AudFlag() As String
            Get
                Return _AudFlag
            End Get
            Set(ByVal value As String)
                _AudFlag = value
            End Set
        End Property

        ''' <summary>追加変更削除日</summary>
        Private _AudBi As Nullable(Of Int32)
        Public Property AudBi() As Nullable(Of Int32)
            Get
                Return _AudBi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _AudBi = value
            End Set
        End Property

        ''' <summary>変化点</summary>
        Private _Henkaten As String
        Public Property Henkaten() As String
            Get
                Return _Henkaten
            End Get
            Set(ByVal value As String)
                _Henkaten = value
            End Set
        End Property

        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        Public Property CreatedUserId() As String
            Get
                Return _CreatedUserId
            End Get
            Set(ByVal value As String)
                _CreatedUserId = value
            End Set
        End Property

        ''' <summary>作成年月日</summary>
        Private _CreatedDate As String
        Public Property CreatedDate() As String
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As String)
                _CreatedDate = value
            End Set
        End Property

        ''' <summary>作成時分秒</summary>
        Private _CreatedTime As String
        Public Property CreatedTime() As String
            Get
                Return _CreatedTime
            End Get
            Set(ByVal value As String)
                _CreatedTime = value
            End Set
        End Property

        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        Public Property UpdatedUserId() As String
            Get
                Return _UpdatedUserId
            End Get
            Set(ByVal value As String)
                _UpdatedUserId = value
            End Set
        End Property

        ''' <summary>更新年月日</summary>
        Private _UpdatedDate As String
        Public Property UpdatedDate() As String
            Get
                Return _UpdatedDate
            End Get
            Set(ByVal value As String)
                _UpdatedDate = value
            End Set
        End Property

        ''' <summary>更新時分秒</summary>
        Private _UpdatedTime As String
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