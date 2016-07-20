Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Dao

    Public Class EditBlock2ExcelTitle1BodyVo
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

        ''試作型式
        Private _ShisakuKatashiki As String
        ''試作仕向け
        Private _ShisakuShimuke As String
        ''試作OP
        Private _ShisakuOp As String
        ''試作ハンドル
        Private _ShisakuHandoru As String
        ''試作車型
        Private _ShisakuSyagata As String
        ''試作グレード
        Private _ShisakuGrade As String
        ''試作車台№
        Private _ShisakuSyadaiNo As String
        ''試作外装色
        Private _ShisakuGaisousyoku As String
        ''試作内装色
        Private _ShisakuNaisousyoku As String
        ''試作グループ
        Private _ShisakuGroup As String
        ''試作工指№
        Private _ShisakuKoushiNo As String
        ''試作完成日
        Private _ShisakuKanseibi As String
        ''試作E/G型式
        Private _ShisakuEgKatashiki As String
        ''試作E/G排気量
        Private _ShisakuEgHaikiryou As String
        ''試作E/Gシステム
        Private _ShisakuEgSystem As String
        ''試作E/G過給機
        Private _ShisakuEgKakyuuki As String
        ''試作T/M駆動
        Private _ShisakuTmKudou As String
        ''試作T/M変速機
        Private _ShisakuTmHensokuki As String
        ''試作T/M副変速機
        Private _ShisakuTmFukuHensokuki As String
        ''試作使用部署
        Private _ShisakuSiyouBusyo As String
        ''試作試験目的
        Private _ShisakuShikenMokuteki As String

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
        '' 製作一覧_車体№
        Private _SeisakuSyataiNo As String

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


        ''' <summary>完成仕向地・仕向け</summary>
        Private _ShisakuShimukechiShimuke As String
        ''' <summary>完成試作E/Gメモ１</summary>
        Private _ShisakuEgMemo1 As String
        ''' <summary>完成試作E/Gメモ２</summary>
        Private _ShisakuEgMemo2 As String
        ''' <summary>完成試作T/Mメモ１</summary>
        Private _ShisakuTmMemo1 As String
        ''' <summary>完成試作T/Mメモ２</summary>
        Private _ShisakuTmMemo2 As String
        ''' <summary>完成試作外装色名</summary>
        Private _ShisakuGaisousyokuName As String
        ''' <summary>完成試作内装色名</summary>
        Private _ShisakuNaisousyokuName As String
        ''' <summary>完成試作使用目的</summary>
        Private _ShisakuShiyouMokuteki As String
        ''' <summary>完成試作製作順序</summary>
        Private _ShisakuSeisakuJunjyo As String
        ''' <summary>完成試作製作方法区分</summary>
        Private _ShisakuSeisakuHouhouKbn As String
        ''' <summary>完成試作製作方法</summary>
        Private _ShisakuSeisakuHouhou As String
        ''' <summary>完成試作製作方法</summary>
        Private _ShisakuMemo As String



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
        ''' <summary>試作型式</summary>
        ''' <value>試作型式</value>
        ''' <returns>試作型式</returns>
        Public Property ShisakuKatashiki() As String
            Get
                Return _ShisakuKatashiki
            End Get
            Set(ByVal value As String)
                _ShisakuKatashiki = value
            End Set
        End Property
        ''' <summary>試作仕向け</summary>
        ''' <value>試作仕向け</value>
        ''' <returns>試作仕向け</returns>
        Public Property ShisakuShimuke() As String
            Get
                Return _ShisakuShimuke
            End Get
            Set(ByVal value As String)
                _ShisakuShimuke = value
            End Set
        End Property
        ''' <summary>試作OP</summary>
        ''' <value>試作OP</value>
        ''' <returns>試作OP</returns>
        Public Property ShisakuOp() As String
            Get
                Return _ShisakuOp
            End Get
            Set(ByVal value As String)
                _ShisakuOp = value
            End Set
        End Property
        ''' <summary>試作ハンドル</summary>
        ''' <value>試作ハンドル</value>
        ''' <returns>試作ハンドル</returns>
        Public Property ShisakuHandoru() As String
            Get
                Return _ShisakuHandoru
            End Get
            Set(ByVal value As String)
                _ShisakuHandoru = value
            End Set
        End Property
        ''' <summary>試作車型</summary>
        ''' <value>試作車型</value>
        ''' <returns>試作車型</returns>
        Public Property ShisakuSyagata() As String
            Get
                Return _ShisakuSyagata
            End Get
            Set(ByVal value As String)
                _ShisakuSyagata = value
            End Set
        End Property
        ''' <summary>試作グレード</summary>
        ''' <value>試作グレード</value>
        ''' <returns>試作グレード</returns>
        Public Property ShisakuGrade() As String
            Get
                Return _ShisakuGrade
            End Get
            Set(ByVal value As String)
                _ShisakuGrade = value
            End Set
        End Property
        ''' <summary>試作車台№</summary>
        ''' <value>試作車台№</value>
        ''' <returns>試作車台№</returns>
        Public Property ShisakuSyadaiNo() As String
            Get
                Return _ShisakuSyadaiNo
            End Get
            Set(ByVal value As String)
                _ShisakuSyadaiNo = value
            End Set
        End Property
        ''' <summary>試作外装色</summary>
        ''' <value>試作外装色</value>
        ''' <returns>試作外装色</returns>
        Public Property ShisakuGaisousyoku() As String
            Get
                Return _ShisakuGaisousyoku
            End Get
            Set(ByVal value As String)
                _ShisakuGaisousyoku = value
            End Set
        End Property
        ''' <summary>試作内装色</summary>
        ''' <value>試作内装色</value>
        ''' <returns>試作内装色</returns>
        Public Property ShisakuNaisousyoku() As String
            Get
                Return _ShisakuNaisousyoku
            End Get
            Set(ByVal value As String)
                _ShisakuNaisousyoku = value
            End Set
        End Property
        ''' <summary>試作グループ</summary>
        ''' <value>試作グループ</value>
        ''' <returns>試作グループ</returns>
        Public Property ShisakuGroup() As String
            Get
                Return _ShisakuGroup
            End Get
            Set(ByVal value As String)
                _ShisakuGroup = value
            End Set
        End Property
        ''' <summary>試作工指№</summary>
        ''' <value>試作工指№</value>
        ''' <returns>試作工指№</returns>
        Public Property ShisakuKoushiNo() As String
            Get
                Return _ShisakuKoushiNo
            End Get
            Set(ByVal value As String)
                _ShisakuKoushiNo = value
            End Set
        End Property
        ''' <summary>試作完成日</summary>
        ''' <value>試作完成日</value>
        ''' <returns>試作完成日</returns>
        Public Property ShisakuKanseibi() As String
            Get
                Return _ShisakuKanseibi
            End Get
            Set(ByVal value As String)
                _ShisakuKanseibi = value
            End Set
        End Property

        ''' <summary>試作E/G型式</summary>
        ''' <value>試作E/G型式</value>
        ''' <returns>試作E/G型式</returns>
        Public Property ShisakuEgKatashiki() As String
            Get
                Return _ShisakuEgKatashiki
            End Get
            Set(ByVal value As String)
                _ShisakuEgKatashiki = value
            End Set
        End Property

        ''' <summary>試作E/G排気量</summary>
        ''' <value>試作E/G排気量</value>
        ''' <returns>試作E/G排気量</returns>
        Public Property ShisakuEgHaikiryou() As String
            Get
                Return _ShisakuEgHaikiryou
            End Get
            Set(ByVal value As String)
                _ShisakuEgHaikiryou = value
            End Set
        End Property

        ''' <summary>試作E/Gシステム</summary>
        ''' <value>試作E/Gシステム</value>
        ''' <returns>試作E/Gシステム</returns>
        Public Property ShisakuEgSystem() As String
            Get
                Return _ShisakuEgSystem
            End Get
            Set(ByVal value As String)
                _ShisakuEgSystem = value
            End Set
        End Property

        ''' <summary>試作E/G過給機</summary>
        ''' <value>試作E/G過給機</value>
        ''' <returns>試作E/G過給機</returns>
        Public Property ShisakuEgKakyuuki() As String
            Get
                Return _ShisakuEgKakyuuki
            End Get
            Set(ByVal value As String)
                _ShisakuEgKakyuuki = value
            End Set
        End Property

        ''' <summary>試作T/M駆動</summary>
        ''' <value>試作T/M駆動</value>
        ''' <returns>試作T/M駆動</returns>
        Public Property ShisakuTmKudou() As String
            Get
                Return _ShisakuTmKudou
            End Get
            Set(ByVal value As String)
                _ShisakuTmKudou = value
            End Set
        End Property

        ''' <summary>試作T/M変速機</summary>
        ''' <value>試作T/M変速機</value>
        ''' <returns>試作T/M変速機</returns>
        Public Property ShisakuTmHensokuki() As String
            Get
                Return _ShisakuTmHensokuki
            End Get
            Set(ByVal value As String)
                _ShisakuTmHensokuki = value
            End Set
        End Property

        ''' <summary>試作T/M副変速機</summary>
        ''' <value>試作T/M副変速機</value>
        ''' <returns>試作T/M副変速機</returns>
        Public Property ShisakuTmFukuHensokuki() As String
            Get
                Return _ShisakuTmFukuHensokuki
            End Get
            Set(ByVal value As String)
                _ShisakuTmFukuHensokuki = value
            End Set
        End Property

        ''' <summary>試作使用部署</summary>
        ''' <value>試作使用部署</value>
        ''' <returns>試作使用部署</returns>
        Public Property ShisakuSiyouBusyo() As String
            Get
                Return _ShisakuSiyouBusyo
            End Get
            Set(ByVal value As String)
                _ShisakuSiyouBusyo = value
            End Set
        End Property

        ''' <summary>試作試験目的</summary>
        ''' <value>試作試験目的</value>
        ''' <returns>試作試験目的</returns>
        Public Property ShisakuShikenMokuteki() As String
            Get
                Return _ShisakuShikenMokuteki
            End Get
            Set(ByVal value As String)
                _ShisakuShikenMokuteki = value
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



        ''' <summary>完成仕向地・仕向け</summary>
        ''' <value>完成仕向地・仕向け</value>
        ''' <returns>完成仕向地・仕向け</returns>
        Public Property ShisakuShimukechiShimuke() As String
            Get
                Return _ShisakuShimukechiShimuke
            End Get
            Set(ByVal value As String)
                _ShisakuShimukechiShimuke = value
            End Set
        End Property

        ''' <summary>完成試作E/Gメモ１</summary>
        ''' <value>完成試作E/Gメモ１</value>
        ''' <returns>完成試作E/Gメモ１</returns>
        Public Property ShisakuEgMemo1() As String
            Get
                Return _ShisakuEgMemo1
            End Get
            Set(ByVal value As String)
                _ShisakuEgMemo1 = value
            End Set
        End Property

        ''' <summary>完成試作E/Gメモ２</summary>
        ''' <value>完成試作E/Gメモ２</value>
        ''' <returns>完成試作E/Gメモ２</returns>
        Public Property ShisakuEgMemo2() As String
            Get
                Return _ShisakuEgMemo2
            End Get
            Set(ByVal value As String)
                _ShisakuEgMemo2 = value
            End Set
        End Property

        ''' <summary>完成試作T/Mメモ１</summary>
        ''' <value>完成試作T/Mメモ１</value>
        ''' <returns>完成試作T/Mメモ１</returns>
        Public Property ShisakuTmMemo1() As String
            Get
                Return _ShisakuTmMemo1
            End Get
            Set(ByVal value As String)
                _ShisakuTmMemo1 = value
            End Set
        End Property

        ''' <summary>完成試作T/Mメモ２</summary>
        ''' <value>完成試作T/Mメモ２</value>
        ''' <returns>完成試作T/Mメモ２</returns>
        Public Property ShisakuTmMemo2() As String
            Get
                Return _ShisakuTmMemo2
            End Get
            Set(ByVal value As String)
                _ShisakuTmMemo2 = value
            End Set
        End Property

        ''' <summary>完成試作外装色名</summary>
        ''' <value>完成試作外装色名</value>
        ''' <returns>完成試作外装色名</returns>
        Public Property ShisakuGaisousyokuName() As String
            Get
                Return _ShisakuGaisousyokuName
            End Get
            Set(ByVal value As String)
                _ShisakuGaisousyokuName = value
            End Set
        End Property

        ''' <summary>完成試作内装色名</summary>
        ''' <value>完成試作内装色名</value>
        ''' <returns>完成試作内装色名</returns>
        Public Property ShisakuNaisousyokuName() As String
            Get
                Return _ShisakuNaisousyokuName
            End Get
            Set(ByVal value As String)
                _ShisakuNaisousyokuName = value
            End Set
        End Property

        ''' <summary>完成試作使用目的</summary>
        ''' <value>完成試作使用目的</value>
        ''' <returns>完成試作使用目的</returns>
        Public Property ShisakuShiyouMokuteki() As String
            Get
                Return _ShisakuShiyouMokuteki
            End Get
            Set(ByVal value As String)
                _ShisakuShiyouMokuteki = value
            End Set
        End Property

        ''' <summary>完成試作製作順序</summary>
        ''' <value>完成試作製作順序</value>
        ''' <returns>完成試作製作順序</returns>
        Public Property ShisakuSeisakuJunjyo() As String
            Get
                Return _ShisakuSeisakuJunjyo
            End Get
            Set(ByVal value As String)
                _ShisakuSeisakuJunjyo = value
            End Set
        End Property

        ''' <summary>完成試作製作方法区分</summary>
        ''' <value>完成試作製作方法区分</value>
        ''' <returns>完成試作製作方法区分</returns>
        Public Property ShisakuSeisakuHouhouKbn() As String
            Get
                Return _ShisakuSeisakuHouhouKbn
            End Get
            Set(ByVal value As String)
                _ShisakuSeisakuHouhouKbn = value
            End Set
        End Property

        ''' <summary>完成試作製作方法</summary>
        ''' <value>完成試作製作方法</value>
        ''' <returns>完成試作製作方法</returns>
        Public Property ShisakuSeisakuHouhou() As String
            Get
                Return _ShisakuSeisakuHouhou
            End Get
            Set(ByVal value As String)
                _ShisakuSeisakuHouhou = value
            End Set
        End Property

        ''' <summary>完成試作製作方法</summary>
        ''' <value>完成試作製作方法</value>
        ''' <returns>完成試作製作方法</returns>
        Public Property ShisakuMemo() As String
            Get
                Return _ShisakuMemo
            End Get
            Set(ByVal value As String)
                _ShisakuMemo = value
            End Set
        End Property



    End Class


End Namespace
