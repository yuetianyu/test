Namespace Db.EBom.Vo
    ''' <summary>試作部品編集情報（改訂抽出）</summary>
    Public Class TShisakuBuhinEditKaiteiVo
        ''' <summary>試作イベントコード</summary>
        Private _ShisakuEventCode As String
        ''' <summary>試作リストコード</summary>
        Private _ShisakuListCode As String
        ''' <summary>試作リストコード改訂№</summary>
        Private _ShisakuListCodeKaiteiNo As String
        ''' <summary>試作部課コード</summary>
        Private _ShisakuBukaCode As String
        ''' <summary>試作ブロック№</summary>
        Private _ShisakuBlockNo As String
        ''' <summary>試作ブロック№改訂№</summary>
        Private _ShisakuBlockNoKaiteiNo As String
        ''' <summary>前回試作ブロック№改訂№</summary>
        Private _ZenkaiShisakuBlockNoKaiteiNo As String
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
        '2012/01/23 供給セクション追加
        ''' <summary>供給セクション</summary>
        Private _KyoukuSection As String
        ''' <summary>取引先コード</summary>
        Private _MakerCode As String
        ''' <summary>取引先名称</summary>
        Private _MakerName As String
        ''' <summary>部品番号</summary>
        Private _BuhinNo As String
        ''' <summary>部品番号試作区分</summary>
        Private _BuhinNoKbn As String
        ''' <summary>部品番号改訂No.</summary>
        Private _BuhinNoKaiteiNo As String
        ''' <summary>枝番</summary>
        Private _EdaBan As String
        ''' <summary>部品名称</summary>
        Private _BuhinName As String
        ''' <summary>再使用不可</summary>
        Private _Saishiyoufuka As String
        ''' <summary>号車発注展開_フラグ</summary>
        Private _GousyaHachuTenkaiFlg As String
        ''' <summary>号車発注展開_ユニット区分</summary>
        Private _GousyaHachuTenkaiUnitKbn As String
        ''' <summary>出図予定日</summary>
        Private _ShutuzuYoteiDate As Nullable(Of Int32)
        ''' <summary>出図実績_日付</summary>
        Private _ShutuzuJisekiDate As Nullable(Of Int32)
        ''' <summary>出図実績_改訂№</summary>
        Private _ShutuzuJisekiKaiteiNo As String
        ''' <summary>出図実績_設通№</summary>
        Private _ShutuzuJisekiStsrDhstba As String
        ''' <summary>最終織込設変情報_日付</summary>
        Private _SaisyuSetsuhenDate As Nullable(Of Int32)
        ''' <summary>最終織込設変情報_改訂№</summary>
        Private _SaisyuSetsuhenKaiteiNo As String
        ''' <summary>最終織込設変情報_設通№</summary>
        Private _StsrDhstba As String
        ''' <summary>材質・規格１</summary>
        Private _ZaishituKikaku1 As String
        ''' <summary>材質・規格２</summary>
        Private _ZaishituKikaku2 As String
        ''' <summary>材質・規格３</summary>
        Private _ZaishituKikaku3 As String
        ''' <summary>材質・メッキ</summary>
        Private _ZaishituMekki As String
        ''' <summary>作り方・製作方法</summary>
        Private _TsukurikataSeisaku As String
        ''' <summary>作り方・型仕様1</summary>
        Private _TsukurikataKatashiyou1 As String
        ''' <summary>作り方・型仕様2</summary>
        Private _TsukurikataKatashiyou2 As String
        ''' <summary>作り方・型仕様3</summary>
        Private _TsukurikataKatashiyou3 As String
        ''' <summary>作り方・治具</summary>
        Private _TsukurikataTigu As String
        ''' <summary>作り方・納入見通し</summary>
        Private _TsukurikataNounyu As Int32?
        ''' <summary>作り方・部品製作規模・概要</summary>
        Private _TsukurikataKibo As String
        ''' <summary>板厚</summary>
        Private _ShisakuBankoSuryo As String
        ''' <summary>板厚・ｕ</summary>
        Private _ShisakuBankoSuryoU As String
        ''' <summary>材料情報・製品長</summary>
        Private _MaterialInfoLength As Nullable(Of Int32)
        ''' <summary>材料情報・製品幅</summary>
        Private _MaterialInfoWidth As Nullable(Of Int32)
        ''' <summary>材料寸法_X(mm)</summary>
        Private _ZairyoSunpoX As Nullable(Of Decimal)
        ''' <summary>材料寸法_Y(mm)</summary>
        Private _ZairyoSunpoY As Nullable(Of Decimal)
        ''' <summary>材料寸法_Z(mm)</summary>
        Private _ZairyoSunpoZ As Nullable(Of Decimal)
        ''' <summary>材料寸法_X+Y(mm)</summary>
        Private _ZairyoSunpoXy As Nullable(Of Decimal)
        ''' <summary>材料寸法_X+Z(mm)</summary>
        Private _ZairyoSunpoXz As Nullable(Of Decimal)
        ''' <summary>材料寸法_Y+Z(mm)</summary>
        Private _ZairyoSunpoYz As Nullable(Of Decimal)
        ''' <summary>材料情報・発注対象</summary>
        Private _MaterialInfoOrderTarget As String
        ''' <summary>材料情報・発注対象最終更新年月日</summary>
        Private _MaterialInfoOrderTargetDate As String
        ''' <summary>材料情報・発注済</summary>
        Private _MaterialInfoOrderChk As String
        ''' <summary>材料情報・発注済最終更新年月日</summary>
        Private _MaterialInfoOrderChkDate As String
        ''' <summary>データ項目・改訂№</summary>
        Private _DataItemKaiteiNo As String
        ''' <summary>データ項目・エリア名</summary>
        Private _DataItemAreaName As String
        ''' <summary>データ項目・セット名</summary>
        Private _DataItemSetName As String
        ''' <summary>データ項目・改訂情報</summary>
        Private _DataItemKaiteiInfo As String
        ''' <summary>データ項目・データ支給チェック欄</summary>
        Private _DataItemDataProvision As String
        ''' <summary>データ項目・データ支給チェック欄最終更新年月日</summary>
        Private _DataItemDataProvisionDate As String
        ''' <summary>試作部品費(円)</summary>
        Private _ShisakuBuhinHi As Nullable(Of Int32)
        ''' <summary>試作型費(千円)</summary>
        Private _ShisakuKataHi As Nullable(Of Int32)
        ''' <summary>備考</summary>
        Private _Bikou As String
        ''' <summary>NOTE</summary>
        Private _BuhinNote As String
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
        ''' <summary>試作リストコード</summary>
        ''' <value>試作リストコード</value>
        ''' <returns>試作リストコード</returns>
        Public Property ShisakuListCode() As String
            Get
                Return _ShisakuListCode
            End Get
            Set(ByVal value As String)
                _ShisakuListCode = value
            End Set
        End Property
        ''' <summary>試作リストコード改訂№</summary>
        ''' <value>試作リストコード改訂№</value>
        ''' <returns>試作リストコード改訂№</returns>
        Public Property ShisakuListCodeKaiteiNo() As String
            Get
                Return _ShisakuListCodeKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShisakuListCodeKaiteiNo = value
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
        ''' <summary>前回試作ブロック№改訂№</summary>
        ''' <value>前回試作ブロック№改訂№</value>
        ''' <returns>前回試作ブロック№改訂№</returns>
        Public Property ZenkaiShisakuBlockNoKaiteiNo() As String
            Get
                Return _ZenkaiShisakuBlockNoKaiteiNo
            End Get
            Set(ByVal value As String)
                _ZenkaiShisakuBlockNoKaiteiNo = value
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
        ''' <summary>部品番号試作区分</summary>
        ''' <value>部品番号試作区分</value>
        ''' <returns>部品番号試作区分</returns>
        Public Property BuhinNoKbn() As String
            Get
                Return _BuhinNoKbn
            End Get
            Set(ByVal value As String)
                _BuhinNoKbn = value
            End Set
        End Property
        ''' <summary>部品番号改訂No.</summary>
        ''' <value>部品番号改訂No.</value>
        ''' <returns>部品番号改訂No.</returns>
        Public Property BuhinNoKaiteiNo() As String
            Get
                Return _BuhinNoKaiteiNo
            End Get
            Set(ByVal value As String)
                _BuhinNoKaiteiNo = value
            End Set
        End Property
        ''' <summary>枝番</summary>
        ''' <value>枝番</value>
        ''' <returns>枝番</returns>
        Public Property EdaBan() As String
            Get
                Return _EdaBan
            End Get
            Set(ByVal value As String)
                _EdaBan = value
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
        ''' <summary>再使用不可</summary>
        ''' <value>再使用不可</value>
        ''' <returns>再使用不可</returns>
        Public Property Saishiyoufuka() As String
            Get
                Return _Saishiyoufuka
            End Get
            Set(ByVal value As String)
                _Saishiyoufuka = value
            End Set
        End Property
        ''' <summary>号車発注展開_フラグ</summary>
        ''' <value>号車発注展開_フラグ</value>
        ''' <returns>号車発注展開_フラグ</returns>
        Public Property GousyaHachuTenkaiFlg() As String
            Get
                Return _GousyaHachuTenkaiFlg
            End Get
            Set(ByVal value As String)
                _GousyaHachuTenkaiFlg = value
            End Set
        End Property
        ''' <summary>号車発注展開_ユニット区分</summary>
        ''' <value>号車発注展開_ユニット区分</value>
        ''' <returns>号車発注展開_ユニット区分</returns>
        Public Property GousyaHachuTenkaiUnitKbn() As String
            Get
                Return _GousyaHachuTenkaiUnitKbn
            End Get
            Set(ByVal value As String)
                _GousyaHachuTenkaiUnitKbn = value
            End Set
        End Property
        ''' <summary>出図予定日</summary>
        ''' <value>出図予定日</value>
        ''' <returns>出図予定日</returns>
        Public Property ShutuzuYoteiDate() As Nullable(Of Int32)
            Get
                Return _ShutuzuYoteiDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShutuzuYoteiDate = value
            End Set
        End Property
        ''' <summary>出図実績_日付</summary>
        ''' <value>出図実績_日付</value>
        ''' <returns>出図実績_日付</returns>
        Public Property ShutuzuJisekiDate() As Nullable(Of Int32)
            Get
                Return _ShutuzuJisekiDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShutuzuJisekiDate = value
            End Set
        End Property
        ''' <summary>出図実績_改訂№</summary>
        ''' <value>出図実績_改訂№</value>
        ''' <returns>出図実績_改訂№</returns>
        Public Property ShutuzuJisekiKaiteiNo() As String
            Get
                Return _ShutuzuJisekiKaiteiNo
            End Get
            Set(ByVal value As String)
                _ShutuzuJisekiKaiteiNo = value
            End Set
        End Property
        ''' <summary>出図実績_設通№</summary>
        ''' <value>出図実績_設通№</value>
        ''' <returns>出図実績_設通№</returns>
        Public Property ShutuzuJisekiStsrDhstba() As String
            Get
                Return _ShutuzuJisekiStsrDhstba
            End Get
            Set(ByVal value As String)
                _ShutuzuJisekiStsrDhstba = value
            End Set
        End Property
        ''' <summary>最終織込設変情報_日付</summary>
        ''' <value>最終織込設変情報_日付</value>
        ''' <returns>最終織込設変情報_日付</returns>
        Public Property SaisyuSetsuhenDate() As Nullable(Of Int32)
            Get
                Return _SaisyuSetsuhenDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaisyuSetsuhenDate = value
            End Set
        End Property
        ''' <summary>最終織込設変情報_改訂№</summary>
        ''' <value>最終織込設変情報_改訂№</value>
        ''' <returns>最終織込設変情報_改訂№</returns>
        Public Property SaisyuSetsuhenKaiteiNo() As String
            Get
                Return _SaisyuSetsuhenKaiteiNo
            End Get
            Set(ByVal value As String)
                _SaisyuSetsuhenKaiteiNo = value
            End Set
        End Property
        ''' <summary>最終織込設変情報_設通№</summary>
        ''' <value>最終織込設変情報_設通№</value>
        ''' <returns>最終織込設変情報_設通№</returns>
        Public Property StsrDhstba() As String
            Get
                Return _StsrDhstba
            End Get
            Set(ByVal value As String)
                _StsrDhstba = value
            End Set
        End Property
        ''' <summary>材質・規格１</summary>
        ''' <value>材質・規格１</value>
        ''' <returns>材質・規格１</returns>
        Public Property ZaishituKikaku1() As String
            Get
                Return _ZaishituKikaku1
            End Get
            Set(ByVal value As String)
                _ZaishituKikaku1 = value
            End Set
        End Property
        ''' <summary>材質・規格２</summary>
        ''' <value>材質・規格２</value>
        ''' <returns>材質・規格２</returns>
        Public Property ZaishituKikaku2() As String
            Get
                Return _ZaishituKikaku2
            End Get
            Set(ByVal value As String)
                _ZaishituKikaku2 = value
            End Set
        End Property
        ''' <summary>材質・規格３</summary>
        ''' <value>材質・規格３</value>
        ''' <returns>材質・規格３</returns>
        Public Property ZaishituKikaku3() As String
            Get
                Return _ZaishituKikaku3
            End Get
            Set(ByVal value As String)
                _ZaishituKikaku3 = value
            End Set
        End Property
        ''' <summary>材質・メッキ</summary>
        ''' <value>材質・メッキ</value>
        ''' <returns>材質・メッキ</returns>
        Public Property ZaishituMekki() As String
            Get
                Return _ZaishituMekki
            End Get
            Set(ByVal value As String)
                _ZaishituMekki = value
            End Set
        End Property
        ''' <summary>作り方・製作方法</summary>
        ''' <value>作り方・製作方法</value>
        ''' <returns>作り方・製作方法</returns>
        Public Property TsukurikataSeisaku() As String
            Get
                Return _TsukurikataSeisaku
            End Get
            Set(ByVal value As String)
                _TsukurikataSeisaku = value
            End Set
        End Property
        ''' <summary>作り方・型仕様1</summary>
        ''' <value>作り方・型仕様1</value>
        ''' <returns>作り方・型仕様1</returns>
        Public Property TsukurikataKatashiyou1() As String
            Get
                Return _TsukurikataKatashiyou1
            End Get
            Set(ByVal value As String)
                _TsukurikataKatashiyou1 = value
            End Set
        End Property
        ''' <summary>作り方・型仕様2</summary>
        ''' <value>作り方・型仕様2</value>
        ''' <returns>作り方・型仕様2</returns>
        Public Property TsukurikataKatashiyou2() As String
            Get
                Return _TsukurikataKatashiyou2
            End Get
            Set(ByVal value As String)
                _TsukurikataKatashiyou2 = value
            End Set
        End Property
        ''' <summary>作り方・型仕様3</summary>
        ''' <value>作り方・型仕様3</value>
        ''' <returns>作り方・型仕様3</returns>
        Public Property TsukurikataKatashiyou3() As String
            Get
                Return _TsukurikataKatashiyou3
            End Get
            Set(ByVal value As String)
                _TsukurikataKatashiyou3 = value
            End Set
        End Property
        ''' <summary>作り方・治具</summary>
        ''' <value>作り方・治具</value>
        ''' <returns>作り方・治具</returns>
        Public Property TsukurikataTigu() As String
            Get
                Return _TsukurikataTigu
            End Get
            Set(ByVal value As String)
                _TsukurikataTigu = value
            End Set
        End Property
        ''' <summary>作り方・納入見通し</summary>
        ''' <value>作り方・納入見通し</value>
        ''' <returns>作り方・納入見通し</returns>
        Public Property TsukurikataNounyu() As Int32?
            Get
                Return _TsukurikataNounyu
            End Get
            Set(ByVal value As Int32?)
                _TsukurikataNounyu = value
            End Set
        End Property
        ''' <summary>作り方・部品製作規模・概要</summary>
        ''' <value>作り方・部品製作規模・概要</value>
        ''' <returns>作り方・部品製作規模・概要</returns>
        Public Property TsukurikataKibo() As String
            Get
                Return _TsukurikataKibo
            End Get
            Set(ByVal value As String)
                _TsukurikataKibo = value
            End Set
        End Property
        ''' <summary>板厚</summary>
        ''' <value>板厚</value>
        ''' <returns>板厚</returns>
        Public Property ShisakuBankoSuryo() As String
            Get
                Return _ShisakuBankoSuryo
            End Get
            Set(ByVal value As String)
                _ShisakuBankoSuryo = value
            End Set
        End Property
        ''' <summary>板厚・ｕ</summary>
        ''' <value>板厚・ｕ</value>
        ''' <returns>板厚・ｕ</returns>
        Public Property ShisakuBankoSuryoU() As String
            Get
                Return _ShisakuBankoSuryoU
            End Get
            Set(ByVal value As String)
                _ShisakuBankoSuryoU = value
            End Set
        End Property
        ''' <summary>材料情報・製品長</summary>
        ''' <value>材料情報・製品長</value>
        ''' <returns>材料情報・製品長</returns>
        Public Property MaterialInfoLength() As Nullable(Of Int32)
            Get
                Return _MaterialInfoLength
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _MaterialInfoLength = value
            End Set
        End Property
        ''' <summary>材料情報・製品幅</summary>
        ''' <value>材料情報・製品幅</value>
        ''' <returns>材料情報・製品幅</returns>
        Public Property MaterialInfoWidth() As Nullable(Of Int32)
            Get
                Return _MaterialInfoWidth
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _MaterialInfoWidth = value
            End Set
        End Property
        ''' <summary>材料寸法_X(mm)</summary>
        ''' <value>材料寸法_X(mm)</value>
        ''' <returns>材料寸法_X(mm)</returns>
        Public Property ZairyoSunpoX() As Nullable(Of Decimal)
            Get
                Return _ZairyoSunpoX
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _ZairyoSunpoX = value
            End Set
        End Property
        ''' <summary>材料寸法_Y(mm)</summary>
        ''' <value>材料寸法_Y(mm)</value>
        ''' <returns>材料寸法_Y(mm)</returns>
        Public Property ZairyoSunpoY() As Nullable(Of Decimal)
            Get
                Return _ZairyoSunpoY
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _ZairyoSunpoY = value
            End Set
        End Property
        ''' <summary>材料寸法_Z(mm)</summary>
        ''' <value>材料寸法_Z(mm)</value>
        ''' <returns>材料寸法_Z(mm)</returns>
        Public Property ZairyoSunpoZ() As Nullable(Of Decimal)
            Get
                Return _ZairyoSunpoZ
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _ZairyoSunpoZ = value
            End Set
        End Property
        ''' <summary>材料寸法_X+Y(mm)</summary>
        ''' <value>材料寸法_X+Y(mm)</value>
        ''' <returns>材料寸法_X+Y(mm)</returns>
        Public Property ZairyoSunpoXy() As Nullable(Of Decimal)
            Get
                Return _ZairyoSunpoXy
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _ZairyoSunpoXy = value
            End Set
        End Property
        ''' <summary>材料寸法_X+Z(mm)</summary>
        ''' <value>材料寸法_X+Z(mm)</value>
        ''' <returns>材料寸法_X+Z(mm)</returns>
        Public Property ZairyoSunpoXz() As Nullable(Of Decimal)
            Get
                Return _ZairyoSunpoXz
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _ZairyoSunpoXz = value
            End Set
        End Property
        ''' <summary>材料寸法_Y+Z(mm)</summary>
        ''' <value>材料寸法_Y+Z(mm)</value>
        ''' <returns>材料寸法_Y+Z(mm)</returns>
        Public Property ZairyoSunpoYz() As Nullable(Of Decimal)
            Get
                Return _ZairyoSunpoYz
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _ZairyoSunpoYz = value
            End Set
        End Property
        ''' <summary>材料情報・発注対象</summary>
        ''' <value>材料情報・発注対象</value>
        ''' <returns>材料情報・発注対象</returns>
        Public Property MaterialInfoOrderTarget() As String
            Get
                Return _MaterialInfoOrderTarget
            End Get
            Set(ByVal value As String)
                _MaterialInfoOrderTarget = value
            End Set
        End Property
        ''' <summary>材料情報・発注対象最終更新年月日</summary>
        ''' <value>材料情報・発注対象最終更新年月日</value>
        ''' <returns>材料情報・発注対象最終更新年月日</returns>
        Public Property MaterialInfoOrderTargetDate() As String
            Get
                Return _MaterialInfoOrderTargetDate
            End Get
            Set(ByVal value As String)
                _MaterialInfoOrderTargetDate = value
            End Set
        End Property
        ''' <summary>材料情報・発注済</summary>
        ''' <value>材料情報・発注済</value>
        ''' <returns>材料情報・発注済</returns>
        Public Property MaterialInfoOrderChk() As String
            Get
                Return _MaterialInfoOrderChk
            End Get
            Set(ByVal value As String)
                _MaterialInfoOrderChk = value
            End Set
        End Property
        ''' <summary>材料情報・発注済最終更新年月日</summary>
        ''' <value>材料情報・発注済最終更新年月日</value>
        ''' <returns>材料情報・発注済最終更新年月日</returns>
        Public Property MaterialInfoOrderChkDate() As String
            Get
                Return _MaterialInfoOrderChkDate
            End Get
            Set(ByVal value As String)
                _MaterialInfoOrderChkDate = value
            End Set
        End Property
        ''' <summary>データ項目・改訂№</summary>
        ''' <value>データ項目・改訂№</value>
        ''' <returns>データ項目・改訂№</returns>
        Public Property DataItemKaiteiNo() As String
            Get
                Return _DataItemKaiteiNo
            End Get
            Set(ByVal value As String)
                _DataItemKaiteiNo = value
            End Set
        End Property
        ''' <summary>データ項目・エリア名</summary>
        ''' <value>データ項目・エリア名</value>
        ''' <returns>データ項目・エリア名</returns>
        Public Property DataItemAreaName() As String
            Get
                Return _DataItemAreaName
            End Get
            Set(ByVal value As String)
                _DataItemAreaName = value
            End Set
        End Property
        ''' <summary>データ項目・セット名</summary>
        ''' <value>データ項目・セット名</value>
        ''' <returns>データ項目・セット名</returns>
        Public Property DataItemSetName() As String
            Get
                Return _DataItemSetName
            End Get
            Set(ByVal value As String)
                _DataItemSetName = value
            End Set
        End Property
        ''' <summary>データ項目・改訂情報</summary>
        ''' <value>データ項目・改訂情報</value>
        ''' <returns>データ項目・改訂情報</returns>
        Public Property DataItemKaiteiInfo() As String
            Get
                Return _DataItemKaiteiInfo
            End Get
            Set(ByVal value As String)
                _DataItemKaiteiInfo = value
            End Set
        End Property
        ''' <summary>データ項目・データ支給チェック欄</summary>
        ''' <value>データ項目・データ支給チェック欄</value>
        ''' <returns>データ項目・データ支給チェック欄</returns>
        Public Property DataItemDataProvision() As String
            Get
                Return _DataItemDataProvision
            End Get
            Set(ByVal value As String)
                _DataItemDataProvision = value
            End Set
        End Property
        ''' <summary>データ項目・データ支給チェック欄最終更新年月日</summary>
        ''' <value>データ項目・データ支給チェック欄最終更新年月日</value>
        ''' <returns>データ項目・データ支給チェック欄最終更新年月日</returns>
        Public Property DataItemDataProvisionDate() As String
            Get
                Return _DataItemDataProvisionDate
            End Get
            Set(ByVal value As String)
                _DataItemDataProvisionDate = value
            End Set
        End Property
        ''' <summary>試作部品費(円)</summary>
        ''' <value>試作部品費(円)</value>
        ''' <returns>試作部品費(円)</returns>
        Public Property ShisakuBuhinHi() As Nullable(Of Int32)
            Get
                Return _ShisakuBuhinHi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuBuhinHi = value
            End Set
        End Property
        ''' <summary>試作型費(千円)</summary>
        ''' <value>試作型費(千円)</value>
        ''' <returns>試作型費(千円)</returns>
        Public Property ShisakuKataHi() As Nullable(Of Int32)
            Get
                Return _ShisakuKataHi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuKataHi = value
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
        ''' <summary>NOTE</summary>
        ''' <value>NOTE</value>
        ''' <returns>NOTE</returns>
        Public Property BuhinNote() As String
            Get
                Return _BuhinNote
            End Get
            Set(ByVal value As String)
                _BuhinNote = value
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