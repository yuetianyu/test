Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作部品表ファイル情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AsPRPF02Vo

        ''' <summary>旧リストコード</summary>
        Private _OldListCode As String
        ''' <summary>車種</summary>
        Private _Af As String
        ''' <summary>試作区分</summary>
        Private _Ae As String
        ''' <summary>グループ</summary>
        Private _Gr As String
        ''' <summary>ブロック№</summary>
        Private _Bnba As String
        ''' <summary>工事№</summary>
        Private _Koba As String
        ''' <summary>行ID</summary>
        Private _Gyoid As String
        ''' <summary>管理№</summary>
        Private _Kbba As String
        ''' <summary>関連№</summary>
        Private _Krba As String
        ''' <summary>ページ</summary>
        Private _Pe As String
        ''' <summary>ライン</summary>
        Private _Rn As String
        ''' <summary>履歴</summary>
        Private _Rrk As String
        ''' <summary>管理№フラグ</summary>
        Private _Cxcu As String
        ''' <summary>専用マーク</summary>
        Private _Aamq As String
        ''' <summary>ネック部品マーク</summary>
        Private _Acmq As String
        ''' <summary>レベル</summary>
        Private _Lv As Nullable(Of Int32)
        ''' <summary>ユニット区分</summary>
        Private _Unkm As Char
        ''' <summary>部品番号</summary>
        Private _Buba As String
        ''' <summary>改訂№</summary>
        Private _Kdba As String
        ''' <summary>部品名称</summary>
        Private _Bunm As String
        ''' <summary>図面コード</summary>
        Private _Zmcd As String
        ''' <summary>図面サイズ</summary>
        Private _Zmsi As String
        ''' <summary>図面改訂№</summary>
        Private _Zmkdba As String
        ''' <summary>購坦</summary>
        Private _Ka As String
        ''' <summary>取引先コード</summary>
        Private _Trcd As String
        ''' <summary>段取り</summary>
        Private _Jcda As Nullable(Of Int32)
        ''' <summary>工数</summary>
        Private _Dc As Nullable(Of Int32)
        ''' <summary>予算情報－部品費</summary>
        Private _Yjcl As Nullable(Of Int32)
        ''' <summary>予算情報－型費</summary>
        Private _Yjwh As Nullable(Of Int32)
        ''' <summary>希望単価１０</summary>
        Private _Bota10 As Nullable(Of Int32)
        ''' <summary>設計要求個数</summary>
        Private _Sdyoca As Nullable(Of Int32)
        ''' <summary>設計要求年月日</summary>
        Private _Sdyoym As Nullable(Of Int32)
        ''' <summary>納入場所</summary>
        Private _Nobs As String
        ''' <summary>供給セクション</summary>
        Private _Kysx As String
        ''' <summary>集計コード</summary>
        Private _Skcd As String
        ''' <summary>CKD区分</summary>
        Private _Ckdkbn As String
        ''' <summary>質量</summary>
        Private _Sryo As String
        ''' <summary>設通№</summary>
        Private _Stba As String
        ''' <summary>レコード№</summary>
        Private _Lbba As Nullable(Of Int32)
        ''' <summary>納期</summary>
        Private _Nq As String
        ''' <summary>出図実績－絵無</summary>
        Private _Zpef As String
        ''' <summary>出図実績－絵有</summary>
        Private _Zpeo As String
        ''' <summary>出図実績対象外フラグ</summary>
        Private _Zpgicu As String
        ''' <summary>試作手配個数</summary>
        Private _Sqtica As Nullable(Of Int32)
        ''' <summary>手配記号</summary>
        Private _Tikg As String
        ''' <summary>変更年月日</summary>
        Private _Huym As String
        ''' <summary>備考欄</summary>
        Private _Sdbi As String
        ''' <summary>素材納期</summary>
        Private _Mtnq As String
        ''' <summary>生産出図予定年月日</summary>
        Private _Zqytym As String
        ''' <summary>生産出図－絵無</summary>
        Private _Zqef As String
        ''' <summary>生産出図－絵有</summary>
        Private _Zqeo As String
        ''' <summary>訂正備考欄</summary>
        Private _Sqkk As String
        ''' <summary>材料記号</summary>
        Private _Zrkg As String
        ''' <summary>出図予定年月日</summary>
        Private _Swytym As String
        ''' <summary>出図コード</summary>
        Private _Swcd As String
        ''' <summary>既存区分</summary>
        Private _Kskm As String
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


        ''' <summary>旧リストコード</summary>
        ''' <value>旧リストコード</value>
        ''' <returns>旧リストコード</returns>
        Public Property OldListCode() As String
            Get
                Return _OldListCode
            End Get
            Set(ByVal value As String)
                _OldListCode = value
            End Set
        End Property

        ''' <summary>車種</summary>
        ''' <value>車種</value>
        ''' <returns>車種</returns>
        Public Property Af() As String
            Get
                Return _Af
            End Get
            Set(ByVal value As String)
                _Af = value
            End Set
        End Property

        ''' <summary>試作区分</summary>
        ''' <value>試作区分</value>
        ''' <returns>試作区分</returns>
        Public Property Ae() As String
            Get
                Return _Ae
            End Get
            Set(ByVal value As String)
                _Ae = value
            End Set
        End Property

        ''' <summary>グループ</summary>
        ''' <value>グループ</value>
        ''' <returns>グループ</returns>
        Public Property Gr() As String
            Get
                Return _Gr
            End Get
            Set(ByVal value As String)
                _Gr = value
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

        ''' <summary>関連№</summary>
        ''' <value>関連№</value>
        ''' <returns>関連№</returns>
        Public Property Krba() As String
            Get
                Return _Krba
            End Get
            Set(ByVal value As String)
                _Krba = value
            End Set
        End Property

        ''' <summary>ページ</summary>
        ''' <value>ページ</value>
        ''' <returns>ページ</returns>
        Public Property Pe() As String
            Get
                Return _Pe
            End Get
            Set(ByVal value As String)
                _Pe = value
            End Set
        End Property

        ''' <summary>ライン</summary>
        ''' <value>ライン</value>
        ''' <returns>ライン</returns>
        Public Property Rn() As String
            Get
                Return _Rn
            End Get
            Set(ByVal value As String)
                _Rn = value
            End Set
        End Property

        ''' <summary>履歴</summary>
        ''' <value>履歴</value>
        ''' <returns>履歴</returns>
        Public Property Rrk() As String
            Get
                Return _Rrk
            End Get
            Set(ByVal value As String)
                _Rrk = value
            End Set
        End Property

        ''' <summary>管理№フラグ</summary>
        ''' <value>管理№フラグ</value>
        ''' <returns>管理№フラグ</returns>
        Public Property Cxcu() As String
            Get
                Return _Cxcu
            End Get
            Set(ByVal value As String)
                _Cxcu = value
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

        ''' <summary>ネック部品マーク</summary>
        ''' <value>ネック部品マーク</value>
        ''' <returns>ネック部品マーク</returns>
        Public Property Acmq() As String
            Get
                Return _Acmq
            End Get
            Set(ByVal value As String)
                _Acmq = value
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

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public Property Buba() As String
            Get
                Return _Buba
            End Get
            Set(ByVal value As String)
                _Buba = value
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

        ''' <summary>図面コード</summary>
        ''' <value>図面コード</value>
        ''' <returns>図面コード</returns>
        Public Property Zmcd() As String
            Get
                Return _Zmcd
            End Get
            Set(ByVal value As String)
                _Zmcd = value
            End Set
        End Property

        ''' <summary>図面サイズ</summary>
        ''' <value>図面サイズ</value>
        ''' <returns>図面サイズ</returns>
        Public Property Zmsi() As String
            Get
                Return _Zmsi
            End Get
            Set(ByVal value As String)
                _Zmsi = value
            End Set
        End Property

        ''' <summary>図面改訂№</summary>
        ''' <value>図面改訂№</value>
        ''' <returns>図面改訂№</returns>
        Public Property Zmkdba() As String
            Get
                Return _Zmkdba
            End Get
            Set(ByVal value As String)
                _Zmkdba = value
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

        ''' <summary>段取り</summary>
        ''' <value>段取り</value>
        ''' <returns>段取り</returns>
        Public Property Jcda() As Nullable(Of Int32)
            Get
                Return _Jcda
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Jcda = value
            End Set
        End Property

        ''' <summary>工数</summary>
        ''' <value>工数</value>
        ''' <returns>工数</returns>
        Public Property Dc() As Nullable(Of Int32)
            Get
                Return _Dc
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Dc = value
            End Set
        End Property

        ''' <summary>予算情報－部品費</summary>
        ''' <value>予算情報－部品費</value>
        ''' <returns>予算情報－部品費</returns>
        Public Property Yjcl() As Nullable(Of Int32)
            Get
                Return _Yjcl
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Yjcl = value
            End Set
        End Property

        ''' <summary>予算情報－型費</summary>
        ''' <value>予算情報－型費</value>
        ''' <returns>予算情報－型費</returns>
        Public Property Yjwh() As Nullable(Of Int32)
            Get
                Return _Yjwh
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Yjwh = value
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

        ''' <summary>設計要求個数</summary>
        ''' <value>設計要求個数</value>
        ''' <returns>設計要求個数</returns>
        Public Property Sdyoca() As Nullable(Of Int32)
            Get
                Return _Sdyoca
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Sdyoca = value
            End Set
        End Property

        ''' <summary>設計要求年月日</summary>
        ''' <value>設計要求年月日</value>
        ''' <returns>設計要求年月日</returns>
        Public Property Sdyoym() As Nullable(Of Int32)
            Get
                Return _Sdyoym
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Sdyoym = value
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

        ''' <summary>集計コード</summary>
        ''' <value>集計コード</value>
        ''' <returns>集計コード</returns>
        Public Property Skcd() As String
            Get
                Return _Skcd
            End Get
            Set(ByVal value As String)
                _Skcd = value
            End Set
        End Property

        ''' <summary>CKD区分</summary>
        ''' <value>CKD区分</value>
        ''' <returns>CKD区分</returns>
        Public Property Ckdkbn() As String
            Get
                Return _Ckdkbn
            End Get
            Set(ByVal value As String)
                _Ckdkbn = value
            End Set
        End Property

        ''' <summary>質量</summary>
        ''' <value>質量</value>
        ''' <returns>質量</returns>
        Public Property Sryo() As String
            Get
                Return _Sryo
            End Get
            Set(ByVal value As String)
                _Sryo = value
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

        ''' <summary>レコード№</summary>
        ''' <value>レコード№</value>
        ''' <returns>レコード№</returns>
        Public Property Lbba() As Nullable(Of Int32)
            Get
                Return _Lbba
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Lbba = value
            End Set
        End Property

        ''' <summary>納期</summary>
        ''' <value>納期</value>
        ''' <returns>納期</returns>
        Public Property Nq() As String
            Get
                Return _Nq
            End Get
            Set(ByVal value As String)
                _Nq = value
            End Set
        End Property

        ''' <summary>出図実績－絵無</summary>
        ''' <value>出図実績－絵無</value>
        ''' <returns>出図実績－絵無</returns>
        Public Property Zpef() As String
            Get
                Return _Zpef
            End Get
            Set(ByVal value As String)
                _Zpef = value
            End Set
        End Property

        ''' <summary>出図実績－絵有</summary>
        ''' <value>出図実績－絵有</value>
        ''' <returns>出図実績－絵有</returns>
        Public Property Zpeo() As String
            Get
                Return _Zpeo
            End Get
            Set(ByVal value As String)
                _Zpeo = value
            End Set
        End Property

        ''' <summary>出図実績対象外フラグ</summary>
        ''' <value>出図実績対象外フラグ</value>
        ''' <returns>出図実績対象外フラグ</returns>
        Public Property Zpgicu() As String
            Get
                Return _Zpgicu
            End Get
            Set(ByVal value As String)
                _Zpgicu = value
            End Set
        End Property

        ''' <summary>試作手配個数</summary>
        ''' <value>試作手配個数</value>
        ''' <returns>試作手配個数</returns>
        Public Property Sqtica() As Nullable(Of Int32)
            Get
                Return _Sqtica
            End Get
            Set(ByVal value As Nullable(Of Int32))
                _Sqtica = value
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

        ''' <summary>変更年月日</summary>
        ''' <value>変更年月日</value>
        ''' <returns>変更年月日</returns>
        Public Property Huym() As String
            Get
                Return _Huym
            End Get
            Set(ByVal value As String)
                _Huym = value
            End Set
        End Property

        ''' <summary>備考欄</summary>
        ''' <value>備考欄</value>
        ''' <returns>備考欄</returns>
        Public Property Sdbi() As String
            Get
                Return _Sdbi
            End Get
            Set(ByVal value As String)
                _Sdbi = value
            End Set
        End Property

        ''' <summary>素材納期</summary>
        ''' <value>素材納期</value>
        ''' <returns>素材納期</returns>
        Public Property Mtnq() As String
            Get
                Return _Mtnq
            End Get
            Set(ByVal value As String)
                _Mtnq = value
            End Set
        End Property

        ''' <summary>生産出図予定年月日</summary>
        ''' <value>生産出図予定年月日</value>
        ''' <returns>生産出図予定年月日</returns>
        Public Property Zqytym() As String
            Get
                Return _Zqytym
            End Get
            Set(ByVal value As String)
                _Zqytym = value
            End Set
        End Property

        ''' <summary>生産出図－絵無</summary>
        ''' <value>生産出図－絵無</value>
        ''' <returns>生産出図－絵無</returns>
        Public Property Zqef() As String
            Get
                Return _Zqef
            End Get
            Set(ByVal value As String)
                _Zqef = value
            End Set
        End Property

        ''' <summary>生産出図－絵有</summary>
        ''' <value>生産出図－絵有</value>
        ''' <returns>生産出図－絵有</returns>
        Public Property Zqeo() As String
            Get
                Return _Zqeo
            End Get
            Set(ByVal value As String)
                _Zqeo = value
            End Set
        End Property

        ''' <summary>訂正備考欄</summary>
        ''' <value>訂正備考欄</value>
        ''' <returns>訂正備考欄</returns>
        Public Property Sqkk() As String
            Get
                Return _Sqkk
            End Get
            Set(ByVal value As String)
                _Sqkk = value
            End Set
        End Property

        ''' <summary>材料記号</summary>
        ''' <value>材料記号</value>
        ''' <returns>材料記号</returns>
        Public Property Zrkg() As String
            Get
                Return _Zrkg
            End Get
            Set(ByVal value As String)
                _Zrkg = value
            End Set
        End Property

        ''' <summary>出図予定年月日</summary>
        ''' <value>出図予定年月日</value>
        ''' <returns>出図予定年月日</returns>
        Public Property Swytym() As String
            Get
                Return _Swytym
            End Get
            Set(ByVal value As String)
                _Swytym = value
            End Set
        End Property

        ''' <summary>出図コード</summary>
        ''' <value>出図コード</value>
        ''' <returns>出図コード</returns>
        Public Property Swcd() As String
            Get
                Return _Swcd
            End Get
            Set(ByVal value As String)
                _Swcd = value
            End Set
        End Property

        ''' <summary>既存区分</summary>
        ''' <value>既存区分</value>
        ''' <returns>既存区分</returns>
        Public Property Kskm() As String
            Get
                Return _Kskm
            End Get
            Set(ByVal value As String)
                _Kskm = value
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