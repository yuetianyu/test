﻿Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作部品編集情報(EBOM設変)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuBuhinEditEbomKanshiVo

        '' 試作イベントコード  
        Private _ShisakuEventCode As String
        '' 試作部課コード  
        Private _ShisakuBukaCode As String
        '' 試作ブロック№  
        Private _ShisakuBlockNo As String
        '' 試作ブロック№改訂№  
        Private _ShisakuBlockNoKaiteiNo As String
        '' 部品番号表示順  
        Private _BuhinNoHyoujiJun As Int32?
        '' レベル  
        Private _Level As Int32?
        '' 国内集計コード  
        Private _ShukeiCode As String
        '' 海外SIA集計コード  
        Private _SiaShukeiCode As String
        '' 現調CKD区分  
        Private _GencyoCkdKbn As String
        '' 供給セクション 2012/01/17
        Private _KyoukuSection As String
        '' 取引先コード  
        Private _MakerCode As String
        '' 取引先名称  
        Private _MakerName As String
        '' 部品番号  
        Private _BuhinNo As String
        '' 部品番号試作区分  
        Private _BuhinNoKbn As String
        '' 部品番号改訂No.  
        Private _BuhinNoKaiteiNo As String
        '' 枝番  
        Private _EdaBan As String
        '' 部品名称  
        Private _BuhinName As String
        '' 再使用不可  
        Private _Saishiyoufuka As String
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


        ''↓↓2014/08/04 (DANIEL)柳沼 ADD BEGIN
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
        '' ベース情報フラグ
        Private _BaseBuhinFlg As String
        ''↑↑2014/08/04 (DANIEL)柳沼 ADD END

        ''↓↓2014/12/24 メタル対応追加フィールド (TES)張 ADD BEGIN
        '' 材料情報・製品長
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

        '' 試作部品費（円）  
        Private _ShisakuBuhinHi As Int32?
        '' 試作型費（千円）  
        Private _ShisakuKataHi As Int32?
        '' 備考  
        Private _Bikou As String
        '' 編集登録日  
        Private _EditTourokubi As Int32?
        '' 編集登録時間  
        Private _EditTourokujikan As Int32?
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
        '2012/01/25 部品ノート追加
        '' 部品ノート
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

        ''' <summary>部品番号表示順</summary>
        ''' <value>部品番号表示順</value>
        ''' <returns>部品番号表示順</returns>
        Public Property BuhinNoHyoujiJun() As Int32?
            Get
                Return _BuhinNoHyoujiJun
            End Get
            Set(ByVal value As Int32?)
                _BuhinNoHyoujiJun = value
            End Set
        End Property

        ''' <summary>レベル</summary>
        ''' <value>レベル</value>
        ''' <returns>レベル</returns>
        Public Property Level() As Int32?
            Get
                Return _Level
            End Get
            Set(ByVal value As Int32?)
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

        ''' <summary>供給セクション 2012/01/17</summary>
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
        Public Property ShutuzuYoteiDate() As Int32?
            Get
                Return _ShutuzuYoteiDate
            End Get
            Set(ByVal value As Int32?)
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


        ''↓↓2014/08/04 (DANIEL)柳沼 ADD BEGIN

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
        ''↑↑2014/08/04 (DANIEL)柳沼 ADD END

        ''↓↓2014/12/24 メタル対応追加フィールド (TES)張 ADD BEGIN
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
        ''↑↑2014/12/24 メタル対応追加フィールド (TES)張 ADD END

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
        Public Property EditTourokubi() As Int32?
            Get
                Return _EditTourokubi
            End Get
            Set(ByVal value As Int32?)
                _EditTourokubi = value
            End Set
        End Property

        ''' <summary>編集登録時間</summary>
        ''' <value>編集登録時間</value>
        ''' <returns>編集登録時間</returns>
        Public Property EditTourokujikan() As Int32?
            Get
                Return _EditTourokujikan
            End Get
            Set(ByVal value As Int32?)
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
            '2012/01/25 部品ノート追加
            Get
                Return _BuhinNote
            End Get
            Set(ByVal value As String)
                _BuhinNote = value
            End Set
        End Property
    End Class
End Namespace