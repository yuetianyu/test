Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作部品情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuBuhinVo
        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 試作部課コード
        Private _ShisakuBukaCode As String
        '' 試作ブロック№
        Private _ShisakuBlockNo As String
        '' 試作ブロック№改訂№
        Private _ShisakuBlockNoKaiteiNo As String
        '' 試作号車
        Private _ShisakuGousya As String
        '' 部品番号
        Private _BuhinNo As String
        '' 部品番号区分
        Private _BuhinNoKbn As String
        '' 部品番号改訂No.
        Private _BuhinNoKaiteiNo As String
        '' 枝番
        Private _EdaBan As String
        '' 設計社員番号
        Private _SekkeiShainNo As String
        '' メーカーコード
        Private _MakerCode As String
        '' SIAメーカーコード
        Private _SiaMakerCode As String
        '' 担当コード
        Private _TantoCode As String
        '' 責任部課コード
        Private _SekininBukaCode As String
        '' 責任サイト区分
        Private _SekininSiteKbn As String
        '' 図面番号
        Private _ZumenNo As String
        '' 図面改訂No.
        Private _ZumenKaiteiNo As String
        ''承認サイン
        Private _ShoninSign As String
        ''承認日付
        Private _ShoninDate As Nullable(Of Int32)
        '' 品目番号
        Private _HinmokuNo As String
        '' 部品名称
        Private _BuhinName As String
        '' 部品カタカナ名称
        Private _BuhinKanaName As String
        '' 補助名称
        Private _HojoName As String
        '' 共用係数コード
        Private _KeisuCode As String
        '' 共用車種
        Private _KyoyoModel As String
        '' 納入単位
        Private _NonyuTani As String
        '' 部品種類
        Private _BuhinKind As String
        '' 内製区分
        Private _NaiseiKbn As String
        '' LOWレベル
        Private _LowLevel As Nullable(of Int32)
        '' 板厚入力済み区分
        Private _BankoSuryoInput As String
        '' 部品質量(現在値)
        Private _BuhinShitsuryo As Nullable(of Int32)
        '' 金属質量(現在値)入力済み区分
        Private _KinzokuShitsuryoInput As String
        '' 金属質量(現在値)
        Private _KinzokuShitsuryo As Nullable(of Int32)
        '' 部品費(現在値)
        Private _BuhinhiKingaku As Nullable(of Decimal)
        '' 部品費取得日識別
        Private _BuhinDateId As String
        '' SIA部品費(現在値)
        Private _SiaBuhinhi As Nullable(of Decimal)
        '' SIA部品費取得日識別
        Private _SiaBuhinDateId As String
        '' 支給展伸材費
        Private _Tenshinzai As Nullable(of Decimal)
        '' SIA支給展伸材費
        Private _SiaTenshinzai As Nullable(of Decimal)
        '' 型費(現在値)
        Private _KatahiKingaku As Nullable(of Int32)
        '' SIA型費(現在値)
        Private _SiaKatahiKingaku As Nullable(of Int32)
        '' パレット費
        Private _Pallet As Nullable(of Decimal)
        '' SIAパレット費
        Private _SiaPallet As Nullable(of Decimal)
        '' 開発費
        Private _Kaihatsuhi As Nullable(of Decimal)
        '' SIA開発費
        Private _SiaKaihatsuhi As Nullable(of Decimal)
        '' 板厚
        Private _BankoSuryo As String
        '' 国内集計コード
        Private _ShukeiCode As String
        '' 海外SIA集計コード
        Private _SiaShukeiCode As String
        '' 現調CKD区分
        Private _GencyoCkdKbn As String
        '' 集計用部課コード
        Private _ShukeiBukaCode As String
        '' 図面オーバー
        Private _ZumenOver As String
        '' 図面カラム
        Private _ZumenColumn As String
        '' 適用区分
        Private _TekiyoKbn As String
        '' 注記
        Private _ChukiKijutsu As String
        '' 内外装区分
        Private _NaigaisoKbn As String
        '' 材料・材質
        Private _ZairyoKijutsu As String
        '' 製法
        Private _SeihoKijutu As String
        '' 設計メモ
        Private _DsgnMemo As String
        '' ファイナル区分
        Private _FinalKbn As String
        '' 重要保安区分
        Private _JuyoHoanKbn As String
        '' 重要保安部品コード
        Private _JuyoHoanCode As String
        '' 補用品処置コード
        Private _HoyohinCode As String
        '' リサイクルマーク
        Private _RecycleMark As String
        '' 法規制表示コード
        Private _HokiseiCode As String
        '' 採用年月日
        Private _SaiyoDate As Nullable(of Int32)
        '' 廃止年月日
        Private _HaisiDate As Nullable(of Int32)
        '' シリーズ
        Private _SiriesCode As String
        '' 表面処理
        Private _HyomenShori As String
        '' 生産・調達区分
        Private _SeisanKbn As String
        '' 部品状態区分
        Private _BuhinStatusKbn As String
        '' 参考表示コード
        Private _SankoHyojiCode As String
        '' 出図予定年月日
        Private _ShutuzuYoteiDate As Nullable(of Int32)
        '' ステータス
        Private _Status As String
        '' 更新元ツール区分
        Private _UpdateKbn As Nullable(of Int32)
        '' 部品アイテム名称
        Private _BuhinItemName As String
        '' 変動種別
        Private _HendoShubetsu As String
        '' 梱包輸送費
        Private _KonpoYusohi As Nullable(of Decimal)
        '' コストベンチマーク部品費
        Private _BenchiBuhinhi As Nullable(of Decimal)
        '' SIAコストベンチマーク部品費
        Private _SiaBenchiBuhinhi As Nullable(of Decimal)
        '' コストベンチマーク型費
        Private _BenchiKatahi As Nullable(of Decimal)
        '' SIAコストベンチマーク型費
        Private _SiaBenchiKatahi As Nullable(of Decimal)
        '' 国内コスト区分
        Private _KokunaiCostKbn As String
        '' 海外コスト区分
        Private _KaigaiCostKbn As String
        '' 再使用不可
        Private _Saishiyoufuka As String
        '' 材質・規格１
        Private _ZaishituKikaku1 As String
        '' 材質・規格２
        Private _ZaishituKikaku2 As String
        '' 材質・規格３
        Private _ZaishituKikaku3 As String
        '' 材質・メッキ
        Private _ZaishituMekki As String
        '' 板厚・板厚       
        Private _ShisakuBankoSuryo As String
        '' 板厚・ｕ
        Private _ShisakuBankoSuryoU As String
        '' 試作部品費（円）
        Private _ShisakuBuhinnHi As Nullable(of Int32)
        '' 試作型費（千円）
        Private _ShisakuKataHi As Nullable(of Int32)
        '' 備考
        Private _Bikou As String
        '' 編集登録日
        Private _HensyuTourokubi As Nullable(of Int32)
        '' 編集登録時間
        Private _HensyuTourokujikan As Nullable(of Int32)
        '' 改訂判断フラグ
        Private _KaiteiHandanFlg As String
        '' 試作リストコード
        Private _ShisakuListCode As String
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
        '' 部品ノート 2012/02/10
        Private _BuhinNote As String

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

        ''' <summary>部品番号区分</summary>
        ''' <value>部品番号区分</value>
        ''' <returns>部品番号区分</returns>
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

        ''' <summary>設計社員番号</summary>
        ''' <value>設計社員番号</value>
        ''' <returns>設計社員番号</returns>
        Public Property SekkeiShainNo() As String
            Get
                Return _SekkeiShainNo
            End Get
            Set(ByVal value As String)
                _SekkeiShainNo = value
            End Set
        End Property

        ''' <summary>メーカーコード</summary>
        ''' <value>メーカーコード</value>
        ''' <returns>メーカーコード</returns>
        Public Property MakerCode() As String
            Get
                Return _MakerCode
            End Get
            Set(ByVal value As String)
                _MakerCode = value
            End Set
        End Property

        ''' <summary>SIAメーカーコード</summary>
        ''' <value>SIAメーカーコード</value>
        ''' <returns>SIAメーカーコード</returns>
        Public Property SiaMakerCode() As String
            Get
                Return _SiaMakerCode
            End Get
            Set(ByVal value As String)
                _SiaMakerCode = value
            End Set
        End Property

        ''' <summary>担当コード</summary>
        ''' <value>担当コード</value>
        ''' <returns>担当コード</returns>
        Public Property TantoCode() As String
            Get
                Return _TantoCode
            End Get
            Set(ByVal value As String)
                _TantoCode = value
            End Set
        End Property

        ''' <summary>責任部課コード</summary>
        ''' <value>責任部課コード</value>
        ''' <returns>責任部課コード</returns>
        Public Property SekininBukaCode() As String
            Get
                Return _SekininBukaCode
            End Get
            Set(ByVal value As String)
                _SekininBukaCode = value
            End Set
        End Property

        ''' <summary>責任サイト区分</summary>
        ''' <value>責任サイト区分</value>
        ''' <returns>責任サイト区分</returns>
        Public Property SekininSiteKbn() As String
            Get
                Return _SekininSiteKbn
            End Get
            Set(ByVal value As String)
                _SekininSiteKbn = value
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

        ''' <summary>図面改訂No.</summary>
        ''' <value>図面改訂No.</value>
        ''' <returns>図面改訂No.</returns>
        Public Property ZumenKaiteiNo() As String
            Get
                Return _ZumenKaiteiNo
            End Get
            Set(ByVal value As String)
                _ZumenKaiteiNo = value
            End Set
        End Property

        ''' <summary>
        ''' 承認サイン
        ''' </summary>
        ''' <value>承認サイン</value>
        ''' <returns>承認サイン</returns>
        ''' <remarks></remarks>
        Public Property ShoninSign() As String
            Get
                Return _ShoninSign
            End Get
            Set(ByVal value As String)
                _ShoninSign = value
            End Set
        End Property

        ''' <summary>
        ''' 承認日付
        ''' </summary>
        ''' <value>承認日付</value>
        ''' <returns>承認日付</returns>
        ''' <remarks></remarks>
        Public Property ShoninDate() As Nullable(Of Int32)
            Get
                Return _ShoninDate
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _ShoninDate = value
            End Set
        End Property

        ''' <summary>品目番号</summary>
        ''' <value>品目番号</value>
        ''' <returns>品目番号</returns>
        Public Property HinmokuNo() As String
            Get
                Return _HinmokuNo
            End Get
            Set(ByVal value As String)
                _HinmokuNo = value
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

        ''' <summary>部品カタカナ名称</summary>
        ''' <value>部品カタカナ名称</value>
        ''' <returns>部品カタカナ名称</returns>
        Public Property BuhinKanaName() As String
            Get
                Return _BuhinKanaName
            End Get
            Set(ByVal value As String)
                _BuhinKanaName = value
            End Set
        End Property

        ''' <summary>補助名称</summary>
        ''' <value>補助名称</value>
        ''' <returns>補助名称</returns>
        Public Property HojoName() As String
            Get
                Return _HojoName
            End Get
            Set(ByVal value As String)
                _HojoName = value
            End Set
        End Property

        ''' <summary>共用係数コード</summary>
        ''' <value>共用係数コード</value>
        ''' <returns>共用係数コード</returns>
        Public Property KeisuCode() As String
            Get
                Return _KeisuCode
            End Get
            Set(ByVal value As String)
                _KeisuCode = value
            End Set
        End Property

        ''' <summary>共用車種</summary>
        ''' <value>共用車種</value>
        ''' <returns>共用車種</returns>
        Public Property KyoyoModel() As String
            Get
                Return _KyoyoModel
            End Get
            Set(ByVal value As String)
                _KyoyoModel = value
            End Set
        End Property

        ''' <summary>納入単位</summary>
        ''' <value>納入単位</value>
        ''' <returns>納入単位</returns>
        Public Property NonyuTani() As String
            Get
                Return _NonyuTani
            End Get
            Set(ByVal value As String)
                _NonyuTani = value
            End Set
        End Property

        ''' <summary>部品種類</summary>
        ''' <value>部品種類</value>
        ''' <returns>部品種類</returns>
        Public Property BuhinKind() As String
            Get
                Return _BuhinKind
            End Get
            Set(ByVal value As String)
                _BuhinKind = value
            End Set
        End Property

        ''' <summary>内製区分</summary>
        ''' <value>内製区分</value>
        ''' <returns>内製区分</returns>
        Public Property NaiseiKbn() As String
            Get
                Return _NaiseiKbn
            End Get
            Set(ByVal value As String)
                _NaiseiKbn = value
            End Set
        End Property

        ''' <summary>LOWレベル</summary>
        ''' <value>LOWレベル</value>
        ''' <returns>LOWレベル</returns>
        Public Property LowLevel() As Nullable(of Int32)
            Get
                Return _LowLevel
            End Get
            Set(ByVal value As Nullable(of Int32))
                _LowLevel = value
            End Set
        End Property

        ''' <summary>板厚入力済み区分</summary>
        ''' <value>板厚入力済み区分</value>
        ''' <returns>板厚入力済み区分</returns>
        Public Property BankoSuryoInput() As String
            Get
                Return _BankoSuryoInput
            End Get
            Set(ByVal value As String)
                _BankoSuryoInput = value
            End Set
        End Property

        ''' <summary>部品質量(現在値)</summary>
        ''' <value>部品質量(現在値)</value>
        ''' <returns>部品質量(現在値)</returns>
        Public Property BuhinShitsuryo() As Nullable(of Int32)
            Get
                Return _BuhinShitsuryo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _BuhinShitsuryo = value
            End Set
        End Property

        ''' <summary>金属質量(現在値)入力済み区分</summary>
        ''' <value>金属質量(現在値)入力済み区分</value>
        ''' <returns>金属質量(現在値)入力済み区分</returns>
        Public Property KinzokuShitsuryoInput() As String
            Get
                Return _KinzokuShitsuryoInput
            End Get
            Set(ByVal value As String)
                _KinzokuShitsuryoInput = value
            End Set
        End Property

        ''' <summary>金属質量(現在値)</summary>
        ''' <value>金属質量(現在値)</value>
        ''' <returns>金属質量(現在値)</returns>
        Public Property KinzokuShitsuryo() As Nullable(of Int32)
            Get
                Return _KinzokuShitsuryo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _KinzokuShitsuryo = value
            End Set
        End Property

        ''' <summary>部品費(現在値)</summary>
        ''' <value>部品費(現在値)</value>
        ''' <returns>部品費(現在値)</returns>
        Public Property BuhinhiKingaku() As Nullable(of Decimal)
            Get
                Return _BuhinhiKingaku
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _BuhinhiKingaku = value
            End Set
        End Property

        ''' <summary>部品費取得日識別</summary>
        ''' <value>部品費取得日識別</value>
        ''' <returns>部品費取得日識別</returns>
        Public Property BuhinDateId() As String
            Get
                Return _BuhinDateId
            End Get
            Set(ByVal value As String)
                _BuhinDateId = value
            End Set
        End Property

        ''' <summary>SIA部品費(現在値)</summary>
        ''' <value>SIA部品費(現在値)</value>
        ''' <returns>SIA部品費(現在値)</returns>
        Public Property SiaBuhinhi() As Nullable(of Decimal)
            Get
                Return _SiaBuhinhi
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _SiaBuhinhi = value
            End Set
        End Property

        ''' <summary>SIA部品費取得日識別</summary>
        ''' <value>SIA部品費取得日識別</value>
        ''' <returns>SIA部品費取得日識別</returns>
        Public Property SiaBuhinDateId() As String
            Get
                Return _SiaBuhinDateId
            End Get
            Set(ByVal value As String)
                _SiaBuhinDateId = value
            End Set
        End Property

        ''' <summary>支給展伸材費</summary>
        ''' <value>支給展伸材費</value>
        ''' <returns>支給展伸材費</returns>
        Public Property Tenshinzai() As Nullable(of Decimal)
            Get
                Return _Tenshinzai
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _Tenshinzai = value
            End Set
        End Property

        ''' <summary>SIA支給展伸材費</summary>
        ''' <value>SIA支給展伸材費</value>
        ''' <returns>SIA支給展伸材費</returns>
        Public Property SiaTenshinzai() As Nullable(of Decimal)
            Get
                Return _SiaTenshinzai
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _SiaTenshinzai = value
            End Set
        End Property

        ''' <summary>型費(現在値)</summary>
        ''' <value>型費(現在値)</value>
        ''' <returns>型費(現在値)</returns>
        Public Property KatahiKingaku() As Nullable(of Int32)
            Get
                Return _KatahiKingaku
            End Get
            Set(ByVal value As Nullable(of Int32))
                _KatahiKingaku = value
            End Set
        End Property

        ''' <summary>SIA型費(現在値)</summary>
        ''' <value>SIA型費(現在値)</value>
        ''' <returns>SIA型費(現在値)</returns>
        Public Property SiaKatahiKingaku() As Nullable(of Int32)
            Get
                Return _SiaKatahiKingaku
            End Get
            Set(ByVal value As Nullable(of Int32))
                _SiaKatahiKingaku = value
            End Set
        End Property

        ''' <summary>パレット費</summary>
        ''' <value>パレット費</value>
        ''' <returns>パレット費</returns>
        Public Property Pallet() As Nullable(of Decimal)
            Get
                Return _Pallet
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _Pallet = value
            End Set
        End Property

        ''' <summary>SIAパレット費</summary>
        ''' <value>SIAパレット費</value>
        ''' <returns>SIAパレット費</returns>
        Public Property SiaPallet() As Nullable(of Decimal)
            Get
                Return _SiaPallet
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _SiaPallet = value
            End Set
        End Property

        ''' <summary>開発費</summary>
        ''' <value>開発費</value>
        ''' <returns>開発費</returns>
        Public Property Kaihatsuhi() As Nullable(of Decimal)
            Get
                Return _Kaihatsuhi
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _Kaihatsuhi = value
            End Set
        End Property

        ''' <summary>SIA開発費</summary>
        ''' <value>SIA開発費</value>
        ''' <returns>SIA開発費</returns>
        Public Property SiaKaihatsuhi() As Nullable(of Decimal)
            Get
                Return _SiaKaihatsuhi
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _SiaKaihatsuhi = value
            End Set
        End Property

        ''' <summary>板厚</summary>
        ''' <value>板厚</value>
        ''' <returns>板厚</returns>
        Public Property BankoSuryo() As String
            Get
                Return _BankoSuryo
            End Get
            Set(ByVal value As String)
                _BankoSuryo = value
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

        ''' <summary>集計用部課コード</summary>
        ''' <value>集計用部課コード</value>
        ''' <returns>集計用部課コード</returns>
        Public Property ShukeiBukaCode() As String
            Get
                Return _ShukeiBukaCode
            End Get
            Set(ByVal value As String)
                _ShukeiBukaCode = value
            End Set
        End Property

        ''' <summary>図面オーバー</summary>
        ''' <value>図面オーバー</value>
        ''' <returns>図面オーバー</returns>
        Public Property ZumenOver() As String
            Get
                Return _ZumenOver
            End Get
            Set(ByVal value As String)
                _ZumenOver = value
            End Set
        End Property

        ''' <summary>図面カラム</summary>
        ''' <value>図面カラム</value>
        ''' <returns>図面カラム</returns>
        Public Property ZumenColumn() As String
            Get
                Return _ZumenColumn
            End Get
            Set(ByVal value As String)
                _ZumenColumn = value
            End Set
        End Property

        ''' <summary>適用区分</summary>
        ''' <value>適用区分</value>
        ''' <returns>適用区分</returns>
        Public Property TekiyoKbn() As String
            Get
                Return _TekiyoKbn
            End Get
            Set(ByVal value As String)
                _TekiyoKbn = value
            End Set
        End Property

        ''' <summary>注記</summary>
        ''' <value>注記</value>
        ''' <returns>注記</returns>
        Public Property ChukiKijutsu() As String
            Get
                Return _ChukiKijutsu
            End Get
            Set(ByVal value As String)
                _ChukiKijutsu = value
            End Set
        End Property

        ''' <summary>内外装区分</summary>
        ''' <value>内外装区分</value>
        ''' <returns>内外装区分</returns>
        Public Property NaigaisoKbn() As String
            Get
                Return _NaigaisoKbn
            End Get
            Set(ByVal value As String)
                _NaigaisoKbn = value
            End Set
        End Property

        ''' <summary>材料・材質</summary>
        ''' <value>材料・材質</value>
        ''' <returns>材料・材質</returns>
        Public Property ZairyoKijutsu() As String
            Get
                Return _ZairyoKijutsu
            End Get
            Set(ByVal value As String)
                _ZairyoKijutsu = value
            End Set
        End Property

        ''' <summary>製法</summary>
        ''' <value>製法</value>
        ''' <returns>製法</returns>
        Public Property SeihoKijutu() As String
            Get
                Return _SeihoKijutu
            End Get
            Set(ByVal value As String)
                _SeihoKijutu = value
            End Set
        End Property

        ''' <summary>設計メモ</summary>
        ''' <value>設計メモ</value>
        ''' <returns>設計メモ</returns>
        Public Property DsgnMemo() As String
            Get
                Return _DsgnMemo
            End Get
            Set(ByVal value As String)
                _DsgnMemo = value
            End Set
        End Property

        ''' <summary>ファイナル区分</summary>
        ''' <value>ファイナル区分</value>
        ''' <returns>ファイナル区分</returns>
        Public Property FinalKbn() As String
            Get
                Return _FinalKbn
            End Get
            Set(ByVal value As String)
                _FinalKbn = value
            End Set
        End Property

        ''' <summary>重要保安区分</summary>
        ''' <value>重要保安区分</value>
        ''' <returns>重要保安区分</returns>
        Public Property JuyoHoanKbn() As String
            Get
                Return _JuyoHoanKbn
            End Get
            Set(ByVal value As String)
                _JuyoHoanKbn = value
            End Set
        End Property

        ''' <summary>重要保安部品コード</summary>
        ''' <value>重要保安部品コード</value>
        ''' <returns>重要保安部品コード</returns>
        Public Property JuyoHoanCode() As String
            Get
                Return _JuyoHoanCode
            End Get
            Set(ByVal value As String)
                _JuyoHoanCode = value
            End Set
        End Property

        ''' <summary>補用品処置コード</summary>
        ''' <value>補用品処置コード</value>
        ''' <returns>補用品処置コード</returns>
        Public Property HoyohinCode() As String
            Get
                Return _HoyohinCode
            End Get
            Set(ByVal value As String)
                _HoyohinCode = value
            End Set
        End Property

        ''' <summary>リサイクルマーク</summary>
        ''' <value>リサイクルマーク</value>
        ''' <returns>リサイクルマーク</returns>
        Public Property RecycleMark() As String
            Get
                Return _RecycleMark
            End Get
            Set(ByVal value As String)
                _RecycleMark = value
            End Set
        End Property

        ''' <summary>法規制表示コード</summary>
        ''' <value>法規制表示コード</value>
        ''' <returns>法規制表示コード</returns>
        Public Property HokiseiCode() As String
            Get
                Return _HokiseiCode
            End Get
            Set(ByVal value As String)
                _HokiseiCode = value
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

        ''' <summary>シリーズ</summary>
        ''' <value>シリーズ</value>
        ''' <returns>シリーズ</returns>
        Public Property SiriesCode() As String
            Get
                Return _SiriesCode
            End Get
            Set(ByVal value As String)
                _SiriesCode = value
            End Set
        End Property

        ''' <summary>表面処理</summary>
        ''' <value>表面処理</value>
        ''' <returns>表面処理</returns>
        Public Property HyomenShori() As String
            Get
                Return _HyomenShori
            End Get
            Set(ByVal value As String)
                _HyomenShori = value
            End Set
        End Property

        ''' <summary>生産・調達区分</summary>
        ''' <value>生産・調達区分</value>
        ''' <returns>生産・調達区分</returns>
        Public Property SeisanKbn() As String
            Get
                Return _SeisanKbn
            End Get
            Set(ByVal value As String)
                _SeisanKbn = value
            End Set
        End Property

        ''' <summary>部品状態区分</summary>
        ''' <value>部品状態区分</value>
        ''' <returns>部品状態区分</returns>
        Public Property BuhinStatusKbn() As String
            Get
                Return _BuhinStatusKbn
            End Get
            Set(ByVal value As String)
                _BuhinStatusKbn = value
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

        ''' <summary>出図予定年月日</summary>
        ''' <value>出図予定年月日</value>
        ''' <returns>出図予定年月日</returns>
        Public Property ShutuzuYoteiDate() As Nullable(of Int32)
            Get
                Return _ShutuzuYoteiDate
            End Get
            Set(ByVal value As Nullable(of Int32))
                _ShutuzuYoteiDate = value
            End Set
        End Property

        ''' <summary>ステータス</summary>
        ''' <value>ステータス</value>
        ''' <returns>ステータス</returns>
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property

        ''' <summary>更新元ツール区分</summary>
        ''' <value>更新元ツール区分</value>
        ''' <returns>更新元ツール区分</returns>
        Public Property UpdateKbn() As Nullable(of Int32)
            Get
                Return _UpdateKbn
            End Get
            Set(ByVal value As Nullable(of Int32))
                _UpdateKbn = value
            End Set
        End Property

        ''' <summary>部品アイテム名称</summary>
        ''' <value>部品アイテム名称</value>
        ''' <returns>部品アイテム名称</returns>
        Public Property BuhinItemName() As String
            Get
                Return _BuhinItemName
            End Get
            Set(ByVal value As String)
                _BuhinItemName = value
            End Set
        End Property

        ''' <summary>変動種別</summary>
        ''' <value>変動種別</value>
        ''' <returns>変動種別</returns>
        Public Property HendoShubetsu() As String
            Get
                Return _HendoShubetsu
            End Get
            Set(ByVal value As String)
                _HendoShubetsu = value
            End Set
        End Property

        ''' <summary>梱包輸送費</summary>
        ''' <value>梱包輸送費</value>
        ''' <returns>梱包輸送費</returns>
        Public Property KonpoYusohi() As Nullable(of Decimal)
            Get
                Return _KonpoYusohi
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _KonpoYusohi = value
            End Set
        End Property

        ''' <summary>コストベンチマーク部品費</summary>
        ''' <value>コストベンチマーク部品費</value>
        ''' <returns>コストベンチマーク部品費</returns>
        Public Property BenchiBuhinhi() As Nullable(of Decimal)
            Get
                Return _BenchiBuhinhi
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _BenchiBuhinhi = value
            End Set
        End Property

        ''' <summary>SIAコストベンチマーク部品費</summary>
        ''' <value>SIAコストベンチマーク部品費</value>
        ''' <returns>SIAコストベンチマーク部品費</returns>
        Public Property SiaBenchiBuhinhi() As Nullable(of Decimal)
            Get
                Return _SiaBenchiBuhinhi
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _SiaBenchiBuhinhi = value
            End Set
        End Property

        ''' <summary>コストベンチマーク型費</summary>
        ''' <value>コストベンチマーク型費</value>
        ''' <returns>コストベンチマーク型費</returns>
        Public Property BenchiKatahi() As Nullable(of Decimal)
            Get
                Return _BenchiKatahi
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _BenchiKatahi = value
            End Set
        End Property

        ''' <summary>SIAコストベンチマーク型費</summary>
        ''' <value>SIAコストベンチマーク型費</value>
        ''' <returns>SIAコストベンチマーク型費</returns>
        Public Property SiaBenchiKatahi() As Nullable(of Decimal)
            Get
                Return _SiaBenchiKatahi
            End Get
            Set(ByVal value As Nullable(of Decimal))
                _SiaBenchiKatahi = value
            End Set
        End Property

        ''' <summary>国内コスト区分</summary>
        ''' <value>国内コスト区分</value>
        ''' <returns>国内コスト区分</returns>
        Public Property KokunaiCostKbn() As String
            Get
                Return _KokunaiCostKbn
            End Get
            Set(ByVal value As String)
                _KokunaiCostKbn = value
            End Set
        End Property

        ''' <summary>海外コスト区分</summary>
        ''' <value>海外コスト区分</value>
        ''' <returns>海外コスト区分</returns>
        Public Property KaigaiCostKbn() As String
            Get
                Return _KaigaiCostKbn
            End Get
            Set(ByVal value As String)
                _KaigaiCostKbn = value
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

        ''' <summary>板厚・板厚</summary>
        ''' <value>板厚・板厚</value>
        ''' <returns>板厚・板厚</returns>
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

        ''' <summary>試作部品費（円）</summary>
        ''' <value>試作部品費（円）</value>
        ''' <returns>試作部品費（円）</returns>
        Public Property ShisakuBuhinnHi() As Nullable(of Int32)
            Get
                Return _ShisakuBuhinnHi
            End Get
            Set(ByVal value As Nullable(of Int32))
                _ShisakuBuhinnHi = value
            End Set
        End Property

        ''' <summary>試作型費（千円）</summary>
        ''' <value>試作型費（千円）</value>
        ''' <returns>試作型費（千円）</returns>
        Public Property ShisakuKataHi() As Nullable(of Int32)
            Get
                Return _ShisakuKataHi
            End Get
            Set(ByVal value As Nullable(of Int32))
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

        ''' <summary>編集登録日</summary>
        ''' <value>編集登録日</value>
        ''' <returns>編集登録日</returns>
        Public Property HensyuTourokubi() As Nullable(of Int32)
            Get
                Return _HensyuTourokubi
            End Get
            Set(ByVal value As Nullable(of Int32))
                _HensyuTourokubi = value
            End Set
        End Property

        ''' <summary>編集登録時間</summary>
        ''' <value>編集登録時間</value>
        ''' <returns>編集登録時間</returns>
        Public Property HensyuTourokujikan() As Nullable(of Int32)
            Get
                Return _HensyuTourokujikan
            End Get
            Set(ByVal value As Nullable(of Int32))
                _HensyuTourokujikan = value
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

    End Class
End Namespace
