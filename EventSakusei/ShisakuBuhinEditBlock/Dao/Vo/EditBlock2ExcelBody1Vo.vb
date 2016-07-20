Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Dao


    Public Class EditBlock2ExcelBody1Vo : Inherits TShisakuSekkeiBlockInstlVo
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
        Private _ShisakuOp As String
        ''試作OP
        Private _ShisakuShimuke As String
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

    End Class

End Namespace