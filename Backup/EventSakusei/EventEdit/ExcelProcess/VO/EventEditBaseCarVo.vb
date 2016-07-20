Namespace EventEdit.Vo
    ''' <summary>
    ''' ベース車
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventEditBaseCarVo
        ''' <summary>
        ''' 試作種別
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuSyubetu As String
        ''' <summary>
        ''' 試作号車
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuGousya As String
        ''' <summary>
        ''' 設計展開選択区分
        ''' </summary>
        ''' <remarks></remarks>
        Private _SekkeiTenkaiKbn As String
        ''' <summary>
        ''' 車開発符号
        ''' </summary>
        ''' <remarks></remarks>
        Private _BaseKaihatsuFugo As String
        ''' <summary>
        ''' ベース車仕様情報№
        ''' </summary>
        ''' <remarks></remarks>
        Private _BaseShiyoujyouhouNo As String
        ''' <summary>
        ''' 製作一覧_車種
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakuSyasyu As String
        ''' <summary>
        ''' 製作一覧_グレード
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakuGrade As String
        ''' <summary>
        ''' 製作一覧_仕向地・仕向け
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakuShimuke As String
        ''' <summary>
        ''' 製作一覧_仕向地・ハンドル
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakuHandoru As String
        ''' <summary>
        ''' 製作一覧_E/G仕様・排気量
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakuEgHaikiryou As String
        ''' <summary>
        ''' 製作一覧_E/G仕様・型式
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakuEgKatashiki As String
        ''' <summary>
        ''' 製作一覧_E/G仕様・過給器
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakuEgKakyuuki As String
        ''' <summary>
        ''' 製作一覧_T/M仕様・駆動方式
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakuTmKudou As String
        ''' <summary>
        ''' 製作一覧_T/M仕様・変速機
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakuTmHensokuki As String
        ''' <summary>
        ''' ベース車アプライド№
        ''' </summary>
        ''' <remarks></remarks>
        Private _BaseAppliedNo As String
        ''' <summary>
        '''  ベース車型式
        ''' </summary>
        ''' <remarks></remarks>
        Private _BaseKatashiki As String
        ''' <summary>
        ''' ベース車仕向
        ''' </summary>
        ''' <remarks></remarks>
        Private _BaseShimuke As String
        ''' <summary>
        ''' ベース車OP
        ''' </summary>
        ''' <remarks></remarks>
        Private _BaseOp As String
        ''' <summary>
        ''' ベース車外装色
        ''' </summary>
        ''' <remarks></remarks>
        Private _BaseGaisousyoku As String
        ''' <summary>
        ''' ベース車内装色
        ''' </summary>
        ''' <remarks></remarks>
        Private _BaseNaisousyoku As String
        ''' <summary>
        ''' 試作ベースイベントコード
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuBaseEventCode As String
        ''' <summary>
        ''' 試作ベース号車
        ''' </summary>
        ''' <remarks></remarks>
        Private _ShisakuBaseGousya As String
        ''' <summary>
        ''' 製作一覧・車体№
        ''' </summary>
        ''' <remarks></remarks>
        Private _SeisakuSyataiNo As String


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

        ''' <summary>製作車体№</summary>
        ''' <value>製作車体№</value>
        ''' <returns>製作車体№</returns>
        Public Property SeisakuSyataiNo() As String
            Get
                Return _SeisakuSyataiNo
            End Get
            Set(ByVal value As String)
                _SeisakuSyataiNo = value
            End Set
        End Property

    End Class
End Namespace
