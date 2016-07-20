Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Al.Dao

    ''' <summary>
    ''' ベース車情報・完成車情報をもつVO
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditAlEventVo : Inherits TShisakuEventKanseiVo
        '' 試作号車
        Private _ShisakuGousya As String
        '' ベース車開発符号
        Private _BaseKaihatsuFugo As String
        '' ベース車仕様情報№
        Private _BaseShiyoujyouhouNo As String
        '' ベース車アプライド№
        Private _BaseAppliedNo As String
        '' ベース車型式
        Private _BaseKatashiki As String
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
        '' 試作種別
        Private _ShisakuSyubetu As String  '2012-01-11

        '' 製作一覧_車種
        Private _BaseSeisakuSyasyu As String
        '' 製作一覧_グレード
        Private _BaseSeisakuGrade As String
        '' 製作一覧_仕向地・仕向け
        Private _BaseSeisakuShimuke As String
        '' 製作一覧_仕向地・ハンドル
        Private _BaseSeisakuHandoru As String
        '' 製作一覧_E/G仕様・排気量
        Private _BaseSeisakuEgHaikiryou As String
        '' 製作一覧_E/G仕様・型式
        Private _BaseSeisakuEgKatashiki As String
        '' 製作一覧_E/G仕様・過給器
        Private _BaseSeisakuEgKakyuuki As String
        '' 製作一覧_T/M仕様・駆動方式
        Private _BaseSeisakuTmKudou As String
        '' 製作一覧_T/M仕様・変速機
        Private _BaseSeisakuTmHensokuki As String
        '' 製作一覧_車体№
        Private _ShisakuSeisakuSyataiNo As String


        '' 設計展開ベース車開発符号
        Private _TenkaiBaseKaihatsuFugo As String
        '' 設計展開ベース車仕様情報№
        Private _TenkaiBaseShiyoujyouhouNo As String
        '' 設計展開ベース車アプライド№
        Private _TenkaiBaseAppliedNo As String
        '' 設計展開ベース車型式
        Private _TenkaiBaseKatashiki As String
        '' 設計展開ベース車仕向
        Private _TenkaiBaseShimuke As String
        '' 設計展開ベース車OP
        Private _TenkaiBaseOp As String
        '' 設計展開ベース車外装色
        Private _TenkaiBaseGaisousyoku As String
        '' 設計展開ベース車内装色
        Private _TenkaiBaseNaisousyoku As String
        '' 設計展開試作ベースイベントコード
        Private _TenkaiShisakuBaseEventCode As String
        '' 設計展開試作ベース号車
        Private _TenkaiShisakuBaseGousya As String



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

        ''' 2012-01-11
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


        ''' <summary>製作一覧_車種</summary>
        ''' <value>製作一覧_車種</value>
        ''' <returns>製作一覧_車種</returns>
        Public Property BaseSeisakuSyasyu() As String
            Get
                Return _BaseSeisakuSyasyu
            End Get
            Set(ByVal value As String)
                _BaseSeisakuSyasyu = value
            End Set
        End Property

        ''' <summary>製作一覧_グレード</summary>
        ''' <value>製作一覧_グレード</value>
        ''' <returns>製作一覧_グレード</returns>
        Public Property BaseSeisakuGrade() As String
            Get
                Return _BaseSeisakuGrade
            End Get
            Set(ByVal value As String)
                _BaseSeisakuGrade = value
            End Set
        End Property

        ''' <summary>製作一覧_仕向地・仕向け</summary>
        ''' <value>製作一覧_仕向地・仕向け</value>
        ''' <returns>製作一覧_仕向地・仕向け</returns>
        Public Property BaseSeisakuShimuke() As String
            Get
                Return _BaseSeisakuShimuke
            End Get
            Set(ByVal value As String)
                _BaseSeisakuShimuke = value
            End Set
        End Property

        ''' <summary>製作一覧_仕向地・ﾊﾝﾄﾞﾙ</summary>
        ''' <value>製作一覧_仕向地・ﾊﾝﾄﾞﾙ</value>
        ''' <returns>製作一覧_仕向地・ﾊﾝﾄﾞﾙ</returns>
        Public Property BaseSeisakuHandoru() As String
            Get
                Return _BaseSeisakuHandoru
            End Get
            Set(ByVal value As String)
                _BaseSeisakuHandoru = value
            End Set
        End Property

        ''' <summary>製作一覧_E/G仕様・排気量</summary>
        ''' <value>製作一覧_E/G仕様・排気量</value>
        ''' <returns>製作一覧_E/G仕様・排気量</returns>
        Public Property BaseSeisakuEgHaikiryou() As String
            Get
                Return _BaseSeisakuEgHaikiryou
            End Get
            Set(ByVal value As String)
                _BaseSeisakuEgHaikiryou = value
            End Set
        End Property

        ''' <summary>製作一覧_E/G仕様・型式</summary>
        ''' <value>製作一覧_E/G仕様・型式</value>
        ''' <returns>製作一覧_E/G仕様・型式</returns>
        Public Property BaseSeisakuEgKatashiki() As String
            Get
                Return _BaseSeisakuEgKatashiki
            End Get
            Set(ByVal value As String)
                _BaseSeisakuEgKatashiki = value
            End Set
        End Property

        ''' <summary>製作一覧_E/G仕様・過給器</summary>
        ''' <value>製作一覧_E/G仕様・過給器</value>
        ''' <returns>製作一覧_E/G仕様・過給器</returns>
        Public Property BaseSeisakuEgKakyuuki() As String
            Get
                Return _BaseSeisakuEgKakyuuki
            End Get
            Set(ByVal value As String)
                _BaseSeisakuEgKakyuuki = value
            End Set
        End Property

        ''' <summary>製作一覧_T/M仕様・駆動方式</summary>
        ''' <value>製作一覧_T/M仕様・駆動方式</value>
        ''' <returns>製作一覧_T/M仕様・駆動方式</returns>
        Public Property BaseSeisakuTmKudou() As String
            Get
                Return _BaseSeisakuTmKudou
            End Get
            Set(ByVal value As String)
                _BaseSeisakuTmKudou = value
            End Set
        End Property

        ''' <summary>製作一覧_T/M仕様・変速機</summary>
        ''' <value>製作一覧_T/M仕様・変速機</value>
        ''' <returns>製作一覧_T/M仕様・変速機</returns>
        Public Property BaseSeisakuTmHensokuki() As String
            Get
                Return _BaseSeisakuTmHensokuki
            End Get
            Set(ByVal value As String)
                _BaseSeisakuTmHensokuki = value
            End Set
        End Property

        ''' <summary>製作一覧_車体№</summary>
        ''' <value>製作一覧_車体№</value>
        ''' <returns>製作一覧_車体№</returns>
        Public Property ShisakuSeisakuSyataiNo() As String
            Get
                Return _ShisakuSeisakuSyataiNo
            End Get
            Set(ByVal value As String)
                _ShisakuSeisakuSyataiNo = value
            End Set
        End Property



        ''' <summary>設計展開ベース車開発符号</summary>
        ''' <value>設計展開ベース車開発符号</value>
        ''' <returns>設計展開ベース車開発符号</returns>
        Public Property TenkaiBaseKaihatsuFugo() As String
            Get
                Return _TenkaiBaseKaihatsuFugo
            End Get
            Set(ByVal value As String)
                _TenkaiBaseKaihatsuFugo = value
            End Set
        End Property

        ''' <summary>設計展開ベース車仕様情報№</summary>
        ''' <value>設計展開ベース車仕様情報№</value>
        ''' <returns>設計展開ベース車仕様情報№</returns>
        Public Property TenkaiBaseShiyoujyouhouNo() As String
            Get
                Return _TenkaiBaseShiyoujyouhouNo
            End Get
            Set(ByVal value As String)
                _TenkaiBaseShiyoujyouhouNo = value
            End Set
        End Property

        ''' <summary>設計展開ベース車アプライド№</summary>
        ''' <value>設計展開ベース車アプライド№</value>
        ''' <returns>設計展開ベース車アプライド№</returns>
        Public Property TenkaiBaseAppliedNo() As String
            Get
                Return _TenkaiBaseAppliedNo
            End Get
            Set(ByVal value As String)
                _TenkaiBaseAppliedNo = value
            End Set
        End Property

        ''' <summary>設計展開ベース車型式</summary>
        ''' <value>設計展開ベース車型式</value>
        ''' <returns>設計展開ベース車型式</returns>
        Public Property TenkaiBaseKatashiki() As String
            Get
                Return _TenkaiBaseKatashiki
            End Get
            Set(ByVal value As String)
                _TenkaiBaseKatashiki = value
            End Set
        End Property

        ''' <summary>設計展開ベース車仕向</summary>
        ''' <value>設計展開ベース車仕向</value>
        ''' <returns>設計展開ベース車仕向</returns>
        Public Property TenkaiBaseShimuke() As String
            Get
                Return _TenkaiBaseShimuke
            End Get
            Set(ByVal value As String)
                _TenkaiBaseShimuke = value
            End Set
        End Property

        ''' <summary>設計展開ベース車OP</summary>
        ''' <value>設計展開ベース車OP</value>
        ''' <returns>設計展開ベース車OP</returns>
        Public Property TenkaiBaseOp() As String
            Get
                Return _TenkaiBaseOp
            End Get
            Set(ByVal value As String)
                _TenkaiBaseOp = value
            End Set
        End Property

        ''' <summary>設計展開ベース車外装色</summary>
        ''' <value>設計展開ベース車外装色</value>
        ''' <returns>設計展開ベース車外装色</returns>
        Public Property TenkaiBaseGaisousyoku() As String
            Get
                Return _TenkaiBaseGaisousyoku
            End Get
            Set(ByVal value As String)
                _TenkaiBaseGaisousyoku = value
            End Set
        End Property

        ''' <summary>設計展開ベース車内装色</summary>
        ''' <value>設計展開ベース車内装色</value>
        ''' <returns>設計展開ベース車内装色</returns>
        Public Property TenkaiBaseNaisousyoku() As String
            Get
                Return _TenkaiBaseNaisousyoku
            End Get
            Set(ByVal value As String)
                _TenkaiBaseNaisousyoku = value
            End Set
        End Property

        ''' <summary>設計展開試作ベースイベントコード</summary>
        ''' <value>設計展開試作ベースイベントコード</value>
        ''' <returns>設計展開試作ベースイベントコード</returns>
        Public Property TenkaiShisakuBaseEventCode() As String
            Get
                Return _TenkaiShisakuBaseEventCode
            End Get
            Set(ByVal value As String)
                _TenkaiShisakuBaseEventCode = value
            End Set
        End Property

        ''' <summary>設計展開試作ベース号車</summary>
        ''' <value>設計展開試作ベース号車</value>
        ''' <returns>設計展開試作ベース号車</returns>
        Public Property TenkaiShisakuBaseGousya() As String
            Get
                Return _TenkaiShisakuBaseGousya
            End Get
            Set(ByVal value As String)
                _TenkaiShisakuBaseGousya = value
            End Set
        End Property



    End Class
End Namespace