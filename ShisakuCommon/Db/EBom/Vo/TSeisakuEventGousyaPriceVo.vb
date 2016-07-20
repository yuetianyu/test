Namespace Db.EBom.Vo

    ''' <summary>
    ''' イベント別台当たり単価情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TSeisakuEventGousyaPriceVo
        '発行№
        Private _HakouNo As String
        '最新改訂№
        Private _KaiteiNo As String
        '表示順
        Private _HyojijunNo As Integer
        '試作種別
        Private _ShisakuSyubetu As String
        '号車
        Private _ShisakuGousya As String
        '比例費・トリム予算
        Private _HTYosan As Decimal
        '比例費・トリム見通し
        Private _HTMitoshi As Decimal
        '比例費・メタル予算
        Private _HMYosan As Decimal
        '比例費・メタル見通し
        Private _HMMitoshi As Decimal
        '比例費・加工費予算・合計
        Private _HKakohiYosanTotal As Decimal
        '比例費・加工費予算・MO内製工数
        Private _HKakohiYosanMo As Decimal
        '比例費・加工費予算・SH1内製工数
        Private _HKakohiYosanSh1 As Decimal
        '比例費・加工費予算・工機部依頼工数
        Private _HKakohiYosanKouki As Decimal
        '比例費・加工費予算・生産部依頼工数
        Private _HKakohiYosanSeisan As Decimal
        '比例費・加工費見通し・合計
        Private _HKakohiMitoshiTotal As Decimal
        '比例費・加工費見通し・MO内製工数
        Private _HKakohiMitoshiMo As Decimal
        '比例費・加工費見通し・SH1内製工数
        Private _HKakohiMitoshiSh1 As Decimal
        '比例費・加工費見通し・工機部依頼工数
        Private _HKakohiMitoshiKouki As Decimal
        '比例費・加工費見通し・生産部依頼工数
        Private _HKakohiMitoshiSeisan As Decimal
        '比例費・その他予算・合計
        Private _HSonotaYosanTotal As Decimal
        '比例費・その他予算・MO材料費
        Private _HSonotaYosanMo As Decimal
        '比例費・その他予算・SH1工事外製化
        Private _HSonotaYosanSh1 As Decimal
        '比例費・その他予算・材料
        Private _HSonotaYosanZairyo As Decimal
        '比例費・その他予算・輸送費
        Private _HSonotaYosanYusou As Decimal
        '比例費・その他予算・移管車＆付け替え
        Private _HSonotaYosanIkan As Decimal
        '比例費・その他見通し・合計
        Private _HSonotaMitoshiTotal As Decimal
        '比例費・その他見通し・MO材料費
        Private _HSonotaMitoshiMo As Decimal
        '比例費・その他見通し・SH1工事外製化
        Private _HSonotaMitoshiSh1 As Decimal
        '比例費・その他見通し・材料
        Private _HSonotaMitoshiZairyo As Decimal
        '比例費・その他見通し・輸送費
        Private _HSonotaMitoshiYusou As Decimal
        '比例費・その他見通し・移管車＆付け替え
        Private _HSonotaMitoshiIkan As Decimal
        '固定費・加工費予算・合計
        Private _KKakohiYosanTotal As Decimal
        '固定費・加工費予算・MO内製工数
        Private _KKakohiYosanMo As Decimal
        '固定費・加工費予算・工機内製工数
        Private _KKakohiYosanKouki As Decimal
        '固定費・加工費予算・SH1内製工数
        Private _KKakohiYosanSh1 As Decimal
        '固定費・加工費見通し・合計
        Private _KKakohiMitoshiTotal As Decimal
        '固定費・加工費見通し・MO内製工数
        Private _KKakohiMitoshiMo As Decimal
        '固定費・加工費見通し・工機内製工数
        Private _KKakohiMitoshiKouki As Decimal
        '固定費・加工費見通し・SH1内製工数
        Private _KKakohiMitoshiSh1 As Decimal
        '固定費・その他予算・合計
        Private _KSonotaYosanTotal As Decimal
        '固定費・その他予算・MO直材
        Private _KSonotaYosanMo As Decimal
        '固定費・その他予算・SH1冶具外製化
        Private _KSonotaYosanSh1 As Decimal
        '固定費・その他見通し・合計
        Private _KSonotaMitoshiTotal As Decimal
        '固定費・その他見通し・MO直材
        Private _KSonotaMitoshiMo As Decimal
        '固定費・その他見通し・SH1冶具外製化
        Private _KSonotaMitoshiSh1 As Decimal
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
        '' 登録日
        Private _RegisterDate As String
        '' 登録時間
        Private _RegisterTime As String

        ''' <summary>
        ''' 発行№
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HakouNo() As String
            Get
                Return _HakouNo
            End Get
            Set(ByVal value As String)
                _HakouNo = value
            End Set
        End Property

        ''' <summary>
        ''' 最新改訂№
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KaiteiNo() As String
            Get
                Return _KaiteiNo
            End Get
            Set(ByVal value As String)
                _KaiteiNo = value
            End Set
        End Property

        ''' <summary>
        ''' 表示順
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HyojijunNo() As Integer
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Integer)
                _HyojijunNo = value
            End Set
        End Property

        ''' <summary>
        ''' 試作種別
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ShisakuSyubetu() As String
            Get
                Return _ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                _ShisakuSyubetu = value
            End Set
        End Property

        ''' <summary>
        ''' 号車
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・トリム予算
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HTYosan() As Decimal
            Get
                Return _HTYosan
            End Get
            Set(ByVal value As Decimal)
                _HTYosan = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・トリム見通し
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HTMitoshi() As Decimal
            Get
                Return _HTMitoshi
            End Get
            Set(ByVal value As Decimal)
                _HTMitoshi = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・メタル予算
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HMYosan() As Decimal
            Get
                Return _HMYosan
            End Get
            Set(ByVal value As Decimal)
                _HMYosan = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・メタル見通し
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HMMitoshi() As Decimal
            Get
                Return _HMMitoshi
            End Get
            Set(ByVal value As Decimal)
                _HMMitoshi = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・加工費予算・合計
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HKakohiYosanTotal() As Decimal
            Get
                Return _HKakohiYosanTotal
            End Get
            Set(ByVal value As Decimal)
                _HKakohiYosanTotal = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・加工費予算・MO内製工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HKakohiYosanMo() As Decimal
            Get
                Return _HKakohiYosanMo
            End Get
            Set(ByVal value As Decimal)
                _HKakohiYosanMo = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・加工費予算・SH1内製工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HKakohiYosanSh1() As Decimal
            Get
                Return _HKakohiYosanSh1
            End Get
            Set(ByVal value As Decimal)
                _HKakohiYosanSh1 = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・加工費予算・工機部依頼工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HKakohiYosanKouki() As Decimal
            Get
                Return _HKakohiYosanKouki
            End Get
            Set(ByVal value As Decimal)
                _HKakohiYosanKouki = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・加工費予算・生産部依頼工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HKakohiYosanSeisan() As Decimal
            Get
                Return _HKakohiYosanSeisan
            End Get
            Set(ByVal value As Decimal)
                _HKakohiYosanSeisan = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・加工費見通し・合計
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HKakohiMitoshiTotal() As Decimal
            Get
                Return _HKakohiMitoshiTotal
            End Get
            Set(ByVal value As Decimal)
                _HKakohiMitoshiTotal = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・加工費見通し・MO内製工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HKakohiMitoshiMo() As Decimal
            Get
                Return _HKakohiMitoshiMo
            End Get
            Set(ByVal value As Decimal)
                _HKakohiMitoshiMo = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・加工費見通し・SH1内製工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HKakohiMitoshiSh1() As Decimal
            Get
                Return _HKakohiMitoshiSh1
            End Get
            Set(ByVal value As Decimal)
                _HKakohiMitoshiSh1 = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・加工費見通し・工機部依頼工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HKakohiMitoshiKouki() As Decimal
            Get
                Return _HKakohiMitoshiKouki
            End Get
            Set(ByVal value As Decimal)
                _HKakohiMitoshiKouki = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・加工費見通し・生産部依頼工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HKakohiMitoshiSeisan() As Decimal
            Get
                Return _HKakohiMitoshiSeisan
            End Get
            Set(ByVal value As Decimal)
                _HKakohiMitoshiSeisan = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・その他予算・合計
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HSonotaYosanTotal() As Decimal
            Get
                Return _HSonotaYosanTotal
            End Get
            Set(ByVal value As Decimal)
                _HSonotaYosanTotal = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・その他予算・MO材料費
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HSonotaYosanMo() As Decimal
            Get
                Return _HSonotaYosanMo
            End Get
            Set(ByVal value As Decimal)
                _HSonotaYosanMo = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・その他予算・SH1工事外製化
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HSonotaYosanSh1() As Decimal
            Get
                Return _HSonotaYosanSh1
            End Get
            Set(ByVal value As Decimal)
                _HSonotaYosanSh1 = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・その他予算・材料
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HSonotaYosanZairyo() As Decimal
            Get
                Return _HSonotaYosanZairyo
            End Get
            Set(ByVal value As Decimal)
                _HSonotaYosanZairyo = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・その他予算・輸送費
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HSonotaYosanYusou() As Decimal
            Get
                Return _HSonotaYosanYusou
            End Get
            Set(ByVal value As Decimal)
                _HSonotaYosanYusou = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・その他予算・移管車＆付け替え
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HSonotaYosanIkan() As Decimal
            Get
                Return _HSonotaYosanIkan
            End Get
            Set(ByVal value As Decimal)
                _HSonotaYosanIkan = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・その他見通し・合計
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HSonotaMitoshiTotal() As Decimal
            Get
                Return _HSonotaMitoshiTotal
            End Get
            Set(ByVal value As Decimal)
                _HSonotaMitoshiTotal = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・その他見通し・MO材料費
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HSonotaMitoshiMo() As Decimal
            Get
                Return _HSonotaMitoshiMo
            End Get
            Set(ByVal value As Decimal)
                _HSonotaMitoshiMo = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・その他見通し・SH1工事外製化
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HSonotaMitoshiSh1() As Decimal
            Get
                Return _HSonotaMitoshiSh1
            End Get
            Set(ByVal value As Decimal)
                _HSonotaMitoshiSh1 = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・その他見通し・材料
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HSonotaMitoshiZairyo() As Decimal
            Get
                Return _HSonotaMitoshiZairyo
            End Get
            Set(ByVal value As Decimal)
                _HSonotaMitoshiZairyo = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・その他見通し・輸送費
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HSonotaMitoshiYusou() As Decimal
            Get
                Return _HSonotaMitoshiYusou
            End Get
            Set(ByVal value As Decimal)
                _HSonotaMitoshiYusou = value
            End Set
        End Property

        ''' <summary>
        ''' 比例費・その他見通し・移管車＆付け替え
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property HSonotaMitoshiIkan() As Decimal
            Get
                Return _HSonotaMitoshiIkan
            End Get
            Set(ByVal value As Decimal)
                _HSonotaMitoshiIkan = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・加工費予算・合計
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KKakohiYosanTotal() As Decimal
            Get
                Return _KKakohiYosanTotal
            End Get
            Set(ByVal value As Decimal)
                _KKakohiYosanTotal = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・加工費予算・MO内製工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KKakohiYosanMo() As Decimal
            Get
                Return _KKakohiYosanMo
            End Get
            Set(ByVal value As Decimal)
                _KKakohiYosanMo = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・加工費予算・工機内製工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KKakohiYosanKouki() As Decimal
            Get
                Return _KKakohiYosanKouki
            End Get
            Set(ByVal value As Decimal)
                _KKakohiYosanKouki = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・加工費予算・SH1内製工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KKakohiYosanSh1() As Decimal
            Get
                Return _KKakohiYosanSh1
            End Get
            Set(ByVal value As Decimal)
                _KKakohiYosanSh1 = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・加工費見通し・合計
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KKakohiMitoshiTotal() As Decimal
            Get
                Return _KKakohiMitoshiTotal
            End Get
            Set(ByVal value As Decimal)
                _KKakohiMitoshiTotal = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・加工費見通し・MO内製工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KKakohiMitoshiMo() As Decimal
            Get
                Return _KKakohiMitoshiMo
            End Get
            Set(ByVal value As Decimal)
                _KKakohiMitoshiMo = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・加工費見通し・工機内製工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KKakohiMitoshiKouki() As Decimal
            Get
                Return _KKakohiMitoshiKouki
            End Get
            Set(ByVal value As Decimal)
                _KKakohiMitoshiKouki = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・加工費見通し・SH1内製工数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KKakohiMitoshiSh1() As Decimal
            Get
                Return _KKakohiMitoshiSh1
            End Get
            Set(ByVal value As Decimal)
                _KKakohiMitoshiSh1 = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・その他予算・合計
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KSonotaYosanTotal() As Decimal
            Get
                Return _KSonotaYosanTotal
            End Get
            Set(ByVal value As Decimal)
                _KSonotaYosanTotal = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・その他予算・MO直材
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KSonotaYosanMo() As Decimal
            Get
                Return _KSonotaYosanMo
            End Get
            Set(ByVal value As Decimal)
                _KSonotaYosanMo = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・その他予算・SH1冶具外製化
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KSonotaYosanSh1() As Decimal
            Get
                Return _KSonotaYosanSh1
            End Get
            Set(ByVal value As Decimal)
                _KSonotaYosanSh1 = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・その他見通し・合計
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KSonotaMitoshiTotal() As Decimal
            Get
                Return _KSonotaMitoshiTotal
            End Get
            Set(ByVal value As Decimal)
                _KSonotaMitoshiTotal = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・その他見通し・MO直材
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KSonotaMitoshiMo() As Decimal
            Get
                Return _KSonotaMitoshiMo
            End Get
            Set(ByVal value As Decimal)
                _KSonotaMitoshiMo = value
            End Set
        End Property

        ''' <summary>
        ''' 固定費・その他見通し・SH1冶具外製化
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KSonotaMitoshiSh1() As Decimal
            Get
                Return _KSonotaMitoshiSh1
            End Get
            Set(ByVal value As Decimal)
                _KSonotaMitoshiSh1 = value
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

        ''' <summary>登録日</summary>
        ''' <value>登録日</value>
        ''' <returns>登録日</returns>
        Public Property RegisterDate() As String
            Get
                Return _RegisterDate
            End Get
            Set(ByVal value As String)
                _RegisterDate = value
            End Set
        End Property

        ''' <summary>登録時間</summary>
        ''' <value>登録時間</value>
        ''' <returns>登録時間</returns>
        Public Property RegisterTime() As String
            Get
                Return _RegisterTime
            End Get
            Set(ByVal value As String)
                _RegisterTime = value
            End Set
        End Property

    End Class
End Namespace
