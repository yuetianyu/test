Namespace Db.EBom.Vo
    ''' <summary>
    ''' 発注納入状況ファイル情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AsORPF32Vo

        ''' <summary>発注年月０６</summary>
        Private _Hayy06 As Nullable(Of Int32)
        ''' <summary>作業依頼書№</summary>
        Private _Sgisba As String
        ''' <summary>管理№</summary>
        Private _Kbba As String
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
        ''' <summary>納入年月日</summary>
        Private _Noym As Nullable(Of Int32)
        ''' <summary>納入累計数</summary>
        Private _Noru As Nullable(Of Int32)
        ''' <summary>納入残数</summary>
        Private _Nozu As Nullable(Of Int32)
        ''' <summary>納入場所</summary>
        Private _Nobs As String
        ''' <summary>納入区分</summary>
        Private _Nokm As String
        ''' <summary>取消年月日</summary>
        Private _Tlym As Nullable(Of Int32)
        ''' <summary>取消数</summary>
        Private _Tlca As Nullable(Of Int32)
        ''' <summary>納期回答１</summary>
        Private _Nqans1 As Nullable(Of Int32)
        ''' <summary>予定数１</summary>
        Private _Ytca1 As Nullable(Of Int32)
        ''' <summary>納期回答２</summary>
        Private _Nqans2 As Nullable(Of Int32)
        ''' <summary>予定数２</summary>
        Private _Ytca2 As Nullable(Of Int32)
        ''' <summary>備考</summary>
        Private _Biko As String
        ''' <summary>記事欄</summary>
        Private _Kk As String
        ''' <summary>購坦</summary>
        Private _Ka As String
        ''' <summary>発注区分</summary>
        Private _Hakm As String
        ''' <summary>決定単価</summary>
        Private _Fk3ta As Nullable(Of Int32)
        ''' <summary>検収単価</summary>
        Private _Gkuta As Nullable(Of Int32)
        ''' <summary>検収日</summary>
        Private _Cjksdy As Nullable(Of Int32)
        ''' <summary>手配記号</summary>
        Private _Tikg As String
        ''' <summary>ロック№</summary>
        Private _Bnba As String
        ''' <summary>購入希望部品費</summary>
        Private _Yjcl As Nullable(Of Int32)
        ''' <summary>購入希望型費</summary>
        Private _Yjwh As Nullable(Of Int32)
        ''' <summary>購入希望支給品代</summary>
        Private _Whmg As Nullable(Of Int32)
        ''' <summary>取引先部品費</summary>
        Private _Myjcl As Nullable(Of Int32)
        ''' <summary>取引先型費</summary>
        Private _Myjwh As Nullable(Of Int32)
        ''' <summary>取引先見積支給品代</summary>
        Private _Mwhmg As Nullable(Of Int32)
        ''' <summary>取引先見積単価</summary>
        Private _Mota10 As Nullable(Of Int32)
        ''' <summary>検収予定部品費</summary>
        Private _Kyjcl As Nullable(Of Int32)
        ''' <summary>検収予定型費</summary>
        Private _Kyjwh As Nullable(Of Int32)
        ''' <summary>検収予定支給品代</summary>
        Private _Kwhmg As Nullable(Of Int32)
        ''' <summary>検収予定単価</summary>
        Private _Kota10 As Nullable(Of Int32)
        ''' <summary>審議</summary>
        Private _Shingi As String
        ''' <summary>理由</summary>
        Private _Riyuu As String
        ''' <summary>状態</summary>
        Private _Jotai As String
        ''' <summary>承認日</summary>
        Private _Symd As Nullable(Of Int32)
        ''' <summary>検収システム対象</summary>
        Private _Taisho As String
        ''' <summary>工法</summary>
        Private _Koho As Nullable(Of Int32)
        ''' <summary>支給単価１０</summary>
        Private _Sita10 As Nullable(Of Int32)
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

        ''' <summary>納期回答１</summary>
        ''' <value>納期回答１</value>
        ''' <returns>納期回答１</returns>
        Public Property Nqans1() As Nullable(Of Int32)
            Get
                Return _Nqans1
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Nqans1 = value
            End Set
        End Property

        ''' <summary>予定数１</summary>
        ''' <value>予定数１</value>
        ''' <returns>予定数１</returns>
        Public Property Ytca1() As Nullable(Of Int32)
            Get
                Return _Ytca1
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ytca1 = value
            End Set
        End Property

        ''' <summary>納期回答２</summary>
        ''' <value>納期回答２</value>
        ''' <returns>納期回答２</returns>
        Public Property Nqans2() As Nullable(Of Int32)
            Get
                Return _Nqans2
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Nqans2 = value
            End Set
        End Property

        ''' <summary>予定数２</summary>
        ''' <value>予定数２</value>
        ''' <returns>予定数２</returns>
        Public Property Ytca2() As Nullable(Of Int32)
            Get
                Return _Ytca2
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Ytca2 = value
            End Set
        End Property

        ''' <summary>備考</summary>
        ''' <value>備考</value>
        ''' <returns>備考</returns>
        Public Property Biko() As String
            Get
                Return _Biko
            End Get
            Set(ByVal value As String)
                _Biko = value
            End Set
        End Property

        ''' <summary>記事欄</summary>
        ''' <value>記事欄</value>
        ''' <returns>記事欄</returns>
        Public Property Kk() As String
            Get
                Return _Kk
            End Get
            Set(ByVal value As String)
                _Kk = value
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

        ''' <summary>発注区分</summary>
        ''' <value>発注区分</value>
        ''' <returns>発注区分</returns>
        Public Property Hakm() As String
            Get
                Return _Hakm
            End Get
            Set(ByVal value As String)
                _Hakm = value
            End Set
        End Property

        ''' <summary>決定単価</summary>
        ''' <value>決定単価</value>
        ''' <returns>決定単価</returns>
        Public Property Fk3ta() As Nullable(Of Int32)
            Get
                Return _Fk3ta
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Fk3ta = value
            End Set
        End Property

        ''' <summary>検収単価</summary>
        ''' <value>検収単価</value>
        ''' <returns>検収単価</returns>
        Public Property Gkuta() As Nullable(Of Int32)
            Get
                Return _Gkuta
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Gkuta = value
            End Set
        End Property

        ''' <summary>検収日</summary>
        ''' <value>検収日</value>
        ''' <returns>検収日</returns>
        Public Property Cjksdy() As Nullable(Of Int32)
            Get
                Return _Cjksdy
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Cjksdy = value
            End Set
        End Property

        ''' <summary>手配記号</summary>
        ''' <value>手配記号</value>
        ''' <returns>手配記号</returns>
        Public Property Tikg() As String
            Get
                Return _Tikg
            End Get
            Set(ByVal value As String)
                _Tikg = value
            End Set
        End Property

        ''' <summary>ロック№</summary>
        ''' <value>ロック№</value>
        ''' <returns>ロック№</returns>
        Public Property Bnba() As String
            Get
                Return _Bnba
            End Get
            Set(ByVal value As String)
                _Bnba = value
            End Set
        End Property

        ''' <summary>購入希望部品費</summary>
        ''' <value>購入希望部品費</value>
        ''' <returns>購入希望部品費</returns>
        Public Property Yjcl() As Nullable(Of Int32)
            Get
                Return _Yjcl
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Yjcl = value
            End Set
        End Property

        ''' <summary>購入希望型費</summary>
        ''' <value>購入希望型費</value>
        ''' <returns>購入希望型費</returns>
        Public Property Yjwh() As Nullable(Of Int32)
            Get
                Return _Yjwh
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Yjwh = value
            End Set
        End Property

        ''' <summary>購入希望支給品代</summary>
        ''' <value>購入希望支給品代</value>
        ''' <returns>購入希望支給品代</returns>
        Public Property Whmg() As Nullable(Of Int32)
            Get
                Return _Whmg
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Whmg = value
            End Set
        End Property

        ''' <summary>取引先部品費</summary>
        ''' <value>取引先部品費</value>
        ''' <returns>取引先部品費</returns>
        Public Property Myjcl() As Nullable(Of Int32)
            Get
                Return _Myjcl
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Myjcl = value
            End Set
        End Property

        ''' <summary>取引先型費</summary>
        ''' <value>取引先型費</value>
        ''' <returns>取引先型費</returns>
        Public Property Myjwh() As Nullable(Of Int32)
            Get
                Return _Myjwh
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Myjwh = value
            End Set
        End Property

        ''' <summary>取引先見積支給品代</summary>
        ''' <value>取引先見積支給品代</value>
        ''' <returns>取引先見積支給品代</returns>
        Public Property Mwhmg() As Nullable(Of Int32)
            Get
                Return _Mwhmg
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Mwhmg = value
            End Set
        End Property

        ''' <summary>取引先見積単価</summary>
        ''' <value>取引先見積単価</value>
        ''' <returns>取引先見積単価</returns>
        Public Property Mota10() As Nullable(Of Int32)
            Get
                Return _Mota10
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Mota10 = value
            End Set
        End Property

        ''' <summary>検収予定部品費</summary>
        ''' <value>検収予定部品費</value>
        ''' <returns>検収予定部品費</returns>
        Public Property Kyjcl() As Nullable(Of Int32)
            Get
                Return _Kyjcl
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Kyjcl = value
            End Set
        End Property

        ''' <summary>検収予定型費</summary>
        ''' <value>検収予定型費</value>
        ''' <returns>検収予定型費</returns>
        Public Property Kyjwh() As Nullable(Of Int32)
            Get
                Return _Kyjwh
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Kyjwh = value
            End Set
        End Property

        ''' <summary>検収予定支給品代</summary>
        ''' <value>検収予定支給品代</value>
        ''' <returns>検収予定支給品代</returns>
        Public Property Kwhmg() As Nullable(Of Int32)
            Get
                Return _Kwhmg
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Kwhmg = value
            End Set
        End Property

        ''' <summary>検収予定単価</summary>
        ''' <value>検収予定単価</value>
        ''' <returns>検収予定単価</returns>
        Public Property Kota10() As Nullable(Of Int32)
            Get
                Return _Kota10
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Kota10 = value
            End Set
        End Property

        ''' <summary>審議</summary>
        ''' <value>審議</value>
        ''' <returns>審議</returns>
        Public Property Shingi() As String
            Get
                Return _Shingi
            End Get
            Set(ByVal value As String)
                _Shingi = value
            End Set
        End Property

        ''' <summary>理由</summary>
        ''' <value>理由</value>
        ''' <returns>理由</returns>
        Public Property Riyuu() As String
            Get
                Return _Riyuu
            End Get
            Set(ByVal value As String)
                _Riyuu = value
            End Set
        End Property

        ''' <summary>状態</summary>
        ''' <value>状態</value>
        ''' <returns>状態</returns>
        Public Property Jotai() As String
            Get
                Return _Jotai
            End Get
            Set(ByVal value As String)
                _Jotai = value
            End Set
        End Property

        ''' <summary>承認日</summary>
        ''' <value>承認日</value>
        ''' <returns>承認日</returns>
        Public Property Symd() As Nullable(Of Int32)
            Get
                Return _Symd
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Symd = value
            End Set
        End Property

        ''' <summary>検収システム対象</summary>
        ''' <value>検収システム対象</value>
        ''' <returns>検収システム対象</returns>
        Public Property Taisho() As String
            Get
                Return _Taisho
            End Get
            Set(ByVal value As String)
                _Taisho = value
            End Set
        End Property

        ''' <summary>工法</summary>
        ''' <value>工法</value>
        ''' <returns>工法</returns>
        Public Property Koho() As Nullable(Of Int32)
            Get
                Return _Koho
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Koho = value
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