Namespace Db.EBom.Vo
    ''' <summary>
    ''' 試作イベントベース車情報（製作一覧）
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuEventBaseSeisakuIchiranVo
        '' 試作イベントコード
        Private _ShisakuEventCode As String
        '' 表示順
        Private _HyojijunNo As Nullable(of Int32)
        '' 試作種別
        Private _ShisakuSyubetu As String
        '' 試作号車
        Private _ShisakuGousya As String
        '' 設計展開選択区分
        Private _SekkeiTenkaiKbn As String
        '' ベース車開発符号
        Private _BaseKaihatsuFugo As String
        '' ベース車仕様情報№
        Private _BaseShiyoujyouhouNo As String
        '' 製作一覧_車種
        Private _SeisakuSyasyu As String
        '' 製作一覧_グレード
        Private _SeisakuGrade As String
        '' 製作一覧_仕向地・仕向け
        Private _SeisakuShimuke As String
        '' 製作一覧_仕向地・ハンドル
        Private _SeisakuHandoru As String
        '' 製作一覧_E/G仕様・排気量
        Private _SeisakuEgHaikiryou As String
        '' 製作一覧_E/G仕様・型式
        Private _SeisakuEgKatashiki As String
        '' 製作一覧_E/G仕様・過給器
        Private _SeisakuEgKakyuuki As String
        '' 製作一覧_T/M仕様・駆動方式
        Private _SeisakuTmKudou As String
        '' 製作一覧_T/M仕様・変速機
        Private _SeisakuTmHensokuki As String
        '' ベース車アプライド№
        Private _BaseAppliedNo As String
        '' ベース車型式
        Private _BaseKatashiki As String
        '' ベース車7桁型式識別コード
        Private _BaseKatashikiScd7 As String
        '' ベース車装備改訂No.
        Private _BaseSobiKaiteiNo As String
        '' ベース車仕向
        Private _BaseShimuke As String
        '' ベース車OP
        Private _BaseOp As String
        '' ベース車外装色
        Private _BaseGaisousyoku As String
        '' ベース車内装色
        Private _BaseNaisousyoku As String
        '' 試作ベースイベントコード
        Private _ShisakuBaseEventCode As String
        '' 試作ベース号車
        Private _ShisakuBaseGousya As String
        '' 製作一覧_車体№
        Private _SeisakuSyataiNo As String
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

        ''' <summary>表示順</summary>
        ''' <value>表示順</value>
        ''' <returns>表示順</returns>
        Public Property HyojijunNo() As Nullable(of Int32)
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Nullable(of Int32))
                _HyojijunNo = value
            End Set
        End Property

        ''' <summary>試作種別</summary>
        ''' <value>試作種別</value>
        ''' <returns>試作種別</returns>
        Public Property ShisakuSyubetu() As String
            Get
                Return _ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                _ShisakuSyubetu = value
            End Set
        End Property

        ''' <summary>試作号車</summary>
        ''' <value>試作号車</value>
        ''' <returns>試作号車</returns>
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

        ''' <summary>設計展開選択区分</summary>
        ''' <value>設計展開選択区分</value>
        ''' <returns>設計展開選択区分</returns>
        Public Property SekkeiTenkaiKbn() As String
            Get
                Return _SekkeiTenkaiKbn
            End Get
            Set(ByVal value As String)
                _SekkeiTenkaiKbn = value
            End Set
        End Property

        ''' <summary>ベース車開発符号</summary>
        ''' <value>ベース車開発符号</value>
        ''' <returns>ベース車開発符号</returns>
        Public Property BaseKaihatsuFugo() As String
            Get
                Return _BaseKaihatsuFugo
            End Get
            Set(ByVal value As String)
                _BaseKaihatsuFugo = value
            End Set
        End Property

        ''' <summary>ベース車仕様情報№</summary>
        ''' <value>ベース車仕様情報№</value>
        ''' <returns>ベース車仕様情報№</returns>
        Public Property BaseShiyoujyouhouNo() As String
            Get
                Return _BaseShiyoujyouhouNo
            End Get
            Set(ByVal value As String)
                _BaseShiyoujyouhouNo = value
            End Set
        End Property

        ''' <summary>製作一覧_車種</summary>
        ''' <value>製作一覧_車種</value>
        ''' <returns>製作一覧_車種</returns>
        Public Property SeisakuSyasyu() As String
            Get
                Return _SeisakuSyasyu
            End Get
            Set(ByVal value As String)
                _SeisakuSyasyu = value
            End Set
        End Property

        ''' <summary>製作一覧_グレード</summary>
        ''' <value>製作一覧_グレード</value>
        ''' <returns>製作一覧_グレード</returns>
        Public Property SeisakuGrade() As String
            Get
                Return _SeisakuGrade
            End Get
            Set(ByVal value As String)
                _SeisakuGrade = value
            End Set
        End Property

        ''' <summary>製作一覧_仕向地・仕向け</summary>
        ''' <value>製作一覧_仕向地・仕向け</value>
        ''' <returns>製作一覧_仕向地・仕向け</returns>
        Public Property SeisakuShimuke() As String
            Get
                Return _SeisakuShimuke
            End Get
            Set(ByVal value As String)
                _SeisakuShimuke = value
            End Set
        End Property

        ''' <summary>製作一覧_仕向地・ﾊﾝﾄﾞﾙ</summary>
        ''' <value>製作一覧_仕向地・ﾊﾝﾄﾞﾙ</value>
        ''' <returns>製作一覧_仕向地・ﾊﾝﾄﾞﾙ</returns>
        Public Property SeisakuHandoru() As String
            Get
                Return _SeisakuHandoru
            End Get
            Set(ByVal value As String)
                _SeisakuHandoru = value
            End Set
        End Property

        ''' <summary>製作一覧_E/G仕様・排気量</summary>
        ''' <value>製作一覧_E/G仕様・排気量</value>
        ''' <returns>製作一覧_E/G仕様・排気量</returns>
        Public Property SeisakuEgHaikiryou() As String
            Get
                Return _SeisakuEgHaikiryou
            End Get
            Set(ByVal value As String)
                _SeisakuEgHaikiryou = value
            End Set
        End Property

        ''' <summary>製作一覧_E/G仕様・型式</summary>
        ''' <value>製作一覧_E/G仕様・型式</value>
        ''' <returns>製作一覧_E/G仕様・型式</returns>
        Public Property SeisakuEgKatashiki() As String
            Get
                Return _SeisakuEgKatashiki
            End Get
            Set(ByVal value As String)
                _SeisakuEgKatashiki = value
            End Set
        End Property

        ''' <summary>製作一覧_E/G仕様・過給器</summary>
        ''' <value>製作一覧_E/G仕様・過給器</value>
        ''' <returns>製作一覧_E/G仕様・過給器</returns>
        Public Property SeisakuEgKakyuuki() As String
            Get
                Return _SeisakuEgKakyuuki
            End Get
            Set(ByVal value As String)
                _SeisakuEgKakyuuki = value
            End Set
        End Property

        ''' <summary>製作一覧_T/M仕様・駆動方式</summary>
        ''' <value>製作一覧_T/M仕様・駆動方式</value>
        ''' <returns>製作一覧_T/M仕様・駆動方式</returns>
        Public Property SeisakuTmKudou() As String
            Get
                Return _SeisakuTmKudou
            End Get
            Set(ByVal value As String)
                _SeisakuTmKudou = value
            End Set
        End Property

        ''' <summary>製作一覧_T/M仕様・変速機</summary>
        ''' <value>製作一覧_T/M仕様・変速機</value>
        ''' <returns>製作一覧_T/M仕様・変速機</returns>
        Public Property SeisakuTmHensokuki() As String
            Get
                Return _SeisakuTmHensokuki
            End Get
            Set(ByVal value As String)
                _SeisakuTmHensokuki = value
            End Set
        End Property

        ''' <summary>ベース車アプライド№</summary>
        ''' <value>ベース車アプライド№</value>
        ''' <returns>ベース車アプライド№</returns>
        Public Property BaseAppliedNo() As String
            Get
                Return _BaseAppliedNo
            End Get
            Set(ByVal value As String)
                _BaseAppliedNo = value
            End Set
        End Property

        ''' <summary>ベース車型式</summary>
        ''' <value>ベース車型式</value>
        ''' <returns>ベース車型式</returns>
        Public Property BaseKatashiki() As String
            Get
                Return _BaseKatashiki
            End Get
            Set(ByVal value As String)
                _BaseKatashiki = value
            End Set
        End Property

        ''' <summary>ベース車7桁型式識別コード</summary>
        ''' <value>ベース車7桁型式識別コード</value>
        ''' <returns>ベース車7桁型式識別コード</returns>
        Public Property BaseKatashikiScd7() As String
            Get
                Return _BaseKatashikiSCD7
            End Get
            Set(ByVal value As String)
                _BaseKatashikiSCD7 = value
            End Set
        End Property

        ''' <summary>ベース車装備改訂№</summary>
        ''' <value>ベース車装備改訂№</value>
        ''' <returns>ベース車装備改訂№</returns>
        Public Property BaseSobiKaiteiNo() As String
            Get
                Return _BaseSobiKaiteiNo
            End Get
            Set(ByVal value As String)
                _BaseSobiKaiteiNo = value
            End Set
        End Property

        ''' <summary>ベース車仕向</summary>
        ''' <value>ベース車仕向</value>
        ''' <returns>ベース車仕向</returns>
        Public Property BaseShimuke() As String
            Get
                Return _BaseShimuke
            End Get
            Set(ByVal value As String)
                _BaseShimuke = value
            End Set
        End Property

        ''' <summary>ベース車OP</summary>
        ''' <value>ベース車OP</value>
        ''' <returns>ベース車OP</returns>
        Public Property BaseOp() As String
            Get
                Return _BaseOp
            End Get
            Set(ByVal value As String)
                _BaseOp = value
            End Set
        End Property

        ''' <summary>ベース車外装色</summary>
        ''' <value>ベース車外装色</value>
        ''' <returns>ベース車外装色</returns>
        Public Property BaseGaisousyoku() As String
            Get
                Return _BaseGaisousyoku
            End Get
            Set(ByVal value As String)
                _BaseGaisousyoku = value
            End Set
        End Property

        ''' <summary>ベース車内装色</summary>
        ''' <value>ベース車内装色</value>
        ''' <returns>ベース車内装色</returns>
        Public Property BaseNaisousyoku() As String
            Get
                Return _BaseNaisousyoku
            End Get
            Set(ByVal value As String)
                _BaseNaisousyoku = value
            End Set
        End Property

        ''' <summary>試作ベースイベントコード</summary>
        ''' <value>試作ベースイベントコード</value>
        ''' <returns>試作ベースイベントコード</returns>
        Public Property ShisakuBaseEventCode() As String
            Get
                Return _ShisakuBaseEventCode
            End Get
            Set(ByVal value As String)
                _ShisakuBaseEventCode = value
            End Set
        End Property

        ''' <summary>試作ベース号車</summary>
        ''' <value>試作ベース号車</value>
        ''' <returns>試作ベース号車</returns>
        Public Property ShisakuBaseGousya() As String
            Get
                Return _ShisakuBaseGousya
            End Get
            Set(ByVal value As String)
                _ShisakuBaseGousya = value
            End Set
        End Property

        ''' <summary>製作一覧_車体№</summary>
        ''' <value>製作一覧_車体№</value>
        ''' <returns>製作一覧_車体№</returns>
        Public Property SeisakuSyataiNo() As String
            Get
                Return _SeisakuSyataiNo
            End Get
            Set(ByVal value As String)
                _SeisakuSyataiNo = value
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
