Namespace Db.EBom.Vo
    ''' <summary>
    ''' 現調品発注控ファイル情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AsORPF57Vo

        ''' <summary>リストコード</summary>
        Private _Lscd As String
        ''' <summary>リクエスト№</summary>
        Private _Reqno As String
        ''' <summary>グループ№</summary>
        Private _Grno As String
        ''' <summary>シリアル№</summary>
        Private _Srno As String
        ''' <summary>工事指令書№</summary>
        Private _Komzba As String
        ''' <summary>ブロック№</summary>
        Private _Bnba As String
        ''' <summary>管理№</summary>
        Private _Kbba As String
        ''' <summary>行ID</summary>
        Private _Gyoid As String
        ''' <summary>発注年月０６</summary>
        Private _Hayy06 As Nullable(Of Int32)
        ''' <summary>作業依頼書№</summary>
        Private _Sgisba As String
        ''' <summary>工事区分</summary>
        Private _Kokm As String
        ''' <summary>工事№</summary>
        Private _Koba As String
        ''' <summary>部品番号１５</summary>
        Private _Buba15 As String
        ''' <summary>部品名称</summary>
        Private _Bunm As String
        ''' <summary>設計要求日</summary>
        Private _Sdyoym As Nullable(Of Int32)
        ''' <summary>注文書№</summary>
        Private _Cmba As String
        ''' <summary>取引先コード</summary>
        Private _Trcd As String
        ''' <summary>供給セクション</summary>
        Private _Kysx As String
        ''' <summary>発注年月日</summary>
        Private _Haym As Nullable(Of Int32)
        ''' <summary>発注個数</summary>
        Private _Haca As Nullable(Of Int32)
        ''' <summary>希望単価１０</summary>
        Private _Bota10 As Nullable(Of Int32)
        ''' <summary>納入場所</summary>
        Private _Nobs As String
        ''' <summary>納入区分</summary>
        Private _Nokm As String
        ''' <summary>取消年月日</summary>
        Private _Tlym As Nullable(Of Int32)
        ''' <summary>取消数</summary>
        Private _Tlca As Nullable(Of Int32)
        ''' <summary>取消フラグ</summary>
        Private _Tlflg As String
        ''' <summary>購坦</summary>
        Private _Ka As String
        ''' <summary>購入単価１０</summary>
        Private _Kota10 As Nullable(Of Int32)
        ''' <summary>支給単価１０</summary>
        Private _Sita10 As Nullable(Of Int32)
        ''' <summary>支払済フラグ</summary>
        Private _Shflg As String
        ''' <summary>支払日</summary>
        Private _Sihady As Nullable(Of Int32)
        ''' <summary>ユニット区分</summary>
        Private _Unkm As String
        ''' <summary>納入年月日</summary>
        Private _Noym As Nullable(Of Int32)
        ''' <summary>納入累計数</summary>
        Private _Noru As Nullable(Of Int32)
        ''' <summary>納入残数</summary>
        Private _Nozu As Nullable(Of Int32)
        ''' <summary>レベル</summary>
        Private _Lv As Nullable(Of Int32)
        ''' <summary>専用マーク</summary>
        Private _Aamq As String
        ''' <summary>改訂№</summary>
        Private _Kdba As String
        ''' <summary>設通№</summary>
        Private _Stba As String
        ''' <summary>バーコード情報</summary>
        Private _Brno As String
        ''' <summary>輸送会社</summary>
        Private _Yusona As String
        ''' <summary>現地供給セクション</summary>
        Private _Gkyna As String
        ''' <summary>配送日</summary>
        Private _Hsdy As Nullable(Of Int32)
        ''' <summary>配送先名称</summary>
        Private _Hsna As String
        ''' <summary>SIA備考</summary>
        Private _Siabk As String
        ''' <summary>オーダーシート発行日</summary>
        Private _Odhcdy As Nullable(Of Int32)
        ''' <summary>データインポート日時</summary>
        Private _Eipym As String
        ''' <summary>D/O№</summary>
        Private _Edono As String
        ''' <summary>EVENTRA発注処理日</summary>
        Private _Eevenym As Nullable(Of Int32)
        ''' <summary>納入指示日サプライヤー</summary>
        Private _Enoym As Nullable(Of Int32)
        ''' <summary>納入指示数サプライヤー</summary>
        Private _Enoca As Nullable(Of Int32)
        ''' <summary>取引先コードサプライヤー</summary>
        Private _Etrcd As String
        ''' <summary>取消日サプライヤー</summary>
        Private _Etlym As Nullable(Of Int32)
        ''' <summary>取消累計数サプライヤー</summary>
        Private _Etlca As Nullable(Of Int32)
        ''' <summary>取消フラグサプライヤー</summary>
        Private _Etlflg As String
        ''' <summary>現調品購入単価サプライヤー</summary>
        Private _Egkota10 As Nullable(Of Int32)
        ''' <summary>納期回答日サプライヤー</summary>
        Private _Enqans As Nullable(Of Int32)
        ''' <summary>納入予定累計数サプライヤー</summary>
        Private _Enoytca As Nullable(Of Int32)
        ''' <summary>出荷日サプライヤー</summary>
        Private _Eshym As Nullable(Of Int32)
        ''' <summary>出荷数累計サプライヤー</summary>
        Private _Eshca As Nullable(Of Int32)
        ''' <summary>到着日</summary>
        Private _Toym As Nullable(Of Int32)
        ''' <summary>到着数</summary>
        Private _Toca As Nullable(Of Int32)
        ''' <summary>到着累計</summary>
        Private _Torca As Nullable(Of Int32)
        ''' <summary>出荷指示日</summary>
        Private _Ssym As Nullable(Of Int32)
        ''' <summary>出荷指示数</summary>
        Private _Sssu As Nullable(Of Int32)
        ''' <summary>出荷指示数累計</summary>
        Private _Ssrsu As Nullable(Of Int32)
        ''' <summary>配達予定日</summary>
        Private _Htyoym As Nullable(Of Int32)
        ''' <summary>配達予定数</summary>
        Private _Htyoca As Nullable(Of Int32)
        ''' <summary>配達予定数累計</summary>
        Private _Htyorca As Nullable(Of Int32)
        ''' <summary>オーダーシート先行手配</summary>
        Private _Istflg As String
        ''' <summary>位置情報１</summary>
        Private _Ilscd As String
        ''' <summary>位置情報２</summary>
        Private _Ikomzba As String
        ''' <summary>位置情報３</summary>
        Private _Ikbba As String
        ''' <summary>部品代（実績）</summary>
        Private _Budai As Nullable(Of Int32)
        ''' <summary>支払確認（部品代）</summary>
        Private _Budflg As String
        ''' <summary>処理日（部品代）</summary>
        Private _Busoym As Nullable(Of Int32)
        ''' <summary>輸送費（実績）</summary>
        Private _Yudai As Nullable(Of Int32)
        ''' <summary>支払確認（輸送費）</summary>
        Private _Yudflg As String
        ''' <summary>処理日（輸送費）</summary>
        Private _Yusoym As Nullable(Of Int32)
        ''' <summary>ＢＵ区分</summary>
        Private _Bukbn As String
        ''' <summary>REV-NO（オーダーシート改訂№））</summary>
        Private _Orvifba As String
        ''' <summary>REV-NO（CANCEL）</summary>
        Private _Crvifba As String
        ''' <summary>作成年月日</summary>
        Private _Wtdta As Nullable(Of Int32)
        ''' <summary>作成時間</summary>
        Private _Wttim As Nullable(Of Int32)
        ''' <summary>更新年月日</summary>
        Private _Updta As Nullable(Of Int32)
        ''' <summary>更新時間</summary>
        Private _Uptim As Nullable(Of Int32)
        ''' <summary>プログラムID</summary>
        Private _Pgmid As String
        ''' <summary>作成ユーザーID</summary>
        Private _CreatedUserId As String
        ''' <summary>作成日</summary>
        Private _CreatedDate As String
        ''' <summary>作成時間</summary>
        Private _CreatedTime As String
        ''' <summary>更新ユーザーID</summary>
        Private _UpdatedUserId As String
        ''' <summary>更新日</summary>
        Private _UpdatedDate As String
        ''' <summary>更新時間</summary>
        Private _UpdatedTime As String


        ''' <summary>リストコード</summary>
        ''' <value>リストコード</value>
        ''' <returns>リストコード</returns>
        Public Property Lscd() As String
            Get
                Return _Lscd
            End Get
            Set(ByVal value As String)
                _Lscd = value
            End Set
        End Property

        ''' <summary>リクエスト№</summary>
        ''' <value>リクエスト№</value>
        ''' <returns>リクエスト№</returns>
        Public Property Reqno() As String
            Get
                Return _Reqno
            End Get
            Set(ByVal value As String)
                _Reqno = value
            End Set
        End Property

        ''' <summary>グループ№</summary>
        ''' <value>グループ№</value>
        ''' <returns>グループ№</returns>
        Public Property Grno() As String
            Get
                Return _Grno
            End Get
            Set(ByVal value As String)
                _Grno = value
            End Set
        End Property

        ''' <summary>シリアル№</summary>
        ''' <value>シリアル№</value>
        ''' <returns>シリアル№</returns>
        Public Property Srno() As String
            Get
                Return _Srno
            End Get
            Set(ByVal value As String)
                _Srno = value
            End Set
        End Property

        ''' <summary>工事指令書№</summary>
        ''' <value>工事指令書№</value>
        ''' <returns>工事指令書№</returns>
        Public Property Komzba() As String
            Get
                Return _Komzba
            End Get
            Set(ByVal value As String)
                _Komzba = value
            End Set
        End Property

        ''' <summary>ブロック№</summary>
        ''' <value>ブロック№</value>
        ''' <returns>ブロック№</returns>
        Public Property Bnba() As String
            Get
                Return _Bnba
            End Get
            Set(ByVal value As String)
                _Bnba = value
            End Set
        End Property

        ''' <summary>管理№</summary>
        ''' <value>管理№</value>
        ''' <returns>管理№</returns>
        Public Property Kbba() As String
            Get
                Return _Kbba
            End Get
            Set(ByVal value As String)
                _Kbba = value
            End Set
        End Property

        ''' <summary>行ID</summary>
        ''' <value>行ID</value>
        ''' <returns>行ID</returns>
        Public Property Gyoid() As String
            Get
                Return _Gyoid
            End Get
            Set(ByVal value As String)
                _Gyoid = value
            End Set
        End Property

        ''' <summary>発注年月０６</summary>
        ''' <value>発注年月０６</value>
        ''' <returns>発注年月０６</returns>
        Public Property Hayy06() As Nullable(Of Int32)
            Get
                Return _Hayy06
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Hayy06 = value
            End Set
        End Property

        ''' <summary>作業依頼書№</summary>
        ''' <value>作業依頼書№</value>
        ''' <returns>作業依頼書№</returns>
        Public Property Sgisba() As String
            Get
                Return _Sgisba
            End Get
            Set(ByVal value As String)
                _Sgisba = value
            End Set
        End Property

        ''' <summary>工事区分</summary>
        ''' <value>工事区分</value>
        ''' <returns>工事区分</returns>
        Public Property Kokm() As String
            Get
                Return _Kokm
            End Get
            Set(ByVal value As String)
                _Kokm = value
            End Set
        End Property

        ''' <summary>工事№</summary>
        ''' <value>工事№</value>
        ''' <returns>工事№</returns>
        Public Property Koba() As String
            Get
                Return _Koba
            End Get
            Set(ByVal value As String)
                _Koba = value
            End Set
        End Property

        ''' <summary>部品番号１５</summary>
        ''' <value>部品番号１５</value>
        ''' <returns>部品番号１５</returns>
        Public Property Buba15() As String
            Get
                Return _Buba15
            End Get
            Set(ByVal value As String)
                _Buba15 = value
            End Set
        End Property

        ''' <summary>部品名称</summary>
        ''' <value>部品名称</value>
        ''' <returns>部品名称</returns>
        Public Property Bunm() As String
            Get
                Return _Bunm
            End Get
            Set(ByVal value As String)
                _Bunm = value
            End Set
        End Property

        ''' <summary>設計要求日</summary>
        ''' <value>設計要求日</value>
        ''' <returns>設計要求日</returns>
        Public Property Sdyoym() As Nullable(Of Int32)
            Get
                Return _Sdyoym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Sdyoym = value
            End Set
        End Property

        ''' <summary>注文書№</summary>
        ''' <value>注文書№</value>
        ''' <returns>注文書№</returns>
        Public Property Cmba() As String
            Get
                Return _Cmba
            End Get
            Set(ByVal value As String)
                _Cmba = value
            End Set
        End Property

        ''' <summary>取引先コード</summary>
        ''' <value>取引先コード</value>
        ''' <returns>取引先コード</returns>
        Public Property Trcd() As String
            Get
                Return _Trcd
            End Get
            Set(ByVal value As String)
                _Trcd = value
            End Set
        End Property

        ''' <summary>供給セクション</summary>
        ''' <value>供給セクション</value>
        ''' <returns>供給セクション</returns>
        Public Property Kysx() As String
            Get
                Return _Kysx
            End Get
            Set(ByVal value As String)
                _Kysx = value
            End Set
        End Property

        ''' <summary>発注年月日</summary>
        ''' <value>発注年月日</value>
        ''' <returns>発注年月日</returns>
        Public Property Haym() As Nullable(Of Int32)
            Get
                Return _Haym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Haym = value
            End Set
        End Property

        ''' <summary>発注個数</summary>
        ''' <value>発注個数</value>
        ''' <returns>発注個数</returns>
        Public Property Haca() As Nullable(Of Int32)
            Get
                Return _Haca
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Haca = value
            End Set
        End Property

        ''' <summary>希望単価１０</summary>
        ''' <value>希望単価１０</value>
        ''' <returns>希望単価１０</returns>
        Public Property Bota10() As Nullable(Of Int32)
            Get
                Return _Bota10
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Bota10 = value
            End Set
        End Property

        ''' <summary>納入場所</summary>
        ''' <value>納入場所</value>
        ''' <returns>納入場所</returns>
        Public Property Nobs() As String
            Get
                Return _Nobs
            End Get
            Set(ByVal value As String)
                _Nobs = value
            End Set
        End Property

        ''' <summary>納入区分</summary>
        ''' <value>納入区分</value>
        ''' <returns>納入区分</returns>
        Public Property Nokm() As String
            Get
                Return _Nokm
            End Get
            Set(ByVal value As String)
                _Nokm = value
            End Set
        End Property

        ''' <summary>取消年月日</summary>
        ''' <value>取消年月日</value>
        ''' <returns>取消年月日</returns>
        Public Property Tlym() As Nullable(Of Int32)
            Get
                Return _Tlym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Tlym = value
            End Set
        End Property

        ''' <summary>取消数</summary>
        ''' <value>取消数</value>
        ''' <returns>取消数</returns>
        Public Property Tlca() As Nullable(Of Int32)
            Get
                Return _Tlca
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Tlca = value
            End Set
        End Property

        ''' <summary>取消フラグ</summary>
        ''' <value>取消フラグ</value>
        ''' <returns>取消フラグ</returns>
        Public Property Tlflg() As String
            Get
                Return _Tlflg
            End Get
            Set(ByVal value As String)
                _Tlflg = value
            End Set
        End Property

        ''' <summary>購坦</summary>
        ''' <value>購坦</value>
        ''' <returns>購坦</returns>
        Public Property Ka() As String
            Get
                Return _Ka
            End Get
            Set(ByVal value As String)
                _Ka = value
            End Set
        End Property

        ''' <summary>購入単価１０</summary>
        ''' <value>購入単価１０</value>
        ''' <returns>購入単価１０</returns>
        Public Property Kota10() As Nullable(Of Int32)
            Get
                Return _Kota10
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Kota10 = value
            End Set
        End Property

        ''' <summary>支給単価１０</summary>
        ''' <value>支給単価１０</value>
        ''' <returns>支給単価１０</returns>
        Public Property Sita10() As Nullable(Of Int32)
            Get
                Return _Sita10
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Sita10 = value
            End Set
        End Property

        ''' <summary>支払済フラグ</summary>
        ''' <value>支払済フラグ</value>
        ''' <returns>支払済フラグ</returns>
        Public Property Shflg() As String
            Get
                Return _Shflg
            End Get
            Set(ByVal value As String)
                _Shflg = value
            End Set
        End Property

        ''' <summary>支払日</summary>
        ''' <value>支払日</value>
        ''' <returns>支払日</returns>
        Public Property Sihady() As Nullable(Of Int32)
            Get
                Return _Sihady
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Sihady = value
            End Set
        End Property

        ''' <summary>ユニット区分</summary>
        ''' <value>ユニット区分</value>
        ''' <returns>ユニット区分</returns>
        Public Property Unkm() As String
            Get
                Return _Unkm
            End Get
            Set(ByVal value As String)
                _Unkm = value
            End Set
        End Property

        ''' <summary>納入年月日</summary>
        ''' <value>納入年月日</value>
        ''' <returns>納入年月日</returns>
        Public Property Noym() As Nullable(Of Int32)
            Get
                Return _Noym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Noym = value
            End Set
        End Property

        ''' <summary>納入累計数</summary>
        ''' <value>納入累計数</value>
        ''' <returns>納入累計数</returns>
        Public Property Noru() As Nullable(Of Int32)
            Get
                Return _Noru
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Noru = value
            End Set
        End Property

        ''' <summary>納入残数</summary>
        ''' <value>納入残数</value>
        ''' <returns>納入残数</returns>
        Public Property Nozu() As Nullable(Of Int32)
            Get
                Return _Nozu
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Nozu = value
            End Set
        End Property

        ''' <summary>レベル</summary>
        ''' <value>レベル</value>
        ''' <returns>レベル</returns>
        Public Property Lv() As Nullable(Of Int32)
            Get
                Return _Lv
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Lv = value
            End Set
        End Property

        ''' <summary>専用マーク</summary>
        ''' <value>専用マーク</value>
        ''' <returns>専用マーク</returns>
        Public Property Aamq() As String
            Get
                Return _Aamq
            End Get
            Set(ByVal value As String)
                _Aamq = value
            End Set
        End Property

        ''' <summary>改訂№</summary>
        ''' <value>改訂№</value>
        ''' <returns>改訂№</returns>
        Public Property Kdba() As String
            Get
                Return _Kdba
            End Get
            Set(ByVal value As String)
                _Kdba = value
            End Set
        End Property

        ''' <summary>設通№</summary>
        ''' <value>設通№</value>
        ''' <returns>設通№</returns>
        Public Property Stba() As String
            Get
                Return _Stba
            End Get
            Set(ByVal value As String)
                _Stba = value
            End Set
        End Property

        ''' <summary>バーコード情報</summary>
        ''' <value>バーコード情報</value>
        ''' <returns>バーコード情報</returns>
        Public Property Brno() As String
            Get
                Return _Brno
            End Get
            Set(ByVal value As String)
                _Brno = value
            End Set
        End Property

        ''' <summary>輸送会社</summary>
        ''' <value>輸送会社</value>
        ''' <returns>輸送会社</returns>
        Public Property Yusona() As String
            Get
                Return _Yusona
            End Get
            Set(ByVal value As String)
                _Yusona = value
            End Set
        End Property

        ''' <summary>現地供給セクション</summary>
        ''' <value>現地供給セクション</value>
        ''' <returns>現地供給セクション</returns>
        Public Property Gkyna() As String
            Get
                Return _Gkyna
            End Get
            Set(ByVal value As String)
                _Gkyna = value
            End Set
        End Property

        ''' <summary>配送日</summary>
        ''' <value>配送日</value>
        ''' <returns>配送日</returns>
        Public Property Hsdy() As Nullable(Of Int32)
            Get
                Return _Hsdy
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Hsdy = value
            End Set
        End Property

        ''' <summary>配送先名称</summary>
        ''' <value>配送先名称</value>
        ''' <returns>配送先名称</returns>
        Public Property Hsna() As String
            Get
                Return _Hsna
            End Get
            Set(ByVal value As String)
                _Hsna = value
            End Set
        End Property

        ''' <summary>SIA備考</summary>
        ''' <value>SIA備考</value>
        ''' <returns>SIA備考</returns>
        Public Property Siabk() As String
            Get
                Return _Siabk
            End Get
            Set(ByVal value As String)
                _Siabk = value
            End Set
        End Property

        ''' <summary>オーダーシート発行日</summary>
        ''' <value>オーダーシート発行日</value>
        ''' <returns>オーダーシート発行日</returns>
        Public Property Odhcdy() As Nullable(Of Int32)
            Get
                Return _Odhcdy
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Odhcdy = value
            End Set
        End Property

        ''' <summary>データインポート日時</summary>
        ''' <value>データインポート日時</value>
        ''' <returns>データインポート日時</returns>
        Public Property Eipym() As String
            Get
                Return _Eipym
            End Get
            Set(ByVal value As String)
                _Eipym = value
            End Set
        End Property

        ''' <summary>D/O№</summary>
        ''' <value>D/O№</value>
        ''' <returns>D/O№</returns>
        Public Property Edono() As String
            Get
                Return _Edono
            End Get
            Set(ByVal value As String)
                _Edono = value
            End Set
        End Property

        ''' <summary>EVENTRA発注処理日</summary>
        ''' <value>EVENTRA発注処理日</value>
        ''' <returns>EVENTRA発注処理日</returns>
        Public Property Eevenym() As Nullable(Of Int32)
            Get
                Return _Eevenym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Eevenym = value
            End Set
        End Property

        ''' <summary>納入指示日サプライヤー</summary>
        ''' <value>納入指示日サプライヤー</value>
        ''' <returns>納入指示日サプライヤー</returns>
        Public Property Enoym() As Nullable(Of Int32)
            Get
                Return _Enoym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Enoym = value
            End Set
        End Property

        ''' <summary>取引先コードサプライヤー</summary>
        ''' <value>取引先コードサプライヤー</value>
        ''' <returns>取引先コードサプライヤー</returns>
        Public Property Etrcd() As String
            Get
                Return _Etrcd
            End Get
            Set(ByVal value As String)
                _Etrcd = value
            End Set
        End Property

        ''' <summary>取消日サプライヤー</summary>
        ''' <value>取消日サプライヤー</value>
        ''' <returns>取消日サプライヤー</returns>
        Public Property Etlym() As Nullable(Of Int32)
            Get
                Return _Etlym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Etlym = value
            End Set
        End Property

        ''' <summary>取消累計数サプライヤー</summary>
        ''' <value>取消累計数サプライヤー</value>
        ''' <returns>取消累計数サプライヤー</returns>
        Public Property Etlca() As Nullable(Of Int32)
            Get
                Return _Etlca
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Etlca = value
            End Set
        End Property

        ''' <summary>取消フラグサプライヤー</summary>
        ''' <value>取消フラグサプライヤー</value>
        ''' <returns>取消フラグサプライヤー</returns>
        Public Property Etlflg() As String
            Get
                Return _Etlflg
            End Get
            Set(ByVal value As String)
                _Etlflg = value
            End Set
        End Property

        ''' <summary>現調品購入単価サプライヤー</summary>
        ''' <value>現調品購入単価サプライヤー</value>
        ''' <returns>現調品購入単価サプライヤー</returns>
        Public Property Egkota10() As Nullable(Of Int32)
            Get
                Return _Egkota10
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Egkota10 = value
            End Set
        End Property

        ''' <summary>納期回答日サプライヤー</summary>
        ''' <value>納期回答日サプライヤー</value>
        ''' <returns>納期回答日サプライヤー</returns>
        Public Property Enqans() As Nullable(Of Int32)
            Get
                Return _Enqans
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Enqans = value
            End Set
        End Property

        ''' <summary>納入予定累計数サプライヤー</summary>
        ''' <value>納入予定累計数サプライヤー</value>
        ''' <returns>納入予定累計数サプライヤー</returns>
        Public Property Enoytca() As Nullable(Of Int32)
            Get
                Return _Enoytca
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Enoytca = value
            End Set
        End Property

        ''' <summary>出荷日サプライヤー</summary>
        ''' <value>出荷日サプライヤー</value>
        ''' <returns>出荷日サプライヤー</returns>
        Public Property Eshym() As Nullable(Of Int32)
            Get
                Return _Eshym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Eshym = value
            End Set
        End Property

        ''' <summary>出荷数累計サプライヤー</summary>
        ''' <value>出荷数累計サプライヤー</value>
        ''' <returns>出荷数累計サプライヤー</returns>
        Public Property Eshca() As Nullable(Of Int32)
            Get
                Return _Eshca
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Eshca = value
            End Set
        End Property

        ''' <summary>到着日</summary>
        ''' <value>到着日</value>
        ''' <returns>到着日</returns>
        Public Property Toym() As Nullable(Of Int32)
            Get
                Return _Toym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Toym = value
            End Set
        End Property

        ''' <summary>到着数</summary>
        ''' <value>到着数</value>
        ''' <returns>到着数</returns>
        Public Property Toca() As Nullable(Of Int32)
            Get
                Return _Toca
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Toca = value
            End Set
        End Property

        ''' <summary>到着累計</summary>
        ''' <value>到着累計</value>
        ''' <returns>到着累計</returns>
        Public Property Torca() As Nullable(Of Int32)
            Get
                Return _Torca
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Torca = value
            End Set
        End Property

        ''' <summary>出荷指示日</summary>
        ''' <value>出荷指示日</value>
        ''' <returns>出荷指示日</returns>
        Public Property Ssym() As Nullable(Of Int32)
            Get
                Return _Ssym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ssym = value
            End Set
        End Property

        ''' <summary>出荷指示数</summary>
        ''' <value>出荷指示数</value>
        ''' <returns>出荷指示数</returns>
        Public Property Sssu() As Nullable(Of Int32)
            Get
                Return _Sssu
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Sssu = value
            End Set
        End Property

        ''' <summary>出荷指示数累計</summary>
        ''' <value>出荷指示数累計</value>
        ''' <returns>出荷指示数累計</returns>
        Public Property Ssrsu() As Nullable(Of Int32)
            Get
                Return _Ssrsu
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ssrsu = value
            End Set
        End Property

        ''' <summary>配達予定日</summary>
        ''' <value>配達予定日</value>
        ''' <returns>配達予定日</returns>
        Public Property Htyoym() As Nullable(Of Int32)
            Get
                Return _Htyoym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Htyoym = value
            End Set
        End Property

        ''' <summary>配達予定数</summary>
        ''' <value>配達予定数</value>
        ''' <returns>配達予定数</returns>
        Public Property Htyoca() As Nullable(Of Int32)
            Get
                Return _Htyoca
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Htyoca = value
            End Set
        End Property

        ''' <summary>配達予定数累計</summary>
        ''' <value>配達予定数累計</value>
        ''' <returns>配達予定数累計</returns>
        Public Property Htyorca() As Nullable(Of Int32)
            Get
                Return _Htyorca
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Htyorca = value
            End Set
        End Property

        ''' <summary>オーダーシート先行手配</summary>
        ''' <value>オーダーシート先行手配</value>
        ''' <returns>オーダーシート先行手配</returns>
        Public Property Istflg() As String
            Get
                Return _Istflg
            End Get
            Set(ByVal value As String)
                _Istflg = value
            End Set
        End Property

        ''' <summary>位置情報１</summary>
        ''' <value>位置情報１</value>
        ''' <returns>位置情報１</returns>
        Public Property Ilscd() As String
            Get
                Return _Ilscd
            End Get
            Set(ByVal value As String)
                _Ilscd = value
            End Set
        End Property

        ''' <summary>位置情報２</summary>
        ''' <value>位置情報２</value>
        ''' <returns>位置情報２</returns>
        Public Property Ikomzba() As String
            Get
                Return _Ikomzba
            End Get
            Set(ByVal value As String)
                _Ikomzba = value
            End Set
        End Property

        ''' <summary>位置情報３</summary>
        ''' <value>位置情報３</value>
        ''' <returns>位置情報３</returns>
        Public Property Ikbba() As String
            Get
                Return _Ikbba
            End Get
            Set(ByVal value As String)
                _Ikbba = value
            End Set
        End Property

        ''' <summary>部品代（実績）</summary>
        ''' <value>部品代（実績）</value>
        ''' <returns>部品代（実績）</returns>
        Public Property Budai() As Nullable(Of Int32)
            Get
                Return _Budai
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Budai = value
            End Set
        End Property

        ''' <summary>支払確認（部品代）</summary>
        ''' <value>支払確認（部品代）</value>
        ''' <returns>支払確認（部品代）</returns>
        Public Property Budflg() As String
            Get
                Return _Budflg
            End Get
            Set(ByVal value As String)
                _Budflg = value
            End Set
        End Property

        ''' <summary>処理日（部品代）</summary>
        ''' <value>処理日（部品代）</value>
        ''' <returns>処理日（部品代）</returns>
        Public Property Busoym() As Nullable(Of Int32)
            Get
                Return _Busoym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Busoym = value
            End Set
        End Property

        ''' <summary>輸送費（実績）</summary>
        ''' <value>輸送費（実績）</value>
        ''' <returns>輸送費（実績）</returns>
        Public Property Yudai() As Nullable(Of Int32)
            Get
                Return _Yudai
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Yudai = value
            End Set
        End Property

        ''' <summary>支払確認（輸送費）</summary>
        ''' <value>支払確認（輸送費）</value>
        ''' <returns>支払確認（輸送費）</returns>
        Public Property Yudflg() As String
            Get
                Return _Yudflg
            End Get
            Set(ByVal value As String)
                _Yudflg = value
            End Set
        End Property

        ''' <summary>処理日（輸送費）</summary>
        ''' <value>処理日（輸送費）</value>
        ''' <returns>処理日（輸送費）</returns>
        Public Property Yusoym() As Nullable(Of Int32)
            Get
                Return _Yusoym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Yusoym = value
            End Set
        End Property

        ''' <summary>ＢＵ区分</summary>
        ''' <value>ＢＵ区分</value>
        ''' <returns>ＢＵ区分</returns>
        Public Property Bukbn() As String
            Get
                Return _Bukbn
            End Get
            Set(ByVal value As String)
                _Bukbn = value
            End Set
        End Property

        ''' <summary>REV-NO（オーダーシート改訂№））</summary>
        ''' <value>REV-NO（オーダーシート改訂№））</value>
        ''' <returns>REV-NO（オーダーシート改訂№））</returns>
        Public Property Orvifba() As String
            Get
                Return _Orvifba
            End Get
            Set(ByVal value As String)
                _Orvifba = value
            End Set
        End Property

        ''' <summary>REV-NO（CANCEL）</summary>
        ''' <value>REV-NO（CANCEL）</value>
        ''' <returns>REV-NO（CANCEL）</returns>
        Public Property Crvifba() As String
            Get
                Return _Crvifba
            End Get
            Set(ByVal value As String)
                _Crvifba = value
            End Set
        End Property

        ''' <summary>作成年月日</summary>
        ''' <value>作成年月日</value>
        ''' <returns>作成年月日</returns>
        Public Property Wtdta() As Nullable(Of Int32)
            Get
                Return _Wtdta
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Wtdta = value
            End Set
        End Property

        ''' <summary>作成時間</summary>
        ''' <value>作成時間</value>
        ''' <returns>作成時間</returns>
        Public Property Wttim() As Nullable(Of Int32)
            Get
                Return _Wttim
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Wttim = value
            End Set
        End Property

        ''' <summary>更新年月日</summary>
        ''' <value>更新年月日</value>
        ''' <returns>更新年月日</returns>
        Public Property Updta() As Nullable(Of Int32)
            Get
                Return _Updta
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Updta = value
            End Set
        End Property

        ''' <summary>更新時間</summary>
        ''' <value>更新時間</value>
        ''' <returns>更新時間</returns>
        Public Property Uptim() As Nullable(Of Int32)
            Get
                Return _Uptim
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Uptim = value
            End Set
        End Property

        ''' <summary>プログラムID</summary>
        ''' <value>プログラムID</value>
        ''' <returns>プログラムID</returns>
        Public Property Pgmid() As String
            Get
                Return _Pgmid
            End Get
            Set(ByVal value As String)
                _Pgmid = value
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