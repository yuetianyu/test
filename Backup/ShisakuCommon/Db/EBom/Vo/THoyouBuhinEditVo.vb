Namespace Db.EBom.Vo
    ''' <summary>
    ''' 補用部品編集情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class THoyouBuhinEditVo

        '' 補用イベントコード  
        Private _HoyouEventCode As String
        '' 補用部課コード  
        Private _HoyouBukaCode As String
        '' 補用担当  
        Private _HoyouTanto As String
        '' 補用担当改訂№  
        Private _HoyouTantoKaiteiNo As String
        '' 部品番号表示順  
        Private _BuhinNoHyoujiJun As Nullable(Of Int32)

        '   改訂差分抽出のため追加
        '  2014.05.28
        ' 部品番号表示順 前回値 
        Private _BuhinNoHyoujiJunZenkai As Nullable(Of Int32)

        '' レベル  
        Private _Level As Nullable(Of Int32)
        '' 国内集計コード  
        Private _ShukeiCode As String
        '' 海外SIA集計コード  
        Private _SiaShukeiCode As String
        '' 現調CKD区分  
        Private _GencyoCkdKbn As String
        '' 取引先コード  
        Private _MakerCode As String
        '' 取引先名称  
        Private _MakerName As String
        '' 部品番号  
        Private _BuhinNo As String
        '' 部品名称  
        Private _BuhinName As String
        '' 員数
        Private _InsuSuryo As String
        '' 供給セクション 
        Private _KyoukuSection As String
        '' 部品ノート
        Private _BuhinNote As String
        '' メモ０１
        Private _Memo1 As String
        '' メモ０２
        Private _Memo2 As String
        '' メモ０３
        Private _Memo3 As String
        '' メモ０４
        Private _Memo4 As String
        '' メモ０５
        Private _Memo5 As String
        '' メモ０６
        Private _Memo6 As String
        '' メモ０７
        Private _Memo7 As String
        '' メモ０８
        Private _Memo8 As String
        '' メモ０９
        Private _Memo9 As String
        '' メモ１０
        Private _Memo10 As String
        '' メモ０１タイトル
        Private _Memo1T As String
        '' メモ０２タイトル
        Private _Memo2T As String
        '' メモ０３タイトル
        Private _Memo3T As String
        '' メモ０４タイトル
        Private _Memo4T As String
        '' メモ０５タイトル
        Private _Memo5T As String
        '' メモ０６タイトル
        Private _Memo6T As String
        '' メモ０７タイトル
        Private _Memo7T As String
        '' メモ０８タイトル
        Private _Memo8T As String
        '' メモ０９タイトル
        Private _Memo9T As String
        '' メモ１０タイトル
        Private _Memo10T As String
        '' メモシート系０１
        Private _MemoSheet1 As String
        '' メモシート系０２
        Private _MemoSheet2 As String
        '' メモシート系０３
        Private _MemoSheet3 As String
        '' メモシート系０４
        Private _MemoSheet4 As String
        '' メモシート系０５
        Private _MemoSheet5 As String
        '' メモシート系０６
        Private _MemoSheet6 As String
        '' メモシート系０７
        Private _MemoSheet7 As String
        '' メモシート系０８
        Private _MemoSheet8 As String
        '' メモドアトリム系０１
        Private _MemoDoorTrim1 As String
        '' メモドアトリム系０２
        Private _MemoDoorTrim2 As String
        '' メモルーフトリム系０１
        Private _MemoRoofTrim1 As String
        '' メモルーフトリム系０２
        Private _MemoRoofTrim2 As String
        '' メモサンルーフトリム系０１
        Private _MemoSunroofTrim1 As String
        '' 備考  
        Private _Bikou As String
        '' 編集登録日  
        Private _EditTourokubi As Nullable(Of Int32)
        '' 編集登録時間  
        Private _EditTourokujikan As Nullable(Of Int32)
        '' 改訂判断フラグ  
        Private _KaiteiHandanFlg As String
        '' 補用リストコード  
        Private _HoyouListCode As String
        '' 入力フラグ  
        Private _InputFlg As String
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

        ''↓↓2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_by) (TES)張 ADD BEGIN
        '' 作り方・製作方法
        Private _TsukurikataSeisaku As String
        '' 作り方・型仕様1
        Private _TsukurikataKatashiyou1 As String
        '' 作り方・型仕様2
        Private _TsukurikataKatashiyou2 As String
        '' 作り方・型仕様3
        Private _TsukurikataKatashiyou3 As String
        '' 作り方・治具
        Private _TsukurikataTigu As String
        '' 作り方・納入見通し
        Private _TsukurikataNounyu As Int32?
        '' 作り方・部品製作規模・概要
        Private _TsukurikataKibo As String
        '' 部品番号試作区分  
        Private _BuhinNoKbn As String
        '' 部品番号改訂No.  
        Private _BuhinNoKaiteiNo As String
        '' 枝番  
        Private _EdaBan As String
        '' 出図予定日  
        Private _ShutuzuYoteiDate As Int32?
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
        Private _ShisakuBuhinHi As Int32?
        '' 試作型費（千円）  
        Private _ShisakuKataHi As Int32?
        ''↑↑2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_by) (TES)張 ADD END
        ''↓↓2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bd) (TES)張 ADD BEGIN
        '' 再使用不可  
        Private _Saishiyoufuka As String
        '' ベース情報フラグ
        Private _BaseBuhinFlg As String
        '' INSTL品番表示順  
        Private _InstlHinbanHyoujiJun As Int32?
        ''↑↑2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bd) (TES)張 ADD END

        ''↓↓2014/12/24 メタル対応追加フィールド (TES)張 ADD BEGIN
        '材料情報・製品長
        Private _MaterialInfoLength As Nullable(Of Int32)
        '' 材料情報・製品幅
        Private _MaterialInfoWidth As Nullable(Of Int32)
        '' データ項目・改訂№
        Private _DataItemKaiteiNo As String
        '' データ項目・エリア名
        Private _DataItemAreaName As String
        '' データ項目・セット名
        Private _DataItemSetName As String
        '' データ項目・改訂情報
        Private _DataItemKaiteiInfo As String
        ''↑↑2014/12/24 メタル対応追加フィールド (TES)張 ADD END

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

        ''' <summary>部品番号表示順前回値</summary>
        ''' <value>部品番号表示順前回値</value>
        ''' <returns>部品番号表示順前回値</returns>
        Public Property BuhinNoHyoujiJunZenkai() As Nullable(Of Int32)
            Get
                Return _BuhinNoHyoujiJunZenkai
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _BuhinNoHyoujiJunZenkai = value
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
        Public Property InsuSuryo() As String
            Get
                Return _InsuSuryo
            End Get
            Set(ByVal value As String)
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
            '2012/01/25 部品ノート追加
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
        Public Property Memo1() As String
            Get
                Return _Memo1
            End Get
            Set(ByVal value As String)
                _Memo1 = value
            End Set
        End Property

        ''' <summary>メモ０２</summary>
        ''' <value>メモ０２</value>
        ''' <returns>メモ０２</returns>
        Public Property Memo2() As String
            Get
                Return _Memo2
            End Get
            Set(ByVal value As String)
                _Memo2 = value
            End Set
        End Property

        ''' <summary>メモ０３</summary>
        ''' <value>メモ０３</value>
        ''' <returns>メモ０３</returns>
        Public Property Memo3() As String
            Get
                Return _Memo3
            End Get
            Set(ByVal value As String)
                _Memo3 = value
            End Set
        End Property

        ''' <summary>メモ０４</summary>
        ''' <value>メモ０４</value>
        ''' <returns>メモ０４</returns>
        Public Property Memo4() As String
            Get
                Return _Memo4
            End Get
            Set(ByVal value As String)
                _Memo4 = value
            End Set
        End Property

        ''' <summary>メモ０５</summary>
        ''' <value>メモ０５</value>
        ''' <returns>メモ０５</returns>
        Public Property Memo5() As String
            Get
                Return _Memo5
            End Get
            Set(ByVal value As String)
                _Memo5 = value
            End Set
        End Property

        ''' <summary>メモ０６</summary>
        ''' <value>メモ０６</value>
        ''' <returns>メモ０６</returns>
        Public Property Memo6() As String
            Get
                Return _Memo6
            End Get
            Set(ByVal value As String)
                _Memo6 = value
            End Set
        End Property

        ''' <summary>メモ０７</summary>
        ''' <value>メモ０７</value>
        ''' <returns>メモ０７</returns>
        Public Property Memo7() As String
            Get
                Return _Memo7
            End Get
            Set(ByVal value As String)
                _Memo7 = value
            End Set
        End Property

        ''' <summary>メモ０８</summary>
        ''' <value>メモ０８</value>
        ''' <returns>メモ０８</returns>
        Public Property Memo8() As String
            Get
                Return _Memo8
            End Get
            Set(ByVal value As String)
                _Memo8 = value
            End Set
        End Property

        ''' <summary>メモ０９</summary>
        ''' <value>メモ０９</value>
        ''' <returns>メモ０９</returns>
        Public Property Memo9() As String
            Get
                Return _Memo9
            End Get
            Set(ByVal value As String)
                _Memo9 = value
            End Set
        End Property

        ''' <summary>メモ１０</summary>
        ''' <value>メモ１０</value>
        ''' <returns>メモ１０</returns>
        Public Property Memo10() As String
            Get
                Return _Memo10
            End Get
            Set(ByVal value As String)
                _Memo10 = value
            End Set
        End Property

        ''' <summary>メモ０１タイトル</summary>
        ''' <value>メモ０１タイトル</value>
        ''' <returns>メモ０１タイトル</returns>
        Public Property Memo1T() As String
            Get
                Return _Memo1T
            End Get
            Set(ByVal value As String)
                _Memo1T = value
            End Set
        End Property

        ''' <summary>メモ０２タイトル</summary>
        ''' <value>メモ０２タイトル</value>
        ''' <returns>メモ０２タイトル</returns>
        Public Property Memo2T() As String
            Get
                Return _Memo2T
            End Get
            Set(ByVal value As String)
                _Memo2T = value
            End Set
        End Property

        ''' <summary>メモ０３タイトル</summary>
        ''' <value>メモ０３タイトル</value>
        ''' <returns>メモ０３タイトル</returns>
        Public Property Memo3T() As String
            Get
                Return _Memo3T
            End Get
            Set(ByVal value As String)
                _Memo3T = value
            End Set
        End Property

        ''' <summary>メモ０４タイトル</summary>
        ''' <value>メモ０４タイトル</value>
        ''' <returns>メモ０４タイトル</returns>
        Public Property Memo4T() As String
            Get
                Return _Memo4T
            End Get
            Set(ByVal value As String)
                _Memo4T = value
            End Set
        End Property

        ''' <summary>メモ０５タイトル</summary>
        ''' <value>メモ０５タイトル</value>
        ''' <returns>メモ０５タイトル</returns>
        Public Property Memo5T() As String
            Get
                Return _Memo5T
            End Get
            Set(ByVal value As String)
                _Memo5T = value
            End Set
        End Property

        ''' <summary>メモ０６タイトル</summary>
        ''' <value>メモ０６タイトル</value>
        ''' <returns>メモ０６タイトル</returns>
        Public Property Memo6T() As String
            Get
                Return _Memo6T
            End Get
            Set(ByVal value As String)
                _Memo6T = value
            End Set
        End Property

        ''' <summary>メモ０７タイトル</summary>
        ''' <value>メモ０７タイトル</value>
        ''' <returns>メモ０７タイトル</returns>
        Public Property Memo7T() As String
            Get
                Return _Memo7T
            End Get
            Set(ByVal value As String)
                _Memo7T = value
            End Set
        End Property

        ''' <summary>メモ０８タイトル</summary>
        ''' <value>メモ０８タイトル</value>
        ''' <returns>メモ０８タイトル</returns>
        Public Property Memo8T() As String
            Get
                Return _Memo8T
            End Get
            Set(ByVal value As String)
                _Memo8T = value
            End Set
        End Property

        ''' <summary>メモ０９タイトル</summary>
        ''' <value>メモ０９タイトル</value>
        ''' <returns>メモ０９タイトル</returns>
        Public Property Memo9T() As String
            Get
                Return _Memo9T
            End Get
            Set(ByVal value As String)
                _Memo9T = value
            End Set
        End Property

        ''' <summary>メモ１０タイトル</summary>
        ''' <value>メモ１０タイトル</value>
        ''' <returns>メモ１０タイトル</returns>
        Public Property Memo10T() As String
            Get
                Return _Memo10T
            End Get
            Set(ByVal value As String)
                _Memo10T = value
            End Set
        End Property

        ''' <summary>メモシート系０１</summary>
        ''' <value>メモシート系０１</value>
        ''' <returns>メモシート系０１</returns>
        Public Property MemoSheet1() As String
            Get
                Return _MemoSheet1
            End Get
            Set(ByVal value As String)
                _MemoSheet1 = value
            End Set
        End Property

        ''' <summary>メモシート系０２</summary>
        ''' <value>メモシート系０２</value>
        ''' <returns>メモシート系０２</returns>
        Public Property MemoSheet2() As String
            Get
                Return _MemoSheet2
            End Get
            Set(ByVal value As String)
                _MemoSheet2 = value
            End Set
        End Property

        ''' <summary>メモシート系０３</summary>
        ''' <value>メモシート系０３</value>
        ''' <returns>メモシート系０３</returns>
        Public Property MemoSheet3() As String
            Get
                Return _MemoSheet3
            End Get
            Set(ByVal value As String)
                _MemoSheet3 = value
            End Set
        End Property

        ''' <summary>メモシート系０４</summary>
        ''' <value>メモシート系０４</value>
        ''' <returns>メモシート系０４</returns>
        Public Property MemoSheet4() As String
            Get
                Return _MemoSheet4
            End Get
            Set(ByVal value As String)
                _MemoSheet4 = value
            End Set
        End Property

        ''' <summary>メモシート系０５</summary>
        ''' <value>メモシート系０５</value>
        ''' <returns>メモシート系０５</returns>
        Public Property MemoSheet5() As String
            Get
                Return _MemoSheet5
            End Get
            Set(ByVal value As String)
                _MemoSheet5 = value
            End Set
        End Property

        ''' <summary>メモシート系０６</summary>
        ''' <value>メモシート系０６</value>
        ''' <returns>メモシート系０６</returns>
        Public Property MemoSheet6() As String
            Get
                Return _MemoSheet6
            End Get
            Set(ByVal value As String)
                _MemoSheet6 = value
            End Set
        End Property

        ''' <summary>メモシート系０７</summary>
        ''' <value>メモシート系０７</value>
        ''' <returns>メモシート系０７</returns>
        Public Property MemoSheet7() As String
            Get
                Return _MemoSheet7
            End Get
            Set(ByVal value As String)
                _MemoSheet7 = value
            End Set
        End Property

        ''' <summary>メモシート系０８</summary>
        ''' <value>メモシート系０８</value>
        ''' <returns>メモシート系０８</returns>
        Public Property MemoSheet8() As String
            Get
                Return _MemoSheet8
            End Get
            Set(ByVal value As String)
                _MemoSheet8 = value
            End Set
        End Property

        ''' <summary>メモドアトリム系０１</summary>
        ''' <value>メモドアトリム系０１</value>
        ''' <returns>メモドアトリム系０１</returns>
        Public Property MemoDoorTrim1() As String
            Get
                Return _MemoDoorTrim1
            End Get
            Set(ByVal value As String)
                _MemoDoorTrim1 = value
            End Set
        End Property

        ''' <summary>メモドアトリム系０２</summary>
        ''' <value>メモドアトリム系０２</value>
        ''' <returns>メモドアトリム系０２</returns>
        Public Property MemoDoorTrim2() As String
            Get
                Return _MemoDoorTrim2
            End Get
            Set(ByVal value As String)
                _MemoDoorTrim2 = value
            End Set
        End Property

        ''' <summary>メモルーフトリム系０１</summary>
        ''' <value>メモルーフトリム系０１</value>
        ''' <returns>メモルーフトリム系０１</returns>
        Public Property MemoRoofTrim1() As String
            Get
                Return _MemoRoofTrim1
            End Get
            Set(ByVal value As String)
                _MemoRoofTrim1 = value
            End Set
        End Property

        ''' <summary>メモルーフトリム系０２</summary>
        ''' <value>メモルーフトリム系０２</value>
        ''' <returns>メモルーフトリム系０２</returns>
        Public Property MemoRoofTrim2() As String
            Get
                Return _MemoRoofTrim2
            End Get
            Set(ByVal value As String)
                _MemoRoofTrim2 = value
            End Set
        End Property

        ''' <summary>メモサンルーフトリム系０１</summary>
        ''' <value>メモサンルーフトリム系０１</value>
        ''' <returns>メモサンルーフトリム系０１</returns>
        Public Property MemoSunroofTrim1() As String
            Get
                Return _MemoSunroofTrim1
            End Get
            Set(ByVal value As String)
                _MemoSunroofTrim1 = value
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

        ''' <summary>入力フラグ</summary>
        ''' <value>入力フラグ</value>
        ''' <returns>入力フラグ</returns>
        Public Property InputFlg() As String
            Get
                Return _InputFlg
            End Get
            Set(ByVal value As String)
                _InputFlg = value
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

        ''↓↓2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_by) (TES)張 ADD BEGIN
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

        ''' <summary>出図予定日</summary>
        ''' <value>出図予定日</value>
        ''' <returns>出図予定日</returns>
        Public Property ShutuzuYoteiDate() As Int32?
            Get
                Return _ShutuzuYoteiDate
            End Get
            Set(ByVal value As Int32?)
                _ShutuzuYoteiDate = value
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
        Public Property ShisakuBuhinHi() As Int32?
            Get
                Return _ShisakuBuhinHi
            End Get
            Set(ByVal value As Int32?)
                _ShisakuBuhinHi = value
            End Set
        End Property

        ''' <summary>試作型費（千円）</summary>
        ''' <value>試作型費（千円）</value>
        ''' <returns>試作型費（千円）</returns>
        Public Property ShisakuKataHi() As Int32?
            Get
                Return _ShisakuKataHi
            End Get
            Set(ByVal value As Int32?)
                _ShisakuKataHi = value
            End Set
        End Property
        ''↑↑2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_by) (TES)張 ADD END

        ''↓↓2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bd) (TES)張 ADD BEGIN
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

        ''' <summary>INSTL品番表示順</summary>
        ''' <value>INSTL品番表示順</value>
        ''' <returns>INSTL品番表示順</returns>
        Public Property InstlHinbanHyoujiJun() As Int32?
            Get
                Return _InstlHinbanHyoujiJun
            End Get
            Set(ByVal value As Int32?)
                _InstlHinbanHyoujiJun = value
            End Set
        End Property
        ''↑↑2014/08/05 Ⅰ.3.設計編集 ベース車改修専用化_bd) (TES)張 ADD END

        '↓↓↓2014/12/25 メタル対応追加フィールド TES)張 ADD BEGIN
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
        '↑↑↑2014/12/25 メタル対応追加フィールド (TES)張 ADD END

    End Class
End Namespace