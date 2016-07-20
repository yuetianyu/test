Namespace Db.EBom.Vo
    ''' <summary>
    ''' ３ヶ月インフォメーション情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AsKPSM10PVo

        ''' <summary>工事指令№</summary>
        Private _Komzba As String
        ''' <summary>連番</summary>
        Private _Rnno As Nullable(Of Int32)
        ''' <summary>リスト分類</summary>
        Private _Ltbn As String
        ''' <summary>取引先コード</summary>
        Private _Trcd As String
        ''' <summary>製品区分</summary>
        Private _Snkm As String
        ''' <summary>部品番号</summary>
        Private _Buba15 As String
        ''' <summary>内外区分</summary>
        Private _Ngkm As String
        ''' <summary>納入ライン区</summary>
        Private _Nolkm As String
        ''' <summary>要求元</summary>
        Private _Ykmt As String
        ''' <summary>納入方式</summary>
        Private _Noho As String
        ''' <summary>部品層別区分</summary>
        Private _Busokm As String
        ''' <summary>デポ区分</summary>
        Private _Depo As String
        ''' <summary>余白１</summary>
        Private _Yohk1 As String
        ''' <summary>工場区分</summary>
        Private _Kjkm As String
        ''' <summary>生坦</summary>
        Private _Setn As String
        ''' <summary>部品番号１２</summary>
        Private _Buba12 As String
        ''' <summary>部品名称</summary>
        Private _Bunm As String
        ''' <summary>購坦</summary>
        Private _Ka As String
        ''' <summary>発注区分</summary>
        Private _Hakm As String
        ''' <summary>重保</summary>
        Private _Juho As String
        ''' <summary>新廃区分</summary>
        Private _Shkm As String
        ''' <summary>打切マーク</summary>
        Private _Uchmk As String
        ''' <summary>ＳＮＰ</summary>
        Private _Snp As String
        ''' <summary>納入ロット</summary>
        Private _Norot As String
        ''' <summary>打切余月</summary>
        Private _Uchym As String
        ''' <summary>検査方式</summary>
        Private _Knsk As String
        ''' <summary>検査場所</summary>
        Private _Knbs As String
        ''' <summary>納入場所</summary>
        Private _Nobs As String
        ''' <summary>供給担当</summary>
        Private _Kytn As String
        ''' <summary>先行度</summary>
        Private _Snko As String
        ''' <summary>分割コード</summary>
        Private _Bncd As String
        ''' <summary>サイクル</summary>
        Private _Cycl As String
        ''' <summary>予測区分</summary>
        Private _Yskkm As String
        ''' <summary>納入区分</summary>
        Private _Nokm As String
        ''' <summary>支給区分</summary>
        Private _Sikm As String
        ''' <summary>供給セクション</summary>
        Private _Kysx As String
        ''' <summary>発注数－前月</summary>
        Private _Hazen As Nullable(Of Int32)
        ''' <summary>発注数－非ＯＥＳ</summary>
        Private _Hanoes As Nullable(Of Int32)
        ''' <summary>発注数－基礎数</summary>
        Private _Hakiso As Nullable(Of Int32)
        ''' <summary>発注数－変動数</summary>
        Private _Hahen As Nullable(Of Int32)
        ''' <summary>発注数－次月</summary>
        Private _Haji As Nullable(Of Int32)
        ''' <summary>発注数－次々月</summary>
        Private _Hajiji As Nullable(Of Int32)
        ''' <summary>処理対象－年</summary>
        Private _Sriy As Nullable(Of Int32)
        ''' <summary>処理対象－月</summary>
        Private _Srim As Nullable(Of Int32)
        ''' <summary>前日比</summary>
        Private _Znhi As Nullable(Of Int32)
        ''' <summary>小梱包区分</summary>
        Private _Sknkm As String
        ''' <summary>防錆区分</summary>
        Private _Bsikm As String
        ''' <summary>ユニット製品区分</summary>
        Private _Utkm As String
        ''' <summary>バンクストック数</summary>
        Private _Bnkstk As String
        ''' <summary>繰越し数</summary>
        Private _Krks As String
        ''' <summary>取引店名</summary>
        Private _Trnm As String
        ''' <summary>梱包工場新設区分</summary>
        Private _Kpkosn As String
        ''' <summary>出荷調達区分</summary>
        Private _Sucho As String
        ''' <summary>余白５</summary>
        Private _Yohk5 As String
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


        ''' <summary>工事指令№</summary>
        ''' <value>工事指令№</value>
        ''' <returns>工事指令№</returns>
        Public Property Komzba() As String
            Get
                Return _Komzba
            End Get
            Set(ByVal value As String)
                _Komzba = value
            End Set
        End Property

        ''' <summary>連番</summary>
        ''' <value>連番</value>
        ''' <returns>連番</returns>
        Public Property Rnno() As Nullable(Of Int32)
            Get
                Return _Rnno
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Rnno = value
            End Set
        End Property

        ''' <summary>リスト分類</summary>
        ''' <value>リスト分類</value>
        ''' <returns>リスト分類</returns>
        Public Property Ltbn() As String
            Get
                Return _Ltbn
            End Get
            Set(ByVal value As String)
                _Ltbn = value
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

        ''' <summary>製品区分</summary>
        ''' <value>製品区分</value>
        ''' <returns>製品区分</returns>
        Public Property Snkm() As String
            Get
                Return _Snkm
            End Get
            Set(ByVal value As String)
                _Snkm = value
            End Set
        End Property

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public Property Buba15() As String
            Get
                Return _Buba15
            End Get
            Set(ByVal value As String)
                _Buba15 = value
            End Set
        End Property

        ''' <summary>内外区分</summary>
        ''' <value>内外区分</value>
        ''' <returns>内外区分</returns>
        Public Property Ngkm() As String
            Get
                Return _Ngkm
            End Get
            Set(ByVal value As String)
                _Ngkm = value
            End Set
        End Property

        ''' <summary>納入ライン区</summary>
        ''' <value>納入ライン区</value>
        ''' <returns>納入ライン区</returns>
        Public Property Nolkm() As String
            Get
                Return _Nolkm
            End Get
            Set(ByVal value As String)
                _Nolkm = value
            End Set
        End Property

        ''' <summary>要求元</summary>
        ''' <value>要求元</value>
        ''' <returns>要求元</returns>
        Public Property Ykmt() As String
            Get
                Return _Ykmt
            End Get
            Set(ByVal value As String)
                _Ykmt = value
            End Set
        End Property

        ''' <summary>納入方式</summary>
        ''' <value>納入方式</value>
        ''' <returns>納入方式</returns>
        Public Property Noho() As String
            Get
                Return _Noho
            End Get
            Set(ByVal value As String)
                _Noho = value
            End Set
        End Property

        ''' <summary>部品層別区分</summary>
        ''' <value>部品層別区分</value>
        ''' <returns>部品層別区分</returns>
        Public Property Busokm() As String
            Get
                Return _Busokm
            End Get
            Set(ByVal value As String)
                _Busokm = value
            End Set
        End Property

        ''' <summary>デポ区分</summary>
        ''' <value>デポ区分</value>
        ''' <returns>デポ区分</returns>
        Public Property Depo() As String
            Get
                Return _Depo
            End Get
            Set(ByVal value As String)
                _Depo = value
            End Set
        End Property

        ''' <summary>余白１</summary>
        ''' <value>余白１</value>
        ''' <returns>余白１</returns>
        Public Property Yohk1() As String
            Get
                Return _Yohk1
            End Get
            Set(ByVal value As String)
                _Yohk1 = value
            End Set
        End Property

        ''' <summary>工場区分</summary>
        ''' <value>工場区分</value>
        ''' <returns>工場区分</returns>
        Public Property Kjkm() As String
            Get
                Return _Kjkm
            End Get
            Set(ByVal value As String)
                _Kjkm = value
            End Set
        End Property

        ''' <summary>生担</summary>
        ''' <value>生担</value>
        ''' <returns>生担</returns>
        Public Property Setn() As String
            Get
                Return _Setn
            End Get
            Set(ByVal value As String)
                _Setn = value
            End Set
        End Property

        ''' <summary>部品番号１２</summary>
        ''' <value>部品番号１２</value>
        ''' <returns>部品番号１２</returns>
        Public Property Buba12() As String
            Get
                Return _Buba12
            End Get
            Set(ByVal value As String)
                _Buba12 = value
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

        ''' <summary>購担</summary>
        ''' <value>購担</value>
        ''' <returns>購担</returns>
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

        ''' <summary>重保</summary>
        ''' <value>重保</value>
        ''' <returns>重保</returns>
        Public Property Juho() As String
            Get
                Return _Juho
            End Get
            Set(ByVal value As String)
                _Juho = value
            End Set
        End Property

        ''' <summary>新廃区分</summary>
        ''' <value>新廃区分</value>
        ''' <returns>新廃区分</returns>
        Public Property Shkm() As String
            Get
                Return _Shkm
            End Get
            Set(ByVal value As String)
                _Shkm = value
            End Set
        End Property

        ''' <summary>打切マーク</summary>
        ''' <value>打切マーク</value>
        ''' <returns>打切マーク</returns>
        Public Property Uchmk() As String
            Get
                Return _Uchmk
            End Get
            Set(ByVal value As String)
                _Uchmk = value
            End Set
        End Property

        ''' <summary>ＳＮＰ</summary>
        ''' <value>ＳＮＰ</value>
        ''' <returns>ＳＮＰ</returns>
        Public Property Snp() As Nullable(Of Int32)
            Get
                Return _Snp
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Snp = value
            End Set
        End Property

        ''' <summary>納入ロット</summary>
        ''' <value>納入ロット</value>
        ''' <returns>納入ロット</returns>
        Public Property Norot() As Nullable(Of Int32)
            Get
                Return _Norot
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Norot = value
            End Set
        End Property

        ''' <summary>打切余月</summary>
        ''' <value>打切余月</value>
        ''' <returns>打切余月</returns>
        Public Property Uchym() As String
            Get
                Return _Uchym
            End Get
            Set(ByVal value As String)
                _Uchym = value
            End Set
        End Property

        ''' <summary>検査方式</summary>
        ''' <value>検査方式</value>
        ''' <returns>検査方式</returns>
        Public Property Knsk() As String
            Get
                Return _Knsk
            End Get
            Set(ByVal value As String)
                _Knsk = value
            End Set
        End Property

        ''' <summary>検査場所</summary>
        ''' <value>検査場所</value>
        ''' <returns>検査場所</returns>
        Public Property Knbs() As String
            Get
                Return _Knbs
            End Get
            Set(ByVal value As String)
                _Knbs = value
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

        ''' <summary>供給担当</summary>
        ''' <value>供給担当</value>
        ''' <returns>供給担当</returns>
        Public Property Kytn() As String
            Get
                Return _Kytn
            End Get
            Set(ByVal value As String)
                _Kytn = value
            End Set
        End Property

        ''' <summary>先行度</summary>
        ''' <value>先行度</value>
        ''' <returns>先行度</returns>
        Public Property Snko() As String
            Get
                Return _Snko
            End Get
            Set(ByVal value As String)
                _Snko = value
            End Set
        End Property

        ''' <summary>分割コード</summary>
        ''' <value>分割コード</value>
        ''' <returns>分割コード</returns>
        Public Property Bncd() As String
            Get
                Return _Bncd
            End Get
            Set(ByVal value As String)
                _Bncd = value
            End Set
        End Property

        ''' <summary>サイクル</summary>
        ''' <value>サイクル</value>
        ''' <returns>サイクル</returns>
        Public Property Cycl() As String
            Get
                Return _Cycl
            End Get
            Set(ByVal value As String)
                _Cycl = value
            End Set
        End Property

        ''' <summary>予測区分</summary>
        ''' <value>予測区分</value>
        ''' <returns>予測区分</returns>
        Public Property Yskkm() As String
            Get
                Return _Yskkm
            End Get
            Set(ByVal value As String)
                _Yskkm = value
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

        ''' <summary>支給区分</summary>
        ''' <value>支給区分</value>
        ''' <returns>支給区分</returns>
        Public Property Sikm() As String
            Get
                Return _Sikm
            End Get
            Set(ByVal value As String)
                _Sikm = value
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

        ''' <summary>発注数－前月</summary>
        ''' <value>発注数－前月</value>
        ''' <returns>発注数－前月</returns>
        Public Property Hazen() As Nullable(Of Int32)
            Get
                Return _Hazen
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Hazen = value
            End Set
        End Property

        ''' <summary>発注数－非ＯＥＳ</summary>
        ''' <value>発注数－非ＯＥＳ</value>
        ''' <returns>発注数－非ＯＥＳ</returns>
        Public Property Hanoes() As Nullable(Of Int32)
            Get
                Return _Hanoes
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Hanoes = value
            End Set
        End Property

        ''' <summary>発注数－基礎数</summary>
        ''' <value>発注数－基礎数</value>
        ''' <returns>発注数－基礎数</returns>
        Public Property Hakiso() As Nullable(Of Int32)
            Get
                Return _Hakiso
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Hakiso = value
            End Set
        End Property

        ''' <summary>発注数－変動数</summary>
        ''' <value>発注数－変動数</value>
        ''' <returns>発注数－変動数</returns>
        Public Property Hahen() As Nullable(Of Int32)
            Get
                Return _Hahen
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Hahen = value
            End Set
        End Property

        ''' <summary>発注数－次月</summary>
        ''' <value>発注数－次月</value>
        ''' <returns>発注数－次月</returns>
        Public Property Haji() As Nullable(Of Int32)
            Get
                Return _Haji
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Haji = value
            End Set
        End Property

        ''' <summary>発注数－次々月</summary>
        ''' <value>発注数－次々月</value>
        ''' <returns>発注数－次々月</returns>
        Public Property Hajiji() As Nullable(Of Int32)
            Get
                Return _Hajiji
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Hajiji = value
            End Set
        End Property

        ''' <summary>処理対象－年</summary>
        ''' <value>処理対象－年</value>
        ''' <returns>処理対象－年</returns>
        Public Property Sriy() As Nullable(Of Int32)
            Get
                Return _Sriy
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Sriy = value
            End Set
        End Property

        ''' <summary>処理対象－月</summary>
        ''' <value>処理対象－月</value>
        ''' <returns>処理対象－月</returns>
        Public Property Srim() As Nullable(Of Int32)
            Get
                Return _Srim
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Srim = value
            End Set
        End Property

        ''' <summary>前日比</summary>
        ''' <value>前日比</value>
        ''' <returns>前日比</returns>
        Public Property Znhi() As Nullable(Of Int32)
            Get
                Return _Znhi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Znhi = value
            End Set
        End Property

        ''' <summary>小梱包区分</summary>
        ''' <value>小梱包区分</value>
        ''' <returns>小梱包区分</returns>
        Public Property Sknkm() As String
            Get
                Return _Sknkm
            End Get
            Set(ByVal value As String)
                _Sknkm = value
            End Set
        End Property

        ''' <summary>防錆区分</summary>
        ''' <value>防錆区分</value>
        ''' <returns>防錆区分</returns>
        Public Property Bsikm() As String
            Get
                Return _Bsikm
            End Get
            Set(ByVal value As String)
                _Bsikm = value
            End Set
        End Property

        ''' <summary>ユニット製品区分</summary>
        ''' <value>ユニット製品区分</value>
        ''' <returns>ユニット製品区分</returns>
        Public Property Utkm() As String
            Get
                Return _Utkm
            End Get
            Set(ByVal value As String)
                _Utkm = value
            End Set
        End Property

        ''' <summary>バンクストック数</summary>
        ''' <value>バンクストック数</value>
        ''' <returns>バンクストック数</returns>
        Public Property Bnkstk() As String
            Get
                Return _Bnkstk
            End Get
            Set(ByVal value As String)
                _Bnkstk = value
            End Set
        End Property

        ''' <summary>繰越し数</summary>
        ''' <value>繰越し数</value>
        ''' <returns>繰越し数</returns>
        Public Property Krks() As String
            Get
                Return _Krks
            End Get
            Set(ByVal value As String)
                _Krks = value
            End Set
        End Property

        ''' <summary>取引店名</summary>
        ''' <value>取引店名</value>
        ''' <returns>取引店名</returns>
        Public Property Trnm() As String
            Get
                Return _Trnm
            End Get
            Set(ByVal value As String)
                _Trnm = value
            End Set
        End Property

        ''' <summary>梱包工場新設区分</summary>
        ''' <value>梱包工場新設区分</value>
        ''' <returns>梱包工場新設区分</returns>
        Public Property Kpkosn() As String
            Get
                Return _Kpkosn
            End Get
            Set(ByVal value As String)
                _Kpkosn = value
            End Set
        End Property

        ''' <summary>出荷調達区分</summary>
        ''' <value>出荷調達区分</value>
        ''' <returns>出荷調達区分</returns>
        Public Property Sucho() As String
            Get
                Return _Sucho
            End Get
            Set(ByVal value As String)
                _Sucho = value
            End Set
        End Property

        ''' <summary>余白５</summary>
        ''' <value>余白５</value>
        ''' <returns>余白５</returns>
        Public Property Yohk5() As String
            Get
                Return _Yohk5
            End Get
            Set(ByVal value As String)
                _Yohk5 = value
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