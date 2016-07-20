Namespace Db.EBom.Vo
    ''' <summary>試作部品編集情報（TMP）</summary>
    Public Class TShisakuBuhinEditTmp3dVo
        ''' <summary>GUID</summary>
        Private _Guid As String
        ''' <summary>試作イベントコード</summary>
        Private _ShisakuEventCode As String
        ''' <summary>試作部課コード</summary>
        Private _ShisakuBukaCode As String
        ''' <summary>試作ブロック№</summary>
        Private _ShisakuBlockNo As String
        ''' <summary>試作ブロック№改訂№</summary>
        Private _ShisakuBlockNoKaiteiNo As String
        ''' <summary>部品番号表示順</summary>
        Private _BuhinNoHyoujiJun As Nullable(Of Int32)
        ''' <summary>行ＩＤ</summary>
        Private _GyouId As String
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
        ''' <summary>出図予定日</summary>
        Private _ShutuzuYoteiDate As Nullable(Of Int32)
        ''' <summary>材質・規格１</summary>
        Private _ZaishituKikaku1 As String
        ''' <summary>材質・規格２</summary>
        Private _ZaishituKikaku2 As String
        ''' <summary>材質・規格３</summary>
        Private _ZaishituKikaku3 As String
        ''' <summary>材質・メッキ</summary>
        Private _ZaishituMekki As String
        ''' <summary>板厚</summary>
        Private _ShisakuBankoSuryo As String
        ''' <summary>板厚・ｕ</summary>
        Private _ShisakuBankoSuryoU As String
        ''' <summary>試作部品費(円)</summary>
        Private _ShisakuBuhinHi As Nullable(Of Int32)
        ''' <summary>試作型費(千円)</summary>
        Private _ShisakuKataHi As Nullable(Of Int32)
        ''' <summary>備考</summary>
        Private _Bikou As String
        ''' <summary>編集登録日</summary>
        Private _EditTourokubi As Nullable(Of Int32)
        ''' <summary>編集登録時間</summary>
        Private _EditTourokujikan As Nullable(Of Int32)
        ''' <summary>改訂判断フラグ</summary>
        Private _KaiteiHandanFlg As String
        ''' <summary>手配記号</summary>
        Private _TehaiKigou As String
        ''' <summary>納場</summary>
        Private _Nouba As String
        ''' <summary>供給セクション</summary>
        Private _KyoukuSection As String
        ''' <summary>専用マーク</summary>
        Private _SenyouMark As String
        ''' <summary>購担</summary>
        Private _Koutan As String
        ''' <summary>図面設通№</summary>
        Private _StsrDhstba As String
        ''' <summary>変化点</summary>
        Private _Henkaten As String
        ''' <summary>試作製品区分</summary>
        Private _ShisakuSeihinKbn As String
        ''' <summary>試作リストコード</summary>
        Private _ShisakuListCode As String
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

        ''' <summary>GUID</summary>
        ''' <value>GUID</value>
        ''' <returns>GUID</returns>
        Public Property Guid() As String
            Get
                Return _Guid
            End Get
            Set(ByVal value As String)
                _Guid = value
            End Set
        End Property
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
        ''' <summary>行ＩＤ</summary>
        ''' <value>行ＩＤ</value>
        ''' <returns>行ＩＤ</returns>
        Public Property GyouId() As String
            Get
                Return _GyouId
            End Get
            Set(ByVal value As String)
                _GyouId = value
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
        ''' <summary>図面設通№</summary>
        ''' <value>図面設通№</value>
        ''' <returns>図面設通№</returns>
        Public Property StsrDhstba() As String
            Get
                Return _StsrDhstba
            End Get
            Set(ByVal value As String)
                _StsrDhstba = value
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
    End Class
End Namespace