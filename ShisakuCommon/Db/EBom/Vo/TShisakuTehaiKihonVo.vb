Namespace Db.EBom.Vo
    ''' <summary>試作手配帳情報（基本情報）</summary>
    Public Class TShisakuTehaiKihonVo
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
        ''' <summary>部品番号表示順</summary>
        Private _BuhinNoHyoujiJun As Nullable(Of Int32)
        ''' <summary>ソート順</summary>
        Private _SortJun As Nullable(Of Int32)
        ''' <summary>履歴</summary>
        Private _Rireki As String
        ''' <summary>行ID</summary>
        Private _GyouId As String
        ''' <summary>専用マーク</summary>
        Private _SenyouMark As String
        ''' <summary>レベル</summary>
        Private _Level As Nullable(Of Int32)
        ''' <summary>ユニット区分</summary>
        Private _UnitKbn As String
        ''' <summary>部品番号</summary>
        Private _BuhinNo As String
        ''' <summary>部品番号試作区分</summary>
        Private _BuhinNoKbn As String
        ''' <summary>部品番号改訂№</summary>
        Private _BuhinNoKaiteiNo As String
        ''' <summary>枝番</summary>
        Private _EdaBan As String
        ''' <summary>部品名称</summary>
        Private _BuhinName As String
        ''' <summary>集計コード</summary>
        Private _ShukeiCode As String
        ''' <summary>現調CKD区分</summary>
        Private _GencyoCkdKbn As String
        ''' <summary>手配記号</summary>
        Private _TehaiKigou As String
        ''' <summary>購担</summary>
        Private _Koutan As String
        ''' <summary>取引先コード</summary>
        Private _TorihikisakiCode As String
        ''' <summary>納場</summary>
        Private _Nouba As String
        ''' <summary>供給セクション</summary>
        Private _KyoukuSection As String
        ''' <summary>納入指示日</summary>
        Private _NounyuShijibi As Nullable(Of Int32)
        ''' <summary>合計員数</summary>
        Private _TotalInsuSuryo As Nullable(Of Int32)
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
        ''' <summary>ベース情報フラグ</summary>
        Private _BaseBuhinFlg As String
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
        Private _ShisakuBuhinnHi As Nullable(Of Int32)
        ''' <summary>試作型費(千円)</summary>
        Private _ShisakuKataHi As Nullable(Of Int32)
        ''' <summary>取引先名称</summary>
        Private _MakerCode As String
        ''' <summary>備考</summary>
        Private _Bikou As String
        ''' <summary>部品番号(親)</summary>
        Private _BuhinNoOya As String
        ''' <summary>部品番号試作区分(親)</summary>
        Private _BuhinNoKbnOya As String
        ''' <summary>エラー判定</summary>
        Private _ErrorKbn As String
        ''' <summary>追加変更削除フラグ</summary>
        Private _AudFlag As String
        ''' <summary>追加変更削除日</summary>
        Private _AudBi As Nullable(Of Int32)
        ''' <summary>結合№</summary>
        Private _KetugouNo As String
        ''' <summary>変化点</summary>
        Private _Henkaten As String
        ''' <summary>試作製品区分</summary>
        Private _ShisakuSeihinKbn As String
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
        ''' <summary>ソート順</summary>
        ''' <value>ソート順</value>
        ''' <returns>ソート順</returns>
        Public Property SortJun() As Nullable(Of Int32)
            Get
                Return _SortJun
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SortJun = value
            End Set
        End Property
        ''' <summary>履歴</summary>
        ''' <value>履歴</value>
        ''' <returns>履歴</returns>
        Public Property Rireki() As String
            Get
                Return _Rireki
            End Get
            Set(ByVal value As String)
                _Rireki = value
            End Set
        End Property
        ''' <summary>行ID</summary>
        ''' <value>行ID</value>
        ''' <returns>行ID</returns>
        Public Property GyouId() As String
            Get
                Return _GyouId
            End Get
            Set(ByVal value As String)
                _GyouId = value
            End Set
        End Property
        ''' <summary>専用マーク</summary>
        ''' <value>専用マーク</value>
        ''' <returns>専用マーク</returns>
        Public Property SenyouMark() As String
            Get
                Return _SenyouMark
            End Get
            Set(ByVal value As String)
                _SenyouMark = value
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
        ''' <summary>部品番号改訂№</summary>
        ''' <value>部品番号改訂№</value>
        ''' <returns>部品番号改訂№</returns>
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
        ''' <summary>手配記号</summary>
        ''' <value>手配記号</value>
        ''' <returns>手配記号</returns>
        Public Property TehaiKigou() As String
            Get
                Return _TehaiKigou
            End Get
            Set(ByVal value As String)
                _TehaiKigou = value
            End Set
        End Property
        ''' <summary>購担</summary>
        ''' <value>購担</value>
        ''' <returns>購担</returns>
        Public Property Koutan() As String
            Get
                Return _Koutan
            End Get
            Set(ByVal value As String)
                _Koutan = value
            End Set
        End Property
        ''' <summary>取引先コード</summary>
        ''' <value>取引先コード</value>
        ''' <returns>取引先コード</returns>
        Public Property TorihikisakiCode() As String
            Get
                Return _TorihikisakiCode
            End Get
            Set(ByVal value As String)
                _TorihikisakiCode = value
            End Set
        End Property
        ''' <summary>納場</summary>
        ''' <value>納場</value>
        ''' <returns>納場</returns>
        Public Property Nouba() As String
            Get
                Return _Nouba
            End Get
            Set(ByVal value As String)
                _Nouba = value
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
        ''' <summary>納入指示日</summary>
        ''' <value>納入指示日</value>
        ''' <returns>納入指示日</returns>
        Public Property NounyuShijibi() As Nullable(Of Int32)
            Get
                Return _NounyuShijibi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _NounyuShijibi = value
            End Set
        End Property
        ''' <summary>合計員数</summary>
        ''' <value>合計員数</value>
        ''' <returns>合計員数</returns>
        Public Property TotalInsuSuryo() As Nullable(Of Int32)
            Get
                Return _TotalInsuSuryo
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _TotalInsuSuryo = value
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
        ''' <summary>最終織込設変情報・日付</summary>
        ''' <value>最終織込設変情報・日付</value>
        ''' <returns>最終織込設変情報・日付</returns>
        Public Property SaisyuSetsuhenDate() As Nullable(Of Int32)
            Get
                Return _SaisyuSetsuhenDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _SaisyuSetsuhenDate = value
            End Set
        End Property
        ''' <summary>最終織込設変情報・改訂№</summary>
        ''' <value>最終織込設変情報・改訂№</value>
        ''' <returns>最終織込設変情報・改訂№</returns>
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
        ''' <summary>ベース情報フラグ</summary>
        ''' <value>ベース情報フラグ</value>
        ''' <returns>ベース情報フラグ</returns>
        Public Property BaseBuhinFlg() As String
            Get
                Return _BaseBuhinFlg
            End Get
            Set(ByVal value As String)
                _BaseBuhinFlg = value
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
        Public Property ShisakuBuhinnHi() As Nullable(Of Int32)
            Get
                Return _ShisakuBuhinnHi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShisakuBuhinnHi = value
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
        ''' <summary>取引先名称</summary>
        ''' <value>取引先名称</value>
        ''' <returns>取引先名称</returns>
        Public Property MakerCode() As String
            Get
                Return _MakerCode
            End Get
            Set(ByVal value As String)
                _MakerCode = value
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
        ''' <summary>部品番号試作区分(親)</summary>
        ''' <value>部品番号試作区分(親)</value>
        ''' <returns>部品番号試作区分(親)</returns>
        Public Property BuhinNoKbnOya() As String
            Get
                Return _BuhinNoKbnOya
            End Get
            Set(ByVal value As String)
                _BuhinNoKbnOya = value
            End Set
        End Property
        ''' <summary>エラー判定</summary>
        ''' <value>エラー判定</value>
        ''' <returns>エラー判定</returns>
        Public Property ErrorKbn() As String
            Get
                Return _ErrorKbn
            End Get
            Set(ByVal value As String)
                _ErrorKbn = value
            End Set
        End Property
        ''' <summary>追加変更削除フラグ</summary>
        ''' <value>追加変更削除フラグ</value>
        ''' <returns>追加変更削除フラグ</returns>
        Public Property AudFlag() As String
            Get
                Return _AudFlag
            End Get
            Set(ByVal value As String)
                _AudFlag = value
            End Set
        End Property
        ''' <summary>追加変更削除日</summary>
        ''' <value>追加変更削除日</value>
        ''' <returns>追加変更削除日</returns>
        Public Property AudBi() As Nullable(Of Int32)
            Get
                Return _AudBi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _AudBi = value
            End Set
        End Property
        ''' <summary>結合№</summary>
        ''' <value>結合№</value>
        ''' <returns>結合№</returns>
        Public Property KetugouNo() As String
            Get
                Return _KetugouNo
            End Get
            Set(ByVal value As String)
                _KetugouNo = value
            End Set
        End Property
        ''' <summary>変化点</summary>
        ''' <value>変化点</value>
        ''' <returns>変化点</returns>
        Public Property Henkaten() As String
            Get
                Return _Henkaten
            End Get
            Set(ByVal value As String)
                _Henkaten = value
            End Set
        End Property
        ''' <summary>試作製品区分</summary>
        ''' <value>試作製品区分</value>
        ''' <returns>試作製品区分</returns>
        Public Property ShisakuSeihinKbn() As String
            Get
                Return _ShisakuSeihinKbn
            End Get
            Set(ByVal value As String)
                _ShisakuSeihinKbn = value
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